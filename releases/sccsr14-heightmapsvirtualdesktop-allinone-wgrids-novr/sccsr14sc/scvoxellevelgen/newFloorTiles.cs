using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

using SimplexNoise;

namespace sccs
{

    public class newFloorTiles
    {
        private static newFloorTiles chunker;

        public static Dictionary<newFloorTiles, Vector3> chunkz = new Dictionary<newFloorTiles, Vector3>();


        private int width = 20;
        private int height = 20;
        private int depth = 20;

        private byte[] map;
        private float planeSize = 0.1f;
        private int seed = 3420;

        private int block;

        private sclevelgenchunk.DVertex[] arrayOfVerts;
        private Vector4[] arrayForData;
        private Vector4[] positions;
        private Vector3[] normals;
        private Vector2[] textureCoordinates;
        private int[] triangleIndices;
        private Vector3[] norms;
        private Vector2[] tex;
        Vector4[] tangents;


        private int counterVertexTop = 0;
        private int counterVertexBottom = 0;
        private int counterVertexRight = 0;
        private int counterVertexLeft = 0;
        private int counterVertexFront = 0;
        private int counterVertexBack = 0;

        private int vertzIndex = 0;
        private int trigsIndex = 0;

        private int _detailScale = 200;
        private int _heightScale = 5;

        //private int _detailScale = 200;
        //private int _heightScale = 5;

        private Vector4 forward = new Vector4(0, 0, 1, 1);
        private Vector4 back = new Vector4(0, 0, -1, 1);
        private Vector4 right = new Vector4(1, 0, 0, 1);
        private Vector4 left = new Vector4(-1, 0, 0, 1);
        private Vector4 up = new Vector4(0, 1, 0, 1);
        private Vector4 down = new Vector4(0, -1, 0, 1);

        int randX = 3420;
        int randY = 3420;
        public static int countingArrayOfChunks = 0;

        public float floorHeight = 80;

        public byte[] leftExtremity;
        public byte[] rightExtremity;
        public byte[] frontExtremity;
        public byte[] backExtremity;

        public byte[] leftInsideCornerExtremity;
        public byte[] rightInsideCornerExtremity;
        public byte[] frontInsideCornerExtremity;
        public byte[] backInsideCornerExtremity;

        public byte[] leftOutsideCornerExtremity;
        public byte[] rightOutsideCornerExtremity;
        public byte[] frontOutsideCornerExtremity;
        public byte[] backOutsideCornerExtremity;



        public static float xChunkPos;
        public static float yChunkPos;
        public static float zChunkPos;

        public byte[] startBuildingArray(Vector3 currentPosition, out Vector4[] vertexArray, out int[] indicesArray, out byte[] mapper, out sclevelgenchunk.DVertex[] dVertexArray, out Vector3[] norms, out Vector2[] tex, int tagnumber, LevelGenerator4 levelgen) //, out int vertexNum, out int indicesNum
        {


            chunkz.Add(this, currentPosition);
            //Console.WriteLine("yo000");
            map = new byte[width * height * depth];
            leftExtremity = new byte[width * height * depth];

            xChunkPos = currentPosition.X;
            yChunkPos = currentPosition.Y;
            zChunkPos = currentPosition.Z;



            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        float noiseXZ = 10;
                        //noiseXZ *= SimplexNoise.Noise((((x * planeSize) + currentPosition.X + seed) / _detailScale) * _heightScale, (((z * planeSize) + currentPosition.Z + seed) / _detailScale) * _heightScale);
                        noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * planeSize) + currentPosition.X + seed) / _detailScale) * _heightScale, (((z * planeSize) + currentPosition.Z + seed) / _detailScale) * _heightScale, 15, 15, 0);

                        float size0 = (1 / planeSize) * currentPosition.Y;
                        noiseXZ -= size0;
                        //noise = (noise + 1.0f) * 0.5f;
                        //float noiser1 = SimplexNoise.Noise(x, y);

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

            var seed0 = 3420;


            for (int j = 0; j < levelgen.leftWall.Count; j++)
            {
                if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.leftWall[j])
                {
                    for (int x = 0; x < width; x++)
                    {
                        float noiseX = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 100);
                        float noiseX2 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        for (int y = 0; y < height; y++)
                        {
                            float noiseY = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 100);
                            float noiseY2 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            for (int z = 0; z < width; z++)
                            {
                                float noiseZ = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 100);
                                float noiseZ2 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);

                                float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                                noiseValue += (10 - (float)y) / 10;
                                noiseValue /= (float)y / 5;

                                if (noiseValue > 0.2f && y < floorHeight)
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
                                    leftExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                }
                            }
                        }
                    }
                }
            }





            for (int j = 0; j < levelgen.rightWall.Count; j++)
            {
                if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.rightWall[j])
                {
                    for (int x = 0; x < width; x++)
                    {
                        float noiseX = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 100);
                        float noiseX2 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        for (int y = 0; y < height; y++)
                        {
                            float noiseY = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 100);
                            float noiseY2 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            for (int z = 0; z < width; z++)
                            {
                                float noiseZ = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 100);
                                float noiseZ2 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);

                                float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                                float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);

                                noiseValue += (10 - (float)y) / 10;
                                noiseValue /= (float)y / 5;

                                if (noiseValue > 0.2f && y < floorHeight)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }

                                float noiseValue3i = noiseValue2;

                                noiseValue3i += (5 - (float)x) / 5;
                                noiseValue3i /= (float)x / 5;

                                if (noiseValue3i < 0.2f)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                    rightExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                }
                            }
                        }
                    }
                }
            }

            /////////////////////////////////////FRONT WALL/////////////////////////////////

            for (int j = 0; j < levelgen.frontWall.Count; j++)
            {
                if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.frontWall[j])
                {
                    for (int x = 0; x < width; x++)
                    {
                        float noiseX = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 100);
                        float noiseX5 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        for (int y = 0; y < height; y++)
                        {
                            float noiseY = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 100);
                            float noiseY5 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            for (int z = 0; z < width; z++)
                            {
                                float noiseZ = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 100);
                                float noiseZ5 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);

                                float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                                float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                noiseValue += (10 - (float)y) / 10;
                                noiseValue /= (float)y / 5;

                                if (noiseValue > 0.2f && y < floorHeight)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }

                                float noiseValue6i = noiseValue5;

                                noiseValue6i += (5 - (float)z) / 5;
                                noiseValue6i /= (float)z / 5;

                                if (noiseValue6i > 0.2f)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                    frontExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                }
                            }
                        }
                    }
                }
            }



            /////////////////////////////////////BACK WALL////////////////////////////////

            for (int j = 0; j < levelgen.backWall.Count; j++)
            {
                if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.backWall[j])
                {
                    for (int x = 0; x < width; x++)
                    {
                        float noiseX = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 100);
                        float noiseX5 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        for (int y = 0; y < height; y++)
                        {
                            float noiseY = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 100);
                            float noiseY5 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            for (int z = 0; z < width; z++)
                            {
                                float noiseZ = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 100);
                                float noiseZ5 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);

                                float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                                float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                noiseValue += (10 - (float)y) / 10;
                                noiseValue /= (float)y / 5;

                                if (noiseValue > 0.2f && y < floorHeight)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }

                                float noiseValue4i = noiseValue5;

                                noiseValue4i += (5 - (float)z) / 5;
                                noiseValue4i /= (float)z / 5;


                                if (noiseValue4i < 0.2f)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                    backExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                }
                            }
                        }
                    }
                }
            }



            /////////////////////////////////////LEFT FRONT INSIDE CORNER////////////////////////////////

            for (int j = 0; j < levelgen.builtLeftFrontInsideCorner.Count; j++)
            {
                if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtLeftFrontInsideCorner[j])
                {
                    for (int x = 0; x < width; x++)
                    {
                        float noiseX = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 100);
                        float noiseX2 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        float noiseX5 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        for (int y = 0; y < height; y++)
                        {
                            float noiseY = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 100);
                            float noiseY2 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            float noiseY5 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            for (int z = 0; z < width; z++)
                            {
                                float noiseZ = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 100);
                                float noiseZ2 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);
                                float noiseZ5 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);

                                float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);


                                float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                noiseValue += (10 - (float)y) / 10;
                                noiseValue /= (float)y / 5;

                                if (noiseValue > 0.2f && y < floorHeight)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }

                                float noiseValue2i = noiseValue2;
                                noiseValue2i += (5 - (float)x) / 5;
                                noiseValue2i /= (float)x / 5;

                                float noiseValue5i = noiseValue5;

                                noiseValue5i += (5 - (float)z) / 5;
                                noiseValue5i /= (float)z / 5;


                                if (noiseValue2i > 0.2f || noiseValue5i < 0.2f)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                    leftInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                }
                            }
                        }
                    }
                }
            }






            /////////////////////////////////////RIGHT FRONT INSIDE CORNER////////////////////////////////

            for (int j = 0; j < levelgen.builtRightFrontInsideCorner.Count; j++)
            {
                if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtRightFrontInsideCorner[j])
                {
                    for (int x = 0; x < width; x++)
                    {
                        float noiseX = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 100);
                        float noiseX2 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        float noiseX5 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        for (int y = 0; y < height; y++)
                        {
                            float noiseY = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 100);
                            float noiseY2 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            float noiseY5 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            for (int z = 0; z < width; z++)
                            {
                                float noiseZ = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 100);
                                float noiseZ2 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);
                                float noiseZ5 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);

                                float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);


                                float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                noiseValue += (10 - (float)y) / 10;
                                noiseValue /= (float)y / 5;

                                if (noiseValue > 0.2f && y < floorHeight)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }

                                float noiseValue7i = noiseValue2;
                                noiseValue7i += (5 - (float)x) / 5;
                                noiseValue7i /= (float)x / 5;

                                float noiseValue8i = noiseValue5;
                                noiseValue8i += (5 - (float)z) / 5;
                                noiseValue8i /= (float)z / 5;

                                if (noiseValue7i < 0.2f || noiseValue8i < 0.2f)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                    rightInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                }
                            }
                        }
                    }
                }
            }




            /////////////////////////////////////LEFT BACK INSIDE CORNER////////////////////////////////

            for (int j = 0; j < levelgen.builtLeftBackInsideCorner.Count; j++)
            {
                if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtLeftBackInsideCorner[j])
                {
                    for (int x = 0; x < width; x++)
                    {
                        float noiseX = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 100);
                        float noiseX2 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        float noiseX5 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        for (int y = 0; y < height; y++)
                        {
                            float noiseY = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 100);
                            float noiseY2 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            float noiseY5 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            for (int z = 0; z < width; z++)
                            {
                                float noiseZ = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 100);
                                float noiseZ2 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);
                                float noiseZ5 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);

                                float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);


                                float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                noiseValue += (10 - (float)y) / 10;
                                noiseValue /= (float)y / 5;

                                if (noiseValue > 0.2f && y < floorHeight)
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
                                    backInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                }
                            }
                        }
                    }
                }
            }





            /////////////////////////////////////RIGHT BACK INSIDE CORNER////////////////////////////////

            for (int j = 0; j < levelgen.builtRightBackInsideCorner.Count; j++)
            {
                if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtRightBackInsideCorner[j])
                {
                    for (int x = 0; x < width; x++)
                    {
                        float noiseX = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 100);
                        float noiseX2 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        float noiseX5 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        for (int y = 0; y < height; y++)
                        {
                            float noiseY = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 100);
                            float noiseY2 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            float noiseY5 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            for (int z = 0; z < width; z++)
                            {
                                float noiseZ = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 100);
                                float noiseZ2 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);
                                float noiseZ5 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);

                                float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);


                                float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                noiseValue += (10 - (float)y) / 10;
                                noiseValue /= (float)y / 5;

                                if (noiseValue > 0.2f && y < floorHeight)
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
                                    frontInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                }
                            }
                        }
                    }
                }
            }



            /////////////////////////////////////LEFT FRONT OUTSIDE CORNER////////////////////////////////

            for (int j = 0; j < levelgen.builtLeftFrontOutsideCorner.Count; j++)
            {
                if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtLeftFrontOutsideCorner[j])
                {
                    for (int x = 0; x < width; x++)
                    {
                        float noiseX = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 100);
                        float noiseX2 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        float noiseX5 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        for (int y = 0; y < height; y++)
                        {
                            float noiseY = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 100);
                            float noiseY2 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            float noiseY5 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            for (int z = 0; z < width; z++)
                            {
                                float noiseZ = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 100);
                                float noiseZ2 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);
                                float noiseZ5 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);

                                float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);


                                float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                noiseValue += (10 - (float)y) / 10;
                                noiseValue /= (float)y / 5;

                                if (noiseValue > 0.2f && y < floorHeight)
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
                                    leftOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                }
                            }
                        }
                    }
                }
            }



            /////////////////////////////////////RIGHT FRONT OUTSIDE CORNER////////////////////////////////

            for (int j = 0; j < levelgen.builtRightFrontOutsideCorner.Count; j++)
            {
                if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtRightFrontOutsideCorner[j])
                {
                    for (int x = 0; x < width; x++)
                    {
                        float noiseX = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 100);
                        float noiseX2 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        float noiseX5 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        for (int y = 0; y < height; y++)
                        {
                            float noiseY = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 100);
                            float noiseY2 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            float noiseY5 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            for (int z = 0; z < width; z++)
                            {
                                float noiseZ = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 100);
                                float noiseZ2 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);
                                float noiseZ5 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);

                                float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);


                                float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                noiseValue += (10 - (float)y) / 10;
                                noiseValue /= (float)y / 5;

                                if (noiseValue > 0.2f && y < floorHeight)
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
                                    rightOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                }
                            }
                        }
                    }
                }
            }



            /////////////////////////////////////LEFT BACK OUTSIDE CORNER////////////////////////////////


            for (int j = 0; j < levelgen.builtLeftBackOutsideCorner.Count; j++)
            {
                if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtLeftBackOutsideCorner[j])
                {
                    for (int x = 0; x < width; x++)
                    {
                        float noiseX = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 100);
                        float noiseX2 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        float noiseX5 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        for (int y = 0; y < height; y++)
                        {
                            float noiseY = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 100);
                            float noiseY2 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            float noiseY5 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            for (int z = 0; z < width; z++)
                            {
                                float noiseZ = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 100);
                                float noiseZ2 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);
                                float noiseZ5 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);

                                float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);


                                float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                noiseValue += (10 - (float)y) / 10;
                                noiseValue /= (float)y / 5;

                                if (noiseValue > 0.2f && y < floorHeight)
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
                                    backOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                }
                            }
                        }
                    }
                }
            }




            /////////////////////////////////////RIGHT BACK OUTSIDE CORNER////////////////////////////////

            for (int j = 0; j < levelgen.builtRightBackOutsideCorner.Count; j++)
            {
                if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtRightBackOutsideCorner[j])
                {
                    for (int x = 0; x < width; x++)
                    {
                        float noiseX = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 100);
                        float noiseX2 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        float noiseX5 = Math.Abs((float)(x * planeSize + currentPosition.X + seed0) / 25);
                        for (int y = 0; y < height; y++)
                        {
                            float noiseY = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 100);
                            float noiseY2 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            float noiseY5 = Math.Abs((float)(y * planeSize + currentPosition.Y + seed0) / 25);
                            for (int z = 0; z < width; z++)
                            {
                                float noiseZ = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 100);
                                float noiseZ2 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);
                                float noiseZ5 = Math.Abs((float)(z * planeSize + currentPosition.Z + seed0) / 25);

                                float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);


                                float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                noiseValue += (10 - (float)y) / 10;
                                noiseValue /= (float)y / 5;

                                if (noiseValue > 0.2f && y < floorHeight)
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
                                    frontOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                }
                            }
                        }

                    }
                }
            }













            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        block = map[x + width * (y + height * z)];
                        //calculateNumberOfVertex(x + width * (y + height * z));
                        if (block == 0) continue;
                        {
                            calculateNumberOfVertex(x,y,z);
                        }
                    }
                }
            }












            // + counterVertexBottom * 6 + counterVertexRight * 6 + counterVertexLeft * 6 + counterVertexFront * 6 + counterVertexBack * 6
            // * 4 + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4
            // + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4

            arrayOfVerts = new sclevelgenchunk.DVertex[counterVertexTop * 4 + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4];

            positions = new Vector4[counterVertexTop * 4 + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4];
            normals = new Vector3[counterVertexTop * 4 + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4];
            textureCoordinates = new Vector2[counterVertexTop * 4 + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4];
            triangleIndices = new int[counterVertexTop * 6 + counterVertexBottom * 6 + counterVertexRight * 6 + counterVertexLeft * 6 + counterVertexFront * 6 + counterVertexBack * 6];

            Regenerate(currentPosition);




            /*Vector3[] tan1 = new Vector3[positions.Length];
            Vector3[] tan2 = new Vector3[positions.Length];
            tangents = new Vector4[positions.Length];

            for (long a = 0; a < triangleIndices.Length / 3; a += 3)
            {
                long i1 = triangleIndices[a + 0];
                long i2 = triangleIndices[a + 1];
                long i3 = triangleIndices[a + 2];
                Vector3 v1 = new Vector3(positions[i1].X, positions[i1].Y, positions[i1].Z);
                Vector3 v2 = new Vector3(positions[i2].X, positions[i2].Y, positions[i2].Z); //positions[i2];
                Vector3 v3 = new Vector3(positions[i3].X, positions[i3].Y, positions[i3].Z); //positions[i3];
                Vector2 w1 = textureCoordinates[i1];
                Vector2 w2 = textureCoordinates[i2];
                Vector2 w3 = textureCoordinates[i3];
                float x1 = v2.X - v1.X;
                float x2 = v3.X - v1.X;
                float y1 = v2.Y - v1.Y;
                float y2 = v3.Y - v1.Y;
                float z1 = v2.Z - v1.Z;
                float z2 = v3.Z - v1.Z;
                float s1 = w2.X - w1.X;
                float s2 = w3.X - w1.X;
                float t1 = w2.Y - w1.Y;
                float t2 = w3.Y - w1.Y;
                float r = 1.0f / (s1 * t2 - s2 * t1);
                Vector3 sdir = new Vector3((t2 * x1 - t1 * x2) * r, (t2 * y1 - t1 * y2) * r, (t2 * z1 - t1 * z2) * r);
                Vector3 tdir = new Vector3((s1 * x2 - s2 * x1) * r, (s1 * y2 - s2 * y1) * r, (s1 * z2 - s2 * z1) * r);
                tan1[i1] += sdir;
                tan1[i2] += sdir;
                tan1[i3] += sdir;
                tan2[i1] += tdir;
                tan2[i2] += tdir;
                tan2[i3] += tdir;
            }
            for (long a = 0; a < positions.Length; ++a)
            {
                Vector3 n = normals[a];
                Vector3 t = tan1[a];
                Vector3 tmp = (t - n * Vector3.Dot(n, t));
                tmp.Normalize();
                tangents[a] = new Vector4(tmp.X, tmp.Y, tmp.Z,1);
                tangents[a].W = (Vector3.Dot(Vector3.Cross(n, t), tan2[a]) < 0.0f) ? -1.0f : 1.0f;
            }


            //tangentz = tangents;



            for (int i = 0;i < arrayOfVerts.Length;i++)
            {
                arrayOfVerts[i].tangent = tangents[i];
            }*/




            //float3 binormal = cross(normal, tangent.xyz) * tangent.w;















            vertexArray = positions;
            indicesArray = triangleIndices;

            norms = normals;
            tex = textureCoordinates;
            dVertexArray = arrayOfVerts;

            //vertexArray = positions;
            //triangleArray = triangleIndices;
            mapper = map;

            //arrayOfVertz = arrayOfVerts;
            /*if (arrayOfVerts.Length<=0)
            {
                arrayOfVertz = null;
            }
            else
            {
          
            }*/
            return map;

            //vertexNum = counterVertexTop * 4;// + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4;
            //indicesNum = counterVertexTop * 6;// + counterVertexBottom * 6 + counterVertexRight * 6 + counterVertexLeft * 6 + counterVertexFront * 6 + counterVertexBack * 6;


            //currentChunk = new GameObject();
            //mesh = new Mesh();
            //mesh.Clear();
            //currentChunk.AddComponent<MeshFilter>().mesh = mesh;

            //string texture = "Assets/Resources/Textures/green";
            //mat = Resources.Load(texture, typeof(Texture)) as Texture;
            //currentChunk.AddComponent<MeshRenderer>().material.mainTexture = mat;
            //mesh.vertices = positions.ToArray();
            //mesh.triangles = triangleIndices.ToArray();
            ///mesh.RecalculateNormals();
            //currentChunk.transform.position = position;
        }

        public void calculateNumberOfVertex(int x, int y, int z)
        {
            //TOPFACE
            if (IsTransparent(x, y + 1, z))
            {
                counterVertexTop += 1;
            }
            //LEFTFACE
            if (IsTransparent(x - 1, y, z))
            {
                counterVertexLeft += 1;
            }
            //RIGHTFACE
            if (IsTransparent(x + 1, y, z))
            {
                counterVertexRight += 1;
            }
            //FRONTFACE
            if (IsTransparent(x, y, z - 1))
            {
                counterVertexFront += 1;
            }
            //BACKFACE
            if (IsTransparent(x, y, z + 1))
            {
                counterVertexBack += 1;
            }
            //BOTTOMFACE
            if (IsTransparent(x, y - 1, z))
            {
                counterVertexBottom += 1;
            }
        }
        public void Regenerate(Vector3 currentPosition)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        block = map[x + width * (y + height * z)];

                        if (block == 0) continue;
                        {
                            DrawBrick(x,y,z, currentPosition);
                        }
                    }
                }
            }
        }

        //chunkPosBig chunkbig;

        public void DrawBrick(int x, int y, int z, Vector3 currentPosition)
        {

            Vector4 start = new Vector4(x * planeSize, y * planeSize, z * planeSize, 1);
            Vector4 offset1, offset2;

            //TOPFACE
            if (IsTransparent(x, y + 1, z))
            {
                offset1 = forward * planeSize;
                offset2 = right * planeSize;
                createTopFace(start + up * planeSize, offset1, offset2);
                vertzIndex += 4;
                trigsIndex += 6;
            }
            //LEFTFACE
            if (IsTransparent(x - 1, y, z))
            {
                offset1 = back * planeSize;
                offset2 = down * planeSize;
                createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2);
                vertzIndex += 4;
                trigsIndex += 6;
            }

            //RIGHTFACE
            if (IsTransparent(x + 1, y, z))
            {
                offset1 = up * planeSize;
                offset2 = forward * planeSize;
                createRightFace(start + right * planeSize, offset1, offset2);
                vertzIndex += 4;
                trigsIndex += 6;
            }
            //FRONTFACE
            if (IsTransparent(x, y, z - 1))
            {
                offset1 = left * planeSize;
                offset2 = up * planeSize;
                createFrontFace(start + right * planeSize, offset1, offset2);
                vertzIndex += 4;
                trigsIndex += 6;
            }
            //BACKFACE
            if (IsTransparent(x, y, z + 1))
            {
                offset1 = right * planeSize;
                offset2 = up * planeSize;
                createBackFace(start + forward * planeSize, offset1, offset2);
                vertzIndex += 4;
                trigsIndex += 6;
            }
            //BOTTOMFACE
            if (IsTransparent(x, y - 1, z))
            {
                offset1 = right * planeSize;
                offset2 = forward * planeSize;
                createBottomFace(start, offset1, offset2);
                vertzIndex += 4;
                trigsIndex += 6;
            }
        }

        private void createTopFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {
            positions[0 + vertzIndex] = start;
            positions[1 + vertzIndex] = start + offset1;
            positions[2 + vertzIndex] = start + offset2;
            positions[3 + vertzIndex] = start + offset1 + offset2;


            arrayOfVerts[0 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(1f, 1f),
            };
            arrayOfVerts[1 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start + offset1,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(1f, 1f),
            };

            arrayOfVerts[2 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(1f, 1f),
            };

            arrayOfVerts[3 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start + offset1 + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(1f, 1f),
            };




            normals[0 + vertzIndex] = new Vector3(-1, 1, 0);
            normals[1 + vertzIndex] = new Vector3(-1, 1, 0);
            normals[2 + vertzIndex] = new Vector3(-1, 1, 0);
            normals[3 + vertzIndex] = new Vector3(-1, 1, 0);


            textureCoordinates[0 + vertzIndex] = new Vector2(1f, 1f);
            textureCoordinates[1 + vertzIndex] = new Vector2(1f, 1f);
            textureCoordinates[2 + vertzIndex] = new Vector2(1f, 1f);
            textureCoordinates[3 + vertzIndex] = new Vector2(1f, 1f);

            triangleIndices[0 + trigsIndex] = 0 + vertzIndex;
            triangleIndices[1 + trigsIndex] = 1 + vertzIndex;
            triangleIndices[2 + trigsIndex] = 2 + vertzIndex;
            triangleIndices[3 + trigsIndex] = 3 + vertzIndex;
            triangleIndices[4 + trigsIndex] = 2 + vertzIndex;
            triangleIndices[5 + trigsIndex] = 1 + vertzIndex;
        }



        private void createBottomFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {

            //offset1 = right * planeSize;
            //offset2 = forward * planeSize;
            //createBottomFace(start, offset1, offset2);
            //vertzIndex += 4;
            //trigsIndex += 6;

            positions[0 + vertzIndex] = start; //(x,y,z)
            positions[1 + vertzIndex] = start + offset1; //(x+1,y,z)
            positions[2 + vertzIndex] = start + offset2;//(x,y,z+1)
            positions[3 + vertzIndex] = start + offset1 + offset2;//(x+1,y,z+1)


            arrayOfVerts[0 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(0f, 1f),

            };
            arrayOfVerts[1 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start + offset1,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(0f, 1f),
            };

            arrayOfVerts[2 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(0f, 1f),
            };

            arrayOfVerts[3 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start + offset1 + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(0f, 1f),
            };

            normals[0 + vertzIndex] = new Vector3(0, 1, -1);
            normals[1 + vertzIndex] = new Vector3(0, 1, -1);
            normals[2 + vertzIndex] = new Vector3(0, 1, -1);
            normals[3 + vertzIndex] = new Vector3(0, 1, -1);

            textureCoordinates[0 + vertzIndex] = new Vector2(0f, 1f);
            textureCoordinates[1 + vertzIndex] = new Vector2(0f, 1f);
            textureCoordinates[2 + vertzIndex] = new Vector2(0f, 1f);
            textureCoordinates[3 + vertzIndex] = new Vector2(0f, 1f);

            triangleIndices[0 + trigsIndex] = 0 + vertzIndex;
            triangleIndices[1 + trigsIndex] = 1 + vertzIndex;
            triangleIndices[2 + trigsIndex] = 2 + vertzIndex;
            triangleIndices[3 + trigsIndex] = 3 + vertzIndex;
            triangleIndices[4 + trigsIndex] = 2 + vertzIndex;
            triangleIndices[5 + trigsIndex] = 1 + vertzIndex;
        }


        private void createFrontFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {

            //offset1 = left * planeSize;
            //offset2 = up * planeSize;
            //createFrontFace(start + right * planeSize, offset1, offset2);
            //vertzIndex += 4;
            //trigsIndex += 6;


            positions[0 + vertzIndex] = start; //(x+1,y,z)
            positions[1 + vertzIndex] = start + offset1;//(x,y,z)
            positions[2 + vertzIndex] = start + offset2;//(x+1,y+1,z)
            positions[3 + vertzIndex] = start + offset1 + offset2;//(x,y+1,z)

            arrayOfVerts[0 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(1, 0),

            };
            arrayOfVerts[1 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start + offset1,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(1, 1f),
            };

            arrayOfVerts[2 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(1, 0),
            };

            arrayOfVerts[3 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start + offset1 + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(0f, 1f),
            };

            normals[0 + vertzIndex] = new Vector3(-1, 0, 0);
            normals[1 + vertzIndex] = new Vector3(-1, 0, 0);
            normals[2 + vertzIndex] = new Vector3(-1, 0, 0);
            normals[3 + vertzIndex] = new Vector3(-1, 0, 0);

            textureCoordinates[0 + vertzIndex] = new Vector2(1f, 0f);
            textureCoordinates[1 + vertzIndex] = new Vector2(1f, 1f);
            textureCoordinates[2 + vertzIndex] = new Vector2(1f, 0f);
            textureCoordinates[3 + vertzIndex] = new Vector2(0f, 1f);

            triangleIndices[0 + trigsIndex] = 0 + vertzIndex;
            triangleIndices[1 + trigsIndex] = 1 + vertzIndex;
            triangleIndices[2 + trigsIndex] = 2 + vertzIndex;
            triangleIndices[3 + trigsIndex] = 3 + vertzIndex;
            triangleIndices[4 + trigsIndex] = 2 + vertzIndex;
            triangleIndices[5 + trigsIndex] = 1 + vertzIndex;
        }
        private void createBackFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {
            //offset1 = right * planeSize;
            //offset2 = up * planeSize;
            //createBackFace(start + forward * planeSize, offset1, offset2);
            //vertzIndex += 4;
            //trigsIndex += 6;


            positions[0 + vertzIndex] = start; //(x,y,z+1)
            positions[1 + vertzIndex] = start + offset1;//(x+1,y,z+1)
            positions[2 + vertzIndex] = start + offset2;//(x,y+1,z+1)
            positions[3 + vertzIndex] = start + offset1 + offset2;//(x+1,y+1,z+1)


            arrayOfVerts[0 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(1, 1),

            };
            arrayOfVerts[1 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start + offset1,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(1, 0),
            };

            arrayOfVerts[2 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(1, 1),
            };

            arrayOfVerts[3 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start + offset1 + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(0f, 1f),
            };
            normals[0 + vertzIndex] = new Vector3(0, 0, -1);
            normals[1 + vertzIndex] = new Vector3(0, 0, -1);
            normals[2 + vertzIndex] = new Vector3(0, 0, -1);
            normals[3 + vertzIndex] = new Vector3(0, 0, -1);

            textureCoordinates[0 + vertzIndex] = new Vector2(1f, 1f);
            textureCoordinates[1 + vertzIndex] = new Vector2(1f, 0f);
            textureCoordinates[2 + vertzIndex] = new Vector2(1f, 1f);
            textureCoordinates[3 + vertzIndex] = new Vector2(0f, 1f);

            triangleIndices[0 + trigsIndex] = 0 + vertzIndex;
            triangleIndices[1 + trigsIndex] = 1 + vertzIndex;
            triangleIndices[2 + trigsIndex] = 2 + vertzIndex;
            triangleIndices[3 + trigsIndex] = 3 + vertzIndex;
            triangleIndices[4 + trigsIndex] = 2 + vertzIndex;
            triangleIndices[5 + trigsIndex] = 1 + vertzIndex;
        }

        private void createRightFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {
            //offset1 = up * planeSize;
            //offset2 = forward * planeSize;
            //createRightFace(start + right * planeSize, offset1, offset2);
            //vertzIndex += 4;
            //trigsIndex += 6;



            positions[0 + vertzIndex] = start; // (x+1,y,z)
            positions[1 + vertzIndex] = start + offset1; // (x+1,y+1,z)
            positions[2 + vertzIndex] = start + offset2; // // (x+1,y,z+1)
            positions[3 + vertzIndex] = start + offset1 + offset2; //(x+1,y+1,z+1)

            arrayOfVerts[0 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(1, 0),

            };
            arrayOfVerts[1 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start + offset1,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(1, 0),
            };

            arrayOfVerts[2 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(1, 0),
            };

            arrayOfVerts[3 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start + offset1 + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(0f, 1f),
            };

            normals[0 + vertzIndex] = new Vector3(-1, 0, -1);
            normals[1 + vertzIndex] = new Vector3(-1, 0, -1);
            normals[2 + vertzIndex] = new Vector3(-1, 0, -1);
            normals[3 + vertzIndex] = new Vector3(-1, 0, -1);



            textureCoordinates[0 + vertzIndex] = new Vector2(1f, 0f);
            textureCoordinates[1 + vertzIndex] = new Vector2(1f, 0f);
            textureCoordinates[2 + vertzIndex] = new Vector2(1f, 0f);
            textureCoordinates[3 + vertzIndex] = new Vector2(0f, 1f);

            triangleIndices[0 + trigsIndex] = 0 + vertzIndex;
            triangleIndices[1 + trigsIndex] = 1 + vertzIndex;
            triangleIndices[2 + trigsIndex] = 2 + vertzIndex;
            triangleIndices[3 + trigsIndex] = 3 + vertzIndex;
            triangleIndices[4 + trigsIndex] = 2 + vertzIndex;
            triangleIndices[5 + trigsIndex] = 1 + vertzIndex;
        }

        private void createleftFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {
            //offset1 = back * planeSize;
            //offset2 = down * planeSize;

            positions[0 + vertzIndex] = start; //(x,y+1,z+1)
            positions[1 + vertzIndex] = start + offset1;//(x,y+1,z)
            positions[2 + vertzIndex] = start + offset2; //(x,y,z+1)
            positions[3 + vertzIndex] = start + offset1 + offset2;//(x,y,z)

            arrayOfVerts[0 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            };
            arrayOfVerts[1 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start + offset1,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),
            };

            arrayOfVerts[2 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),
            };

            arrayOfVerts[3 + vertzIndex] = new sclevelgenchunk.DVertex()
            {
                position = start + offset1 + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0f, 0),
            };

            normals[0 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[1 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[2 + vertzIndex] = new Vector3(-1, 1, -1);
            normals[3 + vertzIndex] = new Vector3(-1, 1, -1);

            textureCoordinates[0 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[1 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[2 + vertzIndex] = new Vector2(0f, 0f);
            textureCoordinates[3 + vertzIndex] = new Vector2(0f, 0f);

            triangleIndices[0 + trigsIndex] = 0 + vertzIndex;
            triangleIndices[1 + trigsIndex] = 1 + vertzIndex;
            triangleIndices[2 + trigsIndex] = 2 + vertzIndex;
            triangleIndices[3 + trigsIndex] = 3 + vertzIndex;
            triangleIndices[4 + trigsIndex] = 2 + vertzIndex;
            triangleIndices[5 + trigsIndex] = 1 + vertzIndex;
        }

        public bool IsTransparent(int x, int y, int z)
        {
            if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= height) || (z >= depth)) return true;
            {
                return map[x + width * (y + height * z)] == 0;
                //return map[x + width * (y + depth * z)] == 0;
            }
        }
        public int GetByte(int x, int y, int z)
        {
            if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
            {
                return 0;
            }
            return map[x + width * (y + height * z)];
            //return map[x + width * (y + depth * z)];
        }
        /*public bigChunk getBigChunk(float xi, float yi, float zi)
        {
            int x = (int)xi;
            int y = (int)yi;
            int z = (int)zi;

            if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= width) || (z >= width))
            {
                return null;
            }
            return bigFuckingChunk[x + width * (y + height * z)];
        }*/
    }
}

