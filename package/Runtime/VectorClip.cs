using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using LibTessDotNet;
using ClipperLib;

namespace Unity.VectorGraphics
{
    internal static class VectorClip
    {
        const int kClipperScale = 100000;

        private static Stack<List<List<IntPoint>>> m_ClipStack = new Stack<List<List<IntPoint>>>();

        internal static void ResetClip()
        {
            m_ClipStack.Clear();
        }

        internal static void PushClip(List<Vector2[]> clipper, Matrix2D transform)
        {
            var clipperPaths = new List<List<IntPoint>>(10);
            foreach (var shape in clipper)
            {
                var verts = new List<IntPoint>(shape.Length);
                foreach (var v in shape)
                {
                    var tv = transform * v;
                    verts.Add(new IntPoint(tv.x * kClipperScale, tv.y * kClipperScale));
                }
                clipperPaths.Add(verts);
            }

            m_ClipStack.Push(clipperPaths);
        }

        internal static void PopClip()
        {
            m_ClipStack.Pop();
        }

        internal static void ClipGeometry(VectorUtils.Geometry geom)
        {
            var clipper = new Clipper();
            foreach (var clipperPaths in m_ClipStack)
            {
                var vertices = new List<Vector2>(geom.vertices.Length);
                var indices = new List<UInt16>(geom.indices.Length);
                var paths = BuildTriangleClipPaths(geom);
                var result = new List<List<IntPoint>>();

                UInt16 maxIndex = 0;

                foreach (var path in paths)
                {
                    clipper.AddPaths(clipperPaths, PolyType.ptClip, true);
                    clipper.AddPath(path, PolyType.ptSubject, true);
                    clipper.Execute(ClipType.ctIntersection, result, PolyFillType.pftNonZero, PolyFillType.pftNonZero);

                    if (result.Count > 0)
                        BuildGeometryFromClipPaths(geom, result, vertices, indices, ref maxIndex);

                    clipper.Clear();
                    result.Clear();
                }

                geom.vertices = vertices.ToArray();
                geom.indices = indices.ToArray();
            }
        }

        private static List<List<IntPoint>> BuildTriangleClipPaths(VectorUtils.Geometry geom)
        {
            var paths = new List<List<IntPoint>>(geom.indices.Length/3);
            var verts = geom.vertices;
            var inds = geom.indices;
            var indexCount = geom.indices.Length;
            var matrix = geom.worldTransform;
            for (int i = 0; i < indexCount; i += 3)
            {
                var v0 = matrix * verts[inds[i]];
                var v1 = matrix * verts[inds[i+1]];
                var v2 = matrix * verts[inds[i+2]];
                var tri = new List<IntPoint>(3);
                tri.Add(new IntPoint(v0.x * kClipperScale, v0.y * kClipperScale));
                tri.Add(new IntPoint(v1.x * kClipperScale, v1.y * kClipperScale));
                tri.Add(new IntPoint(v2.x * kClipperScale, v2.y * kClipperScale));
                paths.Add(tri);
            }
            return paths;
        }

        private static void BuildGeometryFromClipPaths(VectorUtils.Geometry geom, List<List<IntPoint>> paths, List<Vector2> outVerts, List<UInt16> outInds, ref UInt16 maxIndex)
        {
            var vertices = new List<Vector2>(100);
            var indices = new List<UInt16>(vertices.Capacity*3);
            var vertexIndex = new Dictionary<IntPoint, UInt16>();

            foreach (var path in paths)
            {
                if (path.Count == 3)
                {
                    // Triangle case, no need to tessellate
                    foreach (var pt in path)
                        StoreClipVertex(vertexIndex, vertices, indices, pt, ref maxIndex);
                }
                else if (path.Count > 3)
                {
                    // Generic polygon case, we need to tessellate first
                    var tess = new Tess();
                    var contour = new ContourVertex[path.Count];
                    for (int i = 0; i < path.Count; ++i)
                        contour[i] = new ContourVertex() { Position = new Vec3() { X = path[i].X, Y = path[i].Y, Z = 0.0f }};
                    tess.AddContour(contour, ContourOrientation.Original);

                    var windingRule = WindingRule.NonZero; 
                    tess.Tessellate(windingRule, ElementType.Polygons, 3);

                    foreach (var e in tess.Elements)
                    {
                        var v = tess.Vertices[e];
                        var pt = new IntPoint(v.Position.X, v.Position.Y);
                        StoreClipVertex(vertexIndex, vertices, indices, pt, ref maxIndex);
                    }
                }                
            }

            var invMatrix = geom.worldTransform.Inverse();

            outVerts.AddRange(vertices.Select(v => invMatrix * v));
            outInds.AddRange(indices);
        }

        private static void StoreClipVertex(Dictionary<IntPoint, UInt16> vertexIndex, List<Vector2> vertices, List<UInt16> indices, IntPoint pt,  ref UInt16 index)
        {
            UInt16 storedIndex;
            if (vertexIndex.TryGetValue(pt, out storedIndex))
            {
                indices.Add(storedIndex);
            }
            else
            {
                vertices.Add(new Vector2(((float)pt.X) / kClipperScale, ((float)pt.Y) / kClipperScale));
                indices.Add(index);
                vertexIndex[pt] = index;
                ++index;
            }
        }
    }
}