using System;
using System.Collections.Generic;
using SharpDX;

using SimplexNoise;

using System.Collections;

namespace sccs
{
    public class sclevelgenvert
    {

        int arrayindex;

        //public int[] map;
        public int[] map;
        private int block;

        private Vector4 forward = new Vector4(0, 0, 1, 1);
        private Vector4 back = new Vector4(0, 0, -1, 1);
        private Vector4 right = new Vector4(1, 0, 0, 1);
        private Vector4 left = new Vector4(-1, 0, 0, 1);
        private Vector4 up = new Vector4(0, 1, 0, 1);
        private Vector4 down = new Vector4(0, -1, 0, 1);


        public List<sclevelgenclass.DVertex> vertexlist = new List<sclevelgenclass.DVertex>(); //listOfVerts
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


        //sclevelgenclass componentparent;
        sclevelgenclassPrim componentparent;

        //sclevelgenclass_instances componentparentthischunk; sclevelgenclassPrim componentparentprim; sclevelgenclass componentparentinstance;
        int voxeltype;
        int fullface;

        public int width = 10;
        public int height = 10;
        public int depth = 10;

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

        //public static Dictionary<sclevelgenvert, Vector3> chunkz = new Dictionary<sclevelgenvert, Vector3>();

        int typeofterraintiles;
        sclevelgenvert[] listofchunksadjacent = new sclevelgenvert[6];
        sclevelgenvert[] listofchunksadjacentfloor = new sclevelgenvert[6];




        float levellimitroofy = 5.0f;
        float levellimitfloory = 0.0f;
        // out Vector3[] norms, out Vector2[] tex
        Vector4 chunkoriginpos;
        public int levelofdetail = 1;
        public int[] startBuildingArray(Vector3 currentPosition, out int[] indicesArray, out int[] mapper, out sclevelgenclass.DVertex[] dVertexArray, float planeSize_, LevelGenerator4 levelgen_, Vector3 _chunkPos, int width_, int height_, int depth_, int typeofterraintiles_, sclevelgenclassPrim componentparent_, int[] map_, int levelofdetail_) //, out int vertexNum, out int indicesNum
        //public void startBuildingArray(Vector4 currentPosition, out sclevelgenclass.DVertex[] vertexArray, out int[] triangleArray, out int[] mapper, int padding0_, int padding1_, int padding2_, int numberOfObjectInWidth_, int numberOfObjectInHeight_, int numberOfObjectInDepth_, int numberOfInstancesPerObjectInWidth_, int numberOfInstancesPerObjectInHeight_, int numberOfInstancesPerObjectInDepth_, int tinyChunkWidth_, int tinyChunkHeight_, int tinyChunkDepth_, float planeSize_, sclevelgenclassPrim componentparentprim_, sclevelgenclass componentparentinstance_, sclevelgenclass_instances componentparentthischunk_, int fullface_, int voxeltype_)
        {

            levelofdetail = levelofdetail_;
            chunkoriginpos = new Vector4(currentPosition.X, currentPosition.Y, currentPosition.Z, 1.0f);
            levellimitroofy = LevelGenerator4.wallheightsize;

            typeofterraintiles = typeofterraintiles_;
            //chunkz.Add(this, currentPosition);
            /*voxeltype = voxeltype_;
            fullface = fullface_;

            //componentparentthischunk = componentparentthischunk_;
            //componentparentprim = componentparentprim_;
            //componentparentinstance = componentparentinstance_;

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

            /*xChunkPos = currentPosition.X;
            yChunkPos = currentPosition.Y;
            zChunkPos = currentPosition.Z;*/


            /*
            leftExtremity = new int[width*height*depth];
            rightExtremity = new int[width*height*depth];
            frontExtremity = new int[width*height*depth];
            backExtremity = new int[width*height*depth];

            leftInsideCornerExtremity = new int[width*height*depth];
            rightInsideCornerExtremity = new int[width*height*depth];
            frontInsideCornerExtremity = new int[width*height*depth];
            backInsideCornerExtremity = new int[width*height*depth];

            leftOutsideCornerExtremity = new int[width*height*depth];
            rightOutsideCornerExtremity = new int[width*height*depth];
            frontOutsideCornerExtremity = new int[width*height*depth];
            backOutsideCornerExtremity = new int[width*height*depth];
            */


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





            levelgen = levelgen_;

            width = width_ / levelofdetail;
            height = height_ / levelofdetail;
            depth = depth_ / levelofdetail;

            floorHeight = height;

            xChunkPos = _chunkPos.X;
            yChunkPos = _chunkPos.Y;
            zChunkPos = _chunkPos.Z;

            tinyChunkWidth = width;
            tinyChunkHeight = height;
            tinyChunkDepth = depth;

            planeSize = planeSize_;

            voxeltype = 0;

            /*staticPlaneSize = planeSize;

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
            else if (staticPlaneSize == 0.2f)
            {
                staticPlaneSize = planeSize;
                alternateStaticPlaneSize = planeSize * 10;
            }
            else if (staticPlaneSize == 0.02f)
            {
                staticPlaneSize = planeSize;
                alternateStaticPlaneSize = planeSize * 1000;
            }*/

            chunkPos = _chunkPos;// new Vector3(currentPosition.X, currentPosition.Y, currentPosition.Z);

            //planeSize = planeSize;
            //realplanetwidth = 4;
            //width = tinyChunkWidth;
            //height = tinyChunkHeight;
            //depth = tinyChunkDepth;

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

            vertexlist = new List<sclevelgenclass.DVertex>();

            listOfTriangleIndices = new List<int>();
            //normalslist = new List<Vector3>();
            //colorslist = new List<Vector4>();
            //indexPoslist = new List<Vector4>();
            //textureslist = new List<Vector2>();

            //Console.WriteLine(tinyChunkWidth + " " + width);

            float somemul = 1.0f;

            List<Vector3> listofvecs = new List<Vector3>();

            //LEFT CHUNK
            Vector3 somechunkpos = chunkPos;
            somechunkpos.X -= 1.0f * somemul;
            listofvecs.Add(somechunkpos);

            //RIGHT CHUNK
            somechunkpos = chunkPos;
            somechunkpos.X += 1.0f * somemul;
            listofvecs.Add(somechunkpos);

            //BACK CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Z -= 1.0f * somemul;
            listofvecs.Add(somechunkpos);

            //FRONT CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Z += 1.0f * somemul;
            listofvecs.Add(somechunkpos);

            //BOTTOM CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Y -= 1.0f * somemul;
            listofvecs.Add(somechunkpos);

            //TOP CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Y += 1.0f * somemul;
            listofvecs.Add(somechunkpos);



            for (int i = 0; i < listofvecs.Count; i++)
            {
                listofchunksadjacent[i] = (sclevelgenvert)componentparent.getChunk(listofvecs[i].X, listofvecs[i].Y, listofvecs[i].Z, out arrayindex);
            }


            List<Vector3> listofvecsadjacentfloor = new List<Vector3>();

            //LEFT CHUNK
            somechunkpos = chunkPos;
            somechunkpos.X -= 1.0f * somemul;
            listofvecsadjacentfloor.Add(somechunkpos);

            //RIGHT CHUNK
            somechunkpos = chunkPos;
            somechunkpos.X += 1.0f * somemul;
            listofvecsadjacentfloor.Add(somechunkpos);

            //BACK CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Z -= 1.0f * somemul;
            listofvecsadjacentfloor.Add(somechunkpos);

            //FRONT CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Z += 1.0f * somemul;
            listofvecsadjacentfloor.Add(somechunkpos);

            //BOTTOM CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Y -= 1.0f * somemul;
            listofvecsadjacentfloor.Add(somechunkpos);

            //TOP CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Y += 1.0f * somemul;
            listofvecsadjacentfloor.Add(somechunkpos);


            for (int i = 0; i < listofvecsadjacentfloor.Count; i++)
            {
                listofchunksadjacentfloor[i] = (sclevelgenvert)componentparent.getChunk(listofvecsadjacentfloor[i].X, 0, listofvecsadjacentfloor[i].Z, out arrayindex);
            }




            //realplanetwidth = planeSize * width;

            map = map_;// new int[tinyChunkWidth * tinyChunkHeight * tinyChunkDepth];


            
            sccsSetMap();
            Regenerate(); //currentPosition

            //vertexArray = vertexlist.ToArray();
            //triangleArray = listOfTriangleIndices.ToArray();
            dVertexArray = vertexlist.ToArray();
            indicesArray = listOfTriangleIndices.ToArray();


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

            mapper = map;





            /*
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
            listOfTriangleIndices = null;*/
            
            //map = null;
            
            //listofchunksadjacent = null;
            //listofchunksadjacentfloor = null;

            return map_;
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

            _newVertzCounter = 0;
            vertexlist.Clear();
            listOfTriangleIndices.Clear();



            /*
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
            _testVertexArray5 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];*/



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
























        public void Regenerate()
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


            for (int x = 0; x < tinyChunkWidth; x++)
            {
                xi = x;
                for (int y = 0; y < tinyChunkHeight; y++)
                {
                    yi = y;
                    for (int z = 0; z < tinyChunkDepth; z++)
                    {                                       
                        zi = z;











                        if (IsTransparent(xi - 1, yi, zi))
                        {
                            if (xi != width - 1 && xi != 0 && levelofdetail == 1)
                            {
                                buildTopLeft(xi, yi, zi, 1.0f);
                            }
                            else if (xi == width - 1 && levelofdetail == 1)
                            {
                                buildTopLeft(xi, yi, zi, 1.0f);
                            }
                            else if (xi == 0 && levelofdetail == 1)
                            {
                                //Vector3 somechunkpos = chunkPos;
                                //somechunkpos.X -= 1.0f;
                                //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

                                if (listofchunksadjacent[0] != null )
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
                                    //var someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, 0, somechunkpos.Z);

                                    if (listofchunksadjacentfloor[0] != null)
                                    {
                                        buildTopLeft(xi, yi, zi, 1.0f);
                                    }

                                  


                                    //buildTopLeft(xi, yi, zi, 1.0f);
                                }
                            }
                            else
                            {
                                if (levelofdetail != 1)
                                {
                                    buildTopLeft(xi, yi, zi, 1.0f);
                                }

                            }
                        }



                        if (IsTransparent(xi + 1, yi, zi))
                        {
                            if (xi != width - 1 && xi != 0 && levelofdetail == 1)
                            {
                                buildTopRight(xi, yi, zi, 1.0f);
                            }
                            else if (xi == 0 && levelofdetail == 1)
                            {
                                buildTopRight(xi, yi, zi, 1.0f);
                            }
                            else if (xi == width - 1 && levelofdetail == 1)
                            {

                                //Vector3 somechunkpos = chunkPos;
                                //somechunkpos.X += 1.0f;
                                //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);


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
                                    //var someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, 0, somechunkpos.Z);

                                    if (listofchunksadjacentfloor[1] != null)
                                    {

                                        buildTopRight(xi, yi, zi, 1.0f);
                                    }

                                 
                                    //buildTopRight(xi, yi, zi, 1.0f);
                                }
                            }
                            else
                            {
                                if (levelofdetail != 1)
                                {
                                    buildTopRight(xi, yi, zi, 1.0f);
                                }
                            }
                        }


                        if (IsTransparent(xi, yi - 1, zi))
                        {
                            if (yi != height - 1 && yi != 0 && levelofdetail == 1)
                            {
                                buildBottomFace(xi, yi, zi, 1.0f);
                            }
                            else if (yi == height - 1 && levelofdetail == 1)
                            {
                                buildBottomFace(xi, yi, zi, 1.0f);
                            }
                            else if (yi == 0 && levelofdetail == 1)
                            {

                                //Vector3 somechunkpos = chunkPos;
                                //somechunkpos.Y -= 1.0f;
                                //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

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
                                    //var somechunkpos = chunkPos;
                                    //somechunkpos.Y -= 1.0f;
                                    //var someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, 0, somechunkpos.Z);

                                    if (listofchunksadjacentfloor[4] != null && chunkPos.Y != levellimitfloory)
                                    {

                                        buildBottomFace(xi, yi, zi, 1.0f);
                                    }


                                    //buildBottomFace(xi, yi, zi, 1.0f);
                                }
                            }
                            else
                            {
                                if (levelofdetail != 1)
                                {
                                    buildBottomFace(xi, yi, zi, 1.0f);
                                }
                            }
                        }


                        if (IsTransparent(xi, yi + 1, zi))
                        {
                            if (yi != height - 1 && yi != 0 && levelofdetail == 1)
                            {
                                buildTopFace(xi, yi, zi, 1.0f);
                            }
                            else if (yi == 0 && levelofdetail == 1)
                            {
                                buildTopFace(xi, yi, zi, 1.0f);
                            }
                            else if (yi == height - 1 && levelofdetail == 1)
                            {
                                //buildTopFace(xi, yi, zi, 1.0f);
                                //Vector3 somechunkpos = chunkPos;
                                //somechunkpos.Y += 1.0f;
                                //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);


                                if (listofchunksadjacent[5] != null )
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
                                    //var somechunkpos = chunkPos;
                                    //somechunkpos.Y += (height) * planeSize;
                                    //var someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, 0, somechunkpos.Z);

                                    if (listofchunksadjacentfloor[5] != null && chunkPos.Y != levellimitroofy)
                                    {

                                        buildTopFace(xi, yi, zi, 1.0f);
                                    }


                                 
                                    //buildTopFace(xi, yi, zi, 1.0f);
                                }
                            }
                            else
                            {
                                if (levelofdetail != 1)
                                {
                                    buildTopFace(xi, yi, zi, 1.0f);
                                }
                            }
                        }



                        if (IsTransparent(xi, yi, zi + 1))
                        {

                            if (zi != depth - 1 && zi != 0 && levelofdetail == 1)
                            {
                                buildFrontFace(xi, yi, zi, 1.0f);
                            }
                            else if (zi == 0 && levelofdetail == 1)
                            {
                                buildFrontFace(xi, yi, zi, 1.0f);
                            }
                            else if (zi == depth - 1 && levelofdetail == 1)
                            {

                                //Vector3 somechunkpos = chunkPos;
                                //somechunkpos.Z += 1.0f;
                                //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

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
                                    //var someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, 0, somechunkpos.Z);

                                    if (listofchunksadjacentfloor[3] != null)
                                    {
                                        buildFrontFace(xi, yi, zi, 1.0f);
                                    }

                                }
                            }
                            else
                            {

                                if (levelofdetail != 1)
                                {
                                    buildFrontFace(xi, yi, zi, 1.0f);
                                }
                            }
                        }


                        if (IsTransparent(xi, yi, zi - 1))
                        {
                            if (zi != depth - 1 && zi != 0 &&levelofdetail == 1 )
                            {
                                buildBackFace(xi, yi, zi, 1.0f);
                            }
                            else if (zi == depth - 1&& levelofdetail == 1)
                            {
                                buildBackFace(xi, yi, zi, 1.0f);
                            }
                            else if (zi == 0&& levelofdetail == 1)
                            {

                                //Vector3 somechunkpos = chunkPos;
                                //somechunkpos.Z -= 1.0f;
                                //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

                                if (listofchunksadjacent[2] != null && levelofdetail == 1)
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
                                    //var someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, 0, somechunkpos.Z);

                                    if (listofchunksadjacentfloor[2] != null)
                                    {
                                        buildBackFace(xi, yi, zi, 1.0f);
                                    }


                                   
                                    //buildBackFace(xi, yi, zi, 1.0f);
                                }
                            }
                            else
                            {
                                if (levelofdetail != 1)
                                {
                                    buildBackFace(xi, yi, zi, 1.0f);
                                }
                                //Console.WriteLine("test");
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

            for (int x = 0; x < tinyChunkWidth; x++)
            {
                xi = x;
                for (int y = 0; y < tinyChunkHeight; y++)
                {
                    yi = y;
                    for (int z = 0; z < tinyChunkDepth; z++)
                    {
                        zi = z;











                        if (IsTransparent(xi - 1, yi, zi))
                        {
                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                            var offset1 = back * planeSize;
                            var offset2 = down * planeSize;
                            createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2);
                        }


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
                    if (IsTransparent(xi - 1, yi, zi))
                    {
                        buildTopLeft(xi, yi, zi, 1.0f);
                    }*/
                    



                    //LEFT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                    /*if (IsTransparent(xi - 1, yi, zi))
                    {
                        int someswtcLeft = 0;



                        if (componentparent.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz) != null)
                        {
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz);

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
                            buildTopLeft(xi, yi, zi, 1.0f);
                        }
                        //buildTopLeft();
                    }*/





                    /*
                    Vector3 somechunkpos = chunkPos;
                    somechunkpos.X -= (width) * planeSize;
                    sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);
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
                            //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

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
                                //var someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, 0, somechunkpos.Z);

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
                            //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);


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
                                //var someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, 0, somechunkpos.Z);

                                if (listofchunksadjacentfloor[1] != null)
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
                            buildBottomFace(xi, yi, zi, 1.0f);
                        }
                        else if (yi == 0)
                        {

                            //Vector3 somechunkpos = chunkPos;
                            //somechunkpos.Y -= 1.0f;
                            //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

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
                                //var somechunkpos = chunkPos;
                                //somechunkpos.Y -= 1.0f;
                                //var someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, 0, somechunkpos.Z);

                                if (listofchunksadjacentfloor[4] != null && chunkPos.Y != levellimitfloory)
                                {

                                    buildBottomFace(xi, yi, zi, 1.0f);
                                }
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
                            buildTopFace(xi, yi, zi, 1.0f);
                        }
                        else if (yi == height - 1)
                        {
                            //buildTopFace(xi, yi, zi, 1.0f);
                            //Vector3 somechunkpos = chunkPos;
                            //somechunkpos.Y += 1.0f;
                            //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);


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
                                //var someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, 0, somechunkpos.Z);

                                if (listofchunksadjacentfloor[5] != null && chunkPos.Y != levellimitroofy)
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
                            //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

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
                               //var someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, 0, somechunkpos.Z);

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
                            //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

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
                                //var someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, 0, somechunkpos.Z);

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
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);
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

                        if (componentparent.getChunk(somechunkpos.X, 0, somechunkpos.Z) != null)
                        {
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X,0, somechunkpos.Z);

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

                        if (componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z) != null)
                        {
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

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

                        if (componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z) != null)
                        {
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

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
                                    //if (componentparent.getChunk(somevalueforRightx, somevalueforRighty, somevalueforRightz) == null)
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
                                //if (componentparent.getChunk(somevalueforRightx, somevalueforRighty, somevalueforRightz) == null)
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

                        if (componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z) != null)
                        {
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

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
                                        //if (componentparent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz) == null)
                                        //{
                                        //    someswtcTop = 1;
                                        //}
                                        someswtcFront = 1;
                                    }
                                }
                                else
                                {
                                    //if (componentparent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz) == null)
                                    //{
                                    //    someswtcTop = 1;
                                    //}
                                    someswtcFront = 1;
                                }
                            }
                            else
                            {
                                //if (componentparent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz) == null)
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

                        if (componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z) != null)
                        {
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);
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
                                        //if (componentparent.getChunk(somevalueforBackx, somevalueforBacky, somevalueforBackz) == null)
                                        //{
                                        //    someswtcBack = 1;
                                        //}
                                        someswtcBack = 1;
                                    }
                                }
                                else
                                {
                                    //if (componentparent.getChunk(somevalueforBackx, somevalueforBacky, somevalueforBackz) == null)
                                    //{
                                    //    someswtcBack = 1;
                                    //}
                                    someswtcBack = 1;
                                }
                            }
                            else
                            {
                                //if (componentparent.getChunk(somevalueforBackx, somevalueforBacky, somevalueforBackz) == null)
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

                        if (componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z) != null)
                        {
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

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
                                        //if (componentparent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz) == null)
                                        //{
                                        //    someswtcTop = 1;
                                        //}
                                        someswtcTop = 1;
                                    }
                                }
                                else
                                {
                                    //if (componentparent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz) == null)
                                    //{
                                    //    someswtcTop = 1;
                                    //}
                                    someswtcTop = 1;
                                }

                            }
                            else
                            {
                                //if (componentparent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz) == null)
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

                        if (componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z) != null)
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

                        if (componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z) != null)
                        {
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

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
                                    //if (componentparent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz) == null)
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
                        if (levelgen.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz) != null)
                        {
                            //Vector3 somechunkpos = chunkPos;
                            //somechunkpos.X += (1.0f * width) * planeSize;


                            sclevelgenvert someChunk = (sclevelgenvert)levelgen.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz);
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
                                    //if (componentparent.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz) == null)
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

                        if (componentparent.getChunk(somevalueforBackx, somevalueforBacky, somevalueforBackz) != null)
                        {
                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz);
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
                                    //if (componentparent.getChunk(somevalueforBackx, somevalueforBacky, somevalueforBackz) == null)
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


                        if (componentparent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz) != null)
                        {

                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz);

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
                                    //if (componentparent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz) == null)
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

                        if (componentparent.getChunk(somevalueforRightx, somevalueforRighty, somevalueforRightz) != null)
                        {

                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz);
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
                                    //if (componentparent.getChunk(somevalueforRightx, somevalueforRighty, somevalueforRightz) == null)
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

                        if (componentparent.getChunk(somevalueforFrontx, somevalueforFronty, somevalueforFrontz) != null)
                        {

                            sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunk(somevalueforFrontx, somevalueforFronty, somevalueforFrontz);

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
                                    //if (componentparent.getChunk(somevalueforFrontx, somevalueforFronty, somevalueforFrontz) == null)
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





                    if (getChunkVertexint0(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize * levelofdetail, oneVertIndexY * planeSize * levelofdetail, oneVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint0(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize * levelofdetail, twoVertIndexY * planeSize * levelofdetail, twoVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(0, 1),
                            padding1 = padding1,
                            padding2 = padding2
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint0(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize * levelofdetail, threeVertIndexY * planeSize * levelofdetail, threeVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 0),
                            padding1 = padding1,
                            padding2 = padding2
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint0(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize * levelofdetail, fourVertIndexY * planeSize * levelofdetail, fourVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(0, 0),
                            padding1 = padding1,
                            padding2 = padding2
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint0(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexint0(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexint0(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexint0(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
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


        private void createleftFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {
            //offset1 = back * planeSize;
            //offset2 = down * planeSize;

            //positions[0 + vertzIndex] = start; //(x,y+1,z+1)
            //positions[1 + vertzIndex] = start + offset1;//(x,y+1,z)
            //positions[2 + vertzIndex] = start + offset2; //(x,y,z+1)
            //positions[3 + vertzIndex] = start + offset1 + offset2;//(x,y,z)

            int index0 = vertexlist.Count;// _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index1 = vertexlist.Count;//_newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index2 = vertexlist.Count;//_newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index3 = vertexlist.Count;// _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1 + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            /*normals[0 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[1 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[2 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[3 + vertzIndex] = new Vector3(-1, 1, -1);

            textureCoordinates[0 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[1 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[2 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[3 + vertzIndex] = new Vector2(0f, 0f);*/

            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index0);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index3);
        }


        private void createRightFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {
            //offset1 = back * planeSize;
            //offset2 = down * planeSize;

            //positions[0 + vertzIndex] = start; //(x,y+1,z+1)
            //positions[1 + vertzIndex] = start + offset1;//(x,y+1,z)
            //positions[2 + vertzIndex] = start + offset2; //(x,y,z+1)
            //positions[3 + vertzIndex] = start + offset1 + offset2;//(x,y,z)

            int index0 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index1 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index2 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index3 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1 + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            /*normals[0 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[1 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[2 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[3 + vertzIndex] = new Vector3(-1, 1, -1);

            textureCoordinates[0 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[1 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[2 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[3 + vertzIndex] = new Vector2(0f, 0f);*/

            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index0);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index3);
        }


        private void createTopFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {
            //offset1 = back * planeSize;
            //offset2 = down * planeSize;

            //positions[0 + vertzIndex] = start; //(x,y+1,z+1)
            //positions[1 + vertzIndex] = start + offset1;//(x,y+1,z)
            //positions[2 + vertzIndex] = start + offset2; //(x,y,z+1)
            //positions[3 + vertzIndex] = start + offset1 + offset2;//(x,y,z)

            int index0 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index1 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index2 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index3 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1 + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            /*normals[0 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[1 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[2 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[3 + vertzIndex] = new Vector3(-1, 1, -1);

            textureCoordinates[0 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[1 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[2 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[3 + vertzIndex] = new Vector2(0f, 0f);*/

            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index0);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index3);
        }

        private void createBottomFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {
            //offset1 = back * planeSize;
            //offset2 = down * planeSize;

            //positions[0 + vertzIndex] = start; //(x,y+1,z+1)
            //positions[1 + vertzIndex] = start + offset1;//(x,y+1,z)
            //positions[2 + vertzIndex] = start + offset2; //(x,y,z+1)
            //positions[3 + vertzIndex] = start + offset1 + offset2;//(x,y,z)

            int index0 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index1 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index2 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index3 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1 + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            /*normals[0 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[1 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[2 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[3 + vertzIndex] = new Vector3(-1, 1, -1);

            textureCoordinates[0 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[1 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[2 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[3 + vertzIndex] = new Vector2(0f, 0f);*/

            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index0);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index3);
        }

        private void createFrontFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {
            //offset1 = back * planeSize;
            //offset2 = down * planeSize;

            //positions[0 + vertzIndex] = start; //(x,y+1,z+1)
            //positions[1 + vertzIndex] = start + offset1;//(x,y+1,z)
            //positions[2 + vertzIndex] = start + offset2; //(x,y,z+1)
            //positions[3 + vertzIndex] = start + offset1 + offset2;//(x,y,z)

            int index0 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index1 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index2 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index3 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1 + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            /*normals[0 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[1 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[2 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[3 + vertzIndex] = new Vector3(-1, 1, -1);

            textureCoordinates[0 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[1 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[2 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[3 + vertzIndex] = new Vector2(0f, 0f);*/

            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index0);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index3);
        }



        private void createBackFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {
            //offset1 = back * planeSize;
            //offset2 = down * planeSize;

            //positions[0 + vertzIndex] = start; //(x,y+1,z+1)
            //positions[1 + vertzIndex] = start + offset1;//(x,y+1,z)
            //positions[2 + vertzIndex] = start + offset2; //(x,y,z+1)
            //positions[3 + vertzIndex] = start + offset1 + offset2;//(x,y,z)

            int index0 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index1 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index2 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index3 = _newVertzCounter;
            vertexlist.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1 + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            /*normals[0 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[1 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[2 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[3 + vertzIndex] = new Vector3(-1, 1, -1);

            textureCoordinates[0 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[1 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[2 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[3 + vertzIndex] = new Vector2(0f, 0f);*/
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index0);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index3);
        }


        void buildTopLeft(int xit, int yit, int zit, float block) //int _x, int _y, int _z, Vector3 chunkPos
        {
            //Vector3 somechunkpos = chunkPos;
            //somechunkpos.X -= (1.0f * width) * planeSize;
            //sccstriglevelchunk someChunk = (sccstriglevelchunk)componentparent.getChunk(somechunkpos.X, 0, somechunkpos.Z);


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







                                if (getChunkVertexint1(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                                {
                                    vertexlist.Add(new sclevelgenclass.DVertex()
                                    {
                                        position = new Vector4(oneVertIndexX * planeSize * levelofdetail, oneVertIndexY * planeSize * levelofdetail, oneVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                                        //indexPos = new Vector4(xi, yi, zi, _block),
                                        color = new Vector4(xit, yit, zit, block),
                                        normal = new Vector3(-1, 0, 0),
                                        //padding0 = padding0,
                                        tex = new Vector2(1, 1),
                                        //padding1 = padding1,
                                        //padding2 = padding2,
                                    });

                                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                                    _chunkVertexArray1[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                                    _testVertexArray1[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                                    _newVertzCounter++;
                                }

                                if (getChunkVertexint1(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                                {
                                    vertexlist.Add(new sclevelgenclass.DVertex()
                                    {
                                        position = new Vector4(twoVertIndexX * planeSize * levelofdetail, twoVertIndexY * planeSize * levelofdetail, twoVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                                        //indexPos = new Vector4(xi, yi, zi, _block),
                                        color = new Vector4(xit, yit, zit, block),
                                        normal = new Vector3(-1, 0, 0),
                                        //padding0 = padding0,
                                        tex = new Vector2(0, 1),
                                        //padding1 = padding1,
                                        //padding2 = padding2,
                                    });

                                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                                    _chunkVertexArray1[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                                    _testVertexArray1[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                                    _newVertzCounter++;
                                }

                                if (getChunkVertexint1(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                                {
                                    vertexlist.Add(new sclevelgenclass.DVertex()
                                    {
                                        position = new Vector4(threeVertIndexX * planeSize * levelofdetail, threeVertIndexY * planeSize * levelofdetail, threeVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                                        //indexPos = new Vector4(xi, yi, zi, _block),
                                        color = new Vector4(xit, yit, zit, block),
                                        normal = new Vector3(-1, 0, 0),
                                        //padding0 = padding0,
                                        tex = new Vector2(1, 0),
                                        //padding1 = padding1,
                                        //padding2 = padding2,
                                    });

                                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                                    _chunkVertexArray1[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                                    _testVertexArray1[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                                    _newVertzCounter++;
                                }

                                if (getChunkVertexint1(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                                {
                                    vertexlist.Add(new sclevelgenclass.DVertex()
                                    {
                                        position = new Vector4(fourVertIndexX * planeSize * levelofdetail, fourVertIndexY * planeSize * levelofdetail, fourVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                                        //indexPos = new Vector4(xi, yi, zi, _block),
                                        color = new Vector4(xit, yit, zit, block),
                                        normal = new Vector3(-1, 0, 0),
                                        //padding0 = padding0,
                                        tex = new Vector2(0, 0),
                                        //padding1 = padding1,
                                        //padding2 = padding2,
                                    });

                                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                                    _chunkVertexArray1[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                                    _testVertexArray1[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                                    _newVertzCounter++;
                                }

                                if (getChunkVertexint1(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexint1(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexint1(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexint1(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                                {
                                    _index0 = _testVertexArray1[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                                    _index1 = _testVertexArray1[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                                    _index2 = _testVertexArray1[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                                    _index3 = _testVertexArray1[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];



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
                                    }
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
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

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
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = xi + 1;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = xi + 1;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                                        }
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi + 1;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi + 1;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(xi, rowIterateY, rowIterateZ))
                            {
                                _tempChunkArrayRightFace[(xi) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                                ////////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                            }
                        }
                    }





                    if (getChunkVertexint2(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize * levelofdetail, oneVertIndexY * planeSize * levelofdetail, oneVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(1, 0, 0),
                            //padding0 = padding0,
                            tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                        _chunkVertexArray2[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray2[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint2(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize * levelofdetail, twoVertIndexY * planeSize * levelofdetail, twoVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(1, 0, 0),
                            //padding0 = padding0,
                            tex = new Vector2(0, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                        _chunkVertexArray2[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray2[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint2(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize * levelofdetail, threeVertIndexY * planeSize * levelofdetail, threeVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(1, 0, 0),
                            //padding0 = padding0,
                            tex = new Vector2(1, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray2[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray2[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint2(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize * levelofdetail, fourVertIndexY * planeSize * levelofdetail, fourVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(1, 0, 0),
                            //padding0 = padding0,
                            tex = new Vector2(0, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                        _chunkVertexArray2[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray2[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint2(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexint2(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexint2(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexint2(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                    {
                        _index0 = _testVertexArray2[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray2[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray2[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray2[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];




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
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);

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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);

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
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi + 1;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi + 1;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
                                        }
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi + 1;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);

                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - ziz) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);

                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi + 1;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(rowIterateX, rowIterateY, zi))
                            {
                                _tempChunkArrayFrontFace[(rowIterateX) + width * (rowIterateY + height * (zi))] = 2;
                                //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
                            }
                        }
                    }




                    if (getChunkVertexint3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize * levelofdetail, oneVertIndexY * planeSize * levelofdetail, oneVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 0, 1),
                            //padding0 = padding0,
                            tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                        _chunkVertexArray3[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray3[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize * levelofdetail, twoVertIndexY * planeSize * levelofdetail, twoVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 0, 1),
                            //padding0 = padding0,
                            tex = new Vector2(0, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                        _chunkVertexArray3[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray3[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint3(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize * levelofdetail, threeVertIndexY * planeSize * levelofdetail, threeVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 0, 1),
                            //padding0 = padding0,
                            tex = new Vector2(1, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray3[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray3[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexint3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize * levelofdetail, fourVertIndexY * planeSize * levelofdetail, fourVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 0, 1),
                            //padding0 = padding0,
                            tex = new Vector2(0, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                        _chunkVertexArray3[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray3[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }


                    if (getChunkVertexint3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexint3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexint3(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexint3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                    {
                        _index0 = _testVertexArray3[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray3[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray3[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray3[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];




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
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);

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
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
                                        }
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);

                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - ziz) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);

                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(rowIterateX, rowIterateY, zi))
                            {
                                _tempChunkArrayBackFace[(rowIterateX) + width * (rowIterateY + height * (zi))] = 2;
                                //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize * levelofdetail + _chunkPos, Quaternion.identity);
                            }
                        }
                    }






                    if (getChunkVertexint4(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize * levelofdetail, oneVertIndexY * planeSize * levelofdetail, oneVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 0, -1),
                            //padding0 = padding0,
                            tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                        _chunkVertexArray4[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray4[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint4(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize * levelofdetail, twoVertIndexY * planeSize * levelofdetail, twoVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 0, -1),
                            //padding0 = padding0,
                            tex = new Vector2(0, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                        _chunkVertexArray4[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray4[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexint4(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize * levelofdetail, threeVertIndexY * planeSize * levelofdetail, threeVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 0, -1),
                            //padding0 = padding0,
                            tex = new Vector2(1, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray4[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray4[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }



                    if (getChunkVertexint4(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize * levelofdetail, fourVertIndexY * planeSize * levelofdetail, fourVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, 0, -1),
                            //padding0 = padding0,
                            tex = new Vector2(0, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                        _chunkVertexArray4[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray4[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }


                    if (getChunkVertexint4(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexint4(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexint4(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexint4(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                    {
                        _index0 = _testVertexArray4[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray4[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray4[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray4[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];



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
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

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
                                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                                        }
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                                    ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, yi + 1, rowIterateZ - _zz) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize * levelofdetail + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
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
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(rowIterateX, yi, rowIterateZ))
                            {
                                _tempChunkArrayBottomFace[(rowIterateX) + width * (yi + height * (rowIterateZ))] = 2;
                                //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                            }
                        }
                    }







                    if (getChunkVertexint5(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize * levelofdetail, oneVertIndexY * planeSize * levelofdetail, oneVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, -1, 0),
                            //padding0 = padding0,
                            tex = new Vector2(1, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                        _chunkVertexArray5[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray5[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexint5(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize * levelofdetail, twoVertIndexY * planeSize * levelofdetail, twoVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, -1, 0),
                            //padding0 = padding0,
                            tex = new Vector2(0, 1),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                        _chunkVertexArray5[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray5[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexint5(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize * levelofdetail, threeVertIndexY * planeSize * levelofdetail, threeVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, -1, 0),
                            //padding0 = padding0,
                            tex = new Vector2(1, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray5[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray5[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }



                    if (getChunkVertexint5(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        vertexlist.Add(new sclevelgenclass.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize * levelofdetail, fourVertIndexY * planeSize * levelofdetail, fourVertIndexZ * planeSize * levelofdetail, 1) + chunkoriginpos,
                            //indexPos = new Vector4(xi, yi, zi, _block),
                            color = new Vector4(xit, yit, zit, block),
                            normal = new Vector3(0, -1, 0),
                            //padding0 = padding0,
                            tex = new Vector2(0, 0),
                            //padding1 = padding1,
                            //padding2 = padding2,
                        });

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize * levelofdetail + chunkPos, Quaternion.identity);
                        _chunkVertexArray5[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray5[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }


                    if (getChunkVertexint5(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexint5(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexint5(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexint5(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                    {
                        _index0 = _testVertexArray5[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray5[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray5[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray5[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];




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
                if (componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z);

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
                if (componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z);
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
                if (componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
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
                if (componentparent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
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
                if (componentparent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                /*if (componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z);

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

                if (componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
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

                /*if (componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);

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
                /*if (componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X , (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);

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


                if (componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);

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

                if (componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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

                if (componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                /*if (componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);

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

                if (componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);

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

                if (componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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

                if (componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                /*if (componentparent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z- useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);

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

                if (componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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

                if (componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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

                if (componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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

                if (componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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

                if (componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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



                if (componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
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
                if (componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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

                if (componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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


                if (componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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

                if (componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
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
                if (componentparent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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

                if (componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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

                if (componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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

                if (componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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

                if (componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                if (componentparent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunk((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
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
                return 0;
            }
            return map[x + width * (y + height * z)];
        }


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



        int getChunkVertexint0(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
            {
                return _chunkVertexArray0[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
            }
            return 0;
        }


        int getChunkVertexint1(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
            {
                return _chunkVertexArray1[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
            }
            return 0;
        }




        int getChunkVertexint2(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
            {
                return _chunkVertexArray2[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
            }
            return 0;
        }




        int getChunkVertexint3(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
            {
                return _chunkVertexArray3[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
            }
            return 0;
        }




        int getChunkVertexint4(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
            {
                return _chunkVertexArray4[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
            }
            return 0;
        }




        int getChunkVertexint5(int _x, int _y, int _z)
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
            if ((x < 0) || (y < 0) || (z < 0) || (x >= tinyChunkWidth) || (y >= tinyChunkHeight) || (z >= tinyChunkDepth)) return true;
            {
                return map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] == 0;
            }
        }*/
    }
}

