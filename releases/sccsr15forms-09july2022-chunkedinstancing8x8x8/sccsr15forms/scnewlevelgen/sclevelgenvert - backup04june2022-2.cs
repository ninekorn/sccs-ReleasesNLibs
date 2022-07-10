﻿using System;
using System.Collections.Generic;
using SharpDX;

using SimplexNoise;

using System.Collections;

namespace sccsr15forms
{
    public class sclevelgenvert
    {
        public sclevelgenvert(int x, int y, int z, float[] newchunkpos_)
        {
            chunkPos = new int[3];

            chunkPos[0] = x;
            chunkPos[1] = y;
            chunkPos[2] = z;

            newchunkpos = newchunkpos_;
        }


        public int indexinlevelgenmap;
        public float[] newchunkpos;

        int arrayindex;

        //public int[] map;
        public int[] map;
        private int block;

        private Vector4 forward = new Vector4(0, 0, 1, 0);
        private Vector4 back = new Vector4(0, 0, -1, 0);
        private Vector4 right = new Vector4(1, 0, 0, 0);
        private Vector4 left = new Vector4(-1, 0, 0, 0);
        private Vector4 up = new Vector4(0, 1, 0, 0);
        private Vector4 down = new Vector4(0, -1, 0, 0);




        private Vector4 topfacecolor = new Vector4(0, 1, 1, 1);
        private Vector4 leftfacecolor = new Vector4(1, 0, 0, 1);
        private Vector4 rightfacecolor = new Vector4(0, 1, 0, 1);
        private Vector4 frontfacecolor = new Vector4(0, 0, 1, 1);
        private Vector4 backfacecolor = new Vector4(1, 1, 0, 1);
        private Vector4 bottomfacecolor = new Vector4(1, 0, 1, 1);





        //public List<sclevelgenclass.DVertex> vertexlist = new List<sclevelgenclass.DVertex>(); //listOfVerts
        public List<tutorialcubeaschunk.DVertex> vertexlist = new List<tutorialcubeaschunk.DVertex>(); //listOfVerts

        public List<int> listOfTriangleIndices = new List<int>();

        //public List<Vector3> vertexlist;
        //public List<Vector3> normalslist;
        //public List<Vector4> colorslist;
        //public List<Vector4> indexposlist;
        //public List<Vector2> textureslist;


        private int _detailScale = 200;
        private int _heightScale = 5;



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

        int[][] somearrayofcoords = new int[6][];
        int[][] somearrayofcoordsfloor = new int[6][];

        //public List<int> triangles;
        public int[] _chunkArray;
        public int[] _tempChunkArray;


        public int[] _tempChunkArrayRightFace;
        public int[] _tempChunkArrayLeftFace;
        public int[] _tempChunkArrayFrontFace;
        public int[] _tempChunkArrayBackFace;
        public int[] _tempChunkArrayBottomFace;


        public int[] _chunkVertexArray0;
        public int[] _chunkVertexArray1;
        public int[] _chunkVertexArray2;
        public int[] _chunkVertexArray3;
        public int[] _chunkVertexArray4;
        public int[] _chunkVertexArray5;


        public int[] _testVertexArray0;
        public int[] _testVertexArray1;
        public int[] _testVertexArray2;
        public int[] _testVertexArray3;
        public int[] _testVertexArray4;
        public int[] _testVertexArray5;




        public int activeBlockType;
        //public float planeSize;
        public int[] chunkPos;
        public float realplanetwidth;

        int total;
        int totalints;
        int vertexlistWidth;
        int vertexlistHeight;
        int vertexlistDepth;


        int counterCreateChunkObjectFacesints;// 
        int tints;// 
        int posxints;// 
        int posyints;// 
        int poszints;// 
        int xxints;// 
        int yyints;// 
        int zzints;// 
        int xiints;// 
        int yiints;// 
        int ziints;// 

        int swtchxints;// 
        int swtchyints;// 
        public int swtchzints;// 

        int rowIterateXints;// 
        int rowIterateZints;// 
        int rowIterateYints;// 
        public int chunkbuildingswtc;
        //public void sccsCustomStart(Transform planetchunk_, Vector3 chunkpos_, float planeSize_, float realplanetwidth_, int width_, int height_, int depth_, sccsproceduralplanetbuilderGen2 componentparent_, int addfracturedcubeonimpact_, NewObjectPoolerScript UnityTutorialGameObjectPool_)

        int _maxWidth;// = 0;
        int _maxHeight;
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
        //OriginalSimplexNoise fastNoise = new OriginalSimplexNoise();

        FastNoise fastNoise = new FastNoise();


        float staticPlaneSize;
        float alternateStaticPlaneSize;
        private int seed = 3420; // 3420

        int padding0;
        int padding1;
        int padding2;

        int numberOfObjectInWidth; int numberOfObjectInHeight; int numberOfObjectInDepth; int numberOfInstancesPerObjectInWidth; int numberOfInstancesPerObjectInHeight; int numberOfInstancesPerObjectInDepth; float planeSize;

        //int width; int height; int depth;

        int voxeltype;
        int fullface;

        public int width = 0;
        public int height = 0;
        public int depth = 0;

        //public float floorHeight = 10;

        public int[] leftExtremity;
        public int[] rightExtremity;
        public int[] frontExtremity;
        public int[] backExtremity;

        public int[] leftInsideCornerExtremity;
        public int[] rightInsideCornerExtremity;
        public int[] frontInsideCornerExtremity;
        public int[] backInsideCornerExtremity;

        public int[] leftOutsideCornerExtremity;
        public int[] rightOutsideCornerExtremity;
        public int[] frontOutsideCornerExtremity;
        public int[] backOutsideCornerExtremity;


        //public static float xChunkPos;
        //public static float yChunkPos;
        //public static float zChunkPos;
        //, out Vector3[] norms, out Vector2[] tex
        //, out Vector4[] vertexArray

        //public static Dictionary<sclevelgenvert, Vector3> chunkz = new Dictionary<sclevelgenvert, Vector3>();

        int typeofterraintiles;
        sclevelgenvert[] listofchunksadjacent = new sclevelgenvert[6];
        sclevelgenvert[] listofchunksadjacentfloor = new sclevelgenvert[6];


        public tutorialcubeaschunk.DVertex[] arrayofverts;
        public int[] arrayoftrigs;

        float levellimitroofy = 5.0f;
        float levellimitfloory = 0.0f;
        // out Vector3[] norms, out Vector2[] tex
        Vector4 chunkoriginpos;
        public int levelofdetail = 1;
        public float levelofdetailmul = 1.0f;
        private int detailScale = 10; // 10
        private int HeightScale = 200; //200

        int facetype;
        tutorialcubeaschunk componentparent;

        //public Vector3 realpos;
        public int X;
        public int Y;
        public int Z;




        float levelgenmapsplanesize;

        //Vector3 currentPosition, Vector3 _chunkPos, 
        public int[] buildchunkmaps(int typeofterraintiles_, tutorialcubeaschunk componentparent_, int levelofdetail_) //, out int vertexNum, out int indicesNum
        //public void startBuildingArray(Vector4 currentPosition, out sclevelgenchunk.DVertex[] vertexArray, out int[] triangleArray, out int[] mapper, int padding0_, int padding1_, int padding2_, int numberOfObjectInWidth_, int numberOfObjectInHeight_, int numberOfObjectInDepth_, int numberOfInstancesPerObjectInWidth_, int numberOfInstancesPerObjectInHeight_, int numberOfInstancesPerObjectInDepth_, int width_, int height_, int depth_, float planeSize_, sclevelgenchunkPrim componentparentprim_, sclevelgenchunk componentparentinstance_, sclevelgenchunk_instances componentparentthischunk_, int fullface_, int voxeltype_)
        {

            typeofterraintiles = typeofterraintiles_;


            componentparent = componentparent_;


            levelofdetail = levelofdetail_;
            //chunkz.Add(this, _chunkPos);


            levellimitfloory = 0;

            if (levelofdetail == 1)
            {
                width = componentparent.somelevelgenprimglobals.widthlod0;
                height = componentparent.somelevelgenprimglobals.heightlod0;
                depth = componentparent.somelevelgenprimglobals.depthlod0;

                //width = 10;
                //height = 10;
                //depth = 10;
            }
            else if (levelofdetail == 2)
            {
                width = componentparent.somelevelgenprimglobals.widthlod1;
                height = componentparent.somelevelgenprimglobals.heightlod1;
                depth = componentparent.somelevelgenprimglobals.depthlod1;

                //width = 5;
                //height = 5;
                //depth = 5;
            }

            else if (levelofdetail == 3)
            {
                width = componentparent.somelevelgenprimglobals.widthlod2;
                height = componentparent.somelevelgenprimglobals.heightlod2;
                depth = componentparent.somelevelgenprimglobals.depthlod2;
                //width = 3;
                //height = 3;
                //depth = 3;
            }
            else if (levelofdetail == 4)
            {
                width = componentparent.somelevelgenprimglobals.widthlod3;
                height = componentparent.somelevelgenprimglobals.heightlod3;
                depth = componentparent.somelevelgenprimglobals.depthlod3;

                //width = 2;
                //height = 2;
                //depth = 2;
            }
            else if (levelofdetail == 5)
            {
                width = componentparent.somelevelgenprimglobals.widthlod4;
                height = componentparent.somelevelgenprimglobals.heightlod4;
                depth = componentparent.somelevelgenprimglobals.depthlod4;

                //width = 2;
                //height = 2;
                //depth = 2;
            }

            /*

            if (levelofdetail == 1)
            {
                width = 10;
                height = 10;
                depth = 10;
            }
            else if (levelofdetail == 2)
            {
                width = 5;
                height = 5;
                depth = 5;
            }

            else if (levelofdetail == 3)
            {
                width = 3;
                height = 3;
                depth = 3;
            }
            else if (levelofdetail == 4)
            {
                width = 2;
                height = 2;
                depth = 2;
            }

            */

            /*
            if (levelofdetail == 1)
            {
                width = 6;
                height = 6;
                depth = 6;
            }
            else if (levelofdetail == 2)
            {
                width = 3;
                height = 3;
                depth = 3;
            }

            else if (levelofdetail == 3)
            {
                width = 2;
                height = 2;
                depth = 2;
            }
            else if (levelofdetail == 4)
            {
                width = 1;
                height = 1;
                depth = 1;
            }*/

            /*
            if (levelofdetail == 1)
            {
                width = 20;
                height = 20;
                depth = 20;
            }
            else if (levelofdetail == 2)
            {
                width = 10;
                height = 10;
                depth = 10;
            }

            else if (levelofdetail == 3)
            {
                width = 6;
                height = 6;
                depth = 6;
            }
            else if (levelofdetail == 4)
            {
                width = 5;
                height = 5;
                depth = 5;
            }*/




            /*
            width = width_ / levelofdetail_;
            height = height_ / levelofdetail_;
            depth = depth_ / levelofdetail_;*/



            //chunkPos = _chunkPos;//

            /*
            chunkPos.X *= 2f;
            chunkPos.Y *= 2f;
            chunkPos.Z *= 2f;*/










            //xChunkPos = _chunkPos.X;
            //yChunkPos = _chunkPos.Y;
            //zChunkPos = _chunkPos.Z;


            //floorHeight = height;



            voxeltype = 0;



            float standardwidth = 10.0f;
            float currentratio = standardwidth / width;

            //Console.WriteLine(currentratio);
            levelgenmapsplanesize = 0.1f * currentratio; //0.05f when 20w20h20d



            staticPlaneSize = levelgenmapsplanesize;

            if (staticPlaneSize == 1)
            {
                staticPlaneSize = levelgenmapsplanesize * 0.1f;
                alternateStaticPlaneSize = levelgenmapsplanesize * 0.1f;
            }
            else if (staticPlaneSize == 0.1f)
            {
                staticPlaneSize = levelgenmapsplanesize;
                alternateStaticPlaneSize = levelgenmapsplanesize * 10;
            }
            else if (staticPlaneSize == 0.01f)
            {
                staticPlaneSize = levelgenmapsplanesize;
                alternateStaticPlaneSize = levelgenmapsplanesize * 1000;
            }
            else if (staticPlaneSize == 0.2f)
            {
                staticPlaneSize = levelgenmapsplanesize;
                alternateStaticPlaneSize = levelgenmapsplanesize * 10;
            }
            else if (staticPlaneSize == 0.02f)
            {
                staticPlaneSize = levelgenmapsplanesize;
                alternateStaticPlaneSize = levelgenmapsplanesize * 1000;
            }







            // new Vector3(currentPosition.X, currentPosition.Y, currentPosition.Z);


            //planeSize = planeSize;
            //realplanetwidth = 4;
            //width = width;
            //height = height;
            //depth = depth;









            //componentparent = componentparent_;
            //addfracturedcubeonimpact = addfracturedcubeonimpact_;
            //UnityTutorialGameObjectPool = UnityTutorialGameObjectPool_;

            // this.GameObject.position;

            /*
            this.gameObject.tag = "collisionObject";
            this.gameObject.layer = 8; //"collisionObject"
            UnityTutorialGameObjectPool = this.GameObject.GetComponent<NewObjectPoolerScript>();

            parentObject = this.GameObject.parent;
            //componentparent = parentObject.gameObject.GetComponent<sccsproceduralplanetbuilderGen2>().currentplanetbuilder;

            mesh = new Mesh();
            this.gameObject.GetComponent<MeshFilter>().mesh = mesh;
            this.gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
            */

            map = new int[width * height * depth];

            //normalslist = new List<Vector3>();
            //colorslist = new List<Vector4>();
            //indexposlist = new List<Vector4>();
            //textureslist = new List<Vector2>();



            //STALAGMITE CAVE SYSTEM VALUES FOR CEILING 
            //DETAILSCALE = 10 // 20 // 75.0fgood
            //HEIGHTSCALE = 1.25f //1.25f //4.55fgood

            float _detailScaleceiling = 200.0f;//200
            float _heightScaleceiling = 2.0f;//5

            float _detailScale = 200;//200
            float _heightScale = 5;//5

            float somenoisevalue = 10.0f;
            float someothernoisevalue = 20.0f;


            //realplanetwidth = planeSize * width;

            //map = new int[width * height * depth];

            float somenoiseval0 = 200; //200
            float somenoiseval1 = 5; //5

            var seed0 = 3420;


            if (typeofterraintiles == 0)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int z = 0; z < depth; z++)
                        {

                            //map[x + width * (y + height * z)] = 1;
                            //map[x + width * (y + height * z)] = 1;


                            /*float noiseXZ = 20;

                            noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (currentPosition.X * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (currentPosition.Y * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (currentPosition.Z * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);
                            Console.WriteLine(noiseXZ);
                            if (noiseXZ >= 0.2f)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else if (y == 0 && currentPosition.Y == 0)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                                map[x + width * (y + height * z)] = 0;
                            }*/

                            float noiseXZ = somenoisevalue;
                            //noiseXZ *= OriginalSimplexNoise.Noise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z* levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale);
                            noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z * levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale, 15, 15, 0);


                            if (chunkPos[1] == LevelGenerator4.wallheightsize - 1)
                            {
                                //Console.WriteLine("noiseXZ" + noiseXZ);
                                if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                {
                                    map[x + width * ((height - 1 - y) + height * z)] = 1;
                                }
                                /*else if (y == height - 1)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                else
                                {
                                    map[x + width * ((height - 1 - y) + height * z)] = 0;
                                }*/
                            }

                            float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                            noiseXZ -= size0;
                            //noise = (noise + 1.0f) * 0.5f;
                            //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                            //float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                            //noise -= size0;
                            //Console.WriteLine(noiseXZ + " y: " + y);

                            if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                            {
                                map[x + width * (y + height * z)] = 1;
                            }

                            if (y == 0 && chunkPos[1] == 0)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            if (y == height - 1 && chunkPos[1] == LevelGenerator4.wallheightsize - 1)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }

                            /*else
                            {
                                map[x + width * (y + height * z)] = 0;
                            }*/

                            //map[x + width * (y + height * z)] = 1;
                        }
                    }
                }
            }



            if (typeofterraintiles != 1115 && typeofterraintiles != 0 && chunkPos[1] != 0)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int z = 0; z < depth; z++)
                        {

                            //map[x + width * (y + height * z)] = 1;
                            //map[x + width * (y + height * z)] = 1;


                            /*float noiseXZ = 20;

                            noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (currentPosition.X * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (currentPosition.Y * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (currentPosition.Z * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);
                            Console.WriteLine(noiseXZ);
                            if (noiseXZ >= 0.2f)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else if (y == 0 && currentPosition.Y == 0)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                                map[x + width * (y + height * z)] = 0;
                            }*/

                            float noiseXZ = somenoisevalue;
                            //noiseXZ *= OriginalSimplexNoise.Noise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z* levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale);
                            noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScaleceiling) * _heightScaleceiling, (((z * levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScaleceiling) * _heightScaleceiling, 15, 15, 0);


                            if (chunkPos[1] == LevelGenerator4.wallheightsize - 1)
                            {
                                //Console.WriteLine("noiseXZ" + noiseXZ);
                                if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                {
                                    map[x + width * ((height - 1 - y) + height * z)] = 1;
                                }
                                /*else if (y == height - 1)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                else
                                {
                                    map[x + width * ((height - 1 - y) + height * z)] = 0;
                                }*/
                            }

                            float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                            noiseXZ -= size0;
                            //noise = (noise + 1.0f) * 0.5f;
                            //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                            //float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                            //noise -= size0;
                            //Console.WriteLine(noiseXZ + " y: " + y);

                            if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                            {
                                map[x + width * (y + height * z)] = 1;
                            }

                            if (y == 0 && chunkPos[1] == 0)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            if (y == height - 1 && chunkPos[1] == LevelGenerator4.wallheightsize - 1)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }

                            /*else
                            {
                                map[x + width * (y + height * z)] = 0;
                            }*/

                            //map[x + width * (y + height * z)] = 1;
                        }
                    }
                }
            }


            if (typeofterraintiles != 1115 && typeofterraintiles != 0 && chunkPos[1] == 0)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int z = 0; z < depth; z++)
                        {

                            //map[x + width * (y + height * z)] = 1;
                            //map[x + width * (y + height * z)] = 1;


                            /*float noiseXZ = 20;

                            noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (currentPosition.X * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (currentPosition.Y * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (currentPosition.Z * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);
                            Console.WriteLine(noiseXZ);
                            if (noiseXZ >= 0.2f)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else if (y == 0 && currentPosition.Y == 0)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                                map[x + width * (y + height * z)] = 0;
                            }*/

                            float noiseXZ = somenoisevalue;
                            //noiseXZ *= OriginalSimplexNoise.Noise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z* levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale);
                            noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z * levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale, 15, 15, 0);


                            if (chunkPos[1] == LevelGenerator4.wallheightsize - 1)
                            {
                                //Console.WriteLine("noiseXZ" + noiseXZ);
                                if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                {
                                    map[x + width * ((height - 1 - y) + height * z)] = 1;
                                }
                                /*else if (y == height - 1)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                else
                                {
                                    map[x + width * ((height - 1 - y) + height * z)] = 0;
                                }*/
                            }

                            float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                            noiseXZ -= size0;
                            //noise = (noise + 1.0f) * 0.5f;
                            //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                            //float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                            //noise -= size0;
                            //Console.WriteLine(noiseXZ + " y: " + y);

                            if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                            {
                                map[x + width * (y + height * z)] = 1;
                            }

                            if (y == 0 && chunkPos[1] == 0)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            if (y == height - 1 && chunkPos[1] == LevelGenerator4.wallheightsize - 1)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }

                            /*else
                            {
                                map[x + width * (y + height * z)] = 0;
                            }*/

                            //map[x + width * (y + height * z)] = 1;
                        }
                    }
                }
            }

            if (typeofterraintiles == 1115)
            {
                int[] fakepos = chunkPos;
                fakepos[1] = 0;

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int z = 0; z < depth; z++)
                        {
                            float noiseXZ = somenoisevalue;
                            //noiseXZ *= OriginalSimplexNoise.Noise((((x * levelgenmapsplanesize) + fakepos.X + seed) / _detailScale) * _heightScale, (((z* levelgenmapsplanesize) + fakepos.Z + seed) / _detailScale) * _heightScale);
                            noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * levelgenmapsplanesize) + fakepos[0] + seed) / _detailScaleceiling) * _heightScaleceiling, (((z * levelgenmapsplanesize) + fakepos[2] + seed) / _detailScaleceiling) * _heightScaleceiling, 15, 15, 0);

                            float size0 = (1 / levelgenmapsplanesize) * fakepos[1];
                            noiseXZ -= size0;
                            //noise = (noise + 1.0f) * 0.5f;
                            //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                            //float size0 = (1 / levelgenmapsplanesize) * fakepos.Y;
                            //noise -= size0;
                            //Console.WriteLine(noiseXZ + " y: " + y);

                            if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                            {
                                map[x + width * ((height - 1 - y) + height * z)] = 1;
                            }
                            else if (y == height - 1 && fakepos[1] == 0)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }

                            else
                            {
                                map[x + width * ((height - 1 - y) + height * z)] = 0;
                            }
                        }
                    }
                }
            }










            if (typeofterraintiles == -99) //typeofterraintiles == -2
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int z = 0; z < depth; z++)
                        {
                            //map[x + width * (y + height * z)] = 1;

                            /*float noiseXZ = somenoisevalue;
                            //noiseXZ *= OriginalSimplexNoise.Noise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z* levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale);
                            noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z* levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale, 15, 15, 0);

                            float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                            noiseXZ -= size0;
                            //noise = (noise + 1.0f) * 0.5f;
                            //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                            //float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                            //noise -= size0;
                            //Console.WriteLine(noiseXZ + " y: " + y);

                            if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else if (y == 0 && chunkPos[1] == 0)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                                map[x + width * (y + height * z)] = 0;
                            }*/
                            /* if (y < width /1.15f)
                             {
                                 map[x + width * (y + height * z)] = 1;
                             }*/
                            map[x + width * (y + height * z)] = 1;
                        }
                    }
                }
            }














            if (typeofterraintiles == -99) ////typeofterraintiles == -3
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int z = 0; z < depth; z++)
                        {
                            //map[x + width * (y + height * z)] = 1;

                            /*float noiseXZ = somenoisevalue;
                            //noiseXZ *= OriginalSimplexNoise.Noise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z* levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale);
                            noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z* levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale, 15, 15, 0);

                            float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                            noiseXZ -= size0;
                            //noise = (noise + 1.0f) * 0.5f;
                            //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                            //float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                            //noise -= size0;
                            //Console.WriteLine(noiseXZ + " y: " + y);

                            if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else if (y == 0 && chunkPos[1] == 0)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                                map[x + width * (y + height * z)] = 0;
                            }*/
                            if (y < width / 1.05f)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }

                        }
                    }
                }
            }



            //TERRAIN INSIDE TILES. DOES NOT INCLUDE THE WALLS BOTTOM FLOOR THAT IS PART OF WALLS.
            //TERRAIN INSIDE TILES. DOES NOT INCLUDE THE WALLS BOTTOM FLOOR THAT IS PART OF WALLS.
            if (typeofterraintiles == 0)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int z = 0; z < depth; z++)
                        {
                            //map[x + width * (y + height * z)] = 1;


                            /*float noiseXZ = 20;

                            noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);
                            Console.WriteLine(noiseXZ);
                            if (noiseXZ >= 0.2f)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else if (y == 0 && chunkPos[1] == 0)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                                map[x + width * (y + height * z)] = 0;
                            }*/

                            float noiseXZ = somenoisevalue;
                            //noiseXZ *= OriginalSimplexNoise.Noise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z* levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale);
                            noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z * levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale, 15, 15, 0);

                            float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                            noiseXZ -= size0;
                            //noise = (noise + 1.0f) * 0.5f;
                            //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                            //float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                            //noise -= size0;
                            //Console.WriteLine(noiseXZ + " y: " + y);

                            if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else if (y == 0 && chunkPos[1] == 0)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                                map[x + width * (y + height * z)] = 0;
                            }
                            //map[x + width * (y + height * z)] = 1;
                        }
                    }
                }
            }
            //TERRAIN INSIDE TILES. DOES NOT INCLUDE THE WALLS BOTTOM FLOOR THAT IS PART OF WALLS.
            //TERRAIN INSIDE TILES. DOES NOT INCLUDE THE WALLS BOTTOM FLOOR THAT IS PART OF WALLS.








            //LEFT WALL
            if (typeofterraintiles == 1101)
            {
                //for (int j = 0; j < levelgen.leftWall.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.leftWall[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                    float noiseValue = someothernoisevalue;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);

                                    /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos[1] == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }*/
                                    //noiseValue += (10 - (float)y) / somenoisevalue;
                                    //noiseValue /= (float)y / 5;

                                    if (noiseValue > 0.2f && y < LevelGenerator4.wallheightsize - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);

                                    float noiseValue1i = noiseValue2;

                                    noiseValue1i += (5 - (float)x) / 5;
                                    noiseValue1i /= (float)x / 5;


                                    if (noiseValue1i > 0.2f)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //leftExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }

                                    if (x == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //LEFT WALL



            //RIGHT WALL
            if (typeofterraintiles == 1102)
            {
                // for (int j = 0; j < levelgen.rightWall.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.rightWall[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                for (int z = 0; z < depth; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                    float noiseValue = someothernoisevalue;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);

                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);

                                    //noiseValue += (10 - (float)y) / somenoisevalue;
                                    //noiseValue /= (float)y / 5;

                                    /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos[1] == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }*/
                                    if (noiseValue > 0.2f && y < LevelGenerator4.wallheightsize - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue3i = noiseValue2;

                                    noiseValue3i += (5 - (float)x) / 5;
                                    noiseValue3i /= (float)x / 5;

                                    if (noiseValue3i > 0.2f)
                                    {
                                        map[(width - 1 - x) + width * (y + height * z)] = 1;
                                        //rightExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }
                                    /*
                                    if (x == width-1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }*/
                                }
                            }
                        }
                    }
                }
            }
            //RIGHT WALL




            /////////////////////////////////////FRONT WALL/////////////////////////////////
            if (typeofterraintiles == 1103)
            {
                //for (int j = 0; j < levelgen.frontWall.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.frontWall[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                            float noiseX5 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);6
                                float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                    float noiseZ5 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    //noiseValue += (10 - (float)y) / somenoisevalue;
                                    //noiseValue /= (float)y / 5;
                                    float noiseValue = someothernoisevalue;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);

                                    /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos[1] == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }*/

                                    if (noiseValue > 0.2f && y < LevelGenerator4.wallheightsize - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue6i = noiseValue5;

                                    noiseValue6i += (5 - (float)z) / 5;
                                    noiseValue6i /= (float)z / 5;

                                    if (noiseValue6i > 0.2f)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //frontExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }

                                    if (z == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }





            /////////////////////////////////////BACK WALL////////////////////////////////
            if (typeofterraintiles == 1104)
            {
                //for (int j = 0; j < levelgen.backWall.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.backWall[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                            float noiseX5 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                    float noiseZ5 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                    float noiseValue = someothernoisevalue;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);

                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos[1] == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }*/
                                    //noiseValue += (10 - (float)y) / somenoisevalue;
                                    //noiseValue /= (float)y / 5;

                                    if (noiseValue > 0.2f && y < LevelGenerator4.wallheightsize - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue4i = noiseValue5;

                                    noiseValue4i += (5 - (float)z) / 5;
                                    noiseValue4i /= (float)z / 5;


                                    if (noiseValue4i > 0.2f)
                                    {
                                        map[x + width * (y + height * (depth - 1 - z))] = 1;
                                        //backExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }

                                    if (z == depth - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }




            /////////////////////////////////////LEFT BACK INSIDE CORNER////////////////////////////////
            if (typeofterraintiles == 1105)
            {
                //for (int j = 0; j < levelgen.builtLeftFrontInsideCorner.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtLeftFrontInsideCorner[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)((depth - 1 - z) * planeSize + chunkPos[2] + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)((depth - 1 - z) * planeSize + chunkPos[2] + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                                    float noiseValue = someothernoisevalue;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);

                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    if (noiseValue > 0.2f && y < LevelGenerator4.wallheightsize - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue9i = noiseValue2;

                                    noiseValue9i += (5 - (float)x) / 5;
                                    noiseValue9i /= (float)x / 5;

                                    float noiseValue10i = noiseValue5;
                                    noiseValue10i += (5 - (float)z) / 5;
                                    noiseValue10i /= (float)z / 5;



                                    if (noiseValue9i > 0.2f || noiseValue10i > 0.2f)
                                    {
                                        map[x + width * (y + height * (depth - 1 - z))] = 1;
                                        //backInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }

                                    /*if (x == 0 || z == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    map[x + width * (y + height * z)] = 1;*/

                                }
                            }
                        }
                    }
                }
            }





            /////////////////////////////////////RIGHT BACK INSIDE CORNER////////////////////////////////
            if (typeofterraintiles == 1106)
            {
                //for (int j = 0; j < levelgen.builtRightFrontInsideCorner.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtRightFrontInsideCorner[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)((width - 1 - x) * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)((width - 1 - x) * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)((depth - 1 - z) * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)((depth - 1 - z) * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                                    float noiseValue = someothernoisevalue;
                                    noiseValue *= fastNoise.GetNoise(((((width - 1 - x) * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);

                                    /*float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);
                                    
                                    
                                    if (noiseValue > 0.2f && y < LevelGenerator4.wallheightsize -1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue11i = noiseValue5;
                                    noiseValue11i += (5 - (float)z) / 5;
                                    noiseValue11i /= (float)z / 5;

                                    float noiseValue12i = noiseValue2;

                                    noiseValue12i += (5 - (float)x) / 5;
                                    noiseValue12i /= (float)x / 5;


                                    if (noiseValue11i > 0.2f || noiseValue12i < 0.2f)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //frontInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }*/

                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    if (noiseValue > 0.2f && y < LevelGenerator4.wallheightsize - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue9i = noiseValue2;

                                    noiseValue9i += (5 - (float)x) / 5;
                                    noiseValue9i /= (float)x / 5;

                                    float noiseValue10i = noiseValue5;
                                    noiseValue10i += (5 - (float)z) / 5;
                                    noiseValue10i /= (float)z / 5;



                                    if (noiseValue9i > 0.2f || noiseValue10i > 0.2f)
                                    {
                                        map[(width - 1 - x) + width * (y + height * (depth - 1 - z))] = 1;
                                        //backInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }
                                    /*
                                    if (x == 0 || z == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    map[x + width * (y + height * z)] = 1;*/

                                }
                            }
                        }
                    }
                }
            }








            /////////////////////////////////////LEFT FRONT INSIDE CORNER////////////////////////////////
            if (typeofterraintiles == 1107)
            {
                //for (int j = 0; j < levelgen.builtLeftBackInsideCorner.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtLeftBackInsideCorner[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                                    float noiseValue = someothernoisevalue;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);

                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    if (noiseValue > 0.2f && y < LevelGenerator4.wallheightsize - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue9i = noiseValue2;

                                    noiseValue9i += (5 - (float)x) / 5;
                                    noiseValue9i /= (float)x / 5;

                                    float noiseValue10i = noiseValue5;
                                    noiseValue10i += (5 - (float)z) / 5;
                                    noiseValue10i /= (float)z / 5;



                                    if (noiseValue9i > 0.2f || noiseValue10i > 0.2f)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //backInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }

                                    /*if (x == 0 || z == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    map[x + width * (y + height * z)] = 1;*/

                                }
                            }
                        }
                    }
                }
            }




            /////////////////////////////////////RIGHT FRONT INSIDE CORNER////////////////////////////////
            if (typeofterraintiles == 1108)
            {
                //for (int j = 0; j < levelgen.builtRightBackInsideCorner.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtRightBackInsideCorner[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)((width - 1 - x) * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)((width - 1 - x) * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                                    float noiseValue = someothernoisevalue;
                                    noiseValue *= fastNoise.GetNoise(((((width - 1 - x) * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);

                                    /*float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);
                                    
                                    
                                    if (noiseValue > 0.2f && y < LevelGenerator4.wallheightsize -1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue11i = noiseValue5;
                                    noiseValue11i += (5 - (float)z) / 5;
                                    noiseValue11i /= (float)z / 5;

                                    float noiseValue12i = noiseValue2;

                                    noiseValue12i += (5 - (float)x) / 5;
                                    noiseValue12i /= (float)x / 5;


                                    if (noiseValue11i > 0.2f || noiseValue12i < 0.2f)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //frontInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }*/

                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    if (noiseValue > 0.2f && y < LevelGenerator4.wallheightsize - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue9i = noiseValue2;

                                    noiseValue9i += (5 - (float)x) / 5;
                                    noiseValue9i /= (float)x / 5;

                                    float noiseValue10i = noiseValue5;
                                    noiseValue10i += (5 - (float)z) / 5;
                                    noiseValue10i /= (float)z / 5;



                                    if (noiseValue9i > 0.2f || noiseValue10i > 0.2f)
                                    {
                                        map[(width - 1 - x) + width * (y + height * z)] = 1;
                                        //backInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }
                                    /*
                                    if (x == 0 || z == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    map[x + width * (y + height * z)] = 1;*/

                                }
                            }
                        }
                    }
                }
            }




            /////////////////////////////////////LEFT BACK OUTSIDE CORNER////////////////////////////////
            if (typeofterraintiles == 1109)
            {
                // for (int j = 0; j < levelgen.builtLeftFrontOutsideCorner.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtLeftFrontOutsideCorner[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)((depth - 1 - z) * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)((depth - 1 - z) * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                    float noiseValue = someothernoisevalue;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);


                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    //noiseValue += (10 - (float)y) / somenoisevalue;
                                    //noiseValue /= (float)y / 5;

                                    if (noiseValue > 0.2f && y < LevelGenerator4.wallheightsize - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue17i = noiseValue2;

                                    noiseValue17i += (5 - (float)x) / 5;
                                    noiseValue17i /= (float)x / 5;

                                    float noiseValue18i = noiseValue5;

                                    noiseValue18i += (5 - (float)z) / 5;
                                    noiseValue18i /= (float)z / 5;

                                    if (noiseValue17i > 0.2f && noiseValue18i > 0.2f)
                                    {
                                        map[x + width * (y + height * (depth - 1 - z))] = 1;
                                        //backOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }

                                    //map[x + width * (y + height * z)] = 1;
                                }
                            }
                        }
                    }
                }
            }



            /////////////////////////////////////RIGHT BACK OUTSIDE CORNER////////////////////////////////
            if (typeofterraintiles == 1110)
            {
                //for (int j = 0; j < levelgen.builtRightFrontOutsideCorner.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtRightFrontOutsideCorner[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)((width - 1 - x) * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)((width - 1 - x) * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)((depth - 1 - z) * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)((depth - 1 - z) * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                    float noiseValue = someothernoisevalue;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);


                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    //noiseValue += (10 - (float)y) / somenoisevalue;
                                    //noiseValue /= (float)y / 5;

                                    if (noiseValue > 0.2f && y < LevelGenerator4.wallheightsize - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue17i = noiseValue2;

                                    noiseValue17i += (5 - (float)x) / 5;
                                    noiseValue17i /= (float)x / 5;

                                    float noiseValue18i = noiseValue5;

                                    noiseValue18i += (5 - (float)z) / 5;
                                    noiseValue18i /= (float)z / 5;

                                    if (noiseValue17i > 0.2f && noiseValue18i > 0.2f)
                                    {
                                        map[(width - 1 - x) + width * (y + height * (depth - 1 - z))] = 1;
                                        //backOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }

                                    //map[x + width * (y + height * z)] = 1;
                                }
                            }
                        }
                    }
                }
            }


            /////////////////////////////////////LEFT FRONT OUTSIDE CORNER////////////////////////////////

            if (typeofterraintiles == 1111)
            {
                //for (int j = 0; j < levelgen.builtLeftBackOutsideCorner.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtLeftBackOutsideCorner[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                    float noiseValue = someothernoisevalue;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);


                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    //noiseValue += (10 - (float)y) / somenoisevalue;
                                    //noiseValue /= (float)y / 5;

                                    if (noiseValue > 0.2f && y < LevelGenerator4.wallheightsize - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue17i = noiseValue2;

                                    noiseValue17i += (5 - (float)x) / 5;
                                    noiseValue17i /= (float)x / 5;

                                    float noiseValue18i = noiseValue5;

                                    noiseValue18i += (5 - (float)z) / 5;
                                    noiseValue18i /= (float)z / 5;

                                    if (noiseValue17i > 0.2f && noiseValue18i > 0.2f)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //backOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }

                                    //map[x + width * (y + height * z)] = 1;
                                }
                            }
                        }
                    }
                }

            }


            /////////////////////////////////////RIGHT FRONT OUTSIDE CORNER////////////////////////////////
            if (typeofterraintiles == 1112)
            {
                //for (int j = 0; j < levelgen.builtRightBackOutsideCorner.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtRightBackOutsideCorner[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)((width - 1 - x) * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)((width - 1 - x) * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                    float noiseValue = someothernoisevalue;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);


                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    //noiseValue += (10 - (float)y) / somenoisevalue;
                                    //noiseValue /= (float)y / 5;

                                    if (noiseValue > 0.2f && y < LevelGenerator4.wallheightsize - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue17i = noiseValue2;

                                    noiseValue17i += (5 - (float)x) / 5;
                                    noiseValue17i /= (float)x / 5;

                                    float noiseValue18i = noiseValue5;

                                    noiseValue18i += (5 - (float)z) / 5;
                                    noiseValue18i /= (float)z / 5;

                                    if (noiseValue17i > 0.2f && noiseValue18i > 0.2f)
                                    {
                                        map[(width - 1 - x) + width * (y + height * z)] = 1;
                                        //backOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }

                                    //map[x + width * (y + height * z)] = 1;
                                }
                            }
                        }
                    }
                }
            }






























            /////////////////////////////////////LEFT FRONT OUTSIDE CORNER////////////////////////////////
            if (typeofterraintiles == 13)
            {
                // for (int j = 0; j < levelgen.builtLeftFrontOutsideCorner.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtLeftFrontOutsideCorner[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                    float noiseValue = someothernoisevalue;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);

                                    /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos[1] == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }*/

                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    //noiseValue += (10 - (float)y) / somenoisevalue;
                                    //noiseValue /= (float)y / 5;

                                    if (noiseValue > 0.2f && y < LevelGenerator4.wallheightsize - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue13i = noiseValue2;

                                    noiseValue13i += (5 - (float)x) / 5;
                                    noiseValue13i /= (float)x / 5;

                                    float noiseValue14i = noiseValue5;

                                    noiseValue14i += (5 - (float)z) / 5;
                                    noiseValue14i /= (float)z / 5;


                                    if (noiseValue13i > 0.2f && noiseValue14i < 0.2f)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //leftOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }

                                    if (noiseValue > 0.2f && y < LevelGenerator4.wallheightsize - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue19i = noiseValue5;
                                    noiseValue19i += (5 - (float)z) / 5;
                                    noiseValue19i /= (float)z / 5;

                                    float noiseValue20i = noiseValue2;
                                    noiseValue20i += (5 - (float)x) / 5;
                                    noiseValue20i /= (float)x / 5;


                                    if (noiseValue19i > 0.2f && noiseValue20i < 0.2f)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //frontOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }
                                }
                            }
                        }
                    }
                }
            }









            /////////////////////////////////////RIGHT FRONT OUTSIDE CORNER////////////////////////////////
            if (typeofterraintiles == 14)
            {
                //for (int j = 0; j < levelgen.builtRightFrontOutsideCorner.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtRightFrontOutsideCorner[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                    float noiseValue = someothernoisevalue;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);


                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    //noiseValue += (10 - (float)y) / somenoisevalue;
                                    //noiseValue /= (float)y / 5;

                                    /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos[1] == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }*/


                                    if (noiseValue > 0.2f && y < LevelGenerator4.wallheightsize - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue15i = noiseValue2;

                                    noiseValue15i += (5 - (float)x) / 5;
                                    noiseValue15i /= (float)x / 5;

                                    float noiseValue16i = noiseValue5;

                                    noiseValue16i += (5 - (float)z) / 5;
                                    noiseValue16i /= (float)z / 5;


                                    if (noiseValue15i < 0.2f && noiseValue16i < 0.2f)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //rightOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }

                                    float noiseValue17i = noiseValue2;

                                    noiseValue17i += (5 - (float)x) / 5;
                                    noiseValue17i /= (float)x / 5;

                                    float noiseValue18i = noiseValue5;

                                    noiseValue18i += (5 - (float)z) / 5;
                                    noiseValue18i /= (float)z / 5;

                                    if (noiseValue17i > 0.2f && noiseValue18i > 0.2f)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //backOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return map;
        }


        public int[] startBuildingArray(int facetype_, tutorialcubeaschunk componentparent_, int levelofdetail_) //, out int vertexNum, out int indicesNum
        //public void startBuildingArray(Vector4 currentPosition, out sclevelgenclass.DVertex[] vertexArray, out int[] triangleArray, out int[] mapper, int padding0_, int padding1_, int padding2_, int numberOfObjectInWidth_, int numberOfObjectInHeight_, int numberOfObjectInDepth_, int numberOfInstancesPerObjectInWidth_, int numberOfInstancesPerObjectInHeight_, int numberOfInstancesPerObjectInDepth_, int width_, int height_, int depth_, float planeSize_, sclevelgenclassPrim componentparentprim_, sclevelgenclass componentparentinstance_, sclevelgenclass_instances componentparentthischunk_, int fullface_, int voxeltype_)
        {
            //realpos = newchunkpos_;
            levelofdetail = levelofdetail_;
            // TOFIX
            // TOFIX
            // TOFIX
            //levelofdetail
            //width = 10;
            //height = 10;
            //depth = 10;
            // TOFIX
            // TOFIX
            // TOFIX
            //newchunkpos = newchunkpos_;

            componentparent = componentparent_;

            //map = map_;

            facetype = facetype_;

            width = 10;
            height = 10;
            depth = 10;

            //Console.WriteLine(chunkPos);


            if (levelofdetail == 3)
            {
                levelofdetailmul = 1.15f;
            }
            if (levelofdetail == 4)
            {
                levelofdetailmul = 1.25f;
            }
            if (levelofdetail == 5)
            {
                levelofdetailmul = 2.0f;
            }




            if (levelofdetail == 1)
            {
                width = componentparent.somelevelgenprimglobals.widthlod0;
                height = componentparent.somelevelgenprimglobals.heightlod0;
                depth = componentparent.somelevelgenprimglobals.depthlod0;

                //width = 10;
                //height = 10;
                //depth = 10;
            }
            else if (levelofdetail == 2)
            {
                width = componentparent.somelevelgenprimglobals.widthlod1;
                height = componentparent.somelevelgenprimglobals.heightlod1;
                depth = componentparent.somelevelgenprimglobals.depthlod1;

                //width = 5;
                //height = 5;
                //depth = 5;
            }

            else if (levelofdetail == 3)
            {
                width = componentparent.somelevelgenprimglobals.widthlod2;
                height = componentparent.somelevelgenprimglobals.heightlod2;
                depth = componentparent.somelevelgenprimglobals.depthlod2;
                //width = 3;
                //height = 3;
                //depth = 3;
            }
            else if (levelofdetail == 4)
            {
                width = componentparent.somelevelgenprimglobals.widthlod3;
                height = componentparent.somelevelgenprimglobals.heightlod3;
                depth = componentparent.somelevelgenprimglobals.depthlod3;

                //width = 2;
                //height = 2;
                //depth = 2;
            }
            else if (levelofdetail == 5)
            {
                width = componentparent.somelevelgenprimglobals.widthlod4;
                height = componentparent.somelevelgenprimglobals.heightlod4;
                depth = componentparent.somelevelgenprimglobals.depthlod4;

                //width = 2;
                //height = 2;
                //depth = 2;
            }




            planeSize = componentparent.somelevelgenprimglobals.planeSize;












            /*
            planeSize = 0.1f;
            staticPlaneSize = planeSize;

            if (staticPlaneSize == 1)
            {
                staticPlaneSize = planeSize * 0.1f;
                alternateStaticPlaneSize = planeSize * 0.1f;
            }
            else if (staticPlaneSize == 0.1f)
            {
                staticPlaneSize = planeSize;
                alternateStaticPlaneSize = planeSize * 10;
            }
            else if (staticPlaneSize == 0.01f)
            {
                staticPlaneSize = planeSize;
                alternateStaticPlaneSize = planeSize * 1000;
            }*/
            //width = 10;
            seed = 3420;


            chunkoriginpos = new Vector4(newchunkpos[0], newchunkpos[1], newchunkpos[2], 0.0f);

            //floorHeight = height;


            total = width * height * depth;
            totalints = width * height * depth;

            vertexlistWidth = width + 1;
            vertexlistHeight = height + 1;
            vertexlistDepth = depth + 1;
            //map = new int[width * height * depth];

            _tempChunkArrayBottomFace = new int[width * height * depth];
            _tempChunkArrayBackFace = new int[width * height * depth];
            _tempChunkArrayFrontFace = new int[width * height * depth];
            _tempChunkArrayLeftFace = new int[width * height * depth];
            _tempChunkArrayRightFace = new int[width * height * depth];
            _tempChunkArray = new int[width * height * depth];

            _chunkArray = new int[width * height * depth];

            _chunkVertexArray0 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray1 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray2 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray3 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray4 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray5 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

            _testVertexArray0 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray1 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray2 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray3 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray4 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray5 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

            //vertexlist = new List<Vector3>();

            vertexlist = new List<tutorialcubeaschunk.DVertex>();

            listOfTriangleIndices = new List<int>();
            //normalslist = new List<Vector3>();
            //colorslist = new List<Vector4>();
            //indexPoslist = new List<Vector4>();
            //textureslist = new List<Vector2>();

            //Console.WriteLine(width + " " + width);

            /*
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        float noiseXZ = 20;

                        noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (currentPosition.X * alternateStaticPlaneSize) + seed) / detailScale) * HeightScale, (((y * staticPlaneSize) + (currentPosition.Y * alternateStaticPlaneSize) + seed) / detailScale) * HeightScale, (((z * staticPlaneSize) + (currentPosition.Z * alternateStaticPlaneSize) + seed) / detailScale) * HeightScale);

                        //Console.WriteLine(noiseXZ);

                        if (noiseXZ >= 0.1f)
                        {
                            map[x + width * (y + height * z)] = 1;
                        }
                        else if (y == 0 && currentPosition.Y == 0)
                        {
                            map[x + width * (y + height * z)] = 1;
                        }
                        else
                        {
                            map[x + width * (y + height * z)] = 0;
                        }

                        //map[x + width * (y + height * z)] = 1;
                    }
                }
            }*/










            for (int i = 0; i < somearrayofcoords.Length; i++)
            {
                somearrayofcoords[i] = new int[3];
            }



            somearrayofcoords[0][0] = X - 1;
            somearrayofcoords[0][1] = Y;
            somearrayofcoords[0][2] = Z;

            somearrayofcoords[1][0] = X + 1;
            somearrayofcoords[1][1] = Y;
            somearrayofcoords[1][2] = Z;

            somearrayofcoords[2][0] = X;
            somearrayofcoords[2][1] = Y;
            somearrayofcoords[2][2] = Z - 1;

            somearrayofcoords[3][0] = X;
            somearrayofcoords[3][1] = Y;
            somearrayofcoords[3][2] = Z + 1;

            somearrayofcoords[4][0] = X;
            somearrayofcoords[4][1] = Y - 1;
            somearrayofcoords[4][2] = Z;

            somearrayofcoords[5][0] = X;
            somearrayofcoords[5][1] = Y + 1;
            somearrayofcoords[5][2] = Z;


            // Console.WriteLine(chunkPos);

            //float somemul = 1.0f;

            //List<Vector3> listofvecs = new List<Vector3>();

            //Console.WriteLine(chunkPos);

            //LEFT CHUNK
            /*Vector3 somechunkpos = chunkPos;
            somechunkpos.X -= 1.0f;
            listofvecs.Add(somechunkpos);

            //RIGHT CHUNK
            somechunkpos = chunkPos;
            somechunkpos.X += 1.0f;
            listofvecs.Add(somechunkpos);

            //BACK CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Z -= 1.0f;
            listofvecs.Add(somechunkpos);

            //FRONT CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Z += 1.0f;
            listofvecs.Add(somechunkpos);

            //BOTTOM CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Y -= 1.0f;
            listofvecs.Add(somechunkpos);

            //TOP CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Y += 1.0f;
            listofvecs.Add(somechunkpos);*/








            /*for (int i = 0; i < listofvecs.Count; i++)
            {
                listofchunksadjacent[i] = componentparent_.getChunklod0((int)Math.Round(listofvecs[i].X), (int)Math.Round(listofvecs[i].Y), (int)Math.Round(listofvecs[i].Z), out arrayindex); //(int)listofvecs[i].Y
                //listofchunksadjacent[i] = componentparent_.getchunkinlevelgenmap((int)Math.Round(listofvecs[i].X), (int)Math.Round(listofvecs[i].Y), (int)Math.Round(listofvecs[i].Z), levelofdetail); //(int)listofvecs[i].Y

                if (listofchunksadjacent[i] != null)
                {
                    //Console.WriteLine(listofchunksadjacent[i].chunkPos);
                }
            }*/




            //Console.WriteLine("LOD:" + levelofdetail);

            for (int i = 0; i < somearrayofcoords.Length; i++)
            {
                //if (levelofdetail == 1)
                {
                    //listofchunksadjacent[i] = (sclevelgenvert)componentparent.getChunklod0(somearrayofcoords[i][0], somearrayofcoords[i][1], somearrayofcoords[i][2], out arrayindex);
                    listofchunksadjacent[i] = (sclevelgenvert)componentparent.getchunkinlevelgenmap(somearrayofcoords[i][0], somearrayofcoords[i][1], somearrayofcoords[i][2], levelofdetail);
                }
                /* else if (levelofdetail == 2)
                 {
                     listofchunksadjacent[i] = (sclevelgenvert)componentparent.getChunklod1(listofvecs[i].X, listofvecs[i].Y, listofvecs[i].Z, out arrayindex);


                 }
                 else if (levelofdetail == 3)
                 {
                     listofchunksadjacent[i] = (sclevelgenvert)componentparent.getChunklod2(listofvecs[i].X, listofvecs[i].Y, listofvecs[i].Z, out arrayindex);


                 }
                 else if (levelofdetail == 4)
                 {
                     listofchunksadjacent[i] = (sclevelgenvert)componentparent.getChunklod3(listofvecs[i].X, listofvecs[i].Y, listofvecs[i].Z, out arrayindex);

                 }
                 else if (levelofdetail == 5)
                 {
                     listofchunksadjacent[i] = (sclevelgenvert)componentparent.getChunklod4(listofvecs[i].X, listofvecs[i].Y, listofvecs[i].Z, out arrayindex);

                 }*/
            }








            /*
            if (levelofdetail == 1)
            {
              

            }
            else if (levelofdetail == 2)
            {
                for (int i = 0; i < listofvecs.Count; i++)
                {
                    listofchunksadjacent[i] = (sclevelgenvert)componentparent.getChunklod1(listofvecs[i].X, listofvecs[i].Y, listofvecs[i].Z, out arrayindex);
                }

            }
            else if (levelofdetail == 3)
            {
                for (int i = 0; i < listofvecs.Count; i++)
                {
                    listofchunksadjacent[i] = (sclevelgenvert)componentparent.getChunklod2(listofvecs[i].X, listofvecs[i].Y, listofvecs[i].Z, out arrayindex);
                }

            }
            else if (levelofdetail == 4)
            {
                for (int i = 0; i < listofvecs.Count; i++)
                {
                    listofchunksadjacent[i] = (sclevelgenvert)componentparent.getChunklod3(listofvecs[i].X, listofvecs[i].Y, listofvecs[i].Z, out arrayindex);
                }
            }*/








            /*List<Vector3> listofvecsadjacentfloor = new List<Vector3>();

            //LEFT CHUNK
            somechunkpos = chunkPos;
            somechunkpos.X -= 1.0f;
            listofvecsadjacentfloor.Add(somechunkpos);

            //RIGHT CHUNK
            somechunkpos = chunkPos;
            somechunkpos.X += 1.0f;
            listofvecsadjacentfloor.Add(somechunkpos);

            //BACK CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Z -= 1.0f;
            listofvecsadjacentfloor.Add(somechunkpos);

            //FRONT CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Z += 1.0f;
            listofvecsadjacentfloor.Add(somechunkpos);

            //BOTTOM CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Y -= 1.0f;
            listofvecsadjacentfloor.Add(somechunkpos);

            //TOP CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Y += 1.0f;
            listofvecsadjacentfloor.Add(somechunkpos);*/

            for (int i = 0; i < somearrayofcoordsfloor.Length; i++)
            {
                somearrayofcoordsfloor[i] = new int[3];
            }



            somearrayofcoordsfloor[0][0] = X - 1;
            somearrayofcoordsfloor[0][1] = 0;
            somearrayofcoordsfloor[0][2] = Z;

            somearrayofcoordsfloor[1][0] = X + 1;
            somearrayofcoordsfloor[1][1] = 0;
            somearrayofcoordsfloor[1][2] = Z;

            somearrayofcoordsfloor[2][0] = X;
            somearrayofcoordsfloor[2][1] = 0;
            somearrayofcoordsfloor[2][2] = Z - 1;

            somearrayofcoordsfloor[3][0] = X;
            somearrayofcoordsfloor[3][1] = 0;
            somearrayofcoordsfloor[3][2] = Z + 1;

            somearrayofcoordsfloor[4][0] = X;
            somearrayofcoordsfloor[4][1] = 0;
            somearrayofcoordsfloor[4][2] = Z;

            somearrayofcoordsfloor[5][0] = X;
            somearrayofcoordsfloor[5][1] = 0;
            somearrayofcoordsfloor[5][2] = Z;


            for (int i = 0; i < somearrayofcoordsfloor.Length; i++)
            {
                //if (levelofdetail == 1)
                {
                    //listofchunksadjacentfloor[i] = (sclevelgenvert)componentparent.getChunklod0(somearrayofcoordsfloor[i][0], somearrayofcoordsfloor[i][1], somearrayofcoordsfloor[i][2], out arrayindex);
                    listofchunksadjacentfloor[i] = (sclevelgenvert)componentparent.getchunkinlevelgenmap(somearrayofcoordsfloor[i][0], 0, somearrayofcoordsfloor[i][2], levelofdetail);
                }
                /*else if (levelofdetail == 2)
                {
                    listofchunksadjacentfloor[i] = (sclevelgenvert)componentparent.getChunklod1(listofvecsadjacentfloor[i].X, 0, listofvecsadjacentfloor[i].Z, out arrayindex);


                }
                else if (levelofdetail == 3)
                {
                    listofchunksadjacentfloor[i] = (sclevelgenvert)componentparent.getChunklod2(listofvecsadjacentfloor[i].X, 0, listofvecsadjacentfloor[i].Z, out arrayindex);


                }
                else if (levelofdetail == 4)
                {
                    listofchunksadjacentfloor[i] = (sclevelgenvert)componentparent.getChunklod3(listofvecsadjacentfloor[i].X, 0, listofvecsadjacentfloor[i].Z, out arrayindex);

                }
                else if (levelofdetail == 5)
                {
                    listofchunksadjacentfloor[i] = (sclevelgenvert)componentparent.getChunklod4(listofvecsadjacentfloor[i].X, 0, listofvecsadjacentfloor[i].Z, out arrayindex);

                }*/
            }



            /*
            for (int i = 0; i < listofvecsadjacentfloor.Count; i++)
            {
                //Console.WriteLine("x:" + listofvecsadjacentfloor[i].X + "/y:" + listofvecsadjacentfloor[i].Y + "/z:" + listofvecsadjacentfloor[i].Z);
                //listofchunksadjacentfloor[i] = componentparent_.getChunklod0((int)Math.Round(listofvecsadjacentfloor[i].X), 0, (int)Math.Round(listofvecsadjacentfloor[i].Z) , out arrayindex);
                listofchunksadjacentfloor[i] = componentparent_.getChunklod0((int)Math.Round(listofvecsadjacentfloor[i].X), 0, (int)Math.Round(listofvecsadjacentfloor[i].Z), out arrayindex); //(int)listofvecs[i].Y
                //listofchunksadjacentfloor[i] = componentparent_.getchunkinlevelgenmap((int)Math.Round(listofvecsadjacentfloor[i].X), (int)Math.Round(listofvecsadjacentfloor[i].Y), (int)Math.Round(listofvecsadjacentfloor[i].Z), levelofdetail); //(int)listofvecs[i].Y



                if (listofchunksadjacentfloor[i] != null)
                {
                    //Console.WriteLine(listofchunksadjacentfloor[i].chunkPos);
                }

            }*/





            //cannot be zero otherwise each vertex will be at zero
            //levelofdetail += 1;
            //cannot be zero otherwise each vertex will be at zero


            sccsSetMap();
            Regenerate(facetype); //currentPosition

            //vertexArray = vertexlist.ToArray();
            //triangleArray = listOfTriangleIndices.ToArray();
            arrayofverts = vertexlist.ToArray();
            arrayoftrigs = listOfTriangleIndices.ToArray();


            /*
            vertexArray = new Vector4[vertexlist.Count];

            for (int i = 0; i < vertexArray.Length; i++)
            {
                vertexArray[i] = vertexlist[i].position;
            }*/

            /*
            norms = new Vector3[vertexlist.Count];

            for (int i = 0; i < vertexArray.Length; i++)
            {
                norms[i] = vertexlist[i].normal;
            }

            tex = new Vector2[vertexlist.Count];

            for (int i = 0; i < vertexArray.Length; i++)
            {
                tex[i] = vertexlist[i].tex;
            }
            */







            _tempChunkArrayBottomFace = null;// new int[width * height * depth];
            _tempChunkArrayBackFace = null;// new int[width * height * depth];
            _tempChunkArrayFrontFace = null;// new int[width * height * depth];
            _tempChunkArrayLeftFace = null;//new int[width * height * depth];
            _tempChunkArrayRightFace = null;//new int[width * height * depth];
            _tempChunkArray = null;// new int[width * height * depth];

            _chunkArray = null;//new int[width * height * depth];

            _chunkVertexArray0 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray1 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray2 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray3 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray4 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray5 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

            _testVertexArray0 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray1 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray2 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray3 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray4 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray5 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

            vertexlist = null;
            listOfTriangleIndices = null;

            //map = null;

            //listofchunksadjacent = null;
            //listofchunksadjacentfloor = null;

            return map;
        }

        public void cleararrays()
        {

            _tempChunkArrayBottomFace = null;// new int[width * height * depth];
            _tempChunkArrayBackFace = null;// new int[width * height * depth];
            _tempChunkArrayFrontFace = null;// new int[width * height * depth];
            _tempChunkArrayLeftFace = null;//new int[width * height * depth];
            _tempChunkArrayRightFace = null;//new int[width * height * depth];
            _tempChunkArray = null;// new int[width * height * depth];

            _chunkArray = null;//new int[width * height * depth];

            _chunkVertexArray0 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray1 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray2 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray3 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray4 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray5 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

            _testVertexArray0 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray1 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray2 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray3 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray4 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray5 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

            vertexlist = null;
            listOfTriangleIndices = null;
        }




        public void sccsSetMap()
        {
            //_newVertzCounter = 0;
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

            if (vertexlist == null)
            {
                vertexlist = new List<tutorialcubeaschunk.DVertex>();

            }
            else
            {
                vertexlist.Clear();
            }

            if (listOfTriangleIndices == null)
            {
                listOfTriangleIndices = new List<int>();
            }
            else
            {
                listOfTriangleIndices.Clear();
            }

            _newVertzCounter = 0;
            //vertexlist.Clear();
            //listOfTriangleIndices.Clear();




            _tempChunkArrayBottomFace = new int[width * height * depth];
            _tempChunkArrayBackFace = new int[width * height * depth];
            _tempChunkArrayFrontFace = new int[width * height * depth];
            _tempChunkArrayLeftFace = new int[width * height * depth];
            _tempChunkArrayRightFace = new int[width * height * depth];
            _tempChunkArray = new int[width * height * depth];

            _chunkArray = new int[width * height * depth];

            _chunkVertexArray0 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray1 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray2 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray3 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray4 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray5 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

            _testVertexArray0 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray1 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray2 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray3 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray4 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray5 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];



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
                    _chunkVertexArray0[t] = 0;
                    _chunkVertexArray1[t] = 0;
                    _chunkVertexArray2[t] = 0;
                    _chunkVertexArray3[t] = 0;
                    _chunkVertexArray4[t] = 0;
                    _chunkVertexArray5[t] = 0;

                    _testVertexArray0[t] = 0;
                    _testVertexArray1[t] = 0;
                    _testVertexArray2[t] = 0;
                    _testVertexArray3[t] = 0;
                    _testVertexArray4[t] = 0;
                    _testVertexArray5[t] = 0;
                }
            }
        }





        public void sccsSetMapMOD()
        {
            _newVertzCounter = 0;
            /*
            if (vertexlist == null)
            {
                //vertexlist = new List<sclevelgenclass.DVertex>();

            }
            else
            {
                vertexlist.Clear();
            }

            if (listOfTriangleIndices == null)
            {
                //listOfTriangleIndices = new List<int>();
            }
            else
            {
                listOfTriangleIndices.Clear();
            }
            */


            vertexlist.Clear();
            listOfTriangleIndices.Clear();

            //sccsSetMap();
            //Regenerate(); //currentPosition



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
                    _chunkVertexArray0[t] = 0;
                    _chunkVertexArray1[t] = 0;
                    _chunkVertexArray2[t] = 0;
                    _chunkVertexArray3[t] = 0;
                    _chunkVertexArray4[t] = 0;
                    _chunkVertexArray5[t] = 0;

                    _testVertexArray0[t] = 0;
                    _testVertexArray1[t] = 0;
                    _testVertexArray2[t] = 0;
                    _testVertexArray3[t] = 0;
                    _testVertexArray4[t] = 0;
                    _testVertexArray5[t] = 0;
                }
            }
        }
























        public void Regenerate(int facetype)
        {


            if (vertexlist == null)
            {
                vertexlist = new List<tutorialcubeaschunk.DVertex>();
            }
            else
            {
                vertexlist.Clear();
            }

            if (listOfTriangleIndices == null)
            {
                listOfTriangleIndices = new List<int>();
            }
            else
            {
                listOfTriangleIndices.Clear();
            }


            //vertexlist.Clear();
            //listOfTriangleIndices.Clear();

            /*_tempChunkArrayBottomFace = new int[width * height * depth];
            _tempChunkArrayBackFace = new int[width * height * depth];
            _tempChunkArrayFrontFace = new int[width * height * depth];
            _tempChunkArrayLeftFace = new int[width * height * depth];
            _tempChunkArrayRightFace = new int[width * height * depth];
            _tempChunkArray = new int[width * height * depth];

            _chunkArray = new int[width * height * depth];

            _chunkVertexArray0 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray1 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray2 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray3 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray4 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray5 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

            _testVertexArray0 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray1 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray2 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray3 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray4 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray5 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];*/
            //normalslist.Clear();
            //colorslist.Clear();
            //indexPoslist.Clear();
            //textureslist.Clear();

            /*
            int xi = 0;
            int yi = 0;
            int zi = 0;*/


            //CalculateintsForFaces();
            //CreateChunkFaces();


            for (int x = 0; x < width; x++)
            {
                var xi = x;
                for (int y = 0; y < height; y++)
                {
                    var yi = y;
                    for (int z = 0; z < depth; z++)
                    {
                        var zi = z;





                        /*
                        buildTopFace(xi, yi, zi, 1.0f);
                        buildTopLeft(xi, yi, zi, 1.0f);
                        buildTopRight(xi, yi, zi, 1.0f);
                        //buildFrontFace(xi, yi, zi, 1.0f);
                        buildBackFace(xi, yi, zi, 1.0f);
                        buildBottomFace(xi, yi, zi, 1.0f);*/

                        /*
                        if (facetype == 0)
                        {
                            buildTopFace(xi, yi, zi, 1.0f);
                        }
                        else if (facetype == 1)
                        {

                            buildTopLeft(xi, yi, zi, 1.0f);
                        }
                        else if (facetype == 2)
                        {
                            buildTopRight(xi, yi, zi, 1.0f);
                        }
                        else if (facetype == 3)
                        {
                            buildFrontFace(xi, yi, zi, 1.0f);
                        }
                        else if (facetype == 4)
                        {
                            buildBackFace(xi, yi, zi, 1.0f);
                        }
                        else if (facetype == 5)
                        {
                            buildBottomFace(xi, yi, zi, 1.0f);
                        }
                        */


                        index = xi + (width) * (yi + (height) * zi);

                        block = map[index];
                        //if (block == 1)
                        {
                            /*if (IsTransparent(xi - 1, yi, zi))
                            {
                                if (xi != width - 1 && xi != 0)
                                {
                                    Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                                    var offset1 = back * planeSize;
                                    var offset2 = down * planeSize;
                                    createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2);
                                }
                                else if (xi == width - 1)
                                {
                                    Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                                    var offset1 = back * planeSize;
                                    var offset2 = down * planeSize;
                                    createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2);
                                }
                                else if (xi == 0)
                                {

                                    if (listofchunksadjacent[0] != null) // && levelofdetail == 1  && levelofdetail == 1
                                    {
                                        //Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                        if (listofchunksadjacent[0].map != null)
                                        {

                                            //Console.WriteLine("mapl:" + listofchunksadjacent[0].map.Length);
                                            if (listofchunksadjacent[0].GetByte(width - 1, yi, zi) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                            {
                                                Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                                                var offset1 = back * planeSize;
                                                var offset2 = down * planeSize;
                                                createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2);

                                            }
                                        }
                                    }
                                    else
                                    {
                                        /*if (listofchunksadjacentfloor[0] != null)
                                        {
                                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                                            var offset1 = back * planeSize;
                                            var offset2 = down * planeSize;
                                            createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2);
                                        }
                                    }
                                }
                            }







                            
                            if (IsTransparent(xi + 1, yi, zi))
                            {
                                if (xi != width - 1 && xi != 0)
                                {
                                    Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                                    var offset1 = up * planeSize;
                                    var offset2 = forward * planeSize;
                                    createRightFace(start + right * planeSize, offset1, offset2);
                                }
                                else if (xi == 0)
                                {
                                    Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                                    var offset1 = up * planeSize;
                                    var offset2 = forward * planeSize;
                                    createRightFace(start + right * planeSize, offset1, offset2);
                                }
                                else if (xi == width - 1)
                                {

                                    //Vector3 somechunkpos = chunkPos;
                                    //somechunkpos.X += 1.0f;
                                    //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);


                                    if (listofchunksadjacent[1] != null)//&& levelofdetail == 1
                                    {
                                        //Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                        if (listofchunksadjacent[1].map != null)
                                        {
                                            if (listofchunksadjacent[1].GetByte(0, yi, zi) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                            {
                                                //Console.WriteLine("found empty byte");
                                                Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                                                var offset1 = up * planeSize;
                                                var offset2 = forward * planeSize;
                                                createRightFace(start + right * planeSize, offset1, offset2);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (listofchunksadjacentfloor[1] != null)
                                        {
                                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                                            var offset1 = up * planeSize;
                                            var offset2 = forward * planeSize;
                                            createRightFace(start + right * planeSize, offset1, offset2);
                                        }
                                    }
                                }                                 
                            }





                            
                            
                            
                            if (IsTransparent(xi, yi + 1, zi))
                            {
                                if (yi != height - 1 && yi != 0)
                                {
                                    Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                                    var offset1 = forward * planeSize;
                                    var offset2 = right * planeSize;
                                    createTopFace(start + up * planeSize, offset1, offset2);
                                }
                                else if (yi == 0)
                                {
                                    Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                                    var offset1 = forward * planeSize;
                                    var offset2 = right * planeSize;
                                    createTopFace(start + up * planeSize, offset1, offset2);
                                }
                                else if (yi == height - 1)
                                {
                                    //buildTopFace(xi, yi, zi, 1.0f);
                                    //Vector3 somechunkpos = chunkPos;
                                    //somechunkpos.Y += 1.0f;
                                    //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);


                                    if (listofchunksadjacent[5] != null) //&& levelofdetail == 1
                                    {
                                        //Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                        if (listofchunksadjacent[5].map != null)
                                        {
                                            if (listofchunksadjacent[5].GetByte(xi, 0, zi) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                            {
                                                Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                                                var offset1 = forward * planeSize;
                                                var offset2 = right * planeSize;
                                                createTopFace(start + up * planeSize, offset1, offset2);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (listofchunksadjacentfloor[5] != null)
                                        {
                                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                                            var offset1 = forward * planeSize;
                                            var offset2 = right * planeSize;
                                            createTopFace(start + up * planeSize, offset1, offset2);
                                        }
                                    }
                                }
                                              
                            }
                            if (IsTransparent(xi, yi, zi + 1))
                            {
                                if (zi != depth - 1 && zi != 0)
                                {

                                    Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                                    var offset1 = right * planeSize;
                                    var offset2 = up * planeSize;
                                    createFrontFace(start + forward * planeSize, offset1, offset2);
                                }
                                else if (zi == 0)
                                {

                                    Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                                    var offset1 = right * planeSize;
                                    var offset2 = up * planeSize;
                                    createFrontFace(start + forward * planeSize, offset1, offset2);
                                }
                                else if (zi == depth - 1)
                                {
                                    if (listofchunksadjacent[3] != null) // && levelofdetail == 1
                                    {
                                        //Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                        if (listofchunksadjacent[3].map != null)
                                        {
                                            if (listofchunksadjacent[3].GetByte(xi, yi, 0) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                            {
                                                Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                                                var offset1 = right * planeSize;
                                                var offset2 = up * planeSize;
                                                createFrontFace(start + forward * planeSize, offset1, offset2);
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (listofchunksadjacentfloor[3] != null)
                                        {
                                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                                            var offset1 = right * planeSize;
                                            var offset2 = up * planeSize;
                                            createFrontFace(start + forward * planeSize, offset1, offset2);
                                        }
                                    }
                                }
                            }
                            
                            if (IsTransparent(xi, yi, zi - 1))
                            {
                                if (zi != depth - 1 && zi != 0)
                                {
                                    Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;

                                    var offset1 = left * planeSize;
                                    var offset2 = up * planeSize;
                                    createBackFace(start + right * planeSize, offset1, offset2);
                                }
                                else if (zi == depth - 1)
                                {
                                    Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;

                                    var offset1 = left * planeSize;
                                    var offset2 = up * planeSize;
                                    createBackFace(start + right * planeSize, offset1, offset2);
                                }
                                else if (zi == 0)
                                {

                                    //Vector3 somechunkpos = chunkPos;
                                    //somechunkpos.Z -= 1.0f;
                                    //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

                                    if (listofchunksadjacent[2] != null) //
                                    {
                                        //Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                        if (listofchunksadjacent[2].map != null)
                                        {

                                            //Console.WriteLine("x:" + xi + "/y:" + yi + "/z:" + zi + "depth:" + depth);
                                            if (listofchunksadjacent[2].GetByte(xi, yi, depth - 1) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                            {
                                                Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;

                                                var offset1 = left * planeSize;
                                                var offset2 = up * planeSize;
                                                createBackFace(start + right * planeSize, offset1, offset2);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (listofchunksadjacentfloor[2] != null)
                                        {
                                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;

                                            var offset1 = left * planeSize;
                                            var offset2 = up * planeSize;
                                            createBackFace(start + right * planeSize, offset1, offset2);
                                        }

                                    }
                                }
                            }*/













                            if (IsTransparent(xi - 1, yi, zi))
                            {
                                if (xi != width - 1 && xi != 0)
                                {
                                    buildTopLeft(xi, yi, zi, 1.0f);
                                }
                                else if (xi == width - 1)
                                {
                                    //if (levelofdetail != 5)
                                    {
                                        //buildTopLeft(xi, yi, zi, 1.0f);
                                    }
                                    if (//listofchunksadjacent[0] != null && typeofterraintiles == 1101 && levelofdetail != 5 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1102 && levelofdetail != 5 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1103 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1104 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1105 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1106 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1107 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1108 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1109 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1110 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1111 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1112 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1115 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 0)
                                    {
                                        buildTopLeft(xi, yi, zi, 1.0f);
                                    }

                                    if (listofchunksadjacent[0] == null && typeofterraintiles == 1101 && levelofdetail != 5 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1102 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1103 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1104 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1105 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1106 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1107 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1108 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1109 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1110 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1111 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1112 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1115 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 0)
                                    {
                                        buildTopLeft(xi, yi, zi, 1.0f);
                                    }
                                }
                                else if (xi == 0)
                                {
                                    //Vector3 somechunkpos = chunkPos;
                                    //somechunkpos.X -= 1.0f;
                                    //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkPos[1], somechunkpos.Z, out arrayindex);

                                    if (listofchunksadjacent[0] != null && levelofdetail == 1) // && levelofdetail == 1  && levelofdetail == 1
                                    {
                                        //Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                        if (listofchunksadjacent[0].map != null)
                                        {

                                            //Console.WriteLine("mapl:" + listofchunksadjacent[0].map.Length);
                                            if (listofchunksadjacent[0].GetByte(width - 1, yi, zi) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                            {

                                                /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                var offset1 = back * planeSize;
                                                var offset2 = down * planeSize;
                                                createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2);
                                                */

                                                /*if (typeofterraintiles != 0)
                                                {
                                                    Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                    var offset1 = back * planeSize;
                                                    var offset2 = down * planeSize;
                                                    createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2);
                                                }
                                                else
                                                {
                                                    buildTopLeft(xi, yi, zi, 1.0f);
                                                }*/

                                                buildTopLeft(xi, yi, zi, 1.0f);
                                            }
                                            else
                                            {
                                                //buildTopLeft(xi, yi, zi, 1.0f);
                                                //buildTopLeft(xi, yi, zi, 1.0f);
                                            }
                                        }
                                        else
                                        {
                                            //buildTopLeft(xi, yi, zi, 1.0f);
                                        }
                                    }
                                    else
                                    {
                                        //var somechunkpos = chunkPos;
                                        //somechunkpos.X -= 1.0f;
                                        //var someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);

                                        /*if (listofchunksadjacentfloor[0] != null)
                                        {
                                            buildTopLeft(xi, yi, zi, 1.0f);
                                        }

                                        if (levelofdetail != 1)
                                        {
                                            if (listofchunksadjacent[0] != null)
                                            {
                                                buildTopLeft(xi, yi, zi, 1.0f);
                                            }

                                        }*/


                                        if (//listofchunksadjacent[0] != null && typeofterraintiles == 1101 && levelofdetail != 5 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1102 && levelofdetail != 5 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1103 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1104 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1105 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1106 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1107 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1108 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1109 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1110 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1111 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1112 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 1115 ||
                                            listofchunksadjacent[0] != null && typeofterraintiles == 0)
                                        {
                                            buildTopLeft(xi, yi, zi, 1.0f);
                                        }

                                        if (//listofchunksadjacent[0] == null && typeofterraintiles == 1101 && levelofdetail != 5 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1102 && levelofdetail != 5 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1103 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1104 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1105 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1106 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1107 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1108 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1109 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1110 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1111 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1112 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1115 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 0)
                                        {
                                            buildTopLeft(xi, yi, zi, 1.0f);
                                        }






                                        /*
                                        if (listofchunksadjacent[0] != null && typeofterraintiles == 1101)
                                        {
                                            buildTopLeft(xi, yi, zi, 1.0f);
                                        }

                                        if (listofchunksadjacent[0] == null && typeofterraintiles == 1101 && levelofdetail != 5)
                                        {
                                            buildTopLeft(xi, yi, zi, 1.0f);
                                        }*/





                                        /*
                                        if (listofchunksadjacent[0] != null && typeofterraintiles == 1102 && levelofdetail == 5)
                                        {
                                            buildTopLeft(xi, yi, zi, 1.0f);
                                        }*/







                                        /*
                                        if (listofchunksadjacent[0] != null && typeofterraintiles == 1101 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[0] != null && typeofterraintiles == 1102 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[0] != null && typeofterraintiles == 1103 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[0] != null && typeofterraintiles == 1104 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[0] != null && typeofterraintiles == 1105 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[0] != null && typeofterraintiles == 1106 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[0] != null && typeofterraintiles == 1107 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[0] != null && typeofterraintiles == 1108 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[0] != null && typeofterraintiles == 1109 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[0] != null && typeofterraintiles == 1110 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[0] != null && typeofterraintiles == 1111 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[0] != null && typeofterraintiles == 1112 && yi != 0 && levelofdetail != 5)
                                        {
                                            buildTopLeft(xi, yi, zi, 1.0f);
                                        }


                                        if (typeofterraintiles == 0 && yi != 0 && levelofdetail != 5)
                                        {
                                            buildTopLeft(xi, yi, zi, 1.0f);
                                        }*/
















                                        /*
                                        if (listofchunksadjacent[0] == null && levelofdetail == 1 && chunkPos[1] == levellimitfloory) //&& levelofdetail == 1
                                        {
                                            buildTopLeft(xi, yi, zi, 1.0f);
                                        }
                                        else if (listofchunksadjacent[0] == null && levelofdetail == 1 && chunkPos[1] == sclevelgen.wallheightsize)
                                        {
                                            //dont build
                                        }
                                        else
                                        {
                                            if (listofchunksadjacentfloor[0] != null && chunkPos[1] != sclevelgen.wallheightsize)
                                            {
                                                buildTopLeft(xi, yi, zi, 1.0f);
                                            }
                                        }*/



                                        //buildTopLeft(xi, yi, zi, 1.0f);
                                    }
                                }
                            }



                            if (IsTransparent(xi + 1, yi, zi))
                            {
                                if (xi != width - 1 && xi != 0)
                                {
                                    buildTopRight(xi, yi, zi, 1.0f);
                                }
                                else if (xi == 0)
                                {
                                    //if (levelofdetail != 5)
                                    {
                                        //buildTopRight(xi, yi, zi, 1.0f);
                                    }

                                    if (listofchunksadjacent[1] != null && typeofterraintiles == 1101 && levelofdetail != 5 ||
                                            //listofchunksadjacent[1] != null && typeofterraintiles == 1102 && levelofdetail != 5 ||
                                            listofchunksadjacent[1] != null && typeofterraintiles == 1103 ||
                                            listofchunksadjacent[1] != null && typeofterraintiles == 1104 ||
                                            listofchunksadjacent[1] != null && typeofterraintiles == 1105 ||
                                            listofchunksadjacent[1] != null && typeofterraintiles == 1106 ||
                                            listofchunksadjacent[1] != null && typeofterraintiles == 1107 ||
                                            listofchunksadjacent[1] != null && typeofterraintiles == 1108 ||
                                            listofchunksadjacent[1] != null && typeofterraintiles == 1109 ||
                                            listofchunksadjacent[1] != null && typeofterraintiles == 1110 ||
                                            listofchunksadjacent[1] != null && typeofterraintiles == 1111 ||
                                            listofchunksadjacent[1] != null && typeofterraintiles == 1112 ||
                                            listofchunksadjacent[1] != null && typeofterraintiles == 1115 ||
                                            listofchunksadjacent[1] != null && typeofterraintiles == 0)
                                    {
                                        buildTopRight(xi, yi, zi, 1.0f);
                                    }

                                    if (listofchunksadjacent[1] == null && typeofterraintiles == 1101 ||
                                         listofchunksadjacent[1] == null && typeofterraintiles == 1102 && levelofdetail != 5 ||
                                         listofchunksadjacent[1] == null && typeofterraintiles == 1103 ||
                                         listofchunksadjacent[1] == null && typeofterraintiles == 1104 ||
                                         listofchunksadjacent[1] == null && typeofterraintiles == 1105 ||
                                         listofchunksadjacent[1] == null && typeofterraintiles == 1106 ||
                                         listofchunksadjacent[1] == null && typeofterraintiles == 1107 ||
                                         listofchunksadjacent[1] == null && typeofterraintiles == 1108 ||
                                         listofchunksadjacent[1] == null && typeofterraintiles == 1109 ||
                                         listofchunksadjacent[1] == null && typeofterraintiles == 1110 ||
                                         listofchunksadjacent[1] == null && typeofterraintiles == 1111 ||
                                         listofchunksadjacent[1] == null && typeofterraintiles == 1112 ||
                                         listofchunksadjacent[1] == null && typeofterraintiles == 1115 ||
                                         listofchunksadjacent[1] == null && typeofterraintiles == 0)
                                    {
                                        buildTopRight(xi, yi, zi, 1.0f);
                                    }
                                }
                                else if (xi == width - 1)
                                {

                                    //Vector3 somechunkpos = chunkPos;
                                    //somechunkpos.X += 1.0f;
                                    //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkPos[1], somechunkpos.Z);


                                    if (listofchunksadjacent[1] != null && levelofdetail == 1)//&& levelofdetail == 1
                                    {
                                        //Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                        if (listofchunksadjacent[1].map != null)
                                        {
                                            if (listofchunksadjacent[1].GetByte(0, yi, zi) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                            {
                                                /*Vector4 start = new Vector4(xi * planeSize * levelofdetail* levelofdetailmul, yi * planeSize * levelofdetail * levelofdetailmul, zi * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos;
                                                var offset1 = up * planeSize;
                                                var offset2 = forward * planeSize;
                                                createRightFace(start + right * planeSize, offset1, offset2);*/
                                                /*
                                                if (typeofterraintiles != 0)
                                                {
                                                    Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                    var offset1 = up * planeSize;
                                                    var offset2 = forward * planeSize;
                                                    createRightFace(start + right * planeSize, offset1, offset2);
                                                }
                                                else
                                                {
                                                    buildTopRight(xi, yi, zi, 1.0f);
                                                }*/


                                                buildTopRight(xi, yi, zi, 1.0f);
                                            }
                                            else
                                            {

                                            }
                                        }
                                        else
                                        {
                                            //buildTopRight(xi, yi, zi, 1.0f);
                                        }
                                    }
                                    else
                                    {
                                        //var somechunkpos = chunkPos;
                                        //somechunkpos.X += 1.0f;
                                        //var someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);

                                        /*if (listofchunksadjacentfloor[1] != null)
                                        {

                                            buildTopRight(xi, yi, zi, 1.0f);
                                        }

                                        if (levelofdetail != 1)
                                        {
                                            if (listofchunksadjacent[1] != null)
                                            {
                                                buildTopRight(xi, yi, zi, 1.0f);
                                            }

                                        }*/

                                        if (listofchunksadjacent[0] != null && typeofterraintiles == 1101 && levelofdetail != 5 ||
                                          //listofchunksadjacent[0] != null && typeofterraintiles == 1102 && levelofdetail != 5 ||
                                          listofchunksadjacent[0] != null && typeofterraintiles == 1103 ||
                                          listofchunksadjacent[0] != null && typeofterraintiles == 1104 ||
                                          listofchunksadjacent[0] != null && typeofterraintiles == 1105 ||
                                          listofchunksadjacent[0] != null && typeofterraintiles == 1106 ||
                                          listofchunksadjacent[0] != null && typeofterraintiles == 1107 ||
                                          listofchunksadjacent[0] != null && typeofterraintiles == 1108 ||
                                          listofchunksadjacent[0] != null && typeofterraintiles == 1109 ||
                                          listofchunksadjacent[0] != null && typeofterraintiles == 1110 ||
                                          listofchunksadjacent[0] != null && typeofterraintiles == 1111 ||
                                          listofchunksadjacent[0] != null && typeofterraintiles == 1112 ||
                                          listofchunksadjacent[0] != null && typeofterraintiles == 1115 ||
                                          listofchunksadjacent[0] != null && typeofterraintiles == 0)
                                        {
                                            buildTopRight(xi, yi, zi, 1.0f);
                                        }

                                        if (listofchunksadjacent[0] == null && typeofterraintiles == 1101 && levelofdetail != 5 ||
                                         //listofchunksadjacent[0] == null && typeofterraintiles == 1102 && levelofdetail != 5 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1103 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1104 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1105 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1106 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1107 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1108 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1109 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1110 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1111 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1112 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 1115 ||
                                         listofchunksadjacent[0] == null && typeofterraintiles == 0)
                                        {
                                            buildTopRight(xi, yi, zi, 1.0f);
                                        }

                                        //buildTopRight(xi, yi, zi, 1.0f);
                                    }
                                }
                            }


                            if (IsTransparent(xi, yi - 1, zi))
                            {
                                if (yi != height - 1 && yi != 0)
                                {
                                    buildBottomFace(xi, yi, zi, 1.0f);
                                }
                                else if (yi == height - 1)
                                {
                                    if (typeofterraintiles != 0) //if (levelofdetail != 5)
                                    {
                                        buildBottomFace(xi, yi, zi, 1.0f);
                                    }
                                }
                                else if (yi == 0)
                                {

                                    //Vector3 somechunkpos = chunkPos;
                                    //somechunkPos[1] -= 1.0f;
                                    //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkPos[1], somechunkpos.Z);

                                    if (listofchunksadjacent[4] != null && levelofdetail == 1 && chunkPos[1] != levellimitfloory) //&& levelofdetail == 1 // && chunkPos[1] != levellimitfloory
                                    {
                                        //Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                        if (listofchunksadjacent[4].map != null)
                                        {
                                            if (listofchunksadjacent[4].GetByte(xi, height - 1, zi) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                            {
                                                /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                var offset1 = right * planeSize;
                                                var offset2 = forward * planeSize;
                                                createBottomFace(start, offset1, offset2);*/
                                                /*if (typeofterraintiles != 0)
                                                {
                                                    Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                    var offset1 = right * planeSize;
                                                    var offset2 = forward * planeSize;
                                                    createBottomFace(start, offset1, offset2);
                                                }
                                                else
                                                {
                                                    buildBottomFace(xi, yi, zi, 1.0f);
                                                }*/
                                                buildBottomFace(xi, yi, zi, 1.0f);
                                            }
                                            else
                                            {
                                                //buildBottomFace(xi, yi, zi, 1.0f);
                                            }
                                        }
                                        else
                                        {
                                            //buildBottomFace(xi, yi, zi, 1.0f);
                                        }
                                    }
                                    else
                                    {





                                        if (listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1101 && levelofdetail != 5 ||
                                            listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1102 && levelofdetail != 5 ||
                                            listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1103 && levelofdetail != 5 ||
                                            listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1104 && levelofdetail != 5 ||
                                            listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1105 && levelofdetail != 5 ||
                                            listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1106 && levelofdetail != 5 ||
                                            listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1107 && levelofdetail != 5 ||
                                            listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1108 && levelofdetail != 5 ||
                                            listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1109 && levelofdetail != 5 ||
                                            listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1110 && levelofdetail != 5 ||
                                            listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1111 && levelofdetail != 5 ||
                                            listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1112 && levelofdetail != 5 ||
                                            typeofterraintiles == 1115)
                                        {
                                            buildBottomFace(xi, yi, zi, 1.0f);
                                        }



                                        if (listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1101 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1102 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1103 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1104 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1105 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1106 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1107 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1108 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1109 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1110 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1111 && yi != 0 && levelofdetail != 5 ||
                                           listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1112 && yi != 0 && levelofdetail != 5)
                                        {
                                            buildBottomFace(xi, yi, zi, 1.0f);
                                        }


                                        if (typeofterraintiles == 0 && yi != 0 && levelofdetail != 5)
                                        {
                                            buildBottomFace(xi, yi, zi, 1.0f);
                                        }












                                        /*

                                        if (listofchunksadjacent[4] == null && chunkPos[1] == 0.0f)
                                        {
                                            buildBottomFace(xi, yi, zi, 1.0f);
                                        }
                                        else if (listofchunksadjacent[4] != null && chunkPos[1] != sclevelgen.wallheightsize - 1)
                                        {
                                            buildBottomFace(xi, yi, zi, 1.0f);
                                        }

                                        */


                                        /*if (listofchunksadjacent[4] != null && chunkPos[1] == sclevelgen.wallheightsize - 1)
                                        {
                                            buildBottomFace(xi, yi, zi, 1.0f);
                                        }*/
                                        /*else if (listofchunksadjacent[4] == null && chunkPos[1] != levellimitfloory)
                                        {
                                            buildBottomFace(xi, yi, zi, 1.0f);
                                        }*/











                                        /*
                                        if (listofchunksadjacent[4] == null && levelofdetail != 1 && levelofdetail != 5 && chunkPos[1] == sclevelgen.wallheightsize - 1)
                                        {
                                            buildBottomFace(xi, yi, zi, 1.0f);
                                        }
                                        else if (listofchunksadjacent[4] == null && levelofdetail == 5 && chunkPos[1] == sclevelgen.wallheightsize - 1 && yi == 0)
                                        {
                                            //buildTopFace(xi, yi, zi, 1.0f);
                                        }

                                        if (listofchunksadjacent[4] != null && levelofdetail != 1 && chunkPos[1] != sclevelgen.wallheightsize - 1)
                                        {
                                            //buildTopFace(xi, yi, zi, 1.0f);
                                        }
                                        if (listofchunksadjacent[4] == null && levelofdetail != 1 && chunkPos[1] != sclevelgen.wallheightsize - 1)
                                        {
                                            buildBottomFace(xi, yi, zi, 1.0f);
                                        }*/








                                        /*if (listofchunksadjacent[5] == null && levelofdetail != 1 && levelofdetail != 5 && chunkPos[1] == levellimitfloory && yi != 0)
                                        {
                                            buildBottomFace(xi, yi, zi, 1.0f);
                                        }
                                        else if (listofchunksadjacent[5] == null && levelofdetail == 5 && chunkPos[1] == levellimitfloory && yi == 0)
                                        {
                                            //buildTopFace(xi, yi, zi, 1.0f);
                                        }

                                        if (listofchunksadjacent[5] != null && levelofdetail != 1 && chunkPos[1] != levellimitfloory)
                                        {
                                            //buildTopFace(xi, yi, zi, 1.0f);
                                        }
                                        if (listofchunksadjacent[5] == null && levelofdetail != 1 && chunkPos[1] != levellimitfloory)
                                        {
                                            buildBottomFace(xi, yi, zi, 1.0f);
                                        }*/

                                        /*
                                        if (listofchunksadjacent[4] == null && chunkPos[1] == levellimitfloory && yi == 0)
                                        {
                                            //buildBottomFace(xi, yi, zi, 1.0f);
                                        }
                                        else //if()
                                        {
                                            buildBottomFace(xi, yi, zi, 1.0f);
                                        }*/




                                        /*
                                        if (listofchunksadjacentfloor[4] != null && chunkPos[1] != levellimitfloory)
                                        {

                                            buildBottomFace(xi, yi, zi, 1.0f);
                                        }
                                        else if (listofchunksadjacentfloor[4] != null && chunkPos[1] == levellimitfloory && yi != 0)
                                        {
                                            buildBottomFace(xi, yi, zi, 1.0f);
                                        }
                                        else if (listofchunksadjacent[4] != null && levelofdetail == 1 && chunkPos[1] == levellimitfloory && yi != 0)
                                        {
                                            buildBottomFace(xi, yi, zi, 1.0f);
                                        }

                                        if (levelofdetail != 1)
                                        {
                                            if (listofchunksadjacent[4] == null && chunkPos[1] == levellimitfloory)
                                            {
                                                buildBottomFace(xi, yi, zi, 1.0f);
                                            }
                                        
                                        }


                                        if (levelofdetail == 1)
                                        {
                                            if (chunkPos[1] == sclevelgen.wallheightsize - 1 && yi == 0)
                                            {
                                                buildBottomFace(xi, yi, zi, 1.0f);
                                            }
                                        
                                        }*/


                                        //buildBottomFace(xi, yi, zi, 1.0f);


                                        /*if (listofchunksadjacent[4] == null && chunkPos[1] == sclevelgen.wallheightsize) //&& levelofdetail == 1 // && levelofdetail == 1 
                                        {
                                            buildBottomFace(xi, yi, zi, 1.0f);
                                        }
                                        else if (listofchunksadjacent[4] == null && levelofdetail == 1 && chunkPos[1] == levellimitfloory)
                                        {
                                            //dont build
                                        }
                                        else
                                        {
                                            if (listofchunksadjacentfloor[4] != null && chunkPos[1] != levellimitfloory)
                                            {
                                                buildBottomFace(xi, yi, zi, 1.0f);
                                            }
                                        }*/

                                        //var somechunkpos = chunkPos;
                                        //somechunkPos[1] -= 1.0f;
                                        //var someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);

                                        /*if (listofchunksadjacentfloor[4] != null && chunkPos[1] != levellimitfloory)
                                        {
                                            buildBottomFace(xi, yi, zi, 1.0f);
                                        }



                                        if (levelofdetail != 1)
                                        {
                                            if (listofchunksadjacent[4] != null)
                                            {
                                                buildBottomFace(xi, yi, zi, 1.0f);
                                            }
                                        }*/
                                        //buildBottomFace(xi, yi, zi, 1.0f);
                                    }
                                }
                            }






                            if (IsTransparent(xi, yi + 1, zi))
                            {
                                if (yi != height - 1 && yi != 0)
                                {
                                    buildTopFace(xi, yi, zi, 1.0f);
                                }
                                else if (yi == 0)
                                {
                                    if (typeofterraintiles != 1115)// levelofdetail != 5)
                                    {
                                        buildTopFace(xi, yi, zi, 1.0f);
                                    }

                                }
                                else if (yi == height - 1)
                                {
                                    //buildTopFace(xi, yi, zi, 1.0f);
                                    //Vector3 somechunkpos = chunkPos;
                                    //somechunkPos[1] += 1.0f;
                                    //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkPos[1], somechunkpos.Z);


                                    if (listofchunksadjacent[5] != null && levelofdetail == 1 && chunkPos[1] != sclevelgen.wallheightsize - 1) //&& levelofdetail == 1
                                    {
                                        //Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                        if (listofchunksadjacent[5].map != null)
                                        {
                                            if (listofchunksadjacent[5].GetByte(xi, 0, zi) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                            {
                                                /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                var offset1 = forward * planeSize;
                                                var offset2 = right * planeSize;
                                                createTopFace(start + up * planeSize, offset1, offset2);*/

                                                /*if (typeofterraintiles != 0)
                                                {
                                                    Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                    var offset1 = forward * planeSize;
                                                    var offset2 = right * planeSize;
                                                    createTopFace(start + up * planeSize, offset1, offset2);
                                                }
                                                else
                                                {
                                                    buildTopFace(xi, yi, zi, 1.0f);
                                                }*/
                                                buildTopFace(xi, yi, zi, 1.0f);
                                            }
                                            else
                                            {
                                                /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                var offset1 = forward * planeSize;
                                                var offset2 = right * planeSize;
                                                createTopFace(start + up * planeSize, offset1, offset2);*/
                                                //buildTopFace(xi, yi, zi, 1.0f);
                                            }
                                        }
                                        else
                                        {
                                            //buildTopFace(xi, yi, zi, 1.0f);
                                        }
                                    }
                                    else
                                    {

                                        /*if (listofchunksadjacent[5] == null && levelofdetail != 1 && levelofdetail != 5 && chunkPos[1] == sclevelgen.wallheightsize - 1 && yi != height - 1)
                                        {
                                            buildTopFace(xi, yi, zi, 1.0f);
                                        }
                                        else if (listofchunksadjacent[5] == null && levelofdetail == 5 && chunkPos[1] == sclevelgen.wallheightsize - 1 && yi == height - 1)
                                        {
                                            //buildTopFace(xi, yi, zi, 1.0f);
                                        }

                                        if (listofchunksadjacent[5] != null && levelofdetail != 1 && chunkPos[1] != sclevelgen.wallheightsize - 1)
                                        {
                                            //buildTopFace(xi, yi, zi, 1.0f);
                                        }
                                        if (listofchunksadjacent[5] == null && levelofdetail != 1 && chunkPos[1] != sclevelgen.wallheightsize - 1)
                                        {
                                            buildTopFace(xi, yi, zi, 1.0f);
                                        }*/




                                        /*if (listofchunksadjacent[5] == null && chunkPos[1] != sclevelgen.wallheightsize - 1)
                                        {
                                            buildTopFace(xi, yi, zi, 1.0f);
                                        }
                                        else if (listofchunksadjacent[5] != null && chunkPos[1] != levellimitfloory)
                                        {
                                            buildTopFace(xi, yi, zi, 1.0f);
                                        }

                                        if (listofchunksadjacent[5] == null && chunkPos[1] == levellimitfloory)
                                        {
                                            buildTopFace(xi, yi, zi, 1.0f);
                                        }*/

                                        if (listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1101 ||
                                            listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1102 ||
                                            listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1103 ||
                                            listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1104 ||
                                            listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1105 ||
                                            listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1106 ||
                                            listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1107 ||
                                            listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1108 ||
                                            listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1109 ||
                                            listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1110 ||
                                            listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1111 ||
                                            listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1112 ||
                                            typeofterraintiles == 0)
                                        {
                                            buildTopFace(xi, yi, zi, 1.0f);
                                        }



                                        if (listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1101 && yi != height - 1 ||
                                           listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1102 && yi != height - 1 ||
                                           listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1103 && yi != height - 1 ||
                                           listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1104 && yi != height - 1 ||
                                           listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1105 && yi != height - 1 ||
                                           listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1106 && yi != height - 1 ||
                                           listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1107 && yi != height - 1 ||
                                           listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1108 && yi != height - 1 ||
                                           listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1109 && yi != height - 1 ||
                                           listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1110 && yi != height - 1 ||
                                           listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1111 && yi != height - 1 ||
                                           listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1112 && yi != height - 1)
                                        {
                                            buildTopFace(xi, yi, zi, 1.0f);
                                        }


                                        if (typeofterraintiles == 1115 && yi != height - 1)
                                        {
                                            buildTopFace(xi, yi, zi, 1.0f);
                                        }


                                        /*

                                        if (listofchunksadjacent[5] == null && chunkPos[1] == levellimitfloory)
                                        {
                                            buildTopFace(xi, yi, zi, 1.0f);
                                        }

                                        if (listofchunksadjacent[5] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1112)
                                        {

                                        }
                                     */


                                        /*
                                        if (listofchunksadjacent[5] != null && typeofterraintiles == 1101 ||
                                            listofchunksadjacent[5] != null && typeofterraintiles == 1102 ||
                                            listofchunksadjacent[5] != null && typeofterraintiles == 1103 ||
                                            listofchunksadjacent[5] != null && typeofterraintiles == 1104 ||
                                            listofchunksadjacent[5] != null && typeofterraintiles == 1105 ||
                                            listofchunksadjacent[5] != null && typeofterraintiles == 1106 ||
                                            listofchunksadjacent[5] != null && typeofterraintiles == 1107 ||
                                            listofchunksadjacent[5] != null && typeofterraintiles == 1108 ||
                                            listofchunksadjacent[5] != null && typeofterraintiles == 1109 ||
                                            listofchunksadjacent[5] != null && typeofterraintiles == 1110 ||
                                            listofchunksadjacent[5] != null && typeofterraintiles == 1111 ||
                                            listofchunksadjacent[5] != null && typeofterraintiles == 1112 ||
                                            listofchunksadjacent[5] != null && typeofterraintiles == 1115)
                                        {
                                            buildTopFace(xi, yi, zi, 1.0f);
                                        }*/








                                        //var somechunkpos = chunkPos;
                                        //somechunkPos[1] += (height) * planeSize;
                                        //var someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);

                                        /*if (listofchunksadjacent[5] == null && levelofdetail == 1 && chunkPos[1] == levellimitfloory) //&& levelofdetail == 1
                                        {
                                            buildTopFace(xi, yi, zi, 1.0f);
                                        }
                                        else if (listofchunksadjacent[5] == null && levelofdetail == 1 && chunkPos[1] == sclevelgen.wallheightsize)
                                        {
                                            //dont build
                                        }
                                        else
                                        {
                                            if (listofchunksadjacentfloor[5] != null && chunkPos[1] != sclevelgen.wallheightsize)
                                            {
                                                buildTopFace(xi, yi, zi, 1.0f);
                                            }
                                        }*/
                                        /*
                                        if (listofchunksadjacentfloor[5] != null && chunkPos[1] != sclevelgen.wallheightsize - 1)
                                        {

                                            buildTopFace(xi, yi, zi, 1.0f);
                                        }*/
                                        /*else if (listofchunksadjacentfloor[5] == null && chunkPos[1] == sclevelgen.wallheightsize - 1 && yi != height-1)
                                        {
                                            buildTopFace(xi, yi, zi, 1.0f);
                                        }*/



                                        /*
                                        if (listofchunksadjacent[5] == null && chunkPos[1] == sclevelgen.wallheightsize - 1 && yi != height - 1)
                                        {
                                            buildTopFace(xi, yi, zi, 1.0f);
                                        }

                                        if (listofchunksadjacent[5] != null && levelofdetail == 1 && chunkPos[1] == sclevelgen.wallheightsize - 1 && yi != height - 1)
                                        {
                                            buildTopFace(xi, yi, zi, 1.0f);
                                        }
                                        */
                                        /*if (levelofdetail != 1)
                                        {
                                            if (listofchunksadjacent[5] != null && chunkPos[1] == sclevelgen.wallheightsize-1)
                                            {
                                                buildTopFace(xi, yi, zi, 1.0f);
                                            }

                                        }*/


                                        //buildTopFace(xi, yi, zi, 1.0f);
                                    }
                                }
                            }







                            /*
                            if (IsTransparent(xi, yi, zi + 1))
                            {
                                if (zi != depth - 1 && zi != 0)
                                {
                                    buildFrontFace(xi, yi, zi, 1.0f);
                                }
                                else if (zi == 0)
                                {
                                    buildFrontFace(xi, yi, zi, 1.0f);
                                }
                                else if (zi == depth - 1)
                                {
                                    if (listofchunksadjacent[3] != null) // && levelofdetail == 1
                                    {
                                        //Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                        if (listofchunksadjacent[3].map != null)
                                        {
                                            if (listofchunksadjacent[3].GetByte(xi, yi, 0) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                            {
                                                /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                                                var offset1 = right * planeSize;
                                                var offset2 = up * planeSize;
                                                createFrontFace(start + forward * planeSize, offset1, offset2);
                                                buildFrontFace(xi, yi, zi, 1.0f);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (listofchunksadjacentfloor[3] != null)
                                        {
                                            buildFrontFace(xi, yi, zi, 1.0f);
                                        }

                                        if (levelofdetail != 1)
                                        {
                                            if (listofchunksadjacent[3] != null)
                                            {
                                                buildFrontFace(xi, yi, zi, 1.0f);
                                            }
                                        }
                                    }
                                }
                            }*/




                            if (IsTransparent(xi, yi, zi + 1))
                            {
                                if (zi != depth - 1 && zi != 0)
                                {
                                    buildFrontFace(xi, yi, zi, 1.0f);
                                }
                                else if (zi == 0)
                                {
                                    if (listofchunksadjacent[3] != null && typeofterraintiles == 1101 ||
                                        listofchunksadjacent[3] != null && typeofterraintiles == 1102 ||
                                        listofchunksadjacent[3] != null && typeofterraintiles == 1103 && levelofdetail != 5 ||
                                        //listofchunksadjacent[3] != null && typeofterraintiles == 1104 && levelofdetail != 5 ||
                                        listofchunksadjacent[3] != null && typeofterraintiles == 1105 ||
                                        listofchunksadjacent[3] != null && typeofterraintiles == 1106 ||
                                        listofchunksadjacent[3] != null && typeofterraintiles == 1107 ||
                                        listofchunksadjacent[3] != null && typeofterraintiles == 1108 ||
                                        listofchunksadjacent[3] != null && typeofterraintiles == 1109 ||
                                        listofchunksadjacent[3] != null && typeofterraintiles == 1110 ||
                                        listofchunksadjacent[3] != null && typeofterraintiles == 1111 ||
                                        listofchunksadjacent[3] != null && typeofterraintiles == 1112 ||
                                        listofchunksadjacent[3] != null && typeofterraintiles == 1115 ||
                                        listofchunksadjacent[3] != null && typeofterraintiles == 0)
                                    {
                                        buildFrontFace(xi, yi, zi, 1.0f);
                                    }

                                    if (listofchunksadjacent[3] == null && typeofterraintiles == 1101 ||
                                     listofchunksadjacent[3] == null && typeofterraintiles == 1102 ||
                                     listofchunksadjacent[3] == null && typeofterraintiles == 1103 ||
                                     listofchunksadjacent[3] == null && typeofterraintiles == 1104 && levelofdetail != 5 ||
                                     listofchunksadjacent[3] == null && typeofterraintiles == 1105 ||
                                     listofchunksadjacent[3] == null && typeofterraintiles == 1106 ||
                                     listofchunksadjacent[3] == null && typeofterraintiles == 1107 ||
                                     listofchunksadjacent[3] == null && typeofterraintiles == 1108 ||
                                     listofchunksadjacent[3] == null && typeofterraintiles == 1109 ||
                                     listofchunksadjacent[3] == null && typeofterraintiles == 1110 ||
                                     listofchunksadjacent[3] == null && typeofterraintiles == 1111 ||
                                     listofchunksadjacent[3] == null && typeofterraintiles == 1112 ||
                                     listofchunksadjacent[3] == null && typeofterraintiles == 1115 ||
                                     listofchunksadjacent[3] == null && typeofterraintiles == 0)
                                    {
                                        buildFrontFace(xi, yi, zi, 1.0f);
                                    }

                                    //if (levelofdetail != 5)
                                    {
                                        //buildFrontFace(xi, yi, zi, 1.0f);
                                    }

                                }
                                else if (zi == depth - 1)
                                {
                                    //Vector3 somechunkpos = chunkPos;
                                    //somechunkpos.Z += 1.0f;
                                    //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

                                    if (listofchunksadjacent[3] != null && levelofdetail == 1) // && levelofdetail == 1
                                    {
                                        //Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                        if (listofchunksadjacent[3].map != null)
                                        {
                                            if (listofchunksadjacent[3].GetByte(xi, yi, 0) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                            {
                                                /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                var offset1 = right * planeSize;
                                                var offset2 = up * planeSize;
                                                createFrontFace(start + forward * planeSize, offset1, offset2);*/

                                                /*if (typeofterraintiles != 0)
                                                {
                                                    Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                    var offset1 = right * planeSize;
                                                    var offset2 = up * planeSize;
                                                    createFrontFace(start + forward * planeSize, offset1, offset2);
                                                }
                                                else
                                                {
                                                    buildFrontFace(xi, yi, zi, 1.0f);
                                                }*/
                                                /*offset1 = left * planeSize;
                                                offset2 = up * planeSize;
                                                createFrontFace(start + right * planeSize, offset1, offset2);*/
                                                buildFrontFace(xi, yi, zi, 1.0f);
                                            }
                                            else
                                            {

                                            }
                                        }
                                        else
                                        {
                                            //buildFrontFace(xi, yi, zi, 1.0f);
                                        }
                                    }
                                    else
                                    {
                                        //buildFrontFace(xi, yi, zi, 1.0f);

                                        //var somechunkpos = chunkPos;
                                        //somechunkpos.Z += 1.0f;
                                        //var someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);

                                        /*if (listofchunksadjacentfloor[3] != null)
                                        {
                                            buildFrontFace(xi, yi, zi, 1.0f);
                                        }

                                        if (levelofdetail != 1)
                                        {
                                            if (listofchunksadjacent[3] != null)
                                            {
                                                buildFrontFace(xi, yi, zi, 1.0f);
                                            }
                                        }*/


                                        if (listofchunksadjacent[3] != null && typeofterraintiles == 1101 ||
                                         listofchunksadjacent[3] != null && typeofterraintiles == 1102 ||
                                         listofchunksadjacent[3] != null && typeofterraintiles == 1103 && levelofdetail != 5 ||
                                         //listofchunksadjacent[3] != null && typeofterraintiles == 1104 && levelofdetail != 5 ||
                                         listofchunksadjacent[3] != null && typeofterraintiles == 1105 ||
                                         listofchunksadjacent[3] != null && typeofterraintiles == 1106 ||
                                         listofchunksadjacent[3] != null && typeofterraintiles == 1107 ||
                                         listofchunksadjacent[3] != null && typeofterraintiles == 1108 ||
                                         listofchunksadjacent[3] != null && typeofterraintiles == 1109 ||
                                         listofchunksadjacent[3] != null && typeofterraintiles == 1110 ||
                                         listofchunksadjacent[3] != null && typeofterraintiles == 1111 ||
                                         listofchunksadjacent[3] != null && typeofterraintiles == 1112 ||
                                         listofchunksadjacent[3] != null && typeofterraintiles == 1115 ||
                                         listofchunksadjacent[3] != null && typeofterraintiles == 0)
                                        {
                                            buildFrontFace(xi, yi, zi, 1.0f);
                                        }

                                        if (listofchunksadjacent[3] == null && typeofterraintiles == 1101 ||
                                         listofchunksadjacent[3] == null && typeofterraintiles == 1102 ||
                                         listofchunksadjacent[3] == null && typeofterraintiles == 1103 && levelofdetail != 5 ||
                                         //listofchunksadjacent[3] == null && typeofterraintiles == 1104 && levelofdetail != 5 ||
                                         listofchunksadjacent[3] == null && typeofterraintiles == 1105 ||
                                         listofchunksadjacent[3] == null && typeofterraintiles == 1106 ||
                                         listofchunksadjacent[3] == null && typeofterraintiles == 1107 ||
                                         listofchunksadjacent[3] == null && typeofterraintiles == 1108 ||
                                         listofchunksadjacent[3] == null && typeofterraintiles == 1109 ||
                                         listofchunksadjacent[3] == null && typeofterraintiles == 1110 ||
                                         listofchunksadjacent[3] == null && typeofterraintiles == 1111 ||
                                         listofchunksadjacent[3] == null && typeofterraintiles == 1112 ||
                                         listofchunksadjacent[3] == null && typeofterraintiles == 1115 ||
                                         listofchunksadjacent[3] == null && typeofterraintiles == 0)
                                        {
                                            buildFrontFace(xi, yi, zi, 1.0f);
                                        }

                                    }
                                }
                            }





                            if (IsTransparent(xi, yi, zi - 1))
                            {
                                if (zi != depth - 1 && zi != 0)
                                {
                                    buildBackFace(xi, yi, zi, 1.0f);
                                }
                                else if (zi == depth - 1)
                                {
                                    if (listofchunksadjacent[2] != null && typeofterraintiles == 1101 ||
                                            listofchunksadjacent[2] != null && typeofterraintiles == 1102 ||
                                            //listofchunksadjacent[2] != null && typeofterraintiles == 1103 ||
                                            listofchunksadjacent[2] != null && typeofterraintiles == 1104 && levelofdetail != 5 ||
                                            listofchunksadjacent[2] != null && typeofterraintiles == 1105 ||
                                            listofchunksadjacent[2] != null && typeofterraintiles == 1106 ||
                                            listofchunksadjacent[2] != null && typeofterraintiles == 1107 ||
                                            listofchunksadjacent[2] != null && typeofterraintiles == 1108 ||
                                            listofchunksadjacent[2] != null && typeofterraintiles == 1109 ||
                                            listofchunksadjacent[2] != null && typeofterraintiles == 1110 ||
                                            listofchunksadjacent[2] != null && typeofterraintiles == 1111 ||
                                            listofchunksadjacent[2] != null && typeofterraintiles == 1112 ||
                                            listofchunksadjacent[2] != null && typeofterraintiles == 1115 ||
                                            listofchunksadjacent[2] != null && typeofterraintiles == 0)
                                    {
                                        buildBackFace(xi, yi, zi, 1.0f);
                                    }

                                    if (listofchunksadjacent[2] == null && typeofterraintiles == 1101 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1102 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1103 && levelofdetail != 5 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1104 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1105 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1106 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1107 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1108 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1109 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1110 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1111 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1112 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1115 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 0)
                                    {
                                        buildBackFace(xi, yi, zi, 1.0f);
                                    }
                                    //if (levelofdetail != 5)
                                    {
                                        //buildBackFace(xi, yi, zi, 1.0f);
                                    }
                                    /*if (listofchunksadjacent[2] != null && typeofterraintiles == 1101 ||
                                         listofchunksadjacent[2] != null && typeofterraintiles == 1102 ||
                                         listofchunksadjacent[2] != null && typeofterraintiles == 1103 && levelofdetail != 5 ||
                                         //listofchunksadjacent[2] != null && typeofterraintiles == 1104 ||
                                         listofchunksadjacent[2] != null && typeofterraintiles == 1105 ||
                                         listofchunksadjacent[2] != null && typeofterraintiles == 1106 ||
                                         listofchunksadjacent[2] != null && typeofterraintiles == 1107 ||
                                         listofchunksadjacent[2] != null && typeofterraintiles == 1108 ||
                                         listofchunksadjacent[2] != null && typeofterraintiles == 1109 ||
                                         listofchunksadjacent[2] != null && typeofterraintiles == 1110 ||
                                         listofchunksadjacent[2] != null && typeofterraintiles == 1111 ||
                                         listofchunksadjacent[2] != null && typeofterraintiles == 1112 ||
                                         listofchunksadjacent[2] != null && typeofterraintiles == 1115 ||
                                         listofchunksadjacent[2] != null && typeofterraintiles == 0)
                                    {
                                        buildBackFace(xi, yi, zi, 1.0f);
                                    }

                                    if (listofchunksadjacent[2] == null && typeofterraintiles == 1101 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1102 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1103 && levelofdetail != 5 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1104  ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1105 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1106 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1107 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1108 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1109 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1110 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1111 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1112 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 1115 ||
                                     listofchunksadjacent[2] == null && typeofterraintiles == 0)
                                    {
                                        buildBackFace(xi, yi, zi, 1.0f);
                                    }*/
                                }
                                else if (zi == 0)
                                {

                                    //Vector3 somechunkpos = chunkPos;
                                    //somechunkpos.Z -= 1.0f;
                                    //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

                                    if (listofchunksadjacent[2] != null && levelofdetail == 1) //
                                    {
                                        //Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                        if (listofchunksadjacent[2].map != null)
                                        {

                                            //Console.WriteLine("x:" + xi + "/y:" + yi + "/z:" + zi + "depth:" + depth);
                                            if (listofchunksadjacent[2].GetByte(xi, yi, depth - 1) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                            {

                                                /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;

                                                var offset1 = left * planeSize;
                                                var offset2 = up * planeSize;
                                                createBackFace(start + right * planeSize, offset1, offset2);*/
                                                /*if (typeofterraintiles != 0)
                                                {
                                                    Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;

                                                    var offset1 = left * planeSize;
                                                    var offset2 = up * planeSize;
                                                    createBackFace(start + right * planeSize, offset1, offset2);
                                                }
                                                else
                                                {
                                                    buildBackFace(xi, yi, zi, 1.0f);
                                                }*/
                                                buildBackFace(xi, yi, zi, 1.0f);
                                            }
                                            else
                                            {

                                            }
                                        }
                                        else
                                        {
                                            //buildBackFace(xi, yi, zi, 1.0f);
                                        }
                                    }
                                    else
                                    {
                                        //var somechunkpos = chunkPos;
                                        //somechunkpos.Z -= 1.0f;
                                        //var someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);

                                        /*if (listofchunksadjacentfloor[2] != null)
                                        {
                                            buildBackFace(xi, yi, zi, 1.0f);
                                        }


                                        if (levelofdetail != 1)
                                        {
                                            if (listofchunksadjacent[2] != null)
                                            {
                                                buildBackFace(xi, yi, zi, 1.0f);
                                            }

                                        }*/
                                        if (listofchunksadjacent[2] != null && typeofterraintiles == 1101 ||
                                          listofchunksadjacent[2] != null && typeofterraintiles == 1102 ||
                                          //listofchunksadjacent[2]!= null && typeofterraintiles == 1103||
                                          listofchunksadjacent[2] != null && typeofterraintiles == 1104 && levelofdetail != 5 ||
                                          listofchunksadjacent[2] != null && typeofterraintiles == 1105 ||
                                          listofchunksadjacent[2] != null && typeofterraintiles == 1106 ||
                                          listofchunksadjacent[2] != null && typeofterraintiles == 1107 ||
                                          listofchunksadjacent[2] != null && typeofterraintiles == 1108 ||
                                          listofchunksadjacent[2] != null && typeofterraintiles == 1109 ||
                                          listofchunksadjacent[2] != null && typeofterraintiles == 1110 ||
                                          listofchunksadjacent[2] != null && typeofterraintiles == 1111 ||
                                          listofchunksadjacent[2] != null && typeofterraintiles == 1112 ||
                                          listofchunksadjacent[2] != null && typeofterraintiles == 1115 ||
                                          listofchunksadjacent[2] != null && typeofterraintiles == 0)
                                        {
                                            buildBackFace(xi, yi, zi, 1.0f);
                                        }

                                        if (listofchunksadjacent[2] == null && typeofterraintiles == 1101 ||
                                         listofchunksadjacent[2] == null && typeofterraintiles == 1102 ||
                                         //listofchunksadjacent[2]== null && typeofterraintiles == 1103  ||
                                         listofchunksadjacent[2] == null && typeofterraintiles == 1104 && levelofdetail != 5 ||
                                         listofchunksadjacent[2] == null && typeofterraintiles == 1105 ||
                                         listofchunksadjacent[2] == null && typeofterraintiles == 1106 ||
                                         listofchunksadjacent[2] == null && typeofterraintiles == 1107 ||
                                         listofchunksadjacent[2] == null && typeofterraintiles == 1108 ||
                                         listofchunksadjacent[2] == null && typeofterraintiles == 1109 ||
                                         listofchunksadjacent[2] == null && typeofterraintiles == 1110 ||
                                         listofchunksadjacent[2] == null && typeofterraintiles == 1111 ||
                                         listofchunksadjacent[2] == null && typeofterraintiles == 1112 ||
                                         listofchunksadjacent[2] == null && typeofterraintiles == 1115 ||
                                         listofchunksadjacent[2] == null && typeofterraintiles == 0)
                                        {
                                            buildBackFace(xi, yi, zi, 1.0f);
                                        }


                                        //buildBackFace(xi, yi, zi, 1.0f);
                                    }
                                }
                                else
                                {
                                    //Console.WriteLine("test");
                                }
                            }






                        }





                        //CalculateintsForFaces();
                        /*if (swtchz == 0)
                        {
                            CreateChunkFaces();
                            //CalculateintsForFaces();


                        }
                        else
                        {
                            break;
                        }*/
                    }
                }
            }
        }



        public void RegenerateNotReduced()
        {
            /*if (vertexlist == null)
             {
                 //vertexlist = new List<sclevelgenclass.DVertex>();

             }
             else
             {
                 vertexlist.Clear();
             }

             if (listOfTriangleIndices == null)
             {
                 //listOfTriangleIndices = new List<int>();
             }
             else
             {
                 listOfTriangleIndices.Clear();
             }*/

            //_newVertzCounter = 0;
            vertexlist.Clear();
            listOfTriangleIndices.Clear();

            /*_tempChunkArrayBottomFace = new int[width * height * depth];
            _tempChunkArrayBackFace = new int[width * height * depth];
            _tempChunkArrayFrontFace = new int[width * height * depth];
            _tempChunkArrayLeftFace = new int[width * height * depth];
            _tempChunkArrayRightFace = new int[width * height * depth];
            _tempChunkArray = new int[width * height * depth];

            _chunkArray = new int[width * height * depth];

            _chunkVertexArray0 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray1 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray2 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray3 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray4 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray5 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

            _testVertexArray0 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray1 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray2 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray3 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray4 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray5 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];*/
            //normalslist.Clear();
            //colorslist.Clear();
            //indexPoslist.Clear();
            //textureslist.Clear();

            /*
            int xi = 0;
            int yi = 0;
            int zi = 0;*/


            //CalculateintsForFaces();
            //CreateChunkFaces();

            xi = 0;
            yi = 0;
            zi = 0;

            for (int x = 0; x < width; x++)
            {
                xi = x;
                for (int y = 0; y < height; y++)
                {
                    yi = y;
                    for (int z = 0; z < depth; z++)
                    {
                        zi = z;










                        /*
                        if (IsTransparent(xi - 1, yi, zi))
                        {
                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                            var offset1 = back * planeSize;
                            var offset2 = down * planeSize;
                            createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2);
                        }*/


                        /*
                        if (IsTransparent(xi + 1, yi, zi))
                        {
                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                            var offset1 = up * planeSize;
                            var offset2 = forward * planeSize;
                            createRightFace(start + right * planeSize, offset1, offset2);


                        }


                        if (IsTransparent(xi, yi - 1, zi))
                        {
                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                            var offset1 = right * planeSize;
                            var offset2 = forward * planeSize;
                            createBottomFace(start, offset1, offset2);


                        }


                        if (IsTransparent(xi, yi + 1, zi))
                        {
                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                            var offset1 = forward * planeSize;
                            var offset2 = right * planeSize;
                            createTopFace(start + up * planeSize, offset1, offset2);
                        }



                        if (IsTransparent(xi, yi, zi + 1))
                        {
                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                            var offset1 = right * planeSize;
                            var offset2 = up * planeSize;
                            createFrontFace(start + forward * planeSize, offset1, offset2);
                        }


                        if (IsTransparent(xi, yi, zi - 1))
                        {
                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;

                            var offset1 = left * planeSize;
                            var offset2 = up * planeSize;
                            createBackFace(start + right * planeSize, offset1, offset2);
                        }
                        */


                    }
                }
            }
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
                            counterCreateChunkObjectFacesints = 0;

                            tints = 0;

                            posxints = 0;
                            posyints = 0;
                            poszints = 0;

                            xxints = 0;
                            yyints = 0;
                            zzints = 0;

                            xiints = 0;
                            yiints = 0;
                            ziints = 0;

                            swtchxints = 0;
                            swtchyints = 0;
                            swtchzints = 0;

                            rowIterateXints = 0;
                            rowIterateYints = 0;
                            rowIterateZints = 0;

                            _maxWidth = width;
                            _maxHeight = height;
                            _maxDepth = depth;

                            foundVertOne = false;
                            foundVertTwo = false;
                            foundVertThree = false;
                            foundVertFour = false;

                            if (swtchzints == 0)
                            {


                                CalculateintsForFaces();
                            }

                            /*for (int i = 0; i < totalints; i++)
                            {
                                if (swtchzints == 0)
                                {
                                    CalculateintsForFaces();
                                }
                                else
                                {
                                    i = totalints;
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

                _maxWidth = 0;
                _maxHeight = 0;
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

                tints = 0;

                posxints = 0;
                posyints = 0;
                poszints = 0;

                xxints = 0;
                yyints = 0;
                zzints = 0;

                xiints = 0;
                yiints = 0;
                ziints = 0;

                swtchxints = 0;
                swtchyints = 0;
                swtchzints = 0;
                counterCreateChunkObjectFacesints = 0;



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
                        _chunkVertexArray0[t] = 0;
                        _chunkVertexArray1[t] = 0;
                        _chunkVertexArray2[t] = 0;
                        _chunkVertexArray3[t] = 0;
                        _chunkVertexArray4[t] = 0;
                        _chunkVertexArray5[t] = 0;

                        _testVertexArray0[t] = 0;
                        _testVertexArray1[t] = 0;
                        _testVertexArray2[t] = 0;
                        _testVertexArray3[t] = 0;
                        _testVertexArray4[t] = 0;
                        _testVertexArray5[t] = 0;
                    }
                }
            }
        }
























        void CalculateintsForFaces()
        {
            if (tints < totalints)
            {
                posxints = (xxints);
                posyints = (yyints);
                poszints = (zzints);

                Vector3 somepos = new Vector3(posxints, posyints, poszints);

                xiints = xxints;
                yiints = yyints;
                ziints = zzints;

                rowIterateXints = xiints + xi;
                rowIterateYints = yiints + yi;
                rowIterateZints = ziints + zi;

                var indexints = rowIterateXints + (width) * (rowIterateYints + (height) * rowIterateZints);

                //Debug.Log("xiints:" + xiints + "/yiints:" + yiints + "/ziints:" + ziints);
                if (indexints < totalints)
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
                    /*
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

                    */








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
                    if (IsTransparent(xi - 1, yi, zi))
                    {
                        buildTopLeft(xi, yi, zi, 1.0f);
                    }*/




                    //LEFT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                    /*if (IsTransparent(xi - 1, yi, zi))
                    {
                        int someswtcLeft = 0;



                        if (componentparent.getChunklod0(somevalueforLeftx, somevalueforLefty, somevalueforLeftz) != null)
                        {
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somevalueforLeftx, somevalueforLefty, somevalueforLeftz);

                            if (xi == 0 && someChunk.map != null)
                            {
                                if (someChunk.map != null)
                                {
                                    if (someChunk.IsTransparent(width - 1, yi, zi))
                                    {
                                        //someswtcLeft = 1;
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
                                //if (componentParent.getChunklod0(somevalueforLeftx, somevalueforLefty, somevalueforLeftz) == null)
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
                            buildTopLeft(xi, yi, zi, 1.0f);
                        }
                        //buildTopLeft();
                    }*/





                    /*
                    Vector3 somechunkpos = chunkPos;
                    somechunkpos.X -= (width) * planeSize;
                    sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);
                    if (someChunk != null)
                    {
                        if (someChunk.map != null)
                        {
                            if (someChunk.IsTransparent(width - 1, yi, zi)) //someChunk.GetByte(width - 1, yi, zi) == 0)
                            {
                            }
                        }
                    }*/







                    if (IsTransparent(xi - 1, yi, zi))
                    {
                        if (xi != width - 1 && xi != 0)
                        {
                            buildTopLeft(xi, yi, zi, 1.0f);
                        }
                        else if (xi == width - 1)
                        {
                            buildTopLeft(xi, yi, zi, 1.0f);
                        }
                        else if (xi == 0)
                        {
                            //Vector3 somechunkpos = chunkPos;
                            //somechunkpos.X -= 1.0f;
                            //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

                            if (listofchunksadjacent[0] != null)
                            {
                                //Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                if (listofchunksadjacent[0].map != null)
                                {
                                    if (listofchunksadjacent[0].GetByte(width - 1, yi, zi) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                    {
                                        /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                        var offset1 = back * planeSize;
                                        var offset2 = down * planeSize;
                                        createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2);
                                        */

                                        /*if (typeofterraintiles != 0)
                                        {
                                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                            var offset1 = back * planeSize;
                                            var offset2 = down * planeSize;
                                            createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2);
                                        }
                                        else
                                        {
                                            buildTopLeft(xi, yi, zi, 1.0f);
                                        }*/

                                        buildTopLeft(xi, yi, zi, 1.0f);
                                    }
                                    else
                                    {
                                        //buildTopLeft(xi, yi, zi, 1.0f);
                                    }
                                }
                                else
                                {
                                    //buildTopLeft(xi, yi, zi, 1.0f);
                                }
                            }
                            else
                            {
                                //var somechunkpos = chunkPos;
                                //somechunkpos.X -= 1.0f;
                                //var someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);

                                if (listofchunksadjacentfloor[0] != null)
                                {
                                    buildTopLeft(xi, yi, zi, 1.0f);
                                }

                                //buildTopLeft(xi, yi, zi, 1.0f);
                            }
                        }
                    }



                    if (IsTransparent(xi + 1, yi, zi))
                    {
                        if (xi != width - 1 && xi != 0)
                        {
                            buildTopRight(xi, yi, zi, 1.0f);
                        }
                        else if (xi == 0)
                        {
                            buildTopRight(xi, yi, zi, 1.0f);
                        }
                        else if (xi == width - 1)
                        {

                            //Vector3 somechunkpos = chunkPos;
                            //somechunkpos.X += 1.0f;
                            //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);


                            if (listofchunksadjacent[1] != null)
                            {
                                //Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                if (listofchunksadjacent[1].map != null)
                                {
                                    if (listofchunksadjacent[1].GetByte(0, yi, zi) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                    {
                                        /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                        var offset1 = up * planeSize;
                                        var offset2 = forward * planeSize;
                                        createRightFace(start + right * planeSize, offset1, offset2);*/
                                        /*
                                        if (typeofterraintiles != 0)
                                        {
                                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                            var offset1 = up * planeSize;
                                            var offset2 = forward * planeSize;
                                            createRightFace(start + right * planeSize, offset1, offset2);
                                        }
                                        else
                                        {
                                            buildTopRight(xi, yi, zi, 1.0f);
                                        }*/


                                        buildTopRight(xi, yi, zi, 1.0f);
                                    }
                                    else
                                    {

                                    }
                                }
                                else
                                {
                                    //buildTopRight(xi, yi, zi, 1.0f);
                                }
                            }
                            else
                            {
                                //var somechunkpos = chunkPos;
                                //somechunkpos.X += 1.0f;
                                //var someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);

                                if (listofchunksadjacentfloor[1] != null)
                                {

                                    buildTopRight(xi, yi, zi, 1.0f);
                                }
                                //buildTopRight(xi, yi, zi, 1.0f);
                            }
                        }
                    }




                    /*
                    if (IsTransparent(xi, yi - 1, zi))
                    {
                        if (yi != height - 1 && yi != 0)
                        {
                            buildBottomFace(xi, yi, zi, 1.0f);
                        }
                        else if (yi == height - 1)
                        {
                            buildBottomFace(xi, yi, zi, 1.0f);
                        }
                        else if (yi == 0)
                        {

                            //Vector3 somechunkpos = chunkPos;
                            //somechunkpos.Y -= 1.0f;
                            //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

                            if (listofchunksadjacent[4] != null)
                            {
                                //Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                if (listofchunksadjacent[4].map != null)
                                {
                                    if (listofchunksadjacent[4].GetByte(xi, height - 1, zi) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                    {
                                        /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                        var offset1 = right * planeSize;
                                        var offset2 = forward * planeSize;
                                        createBottomFace(start, offset1, offset2);*/
                    /*if (typeofterraintiles != 0)
                    {
                        Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                        var offset1 = right * planeSize;
                        var offset2 = forward * planeSize;
                        createBottomFace(start, offset1, offset2);
                    }
                    else
                    {
                        buildBottomFace(xi, yi, zi, 1.0f);
                    }
                    buildBottomFace(xi, yi, zi, 1.0f);
                }
                else
                {
                    //buildBottomFace(xi, yi, zi, 1.0f);
                }
            }
            else
            {
                //buildBottomFace(xi, yi, zi, 1.0f);
            }
        }
        else
        {
            //var somechunkpos = chunkPos;
            //somechunkpos.Y -= 1.0f;
            //var someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);

            if (listofchunksadjacentfloor[4] != null && chunkPos.Y != levellimitfloory)
            {

                buildBottomFace(xi, yi, zi, 1.0f);
            }
            //buildBottomFace(xi, yi, zi, 1.0f);
        }
    }
}*/


                    if (IsTransparent(xi, yi + 1, zi))
                    {
                        if (yi != height - 1 && yi != 0)
                        {
                            buildTopFace(xi, yi, zi, 1.0f);
                        }
                        else if (yi == 0)
                        {
                            buildTopFace(xi, yi, zi, 1.0f);
                        }
                        else if (yi == height - 1)
                        {
                            //buildTopFace(xi, yi, zi, 1.0f);
                            //Vector3 somechunkpos = chunkPos;
                            //somechunkpos.Y += 1.0f;
                            //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);


                            if (listofchunksadjacent[5] != null)
                            {
                                //Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                if (listofchunksadjacent[5].map != null)
                                {
                                    if (listofchunksadjacent[5].GetByte(xi, 0, zi) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                    {
                                        /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                        var offset1 = forward * planeSize;
                                        var offset2 = right * planeSize;
                                        createTopFace(start + up * planeSize, offset1, offset2);*/

                                        /*if (typeofterraintiles != 0)
                                        {
                                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                            var offset1 = forward * planeSize;
                                            var offset2 = right * planeSize;
                                            createTopFace(start + up * planeSize, offset1, offset2);
                                        }
                                        else
                                        {
                                            buildTopFace(xi, yi, zi, 1.0f);
                                        }*/
                                        buildTopFace(xi, yi, zi, 1.0f);
                                    }
                                    else
                                    {
                                        //buildTopFace(xi, yi, zi, 1.0f);
                                    }
                                }
                                else
                                {
                                    //buildTopFace(xi, yi, zi, 1.0f);
                                }
                            }
                            else
                            {
                                //var somechunkpos = chunkPos;
                                //somechunkpos.Y += (height) * planeSize;
                                //var someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);

                                if (listofchunksadjacentfloor[5] != null && chunkPos[1] != levellimitroofy)
                                {

                                    buildTopFace(xi, yi, zi, 1.0f);
                                }

                                //buildTopFace(xi, yi, zi, 1.0f);
                            }
                        }
                    }



                    if (IsTransparent(xi, yi, zi + 1))
                    {
                        if (zi != depth - 1 && zi != 0)
                        {
                            buildFrontFace(xi, yi, zi, 1.0f);
                        }
                        else if (zi == 0)
                        {
                            buildFrontFace(xi, yi, zi, 1.0f);
                        }
                        else if (zi == depth - 1)
                        {

                            //Vector3 somechunkpos = chunkPos;
                            //somechunkpos.Z += 1.0f;
                            //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

                            if (listofchunksadjacent[3] != null)
                            {
                                //Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                if (listofchunksadjacent[3].map != null)
                                {
                                    if (listofchunksadjacent[3].GetByte(xi, yi, 0) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                    {

                                        /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                        var offset1 = right * planeSize;
                                        var offset2 = up * planeSize;
                                        createFrontFace(start + forward * planeSize, offset1, offset2);*/

                                        /*if (typeofterraintiles != 0)
                                        {
                                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                            var offset1 = right * planeSize;
                                            var offset2 = up * planeSize;
                                            createFrontFace(start + forward * planeSize, offset1, offset2);
                                        }
                                        else
                                        {
                                            buildFrontFace(xi, yi, zi, 1.0f);
                                        }*/
                                        /*offset1 = left * planeSize;
                                        offset2 = up * planeSize;
                                        createFrontFace(start + right * planeSize, offset1, offset2);*/
                                        buildFrontFace(xi, yi, zi, 1.0f);
                                    }
                                    else
                                    {

                                    }
                                }
                                else
                                {
                                    //buildFrontFace(xi, yi, zi, 1.0f);
                                }
                            }
                            else
                            {
                                //buildFrontFace(xi, yi, zi, 1.0f);

                                //var somechunkpos = chunkPos;
                                //somechunkpos.Z += 1.0f;
                                //var someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);

                                if (listofchunksadjacentfloor[3] != null)
                                {
                                    buildFrontFace(xi, yi, zi, 1.0f);
                                }
                            }
                        }
                    }


                    if (IsTransparent(xi, yi, zi - 1))
                    {
                        if (zi != depth - 1 && zi != 0)
                        {
                            buildBackFace(xi, yi, zi, 1.0f);
                        }
                        else if (zi == depth - 1)
                        {
                            buildBackFace(xi, yi, zi, 1.0f);
                        }
                        else if (zi == 0)
                        {

                            //Vector3 somechunkpos = chunkPos;
                            //somechunkpos.Z -= 1.0f;
                            //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

                            if (listofchunksadjacent[2] != null)
                            {
                                //Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                if (listofchunksadjacent[2].map != null)
                                {
                                    if (listofchunksadjacent[2].GetByte(xi, yi, depth - 1) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                    {

                                        /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;

                                        var offset1 = left * planeSize;
                                        var offset2 = up * planeSize;
                                        createBackFace(start + right * planeSize, offset1, offset2);*/
                                        /*if (typeofterraintiles != 0)
                                        {
                                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;

                                            var offset1 = left * planeSize;
                                            var offset2 = up * planeSize;
                                            createBackFace(start + right * planeSize, offset1, offset2);
                                        }
                                        else
                                        {
                                            buildBackFace(xi, yi, zi, 1.0f);
                                        }*/
                                        buildBackFace(xi, yi, zi, 1.0f);
                                    }
                                    else
                                    {

                                    }
                                }
                                else
                                {
                                    //buildBackFace(xi, yi, zi, 1.0f);
                                }
                            }
                            else
                            {
                                //var somechunkpos = chunkPos;
                                //somechunkpos.Z -= 1.0f;
                                //var someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);

                                if (listofchunksadjacentfloor[2] != null)
                                {
                                    buildBackFace(xi, yi, zi, 1.0f);
                                }
                                //buildBackFace(xi, yi, zi, 1.0f);
                            }
                        }
                        else
                        {
                            //Console.WriteLine("test");
                        }
                    }


                    /*if (IsTransparent(xi - 1, yi, zi))
                    {

                        if (xi == 0)
                        {
                            //buildTopLeft(xi, yi, zi, 1.0f);
                            Vector3 somechunkpos = chunkPos;
                            somechunkpos.X -= (1.0f * width) * planeSize;
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);
                            if (someChunk != null)
                            {
                                if (someChunk.map != null)
                                {
                                    if (someChunk.IsTransparent(width - 1, yi, zi))
                                    {
                                        buildTopLeft(xi, yi, zi, 1.0f);
                                    }
                                    else
                                    {

                                    }
                                }
                            }



                        }
                        else if (xi != width - 1)
                        {
                            buildTopLeft(xi, yi, zi, 1.0f);
                        }
                        else if (xi == width - 1)
                        {
                            //buildTopLeft(xi, yi, zi, 1.0f);
                        }







                        /*Vector3 somechunkpos = chunkPos;
                        somechunkpos.X -= (1.0f * width) * planeSize;

                        if (componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z) != null)
                        {
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X,0, somechunkpos.Z);

                            if (someChunk != null)
                            {
                                if (someChunk.map != null)
                                {
                                    if (xi == 0)
                                    {
                                        //someswtcLeft = 1;

                                        if (someChunk.IsTransparent(width - 1, yi, zi))
                                        {
                                            //someswtcLeft = 1;
                                            buildTopLeft(xi, yi, zi, 1.0f);
                                        }
                                        else
                                        {
                                            //Console.WriteLine("TEST");
                                            //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.GameObject.position, Quaternion.identity);
                                            //someObject.GameObject.parent = chunkdata.planetchunk.GameObject;
                                        }
                                    }
                                    else if ( xi != width-1)
                                    {
                                        //someswtcLeft = 1;
                                        //buildTopLeft(xi, yi, zi, 1.0f);
                                    }
                                    else if (xi == width - 1)
                                    {
                                        //buildTopLeft(xi, yi, zi, 1.0f);
                                    }

                                    //buildTopLeft(xi, yi, zi, 1.0f);
                                }
                            }
                        }
                    }*/










                    //LEFT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    /*if (IsTransparent(xi - 1, yi, zi))
                    {
                        int someswtcLeft = 0;

                        Vector3 somechunkpos = chunkPos;
                        somechunkpos.X -= (1.0f * width) * planeSize;

                        if (componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z) != null)
                        {
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

                            if (someChunk != null)
                            {

                                if (someChunk.map != null)
                                {
                                    if (xi == 0)
                                    {
                                        //someswtcLeft = 1;

                                        if (someChunk.IsTransparent(width - 1, yi, zi))
                                        {
                                            //someswtcLeft = 1;
                                            buildTopLeft(xi, yi, zi, 1.0f);
                                        }
                                        else
                                        {
                                            //Console.WriteLine("TEST");
                                            //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.GameObject.position, Quaternion.identity);
                                            //someObject.GameObject.parent = chunkdata.planetchunk.GameObject;
                                        }
                                    }
                                    else //if (xi > 0 && xi < width)
                                    {
                                        someswtcLeft = 1;                          
                                    }

                                }
                                else
                                {
                                    someswtcLeft = 1;
                                }
                            }
                            else
                            {
                                someswtcLeft = 1;
                            }
                        }
                        else
                        {
                            if (xi > 0 && xi < width -1)
                            {
                                //buildTopLeft(xi, yi, zi, 1.0f);
                                someswtcLeft = 1;
                            }
                            else
                            {
                                //someswtcLeft = 0;
                            }
                        }

                        if (someswtcLeft == 1)
                        {
                            buildTopLeft(xi, yi, zi, 1.0f);
                            //buildTopLeft(xi, yi, zi, 1.0f);

                            //buildTopLeft();
                            //if (IsTransparent(xi - 1, yi, zi))
                            //{
                            //buildTopLeft(xi, yi, zi, 1.0f);
                            //}
                        }
                    }*/


                    /*//RIGHT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    if (IsTransparent(xi + 1, yi, zi))
                    {
                        int someswtcright = 0;

                        Vector3 somechunkpos = chunkPos;
                        somechunkpos.X += (1.0f * width) * planeSize;

                        if (componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z) != null)
                        {
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

                            if (someChunk != null)
                            {

                                if (someChunk.map != null)
                                {
                                    if (xi == width - 1)
                                    {

                                        if (someChunk.IsTransparent(0, yi, zi))
                                        {
                                            //buildTopRight(xi, yi, zi, 1.0f);
                                            someswtcright = 1;
                                        }
                                        else
                                        {
                                            //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.GameObject.position, Quaternion.identity);
                                            //someObject.GameObject.parent = chunkdata.planetchunk.GameObject;
                                        }

                                    }
                                    else
                                    {
                                        someswtcright = 1;
                                    }
                                    //else if (xi != width - 1)
                                    //{
                                    //    //buildTopRight(xi, yi, zi, 1.0f);
                                    //    someswtcright = 1;
                                    //}
                                }

                                else
                                {
                                    //if (componentparent.getChunklod0(somevalueforRightx, somevalueforRighty, somevalueforRightz) == null)
                                    //{
                                    //    someswtcRight = 1;
                                    //}
                                    //else
                                    //{
                                    //    someswtcRight = 1;
                                    //}
                                    someswtcright = 1;
                                }
                            }
                            else
                            {
                                //if (componentparent.getChunklod0(somevalueforRightx, somevalueforRighty, somevalueforRightz) == null)
                                //{
                                //    someswtcRight = 1;
                                //}
                                //else
                                //{
                                //    someswtcRight = 1;
                                //}
                                someswtcright = 1;
                            }
                        }
                        else
                        {
                            if (xi < width -1)
                            {
                                buildTopRight(xi, yi, zi, 1.0f);
                            }
                            //someswtcright = 0;
                        }

                        if (someswtcright == 1)
                        {
                            buildTopRight(xi, yi, zi, 1.0f);
                            //buildTopLeft();
                            //if (IsTransparent(xi - 1, yi, zi))
                            //{
                            //buildTopLeft(xi, yi, zi, 1.0f);
                            //}
                        }
                    }



                    //FRONT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi, zi+1))
                    {
                        int someswtcFront = 0;

                        Vector3 somechunkpos = chunkPos;
                        somechunkpos.Z += (1.0f * depth) * planeSize;

                        if (componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z) != null)
                        {
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

                            if (someChunk != null)
                            {
                                if (someChunk.map != null)
                                {
                                    if (zi == depth - 1)
                                    {

                                        if (someChunk.IsTransparent(xi, yi, 0))
                                        {
                                            someswtcFront = 1;
                                        }
                                        else
                                        {
                                            //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.GameObject.position, Quaternion.identity);
                                            //someObject.GameObject.parent = chunkdata.planetchunk.GameObject;
                                        }

                                    }
                                    //else if (zi != depth -1)
                                    //{
                                    //    someswtcFront = 1;
                                    //}
                                    else
                                    {
                                        //if (componentparent.getChunklod0(somevalueforTopx, somevalueforTopy, somevalueforTopz) == null)
                                        //{
                                        //    someswtcTop = 1;
                                        //}
                                        someswtcFront = 1;
                                    }
                                }
                                else
                                {
                                    //if (componentparent.getChunklod0(somevalueforTopx, somevalueforTopy, somevalueforTopz) == null)
                                    //{
                                    //    someswtcTop = 1;
                                    //}
                                    someswtcFront = 1;
                                }
                            }
                            else
                            {
                                //if (componentparent.getChunklod0(somevalueforTopx, somevalueforTopy, somevalueforTopz) == null)
                                //{
                                //    someswtcTop = 1;
                                //}
                                someswtcFront = 1;
                            }
                        }
                        else
                        {
                            if (zi < depth -1)
                            {
                                buildFrontFace(xi, yi, zi, 1.0f);
                            }
                            //someswtcFront = 0;
                        }

                        if (someswtcFront == 1)
                        {
                            buildFrontFace(xi, yi, zi, 1.0f);
                            //buildTopLeft();
                            //if (IsTransparent(xi - 1, yi, zi))
                            //{
                            //buildTopLeft(xi, yi, zi, 1.0f);
                            //}
                        }
                    }


                    //BACK FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi, zi-1))
                    {
                        int someswtcBack = 0;

                        Vector3 somechunkpos = chunkPos;
                        somechunkpos.Z -= (1.0f * depth) * planeSize;

                        if (componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z) != null)
                        {
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);
                            if (someChunk != null)
                            {
                                if (someChunk.map != null)
                                {
                                    if (zi == 0)
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
                                    //else if (zi != 0)
                                    //{
                                    //    someswtcBack = 1;
                                    //}
                                    else
                                    {
                                        //if (componentparent.getChunklod0(somevalueforBackx, somevalueforBacky, somevalueforBackz) == null)
                                        //{
                                        //    someswtcBack = 1;
                                        //}
                                        someswtcBack = 1;
                                    }
                                }
                                else
                                {
                                    //if (componentparent.getChunklod0(somevalueforBackx, somevalueforBacky, somevalueforBackz) == null)
                                    //{
                                    //    someswtcBack = 1;
                                    //}
                                    someswtcBack = 1;
                                }
                            }
                            else
                            {
                                //if (componentparent.getChunklod0(somevalueforBackx, somevalueforBacky, somevalueforBackz) == null)
                                //{
                                //    someswtcBack = 1;
                                //}
                                someswtcBack = 1;
                            }
                        }
                        else
                        {
                            if (zi > 0)
                            {
                                buildBackFace(xi, yi, zi, 1.0f);
                            }
                            //someswtcBack = 0;
                        }

                        if (someswtcBack == 1)
                        {
                            buildBackFace(xi, yi, zi, 1.0f);
                            //buildTopLeft();
                            //if (IsTransparent(xi - 1, yi, zi))
                            //{
                            //buildTopLeft(xi, yi, zi, 1.0f);
                            //}
                        }
                    }



                    //TOP FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi+1, zi))
                    {
                        int someswtcTop = 0;

                        Vector3 somechunkpos = chunkPos;
                        somechunkpos.Y += (1.0f * width) * planeSize;

                        if (componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z) != null)
                        {
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

                            if (someChunk != null)
                            {

                                if (someChunk.map != null)
                                {
                                    if (yi == height - 1)
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
                                    //else if (yi != height - 1)
                                    //{
                                    //    someswtcTop = 1;
                                    //}
                                    else
                                    {
                                        //if (componentparent.getChunklod0(somevalueforTopx, somevalueforTopy, somevalueforTopz) == null)
                                        //{
                                        //    someswtcTop = 1;
                                        //}
                                        someswtcTop = 1;
                                    }
                                }
                                else
                                {
                                    //if (componentparent.getChunklod0(somevalueforTopx, somevalueforTopy, somevalueforTopz) == null)
                                    //{
                                    //    someswtcTop = 1;
                                    //}
                                    someswtcTop = 1;
                                }

                            }
                            else
                            {
                                //if (componentparent.getChunklod0(somevalueforTopx, somevalueforTopy, somevalueforTopz) == null)
                                //{
                                //    someswtcTop = 1;
                                //}
                                someswtcTop = 1;
                            }
                        }
                        else
                        {
                            //if (yi < height -1)
                            //{
                    //
                            //}
                            someswtcTop = 1;
                        }

                        if (someswtcTop == 1)
                        {
                            buildTopFace(xi, yi, zi, 1.0f);

                            //buildTopLeft();
                            //if (IsTransparent(xi - 1, yi, zi))
                            //{
                            //buildTopLeft(xi, yi, zi, 1.0f);
                            //}
                        }
                    }



                    //BOTTOM FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi - 1, zi))
                    {
                        int someswtcright = 0;

                        Vector3 somechunkpos = chunkPos;
                        somechunkpos.Y -= (1.0f * width) * planeSize;

                        if (componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z) != null)
                        {
                            buildBottomFace(xi, yi, zi, 1.0f);
                            someswtcright = 1;
                        }
                        else
                        {
                            buildBottomFace(xi, yi, zi, 1.0f);
                            //someswtcright = 0;
                        }

                        if (someswtcright == 1)
                        {
                            buildBottomFace(xi, yi, zi, 1.0f);
                            //buildTopLeft();
                            //if (IsTransparent(xi - 1, yi, zi))
                            //{
                            //buildTopLeft(xi, yi, zi, 1.0f);
                            //}
                        }
                    }*/




                    //LEFT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    /*if (IsTransparent(xi - 1, yi, zi))
                    {
                        int someswtcLeft = 0;

                        Vector3 somechunkpos = chunkPos;
                        somechunkpos.X -= (width * planeSize);
                        //somechunkpos.Y = 0;

                        if (componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z) != null)
                        {
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

                            if (someChunk != null)
                            {
                                if (xi == 0 && someChunk.map != null)
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
                                else if (xi != 0)
                                {
                                    someswtcLeft = 1;
                                }
                                else
                                {
                                    //if (componentparent.getChunklod0(somevalueforTopx, somevalueforTopy, somevalueforTopz) == null)
                                    //{
                                    //    someswtcTop = 1;
                                    //}
                                    someswtcLeft = 1;
                                }
                            }

                        }
                        else
                        {
                            someswtcLeft = 0;
                        }

                        if (someswtcLeft == 1)
                        {
                            //buildTopFace();

                            //if (IsTransparent(xi, yi + 1, zi))
                            //{
                            buildTopLeft(xi, yi, zi, 1.0f);
                            //}
                        }
                        //buildTopFace();
                    }*/
                    /*
                    int someswtcLeft = 0;
                    //LEFT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    if (IsTransparent(xi - 1, yi, zi))
                    {
                        if (levelgen.getChunklod0(somevalueforLeftx, somevalueforLefty, somevalueforLeftz) != null)
                        {
                            //Vector3 somechunkpos = chunkPos;
                            //somechunkpos.X += (1.0f * width) * planeSize;


                            sclevelgenvert someChunk = (sclevelgenvert)levelgen.getChunklod0(somevalueforLeftx, somevalueforLefty, somevalueforLeftz);
                            if (someChunk != null)
                            {
                                if (xi == 0 && someChunk.map != null)
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
                                else if (xi != 0)
                                {
                                    someswtcLeft = 1;
                                }
                                else
                                {
                                    //if (componentparent.getChunklod0(somevalueforLeftx, somevalueforLefty, somevalueforLeftz) == null)
                                    //{
                                    //    someswtcLeft = 1;
                                    //}


                                    someswtcLeft = 1;
                                    /*if (someChunk.map == null)
                                    {
                                        someswtcLeft = 0;
                                    }
                                    else
                                    {
                                        someswtcLeft = 1;
                                    }
                                }
                            }
                            else
                            {
                                //someswtcLeft = 0;
                            }
                        }
                        else
                        {
                            someswtcLeft = 0;
                        }

                        if (someswtcLeft == 1)
                        {
                            //buildTopLeft();


                            /*if (IsTransparent(xi - 1, yi, zi))
                            {

                            }
                            buildTopLeft(xi, yi, zi, 1.0f);

                        }
                        //buildTopLeft();
                    }





                    //BACK FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi, zi - 1))
                    {

                        int someswtcBack = 0;

                        if (componentparent.getChunklod0(somevalueforBackx, somevalueforBacky, somevalueforBackz) != null)
                        {
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somevalueforLeftx, somevalueforLefty, somevalueforLeftz);
                            if (someChunk != null)
                            {
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
                                    //if (componentparent.getChunklod0(somevalueforBackx, somevalueforBacky, somevalueforBackz) == null)
                                    //{
                                    //    someswtcBack = 1;
                                    //}
                                    someswtcBack = 1;
                                }
                            }
                        }
                        else
                        {
                            someswtcBack = 1;
                        }

                        if (someswtcBack == 1)
                        {
                            //buildBackFace();



                            //BACK FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                            //if (IsTransparent(xi, yi, zi - 1))
                            //{
                                buildBackFace(xi, yi, zi, 1.0f);
                            //}


                        }
                        //buildBackFace();
                    }





                    //TOP FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi + 1, zi))
                    {

                        int someswtcTop = 0;


                        if (componentparent.getChunklod0(somevalueforTopx, somevalueforTopy, somevalueforTopz) != null)
                        {

                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somevalueforLeftx, somevalueforLefty, somevalueforLeftz);

                            if (someChunk != null)
                            {
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
                                    //if (componentparent.getChunklod0(somevalueforTopx, somevalueforTopy, somevalueforTopz) == null)
                                    //{
                                    //    someswtcTop = 1;
                                    //}
                                    someswtcTop = 1;
                                }
                            }

                        }
                        else
                        {
                            someswtcTop = 1;
                        }

                        if (someswtcTop == 1)
                        {
                            //buildTopFace();

                            //if (IsTransparent(xi, yi + 1, zi))
                            //{
                                buildTopFace(xi, yi, zi, 1.0f);
                            //}
                        }
                        //buildTopFace();
                    }




                    //RIGHT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    if (IsTransparent(xi + 1, yi, zi))
                    {

                        int someswtcRight = 0;

                        if (componentparent.getChunklod0(somevalueforRightx, somevalueforRighty, somevalueforRightz) != null)
                        {

                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somevalueforLeftx, somevalueforLefty, somevalueforLeftz);
                            if (someChunk != null)
                            {

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
                                    //if (componentparent.getChunklod0(somevalueforRightx, somevalueforRighty, somevalueforRightz) == null)
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
                        }
                        else
                        {
                            someswtcRight = 1;
                        }

                        if (someswtcRight == 1)
                        {
                            //buildTopRight();


                            //if (IsTransparent(xi + 1, yi, zi))
                            //{
                                buildTopRight(xi, yi, zi, 1.0f);
                            //}


                        }
                        //buildTopRight();
                    }

                    //FRONT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi, zi + 1))
                    {
                        int someswtcFront = 0;

                        if (componentparent.getChunklod0(somevalueforFrontx, somevalueforFronty, somevalueforFrontz) != null)
                        {

                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somevalueforFrontx, somevalueforFronty, somevalueforFrontz);

                            if (someChunk!= null)
                            {
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
                                    //if (componentparent.getChunklod0(somevalueforFrontx, somevalueforFronty, somevalueforFrontz) == null)
                                    //{
                                    //    someswtcFront = 1;
                                    //}
                                    someswtcFront = 1;
                                }
                            }


                        }
                        else
                        {
                            someswtcFront = 1;
                        }

                        if (someswtcFront == 1)
                        {
                            //buildFrontFace();

                            //if (IsTransparent(xi, yi, zi + 1))
                            //{
                                buildFrontFace(xi, yi, zi, 1.0f);
                            //}

                        }
                        //buildFrontFace();
                    }
                    */



                    /*
                    if (IsTransparent(xi, yi + 1, zi))
                    {
                        buildTopFace(xi, yi, zi, 1.0f);
                    }

                    //WORKING
                    //LEFT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi - 1, zi))
                    {
                        buildBottomFace(xi, yi, zi, 1.0f);
                    }
                    if (IsTransparent(xi - 1, yi, zi))
                    {

                        buildTopLeft(xi, yi, zi, 1.0f);
                    }

                    if (IsTransparent(xi + 1, yi, zi))
                    {
                        buildTopRight(xi, yi, zi, 1.0f);
                    }

                    if (IsTransparent(xi, yi, zi + 1))
                    {
                        buildFrontFace(xi, yi, zi, 1.0f);
                    }

                    //BACK FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi, zi - 1))
                    {
                        buildBackFace(xi, yi, zi, 1.0f);
                    }*/




                    /*
                    if (fullface == 0)
                    {
                        //BACK FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                        if (IsTransparent(xi, yi, zi - 1))
                        {
                            buildBackFace(xi, yi, zi, 1.0f);
                        }
                    }
                    else if (fullface == 1)
                    {

                        if (IsTransparent(xi, yi + 1, zi))
                        {
                            buildTopFace(xi, yi, zi, 1.0f);
                        }

                        //WORKING
                        //LEFT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                        if (IsTransparent(xi, yi - 1, zi))
                        {
                            buildBottomFace(xi, yi, zi, 1.0f);
                        }
                        if (IsTransparent(xi - 1, yi, zi))
                        {

                            buildTopLeft(xi, yi, zi, 1.0f);
                        }

                        if (IsTransparent(xi + 1, yi, zi))
                        {
                            buildTopRight(xi, yi, zi, 1.0f);
                        }

                        if (IsTransparent(xi, yi, zi + 1))
                        {
                            buildFrontFace(xi, yi, zi, 1.0f);
                        }

                        //BACK FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                        if (IsTransparent(xi, yi, zi - 1))
                        {
                            buildBackFace(xi, yi, zi, 1.0f);
                        }

                    }*/

                    zzints++;
                    if (zzints >= (depth))
                    {
                        yyints++;
                        zzints = 0;
                        swtchyints = 1;
                    }
                    if (yyints >= (height) && swtchyints == 1)
                    {
                        xxints++;
                        yyints = 0;
                        swtchyints = 0;
                        swtchxints = 1;
                    }
                    if (xxints >= (width) && swtchxints == 1)
                    {
                        swtchyints = 0;
                        swtchxints = 0;
                        swtchzints = 1;
                    }

                    tints++;
                    counterCreateChunkObjectFacesints++;
                }
            }
        }

        //UnityEngine.Debug.Log("_xx: " + _xx + " _zz: " + _zz + " _maxWidth: " + _maxWidth + " _maxDepth: " + _maxDepth + " rowIterateX: " + rowIterateX + " rowIterateZ: " + rowIterateZ);
        void buildTopFace(int xi, int yi, int zi, float block) //int _x, int _y, int _z, Vector3 chunkPos
        {
            _maxWidth = width;
            _maxDepth = depth;
            _maxHeight = height;
            foundVertOne = false;
            foundVertTwo = false;
            foundVertThree = false;
            foundVertFour = false;
            //TOPFACE

            _block = _tempChunkArray[xi + width * (yi + height * zi)];

            if (_block == 1) //|| _block == 2
            {

                if (IsTransparent(xi, yi + 1, zi))
                {
                    for (int _xx = 0; _xx < _maxWidth; _xx++)
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
                                                _maxWidth = _xx;
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
                                                        _maxWidth = _xx;
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
                                            _maxWidth = _xx;
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
                                                _maxWidth = _xx;
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
                                                        _maxWidth = _xx;
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
                                                _maxWidth = _xx;
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
                                                        _maxWidth = _xx;
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
                                            _maxWidth = _xx;
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
                                                _maxWidth = _xx;
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
                                                        _maxWidth = _xx;

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






                    if (getChunklod0Vertexint0(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize * levelofdetail * levelofdetailmul, oneVertIndexY * planeSize * levelofdetail * levelofdetailmul, oneVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = topfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        //_index0 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(oneVertIndexX * planeSize * levelofdetail * levelofdetailmul, oneVertIndexY * planeSize * levelofdetail * levelofdetailmul, oneVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);





                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint0(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize * levelofdetail * levelofdetailmul, twoVertIndexY * planeSize * levelofdetail * levelofdetailmul, twoVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = topfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });
                        //_index1 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(twoVertIndexX * planeSize * levelofdetail * levelofdetailmul, twoVertIndexY * planeSize * levelofdetail * levelofdetailmul, twoVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint0(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize * levelofdetail * levelofdetailmul, threeVertIndexY * planeSize * levelofdetail * levelofdetailmul, threeVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = topfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });
                        //_index2 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(threeVertIndexX * planeSize * levelofdetail * levelofdetailmul, threeVertIndexY * planeSize * levelofdetail * levelofdetailmul, threeVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint0(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize * levelofdetail * levelofdetailmul, fourVertIndexY * planeSize * levelofdetail * levelofdetailmul, fourVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = topfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });
                        //_index3 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(fourVertIndexX * planeSize * levelofdetail * levelofdetailmul, fourVertIndexY * planeSize * levelofdetail * levelofdetailmul, fourVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint0(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunklod0Vertexint0(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunklod0Vertexint0(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunklod0Vertexint0(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)//
                    {
                        _index0 = _testVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                        /*listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);
                        */


                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);

                        /*
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);*/

                        /*
                        if (voxeltype == 0 || voxeltype == 2)
                        {
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
                        }
                        else if (voxeltype == 3)
                        {
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index1);
                        }
                        else if (voxeltype == 1)
                        {
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
                            listOfTriangleIndices.Add(_index3);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index0);
                        }*/
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


        private void createleftFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {
            //offset1 = back * planeSize;
            //offset2 = down * planeSize;

            //positions[0 + vertzIndex] = start; //(x,y+1,z+1)
            //positions[1 + vertzIndex] = start + offset1;//(x,y+1,z)
            //positions[2 + vertzIndex] = start + offset2; //(x,y,z+1)
            //positions[3 + vertzIndex] = start + offset1 + offset2;//(x,y,z)



            int index0 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start,
                color = leftfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index1 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset1);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1,
                color = leftfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index2 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset2,
                color = leftfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index3 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start + offset1 + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1 + offset2,
                color = leftfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            listOfTriangleIndices.Add(index0);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index3);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index1);
        }



        private void createRightFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {


            int index0 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start,
                color = rightfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index1 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset1);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1,
                color = rightfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index2 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset2,
                color = rightfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index3 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start + offset1 + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1 + offset2,
                color = rightfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            listOfTriangleIndices.Add(index0);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index3);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index1);
        }


        private void createTopFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {


            int index0 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start,
                color = topfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index1 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset1);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1,
                color = topfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index2 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset2,
                color = topfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index3 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start + offset1 + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1 + offset2,
                color = topfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;


            listOfTriangleIndices.Add(index0);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index3);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index1);
        }

        private void createBottomFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {


            int index0 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start,
                color = bottomfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index1 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset1);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1,
                color = bottomfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index2 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset2,
                color = bottomfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index3 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start + offset1 + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1 + offset2,
                color = bottomfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;


            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index0);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index3);
        }

        private void createFrontFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {


            int index0 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start,
                color = frontfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index1 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset1);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1,
                color = frontfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index2 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset2,
                color = frontfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index3 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start + offset1 + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1 + offset2,
                color = frontfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;


            listOfTriangleIndices.Add(index0);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index3);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index1);
        }



        private void createBackFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {


            int index0 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start,
                color = backfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index1 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset1);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1,
                color = backfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index2 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset2,
                color = backfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index3 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start + offset1 + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1 + offset2,
                color = backfacecolor,
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),

            });
            _newVertzCounter++;


            listOfTriangleIndices.Add(index0);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index3);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index1);
        }


        void buildTopLeft(int xi, int yi, int zi, float block) //int _x, int _y, int _z, Vector3 chunkPos
        {
            //Vector3 somechunkpos = chunkPos;
            //somechunkpos.X -= (1.0f * width) * planeSize;
            //sccstriglevelchunk someChunk = (sccstriglevelchunk)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);


            //Console.WriteLine(planeSize);

            _maxWidth = width;
            _maxDepth = depth;
            _maxHeight = height;
            foundVertOne = false;
            foundVertTwo = false;
            foundVertThree = false;
            foundVertFour = false;
            //TOPFACE

            _block = _tempChunkArrayLeftFace[xi + width * (yi + height * zi)];

            if (_block == 1) //|| _block == 2
            {
                //if (someChunk != null)
                {
                    //if (someChunk.map != null)
                    {
                        //if (someChunk.IsTransparent(width - 1, yi, zi))
                        {

                            if (IsTransparent(xi - 1, yi, zi))// && someChunk.IsTransparent(width - 1, yi, zi))
                            {
                                for (int _yy = 0; _yy < _maxHeight; _yy++)
                                {
                                    rowIterateY = yi + _yy;

                                    for (int _zz = 0; _zz < _maxDepth; _zz++)//int _zz = _maxDepth-1; _zz >= 0; _zz--) //int _zz = 0; _zz < _maxDepth; _zz++
                                    {
                                        rowIterateZ = zi + _zz;

                                        //if (someChunk != null)
                                        {
                                            //if (someChunk.map != null)
                                            {
                                                //if (someChunk.IsTransparent(width - 1, rowIterateY, rowIterateZ))
                                                {



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
                                                                    _maxHeight = _yy;
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
                                                                            _maxHeight = _yy;
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
                                                                _maxHeight = _yy;
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
                                                                    _maxHeight = _yy;
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
                                                                            _maxHeight = _yy;
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
                                                                    _maxHeight = _yy;
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
                                                                            _maxHeight = _yy;
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
                                                                _maxHeight = _yy;
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
                                                                    _maxHeight = _yy;
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
                                                                            _maxHeight = _yy;

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
                                        }

                                    }
                                }






                                if (getChunklod0Vertexint1(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                                {
                                    vertexlist.Add(new tutorialcubeaschunk.DVertex()
                                    {
                                        position = new Vector4(oneVertIndexX * planeSize * levelofdetail * levelofdetailmul, oneVertIndexY * planeSize * levelofdetail * levelofdetailmul, oneVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                                        //indexPos = new Vector4(xi, yi, zi, _block),
                                        color = leftfacecolor,
                                        //normal = new Vector3(-1, 0, 0),
                                        //padding0 = padding0,
                                        //tex = new Vector2(1, 1),
                                        //padding1 = padding1,
                                        //padding2 = padding2,
                                    });

                                    //_index0 = vertexlist.Count;
                                    //vertexlist.Add(new Vector4(oneVertIndexX * planeSize * levelofdetail * levelofdetailmul, oneVertIndexY * planeSize * levelofdetail * levelofdetailmul, oneVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                                    //vertexlist.Add(new Vector4(xit, yit, zit, block));
                                    //vertexlist.Add(leftfacecolor);





                                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    _chunkVertexArray1[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                                    _testVertexArray1[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                                    _newVertzCounter++;
                                }

                                if (getChunklod0Vertexint1(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                                {
                                    vertexlist.Add(new tutorialcubeaschunk.DVertex()
                                    {
                                        position = new Vector4(twoVertIndexX * planeSize * levelofdetail * levelofdetailmul, twoVertIndexY * planeSize * levelofdetail * levelofdetailmul, twoVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                                        //indexPos = new Vector4(xi, yi, zi, _block),
                                        color = leftfacecolor,
                                        //normal = new Vector3(-1, 0, 0),
                                        //padding0 = padding0,
                                        //tex = new Vector2(1, 1),
                                        //padding1 = padding1,
                                        //padding2 = padding2,
                                    });
                                    //_index1 = vertexlist.Count;
                                    //vertexlist.Add(new Vector4(twoVertIndexX * planeSize * levelofdetail * levelofdetailmul, twoVertIndexY * planeSize * levelofdetail * levelofdetailmul, twoVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                                    //vertexlist.Add(new Vector4(xit, yit, zit, block));
                                    //vertexlist.Add(leftfacecolor);

                                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    _chunkVertexArray1[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                                    _testVertexArray1[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                                    _newVertzCounter++;
                                }

                                if (getChunklod0Vertexint1(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                                {
                                    vertexlist.Add(new tutorialcubeaschunk.DVertex()
                                    {
                                        position = new Vector4(threeVertIndexX * planeSize * levelofdetail * levelofdetailmul, threeVertIndexY * planeSize * levelofdetail * levelofdetailmul, threeVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                                        //indexPos = new Vector4(xi, yi, zi, _block),
                                        color = leftfacecolor,
                                        //normal = new Vector3(-1, 0, 0),
                                        //padding0 = padding0,
                                        //tex = new Vector2(1, 1),
                                        //padding1 = padding1,
                                        //padding2 = padding2,
                                    });
                                    //_index2 = vertexlist.Count;
                                    //vertexlist.Add(new Vector4(threeVertIndexX * planeSize * levelofdetail * levelofdetailmul, threeVertIndexY * planeSize * levelofdetail * levelofdetailmul, threeVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                                    //vertexlist.Add(new Vector4(xit, yit, zit, block));
                                    //vertexlist.Add(leftfacecolor);

                                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                                    _chunkVertexArray1[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                                    _testVertexArray1[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                                    _newVertzCounter++;
                                }

                                if (getChunklod0Vertexint1(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                                {
                                    vertexlist.Add(new tutorialcubeaschunk.DVertex()
                                    {
                                        position = new Vector4(fourVertIndexX * planeSize * levelofdetail * levelofdetailmul, fourVertIndexY * planeSize * levelofdetail * levelofdetailmul, fourVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                                        //indexPos = new Vector4(xi, yi, zi, _block),
                                        color = leftfacecolor,
                                        //normal = new Vector3(-1, 0, 0),
                                        //padding0 = padding0,
                                        //tex = new Vector2(1, 1),
                                        //padding1 = padding1,
                                        //padding2 = padding2,
                                    });
                                    //_index3 = vertexlist.Count;
                                    //vertexlist.Add(new Vector4(fourVertIndexX * planeSize * levelofdetail * levelofdetailmul, fourVertIndexY * planeSize * levelofdetail * levelofdetailmul, fourVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                                    //vertexlist.Add(new Vector4(xit, yit, zit, block));
                                    //vertexlist.Add(leftfacecolor);

                                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    _chunkVertexArray1[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                                    _testVertexArray1[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                                    _newVertzCounter++;
                                }

                                if (getChunklod0Vertexint1(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunklod0Vertexint1(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunklod0Vertexint1(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunklod0Vertexint1(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)//
                                {
                                    _index0 = _testVertexArray1[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                                    _index1 = _testVertexArray1[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                                    _index2 = _testVertexArray1[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                                    _index3 = _testVertexArray1[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                                    /*listOfTriangleIndices.Add(_index2);
                                    listOfTriangleIndices.Add(_index1);
                                    listOfTriangleIndices.Add(_index0);
                                    listOfTriangleIndices.Add(_index1);
                                    listOfTriangleIndices.Add(_index2);
                                    listOfTriangleIndices.Add(_index3);
                                    */


                                    listOfTriangleIndices.Add(_index0);
                                    listOfTriangleIndices.Add(_index1);
                                    listOfTriangleIndices.Add(_index2);
                                    listOfTriangleIndices.Add(_index3);
                                    listOfTriangleIndices.Add(_index2);
                                    listOfTriangleIndices.Add(_index1);

                                    /*
                                    listOfTriangleIndices.Add(_index2);
                                    listOfTriangleIndices.Add(_index1);
                                    listOfTriangleIndices.Add(_index0);
                                    listOfTriangleIndices.Add(_index1);
                                    listOfTriangleIndices.Add(_index2);
                                    listOfTriangleIndices.Add(_index3);*/

                                    /*
                                    if (voxeltype == 0 || voxeltype == 2)
                                    {
                                        listOfTriangleIndices.Add(_index2);
                                        listOfTriangleIndices.Add(_index1);
                                        listOfTriangleIndices.Add(_index0);
                                        listOfTriangleIndices.Add(_index1);
                                        listOfTriangleIndices.Add(_index2);
                                        listOfTriangleIndices.Add(_index3);
                                    }
                                    else if (voxeltype == 3)
                                    {
                                        listOfTriangleIndices.Add(_index0);
                                        listOfTriangleIndices.Add(_index1);
                                        listOfTriangleIndices.Add(_index2);
                                        listOfTriangleIndices.Add(_index3);
                                        listOfTriangleIndices.Add(_index2);
                                        listOfTriangleIndices.Add(_index1);
                                    }
                                    else if (voxeltype == 1)
                                    {
                                        listOfTriangleIndices.Add(_index0);
                                        listOfTriangleIndices.Add(_index2);
                                        listOfTriangleIndices.Add(_index2);
                                        listOfTriangleIndices.Add(_index3);
                                        listOfTriangleIndices.Add(_index3);
                                        listOfTriangleIndices.Add(_index1);
                                        listOfTriangleIndices.Add(_index1);
                                        listOfTriangleIndices.Add(_index0);
                                    }*/
                                }
                            }
                        }
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

        void buildTopRight(int xi, int yi, int zi, float block) //int xi, int _y, int _z, Vector3 chunkPos
        {
            _maxWidth = width;
            _maxDepth = depth;
            _maxHeight = height;
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
                    for (int _yy = 0; _yy < _maxHeight; _yy++)
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
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize  + chunkPos, Quaternion.identity);
                                    foundVertOne = true;

                                    if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                    {
                                        _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = xi + 1;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = rowIterateZ;
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize  + chunkPos, Quaternion.identity);

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
                                                    _maxHeight = _yy;
                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize  + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = xi + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize  + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = xi + 1;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = xi + 1;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);
                                    }

                                    if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                    {
                                        _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = xi + 1;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                                    _maxHeight = _yy;
                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);
                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi + 1;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                                    _maxHeight = _yy;
                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = xi + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                                    _maxHeight = _yy;

                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi + 1;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(xi, rowIterateY, rowIterateZ))
                            {
                                _tempChunkArrayRightFace[(xi) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                                ////////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize  + chunkPos, Quaternion.identity);
                            }
                        }
                    }





                    if (getChunklod0Vertexint2(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize * levelofdetail * levelofdetailmul, oneVertIndexY * planeSize * levelofdetail * levelofdetailmul, oneVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = rightfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        //_index0 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(oneVertIndexX * planeSize * levelofdetail * levelofdetailmul, oneVertIndexY * planeSize * levelofdetail * levelofdetailmul, oneVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);





                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray2[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray2[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint2(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize * levelofdetail * levelofdetailmul, twoVertIndexY * planeSize * levelofdetail * levelofdetailmul, twoVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = rightfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });
                        //_index1 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(twoVertIndexX * planeSize * levelofdetail * levelofdetailmul, twoVertIndexY * planeSize * levelofdetail * levelofdetailmul, twoVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray2[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray2[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint2(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize * levelofdetail * levelofdetailmul, threeVertIndexY * planeSize * levelofdetail * levelofdetailmul, threeVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = rightfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });
                        //_index2 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(threeVertIndexX * planeSize * levelofdetail * levelofdetailmul, threeVertIndexY * planeSize * levelofdetail * levelofdetailmul, threeVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray2[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray2[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint2(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize * levelofdetail * levelofdetailmul, fourVertIndexY * planeSize * levelofdetail * levelofdetailmul, fourVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = rightfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });
                        //_index3 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(fourVertIndexX * planeSize * levelofdetail * levelofdetailmul, fourVertIndexY * planeSize * levelofdetail * levelofdetailmul, fourVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray2[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray2[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint2(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunklod0Vertexint2(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunklod0Vertexint2(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunklod0Vertexint2(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)//
                    {
                        _index0 = _testVertexArray2[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray2[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray2[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray2[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                        /*listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);
                        */


                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);

                        /*
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);*/

                        /*
                        if (voxeltype == 0 || voxeltype == 2)
                        {
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
                        }
                        else if (voxeltype == 3)
                        {
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index1);
                        }
                        else if (voxeltype == 1)
                        {
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
                            listOfTriangleIndices.Add(_index3);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index0);
                        }*/
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




        void buildFrontFace(int xi, int yi, int zi, float block) // int _x, int _y, int _z, Vector3 chunkPos
        {

            _maxWidth = width;
            _maxDepth = depth;
            _maxHeight = height;
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
                    for (int _yy = 0; _yy < _maxHeight; _yy++)
                    {
                        rowIterateY = yi + _yy;
                        for (int _xx = 0; _xx < _maxWidth; _xx++)
                        {
                            rowIterateX = xi + _xx;

                            if (rowIterateY < height && rowIterateX < width)
                            {
                                if (_yy == 0 && _xx == 0)
                                {
                                    oneVertIndexX = rowIterateX;
                                    oneVertIndexY = rowIterateY;
                                    oneVertIndexZ = zi + 1;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize  + _chunkPos, Quaternion.identity);

                                    foundVertOne = true;

                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = rowIterateX;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi + 1;
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize  + _chunkPos, Quaternion.identity);

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
                                                    _maxHeight = _yy;
                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize  + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                                        _maxWidth = _xx + 1;
                                                        foundVertTwo = true;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi + 1;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                            }
                                            else if (_block == 2)
                                            {
                                                twoVertIndexX = rowIterateX + 1;
                                                twoVertIndexY = rowIterateY;
                                                twoVertIndexZ = zi + 1;
                                                _maxWidth = _xx + 1;
                                                foundVertTwo = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi + 1;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                                        _maxWidth = _xx + 1;
                                                        foundVertTwo = true;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi + 1;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                                _maxWidth = _xx + 1;
                                                foundVertTwo = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi + 1;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);
                                    }

                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = rowIterateX - _xx;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi + 1;
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                                    _maxHeight = _yy;
                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi + 1;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                                    _maxHeight = _yy;
                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                                    _maxHeight = _yy;

                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi + 1;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(rowIterateX, rowIterateY, zi))
                            {
                                _tempChunkArrayFrontFace[(rowIterateX) + width * (rowIterateY + height * (zi))] = 2;
                                //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize  + _chunkPos, Quaternion.identity);
                            }
                        }
                    }



                    if (getChunklod0Vertexint3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize * levelofdetail * levelofdetailmul, oneVertIndexY * planeSize * levelofdetail * levelofdetailmul, oneVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = frontfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        //_index0 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(oneVertIndexX * planeSize * levelofdetail * levelofdetailmul, oneVertIndexY * planeSize * levelofdetail * levelofdetailmul, oneVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);





                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray3[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray3[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize * levelofdetail * levelofdetailmul, twoVertIndexY * planeSize * levelofdetail * levelofdetailmul, twoVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = frontfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });
                        //_index1 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(twoVertIndexX * planeSize * levelofdetail * levelofdetailmul, twoVertIndexY * planeSize * levelofdetail * levelofdetailmul, twoVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray3[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray3[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint3(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize * levelofdetail * levelofdetailmul, threeVertIndexY * planeSize * levelofdetail * levelofdetailmul, threeVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = frontfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });
                        //_index2 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(threeVertIndexX * planeSize * levelofdetail * levelofdetailmul, threeVertIndexY * planeSize * levelofdetail * levelofdetailmul, threeVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray3[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray3[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize * levelofdetail * levelofdetailmul, fourVertIndexY * planeSize * levelofdetail * levelofdetailmul, fourVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = frontfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });
                        //_index3 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(fourVertIndexX * planeSize * levelofdetail * levelofdetailmul, fourVertIndexY * planeSize * levelofdetail * levelofdetailmul, fourVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray3[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray3[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunklod0Vertexint3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunklod0Vertexint3(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunklod0Vertexint3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)//
                    {
                        _index0 = _testVertexArray3[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray3[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray3[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray3[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                        /*listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);
                        */


                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);

                        /*
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);*/

                        /*
                        if (voxeltype == 0 || voxeltype == 2)
                        {
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
                        }
                        else if (voxeltype == 3)
                        {
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index1);
                        }
                        else if (voxeltype == 1)
                        {
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
                            listOfTriangleIndices.Add(_index3);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index0);
                        }*/
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


        void buildBackFace(int xi, int yi, int zi, float block) //int _x, int _y, int zi, Vector3 chunkPos
        {
            _maxWidth = width;
            _maxDepth = depth;
            _maxHeight = height;
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





                    for (int _yy = 0; _yy < _maxHeight; _yy++)
                    {
                        rowIterateY = yi + _yy;
                        for (int _xx = 0; _xx < _maxWidth; _xx++)
                        {
                            rowIterateX = xi + _xx;

                            if (rowIterateY < height && rowIterateX < width)
                            {
                                if (_yy == 0 && _xx == 0)
                                {
                                    oneVertIndexX = rowIterateX;
                                    oneVertIndexY = rowIterateY;
                                    oneVertIndexZ = zi;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize  + _chunkPos, Quaternion.identity);
                                    foundVertOne = true;

                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = rowIterateX;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi;
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize  + _chunkPos, Quaternion.identity);

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
                                                    _maxHeight = _yy;
                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize  + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                                        _maxWidth = _xx + 1;
                                                        foundVertTwo = true;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                            }
                                            else if (_block == 2)
                                            {
                                                twoVertIndexX = rowIterateX + 1;
                                                twoVertIndexY = rowIterateY;
                                                twoVertIndexZ = zi;
                                                _maxWidth = _xx + 1;
                                                foundVertTwo = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                                        _maxWidth = _xx + 1;
                                                        foundVertTwo = true;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                                _maxWidth = _xx + 1;
                                                foundVertTwo = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);
                                    }

                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = rowIterateX - _xx;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi;
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                                    _maxHeight = _yy;
                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                                    _maxHeight = _yy;
                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                                    _maxHeight = _yy;

                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(rowIterateX, rowIterateY, zi))
                            {
                                _tempChunkArrayBackFace[(rowIterateX) + width * (rowIterateY + height * (zi))] = 2;
                                //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize  + _chunkPos, Quaternion.identity);
                            }
                        }
                    }




                    if (getChunklod0Vertexint4(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize * levelofdetail * levelofdetailmul, oneVertIndexY * planeSize * levelofdetail * levelofdetailmul, oneVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = backfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        //_index0 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(oneVertIndexX * planeSize * levelofdetail * levelofdetailmul, oneVertIndexY * planeSize * levelofdetail * levelofdetailmul, oneVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);





                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray4[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray4[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint4(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize * levelofdetail * levelofdetailmul, twoVertIndexY * planeSize * levelofdetail * levelofdetailmul, twoVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = backfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });
                        //_index1 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(twoVertIndexX * planeSize * levelofdetail * levelofdetailmul, twoVertIndexY * planeSize * levelofdetail * levelofdetailmul, twoVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray4[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray4[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint4(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize * levelofdetail * levelofdetailmul, threeVertIndexY * planeSize * levelofdetail * levelofdetailmul, threeVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = backfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });
                        //_index2 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(threeVertIndexX * planeSize * levelofdetail * levelofdetailmul, threeVertIndexY * planeSize * levelofdetail * levelofdetailmul, threeVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray4[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray4[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint4(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize * levelofdetail * levelofdetailmul, fourVertIndexY * planeSize * levelofdetail * levelofdetailmul, fourVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = backfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });
                        //_index3 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(fourVertIndexX * planeSize * levelofdetail * levelofdetailmul, fourVertIndexY * planeSize * levelofdetail * levelofdetailmul, fourVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray4[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray4[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint4(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunklod0Vertexint4(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunklod0Vertexint4(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunklod0Vertexint4(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)//
                    {
                        _index0 = _testVertexArray4[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray4[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray4[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray4[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                        /*listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);
                        */


                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);

                        /*
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);*/

                        /*
                        if (voxeltype == 0 || voxeltype == 2)
                        {
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
                        }
                        else if (voxeltype == 3)
                        {
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index1);
                        }
                        else if (voxeltype == 1)
                        {
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
                            listOfTriangleIndices.Add(_index3);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index0);
                        }*/
                    }


                }
            }
        }




        void buildBottomFace(int xi, int yi, int zi, float block) //int _x, int _y, int _z, Vector3 chunkPos
        {
            _maxWidth = width;
            _maxDepth = depth;
            _maxHeight = height;
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
                    for (int _xx = 0; _xx < _maxWidth; _xx++)
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
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ) * planeSize  + chunkPos, Quaternion.identity);
                                    foundVertOne = true;

                                    if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                    {
                                        _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi;
                                            threeVertIndexZ = rowIterateZ;
                                            _maxWidth = _xx;
                                            foundVertThree = true;
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize  + chunkPos, Quaternion.identity);

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
                                                    _maxWidth = _xx;
                                                    foundVertThree = true;
                                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize  + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = yi;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize  + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                    {
                                        _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxWidth = _xx;
                                            foundVertThree = true;
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                                    _maxWidth = _xx;
                                                    foundVertThree = true;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                            _maxWidth = _xx;
                                            foundVertThree = true;
                                            ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                                    _maxWidth = _xx;
                                                    foundVertThree = true;
                                                    ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = yi;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                            _maxWidth = _xx;
                                            foundVertThree = true;
                                            ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, yi + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                                    _maxWidth = _xx;

                                                    foundVertThree = true;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
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
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(rowIterateX, yi, rowIterateZ))
                            {
                                _tempChunkArrayBottomFace[(rowIterateX) + width * (yi + height * (rowIterateZ))] = 2;
                                //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize  + chunkPos, Quaternion.identity);
                            }
                        }
                    }





                    if (getChunklod0Vertexint5(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize * levelofdetail * levelofdetailmul, oneVertIndexY * planeSize * levelofdetail * levelofdetailmul, oneVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = bottomfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        //_index0 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(oneVertIndexX * planeSize * levelofdetail * levelofdetailmul, oneVertIndexY * planeSize * levelofdetail * levelofdetailmul, oneVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);





                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray5[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray5[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint5(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize * levelofdetail * levelofdetailmul, twoVertIndexY * planeSize * levelofdetail * levelofdetailmul, twoVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = bottomfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });
                        //_index1 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(twoVertIndexX * planeSize * levelofdetail * levelofdetailmul, twoVertIndexY * planeSize * levelofdetail * levelofdetailmul, twoVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray5[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray5[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint5(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize * levelofdetail * levelofdetailmul, threeVertIndexY * planeSize * levelofdetail * levelofdetailmul, threeVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = bottomfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });
                        //_index2 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(threeVertIndexX * planeSize * levelofdetail * levelofdetailmul, threeVertIndexY * planeSize * levelofdetail * levelofdetailmul, threeVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray5[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray5[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint5(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new tutorialcubeaschunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize * levelofdetail * levelofdetailmul, fourVertIndexY * planeSize * levelofdetail * levelofdetailmul, fourVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = bottomfacecolor,
                            //normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            //tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });
                        //_index3 = vertexlist.Count;
                        //vertexlist.Add(new Vector4(fourVertIndexX * planeSize * levelofdetail * levelofdetailmul, fourVertIndexY * planeSize * levelofdetail * levelofdetailmul, fourVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos);
                        //vertexlist.Add(new Vector4(xit, yit, zit, block));
                        //vertexlist.Add(leftfacecolor);

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray5[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray5[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunklod0Vertexint5(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunklod0Vertexint5(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunklod0Vertexint5(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunklod0Vertexint5(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)//
                    {
                        _index0 = _testVertexArray5[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray5[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray5[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray5[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                        /*listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);
                        */


                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);

                        /*
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);*/

                        /*
                        if (voxeltype == 0 || voxeltype == 2)
                        {
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
                        }
                        else if (voxeltype == 3)
                        {
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index1);
                        }
                        else if (voxeltype == 1)
                        {
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
                            listOfTriangleIndices.Add(_index3);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index0);
                        }*/
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














        public void setAdjacentChunks(Vector3 pos, int indexx, int indexy, int indexz)
        {
            /*if (indexx == 0)
            {
                if (listofchunksadjacent)
                {

                }
            }*/
        }






        /*
        public void setAdjacentChunks(Vector3 pos, int indexx, int indexy, int indexz)
        {
            //int width = currentChunk.sclevelgenclass.width;
            //int height = currentChunk.sclevelgenclass.height;
            //int depth = currentChunk.sclevelgenclass.depth;

            //////Debug.Log("x: " + (indexx) + " y: " + (indexy) + " z: " + (indexz));

            int useonlyunitOneForNeighboorIndexPlease = 1;

            if (indexx == 0)
            {
                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z);

                    if (adjacentChunk.map != null)
                    {


                        if (adjacentChunk.Getint((int)width - 1, (int)indexy, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)indexy, (int)indexz, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)indexy, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk right exists");
                            adjacentChunk.Setint((int)0, (int)indexy, (int)indexz, activeBlockType, pos);
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
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)indexx, (int)height - 1, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)indexx, (int)height - 1, (int)indexz, activeBlockType, pos);
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
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)indexx, (int)0, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)indexx, (int)0, (int)indexz, activeBlockType, pos);
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
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)indexx, (int)indexy, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)indexx, (int)indexy, (int)depth - 1, activeBlockType, pos);
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
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)indexx, (int)indexy, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)indexx, (int)indexy, (int)0, activeBlockType, pos);
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
                /*if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z);

                    if (adjacentChunk.Getint((int)width - 1, (int)indexy, (int)indexz) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.Setint((int)width - 1, (int)indexy, (int)indexz, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }

                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)indexz, activeBlockType, pos);

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

                /*if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);

                    if (adjacentChunk.Getint((int)indexx, (int)height - 1, (int)indexz) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.Setint((int)indexx, (int)height - 1, (int)indexz, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }
            }
            if (indexx == 0 && indexy == 0 && indexz == 0)
            {
                /*if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X , (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);

                    if (adjacentChunk.Getint((int)width-1, (int)height - 1, (int)depth-1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }


                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);

                    if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }

                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

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

                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

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
                /*if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);

                    if (adjacentChunk.Getint((int)indexx, (int)height - 1, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.Setint((int)indexx, (int)height - 1, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }

                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);

                    if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);
                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }

                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);
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

                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);
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
                /*if (componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z- useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);

                    if (adjacentChunk.Getint((int)width - 1, (int)indexz, (int)depth-1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.Setint((int)width - 1, (int)indexz, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }

                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)indexy, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)indexy, (int)depth - 1, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

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

                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

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

                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

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

                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)indexx, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)indexx, (int)height - 1, (int)depth - 1, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)depth - 1, activeBlockType, pos);

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

                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)depth - 1, activeBlockType, pos);

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



                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)depth - 1, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)indexz, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)0, activeBlockType, pos);

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

                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)0, activeBlockType, pos);

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


                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)0, activeBlockType, pos);

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

                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)indexy, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)indexy, (int)depth - 1, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)0, (int)0, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)0, (int)0, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)0, (int)0, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)indexx, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)indexx, (int)0, (int)depth - 1, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)0, (int)depth - 1, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)0, (int)depth - 1, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)0, (int)depth - 1, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)0, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)0, (int)indexz, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)0, (int)0, activeBlockType, pos);

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

                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)0, (int)0, activeBlockType, pos);

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

                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)0, (int)0, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)indexy, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)indexy, (int)0, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)0, activeBlockType, pos);

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

                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)0, activeBlockType, pos);

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

                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)0, activeBlockType, pos);

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
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)indexx, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)indexx, (int)0, (int)0, activeBlockType, pos);

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

        public int GetByte(int x, int y, int z)
        {
            if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= height) || (z >= depth))
            {
                //Console.WriteLine("out of range");
                return 0;
            }
            //Console.WriteLine("index:" + (x + width * (y + height * z)) + "/mapl:" + map.Length + "/x:" + x + "/y:" + y + "/z:" + z + "/w:" + width + "/h:" + height + "/d:" + depth);
            return map[x + width * (y + height * z)];
        }


        public bool IsTransparent(int _x, int _y, int _z)
        {
            if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= width) || (_y >= height) || (_z >= depth)) return true;
            return map[_x + width * (_y + height * _z)] == 0; //_chunkArray
        }

        int getChunklod0int(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < width && _y < height && _z < depth)
            {
                return map[_x + width * (_y + height * _z)]; //_chunkArray
            }
            return 0;
        }


        int getTempArrayint(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < width && _y < height && _z < depth)
            {
                return _tempChunkArray[_x + width * (_y + height * _z)];
            }
            return 0;
        }



        int getChunklod0Vertexint0(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
            {
                return _chunkVertexArray0[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
            }
            return 0;
        }


        int getChunklod0Vertexint1(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
            {
                return _chunkVertexArray1[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
            }
            return 0;
        }




        int getChunklod0Vertexint2(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
            {
                return _chunkVertexArray2[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
            }
            return 0;
        }




        int getChunklod0Vertexint3(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
            {
                return _chunkVertexArray3[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
            }
            return 0;
        }




        int getChunklod0Vertexint4(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
            {
                return _chunkVertexArray4[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
            }
            return 0;
        }




        int getChunklod0Vertexint5(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
            {
                return _chunkVertexArray5[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
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


        public void Setint(int x, int y, int z, int block, Vector3 chunkintpos_)
        {
            /*if (addfracturedcubeonimpact == 1)
            {
                //var unityTutorialObjectPool = this.GameObject.GetComponent<NewObjectPoolerScript>();
                var UnityTutorialPooledObject = UnityTutorialGameObjectPool.GetPooledObject();
                UnityTutorialPooledObject.transform.position = chunkintpos_;
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


        public int Getint(int x, int y, int z)
        {
            if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
            {
                return 0;
            }

            int indexOf = x + width * (y + depth * z);
            return map[indexOf];
            //return map[x + width * (y + depth * z)];
        }




















        /*
        public void Regenerate(Vector4 currentPosition)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {

                        block = map[x + width * (y + height * z)];

                        DrawBrick(x, y, z, currentPosition, block);
                    }
                }
            }
        }

        public void DrawBrick(int x, int y, int z, Vector4 currentPosition, int block)
        {
            Vector4 start = new Vector4(x * planeSize, y * planeSize, z * planeSize,1) + chunkoriginpos;
            Vector4 offset1, offset2;

            offset1 = forward * planeSize;
            offset2 = right * planeSize;
            createTopFace(start + up * planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = back * planeSize;
            offset2 = down * planeSize;
            createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = up * planeSize;
            offset2 = forward * planeSize;
            createRightFace(start + right * planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = left * planeSize;
            offset2 = up * planeSize;
            createFrontFace(start + right * planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = right * planeSize;
            offset2 = up * planeSize;
            createBackFace(start + forward * planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = right * planeSize;
            offset2 = forward * planeSize;
            createBottomFace(start, offset1, offset2, currentPosition, x, y, z, 1);
        }


        private void createTopFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(0, 1),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(1f, 1),
            });

            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
        }



        private void createBottomFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block)
        {
            int index = listOfVerts.Count;
            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(0f, 0),
            });

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(0f, 1f),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                normal = new Vector3(0, 1, -1),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                tex = new Vector2(1, 0),

            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(1, 1f),
            });

            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);

        }


        private void createFrontFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(0, 1f),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(1, 1f),
            });

            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);

        }
        private void createBackFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(0, 1),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(1, 1f),
            });

            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
        }

        private void createRightFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(0, 1),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(1, 1f),
            });

            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
        }

        private void createleftFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block)
        {
            int index = listOfVerts.Count;
            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 1),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(1, 1),
            });

            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);

        }
        public bool IsTransparent(int x, int y, int z)
        {
            if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= height) || (z >= depth)) return true;
            {
                return map[x + width * (y + height * z)] == 0;
            }
        }*/
    }
}

