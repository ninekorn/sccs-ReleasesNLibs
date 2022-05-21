using System;
using System.Collections.Generic;
using SharpDX;

namespace sccs
{
    public class sccstrigvertbuilderreduced
    {
        //public int[] map;
        private int[] map;
        private int block;

        private Vector4 forward = new Vector4(0, 0, 1, 1);
        private Vector4 back = new Vector4(0, 0, -1, 1);
        private Vector4 right = new Vector4(1, 0, 0, 1);
        private Vector4 left = new Vector4(-1, 0, 0, 1);
        private Vector4 up = new Vector4(0, 1, 0, 1);
        private Vector4 down = new Vector4(0, -1, 0, 1);


        public List<SC_VR_Chunk.DVertex> vertexlist = new List<SC_VR_Chunk.DVertex>(); //listOfVerts
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
        //public void sccsCustomStart(Transform planetchunk_, Vector3 chunkpos_, float planeSize_, float realplanetwidth_, int width_, int height_, int depth_, sccsproceduralplanetbuilderGen2 componentParent_, int addfracturedcubeonimpact_, NewObjectPoolerScript UnityTutorialGameObjectPool_)

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
        OriginalSimplexNoise fastNoise = new OriginalSimplexNoise();
        float staticPlaneSize;
        float alternateStaticPlaneSize;
        private int seed = 3420; // 3420

        int padding0;
        int padding1;
        int padding2;

        int numberOfObjectInWidth; int numberOfObjectInHeight; int numberOfObjectInDepth; int numberOfInstancesPerObjectInWidth; int numberOfInstancesPerObjectInHeight; int numberOfInstancesPerObjectInDepth; float planeSize;

        int tinyChunkWidth; int tinyChunkHeight; int tinyChunkDepth;


        //SC_VR_Chunk_instances componentParentthischunk; SC_VR_ChunkPrim componentParentprim; SC_VR_Chunk componentParentinstance;
        int voxeltype;
        int fullface;

        private int width = 20;
        private int height = 20;
        private int depth = 20;


        //, out Vector3[] norms, out Vector2[] tex
        //, out Vector4[] vertexArray
        public int[] startBuildingArray(Vector3 currentPosition, out Vector4[] vertexArray, out int[] indicesArray, out int[] mapper, out SC_VR_Chunk.DVertex[] dVertexArray, out Vector3[] norms, out Vector2[] tex, float planeSize_) //, out int vertexNum, out int indicesNum
        //public void startBuildingArray(Vector4 currentPosition, out SC_VR_Chunk.DVertex[] vertexArray, out int[] triangleArray, out int[] mapper, int padding0_, int padding1_, int padding2_, int numberOfObjectInWidth_, int numberOfObjectInHeight_, int numberOfObjectInDepth_, int numberOfInstancesPerObjectInWidth_, int numberOfInstancesPerObjectInHeight_, int numberOfInstancesPerObjectInDepth_, int tinyChunkWidth_, int tinyChunkHeight_, int tinyChunkDepth_, float planeSize_, SC_VR_ChunkPrim componentParentprim_, SC_VR_Chunk componentParentinstance_, SC_VR_Chunk_instances componentParentthischunk_, int fullface_, int voxeltype_)
        {
            /*voxeltype = voxeltype_;
            fullface = fullface_;

            //componentParentthischunk = componentParentthischunk_;
            //componentParentprim = componentParentprim_;
            //componentParentinstance = componentParentinstance_;

            padding0 = padding0_;
            padding1 = padding1_;
            padding2 = padding2_;

            tinyChunkWidth = tinyChunkWidth_;
            tinyChunkHeight = tinyChunkHeight_;
            tinyChunkDepth = tinyChunkDepth_;

            numberOfObjectInWidth = numberOfObjectInWidth_;
            numberOfObjectInHeight = numberOfObjectInHeight_;
            numberOfObjectInDepth = numberOfObjectInDepth_;
            numberOfInstancesPerObjectInWidth = numberOfInstancesPerObjectInWidth_;
            numberOfInstancesPerObjectInHeight = numberOfInstancesPerObjectInHeight_;
            numberOfInstancesPerObjectInDepth = numberOfInstancesPerObjectInDepth_;
            planeSize = planeSize_;*/


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

            //_detailScale = 10;
            //_heightScale = 200;

            /*activeBlockType = 0;
            planetchunk = planetchunk_;
            chunkPos = chunkpos_;
            planeSize = planeSize_;
            realplanetwidth = realplanetwidth_;
            width = width_;
            height = height_;
            depth = depth_;*/


            chunkPos = new Vector3(currentPosition.X, currentPosition.Y, currentPosition.Z);
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
            totalints = width * height * depth;

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

            vertexlist = new List<SC_VR_Chunk.DVertex>();

            listOfTriangleIndices = new List<int>();
            //normalslist = new List<Vector3>();
            //colorslist = new List<Vector4>();
            //indexposlist = new List<Vector4>();
            //textureslist = new List<Vector2>();










            realplanetwidth = planeSize * width;

            map = new int[tinyChunkWidth * tinyChunkHeight * tinyChunkDepth];

            for (int x = 0; x < tinyChunkWidth; x++)
            {
                for (int y = 0; y < tinyChunkHeight; y++)
                {
                    for (int z = 0; z < tinyChunkDepth; z++)
                    {
                        //map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;


                        /*float noiseXZ = 20;

                        noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (currentPosition.X * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale, (((y * staticPlaneSize) + (currentPosition.Y * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale, (((z * staticPlaneSize) + (currentPosition.Z * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale);

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
                        }*/

                        float noiseXZ = 10;
                        //noiseXZ *= OriginalSimplexNoise.Noise((((x * planeSize) + currentPosition.X + seed) / _detailScale) * _heightScale, (((z * planeSize) + currentPosition.Z + seed) / _detailScale) * _heightScale);
                        noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * planeSize) + currentPosition.X + seed) / _detailScale) * _heightScale, (((z * planeSize) + currentPosition.Z + seed) / _detailScale) * _heightScale, 15, 15, 0);

                        float size0 = (1 / planeSize) * currentPosition.Y;
                        noiseXZ -= size0;
                        //noise = (noise + 1.0f) * 0.5f;
                        //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                        //float size0 = (1 / planeSize) * currentPosition.Y;
                        //noise -= size0;
                        //Console.WriteLine(noiseXZ + " y: " + y);

                        if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
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

                    }
                }
            }



            sccsSetMap();
            Regenerate(); //currentPosition

            //vertexArray = vertexlist.ToArray();
            //triangleArray = listOfTriangleIndices.ToArray();
            dVertexArray = vertexlist.ToArray();
            indicesArray = listOfTriangleIndices.ToArray();

            vertexArray = new Vector4[vertexlist.Count];

            for (int i = 0;i < vertexArray.Length;i++)
            {
                vertexArray[i] = vertexlist[i].position;
            }


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




            mapper = map;
            return map;
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
                        _chunkVertexArray[t] = 0;
                        _testVertexArray[t] = 0;
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
                    //BOTTOM FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi - 1, zi))
                    {
                        int someswtcBottom = 0;

                        if (componentParent.getChunk(somevalueforBottomx, somevalueforBottomy, somevalueforBottomz) != null)
                        {
                            SC_VR_Chunk someChunk = (SC_VR_Chunk)componentParent.getChunk(somevalueforBottomx, somevalueforBottomy, somevalueforBottomz);

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
















                    //LEFT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    if (IsTransparent(xi - 1, yi, zi))
                    {
                        int someswtcLeft = 0;



                        if (componentParent.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz) != null)
                        {
                            SC_VR_Chunk someChunk = (SC_VR_Chunk)componentParent.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz);

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




                    //BACK FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi, zi - 1))
                    {

                        int someswtcBack = 0;

                        if (componentParent.getChunk(somevalueforBackx, somevalueforBacky, somevalueforBackz) != null)
                        {
                            SC_VR_Chunk someChunk = (SC_VR_Chunk)componentParent.getChunk(somevalueforBackx, somevalueforBacky, somevalueforBackz);

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





                    //TOP FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi + 1, zi))
                    {

                        int someswtcTop = 0;


                        if (componentParent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz) != null)
                        {

                            SC_VR_Chunk someChunk = (SC_VR_Chunk)componentParent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz);


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

                    //RIGHT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    if (IsTransparent(xi + 1, yi, zi))
                    {

                        int someswtcRight = 0;

                        if (componentParent.getChunk(somevalueforRightx, somevalueforRighty, somevalueforRightz) != null)
                        {
                        
                            SC_VR_Chunk someChunk = (SC_VR_Chunk)componentParent.getChunk(somevalueforRightx, somevalueforRighty, somevalueforRightz);
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

                    //FRONT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK intS NOT WORKING ENTIRELY.
                    if (IsTransparent(xi, yi, zi + 1))
                    {
                        int someswtcFront = 0;

                        if (componentParent.getChunk(somevalueforFrontx, somevalueforFronty, somevalueforFrontz) != null)
                        {

                            SC_VR_Chunk someChunk = (SC_VR_Chunk)componentParent.getChunk(somevalueforFrontx, somevalueforFronty, somevalueforFrontz);

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
        void buildTopFace(int xit, int yit, int zit, float block) //int _x, int _y, int _z, Vector3 chunkPos
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





                    if (getChunkVertexint(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 1, 0),
                            //padding0 = padding0,
                            tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 1, 0),
                            //padding0 = padding0,
                            tex = new Vector2(0, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 1, 0),
                            //padding0 = padding0,
                            tex = new Vector2(1, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 1, 0),
                            //padding0 = padding0,
                            tex = new Vector2(0, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexint(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexint(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexint(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                    {
                        _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                        /*listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);
                        */



                        if (voxeltype == 0 || voxeltype == 2)
                        {
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
                        }
                        else if(voxeltype == 3)
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
                        }
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







        void buildTopLeft(int xit, int yit, int zit, float block) //int _x, int _y, int _z, Vector3 chunkPos
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







                    if (getChunkVertexint(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            tex = new Vector2(0, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            tex = new Vector2(1, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(-1, 0, 0),
                            //padding0 = padding0,
                            tex = new Vector2(0, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexint(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexint(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexint(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                    {
                        _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];



                        if (voxeltype == 0 || voxeltype == 2)
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

        void buildTopRight(int xit, int yit, int zit, float block) //int xi, int _y, int _z, Vector3 chunkPos
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
                                            _maxHeight = _yy;
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
                                                    _maxHeight = _yy;
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
                                        _maxHeight = _yy;
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
                                            _maxHeight = _yy;
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
                                                    _maxHeight = _yy;
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
                                            _maxHeight = _yy;
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
                                                    _maxHeight = _yy;
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
                                        _maxHeight = _yy;
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
                                            _maxHeight = _yy;
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
                                                    _maxHeight = _yy;

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





                    if (getChunkVertexint(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(1, 0, 0),
                            //padding0 = padding0,
                            tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(1, 0, 0),
                            //padding0 = padding0,
                            tex = new Vector2(0, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(1, 0, 0),
                            //padding0 = padding0,
                            tex = new Vector2(1, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(1, 0, 0),
                            //padding0 = padding0,
                            tex = new Vector2(0, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexint(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexint(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexint(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                    {
                        _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];




                        if (voxeltype == 0 || voxeltype == 2)
                        {
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index1);
                        }
                        else if (voxeltype == 3)
                        {
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
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
                        }
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




        void buildFrontFace(int xit, int yit, int zit, float block) // int _x, int _y, int _z, Vector3 chunkPos
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
                                            _maxHeight = _yy;
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
                                                    _maxHeight = _yy;
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
                                        _maxHeight = _yy;
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
                                            _maxWidth = _xx + 1;
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
                                                        _maxWidth = _xx + 1;
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
                                                _maxWidth = _xx + 1;
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
                                        _maxWidth = _xx + 1;
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
                                            _maxWidth = _xx + 1;
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
                                                        _maxWidth = _xx + 1;
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
                                                _maxWidth = _xx + 1;
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
                                        _maxWidth = _xx + 1;
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
                                            _maxHeight = _yy;
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
                                                    _maxHeight = _yy;
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
                                            _maxHeight = _yy;
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
                                                    _maxHeight = _yy;
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
                                        _maxHeight = _yy;
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
                                            _maxHeight = _yy;
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
                                                    _maxHeight = _yy;

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




                    if (getChunkVertexint(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 0, 1),
                            //padding0 = padding0,
                            tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 0, 1),
                            //padding0 = padding0,
                            tex = new Vector2(0, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 0, 1),
                            //padding0 = padding0,
                            tex = new Vector2(1, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexint(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 0, 1),
                            //padding0 = padding0,
                            tex = new Vector2(0, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }


                    if (getChunkVertexint(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexint(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexint(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexint(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                    {
                        _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];




                        if (voxeltype == 0 || voxeltype == 2)
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
        }


        void buildBackFace(int xit, int yit, int zit, float block) //int _x, int _y, int zi, Vector3 chunkPos
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
                                            _maxHeight = _yy;
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
                                                    _maxHeight = _yy;
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
                                        _maxHeight = _yy;
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
                                            _maxWidth = _xx + 1;
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
                                                        _maxWidth = _xx + 1;
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
                                                _maxWidth = _xx + 1;
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
                                        _maxWidth = _xx + 1;
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
                                            _maxWidth = _xx + 1;
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
                                                        _maxWidth = _xx + 1;
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
                                                _maxWidth = _xx + 1;
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
                                        _maxWidth = _xx + 1;
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
                                            _maxHeight = _yy;
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
                                                    _maxHeight = _yy;
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
                                            _maxHeight = _yy;
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
                                                    _maxHeight = _yy;
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
                                        _maxHeight = _yy;
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
                                            _maxHeight = _yy;
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
                                                    _maxHeight = _yy;

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






                    if (getChunkVertexint(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 0, -1),
                            //padding0 = padding0,
                            tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 0, -1),
                            //padding0 = padding0,
                            tex = new Vector2(0, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexint(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 0, -1),
                            //padding0 = padding0,
                            tex = new Vector2(1, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }



                    if (getChunkVertexint(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 0, -1),
                            //padding0 = padding0,
                            tex = new Vector2(0, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }


                    if (getChunkVertexint(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexint(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexint(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexint(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                    {
                        _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];



                        if (voxeltype == 0 || voxeltype == 2)
                        {
                         
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index1);
                        }
                        else if (voxeltype == 3)
                        {
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);

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
                        }
                    }
                }
            }
        }




        void buildBottomFace(int xit, int yit, int zit, float block) //int _x, int _y, int _z, Vector3 chunkPos
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
                                            _maxWidth = _xx;
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
                                                    _maxWidth = _xx;
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
                                        _maxWidth = _xx;
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
                                            _maxWidth = _xx;
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
                                                    _maxWidth = _xx;
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
                                            _maxWidth = _xx;
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
                                                    _maxWidth = _xx;
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
                                        _maxWidth = _xx;
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
                                            _maxWidth = _xx;
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
                                                    _maxWidth = _xx;

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







                    if (getChunkVertexint(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, -1, 0),
                            //padding0 = padding0,
                            tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, -1, 0),
                            //padding0 = padding0,
                            tex = new Vector2(0, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexint(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, -1, 0),
                            //padding0 = padding0,
                            tex = new Vector2(1, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }



                    if (getChunkVertexint(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new SC_VR_Chunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize, 1),
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, -1, 0),
                            //padding0 = padding0,
                            tex = new Vector2(0, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }


                    if (getChunkVertexint(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexint(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexint(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexint(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                    {
                        _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];




                        if (voxeltype == 0 || voxeltype == 2)
                        {
                            listOfTriangleIndices.Add(_index0);
                            listOfTriangleIndices.Add(_index1);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index3);
                            listOfTriangleIndices.Add(_index2);
                            listOfTriangleIndices.Add(_index1);
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
                        }
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
            //int width = currentChunk.SC_VR_Chunk.width;
            //int height = currentChunk.SC_VR_Chunk.height;
            //int depth = currentChunk.SC_VR_Chunk.depth;

            //////Debug.Log("x: " + (indexx) + " y: " + (indexy) + " z: " + (indexz));

            int useonlyunitOneForNeighboorIndexPlease = 1;

            if (indexx == 0)
            {
                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z);

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
                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z);
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
                if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
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
                if (componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
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
                if (componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                /*if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z);

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

                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
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

                /*if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);

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
                /*if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X , (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);

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


                if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);

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

                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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

                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                /*if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);

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

                if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);

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

                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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

                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                /*if (componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z- useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);

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

                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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

                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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

                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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

                if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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

                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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



                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
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
                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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

                if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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


                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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

                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
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
                if (componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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

                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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

                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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

                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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

                if (componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    SC_VR_Chunk adjacentChunk = (SC_VR_Chunk)componentParent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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

        public bool IsTransparent(int _x, int _y, int _z)
        {
            if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= width) || (_y >= height) || (_z >= depth)) return true;
            return map[_x + width * (_y + height * _z)] == 0; //_chunkArray
        }

        int getChunkint(int _x, int _y, int _z)
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



        int getChunkVertexint(int _x, int _y, int _z)
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

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(0, 1),
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
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
            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(0f, 0),
            });

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(0f, 1f),
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                normal = new Vector3(0, 1, -1),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                tex = new Vector2(1, 0),

            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
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

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(0, 1f),
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
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

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(0, 1),
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
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

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(0, 1),
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
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
            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 1),
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
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

