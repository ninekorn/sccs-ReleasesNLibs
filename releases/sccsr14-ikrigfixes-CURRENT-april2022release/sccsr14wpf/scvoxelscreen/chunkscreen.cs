using SharpDX;
using System;

namespace sccs
{
    public class chunkscreen
    {
        //int maxveclength = 8; // 4 or 8
        int maxveclength = 8; // 4 or 8


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

        int _block = 0;


        public int[] _chunkArray;
        public int[] _tempChunkArray;
        public int[] _tempChunkArrayRightFace;
        public int[] _tempChunkArrayLeftFace;
        public int[] _tempChunkArrayFrontFace;
        public int[] _tempChunkArrayBackFace;
        public int[] _tempChunkArrayBottomFace;
        public int[] _chunkVertexArray;
        public int[] _testVertexArray;


        FastNoise fastNoise = new FastNoise();


        float staticPlaneSize;
        //float alternateStaticPlaneSize;

        public int[] map;
        private int seed = 3420; // 3420

        private int _detailScale = 10; // 10
        private int _HeightScale = 200; //200

        //public float planeSize;
        public float realplanetwidth;

        public int width = 0;
        public int height = 0;
        public int depth = 0;

        SC_instancedChunk componentParent;
        int swtcsetneighboors;

        int total = 0;
        public Vector4 somechunkpos;

        public chunkscreen()
        {
            width = numberOfInstancesPerObjectInWidth;
            height = numberOfInstancesPerObjectInHeight;
            depth = numberOfInstancesPerObjectInDepth;
        }


        public void setsizetinychunk()
        {
            width = tinyChunkWidth;
            height = tinyChunkHeight;
            depth = tinyChunkDepth;
        }
        public void setsizeinstancechunk()
        {
            width = numberOfInstancesPerObjectInWidth;
            height = numberOfInstancesPerObjectInHeight;
            depth = numberOfInstancesPerObjectInDepth;
        }

        int numberOfObjectInWidth; int numberOfObjectInHeight; int numberOfObjectInDepth; int numberOfInstancesPerObjectInWidth; int numberOfInstancesPerObjectInHeight; int numberOfInstancesPerObjectInDepth; float planeSize;

        int tinyChunkWidth; int tinyChunkHeight; int tinyChunkDepth;

        int somechunkkeyboardpriminstanceindex;
        int chunkprimindex;
        int chunkinstindex;

        int voxeltype;

        int typeofbytemapobject;
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
            typeofbytemapobject = typeofbytemapobject_;


            //somechunkkeyboardpriminstanceindex = somechunkkeyboardpriminstanceindex_;
            //chunkprimindex = chunkprimindex_;
            //chunkinstindex = chunkinstindex_;

            maxveclength = tinyChunkWidth_; //tinyChunkWidth_

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

            somechunkpos = currentPosition;

            swtcsetneighboors = swtcsetneighboors_;

            if (swtcsetneighboors_ == -1)
            {
                width = numberOfInstancesPerObjectInWidth;
                height = numberOfInstancesPerObjectInHeight;
                depth = numberOfInstancesPerObjectInDepth;
            }
            else // if(swtcsetneighboors_ == -)
            {
                width = tinyChunkWidth;
                height = tinyChunkHeight;
                depth = tinyChunkDepth;
            }

            componentParent = componentParent_;



            staticPlaneSize = planeSize;

            /*if (staticPlaneSize == 1)
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

            //float staticPlaneSize =planeSize; //
            //float alternateStaticPlaneSize =planeSize * 10;

            _detailScale = 10;
            _HeightScale = 200;



            realplanetwidth = 4;
            realplanetwidth = planeSize * width;





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



            int chunkwidthl = (width / 2);
            int chunkwidthr = (width / 2) - 1;

            int chunkheightl = (height / 2);
            int chunkheightr = (height / 2) - 1;

            int chunkdepthl = (depth / 2);
            int chunkdepthr = (depth / 2) - 1;


            var sometotal = (chunkwidthl + chunkwidthr + 1) * (chunkheightl + chunkheightr + 1) * (chunkdepthl + chunkdepthr + 1);

            int posx = 0;
            int posy = 0;
            int posz = 0;
            int xx = -chunkwidthl;
            int yy = -chunkheightl;
            int zz = -chunkdepthl;
            int swtchx = 0;
            int swtchy = 0;
            int swtchz = 0;



            for (int i = 0; i < sometotal; i++)
            {
                //if (t0 < sometotal)
                {
                    posx = (xx);
                    posy = (yy);
                    posz = (zz);

                    var planetchunkpos = new Vector3(posx * realplanetwidth, posy * realplanetwidth, posz * realplanetwidth);
                    //planetchunkpos = new Vector3(posx0, posy0, posz0);

                    int xi = xx;
                    int yi = yy;
                    int zi = zz;

                    if (xi < 0)
                    {
                        xi *= -1;
                        xi = (chunkwidthr) + xi;
                    }
                    if (yi < 0)
                    {
                        yi *= -1;
                        yi = (chunkheightr) + yi;
                    }
                    if (zi < 0)
                    {
                        zi *= -1;
                        zi = (chunkdepthr) + zi;
                    }

                    var someindexmain = xi + (chunkwidthl + chunkwidthr + 1) * (yi + (chunkheightl + chunkheightr + 1) * zi);

                    if (someindexmain < sometotal)
                    {


                    }
                    else
                    {
                        ////t = sometotal;
                        //taskcancelFlagTwo = 1;
                    }

                    zz++;
                    if (zz >= (chunkdepthr))
                    {
                        xx++;
                        zz = -chunkdepthl;
                        swtchx = 1;
                    }
                    if (xx >= (chunkwidthr) && swtchx == 1)
                    {
                        yy++;
                        xx = -chunkwidthl;
                        swtchx = 0;
                        swtchy = 1;
                    }
                    if (yy >= (chunkheightr) && swtchy == 1)
                    {
                        //yy = -ChunkHeight_L;
                        swtchy = 0;
                        swtchx = 0;
                        swtchz = 1;
                    }
                    //t++;
                }
            }












            if (swtcsetneighboors == 0 || swtcsetneighboors == -1 || swtcsetneighboors == 1 || swtcsetneighboors == -3)
            {

                map = new int[width * height * depth];



                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int z = 0; z < depth; z++)
                        {

                            if (swtcsetneighboors == 0)
                            {
                                map[x + width * (y + height * z)] = 0;
                            }
                            else if (swtcsetneighboors == -1)
                            {
                                /*FastNoise fastNoise = new FastNoise();
                                float noiseXZ = 20;

                                noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (currentPosition.X * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale, (((y * staticPlaneSize) + (currentPosition.Y * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale, (((z * staticPlaneSize) + (currentPosition.Z * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale);

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
                                }*/
                                map[x + width * (y + height * z)] = 1;


                            }
                            else if (swtcsetneighboors == 1)
                            {
                                if (typeofbytemapobject == 2)
                                {
                                    if (x == tinyChunkWidth - 1 && mainix == numberofmainobjectx - 1 && meshzeroix == numberOfObjectInWidth - 1 && instix == numberOfInstancesPerObjectInWidth - 1)
                                    {
                                        map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                                    }
                                    if (x == 0 && mainix == 0 && meshzeroix == 0 && instix == 0)
                                    {
                                        map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                                    }


                                    if (y == tinyChunkHeight - 1 && mainiy == numberofmainobjecty - 1 && meshzeroiy == numberOfObjectInHeight - 1 && instiy == numberOfInstancesPerObjectInHeight - 1)
                                    {
                                        map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                                    }
                                    if (y == 0 && mainiy == 0 && meshzeroiy == 0 && instiy == 0)
                                    {
                                        map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                                    }

                                    if (z == tinyChunkDepth - 1 && mainiz == numberofmainobjectz - 1 && meshzeroiz == numberOfObjectInDepth - 1 && instiz == numberOfInstancesPerObjectInDepth - 1)
                                    {
                                        map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                                    }
                                    if (z == 0 && mainiz == 0 && meshzeroiz == 0 && instiz == 0)
                                    {
                                        map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                                    }


                                    /*if (y == tinyChunkHeight - 1)
                                    {
                                        map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                                    }

                                    if (z == tinyChunkDepth - 1)
                                    {
                                        map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                                    }

                                    if (x == 0)
                                    {
                                        map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                                    }
                                    if (y == 0)
                                    {
                                        map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                                    }
                                    if (z == 0)
                                    {
                                        map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                                    }*/


                                    //map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                                }
                                else
                                {
                                    map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                                }
                            }
                            else if (swtcsetneighboors == -3)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                        }
                    }
                }
            }


















            /*if (swtcsetneighboors == 2)
            {
                //map = someoriginalmap;
                sccsSetMap();
                Regenerate(new Vector3(currentPosition.X, currentPosition.Y, currentPosition.Z));
                //map = _tempChunkArray;
            }*/

            /*int DarrayOfDeVectorMapTempX = 111111111;// 1; //111111111
            int DarrayOfDeVectorMapTempY = 111111111;//  1;

            int DarrayOfDeVectorMapTempZ = 111111111;//  1;
            int DarrayOfDeVectorMapTempW = 111111111;// 1;

            int DarrayOfDeVectorMapTempTwoX = 111111111;//  1;
            int DarrayOfDeVectorMapTempTwoY = 111111111;//  1;

            int DarrayOfDeVectorMapTempTwoZ = 111111111;// 1;
            int DarrayOfDeVectorMapTempTwoW = 111111111;// 1;*/

            /*float DarrayOfDeVectorMapTempX = 111111111111111111111111111111111111111.111111111111111111111111111111111111111f;// 1; //111111111
            float DarrayOfDeVectorMapTempY = 111111111111111111111111111111111111111.111111111111111111111111111111111111111f;//  1;

            float DarrayOfDeVectorMapTempZ = 111111111111111111111111111111111111111.111111111111111111111111111111111111111f;//  1;
            float DarrayOfDeVectorMapTempW = 111111111111111111111111111111111111111.111111111111111111111111111111111111111f;// 1;

            float DarrayOfDeVectorMapTempTwoX = 111111111111111111111111111111111111111.111111111111111111111111111111111111111f;//  1;
            float DarrayOfDeVectorMapTempTwoY = 111111111111111111111111111111111111111.111111111111111111111111111111111111111f;//  1;

            float DarrayOfDeVectorMapTempTwoZ = 111111111111111111111111111111111111111.111111111111111111111111111111111111111f;// 1;
            float DarrayOfDeVectorMapTempTwoW = 111111111111111111111111111111111111111.111111111111111111111111111111111111111f;// 1;*/

            /*float DarrayOfDeVectorMapTempX = 100000000000000000000000000000000000000.000000000000000000000000000000000000001f;// 1; //111111111
            float DarrayOfDeVectorMapTempY = 100000000000000000000000000000000000000.000000000000000000000000000000000000001f;//  1;

            float DarrayOfDeVectorMapTempZ = 100000000000000000000000000000000000000.000000000000000000000000000000000000001f;//  1;
            float DarrayOfDeVectorMapTempW = 100000000000000000000000000000000000000.000000000000000000000000000000000000001f;// 1;

            float DarrayOfDeVectorMapTempTwoX = 100000000000000000000000000000000000000.000000000000000000000000000000000000001f;//  1;
            float DarrayOfDeVectorMapTempTwoY = 100000000000000000000000000000000000000.000000000000000000000000000000000000001f;//  1;

            float DarrayOfDeVectorMapTempTwoZ = 100000000000000000000000000000000000000.000000000000000000000000000000000000001f;// 1;
            float DarrayOfDeVectorMapTempTwoW = 100000000000000000000000000000000000000.000000000000000000000000000000000000001f;// 1;*/

            //if trying to put the index of the chunk instance and mesh origin in the decimal of the float, i'd first have to get the number of digits that the integer variable below are containing, and then divide or multiply by 0.1f until it is a complete decimal.
            //somechunkkeyboardpriminstanceindex = somechunkkeyboardpriminstanceindex_;
            //chunkprimindex = chunkprimindex_;
            //chunkinstindex = chunkinstindex_;
            //if trying to put the index of the chunk instance and mesh origin in the decimal of the float, i'd first have to get the number of digits that the integer variable above are containing, and then divide or multiply by 0.1f until it is a complete decimal.

            /*int DarrayOfDeVectorMapTempX = 111;// 1; //111111111
            int DarrayOfDeVectorMapTempY = 111;//  1;

            int DarrayOfDeVectorMapTempZ = 111;//  1;
            int DarrayOfDeVectorMapTempW = 111;// 1;

            int DarrayOfDeVectorMapTempTwoX = 111;//  1;
            int DarrayOfDeVectorMapTempTwoY = 111;//  1;

            int DarrayOfDeVectorMapTempTwoZ = 111;// 1;
            int DarrayOfDeVectorMapTempTwoW = 111;// 1;*/

            /*int DarrayOfDeVectorMapTempX = 111111111;// 1; //111111111111111111111111111
            int DarrayOfDeVectorMapTempY = 111111111;//  1;

            int DarrayOfDeVectorMapTempZ = 111111111;//  1;
            int DarrayOfDeVectorMapTempW = 111111111;// 1;

            int DarrayOfDeVectorMapTempTwoX = 111111111;//  1;
            int DarrayOfDeVectorMapTempTwoY = 111111111;//  1;

            int DarrayOfDeVectorMapTempTwoZ = 111111111;// 1;
            int DarrayOfDeVectorMapTempTwoW = 111111111;// 1;
            */

            //float DarrayOfDeVectorMapTempX = 5111111111111111111111111111111115.5111111111111111111111111111111115f;// 1; //111111111111111111111111111
            //float DarrayOfDeVectorMapTempY = 5111111111111111111111111111111115.5111111111111111111111111111111115f;//  1; 

            //float DarrayOfDeVectorMapTempZ = 5111111111111111111111111111111115.5111111111111111111111111111111115f;//  1;
            //float DarrayOfDeVectorMapTempW = 5111111111111111111111111111111115.5111111111111111111111111111111115f;// 1;

            //float DarrayOfDeVectorMapTempTwoX = 5111111111111111111111111111111115.5111111111111111111111111111111115f;//  1;
            //float DarrayOfDeVectorMapTempTwoY = 5111111111111111111111111111111115.5111111111111111111111111111111115f;//  1;

            //float DarrayOfDeVectorMapTempTwoZ = 5111111111111111111111111111111115.5111111111111111111111111111111115f;// 1;
            //float DarrayOfDeVectorMapTempTwoW = 5111111111111111111111111111111115.5111111111111111111111111111111115f;// 1;


            /*float DarrayOfDeVectorMapTempX = 5111111111111111111111111111111110.2111111111111111111111111111111115f;// 1; //111111111111111111111111111
            float DarrayOfDeVectorMapTempY = 5111111111111111111111111111111113.4111111111111111111111111111111115f;//  1;

            float DarrayOfDeVectorMapTempZ = 5111111111111111111111111111111115.6111111111111111111111111111111115f;//  1;
            float DarrayOfDeVectorMapTempW = 5111111111111111111111111111111117.8111111111111111111111111111111115f;// 1;

            float DarrayOfDeVectorMapTempTwoX = 5111111111111111111111111111111119.1011111111111111111111111111111115f;//  1;
            float DarrayOfDeVectorMapTempTwoY = 5111111111111111111111111111111122.3311111111111111111111111111111115f;//  1;

            float DarrayOfDeVectorMapTempTwoZ = 5111111111111111111111111111111144.5511111111111111111111111111111115f;// 1;
            float DarrayOfDeVectorMapTempTwoW = 5111111111111111111111111111111166.7711111111111111111111111111111115f;// 1;
            */

            /*
            float DarrayOfDeVectorMapTempX = 521098764321098765432109876543210.0123456789012345678901234567890125f;// 1; //111111111111111111111111111
            float DarrayOfDeVectorMapTempY = 521098764321098765432109876543210.0123456789012345678901234567890125f;//  1;

            float DarrayOfDeVectorMapTempZ = 521098764321098765432109876543210.0123456789012345678901234567890125f;//  1;
            float DarrayOfDeVectorMapTempW = 521098764321098765432109876543210.0123456789012345678901234567890125f;// 1;

            float DarrayOfDeVectorMapTempTwoX = 521098764321098765432109876543210.0123456789012345678901234567890125f;//  1;
            float DarrayOfDeVectorMapTempTwoY = 521098764321098765432109876543210.0123456789012345678901234567890125f;//  1;

            float DarrayOfDeVectorMapTempTwoZ = 521098764321098765432109876543210.0123456789012345678901234567890125f;// 1;
            float DarrayOfDeVectorMapTempTwoW = 521098764321098765432109876543210.0123456789012345678901234567890125f;// 1;
            */

            //16 loops of 2... or 6 loops of 10 and 2 loops of 3.

            //012345678998765432100123456789
            //float DarrayOfDeVectorMapTempX = 5 210 987643210 9876543210 9876543210 .0123456789 01234567890 123456789 012 5f;// 1; //111111111111111111111111111

            //16 loops of 2 per float4/vector4 or //... or 6 loops of 10 and 2 loops of 3.
            //5 210 987643210 9876543210 9876543210 . 0123456789 01234567890 123456789 012 5f
            //5 210 987643210 9876543210 9876543210 . 0123456789 01234567890 123456789 012 5f

            //5 210 987643210 9876543210 9876543210 . 0123456789 01234567890 123456789 012 5f
            //5 210 987643210 9876543210 9876543210 . 0123456789 01234567890 123456789 012 5f

            //5 210 987643210 9876543210 9876543210 . 0123456789 01234567890 123456789 012 5f
            //5 210 987643210 9876543210 9876543210 . 0123456789 01234567890 123456789 012 5f

            //5 210 987643210 9876543210 9876543210 . 0123456789 01234567890 123456789 012 5f
            //5 210 987643210 9876543210 9876543210 . 0123456789 01234567890 123456789 012 5f

            //0-4-1-5-2-6-3-7
            //8-12-9-13-10-14-11-15
            //16-20-17-21-18-22-19-23
            //24-28-25-29-26-30-27-31
            //32-36-33-37-34-38-35-39
            //40-44-41-45-42-46-43-47
            //48-52-49-53-50-54-51-55
            //56-60-57-61-58-62-59-63
            /*
            int someindexcounter = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        //4*1
                        int index = x + (width * (y + (height * z)));
                        int currentByte = map[index];

                        //Console.SetCursorPosition(0 + someindexcounter,11);
                        Console.WriteLine(index + "/");
                        someindexcounter++;
                    }
                }
            }*/

            /*
            float DarrayOfDeVectorMapTempX = 521098764321098765432109876543210.0123456789012345678901234567890125f;//  1;
            float DarrayOfDeVectorMapTempY = 521098764321098765432109876543210.0123456789012345678901234567890125f;//  1;

            float DarrayOfDeVectorMapTempZ = 521098764321098765432109876543210.0123456789012345678901234567890125f;//  1;
            float DarrayOfDeVectorMapTempW = 521098764321098765432109876543210.0123456789012345678901234567890125f;// 1;

            float DarrayOfDeVectorMapTempTwoX = 521098764321098765432109876543210.0123456789012345678901234567890125f;//  1;
            float DarrayOfDeVectorMapTempTwoY = 521098764321098765432109876543210.0123456789012345678901234567890125f;//  1;

            float DarrayOfDeVectorMapTempTwoZ = 521098764321098765432109876543210.0123456789012345678901234567890125f;// 1;
            float DarrayOfDeVectorMapTempTwoW = 521098764321098765432109876543210.0123456789012345678901234567890125f;// 1;*/
            /*
            double arrayofbytemaprowm11 =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12 =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13 =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14 =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21 =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22 =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23 =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24 =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31 =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32 =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33 =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34 =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41 =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42 =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43 =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44 =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            */




            double arrayofbytemaprowm11a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111 
            double arrayofbytemaprowm22a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111






            double arrayofbytemaprowm11b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111




            double arrayofbytemaprowm11c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111





            double arrayofbytemaprowm11d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111





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
                                    arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 10) + 1;
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
                                    arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 10) + 1;
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
                                    arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 10) + 1;
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
                                    arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 10) + 1;
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
                                    arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 10) + 1;
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
                                    arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 10) + 1;
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
                                    arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 10) + 1;
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
                                    arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 10) + 1;
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
                                    arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 10) + 1;
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
                                    arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 10) + 1;
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
                                    arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 10) + 1;
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
                                    arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 10) + 1;
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
                                    arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 10) + 1;
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
                                    arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 10) + 1;
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
                                    arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 10) + 1;
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
                                    arrayofbytemaprowm11b = (arrayofbytemaprowm11b * 10) + 1;
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
                                    arrayofbytemaprowm12b = (arrayofbytemaprowm12b * 10) + 1;
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
                                    arrayofbytemaprowm13b = (arrayofbytemaprowm13b * 10) + 1;
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
                                    arrayofbytemaprowm14b = (arrayofbytemaprowm14b * 10) + 1;
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
                                    arrayofbytemaprowm21b = (arrayofbytemaprowm21b * 10) + 1;
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
                                    arrayofbytemaprowm22b = (arrayofbytemaprowm22b * 10) + 1;
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
                                    arrayofbytemaprowm23b = (arrayofbytemaprowm23b * 10) + 1;
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
                                    arrayofbytemaprowm24b = (arrayofbytemaprowm24b * 10) + 1;
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
                                    arrayofbytemaprowm31b = (arrayofbytemaprowm31b * 10) + 1;
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
                                    arrayofbytemaprowm32b = (arrayofbytemaprowm32b * 10) + 1;
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
                                    arrayofbytemaprowm33b = (arrayofbytemaprowm33b * 10) + 1;
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
                                    arrayofbytemaprowm34b = (arrayofbytemaprowm34b * 10) + 1;
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
                                    arrayofbytemaprowm41b = (arrayofbytemaprowm41b * 10) + 1;
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
                                    arrayofbytemaprowm42b = (arrayofbytemaprowm42b * 10) + 1;
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
                                    arrayofbytemaprowm43b = (arrayofbytemaprowm43b * 10) + 1;
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
                                    arrayofbytemaprowm44b = (arrayofbytemaprowm44b * 10) + 1;
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
                                    arrayofbytemaprowm11c = (arrayofbytemaprowm11c * 10) + 1;
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
                                    arrayofbytemaprowm12c = (arrayofbytemaprowm12c * 10) + 1;
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
                                    arrayofbytemaprowm13c = (arrayofbytemaprowm13c * 10) + 1;
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
                                    arrayofbytemaprowm14c = (arrayofbytemaprowm14c * 10) + 1;
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
                                    arrayofbytemaprowm21c = (arrayofbytemaprowm21c * 10) + 1;
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
                                    arrayofbytemaprowm22c = (arrayofbytemaprowm22c * 10) + 1;
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
                                    arrayofbytemaprowm23c = (arrayofbytemaprowm23c * 10) + 1;
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
                                    arrayofbytemaprowm24c = (arrayofbytemaprowm24c * 10) + 1;
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
                                    arrayofbytemaprowm31c = (arrayofbytemaprowm31c * 10) + 1;
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
                                    arrayofbytemaprowm32c = (arrayofbytemaprowm32c * 10) + 1;
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
                                    arrayofbytemaprowm33c = (arrayofbytemaprowm33c * 10) + 1;
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
                                    arrayofbytemaprowm34c = (arrayofbytemaprowm34c * 10) + 1;
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
                                    arrayofbytemaprowm41c = (arrayofbytemaprowm41c * 10) + 1;
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
                                    arrayofbytemaprowm42c = (arrayofbytemaprowm42c * 10) + 1;
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
                                    arrayofbytemaprowm43c = (arrayofbytemaprowm43c * 10) + 1;
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
                                    arrayofbytemaprowm44c = (arrayofbytemaprowm44c * 10) + 1;
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
                                    arrayofbytemaprowm11d = (arrayofbytemaprowm11d * 10) + 1;
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
                                    arrayofbytemaprowm12d = (arrayofbytemaprowm12d * 10) + 1;
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
                                    arrayofbytemaprowm13d = (arrayofbytemaprowm13d * 10) + 1;
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
                                    arrayofbytemaprowm14d = (arrayofbytemaprowm14d * 10) + 1;
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
                                    arrayofbytemaprowm21d = (arrayofbytemaprowm21d * 10) + 1;
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
                                    arrayofbytemaprowm22d = (arrayofbytemaprowm22d * 10) + 1;
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
                                    arrayofbytemaprowm23d = (arrayofbytemaprowm23d * 10) + 1;
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
                                    arrayofbytemaprowm24d = (arrayofbytemaprowm24d * 10) + 1;
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
                                    arrayofbytemaprowm31d = (arrayofbytemaprowm31d * 10) + 1;
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
                                    arrayofbytemaprowm32d = (arrayofbytemaprowm32d * 10) + 1;
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
                                    arrayofbytemaprowm33d = (arrayofbytemaprowm33d * 10) + 1;
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
                                    arrayofbytemaprowm34d = (arrayofbytemaprowm34d * 10) + 1;
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
                                    arrayofbytemaprowm41d = (arrayofbytemaprowm41d * 10) + 1;
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
                                    arrayofbytemaprowm42d = (arrayofbytemaprowm42d * 10) + 1;
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
                                    arrayofbytemaprowm43d = (arrayofbytemaprowm43d * 10) + 1;
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
                                    arrayofbytemaprowm44d = (arrayofbytemaprowm44d * 10) + 1;
                                }
                                m44dadder++;
                                break;

                        };
                        /*
                        if (currentByte == 0)
                        {
                            selectablevectordouble = (selectablevectordouble * 10);
                        }
                        else
                        {
                            selectablevectordouble = (selectablevectordouble * 10) + 1;
                        }


                        switch (somecountermul)
                        {
                            case 0:
                                arrayofbytemaprowm11a = selectablevectordouble;
                                break;
                            case 1:
                                arrayofbytemaprowm12a = selectablevectordouble;
                                break;
                            case 2:
                                arrayofbytemaprowm13a = selectablevectordouble;
                                break;
                            case 3:
                                arrayofbytemaprowm14a = selectablevectordouble;
                                break;
                            case 4:
                                arrayofbytemaprowm21a = selectablevectordouble;
                                break;
                            case 5:
                                arrayofbytemaprowm22a = selectablevectordouble;
                                break;
                            case 6:
                                arrayofbytemaprowm23a = selectablevectordouble;
                                break;
                            case 7:
                                arrayofbytemaprowm24a = selectablevectordouble;
                                break;
                            case 8:
                                arrayofbytemaprowm31a = selectablevectordouble;
                                break;
                            case 9:
                                arrayofbytemaprowm32a = selectablevectordouble;
                                break;
                            case 10:
                                arrayofbytemaprowm33a = selectablevectordouble;
                                break;
                            case 11:
                                arrayofbytemaprowm34a = selectablevectordouble;
                                break;
                            case 12:
                                arrayofbytemaprowm41a = selectablevectordouble;
                                break;
                            case 13:
                                arrayofbytemaprowm42a = selectablevectordouble;
                                break;
                            case 14:
                                arrayofbytemaprowm43a = selectablevectordouble;
                                break;
                            case 15:
                                arrayofbytemaprowm44a = selectablevectordouble;
                                break;

                        };*/
                    }
                }
            }


            if (someswtc == 1)
            {
                if (m11adder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "" + m11adder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m11adder" + m11adder, "sccsmsg", 0);
                }
                if (m12adder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m12adder" + m12adder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m12adder" + m12adder, "sccsmsg", 0);
                }


                if (m13adder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m13adder" + m12adder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m13adder" + m13adder, "sccsmsg", 0);
                }


                if (m14adder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14adder" + m14adder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m14adder" + m14adder, "sccsmsg", 0);
                }



                if (m21adder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14adder" + m14adder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m21adder" + m21adder, "sccsmsg", 0);
                }

                if (m22adder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14adder" + m14adder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m22adder" + m22adder, "sccsmsg", 0);
                }


                if (m23adder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14adder" + m14adder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m23adder" + m23adder, "sccsmsg", 0);
                }


                if (m24adder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14adder" + m14adder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m24adder" + m24adder, "sccsmsg", 0);
                }

                if (m31adder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14adder" + m14adder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m31adder" + m31adder, "sccsmsg", 0);
                }

                if (m32adder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14adder" + m14adder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m32adder" + m32adder, "sccsmsg", 0);
                }
                if (m33adder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14adder" + m14adder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m33adder" + m33adder, "sccsmsg", 0);
                }

                if (m34adder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14adder" + m14adder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m34adder" + m34adder, "sccsmsg", 0);
                }



                if (m41adder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14adder" + m14adder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m41adder" + m41adder, "sccsmsg", 0);
                }

                if (m42adder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14adder" + m14adder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m42adder" + m42adder, "sccsmsg", 0);
                }
                if (m43adder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14adder" + m14adder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m43adder" + m43adder, "sccsmsg", 0);
                }

                if (m44adder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14adder" + m14adder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m44adder" + m44adder, "sccsmsg", 0);
                }





















                if (m11badder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "" + m11badder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m11badder" + m11badder, "sccsmsg", 0);
                }
                if (m12badder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m12badder" + m12badder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m12badder" + m12badder, "sccsmsg", 0);
                }


                if (m13badder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m13badder" + m12badder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m13badder" + m13badder, "sccsmsg", 0);
                }


                if (m14badder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14badder" + m14badder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m14badder" + m14badder, "sccsmsg", 0);
                }



                if (m21badder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14badder" + m14badder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m21badder" + m21badder, "sccsmsg", 0);
                }

                if (m22badder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14badder" + m14badder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m22badder" + m22badder, "sccsmsg", 0);
                }


                if (m23badder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14badder" + m14badder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m23badder" + m23badder, "sccsmsg", 0);
                }


                if (m24badder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14badder" + m14badder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m24badder" + m24badder, "sccsmsg", 0);
                }

                if (m31badder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14badder" + m14badder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m31badder" + m31badder, "sccsmsg", 0);
                }

                if (m32badder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14badder" + m14badder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m32badder" + m32badder, "sccsmsg", 0);
                }
                if (m33badder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14badder" + m14badder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m33badder" + m33badder, "sccsmsg", 0);
                }

                if (m34badder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14badder" + m14badder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m34badder" + m34badder, "sccsmsg", 0);
                }



                if (m41badder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14badder" + m14badder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m41badder" + m41badder, "sccsmsg", 0);
                }

                if (m42badder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14badder" + m14badder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m42badder" + m42badder, "sccsmsg", 0);
                }
                if (m43badder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14badder" + m14badder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m43badder" + m43badder, "sccsmsg", 0);
                }

                if (m44badder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14badder" + m14badder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m44badder" + m44badder, "sccsmsg", 0);
                }


















                if (m11cadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "" + m11cadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m11cadder" + m11cadder, "sccsmsg", 0);
                }
                if (m12cadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m12cadder" + m12cadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m12cadder" + m12cadder, "sccsmsg", 0);
                }


                if (m13cadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m13cadder" + m12cadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m13cadder" + m13cadder, "sccsmsg", 0);
                }


                if (m14cadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14cadder" + m14cadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m14cadder" + m14cadder, "sccsmsg", 0);
                }



                if (m21cadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14cadder" + m14cadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m21cadder" + m21cadder, "sccsmsg", 0);
                }

                if (m22cadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14cadder" + m14cadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m22cadder" + m22cadder, "sccsmsg", 0);
                }


                if (m23cadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14cadder" + m14cadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m23cadder" + m23cadder, "sccsmsg", 0);
                }


                if (m24cadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14cadder" + m14cadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m24cadder" + m24cadder, "sccsmsg", 0);
                }

                if (m31cadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14cadder" + m14cadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m31cadder" + m31cadder, "sccsmsg", 0);
                }

                if (m32cadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14cadder" + m14cadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m32cadder" + m32cadder, "sccsmsg", 0);
                }
                if (m33cadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14cadder" + m14cadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m33cadder" + m33cadder, "sccsmsg", 0);
                }

                if (m34cadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14cadder" + m14cadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m34cadder" + m34cadder, "sccsmsg", 0);
                }



                if (m41cadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14cadder" + m14cadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m41cadder" + m41cadder, "sccsmsg", 0);
                }

                if (m42cadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14cadder" + m14cadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m42cadder" + m42cadder, "sccsmsg", 0);
                }
                if (m43cadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14cadder" + m14cadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m43cadder" + m43cadder, "sccsmsg", 0);
                }

                if (m44cadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14cadder" + m14cadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m44cadder" + m44cadder, "sccsmsg", 0);
                }








                if (m11dadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "" + m11dadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m11dadder" + m11dadder, "sccsmsg", 0);
                }
                if (m12dadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m12dadder" + m12dadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m12dadder" + m12dadder, "sccsmsg", 0);
                }


                if (m13dadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m13dadder" + m12dadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m13dadder" + m13dadder, "sccsmsg", 0);
                }


                if (m14dadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14dadder" + m14dadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m14dadder" + m14dadder, "sccsmsg", 0);
                }



                if (m21dadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14dadder" + m14dadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m21dadder" + m21dadder, "sccsmsg", 0);
                }

                if (m22dadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14dadder" + m14dadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m22dadder" + m22dadder, "sccsmsg", 0);
                }


                if (m23dadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14dadder" + m14dadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m23dadder" + m23dadder, "sccsmsg", 0);
                }


                if (m24dadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14dadder" + m14dadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m24dadder" + m24dadder, "sccsmsg", 0);
                }

                if (m31dadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14dadder" + m14dadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m31dadder" + m31dadder, "sccsmsg", 0);
                }

                if (m32dadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14dadder" + m14dadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m32dadder" + m32dadder, "sccsmsg", 0);
                }
                if (m33dadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14dadder" + m14dadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m33dadder" + m33dadder, "sccsmsg", 0);
                }

                if (m34dadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14dadder" + m14dadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m34dadder" + m34dadder, "sccsmsg", 0);
                }



                if (m41dadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14dadder" + m14dadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m41dadder" + m41dadder, "sccsmsg", 0);
                }

                if (m42dadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14dadder" + m14dadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m42dadder" + m42dadder, "sccsmsg", 0);
                }
                if (m43dadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14dadder" + m14dadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m43dadder" + m43dadder, "sccsmsg", 0);
                }

                if (m44dadder == maxveclength)
                {
                    //Program.MessageBox((IntPtr)0, "m14dadder" + m14dadder, "sccsmsg", 0);
                }
                else
                {
                   MainWindow.MessageBox((IntPtr)0, "m44dadder" + m44dadder, "sccsmsg", 0);
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


            if (someswtc == 1)
            {
               MainWindow.MessageBox((IntPtr)0, "m11aadder" + arrayofbytemaprowm11a, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m12aadder" + arrayofbytemaprowm12a, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m13aadder" + arrayofbytemaprowm13a, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m14aadder" + arrayofbytemaprowm14a, "sccsmsg", 0);

               MainWindow.MessageBox((IntPtr)0, "m21aadder" + arrayofbytemaprowm21a, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m22aadder" + arrayofbytemaprowm22a, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m23aadder" + arrayofbytemaprowm23a, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m24aadder" + arrayofbytemaprowm24a, "sccsmsg", 0);

               MainWindow.MessageBox((IntPtr)0, "m31aadder" + arrayofbytemaprowm31a, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m32aadder" + arrayofbytemaprowm32a, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m33aadder" + arrayofbytemaprowm33a, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m34aadder" + arrayofbytemaprowm34a, "sccsmsg", 0);

               MainWindow.MessageBox((IntPtr)0, "m41aadder" + arrayofbytemaprowm41a, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m42aadder" + arrayofbytemaprowm42a, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m43aadder" + arrayofbytemaprowm43a, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m44aadder" + arrayofbytemaprowm44a, "sccsmsg", 0);
            }

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


            if (someswtc == 1)
            {
               MainWindow.MessageBox((IntPtr)0, "m11badder" + arrayofbytemaprowm11b, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m12badder" + arrayofbytemaprowm12b, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m13badder" + arrayofbytemaprowm13b, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m14badder" + arrayofbytemaprowm14b, "sccsmsg", 0);

               MainWindow.MessageBox((IntPtr)0, "m21badder" + arrayofbytemaprowm21b, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m22badder" + arrayofbytemaprowm22b, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m23badder" + arrayofbytemaprowm23b, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m24badder" + arrayofbytemaprowm24b, "sccsmsg", 0);

               MainWindow.MessageBox((IntPtr)0, "m31badder" + arrayofbytemaprowm31b, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m32badder" + arrayofbytemaprowm32b, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m33badder" + arrayofbytemaprowm33b, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m34badder" + arrayofbytemaprowm34b, "sccsmsg", 0);

               MainWindow.MessageBox((IntPtr)0, "m41badder" + arrayofbytemaprowm41b, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m42badder" + arrayofbytemaprowm42b, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m43badder" + arrayofbytemaprowm43b, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m44badder" + arrayofbytemaprowm44b, "sccsmsg", 0);
            }

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


            if (someswtc == 1)
            {
               MainWindow.MessageBox((IntPtr)0, "m11cadder" + arrayofbytemaprowm11c, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m12cadder" + arrayofbytemaprowm12c, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m13cadder" + arrayofbytemaprowm13c, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m14cadder" + arrayofbytemaprowm14c, "sccsmsg", 0);

               MainWindow.MessageBox((IntPtr)0, "m21cadder" + arrayofbytemaprowm21c, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m22cadder" + arrayofbytemaprowm22c, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m23cadder" + arrayofbytemaprowm23c, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m24cadder" + arrayofbytemaprowm24c, "sccsmsg", 0);

               MainWindow.MessageBox((IntPtr)0, "m31cadder" + arrayofbytemaprowm31c, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m32cadder" + arrayofbytemaprowm32c, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m33cadder" + arrayofbytemaprowm33c, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m34cadder" + arrayofbytemaprowm34c, "sccsmsg", 0);

               MainWindow.MessageBox((IntPtr)0, "m41cadder" + arrayofbytemaprowm41c, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m42cadder" + arrayofbytemaprowm42c, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m43cadder" + arrayofbytemaprowm43c, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m44cadder" + arrayofbytemaprowm44c, "sccsmsg", 0);

            }

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



            if (someswtc == 1)
            {
               MainWindow.MessageBox((IntPtr)0, "m11dadder" + arrayofbytemaprowm11d, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m12dadder" + arrayofbytemaprowm12d, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m13dadder" + arrayofbytemaprowm13d, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m14dadder" + arrayofbytemaprowm14d, "sccsmsg", 0);

               MainWindow.MessageBox((IntPtr)0, "m21dadder" + arrayofbytemaprowm21d, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m22dadder" + arrayofbytemaprowm22d, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m23dadder" + arrayofbytemaprowm23d, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m24dadder" + arrayofbytemaprowm24d, "sccsmsg", 0);

               MainWindow.MessageBox((IntPtr)0, "m31dadder" + arrayofbytemaprowm31d, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m32dadder" + arrayofbytemaprowm32d, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m33dadder" + arrayofbytemaprowm33d, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m34dadder" + arrayofbytemaprowm34d, "sccsmsg", 0);

               MainWindow.MessageBox((IntPtr)0, "m41dadder" + arrayofbytemaprowm41d, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m42dadder" + arrayofbytemaprowm42d, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m43dadder" + arrayofbytemaprowm43d, "sccsmsg", 0);
               MainWindow.MessageBox((IntPtr)0, "m44dadder" + arrayofbytemaprowm44d, "sccsmsg", 0);
            }











            //0-31.32-63 
            //64-95.96-127
            //128-159.160-191
            //192-223.224-255
            //256-287.288-319
            //320-351.352-383
            //384-415.416-447
            //448-479.480-511

            //add 1 to integer, on the right of the dot.
            //1*0.1f == 0.1f
            //add 1 to integer, on the right of the dot.
            //0.1f*0.1f == 0.01f
            /*
            total = width * height * depth;


            int switchXX = 0;
            int switchYY = 0;


            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        int index = x + (width * (y + (height * z)));
                        //Console.WriteLine("index:" + index);

                        int currentByte = map[index];

                        if (index >= 0 && index <= 3)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm11 = (arrayofbytemaprowm11 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm11 = (arrayofbytemaprowm11 * 10) + 1;
                            }
                        }
                        else if (index >= 4 && index <= 7)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm12 = (arrayofbytemaprowm12 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm12 = (arrayofbytemaprowm12 * 10) + 1;
                            }
                        }
                        else if (index >= 8 && index <= 11)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm13 = (arrayofbytemaprowm13 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm13 = (arrayofbytemaprowm13 * 10) + 1;
                            }
                        }
                        else if (index >= 12 && index <= 15)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm14 = (arrayofbytemaprowm14 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm14 = (arrayofbytemaprowm14 * 10) + 1;
                            }
                        }

                        else if (index >= 16 && index <= 19)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm21 = (arrayofbytemaprowm21 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm21 = (arrayofbytemaprowm21 * 10) + 1;
                            }
                        }
                        else if (index >= 20 && index <= 23)
                        {

                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm22 = (arrayofbytemaprowm22 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm22 = (arrayofbytemaprowm22 * 10) + 1;
                            }

                        }
                        else if (index >= 24 && index <= 27)
                        {

                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm23 = (arrayofbytemaprowm23 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm23 = (arrayofbytemaprowm23 * 10) + 1;
                            }

                        }
                        else if (index >= 28 && index <= 31)
                        {

                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm24 = (arrayofbytemaprowm24 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm24 = (arrayofbytemaprowm24 * 10) + 1;
                            }

                        }
                        else if (index >= 32 && index <= 35)
                        {

                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm31 = (arrayofbytemaprowm31 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm31 = (arrayofbytemaprowm31 * 10) + 1;
                            }

                        }
                        else if (index >= 36 && index <= 39)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm32 = (arrayofbytemaprowm32 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm32 = (arrayofbytemaprowm32 * 10) + 1;
                            }
                        }
                        else if (index >= 40 && index <= 43)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm33 = (arrayofbytemaprowm33 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm33 = (arrayofbytemaprowm33 * 10) + 1;
                            }
                        }
                        else if (index >= 44 && index <= 47)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm34 = (arrayofbytemaprowm34 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm34 = (arrayofbytemaprowm34 * 10) + 1;
                            }
                        }
                        else if (index >= 48 && index <= 51)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm41 = (arrayofbytemaprowm41 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm41 = (arrayofbytemaprowm41 * 10) + 1;
                            }
                        }
                        else if (index >= 52 && index <= 55)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm42 = (arrayofbytemaprowm42 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm42 = (arrayofbytemaprowm42 * 10) + 1;
                            }
                        }
                        else if (index >= 56 && index <= 59)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm43 = (arrayofbytemaprowm43 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm43 = (arrayofbytemaprowm43 * 10) + 1;
                            }
                        }
                        else if (index >= 60 && index <= 63)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm44 = (arrayofbytemaprowm44 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm44 = (arrayofbytemaprowm44 * 10) + 1;
                            }
                        }
                    }
                }
            }


            m11 = arrayofbytemaprowm11;
            m12 = arrayofbytemaprowm12;
            m13 = arrayofbytemaprowm13;
            m14 = arrayofbytemaprowm14;

            m21 = arrayofbytemaprowm21;
            m22 = arrayofbytemaprowm22;
            m23 = arrayofbytemaprowm23;
            m24 = arrayofbytemaprowm24;

            m31 = arrayofbytemaprowm31;
            m32 = arrayofbytemaprowm32;
            m33 = arrayofbytemaprowm33;
            m34 = arrayofbytemaprowm34;

            m41 = arrayofbytemaprowm41;
            m42 = arrayofbytemaprowm42;
            m43 = arrayofbytemaprowm43;
            m44 = arrayofbytemaprowm44;*/

            mapper = map;

            if (swtcsetneighboors != -1)
            {
                //Program.MessageBox((IntPtr)0, "/m11:" + oneInt + "/m12:" + oneIntTwo + "/m13:" + twoInt + "/m14:" + twoIntTwo + "/m21:" + threeInt + "/m22:" + threeIntTwo + "/m23:" + fourInt + "/m24:" + fourIntTwo, "sccs message 1", 0);

            }
        }





        public void resetmap(out double m11, out double m12, out double m13, out double m14, out double m21, out double m22, out double m23, out double m24, out double m31, out double m32, out double m33, out double m34, out double m41, out double m42, out double m43, out double m44, int chosenmap, int voxelchunkinvertoption
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
            float seed = (float)sc_maths.getSomeRandNum(3415, 3425); //3420;


            map = new int[width * height * depth];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        //5 full chunk cube all 1s for byte breaking when 1s becomes 0s (and for byte adding when byte 0s become 1s WIP)
                        //4 full chunk cube all 0s for path tracing with path traced with bytes becoming 1s when the player moves around the invisible chunk.
                        //3 full chunk cube all 0s for a way to visualize spatial location of objects in a 3d scene.
                        //2 full chunk cube all 1s for byte breaking when 1s becomes 0s (and for byte adding when byte 0s become 1s WIP) - using random perlin WIP
                        //1 WIP TRANSPARENCY GRID LIKE CHUNK WITH MY UPCOMING CODING CHALLENGE TO LEARN RASTERTEK C# TRANSPARENCY.

                        if (voxelchunkinvertoption == -1) //5
                        {
                            map[x + width * (y + height * z)] = 1;
                        }
                        else if (voxelchunkinvertoption == 4)
                        {
                            map[x + width * (y + height * z)] = 1;
                        }
                        else if (voxelchunkinvertoption == 3)
                        {
                            map[x + width * (y + height * z)] = 0;
                        }
                        else if (voxelchunkinvertoption == 2)
                        {
                            _detailScale = 7; // 10
                            _HeightScale = 5; //200
                            float noiseXZ = 20;

                            fastNoise = new FastNoise();
                            noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (somechunkpos.X * staticPlaneSize) + seed) / _detailScale) * _HeightScale, (((y * staticPlaneSize) + (somechunkpos.Y * staticPlaneSize) + seed) / _detailScale) * _HeightScale, (((z * staticPlaneSize) + (somechunkpos.Z * staticPlaneSize) + seed) / _detailScale) * _HeightScale);

                            if (noiseXZ >= 0.1f)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                                map[x + width * (y + height * z)] = 0;
                            }
                        }
                        else if (voxelchunkinvertoption == 1)
                        {
                            _detailScale = 20; // 10
                            _HeightScale = 20; //200
                            float noiseXZ = 20;

                            fastNoise = new FastNoise();
                            noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (somechunkpos.X * staticPlaneSize) + seed) / _detailScale) * _HeightScale, (((y * staticPlaneSize) + (somechunkpos.Y * staticPlaneSize) + seed) / _detailScale) * _HeightScale, (((z * staticPlaneSize) + (somechunkpos.Z * staticPlaneSize) + seed) / _detailScale) * _HeightScale);

                            if (noiseXZ >= 0.1f)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                                map[x + width * (y + height * z)] = 0;
                            }
                        }

                        /*
                        if (swtcsetneighboors == 0)
                        {
                            map[x + width * (y + height * z)] = 0;
                        }
                        else if (swtcsetneighboors == -1)
                        {
                            FastNoise fastNoise = new FastNoise();
                            float noiseXZ = 20;

                            noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (somechunkpos.X * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale, (((y * staticPlaneSize) + (somechunkpos.Y * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale, (((z * staticPlaneSize) + (somechunkpos.Z * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale);

                            if (noiseXZ >= 0.1f)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else if (y == 0 && somechunkpos.Y == 0)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                                map[x + width * (y + height * z)] = 0;
                            }
                            //map[x + width * (y + height * z)] = 1;


                        }
                        else if (swtcsetneighboors == 1)
                        {
                            map[x + width * (y + height * z)] = 1;
                        }
                        else if (swtcsetneighboors == -3)
                        {
                            map[x + width * (y + height * z)] = 1;
                        }*/
                    }
                }
            }



            width = tinyChunkWidth;
            height = tinyChunkHeight;
            depth = tinyChunkDepth;



            double arrayofbytemaprowm11a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111 
            double arrayofbytemaprowm22a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111




            double arrayofbytemaprowm11b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111




            double arrayofbytemaprowm11c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111





            double arrayofbytemaprowm11d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111





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


                        /*
                        case 16:
                            selectablevectordouble = arrayofbytemaprowm11b;
                            break;
                        case 17:
                            selectablevectordouble = arrayofbytemaprowm12b;
                            break;
                        case 18:
                            selectablevectordouble = arrayofbytemaprowm13b;
                            break;
                        case 19:
                            selectablevectordouble = arrayofbytemaprowm14b;
                            break;
                        case 20:
                            selectablevectordouble = arrayofbytemaprowm21b;
                            break;
                        case 21:
                            selectablevectordouble = arrayofbytemaprowm22b;
                            break;
                        case 22:
                            selectablevectordouble = arrayofbytemaprowm23b;
                            break;
                        case 23:
                            selectablevectordouble = arrayofbytemaprowm24b;
                            break;
                        case 24:
                            selectablevectordouble = arrayofbytemaprowm31b;
                            break;
                        case 25:
                            selectablevectordouble = arrayofbytemaprowm32b;
                            break;
                        case 26:
                            selectablevectordouble = arrayofbytemaprowm33b;
                            break;
                        case 27:
                            selectablevectordouble = arrayofbytemaprowm34b;
                            break;
                        case 28:
                            selectablevectordouble = arrayofbytemaprowm41b;
                            break;
                        case 29:
                            selectablevectordouble = arrayofbytemaprowm42b;
                            break;
                        case 30:
                            selectablevectordouble = arrayofbytemaprowm43b;
                            break;
                        case 31:
                            selectablevectordouble = arrayofbytemaprowm44b;
                            break;



                        case 32:
                            selectablevectordouble = arrayofbytemaprowm11c;
                            break;
                        case 33:
                            selectablevectordouble = arrayofbytemaprowm12c;
                            break;
                        case 34:
                            selectablevectordouble = arrayofbytemaprowm13c;
                            break;
                        case 35:
                            selectablevectordouble = arrayofbytemaprowm14c;
                            break;
                        case 36:
                            selectablevectordouble = arrayofbytemaprowm21c;
                            break;
                        case 37:
                            selectablevectordouble = arrayofbytemaprowm22c;
                            break;
                        case 38:
                            selectablevectordouble = arrayofbytemaprowm23c;
                            break;
                        case 39:
                            selectablevectordouble = arrayofbytemaprowm24c;
                            break;
                        case 40:
                            selectablevectordouble = arrayofbytemaprowm31c;
                            break;
                        case 41:
                            selectablevectordouble = arrayofbytemaprowm32c;
                            break;
                        case 42:
                            selectablevectordouble = arrayofbytemaprowm33c;
                            break;
                        case 43:
                            selectablevectordouble = arrayofbytemaprowm34c;
                            break;
                        case 44:
                            selectablevectordouble = arrayofbytemaprowm41c;
                            break;
                        case 45:
                            selectablevectordouble = arrayofbytemaprowm42c;
                            break;
                        case 46:
                            selectablevectordouble = arrayofbytemaprowm43c;
                            break;
                        case 47:
                            selectablevectordouble = arrayofbytemaprowm44c;
                            break;




                        case 48:
                            selectablevectordouble = arrayofbytemaprowm11d;
                            break;
                        case 49:
                            selectablevectordouble = arrayofbytemaprowm12d;
                            break;
                        case 50:
                            selectablevectordouble = arrayofbytemaprowm13d;
                            break;
                        case 51:
                            selectablevectordouble = arrayofbytemaprowm14d;
                            break;
                        case 52:
                            selectablevectordouble = arrayofbytemaprowm21d;
                            break;
                        case 53:
                            selectablevectordouble = arrayofbytemaprowm22d;
                            break;
                        case 54:
                            selectablevectordouble = arrayofbytemaprowm23d;
                            break;
                        case 55:
                            selectablevectordouble = arrayofbytemaprowm24d;
                            break;
                        case 56:
                            selectablevectordouble = arrayofbytemaprowm31d;
                            break;
                        case 57:
                            selectablevectordouble = arrayofbytemaprowm32d;
                            break;
                        case 58:
                            selectablevectordouble = arrayofbytemaprowm33d;
                            break;
                        case 59:
                            selectablevectordouble = arrayofbytemaprowm34d;
                            break;
                        case 60:
                            selectablevectordouble = arrayofbytemaprowm41d;
                            break;
                        case 61:
                            selectablevectordouble = arrayofbytemaprowm42d;
                            break;
                        case 62:
                            selectablevectordouble = arrayofbytemaprowm43d;
                            break;
                        case 63:
                            selectablevectordouble = arrayofbytemaprowm44d;
                            break;*/
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



            double arrayofbytemaprowm11a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111 
            double arrayofbytemaprowm22a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44a =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111




            double arrayofbytemaprowm11b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44b =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111




            double arrayofbytemaprowm11c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44c =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111





            double arrayofbytemaprowm11d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44d =MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111





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

                        /*
                        if (currentByte == 0)
                        {
                            selectablevectordouble = (selectablevectordouble * 10);
                        }
                        else
                        {
                            selectablevectordouble = (selectablevectordouble * 10) + 1;
                        }


                        switch (somecountermul)
                        {
                            case 0:
                                arrayofbytemaprowm11a = selectablevectordouble;
                                break;
                            case 1:
                                arrayofbytemaprowm12a = selectablevectordouble;
                                break;
                            case 2:
                                arrayofbytemaprowm13a = selectablevectordouble;
                                break;
                            case 3:
                                arrayofbytemaprowm14a = selectablevectordouble;
                                break;
                            case 4:
                                arrayofbytemaprowm21a = selectablevectordouble;
                                break;
                            case 5:
                                arrayofbytemaprowm22a = selectablevectordouble;
                                break;
                            case 6:
                                arrayofbytemaprowm23a = selectablevectordouble;
                                break;
                            case 7:
                                arrayofbytemaprowm24a = selectablevectordouble;
                                break;
                            case 8:
                                arrayofbytemaprowm31a = selectablevectordouble;
                                break;
                            case 9:
                                arrayofbytemaprowm32a = selectablevectordouble;
                                break;
                            case 10:
                                arrayofbytemaprowm33a = selectablevectordouble;
                                break;
                            case 11:
                                arrayofbytemaprowm34a = selectablevectordouble;
                                break;
                            case 12:
                                arrayofbytemaprowm41a = selectablevectordouble;
                                break;
                            case 13:
                                arrayofbytemaprowm42a = selectablevectordouble;
                                break;
                            case 14:
                                arrayofbytemaprowm43a = selectablevectordouble;
                                break;
                            case 15:
                                arrayofbytemaprowm44a = selectablevectordouble;
                                break;

                        };*/
                    }
                }
            }










            /*
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        int index = x + (width * (y + (height * z)));
                        //Console.WriteLine("index:" + index);

                        int currentByte = map[index];

                        if (index >= 0 && index <= 3 && chosenmap == 0)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm11 = (arrayofbytemaprowm11 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm11 = (arrayofbytemaprowm11 * 10) + 1;
                            }

                        }
                        else if (index >= 4 && index <= 7 && chosenmap == 1)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm12 = (arrayofbytemaprowm12 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm12 = (arrayofbytemaprowm12 * 10) + 1;
                            }
                        }
                        else if (index >= 8 && index <= 11 && chosenmap == 2)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm13 = (arrayofbytemaprowm13 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm13 = (arrayofbytemaprowm13 * 10) + 1;
                            }
                        }
                        else if (index >= 12 && index <= 15 && chosenmap == 3)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm14 = (arrayofbytemaprowm14 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm14 = (arrayofbytemaprowm14 * 10) + 1;
                            }
                        }

                        else if (index >= 16 && index <= 19 && chosenmap == 4)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm21 = (arrayofbytemaprowm21 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm21 = (arrayofbytemaprowm21 * 10) + 1;
                            }
                        }
                        else if (index >= 20 && index <= 23 && chosenmap == 5)
                        {

                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm22 = (arrayofbytemaprowm22 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm22 = (arrayofbytemaprowm22 * 10) + 1;
                            }

                        }
                        else if (index >= 24 && index <= 27 && chosenmap == 6)
                        {

                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm23 = (arrayofbytemaprowm23 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm23 = (arrayofbytemaprowm23 * 10) + 1;
                            }

                        }
                        else if (index >= 28 && index <= 31 && chosenmap == 7)
                        {

                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm24 = (arrayofbytemaprowm24 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm24 = (arrayofbytemaprowm24 * 10) + 1;
                            }

                        }
                        else if (index >= 32 && index <= 35 && chosenmap == 8)
                        {

                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm31 = (arrayofbytemaprowm31 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm31 = (arrayofbytemaprowm31 * 10) + 1;
                            }

                        }
                        else if (index >= 36 && index <= 39 && chosenmap == 9)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm32 = (arrayofbytemaprowm32 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm32 = (arrayofbytemaprowm32 * 10) + 1;
                            }
                        }
                        else if (index >= 40 && index <= 43 && chosenmap == 10)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm33 = (arrayofbytemaprowm33 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm33 = (arrayofbytemaprowm33 * 10) + 1;
                            }
                        }
                        else if (index >= 44 && index <= 47 && chosenmap == 11)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm34 = (arrayofbytemaprowm34 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm34 = (arrayofbytemaprowm34 * 10) + 1;
                            }
                        }
                        else if (index >= 48 && index <= 51 && chosenmap == 12)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm41 = (arrayofbytemaprowm41 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm41 = (arrayofbytemaprowm41 * 10) + 1;
                            }
                        }
                        else if (index >= 52 && index <= 55 && chosenmap == 13)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm42 = (arrayofbytemaprowm42 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm42 = (arrayofbytemaprowm42 * 10) + 1;
                            }
                        }
                        else if (index >= 56 && index <= 59 && chosenmap == 14)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm43 = (arrayofbytemaprowm43 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm43 = (arrayofbytemaprowm43 * 10) + 1;
                            }
                        }
                        else if (index >= 60 && index <= 63 && chosenmap == 15)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm44 = (arrayofbytemaprowm44 * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm44 = (arrayofbytemaprowm44 * 10) + 1;
                            }
                        }
                    }
                }
            }*/





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




        public void Regenerate(Vector3 chunkpos)
        {
            //vertexlist.Clear();
            //listOfTriangleIndices.Clear();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        var index = x + (width) * (y + (height) * z);

                        var block = map[index];

                        if (block == 1)
                        {

                        }

                        setneighboors(x, y, z, chunkpos);
                    }
                }
            }
        }



        public bool somechunkIsTransparent(int _x, int _y, int _z, int[] somemap)
        {
            if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= SC_Globals.numberOfInstancesPerObjectInWidth) || (_y >= SC_Globals.numberOfInstancesPerObjectInHeight) || (_z >= SC_Globals.numberOfInstancesPerObjectInDepth)) return true;
            return somemap[_x + SC_Globals.numberOfInstancesPerObjectInWidth * (_y + SC_Globals.numberOfInstancesPerObjectInHeight * _z)] == 0; //_chunkArray
        }




        public bool IsTransparent(int _x, int _y, int _z)
        {
            if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= width) || (_y >= height) || (_z >= depth)) return true;
            return map[_x + width * (_y + height * _z)] == 0; //_chunkArray
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

            //vertexlist.Clear();
            //listOfTriangleIndices.Clear();

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


        public void setneighboors(int xi, int yi, int zi, Vector3 chunkPos)
        {




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
                            if (somechunkIsTransparent(xi, height - 1, zi, someChunk.map))
                            {
                                var index = xi + (width) * (yi + (height) * zi);
                                map[index] = 0;

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
                    //buildBottomFace(xi, yi, zi);
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
                            if (somechunkIsTransparent(width - 1, yi, zi, someChunk.map))
                            {
                                var index = xi + (width) * (yi + (height) * zi);
                                map[index] = 0;
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
                    //buildTopLeft(xi, yi, zi);
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
                            if (somechunkIsTransparent(xi, yi, depth - 1, someChunk.map))
                            {
                                var index = xi + (width) * (yi + (height) * zi);
                                map[index] = 0;
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
                    //buildBackFace(xi, yi, zi);
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
                            if (somechunkIsTransparent(xi, 0, zi, someChunk.map))
                            {
                                var index = xi + (width) * (yi + (height) * zi);
                                map[index] = 0;
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
                    //buildTopFace(xi, yi, zi);
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
                            if (somechunkIsTransparent(0, yi, zi, someChunk.map))
                            {
                                var index = xi + (width) * (yi + (height) * zi);
                                map[index] = 0;
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
                    //buildTopRight(xi, yi, zi);
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
                            if (somechunkIsTransparent(xi, yi, 0, someChunk.map))
                            {
                                var index = xi + (width) * (yi + (height) * zi);
                                map[index] = 0;
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
                    //buildFrontFace(xi, yi, zi);
                }
                //buildFrontFace();
            }
        }










        //UnityEngine.Debug.Log("_xx: " + _xx + " _zz: " + _zz + " _maxWidth: " + _maxWidth + " _maxDepth: " + _maxDepth + " rowIterateX: " + rowIterateX + " rowIterateZ: " + rowIterateZ);
        void buildTopFace(int xi, int yi, int zi) //int _x, int _y, int _z, Vector3 chunkPos
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





                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            tex = new Vector2(1, 1),
                        });*/

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            tex = new Vector2(0, 1),
                        });*/

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            tex = new Vector2(1, 0),
                        });*/

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, 0),
                            tex = new Vector2(0, 0),
                        });*/

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

                        /*listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);*/
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








                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, -1),
                            tex = new Vector2(0, 0),
                        });*/

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {

                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, -1),
                            tex = new Vector2(0, 1),
                        });*/

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, -1),
                            tex = new Vector2(1, 0),
                        });*/

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                    }
                    if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 1, -1),
                            tex = new Vector2(1, 1),
                        });*/

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

                        /*listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);*/
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






                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 0, -1),
                            tex = new Vector2(1, 1),
                        });*/

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {

                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 0, -1),
                            tex = new Vector2(1, 0),
                        });*/

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 0, -1),
                            tex = new Vector2(0, 1),
                        });*/

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                    }
                    if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 0, -1),
                            tex = new Vector2(0, 0),
                        });*/

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

                        /*listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);*/
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





                    //looping in x than y
                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize, 1),

                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 0, 0),
                            tex = new Vector2(0, 1),
                        });*/

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }

                    if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 0, 0),
                            tex = new Vector2(1, 1),
                        });*/

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }




                    if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 0, 0),
                            tex = new Vector2(1, 0),
                        });*/

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(-1, 0, 0),
                            tex = new Vector2(0, 0),
                        });*/

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

                        /*listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);*/
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


                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(0, 0, -1),
                            tex = new Vector2(1, 1),
                        });*/

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {

                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(0, 0, -1),
                            tex = new Vector2(0, 1),
                        });*/

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(0, 0, -1),
                            tex = new Vector2(1, 0),
                        });*/

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                    }
                    if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(0, 0, -1),
                            tex = new Vector2(0, 0),
                        });*/

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


                        /*listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);*/
                    }
                }
            }
        }




        void buildBottomFace(int xi, int yi, int zi) //int _x, int _y, int _z, Vector3 chunkPos
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







                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(0, 1, -1),
                            tex = new Vector2(0, 0),
                        });*/

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                        _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {

                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(0, 1, -1),
                            tex = new Vector2(0, 1),
                        });*/

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                        _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }
                    if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(0, 1, -1),
                            tex = new Vector2(1, 0),
                        });*/

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                        _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                    }
                    if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        /*vertexlist.Add(new SC_instancedChunk.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize, 1),
                            indexPos = new Vector4(xi, yi, zi, _block),
                            color = chunkcolor,
                            normal = new Vector3(0, 1, -1),
                            tex = new Vector2(1, 1),
                        });*/

                        ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                        _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                        _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;
                    }


                    if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                    {
                        /*_index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                        listOfTriangleIndices.Add(_index0);
                        listOfTriangleIndices.Add(_index1);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index3);
                        listOfTriangleIndices.Add(_index2);
                        listOfTriangleIndices.Add(_index1);*/
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

        int _index0;// = 0;
        int _index1;// = 0;
        int _index2;// = 0;
        int _index3;// = 0;


        int totalBytes;
        int vertexlistWidth;
        int vertexlistHeight;
        int vertexlistDepth;

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
    }
}