using System;
using SharpDX;
using System.Runtime.InteropServices;

//using SCCoreSystems.sc_graphics;

using SharpDX.Direct3D;
using SharpDX.Direct3D11;

using System.Drawing;
using Jitter;
using Jitter.Dynamics;
using Jitter.Collision.Shapes;

using Jitter.LinearMath;
using System.Collections;
using System.Collections.Generic;


namespace SCCoreSystems
{
    public class SC_instancedChunk
    {

        public SC_instancedChunk_instances[] arrayOfScriptForInstances;


        public DVertex[] Vertices { get; set; }
        public int[] indices;

        private float _sizeX = 0;
        private float _sizeY = 0;
        private float _sizeZ = 0;

        public DInstanceType[] instances { get; set; }
        public DInstanceShipData[] instancesDataFORWARD { get; set; }
        public DInstanceShipData[] instancesDataRIGHT { get; set; }
        public DInstanceShipData[] instancesDataUP { get; set; }

        public DInstanceType[] instancesIndex { get; set; }

        public int[][] arrayOfSomeMap { get; set; }
        public Matrix[] totalArrayOfMatrixData { get; set; }

        [StructLayout(LayoutKind.Explicit, Size = 72)]
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
        }

        [StructLayout(LayoutKind.Explicit, Size = 48)] //, Pack = 48
        public struct DInstanceType
        {
            [FieldOffset(0)]
            public int one;
            [FieldOffset(4)]
            public int two;
            [FieldOffset(8)]
            public int three;
            [FieldOffset(12)]
            public int four;
            [FieldOffset(16)]
            public int oneTwo;
            [FieldOffset(20)]
            public int twoTwo;
            [FieldOffset(24)]
            public int threeTwo;
            [FieldOffset(28)]
            public int fourTwo;
            [FieldOffset(32)]
            public Vector4 instancePos;
        };

        [StructLayout(LayoutKind.Explicit, Size = 16)] //, Pack = 48
        public struct DInstanceShipData
        {
            [FieldOffset(0)]
            public Vector4 instancePos;
        };


        [StructLayout(LayoutKind.Sequential, Pack = 16)]
        public struct DInstanceTypeTwo
        {
            public Matrix matrix;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct DIndexType
        {
            public int indexPos;
        };


        Matrix maininstancematrix;

        public SC_instancedChunk(float sizex, float sizey, float sizez, Vector4 color, int width, int height, int depth, Vector3 pos, Matrix _maininstancematrix)
        {

            maininstancematrix = _maininstancematrix;

            this._color = color;
            this._sizeX = sizex;
            this._sizeY = sizey;
            this._sizeZ = sizez;

            this._chunkPos = pos;

            int[] theMap;

            arrayOfScriptForInstances = new SC_instancedChunk_instances[SC_Globals.numberOfInstancesPerObjectInWidth * SC_Globals.numberOfInstancesPerObjectInHeight * SC_Globals.numberOfInstancesPerObjectInDepth];

            instances = new DInstanceType[SC_Globals.numberOfInstancesPerObjectInWidth * SC_Globals.numberOfInstancesPerObjectInHeight * SC_Globals.numberOfInstancesPerObjectInDepth];
            instancesIndex = new DInstanceType[SC_Globals.numberOfInstancesPerObjectInWidth * SC_Globals.numberOfInstancesPerObjectInHeight * SC_Globals.numberOfInstancesPerObjectInDepth];
            arrayOfSomeMap = new int[SC_Globals.numberOfInstancesPerObjectInWidth * SC_Globals.numberOfInstancesPerObjectInHeight * SC_Globals.numberOfInstancesPerObjectInDepth][];

            instancesDataFORWARD = new DInstanceShipData[SC_Globals.numberOfInstancesPerObjectInWidth * SC_Globals.numberOfInstancesPerObjectInHeight * SC_Globals.numberOfInstancesPerObjectInDepth];
            instancesDataRIGHT = new DInstanceShipData[SC_Globals.numberOfInstancesPerObjectInWidth * SC_Globals.numberOfInstancesPerObjectInHeight * SC_Globals.numberOfInstancesPerObjectInDepth];
            instancesDataUP = new DInstanceShipData[SC_Globals.numberOfInstancesPerObjectInWidth * SC_Globals.numberOfInstancesPerObjectInHeight * SC_Globals.numberOfInstancesPerObjectInDepth];




            Vector4 position;
            chunk newChunker;

            int oner;
            int twoer;
            int threer;
            int fourer;

            int onerTwo;
            int twoerTwo;
            int threerTwo;
            int fourerTwo;

            var offsetX = 1;
            var offsetY = 1;
            var offsetZ = 1;

            var offsetInstancedShips = 2;// 2.5f;



            for (int x = 0; x < SC_Globals.numberOfInstancesPerObjectInWidth; x++)
            {
                for (int y = 0; y < SC_Globals.numberOfInstancesPerObjectInHeight; y++)
                {
                    for (int z = 0; z < SC_Globals.numberOfInstancesPerObjectInDepth; z++)
                    {
                        var index0 = x + SC_Globals.numberOfInstancesPerObjectInWidth * (y + SC_Globals.numberOfInstancesPerObjectInHeight * z);

                        position = new Vector4(x * offsetInstancedShips, y * offsetInstancedShips, z * offsetInstancedShips, 1);

                        newChunker = new chunk();

                        position.X *= (SC_Globals.tinyChunkWidth * 1) * offsetX;
                        position.Y *= (SC_Globals.tinyChunkHeight * 1) * offsetY;
                        position.Z *= (SC_Globals.tinyChunkDepth * 1) * offsetZ;

                        position.X *= (SC_Globals.planeSize) * offsetX;
                        position.Y *= (SC_Globals.planeSize) * offsetY;
                        position.Z *= (SC_Globals.planeSize) * offsetZ;

                        position.X += (_chunkPos.X );
                        position.Y += (_chunkPos.Y );
                        position.Z += (_chunkPos.Z );

                        //position.X += (offsetX);
                        //position.Y += (offsetY);
                        //position.Z += (offsetZ);

                        newChunker.startBuildingArray(position, out oner, out twoer, out threer, out fourer, out onerTwo, out twoerTwo, out threerTwo, out fourerTwo, out theMap);

                        /*Matrix _matrix = Matrix.Identity;

                        _matrix.M11 = position.X;
                        _matrix.M12 = position.Y;
                        _matrix.M13 = position.Z;
                        _matrix.M14 = position.W;*/

                        //SC_instancedChunk_instances chunkinstance = new SC_instancedChunk_instances();

                        Matrix _tempMatrix = maininstancematrix;

                        position.X += maininstancematrix.M41;
                        position.Y += maininstancematrix.M42;
                        position.Z += maininstancematrix.M43;

                        _tempMatrix.M41 = position.X;
                        _tempMatrix.M42 = position.Y;
                        _tempMatrix.M43 = position.Z;

                        arrayOfScriptForInstances[index0]  = new SC_instancedChunk_instances();
                        arrayOfScriptForInstances[index0].current_pos = _tempMatrix;

                        //_matrix = Matrix.Identity;

                        /*_matrix.M11 = position.X;
                        _matrix.M12 = position.Y;
                        _matrix.M13 = position.Z;
                        _matrix.M14 = position.W; // can be switched for something else as it's not even required in the shader.

                        _matrix.M21 = VectorTemp.X;
                        _matrix.M22 = VectorTemp.Y;
                        _matrix.M23 = VectorTemp.Z;
                        _matrix.M24 = VectorTemp.W;

                        _matrix.M31 = VectorTempTwo.X;
                        _matrix.M32 = VectorTempTwo.Y;
                        _matrix.M33 = VectorTempTwo.Z;
                        _matrix.M34 = VectorTempTwo.W;*/

                        //_matrix.M41 = X;
                        //_matrix.M42 = Y;
                        //_matrix.M43 = Z;
                        //_matrix.M44 = W;

                        //It is less convoluted when the data is inside of 1 Matrix isntead of 8 ints.

                        instances[x + SC_Globals.numberOfInstancesPerObjectInWidth * (y + SC_Globals.numberOfInstancesPerObjectInHeight * z)] = new DInstanceType()
                        {
                            one = oner,
                            two = twoer,
                            three = threer,
                            four = fourer,
                            oneTwo = onerTwo,
                            twoTwo = twoerTwo,
                            threeTwo = threerTwo,
                            fourTwo = fourerTwo,
                            instancePos = new Vector4(position.X, position.Y, position.Z, 1),
                        };

                        instancesDataFORWARD[x + SC_Globals.numberOfInstancesPerObjectInWidth * (y + SC_Globals.numberOfInstancesPerObjectInHeight * z)] = new DInstanceShipData()
                        {
                            instancePos = new Vector4(position.X, position.Y, position.Z, 1),
                        };
                        instancesDataRIGHT[x + SC_Globals.numberOfInstancesPerObjectInWidth * (y + SC_Globals.numberOfInstancesPerObjectInHeight * z)] = new DInstanceShipData()
                        {
                            instancePos = new Vector4(position.X, position.Y, position.Z, 1),
                        };
                        instancesDataUP[x + SC_Globals.numberOfInstancesPerObjectInWidth * (y + SC_Globals.numberOfInstancesPerObjectInHeight * z)] = new DInstanceShipData()
                        {
                            instancePos = new Vector4(position.X, position.Y, position.Z, 1),
                        };

                        arrayOfSomeMap[x + SC_Globals.numberOfInstancesPerObjectInWidth * (y + SC_Globals.numberOfInstancesPerObjectInHeight * z)] = theMap;
                    }
                }
            }
        }

        //SC_instancedChunk[] arrayOfChunks;
        //chunkData[] arrayOfChunkData;

        public int InstanceCount = 0;

        public Vector4 _color;

        public int instanceCounter { get; set; }
        public byte[] map { get; set; }

        public Vector3 _chunkPos { get; set; }

    }
}