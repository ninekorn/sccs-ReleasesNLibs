using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using SharpDX;


//https://wiki.unity3d.com/index.php/CreateIcoSphere
//stuff of stack overflow or unity3d forums. i am not intellectually smart enough to visually interpret or calculate this type of objects. only cubes/rectangles/polygons convexbutnoooo only one 1 axis of course lol, yeah retarded mathematics levels
//. maybe spheres later. I do have tons of experience but on hiatus for a very long time, on voxel terrains. Labyrynth random generation, planet random generation. if i can make those work again. Not sure if they are on google drive or not.
//oh probably in my folder ""


namespace _sc_core_systems
{
    public class CreateSphere
    {
        //Regular sphere.Source : http://stackoverflow.com/questions/4081898/procedurally-generate-a-sphere-mesh
        public CreateSphere(float radius, int nbLong, int nbLat, out List<Vector3> vertList, out List<int> triList, out List<Vector3> normals)
        {
            //MeshFilter filter = gameObject.AddComponent<MeshFilter>();
            //Mesh mesh = filter.mesh;
            //mesh.Clear();

            //float radius = 1f;
            // Longitude |||
            //int nbLong = 24;
            // Latitude ---
            //int nbLat = 16;

            #region Vertices
            Vector3[] vertices = new Vector3[(nbLong + 1) * nbLat + 2];
            float _pi = (float)Math.PI;
            float _2pi = _pi * 2f;

            vertices[0] = Vector3.Up * radius;
            for (int lat = 0; lat < nbLat; lat++)
            {
                float a1 = _pi * (float)(lat + 1) / (nbLat + 1);
                float sin1 = (float)Math.Sin(a1);
                float cos1 = (float)Math.Cos(a1);

                for (int lon = 0; lon <= nbLong; lon++)
                {
                    float a2 = _2pi * (float)(lon == nbLong ? 0 : lon) / nbLong;
                    float sin2 = (float)Math.Sin(a2);
                    float cos2 = (float)Math.Cos(a2);

                    vertices[lon + lat * (nbLong + 1) + 1] = new Vector3(sin1 * cos2, cos1, sin1 * sin2) * radius;
                }
            }
            vertices[vertices.Length - 1] = Vector3.Up * -radius;
            #endregion

            #region Normales		
            Vector3[] normales = new Vector3[vertices.Length];
            for (int n = 0; n < vertices.Length; n++)
            {
                Vector3 vert = vertices[n];
                vert.Normalize();
                normales[n] = vert;
            }
               
            #endregion

            #region UVs
            Vector2[] uvs = new Vector2[vertices.Length];
            uvs[0] = Vector2.UnitY; // UP
            uvs[uvs.Length - 1] = Vector2.Zero;
            for (int lat = 0; lat < nbLat; lat++)
                for (int lon = 0; lon <= nbLong; lon++)
                    uvs[lon + lat * (nbLong + 1) + 1] = new Vector2((float)lon / nbLong, 1f - (float)(lat + 1) / (nbLat + 1));
            #endregion

            #region Triangles
            int nbFaces = vertices.Length;
            int nbTriangles = nbFaces * 2;
            int nbIndexes = nbTriangles * 3;
            int[] triangles = new int[nbIndexes];

            //Top Cap
            int i = 0;
            for (int lon = 0; lon < nbLong; lon++)
            {
                triangles[i++] = lon + 2;
                triangles[i++] = lon + 1;
                triangles[i++] = 0;
            }

            //Middle
            for (int lat = 0; lat < nbLat - 1; lat++)
            {
                for (int lon = 0; lon < nbLong; lon++)
                {
                    int current = lon + lat * (nbLong + 1) + 1;
                    int next = current + nbLong + 1;

                    triangles[i++] = current;
                    triangles[i++] = current + 1;
                    triangles[i++] = next + 1;

                    triangles[i++] = current;
                    triangles[i++] = next + 1;
                    triangles[i++] = next;
                }
            }

            //Bottom Cap
            for (int lon = 0; lon < nbLong; lon++)
            {
                triangles[i++] = vertices.Length - 1;
                triangles[i++] = vertices.Length - (lon + 2) - 1;
                triangles[i++] = vertices.Length - (lon + 1) - 1;
            }
            #endregion


            vertList = new List<Vector3>();
            normals = new List<Vector3>();
            triList = new List<int>();

            for (int v = 0; v < vertices.Length; v++)
            {
                vertList.Add(vertices[v]);
            }
            for (int v = 0; v < normales.Length; v++)
            {
                normals.Add(normales[v]);
            }

            for (int v = 0; v < triangles.Length; v++)
            {
                triList.Add(triangles[v]);
            }

            //mesh.vertices = vertices;
            //mesh.normals = normales;
            //mesh.uv = uvs;
            //mesh.triangles = triangles;
            //mesh.RecalculateBounds();
            //mesh.Optimize();

        }

        public Vector3 Spherical(float r, float theta, float phi)
        {
            Vector3 pt = new Vector3();
            float snt = (float)Math.Sin(theta * Math.PI / 180);
            float cnt = (float)Math.Cos(theta * Math.PI / 180);
            float snp = (float)Math.Sin(phi * Math.PI / 180);
            float cnp = (float)Math.Cos(phi * Math.PI / 180);
            pt.X = r * snt * cnp;
            pt.Y = r * cnt;
            pt.Z = -r * snt * snp;
            //pt.W = 1;
            return pt;
        }

    }
}








/*
// create 20 triangles of the icosahedron
List<TriangleIndices> faces = new List<TriangleIndices>();
normals = new List<Vector3>();

            // 5 faces around point 0
            faces.Add(new TriangleIndices(0, 11, 5));

            var someedge0 = tempList[0] - tempList[11];
var someedge1 = tempList[0] - tempList[5];
Vector3 vec_result;
Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);






            faces.Add(new TriangleIndices(0, 5, 1));
            someedge0 = tempList[0] - tempList[5];
           someedge1 = tempList[0] - tempList[1];
           
            Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);





            faces.Add(new TriangleIndices(0, 1, 7));
            someedge0 = tempList[0] - tempList[1];
           someedge1 = tempList[0] - tempList[7];
           
            Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);





            faces.Add(new TriangleIndices(0, 7, 10));
            someedge0 = tempList[0] - tempList[7];
           someedge1 = tempList[0] - tempList[10];
           
            Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);



            faces.Add(new TriangleIndices(0, 10, 11));
            someedge0 = tempList[0] - tempList[10];
           someedge1 = tempList[0] - tempList[11];
           
            Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);


            // 5 adjacent faces
            faces.Add(new TriangleIndices(1, 5, 9));
            someedge0 = tempList[1] - tempList[5];
           someedge1 = tempList[1] - tempList[9];
           
            Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);


            faces.Add(new TriangleIndices(5, 11, 4));
            someedge0 = tempList[5] - tempList[11];
           someedge1 = tempList[5] - tempList[4];
           
            Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);




            faces.Add(new TriangleIndices(11, 10, 2));
            someedge0 = tempList[11] - tempList[10];
           someedge1 = tempList[11] - tempList[2];
           
            Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);


            faces.Add(new TriangleIndices(10, 7, 6));
            someedge0 = tempList[10] - tempList[7];
           someedge1 = tempList[10] - tempList[6];
           
            Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);



            faces.Add(new TriangleIndices(7, 1, 8));
            someedge0 = tempList[7] - tempList[1];
           someedge1 = tempList[7] - tempList[8];
           
            Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);




            // 5 faces around point 3
            faces.Add(new TriangleIndices(3, 9, 4));
            someedge0 = tempList[3] - tempList[9];
           someedge1 = tempList[3] - tempList[4];
           
            Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);



            faces.Add(new TriangleIndices(3, 4, 2));
            someedge0 = tempList[3] - tempList[4];
           someedge1 = tempList[3] - tempList[2];
           
            Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);

            faces.Add(new TriangleIndices(3, 2, 6));
            someedge0 = tempList[3] - tempList[2];
           someedge1 = tempList[3] - tempList[6];
           
            Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);


            faces.Add(new TriangleIndices(3, 6, 8));
            someedge0 = tempList[3] - tempList[6];
           someedge1 = tempList[3] - tempList[8];
           
            Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);



            faces.Add(new TriangleIndices(3, 8, 9));
            someedge0 = tempList[3] - tempList[8];
           someedge1 = tempList[3] - tempList[9];
           
            Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);




            // 5 adjacent faces
            faces.Add(new TriangleIndices(4, 9, 5));
            someedge0 = tempList[4] - tempList[9];
           someedge1 = tempList[4] - tempList[5];
           
            Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);



            faces.Add(new TriangleIndices(2, 4, 11));
            someedge0 = tempList[2] - tempList[4];
           someedge1 = tempList[2] - tempList[11];
           
            Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);



            faces.Add(new TriangleIndices(6, 2, 10));
            someedge0 = tempList[6] - tempList[2];
           someedge1 = tempList[6] - tempList[10];
           
            Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);




            faces.Add(new TriangleIndices(8, 6, 7));
            someedge0 = tempList[8] - tempList[6];
           someedge1 = tempList[8] - tempList[7];
           
            Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);



            faces.Add(new TriangleIndices(9, 8, 1));
            someedge0 = tempList[9] - tempList[8];
           someedge1 = tempList[9] - tempList[1];
           
            Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
            vec_result.Normalize();
            normals.Add(vec_result);





*/