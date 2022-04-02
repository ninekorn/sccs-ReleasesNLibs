using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;


namespace _sc_core_systems
{
    public class chunk
    {
        private static chunk chunker;


        public const int mapWidth = 4;
        public const int mapHeight = 4;
        public const int mapDepth = 4;

        public const int tinyChunkWidth = 4;
        public const int tinyChunkHeight = 4;
        public const int tinyChunkDepth = 4;

        public const int mapObjectInstanceWidth = 4;
        public const int mapObjectInstanceHeight = 4;
        public const int mapObjectInstanceDepth = 4;

        private int[] map;
        private float planeSize = 0.1f;
        private int seed = 3420;

        private int block;

        private List<SC_VR_Chunk.DVertex> listOfVerts = new List<SC_VR_Chunk.DVertex>();
        private List<int> listOfTriangleIndices = new List<int>();

        private List<Vector4> listOfInstancePos = new List<Vector4>();

        private int[] triangleIndices;
        private SC_VR_Chunk.DVertex[] arrayOfVerts;
        private Vector4[] arrayForData;
        private Vector4[] positions;
        private Vector3[] normals;
        private Vector2[] textureCoordinates;

        Vector4[] tangents;


        private int counterVertexTop = 0;
        private int counterVertexBottom = 0;
        private int counterVertexRight = 0;
        private int counterVertexLeft = 0;
        private int counterVertexFront = 0;
        private int counterVertexBack = 0;

        private int vertzIndex = 0;
        private int trigsIndex = 0;

        private int _detailScale = 10;
        private int _tinyChunkHeightScale = 200; //200

        //private int _detailScale = 200;
        //private int _tinyChunkHeightScale = 5;

        private Vector4 forward = new Vector4(0, 0, 1, 0);
        private Vector4 back = new Vector4(0, 0, -1, 0);
        private Vector4 right = new Vector4(1, 0, 0, 0);
        private Vector4 left = new Vector4(-1, 0, 0, 0);
        private Vector4 up = new Vector4(0, 1, 0, 0);
        private Vector4 down = new Vector4(0, -1, 0, 0);

        int randX = 3420;
        int randY = 3420;










        //Matrix[] arrayOfMatrixData;
        public static int countingArrayOfChunks = 0;
        //, out SC_VR_Chunk.DVertex[] dVertexArray, out Vector3[] norms, out Vector2[] tex // out Vector4[] vertexArray, out int[] indicesArray,
        public void startBuildingArray(Vector4 currentPosition, out Vector4 arrayOfDeVectorMapTemp, out Vector4 arrayOfDeVectorMapTempTwo) //, out int vertexNum, out int indicesNum //out SC_VR_Chunk.DInstanceType arrayOfInstancePos
        {







            map = new int[tinyChunkWidth * tinyChunkHeight * tinyChunkDepth];





            int[][] arrayOfDeMapTemp = new int[8][];
            arrayOfDeVectorMapTemp = new Vector4(9.0f, 9.0f, 9.0f, 9.0f);
            arrayOfDeVectorMapTempTwo = new Vector4(9.0f, 9.0f, 9.0f, 9.0f);

            int someCount = 0;
            int counterOne = 0;
            int counterTwo = 0;
            int counterThree = 0;
            int counterFour = 0;
      
            for (int i = 0; i < arrayOfDeMapTemp.Length; i++)
            {
                arrayOfDeMapTemp[i] = new int[8];
            }




            int countOne = 0;
            int countTwo = 0;
            int countThree = 0;
            int countFour = 0;

            float tempX = 0;
            float tempY = 0;
            float tempZ = 0;
            float tempW = 0;

            float tempOriX = 0;
            float tempOriY = 0;
            float tempOriZ = 0;
            float tempOriW = 0;

            float tempXX = 0;
            float tempYY = 0;
            float tempZZ = 0;
            float tempWW = 0;






            FastNoise fastNoise = new FastNoise();

            counterVertexTop = 0;
            //arrayOfVerts = null;
            //positions = new Vector4[counterVertexTop * 4 + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4];
            //normals = null;
            //textureCoordinates = null;
            //triangleIndices = null;



            for (int x = 0; x < tinyChunkWidth; x++)
            {
                for (int y = 0; y < tinyChunkHeight; y++)
                {
                    for (int z = 0; z < tinyChunkDepth; z++)
                    {
                        float noiseXZ = 20;

                        //float noiseXZ = 10;
                        //noiseXZ *= SimplexNoise.Noise((((x * planeSize) + currentPosition.X + seed) / _detailScale) * _tinyChunkHeightScale, (((z * planeSize) + currentPosition.Z + seed) / _detailScale) * _tinyChunkHeightScale);
                        //noiseXZ *= SimplexNoise.SeamlessNoise((((x * planeSize) + currentPosition.X + seed) / _detailScale) * _tinyChunkHeightScale, (((z * planeSize) + currentPosition.Z + seed) / _detailScale) * _tinyChunkHeightScale,16, 16, 0);
                        //noiseXZ = (noiseXZ + 1.0f) * 0.5f;
                        //float size0 = (1 / planeSize) * currentPosition.Y;
                        //noiseXZ -= size0;

                        //noise = (noise + 1.0f) * 0.5f;
                        //noiseXZ = SimplexNoise.Noise(x + currentPosition.X, z + currentPosition.Z);

                        //float size0 = (1 / planeSize) * currentPosition.Y;
                        //noise -= size0;
                        //Console.WriteLine(noiseXZ + " y: " + y);
                        //noiseXZ *= SimplexNoise.SeamlessNoise(x + currentPosition.X, z + currentPosition.Z, 16, 16, 0);
                        //noiseXZ *= SimplexNoise.Noise((((x * planeSize) + currentPosition.X + seed) / _detailScale) * _tinyChunkHeightScale, (((z * planeSize) + currentPosition.Z + seed) / _detailScale) * _tinyChunkHeightScale);
                        //noiseXZ *= SimplexNoise.SeamlessNoise((((x * planeSize) + currentPosition.X + seed) / _detailScale) * _tinyChunkHeightScale, (((z * planeSize) + currentPosition.Z + seed) / _detailScale) * _tinyChunkHeightScale,16, 16, 0);
                        //noiseXZ *= SimplexNoise.Noise.Generate((((x * planeSize) + currentPosition.X + seed) / _detailScale) * _tinyChunkHeightScale, (((z * planeSize) + currentPosition.Z + seed) / _detailScale) * _tinyChunkHeightScale);
                        //noiseXZ *= SimplexNoise.Noise.Generate((((x * planeSize) + currentPosition.X + seed) / _detailScale) * _tinyChunkHeightScale, (((y * planeSize) + currentPosition.Y + seed) / _detailScale) * _tinyChunkHeightScale, (((z * planeSize) + currentPosition.Z + seed) / _detailScale) * _tinyChunkHeightScale);

                        //noiseXZ *= SimplexNoise.Noise.Generate((((x * planeSize) + currentPosition.X) / _detailScale) * _tinyChunkHeightScale, (((z * planeSize) + currentPosition.Z) / _detailScale) * _tinyChunkHeightScale);

                        //noiseXZ *= SimplexNoise.Noise.Generate(x + currentPosition.X, z + currentPosition.Z);

                        //float a = (x * planeSize) + currentPosition.X;
                        //float b = (z * planeSize) + currentPosition.Z;
                        //noiseXZ *= SimplexNoise.Noise.Generate(a, b);
                        //noiseXZ *= SimplexNoise.Noise.Generate(noiseXZ);
                        //float size0 = (1 / planeSize) * currentPosition.Y;
                        //noiseXZ -= size0;

                        //noiseXZ *= fastNoise.GetNoise((((x * planeSize) + currentPosition.X + seed) / _detailScale) * _tinyChunkHeightScale,(((z * planeSize) + currentPosition.Z + seed) / _detailScale) * _tinyChunkHeightScale);
                        //noiseXZ = (noiseXZ + 10) * 0.5f;

                        //noiseXZ *= fastNoise.GetNoise((x * planeSize) + currentPosition.X, (z * planeSize) + currentPosition.Z);
                        //noiseXZ = (noiseXZ + 5) * 0.5f;

                        //noiseXZ *= fastNoise.GetNoise((x * planeSize) + currentPosition.X, (y * planeSize) + currentPosition.Y, (z * planeSize) + currentPosition.Z);
                        //noiseXZ = (noiseXZ + 1.0f) * 0.5f;

                        //noiseXZ *= SimplexNoise.Noise.Generate(x,y);
                        //noiseXZ = (noiseXZ + 1.0f) * 0.5f;
                        //noiseXZ *= SimplexNoise.Noise.Generate((x * planeSize) + currentPosition.X, (z * planeSize) + currentPosition.Z);
                        //noiseXZ = (noiseXZ + 1.0f) * 0.5f;
                        //Console.WriteLine(noiseXZ + " y: " + y);
                        noiseXZ *= fastNoise.GetNoise((((x * planeSize) + currentPosition.X + seed) / _detailScale) * _tinyChunkHeightScale, (((y * planeSize) + currentPosition.Y + seed) / _detailScale) * _tinyChunkHeightScale, (((z * planeSize) + currentPosition.Z + seed) / _detailScale) * _tinyChunkHeightScale);

                        if (noiseXZ >= 0.1f) //|| (int)Math.Round(noiseXZ) < -y
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

                        /*int index = x + tinyChunkWidth * (y + tinyChunkHeight * z);
                        int index0;
                        if (index >= 0 && index <= 15)
                        {
                            arrayOfDeMapTemp[0][index] = index;
                            counterOne++;
                        }
                        else if (index >= 16 && index <= 31)
                        {
                            index0 = 31 - index;
                            arrayOfDeMapTemp[1][index0] = index;
                            counterTwo++;
                        }
                        else if (index >= 32 && index <= 47)
                        {
                            index0 = 47 - index;
                            arrayOfDeMapTemp[2][index0] = index;
                            counterThree++;
                        }
                        else if (index >= 48 && index <= 63)
                        {
                            index0 = 63 - index;
                            arrayOfDeMapTemp[3][index0] = index;
                            counterFour++;
                        }*/

                        /*int index = x + tinyChunkWidth * (y + tinyChunkHeight * z);
                        int index0;
                        int currentByte = map[x + tinyChunkWidth * (y + tinyChunkHeight * z)];

                        if (index >= 0 && index <= 7)
                        {
                            index0 = 7 - index;
                            arrayOfDeMapTemp[0][index0] = index;
                        }
                        else if (index >= 8 && index <= 15)
                        {
                            index0 = 15 - index;
                            index0 = 7 - index0;
                            arrayOfDeMapTemp[1][index0] = index;
                        }
                        else if (index >= 16 && index <= 23)
                        {
                            index0 = 23 - index;
                            index0 = 7 - index0;
                            arrayOfDeMapTemp[2][index0] = index;
                        }
                        else if (index >= 24 && index <= 31)
                        {
                            index0 = 31 - index;
                            index0 = 7 - index0;
                            arrayOfDeMapTemp[3][index0] = index;
                        }
                        else if (index >= 32 && index <= 39)
                        {
                            index0 = 39 - index;
                            index0 = 7 - index0;
                            arrayOfDeMapTemp[4][index0] = index;
                        }
                        else if (index >= 40 && index <= 47)
                        {
                            index0 = 47 - index;
                            index0 = 7 - index0;
                            arrayOfDeMapTemp[5][index0] = index;
                        }
                        else if (index >= 48 && index <= 55)
                        {
                            index0 = 55 - index;
                            index0 = 7 - index0;
                            arrayOfDeMapTemp[6][index0] = index;
                        }
                        else if (index >= 56 && index <= 63)
                        {
                            index0 = 63 - index;
                            index0 = 7 - index0;
                            arrayOfDeMapTemp[7][index0] = index;
                        }*/



                        /*int index = x + tinyChunkWidth * (y + tinyChunkHeight * z);
                        //int index0;
                        int currentByte = map[x + tinyChunkWidth * (y + tinyChunkHeight * z)];

                        if (index >= 0 && index <= 7)
                        {
                            if (currentByte == 0)
                            {
                                arrayOfDeVectorMapTemp.X = (arrayOfDeVectorMapTemp.X * 10f);
                            }
                            else
                            {
                                arrayOfDeVectorMapTemp.X = (arrayOfDeVectorMapTemp.X * 10f) + 1;
                            }
                        }
                        else if (index >= 8 && index <= 15)
                        {
                            //index0 = 15 - index;
                            //index0 = 7 - index0;
                            if (currentByte == 0)
                            {
                                arrayOfDeVectorMapTempTwo.X = (arrayOfDeVectorMapTempTwo.X * 10f);
                            }
                            else
                            {
                                arrayOfDeVectorMapTempTwo.X = (arrayOfDeVectorMapTempTwo.X * 10f) + 1;
                            }
                        }
                        else if (index >= 16 && index <= 23)
                        {
                            //index0 = 23 - index;
                            //index0 = 8 - index0;
                            if (currentByte == 0)
                            {
                                arrayOfDeVectorMapTemp.Y = (arrayOfDeVectorMapTemp.Y * 10f);
                            }
                            else
                            {
                                arrayOfDeVectorMapTemp.Y = (arrayOfDeVectorMapTemp.Y * 10f) + 1;
                            }
                        }
                        else if (index >= 24 && index <= 31)
                        {
                            //index0 = 31 - index;
                            //index0 = 8 - index0;
                            if (currentByte == 0)
                            {
                                arrayOfDeVectorMapTempTwo.Y = (arrayOfDeVectorMapTempTwo.Y * 10f);
                            }
                            else
                            {
                                arrayOfDeVectorMapTempTwo.Y = (arrayOfDeVectorMapTempTwo.Y * 10f) + 1;
                            }
                        }
                        else if (index >= 32 && index <= 39)
                        {
                            //index0 = 39 - index;
                            //index0 = 8 - index0;
                            if (currentByte == 0)
                            {
                                arrayOfDeVectorMapTemp.Z = (arrayOfDeVectorMapTemp.Z * 10f);
                            }
                            else
                            {
                                arrayOfDeVectorMapTemp.Z = (arrayOfDeVectorMapTemp.Z * 10f) + 1;
                            }
                        }
                        else if (index >= 40 && index <= 47)
                        {
                            //index0 = 47 - index;
                            //index0 = 8 - index0;
                            if (currentByte == 0)
                            {
                                arrayOfDeVectorMapTempTwo.Z = (arrayOfDeVectorMapTempTwo.Z * 10f);
                            }
                            else
                            {
                                arrayOfDeVectorMapTempTwo.Z = (arrayOfDeVectorMapTempTwo.Z * 10f) + 1;
                            }
                        }
                        else if (index >= 48 && index <= 55)
                        {
                            //index0 = 55 - index;
                            //index0 = 8 - index0;
                            if (currentByte == 0)
                            {
                                arrayOfDeVectorMapTemp.W = (arrayOfDeVectorMapTemp.W * 10f);
                            }
                            else
                            {
                                arrayOfDeVectorMapTemp.W = (arrayOfDeVectorMapTemp.W * 10f) + 1;
                            }
                        }
                        else if (index >= 56 && index <= 63)
                        {
                            //Console.WriteLine(index);
                            //index0 = 63 - index;
                            //index0 = 8 - index0;
                            if (currentByte == 0)
                            {
                                arrayOfDeVectorMapTempTwo.W = (arrayOfDeVectorMapTempTwo.W * 10f);
                            }
                            else
                            {
                                arrayOfDeVectorMapTempTwo.W = (arrayOfDeVectorMapTempTwo.W * 10f) + 1;
                            }
                        }*/
                        /*int currentByte = map[x + tinyChunkWidth * (y + tinyChunkHeight * z)];
                        if (y == 0)
                        {
                            if (countOne <= 7)
                            {
                                if (currentByte == 0)
                                {
                                    arrayOfDeVectorMapTemp.X = (arrayOfDeVectorMapTemp.X * 10f);
                                }
                                else
                                {
                                    arrayOfDeVectorMapTemp.X = (arrayOfDeVectorMapTemp.X * 10f) + 1;
                                }
                            }
                            else
                            {
                                if (currentByte == 0)
                                {
                                    arrayOfDeVectorMapTempTwo.X = (arrayOfDeVectorMapTempTwo.X * 10f);
                                }
                                else
                                {
                                    arrayOfDeVectorMapTempTwo.X = (arrayOfDeVectorMapTempTwo.X * 10f) + 1;
                                }
                            }

                            countOne++;
                        }
                        else if (y == 1)
                        {
                            if (countTwo <= 7)
                            {
                                if (currentByte == 0)
                                {
                                    arrayOfDeVectorMapTemp.Y = (arrayOfDeVectorMapTemp.Y * 10f);
                                }
                                else
                                {
                                    arrayOfDeVectorMapTemp.Y = (arrayOfDeVectorMapTemp.Y * 10f) + 1;
                                }
                                //Console.WriteLine(arrayOfDeVectorMapTemp.Y);
                            }
                            else
                            {
                                if (currentByte == 0)
                                {
                                    arrayOfDeVectorMapTempTwo.Y = (arrayOfDeVectorMapTempTwo.Y * 10f);
                                }
                                else
                                {
                                    arrayOfDeVectorMapTempTwo.Y = (arrayOfDeVectorMapTempTwo.Y * 10f) + 1;
                                }
                            }
                            countTwo++;
                        }
                        else if (y == 2)
                        {
                            if (countThree <= 7)
                            {
                                if (currentByte == 0)
                                {
                                    arrayOfDeVectorMapTemp.Z = (arrayOfDeVectorMapTemp.Z * 10f);
                                }
                                else
                                {
                                    arrayOfDeVectorMapTemp.Z = (arrayOfDeVectorMapTemp.Z * 10f) + 1;
                                }
                            }
                            else
                            {
                                if (currentByte == 0)
                                {
                                    arrayOfDeVectorMapTempTwo.Z = (arrayOfDeVectorMapTempTwo.Z * 10f);
                                }
                                else
                                {
                                    arrayOfDeVectorMapTempTwo.Z = (arrayOfDeVectorMapTempTwo.Z * 10f) + 1;
                                }
                            }
                            countThree++;
                        }
                        else if (y == 3)
                        {
                            if (countFour <= 7)
                            {
                                if (currentByte == 0)
                                {
                                    arrayOfDeVectorMapTemp.W = (arrayOfDeVectorMapTemp.W * 10f);
                                }
                                else
                                {
                                    arrayOfDeVectorMapTemp.W = (arrayOfDeVectorMapTemp.W * 10f) + 1;
                                }
                            }
                            else
                            {
                                if (currentByte == 0)
                                {
                                    arrayOfDeVectorMapTempTwo.W = (arrayOfDeVectorMapTempTwo.W * 10f);
                                }
                                else
                                {
                                    arrayOfDeVectorMapTempTwo.W = (arrayOfDeVectorMapTempTwo.W * 10f) + 1;
                                }
                            }
                            countFour++;
                        }*/
                    }
                }
            }

            //Console.WriteLine(counterOne + " " + counterTwo + " " + counterThree + " " + counterFour);

            /*int switch1 = 1;
            int counter1 = 0;
            int mainCounter = 0;
            int counterer = 0;
            int yIndex = 0;

            floatResults = new float[tinyChunkHeight];

            for (int i = 0; i < floatResults.Length; i++)
            {
                floatResults[i] = 0.9f;
            }*/

            /*int[][] arrayOfDeMapTempSorted = new int[8][];
            int[][] tempArray = arrayOfDeMapTemp;
            for (int i = 0; i < arrayOfDeMapTempSorted.Length; i++)
            {
                arrayOfDeMapTempSorted[i] = tempArray[i].OrderBy(x=>x).ToArray();// new int[map.Length / tinyChunkHeight];      
            }*/


            /*for (int i = 0; i < arrayOfDeMapTempSorted.Length; i++)
            {
                for (int j = 0; j < arrayOfDeMapTempSorted[i].Length; j++)
                {
                    Console.WriteLine(arrayOfDeMapTempSorted[i][j]);
                }
            }*/

            /*for (int i = 0;i < arrayOfDeVectorMapTemp.Length;i++)
            {
                arrayOfDeVectorMapTemp[i].X = 1;
                arrayOfDeVectorMapTemp[i].Y = 1;
                arrayOfDeVectorMapTemp[i].Z = 1;
                arrayOfDeVectorMapTemp[i].W = 1;
            }*/





            for (int i = 0; i < map.Length; i++)
            {
                int currentByte = map[i];

                if (i >= 0 && i <= 7)
                {
                    if (currentByte == 0)
                    {
                        arrayOfDeVectorMapTemp.X = (arrayOfDeVectorMapTemp.X * 10f);
                    }
                    else
                    {
                        arrayOfDeVectorMapTemp.X = (arrayOfDeVectorMapTemp.X * 10f) + 1;
                    }
                }
                else if(i >= 8 && i <= 15)
                {
                    if (currentByte == 0)
                    {
                        arrayOfDeVectorMapTempTwo.X = (arrayOfDeVectorMapTempTwo.X * 10f);
                    }
                    else
                    {
                        arrayOfDeVectorMapTempTwo.X = (arrayOfDeVectorMapTempTwo.X * 10f) + 1;
                    }
                }


                else if (i >= 16 && i <= 23)
                {
                    if (currentByte == 0)
                    {
                        arrayOfDeVectorMapTemp.Y = (arrayOfDeVectorMapTemp.Y * 10f);
                    }
                    else
                    {
                        arrayOfDeVectorMapTemp.Y = (arrayOfDeVectorMapTemp.Y * 10f) + 1;
                    }
                    //Console.WriteLine(arrayOfDeVectorMapTemp.Y);
                }
                else if (i >= 24 && i <= 31)
                {
                    if (currentByte == 0)
                    {
                        arrayOfDeVectorMapTempTwo.Y = (arrayOfDeVectorMapTempTwo.Y * 10f);
                    }
                    else
                    {
                        arrayOfDeVectorMapTempTwo.Y = (arrayOfDeVectorMapTempTwo.Y * 10f) + 1;
                    }
                }

                else if (i >= 32 && i <= 39)
                {
                    if (currentByte == 0)
                    {
                        arrayOfDeVectorMapTemp.Z = (arrayOfDeVectorMapTemp.Z * 10f);
                    }
                    else
                    {
                        arrayOfDeVectorMapTemp.Z = (arrayOfDeVectorMapTemp.Z * 10f) + 1;
                    }
                }
                else if (i >= 40 && i <= 47)
                {
                    if (currentByte == 0)
                    {
                        arrayOfDeVectorMapTempTwo.Z = (arrayOfDeVectorMapTempTwo.Z * 10f);
                    }
                    else
                    {
                        arrayOfDeVectorMapTempTwo.Z = (arrayOfDeVectorMapTempTwo.Z * 10f) + 1;
                    }
                }

                else if (i >= 48 && i <= 55)
                {
                    if (currentByte == 0)
                    {
                        arrayOfDeVectorMapTemp.W = (arrayOfDeVectorMapTemp.W * 10f);
                    }
                    else
                    {
                        arrayOfDeVectorMapTemp.W = (arrayOfDeVectorMapTemp.W * 10f) + 1;
                    }
                }
                else if (i >= 56 && i <= 63)
                {
                    if (currentByte == 0)
                    {
                        arrayOfDeVectorMapTempTwo.W = (arrayOfDeVectorMapTempTwo.W * 10f);
                    }
                    else
                    {
                        arrayOfDeVectorMapTempTwo.W = (arrayOfDeVectorMapTempTwo.W * 10f) + 1;
                    }
                }

            }








            /*for (int i = 0; i < arrayOfDeMapTemp.Length; i++)
            {
                for (int j = 0; j < arrayOfDeMapTemp[i].Length; j++)
                {
                    int currentByte = map[arrayOfDeMapTemp[i][j]];

                    if (i == 0)
                    {
                        if (countOne <= 7)
                        {
                            if (currentByte == 0)
                            {
                                arrayOfDeVectorMapTemp.X = (arrayOfDeVectorMapTemp.X * 10f);
                            }
                            else
                            {
                                arrayOfDeVectorMapTemp.X = (arrayOfDeVectorMapTemp.X * 10f) + 1;
                            }
                        }
                        else
                        {
                            if (currentByte == 0)
                            {
                                arrayOfDeVectorMapTempTwo.X = (arrayOfDeVectorMapTempTwo.X * 10f);
                            }
                            else
                            {
                                arrayOfDeVectorMapTempTwo.X = (arrayOfDeVectorMapTempTwo.X * 10f) + 1;
                            }
                        }

                        countOne++;
                    }
                    else if (i == 1)
                    {
                        if (countTwo <= 7)
                        {
                            if (currentByte == 0)
                            {
                                arrayOfDeVectorMapTemp.Y = (arrayOfDeVectorMapTemp.Y * 10f);
                            }
                            else
                            {
                                arrayOfDeVectorMapTemp.Y = (arrayOfDeVectorMapTemp.Y * 10f) + 1;
                            }
                            //Console.WriteLine(arrayOfDeVectorMapTemp.Y);
                        }
                        else
                        {
                            if (currentByte == 0)
                            {
                                arrayOfDeVectorMapTempTwo.Y = (arrayOfDeVectorMapTempTwo.Y * 10f);
                            }
                            else
                            {
                                arrayOfDeVectorMapTempTwo.Y = (arrayOfDeVectorMapTempTwo.Y * 10f) + 1;
                            }
                        }
                        countTwo++;
                    }
                    else if (i == 2)
                    {
                        if (countThree <= 7)
                        {
                            if (currentByte == 0)
                            {
                                arrayOfDeVectorMapTemp.Z = (arrayOfDeVectorMapTemp.Z * 10f);
                            }
                            else
                            {
                                arrayOfDeVectorMapTemp.Z = (arrayOfDeVectorMapTemp.Z * 10f) + 1;
                            }
                        }
                        else
                        {
                            if (currentByte == 0)
                            {
                                arrayOfDeVectorMapTempTwo.Z = (arrayOfDeVectorMapTempTwo.Z * 10f);
                            }
                            else
                            {
                                arrayOfDeVectorMapTempTwo.Z = (arrayOfDeVectorMapTempTwo.Z * 10f) + 1;
                            }
                        }
                        countThree++;
                    }
                    else if (i == 3)
                    {
                        if (countFour <= 7)
                        {
                            if (currentByte == 0)
                            {
                                arrayOfDeVectorMapTemp.W = (arrayOfDeVectorMapTemp.W * 10f);
                            }
                            else
                            {
                                arrayOfDeVectorMapTemp.W = (arrayOfDeVectorMapTemp.W * 10f) + 1;
                            }
                        }
                        else
                        {
                            if (currentByte == 0)
                            {
                                arrayOfDeVectorMapTempTwo.W = (arrayOfDeVectorMapTempTwo.W * 10f);
                            }
                            else
                            {
                                arrayOfDeVectorMapTempTwo.W = (arrayOfDeVectorMapTempTwo.W * 10f) + 1;
                            }
                        }
                        countFour++;
                    }
                }
            }*/

            //Console.WriteLine(countOne + " " + countTwo + " " + countThree + " " + countFour);


            //Console.WriteLine(arrayOfDeVectorMapTemp);
            //Console.WriteLine(arrayOfDeVectorMapTempTwo);































            /*for (int i = 0; i < arrayOfDeMapTemp.Length; i++)
            {            
                for (int j = 0; j < arrayOfDeMapTemp[i].Length; j++)
                {
                    int currentByte = map[arrayOfDeMapTemp[i][j]];

                    if (i == 0)
                    {
                        if (countOne <= 7)
                        {
                            if (currentByte == 0)
                            {
                                tempOriX = arrayOfDeVectorMapTemp.X; //911011.10119
                                tempX = (float)(Math.Round(arrayOfDeVectorMapTemp.X)); //911011
                                tempXX = tempOriX - tempX; // = 0.10119
                                arrayOfDeVectorMapTemp.X = (float)((tempX * 10) + tempXX); // 9110110.10119
                            }
                            else
                            {
                                tempOriX = arrayOfDeVectorMapTemp.X;  //911011.10119
                                tempX = (float)(Math.Round(arrayOfDeVectorMapTemp.X));  //911011
                                tempXX = tempOriX - tempX; // 0.10119
                                arrayOfDeVectorMapTemp.X = (float)((tempX * 10) + tempXX + 1); //  9110111.10119
                            }
                        }
                        else
                        {
                            if (currentByte == 0)
                            {
                                tempOriX = arrayOfDeVectorMapTemp.X; //911011.10119
                                tempX = (float)(Math.Round(arrayOfDeVectorMapTemp.X)); //911011
                                tempXX = tempOriX - tempX; //0.10119
                                arrayOfDeVectorMapTemp.X = (float)(((tempXX * 0.1f)) + tempX); // 0.010119 + 911011 = 911011.010119
                            }
                            else
                            {
                                tempOriX = arrayOfDeVectorMapTemp.X; //911011.10119
                                tempX = (float)(Math.Round(arrayOfDeVectorMapTemp.X)); // //911011.
                                tempXX = tempOriX - tempX; // 0.10119
                                arrayOfDeVectorMapTemp.X = (float)(((tempXX +1)*0.1f) + tempX); // 1.10119 = 0.110119 + 911011 = 911011.110119
                            }
                        }
                        
                        countOne++;
                    }
                    else if (i == 1)
                    {
                        if (countTwo <= 7)
                        {
                            if (currentByte == 0)
                            {
                                tempOriY = arrayOfDeVectorMapTemp.Y;
                                tempY = (float)(Math.Round(arrayOfDeVectorMapTemp.Y));
                                tempYY = tempOriY - tempY;
                                arrayOfDeVectorMapTemp.Y = (float)((tempY * 10) + tempYY);
                            }
                            else
                            {
                                tempOriY = arrayOfDeVectorMapTemp.Y;
                                tempY = (float)(Math.Round(arrayOfDeVectorMapTemp.Y));
                                tempYY = tempOriY - tempY;
                                arrayOfDeVectorMapTemp.Y = (float)((tempY * 10) + tempYY + 1);
                            }
                        }
                        else
                        {
                            if (currentByte == 0)
                            {
                                tempOriY = arrayOfDeVectorMapTemp.Y;
                                tempY = (float)(Math.Round(arrayOfDeVectorMapTemp.Y));
                                tempYY = tempOriY - tempY;
                                arrayOfDeVectorMapTemp.Y = (float)(((tempYY * 0.1f)) + tempY);
                            }
                            else
                            {
                                tempOriY = arrayOfDeVectorMapTemp.Y;
                                tempY = (float)(Math.Round(arrayOfDeVectorMapTemp.Y));
                                tempYY = tempOriY - tempY;
                                arrayOfDeVectorMapTemp.Y = (float)(((tempYY + 1)*0.1f) + tempY);
                            }
                        }
                        countTwo++;
                    }
                    else if (i == 2)
                    {
                        if (countThree <= 7)
                        {
                            if (currentByte == 0)
                            {
                                tempOriZ = arrayOfDeVectorMapTemp.Z;
                                tempZ = (float)(Math.Round(arrayOfDeVectorMapTemp.Z));
                                tempZZ = tempOriZ - tempZ;
                                arrayOfDeVectorMapTemp.Z = (float)((tempZ * 10) + tempZZ);
                            }
                            else
                            {
                                tempOriZ = arrayOfDeVectorMapTemp.Z;
                                tempZ = (float)(Math.Round(arrayOfDeVectorMapTemp.Z));
                                tempZZ = tempOriZ - tempZ;
                                arrayOfDeVectorMapTemp.Z = (float)((tempZ * 10) + tempZZ + 1);
                            }
                        }
                        else
                        {
                            if (currentByte == 0)
                            {
                                tempOriZ = arrayOfDeVectorMapTemp.Z;
                                tempZ = (float)(Math.Round(arrayOfDeVectorMapTemp.Z));
                                tempZZ = tempOriZ - tempZ;
                                arrayOfDeVectorMapTemp.Z = (float)(((tempZZ * 0.1f)) + tempZ);
                            }
                            else
                            {
                                tempOriZ = arrayOfDeVectorMapTemp.Z;
                                tempZ = (float)(Math.Round(arrayOfDeVectorMapTemp.Z));
                                tempZZ = tempOriZ - tempZ;
                                arrayOfDeVectorMapTemp.Z = (float)(((tempZZ + 1) * 0.1f) + tempZ);
                            }
                        }
                        countThree++;
                    }
                    else if (i == 3)
                    {
                        if (countFour <= 7)
                        {
                            if (currentByte == 0)
                            {
                                tempOriW = (float)(arrayOfDeVectorMapTemp.W);
                                tempW = (float)(Math.Round(arrayOfDeVectorMapTemp.W));
                                tempWW = (float)(tempOriW - tempW);
                                arrayOfDeVectorMapTemp.W = (float)((tempW * 10) + tempWW);
                            }
                            else
                            {

                                tempOriW = (float)(arrayOfDeVectorMapTemp.W);
                                tempW = (float)(Math.Round(arrayOfDeVectorMapTemp.W));
                                tempWW = (float)(tempOriW - tempW);
                                arrayOfDeVectorMapTemp.W = (float)((tempW * 10) + tempWW + 1);
                            }
                        }
                        else
                        {
                            if (currentByte == 0)
                            {
                                tempOriW = (float)(arrayOfDeVectorMapTemp.W);
                                tempW = (float)(Math.Round(arrayOfDeVectorMapTemp.W));
                                tempWW = (float)(tempOriW - tempW);
                                arrayOfDeVectorMapTemp.W = (float)(((tempWW * 0.1f)) + tempW);
                            }
                            else
                            {
                                tempOriW = (float)arrayOfDeVectorMapTemp.W;
                                tempW = (float)(Math.Round(arrayOfDeVectorMapTemp.W));
                                tempWW = (float)(tempOriW - tempW);
                                arrayOfDeVectorMapTemp.W = (float)(((tempWW + 1) * 0.1f) + tempW);
                            }
                        }
                        countFour++;
                    }
                }
            }*/








































            //Console.WriteLine(countFour);



            //decimal d = Decimal.Parse("1.2345E-02", System.Globalization.NumberStyles.Float);
            //decimal.Negate(arrayOfDeVectorMapTemp);
            //string reversedStr = new string(arrayOfDeVectno imorMapTemp.ToString().Reverse().ToArray());




            /*for (int i = 0; i < 1; i++)
            {
                arrayOfDeVectorMapTemp *= 0.0000000000000001f;
            }*/


            //Console.WriteLine(arrayOfDeVectorMapTemp);




































































            // + counterVertexBottom * 6 + counterVertexRight * 6 + counterVertexLeft * 6 + counterVertexFront * 6 + counterVertexBack * 6
            // * 4 + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4
            // + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4

            //arrayOfVerts = new SC_VR_Chunk.DVertex[counterVertexTop * 4 + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4];

            //positions = new Vector4[counterVertexTop * 4 + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4];
            //normals = new Vector3[counterVertexTop * 4 + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4];
            //textureCoordinates = new Vector2[counterVertexTop * 4 + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4];
            //triangleIndices = new int[counterVertexTop * 6 + counterVertexBottom * 6 + counterVertexRight * 6 + counterVertexLeft * 6 + counterVertexFront * 6 + counterVertexBack * 6];

            //arrayOfVerts = new SC_VR_Chunk.DVertex[counterVertexTop * 4];  //new SC_VR_Chunk.DVertex[counterVertexTop * 4];  

            //positions = new Vector4[counterVertexTop*4];
            //arrayOfInstancePos = new SC_VR_Chunk.DInstanceType;
            //normals = new Vector3[counterVertexTop * 4];
            //textureCoordinates = new Vector2[counterVertexTop * 4];
            //triangleIndices =  new int[counterVertexTop * 6]; //new int[counterVertexTop * 6]; //

            //Regenerate(currentPosition);


            /*arrayOfVerts = new SC_VR_Chunk.DVertex[listOfVerts.Count];
            for (int i = 0; i < listOfVerts.Count; i++)
            {
            arrayOfVerts[i] = listOfVerts[i];
            }

            triangleIndices = new int[listOfTriangleIndices.Count];
            for (int i = 0; i < listOfTriangleIndices.Count; i++)
            {
            triangleIndices[i] = listOfTriangleIndices[i];
            }*/




            //Console.WriteLine(arrayOfVerts.Length);
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




            //for (int i = 0;i < listOfVerts.Count;i++)
            //{
            //    listOfInstancePos.Add(listOfVerts[i].position);
            //}


            //arrayOfInstancePos.position = listOfInstancePos.ToArray();






            //arrayOfInstancePos = listOfInstancePos.ToArray();
            //vertexArray = positions;
            //indicesArray = triangleIndices;

            //norms = normals;
            //tex = textureCoordinates;
            //dVertexArray = arrayOfVerts;





            //vertexArray = positions;
            //triangleArray = triangleIndices;


            //arrayOfVertz = arrayOfVerts;
            /*if (arrayOfVerts.Length<=0)
            {
                arrayOfVertz = null;
            }
            else
            {
          
            }*/
            //return map;

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
            //BOTTOMFACE
            if (IsTransparent(x, y - 1, z))
            {
                counterVertexTop++;
                //counterVertexBottom += 1;
            }
            //TOPFACE
            if (IsTransparent(x, y + 1, z))
            {
                counterVertexTop++;
                //counterVertexTop += 1;
            }

            //LEFTFACE
            if (IsTransparent(x - 1, y, z))
            {
                counterVertexTop++;
                //counterVertexLeft += 1;
            }

            //RIGHTFACE
            if (IsTransparent(x + 1, y, z))
            {
                counterVertexTop++;
                //counterVertexRight += 1;
            }

            ///FRONTFACE
            if (IsTransparent(x, y, z - 1))
            {
                counterVertexTop++;
                //counterVertexFront += 1;
            }
            //BACKFACE
            if (IsTransparent(x, y, z + 1))
            {
                counterVertexTop++;
                //counterVertexBack += 1;
            }
        }

        int tester = 0;
        public void Regenerate(Vector4 currentPosition)
        {
            for (int x = 0; x < tinyChunkWidth; x++)
            {
                for (int y = 0; y < tinyChunkHeight; y++)
                {
                    for (int z = 0; z < tinyChunkDepth; z++)
                    {
                        block = map[x + tinyChunkWidth * (y + tinyChunkHeight * z)];

                        if (block == 0) continue;
                        {
                            DrawBrick(x, y, z, currentPosition);
                        }
                    }
                }
            }
        }

        //chunkPosBig chunkbig;

        public void DrawBrick(int x, int y, int z, Vector4 currentPosition)
        {

            Vector4 start = new Vector4(x * planeSize, y * planeSize, z * planeSize, 0);
            Vector4 offset1, offset2;

            /*if (IsTransparent(x, y - 1, z))
            {
                offset1 = left * planeSize;
                offset2 = back * planeSize;
                createBottomFace(start + right * planeSize, offset1, offset2);
            }
            if (IsTransparent(x, y + 1, z))
            {
                offset1 = right * planeSize;
                offset2 = back * planeSize;
                createTopFace(start + up * planeSize, offset1, offset2);
            }

            if (IsTransparent(x - 1, y, z))
            {
                offset1 = up* planeSize;
                offset2 = back* planeSize;
                createRightFace(start, offset1, offset2);
            }

            if (IsTransparent(x + 1, y, z))
            {
                offset1 = down* planeSize;
                offset2 = back* planeSize;
                createleftFace(start + right* planeSize + up* planeSize, offset1, offset2);
            }

            if (IsTransparent(x, y, z - 1))
            {
                offset1 = left* planeSize;
                offset2 = up* planeSize;
                createBackFace(start + right* planeSize + back* planeSize, offset1, offset2);
            }

            if (IsTransparent(x, y, z + 1))
            {
                offset1 = right* planeSize;
                offset2 = up* planeSize;
                createFrontFace(start, offset1, offset2);
            }*/
            /*//BOTTOMFACE
            if (IsTransparent(x, y - 1, z))
            {
                offset1 = right * planeSize;
                offset2 = forward * planeSize;
                createBottomFace(start, offset1, offset2, currentPosition);
                vertzIndex += 4;
                trigsIndex += 6;
            }*/
            //TOPFACE
            if (IsTransparent(x, y + 1, z))
            {
                offset1 = forward * planeSize;
                offset2 = right * planeSize;
                createTopFace(start + (up * planeSize), offset1, offset2, currentPosition);
                vertzIndex += 4;
                trigsIndex += 6;
            }
            //LEFTFACE
            if (IsTransparent(x - 1, y, z))
            {
                offset1 = back * planeSize;
                offset2 = down * planeSize;
                createleftFace(start + (up * planeSize) + (forward * planeSize), offset1, offset2, currentPosition);
                vertzIndex += 4;
                trigsIndex += 6;
            }
            ///RIGHTFACE
            if (IsTransparent(x + 1, y, z))
            {
                offset1 = up * planeSize;
                offset2 = forward * planeSize;
                createRightFace(start + (right * planeSize), offset1, offset2, currentPosition);
                vertzIndex += 4;
                trigsIndex += 6;
            }
            //LEFTFACE
            if (IsTransparent(x - 1, y, z))
            {
                offset1 = back * planeSize;
                offset2 = down * planeSize;
                createleftFace(start + (up * planeSize) + (forward * planeSize), offset1, offset2, currentPosition);
                vertzIndex += 4;
                trigsIndex += 6;
            }

            //FRONTFACE
            if (IsTransparent(x, y, z - 1))
            {
                offset1 = left * planeSize;
                offset2 = up * planeSize;
                createFrontFace(start + (right * planeSize), offset1, offset2, currentPosition);
                vertzIndex += 4;
                trigsIndex += 6;
            }
            //BACKFACE
            if (IsTransparent(x, y, z + 1))
            {
                offset1 = right * planeSize;
                offset2 = up * planeSize;
                createBackFace(start + (forward * planeSize), offset1, offset2, currentPosition);
                vertzIndex += 4;
                trigsIndex += 6;
            }
        }

        private void createTopFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition)
        {
            int index = listOfVerts.Count;

            //positions[0 + vertzIndex] = new Vector4(currentPosition.X,currentPosition.Y,currentPosition.Z,1);//start;
            //positions[1 + vertzIndex] = new Vector4(currentPosition.X, currentPosition.Y, currentPosition.Z, 1); ;// start + offset1;
            //positions[2 + vertzIndex] = new Vector4(currentPosition.X, currentPosition.Y, currentPosition.Z, 1); ;// start + offset2;
            //positions[3 + vertzIndex] = new Vector4(currentPosition.X, currentPosition.Y, currentPosition.Z, 1); ;// start + offset1 + offset2;

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(-1, 1, 0),
                //tex = new Vector2(1f, 1f),
                //instancePos = currentPosition,
            });

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset1,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(-1, 1, 0),
                //tex = new Vector2(1f, 1f),
                //instancePos = currentPosition,
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset2,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(-1, 1, 0),
                //tex = new Vector2(1f, 1f),
                //instancePos = currentPosition,
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset1 + offset2,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(-1, 1, 0),
                //tex = new Vector2(1f, 1f),
                //instancePos = currentPosition,
            });

            listOfInstancePos.Add(currentPosition);
            listOfInstancePos.Add(currentPosition);
            listOfInstancePos.Add(currentPosition);
            listOfInstancePos.Add(currentPosition);
            /*listOfInstancePos.Add(new SC_VR_Chunk.DInstanceType()
            {
                position = currentPosition,

            });
            listOfInstancePos.Add(new SC_VR_Chunk.DInstanceType()
            {
                position = currentPosition,

            });
            listOfInstancePos.Add(new SC_VR_Chunk.DInstanceType()
            {
                position = currentPosition,

            });
            listOfInstancePos.Add(new SC_VR_Chunk.DInstanceType()
            {
                position = currentPosition,

            });*/




            /*arrayOfVerts[vertzIndex  + 0] = new SC_VR_Chunk.DVertex()
            {
                position = start,
                color = new Vector4(0.25f, 0.25f, 0.25f,1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(1f, 1f),
            };
            arrayOfVerts[vertzIndex  + 1] = new SC_VR_Chunk.DVertex()
            {
                position = start + offset1,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(1f, 1f),
            };

            arrayOfVerts[vertzIndex  + 2] = new SC_VR_Chunk.DVertex()
            {
                position = start + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(1f, 1f),
            };

            arrayOfVerts[vertzIndex + 3 ] = new SC_VR_Chunk.DVertex()
            {
                position = start + offset1 + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(1f, 1f),
            };*/


            /*normals[tester * 4 + 0] = new Vector3(-1, 1, 0);
            normals[tester * 4 + 1] = new Vector3(-1, 1, 0);
            normals[tester * 4 + 2] = new Vector3(-1, 1, 0);
            normals[tester * 4 + 3] = new Vector3(-1, 1, 0);

            textureCoordinates[tester * 4 + 0] = new Vector2(1f, 1f);
            textureCoordinates[tester * 4 + 1] = new Vector2(1f, 1f);
            textureCoordinates[tester * 4 + 2] = new Vector2(1f, 1f);
            textureCoordinates[tester * 4 + 3] = new Vector2(1f, 1f);*/


            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);

            /*triangleIndices[trigsIndex + 0] = (vertzIndex) + 0;
            triangleIndices[trigsIndex + 1] = (vertzIndex) + 1;
            triangleIndices[trigsIndex + 2] = (vertzIndex) + 2;
            triangleIndices[trigsIndex + 3] = (vertzIndex) + 3;
            triangleIndices[trigsIndex + 4] = (vertzIndex) + 2;
            triangleIndices[trigsIndex + 5] = (vertzIndex) + 1;*/

        }



        private void createBottomFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition)
        {
            int index = listOfVerts.Count;
            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(0, 1, -1),
                //tex = new Vector2(0f, 1f),
                //instancePos = currentPosition,
            });

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset1,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(0, 1, -1),
                //tex = new Vector2(0f, 1f),
                //instancePos = currentPosition,
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset2,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(0, 1, -1),
                //tex = new Vector2(0f, 1f),
                //instancePos = currentPosition,
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset1 + offset2,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(0, 1, -1),
                //tex = new Vector2(0f, 1f),
                //instancePos = currentPosition,
            });


            listOfInstancePos.Add(currentPosition);
            listOfInstancePos.Add(currentPosition);
            listOfInstancePos.Add(currentPosition);
            listOfInstancePos.Add(currentPosition);
            /*listOfInstancePos.Add(new SC_VR_Chunk.DInstanceType()
            {
                position = currentPosition,

            });
            listOfInstancePos.Add(new SC_VR_Chunk.DInstanceType()
            {
                position = currentPosition,

            });
            listOfInstancePos.Add(new SC_VR_Chunk.DInstanceType()
            {
                position = currentPosition,

            });
            listOfInstancePos.Add(new SC_VR_Chunk.DInstanceType()
            {
                position = currentPosition,

            });*/




            //offset1 = right * planeSize;
            //offset2 = forward * planeSize;
            //createBottomFace(start, offset1, offset2);
            //vertzIndex += 4;
            //trigsIndex += 6;

            //positions[0 + vertzIndex] = new Vector4(currentPosition.X, currentPosition.Y, currentPosition.Z, 1);//start;
            //positions[1 + vertzIndex] = new Vector4(currentPosition.X, currentPosition.Y, currentPosition.Z, 1); ;// start + offset1;
            //positions[2 + vertzIndex] = new Vector4(currentPosition.X, currentPosition.Y, currentPosition.Z, 1); ;// start + offset2;
            //positions[3 + vertzIndex] = new Vector4(currentPosition.X, currentPosition.Y, currentPosition.Z, 1); ;// start + offset1 + offset2;

            /*arrayOfVerts[vertzIndex + 0] = new SC_VR_Chunk.DVertex()
            {
                position = start,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(0f, 1f),

            };
            arrayOfVerts[vertzIndex + 1] = new SC_VR_Chunk.DVertex()
            {
                position = start + offset1,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(0f, 1f),
            };

            arrayOfVerts[vertzIndex + 2] = new SC_VR_Chunk.DVertex()
            {
                position = start + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(0f, 1f),
            };

            arrayOfVerts[vertzIndex + 3] = new SC_VR_Chunk.DVertex()
            {
                position = start + offset1 + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(0f, 1f),
            };*/

            /*normals[tester * 4 + 0] = new Vector3(0, 1, -1);
            normals[tester * 4 + 1] = new Vector3(0, 1, -1);
            normals[tester * 4 + 2] = new Vector3(0, 1, -1);
            normals[tester * 4 + 3] = new Vector3(0, 1, -1);

            textureCoordinates[tester * 4 + 0] = new Vector2(0f, 1f);
            textureCoordinates[tester * 4 + 1] = new Vector2(0f, 1f);
            textureCoordinates[tester * 4 + 2] = new Vector2(0f, 1f);
            textureCoordinates[tester * 4 + 3] = new Vector2(0f, 1f);*/

            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);

            /*triangleIndices[trigsIndex + 0] = (vertzIndex) + 0;
            triangleIndices[trigsIndex + 1] = (vertzIndex) + 1;
            triangleIndices[trigsIndex + 2] = (vertzIndex) + 2;
            triangleIndices[trigsIndex + 3] = (vertzIndex) + 3;
            triangleIndices[trigsIndex + 4] = (vertzIndex) + 2;
            triangleIndices[trigsIndex + 5] = (vertzIndex) + 1;*/



        }


        private void createFrontFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition)
        {
            int index = listOfVerts.Count;

            //offset1 = left * planeSize;
            //offset2 = up * planeSize;
            //createFrontFace(start + right * planeSize, offset1, offset2);
            //vertzIndex += 4;
            //trigsIndex += 6;

            //positions[0 + vertzIndex] = start; //(x+1,y,z)
            //positions[1 + vertzIndex] = start + offset1;//(x,y,z)
            //positions[2 + vertzIndex] = start + offset2;//(x+1,y+1,z)
            //positions[3 + vertzIndex] = start + offset1 + offset2;//(x,y+1,z)

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(-1, 0, 0),
                //tex = new Vector2(1, 0),
            });

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset1,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(-1, 0, 0),
                //tex = new Vector2(1, 1f),
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset2,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(-1, 0, 0),
                //tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset1 + offset2,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(-1, 0, 0),
                //tex = new Vector2(0f, 1f),
            });

            /*arrayOfVerts[tester * 4 + 0] = new SC_VR_Chunk.DVertex()
            {
                position = start,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(1, 0),

            };
            arrayOfVerts[tester * 4 + 1] = new SC_VR_Chunk.DVertex()
            {
                position = start + offset1,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(1, 1f),
            };

            arrayOfVerts[tester * 4 + 2] = new SC_VR_Chunk.DVertex()
            {
                position = start + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(1, 0),
            };

            arrayOfVerts[tester * 4 + 3] = new SC_VR_Chunk.DVertex()
            {
                position = start + offset1 + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(0f, 1f),
            };*/

            /*normals[tester * 4 + 0] = new Vector3(-1, 0, 0);
            normals[tester * 4 + 1] = new Vector3(-1, 0, 0);
            normals[tester * 4 + 2] = new Vector3(-1, 0, 0);
            normals[tester * 4 + 3] = new Vector3(-1, 0, 0);

            textureCoordinates[tester * 4 + 0] = new Vector2(1f, 0f);
            textureCoordinates[tester * 4 + 1] = new Vector2(1f, 1f);
            textureCoordinates[tester * 4 + 2] = new Vector2(1f, 0f);
            textureCoordinates[tester * 4 + 3] = new Vector2(0f, 1f);*/

            /*triangleIndices[tester * 6 + 0] = tester * 4 + 0;
            triangleIndices[tester * 6 + 1] = tester * 4 + 1;
            triangleIndices[tester * 6 + 2] = tester * 4 + 2;
            triangleIndices[tester * 6 + 3] = tester * 4 + 3;
            triangleIndices[tester * 6 + 4] = tester * 4 + 2;
            triangleIndices[tester * 6 + 5] = tester * 4 + 1;*/
            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);

            listOfInstancePos.Add(currentPosition);
            listOfInstancePos.Add(currentPosition);
            listOfInstancePos.Add(currentPosition);
            listOfInstancePos.Add(currentPosition);
        }
        private void createBackFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition)
        {
            int index = listOfVerts.Count;
            //offset1 = right * planeSize;
            //offset2 = up * planeSize;
            //createBackFace(start + forward * planeSize, offset1, offset2);
            //vertzIndex += 4;
            //trigsIndex += 6;


            //positions[0 + vertzIndex] = start; //(x,y,z+1)
            //positions[1 + vertzIndex] = start + offset1;//(x+1,y,z+1)
            //positions[2 + vertzIndex] = start + offset2;//(x,y+1,z+1)
            //positions[3 + vertzIndex] = start + offset1 + offset2;//(x+1,y+1,z+1)

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                // normal = new Vector3(0, 0, -1),
                //tex = new Vector2(1, 1f),
            });

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset1,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(0, 0, -1),
                //tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset2,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(0, 0, -1),
                //tex = new Vector2(1, 1f),
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset1 + offset2,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(0, 0, -1),
                //tex = new Vector2(0f, 1f),
            });



            /*arrayOfVerts[tester * 4 + 0] = new SC_VR_Chunk.DVertex()
            {
                position = start,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(1, 1),

            };
            arrayOfVerts[tester * 4 + 1] = new SC_VR_Chunk.DVertex()
            {
                position = start + offset1,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(1, 0),
            };

            arrayOfVerts[tester * 4 + 2] = new SC_VR_Chunk.DVertex()
            {
                position = start + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(1, 1),
            };

            arrayOfVerts[tester * 4 + 3] = new SC_VR_Chunk.DVertex()
            {
                position = start + offset1 + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(0f, 1f),
            };*/

            /*normals[tester * 4 + 0] = new Vector3(0, 0, -1);
            normals[tester * 4 + 1] = new Vector3(0, 0, -1);
            normals[tester * 4 + 2] = new Vector3(0, 0, -1);
            normals[tester * 4 + 3] = new Vector3(0, 0, -1);

            textureCoordinates[tester * 4 + 0] = new Vector2(1f, 1f);
            textureCoordinates[tester * 4 + 1] = new Vector2(1f, 0f);
            textureCoordinates[tester * 4 + 2] = new Vector2(1f, 1f);
            textureCoordinates[tester * 4 + 3] = new Vector2(0f, 1f);*/


            /*triangleIndices[tester * 6 + 0] = tester * 4 + 0;
            triangleIndices[tester * 6 + 1] = tester * 4 + 1;
            triangleIndices[tester * 6 + 2] = tester * 4 + 2;
            triangleIndices[tester * 6 + 3] = tester * 4 + 3;
            triangleIndices[tester * 6 + 4] = tester * 4 + 2;
            triangleIndices[tester * 6 + 5] = tester * 4 + 1;*/

            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);

            listOfInstancePos.Add(currentPosition);
            listOfInstancePos.Add(currentPosition);
            listOfInstancePos.Add(currentPosition);
            listOfInstancePos.Add(currentPosition);

        }

        private void createRightFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition)
        {
            int index = listOfVerts.Count;
            //offset1 = up * planeSize;
            //offset2 = forward * planeSize;
            //createRightFace(start + right * planeSize, offset1, offset2);
            //vertzIndex += 4;
            //trigsIndex += 6;

            //positions[0 + vertzIndex] = start; // (x+1,y,z)
            //positions[1 + vertzIndex] = start + offset1; // (x+1,y+1,z)
            //positions[2 + vertzIndex] = start + offset2; // // (x+1,y,z+1)
            //positions[3 + vertzIndex] = start + offset1 + offset2; //(x+1,y+1,z+1)

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(-1, 0, -1),
                //tex = new Vector2(1, 0),
            });

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset1,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(-1, 0, -1),
                //tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset2,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(-1, 0, -1),
                //tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset1 + offset2,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(-1, 0, -1),
                //tex = new Vector2(0f, 1f),
            });




            /*arrayOfVerts[vertzIndex  + 0] = new SC_VR_Chunk.DVertex()
            {
                position = start,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(1, 0),

            };
            arrayOfVerts[vertzIndex  + 1] = new SC_VR_Chunk.DVertex()
            {
                position = start + offset1,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(1, 0),
            };

            arrayOfVerts[vertzIndex + 2] = new SC_VR_Chunk.DVertex()
            {
                position = start + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(1, 0),
            };

            arrayOfVerts[vertzIndex+ 3] = new SC_VR_Chunk.DVertex()
            {
                position = start + offset1 + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(0f, 1f),
            };*/

            /*normals[tester * 4 + 0] = new Vector3(-1, 0, -1);
            normals[tester * 4 + 1] = new Vector3(-1, 0, -1);
            normals[tester * 4 + 2] = new Vector3(-1, 0, -1);
            normals[tester * 4 + 3] = new Vector3(-1, 0, -1);



            textureCoordinates[tester * 4 + 0] = new Vector2(1f, 0f);
            textureCoordinates[tester * 4 + 1] = new Vector2(1f, 0f);
            textureCoordinates[tester * 4 + 2] = new Vector2(1f, 0f);
            textureCoordinates[tester * 4 + 3] = new Vector2(0f, 1f);*/


            /*triangleIndices[trigsIndex + 0] = (vertzIndex ) + 0;
            triangleIndices[trigsIndex + 1] = (vertzIndex ) + 1;
            triangleIndices[trigsIndex + 2] = (vertzIndex ) + 2;
            triangleIndices[trigsIndex + 3] = (vertzIndex) + 3;
            triangleIndices[trigsIndex + 4] = (vertzIndex ) + 2;
            triangleIndices[trigsIndex + 5] = (vertzIndex) + 1;*/

            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);

            listOfInstancePos.Add(currentPosition);
            listOfInstancePos.Add(currentPosition);
            listOfInstancePos.Add(currentPosition);
            listOfInstancePos.Add(currentPosition);

        }

        private void createleftFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition)
        {
            int index = listOfVerts.Count;
            //offset1 = back * planeSize;
            //offset2 = down * planeSize;

            //positions[0 + vertzIndex] = start; //(x,y+1,z+1)
            //positions[1 + vertzIndex] = start + offset1;//(x,y+1,z)
            //positions[2 + vertzIndex] = start + offset2; //(x,y,z+1)
            //positions[3 + vertzIndex] = start + offset1 + offset2;//(x,y,z)
            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset1,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset2,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),
            });


            listOfVerts.Add(new SC_VR_Chunk.DVertex()
            {
                position = start + offset1 + offset2,
                //color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                //normal = new Vector3(-1, 1, -1),
                //tex = new Vector2(0, 0),
            });


            /*arrayOfVerts[vertzIndex + 0] = new SC_VR_Chunk.DVertex()
            {
                position = start,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            };
            arrayOfVerts[vertzIndex + 1] = new SC_VR_Chunk.DVertex()
            {
                position = start + offset1,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),
            };

            arrayOfVerts[vertzIndex + 2] = new SC_VR_Chunk.DVertex()
            {
                position = start + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),
            };

            arrayOfVerts[vertzIndex + 3] = new SC_VR_Chunk.DVertex()
            {
                position = start + offset1 + offset2,
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0f, 0),
            };*/

            /*normals[tester * 4 + 0] = new Vector3(-1, 1, -1);
            normals[tester * 4 + 1] = new Vector3(-1, 1, -1);
            normals[tester * 4 + 2] = new Vector3(-1, 1, -1);
            normals[tester * 4 + 3] = new Vector3(-1, 1, -1);

            textureCoordinates[tester * 4 + 0] = new Vector2(0f, 0f);
            textureCoordinates[tester * 4 + 1] = new Vector2(0f, 0f);
            textureCoordinates[tester * 4 + 2] = new Vector2(0f, 0f);
            textureCoordinates[tester * 4 + 3] = new Vector2(0f, 0f);*/



            /*triangleIndices[trigsIndex + 0] = (vertzIndex) + 0;
            triangleIndices[trigsIndex + 1] = (vertzIndex) + 1;
            triangleIndices[trigsIndex + 2] = (vertzIndex) + 2;
            triangleIndices[trigsIndex + 3] = (vertzIndex) + 3;
            triangleIndices[trigsIndex + 4] = (vertzIndex) + 2;
            triangleIndices[trigsIndex + 5] = (vertzIndex) + 1;*/

            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);

            listOfInstancePos.Add(currentPosition);
            listOfInstancePos.Add(currentPosition);
            listOfInstancePos.Add(currentPosition);
            listOfInstancePos.Add(currentPosition);

        }

        public bool IsTransparent(int x, int y, int z)
        {
            if ((x < 0) || (y < 0) || (z < 0) || (x >= tinyChunkWidth) || (y >= tinyChunkHeight) || (z >= tinyChunkDepth)) return true;
            {
                return map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] == 0;
            }
        }
    }
}


/*if (z == 0)
{
    arrayOfMatrixData[y].M13 = z;
    arrayOfMatrixData[y].M14 = map[x + tinyChunkWidth * (y + tinyChunkHeight * z)];
}
else if (z == 1)
{
    arrayOfMatrixData[y].M23 = z;
    arrayOfMatrixData[y].M24 = map[x + tinyChunkWidth * (y + tinyChunkHeight * z)];
}
else if (z == 2)
{
    arrayOfMatrixData[y].M33 = z;
    arrayOfMatrixData[y].M34 = map[x + tinyChunkWidth * (y + tinyChunkHeight * z)];
}
else if (z == 3)
{
    arrayOfMatrixData[y].M43 = z;
    arrayOfMatrixData[y].M44 = map[x + tinyChunkWidth * (y + tinyChunkHeight * z)];
}

if (x == 0)
{
    arrayOfMatrixData[y].M12 = x;
    arrayOfMatrixData[y].M14 = map[x + tinyChunkWidth * (y + tinyChunkHeight * z)];

}
else if (x == 1)
{
    arrayOfMatrixData[y].M22 = x;
    arrayOfMatrixData[y].M24 = map[x + tinyChunkWidth * (y + tinyChunkHeight * z)];
}
else if (x == 2)
{
    arrayOfMatrixData[y].M32 = x;
    arrayOfMatrixData[y].M34 = map[x + tinyChunkWidth * (y + tinyChunkHeight * z)];
}
else if (x == 3)
{
    arrayOfMatrixData[y].M42 = x;
    arrayOfMatrixData[y].M44 = map[x + tinyChunkWidth * (y + tinyChunkHeight * z)];
}

if (y == 0)
{
    arrayOfMatrixData[y].M11 = y;
    arrayOfMatrixData[y].M14 = map[x + tinyChunkWidth * (y + tinyChunkHeight * z)];
}
else if (y == 1)
{
    arrayOfMatrixData[y].M21 = y;
    arrayOfMatrixData[y].M24 = map[x + tinyChunkWidth * (y + tinyChunkHeight * z)];
}
else if (y == 2)
{
    arrayOfMatrixData[y].M31 = y;
    arrayOfMatrixData[y].M34 = map[x + tinyChunkWidth * (y + tinyChunkHeight * z)];
}
else if (y == 3)
{
    arrayOfMatrixData[y].M41 = y;
    arrayOfMatrixData[y].M44 = map[x + tinyChunkWidth * (y + tinyChunkHeight * z)];
}*/
