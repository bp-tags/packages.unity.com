using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Unity.Collections;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.U2D;
using UnityEngine.Experimental.U2D;

namespace Unity.VectorGraphics.Editor
{
    [Serializable]
    internal class SVGSpriteData
    {
        public float TessellationDetail = 0.0f;
        public SpriteRect SpriteRect = new SpriteRect();
        public List<Vector2> PhysicsOutlineVectors = new List<Vector2>();
        public List<int> PhysicsOutlineLengths = new List<int>();
        public List<SpriteBone> Bones = new List<SpriteBone>();
        public Vertex2DMetaData[] SkinVertices = new Vertex2DMetaData[] { };
        public int[] SkinIndices = new int[] { };
        public Vector2Int[] SkinEdges = new Vector2Int[] { };

        public void Load(SerializedObject so)
        {
            var importer = so.targetObject as SVGImporter;
            var sprite = SVGImporter.GetImportedSprite(importer.assetPath);
            if (sprite == null)
                return;

            SpriteRect.name = sprite.name;

            int targetWidth;
            int targetHeight;
            importer.TextureSizeForSpriteEditor(out targetWidth, out targetHeight);
            SpriteRect.rect = new Rect(0, 0, targetWidth, targetHeight);
            var textureSize = new Vector2(targetWidth, targetHeight);

            var baseSP = so.FindProperty("m_SpriteData");
            var spriteRectSP = baseSP.FindPropertyRelative("SpriteRect");
            SpriteRect.border = Vector4.zero;
            SpriteRect.pivot = sprite.pivot / textureSize;
            var guidSP = spriteRectSP.FindPropertyRelative("m_SpriteID");
            SpriteRect.spriteID = new GUID(guidSP.stringValue);
        }
    }

    internal class SVGDataProviderBase
    {
        private SVGImporter m_Importer;

        public SVGDataProviderBase(SVGImporter importer)
        {
            m_Importer = importer;
        }

        public SVGSpriteData GetSVGSpriteData()
        {
            return m_Importer.GetSVGSpriteData();
        }

        public SVGImporter GetImporter()
        {
            return m_Importer;
        }

        public Sprite GetImportedSprite()
        {
            return SVGImporter.GetImportedSprite(GetImporter().assetPath);
        }

        public Vector2 GetTextureSize()
        {
            int targetWidth;
            int targetHeight;
            GetImporter().TextureSizeForSpriteEditor(out targetWidth, out targetHeight);
            return new Vector2(targetWidth, targetHeight);
        }
    }

    internal class SVGTextureDataProvider : SVGDataProviderBase, ITextureDataProvider
    {
        private float m_TextureScale = 1.0f;

        public SVGTextureDataProvider(SVGImporter importer) : base(importer)
        { }

        private Texture2D m_Texture;
        public Texture2D texture
        {
            get
            {
                if (m_Texture == null)
                {
                    var sprite = GetImportedSprite();
                    var size = ((Vector2)sprite.bounds.size) * sprite.pixelsPerUnit;

                    const float kMinTextureSize = 1024.0f;
                    if (size.x < kMinTextureSize && size.y < kMinTextureSize)
                    {
                        var maxSize = Math.Max(size.x, size.y);
                        m_TextureScale = kMinTextureSize / maxSize;
                    }

                    m_Texture = VectorUtils.RenderSpriteToTexture2D(sprite, (int)(size.x * m_TextureScale), (int)(size.y * m_TextureScale), SVGImporter.GetSVGSpriteMaterial(sprite), 4);
                }
                return m_Texture;
            }
        }

        public void GetTextureActualWidthAndHeight(out int width, out int height)
        {
            GetImporter().TextureSizeForSpriteEditor(out width, out height);
        }

        public Texture2D GetReadableTexture2D()
        {
            return texture;
        }
    }

    internal class SVGPhysicsOutlineDataProvider : SVGDataProviderBase, ISpritePhysicsOutlineDataProvider
    {
        public SVGPhysicsOutlineDataProvider(SVGImporter importer) : base(importer)
        { }

        List<Vector2[]> ISpritePhysicsOutlineDataProvider.GetOutlines(GUID guid)
        {
            return DecodeOutlines(GetSVGSpriteData().PhysicsOutlineVectors, GetSVGSpriteData().PhysicsOutlineLengths);
        }

        void ISpritePhysicsOutlineDataProvider.SetOutlines(GUID guid, List<Vector2[]> data)
        {
            EncodeOutlines(data, ref GetSVGSpriteData().PhysicsOutlineVectors, ref GetSVGSpriteData().PhysicsOutlineLengths);
        }

        float ISpritePhysicsOutlineDataProvider.GetTessellationDetail(GUID guid)
        {
            return GetSVGSpriteData().TessellationDetail;
        }

        void ISpritePhysicsOutlineDataProvider.SetTessellationDetail(GUID guid, float value)
        {
            GetSVGSpriteData().TessellationDetail = value;
        }

        internal static List<Vector2[]> DecodeOutlines(List<Vector2> vectors, List<int> lengths)
        {
            int i = 0;
            var o = new List<Vector2[]>(lengths.Count);
            foreach (int length in lengths)
            {
                var a = new Vector2[length];
                for (int j = 0; j < length; ++j)
                    a[j] = vectors[i + j];
                o.Add(a);
                i += length;
            }
            return o;
        }

        internal static void EncodeOutlines(List<Vector2[]> outlines, ref List<Vector2> vectors, ref List<int> lengths)
        {
            vectors.Clear();
            lengths.Clear();

            foreach (var a in outlines)
            {
                lengths.Add(a.Length);
                foreach (var v in a)
                    vectors.Add(v);
            }
        }
    }

    internal class SVGBoneDataProvider : SVGDataProviderBase, ISpriteBoneDataProvider
    {
        public SVGBoneDataProvider(SVGImporter importer) : base(importer)
        { }

        public List<SpriteBone> GetBones(GUID guid)
        {
            return GetSVGSpriteData().Bones;
        }

        public void SetBones(GUID guid, List<SpriteBone> bones)
        {
            GetSVGSpriteData().Bones = bones;
        }
    }

    internal class SVGMeshDataProvider : SVGDataProviderBase, ISpriteMeshDataProvider
    {
        private struct Triangle
        {
            public int A;
            public int B;
            public int C;
        }

        public SVGMeshDataProvider(SVGImporter importer) : base(importer)
        {
            var data = GetSVGSpriteData();
            if (data.SkinVertices.Length == 0)
            {
                BuildEmptySkinnedMesh(out data.SkinVertices, out data.SkinIndices, out data.SkinEdges);
            }
            else
            {
                UpdateWeightsIfNeeded();
            }
        }

        public Vertex2DMetaData[] GetVertices(GUID guid)
        {
            return GetSVGSpriteData().SkinVertices;
        }

        public void SetVertices(GUID guid, Vertex2DMetaData[] vertices)
        {
            GetSVGSpriteData().SkinVertices = vertices;
        }

        public int[] GetIndices(GUID guid)
        {
            return GetSVGSpriteData().SkinIndices;
        }

        public void SetIndices(GUID guid, int[] indices)
        {
            GetSVGSpriteData().SkinIndices = indices;
        }

        public Vector2Int[] GetEdges(GUID guid)
        {
            return GetSVGSpriteData().SkinEdges;
        }

        public void SetEdges(GUID guid, Vector2Int[] edges)
        {
            GetSVGSpriteData().SkinEdges = edges;
        }

        private Vector2 MapToTextureSpace(Sprite sprite, SpriteRect spriteRect, Vector2 v)
        {
            v *= sprite.pixelsPerUnit;
            v += Vector2.Scale(spriteRect.rect.size, spriteRect.pivot);
            return v;
        }

        private void UpdateWeightsIfNeeded()
        {
            var data = GetSVGSpriteData();
            var sprite = GetImportedSprite();
            if (data.SkinVertices.Length != sprite.vertices.Length)
            {
                // Vertices changed, transfer the bone weights
                var spriteRect = ((ISpriteEditorDataProvider)GetImporter()).GetSpriteRects()[0];
                var spriteVertices = sprite.vertices;

                var newVertices = new Vertex2DMetaData[spriteVertices.Length];
                for (int i = 0; i < spriteVertices.Length; ++i)
                    newVertices[i].position = MapToTextureSpace(sprite, spriteRect, spriteVertices[i]);
                
                var weightTransfer = new SVGBoneWeightTransfer(this);
                weightTransfer.TransferBoneWeights(data.SkinVertices, data.SkinIndices, newVertices);

                data.SkinVertices = newVertices;
                data.SkinIndices = sprite.triangles.Select(i => (int)i).ToArray();

                DetectEdges(sprite, out data.SkinEdges);
            }
        }

        private void BuildEmptySkinnedMesh(out Vertex2DMetaData[] vertices, out int[] indices, out Vector2Int[] edges)
        {
            var sprite = GetImportedSprite();
            var spriteRect = ((ISpriteEditorDataProvider)GetImporter()).GetSpriteRects()[0];
            var verts = new Vertex2DMetaData[sprite.vertices.Length];
            for (int i = 0; i < sprite.vertices.Length; ++i)
            {
                var p = sprite.vertices[i];
                verts[i] = new Vertex2DMetaData() { position = MapToTextureSpace(sprite, spriteRect, p) };
            }

            vertices = verts;
            indices = sprite.triangles.Select(x => (int)x).ToArray();

            DetectEdges(sprite, out edges);
        }

        private void DetectEdges(Sprite sprite, out Vector2Int[] edges)
        {
            // Build triangle list for each edge
            var edgeTrianglesMap = new Dictionary<Vector2Int, HashSet<Triangle>>();
            var triangles = sprite.triangles;
            for (int t = 0; t < triangles.Length; t += 3)
            {
                var a = triangles[t];
                var b = triangles[t + 1];
                var c = triangles[t + 2];
                var triangle = new Triangle() { A = a, B = b, C = c };

                var edge0 = new Vector2Int(Math.Min(a, b), Math.Max(a, b));
                var edge1 = new Vector2Int(Math.Min(b, c), Math.Max(b, c));
                var edge2 = new Vector2Int(Math.Min(c, a), Math.Max(c, a));

                if (!edgeTrianglesMap.ContainsKey(edge0))
                    edgeTrianglesMap.Add(edge0, new HashSet<Triangle>());
                if (!edgeTrianglesMap.ContainsKey(edge1))
                    edgeTrianglesMap.Add(edge1, new HashSet<Triangle>());
                if (!edgeTrianglesMap.ContainsKey(edge2))
                    edgeTrianglesMap.Add(edge2, new HashSet<Triangle>());

                edgeTrianglesMap[edge0].Add(triangle);
                edgeTrianglesMap[edge1].Add(triangle);
                edgeTrianglesMap[edge2].Add(triangle);
            }

            // Select edges that are associated with a single triangle (i.e., they are not shared)
            edges = edgeTrianglesMap.Where(x => x.Value.Count == 1).Select(x => x.Key).ToArray();
        }
    }
}