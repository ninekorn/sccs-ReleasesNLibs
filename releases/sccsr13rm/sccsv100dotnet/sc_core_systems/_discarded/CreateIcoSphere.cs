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


//https://gamedev.stackexchange.com/questions/31308/algorithm-for-creating-spheres
namespace _sc_core_systems
{

   /* public static class GeometryProvider
    {

        private static int GetMidpointIndex(Dictionary<string, int> midpointIndices, List<Vector3> vertices, int i0, int i1)
        {

            var edgeKey = string.Format("{0}_{1}", Math.Min(i0, i1), Math.Max(i0, i1));

            var midpointIndex = -1;

            if (!midpointIndices.TryGetValue(edgeKey, out midpointIndex))
            {
                var v0 = vertices[i0];
                var v1 = vertices[i1];

                var midpoint = (v0 + v1) / 2f;

                if (vertices.Contains(midpoint))
                    midpointIndex = vertices.IndexOf(midpoint);
                else
                {
                    midpointIndex = vertices.Count;
                    vertices.Add(midpoint);
                    midpointIndices.Add(edgeKey, midpointIndex);
                }
            }


            return midpointIndex;

        }

        /// <remarks>
        ///      i0
        ///     /  \
        ///    m02-m01
        ///   /  \ /  \
        /// i2---m12---i1
        /// </remarks>
        /// <param name="vectors"></param>
        /// <param name="indices"></param>
        public static void Subdivide(List<Vector3> vectors, List<int> indices, bool removeSourceTriangles)
        {
            var midpointIndices = new Dictionary<string, int>();

            var newIndices = new List<int>(indices.Count * 4);

            if (!removeSourceTriangles)
                newIndices.AddRange(indices);

            for (var i = 0; i < indices.Count - 2; i += 3)
            {
                var i0 = indices[i];
                var i1 = indices[i + 1];
                var i2 = indices[i + 2];

                var m01 = GetMidpointIndex(midpointIndices, vectors, i0, i1);
                var m12 = GetMidpointIndex(midpointIndices, vectors, i1, i2);
                var m02 = GetMidpointIndex(midpointIndices, vectors, i2, i0);

                newIndices.AddRange(
                    new[] {
                    i0,m01,m02
                    ,
                    i1,m12,m01
                    ,
                    i2,m02,m12
                    ,
                    m02,m01,m12
                    }
                    );

            }

            indices.Clear();
            indices.AddRange(newIndices);
        }

        /// <summary>
        /// create a regular icosahedron (20-sided polyhedron)
        /// </summary>
        /// <param name="primitiveType"></param>
        /// <param name="size"></param>
        /// <param name="vertices"></param>
        /// <param name="indices"></param>
        /// <remarks>
        /// You can create this programmatically instead of using the given vertex 
        /// and index list, but it's kind of a pain and rather pointless beyond a 
        /// learning exercise.
        /// </remarks>

        /// note: icosahedron definition may have come from the OpenGL red book. I don't recall where I found it. 
        public static void Icosahedron(List<Vector3> vertices, List<int> indices)
        {

            indices.AddRange(
                new int[]
                {
                0,4,1,
                0,9,4,
                9,5,4,
                4,5,8,
                4,8,1,
                8,10,1,
                8,3,10,
                5,3,8,
                5,2,3,
                2,7,3,
                7,10,3,
                7,6,10,
                7,11,6,
                11,0,6,
                0,1,6,
                6,1,10,
                9,0,11,
                9,11,2,
                9,2,5,
                7,2,11
                }
                .Select(i => i + vertices.Count)
            );

            var X = 0.525731112119133606f;
            var Z = 0.850650808352039932f;

            vertices.AddRange(
                new[]
                {
                new Vector3(-X, 0f, Z),
                new Vector3(X, 0f, Z),
                new Vector3(-X, 0f, -Z),
                new Vector3(X, 0f, -Z),
                new Vector3(0f, Z, X),
                new Vector3(0f, Z, -X),
                new Vector3(0f, -Z, X),
                new Vector3(0f, -Z, -X),
                new Vector3(Z, X, 0f),
                new Vector3(-Z, X, 0f),
                new Vector3(Z, -X, 0f),
                new Vector3(-Z, -X, 0f)
                }
            );
        }
    }*/

    
    public class CreateIcoSphere
    {
        // Not implemented
        public enum AnchorPoint
        {
            TopLeft,
            TopHalf,
            TopRight,
            RightHalf,
            BottomRight,
            BottomHalf,
            BottomLeft,
            LeftHalf,
            Center
        }

        //public int recursionLevel = 1;
        public float radius = 0;
        //private AnchorPoint anchor = AnchorPoint.Center;
        public bool addCollider = false;
        public bool createAtOrigin = true;
        public string optionalName;

        private struct TriangleIndices
        {
            public int v1;
            public int v2;
            public int v3;

            public TriangleIndices(int v1, int v2, int v3)
            {
                this.v1 = v1;
                this.v2 = v2;
                this.v3 = v3;
            }
        }

        // return index of point in the middle of p1 and p2
        private int getMiddlePoint(int p1, int p2, ref List<Vector3> vertices, ref Dictionary<long, int> cache, float radius)
        {
            // first check if we have it already
            bool firstIsSmaller = p1 < p2;
            long smallerIndex = firstIsSmaller ? p1 : p2;
            long greaterIndex = firstIsSmaller ? p2 : p1;
            long key = (smallerIndex << 32) + greaterIndex;

            int ret;
            if (cache.TryGetValue(key, out ret))
            {
                return ret;
            }

            // not in cache, calculate it
            Vector3 point1 = vertices[p1];
            Vector3 point2 = vertices[p2];
            Vector3 middle = new Vector3
            (
                (point1.X + point2.X) / 2f,
                (point1.Y + point2.Y) / 2f,
                (point1.Z + point2.Z) / 2f
            );

            // add vertex makes sure point is on unit sphere
            int i = vertices.Count;

            middle.Normalize();
            vertices.Add(middle * radius);

            // store it, return index
            cache.Add(key, i);

            return i;
        }
        int recursionLevel = 1;
        public void OnWizardCreate(float _radius, int sizeA, int sizeB, out List<Vector3> vertList, out List<int> triList, out List<Vector3> normals)
        {
            radius = _radius;

            List<int> tempTriList = new List<int>();
            List<Vector3> tempList = new List<Vector3>();
            recursionLevel = MathUtil.Clamp(recursionLevel, sizeA, sizeB);
            tempList = new List<Vector3>();
            Dictionary<long, int> middlePointIndexCache = new Dictionary<long, int>();

            // create 12 vertices of a icosahedron
            float t = (float)(1f + Math.Sqrt(5f)) / 2f; // i dont fucking get this //1.618033988749895



            Vector3 aa = new Vector3(-1f, t, 0f);
            aa.Normalize();
            aa *= radius;
            tempList.Add(aa);
            Vector3 bb = new Vector3(1f, t, 0f);
            bb.Normalize();
            bb *= radius;
            tempList.Add(bb);
            Vector3 cc = new Vector3(-1f, -t, 0f);
            cc.Normalize();
            cc *= radius;
            tempList.Add(cc);
            Vector3 dd = new Vector3(1f, -t, 0f);
            dd.Normalize();
            dd *= radius;
            tempList.Add(dd);



            aa = new Vector3(0f, -1f, t);
            aa.Normalize();
            aa *= radius;
            tempList.Add(aa);
            bb = new Vector3(0f, 1f, t);
            bb.Normalize();
            bb *= radius;
            tempList.Add(bb);
            cc = new Vector3(0f, -1f, -t);
            cc.Normalize();
            cc *= radius;
            tempList.Add(cc);
            dd = new Vector3(0f, 1f, -t);
            dd.Normalize();
            dd *= radius;
            tempList.Add(dd);


            aa = new Vector3(t, 0f, -1f);
            aa.Normalize();
            aa *= radius;
            tempList.Add(aa);
            bb = new Vector3(t, 0f, 1f);
            bb.Normalize();
            bb *= radius;
            tempList.Add(bb);
            cc = new Vector3(-t, 0f, -1f);
            cc.Normalize();
            cc *= radius;
            tempList.Add(cc);
            dd = new Vector3(-t, 0f, 1f);
            dd.Normalize();
            dd *= radius;
            tempList.Add(dd);





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













            
            // create 20 triangles of the icosahedron
            List<TriangleIndices> faces = new List<TriangleIndices>();
            normals = new List<Vector3>();



            // 5 faces around point 0
            faces.Add(new TriangleIndices(0, 11, 5));
            faces.Add(new TriangleIndices(0, 5, 1));
            faces.Add(new TriangleIndices(0, 1, 7));
            faces.Add(new TriangleIndices(0, 7, 10));
            faces.Add(new TriangleIndices(0, 10, 11));

            // 5 adjacent faces
            faces.Add(new TriangleIndices(1, 5, 9));
            faces.Add(new TriangleIndices(5, 11, 4));
            faces.Add(new TriangleIndices(11, 10, 2));
            faces.Add(new TriangleIndices(10, 7, 6));
            faces.Add(new TriangleIndices(7, 1, 8));

            // 5 faces around point 3
            faces.Add(new TriangleIndices(3, 9, 4));
            faces.Add(new TriangleIndices(3, 4, 2));
            faces.Add(new TriangleIndices(3, 2, 6));
            faces.Add(new TriangleIndices(3, 6, 8));
            faces.Add(new TriangleIndices(3, 8, 9));

            // 5 adjacent faces
            faces.Add(new TriangleIndices(4, 9, 5));
            faces.Add(new TriangleIndices(2, 4, 11));
            faces.Add(new TriangleIndices(6, 2, 10));
            faces.Add(new TriangleIndices(8, 6, 7));
            faces.Add(new TriangleIndices(9, 8, 1));


            // refine triangles
            for (int i = 0; i < recursionLevel; i++)
            {
                List<TriangleIndices> faces2 = new List<TriangleIndices>();
                foreach (var tri in faces)
                {
                    // replace triangle by 4 triangles
                    int a = getMiddlePoint(tri.v1, tri.v2, ref tempList, ref middlePointIndexCache, radius);
                    int b = getMiddlePoint(tri.v2, tri.v3, ref tempList, ref middlePointIndexCache, radius);
                    int c = getMiddlePoint(tri.v3, tri.v1, ref tempList, ref middlePointIndexCache, radius);

                    faces2.Add(new TriangleIndices(tri.v1, a, c));
                    faces2.Add(new TriangleIndices(tri.v2, b, a));
                    faces2.Add(new TriangleIndices(tri.v3, c, b));
                    faces2.Add(new TriangleIndices(a, b, c));
                }
                faces = faces2;
            }

            //mesh.vertices = vertList.ToArray();
            //tempTriList = new List<int>();


            for (int i = 0; i < faces.Count; i++)
            {
                tempTriList.Add(faces[i].v1);
                tempTriList.Add(faces[i].v2);
                tempTriList.Add(faces[i].v3);


                var someedge0 = tempList[faces[i].v1] - tempList[faces[i].v2];
                someedge0.Normalize();

                var someedge1 = tempList[faces[i].v3] - tempList[faces[i].v1];
                someedge1.Normalize();

                Vector3 vec_result;
                Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
                vec_result.Normalize();

                normals.Add(vec_result);
                normals.Add(vec_result);
                normals.Add(vec_result);


            }












            /*for (int i = 0; i < faces.Count / 3; i++)
            {

                var someedge0 = tempList[faces[i].v1] - tempList[faces[i].v2];
                someedge0.Normalize();

                var someedge1 = tempList[faces[i].v2] - tempList[faces[i].v3];
                someedge1.Normalize();

                Vector3 vec_result;
                Vector3.Cross(ref someedge0, ref someedge1, out vec_result);
                vec_result.Normalize();
                normals.Add(vec_result);


            }*/








            vertList = tempList;
            triList = tempTriList;









            //mesh.triangles = triList.ToArray();



            /*var nVertices = tempList.ToArray();//  mesh.vertices;
            Vector2[] UVs = new Vector2[nVertices.Length];



            for (var i = 0; i < nVertices.Length; i++)
            {
                nVertices[i].Normalize();
                var unitVector = nVertices[i];
                Vector2 ICOuv = new Vector2(0, 0);
                ICOuv.X = (float)(Math.Atan2(unitVector.X, unitVector.Z) + Math.PI) / (float)Math.PI / 2;
                ICOuv.Y = (float)(Math.Acos(unitVector.Y) + Math.PI) / (float)Math.PI - 1;
                UVs[i] = new Vector2(ICOuv.X, ICOuv.Y);
            }*/

            //mesh.uv = UVs;

            //normales = new Vector3[vertList.Count]; //Vector3[]
            /*normals = new List<Vector3>();
            List<Vector3> tempLister = new List<Vector3>();

            for (int i = 0; i < vertList.Count; i++)
            {
                Vector3 vert = vertList[i];
                vert.Normalize();
                //tempLister.Add(vert);
                //tempLister[i].Normalize();
                //normales[i] = tempLister[i];
                normals.Add(vert);
            }*/

            //normals = new Vector3[vertList.Count];
            /*normals = new List<Vector3>();

            for (int i = 0; i < vertList.Count; i++)
            {
                var vert = vertList[i];
                vert.Normalize();
                normals.Add(vert);
            }*/
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