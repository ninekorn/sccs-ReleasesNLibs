using System;
using System.Collections.Generic;
using SharpDX;

namespace sccsr15forms
{
    public class vertforinstances
    {
        //public byte[] map;
        private int[] map;
        private int block;

        private Vector4 forward = new Vector4(0, 0, 1, 0);
        private Vector4 back = new Vector4(0, 0, -1, 0);
        private Vector4 right = new Vector4(1, 0, 0, 0);
        private Vector4 left = new Vector4(-1, 0, 0, 0);
        private Vector4 up = new Vector4(0, 1, 0, 0);
        private Vector4 down = new Vector4(0, -1, 0, 0);

        float staticPlaneSize;
        float alternateStaticPlaneSize;
        private int _detailScale = 10; // 10
        private int _HeightScale = 200; //200
        int seed = 3420;

        private List<tutorialcubeaschunkinst.DVertex> listOfVerts = new List<tutorialcubeaschunkinst.DVertex>();
        private List<int> listOfTriangleIndices = new List<int>();

        int somewidth = 4;
        int someheight = 4;
        int somedepth = 4;

        float planeSize = 1.0f;

        float padding0 = 0;
        float padding1 = 0;
        float padding2= 0;

        int facetype;

        public void startBuildingArray(Vector4 currentPosition, out tutorialcubeaschunkinst.DVertex[] vertexArray, out int[] triangleArray, float planeSize_,int w,int h, int d, int numberofmesh, int facetype_)
        {

            facetype = facetype_;

            somewidth = w;
            someheight = h;
            somedepth = d;

            padding0 = w;// + somewidth * (h + someheight * d);
            padding1 = h;//
            padding2 = d;//

            ////Console.WriteLine("/w:" + w + "/h:" + h + "/d:" + d);

            planeSize = planeSize_;
            //planeSize = 1.0f;


            map = new int[numberofmesh];

            FastNoise fastNoise = new FastNoise();

            int somecounter = 0;
            /*for (int x = 0; x < somewidth; x++)
            {
                for (int y = 0; y < someheight; y++)
                {
                    for (int z = 0; z < somedepth; z++)
                    {
                        /*float noiseXZ = 20;

                        noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (currentPosition.X * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale, (((y * staticPlaneSize) + (currentPosition.Y * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale, (((z * staticPlaneSize) + (currentPosition.Z * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale);

                        ////Console.WriteLine(noiseXZ);

                        if (noiseXZ >= 0.1f)
                        {
                            map[x + somewidth * (y + someheight * z)] = 1;
                        }
                        else if (y == 0 && currentPosition.Y == 0)
                        {
                            map[x + somewidth * (y + someheight * z)] = 1;
                        }
                        else
                        {
                            map[x + somewidth * (y + someheight * z)] = 0;
                        }
                        
                        map[x + somewidth * (y + someheight * z)] = 1;
                        somecounter++;
                    }
                }
            }*/



            for (int i = 0; i < map.Length; i++)
            {
                map[i] = 1;
                somecounter++;
            }














            ////Console.WriteLine("c:" + somecounter);

            /*
            for (int i = 0;i < map.Length;i++)
            {
                map[i] = 1;
            }
            */



            Regenerate(currentPosition);

            vertexArray = listOfVerts.ToArray();
            triangleArray = listOfTriangleIndices.ToArray();

            //mapper = map;
            map = null;
        }

        public void Regenerate(Vector4 currentPosition)
        {
            int someixx = 0;
            int someiyy = 0;
            int someizz = 0;



            for (int i = 0; i < map.Length; i++)
            {

                int someflatindex = someixx + somewidth * ((someiyy) + someheight * (someizz));
               
                block = map[i];

                //if (block == 1)
                {
                    DrawBrick(someixx, someiyy, someizz, currentPosition, block);
                }

                someizz++;
                if (someizz == 4)
                {
                    someiyy++;
                    someizz = 0;
                }
                if (someiyy == 4)
                {
                    someixx++;
                    someiyy = 0;
                }
                if (someixx == 4)
                {
                    someixx = 0;
                }
            }
            /*for (int x = 0; x < somewidth; x++)
            {
                for (int y = 0; y < someheight; y++)
                {
                    for (int z = 0; z < somedepth; z++)
                    {
                        block = map[x + somewidth * (y + someheight * z)];

                        //if (block == 1)
                        {
                            DrawBrick(x, y, z, currentPosition, block);
                        }
                    }
                }
            }*/
        }

        public void DrawBrick(int x, int y, int z, Vector4 currentPosition, int block)
        {
            Vector4 start = new Vector4(0 * planeSize, 0 * planeSize, 0 * planeSize, 1);
            Vector4 offset1, offset2;

            if (facetype == 0)
            {
                //TOPFACE
                //if (IsTransparent(x, y + 1, z))
                {
                    offset1 = forward * planeSize;
                    offset2 = right * planeSize;
                    createTopFace(start + up * planeSize, offset1, offset2, currentPosition, x, y, z, 1);
                }
            }
            else if(facetype == 1)
            {
                //LEFTFACE
                //if (IsTransparent(x - 1, y, z))
                {
                    offset1 = back * planeSize;
                    offset2 = down * planeSize;
                    createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2, currentPosition, x, y, z, 1);
                }
            }
            else if (facetype == 2)
            {
                //RIGHTFACE
                //if (IsTransparent(x + 1, y, z))
                {
                    offset1 = up * planeSize;
                    offset2 = forward * planeSize;
                    createRightFace(start + right * planeSize, offset1, offset2, currentPosition, x, y, z, 1);
                }
            }
            else if (facetype == 3)
            {
                //FRONTFACE
                //if (IsTransparent(x, y, z - 1))
                {
                    offset1 = left * planeSize;
                    offset2 = up * planeSize;
                    createFrontFace(start + right * planeSize, offset1, offset2, currentPosition, x, y, z, 1);

                }
            }
            else if (facetype == 4)
            {
                //BACKFACE
                //if (IsTransparent(x, y, z + 1))
                {
                    offset1 = right * planeSize;
                    offset2 = up * planeSize;
                    createBackFace(start + forward * planeSize, offset1, offset2, currentPosition, x, y, z, 1);

                }
            }
            else if (facetype == 5)
            {
                //BOTTOMFACE
                //if (IsTransparent(x, y - 1, z))
                {
                    offset1 = right * planeSize;
                    offset2 = forward * planeSize;
                    createBottomFace(start, offset1, offset2, currentPosition, x, y, z, 1);
                }

            }














            /*
            offset1 = forward * planeSize;
            offset2 = right * planeSize;
            createTopFace(start + up * planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = back * planeSize;
            offset2 = down * planeSize;
            createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = up * planeSize;
            offset2 = forward * planeSize;
            createRightFace(start + right * planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = left * planeSize;
            offset2 = up * planeSize;
            createFrontFace(start + right * planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = right * planeSize;
            offset2 = up * planeSize;
            createBackFace(start + forward * planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = right * planeSize;
            offset2 = forward * planeSize;
            createBottomFace(start, offset1, offset2, currentPosition, x, y, z, 1);*/
        }


        private void createTopFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block)
        {

            ////Console.WriteLine("x:" + x + "/y:" + y + "/z:" + z);

            int index = listOfVerts.Count;

            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, 1),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start, // + offset1
                indexPos = new Vector4(x, y, z, 2),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                padding0 = padding0,
                tex = new Vector2(0, 1),
                padding1 = padding1,
                padding2 = padding2,
            });


            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start, // + offset2
                indexPos = new Vector4(x, y, z, 3),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                padding0 = padding0,
                tex = new Vector2(1, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start, // + offset1 + offset2
                indexPos = new Vector4(x, y, z, 4),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                padding0 = padding0,
                tex = new Vector2(1f, 1),
                padding1 = padding1,
                padding2 = padding2,
            });


            if (Program.useOculusRift == 0)
            {
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
            }
            else if (Program.useOculusRift == 1)
            {
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
            }
          
        }






        private void createleftFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block)
        {
            int index = listOfVerts.Count;
            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, 5),
                color = new Vector4(0.25f, 0.75f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,// + offset1,
                indexPos = new Vector4(x, y, z, 6),
                color = new Vector4(0.25f, 0.75f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,// + offset2,
                indexPos = new Vector4(x, y, z, 7),
                color = new Vector4(0.25f, 0.75f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,// + offset1 + offset2,
                indexPos = new Vector4(x, y, z, 8),
                color = new Vector4(0.25f, 0.75f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            if (Program.useOculusRift == 0)
            {
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
            }
            else if (Program.useOculusRift == 1)
            {
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
            }

        }





        private void createRightFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, 9),
                color = new Vector4(0.75f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,//  + offset1,
                indexPos = new Vector4(x, y, z, 10),
                color = new Vector4(0.75f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,//  + offset2,
                indexPos = new Vector4(x, y, z, 11),
                color = new Vector4(0.75f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,//  + offset1 + offset2,
                indexPos = new Vector4(x, y, z, 12),
                color = new Vector4(0.75f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            if (Program.useOculusRift == 0)
            {
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);

              
            }
            else if (Program.useOculusRift == 1)
            {
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
            }

         
        }


        private void createFrontFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, 13),
                color = new Vector4(0.25f, 0.25f, 0.75f, 1),
                normal = new Vector3(-1, 0, 0),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,// + offset1,
                indexPos = new Vector4(x, y, z, 14),
                color = new Vector4(0.25f, 0.25f, 0.75f, 1),
                normal = new Vector3(-1, 0, 0),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,//  + offset2,
                indexPos = new Vector4(x, y, z, 15),
                color = new Vector4(0.25f, 0.25f, 0.75f, 1),
                normal = new Vector3(-1, 0, 0),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,//  + offset1 + offset2,
                indexPos = new Vector4(x, y, z, 16),
                color = new Vector4(0.25f, 0.25f, 0.75f, 1),
                normal = new Vector3(-1, 0, 0),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });



            if (Program.useOculusRift == 0)
            {
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);

        


            }
            else if (Program.useOculusRift == 1)
            {
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
            }


        }


        private void createBackFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, 17),
                color = new Vector4(0.75f, 0.75f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,// + offset1,
                indexPos = new Vector4(x, y, z, 18),
                color = new Vector4(0.75f, 0.75f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,//  + offset2,
                indexPos = new Vector4(x, y, z, 19),
                color = new Vector4(0.75f, 0.75f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,// + offset1 + offset2,
                indexPos = new Vector4(x, y, z, 20),
                color = new Vector4(0.75f, 0.75f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });
            if (Program.useOculusRift == 0)
            {
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);


            }
            else if (Program.useOculusRift == 1)
            {
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
            }
        }

        private void createBottomFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block)
        {
            int index = listOfVerts.Count;
            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, 21),
                color = new Vector4(0.25f, 0.75f, 0.75f, 1),
                normal = new Vector3(0, 1, -1),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,// + offset1,
                indexPos = new Vector4(x, y, z, 22),
                color = new Vector4(0.25f, 0.75f, 0.75f, 1),
                normal = new Vector3(0, 1, -1),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });


            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,//  + offset2,
                indexPos = new Vector4(x, y, z, 23),
                normal = new Vector3(0, 1, -1),
                color = new Vector4(0.25f, 0.75f, 0.75f, 1),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,

            });


            listOfVerts.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,//  + offset1 + offset2,
                indexPos = new Vector4(x, y, z, 24),
                color = new Vector4(0.25f, 0.75f, 0.75f, 1),
                normal = new Vector3(0, 1, -1),
                padding0 = padding0,
                tex = new Vector2(0, 0),
                padding1 = padding1,
                padding2 = padding2,
            });

            if (Program.useOculusRift == 0)
            {
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 0);
                listOfTriangleIndices.Add(index + 1);
                listOfTriangleIndices.Add(index + 2);
                listOfTriangleIndices.Add(index + 3);


            }
            else if (Program.useOculusRift == 1)
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
            if ((x < 0) || (y < 0) || (z < 0) || (x >= somewidth) || (y >= someheight) || (z >= somedepth)) return true;
            {
                return map[x + somewidth * (y + someheight * z)] == 0;
            }
        }
    }
}

