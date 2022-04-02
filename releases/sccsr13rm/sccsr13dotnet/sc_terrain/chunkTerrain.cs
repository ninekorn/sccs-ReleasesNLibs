using System;
using System.Collections.Generic;
using SharpDX;
using SCCoreSystems.SC_Graphics;


namespace SCCoreSystems
{
    public class chunkTerrain
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

        private int seed = 3420; // 3420

        private int _detailScale = 10; // 10
        private int _HeightScale = 200; //200


        private List<SC_instancedChunk.DVertex> listOfVerts = new List<SC_instancedChunk.DVertex>();
        private List<int> listOfTriangleIndices = new List<int>();

        public void startBuildingArray(Vector4 currentPosition, out SC_instancedChunk.DVertex[] vertexArray, out int[] triangleArray, out int[] mapper)
        {
            map = new int[SC_Globals.tinyChunkWidth * SC_Globals.tinyChunkHeight * SC_Globals.tinyChunkDepth];

            FastNoise fastNoise = new FastNoise();

            staticPlaneSize = SC_Globals.planeSize;

            if (staticPlaneSize == 1)
            {
                staticPlaneSize = SC_Globals.planeSize * 0.1f;
                alternateStaticPlaneSize = SC_Globals.planeSize * 0.1f;
            }
            else if (staticPlaneSize == 0.1f)
            {
                staticPlaneSize = SC_Globals.planeSize;
                alternateStaticPlaneSize = SC_Globals.planeSize * 10;
            }
            else if (staticPlaneSize == 0.01f)
            {
                staticPlaneSize = SC_Globals.planeSize;
                alternateStaticPlaneSize = SC_Globals.planeSize * 1000;
            }

            for (int x = 0; x < SC_Globals.tinyChunkWidth; x++)
            {
                for (int y = 0; y < SC_Globals.tinyChunkHeight; y++)
                {
                    for (int z = 0; z < SC_Globals.tinyChunkDepth; z++)
                    {
                        map[x + SC_Globals.tinyChunkWidth * (y + SC_Globals.tinyChunkHeight * z)] = 1;

                        /*float noiseXZ = 20;
                        noiseXZ *= fastNoise.GetPerlin((((x * staticPlaneSize) + (currentPosition.X * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale, (((y * staticPlaneSize) + (currentPosition.Y * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale, (((z * staticPlaneSize) + (currentPosition.Z * alternateStaticPlaneSize) + seed) / _detailScale) * _HeightScale);

                        //Console.WriteLine(noiseXZ);

                        if (noiseXZ >= 0.1f)
                        {
                            map[x + SC_Globals.tinyChunkWidth * (y + SC_Globals.tinyChunkHeight * z)] = 1;
                        }
                        else if (y == 0 && currentPosition.Y == 0)
                        {
                            map[x + SC_Globals.tinyChunkWidth * (y + SC_Globals.tinyChunkHeight * z)] = 1;
                        }
                        else
                        {
                            map[x + SC_Globals.tinyChunkWidth * (y + SC_Globals.tinyChunkHeight * z)] = 0;
                        }*/
                    }
                }
            }

            Regenerate(currentPosition);

            vertexArray = listOfVerts.ToArray();
            triangleArray = listOfTriangleIndices.ToArray();
            mapper = map;
        }

        public void Regenerate(Vector4 currentPosition)
        {
            for (int x = 0; x < SC_Globals.tinyChunkWidth; x++)
            {
                for (int y = 0; y < SC_Globals.tinyChunkHeight; y++)
                {
                    for (int z = 0; z < SC_Globals.tinyChunkDepth; z++)
                    {
                        block = map[x + SC_Globals.tinyChunkWidth * (y + SC_Globals.tinyChunkHeight * z)];

                        if (block == 0) continue;
                        {
                            float max = 1;

                            var valX = (x * SC_Globals.planeSize);
                            valX = sc_maths.LimitInclusiveFloat(valX, 0, 1);

                            var valY = (y * SC_Globals.planeSize);
                            valY = sc_maths.LimitInclusiveFloat(valY, 0, 1);

                            var valZ = (z * SC_Globals.planeSize);
                            valZ = sc_maths.LimitInclusiveFloat(valZ, 0, 1);

                            SharpDX.Vector4 _color = new SharpDX.Vector4(valX, valY, valZ, 1);

                            //MainWindow.MessageBox((IntPtr)0, "++++", "sccoresystems messahge", 0);
                            DrawBrick(x, y, z, currentPosition, block, _color);
                        }
                    }
                }
            }
        }


        public void DrawBrick(int x, int y, int z,Vector4 currentPosition,int block, SharpDX.Vector4 _color)
        {
            Vector4 start = new Vector4((x * SC_Globals.planeSize), (y * SC_Globals.planeSize), (z * SC_Globals.planeSize), 1);
            Vector4 offset1, offset2;

            //RIGHTFACE
            if (IsTransparent(x + 1, y, z))
            {
                //map[x + SC_Globals.tinyChunkWidth * (y + SC_Globals.tinyChunkHeight * z)] = 1;
                //MainWindow.MessageBox((IntPtr)0, "++++", "sccoresystems message", 0);

                offset1 = up * SC_Globals.planeSize;
                offset2 = forward * SC_Globals.planeSize;
                createRightFace(start + right * SC_Globals.planeSize, offset1, offset2, currentPosition, x, y, z, 1, _color);
                //MainWindow.MessageBox((IntPtr)0, "++++", "sccoresystems messahge", 0);
            }

            //LEFTFACE
            if (IsTransparent(x - 1, y, z))
            {
                //map[x + SC_Globals.tinyChunkWidth * (y + SC_Globals.tinyChunkHeight * z)] = 1;
                offset1 = back * SC_Globals.planeSize;
                offset2 = down * SC_Globals.planeSize;
                createleftFace(start + up * SC_Globals.planeSize + forward * SC_Globals.planeSize, offset1, offset2, currentPosition, x, y, z, 1, _color);
            }

            //FRONTFACE
            if (IsTransparent(x, y, z - 1))
            {
                //map[x + SC_Globals.tinyChunkWidth * (y + SC_Globals.tinyChunkHeight * z)] = 1;

                offset1 = left * SC_Globals.planeSize;
                offset2 = up * SC_Globals.planeSize;
                createFrontFace(start + right * SC_Globals.planeSize, offset1, offset2, currentPosition, x, y, z, 1, _color);
                //offset1 = Vector3.Left * planeSize;
                //offset2 = Vector3.Up * planeSize;
                //createFrontFace(start + Vector3.Right * planeSize, offset1, offset2);
            }

            //BACKFACE
            if (IsTransparent(x, y, z + 1))
            {
                //map[x + SC_Globals.tinyChunkWidth * (y + SC_Globals.tinyChunkHeight * z)] = 1;

                offset1 = right * SC_Globals.planeSize;
                offset2 = up * SC_Globals.planeSize;
                createBackFace(start + forward * SC_Globals.planeSize, offset1, offset2, currentPosition, x, y, z, 1, _color);
                //offset1 = Vector3.Right * planeSize;
                //offset2 = Vector3.Up * planeSize;
                //createBackFace(start + Vector3.ForwardLH * planeSize, offset1, offset2);
            }
            
            //TOPFACE
            if (IsTransparent(x, y + 1, z))
            {
                offset1 = forward * SC_Globals.planeSize;
                offset2 = right * SC_Globals.planeSize;
                createTopFace(start + up * SC_Globals.planeSize, offset1, offset2, currentPosition, x, y, z, 1, _color);
                //map[x + SC_Globals.tinyChunkWidth * (y + SC_Globals.tinyChunkHeight * z)] = 1;
               
                //offset1 = Vector3.ForwardLH * planeSize;
                //offset2 = Vector3.Right * planeSize;
                //createTopFace(start + Vector3.Up * planeSize, offset1, offset2);
            }

            //BOTTOMFACE
            if (IsTransparent(x, y - 1, z))
            {
                //map[x + SC_Globals.tinyChunkWidth * (y + SC_Globals.tinyChunkHeight * z)] = 1;
                offset1 = right * SC_Globals.planeSize;
                offset2 = forward * SC_Globals.planeSize;
                createBottomFace(start, offset1, offset2, currentPosition, x, y, z, 1, _color);
                //offset1 = Vector3.Right * planeSize;
                //offset2 = Vector3.ForwardLH * planeSize;
                //createBottomFace(start, offset1, offset2);
            }

            /*offset1 = forward * SC_Globals.planeSize;
            offset2 = right * SC_Globals.planeSize;
            createTopFace(start + up * SC_Globals.planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = back * SC_Globals.planeSize;
            offset2 = down * SC_Globals.planeSize;
            createleftFace(start + up * SC_Globals.planeSize + forward * SC_Globals.planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = up * SC_Globals.planeSize;
            offset2 = forward * SC_Globals.planeSize;
            createRightFace(start + right * SC_Globals.planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = left * SC_Globals.planeSize;
            offset2 = up * SC_Globals.planeSize;
            createFrontFace(start + right * SC_Globals.planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = right * SC_Globals.planeSize;
            offset2 = up * SC_Globals.planeSize;
            createBackFace(start + forward * SC_Globals.planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = right * SC_Globals.planeSize;
            offset2 = forward * SC_Globals.planeSize;
            createBottomFace(start, offset1, offset2, currentPosition,x,y,z, 1);*/
        }

        private void createTopFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z,int block, SharpDX.Vector4 _color)
        {
            int index = listOfVerts.Count;

            /*listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(0, 1),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(1, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(1f, 1),
            });*/



            float _sizeX = 0.1f;
            float _sizeY = 0.1f;
            float _sizeZ = 0.1f;

            float vertoffsetx = 0;
            float vertoffsety = 0;
            float vertoffsetz = 0;


            //TOP
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(0, 1, 1),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(0, 1, 1),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(0, 1, 1),

            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(0, 1, 1),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(0, 1, 1),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (1 + vertoffsetz) * _sizeZ,1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(0, 1, 1),
            });





            /*listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);*/

            /*listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);*/

            listOfTriangleIndices.Add(index + 5);
            listOfTriangleIndices.Add(index + 4);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
        }



        private void createBottomFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block, SharpDX.Vector4 _color)
        {
            int index = listOfVerts.Count;
            /*listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(0f, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(0f, 1f),
            });


            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                normal = new Vector3(0, 1, -1),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                tex = new Vector2(1, 0),

            });


            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(1, 1f),
            });*/




            float _sizeX = 0.1f;
            float _sizeY = 0.1f;
            float _sizeZ = 0.1f;

            float vertoffsetx = 0;
            float vertoffsety = 0;
            float vertoffsetz = 0;



            //BOTTOM
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ,1),
                tex = new Vector2(0, 0),
                normal = new Vector3(1, 0, 1),
                color = _color,
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(1, 0, 1),
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(1, 0, 1),
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(1, 0, 1),
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(1, 0, 1),
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(1, 0, 1),
            });







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
            listOfTriangleIndices.Add(index + 3);*/

            listOfTriangleIndices.Add(index + 5);
            listOfTriangleIndices.Add(index + 4);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
        }


        private void createFrontFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block, SharpDX.Vector4 _color)
        {
            int index = listOfVerts.Count;

            /*listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(0, 1f),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(1, 1f),
            });*/

            float _sizeX = 0.1f;
            float _sizeY = 0.1f;
            float _sizeZ = 0.1f;

            float vertoffsetx = 0;
            float vertoffsety = 0;
            float vertoffsetz = 0;

            //FACE NEAR
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ,1),
                tex = new Vector2(1, 1),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(1, 0, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(1, 0),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(1, 0, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(1, 0, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 1),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(1, 0, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(1, 1),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(1, 0, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(1, 0, 0),
            });





            //////////////////TOREADD
            //////////////////TOREADD
            //////////////////TOREADD
            /*
            //FACE NEAR
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(1, 0, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 1),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(1, 0, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(1, 1),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(1, 0, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(1, 0),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(1, 0, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(1, 0, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(1, 1),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(1, 0, 0),
            });
            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
            */
            //////////////////TOREADD
            //////////////////TOREADD
            //////////////////TOREADD




            /*listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 4);
            listOfTriangleIndices.Add(index + 5);*/

            /*listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);*/

            listOfTriangleIndices.Add(index + 5);
            listOfTriangleIndices.Add(index + 4);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);



            /*listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);*/

        }
        private void createBackFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block, SharpDX.Vector4 _color)
        {
            int index = listOfVerts.Count;

            /*listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(0, 1),
            });


            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(1, 1f),
            });*/



            float _sizeX = 0.1f;
            float _sizeY = 0.1f;
            float _sizeZ = 0.1f;

            float vertoffsetx = 0;
            float vertoffsety = 0;
            float vertoffsetz = 0;


            //FACE FAR
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (1 + vertoffsetz) * _sizeZ,1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(0, 1, 0),
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(0, 1, 0),
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(0, 1, 0),
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(0, 1, 0),
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(0, 1, 0),
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(0, 1, 0),
            });

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
            listOfTriangleIndices.Add(index + 3);*/

            listOfTriangleIndices.Add(index + 5);
            listOfTriangleIndices.Add(index + 4);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);

        }

        private void createRightFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block, SharpDX.Vector4 _color)
        {
            int index = listOfVerts.Count;

            /*listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(0, 1),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(1, 0),
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(1, 1f),              
            });*/




            float _sizeX = 0.1f;
            float _sizeY = 0.1f;
            float _sizeZ = 0.1f;

            float vertoffsetx = 0;
            float vertoffsety = 0;
            float vertoffsetz = 0;


            //1.632 0.918
            //FACE RIGHT
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(1, 1, 0),
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(1, 1, 0),
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(1, 1, 0),
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(1, 1, 0),
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(1, 1, 0),
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ,1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(1, 1, 0),
            });




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
            listOfTriangleIndices.Add(index + 3);*/

            listOfTriangleIndices.Add(index + 5);
            listOfTriangleIndices.Add(index + 4);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
        }

        private void createleftFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition,int x, int y, int z, int block, SharpDX.Vector4 _color)
        {
            int index = listOfVerts.Count;
            /*listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),           
            });

            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 1),
            });


            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(1, 0),        
            });


            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(1, 1),
            });*/




            float _sizeX = 0.1f;
            float _sizeY = 0.1f;
            float _sizeZ = 0.1f;

            float vertoffsetx = 0;
            float vertoffsety = 0;
            float vertoffsetz = 0;
            //FACE LEFT
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (1 + vertoffsetz) * _sizeZ,1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(0, 0, 1),
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(0, 0, 1),
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(0, 0, 1),
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (-1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(0, 0, 1),
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (-1 + vertoffsety) * _sizeY, (1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(0, 0, 1),
            });
            listOfVerts.Add(new SC_instancedChunk.DVertex()
            {
                position = new Vector4((-1 + vertoffsetx) * _sizeX, (1 + vertoffsety) * _sizeY, (1 + vertoffsetz) * _sizeZ, 1),
                tex = new Vector2(0, 0),
                color = _color,
                normal = new Vector3(0, 0, 1),
            });

            /*listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);*/


            /*listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            */

            /*listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);*/

            listOfTriangleIndices.Add(index + 5);
            listOfTriangleIndices.Add(index + 4);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);

        }

        public bool IsTransparent(int x, int y, int z)
        {
            if ((x < 0) || (y < 0) || (z < 0) || (x >= SC_Globals.tinyChunkWidth) || (y >= SC_Globals.tinyChunkHeight) || (z >= SC_Globals.tinyChunkDepth)) return true;
            {
                return map[x + SC_Globals.tinyChunkWidth * (y + SC_Globals.tinyChunkHeight * z)] == 0;
            }
        }
    }
}

