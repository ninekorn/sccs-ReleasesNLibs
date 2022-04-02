using System;
using System.Collections.Generic;
using SharpDX;

namespace sccs
{
    public class sccstrigvertbuilderscreen : IDisposable
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

        SC_instancedChunk_instances componentParentthischunk; SC_instancedChunkPrim componentParentprim; SC_instancedChunk componentParentinstance;

        int fullface;

        int voxeltype;
        int builddualface = 0;

        public void Dispose()
        {
            //public byte[] map;
            map = null;
            block = 0;

            /*private Vector4 forward = new Vector4(0, 0, 1, 1);
            private Vector4 back = new Vector4(0, 0, -1, 1);
            private Vector4 right = new Vector4(1, 0, 0, 1);
            private Vector4 left = new Vector4(-1, 0, 0, 1);
            private Vector4 up = new Vector4(0, 1, 0, 1);
            private Vector4 down = new Vector4(0, -1, 0, 1);
            */
            staticPlaneSize = 0;
            alternateStaticPlaneSize = 0;
            _detailScale = 0; // 10
            _HeightScale = 0; //200
            seed = 0;

            vertexlist = null;// new List<SC_instancedChunk.DVertex>();
            listOfTriangleIndices = null;// new List<int>();

            padding0 = 0;
            padding1 = 0;
            padding2 = 0;

            numberOfObjectInWidth = 0;
            numberOfObjectInHeight = 0;
            numberOfObjectInDepth = 0;
            numberOfInstancesPerObjectInWidth = 0;
            numberOfInstancesPerObjectInHeight = 0;
            numberOfInstancesPerObjectInDepth = 0;
            planeSize = 0;

            tinyChunkWidth = 0;
            tinyChunkHeight = 0;
            tinyChunkDepth = 0;

            componentParentthischunk = null;
            componentParentprim = null;
            componentParentinstance = null;

            fullface = 0;

            voxeltype = 0;
            builddualface = 0;
        }
        public void startBuildingArray(Vector4 currentPosition, out SC_instancedChunk.DVertex[] vertexArray, out int[] triangleArray, out int[] mapper, float padding0_, float padding1_, float padding2_, int numberOfObjectInWidth_, int numberOfObjectInHeight_, int numberOfObjectInDepth_, int numberOfInstancesPerObjectInWidth_, int numberOfInstancesPerObjectInHeight_, int numberOfInstancesPerObjectInDepth_, int tinyChunkWidth_, int tinyChunkHeight_, int tinyChunkDepth_, float planeSize_, SC_instancedChunkPrim componentParentprim_, SC_instancedChunk componentParentinstance_, SC_instancedChunk_instances componentParentthischunk_, int fullface_, int voxeltype_)
        {
            fullface = fullface_;

            voxeltype = voxeltype_;
            //voxeltype = 2;

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






            /*int chunkwidthl = (tinyChunkWidth / 2);
            int chunkwidthr = (tinyChunkWidth / 2) - 1;

            int chunkheightl = (tinyChunkHeight / 2);
            int chunkheightr = (tinyChunkHeight / 2) - 1;

            int chunkdepthl = (tinyChunkDepth / 2);
            int chunkdepthr = (tinyChunkDepth / 2) - 1;


            var sometotal = (chunkwidthl + chunkwidthr + 1) * (chunkheightl + chunkheightr + 1) * (chunkdepthl + chunkdepthr + 1);
            */




            var sometotal = (tinyChunkWidth) * (tinyChunkHeight) * (tinyChunkDepth);

            int posx = 0;
            int posy = 0;
            int posz = 0;
            int xx = tinyChunkWidth;
            int yy = tinyChunkHeight;
            int zz = tinyChunkDepth;
            int swtchx = 0;
            int swtchy = 0;
            int swtchz = 0;


            for (int i = 0; i < sometotal; i++)
            {
                //if (t0 < sometotal)
                {
 
                    if (zz >= (tinyChunkDepth))
                    {
                        xx++;
                        zz = 0;
                        swtchx = 1;
                    }
                    if (xx >= (tinyChunkWidth) && swtchx == 1)
                    {
                        //faceStart = 0;
                        yy++;
                        xx = 0;
                        swtchx = 0;
                        swtchy = 1;
                    }
                    if (yy >= (tinyChunkHeight) && swtchy == 1)
                    {
                        yy = 0;
                        swtchy = 0;
                        swtchx = 0;
                        swtchz = 1;
                    }

                    /*if (xi < 0)
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
                    }*/

                    var someindexmain = xx + (tinyChunkWidth) * (yy + (tinyChunkHeight) * zz);

                    if (someindexmain < sometotal)
                    {
                        map[someindexmain] = 1;
                    }
                    else
                    {
                        ////t = sometotal;
                        //taskcancelFlagTwo = 1;
                    }

                    zz++;
                  
                    //t++;
                }
            }




            /*
            for (int x = 0; x < tinyChunkWidth; x++)
            {
                for (int y = 0; y < tinyChunkHeight; y++)
                {
                    for (int z = 0; z < tinyChunkDepth; z++)
                    {
                        float noiseXZ = 20;

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
                        }

                        map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                    }
                }
            }
            */
    














            Regenerate(currentPosition, voxeltype);

            vertexArray = vertexlist.ToArray();
            triangleArray = listOfTriangleIndices.ToArray();

            mapper = map;
        }

        public void Regenerate(Vector4 currentPosition, int voxeltype)
        {



            
            var sometotal = (tinyChunkWidth) * (tinyChunkHeight) * (tinyChunkDepth);

            int posx = 0;
            int posy = 0;
            int posz = 0;
            int xx = tinyChunkWidth;
            int yy = tinyChunkHeight;
            int zz = tinyChunkDepth;
            int swtchx = 0;
            int swtchy = 0;
            int swtchz = 0;


            for (int i = 0; i < sometotal;)
            {

           
                if (zz >= (tinyChunkDepth))
                {
                    xx++;
                    zz = 0;
                    swtchx = 1;
                }
                if (xx >= (tinyChunkWidth) && swtchx == 1)
                {
                    //faceStart = 0;
                    yy++;
                    xx = 0;
                    swtchx = 0;
                    swtchy = 1;
                }
                if (yy >= (tinyChunkHeight) && swtchy == 1)
                {
                    yy = 0;
                    swtchy = 0;
                    swtchx = 0;
                    swtchz = 1;
                }

                var someindexmain = xx + tinyChunkWidth * (yy + tinyChunkHeight * zz);

                if (someindexmain < map.Length)
                {
                    block = map[someindexmain];

                    if (block == 1)
                    {

                    }
                    DrawBrick(xx, yy, zz, currentPosition, block, voxeltype);
                }
                else
                {
                    //Program.MessageBox((IntPtr)0, "" + someindexmain, "sccsmsg", 0);

                    ////t = sometotal;
                    //taskcancelFlagTwo = 1;
                }


                //Console.WriteLine("x:" + xx + " y:" + yy + " z:" + zz);




                zz++;
                //t++;
                i++;


                /*zz++;
                if (zz > (tinyChunkDepth-1))
                {
                    xx++;
                    zz = 0;
                    swtchx = 1;
                }
                if (xx > (tinyChunkWidth-1) && swtchx == 1)
                {
                    yy++;
                    xx = 0;
                    swtchx = 0;
                    swtchy = 1;
                }
                if (yy > (tinyChunkHeight-1) && swtchy == 1)
                {
                    yy = 0;
                    swtchy = 0;
                    swtchx = 0;
                    swtchz = 1;
                }*/

            }


            /*
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
                        DrawBrick(x, y, z, currentPosition, block, voxeltype);

                    }
                }
            }*/
        }

        public void DrawBrick(int x, int y, int z, Vector4 currentPosition, int block, int voxeltype)
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
                /*if (Program._useOculusRift == 0)
                {
                    offset1 = left * planeSize;
                    offset2 = up * planeSize;
                    createFrontFace(start + right * planeSize, offset1, offset2, currentPosition, x, y, z, 0.0f, voxeltype);
                }
                else if (Program._useOculusRift == 1)
                {
                    offset1 = left * planeSize;
                    offset2 = up * planeSize;
                    createFrontFace(start + right * planeSize, offset1, offset2, currentPosition, x, y, z, 0.0f, voxeltype);
                }
                */


                offset1 = left * planeSize;
                offset2 = up * planeSize;
                createFrontFace(start + right * planeSize, offset1, offset2, currentPosition, x, y, z, 0.0f, voxeltype);

                
                offset1 = right * planeSize;
                offset2 = up * planeSize;
                createBackFace(start + forward * planeSize, offset1, offset2, currentPosition, x, y, z, 4.0f, voxeltype);
                
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
                color = new Vector4(x, y, z, block),
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
                color = new Vector4(x, y, z + 0.1f, block),
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
                color = new Vector4(x, y, z , block),
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
                color = new Vector4(x, y, z + 0.1f, block),
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
            else if (voxeltype == 2)
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
                color = new Vector4(x, y, z, block),
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
                color = new Vector4(x, y, z , block),
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
                color = new Vector4(x, y, z + 0.1f, block),
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
                color = new Vector4(x, y, z + 0.1f, block),
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
            else if (voxeltype == 2)
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
                color = new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 0, 1),
                padding0 = padding0,
                tex = new Vector2(1, 1),
                padding1 = padding1,
                padding2 = padding2,
            });

            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z + 0.1f, block),
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
                color = new Vector4(x, y, z + 0.1f, block),
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
                color = new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 0, 1),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });



            /*
            int index = vertexlist.Count;
            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z + 0.1f, block),
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
                color = new Vector4(x, y, z + 0.1f, block),
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
                color = new Vector4(x, y, z + 0.1f, block),
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
                color = new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 0, 1),
                padding0 = padding0,
                tex = new Vector2(1, 1),
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
            else if (voxeltype == 2)
            {

                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
            }
        }
        private void createBackFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, float block, int voxeltype)
        {
            int index = vertexlist.Count;
            vertexlist.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = new Vector4(x, y, z, block),
                normal = new Vector3(0, 0, -1),
                padding0 = padding0,
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
                padding0 = padding0,
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
                padding0 = padding0,
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
                padding0 = padding0,
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
            else if (voxeltype == 2)
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
                color = new Vector4(x, y, z, block),
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
                color = new Vector4(x, y, z , block),
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
                color = new Vector4(x, y, z + 0.1f, block),
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
                color = new Vector4(x, y, z + 0.1f, block),
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
            else if (voxeltype == 2)
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
                color = new Vector4(x, y, z + 0.1f, block),
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
                color = new Vector4(x, y, z, block),
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
                color = new Vector4(x, y, z + 0.1f, block),
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
                color = new Vector4(x, y, z , block),
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
            else if (voxeltype == 2)
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

