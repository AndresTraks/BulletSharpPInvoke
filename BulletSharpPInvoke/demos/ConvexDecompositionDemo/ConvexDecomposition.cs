using BulletSharp;
using BulletSharp.Math;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ConvexDecompositionDemo
{
    class ConvexDecomposition
    {
        StreamWriter _output;
        CultureInfo floatFormat = new CultureInfo("en-US");
        int baseIndex = 0;

        public List<ConvexHullShape> convexShapes = new List<ConvexHullShape>();
        public List<Vector3> convexCentroids = new List<Vector3>();

        public Vector3 LocalScaling { get; set; } = new Vector3(1, 1, 1);

        public ConvexDecomposition(StreamWriter output)
        {
            _output = output;
        }

        public void Result(Vector3[] hullVertices, int[] hullIndices)
        {
            OutputResult(hullVertices, hullIndices);

            // Calculate centroid, to shift vertices around center of mass
            Vector3 centroid = CalculateCentroid(hullVertices);
            convexCentroids.Add(centroid);

            List<Vector3> outVertices = hullVertices.Select(v => v * LocalScaling - centroid).ToList();

            // This is a tools issue:
            // due to collision margin, convex objects overlap, compensate for it here.
#if false
            outVertices = ShrinkObjectInwards(hullVertices);
#endif

            var convexShape = new ConvexHullShape(outVertices);
            convexShape.Margin = 0.01f;
            convexShapes.Add(convexShape);
        }

        private void OutputResult(Vector3[] hullVertices, int[] hullIndices)
        {
            if (_output == null)
                return;

            _output.WriteLine("## Hull Piece {0} with {1} vertices and {2} triangles.", convexShapes.Count, hullVertices.Length, hullIndices.Length / 3);

            _output.WriteLine("usemtl Material{0}", baseIndex);
            _output.WriteLine("o Object{0}", baseIndex);

            foreach (Vector3 p in hullVertices)
            {
                _output.WriteLine(string.Format(floatFormat, "v {0:F9} {1:F9} {2:F9}", p.X, p.Y, p.Z));
            }

            for (int i = 0; i < hullIndices.Length; i += 3)
            {
                int index0 = baseIndex + hullIndices[i];
                int index1 = baseIndex + hullIndices[i + 1];
                int index2 = baseIndex + hullIndices[i + 2];

                _output.WriteLine("f {0} {1} {2}", index0 + 1, index1 + 1, index2 + 1);
            }
            baseIndex += hullVertices.Length;
        }

        private Vector3 CalculateCentroid(ICollection<Vector3> vertices)
        {
            Vector3 centroid = Vector3.Zero;
            foreach (Vector3 v in vertices)
            {
                centroid += v;
            }
            return (centroid * LocalScaling) / vertices.Count;
        }

        private List<Vector3> ShrinkObjectInwards(ICollection<Vector3> vertices)
        {
            const float collisionMargin = 0.01f;

            List<Vector4> planeEquations = GeometryUtil.GetPlaneEquationsFromVertices(vertices);
            List<Vector4> shiftedPlaneEquations =
                planeEquations.Select(p => new Vector4(p.X, p.Y, p.Z, p.W + collisionMargin)).ToList();
            return GeometryUtil.GetVerticesFromPlaneEquations(shiftedPlaneEquations);
        }
    }
}
