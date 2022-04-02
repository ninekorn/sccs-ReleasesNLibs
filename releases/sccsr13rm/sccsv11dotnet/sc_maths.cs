using System;
using System.Collections.Generic;
using System.Text;

using SharpDX;

namespace _sc_core_systems
{
    public static class sc_maths
    {

        //MODIFIED 2D TO 3D VERSION OF SEBASTIEN LAGUE WITH SOME MODS SIMPLY FOR VISUALLY BEING ABLE TO MODIFY TO ELLIPSOID AND OTHER GEOMETRY FORMS - it kinda works but ive got a hard time getting a perfect sphere. im not a mathematician
        //and i am a lazy programmer.
        public static float sc_check_distance_node_3d_geometry(Vector3 nodeA, Vector3 nodeB, float minx, float miny, float minz, float maxx, float maxy, float maxz) // i was thinking about using the index instead and then was like well i need the distance man.
        {
            //var pointFrontX = (1 * Math.cos(radToDeg * Math.PI / 180));
            //var pointFrontY = (1 * Math.sin(radToDeg * Math.PI / 180));

            //SEBASTIEN LAGUE 2D BLUEPRINT FOR NODE DIAGONAL OR NOT DISTANCE.
            /*var dstX = Math.Abs((nodeA.X) - (nodeB.X));
            var dstZ = Math.Abs((nodeA.Y) - (nodeB.Y));

            if (dstX > dstZ)
            {
                return 14 * dstZ + 10 * (dstX - dstZ);
            }
            return 14 * dstX + 10 * (dstZ - dstX);*/

            var dstX = Math.Abs((nodeA.X) - (nodeB.X));
            var dstY = Math.Abs((nodeA.Y) - (nodeB.Y));
            var dstZ = Math.Abs((nodeA.Z) - (nodeB.Z));

            float dstX_vs_dstZ = 0;
            float dstX_vs_dstY = 0;
            float dstY_vs_dstZ = 0;

            if (dstX > dstZ)
            {
                dstX_vs_dstZ = maxx * dstZ + minx * (dstX - dstZ);
            }
            else
            {
                dstX_vs_dstZ = maxx * dstX + minx * (dstZ - dstX);
            }

            if (dstX > dstY)
            {
                dstX_vs_dstY = maxy * dstY + miny * (dstX - dstY);
            }
            else
            {
                dstX_vs_dstY = maxy * dstX + miny * (dstY - dstX);
            }

            if (dstY > dstZ)
            {
                dstY_vs_dstZ = maxz * dstZ + minz * (dstY - dstZ);
            }
            else
            {
                dstY_vs_dstZ = maxz * dstY + minz * (dstZ - dstY);
            }

            return dstX_vs_dstY + dstX_vs_dstZ + dstY_vs_dstZ;
        }














        public static float sc_sebastian_lague_check_distance_node_3d_ellipsoid_not_really_ellipsoid(Vector3 nodeA, Vector3 nodeB)
       {
           //SEBASTIEN LAGUE 2D BLUEPRINT FOR NODE DIAGONAL OR NOT DISTANCE.
           /*var dstX = Math.Abs((nodeA.X) - (nodeB.X));
           var dstZ = Math.Abs((nodeA.Y) - (nodeB.Y));

           if (dstX > dstZ)
           {
               return 14 * dstZ + 10 * (dstX - dstZ);
           }
           return 14 * dstX + 10 * (dstZ - dstX);*/


           var dstX = Math.Abs((nodeA.X) - (nodeB.X));
            var dstY = Math.Abs((nodeA.Y) - (nodeB.Y));
            var dstZ = Math.Abs((nodeA.Z) - (nodeB.Z));

            float dstX_vs_dstZ = 0;
            float dstX_vs_dstY = 0;

            if (dstX > dstZ)
            {
                dstX_vs_dstZ = 14 * dstZ + 10 * (dstX - dstZ);
            }
            else
            {
                dstX_vs_dstZ = 14 * dstX + 10 * (dstZ - dstX);
            }

            if (dstX > dstY)
            {
                dstX_vs_dstY = 14 * dstY + 10 * (dstX - dstY);
            }
            else
            {
                dstX_vs_dstY = 14 * dstX + 10 * (dstY - dstX);
            }

            /*if (dstX_vs_dstY > dstX_vs_dstZ)
            {
                return dstX_vs_dstY;
            }
            else
            {
                return dstX_vs_dstZ;
            }*/

            return dstX_vs_dstY + dstX_vs_dstZ;
        }




        /*
        public float sc_check_distance_sebastian_lague_node_3d()
        {
            if (dstX > dstZ)
            {
                if (dstX > dstY)
                {
                    return 14 * dstY + 14 * dstZ + 10 * (dstX - dstZ) + 10 * (dstX - dstY);
                }
                else
                {
                    return 14 * dstX + 14 * dstZ + 10 * (dstX - dstZ) + 10 * (dstY - dstX);
                }
            }

            //calculating x
            if (dstX > dstY && dstX > dstZ)
            {
                var part_00 = 14 * dstY + 10 * (dstX - dstY);
                var part_01 = 14 * dstZ + 10 * (dstX - dstZ);
                return part_00 + part_01;
            }
            else if (dstX > dstY && dstX < dstZ)
            {
                var part_00 = 14 * dstY + 10 * (dstX - dstY);
                var part_01 = 14 * dstX + 10 * (dstZ - dstX);
                return part_00 + part_01;
            }
            else if (dstX < dstY && dstX < dstZ)
            {
                var part_00 = 14 * dstX + 10 * (dstY - dstX);
                var part_01 = 14 * dstX + 10 * (dstZ - dstX);
                return part_00 + part_01;
            }
            else if (dstX < dstY && dstX > dstZ)
            {
                var part_00 = 14 * dstX + 10 * (dstY - dstX);
                var part_01 = 14 * dstZ + 10 * (dstX - dstZ);
                return part_00 + part_01;
            }
            //calculating y
            else if (dstY > dstX && dstY > dstZ)
            {
                var part_00 = 14 * dstX + 10 * (dstY - dstX);
                var part_01 = 14 * dstZ + 10 * (dstY - dstZ);
                return part_00 + part_01;
            }
            else if (dstY > dstX && dstY < dstZ)
            {
                var part_00 = 14 * dstX + 10 * (dstY - dstX);
                var part_01 = 14 * dstY + 10 * (dstZ - dstY);
                return part_00 + part_01;
            }
            else if (dstY < dstX && dstY < dstZ)
            {
                var part_00 = 14 * dstY + 10 * (dstX - dstY);
                var part_01 = 14 * dstY + 10 * (dstZ - dstY);
                return part_00 + part_01;
            }
            else if (dstY < dstX && dstY > dstZ)
            {
                var part_00 = 14 * dstY + 10 * (dstX - dstY);
                var part_01 = 14 * dstZ + 10 * (dstY - dstZ);
                return part_00 + part_01;
            }

            //calculating z
            else if (dstZ > dstX && dstZ > dstY)
            {
                var part_00 = 14 * dstX + 10 * (dstZ - dstX);
                var part_01 = 14 * dstY + 10 * (dstZ - dstY);
                return part_00 + part_01;
            }
            else if (dstZ > dstX && dstZ < dstY)
            {
                var part_00 = 14 * dstX + 10 * (dstZ - dstX);
                var part_01 = 14 * dstZ + 10 * (dstY - dstZ);
                return part_00 + part_01;
            }
            else if (dstZ < dstX && dstZ < dstY)
            {
                var part_00 = 14 * dstZ + 10 * (dstX - dstZ);
                var part_01 = 14 * dstZ + 10 * (dstY - dstZ);
                return part_00 + part_01;
            }
            else if (dstZ < dstX && dstZ > dstY)
            {
                var part_00 = 14 * dstZ + 10 * (dstX - dstZ);
                var part_01 = 14 * dstY + 10 * (dstZ - dstY);
                return part_00 + part_01;
            }*/
            //calculating diagonals ? not sure that covers them all. and it doesnt work
            /*else
            {
                var part_00 = 10 * dstX; //14
                var part_01 = 10 * dstY; //14
                var part_02 = 10 * dstZ; //14
                return 10; //part_00 + part_01 + part_02
            }
        }*/













    }
}


