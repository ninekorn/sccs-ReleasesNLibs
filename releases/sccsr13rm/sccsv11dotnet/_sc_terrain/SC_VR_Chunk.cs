using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System.Linq;
using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;


namespace _sc_core_systems
{
    public class SC_VR_Chunk
    {
        public const int mapWidth = 4;
        public const int mapHeight = 4;
        public const int mapDepth = 4;

        public const int tinyChunkWidth = 4;
        public const int tinyChunkHeight = 4;
        public const int tinyChunkDepth = 4;

        public const int mapObjectInstanceWidth = 4;
        public const int mapObjectInstanceHeight = 4;
        public const int mapObjectInstanceDepth = 4;

        public float _planeSize = 0.1f;

        public SC_VR_Chunk_Shader shaderOfChunk { get; set; }

        public SharpDX.Direct3D11.Buffer InstanceBuffer { get; set; }

        public int VertexCount { get; set; }
        public int IndexCount { get; set; }

        public DVertex[] Vertices { get; set; }
        public int[] indices;

        private float _sizeX = 0;
        private float _sizeY = 0;
        private float _sizeZ = 0;

        public DVertex[][] arrayOfDVertex { get; set; }
        public DInstanceType[] instances { get; set; }


        public DInstanceType[] instancesIndex { get; set; }

        public Vector4[][] arrayOfInstanceVertex { get; set; }
        public int[][] arrayOfInstanceIndices { get; set; }


        public Vector3[][] arrayOfInstanceNormals { get; set; }
        public Vector2[][] arrayOfInstanceTexturesCoordinates { get; set; }

        public DInstanceType[] arrayOfInstancesPosition { get; set; }


        public int[] arrayOfSwitches { get; set; }

        //public DMap theMaps { get; set; }

        public int[][] arrayOfSomeMap { get; set; }


        //public DMapArray dMapArray { get; set; }

        //public static DMapArray _dMapArray;
        public Matrix[] totalArrayOfMatrixData { get; set; }


        //public DMatrixArray matrixArrayOfMapData { get; set; }

        //public DFloatArray[] arrayOfFloatsY0 { get; set; }
        //public DFloatArray[] arrayOfFloatsY1 { get; set; }
        //public DFloatArray[] arrayOfFloatsY2 { get; set; }
        //public DFloatArray[] arrayOfFloatsY3 { get; set; }

        //public Vector4[] arrayOfDeVectorMapTemp { get; set; }




        public DInstanceType[] arrayOfDeVectorMapTemp { get; set; }

        public DInstanceType[] arrayOfDeVectorMapTempTwo { get; set; }










        [StructLayout(LayoutKind.Explicit, Size = 80)]
        public struct DVertex
        {
            [FieldOffset(0)]
            public Vector4 position;
            [FieldOffset(16)]
            public Vector4 indexPos;
            [FieldOffset(32)]
            public Vector4 color;
            [FieldOffset(48)]
            public Vector3 normal;
            [FieldOffset(64)]
            public Vector2 tex;
            [FieldOffset(68)]
            public Vector3 dummyStuff;
            //[FieldOffset(68)]
            //public Vector3 dummyStuff;
            //public Vector4 tangent;
            //public Vector3 binormal;
        }


        /*[StructLayout(LayoutKind.Sequential)]
        public struct DFloatArray
        {
            public float indexY0;
        };*/

        /*[StructLayout(LayoutKind.Sequential, Pack = 16)]
        public struct DMatrixArray
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)] //16384
            public Matrix[] matrixArray;
        };*/

        /*[StructLayout(LayoutKind.Sequential, Pack = 16)]
        public struct DMapArray
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public int[][] chunkMap;
        };*/

        /*[StructLayout(LayoutKind.Sequential, Pack = 16)]
        public struct DMap
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] //65536
            public int[] chunkMap;
        };*/

        [StructLayout(LayoutKind.Sequential, Pack = 16)]
        public struct DInstanceType
        {
            public Vector4 position;
        };



        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct DIndexType
        {
            public int indexPos;
        };


        /*[StructLayout(LayoutKind.Sequential, Pack = 16)]
        public struct DColorType
        {
            public Vector4[] Color;
        };*/























        // Constructor
        public SC_VR_Chunk(float xxi, float yyi, float zzi, Vector4 color, int width, int height, int depth, Vector3 pos) //,DInstanceType[] _instances
        {
            this._color = color;
            this._sizeX = xxi;
            this._sizeY = yyi;
            this._sizeZ = zzi;

            this._chunkPos = pos;

            VertexCount = 1;
            // Set number of vertices in the index array.
            IndexCount = 3;

            int[] theMap;

            arrayOfInstanceVertex = new Vector4[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceIndices = new int[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceNormals = new Vector3[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceTexturesCoordinates = new Vector2[mapWidth * mapHeight * mapDepth][];
            arrayOfInstancesPosition = new DInstanceType[mapWidth * mapHeight * mapDepth];

            /*theMaps = new DMap()
            {
                chunkMap = new int[mapWidth * mapHeight * mapDepth * tinyChunkWidth * tinyChunkHeight * tinyChunkDepth],
            };*/
          
            arrayOfSwitches = new int[mapWidth * mapHeight * mapDepth];
            InstanceCount = mapWidth * mapHeight * mapDepth;
            instances = new DInstanceType[InstanceCount];
            instancesIndex = new DInstanceType[InstanceCount];

            arrayOfSomeMap = new int[mapWidth * mapHeight * mapDepth][];

            //dMapArray = new DMapArray();
            totalArrayOfMatrixData = new Matrix[mapWidth * mapHeight * mapDepth * 4];
            //dMapArray.chunkMap = new int[mapWidth * mapHeight * mapDepth][];



            arrayOfDeVectorMapTemp = new DInstanceType[mapWidth * mapHeight * mapDepth];
            arrayOfDeVectorMapTempTwo = new DInstanceType[mapWidth * mapHeight * mapDepth];
            //arrayOfFloatsY0 = new DFloatArray[mapWidth * mapHeight * mapDepth];
            //arrayOfFloatsY1 = new DFloatArray[mapWidth * mapHeight * mapDepth];
            //arrayOfFloatsY2 = new DFloatArray[mapWidth * mapHeight * mapDepth];
            //arrayOfFloatsY3 = new DFloatArray[mapWidth * mapHeight * mapDepth];

            Vector4 position;
            chunk newChunker;
            arrayOfDVertex = new DVertex[InstanceCount][];
            DVertex[] arrayOfD;// = new DVertex[];

            DInstanceType arrayOfInstancePositions;

            int someCounter = 0;
            Matrix[] arrayOfMatrix;
            int someOtherCounter = 0;

            int[] byteMap = new int[mapWidth * mapHeight * mapDepth * tinyChunkWidth * tinyChunkHeight * tinyChunkDepth];

            Vector4 VectorTemp;
            Vector4 VectorTempTwo;
            float[] floatArray;
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    for (int z = 0; z < mapDepth; z++)
                    {
                        //Console.WriteLine("test");
                        position = new Vector4(x, y, z, 1);
                        newChunker = new chunk();
                        //position.X = position.X + (_chunkPos.X ); //*1.05f
                        //position.Y = position.Y + (_chunkPos.Y );
                        //position.Z = position.Z + (_chunkPos.Z );


                        position.X *= (tinyChunkWidth);
                        position.Y *= (tinyChunkHeight);
                        position.Z *= (tinyChunkDepth);

                        position.X *= (_planeSize);
                        position.Y *= (_planeSize);
                        position.Z *= (_planeSize);

                        position.X += (_chunkPos.X);
                        position.Y += (_chunkPos.Y);
                        position.Z += (_chunkPos.Z);

                        //position.X += (10);
                        //position.Y += (10);
                        //position.Z += (10);
                        // byte[] tester = 
                        newChunker.startBuildingArray(position,out VectorTemp, out VectorTempTwo); //, out arrayOfInstancePositions
                 
                        instances[x + mapWidth * (y + mapHeight * z)] = new DInstanceType()
                        {
                            position = new Vector4(position.X, position.Y, position.Z, 1),
                        };

                        arrayOfDeVectorMapTemp[x + mapWidth * (y + mapHeight * z)] = new DInstanceType()
                        {
                            position = VectorTemp,
                        };

                        arrayOfDeVectorMapTempTwo[x + mapWidth * (y + mapHeight * z)] = new DInstanceType()
                        {
                            position = VectorTemp,
                        };





                        //arrayOfInstanceVertex[xx + mapWidth * (yy + mapHeight * zz)] = vertexArray0; //new Vector4(vertexArray0[v].X, vertexArray0[v].Y, vertexArray0[v].Z, 1);
                        //arrayOfInstanceIndices[xx + mapWidth * (yy + mapHeight * zz)] = indicesArray0;
                        //arrayOfDVertex[xx + mapWidth * (yy + mapHeight * zz)] = arrayOfD;
                        //arrayOfSwitches[xx + mapWidth * (yy + mapHeight * zz)] = 1;


                        //arrayOfInstanceNormals[xx + mapWidth * (yy + mapHeight * zz)] = normals;
                        //arrayOfInstanceTexturesCoordinates[xx + mapWidth * (yy + mapHeight * zz)] = texturesCoordinates;

                        //instances[xx + mapWidth * (yy + mapHeight * zz)] = new Vector4[1];
                        //instances[xx + mapWidth * (yy + mapHeight * zz)][0] = new Vector4(position.X, position.Y, position.Z, 1);


                        //for (int i = 0;i < arrayOfMatrix.Length;i++)
                        //{
                        //    totalArrayOfMatrixData[someOtherCounter] = arrayOfMatrix[i];
                        //    someOtherCounter++;
                        //}





                        //instancesIndex[x + mapWidth * (y + mapHeight * z)] = new DInstanceType()
                        //{
                        //    position = new Vector4(x,y,z, someOtherCounter),
                        //};

                        //arrayOfSomeMap[x + mapWidth * (y + mapHeight * z)] = theMap;




                        /*arrayOfFloatsY0[x + mapWidth * (y + mapHeight * z)] = new DFloatArray()
                        {
                            indexY0 = floatArray[0],
                        };
                        arrayOfFloatsY1[x + mapWidth * (y + mapHeight * z)] = new DFloatArray()
                        {
                            indexY0 = floatArray[1],
                        };
                        arrayOfFloatsY2[x + mapWidth * (y + mapHeight * z)] = new DFloatArray()
                        {
                            indexY0 = floatArray[2],
                        };
                        arrayOfFloatsY3[x + mapWidth * (y + mapHeight * z)] = new DFloatArray()
                        {
                            indexY0 = floatArray[3],
                        };*/



                        /*int index = x + mapWidth * (y + mapHeight * z);
                        index *= 64;
                        //int indexone = x + mapWidth * (y + mapHeight * z);

                        for (int xi = 0; xi < tinyChunkWidth; xi++)
                        {
                            for (int yi = 0; yi < tinyChunkHeight; yi++)
                            {
                                for (int zi = 0; zi < tinyChunkDepth; zi++)
                                {
                                    int indexTwo = xi + tinyChunkWidth * (yi + tinyChunkHeight * zi);
                                    byteMap[index + indexTwo] = theMap[indexTwo];
                                }
                            }
                        }*/
                    }
                }
            }



            /*theMaps = new DMap()
            {
                chunkMap = byteMap,
            };*/

            /*matrixArrayOfMapData = new DMatrixArray()
            {
                matrixArray = totalArrayOfMatrixData,
            };*/

        }


        //DColorType[] arrayOfColor;

        public int InstanceCount = 0;

        Vector4[] vertexArray0;
        int[] indicesArray0;

        Vector3[] normals;
        Vector2[] texturesCoordinates;


        Vector4[] vertexArray => vertexArray0;
        int[] indicesArray => indicesArray0;



        public Vector4 _color;


        public int instanceCounter { get; set; }
        public byte[] map { get; set; }

        public Vector3 _chunkPos { get; set; }



        private void ShutDownBuffers()
        {
            //InstanceBuffer?.Dispose();
            //InstanceBuffer = null;
        }
    }
}