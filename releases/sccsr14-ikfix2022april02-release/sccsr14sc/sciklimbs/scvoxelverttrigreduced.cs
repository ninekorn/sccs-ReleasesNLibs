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

namespace sccs.scgraphics
{




    //https://www.c-sharpcorner.com/article/generating-random-number-and-string-in-C-Sharp/
    public class scvoxelverttrigreduced
    {
        public struct sc_chunk_node
        {
            public float _distance_parent_node_to_this;
            public float _distance_to_target;
            public Vector3 _position;
        }



        //vertex/triangle reducer variables
        //public byte[] map;
        private int[] map;
        private int block;

        private Vector4 forward = new Vector4(0, 0, 1, 1);
        private Vector4 back = new Vector4(0, 0, -1, 1);
        private Vector4 right = new Vector4(1, 0, 0, 1);
        private Vector4 left = new Vector4(-1, 0, 0, 1);
        private Vector4 up = new Vector4(0, 1, 0, 1);
        private Vector4 down = new Vector4(0, -1, 0, 1);


        private List<SC_cube.DVertex> vertexlist = new List<SC_cube.DVertex>(); //listOfVerts
        private List<int> listOfTriangleIndices = new List<int>();

        //public List<Vector3> vertexlist;
        //public List<Vector3> normalslist;
        //public List<Vector4> colorslist;
        //public List<Vector4> indexposlist;
        //public List<Vector2> textureslist;





        int _block;
        int index;


        int t;//  = 0;
        int posx;//  = 0;
        int posy;//  = 0;
        int posz;//  = 0;
        int xx;// 
        int yy;// 
        int zz;// 
        int xi;// 
        int yi;// 
        int zi;// 

        int swtchx;// 
        int swtchy;// 
        public int swtchz;// 



        //public List<int> triangles;
        public int[] _chunkArray;
        public int[] _tempChunkArray;
        public int[] _tempChunkArrayRightFace;
        public int[] _tempChunkArrayLeftFace;
        public int[] _tempChunkArrayFrontFace;
        public int[] _tempChunkArrayBackFace;
        public int[] _tempChunkArrayBottomFace;
        public int[] _chunkVertexArray;
        public int[] _testVertexArray;

        public int activeBlockType;
        public float planeSize;
        public Vector3 chunkPos;
        public float realplanetwidth;


        public int width;
        public int height;
        public int depth;

        int total;
        int totalBytes;
        int vertexlistWidth;
        int vertexlistHeight;
        int vertexlistDepth;


        int counterCreateChunkObjectFacesBytes;// 
        int tBytes;// 
        int posxBytes;// 
        int posyBytes;// 
        int poszBytes;// 
        int xxBytes;// 
        int yyBytes;// 
        int zzBytes;// 
        int xiBytes;// 
        int yiBytes;// 
        int ziBytes;// 

        int swtchxBytes;// 
        int swtchyBytes;// 
        public int swtchzBytes;// 

        int rowIterateXBytes;// 
        int rowIterateZBytes;// 
        int rowIterateYBytes;// 
        public int chunkbuildingswtc;
        //public void sccsCustomStart(Transform planetchunk_, Vector3 chunkpos_, float planeSize_, float realplanetwidth_, int width_, int height_, int depth_, sccsproceduralplanetbuilderGen2 componentParent_, int addfracturedcubeonimpact_, NewObjectPoolerScript UnityTutorialGameObjectPool_)

        int _maxWdth;// = 0;
        int _maxHght;
        int _maxDepth;// = 0;

        int rowIterateX;// = 0;
        int rowIterateY;
        int rowIterateZ;// = 0;

        bool foundVertOne;// = false;
        bool foundVertTwo;// = false;
        bool foundVertThree;// = false;
        bool foundVertFour;// = false;



        int _index0;// = 0;
        int _index1;// = 0;
        int _index2;// = 0;
        int _index3;// = 0;
        int _newVertzCounter;// = 0;

        int oneVertIndexX;// = 0;
        int oneVertIndexY;// = 0;
        int oneVertIndexZ;// = 0;

        int twoVertIndexX;// = 0;
        int twoVertIndexY;// = 0;
        int twoVertIndexZ;// = 0;

        int threeVertIndexX;// = 0;
        int threeVertIndexY;//= 0;
        int threeVertIndexZ;// = 0;

        int fourVertIndexX;// = 0;
        int fourVertIndexY;// = 0;
        int fourVertIndexZ;// = 0;
        int counterCreateChunkObjectFaces;//  = 0;

        Vector4 chunkcolor = new Vector4(0.35f, 0.35f, 0.35f, 1);
        FastNoise fastNoise = new FastNoise();
        float staticPlaneSize;
        float alternateStaticPlaneSize;
        private int seed = 3420; // 3420
        private int _detailScale = 10; // 10
        private int _HeightScale = 200; //200





































        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        private Bitmap bitmap;
        private System.Drawing.Color[,] colors;
        private Bitmap earthLookupBitmap;
        private System.Drawing.Color[] earthLookupTable;



        public int ChunkWidth_L = 50;
        public int ChunkWidth_R = 49;

        public int ChunkHeight_L = 50;
        public int ChunkHeight_R = 49;

        public int ChunkDepth_L = 50;
        public int ChunkDepth_R = 49;

        //public int ChunkWidth = -1;
        //public int ChunkHeight = -1;
        //public int ChunkDepth = -1;


        int _max;


        float _current_visual_distance_spike_glyco_protein_covid19_min = 0;
        float _current_visual_distance_spike_glyco_protein_covid19_max = 0;
        float _current_counter_for_adding_spike_glyco_protein_covid19 = 0;

        //public byte[] map;
        /*private int[] map;
        private float planeSize = 1;
        private int seed = 3420;

        private int block;*/

        private int counterVertexTop = 0;

        private int vertzIndex = 0;
        private int trigsIndex = 0;

        //private int _detailScale = 10;
        private int _heightScale = 10;

        /*private Vector3 forward = new Vector3(0, 0, 1);
        private Vector3 back = new Vector3(0, 0, -1);
        private Vector3 right = new Vector3(1, 0, 0);
        private Vector3 left = new Vector3(-1, 0, 0);
        private Vector3 up = new Vector3(0, 1, 0);
        private Vector3 down = new Vector3(0, -1, 0);*/


        //private List<SC_cube.DVertex> listOfVerts = new List<SC_cube.DVertex>();
        //private List<int> listOfTriangleIndices = new List<int>();

        int randX = 3420;
        int randY = 3420;
        public static int countingArrayOfChunks = 0;


        float colorX = 0.75f;
        float colorY = 0.75f;
        float colorZ = 0.75f;
        float _tinyChunkHeightScale = 200;
        Vector3 chunkinitpos;



        int _swtch_spike_00 = 0;
        int _swtch_spike_01 = 0;
        int _swtch_spike_02 = 0;
        int _swtch_spike_03 = 0;
        int _swtch_spike_04 = 0;
        int _swtch_spike_05 = 0;
        int _swtch_spike_06 = 0;
        int _swtch_spike_07 = 0;



        //FROM CRAIG PERKO TUTORIALS
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


            for (float i = 0; i < _spike_max_length * 1.0f;) //_spike_max_length //(float)Math.Round(_spike_max_length * 0.75f)
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
            for (int xx = -6; xx <= 6; xx++)
            {
                for (int yy = -6; yy <= 6; yy++)
                {
                    for (int zz = -6; zz <= 6; zz++)
                    {
                        xpos = (int)Math.Round(current_start_move_pos.Value.X + xx);
                        ypos = (int)Math.Round(current_start_move_pos.Value.Y + yy);
                        zpos = (int)Math.Round(current_start_move_pos.Value.Z + zz);

                        float distance = Vector3.Distance(current_start_move_pos.Value, new Vector3(xpos, ypos, zpos));
                        if (distance < 5)
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








        //shit. where did i find that.
        static internal int FastRand(int Seed, int MaxN)
        {
            Seed = (214013 * Seed + 2531011);
            return ((Seed >> 16) & 0x7FFF) % MaxN;
        }





        float _vert_offset_x;
        float _vert_offset_y;
        float _vert_offset_z;

        Vector3 someOffsetPos = new Vector3(0, 0, 0);
        public List<Vector3> listofpickaxetipbytes = new List<Vector3>();

        /*
        //public void startBuildingArray(Vector3 currentPosition, out SC_cube.DVertex[] vertexArray, out int[] triangleArray, out int[] mapper, float _planeSize, int minx, int miny, int minz, float diagmaxx, float diagmaxy, float diagmaxz, float diagminx, float diagminy, float diagminz, int _ChunkWidth_L, int _ChunkWidth_R, int _ChunkHeight_L, int _ChunkHeight_R, int _ChunkDepth_L, int _ChunkDepth_R, float maxDistance, float vert_offset_x, float vert_offset_y, float vert_offset_z, int voxel_type) //, out int vertexNum, out int indicesNum //Vector3 currentPosition, out Vector3[] vertexArray, out int[] indicesArray, 
        public void startBuildingArray(Vector4 currentPosition, out SC_cube.DVertex[] vertexArray, out int[] triangleArray, out int[] mapper, float _planeSize, int minx, int miny, int minz, float diagmaxx, float diagmaxy, float diagmaxz, float diagminx, float diagminy, float diagminz, int _ChunkWidth_L, int _ChunkWidth_R, int _ChunkHeight_L, int _ChunkHeight_R, int _ChunkDepth_L, int _ChunkDepth_R, float maxDistance, float vert_offset_x, float vert_offset_y, float vert_offset_z, int voxel_type)
        {




            /*currentPosition.X -= planeSize * diagmaxx;
            currentPosition.Y -= planeSize * diagmaxy;
            currentPosition.Z -= planeSize * diagmaxz;


            staticPlaneSize = SC_Globals.planeSize;

            if (staticPlaneSize == 1)
            {
                staticPlaneSize = SC_Globals.planeSize * 0.1f;
                alternateStaticPlaneSize = SC_Globals.planeSize * 0.1f;
            }
            else if (staticPlaneSize == 0.1f)
            {
                staticPlaneSize = SC_Globals.planeSize;
                alternateStaticPlaneSize = SC_Globals.planeSize * 10;
            }
            else if (staticPlaneSize == 0.01f)
            {
                staticPlaneSize = SC_Globals.planeSize;
                alternateStaticPlaneSize = SC_Globals.planeSize * 1000;
            }

            _detailScale = 10;
            _HeightScale = 200;

            /*activeBlockType = 0;
            planetchunk = planetchunk_;
            chunkPos = chunkpos_;
            planeSize = planeSize_;
            realplanetwidth = realplanetwidth_;
            width = width_;
            height = height_;
            depth = depth_;


            chunkPos = new Vector3(currentPosition.X, currentPosition.Y, currentPosition.Z);

            planeSize = 1; //SC_Globals.planeSize

            realplanetwidth = 4;










            //componentParent = componentParent_;
            //addfracturedcubeonimpact = addfracturedcubeonimpact_;
            //UnityTutorialGameObjectPool = UnityTutorialGameObjectPool_;

            // this.GameObject.position;

            /*
            this.gameObject.tag = "collisionObject";
            this.gameObject.layer = 8; //"collisionObject"
            UnityTutorialGameObjectPool = this.GameObject.GetComponent<NewObjectPoolerScript>();

            parentObject = this.GameObject.parent;
            //componentParent = parentObject.gameObject.GetComponent<sccsproceduralplanetbuilderGen2>().currentplanetbuilder;

            mesh = new Mesh();
            this.gameObject.GetComponent<MeshFilter>().mesh = mesh;
            this.gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
            */

        //normalslist = new List<Vector3>();
        //colorslist = new List<Vector4>();
        //indexposlist = new List<Vector4>();
        //textureslist = new List<Vector2>();










        /*realplanetwidth = planeSize * width;

        map = new int[SC_Globals.tinyChunkWidth * SC_Globals.tinyChunkHeight * SC_Globals.tinyChunkDepth];

        for (int x = 0; x < SC_Globals.tinyChunkWidth; x++)
        {
            for (int y = 0; y < SC_Globals.tinyChunkHeight; y++)
            {
                for (int z = 0; z < SC_Globals.tinyChunkDepth; z++)
                {
                    map[x + SC_Globals.tinyChunkWidth * (y + SC_Globals.tinyChunkHeight * z)] = 1;


                    /*float noiseXZ = 20;

                    noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (currentPosition.X * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale, (((y * staticPlaneSize) + (currentPosition.Y * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale, (((z * staticPlaneSize) + (currentPosition.Z * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale);

                    if (noiseXZ >= 0.1f)
                    {
                        map[x + SC_Globals.tinyChunkWidth * (y + SC_Globals.tinyChunkHeight * z)] = 1;
                    }
                    else if (y == 0 && currentPosition.Y == 0)
                    {
                        map[x + SC_Globals.tinyChunkWidth * (y + SC_Globals.tinyChunkHeight * z)] = 1;
                    }
                    else
                    {
                        map[x + SC_Globals.tinyChunkWidth * (y + SC_Globals.tinyChunkHeight * z)] = 0;
                    }


                }
            }
        }


        _vert_offset_x = vert_offset_x;
        _vert_offset_y = vert_offset_y;
        _vert_offset_z = vert_offset_z;

        ChunkWidth_L = _ChunkWidth_L;
        ChunkWidth_R = _ChunkWidth_R;
        ChunkHeight_L = _ChunkHeight_L;
        ChunkHeight_R = _ChunkHeight_R;
        ChunkDepth_L = _ChunkDepth_L;
        ChunkDepth_R = _ChunkDepth_R;







        _pos = new Vector3(currentPosition.X, currentPosition.Y, currentPosition.Z);


        planeSize = _planeSize;

        /*
        _pos.X -= (ChunkWidth_L * planeSize);
        _pos.Y -= (ChunkHeight_L * planeSize);
        _pos.Z -= (ChunkDepth_L * planeSize);






        //ChunkWidth = _width;
        //ChunkHeight = _height;
        //ChunkDepth = _depth;

        _max = (ChunkWidth_L + ChunkWidth_R + 1) * (ChunkHeight_L + ChunkHeight_R + 1) * (ChunkDepth_L + ChunkDepth_R + 1);












        total = _max;
        totalBytes = _max;

        vertexlistWidth = ChunkWidth_L + ChunkWidth_R + 2;
        vertexlistHeight = ChunkHeight_L + ChunkHeight_R + 2;
        vertexlistDepth = ChunkDepth_L + ChunkDepth_R + 2;

        _tempChunkArrayBottomFace = new int[total];
        _tempChunkArrayBackFace = new int[total];
        _tempChunkArrayFrontFace = new int[total];
        _tempChunkArrayLeftFace = new int[total];
        _tempChunkArrayRightFace = new int[total];
        _tempChunkArray = new int[total];

        _chunkArray = new int[total];

        _chunkVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
        _testVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

        //vertexlist = new List<Vector3>();

        vertexlist = new List<SC_cube.DVertex>();

        listOfTriangleIndices = new List<int>();




        width = ChunkWidth_L + ChunkWidth_R + 1;
        height = ChunkHeight_L + ChunkHeight_R + 1;
        depth = ChunkDepth_L + ChunkDepth_R + 1;







        map = new int[(int)_max];

        int radius = 5;

        //Vector3 center = new Vector3(((ChunkWidth_L + ChunkWidth_R + 1)) * 0.5f, (ChunkHeight) * 0.5f, (ChunkDepth) * 0.5f);
        //var fastNoise = new FastNoise();

        seed = (int)Math.Floor(rand.NextDouble() * (currentPosition.X - 1) + 1);
        // not working as i need my other program called 1stVersion for instances to also incorporate different chunk seeds.                                                                               
        //and that other program might be long to incorporate.

















        /*
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    float posX = (x);
                    float posY = (y);
                    float posZ = (z);

                    int _index = x + (width) * (y + (height) * z);

                    Vector3 position = new Vector3(posX, posY, posZ);
                    Vector3 altposition = new Vector3(x, y, z);


                    float distance = 0;
                    //float distance = sc_maths.sc_check_distance_node_3d_geometry(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, maxx, maxy, maxz); //11.31415926535f


                    //if(_index < total)
                    //{
                    //    map[_index] = 1;
                    //}

                    map[_index] = 1;

                    /*
                    if (voxel_type == 0)
                    {
                        distance = Vector3.Distance(position, _pos);
                        if (distance < ((ChunkWidth_L) * 0.25f)) // 0.35f
                        {
                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                            }
                        }
                    }
                    else if (voxel_type == 1)
                    {
                        //distance = sc_maths.sc_check_distance_node_3d_geometry(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, maxx, maxy, maxz);
                        distance = sc_maths.sc_check_distance_node_3d(_pos, new Vector3(posX, posY, posZ), minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz);

                        if (distance < ((ChunkWidth_L) * maxDistance)) // 0.35f
                        {
                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                            }
                        }
                    }
                    else if (voxel_type == 2)
                    {
                        //distance = Vector3.Distance(altposition, _pos);// Vector3.Zero);
                        distance = Vector3.Distance(position, _pos);
                        //distance = sc_maths.sc_check_distance_node_3d(_pos, new Vector3(posX, posY, posZ), minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz);

                        //if (distance < ((ChunkWidth_L) * maxDistance)) // 0.35f
                        {
                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                            }
                        }
                    }
                }
            }
        }



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
                    Vector3 altposition = new Vector3(x, y, posZ);


                    //float distance = 0;
                    //float distance = sc_maths.sc_check_distance_node_3d_geometry(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, maxx, maxy, maxz); //11.31415926535f
                    float distance = sc_maths.sc_check_distance_node_3d(_pos, new Vector3(posX, posY, posZ), minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz);


                    //if(_index < total)
                    //{
                    //    map[_index] = 1;
                    //}
                    if (_index >= 0 && _index < _max)
                    {
                        map[_index] = 1;
                    }



                    /*if (voxel_type == 0)
                    {
                        distance = Vector3.Distance(position, _pos);
                        if (distance < ((ChunkWidth_L) * 0.25f)) // 0.35f
                        {
                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                            }
                        }
                    }
                    else if (voxel_type == 1)
                    {
                        //distance = sc_maths.sc_check_distance_node_3d_geometry(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, maxx, maxy, maxz);
                        distance = sc_maths.sc_check_distance_node_3d(_pos, new Vector3(posX, posY, posZ), minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz);

                        if (distance < ((ChunkWidth_L) * maxDistance)) // 0.35f
                        {
                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                            }
                        }
                    }
                    else if (voxel_type == 2)
                    {
                        //distance = Vector3.Distance(altposition, Vector3.Zero);
                        distance = Vector3.Distance(position, _pos);
                        //distance = sc_maths.sc_check_distance_node_3d(_pos, new Vector3(posX, posY, posZ), minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz);

                        if (distance < ((ChunkWidth_L) * maxDistance)) // 0.35f
                        {
                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                            }
                        }
                    }
                }
            }
        }




        /*
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
                    Vector3 altposition = new Vector3(x, y, posZ);


                    float distance = 0;
                    //float distance = sc_maths.sc_check_distance_node_3d_geometry(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, maxx, maxy, maxz); //11.31415926535f


                    //if(_index < total)
                    //{
                    //    map[_index] = 1;
                    //}




                    if (voxel_type == 0)
                    {
                        distance = Vector3.Distance(position, _pos);
                        if (distance < ((ChunkWidth_L) * 0.25f)) // 0.35f
                        {
                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                            }
                        }
                    }
                    else if (voxel_type == 1)
                    {
                        //distance = sc_maths.sc_check_distance_node_3d_geometry(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, maxx, maxy, maxz);
                        distance = sc_maths.sc_check_distance_node_3d(_pos, new Vector3(posX, posY, posZ), minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz);

                        if (distance < ((ChunkWidth_L) * maxDistance)) // 0.35f
                        {
                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                            }
                        }
                    }
                    else if (voxel_type == 2)
                    {
                        distance = Vector3.Distance(altposition, Vector3.Zero);
                        //distance = Vector3.Distance(position, _pos);
                        //distance = sc_maths.sc_check_distance_node_3d(_pos, new Vector3(posX, posY, posZ), minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz);

                        if (distance < ((ChunkWidth_L) * maxDistance)) // 0.35f
                        {
                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                            }
                        }
                    }

                }
            }
        }*/





        /*
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    float posX = (x);
                    float posY = (y);
                    float posZ = (z);

                    int _index = x + (width) * (y + (height) * z);

                    Vector3 position = new Vector3(posX, posY, posZ);

                    float distance = 0;
                    //float distance = sc_maths.sc_check_distance_node_3d_geometry(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, maxx, maxy, maxz); //11.31415926535f



                    if (voxel_type == 0)
                    {
                        distance = Vector3.Distance(position, _pos);
                        if (distance < ((ChunkWidth_L) * 0.25f)) // 0.35f
                        {
                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                            }
                        }
                    }
                    else if (voxel_type == 1)
                    {
                        //distance = sc_maths.sc_check_distance_node_3d_geometry(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, maxx, maxy, maxz);
                        distance = sc_maths.sc_check_distance_node_3d(_pos, new Vector3(posX, posY, posZ), minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz);

                        if (distance < ((ChunkWidth_L) * maxDistance)) // 0.35f
                        {
                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                            }
                        }
                    }
                    else if (voxel_type == 2)
                    {
                        distance = Vector3.Distance(position + _pos, _pos);
                        if (distance < ((ChunkWidth_L) * maxDistance)) // 0.35f
                        {
                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                            }
                        }
                    }
                }
            }
        }*/









        /*
        for (int x = 0; x < SC_Globals.tinyChunkWidth; x++)
        {
            for (int y = 0; y < SC_Globals.tinyChunkHeight; y++)
            {
                for (int z = 0; z < SC_Globals.tinyChunkDepth; z++)
                {
                    if (swtchz == 0)
                    {
                        CreateChunkFaces();
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }



        sccsSetMap();
        Regenerate(); //currentPosition

        vertexArray = vertexlist.ToArray();
        triangleArray = listOfTriangleIndices.ToArray();
        mapper = map;


    }*/




        public void startBuildingArray(Vector3 currentPosition, out SC_cube.DVertex[] vertexArray, out int[] triangleArray, out int[] mapper, float _planeSize, int minx, int miny, int minz, float diagmaxx, float diagmaxy, float diagmaxz, float diagminx, float diagminy, float diagminz, int _ChunkWidth_L, int _ChunkWidth_R, int _ChunkHeight_L, int _ChunkHeight_R, int _ChunkDepth_L, int _ChunkDepth_R, float maxDistance, float vert_offset_x, float vert_offset_y, float vert_offset_z, int voxel_type, int voxelbodyparttype, int somechunkpriminstanceikarmvoxelindex) //, out int vertexNum, out int indicesNum //Vector3 currentPosition, out Vector3[] vertexArray, out int[] indicesArray, 
        {
            float maxx = 2;
            float maxy = 8;
            float maxz = 2;

            /*_vert_offset_x = vert_offset_x;
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

            int radius = 5;

            //Vector3 center = new Vector3(((ChunkWidth_L + ChunkWidth_R + 1)) * 0.5f, (ChunkHeight) * 0.5f, (ChunkDepth) * 0.5f);
            //var fastNoise = new FastNoise();

            seed = (int)Math.Floor(rand.NextDouble() * (currentPosition.X - 1) + 1);
            // not working as i need my other program called 1stVersion for instances to also incorporate different chunk seeds.                                                                               
            //and that other program might be long to incorporate.
            */


            /*
            total = _max;
            totalBytes = _max;

            vertexlistWidth = ChunkWidth_L + ChunkWidth_R + 2;
            vertexlistHeight = ChunkHeight_L + ChunkHeight_R + 2;
            vertexlistDepth = ChunkDepth_L + ChunkDepth_R + 2;

            _tempChunkArrayBottomFace = new int[total];
            _tempChunkArrayBackFace = new int[total];
            _tempChunkArrayFrontFace = new int[total];
            _tempChunkArrayLeftFace = new int[total];
            _tempChunkArrayRightFace = new int[total];
            _tempChunkArray = new int[total];

            _chunkArray = new int[total];

            _chunkVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

            //vertexlist = new List<Vector3>();

            vertexlist = new List<SC_cube.DVertex>();

            listOfTriangleIndices = new List<int>();*/




            /*
            currentPosition.X -= planeSize * diagmaxx;
            currentPosition.Y -= planeSize * diagmaxy;
            currentPosition.Z -= planeSize * diagmaxz;
            */



            staticPlaneSize = SC_Globals.planeSize;

            if (staticPlaneSize == 1)
            {
                staticPlaneSize = SC_Globals.planeSize * 0.1f;
                alternateStaticPlaneSize = SC_Globals.planeSize * 0.1f;
            }
            else if (staticPlaneSize == 0.1f)
            {
                staticPlaneSize = SC_Globals.planeSize;
                alternateStaticPlaneSize = SC_Globals.planeSize * 10;
            }
            else if (staticPlaneSize == 0.01f)
            {
                staticPlaneSize = SC_Globals.planeSize;
                alternateStaticPlaneSize = SC_Globals.planeSize * 1000;
            }

            _detailScale = 10;
            _HeightScale = 200;

            /*activeBlockType = 0;
            planetchunk = planetchunk_;
            chunkPos = chunkpos_;
            planeSize = planeSize_;
            realplanetwidth = realplanetwidth_;
            width = width_;
            height = height_;
            depth = depth_;


            chunkPos = new Vector3(currentPosition.X, currentPosition.Y, currentPosition.Z);

            planeSize = 1; //SC_Globals.planeSize

            realplanetwidth = 4;*/










            //componentParent = componentParent_;
            //addfracturedcubeonimpact = addfracturedcubeonimpact_;
            //UnityTutorialGameObjectPool = UnityTutorialGameObjectPool_;

            // this.GameObject.position;

            /*
            this.gameObject.tag = "collisionObject";
            this.gameObject.layer = 8; //"collisionObject"
            UnityTutorialGameObjectPool = this.GameObject.GetComponent<NewObjectPoolerScript>();

            parentObject = this.GameObject.parent;
            //componentParent = parentObject.gameObject.GetComponent<sccsproceduralplanetbuilderGen2>().currentplanetbuilder;

            mesh = new Mesh();
            this.gameObject.GetComponent<MeshFilter>().mesh = mesh;
            this.gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
            */

            //normalslist = new List<Vector3>();
            //colorslist = new List<Vector4>();
            //indexposlist = new List<Vector4>();
            //textureslist = new List<Vector2>();








            _vert_offset_x = vert_offset_x;
            _vert_offset_y = vert_offset_y;
            _vert_offset_z = vert_offset_z;

            ChunkWidth_L = _ChunkWidth_L;
            ChunkWidth_R = _ChunkWidth_R;
            ChunkHeight_L = _ChunkHeight_L;
            ChunkHeight_R = _ChunkHeight_R;
            ChunkDepth_L = _ChunkDepth_L;
            ChunkDepth_R = _ChunkDepth_R;







            chunkinitpos = new Vector3(currentPosition.X, currentPosition.Y, currentPosition.Z);


            planeSize = _planeSize;

            /*
            currentPosition.X -= (ChunkWidth_L * planeSize);
            currentPosition.Y -= (ChunkHeight_L * planeSize);
            currentPosition.Z -= (ChunkDepth_L * planeSize);
            */


            //ChunkWidth = _width;
            //ChunkHeight = _height;
            //ChunkDepth = _depth;

            _max = (ChunkWidth_L + ChunkWidth_R + 1) * (ChunkHeight_L + ChunkHeight_R + 1) * (ChunkDepth_L + ChunkDepth_R + 1);


            total = _max;
            totalBytes = _max;

            vertexlistWidth = ChunkWidth_L + ChunkWidth_R + 2;
            vertexlistHeight = ChunkHeight_L + ChunkHeight_R + 2;
            vertexlistDepth = ChunkDepth_L + ChunkDepth_R + 2;

            _tempChunkArrayBottomFace = new int[total];
            _tempChunkArrayBackFace = new int[total];
            _tempChunkArrayFrontFace = new int[total];
            _tempChunkArrayLeftFace = new int[total];
            _tempChunkArrayRightFace = new int[total];
            _tempChunkArray = new int[total];

            _chunkArray = new int[total];

            _chunkVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

            //vertexlist = new List<Vector3>();

            vertexlist = new List<SC_cube.DVertex>();

            listOfTriangleIndices = new List<int>();


            width = ChunkWidth_L + ChunkWidth_R + 1;
            height = ChunkHeight_L + ChunkHeight_R + 1;
            depth = ChunkDepth_L + ChunkDepth_R + 1;







            map = new int[(int)_max];

            int radius = 5;

            //Vector3 center = new Vector3(((ChunkWidth_L + ChunkWidth_R + 1)) * 0.5f, (ChunkHeight) * 0.5f, (ChunkDepth) * 0.5f);
            //var fastNoise = new FastNoise();

            seed = (int)Math.Floor(rand.NextDouble() * (currentPosition.X - 1) + 1);
            // not working as i need my other program called 1stVersion for instances to also incorporate different chunk seeds.                                                                               
            //and that other program might be long to incorporate.













            //create sphere
            for (int x = -ChunkWidth_L; x <= ChunkWidth_R; x++)
            //for (int x = 0; x < width; x++)
            {
                for (int y = -ChunkHeight_L; y <= ChunkHeight_R; y++)
                //for (int y = 0; y < height; y++)
                {
                    for (int z = -ChunkDepth_L; z <= ChunkDepth_R; z++)
                    //for (int z = 0; z < depth; z++)
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

                        //int _index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);
                        int _index = xx + (width) * (yy + (height) * zz);

                        Vector3 position = new Vector3(posX, posY, posZ);

                        float distance = 0;
                        //float distance = sc_maths.sc_check_distance_node_3d_geometry(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, maxx, maxy, maxz); //11.31415926535f

                        if (_index < _max)
                        {




                            if (voxel_type == 0)
                            {
                                distance = Vector3.Distance(position, currentPosition);
                                if (distance < ((ChunkWidth_L) * 0.25f)) // 0.35f
                                {
                                    if (_index >= 0 && _index < _max)
                                    {
                                        map[_index] = 1;
                                    }
                                }

                            }
                            else if (voxel_type == 1)
                            {
                                //distance = sc_maths.sc_check_distance_node_3d_geometry(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, maxx, maxy, maxz);
                                distance = sc_maths.sc_check_distance_node_3d(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz);
                                if (distance < ((ChunkWidth_L) * maxDistance)) // 0.35f
                                {
                                    if (_index >= 0 && _index < _max)
                                    {
                                        map[_index] = 1;
                                    }
                                }
                            }
                            else if (voxel_type == 2)
                            {

                                //distance = sc_maths.sc_check_distance_node_3d_geometry(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, maxx, maxy, maxz);
                                /*distance = sc_maths.sc_check_distance_node_3d(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz);
                                if (distance < ((ChunkWidth_L) * maxDistance)) // 0.35f
                                {
                                    if (_index >= 0 && _index < _max)
                                    {
                                        map[_index] = 1;
                                    }
                                }
                                */

                                //OUT OF THE BOX - WRONG DISTANCE SO IT GOES FULL SIZE SO YOU CAN CHECK AND COMPARED WITH WHAT YOU'VE GOT IN minx/miny/minz/maxx/maxy/maxz/diagminx/diagminy/diagminz/diagmaxx/diagmaxy/diagmaxz/chunkwidthl/chunkwidthr/chunkheightl/chunkheighr/chunkdepthl/chunkdepthr
                                distance = Vector3.Distance(position, currentPosition);
                                if (distance < ((ChunkWidth_L) * maxDistance)) // 0.35f
                                {
                                    if (_index >= 0 && _index < _max)
                                    {
                                        map[_index] = 1;
                                    }
                                }




                            }
                            else if (voxel_type == 3)
                            {
                                //distance = sc_maths.sc_check_distance_node_3d_geometry(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, maxx, maxy, maxz);
                                distance = sc_maths.sc_check_distance_node_3d(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz);

                                if (distance < ((ChunkWidth_L) * maxDistance) && x >= -maxx && x < maxx && z >= -maxz && z < maxz) // 0.35f
                                {
                                    if (_index >= 0 && _index < _max)
                                    {
                                        map[_index] = 1;
                                    }
                                }

                                if (y >= -ChunkHeight_L && y < -ChunkHeight_L + maxy) // 0.35f // distance < ((ChunkHeight_R) * maxDistance) && 
                                {
                                    if (_index >= 0 && _index < _max)
                                    {

                                        map[_index] = 1;
                                    }
                                }

                                if (y >= -ChunkHeight_L + maxy && y < maxy) // 0.35f // distance < ((ChunkHeight_R) * maxDistance) && 
                                {
                                    if (_index >= 0 && _index < _max)
                                    {
                                        if (y >= -ChunkHeight_L + maxy && y < -ChunkHeight_L + (maxy * 2) && z < -ChunkDepth_L + (maxz * 2))// 0.35f // distance < ((ChunkHeight_R) * maxDistance) && 
                                        {
                                            map[_index] = 2;

                                            listofpickaxetipbytes.Add(position);


                                            float _decimal_for_random = 1.0f;
                                            //float max_spike_length_for_random = 0.85f;

                                            float minX = 1;// ((ChunkWidth_L + ChunkWidth_R + 1) * 0.55f);
                                            float maxX = ((ChunkWidth_L) * 0.975f);

                                            /*if (posX < 0)
                                            {
                                                minX = minX * -1;
                                                maxX = maxX * -1;
                                            }*/

                                            float minY = 1;// ((ChunkHeight_L + ChunkHeight_R + 1) * 0.55f);
                                            float maxY = ((ChunkHeight_L) * 0.975f);
                                            /*if (posY < 0)
                                            {
                                                minY = minY * -1;
                                                maxY = maxY * -1;
                                            }*/

                                            float minZ = 1;// ((ChunkDepth_L + ChunkDepth_R + 1) * 0.55f);
                                            float maxZ = ((ChunkDepth_L) * 0.975f);

                                            /*if (posZ < 0)
                                            {
                                                minZ = minZ * -1;
                                                maxZ = maxZ * -1;
                                            }*/

                                            //int maxDist_width = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * max_spike_length_for_random));
                                            int randX = (int)(Math.Round(sc_maths.getSomeRandNumThousandDecimal((int)Math.Round(minX), (int)Math.Round(maxX), _decimal_for_random, 0, 0)));
                                            //int maxDist_height = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * max_spike_length_for_random));
                                            int randY = (int)(Math.Round(sc_maths.getSomeRandNumThousandDecimal((int)Math.Round(minY), (int)Math.Round(maxY), _decimal_for_random, 0, 0)));
                                            //int maxDist_depth = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * max_spike_length_for_random));
                                            int randZ = (int)(Math.Round(sc_maths.getSomeRandNumThousandDecimal((int)Math.Round(minZ), (int)Math.Round(maxZ), _decimal_for_random, 0, 0)));

                                            float _someMin = 0.35f;
                                            if (posX < 0)
                                            {
                                                randX *= -1;
                                                if ((randX) >= -(ChunkWidth_L + ChunkWidth_R + 1) * _someMin)
                                                {
                                                    randX = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * _someMin)) * -1;
                                                }
                                            }
                                            else
                                            {
                                                if ((randX) < (ChunkWidth_L + ChunkWidth_R + 1) * _someMin)
                                                {
                                                    randX = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * _someMin));
                                                }
                                            }

                                            if (posY < 0)
                                            {
                                                randY *= -1;
                                                if ((randY) >= -(ChunkHeight_L + ChunkHeight_R + 1) * _someMin)
                                                {
                                                    randY = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * _someMin)) * -1;
                                                }
                                            }
                                            else
                                            {
                                                if ((randY) < (ChunkHeight_L + ChunkHeight_R + 1) * _someMin)
                                                {
                                                    randY = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * _someMin));
                                                }
                                            }


                                            if (posZ < 0)
                                            {
                                                randZ *= -1;
                                                if ((randZ) >= -(ChunkDepth_L + ChunkDepth_R + 1) * _someMin)
                                                {
                                                    randZ = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * _someMin)) * -1;
                                                }
                                            }
                                            else
                                            {
                                                if ((randZ) < (ChunkDepth_L + ChunkDepth_R + 1) * _someMin)
                                                {
                                                    randZ = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * _someMin));
                                                }
                                            }

                                            /*if(randX < 0 || randY < 0|| randZ < 0)
                                            {
                                                Console.WriteLine(randX + " _ " + randY + "  " + randZ);
                                            }*/


                                            /*if (_swtch_spike_02 == 0)
                                            {
                                                CreateSpikeGlycoProteinCOVID19(currentPosition, x, y, z);

                                                _swtch_spike_02 = 1;
                                            }*/

                                        }
                                    }
                                }
                            }
                        }






                        //Vector3 position1 = currentPosition;
                        //float distance1 = Vector3.Distance(position1, center);
                        //_current_visual_distance_spike_glyco_protein_covid19_min = ((ChunkWidth_L + ChunkWidth_R) * 0.35f);
                        //_current_visual_distance_spike_glyco_protein_covid19_max = ((ChunkWidth_L + ChunkWidth_R) - 1); // increase size of chunk for longer glycoprotein spike


                        /*if (randX < 0)
                        {
                            randX -= 100;
                        }
                        else
                        {
                            randX += 100;
                        }


                        if (randY < 0)
                        {
                            randY -= 100;
                        }
                        else
                        {
                            randY += 100;
                        }

                        if (randZ < 0)
                        0   .


                        {
                            randZ -= 100;
                        }
                        else
                        {
                            randZ += 100;
                        }*/



                        if (distance >= ((ChunkWidth_L) * 0.35f) && distance < ((ChunkWidth_L) * 0.975f) && voxel_type == 0 || distance >= ((ChunkWidth_L) * 0.35f) && distance < ((ChunkWidth_L) * 0.975f) && voxel_type == 1) //0.35f 0.45f
                        {
                            float _decimal_for_random = 1.0f;
                            //float max_spike_length_for_random = 0.85f;

                            float minX = 1;// ((ChunkWidth_L + ChunkWidth_R + 1) * 0.55f);
                            float maxX = ((ChunkWidth_L) * 0.975f);

                            /*if (posX < 0)
                            {
                                minX = minX * -1;
                                maxX = maxX * -1;
                            }*/

                            float minY = 1;// ((ChunkHeight_L + ChunkHeight_R + 1) * 0.55f);
                            float maxY = ((ChunkHeight_L) * 0.975f);
                            /*if (posY < 0)
                            {
                                minY = minY * -1;
                                maxY = maxY * -1;
                            }*/

                            float minZ = 1;// ((ChunkDepth_L + ChunkDepth_R + 1) * 0.55f);
                            float maxZ = ((ChunkDepth_L) * 0.975f);

                            /*if (posZ < 0)
                            {
                                minZ = minZ * -1;
                                maxZ = maxZ * -1;
                            }*/

                            //int maxDist_width = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * max_spike_length_for_random));
                            int randX = (int)(Math.Round(sc_maths.getSomeRandNumThousandDecimal((int)Math.Round(minX), (int)Math.Round(maxX), _decimal_for_random, 0, 0)));
                            //int maxDist_height = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * max_spike_length_for_random));
                            int randY = (int)(Math.Round(sc_maths.getSomeRandNumThousandDecimal((int)Math.Round(minY), (int)Math.Round(maxY), _decimal_for_random, 0, 0)));
                            //int maxDist_depth = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * max_spike_length_for_random));
                            int randZ = (int)(Math.Round(sc_maths.getSomeRandNumThousandDecimal((int)Math.Round(minZ), (int)Math.Round(maxZ), _decimal_for_random, 0, 0)));

                            float _someMin = 0.35f;
                            if (posX < 0)
                            {
                                randX *= -1;
                                if ((randX) >= -(ChunkWidth_L + ChunkWidth_R + 1) * _someMin)
                                {
                                    randX = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * _someMin)) * -1;
                                }
                            }
                            else
                            {
                                if ((randX) < (ChunkWidth_L + ChunkWidth_R + 1) * _someMin)
                                {
                                    randX = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * _someMin));
                                }
                            }

                            if (posY < 0)
                            {
                                randY *= -1;
                                if ((randY) >= -(ChunkHeight_L + ChunkHeight_R + 1) * _someMin)
                                {
                                    randY = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * _someMin)) * -1;
                                }
                            }
                            else
                            {
                                if ((randY) < (ChunkHeight_L + ChunkHeight_R + 1) * _someMin)
                                {
                                    randY = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * _someMin));
                                }
                            }


                            if (posZ < 0)
                            {
                                randZ *= -1;
                                if ((randZ) >= -(ChunkDepth_L + ChunkDepth_R + 1) * _someMin)
                                {
                                    randZ = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * _someMin)) * -1;
                                }
                            }
                            else
                            {
                                if ((randZ) < (ChunkDepth_L + ChunkDepth_R + 1) * _someMin)
                                {
                                    randZ = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * _someMin));
                                }
                            }

                            /*if(randX < 0 || randY < 0|| randZ < 0)
                            {
                                Console.WriteLine(randX + " _ " + randY + "  " + randZ);
                            }*/




                            if (voxel_type == 0)
                            {

                            }
                            else if (voxel_type == 1)
                            {

                                if (posY < 0 && posX < 0 && posZ < 0)
                                {
                                    if (_swtch_spike_00 == 0)
                                    {
                                        CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                        _swtch_spike_00 = 1;
                                    }
                                }
                                else if (posY >= 0 && posX >= 0 && posZ >= 0)
                                {
                                    if (_swtch_spike_01 == 0)
                                    {
                                        CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                        _swtch_spike_01 = 1;
                                    }
                                }
                                else if (posY >= 0 && posX < 0 && posZ >= 0)
                                {
                                    if (_swtch_spike_02 == 0)
                                    {
                                        CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                        _swtch_spike_02 = 1;
                                    }
                                }
                                else if (posY >= 0 && posX >= 0 && posZ < 0)
                                {
                                    if (_swtch_spike_03 == 0)
                                    {
                                        CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                        _swtch_spike_03 = 1;
                                    }
                                }
                                else if (posY >= 0 && posX < 0 && posZ < 0)
                                {
                                    if (_swtch_spike_04 == 0)
                                    {
                                        CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                        _swtch_spike_04 = 1;
                                    }
                                }
                                else if (posY < 0 && posX >= 0 && posZ < 0)
                                {
                                    if (_swtch_spike_05 == 0)
                                    {
                                        CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                        _swtch_spike_05 = 1;
                                    }

                                }
                                else if (posY < 0 && posX >= 0 && posZ >= 0)
                                {
                                    if (_swtch_spike_06 == 0)
                                    {
                                        CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                        _swtch_spike_06 = 1;
                                    }
                                }
                                else if (posY < 0 && posX < 0 && posZ >= 0)
                                {
                                    if (_swtch_spike_07 == 0)
                                    {
                                        CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                        _swtch_spike_07 = 1;
                                    }
                                }
                            }
                            else if (voxel_type == 2)
                            {

                            }
                        }
                        else if (voxel_type == 3) //distance >= ((ChunkWidth_L) * 0.35f) && distance < ((ChunkWidth_L) * 0.975f)  && 
                        {
                            float _decimal_for_random = 1.0f;
                            //float max_spike_length_for_random = 0.85f;

                            float minX = 1;// ((ChunkWidth_L + ChunkWidth_R + 1) * 0.55f);
                            float maxX = ((ChunkWidth_L) * 0.975f);

                            /*if (posX < 0)
                            {
                                minX = minX * -1;
                                maxX = maxX * -1;
                            }*/

                            float minY = 1;// ((ChunkHeight_L + ChunkHeight_R + 1) * 0.55f);
                            float maxY = ((ChunkHeight_L) * 0.975f);
                            /*if (posY < 0)
                            {
                                minY = minY * -1;
                                maxY = maxY * -1;
                            }*/

                            float minZ = 1;// ((ChunkDepth_L + ChunkDepth_R + 1) * 0.55f);
                            float maxZ = ((ChunkDepth_L) * 0.975f);

                            /*if (posZ < 0)
                            {
                                minZ = minZ * -1;
                                maxZ = maxZ * -1;
                            }*/

                            //int maxDist_width = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * max_spike_length_for_random));
                            int randX = 0;// (int)(Math.Round(sc_maths.getSomeRandNumThousandDecimal((int)Math.Round(minX), (int)Math.Round(maxX), _decimal_for_random, 0)));
                            //int maxDist_height = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * max_spike_length_for_random));
                            int randY = 0;// (int)(Math.Round(sc_maths.getSomeRandNumThousandDecimal((int)Math.Round(minY), (int)Math.Round(maxY), _decimal_for_random, 0)));
                            //int maxDist_depth = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * max_spike_length_for_random));
                            int randZ = 0;// (int)(Math.Round(sc_maths.getSomeRandNumThousandDecimal((int)Math.Round(minZ), (int)Math.Round(maxZ), _decimal_for_random, 0)));

                            float _someMin = 0.35f;
                            /*if (posX < 0)
                            {
                                randX *= -1;
                                if ((randX) >= -(ChunkWidth_L + ChunkWidth_R + 1) * _someMin)
                                {
                                    randX = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * _someMin)) * -1;
                                }
                            }
                            else
                            {
                                if ((randX) < (ChunkWidth_L + ChunkWidth_R + 1) * _someMin)
                                {
                                    randX = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * _someMin));
                                }
                            }

                            if (posY < 0)
                            {
                                randY *= -1;
                                if ((randY) >= -(ChunkHeight_L + ChunkHeight_R + 1) * _someMin)
                                {
                                    randY = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * _someMin)) * -1;
                                }
                            }
                            else
                            {
                                if ((randY) < (ChunkHeight_L + ChunkHeight_R + 1) * _someMin)
                                {
                                    randY = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * _someMin));
                                }
                            }


                            if (posZ < 0)
                            {
                                randZ *= -1;
                                if ((randZ) >= -(ChunkDepth_L + ChunkDepth_R + 1) * _someMin)
                                {
                                    randZ = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * _someMin)) * -1;
                                }
                            }
                            else
                            {
                                if ((randZ) < (ChunkDepth_L + ChunkDepth_R + 1) * _someMin)
                                {
                                    randZ = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * _someMin));
                                }
                            }*/

                            /*if(randX < 0 || randY < 0|| randZ < 0)
                            {
                                Console.WriteLine(randX + " _ " + randY + "  " + randZ);
                            }*/


                            if (posY < 0 && posX < 0 && posZ < 0)
                            {
                                if (_swtch_spike_00 == 0)
                                {
                                    CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                    _swtch_spike_00 = 1;
                                }
                            }
                            /*else if (posY >= 0 && posX >= 0 && posZ >= 0)
                            {
                                if (_swtch_spike_01 == 0)
                                {
                                    CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                    _swtch_spike_01 = 1;
                                }
                            }
                            else if (posY >= 0 && posX < 0 && posZ >= 0)
                            {
                                if (_swtch_spike_02 == 0)
                                {
                                    CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                    _swtch_spike_02 = 1;
                                }
                            }
                            else if (posY >= 0 && posX >= 0 && posZ < 0)
                            {
                                if (_swtch_spike_03 == 0)
                                {
                                    CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                    _swtch_spike_03 = 1;
                                }
                            }
                            else if (posY >= 0 && posX < 0 && posZ < 0)
                            {
                                if (_swtch_spike_04 == 0)
                                {
                                    CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                    _swtch_spike_04 = 1;
                                }
                            }
                            else if (posY < 0 && posX >= 0 && posZ < 0)
                            {
                                if (_swtch_spike_05 == 0)
                                {
                                    CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                    _swtch_spike_05 = 1;
                                }

                            }
                            else if (posY < 0 && posX >= 0 && posZ >= 0)
                            {
                                if (_swtch_spike_06 == 0)
                                {
                                    CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                    _swtch_spike_06 = 1;
                                }
                            }
                            else if (posY < 0 && posX < 0 && posZ >= 0)
                            {
                                if (_swtch_spike_07 == 0)
                                {
                                    CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                    _swtch_spike_07 = 1;
                                }
                            }*/
                        }

                        /*if (_current_counter_for_adding_spike_glyco_protein_covid19 >= (_max) * 0.05f)
                        {
                            float _decimal = 1.0f;
                            float max_spike_length = 0.85f;

                            int maxDist_width = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * max_spike_length));
                            int randX = (int)(Math.Round(getSomeRandNumThousandDecimal(maxDist_width, _decimal)));

                            int maxDist_height = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * max_spike_length));
                            int randY = (int)(Math.Round(getSomeRandNumThousandDecimal(maxDist_height, _decimal)));

                            int maxDist_depth = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * max_spike_length));
                            int randZ = (int)(Math.Round(getSomeRandNumThousandDecimal(maxDist_depth, _decimal)));

                            CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                            _current_counter_for_adding_spike_glyco_protein_covid19 = 0;
                        }*/

















                        /*randomX = 1;
                        randomY = 1;
                        randomZ = 1;

                        int isX = (int)Math.Floor(rand.NextDouble() * (2 - 0) + 0);
                        //rand = new Random();
                        int isY = (int)Math.Floor(rand.NextDouble() * (2 - 0) + 0);
                        //rand = new Random();
                        int isZ = (int)Math.Floor(rand.NextDouble() * (2 - 0) + 0);
                        //rand = new Random();
                        //decide if possible or negative

                        if (isX == 0)
                        {
                            isX = 1;
                            randomX *= isX;
                        }
                        else //if (isX == 1)
                        {
                            isX = -1;
                            randomX *= isX;
                        }

                        if (isY == 0)
                        {
                            isY = 1;
                            randomY *= isY;
                        }
                        else //if (isY == 1)
                        {
                            isY = -1;
                            randomY *= isY;
                        }

                        if (isZ == 0)
                        {
                            isZ = 1;
                            randomZ *= isZ;
                        }
                        else //if (isZ == 1)
                        {
                            isZ = -1;
                            randomZ *= isZ;
                        }*/










                        /*var data_one_x = (int)(Math.Round(currentPosition.X - ((ChunkWidth_L + ChunkWidth_R + 1) * 0.35f)));
                        var data_two_x = (int)(Math.Round(currentPosition.X + ((ChunkWidth_L + ChunkWidth_R + 1) * 0.35f)));
                        //var randX = FastRand(seed, data_two_x);
                        var randX = rand.Next(data_one_x, data_two_x);

                        var data_one_y = (int)(Math.Round(currentPosition.Y - ((ChunkHeight_L + ChunkHeight_R + 1) * 0.35f)));
                        var data_two_y = (int)(Math.Round(currentPosition.Y + ((ChunkHeight_L + ChunkHeight_R + 1) * 0.35f)));
                        //var randY = FastRand(seed, data_two_y);
                        var randY = rand.Next(data_one_y, data_two_y);

                        var data_one_z = (int)(Math.Round(currentPosition.Z - ((ChunkDepth_L + ChunkDepth_R + 1) * 0.35f)));
                        var data_two_z = (int)(Math.Round(currentPosition.Z + ((ChunkDepth_L + ChunkDepth_R + 1) * 0.35f)));
                        //var randZ = FastRand(seed, data_two_z);
                        var randZ = rand.Next(data_one_z, data_two_z);
                        */

                        /*if (randX < 0)
                        {
                            randX *= -1;
                            randX = (ChunkWidth_R) + randX;
                        }
                        if (randY < 0)
                        {
                            randY *= -1;
                            randY = (ChunkHeight_R) + randY;
                        }
                        if (randZ < 0)
                        {
                            randZ *= -1;
                            randZ = (ChunkDepth_R) + randZ;
                        }

                        _index = randX + (ChunkWidth_L + ChunkWidth_R + 1) * (randY + (ChunkHeight_L + ChunkHeight_R + 1) * randZ);
                        if (_index >= 0 && _index < _max)
                        {
                            map[_index] = 1;
                        }*/

                        /*if (_current_counter_for_adding_spike_glyco_protein_covid19 >= (_max) * 0.05f)
                        {
                            _index = randX + (ChunkWidth_L + ChunkWidth_R + 1) * (randY + (ChunkHeight_L + ChunkHeight_R + 1) * randZ);

                            if (_index >= 0 && _index < _max)
                            {
                                CreateSpikeGlycoProteinCOVID19(currentPosition, x, y, z);
                            }

                            //SC_pathfind_assets.sc_pathfind_grid grid = new SC_pathfind_assets.sc_pathfind_grid();
                            //grid.sc_init_pathfind_grid(ChunkWidth_L, ChunkWidth_R, ChunkHeight_L, ChunkHeight_R, ChunkDepth_L, ChunkDepth_R); // working but i didnt convert the rest yet.
                            //i just got to figure out how i planified the arrays in javascript. i dont know why i am so fucking stressed as this is so fucking easy to do.
                            /*if (_index >= 0 && _index < _max)
                            {
                                //MessageBox((IntPtr)0,  "test", "Oculus error", 0);
                                CreateSpikeGlycoProteinCOVID19(currentPosition, x, y, z);
                            }
                            _current_counter_for_adding_spike_glyco_protein_covid19 = 0;
                        }
                        }*/





                        //int n = number.Next(0, 1000);


                        /*if (_current_counter_for_adding_spike_glyco_protein_covid19 >= (_max) * 0.05f)
                        {
                            _index = randX + (ChunkWidth_L + ChunkWidth_R + 1) * (randY + (ChunkHeight_L + ChunkHeight_R + 1) * randZ);

                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                                //MessageBox((IntPtr)0,  "test", "Oculus error", 0);
                                //CreateSpikeGlycoProteinCOVID19(currentPosition, x, y, z);
                            }

                            //SC_pathfind_assets.sc_pathfind_grid grid = new SC_pathfind_assets.sc_pathfind_grid();
                            //grid.sc_init_pathfind_grid(ChunkWidth_L, ChunkWidth_R, ChunkHeight_L, ChunkHeight_R, ChunkDepth_L, ChunkDepth_R); // working but i didnt convert the rest yet.
                            //i just got to figure out how i planified the arrays in javascript. i dont know why i am so fucking stressed as this is so fucking easy to do.
                            /*if (_index >= 0 && _index < _max)
                            {
                                //MessageBox((IntPtr)0,  "test", "Oculus error", 0);
                                CreateSpikeGlycoProteinCOVID19(currentPosition, x, y, z);
                            }
                            _current_counter_for_adding_spike_glyco_protein_covid19 = 0;
                        }*/


                        //_index = randX + (ChunkWidth_L + ChunkWidth_R + 1) * (randY + (ChunkHeight_L + ChunkHeight_R + 1) * randZ);






                        /*if (posY < 0 && posX < 0 && posZ < 0)
                        {
                            if (_swtch_spike_00 == 0)
                            {
                                CreateSpikeGlycoProteinCOVID19(center, randX, randY, randZ);
                                //CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ));
                                _swtch_spike_00 = 1;
                            }
                        }
                        else if (posY >= 0 && posX >= 0 && posZ >= 0)
                        {
                            if (_swtch_spike_01 == 0)
                            {
                                CreateSpikeGlycoProteinCOVID19(center, randX, randY, randZ);
                                //CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ));
                                _swtch_spike_01 = 1;
                            }
                        }
                        else if (posY >= 0 && posX < 0 && posZ >= 0)
                        {
                            if (_swtch_spike_02 == 0)
                            {
                                CreateSpikeGlycoProteinCOVID19(center, randX, randY, randZ);
                                //CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ));
                                _swtch_spike_02 = 1;
                            }
                        }
                        else if (posY >= 0 && posX >= 0 && posZ < 0)
                        {
                            if (_swtch_spike_03 == 0)
                            {
                                CreateSpikeGlycoProteinCOVID19(center, randX, randY, randZ);
                                //CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ));
                                _swtch_spike_03 = 1;
                            }
                        }
                        else if (posY >= 0 && posX < 0 && posZ < 0)
                        {
                            if (_swtch_spike_04 == 0)
                            {
                                CreateSpikeGlycoProteinCOVID19(center, randX, randY, randZ);
                                //CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ));
                                _swtch_spike_04 = 1;
                            }
                        }
                        else if (posY < 0 && posX >= 0 && posZ < 0)
                        {
                            if (_swtch_spike_05 == 0)
                            {
                                CreateSpikeGlycoProteinCOVID19(center, randX, randY, randZ);
                                //CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ));
                                _swtch_spike_05 = 1;
                            }

                        }
                        else if (posY < 0 && posX >= 0 && posZ >= 0)
                        {
                            if (_swtch_spike_06 == 0)
                            {
                                CreateSpikeGlycoProteinCOVID19(center, randX, randY, randZ);
                                //CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ));
                                _swtch_spike_06 = 1;
                            }
                        }
                        else if (posY < 0 && posX < 0 && posZ >= 0)
                        {
                            if (_swtch_spike_07 == 0)
                            {
                                CreateSpikeGlycoProteinCOVID19(center, randX, randY, randZ);
                                //CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ));
                                _swtch_spike_07 = 1;
                            }
                        }*/

                        /*else if (distance >= (ChunkWidth * 0.40f) && distance < (ChunkWidth * 0.425f))
                        {
                            //map[_index] = 1; // to see external sheet/membrane
                            //map[_index] = 0;
                        }
                        else if (distance >= (ChunkWidth * 0.425f) && distance < (ChunkWidth))
                        {
                            //map[_index] = 0;
                        }
                        else
                        { 
                           //map[_index] = 0;
                        }*/

                        _current_counter_for_adding_spike_glyco_protein_covid19++;
                    }
                }
            }




            /*
            for (int i = 0;i < 50;i++)
            {
                randomX = 1;
                randomY = 1;
                randomZ = 1;

                int isX = (int)Math.Floor(rand.NextDouble() * (2 - 0) + 0);
                int isY = (int)Math.Floor(rand.NextDouble() * (2 - 0) + 0);
                int isZ = (int)Math.Floor(rand.NextDouble() * (2 - 0) + 0);

                //decide if possible or negative
                if (isX == 0)
                {

                }
                else if (isX == 1)
                {
                    isX = -1;
                    randomX *= isX;
                }

                if (isY == 0)
                {

                }
                else if (isY == 1)
                {
                    isY = -1;
                    randomY *= isY;
                }

                if (isZ == 0)
                {

                }
                else if (isZ == 1)
                {
                    isZ = -1;
                    randomZ *= isZ;
                }


                //randomX = randomX * (int)(Math.Floor(rand.NextDouble() * (_current_visual_distance_spike_glyco_protein_covid19_max - 1) + _current_visual_distance_spike_glyco_protein_covid19_min + isX));
                //randomY = randomY * (int)(Math.Floor(rand.NextDouble() * (_current_visual_distance_spike_glyco_protein_covid19_max - 1) + _current_visual_distance_spike_glyco_protein_covid19_min + isY));
                //randomZ = randomZ * (int)(Math.Floor(rand.NextDouble() * (_current_visual_distance_spike_glyco_protein_covid19_max - 1) + _current_visual_distance_spike_glyco_protein_covid19_min + isZ));

                randomX = randomX * (_current_visual_distance_spike_glyco_protein_covid19_min);
                randomY = randomZ * (_current_visual_distance_spike_glyco_protein_covid19_min);
                randomZ = randomY * (_current_visual_distance_spike_glyco_protein_covid19_min);


                float posX = (randomX) + currentPosition.X;
                float posY = (randomY) + currentPosition.Y;
                float posZ = (randomZ) + currentPosition.Z;

                Vector3 position = new Vector3(posX, posY, posZ);

                float distance = Vector3.Distance(position, center);

                if (distance >= (ChunkWidth * 0.25f) && distance < (ChunkWidth))
                {
                    //if (_swtch == 0)
                    {
                        if (_current_counter_for_adding_spike_glyco_protein_covid19 >= 0)
                        {
                            CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ), _current_visual_distance_spike_glyco_protein_covid19_min, _current_visual_distance_spike_glyco_protein_covid19_max);
                            _current_counter_for_adding_spike_glyco_protein_covid19 = 0;
                            //_swtch = 1;
                        }
                    }
                }
                else
                {
                    //map[_index] = 0;
                    //_swtch = 0;
                }
            }*/
























            /*

            float max_spikes_percent = 0.1f;

            for (int i = 0;i < 1; i++) //(int)Math.Round(_max * max_spikes_percent)
            {
                int _loop_counter = 0;
            _loop:

                if (_loop_counter < 10)
                {
                    randomX = 1;
                    randomY = 1;
                    randomZ = 1;

                    int isX = (int)Math.Floor(rand.NextDouble() * (2 - 0) + 0);
                    int isY = (int)Math.Floor(rand.NextDouble() * (2 - 0) + 0);
                    int isZ = (int)Math.Floor(rand.NextDouble() * (2 - 0) + 0);

                    //decide if possible or negative
                    if (isX == 0)
                    {

                    }
                    else if (isX == 1)
                    {
                        isX = -1;
                        randomX *= isX;
                    }

                    if (isY == 0)
                    {

                    }
                    else if (isY == 1)
                    {
                        isY = -1;
                        randomY *= isY;
                    }

                    if (isZ == 0)
                    {

                    }
                    else if (isZ == 1)
                    {
                        isZ = -1;
                        randomZ *= isZ;
                    }
      

                    randomX = randomX * (int)(Math.Floor(rand.NextDouble() * (_current_visual_distance_spike_glyco_protein_covid19_max - 1) + _current_visual_distance_spike_glyco_protein_covid19_min + isX));
                    randomY = randomY * (int)(Math.Floor(rand.NextDouble() * (_current_visual_distance_spike_glyco_protein_covid19_max - 1) + _current_visual_distance_spike_glyco_protein_covid19_min + isY));
                    randomZ = randomZ * (int)(Math.Floor(rand.NextDouble() * (_current_visual_distance_spike_glyco_protein_covid19_max - 1) + _current_visual_distance_spike_glyco_protein_covid19_min + isZ));

                    float posX = (randomX * planeSize) + currentPosition.X;
                    float posY = (randomY * planeSize) + currentPosition.Y;
                    float posZ = (randomZ * planeSize) + currentPosition.Z;

                    Vector3 position = new Vector3(posX, posY, posZ);

                    float distance = Vector3.Distance(position, center);

                    if (distance > (ChunkWidth * 0.35f) * planeSize && distance < (ChunkWidth) * planeSize)
                    {
                        //if (_swtch == 0)
                        {
                            if (_current_counter_for_adding_spike_glyco_protein_covid19 >= 0) // maxframe is ChunkWidth *ChunkHeight * ChunkDepth
                            {
                                CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ), _current_visual_distance_spike_glyco_protein_covid19_min, _current_visual_distance_spike_glyco_protein_covid19_max);
                                _current_counter_for_adding_spike_glyco_protein_covid19 = 0;
                                //_swtch = 1;
                            }
                        }
                    }
                    else
                    {
                        //map[_index] = 0;
                        //_swtch = 0;
                        _loop_counter++;
                        goto _loop;
                    }
                }
            }*/








            /*
            //create protruding spikes
            for (int x = 0; x < ChunkWidth; x++)
            {
                float noiseX = Math.Abs(((float)(x * planeSize + currentPosition.X + seed) / _detailScale) * _heightScale);

                for (int y = 0; y < ChunkHeight; y++)
                {
                    float noiseY = Math.Abs(((float)(y * planeSize + currentPosition.Y + seed) / _detailScale) * _heightScale);

                    for (int z = 0; z < ChunkDepth; z++)
                    {
                        float noiseZ = Math.Abs(((float)(z * planeSize + currentPosition.Z + seed) / _detailScale) * _heightScale);

                        float posX = (x * planeSize) + currentPosition.X;
                        float posY = (y * planeSize) + currentPosition.Y;
                        float posZ = (z * planeSize) + currentPosition.Z;

                        Vector3 position = new Vector3(posX, posY, posZ);

                        float distance = Vector3.Distance(position, center);

                        //Vector3 position1 = currentPosition;
                        //float distance1 = Vector3.Distance(position1, center);

                        posX -= center.X;
                        posY -= center.Y;
                        posZ -= center.Z;

                        _current_visual_distance_spike_glyco_protein_covid19_min = (ChunkWidth * 0.35f) * planeSize;
                        _current_visual_distance_spike_glyco_protein_covid19_max = (ChunkWidth - 1) * planeSize; // increase size of chunk for longer glycoproteinthin

                        int _index = x + ChunkWidth * (y + ChunkHeight * z);

                        if (distance > (ChunkWidth * 0.35f) * planeSize && distance < (ChunkWidth) * planeSize)
                        {
                            if (_swtch == 0)
                            {
                                if (_current_counter_for_adding_spike_glyco_protein_covid19 >= 0) // maxframe is ChunkWidth *ChunkHeight * ChunkDepth
                                {
                                    CreateSpikeGlycoProteinCOVID19(center, x, y, z, _current_visual_distance_spike_glyco_protein_covid19_min, _current_visual_distance_spike_glyco_protein_covid19_max);

                                    _current_counter_for_adding_spike_glyco_protein_covid19 = 0;
                                    _swtch = 1;
                                }
                            }
                        }
                        else
                        {
                            //map[_index] = 0;
                        }
                        _current_counter_for_adding_spike_glyco_protein_covid19++;
                    }
                }
            }*/

            /*Regenerate(currentPosition);

            vertexArray = listOfVerts.ToArray();
            triangleArray = listOfTriangleIndices.ToArray();
            mapper = map;*/



            sccsSetMap();
            Regenerate(); //currentPosition

            vertexArray = vertexlist.ToArray();
            triangleArray = listOfTriangleIndices.ToArray();
            mapper = map;
        }








        public void sccsSetMap()
        {
            /*_tempChunkArrayBottomFace = new int[width * height * depth];
            _tempChunkArrayBackFace = new int[width * height * depth];
            _tempChunkArrayFrontFace = new int[width * height * depth];
            _tempChunkArrayLeftFace = new int[width * height * depth];
            _tempChunkArrayRightFace = new int[width * height * depth];
            _tempChunkArray = new int[width * height * depth];

            _chunkArray = new int[width * height * depth];

            _chunkVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

            vertexlist = new List<Vector3>();
            triangles = new List<int>();*/

            vertexlist.Clear();
            listOfTriangleIndices.Clear();

            for (int t = 0; t < vertexlistWidth * vertexlistHeight * vertexlistDepth; t++) //total
            {
                if (t < total)
                {
                    if (map[t] == 1)
                    {
                        _chunkArray[t] = 1;

                        _tempChunkArray[t] = 1;
                        _tempChunkArrayRightFace[t] = 1;
                        _tempChunkArrayLeftFace[t] = 1;

                        _tempChunkArrayBottomFace[t] = 1;
                        _tempChunkArrayBackFace[t] = 1;
                        _tempChunkArrayFrontFace[t] = 1;
                    }
                    else
                    {
                        _chunkArray[t] = 0;

                        _tempChunkArray[t] = 0;
                        _tempChunkArrayRightFace[t] = 0;
                        _tempChunkArrayLeftFace[t] = 0;

                        _tempChunkArrayBottomFace[t] = 0;
                        _tempChunkArrayBackFace[t] = 0;
                        _tempChunkArrayFrontFace[t] = 0;

                    }
                }

                if (t < vertexlistWidth * vertexlistHeight * vertexlistDepth)
                {
                    _chunkVertexArray[t] = 0;
                    _testVertexArray[t] = 0;
                }
            }
        }




        public void Regenerate()
        {
            vertexlist.Clear();
            listOfTriangleIndices.Clear();

            //normalslist.Clear();
            //colorslist.Clear();
            //indexposlist.Clear();
            //textureslist.Clear();




            /*
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        var xx = x;
                        var yy = y;
                        var zz = z;

                        index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);

                        if (index < total)
                        {

                            if (_block == 1)
                            {

                                if (IsTransparent(xx, yy + 1, zz))
                                {
                                    buildTopFace(xx,yy,zz);
                                }

                                //WORKING
                                //LEFT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                               /* if (IsTransparent(xx, yy - 1, zz))
                                {
                                    buildBottomFace();
                                }
                                if (IsTransparent(xx - 1, yy, zz))
                                {

                                    buildTopLeft();
                                }

                                if (IsTransparent(xx + 1, yy, zz))
                                {
                                    buildTopRight();
                                }

                                if (IsTransparent(xx, yy, zz + 1))
                                {
                                    buildFrontFace();
                                }

                                //BACK FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                                if (IsTransparent(xx, yy, zz - 1))
                                {
                                    buildBackFace();
                                }
                            }
                        }
                    }
                }
            */











            /*
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

                        //xi = xx;
                        //yi = yy;
                        //zi = zz;


                        index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);

                        if (index < total)
                        {
                            _block = _chunkArray[index]; //map[x, y, z];_tempChunkArrayRightFace[index];

                            if (_block == 1)
                            {

                                if (IsTransparent(xx, yy + 1, zz))
                                {
                                    buildTopFace(xx,yy,zz);
                                }

                                //WORKING
                                //LEFT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                                if (IsTransparent(xx, yy - 1, zz))
                                {
                                    buildBottomFace(xx, yy, zz);
                                }
                                if (IsTransparent(xx - 1, yy, zz))
                                {

                                    buildTopLeft(xx, yy, zz);
                                }

                                if (IsTransparent(xx + 1, yy, zz))
                                {
                                    buildTopRight(xx, yy, zz);
                                }

                                if (IsTransparent(xx, yy, zz + 1))
                                {
                                    buildFrontFace(xx, yy, zz);
                                }

                                //BACK FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                                if (IsTransparent(xx, yy, zz - 1))
                                {
                                    buildBackFace(xx, yy, zz);
                                }
                            }
                        }
                    }
                }
            }*/



            for (int t = 0; t < total; t++) //total
            {
                if (swtchz == 0)
                {
                    CreateChunkFaces();
                    //CalculateBytesForFaces();
                }
                else
                {
                    break;
                }
            }





            /*for (int x = -ChunkWidth_L; x <= ChunkWidth_R; x++)
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

                        //xi = xx;
                        //yi = yy;
                        //zi = zz;


                        index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);

                        if (index < total)
                        {
                            _block = _chunkArray[index]; //map[x, y, z];_tempChunkArrayRightFace[index];

                            if (_block == 1)
                            {

                                CreateChunkFaces();
                                
                                //CalculateBytesForFaces();

                            }
                        }
                    }
                }
            }*/


            /*
            for (int x = 0; x < SC_Globals.tinyChunkWidth; x++)
            {
                for (int y = 0; y < SC_Globals.tinyChunkHeight; y++)
                {
                    for (int z = 0; z < SC_Globals.tinyChunkDepth; z++)
                    {
                        if (swtchz == 0)
                        {
                            CreateChunkFaces();
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }*/
        }





        public void CreateChunkFaces()
        {
            if (swtchz == 0)
            {
                if (t < total)
                {
                    posx = (xx);
                    posy = (yy);
                    posz = (zz);

                    xi = xx;
                    yi = yy;
                    zi = zz;

                    if (xi < 0)
                    {
                        xi *= -1;
                        xi = (width) + xi;
                    }
                    if (yi < 0)
                    {
                        yi *= -1;
                        yi = (height) + yi;
                    }
                    if (zi < 0)
                    {
                        zi *= -1;
                        zi = (depth) + zi;
                    }

                    //zi = (depth - 1) - zi;

                    index = xi + (width) * (yi + (height) * zi);

                    if (index < total)
                    {
                        _block = _chunkArray[index]; //map[x, y, z];_tempChunkArrayRightFace[index];

                        if (_block == 1)
                        {
                            counterCreateChunkObjectFacesBytes = 0;

                            tBytes = 0;

                            posxBytes = 0;
                            posyBytes = 0;
                            poszBytes = 0;

                            xxBytes = 0;
                            yyBytes = 0;
                            zzBytes = 0;

                            xiBytes = 0;
                            yiBytes = 0;
                            ziBytes = 0;

                            swtchxBytes = 0;
                            swtchyBytes = 0;
                            swtchzBytes = 0;

                            rowIterateXBytes = 0;
                            rowIterateYBytes = 0;
                            rowIterateZBytes = 0;

                            _maxWdth = width;
                            _maxHght = height;
                            _maxDepth = depth;

                            foundVertOne = false;
                            foundVertTwo = false;
                            foundVertThree = false;
                            foundVertFour = false;

                            if (swtchzBytes == 0)
                            {
                                CalculateBytesForFaces();
                            }

                            /*for (int i = 0; i < totalBytes; i++)
                            {
                                if (swtchzBytes == 0)
                                {
                                    CalculateBytesForFaces();
                                }
                                else
                                {
                                    i = totalBytes;
                                    //break;
                                }
                            }*/
                        }
                    }
                    else
                    {
                        //t = total;
                    }

                    zz++;
                    if (zz >= (depth))
                    {
                        xx++;
                        zz = 0;
                        swtchx = 1;
                    }
                    if (xx >= (width) && swtchx == 1)
                    {
                        //faceStart = 0;
                        yy++;
                        xx = 0;
                        swtchx = 0;
                        swtchy = 1;
                    }
                    if (yy >= (height) && swtchy == 1)
                    {
                        //yy = -ChunkHeight_L;
                        swtchy = 0;
                        swtchx = 0;
                        swtchz = 1;
                    }
                    t++;
                    counterCreateChunkObjectFaces++;
                }

                //Debug.Log("total:" + total + "/t:" + t);
            }


            if (swtchz == 1)
            {


                _index0 = 0;
                _index1 = 0;
                _index2 = 0;
                _index3 = 0;

                _newVertzCounter = 0;

                oneVertIndexX = 0;
                oneVertIndexY = 0;
                oneVertIndexZ = 0;

                twoVertIndexX = 0;
                twoVertIndexY = 0;
                twoVertIndexZ = 0;

                threeVertIndexX = 0;
                threeVertIndexY = 0;
                threeVertIndexZ = 0;

                fourVertIndexX = 0;
                fourVertIndexY = 0;
                fourVertIndexZ = 0;

                _maxWdth = 0;
                _maxHght = 0;
                _maxDepth = 0;

                rowIterateX = 0;
                rowIterateZ = 0;

                foundVertOne = false;
                foundVertTwo = false;
                foundVertThree = false;
                foundVertFour = false;



                t = 0;

                xx = 0;
                yy = 0;
                zz = 0;

                swtchx = 0;
                swtchy = 0;
                swtchz = 0;

                counterCreateChunkObjectFaces = 0;

                tBytes = 0;

                posxBytes = 0;
                posyBytes = 0;
                poszBytes = 0;

                xxBytes = 0;
                yyBytes = 0;
                zzBytes = 0;

                xiBytes = 0;
                yiBytes = 0;
                ziBytes = 0;

                swtchxBytes = 0;
                swtchyBytes = 0;
                swtchzBytes = 0;
                counterCreateChunkObjectFacesBytes = 0;



                for (int t = 0; t < vertexlistWidth * vertexlistHeight * vertexlistDepth; t++) //total
                {
                    if (t < total)
                    {
                        if (map[t] == 1)
                        {
                            _chunkArray[t] = 1;

                            _tempChunkArray[t] = 1;
                            _tempChunkArrayRightFace[t] = 1;
                            _tempChunkArrayLeftFace[t] = 1;

                            _tempChunkArrayBottomFace[t] = 1;
                            _tempChunkArrayBackFace[t] = 1;
                            _tempChunkArrayFrontFace[t] = 1;
                        }
                        else
                        {
                            _chunkArray[t] = 0;

                            _tempChunkArray[t] = 0;
                            _tempChunkArrayRightFace[t] = 0;
                            _tempChunkArrayLeftFace[t] = 0;

                            _tempChunkArrayBottomFace[t] = 0;
                            _tempChunkArrayBackFace[t] = 0;
                            _tempChunkArrayFrontFace[t] = 0;

                        }
                    }

                    if (t < vertexlistWidth * vertexlistHeight * vertexlistDepth)
                    {
                        _chunkVertexArray[t] = 0;
                        _testVertexArray[t] = 0;
                    }
                }
            }
        }
























        void CalculateBytesForFaces()
        {
            if (tBytes < totalBytes)
            {
                posxBytes = (xxBytes);
                posyBytes = (yyBytes);
                poszBytes = (zzBytes);

                Vector3 somepos = new Vector3(posxBytes, posyBytes, poszBytes);

                xiBytes = xxBytes;
                yiBytes = yyBytes;
                ziBytes = zzBytes;

                rowIterateXBytes = xiBytes + xi;
                rowIterateYBytes = yiBytes + yi;
                rowIterateZBytes = ziBytes + zi;

                var indexBytes = rowIterateXBytes + (width) * (rowIterateYBytes + (height) * rowIterateZBytes);

                //Debug.Log("xiBytes:" + xiBytes + "/yiBytes:" + yiBytes + "/ziBytes:" + ziBytes);
                if (indexBytes < totalBytes)
                {


                    //var neighboorindexx = (int)Math.Floor((chunkPos.X / planeSize) / fractionOf); //4.654321/0.2 = 23.271605 => 23.271605/fractionOf = floor(2.3f)
                    //var neighboorindexy = (int)Math.Floor((chunkPos.Y / planeSize) / fractionOf);
                    //var neighboorindexz = (int)Math.Floor((chunkPos.Z / planeSize) / fractionOf);
                    /*
                    var somevalueforTopx = neighboorindexx;
                    var somevalueforTopy = neighboorindexy + realplanetwidth;
                    var somevalueforTopz = neighboorindexz;

                    var somevalueforFrontx = neighboorindexx;
                    var somevalueforFronty = neighboorindexy;
                    var somevalueforFrontz = neighboorindexz + realplanetwidth;

                    var somevalueforBackx = neighboorindexx;
                    var somevalueforBacky = neighboorindexy;
                    var somevalueforBackz = neighboorindexz - realplanetwidth;

                    var somevalueforLeftx = neighboorindexx - realplanetwidth;
                    var somevalueforLefty = neighboorindexy;
                    var somevalueforLeftz = neighboorindexz;

                    var somevalueforRightx = neighboorindexx + realplanetwidth;
                    var somevalueforRighty = neighboorindexy;
                    var somevalueforRightz = neighboorindexz;

                    var somevalueforBottomx = neighboorindexx;
                    var somevalueforBottomy = neighboorindexy - realplanetwidth;
                    var somevalueforBottomz = neighboorindexz;
                    */




                    var fractionOf = realplanetwidth / planeSize;



                    var somevalueforTopx = 0;
                    var somevalueforTopy = 0;
                    var somevalueforTopz = 0;

                    if (chunkPos.X < 0)
                    {
                        somevalueforTopx = (int)Math.Floor((chunkPos.X / planeSize) / fractionOf); //(int)Math.Floor(chunkPos.X / realplanetwidth);
                    }
                    else
                    {
                        somevalueforTopx = (int)Math.Floor((chunkPos.X / planeSize) / fractionOf); //(int)Math.Floor(chunkPos.X / realplanetwidth);
                    }

                    if (chunkPos.Y < 0)
                    {
                        somevalueforTopy = (int)Math.Floor(((chunkPos.Y + (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((chunkPos.Y + (planeSize * width)) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforTopy = (int)Math.Floor(((chunkPos.Y + (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((chunkPos.Y + (planeSize * width)) / realplanetwidth);
                                                                                                                           //posnot0roundedy -= 1;
                    }

                    if (chunkPos.Z < 0)
                    {
                        somevalueforTopz = (int)Math.Floor(((chunkPos.Z) / planeSize) / fractionOf); //(int)Math.Floor(chunkPos.Z / realplanetwidth);
                                                                                                     //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforTopz = (int)Math.Floor(((chunkPos.Z) / planeSize) / fractionOf); //(int)Math.Floor(chunkPos.Z / realplanetwidth);
                    }

                    var somevalueforBottomx = 0;
                    var somevalueforBottomy = 0;
                    var somevalueforBottomz = 0;

                    if (chunkPos.X < 0)
                    {
                        somevalueforBottomx = (int)Math.Floor(((chunkPos.X) / planeSize) / fractionOf); //(int)Math.Floor(chunkPos.X / realplanetwidth);
                    }
                    else
                    {
                        somevalueforBottomx = (int)Math.Floor(((chunkPos.X) / planeSize) / fractionOf); //(int)Math.Floor(chunkPos.X / realplanetwidth);
                    }

                    if (chunkPos.Y < 0)
                    {
                        somevalueforBottomy = (int)Math.Floor(((chunkPos.Y - (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((chunkPos.Y - (planeSize * width)) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforBottomy = (int)Math.Floor(((chunkPos.Y - (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((chunkPos.Y - (planeSize * width)) / realplanetwidth);
                                                                                                                              //posnot0roundedy -= 1;
                    }

                    if (chunkPos.Z < 0)
                    {
                        somevalueforBottomz = (int)Math.Floor(((chunkPos.Z) / planeSize) / fractionOf); //(int)Math.Floor(chunkPos.Z / realplanetwidth);
                                                                                                        //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforBottomz = (int)Math.Floor(((chunkPos.Z) / planeSize) / fractionOf); //(int)Math.Floor(chunkPos.Z / realplanetwidth);
                    }


                    var somevalueforRightx = 0;
                    var somevalueforRighty = 0;
                    var somevalueforRightz = 0;

                    if (chunkPos.X < 0)
                    {
                        somevalueforRightx = (int)Math.Floor(((chunkPos.X + (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((chunkPos.X + (planeSize * width)) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforRightx = (int)Math.Floor(((chunkPos.X + (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((chunkPos.X + (planeSize * width)) / realplanetwidth);
                    }

                    if (chunkPos.Y < 0)
                    {
                        somevalueforRighty = (int)Math.Floor(((chunkPos.Y) / planeSize) / fractionOf); //(int)Math.Floor((chunkPos.Y) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforRighty = (int)Math.Floor(((chunkPos.Y) / planeSize) / fractionOf); //(int)Math.Floor((chunkPos.Y) / realplanetwidth);
                                                                                                       //posnot0roundedy -= 1;
                    }

                    if (chunkPos.Z < 0)
                    {
                        somevalueforRightz = (int)Math.Floor(((chunkPos.Z) / planeSize) / fractionOf); //(int)Math.Floor(chunkPos.Z / realplanetwidth);
                                                                                                       //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforRightz = (int)Math.Floor(((chunkPos.Z) / planeSize) / fractionOf); //(int)Math.Floor(chunkPos.Z / realplanetwidth);
                    }

                    var somevalueforLeftx = 0;
                    var somevalueforLefty = 0;
                    var somevalueforLeftz = 0;

                    if (chunkPos.X < 0)
                    {
                        somevalueforLeftx = (int)Math.Floor(((chunkPos.X - (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((chunkPos.X - (planeSize * width)) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforLeftx = (int)Math.Floor(((chunkPos.X - (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((chunkPos.X - (planeSize * width)) / realplanetwidth);
                    }

                    if (chunkPos.Y < 0)
                    {
                        somevalueforLefty = (int)Math.Floor(((chunkPos.Y) / planeSize) / fractionOf); //(int)Math.Floor((chunkPos.Y) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforLefty = (int)Math.Floor(((chunkPos.Y) / planeSize) / fractionOf); //(int)Math.Floor((chunkPos.Y) / realplanetwidth);
                                                                                                      //posnot0roundedy -= 1;
                    }

                    if (chunkPos.Z < 0)
                    {
                        somevalueforLeftz = (int)Math.Floor(((chunkPos.Z) / planeSize) / fractionOf); //(int)Math.Floor(chunkPos.Z / realplanetwidth);
                                                                                                      //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforLeftz = (int)Math.Floor(((chunkPos.Z) / planeSize) / fractionOf); //(int)Math.Floor(chunkPos.Z / realplanetwidth);
                    }





                    var somevalueforFrontx = 0;
                    var somevalueforFronty = 0;
                    var somevalueforFrontz = 0;

                    if (chunkPos.X < 0)
                    {
                        somevalueforFrontx = (int)Math.Floor(((chunkPos.X) / planeSize) / fractionOf); // (int)Math.Floor((chunkPos.X) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforFrontx = (int)Math.Floor(((chunkPos.X) / planeSize) / fractionOf); //(int)Math.Floor((chunkPos.X) / realplanetwidth);
                    }

                    if (chunkPos.Y < 0)
                    {
                        somevalueforFronty = (int)Math.Floor(((chunkPos.Y) / planeSize) / fractionOf); // (int)Math.Floor((chunkPos.Y) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforFronty = (int)Math.Floor(((chunkPos.Y) / planeSize) / fractionOf); //(int)Math.Floor((chunkPos.Y) / realplanetwidth);
                                                                                                       //posnot0roundedy -= 1;
                    }

                    if (chunkPos.Z < 0)
                    {
                        somevalueforFrontz = (int)Math.Floor(((chunkPos.Z + (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((chunkPos.Z + (planeSize * width)) / realplanetwidth);
                                                                                                                             //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforFrontz = (int)Math.Floor(((chunkPos.Z + (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((chunkPos.Z + (planeSize * width)) / realplanetwidth);
                    }





                    var somevalueforBackx = 0;
                    var somevalueforBacky = 0;
                    var somevalueforBackz = 0;

                    if (chunkPos.X < 0)
                    {
                        somevalueforBackx = (int)Math.Floor(((chunkPos.X) / planeSize) / fractionOf); // (int)Math.Floor((chunkPos.X) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforBackx = (int)Math.Floor(((chunkPos.X) / planeSize) / fractionOf); // (int)Math.Floor((chunkPos.X) / realplanetwidth);
                    }

                    if (chunkPos.Y < 0)
                    {
                        somevalueforBacky = (int)Math.Floor(((chunkPos.Y) / planeSize) / fractionOf); //(int)Math.Floor((chunkPos.Y) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforBacky = (int)Math.Floor(((chunkPos.Y) / planeSize) / fractionOf); // (int)Math.Floor((chunkPos.Y) / realplanetwidth);
                                                                                                      //posnot0roundedy -= 1;
                    }

                    if (chunkPos.Z < 0)
                    {
                        somevalueforBackz = (int)Math.Floor(((chunkPos.Z - (planeSize * width)) / planeSize) / fractionOf); // (int)Math.Floor((chunkPos.Z - (planeSize * width)) / realplanetwidth);
                                                                                                                            //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforBackz = (int)Math.Floor(((chunkPos.Z - (planeSize * width)) / planeSize) / fractionOf); // (int)Math.Floor((chunkPos.Z - (planeSize * width)) / realplanetwidth);
                    }










                    /*
                    if (chunkPos.X < 0)
                    {
                        somevalueforTopx = (int)Math.Floor(chunkPos.X) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforTopx = (int)Math.Floor(chunkPos.X) / realplanetwidth;
                    }

                    if (chunkPos.Y < 0)
                    {
                        somevalueforTopy = (int)Math.Floor((chunkPos.Y + realplanetwidth)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforTopy = (int)Math.Floor((chunkPos.Y + realplanetwidth)) / realplanetwidth;
                        //posnot0roundedy -= 1;
                    }

                    if (chunkPos.Z < 0)
                    {
                        somevalueforTopz = (int)Math.Floor(chunkPos.Z) / realplanetwidth;
                        //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforTopz = (int)Math.Floor(chunkPos.Z) / realplanetwidth;
                    }

                    var somevalueforBottomx = 0;
                    var somevalueforBottomy = 0;
                    var somevalueforBottomz = 0;

                    if (chunkPos.X < 0)
                    {
                        somevalueforBottomx = (int)Math.Floor(chunkPos.X) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforBottomx = (int)Math.Floor(chunkPos.X) / realplanetwidth;
                    }

                    if (chunkPos.Y < 0)
                    {
                        somevalueforBottomy = (int)Math.Floor((chunkPos.Y - realplanetwidth)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforBottomy = (int)Math.Floor((chunkPos.Y - realplanetwidth)) / realplanetwidth;
                        //posnot0roundedy -= 1;
                    }

                    if (chunkPos.Z < 0)
                    {
                        somevalueforBottomz = (int)Math.Floor(chunkPos.Z) / realplanetwidth;
                        //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforBottomz = (int)Math.Floor(chunkPos.Z) / realplanetwidth;
                    }


                    var somevalueforRightx = 0;
                    var somevalueforRighty = 0;
                    var somevalueforRightz = 0;

                    if (chunkPos.X < 0)
                    {
                        somevalueforRightx = (int)Math.Floor((chunkPos.X + realplanetwidth)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforRightx = (int)Math.Floor((chunkPos.X + realplanetwidth)) / realplanetwidth;
                    }

                    if (chunkPos.Y < 0)
                    {
                        somevalueforRighty = (int)Math.Floor((chunkPos.Y)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforRighty = (int)Math.Floor((chunkPos.Y)) / realplanetwidth;
                        //posnot0roundedy -= 1;
                    }

                    if (chunkPos.Z < 0)
                    {
                        somevalueforRightz = (int)Math.Floor(chunkPos.Z) / realplanetwidth;
                        //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforRightz = (int)Math.Floor(chunkPos.Z) / realplanetwidth;
                    }

                    var somevalueforLeftx = 0;
                    var somevalueforLefty = 0;
                    var somevalueforLeftz = 0;

                    if (chunkPos.X < 0)
                    {
                        somevalueforLeftx = (int)Math.Floor((chunkPos.X - realplanetwidth)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforLeftx = (int)Math.Floor((chunkPos.X - realplanetwidth)) / realplanetwidth;
                    }

                    if (chunkPos.Y < 0)
                    {
                        somevalueforLefty = (int)Math.Floor((chunkPos.Y)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforLefty = (int)Math.Floor((chunkPos.Y)) / realplanetwidth;
                        //posnot0roundedy -= 1;
                    }

                    if (chunkPos.Z < 0)
                    {
                        somevalueforLeftz = (int)Math.Floor(chunkPos.Z) / realplanetwidth;
                        //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforLeftz = (int)Math.Floor(chunkPos.Z) / realplanetwidth;
                    }





                    var somevalueforFrontx = 0;
                    var somevalueforFronty = 0;
                    var somevalueforFrontz = 0;

                    if (chunkPos.X < 0)
                    {
                        somevalueforFrontx = (int)Math.Floor((chunkPos.X)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforFrontx = (int)Math.Floor((chunkPos.X)) / realplanetwidth;
                    }

                    if (chunkPos.Y < 0)
                    {
                        somevalueforFronty = (int)Math.Floor((chunkPos.Y)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforFronty = (int)Math.Floor((chunkPos.Y)) / realplanetwidth;
                        //posnot0roundedy -= 1;
                    }

                    if (chunkPos.Z < 0)
                    {
                        somevalueforFrontz = (int)Math.Floor((chunkPos.Z + realplanetwidth)) / realplanetwidth;
                        //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforFrontz = (int)Math.Floor((chunkPos.Z + realplanetwidth)) / realplanetwidth;
                    }





                    var somevalueforBackx = 0;
                    var somevalueforBacky = 0;
                    var somevalueforBackz = 0;

                    if (chunkPos.X < 0)
                    {
                        somevalueforBackx = (int)Math.Floor((chunkPos.X)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforBackx = (int)Math.Floor((chunkPos.X)) / realplanetwidth;
                    }

                    if (chunkPos.Y < 0)
                    {
                        somevalueforBacky = (int)Math.Floor((chunkPos.Y)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforBacky = (int)Math.Floor((chunkPos.Y)) / realplanetwidth;
                        //posnot0roundedy -= 1;
                    }

                    if (chunkPos.Z < 0)
                    {
                        somevalueforBackz = (int)Math.Floor((chunkPos.Z - realplanetwidth)) / realplanetwidth;
                        //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforBackz = (int)Math.Floor((chunkPos.Z - realplanetwidth)) / realplanetwidth;
                    }*/





































                    /*
                    //BOTTOM FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi - 1, zi))
                    {
                        int someswtcBottom = 0;

                        if (componentParent.getChunk(somevalueforBottomx, somevalueforBottomy, somevalueforBottomz) != null)
                        {
                            sccsChunk someChunk = (sccsChunk)componentParent.getChunk(somevalueforBottomx, somevalueforBottomy, somevalueforBottomz);

                            if (yi == 0 && someChunk.map != null)
                            {
                                if (someChunk.map != null)
                                {
                                    if (someChunk.IsTransparent(xi, height - 1, zi))
                                    {
                                        someswtcBottom = 1;
                                    }
                                    else
                                    {
                                        //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.GameObject.position, Quaternion.identity);
                                        //someObject.GameObject.parent = chunkdata.planetchunk.GameObject;
                                    }
                                }
                                else
                                {
                                    someswtcBottom = 1;
                                }
                            }
                            else if (yi != 0)
                            {
                                someswtcBottom = 1;
                            }
                            else
                            {
                                //if (componentParent.getChunk(somevalueforBottomx, somevalueforBottomy, somevalueforBottomz) == null)
                                //{
                                //    someswtcBottom = 1;
                                //}
                                someswtcBottom = 1;
                            }
                        }
                        else
                        {
                            someswtcBottom = 1;
                        }
                        if (someswtcBottom == 1)
                        {
                            buildBottomFace();
                        }
                        //buildBottomFace();
                        //if (componentParent.getChunk(somevalueforBottomx, somevalueforBottomy, somevalueforBottomz) != null)
                        //{
                        //
                        //}
                    }
















                    //LEFT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                    if (IsTransparent(xi - 1, yi, zi))
                    {
                        int someswtcLeft = 0;



                        if (componentParent.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz) != null)
                        {
                            sccsChunk someChunk = (sccsChunk)componentParent.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz);

                            if (xi == 0 && someChunk.map != null)
                            {
                                if (someChunk.map != null)
                                {
                                    if (someChunk.IsTransparent(width - 1, yi, zi))
                                    {
                                        someswtcLeft = 1;
                                    }
                                    else
                                    {
                                        //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.GameObject.position, Quaternion.identity);
                                        //someObject.GameObject.parent = chunkdata.planetchunk.GameObject;
                                    }
                                }
                                else
                                {
                                    someswtcLeft = 1;
                                }
                            }
                            else if (xi != 0)
                            {
                                someswtcLeft = 1;
                            }
                            else
                            {
                                //if (componentParent.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz) == null)
                                //{
                                //    someswtcLeft = 1;
                                //}
                                someswtcLeft = 1;
                            }
                        }
                        else
                        {
                            someswtcLeft = 1;
                        }

                        if (someswtcLeft == 1)
                        {
                            buildTopLeft();
                        }
                        //buildTopLeft();
                    }




                    //BACK FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi, zi - 1))
                    {

                        int someswtcBack = 0;

                        if (componentParent.getChunk(somevalueforBackx, somevalueforBacky, somevalueforBackz) != null)
                        {
                            sccsChunk someChunk = (sccsChunk)componentParent.getChunk(somevalueforBackx, somevalueforBacky, somevalueforBackz);

                            if (zi == 0 && someChunk.map != null)
                            {

                                if (someChunk.map != null)
                                {
                                    if (someChunk.IsTransparent(xi, yi, depth - 1))
                                    {
                                        someswtcBack = 1;
                                    }
                                    else
                                    {
                                        //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.GameObject.position, Quaternion.identity);
                                        //someObject.GameObject.parent = chunkdata.planetchunk.GameObject;
                                    }
                                }
                                else
                                {
                                    someswtcBack = 1;
                                }
                            }
                            else if (zi != 0)
                            {
                                someswtcBack = 1;
                            }
                            else
                            {
                                if (componentParent.getChunk(somevalueforBackx, somevalueforBacky, somevalueforBackz) == null)
                                {
                                    someswtcBack = 1;
                                }
                                //someswtcBack = 1;
                            }
                        }
                        else
                        {
                            someswtcBack = 1;
                        }

                        if (someswtcBack == 1)
                        {
                            buildBackFace();
                        }
                        //buildBackFace();
                    }





                    //TOP FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi + 1, zi))
                    {

                        int someswtcTop = 0;


                        if (componentParent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz) != null)
                        {

                            sccsChunk someChunk = (sccsChunk)componentParent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz);


                            if (yi == height - 1 && someChunk.map != null)
                            {
                                if (someChunk.map != null)
                                {
                                    if (someChunk.IsTransparent(xi, 0, zi))
                                    {
                                        someswtcTop = 1;

                                    }
                                    else
                                    {
                                        //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.GameObject.position, Quaternion.identity);
                                        //someObject.GameObject.parent = chunkdata.planetchunk.GameObject;
                                    }
                                }
                                else
                                {
                                    someswtcTop = 1;
                                }
                            }
                            else if (yi != height - 1)
                            {
                                someswtcTop = 1;
                            }
                            else
                            {
                                //if (componentParent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz) == null)
                                //{
                                //    someswtcTop = 1;
                                //}
                                someswtcTop = 1;
                            }

                        }
                        else
                        {
                            someswtcTop = 1;
                        }

                        if (someswtcTop == 1)
                        {
                            buildTopFace();
                        }
                        //buildTopFace();
                    }

                    //RIGHT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                    if (IsTransparent(xi + 1, yi, zi))
                    {

                        int someswtcRight = 0;

                        if (componentParent.getChunk(somevalueforRightx, somevalueforRighty, somevalueforRightz) != null)
                        {
                        buildTopLeft
                            sccsChunk someChunk = (sccsChunk)componentParent.getChunk(somevalueforRightx, somevalueforRighty, somevalueforRightz);
                            if (xi == width - 1 && someChunk.map != null)
                            {
                                if (someChunk.map != null)
                                {
                                    if (someChunk.IsTransparent(0, yi, zi))
                                    {
                                        someswtcRight = 1;
                                    }
                                    else
                                    {
                                        //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.GameObject.position, Quaternion.identity);
                                        //someObject.GameObject.parent = chunkdata.planetchunk.GameObject;
                                    }
                                }
                                else
                                {
                                    someswtcRight = 1;
                                }
                            }
                            else if (xi != width - 1)
                            {
                                someswtcRight = 1;
                            }
                            else
                            {
                                //if (componentParent.getChunk(somevalueforRightx, somevalueforRighty, somevalueforRightz) == null)
                                //{
                                //    someswtcRight = 1;
                                //}
                                //else
                                //{
                                //    someswtcRight = 1;
                                //}
                                //someswtcRight = 1;
                            }
                        }
                        else
                        {
                            someswtcRight = 1;
                        }

                        if (someswtcRight == 1)
                        {
                            buildTopRight();
                        }
                        //buildTopRight();
                    }

                    //FRONT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi, zi + 1))
                    {
                        int someswtcFront = 0;

                        if (componentParent.getChunk(somevalueforFrontx, somevalueforFronty, somevalueforFrontz) != null)
                        {

                            sccsChunk someChunk = (sccsChunk)componentParent.getChunk(somevalueforFrontx, somevalueforFronty, somevalueforFrontz);

                            if (zi == depth - 1 && someChunk.map != null)
                            {

                                if (someChunk.map != null)
                                {
                                    if (someChunk.IsTransparent(xi, yi, 0))
                                    {

                                        //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.GameObject.position + new Vector3(xi * planeSize, yi*planeSize,0), Quaternion.identity);
                                        //someObject.GameObject.parent = chunkdata.planetchunk.GameObject;

                                        someswtcFront = 1;
                                    }
                                    else
                                    {
                                        //someswtcFront = 1;
                                        //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.GameObject.position, Quaternion.identity);
                                        //someObject.GameObject.parent = chunkdata.planetchunk.GameObject;
                                    }
                                }
                                else
                                {
                                    someswtcFront = 1;
                                }
                            }
                            else if (zi != depth - 1)
                            {
                                someswtcFront = 1;
                            }
                            else
                            {
                                //if (componentParent.getChunk(somevalueforFrontx, somevalueforFronty, somevalueforFrontz) == null)
                                //{
                                //    someswtcFront = 1;
                                //}
                                someswtcFront = 1;
                            }
                        }
                        else
                        {
                            someswtcFront = 1;
                        }

                        if (someswtcFront == 1).y
                        {
                            buildFrontFace();
                        }
                        //buildFrontFace();
                    }*/










                    if (IsTransparent(xi, yi + 1, zi))
                    {
                        buildTopFace(xi, yi, zi);
                    }

                    //WORKING
                    //LEFT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi - 1, zi))
                    {
                        buildBottomFace(xi, yi, zi);
                    }
                    if (IsTransparent(xi - 1, yi, zi))
                    {

                        buildTopLeft(xi, yi, zi);
                    }

                    if (IsTransparent(xi + 1, yi, zi))
                    {
                        buildTopRight(xi, yi, zi);
                    }

                    if (IsTransparent(xi, yi, zi + 1))
                    {
                        buildFrontFace(xi, yi, zi);
                    }

                    //BACK FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi, zi - 1))
                    {
                        buildBackFace(xi, yi, zi);
                    }


                    zzBytes++;
                    if (zzBytes >= (depth))
                    {
                        yyBytes++;
                        zzBytes = 0;
                        swtchyBytes = 1;
                    }
                    if (yyBytes >= (height) && swtchyBytes == 1)
                    {
                        xxBytes++;
                        yyBytes = 0;
                        swtchyBytes = 0;
                        swtchxBytes = 1;
                    }
                    if (xxBytes >= (width) && swtchxBytes == 1)
                    {
                        swtchyBytes = 0;
                        swtchxBytes = 0;
                        swtchzBytes = 1;
                    }

                    tBytes++;
                    counterCreateChunkObjectFacesBytes++;
                }
            }
        }

        //UnityEngine.Debug.Log("_xx: " + _xx + " _zz: " + _zz + " _maxWdth: " + _maxWdth + " _maxDepth: " + _maxDepth + " rowIterateX: " + rowIterateX + " rowIterateZ: " + rowIterateZ);
        void buildTopFace(int xi, int yi, int zi) //int _x, int _y, int _z, Vector3 chunkPos
        {
            _maxWdth = width;
            _maxDepth = depth;
            _maxHght = height;
            foundVertOne = false;
            foundVertTwo = false;
            foundVertThree = false;
            foundVertFour = false;
            //TOPFACE

            _block = _tempChunkArray[xi + width * (yi + height * zi)];

            if (_block == 1) //|| _block == 2
            {

                //if (IsTransparent(temptopfacexi, temptopfaceyi + 1, temptopfacezi))
                {
                    for (int _xx = 0; _xx < _maxWdth; _xx++)
                    {


                        rowIterateX = xi + _xx;
                        for (int _zz = 0; _zz < _maxDepth; _zz++)
                        {
                            rowIterateZ = zi + _zz;

                            if (rowIterateX < width && rowIterateZ < depth)
                            {

                                //if (someswtc == 1)
                                {
                                    if (_xx == 0 && _zz == 0)
                                    {
                                        oneVertIndexX = rowIterateX;
                                        oneVertIndexY = yi + 1;
                                        oneVertIndexZ = rowIterateZ;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                        foundVertOne = true;

                                        if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                        {
                                            _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                            if (_block == 0)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = yi + 1;
                                                threeVertIndexZ = rowIterateZ;
                                                _maxWdth = _xx;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                            }
                                            else if (_block == 1 || _block == 2)
                                            {
                                                if (blockExistsInArray(rowIterateX + 1, yi + 1, rowIterateZ))
                                                {
                                                    _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi + 1) + height * (rowIterateZ))];

                                                    if (_block == 1 || _block == 2)
                                                    {
                                                        threeVertIndexX = rowIterateX + 1;
                                                        threeVertIndexY = yi + 1;
                                                        threeVertIndexZ = rowIterateZ;
                                                        _maxWdth = _xx;
                                                        foundVertThree = true;
                                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi + 1;
                                            threeVertIndexZ = rowIterateZ;
                                            _maxWdth = _xx;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }

                                        if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArray[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                            if (_block == 0)
                                            {
                                                twoVertIndexX = rowIterateX;
                                                twoVertIndexY = yi + 1;
                                                twoVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz + 1;
                                                foundVertTwo = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                            else if (_block == 1 || _block == 2) //_block == 1||
                                            {
                                                if (_block == 1)
                                                {
                                                    if (blockExistsInArray(rowIterateX, yi + 1, rowIterateZ + 1))
                                                    {
                                                        _block = _tempChunkArray[(rowIterateX) + width * ((yi + 1) + height * (rowIterateZ + 1))];

                                                        if (_block == 1 || _block == 2)
                                                        {
                                                            twoVertIndexX = rowIterateX;
                                                            twoVertIndexY = yi + 1;
                                                            twoVertIndexZ = rowIterateZ + 1;
                                                            _maxDepth = _zz + 1;
                                                            foundVertTwo = true;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                            {
                                                                fourVertIndexX = threeVertIndexX;
                                                                fourVertIndexY = yi + 1;
                                                                fourVertIndexZ = twoVertIndexZ;
                                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (_block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = yi + 1;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = yi + 1;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }

                                    else if (_xx == 0 && _zz > 0)
                                    {
                                        if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArray[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                            if (_block == 0)
                                            {
                                                twoVertIndexX = rowIterateX;
                                                twoVertIndexY = yi + 1;
                                                twoVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz + 1;
                                                foundVertTwo = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }


                                            }
                                            else if (_block == 1 || _block == 2) //_block == 1||
                                            {
                                                if (_block == 1)
                                                {
                                                    if (blockExistsInArray(rowIterateX, yi + 1, rowIterateZ + 1))
                                                    {
                                                        _block = _tempChunkArray[(rowIterateX) + width * ((yi + 1) + height * (rowIterateZ + 1))];
                                                        if (_block == 1 || _block == 2)
                                                        {
                                                            twoVertIndexX = rowIterateX;
                                                            twoVertIndexY = yi + 1;
                                                            twoVertIndexZ = rowIterateZ + 1;
                                                            _maxDepth = _zz + 1;
                                                            foundVertTwo = true;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                            {
                                                                fourVertIndexX = threeVertIndexX;
                                                                fourVertIndexY = yi + 1;
                                                                fourVertIndexZ = twoVertIndexZ;
                                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                            }
                                                        }
                                                    }
                                                    else //continue??
                                                    {

                                                    }
                                                }
                                                else if (_block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = yi + 1;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = yi + 1;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                        }

                                        if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                        {
                                            _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                            if (_block == 0)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = yi + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWdth = _xx;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                            else if (_block == 1 || _block == 2)
                                            {
                                                //********************************************************
                                                if (blockExistsInArray(rowIterateX + 1, yi + 1, rowIterateZ))
                                                {
                                                    _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi + 1) + height * (rowIterateZ))];
                                                    if (_block == 1 || _block == 2)
                                                    {
                                                        threeVertIndexX = rowIterateX + 1;
                                                        threeVertIndexY = yi + 1;
                                                        threeVertIndexZ = rowIterateZ - _zz;
                                                        _maxWdth = _xx;
                                                        foundVertThree = true;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi + 1;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                                //************************************************************
                                            }
                                        }
                                        else
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_xx > 0 && _zz == 0)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                        {
                                            _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                            if (_block == 0)
                                            {
                                                //UnityEngine.Debug.Log("test");
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = yi + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWdth = _xx;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                if (foundVertTwo)
                                                {
                                                    if (foundVertThree)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            else if (_block == 1 || _block == 2)
                                            {
                                                if (blockExistsInArray(rowIterateX + 1, yi + 1, rowIterateZ))
                                                {
                                                    _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi + 1) + height * (rowIterateZ))];
                                                    if (_block == 1 || _block == 2)
                                                    {
                                                        threeVertIndexX = rowIterateX + 1;
                                                        threeVertIndexY = yi + 1;
                                                        threeVertIndexZ = rowIterateZ - _zz;
                                                        _maxWdth = _xx;
                                                        foundVertThree = true;
                                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi + 1;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxWdth = _xx;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }

                                        if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArray[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }

                                            if (blockExistsInArray(rowIterateX, yi + 1, rowIterateZ + 1))
                                            {
                                                //*****************************************************************************
                                                _block = _tempChunkArray[(rowIterateX) + width * ((yi + 1) + height * (rowIterateZ + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                                //*****************************************************************************
                                            }
                                        }
                                        else
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }

                                    else if (_xx > 0 && _zz > 0)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                        {
                                            _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                            if (_block == 0)
                                            {
                                                //UnityEngine.Debug.Log("test");
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = yi + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWdth = _xx;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                            else if (_block == 1 || _block == 2)
                                            {
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }

                                                //***********************************************************
                                                if (blockExistsInArray(rowIterateX + 1, yi + 1, rowIterateZ))
                                                {
                                                    _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi + 1) + height * (rowIterateZ))];
                                                    if (_block == 1 || _block == 2)
                                                    {
                                                        threeVertIndexX = rowIterateX + 1;
                                                        threeVertIndexY = yi + 1;
                                                        threeVertIndexZ = rowIterateZ - _zz;
                                                        _maxWdth = _xx;

                                                        foundVertThree = true;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi + 1;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                                //*******************************************************
                                            }
                                        }
                                        else
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }

                                        if (!blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(rowIterateX, yi, rowIterateZ))
                            {
                                _tempChunkArray[(rowIterateX) + width * (yi + height * (rowIterateZ))] = 2;
                                ////////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + chunkPos, Quaternion.identity);
                            }
                        }
                    }





                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((oneVertIndexX+ _vert_offset_x) * planeSize, (oneVertIndexY + _vert_offset_y) * planeSize, (oneVertIndexZ + _vert_offset_z )* planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(1, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((twoVertIndexX + _vert_offset_x) * planeSize,( twoVertIndexY + _vert_offset_y) * planeSize,( twoVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(0, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((threeVertIndexX + _vert_offset_x) * planeSize, (threeVertIndexY + _vert_offset_y) * planeSize,( threeVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(1, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((fourVertIndexX + _vert_offset_x) * planeSize, (fourVertIndexY + _vert_offset_y) * planeSize, (fourVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(0, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                    {
                        _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);
                    }
                }
            }

            /*//_mesh = new Mesh();
            _mesh.vertices = vertexlist.ToArray();
            _mesh.listOfTriangleIndices = listOfTriangleIndices.ToArray();

            _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

            _meshRend = _testChunk.GetComponent<MeshRenderer>();
            _meshRend.material = _mat;*/

        }







        void buildTopLeft(int xi, int yi, int zi) //int _x, int _y, int _z, Vector3 chunkPos
        {
            _maxWdth = width;
            _maxDepth = depth;
            _maxHght = height;
            foundVertOne = false;
            foundVertTwo = false;
            foundVertThree = false;
            foundVertFour = false;
            //TOPFACE

            _block = _tempChunkArrayLeftFace[xi + width * (yi + height * zi)];
            if (_block == 1) //|| _block == 2
            {
                if (IsTransparent(xi - 1, yi, zi))
                {
                    for (int _yy = 0; _yy < _maxHght; _yy++)
                    {
                        rowIterateY = yi + _yy;
                        for (int _zz = 0; _zz < _maxDepth; _zz++)
                        {
                            rowIterateZ = zi + _zz;

                            if (rowIterateY < height && rowIterateZ < depth)
                            {
                                if (_yy == 0 && _zz == 0)
                                {
                                    oneVertIndexX = xi;
                                    oneVertIndexY = rowIterateY;
                                    oneVertIndexZ = rowIterateZ;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                    foundVertOne = true;

                                    if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                    {
                                        _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = xi;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = rowIterateZ;
                                            _maxHght = _yy;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (blockExistsInArray(xi - 1, rowIterateY + 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = xi;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = rowIterateZ;
                                                    _maxHght = _yy;
                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = xi;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxHght = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                    {
                                        _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                        if (_block == 0)
                                        {
                                            twoVertIndexX = xi;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (_block == 1 || _block == 2) //_block == 1||
                                        {
                                            if (_block == 1)
                                            {
                                                if (blockExistsInArray(xi - 1, rowIterateY, rowIterateZ + 1))
                                                {
                                                    _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                                    if (_block == 1 || _block == 2)
                                                    {
                                                        twoVertIndexX = xi;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = rowIterateZ + 1;
                                                        _maxDepth = _zz + 1;
                                                        foundVertTwo = true;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = xi;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                            }
                                            else if (_block == 2)
                                            {
                                                twoVertIndexX = xi;
                                                twoVertIndexY = rowIterateY;
                                                twoVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz + 1;
                                                foundVertTwo = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = xi;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_yy == 0 && _zz > 0)
                                {
                                    if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                    {
                                        _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                        if (_block == 0)
                                        {
                                            twoVertIndexX = xi;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }


                                        }
                                        else if (_block == 1 || _block == 2) //_block == 1||
                                        {
                                            if (_block == 1)
                                            {
                                                if (blockExistsInArray(xi - 1, rowIterateY, rowIterateZ + 1))
                                                {
                                                    _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                                    if (_block == 1 || _block == 2)
                                                    {
                                                        twoVertIndexX = xi;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = rowIterateZ + 1;
                                                        _maxDepth = _zz + 1;
                                                        foundVertTwo = true;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = xi;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                                else //continue??
                                                {

                                                }
                                            }
                                            else if (_block == 2)
                                            {
                                                twoVertIndexX = xi;
                                                twoVertIndexY = rowIterateY;
                                                twoVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz + 1;
                                                foundVertTwo = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = xi;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                    }

                                    if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                    {
                                        _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = xi;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxHght = _yy;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            //********************************************************
                                            if (blockExistsInArray(xi - 1, rowIterateY + 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = xi;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxHght = _yy;
                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);
                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //************************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                                else if (_yy > 0 && _zz == 0)
                                {
                                    if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                    {
                                        _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = xi;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxHght = _yy;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = xi;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (blockExistsInArray(xi - 1, rowIterateY + 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = xi;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxHght = _yy;
                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                    fourVertIndexX = xi;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = xi;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHght = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                    {
                                        _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }

                                        if (blockExistsInArray(xi - 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            //*****************************************************************************
                                            _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                            //*****************************************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_yy > 0 && _zz > 0)
                                {
                                    if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                    {
                                        _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = xi;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxHght = _yy;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }

                                            //***********************************************************
                                            if (blockExistsInArray(xi - 1, rowIterateY + 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = xi;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxHght = _yy;

                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //*******************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(xi, rowIterateY, rowIterateZ))
                            {
                                _tempChunkArrayLeftFace[(xi) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                                ////////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + chunkPos, Quaternion.identity);
                            }
                        }
                    }








                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((oneVertIndexX + _vert_offset_x) * planeSize, (oneVertIndexY + _vert_offset_y) * planeSize, (oneVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(1, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((twoVertIndexX + _vert_offset_x) * planeSize, (twoVertIndexY + _vert_offset_y) * planeSize, (twoVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(0, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((threeVertIndexX + _vert_offset_x) * planeSize, (threeVertIndexY + _vert_offset_y) * planeSize, (threeVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(1, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((fourVertIndexX + _vert_offset_x) * planeSize, (fourVertIndexY + _vert_offset_y) * planeSize, (fourVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(0, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                    {
                        _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);
                    }
                }
            }
            /*//_mesh = new Mesh();
            _mesh.vertices = vertexlist.ToArray();
            _mesh.listOfTriangleIndices = _trigz.ToArray();

            _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

            _meshRend = _testChunk.GetComponent<MeshRenderer>();
            _meshRend.material = _mat;*/

            /*_mesh.vertices = vertexlist.ToArray();
            _mesh.listOfTriangleIndices = listOfTriangleIndices.ToArray();

            _testChunk.GetComponent<MeshFilter>().mesh = _mesh;*/
            //_testChunk.GetComponent<MeshRenderer>().material = _mat;
        }

        void buildTopRight(int xi, int yi, int zi) //int xi, int _y, int _z, Vector3 chunkPos
        {
            _maxWdth = width;
            _maxDepth = depth;
            _maxHght = height;
            foundVertOne = false;
            foundVertTwo = false;
            foundVertThree = false;
            foundVertFour = false;
            //TOPFACE

            _block = _tempChunkArrayRightFace[xi + width * (yi + height * zi)];

            if (_block == 1) //|| _block == 2
            {
                if (IsTransparent(xi + 1, yi, zi))
                {
                    for (int _yy = 0; _yy < _maxHght; _yy++)
                    {
                        rowIterateY = yi + _yy;
                        for (int _zz = 0; _zz < _maxDepth; _zz++)
                        {
                            rowIterateZ = zi + _zz;

                            if (rowIterateY < height && rowIterateZ < depth)
                            {
                                if (_yy == 0 && _zz == 0)
                                {
                                    oneVertIndexX = xi + 1;
                                    oneVertIndexY = rowIterateY;
                                    oneVertIndexZ = rowIterateZ;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                    foundVertOne = true;

                                    if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                    {
                                        _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = xi + 1;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = rowIterateZ;
                                            _maxHght = _yy;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (blockExistsInArray(xi + 1, rowIterateY + 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = xi + 1;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = rowIterateZ;
                                                    _maxHght = _yy;
                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = xi + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxHght = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                    {
                                        _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                        if (_block == 0)
                                        {
                                            twoVertIndexX = xi + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (_block == 1 || _block == 2) //_block == 1||
                                        {
                                            if (_block == 1)
                                            {
                                                if (blockExistsInArray(xi + 1, rowIterateY, rowIterateZ + 1))
                                                {
                                                    _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                                    if (_block == 1 || _block == 2)
                                                    {
                                                        twoVertIndexX = xi + 1;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = rowIterateZ + 1;
                                                        _maxDepth = _zz + 1;
                                                        foundVertTwo = true;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = xi + 1;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                            }
                                            else if (_block == 2)
                                            {
                                                twoVertIndexX = xi + 1;
                                                twoVertIndexY = rowIterateY;
                                                twoVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz + 1;
                                                foundVertTwo = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = xi + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_yy == 0 && _zz > 0)
                                {
                                    if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                    {
                                        _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                        if (_block == 0)
                                        {
                                            twoVertIndexX = xi + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }


                                        }
                                        else if (_block == 1 || _block == 2) //_block == 1||
                                        {
                                            if (_block == 1)
                                            {
                                                if (blockExistsInArray(xi + 1, rowIterateY, rowIterateZ + 1))
                                                {
                                                    _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                                    if (_block == 1 || _block == 2)
                                                    {
                                                        twoVertIndexX = xi + 1;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = rowIterateZ + 1;
                                                        _maxDepth = _zz + 1;
                                                        foundVertTwo = true;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = xi + 1;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                                else //continue??
                                                {

                                                }
                                            }
                                            else if (_block == 2)
                                            {
                                                twoVertIndexX = xi + 1;
                                                twoVertIndexY = rowIterateY;
                                                twoVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz + 1;
                                                foundVertTwo = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = xi + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                    }

                                    if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                    {
                                        _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = xi + 1;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxHght = _yy;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            //********************************************************
                                            if (blockExistsInArray(xi + 1, rowIterateY + 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = xi + 1;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxHght = _yy;
                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);
                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi + 1;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //************************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                                else if (_yy > 0 && _zz == 0)
                                {
                                    if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                    {
                                        _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = xi + 1;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxHght = _yy;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (blockExistsInArray(xi + 1, rowIterateY + 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = xi + 1;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxHght = _yy;
                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = xi + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHght = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                    {
                                        _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }

                                        if (blockExistsInArray(xi + 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            //*****************************************************************************
                                            _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                            //*****************************************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_yy > 0 && _zz > 0)
                                {
                                    if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                    {
                                        _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = xi + 1;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxHght = _yy;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }

                                            //***********************************************************
                                            if (blockExistsInArray(xi + 1, rowIterateY + 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = xi + 1;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxHght = _yy;

                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi + 1;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //*******************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(xi, rowIterateY, rowIterateZ))
                            {
                                _tempChunkArrayRightFace[(xi) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                                ////////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + chunkPos, Quaternion.identity);
                            }
                        }
                    }






                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((oneVertIndexX + _vert_offset_x) * planeSize, (oneVertIndexY + _vert_offset_y) * planeSize, (oneVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(1, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((twoVertIndexX + _vert_offset_x) * planeSize, (twoVertIndexY + _vert_offset_y) * planeSize, (twoVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(0, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((threeVertIndexX + _vert_offset_x) * planeSize, (threeVertIndexY + _vert_offset_y) * planeSize, (threeVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(1, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((fourVertIndexX + _vert_offset_x) * planeSize, (fourVertIndexY + _vert_offset_y) * planeSize, (fourVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(0, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }



                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                    {
                        _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                    }
                }
            }
            /*//_mesh = new Mesh();
            _mesh.vertices = vertexlist.ToArray();
            _mesh.listOfTriangleIndices = listOfTriangleIndices.ToArray();

            _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

            _meshRend = _testChunk.GetComponent<MeshRenderer>();
            _meshRend.material = _mat;*/
        }




        void buildFrontFace(int xi, int yi, int zi) // int _x, int _y, int _z, Vector3 chunkPos
        {

            _maxWdth = width;
            _maxDepth = depth;
            _maxHght = height;
            foundVertOne = false;
            foundVertTwo = false;
            foundVertThree = false;
            foundVertFour = false;
            //TOPFACE

            _block = _tempChunkArrayFrontFace[xi + width * (yi + height * zi)];

            if (_block == 1) //|| _block == 2
            {
                if (IsTransparent(xi, yi, zi + 1))
                {
                    for (int _yy = 0; _yy < _maxHght; _yy++)
                    {
                        rowIterateY = yi + _yy;
                        for (int _xx = 0; _xx < _maxWdth; _xx++)
                        {
                            rowIterateX = xi + _xx;

                            if (rowIterateY < height && rowIterateX < width)
                            {
                                if (_yy == 0 && _xx == 0)
                                {
                                    oneVertIndexX = rowIterateX;
                                    oneVertIndexY = rowIterateY;
                                    oneVertIndexZ = zi + 1;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    foundVertOne = true;

                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = rowIterateX;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi + 1;
                                            _maxHght = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi + 1))
                                            {
                                                _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = zi + 1;
                                                    _maxHght = _yy;
                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi + 1;
                                        _maxHght = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                        if (_block == 0)
                                        {
                                            twoVertIndexX = rowIterateX + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = zi + 1;
                                            _maxWdth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (_block == 1 || _block == 2) //_block == 1||
                                        {
                                            if (_block == 1)
                                            {
                                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi + 1))
                                                {
                                                    _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi + 1))];

                                                    if (_block == 1 || _block == 2)
                                                    {
                                                        twoVertIndexX = rowIterateX + 1;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = zi + 1;
                                                        _maxWdth = _xx + 1;
                                                        foundVertTwo = true;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi + 1;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                            }
                                            else if (_block == 2)
                                            {
                                                twoVertIndexX = rowIterateX + 1;
                                                twoVertIndexY = rowIterateY;
                                                twoVertIndexZ = zi + 1;
                                                _maxWdth = _xx + 1;
                                                foundVertTwo = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi + 1;
                                        _maxWdth = _xx + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_yy == 0 && _xx > 0)
                                {
                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                        if (_block == 0)
                                        {
                                            twoVertIndexX = rowIterateX + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = zi + 1;
                                            _maxWdth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }


                                        }
                                        else if (_block == 1 || _block == 2) //_block == 1||
                                        {
                                            if (_block == 1)
                                            {
                                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi + 1))
                                                {
                                                    _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi + 1))];
                                                    if (_block == 1 || _block == 2)
                                                    {
                                                        twoVertIndexX = rowIterateX + 1;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = zi + 1;
                                                        _maxWdth = _xx + 1;
                                                        foundVertTwo = true;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi + 1;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                                else //continue??
                                                {

                                                }
                                            }
                                            else if (_block == 2)
                                            {
                                                twoVertIndexX = rowIterateX + 1;
                                                twoVertIndexY = rowIterateY;
                                                twoVertIndexZ = zi + 1;
                                                _maxWdth = _xx + 1;
                                                foundVertTwo = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi + 1;
                                        _maxWdth = _xx + 1;
                                        foundVertTwo = true;


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
                                    }

                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = rowIterateX - _xx;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi + 1;
                                            _maxHght = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            //********************************************************
                                            if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi + 1))
                                            {
                                                _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX - _xx;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = zi + 1;
                                                    _maxHght = _yy;
                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi + 1;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //************************************************************
                                        }
                                    }
                                    else
                                    {

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                                else if (_yy > 0 && _xx == 0)
                                {
                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (_block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = rowIterateX - _xx;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi + 1;
                                            _maxHght = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi + 1))
                                            {
                                                _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX - _xx;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = zi + 1;
                                                    _maxHght = _yy;
                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi + 1;
                                        _maxHght = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }

                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi + 1))
                                        {
                                            //*****************************************************************************
                                            _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                            //*****************************************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_yy > 0 && _xx > 0)
                                {
                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (_block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = rowIterateX - _xx;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi + 1;
                                            _maxHght = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }

                                            //***********************************************************
                                            if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi + 1))
                                            {
                                                _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX - _xx;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = zi + 1;
                                                    _maxHght = _yy;

                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi + 1;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //*******************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(rowIterateX, rowIterateY, zi))
                            {
                                _tempChunkArrayFrontFace[(rowIterateX) + width * (rowIterateY + height * (zi))] = 2;
                                //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + _chunkPos, Quaternion.identity);
                            }
                        }
                    }




                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((oneVertIndexX + _vert_offset_x) * planeSize, (oneVertIndexY + _vert_offset_y) * planeSize, (oneVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(1, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((twoVertIndexX + _vert_offset_x) * planeSize, (twoVertIndexY + _vert_offset_y) * planeSize, (twoVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(0, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                

                    if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((fourVertIndexX + _vert_offset_x) * planeSize, (fourVertIndexY + _vert_offset_y) * planeSize, (fourVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(0, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((threeVertIndexX + _vert_offset_x) * planeSize, (threeVertIndexY + _vert_offset_y) * planeSize, (threeVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(1, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                    {
                        _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                        /*if (map[x, y, z] == leftExtremity[x, y, z]
                         || map[x, y, z] == backExtremity[x, y, z]
                         || map[x, y, z] == rightExtremity[x, y, z]
                         || map[x, y, z] == frontExtremity[x, y, z]
                         || map[x, y, z] == leftInsideCornerExtremity[x, y, z]
                         || map[x, y, z] == rightInsideCornerExtremity[x, y, z]
                        || map[x, y, z] == backInsideCornerExtremity[x, y, z]
                        || map[x, y, z] == frontInsideCornerExtremity[x, y, z]
                        || map[x, y, z] == leftOutsideCornerExtremity[x, y, z]
                        || map[x, y, z] == rightOutsideCornerExtremity[x, y, z]
                        || map[x, y, z] == backOutsideCornerExtremity[x, y, z]
                        || map[x, y, z] == frontOutsideCornerExtremity[x, y, z])
                        {
                            uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                            uv.Add(new Vector2(0.0625f, 0.9375f));
                            uv.Add(new Vector2(0, 0.875f));
                            uv.Add(new Vector2(0.0625f, 0.875f));
                        }
                        else
                        {
                            uv.Add(new Vector2(0, 1)); //// dis is weed
                            uv.Add(new Vector2(0.0625f, 1));
                            uv.Add(new Vector2(0, 0.9375f));
                            uv.Add(new Vector2(0.0625f, 0.9375f));
                        }*/


                        /*uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                        uv.Add(new Vector2(0.0625f, 0.9375f));
                        uv.Add(new Vector2(0, 0.875f));
                        uv.Add(new Vector2(0.0625f, 0.875f));*/

                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);
                    }
                }
            }
            /*//_mesh = new Mesh();
            _mesh.vertices = vertexlist.ToArray();
            _mesh.listOfTriangleIndices = _trigz.ToArray();

            _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

            _meshRend = _testChunk.GetComponent<MeshRenderer>();
            _meshRend.material = _mat;*/
        }


        void buildBackFace(int xi, int yi, int zi) //int _x, int _y, int zi, Vector3 chunkPos
        {
            _maxWdth = width;
            _maxDepth = depth;
            _maxHght = height;
            foundVertOne = false;
            foundVertTwo = false;
            foundVertThree = false;
            foundVertFour = false;
            //TOPFACE

            _block = _tempChunkArrayBackFace[xi + width * (yi + height * zi)];
            if (_block == 1) //|| _block == 2
            {
                if (IsTransparent(xi, yi, zi - 1))
                {
                    for (int _yy = 0; _yy < _maxHght; _yy++)
                    {
                        rowIterateY = yi + _yy;
                        for (int _xx = 0; _xx < _maxWdth; _xx++)
                        {
                            rowIterateX = xi + _xx;

                            if (rowIterateY < height && rowIterateX < width)
                            {
                                if (_yy == 0 && _xx == 0)
                                {
                                    oneVertIndexX = rowIterateX;
                                    oneVertIndexY = rowIterateY;
                                    oneVertIndexZ = zi;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                    foundVertOne = true;

                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = rowIterateX;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi;
                                            _maxHght = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi - 1))
                                            {
                                                _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi - 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = zi;
                                                    _maxHght = _yy;
                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi;
                                        _maxHght = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                        if (_block == 0)
                                        {
                                            twoVertIndexX = rowIterateX + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = zi;
                                            _maxWdth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (_block == 1 || _block == 2) //_block == 1||
                                        {
                                            if (_block == 1)
                                            {
                                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi - 1))
                                                {
                                                    _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi - 1))];

                                                    if (_block == 1 || _block == 2)
                                                    {
                                                        twoVertIndexX = rowIterateX + 1;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = zi;
                                                        _maxWdth = _xx + 1;
                                                        foundVertTwo = true;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                            }
                                            else if (_block == 2)
                                            {
                                                twoVertIndexX = rowIterateX + 1;
                                                twoVertIndexY = rowIterateY;
                                                twoVertIndexZ = zi;
                                                _maxWdth = _xx + 1;
                                                foundVertTwo = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi;
                                        _maxWdth = _xx + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_yy == 0 && _xx > 0)
                                {
                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                        if (_block == 0)
                                        {
                                            twoVertIndexX = rowIterateX + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = zi;
                                            _maxWdth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }


                                        }
                                        else if (_block == 1 || _block == 2) //_block == 1||
                                        {
                                            if (_block == 1)
                                            {
                                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi - 1))
                                                {
                                                    _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi - 1))];
                                                    if (_block == 1 || _block == 2)
                                                    {
                                                        twoVertIndexX = rowIterateX + 1;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = zi;
                                                        _maxWdth = _xx + 1;
                                                        foundVertTwo = true;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                                else //continue??
                                                {

                                                }
                                            }
                                            else if (_block == 2)
                                            {
                                                twoVertIndexX = rowIterateX + 1;
                                                twoVertIndexY = rowIterateY;
                                                twoVertIndexZ = zi;
                                                _maxWdth = _xx + 1;
                                                foundVertTwo = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi;
                                        _maxWdth = _xx + 1;
                                        foundVertTwo = true;


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
                                    }

                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = rowIterateX - _xx;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi;
                                            _maxHght = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            //********************************************************
                                            if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi - 1))
                                            {
                                                _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi - 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX - _xx;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = zi;
                                                    _maxHght = _yy;
                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //************************************************************
                                        }
                                    }
                                    else
                                    {

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                                else if (_yy > 0 && _xx == 0)
                                {
                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (_block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = rowIterateX - _xx;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi;
                                            _maxHght = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi - 1))
                                            {
                                                _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi - 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX - _xx;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = zi;
                                                    _maxHght = _yy;
                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi;
                                        _maxHght = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }

                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi - 1))
                                        {
                                            //*****************************************************************************
                                            _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi - 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                            //*****************************************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_yy > 0 && _xx > 0)
                                {
                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (_block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = rowIterateX - _xx;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi;
                                            _maxHght = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }

                                            //***********************************************************
                                            if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi - 1))
                                            {
                                                _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi - 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX - _xx;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = zi;
                                                    _maxHght = _yy;

                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //*******************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(rowIterateX, rowIterateY, zi))
                            {
                                _tempChunkArrayBackFace[(rowIterateX) + width * (rowIterateY + height * (zi))] = 2;
                                //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + _chunkPos, Quaternion.identity);
                            }
                        }
                    }


                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((oneVertIndexX + _vert_offset_x) * planeSize, (oneVertIndexY + _vert_offset_y) * planeSize, (oneVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(1, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((twoVertIndexX + _vert_offset_x) * planeSize, (twoVertIndexY + _vert_offset_y) * planeSize, (twoVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(0, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }



                
                    if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((threeVertIndexX + _vert_offset_x) * planeSize, (threeVertIndexY + _vert_offset_y) * planeSize, (threeVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(1, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((fourVertIndexX + _vert_offset_x) * planeSize, (fourVertIndexY + _vert_offset_y) * planeSize, (fourVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(0, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                    {
                        _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                        /*if (map[x, y, z] == leftExtremity[x, y, z]
                         || map[x, y, z] == backExtremity[x, y, z]
                         || map[x, y, z] == rightExtremity[x, y, z]
                         || map[x, y, z] == frontExtremity[x, y, z]
                         || map[x, y, z] == leftInsideCornerExtremity[x, y, z]
                         || map[x, y, z] == rightInsideCornerExtremity[x, y, z]
                        || map[x, y, z] == backInsideCornerExtremity[x, y, z]
                        || map[x, y, z] == frontInsideCornerExtremity[x, y, z]
                        || map[x, y, z] == leftOutsideCornerExtremity[x, y, z]
                        || map[x, y, z] == rightOutsideCornerExtremity[x, y, z]
                        || map[x, y, z] == backOutsideCornerExtremity[x, y, z]
                        || map[x, y, z] == frontOutsideCornerExtremity[x, y, z])
                        {
                            uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                            uv.Add(new Vector2(0.0625f, 0.9375f));
                            uv.Add(new Vector2(0, 0.875f));
                            uv.Add(new Vector2(0.0625f, 0.875f));
                        }
                        else
                        {
                            uv.Add(new Vector2(0, 1)); //// dis is weed
                            uv.Add(new Vector2(0.0625f, 1));
                            uv.Add(new Vector2(0, 0.9375f));
                            uv.Add(new Vector2(0.0625f, 0.9375f));
                        }*/


                        /*uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                        uv.Add(new Vector2(0.0625f, 0.9375f));
                        uv.Add(new Vector2(0, 0.875f));
                        uv.Add(new Vector2(0.0625f, 0.875f));*/


                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                    }
                }
            }
        }




        void buildBottomFace(int xi,int yi, int zi) //int _x, int _y, int _z, Vector3 chunkPos
        {
            _maxWdth = width;
            _maxDepth = depth;
            _maxHght = height;
            foundVertOne = false;
            foundVertTwo = false;
            foundVertThree = false;
            foundVertFour = false;
            //TOPFACE

            _block = _tempChunkArrayBottomFace[xi + width * (yi + height * zi)];
            if (_block == 1) //|| _block == 2
            {
                if (IsTransparent(xi, yi - 1, zi))
                {
                    for (int _xx = 0; _xx < _maxWdth; _xx++)
                    {
                        rowIterateX = xi + _xx;
                        for (int _zz = 0; _zz < _maxDepth; _zz++)
                        {
                            rowIterateZ = zi + _zz;

                            if (rowIterateX < width && rowIterateZ < depth)
                            {
                                if (_xx == 0 && _zz == 0)
                                {
                                    oneVertIndexX = rowIterateX;
                                    oneVertIndexY = yi;
                                    oneVertIndexZ = rowIterateZ;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                    foundVertOne = true;

                                    if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                    {
                                        _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi;
                                            threeVertIndexZ = rowIterateZ;
                                            _maxWdth = _xx;
                                            foundVertThree = true;
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (blockExistsInArray(rowIterateX + 1, yi - 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi - 1) + height * (rowIterateZ))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX + 1;
                                                    threeVertIndexY = yi;
                                                    threeVertIndexZ = rowIterateZ;
                                                    _maxWdth = _xx;
                                                    foundVertThree = true;
                                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = yi;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxWdth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                        if (_block == 0)
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = yi;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (_block == 1 || _block == 2) //_block == 1||
                                        {
                                            if (_block == 1)
                                            {
                                                if (blockExistsInArray(rowIterateX, yi - 1, rowIterateZ + 1))
                                                {
                                                    _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((yi - 1) + height * (rowIterateZ + 1))];

                                                    if (_block == 1 || _block == 2)
                                                    {
                                                        twoVertIndexX = rowIterateX;
                                                        twoVertIndexY = yi;
                                                        twoVertIndexZ = rowIterateZ + 1;
                                                        _maxDepth = _zz + 1;
                                                        foundVertTwo = true;
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                            }
                                            else if (_block == 2)
                                            {
                                                twoVertIndexX = rowIterateX;
                                                twoVertIndexY = yi;
                                                twoVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz + 1;
                                                foundVertTwo = true;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = yi;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_xx == 0 && _zz > 0)
                                {
                                    if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                        if (_block == 0)
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = yi;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }


                                        }
                                        else if (_block == 1 || _block == 2) //_block == 1||
                                        {
                                            if (_block == 1)
                                            {
                                                if (blockExistsInArray(rowIterateX, yi - 1, rowIterateZ + 1))
                                                {
                                                    _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((yi - 1) + height * (rowIterateZ + 1))];
                                                    if (_block == 1 || _block == 2)
                                                    {
                                                        twoVertIndexX = rowIterateX;
                                                        twoVertIndexY = yi;
                                                        twoVertIndexZ = rowIterateZ + 1;
                                                        _maxDepth = _zz + 1;
                                                        foundVertTwo = true;
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                                else //continue??
                                                {

                                                }
                                            }
                                            else if (_block == 2)
                                            {
                                                twoVertIndexX = rowIterateX;
                                                twoVertIndexY = yi;
                                                twoVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz + 1;
                                                foundVertTwo = true;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = yi;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                    {
                                        _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxWdth = _xx;
                                            foundVertThree = true;
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            //********************************************************
                                            if (blockExistsInArray(rowIterateX + 1, yi - 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi - 1) + height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX + 1;
                                                    threeVertIndexY = yi;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxWdth = _xx;
                                                    foundVertThree = true;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //************************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                                else if (_xx > 0 && _zz == 0)
                                {
                                    if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                    {
                                        _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxWdth = _xx;
                                            foundVertThree = true;
                                            ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (blockExistsInArray(rowIterateX + 1, yi - 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi - 1) + height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX + 1;
                                                    threeVertIndexY = yi;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxWdth = _xx;
                                                    foundVertThree = true;
                                                    ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = yi;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWdth = _xx;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }

                                        if (blockExistsInArray(rowIterateX, yi - 1, rowIterateZ + 1))
                                        {
                                            //*****************************************************************************
                                            _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((yi - 1) + height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                            //*****************************************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_xx > 0 && _zz > 0)
                                {
                                    if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                    {
                                        _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxWdth = _xx;
                                            foundVertThree = true;
                                            ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }

                                            //***********************************************************
                                            if (blockExistsInArray(rowIterateX + 1, yi - 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi - 1) + height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX + 1;
                                                    threeVertIndexY = yi;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxWdth = _xx;

                                                    foundVertThree = true;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //*******************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(rowIterateX, yi, rowIterateZ))
                            {
                                _tempChunkArrayBottomFace[(rowIterateX) + width * (yi + height * (rowIterateZ))] = 2;
                                //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + chunkPos, Quaternion.identity);
                            }
                        }
                    }






                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((oneVertIndexX + _vert_offset_x) * planeSize, (oneVertIndexY + _vert_offset_y) * planeSize, (oneVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(1, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((twoVertIndexX + _vert_offset_x) * planeSize, (twoVertIndexY + _vert_offset_y) * planeSize, (twoVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(0, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }




                    if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((threeVertIndexX + _vert_offset_x) * planeSize, (threeVertIndexY + _vert_offset_y) * planeSize, (threeVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(1, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_cube.DVertex()
                        {
                            position = new Vector3((fourVertIndexX + _vert_offset_x) * planeSize, (fourVertIndexY + _vert_offset_y) * planeSize, (fourVertIndexZ + _vert_offset_z) * planeSize) + chunkinitpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            texture = new Vector2(0, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                    {
                        _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                    }
                }
            }
            /*//_mesh = new Mesh();
            _mesh.vertices = vertexlist.ToArray();
            _mesh.listOfTriangleIndices = listOfTriangleIndices.ToArray();

            _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

            _meshRend = _testChunk.GetComponent<MeshRenderer>();
            _meshRend.material = _mat;*/
        }























        /*
        public void setAdjacentChunks(Vector3 pos, int indexx, int indexy, int indexz)
        {
            //int width = currentChunk.sccsChunk.width;
            //int height = currentChunk.sccsChunk.height;
            //int depth = currentChunk.sccsChunk.depth;

            //////Debug.Log("x: " + (indexx) + " y: " + (indexy) + " z: " + (indexz));

            int useonlyunitOneForNeighboorIndexPlease = 1;

            if (indexx == 0)
            {
                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z);

                    if (adjacentChunk.map != null)
                    {


                        if (adjacentChunk.GetByte((int)width - 1, (int)indexy, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)indexy, (int)indexz, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            if (indexx == width - 1)
            {
                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)0, (int)indexy, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk right exists");
                            adjacentChunk.SetByte((int)0, (int)indexy, (int)indexz, activeBlockType, pos);
                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            if (indexy == 0)
            {
                if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)indexx, (int)height - 1, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)indexx, (int)height - 1, (int)indexz, activeBlockType, pos);
                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            if (indexy == height - 1)
            {
                if (componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)indexx, (int)0, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)indexx, (int)0, (int)indexz, activeBlockType, pos);
                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            if (indexz == 0)
            {
                if (componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)indexx, (int)indexy, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)indexx, (int)indexy, (int)depth - 1, activeBlockType, pos);
                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            if (indexz == depth - 1)
            {
                if (componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)indexx, (int)indexy, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)indexx, (int)indexy, (int)0, activeBlockType, pos);
                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }
















            //neighboorTiles
            if (indexx == 0 && indexy == 0 && indexz > 0 && indexz < depth - 1)
            {
                //already checked
                /*if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z);

                    if (adjacentChunk.GetByte((int)width - 1, (int)indexy, (int)indexz) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)indexy, (int)indexz, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }

                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)indexz, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                /*if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);

                    if (adjacentChunk.GetByte((int)indexx, (int)height - 1, (int)indexz) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)indexx, (int)height - 1, (int)indexz, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }
            }
            if (indexx == 0 && indexy == 0 && indexz == 0)
            {
                /*if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X , (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);

                    if (adjacentChunk.GetByte((int)width-1, (int)height - 1, (int)depth-1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }


                if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                /*
                if (componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);

                    if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }

                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }










            if (indexx == 0 && indexy == 0 && indexz == depth - 1)
            {
                /*if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);

                    if (adjacentChunk.GetByte((int)indexx, (int)height - 1, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)indexx, (int)height - 1, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }

                if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                /*
                if (componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);

                    if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);
                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }

                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);
                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);
                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }




            if (indexx == 0 && indexz == 0 && indexy > 0 && indexy < height - 1)
            {
                /*if (componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z- useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);

                    if (adjacentChunk.GetByte((int)width - 1, (int)indexz, (int)depth-1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)indexz, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }

                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)indexy, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)indexy, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }
            /*if (indexx == 0 && indexz == 0 && indexy == 0)
            {

            }
            if (indexx == 0 && indexz == 0 && indexy == height - 1)
            {
                if (componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            if (indexz == 0 && indexy == 0 && indexx > 0 && indexx < width - 1)
            {

                if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)indexx, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)indexx, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

            }
            /*if (indexz == 0 && indexy == 0 && indexx == 0)
            {

            }
            if (indexz == 0 && indexy == 0 && indexx == width - 1)
            {
                if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)0, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)0, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }



                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)0, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            if (indexx == width - 1 && indexy == 0 && indexz > 0 && indexz < depth - 1)
            {
                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)0, (int)height - 1, (int)indexz, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }
            /*if (indexx == width - 1 && indexy == 0 && indexz == 0)
            {

            }
            if (indexx == width - 1 && indexy == 0 && indexz == depth - 1)
            {
                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)0, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)0, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }


                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)0, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            if (indexx == 0 && indexz == depth - 1 && indexy > 0 && indexy < height - 1)
            {

                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)indexy, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)indexy, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }
            if (indexx == 0 && indexz == depth - 1 && indexy == 0)
            {
                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }




            }
            if (indexx == 0 && indexz == depth - 1 && indexy == height - 1)
            {
                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)0, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                if (componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)0, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)0, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

            }
            if (indexz == 0 && indexy == height - 1 && indexx > 0 && indexx < width - 1)
            {
                if (componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)indexx, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)indexx, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }
            if (indexz == 0 && indexy == height - 1 && indexx == 0)
            {
                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                if (componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)width - 1, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }
            if (indexz == 0 && indexy == height - 1 && indexx == width - 1)
            {
                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)0, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)0, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                if (componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)0, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)0, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)0, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)0, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            if (indexx == width - 1 && indexy == height - 1 && indexz > 0 && indexz < depth - 1)
            {
                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)0, (int)0, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)0, (int)0, (int)indexz, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }
            /*if (indexx == width - 1 && indexy == height - 1 && indexz == 0)
            {

            }
            if (indexx == width - 1 && indexy == height - 1 && indexz == depth - 1)
            {
                if (componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)0, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)0, (int)0, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)0, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)0, (int)0, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)0, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)0, (int)0, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }


            if (indexx == width - 1 && indexz == depth - 1 && indexy > 0 && indexy < height - 1)
            {
                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)0, (int)indexy, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)0, (int)indexy, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }
            if (indexx == width - 1 && indexz == depth - 1 && indexy == 0)
            {
                if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)0, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)0, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)0, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }
            /*if (indexx == width - 1 && indexz == depth - 1 && indexy == height - 1)
            {

            }


            if (indexz == depth - 1 && indexy == height - 1 && indexx > 0 && indexx < width - 1)
            {
                if (componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.GetByte((int)indexx, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.SetByte((int)indexx, (int)0, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            /*if (indexz == depth - 1 && indexy == height - 1 && indexx == 0)
            {

            }*/
        /*if (indexz == depth - 1 && indexy == height - 1 && indexx == width - 1)
        {

        }*/

        /*for (int x = -1; x < 1; x++)
        {
            for (int y = -1; y < 1; y++)
            {
                for (int z = -1; z < 1; z++)
                {

                }
            }
        }
        }*/

        public bool IsTransparent(int _x, int _y, int _z)
        {
            if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= width) || (_y >= height) || (_z >= depth)) return true;
            return map[_x + width * (_y + height * _z)] == 0; //_chunkArray
        }

        int getChunkByte(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < width && _y < height && _z < depth)
            {
                return map[_x + width * (_y + height * _z)]; //_chunkArray
            }
            return 0;
        }


        int getTempArrayByte(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < width && _y < height && _z < depth)
            {
                return _tempChunkArray[_x + width * (_y + height * _z)];
            }
            return 0;
        }



        int getChunkVertexByte(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
            {
                return _chunkVertexArray[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
            }
            return 0;
        }

        public bool blockExistsInArray(int _x, int _y, int _z)
        {
            if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= width) || (_y >= height) || (_z >= depth))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public void SetByte(int x, int y, int z, int block, Vector3 chunkbytepos_)
        {
            /*if (addfracturedcubeonimpact == 1)
            {
                //var unityTutorialObjectPool = this.GameObject.GetComponent<NewObjectPoolerScript>();
                var UnityTutorialPooledObject = UnityTutorialGameObjectPool.GetPooledObject();
                UnityTutorialPooledObject.transform.position = chunkbytepos_;
                UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                UnityTutorialPooledObject.SetActive(true);
            }*/

            if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
            {
                //Debug.Log("out of range");
                return;
            }

            int indexOf = x + width * (y + depth * z);
            map[indexOf] = block;
        }


        public int GetByte(int x, int y, int z)
        {
            if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
            {
                return 0;
            }

            int indexOf = x + width * (y + depth * z);
            return map[indexOf];
            //return map[x + width * (y + depth * z)];
        }
    }
}













//Vector3 current_start_move_pos = new Vector3(x,y,z);
//float distance = Vector3.Distance(current_target_pos, center);

/*if(_spike_max_length < 50)
{
    _spike_max_length = 50;
}*/





/*int xpos = (int)Math.Round(current_start_move_pos.X);
int ypos = (int)Math.Round(current_start_move_pos.Y);
int zpos = (int)Math.Round(current_start_move_pos.Z);

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

var _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

if (_index >= 0 && _index < _max)
{
    //Console.WriteLine("test");
    map[_index] = 1;
}*/



//float distFromGoalTarget =





//List<KeyValuePair<Vector3, float>> _dictionary = new List<KeyValuePair<Vector3, float>>();


//KeyValuePair<Vector3, float> _keyvaluepair = new KeyValuePair<Vector3, float>(current_spike_neighboor_pos, dist);
//_dictionary.Add(_keyvaluepair);
/*_dictionary.Sort(
    delegate (KeyValuePair<Vector3, float> pair1,
    KeyValuePair<Vector3, float> pair2)
    {
        return pair1.Value.CompareTo(pair2.Value);
    }
);

var key = _dictionary.FirstOrDefault();
current_target_pos = key.Key;*/







/*xpos = (int)Math.Round(current_start_move_pos.X);
ypos = (int)Math.Round(current_start_move_pos.Y);
zpos = (int)Math.Round(current_start_move_pos.Z);

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

_index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

if (_index >= 0 && _index < _max)
{
    map[_index] = 1;
}*/


/*current_start_move_pos = current_start_move_pos + (_spike_direction * 1);

int xpos = (int)Math.Round(current_start_move_pos.X);
int ypos = (int)Math.Round(current_start_move_pos.Y);
int zpos = (int)Math.Round(current_start_move_pos.Z);

if (current_target_pos.X == xpos && current_target_pos.Y == ypos && current_target_pos.Z == zpos)
{
    break;
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

var _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

if (_index >= 0 && _index < _max)
{
    map[_index] = 1;
}*/



/*for (int xx = -1; xx < 1; xx++)
{
    for (int yy = -1; yy < 1; yy++)
    {
        for (int zz = -1; zz < 1; zz++)
        {
            xpos = xpos + xx;
            ypos = ypos + yy;
            zpos = zpos + zz;

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

            int _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

            if (_index >= 0 && _index < _max)
            {
                map[_index] = 1;
            }
        }
    }
}*/





//List<float> _list = new List<float>();
//Dictionary<Vector3,float> _dictionary = new Dictionary<Vector3, float>();
//var total = (1 + 1) * (1 + 1) * (1 + 1);

/*int _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

if (_index >= 0 && _index < _max)
{
    //Console.WriteLine("test");
    map[_index] = 1;
}*/

//current_start_move_pos = current_start_move_pos + (_spike_direction * 1.4f);

/*int xpos = (int)Math.Round(current_start_move_pos.X);
int ypos = (int)Math.Round(current_start_move_pos.Y);
int zpos = (int)Math.Round(current_start_move_pos.Z);

if (current_target_pos.X == xpos && current_target_pos.Y == ypos && current_target_pos.Z == zpos ||
    Vector3.Distance(new Vector3(xpos,ypos,zpos), center) > _spike_max_length-1)
{
    break;
}

List<KeyValuePair<Vector3, float>> _dictionary = new List<KeyValuePair<Vector3, float>>();

for (int xx = -1; xx <= 1; xx++)
{
    for (int yy = -1; yy <= 1; yy++)
    {
        for (int zz = -1; zz <= 1; zz++)
        {
            xpos = xpos + xx;
            ypos = ypos + yy;
            zpos = zpos + zz;

            var current_spike_neighboor_pos = new Vector3(xpos, ypos, zpos);
            float dist = _check_node_distance(current_target_pos, current_spike_neighboor_pos);
            KeyValuePair<Vector3, float> _keyvaluepair = new KeyValuePair<Vector3, float>(current_spike_neighboor_pos, dist);
            _dictionary.Add(_keyvaluepair);

            if (current_target_pos.X == xpos && current_target_pos.Y == ypos && current_target_pos.Z == zpos)
            {
                break;
            }
        }
    }
}

_dictionary.Sort(
    delegate (KeyValuePair<Vector3, float> pair1,
    KeyValuePair<Vector3, float> pair2)
    {
        return pair1.Value.CompareTo(pair2.Value);
    }
);

var key = _dictionary.FirstOrDefault();
current_start_move_pos = key.Key;

xpos = (int)Math.Round(key.Key.X);
ypos = (int)Math.Round(key.Key.Y);
zpos = (int)Math.Round(key.Key.Z);

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

var _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

if (_index >= 0 && _index < _max)
{
    //Console.WriteLine("test");
    map[_index] = 1;
}*/



/*for (int xx = -1; xx < 1; xx++)
{
    for (int yy = -1; yy < 1; yy++)
    {
        for (int zz = -1; zz < 1; zz++)
        {
            //xpos = xpos + xx;
            //ypos = ypos + yy;
            //zpos = zpos + zz;
            xpos = (int)Math.Round(key.Key.X + xx);
            ypos = (int)Math.Round(key.Key.Y + yy);
            zpos = (int)Math.Round(key.Key.Z + zz);

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

            var _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

            if (_index >= 0 && _index < _max)
            {
                //Console.WriteLine("test");
                map[_index] = 1;
            }
        }
    }
}*/









/*

for (int xx = -1; xx < 1; xx++)
{
    for (int yy = -1; yy < 1; yy++)
    {
        for (int zz = -1; zz < 1; zz++)
        {
            //xpos = xpos + xx;
            //ypos = ypos + yy;
            //zpos = zpos + zz;
            xpos = (int)Math.Round(key.Key.X + xx);
            ypos = (int)Math.Round(key.Key.Y + yy);
            zpos = (int)Math.Round(key.Key.Z + zz);

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

            var _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

            if (_index >= 0 && _index < _max)
            {
                //Console.WriteLine("test");
                map[_index] = 1;
            }
        }
    }
}*/















//current_start_move_pos = current_start_move_pos + (_spike_direction * 1.4f);

//why did i even need that now? keeping this for later :)
//_check_node_distance();


/*int xpos = (int)Math.Round(current_spike_position.X);
int ypos = (int)Math.Round(current_spike_position.Y);
int zpos = (int)Math.Round(current_spike_position.Z);

_index = xpos + (ChunkWidth) * (ypos + (ChunkHeight) * zpos);

if (_index >= 0 && _index < _max)
{
    map[_index] = 1;
}*/



//List<float> _list = new List<float>();
//Dictionary<Vector3,float> _dictionary = new Dictionary<Vector3, float>();
//List<KeyValuePair<Vector3, float>> _dictionary = new List<KeyValuePair<Vector3, float>>();
//var total = (1 + 1) * (1 + 1) * (1 + 1);

/*int _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

if (_index >= 0 && _index < _max)
{
    //Console.WriteLine("test");
    map[_index] = 1;
}*/

/*for (int xx = -1; xx < 1; xx++)
{
    for (int yy = -1; yy < 1; yy++)
    {
        for (int zz = -1; zz < 1; zz++)
        {
            xpos = xpos + xx;
            ypos = ypos + yy;
            zpos = zpos + zz;

            var current_spike_neighboor_pos = new Vector3(xpos, ypos, zpos);
            float dist = _check_node_distance(center, current_spike_neighboor_pos);
            KeyValuePair<Vector3, float> _keyvaluepair = new KeyValuePair<Vector3, float>(current_spike_neighboor_pos, dist);
            //_keyvaluepair.Key = current_spike_neighboor_pos;
            //_keyvaluepair.Value = current_spike_neighboor_pos;
            //_dictionary.Add(_keyvaluepair);          


            int _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

            if (_index >= 0 && _index < _max)
            {
                //Console.WriteLine("test");
                map[_index] = 1;
            }
        }
    }
}*/


//List<KeyValuePair<Vector3, float>> myList = _dictionary.ToList();
/*_dictionary.Sort(
    delegate (KeyValuePair<Vector3, float> pair1,
    KeyValuePair<Vector3, float> pair2)
    {
        return pair1.Value.CompareTo(pair2.Value);
    }
);

foreach (var val in _dictionary)
{
    //Console.WriteLine(val);
}
var key = _dictionary.Where(pair => pair.Value == 0).Select(pair => pair.Key).FirstOrDefault();
*/
/*for (int xx = -1; xx < 1; xx++)
{
    for (int yy = -1; yy < 1; yy++)
    {
        for (int zz = -1; zz < 1; zz++)
        {
            xpos = (int)Math.Round(key.X + xx);
            ypos = (int)Math.Round(key.Y + yy);
            zpos = (int)Math.Round(key.Z + zz);

            int _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

            if (_index >= 0 && _index < _max)
            {
                //Console.WriteLine("test");
                map[_index] = 1;
            }
        }
    }
}*/

/*int _index = (int)Math.Round(key.X + (ChunkWidth_L + ChunkWidth_R + 1) * (key.Y + (ChunkHeight_L + ChunkHeight_R + 1) * key.Z));

if (_index >= 0 && _index < _max)
{
    //Console.WriteLine("test");
    map[_index] = 1;
}*/









//IEnumerator _enum = _dictionary.GetEnumerator();
//var _current = (KeyValuePair<Vector3, float>)_enum.Current;

//var lowestorhighest = _list[0];








/*_dictionary = _dictionary.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

IEnumerator _enum = _dictionary.GetEnumerator();

var _current = (KeyValuePair<Vector3, float>)_enum.Current;
for (int xx = -1; xx < 1; xx++)
{
    for (int yy = -1; yy < 1; yy++)
    {
        for (int zz = -1; zz < 1; zz++)
        {
            xpos = (int)Math.Round(_current.Key.X + xx);
            ypos = (int)Math.Round(_current.Key.Y + yy);
            zpos = (int)Math.Round(_current.Key.Z + zz);

            _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

            if (_index >= 0 && _index < _max)
            {
                //Console.WriteLine("test");
                map[_index] = 1;
            }
        }
    }
}*/



/*
List<KeyValuePair<Vector3, float>> myList = _dictionary.ToList();
myList.Sort(
    delegate (KeyValuePair<Vector3, float> pair1,
    KeyValuePair<Vector3, float> pair2)
    {
        return pair1.Value.CompareTo(pair2.Value);
    }
);


var lowestorhighest =_list[0];*/







//_list.Sort();







/*
_index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);
if (_index >= 0 && _index < _max)
{
    //Console.WriteLine("test");
    map[_index] = 1;
}*/


//_spike_direction = current_spike_position - center;
//_spike_direction.Normalize();
//var length = Math.Sqrt((current_spike_position.X * current_spike_position.X) + (current_spike_position.Y * current_spike_position.Y) + (current_spike_position.Z * current_spike_position.Z));
//var _side_z = ((ChunkWidth * ChunkWidth) + (ChunkHeight * ChunkHeight)) * planeSize;
//var _side_z = (current_spike_position.X * current_spike_position.X) + (current_spike_position.Y* current_spike_position.Y);

/*current_spike_position = current_spike_position + (_spike_direction * 1.4f);

int xpos = (int)Math.Round(current_spike_position.X);
int ypos = (int)Math.Round(current_spike_position.Y);
int zpos = (int)Math.Round(current_spike_position.Z);
_index = xpos + (ChunkWidth) * (ypos + (ChunkHeight) * zpos);

if (_index >= 0 && _index < _max)
{
    map[_index] = 1;
}*/



/*int xpos = (int)Math.Round(current_spike_position.X);
int ypos = (int)Math.Round(current_spike_position.Y);
int zpos = (int)Math.Round(current_spike_position.Z);

_index = xpos + (ChunkWidth) * (ypos + (ChunkHeight) * zpos);

if (_index >= 0 && _index < _max)
{
    map[_index] = 1;
}

xpos = (int)Math.Floor(current_spike_position.X);
ypos = (int)Math.Floor(current_spike_position.Y);
zpos = (int)Math.Floor(current_spike_position.Z);

_index = xpos + (ChunkWidth) * (ypos + (ChunkHeight) * zpos);

if (_index >= 0 && _index < _max)
{
    map[_index] = 1;
}

xpos = (int)Math.Ceiling(current_spike_position.X);
ypos = (int)Math.Ceiling(current_spike_position.Y);
zpos = (int)Math.Ceiling(current_spike_position.Z);

_index = xpos + (ChunkWidth) * (ypos + (ChunkHeight) * zpos);

if (_index >= 0 && _index < _max)
{
    map[_index] = 1;
}
_index = xpos + (ChunkWidth) * (ypos + (ChunkHeight) * zpos);*/

/*int xpos = (int)Math.Round(current_spike_position.X);
int ypos = (int)Math.Round(current_spike_position.Y);
int zpos = (int)Math.Round(current_spike_position.Z);

_index = xpos + (ChunkWidth) * (ypos + (ChunkHeight) * zpos);

*/
/*for (int xx = -1; xx < 1; xx++)
{
    for (int yy = -1; yy < 1; yy++)
    {
        for (int zz = -1; zz < 1; zz++)
        {
            xpos = xpos + xx;
            ypos = ypos + yy;
            zpos = zpos + zz;

            _index = xpos + (ChunkWidth) * (ypos + (ChunkHeight) * zpos);

            if (_index >= 0 && _index < _max)
            {
                //Console.WriteLine("test");
                map[_index] = 1;
            }
        }
    }
}*/

/*
if (_index >= 0 && _index < _max)
{


}
else
{
    //break;
}*/


/*if (_spike_max_length < _max * 0.475f) //depending on where the spike is started.
{
    _spike_max_length = (int)Math.Round(_max * 0.475f);// (int)Math.Round(max - min + 2);
}*/

//Console.WriteLine(_spike_max_length);




/*public float sc_check_node(Vector3 nodeA, Vector3 nodeB)
{

    var dstX = Math.Abs((nodeA.X) - (nodeB.X));
    var dstY = Math.Abs((nodeA.Y) - (nodeB.Y));
    var dstZ = Math.Abs((nodeA.Z) - (nodeB.Z));

    if (dstX >= dstY && dstX >= dstZ)
    {
        return 14 * dstY + 14 * dstZ + (10 * ((dstX - dstZ) + (dstX - dstY)));
    }
    else if (dstX >= dstY && dstX < dstZ)
    {
        return 14 * dstY + 14 * dstZ + (10 * ((dstX - dstZ) + (dstX - dstY)));
    }



    var dstX = Math.Abs((nodeA.X) - (nodeB.X));
    var dstZ = Math.Abs((nodeA.Y) - (nodeB.Y));

    if (dstX > dstZ)
    {
        return 14 * dstZ + 10 * (dstX - dstZ);
    }
    return 14 * dstX + 10 * (dstZ - dstX);
}*/
