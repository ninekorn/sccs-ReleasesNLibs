using System;
using SharpDX;

namespace SCCoreSystems
{
    public class chunk
    {


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

        byte _block = 0;


        //FastNoise fastNoise = new FastNoise();


        float staticPlaneSize;
        //float alternateStaticPlaneSize;

        public byte[] map;
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

        public chunk()
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
            out double m21, out double m22, out double m23, out double m24, out double m31, out double m32, out double m33, out double m34, out double m41, out double m42, out double m43, out double m44, out byte[] mapper, SC_instancedChunk componentParent_, int swtcsetneighboors_, byte[] someoriginalmap, int numberofmainobjectx, int numberofmainobjecty, int numberofmainobjectz, int numberOfObjectInWidth_, int numberOfObjectInHeight_, int numberOfObjectInDepth_, int numberOfInstancesPerObjectInWidth_, int numberOfInstancesPerObjectInHeight_, int numberOfInstancesPerObjectInDepth_, int tinyChunkWidth_, int tinyChunkHeight_, int tinyChunkDepth_, float planeSize_,int voxeltype_,int typeofbytemapobject_, int mainix, int mainiy, int mainiz, int meshzeroix, int meshzeroiy, int meshzeroiz, int instix, int instiy, int instiz
            ,
            out double m11b, out double m12b, out double m13b, out double m14b,
            out double m21b, out double m22b, out double m23b, out double m24b, out double m31b, out double m32b, out double m33b, out double m34b, out double m41b, out double m42b, out double m43b, out double m44b
             ,
            out double m11c, out double m12c, out double m13c, out double m14c,
            out double m21c, out double m22c, out double m23c, out double m24c, out double m31c, out double m32c, out double m33c, out double m34c, out double m41c, out double m42c, out double m43c, out double m44c
            ,
            out double m11d, out double m12d, out double m13d, out double m14d,
            out double m21d, out double m22d, out double m23d, out double m24d, out double m31d, out double m32d, out double m33d, out double m34d, out double m41d, out double m42d, out double m43d, out double m44d

            )// , int somechunkkeyboardpriminstanceindex_, int chunkprimindex_, int chunkinstindex_
        {
            typeofbytemapobject = typeofbytemapobject_;


            //somechunkkeyboardpriminstanceindex = somechunkkeyboardpriminstanceindex_;
            //chunkprimindex = chunkprimindex_;
            //chunkinstindex = chunkinstindex_;



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

            map = new byte[width * height * depth];


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

                map = new byte[width * height * depth];



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

                                    if (z == tinyChunkDepth - 1 && mainiz == numberofmainobjectz-1 && meshzeroiz == numberOfObjectInDepth-1 && instiz == numberOfInstancesPerObjectInDepth-1)
                                    {
                                        map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                                    }
                                    if (z == 0 && mainiz== 0 && meshzeroiz== 0 && instiz == 0)
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

            double arrayofbytemaprowm11a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111



            double arrayofbytemaprowm11b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111




            double arrayofbytemaprowm11c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111





            double arrayofbytemaprowm11d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111


            double selectablevectordouble = 0;















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

            total = width * height * depth;

            int switchXX = 0;
            int switchYY = 0;

            int byte1st = 0;
            int byte2nd = 0;
            int byte3rd = 0;

            int somebytecounter = 0;


            int thedigitbyte = 0;



            /*
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {

                        byte1st = 0;
                        byte2nd = 0;
                        byte3rd = 0;


                        int index = x + width * (y + height * z);// x + (width * (y + (height * z)));
                        int currentByte = map[index];

                        //Console.Write(" " + index);


                        int maxv = width * height * depth;
                        int somemaxvecdigit = 16;
                        int somecountermul = 0;
                        int somec = 0;

                        for (int t = 0; t < index; t++)
                        {
                            if (somec == somemaxvecdigit - 1)
                            {
                                somecountermul++;
                                somec = 0;
                            }
                            somec++;
                        }


                        //int somediv = index % 16;


                        if (somebytecounter == 0)
                        {
                            byte1st = currentByte;
                        }
                        else if (somebytecounter == 1)
                        {
                            byte2nd = currentByte;
                        }
                        else if (somebytecounter == 2)
                        {
                            byte3rd = currentByte;
                        }




                        switch (somecountermul)
                        {
                            case 0:
                                selectablevectordouble = arrayofbytemaprowm11a;
                                break;
                            case 1:
                                selectablevectordouble = arrayofbytemaprowm12a;
                                break;
                            case 2:
                                selectablevectordouble = arrayofbytemaprowm13a;
                                break;
                            case 3:
                                selectablevectordouble = arrayofbytemaprowm14a;
                                break;
                            case 4:
                                selectablevectordouble = arrayofbytemaprowm21a;
                                break;
                            case 5:
                                selectablevectordouble = arrayofbytemaprowm22a;
                                break;
                            case 6:
                                selectablevectordouble = arrayofbytemaprowm23a;
                                break;
                            case 7:
                                selectablevectordouble = arrayofbytemaprowm24a;
                                break;
                            case 8:
                                selectablevectordouble = arrayofbytemaprowm31a;
                                break;
                            case 9:
                                selectablevectordouble = arrayofbytemaprowm32a;
                                break;
                            case 10:
                                selectablevectordouble = arrayofbytemaprowm33a;
                                break;
                            case 11:
                                selectablevectordouble = arrayofbytemaprowm34a;
                                break;
                            case 12:
                                selectablevectordouble = arrayofbytemaprowm41a;
                                break;
                            case 13:
                                selectablevectordouble = arrayofbytemaprowm42a;
                                break;
                            case 14:
                                selectablevectordouble = arrayofbytemaprowm43a;
                                break;
                            case 15:
                                selectablevectordouble = arrayofbytemaprowm44a;
                                break;




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
                                break;
                        };





                        
                        if (somebytecounter >= 2)
                        {
                            if (byte1st == 0 && byte2nd == 0 && byte3rd == 0)
                            {
                                thedigitbyte = 0;


                                if (currentByte == 0)
                                {
                                    selectablevectordouble = (selectablevectordouble * 10);
                                }
                                else
                                {
                                    selectablevectordouble = (selectablevectordouble * 10) + thedigitbyte;
                                }
                            }
                            else if (byte1st == 1 && byte2nd == 0 && byte3rd == 0)
                            {
                                thedigitbyte = 1;
                                if (currentByte == 0)
                                {
                                    selectablevectordouble = (selectablevectordouble * 10);
                                }
                                else
                                {
                                    selectablevectordouble = (selectablevectordouble * 10) + thedigitbyte;
                                }

                            }
                            else if (byte1st == 0 && byte2nd == 1 && byte3rd == 0)
                            {
                                thedigitbyte = 2;
                                if (currentByte == 0)
                                {
                                    selectablevectordouble = (selectablevectordouble * 10);
                                }
                                else
                                {
                                    selectablevectordouble = (selectablevectordouble * 10) + thedigitbyte;
                                }
                            }
                            else if (byte1st == 0 && byte2nd == 0 && byte3rd == 1)
                            {
                                thedigitbyte = 3;
                                if (currentByte == 0)
                                {
                                    selectablevectordouble = (selectablevectordouble * 10);
                                }
                                else
                                {
                                    selectablevectordouble = (selectablevectordouble * 10) + thedigitbyte;
                                }
                            }
                            else if (byte1st == 1 && byte2nd == 0 && byte3rd == 1)
                            {
                                thedigitbyte = 4;
                                if (currentByte == 0)
                                {
                                    selectablevectordouble = (selectablevectordouble * 10);
                                }
                                else
                                {
                                    selectablevectordouble = (selectablevectordouble * 10) + thedigitbyte;
                                }
                            }
                            else if (byte1st == 1 && byte2nd == 1 && byte3rd == 0)
                            {
                                thedigitbyte = 5;
                                if (currentByte == 0)
                                {
                                    selectablevectordouble = (selectablevectordouble * 10);
                                }
                                else
                                {
                                    selectablevectordouble = (selectablevectordouble * 10) + thedigitbyte;
                                }
                            }
                            else if (byte1st == 1 && byte2nd == 1 && byte3rd == 1)
                            {
                                thedigitbyte = 6;
                                if (currentByte == 0)
                                {
                                    selectablevectordouble = (selectablevectordouble * 10);
                                }
                                else
                                {
                                    selectablevectordouble = (selectablevectordouble * 10) + thedigitbyte;
                                }
                            }
                            else if (byte1st == 0 && byte2nd == 1 && byte3rd == 1)
                            {
                                thedigitbyte = 7;
                                if (currentByte == 0)
                                {
                                    selectablevectordouble = (selectablevectordouble * 10);
                                }
                                else
                                {
                                    selectablevectordouble = (selectablevectordouble * 10) + thedigitbyte;
                                }
                            }
                            somebytecounter = 0;
                        }
                        somebytecounter++;

                    }
                }
            }*/








            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        //int index = x + (width * (y + (height * z)));
                        //Console.WriteLine("index:" + index);
                        //int currentByte = map[index];

                        int index = x + width * (y + height * z);// x + (width * (y + (height * z)));
                        int currentByte = map[index];

                        //Console.Write(" " + index);

                        int maxv = width * height * depth;
                        int somemaxvecdigit = 4;
                        int somecountermul = 0;
                        int somec = 0;

                        for (int t = 0; t < index; t++)
                        {
                            if (somec == somemaxvecdigit - 1)
                            {
                                somecountermul++;
                                somec = 0;
                            }
                            somec++;
                        }


                        switch (somecountermul)
                        {
                            case 0:
                                selectablevectordouble = arrayofbytemaprowm11a;
                                break;
                            case 1:
                                selectablevectordouble = arrayofbytemaprowm12a;
                                break;
                            case 2:
                                selectablevectordouble = arrayofbytemaprowm13a;
                                break;
                            case 3:
                                selectablevectordouble = arrayofbytemaprowm14a;
                                break;
                            case 4:
                                selectablevectordouble = arrayofbytemaprowm21a;
                                break;
                            case 5:
                                selectablevectordouble = arrayofbytemaprowm22a;
                                break;
                            case 6:
                                selectablevectordouble = arrayofbytemaprowm23a;
                                break;
                            case 7:
                                selectablevectordouble = arrayofbytemaprowm24a;
                                break;
                            case 8:
                                selectablevectordouble = arrayofbytemaprowm31a;
                                break;
                            case 9:
                                selectablevectordouble = arrayofbytemaprowm32a;
                                break;
                            case 10:
                                selectablevectordouble = arrayofbytemaprowm33a;
                                break;
                            case 11:
                                selectablevectordouble = arrayofbytemaprowm34a;
                                break;
                            case 12:
                                selectablevectordouble = arrayofbytemaprowm41a;
                                break;
                            case 13:
                                selectablevectordouble = arrayofbytemaprowm42a;
                                break;
                            case 14:
                                selectablevectordouble = arrayofbytemaprowm43a;
                                break;
                            case 15:
                                selectablevectordouble = arrayofbytemaprowm44a;
                                break;



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
                        };




                        if (currentByte == 0)
                        {
                            selectablevectordouble = (selectablevectordouble * 10);
                        }
                        else
                        {
                            selectablevectordouble = (selectablevectordouble * 10) + 1;
                        }






                        /*
                        if (index >= 0 && index <= 3)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 10) + 1;
                            }
                        }
                        else if (index >= 4 && index <= 7)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 10) + 1;
                            }
                        }
                        else if (index >= 8 && index <= 11)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 10) + 1;
                            }
                        }
                        else if (index >= 12 && index <= 15)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 10) + 1;
                            }
                        }

                        else if (index >= 16 && index <= 19)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 10) + 1;
                            }
                        }
                        else if (index >= 20 && index <= 23)
                        {

                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 10) + 1;
                            }

                        }
                        else if (index >= 24 && index <= 27)
                        {

                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 10) + 1;
                            }

                        }
                        else if (index >= 28 && index <= 31)
                        {

                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 10) + 1;
                            }

                        }
                        else if (index >= 32 && index <= 35)
                        {

                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 10) + 1;
                            }

                        }
                        else if (index >= 36 && index <= 39)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 10) + 1;
                            }
                        }
                        else if (index >= 40 && index <= 43)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 10) + 1;
                            }
                        }
                        else if (index >= 44 && index <= 47)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 10) + 1;
                            }
                        }
                        else if (index >= 48 && index <= 51)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 10) + 1;
                            }
                        }
                        else if (index >= 52 && index <= 55)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 10) + 1;
                            }
                        }
                        else if (index >= 56 && index <= 59)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 10) + 1;
                            }
                        }
                        else if (index >= 60 && index <= 63)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 10) + 1;
                            }
                        }*/
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











            mapper = map;

            if (swtcsetneighboors != -1)
            {
                //MainWindow.MessageBox((IntPtr)0, "/m11:" + oneInt + "/m12:" + oneIntTwo + "/m13:" + twoInt + "/m14:" + twoIntTwo + "/m21:" + threeInt + "/m22:" + threeIntTwo + "/m23:" + fourInt + "/m24:" + fourIntTwo, "sccs message 1", 0);

            }
        }





        public void resetmap(out double m11, out double m12, out double m13, out double m14, out double m21, out double m22, out double m23, out double m24, out double m31, out double m32, out double m33, out double m34, out double m41, out double m42, out double m43, out double m44, int chosenmap, int voxelchunkinvertoption)
        {
            float seed = (float)sc_maths.getSomeRandNum(3415, 3425); //3420;
      

            map = new byte[width * height * depth];

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

                        if (voxelchunkinvertoption == 5)
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




                            /*fastNoise = new FastNoise();
                            noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (somechunkpos.X * staticPlaneSize) + seed) / _detailScale) * _HeightScale, (((y * staticPlaneSize) + (somechunkpos.Y * staticPlaneSize) + seed) / _detailScale) * _HeightScale, (((z * staticPlaneSize) + (somechunkpos.Z * staticPlaneSize) + seed) / _detailScale) * _HeightScale);
                       
                            if (noiseXZ >= 0.1f)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                                map[x + width * (y + height * z)] = 0;
                            }*/



                        }
                        else if (voxelchunkinvertoption == 1)
                        {
                            _detailScale = 20; // 10
                            _HeightScale = 20; //200
                            float noiseXZ = 20;



                           /* fastNoise = new FastNoise();
                            noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (somechunkpos.X * staticPlaneSize) + seed) / _detailScale) * _HeightScale, (((y * staticPlaneSize) + (somechunkpos.Y * staticPlaneSize) + seed) / _detailScale) * _HeightScale, (((z * staticPlaneSize) + (somechunkpos.Z * staticPlaneSize) + seed) / _detailScale) * _HeightScale);

                            if (noiseXZ >= 0.1f)
                            {
                                map[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                                map[x + width * (y + height * z)] = 0;
                            }*/





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



            double arrayofbytemaprowm11 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111 
            double arrayofbytemaprowm22 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111








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
                        else if (index >= 12 && index <= 15 )
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

                        else if (index >= 16 && index <= 19 )
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
                        else if (index >= 20 && index <= 23 )
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
                        else if (index >= 32 && index <= 35 )
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
                        else if (index >= 36 && index <= 39 )
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
                        else if (index >= 48 && index <= 51 )
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
            m44 = arrayofbytemaprowm44;


        }








        public void setnewmap(out double m11, out double m12, out double m13, out double m14, out double m21, out double m22, out double m23, out double m24, out double m31, out double m32, out double m33, out double m34, out double m41, out double m42, out double m43, out double m44, int chosenmap,int voxelchunkinvertoption
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



            double arrayofbytemaprowm11 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111 
            double arrayofbytemaprowm22 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44 = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111







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



            double arrayofbytemaprowm11a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44a = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111



            double arrayofbytemaprowm11b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44b = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111




            double arrayofbytemaprowm11c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44c = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111





            double arrayofbytemaprowm11d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44d = MainWindow.usetypeofvoxel;// 1; //111111111111111111111111111


            double selectablevectordouble = 0;

            int switchXX = 0;
            int switchYY = 0;

            int byte1st = 0;
            int byte2nd = 0;
            int byte3rd = 0;

            int somebytecounter = 0;


            int thedigitbyte = 0;





























            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        int index = x + (width * (y + (height * z)));
                        int currentByte = map[index];

                        int maxv = width * height * depth;
                        int somemaxvecdigit = 16;
                        int somecountermul = 0;
                        int somec = 0;

                        for (int t = 0; t < index; t++)
                        {
                            if (somec == somemaxvecdigit)
                            {
                                somecountermul++;
                                somec = 0;
                            }
                            somec++;
                        }


                        //int somediv = index % 16;


                        if (somebytecounter == 0)
                        {
                            byte1st = currentByte;
                        }
                        else if (somebytecounter == 1)
                        {
                            byte2nd = currentByte;
                        }
                        else if (somebytecounter == 2)
                        {
                            byte3rd = currentByte;
                        }




                        switch (somecountermul)
                        {
                            case 0:
                                selectablevectordouble = arrayofbytemaprowm11a;
                                break;
                            case 1:
                                selectablevectordouble = arrayofbytemaprowm12a;
                                break;
                            case 2:
                                selectablevectordouble = arrayofbytemaprowm13a;
                                break;
                            case 3:
                                selectablevectordouble = arrayofbytemaprowm14a;
                                break;
                            case 4:
                                selectablevectordouble = arrayofbytemaprowm21a;
                                break;
                            case 5:
                                selectablevectordouble = arrayofbytemaprowm22a;
                                break;
                            case 6:
                                selectablevectordouble = arrayofbytemaprowm23a;
                                break;
                            case 7:
                                selectablevectordouble = arrayofbytemaprowm24a;
                                break;
                            case 8:
                                selectablevectordouble = arrayofbytemaprowm31a;
                                break;
                            case 9:
                                selectablevectordouble = arrayofbytemaprowm32a;
                                break;
                            case 10:
                                selectablevectordouble = arrayofbytemaprowm33a;
                                break;
                            case 11:
                                selectablevectordouble = arrayofbytemaprowm34a;
                                break;
                            case 12:
                                selectablevectordouble = arrayofbytemaprowm41a;
                                break;
                            case 13:
                                selectablevectordouble = arrayofbytemaprowm42a;
                                break;
                            case 14:
                                selectablevectordouble = arrayofbytemaprowm43a;
                                break;
                            case 15:
                                selectablevectordouble = arrayofbytemaprowm44a;
                                break;




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
                                break;
                        };






                        if (somebytecounter >= 2)
                        {
                            if (byte1st == 0 && byte2nd == 0 && byte3rd == 0)
                            {
                                thedigitbyte = 0;


                                if (currentByte == 0)
                                {
                                    selectablevectordouble = (selectablevectordouble * 10);
                                }
                                else
                                {
                                    selectablevectordouble = (selectablevectordouble * 10) + thedigitbyte;
                                }
                            }
                            else if (byte1st == 1 && byte2nd == 0 && byte3rd == 0)
                            {
                                thedigitbyte = 1;
                                if (currentByte == 0)
                                {
                                    selectablevectordouble = (selectablevectordouble * 10);
                                }
                                else
                                {
                                    selectablevectordouble = (selectablevectordouble * 10) + thedigitbyte;
                                }

                            }
                            else if (byte1st == 0 && byte2nd == 1 && byte3rd == 0)
                            {
                                thedigitbyte = 2;
                                if (currentByte == 0)
                                {
                                    selectablevectordouble = (selectablevectordouble * 10);
                                }
                                else
                                {
                                    selectablevectordouble = (selectablevectordouble * 10) + thedigitbyte;
                                }
                            }
                            else if (byte1st == 0 && byte2nd == 0 && byte3rd == 1)
                            {
                                thedigitbyte = 3;
                                if (currentByte == 0)
                                {
                                    selectablevectordouble = (selectablevectordouble * 10);
                                }
                                else
                                {
                                    selectablevectordouble = (selectablevectordouble * 10) + thedigitbyte;
                                }
                            }
                            else if (byte1st == 1 && byte2nd == 0 && byte3rd == 1)
                            {
                                thedigitbyte = 4;
                                if (currentByte == 0)
                                {
                                    selectablevectordouble = (selectablevectordouble * 10);
                                }
                                else
                                {
                                    selectablevectordouble = (selectablevectordouble * 10) + thedigitbyte;
                                }
                            }
                            else if (byte1st == 1 && byte2nd == 1 && byte3rd == 0)
                            {
                                thedigitbyte = 5;
                                if (currentByte == 0)
                                {
                                    selectablevectordouble = (selectablevectordouble * 10);
                                }
                                else
                                {
                                    selectablevectordouble = (selectablevectordouble * 10) + thedigitbyte;
                                }
                            }
                            else if (byte1st == 1 && byte2nd == 1 && byte3rd == 1)
                            {
                                thedigitbyte = 6;
                                if (currentByte == 0)
                                {
                                    selectablevectordouble = (selectablevectordouble * 10);
                                }
                                else
                                {
                                    selectablevectordouble = (selectablevectordouble * 10) + thedigitbyte;
                                }
                            }
                            else if (byte1st == 0 && byte2nd == 1 && byte3rd == 1)
                            {
                                thedigitbyte = 7;
                                if (currentByte == 0)
                                {
                                    selectablevectordouble = (selectablevectordouble * 10);
                                }
                                else
                                {
                                    selectablevectordouble = (selectablevectordouble * 10) + thedigitbyte;
                                }
                            }
                            somebytecounter = 0;
                        }
                        somebytecounter++;

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





        public void SetByte(int x, int y, int z, byte block, Vector3 chunkbytepos_)
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



        public bool somechunkIsTransparent(int _x, int _y, int _z, byte[] somemap)
        {
            if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= SC_Globals.numberOfInstancesPerObjectInWidth) || (_y >= SC_Globals.numberOfInstancesPerObjectInHeight) || (_z >= SC_Globals.numberOfInstancesPerObjectInDepth)) return true;
            return somemap[_x + SC_Globals.numberOfInstancesPerObjectInWidth * (_y + SC_Globals.numberOfInstancesPerObjectInHeight * _z)] == 0; //_chunkArray
        }




        public bool IsTransparent(int _x, int _y, int _z)
        {
            if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= width) || (_y >= height) || (_z >= depth)) return true;
            return map[_x + width * (_y + height * _z)] == 0; //_chunkArray
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









        /*
        int getChunkVertexByte(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
            {
                return _chunkVertexArray[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
            }
            return 0;
        }*/


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