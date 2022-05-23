using System;
using System.Collections.Generic;
using SharpDX;

namespace sccs
{
    public class sccstrigvertbuilderscreenmorescreens
    {
        //public byte[] map;
        private int[] map;
        private int block;

        private Vector4 forward = new Vector4(0, 0, 1, 1);
        private Vector4 back = new Vector4(0, 0, -1, 1);
        private Vector4 right = new Vector4(1, 0, 0, 1);
        private Vector4 left = new Vector4(-1, 0, 0, 1);
        private Vector4 up = new Vector4(0, 1, 0, 1);
        private Vector4 down = new Vector4(0, -1, 0, 1);

        float staticPlaneSize;
        float alternateStaticPlaneSize;
        private int _detailScale = 10; // 10
        private int _HeightScale = 200; //200
        int seed = 3420;

        public List<SC_instancedChunk.DVertex> vertexlist = new List<SC_instancedChunk.DVertex>();
        public List<int> listOfTriangleIndices = new List<int>();

        float padding0;
        float padding1;
        float padding2;

        int numberOfObjectInWidth; int numberOfObjectInHeight; int numberOfObjectInDepth; int numberOfInstancesPerObjectInWidth; int numberOfInstancesPerObjectInHeight; int numberOfInstancesPerObjectInDepth; float planeSize;

        int tinyChunkWidth; int tinyChunkHeight; int tinyChunkDepth;

        SC_instancedChunk_instances componentParentthischunk;
        SC_instancedChunkPrim componentParentprim;
        SC_instancedChunk componentParentinstance;

        int fullface;

        int voxeltype;
        int builddualface = 0;

        public void startBuildingArray(Vector4 currentPosition, out SC_instancedChunk.DVertex[] vertexArray, out int[] triangleArray, out int[] mapper, float padding0_, float padding1_, float padding2_, int numberOfObjectInWidth_, int numberOfObjectInHeight_, int numberOfObjectInDepth_, int numberOfInstancesPerObjectInWidth_, int numberOfInstancesPerObjectInHeight_, int numberOfInstancesPerObjectInDepth_, int tinyChunkWidth_, int tinyChunkHeight_, int tinyChunkDepth_, float planeSize_, SC_instancedChunkPrim componentParentprim_, SC_instancedChunk componentParentinstance_, SC_instancedChunk_instances componentParentthischunk_, int fullface_, int voxeltype_, int indexobjw, int indexobjh, int indexobjd)
        {
            fullface = fullface_;

            voxeltype = voxeltype_;

            //voxeltype = 0;




            componentParentthischunk = componentParentthischunk_;
            componentParentprim = componentParentprim_;
            componentParentinstance = componentParentinstance_;

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
            planeSize = planeSize_;

            map = new int[tinyChunkWidth * tinyChunkHeight * tinyChunkDepth];

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

            //FastNoise fastNoise = new FastNoise();

            for (int x = 0; x < tinyChunkWidth; x++)
            {
                for (int y = 0; y < tinyChunkHeight; y++)
                {
                    for (int z = 0; z < tinyChunkDepth; z++)
                    {
                        /*float noiseXZ = 20;

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
                        }*/

                        map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                    }
                }
            }

            Regenerate(currentPosition, voxeltype, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, indexobjw, indexobjh, indexobjd);

            vertexArray = vertexlist.ToArray();
            triangleArray = listOfTriangleIndices.ToArray();

            mapper = map;
        }

        public void Regenerate(Vector4 currentPosition, int voxeltype, int numberOfObjectInWidth, int numberOfObjectInHeight, int numberOfObjectInDepth, int indexobjw, int indexobjh, int indexobjd)
        {
            for (int x = 0; x < tinyChunkWidth; x++)
            {
                for (int y = 0; y < tinyChunkHeight; y++)
                {
                    for (int z = 0; z < tinyChunkDepth; z++)
                    {
                        block = map[x + tinyChunkWidth * (y + tinyChunkHeight * z)];

                        if (block == 1)
                        {

                        }
                        DrawBrick(x, y, z, currentPosition, block, voxeltype, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, indexobjw, indexobjh, indexobjd);
                    }
                }
            }
        }

        public void DrawBrick(int x, int y, int z, Vector4 currentPosition, int block, int voxeltype, int numberOfObjectInWidth, int numberOfObjectInHeight, int numberOfObjectInDepth, int indexobjw, int indexobjh, int indexobjd)
        {
            Vector4 start = new Vector4(x * planeSize, y * planeSize, z * planeSize, 1);
            Vector4 offset1, offset2;

            if (fullface == 0)
            {
                offset1 = left * planeSize;
                offset2 = up * planeSize;
                createFrontFace(start + right * planeSize, offset1, offset2, currentPosition, x, y, z, 0.0f, voxeltype);

            }
            else if (fullface == 1)
            {
                
                /*
                offset1 = right * planeSize;
                offset2 = up * planeSize;
                createBackFace(start + forward * planeSize, offset1, offset2, currentPosition, x, y, z, 4.0f, voxeltype, indexobjw, indexobjh, indexobjd);
              */
                
                offset1 = left * planeSize;
                offset2 = up * planeSize;
                createFrontFace(start + right * planeSize, offset1, offset2, currentPosition, x, y, z, 0.0f, voxeltype);
                

                /*
                offset1 = forward * planeSize;
                offset2 = right * planeSize;
                createTopFace(start + up * planeSize, offset1, offset2, currentPosition, x, y, z, 1.0f, voxeltype);
                
                offset1 = back * planeSize;
                offset2 = down * planeSize;
                createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2, currentPosition, x, y, z, 2.0f, voxeltype);
                
                offset1 = up * planeSize;
                offset2 = forward * planeSize;
                createRightFace(start + right * planeSize, offset1, offset2, currentPosition, x, y, z, 3.0f, voxeltype);
                
                offset1 = right * planeSize;
                offset2 = forward * planeSize;
                createBottomFace(start, offset1, offset2, currentPosition, x, y, z, 5.0f, voxeltype);            
                */

                /*
                //TOPFACE
                if (IsTransparent(x, y + 1, z))
                {
                    offset1 = forward * planeSize;
                    offset2 = right * planeSize;
                    createTopFace(start + up * planeSize, offset1, offset2, currentPosition, x, y, z, 1);
                }

                //BOTTOMFACE
                if (IsTransparent(x, y - 1, z))
                {
                    offset1 = right * planeSize;
                    offset2 = forward * planeSize;
                    createBottomFace(start, offset1, offset2, currentPosition, x, y, z, 1);
                }
                //RIGHTFACE
                if (IsTransparent(x + 1, y, z))
                {
                    offset1 = up * planeSize;
                    offset2 = forward * planeSize;
                    createRightFace(start + right * planeSize, offset1, offset2, currentPosition, x, y, z, 1);
                }
                //LEFTFACE
                if (IsTransparent(x - 1, y, z))
                {
                    offset1 = back * planeSize;
                    offset2 = down * planeSize;
                    createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2, currentPosition, x, y, z, 1);
                }
                //FRONTFACE
                if (IsTransparent(x, y, z - 1))
                {
                    offset1 = left * planeSize;
                    offset2 = up * planeSize;
                    createFrontFace(start + right * planeSize, offset1, offset2, currentPosition, x, y, z, 1);
                }

                //BACKFACE
                if (IsTransparent(x, y, z + 1))
                {
                    offset1 = right * planeSize;
                    offset2 = up * planeSize;
                    createBackFace(start + forward * planeSize, offset1, offset2, currentPosition, x, y, z, 1);
                }*/
            }
        }


        private void createTopFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, float block, int voxeltype)
        {

            float xx = x;
            float yy = y;
            float zz = z;


            int index = vertexlist.Count;
            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z , block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(0, 1),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(1, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1 + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z , block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(1, 1),
                padding1 = padding1,
                padding2 = padding2,
            });



            if (voxeltype == 0)
            {
                /*listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);*/


                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);

                if (builddualface == 1)
                {
                    listOfTriangleIndices.Add(index + 0);
                    listOfTriangleIndices.Add(index + 1);
                    listOfTriangleIndices.Add(index + 2);
                    listOfTriangleIndices.Add(index + 3);
                    listOfTriangleIndices.Add(index + 2);
                    listOfTriangleIndices.Add(index + 1);
                }
                
            }
            else if (voxeltype == 1)
            {
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 0);
            }
            else if (voxeltype == 2 || voxeltype == 3)
            {
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
            }
        }



        private void createBottomFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, float block, int voxeltype)
        {
            int index = vertexlist.Count;
            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, -1, 0),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, -1, 0),
                padding0 = padding0,
                tex = new Vector2(0, 1),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z , block),
                normal = new Vector3(0, -1, 0),
                padding0 = padding0,
                tex = new Vector2(1, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1 + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z, block),
                normal = new Vector3(0, -1, 0),
                padding0 = padding0,
                tex = new Vector2(1, 1),
                padding1 = padding1,
                padding2 = padding2,
            });



            if (voxeltype == 0)
            {
                /*listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);*/


                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);

                if (builddualface == 1)
                {
                    listOfTriangleIndices.Add(index + 0);
                    listOfTriangleIndices.Add(index + 1);
                    listOfTriangleIndices.Add(index + 2);
                    listOfTriangleIndices.Add(index + 3);
                    listOfTriangleIndices.Add(index + 2);
                    listOfTriangleIndices.Add(index + 1);
                }
            }
            else if (voxeltype == 1)
            {
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 0);
            }
            else if (voxeltype == 2 || voxeltype == 3)
            {
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
            }


        }

        private void createFrontFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, float block, int voxeltype)
        {
            int index = vertexlist.Count;
            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z, block),
                normal = new Vector3(0, 0, 1),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z, block),
                normal = new Vector3(0, 0, 1),
                padding0 = padding0,
                tex = new Vector2(0, 1),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z, block),
                normal = new Vector3(0, 0, 1),
                padding0 = padding0,
                tex = new Vector2(1, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1 + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z, block),
                normal = new Vector3(0, 0, 1),
                padding0 = padding0,
                tex = new Vector2(1, 1),
                padding1 = padding1,
                padding2 = padding2,
            });


            if (voxeltype == 0)
            {
                /*listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);*/


                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                if (builddualface == 1)
                {
                    listOfTriangleIndices.Add(index + 0);
                    listOfTriangleIndices.Add(index + 1);
                    listOfTriangleIndices.Add(index + 2);
                    listOfTriangleIndices.Add(index + 3);
                    listOfTriangleIndices.Add(index + 2);
                    listOfTriangleIndices.Add(index + 1);
                }
            }
            else if (voxeltype == 1)
            {
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 0);
            }
            else if (voxeltype == 2 || voxeltype == 3)
            {
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
            }
        }
        private void createBackFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, float block, int voxeltype, int indexobjw, int indexobjh, int indexobjd)
        {

            float padding = indexobjh * 0.1f;
            float nooiscreen = indexobjw + padding;

            //Console.WriteLine("pad " + nooiscreen);

            int index = vertexlist.Count;
            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z, block),
                normal = new Vector3(0, 0, -1),
                padding0 = nooiscreen,
                tex = new Vector2(0, 1), // 0 0
                padding1 = padding1,
                padding2 = padding2,
            });

            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z, block),
                normal = new Vector3(0, 0, -1),
                padding0 = nooiscreen,
                tex = new Vector2(1, 1), // 0 1
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z, block),
                normal = new Vector3(0, 0, -1),
                padding0 = nooiscreen,
                tex = new Vector2(0, 0), // 1 0
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1 + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z, block),
                normal = new Vector3(0, 0, -1),
                padding0 = nooiscreen,
                tex = new Vector2(1, 0), // 1 1
                padding1 = padding1,
                padding2 = padding2,
            });





            //SCCS 2 MILLION VIRTUAL DESKTOP EXAMple.
            /*int index = vertexlist.Count;
            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z, block),
                normal = new Vector3(0, 0, -1),
                padding0 = padding0,
                tex = new Vector2(0, 0), // 0 0 // 1 1
                padding1 = padding1,
                padding2 = padding2,
            });

            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z, block),
                normal = new Vector3(0, 0, -1),
                padding0 = padding0,
                tex = new Vector2(0, 1.0f), // 0 1 // 1 0
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z, block),
                normal = new Vector3(0, 0, -1),
                padding0 = padding0,
                tex = new Vector2(1.0f, 0), // 1 0 // 0 1
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1 + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z, block),
                normal = new Vector3(0, 0, -1),
                padding0 = padding0,
                tex = new Vector2(1.0f, 1.0f), // 1 1 // 0 0
                padding1 = padding1,
                padding2 = padding2,
            });*/


            if (voxeltype == 0)
            {
                /*listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);*/


                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);

                if (builddualface == 1)
                {
                    listOfTriangleIndices.Add(index + 0);
                    listOfTriangleIndices.Add(index + 1);
                    listOfTriangleIndices.Add(index + 2);
                    listOfTriangleIndices.Add(index + 3);
                    listOfTriangleIndices.Add(index + 2);
                    listOfTriangleIndices.Add(index + 1);
                }
            }
            else if (voxeltype == 1)
            {
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 0);
            }
            else if (voxeltype == 2 || voxeltype == 3)
            {
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);


            }
        }

        private void createRightFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, float block, int voxeltype)
        {
            int index = vertexlist.Count;
            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(1, 0, 0),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(1, 0, 0),
                padding0 = padding0,
                tex = new Vector2(0, 1),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z, block),
                normal = new Vector3(1, 0, 0),
                padding0 = padding0,
                tex = new Vector2(1, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1 + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z, block),
                normal = new Vector3(1, 0, 0),
                padding0 = padding0,
                tex = new Vector2(1, 1),
                padding1 = padding1,
                padding2 = padding2,
            });



            if (voxeltype == 0)
            {
                /*listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);*/


                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);

                if (builddualface == 1)
                {
                    listOfTriangleIndices.Add(index + 0);
                    listOfTriangleIndices.Add(index + 1);
                    listOfTriangleIndices.Add(index + 2);
                    listOfTriangleIndices.Add(index + 3);
                    listOfTriangleIndices.Add(index + 2);
                    listOfTriangleIndices.Add(index + 1);
                }
            }
            else if (voxeltype == 1)
            {
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 0);
            }
            else if (voxeltype == 2 || voxeltype == 3)
            {
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
            }
        }

        private void createleftFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, float block, int voxeltype)
        {


            float xx = x;
            float yy = y;
            float zz = z;

            int index = vertexlist.Count;
            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z, block),
                normal = new Vector3(-1, 0, 0),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(-1, 0, 0),
                padding0 = padding0,
                tex = new Vector2(0, 1),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z, block),
                normal = new Vector3(-1, 0, 0),
                padding0 = padding0,
                tex = new Vector2(1, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1 + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(-1, 0, 0),
                padding0 = padding0,
                tex = new Vector2(1, 1),
                padding1 = padding1,
                padding2 = padding2,
            });



            if (voxeltype == 0)
            {
                /*listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);*/


                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);

                if (builddualface == 1)
                {
                    listOfTriangleIndices.Add(index + 0);
                    listOfTriangleIndices.Add(index + 1);
                    listOfTriangleIndices.Add(index + 2);
                    listOfTriangleIndices.Add(index + 3);
                    listOfTriangleIndices.Add(index + 2);
                    listOfTriangleIndices.Add(index + 1);
                }
            }
            else if (voxeltype == 1)
            {
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 0);
            }
            else if (voxeltype == 2 || voxeltype == 3)
            {
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
            }
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

