﻿using System;
using System.Collections.Generic;
using SharpDX;



namespace sccsr15forms
{
    public class sccstrigvertbuilder : IDisposable
    {
        //public byte[] map;
        private int[] map;
        private int block;

        private Vector4 forward = new Vector4(0, 0, 1, 0);
        private Vector4 back = new Vector4(0, 0, 0, 0);
        private Vector4 right = new Vector4(1, 0, 0, 0);
        private Vector4 left = new Vector4(0, 0, 0, 0);
        private Vector4 up = new Vector4(0, 1, 0, 0);
        private Vector4 down = new Vector4(0, 0, 0, 0);

        float staticPlaneSize;
        float alternateStaticPlaneSize;
        private int _detailScale = 10; // 10
        private int _HeightScale = 200; //200
        int seed = 3420;

        FastNoise fastNoise = new FastNoise();


        public List<Vector4> vertexlist = new List<Vector4>();
        //public List<tutorialcubeaschunk.DVertex> vertexlist = new List<tutorialcubeaschunk.DVertex>();
        public List<int> listOfTriangleIndices = new List<int>();

        float padding0;
        float padding1;
        float padding2;

        int numberOfObjectInWidth; int numberOfObjectInHeight; int numberOfObjectInDepth;
        int numberOfInstancesPerObjectInWidth; int numberOfInstancesPerObjectInHeight; 
        int numberOfInstancesPerObjectInDepth; float planeSize;

        int tinyChunkWidth = 10; 
        int tinyChunkHeight = 10; 
        int tinyChunkDepth = 10;


        int fullface = 1;

        int voxeltype = 0;
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

            vertexlist = null;// new List<tutorialcubeaschunk.DVertex>();
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

            fullface = 0;

            voxeltype = 0;
            builddualface = 0;
        }


        Vector4 mcolor = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);

        public void startBuildingArray(Vector4 currentPosition, out Vector4[] vertexArray, out int[] triangleArray)
        {

            tinyChunkWidth = 10;
            tinyChunkHeight = 10;
            tinyChunkDepth = 10;
            planeSize = 0.1f;
            voxeltype = 0;



            /*fullface = fullface_;

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
            planeSize = planeSize_;*/

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

                        //map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] = 1;
                    }
                }
            }

            Regenerate(currentPosition, voxeltype);

            vertexArray = vertexlist.ToArray();
            triangleArray = listOfTriangleIndices.ToArray();

           // mapper = map;
        }

        public void Regenerate(Vector4 currentPosition, int voxeltype)
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
                            DrawBrick(x, y, z, currentPosition, block, voxeltype);

                        }

                    }
                }
            }
        }

        public void DrawBrick(int x, int y, int z, Vector4 currentPosition, int block, int voxeltype)
        {








            Vector4 start = new Vector4(x, y, z, 1.0f);
            Vector4 offset1, offset2;

            /*
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
            */

            
            //TOPFACE
            if (IsTransparent(x, y + 1, z))
            {
                //offset1 = forward * planeSize;
                //offset2 = right * planeSize;
                //createTopFace(start + up * planeSize, offset1, offset2, currentPosition, x, y, z, 1, 0);
                createTopFace(start);

            }

            //BOTTOMFACE
            if (IsTransparent(x, y - 1, z))
            {
                createBottomFace(start);
            }
            //RIGHTFACE
            if (IsTransparent(x + 1, y, z))
            {
                createRightFace(start);
            }
            //LEFTFACE
            if (IsTransparent(x - 1, y, z))
            {
                createleftFace(start);
            }
            //FRONTFACE
            if (IsTransparent(x, y, z - 1))
            {
                createFrontFace(start);
                //createFrontFace(start, offset1, offset2, currentPosition, x, y, z, 1, 0);

            }
            
            //BACKFACE
            if (IsTransparent(x, y, z + 1))
            {
                //offset1 = right * planeSize;
                //offset2 = up * planeSize;
                //createBackFace(start + forward * planeSize, offset1, offset2, currentPosition, x, y, z, 1, 0);
                createBackFace(start);

            }



            if (fullface == 0)
            {
               /* offset1 = left * planeSize;
                offset2 = up * planeSize;
                createFrontFace(start + right * planeSize, offset1, offset2, currentPosition, x, y, z, 0.0f, voxeltype);
               */
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


        private void createTopFace(Vector4 start)
        {
            int index = vertexlist.Count;


            mcolor = new Vector4(0.15f, 1.0f, 0.15f, 1.0f);
            vertexlist.Add((start + up + back) * planeSize);
            vertexlist.Add(mcolor);
            vertexlist.Add((start+ up + forward) * planeSize);
            vertexlist.Add(mcolor);
            vertexlist.Add((start + up + forward + right) * planeSize);
            vertexlist.Add(mcolor);
            vertexlist.Add((start + up + right + back) * planeSize);
            vertexlist.Add(mcolor);


            /*
            new Vector4(-1.0f, 1.0f, -1.0f, 1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f), // Top 
            new Vector4(-1.0f, 1.0f, 1.0f, 1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
            new Vector4(1.0f, 1.0f, 1.0f, 1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
            new Vector4(-1.0f, 1.0f, -1.0f, 1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
            new Vector4(1.0f, 1.0f, 1.0f, 1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
            new Vector4(1.0f, 1.0f, -1.0f, 1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),*/






            /*vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// mcolor,//new Vector4(x, y, z, block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z + 0.1f, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(0, 1),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z , block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(1, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z + 0.1f, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 1, 0),
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

                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 4);
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 4);
                listOfTriangleIndices.Add(index + 6);

                /*
                if (builddualface == 1)
                {
                    listOfTriangleIndices.Add(index + 0);
                    listOfTriangleIndices.Add(index + 1);
                    listOfTriangleIndices.Add(index + 2);
                    listOfTriangleIndices.Add(index + 3);
                    listOfTriangleIndices.Add(index + 2);
                    listOfTriangleIndices.Add(index + 1);
                }*/

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



        private void createBottomFace(Vector4 start)
        {

            mcolor = new Vector4(0.15f, 0.15f, 1.0f, 1.0f);

            int index = vertexlist.Count;
            /*vertexlist.Add(start);
            vertexlist.Add(mcolor);
            vertexlist.Add(start + offset1);
            vertexlist.Add(mcolor);
            vertexlist.Add(start + offset2);
            vertexlist.Add(mcolor);
            vertexlist.Add(start + offset1 + offset2);
            vertexlist.Add(mcolor);*/

            vertexlist.Add((start  + back) * planeSize);
            vertexlist.Add(mcolor);
            vertexlist.Add((start  + forward) * planeSize);
            vertexlist.Add(mcolor);
            vertexlist.Add((start  + forward + right) * planeSize);
            vertexlist.Add(mcolor);
            vertexlist.Add((start  + right + back) * planeSize);
            vertexlist.Add(mcolor);
            /*vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// mcolor,//new Vector4(x, y, z, block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z + 0.1f, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(0, 1),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z , block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(1, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z + 0.1f, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 1, 0),
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


                listOfTriangleIndices.Add(index + 4);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 6);
                listOfTriangleIndices.Add(index + 4);
                listOfTriangleIndices.Add(index + 0);

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

        private void createFrontFace(Vector4 start)
        {
            mcolor = new Vector4(1.0f, 1.0f, 0.15f, 1.0f);
            int index = vertexlist.Count;

            //Vector4 somestartpos = new Vector4(x, y, z, 1.0f);
            Vector4 someoffset = new Vector4(1.0f, 0, 0, 0) * 0.25f;
            //planeSize = 1.0f;
            vertexlist.Add(((start)*planeSize));
            vertexlist.Add(mcolor);
            vertexlist.Add(((start + up) * planeSize));
            vertexlist.Add(mcolor);
            vertexlist.Add(((start + right + up) * planeSize));
            vertexlist.Add(mcolor);
            vertexlist.Add(((start + right) * planeSize));
            vertexlist.Add(mcolor);



            /*
            vertexlist.Add(somestartpos);
            vertexlist.Add(mcolor);
            vertexlist.Add(somestartpos + Vector4.UnitY);
            vertexlist.Add(mcolor);
            vertexlist.Add(somestartpos + Vector4.UnitX + Vector4.UnitY);
            vertexlist.Add(mcolor);*/

            /*
            vertexlist.Add(somestartpos);
            vertexlist.Add(mcolor);
            vertexlist.Add(somestartpos + right + up);
            vertexlist.Add(mcolor);
            vertexlist.Add(somestartpos + right);
            vertexlist.Add(mcolor);*/




            /*
            vertexlist.Add(start + offset1 + offset2);
            vertexlist.Add(mcolor);
            */













            /*
            int index0 = vertexlist.Count;
            vertexlist.Add(new Vector4(0, 0, 0, 1.0f) ); //bottom left 0
            vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f)); //1

            int index1 = vertexlist.Count;
            vertexlist.Add(new Vector4(0, 1.0f, 0, 1.0f) );//top left 2
            vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));//3

            int index2 = vertexlist.Count;
            vertexlist.Add(new Vector4(1.0f, 1.0f, 0, 1.0f));//top right 4
            vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));//5

            int index3 = vertexlist.Count;
            vertexlist.Add(new Vector4(0, 0, 0, 1.0f));//bottom left 6
            vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));//7

            int index4 = vertexlist.Count;
            vertexlist.Add(new Vector4(1.0f, 1.0f, 0, 1.0f));//top right 8
            vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));//9

            int index5 = vertexlist.Count;
            vertexlist.Add(new Vector4(1.0f, 0, 0, 1.0f));//bottom right 10
            vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));*/








            /*
                 int index0 = vertexlist.Count;
                 vertexlist.Add(new Vector4(-1.0f, -1.0f, -1.0f, 1.0f) + start); //bottom left 0
                 vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f)); //1

                 int index1 = vertexlist.Count;
                 vertexlist.Add(new Vector4(-1.0f, 1.0f, -1.0f, 1.0f) + start);//top left 2
                 vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));//3

                 int index2 = vertexlist.Count;
                 vertexlist.Add(new Vector4(1.0f, 1.0f, -1.0f, 1.0f) + start);//top right 4
                 vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));//5

                 int index3= vertexlist.Count;
                 vertexlist.Add(new Vector4(-1.0f, -1.0f, -1.0f, 1.0f) + start);//bottom left 6
                 vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));//7

                 int index4 = vertexlist.Count;
                 vertexlist.Add(new Vector4(1.0f, 1.0f, -1.0f, 1.0f) + start);//top right 8
                 vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));//9

                 int index5 = vertexlist.Count;
                 vertexlist.Add(new Vector4(1.0f, -1.0f, -1.0f, 1.0f) + start);//bottom right 10
                 vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));//10*/







            /*
            int index0 = vertexlist.Count;
            vertexlist.Add(new Vector4(-1.0f , -1.0f , -1.0f , 1.0f ) + (start )); //bottom left
            vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));

            int index1 = vertexlist.Count;
            vertexlist.Add(new Vector4(-1.0f , 1.0f , -1.0f , 1.0f ) + (start ));//top left
            vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));

            int index2 = vertexlist.Count;
            vertexlist.Add(new Vector4(1.0f , 1.0f , -1.0f , 1.0f ) + (start ));//top right
            vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));
            
            int index3 = vertexlist.Count;
            vertexlist.Add(new Vector4(1.0f , -1.0f , -1.0f , 1.0f ) + (start ));//bottom right
            vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));
            */








            /*
            int index0 = vertexlist.Count;
            vertexlist.Add(new Vector4(-1.0f, -1.0f, -1.0f, 1.0f) + (start)); //bottom left
            vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));

            int index1 = vertexlist.Count;
            vertexlist.Add(new Vector4(-1.0f, 1.0f, -1.0f, 1.0f) + (start));//top left
            vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));

            int index2 = vertexlist.Count;
            vertexlist.Add(new Vector4(1.0f, 1.0f, -1.0f, 1.0f) + (start));//top right
            vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));

            int index3 = vertexlist.Count;
            vertexlist.Add(new Vector4(1.0f, -1.0f, -1.0f, 1.0f) + (start));//bottom right
            vertexlist.Add(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));
            */







            /*vertexlist.Add(start);
            vertexlist.Add(mcolor);
            vertexlist.Add(start + offset1);
            vertexlist.Add(mcolor);
            vertexlist.Add(start + offset2);
            vertexlist.Add(mcolor);
            vertexlist.Add(start + offset1 + offset2);
            vertexlist.Add(mcolor);*/

            /*vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// mcolor,//new Vector4(x, y, z, block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z + 0.1f, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(0, 1),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z , block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(1, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z + 0.1f, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(1, 1),
                padding1 = padding1,
                padding2 = padding2,
            });*/


            /*
            int index = vertexlist.Count;
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 0, 1),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 0, 1),
                padding0 = padding0,
                tex = new Vector2(0, 1),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 0, 1),
                padding0 = padding0,
                tex = new Vector2(1, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1 + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 0, 1),
                padding0 = padding0,
                tex = new Vector2(1, 1),
                padding1 = padding1,
                padding2 = padding2,
            });*/
            if (voxeltype == 0)
            {
                
                listOfTriangleIndices.Add(index + (0));
                listOfTriangleIndices.Add(index + (2));
                listOfTriangleIndices.Add(index + (4));
                listOfTriangleIndices.Add(index + (0));
                listOfTriangleIndices.Add(index + (4));
                listOfTriangleIndices.Add(index + (6));


                /*listOfTriangleIndices.Add(index + (6));
                listOfTriangleIndices.Add(index + (8));
                listOfTriangleIndices.Add(index + (10));*/


                /*
                listOfTriangleIndices.Add(index0);
                listOfTriangleIndices.Add(index1);
                listOfTriangleIndices.Add(index2);*/
                /*listOfTriangleIndices.Add(index3);
                listOfTriangleIndices.Add(index4);
                listOfTriangleIndices.Add(index5);*/

                /*
                listOfTriangleIndices.Add(index + (2));
                listOfTriangleIndices.Add(index + (1));
                listOfTriangleIndices.Add(index + (0));
                listOfTriangleIndices.Add(index + (1));
                listOfTriangleIndices.Add(index + (2));
                listOfTriangleIndices.Add(index + (3));*/


                /*listOfTriangleIndices.Add(index + (2 * 2));
                listOfTriangleIndices.Add(index + (1 * 2));
                listOfTriangleIndices.Add(index + (0 * 2));
                listOfTriangleIndices.Add(index + (1 * 2));
                listOfTriangleIndices.Add(index + (2 * 2));
                listOfTriangleIndices.Add(index + (3 * 2));*/


                /*
                listOfTriangleIndices.Add(index + (0 ));
                listOfTriangleIndices.Add(index + (2 ));
                listOfTriangleIndices.Add(index + (4 ));
                listOfTriangleIndices.Add(index + (6 ));
                listOfTriangleIndices.Add(index + (8 ));
                listOfTriangleIndices.Add(index + (10 ));*/




                /*
                listOfTriangleIndices.Add(index + (2 * 3));
                listOfTriangleIndices.Add(index + (1 * 3));
                listOfTriangleIndices.Add(index + (0 * 3));
                listOfTriangleIndices.Add(index + (1 * 3));
                listOfTriangleIndices.Add(index + (2 * 3));
                listOfTriangleIndices.Add(index + (3 * 3));*/
                /*
                listOfTriangleIndices.Add(index0);
                listOfTriangleIndices.Add(index1);
                listOfTriangleIndices.Add(index2);
                listOfTriangleIndices.Add(index2);
                listOfTriangleIndices.Add(index0);
                listOfTriangleIndices.Add(index3);
                */
                if (builddualface == 1)
                {
                    /*listOfTriangleIndices.Add(index + 0);
                    listOfTriangleIndices.Add(index + 1);
                    listOfTriangleIndices.Add(index + 2);
                    listOfTriangleIndices.Add(index + 3);
                    listOfTriangleIndices.Add(index + 2);
                    listOfTriangleIndices.Add(index + 1);*/
                }
            }
            else if (voxeltype == 1)
            {
                /*listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 0);*/
            }
            else if (voxeltype == 2)
            {
                /*
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);*/
            }
        }
        private void createBackFace(Vector4 start)
        {
            mcolor = new Vector4(1.0f, 0.15f, 0.15f, 1.0f);
            int index = vertexlist.Count;
            /*vertexlist.Add((start + forward) * planeSize);
            vertexlist.Add(mcolor);
            vertexlist.Add((start + forward + up) * planeSize);
            vertexlist.Add(mcolor);
            vertexlist.Add((start + forward + up + left) * planeSize);
            vertexlist.Add(mcolor);
            vertexlist.Add((start + forward + left) * planeSize);
            vertexlist.Add(mcolor);*/

            vertexlist.Add(((start + forward) * planeSize));
            vertexlist.Add(mcolor);
            vertexlist.Add(((start + forward + up) * planeSize));
            vertexlist.Add(mcolor);
            vertexlist.Add(((start + forward + right + up) * planeSize));
            vertexlist.Add(mcolor);
            vertexlist.Add(((start + forward + right) * planeSize));
            vertexlist.Add(mcolor);


            /*vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// mcolor,//new Vector4(x, y, z, block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z + 0.1f, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(0, 1),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z , block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(1, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z + 0.1f, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(1, 1),
                padding1 = padding1,
                padding2 = padding2,
            });*/





            //SCCS 2 MILLION VIRTUAL DESKTOP EXAMple.
            /*int index = vertexlist.Count;
            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start,
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z, block),
                normal = new Vector3(0, 0, -1),
                padding0 = padding0,
                tex = new Vector2(0, 0), // 0 0 // 1 1
                padding1 = padding1,
                padding2 = padding2,
            });

            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1,
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z, block),
                normal = new Vector3(0, 0, -1),
                padding0 = padding0,
                tex = new Vector2(0, 1.0f), // 0 1 // 1 0
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z, block),
                normal = new Vector3(0, 0, -1),
                padding0 = padding0,
                tex = new Vector2(1.0f, 0), // 1 0 // 0 1
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1 + offset2,
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z, block),
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


                /*listOfTriangleIndices.Add(index + 2);
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
                }*/
                listOfTriangleIndices.Add(index + 4);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 6);
                listOfTriangleIndices.Add(index + 4);
                listOfTriangleIndices.Add(index + 0);
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

        private void createRightFace(Vector4 start)
        {

            mcolor = new Vector4(0.5f, 1.0f, 0.5f, 1.0f);
            int index = vertexlist.Count;

            vertexlist.Add((start + right) * planeSize);
            vertexlist.Add(mcolor);
            vertexlist.Add((start + right + up) * planeSize);
            vertexlist.Add(mcolor);
            vertexlist.Add((start + right + up + forward) * planeSize);
            vertexlist.Add(mcolor);
            vertexlist.Add((start + right + down + forward) * planeSize);
            vertexlist.Add(mcolor);

            /*vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// mcolor,//new Vector4(x, y, z, block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z + 0.1f, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(0, 1),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z , block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(1, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z + 0.1f, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 1, 0),
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


                /*listOfTriangleIndices.Add(index + 2);
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
                }*/

                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 4);
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 4);
                listOfTriangleIndices.Add(index + 6);


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

        private void createleftFace(Vector4 start)
        {

            mcolor = new Vector4(0.5f, 0.5f, 0.5f, 1.0f);

            int index = vertexlist.Count;
            vertexlist.Add((start + left) * planeSize);
            vertexlist.Add(mcolor);
            vertexlist.Add((start + left + up) * planeSize);
            vertexlist.Add(mcolor);
            vertexlist.Add((start + left + up + forward) * planeSize);
            vertexlist.Add(mcolor);
            vertexlist.Add((start + left + down + forward) * planeSize);
            vertexlist.Add(mcolor);

            /*vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// mcolor,//new Vector4(x, y, z, block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z + 0.1f, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(0, 1),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z , block),
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(1, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            vertexlist.Add(new tutorialcubeaschunk.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z + 0.1f, block),
                //indexPos = new Vector4(x, y, z, block),
                color = mcolor,// new Vector4(x, y, z + 0.1f, block),
                normal = new Vector3(0, 1, 0),
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


                /*listOfTriangleIndices.Add(index + 2);
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
                }*/

                listOfTriangleIndices.Add(index + 4);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 6);
                listOfTriangleIndices.Add(index + 4);
                listOfTriangleIndices.Add(index + 0);
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

