using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Unity.VectorGraphics
{
    public partial class VectorUtils
    {
        internal static void TessellateRectangle(Rectangle rect, List<Geometry> geoms, TessellationOptions tessellationOptions)
        {
            var width = rect.Size.x;
            var height = rect.Size.y;

            if (width <= VectorUtils.Epsilon || height <= VectorUtils.Epsilon)
                return;

            if (IsSimpleRectangle(rect))
            {
                // Fast path, square corners, no patterns
                TessellateRectangleSquareCorners(rect, geoms);
            }
            else
            {
                TessellateRectangleRoundedCorners(rect, geoms, tessellationOptions);
            }
        }

        internal static Vector2[] TraceRectangle(Rectangle rect, Stroke stroke, TessellationOptions tessellationOptions)
        {
            var width = rect.Size.x;
            var height = rect.Size.y;

            if (width <= VectorUtils.Epsilon || height <= VectorUtils.Epsilon)
                return null;

            if (IsSimpleRectangle(rect))
            {
                // Fast path, square corners, no patterns
                var r = new Rect(rect.Position, rect.Size);
                return new Vector2[] {
                    new Vector2(r.xMin, r.yMin),
                    new Vector2(r.xMax, r.yMin),
                    new Vector2(r.xMax, r.yMax),
                    new Vector2(r.xMin, r.yMax)
                };
            }
            else
            {
                var contour = BuildRectangleContour(rect);
                return VectorUtils.TraceShape(contour, stroke, tessellationOptions);    
            }
        }

        private static void TessellateRectangleSquareCorners(Rectangle rect, List<Geometry> geoms)
        {
            var x = rect.Position.x;
            var y = rect.Position.y;
            var width = rect.Size.x;
            var height = rect.Size.y;

            // Don't generate any geometry for pattern fills since these are generated from another SceneNode
            if (rect.Fill != null && !(rect.Fill is PatternFill))
            {
                Vector2[] vertices;
                UInt16[] indices;
                VectorUtils.TessellateRect(new Rect(x, y, width, height), out vertices, out indices);

                var solidFill = rect.Fill as SolidFill;
                var color = solidFill != null ? solidFill.Color : Color.white;
                color.a *= rect.Fill.Opacity;

                geoms.Add(new Geometry() { Vertices = vertices, Indices = indices, Color = color, Fill = rect.Fill, FillTransform = rect.FillTransform });
            }

            if (rect.PathProps.Stroke != null)
            {
                Vector2[] vertices;
                UInt16[] indices;
                VectorUtils.TessellateRectBorder(new Rect(x, y, width, height), rect.PathProps.Stroke.HalfThickness, out vertices, out indices);

                geoms.Add(new Geometry() { Vertices = vertices, Indices = indices, Color = rect.PathProps.Stroke.Color });
            }
        }

        private static void TessellateRectangleRoundedCorners(Rectangle rect, List<Geometry> geoms, TessellationOptions tessellationOptions)
        {
            var contour = BuildRectangleContour(rect);
            var shape = new Shape() {
                Contours = new BezierContour[] { contour },
                PathProps = rect.PathProps,
                Fill = rect.Fill,
                FillTransform = rect.FillTransform
            };
            VectorUtils.TessellateShape(shape, geoms, tessellationOptions);
        }

        /// <summary>Builds a BezierContour from a Rectangle.</summary>
        /// <param name="rect">The rectangle to build the contour from</param>
        /// <returns>A BezierContour that follows the rectangle contour</returns>
        public static BezierContour BuildRectangleContour(Rectangle rect)
        {
            var width = rect.Size.x;
            var height = rect.Size.y;

            var halfSize = new Vector2(width / 2.0f, height / 2.0f);
            var radiusTL = Vector2.Max(Vector2.Min(rect.RadiusTL, halfSize), Vector2.zero);
            var radiusTR = Vector2.Max(Vector2.Min(rect.RadiusTR, halfSize), Vector2.zero);
            var radiusBR = Vector2.Max(Vector2.Min(rect.RadiusBR, halfSize), Vector2.zero);
            var radiusBL = Vector2.Max(Vector2.Min(rect.RadiusBL, halfSize), Vector2.zero);

            var leftSegmentSize = height - (radiusBL.y + radiusTL.y);
            var topSegmentSize = width - (radiusTL.x + radiusTR.x);
            var rightSegmentSize = height - (radiusBR.y + radiusTR.y);
            var bottomSegmentSize = width - (radiusBL.x + radiusBR.x);

            var segments = new List<BezierPathSegment>(8);
            BezierPathSegment seg;

            if (leftSegmentSize > VectorUtils.Epsilon)
            {
                seg = MakePathLine(new Vector2(0.0f, radiusTL.y + leftSegmentSize), new Vector2(0.0f, radiusTL.y))[0];
                segments.Add(seg);
            }

            if (radiusTL.magnitude > VectorUtils.Epsilon)
            {
                var circleArc = VectorUtils.MakeArc(Vector2.zero, -Mathf.PI, Mathf.PI / 2.0f, 1.0f);
                circleArc = VectorUtils.TransformBezierPath(circleArc, radiusTL, 0.0f, radiusTL);
                segments.Add(circleArc[0]);
            }

            if (topSegmentSize > VectorUtils.Epsilon)
            {
                seg = MakePathLine(new Vector2(radiusTL.x, 0.0f), new Vector2(radiusTL.x + topSegmentSize, 0.0f))[0];
                segments.Add(seg);
            }

            if (radiusTR.magnitude > VectorUtils.Epsilon)
            {
                var topRight = new Vector2(width - radiusTR.x, radiusTR.y);
                var circleArc = VectorUtils.MakeArc(Vector2.zero, -Mathf.PI / 2.0f, Mathf.PI / 2.0f, 1.0f);
                circleArc = VectorUtils.TransformBezierPath(circleArc, topRight, 0.0f, radiusTR);
                segments.Add(circleArc[0]);
            }

            if (rightSegmentSize > VectorUtils.Epsilon)
            {
                seg = MakePathLine(new Vector2(width, radiusTR.y), new Vector2(width, radiusTR.y + rightSegmentSize))[0];
                segments.Add(seg);
            }

            if (radiusBR.magnitude > VectorUtils.Epsilon)
            {
                var bottomRight = new Vector2(width - radiusBR.x, height - radiusBR.y);
                var circleArc = VectorUtils.MakeArc(Vector2.zero, 0.0f, Mathf.PI / 2.0f, 1.0f);
                circleArc = VectorUtils.TransformBezierPath(circleArc, bottomRight, 0.0f, radiusBR);
                segments.Add(circleArc[0]);
            }

            if (bottomSegmentSize > VectorUtils.Epsilon)
            {
                seg = MakePathLine(new Vector2(width - radiusBR.x, height), new Vector2(width - (radiusBR.x + bottomSegmentSize), height))[0];
                segments.Add(seg);
            }

            if (radiusBL.magnitude > VectorUtils.Epsilon)
            {
                var bottomLeft = new Vector2(radiusBL.x, height - radiusBL.y);
                var circleArc = VectorUtils.MakeArc(Vector2.zero, Mathf.PI / 2.0f, Mathf.PI / 2.0f, 1.0f);
                circleArc = VectorUtils.TransformBezierPath(circleArc, bottomLeft, 0.0f, radiusBL);
                segments.Add(circleArc[0]);

                var end = new Vector2(0.0f, height - radiusBL.y);
                segments.Add(new BezierPathSegment() { P0 = end });
            }

            // Offset segments to position
            for (int i = 0; i < segments.Count; ++i)
            {
                var s = segments[i];
                s.P0 += rect.Position;
                s.P1 += rect.Position;
                s.P2 += rect.Position;
                segments[i] = s;
            }

            return new BezierContour() { Segments = segments.ToArray(), Closed = true };
        }

        private static bool IsSimpleRectangle(Rectangle rect)
        {
            if (rect.RadiusTL.magnitude > VectorUtils.Epsilon ||
                rect.RadiusTR.magnitude > VectorUtils.Epsilon ||
                rect.RadiusBL.magnitude > VectorUtils.Epsilon ||
                rect.RadiusBR.magnitude > VectorUtils.Epsilon)
            {
                // Not simple if any corner is rounded
                return false;
            }

            if (rect.PathProps.Stroke != null && rect.PathProps.Stroke.Pattern != null && rect.PathProps.Stroke.Pattern.Length > 0)
            {
                // Not simple if there's a pattern
                return false;
            }

            if (rect.PathProps.Corners != PathCorner.Tipped)
            {
                // Not simple if corners aren't tipped
                return false;
            }

            // We can ignore the pathProps head/tail properties, they should have
            // no impact on the closed rectangle outline
            return true;
        }
    }
}
