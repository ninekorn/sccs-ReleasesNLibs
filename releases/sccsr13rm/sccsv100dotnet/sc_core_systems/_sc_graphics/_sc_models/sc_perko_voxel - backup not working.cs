using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

using SimplexNoise;

//from youtube Craig Perko.
using Math = System.Math;

using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace _sc_core_systems.SC_Graphics
{

    public struct sc_chunk_node
    {
        public float _distance_parent_node_to_this;
        public float _distance_to_target;
        public Vector3 _position;
    }




    //https://www.c-sharpcorner.com/article/generating-random-number-and-string-in-C-Sharp/
    public class sc_perko_voxel
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        private Bitmap bitmap;
        private System.Drawing.Color[,] colors;
        private Bitmap earthLookupBitmap;
        private System.Drawing.Color[] earthLookupTable;

        int _size_of_spike_end = 10;

        float _max_spike_length = 0.975f;
        float _min_spike_length = 0.65f;

        float _min_sphere_covid19_diameter = 0.55f;



        float _min_spike_end = 9.31415926535f; //9.31415926535f // not sure that PI number crunched in is working...
        float _max_spike_end = 10.31415926535f; //10.31415926535f
        float _diameter_spike_end = 210;





        float _spawn_rate = 0;


        public int ChunkWidth_L = 4;
        public int ChunkWidth_R = 3;

        public int ChunkHeight_L = 20;
        public int ChunkHeight_R = 19;

        public int ChunkDepth_L = 20;
        public int ChunkDepth_R = 19;

        //public int ChunkWidth = -1;
        //public int ChunkHeight = -1;
        //public int ChunkDepth = -1;

        int _max;

        float _current_visual_distance_spike_glyco_protein_covid19_min = 0;
        float _current_visual_distance_spike_glyco_protein_covid19_max = 0;
        float _current_counter_for_adding_spike_glyco_protein_covid19_00 = 0;
        float _current_counter_for_adding_spike_glyco_protein_covid19_01 = 0;
        float _current_counter_for_adding_spike_glyco_protein_covid19_02 = 0;
        float _current_counter_for_adding_spike_glyco_protein_covid19_03 = 0;
        float _current_counter_for_adding_spike_glyco_protein_covid19_04 = 0;
        float _current_counter_for_adding_spike_glyco_protein_covid19_05 = 0;
        float _current_counter_for_adding_spike_glyco_protein_covid19_06 = 0;
        float _current_counter_for_adding_spike_glyco_protein_covid19_07 = 0;



        //public byte[] map;
        private int[] map;
        private float planeSize = 1;
        private int seed = 3420;

        private int block;

        private int counterVertexTop = 0;

        private int vertzIndex = 0;
        private int trigsIndex = 0;

        private int _detailScale = 10;
        private int _heightScale = 10;

        private Vector3 forward = new Vector3(0, 0, 1);
        private Vector3 back = new Vector3(0, 0, -1);
        private Vector3 right = new Vector3(1, 0, 0);
        private Vector3 left = new Vector3(-1, 0, 0);
        private Vector3 up = new Vector3(0, 1, 0);
        private Vector3 down = new Vector3(0, -1, 0);


        private List<SC_cube.DVertex> listOfVerts = new List<SC_cube.DVertex>();
        private List<int> listOfTriangleIndices = new List<int>();

        int randX = 3420;
        int randY = 3420;
        public static int countingArrayOfChunks = 0;


        float colorX = 0.75f;
        float colorY = 0.75f;
        float colorZ = 0.75f;
        float _tinyChunkHeightScale = 200;
        Vector3 _pos;



        int _swtch_spike_00 = 0;
        int _swtch_spike_01 = 0;
        int _swtch_spike_02 = 0;
        int _swtch_spike_03 = 0;
        int _swtch_spike_04 = 0;
        int _swtch_spike_05 = 0;
        int _swtch_spike_06 = 0;
        int _swtch_spike_07 = 0;




        public virtual float CalculateNoiseValue(Vector3 pos, Vector3 offset, float scale)
        {

            float noiseX = Math.Abs((pos.X + offset.X) * scale);
            float noiseY = Math.Abs((pos.Y + offset.Y) * scale);
            float noiseZ = Math.Abs((pos.Z + offset.Z) * scale);

            return Noise.Generate(noiseX, noiseY, noiseZ);

        }

        Random rand = new Random();



        Vector3 SphericalToCartesian(float radius, float polar, float elevation) //xyz
        {
            float a = (float)(radius * Math.Cos(elevation));
            float x = (float)(a * Math.Cos(polar));
            float y = (float)(radius * Math.Sin(elevation));
            float z = (float)(a * Math.Sin(polar));

            return new Vector3(x, y, z);
        }


        //https://pastebin.com/fAFp6NnN // Also found on the unity3D forums.
        public static Vector3 _getDirection(Vector3 value, SharpDX.Quaternion rotation)
        {
            Vector3 vector;
            double num12 = rotation.X + rotation.X;
            double num2 = rotation.Y + rotation.Y;
            double num = rotation.Z + rotation.Z;
            double num11 = rotation.W * num12;
            double num10 = rotation.W * num2;
            double num9 = rotation.W * num;
            double num8 = rotation.X * num12;
            double num7 = rotation.X * num2;
            double num6 = rotation.X * num;
            double num5 = rotation.Y * num2;
            double num4 = rotation.Y * num;
            double num3 = rotation.Z * num;
            double num15 = ((value.X * ((1f - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
            double num14 = ((value.X * (num7 + num9)) + (value.Y * ((1f - num8) - num3))) + (value.Z * (num4 - num11));
            double num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((1f - num8) - num5));
            vector.X = (float)num15;
            vector.Y = (float)num14;
            vector.Z = (float)num13;
            return vector;
        }

        //http://james-ramsden.com/angle-between-two-vectors/
        double AngleBetween(Vector3 u, Vector3 v, bool returndegrees)
        {
            double toppart = 0;
            for (int d = 0; d < 3; d++) toppart += u[d] * v[d];

            double u2 = 0; //u squared
            double v2 = 0; //v squared
            for (int d = 0; d < 3; d++)
            {
                u2 += u[d] * u[d];
                v2 += v[d] * v[d];
            }

            double bottompart = 0;
            bottompart = Math.Sqrt(u2 * v2);


            double rtnval = Math.Acos(toppart / bottompart);
            if (returndegrees) rtnval *= 360.0 / (2 * Math.PI);
            return rtnval;
        }


        float randomX = 1;
        float randomY = 1;
        float randomZ = 1;
        /*private float npcCheckDistance (Vector3 nodeA, Vector3 nodeB)
        {
            var dstX = Math.Abs((nodeA.X) - (nodeB.X));
            var dstY = Math.Abs((nodeA.Y) - (nodeB.Y));
            var dstZ = Math.Abs((nodeA.Z) - (nodeB.Z));

            if (dstX > dstZ)
                return 14 * dstZ + 10 * (dstX - dstZ);
            return 14 * dstX + 10 * (dstZ - dstX);
        }*/

        Random randomer = new Random();

        float getSomeRandNumThousandDecimal(int minNum, int maxNum, float _decimal, int autonegative)
        {
            var num = Math.Floor(randomer.NextDouble() * maxNum) + minNum; // this will get a number between 1 and 999;

            if (autonegative == 1)
            {
                num *= Math.Floor(randomer.NextDouble() * 2) == 1 ? 1 : -1; // this will add minus sign in 50% of cases
            }

            //if (num == 0)
            //{
            //    return (float)getSomeRandNumThousandDecimal(maxNum, _decimal, autonegative);
            //}

            return (float)(num * _decimal);
        }



        //CURRENTLY THE MIX IS NOT HOMOGENOUS SO I CANNOT RANDOMLY CREATE SPIKES ALL AROUND AND HAVE THEM DISTANCED ENOUGH FROM EACH OTHER. I HAVE TO CREATE ONE SPIKE PER EIGTH OF THAT
        //SPHEROID CHUNK

        private void CreateSpikeGlycoProteinCOVID19(Vector3 center, int x, int y, int z) //, float min, float max //ref BlockData[,] blocks, TerrainType terrain
        {
            //MessageBox((IntPtr)0, "test", "Oculus error", 0);
            Vector3 current_target_pos = new Vector3(x, y, z);

            Vector3 _spike_direction = current_target_pos;

            float _spike_length = _spike_direction.Length();
            _spike_direction.Normalize();

            int _spike_max_length = (int)Math.Round(_spike_length - 1);// (int)Math.Round(_spike_length);// (int)Math.Round(_spike_length);// (int)(Math.Floor(rand.NextDouble() * (ChunkHeight - 1) + 0));

            Vector3? current_start_move_pos = center;

            //float xpos = x;
            //float ypos = y;
            //float zpos = z;

            float xpos = (int)Math.Round(current_target_pos.X);
            float ypos = (int)Math.Round(current_target_pos.Y);
            float zpos = (int)Math.Round(current_target_pos.Z);

            if (xpos < 0)
            {
                xpos *= -1;
                xpos = (ChunkWidth_R) + xpos;
            }

            if (ypos < 0)
            {
                ypos *= -1;
                ypos = (ChunkHeight_R) + ypos;
            }

            if (zpos < 0)
            {
                zpos *= -1;
                zpos = (ChunkDepth_R) + zpos;
            }

            var _index = (int)Math.Round(xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos));



            /*if (_index >= 0 && _index < _max)
            {
                //MessageBox((IntPtr)0, "in of array length " + _max, "Oculus error", 0);
                map[_index] = 1;
            }
            else
            {
                MessageBox((IntPtr)0, _index+ " _ " + _max, "Oculus error", 0);
            }*/




            /*for (int xx = -1; xx <= 1; xx++)
            {
                for (int yy = -1; yy <= 1; yy++)
                {
                    for (int zz = -1; zz <= 1; zz++)
                    {

                        xpos = (int)Math.Round(current_target_pos.X + xx);
                        ypos = (int)Math.Round(current_target_pos.Y + yy);
                        zpos = (int)Math.Round(current_target_pos.Z + zz);

                        if (xpos < 0)
                        {
                            xpos *= -1;
                            xpos = (ChunkWidth_R) + xpos;
                        }

                        if (ypos < 0)
                        {
                            ypos *= -1;
                            ypos = (ChunkHeight_R) + ypos;
                        }

                        if (zpos < 0)
                        {
                            zpos *= -1;
                            zpos = (ChunkDepth_R) + zpos;
                        }

                        _index = (int)Math.Round(xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos));

                        if (_index >= 0 && _index < _max)
                        {
                            map[_index] = 1;
                        }
                    }
                }
            }*/










            _index = 0;
            float sqrtX = 0;
            float sqrtY = 0;
            float sqrtZ = 0;
            float dist = 0;

            xpos = 0;
            ypos = 0;
            zpos = 0;
            Vector3 current_spike_neighboor_pos;
            int _end = 0;

            sc_chunk_node _sc_node;
            List<sc_chunk_node> _sc_node_list;
            Vector3 last_iteration_location = Vector3.Zero;


            for (float i = 0; i < _spike_max_length * 0.55f;) //_spike_max_length //(float)Math.Round(_spike_max_length * 0.75f)
            {
                if (current_start_move_pos == null)
                {
                    break;
                }
                _sc_node_list = new List<sc_chunk_node>();

                for (int xx = -1; xx <= 1; xx++)
                {
                    for (int yy = -1; yy <= 1; yy++)
                    {
                        for (int zz = -1; zz <= 1; zz++)
                        {
                            xpos = (int)Math.Round(current_start_move_pos.Value.X + xx);
                            ypos = (int)Math.Round(current_start_move_pos.Value.Y + yy);
                            zpos = (int)Math.Round(current_start_move_pos.Value.Z + zz);
                            current_spike_neighboor_pos = new Vector3(xpos, ypos, zpos);

                            if (xx == 0 && yy == 0 && zz == 0)
                            {
                                continue;
                            }

                            _sc_node = new sc_chunk_node();
                            _sc_node._position = current_spike_neighboor_pos;

                            sqrtX = ((current_target_pos.X - current_spike_neighboor_pos.X) * (current_target_pos.X - current_spike_neighboor_pos.X));
                            sqrtY = ((current_target_pos.Y - current_spike_neighboor_pos.Y) * (current_target_pos.Y - current_spike_neighboor_pos.Y));
                            sqrtZ = ((current_target_pos.Z - current_spike_neighboor_pos.Z) * (current_target_pos.Z - current_spike_neighboor_pos.Z));
                            dist = (float)Math.Sqrt(sqrtX + sqrtY + sqrtZ);
                            _sc_node._distance_to_target = dist;

                            sqrtX = ((current_start_move_pos.Value.X - current_spike_neighboor_pos.X) * (current_start_move_pos.Value.X - current_spike_neighboor_pos.X));
                            sqrtY = ((current_start_move_pos.Value.Y - current_spike_neighboor_pos.Y) * (current_start_move_pos.Value.Y - current_spike_neighboor_pos.Y));
                            sqrtZ = ((current_start_move_pos.Value.Z - current_spike_neighboor_pos.Z) * (current_start_move_pos.Value.Z - current_spike_neighboor_pos.Z));
                            dist = (float)Math.Sqrt(sqrtX + sqrtY + sqrtZ);
                            _sc_node._distance_parent_node_to_this = dist;

                            _sc_node_list.Add(_sc_node);

                            if (xpos < 0)
                            {
                                xpos *= -1;
                                xpos = (ChunkWidth_R) + xpos;
                            }

                            if (ypos < 0)
                            {
                                ypos *= -1;
                                ypos = (ChunkHeight_R) + ypos;
                            }

                            if (zpos < 0)
                            {
                                zpos *= -1;
                                zpos = (ChunkDepth_R) + zpos;
                            }

                            _index = (int)Math.Round(xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos));

                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                            }
                            /*if (current_spike_neighboor_pos.X == current_target_pos.X && current_spike_neighboor_pos.Y == current_target_pos.Y && current_spike_neighboor_pos.Z == current_target_pos.Z ||
                               current_start_move_pos.Value.X == current_target_pos.X && current_start_move_pos.Value.Y == current_target_pos.Y && current_start_move_pos.Value.Z == current_target_pos.Z)
                            {
                                //_end = 1;
                                // continue;
                                break;
                            }*/
                        }
                    }
                }

                current_start_move_pos = null;

                if (_sc_node_list.Count > 0)
                {
                    _sc_node_list.Sort((s1, s2) => s1._distance_to_target.CompareTo(s2._distance_to_target));

                    xpos = (int)Math.Round(_sc_node_list[0]._position.X);
                    ypos = (int)Math.Round(_sc_node_list[0]._position.Y);
                    zpos = (int)Math.Round(_sc_node_list[0]._position.Z);

                    current_start_move_pos = new Vector3(xpos, ypos, zpos);

                    if (xpos < 0)
                    {
                        xpos *= -1;
                        xpos = (ChunkWidth_R) + xpos;
                    }

                    if (ypos < 0)
                    {
                        ypos *= -1;
                        ypos = (ChunkHeight_R) + ypos;
                    }

                    if (zpos < 0)
                    {
                        zpos *= -1;
                        zpos = (ChunkDepth_R) + zpos;
                    }

                    //_index = (int)Math.Round(xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos));

                    //if (_index >= 0 && _index < _max)
                    //{
                    //    map[_index] = 1;
                    //}

                    for (int xx = -1; xx <= 1; xx++)
                    {
                        for (int yy = -1; yy <= 1; yy++)
                        {
                            for (int zz = -1; zz <= 1; zz++)
                            {
                                xpos = (int)Math.Round(current_start_move_pos.Value.X + xx);
                                ypos = (int)Math.Round(current_start_move_pos.Value.Y + yy);
                                zpos = (int)Math.Round(current_start_move_pos.Value.Z + zz);

                                current_spike_neighboor_pos = new Vector3(xpos, ypos, zpos);

                                if (xx == 0 && yy == 0 && zz == 0)
                                {
                                    continue;
                                }

                                if (xpos < 0)
                                {
                                    xpos *= -1;
                                    xpos = (ChunkWidth_R) + xpos;
                                }

                                if (ypos < 0)
                                {
                                    ypos *= -1;
                                    ypos = (ChunkHeight_R) + ypos;
                                }

                                if (zpos < 0)
                                {
                                    zpos *= -1;
                                    zpos = (ChunkDepth_R) + zpos;
                                }

                                _index = (int)Math.Round(xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos));

                                if (_index >= 0 && _index < _max)
                                {
                                    map[_index] = 1;
                                }

                                /*if (current_spike_neighboor_pos.X == current_target_pos.X && current_spike_neighboor_pos.Y == current_target_pos.Y && current_spike_neighboor_pos.Z == current_target_pos.Z ||
                                    current_start_move_pos.Value.X == current_target_pos.X && current_start_move_pos.Value.Y == current_target_pos.Y && current_start_move_pos.Value.Z == current_target_pos.Z)
                                {
                                    //_end = 1;
                                    // continue;
                                    break;
                                }*/
                            }
                        }
                    }

                    i += (float)Math.Ceiling(_sc_node_list[0]._distance_parent_node_to_this);
                }
                else
                {
                    i += 1;
                }

            }

            //head of spike tree mushroom looking or brocoli looking.
            for (int xx = -_size_of_spike_end; xx <= _size_of_spike_end; xx++)
            {
                for (int yy = -_size_of_spike_end; yy <= _size_of_spike_end; yy++)
                {
                    for (int zz = -_size_of_spike_end; zz <= _size_of_spike_end; zz++)
                    {
                        xpos = (int)Math.Round(current_start_move_pos.Value.X + xx);
                        ypos = (int)Math.Round(current_start_move_pos.Value.Y + yy);
                        zpos = (int)Math.Round(current_start_move_pos.Value.Z + zz);

                        //float distance = Vector3.Distance(current_start_move_pos.Value, new Vector3(xpos, ypos, zpos));
                        float distance = sc_maths.sc_check_distance_node_3d_geometry(current_start_move_pos.Value, new Vector3(xpos, ypos, zpos), _min_spike_end, _min_spike_end, _min_spike_end, _max_spike_end, _max_spike_end, _max_spike_end); //11.31415926535f

                        if (distance < _diameter_spike_end)
                        {
                            if (xx == 0 && yy == 0 && zz == 0)
                            {
                                continue;
                            }

                            if (xpos < 0)
                            {
                                xpos *= -1;
                                xpos = (ChunkWidth_R) + xpos;
                            }

                            if (ypos < 0)
                            {
                                ypos *= -1;
                                ypos = (ChunkHeight_R) + ypos;
                            }

                            if (zpos < 0)
                            {
                                zpos *= -1;
                                zpos = (ChunkDepth_R) + zpos;
                            }

                            _index = (int)Math.Round(xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos));

                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                            }
                        }
                    }
                }
            }
        }








        //shit. where did i find that. stackoverflow? code project?
        static internal int FastRand(int Seed, int MaxN)
        {
            Seed = (214013 * Seed + 2531011);
            return ((Seed >> 16) & 0x7FFF) % MaxN;
        }



        float _vert_offset_x;
        float _vert_offset_y;
        float _vert_offset_z;


        public void startBuildingArray(Vector3 currentPosition, out SC_cube.DVertex[] vertexArray, out int[] triangleArray, out int[] mapper, float _planeSize, int minx, int maxx, int miny, int maxy, int minz, int maxz, int _ChunkWidth_L, int _ChunkWidth_R, int _ChunkHeight_L, int _ChunkHeight_R, int _ChunkDepth_L, int _ChunkDepth_R, float maxDistance, float vert_offset_x, float vert_offset_y, float vert_offset_z) //, out int vertexNum, out int indicesNum //Vector3 currentPosition, out Vector3[] vertexArray, out int[] indicesArray, 
        {

            _vert_offset_x = vert_offset_x;
            _vert_offset_y = vert_offset_y;
            _vert_offset_z = vert_offset_z;




            ChunkWidth_L = _ChunkWidth_L;
            ChunkWidth_R = _ChunkWidth_R;
            ChunkHeight_L = _ChunkHeight_L;
            ChunkHeight_R = _ChunkHeight_R;
            ChunkDepth_L = _ChunkDepth_L;
            ChunkDepth_R = _ChunkDepth_R;

            _pos = currentPosition;

            planeSize = _planeSize;

            //ChunkWidth = _width;
            //ChunkHeight = _height;
            //ChunkDepth = _depth;

            _max = (ChunkWidth_L + ChunkWidth_R + 1) * (ChunkHeight_L + ChunkHeight_R + 1) * (ChunkDepth_L + ChunkDepth_R + 1);

            map = new int[(int)_max];

            //int radius = 5;
            //var fastNoise = new FastNoise(); // to use later for spike ends perlin noise. 

            //seed = (int)Math.Floor(rand.NextDouble() * (currentPosition.X - 1) + 1);

            //SC_pathfind_assets.sc_pathfind_grid grid = new SC_pathfind_assets.sc_pathfind_grid();
            //grid.sc_init_pathfind_grid(ChunkWidth_L, ChunkWidth_R, ChunkHeight_L, ChunkHeight_R, ChunkDepth_L, ChunkDepth_R);


            //create sphere
            for (int x = -ChunkWidth_L; x <= ChunkWidth_R; x++)
            {
                for (int y = -ChunkHeight_L; y <= ChunkHeight_R; y++)
                {
                    for (int z = -ChunkDepth_L; z <= ChunkDepth_R; z++)
                    {
                        float posX = (x);
                        float posY = (y);
                        float posZ = (z);

                        var xx = x;
                        var yy = y;
                        var zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = (ChunkWidth_R) + xx;
                        }
                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = (ChunkHeight_R) + yy;
                        }
                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = (ChunkDepth_R) + zz;
                        }

                        int _index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);

                        Vector3 position = new Vector3(posX, posY, posZ);

                        float distance = Vector3.Distance(position, currentPosition);

                        //Vector3 position1 = currentPosition;
                        //float distance1 = Vector3.Distance(position1, center);
                        //_current_visual_distance_spike_glyco_protein_covid19_min = ((ChunkWidth_L + ChunkWidth_R) * 0.35f);
                        //_current_visual_distance_spike_glyco_protein_covid19_max = ((ChunkWidth_L + ChunkWidth_R) - 1); // increase size of chunk for longer glycoprotein spike
                        float distancer = sc_maths.sc_check_distance_node_3d_geometry(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, maxx, maxy, maxz); //11.31415926535f


                        if (distancer < ((maxDistance))) // 0.35f
                        {
                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                            }
                        }

                        /*
                        if (distancer < ((ChunkWidth_L) * _min_sphere_covid19_diameter)) // 0.35f
                        {
                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                            }
                        }*/
                    }
                }
            }

            Regenerate(currentPosition);

            vertexArray = listOfVerts.ToArray();
            triangleArray = listOfTriangleIndices.ToArray();
            mapper = map;
        }

        public void Regenerate(Vector3 currentPosition)
        {
            for (int x = -ChunkWidth_L; x <= ChunkWidth_R; x++)
            {
                for (int y = -ChunkHeight_L; y <= ChunkHeight_R; y++)
                {
                    for (int z = -ChunkDepth_L; z <= ChunkDepth_R; z++)
                    {
                        var xx = x;
                        var yy = y;
                        var zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = (ChunkWidth_R) + xx;
                        }
                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = (ChunkHeight_R) + yy;
                        }
                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = (ChunkDepth_R) + zz;
                        }

                        int _index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);

                        block = map[_index];

                        if (block == 0) continue;
                        {
                            DrawBrick(x, y, z, xx, yy, zz);
                        }
                    }
                }
            }
        }

        public void DrawBrick(int x, int y, int z, int xx, int yy, int zz)
        {
            Vector3 start = new Vector3((x + _vert_offset_x) * planeSize, (y + _vert_offset_y) * planeSize, (z + _vert_offset_z) * planeSize) + _pos;
            //start.X -= ((ChunkWidth_L + ChunkWidth_R + 1) * planeSize) * 0.5f;
            //start.Y -= ((ChunkHeight_L + ChunkHeight_R + 1) * planeSize) * 0.5f;
            //start.Z -= ((ChunkDepth_L + ChunkDepth_R + 1) * planeSize) * 0.5f;

            Vector3 offset1, offset2;


            //TOPFACE
            if (IsTransparent(x, y + 1, z))
            {
                offset1 = forward * planeSize;
                offset2 = right * planeSize;
                createTopFace(start + up * planeSize, offset1, offset2);
                //MessageBox((IntPtr)0, "createTopFace", "Oculus error", 0);
            }
            //BOTTOMFACE
            if (IsTransparent(x, y - 1, z))
            {
                offset1 = right * planeSize;
                offset2 = forward * planeSize;
                createBottomFace(start, offset1, offset2);
                //MessageBox((IntPtr)0, "createBottomFace", "Oculus error", 0);
            }

            //LEFTFACE
            if (IsTransparent(x - 1, y, z))
            {
                offset1 = back * planeSize;
                offset2 = down * planeSize;
                createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2);
            }

            //RIGHTFACE
            if (IsTransparent(x + 1, y, z))
            {
                offset1 = up * planeSize;
                offset2 = forward * planeSize;
                createRightFace(start + right * planeSize, offset1, offset2);
            }
            //FRONTFACE
            if (IsTransparent(x, y, z - 1))
            {
                offset1 = left * planeSize;
                offset2 = up * planeSize;
                createFrontFace(start + right * planeSize, offset1, offset2);
            }
            //BACKFACE
            if (IsTransparent(x, y, z + 1))
            {
                offset1 = right * planeSize;
                offset2 = up * planeSize;
                createBackFace(start + forward * planeSize, offset1, offset2);
            }

        }

        private void createTopFace(Vector3 start, Vector3 offset1, Vector3 offset2)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start,
                texture = new Vector2(0, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 1, 0),

            });

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1,
                texture = new Vector2(0, 1),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 1, 0),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset2,
                texture = new Vector2(1, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 1, 0),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1 + offset2,
                texture = new Vector2(1, 1),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 1, 0),
            });

            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
        }



        private void createBottomFace(Vector3 start, Vector3 offset1, Vector3 offset2)
        {
            int index = listOfVerts.Count;
            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start,
                texture = new Vector2(0f, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(0, 1, -1),
            });

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1,
                texture = new Vector2(0f, 1f),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(0, 1, -1),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset2,
                texture = new Vector2(1, 0),
                normal = new Vector3(0, 1, -1),
                color = new Vector4(colorX, colorY, colorZ, 1),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1 + offset2,
                texture = new Vector2(1, 1f),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(0, 1, -1),
            });

            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
        }


        private void createFrontFace(Vector3 start, Vector3 offset1, Vector3 offset2)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start,
                texture = new Vector2(0, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 0, 0),
            });

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1,
                texture = new Vector2(0, 1f),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 0, 0),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset2,
                texture = new Vector2(1, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 0, 0),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1 + offset2,
                texture = new Vector2(1, 1f),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 0, 0),
            });

            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);

        }
        private void createBackFace(Vector3 start, Vector3 offset1, Vector3 offset2)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start,
                texture = new Vector2(0, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(0, 0, -1),
            });

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1,
                texture = new Vector2(0, 1),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(0, 0, -1),
            });

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset2,
                texture = new Vector2(1, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(0, 0, -1),
            });

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1 + offset2,
                texture = new Vector2(1, 1f),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(0, 0, -1),
            });

            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);

        }

        private void createRightFace(Vector3 start, Vector3 offset1, Vector3 offset2)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start,
                texture = new Vector2(0, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 0, -1),
            });

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1,
                texture = new Vector2(0, 1),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 0, -1),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset2,
                texture = new Vector2(1, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 0, -1),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1 + offset2,
                texture = new Vector2(1, 1f),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 0, -1),
            });

            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
        }

        private void createleftFace(Vector3 start, Vector3 offset1, Vector3 offset2)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start,
                texture = new Vector2(0, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 1, -1),
            });

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1,
                texture = new Vector2(0, 1),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 1, -1),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset2,
                texture = new Vector2(1, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 1, -1),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1 + offset2,
                texture = new Vector2(1, 1),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 1, -1),
            });


            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
        }
        public bool IsTransparent(int x, int y, int z)
        {
            if ((x < -ChunkWidth_L) || (y < -ChunkHeight_L) || (z < -ChunkDepth_L) || (x >= ChunkWidth_R + 1) || (y >= (ChunkHeight_R + 1)) || (z >= (ChunkDepth_R + 1)))
            {
                return true;
            }

            if (x < 0)
            {
                x *= -1;
                x = (ChunkWidth_R) + x;
            }
            if (y < 0)
            {
                y *= -1;
                y = (ChunkHeight_R) + y;
            }
            if (z < 0)
            {
                z *= -1;
                z = (ChunkDepth_R) + z;
            }

            int _index = x + (ChunkWidth_L + ChunkWidth_R + 1) * (y + (ChunkHeight_L + ChunkHeight_R + 1) * z);
            return map[_index] == 0;
        }
        public void SetBrick(int x, int y, int z, byte block, Vector3 currentposition)
        {

            /*x -= Math.Round(posX);
            y -= Math.Round(posY);
            z -= Math.Round(posZ);

            if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= width) || (z >= width))
            {
                return;
            }
            if (map[x, y, z] != block)
            {
                map[x, y, z] = block;
                Regenerate();
            }*/
        }
    }
}








