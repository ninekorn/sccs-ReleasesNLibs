using System;
using System.Collections.Generic;
using SharpDX;

namespace sccs
{
    public class chunkmesh
    {
        int mapindex0 = 0;
        int mapindex1 = 0;
        int mapindex2 = 0;
        int mapindex3 = 0;

        //public byte[] map;

        public int[] map;
        private int block;

        private Vector4 forward = new Vector4(0, 0, 1, 1);
        private Vector4 back = new Vector4(0, 0, -1, 1);
        private Vector4 right = new Vector4(1, 0, 0, 1);
        private Vector4 left = new Vector4(-1, 0, 0, 1);
        private Vector4 up = new Vector4(0, 1, 0, 1);
        private Vector4 down = new Vector4(0, -1, 0, 1);


        public List<SC_instancedChunk.DVertex> vertexlist = new List<SC_instancedChunk.DVertex>(); //listOfVerts
        public List<int> listOfTriangleIndices = new List<int>();

        //public List<Vector3> vertexlist;
        //public List<Vector3> normalslist;
        //public List<Vector4> colorslist;
        //public List<Vector4> indexposlist;
        //public List<Vector2> textureslist;


        int maxveclength = 4;


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
        public Vector4 somechunkpos;
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
        //public void sccsCustomStart(Transform planetchunk_, Vector3 somechunkpos_, float planeSize_, float realplanetwidth_, int width_, int height_, int depth_, sccsproceduralplanetbuilderGen2 componentParent_, int addfracturedcubeonimpact_, NewObjectPoolerScript UnityTutorialGameObjectPool_)

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
        FastNoise fastNoise = new FastNoise();
        float staticPlaneSize;
        float alternateStaticPlaneSize;
        private int seed = 3420; // 3420
        private int _detailScale = 10; // 10
        private int _HeightScale = 200; //200

        SC_instancedChunk componentParent;

        SC_instancedChunk.DVertex[] vertexArray;
        int[] triangleArray;



        int numberOfObjectInWidth; int numberOfObjectInHeight; int numberOfObjectInDepth; int numberOfInstancesPerObjectInWidth; int numberOfInstancesPerObjectInHeight; int numberOfInstancesPerObjectInDepth;

        int tinyChunkWidth; int tinyChunkHeight; int tinyChunkDepth;
        float planeSize;

        public void startBuildingArray(Vector4 currentPosition,
           out double m11, out double m12, out double m13, out double m14,
           out double m21, out double m22, out double m23, out double m24, out double m31, out double m32, out double m33, out double m34, out double m41, out double m42, out double m43, out double m44, out int[] mapper, SC_instancedChunk componentParent_, int swtcsetneighboors_, int[] someoriginalmap, int numberofmainobjectx, int numberofmainobjecty, int numberofmainobjectz, int numberOfObjectInWidth_, int numberOfObjectInHeight_, int numberOfObjectInDepth_, int numberOfInstancesPerObjectInWidth_, int numberOfInstancesPerObjectInHeight_, int numberOfInstancesPerObjectInDepth_, int tinyChunkWidth_, int tinyChunkHeight_, int tinyChunkDepth_, float planeSize_, int voxeltype_, int typeofbytemapobject_, int mainix, int mainiy, int mainiz, int meshzeroix, int meshzeroiy, int meshzeroiz, int instix, int instiy, int instiz
                       ,
           out double m11b, out double m12b, out double m13b, out double m14b,
           out double m21b, out double m22b, out double m23b, out double m24b, out double m31b, out double m32b, out double m33b, out double m34b, out double m41b, out double m42b, out double m43b, out double m44b
            ,
           out double m11c, out double m12c, out double m13c, out double m14c,
           out double m21c, out double m22c, out double m23c, out double m24c, out double m31c, out double m32c, out double m33c, out double m34c, out double m41c, out double m42c, out double m43c, out double m44c
           ,
           out double m11d, out double m12d, out double m13d, out double m14d,
           out double m21d, out double m22d, out double m23d, out double m24d, out double m31d, out double m32d, out double m33d, out double m34d, out double m41d, out double m42d, out double m43d, out double m44d, int someswtc)// , int somechunkkeyboardpriminstanceindex_, int chunkprimindex_, int chunkinstindex_
        {


            tinyChunkWidth = tinyChunkWidth_;
            tinyChunkHeight = tinyChunkHeight_;
            tinyChunkDepth = tinyChunkDepth_;

            numberOfObjectInWidth = numberOfObjectInWidth_;
            numberOfObjectInHeight = numberOfObjectInHeight_;
            numberOfObjectInDepth = numberOfObjectInDepth_;
            numberOfInstancesPerObjectInWidth = numberOfInstancesPerObjectInWidth_;
            numberOfInstancesPerObjectInHeight = numberOfInstancesPerObjectInHeight_;
            numberOfInstancesPerObjectInDepth = numberOfInstancesPerObjectInDepth_;
            planeSize = planeSize_;






            componentParent = componentParent_;


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
            else if (staticPlaneSize == 0.001f)
            {
                staticPlaneSize = planeSize;
                alternateStaticPlaneSize = planeSize * 1000;
            }


            /*activeBlockType = 0;
            planetchunk = planetchunk_;
            somechunkpos = somechunkpos_;
            planeSize = planeSize_;
            realplanetwidth = realplanetwidth_;
            width = width_;
            height = height_;
            depth = depth_;*/


            somechunkpos = new Vector4(currentPosition.X, currentPosition.Y, currentPosition.Z,1.0f);
            planeSize = planeSize;
            realplanetwidth = 4;
            width = tinyChunkWidth;
            height = tinyChunkHeight;
            depth = tinyChunkDepth;










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

            total = width * height * depth;
            totalBytes = width * height * depth;

            vertexlistWidth = width + 1;
            vertexlistHeight = height + 1;
            vertexlistDepth = depth + 1;
            map = new int[width * height * depth];

            _tempChunkArrayBottomFace = new int[width * height * depth];
            _tempChunkArrayBackFace = new int[width * height * depth];
            _tempChunkArrayFrontFace = new int[width * height * depth];
            _tempChunkArrayLeftFace = new int[width * height * depth];
            _tempChunkArrayRightFace = new int[width * height * depth];
            _tempChunkArray = new int[width * height * depth];

            _chunkArray = new int[width * height * depth];

            _chunkVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

            //vertexlist = new List<Vector3>();

            vertexlist = new List<SC_instancedChunk.DVertex>();

            listOfTriangleIndices = new List<int>();
            //normalslist = new List<Vector3>();
            //colorslist = new List<Vector4>();
            //indexposlist = new List<Vector4>();
            //textureslist = new List<Vector2>();






            _detailScale = 5;
            _HeightScale = 10;
            seed = 3420; // 3420





            realplanetwidth = planeSize * width;

            map = new int[tinyChunkWidth * tinyChunkHeight * tinyChunkDepth];

            for (int x = 0; x < tinyChunkWidth; x++)
            {
                for (int y = 0; y < tinyChunkHeight; y++)
                {
                    for (int z = 0; z < tinyChunkDepth; z++)
                    {
                        //map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;

                        
                        float noiseXZ = 10;

                        noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (currentPosition.X * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale, (((y * staticPlaneSize) + (currentPosition.Y * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale, (((z * staticPlaneSize) + (currentPosition.Z * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale);

                        //Console.WriteLine(noiseXZ);
                        if (noiseXZ >= 0.1f)
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
                        }


                    }
                }
            }



            mapper = map;



            double arrayofbytemaprowm11a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14a = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21a = Program.usetypeofvoxel;// 1; //111111111111111111111111111 
            double arrayofbytemaprowm22a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24a = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34a = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44a = Program.usetypeofvoxel;// 1; //111111111111111111111111111






            double arrayofbytemaprowm11b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14b = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24b = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34b = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44b = Program.usetypeofvoxel;// 1; //111111111111111111111111111




            double arrayofbytemaprowm11c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14c = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24c = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34c = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44c = Program.usetypeofvoxel;// 1; //111111111111111111111111111





            double arrayofbytemaprowm11d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14d = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24d = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34d = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44d = Program.usetypeofvoxel;// 1; //111111111111111111111111111





            total = width * height * depth;

            int switchXX = 0;
            int switchYY = 0;




            double selectablevectordouble = 0;
            int maxv = width * height * depth;



            int m11adder = 0;
            int m12adder = 0;
            int m13adder = 0;
            int m14adder = 0;

            int m21adder = 0;
            int m22adder = 0;
            int m23adder = 0;
            int m24adder = 0;

            int m31adder = 0;
            int m32adder = 0;
            int m33adder = 0;
            int m34adder = 0;

            int m41adder = 0;
            int m42adder = 0;
            int m43adder = 0;
            int m44adder = 0;


            int m11badder = 0;
            int m12badder = 0;
            int m13badder = 0;
            int m14badder = 0;

            int m21badder = 0;
            int m22badder = 0;
            int m23badder = 0;
            int m24badder = 0;

            int m31badder = 0;
            int m32badder = 0;
            int m33badder = 0;
            int m34badder = 0;

            int m41badder = 0;
            int m42badder = 0;
            int m43badder = 0;
            int m44badder = 0;



            int m11cadder = 0;
            int m12cadder = 0;
            int m13cadder = 0;
            int m14cadder = 0;

            int m21cadder = 0;
            int m22cadder = 0;
            int m23cadder = 0;
            int m24cadder = 0;

            int m31cadder = 0;
            int m32cadder = 0;
            int m33cadder = 0;
            int m34cadder = 0;

            int m41cadder = 0;
            int m42cadder = 0;
            int m43cadder = 0;
            int m44cadder = 0;


            int m11dadder = 0;
            int m12dadder = 0;
            int m13dadder = 0;
            int m14dadder = 0;

            int m21dadder = 0;
            int m22dadder = 0;
            int m23dadder = 0;
            int m24dadder = 0;

            int m31dadder = 0;
            int m32dadder = 0;
            int m33dadder = 0;
            int m34dadder = 0;

            int m41dadder = 0;
            int m42dadder = 0;
            int m43dadder = 0;
            int m44dadder = 0;



            sccsSetMap();
            Regenerate(); //currentPosition

            vertexArray = vertexlist.ToArray();
            triangleArray = listOfTriangleIndices.ToArray();



            mapper = map;


            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        //int index = x + (width * (y + (height * z)));
                        //Console.WriteLine("index:" + index);
                        //int currentByte = map[index];

                        int index = x + width * (y + height * z);//; //x + width * (y + height * z);//
                        int currentByte = map[index];

                        //Console.Write(" " + index);


                        int somemaxvecdigit = maxveclength;
                        int somecountermul = 0;
                        int somec = 0;

                        //3 

                        for (int t = 0; t <= index; t++) // index == 45 == 11 
                        {
                            if (somec == somemaxvecdigit)
                            {
                                somecountermul++;
                                somec = 0;
                            }
                            somec++;
                        }


                        switch (somecountermul)
                        {
                            case 0:
                                //selectablevectordouble = arrayofbytemaprowm11a;


                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 10) + currentByte;
                                }

                                m11adder++;
                                break;
                            case 1:
                                //selectablevectordouble = arrayofbytemaprowm12a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 10) + currentByte;
                                }
                                m12adder++;
                                break;
                            case 2:
                                //selectablevectordouble = arrayofbytemaprowm13a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 10) + currentByte;
                                }
                                m13adder++;
                                break;
                            case 3:
                                //selectablevectordouble = arrayofbytemaprowm14a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 10) + currentByte;
                                }
                                m14adder++;
                                break;
                            case 4:
                                //selectablevectordouble = arrayofbytemaprowm21a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 10) + currentByte;
                                }
                                m21adder++;
                                break;
                            case 5:
                                //selectablevectordouble = arrayofbytemaprowm22a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 10) + currentByte;
                                }
                                m22adder++;
                                break;
                            case 6:
                                //selectablevectordouble = arrayofbytemaprowm23a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 10) + currentByte;
                                }
                                m23adder++;
                                break;
                            case 7:
                                //selectablevectordouble = arrayofbytemaprowm24a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 10) + currentByte;
                                }
                                m24adder++;
                                break;
                            case 8:
                                //selectablevectordouble = arrayofbytemaprowm31a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 10) + currentByte;
                                }
                                m31adder++;
                                break;
                            case 9:
                                //selectablevectordouble = arrayofbytemaprowm32a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 10) + currentByte;
                                }
                                m32adder++;

                                break;
                            case 10:
                                //selectablevectordouble = arrayofbytemaprowm33a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 10) + currentByte;
                                }
                                m33adder++;
                                break;
                            case 11:
                                //selectablevectordouble = arrayofbytemaprowm34a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 10) + currentByte;
                                }
                                m34adder++;
                                break;
                            case 12:
                                //selectablevectordouble = arrayofbytemaprowm41a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 10) + currentByte;
                                }
                                m41adder++;
                                break;
                            case 13:
                                //selectablevectordouble = arrayofbytemaprowm42a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 10) + currentByte;
                                }
                                m42adder++;
                                break;
                            case 14:
                                //selectablevectordouble = arrayofbytemaprowm43a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 10) + currentByte;
                                }
                                m43adder++;
                                break;
                            case 15:
                                //selectablevectordouble = arrayofbytemaprowm44a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 10) + currentByte;
                                }
                                m44adder++;
                                break;




                            case 16:
                                //selectablevectorbouble = arrayofbytemaprowm11a;


                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm11b = (arrayofbytemaprowm11b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm11b = (arrayofbytemaprowm11b * 10) + currentByte;
                                }
                                m11badder++;
                                break;
                            case 17:
                                //selectablevectorbouble = arrayofbytemaprowm12a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm12b = (arrayofbytemaprowm12b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm12b = (arrayofbytemaprowm12b * 10) + currentByte;
                                }
                                m12badder++;
                                break;
                            case 18:
                                //selectablevectorbouble = arrayofbytemaprowm13a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm13b = (arrayofbytemaprowm13b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm13b = (arrayofbytemaprowm13b * 10) + currentByte;
                                }
                                m13badder++;
                                break;
                            case 19:
                                //selectablevectorbouble = arrayofbytemaprowm14a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm14b = (arrayofbytemaprowm14b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm14b = (arrayofbytemaprowm14b * 10) + currentByte;
                                }
                                m14badder++;
                                break;
                            case 20:
                                //selectablevectorbouble = arrayofbytemaprowm21a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm21b = (arrayofbytemaprowm21b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm21b = (arrayofbytemaprowm21b * 10) + currentByte;
                                }
                                m21badder++;
                                break;
                            case 21:
                                //selectablevectorbouble = arrayofbytemaprowm22a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm22b = (arrayofbytemaprowm22b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm22b = (arrayofbytemaprowm22b * 10) + currentByte;
                                }
                                m22badder++;
                                break;
                            case 22:
                                //selectablevectorbouble = arrayofbytemaprowm23a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm23b = (arrayofbytemaprowm23b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm23b = (arrayofbytemaprowm23b * 10) + currentByte;
                                }
                                m23badder++;
                                break;
                            case 23:
                                //selectablevectorbouble = arrayofbytemaprowm24a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm24b = (arrayofbytemaprowm24b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm24b = (arrayofbytemaprowm24b * 10) + currentByte;
                                }
                                m24badder++;
                                break;
                            case 24:
                                //selectablevectorbouble = arrayofbytemaprowm31a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm31b = (arrayofbytemaprowm31b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm31b = (arrayofbytemaprowm31b * 10) + currentByte;
                                }
                                m31badder++;
                                break;
                            case 25:
                                //selectablevectorbouble = arrayofbytemaprowm32a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm32b = (arrayofbytemaprowm32b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm32b = (arrayofbytemaprowm32b * 10) + currentByte;
                                }
                                m32badder++;
                                break;
                            case 26:
                                //selectablevectorbouble = arrayofbytemaprowm33a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm33b = (arrayofbytemaprowm33b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm33b = (arrayofbytemaprowm33b * 10) + currentByte;
                                }
                                m33badder++;
                                break;
                            case 27:
                                //selectablevectorbouble = arrayofbytemaprowm34a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm34b = (arrayofbytemaprowm34b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm34b = (arrayofbytemaprowm34b * 10) + currentByte;
                                }
                                m34badder++;
                                break;
                            case 28:
                                //selectablevectorbouble = arrayofbytemaprowm41a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm41b = (arrayofbytemaprowm41b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm41b = (arrayofbytemaprowm41b * 10) + currentByte;
                                }
                                m41badder++;
                                break;
                            case 29:
                                //selectablevectorbouble = arrayofbytemaprowm42a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm42b = (arrayofbytemaprowm42b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm42b = (arrayofbytemaprowm42b * 10) + currentByte;
                                }
                                m42badder++;
                                break;
                            case 30:
                                //selectablevectorbouble = arrayofbytemaprowm43a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm43b = (arrayofbytemaprowm43b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm43b = (arrayofbytemaprowm43b * 10) + currentByte;
                                }
                                m43badder++;
                                break;
                            case 31:
                                //selectablevectorbouble = arrayofbytemaprowm44a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm44b = (arrayofbytemaprowm44b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm44b = (arrayofbytemaprowm44b * 10) + currentByte;
                                }
                                m44badder++;
                                break;








                            case 32:
                                //selectablevectorcouble = arrayofbytemaprowm11a;


                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm11c = (arrayofbytemaprowm11c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm11c = (arrayofbytemaprowm11c * 10) + currentByte;
                                }
                                m11cadder++;
                                break;
                            case 33:
                                //selectablevectorcouble = arrayofbytemaprowm12a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm12c = (arrayofbytemaprowm12c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm12c = (arrayofbytemaprowm12c * 10) + currentByte;
                                }
                                m12cadder++;
                                break;
                            case 34:
                                //selectablevectorcouble = arrayofbytemaprowm13a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm13c = (arrayofbytemaprowm13c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm13c = (arrayofbytemaprowm13c * 10) + currentByte;
                                }
                                m13cadder++;
                                break;
                            case 35:
                                //selectablevectorcouble = arrayofbytemaprowm14a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm14c = (arrayofbytemaprowm14c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm14c = (arrayofbytemaprowm14c * 10) + currentByte;
                                }
                                m14cadder++;
                                break;
                            case 36:
                                //selectablevectorcouble = arrayofbytemaprowm21a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm21c = (arrayofbytemaprowm21c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm21c = (arrayofbytemaprowm21c * 10) + currentByte;
                                }
                                m21cadder++;
                                break;
                            case 37:
                                //selectablevectorcouble = arrayofbytemaprowm22a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm22c = (arrayofbytemaprowm22c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm22c = (arrayofbytemaprowm22c * 10) + currentByte;
                                }
                                m22cadder++;
                                break;
                            case 38:
                                //selectablevectorcouble = arrayofbytemaprowm23a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm23c = (arrayofbytemaprowm23c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm23c = (arrayofbytemaprowm23c * 10) + currentByte;
                                }
                                m23cadder++;
                                break;
                            case 39:
                                //selectablevectorcouble = arrayofbytemaprowm24a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm24c = (arrayofbytemaprowm24c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm24c = (arrayofbytemaprowm24c * 10) + currentByte;
                                }
                                m24cadder++;
                                break;
                            case 40:
                                //selectablevectorcouble = arrayofbytemaprowm31a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm31c = (arrayofbytemaprowm31c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm31c = (arrayofbytemaprowm31c * 10) + currentByte;
                                }
                                m31cadder++;
                                break;
                            case 41:
                                //selectablevectorcouble = arrayofbytemaprowm32a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm32c = (arrayofbytemaprowm32c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm32c = (arrayofbytemaprowm32c * 10) + currentByte;
                                }
                                m32cadder++;
                                break;
                            case 42:
                                //selectablevectorcouble = arrayofbytemaprowm33a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm33c = (arrayofbytemaprowm33c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm33c = (arrayofbytemaprowm33c * 10) + currentByte;
                                }
                                m33cadder++;
                                break;
                            case 43:
                                //selectablevectorcouble = arrayofbytemaprowm34a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm34c = (arrayofbytemaprowm34c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm34c = (arrayofbytemaprowm34c * 10) + currentByte;
                                }
                                m34cadder++;
                                break;
                            case 44:
                                //selectablevectorcouble = arrayofbytemaprowm41a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm41c = (arrayofbytemaprowm41c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm41c = (arrayofbytemaprowm41c * 10) + currentByte;
                                }
                                m41cadder++;
                                break;
                            case 45:
                                //selectablevectorcouble = arrayofbytemaprowm42a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm42c = (arrayofbytemaprowm42c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm42c = (arrayofbytemaprowm42c * 10) + currentByte;
                                }
                                m42cadder++;
                                break;
                            case 46:
                                //selectablevectorcouble = arrayofbytemaprowm43a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm43c = (arrayofbytemaprowm43c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm43c = (arrayofbytemaprowm43c * 10) + currentByte;
                                }
                                m43cadder++;
                                break;
                            case 47:
                                //selectablevectorcouble = arrayofbytemaprowm44a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm44c = (arrayofbytemaprowm44c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm44c = (arrayofbytemaprowm44c * 10) + currentByte;
                                }
                                m44cadder++;
                                break;







                            case 48:
                                //selectablevectordouble = arrayofbytemaprowm11a;


                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm11d = (arrayofbytemaprowm11d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm11d = (arrayofbytemaprowm11d * 10) + currentByte;
                                }
                                m11dadder++;
                                break;
                            case 49:
                                //selectablevectordouble = arrayofbytemaprowm12a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm12d = (arrayofbytemaprowm12d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm12d = (arrayofbytemaprowm12d * 10) + currentByte;
                                }
                                m12dadder++;
                                break;
                            case 50:
                                //selectablevectordouble = arrayofbytemaprowm13a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm13d = (arrayofbytemaprowm13d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm13d = (arrayofbytemaprowm13d * 10) + currentByte;
                                }
                                m13dadder++;
                                break;
                            case 51:
                                //selectablevectordouble = arrayofbytemaprowm14a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm14d = (arrayofbytemaprowm14d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm14d = (arrayofbytemaprowm14d * 10) + currentByte;
                                }
                                m14dadder++;
                                break;
                            case 52:
                                //selectablevectordouble = arrayofbytemaprowm21a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm21d = (arrayofbytemaprowm21d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm21d = (arrayofbytemaprowm21d * 10) + currentByte;
                                }
                                m21dadder++;
                                break;
                            case 53:
                                //selectablevectordouble = arrayofbytemaprowm22a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm22d = (arrayofbytemaprowm22d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm22d = (arrayofbytemaprowm22d * 10) + currentByte;
                                }
                                m22dadder++;
                                break;
                            case 54:
                                //selectablevectordouble = arrayofbytemaprowm23a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm23d = (arrayofbytemaprowm23d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm23d = (arrayofbytemaprowm23d * 10) + currentByte;
                                }
                                m23dadder++;

                                break;
                            case 55:
                                //selectablevectordouble = arrayofbytemaprowm24a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm24d = (arrayofbytemaprowm24d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm24d = (arrayofbytemaprowm24d * 10) + currentByte;
                                }
                                m24dadder++;
                                break;
                            case 56:
                                //selectablevectordouble = arrayofbytemaprowm31a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm31d = (arrayofbytemaprowm31d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm31d = (arrayofbytemaprowm31d * 10) + currentByte;
                                }
                                m31dadder++;
                                break;
                            case 57:
                                //selectablevectordouble = arrayofbytemaprowm32a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm32d = (arrayofbytemaprowm32d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm32d = (arrayofbytemaprowm32d * 10) + currentByte;
                                }
                                m32dadder++;
                                break;
                            case 58:
                                //selectablevectordouble = arrayofbytemaprowm33a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm33d = (arrayofbytemaprowm33d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm33d = (arrayofbytemaprowm33d * 10) + currentByte;
                                }
                                m33dadder++;
                                break;
                            case 59:
                                //selectablevectordouble = arrayofbytemaprowm34a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm34d = (arrayofbytemaprowm34d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm34d = (arrayofbytemaprowm34d * 10) + currentByte;
                                }
                                m34dadder++;
                                break;
                            case 60:
                                //selectablevectordouble = arrayofbytemaprowm41a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm41d = (arrayofbytemaprowm41d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm41d = (arrayofbytemaprowm41d * 10) + currentByte;
                                }
                                m41dadder++;
                                break;
                            case 61:
                                //selectablevectordouble = arrayofbytemaprowm42a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm42d = (arrayofbytemaprowm42d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm42d = (arrayofbytemaprowm42d * 10) + currentByte;
                                }
                                m42dadder++;
                                break;
                            case 62:
                                //selectablevectordouble = arrayofbytemaprowm43a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm43d = (arrayofbytemaprowm43d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm43d = (arrayofbytemaprowm43d * 10) + currentByte;
                                }
                                m43dadder++;
                                break;
                            case 63:
                                //selectablevectordouble = arrayofbytemaprowm44a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm44d = (arrayofbytemaprowm44d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm44d = (arrayofbytemaprowm44d * 10) + currentByte;
                                }
                                m44dadder++;
                                break;

                        };
  
                    }
                }
            }
















            m11 = arrayofbytemaprowm11a;
            m12 = arrayofbytemaprowm12a;
            m13 = arrayofbytemaprowm13a;
            m14 = arrayofbytemaprowm14a;
            m21 = arrayofbytemaprowm21a;
            m22 = arrayofbytemaprowm22a;
            m23 = arrayofbytemaprowm23a;
            m24 = arrayofbytemaprowm24a;
            m31 = arrayofbytemaprowm31a;
            m32 = arrayofbytemaprowm32a;
            m33 = arrayofbytemaprowm33a;
            m34 = arrayofbytemaprowm34a;
            m41 = arrayofbytemaprowm41a;
            m42 = arrayofbytemaprowm42a;
            m43 = arrayofbytemaprowm43a;
            m44 = arrayofbytemaprowm44a;

         

            m11b = arrayofbytemaprowm11b;
            m12b = arrayofbytemaprowm12b;
            m13b = arrayofbytemaprowm13b;
            m14b = arrayofbytemaprowm14b;

            m21b = arrayofbytemaprowm21b;
            m22b = arrayofbytemaprowm22b;
            m23b = arrayofbytemaprowm23b;
            m24b = arrayofbytemaprowm24b;

            m31b = arrayofbytemaprowm31b;
            m32b = arrayofbytemaprowm32b;
            m33b = arrayofbytemaprowm33b;
            m34b = arrayofbytemaprowm34b;

            m41b = arrayofbytemaprowm41b;
            m42b = arrayofbytemaprowm42b;
            m43b = arrayofbytemaprowm43b;
            m44b = arrayofbytemaprowm44b;

        

            m11c = arrayofbytemaprowm11c;
            m12c = arrayofbytemaprowm12c;
            m13c = arrayofbytemaprowm13c;
            m14c = arrayofbytemaprowm14c;

            m21c = arrayofbytemaprowm21c;
            m22c = arrayofbytemaprowm22c;
            m23c = arrayofbytemaprowm23c;
            m24c = arrayofbytemaprowm24c;

            m31c = arrayofbytemaprowm31c;
            m32c = arrayofbytemaprowm32c;
            m33c = arrayofbytemaprowm33c;
            m34c = arrayofbytemaprowm34c;

            m41c = arrayofbytemaprowm41c;
            m42c = arrayofbytemaprowm42c;
            m43c = arrayofbytemaprowm43c;
            m44c = arrayofbytemaprowm44c;




            m11d = arrayofbytemaprowm11d;
            m12d = arrayofbytemaprowm12d;
            m13d = arrayofbytemaprowm13d;
            m14d = arrayofbytemaprowm14d;

            m21d = arrayofbytemaprowm21d;
            m22d = arrayofbytemaprowm22d;
            m23d = arrayofbytemaprowm23d;
            m24d = arrayofbytemaprowm24d;

            m31d = arrayofbytemaprowm31d;
            m32d = arrayofbytemaprowm32d;
            m33d = arrayofbytemaprowm33d;
            m34d = arrayofbytemaprowm34d;

            m41d = arrayofbytemaprowm41d;
            m42d = arrayofbytemaprowm42d;
            m43d = arrayofbytemaprowm43d;
            m44d = arrayofbytemaprowm44d;










        }





        public void setnewmap(out double m11, out double m12, out double m13, out double m14, out double m21, out double m22, out double m23, out double m24, out double m31, out double m32, out double m33, out double m34, out double m41, out double m42, out double m43, out double m44, int chosenmap, int voxelchunkinvertoption

                        ,
            out double m11b, out double m12b, out double m13b, out double m14b,
            out double m21b, out double m22b, out double m23b, out double m24b, out double m31b, out double m32b, out double m33b, out double m34b, out double m41b, out double m42b, out double m43b, out double m44b
             ,
            out double m11c, out double m12c, out double m13c, out double m14c,
            out double m21c, out double m22c, out double m23c, out double m24c, out double m31c, out double m32c, out double m33c, out double m34c, out double m41c, out double m42c, out double m43c, out double m44c
            ,
            out double m11d, out double m12d, out double m13d, out double m14d,
            out double m21d, out double m22d, out double m23d, out double m24d, out double m31d, out double m32d, out double m33d, out double m34d, out double m41d, out double m42d, out double m43d, out double m44d)
        {

            width = tinyChunkWidth;
            height = tinyChunkHeight;
            depth = tinyChunkDepth;



            double arrayofbytemaprowm11a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14a = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21a = Program.usetypeofvoxel;// 1; //111111111111111111111111111 
            double arrayofbytemaprowm22a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24a = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34a = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43a = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44a = Program.usetypeofvoxel;// 1; //111111111111111111111111111




            double arrayofbytemaprowm11b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14b = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24b = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34b = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43b = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44b = Program.usetypeofvoxel;// 1; //111111111111111111111111111




            double arrayofbytemaprowm11c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14c = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24c = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34c = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43c = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44c = Program.usetypeofvoxel;// 1; //111111111111111111111111111





            double arrayofbytemaprowm11d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14d = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24d = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34d = Program.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43d = Program.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44d = Program.usetypeofvoxel;// 1; //111111111111111111111111111





            total = width * height * depth;

            int switchXX = 0;
            int switchYY = 0;




            double selectablevectordouble = 0;
            int maxv = width * height * depth;


            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        //int index = x + (width * (y + (height * z)));
                        //Console.WriteLine("index:" + index);
                        //int currentByte = map[index];

                        int index = x + width * (y + height * z);//; //x + width * (y + height * z);//
                        int currentByte = map[index];

                        //Console.Write(" " + index);


                        int somemaxvecdigit = maxveclength;
                        int somecountermul = 0;
                        int somec = 0;

                        //3 

                        for (int t = 0; t <= index; t++) // index == 45 == 11 
                        {
                            if (somec == somemaxvecdigit)
                            {
                                somecountermul++;
                                somec = 0;
                            }
                            somec++;
                        }


                        switch (somecountermul)
                        {
                            case 0:
                                //selectablevectordouble = arrayofbytemaprowm11a;


                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 10) + 1;
                                }

                                break;
                            case 1:
                                //selectablevectordouble = arrayofbytemaprowm12a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 10) + 1;
                                }

                                break;
                            case 2:
                                //selectablevectordouble = arrayofbytemaprowm13a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 10) + 1;
                                }

                                break;
                            case 3:
                                //selectablevectordouble = arrayofbytemaprowm14a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 10) + 1;
                                }

                                break;
                            case 4:
                                //selectablevectordouble = arrayofbytemaprowm21a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 10) + 1;
                                }

                                break;
                            case 5:
                                //selectablevectordouble = arrayofbytemaprowm22a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 10) + 1;
                                }

                                break;
                            case 6:
                                //selectablevectordouble = arrayofbytemaprowm23a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 10) + 1;
                                }

                                break;
                            case 7:
                                //selectablevectordouble = arrayofbytemaprowm24a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 10) + 1;
                                }

                                break;
                            case 8:
                                //selectablevectordouble = arrayofbytemaprowm31a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 10) + 1;
                                }

                                break;
                            case 9:
                                //selectablevectordouble = arrayofbytemaprowm32a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 10) + 1;
                                }

                                break;
                            case 10:
                                //selectablevectordouble = arrayofbytemaprowm33a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 10) + 1;
                                }

                                break;
                            case 11:
                                //selectablevectordouble = arrayofbytemaprowm34a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 10) + 1;
                                }

                                break;
                            case 12:
                                //selectablevectordouble = arrayofbytemaprowm41a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 10) + 1;
                                }

                                break;
                            case 13:
                                //selectablevectordouble = arrayofbytemaprowm42a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 10) + 1;
                                }

                                break;
                            case 14:
                                //selectablevectordouble = arrayofbytemaprowm43a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 10) + 1;
                                }

                                break;
                            case 15:
                                //selectablevectordouble = arrayofbytemaprowm44a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 10) + 1;
                                }

                                break;




                            case 16:
                                //selectablevectorbouble = arrayofbytemaprowm11a;


                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm11b = (arrayofbytemaprowm11b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm11b = (arrayofbytemaprowm11b * 10) + 1;
                                }

                                break;
                            case 17:
                                //selectablevectorbouble = arrayofbytemaprowm12a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm12b = (arrayofbytemaprowm12b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm12b = (arrayofbytemaprowm12b * 10) + 1;
                                }

                                break;
                            case 18:
                                //selectablevectorbouble = arrayofbytemaprowm13a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm13b = (arrayofbytemaprowm13b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm13b = (arrayofbytemaprowm13b * 10) + 1;
                                }

                                break;
                            case 19:
                                //selectablevectorbouble = arrayofbytemaprowm14a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm14b = (arrayofbytemaprowm14b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm14b = (arrayofbytemaprowm14b * 10) + 1;
                                }

                                break;
                            case 20:
                                //selectablevectorbouble = arrayofbytemaprowm21a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm21b = (arrayofbytemaprowm21b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm21b = (arrayofbytemaprowm21b * 10) + 1;
                                }

                                break;
                            case 21:
                                //selectablevectorbouble = arrayofbytemaprowm22a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm22b = (arrayofbytemaprowm22b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm22b = (arrayofbytemaprowm22b * 10) + 1;
                                }

                                break;
                            case 22:
                                //selectablevectorbouble = arrayofbytemaprowm23a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm23b = (arrayofbytemaprowm23b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm23b = (arrayofbytemaprowm23b * 10) + 1;
                                }

                                break;
                            case 23:
                                //selectablevectorbouble = arrayofbytemaprowm24a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm24b = (arrayofbytemaprowm24b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm24b = (arrayofbytemaprowm24b * 10) + 1;
                                }

                                break;
                            case 24:
                                //selectablevectorbouble = arrayofbytemaprowm31a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm31b = (arrayofbytemaprowm31b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm31b = (arrayofbytemaprowm31b * 10) + 1;
                                }

                                break;
                            case 25:
                                //selectablevectorbouble = arrayofbytemaprowm32a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm32b = (arrayofbytemaprowm32b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm32b = (arrayofbytemaprowm32b * 10) + 1;
                                }

                                break;
                            case 26:
                                //selectablevectorbouble = arrayofbytemaprowm33a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm33b = (arrayofbytemaprowm33b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm33b = (arrayofbytemaprowm33b * 10) + 1;
                                }

                                break;
                            case 27:
                                //selectablevectorbouble = arrayofbytemaprowm34a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm34b = (arrayofbytemaprowm34b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm34b = (arrayofbytemaprowm34b * 10) + 1;
                                }

                                break;
                            case 28:
                                //selectablevectorbouble = arrayofbytemaprowm41a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm41b = (arrayofbytemaprowm41b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm41b = (arrayofbytemaprowm41b * 10) + 1;
                                }

                                break;
                            case 29:
                                //selectablevectorbouble = arrayofbytemaprowm42a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm42b = (arrayofbytemaprowm42b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm42b = (arrayofbytemaprowm42b * 10) + 1;
                                }

                                break;
                            case 30:
                                //selectablevectorbouble = arrayofbytemaprowm43a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm43b = (arrayofbytemaprowm43b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm43b = (arrayofbytemaprowm43b * 10) + 1;
                                }

                                break;
                            case 31:
                                //selectablevectorbouble = arrayofbytemaprowm44a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm44b = (arrayofbytemaprowm44b * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm44b = (arrayofbytemaprowm44b * 10) + 1;
                                }

                                break;








                            case 32:
                                //selectablevectorcouble = arrayofbytemaprowm11a;


                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm11c = (arrayofbytemaprowm11c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm11c = (arrayofbytemaprowm11c * 10) + 1;
                                }

                                break;
                            case 33:
                                //selectablevectorcouble = arrayofbytemaprowm12a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm12c = (arrayofbytemaprowm12c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm12c = (arrayofbytemaprowm12c * 10) + 1;
                                }

                                break;
                            case 34:
                                //selectablevectorcouble = arrayofbytemaprowm13a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm13c = (arrayofbytemaprowm13c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm13c = (arrayofbytemaprowm13c * 10) + 1;
                                }

                                break;
                            case 35:
                                //selectablevectorcouble = arrayofbytemaprowm14a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm14c = (arrayofbytemaprowm14c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm14c = (arrayofbytemaprowm14c * 10) + 1;
                                }

                                break;
                            case 36:
                                //selectablevectorcouble = arrayofbytemaprowm21a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm21c = (arrayofbytemaprowm21c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm21c = (arrayofbytemaprowm21c * 10) + 1;
                                }

                                break;
                            case 37:
                                //selectablevectorcouble = arrayofbytemaprowm22a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm22c = (arrayofbytemaprowm22c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm22c = (arrayofbytemaprowm22c * 10) + 1;
                                }

                                break;
                            case 38:
                                //selectablevectorcouble = arrayofbytemaprowm23a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm23c = (arrayofbytemaprowm23c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm23c = (arrayofbytemaprowm23c * 10) + 1;
                                }

                                break;
                            case 39:
                                //selectablevectorcouble = arrayofbytemaprowm24a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm24c = (arrayofbytemaprowm24c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm24c = (arrayofbytemaprowm24c * 10) + 1;
                                }

                                break;
                            case 40:
                                //selectablevectorcouble = arrayofbytemaprowm31a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm31c = (arrayofbytemaprowm31c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm31c = (arrayofbytemaprowm31c * 10) + 1;
                                }

                                break;
                            case 41:
                                //selectablevectorcouble = arrayofbytemaprowm32a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm32c = (arrayofbytemaprowm32c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm32c = (arrayofbytemaprowm32c * 10) + 1;
                                }

                                break;
                            case 42:
                                //selectablevectorcouble = arrayofbytemaprowm33a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm33c = (arrayofbytemaprowm33c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm33c = (arrayofbytemaprowm33c * 10) + 1;
                                }

                                break;
                            case 43:
                                //selectablevectorcouble = arrayofbytemaprowm34a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm34c = (arrayofbytemaprowm34c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm34c = (arrayofbytemaprowm34c * 10) + 1;
                                }

                                break;
                            case 44:
                                //selectablevectorcouble = arrayofbytemaprowm41a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm41c = (arrayofbytemaprowm41c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm41c = (arrayofbytemaprowm41c * 10) + 1;
                                }

                                break;
                            case 45:
                                //selectablevectorcouble = arrayofbytemaprowm42a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm42c = (arrayofbytemaprowm42c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm42c = (arrayofbytemaprowm42c * 10) + 1;
                                }

                                break;
                            case 46:
                                //selectablevectorcouble = arrayofbytemaprowm43a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm43c = (arrayofbytemaprowm43c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm43c = (arrayofbytemaprowm43c * 10) + 1;
                                }

                                break;
                            case 47:
                                //selectablevectorcouble = arrayofbytemaprowm44a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm44c = (arrayofbytemaprowm44c * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm44c = (arrayofbytemaprowm44c * 10) + 1;
                                }

                                break;







                            case 48:
                                //selectablevectordouble = arrayofbytemaprowm11a;


                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm11d = (arrayofbytemaprowm11d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm11d = (arrayofbytemaprowm11d * 10) + 1;
                                }

                                break;
                            case 49:
                                //selectablevectordouble = arrayofbytemaprowm12a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm12d = (arrayofbytemaprowm12d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm12d = (arrayofbytemaprowm12d * 10) + 1;
                                }

                                break;
                            case 50:
                                //selectablevectordouble = arrayofbytemaprowm13a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm13d = (arrayofbytemaprowm13d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm13d = (arrayofbytemaprowm13d * 10) + 1;
                                }

                                break;
                            case 51:
                                //selectablevectordouble = arrayofbytemaprowm14a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm14d = (arrayofbytemaprowm14d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm14d = (arrayofbytemaprowm14d * 10) + 1;
                                }

                                break;
                            case 52:
                                //selectablevectordouble = arrayofbytemaprowm21a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm21d = (arrayofbytemaprowm21d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm21d = (arrayofbytemaprowm21d * 10) + 1;
                                }

                                break;
                            case 53:
                                //selectablevectordouble = arrayofbytemaprowm22a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm22d = (arrayofbytemaprowm22d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm22d = (arrayofbytemaprowm22d * 10) + 1;
                                }

                                break;
                            case 54:
                                //selectablevectordouble = arrayofbytemaprowm23a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm23d = (arrayofbytemaprowm23d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm23d = (arrayofbytemaprowm23d * 10) + 1;
                                }

                                break;
                            case 55:
                                //selectablevectordouble = arrayofbytemaprowm24a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm24d = (arrayofbytemaprowm24d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm24d = (arrayofbytemaprowm24d * 10) + 1;
                                }

                                break;
                            case 56:
                                //selectablevectordouble = arrayofbytemaprowm31a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm31d = (arrayofbytemaprowm31d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm31d = (arrayofbytemaprowm31d * 10) + 1;
                                }

                                break;
                            case 57:
                                //selectablevectordouble = arrayofbytemaprowm32a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm32d = (arrayofbytemaprowm32d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm32d = (arrayofbytemaprowm32d * 10) + 1;
                                }

                                break;
                            case 58:
                                //selectablevectordouble = arrayofbytemaprowm33a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm33d = (arrayofbytemaprowm33d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm33d = (arrayofbytemaprowm33d * 10) + 1;
                                }

                                break;
                            case 59:
                                //selectablevectordouble = arrayofbytemaprowm34a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm34d = (arrayofbytemaprowm34d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm34d = (arrayofbytemaprowm34d * 10) + 1;
                                }

                                break;
                            case 60:
                                //selectablevectordouble = arrayofbytemaprowm41a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm41d = (arrayofbytemaprowm41d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm41d = (arrayofbytemaprowm41d * 10) + 1;
                                }

                                break;
                            case 61:
                                //selectablevectordouble = arrayofbytemaprowm42a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm42d = (arrayofbytemaprowm42d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm42d = (arrayofbytemaprowm42d * 10) + 1;
                                }

                                break;
                            case 62:
                                //selectablevectordouble = arrayofbytemaprowm43a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm43d = (arrayofbytemaprowm43d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm43d = (arrayofbytemaprowm43d * 10) + 1;
                                }

                                break;
                            case 63:
                                //selectablevectordouble = arrayofbytemaprowm44a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm44d = (arrayofbytemaprowm44d * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm44d = (arrayofbytemaprowm44d * 10) + 1;
                                }

                                break;

                        };

                    }
                }
            }


            m11 = arrayofbytemaprowm11a;
            m12 = arrayofbytemaprowm12a;
            m13 = arrayofbytemaprowm13a;
            m14 = arrayofbytemaprowm14a;
            m21 = arrayofbytemaprowm21a;
            m22 = arrayofbytemaprowm22a;
            m23 = arrayofbytemaprowm23a;
            m24 = arrayofbytemaprowm24a;
            m31 = arrayofbytemaprowm31a;
            m32 = arrayofbytemaprowm32a;
            m33 = arrayofbytemaprowm33a;
            m34 = arrayofbytemaprowm34a;
            m41 = arrayofbytemaprowm41a;
            m42 = arrayofbytemaprowm42a;
            m43 = arrayofbytemaprowm43a;
            m44 = arrayofbytemaprowm44a;




            m11b = arrayofbytemaprowm11b;
            m12b = arrayofbytemaprowm12b;
            m13b = arrayofbytemaprowm13b;
            m14b = arrayofbytemaprowm14b;

            m21b = arrayofbytemaprowm21b;
            m22b = arrayofbytemaprowm22b;
            m23b = arrayofbytemaprowm23b;
            m24b = arrayofbytemaprowm24b;

            m31b = arrayofbytemaprowm31b;
            m32b = arrayofbytemaprowm32b;
            m33b = arrayofbytemaprowm33b;
            m34b = arrayofbytemaprowm34b;

            m41b = arrayofbytemaprowm41b;
            m42b = arrayofbytemaprowm42b;
            m43b = arrayofbytemaprowm43b;
            m44b = arrayofbytemaprowm44b;





            m11c = arrayofbytemaprowm11c;
            m12c = arrayofbytemaprowm12c;
            m13c = arrayofbytemaprowm13c;
            m14c = arrayofbytemaprowm14c;

            m21c = arrayofbytemaprowm21c;
            m22c = arrayofbytemaprowm22c;
            m23c = arrayofbytemaprowm23c;
            m24c = arrayofbytemaprowm24c;

            m31c = arrayofbytemaprowm31c;
            m32c = arrayofbytemaprowm32c;
            m33c = arrayofbytemaprowm33c;
            m34c = arrayofbytemaprowm34c;

            m41c = arrayofbytemaprowm41c;
            m42c = arrayofbytemaprowm42c;
            m43c = arrayofbytemaprowm43c;
            m44c = arrayofbytemaprowm44c;


            m11d = arrayofbytemaprowm11d;
            m12d = arrayofbytemaprowm12d;
            m13d = arrayofbytemaprowm13d;
            m14d = arrayofbytemaprowm14d;

            m21d = arrayofbytemaprowm21d;
            m22d = arrayofbytemaprowm22d;
            m23d = arrayofbytemaprowm23d;
            m24d = arrayofbytemaprowm24d;

            m31d = arrayofbytemaprowm31d;
            m32d = arrayofbytemaprowm32d;
            m33d = arrayofbytemaprowm33d;
            m34d = arrayofbytemaprowm34d;

            m41d = arrayofbytemaprowm41d;
            m42d = arrayofbytemaprowm42d;
            m43d = arrayofbytemaprowm43d;
            m44d = arrayofbytemaprowm44d;




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




            for (int x = 0; x < tinyChunkWidth; x++)
            {
                for (int y = 0; y < tinyChunkHeight; y++)
                {
                    for (int z = 0; z < tinyChunkDepth; z++)
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

                            _maxWidth = width;
                            _maxHeight = height;
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


                    //var neighboorindexx = (int)Math.Floor((somechunkpos.X / planeSize) / fractionOf); //4.654321/0.2 = 23.271605 => 23.271605/fractionOf = floor(2.3f)
                    //var neighboorindexy = (int)Math.Floor((somechunkpos.Y / planeSize) / fractionOf);
                    //var neighboorindexz = (int)Math.Floor((somechunkpos.Z / planeSize) / fractionOf);
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

                    if (somechunkpos.X < 0)
                    {
                        somevalueforTopx = (int)Math.Floor((somechunkpos.X / planeSize) / fractionOf); //(int)Math.Floor(somechunkpos.X / realplanetwidth);
                    }
                    else
                    {
                        somevalueforTopx = (int)Math.Floor((somechunkpos.X / planeSize) / fractionOf); //(int)Math.Floor(somechunkpos.X / realplanetwidth);
                    }

                    if (somechunkpos.Y < 0)
                    {
                        somevalueforTopy = (int)Math.Floor(((somechunkpos.Y + (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((somechunkpos.Y + (planeSize * width)) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforTopy = (int)Math.Floor(((somechunkpos.Y + (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((somechunkpos.Y + (planeSize * width)) / realplanetwidth);
                                                                                                                           //posnot0roundedy -= 1;
                    }

                    if (somechunkpos.Z < 0)
                    {
                        somevalueforTopz = (int)Math.Floor(((somechunkpos.Z) / planeSize) / fractionOf); //(int)Math.Floor(somechunkpos.Z / realplanetwidth);
                                                                                                     //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforTopz = (int)Math.Floor(((somechunkpos.Z) / planeSize) / fractionOf); //(int)Math.Floor(somechunkpos.Z / realplanetwidth);
                    }

                    var somevalueforBottomx = 0;
                    var somevalueforBottomy = 0;
                    var somevalueforBottomz = 0;

                    if (somechunkpos.X < 0)
                    {
                        somevalueforBottomx = (int)Math.Floor(((somechunkpos.X) / planeSize) / fractionOf); //(int)Math.Floor(somechunkpos.X / realplanetwidth);
                    }
                    else
                    {
                        somevalueforBottomx = (int)Math.Floor(((somechunkpos.X) / planeSize) / fractionOf); //(int)Math.Floor(somechunkpos.X / realplanetwidth);
                    }

                    if (somechunkpos.Y < 0)
                    {
                        somevalueforBottomy = (int)Math.Floor(((somechunkpos.Y - (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((somechunkpos.Y - (planeSize * width)) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforBottomy = (int)Math.Floor(((somechunkpos.Y - (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((somechunkpos.Y - (planeSize * width)) / realplanetwidth);
                                                                                                                              //posnot0roundedy -= 1;
                    }

                    if (somechunkpos.Z < 0)
                    {
                        somevalueforBottomz = (int)Math.Floor(((somechunkpos.Z) / planeSize) / fractionOf); //(int)Math.Floor(somechunkpos.Z / realplanetwidth);
                                                                                                        //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforBottomz = (int)Math.Floor(((somechunkpos.Z) / planeSize) / fractionOf); //(int)Math.Floor(somechunkpos.Z / realplanetwidth);
                    }


                    var somevalueforRightx = 0;
                    var somevalueforRighty = 0;
                    var somevalueforRightz = 0;

                    if (somechunkpos.X < 0)
                    {
                        somevalueforRightx = (int)Math.Floor(((somechunkpos.X + (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((somechunkpos.X + (planeSize * width)) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforRightx = (int)Math.Floor(((somechunkpos.X + (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((somechunkpos.X + (planeSize * width)) / realplanetwidth);
                    }

                    if (somechunkpos.Y < 0)
                    {
                        somevalueforRighty = (int)Math.Floor(((somechunkpos.Y) / planeSize) / fractionOf); //(int)Math.Floor((somechunkpos.Y) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforRighty = (int)Math.Floor(((somechunkpos.Y) / planeSize) / fractionOf); //(int)Math.Floor((somechunkpos.Y) / realplanetwidth);
                                                                                                       //posnot0roundedy -= 1;
                    }

                    if (somechunkpos.Z < 0)
                    {
                        somevalueforRightz = (int)Math.Floor(((somechunkpos.Z) / planeSize) / fractionOf); //(int)Math.Floor(somechunkpos.Z / realplanetwidth);
                                                                                                       //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforRightz = (int)Math.Floor(((somechunkpos.Z) / planeSize) / fractionOf); //(int)Math.Floor(somechunkpos.Z / realplanetwidth);
                    }

                    var somevalueforLeftx = 0;
                    var somevalueforLefty = 0;
                    var somevalueforLeftz = 0;

                    if (somechunkpos.X < 0)
                    {
                        somevalueforLeftx = (int)Math.Floor(((somechunkpos.X - (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((somechunkpos.X - (planeSize * width)) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforLeftx = (int)Math.Floor(((somechunkpos.X - (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((somechunkpos.X - (planeSize * width)) / realplanetwidth);
                    }

                    if (somechunkpos.Y < 0)
                    {
                        somevalueforLefty = (int)Math.Floor(((somechunkpos.Y) / planeSize) / fractionOf); //(int)Math.Floor((somechunkpos.Y) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforLefty = (int)Math.Floor(((somechunkpos.Y) / planeSize) / fractionOf); //(int)Math.Floor((somechunkpos.Y) / realplanetwidth);
                                                                                                      //posnot0roundedy -= 1;
                    }

                    if (somechunkpos.Z < 0)
                    {
                        somevalueforLeftz = (int)Math.Floor(((somechunkpos.Z) / planeSize) / fractionOf); //(int)Math.Floor(somechunkpos.Z / realplanetwidth);
                                                                                                      //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforLeftz = (int)Math.Floor(((somechunkpos.Z) / planeSize) / fractionOf); //(int)Math.Floor(somechunkpos.Z / realplanetwidth);
                    }





                    var somevalueforFrontx = 0;
                    var somevalueforFronty = 0;
                    var somevalueforFrontz = 0;

                    if (somechunkpos.X < 0)
                    {
                        somevalueforFrontx = (int)Math.Floor(((somechunkpos.X) / planeSize) / fractionOf); // (int)Math.Floor((somechunkpos.X) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforFrontx = (int)Math.Floor(((somechunkpos.X) / planeSize) / fractionOf); //(int)Math.Floor((somechunkpos.X) / realplanetwidth);
                    }

                    if (somechunkpos.Y < 0)
                    {
                        somevalueforFronty = (int)Math.Floor(((somechunkpos.Y) / planeSize) / fractionOf); // (int)Math.Floor((somechunkpos.Y) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforFronty = (int)Math.Floor(((somechunkpos.Y) / planeSize) / fractionOf); //(int)Math.Floor((somechunkpos.Y) / realplanetwidth);
                                                                                                       //posnot0roundedy -= 1;
                    }

                    if (somechunkpos.Z < 0)
                    {
                        somevalueforFrontz = (int)Math.Floor(((somechunkpos.Z + (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((somechunkpos.Z + (planeSize * width)) / realplanetwidth);
                                                                                                                             //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforFrontz = (int)Math.Floor(((somechunkpos.Z + (planeSize * width)) / planeSize) / fractionOf); //(int)Math.Floor((somechunkpos.Z + (planeSize * width)) / realplanetwidth);
                    }





                    var somevalueforBackx = 0;
                    var somevalueforBacky = 0;
                    var somevalueforBackz = 0;

                    if (somechunkpos.X < 0)
                    {
                        somevalueforBackx = (int)Math.Floor(((somechunkpos.X) / planeSize) / fractionOf); // (int)Math.Floor((somechunkpos.X) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforBackx = (int)Math.Floor(((somechunkpos.X) / planeSize) / fractionOf); // (int)Math.Floor((somechunkpos.X) / realplanetwidth);
                    }

                    if (somechunkpos.Y < 0)
                    {
                        somevalueforBacky = (int)Math.Floor(((somechunkpos.Y) / planeSize) / fractionOf); //(int)Math.Floor((somechunkpos.Y) / realplanetwidth);
                    }
                    else
                    {
                        somevalueforBacky = (int)Math.Floor(((somechunkpos.Y) / planeSize) / fractionOf); // (int)Math.Floor((somechunkpos.Y) / realplanetwidth);
                                                                                                      //posnot0roundedy -= 1;
                    }

                    if (somechunkpos.Z < 0)
                    {
                        somevalueforBackz = (int)Math.Floor(((somechunkpos.Z - (planeSize * width)) / planeSize) / fractionOf); // (int)Math.Floor((somechunkpos.Z - (planeSize * width)) / realplanetwidth);
                                                                                                                            //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforBackz = (int)Math.Floor(((somechunkpos.Z - (planeSize * width)) / planeSize) / fractionOf); // (int)Math.Floor((somechunkpos.Z - (planeSize * width)) / realplanetwidth);
                    }










                    /*
                    if (somechunkpos.X < 0)
                    {
                        somevalueforTopx = (int)Math.Floor(somechunkpos.X) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforTopx = (int)Math.Floor(somechunkpos.X) / realplanetwidth;
                    }

                    if (somechunkpos.Y < 0)
                    {
                        somevalueforTopy = (int)Math.Floor((somechunkpos.Y + realplanetwidth)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforTopy = (int)Math.Floor((somechunkpos.Y + realplanetwidth)) / realplanetwidth;
                        //posnot0roundedy -= 1;
                    }

                    if (somechunkpos.Z < 0)
                    {
                        somevalueforTopz = (int)Math.Floor(somechunkpos.Z) / realplanetwidth;
                        //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforTopz = (int)Math.Floor(somechunkpos.Z) / realplanetwidth;
                    }

                    var somevalueforBottomx = 0;
                    var somevalueforBottomy = 0;
                    var somevalueforBottomz = 0;

                    if (somechunkpos.X < 0)
                    {
                        somevalueforBottomx = (int)Math.Floor(somechunkpos.X) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforBottomx = (int)Math.Floor(somechunkpos.X) / realplanetwidth;
                    }

                    if (somechunkpos.Y < 0)
                    {
                        somevalueforBottomy = (int)Math.Floor((somechunkpos.Y - realplanetwidth)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforBottomy = (int)Math.Floor((somechunkpos.Y - realplanetwidth)) / realplanetwidth;
                        //posnot0roundedy -= 1;
                    }

                    if (somechunkpos.Z < 0)
                    {
                        somevalueforBottomz = (int)Math.Floor(somechunkpos.Z) / realplanetwidth;
                        //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforBottomz = (int)Math.Floor(somechunkpos.Z) / realplanetwidth;
                    }


                    var somevalueforRightx = 0;
                    var somevalueforRighty = 0;
                    var somevalueforRightz = 0;

                    if (somechunkpos.X < 0)
                    {
                        somevalueforRightx = (int)Math.Floor((somechunkpos.X + realplanetwidth)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforRightx = (int)Math.Floor((somechunkpos.X + realplanetwidth)) / realplanetwidth;
                    }

                    if (somechunkpos.Y < 0)
                    {
                        somevalueforRighty = (int)Math.Floor((somechunkpos.Y)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforRighty = (int)Math.Floor((somechunkpos.Y)) / realplanetwidth;
                        //posnot0roundedy -= 1;
                    }

                    if (somechunkpos.Z < 0)
                    {
                        somevalueforRightz = (int)Math.Floor(somechunkpos.Z) / realplanetwidth;
                        //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforRightz = (int)Math.Floor(somechunkpos.Z) / realplanetwidth;
                    }

                    var somevalueforLeftx = 0;
                    var somevalueforLefty = 0;
                    var somevalueforLeftz = 0;

                    if (somechunkpos.X < 0)
                    {
                        somevalueforLeftx = (int)Math.Floor((somechunkpos.X - realplanetwidth)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforLeftx = (int)Math.Floor((somechunkpos.X - realplanetwidth)) / realplanetwidth;
                    }

                    if (somechunkpos.Y < 0)
                    {
                        somevalueforLefty = (int)Math.Floor((somechunkpos.Y)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforLefty = (int)Math.Floor((somechunkpos.Y)) / realplanetwidth;
                        //posnot0roundedy -= 1;
                    }

                    if (somechunkpos.Z < 0)
                    {
                        somevalueforLeftz = (int)Math.Floor(somechunkpos.Z) / realplanetwidth;
                        //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforLeftz = (int)Math.Floor(somechunkpos.Z) / realplanetwidth;
                    }





                    var somevalueforFrontx = 0;
                    var somevalueforFronty = 0;
                    var somevalueforFrontz = 0;

                    if (somechunkpos.X < 0)
                    {
                        somevalueforFrontx = (int)Math.Floor((somechunkpos.X)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforFrontx = (int)Math.Floor((somechunkpos.X)) / realplanetwidth;
                    }

                    if (somechunkpos.Y < 0)
                    {
                        somevalueforFronty = (int)Math.Floor((somechunkpos.Y)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforFronty = (int)Math.Floor((somechunkpos.Y)) / realplanetwidth;
                        //posnot0roundedy -= 1;
                    }

                    if (somechunkpos.Z < 0)
                    {
                        somevalueforFrontz = (int)Math.Floor((somechunkpos.Z + realplanetwidth)) / realplanetwidth;
                        //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforFrontz = (int)Math.Floor((somechunkpos.Z + realplanetwidth)) / realplanetwidth;
                    }





                    var somevalueforBackx = 0;
                    var somevalueforBacky = 0;
                    var somevalueforBackz = 0;

                    if (somechunkpos.X < 0)
                    {
                        somevalueforBackx = (int)Math.Floor((somechunkpos.X)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforBackx = (int)Math.Floor((somechunkpos.X)) / realplanetwidth;
                    }

                    if (somechunkpos.Y < 0)
                    {
                        somevalueforBacky = (int)Math.Floor((somechunkpos.Y)) / realplanetwidth;
                    }
                    else
                    {
                        somevalueforBacky = (int)Math.Floor((somechunkpos.Y)) / realplanetwidth;
                        //posnot0roundedy -= 1;
                    }

                    if (somechunkpos.Z < 0)
                    {
                        somevalueforBackz = (int)Math.Floor((somechunkpos.Z - realplanetwidth)) / realplanetwidth;
                        //posnot0roundedz += 1;
                    }
                    else
                    {
                        somevalueforBackz = (int)Math.Floor((somechunkpos.Z - realplanetwidth)) / realplanetwidth;
                    }*/




































                    /*
                    
                    //BOTTOM FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi - 1, zi))
                    {
                        int someswtcBottom = 0;

                        if (componentParent.getChunk(somevalueforBottomx, somevalueforBottomy, somevalueforBottomz) != null)
                        {
                             SC_instancedChunk_instances someChunk = (SC_instancedChunk_instances)componentParent.getChunk(somevalueforBottomx, somevalueforBottomy, somevalueforBottomz);

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
                             SC_instancedChunk_instances someChunk = (SC_instancedChunk_instances)componentParent.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz);

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
                             SC_instancedChunk_instances someChunk = (SC_instancedChunk_instances)componentParent.getChunk(somevalueforBackx, somevalueforBacky, somevalueforBackz);

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

                             SC_instancedChunk_instances someChunk = (SC_instancedChunk_instances)componentParent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz);


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
                        
                             SC_instancedChunk_instances someChunk = (SC_instancedChunk_instances)componentParent.getChunk(somevalueforRightx, somevalueforRighty, somevalueforRightz);
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

                             SC_instancedChunk_instances someChunk = (SC_instancedChunk_instances)componentParent.getChunk(somevalueforFrontx, somevalueforFronty, somevalueforFrontz);

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

                        if (someswtcFront == 1)
                        {
                            buildFrontFace();
                        }
                        //buildFrontFace();
                    }*/









                    
                    if (IsTransparent(xi, yi + 1, zi))
                    {
                        buildTopFace();
                    }
                    
                    //WORKING
                    //LEFT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                    /*if (IsTransparent(xi, yi - 1, zi))
                    {
                        buildBottomFace();
                    }*/
                    /*if (IsTransparent(xi - 1, yi, zi))
                    {

                        buildTopLeft();
                    }

                    if (IsTransparent(xi + 1, yi, zi))
                    {
                        buildTopRight();
                    }

                    if (IsTransparent(xi, yi, zi + 1))
                    {
                        buildFrontFace();
                    }

                    //BACK FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi, zi - 1))
                    {
                        buildBackFace();
                    }*/








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

        //UnityEngine.Debug.Log("_xx: " + _xx + " _zz: " + _zz + " _maxWidth: " + _maxWidth + " _maxDepth: " + _maxDepth + " rowIterateX: " + rowIterateX + " rowIterateZ: " + rowIterateZ);
        void buildTopFace() //int _x, int _y, int _z, Vector3 somechunkpos
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

                //if (IsTransparent(temptopfacexi, temptopfaceyi + 1, temptopfacezi))
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + somechunkpos, Quaternion.identity);

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
                                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + somechunkpos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                            {
                                                                fourVertIndexX = threeVertIndexX;
                                                                fourVertIndexY = yi + 1;
                                                                fourVertIndexZ = twoVertIndexZ;
                                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                            {
                                                                fourVertIndexX = threeVertIndexX;
                                                                fourVertIndexY = yi + 1;
                                                                fourVertIndexZ = twoVertIndexZ;
                                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                                            }
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi + 1;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                                if (foundVertTwo)
                                                {
                                                    if (foundVertThree)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                                            }
                                            else if (_block == 1 || _block == 2)
                                            {
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi + 1;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                                            }
                                        }

                                        if (!blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(rowIterateX, yi, rowIterateZ))
                            {
                                _tempChunkArray[(rowIterateX) + width * (yi + height * (rowIterateZ))] = 2;
                                ////////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + somechunkpos, Quaternion.identity);
                            }
                        }
                    }





                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            tex = new Vector2(1, 1),
                        });

                        mapindex0 = (oneVertIndexX) + width * (yi + height * (oneVertIndexZ));
                        //mapindex0 = (width-1-rowIterateX) + width * ((height - 1 - rowIterateY) + height * (rowIterateZ));
                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            tex = new Vector2(0, 1),
                        });

                        mapindex1 = (twoVertIndexX) + width * (yi + height * (twoVertIndexZ-1));
                        //mapindex1 = (xi) + width * (yi + height * (zi));
                        //mapindex1 = (rowIterateX) + width * (rowIterateY + height * (rowIterateZ));
                        //mapindex1 = (rowIterateX) + width * ((height - 1 - rowIterateY) + height * (rowIterateZ));
                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            tex = new Vector2(1, 0),
                        });
                        mapindex2 = (threeVertIndexX - 1) + width * (yi + height * (zi));
                        //mapindex2 = (xi) + width * (yi + height * (zi));
                        //mapindex2 = (width-1- rowIterateX) + width * ((height - 1 - rowIterateY) + height * (depth-1- rowIterateZ));
                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + somechunkpos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            tex = new Vector2(0, 0),
                        });
                        mapindex3 = (fourVertIndexX - 1) + width * (yi + height * (fourVertIndexZ-1));
                        //mapindex3 = (xi) + width * (yi + height * (zi));
                        //mapindex3 = (rowIterateX) + width * ((height - 1 - rowIterateY) + height * (depth - 1 - rowIterateZ));
                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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


                        map[mapindex0] = 2;
                        map[mapindex1] = 2;
                        map[mapindex2] = 2;
                        map[mapindex3] = 2;

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







        void buildTopLeft() //int _x, int _y, int _z, Vector3 somechunkpos
        {
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
                if (IsTransparent(xi - 1, yi, zi))
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
                                    oneVertIndexX = xi;
                                    oneVertIndexY = rowIterateY;
                                    oneVertIndexZ = rowIterateZ;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + somechunkpos, Quaternion.identity);

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
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + somechunkpos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = xi;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = xi;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                                        }
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);
                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = xi;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                                    fourVertIndexX = xi;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(xi, rowIterateY, rowIterateZ))
                            {
                                _tempChunkArrayLeftFace[(xi) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                                ////////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + somechunkpos, Quaternion.identity);
                            }
                        }
                    }








                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, -1),
                            tex = new Vector2(0, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {

                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, -1),
                            tex = new Vector2(0, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, -1),
                            tex = new Vector2(1, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + somechunkpos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                    }
                    if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, -1),
                            tex = new Vector2(1, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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

        void buildTopRight() //int xi, int _y, int _z, Vector3 somechunkpos
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
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + somechunkpos, Quaternion.identity);

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
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + somechunkpos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = xi + 1;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = xi + 1;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                                        }
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);
                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi + 1;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi + 1;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(xi, rowIterateY, rowIterateZ))
                            {
                                _tempChunkArrayRightFace[(xi) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                                ////////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + somechunkpos, Quaternion.identity);
                            }
                        }
                    }






                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 0, -1),
                            tex = new Vector2(1, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {

                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 0, -1),
                            tex = new Vector2(1, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 0, -1),
                            tex = new Vector2(0, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + somechunkpos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                    }
                    if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 0, -1),
                            tex = new Vector2(0, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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




        void buildFrontFace() // int _x, int _y, int _z, Vector3 somechunkpos
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
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + _somechunkpos, Quaternion.identity);

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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + _somechunkpos, Quaternion.identity);

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
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _somechunkpos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _somechunkpos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _somechunkpos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi + 1;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _somechunkpos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _somechunkpos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _somechunkpos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _somechunkpos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi + 1;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _somechunkpos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
                                        }
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _somechunkpos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _somechunkpos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _somechunkpos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi + 1;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _somechunkpos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _somechunkpos, Quaternion.identity);

                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _somechunkpos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - ziz) * planeSize + _somechunkpos, Quaternion.identity);

                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _somechunkpos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi + 1;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(rowIterateX, rowIterateY, zi))
                            {
                                _tempChunkArrayFrontFace[(rowIterateX) + width * (rowIterateY + height * (zi))] = 2;
                                //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + _somechunkpos, Quaternion.identity);
                            }
                        }
                    }





                    //looping in x than y
                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize, 1),

                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 0, 0),
                            tex = new Vector2(0, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 0, 0),
                            tex = new Vector2(1, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }




                    if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 0, 0),
                            tex = new Vector2(1, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                        _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 0, 0),
                            tex = new Vector2(0, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + somechunkpos, Quaternion.identity);
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


        void buildBackFace() //int _x, int _y, int zi, Vector3 somechunkpos
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
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + _somechunkpos, Quaternion.identity);

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
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _somechunkpos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _somechunkpos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _somechunkpos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _somechunkpos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _somechunkpos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _somechunkpos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _somechunkpos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _somechunkpos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
                                        }
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _somechunkpos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _somechunkpos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _somechunkpos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _somechunkpos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _somechunkpos, Quaternion.identity);

                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _somechunkpos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - ziz) * planeSize + _somechunkpos, Quaternion.identity);

                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _somechunkpos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _somechunkpos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(rowIterateX, rowIterateY, zi))
                            {
                                _tempChunkArrayBackFace[(rowIterateX) + width * (rowIterateY + height * (zi))] = 2;
                                //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + _somechunkpos, Quaternion.identity);
                            }
                        }
                    }


                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(0, 0, -1),
                            tex = new Vector2(1, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {

                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(0, 0, -1),
                            tex = new Vector2(0, 1),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(0, 0, -1),
                            tex = new Vector2(1, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + somechunkpos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                    }
                    if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(0, 0, -1),
                            tex = new Vector2(0, 0),
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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




        void buildBottomFace() //int _x, int _y, int _z, Vector3 somechunkpos
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
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize + somechunkpos, Quaternion.identity);

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
                                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize + somechunkpos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                                        }
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + somechunkpos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                    ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, yi + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + somechunkpos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(rowIterateX, yi, rowIterateZ))
                            {
                                _tempChunkArrayBottomFace[(rowIterateX) + width * (yi + height * (rowIterateZ))] = 2;
                                //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + somechunkpos, Quaternion.identity);
                            }
                        }
                    }





                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            tex = new Vector2(1, 1),
                        });

                        mapindex0 = (xi) + width * (yi + height * (zi));
                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            tex = new Vector2(0, 1),
                        });
                        mapindex1 = (rowIterateX) + width * (rowIterateY + height * (rowIterateZ));
                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            tex = new Vector2(1, 0),
                        });
                        mapindex2 = (rowIterateX) + width * (rowIterateY + height * (rowIterateZ));
                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + somechunkpos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            tex = new Vector2(0, 0),
                        });
                        mapindex3 = (rowIterateX) + width * (rowIterateY + height * (rowIterateZ));
                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + somechunkpos, Quaternion.identity);
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

                        map[mapindex0] = 2;



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
            //int width = currentChunk.SC_instancedChunk.width;
            //int height = currentChunk.SC_instancedChunk.height;
            //int depth = currentChunk.SC_instancedChunk.depth;

            //////Debug.Log("x: " + (indexx) + " y: " + (indexy) + " z: " + (indexz));

            int useonlyunitOneForNeighboorIndexPlease = 1;

            if (indexx == 0)
            {
                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z) != null)
                {
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z);

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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z);

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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);

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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X , (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);

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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);

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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);

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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);

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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);

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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                    SC_instancedChunk adjacentChunk = (SC_instancedChunk_instances)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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




















        /*
        public void Regenerate(Vector4 currentPosition)
        {
            for (int x = 0; x < tinyChunkWidth; x++)
            {
                for (int y = 0; y < tinyChunkHeight; y++)
                {
                    for (int z = 0; z < tinyChunkDepth; z++)
                    {

                        block = map[x + tinyChunkWidth * (y + tinyChunkHeight * z)];

                        DrawBrick(x, y, z, currentPosition, block);
                    }
                }
            }
        }

        public void DrawBrick(int x, int y, int z, Vector4 currentPosition, int block)
        {
            Vector4 start = new Vector4(x * planeSize, y * planeSize, z * planeSize, 1);
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

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(0, 1),
            });


            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1 + offset2,
                //indexPos = new Vector4(x, y, z, block),
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
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(0f, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(0f, 1f),
            });


            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                normal = new Vector3(0, 1, -1),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                tex = new Vector2(1, 0),

            });


            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1 + offset2,
                //indexPos = new Vector4(x, y, z, block),
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

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(0, 1f),
            });


            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1 + offset2,
                //indexPos = new Vector4(x, y, z, block),
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

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(0, 1),
            });


            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1 + offset2,
                //indexPos = new Vector4(x, y, z, block),
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

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(0, 1),
            });


            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1 + offset2,
                //indexPos = new Vector4(x, y, z, block),
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
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 1),
            });


            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1 + offset2,
                //indexPos = new Vector4(x, y, z, block),
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
            if ((x < 0) || (y < 0) || (z < 0) || (x >= tinyChunkWidth) || (y >= tinyChunkHeight) || (z >= tinyChunkDepth)) return true;
            {
                return map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] == 0;
            }
        }*/
    }
}

