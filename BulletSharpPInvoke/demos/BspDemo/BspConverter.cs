using BulletSharp;
using BulletSharp.Math;

namespace BspDemo
{
    public abstract class BspConverter
    {
        public void ConvertBsp(BspLoader bspLoader, float scaling)
        {
            Vector3 playerStart = GetPlayerPosition(bspLoader);
            playerStart.Z += 20.0f; //start a bit higher
            playerStart *= scaling;

            foreach (BspLeaf leaf in bspLoader.Leaves)
            {
                bool isValidBrush = false;

                for (int b = 0; b < leaf.NumLeafBrushes; b++)
                {
                    int brushID = bspLoader.LeafBrushes[leaf.FirstLeafBrush + b];
                    BspBrush brush = bspLoader.Brushes[brushID];

                    if (brush.ShaderNum == -1) continue;

                    ContentFlags flags = bspLoader.IsVbsp
                        ? (ContentFlags)brush.ShaderNum
                        : bspLoader.Shaders[brush.ShaderNum].ContentFlags;

                    if ((flags & ContentFlags.Solid) == 0) continue;

                    var planeEquations = new AlignedVector3Array();
                    brush.ShaderNum = -1;

                    for (int p = 0; p < brush.NumSides; p++)
                    {
                        int sideid = brush.FirstSide + p;

                        BspBrushSide brushside = bspLoader.BrushSides[sideid];
                        int planeId = brushside.PlaneNum;
                        BspPlane plane = bspLoader.Planes[planeId];
                        Vector4 planeEq = new Vector4(plane.Normal, scaling * -plane.Distance);
                        planeEquations.Add(planeEq);
                        isValidBrush = true;
                    }
                    if (isValidBrush)
                    {
                        var vertices = new AlignedVector3Array();
                        GeometryUtil.GetVerticesFromPlaneEquations(planeEquations, vertices);

                        const bool isEntity = false;
                        Vector3 entityTarget = Vector3.Zero;
                        AddConvexVerticesCollider(vertices, isEntity, entityTarget);
                        vertices.Dispose();
                    }

                    planeEquations.Dispose();
                }
            }
            /*
            foreach (BspEntity entity in bspLoader.Entities)
            {
                if (entity.ClassName == "trigger_push")
                {
                }
            }
            */
        }

        private Vector3 GetPlayerPosition(BspLoader bspLoader)
        {
            BspEntity player;
            if (bspLoader.Entities.TryGetValue("info_player_start", out player))
            {
                return player.Origin;
            }
            else if (bspLoader.Entities.TryGetValue("info_player_deathmatch", out player))
            {
                return player.Origin;
            }
            return new Vector3(0, 0, 100);
        }

        public abstract void AddConvexVerticesCollider(AlignedVector3Array vertices, bool isEntity, Vector3 entityTargetLocation);
    }
}
