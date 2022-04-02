using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


using SharpDX;

namespace SC_skYaRk_VR_V007.SC_Graphics.SC_Models
{


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
        public float radius = 0.25f;
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
        public void OnWizardCreate( int sizeA, int sizeB, out List<Vector3> vertList, out List<int> triList)
        {

            List<int> tempTriList = new List<int>();
            List<Vector3> tempList = new List<Vector3>();
            recursionLevel = MathUtil.Clamp(recursionLevel, sizeA, sizeB);
            tempList = new List<Vector3>();
            Dictionary<long, int> middlePointIndexCache = new Dictionary<long, int>();

            // create 12 vertices of a icosahedron
            float t = (float)(1f + Math.Sqrt(5f)) / 2f;

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

            // create 20 triangles of the icosahedron
            List<TriangleIndices> faces = new List<TriangleIndices>();

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
            }
            //mesh.triangles = triList.ToArray();

            vertList = tempList;
            triList = tempTriList;

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

            /*Vector3[] normales = new Vector3[vertList.Count];

            List<Vector3> tempList = new List<Vector3>();

            for (int i = 0; i < normales.Length; i++)
            {
                //tempList[i] = vertList[i];
                //tempList[i].Normalize();
                normales[i] = tempList[i];
            }*/
        }
    }
}
