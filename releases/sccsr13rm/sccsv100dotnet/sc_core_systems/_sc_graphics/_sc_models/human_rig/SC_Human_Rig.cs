using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using SharpDX;

using Jitter;
using Jitter.Dynamics;
using Jitter.Collision;
using Jitter.LinearMath;
using Jitter.Collision.Shapes;
using System.Runtime.InteropServices;

namespace _sc_core_systems.SC_Graphics.SC_Models.human_rig
{
    public class SC_human_RIG 
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);
        





        public SC_human_RIG(SC_console_directx D3D, IntPtr HWND, World World, Matrix WorldMatrix, float size_screen, float r, float g, float b, float a, float offsetPosX, float offsetPosY, float offsetPosZ, float mass)
        {
            /*_mass = mass;
            worldMatrix_base = new Matrix[1];
            worldMatrix_base[0] = Matrix.Identity;

            //_World = World;
            _D3D = D3D;
            _WorldMatrix = WorldMatrix;
            _HWND = HWND;

            _size_screen = size_screen;
            _r = r;
            _g = g;
            _b = b;
            _a = a;

            //_instX = instX;
            //_instY = instY;
            //_instZ = instZ;

            _offsetPosX = offsetPosX;
            _offsetPosY = offsetPosY;
            _offsetPosZ = offsetPosZ;

            //_worldMatrix_instances = new Matrix[_instX * _instY * _instZ];

            worldMatrix_Terrain_instances = new Matrix[1];*/
        }










        bool _static = true;



        public void SC_human_RIG_create(SC_console_directx D3D, IntPtr HWND, World World, Matrix WorldMatrix, float size_screen, float r, float g, float b, float a, float offsetPosX, float offsetPosY, float offsetPosZ, int _index)
        {
            //var _index = x + SC_Console_GRAPHICS._physics_engine_instance_x * (y + SC_Console_GRAPHICS._physics_engine_instance_y * z);


            
        }




        public void render_human_rig(Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Matrix viewMatrix, Matrix _projectionMatrix, Vector3 oculusRiftDir, Matrix rotatingMatrix, Vector3 OFFSETPOS, Matrix rotatingMatrixForPelvis, out Matrix _handPositionLeft, out Matrix _handPositionRight, int _index)
        {
          
            try
            {

                





                /*
                _spine_upper_body_pos.X = OFFSETPOS.X;
                _spine_upper_body_pos.Y = OFFSETPOS.Y;


                _SC_modL_pelvis_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_pelvis._POSITION.M41, _player_pelvis._POSITION.M42, _player_pelvis._POSITION.M43),
                    padding1 = 100
                };

                /*Quaternion.RotationMatrix(ref _spine_upper_body_rot, out quat);

                direction_feet_forward = _getDirection(Vector3.ForwardRH, quat);
                direction_feet_right = _getDirection(Vector3.Right, quat);
                direction_feet_up = _getDirection(Vector3.Up, quat);

                normalPosBefore = originPos;
                originalPosShoulder = new Vector3(_player_pelvis._ORIGINPOSITION.M41, _player_pelvis._ORIGINPOSITION.M42, _player_pelvis._ORIGINPOSITION.M43);

                diffNormPosX = (originalPosShoulder.X - _spine_upper_body_pos.X) + _spine_upper_body_pos.X;
                diffNormPosY = (originalPosShoulder.Y - _spine_upper_body_pos.Y) + _spine_upper_body_pos.Y;
                diffNormPosZ = (originalPosShoulder.Z - _spine_upper_body_pos.Z) + _spine_upper_body_pos.Z;

                //_spine_upper_body_pos = _spine_upper_body_pos;// + (direction_feet_right * diffNormPosX);
                //_spine_upper_body_pos = _spine_upper_body_pos;// + -(direction_feet_up * diffNormPosY);
                //_spine_upper_body_pos = _spine_upper_body_pos;// + -(direction_feet_forward * diffNormPosZ);

                matrixerer = Matrix.Identity;

                /*matrixerer.M11 = _player_pelvis._ORIGINPOSITION.M11;
                matrixerer.M12 = _player_pelvis._ORIGINPOSITION.M12;
                matrixerer.M13 = _player_pelvis._ORIGINPOSITION.M13;
                matrixerer.M14 = _player_pelvis._ORIGINPOSITION.M14;

                matrixerer.M21 = _player_pelvis._ORIGINPOSITION.M21;
                matrixerer.M22 = _player_pelvis._ORIGINPOSITION.M22;
                matrixerer.M23 = _player_pelvis._ORIGINPOSITION.M23;
                matrixerer.M24 = _player_pelvis._ORIGINPOSITION.M24;

                matrixerer.M31 = _player_pelvis._ORIGINPOSITION.M31;
                matrixerer.M32 = _player_pelvis._ORIGINPOSITION.M32;
                matrixerer.M33 = _player_pelvis._ORIGINPOSITION.M33;
                matrixerer.M34 = _player_pelvis._ORIGINPOSITION.M34;


                matrixerer.M11 = rotatingMatrix.M11;
                matrixerer.M12 = rotatingMatrix.M12;
                matrixerer.M13 = rotatingMatrix.M13;
                matrixerer.M14 = rotatingMatrix.M14;

                matrixerer.M21 = rotatingMatrix.M21;
                matrixerer.M22 = rotatingMatrix.M22;
                matrixerer.M23 = rotatingMatrix.M23;
                matrixerer.M24 = rotatingMatrix.M24;

                matrixerer.M31 = rotatingMatrix.M31;
                matrixerer.M32 = rotatingMatrix.M32;
                matrixerer.M33 = rotatingMatrix.M33;
                matrixerer.M34 = rotatingMatrix.M34;

                matrixerer.M41 = rotatingMatrix.M41;// + OFFSETPOS.X;
                matrixerer.M42 = rotatingMatrix.M42;// + OFFSETPOS.Y;
                matrixerer.M43 = rotatingMatrix.M43;// + OFFSETPOS.Z;

                matrixerer.M44 = 1;

                worldMatrix_Terrain_instances[0] = _WorldMatrix;
                _player_pelvis._POSITION = matrixerer;

                _player_pelvis.Render(_D3D.device.ImmediateContext);
                _player_pelvis.RenderInstancedObject(_D3D.device.ImmediateContext, _player_pelvis.IndexCount, _player_pelvis.InstanceCount, _player_pelvis._POSITION, viewMatrix, _projectionMatrix, null, worldMatrix_Terrain_instances, _SC_modL_pelvis_BUFFER, oculusRiftDir);
                _player_pelvis._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_pelvis._POSITION.M41, _player_pelvis._POSITION.M42, _player_pelvis._POSITION.M43);
                */
            }
            catch (Exception ex)
            {
                MessageBox((IntPtr)0, ex.ToString() + "", "Oculus error", 0);

            }
        }






        public void _update_human_rig_buffers(int _index)
        {
            //var _index = x + SC_Console_GRAPHICS._physics_engine_instance_x * (y + SC_Console_GRAPHICS._physics_engine_instance_y * z);


        }

        public void _update_human_rig_physics(RigidBody body, int _index)// int _index)
        {

        }






        //https://pastebin.com/fAFp6NnN
        public static Vector3 _getDirection(Vector3 value, SharpDX.Quaternion rotation)
        {
            Vector3 vector;
            double num12 = rotation.X + rotation.X;
            double num2 = rotation.Y + rotation.Y;
            double num = rotation.Z + rotation.Z;
            double num11 = rotation.W * num12;
            double num10 = rotation.W * num2;
            double num9 = rotation.W * num;
            double num8 = rotation.X * num12;
            double num7 = rotation.X * num2;
            double num6 = rotation.X * num;
            double num5 = rotation.Y * num2;
            double num4 = rotation.Y * num;
            double num3 = rotation.Z * num;
            double num15 = ((value.X * ((1f - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
            double num14 = ((value.X * (num7 + num9)) + (value.Y * ((1f - num8) - num3))) + (value.Z * (num4 - num11));
            double num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((1f - num8) - num5));
            vector.X = (float)num15;
            vector.Y = (float)num14;
            vector.Z = (float)num13;
            return vector;
        }





    }
}




/*//////////////////////////
           //UPPER ARM RIGHT
           _SC_modL_rght_upper_arm_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
           {
               ambientColor = ambientColor,
               diffuseColor = diffuseColour,
               lightDirection = dirLight,
               padding0 = 7,
               lightPosition = new Vector3(_player_rght_upper_arm._POSITION.M41, _player_rght_upper_arm._POSITION.M42, _player_rght_upper_arm._POSITION.M43),
               padding1 = 100
           };

           //MOVINGPOINTER = new Vector3(_player_rght_upper_arm._POSITION.M41, _player_rght_upper_arm._POSITION.M42, _player_rght_upper_arm._POSITION.M43);
           MOVINGPOINTER = new Vector3(_player_rght_upper_arm._ORIGINPOSITION.M41, _player_rght_upper_arm._ORIGINPOSITION.M42, _player_rght_upper_arm._ORIGINPOSITION.M43) + OFFSETPOS;

           _rotMatrixer = _player_rght_upper_arm._ORIGINPOSITION;
           Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

           direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
           direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
           direction_feet_up_ori = _getDirection(Vector3.Up, forTest);
           direction_feet_up_ori.Normalize();

           MOVINGPOINTER = MOVINGPOINTER + (direction_feet_up_ori * (_player_rght_upper_arm._total_torso_height * 0.5f));


           someNewPointer = MOVINGPOINTER;

           Vector3 upperRightArmPivotPoint = MOVINGPOINTER;

           Vector3 somePosOfRightShoulder = new Vector3(_player_right_shldr._POSITION.M41, _player_right_shldr._POSITION.M42, _player_right_shldr._POSITION.M43);
           somePosOfRightHand = new Vector3(_player_right_hnd._POSITION.M41, _player_right_hnd._POSITION.M42, _player_right_hnd._POSITION.M43);
           var somePosOfUpperElbowTargetTwo = new Vector3(_player_rght_elbow_target_two._POSITION.M41, _player_rght_elbow_target_two._POSITION.M42, _player_rght_elbow_target_two._POSITION.M43);
           //var somePosOfUpperElbowTargetTwo = upperRightArmPivotPoint;//

           var somePosOfUpperElbowTargetOne = new Vector3(_player_rght_elbow_target._POSITION.M41, _player_rght_elbow_target._POSITION.M42, _player_rght_elbow_target._POSITION.M43);

           var someDirFromElbowTargetOneToTwo = somePosOfUpperElbowTargetTwo - somePosOfUpperElbowTargetOne;
           var someDirFromElbowTargetOneToRghtHand = somePosOfRightHand - somePosOfUpperElbowTargetOne;



           Vector3 crossRes;
           Vector3.Cross(ref someDirFromElbowTargetOneToTwo, ref someDirFromElbowTargetOneToRghtHand, out crossRes);
           crossRes.Normalize();

           var pointA = upperRightArmPivotPoint + (crossRes);
           //var pointB = upperRightArmPivotPoint + (crossRes);

           var someDirFromPivotUpperToHand = somePosOfRightHand - upperRightArmPivotPoint;
           var lengthOfDirFromPivotUpperToHand = someDirFromPivotUpperToHand.Length();
           someDirFromPivotUpperToHand.Normalize();

           var someDirFromPivotUpperToA = pointA - upperRightArmPivotPoint;
           var lengthOfDirFromPivotUpperToA = someDirFromPivotUpperToA.Length();
           someDirFromPivotUpperToA.Normalize();
           var lengthOfLowerArm = _player_rght_lower_arm._total_torso_height * 4.25f;
           var lengthOfUpperArm = _player_rght_upper_arm._total_torso_height * 5.25f;

           var totalArmLength = lengthOfLowerArm + lengthOfUpperArm;

           lengthOfDirFromPivotUpperToHand = Math.Min(lengthOfDirFromPivotUpperToHand, totalArmLength - totalArmLength * 0.001f);

           var upperEquationCirCirIntersect = (lengthOfDirFromPivotUpperToHand * lengthOfDirFromPivotUpperToHand) - (lengthOfLowerArm * lengthOfLowerArm) + (lengthOfUpperArm * lengthOfUpperArm);
           var adjacentSolvingForX = upperEquationCirCirIntersect / (2 * lengthOfDirFromPivotUpperToHand);

           var resulter = Math.Pow(lengthOfUpperArm, 2) - Math.Pow(adjacentSolvingForX, 2);
           if (resulter < 0)
           {
               resulter *= -1;
           }

           var oppositeSolvingForHalfA = (float)Math.Sqrt(resulter);

           oppositeSolvingForHalfA = Math.Min(oppositeSolvingForHalfA, lengthOfUpperArm - lengthOfUpperArm * 0.001f);

           someNewPointer = upperRightArmPivotPoint + (someDirFromPivotUpperToHand * adjacentSolvingForX);
           Vector3.Cross(ref someDirFromPivotUpperToA, ref someDirFromPivotUpperToHand, out crossRes);
           crossRes.Normalize();
           someNewPointer = someNewPointer + (crossRes * oppositeSolvingForHalfA);

           diffNormPosXElbowRight = (_player_rght_upper_arm._ORIGINPOSITION.M41) - (TORSOPIVOT.X);
           diffNormPosYElbowRight = (_player_rght_upper_arm._ORIGINPOSITION.M42) - (TORSOPIVOT.Y);
           diffNormPosZElbowRight = (_player_rght_upper_arm._ORIGINPOSITION.M43) - (TORSOPIVOT.Z);

           MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
           MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
           MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));

           someNewPointer.X = someNewPointer.X + MOVINGPOINTER.X;
           someNewPointer.Y = someNewPointer.Y + MOVINGPOINTER.Y;
           someNewPointer.Z = someNewPointer.Z + MOVINGPOINTER.Z;


           matrixerer = Matrix.Identity;

           matrixerer.M11 = _rotatingMatrix.M11;
           matrixerer.M12 = _rotatingMatrix.M12;
           matrixerer.M13 = _rotatingMatrix.M13;
           matrixerer.M14 = _rotatingMatrix.M14;

           matrixerer.M21 = _rotatingMatrix.M21;
           matrixerer.M22 = _rotatingMatrix.M22;
           matrixerer.M23 = _rotatingMatrix.M23;
           matrixerer.M24 = _rotatingMatrix.M24;

           matrixerer.M31 = _rotatingMatrix.M31;
           matrixerer.M32 = _rotatingMatrix.M32;
           matrixerer.M33 = _rotatingMatrix.M33;
           matrixerer.M34 = _rotatingMatrix.M34;

           matrixerer.M41 = someNewPointer.X;// + OFFSETPOS.X;
           matrixerer.M42 = someNewPointer.Y;// + OFFSETPOS.Y;
           matrixerer.M43 = someNewPointer.Z;// + OFFSETPOS.Z;
           matrixerer.M44 = 1;

           worldMatrix_Terrain_instances[0] = _WorldMatrix;
           _player_rght_upper_arm._POSITION = matrixerer;

           _player_rght_upper_arm.Render(_D3D.device.ImmediateContext);
           _player_rght_upper_arm.RenderInstancedObject(_D3D.device.ImmediateContext, _player_rght_upper_arm.IndexCount, _player_rght_upper_arm.InstanceCount, _player_rght_upper_arm._POSITION, viewMatrix, _projectionMatrix, null, worldMatrix_Terrain_instances, _SC_modL_rght_upper_arm_BUFFER, oculusRiftDir);
           _player_rght_upper_arm._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_rght_upper_arm._POSITION.M41, _player_rght_upper_arm._POSITION.M42, _player_rght_upper_arm._POSITION.M43);
           Matrix matrixerera = Matrix.Identity;

           matrixerera = Matrix.Identity;

           /*matrixerera.M11 = _rotatingMatrix.M11;
           matrixerera.M12 = _rotatingMatrix.M12;
           matrixerera.M13 = _rotatingMatrix.M13;
           matrixerera.M14 = _rotatingMatrix.M14;

           matrixerera.M21 = _rotatingMatrix.M21;
           matrixerera.M22 = _rotatingMatrix.M22;
           matrixerera.M23 = _rotatingMatrix.M23;
           matrixerera.M24 = _rotatingMatrix.M24;

           matrixerera.M31 = _rotatingMatrix.M31;
           matrixerera.M32 = _rotatingMatrix.M32;
           matrixerera.M33 = _rotatingMatrix.M33;
           matrixerera.M34 = _rotatingMatrix.M34;


           matrixerera.M41 = _player_rght_upper_arm._ORIGINPOSITION.M41 + OFFSETPOS.X;
           matrixerera.M42 = _player_rght_upper_arm._ORIGINPOSITION.M42 + OFFSETPOS.Y;
           matrixerera.M43 = _player_rght_upper_arm._ORIGINPOSITION.M43 + OFFSETPOS.Z;
           matrixerera.M44 = 1;

           worldMatrix_Terrain_instances[0] = _WorldMatrix;
           _player_rght_upper_arm._POSITION = matrixerera;*/
