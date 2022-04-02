using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

using SimplexNoise;

namespace _sc_core_systems
{

    public class chunk
    {
        private static chunk chunker;

        private int width = 10;
        private int height = 10;
        private int depth = 10;

        private byte[] map;
        private float planeSize = 0.01f;
        private int seed = 3420;

        private int block;

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
        private Vector4 down = new Vector4(0, -1, 0,1);

        int randX = 3420;
        int randY = 3420;
        public static int countingArrayOfChunks = 0;
        public void startBuildingArray(Vector3 currentPosition, out Vector4[] vertexArray, out int[] indicesArray, out Vector3[] norms) //, out int vertexNum, out int indicesNum
        {
            map = new byte[width* height* depth];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        /*Vector3 position = new Vector3(x,y,z);

                        Vector3 center = Vector3.Zero;
                        float distance = Vector3.Distance(position,center);
                        if (distance > width -3)
                        {
                            map[x + width * (y + height * z)] = 0;
                        }
                        else
                        {
                            map[x + width * (y + height * z)] = 1;
                        }*/
                        map[x + width * (y + height * z)] = 1;
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
                        if (block == 0) continue;
                        {
                            calculateNumberOfVertex(x, y, z);
                        }                     
                    }
                }
            }

            positions = new Vector4[counterVertexTop * 4 + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4];
            normals = new Vector3[counterVertexTop * 4 + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4];
            textureCoordinates = new Vector2[counterVertexTop * 4 + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4];
            triangleIndices = new int[counterVertexTop * 6 + counterVertexBottom * 6 + counterVertexRight * 6 + counterVertexLeft * 6 + counterVertexFront * 6 + counterVertexBack * 6];

            Regenerate(currentPosition);



            vertexArray = positions;
            indicesArray = triangleIndices;

            norms = normals;
            tex = textureCoordinates;

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
                            DrawBrick(x, y, z, currentPosition);
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
            return bigFuckingChunk[x, y, z];
        }*/
    }
}

