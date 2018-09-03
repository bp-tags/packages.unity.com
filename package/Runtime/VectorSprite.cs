using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.U2D;
using UnityEngine.Experimental.U2D;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine.Experimental.Rendering;

namespace Unity.VectorGraphics
{
    public static partial class VectorUtils
    {
        /// <summary>Sprite alignment.</summary>
        public enum Alignment
        {
            Center = 0,
            TopLeft = 1,
            TopCenter = 2,
            TopRight = 3,
            LeftCenter = 4,
            RightCenter = 5,
            BottomLeft = 6,
            BottomCenter = 7,
            BottomRight = 8,
            SVGOrigin = 9,
            Custom = 10
        }

        private struct ShapeRange
        {
            public int start;
            public int end;
        }

        /// <summary>Builds a sprite asset from a scene tessellation.</summary>
        /// <param name="geoms">The list of tessellated Geometry instances</param>
        /// <param name="svgPixelsPerUnit">How many SVG "pixels" map into a Unity unit</param>
        /// <param name="alignment">The position of the sprite origin</param>
        /// <param name="customPivot">If alignment is Custom, customPivot is used to compute the sprite origin</param>
        /// <param name="gradientResolution">The maximum size of the texture holding gradient data</param>
        /// <returns>A new Sprite containing the provided geometry. The Sprite may have a texture if the geometry has any texture and/or gradients</returns>
        public static Sprite BuildSprite(List<Geometry> geoms, float svgPixelsPerUnit, Alignment alignment, Vector2 customPivot, UInt16 gradientResolution)
        {
            // Generate atlas
            var texAtlas = GenerateAtlasAndFillItsUVs(geoms, gradientResolution);

            int totalVerts = 0, totalIndices = 0;
            foreach (var geom in geoms)
            {
                if (geom.indices.Length != 0)
                {
                    totalIndices += geom.indices.Length;
                    totalVerts += geom.vertices.Length;
                }
            }

            var vertices = new List<Vector2>(totalVerts);
            var indices = new List<UInt16>(totalIndices);
            var colors = new List<Color>(totalVerts);
            var uvs = (texAtlas != null) ? new List<Vector2>(totalVerts) : null;
            var settingIndices = (texAtlas != null) ? new List<Vector2>(totalVerts) : null;

            var shapeRanges = new List<ShapeRange>();
            foreach (var geom in geoms)
            {
                shapeRanges.Add(new ShapeRange()
                {
                    start = indices.Count,
                    end = indices.Count + geom.indices.Length - 1
                });

                indices.AddRange(geom.indices.Select(x => (UInt16)(x + vertices.Count)));
                vertices.AddRange(geom.vertices.Select(x => geom.worldTransform * x));
                colors.AddRange(Enumerable.Repeat(geom.color, geom.vertices.Length));
                System.Diagnostics.Debug.Assert(uvs == null || geom.uvs != null);
                if (uvs != null)
                {
                    uvs.AddRange(geom.uvs);
                    for (int i = 0; i < geom.uvs.Length; i++)
                        settingIndices.Add(new Vector2(geom.settingIndex, 0));
                }
            }

            var bbox = VectorUtils.RealignVerticesInBounds(vertices, true);
            var rect = new Rect(0, 0, bbox.width, bbox.height);
            var pivot = GetPivot(alignment, customPivot, bbox);

            // Adjust the winding order of the shapes to be consistent since some shapes can be reversed
            // for hole-cutting purposes.
            foreach (var range in shapeRanges)
                FlipShapeIfNecessary(range, vertices, indices);

            // The Sprite.Create(Rect, Vector2, float, Texture2D) method is internal. Using reflection
            // until it becomes public.
            var spriteCreateMethod = typeof(Sprite).GetMethod("Create", BindingFlags.Static | BindingFlags.NonPublic, Type.DefaultBinder, new Type[] { typeof(Rect), typeof(Vector2), typeof(float), typeof(Texture2D) }, null);
            var sprite = spriteCreateMethod.Invoke(null, new object[] { rect, pivot, svgPixelsPerUnit, texAtlas }) as Sprite;

            sprite.OverrideGeometry(vertices.ToArray(), indices.ToArray());

            if (colors != null)
            {
                var colors32 = colors.Select(c => (Color32)c);
                using (var nativeColors = new NativeArray<Color32>(colors32.ToArray(), Allocator.Temp))
                    sprite.SetVertexAttribute<Color32>(VertexAttribute.Color, nativeColors);
            }
            if (uvs != null)
            {
                using (var nativeUVs = new NativeArray<Vector2>(uvs.ToArray(), Allocator.Temp))
                    sprite.SetVertexAttribute<Vector2>(VertexAttribute.TexCoord0, nativeUVs);
                using (var nativeSettingIndices = new NativeArray<Vector2>(settingIndices.ToArray(), Allocator.Temp))
                    sprite.SetVertexAttribute<Vector2>(VertexAttribute.TexCoord2, nativeSettingIndices);
            }

            return sprite;
        }

        /// <summary>Draws a vector sprite using the provided material.</summary>
        /// <param name="sprite">The sprite to render</param>
        /// <param name="mat">The material used for rendering</param>
        public static void RenderSprite(Sprite sprite, Material mat) 
        {
            float spriteWidth = sprite.rect.width;
            float spriteHeight = sprite.rect.height;
            float pixelsToUnits = sprite.rect.width / sprite.bounds.size.x;

            var vertices = sprite.vertices;
            var uvs = sprite.uv;
            var triangles = sprite.triangles;
            var pivot = sprite.pivot;

            NativeSlice<Color32>? colors = null;
            if (sprite.HasVertexAttribute(VertexAttribute.Color))
                colors = sprite.GetVertexAttribute<Color32>(VertexAttribute.Color);

            NativeSlice<Vector2>? settings = null;
            if (sprite.HasVertexAttribute(VertexAttribute.TexCoord2))
                settings = sprite.GetVertexAttribute<Vector2>(VertexAttribute.TexCoord2);

            mat.SetTexture("_MainTex", sprite.texture);
            mat.SetPass(0);

            GL.Clear(true, true, new Color(0f, 0f, 0f, 0f));
            GL.PushMatrix();
            GL.LoadOrtho();
            GL.Color(new Color(1, 1, 1, 1));
            GL.Begin(GL.TRIANGLES);
            for (int i = 0; i < triangles.Length; ++i)
            {
                ushort index = triangles[i];
                Vector2 vertex = vertices[index];
                Vector2 uv = uvs[index];
                GL.TexCoord2(uv.x, uv.y);
                if (settings != null)
                {
                    var setting = settings.Value[index];
                    GL.MultiTexCoord2(2, setting.x, setting.y);
                }
                if (colors != null)
                {
                    GL.Color(colors.Value[index]);
                }
                GL.Vertex3((vertex.x * pixelsToUnits + pivot.x) / spriteWidth, (vertex.y * pixelsToUnits + pivot.y) / spriteHeight, 0);
            }
            GL.End();
            GL.PopMatrix();

            mat.SetTexture("_MainTex", null);
        }

        private static Material s_VectorMat = null;
        private static Material s_GradientMat = null;

        /// <summary>Renders a vector sprite to Texture2D.</summary>
        /// <param name="sprite">The sprite to render</param>
        /// <param name="width">The desired width of the resulting texture</param>
        /// <param name="height">The desired height of the resulting texture</param>
        /// <param name="antiAliasing">The number of samples per pixel for anti-aliasing</param>
        /// <returns>A Texture2D object containing the rendered vector sprite</returns>
        public static Texture2D RenderSpriteToTexture2D(Sprite sprite, int width, int height, int antiAliasing = 1)
        {
            Material mat = null;
            if (sprite.texture != null)
            {
                if (s_GradientMat == null)
                {
                    string gradientMatPath = "Packages/com.unity.vectorgraphics/Runtime/Materials/Unlit_VectorGradient.mat";
                    s_GradientMat = AssetDatabase.LoadMainAssetAtPath(gradientMatPath) as Material;
                }
                mat = s_GradientMat;
            }
            else
            {
                if (s_VectorMat == null)
                {
                    string vectorMatPath = "Packages/com.unity.vectorgraphics/Runtime/Materials/Unlit_Vector.mat";
                    s_VectorMat = AssetDatabase.LoadMainAssetAtPath(vectorMatPath) as Material;
                }
                mat = s_VectorMat;
            }

            var tex = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
            tex.antiAliasing = antiAliasing;

            var oldActive = RenderTexture.active;
            RenderTexture.active = tex;

            RenderSprite(sprite, mat);

            Texture2D copy = new Texture2D(width, height, TextureFormat.RGBA32, false);
            copy.hideFlags = HideFlags.HideAndDontSave;
            copy.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            copy.Apply();

            RenderTexture.active = oldActive;
            tex.Release();

            return copy;
        }

        private static void FlipShapeIfNecessary(ShapeRange range, IList<Vector2> vertices, IList<UInt16> indices)
        {
            // For each range, find the first valid triangle and check its winding order. If that triangle needs flipping, then flip the whole range.
            bool shouldFlip = false;
            for (int i = range.start; i <= range.end; i += 3)
            {
                var v0 = (Vector3)vertices[indices[i]];
                var v1 = (Vector3)vertices[indices[i + 1]];
                var v2 = (Vector3)vertices[indices[i + 2]];
                var s = (v1 - v0).normalized;
                var t = (v2 - v0).normalized;
                if (s == Vector3.zero || t == Vector3.zero || Mathf.Approximately(Vector3.Dot(s, t), 0.0f))
                    continue;
                var n = Vector3.Cross(s, t);
                if (Mathf.Approximately(n.magnitude, 0.0f))
                    continue;
                shouldFlip = n.z > 0.0f;
                break;
            }
            if (shouldFlip)
            {
                for (int i = range.start; i <= range.end; i += 3)
                {
                    var tmp = indices[i + 1];
                    indices[i + 1] = indices[i + 2];
                    indices[i + 2] = tmp;
                }
            }
        }

        private static Vector2 GetPivot(Alignment alignment, Vector2 customPivot, Rect bbox)
        {
            switch (alignment)
            {
                case Alignment.Center: return new Vector2(0.5f, 0.5f);
                case Alignment.TopLeft: return new Vector2(0.0f, 1.0f);
                case Alignment.TopCenter: return new Vector2(0.5f, 1.0f);
                case Alignment.TopRight: return new Vector2(1.0f, 1.0f);
                case Alignment.LeftCenter: return new Vector2(0.0f, 0.5f);
                case Alignment.RightCenter: return new Vector2(1.0f, 0.5f);
                case Alignment.BottomLeft: return new Vector2(0.0f, 0.0f);
                case Alignment.BottomCenter: return new Vector2(0.5f, 0.0f);
                case Alignment.BottomRight: return new Vector2(1.0f, 0.0f);
                case Alignment.SVGOrigin: return -bbox.position / bbox.size;
                case Alignment.Custom: return customPivot;
            }
            return Vector2.zero;
        }
    }
}
