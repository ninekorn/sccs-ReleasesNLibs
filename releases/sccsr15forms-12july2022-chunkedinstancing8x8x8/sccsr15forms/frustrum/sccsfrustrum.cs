using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;


namespace sccs
{
    public class sccsfrustrum
    {



        //from DSharpDXRastertek.Tut16.Graphics.Data
        public void ConstructSomeFrustrum(float screenDepth, Matrix projection, Matrix view)
        {
            // Calculate the minimum Z distance in the frustum.
            float zMinimum = -projection.M43 / projection.M33;
            float r = screenDepth / (screenDepth - zMinimum);
            projection.M33 = r;
            projection.M43 = -r * zMinimum;

            // Create the frustum matrix from the view matrix and updated projection matrix.
            Matrix matrix = view * projection;

            // Calculate near plane of frustum.
            _Planes[0] = new Plane(matrix.M14 + matrix.M13, matrix.M24 + matrix.M23, matrix.M34 + matrix.M33, matrix.M44 + matrix.M43);
            _Planes[0].Normalize();

        }













        private Plane[] _Planes = new Plane[6];

        List<Plane> someplanelist = new List<Plane>();

        public void ConstructFrustum(Vector3 right, Vector3 up, Vector3 forward)
        {
            // Calculate near plane of frustum.
            //_Planes[0] = new Plane(-1 * -right.X, -1 * -right.X, -1 * -right.X, -1 * -right.X);
            //_Planes[0].Normalize();
         
            //backplane
            Vector3 pointleftbackdown = new Vector3(right.X * -1, up.Y * -1, forward.Z * -0.1f);
            Vector3 pointleftbackup = new Vector3(right.X * -1, up.Y * 1, forward.Z * -0.1f);
            Vector3 pointrightbackup = new Vector3(right.X * 1, up.Y * 1, forward.Z * -0.1f);
            Vector3 pointrightbackdown = new Vector3(right.X * 1, up.Y * -1, forward.Z * -0.1f);
            //Plane backplane = new Plane(pointleftbackdown, pointleftbackup, pointrightbackup);
            Plane backplane = new Plane(pointleftbackdown, forward);
            backplane.Normalize();

            //frontplane
            Vector3 pointleftfrontdown = new Vector3(right.X * -1, up.Y * -1, forward.Z * 1);
            Vector3 pointleftfrontup = new Vector3(right.X * -1, up.Y * 1, forward.Z * 1);
            Vector3 pointrightfrontup = new Vector3(right.X * 1, up.Y * 1, forward.Z * 1);       
            Vector3 pointrightfrontdown = new Vector3(right.X * 1, up.Y * -1, forward.Z * 1);
            Plane frontplane = new Plane(pointleftfrontdown, pointleftfrontup, pointrightfrontup);
            //Plane frontplane = new Plane(pointrightfrontup, pointleftfrontup, pointleftfrontdown);

            //leftplane
            Vector3 pointleftbackdown1 = new Vector3(right.X * -1, up.Y * -1, forward.Z * -0.1f);
            Vector3 pointleftbackup1 = new Vector3(right.X * -1, up.Y * 1, forward.Z * -0.1f);
            Vector3 pointleftfrontup1 = new Vector3(right.X * -1, up.Y * 1, forward.Z * 1);
            Vector3 pointleftfrontdown1 = new Vector3(right.X * -1, up.Y * -1, forward.Z * 1);
            //Plane leftplane = new Plane(pointleftbackdown1, pointleftbackup1, pointleftfrontup1);
            Plane leftplane = new Plane(pointleftbackdown1, right);



            //rightplane
            Vector3 pointrightbackup1 = new Vector3(right.X * 1, up.Y * 1, forward.Z * -0.1f);
            Vector3 pointrightbackdown1 = new Vector3(right.X * 1, up.Y * -1, forward.Z * -0.1f);
            Vector3 pointrightfrontdown1 = new Vector3(right.X * 1, up.Y * -1, forward.Z * 1);
            Vector3 pointrightfrontup1 = new Vector3(right.X * 1, up.Y * 1, forward.Z * 1);
            Plane rightplane = new Plane(pointrightbackdown1, pointrightfrontdown1, pointrightbackup1);

            //upplane
            Vector3 pointrightfrontup2 = new Vector3(right.X * 1, up.Y * 1, forward.Z * 1);
            Vector3 pointleftfrontup2 = new Vector3(right.X * -1, up.Y * 1, forward.Z * 1);
            Vector3 pointleftbackup2 = new Vector3(right.X * -1, up.Y * 1, forward.Z * -0.1f);
            Vector3 pointrightbackup2 = new Vector3(right.X * 1, up.Y * 1, forward.Z * -0.1f);
            Plane upplane = new Plane(pointrightfrontup2, pointleftfrontup2, pointleftbackup2);

            //downplane
            Vector3 pointrightfrontdown2 = new Vector3(right.X * 1, up.Y * -1, forward.Z * 1);
            Vector3 pointleftfrontdown2 = new Vector3(right.X * -1, up.Y * -1, forward.Z * 1);
            Vector3 pointleftbackdown2 = new Vector3(right.X * -1, up.Y * -1, forward.Z * -0.1f);
            Vector3 pointrightbackdown2 = new Vector3(right.X * 1, up.Y * -1, forward.Z * -0.1f);

            Plane downplane = new Plane(pointrightfrontdown2, pointleftfrontdown2, pointleftbackdown2);

            someplanelist.Add(backplane);
            someplanelist.Add(frontplane);
            someplanelist.Add(leftplane);
            someplanelist.Add(rightplane);
            someplanelist.Add(upplane);
            someplanelist.Add(downplane);


        }


        public bool checkbackplane(Vector3 center,float radius)
        {
            //Plane.
            float result;
            Plane someplane = someplanelist[0];
            //Plane.DotCoordinate(ref someplane, ref center, out result);
            if (Plane.DotCoordinate(someplanelist[0], center) < -radius)
            {
                return false;
            }

            /*Vector3 pointleftbackdown = new Vector3(right.X * -1, up.Y * -1, 0);
            Vector3 pointrightbackup = new Vector3(right.X * 1, up.Y * 1, 0);
            Vector3 line = pointleftbackdown - pointrightbackup;
            line.Normalize();

            float result = Vector3.Dot(line, center);*/


            return true;
        }

        public float checkfrontplane(Vector4 center)
        {
            //Plane.
            float result;
            Plane someplane = someplanelist[1];

            Plane.Dot(ref someplane, ref center, out result);

            return result;
        }

        public float checkleftplane(Vector4 center)
        {
            //Plane.
            float result;
            Plane someplane = someplanelist[2];

            Plane.Dot(ref someplane, ref center, out result);

            return result;
        }


        public float checkrightplane(Vector4 center)
        {
            //Plane.
            float result;
            Plane someplane = someplanelist[3];

            Plane.Dot(ref someplane, ref center, out result);

            return result;
        }






        ////from DSharpDXRastertek.Tut16.Graphics.Data
        public bool CheckSphere(Vector3 center, float radius)
        {
            // Check if the radius of the sphere is inside the view frustum.
            for (int i = 0; i < _Planes.Length; i++)
            {
                if (Plane.DotCoordinate(_Planes[i], center) < -radius)
                    return false;
            }
            return true;
        }
    }
}
