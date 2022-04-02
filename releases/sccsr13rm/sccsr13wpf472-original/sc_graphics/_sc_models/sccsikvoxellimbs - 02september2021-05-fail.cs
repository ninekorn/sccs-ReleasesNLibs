﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

using scmessageobjectjitter = SCCoreSystems.scmessageobject.scmessageobjectjitter;
using Jitter;

using Ab3d.OculusWrap;

using SC_console_directx = SCCoreSystems.sc_console.SC_console_directx;
using SC_Update = SCCoreSystems.sc_console.SC_Update;
using Jitter.LinearMath;



namespace SCCoreSystems.SC_Graphics
{
    public class sccsikvoxellimbs
    {
        Vector3 somenewtargetlocation = Vector3.Zero;
        Quaternion _testQuater;
        Matrix matrixerer;

        float diffNormPosX;
        float diffNormPosY;
        float diffNormPosZ;
        Vector3 direction_head_forward;
        Vector3 direction_head_right;
        Vector3 direction_head_up;
        Vector3 torsooripos;
        Vector3 tempPoint;
        Vector3 dirToPoint;
        Vector3 realPosOfRS;
        Vector3 pivotOfHead;
        Matrix shoulderMatrix;
        Matrix theheadrotmatrix;

        Vector3 direction_feet_forward_ori;
        Vector3 direction_feet_right_ori;
        Vector3 direction_feet_up_ori;






        public sccsikvoxellimbs parentobject;

        sc_voxel voxel_cuber_r_hand_grab;
        sc_voxel voxel_cuber_r_hnd;
        sc_voxel voxel_cuber_r_up_arm;
        sc_voxel voxel_cuber_r_low_arm;
        sc_voxel voxel_cuber_r_shld;
        sc_voxel voxel_cuber_r_targ;
        sc_voxel voxel_cuber_r_targ_two;
        Matrix[] voxel_sometester_r_hand_grab;
        Matrix[] voxel_sometester_r_hnd;
        Matrix[] voxel_sometester_r_up_arm;
        Matrix[] voxel_sometester_r_low_arm;
        Matrix[] voxel_sometester_r_shld;
        Matrix[] voxel_sometester_r_targ;
        Matrix[] voxel_sometester_r_targ_two;


        sc_voxel.DLightBuffer[] _SC_modL_r_hand_grab_BUFFER = new sc_voxel.DLightBuffer[1];

        sc_voxel.DLightBuffer[] _SC_modL_rght_hnd_BUFFER = new sc_voxel.DLightBuffer[1];


        sc_voxel.DLightBuffer[] _SC_modL_rght_shldr_BUFFER = new sc_voxel.DLightBuffer[1];

        sc_voxel.DLightBuffer[] _SC_modL_rght_elbow_target_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_rght_lower_arm_BUFFER = new sc_voxel.DLightBuffer[1];


        sc_voxel.DLightBuffer[] _SC_modL_rght_upper_arm_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_rght_elbow_target_two_BUFFER = new sc_voxel.DLightBuffer[1];


        Matrix[][][] worldMatrix_instances_r_hand_grab;
        Matrix[][][] worldMatrix_instances_r_elbow_target;
        Matrix[][][] worldMatrix_instances_r_elbow_target_two;
        Matrix[][][] worldMatrix_instances_r_hand;
        Matrix[][][] worldMatrix_instances_r_shoulder;

        Matrix[][][] worldMatrix_instances_r_upperarm;
        Matrix[][][] worldMatrix_instances_r_lowerarm;
        public sc_voxel[][] _player_r_hand_grab;

        public sc_voxel[][] _player_rght_hnd;

        public sc_voxel[][] _player_rght_shldr;
        public sc_voxel[][] _player_rght_elbow_target;
        public sc_voxel[][] _player_rght_lower_arm;
        public sc_voxel[][] _player_rght_upper_arm;
        public sc_voxel[][] _player_rght_elbow_target_two;

        public Matrix worldmatofobj;

        public sccsikvoxellimbs()
        {



        }

        object _some_data0;
        World _thejitter_world;

        //lightpos = new Vector3(0, 100, 0);
        Vector4 ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
        Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
        Vector3 lightDirection = new Vector3(0, -1, -1);
        Vector3 dirLight = Vector3.Zero;
        Vector3 lightpos = Vector3.Zero;


        public Vector3 initchunkposition;



        Matrix[] voxel_sometester_head;
        sc_voxel voxel_cuber_head;
        sc_voxel voxel_cuber_pelvis;
        sc_voxel voxel_cuber_torso;
        Matrix[] voxel_sometester_pelvis;
        Matrix[] voxel_sometester_torso;
        sc_voxel.DLightBuffer[] _SC_modL_pelvis_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_head_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_torso_BUFFER = new sc_voxel.DLightBuffer[1];
        Matrix[][][] worldMatrix_instances_head;
        Matrix[][][] worldMatrix_instances_torso;
        Matrix[][][] worldMatrix_instances_pelvis;


        public sc_voxel[][] _player_head;
        public sc_voxel[][] _player_pelvis;
        public sc_voxel[][] _player_torso;




        int humaninstupperrigx;
        int humaninstupperrigy;
        int humaninstupperrigz;
        int somechunkpriminstanceikarmvoxelindex;

        public scmessageobjectjitter[][] createikbody(scmessageobjectjitter[][] _sc_jitter_tasks, int tempMultiInstancePhysicsTotal, Vector3 ikarmpivotinitposition, sccsikvoxellimbs parentobject_, Matrix worldmatofobj_, int somechunkpriminstanceikarmvoxelindex_, int humaninstupperrigx_, int humaninstupperrigy_, int humaninstupperrigz_)
        {

            somechunkpriminstanceikarmvoxelindex = somechunkpriminstanceikarmvoxelindex_;

            //head // 0
            //pelvis // 1
            //torso // 2
            //pivot arm (shoulder) // 3
            //lower arm // 4
            //upper arm // 5
            //pivot leg // 6
            //lower leg // 7
            //upper leg // 8
            int voxelbodyparttype = -1;


            humaninstupperrigx = humaninstupperrigx_;
            humaninstupperrigy = humaninstupperrigy_;
            humaninstupperrigz = humaninstupperrigz_;



            worldmatofobj = worldmatofobj_;


            if (parentobject_ != null)
            {
                parentobject = parentobject_;
            }

            float voxel_general_size = 0.0025f;

            initchunkposition = ikarmpivotinitposition;

            if (Program.usejitterphysics == 1)
            {
                //SETTING UP SINGLE WORLD OBJECTS
                //END OF LOOP FOR PHYSICS ENGINE INSTANCES
                _some_data0 = (object)_sc_jitter_tasks[0][0]._world_data[0];
                //World[] _jitter_worlds0 = (World[])_some_data0;
                _thejitter_world = (World)_some_data0;
            }




            int _inst_p_torso_x = humaninstupperrigx;
            int _inst_p_torso_y = humaninstupperrigy;
            int _inst_p_torso_z = humaninstupperrigz;
            int _inst_p_pelvis_x = humaninstupperrigx;
            int _inst_p_pelvis_y = humaninstupperrigy;
            int _inst_p_pelvis_z = humaninstupperrigz;

            Matrix WorldMatrix = Matrix.Identity;
            float _dist_between = 1;
            int tempIndex = 0;
            bool is_static = true;
            int _addToWorld = 0;
            int voxel_type = 2;

            _player_torso = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_pelvis = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_head = new sc_voxel[tempMultiInstancePhysicsTotal][];


            worldMatrix_instances_head = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_torso = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_pelvis = new Matrix[tempMultiInstancePhysicsTotal][][];



            _player_pelvis[0] = new sc_voxel[1];
            worldMatrix_instances_pelvis[0] = new Matrix[1][];



            _player_torso[0] = new sc_voxel[1];
            worldMatrix_instances_torso[0] = new Matrix[1][];


            _player_head[0] = new sc_voxel[1];
            worldMatrix_instances_head[0] = new Matrix[1][];









            Quaternion otherQuat;






            //THE HEAD IS AT ZERO
            /////////////////
            ///////HEAD//////
            /////////////////
            float vertoffsetx = 0;
            float vertoffsety = 0;
            float vertoffsetz = 0;
            float r = 0.19f;
            float g = 0.19f;
            float b = 0.19f;
            float a = 1;
            Matrix tempmatrix = Matrix.Identity;
            tempmatrix = WorldMatrix;
            tempmatrix.M41 = 0;// SC_Update.viewpositionorigin.X;
            tempmatrix.M42 = 0 + initchunkposition.Y; // 0 + SC_Update.viewpositionorigin.Y
            tempmatrix.M43 = 0; //SC_Update.viewpositionorigin.Z
            tempmatrix.M44 = 1;
            float offsetPosX = _dist_between * 2;
            float offsetPosY = _dist_between * 2;
            float offsetPosZ = _dist_between * 2;
            float _mass = 100;
            int _type_of_cube = 2;
            voxelbodyparttype = 0;
            _player_head[0][0] = new sc_voxel();
            _player_head[0][0].Initialize(
                SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight,
                0, 1, 1, 1, 0.05f, 0.05f, 0.05f,
                new Vector4(r, g, b, a),
                _inst_p_torso_x, _inst_p_torso_y, _inst_p_torso_z,
                Program.consoleHandle,
                tempmatrix,
                _type_of_cube,
                offsetPosX, offsetPosY, offsetPosZ,
                _thejitter_world,
                _mass,
                is_static,
                SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerHead,
                //9, 9, 9, 9, 9, 15, 35, 20, 20, 40, 23, 22, BACKUP
                9, 11, 9, 11, 9, 11, 11, 9, 11, 30, 29, 30, 29, 30, 29,
                voxel_general_size,
                new Vector3(0, 0, 0),
                23,
                vertoffsetx, vertoffsety, vertoffsetz,
                _addToWorld,
                voxel_type, voxelbodyparttype, somechunkpriminstanceikarmvoxelindex); //, "terrainGrassDirt.bmp" //0.00035f

            worldMatrix_instances_head[0][0] = new Matrix[_inst_p_torso_x * _inst_p_torso_y * _inst_p_torso_z];




            for (int i = 0; i < worldMatrix_instances_head[0][0].Length; i++)
            {
                //var movingpoint = + (direction_feet_up_torso * heightOftorso * 0.5f);
                var movingpoint = new Vector3(_player_head[0][0]._arrayOfInstances[0].current_pos.M41, _player_head[0][0]._arrayOfInstances[0].current_pos.M42, _player_head[0][0]._arrayOfInstances[0].current_pos.M43);

                Matrix worldmatrixhead = _player_head[0][0]._arrayOfInstances[0].current_pos; //Matrix.Identity;

                Quaternion.RotationMatrix(ref worldmatrixhead, out otherQuat);
                var direction_feet_forward_head = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right_head = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_feet_up_head = sc_maths._getDirection(Vector3.Up, otherQuat);

                var widthofhead = _player_head[0][0]._total_torso_width * 1.0f;
                var heightofhead = _player_head[0][0]._total_torso_height * 1.0f;
                var depthofhead = _player_head[0][0]._total_torso_depth * 1.0f;

                Matrix worldmatrixtorso = _player_head[0][0]._arrayOfInstances[i].current_pos; //Matrix.Identity;
                Quaternion.RotationMatrix(ref worldmatrixtorso, out otherQuat);
                var direction_feet_forward_torso = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right_torso = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_feet_up_torso = sc_maths._getDirection(Vector3.Up, otherQuat);

                movingpoint = movingpoint + (-direction_feet_up_head * heightofhead);
                //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso);
                movingpoint = movingpoint + (-direction_feet_up_torso * _player_head[0][0]._total_torso_height);
                //movingpoint = movingpoint + (-direction_feet_up_torso * _player_pelvis[0][0]._total_torso_height);
                //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso * 0.5f);
                //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso * 0.5f);
                //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso * 0.5f);

                //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso * 0.5f);
                //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso * 0.5f);
                /*tempmatrix = _player_torso[0][0]._arrayOfInstances[i]._ORIGINPOSITION;// _player_torso[0][0]._ORIGINPOSITION;
                /*tempmatrix.M41 = movingpoint.X;
                tempmatrix.M42 = movingpoint.Y;
                tempmatrix.M43 = movingpoint.Z;*/

                //_player_torso[0][0]._ORIGINPOSITION = tempmatrix;




                var tempmatrix1 = _player_head[0][0]._arrayOfInstances[i].current_pos; //Matrix.Identity; //_player_pelvis[0][0]._arrayOfInstances[i].current_pos;

                tempmatrix1.M41 = _player_head[0][0]._arrayOfInstances[i].current_pos.M41;
                tempmatrix1.M42 = _player_head[0][0]._arrayOfInstances[i].current_pos.M42;
                tempmatrix1.M43 = _player_head[0][0]._arrayOfInstances[i].current_pos.M43;

                tempmatrix1.M41 += movingpoint.X;
                tempmatrix1.M42 += movingpoint.Y;
                tempmatrix1.M43 += movingpoint.Z;

                tempmatrix1.M44 = 1.0f;

                //_player_torso[0][0]._arrayOfInstances[i].current_pos = tempmatrix1;
                //_player_torso[0][0]._arrayOfInstances[0]

                worldMatrix_instances_head[0][0][i] = _player_head[0][0]._arrayOfInstances[i]._ORIGINPOSITION; // Matrix.Identity;

                /*var hmdposx = SC_Update.viewpositionorigin.X;
                var hmdposy = SC_Update.viewpositionorigin.Y;
                var hmdposz = SC_Update.viewpositionorigin.Z;

                //var movingpoint = +(direction_feet_up_torso * heightOftorso * 0.5f);
                var movingpoint = new Vector3(_player_head[0][0]._arrayOfInstances[i].current_pos.M41, _player_head[0][0]._arrayOfInstances[i].current_pos.M42, _player_head[0][0]._arrayOfInstances[i].current_pos.M43);

                Matrix worldmatrixhead = _player_head[0][0]._arrayOfInstances[i].current_pos; //Matrix.Identity;

                Quaternion.RotationMatrix(ref worldmatrixhead, out otherQuat);
                var direction_feet_forward_head = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right_head = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_feet_up_head = sc_maths._getDirection(Vector3.Up, otherQuat);
                */

                /*
                var widthofhead = _player_head[0][0]._total_torso_width * 1.0f;
                var heightofhead = _player_head[0][0]._total_torso_height * 1.0f;
                var depthofhead = _player_head[0][0]._total_torso_depth * 1.0f;


                Matrix worldmatrixtorso = _player_head[0][0]._arrayOfInstances[i].current_pos; //Matrix.Identity;
                Quaternion.RotationMatrix(ref worldmatrixtorso, out otherQuat);
                var direction_feet_forward_torso = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right_torso = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_feet_up_torso = sc_maths._getDirection(Vector3.Up, otherQuat);




                movingpoint = movingpoint + (-direction_feet_up_head * heightofhead);
                //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso);
                movingpoint = movingpoint + (-direction_feet_up_torso * _player_head[0][0]._total_torso_height);*/
                //movingpoint = movingpoint + (-direction_feet_up_torso * _player_pelvis[0][0]._total_torso_height);
                //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso * 0.5f);
                //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso * 0.5f);
                //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso * 0.5f);

                //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso * 0.5f);
                //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso * 0.5f);
                /*tempmatrix = _player_torso[0][0]._arrayOfInstances[i]._ORIGINPOSITION;// _player_torso[0][0]._ORIGINPOSITION;
                /*tempmatrix.M41 = movingpoint.X;
                tempmatrix.M42 = movingpoint.Y;
                tempmatrix.M43 = movingpoint.Z;*/

                //_player_torso[0][0]._ORIGINPOSITION = tempmatrix;



                /*
                var tempmatrix1 = _player_head[0][0]._arrayOfInstances[i].current_pos; //Matrix.Identity; //_player_pelvis[0][0]._arrayOfInstances[i].current_pos;

                tempmatrix1.M41 = _player_head[0][0]._arrayOfInstances[i].current_pos.M41;
                tempmatrix1.M42 = _player_head[0][0]._arrayOfInstances[i].current_pos.M42;
                tempmatrix1.M43 = _player_head[0][0]._arrayOfInstances[i].current_pos.M43;
                */
                //tempmatrix1.M41 += hmdposx;
                //tempmatrix1.M42 += hmdposy;
                //tempmatrix1.M43 += hmdposz;

                /*tempmatrix1.M41 = SC_Update.viewpositionorigin.X;
                tempmatrix1.M42 = SC_Update.viewpositionorigin.Y;
                tempmatrix1.M43 = SC_Update.viewpositionorigin.Z;

                tempmatrix1.M41 += _player_head[0][0]._arrayOfInstances[i].current_pos.M41;
                tempmatrix1.M42 += _player_head[0][0]._arrayOfInstances[i].current_pos.M42;
                tempmatrix1.M43 += _player_head[0][0]._arrayOfInstances[i].current_pos.M43;
                */

                //tempmatrix1.M44 = 1.0f;
                //I'VE FINALLY FOUND WHERE THE FREE multiplication MESH IS COMING FROM. COMMENT OUT THE PART HERE AND SOMEHOW THE MESH IS MULTIPLIED BY 2 OR SOMETHING ELSE IS GOING ON. NOT SURE ABOUT THE STABILITY OF THE RENDERING OF THOSE THOUGH. WILL TEST LATER. SCREENRECORDING OF 01ST SEPTEMBER 2021. IT DOESNT SEEM TO BE THE ONLY THING. WHEN SETTING THE MATRIX TO THE ARRAYOFINSTANCES[I].CURRENTPOS INSTEAD OF MATRIX.IDENTITY, THAT'S WHERE I GET FREE MESH OR SOMETHING. WHY, THE NUMBER OF INSTANCES IN ARRAYOFINSTANCES IS THE SAME NORMALLY AS THE ARRAY OF WORLDMATRIXES....

                //_player_torso[0][0]._arrayOfInstances[i].current_pos = tempmatrix1;
                //_player_torso[0][0]._arrayOfInstances[0]

                /*worldMatrix_instances_head[0][0][i] = Matrix.Identity;// _player_head[0][0]._arrayOfInstances[i].current_pos;// tempmatrix1;// _player_torso[0][0]._arrayOfInstances[i].current_pos;// Matrix.Identity;

                worldMatrix_instances_head[0][0][i].M41 = SC_Update.viewpositionorigin.X;// _player_head[0][0]._arrayOfInstances[i].current_pos.M41;
                worldMatrix_instances_head[0][0][i].M42 = SC_Update.viewpositionorigin.Y;//_player_head[0][0]._arrayOfInstances[i].current_pos.M42;
                worldMatrix_instances_head[0][0][i].M43 = SC_Update.viewpositionorigin.Z;//_player_head[0][0]._arrayOfInstances[i].current_pos.M43;
                */
                //worldMatrix_instances_head[0][0][i] =  _player_head[0][0]._arrayOfInstances[i].current_pos;

            }
            /////////////////
            ///////HEAD//////
            /////////////////

            //chest looking like voxel settings 11, 12, 11, 12, 11, 14, 9, 9, 9, 35, 33, 35, 33, 35, 33,
            //same top japanese looking like voxel settings 11, 12, 11, 12, 11, 14, 9, 9, 9, 35, 33, 35, 33, 35, 33,
            //Elite Dangerous stations voxel type settings 9, 9, 9, 9, 9, 9, 35, 34, 40, 59, 20, 19, 
            //9, 9, 9, 9, 9, 9, 35, 34, 40, 59, 35, 34, 
            //RIDDICK 1 kinda monster head . id have to recheck the movie. 7, 9, 7, 30, 9, 40, 30, 9, 40, 30, 30, 30, 30, 30, 30,





            //Matrix worldmatrixhead = _player_head[0][0]._arrayOfInstances[0]._ORIGINPOSITION;
            //Quaternion otherQuat;


            tempIndex = 0;

            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = -(16 + 15) * 0.015f;// - 0.25f;;

            _dist_between = 0.30f;

            ///////////////////////////////
            ///////////HUMAN RIG///////////
            ///////////////////////////////

            ////////////////////////////////////////////////
            //////////CONTAINMENT GRIDS RIGHT HAND//////////
            ////////////////////////////////////////////////

            /*
            _cubeGridFaceTop = new DObjectGrid();
            _cubeGridFaceTop.Initialize(Device, 4, 4, 0.01f, 0, 0, 0, false, true, false, false, false, false);

            _cubeGridFaceBottom = new DObjectGrid();
            _cubeGridFaceBottom.Initialize(Device, 4, 4, 0.01f, 0, 0, 0, true, false, false, false, false, false);


            _cubeGridFaceLeft = new DObjectGrid();
            _cubeGridFaceLeft.Initialize(Device, 4, 4, 0.01f, 0, 0, 0, false, false, true, false, false, false);

            _cubeGridFaceRight = new DObjectGrid();
            _cubeGridFaceRight.Initialize(Device, 4, 4, 0.01f, 0, 0, 0, false, false, false, true, false, false);

            _cubeGridFaceFront = new DObjectGrid();
            _cubeGridFaceFront.Initialize(Device, 4, 4, 0.01f, 0, 0, 0, false, false, false, false, true, false);

            _cubeGridFaceBack = new DObjectGrid();
            _cubeGridFaceBack.Initialize(Device, 4, 4, 0.01f, 0, 0, 0, false, false, false, false, false, true);*/


            //var movingpoint = +(direction_feet_up_torso * heightOftorso * 0.5f);
            //var movingpoint = new Vector3(_player_head[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M41, _player_head[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42, _player_head[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M43);
            //movingpoint = movingpoint + (direction_feet_up_head * heightofhead * 0.5f);
            //movingpoint = movingpoint + (direction_feet_up_torso * heightOftorso * 0.5f);




            //TORSO
            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;
            r = 0.19f;
            g = 0.19f;
            b = 0.19f;
            a = 1;
            tempmatrix = Matrix.Identity;
            tempmatrix = WorldMatrix;

            tempmatrix.M41 = 0 + 0;
            tempmatrix.M42 = 0 + initchunkposition.Y; // -0.1f ///0;//
            tempmatrix.M43 = 0;
            tempmatrix.M44 = 1;

            offsetPosX = 0;//_dist_between * 2;
            offsetPosY = 0;//_dist_between * 2;
            offsetPosZ = 0;//_dist_between * 2;

            //_player_torso[0] = new sc_voxel();
            //_hasinit0 = _player_torso.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, tempmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f                                                                                                                                                                                                                                                                                        //_hasinit0 = _player_torso.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, tempmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_player_torso[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), _inst_p_torso_x, _inst_p_torso_y, _inst_p_torso_z, Hwnd, tempmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerTorso, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            //voxel_general_size = 0.0025f;
            //voxel_type = 0;

            _type_of_cube = 2;
            _mass = 100;
            voxelbodyparttype = 2;

            _player_torso[0][0] = new sc_voxel();

            _player_torso[0][0].Initialize(
                SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight,
                0, 1, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a),
                _inst_p_torso_x, _inst_p_torso_y, _inst_p_torso_z,
                Program.consoleHandle, tempmatrix, _type_of_cube,
                offsetPosX, offsetPosY, offsetPosZ,
                _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerTorso,
                9, 9, 9, 30, 9, 40, 30, 9, 40, 45, 44, 57, 56, 17, 16, voxel_general_size,
                new Vector3(0, 0, 0), 43,

                vertoffsetx, vertoffsety, vertoffsetz,
                _addToWorld, voxel_type, voxelbodyparttype, somechunkpriminstanceikarmvoxelindex); //, "terrainGrassDirt.bmp" //0.00035f



            //Matrix worldmatrixtorso = _player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION;
            //Quaternion otherQuat;
            //Quaternion.RotationMatrix(ref worldmatrixtorso, out otherQuat);
            //var direction_feet_forward_torso = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
            //var direction_feet_right_torso = sc_maths._getDirection(Vector3.Right, otherQuat);
            //var direction_feet_up_torso = sc_maths._getDirection(Vector3.Up, otherQuat);
            /*
            Matrix worldmatrixpelvis = _player_pelvis[0][0]._arrayOfInstances[0]._ORIGINPOSITION;
            //Quaternion otherQuat;
            Quaternion.RotationMatrix(ref worldmatrixpelvis, out otherQuat);
            var direction_feet_forward_pelvis = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
            var direction_feet_right_pelvis = sc_maths._getDirection(Vector3.Right, otherQuat);
            var direction_feet_up_pelvis = sc_maths._getDirection(Vector3.Up, otherQuat);*/

            var depthoftorso = _player_torso[0][0]._total_torso_depth * 1.0f;
            var heightOftorso = _player_torso[0][0]._total_torso_height * 1.0f;
            var widthoftorso = _player_torso[0][0]._total_torso_width * 1.0f;
            /*
            var depthofpelvis = _player_pelvis[0][0]._total_torso_depth * 1.0f;
            var widthofpelvis = _player_pelvis[0][0]._total_torso_width * 1.0f;
            var heightofpelvis = _player_pelvis[0][0]._total_torso_height * 1.0f;*/




            //_player_torso[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), _inst_p_torso_x, _inst_p_torso_y, _inst_p_torso_z, Hwnd, tempmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerTorso, 2, 9, 2, 2, 2, 2, 45, 44, 60, 59, 10, 9, 0.0025f, new Vector3(0, 0, 0), 500); //, "terrainGrassDirt.bmp" //0.00035f
            worldMatrix_instances_torso[0][0] = new Matrix[_inst_p_torso_x * _inst_p_torso_y * _inst_p_torso_z];

            for (int i = 0; i < worldMatrix_instances_torso[0][0].Length; i++)
            {
                //var movingpoint = +(direction_feet_up_torso * heightOftorso * 0.5f);
                var movingpoint = new Vector3(_player_head[0][0]._arrayOfInstances[i].current_pos.M41, _player_head[0][0]._arrayOfInstances[i].current_pos.M42, _player_head[0][0]._arrayOfInstances[i].current_pos.M43);

                Matrix worldmatrixhead = _player_head[0][0]._arrayOfInstances[i].current_pos; //Matrix.Identity;

                Quaternion.RotationMatrix(ref worldmatrixhead, out otherQuat);
                var direction_feet_forward_head = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right_head = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_feet_up_head = sc_maths._getDirection(Vector3.Up, otherQuat);

                var widthofhead = _player_head[0][0]._total_torso_width * 1.0f;
                var heightofhead = _player_head[0][0]._total_torso_height * 1.0f;
                var depthofhead = _player_head[0][0]._total_torso_depth * 1.0f;

                Matrix worldmatrixtorso = _player_torso[0][0]._arrayOfInstances[i].current_pos; //Matrix.Identity;
                Quaternion.RotationMatrix(ref worldmatrixtorso, out otherQuat);
                var direction_feet_forward_torso = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right_torso = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_feet_up_torso = sc_maths._getDirection(Vector3.Up, otherQuat);

                movingpoint = movingpoint + (-direction_feet_up_head * heightofhead);
                movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso);

                var tempmatrix1 = _player_torso[0][0]._arrayOfInstances[i].current_pos; //Matrix.Identity; //_player_pelvis[0][0]._arrayOfInstances[i].current_pos;

                tempmatrix1.M41 = _player_torso[0][0]._arrayOfInstances[i].current_pos.M41;
                tempmatrix1.M42 = _player_torso[0][0]._arrayOfInstances[i].current_pos.M42;
                tempmatrix1.M43 = _player_torso[0][0]._arrayOfInstances[i].current_pos.M43;

                tempmatrix1.M41 += movingpoint.X;
                tempmatrix1.M42 += movingpoint.Y;
                tempmatrix1.M43 += movingpoint.Z;

                tempmatrix1.M44 = 1.0f;

                worldMatrix_instances_torso[0][0][i] = tempmatrix1;
            }





























            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;
            //PELVIS
            r = 0.19f;
            g = 0.19f;
            b = 0.19f;
            a = 1;
            tempmatrix = Matrix.Identity;
            tempmatrix = WorldMatrix;
            tempmatrix.M41 = 0;
            tempmatrix.M42 = 0 + initchunkposition.Y; // 0
            tempmatrix.M43 = 0;
            tempmatrix.M44 = 1;
            offsetPosX = 0;//_dist_between * 2;
            offsetPosY = 0;//_dist_between * 2;
            offsetPosZ = 0;// _dist_between * 2;
            _mass = 100;
            //_player_pelvis[0] = new sc_voxel();
            //_hasinit0 = _player_pelvis.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, tempmatrix, 9, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_pelvis[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, tempmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerPelvis, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            //float voxel_general_size = 0.0025f;
            //voxel_type = 0;
            _type_of_cube = 2;
            _player_pelvis[0][0] = new sc_voxel();
            voxelbodyparttype = 1;
            //_player_pelvis[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, tempmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerPelvis, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
            _player_pelvis[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight,
                0, 1, 1, 1, 0.125f, 0.05f, 0.065f,
                new Vector4(r, g, b, a), _inst_p_pelvis_x, _inst_p_pelvis_y, _inst_p_pelvis_z,
                Program.consoleHandle, tempmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerPelvis,
                9, 9, 9, 20, 9, 9, 9, 9, 9, 40, 39, 25, 24, 20, 19,
                voxel_general_size, new Vector3(0, 0, 0), 24,
                vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type, voxelbodyparttype, somechunkpriminstanceikarmvoxelindex); //, "terrainGrassDirt.bmp" //0.00035f
            worldMatrix_instances_pelvis[0][0] = new Matrix[_inst_p_pelvis_x * _inst_p_pelvis_y * _inst_p_pelvis_z];

            for (int i = 0; i < worldMatrix_instances_pelvis[0][0].Length; i++)
            {
                //var movingpoint = +(direction_feet_up_torso * heightOftorso * 0.5f);
                var movingpoint = new Vector3(_player_head[0][0]._arrayOfInstances[i].current_pos.M41, _player_head[0][0]._arrayOfInstances[i].current_pos.M42, _player_head[0][0]._arrayOfInstances[i].current_pos.M43);

                Matrix worldmatrixhead = _player_head[0][0]._arrayOfInstances[i].current_pos; //Matrix.Identity;

                Quaternion.RotationMatrix(ref worldmatrixhead, out otherQuat);
                var direction_feet_forward_head = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right_head = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_feet_up_head = sc_maths._getDirection(Vector3.Up, otherQuat);



                var widthofhead = _player_head[0][0]._total_torso_width * 1.0f;
                var heightofhead = _player_head[0][0]._total_torso_height * 1.0f;
                var depthofhead = _player_head[0][0]._total_torso_depth * 1.0f;


                Matrix worldmatrixtorso = _player_torso[0][0]._arrayOfInstances[i].current_pos; //Matrix.Identity;
                Quaternion.RotationMatrix(ref worldmatrixtorso, out otherQuat);
                var direction_feet_forward_torso = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right_torso = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_feet_up_torso = sc_maths._getDirection(Vector3.Up, otherQuat);


                movingpoint = movingpoint + (-direction_feet_up_head * heightofhead);
                movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso);
                movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso);
                movingpoint = movingpoint + (-direction_feet_up_torso * _player_pelvis[0][0]._total_torso_height);
                //movingpoint = movingpoint + (-direction_feet_up_torso * _player_pelvis[0][0]._total_torso_height);
                //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso * 0.5f);
                //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso * 0.5f);
                //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso * 0.5f);

                //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso * 0.5f);
                //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso * 0.5f);
                /*tempmatrix = _player_torso[0][0]._arrayOfInstances[i]._ORIGINPOSITION;// _player_torso[0][0]._ORIGINPOSITION;
                /*tempmatrix.M41 = movingpoint.X;
                tempmatrix.M42 = movingpoint.Y;
                tempmatrix.M43 = movingpoint.Z;*/

                //_player_torso[0][0]._ORIGINPOSITION = tempmatrix;




                var tempmatrix1 = _player_pelvis[0][0]._arrayOfInstances[i].current_pos; //Matrix.Identity; //_player_pelvis[0][0]._arrayOfInstances[i].current_pos;

                tempmatrix1.M41 = _player_pelvis[0][0]._arrayOfInstances[i].current_pos.M41;
                tempmatrix1.M42 = _player_pelvis[0][0]._arrayOfInstances[i].current_pos.M42;
                tempmatrix1.M43 = _player_pelvis[0][0]._arrayOfInstances[i].current_pos.M43;

                tempmatrix1.M41 += movingpoint.X;
                tempmatrix1.M42 += movingpoint.Y;
                tempmatrix1.M43 += movingpoint.Z;

                tempmatrix1.M44 = 1.0f;

                //_player_torso[0][0]._arrayOfInstances[i].current_pos = tempmatrix1;
                //_player_torso[0][0]._arrayOfInstances[0]

                worldMatrix_instances_pelvis[0][0][i] = tempmatrix1;// _player_torso[0][0]._arrayOfInstances[i].current_pos;// Matrix.Identity;
            }

















            if (Program.usejitterphysics == 1)
            {


                for (int phys = 0; phys < Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z; phys++)
                {
                    for (int i = 0; i < Program.world_width * Program.world_height * Program.world_depth; i++)
                    {
                        object _some_dator = (object)_sc_jitter_tasks[phys][i]._world_data[0];
                        World _the_current_world = (World)_some_dator;


                        /*for (int rb = 0; rb < _the_current_world.AddBody(_player_pelvis[0][0]._arrayOfInstances.Length)
                        {

                        }*/

                        //_the_current_world.AddBody(_player_rght_upper_arm[0][0]._arrayOfInstances[_iterator].transform.Component.rigidbody);
                        //_the_current_world.AddBody(_player_rght_lower_arm[0][0]._arrayOfInstances[_iterator].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_pelvis[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_torso[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        //_the_current_world.AddBody(_player_rght_hnd[0][0]._arrayOfInstances[_iterator].transform.Component.rigidbody);
                        //_the_current_world.AddBody(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].transform.Component.rigidbody);
                    }
                }
            }






            _SC_modL_head_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };
            _SC_modL_torso_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };
            _SC_modL_pelvis_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };
















            return _sc_jitter_tasks;
        }


        public scmessageobjectjitter[][] setikbodytargetnlimbspositionsNrotations(scmessageobjectjitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix hmdrotMatrix, Matrix hmd_matrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft, Matrix oriProjectionMatrix, sc_voxel mainarmparentmeshobject, Vector3 lightpos_, Vector3 dirLight_, Matrix finalRotationMatrix_, sc_voxel mainarmparentmeshobjectmain, Matrix hmd_matrix_current, Matrix extramatrix, Matrix hmdmatrixcurrentforpelvis)
        {





            //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
            //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
            //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
            Matrix sometempmat2 = _player_head[0][0]._arrayOfInstances[0].current_pos;
            Quaternion somedirquat2;
            Quaternion.RotationMatrix(ref sometempmat2, out somedirquat2);
            var dirikvoxelbodyInstanceRight2 = -sc_maths._newgetdirleft(somedirquat2);
            var dirikvoxelbodyInstanceUp2 = sc_maths._newgetdirup(somedirquat2);
            var dirikvoxelbodyInstanceForward2 = sc_maths._newgetdirforward(somedirquat2);


            Vector3 tempOffset = OFFSETPOS;

            //int usethirdpersonview = 1;

            if (Program.usethirdpersonview == 0)
            {
                tempOffset.X = SC_Update.viewpositionorigin.X;
                //tempOffset.Y = _player_head[0][0]._arrayOfInstances[0]._TEMPPIVOT.M42;
                tempOffset.Y = SC_Update.viewpositionorigin.Y;
                tempOffset.Z = SC_Update.viewpositionorigin.Z;

                //tempOffset.Y = tempOffset.Y - (dirikvoxelbodyInstanceUp2.Y * _player_head[0][0]._arrayOfInstances[0]._TEMPPIVOT.M42 * 0.75f);



                //(_player_torso[0][0].ChunkHeight_L + _player_torso[0][0].ChunkHeight_R + 1) +
                //tempOffset.Y = tempOffset.Y + (_player_head[0][0]._arrayOfInstances[0]._TEMPPIVOT.M42 * 0.75f);
                //tempOffset = tempOffset + (dirikvoxelbodyInstanceUp2 * (_player_head[0][0]._arrayOfInstances[0]._TEMPPIVOT.M42 * 0.75f));
                //tempOffset = tempOffset + (dirikvoxelbodyInstanceUp2 * (((_player_head[0][0].ChunkHeight_L + _player_head[0][0].ChunkHeight_R + 1)) * _player_head[0][0]._sizeY * (_player_head[0][0].planesize) * 4 * 10));


                //tempOffset.Y = tempOffset.Y + ikvoxelbody._player_head[0][0]._arrayOfInstances[0]._TEMPPIVOT.M42;
                //tempOffset = tempOffset + (dirikvoxelbodyInstanceUp2 * (ikvoxelbody._player_head[0][0]._total_torso_height * 0.5f));

                //tempOffset.Y = tempOffset.Y + _player_head[0][0]._arrayOfInstances[0]._TEMPPIVOT.M42;
                //tempOffset = tempOffset + (dirikvoxelbodyInstanceUp2 * (_player_head[0][0]._total_torso_height * 0.5f*10));


                //tempOffset.Y = tempOffset.Y + _player_head[0][0]._arrayOfInstances[0]._TEMPPIVOT.M42;
                //tempOffset = tempOffset + (dirikvoxelbodyInstanceUp2 * (_player_head[0][0]._total_torso_height * 0.5f * (_player_head[0][0].ChunkHeight_L + _player_head[0][0].ChunkHeight_R + 1) * 4 * (voxel_general_size * 10 * 4)));
                //tempOffset = tempOffset + (dirikvoxelbodyInstanceUp2 * (((_player_head[0][0].ChunkWidth_L + _player_head[0][0].ChunkWidth_R + 1 + _player_head[0][0].ChunkWidth_R + 1)) * _player_head[0][0]._sizeX * (_player_head[0][0].planesize)* 10 ));



                //_total_torso_width = (((ChunkWidth_L + ChunkWidth_R + 1) * _sizeX * planesize * 4));
            }
            else if (Program.usethirdpersonview == 1)
            {

                //OFFSETPOS.X = SC_Update.viewPosition.X;
                //OFFSETPOS.Y = SC_Update.viewPosition.Y;
                //OFFSETPOS.Z = SC_Update.viewPosition.Z;

                //OFFSETPOS = OFFSETPOS + (dirikvoxelbodyInstanceUp0 * -0.125f);
                //SC_Update.viewPosition = SC_Update.viewPosition + (dirikvoxelbodyInstanceRight0 * -1.5f);





                /*//tempmatter = hmd_matrix * rotatingMatrixForPelvis * hmdmatrixRot;
                Quaternion quatt;
                Quaternion.RotationMatrix(ref SC_Update.tempmatter, out quatt);
                // quatt.Invert();

                //THIRD PERSON VR VIEW. COMMENT THIS PART OUT TO HAVE FIRST PERSON VIEW
                Vector3 forwardOVR = sc_maths._getDirection(Vector3.ForwardRH, quatt);
                Vector3 upOVR = sc_maths._getDirection(Vector3.Up, quatt);
                Vector3 rightOVR = sc_maths._getDirection(Vector3.Right, quatt);
                upOVR.Normalize();
                rightOVR.Normalize();
                forwardOVR.Normalize();

                forwardOVR *= -0.5f; // -1.0f

                Vector3 thirdpersonview = OFFSETPOS + (-forwardOVR * 2.0f); //1.5f // + (upOVR * 0.25f)

                OFFSETPOS.X = thirdpersonview.X;// SC_Update.viewPosition.X;
                OFFSETPOS.Y = thirdpersonview.Y;// SC_Update.viewPosition.Y;
                OFFSETPOS.Z = thirdpersonview.Z;// SC_Update.viewPosition.Z;*/
            }



            //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
            //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
            //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW












            _SC_modL_pelvis_BUFFER[0].lightPosition = lightpos;
            _SC_modL_pelvis_BUFFER[0].lightDirection = dirLight;

            _SC_modL_torso_BUFFER[0].lightPosition = lightpos;
            _SC_modL_torso_BUFFER[0].lightDirection = dirLight;

            _SC_modL_head_BUFFER[0].lightPosition = lightpos;
            _SC_modL_head_BUFFER[0].lightDirection = dirLight;



            Matrix finalRotationMatrix = finalRotationMatrix_;// originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdrotMatrix;

            //finalRotationMatrix = hmd_matrix_current * finalRotationMatrix;

            ////////////////////
            /////HUMAN RIG////// 
            ////////////////////
            for (int _iterator = 0; _iterator < _player_torso[0][0]._arrayOfInstances.Length; _iterator++) //
            {
                var connectorOfUpperArmRightOffsetMul = 1.0f; //1.55f
                var connectorOfLowerArmRightOffsetMul = 1.0f; //0.70f
                var connectorOfHandOffsetMul = 1.00123f; // 1.00123f

                var connectorOfUpperLegOffsetMul = 1.0f;
                var connectorOfLowerLegOffsetMul = 1.0f;

                //THE CURRENT PIVOT POINT OF THE TORSO IS RIGHT IN THE MIDDLE OF THE TORSO ITSELF
                Vector3 MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                //SAVING IN MEMORY THE ORIGINAL TORSO MATRIX NOT AFFECTED BY CURRENT POSITION AND ROTATION CHANGES.
                Matrix _rotMatrixer = _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION;// _player_torso[0][0]._ORIGINPOSITION;
                Quaternion forTest;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                //FROM THE MATRIX OF ROTATION/POSITION, I GET THE QUATERNION OUT OF THAT AND CREATE THE DIRECTIONS THAT THE OBJECTS ARE ORIGINALLY FACING.
                var direction_feet_forward_ori_torso = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                var direction_feet_right_ori_torso = sc_maths._getDirection(Vector3.Right, forTest);
                var direction_feet_up_ori_torso = sc_maths._getDirection(Vector3.Up, forTest);

                //SINCE THE PIVOT POINT IS CURRENTLY IN THE MIDDLE OF THE TORSO, IT CANNOT ROTATE AT THAT POINT OTHERWISE, IT WONT FOLLOW THE PELVIS ROTATION LATER ON.
                //SO WE CURRENTLY ONLY OFFSET THE TORSO "MIDDLE OF SPINE APPROX" TO HALF OF THE CURRENT HEIGHT IN ORDER TO MAKE THE PIVOT POINT, APPROX WHERE THE PELVIS IS.
                Vector3 TORSOPIVOT = MOVINGPOINTER + -(direction_feet_up_ori_torso * (_player_torso[0][0]._total_torso_height * 0.5f));

                _rotMatrixer = _player_torso[0][0]._arrayOfInstances[_iterator].current_pos;// _player_torso[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                var direction_upper_torso = sc_maths._getDirection(Vector3.Up, forTest);

                //Vector3 NECKPIVOTORIGINAL = MOVINGPOINTER + (direction_upper_torso * (_player_torso[0][0]._total_torso_height * 0.5f));
                //NECKPIVOTORIGINAL = NECKPIVOTORIGINAL + (direction_upper_torso * (_player_head[0][0]._total_torso_height * 0.5f));
                //Vector3 NECKPIVOTORIGINALWITHROTATIONOFFSET = NECKPIVOTORIGINAL;

                //I AM USING THE SAME POINT THAT WAS DECLARED EARLIER TO SHRINK THE NUMBER OF VARIABLES CONTAINED IN THE SCRIPT, EVEN THOUGH THIS IS CURRENTLY ONLY A DRAFT PROJEKT.
                //I AM STARTING THE POSITION OF ALL OF THE FOLLOWING TRANSLATION TO BE ADDED TO THIS ONE. THIS IS SO MUCH EASIER TO COMPREHEND FOR ME THEN USING QUATERNIONS FOR OTHER THINGS.
                //I DO NOT HAVE THE ABILITY YET TO UNDERSTAND WHAT THE X AND Y AND Z AND W OF A QUATERNION IS UNLESS CONVERTED TO THE PITCH/YAW/ROLL OR CARTESIAN/POLAR COORDINATES.
                MOVINGPOINTER = new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                //I FOR SOME REASONS IS SAVING THE ORIGINAL TORSO POSITION INSIDE OF THAT MATRIX AGAIN. FOR SOME REASONS, I THOUGHT "REF" MEANT "FOR THE CURRENT VARIABLE TO ALSO BE MODIFIED AFTER THE FUNCTION HAS RUN".
                //DEFECT IN MY LEARNING PROCESS.
                //_rotMatrixer = _player_torso._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                //CALCULATED IT TWICE... NO NEED FOR THAT.

                //REMOVED THAT TOO... WTF AND WHY AM I REMOVING THE TOTAL HEIGHT OF THE TORSO INSTEAD OF JUST HALF IS.ive got no clue. removing it.
                //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori_torso * (_player_torso._total_torso_height * 0.5f));

                //GETTING THE CURRENT ROTATION MATRIX OF THE PIVOT BOTTOM OF SPINE AREA.
                Quaternion otherQuat;
                Quaternion.RotationMatrix(ref finalRotationMatrix, out otherQuat);

                //CONVERTING THE QUATERNION OF THAT TO THE DIRECTION OF ITS ROTATION
                var direction_feet_forward_torso = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right_torso = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_feet_up_torso = sc_maths._getDirection(Vector3.Up, otherQuat);

                //I AM CALCULATING THE DIFFERENCE IN THE MOVEMENT FROM THE ORIGINAL POSITION TO THE CURRENT OFFSET AT THE BOTTOM OF THE SPINE WHERE I MOVED THAT POINT.
                var diffNormPosX = (MOVINGPOINTER.X) - _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41; //_player_torso[0][0]._ORIGINPOSITION.M41;
                var diffNormPosY = (MOVINGPOINTER.Y) - _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42; //_player_torso[0][0]._ORIGINPOSITION.M42;
                var diffNormPosZ = (MOVINGPOINTER.Z) - _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43; //_player_torso[0][0]._ORIGINPOSITION.M43;

                /*//I AM USING THE NEW PIVOT POINT AT THE BOTTOM OF THE SPINE AND ADDING THE FRONT/RIGHT/UP VECTOR OF THE ROTATION OF THAT SPINE AND THEN ADDING THE DIFFERENCE X/W/Z BETWEEN ORIGINAL POS AND THE NEW PIVOT POS
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right_torso * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_torso * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward_torso * (diffNormPosZ));
                */



                var heightofhead = _player_head[0][0]._total_torso_height * 1.0f;
                var heightOftorso = _player_torso[0][0]._total_torso_height * 1.0f;
                var heightofpelvis = _player_pelvis[0][0]._total_torso_height * 1.0f;

                var widthofhead = _player_head[0][0]._total_torso_width * 1.0f;
                var widthoftorso = _player_torso[0][0]._total_torso_width * 1.0f;
                var widthofpelvis = _player_pelvis[0][0]._total_torso_width * 1.0f;

                var depthofhead = _player_head[0][0]._total_torso_depth * 1.0f;
                var depthoftorso = _player_torso[0][0]._total_torso_depth * 1.0f;
                var depthofpelvis = _player_pelvis[0][0]._total_torso_depth * 1.0f;


                MOVINGPOINTER = new Vector3(_player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42 - (heightOftorso * 2), _player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                //MOVINGPOINTER = new Vector3(_player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42 - (-direction_feet_up_torso.Y * (heightOftorso * 0.5f)) - (-direction_feet_up_torso.Y * (heightofhead * 0.5f)), _player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                //- (heightofhead * 0.5f) - (heightOftorso * 0.5f)

                //MOVINGPOINTER.Y = MOVINGPOINTER.Y + (-direction_feet_up_torso.Y * (heightofhead * 0.5f));
                //MOVINGPOINTER.Y = MOVINGPOINTER.Y + (-direction_feet_up_torso.Y * (heightOftorso * 0.5f));
                //MOVINGPOINTER.Y = MOVINGPOINTER.Y + (-direction_feet_up_torso.Y * (heightofpelvis * 0.5f));

                MOVINGPOINTER = MOVINGPOINTER + (direction_feet_up_torso * (heightofhead));
                MOVINGPOINTER = MOVINGPOINTER + (direction_feet_up_torso * (heightOftorso));












                //HEAD STUFF
                //HEAD STUFF
                //HEAD STUFF
                Matrix tempTorsoMat = finalRotationMatrix;// _player_torso[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref tempTorsoMat, out forTest);
                var direction_up_torso = sc_maths._getDirection(Vector3.Up, forTest);
                var direction_right_torso = sc_maths._getDirection(Vector3.Right, forTest);
                var headPoint = MOVINGPOINTER + (-direction_feet_up_torso * (diffNormPosY));// direction_up_torso * (_player_torso[0][0]._total_torso_height * 0.5f);
                var oriHeadPivot = headPoint;
                oriHeadPivot += OFFSETPOS;

                headPoint = headPoint + (direction_feet_up_torso * (_player_head[0][0]._total_torso_height * 4));
                headPoint = headPoint + (direction_feet_up_torso * 0.01f);
                headPoint += OFFSETPOS;
                //HEAD STUFF
                //HEAD STUFF
                //HEAD STUFF


                //MOVINGPOINTER.X += OFFSETPOS.X;
                //MOVINGPOINTER.Y += OFFSETPOS.Y;
                //MOVINGPOINTER.Z += OFFSETPOS.Z;

                var matrixerer = finalRotationMatrix;


                //MOVINGPOINTER += tempOffset;

                if (Program.usethirdpersonview == 0)
                {
                    MOVINGPOINTER += OFFSETPOS;
                    //MOVINGPOINTER.Y = tempOffset.Y;

                }
                else if (Program.usethirdpersonview == 1)
                {
                    MOVINGPOINTER += tempOffset;
                }




                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;
                matrixerer.M44 = 1;


                Matrix _body_pos = matrixerer;
                Quaternion _quat;
                Quaternion.RotationMatrix(ref _body_pos, out _quat);

                JQuaternion _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                var matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                worldMatrix_instances_torso[0][0][_iterator] = matrixerer;// _player_pelvis[0][0].current_pos;// translationMatrix;
                _player_torso[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;

                _player_torso[0][0]._arrayOfInstances[_iterator].forwarddirection = direction_feet_forward_torso;
                _player_torso[0][0]._arrayOfInstances[_iterator].updirection = direction_feet_up_torso;
                _player_torso[0][0]._arrayOfInstances[_iterator].rightdirection = direction_feet_right_torso;

















                ///////////
                //SOMETESTS
                Quaternion.RotationMatrix(ref finalRotationMatrix, out otherQuat);
                var direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                Vector3 current_rotation_of_torso_pivot_forward = direction_feet_forward;
                Vector3 current_rotation_of_torso_pivot_right = direction_feet_right;
                Vector3 current_rotation_of_torso_pivot_up = direction_feet_up;
                //SOMETESTS
                ///////////





                //HEAD
                var finalHMDMatrix = hmd_matrix_current * finalRotationMatrix;
                Quaternion.RotationMatrix(ref hmd_matrix_current, out otherQuat);
                var direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                _SC_modL_head_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_head[0][0]._POSITION.M41, _player_head[0][0]._POSITION.M42, _player_head[0][0]._POSITION.M43),
                    padding1 = 100
                };
                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                var torsooripos = MOVINGPOINTER;
                _rotMatrixer = _player_head[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                var direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                var direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                var direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                diffNormPosX = (MOVINGPOINTER.X) - _player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                var tempPoint = MOVINGPOINTER;
                tempPoint = tempPoint + -(direction_feet_right * (diffNormPosX));
                tempPoint = tempPoint + -(direction_feet_up * (diffNormPosY));
                tempPoint = tempPoint + -(direction_feet_forward * (diffNormPosZ));
                var dirToPoint = tempPoint - torsooripos;
                dirToPoint.Normalize();
                var realPosOfHead = TORSOPIVOT + (dirToPoint * ((_player_torso[0][0]._total_torso_height + (_player_head[0][0]._total_torso_height * 4))));
                var pivotOfHead = TORSOPIVOT + (dirToPoint * ((_player_torso[0][0]._total_torso_height)));
                //realPosOfHead.X += OFFSETPOS.X;
                //realPosOfHead.Y += OFFSETPOS.Y;
                //realPosOfHead.Z += OFFSETPOS.Z;

                finalHMDMatrix = hmd_matrix_current * finalRotationMatrix;
                Quaternion.RotationMatrix(ref finalHMDMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                var theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right, direction_head_up);
                theheadrotmatrix.Invert();
                matrixerer = theheadrotmatrix;







                Vector3 somepivotheadoffset = pivotOfHead;// + OFFSETPOS;




                matrixerer.M41 = somepivotheadoffset.X;
                matrixerer.M42 = somepivotheadoffset.Y;
                matrixerer.M43 = somepivotheadoffset.Z;
                _player_head[0][0]._arrayOfInstances[_iterator]._TEMPPIVOT = matrixerer;


                //_player_head[0][0]._arrayOfInstances[_iterator]._TEMPPIVOT.M41 = realPosOfHead.X;
                //_player_head[0][0]._arrayOfInstances[_iterator]._TEMPPIVOT.M42 = realPosOfHead.Y;
                //_player_head[0][0]._arrayOfInstances[_iterator]._TEMPPIVOT.M43 = realPosOfHead.Z;


                realPosOfHead = realPosOfHead + (direction_head_up * (_player_head[0][0]._total_torso_depth * 4));
                realPosOfHead = realPosOfHead + (-current_rotation_of_torso_pivot_up * (_player_head[0][0]._total_torso_depth * 4));



                if (Program.usethirdpersonview == 0)
                {

                    //realPosOfHead = OFFSETPOS;
                    realPosOfHead = tempOffset;
                    //realPosOfHead.X += tempOffset.X;
                    //realPosOfHead.Y += tempOffset.Y;
                    //realPosOfHead.Z += tempOffset.Z;
                    realPosOfHead.Y = OFFSETPOS.Y;
                    realPosOfHead.X += OFFSETPOS.X;
                    realPosOfHead.Z += OFFSETPOS.Z;

                }
                else if (Program.usethirdpersonview == 1)
                {
                    realPosOfHead += tempOffset;
                }




                matrixerer.M41 = realPosOfHead.X;
                matrixerer.M42 = realPosOfHead.Y;
                matrixerer.M43 = realPosOfHead.Z;
                worldMatrix_instances_head[0][0][_iterator] = matrixerer;
                _player_head[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;




























                /*_SC_modL_pelvis_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_pelvis[0][0]._POSITION.M41, _player_pelvis[0][0]._POSITION.M42, _player_pelvis[0][0]._POSITION.M43),
                    padding1 = 100
                };
                //var _cuber_pelvis = _player_pelvis[0][0];
                var _spine_upper_body_pos = new Vector3(_player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                /*var MOVINGPOINTERPELVIS = _spine_upper_body_pos;

                MOVINGPOINTERPELVIS.X += OFFSETPOS.X;
                MOVINGPOINTERPELVIS.Y += OFFSETPOS.Y;
                MOVINGPOINTERPELVIS.Z += OFFSETPOS.Z;
                
                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                torsooripos = MOVINGPOINTER;
                _rotMatrixer = _player_pelvis[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                diffNormPosX = (MOVINGPOINTER.X) - _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                tempPoint = MOVINGPOINTER;
                tempPoint = tempPoint + -(direction_feet_right * (diffNormPosX));
                tempPoint = tempPoint + -(direction_feet_up * (diffNormPosY));
                tempPoint = tempPoint + -(direction_feet_forward * (diffNormPosZ));



                //finalRotationMatrix = originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdrotMatrix;

                //matrixerer = finalRotationMatrix;
                // originRot * rotatingMatrixForPelvis * hmdrotMatrix;              
                //matrixerer = finalRotationMatrix;


                dirToPoint = tempPoint - torsooripos;
                dirToPoint.Normalize();
                var realPosOfpelvis = TORSOPIVOT + (dirToPoint * ((_player_torso[0][0]._total_torso_height + (_player_pelvis[0][0]._total_torso_height * 2))));
                var pivotOfpelvis = TORSOPIVOT + (dirToPoint * ((_player_torso[0][0]._total_torso_height)));
                realPosOfpelvis.X += OFFSETPOS.X;
                realPosOfpelvis.Y += OFFSETPOS.Y;
                realPosOfpelvis.Z += OFFSETPOS.Z;

                finalHMDMatrix = rotatingMatrixForPelvis; //finalRotationMatrix;

                Quaternion.RotationMatrix(ref finalHMDMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                theheadrotmatrix = Matrix.LookAtRH(pivotOfpelvis, pivotOfpelvis + direction_head_forward, direction_head_up);
                theheadrotmatrix.Invert();
                matrixerer = theheadrotmatrix;

                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;

                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;

                matrixerer.M44 = 1;

                _body_pos = matrixerer;
                Quaternion.RotationMatrix(ref _body_pos, out _quat);
                _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                matrixIn = JMatrix.CreateFromQuaternion(_other_quat);
                worldMatrix_instances_pelvis[0][0][_iterator] = matrixerer;
                _player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;*/





                /*
                finalHMDMatrix = finalRotationMatrix;
                Quaternion.RotationMatrix(ref finalHMDMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                _SC_modL_pelvis_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_pelvis[0][0]._POSITION.M41, _player_pelvis[0][0]._POSITION.M42, _player_pelvis[0][0]._POSITION.M43),
                    padding1 = 100
                };
                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                torsooripos = MOVINGPOINTER;
                _rotMatrixer = _player_pelvis[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                diffNormPosX = (MOVINGPOINTER.X) - _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                tempPoint = MOVINGPOINTER;
                tempPoint = tempPoint + -(direction_feet_right * (diffNormPosX));
                tempPoint = tempPoint + -(direction_feet_up * (diffNormPosY));
                tempPoint = tempPoint + -(direction_feet_forward * (diffNormPosZ));
                dirToPoint = tempPoint - torsooripos;
                dirToPoint.Normalize();
                var realPosOfpelvis = TORSOPIVOT + (dirToPoint * ((_player_torso[0][0]._total_torso_height + (_player_pelvis[0][0]._total_torso_height * 2))));
                var pivotOfpelvis = TORSOPIVOT + (dirToPoint * ((_player_torso[0][0]._total_torso_height)));
                realPosOfpelvis.X += OFFSETPOS.X;
                realPosOfpelvis.Y += OFFSETPOS.Y;
                realPosOfpelvis.Z += OFFSETPOS.Z;
                finalHMDMatrix = finalRotationMatrix;
                Quaternion.RotationMatrix(ref finalHMDMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                theheadrotmatrix = Matrix.LookAtRH(pivotOfpelvis, pivotOfpelvis + direction_head_forward, direction_head_up);
                theheadrotmatrix.Invert();
                matrixerer = theheadrotmatrix;

                realPosOfpelvis = realPosOfpelvis + (direction_head_up * (_player_pelvis[0][0]._total_torso_depth * 2));
                //realPosOfpelvis = realPosOfpelvis + (-current_rotation_of_torso_pivot_up * (_player_pelvis[0][0]._total_torso_depth * 2));

                pivotOfpelvis.X += OFFSETPOS.X;
                pivotOfpelvis.Y += OFFSETPOS.Y;
                pivotOfpelvis.Z += OFFSETPOS.Z;



                matrixerer.M41 = pivotOfpelvis.X;
                matrixerer.M42 = pivotOfpelvis.Y;
                matrixerer.M43 = pivotOfpelvis.Z;
                worldMatrix_instances_pelvis[0][0][_iterator] = matrixerer;
                _player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
                */













                finalHMDMatrix = originRot * rotatingMatrixForPelvis * hmdrotMatrix;//finalRotationMatrix = 



                Quaternion.RotationMatrix(ref finalHMDMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                _SC_modL_pelvis_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_pelvis[0][0]._POSITION.M41, _player_pelvis[0][0]._POSITION.M42, _player_pelvis[0][0]._POSITION.M43),
                    padding1 = 100
                };
                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                torsooripos = MOVINGPOINTER;
                _rotMatrixer = _player_pelvis[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                diffNormPosX = (MOVINGPOINTER.X) - _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                tempPoint = MOVINGPOINTER;
                tempPoint = tempPoint + -(direction_feet_right * (diffNormPosX));
                tempPoint = tempPoint + -(direction_feet_up * (diffNormPosY));
                tempPoint = tempPoint + -(direction_feet_forward * (diffNormPosZ));


                dirToPoint = tempPoint - torsooripos;
                dirToPoint.Normalize();
                var realPosOfpelvis = TORSOPIVOT + (dirToPoint * ((_player_torso[0][0]._total_torso_height + (_player_pelvis[0][0]._total_torso_height * 2))));

                //MOVINGPOINTER = new Vector3(_player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                MOVINGPOINTER = new Vector3(_player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42 - (direction_feet_up_torso.Y * (heightOftorso * 2)) - (direction_feet_up_torso.Y * (heightofpelvis * 2)), _player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                var pivotOfpelvis = MOVINGPOINTER; // + (dirToPoint * ((_player_torso[0][0]._total_torso_height)));
                realPosOfpelvis.X += OFFSETPOS.X;
                realPosOfpelvis.Y += OFFSETPOS.Y;
                realPosOfpelvis.Z += OFFSETPOS.Z;

                //finalHMDMatrix = hmdmatrixcurrentforpelvis * finalRotationMatrix;// originRot * rotatingMatrixForPelvis * hmdrotMatrix; ;// finalRotationMatrix; //rotatingMatrix

                finalHMDMatrix = extramatrix * hmdrotMatrix;


                Quaternion.RotationMatrix(ref finalHMDMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                theheadrotmatrix = Matrix.LookAtRH(pivotOfpelvis, pivotOfpelvis + direction_head_forward, direction_head_up);
                theheadrotmatrix.Invert();


                matrixerer = theheadrotmatrix;
                //matrixerer = hmdmatrixcurrentforpelvis * matrixerer;




                realPosOfpelvis = realPosOfpelvis + (direction_head_up * (_player_pelvis[0][0]._total_torso_depth * 2));
                realPosOfpelvis = realPosOfpelvis + (current_rotation_of_torso_pivot_up * (_player_pelvis[0][0]._total_torso_depth * 2));

                //pivotOfpelvis.X += OFFSETPOS.X;
                //pivotOfpelvis.Y += OFFSETPOS.Y;
                //pivotOfpelvis.Z += OFFSETPOS.Z;


                //pivotOfpelvis += tempOffset;

                if (Program.usethirdpersonview == 0)
                {
                    pivotOfpelvis += OFFSETPOS;
                    //pivotOfpelvis.Y = tempOffset.Y;

                }
                else if (Program.usethirdpersonview == 1)
                {
                    pivotOfpelvis += tempOffset;
                }



                matrixerer.M41 = pivotOfpelvis.X;
                matrixerer.M42 = pivotOfpelvis.Y;
                matrixerer.M43 = pivotOfpelvis.Z;
                worldMatrix_instances_pelvis[0][0][_iterator] = matrixerer;
                _player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
















            }



            return _sc_jitter_tasks;
        }


        public scmessageobjectjitter[][] ikbodyrender(scmessageobjectjitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix hmdrotMatrix, Matrix hmd_matrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft, Matrix oriProjectionMatrix)
        {
            //#region
            //UPPERBODY
            var _cuber001 = _player_torso[0][0];
            _cuber001.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_torso(SC_console_directx.D3D.device.ImmediateContext, _cuber001.IndexCount, _cuber001.InstanceCount, _cuber001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_torso_BUFFER, _cuber001);
            _cuber001 = _player_pelvis[0][0];
            _cuber001.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_pelvis(SC_console_directx.D3D.device.ImmediateContext, _cuber001.IndexCount, _cuber001.InstanceCount, _cuber001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_pelvis_BUFFER, _cuber001);
            _player_head[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_torso(SC_console_directx.D3D.device.ImmediateContext, _player_head[0][0].IndexCount, _player_head[0][0].InstanceCount, _player_head[0][0]._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_torso_BUFFER, _player_head[0][0]);



            return _sc_jitter_tasks;
        }

        public scmessageobjectjitter[][] writeikbodytobuffer(scmessageobjectjitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix hmdrotMatrix, Matrix hmd_matrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft, Matrix oriProjectionMatrix)
        {

            Matrix[] worldMatrix_base = new Matrix[1];
            worldMatrix_base[0] = Matrix.Identity;


            //PHYSICS PELVIS
            _player_pelvis[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_pelvis[0][0];
            _player_pelvis[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS TORSO
            _player_torso[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_torso[0][0];
            _player_torso[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS TORSO
            _player_head[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_head[0][0];
            _player_head[0][0]._POSITION = worldMatrix_base[0];

            //tick_perf_counter.Stop();
            //tick_perf_counter.Reset();
            //tick_perf_counter.Restart();

            voxel_cuber_pelvis = _player_pelvis[0][0];
            voxel_sometester_pelvis = voxel_cuber_pelvis._WORLDMATRIXINSTANCES;

            voxel_cuber_torso = _player_torso[0][0];
            voxel_sometester_torso = voxel_cuber_torso._WORLDMATRIXINSTANCES;

            voxel_cuber_head = _player_head[0][0];
            voxel_sometester_head = voxel_cuber_head._WORLDMATRIXINSTANCES;
            for (int i = 0; i < voxel_cuber_pelvis.instances.Length; i++)
            {
                //voxel_cuber = _player_pelvis[tempIndex];
                //voxel_instancers = voxel_cuber.instances;
                //voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                var xxx = voxel_sometester_pelvis[i].M41;
                var yyy = voxel_sometester_pelvis[i].M42;
                var zzz = voxel_sometester_pelvis[i].M43;

                voxel_cuber_pelvis.instances[i].position.X = xxx;
                voxel_cuber_pelvis.instances[i].position.Y = yyy;
                voxel_cuber_pelvis.instances[i].position.Z = zzz;
                voxel_cuber_pelvis.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_pelvis[i], out _testQuater);

                var dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_pelvis.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_pelvis.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_pelvis.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_pelvis.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_pelvis.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_pelvis.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_pelvis.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_pelvis.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_pelvis.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_pelvis.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_pelvis.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_pelvis.instancesDataUP[i].rotation.W = 1;


                //voxel_cuber = _player_torso[tempIndex];
                //voxel_instancers = voxel_cuber.instances;
                //voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                xxx = voxel_sometester_torso[i].M41;
                yyy = voxel_sometester_torso[i].M42;
                zzz = voxel_sometester_torso[i].M43;

                voxel_cuber_torso.instances[i].position.X = xxx;
                voxel_cuber_torso.instances[i].position.Y = yyy;
                voxel_cuber_torso.instances[i].position.Z = zzz;
                voxel_cuber_torso.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_torso[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_torso.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_torso.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_torso.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_torso.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_torso.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_torso.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_torso.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_torso.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_torso.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_torso.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_torso.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_torso.instancesDataUP[i].rotation.W = 1;





                xxx = voxel_sometester_head[i].M41;
                yyy = voxel_sometester_head[i].M42;
                zzz = voxel_sometester_head[i].M43;

                voxel_cuber_head.instances[i].position.X = xxx;
                voxel_cuber_head.instances[i].position.Y = yyy;
                voxel_cuber_head.instances[i].position.Z = zzz;
                voxel_cuber_head.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_head[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_head.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_head.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_head.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_head.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_head.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_head.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_head.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_head.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_head.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_head.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_head.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_head.instancesDataUP[i].rotation.W = 1;








            }



            //PHYSICS CONTAINMENT GRID HANDRIGHT
            /*_world_containment_grid_list_RH[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_hand[0][0];// worldMatrix_instances_containment_grid_RH[0][0];// _world_containment_grid_list_RH[0][0]._arrayOfInstances[_iterator].current_pos;// worldMatrix_instances_containment_grid_RH[0][0]; // 
            _world_containment_grid_list_RH[0][0]._POSITION = worldMatrix_base[0];

            var cuber_containment_grid_RH = _world_containment_grid_list_RH[0][0];
            var instancers_containment_grid_RH = cuber_containment_grid_RH.instances;
            var sometester_containment_grid_RH = cuber_containment_grid_RH._WORLDMATRIXINSTANCES;


            for (int i = 0; i < instancers_containment_grid_RH.Length; i++)
            {
                float xxx = sometester_containment_grid_RH[i].M41;
                float yyy = sometester_containment_grid_RH[i].M42;
                float zzz = sometester_containment_grid_RH[i].M43;

                cuber_containment_grid_RH.instances[i].position.X = xxx;
                cuber_containment_grid_RH.instances[i].position.Y = yyy;
                cuber_containment_grid_RH.instances[i].position.Z = zzz;
                cuber_containment_grid_RH.instances[i].position.W = 1;

                Quaternion.RotationMatrix(ref sometester_containment_grid_RH[i], out quat_buffers);

                var dirInstance = sc_maths._newgetdirforward(quat_buffers);
                //var dirInstance = _newgetdirforward(_testQuater);
                cuber_containment_grid_RH.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber_containment_grid_RH.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber_containment_grid_RH.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber_containment_grid_RH.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(quat_buffers);
                //dirInstance = -_newgetdirleft(_testQuater);
                cuber_containment_grid_RH.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber_containment_grid_RH.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber_containment_grid_RH.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber_containment_grid_RH.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = sc_maths._newgetdirup(quat_buffers);
                //dirInstance = dirInstance = _newgetdirup(_testQuater);
                cuber_containment_grid_RH.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber_containment_grid_RH.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber_containment_grid_RH.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber_containment_grid_RH.instancesDataUP[i].rotation.W = 1;
            }
            //END OF*/
            return _sc_jitter_tasks;
        }







        int hasparentobject = -1;



        public float voxel_general_size = 0.0025f;




        int humaninstlimbsx;
        int humaninstlimbsy;
        int humaninstlimbsz;

        int somechunkpriminstanceikarmvoxellimbsindex;

        public scmessageobjectjitter[][] createikarm(scmessageobjectjitter[][] _sc_jitter_tasks, int tempMultiInstancePhysicsTotal, Vector3 ikarmpivotinitposition, sccsikvoxellimbs parentobject_, int somechunkpriminstanceikarmvoxellimbsindex_, int humaninstlimbsx_, int humaninstlimbsy_, int humaninstlimbsz_)
        {



            somechunkpriminstanceikarmvoxellimbsindex = somechunkpriminstanceikarmvoxellimbsindex_;
            //head // 0
            //pelvis // 1
            //torso // 2
            //pivot arm (shoulder) // 3
            //lower arm // 4
            //upper arm // 5
            //hand // 6
            int voxelbodyparttype = -1;


            humaninstlimbsx = humaninstlimbsx_;
            humaninstlimbsy = humaninstlimbsy_;
            humaninstlimbsz = humaninstlimbsz_;


            if (parentobject_ != null)
            {
                parentobject = parentobject_;
                hasparentobject = 1;
            }



            initchunkposition = ikarmpivotinitposition;

            if (Program.usejitterphysics == 1)
            {
                //SETTING UP SINGLE WORLD OBJECTS
                //END OF LOOP FOR PHYSICS ENGINE INSTANCES
                _some_data0 = (object)_sc_jitter_tasks[0][0]._world_data[0];
                //World[] _jitter_worlds0 = (World[])_some_data0;
                _thejitter_world = (World)_some_data0;
            }




            int _inst_p_r_hand_x = humaninstlimbsx;
            int _inst_p_r_hand_y = humaninstlimbsy;
            int _inst_p_r_hand_z = humaninstlimbsz;
            int _inst_p_r_shoulder_x = humaninstlimbsx;
            int _inst_p_r_shoulder_y = humaninstlimbsy;
            int _inst_p_r_shoulder_z = humaninstlimbsz;
            int _inst_p_r_upperarm_x = humaninstlimbsx;
            int _inst_p_r_upperarm_y = humaninstlimbsy;
            int _inst_p_r_upperarm_z = humaninstlimbsz;
            int _inst_p_r_lowerarm_x = humaninstlimbsx;
            int _inst_p_r_lowerarm_y = humaninstlimbsy;
            int _inst_p_r_lowerarm_z = humaninstlimbsz;

            int _type_of_cube = 2;
            bool is_static = false;


            var WorldMatrix = Matrix.Identity;
            float _dist_between = 1;
            int _addToWorld = 0;

            int voxel_type = 2;


            _player_rght_hnd = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_shldr = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_lower_arm = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_upper_arm = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_elbow_target = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_elbow_target_two = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_r_hand_grab = new sc_voxel[tempMultiInstancePhysicsTotal][];
            worldMatrix_instances_r_hand_grab = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_elbow_target = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_elbow_target_two = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_hand = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_shoulder = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_upperarm = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_lowerarm = new Matrix[tempMultiInstancePhysicsTotal][][];

            //UPPER BODY
            _player_r_hand_grab[0] = new sc_voxel[1];
            worldMatrix_instances_r_hand_grab[0] = new Matrix[1][];
            _player_rght_hnd[0] = new sc_voxel[1];
            worldMatrix_instances_r_hand[0] = new Matrix[1][];
            _player_rght_shldr[0] = new sc_voxel[1];
            worldMatrix_instances_r_shoulder[0] = new Matrix[1][];
            _player_rght_lower_arm[0] = new sc_voxel[1];
            worldMatrix_instances_r_lowerarm[0] = new Matrix[1][];
            _player_rght_upper_arm[0] = new sc_voxel[1];
            worldMatrix_instances_r_upperarm[0] = new Matrix[1][];
            _player_rght_elbow_target[0] = new sc_voxel[1];
            worldMatrix_instances_r_elbow_target[0] = new Matrix[1][];
            _player_rght_elbow_target_two[0] = new sc_voxel[1];
            worldMatrix_instances_r_elbow_target_two[0] = new Matrix[1][];

            Quaternion otherQuat;

            float vertoffsetx = 0;
            float vertoffsety = 0;
            float vertoffsetz = 0;
            //PLAYER RIGHT SHOULDER
            float r = 0.19f;
            float g = 0.19f;
            float b = 0.19f;
            float a = 1;

            Matrix tempmatrix = Matrix.Identity;
            tempmatrix = WorldMatrix;
            tempmatrix.M41 = 0;// parentobject.initchunkposition.X + ikarmpivotinitposition.X;
            tempmatrix.M42 = 0;// parentobject.initchunkposition.Y + ikarmpivotinitposition.Y;
            tempmatrix.M43 = 0;
            tempmatrix.M44 = 1;
            float offsetPosX = _dist_between * 2;
            float offsetPosY = _dist_between * 2;
            float offsetPosZ = _dist_between * 2;
            //_player_rght_shldr[0] = new sc_voxel();
            //_hasinit0 = _player_rght_shldr.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, tempmatrix, 9, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_rght_shldr[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_shoulder_x, _inst_p_r_shoulder_y, _inst_p_r_shoulder_z, Hwnd, tempmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerShoulderRight, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            float _mass = 100;
            //voxel_general_size = 0.0025f;
            //voxel_type = 0;
            _type_of_cube = 2;

            voxelbodyparttype = 3;
            _player_rght_shldr[0][0] = new sc_voxel();
            //_player_rght_shldr[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, tempmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerShoulderRight, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
            _player_rght_shldr[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_shoulder_x, _inst_p_r_shoulder_y, _inst_p_r_shoulder_z, Program.consoleHandle, tempmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerShoulderRight,
                9, 9, 9, 9, 9, 9, 9, 9, 9, 10, 9, 10, 9, 10, 9,

                voxel_general_size, new Vector3(0, 0, 0), 17, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type, voxelbodyparttype, somechunkpriminstanceikarmvoxellimbsindex); //, "terrainGrassDirt.bmp" //0.00035f


            worldMatrix_instances_r_shoulder[0][0] = new Matrix[_inst_p_r_shoulder_x * _inst_p_r_shoulder_y * _inst_p_r_shoulder_z];

            //Program.MessageBox((IntPtr)0, "TESt" + worldMatrix_instances_r_shoulder[0][0].Length, "sc core systems message", 0);

            for (int i = 0; i < worldMatrix_instances_r_shoulder[0][0].Length; i++)
            {
                //worldMatrix_instances_r_shoulder[0][0][i] = Matrix.Identity;


                //var movingpoint = +(direction_feet_up_torso * heightOftorso * 0.5f);
                var movingpoint = new Vector3(parentobject_._player_head[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M41, parentobject_._player_head[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42, parentobject_._player_head[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M43);

                //Matrix worldmatrixhead = _player_rght_shldr[0][0]._arrayOfInstances[i].current_pos; //Matrix.Identity;
                Matrix worldmatrixtorso = parentobject_._player_torso[0][0]._arrayOfInstances[0].current_pos; //Matrix.Identity;

                Quaternion.RotationMatrix(ref worldmatrixtorso, out otherQuat);
                var direction_feet_forward_shldr = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right_shldr = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_feet_up_shldr = sc_maths._getDirection(Vector3.Up, otherQuat);

                //var widthofhead = _player_rght_shldr[0][0]._total_torso_width * 1.0f;
                //var heightofhead = _player_rght_shldr[0][0]._total_torso_height * 1.0f;
                //var depthofhead = _player_rght_shldr[0][0]._total_torso_depth * 1.0f;

                /*Matrix worldmatrixtorso = _player_rght_shldr[0][0]._arrayOfInstances[i].current_pos; //Matrix.Identity;
                Quaternion.RotationMatrix(ref worldmatrixtorso, out otherQuat);

                var direction_feet_forward_torso = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right_torso = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_feet_up_torso = sc_maths._getDirection(Vector3.Up, otherQuat);
                */




                var heightofhead = parentobject_._player_head[0][0]._total_torso_height * 1.0f;
                var heightOftorso = parentobject_._player_torso[0][0]._total_torso_height * 1.0f;
                var heightofpelvis = parentobject_._player_pelvis[0][0]._total_torso_height * 1.0f;

                var widthofhead = parentobject_._player_head[0][0]._total_torso_width * 1.0f;
                var widthoftorso = parentobject_._player_torso[0][0]._total_torso_width * 1.0f;
                var widthofpelvis = parentobject_._player_pelvis[0][0]._total_torso_width * 1.0f;

                var depthofhead = parentobject_._player_head[0][0]._total_torso_depth * 1.0f;
                var depthoftorso = parentobject_._player_torso[0][0]._total_torso_depth * 1.0f;
                var depthofpelvis = parentobject_._player_pelvis[0][0]._total_torso_depth * 1.0f;

                var shoulderwidth = _player_rght_shldr[0][0]._total_torso_width * 1.0f;
                var shoulderheight = _player_rght_shldr[0][0]._total_torso_height * 1.0f;
                var shoulderdepth = _player_rght_shldr[0][0]._total_torso_depth * 1.0f;


                if (somechunkpriminstanceikarmvoxellimbsindex == 0)
                {
                    
                    /*///movingpoint = movingpoint + (-direction_feet_up_shldr * heightofhead);
                    movingpoint = movingpoint + (-direction_feet_right_shldr * widthoftorso);
                    //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso);
                    //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso);
                    //movingpoint = movingpoint + (-direction_feet_up_torso * _player_rght_shldr[0][0]._total_torso_height);

                    var tempmatrix1 = Matrix.Identity; //Matrix.Identity; //_player_rght_shldr[0][0]._arrayOfInstances[i].current_pos;

                    ///tempmatrix1.M41 = _player_rght_shldr[0][0]._arrayOfInstances[i].current_pos.M41;
                    //tempmatrix1.M42 = _player_rght_shldr[0][0]._arrayOfInstances[i].current_pos.M42;
                    //tempmatrix1.M43 = _player_rght_shldr[0][0]._arrayOfInstances[i].current_pos.M43;

                    tempmatrix1.M41 = movingpoint.X;
                    tempmatrix1.M42 = movingpoint.Y;
                    tempmatrix1.M43 = movingpoint.Z;


                    tempmatrix1.M44 = 1.0f;

                    //_player_rght_shldr[0][0]._arrayOfInstances[i].current_pos = tempmatrix1;
                    //_player_rght_shldr[0][0]._arrayOfInstances[0]

                    worldMatrix_instances_r_shoulder[0][0][i] = tempmatrix1;// _player_rght_shldr[0][0]._arrayOfInstances[i].current_pos;// Matrix.Identity;*/
                }
                else if (somechunkpriminstanceikarmvoxellimbsindex == 1)
                {

                }
                else if (somechunkpriminstanceikarmvoxellimbsindex == 2)
                {

                }
                else if (somechunkpriminstanceikarmvoxellimbsindex == 3)
                {
                    /*Program.MessageBox((IntPtr)0, "TESt" + somechunkpriminstanceikarmvoxellimbsindex, "sc core systems message", 0);
                    ///movingpoint = movingpoint + (-direction_feet_up_shldr * heightofhead);
                    movingpoint = movingpoint - (-direction_feet_right_shldr * widthoftorso);
                    //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso);
                    //movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso);
                    //movingpoint = movingpoint + (-direction_feet_up_torso * _player_rght_shldr[0][0]._total_torso_height);

                    var tempmatrix1 = _player_rght_shldr[0][0]._arrayOfInstances[i].current_pos;// Matrix.Identity; //Matrix.Identity; //_player_rght_shldr[0][0]._arrayOfInstances[i].current_pos;

                    ///tempmatrix1.M41 = _player_rght_shldr[0][0]._arrayOfInstances[i].current_pos.M41;
                    //tempmatrix1.M42 = _player_rght_shldr[0][0]._arrayOfInstances[i].current_pos.M42;
                    //tempmatrix1.M43 = _player_rght_shldr[0][0]._arrayOfInstances[i].current_pos.M43;

                    tempmatrix1.M41 = movingpoint.X;
                    tempmatrix1.M42 = movingpoint.Y;
                    tempmatrix1.M43 = movingpoint.Z;


                    tempmatrix1.M44 = 1.0f;

                    //_player_rght_shldr[0][0]._arrayOfInstances[i].current_pos = tempmatrix1;
                    //_player_rght_shldr[0][0]._arrayOfInstances[0]

                    worldMatrix_instances_r_shoulder[0][0][i] = tempmatrix1;   // _player_rght_shldr[0][0]._arrayOfInstances[i].current_pos;// Matrix.Identity;*/

                    //var movingpoint = +(direction_feet_up_torso * heightOftorso * 0.5f);
                    movingpoint = new Vector3(parentobject_._player_torso[0][0]._arrayOfInstances[i].current_pos.M41, parentobject_._player_torso[0][0]._arrayOfInstances[i].current_pos.M42, parentobject_._player_torso[0][0]._arrayOfInstances[i].current_pos.M43);

                    /*Matrix worldmatrixhead = parentobject_._player_head[0][0]._arrayOfInstances[i].current_pos; //Matrix.Identity;

                    Quaternion.RotationMatrix(ref worldmatrixhead, out otherQuat);
                    var direction_feet_forward_head = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    var direction_feet_right_head = sc_maths._getDirection(Vector3.Right, otherQuat);
                    var direction_feet_up_head = sc_maths._getDirection(Vector3.Up, otherQuat);

                    widthofhead = parentobject_._player_head[0][0]._total_torso_width * 1.0f;
                    heightofhead = parentobject_._player_head[0][0]._total_torso_height * 1.0f;
                    depthofhead = parentobject_._player_head[0][0]._total_torso_depth * 1.0f;

                    worldmatrixtorso = _player_rght_shldr[0][0]._arrayOfInstances[i].current_pos; //Matrix.Identity;
                    Quaternion.RotationMatrix(ref worldmatrixtorso, out otherQuat);
                    var direction_feet_forward_torso = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    var direction_feet_right_torso = sc_maths._getDirection(Vector3.Right, otherQuat);
                    var direction_feet_up_torso = sc_maths._getDirection(Vector3.Up, otherQuat);

                    movingpoint = movingpoint + (-direction_feet_up_head * heightofhead);
                    movingpoint = movingpoint + (-direction_feet_up_torso * heightOftorso);

                    var tempmatrix1 = _player_rght_shldr[0][0]._arrayOfInstances[i].current_pos; //Matrix.Identity; //_player_pelvis[0][0]._arrayOfInstances[i].current_pos;

                    tempmatrix1.M41 = _player_rght_shldr[0][0]._arrayOfInstances[i].current_pos.M41;
                    tempmatrix1.M42 = _player_rght_shldr[0][0]._arrayOfInstances[i].current_pos.M42;
                    tempmatrix1.M43 = _player_rght_shldr[0][0]._arrayOfInstances[i].current_pos.M43;
                    */

                    Matrix tempmatrix1 = Matrix.Identity;
                    tempmatrix1.M41 = movingpoint.X;
                    tempmatrix1.M42 = movingpoint.Y;
                    tempmatrix1.M43 = movingpoint.Z;

                    tempmatrix1.M44 = 1.0f;

                    worldMatrix_instances_r_shoulder[0][0][i] = tempmatrix1;


                }
            }






            ////////////////////////////////////////////////
            //////////CONTAINMENT GRIDS RIGHT HAND//////////
            ////////////////////////////////////////////////
            /*r = 0.85f;
            g = 0.85f;
            b = 0.85f;
            a = 1;
            _object_worldmatrix = Matrix.Identity;
            //offsetPosX = _grid_size_x * 1.15f; //x between each world instance
            //offsetPosY = _grid_size_y * 1.15f; //y between each world instance
            //offsetPosZ = _grid_size_z * 1.15f; //z between each world instance
            _object_worldmatrix = WorldMatrix;
            _object_worldmatrix.M41 = 0;set
            _size_screen = 0.015f;
            _object_worldmatrix.M42 = _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._POSITION.M42 + (_player_rght_hnd[0][0]._total_torso_height) + (1 * 0.001f); //_terrain_size_y + (_terrain_size_y * 0.501f)-5 //_terrain[0][0]._arrayOfInstances[_iterator]._POSITION.M42
            _object_worldmatrix.M43 = 0;
            _object_worldmatrix.M44 = 1;
            _type_of_cube = 0;
            _world_containment_grid_list_RH[0] = new sc_containment_grid[1];
            _world_containment_grid_list_RH[0][0] = new sc_containment_grid();
            _world_containment_grid_list_RH[0][0].Initialize(SC_console_directx.D3D, 10, 10, _size_screen, 10, 10, _grid_size_x, _grid_size_y, _grid_size_z, new Vector4(r, g, b, a), _inst_terrain_tile_x, _inst_terrain_tile_y, _inst_terrain_tile_z, SC_Update.HWND, _object_worldmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, SCCoreSystems.sc_console.SC_console_directx.BodyTag.sc_containment_grid, true, 0, 10, 0, 0, 0, 0, 0, 0, false, true, false, false, false, false); //, "terrainGrassDirt.bmp" //0.00035f
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    //_world_containment_grid_list_RH[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0.1f, 1, 1, _inst_terrain_tile_x, _inst_terrain_tile_y, _inst_terrain_tile_z, new Vector4(r, g, b, a), _inst_screen_x, _inst_screen_y, _inst_screen_z, SC_Update.HWND, _object_worldmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, SCCoreSystems.sc_console.SC_console_directx.BodyTag.sc_containment_grid, false, 1, 100, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

            worldMatrix_instances_containment_grid_RH[0] = new Matrix[1][];
            worldMatrix_instances_containment_grid_RH[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
            for (int i = 0; i < worldMatrix_instances_containment_grid_RH[0][0].Length; i++)
            {
                worldMatrix_instances_containment_grid_RH[0][0][i] = _player_rght_hnd[0][0]._arrayOfInstances[i].current_pos;
            }*/










            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;

            //RIGHT LOWER ARM
            r = 0.19f;
            g = 0.19f;
            b = 0.19f;
            a = 1;
            tempmatrix = Matrix.Identity;
            tempmatrix = WorldMatrix;

            tempmatrix.M41 = 0;
            tempmatrix.M42 = 0;//  0 + -0.15f;
            tempmatrix.M43 = 0;

            tempmatrix.M44 = 1;
            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;

            //_player_lft_lower_arm[0] = new sc_voxel();
            //_hasinit0 = _player_lft_lower_arm.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, tempmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_lft_lower_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_l_lowerarm_x, _inst_p_l_lowerarm_y, _inst_p_l_lowerarm_z, Hwnd, tempmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerArmLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            //voxel_general_size = 0.0025f;
            //voxel_type = 0;
            _type_of_cube = 2;
            voxelbodyparttype = 4;
            _player_rght_lower_arm[0][0] = new sc_voxel();
            // _player_lft_lower_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, tempmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerArmLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
            _player_rght_lower_arm[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a),
                _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, tempmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static,
                SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerArmRight,
                8, 8, 8, 8, 8, 8, 8, 8, 8, 11, 10, 20, 19, 11, 10,

                voxel_general_size, new Vector3(0, 0, 0), 30, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type, voxelbodyparttype, somechunkpriminstanceikarmvoxellimbsindex); //, "terrainGrassDirt.bmp" //0.00035f
                                                                                                                                                                                             //_player_rght_lower_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, tempmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerArmRight, 7, 7, 9, 18, 21, 25, 10, 9, 33, 32, 11, 10, voxel_general_size, new Vector3(0, 0, 0), 72, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

            //FOR VOXEL ARROW
            //_player_lft_lower_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, tempmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerArmLeft, 9, 9, 9, 18, 17, 100, 3, 10, 33, 32, 11, 10, voxel_general_size, new Vector3(0, 0, 0), 70, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

            worldMatrix_instances_r_lowerarm[0][0] = new Matrix[_inst_p_r_lowerarm_x * _inst_p_r_lowerarm_y * _inst_p_r_lowerarm_z];
            for (int i = 0; i < worldMatrix_instances_r_lowerarm[0][0].Length; i++)
            {
                worldMatrix_instances_r_lowerarm[0][0][i] = Matrix.Identity;
            }

            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;
            //RIGHT UPPER ARM
            r = 0.19f;
            g = 0.19f;
            b = 0.19f;
            a = 1;
            tempmatrix = Matrix.Identity;
            tempmatrix = WorldMatrix;


            tempmatrix.M41 = 0;
            tempmatrix.M42 = 0;// 0 + -0.375f;
            tempmatrix.M43 = 0;

            tempmatrix.M44 = 1;
            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;
            //_player_rght_upper_arm[0] = new sc_voxel();
            //_hasinit0 = _player_rght_upper_arm.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, tempmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_rght_upper_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_upperarm_x, _inst_p_r_upperarm_y, _inst_p_r_upperarm_z, Hwnd, tempmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmRight, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            //voxel_general_size = 0.0025f;
            //voxel_type = 0;
            _type_of_cube = 2;
            voxelbodyparttype = 5;

            _player_rght_upper_arm[0][0] = new sc_voxel();
            //_player_rght_upper_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, tempmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmRight, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
            _player_rght_upper_arm[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.035f, 0.1055f, 0.035f, new Vector4(r, g, b, a),
            _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, tempmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static,
            SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmRight,
            8, 8, 8, 8, 8, 8, 8, 8, 8, 13, 12, 30, 29, 13, 12,
            voxel_general_size, new Vector3(0, 0, 0), 40, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type, voxelbodyparttype, somechunkpriminstanceikarmvoxellimbsindex); //, "terrainGrassDirt.bmp" //0.00035f
            worldMatrix_instances_r_upperarm[0][0] = new Matrix[_inst_p_r_upperarm_x * _inst_p_r_upperarm_y * _inst_p_r_upperarm_z];
            for (int i = 0; i < worldMatrix_instances_r_upperarm[0][0].Length; i++)
            {
                worldMatrix_instances_r_upperarm[0][0][i] = Matrix.Identity;
            }






            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;
            //RIGHT ELBOW TARGET
            r = 0.19f;
            g = 0.99f;
            b = 0.19f;
            a = 1;
            //tempmatrix.M41 = -0.25f; /
            //tempmatrix.M42 = -0.2f;
            tempmatrix = Matrix.Identity;
            tempmatrix = WorldMatrix;

            tempmatrix.M41 = 0;
            tempmatrix.M42 = 0 - (_player_rght_upper_arm[0][0]._POSITION.M42 + (_player_rght_upper_arm[0][0]._total_torso_height * 0.5f) - 0.45f);
            tempmatrix.M43 = 0 + 3;

            tempmatrix.M44 = 1;
            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;
            //_player_rght_elbow_target[0] = new sc_voxel();
            //_hasinit0 = _player_rght_elbow_target.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, tempmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_rght_elbow_target[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, tempmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            //voxel_general_size = 0.0025f;
            //voxel_type = 0;
            _type_of_cube = 2;

            voxelbodyparttype = -1;
            _player_rght_elbow_target[0][0] = new sc_voxel();
            _player_rght_elbow_target[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, tempmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightElbowTarget, 2, 2, 2, 2, 2, 2, 9, 9, 9, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 25, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type, voxelbodyparttype, somechunkpriminstanceikarmvoxellimbsindex); //, "terrainGrassDirt.bmp" //0.00035f
            worldMatrix_instances_r_elbow_target[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
            for (int i = 0; i < worldMatrix_instances_r_elbow_target[0][0].Length; i++)
            {
                worldMatrix_instances_r_elbow_target[0][0][i] = Matrix.Identity;
            }









            //RIGHT ELBOW TARGET TWO
            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;
            r = 0.19f;
            g = 0.19f;
            b = 0.99f;
            a = 1;
            tempmatrix = Matrix.Identity;
            tempmatrix = WorldMatrix;

            tempmatrix.M41 = 0;
            tempmatrix.M42 = 0 + (_player_rght_upper_arm[0][0]._POSITION.M42 + (_player_rght_upper_arm[0][0]._total_torso_height * 0.5f) + 1);
            tempmatrix.M43 = 0 + 0;


            tempmatrix.M44 = 1;
            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;
            //_player_rght_elbow_target_two[0] = new sc_voxel();
            //_hasinit0 = _player_rght_elbow_target_two.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, tempmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
            //_hasinit0 = _player_rght_elbow_target_two[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, tempmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
            //voxel_general_size = 0.0025f;
            //voxel_type = 0;
            _type_of_cube = 2;
            _player_rght_elbow_target_two[0][0] = new sc_voxel();
            _player_rght_elbow_target_two[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, tempmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightElbowTargettwo, 2, 2, 2, 2, 2, 2, 9, 9, 9, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 75, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type, voxelbodyparttype, somechunkpriminstanceikarmvoxellimbsindex); //, "terrainGrassDirt.bmp" //0.00035f
            worldMatrix_instances_r_elbow_target_two[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
            for (int i = 0; i < worldMatrix_instances_r_elbow_target_two[0][0].Length; i++)
            {
                worldMatrix_instances_r_elbow_target_two[0][0][i] = Matrix.Identity;
            }




            //PLAYER RIGHT HAND GRAB
            //voxel_type = 2;
            r = 0.19f;
            g = 0.19f;
            b = 0.19f;
            a = 1;
            //instX = 1;
            //instY = 1;
            //instZ = 1;
            tempmatrix = Matrix.Identity;
            tempmatrix = WorldMatrix;


            tempmatrix.M41 = 0;
            tempmatrix.M42 = 0;// + -(_player_rght_upper_arm[0][0]._total_torso_height - _player_rght_lower_arm[0][0]._total_torso_height);
            tempmatrix.M43 = 0;



            tempmatrix.M44 = 1;
            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;
            _mass = 100;
            vertoffsetx = 0;
            vertoffsety = 0;
            vertoffsetz = 0;

            /*if (voxel_type == 0)
            {
                vertoffsetz = -13 * 0.075f;
            }
            else
            {
                vertoffsetz = -13;
            }*/
            //voxel_general_size = 0.0025f;
            voxel_type = 3;
            _type_of_cube = 2;
            is_static = true;

            //covid19/snowflake/mine init = 9, 9, 9, 9, 9, 9, 9, 9, 9, 60, 60, 60, 60, 60, 60,

            _player_r_hand_grab[0][0] = new sc_voxel();
            _player_r_hand_grab[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, tempmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightHandGrabTarget, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 3, 65, 64, 40, 39, voxel_general_size, new Vector3(0, 0, -0.1f), 95, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type, voxelbodyparttype, somechunkpriminstanceikarmvoxellimbsindex); //, "terrainGrassDirt.bmp" //0.00035f
            worldMatrix_instances_r_hand_grab[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
            for (int i = 0; i < worldMatrix_instances_r_hand_grab[0][0].Length; i++)
            {
                worldMatrix_instances_r_hand_grab[0][0][i] = Matrix.Identity;
            }













            int minx = 1;
            int miny = 1;
            int minz = 1;

            int diagmaxx = 1;
            int diagmaxy = 1;
            int diagmaxz = 1;

            int diagminx = 1;
            int diagminy = 1;
            int diagminz = 1;

            int chunkwidthl = 1;
            int chunkwidthr = 1;

            int chunkheightl = 1;
            int chunkheightr = 1;

            int chunkdepthl = 1;
            int chunkdepthr = 1;

            float distance = 1.0f;

            //9, 9, 9, 18, 9, 9, 9, 9, 9, 4, 3, 13, 12, 18, 17
            if (somechunkpriminstanceikarmvoxellimbsindex == 0)
            {

                minx = 9;
                miny = 9;
                minz = 9;

                diagmaxx = 18;
                diagmaxy = 9;
                diagmaxz = 9;

                diagminx = 9;
                diagminy = 9;
                diagminz = 9;

                chunkwidthl = 4;
                chunkwidthr = 3;

                chunkheightl = 13;
                chunkheightr = 12;

                chunkdepthl = 18;
                chunkdepthr = 17;
                distance = 75;
            }
            else if (somechunkpriminstanceikarmvoxellimbsindex == 1)
            {
                minx = 1;
                miny = 1;
                minz = 1;

                diagmaxx = 1;
                diagmaxy = 1;
                diagmaxz = 1;

                diagminx = 1;
                diagminy = 1;
                diagminz = 1;

                chunkwidthl = 13;
                chunkwidthr = 12;

                chunkheightl = 4;
                chunkheightr = 3;

                chunkdepthl = 18;
                chunkdepthr = 17;
                distance = 25;
            }
            else if (somechunkpriminstanceikarmvoxellimbsindex == 2)
            {
                minx = 1;
                miny = 1;
                minz = 1;

                diagmaxx = 1;
                diagmaxy = 1;
                diagmaxz = 1;

                diagminx = 1;
                diagminy = 1;
                diagminz = 1;

                chunkwidthl = 13;
                chunkwidthr = 12;

                chunkheightl = 4;
                chunkheightr = 3;

                chunkdepthl = 18;
                chunkdepthr = 17;
                distance = 25;
            }
            else if (somechunkpriminstanceikarmvoxellimbsindex == 3)
            {

                minx = 9;
                miny = 9;
                minz = 9;

                diagmaxx = 18;
                diagmaxy = 9;
                diagmaxz = 9;

                diagminx = 9;
                diagminy = 9;
                diagminz = 9;

                chunkwidthl = 4;
                chunkwidthr = 3;

                chunkheightl = 13;
                chunkheightr = 12;

                chunkdepthl = 18;
                chunkdepthr = 17;
                distance = 75;
            }







            //PLAYER RIGHT HAND
            voxel_type = 2;
            r = 0.19f;
            g = 0.19f;
            b = 0.19f;
            a = 1;
            //instX = 1;
            //instY = 1;
            //instZ = 1;
            tempmatrix = Matrix.Identity;
            tempmatrix = WorldMatrix;
            tempmatrix.M41 = 0;
            tempmatrix.M42 = -0.65f;// - (_player_rght_upper_arm[0][0]._total_torso_height - _player_rght_lower_arm[0][0]._total_torso_height);
            tempmatrix.M43 = 0;

            tempmatrix.M44 = 1;
            offsetPosX = _dist_between * 2;
            offsetPosY = _dist_between * 2;
            offsetPosZ = _dist_between * 2;
            _mass = 100;
            vertoffsetx = 0;
            vertoffsety = 0;
            if (voxel_type == 0)
            {
                vertoffsetz = -13 * 0.075f;
            }
            else
            {
                vertoffsetz = -13;
            }
            _player_rght_hnd[0][0] = new sc_voxel();
            //voxel_general_size = 0.0025f;
            //voxel_type = 0;
            _type_of_cube = 2;
            is_static = true;
            voxelbodyparttype = 6;

            //9, 9, 9, 18, 9, 9, 9, 9, 9, 4, 3, 13, 12, 18, 17
            _player_rght_hnd[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, tempmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerHandRight, minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz, chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr, voxel_general_size, new Vector3(0, 0, -0.1f), distance, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type, voxelbodyparttype, somechunkpriminstanceikarmvoxellimbsindex); //, "terrainGrassDirt.bmp" //0.00035f
            worldMatrix_instances_r_hand[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
            for (int i = 0; i < worldMatrix_instances_r_hand[0][0].Length; i++)
            {
                worldMatrix_instances_r_hand[0][0][i] = Matrix.Identity;
            }
            minx = 9;
            miny = 9;
            minz = 9;

            diagmaxx = 9;
            diagmaxy = 9;
            diagmaxz = 9;

            diagminx = 9;
            diagminy = 9;
            diagminz = 9;

            chunkwidthl = 9;
            chunkwidthr = 9;

            chunkheightl = 9;
            chunkheightr = 9;

            chunkdepthl = 9;
            chunkdepthr = 9;





            if (Program.usejitterphysics == 1)
            {
                for (int phys = 0; phys < Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z; phys++)
                {
                    for (int i = 0; i < Program.world_width * Program.world_height * Program.world_depth; i++)
                    {
                        object _some_dator = (object)_sc_jitter_tasks[phys][i]._world_data[0];
                        World _the_current_world = (World)_some_dator;

                        _the_current_world.AddBody(_player_rght_upper_arm[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_rght_lower_arm[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_rght_hnd[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_rght_shldr[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                    }
                }
            }
















            _SC_modL_rght_shldr_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };
            _SC_modL_rght_elbow_target_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };

            _SC_modL_rght_elbow_target_two_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };
            _SC_modL_rght_upper_arm_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            }; _SC_modL_rght_lower_arm_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };


            _SC_modL_rght_hnd_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };




            _SC_modL_r_hand_grab_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };


            return _sc_jitter_tasks;
        }



        Vector3[] directionvectoroffsets;

        public scmessageobjectjitter[][] setiktargetnlimbspositionsNrotations(scmessageobjectjitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix hmdrotMatrix, Matrix hmd_matrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft, Matrix oriProjectionMatrix, sc_voxel mainarmparentmeshobject, Vector3 lightpos_, Vector3 dirLight_, Matrix finalRotationMatrix_, sc_voxel mainarmparentmeshobjectmain, sccsikvoxellimbs ikvoxelbody, int somechunkpriminstanceikarmvoxelindex, Matrix hmd_matrix_current, Matrix extramatrix, Vector3[] directionvectoroffsets_)
        {






            //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
            //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
            //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
            Matrix sometempmat2 = ikvoxelbody._player_head[0][0]._arrayOfInstances[0].current_pos;
            Quaternion somedirquat2;
            Quaternion.RotationMatrix(ref sometempmat2, out somedirquat2);
            var dirikvoxelbodyInstanceRight2 = -sc_maths._newgetdirleft(somedirquat2);
            var dirikvoxelbodyInstanceUp2 = sc_maths._newgetdirup(somedirquat2);
            var dirikvoxelbodyInstanceForward2 = sc_maths._newgetdirforward(somedirquat2);


            Vector3 tempOffset = OFFSETPOS;

            //int usethirdpersonview = 1;

            if (Program.usethirdpersonview == 0)
            {
                tempOffset.X = SC_Update.viewPosition.X;
                tempOffset.Y = SC_Update.viewPosition.Y;
                tempOffset.Z = SC_Update.viewPosition.Z;
                /*
                tempOffset.Y = tempOffset.Y + ikvoxelbody._player_head[0][0]._arrayOfInstances[0]._TEMPPIVOT.M42;
                tempOffset = tempOffset + (dirikvoxelbodyInstanceUp2 * (ikvoxelbody._player_head[0][0]._total_torso_height * 0.5f));*/
            }
            else if (Program.usethirdpersonview == 1)
            {
                //OFFSETPOS.X = SC_Update.viewPosition.X;
                //OFFSETPOS.Y = SC_Update.viewPosition.Y;
                //OFFSETPOS.Z = SC_Update.viewPosition.Z;

                //OFFSETPOS = OFFSETPOS + (dirikvoxelbodyInstanceUp0 * -0.125f);
                //SC_Update.viewPosition = SC_Update.viewPosition + (dirikvoxelbodyInstanceRight0 * -1.5f);

                /*//tempmatter = hmd_matrix * rotatingMatrixForPelvis * hmdmatrixRot;
                Quaternion quatt;
                Quaternion.RotationMatrix(ref SC_Update.tempmatter, out quatt);
                // quatt.Invert();

                //THIRD PERSON VR VIEW. COMMENT THIS PART OUT TO HAVE FIRST PERSON VIEW
                Vector3 forwardOVR = sc_maths._getDirection(Vector3.ForwardRH, quatt);
                Vector3 upOVR = sc_maths._getDirection(Vector3.Up, quatt);
                Vector3 rightOVR = sc_maths._getDirection(Vector3.Right, quatt);
                upOVR.Normalize();
                rightOVR.Normalize();
                forwardOVR.Normalize();

                forwardOVR *= -0.5f; // -1.0f

                Vector3 thirdpersonview = OFFSETPOS + (-forwardOVR * 2.0f); //1.5f // + (upOVR * 0.25f)

                OFFSETPOS.X = thirdpersonview.X;// SC_Update.viewPosition.X;
                OFFSETPOS.Y = thirdpersonview.Y;// SC_Update.viewPosition.Y;
                OFFSETPOS.Z = thirdpersonview.Z;// SC_Update.viewPosition.Z;*/
            }



            //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
            //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
            //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW















            directionvectoroffsets = directionvectoroffsets_;


            lightpos = lightpos_;
            dirLight = dirLight_;



            _SC_modL_r_hand_grab_BUFFER[0].lightPosition = lightpos;
            _SC_modL_r_hand_grab_BUFFER[0].lightDirection = dirLight;

            _SC_modL_rght_hnd_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_hnd_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_shldr_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_shldr_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_elbow_target_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_elbow_target_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_elbow_target_two_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_elbow_target_two_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_upper_arm_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_upper_arm_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_lower_arm_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_lower_arm_BUFFER[0].lightDirection = dirLight;





            Matrix finalRotationMatrix = finalRotationMatrix_;//originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdrotMatrix;

            //finalRotationMatrix = finalRotationMatrix * hmdrotMatrix;

            //finalRotationMatrix = hmd_matrix_current * finalRotationMatrix;


            finalRotationMatrix.M41 = 0;
            finalRotationMatrix.M42 = 0;
            finalRotationMatrix.M43 = 0;

            ////////////////////
            /////HUMAN RIG////// 
            ////////////////////
            for (int _iterator = 0; _iterator < _player_rght_hnd[0][0]._arrayOfInstances.Length; _iterator++) //
            {
                var lengthOfLowerArmRight = _player_rght_lower_arm[0][0]._total_torso_height * 1.5f;
                var lengthOfUpperArmRight = _player_rght_upper_arm[0][0]._total_torso_height * 1.5f;
                var lengthOfHandRight = _player_rght_hnd[0][0]._total_torso_height * 1.5f;
                var totalArmLengthRight = lengthOfLowerArmRight + lengthOfUpperArmRight;

                var connectorOfUpperArmRightOffsetMul = 1.5f; //1.55f
                var connectorOfLowerArmRightOffsetMul = 1.5f; //0.70f
                var connectorOfHandOffsetMul = 1.00123f; // 1.00123f

                var connectorOfUpperLegOffsetMul = 1.0f;
                var connectorOfLowerLegOffsetMul = 1.0f;

                //lightpos = new Vector3(0, 100, 0);
                ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
                diffuseColour = new Vector4(1, 1, 1, 1);
                lightDirection = new Vector3(0, -1, -1);
                dirLight = Vector3.Zero;
                lightpos = Vector3.Zero;
                Quaternion otherQuat;


                ///////////
                //SOMETESTS

                Quaternion.RotationMatrix(ref finalRotationMatrix, out otherQuat);
                var direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                var current_rotation_of_torso_pivot_forward = direction_feet_forward;
                var current_rotation_of_torso_pivot_right = direction_feet_right;
                var current_rotation_of_torso_pivot_up = direction_feet_up;
                //SOMETESTS
                ///////////




                Vector3 MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M43);

                //SAVING IN MEMORY THE ORIGINAL TORSO MATRIX NOT AFFECTED BY CURRENT POSITION AND ROTATION CHANGES.
                Matrix _rotMatrixer = mainarmparentmeshobject._arrayOfInstances[_iterator]._ORIGINPOSITION;// _player_torso[0][0]._ORIGINPOSITION;
                Quaternion forTest;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                //FROM THE MATRIX OF ROTATION/POSITION, I GET THE QUATERNION OUT OF THAT AND CREATE THE DIRECTIONS THAT THE OBJECTS ARE ORIGINALLY FACING.
                var direction_feet_forward_ori_torso = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                var direction_feet_right_ori_torso = sc_maths._getDirection(Vector3.Right, forTest);
                var direction_feet_up_ori_torso = sc_maths._getDirection(Vector3.Up, forTest);

                //SINCE THE PIVOT POINT IS CURRENTLY IN THE MIDDLE OF THE TORSO, IT CANNOT ROTATE AT THAT POINT OTHERWISE, IT WONT FOLLOW THE PELVIS ROTATION LATER ON.
                //SO WE CURRENTLY ONLY OFFSET THE TORSO "MIDDLE OF SPINE APPROX" TO HALF OF THE CURRENT HEIGHT IN ORDER TO MAKE THE PIVOT POINT, APPROX WHERE THE PELVIS IS.
                Vector3 TORSOPIVOT = MOVINGPOINTER + -(direction_feet_up_ori_torso * (mainarmparentmeshobject._total_torso_height * 0.5f));













                //RIGHT SHOULDER
                /*
                _rotMatrixer = _player_rght_shldr[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out otherQuat);
                var direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                _SC_modL_rght_shldr_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_rght_shldr[0][0]._POSITION.M41, _player_rght_shldr[0][0]._POSITION.M42, _player_rght_shldr[0][0]._POSITION.M43),
                    padding1 = 100
                };

                MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M43);
                var torsooripos = MOVINGPOINTER;
                _rotMatrixer = _player_rght_shldr[0][0]._ORIGINPOSITION;

                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                var direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                var direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                var direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                var diffNormPosX = (MOVINGPOINTER.X) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                var diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                var diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

                var somepointnearneck = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].updirection * (diffNormPosY));

                var dirToPoint = somepointnearneck - torsooripos;
                dirToPoint.Normalize();

                var shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT;
                Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);

                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);

                var theheadrotmatrix = Matrix.LookAtRH(somepointnearneck, somepointnearneck + direction_head_right, direction_head_up);
                theheadrotmatrix.Invert();
                var matrixerer = theheadrotmatrix;
                //realPosOfRS = realPosOfRS + (mainarmparentmeshobject._arrayOfInstances[_iterator].rightdirection * (_player_rght_shldr[0][0]._total_torso_width));
                //shiftrightshoulderpos = shiftrightshoulderpos + (mainarmparentmeshobject._arrayOfInstances[_iterator].updirection * (_player_rght_shldr[0][0]._total_torso_height * 1f));

                somepointnearneck.X += OFFSETPOS.X;
                somepointnearneck.Y += OFFSETPOS.Y;
                somepointnearneck.Z += OFFSETPOS.Z;

                matrixerer.M41 = somepointnearneck.X;
                matrixerer.M42 = somepointnearneck.Y;
                matrixerer.M43 = somepointnearneck.Z;
                worldMatrix_instances_r_shoulder[0][0][_iterator] = matrixerer;
                _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
                */


                /*
                //var tempPoint = MOVINGPOINTER;
                //tempPoint = tempPoint + -(mainarmparentmeshobject._arrayOfInstances[_iterator].rightdirection * (diffNormPosX));
                var somepointnearneck = tempPoint + -(mainarmparentmeshobject._arrayOfInstances[_iterator].updirection * (diffNormPosY));
                //tempPoint = tempPoint + -(mainarmparentmeshobject._arrayOfInstances[_iterator].updirection * (diffNormPosY));
                //tempPoint = tempPoint + -(mainarmparentmeshobject._arrayOfInstances[_iterator].forwarddirection * (diffNormPosZ));

                var dirToPoint = somepointnearneck - torsooripos;
                dirToPoint.Normalize();

                //var realPosOfRS = MOVINGPOINTER;

                //var realPosOfRS = MOVINGPOINTER + (dirToPoint * ((mainarmparentmeshobject._total_torso_height)));


                //var pivotOfHead = MOVINGPOINTER + (dirToPoint * ((mainarmparentmeshobject._total_torso_height)));

                tempPoint = MOVINGPOINTER;
                tempPoint = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].rightdirection * (diffNormPosX));
                dirToPoint = tempPoint - torsooripos;
                dirToPoint.Normalize();
                //tempPoint = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].rightdirection * (diffNormPosX));
                var shiftrightshoulderpos = somepointnearneck + (dirToPoint * ((mainarmparentmeshobject._total_torso_width))) ;



                shiftrightshoulderpos += realPosOfRS;



                //realPosOfRS.X += OFFSETPOS.X;
                //realPosOfRS.Y += OFFSETPOS.Y;
                //realPosOfRS.Z += OFFSETPOS.Z;

                var shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT;
                Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);

                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);

                var theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right, direction_head_up);
                theheadrotmatrix.Invert();
                var matrixerer = theheadrotmatrix;
                //realPosOfRS = realPosOfRS + (mainarmparentmeshobject._arrayOfInstances[_iterator].rightdirection * (_player_rght_shldr[0][0]._total_torso_width));
                //shiftrightshoulderpos = shiftrightshoulderpos + (mainarmparentmeshobject._arrayOfInstances[_iterator].updirection * (_player_rght_shldr[0][0]._total_torso_height * 1f));

                shiftrightshoulderpos.X += OFFSETPOS.X;
                shiftrightshoulderpos.Y += OFFSETPOS.Y;
                shiftrightshoulderpos.Z += OFFSETPOS.Z;

                matrixerer.M41 = shiftrightshoulderpos.X;
                matrixerer.M42 = shiftrightshoulderpos.Y;
                matrixerer.M43 = shiftrightshoulderpos.Z;
                worldMatrix_instances_r_shoulder[0][0][_iterator] = matrixerer;
                _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
                */


                /*
                //RIGHT SHOULDER
                _rotMatrixer = _player_rght_shldr[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out otherQuat);
                var direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                _SC_modL_rght_shldr_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_rght_shldr[0][0]._POSITION.M41, _player_rght_shldr[0][0]._POSITION.M42, _player_rght_shldr[0][0]._POSITION.M43),
                    padding1 = 100
                };
                MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                var torsooripos = MOVINGPOINTER;
                _rotMatrixer = _player_rght_shldr[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                var direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                var direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                var direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                var diffNormPosX = (MOVINGPOINTER.X) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                var diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                var diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                var tempPoint = MOVINGPOINTER;
                tempPoint = tempPoint + -(direction_feet_right * (diffNormPosX));
                tempPoint = tempPoint + -(direction_feet_up * (diffNormPosY));
                tempPoint = tempPoint + -(direction_feet_forward * (diffNormPosZ));

                var dirToPoint = tempPoint - torsooripos;
                dirToPoint.Normalize();
                var realPosOfRS = TORSOPIVOT + (dirToPoint * ((ikvoxelbody._player_torso[0][0]._total_torso_height)));
                var pivotOfHead = TORSOPIVOT + (dirToPoint * ((ikvoxelbody._player_torso[0][0]._total_torso_height)));
                realPosOfRS.X += OFFSETPOS.X;
                realPosOfRS.Y += OFFSETPOS.Y;
                realPosOfRS.Z += OFFSETPOS.Z;

                var shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT;
                Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                var theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right, direction_head_up);
                theheadrotmatrix.Invert();
                Matrix matrixerer = theheadrotmatrix;
                realPosOfRS = realPosOfRS + (current_rotation_of_torso_pivot_right * (_player_rght_shldr[0][0]._total_torso_width));
                realPosOfRS = realPosOfRS + (-current_rotation_of_torso_pivot_up * (_player_rght_shldr[0][0]._total_torso_height * 1));
                matrixerer.M41 = realPosOfRS.X;
                matrixerer.M42 = realPosOfRS.Y;
                matrixerer.M43 = realPosOfRS.Z;
                worldMatrix_instances_r_shoulder[0][0][_iterator] = matrixerer;
                _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;*/









                /*
                //RIGHT SHOULDER
                _rotMatrixer = _player_rght_shldr[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out otherQuat);
                var direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                _SC_modL_rght_shldr_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_rght_shldr[0][0]._POSITION.M41, _player_rght_shldr[0][0]._POSITION.M42, _player_rght_shldr[0][0]._POSITION.M43),
                    padding1 = 100
                };
                MOVINGPOINTER = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                var torsooripos = MOVINGPOINTER;
                _rotMatrixer = _player_rght_shldr[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                var direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                var direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                var direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);

                var diffNormPosX = (MOVINGPOINTER.X) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                var diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                var diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;


                MOVINGPOINTER = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].rightdirection * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].updirection * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + (mainarmparentmeshobject._arrayOfInstances[_iterator].forwarddirection * (diffNormPosZ));

                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;


                /*var tempPoint = MOVINGPOINTER;

                //tempPoint = tempPoint + -(direction_feet_right * (diffNormPosX));
                tempPoint = tempPoint + -(direction_feet_up * (diffNormPosY));
                //tempPoint = tempPoint + -(direction_feet_forward * (diffNormPosZ));

                var dirToPoint = tempPoint - torsooripos;
                dirToPoint.Normalize();
                var realPosOfRS = TORSOPIVOT + (dirToPoint * ((ikvoxelbody._player_torso[0][0]._total_torso_height)));
                var pivotOfHead = TORSOPIVOT + (dirToPoint * ((ikvoxelbody._player_torso[0][0]._total_torso_height)));
                realPosOfRS.X += OFFSETPOS.X;
                realPosOfRS.Y += OFFSETPOS.Y;
                realPosOfRS.Z += OFFSETPOS.Z;

                var shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT;
                Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                var theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right, direction_head_up);
                theheadrotmatrix.Invert();

                Matrix matrixerer = finalRotationMatrix;


                //realPosOfRS = realPosOfRS + (current_rotation_of_torso_pivot_right * (_player_rght_shldr[0][0]._total_torso_width));
                //realPosOfRS = realPosOfRS + (-current_rotation_of_torso_pivot_up * (_player_rght_shldr[0][0]._total_torso_height * 1));

                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;

                worldMatrix_instances_r_shoulder[0][0][_iterator] = matrixerer;
                _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;*/







                /*
                _rotMatrixer = _player_rght_shldr[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                _SC_modL_rght_shldr_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_rght_shldr[0][0]._POSITION.M41, _player_rght_shldr[0][0]._POSITION.M42, _player_rght_shldr[0][0]._POSITION.M43),
                    padding1 = 100
                };
                MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                torsooripos = MOVINGPOINTER;
                _rotMatrixer = _player_rght_shldr[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);



                //FIRST SETUP OF DISTANCE FROM TORSO PIVOT UP TO WHERE THE SHOULDER SHOULD BE BASED ON ITS ORIGIN POSITION
                //FIRST SETUP OF DISTANCE FROM TORSO PIVOT UP TO WHERE THE SHOULDER SHOULD BE BASED ON ITS ORIGIN POSITION
                diffNormPosX = (MOVINGPOINTER.X) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                tempPoint = MOVINGPOINTER;
                tempPoint = tempPoint + -(direction_feet_right * (diffNormPosX));
                tempPoint = tempPoint + -(direction_feet_up * (diffNormPosY));
                tempPoint = tempPoint + -(direction_feet_forward * (diffNormPosZ));
                //FIRST SETUP OF DISTANCE FROM TORSO PIVOT UP TO WHERE THE SHOULDER SHOULD BE BASED ON ITS ORIGIN POSITION
                //FIRST SETUP OF DISTANCE FROM TORSO PIVOT UP TO WHERE THE SHOULDER SHOULD BE BASED ON ITS ORIGIN POSITION

                realPosOfRS = tempPoint;


                /*dirToPoint = tempPoint - torsooripos;
                dirToPoint.Normalize();
                realPosOfRS = TORSOPIVOT + (dirToPoint * ((ikvoxelbody._player_torso[0][0]._total_torso_height)));
                pivotOfHead = TORSOPIVOT + (dirToPoint * ((ikvoxelbody._player_torso[0][0]._total_torso_height)));
         

                shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT;
                Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right, direction_head_up);
                theheadrotmatrix.Invert();
                matrixerer = theheadrotmatrix;
                //realPosOfRS = realPosOfRS + (current_rotation_of_torso_pivot_right * (_player_rght_shldr[0][0]._total_torso_width));
                realPosOfRS = realPosOfRS + (-current_rotation_of_torso_pivot_up * (_player_rght_shldr[0][0]._total_torso_height * 4));


                matrixerer.M41 = realPosOfRS.X;
                matrixerer.M42 = realPosOfRS.Y;
                matrixerer.M43 = realPosOfRS.Z;
                worldMatrix_instances_r_shoulder[0][0][_iterator] = matrixerer;
                _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;*/






                /*
                //RIGHT SHOULDER
                _rotMatrixer = _player_rght_shldr[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out otherQuat);
                var direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                _SC_modL_rght_shldr_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_rght_shldr[0][0]._POSITION.M41, _player_rght_shldr[0][0]._POSITION.M42, _player_rght_shldr[0][0]._POSITION.M43),
                    padding1 = 100
                };
                MOVINGPOINTER = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                var torsooripos = MOVINGPOINTER;
                _rotMatrixer = _player_rght_shldr[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                var direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                var direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                var direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);

                var diffNormPosX = (MOVINGPOINTER.X) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                var diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                var diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;


                MOVINGPOINTER = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].rightdirection * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].updirection * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + (mainarmparentmeshobject._arrayOfInstances[_iterator].forwarddirection * (diffNormPosZ));

                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;


                var tempPoint = MOVINGPOINTER;

                //tempPoint = tempPoint + -(direction_feet_right * (diffNormPosX));
                tempPoint = tempPoint + -(direction_feet_up * (diffNormPosY));
                //tempPoint = tempPoint + -(direction_feet_forward * (diffNormPosZ));

                var dirToPoint = tempPoint - torsooripos;
                dirToPoint.Normalize();
                var realPosOfRS = TORSOPIVOT + (dirToPoint * ((ikvoxelbody._player_torso[0][0]._total_torso_height)));
                var pivotOfHead = TORSOPIVOT + (dirToPoint * ((ikvoxelbody._player_torso[0][0]._total_torso_height)));
                realPosOfRS.X += OFFSETPOS.X;
                realPosOfRS.Y += OFFSETPOS.Y;
                realPosOfRS.Z += OFFSETPOS.Z;

                var shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT;
                Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                var theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right, direction_head_up);
                theheadrotmatrix.Invert();

                Matrix matrixerer = finalRotationMatrix;


                //realPosOfRS = realPosOfRS + (current_rotation_of_torso_pivot_right * (_player_rght_shldr[0][0]._total_torso_width));
                //realPosOfRS = realPosOfRS + (-current_rotation_of_torso_pivot_up * (_player_rght_shldr[0][0]._total_torso_height * 1));

                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;

                worldMatrix_instances_r_shoulder[0][0][_iterator] = matrixerer;
                _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;*/











                //var hyp0 = (float)(diffNormPosY / Math.Cos(pitch0));





                //RIGHT SHOULDER
                _rotMatrixer = _player_rght_shldr[0][0]._ORIGINPOSITION;





                /*Quaternion.RotationMatrix(ref _rotMatrixer, out otherQuat);
                var direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);*/
                _SC_modL_rght_shldr_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_rght_shldr[0][0]._POSITION.M41, _player_rght_shldr[0][0]._POSITION.M42, _player_rght_shldr[0][0]._POSITION.M43),
                    padding1 = 100
                };
                MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                var torsooripos = MOVINGPOINTER;
                _rotMatrixer = ikvoxelbody._player_torso[0][0]._arrayOfInstances[_iterator].current_pos;// _player_rght_shldr[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                var direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                var direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                var direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                var diffNormPosX = (MOVINGPOINTER.X) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                var diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                var diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                var tempPoint = MOVINGPOINTER;

                

                ///////////
                //SOMETESTS

                Quaternion.RotationMatrix(ref finalRotationMatrix, out otherQuat);
                direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                //current_rotation_of_torso_pivot_forward = direction_feet_forward;
                //current_rotation_of_torso_pivot_right = direction_feet_right;
                //current_rotation_of_torso_pivot_up = direction_feet_up;
                //SOMETESTS
                ///////////




                tempPoint = tempPoint + (direction_feet_up * ikvoxelbody._player_torso[0][0]._total_torso_height*0.5f);

                //tempPoint = tempPoint + -(direction_feet_right * ((ikvoxelbody._player_torso[0][0]._total_torso_width + (_player_rght_shldr[0][0]._total_torso_width*4)))); // (diffNormPosX)
                //tempPoint = tempPoint + -(direction_feet_forward * (diffNormPosZ));












                /*
                if (somechunkpriminstanceikarmvoxelindex == 0)
                {
                    finalRotationMatrix.M41 = 0;
                    finalRotationMatrix.M42 = 0;
                    finalRotationMatrix.M43 = 0;

                    _rotMatrixer = finalRotationMatrix;

                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {
                    extramatrix.M41 = 0;
                    extramatrix.M42 = 0;
                    extramatrix.M43 = 0;
                    _rotMatrixer = extramatrix * hmdrotMatrix;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {
                    extramatrix.M41 = 0;
                    extramatrix.M42 = 0;
                    extramatrix.M43 = 0;
                    _rotMatrixer = extramatrix * hmdrotMatrix;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)
                {
                    finalRotationMatrix.M41 = 0;
                    finalRotationMatrix.M42 = 0;
                    finalRotationMatrix.M43 = 0;

                    _rotMatrixer = finalRotationMatrix;

                }



                */


                //Quaternion otherQuat;
                Quaternion.RotationMatrix(ref finalRotationMatrix, out otherQuat);

                //CONVERTING THE QUATERNION OF THAT TO THE DIRECTION OF ITS ROTATION
                var direction_feet_forward_torso = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right_torso = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_feet_up_torso = sc_maths._getDirection(Vector3.Up, otherQuat);

                //I AM CALCULATING THE DIFFERENCE IN THE MOVEMENT FROM THE ORIGINAL POSITION TO THE CURRENT OFFSET AT THE BOTTOM OF THE SPINE WHERE I MOVED THAT POINT.
                //diffNormPosX = (MOVINGPOINTER.X) - _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41; //_player_torso[0][0]._ORIGINPOSITION.M41;
                //diffNormPosY = (MOVINGPOINTER.Y) - _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42; //_player_torso[0][0]._ORIGINPOSITION.M42;
                //diffNormPosZ = (MOVINGPOINTER.Z) - _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43; //_player_torso[0][0]._ORIGINPOSITION.M43;

                /*//I AM USING THE NEW PIVOT POINT AT THE BOTTOM OF THE SPINE AND ADDING THE FRONT/RIGHT/UP VECTOR OF THE ROTATION OF THAT SPINE AND THEN ADDING THE DIFFERENCE X/W/Z BETWEEN ORIGINAL POS AND THE NEW PIVOT POS
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right_torso * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_torso * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward_torso * (diffNormPosZ));
                */



                var heightofhead = ikvoxelbody._player_head[0][0]._total_torso_height * 1.0f;
                var heightOftorso = ikvoxelbody._player_torso[0][0]._total_torso_height * 1.0f;
                var heightofpelvis = ikvoxelbody._player_pelvis[0][0]._total_torso_height * 1.0f;

                var widthofhead = ikvoxelbody._player_head[0][0]._total_torso_width * 1.0f;
                var widthoftorso = ikvoxelbody._player_torso[0][0]._total_torso_width * 1.0f;
                var widthofpelvis = ikvoxelbody._player_pelvis[0][0]._total_torso_width * 1.0f;

                var depthofhead = ikvoxelbody._player_head[0][0]._total_torso_depth * 1.0f;
                var depthoftorso = ikvoxelbody._player_torso[0][0]._total_torso_depth * 1.0f;
                var depthofpelvis = ikvoxelbody._player_pelvis[0][0]._total_torso_depth * 1.0f;

                var shoulderwidth = _player_rght_shldr[0][0]._total_torso_width * 1.0f;
                var shoulderheight = _player_rght_shldr[0][0]._total_torso_height * 1.0f;
                var shoulderdepth = _player_rght_shldr[0][0]._total_torso_depth * 1.0f;







                Quaternion.RotationMatrix(ref finalRotationMatrix, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);

                var dirToPoint = tempPoint - torsooripos;
                dirToPoint.Normalize();



                //MOVINGPOINTER = MOVINGPOINTER + (dirToPoint * ((ikvoxelbody._player_torso[0][0]._total_torso_height)));
                //var pivotOfHead = TORSOPIVOT + (dirToPoint * ((ikvoxelbody._player_torso[0][0]._total_torso_height)));

                //MOVINGPOINTER = new Vector3(ikvoxelbody._player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, ikvoxelbody._player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42 - (heightOftorso * 1), ikvoxelbody._player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                //MOVINGPOINTER = new Vector3(_player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42 - (-direction_feet_up_torso.Y * (heightOftorso * 0.5f)) - (-direction_feet_up_torso.Y * (heightofhead * 0.5f)), _player_head[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                //- (heightofhead * 0.5f) - (heightOftorso * 0.5f)

                //MOVINGPOINTER.Y = MOVINGPOINTER.Y + (-direction_feet_up_torso.Y * (heightofhead * 0.5f));
                //MOVINGPOINTER.Y = MOVINGPOINTER.Y + (-direction_feet_up_torso.Y * (heightOftorso * 0.5f));
                //MOVINGPOINTER.Y = MOVINGPOINTER.Y + (-direction_feet_up_torso.Y * (heightofpelvis * 0.5f));

                //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_up_torso * (heightofhead));
                /*MOVINGPOINTER = MOVINGPOINTER + (direction_feet_up_torso * (heightOftorso * 1));



                if (somechunkpriminstanceikarmvoxelindex == 0)
                {
                    //MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_torso * (heightOftorso));
                    //MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_right_ori * (widthoftorso));
                    //MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_right_ori * (shoulderwidth));

                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {

                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {

                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)
                {
                    //MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_torso * (heightOftorso));
                    MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_right_ori * (widthoftorso));
                    MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_right_ori * (shoulderwidth));

                }*/

                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;

                var shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT;
                Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                var theshouldermatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right, direction_head_up);
                theshouldermatrix.Invert();

                matrixerer = theshouldermatrix;
                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;


                //realPosOfRS.X += OFFSETPOS.X;
                //realPosOfRS.Y += OFFSETPOS.Y;
                //realPosOfRS.Z += OFFSETPOS.Z;
                /*
                if (somechunkpriminstanceikarmvoxelindex == 0)
                {
                    realPosOfRS = realPosOfRS + (-direction_feet_right_ori * (_player_rght_shldr[0][0]._total_torso_width * 8));
                    var shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT;
                    Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                    direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                    direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                    var theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right, direction_head_up);
                    theheadrotmatrix.Invert();


                    /*Quaternion.RotationMatrix(ref theheadrotmatrix, out otherQuat);
                    var xqu = otherQuat.X;
                    var yqu = otherQuat.Y;
                    var zqu = otherQuat.Z;
                    var wqu = otherQuat.W;

                    var pitch0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);
                    var yaw0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);
                    var roll0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);

                    var pitchdegree = (float)(pitch0 * (180.0f / Math.PI));// (float)((Math.PI * pitch0) / 180);

                    if (pitchdegree < -90)
                    {
                        pitchdegree = -90;
                    }
                    else if (pitchdegree > 90)
                    {
                        pitchdegree = 90;
                    }
                    else
                    {

                    }

                    pitchdegree = (float)((Math.PI * pitchdegree) / 180);

                    theheadrotmatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw0, pitchdegree, roll0);



                    matrixerer = theheadrotmatrix;



                    //realPosOfRS += tempOffset;
                    realPosOfRS += OFFSETPOS;



                    //realPosOfRS = realPosOfRS + (current_rotation_of_torso_pivot_right * (_player_rght_shldr[0][0]._total_torso_width));
                    //realPosOfRS = realPosOfRS + (-current_rotation_of_torso_pivot_up * (_player_rght_shldr[0][0]._total_torso_height * 1));
                    matrixerer.M41 = realPosOfRS.X;
                    matrixerer.M42 = realPosOfRS.Y;
                    matrixerer.M43 = realPosOfRS.Z;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {
                    MOVINGPOINTER = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                    realPosOfRS = MOVINGPOINTER + (direction_feet_right_ori * (ikvoxelbody._player_pelvis[0][0]._total_torso_width));
                    realPosOfRS = realPosOfRS + (-direction_feet_up_ori * ikvoxelbody._player_pelvis[0][0]._total_torso_height * 4);
                    //realPosOfRS = realPosOfRS + (direction_feet_up_ori * -((ikvoxelbody._player_torso[0][0]._total_torso_height*2) + (ikvoxelbody._player_pelvis[0][0]._total_torso_height)));
                    matrixerer = matrixerer = extramatrix;// ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[0].current_pos;  //matrixerer = extramatrix;

                    matrixerer.M41 = realPosOfRS.X;
                    matrixerer.M42 = realPosOfRS.Y;
                    matrixerer.M43 = realPosOfRS.Z;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {
                    MOVINGPOINTER = new Vector3(ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M41, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M42, ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos.M43);

                    realPosOfRS = MOVINGPOINTER + (-direction_feet_right_ori * (ikvoxelbody._player_pelvis[0][0]._total_torso_width));
                    realPosOfRS = realPosOfRS + (-direction_feet_up_ori * ikvoxelbody._player_pelvis[0][0]._total_torso_height * 4);
                    //realPosOfRS = realPosOfRS + (direction_feet_up_ori * -((ikvoxelbody._player_torso[0][0]._total_torso_height*2) + (ikvoxelbody._player_pelvis[0][0]._total_torso_height)));
                    matrixerer = matrixerer = extramatrix;// ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[0].current_pos;  //matrixerer = extramatrix;

                    matrixerer.M41 = realPosOfRS.X;
                    matrixerer.M42 = realPosOfRS.Y;
                    matrixerer.M43 = realPosOfRS.Z;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)
                {
                    realPosOfRS = realPosOfRS + (direction_feet_right_ori * (_player_rght_shldr[0][0]._total_torso_width * 8));
                    var shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT;
                    Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                    direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                    direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                    var theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right, direction_head_up);
                    theheadrotmatrix.Invert();



                    /*Quaternion.RotationMatrix(ref theheadrotmatrix, out otherQuat);
                    var xqu = otherQuat.X;
                    var yqu = otherQuat.Y;
                    var zqu = otherQuat.Z;
                    var wqu = otherQuat.W;

                    var pitch0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);
                    var yaw0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);
                    var roll0 = (float)Math.Atan2(2 * yqu * wqu - 2 * xqu * zqu, 1 - 2 * yqu * yqu - 2 * zqu * zqu);

                    var pitchdegree = (float)(pitch0 * (180.0f / Math.PI));// (float)((Math.PI * pitch0) / 180);

                    if (pitchdegree < -90)
                    {
                        pitchdegree = -90;
                    }
                    else if (pitchdegree > 90)
                    {
                        pitchdegree = 90;
                    }
                    else
                    {

                    }

                    pitchdegree = (float)((Math.PI * pitchdegree) / 180);

                    theheadrotmatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw0, pitchdegree, roll0);


                    matrixerer = theheadrotmatrix;

                    //realPosOfRS += tempOffset;
                    realPosOfRS += OFFSETPOS;



                    //realPosOfRS = realPosOfRS + (current_rotation_of_torso_pivot_right * (_player_rght_shldr[0][0]._total_torso_width));
                    //realPosOfRS = realPosOfRS + (-current_rotation_of_torso_pivot_up * (_player_rght_shldr[0][0]._total_torso_height * 1));
                    matrixerer.M41 = realPosOfRS.X;
                    matrixerer.M42 = realPosOfRS.Y;
                    matrixerer.M43 = realPosOfRS.Z;
                }*/



                worldMatrix_instances_r_shoulder[0][0][_iterator] = matrixerer;
                _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;























                //START OF RIGHT ARM IK
                //START OF RIGHT ARM IK
                //START OF RIGHT ARM IK
                /////////////////////
                //////HANDRIGHT//////
                MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M43);


                Matrix someMatRight = Matrix.Identity;

                if (somechunkpriminstanceikarmvoxelindex == 0)
                {
                    someMatRight = _leftTouchMatrix;
                    someMatRight.M41 = handPoseLeft.Position.X + MOVINGPOINTER.X;
                    someMatRight.M42 = handPoseLeft.Position.Y;// + MOVINGPOINTER.Y;
                    someMatRight.M43 = handPoseLeft.Position.Z + MOVINGPOINTER.Z;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {
                    someMatRight = extramatrix;
                    someMatRight.M41 = _player_rght_shldr[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M41 + MOVINGPOINTER.X;
                    someMatRight.M42 = _player_rght_shldr[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42 + MOVINGPOINTER.Y + _player_rght_hnd[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42;
                    someMatRight.M43 = _player_rght_shldr[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M43 + MOVINGPOINTER.Z;

                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {
                    someMatRight = extramatrix;
                    someMatRight.M41 = _player_rght_shldr[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M41 + MOVINGPOINTER.X;
                    someMatRight.M42 = _player_rght_shldr[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42 + MOVINGPOINTER.Y + _player_rght_hnd[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42;
                    someMatRight.M43 = _player_rght_shldr[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M43 + MOVINGPOINTER.Z;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)
                {
                    someMatRight = _rightTouchMatrix;
                    someMatRight.M41 = handPoseRight.Position.X + MOVINGPOINTER.X;
                    someMatRight.M42 = handPoseRight.Position.Y;// + MOVINGPOINTER.Y;
                    someMatRight.M43 = handPoseRight.Position.Z + MOVINGPOINTER.Z;

                }






                diffNormPosX = (MOVINGPOINTER.X) - someMatRight.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - someMatRight.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - someMatRight.M43;

                MOVINGPOINTER = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].rightdirection * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].updirection * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + (mainarmparentmeshobject._arrayOfInstances[_iterator].forwarddirection * (diffNormPosZ));



                Vector3 somegrabposition = MOVINGPOINTER + (mainarmparentmeshobject._arrayOfInstances[_iterator].forwarddirection * (diffNormPosZ * 0.5f));

                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;

                var posRHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M43);
                Vector3 tempDir = posRHand - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                if (tempDir.Length() > lengthOfLowerArmRight * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
                {
                    tempDir.Normalize();
                    var somePosOfSHLDR = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                    Vector3 tempVect = somePosOfSHLDR + (tempDir * ((lengthOfLowerArmRight * 1.0923f) + (lengthOfUpperArmRight * 1.0923f)));
                    MOVINGPOINTER.X = tempVect.X;
                    MOVINGPOINTER.Y = tempVect.Y;
                    MOVINGPOINTER.Z = tempVect.Z;
                }



                if (somechunkpriminstanceikarmvoxelindex == 0)
                {
                    matrixerer = someMatRight * finalRotationMatrix;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {
                    matrixerer = finalRotationMatrix;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {
                    matrixerer = finalRotationMatrix;
                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)
                {
                    matrixerer = someMatRight * finalRotationMatrix;
                }












                someMatRight.M41 += OFFSETPOS.X;
                someMatRight.M42 += OFFSETPOS.Y;
                someMatRight.M43 += OFFSETPOS.Z;

                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;
                matrixerer.M44 = 1;

                Vector3 realposhand0 = MOVINGPOINTER;

                worldMatrix_instances_r_hand[0][0][_iterator] = matrixerer;// _player_pelvis[0][0].current_pos;// translationMatrix;
                _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
                _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION = matrixerer;
                _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._REALCENTERPOSITION = someMatRight;
                _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._TEMPPOSITION = someMatRight;
                //if (swtch_for_last_pos[0][0][_iterator] > 0)
                //{
                //    _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITIONFORPHYSICS = matrixerer;
                //}











                /////////////////////////
                //////HANDRIGHTGRAB//////
                MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M43);

                if (somechunkpriminstanceikarmvoxelindex == 0)
                {
                    someMatRight = _leftTouchMatrix;
                    someMatRight.M41 = handPoseLeft.Position.X + MOVINGPOINTER.X;
                    someMatRight.M42 = handPoseLeft.Position.Y;// + MOVINGPOINTER.Y;
                    someMatRight.M43 = handPoseLeft.Position.Z + MOVINGPOINTER.Z;



                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {
                    someMatRight = finalRotationMatrix;
                    someMatRight.M41 = _player_rght_shldr[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M41 + MOVINGPOINTER.X;
                    someMatRight.M42 = _player_rght_shldr[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42 + MOVINGPOINTER.Y + _player_rght_hnd[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42;
                    someMatRight.M43 = _player_rght_shldr[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M43 + MOVINGPOINTER.Z;

                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {


                    someMatRight = finalRotationMatrix;
                    someMatRight.M41 = _player_rght_shldr[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M41 + MOVINGPOINTER.X;
                    someMatRight.M42 = _player_rght_shldr[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42 + MOVINGPOINTER.Y + _player_rght_hnd[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42;
                    someMatRight.M43 = _player_rght_shldr[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M43 + MOVINGPOINTER.Z;



                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)
                {

                    someMatRight = _rightTouchMatrix;
                    someMatRight.M41 = handPoseRight.Position.X + MOVINGPOINTER.X;
                    someMatRight.M42 = handPoseRight.Position.Y;// + MOVINGPOINTER.Y;
                    someMatRight.M43 = handPoseRight.Position.Z + MOVINGPOINTER.Z;


                }











                /*
                someMatRight = _rightTouchMatrix;
                someMatRight.M41 = handPoseRight.Position.X + MOVINGPOINTER.X;
                someMatRight.M42 = handPoseRight.Position.Y; //+ MOVINGPOINTER.Y;
                someMatRight.M43 = handPoseRight.Position.Z + MOVINGPOINTER.Z;
                **/

                diffNormPosX = (MOVINGPOINTER.X) - someMatRight.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - someMatRight.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - someMatRight.M43;

                MOVINGPOINTER = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].rightdirection * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].updirection * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + (mainarmparentmeshobject._arrayOfInstances[_iterator].forwarddirection * (diffNormPosZ));





                //MOVINGPOINTER = MOVINGPOINTER + (_player_r_hand_grab[0][0]._arrayOfInstances[_iterator].forwarddirection * (diffNormPosZ));


                if (somechunkpriminstanceikarmvoxelindex == 0)
                {

                    //realPosOfRS = realPosOfRS + (direction_feet_right_ori * (_player_rght_shldr[0][0]._total_torso_width * 8));
                    //var shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT;

                    var somehandmatrix = someMatRight * finalRotationMatrix;
                    Quaternion.RotationMatrix(ref somehandmatrix, out otherQuat);
                    var direction_head_forward0 = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    var direction_head_right0 = sc_maths._getDirection(Vector3.Right, otherQuat);
                    var direction_head_up0 = sc_maths._getDirection(Vector3.Up, otherQuat);

                    var theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right0, direction_head_up0);
                    theheadrotmatrix.Invert();

                    Quaternion.RotationMatrix(ref theheadrotmatrix, out otherQuat);
                    direction_head_forward0 = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    direction_head_right0 = sc_maths._getDirection(Vector3.Right, otherQuat);
                    direction_head_up0 = sc_maths._getDirection(Vector3.Up, otherQuat);


                    posRHand = MOVINGPOINTER;// new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                    var lengthOfHandheight = _player_rght_hnd[0][0]._total_torso_height * 2f;
                    var lengthOfHandgrabheight = _player_r_hand_grab[0][0]._total_torso_height * 2f;

                    Vector3 somevec0 = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);

                    Vector3 tempVect = (posRHand) + (direction_head_forward0 * ((lengthOfHandheight * 1.0923f)));
                    tempVect = (tempVect) + (direction_head_up0 * ((lengthOfHandgrabheight * 1.0923f)));





                    //posRHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M43);

                    //tempDir = posRHand - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;
                    MOVINGPOINTER = tempVect;// _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION - OFFSETPOS;// MOVINGPOINTER + (tempDir * 2);
                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {

                    //realPosOfRS = realPosOfRS + (direction_feet_right_ori * (_player_rght_shldr[0][0]._total_torso_width * 8));
                    //var shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT;

                    var somehandmatrix = someMatRight * finalRotationMatrix;
                    Quaternion.RotationMatrix(ref somehandmatrix, out otherQuat);
                    var direction_head_forward0 = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    var direction_head_right0 = sc_maths._getDirection(Vector3.Right, otherQuat);
                    var direction_head_up0 = sc_maths._getDirection(Vector3.Up, otherQuat);

                    var theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right0, direction_head_up0);
                    theheadrotmatrix.Invert();

                    Quaternion.RotationMatrix(ref theheadrotmatrix, out otherQuat);
                    direction_head_forward0 = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    direction_head_right0 = sc_maths._getDirection(Vector3.Right, otherQuat);
                    direction_head_up0 = sc_maths._getDirection(Vector3.Up, otherQuat);


                    posRHand = MOVINGPOINTER;// new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                    var lengthOfHandheight = _player_rght_hnd[0][0]._total_torso_height * 2f;
                    var lengthOfHandgrabheight = _player_r_hand_grab[0][0]._total_torso_height * 2f;

                    Vector3 somevec0 = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);

                    Vector3 tempVect = (posRHand) + (direction_head_forward0 * ((-lengthOfHandheight * 1.0923f)));
                    tempVect = (tempVect) + (direction_head_up0 * ((lengthOfHandgrabheight * 1.0923f)));





                    //posRHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M43);

                    //tempDir = posRHand - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;
                    MOVINGPOINTER = tempVect;// _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION - OFFSETPOS;// MOVINGPOINTER + (tempDir * 2);
                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {

                    //realPosOfRS = realPosOfRS + (direction_feet_right_ori * (_player_rght_shldr[0][0]._total_torso_width * 8));
                    //var shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT;

                    var somehandmatrix = someMatRight * finalRotationMatrix;
                    Quaternion.RotationMatrix(ref somehandmatrix, out otherQuat);
                    var direction_head_forward0 = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    var direction_head_right0 = sc_maths._getDirection(Vector3.Right, otherQuat);
                    var direction_head_up0 = sc_maths._getDirection(Vector3.Up, otherQuat);

                    var theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right0, direction_head_up0);
                    theheadrotmatrix.Invert();

                    Quaternion.RotationMatrix(ref theheadrotmatrix, out otherQuat);
                    direction_head_forward0 = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    direction_head_right0 = sc_maths._getDirection(Vector3.Right, otherQuat);
                    direction_head_up0 = sc_maths._getDirection(Vector3.Up, otherQuat);


                    posRHand = MOVINGPOINTER;// new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                    var lengthOfHandheight = _player_rght_hnd[0][0]._total_torso_height * 2f;
                    var lengthOfHandgrabheight = _player_r_hand_grab[0][0]._total_torso_height * 2f;

                    Vector3 somevec0 = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);

                    Vector3 tempVect = (posRHand) + (direction_head_forward0 * ((-lengthOfHandheight * 1.0923f)));
                    tempVect = (tempVect) + (direction_head_up0 * ((lengthOfHandgrabheight * 1.0923f)));





                    //posRHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M43);

                    //tempDir = posRHand - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;
                    MOVINGPOINTER = tempVect;// _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION - OFFSETPOS;// MOVINGPOINTER + (tempDir * 2);
                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)
                {


                    //realPosOfRS = realPosOfRS + (direction_feet_right_ori * (_player_rght_shldr[0][0]._total_torso_width * 8));
                    //var shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT;

                    var somehandmatrix = someMatRight * finalRotationMatrix;
                    Quaternion.RotationMatrix(ref somehandmatrix, out otherQuat);
                    var direction_head_forward0 = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    var direction_head_right0 = sc_maths._getDirection(Vector3.Right, otherQuat);
                    var direction_head_up0 = sc_maths._getDirection(Vector3.Up, otherQuat);

                    var theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right0, direction_head_up0);
                    theheadrotmatrix.Invert();

                    Quaternion.RotationMatrix(ref theheadrotmatrix, out otherQuat);
                    direction_head_forward0 = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    direction_head_right0 = sc_maths._getDirection(Vector3.Right, otherQuat);
                    direction_head_up0 = sc_maths._getDirection(Vector3.Up, otherQuat);


                    posRHand = MOVINGPOINTER;// new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                    var lengthOfHandwidth = _player_rght_hnd[0][0]._total_torso_depth * 2f;
                    var lengthOfHandheight = _player_rght_hnd[0][0]._total_torso_height * 2f;
                    var lengthOfHandgrabheight = _player_r_hand_grab[0][0]._total_torso_height * 2f;

                    Vector3 somevec0 = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);

                    Vector3 tempVect = (posRHand) + (direction_head_forward0 * ((-lengthOfHandheight * 1.0923f)));
                    tempVect = (tempVect) + (direction_head_up0 * ((lengthOfHandgrabheight * 1.0923f)));
                    tempVect = (tempVect) + (-direction_head_right0 * ((lengthOfHandwidth * 1.0923f)));




                    //posRHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M43);

                    //tempDir = posRHand - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;
                    MOVINGPOINTER = tempVect;// _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION - OFFSETPOS;// MOVINGPOINTER + (tempDir * 2);
                }

                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;



                //TRYING TO MOVE IK OF HAND GRAB FAIL EXPERIMENT
                //TRYING TO MOVE IK OF HAND GRAB FAIL EXPERIMENT
                //TRYING TO MOVE IK OF HAND GRAB FAIL EXPERIMENT
                if (somechunkpriminstanceikarmvoxelindex == 0)
                {
                    /*posRHand = new Vector3(_player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M41, _player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M42, _player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M43);

                    tempDir = posRHand - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                    if (tempDir.Length() > lengthOfLowerArmRight * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
                    {
                        tempDir.Normalize();
                        var somePosOfSHLDR = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                        Vector3 tempVect = somePosOfSHLDR + (tempDir * ((lengthOfLowerArmRight * 1.0923f) + (lengthOfUpperArmRight * 1.0923f)));
                        MOVINGPOINTER.X = tempVect.X;
                        MOVINGPOINTER.Y = tempVect.Y;
                        MOVINGPOINTER.Z = tempVect.Z;
                    }*/

                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {
                    posRHand = new Vector3(_player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M41, _player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M42, _player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M43);

                    tempDir = posRHand - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                    if (tempDir.Length() > lengthOfLowerArmRight * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
                    {
                        tempDir.Normalize();
                        var somePosOfSHLDR = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                        Vector3 tempVect = somePosOfSHLDR + (tempDir * ((lengthOfLowerArmRight * 1.0923f) + (lengthOfUpperArmRight * 1.0923f)));
                        MOVINGPOINTER.X = tempVect.X;
                        MOVINGPOINTER.Y = tempVect.Y;
                        MOVINGPOINTER.Z = tempVect.Z;
                    }

                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {
                    posRHand = new Vector3(_player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M41, _player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M42, _player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M43);

                    tempDir = posRHand - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                    if (tempDir.Length() > lengthOfLowerArmRight * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
                    {
                        tempDir.Normalize();
                        var somePosOfSHLDR = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                        Vector3 tempVect = somePosOfSHLDR + (tempDir * ((lengthOfLowerArmRight * 1.0923f) + (lengthOfUpperArmRight * 1.0923f)));
                        MOVINGPOINTER.X = tempVect.X;
                        MOVINGPOINTER.Y = tempVect.Y;
                        MOVINGPOINTER.Z = tempVect.Z;
                    }

                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)
                {
                    /*posRHand = new Vector3(_player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M41, _player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M42, _player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M43);

                    tempDir = posRHand - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                    if (tempDir.Length() > lengthOfLowerArmRight * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
                    {
                        tempDir.Normalize();
                        var somePosOfSHLDR = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                        Vector3 tempVect = somePosOfSHLDR + (tempDir * ((lengthOfLowerArmRight * 1.0923f) + (lengthOfUpperArmRight * 1.0923f)));
                        MOVINGPOINTER.X = tempVect.X;
                        MOVINGPOINTER.Y = tempVect.Y;
                        MOVINGPOINTER.Z = tempVect.Z;
                    }
                    */
                    /*posRHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M43);

                    tempDir = posRHand - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                    /*if (tempDir.Length() > lengthOfLowerArmRight * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
                    {
                        tempDir.Normalize();
                        var somePosOfSHLDR = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                        Vector3 tempVect = somePosOfSHLDR + (tempDir * ((lengthOfLowerArmRight * 1.0923f) + (lengthOfUpperArmRight * 1.0923f)));
                        MOVINGPOINTER.X = tempVect.X;
                        MOVINGPOINTER.Y = tempVect.Y;
                        MOVINGPOINTER.Z = tempVect.Z;
                    }
                    
                    tempDir.Normalize();
                    var somePosOfSHLDR = realposhand0;// new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                    Vector3 tempVect = somePosOfSHLDR + (tempDir * 2 *lengthOfHandRight * 1.0923f);
                    MOVINGPOINTER.X = tempVect.X;
                    MOVINGPOINTER.Y = tempVect.Y;
                    MOVINGPOINTER.Z = tempVect.Z;*/
                }
                //TRYING TO MOVE IK OF HAND GRAB FAIL EXPERIMENT
                //TRYING TO MOVE IK OF HAND GRAB FAIL EXPERIMENT
                //TRYING TO MOVE IK OF HAND GRAB FAIL EXPERIMENT




                /**/

                matrixerer = someMatRight * finalRotationMatrix;




                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;
                matrixerer.M44 = 1;

                worldMatrix_instances_r_hand_grab[0][0][_iterator] = matrixerer;// _player_pelvis[0][0].current_pos;// translationMatrix;
                _player_r_hand_grab[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
                _player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION = matrixerer;
                _player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._REALCENTERPOSITION = someMatRight;
                _player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._TEMPPOSITION = someMatRight;
















                //////////////////////
                //ELBOW TARGET RIGHT
                MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M43);
                TORSOPIVOT = MOVINGPOINTER;
                _rotMatrixer = _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_rght_elbow_target[0][0]._total_torso_height * 0.5f));
                diffNormPosX = (MOVINGPOINTER.X) - _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                MOVINGPOINTER = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].rightdirection * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].updirection * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].forwarddirection * (diffNormPosZ));
                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;
                var someDiffX = MOVINGPOINTER.X - _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                var someDiffY = MOVINGPOINTER.Y - _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                var someDiffZ = MOVINGPOINTER.Z - _player_rght_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                var somePosOfPivotUpperArm = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43); //new Vector3(realPIVOTOfUpperArm.X, realPIVOTOfUpperArm.Y, realPIVOTOfUpperArm.Z); ;// new Vector3(_player_rght_shldr.current_pos.M41, _player_rght_shldr.current_pos.M42, _player_rght_shldr.current_pos.M43);
                var somePosOfRightHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT = somePosOfPivotUpperArm;
                var dirShoulderToHand = somePosOfRightHand - somePosOfPivotUpperArm;
                dirShoulderToHand *= -1;
                MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * 3.0f);
                //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_right_ori * 3.0f);


                var someNewPointer = MOVINGPOINTER;




                //NOTES: CLAMP THE ROTATIONS OR SOMETHING IN ORDER TO STOP THE IK FROM GOING INTO THE GIMBAL LOCK SINGULARITY WHATEVER.
                //NOTES: CLAMP THE ROTATIONS OR SOMETHING IN ORDER TO STOP THE IK FROM GOING INTO THE GIMBAL LOCK SINGULARITY WHATEVER.






                if (somechunkpriminstanceikarmvoxelindex == 0)
                {
                    Vector3 dirtohandinvertedx = dirShoulderToHand;
                    dirtohandinvertedx.X *= -1;
                    dirtohandinvertedx.Z *= -1;

                    someNewPointer = (_player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT) + (dirtohandinvertedx * totalArmLengthRight * 2);
                    someNewPointer = someNewPointer + (direction_feet_right_ori * totalArmLengthRight * 2);
                    //someNewPointer = someNewPointer + (direction_feet_up_ori * totalArmLengthRight * 4);
                    someNewPointer = someNewPointer + (direction_feet_up_ori * totalArmLengthRight * 2);
                    someNewPointer = someNewPointer + (direction_feet_forward_ori * totalArmLengthRight * 2);
                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {
                    someNewPointer = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT + (-direction_feet_right_ori * totalArmLengthRight * 2);

                    //someNewPointer = someNewPointer + (direction_feet_up_ori * totalArmLengthRight * 4);
                    someNewPointer = someNewPointer + (-direction_feet_up_ori * totalArmLengthRight * 2);
                    someNewPointer = someNewPointer + (direction_feet_forward_ori * totalArmLengthRight * 2);

                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {
                    someNewPointer = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT + (direction_feet_right_ori * totalArmLengthRight * 2);
                    //someNewPointer = someNewPointer + (direction_feet_up_ori * totalArmLengthRight * 4);
                    someNewPointer = someNewPointer + (-direction_feet_up_ori * totalArmLengthRight * 2);
                    someNewPointer = someNewPointer + (direction_feet_forward_ori * totalArmLengthRight * 2);

                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)
                {
                    Vector3 dirtohandinvertedx = dirShoulderToHand;
                    dirtohandinvertedx.X *= -1;
                    dirtohandinvertedx.Z *= -1;

                    someNewPointer = (_player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT) + (dirtohandinvertedx * totalArmLengthRight * 2);
                    someNewPointer = someNewPointer + (-direction_feet_right_ori * totalArmLengthRight * 2);
                    //someNewPointer = someNewPointer + (direction_feet_up_ori * totalArmLengthRight * 4);
                    someNewPointer = someNewPointer + (direction_feet_up_ori * totalArmLengthRight * 2);
                    someNewPointer = someNewPointer + (direction_feet_forward_ori * totalArmLengthRight * 2);

                }





                /*var diffNormPosXElbowRight = (_player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M41) - (TORSOPIVOT.X);
                var diffNormPosYElbowRight = (_player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M42) - (TORSOPIVOT.Y);
                var diffNormPosZElbowRight = (_player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M43) - (TORSOPIVOT.Z);
                MOVINGPOINTER = TORSOPIVOT.X + -(mainarmparentmeshobject._arrayOfInstances[_iterator].rightdirection * (diffNormPosXElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Y + -(mainarmparentmeshobject._arrayOfInstances[_iterator].updirection * (diffNormPosYElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Z + -(mainarmparentmeshobject._arrayOfInstances[_iterator].forwarddirection * (diffNormPosZElbowRight));
                */

                matrixerer = finalRotationMatrix;
                matrixerer.M41 = someNewPointer.X;
                matrixerer.M42 = someNewPointer.Y;
                matrixerer.M43 = someNewPointer.Z;
                matrixerer.M44 = 1;
                worldMatrix_instances_r_elbow_target[0][0][_iterator] = matrixerer;
                _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
















                //////////////////////////
                //ELBOW TARGET RIGHT TWO
                MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M43);
                TORSOPIVOT = MOVINGPOINTER;


                //_rotMatrixer = ikvoxelbody._player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos;
                //_rotMatrixer = _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                _rotMatrixer = _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);

                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_rght_elbow_target_two[0][0]._total_torso_height * 0.5f));

                diffNormPosX = (MOVINGPOINTER.X) - _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M43;

                MOVINGPOINTER = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].rightdirection * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].updirection * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + -(mainarmparentmeshobject._arrayOfInstances[_iterator].forwarddirection * (diffNormPosZ));

                var xq = otherQuat.X;
                var yq = otherQuat.Y;
                var zq = otherQuat.Z;
                var wq = otherQuat.W;

                var pitcha = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
                var yawa = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
                var rolla = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
                var hyp = (float)(diffNormPosY / Math.Cos(pitcha));

                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;




                /*someDiffX = MOVINGPOINTER.X - _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41;
                someDiffY = MOVINGPOINTER.Y - _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42;
                someDiffZ = MOVINGPOINTER.Z - _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43;
                somePosOfRightHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                dirShoulderToHand = somePosOfRightHand - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT;
                dirShoulderToHand.Normalize();
                */
                //float initz = MOVINGPOINTER.Z;


                if (somechunkpriminstanceikarmvoxelindex == 0)
                {
                    /*somenewtargetlocation = MOVINGPOINTER + (dirShoulderToHand * totalArmLengthRight * 2);
                    somenewtargetlocation = somenewtargetlocation + (direction_feet_up_ori * totalArmLengthRight * 4);
                    somenewtargetlocation = somenewtargetlocation + (-direction_feet_forward_ori * totalArmLengthRight * 4);
                    */

                    Vector3 dirtohandinvertedx = dirShoulderToHand;
                    dirtohandinvertedx.X *= -1;
                    dirtohandinvertedx.Z *= -1;

                    somenewtargetlocation = (_player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT) + (dirtohandinvertedx * totalArmLengthRight * 2);
                    somenewtargetlocation = somenewtargetlocation + (direction_feet_up_ori * totalArmLengthRight * 4);
                    somenewtargetlocation = somenewtargetlocation + (-direction_feet_forward_ori * totalArmLengthRight * 10);


                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {
                    somenewtargetlocation = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT + (dirShoulderToHand * totalArmLengthRight * 2);
                    somenewtargetlocation = somenewtargetlocation + (-direction_feet_up_ori * totalArmLengthRight * 4);

                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {
                    somenewtargetlocation = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT + (dirShoulderToHand * totalArmLengthRight * 2);
                    somenewtargetlocation = somenewtargetlocation + (-direction_feet_up_ori * totalArmLengthRight * 4);

                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)
                {

                    Vector3 dirtohandinvertedx = dirShoulderToHand;
                    dirtohandinvertedx.X *= -1;
                    dirtohandinvertedx.Z *= -1;

                    somenewtargetlocation = (_player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT) + (dirtohandinvertedx * totalArmLengthRight * 2);
                    somenewtargetlocation = somenewtargetlocation + (direction_feet_up_ori * totalArmLengthRight * 4);
                    somenewtargetlocation = somenewtargetlocation + (-direction_feet_forward_ori * totalArmLengthRight * 10);
                }

                /*
                somenewtargetlocation.X += OFFSETPOS.X;
                somenewtargetlocation.Y += OFFSETPOS.Y;
                somenewtargetlocation.Z += OFFSETPOS.Z;*/


                //MOVINGPOINTER.Z = initz;

                someNewPointer = somenewtargetlocation;

                //var someOffsetter = somePosOfRightHand - OFFSETPOS;
                //var someOtherPivotPoint = MOVINGPOINTER;



                /*
                diffNormPosXElbowRight = (_player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41) - (TORSOPIVOT.X);
                diffNormPosYElbowRight = (_player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42) - (TORSOPIVOT.Y);
                diffNormPosZElbowRight = (_player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43) - (TORSOPIVOT.Z);
                MOVINGPOINTER = TORSOPIVOT.X + -(mainarmparentmeshobject._arrayOfInstances[_iterator].rightdirection * (diffNormPosXElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Y + -(mainarmparentmeshobject._arrayOfInstances[_iterator].updirection * (diffNormPosYElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Z + -(mainarmparentmeshobject._arrayOfInstances[_iterator].forwarddirection * (diffNormPosZElbowRight));
                */


                matrixerer = finalRotationMatrix;
                matrixerer.M41 = someNewPointer.X;
                matrixerer.M42 = someNewPointer.Y;
                matrixerer.M43 = someNewPointer.Z;
                matrixerer.M44 = 1;
                var _body_pos = matrixerer;
                Quaternion _quat;
                Quaternion.RotationMatrix(ref _body_pos, out _quat);
                JQuaternion _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                var matrixIn = JMatrix.CreateFromQuaternion(_other_quat);
                worldMatrix_instances_r_elbow_target_two[0][0][_iterator] = matrixerer;
                _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
















                //////////////////
                //UPPER ARM RIGHT
                MOVINGPOINTER = new Vector3(ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M41, ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42, ikvoxelbody._player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M43);
                TORSOPIVOT = MOVINGPOINTER;




                _rotMatrixer = _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;








                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);

                var test = MOVINGPOINTER + OFFSETPOS;
                Quaternion.RotationMatrix(ref finalRotationMatrix, out otherQuat);
                direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                diffNormPosX = (test.X) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41;
                diffNormPosY = (test.Y) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42;
                diffNormPosZ = (test.Z) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43;
                var realPIVOTOfUpperArm = MOVINGPOINTER;
                var realPositionOfUpperArm = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_up * (diffNormPosY));
                realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_forward * (diffNormPosZ));
                realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_right * (diffNormPosX));
                realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_up * (diffNormPosY));
                realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_forward * (diffNormPosZ));
                realPIVOTOfUpperArm = realPIVOTOfUpperArm + (direction_feet_up_ori * (_player_rght_shldr[0][0]._total_torso_height * connectorOfUpperArmRightOffsetMul));

                realPIVOTOfUpperArm.X = realPIVOTOfUpperArm.X + OFFSETPOS.X;
                realPIVOTOfUpperArm.Y = realPIVOTOfUpperArm.Y + OFFSETPOS.Y;
                realPIVOTOfUpperArm.Z = realPIVOTOfUpperArm.Z + OFFSETPOS.Z;
                Vector3 currentFINALPIVOTUPPERARM = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43) + (direction_feet_up_ori * (_player_rght_shldr[0][0]._total_torso_height * connectorOfUpperArmRightOffsetMul));// realPIVOTOfUpperArm;
                realPIVOTOfUpperArm = currentFINALPIVOTUPPERARM;
                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT = currentFINALPIVOTUPPERARM;

                //WAYPOINT
                somePosOfRightHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                var somePosOfUpperElbowTargetTwo = new Vector3(_player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                var somePosOfUpperElbowTargetOne = new Vector3(_player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                var someDirFromElbowTargetOneToTwo = somePosOfUpperElbowTargetTwo - somePosOfUpperElbowTargetOne;
                var someDirFromElbowTargetOneToRghtHand = somePosOfRightHand - somePosOfUpperElbowTargetOne;


                Vector3 crossRes;
                Vector3.Cross(ref someDirFromElbowTargetOneToTwo, ref someDirFromElbowTargetOneToRghtHand, out crossRes);
                crossRes.Normalize();

                var pointA = realPIVOTOfUpperArm + (-crossRes);

                /*//matrixerer = finalRotationMatrix;
                matrixerer.M41 = pointA.X;
                matrixerer.M42 = pointA.Y;
                matrixerer.M43 = pointA.Z;
                matrixerer.M44 = 1;
                worldMatrix_instances_r_elbow_target[0][0][_iterator] = matrixerer;
                _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;*/







                var someDirFromPivotUpperToHand = somePosOfRightHand - realPIVOTOfUpperArm;
                var lengthOfDirFromPivotUpperToHand = someDirFromPivotUpperToHand.Length();
                someDirFromPivotUpperToHand.Normalize();
                var someDirFromPivotUpperToA = pointA - realPIVOTOfUpperArm;
                var lengthOfDirFromPivotUpperToA = someDirFromPivotUpperToA.Length();
                someDirFromPivotUpperToA.Normalize();
                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ARMLENGTH = totalArmLengthRight;



                lengthOfDirFromPivotUpperToHand = Math.Min(lengthOfDirFromPivotUpperToHand, totalArmLengthRight - totalArmLengthRight * 0.001f);




                var upperEquationCirCirIntersect = (lengthOfDirFromPivotUpperToHand * lengthOfDirFromPivotUpperToHand) - (lengthOfLowerArmRight * lengthOfLowerArmRight) + (lengthOfUpperArmRight * lengthOfUpperArmRight);
                var adjacentSolvingForX = upperEquationCirCirIntersect / (2 * lengthOfDirFromPivotUpperToHand);
                adjacentSolvingForX = Math.Min(adjacentSolvingForX, lengthOfUpperArmRight - lengthOfUpperArmRight * 0.001f);
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

                /*diffNormPosXElbowRight = (_player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41) - (TORSOPIVOT.X);
                diffNormPosYElbowRight = (_player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42) - (TORSOPIVOT.Y);
                diffNormPosZElbowRight = (_player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43) - (TORSOPIVOT.Z);
                MOVINGPOINTER = TORSOPIVOT.X + -(mainarmparentmeshobject._arrayOfInstances[_iterator].rightdirection * (diffNormPosXElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Y + -(mainarmparentmeshobject._arrayOfInstances[_iterator].updirection * (diffNormPosYElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Z + -(mainarmparentmeshobject._arrayOfInstances[_iterator].forwarddirection * (diffNormPosZElbowRight));
                */

                var elbowPositionRight = someNewPointer;
                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION = elbowPositionRight;
                var dirPivotUpperRIghtToElbowRight = elbowPositionRight - currentFINALPIVOTUPPERARM;
                //var currentPositionOfUPPERARMROTATION3DPOSITION = currentFINALPIVOTUPPERARM + (dirPivotUpperRIghtToElbowRight * 0.5f);

                var currentPositionOfUPPERARMROTATION3DPOSITION = currentFINALPIVOTUPPERARM;

                if (somechunkpriminstanceikarmvoxelindex == 0)
                {
                    currentPositionOfUPPERARMROTATION3DPOSITION = currentFINALPIVOTUPPERARM + (dirPivotUpperRIghtToElbowRight * 0.5f);
                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {
                    currentPositionOfUPPERARMROTATION3DPOSITION = currentFINALPIVOTUPPERARM + (dirPivotUpperRIghtToElbowRight);
                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {
                    currentPositionOfUPPERARMROTATION3DPOSITION = currentFINALPIVOTUPPERARM + (dirPivotUpperRIghtToElbowRight);
                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)
                {
                    currentPositionOfUPPERARMROTATION3DPOSITION = currentFINALPIVOTUPPERARM + (dirPivotUpperRIghtToElbowRight * 0.5f);
                }




                var dirElbowRightToHand = somePosOfRightHand - elbowPositionRight;
                dirPivotUpperRIghtToElbowRight.Normalize();
                dirElbowRightToHand.Normalize();
                Vector3 someCross0;
                Vector3.Cross(ref dirPivotUpperRIghtToElbowRight, ref dirElbowRightToHand, out someCross0);
                someCross0.Normalize();


                /*
                Vector3 someelbowtargvec = elbowPositionRight;
                someelbowtargvec = someelbowtargvec + (someCross0);

                Matrix elbowtarg = finalRotationMatrix;


                elbowtarg.M41 = someelbowtargvec.X;
                elbowtarg.M42 = someelbowtargvec.Y;
                elbowtarg.M43 = someelbowtargvec.Z;

                elbowtarg.M44 = 1;

                worldMatrix_instances_r_elbow_target[0][0][_iterator] = elbowtarg;
                _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos = elbowtarg;
                */












                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWCROSSVEC = someCross0;
                Vector3 someCross1;
                Vector3.Cross(ref dirPivotUpperRIghtToElbowRight, ref someCross0, out someCross1);
                someCross1.Normalize();

                //FOR DEBUG    
                //FOR DEBUG    
                /*someCross0.X = (float)Math.Round(someCross0.X * 10) * 0.1f;
                someCross0.Y = (float)Math.Round(someCross0.Y * 10) * 0.1f;
                someCross0.Z= (float)Math.Round(someCross0.Z * 10) * 0.1f;

                someCross1.X = (float)Math.Round(someCross1.X * 10) * 0.1f;
                someCross1.Y = (float)Math.Round(someCross1.Y * 10) * 0.1f;
                someCross1.Z = (float)Math.Round(someCross1.Z * 10) * 0.1f;

                Console.Title = "cross0:" + someCross0 + "/cross1:" + someCross1;*/
                //FOR DEBUG    
                //FOR DEBUG   

                var shoulderRotationMatrixRight = Matrix.LookAtRH(currentFINALPIVOTUPPERARM, currentFINALPIVOTUPPERARM + someCross0, dirPivotUpperRIghtToElbowRight);
                shoulderRotationMatrixRight.Invert();






                matrixerer = shoulderRotationMatrixRight;







                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT = shoulderRotationMatrixRight;
                matrixerer.M41 = currentPositionOfUPPERARMROTATION3DPOSITION.X;
                matrixerer.M42 = currentPositionOfUPPERARMROTATION3DPOSITION.Y;
                matrixerer.M43 = currentPositionOfUPPERARMROTATION3DPOSITION.Z;
                matrixerer.M44 = 1;
                _body_pos = matrixerer;
                Quaternion.RotationMatrix(ref _body_pos, out _quat);
                _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                matrixIn = JMatrix.CreateFromQuaternion(_other_quat);
                worldMatrix_instances_r_upperarm[0][0][_iterator] = matrixerer;
                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
                //WAYPOINT

























                //////////////////
                //RIGHT LOWER ARM
                var rShldrPos = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                var dirToLowerArm = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION - rShldrPos;
                dirToLowerArm.Normalize();
                var newpoint = rShldrPos + (dirToLowerArm * lengthOfUpperArmRight);
                newpoint = newpoint + (direction_feet_up_ori * (_player_rght_shldr[0][0]._total_torso_height * connectorOfLowerArmRightOffsetMul));
                var newdir = somePosOfRightHand - newpoint;
                newdir.Normalize();
                //newpoint = newpoint + (newdir * lengthOfLowerArmRight * 0.5f);


                if (somechunkpriminstanceikarmvoxelindex == 0)
                {
                    newpoint = newpoint + (newdir * lengthOfLowerArmRight * 0.5f);

                }
                else if (somechunkpriminstanceikarmvoxelindex == 1)
                {
                    newpoint = newpoint + (newdir * lengthOfLowerArmRight * 1);
                }
                else if (somechunkpriminstanceikarmvoxelindex == 2)
                {
                    newpoint = newpoint + (newdir * lengthOfLowerArmRight * 1);
                }
                else if (somechunkpriminstanceikarmvoxelindex == 3)
                {

                    newpoint = newpoint + (newdir * lengthOfLowerArmRight * 0.5f);
                }




                //currentPositionOfUPPERARMROTATION3DPOSITION = currentFINALPIVOTUPPERARM + (dirPivotUpperRIghtToElbowRight * 0.5f);




                _rotMatrixer = _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                somePosOfRightHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                var somePosererDir = somePosOfRightHand - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;
                var someLowerRightArmPos = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION + (somePosererDir * 0.5f);
                somePosererDir.Normalize();
                someCross0 = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWCROSSVEC;
                Vector3.Cross(ref somePosererDir, ref someCross0, out someCross1);
                someCross1.Normalize();
                var theLowerArmRotationMatrix = Matrix.LookAtRH(_player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION, _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION + someCross1, somePosererDir);
                theLowerArmRotationMatrix.Invert();
                matrixerer = theLowerArmRotationMatrix;
                matrixerer.M41 = newpoint.X;
                matrixerer.M42 = newpoint.Y;
                matrixerer.M43 = newpoint.Z;
                matrixerer.M44 = 1;
                _body_pos = matrixerer;
                Quaternion.RotationMatrix(ref _body_pos, out _quat);
                _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                matrixIn = JMatrix.CreateFromQuaternion(_other_quat);
                worldMatrix_instances_r_lowerarm[0][0][_iterator] = matrixerer;
                _player_rght_lower_arm[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;

                //END OF RIGHT ARM IK
                //END OF RIGHT ARM IK
                //END OF RIGHT ARM IK

            }

            return _sc_jitter_tasks;
        }



        public scmessageobjectjitter[][] ikarmrender(scmessageobjectjitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix hmdrotMatrix, Matrix hmd_matrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft, Matrix oriProjectionMatrix)
        {

            _player_rght_hnd[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_rgt_hnd(SC_console_directx.D3D.device.ImmediateContext, _player_rght_hnd[0][0].IndexCount, _player_rght_hnd[0][0].InstanceCount, _player_rght_hnd[0][0]._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_hnd_BUFFER, _player_rght_hnd[0][0]);
            var _cuber001 = _player_rght_shldr[0][0];
            _cuber001.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_rgt_shldr(SC_console_directx.D3D.device.ImmediateContext, _cuber001.IndexCount, _cuber001.InstanceCount, _cuber001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_shldr_BUFFER, _cuber001);
            _cuber001 = _player_rght_lower_arm[0][0];
            _cuber001.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_rgt_lower_arm(SC_console_directx.D3D.device.ImmediateContext, _cuber001.IndexCount, _cuber001.InstanceCount, _cuber001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_lower_arm_BUFFER, _cuber001);
            _cuber001 = _player_rght_upper_arm[0][0];
            _cuber001.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_rgt_upper_arm(SC_console_directx.D3D.device.ImmediateContext, _cuber001.IndexCount, _cuber001.InstanceCount, _cuber001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_upper_arm_BUFFER, _cuber001);
            //IK TARGETS UPPER BODY.
            var _cuber01 = _player_rght_elbow_target[0][0];
            _cuber01.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_rgt_elbow_targ(SC_console_directx.D3D.device.ImmediateContext, _cuber01.IndexCount, _cuber01.InstanceCount, _cuber01._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_hnd_BUFFER, _cuber01);

            _cuber01 = _player_rght_elbow_target_two[0][0];
            _cuber01.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_rgt_elbow_targ_two(SC_console_directx.D3D.device.ImmediateContext, _cuber01.IndexCount, _cuber01.InstanceCount, _cuber01._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_hnd_BUFFER, _cuber01);

            _cuber01 = _player_r_hand_grab[0][0];
            _cuber01.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_rgt_hnd(SC_console_directx.D3D.device.ImmediateContext, _cuber01.IndexCount, _cuber01.InstanceCount, _cuber01._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_r_hand_grab_BUFFER, _cuber01);





            return _sc_jitter_tasks;
        }



        public scmessageobjectjitter[][] writeikarmtobuffer(scmessageobjectjitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix hmdrotMatrix, Matrix hmd_matrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft, Matrix oriProjectionMatrix)
        {




            Matrix[] worldMatrix_base = new Matrix[1];
            worldMatrix_base[0] = Matrix.Identity;



            //PHYSICS HAND RIGHT GRAB
            _player_r_hand_grab[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_hand_grab[0][0];
            _player_r_hand_grab[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS HAND RIGHT
            _player_rght_hnd[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_hand[0][0];
            _player_rght_hnd[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS LOWER ARM RIGHT
            _player_rght_lower_arm[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_lowerarm[0][0];
            _player_rght_lower_arm[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS UPPER ARM RIGHT
            _player_rght_upper_arm[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_upperarm[0][0];
            _player_rght_upper_arm[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS  RIGHT ELBOWTARGET
            _player_rght_elbow_target[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_elbow_target[0][0];
            _player_rght_elbow_target[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS RIGHT ELBOWTARGET TWO
            _player_rght_elbow_target_two[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_elbow_target_two[0][0];
            _player_rght_elbow_target_two[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS RIGHT SHOULDER
            _player_rght_shldr[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_shoulder[0][0];
            _player_rght_shldr[0][0]._POSITION = worldMatrix_base[0];






            //UPPER BODY

            voxel_cuber_r_shld = _player_rght_shldr[0][0];
            voxel_sometester_r_shld = voxel_cuber_r_shld._WORLDMATRIXINSTANCES;
            voxel_cuber_r_up_arm = _player_rght_upper_arm[0][0];
            voxel_sometester_r_up_arm = voxel_cuber_r_up_arm._WORLDMATRIXINSTANCES;
            voxel_cuber_r_targ = _player_rght_elbow_target[0][0];
            voxel_sometester_r_targ = voxel_cuber_r_targ._WORLDMATRIXINSTANCES;
            voxel_cuber_r_low_arm = _player_rght_lower_arm[0][0];
            voxel_sometester_r_low_arm = voxel_cuber_r_low_arm._WORLDMATRIXINSTANCES;
            voxel_cuber_r_hnd = _player_rght_hnd[0][0];
            voxel_sometester_r_hnd = voxel_cuber_r_hnd._WORLDMATRIXINSTANCES;
            voxel_cuber_r_hand_grab = _player_r_hand_grab[0][0];
            voxel_sometester_r_hand_grab = voxel_cuber_r_hand_grab._WORLDMATRIXINSTANCES;
            voxel_cuber_r_targ_two = _player_rght_elbow_target_two[0][0];
            voxel_sometester_r_targ_two = voxel_cuber_r_targ_two._WORLDMATRIXINSTANCES;


            Quaternion _testQuater;


            for (int i = 0; i < voxel_cuber_r_shld.instances.Length; i++)
            {













                //RIGHT HAND
                var xxx = voxel_sometester_r_hnd[i].M41;
                var yyy = voxel_sometester_r_hnd[i].M42;
                var zzz = voxel_sometester_r_hnd[i].M43;
                voxel_cuber_r_hnd.instances[i].position.X = xxx;
                voxel_cuber_r_hnd.instances[i].position.Y = yyy;
                voxel_cuber_r_hnd.instances[i].position.Z = zzz;
                voxel_cuber_r_hnd.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_r_hnd[i], out _testQuater);
                var dirInstance = -sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_hnd.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_hnd.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_hnd.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_hnd.instancesDataForward[i].rotation.W = 1;
                dirInstance = sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_hnd.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_hnd.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_hnd.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_hnd.instancesDataRIGHT[i].rotation.W = 1;
                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_hnd.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_hnd.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_hnd.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_hnd.instancesDataUP[i].rotation.W = 1;




                //RIGHT HAND GRAB
                xxx = voxel_sometester_r_hand_grab[i].M41;
                yyy = voxel_sometester_r_hand_grab[i].M42;
                zzz = voxel_sometester_r_hand_grab[i].M43;
                voxel_cuber_r_hand_grab.instances[i].position.X = xxx;
                voxel_cuber_r_hand_grab.instances[i].position.Y = yyy;
                voxel_cuber_r_hand_grab.instances[i].position.Z = zzz;
                voxel_cuber_r_hand_grab.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_r_hand_grab[i], out _testQuater);
                dirInstance = -sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_hand_grab.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_hand_grab.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_hand_grab.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_hand_grab.instancesDataForward[i].rotation.W = 1;
                dirInstance = sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_hand_grab.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_hand_grab.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_hand_grab.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_hand_grab.instancesDataRIGHT[i].rotation.W = 1;
                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_hand_grab.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_hand_grab.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_hand_grab.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_hand_grab.instancesDataUP[i].rotation.W = 1;




                //voxel_cuber_r_low_arm = _player_rght_lower_arm[tempIndex];
                //voxel_instancers_r_low_arm = voxel_instancers_r_low_arm.instances;
                //voxel_sometester_r_low_arm = voxel_instancers_r_low_arm._WORLDMATRIXINSTANCES;

                xxx = voxel_sometester_r_low_arm[i].M41;
                yyy = voxel_sometester_r_low_arm[i].M42;
                zzz = voxel_sometester_r_low_arm[i].M43;

                voxel_cuber_r_low_arm.instances[i].position.X = xxx;
                voxel_cuber_r_low_arm.instances[i].position.Y = yyy;
                voxel_cuber_r_low_arm.instances[i].position.Z = zzz;
                voxel_cuber_r_low_arm.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_r_low_arm[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_low_arm.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_low_arm.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_low_arm.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_low_arm.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_low_arm.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_low_arm.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_low_arm.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_low_arm.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_low_arm.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_low_arm.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_low_arm.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_low_arm.instancesDataUP[i].rotation.W = 1;





                xxx = voxel_sometester_r_up_arm[i].M41;
                yyy = voxel_sometester_r_up_arm[i].M42;
                zzz = voxel_sometester_r_up_arm[i].M43;

                voxel_cuber_r_up_arm.instances[i].position.X = xxx;
                voxel_cuber_r_up_arm.instances[i].position.Y = yyy;
                voxel_cuber_r_up_arm.instances[i].position.Z = zzz;
                voxel_cuber_r_up_arm.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_r_up_arm[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_up_arm.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_up_arm.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_up_arm.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_up_arm.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_up_arm.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_up_arm.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_up_arm.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_up_arm.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_up_arm.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_up_arm.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_up_arm.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_up_arm.instancesDataUP[i].rotation.W = 1;






                //voxel_cuber_r_targ = _player_rght_elbow_target[tempIndex];
                //voxel_instancers = voxel_cuber_r_targ.instances;
                //voxel_sometester_r_targ = voxel_cuber_r_targ._WORLDMATRIXINSTANCES;

                xxx = voxel_sometester_r_targ[i].M41;
                yyy = voxel_sometester_r_targ[i].M42;
                zzz = voxel_sometester_r_targ[i].M43;

                voxel_cuber_r_targ.instances[i].position.X = xxx;
                voxel_cuber_r_targ.instances[i].position.Y = yyy;
                voxel_cuber_r_targ.instances[i].position.Z = zzz;
                voxel_cuber_r_targ.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_r_targ[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_targ.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_targ.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_targ.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ.instancesDataUP[i].rotation.W = 1;





                //voxel_cuber_r_targ_two = _player_rght_elbow_target_two[tempIndex];
                //voxel_instancers = voxel_cuber_r_targ_two.instances;
                //voxel_sometester_r_targ_two = voxel_cuber_r_targ_two._WORLDMATRIXINSTANCES;


                xxx = voxel_sometester_r_targ_two[i].M41;
                yyy = voxel_sometester_r_targ_two[i].M42;
                zzz = voxel_sometester_r_targ_two[i].M43;

                voxel_cuber_r_targ_two.instances[i].position.X = xxx;
                voxel_cuber_r_targ_two.instances[i].position.Y = yyy;
                voxel_cuber_r_targ_two.instances[i].position.Z = zzz;
                voxel_cuber_r_targ_two.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_r_targ_two[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_targ_two.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_two.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_two.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_two.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_targ_two.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_two.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_two.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_two.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_targ_two.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_two.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_two.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_two.instancesDataUP[i].rotation.W = 1;

                //voxel_cuber = _player_rght_shldr[tempIndex];
                //voxel_instancers = voxel_cuber.instances;
                //voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                xxx = voxel_sometester_r_shld[i].M41;
                yyy = voxel_sometester_r_shld[i].M42;
                zzz = voxel_sometester_r_shld[i].M43;

                voxel_cuber_r_shld.instances[i].position.X = xxx;
                voxel_cuber_r_shld.instances[i].position.Y = yyy;
                voxel_cuber_r_shld.instances[i].position.Z = zzz;
                voxel_cuber_r_shld.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_r_shld[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_shld.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_shld.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_shld.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_shld.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_shld.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_shld.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_shld.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_shld.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_shld.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_shld.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_shld.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_shld.instancesDataUP[i].rotation.W = 1;

            }

            return _sc_jitter_tasks;
        }



    }
}




/*posRHand = new Vector3(_player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M41, _player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M42, _player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M43);

tempDir = posRHand - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

tempDir.Normalize();
var somePosOfSHLDR = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
Vector3 tempVect = somePosOfSHLDR + (tempDir * ((lengthOfLowerArmRight * 1.0923f) + (lengthOfUpperArmRight * 1.0923f) + (lengthOfHandRight * 1.0923f)));

MOVINGPOINTER.X = tempVect.X;
MOVINGPOINTER.Y = tempVect.Y;
MOVINGPOINTER.Z = tempVect.Z;*/
/*if (tempDir.Length() > lengthOfLowerArmRight * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
{
    tempDir.Normalize();
    var somePosOfSHLDR = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
    Vector3 tempVect = somePosOfSHLDR + (tempDir * ((lengthOfLowerArmRight * 1.0923f) + (lengthOfUpperArmRight * 1.0923f)));
    MOVINGPOINTER.X = tempVect.X;
    MOVINGPOINTER.Y = tempVect.Y;
    MOVINGPOINTER.Z = tempVect.Z;
}*/





/*Matrix handmatrix = someMatRight * finalRotationMatrix;// _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos;

Quaternion.RotationMatrix(ref handmatrix, out forTest);
Vector3 handgrabdirf = sc_maths._getDirection(Vector3.ForwardRH, forTest);
Vector3 handgrabdirr = sc_maths._getDirection(Vector3.Right, forTest);
Vector3 handgrabdiru = sc_maths._getDirection(Vector3.Up, forTest);

//Vector3 somevec0 = MOVINGPOINTER;// new Vector3(MOVINGPOINTER.X, someMatRight.M42, someMatRight.M43);

Vector3 somevec0 = directionvectoroffsets[3];*/
//somevec0 = somevec0 + (handgrabdirf  *);


//MOVINGPOINTER.X += somevec0.X;
//MOVINGPOINTER.Y += somevec0.Y;
//MOVINGPOINTER.Z += somevec0.Z;


//MOVINGPOINTER.Z = MOVINGPOINTER.Z + (handgrabdirf.Z * somevec0.Z);//



/*Matrix handmatrix = someMatRight * finalRotationMatrix;// _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos;

Quaternion.RotationMatrix(ref handmatrix, out forTest);
Vector3 handgrabdirf = sc_maths._getDirection(Vector3.ForwardRH, forTest);
Vector3 handgrabdirr = sc_maths._getDirection(Vector3.Right, forTest);
Vector3 handgrabdiru = sc_maths._getDirection(Vector3.Up, forTest);

MOVINGPOINTER = MOVINGPOINTER + (handgrabdirf * (0.1f));*/
