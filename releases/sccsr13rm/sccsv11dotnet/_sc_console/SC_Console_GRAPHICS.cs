using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Ab3d.OculusWrap;
//using Ab3d.DXEngine.OculusWrap;
using Ab3d.OculusWrap.DemoDX11;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using SharpDX.DirectInput;
using _sc_core_systems.SC_Graphics;
using _sc_core_systems.SC_Graphics.SC_ShaderManager;
using Jitter;
using Jitter.Dynamics;
using Jitter.Collision;
using Jitter.LinearMath;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;

using _sc_core_systems.sound;
using _sc_core_systems._sc_core;
using _sc_core_systems._sc_console;
using _sc_core_systems._sc_console_menu;
using _sc_core_systems._sc_message_object;
using System.Text;
using System.IO;
using SharpDX.Multimedia;
using SharpDX.IO;
using System.Xml;
using SharpDX.XAudio2;
using System.Linq;

namespace _sc_core_systems
{
    public class SC_Console_GRAPHICS
    {
        int _has_init_screen = 0;

        //PHYSICS ENGINE SETTINGS
        public static World[] _world_list;
        public const int _physics_engine_instance_x = 1; //4
        public const int _physics_engine_instance_y = 1; //1
        public const int _physics_engine_instance_z = 1; //4
        JVector _world_gravity = new JVector(0, 0, 0); //-9.81f base
        int _world_iterations = 3; // as high as possible normally for higher precision
        int _world_small_iterations = 3; // as high as possible normally for higher precision
        float _world_allowed_penetration = 0.00123f;
        //END OF

        //SCREEN SETTINGS
        int _inst_screen_x = 20;
        int _inst_screen_y = 20;
        int _inst_screen_z = 1;
        float _screen_size_x = 2; //0.0115f //1.5f
        float _screen_size_y = 2; //0.0115f //1.5f
        float _screen_size_z = 0.0035f; //0.0025f
        int _inst_screen_assets_x = 3;
        int _inst_screen_assets_y = 1;
        int _inst_screen_assets_z = 3;
        float _screen_assets_size_x = 0.0025f; //0.0115f //1.5f
        float _screen_assets_size_y = 0.0025f; //0.0115f //1.5f
        float _screen_assets_size_z = 0.0025f;
        //END OF

        //HUMAN RIG
        const int _human_inst_rig_x = 20;
        const int _human_inst_rig_y = 1;
        const int _human_inst_rig_z = 20;
        const int _addToWorld = 0;

        //the physics engine can run 4000 objects but the voxels lag a bit
        float _voxel_mass = 100;
        int _inst_voxel_cube_x = 5;
        int _inst_voxel_cube_y = 5;
        int _inst_voxel_cube_z = 5;
        float _voxel_cube_size_x = 0.015f; //0.0115f
        float _voxel_cube_size_y = 0.015f; //0.0115f
        float _voxel_cube_size_z = 0.015f;//0.0015f
        float voxel_general_size = 0.1f;

        //PHYSICS CUBES
        int _inst_cube_x = 1;
        int _inst_cube_y = 1;
        int _inst_cube_z = 1;
        float _cube_size_x = 0.015f; //0.0115f //1.5f
        float _cube_size_y = 0.015f; //0.0115f //1.5f
        float _cube_size_z = 0.015f;
        //END OF

        //float _voxel_cube_size_x = 0.0515f;
        //float _voxel_cube_size_y = 0.0515f;
        //float _voxel_cube_size_z = 0.0515f;

        int _inst_spectrum_x = 75; // 36 // 210 //75
        int _inst_spectrum_y = 1;
        int _inst_spectrum_z = 75; // 36 // 210 //75 //5625
        float _spectrum_size_x = 0.001115f;
        float _spectrum_size_y = 0.001115f;
        float _spectrum_size_z = 0.001115f;

        //static cubes 
        int _inst_terrain_tile_x = 1;
        int _inst_terrain_tile_y = 1;
        int _inst_terrain_tile_z = 1;
        float _terrain_tile_size_x = 0.015f;
        float _terrain_tile_size_y = 0.05f;
        float _terrain_tile_size_z = 0.015f;

        //main terrain.
        float _terrain_size_x = 1000;
        float _terrain_size_y = 10;
        float _terrain_size_z = 1000;

        int _type_of_cube = 3;

        int _bounding_box_max_frame_before_check = 0;
        int _max_distance_to_check_bounding_box_temp = 30;

        public static SharpDX.Vector3 originPos = new SharpDX.Vector3(0, 1, 1);
        SharpDX.Vector3 originPosScreen = new SharpDX.Vector3(0, 1, -0.25f);
        Matrix _direction_offsetter;
        Matrix _screen_direction_offsetter_two;

        SC_cube[] _world_screen_list;
        Matrix[][] worldMatrix_instances_screens;
        Matrix[][] world_last_Matrix_instances_screens;

        SC_cube[] _world_cube_list;
        Matrix[][] worldMatrix_instances_cubes;

        //Vector3[][] _last_frame_force;

        _sc_spectrum[] _world_spectrum_list;
        _sc_voxel[] _world_voxel_cube_lists;
        SC_cube _world_terrain;
        SC_cube[] _world_terrain_tile_list;
        SC_cube[] _world_screen_assets_list;
        Matrix[][] worldMatrix_instances_player_ik;
        Matrix[][] worldMatrix_instances_voxel_cube;
        Matrix[][] worldMatrix_instances_spectrum;
        Matrix[][] worldMatrix_instances_icosphere;
        Matrix[][] worldMatrix_instances_sphere;

        Matrix[][] worldMatrix_instances_terrain_tiles;
        Matrix[][] worldMatrix_instances_terrain;
        Matrix[][] worldMatrix_instances_screen_assets;

        SC_cube _current_indexed_cube;

        int _temp_spectrum_dist_swtch = 0;

        float offsetX = 0;
        float offsetY = 0;
        float offsetZ = 0;

        int[][] _some_frame_counter_raycast_00;
        int[][] _some_frame_counter_raycast_01;
        int[][] _some_reset_for_applying_force;

        int _some_frame_counter_raycast_00_max_counter = 50;
        int _some_frame_counter_raycast_01_max_counter = 1000;

        int _some_frame_counter_randomizer_switch = 0;
        int _some_frame_counter_randomizer_switch_counter = 0;

        JVector[][][] _some_last_frame_vector;
        RigidBody[][][] _some_last_frame_rigibodies;

        RigidBody _grabbed_body_right;

        Stopwatch _this_thread_ticks_00 = new Stopwatch();
        Stopwatch _this_thread_ticks_01 = new Stopwatch();
  


        Matrix final_hand_pos_left;
        Matrix final_hand_pos_right;

        Matrix temp_hand_pos_left;
        Matrix temp_hand_pos_right;

        Matrix _rotMatrixer;
        Vector3 someNewPointer;
        Vector3 MOVINGPOINTER;
        Quaternion forTest;
        //Vector4 ambientColor = new Vector4(0.75f, 0.75f, 0.75f, 1.0f);
        //Vector4 diffuseColour = new Vector4(0.95f, 0.95f, 0.95f, 1);
        //Vector3 lightDirection = new Vector3(0, -1, -1);



        Vector3 _ray_dir_left = Vector3.Zero;
        Vector3 _ray_dir_right = Vector3.Zero;

        float _length_of_ray_right = 0;
        float _length_of_ray_left = 0;

        struct _rigid_data
        {
            public RigidBody _body;
            public int _index;
            public int _physics_engine_index;
        }

        _rigid_data _grab_rigid_data;



        Vector3 screenNormalRight;
        Vector3 screenNormalForward;
        Quaternion _testQuater;

        Matrix _locked_screen_matrix_two = Matrix.Identity;

        Vector3 _offset_grab_pos = Vector3.Zero;

        Vector4[][] _array_of_colors;

        Matrix _locked_screen_matrix = Matrix.Identity;

        public int _had_grabbed = 0;




        int _has_used_trigger = 0;
        int _screen_orientation_position_type = 0;


        Matrix _grabbed_object_matrix;
        float _current_hand_float_for_dX = 0;
        float _current_hand_float_for_dY = 0;
        float _current_hand_float_for_dZ = 0;

        int[][] _switch_for_collision;
        int _has_init_ray;
        JMatrix _last_frame_rigid_grab_rot;
        Vector3 _last_frame_rigid_grab_pos;
        Matrix final_hand_pos_right_locked;
        Matrix final_hand_pos_left_locked;
        Vector3 _last_frame_handPos = Vector3.Zero;
        Vector3 _last_final_hand_pos_right;
        int[][] _objects_static_00;
        int[][] _objects_static_counter_00;
        RigidBody[][] _objects_rigid_static_00;

        Vector3 _grab_hand_pos;
        Vector3 _grab_body_pos;

        double _last_frame_yaw = 0;
        double _last_frame_pitch = 0;
        double _last_frame_roll = 0;

        float _grabbed_diff_x;
        float _grabbed_diff_y;
        float _grabbed_diff_z;

        int _has_grabbed_right = -1;
        Vector3 _offset_grabbed_pos_norm = new Vector3(0, 0, 0);
        Vector3 _offset_grabbed_pos = new Vector3(0, 0, 0);
        float _offset_grabbed_pos_dist = 0;

        int _4_falling_after_grab_counter = 0;
        int _4_falling_after_grab_swtch = 0;
        int _4_falling_after_grab_init = 0;

        int _sec_logic_swtch_grab = 0;
        float thumbstickIsRight;
        float thumbstickIsUp;


        int _has_grabbed_right_swtch = 0;
        uint lastbuttonPressedOculusTouchRight = 0;
        uint lastbuttonPressedOculusTouchLeft = 0;
        int _hasLockedMouse = 0;
        Matrix original_left_touch_matrix;
        Matrix original_right_touch_matrix;
        Vector3 screenNormal;
        Vector3[] point3DCollection;// = new Vector3[4];

        SharpDX.Matrix[] _screenDirMatrix_correct_pos;




        float _size_screen;
        float sizeWidtherer;
        float sizeheighterer;
        Matrix originRotScreen;
        Matrix rotatingMatrixScreen;
        float oriRotationScreenY { get; set; }
        float oriRotationScreenX { get; set; }
        float oriRotationScreenZ { get; set; }


        Matrix _last_screen_pos;
        Matrix _current_screen_pos;
        Matrix _finalRotMatrixScreen = Matrix.Identity;
        Matrix rotatingMatrixForGrabber;
        double LastRotationScreenY { get; set; }
        double LastRotationScreenX { get; set; }
        double LastRotationScreenZ { get; set; }

        double RotationScreenY { get; set; }
        double RotationScreenX { get; set; }
        double RotationScreenZ { get; set; }

        float speedRot = 0.85f;
        float speedPos = 0.05f;

        int _has_locked_screen_pos = 0;
        int _has_locked_screen_pos_counter = 0;

        SharpDX.Matrix rotatingMatrixForPelvis = SharpDX.Matrix.Identity;


        //OBSOLETE MAYBE
        //SC_visual_object_manager _SC_visual_object_manager;

        SC_SharpDX_ScreenFrame _desktopFrame;
        public static SC_SharpDX_ScreenCapture _desktopDupe;


        string last_xml_filepath = "";
        string last_wave_filepath = "";
        DateTime _time_of_recording_start = DateTime.Now;
        DateTime _time_of_recording_end = DateTime.Now;
        int sc_can_start_rec_counter = 0;
        int sc_can_start_rec_counter_before_add_index = 0;
        int sc_play_file = 0;
        int sc_play_file_counter = 0;
        int sc_save_file = 0;
        int sc_save_file_counter = 0;
        int sc_start_recording = 0;
        int sc_start_recording_counter = 0;
        byte[] _sound_byte_array = new byte[44100];
        byte[] _sound_byte_array_lift = new byte[44100];

        static XmlTextWriter writer = new XmlTextWriter(Console.Out);
        string path;
        int _records_counter = 0;
        int _frames_to_access_counter = 0;
        int _spectrum_work = 0;
        int _spectrum_work_counter = 0;
        int _has_recorded = 0;


        DSound _sound;// = new DSound();
        SoundPlayer _sound_player = new SoundPlayer();
        string _index = "";

        public static int GetSoundLength(string fileName)
        {
            StringBuilder lengthBuf = new StringBuilder(32);
            mciSendString(string.Format("open \"{0}\" type waveaudio alias wave", fileName), null, 0, IntPtr.Zero);
            mciSendString("status wave length", lengthBuf, lengthBuf.Capacity, IntPtr.Zero);
            mciSendString("close wave", null, 0, IntPtr.Zero);
            int length = 0;
            int.TryParse(lengthBuf.ToString(), out length);
            return length;
        }



        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);
        /*//https://stackoverflow.com/questions/33044848/c-sharp-lerping-from-position-to-position
        float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }

        Vector3 Lerp(Vector3 firstVector, Vector3 secondVector, float by)
        {
            float retX = Lerp(firstVector.X, secondVector.X, by);
            float retY = Lerp(firstVector.Y, secondVector.Y, by);
            float retZ = Lerp(firstVector.Z, secondVector.Z, by);
            return new Vector3(retX, retY, retZ);
        }*/

        //https://dotnetfiddle.net/ylrjgM
        public static float Lerp(float start, float end, float time)
        {
            return (1 - time) * start + time * end;
        }

        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);













        int hasClickedBUTTONA = 0;


        Ab3d.OculusWrap.Result resultsRight;
        uint buttonPressedOculusTouchRight;
        Vector2f[] thumbStickRight;
        float[] handTriggerRight;
        float[] indexTriggerRight;


        Ab3d.OculusWrap.Result resultsLeft;
        uint buttonPressedOculusTouchLeft;
        Vector2f[] thumbStickLeft;
        float[] handTriggerLeft;
        float[] indexTriggerLeft;

        Posef handPoseLeft;
        SharpDX.Quaternion _leftTouchQuat;
        Posef handPoseRight;
        SharpDX.Quaternion _rightTouchQuat;

        Matrix _rightTouchMatrix;
        Matrix _leftTouchMatrix;






        Matrix[] worldMatrix_base;
        int _start_background_worker_00 = 0;
        int _start_background_worker_01 = 0;
        int _swtch = 0;
        int _swtch_counter_00 = 0;
        int _swtch_counter_01 = 0;
        int _swtch_counter_02 = 0;
        float _delta_timer_frame = 0;
        float _delta_timer_time = 0;
        DateTime time1;
        DateTime time2;
        float deltaTime;
        Stopwatch timeStopWatch00 = new Stopwatch();
        Stopwatch timeStopWatch01 = new Stopwatch();
        Stopwatch _SystemTickPerformance = new Stopwatch();
        int textureIndex;
        SharpDX.Vector3 eyePos;
        SharpDX.Matrix eyeQuaternionMatrix;
        SharpDX.Matrix finalRotationMatrix;
        Vector3 lookUp;
        Vector3 lookAt;
        Vector3 viewPosition;
        Matrix viewMatrix;
        Matrix _projectionMatrix;
        SharpDX.Matrix _WorldMatrix;
        double displayMidpoint;
        TrackingState trackingState;
        Posef[] eyePoses;
        EyeType eye;
        EyeTexture eyeTexture;
        bool latencyMark = false;
        TrackingState trackState;
        PoseStatef poseStatefer;
        Posef hmdPose;
        Quaternionf hmdRot;
        Vector3 _hmdPoser;
        Matrix matroxer2;
        int _lockedFrameCounter = 0;
        int _hasLockedkey = 0;
        int _can_work_physics = 0;
        public static List<SC_cube> _arrayOfClothCubes = new List<SC_cube>();
        public static World World { get; set; }
        CollisionSystemPersistentSAP collision;
        int instX;
        int instY;
        int instZ;
        float offsetPosX;
        float offsetPosY;
        float offsetPosZ;
        Matrix _object_worldmatrix;
        public static Matrix WorldMatrix;
        SC_cube _cube;
        SC_cube _terrain;

        Matrix _screen_swap = Matrix.Identity;


        _sc_core_systems.SC_Graphics.SC_cube.DLightBuffer[] _DLightBuffer = new SC_cube.DLightBuffer[1];
        _sc_core_systems.SC_Graphics._sc_spectrum.DLightBuffer[] _DLightBuffer_spectrum = new _sc_spectrum.DLightBuffer[1];
        _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer[] _DLightBuffer_voxel_cube = new _sc_voxel.DLightBuffer[1];


        //float _World_Step;
        int count;
        Matrix translationMatrix;
        IEnumerator enumerator;
        RigidBody body;
        JQuaternion quatterer;
        Quaternion tester;
        Matrix rigidbody_matrix;
        Vector3 OFFSETPOS;
        float pitch;
        float yaw;
        float roll;
        public static SharpDX.Matrix rotatingMatrix = SharpDX.Matrix.Identity;

        public static SharpDX.Vector3 movePos = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Matrix originRot = SharpDX.Matrix.Identity;
        public static double RotationY { get; set; }
        public static double RotationX { get; set; }
        public static double RotationZ { get; set; }
        public static DShaderManager _shaderManager;
        public static _sc_texture_loader _basicTexture;


        public static _sc_texture_loader _pink_texture;

        private _sc_core_systems._sc_camera Camera { get; set; }
        public static IntPtr Hwnd;
        _sc_core_systems._sc_console._sc_console_writer _currentWriter;
        static DirectInput directInput;
        private _sc_core_systems.SC_console_directx D3D { get; set; }

        public async Task DoWork(int timeOut)
        {
            float startTime = (float)(timeStopWatch00.ElapsedMilliseconds);
        _threadLoop:

            if (_swtch == 0 || _swtch == 1)
            {
                if (_swtch == 0)
                {
                    if (_swtch_counter_00 >= 0)
                    {
                        ////////////////////
                        //UPGRADED DELTATIME
                        ////////////////////
                        //IMPORTANT PHYSICS TIME 
                        timeStopWatch00.Start();
                        time1 = DateTime.Now;
                        ////////////////////
                        //UPGRADED DELTATIME
                        ////////////////////
                        _swtch = 1;
                        _swtch_counter_00 = 0;
                    }
                }
                else if (_swtch == 1)
                {
                    if (_swtch_counter_01 >= 0)
                    {
                        ////////////////////
                        //UPGRADED DELTATIME
                        ////////////////////
                        timeStopWatch01.Start();
                        time2 = DateTime.Now;
                        ////////////////////
                        //UPGRADED DELTATIME
                        ////////////////////
                        _swtch = 2;
                        _swtch_counter_01 = 0;
                    }
                }
                else if (_swtch == 2)
                {

                }
            }

            //FRAME DELTATIME
            _delta_timer_frame = (float)Math.Abs((timeStopWatch01.Elapsed.Ticks - timeStopWatch00.Elapsed.Ticks));

            time2 = DateTime.Now;
            _delta_timer_time = (time2.Ticks - time1.Ticks); //100000000f
            //time1 = time2;

            deltaTime = (float)Math.Abs(_delta_timer_time - _delta_timer_frame);

            //time1 = time2;
            await Task.Delay(1);
            Thread.Sleep(timeOut);
            _swtch_counter_00++;
            _swtch_counter_01++;
            _swtch_counter_02++;

            goto _threadLoop;
        }

        public static SharpDX.Matrix[] _screenDirMatrix;

        private bool RaycastCallback(RigidBody body, JVector normal, float fraction)
        {
            if (body.IsStatic) return false;
            else return true;
        }

        public SC_Console_GRAPHICS()
        {

        }



        public bool Initialize(_sc_core_systems._sc_core._sc_system_configuration configuration, IntPtr windowsHandle, _sc_console._sc_console_writer _writer)
        {
            _this_thread_ticks_00.Start();
            _this_thread_ticks_01.Start();
            try
            {
                for (int s = 0; s < _sound_byte_array_lift.Length; s++)
                {
                    _sound_byte_array_lift[s] = 0;
                }

                Hwnd = windowsHandle;
                _currentWriter = _writer;

                D3D = new SC_console_directx();

                if (!D3D.Initialize(configuration, windowsHandle, _writer))
                    return false;

                _desktopDupe = new SC_SharpDX_ScreenCapture(0, 0, D3D.device);

                DoWork(0);
                WorldMatrix = Matrix.Identity;

                _world_screen_list = new SC_cube[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _world_screen_assets_list = new SC_cube[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                worldMatrix_instances_screens = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                world_last_Matrix_instances_screens = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                worldMatrix_instances_screen_assets = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];


                worldMatrix_instances_terrain = new Matrix[1][]; //1 terrain only
                _world_spectrum_list = new _sc_spectrum[1];
                worldMatrix_instances_spectrum = new Matrix[1][];

                _world_list = new World[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _world_cube_list = new SC_cube[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _world_terrain_tile_list = new SC_cube[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _world_voxel_cube_lists = new _sc_voxel[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];

                worldMatrix_instances_cubes = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                worldMatrix_instances_voxel_cube = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                worldMatrix_instances_terrain_tiles = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                _array_of_colors = new Vector4[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];

                _objects_static_00 = new int[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                _objects_static_counter_00 = new int[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                _objects_rigid_static_00 = new RigidBody[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                _switch_for_collision = new int[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];





                //HUMAN RIG STUFF
                _player_rght_hnd = new _sc_voxel[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _player_lft_upper_arm = new _sc_voxel[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _player_lft_hnd = new _sc_voxel[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _player_torso = new _sc_voxel[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _player_pelvis = new _sc_voxel[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _player_rght_shldr = new _sc_voxel[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _player_lft_shldr = new _sc_voxel[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _player_head = new _sc_voxel[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _player_rght_lower_arm = new _sc_voxel[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _player_lft_lower_arm = new _sc_voxel[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _player_rght_upper_arm = new _sc_voxel[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _player_rght_elbow_target = new _sc_voxel[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _player_lft_elbow_target = new _sc_voxel[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _player_rght_elbow_target_two = new _sc_voxel[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _player_lft_elbow_target_two = new _sc_voxel[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];

                worldMatrix_instances_r_elbow_target = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                worldMatrix_instances_l_elbow_target = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];

                worldMatrix_instances_r_elbow_target_two = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                worldMatrix_instances_l_elbow_target_two = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];

                worldMatrix_instances_head = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                worldMatrix_instances_torso = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                worldMatrix_instances_pelvis = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];

                worldMatrix_instances_r_hand = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                worldMatrix_instances_l_hand = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];

                worldMatrix_instances_r_shoulder = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                worldMatrix_instances_l_shoulder = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];

                worldMatrix_instances_r_upperarm = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                worldMatrix_instances_l_upperarm = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];

                worldMatrix_instances_r_lowerarm = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                worldMatrix_instances_l_lowerarm = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];

                worldMatrix_instances_r_upperleg = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                worldMatrix_instances_l_upperleg = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];

                worldMatrix_instances_r_lowerleg = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                worldMatrix_instances_l_lowerleg = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];

                worldMatrix_instances_r_foot = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                worldMatrix_instances_l_foot = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];




                Quaternion.RotationMatrix(ref WorldMatrix, out _testQuater);
                dirLight = _getDirection(Vector3.ForwardRH, _testQuater);





























                //RAYCAST STUFF
                _some_reset_for_applying_force = new int[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                _some_frame_counter_raycast_00 = new int[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                _some_frame_counter_raycast_01 = new int[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                _switch_for_collision = new int[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                _some_last_frame_vector = new JVector[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][][];
                _some_last_frame_rigibodies = new RigidBody[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][][];





                Camera = new _sc_camera();

                _shaderManager = new DShaderManager();
                _shaderManager.Initialize(D3D.Device, windowsHandle);

                Vector3 _offsetPos;



                float r = 0;
                float g = 0;
                float b = 0;
                float a = 1;


                worldMatrix_base = new Matrix[1];
                worldMatrix_base[0] = Matrix.Identity;



                for (int x = 0; x < _physics_engine_instance_x; x++)
                {
                    for (int y = 0; y < _physics_engine_instance_y; y++)
                    {
                        for (int z = 0; z < _physics_engine_instance_z; z++)
                        {
                            var _index = x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z);

                            Vector3 physics_engine_offset_pos = new Vector3(x * 2, y * 2, z * 2);


                            //JITTER PHYSICS
                            collision = new CollisionSystemPersistentSAP();
                            World = new World(collision);
                            World.AllowDeactivation = true;

                            World.Gravity = _world_gravity;
                            World.SetIterations(_world_iterations, _world_small_iterations);
                            World.ContactSettings.AllowedPenetration = _world_allowed_penetration;

                            _world_list[_index] = World;

                            //PHYSICS SCREENS
                            _grab_rigid_data = new _rigid_data();
                            _grab_rigid_data._body = null;
                            _grab_rigid_data._index = -1;
                            _grab_rigid_data._physics_engine_index = -1;
                            //SET TO 0 AND YOU HAVE USE A SHADERRESOURCE INSTEAD for the texture instead of using the color. cheap way for the moment as my switch wasnt working.
                            r = 0;
                            g = 0;
                            b = 0;
                            a = 0;

                            _object_worldmatrix = Matrix.Identity;

                            //offsetPosX = 0.15f; //x between each world instance
                            //offsetPosY = 0.15f; //y between each world instance
                            //offsetPosZ = 0.15f; //z between each world instance

                            _offsetPos = new Vector3(0.15f, 0.15f, 0.15f);

                            _object_worldmatrix = WorldMatrix;

                            _object_worldmatrix.M41 = -1.5f + x;
                            _object_worldmatrix.M42 = 0.5f + y;
                            _object_worldmatrix.M43 = -1.5f + z;

                            _object_worldmatrix.M44 = 1;

                            _object_worldmatrix.M41 += _offsetPos.X;
                            _object_worldmatrix.M42 += _offsetPos.Y;
                            _object_worldmatrix.M43 += _offsetPos.Z;


                            _size_screen = 0.00045f;

                            var sizeWidth01 = (float)(((float)D3D.SurfaceWidth * 0.5f) * _size_screen) * _screen_size_x;
                            var sizeheight01 = (float)((float)(D3D.SurfaceHeight * 0.5f) * _size_screen) * _screen_size_y;
                            var sizedepth01 = 1 * _screen_size_z;

                            float sizeWidther01 = (float)(sizeWidth01 * 0.5f);
                            float sizeHeighter01 = (float)(sizeheight01 * 0.5f);
                            float sizeDepther01 = (float)(sizedepth01 * 0.5f);

                            offsetPosX = sizeWidth01 * 2;
                            offsetPosY = sizeheight01 * 2;
                            offsetPosZ = sizedepth01 * 2;

                            //var _sizeX *= 0.00030f;
                            //var _sizeY *= 0.00030f;
                            //var _sizeZ *= 0.0025f;

                            _cube = new SC_cube();

                            var _hasinit0 = _cube.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, sizeWidther01, sizeHeighter01, sizeDepther01, new Vector4(r, g, b, a), _inst_screen_x, _inst_screen_y, _inst_screen_z, Hwnd, _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.physicsInstancedScreen, false, 1, 1000, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                            _world_screen_list[_index] = _cube;

                            worldMatrix_instances_screens[_index] = new Matrix[_inst_screen_x * _inst_screen_y * _inst_screen_z];
                            world_last_Matrix_instances_screens[_index] = new Matrix[_inst_screen_x * _inst_screen_y * _inst_screen_z];

                            for (int i = 0; i < worldMatrix_instances_screens[_index].Length; i++)
                            {
                                worldMatrix_instances_screens[_index][i] = Matrix.Identity;
                                world_last_Matrix_instances_screens[_index][i] = Matrix.Identity;
                            }








                            //for (int i = 0; i < _world_list.Length; i++)
                            //{
                            //    _world_list[i].AddBody(_cube._singleObjectOnly.transform.Component.rigidbody);
                            //}

                            //PHYSICS SCREEN ASSETS
                            //SET TO 0 AND YOU HAVE USE A SHADERRESOURCE INSTEAD for the texture instead of using the color. cheap way for the moment as my switch wasnt working.

                            //_array_of_colors[0] = new Vector4[_inst_screen_assets_x * _inst_screen_assets_y * _inst_screen_assets_z];

                            //r = 0.05f;
                            //g = 0.05f;
                            //b = 0.05f;
                            //a = 1;

                            //SCREEN CORNERS
                            //for (int i = 0; i < _array_of_colors.Length; i++)
                            //{
                            //    _array_of_colors[0][i] = new Vector4(r, g, b, a);
                            //}

                            //r = 0.05f;
                            //g = 0.05f;
                            //b = 0.05f;
                            //a = 1;
                            //_array_of_colors[0][4] = new Vector4(r, g, b, a);*/
                            
                            r = 0.05f;
                            g = 0.05f;
                            b = 0.05f;
                            a = 1;

                            _object_worldmatrix = Matrix.Identity;

                            //offsetPosX = 0.15f; //x between each world instance
                            //offsetPosY = 0.15f; //y between each world instance
                            //offsetPosZ = 0.15f; //z between each world instance

                            //_offsetPos = new Vector3((x * 1), 0, (z * 1));

                            _object_worldmatrix.M41 = 0 + x;
                            _object_worldmatrix.M42 = 0 + y;
                            _object_worldmatrix.M43 = 0 + z;

                            _object_worldmatrix.M44 = 1;

                            //_object_worldmatrix.M41 += _offsetPos.X;
                            //_object_worldmatrix.M42 += _offsetPos.Y;
                            //_object_worldmatrix.M43 += _offsetPos.Z;

                            //var _sizeX *= 0.00030f;
                            //var _sizeY *= 0.00030f;
                            //var _sizeZ *= 0.0025f;
                            offsetPosX = sizeWidth01 * 2;
                            offsetPosY = sizeheight01 * 2;
                            offsetPosZ = sizedepth01 * 2;
                            _cube = new SC_cube();

                            _hasinit0 = _cube.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, _screen_assets_size_x, _screen_assets_size_y, _screen_assets_size_z, new Vector4(r, g, b, a), _inst_screen_assets_x, _inst_screen_assets_y, _inst_screen_assets_z, Hwnd, _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag._screen_assets, true, 0, 10, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                            _world_screen_assets_list[_index] = _cube;

                            worldMatrix_instances_screen_assets[_index] = new Matrix[_inst_screen_assets_x * _inst_screen_assets_y * _inst_screen_assets_z];

                            for (int i = 0; i < worldMatrix_instances_screen_assets[_index].Length; i++)
                            {
                                worldMatrix_instances_screen_assets[_index][i] = Matrix.Identity;
                            }














                            try
                            {
                                
                                //PHYSICS VOXEL SPHEROID
                                r = 0.15f;
                                g = 0.15f;
                                b = 0.15f;
                                a = 1;

                                _object_worldmatrix = Matrix.Identity;

                                offsetPosX = 0.15f; //x between each world instance
                                offsetPosY = 0.15f; //y between each world instance
                                offsetPosZ = 0.15f; //z between each world instance

                                _offsetPos = new Vector3(0, 0, 0);

                                _object_worldmatrix = WorldMatrix;

                                _object_worldmatrix.M41 = 0 + x;
                                _object_worldmatrix.M42 = 1 + y;
                                _object_worldmatrix.M43 = 0 + z;

                                _object_worldmatrix.M44 = 1;

                                //_object_worldmatrix.M41 += _offsetPos.X;
                                //_object_worldmatrix.M42 += _offsetPos.Y;
                                //_object_worldmatrix.M43 += _offsetPos.Z;



                                offsetPosX = _voxel_cube_size_x * 10;
                                offsetPosY = _voxel_cube_size_y * 10;
                                offsetPosZ = _voxel_cube_size_z * 10;

                                var _sc_voxel_spheroid = new _sc_voxel();


                                voxel_general_size = 0.0035f;
                                int voxel_type = 0;
                                bool is_static = false;

                                var _hasinit00 = _sc_voxel_spheroid.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, _voxel_cube_size_x, _voxel_cube_size_y, _voxel_cube_size_z, 
                                    new Vector4(r, g, b, a), _inst_voxel_cube_x, _inst_voxel_cube_y, _inst_voxel_cube_z, Hwnd, _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World,
                                    _voxel_mass, is_static, SC_console_directx.BodyTag._voxel_spheroid, 2, 2, 2, 2, 2, 2, 50, 49, 50, 49, 50, 49, voxel_general_size, Vector3.Zero, 250, 0, 0, 0, 2); //, "terrainGrassDirt.bmp" //0.00035f

                                //var _hasinit00 = _sc_voxel_spheroid.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 5, 5, 5, _voxel_cube_size_x, _voxel_cube_size_y, _voxel_cube_size_z, new Vector4(r, g, b, a), _inst_voxel_cube_x, _inst_voxel_cube_y, _inst_voxel_cube_z, Hwnd, _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, _voxel_mass, false, SC_console_directx.BodyTag._voxel_spheroid, 2, 2, 2, 2, 2, 2, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 50,0,0,0,2); //, "terrainGrassDirt.bmp" //0.00035f
                                _world_voxel_cube_lists[_index] = _sc_voxel_spheroid;

                                worldMatrix_instances_voxel_cube[_index] = new Matrix[_inst_voxel_cube_x * _inst_voxel_cube_y * _inst_voxel_cube_z];

                                for (int i = 0; i < worldMatrix_instances_voxel_cube[_index].Length; i++)
                                {
                                    worldMatrix_instances_voxel_cube[_index][i] = Matrix.Identity;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox((IntPtr)0, ex.ToString() + "", "Oculus error", 0);
                            }




                            /*
                            //PHYSICS SPECTRUM
                            r = 0.55f;
                            g = 0.15f;
                            b = 0.55f;
                            a = 1;

                            _object_worldmatrix = Matrix.Identity;

                            offsetPosX = _spectrum_size_x * 100; //x between each world instance
                            offsetPosY = _spectrum_size_y * 100; //y between each world instance
                            offsetPosZ = _spectrum_size_z * 100; //z between each world instance

                            //_offsetPos = new Vector3((x * 1), 0, (z * 1));

                            //_object_worldmatrix = _object_worldmatrix;

                            //Vector3 position = new Vector3((x * _sizeX) + offsetPosX, (y * _sizeY) + offsetPosY, (z * _sizeZ) + offsetPosZ);

                            //_object_worldmatrix.M41 = x * _spectrum_size_x;// -(_spectrum_size_x * _inst_spectrum_x);
                            // _object_worldmatrix.M42 = y * _spectrum_size_y;
                            // _object_worldmatrix.M43 = z * _spectrum_size_z;//-(_spectrum_size_z * _inst_spectrum_z);

                            //_object_worldmatrix.M44 = 1;

                            //_object_worldmatrix.M41 += _offsetPos.X;
                            //_object_worldmatrix.M42 += _offsetPos.Y;
                            //_object_worldmatrix.M43 += _offsetPos.Z;

                            var _sc_spectrum = new _sc_spectrum();

                            var _hasinit2 = _sc_spectrum.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, _spectrum_size_x, _spectrum_size_y, _spectrum_size_z, new Vector4(r, g, b, a), _inst_spectrum_x, _inst_spectrum_y, _inst_spectrum_z, Hwnd, _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, true); //, "terrainGrassDirt.bmp" //0.00035f
                            _world_spectrum_list[_index] = _sc_spectrum;

                            worldMatrix_instances_spectrum[_index] = new Matrix[_inst_spectrum_x * _inst_spectrum_y * _inst_spectrum_z];

                            for (int i = 0; i < worldMatrix_instances_spectrum[_index].Length; i++)
                            {
                                worldMatrix_instances_spectrum[_index][i] = Matrix.Identity;
                            }*/

                            /*//TERRAIN TILES
                            r = 0.75f;
                            g = 0.75f;
                            b = 0.75f;
                            a = 1;

                            _object_worldmatrix = Matrix.Identity;
                            _object_worldmatrix = WorldMatrix;

                            _object_worldmatrix.M41 = 0;
                            _object_worldmatrix.M42 = 0;
                            _object_worldmatrix.M43 = 0;
                            _object_worldmatrix.M44 = 1;

                            //_offsetPos = new Vector3(x * (_terrain_tile_size_x), 0, z * (_terrain_tile_size_z));
                            _offsetPos = new Vector3((x * 1), 0, (z * 1));

                            offsetPosX = 0.15f;
                            offsetPosY = 0.15f;
                            offsetPosZ = 0.15f;

                            _object_worldmatrix.M41 += _offsetPos.X;
                            _object_worldmatrix.M42 += _offsetPos.Y;
                            _object_worldmatrix.M43 += _offsetPos.Z;

                            var _terrain = new SC_cube();
                            //_terrain.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, _terrain_tile_size_x, _terrain_tile_size_y, _terrain_tile_size_z, new Vector4(r, g, b, a), _inst_terrain_tile_x, _inst_terrain_tile_y, _inst_terrain_tile_z, Hwnd, _object_worldmatrix, 1, offsetPosX, offsetPosY, offsetPosZ, World); //, "terrainGrassDirt.bmp" //0.00035f
                            var _hasinit01 = _terrain.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, _terrain_tile_size_x, _terrain_tile_size_y, _terrain_tile_size_z, new Vector4(r, g, b, a), _inst_terrain_tile_x, _inst_terrain_tile_y, _inst_terrain_tile_z, Hwnd, _object_worldmatrix, 1, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag._terrain_tiles, true, 1); //, "terrainGrassDirt.bmp" //0.00035f

                            _world_terrain_tile_list[_index] = _terrain;

                            worldMatrix_instances_terrain_tiles[_index] = new Matrix[_inst_terrain_tile_x * _inst_terrain_tile_y * _inst_terrain_tile_z];

                            for (int i = 0; i < worldMatrix_instances_terrain_tiles[_index].Length; i++)
                            {
                                worldMatrix_instances_terrain_tiles[_index][i] = Matrix.Identity;
                            }*/







                            
                            //rgba(228, 120, 51, 1) ZEST ORANGE
                            //rgba(242, 121, 53, 1) JAFFA ORANGE

                            //PHYSICS FALLING CUBES
                            //SET TO 0 AND YOU HAVE USE A SHADERRESOURCE INSTEAD for the texture instead of using the color. cheap way for the moment as my switch wasnt working.
                            r = 0.05f;
                            g = 0.05f;
                            b = 0.05f;
                            a = 1;

                            _object_worldmatrix = Matrix.Identity;

                            offsetPosX = 0.15f; //x between each world instance
                            offsetPosY = 0.15f; //y between each world instance
                            offsetPosZ = 0.15f; //z between each world instance

                            _offsetPos = new Vector3((x * 1), (y * 1), (z * 1));

                            _object_worldmatrix = WorldMatrix;

                            _object_worldmatrix.M41 = 1.5f + x;
                            _object_worldmatrix.M42 = 0.5f + y;
                            _object_worldmatrix.M43 = 1.5f + z;

                            _object_worldmatrix.M44 = 1;

                            //_object_worldmatrix.M41 += _offsetPos.X;
                            //_object_worldmatrix.M42 += _offsetPos.Y;
                            //_object_worldmatrix.M43 += _offsetPos.Z;

                            _size_screen = 0.00045f;

                            var sizeWidth = _cube_size_x;
                            var sizeheight = _cube_size_y;
                            var sizedepth = _cube_size_z;

                            var sizeWidther = (float)(sizeWidth * 0.5f);
                            var sizeHeighter = (float)(sizeheight * 0.5f);
                            var sizeDepther = (float)(sizedepth * 0.5f);
                            //var _sizeX *= 0.00030f;
                            //var _sizeY *= 0.00030f;
                            //var _sizeZ *= 0.0025f;

                            _cube = new SC_cube();

                            var _hasinit02 = _cube.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0.1f, 1, 1, _cube_size_x, _cube_size_y, _cube_size_z, new Vector4(r, g, b, a), _inst_cube_x, _inst_cube_y, _inst_cube_z, Hwnd, _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.physicsInstancedCube, false, 1, 100,0,0,0); //, "terrainGrassDirt.bmp" //0.00035f
                            _world_cube_list[_index] = _cube;

                            worldMatrix_instances_cubes[_index] = new Matrix[_inst_cube_x * _inst_cube_y * _inst_cube_z];

                            for (int i = 0; i < worldMatrix_instances_cubes[_index].Length; i++)
                            {
                                worldMatrix_instances_cubes[_index][i] = Matrix.Identity;
                            }






                            float vertoffsetx = 0;
                            float vertoffsety = 0;
                            float vertoffsetz = -(16 + 15) * 0.015f;// - 0.25f;;

                            float _dist_between = 0.30f;

                            ///////////////////////////////
                            ///////////HUMAN RIG///////////
                            ///////////////////////////////

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
                            _tempMatroxer.M41 = 0 + x;
                            _tempMatroxer.M42 = 0 + y;
                            _tempMatroxer.M43 = 0 + z;
                            _tempMatroxer.M44 = 1;

                            offsetPosX = _dist_between * 2;
                            offsetPosY = _dist_between * 2;
                            offsetPosZ = _dist_between * 2;

                            _mass = 100;

                            _player_rght_hnd[_index] = new _sc_voxel();
                            //_player_rght_hnd[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerHandRight, _static, 1, _mass, 0, 0, -0.75f); //, "terrainGrassDirt.bmp" //0.00035f
                            _player_rght_hnd[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerHandRight, 2, 2, 2, 2, 2, 2, 10, 9, 40, 39, 10, 9, voxel_general_size, new Vector3(0, 0, -0.1f), 300, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld); //, "terrainGrassDirt.bmp" //0.00035f

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

                            _tempMatroxer.M41 = 0 + x;
                            _tempMatroxer.M42 = 0 + y;
                            _tempMatroxer.M43 = 0 + z;
                            _tempMatroxer.M44 = 1;


                            offsetPosX = _dist_between * 2;
                            offsetPosY = _dist_between * 2;
                            offsetPosZ = _dist_between * 2;

                            _mass = 100;
                            //_player_lft_hnd[_index] = new _sc_voxel();
                            //_hasinit0 = _player_lft_hnd[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_l_hand_x, _inst_p_l_hand_y, _inst_p_l_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerHandLeft, _static, 1, _mass, 0, 0, -0.75f); //, "terrainGrassDirt.bmp" //0.00035f

                            _player_lft_hnd[_index] = new _sc_voxel();
                            _player_lft_hnd[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerHandLeft, 2, 2, 2, 2, 2, 2, 4, 10, 9, 10, 10, 9, voxel_general_size, new Vector3(0, 0, -0.1f), 50, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld); //, "terrainGrassDirt.bmp" //0.00035f

                            worldMatrix_instances_l_hand[_index] = new Matrix[_inst_p_l_hand_x * _inst_p_l_hand_y * _inst_p_l_hand_z];
                            for (int i = 0; i < worldMatrix_instances_l_hand[_index].Length; i++)
                            {
                                worldMatrix_instances_l_hand[_index][i] = Matrix.Identity;
                            }



                            vertoffsetx = 0;
                            vertoffsety = 0;
                            vertoffsetz = 0;
                            //TORSO
                            r = 0.035f;
                            g = 0.035f;
                            b = 0.035f;
                            a = 1;


                            _tempMatroxer = Matrix.Identity;
                            _tempMatroxer = _WorldMatrix;

                            _tempMatroxer.M41 = 0 + x;
                            _tempMatroxer.M42 = -0.35f + y; // -0.1f
                            _tempMatroxer.M43 = 0 + z;
                            _tempMatroxer.M44 = 1;

                            offsetPosX = _dist_between * 2;
                            offsetPosY = _dist_between * 2;
                            offsetPosZ = _dist_between * 2;

                            //_player_torso[_index] = new _sc_voxel();
                            //_hasinit0 = _player_torso.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f                                                                                                                                                                                                                                                                                        //_hasinit0 = _player_torso.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                            //_player_torso[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), _inst_p_torso_x, _inst_p_torso_y, _inst_p_torso_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerTorso, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

                            _mass = 100;
                            _player_torso[_index] = new _sc_voxel();
                            _player_torso[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), _inst_p_torso_x, _inst_p_torso_y, _inst_p_torso_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerTorso, 2, 2, 2, 2, 2, 2, 10, 9, 10, 9, 10, 9, voxel_general_size, new Vector3(0, 0, 0), 50, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld); //, "terrainGrassDirt.bmp" //0.00035f

                            //_player_torso[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), _inst_p_torso_x, _inst_p_torso_y, _inst_p_torso_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerTorso, 2, 9, 2, 2, 2, 2, 45, 44, 60, 59, 10, 9, 0.0025f, new Vector3(0, 0, 0), 500); //, "terrainGrassDirt.bmp" //0.00035f

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

                            _tempMatroxer.M41 = 0 + x;
                            _tempMatroxer.M42 = -0.625f  +y;
                            _tempMatroxer.M43 = 0 + z;
                            _tempMatroxer.M44 = 1;

                            offsetPosX = _dist_between * 2;
                            offsetPosY = _dist_between * 2;
                            offsetPosZ = _dist_between * 2;

                            _mass = 100;
                            //_player_pelvis[_index] = new _sc_voxel();
                            //_hasinit0 = _player_pelvis.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 9, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                            //_hasinit0 = _player_pelvis[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerPelvis, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

                            _player_pelvis[_index] = new _sc_voxel();
                            //_player_pelvis[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerPelvis, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                            _player_pelvis[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), _inst_p_pelvis_x, _inst_p_pelvis_y, _inst_p_pelvis_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerPelvis, 2, 2, 2, 2, 2, 2, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 50, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld); //, "terrainGrassDirt.bmp" //0.00035f

                            worldMatrix_instances_pelvis[_index] = new Matrix[_inst_p_pelvis_x * _inst_p_pelvis_y * _inst_p_pelvis_z];
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
                            _tempMatroxer.M41 = 0.15f + x;
                            _tempMatroxer.M42 = -0.2f + y;
                            _tempMatroxer.M43 = 0 + z;
                            _tempMatroxer.M44 = 1;
                           offsetPosX = _dist_between * 2;
                            offsetPosY = _dist_between * 2;
                            offsetPosZ = _dist_between * 2;
                            //_player_rght_shldr[_index] = new _sc_voxel();
                            //_hasinit0 = _player_rght_shldr.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 9, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                            //_hasinit0 = _player_rght_shldr[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_shoulder_x, _inst_p_r_shoulder_y, _inst_p_r_shoulder_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerShoulderRight, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                            _mass = 100;
                            _player_rght_shldr[_index] = new _sc_voxel();
                            //_player_rght_shldr[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerShoulderRight, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                            _player_rght_shldr[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_l_shoulder_x, _inst_p_l_shoulder_y, _inst_p_l_shoulder_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerShoulderRight, 2, 2, 2, 2, 2, 2, 10, 9, 10, 9, 10, 9, voxel_general_size, new Vector3(0, 0, 0), 75, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld); //, "terrainGrassDirt.bmp" //0.00035f


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
                            _tempMatroxer.M41 = -0.15f + x;
                            _tempMatroxer.M42 = -0.2f + y;
                            _tempMatroxer.M43 = 0 + z;
                            _tempMatroxer.M44 = 1;
                           offsetPosX = _dist_between * 2;
                            offsetPosY = _dist_between * 2;
                            offsetPosZ = _dist_between * 2;
                            _mass = 100;
                            //_player_lft_shldr[_index] = new _sc_voxel();
                            //_hasinit0 = _player_lft_shldr.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 9, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                            //_hasinit0 = _player_lft_shldr[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_l_shoulder_x, _inst_p_l_shoulder_y, _inst_p_l_shoulder_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerShoulderLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

                            _player_lft_shldr[_index] = new _sc_voxel();
                            //_player_lft_shldr[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerShoulderLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                            _player_lft_shldr[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_l_shoulder_x, _inst_p_l_shoulder_y, _inst_p_l_shoulder_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerShoulderLeft, 2, 2, 2, 2, 2, 2, 10, 9, 10, 9, 10, 9, voxel_general_size, new Vector3(0, 0, 0), 75, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld); //, "terrainGrassDirt.bmp" //0.00035f

                            worldMatrix_instances_l_shoulder[_index] = new Matrix[_inst_p_l_shoulder_x * _inst_p_l_shoulder_y * _inst_p_l_shoulder_z];
                            for (int i = 0; i < worldMatrix_instances_l_shoulder[_index].Length; i++)
                            {
                                worldMatrix_instances_l_shoulder[_index][i] = Matrix.Identity;
                            }


                            /*
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

                                offsetPosX = _dist_between* 2;
                            offsetPosY = _dist_between * 2;
                            offsetPosZ = _dist_between * 2;
                            //_player_head[_index] = new _sc_voxel();
                            //_hasinit0 = _player_head.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                            //_hasinit0 = _player_head[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_l_shoulder_x, _inst_p_l_shoulder_y, _inst_p_l_shoulder_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerHead, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

                            _player_head[_index] = new _sc_voxel();
                            _player_head[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerHead, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f


                            worldMatrix_instances_head[_index] = new Matrix[_inst_p_l_shoulder_x * _inst_p_l_shoulder_y * _inst_p_l_shoulder_z];
                            for (int i = 0; i < worldMatrix_instances_head[_index].Length; i++)
                            {
                                worldMatrix_instances_head[_index][i] = Matrix.Identity;
                            }*/










                            
                            //LEFT LOWER ARM
                            r = 0.035f;
                            g = 0.035f;
                            b = 0.035f;
                            a = 1;


                            _tempMatroxer = Matrix.Identity;
                            _tempMatroxer = _WorldMatrix;
                            _tempMatroxer.M41 = -0.25f + x;
                            _tempMatroxer.M42 = -0.15f + y;
                            _tempMatroxer.M43 = 0 + z;
                            _tempMatroxer.M44 = 1;
                             offsetPosX =_dist_between* 2;
                            offsetPosY = _dist_between * 2;
                            offsetPosZ = _dist_between * 2;

                            //_player_lft_lower_arm[_index] = new _sc_voxel();
                            //_hasinit0 = _player_lft_lower_arm.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                            //_hasinit0 = _player_lft_lower_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_l_lowerarm_x, _inst_p_l_lowerarm_y, _inst_p_l_lowerarm_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerLowerArmLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

                            _player_lft_lower_arm[_index] = new _sc_voxel();
                            // _player_lft_lower_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerLowerArmLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                            _player_lft_lower_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerLowerArmLeft, 2, 2, 2, 2, 2, 2, 10, 9, 10, 9, 10, 9, voxel_general_size, new Vector3(0, 0, 0), 50, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld); //, "terrainGrassDirt.bmp" //0.00035f



                            worldMatrix_instances_l_lowerarm[_index] = new Matrix[_inst_p_l_lowerarm_x * _inst_p_l_lowerarm_y * _inst_p_l_lowerarm_z];
                            for (int i = 0; i < worldMatrix_instances_l_lowerarm[_index].Length; i++)
                            {
                                worldMatrix_instances_l_lowerarm[_index][i] = Matrix.Identity;
                            }

                            
       


                            //LEFT UPPER ARM
                            r = 0.035f;
                            g = 0.035f;
                            b = 0.035f;
                            a = 1;

                            _tempMatroxer = Matrix.Identity;
                            _tempMatroxer = _WorldMatrix;
                            _tempMatroxer.M41 = -0.25f + x;
                            _tempMatroxer.M42 = -0.25f + y;
                            _tempMatroxer.M43 = 0 + z;
                            _tempMatroxer.M44 = 1;
                            offsetPosX = _dist_between * 2;
                            offsetPosY = _dist_between * 2;
                            offsetPosZ = _dist_between * 2;
                            //_player_lft_upper_arm[_index] = new _sc_voxel();
                            //_hasinit0 = _player_lft_upper_arm.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                            //_hasinit0 = _player_lft_upper_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerUpperArmLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

                            _player_lft_upper_arm[_index] = new _sc_voxel();
                            //_player_lft_upper_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerUpperArmLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                            _player_lft_upper_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerUpperArmLeft, 2, 2, 2, 2, 2, 2, 10, 9, 10, 9, 10, 9, voxel_general_size, new Vector3(0, 0, 0), 50, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld); //, "terrainGrassDirt.bmp" //0.00035f



                            worldMatrix_instances_l_upperarm[_index] = new Matrix[_inst_p_r_upperarm_x * _inst_p_r_upperarm_y * _inst_p_r_upperarm_z];
                            for (int i = 0; i < worldMatrix_instances_l_upperarm[_index].Length; i++)
                            {
                                worldMatrix_instances_l_upperarm[_index][i] = Matrix.Identity;
                            }













                            //RIGHT LOWER ARM
                            r = 0.035f;
                            g = 0.035f;
                            b = 0.035f;
                            a = 1;

                            _tempMatroxer = Matrix.Identity;
                            _tempMatroxer = _WorldMatrix;
                            _tempMatroxer.M41 = 0.25f + x;
                            _tempMatroxer.M42 = -0.15f + y;
                            _tempMatroxer.M43 = 0 + z;
                            _tempMatroxer.M44 = 1;
                            offsetPosX = _dist_between * 2;
                            offsetPosY = _dist_between * 2;
                            offsetPosZ = _dist_between * 2;

                            //_player_rght_lower_arm[_index] = new _sc_voxel();
                            //_hasinit0 = _player_rght_lower_arm.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                            //_hasinit0 = _player_rght_lower_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_lowerarm_x, _inst_p_r_lowerarm_y, _inst_p_r_lowerarm_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerLowerArmRight, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

                            _player_rght_lower_arm[_index] = new _sc_voxel();
                            //_player_rght_lower_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerLowerArmRight, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                            _player_rght_lower_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerLowerArmRight, 2, 2, 2, 2, 2, 2, 10, 9, 10, 9, 10, 9, voxel_general_size, new Vector3(0, 0, 0), 50, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld); //, "terrainGrassDirt.bmp" //0.00035f


                            worldMatrix_instances_r_lowerarm[_index] = new Matrix[_inst_p_r_lowerarm_x * _inst_p_r_lowerarm_y * _inst_p_r_lowerarm_z];
                            for (int i = 0; i < worldMatrix_instances_r_lowerarm[_index].Length; i++)
                            {
                                worldMatrix_instances_r_lowerarm[_index][i] = Matrix.Identity;
                            }
                            //RIGHT UPPER ARM
                            r = 0.035f;
                            g = 0.035f;
                            b = 0.035f;
                            a = 1;

                            _tempMatroxer = Matrix.Identity;
                            _tempMatroxer = _WorldMatrix;
                            _tempMatroxer.M41 = 0.25f + x;
                            _tempMatroxer.M42 = -0.375f + y;
                            _tempMatroxer.M43 = 0 + z;
                            _tempMatroxer.M44 = 1;
                            offsetPosX = _dist_between * 2;
                            offsetPosY = _dist_between * 2;
                            offsetPosZ = _dist_between * 2;
                            //_player_rght_upper_arm[_index] = new _sc_voxel();
                            //_hasinit0 = _player_rght_upper_arm.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                            //_hasinit0 = _player_rght_upper_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_upperarm_x, _inst_p_r_upperarm_y, _inst_p_r_upperarm_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerUpperArmRight, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

                            _player_rght_upper_arm[_index] = new _sc_voxel();
                            //_player_rght_upper_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerUpperArmRight, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                            _player_rght_upper_arm[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerUpperArmRight, 2, 2, 2, 2, 2, 2, 10, 9, 10, 9, 10, 9, voxel_general_size, new Vector3(0, 0, 0), 50, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld); //, "terrainGrassDirt.bmp" //0.00035f



                            worldMatrix_instances_r_upperarm[_index] = new Matrix[_inst_p_r_upperarm_x * _inst_p_r_upperarm_y * _inst_p_r_upperarm_z];
                            for (int i = 0; i < worldMatrix_instances_r_upperarm[_index].Length; i++)
                            {
                                worldMatrix_instances_r_upperarm[_index][i] = Matrix.Identity;
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
                            _tempMatroxer.M41 = 0.25f + x;
                            _tempMatroxer.M42 = (_player_rght_upper_arm[_index]._POSITION.M42 + (_player_rght_upper_arm[_index]._total_torso_height * 0.5f) + 0.45f) +y;// - 0.25f;
                            _tempMatroxer.M43 = -0.25f + z;
                            _tempMatroxer.M44 = 1;
                           offsetPosX = _dist_between * 2;
                            offsetPosY = _dist_between * 2;
                            offsetPosZ = _dist_between * 2;
                            //_player_rght_elbow_target[_index] = new _sc_voxel();

                            //_hasinit0 = _player_rght_elbow_target.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                            //_hasinit0 = _player_rght_elbow_target[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

                            _player_rght_elbow_target[_index] = new _sc_voxel();
                            _player_rght_elbow_target[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerRightElbowTarget, 2, 2, 2, 2, 2, 2, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 25, vertoffsetx, vertoffsety, vertoffsetz, 0); //, "terrainGrassDirt.bmp" //0.00035f


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
                            _tempMatroxer.M41 = -0.25f + x;
                            _tempMatroxer.M42 = (_player_lft_upper_arm[_index]._POSITION.M42 + (_player_lft_upper_arm[_index]._total_torso_height * 0.5f) + 0.45f)+x;// - 0.25f;
                            _tempMatroxer.M43 = -0.25f + z;
                            _tempMatroxer.M44 = 1;
                                offsetPosX = _dist_between * 2;
                            offsetPosY = _dist_between * 2;
                            offsetPosZ = _dist_between * 2;
                            //_player_lft_elbow_target[_index] = new _sc_voxel();
                            //_hasinit0 = _player_lft_elbow_target.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                            //_hasinit0 = _player_lft_elbow_target[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_l_hand_x, _inst_p_l_hand_y, _inst_p_l_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerLeftElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

                            _player_lft_elbow_target[_index] = new _sc_voxel();
                            _player_lft_elbow_target[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerLeftElbowTarget, 2, 2, 2, 2, 2, 2, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 25, vertoffsetx, vertoffsety, vertoffsetz, 0); //, "terrainGrassDirt.bmp" //0.00035f



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
                            _tempMatroxer.M41 = 1.5f + x;
                            _tempMatroxer.M42 = (_player_rght_upper_arm[_index]._POSITION.M42 + (_player_rght_upper_arm[_index]._total_torso_height * 0.5f) + 1) + x;
                            _tempMatroxer.M43 = 0 + z;
                            _tempMatroxer.M44 = 1;
                            offsetPosX = _dist_between * 2;
                            offsetPosY = _dist_between * 2;
                            offsetPosZ = _dist_between * 2;
                            //_player_rght_elbow_target_two[_index] = new _sc_voxel();
                            //_hasinit0 = _player_rght_elbow_target_two.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                            //_hasinit0 = _player_rght_elbow_target_two[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

                            _player_rght_elbow_target_two[_index] = new _sc_voxel();
                            _player_rght_elbow_target_two[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerRightElbowTargettwo, 2, 2, 2, 2, 2, 2, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 75, vertoffsetx, vertoffsety, vertoffsetz, 0); //, "terrainGrassDirt.bmp" //0.00035f


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
                            _tempMatroxer.M41 = -1.5f + x;
                            _tempMatroxer.M42 = (_player_lft_upper_arm[_index]._POSITION.M42 + (_player_lft_upper_arm[_index]._total_torso_height * 0.5f) + 1) + y;
                            _tempMatroxer.M43 = 0 + z;
                            _tempMatroxer.M44 = 1;
                            offsetPosX = _dist_between * 2;
                            offsetPosY = _dist_between * 2;
                            offsetPosZ = _dist_between * 2;
                            //_player_lft_elbow_target_two[_index] = new _sc_voxel();

                            //_hasinit0 = _player_lft_elbow_target_two.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                            //_hasinit0 = _player_lft_elbow_target_two[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerLeftElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                            _player_lft_elbow_target_two[_index] = new _sc_voxel();
                            _player_lft_elbow_target_two[_index].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerLeftElbowTargettwo, 2, 2, 2, 2, 2, 2, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 75, vertoffsetx, vertoffsety, vertoffsetz, 0); //, "terrainGrassDirt.bmp" //0.00035f

                            worldMatrix_instances_l_elbow_target_two[_index] = new Matrix[_inst_p_l_hand_x * _inst_p_l_hand_y * _inst_p_l_hand_z];
                            for (int i = 0; i < worldMatrix_instances_l_elbow_target_two[_index].Length; i++)
                            {
                                worldMatrix_instances_l_elbow_target_two[_index][i] = Matrix.Identity;
                            }
                        }
                    }
                }

                
                /*//SINGLEPLAYERSTUFF
                for (int i = 0; i < _world_list.Length; i++)
                {
                    _world_list[i].AddBody(_player_rght_upper_arm[0]._arrayOfInstances[0].transform.Component.rigidbody);
                    _world_list[i].AddBody(_player_rght_lower_arm[0]._arrayOfInstances[0].transform.Component.rigidbody);

                    _world_list[i].AddBody(_player_lft_upper_arm[0]._arrayOfInstances[0].transform.Component.rigidbody);
                    _world_list[i].AddBody(_player_lft_lower_arm[0]._arrayOfInstances[0].transform.Component.rigidbody);

                    _world_list[i].AddBody(_player_pelvis[0]._arrayOfInstances[0].transform.Component.rigidbody);
                    _world_list[i].AddBody(_player_torso[0]._arrayOfInstances[0].transform.Component.rigidbody);

                    _world_list[i].AddBody(_player_lft_hnd[0]._arrayOfInstances[0].transform.Component.rigidbody);
                    _world_list[i].AddBody(_player_rght_hnd[0]._arrayOfInstances[0].transform.Component.rigidbody);

                    _world_list[i].AddBody(_player_lft_shldr[0]._arrayOfInstances[0].transform.Component.rigidbody);
                    _world_list[i].AddBody(_player_rght_shldr[0]._arrayOfInstances[0].transform.Component.rigidbody);
                }*/
                

                World = _world_list[0];

                /*
                _some_reset_for_applying_force[0] = new int[_inst_screen_assets_x * _inst_screen_assets_y * _inst_screen_assets_z];
                _some_frame_counter_raycast_00[0] = new int[_inst_screen_assets_x* _inst_screen_assets_y*_inst_screen_assets_z];
                _some_frame_counter_raycast_01[0] = new int[_inst_screen_assets_x* _inst_screen_assets_y*_inst_screen_assets_z];
                _switch_for_collision[0] = new int[_inst_screen_assets_x* _inst_screen_assets_y*_inst_screen_assets_z];
                _some_last_frame_vector[0] = new JVector[_inst_screen_assets_x* _inst_screen_assets_y*_inst_screen_assets_z][];
                _some_last_frame_rigibodies[0] = new RigidBody[_inst_screen_assets_x* _inst_screen_assets_y*_inst_screen_assets_z][];

                _switch_for_collision[0] = new int[_inst_screen_assets_x* _inst_screen_assets_y*_inst_screen_assets_z];
                _objects_static_00[0] = new int[_inst_screen_assets_x* _inst_screen_assets_y*_inst_screen_assets_z];
                _objects_static_counter_00[0] = new int[_inst_screen_assets_x* _inst_screen_assets_y*_inst_screen_assets_z];
                _objects_rigid_static_00[0] = new RigidBody[_inst_screen_assets_x* _inst_screen_assets_y*_inst_screen_assets_z];







                for (int i = 0; i < _some_frame_counter_raycast_00[0].Length; i++)
                {
                    _objects_rigid_static_00[0][i] = null;
                    _objects_static_00[0][i] = 0;
                    _objects_static_counter_00[0][i] = 0;

                    var _randNum = new Random();
                    var randomer = _randNum.NextDouble(0, _some_frame_counter_raycast_00_max_counter);
                    _some_frame_counter_raycast_00[0][i] = (int)randomer;

                    _randNum = new Random();
                    randomer = _randNum.NextDouble(0, _some_frame_counter_raycast_01_max_counter);
                    _some_frame_counter_raycast_01[0][i] = (int)randomer;


                    //_randNum = new Random();
                    //randomer = _randNum.NextDouble(0, _some_frame_counter_raycast_01_max_counter);
                    //_switch_for_collision[_index][i] = (int)randomer;
                    _switch_for_collision[0][i] = 0;
                    //_some_last_frame_vector[_index][i] = JVector.Zero;
                    _some_last_frame_rigibodies[0][i] = null;
                }
                */
















                _inst_terrain_tile_x = 1;
                _inst_terrain_tile_y = 1;
                _inst_terrain_tile_z = 1;

                worldMatrix_instances_terrain[0] = new Matrix[1];
                worldMatrix_instances_terrain[0][0] = Matrix.Identity;

                //TERRAIN
                r = 0;
                g = 0;
                b = 0;
                a = 0;

 
                _object_worldmatrix = Matrix.Identity;
                _object_worldmatrix = WorldMatrix;

                _object_worldmatrix.M41 = 0;
                _object_worldmatrix.M42 = -_terrain_size_y;
                _object_worldmatrix.M43 = 0;
                _object_worldmatrix.M44 = 1;

                _offsetPos = new Vector3(0, 0, 0);

                offsetPosX = 0;
                offsetPosY = 0;
                offsetPosZ = 0;

                _terrain = new SC_cube();
                _terrain.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, _terrain_size_x, _terrain_size_y, _terrain_size_z, new Vector4(r, g, b, a), _inst_terrain_tile_x, _inst_terrain_tile_y, _inst_terrain_tile_z, Hwnd, _object_worldmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, _world_list[0], SC_console_directx.BodyTag.Terrain, true, 0, 10, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                _world_terrain = _terrain;

                for (int i = 0; i < _world_list.Length; i++)
                {
                    _world_list[i].AddBody(_terrain._singleObjectOnly.transform.Component.rigidbody);


                    _world_list[i].AddBody(_player_rght_upper_arm[0]._arrayOfInstances[0].transform.Component.rigidbody);
                    _world_list[i].AddBody(_player_rght_lower_arm[0]._arrayOfInstances[0].transform.Component.rigidbody);

                    _world_list[i].AddBody(_player_lft_upper_arm[0]._arrayOfInstances[0].transform.Component.rigidbody);
                    _world_list[i].AddBody(_player_lft_lower_arm[0]._arrayOfInstances[0].transform.Component.rigidbody);

                    _world_list[i].AddBody(_player_pelvis[0]._arrayOfInstances[0].transform.Component.rigidbody);
                    _world_list[i].AddBody(_player_torso[0]._arrayOfInstances[0].transform.Component.rigidbody);

                    _world_list[i].AddBody(_player_lft_hnd[0]._arrayOfInstances[0].transform.Component.rigidbody);
                    _world_list[i].AddBody(_player_rght_hnd[0]._arrayOfInstances[0].transform.Component.rigidbody);

                    _world_list[i].AddBody(_player_lft_shldr[0]._arrayOfInstances[0].transform.Component.rigidbody);
                    _world_list[i].AddBody(_player_rght_shldr[0]._arrayOfInstances[0].transform.Component.rigidbody);
                }

                //_offsetPos = new Vector3((_physics_engine_instance_x * 1) * 0.375f, 0, _physics_engine_instance_z * 1);
                //originPos = new SharpDX.Vector3(-3, 1, 3);

                //MessageBox((IntPtr)0, _hasinit0 + "", "Oculus error", 0);
                




                _basicTexture = new _sc_texture_loader();
                bool _hasinit1 = _basicTexture.Initialize(D3D.Device, "../../../terrainGrassDirt.bmp");
                _pink_texture = new _sc_texture_loader();
                _hasinit1 = _pink_texture.Initialize(D3D.Device, "../../../1x1_pink_color.png");
                






                
                //FOR SCREEN

                oriRotationScreenX = 0;
                oriRotationScreenY = 0;
                oriRotationScreenZ = 0;

                RotationScreenX = oriRotationScreenX;
                RotationScreenY = oriRotationScreenY;
                RotationScreenZ = oriRotationScreenZ;

                //pitcher = oriRotationScreenX * 0.0174532925f;
                //yawer = oriRotationScreenY * 0.0174532925f;
                //roller = oriRotationScreenZ * 0.0174532925f;

                var pitcher = (float)(Math.PI * (oriRotationScreenX) / 180.0f);
                var yawer = (float)(Math.PI * (oriRotationScreenY) / 180.0f);
                var roller = (float)(Math.PI * (oriRotationScreenZ) / 180.0f);


                originRotScreen = SharpDX.Matrix.RotationYawPitchRoll(yawer, pitcher, roller);
                rotatingMatrixScreen = SharpDX.Matrix.RotationYawPitchRoll(yawer, pitcher, roller);


                float oriRotationScreenX0 = 0;
                float oriRotationScreenY0 = 180;
                float oriRotationScreenZ0 = 0;

                //pitcher = oriRotationScreenX0 * 0.0174532925f;
                //yawer = oriRotationScreenY0 * 0.0174532925f;
                //roller = oriRotationScreenZ0 * 0.0174532925f;
                pitcher = (float)(Math.PI * (oriRotationScreenX0) / 180.0f);
                yawer = (float)(Math.PI * (oriRotationScreenY0) / 180.0f);
                roller = (float)(Math.PI * (oriRotationScreenZ0) / 180.0f);


                _direction_offsetter = SharpDX.Matrix.RotationYawPitchRoll(yawer, pitcher, roller);

                _screen_direction_offsetter_two = SharpDX.Matrix.RotationYawPitchRoll(0, 0, 0);


                sizeWidtherer = (float)(((float)D3D.SurfaceWidth * 0.5f) * _size_screen);
                sizeheighterer = (float)((float)(D3D.SurfaceHeight * 0.5f) * _size_screen);

                //float sizeWidther = (float)(sizeWidth * 0.5f);
                //float sizeHeighter = (float)(sizeheight * 0.5f);
                //float sizeDepther = (float)(sizedepth * 0.5f);







                //_screenCorners = new DModelClass4_cube[4];

                rotatingMatrixScreen.M41 = originPosScreen.X;
                rotatingMatrixScreen.M42 = originPosScreen.Y;
                rotatingMatrixScreen.M43 = originPosScreen.Z;

                _screenDirMatrix = new Matrix[4];
                _screenDirMatrix_correct_pos = new Matrix[4];
                point3DCollection = new Vector3[4];

                for (int i = 0; i < _screenDirMatrix.Length; i++)
                {
                    _screenDirMatrix[i] = new Matrix();
                    _screenDirMatrix[i] = rotatingMatrixScreen;
                }

                _screenDirMatrix[0].M41 = _world_screen_list[0].Vertices[16].position.X;// + originPosScreen.X;
                _screenDirMatrix[0].M42 = _world_screen_list[0].Vertices[16].position.Y;// + originPosScreen.Y;
                _screenDirMatrix[0].M43 = _world_screen_list[0].Vertices[16].position.Z;// + originPosScreen.Z;

                _screenDirMatrix[1].M41 = _world_screen_list[0].Vertices[13].position.X;// + originPosScreen.X;
                _screenDirMatrix[1].M42 = _world_screen_list[0].Vertices[13].position.Y;// + originPosScreen.Y;
                _screenDirMatrix[1].M43 = _world_screen_list[0].Vertices[13].position.Z;// + originPosScreen.Z;

                _screenDirMatrix[2].M41 = _world_screen_list[0].Vertices[15].position.X;// + originPosScreen.X;
                _screenDirMatrix[2].M42 = _world_screen_list[0].Vertices[15].position.Y;// + originPosScreen.Y;
                _screenDirMatrix[2].M43 = _world_screen_list[0].Vertices[15].position.Z;// + originPosScreen.Z;

                _screenDirMatrix[3].M41 = _world_screen_list[0].Vertices[17].position.X;// + originPosScreen.X;
                _screenDirMatrix[3].M42 = _world_screen_list[0].Vertices[17].position.Y;// + originPosScreen.Y;
                _screenDirMatrix[3].M43 = _world_screen_list[0].Vertices[17].position.Z;// + originPosScreen.Z;

                //16//13//15//17 
                //8//9//10//11

                for (int i = 0; i < _screenDirMatrix.Length; i++)
                {
                    point3DCollection[i] = new Vector3(_screenDirMatrix[i].M41, _screenDirMatrix[i].M42, _screenDirMatrix[i].M43);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void ShutDown()
        {
            Camera = null;
            D3D?.ShutDown();
            D3D = null;
        }

        _sc_message_object._sc_message_object[] _data_in; // sometimes this shit is
        public _sc_message_object._sc_message_object[] FrameVRTWO(_sc_message_object._sc_message_object[] _main_received_object)
        {
            try
            {
                _data_in = Render(_main_received_object);
            }
            catch (Exception ex)
            {
                MessageBox((IntPtr)0, ex.ToString() + "", "Oculus error", 0);
            }
            return _data_in;// Render(_main_received_object);
        }









        ////////////////////////////////////////////
        ////////////////////////////////////////////
        ////////////////////////////////////////////
        ///////////////////RENDER///////////////////
        ////////////////////////////////////////////
        ////////////////////////////////////////////
        ////////////////////////////////////////////
        int _failed = 0;
        static Matrix _other_left_touch_matrix = Matrix.Identity;
        private _sc_message_object._sc_message_object[] Render(_sc_message_object._sc_message_object[] _main_received_object)
        {
            var _indexer = 0;// x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z);










            //HEADSET POSITION
            displayMidpoint = D3D.OVR.GetPredictedDisplayTime(D3D.sessionPtr, 0);
            trackingState = D3D.OVR.GetTrackingState(D3D.sessionPtr, displayMidpoint, true);
            latencyMark = false;
            trackState = D3D.OVR.GetTrackingState(D3D.sessionPtr, 0.0f, latencyMark);
            poseStatefer = trackState.HeadPose;
            hmdPose = poseStatefer.ThePose;
            hmdRot = hmdPose.Orientation;
            _hmdPoser = new Vector3(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);
            _hmdRoter = new Quaternion(hmdPose.Orientation.X, hmdPose.Orientation.Y, hmdPose.Orientation.Z, hmdPose.Orientation.W);
            //SET CAMERA POSITION
            Camera.SetPosition(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);


            Quaternion quatter = new Quaternion(hmdRot.X, hmdRot.Y, hmdRot.Z, hmdRot.W);
            Vector3 oculusRiftDir = _getDirection(Vector3.ForwardRH, quatter);

            //TOUCH CONTROLLER RIGHT
            resultsRight = D3D.OVR.GetInputState(D3D.sessionPtr, D3D.controllerTypeRTouch, ref D3D.inputStateRTouch);
            buttonPressedOculusTouchRight = D3D.inputStateRTouch.Buttons;
            thumbStickRight = D3D.inputStateRTouch.Thumbstick;
            handTriggerRight = D3D.inputStateRTouch.HandTrigger;
            indexTriggerRight = D3D.inputStateRTouch.IndexTrigger;
            handPoseRight = trackingState.HandPoses[1].ThePose;

            _rightTouchQuat.X = handPoseRight.Orientation.X;
            _rightTouchQuat.Y = handPoseRight.Orientation.Y;
            _rightTouchQuat.Z = handPoseRight.Orientation.Z;
            _rightTouchQuat.W = handPoseRight.Orientation.W;

            _rightTouchQuat = new SharpDX.Quaternion(handPoseRight.Orientation.X, handPoseRight.Orientation.Y, handPoseRight.Orientation.Z, handPoseRight.Orientation.W);

            SharpDX.Matrix.RotationQuaternion(ref _rightTouchQuat, out _rightTouchMatrix);

            //_rightTouchMatrix.M41 = handPoseRight.Position.X + originPos.X;
            //_rightTouchMatrix.M42 = handPoseRight.Position.Y + originPos.Y;
            //_rightTouchMatrix.M43 = handPoseRight.Position.Z + originPos.Z;

            _rightTouchMatrix.M41 = handPoseRight.Position.X + originPos.X + movePos.X;// + _hmdPoser.X;
            _rightTouchMatrix.M42 = handPoseRight.Position.Y + originPos.Y + movePos.Y;// + _hmdPoser.Y;
            _rightTouchMatrix.M43 = handPoseRight.Position.Z + originPos.Z + movePos.Z;// + _hmdPoser.Z;

            //TOUCH CONTROLLER LEFT
            resultsLeft = D3D.OVR.GetInputState(D3D.sessionPtr, D3D.controllerTypeLTouch, ref D3D.inputStateLTouch);
            buttonPressedOculusTouchLeft = D3D.inputStateLTouch.Buttons;
            thumbStickLeft = D3D.inputStateLTouch.Thumbstick;
            handTriggerLeft = D3D.inputStateLTouch.HandTrigger;
            indexTriggerLeft = D3D.inputStateLTouch.IndexTrigger;
            handPoseLeft = trackingState.HandPoses[0].ThePose;

            _leftTouchQuat.X = handPoseLeft.Orientation.X;
            _leftTouchQuat.Y = handPoseLeft.Orientation.Y;
            _leftTouchQuat.Z = handPoseLeft.Orientation.Z;
            _leftTouchQuat.W = handPoseLeft.Orientation.W;

            _leftTouchQuat = new SharpDX.Quaternion(handPoseLeft.Orientation.X, handPoseLeft.Orientation.Y, handPoseLeft.Orientation.Z, handPoseLeft.Orientation.W);

            SharpDX.Matrix.RotationQuaternion(ref _leftTouchQuat, out _leftTouchMatrix);
            _other_left_touch_matrix = _leftTouchMatrix;
            _other_left_touch_matrix.M41 = handPoseLeft.Position.X;
            _other_left_touch_matrix.M42 = handPoseLeft.Position.Y;
            _other_left_touch_matrix.M43 = handPoseLeft.Position.Z;

            //_leftTouchMatrix.M41 = handPoseLeft.Position.X + originPos.X + movePos.X;// + _hmdPoser.X;
            //_leftTouchMatrix.M42 = handPoseLeft.Position.Y + originPos.Y + movePos.Y;// + _hmdPoser.Y;
            //_leftTouchMatrix.M43 = handPoseLeft.Position.Z + originPos.Z + movePos.Z;// + _hmdPoser.Z;

            _leftTouchMatrix.M41 = handPoseLeft.Position.X + originPos.X + movePos.X;// + _hmdPoser.X;
            _leftTouchMatrix.M42 = handPoseLeft.Position.Y + originPos.Y + movePos.Y;// + _hmdPoser.Y;
            _leftTouchMatrix.M43 = handPoseLeft.Position.Z + originPos.Z + movePos.Z;// + _hmdPoser.Z;




            planer = new Plane(new Vector3(worldMatrix_instances_screens[0][0].M41, worldMatrix_instances_screens[0][0].M42, worldMatrix_instances_screens[0][0].M43), screenNormal);

            //FOR SCREEN COLLISION DETECTION
            centerPosRight = new Vector3(final_hand_pos_right_locked.M41, final_hand_pos_right_locked.M42, final_hand_pos_right_locked.M43);
            Quaternion.RotationMatrix(ref final_hand_pos_right_locked, out _rightTouchQuat);
            rayDirRight = _getDirection(Vector3.ForwardRH, _rightTouchQuat);
            someRay = new Ray(centerPosRight, rayDirRight);
            intersecter = someRay.Intersects(ref planer, out intersectPointRight);
            _ray_dir_right = centerPosRight - intersectPointRight;
            _length_of_ray_right = (_ray_dir_right).Length();

            centerPosLeft = new Vector3(final_hand_pos_left_locked.M41, final_hand_pos_left_locked.M42, final_hand_pos_left_locked.M43);
            Quaternion.RotationMatrix(ref final_hand_pos_left_locked, out _leftTouchQuat);
            rayDirLeft = _getDirection(Vector3.ForwardRH, _leftTouchQuat);
            someRayLeft = new Ray(centerPosLeft, rayDirLeft);
            intersecterLeft = someRayLeft.Intersects(ref planer, out intersectPointLeft);
            _ray_dir_left = centerPosLeft - intersectPointLeft;
            _length_of_ray_left = (_ray_dir_left).Length();













            /*if (_some_frame_counter_randomizer_switch_counter > 1000)
            {
                for (int i = 0; i < _some_frame_counter_raycast_00.Length; i++)
                {
                    for (int j = 0; j < _some_frame_counter_raycast_00[i].Length; j++)
                    {
                        var _randNum = new Random();
                        double randomer = _randNum.NextDouble(0, _some_frame_counter_raycast_00_max_counter);
                        _some_frame_counter_raycast_00[i][j] = (int)randomer;

                        _randNum = new Random();
                        randomer = _randNum.NextDouble(0, _some_frame_counter_raycast_01_max_counter);
                        _some_frame_counter_raycast_01[i][j] = (int)randomer;

                    }
                }
                //_some_frame_counter_randomizer_switch = 1;
                _some_frame_counter_randomizer_switch_counter = 0;
            }
            else
            {
                //_some_frame_counter_randomizer_switch_counter = 0;
            }
            _some_frame_counter_randomizer_switch_counter++;*/





            if (handTriggerRight[1] >= 0.0001f)
            {
                if (_has_grabbed_right_swtch == 0)
                {
                    _has_grabbed_right_swtch = 1;
                }
                else
                {

                }
            }
            else
            {
                if (_grab_rigid_data._body != null)
                {
                    //_grab_rigid_data._body = null;
                    //_objects_rigid_static_00[0][0] = _grab_rigid_data._body;
                    //_objects_static_00[0][0] = 1;

                    /*if (!_grab_rigid_data._body.IsStatic)
                    {
                        _objects_rigid_static_00[0][0] = _grab_rigid_data._body;
                        _objects_static_00[0][0] = 1;
                        Matrix.Translation(_grab_rigid_data._body.Position.X, _grab_rigid_data._body.Position.Y, _grab_rigid_data._body.Position.Z, out translationMatrix);
                        quatterer = JQuaternion.CreateFromMatrix(_grab_rigid_data._body.Orientation);
                        tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                        Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                        Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);

                        //var dirToRightHand = new Vector3(final_hand_pos_left.M41, final_hand_pos_left.M42, final_hand_pos_left.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                        //var lengthofdir = dirToRightHand.Length();
                        //dirToRightHand.Normalize();

                        JVector dirFromLastFrame = _grab_rigid_data._body.Position - new JVector(world_last_Matrix_instances_screens[0][count].M41, world_last_Matrix_instances_screens[0][count].M42, world_last_Matrix_instances_screens[0][count].M43);
                        dirFromLastFrame.Normalize();        
                        
                        //var _force = handTriggerLeft[0] * 0.01f;

                        var force = new JVector(dirFromLastFrame.X, dirFromLastFrame.Y, dirFromLastFrame.Z) * 0.00001f;
                        _grab_rigid_data._body.LinearVelocity += force;
                        _grab_rigid_data._body.AddForce(force);

                        //_grab_rigid_data._body = null;
                    }*/

                    //_grab_rigid_data._body.AffectedByGravity = true;
                    //_grab_rigid_data._body.IsActive = true;
                    //_grab_rigid_data._body.IsStatic = false;
                    //_grab_rigid_data._body = null;*/
                }

                RotationGrabbedX = 0;
                RotationGrabbedY = 0;
                RotationGrabbedZ = 0;

                _had_grabbed = 0;
                _swtch_hasRotated = 0;
                _has_grabbed_right = 0;
                _has_grabbed_right_swtch = 0;
                _sec_logic_swtch_grab = 0;
                _tier_logic_swtch_grab = 0;
            }








            
            if (_updateFunctionBoolRight)
            {
                _updateFunctionStopwatchRight.Stop();
                _updateFunctionStopwatchRight.Reset();
                _updateFunctionStopwatchRight.Start();
                _updateFunctionBoolRight = false;
            }
            if (_updateFunctionBoolRightThumbStickGoLeft)
            {
                _updateFunctionStopwatchRightThumbstickGoLeft.Stop();
                _updateFunctionStopwatchRightThumbstickGoLeft.Reset();
                _updateFunctionStopwatchRightThumbstickGoLeft.Start();
                _updateFunctionBoolRightThumbStickGoLeft = false;
            }

            if (_updateFunctionBoolRightThumbStickGoRight)
            {
                _updateFunctionStopwatchRightThumbstickGoRight.Stop();
                _updateFunctionStopwatchRightThumbstickGoRight.Reset();
                _updateFunctionStopwatchRightThumbstickGoRight.Start();
                _updateFunctionBoolRightThumbStickGoRight = false;
            }

            if (_updateFunctionBoolLeftThumbStick)
            {
                _updateFunctionStopwatchLeftThumbstick.Stop();
                _updateFunctionStopwatchLeftThumbstick.Reset();
                _updateFunctionStopwatchLeftThumbstick.Start();
                _updateFunctionBoolLeftThumbStick = false;
            }

            
            for (int i = 0; i < 3;)
            {
                _failed = 0;
                try
                {
                    _desktopFrame = _desktopDupe.ScreenCapture(0);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.ToString());
                    _desktopDupe = new SC_SharpDX_ScreenCapture(0, 0, D3D.device);
                    _failed = 1;
                }
                i++;
                if (_failed == 0)
                {
                    //Console.WriteLine("test");
                    break;
                }
            }













            //DISCARDED - TO READD
            //doc.Load(path);
            //var nodelist = doc.SelectSingleNode("root");
            //if (nodelist != null)
            //doc.Save(path);

            //DISCARDED TO RE-ADD - write to file username and password.
            //XmlAttribute node_attrib = doc.CreateAttribute("username");// = new XmlAttribute("some new attrib");
            //node_attrib.Value = _lastUsername;
            //nodelist.Attributes.Append(node_attrib);

            //DISCARDED TO RE-ADD - write to file new node and inner text but not fancy/correctly "formatted"
            //XmlNode newElem = doc.CreateNode("element", "dataOfelement", "");
            //newElem.InnerText = "Inner Text";
            //XmlElement root = doc.DocumentElement;
            //root.AppendChild(newElem);

            //DISCARDED TO RE-CHECK 
            //Beautify(doc); beautify what exactly? did it work?

            //XmlDocument dok = new XmlDocument();
            //dok.LoadXml("<book xmlns:bk='urn:samples' bk:ISBN='1-861001-57-5'>" +
            //            "<title>Pride And Prejudice</title>" +
            //            "</book>");
            //XmlElement root = dok.DocumentElement;
            // Add a new attribute.
            //root.SetAttribute("genre", "urn:samples", "novel");
            //Console.WriteLine("Display the modified XML...");
            //XmlTextWriter writer = new XmlTextWriter(Console.Out);
            //writer.Formatting = Formatting.Indented;
            //root.WriteTo(writer);

            //XmlNode _new_node = doc.CreateNode(, , "");
            //nodelist.AppendChild(_new_node);
            //XmlDocument doc = new XmlDocument();
            //doc.PreserveWhitespace = true;


            /*
            //RECORD SOUND
            if (Program._keyboard_input._KeyboardState.PressedKeys.Contains(Key.R))
            {
                if (sc_start_recording == 0)
                {
                    _time_of_recording_start = DateTime.Now;

                    mciSendString("open new Type waveaudio Alias recsound", null, 0, IntPtr.Zero);
                    mciSendString("record recsound", null, 0, IntPtr.Zero);

                    sc_start_recording = 1;
                }
            }

            if (sc_start_recording == 2)
            {
                if (sc_start_recording_counter >= 150)
                {
                    sc_start_recording = 0;
                    sc_start_recording_counter = 0;
                }
                sc_start_recording_counter++;
            }

            if (sc_save_file == 1)
            {
                System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                customCulture.NumberFormat.NumberDecimalSeparator = ".";
                System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

                //path = "c:\\Users\\ninekorn\\Desktop\\testXML\\" + 0 + ".xml";
                path = @"C:\Users\ninekorn\Desktop\#RECSOUND\" + "wave_audio_" + sc_can_start_rec_counter + ".xml";
                last_xml_filepath = path;
                sc_can_start_rec_counter_before_add_index = sc_can_start_rec_counter;
                //doc = new XmlDocument();
                //writer = new XmlTextWriter(Console.Out);
                writer = new XmlTextWriter(path, System.Text.Encoding.UTF8);

                //root = doc.DocumentElement;

                writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");

                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;

                writer.WriteStartElement("root"); // open 0
                writer.WriteStartElement("wave"); // open 1
                writer.WriteStartElement("data"); // open 2



                writer.WriteStartElement("StartTime"); //open 3
                writer.WriteString("" + _time_of_recording_start);
                writer.WriteEndElement(); //close 3

                _time_of_recording_end = DateTime.Now;
                writer.WriteStartElement("EndTime"); //open 3
                writer.WriteString("" + _time_of_recording_end);
                writer.WriteEndElement(); //close 3

                writer.WriteStartElement("size"); //open 3
                long size = new System.IO.FileInfo(last_wave_filepath).Length;
                writer.WriteString("" + size);
                writer.WriteEndElement(); //close 3

                writer.WriteStartElement("length"); //open 4
                int length = GetSoundLength(last_wave_filepath);
                writer.WriteString("" + length);
                writer.WriteEndElement(); //close 4


                writer.WriteEndElement(); //close 0
                writer.WriteEndElement(); //close 1
                writer.WriteEndElement(); //close 2

                writer.Close();
                //root.WriteTo(writer);

                //doc.Save(path);
                sc_can_start_rec_counter++;
                _records_counter++;
                sc_save_file = 2;
            }

            //Process p = new Process();
            //p.StartInfo = new ProcessStartInfo()
            //{
            //    FileName = _lastpath
            //};
            //p.Start();
            //p.Refresh();
            //p.Close();
            

            if (_frames_to_access_counter >= 0)
            {
                if (_records_counter > 0)
                {

                }
                //if (_frames_to_access == 0)
                //{
                //
                //    _frames_to_access = 1;
                //}

                _frames_to_access_counter = 0;
            }
















            //SAVE SOUND
            if (Program._keyboard_input._KeyboardState.PressedKeys.Contains(Key.S))
            {
                if (sc_start_recording == 1)
                {
                    if (sc_save_file == 0)
                    {
                        var filename = @"C:\Users\ninekorn\Desktop\#RECSOUND\" + "wave_audio_" + sc_can_start_rec_counter_before_add_index + ".wav";
                        mciSendString("save recsound " + filename, null, 0, IntPtr.Zero);
                        mciSendString("close recsound", null, 0, IntPtr.Zero);
                        _has_recorded = 1;
                        last_wave_filepath = filename;

                        sc_start_recording = 2;
                        sc_save_file = 1;
                    }
                }
            }

            //PLAY SOUND AT INDEX
            if (Program._keyboard_input._KeyboardState.PressedKeys.Contains(Key.P))
            {
                if (sc_play_file == 0)
                {
                    _index = sc_can_start_rec_counter_before_add_index + "";
                    _sound_player.AddWave(_index, last_wave_filepath);

                    var nativeFileStream = new NativeFileStream(last_wave_filepath, NativeFileMode.Open, NativeFileAccess.Read, NativeFileShare.Read);
                    SoundStream soundStream = new SoundStream(nativeFileStream);
                    MemoryStream ms = new MemoryStream();

                    soundStream.CopyTo(ms);
                    _sound_byte_array = ms.ToArray();






                    //Array.Sort(_sound_byte_array);
                    //_sound_byte_array.OrderBy(x > x).FirstOrDefault();
                    //lowestX = point3DCollection.OrderBy(x => x.X).FirstOrDefault();
                    //highestX = point3DCollection.OrderBy(x => x.X).Last();




                    soundStream.Dispose();
                    ms.Dispose();

                    //for (int s = 0; s < _sound_byte_array.Length;s++)
                    //{
                    //    if (_sound_byte_array[s] > 100)
                    //    {
                    //        MessageBox((IntPtr)0, "" + "has sound", "Oculus Message", 0);
                    //    }
                    //    //MessageBox((IntPtr)0, "" + "has recorded", "Oculus Error", 0);
                    //}
                    //_main_received_object[0]._someData[0] = _sound_byte_array;// new object[1];



                    ///MessageBox((IntPtr)0, "" + _sound_byte_array.Length, "_sc_core_systems error", 0);
                    //using (var soundStream = new SoundStream(nativeFileStream))
                    //{
                    //    using (MemoryStream ms = new MemoryStream())
                    //    {
                    //        soundStream.CopyTo(ms);
                    //        _sound_byte_array = ms.ToArray();
                    //        _main_received_messages[0]._someData[0] = _sound_byte_array;// new object[1];
                    //    }
                    //}

                    _sound_player.Play(_index);
                    sc_play_file = 1;
                }
            }

            if (sc_play_file == 1)
            {
                if (sc_play_file_counter >= 250)
                {
                    sc_play_file = 0;
                    sc_play_file_counter = 0;
                }
                sc_play_file_counter++;
            }*/




            /*if (_lockedFrameCounter >= 100)
            {
                if (Program._keyboard_input._KeyboardState.PressedKeys.Contains(Key.L))
                {
                    _can_work_physics = 1;
                    _hasLockedkey = 1;
                    _lockedFrameCounter = 0;
                }
                else
                {
                    _hasLockedkey = 0;
                }     
            }*/

            _lockedFrameCounter++;


            //LOCKING THE POSITION OF THE SCREEN
            if (_has_locked_screen_pos_counter >= 50)
            {
                if (buttonPressedOculusTouchLeft == 256)//Program._KeyboardState.PressedKeys.Contains(Key.Space))
                {
                    if (_has_locked_screen_pos == 0)
                    {
                        _has_locked_screen_pos_counter = 0;
                        _has_locked_screen_pos = 1;
                    }
                    if (_has_locked_screen_pos == 3)
                    {
                        _has_locked_screen_pos_counter = 0;
                        _has_locked_screen_pos = 0;
                    }
                }
            }

            _has_locked_screen_pos_counter++;



            //D3D.hmdDesc.
            //var yo = D3D.OVR.GetTrackerPose(D3D.sessionPtr, (uint)TrackedDeviceType.HMD);
            //var oculusR_pos = yo.Pose.Position;
            //var oculusR_rot = yo.Pose.Orientation;
            //var oculusRiftQuat = new Quaternion(oculusR_rot.X, oculusR_rot.Y, oculusR_rot.Z, oculusR_rot.W);
            //var oculusRiftPos = new Vector3(oculusR_pos.X, oculusR_pos.Y, oculusR_pos.Z);




            /*else
            {
                float distance = sc_maths.sc_check_distance_node_3d_geometry(handPos, screen_pos, 10, 10, 10, 10, 10, 10); //11.31415926535f

                if (distance < 25)
                {

                }

            }*/








            if (_swtch_hasRotated == 1)
            {

            }
            else if (_swtch_hasRotated == 0)
            {
                //RotationGrabbedX = 0;
                //RotationGrabbedY = 0;
                //RotationGrabbedZ = 0;
            }

            Quaternion otherQuat;
            Quaternion.RotationMatrix(ref rotatingMatrixForPelvis, out otherQuat);


            Vector3 direction_feet_forward;
            Vector3 direction_feet_right;
            Vector3 direction_feet_up;

            direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
            direction_feet_right = _getDirection(Vector3.Right, otherQuat);
            direction_feet_up = _getDirection(Vector3.Up, otherQuat);

            if (thumbStickLeft[0].X > 0.15f)
            {
                movePos += direction_feet_right * speedPos * thumbStickLeft[0].X;
            }
            else if (thumbStickLeft[0].X < -0.15f)
            {
                movePos -= direction_feet_right * speedPos * -thumbStickLeft[0].X;
            }

            if (thumbStickLeft[0].Y > 0.15f)
            {
                movePos += direction_feet_forward * speedPos * thumbStickLeft[0].Y;
            }
            else if (thumbStickLeft[0].Y < -0.15f)
            {
                movePos -= direction_feet_forward * speedPos * -thumbStickLeft[0].Y;
            }

            OFFSETPOS = originPos + movePos;


















            //wtf? I DONT REMEMBER WHY I PUT THAT THERE
            try
            {
                originRot.M41 = 0;
                originRot.M42 = 0;
                originRot.M43 = 0;
                originRot.M44 = 1;

                rotatingMatrix.M41 = 0;
                rotatingMatrix.M42 = 0;
                rotatingMatrix.M43 = 0;
                rotatingMatrix.M44 = 1;

                rotatingMatrixForGrabber.M41 = 0;
                rotatingMatrixForGrabber.M42 = 0;
                rotatingMatrixForGrabber.M43 = 0;
                rotatingMatrixForGrabber.M44 = 1;
            }
            catch (Exception ex)
            {
                MessageBox((IntPtr)0, ex.ToString() + "", "Oculus error", 0);
            }








            try
            {








                /*if (_sound_byte_array.Length <  )
                {
                    //_sound_byte_array
                }*/

                /*if (_has_recorded == 2)
                {
                    MessageBox((IntPtr)0, _has_recorded + "", "Oculus error", 0);
                }*/
                //MessageBox((IntPtr)0, _has_recorded + "", "Oculus error", 0);



       


                if (_start_background_worker_01 == 1)
                {
                    //PHYSICS ENGINE STEPS. 
                    if (_can_work_physics == 1)
                    {
                        if (_start_background_worker_00 == 0)
                        {
                            BackgroundWorker threaders = new BackgroundWorker();
                            threaders.DoWork += (object sender, DoWorkEventArgs argers) =>
                            {
                                Stopwatch _this_thread_ticks_01 = new Stopwatch();
                                _this_thread_ticks_01.Start();

                            _thread_looper:

                                //var _delta_timer = (float)Math.Abs((_this_thread_ticks_01.Elapsed.Ticks - _this_thread_ticks_00.Elapsed.Ticks)) * 0.0000000001f; // very slow /100000000000

                                for (int x = 0; x < _physics_engine_instance_x; x++)
                                {
                                    for (int y = 0; y < _physics_engine_instance_y; y++)
                                    {
                                        for (int z = 0; z < _physics_engine_instance_z; z++)
                                        {
                                            if (_world_list[x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z)] != null)
                                            {
                                                if (deltaTime > 1.0f * 0.01f)
                                                {
                                                    deltaTime = 1.0f * 0.01f;
                                                }

                                                _world_list[x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z)].Step(deltaTime, true);
                                            }
                                        }
                                    }
                                }
                                //MessageBox((IntPtr)0, _this_thread_ticks.ElapsedTicks + "", "Oculus error", 0);
                                //Console.WriteLine("ticks: " + _this_thread_ticks.ElapsedTicks);

                                Thread.Sleep(1);
                                goto _thread_looper;
                            };

                            threaders.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs argers)
                            {

                            };

                            threaders.RunWorkerAsync();
                            _start_background_worker_00 = 1;
                            _start_background_worker_01 = 2;
                            _can_work_physics = 2;
                        }
                    }
                    //END OF
                }






                //AFTER PHYSICS ENGINE HAS WORKED, YOU CAN GET THE POSITION OR SET THE NEW POSITION OF THE OBJECTS. I PREFER NOT TO DO IT BEFORE THE PHYSICS ENGINE HAS WORKED.
                if (_start_background_worker_01 == 0)
                {
                    BackgroundWorker threaders = new BackgroundWorker();
                    threaders.DoWork += (object sender, DoWorkEventArgs argers) =>
                    {
                    //object[] parametors = args.Argument as object[];
                    //var workor = (_task_worker)parametors[0];
                    //var parameters[] = _array_stop_watch_tick[_index];
                    //int _indexer = workor._worker_task_id;

                    _thread_looper:



                        for (int x = 0; x < _physics_engine_instance_x; x++)
                        {
                            for (int y = 0; y < _physics_engine_instance_y; y++)
                            {
                                for (int z = 0; z < _physics_engine_instance_z; z++)
                                {

                                    var _index = x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z);

                                    /*var _cuber_pelvis = _player_pelvis[_index];

                                    for (int i = 0; i < worldMatrix_instances_pelvis[_index].Length; i++)
                                    {
                                        Matrix _mat = _cuber_pelvis._arrayOfInstances[i]._POSITION;
                                        _mat.M41 += OFFSETPOS.X;
                                        _mat.M42 += OFFSETPOS.Y;
                                        _mat.M43 += OFFSETPOS.Z;

                                        worldMatrix_instances_pelvis[_index][i] = _mat;
                                    }*/



                                    if (buttonPressedOculusTouchRight != 0)
                                    {
                                        if (buttonPressedOculusTouchRight == 2)
                                        {
                                            _world_list[_index].Gravity = new JVector(0, 0, 0);
                                        }
                                    }

                                    if (buttonPressedOculusTouchLeft != 0)
                                    {
                                        if (buttonPressedOculusTouchLeft == 512)
                                        {
                                            _world_list[_index].Gravity = new JVector(0, -9.81f, 0);
                                        }
                                    }

                                    int _cube_counter = 0;
                                    int _terrain_tiles_count = 0;
                                    int _inactive_counter = 0;
                                    int _terrain_count = 0;
                                    int _spectrum_count = 0;

                                    int _screen_asset_counter = 0;
                                    int _screen_counter = 0;
                                    int _voxel_cube_counter = 0;

                                    int _add_one_swtch = 0;
                                    float _add_one = 0;
                                    int _add_one_counter = 0;

                                    float _z_remove = 0;



                                    int p_r_hnd_count = 0;
                                    int p_l_hnd_count = 0;

                                    int p_l_shldr_count = 0;
                                    int p_r_shldr_count = 0;

                                    int p_l_lowerA_count = 0;
                                    int p_r_lowerA_count = 0;

                                    int p_l_upperA_count = 0;
                                    int p_r_upperA_count = 0;


                                    int p_l_target_count = 0;
                                    int p_r_target_count = 0;

                                    int p_l_target_two_count = 0;
                                    int p_r_target_two_count = 0;


                                    int p_torso_count = 0;
                                    int p_pelvis_count = 0;


                                    int _iterator = 0;
                                    if (_world_list[_index] != null)
                                    {
                                        if (_world_list[_index].RigidBodies.Count > 0)
                                        {
                                            enumerator = _world_list[_index].RigidBodies.GetEnumerator();

                                            while (enumerator.MoveNext())
                                            {
                                                body = (RigidBody)enumerator.Current;

                                                if (body != null && body.Tag != null)
                                                {

                                                    if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.physicsInstancedScreen)
                                                    {
                                                        //Console.WriteLine(body.IsStatic + "" + " ___ " + _objects_static_00[0][0]);

                                                        /*if (sc_body_last_linearvelo.Length() < current_linear_velo.Length())
                                                        {
                                                          
                                                            //if (current_linear_velo.Length() > 0.1f)
                                                            //{
                                                            //   
                                                            //}
                                                        }
                                                        if (sc_body_last_angularvelo.Length() < current_angular_velo.Length())
                                                        {
                                                     
                                                            //if (current_angular_velo.Length() > 0.1f)
                                                            //{
                                                            //    
                                                            //}
                                                        }*/

                                                        /*if (force_to_apply_next_frame_swtch == 0)
                                                        {

                                                        }*/

                                                        if (!body.IsStatic)
                                                        {
                                                            if (handTriggerLeft[0] >= 0.001f)
                                                            {
                                                                Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);

                                                                var dirToRightHand = new Vector3(_player_lft_hnd[_index]._arrayOfInstances[0].current_pos.M41, _player_lft_hnd[_index]._arrayOfInstances[0].current_pos.M42, _player_lft_hnd[_index]._arrayOfInstances[0].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);                                                               
                                                                //FOR MULTIPLAYER OR JUST FOR THE INSTANCES.
                                                                //var dirToRightHand = new Vector3(_player_lft_hnd[_index]._arrayOfInstances[_screen_counter].current_pos.M41, _player_lft_hnd[_index]._arrayOfInstances[_screen_counter].current_pos.M42, _player_lft_hnd[_index]._arrayOfInstances[_screen_counter].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                var lengthofdir = dirToRightHand.Length();
                                                                dirToRightHand.Normalize();


                                                                var _force = handTriggerLeft[0] * 0.01f;
                                                                var force = new JVector(dirToRightHand.X, dirToRightHand.Y, dirToRightHand.Z) * _force;

                                                                body.LinearVelocity += force;
                                                                body.AddForce(force);
                                                                _has_used_trigger = 1;
                                                            }
                                                            else
                                                            {
                                                                //Vector3 sc_body_last_angularvelo = _world_screen_list[0]._singleObjectOnly.sc_last_frame_angularvelocity;
                                                                //Vector3 sc_body_last_linearvelo = _world_screen_list[0]._singleObjectOnly.sc_last_frame_angularvelocity;

                                                                //JVector current_angular_velo = body.AngularVelocity;
                                                                //JVector current_linear_velo = body.LinearVelocity;

                                                                //if (current_linear_velo.Length() > 0.1f)
                                                                //{
                                                                //    body.LinearVelocity *= 0.5f;
                                                                //}

                                                                //if (current_angular_velo.Length() > 0.1f)
                                                                //{
                                                                //    body.AngularVelocity *= 0.5f;
                                                                //}
                                                            }
                                                        }

                                                        Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                        quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                        tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                        Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                        Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                        worldMatrix_instances_screens[_index][_screen_counter] = translationMatrix;

                                                        _screen_counter++;



                                                        /*if (_has_locked_screen_pos == 0)
                                                        {
                                                            if (!body.IsStatic)
                                                            {
                                                                if (_grab_rigid_data._body != null)
                                                                {
                                                                    if (body == _grab_rigid_data._body)
                                                                    {
                                                                        if (_has_grabbed_right_swtch == 2)
                                                                        {
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }*/
                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag._voxel_spheroid)
                                                    {
                                                        if (!body.IsStatic)
                                                        {
                                                            if (handTriggerRight[0] >= 0.001f)
                                                            {
                                                                Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);

                                                                var dirToRightHand = new Vector3(_player_rght_hnd[_index]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[_index]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[_index]._arrayOfInstances[0].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                //var dirToRightHand = new Vector3(_player_lft_hnd[_index]._arrayOfInstances[_voxel_cube_counter].current_pos.M41, _player_lft_hnd[_index]._arrayOfInstances[_voxel_cube_counter].current_pos.M42, _player_lft_hnd[_index]._arrayOfInstances[_voxel_cube_counter].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                var lengthofdir = dirToRightHand.Length();
                                                                dirToRightHand.Normalize();

                                                                var _force = handTriggerRight[0] * 0.01f;
                                                                var force = new JVector(dirToRightHand.X, dirToRightHand.Y, dirToRightHand.Z) * _force;

                                                                body.LinearVelocity += force;
                                                                body.AddForce(force);
                                                                _has_used_trigger = 1;
                                                            }
                                                            Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                            quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                            tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                            Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                            Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                            worldMatrix_instances_voxel_cube[_index][_voxel_cube_counter] = translationMatrix;
                                                        }
                                                        _voxel_cube_counter++;
                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag._screen_assets)
                                                    {

                                                        /*if (_screen_asset_counter < 4)
                                                        {
                                                            Quaternion _quaternion_locked_body_matrix;
                                                            Quaternion.RotationMatrix(ref worldMatrix_instances_screen_assets[0][_screen_asset_counter], out _quaternion_locked_body_matrix);
                                                            //body.Orientation = JMatrix.CreateFromQuaternion(new JQuaternion(_quaternion_locked_body_matrix.X, _quaternion_locked_body_matrix.Y, _quaternion_locked_body_matrix.Z, _quaternion_locked_body_matrix.W));
                                                            //body.Position = new JVector(worldMatrix_instances_screen_assets[0][_screen_asset_counter].M41, worldMatrix_instances_screen_assets[0][_screen_asset_counter].M42, worldMatrix_instances_screen_assets[0][_screen_asset_counter].M43);
                                                            //body.LinearVelocity = JVector.Zero;
                                                            //body.AngularVelocity = JVector.Zero;

                                                        }*/







                                                        /*worldMatrix_instances_screen_assets[_index][_screen_asset_counter] = _screenDirMatrix_correct_pos[_screen_asset_counter];

                                                        JVector bodyPos = new JVector(worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M41,
                                                                                      worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M42,
                                                                                      worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M43);

                                                        Quaternion quat;
                                                        Quaternion.RotationMatrix(ref worldMatrix_instances_screen_assets[_index][_screen_asset_counter], out quat);

                                                        JQuaternion jQuatter = new JQuaternion(quat.X, quat.Y, quat.Z, quat.W);

                                                        JMatrix jMatrixer = JMatrix.CreateFromQuaternion(jQuatter);
                                                        body.Position = bodyPos;
                                                        body.Orientation = jMatrixer;

                                                        //_world_screen_corner_cube_list[x + width * (y + height * z)]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(worldMatrix_screen_cube_corners_instances[indexer][i].M41, worldMatrix_screen_cube_corners_instances[indexer][i].M42, worldMatrix_screen_cube_corners_instances[indexer][i].M43);
                                                        //Quaternion rigidRot;
                                                        //Quaternion.RotationMatrix(ref worldMatrix_screen_cube_corners_instances[indexer][i], out rigidRot);
                                                        //JQuaternion quattor = new JQuaternion(rigidRot.X, rigidRot.Y, rigidRot.Z, rigidRot.W);
                                                        //JMatrix ridigMatrix = JMatrix.CreateFromQuaternion(quattor);
                                                        //_world_screen_corner_cube_list[x + width * (y + height * z)]._singleObjectOnly.transform.Component.rigidbody.Orientation = ridigMatrix;
                                                        _screen_asset_counter++;*/
                                                        /*if (_screen_asset_counter == 0)
                                                        {
                                                            Quaternion _quaternion_locked_body_matrix;
                                                            Quaternion.RotationMatrix(ref worldMatrix_instances_screen_assets[_index][_screen_asset_counter], out _quaternion_locked_body_matrix);
                                                            //body.Orientation = JMatrix.CreateFromQuaternion(new JQuaternion(_quaternion_locked_body_matrix.X, _quaternion_locked_body_matrix.Y, _quaternion_locked_body_matrix.Z, _quaternion_locked_body_matrix.W));
                                                            //body.Position = new JVector(worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M41, worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M42, worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M43);
                                                        }
                                                        else if (_screen_asset_counter == 1)
                                                        {
                                                            Quaternion _quaternion_locked_body_matrix;
                                                            Quaternion.RotationMatrix(ref worldMatrix_instances_screen_assets[_index][_screen_asset_counter], out _quaternion_locked_body_matrix);
                                                            //body.Orientation = JMatrix.CreateFromQuaternion(new JQuaternion(_quaternion_locked_body_matrix.X, _quaternion_locked_body_matrix.Y, _quaternion_locked_body_matrix.Z, _quaternion_locked_body_matrix.W));
                                                            //body.Position = new JVector(worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M41, worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M42, worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M43);
                                                        }
                                                        else if (_screen_asset_counter == 2)
                                                        {
                                                            Quaternion _quaternion_locked_body_matrix;
                                                            Quaternion.RotationMatrix(ref worldMatrix_instances_screen_assets[_index][_screen_asset_counter], out _quaternion_locked_body_matrix);
                                                            //body.Orientation = JMatrix.CreateFromQuaternion(new JQuaternion(_quaternion_locked_body_matrix.X, _quaternion_locked_body_matrix.Y, _quaternion_locked_body_matrix.Z, _quaternion_locked_body_matrix.W));
                                                            //body.Position = new JVector(worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M41, worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M42, worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M43);
                                                        }
                                                        else if (_screen_asset_counter == 3)
                                                        {
                                                            Quaternion _quaternion_locked_body_matrix;
                                                            Quaternion.RotationMatrix(ref worldMatrix_instances_screen_assets[_index][_screen_asset_counter], out _quaternion_locked_body_matrix);
                                                            //body.Orientation = JMatrix.CreateFromQuaternion(new JQuaternion(_quaternion_locked_body_matrix.X, _quaternion_locked_body_matrix.Y, _quaternion_locked_body_matrix.Z, _quaternion_locked_body_matrix.W));
                                                            //body.Position = new JVector(worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M41, worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M42, worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M43);
                                                        }
                                                        else if (_screen_asset_counter == 4)
                                                        {
                                                            Quaternion _quaternion_locked_body_matrix;
                                                            Quaternion.RotationMatrix(ref worldMatrix_instances_screen_assets[_index][_screen_asset_counter], out _quaternion_locked_body_matrix);
                                                            //body.Orientation = JMatrix.CreateFromQuaternion(new JQuaternion(_quaternion_locked_body_matrix.X, _quaternion_locked_body_matrix.Y, _quaternion_locked_body_matrix.Z, _quaternion_locked_body_matrix.W));
                                                            //body.Position = new JVector(worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M41, worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M42, worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M43);
                                                        }
                                                        else if (_screen_asset_counter == 5)
                                                        {
                                                            Quaternion _quaternion_locked_body_matrix;
                                                            Quaternion.RotationMatrix(ref worldMatrix_instances_screen_assets[_index][_screen_asset_counter], out _quaternion_locked_body_matrix);
                                                            //body.Orientation = JMatrix.CreateFromQuaternion(new JQuaternion(_quaternion_locked_body_matrix.X, _quaternion_locked_body_matrix.Y, _quaternion_locked_body_matrix.Z, _quaternion_locked_body_matrix.W));
                                                            //body.Position = new JVector(worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M41, worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M42, worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M43);
                                                        }
                                                        else if (_screen_asset_counter == 6)
                                                        {
                                                            Quaternion _quaternion_locked_body_matrix;
                                                            Quaternion.RotationMatrix(ref worldMatrix_instances_screen_assets[_index][_screen_asset_counter], out _quaternion_locked_body_matrix);
                                                            //body.Orientation = JMatrix.CreateFromQuaternion(new JQuaternion(_quaternion_locked_body_matrix.X, _quaternion_locked_body_matrix.Y, _quaternion_locked_body_matrix.Z, _quaternion_locked_body_matrix.W));
                                                            //body.Position = new JVector(worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M41, worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M42, worldMatrix_instances_screen_assets[_index][_screen_asset_counter].M43);
                                                        }*/
                                                        _screen_asset_counter++;
                                                    }

                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag._spectrum)
                                                    {
                                                        /*Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                        quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                        tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                        Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                        Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);


                                                        if (_add_one_counter > 50)
                                                        {
                                                            //_add_one += _world_spectrum_list[_spectrum_count]._sizeZ * 10;

                                                            //_z_remove = worldMatrix_instances_spectrum[_index][_spectrum_count].M43 - worldMatrix_instances_spectrum[_index][_spectrum_count - 49].M43;

                                                            //_z_remove -=
                                                            _add_one_counter = 0;
                                                        }

                                                        //if (_spectrum_count % 10 == 1)
                                                        //{
                                                        //    //translationMatrix.M41 += 0.1f;
                                                        //    _add_one_swtch = 1;
                                                        //}

                                                        //if (_add_one_swtch == 1)
                                                        //{
                                                        //    _add_one += 0.1f;
                                                        //    _add_one_swtch = 0;
                                                        //}
                                                        Vector3 _last_spectrum_pos = new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                        Vector3 _final_rot_hand = new Vector3(final_hand_pos_left.M41, final_hand_pos_left.M42, final_hand_pos_left.M43);

                                                        //float _dist_spectrum_cube_to_hand;
                                                        //Vector3.Distance(ref _last_spectrum_pos,ref _final_rot_hand, out _dist_spectrum_cube_to_hand);

                                                        translationMatrix.M41 = _other_left_touch_matrix.M41 + translationMatrix.M41 + OFFSETPOS.X + _add_one;
                                                        translationMatrix.M42 = _other_left_touch_matrix.M42 + translationMatrix.M42 + OFFSETPOS.Y;
                                                        translationMatrix.M43 = _other_left_touch_matrix.M43 + translationMatrix.M43 + OFFSETPOS.Z; //- _z_remove

                                                        Quaternion _testQuater;
                                                        Quaternion.RotationMatrix(ref final_hand_pos_left, out _testQuater);
                                                        var screenNormalForward = _getDirection(Vector3.ForwardRH, _testQuater);
                                                        screenNormalForward.Normalize();
                                                        var screenNormalRight = _getDirection(Vector3.Right, _testQuater);
                                                        screenNormalRight.Normalize();
                                                        var screenNormalUp = _getDirection(Vector3.Up, _testQuater);
                                                        screenNormalUp.Normalize();

                                                        if (_temp_spectrum_dist_swtch == 0)
                                                        {
                                                            offsetX = translationMatrix.M41 - _final_rot_hand.X;
                                                            offsetY = translationMatrix.M42 - _final_rot_hand.Y;
                                                            offsetZ = translationMatrix.M43 - _final_rot_hand.Z;
                                                            _temp_spectrum_dist_swtch = 1;
                                                        }

                                                        _last_spectrum_pos = _final_rot_hand + (screenNormalForward * offsetZ);
                                                        _last_spectrum_pos = _last_spectrum_pos + (screenNormalRight * offsetX);
                                                        _last_spectrum_pos = _last_spectrum_pos + (screenNormalUp * offsetY);

                                                        if (_has_recorded == 1)
                                                        {
                                                            translationMatrix.M42 += _sound_byte_array[_spectrum_count] * 0.001f;
                                                        }
                                                        else
                                                        {

                                                        }

                                                        translationMatrix.M41 = _last_spectrum_pos.X;
                                                        translationMatrix.M42 = _last_spectrum_pos.Y;
                                                        translationMatrix.M43 = _last_spectrum_pos.Z;

                                                        //if (_spectrum_count == worldMatrix_instances_spectrum[_index].Length - 2||
                                                        //    _spectrum_count == worldMatrix_instances_spectrum[_index].Length - 1)
                                                        //{
                                                        //    translationMatrix.M42 = 0;
                                                        //}

                                                        worldMatrix_instances_spectrum[_index][_spectrum_count] = translationMatrix;
                                                        _spectrum_count++;
                                                        _add_one_counter++;


                                                        /*Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                        quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                        tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                        Matrix.RotationQuaternion(ref tester, out rotationMatrix);
                                                        Matrix.Multiply(ref rotationMatrix, ref translationMatrix, out translationMatrix);

                                                        var _left_touch_pos = new Vector3(_other_left_touch_matrix.M41, _other_left_touch_matrix.M42, _other_left_touch_matrix.M43);

                                                        Matrix _someMatrix = Matrix.Identity;

                                                        _someMatrix.M41 = _left_touch_pos.X + translationMatrix.M41 + OFFSETPOS.X;
                                                        //_someMatrix.M42 = _left_touch_pos.Y + translationMatrix.M42 + OFFSETPOS.Y;// + (_sound_byte_array[_spectrum_count] * 0.001f);
                                                        _someMatrix.M43 = _left_touch_pos.Z + translationMatrix.M43 + OFFSETPOS.Z;

                                                        Vector3 _current_pos = new Vector3(_someMatrix.M41, _someMatrix.M42, _someMatrix.M43);
                                                        //Vector3 _current_spectrum_pos = new Vector3(_someMatrix.M41, _someMatrix.M42 + (_sound_byte_array_lift[_spectrum_count] * 1), _someMatrix.M43);
                                                        //Vector3 testing = Lerp(_current_pos, _current_spectrum_pos, 10);


                                                        //object _sound_byte_array_object = (object)_main_received_object[0]._someData[0];
                                                        //byte[] _sound_byte_array = (byte[])_sound_byte_array_object;



                                                       // Lerp(_someMatrix.M42, _someMatrix.M42 + (_sound_byte_array[_spectrum_count] * 100000), 1);

                                                        _someMatrix.M41 = _left_touch_pos.X + translationMatrix.M41 + OFFSETPOS.X;
                                                        //_someMatrix.M42 = _left_touch_pos.Y + translationMatrix.M42 + OFFSETPOS.Y;
                                                        _someMatrix.M43 = _left_touch_pos.Z + translationMatrix.M43 + OFFSETPOS.Z;

                                                        if (_has_recorded == 1)
                                                        {
                                                            float spectrum_y = (_sound_byte_array[_spectrum_count] * 100000);
                                                            //MessageBox((IntPtr)0, "" + "has recorded", "Oculus Error", 0);
                                                            //_sound_byte_array_lift[_spectrum_count] = _sound_byte_array[_spectrum_count];
                                                            _someMatrix.M42 = spectrum_y;
                                                            //_spectrum_work = 1;
                                                            _has_recorded = 2;
                                                        }
                                                        else
                                                        {
                                                          
                                                            _someMatrix.M42 = _left_touch_pos.Y + translationMatrix.M42 + OFFSETPOS.Y;
                                                        }

                                   
                                                        worldMatrix_instances_spectrum[_index][_spectrum_count] = _someMatrix;
                                                        _spectrum_count++;
                                                        */



                                                        //_someMatrix.M42 = _left_touch_pos.Y + translationMatrix.M42 + OFFSETPOS.Y + (_sound_byte_array[_spectrum_count]*0.001f);


                                                        /*object _sound_byte_array_object = (object)_main_received_object[0]._someData[0];
                                                        byte[] _sound_byte_array = (byte[])_sound_byte_array_object;

                                                        MessageBox((IntPtr)0, "" + _sound_byte_array.Length, "Oculus Error", 0);
                                                        if (_spectrum_count < _sound_byte_array.Length - 1)
                                                        {
                                                            _someMatrix.M42 = _left_touch_pos.Y + translationMatrix.M42 + OFFSETPOS.Y + _sound_byte_array[_spectrum_count];
                                                        }
                                                        else
                                                        {
                                                            _someMatrix.M42 = _left_touch_pos.Y + translationMatrix.M42 + OFFSETPOS.Y;
                                                        }
                                                        _someMatrix.M42 = _left_touch_pos.Y + translationMatrix.M42 + OFFSETPOS.Y + _sound_byte_array[_spectrum_count];
                                                        */
                                                        /*
                                                        if (_sound_byte_array != null)
                                                        {
                                                            MessageBox((IntPtr)0, "" + _sound_byte_array.Length, "Oculus Error", 0);
                                                            if (_spectrum_count < _sound_byte_array.Length - 1)
                                                            {
                                                                _someMatrix.M42 = _left_touch_pos.Y + translationMatrix.M42 + OFFSETPOS.Y + _sound_byte_array[_spectrum_count] ;
                                                            }
                                                            else
                                                            {
                                                                _someMatrix.M42 = _left_touch_pos.Y + translationMatrix.M42 + OFFSETPOS.Y;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            _someMatrix.M42 = _left_touch_pos.Y + translationMatrix.M42 + OFFSETPOS.Y;
                                                        }*/

                                                        /*Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                        quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                        tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                        Matrix.RotationQuaternion(ref tester, out rotationMatrix);
                                                        Matrix.Multiply(ref rotationMatrix, ref translationMatrix, out translationMatrix);

                                                        Vector3 current_pos = new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);*/
                                                        /*var _someMatrix = _leftTouchMatrix * originRot * rotatingMatrix * rotationMatrix;

                                                        Quaternion otherQuat;
                                                        Quaternion.RotationMatrix(ref _someMatrix, out otherQuat);

                                                        var _left_touch_pos = new Vector3(_leftTouchMatrix.M41, _leftTouchMatrix.M42, _leftTouchMatrix.M43);
                                                        var _body_pos = new Vector3(body.Position.X, body.Position.Y, body.Position.Z);
                                                        //var _body_pos = new Vector3(worldMatrix_instances_spectrum[_index][_spectrum_count].M41, worldMatrix_instances_spectrum[_index][_spectrum_count].M42, worldMatrix_instances_spectrum[_index][_spectrum_count].M43);



                                                        var diffX = _body_pos.X - _left_touch_pos.X;
                                                        var diffY = _body_pos.Y - _left_touch_pos.Y;
                                                        var diffZ = _body_pos.Z - _left_touch_pos.Z;

                                                        var direction_feet_forward_hand = _getDirection(Vector3.ForwardRH, otherQuat);
                                                        var direction_feet_right_hand = _getDirection(Vector3.Right, otherQuat);
                                                        var direction_feet_up_hand = _getDirection(Vector3.Up, otherQuat);

                                                        Vector3 current_pos = new Vector3(_other_left_touch_matrix.M41, _other_left_touch_matrix.M42, _other_left_touch_matrix.M43) + (direction_feet_right_hand * diffX);
                                                        current_pos = current_pos + (direction_feet_up_hand * diffY);
                                                        current_pos = current_pos + (direction_feet_forward_hand * diffZ);
                                                        */
                                                        /*Matrix _someMatrix = _other_left_touch_matrix;
                                                        _someMatrix.M41 += current_pos.X + OFFSETPOS.X;
                                                        _someMatrix.M42 += current_pos.Y + OFFSETPOS.Y;
                                                        _someMatrix.M43 += current_pos.Z + OFFSETPOS.Z;

                                                        worldMatrix_instances_spectrum[_index][_spectrum_count] = _someMatrix;*/

                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.Terrain)
                                                    {
                                                        Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                        quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                        tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                        Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                        Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                        worldMatrix_instances_terrain[0][0] = translationMatrix;
                                                        _terrain_count++;
                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag._terrain_tiles)
                                                    {
                                                        /*Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                        quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                        tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                        Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                        Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                        worldMatrix_instances_terrain_tiles[_index][_terrain_tiles_count] = translationMatrix;
                                                        _terrain_tiles_count++;*/
                                                    }

                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.physicsInstancedCube)
                                                    {
                                                        if (!body.IsStatic)
                                                        {
                                                            if (handTriggerLeft[0] >= 0.001f)
                                                            {
                                                                Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);

                                                                var dirToRightHand = new Vector3(_player_lft_hnd[_index]._arrayOfInstances[0].current_pos.M41, _player_lft_hnd[_index]._arrayOfInstances[0].current_pos.M42, _player_lft_hnd[_index]._arrayOfInstances[0].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                //var dirToRightHand = new Vector3(_player_lft_hnd[_index]._arrayOfInstances[_cube_counter].current_pos.M41, _player_lft_hnd[_index]._arrayOfInstances[_cube_counter].current_pos.M42, _player_lft_hnd[_index]._arrayOfInstances[_cube_counter].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                var lengthofdir = dirToRightHand.Length();
                                                                dirToRightHand.Normalize();

                                                                var _force = handTriggerLeft[0] * 0.01f;
                                                                var force = new JVector(dirToRightHand.X, dirToRightHand.Y, dirToRightHand.Z) * _force;

                                                                body.LinearVelocity += force;
                                                                body.AddForce(force);
                                                                _has_used_trigger = 1;
                                                            }
                                                            Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                            quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                            tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                            Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                            Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                            worldMatrix_instances_cubes[_index][_cube_counter] = translationMatrix;
                                                        }
                                                        _cube_counter++;
                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerHandRight)
                                                    {
                                                        Quaternion temp_quat;
                                                        Matrix temp_mat = worldMatrix_instances_r_hand[_index][p_r_hnd_count];
                                                        Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                        JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                        JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                        body.Orientation = jmat;
                                                        body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                        p_r_hnd_count++;
                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerHandLeft)
                                                    {
                                                        Quaternion temp_quat;
                                                        Matrix temp_mat = worldMatrix_instances_l_hand[_index][p_l_hnd_count];
                                                        Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                        JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                        JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                        body.Orientation = jmat;
                                                        body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                        p_l_hnd_count++;
                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerTorso)
                                                    {
                                                        Quaternion temp_quat;
                                                        Matrix temp_mat = worldMatrix_instances_torso[_index][p_torso_count];
                                                        Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                        JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                        JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                        body.Orientation = jmat;
                                                        body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                        p_torso_count++;
                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerPelvis)
                                                    {
                                                        Quaternion temp_quat;
                                                        Matrix temp_mat = worldMatrix_instances_pelvis[_index][p_pelvis_count];
                                                        Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                        JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                        JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                        body.Orientation = jmat;
                                                        body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                        p_pelvis_count++;
                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerShoulderRight)
                                                    {
                                                        Quaternion temp_quat;
                                                        Matrix temp_mat = worldMatrix_instances_r_shoulder[_index][p_r_shldr_count];
                                                        Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                        JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                        JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                        body.Orientation = jmat;
                                                        body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                        p_r_shldr_count++;
                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerShoulderLeft)
                                                    {
                                                        Quaternion temp_quat;
                                                        Matrix temp_mat = worldMatrix_instances_l_shoulder[_index][p_l_shldr_count];
                                                        Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                        JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                        JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                        body.Orientation = jmat;
                                                        body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                        p_l_shldr_count++;
                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerHead)
                                                    {

                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerLowerArmRight)
                                                    {
                                                        Quaternion temp_quat;
                                                        Matrix temp_mat = worldMatrix_instances_r_lowerarm[_index][p_r_lowerA_count];
                                                        Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                        JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                        JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                        body.Orientation = jmat;
                                                        body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                        p_r_lowerA_count++;
                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerLowerArmLeft)
                                                    {
                                                        Quaternion temp_quat;
                                                        Matrix temp_mat = worldMatrix_instances_l_lowerarm[_index][p_l_lowerA_count];
                                                        Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                        JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                        JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                        body.Orientation = jmat;
                                                        body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                        p_l_lowerA_count++;
                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerUpperArmRight)
                                                    {
                                                        Quaternion temp_quat;
                                                        Matrix temp_mat = worldMatrix_instances_r_upperarm[_index][p_r_upperA_count];
                                                        Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                        JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                        JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                        body.Orientation = jmat;
                                                        body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                        p_r_upperA_count++;
                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerUpperArmLeft)
                                                    {
                                                        Quaternion temp_quat;
                                                        Matrix temp_mat = worldMatrix_instances_l_upperarm[_index][p_l_upperA_count];
                                                        Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                        JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                        JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                        body.Orientation = jmat;
                                                        body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                        p_l_upperA_count++;

                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerLeftElbowTarget)
                                                    {
                                                        Quaternion temp_quat;
                                                        Matrix temp_mat = worldMatrix_instances_l_elbow_target[_index][p_l_target_count];
                                                        Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                        JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                        JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                        body.Orientation = jmat;
                                                        body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                        p_l_target_count++;
                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerRightElbowTarget)
                                                    {
                                                        Quaternion temp_quat;
                                                        Matrix temp_mat = worldMatrix_instances_r_elbow_target[_index][p_r_target_count];
                                                        Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                        JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                        JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                        body.Orientation = jmat;
                                                        body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                        p_r_target_count++;
                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerLeftElbowTargettwo)
                                                    {
                                                        Quaternion temp_quat;
                                                        Matrix temp_mat = worldMatrix_instances_l_elbow_target_two[_index][p_l_target_two_count];
                                                        Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                        JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                        JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                        body.Orientation = jmat;
                                                        body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                        p_l_target_two_count++;
                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerRightElbowTargettwo)
                                                    {
                                                        Quaternion temp_quat;
                                                        Matrix temp_mat = _player_rght_elbow_target_two[_index]._arrayOfInstances[p_r_target_two_count].current_pos;// worldMatrix_instances_r_elbow_target_two[_index][p_r_target_two_count];
                                                        Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                        JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                        JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                        body.Orientation = jmat;
                                                        body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                        p_r_target_two_count++;

                                                    }
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.sc_perko_voxel)
                                                    {

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }












                        /*
                        if (_spectrum_work == 1)
                        {
                            for (int x = 0; x < _physics_engine_instance_x; x++)
                            {
                                for (int y = 0; y < _physics_engine_instance_y; y++)
                                {
                                    for (int z = 0; z < _physics_engine_instance_z; z++)
                                    {
                                        var _index = x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z);

                                        int _spectrum_count = 0;

                                        if (_world_list[_index] != null)
                                        {
                                            if (_world_list[_index].RigidBodies.Count > 0)
                                            {
                                                enumerator = _world_list[_index].RigidBodies.GetEnumerator();

                                                while (enumerator.MoveNext())
                                                {
                                                    body = (RigidBody)enumerator.Current;

                                                    if (body != null && body.Tag != null)
                                                    {
                                                        if ((_SC_console_directx.BodyTag)body.Tag == _SC_console_directx.BodyTag._spectrum)
                                                        {
                                                            
                                                            //Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                            //quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                            //tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                            //Matrix.RotationQuaternion(ref tester, out rotationMatrix);
                                                            //Matrix.Multiply(ref rotationMatrix, ref translationMatrix, out translationMatrix);
                                                            

                                                            //float sound_byte = _sound_byte_array_lift[_spectrum_count];
                                                            //if (sound_byte > 0)
                                                            //{
                                                            //    MessageBox((IntPtr)0, sound_byte + "", "Oculus error", 0);
                                                            //}











                                                            /*var _left_touch_pos = new Vector3(_other_left_touch_matrix.M41, _other_left_touch_matrix.M42, _other_left_touch_matrix.M43);
                                                            Matrix _someMatrix = Matrix.Identity;
                                                            _someMatrix.M41 = _left_touch_pos.X + translationMatrix.M41 + OFFSETPOS.X;
                                                            _someMatrix.M42 = _left_touch_pos.Y + translationMatrix.M42 + OFFSETPOS.Y;// + (_sound_byte_array[_spectrum_count] * 0.001f);
                                                            _someMatrix.M43 = _left_touch_pos.Z + translationMatrix.M43 + OFFSETPOS.Z;
                                                            Vector3 _current_pos = new Vector3(_someMatrix.M41, _someMatrix.M42, _someMatrix.M43);
                                                            Vector3 _current_spectrum_pos = new Vector3(_someMatrix.M41, _someMatrix.M42 + (_sound_byte_array[_spectrum_count] * 0.001f), _someMatrix.M43);
                                                            float spectrum_y = Lerp(_someMatrix.M42, _someMatrix.M42 + (_sound_byte_array[_spectrum_count] * 0.001f), 1);
                                                            var _posX = _left_touch_pos.X + translationMatrix.M41 + OFFSETPOS.X;
                                                            var _posY = spectrum_y;// _left_touch_pos.Y + translationMatrix.M42 + OFFSETPOS.Y;// + (_sound_byte_array[_spectrum_count] * 0.001f);
                                                            var _posZ = _left_touch_pos.Z + translationMatrix.M43 + OFFSETPOS.Z;
                                                            */

                        //Vector3 _current_pos = new Vector3(body.Position.X, body.Position.Y, body.Position.Z);

                        //Vector3 _current_pos = new Vector3(worldMatrix_instances_spectrum[_index][_spectrum_count].M41, worldMatrix_instances_spectrum[_index][_spectrum_count].M42, worldMatrix_instances_spectrum[_index][_spectrum_count].M43);
                        //Matrix _someMatrix = Matrix.Identity;




                        /*//int value = BitConverter.ToInt16(_sound_byte_array_lift, _spectrum_count);
                        _someMatrix.M41 = _current_pos.X;
                        //_someMatrix.M42 = _current_pos.Y;
                        //_someMatrix.M42 = Lerp(_someMatrix.M42 + (_sound_byte_array_lift[_spectrum_count] * 100), _someMatrix.M42, 1);
                        //_someMatrix.M42 = _current_pos.Y + (_sound_byte_array_lift[_spectrum_count] * 0.01f);
                        _someMatrix.M43 = _current_pos.Z;
                        //Vector3 sc_goal_spectrum_pos = new Vector3();


                        if (_someMatrix.M42 > 0)
                        {
                            _someMatrix.M42 -= 0.001f;
                        }*/



                        //_someMatrix.M42 = value;// _sound_byte_array_lift[_spectrum_count];
                        //_sound_byte_array_lift[_spectrum_count] -= 1;


                        /*if (_sound_byte_array_lift[_spectrum_count] > 0)
                        {
                            MessageBox((IntPtr)0, _sound_byte_array_lift[_spectrum_count] + "", "Oculus error", 0);
                            //_someMatrix.M42--;// Lerp(_someMatrix.M42 + (_sound_byte_array_lift[_spectrum_count] * 100), _someMatrix.M42, 1);
                        }
                        else
                        {

                        }*/
                        /*
                        Vector3 _current_pos = new Vector3(body.Position.X, body.Position.Y, body.Position.Z);
                        Matrix _someMatrix = Matrix.Identity;

                        _someMatrix.M41 = _current_pos.X;
                        //_someMatrix.M42 = _current_pos.Y;
                        //_someMatrix.M42 = Lerp(_someMatrix.M42 + (_sound_byte_array_lift[_spectrum_count] * 100), _someMatrix.M42, 1);
                        //_someMatrix.M42 = _current_pos.Y + (_sound_byte_array_lift[_spectrum_count] * 0.01f);
                        _someMatrix.M43 = _current_pos.Z;


                        if (_sound_byte_array_lift[_spectrum_count] > 0)
                        {
                            //float spectrum_y = Lerp(_someMatrix.M42, _someMatrix.M42 + (_sound_byte_array_lift[_spectrum_count] * 0.001f), 1);
                            var test = (_sound_byte_array_lift[_spectrum_count] * 100);// - 0.01f;
                            _sound_byte_array_lift[_spectrum_count] = (byte)test;
                            _someMatrix.M42 = _sound_byte_array_lift[_spectrum_count];
                        }
                        else
                        {
                            _someMatrix.M42 = _current_pos.Y;
                        }

                        //worldMatrix_instances_spectrum[_index][_spectrum_count] = _someMatrix;

                        //Vector3 _current_pos = new Vector3(body.Position.X, body.Position.Y, body.Position.Z);
                        //Vector3 sc_goal_spectrum_pos = _current_spectrum_pos;

                        //Vector3 testing = Lerp(_current_pos, _current_spectrum_pos, 10);
                        //float spectrum_y = Lerp(_someMatrix.M42, _someMatrix.M42 + (_sound_byte_array[_spectrum_count] * 0.001f), 1);

                        //_spectrum_work = 2;
                        _spectrum_count++;
                    }
                }
            }
        }
    }
}
}
}
}




if (_spectrum_work == 2)
{
if (_spectrum_work_counter > 100)
{
_spectrum_work = 1;
_spectrum_work_counter = 0;
}
_spectrum_work_counter++;
}*/




                        /*
                        for (int x = 0; x < _physics_engine_instance_x; x++)
                        {
                            for (int y = 0; y < _physics_engine_instance_y; y++)
                            {
                                for (int z = 0; z < _physics_engine_instance_z; z++)
                                {
                                    var _index = x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z);

                                    for (int s = 0; s < _world_spectrum_list.Length; s++)
                                    {
                                        /*Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);

                                        quatterer = JQuaternion.CreateFromMatrix(body.Orientation);

                                        tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

                                        Matrix.RotationQuaternion(ref tester, out rotationMatrix);

                                        Matrix.Multiply(ref rotationMatrix, ref translationMatrix, out translationMatrix);

                                        var _someMatrix = _leftTouchMatrix * originRot * rotatingMatrix * rotationMatrix;
                                        _someMatrix.M41 = _leftTouchMatrix.M41 + _world_spectrum_list[s]._POSITION.M41;
                                        _someMatrix.M42 = _leftTouchMatrix.M42 + _world_spectrum_list[s]._POSITION.M42;
                                        _someMatrix.M43 = _leftTouchMatrix.M43 + _world_spectrum_list[s]._POSITION.M43;
                                        worldMatrix_instances_spectrum[_index][s] = _someMatrix;
                                    }
                                }
                            }
                        }*/
                        Thread.Sleep(1);
                        goto _thread_looper;
                    };

                    threaders.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs argers)
                    {

                    };

                    threaders.RunWorkerAsync();
                    //END OF













                    //OPENING ANOTHER BACKGROUND WORKER IN ORDER TO FILL IN THE BUFFERS OF THE WORLDPOSITON OF EACH INSTANCES, THE FORWARD/RIGHT/UP DIRECTION OF THAT INSTANCED OBJECT 
                    threaders = new BackgroundWorker();
                    threaders.DoWork += (object sender, DoWorkEventArgs argers) =>
                    {
                    //object[] parametors = args.Argument as object[];
                    //var workor = (_task_worker)parametors[0];
                    //var parameters[] = _array_stop_watch_tick[_index];
                    //int _indexer = workor._worker_task_id;

                    _thread_looper:

                        

                        Thread.Sleep(1);
                        goto _thread_looper;
                    };

                    threaders.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs argers)
                    {

                    };

                    threaders.RunWorkerAsync();
                    //END OF 

                    _start_background_worker_01 = 1;
                }
                //END OF 





                _DLightBuffer[0] = new _sc_core_systems.SC_Graphics.SC_cube.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                    padding1 = 100
                };

                _DLightBuffer_voxel_cube[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                    padding1 = 100
                };


                _DLightBuffer_spectrum[0] = new _sc_core_systems.SC_Graphics._sc_spectrum.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                    padding1 = 100
                };

                _SC_modL_torso_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                    padding1 = 100
                };

                _SC_modL_rght_hnd_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                    padding1 = 100
                };

                _SC_modL_rght_hnd_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                    padding1 = 100
                };

                _SC_modL_lft_hnd_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                    padding1 = 100
                };
                _SC_modL_lft_hnd_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                    padding1 = 100
                };

                _SC_modL_rght_shldr_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                    padding1 = 100
                }; _SC_modL_lft_shldr_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                    padding1 = 100
                }; _SC_modL_rght_elbow_target_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                    padding1 = 100
                };
                _SC_modL_rght_elbow_target_two_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                    padding1 = 100
                };
                _SC_modL_rght_upper_arm_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                    padding1 = 100
                }; _SC_modL_rght_lower_arm_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                    padding1 = 100
                };
                _SC_modL_lft_elbow_target_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                    padding1 = 100
                };
                _SC_modL_lft_elbow_target_two_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                    padding1 = 100
                };
                _SC_modL_lft_upper_arm_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                    padding1 = 100
                };
                _SC_modL_lft_lower_arm_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                    padding1 = 100
                };
                _SC_modL_pelvis_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                    padding1 = 100
                };



                worldMatrix_base[0] = WorldMatrix;
                Vector3f[] hmdToEyeViewOffsets = { D3D.eyeTextures[0].HmdToEyeViewOffset, D3D.eyeTextures[1].HmdToEyeViewOffset };
                displayMidpoint = D3D.OVR.GetPredictedDisplayTime(D3D.sessionPtr, 0);
                trackingState = D3D.OVR.GetTrackingState(D3D.sessionPtr, displayMidpoint, true);
                eyePoses = new Posef[2];
                D3D.OVR.CalcEyePoses(trackingState.HeadPose.ThePose, hmdToEyeViewOffsets, ref eyePoses);

                for (int eyeIndex = 0; eyeIndex < 2; eyeIndex++)
                {
                    eye = (EyeType)eyeIndex;
                    eyeTexture = D3D.eyeTextures[eyeIndex];

                    if (eyeIndex == 0)
                    {
                        D3D.layerEyeFov.RenderPoseLeft = eyePoses[0];
                    }
                    else
                    {
                        D3D.layerEyeFov.RenderPoseRight = eyePoses[1];
                    }

                    eyeTexture.RenderDescription = D3D.OVR.GetRenderDesc(D3D.sessionPtr, eye, D3D.hmdDesc.DefaultEyeFov[eyeIndex]);

                    D3D.result = eyeTexture.SwapTextureSet.GetCurrentIndex(out textureIndex);
                    D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to retrieve texture swap chain current index.");

                    D3D.device.ImmediateContext.OutputMerger.SetRenderTargets(eyeTexture.DepthStencilView, eyeTexture.RenderTargetViews[textureIndex]);
                    D3D.device.ImmediateContext.ClearRenderTargetView(eyeTexture.RenderTargetViews[textureIndex], SharpDX.Color.DimGray);
                    D3D.device.ImmediateContext.ClearDepthStencilView(eyeTexture.DepthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);
                    D3D.device.ImmediateContext.Rasterizer.SetViewport(eyeTexture.Viewport);

                    eyeQuaternionMatrix = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X, eyePoses[eyeIndex].Orientation.Y, eyePoses[eyeIndex].Orientation.Z, eyePoses[eyeIndex].Orientation.W));

                    eyePos = SharpDX.Vector3.Transform(new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z), originRot * rotatingMatrix * rotatingMatrixForPelvis).ToVector3();

                    //finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix;
                    finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix * rotatingMatrixForPelvis;

                    lookUp = Vector3.Transform(new Vector3(0, 1, 0), finalRotationMatrix).ToVector3();
                    lookAt = Vector3.Transform(new Vector3(0, 0, -1), finalRotationMatrix).ToVector3();

                    viewPosition = eyePos + OFFSETPOS;
                    viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);

                    _projectionMatrix = D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.01f, 100.0f, ProjectionModifier.None).ToMatrix();
                    _projectionMatrix.Transpose();

                    _WorldMatrix = D3D.WorldMatrix;






                    //TERRAIN SINGLEOBJECT
                    _terrain.Render(D3D.device.ImmediateContext);
                    _shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _terrain.IndexCount, _terrain.InstanceCount, _terrain._POSITION, viewMatrix, _projectionMatrix, _basicTexture.TextureResource, _DLightBuffer, oculusRiftDir, _terrain);
                    //END OF
                    
                    //PHYSICS SCREENS
                    _world_screen_list[0].Render(D3D.device.ImmediateContext);
                    _shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _world_screen_list[0].IndexCount, _world_screen_list[0].InstanceCount, _world_screen_list[0]._POSITION, viewMatrix, _projectionMatrix, _desktopFrame._ShaderResource, _DLightBuffer, oculusRiftDir, _world_screen_list[0]);
                    //END OF 
                    
                    //PHYSICS SCREEN ASSETS
                    _world_screen_assets_list[0].Render(D3D.device.ImmediateContext);
                    _shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _world_screen_assets_list[0].IndexCount, _world_screen_assets_list[0].InstanceCount, _world_screen_assets_list[0]._POSITION, viewMatrix, _projectionMatrix, null, _DLightBuffer, oculusRiftDir, _world_screen_assets_list[0]);
                    //END OF








                    int _swtch = 0;





                    //INSTANCED OBJECTS ONLY
                    for (int x = 0; x < _physics_engine_instance_x; x++)
                    {
                        for (int y = 0; y < _physics_engine_instance_y; y++)
                        {
                            for (int z = 0; z < _physics_engine_instance_z; z++)
                            {
                                var _index = x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z);










                                
                                //PHYSICS CUBES
                                _current_indexed_cube = _world_cube_list[_index];
                                _current_indexed_cube.Render(D3D.device.ImmediateContext);
                                _shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _current_indexed_cube.IndexCount, _current_indexed_cube.InstanceCount, _current_indexed_cube._POSITION, viewMatrix, _projectionMatrix, _desktopFrame._ShaderResource, _DLightBuffer, oculusRiftDir, _current_indexed_cube);
                                








                                try
                                {
                                    //PHYSICS VOXEL CUBES 
                                    //////////////////////
                                    //////////////////////about 100 ticks more per loop compared to simple physics cubes? will investigate later as when i do 
                                    //////////////////////simple cubes with the chunk it lags more even though the number of vertices are the same as the physics cube up above
                                    //////////////////////todo: culling of faces by distance from player. etc.
                                    //_SystemTickPerformance.Restart();
                                    var _cuber_pelvis1 = _world_voxel_cube_lists[_index];
                                    _cuber_pelvis1.Render(D3D.device.ImmediateContext);
                                    _shaderManager.RenderInstancedObject_voxel_spheroid(D3D.device.ImmediateContext, _cuber_pelvis1.IndexCount, _cuber_pelvis1.InstanceCount, _cuber_pelvis1._POSITION, viewMatrix, _projectionMatrix, _desktopFrame._ShaderResource, _DLightBuffer_voxel_cube, oculusRiftDir, _cuber_pelvis1);
                                    ///Console.WriteLine(_SystemTickPerformance.ElapsedTicks);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox((IntPtr)0, ex.ToString() + "", "Oculus error", 0);
                                }

                                /*try
                                {
                                    //PHYSICS SPECTRUM
                                    var _cuber_02 = _world_spectrum_list[indexer];
                                    _cuber_02.Render(D3D.device.ImmediateContext);
                                    _shaderManager.RenderInstancedObjectSpectrum(D3D.device.ImmediateContext, _cuber_02.IndexCount, _cuber_02.InstanceCount, _cuber_02._POSITION, viewMatrix, _projectionMatrix, null, _DLightBuffer_spectrum, oculusRiftDir, _cuber_02);

                                }
                                catch (Exception ex)
                                {
                                    MessageBox((IntPtr)0, ex.ToString() + "", "Oculus error", 0);
                                }

                                //COLLIDABLE PHYSICS TERRAIN TILES
                                _current_indexed_cube = _world_terrain_tile_list[indexer];
                                _world_terrain_tile_list[indexer].Render(D3D.device.ImmediateContext);
                                _shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _current_indexed_cube.IndexCount, _current_indexed_cube.InstanceCount, _current_indexed_cube._POSITION, viewMatrix, _projectionMatrix, null, _DLightBuffer, oculusRiftDir, _current_indexed_cube);
                                */




                                
                                ////////////////////
                                /////HUMAN RIG////// 
                                ////////////////////
                                for (int _iterator = 0; _iterator < _player_rght_hnd[_index]._arrayOfInstances.Length; _iterator++)
                                {
                                    finalRotationMatrix = originRot * rotatingMatrix * rotatingMatrixForPelvis;
                                    //THE CURRENT PIVOT POINT OF THE TORSO IS RIGHT IN THE MIDDLE OF THE TORSO ITSELF
                                    Vector3 MOVINGPOINTER = new Vector3(_player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                                    //SAVING IN MEMORY THE ORIGINAL TORSO MATRIX NOT AFFECTED BY CURRENT POSITION AND ROTATION CHANGES.
                                    Matrix _rotMatrixer = _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION;// _player_torso[_index]._ORIGINPOSITION;
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

                                    //REMOVED THAT TOO... WTF AND WHY AM I REMOVING THE TOTAL HEIGHT OF THE TORSO INSTEAD OF JUST HALF IS.ive got no clue. removing it.
                                    //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori_torso * (_player_torso._total_torso_height * 0.5f));

                                    //GETTING THE CURRENT ROTATION MATRIX OF THE PIVOT BOTTOM OF SPINE AREA.
                                    _rotatingMatrix = finalRotationMatrix;
                                    Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                                    //CONVERTING THE QUATERNION OF THAT TO THE DIRECTION OF ITS ROTATION
                                    var direction_feet_forward_torso = _getDirection(Vector3.ForwardRH, otherQuat);
                                    var direction_feet_right_torso = _getDirection(Vector3.Right, otherQuat);
                                    var direction_feet_up_torso = _getDirection(Vector3.Up, otherQuat);

                                    //I AM CALCULATING THE DIFFERENCE IN THE MOVEMENT FROM THE ORIGINAL POSITION TO THE CURRENT OFFSET AT THE BOTTOM OF THE SPINE WHERE I MOVED THAT POINT.
                                    diffNormPosX = (MOVINGPOINTER.X) - _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41; //_player_torso[_index]._ORIGINPOSITION.M41;
                                    diffNormPosY = (MOVINGPOINTER.Y) - _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42; //_player_torso[_index]._ORIGINPOSITION.M42;
                                    diffNormPosZ = (MOVINGPOINTER.Z) - _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43; //_player_torso[_index]._ORIGINPOSITION.M43;

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
                                    _rotatingMatrix = finalRotationMatrix;
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


                                    Matrix _body_pos = matrixerer;
                                    Quaternion _quat;
                                    Quaternion.RotationMatrix(ref _body_pos, out _quat);

                                    JQuaternion _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                    matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                                    worldMatrix_instances_torso[_index][_iterator] = matrixerer;// _player_pelvis[_index].current_pos;// translationMatrix;

                                    //_player_torso[_index]._arrayOfInstances[_iterator].current_pos = matrixerer;

                                    ///////////
                                    //SOMETESTS
                                    Quaternion.RotationMatrix(ref finalRotationMatrix, out otherQuat);
                                    direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                    direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                    direction_feet_up = _getDirection(Vector3.Up, otherQuat);

                                    Vector3 current_rotation_of_torso_pivot_forward = direction_feet_forward;
                                    Vector3 current_rotation_of_torso_pivot_right = direction_feet_right;
                                    Vector3 current_rotation_of_torso_pivot_up = direction_feet_up;
                                    //SOMETESTS
                                    ///////////


                                    
                                    MOVINGPOINTER = new Vector3(_player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                                    Matrix someMatRight = _rightTouchMatrix;
                                    someMatRight.M41 = handPoseRight.Position.X + MOVINGPOINTER.X;
                                    someMatRight.M42 = handPoseRight.Position.Y;// + MOVINGPOINTER.Y;
                                    someMatRight.M43 = handPoseRight.Position.Z + MOVINGPOINTER.Z;

                                    ///////////
                                    //HANDRIGHT

                                    _rotatingMatrix = finalRotationMatrix;
                                    //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                                    //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                    //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                    //direction_feet_up = _getDirection(Vector3.Up, otherQuat);

                                    diffNormPosX = (MOVINGPOINTER.X) - someMatRight.M41;
                                    diffNormPosY = (MOVINGPOINTER.Y) - someMatRight.M42;
                                    diffNormPosZ = (MOVINGPOINTER.Z) - someMatRight.M43;



                                    MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_right * (diffNormPosX));
                                    MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_up * (diffNormPosY));
                                    MOVINGPOINTER = MOVINGPOINTER + (current_rotation_of_torso_pivot_forward * (diffNormPosZ));

                                    MOVINGPOINTER.X += OFFSETPOS.X;// + _player_rght_hnd[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                    MOVINGPOINTER.Y += OFFSETPOS.Y;// + _player_rght_hnd[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                    MOVINGPOINTER.Z += OFFSETPOS.Z;//+ _player_rght_hnd[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

                                    matrixerer = Matrix.Identity;


                                    var currentPositionOfRightHand = MOVINGPOINTER;

                                    var currentDirShoulderRightToHandRight = currentPositionOfRightHand - new Vector3(_player_rght_upper_arm[_index]._arrayOfInstances[_iterator]._UPPERARMPIVOT.X, _player_rght_upper_arm[_index]._arrayOfInstances[_iterator]._UPPERARMPIVOT.Y, _player_rght_upper_arm[_index]._arrayOfInstances[_iterator]._UPPERARMPIVOT.Z);// new Vector3(_player_rght_shldr[_index].current_pos.M41, _player_rght_shldr[_index].current_pos.M42, _player_rght_shldr[_index].current_pos.M43);;// new Vector3(_player_rght_hnd[_index]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[_index]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[_index]._arrayOfInstances[_iterator].current_pos.M43);// new Vector3(_player_rght_shldr[_index].current_pos.M41, _player_rght_shldr[_index].current_pos.M42, _player_rght_shldr[_index].current_pos.M43);

                                    if (currentDirShoulderRightToHandRight.Length() > _player_rght_upper_arm[_index]._arrayOfInstances[_iterator]._ARMLENGTH)
                                    {
                                        currentDirShoulderRightToHandRight.Normalize();
                                        currentPositionOfRightHand = new Vector3(_player_rght_shldr[_index]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[_index]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[_index]._arrayOfInstances[_iterator].current_pos.M43) + (currentDirShoulderRightToHandRight * totalArmLengthRight);
                                    }
                                    someMatRight = someMatRight * finalRotationMatrix;

                                    someMatRight.M41 = MOVINGPOINTER.X;
                                    someMatRight.M42 = MOVINGPOINTER.Y;
                                    someMatRight.M43 = MOVINGPOINTER.Z;

                                    matrixerer.M11 = someMatRight.M11;
                                    matrixerer.M12 = someMatRight.M12;
                                    matrixerer.M13 = someMatRight.M13;
                                    matrixerer.M14 = someMatRight.M14;

                                    matrixerer.M21 = someMatRight.M21;
                                    matrixerer.M22 = someMatRight.M22;
                                    matrixerer.M23 = someMatRight.M23;
                                    matrixerer.M24 = someMatRight.M24;

                                    matrixerer.M31 = someMatRight.M31;
                                    matrixerer.M32 = someMatRight.M32;
                                    matrixerer.M33 = someMatRight.M33;
                                    matrixerer.M34 = someMatRight.M34;

                                    matrixerer.M41 = currentPositionOfRightHand.X;
                                    matrixerer.M42 = currentPositionOfRightHand.Y;
                                    matrixerer.M43 = currentPositionOfRightHand.Z;
                                    matrixerer.M44 = 1;

                                    _body_pos = matrixerer;
                                    //Quaternion _quat;
                                    Quaternion.RotationMatrix(ref _body_pos, out _quat);

                                     _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                    matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                                    //body.Position = new JVector(matrixerer.M41, matrixerer.M42, matrixerer.M43);
                                    //body.Orientation = matrixIn;
                                    worldMatrix_instances_r_hand[_index][_iterator] = matrixerer;// _player_pelvis[_index].current_pos;// translationMatrix;


                                    _player_rght_hnd[_index]._arrayOfInstances[_iterator].current_pos = matrixerer;



                                    //_leftTouchMatrix.M41 = handPoseLeft.Position.X + originPos.X + movePos.X;// + _hmdPoser.X;
                                    //_leftTouchMatrix.M42 = handPoseLeft.Position.Y + originPos.Y + movePos.Y;// + _hmdPoser.Y;
                                    //_leftTouchMatrix.M43 = handPoseLeft.Position.Z + originPos.Z + movePos.Z;// + _hmdPoser.Z;
                                    



                                    //HANDLEFT
                                    MOVINGPOINTER = new Vector3(_player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                                    Matrix someMatLeft = _leftTouchMatrix;
                                    someMatLeft.M41 = handPoseLeft.Position.X + MOVINGPOINTER.X;
                                    someMatLeft.M42 = handPoseLeft.Position.Y;// + MOVINGPOINTER.Y;
                                    someMatLeft.M43 = handPoseLeft.Position.Z + MOVINGPOINTER.Z;

                           
                                    _rotatingMatrix = finalRotationMatrix;
                                    //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                                    //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                    //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                    //direction_feet_up = _getDirection(Vector3.Up, otherQuat);

                                    diffNormPosX = (MOVINGPOINTER.X) - someMatLeft.M41;
                                    diffNormPosY = (MOVINGPOINTER.Y) - someMatLeft.M42;
                                    diffNormPosZ = (MOVINGPOINTER.Z) - someMatLeft.M43;



                                    ///
                                    MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_right * (diffNormPosX));
                                    MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_up * (diffNormPosY));
                                    MOVINGPOINTER = MOVINGPOINTER + (current_rotation_of_torso_pivot_forward * (diffNormPosZ));

                                    MOVINGPOINTER.X += OFFSETPOS.X;// + _player_lft_hnd[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                    MOVINGPOINTER.Y += OFFSETPOS.Y;// + _player_lft_hnd[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                    MOVINGPOINTER.Z += OFFSETPOS.Z;// + _player_lft_hnd[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

                                    matrixerer = Matrix.Identity;


                                    currentPositionOfRightHand = MOVINGPOINTER;

                                     currentDirShoulderRightToHandRight = currentPositionOfRightHand - new Vector3(_player_lft_upper_arm[_index]._arrayOfInstances[_iterator]._UPPERARMPIVOT.X, _player_lft_upper_arm[_index]._arrayOfInstances[_iterator]._UPPERARMPIVOT.Y, _player_lft_upper_arm[_index]._arrayOfInstances[_iterator]._UPPERARMPIVOT.Z);// new Vector3(_player_rght_shldr[_index].current_pos.M41, _player_rght_shldr[_index].current_pos.M42, _player_rght_shldr[_index].current_pos.M43);;// new Vector3(_player_rght_hnd[_index]._arrayOfInstances[_counter_hand_r].current_pos.M41, _player_rght_hnd[_index]._arrayOfInstances[_counter_hand_r].current_pos.M42, _player_rght_hnd[_index]._arrayOfInstances[_counter_hand_r].current_pos.M43);// new Vector3(_player_rght_shldr[_index].current_pos.M41, _player_rght_shldr[_index].current_pos.M42, _player_rght_shldr[_index].current_pos.M43);

                                    if (currentDirShoulderRightToHandRight.Length() > _player_lft_upper_arm[_index]._arrayOfInstances[_iterator]._ARMLENGTH)
                                    {
                                        currentDirShoulderRightToHandRight.Normalize();
                                        currentPositionOfRightHand = new Vector3(_player_lft_shldr[_index]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_shldr[_index]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_shldr[_index]._arrayOfInstances[_iterator].current_pos.M43) + (currentDirShoulderRightToHandRight * totalArmLengthRight);
                                    }

                                    someMatLeft = someMatLeft * finalRotationMatrix;

                                    someMatLeft.M41 = MOVINGPOINTER.X;
                                    someMatLeft.M42 = MOVINGPOINTER.Y;
                                    someMatLeft.M43 = MOVINGPOINTER.Z;

                                    matrixerer.M11 = someMatLeft.M11;
                                    matrixerer.M12 = someMatLeft.M12;
                                    matrixerer.M13 = someMatLeft.M13;
                                    matrixerer.M14 = someMatLeft.M14;

                                    matrixerer.M21 = someMatLeft.M21;
                                    matrixerer.M22 = someMatLeft.M22;
                                    matrixerer.M23 = someMatLeft.M23;
                                    matrixerer.M24 = someMatLeft.M24;

                                    matrixerer.M31 = someMatLeft.M31;
                                    matrixerer.M32 = someMatLeft.M32;
                                    matrixerer.M33 = someMatLeft.M33;
                                    matrixerer.M34 = someMatLeft.M34;

                                    matrixerer.M41 = currentPositionOfRightHand.X;
                                    matrixerer.M42 = currentPositionOfRightHand.Y;
                                    matrixerer.M43 = currentPositionOfRightHand.Z;
                                    matrixerer.M44 = 1;

                              
                                    _body_pos = matrixerer;
                                    //Quaternion _quat;
                                    Quaternion.RotationMatrix(ref _body_pos, out _quat);

                                    _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                    matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                                    //body.Position = new JVector(matrixerer.M41, matrixerer.M42, matrixerer.M43);
                                    //body.Orientation = matrixIn;
                                    worldMatrix_instances_l_hand[_index][_iterator] = matrixerer;// _player_pelvis[_index].current_pos;// translationMatrix;

                                    _player_lft_hnd[_index]._arrayOfInstances[_iterator].current_pos = matrixerer;







                                    ///////////
                                    //SHOULDER RIGHT
                                    MOVINGPOINTER = new Vector3(_player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                                    _rotatingMatrix = finalRotationMatrix;

                                    //MOVINGPOINTER = new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                                    _rotMatrixer = _player_rght_shldr[_index]._ORIGINPOSITION;

                                    Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);


                                    var direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                    var direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                    var direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                                    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_rght_shldr[_index]._total_torso_height * 0.5f));
                                    _rotatingMatrix = finalRotationMatrix;
                                    //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                                    //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                    //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                    //direction_feet_up = _getDirection(Vector3.Up, otherQuat);

                                    diffNormPosX = (MOVINGPOINTER.X) - _player_rght_shldr[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                    diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_shldr[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                    diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_shldr[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

                                    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                                    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * (diffNormPosY));
                                    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * (diffNormPosZ));

                                    //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * ((float)hyp));

                                    MOVINGPOINTER.X += OFFSETPOS.X;
                                    MOVINGPOINTER.Y += OFFSETPOS.Y;
                                    MOVINGPOINTER.Z += OFFSETPOS.Z;

                                    //matrixerer = Matrix.Identity;
                                    // _rotatingMatrix = finalRotationMatrix;
                                    matrixerer = Matrix.Identity;
                                    _rotatingMatrix = _player_rght_upper_arm[_index]._arrayOfInstances[_iterator]._SHOULDERROT;


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


                                    _body_pos = matrixerer;
                                    Quaternion.RotationMatrix(ref _body_pos, out _quat);

                                    _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                    matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                                    //body.Position = new JVector(matrixerer.M41, matrixerer.M42, matrixerer.M43);
                                    //body.Orientation = matrixIn;
                                    worldMatrix_instances_r_shoulder[_index][_iterator] = matrixerer;// _player_pelvis[_index].current_pos;// translationMatrix;

                                    _player_rght_shldr[_index]._arrayOfInstances[_iterator].current_pos = matrixerer;




                                    ///////////
                                    //SHOULDER RIGHT
                                    MOVINGPOINTER = new Vector3(_player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                                    _rotatingMatrix = finalRotationMatrix;

                                    //MOVINGPOINTER = new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                                    _rotMatrixer = _player_lft_shldr[_index]._ORIGINPOSITION;

                                    Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);


                                    direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                    direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                    direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                                    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_lft_shldr[_index]._total_torso_height * 0.5f));
                                    _rotatingMatrix = finalRotationMatrix;
                                    //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                                    //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                    //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                    //direction_feet_up = _getDirection(Vector3.Up, otherQuat);

                                    diffNormPosX = (MOVINGPOINTER.X) - _player_lft_shldr[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                    diffNormPosY = (MOVINGPOINTER.Y) - _player_lft_shldr[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                    diffNormPosZ = (MOVINGPOINTER.Z) - _player_lft_shldr[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

                                    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                                    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * (diffNormPosY));
                                    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * (diffNormPosZ));

                                    //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * ((float)hyp));

                                    MOVINGPOINTER.X += OFFSETPOS.X;
                                    MOVINGPOINTER.Y += OFFSETPOS.Y;
                                    MOVINGPOINTER.Z += OFFSETPOS.Z;

                                    //matrixerer = Matrix.Identity;
                                    // _rotatingMatrix = finalRotationMatrix;
                                    matrixerer = Matrix.Identity;
                                    _rotatingMatrix = _player_lft_upper_arm[_index]._arrayOfInstances[_iterator]._SHOULDERROT;



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


                                    _body_pos = matrixerer;
                                    Quaternion.RotationMatrix(ref _body_pos, out _quat);

                                    _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                    matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                                    //body.Position = new JVector(matrixerer.M41, matrixerer.M42, matrixerer.M43);
                                    //body.Orientation = matrixIn;
                                    worldMatrix_instances_l_shoulder[_index][_iterator] = matrixerer;// _player_pelvis[_index].current_pos;// translationMatrix;

                                  
                                    _player_lft_shldr[_index]._arrayOfInstances[_iterator].current_pos = matrixerer;












                                    //////////////////////
                                    //ELBOW TARGET RIGHT
                                    
                                    MOVINGPOINTER = new Vector3(_player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                                    TORSOPIVOT = MOVINGPOINTER;
                                    _rotMatrixer = _player_rght_elbow_target[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION;
                                    Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                    direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                    direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                    direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                                    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_rght_elbow_target[_index]._total_torso_height * 0.5f));

                                    _rotatingMatrix = finalRotationMatrix;
                                    //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                                    //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                    //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                    //direction_feet_up = _getDirection(Vector3.Up, otherQuat);


                                    diffNormPosX = (MOVINGPOINTER.X) - _player_rght_elbow_target[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                    diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_elbow_target[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                    diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_elbow_target[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

                                    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                                    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * (diffNormPosY));
                                    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * (diffNormPosZ));

                                    MOVINGPOINTER.X += OFFSETPOS.X;// + _player_rght_elbow_target._ORIGINPOSITION.M41;
                                    MOVINGPOINTER.Y += OFFSETPOS.Y;// + _player_rght_elbow_target._ORIGINPOSITION.M42;// + _player_rght_elbow_target._ORIGINPOSITION.M42;
                                    MOVINGPOINTER.Z += OFFSETPOS.Z;// + _player_rght_elbow_target._ORIGINPOSITION.M43;

                                    var someDiffX = MOVINGPOINTER.X - _player_rght_hnd[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                    var someDiffY = MOVINGPOINTER.Y - _player_rght_hnd[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                    var someDiffZ = MOVINGPOINTER.Z - _player_rght_hnd[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

                                    var somePosOfPivotUpperArm = new Vector3(_player_rght_shldr[_index]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[_index]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[_index]._arrayOfInstances[_iterator].current_pos.M43); //new Vector3(realPIVOTOfUpperArm.X, realPIVOTOfUpperArm.Y, realPIVOTOfUpperArm.Z); ;// new Vector3(_player_rght_shldr.current_pos.M41, _player_rght_shldr.current_pos.M42, _player_rght_shldr.current_pos.M43);
                                    var somePosOfRightHand = new Vector3(_player_rght_hnd[_index]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[_index]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[_index]._arrayOfInstances[_iterator].current_pos.M43);

                                    _player_rght_upper_arm[_index]._arrayOfInstances[_iterator]._UPPERARMPIVOT = somePosOfPivotUpperArm;

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

                                    someNewPointer = MOVINGPOINTER;

                                    var diffNormPosXElbowRight = (_player_rght_elbow_target[_index]._arrayOfInstances[_iterator].current_pos.M41) - (TORSOPIVOT.X);
                                    var diffNormPosYElbowRight = (_player_rght_elbow_target[_index]._arrayOfInstances[_iterator].current_pos.M42) - (TORSOPIVOT.Y);
                                    var diffNormPosZElbowRight = (_player_rght_elbow_target[_index]._arrayOfInstances[_iterator].current_pos.M43) - (TORSOPIVOT.Z);

                                    MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                                    MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                                    MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));

                                    //someNewPointer.X = someNewPointer.X + MOVINGPOINTER.X;
                                    //someNewPointer.Y = someNewPointer.Y + MOVINGPOINTER.Y;
                                    //someNewPointer.Z = someNewPointer.Z + MOVINGPOINTER.Z;

                                    matrixerer = Matrix.Identity;
                                    _rotatingMatrix = finalRotationMatrix;
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


                                    _body_pos = matrixerer;
                                    Quaternion.RotationMatrix(ref _body_pos, out _quat);

                                    _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                    matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                                    //body.Position = new JVector(matrixerer.M41, matrixerer.M42, matrixerer.M43);
                                    //body.Orientation = matrixIn;
                                    worldMatrix_instances_r_elbow_target[_index][_iterator] = matrixerer;// _player_pelvis[_index].current_pos;// translationMatrix;


                                    _player_rght_elbow_target[_index]._arrayOfInstances[_iterator].current_pos = matrixerer;

                                    //////////////////////////
                                    //ELBOW TARGET RIGHT TWO
                                  
                                    MOVINGPOINTER = new Vector3(_player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                                    TORSOPIVOT = MOVINGPOINTER;

                                    _rotMatrixer = _player_rght_elbow_target_two[_index]._arrayOfInstances[_iterator].current_pos;
                                    Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                    direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                    direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                    direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                                    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_rght_elbow_target_two[_index]._total_torso_height * 0.5f));
                                    _rotatingMatrix = finalRotationMatrix;
                                    //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                                    //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                    //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                    //direction_feet_up = _getDirection(Vector3.Up, otherQuat);


                                    diffNormPosX = (MOVINGPOINTER.X) - _player_rght_elbow_target_two[_index]._arrayOfInstances[_iterator].current_pos.M41;
                                    diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_elbow_target_two[_index]._arrayOfInstances[_iterator].current_pos.M42;
                                    diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_elbow_target_two[_index]._arrayOfInstances[_iterator].current_pos.M43;


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

                                    someDiffX = MOVINGPOINTER.X - _player_rght_hnd[_index]._arrayOfInstances[_iterator].current_pos.M41;
                                    someDiffY = MOVINGPOINTER.Y - _player_rght_hnd[_index]._arrayOfInstances[_iterator].current_pos.M42;
                                    someDiffZ = MOVINGPOINTER.Z - _player_rght_hnd[_index]._arrayOfInstances[_iterator].current_pos.M43;

                                    somePosOfRightHand = new Vector3(_player_rght_hnd[_index]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[_index]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[_index]._arrayOfInstances[_iterator].current_pos.M43);

                                    //dirShoulderToHand = somePosOfRightHand - new Vector3(_player_rght_upper_arm[_index].current_pos.M41, _player_rght_upper_arm[_index].current_pos.M42, _player_rght_upper_arm[_index].current_pos.M43);
                                    //------------------------------------------var dirShoulderToHand = somePosOfRightHand - new Vector3(_player_rght_shldr[_index].current_pos.M41, _player_rght_shldr[_index].current_pos.M42, _player_rght_shldr[_index].current_pos.M43);
                                    dirShoulderToHand = somePosOfRightHand - _player_rght_upper_arm[_index]._arrayOfInstances[_iterator]._UPPERARMPIVOT;


                                    MOVINGPOINTER = _player_rght_upper_arm[_index]._arrayOfInstances[_iterator]._UPPERARMPIVOT + (dirShoulderToHand * 2.5f);

                                    var someOffsetter = somePosOfRightHand - OFFSETPOS;
                                    var someOtherPivotPoint = MOVINGPOINTER;

                                    //MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * 1.0f);
                                    //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_forward_ori * 1.0f);

                                    someNewPointer = MOVINGPOINTER;

                                    diffNormPosXElbowRight = (_player_rght_elbow_target_two[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41) - (TORSOPIVOT.X);
                                    diffNormPosYElbowRight = (_player_rght_elbow_target_two[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42) - (TORSOPIVOT.Y);
                                    diffNormPosZElbowRight = (_player_rght_elbow_target_two[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43) - (TORSOPIVOT.Z);

                                    MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                                    MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                                    MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));

                                    matrixerer = Matrix.Identity;
                                    _rotatingMatrix = finalRotationMatrix;
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

                                    _body_pos = matrixerer;
                                    //Quaternion _quat;
                                    Quaternion.RotationMatrix(ref _body_pos, out _quat);

                                    _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                    matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                                    //body.Position = new JVector(matrixerer.M41, matrixerer.M42, matrixerer.M43);
                                    //body.Orientation = matrixIn;
                                    worldMatrix_instances_r_elbow_target_two[_index][_iterator] = matrixerer;// _player_pelvis[_index].current_pos;// translationMatrix;

                                    _player_rght_elbow_target_two[_index]._arrayOfInstances[_iterator].current_pos = matrixerer;





                                    //////////////////
                                    //UPPER ARM RIGHT
                                 

                                    MOVINGPOINTER = new Vector3(_player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                                    TORSOPIVOT = MOVINGPOINTER;
                                    _rotMatrixer = _player_rght_shldr[_index]._arrayOfInstances[_iterator].current_pos;
                                    Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                    direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                    direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                    direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                                    //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_up_ori * (_player_rght_shldr[_index]._total_torso_height * 0.5f));
                                    //MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * (_player_rght_shldr[_index]._total_torso_height * 0.5f));

                                    _rotatingMatrix = finalRotationMatrix;

                                    //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);
                                    //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                    //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                    //direction_feet_up = _getDirection(Vector3.Up, otherQuat);

                                    diffNormPosX = (MOVINGPOINTER.X) - _player_rght_shldr[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                    diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_shldr[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                    diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_shldr[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

                                    realPIVOTOfUpperArm = MOVINGPOINTER;

                                    realPositionOfUpperArm = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                                    realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_up * (diffNormPosY));
                                    realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_forward * (diffNormPosZ));

                                    realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_right * (diffNormPosX));
                                    realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_up * (diffNormPosY));
                                    realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_forward * (diffNormPosZ));
                                    //realPIVOTOfUpperArm = realPIVOTOfUpperArm + (direction_feet_up_ori * (_player_rght_shldr[_index]._total_torso_height * 0.5f));

                                    realPIVOTOfUpperArm.X = realPIVOTOfUpperArm.X + OFFSETPOS.X;
                                    realPIVOTOfUpperArm.Y = realPIVOTOfUpperArm.Y + OFFSETPOS.Y;
                                    realPIVOTOfUpperArm.Z = realPIVOTOfUpperArm.Z + OFFSETPOS.Z;

                                    Vector3 currentFINALPIVOTUPPERARM = realPIVOTOfUpperArm;
                                    //Vector3 currentFINALPIVOTUPPERARM = new Vector3(_player_rght_shldr[_index]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[_index]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[_index]._arrayOfInstances[_iterator].current_pos.M43);// realPIVOTOfUpperArm;

                                    _player_rght_upper_arm[_index]._arrayOfInstances[_iterator]._UPPERARMPIVOT = currentFINALPIVOTUPPERARM;

                                    //Vector3 somePosOfRightShoulder = new Vector3(_player_rght_shldr[_index].current_pos.M41, _player_rght_shldr[_index].current_pos.M42, _player_rght_shldr[_index].current_pos.M43);
                                    somePosOfRightHand = new Vector3(_player_rght_hnd[_index]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[_index]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[_index]._arrayOfInstances[_iterator].current_pos.M43);
                                    var somePosOfUpperElbowTargetTwo = new Vector3(_player_rght_elbow_target_two[_index]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_elbow_target_two[_index]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_elbow_target_two[_index]._arrayOfInstances[_iterator].current_pos.M43);
                                    var somePosOfUpperElbowTargetOne = new Vector3(_player_rght_elbow_target[_index]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_elbow_target[_index]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_elbow_target[_index]._arrayOfInstances[_iterator].current_pos.M43);

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

                                    _player_rght_upper_arm[_index]._arrayOfInstances[_iterator]._ARMLENGTH = totalArmLengthRight;

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

                                     diffNormPosXElbowRight = (_player_rght_upper_arm[_index]._arrayOfInstances[_iterator].current_pos.M41) - (TORSOPIVOT.X);
                                     diffNormPosYElbowRight = (_player_rght_upper_arm[_index]._arrayOfInstances[_iterator].current_pos.M42) - (TORSOPIVOT.Y);
                                     diffNormPosZElbowRight = (_player_rght_upper_arm[_index]._arrayOfInstances[_iterator].current_pos.M43) - (TORSOPIVOT.Z);

                                    MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                                    MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                                    MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));

                                    //someNewPointer.X = someNewPointer.X;// + MOVINGPOINTER.X;
                                    //someNewPointer.Y = someNewPointer.Y;// + MOVINGPOINTER.Y;
                                    //someNewPointer.Z = someNewPointer.Z;// + MOVINGPOINTER.Z;

                                    var elbowPositionRight = someNewPointer;
                                    _player_rght_upper_arm[_index]._arrayOfInstances[_iterator]._ELBOWPOSITION = elbowPositionRight;


                                    var dirPivotUpperRIghtToElbowRight = elbowPositionRight - currentFINALPIVOTUPPERARM;

                                    var currentPositionOfUPPERARMROTATION3DPOSITION = currentFINALPIVOTUPPERARM + (dirPivotUpperRIghtToElbowRight * 0.5f);

                                    var dirElbowRightToHand = somePosOfRightHand - elbowPositionRight;

                                    dirPivotUpperRIghtToElbowRight.Normalize();
                                    dirElbowRightToHand.Normalize();

                                    Vector3 someCross0;
                                    Vector3.Cross(ref dirPivotUpperRIghtToElbowRight, ref dirElbowRightToHand, out someCross0);
                                    someCross0.Normalize();

                                    _player_rght_upper_arm[_index]._arrayOfInstances[_iterator]._ELBOWCROSSVEC = someCross0;

                                    Vector3 someCross1;
                                    Vector3.Cross(ref dirPivotUpperRIghtToElbowRight, ref someCross0, out someCross1);
                                    someCross1.Normalize();

                                    //Vector3 upper = someCross1;
                                    //Vector3 forward = dirPivotUpperRIghtToElbowRight;
                                    //Vector3 upperWORLD = Vector3.Up;

                                    shoulderRotationMatrixRight = Matrix.LookAtRH(currentFINALPIVOTUPPERARM, currentFINALPIVOTUPPERARM + someCross0, dirPivotUpperRIghtToElbowRight);
                                    shoulderRotationMatrixRight.Invert();
                                    matrixerer = shoulderRotationMatrixRight;


                                    _player_rght_upper_arm[_index]._arrayOfInstances[_iterator]._SHOULDERROT = shoulderRotationMatrixRight;


                                    matrixerer.M41 = currentPositionOfUPPERARMROTATION3DPOSITION.X;// + OFFSETPOS.X;
                                    matrixerer.M42 = currentPositionOfUPPERARMROTATION3DPOSITION.Y;// + OFFSETPOS.Y;
                                    matrixerer.M43 = currentPositionOfUPPERARMROTATION3DPOSITION.Z;// + OFFSETPOS.Z;
                                    matrixerer.M44 = 1;


                                    _body_pos = matrixerer;
                                    Quaternion.RotationMatrix(ref _body_pos, out _quat);

                                    _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                    matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                                    //body.Position = new JVector(matrixerer.M41, matrixerer.M42, matrixerer.M43);
                                    //body.Orientation = matrixIn;
                                    worldMatrix_instances_r_upperarm[_index][_iterator] = matrixerer;// _player_pelvis[_index].current_pos;// translationMatrix;


                                    _player_rght_upper_arm[_index]._arrayOfInstances[_iterator].current_pos = matrixerer;


                                    //////////////////
                                    //RIGHT LOWER ARM

                                    somePosOfRightHand = new Vector3(_player_rght_hnd[_index]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[_index]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[_index]._arrayOfInstances[_iterator].current_pos.M43);

                                    var somePosererDir = somePosOfRightHand - _player_rght_upper_arm[_index]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                                    var someLowerRightArmPos = _player_rght_upper_arm[_index]._arrayOfInstances[_iterator]._ELBOWPOSITION + (somePosererDir * 0.5f);
                                    somePosererDir.Normalize();

                                    //someCross0.Z *= -1;
                                    //Vector3 someCross1;
                                    someCross0 = _player_rght_upper_arm[_index]._arrayOfInstances[_iterator]._ELBOWCROSSVEC;
                                    Vector3.Cross(ref somePosererDir, ref someCross0, out someCross1);
                                    someCross1.Normalize();



                                    var theLowerArmRotationMatrix = Matrix.LookAtRH(_player_rght_upper_arm[_index]._arrayOfInstances[_iterator]._ELBOWPOSITION, _player_rght_upper_arm[_index]._arrayOfInstances[_iterator]._ELBOWPOSITION + someCross1, somePosererDir);
                                    theLowerArmRotationMatrix.Invert();
                                    matrixerer = theLowerArmRotationMatrix;

                                    matrixerer.M41 = someLowerRightArmPos.X;// + OFFSETPOS.X;
                                    matrixerer.M42 = someLowerRightArmPos.Y;// + OFFSETPOS.Y;
                                    matrixerer.M43 = someLowerRightArmPos.Z;// + OFFSETPOS.Z;
                                    matrixerer.M44 = 1;


                                    _body_pos = matrixerer;
                                    Quaternion.RotationMatrix(ref _body_pos, out _quat);

                                    _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                    matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                                    //body.Position = new JVector(matrixerer.M41, matrixerer.M42, matrixerer.M43);
                                    //body.Orientation = matrixIn;
                                    worldMatrix_instances_r_lowerarm[_index][_iterator] = matrixerer;// _player_pelvis[_index].current_pos;// translationMatrix;


                                    _player_rght_lower_arm[_index]._arrayOfInstances[_iterator].current_pos = matrixerer;

                                    
                                    //////////////////////
                                    //ELBOW TARGET LEFT

                                    MOVINGPOINTER = new Vector3(_player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                                    TORSOPIVOT = MOVINGPOINTER;
                                    _rotMatrixer = _player_lft_elbow_target[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION;
                                    Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                    direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                    direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                    direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                                    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_lft_elbow_target[_index]._total_torso_height * 0.5f));

                                    _rotatingMatrix = finalRotationMatrix;
                                    //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                                    //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                    //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                    //direction_feet_up = _getDirection(Vector3.Up, otherQuat);


                                    diffNormPosX = (MOVINGPOINTER.X) - _player_lft_elbow_target[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                    diffNormPosY = (MOVINGPOINTER.Y) - _player_lft_elbow_target[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                    diffNormPosZ = (MOVINGPOINTER.Z) - _player_lft_elbow_target[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

                                    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                                    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * (diffNormPosY));
                                    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * (diffNormPosZ));

                                    MOVINGPOINTER.X += OFFSETPOS.X;// + _player_lft_elbow_target._ORIGINPOSITION.M41;
                                    MOVINGPOINTER.Y += OFFSETPOS.Y;// + _player_lft_elbow_target._ORIGINPOSITION.M42;// + _player_lft_elbow_target._ORIGINPOSITION.M42;
                                    MOVINGPOINTER.Z += OFFSETPOS.Z;// + _player_lft_elbow_target._ORIGINPOSITION.M43;

                                     someDiffX = MOVINGPOINTER.X - _player_lft_hnd[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                     someDiffY = MOVINGPOINTER.Y - _player_lft_hnd[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                     someDiffZ = MOVINGPOINTER.Z - _player_lft_hnd[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

                                     somePosOfPivotUpperArm = new Vector3(_player_lft_shldr[_index]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_shldr[_index]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_shldr[_index]._arrayOfInstances[_iterator].current_pos.M43); //new Vector3(realPIVOTOfUpperArm.X, realPIVOTOfUpperArm.Y, realPIVOTOfUpperArm.Z); ;// new Vector3(_player_rght_shldr.current_pos.M41, _player_rght_shldr.current_pos.M42, _player_rght_shldr.current_pos.M43);
                                    somePosOfRightHand = new Vector3(_player_lft_hnd[_index]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_hnd[_index]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_hnd[_index]._arrayOfInstances[_iterator].current_pos.M43);

                                    _player_lft_upper_arm[_index]._arrayOfInstances[_iterator]._UPPERARMPIVOT = somePosOfPivotUpperArm;

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

                                    diffNormPosXElbowRight = (_player_lft_elbow_target[_index]._arrayOfInstances[_iterator].current_pos.M41) - (TORSOPIVOT.X);
                                    diffNormPosYElbowRight = (_player_lft_elbow_target[_index]._arrayOfInstances[_iterator].current_pos.M42) - (TORSOPIVOT.Y);
                                    diffNormPosZElbowRight = (_player_lft_elbow_target[_index]._arrayOfInstances[_iterator].current_pos.M43) - (TORSOPIVOT.Z);

                                    MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                                    MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                                    MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));

                                    matrixerer = Matrix.Identity;
                                    _rotatingMatrix = finalRotationMatrix;
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


                                    _body_pos = matrixerer;
                                    Quaternion.RotationMatrix(ref _body_pos, out _quat);

                                    _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                    matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                                    //body.Position = new JVector(matrixerer.M41, matrixerer.M42, matrixerer.M43);
                                    //body.Orientation = matrixIn;
                                    worldMatrix_instances_l_elbow_target[_index][_iterator] = matrixerer;// _player_pelvis[_index].current_pos;// translationMatrix;


                                    _player_lft_elbow_target[_index]._arrayOfInstances[_iterator].current_pos = matrixerer;

                                    //////////////////////////
                                    //ELBOW TARGET LEFT TWO
                             
                                    MOVINGPOINTER = new Vector3(_player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                                    TORSOPIVOT = MOVINGPOINTER;

                                    _rotMatrixer = _player_lft_elbow_target_two[_index]._arrayOfInstances[_iterator].current_pos;
                                    Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                    direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                    direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                    direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                                    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_lft_elbow_target_two[_index]._total_torso_height * 0.5f));
                                    _rotatingMatrix = finalRotationMatrix;
                                    //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                                    //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                    //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                    //direction_feet_up = _getDirection(Vector3.Up, otherQuat);


                                    diffNormPosX = (MOVINGPOINTER.X) - _player_lft_elbow_target_two[_index]._arrayOfInstances[_iterator].current_pos.M41;
                                    diffNormPosY = (MOVINGPOINTER.Y) - _player_lft_elbow_target_two[_index]._arrayOfInstances[_iterator].current_pos.M42;
                                    diffNormPosZ = (MOVINGPOINTER.Z) - _player_lft_elbow_target_two[_index]._arrayOfInstances[_iterator].current_pos.M43;


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

                                    someDiffX = MOVINGPOINTER.X - _player_lft_hnd[_index]._arrayOfInstances[_iterator].current_pos.M41;
                                    someDiffY = MOVINGPOINTER.Y - _player_lft_hnd[_index]._arrayOfInstances[_iterator].current_pos.M42;
                                    someDiffZ = MOVINGPOINTER.Z - _player_lft_hnd[_index]._arrayOfInstances[_iterator].current_pos.M43;

                                    somePosOfRightHand = new Vector3(_player_lft_hnd[_index]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_hnd[_index]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_hnd[_index]._arrayOfInstances[_iterator].current_pos.M43);

                                    //dirShoulderToHand = somePosOfRightHand - new Vector3(_player_rght_upper_arm[_index].current_pos.M41, _player_rght_upper_arm[_index].current_pos.M42, _player_rght_upper_arm[_index].current_pos.M43);
                                    //------------------------------------------var dirShoulderToHand = somePosOfRightHand - new Vector3(_player_rght_shldr[_index].current_pos.M41, _player_rght_shldr[_index].current_pos.M42, _player_rght_shldr[_index].current_pos.M43);
                                    dirShoulderToHand = somePosOfRightHand - _player_lft_upper_arm[_index]._arrayOfInstances[_iterator]._UPPERARMPIVOT;


                                    MOVINGPOINTER = _player_lft_upper_arm[_index]._arrayOfInstances[_iterator]._UPPERARMPIVOT + (dirShoulderToHand * 2.5f);

                                     someOffsetter = somePosOfRightHand - OFFSETPOS;
                                     someOtherPivotPoint = MOVINGPOINTER;

                                    //MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * 1.0f);
                                    //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_forward_ori * 1.0f);

                                    someNewPointer = MOVINGPOINTER;

                                    diffNormPosXElbowRight = (_player_lft_elbow_target_two[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41) - (TORSOPIVOT.X);
                                    diffNormPosYElbowRight = (_player_lft_elbow_target_two[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42) - (TORSOPIVOT.Y);
                                    diffNormPosZElbowRight = (_player_lft_elbow_target_two[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43) - (TORSOPIVOT.Z);

                                    MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                                    MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                                    MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));

                                    matrixerer = Matrix.Identity;
                                    _rotatingMatrix = finalRotationMatrix;
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

                                    _body_pos = matrixerer;
                                    Quaternion.RotationMatrix(ref _body_pos, out _quat);

                                    _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                    matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                                    //body.Position = new JVector(matrixerer.M41, matrixerer.M42, matrixerer.M43);
                                    //body.Orientation = matrixIn;
                                    worldMatrix_instances_l_elbow_target_two[_index][_iterator] = matrixerer;// _player_pelvis[_index].current_pos;// translationMatrix;

                                    _player_lft_elbow_target_two[_index]._arrayOfInstances[_iterator].current_pos = matrixerer;





                                    
                                    //////////////////
                                    //UPPER ARM LEFT
                              

                                    MOVINGPOINTER = new Vector3(_player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                                    TORSOPIVOT = MOVINGPOINTER;
                                    _rotMatrixer = _player_lft_shldr[_index]._arrayOfInstances[_iterator].current_pos;
                                    Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                    direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                    direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                    direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                                    //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_up_ori * (_player_lft_shldr[_index]._total_torso_height * 0.5f));
                                    //MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * (_player_lft_shldr[_index]._total_torso_height * 0.5f));

                                    _rotatingMatrix = finalRotationMatrix;

                                    //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);
                                    //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                    //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                    //direction_feet_up = _getDirection(Vector3.Up, otherQuat);

                                    diffNormPosX = (MOVINGPOINTER.X) - _player_lft_shldr[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                    diffNormPosY = (MOVINGPOINTER.Y) - _player_lft_shldr[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                    diffNormPosZ = (MOVINGPOINTER.Z) - _player_lft_shldr[_index]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

                                    realPIVOTOfUpperArm = MOVINGPOINTER;

                                    realPositionOfUpperArm = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                                    realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_up * (diffNormPosY));
                                    realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_forward * (diffNormPosZ));

                                    realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_right * (diffNormPosX));
                                    realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_up * (diffNormPosY));
                                    realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_forward * (diffNormPosZ));
                                    //realPIVOTOfUpperArm = realPIVOTOfUpperArm + (direction_feet_up_ori * (_player_lft_shldr[_index]._total_torso_height * 0.5f));

                                    realPIVOTOfUpperArm.X = realPIVOTOfUpperArm.X + OFFSETPOS.X;
                                    realPIVOTOfUpperArm.Y = realPIVOTOfUpperArm.Y + OFFSETPOS.Y;
                                    realPIVOTOfUpperArm.Z = realPIVOTOfUpperArm.Z + OFFSETPOS.Z;

                                    currentFINALPIVOTUPPERARM = realPIVOTOfUpperArm;
                                    //Vector3 currentFINALPIVOTUPPERARM = new Vector3(_player_lft_shldr[_index]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_shldr[_index]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_shldr[_index]._arrayOfInstances[_iterator].current_pos.M43);// realPIVOTOfUpperArm;

                                    _player_lft_upper_arm[_index]._arrayOfInstances[_iterator]._UPPERARMPIVOT = currentFINALPIVOTUPPERARM;

                                    //Vector3 somePosOfRightShoulder = new Vector3(_player_lft_shldr[_index].current_pos.M41, _player_lft_shldr[_index].current_pos.M42, _player_lft_shldr[_index].current_pos.M43);
                                    somePosOfRightHand = new Vector3(_player_lft_hnd[_index]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_hnd[_index]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_hnd[_index]._arrayOfInstances[_iterator].current_pos.M43);
                                    somePosOfUpperElbowTargetTwo = new Vector3(_player_lft_elbow_target_two[_index]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_elbow_target_two[_index]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_elbow_target_two[_index]._arrayOfInstances[_iterator].current_pos.M43);
                                    somePosOfUpperElbowTargetOne = new Vector3(_player_lft_elbow_target[_index]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_elbow_target[_index]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_elbow_target[_index]._arrayOfInstances[_iterator].current_pos.M43);

                                    someDirFromElbowTargetOneToTwo = somePosOfUpperElbowTargetTwo - somePosOfUpperElbowTargetOne;
                                    someDirFromElbowTargetOneToRghtHand = somePosOfRightHand - somePosOfUpperElbowTargetOne;

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

                                    _player_lft_upper_arm[_index]._arrayOfInstances[_iterator]._ARMLENGTH = totalArmLengthRight;

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

                                    diffNormPosXElbowRight = (_player_lft_upper_arm[_index]._arrayOfInstances[_iterator].current_pos.M41) - (TORSOPIVOT.X);
                                    diffNormPosYElbowRight = (_player_lft_upper_arm[_index]._arrayOfInstances[_iterator].current_pos.M42) - (TORSOPIVOT.Y);
                                    diffNormPosZElbowRight = (_player_lft_upper_arm[_index]._arrayOfInstances[_iterator].current_pos.M43) - (TORSOPIVOT.Z);

                                    MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                                    MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                                    MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));

                                    //someNewPointer.X = someNewPointer.X;// + MOVINGPOINTER.X;
                                    //someNewPointer.Y = someNewPointer.Y;// + MOVINGPOINTER.Y;
                                    //someNewPointer.Z = someNewPointer.Z;// + MOVINGPOINTER.Z;

                                    elbowPositionRight = someNewPointer;
                                    _player_lft_upper_arm[_index]._arrayOfInstances[_iterator]._ELBOWPOSITION = elbowPositionRight;


                                    dirPivotUpperRIghtToElbowRight = elbowPositionRight - currentFINALPIVOTUPPERARM;

                                    currentPositionOfUPPERARMROTATION3DPOSITION = currentFINALPIVOTUPPERARM + (dirPivotUpperRIghtToElbowRight * 0.5f);

                                    dirElbowRightToHand = somePosOfRightHand - elbowPositionRight;

                                    dirPivotUpperRIghtToElbowRight.Normalize();
                                    dirElbowRightToHand.Normalize();

                                    Vector3.Cross(ref dirPivotUpperRIghtToElbowRight, ref dirElbowRightToHand, out someCross0);
                                    someCross0.Normalize();

                                    _player_lft_upper_arm[_index]._arrayOfInstances[_iterator]._ELBOWCROSSVEC = someCross0;


                                    Vector3.Cross(ref dirPivotUpperRIghtToElbowRight, ref someCross0, out someCross1);
                                    someCross1.Normalize();

                                    //Vector3 upper = someCross1;
                                    //Vector3 forward = dirPivotUpperRIghtToElbowRight;
                                    //Vector3 upperWORLD = Vector3.Up;

                                    shoulderRotationMatrixRight = Matrix.LookAtRH(currentFINALPIVOTUPPERARM, currentFINALPIVOTUPPERARM + someCross0, dirPivotUpperRIghtToElbowRight);
                                    shoulderRotationMatrixRight.Invert();
                                    matrixerer = shoulderRotationMatrixRight;


                                    _player_lft_upper_arm[_index]._arrayOfInstances[_iterator]._SHOULDERROT = shoulderRotationMatrixRight;


                                    matrixerer.M41 = currentPositionOfUPPERARMROTATION3DPOSITION.X;// + OFFSETPOS.X;
                                    matrixerer.M42 = currentPositionOfUPPERARMROTATION3DPOSITION.Y;// + OFFSETPOS.Y;
                                    matrixerer.M43 = currentPositionOfUPPERARMROTATION3DPOSITION.Z;// + OFFSETPOS.Z;
                                    matrixerer.M44 = 1;


                                    _body_pos = matrixerer;
                                    Quaternion.RotationMatrix(ref _body_pos, out _quat);

                                    _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                    matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                                    //body.Position = new JVector(matrixerer.M41, matrixerer.M42, matrixerer.M43);
                                    //body.Orientation = matrixIn;
                                    worldMatrix_instances_l_upperarm[_index][_iterator] = matrixerer;// _player_pelvis[_index].current_pos;// translationMatrix;


                                    _player_lft_upper_arm[_index]._arrayOfInstances[_iterator].current_pos = matrixerer;


                                    
                                    /////////////////
                                    //LEFT LOWER ARM
                                    somePosOfRightHand = new Vector3(_player_lft_hnd[_index]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_hnd[_index]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_hnd[_index]._arrayOfInstances[_iterator].current_pos.M43);

                                    somePosererDir = somePosOfRightHand - _player_lft_upper_arm[_index]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                                    someLowerRightArmPos = _player_lft_upper_arm[_index]._arrayOfInstances[_iterator]._ELBOWPOSITION + (somePosererDir * 0.5f);
                                    somePosererDir.Normalize();

                                    //someCross0.Z *= -1;
                                    someCross0 = _player_lft_upper_arm[_index]._arrayOfInstances[_iterator]._ELBOWCROSSVEC;
                                    Vector3.Cross(ref somePosererDir, ref someCross0, out someCross1);
                                    someCross1.Normalize();



                                    theLowerArmRotationMatrix = Matrix.LookAtRH(_player_lft_upper_arm[_index]._arrayOfInstances[_iterator]._ELBOWPOSITION, _player_lft_upper_arm[_index]._arrayOfInstances[_iterator]._ELBOWPOSITION + someCross1, somePosererDir);
                                    theLowerArmRotationMatrix.Invert();


                                    matrixerer = theLowerArmRotationMatrix;

                                    matrixerer.M41 = someLowerRightArmPos.X;// + OFFSETPOS.X;
                                    matrixerer.M42 = someLowerRightArmPos.Y;// + OFFSETPOS.Y;
                                    matrixerer.M43 = someLowerRightArmPos.Z;// + OFFSETPOS.Z;
                                    matrixerer.M44 = 1;

                                    _body_pos = matrixerer;
                                    Quaternion.RotationMatrix(ref _body_pos, out _quat);

                                    _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                    matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                                    //body.Position = new JVector(matrixerer.M41, matrixerer.M42, matrixerer.M43);
                                    //body.Orientation = matrixIn;
                                    worldMatrix_instances_l_lowerarm[_index][_iterator] = matrixerer;// _player_pelvis[_index].current_pos;// translationMatrix;
                                    _player_lft_lower_arm[_index]._arrayOfInstances[_iterator].current_pos = matrixerer;
                                    


























                                    //PELVIS
                                    var _cuber_pelvis = _player_pelvis[_index];
                                    _spine_upper_body_pos = new Vector3(_cuber_pelvis._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _cuber_pelvis._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _cuber_pelvis._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                                    var MOVINGPOINTERPELVIS = _spine_upper_body_pos;

                                    MOVINGPOINTERPELVIS.X += OFFSETPOS.X;
                                    MOVINGPOINTERPELVIS.Y += OFFSETPOS.Y;
                                    MOVINGPOINTERPELVIS.Z += OFFSETPOS.Z;

                                    matrixerer = Matrix.Identity;

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

                                    matrixerer.M41 = MOVINGPOINTERPELVIS.X;
                                    matrixerer.M42 = MOVINGPOINTERPELVIS.Y;
                                    matrixerer.M43 = MOVINGPOINTERPELVIS.Z;
                                    matrixerer.M44 = 1;

                                    _body_pos = matrixerer;
                                    //Quaternion _quat;
                                    Quaternion.RotationMatrix(ref _body_pos, out _quat);

                                    _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                    matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                                    //body.Position = new JVector(matrixerer.M41, matrixerer.M42, matrixerer.M43);
                                    //body.Orientation = matrixIn;
                                    worldMatrix_instances_pelvis[_index][_iterator] = matrixerer;// _player_pelvis[_index].current_pos;// translationMatrix;

                                    _player_pelvis[_index]._arrayOfInstances[_iterator].current_pos = matrixerer;

                                }

















































                                ///////////
                                //TORSO////
                                ///////////
                                var _cuber_001 = _player_torso[_index];
                                _cuber_001.Render(D3D.device.ImmediateContext);
                                _shaderManager._rend_torso(D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_torso_BUFFER, oculusRiftDir, _cuber_001);

                                //PLAYER PELVIS
                                _cuber_001 = _player_pelvis[_index];
                                _cuber_001.Render(D3D.device.ImmediateContext);
                                _shaderManager._rend_pelvis(D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_pelvis_BUFFER, oculusRiftDir, _cuber_001);
                                //_player_pelvis[_index]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_pelvis[_index]._POSITION.M41, _player_pelvis[_index]._POSITION.M42, _player_pelvis[_index]._POSITION.M43);

                                _player_rght_hnd[_index].Render(D3D.device.ImmediateContext);
                                _shaderManager._rend_rgt_hnd(D3D.device.ImmediateContext, _player_rght_hnd[_index].IndexCount, _player_rght_hnd[_index].InstanceCount, _player_rght_hnd[_index]._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_rght_hnd_BUFFER, oculusRiftDir, _player_rght_hnd[_index]);

                                _player_lft_hnd[_index].Render(D3D.device.ImmediateContext);
                                _shaderManager._rend_lft_hnd(D3D.device.ImmediateContext, _player_lft_hnd[_index].IndexCount, _player_lft_hnd[_index].InstanceCount, _player_lft_hnd[_index]._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_hnd_BUFFER, oculusRiftDir, _player_lft_hnd[_index]);

                                
                                _cuber_001 = _player_rght_shldr[_index];
                                _cuber_001.Render(D3D.device.ImmediateContext);
                                _shaderManager._rend_rgt_shldr(D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_rght_shldr_BUFFER, oculusRiftDir, _cuber_001);
                                
                                _cuber_001 = _player_rght_lower_arm[_index];
                                _cuber_001.Render(D3D.device.ImmediateContext);
                                _shaderManager._rend_rgt_lower_arm(D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_rght_lower_arm_BUFFER, oculusRiftDir, _cuber_001);
                                
                                _cuber_001 = _player_rght_upper_arm[_index];
                                _cuber_001.Render(D3D.device.ImmediateContext);
                                _shaderManager._rend_rgt_upper_arm(D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_rght_upper_arm_BUFFER, oculusRiftDir, _cuber_001);

                             
                                _cuber_001 = _player_lft_shldr[_index];
                                _cuber_001.Render(D3D.device.ImmediateContext);
                                _shaderManager._rend_lft_shldr(D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_shldr_BUFFER, oculusRiftDir, _cuber_001);
                                
                                _cuber_001 = _player_lft_lower_arm[_index];
                                _cuber_001.Render(D3D.device.ImmediateContext);
                                _shaderManager._rend_lft_lower_arm(D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_lower_arm_BUFFER, oculusRiftDir, _cuber_001);
                                
                                _cuber_001 = _player_lft_upper_arm[_index];
                                _cuber_001.Render(D3D.device.ImmediateContext);
                                _shaderManager._rend_lft_upper_arm(D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_upper_arm_BUFFER, oculusRiftDir, _cuber_001);
                              
                             


                                /*_cuber_01 = _player_rght_elbow_target[_index];
                                 _cuber_01.Render(D3D.device.ImmediateContext);
                                 _shaderManager._rend_rgt_elbow_targ(D3D.device.ImmediateContext, _cuber_01.IndexCount, _cuber_01.InstanceCount, _cuber_01._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_hnd_BUFFER, oculusRiftDir, _cuber_01);

                                 _cuber_01 = _player_rght_elbow_target_two[_index];
                                 _cuber_01.Render(D3D.device.ImmediateContext);
                                 _shaderManager._rend_rgt_elbow_targ_two(D3D.device.ImmediateContext, _cuber_01.IndexCount, _cuber_01.InstanceCount, _cuber_01._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_hnd_BUFFER, oculusRiftDir, _cuber_01);
                                 */
                                /*
                              _cuber_01 = _player_lft_elbow_target[_index];
                              _cuber_01.Render(D3D.device.ImmediateContext);
                              _shaderManager._rend_lft_elbow_targ(D3D.device.ImmediateContext, _cuber_01.IndexCount, _cuber_01.InstanceCount, _cuber_01._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_hnd_BUFFER, oculusRiftDir, _cuber_01);

                              _cuber_01 = _player_lft_elbow_target_two[_index];
                              _cuber_01.Render(D3D.device.ImmediateContext);
                              _shaderManager._rend_lft_elbow_targ_two(D3D.device.ImmediateContext, _cuber_01.IndexCount, _cuber_01.InstanceCount, _cuber_01._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_hnd_BUFFER, oculusRiftDir, _cuber_01);
                              */





















                                /*
                                //HEAD
                                _SC_modL_head_BUFFER[0] = new _sc_core_systems.SC_Graphics._sc_voxel.DLightBuffer()
                                {
                                    ambientColor = ambientColor,
                                    diffuseColor = diffuseColour,
                                    lightDirection = dirLight,
                                    padding0 = 7,
                                    lightPosition = new Vector3(_player_head[indexer]._POSITION.M41, _player_head[indexer]._POSITION.M42, _player_head[indexer]._POSITION.M43),
                                    padding1 = 100
                                };
                                 _rotatingMatrix = finalRotationMatrix;

                                MOVINGPOINTER = new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                                _rotMatrixer = _player_head[indexer]._ORIGINPOSITION;
                                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_head[indexer]._total_torso_height * 0.5f));
                                 _rotatingMatrix = finalRotationMatrix;
                                //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                                //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                //direction_feet_up = _getDirection(Vector3.Up, otherQuat);


                                diffNormPosX = (MOVINGPOINTER.X) - _player_head[indexer]._ORIGINPOSITION.M41;
                                diffNormPosY = (MOVINGPOINTER.Y) - _player_head[indexer]._ORIGINPOSITION.M42;
                                diffNormPosZ = (MOVINGPOINTER.Z) - _player_head[indexer]._ORIGINPOSITION.M43;


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
                                 _rotatingMatrix = finalRotationMatrix;
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
                                //_player_head[indexer]._POSITION = matrixerer;

                                //_player_head[indexer].Render(D3D.device.ImmediateContext);
                                //_player_head[indexer].RenderInstancedObject(D3D.device.ImmediateContext, _player_head[indexer].IndexCount, _player_head[indexer].InstanceCount, _player_head[indexer]._POSITION, viewMatrix, _projectionMatrix, null, worldMatrix_Terrain_instances, _SC_modL_head_BUFFER, oculusRiftDir);
                                //_player_head[indexer]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_head[indexer]._POSITION.M41, _player_head[indexer]._POSITION.M42, _player_head[indexer]._POSITION.M43);

                                _player_head[indexer].Render(D3D.device.ImmediateContext);
                                _cuber_pelvis = _player_head[indexer];
                                _cuber_pelvis.Render(D3D.device.ImmediateContext);
                                SC_Console_GRAPHICS._shaderManager._rend_torso(D3D.device.ImmediateContext, _cuber_pelvis.IndexCount, _cuber_pelvis.InstanceCount, _cuber_pelvis._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_head_BUFFER, oculusRiftDir, _cuber_pelvis);
                                _player_head[indexer]._singleObjectOnly.transform.Component.rigidbody.Position = new JVector(_player_head[indexer]._POSITION.M41, _player_head[indexer]._POSITION.M42, _player_head[indexer]._POSITION.M43);
                                */
                            }
                        }
                    }

                    D3D.result = eyeTexture.SwapTextureSet.Commit();
                    D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to commit the swap chain texture.");
                }




                //TERRAIN CUBE
                _terrain._WORLDMATRIXINSTANCES = worldMatrix_instances_terrain[0];
                _terrain._POSITION = worldMatrix_base[0];

                var cuber = _terrain;
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
                //END OF

                //PHYSICS SCREENS
                _world_screen_list[0]._WORLDMATRIXINSTANCES = worldMatrix_instances_screens[0];
                _world_screen_list[0]._POSITION = worldMatrix_base[0];
                //END OF 

                Vector3 screenPos = new Vector3(worldMatrix_instances_screens[0][0].M41, worldMatrix_instances_screens[0][0].M42, worldMatrix_instances_screens[0][0].M43);
                float distancer;
                Vector3.Distance(ref screenPos, ref OFFSETPOS, out distancer);

                //SCREEN CORNERS
                //SETTING SCREEN CORNER POSITION
                Matrix screen_mat = worldMatrix_instances_screens[0][0];
                Quaternion.RotationMatrix(ref screen_mat, out _testQuater);
                _testQuater.Normalize();

                var screenNormalForwarder = _getDirection(Vector3.ForwardRH, _testQuater);
                screenNormalForwarder.Normalize();

                var screenNormalRighter = _getDirection(Vector3.Right, _testQuater);
                screenNormalRighter.Normalize();

                var screenNormalToper = _getDirection(Vector3.Up, _testQuater);
                screenNormalToper.Normalize();
                for (int i = 0; i < _screenDirMatrix.Length; i++)
                {
                    Vector3 vert = screenPos + (screenNormalForwarder * _screenDirMatrix[i].M43);// point3DCollection[i] -;
                    vert = vert + (screenNormalRighter * _screenDirMatrix[i].M41);
                    vert = vert + (screenNormalToper * _screenDirMatrix[i].M42);

                    _screenDirMatrix_correct_pos[i].M41 = vert.X;
                    _screenDirMatrix_correct_pos[i].M42 = vert.Y;
                    _screenDirMatrix_correct_pos[i].M43 = vert.Z;

                    worldMatrix_instances_screen_assets[0][i] = screen_mat;

                    worldMatrix_instances_screen_assets[0][i].M41 = _screenDirMatrix_correct_pos[i].M41;
                    worldMatrix_instances_screen_assets[0][i].M42 = _screenDirMatrix_correct_pos[i].M42;
                    worldMatrix_instances_screen_assets[0][i].M43 = _screenDirMatrix_correct_pos[i].M43;
                }





                //SCREEN HMDPOINTER
                if (_out_of_bounds_oculus_rift == 1 || distancer >= 4)
                {
                    Vector3 tester00 = new Vector3(_screenDirMatrix_correct_pos[2].M41, _screenDirMatrix_correct_pos[2].M42, _screenDirMatrix_correct_pos[2].M43);
                    _oculusR_Cursor_matrix = screen_mat;

                    _oculusR_Cursor_matrix.M41 = tester00.X;
                    _oculusR_Cursor_matrix.M42 = tester00.Y;
                    _oculusR_Cursor_matrix.M43 = tester00.Z;
                    worldMatrix_instances_screen_assets[0][4] = _oculusR_Cursor_matrix;
                }
                else
                {
                    worldMatrix_instances_screen_assets[0][4] = _oculusR_Cursor_matrix;
                }
                //END OF 

                //OCULUS TOUCH RIGHT
                if (_out_of_bounds_right == 1 || distancer >= 4)
                {
                    Vector3 tester00 = new Vector3(_screenDirMatrix_correct_pos[2].M41, _screenDirMatrix_correct_pos[2].M42, _screenDirMatrix_correct_pos[2].M43);
                    _intersectTouchRightMatrix = screen_mat;

                    _intersectTouchRightMatrix.M41 = tester00.X;
                    _intersectTouchRightMatrix.M42 = tester00.Y;
                    _intersectTouchRightMatrix.M43 = tester00.Z;
                    worldMatrix_instances_screen_assets[0][5] = _intersectTouchRightMatrix;

                }
                else
                {
                    Vector3 tester00 = new Vector3(intersectPointRight.X, intersectPointRight.Y, intersectPointRight.Z);
                    _intersectTouchRightMatrix = screen_mat;

                    _intersectTouchRightMatrix.M41 = tester00.X;
                    _intersectTouchRightMatrix.M42 = tester00.Y;
                    _intersectTouchRightMatrix.M43 = tester00.Z;
                    worldMatrix_instances_screen_assets[0][5] = _intersectTouchRightMatrix;
                }
                //END OF

                //OCULUS TOUCH LEFT
                if (_out_of_bounds_left == 1 || distancer >= 4)
                {
                    Vector3 tester00 = new Vector3(_screenDirMatrix_correct_pos[2].M41, _screenDirMatrix_correct_pos[2].M42, _screenDirMatrix_correct_pos[2].M43);
                    _intersectTouchLeftMatrix = screen_mat;

                    _intersectTouchLeftMatrix.M41 = tester00.X;
                    _intersectTouchLeftMatrix.M42 = tester00.Y;
                    _intersectTouchLeftMatrix.M43 = tester00.Z;
                    worldMatrix_instances_screen_assets[0][6] = _intersectTouchLeftMatrix;
                }
                else
                {
                    Vector3 tester00 = new Vector3(intersectPointLeft.X, intersectPointLeft.Y, intersectPointLeft.Z);
                    _intersectTouchLeftMatrix = screen_mat;

                    _intersectTouchLeftMatrix.M41 = tester00.X;
                    _intersectTouchLeftMatrix.M42 = tester00.Y;
                    _intersectTouchLeftMatrix.M43 = tester00.Z;
                    worldMatrix_instances_screen_assets[0][6] = _intersectTouchLeftMatrix;
                }
                //END OF

                worldMatrix_instances_screen_assets[0][7] = _screenDirMatrix_correct_pos[0];
                worldMatrix_instances_screen_assets[0][8] = _screenDirMatrix_correct_pos[0];

                //PHYSICS SCREEN ASSETS
                _world_screen_assets_list[0]._WORLDMATRIXINSTANCES = worldMatrix_instances_screen_assets[0];
                _world_screen_assets_list[0]._POSITION = worldMatrix_base[0];
                //END OF 

                //PHYSICS SCREENS
                cuber = _world_screen_list[0];
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
                //END OF 

                //PHYSICS SCREEN ASSETS
                cuber = _world_screen_assets_list[0];
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
                //END OF


                for (int x = 0; x < _physics_engine_instance_x; x++)
                {
                    for (int y = 0; y < _physics_engine_instance_y; y++)
                    {
                        for (int z = 0; z < _physics_engine_instance_z; z++)
                        {
                            var indexer00 = x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z);

                            //TERRAIN TILES CUBES
                            //_world_terrain_tile_list[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_terrain_tiles[indexer00];
                            //_world_terrain_tile_list[indexer00]._POSITION = worldMatrix_base[0];
                            //END OF 

                            //PHYSICS CUBES
                            _world_cube_list[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_cubes[indexer00];
                            _world_cube_list[indexer00]._POSITION = worldMatrix_base[0];
                            //END OF 

                            //PHYSICS VOXEL SPHEROID
                            _world_voxel_cube_lists[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_voxel_cube[indexer00];
                            _world_voxel_cube_lists[indexer00]._POSITION = worldMatrix_base[0];
                            //END OF 

                            //PHYSICS SPECTRUM
                            //_world_spectrum_list[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_spectrum[indexer00];
                            //_world_spectrum_list[indexer00]._POSITION = worldMatrix_base[0];
                            //END OF 

                            /*//TERRAIN TILES CUBES
                            cuber = _world_terrain_tile_list[indexer00];
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
                            //END OF */



                            //PHYSICS CUBES
                            cuber = _world_cube_list[indexer00];
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
                            //END OF



                            //PHYSICS VOXEL THINGS
                            var voxel_cube = _world_voxel_cube_lists[indexer00];
                            var instances_voxel_cube = voxel_cube.instances;
                            var _voxel_cube_Worldmatrix_of_instances = voxel_cube._WORLDMATRIXINSTANCES;

                            for (int i = 0; i < instances_voxel_cube.Length; i++)
                            {
                                float xxx = _voxel_cube_Worldmatrix_of_instances[i].M41;
                                float yyy = _voxel_cube_Worldmatrix_of_instances[i].M42;
                                float zzz = _voxel_cube_Worldmatrix_of_instances[i].M43;

                                voxel_cube.instances[i].position.X = xxx;
                                voxel_cube.instances[i].position.Y = yyy;
                                voxel_cube.instances[i].position.Z = zzz;
                                voxel_cube.instances[i].position.W = 1;
                                Quaternion.RotationMatrix(ref _voxel_cube_Worldmatrix_of_instances[i], out _testQuater);

                                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                                voxel_cube.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cube.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cube.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cube.instancesDataForward[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Right, _testQuater);
                                voxel_cube.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cube.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cube.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cube.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Up, _testQuater);
                                voxel_cube.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cube.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cube.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cube.instancesDataUP[i].rotation.W = 1;
                            }
                            //END OF 



                            //

                            /*
                            //PHYSICS SPECTRUM
                            var _world_spectrum = _world_spectrum_list[indexer00];
                            var instancars01 = _world_spectrum.instances;
                            var sometestar01 = _world_spectrum._WORLDMATRIXINSTANCES;

                            for (int i = 0; i < instancars01.Length; i++)
                            {
                                float xxx = sometestar01[i].M41;
                                float yyy = sometestar01[i].M42;
                                float zzz = sometestar01[i].M43;

                                _world_spectrum.instances[i].position.X = xxx;
                                _world_spectrum.instances[i].position.Y = yyy;
                                _world_spectrum.instances[i].position.Z = zzz;
                                _world_spectrum.instances[i].position.W = 1;
                                Quaternion.RotationMatrix(ref sometestar01[i], out _testQuater);

                                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                                _world_spectrum.instancesDataForward[i].rotation.X = dirInstance.X;
                                _world_spectrum.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                _world_spectrum.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                _world_spectrum.instancesDataForward[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Right, _testQuater);
                                _world_spectrum.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                _world_spectrum.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                _world_spectrum.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                _world_spectrum.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Up, _testQuater);
                                _world_spectrum.instancesDataUP[i].rotation.X = dirInstance.X;
                                _world_spectrum.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                _world_spectrum.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                _world_spectrum.instancesDataUP[i].rotation.W = 1;
                            }
                            //END OF */



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
                            _player_rght_hnd[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_hand[indexer00];
                            _player_rght_hnd[indexer00]._POSITION = worldMatrix_base[0];

                            var voxel_cuber = _player_rght_hnd[indexer00];
                            var voxel_instancers = voxel_cuber.instances;
                            var voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                            for (int i = 0; i < voxel_instancers.Length; i++)
                            {
                                float xxx = voxel_sometester[i].M41;
                                float yyy = voxel_sometester[i].M42;
                                float zzz = voxel_sometester[i].M43;

                                voxel_cuber.instances[i].position.X = xxx;
                                voxel_cuber.instances[i].position.Y = yyy;
                                voxel_cuber.instances[i].position.Z = zzz;
                                voxel_cuber.instances[i].position.W = 1;
                                Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester[i], out _testQuater);

                                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Right, _testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Up, _testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }


                            //PHYSICS HAND LEFT
                            _player_lft_hnd[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_hand[indexer00];
                            _player_lft_hnd[indexer00]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_lft_hnd[indexer00];
                            voxel_instancers = voxel_cuber.instances;
                            voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                            for (int i = 0; i < voxel_instancers.Length; i++)
                            {
                                float xxx = voxel_sometester[i].M41;
                                float yyy = voxel_sometester[i].M42;
                                float zzz = voxel_sometester[i].M43;

                                voxel_cuber.instances[i].position.X = xxx;
                                voxel_cuber.instances[i].position.Y = yyy;
                                voxel_cuber.instances[i].position.Z = zzz;
                                voxel_cuber.instances[i].position.W = 1;
                                Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester[i], out _testQuater);

                                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Right, _testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Up, _testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }








                            //PHYSICS UPPER ARM LEFT
                            _player_lft_upper_arm[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_upperarm[indexer00];
                            _player_lft_upper_arm[indexer00]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_lft_upper_arm[indexer00];
                            voxel_instancers = voxel_cuber.instances;
                            voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                            for (int i = 0; i < voxel_instancers.Length; i++)
                            {
                                float xxx = voxel_sometester[i].M41;
                                float yyy = voxel_sometester[i].M42;
                                float zzz = voxel_sometester[i].M43;

                                voxel_cuber.instances[i].position.X = xxx;
                                voxel_cuber.instances[i].position.Y = yyy;
                                voxel_cuber.instances[i].position.Z = zzz;
                                voxel_cuber.instances[i].position.W = 1;
                                Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester[i], out _testQuater);

                                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Right, _testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Up, _testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }


                            //PHYSICS LOWER ARM LEFT
                            _player_lft_lower_arm[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_lowerarm[indexer00];
                            _player_lft_lower_arm[indexer00]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_lft_lower_arm[indexer00];
                            voxel_instancers = voxel_cuber.instances;
                            voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                            for (int i = 0; i < voxel_instancers.Length; i++)
                            {
                                float xxx = voxel_sometester[i].M41;
                                float yyy = voxel_sometester[i].M42;
                                float zzz = voxel_sometester[i].M43;

                                voxel_cuber.instances[i].position.X = xxx;
                                voxel_cuber.instances[i].position.Y = yyy;
                                voxel_cuber.instances[i].position.Z = zzz;
                                voxel_cuber.instances[i].position.W = 1;
                                Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester[i], out _testQuater);

                                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Right, _testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Up, _testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }




                            //PHYSICS LOWER ARM LEFT ELBOWTARGET
                            _player_lft_elbow_target[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_elbow_target[indexer00];
                            _player_lft_elbow_target[indexer00]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_lft_elbow_target[indexer00];
                            voxel_instancers = voxel_cuber.instances;
                            voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                            for (int i = 0; i < voxel_instancers.Length; i++)
                            {
                                float xxx = voxel_sometester[i].M41;
                                float yyy = voxel_sometester[i].M42;
                                float zzz = voxel_sometester[i].M43;

                                voxel_cuber.instances[i].position.X = xxx;
                                voxel_cuber.instances[i].position.Y = yyy;
                                voxel_cuber.instances[i].position.Z = zzz;
                                voxel_cuber.instances[i].position.W = 1;
                                Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester[i], out _testQuater);

                                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Right, _testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Up, _testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }






                            //PHYSICS LOWER ARM LEFT ELBOWTARGET TWO
                            _player_lft_elbow_target_two[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_elbow_target_two[indexer00];
                            _player_lft_elbow_target_two[indexer00]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_lft_elbow_target_two[indexer00];
                            voxel_instancers = voxel_cuber.instances;
                            voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                            for (int i = 0; i < voxel_instancers.Length; i++)
                            {
                                float xxx = voxel_sometester[i].M41;
                                float yyy = voxel_sometester[i].M42;
                                float zzz = voxel_sometester[i].M43;

                                voxel_cuber.instances[i].position.X = xxx;
                                voxel_cuber.instances[i].position.Y = yyy;
                                voxel_cuber.instances[i].position.Z = zzz;
                                voxel_cuber.instances[i].position.W = 1;
                                Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester[i], out _testQuater);

                                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Right, _testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Up, _testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }





                            //PHYSICS LOWER ARM RIGHT
                            _player_rght_lower_arm[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_lowerarm[indexer00];
                            _player_rght_lower_arm[indexer00]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_rght_lower_arm[indexer00];
                            voxel_instancers = voxel_cuber.instances;
                            voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                            for (int i = 0; i < voxel_instancers.Length; i++)
                            {
                                float xxx = voxel_sometester[i].M41;
                                float yyy = voxel_sometester[i].M42;
                                float zzz = voxel_sometester[i].M43;

                                voxel_cuber.instances[i].position.X = xxx;
                                voxel_cuber.instances[i].position.Y = yyy;
                                voxel_cuber.instances[i].position.Z = zzz;
                                voxel_cuber.instances[i].position.W = 1;
                                Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester[i], out _testQuater);

                                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Right, _testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Up, _testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }

                            //PHYSICS UPPER ARM RIGHT
                            _player_rght_upper_arm[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_upperarm[indexer00];
                            _player_rght_upper_arm[indexer00]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_rght_upper_arm[indexer00];
                            voxel_instancers = voxel_cuber.instances;
                            voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                            for (int i = 0; i < voxel_instancers.Length; i++)
                            {
                                float xxx = voxel_sometester[i].M41;
                                float yyy = voxel_sometester[i].M42;
                                float zzz = voxel_sometester[i].M43;

                                voxel_cuber.instances[i].position.X = xxx;
                                voxel_cuber.instances[i].position.Y = yyy;
                                voxel_cuber.instances[i].position.Z = zzz;
                                voxel_cuber.instances[i].position.W = 1;
                                Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester[i], out _testQuater);

                                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Right, _testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Up, _testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }






                            //PHYSICS  RIGHT ELBOWTARGET
                            _player_rght_elbow_target[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_elbow_target[indexer00];
                            _player_rght_elbow_target[indexer00]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_rght_elbow_target[indexer00];
                            voxel_instancers = voxel_cuber.instances;
                            voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                            for (int i = 0; i < voxel_instancers.Length; i++)
                            {
                                float xxx = voxel_sometester[i].M41;
                                float yyy = voxel_sometester[i].M42;
                                float zzz = voxel_sometester[i].M43;

                                voxel_cuber.instances[i].position.X = xxx;
                                voxel_cuber.instances[i].position.Y = yyy;
                                voxel_cuber.instances[i].position.Z = zzz;
                                voxel_cuber.instances[i].position.W = 1;
                                Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester[i], out _testQuater);

                                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Right, _testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Up, _testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }






                            //PHYSICS RIGHT ELBOWTARGET TWO
                            _player_rght_elbow_target_two[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_elbow_target_two[indexer00];
                            _player_rght_elbow_target_two[indexer00]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_rght_elbow_target_two[indexer00];
                            voxel_instancers = voxel_cuber.instances;
                            voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                            for (int i = 0; i < voxel_instancers.Length; i++)
                            {
                                float xxx = voxel_sometester[i].M41;
                                float yyy = voxel_sometester[i].M42;
                                float zzz = voxel_sometester[i].M43;

                                voxel_cuber.instances[i].position.X = xxx;
                                voxel_cuber.instances[i].position.Y = yyy;
                                voxel_cuber.instances[i].position.Z = zzz;
                                voxel_cuber.instances[i].position.W = 1;
                                Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester[i], out _testQuater);

                                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Right, _testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Up, _testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }



                            //PHYSICS RIGHT SHOULDER
                            _player_rght_shldr[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_shoulder[indexer00];
                            _player_rght_shldr[indexer00]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_rght_shldr[indexer00];
                            voxel_instancers = voxel_cuber.instances;
                            voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                            for (int i = 0; i < voxel_instancers.Length; i++)
                            {
                                float xxx = voxel_sometester[i].M41;
                                float yyy = voxel_sometester[i].M42;
                                float zzz = voxel_sometester[i].M43;

                                voxel_cuber.instances[i].position.X = xxx;
                                voxel_cuber.instances[i].position.Y = yyy;
                                voxel_cuber.instances[i].position.Z = zzz;
                                voxel_cuber.instances[i].position.W = 1;
                                Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester[i], out _testQuater);

                                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Right, _testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Up, _testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }



























                            //PHYSICS PELVIS
                            _player_pelvis[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_pelvis[indexer00];
                            _player_pelvis[indexer00]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_pelvis[indexer00];
                            voxel_instancers = voxel_cuber.instances;
                            voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                            for (int i = 0; i < voxel_instancers.Length; i++)
                            {
                                float xxx = voxel_sometester[i].M41;
                                float yyy = voxel_sometester[i].M42;
                                float zzz = voxel_sometester[i].M43;

                                voxel_cuber.instances[i].position.X = xxx;
                                voxel_cuber.instances[i].position.Y = yyy;
                                voxel_cuber.instances[i].position.Z = zzz;
                                voxel_cuber.instances[i].position.W = 1;
                                Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester[i], out _testQuater);

                                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Right, _testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Up, _testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }


                            //PHYSICS TORSO
                            _player_torso[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_torso[indexer00];
                            _player_torso[indexer00]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_torso[indexer00];
                            voxel_instancers = voxel_cuber.instances;
                            voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                            for (int i = 0; i < voxel_instancers.Length; i++)
                            {
                                float xxx = voxel_sometester[i].M41;
                                float yyy = voxel_sometester[i].M42;
                                float zzz = voxel_sometester[i].M43;

                                voxel_cuber.instances[i].position.X = xxx;
                                voxel_cuber.instances[i].position.Y = yyy;
                                voxel_cuber.instances[i].position.Z = zzz;
                                voxel_cuber.instances[i].position.W = 1;
                                Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester[i], out _testQuater);

                                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Right, _testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Up, _testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }




                            //PHYSICS LEFT SHOULDER
                            _player_lft_shldr[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_shoulder[indexer00];
                            _player_lft_shldr[indexer00]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_lft_shldr[indexer00];
                            voxel_instancers = voxel_cuber.instances;
                            voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                            for (int i = 0; i < voxel_instancers.Length; i++)
                            {
                                float xxx = voxel_sometester[i].M41;
                                float yyy = voxel_sometester[i].M42;
                                float zzz = voxel_sometester[i].M43;

                                voxel_cuber.instances[i].position.X = xxx;
                                voxel_cuber.instances[i].position.Y = yyy;
                                voxel_cuber.instances[i].position.Z = zzz;
                                voxel_cuber.instances[i].position.W = 1;
                                Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester[i], out _testQuater);

                                var dirInstance = _getDirection(Vector3.ForwardRH, _testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Right, _testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = _getDirection(Vector3.Up, _testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }
                        }
                    }
                }




                //SCREEN STUFF
                if (_has_locked_screen_pos == 0)
                {

                }
                else if (_has_locked_screen_pos == 1)
                {
                    _has_locked_screen_pos = 2;
                }

                /*if (_4_falling_after_grab_swtch == 1)
                {
                    if (_4_falling_after_grab_counter >= 100)
                    {
                        _4_falling_after_grab_init = 1;
                        _4_falling_after_grab_swtch = 0;
                        _4_falling_after_grab_counter = 0;
                    }
                    _4_falling_after_grab_counter++;
                }*/




















































































                if (_has_init_screen == 0)
                {
                    screen_mat = worldMatrix_instances_screens[0][0];
                    var somematroxer2 = screen_mat;

                    Quaternion _quat_screen;
                    Quaternion.RotationMatrix(ref screen_mat, out _quat_screen);
                    //_testQuater.Normalize();

                    screenNormal = _getDirection(Vector3.ForwardRH, _quat_screen);
                    screenNormal.Normalize();

                    var planor = new Plane(new Vector3(screen_mat.M41, screen_mat.M42, screen_mat.M43), screenNormal);



                    var centerPosOculusRift = new Vector3(_hmdPoser.X, _hmdPoser.Y, _hmdPoser.Z) + OFFSETPOS;



                    Matrix oculusRifter;
                    Matrix.RotationQuaternion(ref _hmdRoter, out oculusRifter);


                    oculusRifter = oculusRifter * originRot * rotatingMatrix * rotatingMatrixForPelvis;

                    Quaternion some_oculus_quat;
                    Quaternion.RotationMatrix(ref oculusRifter, out some_oculus_quat);

                    var rayDirFront = _getDirection(Vector3.ForwardRH, some_oculus_quat);
                    rayDirFront.Normalize();


                    var someRayer = new Ray(centerPosOculusRift, rayDirFront);

                    Vector3 intersectPointHMD;
                    var intersecterHMD = someRayer.Intersects(ref planor, out intersectPointHMD);

                    somematroxer2.M41 = intersectPointHMD.X;
                    somematroxer2.M42 = intersectPointHMD.Y;
                    somematroxer2.M43 = intersectPointHMD.Z;

                    //CIRCLE CIRCLE INTERSECTION //http://mathworld.wolfram.com/Circle-CircleIntersection.html
                    d = (point3DCollection[2] - point3DCollection[0]).Length();

                    widthLength = (point3DCollection[2] - point3DCollection[0]).Length();
                    heightLength = (point3DCollection[1] - point3DCollection[0]).Length();

                    r = (intersectPointHMD - point3DCollection[0]).Length();
                    R = (intersectPointHMD - point3DCollection[2]).Length();


                    x = ((d * d) - (r * r) + (R * R)) / (2 * d);
                    d1 = x;
                    d2 = d - x;

                    //r is with d2
                    //R is with d1
                    //a2 + b2 = c2

                    b = Math.Sqrt((r * r) - (d2 * d2));
                    currentPosWidth = widthLength - d1;
                    currentPosHeight = heightLength - b;

                    percentXRight = currentPosWidth / widthLength;
                    percentYRight = currentPosHeight / heightLength;
                    percentXRight *= D3D.SurfaceWidth;
                    percentYRight *= D3D.SurfaceHeight;

                    var realOculusRiftCursorPosX = percentXRight;
                    var realOculusRiftCursorPosY = percentYRight;

                    if (realOculusRiftCursorPosX >= 0 && realOculusRiftCursorPosX <= D3D.SurfaceWidth && realOculusRiftCursorPosY >= 0 && realOculusRiftCursorPosY <= D3D.SurfaceHeight)
                    {
                        _oculusR_Cursor_matrix = somematroxer2;
                        _out_of_bounds_oculus_rift = 0;
                    }
                    else
                    {
                        _out_of_bounds_oculus_rift = 1;
                    }

                    //if (_out_of_bounds_oculus_rift == 1)
                    {
                        if (thumbStickRight[1].X < 0 || thumbStickRight[1].X > 0 || thumbStickRight[1].Y < 0 || thumbStickRight[1].Y > 0)
                        {
                            if (thumbStickRight[1].X < 0 && thumbStickRight[1].Y < 0 || thumbStickRight[1].X < 0 && thumbStickRight[1].Y > 0)
                            {
                                thumbstickIsRight = thumbStickRight[1].X;
                                thumbstickIsUp = thumbStickRight[1].Y;
                                //newRotationY;

                                float rotMax = 25;

                                float rot0 = (float)((180 / Math.PI) * (Math.Atan(thumbstickIsUp / thumbstickIsRight))); // opp/adj
                                float rot1 = (float)((180 / Math.PI) * (Math.Atan(thumbstickIsRight / thumbstickIsUp)));

                                float newRotY = thumbstickIsRight * (rotMax) * -1;

                                RotationY = newRotY;
                                float someRotForPelvis = (float)RotationY;

                                if (RotationY > rotMax * 0.99f)
                                {
                                    RotationY = rotMax * 0.99f;
                                    RotationY4Pelvis += speedRot * 10;
                                    RotationGrabbedY += speedRot * 10;
                                }




                                rotMax = 25;
                                float newRotX = thumbstickIsUp * (rotMax) * -1;
                                RotationX = newRotX;

                                if (RotationX > rotMax * 0.99f)
                                {
                                    RotationX = rotMax * 0.99f;
                                }



                                //float pitch = (float)(RotationX * 0.0174532925f);
                                //float yaw = (float)(RotationY * 0.0174532925f);
                                //float roll = (float)(RotationZ * 0.0174532925f);


                                float pitch = (float)(Math.PI * (RotationX) / 180.0f);
                                float yaw = (float)(Math.PI * (RotationY) / 180.0f);
                                float roll = (float)(Math.PI * (RotationZ) / 180.0f);

                                rotatingMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                                //pitch = (float)(RotationX4Pelvis * 0.0174532925f);
                                //yaw = (float)(RotationY4Pelvis * 0.0174532925f);
                                //roll = (float)(RotationZ4Pelvis * 0.0174532925f);


                                pitch = (float)(Math.PI * (RotationX4Pelvis) / 180.0f);
                                yaw = (float)(Math.PI * (RotationY4Pelvis) / 180.0f);
                                roll = (float)(Math.PI * (RotationZ4Pelvis) / 180.0f);


                                rotatingMatrixForPelvis = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                                if (_has_grabbed_right_swtch == 2)
                                {
                                    _swtch_hasRotated = 1;
                                }

                                pitch = (float)(Math.PI * (RotationGrabbedX) / 180.0f);
                                yaw = (float)(Math.PI * (RotationGrabbedY) / 180.0f);
                                roll = (float)(Math.PI * (RotationGrabbedZ) / 180.0f);


                                rotatingMatrixForGrabber = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);
                            }
                            if (thumbStickRight[1].X > 0 && thumbStickRight[1].Y < 0 || thumbStickRight[1].X > 0 && thumbStickRight[1].Y > 0)
                            {
                                thumbstickIsRight = thumbStickRight[1].X;
                                thumbstickIsUp = thumbStickRight[1].Y;

                                float rotMax = 25;

                                //for calculations
                                float rot0 = (float)((180 / Math.PI) * (Math.Atan(thumbstickIsUp / thumbstickIsRight)));
                                float rot1 = (float)((180 / Math.PI) * (Math.Atan(thumbstickIsRight / thumbstickIsUp)));

                                if (rot0 > 0)
                                {
                                    rot0 *= -1;
                                }

                                float newRotY = thumbstickIsRight * (-rotMax);

                                RotationY = newRotY;
                                float someRotForPelvis = (float)RotationY;

                                if (RotationY < -rotMax * 0.99f)
                                {
                                    RotationY = -rotMax * 0.99f;
                                    RotationY4Pelvis -= speedRot * 10;
                                    RotationGrabbedY -= speedRot * 10;
                                }

                                rotMax = 25;
                                float newRotX = thumbstickIsUp * (rotMax) * -1;
                                RotationX = newRotX;

                                if (RotationX > rotMax * 0.99f)
                                {
                                    RotationX = rotMax * 0.99f;
                                }

                                float pitch = (float)(Math.PI * (RotationX) / 180.0f);
                                float yaw = (float)(Math.PI * (RotationY) / 180.0f);
                                float roll = (float)(Math.PI * (RotationZ) / 180.0f);

                                rotatingMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                                pitch = (float)(Math.PI * (RotationX4Pelvis) / 180.0f);
                                yaw = (float)(Math.PI * (RotationY4Pelvis) / 180.0f);
                                roll = (float)(Math.PI * (RotationZ4Pelvis) / 180.0f);

                                rotatingMatrixForPelvis = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);


                                pitch = (float)(Math.PI * (RotationGrabbedX) / 180.0f);
                                yaw = (float)(Math.PI * (RotationGrabbedY) / 180.0f);
                                roll = (float)(Math.PI * (RotationGrabbedZ) / 180.0f);

                                rotatingMatrixForGrabber = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);
                                if (_has_grabbed_right_swtch == 2)
                                {
                                    _swtch_hasRotated = 1;
                                }

                            }
                        }
                        else
                        {
                            if (thumbStickRight[1].X == 0 && thumbStickRight[1].X == 0 && thumbStickRight[1].Y == 0 && thumbStickRight[1].Y == 0)
                            {
                                if (_swtch_hasRotated == 1)
                                {

                                    _swtch_hasRotated = 2;
                                }
                                //_swtch_hasRotated = 0;

                                RotationX = 0;
                                RotationY = 0;
                                RotationZ = 0;

                                float pitch = (float)(RotationX * 0.0174532925f);
                                float yaw = (float)(RotationY * 0.0174532925f);
                                float roll = (float)(RotationZ * 0.0174532925f);

                                rotatingMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                                pitch = (float)(RotationX4Pelvis * 0.0174532925f);
                                yaw = (float)(RotationY4Pelvis * 0.0174532925f);
                                roll = (float)(RotationZ4Pelvis * 0.0174532925f);

                                rotatingMatrixForPelvis = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                                pitch = (float)(RotationGrabbedX * 0.0174532925f);
                                yaw = (float)(RotationGrabbedY * 0.0174532925f);
                                roll = (float)(RotationGrabbedZ * 0.0174532925f);

                                rotatingMatrixForGrabber = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);
                                if (_swtch_hasRotated == 0)
                                {
                                    _sec_logic_swtch_grab = 0;
                                }
                            }
                            else
                            {

                            }
                        }
                    }
                    for (int i = 0; i < _screenDirMatrix_correct_pos.Length; i++)
                    {
                        point3DCollection[i].X = _screenDirMatrix_correct_pos[i].M41;
                        point3DCollection[i].Y = _screenDirMatrix_correct_pos[i].M42;
                        point3DCollection[i].Z = _screenDirMatrix_correct_pos[i].M43;
                    }
                    //END OF


                    //RIGHT CONTROLLER HITPOINT
                    if (currentFrameRight < arrayOfStabilizerPosRight.Length)
                    {
                        arrayOfStabilizerPosXRight[currentFrameRight] = intersectPointRight.X;
                        arrayOfStabilizerPosYRight[currentFrameRight] = intersectPointRight.Y;
                        arrayOfStabilizerPosZRight[currentFrameRight] = intersectPointRight.Z;

                        arrayOfStabilizerPosRight[currentFrameRight] = intersectPointRight;
                    }
                    else
                    {
                        differenceX = 0;
                        differenceY = 0;
                        differenceZ = 0;

                        var j = 1;

                        for (int i = 0; i < arrayOfStabilizerPosXRight.Length - 1; i++, j++)
                        {
                            currentX = arrayOfStabilizerPosXRight[j];
                            currentY = arrayOfStabilizerPosYRight[j];
                            currentZ = arrayOfStabilizerPosZRight[j];

                            lastRightHitPointXFrameOne = arrayOfStabilizerPosXRight[i];
                            lastRightHitPointYFrameOne = arrayOfStabilizerPosYRight[i];
                            lastRightHitPointZFrameOne = arrayOfStabilizerPosZRight[i];

                            if (lastRightHitPointXFrameOne >= currentX)
                            {
                                differenceX = lastRightHitPointXFrameOne - currentX;
                            }
                            else
                            {
                                differenceX = currentX - lastRightHitPointXFrameOne;
                            }
                            arrayOfStabilizerPosDifferenceXRight[i] = differenceX;


                            if (lastRightHitPointYFrameOne >= currentY)
                            {
                                differenceY = lastRightHitPointYFrameOne - currentY;
                            }
                            else
                            {
                                differenceY = currentY - lastRightHitPointYFrameOne;
                            }
                            arrayOfStabilizerPosDifferenceYRight[i] = differenceY;


                            if (lastRightHitPointZFrameOne >= currentZ)
                            {
                                differenceZ = lastRightHitPointZFrameOne - currentZ;
                            }
                            else
                            {
                                differenceZ = currentZ - lastRightHitPointZFrameOne;
                            }
                            arrayOfStabilizerPosDifferenceZRight[i] = differenceZ;
                        }

                        averageXRight = 0;
                        averageYRight = 0;
                        averageZRight = 0;

                        for (int i = 0; i < arrayOfStabilizerPosDifferenceXRight.Length; i++)
                        {
                            averageXRight += arrayOfStabilizerPosDifferenceXRight[i];
                            averageYRight += arrayOfStabilizerPosDifferenceYRight[i];
                            averageZRight += arrayOfStabilizerPosDifferenceZRight[i];
                        }

                        averageXRight = averageXRight / arrayOfStabilizerPosDifferenceXRight.Length;
                        averageYRight = averageYRight / arrayOfStabilizerPosDifferenceYRight.Length;
                        averageZRight = averageZRight / arrayOfStabilizerPosDifferenceZRight.Length;




                        restartFrameCounterRight = true;
                    }

                    if (!restartFrameCounterRight)
                    {
                        currentFrameRight++;
                    }
                    else
                    {
                        currentFrameRight = 0;
                        restartFrameCounterRight = false;
                    }

                    positionXRight = 0;
                    positionYRight = 0;
                    positionZRight = 0;

                    for (int i = 0; i < arrayOfStabilizerPosRight.Length; i++)
                    {
                        positionXRight += arrayOfStabilizerPosRight[i].X;
                        positionYRight += arrayOfStabilizerPosRight[i].Y;
                        positionZRight += arrayOfStabilizerPosRight[i].Z;
                    }

                    positionXRight = positionXRight / arrayOfStabilizerPosRight.Length;
                    positionYRight = positionYRight / arrayOfStabilizerPosRight.Length;
                    positionZRight = positionZRight / arrayOfStabilizerPosRight.Length;


                    stabilizedIntersectionPosRight = new Vector3((float)positionXRight, (float)positionYRight, (float)positionZRight);
                    _intersectTouchRightMatrix.M41 = stabilizedIntersectionPosRight.X;
                    _intersectTouchRightMatrix.M42 = stabilizedIntersectionPosRight.Y;
                    _intersectTouchRightMatrix.M43 = stabilizedIntersectionPosRight.Z;

                    //CIRCLE CIRCLE INTERSECTION //http://mathworld.wolfram.com/Circle-CircleIntersection.html
                    d = (point3DCollection[2] - point3DCollection[0]).Length();

                    widthLength = (point3DCollection[2] - point3DCollection[0]).Length();
                    heightLength = (point3DCollection[1] - point3DCollection[0]).Length();

                    r = (stabilizedIntersectionPosRight - point3DCollection[0]).Length();
                    R = (stabilizedIntersectionPosRight - point3DCollection[2]).Length();


                    x = ((d * d) - (r * r) + (R * R)) / (2 * d);
                    d1 = x;
                    d2 = d - x;

                    //r is with d2
                    //R is with d1
                    //a2 + b2 = c2

                    b = Math.Sqrt((r * r) - (d2 * d2));
                    currentPosWidth = widthLength - d1;
                    currentPosHeight = heightLength - b;

                    percentXRight = currentPosWidth / widthLength;
                    percentYRight = currentPosHeight / heightLength;
                    percentXRight *= D3D.SurfaceWidth;
                    percentYRight *= D3D.SurfaceHeight;

                    //_MicrosoftWindowsMouseRight(percentXRight, percentYRight, thumbStickRight);


                    //LEFT CONTROLLER HITPOINT
                    //////////////////////////////////////////////////////////////////////////

                    if (currentFrameLeft < arrayOfStabilizerPosLeft.Length)
                    {
                        arrayOfStabilizerPosXLeft[currentFrameLeft] = intersectPointLeft.X;
                        arrayOfStabilizerPosYLeft[currentFrameLeft] = intersectPointLeft.Y;
                        arrayOfStabilizerPosZLeft[currentFrameLeft] = intersectPointLeft.Z;
                        arrayOfStabilizerPosLeft[currentFrameLeft] = intersectPointLeft;
                    }
                    else
                    {
                        differenceX = 0;
                        differenceY = 0;
                        differenceZ = 0;

                        var j = 1;
                        for (int i = 0; i < arrayOfStabilizerPosXLeft.Length - 1; i++, j++)
                        {
                            currentX = arrayOfStabilizerPosXLeft[j];
                            currentY = arrayOfStabilizerPosYLeft[j];
                            currentZ = arrayOfStabilizerPosZLeft[j];

                            lastLeftHitPointXFrameOne = arrayOfStabilizerPosXLeft[i];
                            lastLeftHitPointYFrameOne = arrayOfStabilizerPosYLeft[i];
                            lastLeftHitPointZFrameOne = arrayOfStabilizerPosZLeft[i];

                            if (lastLeftHitPointXFrameOne >= currentX)
                            {
                                differenceX = lastLeftHitPointXFrameOne - currentX;
                            }
                            else
                            {
                                differenceX = currentX - lastLeftHitPointXFrameOne;
                            }
                            arrayOfStabilizerPosDifferenceXLeft[i] = differenceX;

                            if (lastLeftHitPointYFrameOne >= currentY)
                            {
                                differenceY = lastLeftHitPointYFrameOne - currentY;
                            }
                            else
                            {
                                differenceY = currentY - lastLeftHitPointYFrameOne;
                            }
                            arrayOfStabilizerPosDifferenceYLeft[i] = differenceY;


                            if (lastLeftHitPointZFrameOne >= currentZ)
                            {
                                differenceZ = lastLeftHitPointZFrameOne - currentZ;
                            }
                            else
                            {
                                differenceZ = currentZ - lastLeftHitPointZFrameOne;
                            }
                            arrayOfStabilizerPosDifferenceZLeft[i] = differenceZ;
                        }

                        averageXLeft = 0;
                        averageYLeft = 0;
                        averageZLeft = 0;


                        for (int i = 0; i < arrayOfStabilizerPosDifferenceXLeft.Length; i++)
                        {
                            averageXLeft += arrayOfStabilizerPosDifferenceXLeft[i];
                            averageYLeft += arrayOfStabilizerPosDifferenceYLeft[i];
                            averageZLeft += arrayOfStabilizerPosDifferenceZLeft[i];
                        }

                        averageXLeft = averageXLeft / arrayOfStabilizerPosDifferenceXLeft.Length;
                        averageYLeft = averageYLeft / arrayOfStabilizerPosDifferenceYLeft.Length;
                        averageZLeft = averageZLeft / arrayOfStabilizerPosDifferenceZLeft.Length;

                        restartFrameCounterLeft = true;
                    }

                    if (!restartFrameCounterLeft)
                    {
                        currentFrameLeft++;
                    }
                    else
                    {
                        currentFrameLeft = 0;
                        restartFrameCounterLeft = false;
                    }

                    positionXLeft = 0;
                    positionYLeft = 0;
                    positionZLeft = 0;

                    for (int i = 0; i < arrayOfStabilizerPosLeft.Length; i++)
                    {
                        positionXLeft += arrayOfStabilizerPosLeft[i].X;
                        positionYLeft += arrayOfStabilizerPosLeft[i].Y;
                        positionZLeft += arrayOfStabilizerPosLeft[i].Z;
                    }

                    positionXLeft = positionXLeft / arrayOfStabilizerPosLeft.Length;
                    positionYLeft = positionYLeft / arrayOfStabilizerPosLeft.Length;
                    positionZLeft = positionZLeft / arrayOfStabilizerPosLeft.Length;

                    stabilizedIntersectionPosLeft = new Vector3((float)positionXLeft, (float)positionYLeft, (float)positionZLeft);
                    _intersectTouchLeftMatrix.M41 = stabilizedIntersectionPosLeft.X;
                    _intersectTouchLeftMatrix.M42 = stabilizedIntersectionPosLeft.Y;
                    _intersectTouchLeftMatrix.M43 = stabilizedIntersectionPosLeft.Z;

                    //intersectPointRight.Z;


                    //CIRCLE CIRCLE INTERSECTION //http://mathworld.wolfram.com/Circle-CircleIntersection.html
                    //d = (point3DCollection[2] - point3DCollection[0]).Length();
                    //widthLength = (point3DCollection[2] - point3DCollection[0]).Length();
                    //heightLength = (point3DCollection[1] - point3DCollection[0]).Length();
                    r = (stabilizedIntersectionPosLeft - point3DCollection[0]).Length();
                    R = (stabilizedIntersectionPosLeft - point3DCollection[2]).Length();

                    x = ((d * d) - (r * r) + (R * R)) / (2 * d);
                    d1 = x;
                    d2 = d - x;

                    //r is with d2
                    //R is with d1
                    //a2 + b2 = c2

                    b = Math.Sqrt((r * r) - (d2 * d2));
                    currentPosWidth = widthLength - d1;
                    currentPosHeight = heightLength - b;

                    percentXLeft = currentPosWidth / widthLength;
                    percentYLeft = currentPosHeight / heightLength;
                    percentXLeft *= D3D.SurfaceWidth;
                    percentYLeft *= D3D.SurfaceHeight;

                    //_MicrosoftWindowsMouseLeft(percentXRight, percentYRight, thumbStickRight);

                    double _final_percentXRight = percentXRight;
                    double _final_percentYRight = percentYRight;



                    double realMousePosX = 0;
                    double realMousePosY = 0;

                    //d = (point3DCollection[2] - point3DCollection[0]).Length();
                    //widthLength = (point3DCollection[2] - point3DCollection[0]).Length();
                    //heightLength = (point3DCollection[1] - point3DCollection[0]).Length();

                    r = (intersectPointRight - point3DCollection[0]).Length();
                    R = (intersectPointRight - point3DCollection[2]).Length();


                    x = ((d * d) - (r * r) + (R * R)) / (2 * d);
                    d1 = x;
                    d2 = d - x;

                    //r is with d2
                    //R is with d1
                    //a2 + b2 = c2

                    b = Math.Sqrt((r * r) - (d2 * d2));
                    currentPosWidth = widthLength - d1;
                    currentPosHeight = heightLength - b;

                    percentXRight = currentPosWidth / widthLength;
                    percentYRight = currentPosHeight / heightLength;

                    percentXRight *= D3D.SurfaceWidth;
                    percentYRight *= D3D.SurfaceHeight;



                    if (_has_locked_screen_pos != 0)
                    {
                        realMousePosX = percentXRight;
                        realMousePosY = percentYRight;
                    }
                    else
                    {
                        realMousePosX = D3D.SurfaceWidth - percentXRight;

                    }

                    realMousePosX = percentXRight;
                    realMousePosY = D3D.SurfaceHeight - percentYRight;

                    _MicrosoftWindowsMouseRight(_final_percentXRight, _final_percentYRight, thumbStickRight, percentXLeft, percentYLeft, thumbStickLeft, realMousePosX, realMousePosY); //, realOculusRiftCursorPosX, realOculusRiftCursorPosY

                    _oculus_touch_controls(_final_percentXRight, _final_percentYRight, thumbStickRight, percentXLeft, percentYLeft, thumbStickLeft, realMousePosX, realMousePosY);

                    lastHasUsedHandTriggerLeft = hasUsedHandTriggerLeft;
                    lastbuttonPressedOculusTouchRight = buttonPressedOculusTouchRight;
                    lastbuttonPressedOculusTouchLeft = buttonPressedOculusTouchLeft;

                    //var absoluteMoveX = Convert.ToUInt32((percentXRight * 65535) / D3D.SurfaceWidth);
                    //var absoluteMoveY = Convert.ToUInt32((percentYRight * 65535) / D3D.SurfaceHeight);

                    _mouseCursorMatrix.M41 = (float)((percentXRight * 65535) / D3D.SurfaceWidth);
                    _mouseCursorMatrix.M42 = (float)((percentYRight * 65535) / D3D.SurfaceHeight);








                    //DISCARDED TO REINSERT
                    //DISCARDED TO REINSERT
                    //DISCARDED TO REINSERT
                    /*JMatrix _mat = body.Orientation;// _SC_visual_object_manager._humRig._player_rght_hnd[0]._singleObjectOnly._LASTPOSITION;
                    JQuaternion quat = JQuaternion.CreateFromMatrix(_mat);
                    Quaternion _quaternion_body_matrix = new Quaternion(quat.X, quat.Y, quat.Z, quat.W);
                    Matrix finalMat;
                    Matrix.RotationQuaternion(ref _quaternion_body_matrix , out finalMat);
                    _player_rght_hnd[0]._singleObjectOnly._LASTPOSITION = finalMat;
                    */
                    //DISCARDED TO REINSERT
                    //DISCARDED TO REINSERT
                    //DISCARDED TO REINSERT*/
                }


                //DISCARDED TO REINSERT
                //DISCARDED TO REINSERT
                //DISCARDED TO REINSERT

                final_hand_pos_right_locked = _player_rght_hnd[0]._arrayOfInstances[0].current_pos;
                final_hand_pos_left_locked = _player_lft_hnd[0]._arrayOfInstances[0].current_pos;

                /*_last_final_hand_pos_right = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);
                _last_frame_handPos = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);

                if (_grab_rigid_data._body != null)
                {
                    _last_frame_rigid_grab_pos = new Vector3(_grab_rigid_data._body.Position.X, _grab_rigid_data._body.Position.Y, _grab_rigid_data._body.Position.Z);
                    _last_frame_rigid_grab_rot = _grab_rigid_data._body.Orientation;//new JQuaternion();// new Vector3(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z);
                }*/
                //DISCARDED TO REINSERT
                //DISCARDED TO REINSERT
                //DISCARDED TO REINSERT



                /*for (int x = 0; x < _physics_engine_instance_x; x++)
                {
                    for (int y = 0; y < _physics_engine_instance_y; y++)
                    {
                        for (int z = 0; z < _physics_engine_instance_z; z++)
                        {
                            
                        }
                    }
                }*/

                /*var _index = 0;// x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z);

                for (int i = 0; i < _objects_static_00[_index].Length; i++)
                {
                    if (_objects_static_00[_index][i] == 2)
                    {
                        if (_objects_static_counter_00[_index][i] >= 20)
                        {
                            if (_objects_rigid_static_00[_index][i] != null)
                            {
                                if(_grab_rigid_data._body != null && _grab_rigid_data._body == _objects_rigid_static_00[_index][i])
                                {
                                    _objects_rigid_static_00[_index][i].AngularVelocity = JVector.Zero;
                                    _objects_rigid_static_00[_index][i].LinearVelocity = JVector.Zero;
                                    _objects_rigid_static_00[_index][i].IsStatic = true;
                                    //_objects_rigid_static_00[_indexer][i].AllowDeactivation = false;
                                }

                                _objects_static_00[_index][i] = 0;
                                _objects_static_counter_00[_index][i] = 0;
                            }
                        }
                        _objects_static_counter_00[_index][i]++;
                    }
                }*/


                LastRotationScreenX = RotationScreenX;
                LastRotationScreenY = RotationScreenY;
                LastRotationScreenZ = RotationScreenZ;


                D3D.result = D3D.OVR.SubmitFrame(D3D.sessionPtr, 0L, IntPtr.Zero, ref D3D.layerEyeFov);
                D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to submit the frame of the current layers.");
                D3D.DeviceContext.CopyResource(D3D.mirrorTextureD3D, D3D.BackBuffer);
                D3D.SwapChain.Present(0, PresentFlags.None); //crap visuals returning to only spheroids.
                _can_work_physics = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return _main_received_object;
        }



        private void _MicrosoftWindowsMouseRight(double percentXRight, double percentYRight, Vector2f[] thumbStickRight, double percentXLeft, double percentYLeft, Vector2f[] thumbStickLeft, double realMousePosX, double realMousePosY) //, double realOculusRiftCursorPosX, double realOculusRiftCursorPosY
        {

            //MessageBox((IntPtr)0, "percentXRight: " + percentXRight + " percentYRight: " + percentYRight, "mouse move", 0);
            //Console.WriteLine("percentXRight: " + percentXRight + " percentYRight: " + percentYRight);

            if (_indexMouseMove == 0)
            {
                /////////////RIGHT OCULUS TOUCH/////////////////////////////////////////////////////////////////////////////////////
                if (percentXRight >= 0 && percentXRight <= D3D.SurfaceWidth && percentYRight >= 0 && percentYRight <= D3D.SurfaceHeight &&
                    realMousePosX >= 0 && realMousePosX <= D3D.SurfaceWidth && realMousePosY >= 0 && realMousePosY <= D3D.SurfaceHeight)
                {
                    //MessageBox((IntPtr)0, "test", "mouse move", 0);

                    //var absoluteMoveX = Convert.ToUInt32((percentXRight * 65535) / D3D.SurfaceWidth);
                    //var absoluteMoveY = Convert.ToUInt32((percentYRight * 65535) / D3D.SurfaceHeight);

                    var yo = _updateFunctionStopwatchRight.Elapsed.Milliseconds;

                    if (_hasLockedMouse == 0)
                    {
                        if (yo >= 5)
                        {
                            var absoluteMoveX = Convert.ToUInt32((realMousePosX * 65535) / D3D.SurfaceWidth);
                            var absoluteMoveY = Convert.ToUInt32((realMousePosY * 65535) / D3D.SurfaceHeight);

                            mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, absoluteMoveX, absoluteMoveY, 0, 0);
                            _updateFunctionStopwatchRight.Stop();
                            _updateFunctionBoolRight = true;
                        }
                    }

                    //Console.WriteLine(percentXRight + "_" + percentYRight);
                    //MessageBox((IntPtr)0,  "test", "mouse move", 0);
                    //MOUSE DOUBLE CLICK LOGIC. IF the PLAYER clicked at one location then it stores the location, and if the player re-clicks inside of 20 frames, then click at the first click location.
                    //It's very basic, and at least I should implement also a certain "radius" of distance from the first click and the second click... If the second click is too far from the first click,
                    //then disregard the first click location.

                    if (buttonPressedOculusTouchRight != 0)
                    {
                        if (buttonPressedOculusTouchRight == 1)
                        {
                            if (hasClickedBUTTONA == 0)
                            {
                                var absoluteMoveX = Convert.ToUInt32((realMousePosX * 65535) / D3D.SurfaceWidth);
                                var absoluteMoveY = Convert.ToUInt32((realMousePosY * 65535) / D3D.SurfaceHeight);

                                if (_frameCounterTouchRight <= 20 && _canResetCounterTouchRightButtonA == true)
                                {
                                    mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, _lastMousePosXRight, _lastMousePosYRight, 0, 0);
                                    _frameCounterTouchRight = 0;
                                }

                                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);

                                _lastMousePosXRight = absoluteMoveX;
                                _lastMousePosYRight = absoluteMoveY;

                                _canResetCounterTouchRightButtonA = true;
                                hasClickedBUTTONA = 1;
                            }
                        }
                        else if (buttonPressedOculusTouchRight == 2)
                        {
                            if (hasClickedBUTTONB == false)
                            {
                                var absoluteMoveX = Convert.ToUInt32((realMousePosX * 65535) / D3D.SurfaceWidth);
                                var absoluteMoveY = Convert.ToUInt32((realMousePosY * 65535) / D3D.SurfaceHeight);

                                mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                hasClickedBUTTONB = true;
                            }
                        }
                    }
                    _out_of_bounds_right = 0;
                }
                else
                {
                    _out_of_bounds_right = 1;
                }


                //////////OCULUS TOUCH BUTTONS PRESSED////////////////////////////////////////
                if (hasClickedBUTTONA == 1 && buttonPressedOculusTouchRight == 0 || hasClickedBUTTONB && buttonPressedOculusTouchRight == 0)
                {
                    if (hasClickedBUTTONA == 1 && buttonPressedOculusTouchRight == 0)
                    {
                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                        hasClickedBUTTONA = 0;
                    }
                    else if (hasClickedBUTTONB && buttonPressedOculusTouchRight == 0)
                    {
                        mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                        hasClickedBUTTONB = false;
                    }
                }

                if (_canResetCounterTouchRightButtonA)
                {
                    _frameCounterTouchRight++;
                }

                if (_frameCounterTouchRight >= 30)
                {
                    _frameCounterTouchRight = 0;
                    _canResetCounterTouchRightButtonA = false;
                }

                if (buttonPressedOculusTouchLeft != 0)
                {
                    var yo = _updateFunctionStopwatchLeftThumbstick.Elapsed.Milliseconds;

                    if (yo >= 200)
                    {
                        if (buttonPressedOculusTouchLeft == 1024 && hasClickedBUTTONX == 0)
                        {

                            /*//https://stackoverflow.com/questions/2929255/unable-to-launch-onscreen-keyboard-osk-exe-from-a-32-bit-process-on-win7-x64
                            Process[] p = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(OnScreenKeyboardExe));
                            //StartOsk();
                            if (p.Length == 0)
                            {
                                // we must start osk from an MTA thread
                                if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
                                {
                                    ThreadStart start = new ThreadStart(StartOsk);
                                    Thread thread = new Thread(start);
                                    thread.SetApartmentState(ApartmentState.MTA);
                                    thread.Start();
                                    thread.Join();
                                }
                                else
                                {
                                    StartOsk();
                                }
                            }
                            else
                            {
                                // there might be a race condition if the process terminated 
                                // meanwhile -> proper exception handling should be added
                                //
                                SendMessage(p[0].MainWindowHandle, WM_SYSCOMMAND, new IntPtr(SC_RESTORE), new IntPtr(0));
                            }
                            */


                            string windowsKeyboard = "osk";

                            foreach (Process clsProcess in Process.GetProcesses())
                            {
                                if (clsProcess.ProcessName.ToLower().Contains(windowsKeyboard.ToLower()))
                                {
                                    break;
                                }
                                else
                                {
                                    // do your stuff

                                    //bool sucessfullyDisabledWow64Redirect = false;

                                    //string path = Path.Combine(dir, "osk.exe");



                                    /*Process proc = new Process();
                                    proc.StartInfo.FileName = windowsKeyboard + ".exe";
                                    proc.Start();*/

                                    ProcessStartInfo psi = new ProcessStartInfo();
                                    //psi.FileName = OnScreenKeyboardExe;// path64;
                                    string windir = Environment.GetEnvironmentVariable("windir");
                                    psi.WorkingDirectory = @"c:\WINDOWS\system32\";
                                    psi.FileName = windir + @"\System32\cmd.exe";
                                    psi.Arguments = "/C " + windir + @"\System32\osk.exe";


                                    psi.UseShellExecute = false;
                                    psi.RedirectStandardOutput = true;
                                    psi.RedirectStandardError = true;
                                    psi.CreateNoWindow = false;
                                    psi.Verb = "runas";

                                    //Process p = System.Diagnostics.Process.Start(path);
                                    Process proc = new Process();
                                    proc.StartInfo = psi;
                                    proc.Start();

                                    bool sucessfullyDisabledWow64Redirect = false;
                                    if (System.Environment.Is64BitOperatingSystem)
                                    {
                                        IntPtr ptr = new IntPtr();
                                        sucessfullyDisabledWow64Redirect = Wow64DisableWow64FsRedirection(ref ptr);
                                        path = string.Empty;
                                    }



                                    //Process proc = new Process();
                                    //proc.StartInfo = psi;
                                    //proc.Start();





                                    break;
                                }
                            }







                            hasClickedBUTTONX = 1;
                            _updateFunctionBoolLeftThumbStick = true;
                        }
                    }
                }
                else if (buttonPressedOculusTouchLeft == 0 && hasClickedHomeButtonTouchLeft || buttonPressedOculusTouchLeft == 0 && hasClickedBUTTONX == 1)
                {
                    if (buttonPressedOculusTouchLeft == 0 && hasClickedHomeButtonTouchLeft)
                    {
                        hasClickedHomeButtonTouchLeft = false;
                    }
                    else if (buttonPressedOculusTouchLeft == 0 && hasClickedBUTTONX == 1)
                    {
                        hasClickedBUTTONX = 0;
                    }
                }

                if (_out_of_bounds_oculus_rift == 0)
                {
                    long test = 80;
                    /////////RIGHT THUMBSTICK///////////
                    var yo6 = _updateFunctionStopwatchRightThumbstickGoLeft.Elapsed.Milliseconds;
                    if (yo6 >= 75)
                    {
                        if (thumbStickRight[1].Y <= -0.1f && hasUsedThumbStickRightE == false)
                        {
                            //Console.WriteLine("test");
                            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -test, 0);
                            hasUsedThumbStickRightE = true;
                        }
                        else if (hasUsedThumbStickRightE)
                        {
                            hasUsedThumbStickRightE = false;
                        }
                        _updateFunctionStopwatchRightThumbstickGoLeft.Stop();
                        _updateFunctionBoolRightThumbStickGoLeft = true;
                    }
                    ///////////////////////////////////////////////////////////////////////////

                    /////////RIGHT THUMBSTICK/////////////////////////////////////////////////////
                    var yo7 = _updateFunctionStopwatchRightThumbstickGoRight.Elapsed.Milliseconds;
                    if (yo7 >= 75)
                    {
                        if (thumbStickRight[1].Y >= 0.1f && hasUsedThumbStickRightQ == false)
                        {
                            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, test, 0);
                            //hasUsedThumbStickRightQ = true;
                        }
                        else if (hasUsedThumbStickRightQ)
                        {
                            hasUsedThumbStickRightQ = false;
                        }
                        _updateFunctionStopwatchRightThumbstickGoRight.Stop();
                        _updateFunctionBoolRightThumbStickGoRight = true;
                    }
                }


















                /////////////LEFT OCULUS TOUCH/////////////////////////////////////////////////////////////////////////////////////
                if (percentXLeft >= 0 && percentXLeft <= 1920 && percentYLeft >= 0 && percentYLeft <= 1080)
                {
                    var absoluteMoveX = Convert.ToUInt32((percentXLeft * 65535) / 1920);
                    var absoluteMoveY = Convert.ToUInt32((percentYLeft * 65535) / 1080);

                    /*var yo = _updateFunctionStopwatchLeft.Elapsed.Milliseconds;

                    if (yo >= 10)
                    {
                    //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, absoluteMoveX, absoluteMoveY, 0, 0);
                    _updateFunctionStopwatchLeft.Stop();
                    _updateFunctionBoolLeft = true;
                    }*/

                    //MOUSE DOUBLE CLICK LOGIC. IF the PLAYER clicked at one location then it stores the location, and if the player re-clicks inside of 20 frames, then click at the first click location.
                    //It's very basic, and at least I should implement also a certain "radius" of distance from the first click and the second click... If the second click is too far from the first click,
                    //then disregard the first click location.

                    if (buttonPressedOculusTouchLeft != 0)
                    {
                        if (_has_locked_screen_pos == 0)
                        {
                            if (buttonPressedOculusTouchLeft == 256)
                            {
                                if (hasClickedBUTTONX == 0)
                                {
                                    /*if (_out_of_bounds_right == 0)
                                    {
                                        absoluteMoveX = Convert.ToUInt32((realMousePosX * 65535) / D3D.SurfaceWidth);
                                        absoluteMoveY = Convert.ToUInt32((realMousePosY * 65535) / D3D.SurfaceHeight);
                                    }

                                    //if (_frameCounterTouchLeft <= 20 && _canResetCounterTouchLeftButtonX == true)
                                    //{
                                    //    mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, _lastMousePosXLeft, _lastMousePosYLeft, 0, 0);
                                    //    _frameCounterTouchLeft = 0;
                                    //}
                                    mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, absoluteMoveX, absoluteMoveY, 0, 0);
                                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, absoluteMoveX, absoluteMoveY, 0, 0);
                                    _lastMousePosXLeft = absoluteMoveX;
                                    _lastMousePosYLeft = absoluteMoveY;*/
                                    _canResetCounterTouchLeftButtonX = true;
                                    hasClickedBUTTONX = 1;
                                }
                            }
                            else if (buttonPressedOculusTouchLeft == 512)
                            {
                                if (hasClickedBUTTONY == 0)
                                {
                                    if (_out_of_bounds_right == 0)
                                    {
                                        absoluteMoveX = Convert.ToUInt32((realMousePosX * 65535) / D3D.SurfaceWidth);
                                        absoluteMoveY = Convert.ToUInt32((realMousePosY * 65535) / D3D.SurfaceHeight);
                                    }
                                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                    //_lastMousePosX = absoluteMoveX;
                                    //_lastMousePosY = absoluteMoveY;
                                    //_canResetCounterTouchRight = true;
                                    hasClickedBUTTONY = 1;
                                }
                            }
                        }
                    }
                    _out_of_bounds_left = 0;
                }
                else
                {
                    _out_of_bounds_left = 1;
                }


                //////////OCULUS TOUCH BUTTONS PRESSED////////////////////////////////////////
                if (hasClickedBUTTONX == 1 && buttonPressedOculusTouchLeft == 0 || hasClickedBUTTONY == 1 && buttonPressedOculusTouchLeft == 0)
                {
                    if (hasClickedBUTTONX == 1 && buttonPressedOculusTouchLeft == 0)
                    {
                        //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                        hasClickedBUTTONX = 0;
                    }
                    else if (hasClickedBUTTONY == 1 && buttonPressedOculusTouchLeft == 0)
                    {
                        mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                        hasClickedBUTTONY = 0;
                    }
                }

                if (_canResetCounterTouchLeftButtonX)
                {
                    _frameCounterTouchLeft++;
                }

                if (_frameCounterTouchLeft >= 30)
                {
                    _frameCounterTouchLeft = 0;
                    _canResetCounterTouchLeftButtonX = false;
                }


                //if (buttonPressedOculusTouchLeft == 0 && hasClickedHomeButtonTouchLeft)
                //{
                //    hasClickedHomeButtonTouchLeft = false;
                //}
            }
            else if (_indexMouseMove == 100)
            {
                /*/////////////LEFT OCULUS TOUCH/////////////////////////////////////////////////////////////////////////////////////
                if (percentXLeft >= 0 && percentXLeft <= 1920 && percentYLeft >= 0 && percentYLeft <= 1080)
                {
                    var absoluteMoveX = Convert.ToUInt32((percentXLeft * 65535) / 1920);
                    var absoluteMoveY = Convert.ToUInt32((percentYLeft * 65535) / 1080);

                    var yo = _updateFunctionStopwatchLeft.Elapsed.Milliseconds;

                    if (yo >= 10)
                    {
                        mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, absoluteMoveX, absoluteMoveY, 0, 0);
                        _updateFunctionStopwatchLeft.Stop();
                        _updateFunctionBoolLeft = true;
                    }

                    //MOUSE DOUBLE CLICK LOGIC. IF the PLAYER clicked at one location then it stores the location, and if the player re-clicks inside of 20 frames, then click at the first click location.
                    //It's very basic, and at least I should implement also a certain "radius" of distance from the first click and the second click... If the second click is too far from the first click,
                    //then disregard the first click location.

                    if (buttonPressedOculusTouchLeft != 0)
                    {
                        if (buttonPressedOculusTouchLeft == 256)
                        {
                            if (hasClickedBUTTONX == 0)
                            {
                                if (_frameCounterTouchLeft <= 20 && _canResetCounterTouchLeftButtonX == true)
                                {
                                    mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, _lastMousePosXLeft, _lastMousePosYLeft, 0, 0);
                                    _frameCounterTouchLeft = 0;
                                }

                                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);

                                _lastMousePosXLeft = absoluteMoveX;
                                _lastMousePosYLeft = absoluteMoveY;
                                _canResetCounterTouchLeftButtonX = true;
                                hasClickedBUTTONX = 1;
                            }
                        }
                        else if (buttonPressedOculusTouchLeft == 512)
                        {
                            if (hasClickedBUTTONY == 0)
                            {
                                mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                //_lastMousePosX = absoluteMoveX;
                                //_lastMousePosY = absoluteMoveY;
                                //_canResetCounterTouchRight = true;
                                hasClickedBUTTONY = 1;
                            }
                        }
                    }
                }*/

                /*//////////OCULUS TOUCH BUTTONS PRESSED////////////////////////////////////////
                if (hasClickedBUTTONX == 1 && buttonPressedOculusTouchLeft == 0 || hasClickedBUTTONY == 1 && buttonPressedOculusTouchLeft == 0)
                {
                    if (hasClickedBUTTONX == 1 && buttonPressedOculusTouchLeft == 0)
                    {
                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                        hasClickedBUTTONX = 0;
                    }
                    else if (hasClickedBUTTONY == 1 && buttonPressedOculusTouchLeft == 0)
                    {
                        mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                        hasClickedBUTTONY = 0;
                    }
                }

                if (_canResetCounterTouchLeftButtonX)
                {
                    _frameCounterTouchLeft++;
                }

                if (_frameCounterTouchLeft >= 30)
                {
                    _frameCounterTouchLeft = 0;
                    _canResetCounterTouchLeftButtonX = false;
                }*/

                /////////////RIGHT OCULUS TOUCH/////////////////////////////////////////////////////////////////////////////////////
                if (percentXRight >= 0 && percentXRight <= D3D.SurfaceWidth && percentYRight >= 0 && percentYRight <= D3D.SurfaceHeight)
                {
                    var absoluteMoveX = Convert.ToUInt32((percentXRight * 65535) / D3D.SurfaceWidth);
                    var absoluteMoveY = Convert.ToUInt32((percentYRight * 65535) / D3D.SurfaceHeight);

                    /*var yo = _updateFunctionStopwatchRight.Elapsed.Milliseconds;

                    if (yo >= 10)
                    {
                        //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, absoluteMoveX, absoluteMoveY, 0, 0);
                        _updateFunctionStopwatchRight.Stop();
                        _updateFunctionBoolRight = true;
                    }*/

                    //MOUSE DOUBLE CLICK LOGIC. IF the PLAYER clicked at one location then it stores the location, and if the player re-clicks inside of 20 frames, then click at the first click location.
                    //It's very basic, and at least I should implement also a certain "radius" of distance from the first click and the second click... If the second click is too far from the first click,
                    //then disregard the first click location.
                    if (buttonPressedOculusTouchRight != 0)
                    {
                        if (buttonPressedOculusTouchRight == 1)
                        {
                            if (hasClickedBUTTONA == 0)
                            {
                                /*if (_frameCounterTouchRight <= 20 && _canResetCounterTouchRightButtonA == true)
                                {
                                    mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, _lastMousePosXRight, _lastMousePosYRight, 0, 0);
                                    _frameCounterTouchRight = 0;
                                }*/

                                mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, absoluteMoveX, absoluteMoveY, 0, 0);
                                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, absoluteMoveX, absoluteMoveY, 0, 0);


                                _lastMousePosXRight = absoluteMoveX;
                                _lastMousePosYRight = absoluteMoveY;
                                _canResetCounterTouchRightButtonA = true;
                                hasClickedBUTTONA = 1;
                            }
                        }
                        else if (buttonPressedOculusTouchRight == 2)
                        {
                            if (hasClickedBUTTONB == false)
                            {
                                mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                hasClickedBUTTONB = true;
                            }
                        }
                    }
                }

                //////////OCULUS TOUCH BUTTONS PRESSED////////////////////////////////////////
                if (hasClickedBUTTONA == 1 && buttonPressedOculusTouchRight == 0 || hasClickedBUTTONB && buttonPressedOculusTouchRight == 0)
                {
                    if (hasClickedBUTTONA == 1 && buttonPressedOculusTouchRight == 0)
                    {
                        //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                        hasClickedBUTTONA = 0;
                    }
                    else if (hasClickedBUTTONB && buttonPressedOculusTouchRight == 0)
                    {
                        mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                        hasClickedBUTTONB = false;
                    }
                }

                if (_canResetCounterTouchRightButtonA)
                {
                    _frameCounterTouchRight++;
                }

                if (_frameCounterTouchRight >= 30)
                {
                    _frameCounterTouchRight = 0;
                    _canResetCounterTouchRightButtonA = false;
                }

                /*if (buttonPressedOculusTouchLeft != 0)
                {
                    if (buttonPressedOculusTouchLeft == 1024 && hasClickedBUTTONX == 0)
                    {
                        string windowsKeyboard = "osk";

                        foreach (Process clsProcess in Process.GetProcesses())
                        {
                            if (clsProcess.ProcessName.ToLower().Contains(windowsKeyboard.ToLower()))
                            {
                                break;
                            }
                            else
                            {
                                Process proc = new Process();
                                proc.StartInfo.FileName = windowsKeyboard + ".exe";
                                proc.Start();
                                break;
                            }
                        }
                        hasClickedBUTTONX = 1;
                    }

                    else if (buttonPressedOculusTouchLeft == 1048576 && buttonPressedOculusTouchLeft != lastbuttonPressedOculusTouchLeft)
                    {
                        if (hasClickedHomeButtonTouchLeft == false)
                        {
                            D3D.OVR.RecenterTrackingOrigin(D3D._oculusRiftVirtualRealityProvider.SessionPtr);
                            hasClickedHomeButtonTouchLeft = true;
                        }
                    }
                }
                else if (buttonPressedOculusTouchLeft == 0 && hasClickedHomeButtonTouchLeft || buttonPressedOculusTouchLeft == 0 && hasClickedBUTTONX == 1)
                {
                    if (buttonPressedOculusTouchLeft == 0 && hasClickedHomeButtonTouchLeft)
                    {
                        hasClickedHomeButtonTouchLeft = false;
                    }
                    else if (buttonPressedOculusTouchLeft == 0 && hasClickedBUTTONX == 1)
                    {
                        hasClickedBUTTONX = 0;
                    }
                }*/

                /*//////////OCULUS TOUCH BUTTONS NOT PRESSED////////////////////////////////////////
                long test = 80;
                /////////RIGHT THUMBSTICK///////////
                var yo6 = _updateFunctionStopwatchRightThumbstickGoLeft.Elapsed.Milliseconds;
                if (yo6 >= 75)
                {
                    if (thumbStickRight[1].Y <= -0.1f && hasUsedThumbStickRightE == false)
                    {
                        //Console.WriteLine("test");
                        mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -test, 0);
                        hasUsedThumbStickRightE = true;
                    }
                    else if (hasUsedThumbStickRightE)
                    {
                        hasUsedThumbStickRightE = false;
                    }
                    _updateFunctionStopwatchRightThumbstickGoLeft.Stop();
                    _updateFunctionBoolRightThumbStickGoLeft = true;
                }
                ///////////////////////////////////////////////////////////////////////////

                /////////RIGHT THUMBSTICK/////////////////////////////////////////////////////
                var yo7 = _updateFunctionStopwatchRightThumbstickGoRight.Elapsed.Milliseconds;
                if (yo7 >= 75)
                {
                    if (thumbStickRight[1].Y >= 0.1f && hasUsedThumbStickRightQ == false)
                    {
                        mouse_event(MOUSEEVENTF_WHEEL, 0, 0, test, 0);
                        //hasUsedThumbStickRightQ = true;
                    }
                    else if (hasUsedThumbStickRightQ)
                    {
                        hasUsedThumbStickRightQ = false;
                    }
                    _updateFunctionStopwatchRightThumbstickGoRight.Stop();
                    _updateFunctionBoolRightThumbStickGoRight = true;
                }*/
                /////////////RIGHT OCULUS TOUCH////////////////////////////////////////////
            }
        }


        void _process_rigidbody_two(RigidBody rigidbody, int x, int y, int z, int count)
        {
            int _index = 0;// x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z);

            if (_has_grabbed_right_swtch == 1)
            {
                Vector3 handPos = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);
                Vector3 screen_pos = new Vector3(_finalRotMatrixScreen.M41, _finalRotMatrixScreen.M42, _finalRotMatrixScreen.M43);

                if (_some_frame_counter_raycast_00[_index][count] >= _bounding_box_max_frame_before_check)
                {
                    float distance = sc_maths.sc_check_distance_node_3d_geometry(handPos, screen_pos, 10, 10, 10, 10, 10, 10); //11.31415926535f

                    if (distance < _max_distance_to_check_bounding_box_temp)
                    {
                        if (_length_of_ray_right < 0.1f + _player_rght_hnd[0]._total_torso_depth)
                        {
                            _process_rigidbody_that_are_currently_activated_or_not(rigidbody, x, y, z, count);

                            //_objects_rigid_static_00[_index][count] = rigidbody;
                            //_objects_static_00[_index][count] = 2;

                            _grab_rigid_data._index = count;
                            _grab_rigid_data._body = rigidbody;
                            _grab_rigid_data._physics_engine_index = _index;

                            _some_frame_counter_raycast_00[_index][count] = 0;
                            _has_grabbed_right_swtch = 2;
                            //Console.WriteLine("colliding");
                        }
                        else
                        {
                            //Console.WriteLine("not colliding");
                        }

                        /*if (!rigidbody.IsStatic)
                        {

                        }*/
                    }

                }
            }
            else
            {
                /*if (_has_grabbed_right_swtch == 1)
                {
                    Vector3 handPos = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);
                    Vector3 screen_pos = new Vector3(_finalRotMatrixScreen.M41, _finalRotMatrixScreen.M42, _finalRotMatrixScreen.M43);

                    if (_some_frame_counter_raycast_00[_index][count] >= _bounding_box_max_frame_before_check)
                    {
                        float distance = sc_maths.sc_check_distance_node_3d_geometry(handPos, screen_pos, 10, 10, 10, 10, 10, 10); //11.31415926535f

                        if (distance < _max_distance_to_check_bounding_box_temp)
                        {
                            if (_length_of_ray_right < 0.0025f + _SC_visual_object_manager._humRig._player_rght_hnd[0]._total_torso_depth)
                            {
                                _process_rigidbody_that_are_currently_activated_or_not(rigidbody, x, y, z, count);

                                _objects_rigid_static_00[_index][count] = rigidbody;
                                _objects_static_00[_index][count] = 2;

                                _grab_rigid_data._index = count;
                                _grab_rigid_data._body = rigidbody;
                                _grab_rigid_data._physics_engine_index = _index;

                                _some_frame_counter_raycast_00[_index][count] = 0;
                                _has_grabbed_right_swtch = 2;
                                //Console.WriteLine("colliding");
                            }
                            else
                            {
                                //Console.WriteLine("not colliding");
                            }



                            //MessageBox((IntPtr)0, "test", "sc core systems message", 0);

                            /*bool _boundingBox = _world_list[_index].CollisionSystem.CheckBoundingBoxes(rigidbody, _SC_visual_object_manager._humRig._player_rght_hnd[0]._singleObjectOnly.transform.Component.rigidbody); //_SC_visual_object_manager._humRig._player_rght_hnd._singleObjectOnly.transform.Component.rigidbody

                            if (!_boundingBox) //DO NOTHING FOR THE MOMENT
                            {
                                Console.WriteLine("colliding");
                                //_process_rigidbody_that_are_currently_activated_or_not(rigidbody, x, y, z, count);
                            }
                            else
                            {
                                Console.WriteLine("not colliding");
                                _process_rigidbody_that_are_currently_activated_or_not(rigidbody, x, y, z, count);

                                _objects_rigid_static_00[_index][count] = rigidbody;
                                _objects_static_00[_index][count] = 2;

                                _grab_rigid_data._index = count;
                                _grab_rigid_data._body = rigidbody;
                                _grab_rigid_data._physics_engine_index = _index;

                                _some_frame_counter_raycast_00[_index][count] = 0;
                                _has_grabbed_right_swtch = 2;
                            }
                        }
                    }
                }*/
            }

            _some_frame_counter_raycast_00[_index][count]++;
        }

        void _process_rigidbody_that_are_currently_activated_or_not(RigidBody rigidbody, int x, int y, int z, int count)
        {
            int _index = x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z);
            JVector currentDir = rigidbody.Position - new JVector(world_last_Matrix_instances_screens[0][count].M41, world_last_Matrix_instances_screens[0][count].M42, world_last_Matrix_instances_screens[0][count].M43);

            if (!rigidbody.IsStatic)
            {
                JVector currentLinearVel = rigidbody.LinearVelocity;
                JVector currentAngularVel = rigidbody.AngularVelocity;

                if (currentLinearVel.Length() < 0.0000015f && currentAngularVel.Length() < 0.0000015f && currentDir.Length() > 0.0000015f) //0.00015f
                {
                    if (!rigidbody.AllowDeactivation)
                    {
                        //rigidbody.AllowDeactivation = true;
                    }
                    
                }
                else
                {
                    if (rigidbody.AllowDeactivation)
                    {
                        //rigidbody.AllowDeactivation = false;
                    }

                    if (rigidbody.IsActive)
                    {
                        //rigidbody.IsActive = true;
                        //rigidbody.AffectedByGravity = true;
                    }
                }
            }
            else
            {

            }














































                /*
                //Console.WriteLine(rigidbody.IsActive);
                if (!rigidbody.IsActive)
                {

                    if (_some_frame_counter_raycast_00[_index][count] >= 10)
                    {
                        Vector3 body1pos = new Vector3(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z);
                        Vector3 body2pos = new Vector3(_world_screen_list[0]._singleObjectOnly.transform.Component.rigidbody.Position.X, _world_screen_list[0]._singleObjectOnly.transform.Component.rigidbody.Position.Y, _world_screen_list[0]._singleObjectOnly.transform.Component.rigidbody.Position.Z);

                        float distance = sc_maths.sc_check_distance_node_3d_geometry(body1pos, body2pos, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f); //11.31415926535f // temp

                        if (distance < 25)
                        {
                            //MessageBox((IntPtr)0, "test", "sc core systems message", 0);

                            bool _boundingBoxer = _world_list[_index].CollisionSystem.CheckBoundingBoxes(rigidbody, _world_screen_list[0]._singleObjectOnly.transform.Component.rigidbody); //_SC_visual_object_manager._humRig._player_rght_hnd._singleObjectOnly.transform.Component.rigidbody

                            if (!_boundingBoxer) //DO NOTHING FOR THE MOMENT
                            {
                                rigidbody.AffectedByGravity = false;
                                rigidbody.IsActive = false;
                            }
                            else
                            {
                                rigidbody.AffectedByGravity = false;
                                rigidbody.IsActive = true;
                            }
                        }
                    }
                }
                else
                {
                    if (!rigidbody.AffectedByGravity)
                    {
                        if (rigidbody.CollisionIsland != null)
                        {
                            //Console.WriteLine("test01");
                            if (rigidbody.CollisionIsland.Bodies != null)
                            {
                                ///Console.WriteLine("test02");
                                if (rigidbody.CollisionIsland.Bodies.Count > 0)
                                {
                                    if (rigidbody.CollisionIsland.Bodies.Count == 1)
                                    {
                                        IEnumerator enumerator1 = rigidbody.CollisionIsland.Bodies.GetEnumerator();

                                        while (enumerator1.MoveNext())
                                        {
                                            RigidBody someCurrentData = (RigidBody)enumerator1.Current;
                                            JVector currentLinearVel = someCurrentData.LinearVelocity;
                                            JVector currentAngularVel = someCurrentData.AngularVelocity;


                                            if (someCurrentData == rigidbody)
                                            {
                                                rigidbody.IsActive = true;
                                                rigidbody.AffectedByGravity = true;

                                                break;
                                            }
                                            else
                                            {

                                                if (_some_frame_counter_raycast_00[_index][count] >= 10)
                                                {
                                                    Vector3 body1pos = new Vector3(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z);
                                                    Vector3 body2pos = new Vector3(_world_screen_list[0]._singleObjectOnly.transform.Component.rigidbody.Position.X, _world_screen_list[0]._singleObjectOnly.transform.Component.rigidbody.Position.Y, _world_screen_list[0]._singleObjectOnly.transform.Component.rigidbody.Position.Z);

                                                    float distance = sc_maths.sc_check_distance_node_3d_geometry(body1pos, body2pos, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f); //11.31415926535f // temp

                                                    if (distance < 25)
                                                    {
                                                        //MessageBox((IntPtr)0, "test", "sc core systems message", 0);

                                                        bool _boundingBoxer = _world_list[_index].CollisionSystem.CheckBoundingBoxes(rigidbody, _world_screen_list[0]._singleObjectOnly.transform.Component.rigidbody); //_SC_visual_object_manager._humRig._player_rght_hnd._singleObjectOnly.transform.Component.rigidbody

                                                        if (!_boundingBoxer) //DO NOTHING FOR THE MOMENT
                                                        {
                                                            if (currentLinearVel.Length() < 0.0000015f && currentAngularVel.Length() < 0.0000015f || currentDir.Length() > 0.0000015f) //0.00015f
                                                            {

                                                                rigidbody.IsActive = false;
                                                                rigidbody.AffectedByGravity = true;
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                if (rigidbody.IsActive)
                                                                {
                                                                    rigidbody.IsActive = true;
                                                                    rigidbody.AffectedByGravity = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (currentLinearVel.Length() < 0.0000015f && currentAngularVel.Length() < 0.0000015f || currentDir.Length() > 0.0000015f) //0.00015f
                                                            {
                                                                rigidbody.IsActive = true;
                                                                rigidbody.AffectedByGravity = false;
                                                            }
                                                            else
                                                            {
                                                                if (rigidbody.IsActive)
                                                                {
                                                                    rigidbody.IsActive = true;
                                                                    rigidbody.AffectedByGravity = true;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        IEnumerator enumerator1 = rigidbody.CollisionIsland.Bodies.GetEnumerator();

                                        int hasbreakeder = 0;
                                        while (enumerator1.MoveNext())
                                        {
                                            RigidBody someCurrentData = (RigidBody)enumerator1.Current;
                                            JVector currentLinearVel = someCurrentData.LinearVelocity;
                                            JVector currentAngularVel = someCurrentData.AngularVelocity;

                                            if (_some_frame_counter_raycast_00[_index][count] >= 10)
                                            {
                                                Vector3 body1pos = new Vector3(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z);
                                                Vector3 body2pos = new Vector3(_world_screen_list[0]._singleObjectOnly.transform.Component.rigidbody.Position.X, _world_screen_list[0]._singleObjectOnly.transform.Component.rigidbody.Position.Y, _world_screen_list[0]._singleObjectOnly.transform.Component.rigidbody.Position.Z);

                                                float distance = sc_maths.sc_check_distance_node_3d_geometry(body1pos, body2pos, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f); //11.31415926535f // temp

                                                if (distance < 25)
                                                {
                                                    //MessageBox((IntPtr)0, "test", "sc core systems message", 0);

                                                    bool _boundingBoxer = _world_list[_index].CollisionSystem.CheckBoundingBoxes(rigidbody, _world_screen_list[0]._singleObjectOnly.transform.Component.rigidbody); //_SC_visual_object_manager._humRig._player_rght_hnd._singleObjectOnly.transform.Component.rigidbody

                                                    if (!_boundingBoxer) //DO NOTHING FOR THE MOMENT
                                                    {

                                                        if (currentLinearVel.Length() < 0.0000015f && currentAngularVel.Length() < 0.0000015f || currentDir.Length() > 0.0000015f) //0.00015f
                                                        {

                                                            rigidbody.IsActive = false;
                                                            rigidbody.AffectedByGravity = true;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            if (rigidbody.IsActive)
                                                            {
                                                                rigidbody.IsActive = true;
                                                                rigidbody.AffectedByGravity = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (currentLinearVel.Length() < 0.0000015f && currentAngularVel.Length() < 0.0000015f || currentDir.Length() > 0.0000015f) //0.00015f
                                                        {
                                                            rigidbody.IsActive = true;
                                                            rigidbody.AffectedByGravity = false;
                                                        }
                                                        else
                                                        {
                                                            if (rigidbody.IsActive)
                                                            {
                                                                rigidbody.IsActive = true;
                                                                rigidbody.AffectedByGravity = true;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    JVector currentLinearVel = rigidbody.LinearVelocity;
                                    JVector currentAngularVel = rigidbody.AngularVelocity;



                                    if (currentLinearVel.Length() < 0.0000015f && currentAngularVel.Length() < 0.0000015f || currentDir.Length() > 0.0000015f) //0.00015f == 400 approx => 0.00075f == 400 approx
                                    {
                                        rigidbody.IsActive = true;
                                        rigidbody.AffectedByGravity = true;
                                    }
                                    else
                                    {
                                        if (rigidbody.IsActive)
                                        {
                                            rigidbody.IsActive = true;
                                            rigidbody.AffectedByGravity = true;
                                        }

                                        //body.IsActive = true;
                                        //body.AffectedByGravity = true;
                                    }
                                }

                            }
                            else
                            {
                                JVector currentLinearVel = rigidbody.LinearVelocity;
                                JVector currentAngularVel = rigidbody.AngularVelocity;

                                if (currentLinearVel.Length() < 0.0000015f && currentAngularVel.Length() < 0.0000015f || currentDir.Length() > 0.0000015f) //0.00015f == 400 approx => 0.00075f == 400 approx
                                {
                                    rigidbody.IsActive = true;
                                    rigidbody.AffectedByGravity = true;
                                }
                                else
                                {
                                    if (rigidbody.IsActive)
                                    {
                                        rigidbody.IsActive = true;
                                        rigidbody.AffectedByGravity = true;
                                    }

                                    //body.IsActive = true;
                                    //body.AffectedByGravity = true;
                                }

                            }
                        }
                        else
                        {
                            if (rigidbody.IsActive)
                            {
                                rigidbody.IsActive = true;
                                rigidbody.AffectedByGravity = true;
                            }
                        }
                    }
                    else
                    {
                        if (rigidbody.IsActive)
                        {
                            rigidbody.IsActive = true;
                            rigidbody.AffectedByGravity = true;
                        }

                    }

                }*/
            }




        void _process_stuffer(RigidBody rigidbody, int x, int y, int z, int count, Matrix _matrix_to_set)
        {
            /*int _index = x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z);

            worldMatrix_instances_screens[x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z)][count] = _matrix_to_set;// someRotationFinal;

            Quaternion quat00;
            Quaternion.RotationMatrix(ref _matrix_to_set, out quat00);

            rigidbody.Orientation = JMatrix.CreateFromQuaternion(new JQuaternion(quat00.X, quat00.Y, quat00.Z, quat00.W));
            rigidbody.Position = new JVector(_matrix_to_set.M41, _matrix_to_set.M42, _matrix_to_set.M43);
            rigidbody.AngularVelocity = JVector.Zero;
            rigidbody.LinearVelocity = JVector.Zero;
            */
            /*Matrix humanBodyRotation = originRot * rotatingMatrix * rotatingMatrixForPelvis; //originRot
            humanBodyRotation.M41 = 0;
            humanBodyRotation.M42 = 0;
            humanBodyRotation.M43 = 0;
            humanBodyRotation.M44 = 1;

            JQuaternion quatterer = JQuaternion.CreateFromMatrix(_grabbed_body_pos_rot); //_grabbed_body_pos_rot
            Quaternion tester;
            tester.X = quatterer.X;
            tester.Y = quatterer.Y;
            tester.Z = quatterer.Z;
            tester.W = quatterer.W;

            Matrix rigidBodyMatrix;
            Matrix.RotationQuaternion(ref tester, out rigidBodyMatrix);

            rigidBodyMatrix.M41 = 0;
            rigidBodyMatrix.M42 = 0;
            rigidBodyMatrix.M43 = 0;
            rigidBodyMatrix.M44 = 1;

            //TORSO PIVOT
            Vector3 MOVINGPOINTER = new Vector3(_SC_visual_object_manager._humRig._player_torso[_index]._ORIGINPOSITION.M41, _SC_visual_object_manager._humRig._player_torso[_index]._ORIGINPOSITION.M42, _SC_visual_object_manager._humRig._player_torso[_index]._ORIGINPOSITION.M43);

            //SAVING IN MEMORY THE ORIGINAL TORSO MATRIX NOT AFFECTED BY CURRENT POSITION AND ROTATION CHANGES.
            Matrix _rotMatrixer = _SC_visual_object_manager._humRig._player_torso[_index]._ORIGINPOSITION;
            Quaternion forTest;
            Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

            //FROM THE MATRIX OF ROTATION/POSITION, I GET THE QUATERNION OUT OF THAT AND CREATE THE DIRECTIONS THAT THE OBJECTS ARE ORIGINALLY FACING.
            var direction_feet_forward_ori_torso = _getDirection(Vector3.ForwardRH, forTest);
            var direction_feet_right_ori_torso = _getDirection(Vector3.Right, forTest);
            var direction_feet_up_ori_torso = _getDirection(Vector3.Up, forTest);

            //SINCE THE PIVOT POINT IS CURRENTLY IN THE MIDDLE OF THE TORSO, IT CANNOT ROTATE AT THAT POINT OTHERWISE, IT WONT FOLLOW THE PELVIS ROTATION LATER ON.
            //SO WE CURRENTLY ONLY OFFSET THE TORSO "MIDDLE OF SPINE APPROX" TO HALF OF THE CURRENT HEIGHT IN ORDER TO MAKE THE PIVOT POINT, APPROX WHERE THE PELVIS IS.
            Vector3 TORSOPIVOT = MOVINGPOINTER + -(direction_feet_up_ori_torso * (_SC_visual_object_manager._humRig._player_torso[_index]._total_torso_height * 0.5f));

            _rightTouchMatrix.M41 = handPoseRight.Position.X;
            _rightTouchMatrix.M42 = handPoseRight.Position.Y;
            _rightTouchMatrix.M43 = handPoseRight.Position.Z;

            Quaternion otherQuat;
            Quaternion.RotationMatrix(ref humanBodyRotation, out otherQuat);

            //CONVERTING THE QUATERNION OF THAT TO THE DIRECTION OF ITS ROTATION
            var direction_feet_forward_torso = _getDirection(Vector3.ForwardRH, otherQuat);
            var direction_feet_right_torso = _getDirection(Vector3.Right, otherQuat);
            var direction_feet_up_torso = _getDirection(Vector3.Up, otherQuat);

            MOVINGPOINTER = TORSOPIVOT;
            //I AM CALCULATING THE DIFFERENCE IN THE MOVEMENT FROM THE ORIGINAL POSITION TO THE CURRENT OFFSET AT THE BOTTOM OF THE SPINE WHERE I MOVED THAT POINT.
            var diffNormPosX = (MOVINGPOINTER.X) - _rightTouchMatrix.M41;
            var diffNormPosY = (MOVINGPOINTER.Y) - _rightTouchMatrix.M42;
            var diffNormPosZ = (MOVINGPOINTER.Z) - _rightTouchMatrix.M43;

            //I AM USING THE NEW PIVOT POINT AT THE BOTTOM OF THE SPINE AND ADDING THE FRONT/RIGHT/UP VECTOR OF THE ROTATION OF THAT SPINE AND THEN ADDING THE DIFFERENCE X/W/Z BETWEEN ORIGINAL POS AND THE NEW PIVOT POS
            MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right_torso * (diffNormPosX));
            MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_torso * (diffNormPosY));
            MOVINGPOINTER = MOVINGPOINTER + (direction_feet_forward_torso * (diffNormPosZ));

            var _rightTouchMatrix_00 = _rightTouchMatrix;
            _rightTouchMatrix_00.M41 = 0;
            _rightTouchMatrix_00.M42 = 0;
            _rightTouchMatrix_00.M43 = 0;
            _rightTouchMatrix_00.M44 = 1;

            _rightTouchMatrix_00 = _rightTouchMatrix_00;// * originRot * rotatingMatrix * rotatingMatrixForPelvis;
            Quaternion.RotationMatrix(ref _rightTouchMatrix_00, out otherQuat);

            //CONVERTING THE QUATERNION OF THAT TO THE DIRECTION OF ITS ROTATION
            var direction_feet_forward_hand = _getDirection(Vector3.ForwardRH, otherQuat);
            var direction_feet_right_hand = _getDirection(Vector3.Right, otherQuat);
            var direction_feet_up_hand = _getDirection(Vector3.Up, otherQuat);

            float yaw = (float)Math.Atan2(_rightTouchMatrix_00.M13, _rightTouchMatrix_00.M33);
            float pitch = (float)Math.Asin(-_rightTouchMatrix_00.M23);
            float roll = (float)Math.Atan2(_rightTouchMatrix_00.M21, _rightTouchMatrix_00.M22);

            var someOtherRigidBodyMatrix = rigidBodyMatrix;// * originRot * rotatingMatrix * rotatingMatrixForGrabber;

            float yaw01 = (float)Math.Atan2(someOtherRigidBodyMatrix.M13, someOtherRigidBodyMatrix.M33);
            float pitch01 = (float)Math.Asin(-someOtherRigidBodyMatrix.M23);
            float roll01 = (float)Math.Atan2(someOtherRigidBodyMatrix.M21, someOtherRigidBodyMatrix.M22);

            var totalDiffX = (pitch01 - pitch);
            var totalDiffY = (yaw01 - yaw);
            var totalDiffZ = (roll01 - roll);

            //var totalDiffX = (pitch -rot_grab_touch_right_ori_X);
            //var totalDiffY = ( yaw - rot_grab_touch_right_ori_Y);
            //var totalDiffZ = (roll - rot_grab_touch_right_ori_Z);

            //var totalDiffORIX = rot_grab_touch_right_ori_X;
            //var totalDiffORIY = rot_grab_touch_right_ori_Y;
            //var totalDiffORIZ = rot_grab_touch_right_ori_Z;

            var diffX = (pitch01 - totalDiffX);// (totalDiffX - totalDiffORIX);
            var diffY = (yaw01 - totalDiffY);// (totalDiffY - totalDiffORIY);
            var diffZ = (roll01 - totalDiffZ);// (totalDiffZ - totalDiffORIZ);


            var some_total_object_rot_matrix = SharpDX.Matrix.RotationYawPitchRoll((float)diffY, (float)diffX, (float)diffZ);
            some_total_object_rot_matrix.Invert();





            someRotationFinal = some_total_object_rot_matrix;




            _last_frame_roll = totalDiffZ;
            _last_frame_yaw = totalDiffY;
            _last_frame_pitch = totalDiffX;



            //_last_min_distX = Math.Abs(_last_min_distX);
            //_last_min_distY = Math.Abs(_last_min_distY);
            _last_min_distZ = Math.Abs(_last_min_distZ);

            //var MOVINGPOINTER0 = MOVINGPOINTER; // new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);
            //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_right_hand * -(_last_min_distX));
            MOVINGPOINTER = MOVINGPOINTER + (direction_feet_up_hand * (_last_min_distY));
            MOVINGPOINTER = MOVINGPOINTER + (direction_feet_forward_hand * (_last_min_distZ));

            MOVINGPOINTER.X += OFFSETPOS.X;
            MOVINGPOINTER.Y += OFFSETPOS.Y;
            MOVINGPOINTER.Z += OFFSETPOS.Z;


            var handPoser = MOVINGPOINTER;


            someRotationFinal.M41 = handPoser.X;
            someRotationFinal.M42 = handPoser.Y;
            someRotationFinal.M43 = handPoser.Z;*/

            /*worldMatrix_instances[x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z)][count] = _grabbed_object_matrix;// someRotationFinal;

            Quaternion quat00;
            Quaternion.RotationMatrix(ref _grabbed_object_matrix, out quat00);
            

            rigidbody.Orientation = JMatrix.CreateFromQuaternion(new JQuaternion(quat00.X, quat00.Y, quat00.Z, quat00.W));
            rigidbody.Position = new JVector(_grabbed_object_matrix.M41, _grabbed_object_matrix.M42, _grabbed_object_matrix.M43);*/


            /*
            Quaternion quat00;
            Quaternion.RotationMatrix(ref someRotationFinal, out quat00);

            JQuaternion quatterer00;
            quatterer00.X = quat00.X;
            quatterer00.Y = quat00.Y;
            quatterer00.Z = quat00.Z;
            quatterer00.W = quat00.W;

            rigidbody.Orientation = JMatrix.CreateFromQuaternion(quatterer00);
            rigidbody.Position = new JVector(handPoser.X, handPoser.Y, handPoser.Z);

            _last_grabbed_object_matrix = someRotationFinal;

            _last_current_hand_pos_for_d = new Vector3(final_hand_pos_right_locked.M41, final_hand_pos_right_locked.M42, final_hand_pos_right_locked.M43);

            _last_current_hand_float_for_d = (handPoser - new Vector3(final_hand_pos_right_locked.M41, final_hand_pos_right_locked.M42, final_hand_pos_right_locked.M43)).Z;*/
        }



        Matrix someRotationFinal;
        float _last_current_hand_float_for_d = 0;
        Vector3 _last_current_hand_pos_for_d;
        Matrix _last_grabbed_object_matrix;

        float _last_min_distX = 0;
        float _last_min_distY = 0;
        float _last_min_distZ = 0;
        JMatrix _grabbed_body_pos_rot;
        double startnGrabbedY { get; set; }
        double startnGrabbedX { get; set; }
        double startnGrabbedZ { get; set; }
        double rot_grab_touch_right_X { get; set; }
        double rot_grab_touch_right_Y { get; set; }
        double rot_grab_touch_right_Z { get; set; }


        Matrix rigidBodyMatrix;

        float _body_collision_fraction;
        RigidBody _body_collision;
        JVector _body_collision_normal;

        Vector3 _last_offset_grabbed_pos_norm;
        float _last_offset_grabbed_pos_norm_dist;
















        double rot_grab_touch_right_ori_X { get; set; }
        double rot_grab_touch_right_ori_Y { get; set; }
        double rot_grab_touch_right_ori_Z { get; set; }


        int _tier_logic_swtch_grab = 0;

        int _swtch_hasRotated = 0;
        Quaternion _hmdRoter;
        SharpDX.Matrix _oculusR_Cursor_matrix = SharpDX.Matrix.Identity;
        double RotationY4Pelvis { get; set; }
        double RotationX4Pelvis { get; set; }
        double RotationZ4Pelvis { get; set; }


        double RotationGrabbedY { get; set; }
        double RotationGrabbedX { get; set; }
        double RotationGrabbedZ { get; set; }


        private void _oculus_touch_controls(double percentXRight, double percentYRight, Vector2f[] thumbStickRight, double percentXLeft, double percentYLeft, Vector2f[] thumbStickLeft, double realMousePosX, double realMousePosY) //
        {
            if (_indexMouseMove == 0)
            {
                if (buttonPressedOculusTouchLeft == 1048576 && buttonPressedOculusTouchLeft != lastbuttonPressedOculusTouchLeft)
                {
                    if (hasClickedHomeButtonTouchLeft == false)
                    {
                        D3D.OVR.RecenterTrackingOrigin(D3D.sessionPtr);
                        hasClickedHomeButtonTouchLeft = true;
                    }
                }
            }
        }

        SharpDX.Matrix _mouseCursorMatrix = SharpDX.Matrix.Identity;
        int _out_of_bounds_oculus_rift = 0;
        int _out_of_bounds_right = 0;
        int _out_of_bounds_left = 0;
        uint _lastMousePosXRight = 9999;
        uint _lastMousePosYRight = 9999;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd,
                                         UInt32 Msg,
                                         IntPtr wParam,
                                         IntPtr lParam);



        bool _updateFunctionBoolLeftThumbStick = true;
        int _frameCounterTouchRight = 0;

        //DModeler _intersectTouchRight;
        SharpDX.Matrix _intersectTouchRightMatrix = SharpDX.Matrix.Identity;

        //DModeler _intersectTouchLeft;
        SharpDX.Matrix _intersectTouchLeftMatrix = SharpDX.Matrix.Identity;

        /*Stopwatch _updateFunctionStopwatchLeftHandTrigger;
        Stopwatch _updateFunctionStopwatchRightHandTrigger;
        Stopwatch _updateFunctionStopwatchLeftThumbstickGoRight;
        Stopwatch _updateFunctionStopwatchLeftThumbstickGoLeft;*/
        Stopwatch _updateFunctionStopwatchRightThumbstickGoRight = new Stopwatch();
        Stopwatch _updateFunctionStopwatchRightThumbstickGoLeft = new Stopwatch();
        //Stopwatch _updateFunctionStopwatchRightThumbstick;
        Stopwatch _updateFunctionStopwatchLeftThumbstick = new Stopwatch();
        /*Stopwatch _updateFunctionStopwatchRightIndexTrigger;
        Stopwatch _updateFunctionStopwatchLeftIndexTrigger;*/
        Stopwatch _updateFunctionStopwatchRight = new Stopwatch();
        //Stopwatch _updateFunctionStopwatchLeft;
        //Stopwatch _updateFunctionStopwatchTouchRightButtonA;
        //Stopwatch _newStopWatch = new Stopwatch();


        Plane planer;

        Vector3 centerPosRight;
        Vector3 rayDirRight;
        SharpDX.Ray someRay;
        Vector3 intersectPointRight;
        bool intersecter;

        Vector3 centerPosLeft;
        Vector3 rayDirLeft;
        SharpDX.Ray someRayLeft;
        Vector3 intersectPointLeft;
        bool intersecterLeft;
        Vector3 stabilizedIntersectionPosLeft;
        Vector3 stabilizedIntersectionPosRight;


        const int _MaxArraySize0 = 50;
        const int _MaxArraySize1 = 49;

        //HERE IS THE MOUSE STABILIZER ARRAYS - THE BIGGER THE ARRAYS THE SLOWER AND MORE STABLE THE MOUSE IS ON THE SCREEN.
        Vector3[] arrayOfStabilizerPosRight = new Vector3[_MaxArraySize0];
        double[] arrayOfStabilizerPosXRight = new double[_MaxArraySize0];
        double[] arrayOfStabilizerPosDifferenceXRight = new double[_MaxArraySize1];
        double[] arrayOfStabilizerPosYRight = new double[_MaxArraySize0];
        double[] arrayOfStabilizerPosDifferenceYRight = new double[_MaxArraySize1];

        double[] arrayOfStabilizerPosZRight = new double[_MaxArraySize0];
        double[] arrayOfStabilizerPosDifferenceZRight = new double[_MaxArraySize1];



        Vector3[] arrayOfStabilizerPosLeft = new Vector3[_MaxArraySize0];
        double[] arrayOfStabilizerPosXLeft = new double[_MaxArraySize0];
        double[] arrayOfStabilizerPosDifferenceXLeft = new double[_MaxArraySize1];
        double[] arrayOfStabilizerPosYLeft = new double[_MaxArraySize0];
        double[] arrayOfStabilizerPosDifferenceYLeft = new double[_MaxArraySize1];

        double[] arrayOfStabilizerPosZLeft = new double[_MaxArraySize0];
        double[] arrayOfStabilizerPosDifferenceZLeft = new double[_MaxArraySize1];

        //
        Vector3[] _arrayOfStabilizerPosRight = new Vector3[_MaxArraySize0];
        double[] _arrayOfStabilizerPosXRight = new double[_MaxArraySize0];
        double[] _arrayOfStabilizerPosDifferenceXRight = new double[_MaxArraySize1];
        double[] _arrayOfStabilizerPosYRight = new double[_MaxArraySize0];
        double[] _arrayOfStabilizerPosDifferenceYRight = new double[_MaxArraySize1];

        Vector3[] _arrayOfStabilizerPosLeft = new Vector3[_MaxArraySize0];
        double[] _arrayOfStabilizerPosXLeft = new double[_MaxArraySize0];
        double[] _arrayOfStabilizerPosDifferenceXLeft = new double[_MaxArraySize1];
        double[] _arrayOfStabilizerPosYLeft = new double[_MaxArraySize0];
        double[] _arrayOfStabilizerPosDifferenceYLeft = new double[_MaxArraySize1];


        bool _canResetCounterTouchRightButtonA = false;
        bool _canResetCounterTouchRightButtonB = false;
        int _frameCounterTouchLeft = 0;
        bool _canResetCounterTouchLeftButtonA = false;
        bool _canResetCounterTouchLeftButtonB = false;
        bool _canResetCounterTouchLeftButtonX = false;
        bool _canResetCounterTouchLeftButtonY = false;
        bool hasUsedThumbStickLeftW = false;
        bool hasUsedThumbStickLeftS = false;
        bool hasUsedThumbStickLeftA = false;
        bool hasUsedThumbStickRightD = false;
        bool hasUsedThumbStickRightQ = false;
        bool hasUsedThumbStickRightE = false;
        bool lastHasUsedHandTriggerLeft = false;
        bool hasUsedHandTriggerLeft = false;

        int currentFrameLeft = 0;
        int currentFrameRight = 0;
        double averageXRight = 0;
        double averageYRight = 0;
        double averageZRight = 0;
        double lastRightHitPointXFrameOne = 0;
        double lastRightHitPointYFrameOne = 0;
        double lastRightHitPointZFrameOne = 0;
        double positionXRight = 0;
        double positionYRight = 0;
        double positionZRight = 0;
        double averageXLeft = 0;
        double averageYLeft = 0;
        double averageZLeft = 0;
        double lastLeftHitPointXFrameOne = 0;
        double lastLeftHitPointYFrameOne = 0;
        double lastLeftHitPointZFrameOne = 0;
        double positionXLeft = 0;
        double positionYLeft = 0;
        double positionZLeft = 0;

        //bool _failed = false;
        bool _createdSceneObjects = false;
        bool _shaderQuality = true;
        public bool _stopWatchSwitch = true;
        bool _startOnce = true;
        bool _startOnce0 = true;
        bool restartFrameCounterRight = false;
        bool hasClickedHomeButtonTouchLeft = false;
        bool isHoldingBUTTONA = false;
        bool hasClickedBUTTONB = false;
        int hasClickedBUTTONX = 0;
        int hasClickedBUTTONY = 0;
        bool restartFrameCounterLeft = false;
        bool _startOnce02 = true;
        bool _updateFunctionBoolRight = true;
        bool _updateFunctionBoolLeft = true;
        bool _updateFunctionBoolLeftThumbStickGoLeft = true;
        bool _updateFunctionBoolLeftThumbStickGoRight = true;
        bool _updateFunctionBoolRightThumbStickGoLeft = true;
        bool _updateFunctionBoolRightThumbStickGoRight = true;
        bool _updateFunctionBoolLeftHandTrigger = true;
        bool _updateFunctionBoolRightHandTrigger = true;
        bool _updateFunctionBoolLeftIndexTrigger = true;
        bool _updateFunctionBoolRightIndexTrigger = true;
        bool _updateFunctionBoolTouchRightButtonA = true;

        double differenceX = 0;
        double differenceY = 0;
        double differenceZ = 0;
        double percentXLeft;
        double percentYLeft;
        float d;

        float widthLength;
        float heightLength;

        float r;
        float R;


        float x;
        float d1;
        float d2;

        double b;
        double currentPosWidth;
        double currentPosHeight;

        double percentXRight;
        double percentYRight;

        double currentX;
        double currentY;
        double currentZ;


        public int _indexMouseMove = 0;










        public const int KEY_W = 0x57;
        public const int KEY_A = 0x41;
        public const int KEY_S = 0x53;
        public const int KEY_D = 0x44;
        public const int KEY_SPACE = 0x20; //0x39
        public const int KEY_E = 0x45;
        public const int KEY_Q = 0x51;

        public const int KEYEVENTF_KEYUP = 0x0002;
        public const int KEYEVENTF_EXTENDEDKEY = 0x0001;

        const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        const uint MOUSEEVENTF_MOVE = 0x0001;
        const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        const uint MOUSEEVENTF_XDOWN = 0x0080;
        const uint MOUSEEVENTF_XUP = 0x0100;
        const uint MOUSEEVENTF_WHEEL = 0x0800;
        const uint MOUSEEVENTF_HWHEEL = 0x01000;
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, EntryPoint = "mouse_event")]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, long dwData, uint dwExtraInfo);























        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);
        private const UInt32 WM_SYSCOMMAND = 0x112;
        private const UInt32 SC_RESTORE = 0xf120;

        private const string OnScreenKeyboardExe = "osk.exe";

        private void ShowKeyboard()
        {
            var path64 = @"C:\Windows\winsxs\amd64_microsoft-windows-osk_31bf3856ad364e35_6.1.7600.16385_none_06b1c513739fb828\osk.exe";
            var path32 = @"C:\windows\system32\osk.exe";
            var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
            Process.Start(path);
        }
        void StartOsk()
        {
            IntPtr ptr = new IntPtr(); ;
            bool sucessfullyDisabledWow64Redirect = false;

            // Disable x64 directory virtualization if we're on x64,
            // otherwise keyboard launch will fail.
            if (System.Environment.Is64BitOperatingSystem)
            {
                sucessfullyDisabledWow64Redirect = Wow64DisableWow64FsRedirection(ref ptr);
            }

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = OnScreenKeyboardExe;
            // We must use ShellExecute to start osk from the current thread
            // with psi.UseShellExecute = false the CreateProcessWithLogon API 
            // would be used which handles process creation on a separate thread 
            // where the above call to Wow64DisableWow64FsRedirection would not 
            // have any effect.

            psi.UseShellExecute = true;
            //psi.Verb = "runas";

            Process.Start(psi);

            // Re-enable directory virtualisation if it was disabled.
            if (System.Environment.Is64BitOperatingSystem)
                if (sucessfullyDisabledWow64Redirect)
                    Wow64RevertWow64FsRedirection(ptr);
        }





        //https://pastebin.com/fAFp6NnN // Also found on the unity3D forums.
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


































        //HUMAN RIG VARIABLES
        JMatrix matrixer = JMatrix.Identity;
        JQuaternion resultQuat;
        JMatrix matrixIn = JMatrix.Identity;


        Vector4 ambientColor = new Vector4(0.75f, 0.75f, 0.75f, 1.0f);
        Vector4 diffuseColour = new Vector4(0.95f, 0.95f, 0.95f, 1);
        Vector3 lightDirection = new Vector3(0, -1, -1);

        Vector3 dirLight =new Vector3(0, -1, 0);



        int _inst_p_torso_x = _human_inst_rig_x;
        int _inst_p_torso_y = _human_inst_rig_y;
        int _inst_p_torso_z = _human_inst_rig_z;
        int _inst_p_pelvis_x = _human_inst_rig_x;
        int _inst_p_pelvis_y = _human_inst_rig_y;
        int _inst_p_pelvis_z = _human_inst_rig_z;

        int _inst_p_r_hand_x = _human_inst_rig_x;
        int _inst_p_r_hand_y = _human_inst_rig_y;
        int _inst_p_r_hand_z = _human_inst_rig_z;
        int _inst_p_l_hand_x = _human_inst_rig_x;
        int _inst_p_l_hand_y = _human_inst_rig_y;
        int _inst_p_l_hand_z = _human_inst_rig_z;

        int _inst_p_r_shoulder_x = _human_inst_rig_x;
        int _inst_p_r_shoulder_y = _human_inst_rig_y;
        int _inst_p_r_shoulder_z = _human_inst_rig_z;
        int _inst_p_l_shoulder_x = _human_inst_rig_x;
        int _inst_p_l_shoulder_y = _human_inst_rig_y;
        int _inst_p_l_shoulder_z = _human_inst_rig_z;





        int _inst_p_l_upperarm_x = _human_inst_rig_x;
        int _inst_p_l_upperarm_y = _human_inst_rig_y;
        int _inst_p_l_upperarm_z = _human_inst_rig_z;

        int _inst_p_r_upperarm_x = _human_inst_rig_x;
        int _inst_p_r_upperarm_y = _human_inst_rig_y;
        int _inst_p_r_upperarm_z = _human_inst_rig_z;

        int _inst_p_l_lowerarm_x = _human_inst_rig_x;
        int _inst_p_l_lowerarm_y = _human_inst_rig_y;
        int _inst_p_l_lowerarm_z = _human_inst_rig_z;

        int _inst_p_r_lowerarm_x = _human_inst_rig_x;
        int _inst_p_r_lowerarm_y = _human_inst_rig_y;
        int _inst_p_r_lowerarm_z = _human_inst_rig_z;

        int _inst_p_head_x = _human_inst_rig_x;
        int _inst_p_head_y = _human_inst_rig_y;
        int _inst_p_head_z = _human_inst_rig_z;


        int _inst_p_r_foot_x = _human_inst_rig_x;
        int _inst_p_r_foot_y = _human_inst_rig_y;
        int _inst_p_r_foot_z = _human_inst_rig_z;

        int _inst_p_l_foot_x = _human_inst_rig_x;
        int _inst_p_l_foot_y = _human_inst_rig_y;
        int _inst_p_l_foot_z = _human_inst_rig_z;

        int _inst_p_l_lowerleg_x = _human_inst_rig_x;
        int _inst_p_l_lowerleg_y = _human_inst_rig_y;
        int _inst_p_l_lowerleg_z = _human_inst_rig_z;

        int _inst_p_r_lowerleg_x = _human_inst_rig_x;
        int _inst_p_r_lowerleg_y = _human_inst_rig_y;
        int _inst_p_r_lowerleg_z = _human_inst_rig_z;

        int _inst_p_l_upperleg_x = _human_inst_rig_x;
        int _inst_p_l_upperleg_y = _human_inst_rig_y;
        int _inst_p_l_upperleg_z = _human_inst_rig_z;

        int _inst_p_r_upperleg_x = _human_inst_rig_x;
        int _inst_p_r_upperleg_y = _human_inst_rig_y;
        int _inst_p_r_upperleg_z = _human_inst_rig_z;

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
        public _sc_voxel[] _player_rght_hnd;

        _sc_voxel.DLightBuffer[] _SC_modL_lft_hnd_BUFFER = new _sc_voxel.DLightBuffer[1];
        _sc_voxel[] _player_lft_hnd;

        _sc_voxel.DLightBuffer[] _SC_modL_torso_BUFFER = new _sc_voxel.DLightBuffer[1];
        public _sc_voxel[] _player_torso;

        _sc_voxel.DLightBuffer[] _SC_modL_rght_shldr_BUFFER = new _sc_voxel.DLightBuffer[1];
        _sc_voxel[] _player_rght_shldr;

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


        Matrix shoulderRotationMatrixRight = Matrix.Identity;
        Matrix shoulderRotationMatrixLeft = Matrix.Identity;

        Matrix[] worldMatrix_Terrain_instances = new Matrix[1];

        bool _hasinit0 = false;

        Matrix _tempMatroxer = Matrix.Identity;

        //int _instX = -1;
        //int _instY = -1;
        //int _instZ = -1;



        float lengthOfLowerArmLeft;
        float lengthOfUpperArmLeft;

        float lengthOfLowerArmRight;
        float lengthOfUpperArmRight;

        float totalArmLengthLeft;
        float totalArmLengthRight;

        //SECOND PART
        Matrix rotationMatrix = Matrix.Identity;
        //Matrix[] _worldMatrix_instances;
        Matrix _tempMatrix = Matrix.Identity;

        //Vector3 somePosOfUpperElbowTargetTwo = new Vector3(0, 0, 0);
        //Vector3 somePosOfUpperElbowTargetOne = new Vector3(0, 0, 0);

        Vector3 realPositionOfUpperArm = new Vector3(0, 0, 0);
        Vector3 realPIVOTOfUpperArm = new Vector3(0, 0, 0);

        //TORSO STUFF
        Matrix _spine_upper_body_rot = Matrix.Identity;
        Vector3 _spine_upper_body_pos;
        Quaternion quat;
        float diffNormPosX;
        float diffNormPosY;
        float diffNormPosZ;
        Matrix matrixerer = Matrix.Identity;

        Matrix _rotatingMatrix = Matrix.Identity;
        float _mass;









    }
}










