﻿using System;
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
        int _inst_p_r_hand_x = 1;
        int _inst_p_r_hand_y = 1;
        int _inst_p_r_hand_z = 1;

        int _inst_p_l_hand_x = 1;
        int _inst_p_l_hand_y = 1;
        int _inst_p_l_hand_z = 1;

        int _inst_p_torso_x = 1;
        int _inst_p_torso_y = 1;
        int _inst_p_torso_z = 1;

        int _inst_p_r_shoulder_x = 1;
        int _inst_p_r_shoulder_y = 1;
        int _inst_p_r_shoulder_z = 1;

        int _inst_p_l_shoulder_x = 1;
        int _inst_p_l_shoulder_y = 1;
        int _inst_p_l_shoulder_z = 1;

        int _inst_p_l_upperarm_x = 1;
        int _inst_p_l_upperarm_y = 1;
        int _inst_p_l_upperarm_z = 1;

        int _inst_p_r_upperarm_x = 1;
        int _inst_p_r_upperarm_y = 1;
        int _inst_p_r_upperarm_z = 1;

        int _inst_p_l_lowerarm_x = 1;
        int _inst_p_l_lowerarm_y = 1;
        int _inst_p_l_lowerarm_z = 1;

        int _inst_p_r_lowerarm_x = 1;
        int _inst_p_r_lowerarm_y = 1;
        int _inst_p_r_lowerarm_z = 1;

        int _inst_p_head_x = 1;
        int _inst_p_head_y = 1;
        int _inst_p_head_z = 1;

        int _inst_p_pelvis_x = 1;
        int _inst_p_pelvis_y = 1;
        int _inst_p_pelvis_z = 1;

        int _inst_p_r_foot_x = 1;
        int _inst_p_r_foot_y = 1;
        int _inst_p_r_foot_z = 1;

        int _inst_p_l_foot_x = 1;
        int _inst_p_l_foot_y = 1;
        int _inst_p_l_foot_z = 1;

        int _inst_p_l_lowerleg_x = 1;
        int _inst_p_l_lowerleg_y = 1;
        int _inst_p_l_lowerleg_z = 1;

        int _inst_p_r_lowerleg_x = 1;
        int _inst_p_r_lowerleg_y = 1;
        int _inst_p_r_lowerleg_z = 1;

        int _inst_p_l_upperleg_x = 1;
        int _inst_p_l_upperleg_y = 1;
        int _inst_p_l_upperleg_z = 1;

        int _inst_p_r_upperleg_x = 1;
        int _inst_p_r_upperleg_y = 1;
        int _inst_p_r_upperleg_z = 1;

        Matrix[][] worldMatrix_instances_r_elbow_target;
        Matrix[][] worldMatrix_instances_l_elbow_target;

        Matrix[][] worldMatrix_instances_r_elbow_target_two;
        Matrix[][] worldMatrix_instances_l_elbow_target_two;

        Matrix[][] worldMatrix_instances_head;
        Matrix[][] worldMatrix_instances_torso;
        Matrix[][] worldMatrix_instances_pelvis;

        Matrix[][] worldMatrix_instances_r_hand;
        Matrix[][] worldMatrix_instances_l_hand;

        Matrix[][] worldMatrix_instances_r_shoulder;
        Matrix[][] worldMatrix_instances_l_shoulder;

        Matrix[][] worldMatrix_instances_r_upperarm;
        Matrix[][] worldMatrix_instances_l_upperarm;

        Matrix[][] worldMatrix_instances_r_lowerarm;
        Matrix[][] worldMatrix_instances_l_lowerarm;

        Matrix[][] worldMatrix_instances_r_upperleg;
        Matrix[][] worldMatrix_instances_l_upperleg;

        Matrix[][] worldMatrix_instances_r_lowerleg;
        Matrix[][] worldMatrix_instances_l_lowerleg;

        Matrix[][] worldMatrix_instances_r_foot;
        Matrix[][] worldMatrix_instances_l_foot;

        Matrix[] worldMatrix_base;

        float xq;//= otherQuat.X;
        float yq;//= otherQuat.Y;
        float zq;//= otherQuat.Z;
        float wq;//= otherQuat.W;

        float pitcha;//= (float) Math.Atan2(2 * yq* wq - 2 * xq* zq, 1 - 2 * yq* yq - 2 * zq* zq); //(float)(180 / Math.PI)
        float yawa;//= (float) Math.Atan2(2 * yq* wq - 2 * xq* zq, 1 - 2 * yq* yq - 2 * zq* zq); //(float)(180 / Math.PI) *
        float rolla;// = (float) Math.Atan2(2 * yq* wq - 2 * xq* zq, 1 - 2 * yq* yq - 2 * zq* zq); // (float)(180 / Math.PI) *

        float hyp;// = diffNormPosY / Math.Cos(pitcha);

        _sc_voxel.DLightBuffer[] _SC_modL_head_BUFFER = new _sc_voxel.DLightBuffer[1];
        _sc_voxel[] _player_head;

        _sc_voxel.DLightBuffer[] _SC_modL_pelvis_BUFFER = new _sc_voxel.DLightBuffer[1];
        public _sc_voxel[] _player_pelvis;

        _sc_voxel.DLightBuffer[] _SC_modL_rght_hnd_BUFFER = new _sc_voxel.DLightBuffer[1];
        public _sc_voxel[] _player_right_hnd;

        _sc_voxel.DLightBuffer[] _SC_modL_lft_hnd_BUFFER = new _sc_voxel.DLightBuffer[1];
        _sc_voxel[] _player_left_hnd;

        _sc_voxel.DLightBuffer[] _SC_modL_torso_BUFFER = new _sc_voxel.DLightBuffer[1];
        public _sc_voxel[] _player_torso;

        _sc_voxel.DLightBuffer[] _SC_modL_rght_shldr_BUFFER = new _sc_voxel.DLightBuffer[1];
        _sc_voxel[] _player_right_shldr;

        _sc_voxel.DLightBuffer[] _SC_modL_lft_shldr_BUFFER = new _sc_voxel.DLightBuffer[1];
        _sc_voxel[] _player_lft_shldr;

        _sc_voxel.DLightBuffer[] _SC_modL_rght_elbow_target_BUFFER = new _sc_voxel.DLightBuffer[1];
        _sc_voxel[] _player_rght_elbow_target;

        _sc_voxel.DLightBuffer[] _SC_modL_lft_elbow_target_BUFFER = new _sc_voxel.DLightBuffer[1];
        _sc_voxel[] _player_lft_elbow_target;

        _sc_voxel.DLightBuffer[] _SC_modL_lft_lower_arm_BUFFER = new _sc_voxel.DLightBuffer[1];
        _sc_voxel[] _player_lft_lower_arm;

        _sc_voxel.DLightBuffer[] _SC_modL_rght_lower_arm_BUFFER = new _sc_voxel.DLightBuffer[1];
        _sc_voxel[] _player_rght_lower_arm;

        _sc_voxel.DLightBuffer[] _SC_modL_lft_upper_arm_BUFFER = new _sc_voxel.DLightBuffer[1];
        _sc_voxel[] _player_lft_upper_arm;

        _sc_voxel.DLightBuffer[] _SC_modL_rght_upper_arm_BUFFER = new _sc_voxel.DLightBuffer[1];
        _sc_voxel[] _player_rght_upper_arm;

        _sc_voxel.DLightBuffer[] _SC_modL_rght_elbow_target_two_BUFFER = new _sc_voxel.DLightBuffer[1];
        _sc_voxel[] _player_rght_elbow_target_two;

        _sc_voxel.DLightBuffer[] _SC_modL_lft_elbow_target_two_BUFFER = new _sc_voxel.DLightBuffer[1];
        _sc_voxel[] _player_lft_elbow_target_two;


        Matrix shoulderRotationMatrixRight;
        Matrix shoulderRotationMatrixLeft;

        Matrix[] worldMatrix_Terrain_instances = new Matrix[1];

        bool _hasinit0 = false;

        Matrix _tempMatroxer = Matrix.Identity;

        float _r = 0;
        float _g = 0;
        float _b = 0;
        float _a = 1;

        //int _instX = -1;
        //int _instY = -1;
        //int _instZ = -1;

        float _offsetPosX = 2;
        float _offsetPosY = 2;
        float _offsetPosZ = 2;

        float _size_screen;

        Matrix _WorldMatrix;

        IntPtr _HWND;
        SC_console_directx _D3D;
        World _World;



        float lengthOfLowerArmLeft;
        float lengthOfUpperArmLeft;

        float lengthOfLowerArmRight;
        float lengthOfUpperArmRight;

        float totalArmLengthLeft;
        float totalArmLengthRight;

        //SECOND PART
        RigidBody body;
        IEnumerator enumerator;
        Quaternion tester;
        JQuaternion quatterer;
        int count = 0;
        Matrix translationMatrix;
        Matrix rotationMatrix;
        //Matrix[] _worldMatrix_instances;
        Matrix _tempMatrix;

        //Vector3 somePosOfUpperElbowTargetTwo = new Vector3(0, 0, 0);
        //Vector3 somePosOfUpperElbowTargetOne = new Vector3(0, 0, 0);

        Vector3 realPositionOfUpperArm = new Vector3(0, 0, 0);
        Vector3 realPIVOTOfUpperArm = new Vector3(0, 0, 0);

        //TORSO STUFF
        Matrix _spine_upper_body_rot;
        Vector3 _spine_upper_body_pos;
        Quaternion quat;
        Vector3 direction_feet_forward;
        Vector3 direction_feet_right;
        Vector3 direction_feet_up;

        Vector3 normalPosBefore;
        Vector3 originalPosShoulder;
        float diffNormPosX;
        float diffNormPosY;
        float diffNormPosZ;
        Matrix matrixerer;

        Matrix _rotatingMatrix;
        float _mass;












        public SC_human_RIG(SC_console_directx D3D, IntPtr HWND, World World, Matrix WorldMatrix, float size_screen, float r, float g, float b, float a, float offsetPosX, float offsetPosY, float offsetPosZ, float mass)
        {
            _mass = mass;
            worldMatrix_base = new Matrix[1];
            worldMatrix_base[0] = Matrix.Identity;

            _player_right_hnd = new _sc_voxel[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z];
            _player_lft_upper_arm = new _sc_voxel[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z];
            _player_left_hnd = new _sc_voxel[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z];
            _player_torso = new _sc_voxel[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z];
            _player_pelvis = new _sc_voxel[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z];
            _player_right_shldr = new _sc_voxel[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z];
            _player_lft_shldr = new _sc_voxel[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z];
            _player_head = new _sc_voxel[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z];
            _player_rght_lower_arm = new _sc_voxel[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z];
            _player_lft_lower_arm = new _sc_voxel[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z];
            _player_rght_upper_arm = new _sc_voxel[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z];
            _player_rght_elbow_target = new _sc_voxel[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z];
            _player_lft_elbow_target = new _sc_voxel[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z];
            _player_rght_elbow_target_two = new _sc_voxel[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z];
            _player_lft_elbow_target_two = new _sc_voxel[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z];

            worldMatrix_instances_r_elbow_target = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];
            worldMatrix_instances_l_elbow_target = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];

            worldMatrix_instances_r_elbow_target_two = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];
            worldMatrix_instances_l_elbow_target_two = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];

            worldMatrix_instances_head = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];
            worldMatrix_instances_torso = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];
            worldMatrix_instances_pelvis = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];

            worldMatrix_instances_r_hand = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];
            worldMatrix_instances_l_hand = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];

            worldMatrix_instances_r_shoulder = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];
            worldMatrix_instances_l_shoulder = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];

            worldMatrix_instances_r_upperarm = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];
            worldMatrix_instances_l_upperarm = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];

            worldMatrix_instances_r_lowerarm = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];
            worldMatrix_instances_l_lowerarm = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];

            worldMatrix_instances_r_upperleg = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];
            worldMatrix_instances_l_upperleg = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];

            worldMatrix_instances_r_lowerleg = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];
            worldMatrix_instances_l_lowerleg = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];

            worldMatrix_instances_r_foot = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];
            worldMatrix_instances_l_foot = new Matrix[SC_Console_GRAPHICS._physics_engine_instance_x * SC_Console_GRAPHICS._physics_engine_instance_y * SC_Console_GRAPHICS._physics_engine_instance_z][];

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

            worldMatrix_Terrain_instances = new Matrix[1];
        }










        bool _static = true;



        public void SC_human_RIG_create(SC_console_directx D3D, IntPtr HWND, World World, Matrix WorldMatrix, float size_screen, float r, float g, float b, float a, float offsetPosX, float offsetPosY, float offsetPosZ, int _index)
        {
            //var _index = x + SC_Console_GRAPHICS._physics_engine_instance_x * (y + SC_Console_GRAPHICS._physics_engine_instance_y * z);


            //PLAYER RIGHT HAND
            r = 0.035f;
            g = 0.035f;
            b = 0.035f;
            a = 1;

            //instX = 1;
            //instY = 1;
            //instZ = 1;

            _tempMatroxer = Matrix.Identity;

            _tempMatroxer = _WorldMatrix;

            _tempMatroxer.M41 = 0;
            _tempMatroxer.M42 = 0;
            _tempMatroxer.M43 = 0;
            _tempMatroxer.M44 = 1;

            offsetPosX = 0;
            offsetPosY = 0;
            offsetPosZ = 0;

            float vertOffsetX = 0;
            float vertOffsetY = 0;
            float vertOffsetZ = 0;
            _player_right_hnd[_index] = new _sc_voxel();
            //_player_right_hnd[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerHandRight, _static, 1, _mass, 0, 0, -0.75f); //, "terrainGrassDirt.bmp" //0.00035f
            _player_right_hnd[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerHandRight, 2, 2, 2, 10, 2, 2, 4, 3, 13, 12, 20, 19, 0.0025f, new Vector3(0, 0, -0.1f), 150); //, "terrainGrassDirt.bmp" //0.00035f


            worldMatrix_instances_r_hand[_index] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
            for (int i = 0; i < worldMatrix_instances_r_hand[_index].Length; i++)
            {
                worldMatrix_instances_r_hand[_index][i] = Matrix.Identity;
            }


            //PLAYER LEFT HAND
            r = 0.035f;
            g = 0.035f;
            b = 0.035f;
            a = 1;

            _tempMatroxer = Matrix.Identity;

            _tempMatroxer = _WorldMatrix;

            _tempMatroxer.M41 = 0;
            _tempMatroxer.M42 = 0;
            _tempMatroxer.M43 = 0;
            _tempMatroxer.M44 = 1;

            offsetPosX = 0;
            offsetPosY = 0;
            offsetPosZ = 0;
            //_player_left_hnd[_index] = new _sc_voxel();
            //_hasinit0 = _player_left_hnd[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_l_hand_x, _inst_p_l_hand_y, _inst_p_l_hand_z, _HWND, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerHandLeft, _static, 1, _mass, 0, 0, -0.75f); //, "terrainGrassDirt.bmp" //0.00035f

            _player_left_hnd[_index] = new _sc_voxel();
            _player_left_hnd[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerHandLeft, 2, 2, 2, 10, 2, 2, 4, 3, 13, 12, 20, 19, 0.0025f, new Vector3(0, 0, -0.1f), 150); //, "terrainGrassDirt.bmp" //0.00035f




            worldMatrix_instances_l_hand[_index] = new Matrix[_inst_p_l_hand_x * _inst_p_l_hand_y * _inst_p_l_hand_z];
            for (int i = 0; i < worldMatrix_instances_l_hand[_index].Length; i++)
            {
                worldMatrix_instances_l_hand[_index][i] = Matrix.Identity;
            }



            //TORSO
            r = 0.035f;
            g = 0.035f;
            b = 0.035f;
            a = 1;


            _tempMatroxer = Matrix.Identity;
            _tempMatroxer = _WorldMatrix;

            _tempMatroxer.M41 = 0;
            _tempMatroxer.M42 = -0.35f; // -0.1f
            _tempMatroxer.M43 = 0;
            _tempMatroxer.M44 = 1;

            offsetPosX = 0;
            offsetPosY = 0;
            offsetPosZ = 0;

            vertOffsetX = 0;
            vertOffsetY = 0;
            vertOffsetZ = 0;
            //_player_torso[_index] = new _sc_voxel();
            //_hasinit0 = _player_torso.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), instX, instY, instZ, _HWND, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f                                                                                                                                                                                                                                                                                        //_hasinit0 = _player_torso.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, _HWND, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_player_torso[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), _inst_p_torso_x, _inst_p_torso_y, _inst_p_torso_z, _HWND, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerTorso, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

            _player_torso[_index] = new _sc_voxel();
            _player_torso[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerTorso, 2, 9, 2, 2, 2, 2, 45, 44, 60, 59, 10, 9, 0.0025f, new Vector3(0, 0, 0), 500); //, "terrainGrassDirt.bmp" //0.00035f



            worldMatrix_instances_torso[_index] = new Matrix[_inst_p_torso_x * _inst_p_torso_y * _inst_p_torso_z];
            for (int i = 0; i < worldMatrix_instances_torso[_index].Length; i++)
            {
                worldMatrix_instances_torso[_index][i] = Matrix.Identity;
            }



            //PELVIS
            r = 0.035f;
            g = 0.035f;
            b = 0.035f;
            a = 1;




            _tempMatroxer = Matrix.Identity;
            _tempMatroxer = _WorldMatrix;

            _tempMatroxer.M41 = 0;
            _tempMatroxer.M42 = -0.625f;
            _tempMatroxer.M43 = 0;
            _tempMatroxer.M44 = 1;

            offsetPosX = 0;
            offsetPosY = 0;
            offsetPosZ = 0;

            vertOffsetX = 0;
            vertOffsetY = 0;
            vertOffsetZ = 0;
            //_player_pelvis[_index] = new _sc_voxel();
            //_hasinit0 = _player_pelvis.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), instX, instY, instZ, _HWND, _tempMatroxer, 9, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_pelvis[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerPelvis, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

            _player_pelvis[_index] = new _sc_voxel();
            //_player_pelvis[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerPelvis, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
            _player_pelvis[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerPelvis, 2, 9, 2, 2, 2, 2, 45, 44, 24, 23, 10, 9, 0.0025f, new Vector3(0, 0, 0), 300); //, "terrainGrassDirt.bmp" //0.00035f



            worldMatrix_instances_pelvis[_index] = new Matrix[_inst_p_torso_x * _inst_p_torso_y * _inst_p_torso_z];
            for (int i = 0; i < worldMatrix_instances_pelvis[_index].Length; i++)
            {
                worldMatrix_instances_pelvis[_index][i] = Matrix.Identity;
            }













            //PLAYER RIGHT SHOULDER
            r = 0.035f;
            g = 0.035f;
            b = 0.035f;
            a = 1;


            _tempMatroxer = Matrix.Identity;
            _tempMatroxer = _WorldMatrix;
            _tempMatroxer.M41 = 0.15f;
            _tempMatroxer.M42 = -0.2f;
            _tempMatroxer.M43 = 0;
            _tempMatroxer.M44 = 1;
            offsetPosX = 0;
            offsetPosY = 0;
            offsetPosZ = 0;
            //_player_right_shldr[_index] = new _sc_voxel();
            //_hasinit0 = _player_right_shldr.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), instX, instY, instZ, _HWND, _tempMatroxer, 9, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_right_shldr[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_shoulder_x, _inst_p_r_shoulder_y, _inst_p_r_shoulder_z, _HWND, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerShoulderRight, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

            _player_right_shldr[_index] = new _sc_voxel();
            //_player_right_shldr[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerShoulderRight, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
            _player_right_shldr[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_l_shoulder_x, _inst_p_l_shoulder_y, _inst_p_l_shoulder_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerShoulderRight, 10, 10, 10, 10, 10, 10, 20, 19, 20, 19, 20, 19, 0.0025f, new Vector3(0, 0, 0), 150); //, "terrainGrassDirt.bmp" //0.00035f



            worldMatrix_instances_r_shoulder[_index] = new Matrix[_inst_p_r_shoulder_x * _inst_p_r_shoulder_y * _inst_p_r_shoulder_z];
            for (int i = 0; i < worldMatrix_instances_r_shoulder[_index].Length; i++)
            {
                worldMatrix_instances_r_shoulder[_index][i] = Matrix.Identity;
            }




            //PLAYER LEFT SHOULDER
            r = 0.035f;
            g = 0.035f;
            b = 0.035f;
            a = 1;



            _tempMatroxer = Matrix.Identity;
            _tempMatroxer = _WorldMatrix;
            _tempMatroxer.M41 = -0.15f;
            _tempMatroxer.M42 = -0.2f;
            _tempMatroxer.M43 = 0;
            _tempMatroxer.M44 = 1;
            offsetPosX = 0;
            offsetPosY = 0;
            offsetPosZ = 0;

            //_player_lft_shldr[_index] = new _sc_voxel();
            //_hasinit0 = _player_lft_shldr.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), instX, instY, instZ, _HWND, _tempMatroxer, 9, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_lft_shldr[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_l_shoulder_x, _inst_p_l_shoulder_y, _inst_p_l_shoulder_z, _HWND, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerShoulderLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

            _player_lft_shldr[_index] = new _sc_voxel();
            //_player_lft_shldr[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerShoulderLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
            _player_lft_shldr[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_l_shoulder_x, _inst_p_l_shoulder_y, _inst_p_l_shoulder_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerShoulderLeft, 10, 10, 10, 10, 10, 10, 20, 19, 20, 19, 20, 19, 0.0025f, new Vector3(0, 0, 0), 150); //, "terrainGrassDirt.bmp" //0.00035f

            worldMatrix_instances_l_shoulder[_index] = new Matrix[_inst_p_l_shoulder_x * _inst_p_l_shoulder_y * _inst_p_l_shoulder_z];
            for (int i = 0; i < worldMatrix_instances_l_shoulder[_index].Length; i++)
            {
                worldMatrix_instances_l_shoulder[_index][i] = Matrix.Identity;
            }











            //HEAD
            r = 0.035f;
            g = 0.035f;
            b = 0.035f;
            a = 1;


            _tempMatroxer = Matrix.Identity;
            _tempMatroxer = _WorldMatrix;

            _tempMatroxer.M41 = 0;
            _tempMatroxer.M42 = 0.30f; // -0.1f
            _tempMatroxer.M43 = 0;
            _tempMatroxer.M44 = 1;

            offsetPosX = 0;
            offsetPosY = 0;
            offsetPosZ = 0;

            vertOffsetX = 0;
            vertOffsetY = 0;
            vertOffsetZ = 0;
            //_player_head[_index] = new _sc_voxel();
            //_hasinit0 = _player_head.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, _HWND, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_head[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_l_shoulder_x, _inst_p_l_shoulder_y, _inst_p_l_shoulder_z, _HWND, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerHead, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

            _player_head[_index] = new _sc_voxel();
            _player_head[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerHead, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f


            worldMatrix_instances_head[_index] = new Matrix[_inst_p_l_shoulder_x * _inst_p_l_shoulder_y * _inst_p_l_shoulder_z];
            for (int i = 0; i < worldMatrix_instances_head[_index].Length; i++)
            {
                worldMatrix_instances_head[_index][i] = Matrix.Identity;
            }













            //RIGHT LOWER ARM
            r = 0.035f;
            g = 0.035f;
            b = 0.035f;
            a = 1;

            _tempMatroxer = Matrix.Identity;
            _tempMatroxer = _WorldMatrix;
            _tempMatroxer.M41 = 0.25f;
            _tempMatroxer.M42 = -0.15f;
            _tempMatroxer.M43 = 0;
            _tempMatroxer.M44 = 1;
            offsetPosX = 0;
            offsetPosY = 0;
            offsetPosZ = 0;
            vertOffsetX = 0;
            vertOffsetY = 0;
            vertOffsetZ = 0;

            //_player_rght_lower_arm[_index] = new _sc_voxel();
            //_hasinit0 = _player_rght_lower_arm.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, _HWND, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_rght_lower_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_lowerarm_x, _inst_p_r_lowerarm_y, _inst_p_r_lowerarm_z, _HWND, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerLowerArmRight, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

            _player_rght_lower_arm[_index] = new _sc_voxel();
            //_player_rght_lower_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerLowerArmRight, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
            _player_rght_lower_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerLowerArmRight, 2, 2, 2, 2, 2, 10, 4, 3, 20, 19, 13, 12, 0.0025f, new Vector3(0, 0, 0), 150); //, "terrainGrassDirt.bmp" //0.00035f


            worldMatrix_instances_r_lowerarm[_index] = new Matrix[_inst_p_r_lowerarm_x * _inst_p_r_lowerarm_y * _inst_p_r_lowerarm_z];
            for (int i = 0; i < worldMatrix_instances_r_lowerarm[_index].Length; i++)
            {
                worldMatrix_instances_r_lowerarm[_index][i] = Matrix.Identity;
            }




            //LEFT LOWER ARM
            r = 0.035f;
            g = 0.035f;
            b = 0.035f;
            a = 1;


            _tempMatroxer = Matrix.Identity;
            _tempMatroxer = _WorldMatrix;
            _tempMatroxer.M41 = -0.25f;
            _tempMatroxer.M42 = -0.15f;
            _tempMatroxer.M43 = 0;
            _tempMatroxer.M44 = 1;
            offsetPosX = 0;
            offsetPosY = 0;
            offsetPosZ = 0;
            vertOffsetX = 0;
            vertOffsetY = 0;
            vertOffsetZ = 0;

            //_player_lft_lower_arm[_index] = new _sc_voxel();
            //_hasinit0 = _player_lft_lower_arm.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, _HWND, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_lft_lower_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_l_lowerarm_x, _inst_p_l_lowerarm_y, _inst_p_l_lowerarm_z, _HWND, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerLowerArmLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

            _player_lft_lower_arm[_index] = new _sc_voxel();
            // _player_lft_lower_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerLowerArmLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
            _player_lft_lower_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerLowerArmLeft, 2, 2, 2, 2, 2, 10, 4, 3, 20, 19, 13, 12, 0.0025f, new Vector3(0, 0, 0), 150); //, "terrainGrassDirt.bmp" //0.00035f



            worldMatrix_instances_l_lowerarm[_index] = new Matrix[_inst_p_l_lowerarm_x * _inst_p_l_lowerarm_y * _inst_p_l_lowerarm_z];
            for (int i = 0; i < worldMatrix_instances_l_lowerarm[_index].Length; i++)
            {
                worldMatrix_instances_l_lowerarm[_index][i] = Matrix.Identity;
            }


            //RIGHT UPPER ARM
            r = 0.035f;
            g = 0.035f;
            b = 0.035f;
            a = 1;

            _tempMatroxer = Matrix.Identity;
            _tempMatroxer = _WorldMatrix;
            _tempMatroxer.M41 = 0.25f;
            _tempMatroxer.M42 = -0.375f;
            _tempMatroxer.M43 = 0;
            _tempMatroxer.M44 = 1;
            offsetPosX = 0;
            offsetPosY = 0;
            offsetPosZ = 0;
            vertOffsetX = 0;
            vertOffsetY = 0;
            vertOffsetZ = 0;
            //_player_rght_upper_arm[_index] = new _sc_voxel();
            //_hasinit0 = _player_rght_upper_arm.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, _HWND, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_rght_upper_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_upperarm_x, _inst_p_r_upperarm_y, _inst_p_r_upperarm_z, _HWND, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerUpperArmRight, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

            _player_rght_upper_arm[_index] = new _sc_voxel();
            //_player_rght_upper_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerUpperArmRight, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
            _player_rght_upper_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerUpperArmRight, 2, 2, 2, 2, 2, 10, 4, 3, 20, 19, 13, 12, 0.0025f, new Vector3(0, 0, 0), 150); //, "terrainGrassDirt.bmp" //0.00035f



            worldMatrix_instances_r_upperarm[_index] = new Matrix[_inst_p_r_upperarm_x * _inst_p_r_upperarm_y * _inst_p_r_upperarm_z];
            for (int i = 0; i < worldMatrix_instances_r_upperarm[_index].Length; i++)
            {
                worldMatrix_instances_r_upperarm[_index][i] = Matrix.Identity;
            }



            //LEFT UPPER ARM
            r = 0.035f;
            g = 0.035f;
            b = 0.035f;
            a = 1;

            _tempMatroxer = Matrix.Identity;
            _tempMatroxer = _WorldMatrix;
            _tempMatroxer.M41 = -0.25f;
            _tempMatroxer.M42 = -0.25f;
            _tempMatroxer.M43 = 0;
            _tempMatroxer.M44 = 1;
            offsetPosX = 0;
            offsetPosY = 0;
            offsetPosZ = 0;
            vertOffsetX = 0;
            vertOffsetY = 0;
            vertOffsetZ = 0;
            //_player_lft_upper_arm[_index] = new _sc_voxel();
            //_hasinit0 = _player_lft_upper_arm.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, _HWND, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_lft_upper_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerUpperArmLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

            _player_lft_upper_arm[_index] = new _sc_voxel();
            //_player_lft_upper_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerUpperArmLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
            _player_lft_upper_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerUpperArmLeft, 2, 2, 2, 2, 2, 10, 4, 3, 20, 19, 13, 12, 0.0025f, new Vector3(0, 0, 0), 150); //, "terrainGrassDirt.bmp" //0.00035f



            worldMatrix_instances_l_upperarm[_index] = new Matrix[_inst_p_r_upperarm_x * _inst_p_r_upperarm_y * _inst_p_r_upperarm_z];
            for (int i = 0; i < worldMatrix_instances_l_upperarm[_index].Length; i++)
            {
                worldMatrix_instances_l_upperarm[_index][i] = Matrix.Identity;
            }



            //RIGHT ELBOW TARGET
            r = 0.035f;
            g = 0.035f;
            b = 0.035f;
            a = 1;

            //SHOULDER RIGHT
            //_tempMatroxer.M41 = -0.25f; /
            //_tempMatroxer.M42 = -0.2f;

            _tempMatroxer = Matrix.Identity;
            _tempMatroxer = _WorldMatrix;
            _tempMatroxer.M41 = 0.25f;
            _tempMatroxer.M42 = _player_rght_upper_arm[_index]._POSITION.M42 + (_player_rght_upper_arm[_index]._total_torso_height * 0.5f) + 0.45f;// - 0.25f;
            _tempMatroxer.M43 = -0.25f;
            _tempMatroxer.M44 = 1;
            offsetPosX = 0;
            offsetPosY = 0;
            offsetPosZ = 0;
            vertOffsetX = 0;
            vertOffsetY = 0;
            vertOffsetZ = 0;
            //_player_rght_elbow_target[_index] = new _sc_voxel();

            //_hasinit0 = _player_rght_elbow_target.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, _HWND, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_rght_elbow_target[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

            _player_rght_elbow_target[_index] = new _sc_voxel();
            _player_rght_elbow_target[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerRightElbowTarget, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f


            worldMatrix_instances_r_elbow_target[_index] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
            for (int i = 0; i < worldMatrix_instances_r_elbow_target[_index].Length; i++)
            {
                worldMatrix_instances_r_elbow_target[_index][i] = Matrix.Identity;
            }




            //LEFT ELBOW TARGET
            r = 0.035f;
            g = 0.035f;
            b = 0.035f;
            a = 1;

            //SHOULDER RIGHT
            //_tempMatroxer.M41 = -0.25f; /
            //_tempMatroxer.M42 = -0.2f;

            _tempMatroxer = Matrix.Identity;
            _tempMatroxer = _WorldMatrix;
            _tempMatroxer.M41 = -0.25f;
            _tempMatroxer.M42 = _player_lft_upper_arm[_index]._POSITION.M42 + (_player_lft_upper_arm[_index]._total_torso_height * 0.5f) + 0.45f;// - 0.25f;
            _tempMatroxer.M43 = -0.25f;
            _tempMatroxer.M44 = 1;
            offsetPosX = 0;
            offsetPosY = 0;
            offsetPosZ = 0;
            vertOffsetX = 0;
            vertOffsetY = 0;
            vertOffsetZ = 0;
            //_player_lft_elbow_target[_index] = new _sc_voxel();
            //_hasinit0 = _player_lft_elbow_target.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, _HWND, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_lft_elbow_target[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_l_hand_x, _inst_p_l_hand_y, _inst_p_l_hand_z, _HWND, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerLeftElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

            _player_lft_elbow_target[_index] = new _sc_voxel();
            _player_lft_elbow_target[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerLeftElbowTarget, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f



            worldMatrix_instances_l_elbow_target[_index] = new Matrix[_inst_p_l_hand_x * _inst_p_l_hand_y * _inst_p_l_hand_z];
            for (int i = 0; i < worldMatrix_instances_l_elbow_target[_index].Length; i++)
            {
                worldMatrix_instances_l_elbow_target[_index][i] = Matrix.Identity;
            }









            //RIGHT ELBOW TARGET TWO
            r = 0.035f;
            g = 0.035f;
            b = 0.035f;
            a = 1;

            _tempMatroxer = Matrix.Identity;
            _tempMatroxer = _WorldMatrix;
            _tempMatroxer.M41 = 1.5f;
            _tempMatroxer.M42 = _player_rght_upper_arm[_index]._POSITION.M42 + (_player_rght_upper_arm[_index]._total_torso_height * 0.5f) + 1;
            _tempMatroxer.M43 = 0;
            _tempMatroxer.M44 = 1;
            offsetPosX = 0;
            offsetPosY = 0;
            offsetPosZ = 0;
            //_player_rght_elbow_target_two[_index] = new _sc_voxel();
            //_hasinit0 = _player_rght_elbow_target_two.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, _HWND, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_rght_elbow_target_two[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

            _player_rght_elbow_target_two[_index] = new _sc_voxel();
            _player_rght_elbow_target_two[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerRightElbowTarget, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f


            worldMatrix_instances_r_elbow_target_two[_index] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
            for (int i = 0; i < worldMatrix_instances_r_elbow_target_two[_index].Length; i++)
            {
                worldMatrix_instances_r_elbow_target_two[_index][i] = Matrix.Identity;
            }








            //LEFT ELBOW TARGET TWO
            r = 0.035f;
            g = 0.035f;
            b = 0.035f;
            a = 1;

            _tempMatroxer = Matrix.Identity;
            _tempMatroxer = _WorldMatrix;
            _tempMatroxer.M41 = -1.5f;
            _tempMatroxer.M42 = _player_lft_upper_arm[_index]._POSITION.M42 + (_player_lft_upper_arm[_index]._total_torso_height * 0.5f) + 1;
            _tempMatroxer.M43 = 0;
            _tempMatroxer.M44 = 1;
            offsetPosX = 0;
            offsetPosY = 0;
            offsetPosZ = 0;
            //_player_lft_elbow_target_two[_index] = new _sc_voxel();

            //_hasinit0 = _player_lft_elbow_target_two.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, _HWND, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_lft_elbow_target_two[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerLeftElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            _player_lft_elbow_target_two[_index] = new _sc_voxel();
            _player_lft_elbow_target_two[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, _HWND, _tempMatroxer, -1, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerLeftElbowTarget, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f



            worldMatrix_instances_l_elbow_target_two[_index] = new Matrix[_inst_p_l_hand_x * _inst_p_l_hand_y * _inst_p_l_hand_z];
            for (int i = 0; i < worldMatrix_instances_l_elbow_target_two[_index].Length; i++)
            {
                worldMatrix_instances_l_elbow_target_two[_index][i] = Matrix.Identity;
            }


        }


        JMatrix matrixer;
        JQuaternion resultQuat;
        JMatrix matrixIn;


        Quaternion _testQuater;
        Vector4 ambientColor = new Vector4(0.75f, 0.75f, 0.75f, 1.0f);
        Vector4 diffuseColour = new Vector4(0.95f, 0.95f, 0.95f, 1);
        Vector3 lightDirection = new Vector3(0, -1, -1);

        Vector3 dirLight;

        public void render_human_rig(Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Matrix viewMatrix, Matrix _projectionMatrix, Vector3 oculusRiftDir, Matrix rotatingMatrix, Vector3 OFFSETPOS, Matrix rotatingMatrixForPelvis, out Matrix _handPositionLeft, out Matrix _handPositionRight, int _index)
        {
            _handPositionLeft = Matrix.Identity;
            _handPositionRight = Matrix.Identity;

            try
            {


                //var _index = x + SC_Console_GRAPHICS._physics_engine_instance_x * (y + SC_Console_GRAPHICS._physics_engine_instance_y * z);

                _rotatingMatrix = rotatingMatrix;

                //Vector3 calculateOffsetOfLookAtMovement... prick calculation again. oh and add it to the offset pos i think... kinda.

                ///////////
                //TORSO////
                ///////////
                ///

                _SC_modL_torso_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_torso[_index]._POSITION.M41, _player_torso[_index]._POSITION.M42, _player_torso[_index]._POSITION.M43),
                    padding1 = 100
                };

                //THE CURRENT PIVOT POINT OF THE TORSO IS RIGHT IN THE MIDDLE OF THE TORSO ITSELF
                Vector3 MOVINGPOINTER = new Vector3(_player_torso[_index]._ORIGINPOSITION.M41, _player_torso[_index]._ORIGINPOSITION.M42, _player_torso[_index]._ORIGINPOSITION.M43);

                //SAVING IN MEMORY THE ORIGINAL TORSO MATRIX NOT AFFECTED BY CURRENT POSITION AND ROTATION CHANGES.
                Matrix _rotMatrixer = _player_torso[_index]._ORIGINPOSITION;
                Quaternion forTest;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                //FROM THE MATRIX OF ROTATION/POSITION, I GET THE QUATERNION OUT OF THAT AND CREATE THE DIRECTIONS THAT THE OBJECTS ARE ORIGINALLY FACING.
                var direction_feet_forward_ori_torso = _getDirection(Vector3.ForwardRH, forTest);
                var direction_feet_right_ori_torso = _getDirection(Vector3.Right, forTest);
                var direction_feet_up_ori_torso = _getDirection(Vector3.Up, forTest);

                //SINCE THE PIVOT POINT IS CURRENTLY IN THE MIDDLE OF THE TORSO, IT CANNOT ROTATE AT THAT POINT OTHERWISE, IT WONT FOLLOW THE PELVIS ROTATION LATER ON.
                //SO WE CURRENTLY ONLY OFFSET THE TORSO "MIDDLE OF SPINE APPROX" TO HALF OF THE CURRENT HEIGHT IN ORDER TO MAKE THE PIVOT POINT, APPROX WHERE THE PELVIS IS.
                Vector3 TORSOPIVOT = MOVINGPOINTER + -(direction_feet_up_ori_torso * (_player_torso[_index]._total_torso_height * 0.5f));
                Vector3 NECKPIVOTORIGINAL = MOVINGPOINTER + (direction_feet_up_ori_torso * (_player_torso[_index]._total_torso_height * 0.5f)); ;
                Vector3 NECKPIVOTORIGINALWITHROTATIONOFFSET = NECKPIVOTORIGINAL;

                //I AM USING THE SAME POINT THAT WAS DECLARED EARLIER TO SHRINK THE NUMBER OF VARIABLES CONTAINED IN THE SCRIPT, EVEN THOUGH THIS IS CURRENTLY ONLY A DRAFT PROJEKT.
                //I AM STARTING THE POSITION OF ALL OF THE FOLLOWING TRANSLATION TO BE ADDED TO THIS ONE. THIS IS SO MUCH EASIER TO COMPREHEND FOR ME THEN USING QUATERNIONS FOR OTHER THINGS.
                //I DO NOT HAVE THE ABILITY YET TO UNDERSTAND WHAT THE X AND Y AND Z AND W OF A QUATERNION IS UNLESS CONVERTED TO THE PITCH/YAW/ROLL OR CARTESIAN/POLAR COORDINATES.
                MOVINGPOINTER = new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                //I FOR SOME REASONS IS SAVING THE ORIGINAL TORSO POSITION INSIDE OF THAT MATRIX AGAIN. FOR SOME REASONS, I THOUGHT "REF" MEANT "FOR THE CURRENT VARIABLE TO ALSO BE MODIFIED AFTER THE FUNCTION HAS RUN".
                //DEFECT IN MY LEARNING PROCESS.
                //_rotMatrixer = _player_torso._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                //CALCULATED IT TWICE... NO NEED FOR THAT.
                //direction_feet_forward_ori_torso = _getDirection(Vector3.ForwardRH, forTest);
                //direction_feet_right_ori_torso = _getDirection(Vector3.Right, forTest);
                //direction_feet_up_ori_torso = _getDirection(Vector3.Up, forTest);

                //REMOVED THAT TOO... WTF AND WHY AM I REMOVING THE TOTAL HEIGHT OF THE TORSO INSTEAD OF JUST HALF IS.ive got no clue. removing it.
                //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori_torso * (_player_torso._total_torso_height * 0.5f));

                //GETTING THE CURRENT ROTATION MATRIX OF THE PIVOT BOTTOM OF SPINE AREA.
                Quaternion otherQuat;
                _rotatingMatrix = rotatingMatrix;
                Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                //CONVERTING THE QUATERNION OF THAT TO THE DIRECTION OF ITS ROTATION
                var direction_feet_forward_torso = _getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right_torso = _getDirection(Vector3.Right, otherQuat);
                var direction_feet_up_torso = _getDirection(Vector3.Up, otherQuat);

                //I AM CALCULATING THE DIFFERENCE IN THE MOVEMENT FROM THE ORIGINAL POSITION TO THE CURRENT OFFSET AT THE BOTTOM OF THE SPINE WHERE I MOVED THAT POINT.
                diffNormPosX = (MOVINGPOINTER.X) - _player_torso[_index]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_torso[_index]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_torso[_index]._ORIGINPOSITION.M43;

                //I AM USING THE NEW PIVOT POINT AT THE BOTTOM OF THE SPINE AND ADDING THE FRONT/RIGHT/UP VECTOR OF THE ROTATION OF THAT SPINE AND THEN ADDING THE DIFFERENCE X/W/Z BETWEEN ORIGINAL POS AND THE NEW PIVOT POS
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right_torso * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_torso * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward_torso * (diffNormPosZ));

                // = MOVINGPOINTER + (direction_feet_up_ori_torso * (_player_torso._total_torso_height * 0.5f));

                NECKPIVOTORIGINALWITHROTATIONOFFSET = NECKPIVOTORIGINALWITHROTATIONOFFSET + -(direction_feet_right_torso * (diffNormPosX * 2));
                NECKPIVOTORIGINALWITHROTATIONOFFSET = NECKPIVOTORIGINALWITHROTATIONOFFSET + -(direction_feet_up_torso * (diffNormPosY * 2));
                NECKPIVOTORIGINALWITHROTATIONOFFSET = NECKPIVOTORIGINALWITHROTATIONOFFSET + -(direction_feet_forward_torso * (diffNormPosZ * 2));

                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;

                matrixerer = Matrix.Identity;
                _rotatingMatrix = rotatingMatrix;
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

                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;
                matrixerer.M44 = 1;

                worldMatrix_Terrain_instances[0] = _WorldMatrix;
                _player_torso[_index]._POSITION = matrixerer;
                _player_torso[_index].Render(_D3D.device.ImmediateContext);
                var _cuber_00 = _player_torso[_index];
                _cuber_00.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_torso(_D3D.device.ImmediateContext, _cuber_00.IndexCount, _cuber_00.InstanceCount, _cuber_00._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_torso_BUFFER, oculusRiftDir, _cuber_00);
                //_player_torso[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_torso[_index]._POSITION.M41, _player_torso[_index]._POSITION.M42, _player_torso[_index]._POSITION.M43);




                ///////////
                //TORSO////
                ///////////


                ///////////
                //SOMETESTS
                _rotatingMatrix = rotatingMatrix;
                Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);
                direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                direction_feet_up = _getDirection(Vector3.Up, otherQuat);

                Vector3 current_rotation_of_torso_pivot_forward = direction_feet_forward;
                Vector3 current_rotation_of_torso_pivot_right = direction_feet_right;
                Vector3 current_rotation_of_torso_pivot_up = direction_feet_up;
                //SOMETESTS
                ///////////


                ///////////
                //HANDRIGHT
                Quaternion.RotationMatrix(ref _rightTouchMatrix, out _testQuater);
                _testQuater.Normalize();
                dirLight = _getDirection(Vector3.ForwardRH, _testQuater);
                dirLight.Normalize();

                _SC_modL_rght_hnd_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_rightTouchMatrix.M41, _rightTouchMatrix.M42, _rightTouchMatrix.M43),
                    padding1 = 100
                };

                MOVINGPOINTER = new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                _rotatingMatrix = rotatingMatrix;
                //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                //direction_feet_up = _getDirection(Vector3.Up, otherQuat);

                diffNormPosX = (MOVINGPOINTER.X) - _rightTouchMatrix.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _rightTouchMatrix.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _rightTouchMatrix.M43;

                MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_right * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_up * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + (current_rotation_of_torso_pivot_forward * (diffNormPosZ));

                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;

                matrixerer = Matrix.Identity;


                var currentPositionOfRightHand = MOVINGPOINTER;

                var currentDirShoulderRightToHandRight = currentPositionOfRightHand - new Vector3(_player_right_shldr[_index]._POSITION.M41, _player_right_shldr[_index]._POSITION.M42, _player_right_shldr[_index]._POSITION.M43);

                if (currentDirShoulderRightToHandRight.Length() > totalArmLengthRight)
                {
                    currentDirShoulderRightToHandRight.Normalize();
                    currentPositionOfRightHand = new Vector3(_player_right_shldr[_index]._POSITION.M41, _player_right_shldr[_index]._POSITION.M42, _player_right_shldr[_index]._POSITION.M43) + (currentDirShoulderRightToHandRight * totalArmLengthRight);
                }




                _rightTouchMatrix = _rightTouchMatrix * rotatingMatrix;

                _rightTouchMatrix.M41 = MOVINGPOINTER.X;
                _rightTouchMatrix.M42 = MOVINGPOINTER.Y;
                _rightTouchMatrix.M43 = MOVINGPOINTER.Z;

                matrixerer.M11 = _rightTouchMatrix.M11;
                matrixerer.M12 = _rightTouchMatrix.M12;
                matrixerer.M13 = _rightTouchMatrix.M13;
                matrixerer.M14 = _rightTouchMatrix.M14;

                matrixerer.M21 = _rightTouchMatrix.M21;
                matrixerer.M22 = _rightTouchMatrix.M22;
                matrixerer.M23 = _rightTouchMatrix.M23;
                matrixerer.M24 = _rightTouchMatrix.M24;

                matrixerer.M31 = _rightTouchMatrix.M31;
                matrixerer.M32 = _rightTouchMatrix.M32;
                matrixerer.M33 = _rightTouchMatrix.M33;
                matrixerer.M34 = _rightTouchMatrix.M34;




                matrixerer.M41 = currentPositionOfRightHand.X;
                matrixerer.M42 = currentPositionOfRightHand.Y;
                matrixerer.M43 = currentPositionOfRightHand.Z;
                matrixerer.M44 = 1;

                worldMatrix_Terrain_instances[0] = _WorldMatrix;
                _player_right_hnd[_index]._POSITION = matrixerer;

                //_player_right_hnd[_index].Render(_D3D.device.ImmediateContext);
                //SC_Console_GRAPHICS._shaderManager._rend_rgt_hnd(_D3D.device.ImmediateContext, _player_right_hnd[_index].IndexCount, _player_right_hnd[_index].InstanceCount, _player_right_hnd[_index]._POSITION, viewMatrix, _projectionMatrix, null, worldMatrix_Terrain_instances, _SC_modL_rght_hnd_BUFFER, oculusRiftDir, _player_right_hnd[_index]);
                //_player_right_hnd[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_right_hnd[_index]._POSITION.M41, _player_right_hnd[_index]._POSITION.M42, _player_right_hnd[_index]._POSITION.M43);
                _player_right_hnd[_index].Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_rgt_hnd(_D3D.device.ImmediateContext, _player_right_hnd[_index].IndexCount, _player_right_hnd[_index].InstanceCount, _player_right_hnd[_index]._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_rght_hnd_BUFFER, oculusRiftDir, _player_right_hnd[_index]);
                _player_right_hnd[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_right_hnd[_index]._POSITION.M41, _player_right_hnd[_index]._POSITION.M42, _player_right_hnd[_index]._POSITION.M43);
                //_handPositionRight.X = matrixerer.M41;
                //_handPositionRight.Y = matrixerer.M42;
                //_handPositionRight.Z = matrixerer.M43;
                _handPositionRight = matrixerer;
                _player_right_hnd[_index]._singleObjectOnly._POSITION = matrixerer;





                ///////////
                //HANDLEFT
                Quaternion.RotationMatrix(ref _leftTouchMatrix, out _testQuater);
                _testQuater.Normalize();
                dirLight = _getDirection(Vector3.ForwardRH, _testQuater);
                dirLight.Normalize();

                _SC_modL_lft_hnd_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_leftTouchMatrix.M41, _leftTouchMatrix.M42, _leftTouchMatrix.M43),
                    padding1 = 100
                };

                MOVINGPOINTER = new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                _rotatingMatrix = rotatingMatrix;
                //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                //direction_feet_up = _getDirection(Vector3.Up, otherQuat);

                diffNormPosX = (MOVINGPOINTER.X) - _leftTouchMatrix.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _leftTouchMatrix.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _leftTouchMatrix.M43;

                MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_right * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_up * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + (current_rotation_of_torso_pivot_forward * (diffNormPosZ));

                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;

                matrixerer = Matrix.Identity;


                currentPositionOfRightHand = MOVINGPOINTER;

                currentDirShoulderRightToHandRight = currentPositionOfRightHand - new Vector3(_player_lft_shldr[_index]._POSITION.M41, _player_lft_shldr[_index]._POSITION.M42, _player_lft_shldr[_index]._POSITION.M43);

                if (currentDirShoulderRightToHandRight.Length() > totalArmLengthRight)
                {
                    currentDirShoulderRightToHandRight.Normalize();
                    currentPositionOfRightHand = new Vector3(_player_lft_shldr[_index]._POSITION.M41, _player_lft_shldr[_index]._POSITION.M42, _player_lft_shldr[_index]._POSITION.M43) + (currentDirShoulderRightToHandRight * totalArmLengthRight);
                }




                _leftTouchMatrix = _leftTouchMatrix * rotatingMatrix;

                _leftTouchMatrix.M41 = MOVINGPOINTER.X;
                _leftTouchMatrix.M42 = MOVINGPOINTER.Y;
                _leftTouchMatrix.M43 = MOVINGPOINTER.Z;

                matrixerer.M11 = _leftTouchMatrix.M11;
                matrixerer.M12 = _leftTouchMatrix.M12;
                matrixerer.M13 = _leftTouchMatrix.M13;
                matrixerer.M14 = _leftTouchMatrix.M14;

                matrixerer.M21 = _leftTouchMatrix.M21;
                matrixerer.M22 = _leftTouchMatrix.M22;
                matrixerer.M23 = _leftTouchMatrix.M23;
                matrixerer.M24 = _leftTouchMatrix.M24;

                matrixerer.M31 = _leftTouchMatrix.M31;
                matrixerer.M32 = _leftTouchMatrix.M32;
                matrixerer.M33 = _leftTouchMatrix.M33;
                matrixerer.M34 = _leftTouchMatrix.M34;

                matrixerer.M41 = currentPositionOfRightHand.X;
                matrixerer.M42 = currentPositionOfRightHand.Y;
                matrixerer.M43 = currentPositionOfRightHand.Z;
                matrixerer.M44 = 1;

                worldMatrix_Terrain_instances[0] = _WorldMatrix;
                _player_left_hnd[_index]._POSITION = matrixerer;
                _player_left_hnd[_index].Render(_D3D.device.ImmediateContext);
                var _cuber_01 = _player_left_hnd[_index];
                _cuber_01.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_rgt_hnd(_D3D.device.ImmediateContext, _cuber_01.IndexCount, _cuber_01.InstanceCount, _cuber_01._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_hnd_BUFFER, oculusRiftDir, _cuber_01);
                _player_left_hnd[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_left_hnd[_index]._POSITION.M41, _player_left_hnd[_index]._POSITION.M42, _player_left_hnd[_index]._POSITION.M43);
                _handPositionLeft = matrixerer;
                _player_left_hnd[_index]._singleObjectOnly._POSITION = matrixerer;



                /*worldMatrix_Terrain_instances[0] = _WorldMatrix;
                _player_left_hnd[_index]._POSITION = matrixerer;
                _player_left_hnd[_index].Render(_D3D.device.ImmediateContext);
                _cuber_00 = _player_left_hnd[_index];
                _cuber_00.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_lft_hnd(_D3D.device.ImmediateContext, _cuber_00.IndexCount, _cuber_00.InstanceCount, _cuber_00._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_hnd_BUFFER, oculusRiftDir, _cuber_00);
                _player_left_hnd[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_left_hnd[_index]._POSITION.M41, _player_left_hnd[_index]._POSITION.M42, _player_left_hnd[_index]._POSITION.M43);
                _handPositionLeft = matrixerer;
                _player_left_hnd[_index]._singleObjectOnly._POSITION = matrixerer;
                */



                ///////////
                //SHOULDER RIGHT
                _SC_modL_rght_shldr_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_right_shldr[_index]._POSITION.M41, _player_right_shldr[_index]._POSITION.M42, _player_right_shldr[_index]._POSITION.M43),
                    padding1 = 100
                };
                _rotatingMatrix = rotatingMatrix;

                MOVINGPOINTER = new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                _rotMatrixer = _player_right_shldr[_index]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                var direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                var direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                var direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_right_shldr[_index]._total_torso_height * 0.5f));
                _rotatingMatrix = rotatingMatrix;
                //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                //direction_feet_up = _getDirection(Vector3.Up, otherQuat);


                diffNormPosX = (MOVINGPOINTER.X) - _player_right_shldr[_index]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_right_shldr[_index]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_right_shldr[_index]._ORIGINPOSITION.M43;


                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * (diffNormPosZ));


                //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * ((float)hyp));

                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;

                //matrixerer = Matrix.Identity;
                //_rotatingMatrix = rotatingMatrix;
                matrixerer = Matrix.Identity;
                _rotatingMatrix = shoulderRotationMatrixRight;


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

                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;
                matrixerer.M44 = 1;

                /*///////////
                worldMatrix_Terrain_instances[0] = _WorldMatrix;
                _player_right_shldr[_index]._POSITION = matrixerer;
                _player_right_shldr[_index].Render(_D3D.device.ImmediateContext);
                _cuber_00 = _player_right_shldr[_index];
                _cuber_00.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_rgt_shldr(_D3D.device.ImmediateContext, _cuber_00.IndexCount, _cuber_00.InstanceCount, _cuber_00._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_rght_shldr_BUFFER, oculusRiftDir, _cuber_00);
                _player_right_shldr[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_right_shldr[_index]._POSITION.M41, _player_right_shldr[_index]._POSITION.M42, _player_right_shldr[_index]._POSITION.M43);
                //_handPositionRight = matrixerer;
                //_player_right_shldr[_index]._singleObjectOnly._POSITION = matrixerer;
                */
                worldMatrix_Terrain_instances[0] = _WorldMatrix;
                _player_right_shldr[_index]._POSITION = matrixerer;
                _player_right_shldr[_index].Render(_D3D.device.ImmediateContext);
                _cuber_01 = _player_right_shldr[_index];
                _cuber_01.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_rgt_shldr(_D3D.device.ImmediateContext, _cuber_01.IndexCount, _cuber_01.InstanceCount, _cuber_01._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_rght_shldr_BUFFER, oculusRiftDir, _cuber_01);
                _player_right_shldr[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_right_shldr[_index]._POSITION.M41, _player_right_shldr[_index]._POSITION.M42, _player_right_shldr[_index]._POSITION.M43);
                //_handPositionLeft = matrixerer;
                _player_right_shldr[_index]._singleObjectOnly._POSITION = matrixerer;






                ///////////
                //SHOULDER LEFT
                _SC_modL_lft_shldr_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_lft_shldr[_index]._POSITION.M41, _player_lft_shldr[_index]._POSITION.M42, _player_lft_shldr[_index]._POSITION.M43),
                    padding1 = 100
                };
                _rotatingMatrix = rotatingMatrix;

                MOVINGPOINTER = new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                _rotMatrixer = _player_lft_shldr[_index]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_lft_shldr[_index]._total_torso_height * 0.5f));
                _rotatingMatrix = rotatingMatrix;
                //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                //direction_feet_up = _getDirection(Vector3.Up, otherQuat);


                diffNormPosX = (MOVINGPOINTER.X) - _player_lft_shldr[_index]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_lft_shldr[_index]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_lft_shldr[_index]._ORIGINPOSITION.M43;


                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * (diffNormPosZ));


                //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * ((float)hyp));

                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;

                //matrixerer = Matrix.Identity;
                //_rotatingMatrix = rotatingMatrix;
                matrixerer = Matrix.Identity;
                _rotatingMatrix = shoulderRotationMatrixRight;


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

                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;
                matrixerer.M44 = 1;

                ///////////
                worldMatrix_Terrain_instances[0] = _WorldMatrix;
                _player_lft_shldr[_index]._POSITION = matrixerer;
                _player_lft_shldr[_index].Render(_D3D.device.ImmediateContext);
                _cuber_00 = _player_lft_shldr[_index];
                _cuber_00.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_rgt_shldr(_D3D.device.ImmediateContext, _cuber_00.IndexCount, _cuber_00.InstanceCount, _cuber_00._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_shldr_BUFFER, oculusRiftDir, _cuber_00);
                _player_lft_shldr[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_lft_shldr[_index]._POSITION.M41, _player_lft_shldr[_index]._POSITION.M42, _player_lft_shldr[_index]._POSITION.M43);
                //_handPositionRight = matrixerer;
                //_player_lft_shldr[_index]._singleObjectOnly._POSITION = matrixerer;









                /////////////////////
                //ELBOW TARGET RIGHT
                _SC_modL_rght_elbow_target_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_rght_elbow_target[_index]._POSITION.M41, _player_rght_elbow_target[_index]._POSITION.M42, _player_rght_elbow_target[_index]._POSITION.M43),
                    padding1 = 100
                };

                MOVINGPOINTER = new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                _rotMatrixer = _player_rght_elbow_target[_index]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_rght_elbow_target[_index]._total_torso_height * 0.5f));
                _rotatingMatrix = rotatingMatrix;
                //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                //direction_feet_up = _getDirection(Vector3.Up, otherQuat);


                diffNormPosX = (MOVINGPOINTER.X) - _player_rght_elbow_target[_index]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_elbow_target[_index]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_elbow_target[_index]._ORIGINPOSITION.M43;

                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * (diffNormPosZ));

                MOVINGPOINTER.X += OFFSETPOS.X;// + _player_rght_elbow_target[_index]._ORIGINPOSITION.M41;
                MOVINGPOINTER.Y += OFFSETPOS.Y;// + _player_rght_elbow_target[_index]._ORIGINPOSITION.M42;// + _player_rght_elbow_target[_index]._ORIGINPOSITION.M42;
                MOVINGPOINTER.Z += OFFSETPOS.Z;// + _player_rght_elbow_target[_index]._ORIGINPOSITION.M43;

                var someDiffX = MOVINGPOINTER.X - _player_right_hnd[_index]._POSITION.M41;
                var someDiffY = MOVINGPOINTER.Y - _player_right_hnd[_index]._POSITION.M42;
                var someDiffZ = MOVINGPOINTER.Z - _player_right_hnd[_index]._POSITION.M43;

                var somePosOfPivotUpperArm = new Vector3(_player_right_shldr[_index]._POSITION.M41, _player_right_shldr[_index]._POSITION.M42, _player_right_shldr[_index]._POSITION.M43); //new Vector3(realPIVOTOfUpperArm.X, realPIVOTOfUpperArm.Y, realPIVOTOfUpperArm.Z); ;// new Vector3(_player_right_shldr[_index]._POSITION.M41, _player_right_shldr[_index]._POSITION.M42, _player_right_shldr[_index]._POSITION.M43);
                var somePosOfRightHand = new Vector3(_player_right_hnd[_index]._POSITION.M41, _player_right_hnd[_index]._POSITION.M42, _player_right_hnd[_index]._POSITION.M43);

                var dirShoulderToHand = somePosOfRightHand - somePosOfPivotUpperArm;
                dirShoulderToHand *= -1;
                //dirShoulderToHand.X *= -1;
                //dirShoulderToHand.Z *= -1;
                //dirShoulderToHand.Y *= -1;

                MOVINGPOINTER = somePosOfPivotUpperArm + (dirShoulderToHand * 2.5f);
                //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_right * -0.15f);
                //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * .0f);
                MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * 3.0f);

                //MOVINGPOINTER= MOVINGPOINTER + (direction_feet_right * 1);
                //Vector3 someOtherOFFSETPOS = MOVINGPOINTER + (direction_feet_right * 5.25f);

                Vector3 someNewPointer = MOVINGPOINTER;

                var diffNormPosXElbowRight = (_player_rght_elbow_target[_index]._ORIGINPOSITION.M41) - (TORSOPIVOT.X);
                var diffNormPosYElbowRight = (_player_rght_elbow_target[_index]._ORIGINPOSITION.M42) - (TORSOPIVOT.Y);
                var diffNormPosZElbowRight = (_player_rght_elbow_target[_index]._ORIGINPOSITION.M43) - (TORSOPIVOT.Z);

                MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));

                someNewPointer.X = someNewPointer.X + MOVINGPOINTER.X;
                someNewPointer.Y = someNewPointer.Y + MOVINGPOINTER.Y;
                someNewPointer.Z = someNewPointer.Z + MOVINGPOINTER.Z;

                matrixerer = Matrix.Identity;
                _rotatingMatrix = rotatingMatrix;
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

                matrixerer.M41 = someNewPointer.X;
                matrixerer.M42 = someNewPointer.Y;
                matrixerer.M43 = someNewPointer.Z;
                matrixerer.M44 = 1;

                worldMatrix_Terrain_instances[0] = _WorldMatrix;
                _player_rght_elbow_target[_index]._POSITION = matrixerer;


                _player_rght_elbow_target[_index].Render(_D3D.device.ImmediateContext);
                _cuber_00 = _player_rght_elbow_target[_index];
                _cuber_00.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_lft_elbow_targ(_D3D.device.ImmediateContext, _cuber_00.IndexCount, _cuber_00.InstanceCount, _cuber_00._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_rght_elbow_target_BUFFER, oculusRiftDir, _cuber_00);
                _player_rght_elbow_target[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_rght_elbow_target[_index]._POSITION.M41, _player_rght_elbow_target[_index]._POSITION.M42, _player_rght_elbow_target[_index]._POSITION.M43);




                //_player_rght_elbow_target[_index].Render(_D3D.device.ImmediateContext);
                //_player_rght_elbow_target[_index].RenderInstancedObject(_D3D.device.ImmediateContext, _player_rght_elbow_target[_index].IndexCount, _player_rght_elbow_target[_index].InstanceCount, _player_rght_elbow_target[_index]._POSITION, viewMatrix, _projectionMatrix, null, worldMatrix_Terrain_instances, _SC_modL_rght_elbow_target_BUFFER, oculusRiftDir);
                //_player_rght_elbow_target[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_rght_elbow_target[_index]._POSITION.M41, _player_rght_elbow_target[_index]._POSITION.M42, _player_rght_elbow_target[_index]._POSITION.M43);
                //ELBOW TARGET RIGHT
                /////////////////////










                //////////////////////////
                //ELBOW TARGET RIGHT TWO
                _SC_modL_rght_elbow_target_two_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_rght_elbow_target_two[_index]._POSITION.M41, _player_rght_elbow_target_two[_index]._POSITION.M42, _player_rght_elbow_target_two[_index]._POSITION.M43),
                    padding1 = 100
                };

                MOVINGPOINTER = new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                _rotMatrixer = _player_rght_elbow_target_two[_index]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_rght_elbow_target_two[_index]._total_torso_height * 0.5f));
                _rotatingMatrix = rotatingMatrix;
                //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                //direction_feet_up = _getDirection(Vector3.Up, otherQuat);


                diffNormPosX = (MOVINGPOINTER.X) - _player_rght_elbow_target_two[_index]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_elbow_target_two[_index]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_elbow_target_two[_index]._ORIGINPOSITION.M43;


                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * (diffNormPosZ));

                xq = otherQuat.X;
                yq = otherQuat.Y;
                zq = otherQuat.Z;
                wq = otherQuat.W;

                pitcha = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq); //(float)(180 / Math.PI)
                yawa = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq); //(float)(180 / Math.PI) *
                rolla = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq); // (float)(180 / Math.PI) *

                hyp = (float)(diffNormPosY / Math.Cos(pitcha));

                //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * ((float)hyp));
                MOVINGPOINTER.X += OFFSETPOS.X;// + _player_rght_elbow_target[_index]_two._ORIGINPOSITION.M41;
                MOVINGPOINTER.Y += OFFSETPOS.Y;// + _player_rght_elbow_target[_index]_two._ORIGINPOSITION.M42;// + _player_rght_elbow_target[_index]_two._ORIGINPOSITION.M42;
                MOVINGPOINTER.Z += OFFSETPOS.Z;// + _player_rght_elbow_target[_index]_two._ORIGINPOSITION.M43;

                someDiffX = MOVINGPOINTER.X - _player_right_hnd[_index]._POSITION.M41;
                someDiffY = MOVINGPOINTER.Y - _player_right_hnd[_index]._POSITION.M42;
                someDiffZ = MOVINGPOINTER.Z - _player_right_hnd[_index]._POSITION.M43;

                somePosOfRightHand = new Vector3(_player_right_hnd[_index]._POSITION.M41, _player_right_hnd[_index]._POSITION.M42, _player_right_hnd[_index]._POSITION.M43);

                //dirShoulderToHand = somePosOfRightHand - new Vector3(_player_rght_upper_arm[_index]._POSITION.M41, _player_rght_upper_arm[_index]._POSITION.M42, _player_rght_upper_arm[_index]._POSITION.M43);
                dirShoulderToHand = somePosOfRightHand - new Vector3(_player_right_shldr[_index]._POSITION.M41, _player_right_shldr[_index]._POSITION.M42, _player_right_shldr[_index]._POSITION.M43);
                dirShoulderToHand = somePosOfRightHand - somePosOfPivotUpperArm;

                MOVINGPOINTER = somePosOfPivotUpperArm + (dirShoulderToHand * 2.5f);

                var someOffsetter = somePosOfRightHand - OFFSETPOS;
                Vector3 someOtherPivotPoint = MOVINGPOINTER;

                //MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * 1.0f);
                //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_forward_ori * 1.0f);

                someNewPointer = MOVINGPOINTER;

                diffNormPosXElbowRight = (_player_rght_elbow_target_two[_index]._ORIGINPOSITION.M41) - (TORSOPIVOT.X);
                diffNormPosYElbowRight = (_player_rght_elbow_target_two[_index]._ORIGINPOSITION.M42) - (TORSOPIVOT.Y);
                diffNormPosZElbowRight = (_player_rght_elbow_target_two[_index]._ORIGINPOSITION.M43) - (TORSOPIVOT.Z);

                MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));

                someNewPointer.X = someNewPointer.X + MOVINGPOINTER.X;
                someNewPointer.Y = someNewPointer.Y + MOVINGPOINTER.Y;
                someNewPointer.Z = someNewPointer.Z + MOVINGPOINTER.Z;

                //someNewPointer.X = MOVINGPOINTER.X;
                //someNewPointer.Y = MOVINGPOINTER.Y;
                //someNewPointer.Z = MOVINGPOINTER.Z;

                //someNewPointer.Y *= -1;
                //someNewPointer.Y = _player_right_shldr[_index]._POSITION.M42;

                matrixerer = Matrix.Identity;
                _rotatingMatrix = rotatingMatrix;
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

                matrixerer.M41 = someNewPointer.X;
                matrixerer.M42 = someNewPointer.Y;
                matrixerer.M43 = someNewPointer.Z;
                matrixerer.M44 = 1;

                worldMatrix_Terrain_instances[0] = _WorldMatrix;
                _player_rght_elbow_target_two[_index]._POSITION = matrixerer;

                _player_rght_elbow_target_two[_index].Render(_D3D.device.ImmediateContext);
                _cuber_00 = _player_rght_elbow_target_two[_index];
                _cuber_00.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_rgt_elbow_targ_two(_D3D.device.ImmediateContext, _cuber_00.IndexCount, _cuber_00.InstanceCount, _cuber_00._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_rght_elbow_target_two_BUFFER, oculusRiftDir, _cuber_00);
                _player_rght_elbow_target_two[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_rght_elbow_target_two[_index]._POSITION.M41, _player_rght_elbow_target_two[_index]._POSITION.M42, _player_rght_elbow_target_two[_index]._POSITION.M43);

                //_player_rght_elbow_target[_index]_two.Render(_D3D.device.ImmediateContext);
                //_player_rght_elbow_target[_index]_two.RenderInstancedObject(_D3D.device.ImmediateContext, _player_rght_elbow_target[_index]_two.IndexCount, _player_rght_elbow_target[_index]_two.InstanceCount, _player_rght_elbow_target[_index]_two._POSITION, viewMatrix, _projectionMatrix, null, worldMatrix_Terrain_instances, _SC_modL_rght_elbow_target_two_BUFFER, oculusRiftDir);
                //_player_rght_elbow_target[_index]_two._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_rght_elbow_target[_index]_two._POSITION.M41, _player_rght_elbow_target[_index]_two._POSITION.M42, _player_rght_elbow_target[_index]_two._POSITION.M43);
                //ELBOW TARGET RIGHT TWO
                //////////////////////////







                //////////////////
                //UPPER ARM RIGHT
                _SC_modL_rght_upper_arm_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_rght_upper_arm[_index]._POSITION.M41, _player_rght_upper_arm[_index]._POSITION.M42, _player_rght_upper_arm[_index]._POSITION.M43),
                    padding1 = 100
                };

                MOVINGPOINTER = new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                _rotMatrixer = _player_right_shldr[_index]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_up_ori * (_player_right_shldr[_index]._total_torso_height * 0.5f));
                //MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * (_player_right_shldr[_index]._total_torso_height * 0.5f));

                _rotatingMatrix = rotatingMatrix;

                //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);
                //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                //direction_feet_up = _getDirection(Vector3.Up, otherQuat);

                diffNormPosX = (MOVINGPOINTER.X) - _player_right_shldr[_index]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_right_shldr[_index]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_right_shldr[_index]._ORIGINPOSITION.M43;

                realPIVOTOfUpperArm = MOVINGPOINTER;

                realPositionOfUpperArm = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_up * (diffNormPosY));
                realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_forward * (diffNormPosZ));

                realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_right * (diffNormPosX));
                realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_up * (diffNormPosY));
                realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_forward * (diffNormPosZ));
                //realPIVOTOfUpperArm = realPIVOTOfUpperArm + (direction_feet_up_ori * (_player_right_shldr[_index]._total_torso_height * 0.5f));

                realPIVOTOfUpperArm.X = realPIVOTOfUpperArm.X + OFFSETPOS.X;
                realPIVOTOfUpperArm.Y = realPIVOTOfUpperArm.Y + OFFSETPOS.Y;
                realPIVOTOfUpperArm.Z = realPIVOTOfUpperArm.Z + OFFSETPOS.Z;

                Vector3 currentFINALPIVOTUPPERARM = realPIVOTOfUpperArm;

                //Vector3 somePosOfRightShoulder = new Vector3(_player_right_shldr[_index]._POSITION.M41, _player_right_shldr[_index]._POSITION.M42, _player_right_shldr[_index]._POSITION.M43);
                somePosOfRightHand = new Vector3(_player_right_hnd[_index]._POSITION.M41, _player_right_hnd[_index]._POSITION.M42, _player_right_hnd[_index]._POSITION.M43);
                var somePosOfUpperElbowTargetTwo = new Vector3(_player_rght_elbow_target_two[_index]._POSITION.M41, _player_rght_elbow_target_two[_index]._POSITION.M42, _player_rght_elbow_target_two[_index]._POSITION.M43);
                var somePosOfUpperElbowTargetOne = new Vector3(_player_rght_elbow_target[_index]._POSITION.M41, _player_rght_elbow_target[_index]._POSITION.M42, _player_rght_elbow_target[_index]._POSITION.M43);

                var someDirFromElbowTargetOneToTwo = somePosOfUpperElbowTargetTwo - somePosOfUpperElbowTargetOne;
                var someDirFromElbowTargetOneToRghtHand = somePosOfRightHand - somePosOfUpperElbowTargetOne;

                Vector3 crossRes;
                Vector3.Cross(ref someDirFromElbowTargetOneToTwo, ref someDirFromElbowTargetOneToRghtHand, out crossRes);
                crossRes.Normalize();

                var pointA = realPIVOTOfUpperArm + (-crossRes);

                var someDirFromPivotUpperToHand = somePosOfRightHand - realPIVOTOfUpperArm;
                var lengthOfDirFromPivotUpperToHand = someDirFromPivotUpperToHand.Length();
                someDirFromPivotUpperToHand.Normalize();

                var someDirFromPivotUpperToA = pointA - realPIVOTOfUpperArm;
                var lengthOfDirFromPivotUpperToA = someDirFromPivotUpperToA.Length();
                someDirFromPivotUpperToA.Normalize();



                lengthOfLowerArmRight = _player_rght_lower_arm[_index]._total_torso_height * 2.55f;
                lengthOfUpperArmRight = _player_rght_upper_arm[_index]._total_torso_height * 2.45f;
                totalArmLengthRight = lengthOfLowerArmRight + lengthOfUpperArmRight;

                lengthOfDirFromPivotUpperToHand = Math.Min(lengthOfDirFromPivotUpperToHand, totalArmLengthRight - totalArmLengthRight * 0.001f);

                var upperEquationCirCirIntersect = (lengthOfDirFromPivotUpperToHand * lengthOfDirFromPivotUpperToHand) - (lengthOfLowerArmRight * lengthOfLowerArmRight) + (lengthOfUpperArmRight * lengthOfUpperArmRight);
                var adjacentSolvingForX = upperEquationCirCirIntersect / (2 * lengthOfDirFromPivotUpperToHand);

                var resulter = Math.Pow(lengthOfUpperArmRight, 2) - Math.Pow(adjacentSolvingForX, 2);
                if (resulter < 0)
                {
                    resulter *= -1;
                }

                var oppositeSolvingForHalfA = (float)Math.Sqrt(resulter);

                oppositeSolvingForHalfA = Math.Min(oppositeSolvingForHalfA, lengthOfUpperArmRight - lengthOfUpperArmRight * 0.001f);

                someNewPointer = realPIVOTOfUpperArm + (someDirFromPivotUpperToHand * adjacentSolvingForX);
                Vector3.Cross(ref someDirFromPivotUpperToA, ref someDirFromPivotUpperToHand, out crossRes);
                crossRes.Normalize();

                someNewPointer = someNewPointer + (crossRes * oppositeSolvingForHalfA);

                diffNormPosXElbowRight = (_player_rght_upper_arm[_index]._ORIGINPOSITION.M41) - (TORSOPIVOT.X);
                diffNormPosYElbowRight = (_player_rght_upper_arm[_index]._ORIGINPOSITION.M42) - (TORSOPIVOT.Y);
                diffNormPosZElbowRight = (_player_rght_upper_arm[_index]._ORIGINPOSITION.M43) - (TORSOPIVOT.Z);

                MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));

                someNewPointer.X = someNewPointer.X + MOVINGPOINTER.X;
                someNewPointer.Y = someNewPointer.Y + MOVINGPOINTER.Y;
                someNewPointer.Z = someNewPointer.Z + MOVINGPOINTER.Z;

                Vector3 elbowPositionRight = someNewPointer;

                Vector3 dirPivotUpperRIghtToElbowRight = elbowPositionRight - currentFINALPIVOTUPPERARM;

                Vector3 currentPositionOfUPPERARMROTATION3DPOSITION = currentFINALPIVOTUPPERARM + (dirPivotUpperRIghtToElbowRight * 0.5f);

                Vector3 dirElbowRightToHand = somePosOfRightHand - elbowPositionRight;

                dirPivotUpperRIghtToElbowRight.Normalize();
                dirElbowRightToHand.Normalize();

                Vector3 someCross0;
                Vector3.Cross(ref dirPivotUpperRIghtToElbowRight, ref dirElbowRightToHand, out someCross0);
                someCross0.Normalize();

                Vector3 someCross1;
                Vector3.Cross(ref dirPivotUpperRIghtToElbowRight, ref someCross0, out someCross1);
                someCross1.Normalize();

                //Vector3 upper = someCross1;
                //Vector3 forward = dirPivotUpperRIghtToElbowRight;
                //Vector3 upperWORLD = Vector3.Up;

                shoulderRotationMatrixRight = Matrix.LookAtRH(currentFINALPIVOTUPPERARM, currentFINALPIVOTUPPERARM + someCross0, dirPivotUpperRIghtToElbowRight);
                shoulderRotationMatrixRight.Invert();
                matrixerer = shoulderRotationMatrixRight;

                matrixerer.M41 = currentPositionOfUPPERARMROTATION3DPOSITION.X;// + OFFSETPOS.X;
                matrixerer.M42 = currentPositionOfUPPERARMROTATION3DPOSITION.Y;// + OFFSETPOS.Y;
                matrixerer.M43 = currentPositionOfUPPERARMROTATION3DPOSITION.Z;// + OFFSETPOS.Z;
                matrixerer.M44 = 1;


                //////////////////
                worldMatrix_Terrain_instances[0] = _WorldMatrix;
                _player_rght_upper_arm[_index]._POSITION = matrixerer;
                /*_player_rght_upper_arm[_index].Render(_D3D.device.ImmediateContext);
                _cuber_00 = _player_rght_upper_arm[_index];
                _cuber_00.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_rgt_shldr(_D3D.device.ImmediateContext, _cuber_00.IndexCount, _cuber_00.InstanceCount, _cuber_00._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_torso_BUFFER, oculusRiftDir, _cuber_00);
                _player_rght_upper_arm[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_rght_upper_arm[_index]._POSITION.M41, _player_rght_upper_arm[_index]._POSITION.M42, _player_rght_upper_arm[_index]._POSITION.M43);
                */

                _player_rght_upper_arm[_index].Render(_D3D.device.ImmediateContext);
                _cuber_00 = _player_rght_upper_arm[_index];
                _cuber_00.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_rgt_upper_arm(_D3D.device.ImmediateContext, _cuber_00.IndexCount, _cuber_00.InstanceCount, _cuber_00._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_rght_upper_arm_BUFFER, oculusRiftDir, _cuber_00);
                _player_rght_upper_arm[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_rght_upper_arm[_index]._POSITION.M41, _player_rght_upper_arm[_index]._POSITION.M42, _player_rght_upper_arm[_index]._POSITION.M43);



                //_handPositionRight = matrixerer;
                //_player_rght_upper_arm[_index]._singleObjectOnly._POSITION = matrixerer;






                /////////////////
                //RIGHT LOWER ARM
                _SC_modL_rght_lower_arm_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_rght_lower_arm[_index]._POSITION.M41, _player_rght_lower_arm[_index]._POSITION.M42, _player_rght_lower_arm[_index]._POSITION.M43),
                    padding1 = 100
                };

                var somePosererDir = somePosOfRightHand - elbowPositionRight;

                var someLowerRightArmPos = elbowPositionRight + (somePosererDir * 0.5f);
                somePosererDir.Normalize();

                //someCross0.Z *= -1;

                Vector3.Cross(ref somePosererDir, ref someCross0, out someCross1);
                someCross1.Normalize();

                var theLowerArmRotationMatrix = Matrix.LookAtRH(elbowPositionRight, elbowPositionRight + someCross1, somePosererDir);
                theLowerArmRotationMatrix.Invert();
                matrixerer = theLowerArmRotationMatrix;

                matrixerer.M41 = someLowerRightArmPos.X;// + OFFSETPOS.X;
                matrixerer.M42 = someLowerRightArmPos.Y;// + OFFSETPOS.Y;
                matrixerer.M43 = someLowerRightArmPos.Z;// + OFFSETPOS.Z;
                matrixerer.M44 = 1;

                worldMatrix_Terrain_instances[0] = _WorldMatrix;
                _player_rght_lower_arm[_index]._POSITION = matrixerer;


                _player_rght_lower_arm[_index].Render(_D3D.device.ImmediateContext);
                _cuber_00 = _player_rght_lower_arm[_index];
                _cuber_00.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_rgt_lower_arm(_D3D.device.ImmediateContext, _cuber_00.IndexCount, _cuber_00.InstanceCount, _cuber_00._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_rght_lower_arm_BUFFER, oculusRiftDir, _cuber_00);
                _player_rght_lower_arm[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_rght_lower_arm[_index]._POSITION.M41, _player_rght_lower_arm[_index]._POSITION.M42, _player_rght_lower_arm[_index]._POSITION.M43);



                /*_player_rght_lower_arm[_index].Render(_D3D.device.ImmediateContext);
                _cuber_00 = _player_rght_lower_arm[_index];
                _cuber_00.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_rgt_shldr(_D3D.device.ImmediateContext, _cuber_00.IndexCount, _cuber_00.InstanceCount, _cuber_00._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_torso_BUFFER, oculusRiftDir, _cuber_00);
                _player_rght_lower_arm[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_rght_lower_arm[_index]._POSITION.M41, _player_rght_lower_arm[_index]._POSITION.M42, _player_rght_lower_arm[_index]._POSITION.M43);
                *///_handPositionRight = matrixerer;
                  //_player_rght_lower_arm[_index]._singleObjectOnly._POSITION = matrixerer;











                ///////////////////
                //LEFTTTTTTTTTTTTT

                /////////////////////
                //ELBOW TARGET LEFT
                _SC_modL_lft_elbow_target_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_lft_elbow_target[_index]._POSITION.M41, _player_lft_elbow_target[_index]._POSITION.M42, _player_lft_elbow_target[_index]._POSITION.M43),
                    padding1 = 100
                };

                MOVINGPOINTER = new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                _rotMatrixer = _player_lft_elbow_target[_index]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_lft_elbow_target[_index]._total_torso_height * 0.5f));
                _rotatingMatrix = rotatingMatrix;
                //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                //direction_feet_up = _getDirection(Vector3.Up, otherQuat);


                diffNormPosX = (MOVINGPOINTER.X) - _player_lft_elbow_target[_index]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_lft_elbow_target[_index]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_lft_elbow_target[_index]._ORIGINPOSITION.M43;

                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * (diffNormPosZ));

                MOVINGPOINTER.X += OFFSETPOS.X;// + _player_lft_elbow_target[_index]._ORIGINPOSITION.M41;
                MOVINGPOINTER.Y += OFFSETPOS.Y;// + _player_lft_elbow_target[_index]._ORIGINPOSITION.M42;// + _player_lft_elbow_target[_index]._ORIGINPOSITION.M42;
                MOVINGPOINTER.Z += OFFSETPOS.Z;// + _player_lft_elbow_target[_index]._ORIGINPOSITION.M43;

                someDiffX = MOVINGPOINTER.X - _player_left_hnd[_index]._POSITION.M41;
                someDiffY = MOVINGPOINTER.Y - _player_left_hnd[_index]._POSITION.M42;
                someDiffZ = MOVINGPOINTER.Z - _player_left_hnd[_index]._POSITION.M43;

                somePosOfPivotUpperArm = new Vector3(_player_lft_shldr[_index]._POSITION.M41, _player_lft_shldr[_index]._POSITION.M42, _player_lft_shldr[_index]._POSITION.M43); //new Vector3(realPIVOTOfUpperArm.X, realPIVOTOfUpperArm.Y, realPIVOTOfUpperArm.Z); ;// new Vector3(_player_right_shldr[_index]._POSITION.M41, _player_right_shldr[_index]._POSITION.M42, _player_right_shldr[_index]._POSITION.M43);
                somePosOfRightHand = new Vector3(_player_left_hnd[_index]._POSITION.M41, _player_left_hnd[_index]._POSITION.M42, _player_left_hnd[_index]._POSITION.M43);

                dirShoulderToHand = somePosOfRightHand - somePosOfPivotUpperArm;
                dirShoulderToHand *= -1;
                //dirShoulderToHand.X *= -1;
                //dirShoulderToHand.Z *= -1;
                //dirShoulderToHand.Y *= -1;

                MOVINGPOINTER = somePosOfPivotUpperArm + (dirShoulderToHand * 2.5f);
                //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_right * -0.15f);
                //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * .0f);
                MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * 3.0f);

                //MOVINGPOINTER= MOVINGPOINTER + (direction_feet_right * 1);
                //Vector3 someOtherOFFSETPOS = MOVINGPOINTER + (direction_feet_right * 5.25f);

                someNewPointer = MOVINGPOINTER;

                diffNormPosXElbowRight = (_player_lft_elbow_target[_index]._ORIGINPOSITION.M41) - (TORSOPIVOT.X);
                diffNormPosYElbowRight = (_player_lft_elbow_target[_index]._ORIGINPOSITION.M42) - (TORSOPIVOT.Y);
                diffNormPosZElbowRight = (_player_lft_elbow_target[_index]._ORIGINPOSITION.M43) - (TORSOPIVOT.Z);

                MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));

                someNewPointer.X = someNewPointer.X + MOVINGPOINTER.X;
                someNewPointer.Y = someNewPointer.Y + MOVINGPOINTER.Y;
                someNewPointer.Z = someNewPointer.Z + MOVINGPOINTER.Z;

                matrixerer = Matrix.Identity;
                _rotatingMatrix = rotatingMatrix;
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

                matrixerer.M41 = someNewPointer.X;
                matrixerer.M42 = someNewPointer.Y;
                matrixerer.M43 = someNewPointer.Z;
                matrixerer.M44 = 1;

                worldMatrix_Terrain_instances[0] = _WorldMatrix;
                _player_lft_elbow_target[_index]._POSITION = matrixerer;


                _player_lft_elbow_target[_index].Render(_D3D.device.ImmediateContext);
                _cuber_00 = _player_lft_elbow_target[_index];
                _cuber_00.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_lft_elbow_targ(_D3D.device.ImmediateContext, _cuber_00.IndexCount, _cuber_00.InstanceCount, _cuber_00._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_elbow_target_BUFFER, oculusRiftDir, _cuber_00);
                _player_lft_elbow_target[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_lft_elbow_target[_index]._POSITION.M41, _player_lft_elbow_target[_index]._POSITION.M42, _player_lft_elbow_target[_index]._POSITION.M43);



                //_player_lft_elbow_target[_index].Render(_D3D.device.ImmediateContext);
                //_player_lft_elbow_target[_index].RenderInstancedObject(_D3D.device.ImmediateContext, _player_lft_elbow_target[_index].IndexCount, _player_lft_elbow_target[_index].InstanceCount, _player_lft_elbow_target[_index]._POSITION, viewMatrix, _projectionMatrix, null, worldMatrix_Terrain_instances, _SC_modL_lft_elbow_target_BUFFER, oculusRiftDir);
                //_player_lft_elbow_target[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_lft_elbow_target[_index]._POSITION.M41, _player_lft_elbow_target[_index]._POSITION.M42, _player_lft_elbow_target[_index]._POSITION.M43);
                //ELBOW TARGET LEFT
                /////////////////////











                //////////////////////////
                //ELBOW TARGET LEFT TWO
                _SC_modL_lft_elbow_target_two_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_lft_elbow_target_two[_index]._POSITION.M41, _player_lft_elbow_target_two[_index]._POSITION.M42, _player_lft_elbow_target_two[_index]._POSITION.M43),
                    padding1 = 100
                };

                MOVINGPOINTER = new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                _rotMatrixer = _player_lft_elbow_target_two[_index]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_lft_elbow_target_two[_index]._total_torso_height * 0.5f));
                _rotatingMatrix = rotatingMatrix;
                //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                //direction_feet_up = _getDirection(Vector3.Up, otherQuat);


                diffNormPosX = (MOVINGPOINTER.X) - _player_lft_elbow_target_two[_index]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_lft_elbow_target_two[_index]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_lft_elbow_target_two[_index]._ORIGINPOSITION.M43;


                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * (diffNormPosZ));

                xq = otherQuat.X;
                yq = otherQuat.Y;
                zq = otherQuat.Z;
                wq = otherQuat.W;

                pitcha = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq); //(float)(180 / Math.PI)
                yawa = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq); //(float)(180 / Math.PI) *
                rolla = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq); // (float)(180 / Math.PI) *

                hyp = (float)(diffNormPosY / Math.Cos(pitcha));

                //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * ((float)hyp));
                MOVINGPOINTER.X += OFFSETPOS.X;// + _player_rght_elbow_target[_index]_two._ORIGINPOSITION.M41;
                MOVINGPOINTER.Y += OFFSETPOS.Y;// + _player_rght_elbow_target[_index]_two._ORIGINPOSITION.M42;// + _player_rght_elbow_target[_index]_two._ORIGINPOSITION.M42;
                MOVINGPOINTER.Z += OFFSETPOS.Z;// + _player_rght_elbow_target[_index]_two._ORIGINPOSITION.M43;

                someDiffX = MOVINGPOINTER.X - _player_left_hnd[_index]._POSITION.M41;
                someDiffY = MOVINGPOINTER.Y - _player_left_hnd[_index]._POSITION.M42;
                someDiffZ = MOVINGPOINTER.Z - _player_left_hnd[_index]._POSITION.M43;

                somePosOfRightHand = new Vector3(_player_left_hnd[_index]._POSITION.M41, _player_left_hnd[_index]._POSITION.M42, _player_left_hnd[_index]._POSITION.M43);

                //dirShoulderToHand = somePosOfRightHand - new Vector3(_player_rght_upper_arm[_index]._POSITION.M41, _player_rght_upper_arm[_index]._POSITION.M42, _player_rght_upper_arm[_index]._POSITION.M43);
                dirShoulderToHand = somePosOfRightHand - new Vector3(_player_lft_shldr[_index]._POSITION.M41, _player_lft_shldr[_index]._POSITION.M42, _player_lft_shldr[_index]._POSITION.M43);
                dirShoulderToHand = somePosOfRightHand - somePosOfPivotUpperArm;

                MOVINGPOINTER = somePosOfPivotUpperArm + (dirShoulderToHand * 2.5f);

                someOffsetter = somePosOfRightHand - OFFSETPOS;
                someOtherPivotPoint = MOVINGPOINTER;

                //MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * 1.0f);
                //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_forward_ori * 1.0f);

                someNewPointer = MOVINGPOINTER;

                diffNormPosXElbowRight = (_player_lft_elbow_target_two[_index]._ORIGINPOSITION.M41) - (TORSOPIVOT.X);
                diffNormPosYElbowRight = (_player_lft_elbow_target_two[_index]._ORIGINPOSITION.M42) - (TORSOPIVOT.Y);
                diffNormPosZElbowRight = (_player_lft_elbow_target_two[_index]._ORIGINPOSITION.M43) - (TORSOPIVOT.Z);

                MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));

                someNewPointer.X = someNewPointer.X + MOVINGPOINTER.X;
                someNewPointer.Y = someNewPointer.Y + MOVINGPOINTER.Y;
                someNewPointer.Z = someNewPointer.Z + MOVINGPOINTER.Z;

                //someNewPointer.X = MOVINGPOINTER.X;
                //someNewPointer.Y = MOVINGPOINTER.Y;
                //someNewPointer.Z = MOVINGPOINTER.Z;

                //someNewPointer.Y *= -1;
                //someNewPointer.Y = _player_right_shldr[_index]._POSITION.M42;

                matrixerer = Matrix.Identity;
                _rotatingMatrix = rotatingMatrix;
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

                matrixerer.M41 = someNewPointer.X;
                matrixerer.M42 = someNewPointer.Y;
                matrixerer.M43 = someNewPointer.Z;
                matrixerer.M44 = 1;

                worldMatrix_Terrain_instances[0] = _WorldMatrix;
                _player_lft_elbow_target_two[_index]._POSITION = matrixerer;

                _player_lft_elbow_target_two[_index].Render(_D3D.device.ImmediateContext);
                _cuber_00 = _player_lft_elbow_target_two[_index];
                _cuber_00.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_rgt_elbow_targ_two(_D3D.device.ImmediateContext, _cuber_00.IndexCount, _cuber_00.InstanceCount, _cuber_00._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_elbow_target_two_BUFFER, oculusRiftDir, _cuber_00);
                _player_lft_elbow_target_two[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_lft_elbow_target_two[_index]._POSITION.M41, _player_lft_elbow_target_two[_index]._POSITION.M42, _player_lft_elbow_target_two[_index]._POSITION.M43);

                //_player_rght_elbow_target[_index]_two.Render(_D3D.device.ImmediateContext);
                //_player_rght_elbow_target[_index]_two.RenderInstancedObject(_D3D.device.ImmediateContext, _player_rght_elbow_target[_index]_two.IndexCount, _player_rght_elbow_target[_index]_two.InstanceCount, _player_rght_elbow_target[_index]_two._POSITION, viewMatrix, _projectionMatrix, null, worldMatrix_Terrain_instances, _SC_modL_rght_elbow_target_two_BUFFER, oculusRiftDir);
                //_player_rght_elbow_target[_index]_two._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_rght_elbow_target[_index]_two._POSITION.M41, _player_rght_elbow_target[_index]_two._POSITION.M42, _player_rght_elbow_target[_index]_two._POSITION.M43);
                //ELBOW TARGET LEFT TWO
                //////////////////////////









                //////////////////
                //UPPER ARM LEFT
                _SC_modL_lft_upper_arm_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_lft_upper_arm[_index]._POSITION.M41, _player_lft_upper_arm[_index]._POSITION.M42, _player_lft_upper_arm[_index]._POSITION.M43),
                    padding1 = 100
                };

                MOVINGPOINTER = new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                _rotMatrixer = _player_lft_shldr[_index]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_up_ori * (_player_right_shldr[_index]._total_torso_height * 0.5f));
                //MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * (_player_right_shldr[_index]._total_torso_height * 0.5f));

                _rotatingMatrix = rotatingMatrix;

                //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);
                //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                //direction_feet_up = _getDirection(Vector3.Up, otherQuat);

                diffNormPosX = (MOVINGPOINTER.X) - _player_lft_shldr[_index]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_lft_shldr[_index]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_lft_shldr[_index]._ORIGINPOSITION.M43;

                realPIVOTOfUpperArm = MOVINGPOINTER;

                realPositionOfUpperArm = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_up * (diffNormPosY));
                realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_forward * (diffNormPosZ));

                realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_right * (diffNormPosX));
                realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_up * (diffNormPosY));
                realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_forward * (diffNormPosZ));
                //realPIVOTOfUpperArm = realPIVOTOfUpperArm + (direction_feet_up_ori * (_player_right_shldr[_index]._total_torso_height * 0.5f));

                realPIVOTOfUpperArm.X = realPIVOTOfUpperArm.X + OFFSETPOS.X;
                realPIVOTOfUpperArm.Y = realPIVOTOfUpperArm.Y + OFFSETPOS.Y;
                realPIVOTOfUpperArm.Z = realPIVOTOfUpperArm.Z + OFFSETPOS.Z;

                currentFINALPIVOTUPPERARM = realPIVOTOfUpperArm;

                //Vector3 somePosOfRightShoulder = new Vector3(_player_right_shldr[_index]._POSITION.M41, _player_right_shldr[_index]._POSITION.M42, _player_right_shldr[_index]._POSITION.M43);
                somePosOfRightHand = new Vector3(_player_left_hnd[_index]._POSITION.M41, _player_left_hnd[_index]._POSITION.M42, _player_left_hnd[_index]._POSITION.M43);
                somePosOfUpperElbowTargetTwo = new Vector3(_player_lft_elbow_target_two[_index]._POSITION.M41, _player_lft_elbow_target_two[_index]._POSITION.M42, _player_lft_elbow_target_two[_index]._POSITION.M43);
                somePosOfUpperElbowTargetOne = new Vector3(_player_lft_elbow_target[_index]._POSITION.M41, _player_lft_elbow_target[_index]._POSITION.M42, _player_lft_elbow_target[_index]._POSITION.M43);

                someDirFromElbowTargetOneToTwo = somePosOfUpperElbowTargetTwo - somePosOfUpperElbowTargetOne;
                someDirFromElbowTargetOneToRghtHand = somePosOfRightHand - somePosOfUpperElbowTargetOne;

                //Vector3 crossRes;
                Vector3.Cross(ref someDirFromElbowTargetOneToTwo, ref someDirFromElbowTargetOneToRghtHand, out crossRes);
                crossRes.Normalize();

                pointA = realPIVOTOfUpperArm + (-crossRes);

                someDirFromPivotUpperToHand = somePosOfRightHand - realPIVOTOfUpperArm;
                lengthOfDirFromPivotUpperToHand = someDirFromPivotUpperToHand.Length();
                someDirFromPivotUpperToHand.Normalize();

                someDirFromPivotUpperToA = pointA - realPIVOTOfUpperArm;
                lengthOfDirFromPivotUpperToA = someDirFromPivotUpperToA.Length();
                someDirFromPivotUpperToA.Normalize();



                lengthOfLowerArmRight = _player_lft_lower_arm[_index]._total_torso_height * 2.55f;
                lengthOfUpperArmRight = _player_lft_upper_arm[_index]._total_torso_height * 2.45f;
                totalArmLengthRight = lengthOfLowerArmRight + lengthOfUpperArmRight;

                lengthOfDirFromPivotUpperToHand = Math.Min(lengthOfDirFromPivotUpperToHand, totalArmLengthRight - totalArmLengthRight * 0.001f);

                upperEquationCirCirIntersect = (lengthOfDirFromPivotUpperToHand * lengthOfDirFromPivotUpperToHand) - (lengthOfLowerArmRight * lengthOfLowerArmRight) + (lengthOfUpperArmRight * lengthOfUpperArmRight);
                adjacentSolvingForX = upperEquationCirCirIntersect / (2 * lengthOfDirFromPivotUpperToHand);

                resulter = Math.Pow(lengthOfUpperArmRight, 2) - Math.Pow(adjacentSolvingForX, 2);
                if (resulter < 0)
                {
                    resulter *= -1;
                }

                oppositeSolvingForHalfA = (float)Math.Sqrt(resulter);

                oppositeSolvingForHalfA = Math.Min(oppositeSolvingForHalfA, lengthOfUpperArmRight - lengthOfUpperArmRight * 0.001f);

                someNewPointer = realPIVOTOfUpperArm + (someDirFromPivotUpperToHand * adjacentSolvingForX);
                Vector3.Cross(ref someDirFromPivotUpperToA, ref someDirFromPivotUpperToHand, out crossRes);
                crossRes.Normalize();

                someNewPointer = someNewPointer + (crossRes * oppositeSolvingForHalfA);

                diffNormPosXElbowRight = (_player_lft_upper_arm[_index]._ORIGINPOSITION.M41) - (TORSOPIVOT.X);
                diffNormPosYElbowRight = (_player_lft_upper_arm[_index]._ORIGINPOSITION.M42) - (TORSOPIVOT.Y);
                diffNormPosZElbowRight = (_player_lft_upper_arm[_index]._ORIGINPOSITION.M43) - (TORSOPIVOT.Z);

                MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));

                someNewPointer.X = someNewPointer.X + MOVINGPOINTER.X;
                someNewPointer.Y = someNewPointer.Y + MOVINGPOINTER.Y;
                someNewPointer.Z = someNewPointer.Z + MOVINGPOINTER.Z;

                elbowPositionRight = someNewPointer;

                dirPivotUpperRIghtToElbowRight = elbowPositionRight - currentFINALPIVOTUPPERARM;

                currentPositionOfUPPERARMROTATION3DPOSITION = currentFINALPIVOTUPPERARM + (dirPivotUpperRIghtToElbowRight * 0.5f);

                dirElbowRightToHand = somePosOfRightHand - elbowPositionRight;

                dirPivotUpperRIghtToElbowRight.Normalize();
                dirElbowRightToHand.Normalize();

                //Vector3 someCross0;
                Vector3.Cross(ref dirPivotUpperRIghtToElbowRight, ref dirElbowRightToHand, out someCross0);
                someCross0.Normalize();

                //Vector3 someCross1;
                Vector3.Cross(ref dirPivotUpperRIghtToElbowRight, ref someCross0, out someCross1);
                someCross1.Normalize();

                //Vector3 upper = someCross1;
                //Vector3 forward = dirPivotUpperRIghtToElbowRight;
                //Vector3 upperWORLD = Vector3.Up;

                shoulderRotationMatrixRight = Matrix.LookAtRH(currentFINALPIVOTUPPERARM, currentFINALPIVOTUPPERARM + someCross0, dirPivotUpperRIghtToElbowRight);
                shoulderRotationMatrixRight.Invert();
                matrixerer = shoulderRotationMatrixRight;

                matrixerer.M41 = currentPositionOfUPPERARMROTATION3DPOSITION.X;// + OFFSETPOS.X;
                matrixerer.M42 = currentPositionOfUPPERARMROTATION3DPOSITION.Y;// + OFFSETPOS.Y;
                matrixerer.M43 = currentPositionOfUPPERARMROTATION3DPOSITION.Z;// + OFFSETPOS.Z;
                matrixerer.M44 = 1;


                //////////////////
                worldMatrix_Terrain_instances[0] = _WorldMatrix;
                _player_lft_upper_arm[_index]._POSITION = matrixerer;
                /*_player_lft_upper_arm[_index].Render(_D3D.device.ImmediateContext);
                _cuber_00 = _player_lft_upper_arm[_index];
                _cuber_00.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_rgt_shldr(_D3D.device.ImmediateContext, _cuber_00.IndexCount, _cuber_00.InstanceCount, _cuber_00._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_torso_BUFFER, oculusRiftDir, _cuber_00);
                _player_lft_upper_arm[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_lft_upper_arm[_index]._POSITION.M41, _player_lft_upper_arm[_index]._POSITION.M42, _player_lft_upper_arm[_index]._POSITION.M43);
                */

                _player_lft_upper_arm[_index].Render(_D3D.device.ImmediateContext);
                _cuber_00 = _player_lft_upper_arm[_index];
                _cuber_00.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_rgt_upper_arm(_D3D.device.ImmediateContext, _cuber_00.IndexCount, _cuber_00.InstanceCount, _cuber_00._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_upper_arm_BUFFER, oculusRiftDir, _cuber_00);
                _player_lft_upper_arm[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_lft_upper_arm[_index]._POSITION.M41, _player_lft_upper_arm[_index]._POSITION.M42, _player_lft_upper_arm[_index]._POSITION.M43);





                /////////////////
                //LEFT LOWER ARM
                _SC_modL_lft_lower_arm_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_lft_lower_arm[_index]._POSITION.M41, _player_lft_lower_arm[_index]._POSITION.M42, _player_lft_lower_arm[_index]._POSITION.M43),
                    padding1 = 100
                };

                somePosererDir = somePosOfRightHand - elbowPositionRight;

                someLowerRightArmPos = elbowPositionRight + (somePosererDir * 0.5f);
                somePosererDir.Normalize();

                //someCross0.Z *= -1;

                Vector3.Cross(ref somePosererDir, ref someCross0, out someCross1);
                someCross1.Normalize();

                theLowerArmRotationMatrix = Matrix.LookAtRH(elbowPositionRight, elbowPositionRight + someCross1, somePosererDir);
                theLowerArmRotationMatrix.Invert();
                matrixerer = theLowerArmRotationMatrix;

                matrixerer.M41 = someLowerRightArmPos.X;// + OFFSETPOS.X;
                matrixerer.M42 = someLowerRightArmPos.Y;// + OFFSETPOS.Y;
                matrixerer.M43 = someLowerRightArmPos.Z;// + OFFSETPOS.Z;
                matrixerer.M44 = 1;

                worldMatrix_Terrain_instances[0] = _WorldMatrix;
                _player_lft_lower_arm[_index]._POSITION = matrixerer;


                _player_lft_lower_arm[_index].Render(_D3D.device.ImmediateContext);
                _cuber_00 = _player_lft_lower_arm[_index];
                _cuber_00.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_rgt_lower_arm(_D3D.device.ImmediateContext, _cuber_00.IndexCount, _cuber_00.InstanceCount, _cuber_00._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_lower_arm_BUFFER, oculusRiftDir, _cuber_00);
                _player_lft_lower_arm[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_lft_lower_arm[_index]._POSITION.M41, _player_lft_lower_arm[_index]._POSITION.M42, _player_lft_lower_arm[_index]._POSITION.M43);



                /*_player_rght_lower_arm[_index].Render(_D3D.device.ImmediateContext);
                _cuber_00 = _player_rght_lower_arm[_index];
                _cuber_00.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_rgt_shldr(_D3D.device.ImmediateContext, _cuber_00.IndexCount, _cuber_00.InstanceCount, _cuber_00._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_torso_BUFFER, oculusRiftDir, _cuber_00);
                _player_rght_lower_arm[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_rght_lower_arm[_index]._POSITION.M41, _player_rght_lower_arm[_index]._POSITION.M42, _player_rght_lower_arm[_index]._POSITION.M43);
                *///_handPositionRight = matrixerer;
                  //_player_rght_lower_arm[_index]._singleObjectOnly._POSITION = matrixerer;





































                //HEAD
                _SC_modL_head_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_head[_index]._POSITION.M41, _player_head[_index]._POSITION.M42, _player_head[_index]._POSITION.M43),
                    padding1 = 100
                };
                _rotatingMatrix = rotatingMatrix;

                MOVINGPOINTER = new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                _rotMatrixer = _player_head[_index]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_head[_index]._total_torso_height * 0.5f));
                _rotatingMatrix = rotatingMatrix;
                //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                //direction_feet_up = _getDirection(Vector3.Up, otherQuat);


                diffNormPosX = (MOVINGPOINTER.X) - _player_head[_index]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_head[_index]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_head[_index]._ORIGINPOSITION.M43;


                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * (diffNormPosZ));

                xq = otherQuat.X;
                yq = otherQuat.Y;
                zq = otherQuat.Z;
                wq = otherQuat.W;

                pitcha = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq); //(float)(180 / Math.PI)
                yawa = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq); //(float)(180 / Math.PI) *
                rolla = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq); // (float)(180 / Math.PI) *

                hyp = (float)(diffNormPosY / Math.Cos(pitcha));

                //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * ((float)hyp));

                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;

                matrixerer = Matrix.Identity;
                _rotatingMatrix = rotatingMatrix;
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

                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;
                matrixerer.M44 = 1;

                //worldMatrix_Terrain_instances[0] = _WorldMatrix;
                //_player_head[_index]._POSITION = matrixerer;

                //_player_head[_index].Render(_D3D.device.ImmediateContext);
                //_player_head[_index].RenderInstancedObject(_D3D.device.ImmediateContext, _player_head[_index].IndexCount, _player_head[_index].InstanceCount, _player_head[_index]._POSITION, viewMatrix, _projectionMatrix, null, worldMatrix_Terrain_instances, _SC_modL_head_BUFFER, oculusRiftDir);
                //_player_head[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_head[_index]._POSITION.M41, _player_head[_index]._POSITION.M42, _player_head[_index]._POSITION.M43);

                _player_head[_index].Render(_D3D.device.ImmediateContext);
                _cuber_00 = _player_head[_index];
                _cuber_00.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_torso(_D3D.device.ImmediateContext, _cuber_00.IndexCount, _cuber_00.InstanceCount, _cuber_00._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_head_BUFFER, oculusRiftDir, _cuber_00);
                _player_head[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_head[_index]._POSITION.M41, _player_head[_index]._POSITION.M42, _player_head[_index]._POSITION.M43);























                //PLAYER PELVIS

                _SC_modL_pelvis_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_pelvis[_index]._POSITION.M41, _player_pelvis[_index]._POSITION.M42, _player_pelvis[_index]._POSITION.M43),
                    padding1 = 100
                };
                _rotatingMatrix = rotatingMatrix;
                _spine_upper_body_pos = new Vector3(_player_pelvis[_index]._ORIGINPOSITION.M41, _player_pelvis[_index]._ORIGINPOSITION.M42, _player_pelvis[_index]._ORIGINPOSITION.M43);

                MOVINGPOINTER = _spine_upper_body_pos;// new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                _rotMatrixer = _player_pelvis[_index]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_pelvis[_index]._total_torso_height * 0.5f));

                //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);
                //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                //direction_feet_up = _getDirection(Vector3.Up, otherQuat);
                //diffNormPosX = (MOVINGPOINTER.X) - _player_pelvis[_index]._ORIGINPOSITION.M41;
                //diffNormPosY = (MOVINGPOINTER.Y) - _player_pelvis[_index]._ORIGINPOSITION.M42;
                //diffNormPosZ = (MOVINGPOINTER.Z) - _player_pelvis[_index]._ORIGINPOSITION.M43;
                //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * (diffNormPosY));
                //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * (diffNormPosZ));
                //xq = otherQuat.X;
                //yq = otherQuat.Y;
                //zq = otherQuat.Z;
                //wq = otherQuat.W;
                //pitcha = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq); //(float)(180 / Math.PI)
                //yawa = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq); //(float)(180 / Math.PI) *
                //rolla = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq); // (float)(180 / Math.PI) *
                //hyp = (float)(diffNormPosY / Math.Cos(pitcha));

                //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * ((float)hyp));

                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;

                matrixerer = Matrix.Identity;

                //rotatingMatrixForPelvis.M41 = MOVINGPOINTER.X;
                //rotatingMatrixForPelvis.M42 = MOVINGPOINTER.Y;
                //rotatingMatrixForPelvis.M43 = MOVINGPOINTER.Z;

                matrixerer.M11 = rotatingMatrixForPelvis.M11;
                matrixerer.M12 = rotatingMatrixForPelvis.M12;
                matrixerer.M13 = rotatingMatrixForPelvis.M13;
                matrixerer.M14 = rotatingMatrixForPelvis.M14;

                matrixerer.M21 = rotatingMatrixForPelvis.M21;
                matrixerer.M22 = rotatingMatrixForPelvis.M22;
                matrixerer.M23 = rotatingMatrixForPelvis.M23;
                matrixerer.M24 = rotatingMatrixForPelvis.M24;

                matrixerer.M31 = rotatingMatrixForPelvis.M31;
                matrixerer.M32 = rotatingMatrixForPelvis.M32;
                matrixerer.M33 = rotatingMatrixForPelvis.M33;
                matrixerer.M34 = rotatingMatrixForPelvis.M34;

                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;
                matrixerer.M44 = 1;


                worldMatrix_Terrain_instances[0] = _WorldMatrix;
                _player_pelvis[_index]._POSITION = matrixerer;
                /*_player_pelvis[_index].Render(_D3D.device.ImmediateContext);
                _cuber_00 = _player_pelvis[_index];
                _cuber_00.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_rgt_shldr(_D3D.device.ImmediateContext, _cuber_00.IndexCount, _cuber_00.InstanceCount, _cuber_00._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_torso_BUFFER, oculusRiftDir, _cuber_00);
                _player_pelvis[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_pelvis[_index]._POSITION.M41, _player_pelvis[_index]._POSITION.M42, _player_pelvis[_index]._POSITION.M43);
                *///_handPositionRight = matrixerer;
                  //_player_pelvis[_index]._singleObjectOnly._POSITION = matrixerer;


                _player_pelvis[_index].Render(_D3D.device.ImmediateContext);
                _cuber_00 = _player_pelvis[_index];
                _cuber_00.Render(_D3D.device.ImmediateContext);
                SC_Console_GRAPHICS._shaderManager._rend_pelvis(_D3D.device.ImmediateContext, _cuber_00.IndexCount, _cuber_00.InstanceCount, _cuber_00._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_pelvis_BUFFER, oculusRiftDir, _cuber_00);
                _player_pelvis[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_pelvis[_index]._POSITION.M41, _player_pelvis[_index]._POSITION.M42, _player_pelvis[_index]._POSITION.M43);



















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


            /*//PHYSICS HEAD
            _player_head[_index]._WORLDMATRIXINSTANCES = worldMatrix_instances_head[_index];
            _player_head[_index]._POSITION = worldMatrix_base[0];

            var cuber = _player_head[_index];
            var instancers = cuber.instances;
            var sometester = cuber._WORLDMATRIXINSTANCES;

            for (int i = 0; i < instancers.Length; i++)
            {
                float xxx = sometester[i].M41;
                float yyy = sometester[i].M42;
                float zzz = sometester[i].M43;

                cuber.instances[i].position.X = xxx;
                cuber.instances[i].position.Y = yyy;
                cuber.instances[i].position.Z = zzz;
                cuber.instances[i].position.W = 1;
                Quaternion _testQuater;
                Quaternion.RotationMatrix(ref sometester[i], out _testQuater);

                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataForward[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Right, _testQuater);
                cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Up, _testQuater);
                cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataUP[i].rotation.W = 1;
            }*/



            //PHYSICS HAND RIGHT
            _player_right_hnd[_index]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_hand[_index];
            _player_right_hnd[_index]._POSITION = worldMatrix_base[0];

            var cuber = _player_right_hnd[_index];
            var instancers = cuber.instances;
            var sometester = cuber._WORLDMATRIXINSTANCES;

            for (int i = 0; i < instancers.Length; i++)
            {
                float xxx = sometester[i].M41;
                float yyy = sometester[i].M42;
                float zzz = sometester[i].M43;

                cuber.instances[i].position.X = xxx;
                cuber.instances[i].position.Y = yyy;
                cuber.instances[i].position.Z = zzz;
                cuber.instances[i].position.W = 1;
                Quaternion _testQuater;
                Quaternion.RotationMatrix(ref sometester[i], out _testQuater);

                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataForward[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Right, _testQuater);
                cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Up, _testQuater);
                cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataUP[i].rotation.W = 1;
            }


            //PHYSICS HAND LEFT
            _player_left_hnd[_index]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_hand[_index];
            _player_left_hnd[_index]._POSITION = worldMatrix_base[0];

            cuber = _player_left_hnd[_index];
            instancers = cuber.instances;
            sometester = cuber._WORLDMATRIXINSTANCES;

            for (int i = 0; i < instancers.Length; i++)
            {
                float xxx = sometester[i].M41;
                float yyy = sometester[i].M42;
                float zzz = sometester[i].M43;

                cuber.instances[i].position.X = xxx;
                cuber.instances[i].position.Y = yyy;
                cuber.instances[i].position.Z = zzz;
                cuber.instances[i].position.W = 1;
                Quaternion _testQuater;
                Quaternion.RotationMatrix(ref sometester[i], out _testQuater);

                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataForward[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Right, _testQuater);
                cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Up, _testQuater);
                cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataUP[i].rotation.W = 1;
            }





            //PHYSICS UPPER ARM RIGHT
            _player_rght_upper_arm[_index]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_upperarm[_index];
            _player_rght_upper_arm[_index]._POSITION = worldMatrix_base[0];

            cuber = _player_rght_upper_arm[_index];
            instancers = cuber.instances;
            sometester = cuber._WORLDMATRIXINSTANCES;

            for (int i = 0; i < instancers.Length; i++)
            {
                float xxx = sometester[i].M41;
                float yyy = sometester[i].M42;
                float zzz = sometester[i].M43;

                cuber.instances[i].position.X = xxx;
                cuber.instances[i].position.Y = yyy;
                cuber.instances[i].position.Z = zzz;
                cuber.instances[i].position.W = 1;
                Quaternion _testQuater;
                Quaternion.RotationMatrix(ref sometester[i], out _testQuater);

                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataForward[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Right, _testQuater);
                cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Up, _testQuater);
                cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataUP[i].rotation.W = 1;
            }



            //PHYSICS UPPER ARM LEFT
            _player_lft_upper_arm[_index]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_upperarm[_index];
            _player_lft_upper_arm[_index]._POSITION = worldMatrix_base[0];

            cuber = _player_lft_upper_arm[_index];
            instancers = cuber.instances;
            sometester = cuber._WORLDMATRIXINSTANCES;

            for (int i = 0; i < instancers.Length; i++)
            {
                float xxx = sometester[i].M41;
                float yyy = sometester[i].M42;
                float zzz = sometester[i].M43;

                cuber.instances[i].position.X = xxx;
                cuber.instances[i].position.Y = yyy;
                cuber.instances[i].position.Z = zzz;
                cuber.instances[i].position.W = 1;
                Quaternion _testQuater;
                Quaternion.RotationMatrix(ref sometester[i], out _testQuater);

                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataForward[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Right, _testQuater);
                cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Up, _testQuater);
                cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataUP[i].rotation.W = 1;
            }






            //PHYSICS LOWER ARM RIGHT
            _player_rght_lower_arm[_index]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_lowerarm[_index];
            _player_rght_lower_arm[_index]._POSITION = worldMatrix_base[0];

            cuber = _player_rght_lower_arm[_index];
            instancers = cuber.instances;
            sometester = cuber._WORLDMATRIXINSTANCES;

            for (int i = 0; i < instancers.Length; i++)
            {
                float xxx = sometester[i].M41;
                float yyy = sometester[i].M42;
                float zzz = sometester[i].M43;

                cuber.instances[i].position.X = xxx;
                cuber.instances[i].position.Y = yyy;
                cuber.instances[i].position.Z = zzz;
                cuber.instances[i].position.W = 1;
                Quaternion _testQuater;
                Quaternion.RotationMatrix(ref sometester[i], out _testQuater);

                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataForward[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Right, _testQuater);
                cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Up, _testQuater);
                cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataUP[i].rotation.W = 1;
            }




            //PHYSICS LOWER ARM LEFT
            _player_lft_lower_arm[_index]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_lowerarm[_index];
            _player_lft_lower_arm[_index]._POSITION = worldMatrix_base[0];

            cuber = _player_lft_lower_arm[_index];
            instancers = cuber.instances;
            sometester = cuber._WORLDMATRIXINSTANCES;

            for (int i = 0; i < instancers.Length; i++)
            {
                float xxx = sometester[i].M41;
                float yyy = sometester[i].M42;
                float zzz = sometester[i].M43;

                cuber.instances[i].position.X = xxx;
                cuber.instances[i].position.Y = yyy;
                cuber.instances[i].position.Z = zzz;
                cuber.instances[i].position.W = 1;
                Quaternion _testQuater;
                Quaternion.RotationMatrix(ref sometester[i], out _testQuater);

                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataForward[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Right, _testQuater);
                cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Up, _testQuater);
                cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataUP[i].rotation.W = 1;
            }






            //PHYSICS PELVIS
            _player_pelvis[_index]._WORLDMATRIXINSTANCES = worldMatrix_instances_pelvis[_index];
            _player_pelvis[_index]._POSITION = worldMatrix_base[0];

            cuber = _player_pelvis[_index];
            instancers = cuber.instances;
            sometester = cuber._WORLDMATRIXINSTANCES;

            for (int i = 0; i < instancers.Length; i++)
            {
                float xxx = sometester[i].M41;
                float yyy = sometester[i].M42;
                float zzz = sometester[i].M43;

                cuber.instances[i].position.X = xxx;
                cuber.instances[i].position.Y = yyy;
                cuber.instances[i].position.Z = zzz;
                cuber.instances[i].position.W = 1;
                Quaternion _testQuater;
                Quaternion.RotationMatrix(ref sometester[i], out _testQuater);

                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataForward[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Right, _testQuater);
                cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Up, _testQuater);
                cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataUP[i].rotation.W = 1;
            }


            //PHYSICS TORSO
            _player_torso[_index]._WORLDMATRIXINSTANCES = worldMatrix_instances_torso[_index];
            _player_torso[_index]._POSITION = worldMatrix_base[0];

            cuber = _player_torso[_index];
            instancers = cuber.instances;
            sometester = cuber._WORLDMATRIXINSTANCES;

            for (int i = 0; i < instancers.Length; i++)
            {
                float xxx = sometester[i].M41;
                float yyy = sometester[i].M42;
                float zzz = sometester[i].M43;

                cuber.instances[i].position.X = xxx;
                cuber.instances[i].position.Y = yyy;
                cuber.instances[i].position.Z = zzz;
                cuber.instances[i].position.W = 1;
                Quaternion _testQuater;
                Quaternion.RotationMatrix(ref sometester[i], out _testQuater);

                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataForward[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Right, _testQuater);
                cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Up, _testQuater);
                cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataUP[i].rotation.W = 1;
            }

            //PHYSICS RIGHT SHOULDER
            _player_right_shldr[_index]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_shoulder[_index];
            _player_right_shldr[_index]._POSITION = worldMatrix_base[0];

            cuber = _player_right_shldr[_index];
            instancers = cuber.instances;
            sometester = cuber._WORLDMATRIXINSTANCES;

            for (int i = 0; i < instancers.Length; i++)
            {
                float xxx = sometester[i].M41;
                float yyy = sometester[i].M42;
                float zzz = sometester[i].M43;

                cuber.instances[i].position.X = xxx;
                cuber.instances[i].position.Y = yyy;
                cuber.instances[i].position.Z = zzz;
                cuber.instances[i].position.W = 1;
                Quaternion _testQuater;
                Quaternion.RotationMatrix(ref sometester[i], out _testQuater);

                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataForward[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Right, _testQuater);
                cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Up, _testQuater);
                cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataUP[i].rotation.W = 1;
            }


            //PHYSICS LEFT SHOULDER
            _player_lft_shldr[_index]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_shoulder[_index];
            _player_lft_shldr[_index]._POSITION = worldMatrix_base[0];

            cuber = _player_lft_shldr[_index];
            instancers = cuber.instances;
            sometester = cuber._WORLDMATRIXINSTANCES;

            for (int i = 0; i < instancers.Length; i++)
            {
                float xxx = sometester[i].M41;
                float yyy = sometester[i].M42;
                float zzz = sometester[i].M43;

                cuber.instances[i].position.X = xxx;
                cuber.instances[i].position.Y = yyy;
                cuber.instances[i].position.Z = zzz;
                cuber.instances[i].position.W = 1;
                Quaternion _testQuater;
                Quaternion.RotationMatrix(ref sometester[i], out _testQuater);

                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataForward[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Right, _testQuater);
                cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = _getDirection(Vector3.Up, _testQuater);
                cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataUP[i].rotation.W = 1;
            }
        }

        public void _update_human_rig_physics(RigidBody body, int _index)// int _index)
        {

            //_World = SC_Console_GRAPHICS._world_list[_index];

            //if (_World != null)
            {
                //_World = (World)tester001[0];

                //if (_World.RigidBodies.Count > 0)
                {
                    count = 0;

                    //var yio = Values.First();
                    //enumerator = _World.RigidBodies.GetEnumerator();

                    int _counter_shoulder_l = 0;
                    int _counter_shoulder_r = 0;

                    int _counter_lowerarm_l = 0;
                    int _counter_lowerarm_r = 0;

                    int _counter_upperarm_l = 0;
                    int _counter_upperarm_r = 0;

                    int _counter_torso = 0;
                    int _counter_pelvis = 0;
                    int _counter_head = 0;


                    int _counter_hand_l = 0;
                    int _counter_hand_r = 0;

                    //_World = world;

                    //while (enumerator.MoveNext())
                    //foreach (RigidBody rigidbody in _World.RigidBodies)
                    {
                        //body = rigidbody;

                        if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerHandRight)
                        {
                            Matrix translationMatrix = Matrix.Identity;
                            Vector3 vec = new Vector3(_player_right_hnd[_index]._POSITION.M41, _player_right_hnd[_index]._POSITION.M42, _player_right_hnd[_index]._POSITION.M43); // new Vector3();

                            _tempMatrix = _player_right_hnd[_index]._POSITION;
                            //_tempMatrix = Matrix.Identity;

                            _tempMatrix.M41 = vec.X;
                            _tempMatrix.M42 = vec.Y;
                            _tempMatrix.M43 = vec.Z;

                            matrixer = new JMatrix();

                            matrixer.M11 = _tempMatrix.M11;
                            matrixer.M12 = _tempMatrix.M12;
                            matrixer.M13 = _tempMatrix.M13;
                            //matrixer.M14 = _tempMatrix.M11;

                            matrixer.M21 = _tempMatrix.M21;
                            matrixer.M22 = _tempMatrix.M22;
                            matrixer.M23 = _tempMatrix.M23;
                            //matrixer.M24 = _tempMatrix.M11;

                            matrixer.M31 = _tempMatrix.M31;
                            matrixer.M32 = _tempMatrix.M32;
                            matrixer.M33 = _tempMatrix.M33;

                            //JQuaternion resultQuat;
                            JQuaternion.CreateFromMatrix(ref matrixer, out resultQuat);
                            resultQuat.Normalize();

                            matrixIn = JMatrix.CreateFromQuaternion(resultQuat);
                            _player_right_hnd[_index]._singleObjectOnly.transform.Component.rigidbody.Orientation = matrixIn;
                            _player_right_hnd[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                            //_player_right_hnd[_index]._POSITION = _tempMatrix;
                            //_player_right_hnd[_index]._singleObjectOnly._POSITION = _tempMatrix;

                            worldMatrix_instances_r_hand[_index][_counter_hand_r] = _tempMatrix;
                            _counter_hand_r++;
                            /*Matrix translationMatrix = Matrix.Identity;
                            Vector3 vec = new Vector3(_player_right_hnd[_index]._POSITION.M41, _player_right_hnd[_index]._POSITION.M42, _player_right_hnd[_index]._POSITION.M43); // new Vector3();

                            _tempMatrix = _player_right_hnd[_index]._POSITION;
                            //_tempMatrix = Matrix.Identity;

                            _tempMatrix.M41 = vec.X;
                            _tempMatrix.M42 = vec.Y;
                            _tempMatrix.M43 = vec.Z;

                            matrixer = new JMatrix();

                            matrixer.M11 = _tempMatrix.M11;
                            matrixer.M12 = _tempMatrix.M12;
                            matrixer.M13 = _tempMatrix.M13;
                            //matrixer.M14 = _tempMatrix.M11;

                            matrixer.M21 = _tempMatrix.M21;
                            matrixer.M22 = _tempMatrix.M22;
                            matrixer.M23 = _tempMatrix.M23;
                            //matrixer.M24 = _tempMatrix.M11;

                            matrixer.M31 = _tempMatrix.M31;
                            matrixer.M32 = _tempMatrix.M32;
                            matrixer.M33 = _tempMatrix.M33;

                            //JQuaternion resultQuat;
                            JQuaternion.CreateFromMatrix(ref matrixer, out resultQuat);
                            resultQuat.Normalize();

                            matrixIn = JMatrix.CreateFromQuaternion(resultQuat);
                            _player_right_hnd[_index]._singleObjectOnly.transform.Component.rigidbody.Orientation = matrixIn;
                            _player_right_hnd[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);



                            //_player_right_hnd._POSITION = _tempMatrix;
                            //_player_right_hnd._singleObjectOnly._POSITION = _tempMatrix;*/
                        }
                        else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerHandLeft)
                        {
                            Matrix translationMatrix = Matrix.Identity;
                            Vector3 vec = new Vector3(_player_left_hnd[_index]._POSITION.M41, _player_left_hnd[_index]._POSITION.M42, _player_left_hnd[_index]._POSITION.M43); // new Vector3();

                            _tempMatrix = _player_left_hnd[_index]._POSITION;
                            //_tempMatrix = Matrix.Identity;

                            _tempMatrix.M41 = vec.X;
                            _tempMatrix.M42 = vec.Y;
                            _tempMatrix.M43 = vec.Z;

                            matrixer = new JMatrix();

                            matrixer.M11 = _tempMatrix.M11;
                            matrixer.M12 = _tempMatrix.M12;
                            matrixer.M13 = _tempMatrix.M13;
                            //matrixer.M14 = _tempMatrix.M11;

                            matrixer.M21 = _tempMatrix.M21;
                            matrixer.M22 = _tempMatrix.M22;
                            matrixer.M23 = _tempMatrix.M23;
                            //matrixer.M24 = _tempMatrix.M11;

                            matrixer.M31 = _tempMatrix.M31;
                            matrixer.M32 = _tempMatrix.M32;
                            matrixer.M33 = _tempMatrix.M33;

                            //JQuaternion resultQuat;
                            JQuaternion.CreateFromMatrix(ref matrixer, out resultQuat);
                            resultQuat.Normalize();

                            matrixIn = JMatrix.CreateFromQuaternion(resultQuat);
                            _player_left_hnd[_index]._singleObjectOnly.transform.Component.rigidbody.Orientation = matrixIn;
                            _player_left_hnd[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                            //_player_left_hnd[_index]._POSITION = _tempMatrix;
                            //_player_left_hnd[_index]._singleObjectOnly._POSITION = _tempMatrix;

                            worldMatrix_instances_l_hand[_index][_counter_hand_l] = _tempMatrix;
                            _counter_hand_l++;
                            /*Matrix translationMatrix = Matrix.Identity;
                            Vector3 vec = new Vector3(_player_left_hnd[_index]._POSITION.M41, _player_left_hnd[_index]._POSITION.M42, _player_left_hnd[_index]._POSITION.M43); // new Vector3();

                            _tempMatrix = _player_left_hnd[_index]._POSITION;
                            //_tempMatrix = Matrix.Identity;

                            _tempMatrix.M41 = vec.X;
                            _tempMatrix.M42 = vec.Y;
                            _tempMatrix.M43 = vec.Z;



                            matrixer = new JMatrix();

                            matrixer.M11 = _tempMatrix.M11;
                            matrixer.M12 = _tempMatrix.M12;
                            matrixer.M13 = _tempMatrix.M13;
                            //matrixer.M14 = _tempMatrix.M11;

                            matrixer.M21 = _tempMatrix.M21;
                            matrixer.M22 = _tempMatrix.M22;
                            matrixer.M23 = _tempMatrix.M23;
                            //matrixer.M24 = _tempMatrix.M11;

                            matrixer.M31 = _tempMatrix.M31;
                            matrixer.M32 = _tempMatrix.M32;
                            matrixer.M33 = _tempMatrix.M33;

                            //JQuaternion resultQuat;
                            JQuaternion.CreateFromMatrix(ref matrixer, out resultQuat);
                            resultQuat.Normalize();

                            matrixIn = JMatrix.CreateFromQuaternion(resultQuat);
                            _player_left_hnd[_index]._singleObjectOnly.transform.Component.rigidbody.Orientation = matrixIn;
                            _player_left_hnd[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                            //_player_left_hnd._POSITION = _tempMatrix;
                            //_player_left_hnd._singleObjectOnly._POSITION = _tempMatrix;*/
                        }
                        else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerTorso)
                        {
                            Matrix translationMatrix = Matrix.Identity;
                            Vector3 vec = new Vector3(_player_torso[_index]._POSITION.M41, _player_torso[_index]._POSITION.M42, _player_torso[_index]._POSITION.M43); // new Vector3();

                            _tempMatrix = _player_torso[_index]._POSITION;
                            //_tempMatrix = Matrix.Identity;

                            _tempMatrix.M41 = vec.X;
                            _tempMatrix.M42 = vec.Y;
                            _tempMatrix.M43 = vec.Z;

                            matrixer = new JMatrix();

                            matrixer.M11 = _tempMatrix.M11;
                            matrixer.M12 = _tempMatrix.M12;
                            matrixer.M13 = _tempMatrix.M13;
                            //matrixer.M14 = _tempMatrix.M11;

                            matrixer.M21 = _tempMatrix.M21;
                            matrixer.M22 = _tempMatrix.M22;
                            matrixer.M23 = _tempMatrix.M23;
                            //matrixer.M24 = _tempMatrix.M11;

                            matrixer.M31 = _tempMatrix.M31;
                            matrixer.M32 = _tempMatrix.M32;
                            matrixer.M33 = _tempMatrix.M33;

                            //JQuaternion resultQuat;
                            JQuaternion.CreateFromMatrix(ref matrixer, out resultQuat);
                            resultQuat.Normalize();

                            matrixIn = JMatrix.CreateFromQuaternion(resultQuat);
                            _player_torso[_index]._singleObjectOnly.transform.Component.rigidbody.Orientation = matrixIn;
                            _player_torso[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                            _player_torso[_index]._POSITION = _tempMatrix;
                            _player_torso[_index]._singleObjectOnly._POSITION = _tempMatrix;

                            worldMatrix_instances_torso[_index][_counter_torso] = _tempMatrix;
                            _counter_torso++;
                        }
                        else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerPelvis)
                        {
                            //Matrix translationMatrix = Matrix.Identity;
                            Vector3 vec = new Vector3(_player_pelvis[_index]._POSITION.M41, _player_pelvis[_index]._POSITION.M42, _player_pelvis[_index]._POSITION.M43); // new Vector3();

                            //vec.Y = _player_pelvis._POSITION.M42;
                            _tempMatrix = _player_pelvis[_index]._POSITION;
                            //_tempMatrix = Matrix.Identity;

                            _tempMatrix.M41 = vec.X;
                            _tempMatrix.M42 = vec.Y;
                            _tempMatrix.M43 = vec.Z;

                            matrixer = new JMatrix();

                            matrixer.M11 = _tempMatrix.M11;
                            matrixer.M12 = _tempMatrix.M12;
                            matrixer.M13 = _tempMatrix.M13;
                            //matrixer.M14 = _tempMatrix.M11;

                            matrixer.M21 = _tempMatrix.M21;
                            matrixer.M22 = _tempMatrix.M22;
                            matrixer.M23 = _tempMatrix.M23;
                            //matrixer.M24 = _tempMatrix.M11;

                            matrixer.M31 = _tempMatrix.M31;
                            matrixer.M32 = _tempMatrix.M32;
                            matrixer.M33 = _tempMatrix.M33;

                            //JQuaternion resultQuat;
                            JQuaternion.CreateFromMatrix(ref matrixer, out resultQuat);
                            resultQuat.Normalize();

                            matrixIn = JMatrix.CreateFromQuaternion(resultQuat);
                            _player_pelvis[_index]._singleObjectOnly.transform.Component.rigidbody.Orientation = matrixIn;

                            _player_pelvis[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                            _player_pelvis[_index]._POSITION = _tempMatrix;
                            _player_pelvis[_index]._singleObjectOnly._POSITION = _tempMatrix;

                            worldMatrix_instances_pelvis[_index][_counter_pelvis] = _tempMatrix;
                            _counter_pelvis++;
                        }
                        else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerShoulderRight)
                        {
                            //Matrix translationMatrix = Matrix.Identity;
                            Vector3 vec = new Vector3(_player_right_shldr[_index]._POSITION.M41, _player_right_shldr[_index]._POSITION.M42, _player_right_shldr[_index]._POSITION.M43); // new Vector3();

                            //vec.Y = _player_pelvis._POSITION.M42;
                            _tempMatrix = _player_right_shldr[_index]._POSITION;
                            //_tempMatrix = Matrix.Identity;

                            _tempMatrix.M41 = vec.X;
                            _tempMatrix.M42 = vec.Y;
                            _tempMatrix.M43 = vec.Z;

                            matrixer = new JMatrix();

                            matrixer.M11 = _tempMatrix.M11;
                            matrixer.M12 = _tempMatrix.M12;
                            matrixer.M13 = _tempMatrix.M13;
                            //matrixer.M14 = _tempMatrix.M11;

                            matrixer.M21 = _tempMatrix.M21;
                            matrixer.M22 = _tempMatrix.M22;
                            matrixer.M23 = _tempMatrix.M23;
                            //matrixer.M24 = _tempMatrix.M11;

                            matrixer.M31 = _tempMatrix.M31;
                            matrixer.M32 = _tempMatrix.M32;
                            matrixer.M33 = _tempMatrix.M33;

                            //JQuaternion resultQuat;
                            JQuaternion.CreateFromMatrix(ref matrixer, out resultQuat);
                            resultQuat.Normalize();

                            matrixIn = JMatrix.CreateFromQuaternion(resultQuat);
                            _player_right_shldr[_index]._singleObjectOnly.transform.Component.rigidbody.Orientation = matrixIn;

                            _player_right_shldr[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                            _player_right_shldr[_index]._POSITION = _tempMatrix;
                            _player_right_shldr[_index]._singleObjectOnly._POSITION = _tempMatrix;
                            worldMatrix_instances_r_shoulder[_index][_counter_shoulder_r] = _tempMatrix;

                            _counter_shoulder_r++;
                        }
                        else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerShoulderLeft)
                        {
                            //Matrix translationMatrix = Matrix.Identity;
                            Vector3 vec = new Vector3(_player_lft_shldr[_index]._POSITION.M41, _player_lft_shldr[_index]._POSITION.M42, _player_lft_shldr[_index]._POSITION.M43); // new Vector3();

                            //vec.Y = _player_pelvis._POSITION.M42;
                            _tempMatrix = _player_lft_shldr[_index]._POSITION;
                            //_tempMatrix = Matrix.Identity;

                            _tempMatrix.M41 = vec.X;
                            _tempMatrix.M42 = vec.Y;
                            _tempMatrix.M43 = vec.Z;

                            matrixer = new JMatrix();

                            matrixer.M11 = _tempMatrix.M11;
                            matrixer.M12 = _tempMatrix.M12;
                            matrixer.M13 = _tempMatrix.M13;
                            //matrixer.M14 = _tempMatrix.M11;

                            matrixer.M21 = _tempMatrix.M21;
                            matrixer.M22 = _tempMatrix.M22;
                            matrixer.M23 = _tempMatrix.M23;
                            //matrixer.M24 = _tempMatrix.M11;

                            matrixer.M31 = _tempMatrix.M31;
                            matrixer.M32 = _tempMatrix.M32;
                            matrixer.M33 = _tempMatrix.M33;

                            //JQuaternion resultQuat;
                            JQuaternion.CreateFromMatrix(ref matrixer, out resultQuat);
                            resultQuat.Normalize();

                            matrixIn = JMatrix.CreateFromQuaternion(resultQuat);
                            _player_lft_shldr[_index]._singleObjectOnly.transform.Component.rigidbody.Orientation = matrixIn;

                            _player_lft_shldr[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                            _player_lft_shldr[_index]._POSITION = _tempMatrix;
                            _player_lft_shldr[_index]._singleObjectOnly._POSITION = _tempMatrix;
                            worldMatrix_instances_l_shoulder[_index][_counter_shoulder_l] = _tempMatrix;
                            _counter_shoulder_l++;
                        }
                        else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerHead)
                        {
                            /* //Matrix translationMatrix = Matrix.Identity;
                             Vector3 vec = new Vector3(_player_head[_index]._POSITION.M41, _player_head[_index]._POSITION.M42, _player_head[_index]._POSITION.M43); // new Vector3();

                             //vec.Y = _player_pelvis._POSITION.M42;
                             _tempMatrix = _player_head[_index]._POSITION;
                             //_tempMatrix = Matrix.Identity;

                             _tempMatrix.M41 = vec.X;
                             _tempMatrix.M42 = vec.Y;
                             _tempMatrix.M43 = vec.Z;

                             matrixer = new JMatrix();

                             matrixer.M11 = _tempMatrix.M11;
                             matrixer.M12 = _tempMatrix.M12;
                             matrixer.M13 = _tempMatrix.M13;
                             //matrixer.M14 = _tempMatrix.M11;

                             matrixer.M21 = _tempMatrix.M21;
                             matrixer.M22 = _tempMatrix.M22;
                             matrixer.M23 = _tempMatrix.M23;
                             //matrixer.M24 = _tempMatrix.M11;

                             matrixer.M31 = _tempMatrix.M31;
                             matrixer.M32 = _tempMatrix.M32;
                             matrixer.M33 = _tempMatrix.M33;

                             //JQuaternion resultQuat;
                             JQuaternion.CreateFromMatrix(ref matrixer, out resultQuat);
                             resultQuat.Normalize();

                             matrixIn = JMatrix.CreateFromQuaternion(resultQuat);
                             _player_head[_index]._singleObjectOnly.transform.Component.rigidbody.Orientation = matrixIn;

                             _player_head[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                             _player_head[_index]._POSITION = _tempMatrix;
                             _player_head[_index]._singleObjectOnly._POSITION = _tempMatrix;*/
                        }
                        else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerRightElbowTarget)
                        {
                            /*//Matrix translationMatrix = Matrix.Identity;
                            Vector3 vec = new Vector3(_player_rght_elbow_target._POSITION.M41, _player_rght_elbow_target._POSITION.M42, _player_rght_elbow_target._POSITION.M43); // new Vector3();

                            //vec.Y = _player_pelvis._POSITION.M42;

                            //_tempMatrix = Matrix.Identity;
                            _tempMatrix = _player_rght_elbow_target._POSITION;

                            _tempMatrix.M41 = vec.X;
                            _tempMatrix.M42 = vec.Y;
                            _tempMatrix.M43 = vec.Z;

                            matrixer = new JMatrix();

                            matrixer.M11 = _tempMatrix.M11;
                            matrixer.M12 = _tempMatrix.M12;
                            matrixer.M13 = _tempMatrix.M13;
                            //matrixer.M14 = _tempMatrix.M11;

                            matrixer.M21 = _tempMatrix.M21;
                            matrixer.M22 = _tempMatrix.M22;
                            matrixer.M23 = _tempMatrix.M23;
                            //matrixer.M24 = _tempMatrix.M11;

                            matrixer.M31 = _tempMatrix.M31;
                            matrixer.M32 = _tempMatrix.M32;
                            matrixer.M33 = _tempMatrix.M33;

                            //JQuaternion resultQuat;
                            JQuaternion.CreateFromMatrix(ref matrixer, out resultQuat);
                            resultQuat.Normalize();

                            matrixIn = JMatrix.CreateFromQuaternion(resultQuat);
                            _player_rght_elbow_target._singleObjectOnly.transform.Component.rigidbody.Orientation = matrixIn;

                            _player_rght_elbow_target._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                            _player_rght_elbow_target._POSITION = _tempMatrix;
                            _player_rght_elbow_target._singleObjectOnly._POSITION = _tempMatrix;*/
                        }
                        else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerLeftElbowTarget)
                        {
                            /*//Matrix translationMatrix = Matrix.Identity;
                            Vector3 vec = new Vector3(_player_lft_elbow_target._POSITION.M41, _player_lft_elbow_target._POSITION.M42, _player_lft_elbow_target._POSITION.M43); // new Vector3();

                            //vec.Y = _player_pelvis._POSITION.M42;
                            _tempMatrix = _player_lft_elbow_target._POSITION;
                            //_tempMatrix = Matrix.Identity;

                            _tempMatrix.M41 = vec.X;
                            _tempMatrix.M42 = vec.Y;
                            _tempMatrix.M43 = vec.Z;

                            matrixer = new JMatrix();

                            matrixer.M11 = _tempMatrix.M11;
                            matrixer.M12 = _tempMatrix.M12;
                            matrixer.M13 = _tempMatrix.M13;
                            //matrixer.M14 = _tempMatrix.M11;

                            matrixer.M21 = _tempMatrix.M21;
                            matrixer.M22 = _tempMatrix.M22;
                            matrixer.M23 = _tempMatrix.M23;
                            //matrixer.M24 = _tempMatrix.M11;

                            matrixer.M31 = _tempMatrix.M31;
                            matrixer.M32 = _tempMatrix.M32;
                            matrixer.M33 = _tempMatrix.M33;

                            //JQuaternion resultQuat;
                            JQuaternion.CreateFromMatrix(ref matrixer, out resultQuat);
                            resultQuat.Normalize();

                            matrixIn = JMatrix.CreateFromQuaternion(resultQuat);
                            _player_lft_elbow_target._singleObjectOnly.transform.Component.rigidbody.Orientation = matrixIn;

                            _player_lft_elbow_target._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                            _player_lft_elbow_target._POSITION = _tempMatrix;
                            _player_lft_elbow_target._singleObjectOnly._POSITION = _tempMatrix;*/
                        }
                        else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerLowerArmRight)
                        {
                            //Matrix translationMatrix = Matrix.Identity;
                            Vector3 vec = new Vector3(_player_rght_lower_arm[_index]._POSITION.M41, _player_rght_lower_arm[_index]._POSITION.M42, _player_rght_lower_arm[_index]._POSITION.M43); // new Vector3();

                            //vec.Y = _player_pelvis._POSITION.M42;
                            _tempMatrix = _player_rght_lower_arm[_index]._POSITION;
                            //_tempMatrix = Matrix.Identity;

                            _tempMatrix.M41 = vec.X;
                            _tempMatrix.M42 = vec.Y;
                            _tempMatrix.M43 = vec.Z;

                            matrixer = new JMatrix();

                            matrixer.M11 = _tempMatrix.M11;
                            matrixer.M12 = _tempMatrix.M12;
                            matrixer.M13 = _tempMatrix.M13;
                            //matrixer.M14 = _tempMatrix.M11;

                            matrixer.M21 = _tempMatrix.M21;
                            matrixer.M22 = _tempMatrix.M22;
                            matrixer.M23 = _tempMatrix.M23;
                            //matrixer.M24 = _tempMatrix.M11;

                            matrixer.M31 = _tempMatrix.M31;
                            matrixer.M32 = _tempMatrix.M32;
                            matrixer.M33 = _tempMatrix.M33;

                            //JQuaternion resultQuat;
                            JQuaternion.CreateFromMatrix(ref matrixer, out resultQuat);
                            resultQuat.Normalize();

                            matrixIn = JMatrix.CreateFromQuaternion(resultQuat);
                            _player_rght_lower_arm[_index]._singleObjectOnly.transform.Component.rigidbody.Orientation = matrixIn;

                            _player_rght_lower_arm[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                            _player_rght_lower_arm[_index]._POSITION = _tempMatrix;
                            _player_rght_lower_arm[_index]._singleObjectOnly._POSITION = _tempMatrix;

                            worldMatrix_instances_r_lowerarm[_index][_counter_lowerarm_r] = _tempMatrix;
                            _counter_lowerarm_r++;
                        }
                        else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerLowerArmLeft)
                        {
                            //Matrix translationMatrix = Matrix.Identity;
                            Vector3 vec = new Vector3(_player_lft_lower_arm[_index]._POSITION.M41, _player_lft_lower_arm[_index]._POSITION.M42, _player_lft_lower_arm[_index]._POSITION.M43); // new Vector3();

                            //vec.Y = _player_pelvis._POSITION.M42;
                            _tempMatrix = _player_lft_lower_arm[_index]._POSITION;
                            //_tempMatrix = Matrix.Identity;

                            _tempMatrix.M41 = vec.X;
                            _tempMatrix.M42 = vec.Y;
                            _tempMatrix.M43 = vec.Z;

                            matrixer = new JMatrix();

                            matrixer.M11 = _tempMatrix.M11;
                            matrixer.M12 = _tempMatrix.M12;
                            matrixer.M13 = _tempMatrix.M13;
                            //matrixer.M14 = _tempMatrix.M11;

                            matrixer.M21 = _tempMatrix.M21;
                            matrixer.M22 = _tempMatrix.M22;
                            matrixer.M23 = _tempMatrix.M23;
                            //matrixer.M24 = _tempMatrix.M11;

                            matrixer.M31 = _tempMatrix.M31;
                            matrixer.M32 = _tempMatrix.M32;
                            matrixer.M33 = _tempMatrix.M33;

                            //JQuaternion resultQuat;
                            JQuaternion.CreateFromMatrix(ref matrixer, out resultQuat);
                            resultQuat.Normalize();

                            matrixIn = JMatrix.CreateFromQuaternion(resultQuat);
                            _player_lft_lower_arm[_index]._singleObjectOnly.transform.Component.rigidbody.Orientation = matrixIn;

                            _player_lft_lower_arm[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                            _player_lft_lower_arm[_index]._POSITION = _tempMatrix;
                            _player_lft_lower_arm[_index]._singleObjectOnly._POSITION = _tempMatrix;

                            worldMatrix_instances_l_lowerarm[_index][_counter_lowerarm_l] = _tempMatrix;
                            _counter_lowerarm_l++;
                        }

                        else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerUpperArmRight)
                        {
                            //Matrix translationMatrix = Matrix.Identity;
                            Vector3 vec = new Vector3(_player_rght_upper_arm[_index]._POSITION.M41, _player_rght_upper_arm[_index]._POSITION.M42, _player_rght_upper_arm[_index]._POSITION.M43); // new Vector3();

                            //vec.Y = _player_pelvis._POSITION.M42;
                            _tempMatrix = _player_rght_upper_arm[_index]._POSITION;
                            //_tempMatrix = Matrix.Identity;

                            _tempMatrix.M41 = vec.X;
                            _tempMatrix.M42 = vec.Y;
                            _tempMatrix.M43 = vec.Z;

                            matrixer = new JMatrix();

                            matrixer.M11 = _tempMatrix.M11;
                            matrixer.M12 = _tempMatrix.M12;
                            matrixer.M13 = _tempMatrix.M13;
                            //matrixer.M14 = _tempMatrix.M11;

                            matrixer.M21 = _tempMatrix.M21;
                            matrixer.M22 = _tempMatrix.M22;
                            matrixer.M23 = _tempMatrix.M23;
                            //matrixer.M24 = _tempMatrix.M11;

                            matrixer.M31 = _tempMatrix.M31;
                            matrixer.M32 = _tempMatrix.M32;
                            matrixer.M33 = _tempMatrix.M33;

                            //JQuaternion resultQuat;
                            JQuaternion.CreateFromMatrix(ref matrixer, out resultQuat);
                            resultQuat.Normalize();

                            matrixIn = JMatrix.CreateFromQuaternion(resultQuat);
                            _player_rght_upper_arm[_index]._singleObjectOnly.transform.Component.rigidbody.Orientation = matrixIn;

                            _player_rght_upper_arm[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                            _player_rght_upper_arm[_index]._POSITION = _tempMatrix;
                            _player_rght_upper_arm[_index]._singleObjectOnly._POSITION = _tempMatrix;


                            worldMatrix_instances_r_upperarm[_index][_counter_upperarm_r] = _tempMatrix;
                            _counter_upperarm_r++;
                        }
                        else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerUpperArmLeft)
                        {
                            //Matrix translationMatrix = Matrix.Identity;
                            Vector3 vec = new Vector3(_player_lft_upper_arm[_index]._POSITION.M41, _player_lft_upper_arm[_index]._POSITION.M42, _player_lft_upper_arm[_index]._POSITION.M43); // new Vector3();

                            //vec.Y = _player_pelvis._POSITION.M42;

                            _tempMatrix = _player_lft_upper_arm[_index]._POSITION;

                            _tempMatrix.M41 = vec.X;
                            _tempMatrix.M42 = vec.Y;
                            _tempMatrix.M43 = vec.Z;

                            matrixer = new JMatrix();

                            matrixer.M11 = _tempMatrix.M11;
                            matrixer.M12 = _tempMatrix.M12;
                            matrixer.M13 = _tempMatrix.M13;
                            //matrixer.M14 = _tempMatrix.M11;

                            matrixer.M21 = _tempMatrix.M21;
                            matrixer.M22 = _tempMatrix.M22;
                            matrixer.M23 = _tempMatrix.M23;
                            //matrixer.M24 = _tempMatrix.M11;

                            matrixer.M31 = _tempMatrix.M31;
                            matrixer.M32 = _tempMatrix.M32;
                            matrixer.M33 = _tempMatrix.M33;

                            //JQuaternion resultQuat;
                            JQuaternion.CreateFromMatrix(ref matrixer, out resultQuat);
                            resultQuat.Normalize();

                            matrixIn = JMatrix.CreateFromQuaternion(resultQuat);
                            _player_lft_upper_arm[_index]._singleObjectOnly.transform.Component.rigidbody.Orientation = matrixIn;

                            _player_lft_upper_arm[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                            _player_lft_upper_arm[_index]._POSITION = _tempMatrix;
                            _player_lft_upper_arm[_index]._singleObjectOnly._POSITION = _tempMatrix;

                            worldMatrix_instances_l_upperarm[_index][_counter_upperarm_l] = _tempMatrix;
                            _counter_upperarm_l++;
                        }
                        /*else if ((SC_Console_DIRECTX.BodyTag)body.Tag == SC_Console_DIRECTX.BodyTag.PlayerLeftElbowTarget)
                        {
                            //Matrix translationMatrix = Matrix.Identity;
                            Vector3 vec = new Vector3(_player_rght_elbow_target_two._POSITION.M41, _player_rght_elbow_target_two._POSITION.M42, _player_rght_elbow_target_two._POSITION.M43); // new Vector3();

                            //vec.Y = _player_pelvis._POSITION.M42;
                            _tempMatrix = _player_rght_elbow_target_two._POSITION;
                            //_tempMatrix = Matrix.Identity;

                            _tempMatrix.M41 = vec.X;
                            _tempMatrix.M42 = vec.Y;
                            _tempMatrix.M43 = vec.Z;

                            matrixer = new JMatrix();

                            matrixer.M11 = _tempMatrix.M11;
                            matrixer.M12 = _tempMatrix.M12;
                            matrixer.M13 = _tempMatrix.M13;
                            //matrixer.M14 = _tempMatrix.M11;

                            matrixer.M21 = _tempMatrix.M21;
                            matrixer.M22 = _tempMatrix.M22;
                            matrixer.M23 = _tempMatrix.M23;
                            //matrixer.M24 = _tempMatrix.M11;

                            matrixer.M31 = _tempMatrix.M31;
                            matrixer.M32 = _tempMatrix.M32;
                            matrixer.M33 = _tempMatrix.M33;

                            //JQuaternion resultQuat;
                            JQuaternion.CreateFromMatrix(ref matrixer, out resultQuat);
                            resultQuat.Normalize();

                            matrixIn = JMatrix.CreateFromQuaternion(resultQuat);
                            _player_rght_elbow_target_two._singleObjectOnly.transform.Component.rigidbody.Orientation = matrixIn;

                            _player_rght_elbow_target_two._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                            _player_rght_elbow_target_two._POSITION = _tempMatrix;
                            _player_rght_elbow_target_two._singleObjectOnly._POSITION = _tempMatrix;
                        }*/

                        /*else if ((SC_Console_DIRECTX.BodyTag)body.Tag == SC_Console_DIRECTX.BodyTag.PlayerLeftElbowTarget)
                        {
                            //Matrix translationMatrix = Matrix.Identity;
                            Vector3 vec = new Vector3(_player_rght_elbow_target_two._POSITION.M41, _player_rght_elbow_target_two._POSITION.M42, _player_rght_elbow_target_two._POSITION.M43); // new Vector3();

                            //vec.Y = _player_pelvis._POSITION.M42;
                            _tempMatrix = _player_rght_elbow_target_two._POSITION;
                            //_tempMatrix = Matrix.Identity;

                            _tempMatrix.M41 = vec.X;
                            _tempMatrix.M42 = vec.Y;
                            _tempMatrix.M43 = vec.Z;

                            matrixer = new JMatrix();

                            matrixer.M11 = _tempMatrix.M11;
                            matrixer.M12 = _tempMatrix.M12;
                            matrixer.M13 = _tempMatrix.M13;
                            //matrixer.M14 = _tempMatrix.M11;

                            matrixer.M21 = _tempMatrix.M21;
                            matrixer.M22 = _tempMatrix.M22;
                            matrixer.M23 = _tempMatrix.M23;
                            //matrixer.M24 = _tempMatrix.M11;

                            matrixer.M31 = _tempMatrix.M31;
                            matrixer.M32 = _tempMatrix.M32;
                            matrixer.M33 = _tempMatrix.M33;

                            //JQuaternion resultQuat;
                            JQuaternion.CreateFromMatrix(ref matrixer, out resultQuat);
                            resultQuat.Normalize();

                            matrixIn = JMatrix.CreateFromQuaternion(resultQuat);
                            _player_rght_elbow_target_two._singleObjectOnly.transform.Component.rigidbody.Orientation = matrixIn;

                            _player_rght_elbow_target_two._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                            _player_rght_elbow_target_two._POSITION = _tempMatrix;
                            _player_rght_elbow_target_two._singleObjectOnly._POSITION = _tempMatrix;
                        }*/






                        /*
                        body = (RigidBody)enumerator.Current;

                        if (body != null && body.Tag != null)
                        {
                            if (body.IsStaticOrInactive == true)
                            {

                                if ((SC_Console_DIRECTX.BodyTag)body.Tag == SC_Console_DIRECTX.BodyTag.PlayerShoulderRight)
                                {
                                    /*Matrix translationMatrix = Matrix.Identity;
                                    Vector3 vec = OFFSETPOS + new Vector3(_player_right_shldr._POSITION.M41, _player_right_shldr._POSITION.M42, _player_right_shldr._POSITION.M43); // new Vector3();

                                    _tempMatrix = Matrix.Identity;

                                    _tempMatrix.M41 = vec.X;
                                    _tempMatrix.M42 = vec.Y;
                                    _tempMatrix.M43 = vec.Z;

                                    _player_right_shldr._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                                    _player_right_shldr._POSITION = _tempMatrix;
                                    _player_right_shldr._singleObjectOnly._POSITION = _tempMatrix;

                                }
                                else if ((SC_Console_DIRECTX.BodyTag)body.Tag == SC_Console_DIRECTX.BodyTag.PlayerShoulderLeft)
                                {
                                    /*Matrix translationMatrix = Matrix.Identity;
                                    Vector3 vec = OFFSETPOS + new Vector3(_player_lft_shldr._POSITION.M41, _player_lft_shldr._POSITION.M42, _player_lft_shldr._POSITION.M43); // new Vector3();

                                    _tempMatrix = Matrix.Identity;

                                    _tempMatrix.M41 = vec.X;
                                    _tempMatrix.M42 = vec.Y;
                                    _tempMatrix.M43 = vec.Z;

                                    _player_lft_shldr._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                                    _player_lft_shldr._POSITION = _tempMatrix;
                                    _player_lft_shldr._singleObjectOnly._POSITION = _tempMatrix;
                                }
                                else if ((SC_Console_DIRECTX.BodyTag)body.Tag == SC_Console_DIRECTX.BodyTag.PlayerTorso)
                                {
                                    /*Matrix translationMatrix = Matrix.Identity;
                                    Vector3 vec = OFFSETPOS + new Vector3(_player_torso._POSITION.M41, _player_torso._POSITION.M42, _player_torso._POSITION.M43); // new Vector3();

                                    _tempMatrix = Matrix.Identity;

                                    _tempMatrix.M41 = vec.X;
                                    _tempMatrix.M42 = vec.Y;
                                    _tempMatrix.M43 = vec.Z;

                                    _player_torso._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                                    _player_torso._POSITION = _tempMatrix;
                                    _player_torso._singleObjectOnly._POSITION = _tempMatrix;
                                }
                                else if ((SC_Console_DIRECTX.BodyTag)body.Tag == SC_Console_DIRECTX.BodyTag.PlayerPelvis)
                                {
                                    Matrix translationMatrix = Matrix.Identity;
                                    Vector3 vec = OFFSETPOS + new Vector3(_player_pelvis._POSITION.M41, _player_pelvis._POSITION.M42, _player_pelvis._POSITION.M43); // new Vector3();

                                    _tempMatrix = Matrix.Identity;

                                    _tempMatrix.M41 = vec.X;
                                    _tempMatrix.M42 = vec.Y;
                                    _tempMatrix.M43 = vec.Z;

                                    _player_pelvis._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                                    _player_pelvis._POSITION = _tempMatrix;
                                    _player_pelvis._singleObjectOnly._POSITION = _tempMatrix;
                                }
                            }
                        }*/

                    }
                }
            }
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
