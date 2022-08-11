using BulletSharp;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ConvexDecompositionDemo
{
    internal sealed class ConvexDecomposition
    {
        private WavefrontWriter _wavefrontWriter;

        public ConvexDecomposition(WavefrontWriter wavefrontWriter = null)
        {
            _wavefrontWriter = wavefrontWriter;
        }

        public List<ConvexHullShape> ConvexShapes { get; } = new List<ConvexHullShape>();
        public List<Vector3> ConvexCentroids { get; } = new List<Vector3>();

        public Vector3 LocalScaling { get; set; } = new Vector3(1, 1, 1);

        public void Result(Vector3[] hullVertices, long[] hullIndices)
        {
            _wavefrontWriter.OutputObject(hullVertices, hullIndices);

            // Calculate centroid, to shift vertices around center of mass
            Vector3 centroid = CalculateCentroid(hullVertices);
            ConvexCentroids.Add(centroid);

            List<Vector3> outVertices = hullVertices.Select(v => v * LocalScaling - centroid).ToList();

            // This is a tools issue:
            // due to collision margin, convex objects overlap, compensate for it here.
#if false
            outVertices = ShrinkObjectInwards(hullVertices);
#endif

            var convexShape = new ConvexHullShape(outVertices);
            convexShape.Margin = 0.01f;
            ConvexShapes.Add(convexShape);
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
