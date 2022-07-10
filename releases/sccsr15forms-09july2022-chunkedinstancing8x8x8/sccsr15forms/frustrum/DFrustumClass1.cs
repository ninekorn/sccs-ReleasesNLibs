﻿using SharpDX;

namespace DSharpDXRastertek.Tut16.Graphics.Data
{
    public class DFrustum                   // 78 lines
    {
        // Variables
        private Plane[] _Planes = new Plane[6];

        // Methods
        public void ConstructFrustum(float screenDepth, Matrix projection, Matrix view)
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

            // Calculate far plane of frustum.
            _Planes[1] = new Plane(matrix.M14 - matrix.M13, matrix.M24 - matrix.M23, matrix.M34 - matrix.M33, matrix.M44 - matrix.M43);
            _Planes[1].Normalize();

            // Calculate left plane of frustum.
            _Planes[2] = new Plane(matrix.M14 + matrix.M11, matrix.M24 + matrix.M21, matrix.M34 + matrix.M31, matrix.M44 + matrix.M41);
            _Planes[2].Normalize();

            // Calculate right plane of frustum.
            _Planes[3] = new Plane(matrix.M14 - matrix.M11, matrix.M24 - matrix.M21, matrix.M34 - matrix.M31, matrix.M44 - matrix.M41);
            _Planes[3].Normalize();

            // Calculate top plane of frustum.
            _Planes[4] = new Plane(matrix.M14 - matrix.M12, matrix.M24 - matrix.M22, matrix.M34 - matrix.M32, matrix.M44 - matrix.M42);
            _Planes[4].Normalize();

            // Calculate bottom plane of frustum.
            _Planes[5] = new Plane(matrix.M14 + matrix.M12, matrix.M24 + matrix.M22, matrix.M34 + matrix.M32, matrix.M44 + matrix.M42);
            _Planes[5].Normalize();
        }
        public bool CheckSphere(Vector3 center, float radius)
        {
            // Check if the radius of the sphere is inside the view frustum.
            for (int i = 0; i < 6; i++)
            {
                if (Plane.DotCoordinate(_Planes[i], center) < -radius)
                    return false;
            }

            /*if (Plane.DotCoordinate(_Planes[4], center) < -radius)
            {
                return false;
            }*/


            return true;
        }
    }
}