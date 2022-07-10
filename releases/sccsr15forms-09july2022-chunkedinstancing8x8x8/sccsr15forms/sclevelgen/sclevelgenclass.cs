﻿using System;
using SharpDX;
using System.Runtime.InteropServices;

//using sccs.scgraphics;

using SharpDX.Direct3D;
using SharpDX.Direct3D11;

using System.Drawing;
using Jitter;
using Jitter.Dynamics;
using Jitter.Collision.Shapes;

using Jitter.LinearMath;
using System.Collections;
using System.Collections.Generic;


namespace sccsr15forms
{
    public class sclevelgenclass
    {

        public int[] choosecolortype;


        public sclevelgenclassPrim.DTessellationBufferType[] tessellationBuffer;
        //public SharpDX.Direct3D11.Buffer ConstantTessellationBuffer;



        public void ShutDown()
        {
            // Release the model texture.
            ReleaseTexture();
            // Release the vertex and index buffers.
            ShutdownBuffers();
        }
        private void ReleaseTexture()
        {
            // Release the texture object.
            //Texture?.ShutDown();
            //Texture = null;



        }
        private void ShutdownBuffers()
        {
            // Return the index buffer.
            //IndexBuffer?.Dispose();
            //IndexBuffer = null;
            // Release the vertex buffer.
            //VertexBuffer?.Dispose();
            //VertexBuffer = null;
            heightmapmatrix = null;
            /*
            instancesmatrix = null;
            instancesmatrixb = null;
            instancesmatrixc = null;
            instancesmatrixd = null;
            */
            arrayOfSomeMap = null;
            /*instancesDataFORWARD = null;
            instancesDataRIGHT = null;
            instancesDataUP = null;
            */
            arrayofzeromeshinstances = null;
            ///instances = null;
            //somechunk = null;
            //instancesIndex = null;
            //instancesLocationW = null;
            //instancesLocationH = null;

            //tessellationBuffer = null;




            lightBuffer = null;


            //public Matrix[] matrixofinstances;


            //somechunk = null;


            arrayofzeromeshinstances = null;
            //public Matrix _POSITION = null;

            Vertices = null;
            indices = null;

            _sizeX = 0;
            _sizeY = 0;
            _sizeZ = 0;

            instancesccsbytemapxyz = null;
            heightmapmatrix = null;
            /*public heightmapinstance[] colorsNFacesy { get; set; }
            public heightmapinstance[] colorsNFacesz { get; set; }
            public heightmapinstance[] colorsNFacesw { get; set; }*/
            /*
            instances = null;
            instancesDataFORWARD = null;
            instancesDataRIGHT = null;
            instancesDataUP = null;

            instancesbytemaps = null;


            instancessccsbytemap = null;*/

            //public DIndexType[] instancesindex { get; set; }
            /*
            instancesIndex = null;

            arrayOfSomeMap = null;
            totalArrayOfMatrixData = null;

            //instancesLocationW = null;
            //instancesLocationH = null;
            //instancesLocationD = null;
            instancesmatrix = null;
            instancesmatrixb = null;
            instancesmatrixc = null;
            instancesmatrixd = null;*/

            //Matrix maininstancematrix;

            _WORLDMATRIXINSTANCES = null;

            somewidth = 0;
            someheight = 0;
            somedepth = 0;
            numberOfObjectInWidth = 0;
            numberOfObjectInHeight = 0;
            numberOfObjectInDepth = 0;
            numberOfInstancesPerObjectInWidth = 0;
            numberOfInstancesPerObjectInHeight = 0;
            numberOfInstancesPerObjectInDepth = 0;
            planeSize = 0;

            //tinyChunkWidth = 0;
            //tinyChunkHeight = 0;
            //tinyChunkDepth = 0;

            voxeltype = 0;
            mapofbytes = 0;


            /*if (somechunk != null)
            {
                for (int i = 0;i < somechunk.Length;i++)
                {
                    if (somechunk[i]!= null)
                    {
                        somechunk[i].Dispose();
                    }
                }
            }*/
        }

        public sclevelgenvert[] arrayofchunks;
        public sclevelgenmaps[] arrayofchunksmaps;

        public static sclevelgenclass thesclevelgenclass;



        public sclevelgenclassPrim.DLightBufferEr[] lightBuffer;


        //public Matrix[] matrixofinstances;


        //public chunkscreen[] somechunk;

        //public chunkmesh[] somechunk;
        //public sclevelgenvert[] somechunk;


        public sclevelgenclasscore[] arrayofzeromeshinstances;
        public Matrix _POSITION { get; set; }

        public DVertex[] Vertices { get; set; }
        public int[] indices;

        private float _sizeX = 0;
        private float _sizeY = 0;
        private float _sizeZ = 0;



        public DInstancesccsbytemapxyz[] instancesccsbytemapxyz { get; set; }
        public heightmapinstance[] heightmapmatrix { get; set; }
        /*public heightmapinstance[] colorsNFacesy { get; set; }
        public heightmapinstance[] colorsNFacesz { get; set; }
        public heightmapinstance[] colorsNFacesw { get; set; }*/
        /*
        public DInstanceType[] instances { get; set; }
        public DInstanceShipData[] instancesDataFORWARD { get; set; }
        public DInstanceShipData[] instancesDataRIGHT { get; set; }
        public DInstanceShipData[] instancesDataUP { get; set; }
        */
        //public DInstancesByteMap[] instancesbytemaps { get; set; }


        //public DInstancesccsbytemapxyz[] instancessccsbytemap { get; set; }


        //public DIndexType[] instancesindex { get; set; }

        //public DInstanceType[][] instancesIndex { get; set; }

        public int[][] arrayOfSomeMap { get; set; }
        //public Matrix[] totalArrayOfMatrixData { get; set; }

        /*
        public sclevelgenclass.DInstanceTypeLocW[] instancesLocationW;
        public sclevelgenclass.DInstanceTypeLocH[] instancesLocationH;
        public sclevelgenclass.DInstanceTypeLocD[] instancesLocationD;
        public sclevelgenclass.DInstanceMatrix[] instancesmatrix;
        public sclevelgenclass.DInstanceMatrix[] instancesmatrixb;
        public sclevelgenclass.DInstanceMatrix[] instancesmatrixc;
        public sclevelgenclass.DInstanceMatrix[] instancesmatrixd;*/



        Matrix maininstancematrix;

        public Matrix[] _WORLDMATRIXINSTANCES { get; set; }

        int somewidth;
        int someheight;
        int somedepth;
        int numberOfObjectInWidth; int numberOfObjectInHeight; int numberOfObjectInDepth; int numberOfInstancesPerObjectInWidth; int numberOfInstancesPerObjectInHeight; int numberOfInstancesPerObjectInDepth; float planeSize;

        //int tinyChunkWidth; int tinyChunkHeight; int tinyChunkDepth;

        int voxeltype;
        int mapofbytes;



        //[StructLayout(LayoutKind.Explicit, Size = 80)]
        [StructLayout(LayoutKind.Explicit)]
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
            [FieldOffset(60)]
            public float padding0;
            [FieldOffset(64)]
            public Vector2 tex;
            [FieldOffset(72)]
            public float padding1;
            [FieldOffset(76)]
            public float padding2;
        }




        /*
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
            public Vector4 normal;
            [FieldOffset(64)]
            public Vector4 tex;
        }*/










        [StructLayout(LayoutKind.Sequential)]
        public struct DInstanceTypeLocW
        {
            public int index;
        };
        [StructLayout(LayoutKind.Sequential)]
        public struct DInstanceTypeLocH
        {
            public int index;
        };
        [StructLayout(LayoutKind.Sequential)]
        public struct DInstanceTypeLocD
        {
            public int index;
        };
        //[StructLayout(LayoutKind.Explicit, Size = 48)] //, Pack = 48
        public struct DInstanceType
        {
            public Vector4 instancePos;
        };

        public struct DInstanceMatrix
        {
            public Matrix instancematrix;
            public Matrix instancematrixb;
            public Matrix instancematrixc;
            public Matrix instancematrixd;
        };

        /*[StructLayout(LayoutKind.Explicit, Size = 32)] //, Pack = 48
        public struct DInstancesByteMap
        {
            [FieldOffset(0)]
            public int one;
            [FieldOffset(4)]
            public int oneTwo;
            [FieldOffset(8)]
            public int two;
            [FieldOffset(12)]
            public int twoTwo;
            [FieldOffset(16)]
            public int three;
            [FieldOffset(20)]
            public int threeTwo;
            [FieldOffset(24)]
            public int four;
            [FieldOffset(28)]
            public int fourTwo;
        };*/


        [StructLayout(LayoutKind.Sequential)] //, Pack = 48 //, Size = 32)
        public struct DInstancesByteMap
        {
            //[FieldOffset(0)]
            public int one;
            //[FieldOffset(4)]
            public int oneTwo;
            //[FieldOffset(8)]
            public int two;
            //[FieldOffset(12)]
            public int twoTwo;
            //[FieldOffset(16)]
            public int three;
            //[FieldOffset(20)]
            public int threeTwo;
            //[FieldOffset(24)]
            public int four;
            //[FieldOffset(28)]
            public int fourTwo;
        };


        [StructLayout(LayoutKind.Explicit)]
        public struct DInstancesccsbytemapxyz
        {
            [FieldOffset(0)]
            public Matrix xyzmap;
            //[FieldOffset(64)]
            //public Matrix ymap;
            //[FieldOffset(128)]
            //public Matrix zmap;
        }







        //[StructLayout(LayoutKind.Explicit, Size = 48)] //, Pack = 48
        public struct heightmapinstance
        {
            public Matrix heightmapmat; //Vector4
        };







        [StructLayout(LayoutKind.Explicit, Size = 16)] //, Pack = 48
        public struct DInstanceShipData
        {
            [FieldOffset(0)]
            public Vector4 instancePos;
        };


        /*[StructLayout(LayoutKind.Sequential, Pack = 16)]
        public struct DInstanceTypeTwo
        {
            //public Matrix matrix;
            public Vector4 instancedir;
        };*/

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct DIndexType
        {
            public int indexPos;
        };




        //Matrix[] DInstanceMatrixarray;
        /*
        public sclevelgenvert getChunk(int x, int y, int z)
        {
            if ((x < 0) || (y < 0) || (z < 0) || (x >= somewidth) || (y >= (someheight)) || (z >= (somedepth)))
            {
                return null;
            }
            /*
            if (x < 0)
            {
                x *= -1;
                x = (PlanetChunkWidth_R) + x;
            }
            if (y < 0)
            {
                y *= -1;
                y = (PlanetChunkHeight_R) + y;
            }
            if (z < 0)
            {
                z *= -1;
                z = (PlanetChunkDepth_R) + z;
            }

            int _index = x + (somewidth) * (y + (someheight) * z);

            return arrayofzeromeshinstances[_index];
        }*/






        public sclevelgenmaps sometrigchunkmap;

        int[] somemap;
        public sclevelgenmaps createbytemaps(Vector3 realposition, LevelGenerator4 levelgen, Vector3 chunkPos, sclevelgenmaps sometrigchunkmap_, int typeofterraintile, out int[] somemap_, sclevelgenclassPrim chunkprim, int levelofdetail)
        {
      
            sometrigchunkmap = sometrigchunkmap_;
            sometrigchunkmap.buildchunkmaps(realposition, out somemap, planeSize, levelgen, chunkPos, typeofterraintile, chunkprim, levelofdetail);
            somemap_ = somemap;


            /*arrayofchunksmaps[ichunkleveltile] = sometrigchunkmap;
            arrayofchunks[ichunkleveltile] = newChunker;
            arrayofchunks[ichunkleveltile].chunkPos = chunkPos;
            arrayofchunks[ichunkleveltile].map = somemap;*/


            return sometrigchunkmap;
        }


        int[] indicesArray0;
        DVertex[] arrayOfD;
        public sclevelgenvert sometrigchunkvert;
        public sclevelgenvert createchunkvert(Vector3 realposition, LevelGenerator4 levelgen, Vector3 chunkPos, sclevelgenvert sometrigchunkvert_, int typeofterraintile, out int[] somemap_,out int[] indicesArray0_, out DVertex[] arrayOfD_, sclevelgenclassPrim chunkprim, sclevelgenmaps levelmap, int levelofdetail)
        {

            sometrigchunkvert = sometrigchunkvert_;
            //sometrigchunkvert.buildchunkmaps(realposition, out somemap, planeSize, levelgen, chunkPos, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, typeofterraintile, this);
            sometrigchunkvert.startBuildingArray(realposition, out indicesArray0, out somemap, out arrayOfD, planeSize, levelgen, chunkPos, typeofterraintile, chunkprim, levelmap.map, levelofdetail);

            somemap_ = somemap;
            /*arrayofchunksmaps[ichunkleveltile] = sometrigchunkmap;
            arrayofchunks[ichunkleveltile] = newChunker;
            arrayofchunks[ichunkleveltile].chunkPos = chunkPos;
            arrayofchunks[ichunkleveltile].map = somemap;*/

            indicesArray0_ = indicesArray0;
            arrayOfD_ = arrayOfD;


            return sometrigchunkvert;
        }


        public DVertex[][] arrayOfDVertex;
        public int[][] arrayOfInstanceIndices;

        int typeofvoxelmesh;
        public sclevelgenclass(Vector4 color, Vector3 pos, Matrix _maininstancematrix , int _addtoworld, float _mass, World _the_world, bool _is_static, directx.BodyTag _tag, float planeSize_, float offsetinstance, LevelGenerator4 levelgen)
        {
            thesclevelgenclass = this;

            /*typeofvoxelmesh = typeofvoxelmesh_;
            mapofbytes = mapofbytes_;
            voxeltype = voxeltype_;*/
            //instancesbytemaps = instancesbytemaps_;
            //instancesbytemaps = instancesbytemaps_;

            //tinyChunkWidth = tinyChunkWidth_;
            //tinyChunkHeight = tinyChunkHeight_;
            //tinyChunkDepth = tinyChunkDepth_;

            /*numberOfObjectInWidth = numberOfObjectInWidth_;
            numberOfObjectInHeight = numberOfObjectInHeight_;
            numberOfObjectInDepth = numberOfObjectInDepth_;
            numberOfInstancesPerObjectInWidth = numberOfInstancesPerObjectInWidth_;
            numberOfInstancesPerObjectInHeight = numberOfInstancesPerObjectInHeight_;
            numberOfInstancesPerObjectInDepth = numberOfInstancesPerObjectInDepth_;*/

            planeSize = planeSize_;

            //somewidth = numberOfInstancesPerObjectInWidth;
            //someheight = numberOfInstancesPerObjectInHeight;
            //somedepth = numberOfInstancesPerObjectInDepth;

            maininstancematrix = _maininstancematrix;

            this._color = color;
            /*
            this._sizeX = sizex;
            this._sizeY = sizey;
            this._sizeZ = sizez;*/

            this._chunkPos = pos;

            int[] theMap;

            arrayofzeromeshinstances = new sclevelgenclasscore[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];


            //choosecolortype = new int[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];

            //instancesbytemaps = new DInstancesByteMap[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];
            //instancessccsbytemap = new DInstancesccsbytemapxyz[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];


            /*
            instancesmatrix = new DInstanceMatrix[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];
            instancesmatrixb = new DInstanceMatrix[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];
            instancesmatrixc = new DInstanceMatrix[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];
            instancesmatrixd = new DInstanceMatrix[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];

            heightmapmatrix = new heightmapinstance[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];
            */
            //instancesLocationW = new DInstanceTypeLocW[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];
            //instancesLocationH = new DInstanceTypeLocH[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];

            //instancesDataFORWARD = new DInstanceShipData[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];
            //instancesDataRIGHT = new DInstanceShipData[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];
            //instancesDataUP = new DInstanceShipData[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];

            //instances = new DInstanceType[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];

            //arrayOfSomeMap = new int[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth][];

            //Vector4 position;
            //chunk newChunker;

            //instancesIndex = new DInstanceType[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth][];


            //tessellationBuffer = new sclevelgenclassPrim.DTessellationBufferType[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];

            /*
            double m11 = 0;
            double m12 = 0;
            double m13 = 0;
            double m14 = 0;
            double m21 = 0;
            double m22 = 0;
            double m23 = 0;
            double m24 = 0;
            double m31 = 0;
            double m32 = 0;
            double m33 = 0;
            double m34 = 0;
            double m41 = 0;
            double m42 = 0;
            double m43 = 0;
            double m44 = 0;


            double m11b = 0;
            double m12b = 0;
            double m13b = 0;
            double m14b = 0;
            double m21b = 0;
            double m22b = 0;
            double m23b = 0;
            double m24b = 0;
            double m31b = 0;
            double m32b = 0;
            double m33b = 0;
            double m34b = 0;
            double m41b = 0;
            double m42b = 0;
            double m43b = 0;
            double m44b = 0;

            double m11c = 0;
            double m12c = 0;
            double m13c = 0;
            double m14c = 0;
            double m21c = 0;
            double m22c = 0;
            double m23c = 0;
            double m24c = 0;
            double m31c = 0;
            double m32c = 0;
            double m33c = 0;
            double m34c = 0;
            double m41c = 0;
            double m42c = 0;
            double m43c = 0;
            double m44c = 0;


            double m11d = 0;
            double m12d = 0;
            double m13d = 0;
            double m14d = 0;
            double m21d = 0;
            double m22d = 0;
            double m23d = 0;
            double m24d = 0;
            double m31d = 0;
            double m32d = 0;
            double m33d = 0;
            double m34d = 0;
            double m41d = 0;
            double m42d = 0;
            double m43d = 0;
            double m44d = 0;*/








            
            var offsetX = 1;
            var offsetY = 1;
            var offsetZ = 1;

            var offsetInstancedShips = offsetinstance;// 2.5f;
            


            //somechunk = new chunkscreen[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];
            //somechunk = new chunkmesh[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];

            Vector4 ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
            Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
            Vector4 lightDirection = new Vector4(0, -1, -1, 1.0f);
            Vector4 dirLight = new Vector4(0, -1, 0, 1.0f);
            Vector4 lightpos = new Vector4(0, 20, 0, 1.0f);



            lightBuffer = new sclevelgenclassPrim.DLightBufferEr[1];
            lightBuffer[0] = new sclevelgenclassPrim.DLightBufferEr()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                //padding0 = 35,
                lightPosition = lightpos,
                lightextras = lightpos,
                //padding1 = 100
            };













           


            /*
            arrayOfInstanceIndices = new int[levelgen.typeoftiles.Count][];
            arrayOfDVertex = new DVertex[levelgen.typeoftiles.Count][];

            arrayofchunks = new sclevelgenvert[levelgen.typeoftiles.Count];
            arrayofchunksmaps = new sclevelgenmaps[levelgen.typeoftiles.Count];

            sclevelgenmaps sometrigchunkmap;
            sclevelgenvert newChunker;





            int[] somemap;

            var enumerator1 = levelgen.typeoftiles.GetEnumerator();
            int ichunkleveltile = 0;

            while (enumerator1.MoveNext())
            {
                var currentTuile = enumerator1.Current;

                var chunkPos = currentTuile.Key;
                var typeofterraintile = currentTuile.Value;

                sometrigchunkmap = new sclevelgenmaps();
                newChunker = new sclevelgenvert();

                Vector3 newchunkpos = chunkPos;

                newchunkpos.X = newchunkpos.X * (tinyChunkWidth * planeSize);
                newchunkpos.Y = newchunkpos.Y * (tinyChunkHeight * planeSize);
                newchunkpos.Z = newchunkpos.Z * (tinyChunkDepth * planeSize);

                //chunkPos.X *= 2.0f;
                //chunkPos.Y *= 2.0f;
                //chunkPos.Z *= 2.0f;

                //int[] tester = 
                sometrigchunkmap.buildchunkmaps(newchunkpos, out somemap, planeSize, levelgen, chunkPos, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, typeofterraintile, this);

                arrayofchunksmaps[ichunkleveltile] = sometrigchunkmap;

                arrayofchunks[ichunkleveltile] = newChunker;
                arrayofchunks[ichunkleveltile].chunkPos = chunkPos;
                arrayofchunks[ichunkleveltile].map = somemap;

                ichunkleveltile++;
            }


            int[] indicesArray0;
            DVertex[] arrayOfD;

            //Vector3 someoffset = new Vector3(2,2,2) * 0.15f;
            enumerator1 = levelgen.typeoftiles.GetEnumerator();
            ichunkleveltile = 0;

            while (enumerator1.MoveNext())
            {
                var currentTuile = enumerator1.Current;
                var chunkPos = currentTuile.Key;

                Vector3 newchunkpos = chunkPos;

                newchunkpos.X = newchunkpos.X * (tinyChunkWidth * planeSize);
                newchunkpos.Y = newchunkpos.Y * (tinyChunkHeight * planeSize);
                newchunkpos.Z = newchunkpos.Z * (tinyChunkDepth * planeSize);

                //chunkPos.X *= 2.0f;
                //chunkPos.Y *= 2.0f;
                //chunkPos.Z *= 2.0f;

                //Console.WriteLine(chunkPos);

                var typeofterraintile = currentTuile.Value;
                //, out normals, out texturesCoordinates                
                int[] tester = arrayofchunks[ichunkleveltile].startBuildingArray(newchunkpos, out indicesArray0, out somemap, out arrayOfD, planeSize, levelgen, chunkPos, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, typeofterraintile, this, arrayofchunksmaps[ichunkleveltile].map);

                //arrayOfInstanceVertex[ichunkleveltile] = vertexArray0; //new Vector4(vertexArray0[v].X, vertexArray0[v].Y, vertexArray0[v].Z, 1);
                
                arrayOfInstanceIndices[ichunkleveltile] = indicesArray0;
                arrayOfDVertex[ichunkleveltile] = arrayOfD;



                //arrayOfInstanceNormals[ichunkleveltile] = normals;
                //arrayOfInstanceTexturesCoordinates[ichunkleveltile] = texturesCoordinates;

                //instances[ichunkleveltile] = new Vector4[1];
                //instances[ichunkleveltile][0] = new Vector4(position.X, position.Y, position.Z, 1);
                //Console.WriteLine(newchunkpos);

                //Console.WriteLine(chunkPos);

                /*instances[ichunkleveltile] = new DInstanceType()
                {
                    position = newchunkpos,
                };

                ichunkleveltile++;
            }*/



























            for (int x = 0; x < numberOfInstancesPerObjectInWidth; x++)
            {
                for (int y = 0; y < numberOfInstancesPerObjectInHeight; y++)
                {
                    for (int z = 0; z < numberOfInstancesPerObjectInDepth; z++)
                    {
                        var chunkinstindex = x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z);
                        //somechunk[chunkinstindex] = new chunkscreen();
                        /*somechunk[chunkinstindex] = new chunkmesh();


                        position = new Vector4(x * offsetInstancedShips, y * offsetInstancedShips, z * offsetInstancedShips, 1);

                        position.X *= (tinyChunkWidth);
                        position.Y *= (tinyChunkHeight);
                        position.Z *= (tinyChunkDepth);

                        position.X *= (planeSize);
                        position.Y *= (planeSize);
                        position.Z *= (planeSize);

                        position.X += (_chunkPos.X);
                        position.Y += (_chunkPos.Y);
                        position.Z += (_chunkPos.Z);

                        */






                        /*position.X *= (numberOfInstancesPerObjectInWidth * 1) * offsetX;
                        position.Y *= (numberOfInstancesPerObjectInHeight * 1) * offsetY;
                        position.Z *= (numberOfInstancesPerObjectInDepth * 1) * offsetZ;
                        */



                        //somechunk[chunkinstindex].somechunkpos = position;



                        Matrix _tempMatrix = maininstancematrix;






                        /*Vector3 somevec = new Vector3(somechunk[chunkinstindex].somechunkpos.X, somechunk[chunkinstindex].somechunkpos.Y, somechunk[chunkinstindex].somechunkpos.Z);


                        somevec += maininstancematrix.M41;
                        somevec += maininstancematrix.M42;
                        somevec += maininstancematrix.M43;


                        Matrix tempMatrixinst = Matrix.Identity;
                        tempMatrixinst.M41 = somevec.X;
                        tempMatrixinst.M42 = somevec.Y;
                        tempMatrixinst.M43 = somevec.Z;*/

                        ///somechunk[chunkinstindex].somechunkpos.X += maininstancematrix.M41;
                        //somechunk[chunkinstindex].somechunkpos.Y += maininstancematrix.M42;
                        //somechunk[chunkinstindex].somechunkpos.Z += maininstancematrix.M43;
                        /*
                        Quaternion somedirquat;
                        Quaternion.RotationMatrix(ref _tempMatrix, out somedirquat);

                        var dirInstanceRight = sc_maths._getDirection(Vector3.Right, somedirquat);
                        var dirInstanceUp = sc_maths._newgetdirup(somedirquat);
                        var dirInstanceForward = sc_maths._newgetdirforward(somedirquat);

                        //position = position - (new Vector4(dirInstanceRight.X, dirInstanceRight.Y, dirInstanceRight.Z, 1.0f) * (tinyChunkWidth * planeSize * 0.5f));
                        //position = position - (new Vector4(dirInstanceUp.X, dirInstanceUp.Y, dirInstanceUp.Z, 1.0f) * (tinyChunkWidth * planeSize * 0.5f));
                        //position = position + (new Vector4(dirInstanceForward.X, dirInstanceForward.Y, dirInstanceForward.Z, 1.0f) * (tinyChunkWidth * planeSize * 0.5f));

                        Vector4 somevecx = new Vector4(dirInstanceRight.X, dirInstanceRight.Y, dirInstanceRight.Z, 1.0f);
                        Vector4 somevecy = new Vector4(dirInstanceUp.X, dirInstanceUp.Y, dirInstanceUp.Z, 1.0f);
                        Vector4 somevecz = new Vector4(dirInstanceForward.X, dirInstanceForward.Y, dirInstanceForward.Z, 1.0f);

                        Vector4 somemainpos = new Vector4(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43, 1.0f);
                        Vector4 someinstancepostest = somemainpos;// Vector4.Zero;

                        someinstancepostest = someinstancepostest + (somevecx * ((x * offsetInstancedShips) * tinyChunkWidth * planeSize));
                        someinstancepostest = someinstancepostest + (somevecy * ((y * offsetInstancedShips) * tinyChunkWidth * planeSize));
                        someinstancepostest = someinstancepostest + (somevecz * ((z * offsetInstancedShips) * tinyChunkWidth * planeSize));

                        position = someinstancepostest;
                        */
                        //FIX FOR SOME INVERTED X AXIS THING GOING ON IN MY ENGINE.
                        //FIX FOR SOME INVERTED X AXIS THING GOING ON IN MY ENGINE.
                        //position.X *= -1;
                        //FIX FOR SOME INVERTED X AXIS THING GOING ON IN MY ENGINE.
                        //FIX FOR SOME INVERTED X AXIS THING GOING ON IN MY ENGINE.





                        //MOVING THE PIVOT POINT OF THE INSTANCES TO BE IN THE MIDDLE OF THE CHUNK
                        //MOVING THE PIVOT POINT OF THE INSTANCES TO BE IN THE MIDDLE OF THE CHUNK
                        //MOVING THE PIVOT POINT OF THE INSTANCES TO BE IN THE MIDDLE OF THE CHUNK
                        /*if (voxeltype == 0)
                        {
                            position = position - (somevecx * numberOfInstancesPerObjectInWidth * tinyChunkWidth * planeSize * 0.5f);
                            position = position - (somevecy * numberOfInstancesPerObjectInHeight * tinyChunkHeight * planeSize * 0.5f);
                            position = position - (somevecz * numberOfInstancesPerObjectInDepth * tinyChunkDepth * planeSize * 0.5f);
                        }*/
                        //MOVING THE PIVOT POINT OF THE INSTANCES TO BE IN THE MIDDLE OF THE CHUNK
                        //MOVING THE PIVOT POINT OF THE INSTANCES TO BE IN THE MIDDLE OF THE CHUNK
                        //MOVING THE PIVOT POINT OF THE INSTANCES TO BE IN THE MIDDLE OF THE CHUNK





                        /*somemainpos = somemainpos + (somevecx * (x * tinyChunkWidth * planeSize));
                        somemainpos = somemainpos + (somevecy * (y * tinyChunkWidth * planeSize));
                        somemainpos = somemainpos + (somevecz * (z * tinyChunkWidth * planeSize));



                        _tempMatrix.M41 = somemainpos.X;
                        _tempMatrix.M42 = somemainpos.Y;
                        _tempMatrix.M43 = somemainpos.Z;*/






                        //instancesIndex[chunkinstindex] = new DInstanceType[tinyChunkWidth * tinyChunkHeight * tinyChunkDepth];

                        //swtcsetneighboors = 1;
                        //int swtcsetneighboors = -1;

                        //somechunk[chunkinstindex].startBuildingArray(position, out m11, out m12, out m13, out m14, out m21, out m22, out m23, out m24, out m31, out m32, out m33, out m34, out m41, out m42, out m43, out m44, out theMap, this, 1, null, numberofmainobjectx, numberofmainobjecty, numberofmainobjectz, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, voxeltype, mapofbytes, mainix, mainiy, mainiz, meshzeroix, meshzeroiy, meshzeroiz, x, y, z); //, somechunkkeyboardpriminstanceindex_, chunkprimindex_, chunkinstindex


                        /*
                        somechunk[chunkinstindex].startBuildingArray(position,
                            out m11, out m12, out m13, out m14, out m21, out m22, out m23, out m24, out m31, out m32, out m33, out m34, out m41, out m42, out m43, out m44, out theMap, this, 
                            swtcsetneighboors, null, numberofmainobjectx, numberofmainobjecty, numberofmainobjectz, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth,
                            numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, voxeltype, mapofbytes,
                            mainix, mainiy, mainiz, meshzeroix, meshzeroiy, meshzeroiz, x, y, z
                           , out m11b, out m12b, out m13b, out m14b, out m21b, out m22b, out m23b, out m24b, out m31b, out m32b, out m33b, out m34b, out m41b, out m42b, out m43b, out m44b,
                           out m11c, out m12c, out m13c, out m14c, out m21c, out m22c, out m23c, out m24c, out m31c, out m32c, out m33c, out m34c, out m41c, out m42c, out m43c, out m44c,
                           out m11d, out m12d, out m13d, out m14d, out m21d, out m22d, out m23d, out m24d, out m31d, out m32d, out m33d, out m34d, out m41d, out m42d, out m43d, out m44d, -1); //, somechunkkeyboardpriminstanceindex_, chunkprimindex_, chunkinstindex
                        */

                        DVertex[] somelistofvert;
                        //int[] somemap;

                        //somechunkmesh.startBuildingArray(new Vector4(0, 0, 0, 1), out somelistofvert, out indices, out theMap, null);
                        /*somechunk[chunkinstindex].startBuildingArray(position,
                         out m11, out m12, out m13, out m14, out m21, out m22, out m23, out m24, out m31, out m32, out m33, out m34, out m41, out m42, out m43, out m44, out theMap, this,
                         swtcsetneighboors, null, numberofmainobjectx, numberofmainobjecty, numberofmainobjectz, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth,
                         numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, voxeltype, mapofbytes,
                         mainix, mainiy, mainiz, meshzeroix, meshzeroiy, meshzeroiz, x, y, z
                        , out m11b, out m12b, out m13b, out m14b, out m21b, out m22b, out m23b, out m24b, out m31b, out m32b, out m33b, out m34b, out m41b, out m42b, out m43b, out m44b,
                        out m11c, out m12c, out m13c, out m14c, out m21c, out m22c, out m23c, out m24c, out m31c, out m32c, out m33c, out m34c, out m41c, out m42c, out m43c, out m44c,
                        out m11d, out m12d, out m13d, out m14d, out m21d, out m22d, out m23d, out m24d, out m31d, out m32d, out m33d, out m34d, out m41d, out m42d, out m43d, out m44d, -1); //, somechunkkeyboardpriminstanceindex_, chunkprimindex_, chunkinstindex
                        */













                        /*
                        if (othervoxeltype == 4)
                        {
                            //Console.WriteLine(somechunk[chunkinstindex].vertexlist.Count);
                        }*/

                        
                        arrayofzeromeshinstances[chunkinstindex] = new sclevelgenclasscore();
                        //arrayofzeromeshinstances[chunkinstindex].map = arrayofchunks[chunkinstindex].map;

                     








                        /*sccstrigvertbuilderreduced = new sccstrigvertbuilderscreenreduced();
                        sccstrigvertbuilderreduced.startBuildingArray(new Vector4(somechunkpriminstancepos.X, somechunkpriminstancepos.Y, somechunkpriminstancepos.Z, 1), out chunkarrayofverts, out chunkarrayofindices, out originalMap, numberOfInstancesPerObjectInWidth_, numberOfInstancesPerObjectInHeight_, numberOfInstancesPerObjectInDepth_, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, this, null, null, fullface, voxeltype);

                        Console.WriteLine(chunkarrayofverts.Length);
                        */




                        _tempMatrix.M41 = arrayofchunks[chunkinstindex].chunkPos.X;
                        _tempMatrix.M42 = arrayofchunks[chunkinstindex].chunkPos.Y;
                        _tempMatrix.M43 = arrayofchunks[chunkinstindex].chunkPos.Z;




                        /*
                        if (someobjectmapchunk.map[chunkinstindex] == 1)
                        {

                            somechunk[chunkinstindex].startBuildingArray(position, out oner, out twoer, out threer, out fourer, out onerTwo, out twoerTwo, out threerTwo, out fourerTwo, out theMap, this, 1, null, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize); //, somechunkkeyboardpriminstanceindex_, chunkprimindex_, chunkinstindex

                            arrayofzeromeshinstances[chunkinstindex] = new sclevelgenclasscore();
                            arrayofzeromeshinstances[chunkinstindex].map = somechunk[chunkinstindex].map;
                        }
                        else
                        {
                            MainWindow.MessageBox((IntPtr)0, "error", "sccs error", 0);

                            somechunk[chunkinstindex].startBuildingArray(position, out oner, out twoer, out threer, out fourer, out onerTwo, out twoerTwo, out threerTwo, out fourerTwo, out theMap, this, 0, null, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize);//, somechunkkeyboardpriminstanceindex_, chunkprimindex_, chunkinstindex
                            //somechunk[chunkinstindex].map = new int[tinyChunkWidth * tinyChunkHeight * tinyChunkDepth];
                            arrayofzeromeshinstances[chunkinstindex] = new sclevelgenclasscore();
                            arrayofzeromeshinstances[chunkinstindex].map = somechunk[chunkinstindex].map;

                        }*/








                        //vertexindexlist

                        //instancesIndex[chunkinstindex][0].instancePos = new Vector4(); 





                        /*
                        //64 positions Vector4 per chunk map array.
                        for (int i = 0;i < instancesIndex[chunkinstindex].Length;i++)
                        {

                            instancesIndex[chunkinstindex] = someoriginalmapNVerts[chunkinstindex].vertexindexlist.t;
                        }
                        */






                        //96 faces  





                        Matrix tempMatrixinst = _tempMatrix;



                        if (_addtoworld == 1)
                        {
                            arrayofzeromeshinstances[chunkinstindex].transform.Component.rigidbody = new RigidBody(new BoxShape(_sizeX * 2 * 1.01f, _sizeY * 2 * 1.01f, _sizeZ * 2 * 1.01f));
                            //arrayofzeromeshinstances[chunkinstindex].transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                            arrayofzeromeshinstances[chunkinstindex].transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(arrayofchunks[chunkinstindex].chunkPos.X, arrayofchunks[chunkinstindex].chunkPos.Y, arrayofchunks[chunkinstindex].chunkPos.Z);



                            arrayofzeromeshinstances[chunkinstindex].transform.Component.rigidbody.Orientation = Conversion.ToJitterMatrix(tempMatrixinst);
                            arrayofzeromeshinstances[chunkinstindex].transform.Component.rigidbody.LinearVelocity = new Jitter.LinearMath.JVector(0, 0, 0);
                            arrayofzeromeshinstances[chunkinstindex].transform.Component.rigidbody.IsStatic = _is_static;
                            arrayofzeromeshinstances[chunkinstindex].transform.Component.rigidbody.Tag = _tag;//SC_console_directx.BodyTag.physicsInstancedCube;

                            arrayofzeromeshinstances[chunkinstindex].transform.Component.rigidbody.Material.Restitution = 0.015f; //0.015f
                            arrayofzeromeshinstances[chunkinstindex].transform.Component.rigidbody.Material.StaticFriction = 0.55f; // 0.55f
                            arrayofzeromeshinstances[chunkinstindex].transform.Component.rigidbody.Material.KineticFriction = 0.55f; //0.55f
                            // _cube.transform.Component.rigidbody.Damping = RigidBody.DampingType.Linear;
                            //arrayofzeromeshinstances[chunkinstindex].transform.Component.rigidbody.AllowDeactivation = false;
                            arrayofzeromeshinstances[chunkinstindex].transform.Component.rigidbody.Mass = _mass;


                            //SC_Console_GRAPHICS._buo.Add(_cube.transform.Component.rigidbody, 3);
                            _the_world.AddBody(arrayofzeromeshinstances[chunkinstindex].transform.Component.rigidbody);



                        }





           

                        arrayofzeromeshinstances[chunkinstindex].current_pos = tempMatrixinst;
                        arrayofzeromeshinstances[chunkinstindex]._POSITION = tempMatrixinst;







                        /*
                        //instancesmatrices
                        instancesccsbytemapxyz[chunkinstindex] = new DInstancesccsbytemapxyz()
                        {
                            instancematrix = tempMatrixinst
                        };*/



                        float r = 0.35f;
                        float g = 0.95f;
                        float b = 0.15f;
                        float a = 1; //





                        /*
                        heightmapmatrix[chunkinstindex] = new heightmapinstance()
                        {
                            heightmapmat = new Matrix()// new Vector4(r, g, b, 0.1f)//255
                        };*/










                        /*somechunk[chunkinstindex].startBuildingArray(position, out oner, out twoer, out threer, out fourer, out onerTwo, out twoerTwo, out threerTwo, out fourerTwo, out theMap, this, 1, null);

                        arrayofzeromeshinstances[chunkinstindex] = new sclevelgenclasscore();
                        arrayofzeromeshinstances[chunkinstindex].map = somechunk[chunkinstindex].map;
                        */




                        //newChunker.map = arrayofzeromeshinstances[chunkinstindex].map;

                        //newChunker.startBuildingArray(position, out oner, out twoer, out threer, out fourer, out onerTwo, out twoerTwo, out threerTwo, out fourerTwo, out theMap, this, 1, newChunker.map);


                        //Matrix _tempMatrix = Matrix.Identity;



                        /*if (typeofvoxelmesh == 1)
                        {
                            somechunk[chunkinstindex].somechunkpos.X += maininstancematrix.M41;
                            somechunk[chunkinstindex].somechunkpos.Y += maininstancematrix.M42;
                            somechunk[chunkinstindex].somechunkpos.Z += maininstancematrix.M43;

                            _tempMatrix.M41 = somechunk[chunkinstindex].somechunkpos.X;
                            _tempMatrix.M42 = somechunk[chunkinstindex].somechunkpos.Y;
                            _tempMatrix.M43 = somechunk[chunkinstindex].somechunkpos.Z;
                        }*/




                        arrayofzeromeshinstances[chunkinstindex].current_pos = _tempMatrix;
                        /*
                        instances[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceType()
                        {
                            instancePos = new Vector4(somechunk[chunkinstindex].somechunkpos.X, somechunk[chunkinstindex].somechunkpos.Y, somechunk[chunkinstindex].somechunkpos.Z, 1),
                        };

                        */
                        /*instancesbytemaps_[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstancesByteMap()
                        {
                            one = oner,
                            oneTwo = onerTwo,
                            two = twoer,
                            twoTwo = twoerTwo,
                            three = threer,
                            threeTwo = threerTwo,
                            four = fourer,
                            fourTwo = fourerTwo,
                        };*/


                        /*
                        tessellationBuffer[chunkinstindex] = new sclevelgenclassPrim.DTessellationBufferType();
                        tessellationBuffer[chunkinstindex].tessellationAmount = 12;
                        tessellationBuffer[chunkinstindex].padding = new Vector3();

                        */
                        /*
                        Matrix somechunkmap = Matrix.Identity;

                        somechunkmap.M11 = (float)m11;
                        somechunkmap.M12 = (float)m12;
                        somechunkmap.M13 = (float)m13;
                        somechunkmap.M14 = (float)m14;

                        somechunkmap.M21 = (float)m21;
                        somechunkmap.M22 = (float)m22;
                        somechunkmap.M23 = (float)m23;
                        somechunkmap.M24 = (float)m24;

                        somechunkmap.M31 = (float)m31;
                        somechunkmap.M32 = (float)m32;
                        somechunkmap.M33 = (float)m33;
                        somechunkmap.M34 = (float)m34;

                        somechunkmap.M41 = (float)m41;
                        somechunkmap.M42 = (float)m42;
                        somechunkmap.M43 = (float)m43;
                        somechunkmap.M44 = (float)m44;

                        instancesmatrix[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceMatrix()
                        {
                            instancematrix = somechunkmap,
                        };




                        somechunkmap = Matrix.Identity;

                        somechunkmap.M11 = (float)m11b;
                        somechunkmap.M12 = (float)m12b;
                        somechunkmap.M13 = (float)m13b;
                        somechunkmap.M14 = (float)m14b;

                        somechunkmap.M21 = (float)m21b;
                        somechunkmap.M22 = (float)m22b;
                        somechunkmap.M23 = (float)m23b;
                        somechunkmap.M24 = (float)m24b;

                        somechunkmap.M31 = (float)m31b;
                        somechunkmap.M32 = (float)m32b;
                        somechunkmap.M33 = (float)m33b;
                        somechunkmap.M34 = (float)m34b;

                        somechunkmap.M41 = (float)m41b;
                        somechunkmap.M42 = (float)m42b;
                        somechunkmap.M43 = (float)m43b;
                        somechunkmap.M44 = (float)m44b;




                        instancesmatrixb[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceMatrix()
                        {
                            instancematrix = somechunkmap,
                        };
                        somechunkmap = Matrix.Identity;

                        somechunkmap.M11 = (float)m11c;
                        somechunkmap.M12 = (float)m12c;
                        somechunkmap.M13 = (float)m13c;
                        somechunkmap.M14 = (float)m14c;

                        somechunkmap.M21 = (float)m21c;
                        somechunkmap.M22 = (float)m22c;
                        somechunkmap.M23 = (float)m23c;
                        somechunkmap.M24 = (float)m24c;

                        somechunkmap.M31 = (float)m31c;
                        somechunkmap.M32 = (float)m32c;
                        somechunkmap.M33 = (float)m33c;
                        somechunkmap.M34 = (float)m34c;

                        somechunkmap.M41 = (float)m41c;
                        somechunkmap.M42 = (float)m42c;
                        somechunkmap.M43 = (float)m43c;
                        somechunkmap.M44 = (float)m44c;


                        instancesmatrixc[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceMatrix()
                        {
                            instancematrix = somechunkmap,
                        };

                        somechunkmap = Matrix.Identity;

                        somechunkmap.M11 = (float)m11d;
                        somechunkmap.M12 = (float)m12d;
                        somechunkmap.M13 = (float)m13d;
                        somechunkmap.M14 = (float)m14d;

                        somechunkmap.M21 = (float)m21d;
                        somechunkmap.M22 = (float)m22d;
                        somechunkmap.M23 = (float)m23d;
                        somechunkmap.M24 = (float)m24d;

                        somechunkmap.M31 = (float)m31d;
                        somechunkmap.M32 = (float)m32d;
                        somechunkmap.M33 = (float)m33d;
                        somechunkmap.M34 = (float)m34d;

                        somechunkmap.M41 = (float)m41d;
                        somechunkmap.M42 = (float)m42d;
                        somechunkmap.M43 = (float)m43d;
                        somechunkmap.M44 = (float)m44d;

                        instancesmatrixd[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceMatrix()
                        {
                            instancematrix = somechunkmap,
                        };
                        */





                        /*
                        instancesLocationW[chunkinstindex] = new DInstanceTypeLocW()
                        {
                            index = (numberofmainobjectx * numberOfInstancesPerObjectInWidth) - 1 - (x)- (mainix * x)
                            //index = x
                        };

                        instancesLocationH[chunkinstindex] = new DInstanceTypeLocH()
                        {
                            index = (numberofmainobjecty * numberOfInstancesPerObjectInHeight) - 1 - (y) - (mainiy * y) //instY - 1 - y
                        };*/




                        /*
                        instancesLocationW[chunkinstindex] = new DInstanceTypeLocW()
                        {
                            index = numberOfInstancesPerObjectInWidth - 1 - x
                            //index = x
                        };

                        instancesLocationH[chunkinstindex] = new DInstanceTypeLocH()
                        {
                            index = numberOfInstancesPerObjectInHeight - 1 - y //instY - 1 - y
                        };*/


                        /*
                        instancessccsbytemap[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstancesccsbytemapxyz()
                        {
                            xyzmap = Matrix.Identity,
                            //ymap = Matrix.Identity,
                            //zmap = Matrix.Identity,

                        };*/















                        /*instancesindex[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DIndexType()
                        {
                            indexPos = new Vector4(somechunk[chunkinstindex].somechunkpos.X, somechunk[chunkinstindex].somechunkpos.Y, somechunk[chunkinstindex].somechunkpos.Z, 1),
                        };*/


                        /*
                        instancesDataFORWARD[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceShipData()
                        {
                            instancePos = new Vector4(somechunk[chunkinstindex].somechunkpos.X, somechunk[chunkinstindex].somechunkpos.Y, somechunk[chunkinstindex].somechunkpos.Z, 1),
                        };
                        instancesDataRIGHT[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceShipData()
                        {
                            instancePos = new Vector4(somechunk[chunkinstindex].somechunkpos.X, somechunk[chunkinstindex].somechunkpos.Y, somechunk[chunkinstindex].somechunkpos.Z, 1),
                        };
                        instancesDataUP[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceShipData()
                        {
                            instancePos = new Vector4(somechunk[chunkinstindex].somechunkpos.X, somechunk[chunkinstindex].somechunkpos.Y, somechunk[chunkinstindex].somechunkpos.Z, 1),
                        };*/

                        //arrayOfSomeMap[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = theMap;


                        //break;
                    }
                }
            }


            //instancesbytemaps = instancesbytemaps_;




            /*
            for (int x = 0; x < numberOfInstancesPerObjectInWidth; x++)
            {
                for (int y = 0; y < numberOfInstancesPerObjectInHeight; y++)
                {
                    for (int z = 0; z < numberOfInstancesPerObjectInDepth; z++)
                    {
                        var chunkinstindex = x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z);

                        position = new Vector4(x * offsetInstancedShips, y * offsetInstancedShips, z * offsetInstancedShips, 1);
                        
                        /*position.X *= (numberOfInstancesPerObjectInWidth * 1) * offsetX;
                        position.Y *= (numberOfInstancesPerObjectInHeight * 1) * offsetY;
                        position.Z *= (numberOfInstancesPerObjectInDepth * 1) * offsetZ;
                        


                        position.X *= (tinyChunkWidth);
                        position.Y *= (tinyChunkHeight);
                        position.Z *= (tinyChunkDepth);

                        position.X *= (planeSize);
                        position.Y *= (planeSize);
                        position.Z *= (planeSize);

                        position.X += (_chunkPos.X);
                        position.Y += (_chunkPos.Y);
                        position.Z += (_chunkPos.Z);

                        //position.X += (offsetX);
                        //position.Y += (offsetY);
                        //position.Z += (offsetZ);

                        somechunk[chunkinstindex].startBuildingArray(somechunk[chunkinstindex].somechunkpos, out oner, out twoer, out threer, out fourer, out onerTwo, out twoerTwo, out threerTwo, out fourerTwo, out theMap, this, 2, arrayofzeromeshinstances[chunkinstindex].map);

                        Matrix _tempMatrix = Matrix.Identity;

                        /*
                        somechunk[chunkinstindex].somechunkpos.X += maininstancematrix.M41;
                        somechunk[chunkinstindex].somechunkpos.Y += maininstancematrix.M42;
                        somechunk[chunkinstindex].somechunkpos.Z += maininstancematrix.M43;

                        _tempMatrix.M41 = somechunk[chunkinstindex].somechunkpos.X;
                        _tempMatrix.M42 = somechunk[chunkinstindex].somechunkpos.Y;
                        _tempMatrix.M43 = somechunk[chunkinstindex].somechunkpos.Z;

                        arrayofzeromeshinstances[chunkinstindex].current_pos = _tempMatrix;

                        instances[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceType()
                        {
                            one = oner,
                            two = twoer,
                            three = threer,
                            four = fourer,
                            oneTwo = onerTwo,
                            twoTwo = twoerTwo,
                            threeTwo = threerTwo,
                            fourTwo = fourerTwo,
                            instancePos = new Vector4(somechunk[chunkinstindex].somechunkpos.X, somechunk[chunkinstindex].somechunkpos.Y, somechunk[chunkinstindex].somechunkpos.Z, 1),
                        };

                        instancesDataFORWARD[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceShipData()
                        {
                            instancePos = new Vector4(somechunk[chunkinstindex].somechunkpos.X, somechunk[chunkinstindex].somechunkpos.Y, somechunk[chunkinstindex].somechunkpos.Z, 1),
                        };
                        instancesDataRIGHT[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceShipData()
                        {
                            instancePos = new Vector4(somechunk[chunkinstindex].somechunkpos.X, somechunk[chunkinstindex].somechunkpos.Y, somechunk[chunkinstindex].somechunkpos.Z, 1),
                        };
                        instancesDataUP[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceShipData()
                        {
                            instancePos = new Vector4(somechunk[chunkinstindex].somechunkpos.X, somechunk[chunkinstindex].somechunkpos.Y, somechunk[chunkinstindex].somechunkpos.Z, 1),
                        };

                        arrayOfSomeMap[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = theMap;
                    }
                }
            }*/
        }

        //sclevelgenclass[] arrayOfChunks;
        //chunkData[] arrayOfChunkData;

        public int InstanceCount = 0;

        public Vector4 _color;

        public int instanceCounter { get; set; }
        public byte[] map { get; set; }

        public Vector3 _chunkPos { get; set; }

    }
}