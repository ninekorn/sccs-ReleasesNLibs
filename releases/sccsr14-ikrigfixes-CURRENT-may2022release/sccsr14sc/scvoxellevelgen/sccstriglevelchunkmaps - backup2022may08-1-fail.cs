using System;
using System.Collections.Generic;
using SharpDX;

using SimplexNoise;

using System.Collections;

namespace sccs
{
    public class sccstriglevelchunkmaps
    {
        //public int[] map;
        public int[] map;

        //private int _detailScale = 200;
        //private int _heightScale = 5;



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
        public Vector3 chunkPos;
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

        int tinyChunkWidth; int tinyChunkHeight; int tinyChunkDepth;


        sclevelgenchunk componentparent;


        //sclevelgenchunk_instances componentparentthischunk; sclevelgenchunkPrim componentparentprim; sclevelgenchunk componentparentinstance;
        int voxeltype;
        int fullface;

        private int width = 10;
        private int height = 10;
        private int depth = 10;

        public float floorHeight = 10;

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


        LevelGenerator4 levelgen;
        public static float xChunkPos;
        public static float yChunkPos;
        public static float zChunkPos;
        //, out Vector3[] norms, out Vector2[] tex
        //, out Vector4[] vertexArray

        public static Dictionary<sccstriglevelchunkmaps, Vector3> chunkz = new Dictionary<sccstriglevelchunkmaps, Vector3>();

        public int[] buildchunkmaps(Vector3 currentPosition, out int[] mapper, float planeSize_, LevelGenerator4 levelgen_, Vector3 _chunkPos, int width_, int height_, int depth_, int typeofterraintiles, sclevelgenchunk componentparent_) //, out int vertexNum, out int indicesNum
        //public void startBuildingArray(Vector4 currentPosition, out sclevelgenchunk.DVertex[] vertexArray, out int[] triangleArray, out int[] mapper, int padding0_, int padding1_, int padding2_, int numberOfObjectInWidth_, int numberOfObjectInHeight_, int numberOfObjectInDepth_, int numberOfInstancesPerObjectInWidth_, int numberOfInstancesPerObjectInHeight_, int numberOfInstancesPerObjectInDepth_, int tinyChunkWidth_, int tinyChunkHeight_, int tinyChunkDepth_, float planeSize_, sclevelgenchunkPrim componentparentprim_, sclevelgenchunk componentparentinstance_, sclevelgenchunk_instances componentparentthischunk_, int fullface_, int voxeltype_)
        {

            chunkz.Add(this, _chunkPos);

            levelgen = levelgen_;

            width = width_;
            height = height_;
            depth = depth_;


            xChunkPos = _chunkPos.X;
            yChunkPos = _chunkPos.Y;
            zChunkPos = _chunkPos.Z;

            tinyChunkWidth = width;
            tinyChunkHeight = height;
            tinyChunkDepth = depth;
            planeSize = planeSize_;

            voxeltype = 0;

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
            }


            chunkPos = _chunkPos;// new Vector3(currentPosition.X, currentPosition.Y, currentPosition.Z);


            planeSize = planeSize;
            realplanetwidth = 4;
            width = tinyChunkWidth;
            height = tinyChunkHeight;
            depth = tinyChunkDepth;










            componentparent = componentparent_;
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




            planeSize = 0.1f;





            realplanetwidth = planeSize * width;

            map = new int[tinyChunkWidth * tinyChunkHeight * tinyChunkDepth];

            float somenoiseval0 = 200; //100
            float somenoiseval1 = 5; //5

            float _detailScalefloor = 20;
            float _detailScalewall = 200;


            float _heightScalewall = 5;
            float _heightScalefloor = 5;
            float someperlinoffset = 15.0f;
            float someperlindivval = 5.0f;


            float noisevalueinitwall = 20.0f;
            float noisevalueminmax = 0.2f;



            var seed0 = 3420;

            if (typeofterraintiles != 15 && typeofterraintiles != 0)
            {
                for (int x = 0; x < tinyChunkWidth; x++)
                {
                    for (int y = 0; y < tinyChunkHeight; y++)
                    {
                        for (int z = 0; z < tinyChunkDepth; z++)
                        {
                            //map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;


                            /*float noiseXZ = 20;

                            noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (currentPosition.X * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (currentPosition.Y * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (currentPosition.Z * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);
                            Console.WriteLine(noiseXZ);
                            if (noiseXZ >= 0.2f)
                            {
                                map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                            }
                            else if (y == 0 && currentPosition.Y == 0)
                            {
                                map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                            }
                            else
                            {
                                map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 0;
                            }*/

                            float noiseXZ = 10;
                            //noiseXZ *= OriginalSimplexNoise.Noise((((x * planeSize) + chunkPos.X + seed) / _detailScale) * _heightScale, (((z * planeSize) + chunkPos.Z + seed) / _detailScale) * _heightScale);
                            noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * planeSize) + chunkPos.X + seed) / _detailScalewall) * _heightScalewall, (((z * planeSize) + chunkPos.Z + seed) / _detailScalewall) * _heightScalewall, someperlinoffset, someperlinoffset, 0);
                            //Console.WriteLine("chunkPos" + chunkPos.Y);
                            
                            if (chunkPos.Y == 5.0f)
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
                            float size0 = (1 / planeSize) * chunkPos.Y;
                            noiseXZ -= size0;
                            //noise = (noise + 1.0f) * 0.5f;
                            //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                            //float size0 = (1 / planeSize) * chunkPos.Y;
                            //noise -= size0;
                            //Console.WriteLine(noiseXZ + " y: " + y);

                            if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                            {
                                map[x + width * (y + height * z)] = 1;
                            }


                            if (y == 0 && chunkPos.Y == 0)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            if (y == height-1 && chunkPos.Y == 5.0f)
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





            if (typeofterraintiles == 15)
            {
                Vector3 fakepos = chunkPos;
                fakepos.Y = 0;

                for (int x = 0; x < tinyChunkWidth; x++)
                {
                    for (int y = 0; y < tinyChunkHeight; y++)
                    {
                        for (int z = 0; z < tinyChunkDepth; z++)
                        {
                            float noiseXZ = 10;
                            //noiseXZ *= OriginalSimplexNoise.Noise((((x * planeSize) + fakepos.X + seed) / _detailScale) * _heightScale, (((z * planeSize) + fakepos.Z + seed) / _detailScale) * _heightScale);
                            noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * planeSize) + fakepos.X + seed) / _detailScalefloor) * _heightScalefloor, (((z * planeSize) + fakepos.Z + seed) / _detailScalefloor) * _heightScalefloor, someperlinoffset, someperlinoffset, 0);

                            float size0 = (1 / planeSize) * fakepos.Y;
                            noiseXZ -= size0;
                            //noise = (noise + 1.0f) * 0.5f;
                            //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                            //float size0 = (1 / planeSize) * fakepos.Y;
                            //noise -= size0;
                            //Console.WriteLine(noiseXZ + " y: " + y);

                            if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                            {
                                map[x + width * ((height-1- y) + height * z)] = 1;
                            }
                            else if (y == height-1 && fakepos.Y == 0)
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










            if (typeofterraintiles == -2)
            {
                for (int x = 0; x < tinyChunkWidth; x++)
                {
                    for (int y = 0; y < tinyChunkHeight; y++)
                    {
                        for (int z = 0; z < tinyChunkDepth; z++)
                        {
                            //map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;

                            /*float noiseXZ = 10;
                            //noiseXZ *= OriginalSimplexNoise.Noise((((x * planeSize) + chunkPos.X + seed) / _detailScale) * _heightScale, (((z * planeSize) + chunkPos.Z + seed) / _detailScale) * _heightScale);
                            noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * planeSize) + chunkPos.X + seed) / _detailScale) * _heightScale, (((z * planeSize) + chunkPos.Z + seed) / _detailScale) * _heightScale, someperlinoffset, someperlinoffset, 0);

                            float size0 = (1 / planeSize) * chunkPos.Y;
                            noiseXZ -= size0;
                            //noise = (noise + 1.0f) * 0.5f;
                            //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                            //float size0 = (1 / planeSize) * chunkPos.Y;
                            //noise -= size0;
                            //Console.WriteLine(noiseXZ + " y: " + y);

                            if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else if (y == 0 && chunkPos.Y == 0)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                                map[x + width * (y + height * z)] = 0;
                            }*/
                            /* if (y < tinyChunkWidth /1.someperlinoffsetf)
                             {
                                 map[x + width * (y + height * z)] = 1;
                             }*/
                            map[x + width * (y + height * z)] = 1;
                        }
                    }
                }
            }














            if (typeofterraintiles == -3)
            {
                for (int x = 0; x < tinyChunkWidth; x++)
                {
                    for (int y = 0; y < tinyChunkHeight; y++)
                    {
                        for (int z = 0; z < tinyChunkDepth; z++)
                        {
                            //map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;

                            /*float noiseXZ = 10;
                            //noiseXZ *= OriginalSimplexNoise.Noise((((x * planeSize) + chunkPos.X + seed) / _detailScale) * _heightScale, (((z * planeSize) + chunkPos.Z + seed) / _detailScale) * _heightScale);
                            noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * planeSize) + chunkPos.X + seed) / _detailScale) * _heightScale, (((z * planeSize) + chunkPos.Z + seed) / _detailScale) * _heightScale, someperlinoffset, someperlinoffset, 0);

                            float size0 = (1 / planeSize) * chunkPos.Y;
                            noiseXZ -= size0;
                            //noise = (noise + 1.0f) * 0.5f;
                            //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                            //float size0 = (1 / planeSize) * chunkPos.Y;
                            //noise -= size0;
                            //Console.WriteLine(noiseXZ + " y: " + y);

                            if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else if (y == 0 && chunkPos.Y == 0)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                                map[x + width * (y + height * z)] = 0;
                            }*/
                            if (y < tinyChunkWidth / 1.05f)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }

                        }
                    }
                }
            }

            if (typeofterraintiles == 0)
            {
                for (int x = 0; x < tinyChunkWidth; x++)
                {
                    for (int y = 0; y < tinyChunkHeight; y++)
                    {
                        for (int z = 0; z < tinyChunkDepth; z++)
                        {
                            //map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;


                            /*float noiseXZ = 20;

                            noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos.X * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos.Y * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos.Z * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);
                            Console.WriteLine(noiseXZ);
                            if (noiseXZ >= 0.2f)
                            {
                                map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                            }
                            else if (y == 0 && chunkPos.Y == 0)
                            {
                                map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                            }
                            else
                            {
                                map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 0;
                            }*/

                            float noiseXZ = 10;
                            //noiseXZ *= OriginalSimplexNoise.Noise((((x * planeSize) + chunkPos.X + seed) / _detailScalewall) * _heightScale, (((z * planeSize) + chunkPos.Z + seed) / _detailScalewall) * _heightScale);
                            noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * planeSize) + chunkPos.X + seed) / _detailScalefloor) * _heightScalefloor, (((z * planeSize) + chunkPos.Z + seed) / _detailScalefloor) * _heightScalefloor, someperlinoffset, someperlinoffset, 0);

                            float size0 = (1 / planeSize) * chunkPos.Y;
                            noiseXZ -= size0;
                            //noise = (noise + 1.0f) * 0.5f;
                            //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                            //float size0 = (1 / planeSize) * chunkPos.Y;
                            //noise -= size0;
                            //Console.WriteLine(noiseXZ + " y: " + y);

                            if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else if (y == 0 && chunkPos.Y == 0)
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



 






            if (typeofterraintiles == 1)
            {
                //for (int j = 0; j < levelgen.leftWall.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.leftWall[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);

                                    float noiseValue = noisevalueinitwall;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos.X * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((y * staticPlaneSize) + (chunkPos.Y * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((z * staticPlaneSize) + (chunkPos.Z * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall);


                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                    //float noiseValue = noisevalueinitwall;
                                    //noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos.X * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((y * staticPlaneSize) + (chunkPos.Y * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((z * staticPlaneSize) + (chunkPos.Z * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall);

                                    /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos.Y == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }*/
                                    //noiseValue += (10 - (float)y) / 10;
                                    //noiseValue /= (float)y / someperlindivval;



                                    if (noiseValue > noisevalueminmax && y < floorHeight)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue2 = fastNoise.GetNoise(noiseY2, noiseX2, noiseZ2);// Noise.Generate(noiseY2, noiseX2, noiseZ2);

                                    float noiseValue1i = noiseValue2;

                                    noiseValue1i += (someperlindivval - (float)x) / someperlindivval;
                                    noiseValue1i /= (float)x / someperlindivval;
                                    //noiseValue1i = (float)Math.Abs(noiseValue1i);

                                    //Console.WriteLine(noiseValue1i);
                                    if (noiseValue1i > noisevalueminmax)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //leftExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }

                                    //Console.WriteLine(noiseValue + " " + noiseValue1i);
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


            if (typeofterraintiles == 2)
            {
                // for (int j = 0; j < levelgen.rightWall.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.rightWall[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                    float noiseValue = noisevalueinitwall;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos.X * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((y * staticPlaneSize) + (chunkPos.Y * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((z * staticPlaneSize) + (chunkPos.Z * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall);

                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);

                                    //noiseValue += (10 - (float)y) / 10;
                                    //noiseValue /= (float)y / someperlindivval;

                                    /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos.Y == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }*/
                                    if (noiseValue > noisevalueminmax && y < floorHeight)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue3i = noiseValue2;

                                    noiseValue3i += (someperlindivval - (float)x) / someperlindivval;
                                    noiseValue3i /= (float)x / someperlindivval;

                                    if (noiseValue3i < noisevalueminmax)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //rightExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }


                                    if (x == width - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }



            /////////////////////////////////////FRONT WALL/////////////////////////////////
            if (typeofterraintiles == 3)
            {
                //for (int j = 0; j < levelgen.frontWall.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.frontWall[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) /somenoiseval0);
                            float noiseX5 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) /somenoiseval0);6
                                float noiseY5 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) /somenoiseval0);
                                    float noiseZ5 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    //noiseValue += (10 - (float)y) / 10;
                                    //noiseValue /= (float)y / someperlindivval;
                                    float noiseValue = noisevalueinitwall;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos.X * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((y * staticPlaneSize) + (chunkPos.Y * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((z * staticPlaneSize) + (chunkPos.Z * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall);

                                    /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos.Y == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }*/

                                    if (noiseValue > noisevalueminmax && y < floorHeight)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue6i = noiseValue5;

                                    noiseValue6i += (someperlindivval - (float)z) / someperlindivval;
                                    noiseValue6i /= (float)z / someperlindivval;

                                    if (noiseValue6i > noisevalueminmax)
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
            if (typeofterraintiles == 4)
            {
                //for (int j = 0; j < levelgen.backWall.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.backWall[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) /somenoiseval0);
                            float noiseX5 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) /somenoiseval0);
                                float noiseY5 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) /somenoiseval0);
                                    float noiseZ5 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                    float noiseValue = noisevalueinitwall;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos.X * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((y * staticPlaneSize) + (chunkPos.Y * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((z * staticPlaneSize) + (chunkPos.Z * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall);

                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos.Y == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }*/
                                    //noiseValue += (10 - (float)y) / 10;
                                    //noiseValue /= (float)y / someperlindivval;

                                    if (noiseValue > noisevalueminmax && y < floorHeight)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue4i = noiseValue5;

                                    noiseValue4i += (someperlindivval - (float)z) / someperlindivval;
                                    noiseValue4i /= (float)z / someperlindivval;


                                    if (noiseValue4i < noisevalueminmax)
                                    {
                                        map[x + width * (y + height * z)] = 1;
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




            /////////////////////////////////////LEFT FRONT INSIDE CORNER////////////////////////////////
            if (typeofterraintiles == 5)
            {
                //for (int j = 0; j < levelgen.builtLeftFrontInsideCorner.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtLeftFrontInsideCorner[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                    float noiseValue = noisevalueinitwall;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos.X * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((y * staticPlaneSize) + (chunkPos.Y * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((z * staticPlaneSize) + (chunkPos.Z * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall);


                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    //noiseValue += (10 - (float)y) / 10;
                                    //noiseValue /= (float)y / someperlindivval;

                                    /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos.Y == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }*/
                                    if (noiseValue > noisevalueminmax && y < floorHeight)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue2i = noiseValue2;
                                    noiseValue2i += (someperlindivval - (float)x) / someperlindivval;
                                    noiseValue2i /= (float)x / someperlindivval;

                                    float noiseValue5i = noiseValue5;

                                    noiseValue5i += (someperlindivval - (float)z) / someperlindivval;
                                    noiseValue5i /= (float)z / someperlindivval;


                                    if (noiseValue2i > noisevalueminmax || noiseValue5i < noisevalueminmax)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //leftInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }

                                    if (x == 0 || z == depth -1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }





            /////////////////////////////////////RIGHT FRONT INSIDE CORNER////////////////////////////////
            if (typeofterraintiles == 6)
            {
                //for (int j = 0; j < levelgen.builtRightFrontInsideCorner.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtRightFrontInsideCorner[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                                    float noiseValue = noisevalueinitwall;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos.X * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((y * staticPlaneSize) + (chunkPos.Y * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((z * staticPlaneSize) + (chunkPos.Z * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall);

                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    //noiseValue += (10 - (float)y) / 10;
                                    //noiseValue /= (float)y / someperlindivval;

                                    /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos.Y == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }
                                    */


                                    if (noiseValue > noisevalueminmax && y < floorHeight)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue7i = noiseValue2;
                                    noiseValue7i += (someperlindivval - (float)x) / someperlindivval;
                                    noiseValue7i /= (float)x / someperlindivval;

                                    float noiseValue8i = noiseValue5;
                                    noiseValue8i += (someperlindivval - (float)z) / someperlindivval;
                                    noiseValue8i /= (float)z / someperlindivval;

                                    if (noiseValue7i < noisevalueminmax || noiseValue8i < noisevalueminmax)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //rightInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }

                                    if (x == width -1 || z == depth - 1)
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
            if (typeofterraintiles == 7)
            {
                //for (int j = 0; j < levelgen.builtLeftBackInsideCorner.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtLeftBackInsideCorner[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                                    float noiseValue = noisevalueinitwall;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos.X * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((y * staticPlaneSize) + (chunkPos.Y * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((z * staticPlaneSize) + (chunkPos.Z * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall);

                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    //noiseValue += (10 - (float)y) / 10;
                                    //noiseValue /= (float)y / someperlindivval;
                                    /*
                                    if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos.Y == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }*/


                                    if (noiseValue > noisevalueminmax && y < floorHeight)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue9i = noiseValue2;

                                    noiseValue9i += (someperlindivval - (float)x) / someperlindivval;
                                    noiseValue9i /= (float)x / someperlindivval;

                                    float noiseValue10i = noiseValue5;
                                    noiseValue10i += (someperlindivval - (float)z) / someperlindivval;
                                    noiseValue10i /= (float)z / someperlindivval;



                                    if (noiseValue9i > noisevalueminmax || noiseValue10i > noisevalueminmax)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //backInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }

                                    if (x == 0 || z == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }




            /////////////////////////////////////RIGHT BACK INSIDE CORNER////////////////////////////////
            if (typeofterraintiles == 8)
            {
                //for (int j = 0; j < levelgen.builtRightBackInsideCorner.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtRightBackInsideCorner[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                                    float noiseValue = noisevalueinitwall;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos.X * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((y * staticPlaneSize) + (chunkPos.Y * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((z * staticPlaneSize) + (chunkPos.Z * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall);

                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    //noiseValue += (10 - (float)y) / 10;
                                    //noiseValue /= (float)y / someperlindivval;

                                    /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos.Y == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }*/


                                    if (noiseValue > noisevalueminmax && y < floorHeight)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue11i = noiseValue5;
                                    noiseValue11i += (someperlindivval - (float)z) / someperlindivval;
                                    noiseValue11i /= (float)z / someperlindivval;

                                    float noiseValue12i = noiseValue2;

                                    noiseValue12i += (someperlindivval - (float)x) / someperlindivval;
                                    noiseValue12i /= (float)x / someperlindivval;


                                    if (noiseValue11i > noisevalueminmax || noiseValue12i < noisevalueminmax)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //frontInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }

                                    if (x == width -1 || z == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }


            /////////////////////////////////////LEFT FRONT OUTSIDE CORNER////////////////////////////////
            if (typeofterraintiles == 9)
            {
                // for (int j = 0; j < levelgen.builtLeftFrontOutsideCorner.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtLeftFrontOutsideCorner[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                    float noiseValue = noisevalueinitwall;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos.X * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((y * staticPlaneSize) + (chunkPos.Y * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((z * staticPlaneSize) + (chunkPos.Z * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall);

                                    /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos.Y == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }*/

                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    //noiseValue += (10 - (float)y) / 10;
                                    //noiseValue /= (float)y / someperlindivval;

                                    if (noiseValue > noisevalueminmax && y < floorHeight)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue13i = noiseValue2;

                                    noiseValue13i += (someperlindivval - (float)x) / someperlindivval;
                                    noiseValue13i /= (float)x / someperlindivval;

                                    float noiseValue14i = noiseValue5;

                                    noiseValue14i += (someperlindivval - (float)z) / someperlindivval;
                                    noiseValue14i /= (float)z / someperlindivval;


                                    if (noiseValue13i > noisevalueminmax && noiseValue14i < noisevalueminmax)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //leftOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }
                                }
                            }
                        }
                    }
                }
            }



            /////////////////////////////////////RIGHT FRONT OUTSIDE CORNER////////////////////////////////
            if (typeofterraintiles == 10)
            {
                //for (int j = 0; j < levelgen.builtRightFrontOutsideCorner.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtRightFrontOutsideCorner[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                    float noiseValue = noisevalueinitwall;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos.X * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((y * staticPlaneSize) + (chunkPos.Y * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((z * staticPlaneSize) + (chunkPos.Z * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall);


                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    //noiseValue += (10 - (float)y) / 10;
                                    //noiseValue /= (float)y / someperlindivval;

                                    /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos.Y == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }*/


                                    if (noiseValue > noisevalueminmax && y < floorHeight)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue15i = noiseValue2;

                                    noiseValue15i += (someperlindivval - (float)x) / someperlindivval;
                                    noiseValue15i /= (float)x / someperlindivval;

                                    float noiseValue16i = noiseValue5;

                                    noiseValue16i += (someperlindivval - (float)z) / someperlindivval;
                                    noiseValue16i /= (float)z / someperlindivval;


                                    if (noiseValue15i < noisevalueminmax && noiseValue16i < noisevalueminmax)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //rightOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }
                                }
                            }
                        }
                    }
                }
            }


            /////////////////////////////////////LEFT BACK OUTSIDE CORNER////////////////////////////////

            if (typeofterraintiles == 11)
            {
                //for (int j = 0; j < levelgen.builtLeftBackOutsideCorner.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtLeftBackOutsideCorner[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                    float noiseValue = noisevalueinitwall;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos.X * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((y * staticPlaneSize) + (chunkPos.Y * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((z * staticPlaneSize) + (chunkPos.Z * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall);


                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    //noiseValue += (10 - (float)y) / 10;
                                    //noiseValue /= (float)y / someperlindivval;

                                    /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos.Y == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }*/


                                    if (noiseValue > noisevalueminmax && y < floorHeight)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue17i = noiseValue2;

                                    noiseValue17i += (someperlindivval - (float)x) / someperlindivval;
                                    noiseValue17i /= (float)x / someperlindivval;

                                    float noiseValue18i = noiseValue5;

                                    noiseValue18i += (someperlindivval - (float)z) / someperlindivval;
                                    noiseValue18i /= (float)z / someperlindivval;

                                    if (noiseValue17i > noisevalueminmax && noiseValue18i > noisevalueminmax)
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


            /////////////////////////////////////RIGHT BACK OUTSIDE CORNER////////////////////////////////
            if (typeofterraintiles == 12)
            {
                //for (int j = 0; j < levelgen.builtRightBackOutsideCorner.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtRightBackOutsideCorner[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                                    float noiseValue = noisevalueinitwall;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos.X * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((y * staticPlaneSize) + (chunkPos.Y * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((z * staticPlaneSize) + (chunkPos.Z * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall);

                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    //noiseValue += (10 - (float)y) / 10;
                                    //noiseValue /= (float)y / someperlindivval;

                                    /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos.Y == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }*/
                                    if (noiseValue > noisevalueminmax && y < floorHeight)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    
                                    float noiseValue19i = noiseValue5;
                                    noiseValue19i += (someperlindivval - (float)z) / someperlindivval;
                                    noiseValue19i /= (float)z / someperlindivval;

                                    float noiseValue20i = noiseValue2;
                                    noiseValue20i += (someperlindivval - (float)x) / someperlindivval;
                                    noiseValue20i /= (float)x / someperlindivval;


                                    if (noiseValue19i > noisevalueminmax && noiseValue20i < noisevalueminmax)
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









            /////////////////////////////////////LEFT FRONT OUTSIDE CORNER////////////////////////////////
            if (typeofterraintiles == 13)
            {
                // for (int j = 0; j < levelgen.builtLeftFrontOutsideCorner.Count; j++)
                {
                    //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtLeftFrontOutsideCorner[j])
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //float noiseX = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                    float noiseValue = noisevalueinitwall;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos.X * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((y * staticPlaneSize) + (chunkPos.Y * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((z * staticPlaneSize) + (chunkPos.Z * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall);

                                    /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos.Y == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }*/

                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    //noiseValue += (10 - (float)y) / 10;
                                    //noiseValue /= (float)y / someperlindivval;

                                    if (noiseValue > noisevalueminmax && y < floorHeight)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue13i = noiseValue2;

                                    noiseValue13i += (someperlindivval - (float)x) / someperlindivval;
                                    noiseValue13i /= (float)x / someperlindivval;

                                    float noiseValue14i = noiseValue5;

                                    noiseValue14i += (someperlindivval - (float)z) / someperlindivval;
                                    noiseValue14i /= (float)z / someperlindivval;


                                    if (noiseValue13i > noisevalueminmax && noiseValue14i < noisevalueminmax)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //leftOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }

                                    if (noiseValue > noisevalueminmax && y < floorHeight)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue19i = noiseValue5;
                                    noiseValue19i += (someperlindivval - (float)z) / someperlindivval;
                                    noiseValue19i /= (float)z / someperlindivval;

                                    float noiseValue20i = noiseValue2;
                                    noiseValue20i += (someperlindivval - (float)x) / someperlindivval;
                                    noiseValue20i /= (float)x / someperlindivval;


                                    if (noiseValue19i > noisevalueminmax && noiseValue20i < noisevalueminmax)
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
                            //float noiseX = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) /somenoiseval0);
                            float noiseX2 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            float noiseX5 = Math.Abs((float)(x * planeSize + chunkPos.X + seed0) / somenoiseval1);
                            for (int y = 0; y < height; y++)
                            {
                                //float noiseY = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) /somenoiseval0);
                                float noiseY2 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                float noiseY5 = Math.Abs((float)(y * planeSize + chunkPos.Y + seed0) / somenoiseval1);
                                for (int z = 0; z < width; z++)
                                {
                                    //float noiseZ = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) /somenoiseval0);
                                    float noiseZ2 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);
                                    float noiseZ5 = Math.Abs((float)(z * planeSize + chunkPos.Z + seed0) / somenoiseval1);

                                    //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                    float noiseValue = noisevalueinitwall;
                                    noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos.X * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((y * staticPlaneSize) + (chunkPos.Y * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall, (((z * staticPlaneSize) + (chunkPos.Z * alternateStaticPlaneSize) + seed) / _detailScalewall) * _heightScalewall);


                                    float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                    float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                    //noiseValue += (10 - (float)y) / 10;
                                    //noiseValue /= (float)y / someperlindivval;

                                    /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else if (y == 0 && chunkPos.Y == 0)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * (y + height * z)] = 0;
                                    }*/


                                    if (noiseValue > noisevalueminmax && y < floorHeight)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }

                                    float noiseValue15i = noiseValue2;

                                    noiseValue15i += (someperlindivval - (float)x) / someperlindivval;
                                    noiseValue15i /= (float)x / someperlindivval;

                                    float noiseValue16i = noiseValue5;

                                    noiseValue16i += (someperlindivval - (float)z) / someperlindivval;
                                    noiseValue16i /= (float)z / someperlindivval;


                                    if (noiseValue15i < noisevalueminmax && noiseValue16i < noisevalueminmax)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                        //rightOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                    }

                                    float noiseValue17i = noiseValue2;

                                    noiseValue17i += (someperlindivval - (float)x) / someperlindivval;
                                    noiseValue17i /= (float)x / someperlindivval;

                                    float noiseValue18i = noiseValue5;

                                    noiseValue18i += (someperlindivval - (float)z) / someperlindivval;
                                    noiseValue18i /= (float)z / someperlindivval;

                                    if (noiseValue17i > noisevalueminmax && noiseValue18i > noisevalueminmax)
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










            mapper = map;


            return map;
        }

    }
}