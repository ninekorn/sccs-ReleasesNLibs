using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Ab3d.OculusWrap;
using Ab3d.DXEngine.OculusWrap;
using Ab3d.OculusWrap.DemoDX11;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using SharpDX.DirectInput;
using SCCoreSystems.SC_Graphics;
using SCCoreSystems.SC_Graphics.SC_ShaderManager;
using Jitter;
using Jitter.Dynamics;
using Jitter.Collision;
using Jitter.LinearMath;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;

using SCCoreSystems.sound;
using SCCoreSystems.sc_core;
using SCCoreSystems.sc_console;
using SCCoreSystems.sc_console_menu;
using SCCoreSystems.sc_message_object;
using System.Text;
using System.IO;
using SharpDX.Multimedia;
using SharpDX.IO;
using System.Xml;
using SharpDX.XAudio2;
using System.Linq;

using SimplexNoise;

using SC_WPF_RENDER;
using SC_WPF_RENDER.SC_Graphics;
using SC_WPF_RENDER.SC_Graphics.SC_Grid;
//using SC_WPF_RENDER.SC_Graphics.SC_Textures;
//using SC_WPF_RENDER.SC_Graphics.SC_Textures.SC_VR_Touch_Textures;
//using SC_WPF_RENDER.SC_Graphics.SC_Models;

using Jitter.Dynamics.Constraints;

namespace SCCoreSystems
{
    public class SC_Console_GRAPHICS
    {
        bool _hasGrabbed = false;
        RigidBody _lastRigidGrab;
        float _lastFraction = 0;
        JVector _lastHitPoint;
        PointPointDistance _distanceConstraintRight;
        PointPointDistance _distanceConstraintLeft;

        SC_jitter_cloth clothRect;
        int frame_counter_4_buttonY = 45;
        int display_grid_type = 0;
  
        int gravity_swtch_counter = 45;
        int gravity_swtch = 0;



        SC_DRGrid _grid;
        //main terrain.
        int _grid_size_x = 2;
        int _grid_size_y = 2;
        int _grid_size_z = 2;

        SC_VR_IcoSphere _icoSphere;
        int _icoVertexCount = 0;
        int[][] swtch_for_last_pos;

        DContainmentGrid _dContainer;
        DContainmentGrid _dTouchRightContainer;
        DContainmentGrid _dTouchLeftContainer;

        SC_VR_Cube _arrayOfCubes;
        const int ChunkWidth_L = 3;
        const int ChunkWidth_R = 2;

        const int ChunkHeight_L = 3;
        const int ChunkHeight_R = 2;

        const int ChunkDepth_L = 3;
        const int ChunkDepth_R = 2;
        float[] arrayX = new float[(ChunkWidth_L + ChunkWidth_R + 1) * (ChunkHeight_L + ChunkHeight_R + 1) * (ChunkDepth_L + ChunkDepth_R + 1)];
        float[] arrayY = new float[(ChunkWidth_L + ChunkWidth_R + 1) * (ChunkHeight_L + ChunkHeight_R + 1) * (ChunkDepth_L + ChunkDepth_R + 1)];
        float[] arrayZ = new float[(ChunkWidth_L + ChunkWidth_R + 1) * (ChunkHeight_L + ChunkHeight_R + 1) * (ChunkDepth_L + ChunkDepth_R + 1)];
        int[] draw_dcontainmentgrid = new int[(ChunkWidth_L + ChunkWidth_R + 1) * (ChunkHeight_L + ChunkHeight_R + 1) * (ChunkDepth_L + ChunkDepth_R + 1)];
        public static int _vertexCount = 8;





        DTerrain _grid_X;
        DTerrain _grid_Y;
        DTerrain _grid_Z;

        DTerrain_Screen _screen_grid_X;
        DTerrain_Screen _screen_grid_Y;
        DTerrain_Screen _screen_grid_Z;


        DTerrain_Screen_Metric _screen_metric_grid_X;
        DTerrain_Screen_Metric _screen_metric_grid_Y;
        DTerrain_Screen_Metric _screen_metric_grid_Z;


        DTerrain_Screen_Metric _WORLD_GRID_X;

























        //int[][] array_of_swtch_rigid;
        //int[][] array_of_swtch_rigid;

        Vector3[][] _array_of_last_frame_voxel_pos;
        Vector3[][] _array_of_last_frame_cube_pos;

        int _static_counter = 0;
        int tempIndex = 0;
        int tempMultiInstancePhysicsTotal = 1; // min 1 
        int isUsingMultiInstancePhysics = 0;

        Quaternion otherQuat;

        float temproll = 0;
        float tempyaw = 0;
        int had_locked_screen = -1;

        float minveloc = 1.175494351F - 5;  //min floating point value == 1.175494351F-38 but this is too low and objects don't deactivate.

        //oculus touch moveNrot speeds
        float speedRot = 0.15f;
        float speedPos = 0.05f;

        Stopwatch tick_perf_counter = new Stopwatch();

        int mirror_move = 0;

        int _has_init_screen = 0;

        //PHYSICS ENGINE SETTINGS // currently only 1 instance is stable.
        public static World[] _world_list;


        //stop wasting time here. its not working yet.
        //stop wasting time here. its not working yet.
        //stop wasting time here. its not working yet.
        public const int _physics_engine_instance_x = 1; //4
        public const int _physics_engine_instance_y = 1; //1
        public const int _physics_engine_instance_z = 1; //4
        //stop wasting time here. its not working yet.
        //stop wasting time here. its not working yet.
        //stop wasting time here. its not working yet.


        JVector _world_gravity = new JVector(0, 0, 0); //-9.81f base
        int _world_iterations = 20; // as high as possible normally for higher precision
        int _world_small_iterations = 20; // as high as possible normally for higher precision
        float _world_allowed_penetration = 0.00123f; //0.00123f
        public static SharpDX.Vector3 originPos = new SharpDX.Vector3(0, 1, 1);
        SharpDX.Vector3 originPosScreen = new SharpDX.Vector3(0, 1, -0.25f);
        //END OF

        //SCREEN SETTINGS
        int _inst_screen_x = 1;
        int _inst_screen_y = 1;
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
        bool is_static = false;
        //END OF

        //HUMAN RIG
        const int _human_inst_rig_x = 1;
        const int _human_inst_rig_y = 1;
        const int _human_inst_rig_z = 1;
        const int _addToWorld = 0;

        //the physics engine can run 4000 objects enabled and having angularOrLinear velocities but the voxels lag a bit at that many objects. but loading as many as 475000 cubes
        //having 36 vertices each and 72 triangles each but at that point it will also lag. will try later to improve the performance.
        float _voxel_mass = 100;
        int _inst_voxel_cube_x = 4;
        int _inst_voxel_cube_y = 20;
        int _inst_voxel_cube_z = 4;
        float _voxel_cube_size_x = 0.015f;//0.0115f //restitution
        float _voxel_cube_size_y = 0.015f;//0.0115f //static friction
        float _voxel_cube_size_z = 0.015f;//0.0015f //kinetic friction
        float voxel_general_size = 0.1f;
        int voxel_type = -1;

        //PHYSICS CUBES
        int _inst_cube_x = 4;
        int _inst_cube_y = 20;
        int _inst_cube_z = 4;
        float _cube_size_x = 0.025f; //0.0115f //1.5f
        float _cube_size_y = 0.025f; //0.0115f //1.5f
        float _cube_size_z = 0.025f;
        //END OF


        //PHYSICS JITTER CLOTH
        int _inst_jitter_cloth_x = 1;
        int _inst_jitter_cloth_y = 1;
        int _inst_jitter_cloth_z = 1;
        //float _cube_size_x = 0.025f; //0.0115f //1.5f
        //float _cube_size_y = 0.025f; //0.0115f //1.5f
        //float _cube_size_z = 0.025f;
        //END OF


        //float _voxel_cube_size_x = 0.0515f;
        //float _voxel_cube_size_y = 0.0515f;
        //float _voxel_cube_size_z = 0.0515f;

        int _inst_spectrum_x = 75; // 36 // 210 //75
        int _inst_spectrum_y = 1;
        int _inst_spectrum_z = 75; // 36 // 210 //75 //5625
        float _spectrum_size_x = 0.001515f; //0.001115f
        float _spectrum_size_y = 0.001515f;
        float _spectrum_size_z = 0.001515f;

        //static cubes 
        int _inst_terrain_tile_x = 1;
        int _inst_terrain_tile_y = 1;
        int _inst_terrain_tile_z = 1;
        float _terrain_tile_size_x = 0.015f;
        float _terrain_tile_size_y = 0.05f;
        float _terrain_tile_size_z = 0.015f;

        //main terrain.
        float _terrain_size_x = 2;
        float _terrain_size_y = 0.02f;
        float _terrain_size_z = 2;

        int _type_of_cube = 3;

        int _bounding_box_max_frame_before_check = 0;
        int _max_distance_to_check_bounding_box_temp = 30;




        _rigid_data _grab_rigid_data;











        Matrix _direction_offsetter;
        Matrix _screen_direction_offsetter_two;

        SC_cube[] _world_screen_list;
        Matrix[][] worldMatrix_instances_screens;
        Matrix[][] world_last_Matrix_instances_screens;

        SC_cube[] _world_cube_list;
        Matrix[][] worldMatrix_instances_cubes;


        SC_jitter_cloth[] _world_jitter_cloth_list;
        Matrix[][] worldMatrix_instances_jitter_cloth;


        //Vector3[][] _last_frame_force;

        sc_spectrum[] _world_spectrum_list;
        sc_voxel[] _world_voxel_cube_lists;
        SC_cube _world_terrain;
        SC_cube[] _world_terrain_tile_list;
        SC_cube[] _world_screen_assets_list;
        Matrix[][] worldMatrix_instances_player_ik;
        Matrix[][] worldMatrix_instances_voxel_cube;
        Matrix[][] worldMatrix_instances_spectrum;
        Matrix[][] worldMatrix_instances_icosphere;
        Matrix[][] worldMatrix_instances_sphere;
        Matrix[][] worldMatrix_instances_DZgrid;

        Matrix[][] worldMatrix_instances_terrain_tiles;
        Matrix[][] worldMatrix_instances_terrain;
        Matrix[][] worldMatrix_instances_screen_assets;
        Matrix[][] _screenDirMatrix;


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


        SC_AI.data_input main_ai_2d_data_input;
        string short_path = "";
        float spectrum_noise_value = 0;

        Matrix spectrum_mat = Matrix.Identity;

        sc_voxel voxel_cuber_r_hnd;
        sc_voxel voxel_cuber_l_hnd;
        sc_voxel voxel_cuber_l_up_arm;
        sc_voxel voxel_cuber_r_up_arm;
        sc_voxel voxel_cuber_l_low_arm;
        sc_voxel voxel_cuber_r_low_arm;
        sc_voxel voxel_cuber_l_shld;
        sc_voxel voxel_cuber_r_shld;
        sc_voxel voxel_cuber_l_targ;
        sc_voxel voxel_cuber_r_targ;
        sc_voxel voxel_cuber_l_targ_two;
        sc_voxel voxel_cuber_r_targ_two;
        sc_voxel voxel_cuber_pelvis;
        sc_voxel voxel_cuber_torso;

        sc_voxel.DInstanceType[] voxel_instancers_r_hnd;
        Matrix[] voxel_sometester_r_hnd;

        sc_voxel.DInstanceType[] voxel_instancers_l_hnd;
        Matrix[] voxel_sometester_l_hnd;

        sc_voxel.DInstanceType[] voxel_instancers_l_up_arm;
        Matrix[] voxel_sometester_l_up_arm;

        sc_voxel.DInstanceType[] voxel_instancers_r_up_arm;
        Matrix[] voxel_sometester_r_up_arm;

        sc_voxel.DInstanceType[] voxel_instancers_l_low_arm;
        Matrix[] voxel_sometester_l_low_arm;

        sc_voxel.DInstanceType[] voxel_instancers_r_low_arm;
        Matrix[] voxel_sometester_r_low_arm;

        sc_voxel.DInstanceType[] voxel_instancers_l_shld;
        Matrix[] voxel_sometester_l_shld;

        sc_voxel.DInstanceType[] voxel_instancers_r_shld;
        Matrix[] voxel_sometester_r_shld;

        sc_voxel.DInstanceType[] voxel_instancers_l_targ;
        Matrix[] voxel_sometester_l_targ;

        sc_voxel.DInstanceType[] voxel_instancers_r_targ;
        Matrix[] voxel_sometester_r_targ;

        sc_voxel.DInstanceType[] voxel_instancers_l_targ_two;
        Matrix[] voxel_sometester_l_targ_two;

        sc_voxel.DInstanceType[] voxel_instancers_r_targ_two;
        Matrix[] voxel_sometester_r_targ_two;

        sc_voxel.DInstanceType[] voxel_instancers_pelvis;
        Matrix[] voxel_sometester_pelvis;

        sc_voxel.DInstanceType[] voxel_instancers_torso;
        Matrix[] voxel_sometester_torso;



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

        //int[][] _switch_for_collision;
        int _has_init_ray;
        JMatrix _last_frame_rigid_grab_rot;
        Vector3 _last_frame_rigid_grab_pos;
        Matrix final_hand_pos_right_locked;
        Matrix final_hand_pos_left_locked;
        Vector3 _last_frame_handPos = Vector3.Zero;
        Vector3 _last_final_hand_pos_right;

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
        Vector3[][] point3DCollection;// = new Vector3[4];

        SharpDX.Matrix[][] _screenDirMatrix_correct_pos;

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
        SC_cube[] _terrain;

        Matrix _screen_swap = Matrix.Identity;

        SCCoreSystems.SC_Graphics.SC_jitter_cloth.DLightBuffer[] _DLightBufferSC_jitter_cloth = new SC_jitter_cloth.DLightBuffer[1];
       
        SCCoreSystems.SC_Graphics.SC_cube.DLightBuffer[] _DLightBuffer = new SC_cube.DLightBuffer[1];
        SCCoreSystems.SC_Graphics.sc_spectrum.DLightBuffer[] _DLightBuffer_spectrum = new sc_spectrum.DLightBuffer[1];
        SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer[] _DLightBuffer_voxel_cube = new sc_voxel.DLightBuffer[1];


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

        private SCCoreSystems._sc_camera Camera { get; set; }
        public static IntPtr Hwnd;
        SCCoreSystems.sc_console.sc_console_writer _currentWriter;
        static DirectInput directInput;
        private SCCoreSystems.SC_console_directx D3D { get; set; }

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

            /*//FRAME DELTATIME
            _delta_timer_frame = (float)Math.Abs((timeStopWatch01.Elapsed.Ticks - timeStopWatch00.Elapsed.Ticks)) * 100000000f;

            time2 = DateTime.Now;
            _delta_timer_time = (time2.Ticks - time1.Ticks) * 100000000f; //100000000f
            //time1 = time2;

            deltaTime = (float)Math.Abs(_delta_timer_time - _delta_timer_frame);
            */

            //FRAME DELTATIME
            _delta_timer_frame = (float)Math.Abs((timeStopWatch01.Elapsed.Ticks - timeStopWatch00.Elapsed.Ticks)) * 100000000f;

            time2 = DateTime.Now;
            _delta_timer_time = (time2.Ticks - time1.Ticks) * 100000000f; //100000000f
            //time1 = time2;

            deltaTime = (float)Math.Abs(_delta_timer_frame);



            //time1 = time2;
            await Task.Delay(1);
            Thread.Sleep(timeOut);
            _swtch_counter_00++;
            _swtch_counter_01++;
            _swtch_counter_02++;

            goto _threadLoop;
        }



        private bool RaycastCallback(RigidBody body, JVector normal, float fraction)
        {
            if (body.IsStatic) return false;
            else return true;
        }

        public SC_Console_GRAPHICS()
        {

        }


        DateTime startTime;

        public bool Initialize(SCCoreSystems.sc_core.sc_system_configuration configuration, IntPtr windowsHandle, sc_console.sc_console_writer _writer)
        {
            startTime = DateTime.Now;

            _this_thread_ticks_00.Start();
            _this_thread_ticks_01.Start();
            try
            {

                main_ai_2d_data_input = new SC_AI.data_input();

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





                _screenDirMatrix = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                _screenDirMatrix_correct_pos = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                point3DCollection = new Vector3[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                _world_screen_list = new SC_cube[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _world_screen_assets_list = new SC_cube[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                worldMatrix_instances_screens = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                world_last_Matrix_instances_screens = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                worldMatrix_instances_screen_assets = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];

                worldMatrix_instances_terrain = new Matrix[1][]; //1 terrain only
                _world_spectrum_list = new sc_spectrum[1];
                worldMatrix_instances_spectrum = new Matrix[1][];
                worldMatrix_instances_DZgrid = new Matrix[1][];

                _world_list = new World[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _world_cube_list = new SC_cube[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _world_terrain_tile_list = new SC_cube[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _world_voxel_cube_lists = new sc_voxel[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _terrain = new SC_cube[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];

                _world_jitter_cloth_list = new SC_jitter_cloth[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];

                worldMatrix_instances_jitter_cloth = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];


                worldMatrix_instances_cubes = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                worldMatrix_instances_voxel_cube = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                worldMatrix_instances_terrain_tiles = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                _array_of_colors = new Vector4[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];


                _array_of_last_frame_cube_pos = new Vector3[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                _array_of_last_frame_voxel_pos = new Vector3[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];


                swtch_for_last_pos = new int[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                tempMultiInstancePhysicsTotal = 1; // or _physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z

                //HUMAN RIG STUFF
                _player_rght_hnd = new sc_voxel[tempMultiInstancePhysicsTotal];
                _player_lft_upper_arm = new sc_voxel[tempMultiInstancePhysicsTotal];
                _player_lft_hnd = new sc_voxel[tempMultiInstancePhysicsTotal];
                _player_torso = new sc_voxel[tempMultiInstancePhysicsTotal];
                _player_pelvis = new sc_voxel[tempMultiInstancePhysicsTotal];
                _player_rght_shldr = new sc_voxel[tempMultiInstancePhysicsTotal];
                _player_lft_shldr = new sc_voxel[tempMultiInstancePhysicsTotal];
                _player_head = new sc_voxel[tempMultiInstancePhysicsTotal];
                _player_rght_lower_arm = new sc_voxel[tempMultiInstancePhysicsTotal];
                _player_lft_lower_arm = new sc_voxel[tempMultiInstancePhysicsTotal];
                _player_rght_upper_arm = new sc_voxel[tempMultiInstancePhysicsTotal];
                _player_rght_elbow_target = new sc_voxel[tempMultiInstancePhysicsTotal];
                _player_lft_elbow_target = new sc_voxel[tempMultiInstancePhysicsTotal];
                _player_rght_elbow_target_two = new sc_voxel[tempMultiInstancePhysicsTotal];
                _player_lft_elbow_target_two = new sc_voxel[tempMultiInstancePhysicsTotal];

                worldMatrix_instances_r_elbow_target = new Matrix[tempMultiInstancePhysicsTotal][];
                worldMatrix_instances_l_elbow_target = new Matrix[tempMultiInstancePhysicsTotal][];

                worldMatrix_instances_r_elbow_target_two = new Matrix[tempMultiInstancePhysicsTotal][];
                worldMatrix_instances_l_elbow_target_two = new Matrix[tempMultiInstancePhysicsTotal][];

                worldMatrix_instances_head = new Matrix[tempMultiInstancePhysicsTotal][];
                worldMatrix_instances_torso = new Matrix[tempMultiInstancePhysicsTotal][];
                worldMatrix_instances_pelvis = new Matrix[tempMultiInstancePhysicsTotal][];

                worldMatrix_instances_r_hand = new Matrix[tempMultiInstancePhysicsTotal][];
                worldMatrix_instances_l_hand = new Matrix[tempMultiInstancePhysicsTotal][];

                worldMatrix_instances_r_shoulder = new Matrix[tempMultiInstancePhysicsTotal][];
                worldMatrix_instances_l_shoulder = new Matrix[tempMultiInstancePhysicsTotal][];

                worldMatrix_instances_r_upperarm = new Matrix[tempMultiInstancePhysicsTotal][];
                worldMatrix_instances_l_upperarm = new Matrix[tempMultiInstancePhysicsTotal][];

                worldMatrix_instances_r_lowerarm = new Matrix[tempMultiInstancePhysicsTotal][];
                worldMatrix_instances_l_lowerarm = new Matrix[tempMultiInstancePhysicsTotal][];

                worldMatrix_instances_r_upperleg = new Matrix[tempMultiInstancePhysicsTotal][];
                worldMatrix_instances_l_upperleg = new Matrix[tempMultiInstancePhysicsTotal][];

                worldMatrix_instances_r_lowerleg = new Matrix[tempMultiInstancePhysicsTotal][];
                worldMatrix_instances_l_lowerleg = new Matrix[tempMultiInstancePhysicsTotal][];

                worldMatrix_instances_r_foot = new Matrix[tempMultiInstancePhysicsTotal][];
                worldMatrix_instances_l_foot = new Matrix[tempMultiInstancePhysicsTotal][];



                //RAYCAST STUFF
                //_some_reset_for_applying_force = new int[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                _some_frame_counter_raycast_00 = new int[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                _some_frame_counter_raycast_01 = new int[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                //_some_last_frame_vector = new JVector[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][][];
                //_some_last_frame_rigibodies = new RigidBody[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][][];

                Camera = new _sc_camera();

                _shaderManager = new DShaderManager();
                _shaderManager.Initialize(D3D.Device, windowsHandle);



                //draw_dcontainmentgrid = new int[6 * 6 * 6];

                for (int i = 0;i < draw_dcontainmentgrid.Length;i++)
                {
                    draw_dcontainmentgrid[i] = 0;
                }





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








                            try
                            {
                                //PHYSICS VOXEL SPHEROID
                                r = 0.75f;
                                g = 0.75f;
                                b = 0.75f;
                                a = 1;

                                /* WHITE
                                r = 0.85f;
                                g = 0.85f;
                                b = 0.85f;
                                a = 1;*/


                                _object_worldmatrix = Matrix.Identity;

                                offsetPosX = _voxel_cube_size_x * ((20 + 19) * voxel_general_size * 1.15f); //x between each world instance
                                offsetPosY = _voxel_cube_size_y * ((20 + 19) * voxel_general_size * 1.15f); //y between each world instance
                                offsetPosZ = _voxel_cube_size_z * ((20 + 19) * voxel_general_size * 1.15f); //z between each world instance

                                _offsetPos = new Vector3(0, 0, 0);

                                _object_worldmatrix = WorldMatrix;

                                _object_worldmatrix.M41 = 0 + x;
                                _object_worldmatrix.M42 = 1 + y;
                                _object_worldmatrix.M43 = 0 + z;

                                _object_worldmatrix.M44 = 1;

                                //_object_worldmatrix.M41 += _offsetPos.X;
                                //_object_worldmatrix.M42 += _offsetPos.Y;
                                //_object_worldmatrix.M43 += _offsetPos.Z;

                                //offsetPosX = _voxel_cube_size_x * 10;
                                //offsetPosY = _voxel_cube_size_y * 10;
                                //offsetPosZ = _voxel_cube_size_z * 10;

                                var sc_voxel_spheroid = new sc_voxel();

                                voxel_general_size = 0.0015f;
                                voxel_type = 1;
                                is_static = false;
                                _voxel_mass = 100;
                                var _hasinit00 = sc_voxel_spheroid.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0, 1, 1, 1, _voxel_cube_size_x, _voxel_cube_size_y, _voxel_cube_size_z, new Vector4(r, g, b, a), _inst_voxel_cube_x, _inst_voxel_cube_y, _inst_voxel_cube_z, Hwnd, _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, _voxel_mass, is_static, SC_console_directx.BodyTag._voxel_spheroid, 9, 9, 9, 9, 9, 9, 20, 19, 20, 19, 20, 19, voxel_general_size, Vector3.Zero, 17, 0, 0, 0, 2, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                                //FOR CUBES AND SET TO voxel_type = 1
                                //var _hasinit00 = sc_voxel_spheroid.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, _voxel_cube_size_x, _voxel_cube_size_y, _voxel_cube_size_z, new Vector4(r, g, b, a), _inst_voxel_cube_x, _inst_voxel_cube_y, _inst_voxel_cube_z, Hwnd, _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, _voxel_mass, is_static, SC_console_directx.BodyTag._voxel_spheroid, 2, 2, 2, 2, 2, 2, 20, 19, 20, 19, 20, 19, voxel_general_size, Vector3.Zero, 250, 0, 0, 0, 2, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                                //FOR CUBES AND SET TO voxel_type = 1

                                _world_voxel_cube_lists[_index] = sc_voxel_spheroid;

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
                            /*r = 0.35f;
                            g = 0.35f;
                            b = 0.95f;
                            a = 1;*/

                            r = 0.75f;
                            g = 0.15f;
                            b = 0;
                            a = 1;


                            //violet/pinky/purple kinda 
                            //r = 0.85f;
                            //g = 0.45f;
                            //b = 0.65f;
                            //a = 1;

                            _object_worldmatrix = Matrix.Identity;

                            offsetPosX = _cube_size_x * 2.15f; //x between each world instance
                            offsetPosY = _cube_size_y * 2.15f; //y between each world instance
                            offsetPosZ = _cube_size_z * 2.15f; //z between each world instance

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

                            //var _sizeX *= 0.00030f;
                            //var _sizeY *= 0.00030f;
                            //var _sizeZ *= 0.0025f;

                            _cube = new SC_cube();

                            var _hasinit3 = _cube.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0.1f, 1, 1, _cube_size_x, _cube_size_y, _cube_size_z, new Vector4(r, g, b, a), _inst_cube_x, _inst_cube_y, _inst_cube_z, Hwnd, _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.physicsInstancedCube, false, 1, 100, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

                            _world_cube_list[_index] = _cube;

                            worldMatrix_instances_cubes[_index] = new Matrix[_inst_cube_x * _inst_cube_y * _inst_cube_z];

                            for (int i = 0; i < worldMatrix_instances_cubes[_index].Length; i++)
                            {
                                worldMatrix_instances_cubes[_index][i] = Matrix.Identity;
                            }









                            //PHYSICS _jitter_cloth
                            //SET TO 0 AND YOU HAVE USE A SHADERRESOURCE INSTEAD for the texture instead of using the color. cheap way for the moment as my switch wasnt working.
                            /*r = 0.35f;
                            g = 0.35f;
                            b = 0.95f;
                            a = 1;*/

                            r = 0.75f;
                            g = 0.15f;
                            b = 0;
                            a = 1;


                            //violet/pinky/purple kinda 
                            //r = 0.85f;
                            //g = 0.45f;
                            //b = 0.65f;
                            //a = 1;

                            _object_worldmatrix = Matrix.Identity;

                            offsetPosX = 0.045f * 2.15f; //x between each world instance
                            offsetPosY = 0.0035f * 2.15f; //y between each world instance
                            offsetPosZ = 0.045f * 2.15f; //z between each world instance

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

                            //var _sizeX *= 0.00030f;
                            //var _sizeY *= 0.00030f;
                            //var _sizeZ *= 0.0025f;

                            var _jitter_cloth = new SC_jitter_cloth();

                            //clothRect.Initialize(D3D.device, 0.045f, 0.0035f, 0.045f, new Vector4(1, 0.1f, 0.1f, 1), 10, 1, 10, posX, posY, posZ);
                            //_jitter_cloth.Initialize(D3D.device, 0.045f, 0.0035f, 0.045f, new Vector4(1, 0.1f, 0.1f, 1), 10, 10, 10, 10, 10, 10, _WorldMatrix);
                            _jitter_cloth.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0.1f, 1, 1, 0.045f, 0.0035f, 0.045f, new Vector4(r, g, b, a), _inst_jitter_cloth_x, _inst_jitter_cloth_y, _inst_jitter_cloth_z, Hwnd, _object_worldmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.sc_jitter_cloth, false, 1, 100, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                            _world_jitter_cloth_list[_index] = _jitter_cloth;

                            worldMatrix_instances_jitter_cloth[_index] = new Matrix[_inst_jitter_cloth_x * _inst_jitter_cloth_y * _inst_jitter_cloth_z];

                            for (int i = 0; i < worldMatrix_instances_jitter_cloth[_index].Length; i++)
                            {
                                worldMatrix_instances_jitter_cloth[_index][i] = Matrix.Identity;
                            }





                            






                            _some_frame_counter_raycast_00[_index] = new int[_inst_cube_x * _inst_cube_y * _inst_cube_z];
                            _some_frame_counter_raycast_01[_index] = new int[_inst_cube_x * _inst_cube_y * _inst_cube_z];
                            _array_of_last_frame_cube_pos[_index] = new Vector3[_inst_cube_x * _inst_cube_y * _inst_cube_z];
                            _array_of_last_frame_voxel_pos[_index] = new Vector3[_inst_cube_x * _inst_cube_y * _inst_cube_z];


                            swtch_for_last_pos[_index] = new int[_inst_cube_x * _inst_cube_y * _inst_cube_z];
                            for (int i = 0; i < _some_frame_counter_raycast_00[_index].Length; i++)
                            {
                                swtch_for_last_pos[_index][i] = 0;
                                _array_of_last_frame_cube_pos[_index][i] = Vector3.Zero;
                                _array_of_last_frame_voxel_pos[_index][i] = Vector3.Zero;


                                var _randNum = new Random();
                                var randomer = _randNum.NextDouble(0, _some_frame_counter_raycast_00_max_counter);
                                _some_frame_counter_raycast_00[_index][i] = (int)randomer;

                                _randNum = new Random();
                                randomer = _randNum.NextDouble(0, _some_frame_counter_raycast_01_max_counter);
                                _some_frame_counter_raycast_01[_index][i] = (int)randomer;
                            }













                        }
                    }
                }
                World = _world_list[0];



             


                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;

                var tempr = r;
                var tempg = g;
                var tempb = b;
                var tempa = a;


                //HUMAN PHYSICS RIG
                tempIndex = 0;

                float vertoffsetx = 0;
                float vertoffsety = 0;
                float vertoffsetz = -(16 + 15) * 0.015f;// - 0.25f;;

                float _dist_between = 0.30f;

                ///////////////////////////////
                ///////////HUMAN RIG///////////
                ///////////////////////////////

                voxel_type = 1;
                //PLAYER RIGHT HAND
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
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


                _player_rght_hnd[tempIndex] = new sc_voxel();
                voxel_general_size = 0.0025f;
                //voxel_type = 0;
                _type_of_cube = 2;
                is_static = true;
                //_player_rght_hnd[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerHandRight, _static, 1, _mass, 0, 0, -0.75f); //, "terrainGrassDirt.bmp" //0.00035f
                _player_rght_hnd[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, SC_console_directx.BodyTag.PlayerHandRight, 9, 9, 9, 18, 9, 9, 4, 3, 13, 12, 18, 17, voxel_general_size, new Vector3(0, 0, -0.1f), 75, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_r_hand[tempIndex] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
                for (int i = 0; i < worldMatrix_instances_r_hand[tempIndex].Length; i++)
                {
                    worldMatrix_instances_r_hand[tempIndex][i] = Matrix.Identity;
                }

                //PLAYER LEFT HAND
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;


                _tempMatroxer = Matrix.Identity;

                _tempMatroxer = _WorldMatrix;

                _tempMatroxer.M41 = 0;
                _tempMatroxer.M42 = 0;
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;


                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;

                _mass = 100;
                //_player_lft_hnd[tempIndex] = new sc_voxel();
                //_hasinit0 = _player_lft_hnd[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_l_hand_x, _inst_p_l_hand_y, _inst_p_l_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerHandLeft, _static, 1, _mass, 0, 0, -0.75f); //, "terrainGrassDirt.bmp" //0.00035f

                _player_lft_hnd[tempIndex] = new sc_voxel();
                voxel_general_size = 0.0025f;
                //voxel_type = 0;
                _type_of_cube = 2;

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
                is_static = true;
                _player_lft_hnd[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, SC_console_directx.BodyTag.PlayerHandLeft, 9, 9, 9, 18, 9, 9, 4, 3, 13, 12, 18, 17, voxel_general_size, new Vector3(0, 0, -0.1f), 75, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_l_hand[tempIndex] = new Matrix[_inst_p_l_hand_x * _inst_p_l_hand_y * _inst_p_l_hand_z];
                for (int i = 0; i < worldMatrix_instances_l_hand[tempIndex].Length; i++)
                {
                    worldMatrix_instances_l_hand[tempIndex][i] = Matrix.Identity;
                }



                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                //TORSO
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;



                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = _WorldMatrix;

                _tempMatroxer.M41 = 0 + x;
                _tempMatroxer.M42 = -0.35f; // -0.1f
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;

                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;

                //_player_torso[tempIndex] = new sc_voxel();
                //_hasinit0 = _player_torso.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f                                                                                                                                                                                                                                                                                        //_hasinit0 = _player_torso.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_player_torso[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), _inst_p_torso_x, _inst_p_torso_y, _inst_p_torso_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerTorso, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = 0.0025f;
                //voxel_type = 0;
                _type_of_cube = 2;
                _mass = 100;
                _player_torso[tempIndex] = new sc_voxel();
                _player_torso[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0, 1, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), _inst_p_torso_x, _inst_p_torso_y, _inst_p_torso_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, SC_console_directx.BodyTag.PlayerTorso, 2, 9, 2, 2, 2, 2, 45, 44, 60, 59, 10, 9, voxel_general_size, new Vector3(0, 0, 0), 500, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                //_player_torso[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), _inst_p_torso_x, _inst_p_torso_y, _inst_p_torso_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerTorso, 2, 9, 2, 2, 2, 2, 45, 44, 60, 59, 10, 9, 0.0025f, new Vector3(0, 0, 0), 500); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_torso[tempIndex] = new Matrix[_inst_p_torso_x * _inst_p_torso_y * _inst_p_torso_z];
                for (int i = 0; i < worldMatrix_instances_torso[tempIndex].Length; i++)
                {
                    worldMatrix_instances_torso[tempIndex][i] = Matrix.Identity;
                }


                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                //PELVIS
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;


                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = _WorldMatrix;

                _tempMatroxer.M41 = 0;
                _tempMatroxer.M42 = -0.625f;
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;

                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;


                _mass = 100;
                //_player_pelvis[tempIndex] = new sc_voxel();
                //_hasinit0 = _player_pelvis.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 9, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_pelvis[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerPelvis, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = 0.0025f;
                //voxel_type = 0;
                _type_of_cube = 2;
                _mass = 100;
                _player_pelvis[tempIndex] = new sc_voxel();
                //_player_pelvis[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerPelvis, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                _player_pelvis[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0, 1, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), _inst_p_pelvis_x, _inst_p_pelvis_y, _inst_p_pelvis_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, SC_console_directx.BodyTag.PlayerPelvis, 2, 9, 2, 2, 2, 2, 45, 44, 24, 23, 10, 9, voxel_general_size, Vector3.Zero, 150, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_pelvis[tempIndex] = new Matrix[_inst_p_pelvis_x * _inst_p_pelvis_y * _inst_p_pelvis_z];
                for (int i = 0; i < worldMatrix_instances_pelvis[tempIndex].Length; i++)
                {
                    worldMatrix_instances_pelvis[tempIndex][i] = Matrix.Identity;
                }












                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                //PLAYER RIGHT SHOULDER
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;

                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = _WorldMatrix;
                _tempMatroxer.M41 = 0.15f;
                _tempMatroxer.M42 = -0.2f;
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_rght_shldr[tempIndex] = new sc_voxel();
                //_hasinit0 = _player_rght_shldr.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 9, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_rght_shldr[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_shoulder_x, _inst_p_r_shoulder_y, _inst_p_r_shoulder_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerShoulderRight, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                _mass = 100;
                voxel_general_size = 0.0025f;
                //voxel_type = 0;
                _type_of_cube = 2;

                _player_rght_shldr[tempIndex] = new sc_voxel();
                //_player_rght_shldr[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerShoulderRight, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                _player_rght_shldr[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_l_shoulder_x, _inst_p_l_shoulder_y, _inst_p_l_shoulder_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, SC_console_directx.BodyTag.PlayerShoulderRight, 9, 9, 9, 9, 9, 9, 20, 19, 20, 19, 20, 19, voxel_general_size, new Vector3(0, 0, 0), 17, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f


                worldMatrix_instances_r_shoulder[tempIndex] = new Matrix[_inst_p_r_shoulder_x * _inst_p_r_shoulder_y * _inst_p_r_shoulder_z];
                for (int i = 0; i < worldMatrix_instances_r_shoulder[tempIndex].Length; i++)
                {
                    worldMatrix_instances_r_shoulder[tempIndex][i] = Matrix.Identity;
                }



                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                //PLAYER LEFT SHOULDER
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;

                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = _WorldMatrix;
                _tempMatroxer.M41 = -0.15f;
                _tempMatroxer.M42 = -0.2f;
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                _mass = 100;
                voxel_general_size = 0.0025f;
                //voxel_type = 0;
                _type_of_cube = 2;
                //_player_lft_shldr[tempIndex] = new sc_voxel();
                //_hasinit0 = _player_lft_shldr.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 9, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_lft_shldr[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_l_shoulder_x, _inst_p_l_shoulder_y, _inst_p_l_shoulder_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerShoulderLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

                _player_lft_shldr[tempIndex] = new sc_voxel();
                //_player_lft_shldr[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerShoulderLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                _player_lft_shldr[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_l_shoulder_x, _inst_p_l_shoulder_y, _inst_p_l_shoulder_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, SC_console_directx.BodyTag.PlayerShoulderLeft, 9, 9, 9, 9, 9, 9, 20, 19, 20, 19, 20, 19, voxel_general_size, new Vector3(0, 0, 0), 17, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_l_shoulder[tempIndex] = new Matrix[_inst_p_l_shoulder_x * _inst_p_l_shoulder_y * _inst_p_l_shoulder_z];
                for (int i = 0; i < worldMatrix_instances_l_shoulder[tempIndex].Length; i++)
                {
                    worldMatrix_instances_l_shoulder[tempIndex][i] = Matrix.Identity;
                }







                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
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
                //_player_head[tempIndex] = new sc_voxel();
                //_hasinit0 = _player_head.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_head[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_l_shoulder_x, _inst_p_l_shoulder_y, _inst_p_l_shoulder_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerHead, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

                _player_head[tempIndex] = new sc_voxel();
                _player_head[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerHead, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f


                worldMatrix_instances_head[tempIndex] = new Matrix[_inst_p_l_shoulder_x * _inst_p_l_shoulder_y * _inst_p_l_shoulder_z];
                for (int i = 0; i < worldMatrix_instances_head[tempIndex].Length; i++)
                {
                    worldMatrix_instances_head[tempIndex][i] = Matrix.Identity;
                }*/


                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                //LEFT LOWER ARM
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;


                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = _WorldMatrix;
                _tempMatroxer.M41 = -0.25f;
                _tempMatroxer.M42 = -0.15f;
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;

                //_player_lft_lower_arm[tempIndex] = new sc_voxel();
                //_hasinit0 = _player_lft_lower_arm.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_lft_lower_arm[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_l_lowerarm_x, _inst_p_l_lowerarm_y, _inst_p_l_lowerarm_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerLowerArmLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = 0.0025f;
                //voxel_type = 0;
                _type_of_cube = 2;
                _player_lft_lower_arm[tempIndex] = new sc_voxel();
                // _player_lft_lower_arm[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerLowerArmLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                //_player_lft_lower_arm[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, SC_console_directx.BodyTag.PlayerLowerArmLeft, 9, 9, 9, 18, 13, 9, 9, 10, 33, 32, 11, 10, voxel_general_size, new Vector3(0, 0, 0), 74, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                _player_lft_lower_arm[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, SC_console_directx.BodyTag.PlayerLowerArmLeft, 9, 9, 9, 18, 13, 13, 10, 9, 33, 32, 11, 10, voxel_general_size, new Vector3(0, 0, 0), 75, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f



                //FOR VOXEL ARROW
                //_player_lft_lower_arm[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerLowerArmLeft, 9, 9, 9, 18, 17, 100, 3, 10, 33, 32, 11, 10, voxel_general_size, new Vector3(0, 0, 0), 70, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                //

                worldMatrix_instances_l_lowerarm[tempIndex] = new Matrix[_inst_p_l_lowerarm_x * _inst_p_l_lowerarm_y * _inst_p_l_lowerarm_z];
                for (int i = 0; i < worldMatrix_instances_l_lowerarm[tempIndex].Length; i++)
                {
                    worldMatrix_instances_l_lowerarm[tempIndex][i] = Matrix.Identity;
                }




                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                //LEFT UPPER ARM
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;

                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = _WorldMatrix;
                _tempMatroxer.M41 = -0.25f;
                _tempMatroxer.M42 = -0.375f;
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_lft_upper_arm[tempIndex] = new sc_voxel();
                //_hasinit0 = _player_lft_upper_arm.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_lft_upper_arm[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerUpperArmLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = 0.0025f;
                //voxel_type = 0;
                _type_of_cube = 2;
                _player_lft_upper_arm[tempIndex] = new sc_voxel();
                //_player_lft_upper_arm[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerUpperArmLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                _player_lft_upper_arm[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, SC_console_directx.BodyTag.PlayerUpperArmLeft, 9, 9, 9, 18, 13, 13, 10, 9, 33, 32, 11, 10, voxel_general_size, new Vector3(0, 0, 0), 75, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f



                worldMatrix_instances_l_upperarm[tempIndex] = new Matrix[_inst_p_r_upperarm_x * _inst_p_r_upperarm_y * _inst_p_r_upperarm_z];
                for (int i = 0; i < worldMatrix_instances_l_upperarm[tempIndex].Length; i++)
                {
                    worldMatrix_instances_l_upperarm[tempIndex][i] = Matrix.Identity;
                }









                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                //RIGHT LOWER ARM
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;


                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = _WorldMatrix;
                _tempMatroxer.M41 = -0.25f;
                _tempMatroxer.M42 = -0.15f;
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;

                //_player_lft_lower_arm[tempIndex] = new sc_voxel();
                //_hasinit0 = _player_lft_lower_arm.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_lft_lower_arm[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_l_lowerarm_x, _inst_p_l_lowerarm_y, _inst_p_l_lowerarm_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerLowerArmLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = 0.0025f;
                //voxel_type = 0;
                _type_of_cube = 2;
                _player_rght_lower_arm[tempIndex] = new sc_voxel();
                // _player_lft_lower_arm[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerLowerArmLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                _player_rght_lower_arm[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, SC_console_directx.BodyTag.PlayerLowerArmRight, 9, 9, 9, 18, 13, 13, 10, 9, 33, 32, 11, 10, voxel_general_size, new Vector3(0, 0, 0), 75, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                //_player_rght_lower_arm[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, SC_console_directx.BodyTag.PlayerLowerArmRight, 7, 7, 9, 18, 21, 25, 10, 9, 33, 32, 11, 10, voxel_general_size, new Vector3(0, 0, 0), 72, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f



                //FOR VOXEL ARROW
                //_player_lft_lower_arm[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerLowerArmLeft, 9, 9, 9, 18, 17, 100, 3, 10, 33, 32, 11, 10, voxel_general_size, new Vector3(0, 0, 0), 70, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                //

                worldMatrix_instances_r_lowerarm[tempIndex] = new Matrix[_inst_p_r_lowerarm_x * _inst_p_r_lowerarm_y * _inst_p_r_lowerarm_z];
                for (int i = 0; i < worldMatrix_instances_r_lowerarm[tempIndex].Length; i++)
                {
                    worldMatrix_instances_r_lowerarm[tempIndex][i] = Matrix.Identity;
                }




                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                //RIGHT UPPER ARM
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;


                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = _WorldMatrix;
                _tempMatroxer.M41 = 0.25f;
                _tempMatroxer.M42 = -0.375f;
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_rght_upper_arm[tempIndex] = new sc_voxel();
                //_hasinit0 = _player_rght_upper_arm.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_rght_upper_arm[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_upperarm_x, _inst_p_r_upperarm_y, _inst_p_r_upperarm_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerUpperArmRight, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = 0.0025f;
                //voxel_type = 0;
                _type_of_cube = 2;
                _player_rght_upper_arm[tempIndex] = new sc_voxel();
                //_player_rght_upper_arm[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SC_console_directx.BodyTag.PlayerUpperArmRight, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                _player_rght_upper_arm[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0, 1, 1, 1, 0.035f, 0.1055f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, SC_console_directx.BodyTag.PlayerUpperArmRight, 9, 9, 9, 18, 13, 13, 10, 9, 33, 32, 11, 10, voxel_general_size, new Vector3(0, 0, 0), 75, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f



                worldMatrix_instances_r_upperarm[tempIndex] = new Matrix[_inst_p_r_upperarm_x * _inst_p_r_upperarm_y * _inst_p_r_upperarm_z];
                for (int i = 0; i < worldMatrix_instances_r_upperarm[tempIndex].Length; i++)
                {
                    worldMatrix_instances_r_upperarm[tempIndex][i] = Matrix.Identity;
                }





                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                //RIGHT ELBOW TARGET
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;


                //SHOULDER RIGHT
                //_tempMatroxer.M41 = -0.25f; /
                //_tempMatroxer.M42 = -0.2f;

                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = _WorldMatrix;
                _tempMatroxer.M41 = 0.25f;
                _tempMatroxer.M42 = (_player_rght_upper_arm[tempIndex]._POSITION.M42 + (_player_rght_upper_arm[tempIndex]._total_torso_height * 0.5f) + 0.45f);// - 0.25f;
                _tempMatroxer.M43 = -0.25f;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_rght_elbow_target[tempIndex] = new sc_voxel();

                //_hasinit0 = _player_rght_elbow_target.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_rght_elbow_target[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = 0.0025f;
                //voxel_type = 0;
                _type_of_cube = 2;
                _player_rght_elbow_target[tempIndex] = new sc_voxel();
                _player_rght_elbow_target[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, SC_console_directx.BodyTag.PlayerRightElbowTarget, 2, 2, 2, 2, 2, 2, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 25, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f


                worldMatrix_instances_r_elbow_target[tempIndex] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
                for (int i = 0; i < worldMatrix_instances_r_elbow_target[tempIndex].Length; i++)
                {
                    worldMatrix_instances_r_elbow_target[tempIndex][i] = Matrix.Identity;
                }



                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                //LEFT ELBOW TARGET
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;

                //SHOULDER RIGHT
                //_tempMatroxer.M41 = -0.25f; /
                //_tempMatroxer.M42 = -0.2f;

                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = _WorldMatrix;
                _tempMatroxer.M41 = -0.25f;
                _tempMatroxer.M42 = (_player_lft_upper_arm[tempIndex]._POSITION.M42 + (_player_lft_upper_arm[tempIndex]._total_torso_height * 0.5f) + 0.45f);// - 0.25f;
                _tempMatroxer.M43 = -0.25f;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_lft_elbow_target[tempIndex] = new sc_voxel();
                //_hasinit0 = _player_lft_elbow_target.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_lft_elbow_target[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_l_hand_x, _inst_p_l_hand_y, _inst_p_l_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerLeftElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = 0.0025f;
                //voxel_type = 0;
                _type_of_cube = 2;
                _player_lft_elbow_target[tempIndex] = new sc_voxel();
                _player_lft_elbow_target[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, SC_console_directx.BodyTag.PlayerLeftElbowTarget, 2, 2, 2, 2, 2, 2, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 25, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f



                worldMatrix_instances_l_elbow_target[tempIndex] = new Matrix[_inst_p_l_hand_x * _inst_p_l_hand_y * _inst_p_l_hand_z];
                for (int i = 0; i < worldMatrix_instances_l_elbow_target[tempIndex].Length; i++)
                {
                    worldMatrix_instances_l_elbow_target[tempIndex][i] = Matrix.Identity;
                }








                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                //RIGHT ELBOW TARGET TWO
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;

                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = _WorldMatrix;
                _tempMatroxer.M41 = 1.5f;
                _tempMatroxer.M42 = (_player_rght_upper_arm[tempIndex]._POSITION.M42 + (_player_rght_upper_arm[tempIndex]._total_torso_height * 0.5f) + 1);
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_rght_elbow_target_two[tempIndex] = new sc_voxel();
                //_hasinit0 = _player_rght_elbow_target_two.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_rght_elbow_target_two[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = 0.0025f;
                //voxel_type = 0;
                _type_of_cube = 2;
                _player_rght_elbow_target_two[tempIndex] = new sc_voxel();
                _player_rght_elbow_target_two[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, SC_console_directx.BodyTag.PlayerRightElbowTargettwo, 2, 2, 2, 2, 2, 2, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 75, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f


                worldMatrix_instances_r_elbow_target_two[tempIndex] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
                for (int i = 0; i < worldMatrix_instances_r_elbow_target_two[tempIndex].Length; i++)
                {
                    worldMatrix_instances_r_elbow_target_two[tempIndex][i] = Matrix.Identity;
                }






                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                //LEFT ELBOW TARGET TWO
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;


                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = _WorldMatrix;
                _tempMatroxer.M41 = -1.5f;
                _tempMatroxer.M42 = (_player_lft_upper_arm[tempIndex]._POSITION.M42 + (_player_lft_upper_arm[tempIndex]._total_torso_height * 0.5f) + 1);
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_lft_elbow_target_two[tempIndex] = new sc_voxel();

                //_hasinit0 = _player_lft_elbow_target_two.Initialize(_D3D, _D3D.SurfaceWidth, _D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_lft_elbow_target_two[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.PlayerLeftElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = 0.0025f;
                //voxel_type = 0;
                _type_of_cube = 2;
                _player_lft_elbow_target_two[tempIndex] = new sc_voxel();
                _player_lft_elbow_target_two[tempIndex].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, SC_console_directx.BodyTag.PlayerLeftElbowTargettwo, 2, 2, 2, 2, 2, 2, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 75, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_l_elbow_target_two[tempIndex] = new Matrix[_inst_p_l_hand_x * _inst_p_l_hand_y * _inst_p_l_hand_z];
                for (int i = 0; i < worldMatrix_instances_l_elbow_target_two[tempIndex].Length; i++)
                {
                    worldMatrix_instances_l_elbow_target_two[tempIndex][i] = Matrix.Identity;
                }
















































                var sc_spectrum = new sc_spectrum();
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = _WorldMatrix;
                _tempMatroxer.M41 = 0;
                _tempMatroxer.M42 = 0;// - 0.25f;
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                float spec_inst_dist = 1;
                //PHYSICS SPECTRUM
                r = 0.75f;
                g = 0.35f;
                b = 0.05f;
                a = 1;

                offsetPosX = _spectrum_size_x * 2; //x between each world instance
                offsetPosY = _spectrum_size_y * 2; //y between each world instance
                offsetPosZ = _spectrum_size_z * 2; //z between each world instance

                //float vertoffsetx = 0;
                //float vertoffsety = 0;
                //float vertoffsetz = 0;// - 0.25f;;

                //offsetPosX = spec_inst_dist * 2;
                //offsetPosY = spec_inst_dist * 2;
                //offsetPosZ = spec_inst_dist * 2;

                var _hasinit2 = sc_spectrum.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, _spectrum_size_x, _spectrum_size_y, _spectrum_size_z, new Vector4(r, g, b, a), _inst_spectrum_x, _inst_spectrum_y, _inst_spectrum_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, true); //, "terrainGrassDirt.bmp" //0.00035f
                _world_spectrum_list[0] = sc_spectrum;

                worldMatrix_instances_spectrum[0] = new Matrix[_inst_spectrum_x * _inst_spectrum_y * _inst_spectrum_z];

                for (int i = 0; i < worldMatrix_instances_spectrum[0].Length; i++)
                {
                    worldMatrix_instances_spectrum[0][i] = Matrix.Identity;
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


















                ////////////////////////////
                ////////////////////////////
                ////////////////////////////
                //////////TERRAIN///////////
                ////////////////////////////
                ////////////////////////////
                ////////////////////////////
                _inst_terrain_tile_x = 1;
                _inst_terrain_tile_y = 1;
                _inst_terrain_tile_z = 1;

                //worldMatrx_instances_terrain[0] = new Matrix[1];
                //worldMatrix_instances_terrain[0][0] = Matrix.Identity;
                r = 0.45f;
                g = 0.45f;
                b = 0.45f;
                a = 1.0f;


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

                _terrain[0] = new SC_cube();
                _terrain[0].Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0.1f, 1, 1, _terrain_size_x, _terrain_size_y, _terrain_size_z, new Vector4(r, g, b, a), _inst_terrain_tile_x, _inst_terrain_tile_y, _inst_terrain_tile_z, Hwnd, _object_worldmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, _world_list[0], SC_console_directx.BodyTag.Terrain, true, 0, 10, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                _world_terrain = _terrain[0];
                worldMatrix_instances_terrain[0] = new Matrix[_inst_terrain_tile_x * _inst_terrain_tile_y * _inst_terrain_tile_z];
                for (int i = 0; i < worldMatrix_instances_terrain[0].Length; i++)
                {
                    worldMatrix_instances_terrain[0][i] = _terrain[0]._arrayOfInstances[0]._POSITION;
                }



                _size_screen = 0.0006f;
                //CURRENT GRID SETUP
                _grid = new SC_DRGrid();
                _grid.Initialize(D3D.device, _grid_size_x, _grid_size_z, 1);


                Matrix dcontainPos = Matrix.Identity;

                int sizeX = (int)1;
                int sizeY = (int)(((D3D.SurfaceHeight * 0.0125f)) * _screen_size_y);// (int)(D3D.SurfaceHeight * _screen_size_y * 0.015f);
                int sizeZ = (int)(((D3D.SurfaceWidth * 0.0125f)) * _screen_size_x);//(int)(D3D.SurfaceWidth * _screen_size_x * 0.015f);

                _dContainer = new DContainmentGrid();
                _dContainer.Initialize(D3D.device, sizeX, sizeY, sizeZ, 0.01225f, 0, 0, 0, dcontainPos);








                dcontainPos = Matrix.Identity;
                _dTouchRightContainer = new DContainmentGrid();
                _dTouchRightContainer.Initialize(D3D.device, 3, 2, 2, 0.015f, 0, 0, 0, dcontainPos);

                dcontainPos = Matrix.Identity;
                _dTouchLeftContainer = new DContainmentGrid();
                _dTouchLeftContainer.Initialize(D3D.device, 3, 2, 2, 0.015f, 0, 0, 0, dcontainPos);




                _arrayOfCubes = new SC_VR_Cube();//[6 * 6 * 6];
                _arrayOfCubes.Initialize(D3D.device, 0.05f, 0.05f, 0.05f, new Vector4(0.1f, 0.1f, 1f, 1), ChunkWidth_L, ChunkWidth_R, ChunkHeight_L, ChunkHeight_R, ChunkDepth_L, ChunkDepth_R);


                Matrix IcoLightPos = Matrix.Identity;

                IcoLightPos.M42 = 5;

                 _icoSphere = new SC_VR_IcoSphere();
                _icoSphere.Initialize(D3D.device, 3, 3, 3, new Vector4(0.25f, 0.25f, 0.25f, 1), IcoLightPos);
                _icoVertexCount = _icoSphere.Vertices.Length;


                int _dvX = 10;
                int _dvY = 10;
                a = 1;
                r = 0.65f;
                g = 0.15f;
                b = 0.15f;
                _size_screen = 0.0006f;
                _screen_grid_Y = new DTerrain_Screen();
                _screen_grid_Y.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, _size_screen, _dvX, _dvY, a, r, g, b);


                _dvX = (int)(D3D.SurfaceWidth * 0.5f * _screen_size_x * 0.01f * 0.55f);
                _dvY = (int)(D3D.SurfaceHeight * 0.5f * _screen_size_y * 0.01f);

                a = 1;
                r = 0.15f;
                g = 0.65f;
                b = 0.15f;
                _size_screen = 0.0006f;

                _screen_metric_grid_Y = new DTerrain_Screen_Metric();
                _screen_metric_grid_Y.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, _size_screen, _dvX, _dvY, a, r, g, b);









                //float posX = 0;
                //float posY = 2;
                //float posZ = 0;
                
                _WorldMatrix = WorldMatrix;
                _WorldMatrix.M41 = 0;
                _WorldMatrix.M42 = 0;
                _WorldMatrix.M43 = 0;


                try
                {


                    


                    /*clothRect.transform.Component.rigidbody.Tag = SC_console_directx.BodyTag.sc_jitter_cloth;
                    World.AddBody(clothRect.transform.Component.softbody);
                    clothRect.transform.Component.softbody.Pressure = 15; //0.00075f
                    /*clothRect._POSITION = _WorldMatrix;
                    
                    clothRect.transform.Component.softbody.Material.KineticFriction = 10;
                    clothRect.transform.Component.softbody.Material.StaticFriction = 10;
                    clothRect.transform.Component.softbody.Material.Restitution = 0;

                    clothRect.transform.Component.softbody.SetSpringValues(SoftBody.SpringType.EdgeSpring, 0.1f, 0.001f);
                    clothRect.transform.Component.softbody.SetSpringValues(SoftBody.SpringType.ShearSpring, 0.1f, 0.001f);
                    clothRect.transform.Component.softbody.SetSpringValues(SoftBody.SpringType.BendSpring, 0.1f, 0.001f);
                    //clothRect.transform.Component.softbody.SelfCollision = true;
                    var _vertexBodies = clothRect.transform.Component.softbody.VertexBodies;

                    for (int i = 0; i < _vertexBodies.Count; i++)
                    {
                        var singleVertexBody = _vertexBodies[i];
                        singleVertexBody.Mass = 0.5f;
                        //singleVertexBody.Position += new JVector(posX, posY, posZ);
                    }*/
                }
                catch (Exception ex)
                {
                    MessageBox((IntPtr)0, "" + ex.ToString(), "sc core systems message", 0);
                }






                for (int i = 0; i < _world_list.Length; i++)
                {
                    _world_list[i].AddBody(_terrain[0]._arrayOfInstances[0].transform.Component.rigidbody);

                    tempIndex = 0; //or = i
                    _world_list[i].AddBody(_player_rght_upper_arm[tempIndex]._arrayOfInstances[0].transform.Component.rigidbody);
                    _world_list[i].AddBody(_player_rght_lower_arm[tempIndex]._arrayOfInstances[0].transform.Component.rigidbody);

                    _world_list[i].AddBody(_player_lft_upper_arm[tempIndex]._arrayOfInstances[0].transform.Component.rigidbody);
                    _world_list[i].AddBody(_player_lft_lower_arm[tempIndex]._arrayOfInstances[0].transform.Component.rigidbody);

                    _world_list[i].AddBody(_player_pelvis[tempIndex]._arrayOfInstances[0].transform.Component.rigidbody);
                    _world_list[i].AddBody(_player_torso[tempIndex]._arrayOfInstances[0].transform.Component.rigidbody);

                    _world_list[i].AddBody(_player_lft_hnd[tempIndex]._arrayOfInstances[0].transform.Component.rigidbody);
                    _world_list[i].AddBody(_player_rght_hnd[tempIndex]._arrayOfInstances[0].transform.Component.rigidbody);

                    _world_list[i].AddBody(_player_lft_shldr[tempIndex]._arrayOfInstances[0].transform.Component.rigidbody);
                    _world_list[i].AddBody(_player_rght_shldr[tempIndex]._arrayOfInstances[0].transform.Component.rigidbody);
                }
                ////////////////////////////
                ////////////////////////////
                ////////////////////////////
                //////////TERRAIN///////////
                ////////////////////////////
                ////////////////////////////
                ////////////////////////////


                //_offsetPos = new Vector3((_physics_engine_instance_x * 1) * 0.375f, 0, _physics_engine_instance_z * 1);
                //originPos = new SharpDX.Vector3(-3, 1, 3);

                //MessageBox((IntPtr)0, _hasinit0 + "", "Oculus error", 0);






















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

                _object_worldmatrix.M41 = -1.5f + 0;
                _object_worldmatrix.M42 = 0.5f + 0;
                _object_worldmatrix.M43 = -1.5f + 0;

                _object_worldmatrix.M44 = 1;

                _object_worldmatrix.M41 += _offsetPos.X;
                _object_worldmatrix.M42 += _offsetPos.Y;
                _object_worldmatrix.M43 += _offsetPos.Z;


                //_size_screen = 0.00045f;
                _size_screen = 0.0006f;
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

                var screen = new SC_cube();

                var _hasinit0 = screen.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, 1, 1, sizeWidther01, sizeHeighter01, sizeDepther01, new Vector4(r, g, b, a), _inst_screen_x, _inst_screen_y, _inst_screen_z, Hwnd, _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.physicsInstancedScreen, false, 1, 100, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                _world_screen_list[0] = screen;
                _world_screen_list[0]._arrayOfInstances[0].transform.Component.rigidbody.AllowDeactivation = false;

                worldMatrix_instances_screens[0] = new Matrix[_inst_screen_x * _inst_screen_y * _inst_screen_z];
                world_last_Matrix_instances_screens[0] = new Matrix[_inst_screen_x * _inst_screen_y * _inst_screen_z];

                for (int i = 0; i < worldMatrix_instances_screens[0].Length; i++)
                {
                    worldMatrix_instances_screens[0][i] = Matrix.Identity;
                    world_last_Matrix_instances_screens[0][i] = Matrix.Identity;
                }




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



                _screenDirMatrix[0] = new Matrix[4];
                point3DCollection[0] = new Vector3[4];
                _screenDirMatrix_correct_pos[0] = new Matrix[4];


                for (int i = 0; i < _screenDirMatrix[0].Length; i++)
                {
                    _screenDirMatrix[0][i] = new Matrix();
                    _screenDirMatrix[0][i] = rotatingMatrixScreen;
                }

                _screenDirMatrix[0][0].M41 = _world_screen_list[0].Vertices[16].position.X;// + originPosScreen.X;
                _screenDirMatrix[0][0].M42 = _world_screen_list[0].Vertices[16].position.Y;// + originPosScreen.Y;
                _screenDirMatrix[0][0].M43 = _world_screen_list[0].Vertices[16].position.Z;// + originPosScreen.Z;

                _screenDirMatrix[0][1].M41 = _world_screen_list[0].Vertices[13].position.X;// + originPosScreen.X;
                _screenDirMatrix[0][1].M42 = _world_screen_list[0].Vertices[13].position.Y;// + originPosScreen.Y;
                _screenDirMatrix[0][1].M43 = _world_screen_list[0].Vertices[13].position.Z;// + originPosScreen.Z;

                _screenDirMatrix[0][2].M41 = _world_screen_list[0].Vertices[15].position.X;// + originPosScreen.X;
                _screenDirMatrix[0][2].M42 = _world_screen_list[0].Vertices[15].position.Y;// + originPosScreen.Y;
                _screenDirMatrix[0][2].M43 = _world_screen_list[0].Vertices[15].position.Z;// + originPosScreen.Z;

                _screenDirMatrix[0][3].M41 = _world_screen_list[0].Vertices[17].position.X;// + originPosScreen.X;
                _screenDirMatrix[0][3].M42 = _world_screen_list[0].Vertices[17].position.Y;// + originPosScreen.Y;
                _screenDirMatrix[0][3].M43 = _world_screen_list[0].Vertices[17].position.Z;// + originPosScreen.Z;



                //16//13//15//17 
                //8//9//10//11

                for (int i = 0; i < _screenDirMatrix.Length; i++)
                {
                    point3DCollection[0][i] = new Vector3(_screenDirMatrix[0][i].M41, _screenDirMatrix[0][i].M42, _screenDirMatrix[0][i].M43);
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

                _object_worldmatrix.M41 = 0 + 0;
                _object_worldmatrix.M42 = 0 + 0;
                _object_worldmatrix.M43 = 0 + 0;

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
                _world_screen_assets_list[0] = _cube;

                worldMatrix_instances_screen_assets[0] = new Matrix[_inst_screen_assets_x * _inst_screen_assets_y * _inst_screen_assets_z];

                for (int i = 0; i < worldMatrix_instances_screen_assets[0].Length; i++)
                {
                    worldMatrix_instances_screen_assets[0][i] = Matrix.Identity;
                }




















                _basicTexture = new _sc_texture_loader();
                bool _hasinit1 = _basicTexture.Initialize(D3D.device, "../../../terrainGrassDirt.bmp");
                _pink_texture = new _sc_texture_loader();
                _hasinit1 = _pink_texture.Initialize(D3D.device, "../../../1x1_pink_color.png");








                //FOR SCREEN

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

        sc_message_object.sc_message_object[] _data_in; // sometimes this shit is
        public sc_message_object.sc_message_object[] FrameVRTWO(sc_message_object.sc_message_object[] _main_received_object)
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
        private sc_message_object.sc_message_object[] Render(sc_message_object.sc_message_object[] _main_received_object)
        {


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

            _rightTouchMatrix.M41 = handPoseRight.Position.X + originPos.X + movePos.X;
            _rightTouchMatrix.M42 = handPoseRight.Position.Y + originPos.Y + movePos.Y;
            _rightTouchMatrix.M43 = handPoseRight.Position.Z + originPos.Z + movePos.Z;

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

            _leftTouchMatrix.M41 = handPoseLeft.Position.X + originPos.X + movePos.X;
            _leftTouchMatrix.M42 = handPoseLeft.Position.Y + originPos.Y + movePos.Y;
            _leftTouchMatrix.M43 = handPoseLeft.Position.Z + originPos.Z + movePos.Z;





            Quaternion _quat_screen00;

            Matrix mater = worldMatrix_instances_screens[0][0];
            Quaternion.RotationMatrix(ref mater, out _quat_screen00);
            screenNormal = _getDirection(Vector3.ForwardRH, _quat_screen00);
            screenNormal.Normalize();

            planer = new Plane(new Vector3(mater.M41, mater.M42, mater.M43), screenNormal);
            var screen_mat = worldMatrix_instances_screens[0][0];
            var somematroxer2 = screen_mat;
            Quaternion _quat_screen;
            Quaternion.RotationMatrix(ref screen_mat, out _quat_screen);
            screenNormal = _getDirection(Vector3.ForwardRH, _quat_screen);
            screenNormal.Normalize();
            planer = new Plane(new Vector3(screen_mat.M41, screen_mat.M42, screen_mat.M43), screenNormal);

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

            



            /*
            somematroxer2 = _finalRotMatrixScreen;

            Quaternion.RotationMatrix(ref somematroxer2, out _quat_screen);
            //_testQuater.Normalize();

            var screenNormaler = _getDirection(Vector3.ForwardRH, _quat_screen);
            screenNormaler.Normalize();

            var planor = new Plane(new Vector3(somematroxer2.M41, somematroxer2.M42, somematroxer2.M43), screenNormaler);



            var centerPosOculusRift = new Vector3(_hmdPoser.X, _hmdPoser.Y, _hmdPoser.Z) + OFFSETPOS;



            Matrix oculusRifter;
            Matrix.RotationQuaternion(ref _hmdRoter, out oculusRifter);


            oculusRifter = oculusRifter * originRot * rotatingMatrix * rotatingMatrixForPelvis;

            Quaternion some_oculus_quat;
            Quaternion.RotationMatrix(ref oculusRifter, out some_oculus_quat);

            var rayDirRighter = _getDirection(Vector3.ForwardRH, some_oculus_quat);

            rayDirRighter.Normalize();


            var someRayer = new Ray(centerPosOculusRift, rayDirRighter);

            Vector3 intersectPointHMD;
            var intersecterHMD = someRayer.Intersects(ref planor, out intersectPointHMD);

            somematroxer2.M41 = intersectPointHMD.X;
            somematroxer2.M42 = intersectPointHMD.Y;
            somematroxer2.M43 = intersectPointHMD.Z;

            //CIRCLE CIRCLE INTERSECTION //http://mathworld.wolfram.com/Circle-CircleIntersection.html
            d = (point3DCollection[0][2] - point3DCollection[0][0]).Length();

            widthLength = (point3DCollection[0][2] - point3DCollection[0][0]).Length();
            heightLength = (point3DCollection[0][1] - point3DCollection[0][0]).Length();

            r = (intersectPointHMD - point3DCollection[0][0]).Length();
            R = (intersectPointHMD - point3DCollection[0][2]).Length();


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
            }*/







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
                    //_grab_rigid_data._body = null;
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
                if (sc_start_recording_counter >= 50)
                {
                    sc_save_file = 0;
                    //MessageBox((IntPtr)0, "" + sc_start_recording_counter + "", "Oculus Message", 0);
                    sc_start_recording = 0;
                    sc_start_recording_counter = 0;
                }
                sc_start_recording_counter++;
            }

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
                        //C:\Users\ninekorn\Desktop\#RECSOUND
                        short_path = "wave_audio_" + _records_counter;
                        var filename = @"C:\Users\ninekorn\Desktop\#RECSOUND\" + "wave_audio_" + _records_counter + ".wav";
                        mciSendString("save recsound " + filename, null, 0, IntPtr.Zero);
                        mciSendString("close recsound", null, 0, IntPtr.Zero);
                        _has_recorded = 1;
                        last_wave_filepath = filename;

                        string audiotype = "wave_audio_";

                        var stringtemp = short_path;
                        //"wave_audio_"
                        stringtemp.Substring(11, 1);
                        var info = new FileInfo(last_wave_filepath);
                        info.Refresh();



                        System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                        customCulture.NumberFormat.NumberDecimalSeparator = ".";
                        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

                        //short_path = "wave_audio_" + _records_counter;
                        //path = "c:\\Users\\ninekorn\\Desktop\\testXML\\" + 0 + ".xml";
                        path = @"C:\Users\ninekorn\Desktop\#RECSOUND\" + "wave_audio_" + _records_counter + ".xml";
                        last_xml_filepath = path;
                        //sc_can_start_rec_counter_before_add_index = sc_can_start_rec_counter;
                        //doc = new XmlDocument();
                        //writer = new XmlTextWriter(Console.Out);
                        writer = new XmlTextWriter(path, System.Text.Encoding.UTF8);
                        //sc_can_start_rec_counter_before_add_index = sc_can_start_rec_counter;

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
                        //sc_save_file = 0;




                        var lastAccess = info.LastAccessTime;


                        //_index = sc_can_start_rec_counter_before_add_index + "";
                        _sound_player.AddWave(_records_counter - 1 + "", last_wave_filepath);


                        var nativeFileStream = new NativeFileStream(last_wave_filepath, NativeFileMode.Open, NativeFileAccess.Read, NativeFileShare.Read);
                        SoundStream soundStream = new SoundStream(nativeFileStream);
                        MemoryStream ms = new MemoryStream();

                        soundStream.CopyTo(ms);
                        _sound_byte_array = ms.ToArray();


                        soundStream.Dispose();
                        ms.Dispose();
                        _sound_player.Play((_records_counter - 1) + "");
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
                    if (File.Exists(last_wave_filepath))
                    {
                        _sound_player.Play((_records_counter - 1) + "");



                        //Directory.Exists(path)) 
















                        //sc_can_start_rec_counter_before_add_index++;

                        //MessageBox((IntPtr)0, "" + stringtemp + "", "Oculus Message", 0);

                        //File.Delete(last_wave_filepath);
                        /*Process p = new Process();
                        p.StartInfo = new ProcessStartInfo()
                        {
                            FileName = last_wave_filepath
                        };
                        p.Start();
                        p.Refresh();
                        p.Close();


                        //int can_play_index= 0;



                        //Array.Sort(_sound_byte_array);
                        //_sound_byte_array.OrderBy(x > x).FirstOrDefault();
                        //lowestX = point3DCollection.OrderBy(x => x.X).FirstOrDefault();
                        //highestX = point3DCollection.OrderBy(x => x.X).Last();




                        //for (int s = 0; s < _sound_byte_array.Length;s++)
                        //{
                        //    if (_sound_byte_array[s] > 100)
                        //    {
                        //        MessageBox((IntPtr)0, "" + "has sound", "Oculus Message", 0);
                        //    }
                        //    //MessageBox((IntPtr)0, "" + "has recorded", "Oculus Error", 0);
                        //}
                        //_main_received_object[0]._someData[0] = _sound_byte_array;// new object[1];



                        ///MessageBox((IntPtr)0, "" + _sound_byte_array.Length, "SCCoreSystems error", 0);
                        //using (var soundStream = new SoundStream(nativeFileStream))
                        //{
                        //    using (MemoryStream ms = new MemoryStream())
                        //    {
                        //        soundStream.CopyTo(ms);
                        //        _sound_byte_array = ms.ToArray();
                        //        _main_received_messages[0]._someData[0] = _sound_byte_array;// new object[1];
                        //    }
                        //}


                        sc_play_file = 1;
                    }
                    else
                    {
                        //create blank file and start listening instead lol. kidding. nerd joke.
                    }
                }
            }

            if (sc_play_file == 1)
            {
                if (sc_play_file_counter >= 50)
                {
                    sc_play_file = 0;
                    sc_play_file_counter = 0;
                }
                sc_play_file_counter++;
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
                    else if (_has_locked_screen_pos == 1)
                    {
                        _has_locked_screen_pos_counter = 0;
                        _has_locked_screen_pos = 0;
                    }

                    /*if (_has_locked_screen_pos == 3)
                    {
                        _has_locked_screen_pos_counter = 0;
                        _has_locked_screen_pos = 0;
                    }*/
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
















            /*

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
            }*/








            try
            {

                //clothRect.transform.Component.softbody.Update(deltaTime);






                /*if (_sound_byte_array.Length <  )
                {
                    //_sound_byte_array
                }*/

                /*if (_has_recorded == 2)
                {
                    MessageBox((IntPtr)0, _has_recorded + "", "Oculus error", 0);
                }*/
                //MessageBox((IntPtr)0, _has_recorded + "", "Oculus error", 0);



                try
                {


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
                                    Quaternion quaterz;
                                    Stopwatch _this_thread_ticks_01 = new Stopwatch();
                                    _this_thread_ticks_01.Start();

                                _thread_looper:



                                    //Quaternion quater;
                               
                                    //var _delta_timer = (float)Math.Abs((_this_thread_ticks_01.Elapsed.Ticks - _this_thread_ticks_00.Elapsed.Ticks)) * 0.0000000001f; // very slow /100000000000

                                    for (int x = 0; x < _physics_engine_instance_x; x++)
                                    {
                                        for (int y = 0; y < _physics_engine_instance_y; y++)
                                        {
                                            for (int z = 0; z < _physics_engine_instance_z; z++)
                                            {
                                                /*Quaternion.RotationMatrix(ref WorldMatrix, out quaterz);

                                                var posLeftHand = new SharpDX.Vector3(_player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITION.M41, _player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITION.M42, _player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITION.M43);

                                                var rayDirForward = _getDirection(SharpDX.Vector3.ForwardRH, quaterz);
                                                var _ray = new SharpDX.Ray(posLeftHand, rayDirForward);


                                                var ray = new JVector(_ray.Direction.X, _ray.Direction.Y, _ray.Direction.Z);
                                                //JVector camp = Conversion.ToJitterVector(Camera.Position);
                                                ray = JVector.Normalize(ray) * 0.25f;

                                                RigidBody grabBody;
                                                JVector hitNormal;
                                                float fraction;
                                                var camp = new JVector(posLeftHand.X, posLeftHand.Y, posLeftHand.Z);

                                                var resulter = _world_list[x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z)].CollisionSystem.Raycast(camp, ray, RaycastCallback, out grabBody, out hitNormal, out fraction);

                                                if (buttonPressedOculusTouchLeft == 256)
                                                {
                                                    if (resulter)
                                                    {
                                                        var hitPoint = camp + fraction * ray;

                                                        if (_distanceConstraintLeft != null)
                                                        {
                                                            _world_list[x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z)].RemoveConstraint(_distanceConstraintLeft);
                                                        }

                                                        JVector lanchor = new JVector(posLeftHand.X, posLeftHand.Y, posLeftHand.Z) - grabBody.Position; //hitPoint
                                                        lanchor = JVector.Transform(lanchor, JMatrix.Transpose(grabBody.Orientation));

                                                        _distanceConstraintLeft = new PointPointDistance(_player_lft_hnd[0]._arrayOfInstances[0].transform.Component.rigidbody, grabBody, camp, hitPoint);

                                                        _distanceConstraintLeft.Softness = 0.0001f;
                                                        _distanceConstraintLeft.BiasFactor = 0.1f;
                                                        _distanceConstraintLeft.Distance = 0.001f;

                                                        _world_list[x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z)].AddConstraint(_distanceConstraintLeft);

                                                        _lastFraction = fraction;
                                                        _lastRigidGrab = grabBody;
                                                        _hasGrabbed = true;
                                                    }
                                                }

                                                if (buttonPressedOculusTouchLeft == 512)
                                                {
                                                    if (_distanceConstraintLeft != null)
                                                    {
                                                        _world_list[x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z)].RemoveConstraint(_distanceConstraintLeft);                            
                                                    }
                                                    _hasGrabbed = false;
                                                }*/

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



                    /*if (_world_screen_list[0]._arrayOfInstances[0].transform.Component.rigidbody.IsStatic)
                    {
                        Console.WriteLine("static");
                    }*/


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
                            int _inactive_counter = 0;
                            int _instance_pos_y = 0;
                        _thread_looper:

















                            gravity_swtch_counter++;
                            frame_counter_4_buttonY++;


                            for (int x = 0; x < _physics_engine_instance_x; x++)
                            {
                                for (int y = 0; y < _physics_engine_instance_y; y++)
                                {
                                    for (int z = 0; z < _physics_engine_instance_z; z++)
                                    {

                                        var _index = x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z);
                                        tempIndex = 0;
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
                                            if (buttonPressedOculusTouchRight == 4)
                                            {                                      
                                                if (gravity_swtch_counter >= 75)
                                                {
                                                    if (gravity_swtch == 0 || gravity_swtch == 2)
                                                    {
                                                        _world_list[_index].Gravity = new JVector(0, 0, 0);
                                                        gravity_swtch_counter = 0;
                                                        /*if (_index > (_physics_engine_instance_x* _physics_engine_instance_y*_physics_engine_instance_z)-1)
                                                        {

                                                        }*/
                                                        gravity_swtch = 1;
                                                    }
                                                    else if (gravity_swtch == 1)
                                                    {
                                                        _world_list[_index].Gravity = new JVector(0, -9.81f, 0);
                                                        gravity_swtch_counter = 0;
                                                        gravity_swtch = 2;
                                                    }
                                                }
                                            }
                                        }
                    

                                        if (buttonPressedOculusTouchLeft != 0)
                                        {
                                            if (buttonPressedOculusTouchLeft == 512)
                                            {
                                                if (frame_counter_4_buttonY >= 75)
                                                {
                                                    if (display_grid_type == 0)
                                                    {
                                                        display_grid_type = 1;
                                                    }
                                                    else if (display_grid_type == 1)
                                                    {
                                                        display_grid_type = 2;
                                                    }
                                                    else if (display_grid_type == 2)
                                                    {
                                                        display_grid_type = 3;
                                                    }
                                                    else if (display_grid_type == 3)
                                                    {
                                                        display_grid_type = 0;
                                                    }

                                                    frame_counter_4_buttonY = 0;
                                                }
                                 
                                                switch_4_started_physics = 1;
                                            }
                                        }
                          

                                        _inactive_counter = 0;
                                        _static_counter = 0;

                                        int sc_jitter_cloth_count = 0;
                                        int _cube_counter = 0;
                                        int _terrain_tiles_count = 0;

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
                                                            if (_has_locked_screen_pos == 0)
                                                            {
                                                                /*if (body.IsStatic)
                                                                {
                                                                    if (body == _grabbed_body_right)
                                                                    {
                                                                        if (_has_grabbed_right_swtch == 2)
                                                                        {
                                                                            //var rigibody_pos = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (_offset_grabbed_pos_norm * _offset_grabbed_pos_dist);

                                                                            var handPoser0 = new Vector3(_player_rght_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M43) + (_offset_grabbed_pos_norm * _offset_grabbed_pos_dist);

                                                                            Quaternion temp_quat;
                                                                            Matrix temp_mat = _player_rght_hnd[0]._arrayOfInstances[0].current_pos;// worldMatrix_instances_screens[0][0];
                                                                            Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                            JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);

                                                                            JMatrix jmat = JMatrix.CreateFromQuaternion(quat);

                                                                            body.Orientation = jmat;
                                                                            body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);

                                                                            worldMatrix_instances_screens[0][0] = _current_screen_pos;

                                                                            if (_swtch_hasRotated == 0)
                                                                            {
                                                                                if (_has_grabbed_right == 0)
                                                                                {
                                                                                    _has_grabbed_right = 1;
                                                                                }
                                                                            }

                                                                            if (_swtch_hasRotated == 2)
                                                                            {
                                                                                if (_tier_logic_swtch_grab == 0)
                                                                                {

                                                                                }
                                                                                else if (_tier_logic_swtch_grab == 1)
                                                                                {

                                                                                }
                                                                            }

                                                                            if (_swtch_hasRotated == 1)
                                                                            {
                                                                                _tier_logic_swtch_grab = 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                _tier_logic_swtch_grab = 0;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                            quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                            tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                            Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                            Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                            worldMatrix_instances_screens[0][0] = translationMatrix;

                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                        quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                        tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                        Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                        Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                        worldMatrix_instances_screens[0][0] = translationMatrix;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                    quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                    tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                    Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                    Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                    worldMatrix_instances_screens[0][0] = translationMatrix;

                                                                    _process_rigidbody_two(body, x, y, z, _cube_counter, 1);
                                                                }*/
                                                            }
                                                            else
                                                            {
                                                                /*if (had_locked_screen == 1)
                                                                {
                                                                    body.LinearVelocity = JVector.Zero;
                                                                    body.AngularVelocity = JVector.Zero;
                                                                    Quaternion quat00;
                                                                    Quaternion.RotationMatrix(ref _current_screen_pos, out quat00);

                                                                    JQuaternion quatterer00;
                                                                    quatterer00.X = quat00.X;
                                                                    quatterer00.Y = quat00.Y;
                                                                    quatterer00.Z = quat00.Z;
                                                                    quatterer00.W = quat00.W;
                                                                    body.Orientation = JMatrix.CreateFromQuaternion(quatterer00);
                                                                    body.Position = new JVector(_current_screen_pos.M41, _current_screen_pos.M42, _current_screen_pos.M43);
                                                                    worldMatrix_instances_screens[0][0] = _current_screen_pos;
                                                                    had_locked_screen = 2;
                                                                }
                                                                else
                                                                {
                                                                    Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                    quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                    tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                    Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                    Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                    worldMatrix_instances_screens[0][0] = translationMatrix;
                                                                }*/
                                                            }



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
                                                            if (_has_locked_screen_pos == 0)
                                                            {
                                                                if (!body.IsStatic)
                                                                {
                                                                    if (handTriggerLeft[0] >= 0.001f)
                                                                    {
                                                                        body.IsActive = true;

                                                                        Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                        quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                        tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                        Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                        Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                        var dirToRightHand = new Vector3(_player_lft_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                        var lengthofdir = dirToRightHand.Length();

                                                                        //var moveRightHand = new JVector(_player_lft_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M43) - new JVector(_player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M41, _player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M42, _player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M43);

                                                                        if (dirToRightHand != null)
                                                                        {
                                                                            dirToRightHand.Normalize();
                                                                            var force = new JVector(dirToRightHand.X, dirToRightHand.Y, dirToRightHand.Z) * 1;

                                                                            if (force != JVector.Zero && force != null && force.Length() > 0)
                                                                            {
                                                                                //MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
                                                                                //MessageBox((IntPtr)0, "" + "not null force", "Oculus Error", 0);
                                                                                force.Normalize();

                                                                                force *= 0.0045f; //0.045f
                                                                                body.LinearVelocity += force;
                                                                                body.AddForce(force);
                                                                                //body.AddTorque(force);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            //Console.WriteLine("null dir");
                                                                            MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
                                                                        }
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
                                                                worldMatrix_instances_screens[0][0] = translationMatrix;
                                                                _world_screen_list[0]._arrayOfInstances[0].current_pos = translationMatrix;// worldMatrix_instances_screens[0][0];
                                                            }
                                                            else
                                                            {
                                                                if (_tier_logic_swtch_lock_screen == 0)
                                                                {
                                                                    if (had_locked_screen == 1)
                                                                    {
                                                                        body.AngularVelocity = JVector.Zero;
                                                                        body.LinearVelocity = JVector.Zero;

                                                                        worldMatrix_instances_screens[0][0] = _current_screen_pos;
                                                                        _world_screen_list[0]._arrayOfInstances[0].current_pos = _current_screen_pos;// worldMatrix_instances_screens[0][0];                                             

                                                                        Quaternion temp_quat;
                                                                        //Matrix temp_mat = worldMatrix_instances_torso[tempIndex][p_torso_count];
                                                                        Quaternion.RotationMatrix(ref _current_screen_pos, out temp_quat);
                                                                        JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                        JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                        body.Orientation = jmat;
                                                                        body.Position = new JVector(_current_screen_pos.M41, _current_screen_pos.M42, _current_screen_pos.M43);
                                                                        _tier_logic_swtch_lock_screen = 1;
                                                                        had_locked_screen = 2;
                                                                    }

                                                                }

                                                                if (_tier_logic_swtch_lock_screen == 1)
                                                                {
                                                                    Quaternion temp_quat;
                                                                    //Matrix temp_mat = worldMatrix_instances_torso[tempIndex][p_torso_count];
                                                                    Quaternion.RotationMatrix(ref _current_screen_pos, out temp_quat);
                                                                    JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                    JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                    body.Orientation = jmat;
                                                                    body.Position = new JVector(_current_screen_pos.M41, _current_screen_pos.M42, _current_screen_pos.M43);

                                                                    worldMatrix_instances_screens[0][0] = _current_screen_pos;
                                                                    _world_screen_list[0]._arrayOfInstances[0].current_pos = _current_screen_pos;// worldMatrix_instances_screens[0][0];
                                                                }

                                                            }
                                                        

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
                                                            /*if (buttonPressedOculusTouchLeft != 0)
                                                            {
                                                                if (buttonPressedOculusTouchLeft == 512)
                                                                {
                                                                    if (!body.IsActive)
                                                                    {
                                                                        body.IsActive = true;
                                                                    }
                                                                }
                                                            }
                                                            Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);

                                                            quatterer = JQuaternion.CreateFromMatrix(body.Orientation);

                                                            tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

                                                            Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);

                                                            Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                            worldMatrix_instances_voxel_cube[_index][_voxel_voxel_cube_counter] = translationMatrix;

                                                            if (handTriggerRight[1] >= 0.001f)
                                                            {
                                                                var dirToRightHand = new Vector3(_player_rght_hnd[tempIndex]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[tempIndex]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[tempIndex]._arrayOfInstances[0].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                //var dirToRightHand = new Vector3(_player_rght_hnd[_index]._arrayOfInstances[_voxel_voxel_cube_counter].current_pos.M41, _player_rght_hnd[_index]._arrayOfInstances[_voxel_voxel_cube_counter].current_pos.M42, _player_rght_hnd[_index]._arrayOfInstances[_voxel_voxel_cube_counter].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                //var dirToRightHand = new Vector3(_rightTouchMatrix.M41, _rightTouchMatrix.M42, _rightTouchMatrix.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                var lengthofdir = dirToRightHand.Length();
                                                                dirToRightHand.Normalize();

                                                                var _force = handTriggerRight[1] * 0.0075f;
                                                                var force = new JVector(dirToRightHand.X, dirToRightHand.Y, dirToRightHand.Z) * _force;

                                                                if (force != JVector.Zero && force != null)
                                                                {
                                                                    body.LinearVelocity += force;
                                                                    body.AddForce(force);
                                                                    body.AddTorque(force);
                                                                }
                                                            }
                                                            _voxel_voxel_cube_counter++;*/





                                                            if (buttonPressedOculusTouchLeft != 0)
                                                            {
                                                                if (buttonPressedOculusTouchLeft == 512)
                                                                {
                                                                    if (body.IsActive == false)
                                                                    {
                                                                        //body.AllowDeactivation = true;
                                                                        body.IsActive = true;
                                                                        //body.AllowDeactivation = false;
                                                                    }
                                                                }
                                                            }

                                                            Vector3 currentPos = new Vector3(body.Position.X, body.Position.Y, body.Position.Z);
                                                            Vector3 bodyPos = OFFSETPOS;
                                                            float distance = Vector3.Distance(currentPos, bodyPos);// sc_maths.sc_check_distance_node_3d_geometry(currentPos, bodyPos, 100000, 100000, 100000, 100000, 100000, 100000); //11.31415926535f //Vector3.Distance(currentPos, bodyPos);// 

                                                            if (distance < 7.5f) // also check size required of distance based on size of the 2 objects.
                                                            {
                                                                /*body.IsActive = true;
                                                                var dirToRightHand = new Vector3(_player_lft_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                //var dirToRightHand = new Vector3(_player_lft_hnd[_index]._arrayOfInstances[_voxel_cube_counter].current_pos.M41, _player_lft_hnd[_index]._arrayOfInstances[_voxel_cube_counter].current_pos.M42, _player_lft_hnd[_index]._arrayOfInstances[_voxel_cube_counter].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                var lengthofdir = dirToRightHand.Length();

                                                                if (dirToRightHand != null)
                                                                {
                                                                    dirToRightHand.Normalize();
                                                                    var force = new JVector(dirToRightHand.X, dirToRightHand.Y, dirToRightHand.Z) * -1;

                                                                    if (force != JVector.Zero && force != null && force.Length() > 0.01f)
                                                                    {
                                                                        //body.LinearVelocity += force;
                                                                        body.AddForce(force * 0.1f);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    //Console.WriteLine("null dir");
                                                                    MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
                                                                }*/

                                                                bool _boundingBoxer = _world_list[_index].CollisionSystem.CheckBoundingBoxes(body, _player_rght_hnd[0]._arrayOfInstances[0].transform.Component.rigidbody);

                                                                if (_boundingBoxer)
                                                                {
                                                                    //MessageBox((IntPtr)0, "" + "bounding box", "sc core systems message", 0);

                                                                    body.IsActive = true;
                                                                    Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                    quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                    tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                    Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                    Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                    var dirToRightHand = new Vector3(_player_rght_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                    var moveRightHand = new JVector(_player_rght_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M43) - new JVector(_player_rght_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M41, _player_rght_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M42, _player_rght_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M43);
                                                                    var lengthofdir = dirToRightHand.Length();

                                                                    if (dirToRightHand != null)
                                                                    {
                                                                        dirToRightHand.Normalize();
                                                                        var force = new JVector(dirToRightHand.X, dirToRightHand.Y, dirToRightHand.Z) * -1;

                                                                        if (force != JVector.Zero && force != null && force.Length() > 0)
                                                                        {
                                                                            //MessageBox((IntPtr)0, "" + "not null force", "Oculus Error", 0);
                                                                            force.Normalize();

                                                                            force *= 0.045f;
                                                                            //body.LinearVelocity += force;
                                                                            body.AddForce(force);
                                                                            body.AddTorque(force);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        //Console.WriteLine("null dir");
                                                                        MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    //MessageBox((IntPtr)0, "" + "!bounding box", "sc core systems message", 0);
                                                                    //body.IsActive = true;
                                                                }

                                                                //body.AllowDeactivation = false;
                                                                /*if (body.IsActive == false)
                                                                {
                                                                    body.AllowDeactivation = true;
                                                                    body.IsActive = true;
                                                                    body.AllowDeactivation = false;
                                                                }*/
                                                            }
                                                            else
                                                            {
                                                                var somePosLastXExtra = _array_of_last_frame_voxel_pos[_index][_voxel_cube_counter].X;
                                                                var somePosLastYExtra = _array_of_last_frame_voxel_pos[_index][_voxel_cube_counter].Y;
                                                                var somePosLastZExtra = _array_of_last_frame_voxel_pos[_index][_voxel_cube_counter].Z;

                                                                var rigidXExtra = Math.Round(somePosLastXExtra * 10) / 10;
                                                                var rigidYExtra = Math.Round(somePosLastYExtra * 10) / 10;
                                                                var rigidZExtra = Math.Round(somePosLastZExtra * 10) / 10;

                                                                var diffXExtra = Math.Abs(somePosLastXExtra - rigidXExtra);
                                                                var diffYExtra = Math.Abs(somePosLastYExtra - rigidYExtra);
                                                                var diffZExtra = Math.Abs(somePosLastZExtra - rigidZExtra);

                                                                //Console.WriteLine(Math.Round(somePosLastX * 10) / 10);

                                                                if (Math.Round(diffXExtra * 10) / 10 < 0.0000000000001f && //0.0000001f
                                                                    Math.Round(diffYExtra * 10) / 10 < 0.0000000000001f &&
                                                                    Math.Round(diffZExtra * 10) / 10 < 0.0000000000001f)
                                                                {

                                                                    JVector currentLinearVelExtra = body.LinearVelocity;
                                                                    JVector currentAngularVelExtra = body.AngularVelocity;
                                                                    if (currentLinearVelExtra.Length() < 0.00000035f && currentAngularVelExtra.Length() < 0.00000035f) //0.00035f == 400 approx => 0.00075f == 400 approx
                                                                    {
                                                                        if (body.IsActive == true)
                                                                        {
                                                                            //body.AllowDeactivation = true;
                                                                            body.IsActive = false;
                                                                            //body.AllowDeactivation = false;
                                                                        }
                                                                        else
                                                                        {
                                                                            body.IsActive = true;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (body.IsActive == false)
                                                                        {
                                                                            //body.AllowDeactivation = true;
                                                                            body.IsActive = true;
                                                                            //body.AllowDeactivation = false;
                                                                        }
                                                                    }





                                                                    /*
                                                                    JVector currentLinearVelExtra = body.LinearVelocity;
                                                                    JVector currentAngularVelExtra = body.AngularVelocity;
                                                                    Vector3 currentPos = new Vector3(body.Position.X, body.Position.Y, body.Position.Z);
                                                                    Vector3 bodyPos = OFFSETPOS;

                                                                    float distance = sc_maths.sc_check_distance_node_3d_geometry(currentPos, bodyPos, 2, 2, 2, 2, 2, 2); //11.31415926535f

                                                                    if (distance < 150) // also check size required of distance based on size of the 2 objects.
                                                                    {
                                                                        if (body.IsActive == false)
                                                                        {
                                                                            //body.AllowDeactivation = true;
                                                                            body.IsActive = true;
                                                                            //body.AllowDeactivation = false;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (currentLinearVelExtra.Length() < 0.35f && currentAngularVelExtra.Length() < 0.35f) //0.00035f == 400 approx => 0.00075f == 400 approx
                                                                        {
                                                                            if (body.IsActive == true)
                                                                            {
                                                                                body.AllowDeactivation = true;
                                                                                body.IsActive = false;
                                                                                //body.AllowDeactivation = false;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (body.IsActive == false)
                                                                            {
                                                                                //body.AllowDeactivation = true;
                                                                                body.IsActive = true;
                                                                                //body.AllowDeactivation = false;
                                                                            }
                                                                        }
                                                                    }*/

                                                                }
                                                            }
                                                            if (!body.IsStatic && body.IsActive)
                                                            {
                                                                if (handTriggerRight[1] >= 0.001f)
                                                                {
                                                                    Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                    quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                    tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                    Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                    Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);

                                                                    var dirToRightHand = new Vector3(_player_rght_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                    //var dirToRightHand = new Vector3(_player_lft_hnd[_index]._arrayOfInstances[_voxel_cube_counter].current_pos.M41, _player_lft_hnd[_index]._arrayOfInstances[_voxel_cube_counter].current_pos.M42, _player_lft_hnd[_index]._arrayOfInstances[_voxel_cube_counter].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                    var lengthofdir = dirToRightHand.Length();

                                                                    var _force = handTriggerRight[1] * 0.55f; //0.025f

                                                                    if (_force > 0.01f)
                                                                    {
                                                                        if (dirToRightHand != null)
                                                                        {
                                                                            dirToRightHand.Normalize();
                                                                            var force = new JVector(dirToRightHand.X, dirToRightHand.Y, dirToRightHand.Z) * _force;

                                                                            if (force != JVector.Zero && force != null && force.Length() > 0.01f)
                                                                            {
                                                                                //body.LinearVelocity += force;
                                                                                body.AddForce(force * 150);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            //Console.WriteLine("null dir");
                                                                            MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
                                                                        }

                                                                    }



                                                                    _has_used_trigger = 1;
                                                                }

                                                            }
                                                            else
                                                            {
                                                                if (handTriggerRight[1] >= 0.001f)
                                                                {
                                                                    if (body.IsActive == false)
                                                                    {
                                                                        body.IsActive = true;

                                                                    }
                                                                }
                                                            }
                                                            Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                            quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                            tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                            Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                            Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                            worldMatrix_instances_voxel_cube[_index][_voxel_cube_counter] = translationMatrix;

                                                            _array_of_last_frame_voxel_pos[_index][_voxel_cube_counter] = new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                            if (!body.IsActive) // || body.Is
                                                            {
                                                                _inactive_counter++;
                                                            }

                                                            if (body.IsStatic) // || body.Is
                                                            {
                                                                _static_counter++;
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
                                                            }
                                                            _screen_asset_counter++;*/
                                                        }
                                                        else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag._spectrum)
                                                        {

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
                                                            try
                                                            {
                                                                if (buttonPressedOculusTouchLeft != 0)
                                                                {
                                                                    if (buttonPressedOculusTouchLeft == 512)
                                                                    {
                                                                        if (body.IsActive == false)
                                                                        {
                                                                            //body.AllowDeactivation = true;
                                                                            body.IsActive = true;
                                                                            //body.AllowDeactivation = false;
                                                                        }
                                                                    }
                                                                }

                                                                Vector3 currentPos = new Vector3(body.Position.X, body.Position.Y, body.Position.Z);
                                                                Vector3 bodyPos = OFFSETPOS;
                                                                float distance = Vector3.Distance(currentPos, bodyPos);// sc_maths.sc_check_distance_node_3d_geometry(currentPos, bodyPos, 100000, 100000, 100000, 100000, 100000, 100000); //11.31415926535f //Vector3.Distance(currentPos, bodyPos);// 

                                                                if (distance < 7.5f) // also check size required of distance based on size of the 2 objects.
                                                                {
                                                                    /*body.IsActive = true;
                                                                    var dirToRightHand = new Vector3(_player_lft_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                    //var dirToRightHand = new Vector3(_player_lft_hnd[_index]._arrayOfInstances[_cube_counter].current_pos.M41, _player_lft_hnd[_index]._arrayOfInstances[_cube_counter].current_pos.M42, _player_lft_hnd[_index]._arrayOfInstances[_cube_counter].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                    var lengthofdir = dirToRightHand.Length();

                                                                    if (dirToRightHand != null)
                                                                    {
                                                                        dirToRightHand.Normalize();
                                                                        var force = new JVector(dirToRightHand.X, dirToRightHand.Y, dirToRightHand.Z) * -1;

                                                                        if (force != JVector.Zero && force != null && force.Length() > 0.01f)
                                                                        {
                                                                            //body.LinearVelocity += force;
                                                                            body.AddForce(force * 0.1f);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        //Console.WriteLine("null dir");
                                                                        MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
                                                                    }*/

                                                                    bool _boundingBoxer = _world_list[_index].CollisionSystem.CheckBoundingBoxes(body, _player_rght_hnd[0]._arrayOfInstances[0].transform.Component.rigidbody);

                                                                    if (_boundingBoxer)
                                                                    {
                                                                        //MessageBox((IntPtr)0, "" + "bounding box", "sc core systems message", 0);

                                                                        body.IsActive = true;
                                                                        Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                        quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                        tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                        Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                        Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                        var dirToRightHand = new Vector3(_player_rght_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                        var moveRightHand = new JVector(_player_rght_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M43) - new JVector(_player_rght_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M41, _player_rght_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M42, _player_rght_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M43);
                                                                        var lengthofdir = dirToRightHand.Length();

                                                                        if (dirToRightHand != null)
                                                                        {
                                                                            dirToRightHand.Normalize();
                                                                            var force = new JVector(dirToRightHand.X, dirToRightHand.Y, dirToRightHand.Z) * -1;

                                                                            if (force != JVector.Zero && force != null && force.Length() > 0)
                                                                            {
                                                                                //MessageBox((IntPtr)0, "" + "not null force", "Oculus Error", 0);
                                                                                force.Normalize();

                                                                                force *= 0.045f;
                                                                                //body.LinearVelocity += force;
                                                                                body.AddForce(force);
                                                                                body.AddTorque(force);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            //Console.WriteLine("null dir");
                                                                            MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        //MessageBox((IntPtr)0, "" + "!bounding box", "sc core systems message", 0);
                                                                        //body.IsActive = true;
                                                                    }

                                                                    //body.AllowDeactivation = false;
                                                                    /*if (body.IsActive == false)
                                                                    {
                                                                        body.AllowDeactivation = true;
                                                                        body.IsActive = true;
                                                                        body.AllowDeactivation = false;
                                                                    }*/
                                                                }
                                                                else
                                                                {
                                                                    var somePosLastXExtra = _array_of_last_frame_cube_pos[_index][_cube_counter].X;
                                                                    var somePosLastYExtra = _array_of_last_frame_cube_pos[_index][_cube_counter].Y;
                                                                    var somePosLastZExtra = _array_of_last_frame_cube_pos[_index][_cube_counter].Z;

                                                                    var rigidXExtra = Math.Round(somePosLastXExtra * 10) / 10;
                                                                    var rigidYExtra = Math.Round(somePosLastYExtra * 10) / 10;
                                                                    var rigidZExtra = Math.Round(somePosLastZExtra * 10) / 10;

                                                                    var diffXExtra = Math.Abs(somePosLastXExtra - rigidXExtra);
                                                                    var diffYExtra = Math.Abs(somePosLastYExtra - rigidYExtra);
                                                                    var diffZExtra = Math.Abs(somePosLastZExtra - rigidZExtra);

                                                                    //Console.WriteLine(Math.Round(somePosLastX * 10) / 10);

                                                                    if (Math.Round(diffXExtra * 10) / 10 < 0.0000000000001f && //0.0000001f
                                                                        Math.Round(diffYExtra * 10) / 10 < 0.0000000000001f &&
                                                                        Math.Round(diffZExtra * 10) / 10 < 0.0000000000001f)
                                                                    {

                                                                        JVector currentLinearVelExtra = body.LinearVelocity;
                                                                        JVector currentAngularVelExtra = body.AngularVelocity;
                                                                        if (currentLinearVelExtra.Length() < 0.00000035f && currentAngularVelExtra.Length() < 0.00000035f) //0.00035f == 400 approx => 0.00075f == 400 approx
                                                                        {
                                                                            if (body.IsActive == true)
                                                                            {
                                                                                //body.AllowDeactivation = true;
                                                                                body.IsActive = false;
                                                                                //body.AllowDeactivation = false;
                                                                            }
                                                                            else
                                                                            {
                                                                                body.IsActive = true;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (body.IsActive == false)
                                                                            {
                                                                                //body.AllowDeactivation = true;
                                                                                body.IsActive = true;
                                                                                //body.AllowDeactivation = false;
                                                                            }
                                                                        }





                                                                        /*
                                                                        JVector currentLinearVelExtra = body.LinearVelocity;
                                                                        JVector currentAngularVelExtra = body.AngularVelocity;
                                                                        Vector3 currentPos = new Vector3(body.Position.X, body.Position.Y, body.Position.Z);
                                                                        Vector3 bodyPos = OFFSETPOS;

                                                                        float distance = sc_maths.sc_check_distance_node_3d_geometry(currentPos, bodyPos, 2, 2, 2, 2, 2, 2); //11.31415926535f

                                                                        if (distance < 150) // also check size required of distance based on size of the 2 objects.
                                                                        {
                                                                            if (body.IsActive == false)
                                                                            {
                                                                                //body.AllowDeactivation = true;
                                                                                body.IsActive = true;
                                                                                //body.AllowDeactivation = false;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (currentLinearVelExtra.Length() < 0.35f && currentAngularVelExtra.Length() < 0.35f) //0.00035f == 400 approx => 0.00075f == 400 approx
                                                                            {
                                                                                if (body.IsActive == true)
                                                                                {
                                                                                    body.AllowDeactivation = true;
                                                                                    body.IsActive = false;
                                                                                    //body.AllowDeactivation = false;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (body.IsActive == false)
                                                                                {
                                                                                    //body.AllowDeactivation = true;
                                                                                    body.IsActive = true;
                                                                                    //body.AllowDeactivation = false;
                                                                                }
                                                                            }
                                                                        }*/

                                                                    }
                                                                }
                                                                if (!body.IsStatic && body.IsActive)
                                                                {
                                                                    if (handTriggerLeft[0] >= 0.001f)
                                                                    {
                                                                        Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                        quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                        tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                        Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                        Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);

                                                                        var dirToRightHand = new Vector3(_player_lft_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                        //var dirToRightHand = new Vector3(_player_lft_hnd[_index]._arrayOfInstances[_cube_counter].current_pos.M41, _player_lft_hnd[_index]._arrayOfInstances[_cube_counter].current_pos.M42, _player_lft_hnd[_index]._arrayOfInstances[_cube_counter].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                        var lengthofdir = dirToRightHand.Length();

                                                                        var _force = handTriggerLeft[0] * 0.55f; //0.025f

                                                                        if (_force > 0.01f)
                                                                        {
                                                                            if (dirToRightHand != null)
                                                                            {
                                                                                dirToRightHand.Normalize();
                                                                                var force = new JVector(dirToRightHand.X, dirToRightHand.Y, dirToRightHand.Z) * _force;

                                                                                if (force != JVector.Zero && force != null && force.Length() > 0.01f)
                                                                                {
                                                                                    //body.LinearVelocity += force;
                                                                                    body.AddForce(force * 100);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                //Console.WriteLine("null dir");
                                                                                MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
                                                                            }

                                                                        }



                                                                        _has_used_trigger = 1;
                                                                    }

                                                                }
                                                                else
                                                                {
                                                                    if (handTriggerLeft[0] >= 0.001f)
                                                                    {
                                                                        if (body.IsActive == false)
                                                                        {
                                                                            body.IsActive = true;

                                                                        }
                                                                    }
                                                                }

                                                                /*Vector3 currentPos = new Vector3(body.Position.X, body.Position.Y, body.Position.Z);
                                                                Vector3 bodyPos = OFFSETPOS;
                                                                float distance = sc_maths.sc_check_distance_node_3d_geometry(currentPos, bodyPos, 10000, 10000, 10000, 10000, 10000, 10000); //11.31415926535f

                                                                if (distance < 15000) // also check size required of distance based on size of the 2 objects.
                                                                {
                                                                    if (body.IsActive == false)
                                                                    {
                                                                        body.IsActive = true;
                                                                    }
                                                                }*/


                                                                /*JVector currentLinearVelExtra = body.LinearVelocity;
                                                                JVector currentAngularVelExtra = body.AngularVelocity;
                                                                if (currentLinearVelExtra.Length() < 0.35f && currentAngularVelExtra.Length() < 0.35f) //0.00035f == 400 approx => 0.00075f == 400 approx
                                                                {
                                                                    if (body.IsActive == true)
                                                                    {
                                                                        body.AllowDeactivation = true;
                                                                        body.IsActive = false;
                                                                        //body.AllowDeactivation = false;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (body.IsActive == false)
                                                                    {
                                                                        //body.AllowDeactivation = true;
                                                                        body.IsActive = true;
                                                                        //body.AllowDeactivation = false;
                                                                    }
                                                                }*/



                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                MessageBox((IntPtr)0, ex.ToString(), "Oculus Error", 0);
                                                            }
                                                            /* if (handTriggerLeft[0] >= 0.001f)
                                                             {
                                                                 var dirToRightHand = new Vector3(_player_lft_hnd[tempIndex]._arrayOfInstances[0].current_pos.M41, _player_lft_hnd[tempIndex]._arrayOfInstances[0].current_pos.M42, _player_lft_hnd[tempIndex]._arrayOfInstances[0].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                 //var dirToRightHand = new Vector3(_player_rght_hnd[_index]._arrayOfInstances[_voxel_cube_counter].current_pos.M41, _player_rght_hnd[_index]._arrayOfInstances[_voxel_cube_counter].current_pos.M42, _player_rght_hnd[_index]._arrayOfInstances[_voxel_cube_counter].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                 //var dirToRightHand = new Vector3(_rightTouchMatrix.M41, _rightTouchMatrix.M42, _rightTouchMatrix.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                 var lengthofdir = dirToRightHand.Length();
                                                                 dirToRightHand.Normalize();

                                                                 var _force = handTriggerLeft[0] * 0.0075f;
                                                                 var force = new JVector(dirToRightHand.X, dirToRightHand.Y, dirToRightHand.Z) * _force;

                                                                 if (force != JVector.Zero && force != null)
                                                                 {
                                                                     body.LinearVelocity += force;
                                                                     body.AddForce(force);
                                                                 }
                                                             }*/

                                                            //Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                            //quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                            //tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                            //Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                            //Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                            //worldMatrix_instances_cubes[_index][_cube_counter] = translationMatrix;



                                                            /*if (buttonPressedOculusTouchRight != 0)
                                                            {
                                                                if (buttonPressedOculusTouchRight == 2)
                                                                {
                                                                    if (body.IsStatic || !body.IsActive)
                                                                    {
                                                                        if (!body.IsActive)
                                                                        {
                                                                            body.IsActive = true;
                                                                        }

                                                                        if (body.IsStatic)
                                                                        {
                                                                            body.IsStatic = false;

                                                                        }
                                                                    }
                                                                    //_world_list[_index].Gravity = new JVector(0, 0, 0);
                                                                }
                                                            }

                                                            if (buttonPressedOculusTouchLeft != 0)
                                                            {
                                                                if (buttonPressedOculusTouchLeft == 512)
                                                                {
                                                                    if (!body.IsStatic || body.IsActive)
                                                                    {
                                                                        if (body.IsActive)
                                                                        {
                                                                            body.IsActive = false;
                                                                        }

                                                                        if (!body.IsStatic)
                                                                        {
                                                                            body.IsStatic = true;

                                                                        }
                                                                    }
                                                                    //_world_list[_index].Gravity = new JVector(0, -9.81f, 0);
                                                                }
                                                            }*/










                                                            /*if (body.IsStatic)
                                                            {
                                                                if (body == _grabbed_body_right)
                                                                {
                                                                    if (_has_grabbed_right_swtch == 2)
                                                                    {
                                                                        //var rigibody_pos = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (_offset_grabbed_pos_norm * _offset_grabbed_pos_dist);

                                                                        var handPoser0 = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (_offset_grabbed_pos_norm * _offset_grabbed_pos_dist);

                                                                        if (_swtch_hasRotated == 0)
                                                                        {
                                                                            if (_has_grabbed_right == 0)
                                                                            {
                                                                                _has_grabbed_right = 1;
                                                                            }
                                                                        }

                                                                        if (_swtch_hasRotated == 2)
                                                                        {
                                                                            if (_tier_logic_swtch_grab == 0)
                                                                            {

                                                                            }
                                                                            else if (_tier_logic_swtch_grab == 1)
                                                                            {

                                                                            }
                                                                        }

                                                                        if (_swtch_hasRotated == 1)
                                                                        {
                                                                            _tier_logic_swtch_grab = 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tier_logic_swtch_grab = 0;
                                                                        }
                                                                    }
                                                                    else if (_has_grabbed_right_swtch == 1)
                                                                    {


                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    body.AffectedByGravity = false;
                                                                    body.IsActive = false;

                                                                    Matrix translationMatrix0;
                                                                    Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix0);

                                                                    JQuaternion quatterer0 = JQuaternion.CreateFromMatrix(body.Orientation);

                                                                    Quaternion tester0;
                                                                    tester0.X = quatterer0.X;
                                                                    tester0.Y = quatterer0.Y;
                                                                    tester0.Z = quatterer0.Z;
                                                                    tester0.W = quatterer0.W;

                                                                    //Quaternion tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

                                                                    Matrix.RotationQuaternion(ref tester0, out rotationMatrix);

                                                                    Matrix.Multiply(ref rotationMatrix, ref translationMatrix0, out translationMatrix0);
                                                                    worldMatrix_instances_cubes[_index][_cube_counter] = translationMatrix0;

                                                                    //_swtch_hasRotated = 0;
                                                                    //_has_grabbed_right = 0;
                                                                    //_has_grabbed_right_swtch = 0;
                                                                    //_grabbed_body_right = null;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                worldMatrix_instances_cubes[_index][_cube_counter] = translationMatrix;
                                                                //_process_rigidbody_two(body, x, y, z, _cube_counter, 0);
                                                                _process_rigidbody_that_are_currently_activated_or_not(body, x, y, z, _cube_counter);
                                                            }*/


                                                            //_some_last_frame_vector[_index][count] = _arrayOfVecs;
                                                            //_some_last_frame_rigibodies[_index][count] = _arrayOfBodies;



                                                            Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                            quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                            tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                            Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                            Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                            worldMatrix_instances_cubes[_index][_cube_counter] = translationMatrix;

                                                            _array_of_last_frame_cube_pos[_index][_cube_counter] = new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                            if (!body.IsActive) // || body.Is
                                                            {
                                                                _inactive_counter++;
                                                            }

                                                            if (body.IsStatic) // || body.Is
                                                            {
                                                                _static_counter++;
                                                            }
                                                            _cube_counter++;
                                                        }

                                                        else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerHandRight)
                                                        {
                                                            Quaternion temp_quat;
                                                            Matrix temp_mat = worldMatrix_instances_r_hand[tempIndex][p_r_hnd_count];
                                                            Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                            JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                            JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                            body.Orientation = jmat;
                                                            body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);

                                                            //_player_rght_hnd[tempIndex]._arrayOfInstances[p_r_hnd_count].current_pos = temp_mat;

                                                            p_r_hnd_count++;

                                                        }
                                                        else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.PlayerHandLeft)
                                                        {
                                                            Quaternion temp_quat;
                                                            Matrix temp_mat = worldMatrix_instances_l_hand[tempIndex][p_l_hnd_count];
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
                                                            Matrix temp_mat = worldMatrix_instances_torso[tempIndex][p_torso_count];
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
                                                            Matrix temp_mat = worldMatrix_instances_pelvis[tempIndex][p_pelvis_count];
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
                                                            Matrix temp_mat = worldMatrix_instances_r_shoulder[tempIndex][p_r_shldr_count];
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
                                                            Matrix temp_mat = worldMatrix_instances_l_shoulder[tempIndex][p_l_shldr_count];
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
                                                            Matrix temp_mat = worldMatrix_instances_r_lowerarm[tempIndex][p_r_lowerA_count];
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
                                                            Matrix temp_mat = worldMatrix_instances_l_lowerarm[tempIndex][p_l_lowerA_count];
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
                                                            Matrix temp_mat = worldMatrix_instances_r_upperarm[tempIndex][p_r_upperA_count];
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
                                                            Matrix temp_mat = worldMatrix_instances_l_upperarm[tempIndex][p_l_upperA_count];
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
                                                            Matrix temp_mat = worldMatrix_instances_l_elbow_target[tempIndex][p_l_target_count];
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
                                                            Matrix temp_mat = worldMatrix_instances_r_elbow_target[tempIndex][p_r_target_count];
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
                                                            Matrix temp_mat = worldMatrix_instances_l_elbow_target_two[tempIndex][p_l_target_two_count];
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
                                                            Matrix temp_mat = _player_rght_elbow_target_two[tempIndex]._arrayOfInstances[p_r_target_two_count].current_pos;// worldMatrix_instances_r_elbow_target_two[tempIndex][p_r_target_two_count];
                                                            Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                            JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                            JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                            body.Orientation = jmat;
                                                            body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                            p_r_target_two_count++;

                                                        }
                                                        else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.sc_jitter_cloth)
                                                        {
                                                            /*Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                            quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                            tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                            Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                            Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                            worldMatrix_instances_jitter_cloth[_index][sc_jitter_cloth_count] = translationMatrix;
                                                            sc_jitter_cloth_count++;*/

                                                            /*Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                            quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                            tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                            Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                            Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                            //worldMatrix_instances_terrain[0][0] = translationMatrix;
                                                            //_terrain_count++;
                                                            clothRect._POSITION = translationMatrix;*/

                                                            /*var orientation = body.Orientation;
                                                            Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                            Matrix.Multiply(ref _tempMatrix, ref translationMatrix, out _tempMatrix);


                                                            _tempMatrix.M11 = orientation.M11;
                                                            _tempMatrix.M12 = orientation.M12;
                                                            _tempMatrix.M13 = orientation.M13;
                                                            _tempMatrix.M14 = orientation.M14;

                                                            _tempMatrix.M21 = orientation.M21;
                                                            _tempMatrix.M22 = orientation.M22;
                                                            _tempMatrix.M23 = orientation.M23;
                                                            _tempMatrix.M24 = orientation.M24;

                                                            _tempMatrix.M31 = orientation.M31;
                                                            _tempMatrix.M32 = orientation.M32;
                                                            _tempMatrix.M33 = orientation.M33;
                                                            _tempMatrix.M34 = orientation.M34;*/


                                                        }
                                                    }
                                                }
                                            }
                                        }


                                        Console.Title = Program._program_name + " => " + "Made by Steve Chassé # Rivière-du-Loup # Québec# Canada # 2020-04-13 " + " # " + " inactive objects: " + _inactive_counter;
                                        //Console.SetCursorPosition(1, _instance_pos_y);
                                        //Console.WriteLine("inactive objects: " + _inactive_counter);



                                    }
                                }
                            }




                            Thread.Sleep(1);
                            goto _thread_looper;
                        };

                        threaders.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs argers)
                        {

                        };

                        threaders.RunWorkerAsync();
                        //END OF












                        /*
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

                        threaders.RunWorkerAsync();*/
                        //END OF 

                        _start_background_worker_01 = 1;
                    }
                    //END OF 



                    Vector4 ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
                    Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
                    //var lightDirection = new Vector3(0, -1, -1);

                    Matrix tempmat = _icoSphere._TEMPPOSITION; //_player_rght_hnd[0]._arrayOfInstances[0].current_pos

                    Quaternion.RotationMatrix(ref tempmat, out otherQuat);
                    Vector3 dirLight = _getDirection(Vector3.ForwardRH, otherQuat);
                    //Vector3 dirLight = new Vector3(0, -1, 0); //lightDirection;// new Vector3(0, -1, 0);
                    //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                    //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                    //direction_feet_up = _getDirection(Vector3.Up, otherQuat);







                    Vector3 lightpos = new Vector3(0, _icoSphere._TEMPPOSITION.M42, 0);// new Vector3(_player_rght_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M43);
                    //Vector3 lightpos = new Vector3(worldMatrix_instances_r_hand[0][0].M41, worldMatrix_instances_r_hand[0][0].M42, worldMatrix_instances_r_hand[0][0].M43);
                    //var lightDirection = new Vector3(_player_rght_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M43);

                    _DLightBuffer[0] = new SCCoreSystems.SC_Graphics.SC_cube.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    };
                    _DLightBufferSC_jitter_cloth[0] = new SCCoreSystems.SC_Graphics.SC_jitter_cloth.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    };

                    
                    _DLightBuffer_voxel_cube[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    };


                    _DLightBuffer_spectrum[0] = new SCCoreSystems.SC_Graphics.sc_spectrum.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    };

                    _SC_modL_torso_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    };

                    _SC_modL_rght_hnd_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    };

                    _SC_modL_rght_hnd_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    };

                    _SC_modL_lft_hnd_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    };
                    _SC_modL_lft_hnd_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    };

                    _SC_modL_rght_shldr_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    }; _SC_modL_lft_shldr_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    }; _SC_modL_rght_elbow_target_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    };
                    _SC_modL_rght_elbow_target_two_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    };
                    _SC_modL_rght_upper_arm_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    }; _SC_modL_rght_lower_arm_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    };
                    _SC_modL_lft_elbow_target_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    };
                    _SC_modL_lft_elbow_target_two_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    };
                    _SC_modL_lft_upper_arm_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    };
                    _SC_modL_lft_lower_arm_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    };
                    _SC_modL_pelvis_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                    {
                        ambientColor = ambientColor,
                        diffuseColor = diffuseColour,
                        lightDirection = dirLight,
                        padding0 = 0,
                        lightPosition = lightpos,//new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43),
                        padding1 = 100
                    };










                    /*for (int s = 0; s < worldMatrix_instances_spectrum[0].Length; s++)
                    {
                        var _left_touch_pos = new Vector3(_leftTouchMatrix.M41, _leftTouchMatrix.M42, _leftTouchMatrix.M43);

                        Matrix refmat = _player_lft_hnd[0]._arrayOfInstances[0].current_pos;
                        Quaternion.RotationMatrix(ref refmat, out otherQuat); //finalRotationMatrix
                        direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                        direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                        direction_feet_up = _getDirection(Vector3.Up, otherQuat);
                        
                        //HANDLEFT
                        MOVINGPOINTER = new Vector3(_player_torso[0]._arrayOfInstances[0]._ORIGINPOSITION.M41, _player_torso[0]._arrayOfInstances[0]._ORIGINPOSITION.M42, _player_torso[0]._arrayOfInstances[0]._ORIGINPOSITION.M43);

                        Matrix someMatLeft = _leftTouchMatrix;
                        someMatLeft.M41 = _player_lft_hnd[0]._arrayOfInstances[0]._TEMPPOSITION.M41 + _world_spectrum_list[0]._arrayOfInstances[s]._POSITION.M41;
                        someMatLeft.M42 = _player_lft_hnd[0]._arrayOfInstances[0]._TEMPPOSITION.M42 + _world_spectrum_list[0]._arrayOfInstances[s]._POSITION.M42;// + MOVINGPOINTER.Y;
                        someMatLeft.M43 = _player_lft_hnd[0]._arrayOfInstances[0]._TEMPPOSITION.M43 + _world_spectrum_list[0]._arrayOfInstances[s]._POSITION.M43;

                        diffNormPosX = (MOVINGPOINTER.X) - someMatLeft.M41;
                        diffNormPosY = (MOVINGPOINTER.Y) - someMatLeft.M42;
                        diffNormPosZ = (MOVINGPOINTER.Z) - someMatLeft.M43;

                        MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                        //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * (diffNormPosY));
                        MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_forward * (diffNormPosZ));



                        var otherdiffx = _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M41 - _player_torso[0]._arrayOfInstances[0].current_pos.M41;
                        var otherdiffy = _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M42 - _player_torso[0]._arrayOfInstances[0].current_pos.M42;
                        var otherdiffz = _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M43 - _player_torso[0]._arrayOfInstances[0].current_pos.M43;

                        MOVINGPOINTER = MOVINGPOINTER + (direction_feet_up * (spectrum_noise_value + (_sound_byte_array[s] * 0.00125f)));



                        MOVINGPOINTER.X += OFFSETPOS.X + otherdiffx;
                        MOVINGPOINTER.Y += OFFSETPOS.Y + otherdiffy;
                        MOVINGPOINTER.Z += OFFSETPOS.Z + otherdiffz;

                        someMatLeft = someMatLeft * finalRotationMatrix;

                        someMatLeft.M41 = MOVINGPOINTER.X;
                        someMatLeft.M42 = MOVINGPOINTER.Y;
                        someMatLeft.M43 = MOVINGPOINTER.Z;

                        spectrum_mat.M11 = someMatLeft.M11;
                        spectrum_mat.M12 = someMatLeft.M12;
                        spectrum_mat.M13 = someMatLeft.M13;
                        spectrum_mat.M14 = someMatLeft.M14;

                        spectrum_mat.M21 = someMatLeft.M21;
                        spectrum_mat.M22 = someMatLeft.M22;
                        spectrum_mat.M23 = someMatLeft.M23;
                        spectrum_mat.M24 = someMatLeft.M24;

                        spectrum_mat.M31 = someMatLeft.M31;
                        spectrum_mat.M32 = someMatLeft.M32;
                        spectrum_mat.M33 = someMatLeft.M33;
                        spectrum_mat.M34 = someMatLeft.M34;

                        spectrum_mat.M41 = MOVINGPOINTER.X;
                        spectrum_mat.M42 = MOVINGPOINTER.Y;// + spectrum_noise_value + (_sound_byte_array[s] * 0.0015f);
                        spectrum_mat.M43 = MOVINGPOINTER.Z;
                        spectrum_mat.M44 = 1;

                        if (_has_recorded == 1)
                        {
                            //spectrum_noise_value = SimplexNoise.Noise.Generate(spectrum_mat.M41, spectrum_mat.M42, spectrum_mat.M43);
                            //noiseXZ *= fastNoise.GetNoise((((x * planeSize) + currentPosition.X + seed) / _detailScale) * _tinyChunkHeightScale, (((y * planeSize) + currentPosition.Y + seed) / _detailScale) * _tinyChunkHeightScale, (((z * planeSize) + currentPosition.Z + seed) / _detailScale) * _tinyChunkHeightScale);
                            var planeSize = 0.01f;
                            var detailscale = 100;
                            var heightmul = 20;
                            var seed = 3420;
                            var fastNoise = new FastNoise(3420);
                            float noise = fastNoise.GetPerlin((((spectrum_mat.M41 * planeSize) + seed) / detailscale) * heightmul, (((spectrum_mat.M42 * planeSize) + seed) / detailscale) * heightmul, (((spectrum_mat.M43 * planeSize) + seed) / detailscale) * heightmul);

                            //MessageBox((IntPtr)0, "" + "has recorded", "Oculus Error", 0);
                            //_sound_byte_array_lift[_spectrum_count] = _sound_byte_array[_spectrum_count];

                            //_spectrum_work = 1;
                            _has_recorded = 2;
                        }
                        else
                        {
                            //spectrum_mat.M41 = _left_touch_pos.X + _world_spectrum_list[0]._arrayOfInstances[s]._POSITION.M41;
                            //spectrum_mat.M42 = _left_touch_pos.Y + _world_spectrum_list[0]._arrayOfInstances[s]._POSITION.M42;
                            //spectrum_mat.M43 = _left_touch_pos.Z + _world_spectrum_list[0]._arrayOfInstances[s]._POSITION.M43;
                        }
                        worldMatrix_instances_spectrum[0][s] = spectrum_mat;
                    }*/


                    Quaternion quater;

                    Matrix Matter = _player_rght_hnd[0]._arrayOfInstances[0]._LASTPOSITION;// _world_screen_list[0]._arrayOfInstances[0].current_pos;
                    SharpDX.Quaternion.RotationMatrix(ref Matter, out quater);
                    var matrixRight = _player_rght_hnd[0]._arrayOfInstances[0]._LASTPOSITION;
                    Vector3 dirTo = _getDirection(Vector3.ForwardRH, quater);
                    Vector3 tempvec0 = new Vector3(matrixRight.M41, matrixRight.M42, matrixRight.M43);
                    tempvec0 = tempvec0 + (dirTo * _player_rght_hnd[0]._total_torso_height * 3.5f);
                    matrixRight.M41 = tempvec0.X;
                    matrixRight.M42 = tempvec0.Y;
                    matrixRight.M43 = tempvec0.Z;

                    Matter = _player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITION;// _world_screen_list[0]._arrayOfInstances[0].current_pos;
                    SharpDX.Quaternion.RotationMatrix(ref Matter, out quater);
                    var matrixLeft = _player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITION;
                    dirTo = _getDirection(Vector3.ForwardRH, quater);
                    Vector3 tempvec1 = new Vector3(matrixLeft.M41, matrixLeft.M42, matrixLeft.M43);
                    tempvec1 = tempvec1 + (dirTo * _player_lft_hnd[0]._total_torso_height * 3.5f);
                    matrixLeft.M41 = tempvec1.X;
                    matrixLeft.M42 = tempvec1.Y;
                    matrixLeft.M43 = tempvec1.Z;

                    SharpDX.Quaternion.RotationMatrix(ref matrixLeft, out quater);


                    //CONTAINMENT BOX TEST
                    var _WorldMatrixContainer = _world_screen_list[0]._arrayOfInstances[0].current_pos;
                    //_WorldMatrixContainer.M41 = 0.5f;
                    //_WorldMatrixContainer.M42 = 0.5f;
                    //_WorldMatrixContainer.M43 = 1;
                    var positionOfDContainer = new Vector3(_WorldMatrixContainer.M41, _WorldMatrixContainer.M42, _WorldMatrixContainer.M43);

                    float posXDTouchContainer = (float)(Math.Round(matrixLeft.M41, 1));// * 0.01f;
                    float posYDTouchContainer = (float)(Math.Round(matrixLeft.M42, 1));// * 0.01f;
                    float posZDTouchContainer = (float)(Math.Round(matrixLeft.M43, 1));// * 0.01f;

                    float posXOfDContainer = (float)(Math.Round(positionOfDContainer.X, 1));// * 0.01f;
                    float posYOfDContainer = (float)(Math.Round(positionOfDContainer.Y, 1));// * 0.01f;
                    float posZOfDContainer = (float)(Math.Round(positionOfDContainer.Z, 1));// * 0.01f;

                    float xxx;
                    float yyy;
                    float zzz;            

                    for (int x = -ChunkWidth_L; x <= ChunkWidth_R; x++)
                    {
                        for (int y = -ChunkHeight_L; y <= ChunkHeight_R; y++)
                        {
                            for (int z = -ChunkDepth_L; z <= ChunkDepth_R; z++)
                            {
                                float posX = (x);
                                float posY = (y);
                                float posZ = (z);

                                var xxi = x;
                                var yyi = y;
                                var zzi = z;

                                if (xxi < 0)
                                {
                                    xxi *= -1;
                                    xxi = (ChunkWidth_R) + xxi;
                                }
                                if (yyi < 0)
                                {
                                    yyi *= -1;
                                    yyi = (ChunkHeight_R) + yyi;
                                }
                                if (zzi < 0)
                                {
                                    zzi *= -1;
                                    zzi = (ChunkDepth_R) + zzi;
                                }

                                int _index = xxi + (ChunkWidth_L + ChunkWidth_R + 1) * (yyi + (ChunkHeight_L + ChunkHeight_R + 1) * zzi);

    
                                //xxx = (xu * 0.1f);
                                //yyy = (yu * 0.1f);
                                //zzz = (zu * 0.1f);
                                xxx = (float)(Math.Round((xxi * 0.1f), 1));
                                yyy = (float)(Math.Round((yyi * 0.1f), 1));
                                zzz = (float)(Math.Round((zzi * 0.1f), 1));


                                //float[] array = getDirection(quater, xxx, yyy, zzz);
                                //arrayX[xii + (_widther + _widther) * (yii + (_widther + _widther) * zii)] = (float)(Math.Round(array[0], 1));
                                //arrayY[xii + (_heighter + _heighter) * (yii + (_heighter + _heighter) * zii)] =  (float)(Math.Round(array[1], 1));
                                //arrayZ[xii + (_depther + _depther) * (yii + (_depther + _depther) * zii)] =  (float)(Math.Round(array[2], 1));

                                //SharpDX.Matrix.Translation(xxx, yyy, zzz, out translationMatrix);
                                _WorldMatrix = WorldMatrix;

                                _WorldMatrix.M41 = xxx;
                                _WorldMatrix.M42 = yyy;
                                _WorldMatrix.M43 = zzz;

                                //SharpDX.Matrix.Multiply(ref _WorldMatrix, ref translationMatrix, out _WorldMatrix);
                                SharpDX.Matrix.Multiply(ref _WorldMatrix, ref matrixLeft, out _WorldMatrix);

                                //arrayX[xii + (_widther + _widther) * (yii + (_widther + _widther) * zii)] = _WorldMatrix.M41;
                                //arrayY[xii + (_heighter + _heighter) * (yii + (_heighter + _heighter) * zii)] = _WorldMatrix.M42;
                                //arrayZ[xii + (_depther + _depther) * (yii + (_depther + _depther) * zii)] = _WorldMatrix.M43;

                                arrayX[xxi + (ChunkWidth_L + ChunkWidth_R + 1) * (yyi + (ChunkHeight_L + ChunkHeight_R + 1) * zzi)] = (float)(Math.Round(_WorldMatrix.M41, 1));
                                arrayY[xxi + (ChunkWidth_L + ChunkWidth_R + 1) * (yyi + (ChunkHeight_L + ChunkHeight_R + 1) * zzi)] = (float)(Math.Round(_WorldMatrix.M42, 1));
                                arrayZ[xxi + (ChunkWidth_L + ChunkWidth_R + 1) * (yyi + (ChunkHeight_L + ChunkHeight_R + 1) * zzi)] = (float)(Math.Round(_WorldMatrix.M43, 1));

                            }
                        }
                    }

                    //_WorldMatrix = WorldMatrix;

                    //float xPos = (float)(Math.Round(matrixOfTouchLeft.M41, 1));
                    //float yPos = (float)(Math.Round(matrixOfTouchLeft.M42, 1));
                    //float zPos = (float)(Math.Round(matrixOfTouchLeft.M43, 1));















                    //RENDERING OCULUS EYE DISPLAYS
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
                        D3D.device.ImmediateContext.ClearRenderTargetView(eyeTexture.RenderTargetViews[textureIndex], SharpDX.Color.Black); //DimGray
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



                     
                        _WorldMatrix = WorldMatrix;
                        //TERRAIN SINGLEOBJECT
                        _terrain[0].Render(D3D.device.ImmediateContext);
                        _shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _terrain[0].IndexCount, _terrain[0].InstanceCount, _terrain[0]._POSITION, viewMatrix, _projectionMatrix, _basicTexture.TextureResource, _DLightBuffer, new Vector3(0, 0, 1), _terrain[0]);
                        //END OF



                        //_zAxis[0].Render(D3D.device.ImmediateContext);
                        //_shaderManager.RenderTouchTextureShader(D3D.device.ImmediateContext, _zAxis[0].IndexCount, _zAxis[0].InstanceCount, _zAxis[0]._POSITION, viewMatrix, _projectionMatrix, _basicTexture.TextureResource, _DLightBuffer_DZAxis, new Vector3(0, 0, 1), _zAxis[0]);
                        //END OF

                        _grid.Render(D3D.device.ImmediateContext);
                        _shaderManager.RenderGrid(D3D.device.ImmediateContext, _grid.IndexCount, _WorldMatrix, viewMatrix, _projectionMatrix);

                        float timeSinceStart = (float)(DateTime.Now - startTime).TotalSeconds;
                        float speed = 0.1f;
                        Matrix world = Matrix.Scaling(1.0f) * Matrix.RotationX(timeSinceStart * speed) * Matrix.RotationY(timeSinceStart * 2 * speed) * Matrix.RotationZ(timeSinceStart * 3 * speed);

                        _icoSphere._TEMPPOSITION = world;// _icoSphere._TEMPPOSITION;
                        _icoSphere._TEMPPOSITION.M42 = 5;
                        _icoSphere.Render(D3D.device.ImmediateContext);
                        _shaderManager.RenderIcoShader(D3D.device.ImmediateContext, _icoSphere.IndexCount, _icoSphere._TEMPPOSITION, viewMatrix, _projectionMatrix, _icoVertexCount, 1);


          
                        /*var handPose = trackingState.HandPoses[1].ThePose;
                        var handPosePosition = handPose.Position;
                        SharpDX.Quaternion _rightControllerQuaternion = new SharpDX.Quaternion(handPose.Orientation.X, handPose.Orientation.Y, handPose.Orientation.Z, handPose.Orientation.W);
                        SharpDX.Matrix.RotationQuaternion(ref _rightControllerQuaternion, out WorldMatrixx);
                        var posToucher = new Vector3(handPose.Position.X + originPos.X, handPose.Position.Y + originPos.Y, handPose.Position.Z + originPos.Z);
                        SharpDX.Matrix.Translation(posToucher.X, posToucher.Y, posToucher.Z, out translationMatrix);
                        SharpDX.Matrix.Multiply(ref WorldMatrixx, ref translationMatrix, out WorldMatrixx);
                        SharpDX.Matrix.Multiply(ref WorldMatrixx, ref originRot, out WorldMatrixx);*/
                        //touchRight.Render(DeviceContext);
                        //ShaderManager.RenderTouchTextureShader(DeviceContext, touchRight.IndexCount, WorldMatrixx, viewMatrix, _projectionMatrix,_vertexCount, 1, _worldMatrix);
                        //touchRight.Position = new Vector3(WorldMatrixx.M41, WorldMatrixx.M42, WorldMatrixx.M43);
                        _dTouchRightContainer.Render(D3D.device.ImmediateContext);
                        _shaderManager.RenderObjectGrid(D3D.device.ImmediateContext, _dTouchRightContainer.IndexCount, matrixRight, viewMatrix, _projectionMatrix);


                        /*var handPoseLeft = trackingState.HandPoses[0].ThePose;
                        SharpDX.Quaternion _leftControllerQuaternion = new SharpDX.Quaternion(handPoseLeft.Orientation.X, handPoseLeft.Orientation.Y, handPoseLeft.Orientation.Z, handPoseLeft.Orientation.W);
                        SharpDX.Matrix.RotationQuaternion(ref _leftControllerQuaternion, out _WorldMatrix);
                        var posToucherLeft = new Vector3(handPoseLeft.Position.X + originPos.X, handPoseLeft.Position.Y + originPos.Y, handPoseLeft.Position.Z + originPos.Z);
                        SharpDX.Matrix.Translation(posToucherLeft.X, posToucherLeft.Y, posToucherLeft.Z, out translationMatrix);
                        SharpDX.Matrix.Multiply(ref _WorldMatrix, ref translationMatrix, out _WorldMatrix);
                        SharpDX.Matrix.Multiply(ref _WorldMatrix, ref originRot, out _WorldMatrix);
                        */
                        //touchLeft.Render(DeviceContext);
                        //ShaderManager.RenderTouchTextureShader(DeviceContext, touchLeft.IndexCount, WorldMatrixx, viewMatrix, _projectionMatrix, _vertexCount, 1, _worldMatrix);
                        //touchLeft.Position = new Vector3(_WorldMatrix.M41, _WorldMatrix.M42, _WorldMatrix.M43);
                        _dTouchLeftContainer.Render(D3D.device.ImmediateContext);
                        _shaderManager.RenderObjectGrid(D3D.device.ImmediateContext, _dTouchLeftContainer.IndexCount, matrixLeft, viewMatrix, _projectionMatrix);


                        if (display_grid_type == 0)
                        {

                        }                      
                        else if (display_grid_type == 1)
                        {
                            Matrix screengridmatrix = _world_screen_list[0]._arrayOfInstances[0].current_pos;
                            screengridmatrix.M43 += _world_screen_list[0]._total_torso_depth * 1.25f;

                            _screen_grid_Y.Render(D3D.Device.ImmediateContext);
                            _shaderManager.RenderGrid(D3D.Device.ImmediateContext, _screen_grid_Y.IndexCount, screengridmatrix, viewMatrix, _projectionMatrix);
                        }
                        else if (display_grid_type == 2)
                        {
                            Matrix screengrid_metricmatrix = _world_screen_list[0]._arrayOfInstances[0].current_pos;
                            screengrid_metricmatrix.M43 += _world_screen_list[0]._total_torso_depth * 1.275f;
                            _screen_metric_grid_Y.Render(D3D.Device.ImmediateContext);
                            _shaderManager.RenderGrid(D3D.Device.ImmediateContext, _screen_metric_grid_Y.IndexCount, screengrid_metricmatrix, viewMatrix, _projectionMatrix);
                        }
                        else if (display_grid_type == 3)
                        {
                            _dContainer.Render(D3D.device.ImmediateContext);
                            _shaderManager.RenderObjectGrid(D3D.device.ImmediateContext, _dContainer.IndexCount, _WorldMatrixContainer, viewMatrix, _projectionMatrix);

                        }




                        //clothRect.Render(D3D.device.ImmediateContext);
                        //_shaderManager.Render_SC_cloth_shader(D3D.device.ImmediateContext, clothRect.IndexCount, _tempMatrix, viewMatrix, _projectionMatrix);
                        //_shaderManager.Render_SC_cloth_shader(D3D.device.ImmediateContext, clothRect.IndexCount, clothRect._POSITION, viewMatrix, _projectionMatrix, _vertexCount, 1);
                        //_shaderManager.RenderInstancedCloth(D3D.device.ImmediateContext, clothRect.IndexCount, clothRect.InstanceCount, clothRect._POSITION, viewMatrix, _projectionMatrix, _basicTexture.TextureResource, _DLightBufferSC_jitter_cloth, new Vector3(0, 0, 1), clothRect); // oculusRiftDir

                        for (int x = 0; x < _physics_engine_instance_x; x++)
                        {
                            for (int y = 0; y < _physics_engine_instance_y; y++)
                            {
                                for (int z = 0; z < _physics_engine_instance_z; z++)
                                {
                                    var _index = x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z);


    
                                    var countingcloth = 0;
                                    foreach (SoftBody body in _world_list[_index].SoftBodies)
                                    {
                                        Matrix _tempMatrix = WorldMatrix;

                                        for (int i = 0; i < body.VertexBodies.Count; i++)
                                        {
                                            var pos1 = body.VertexBodies[i].Position;

                                            //var oriPos = clothRect.Vertices[i].position + new Vector3(0,0,0);

                                            _world_jitter_cloth_list[countingcloth].Vertices[i].position = new Vector3(pos1.X, pos1.Y, pos1.Z);
                                        }
                                        worldMatrix_instances_jitter_cloth[_index][countingcloth] = WorldMatrix;
                                        countingcloth++;
                                        //Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                        //Matrix.Multiply(ref _tempMatrix, ref translationMatrix, out _tempMatrix);


                                        /*_tempMatrix.M11 = orientation.M11;
                                        _tempMatrix.M12 = orientation.M12;
                                        _tempMatrix.M13 = orientation.M13;
                                        _tempMatrix.M14 = orientation.M14;

                                        _tempMatrix.M21 = orientation.M21;
                                        _tempMatrix.M22 = orientation.M22;
                                        _tempMatrix.M23 = orientation.M23;
                                        _tempMatrix.M24 = orientation.M24;

                                        _tempMatrix.M31 = orientation.M31;
                                        _tempMatrix.M32 = orientation.M32;
                                        _tempMatrix.M33 = orientation.M33;
                                        _tempMatrix.M34 = orientation.M34;
                                        //_tempMatrix = WorldMatrixx;
                                        //clothRect.Render(Device.ImmediateContext, Device);
                                        //ShaderManager.RenderTouchTextureShader(Device.ImmediateContext, clothRect.IndexCount, _tempMatrix, viewMatrix, _projectionMatrix);*/
                                    }

                                }
                            }
                        }

                                    








                        for (int x = -ChunkWidth_L; x <= ChunkWidth_R; x++)
                        {
                            for (int y = -ChunkHeight_L; y <= ChunkHeight_R; y++)
                            {
                                for (int z = -ChunkDepth_L; z <= ChunkDepth_R; z++)
                                {
                                    //xx = (x * 0.1f) + posXOfDContainer;
                                    //yy = (y * 0.1f) + posYOfDContainer;
                                    //zz = (z * 0.1f) + posZOfDContainer;

                                    var xx = (float)(Math.Round((x * 0.1f) + posXOfDContainer, 1));
                                    var yy = (float)(Math.Round((y * 0.1f) + posYOfDContainer, 1));
                                    var zz = (float)(Math.Round((z * 0.1f) + posZOfDContainer, 1));

                                    //if (xPos == xx && yPos == yy && zPos == zz)
                                    //{
                                    //    _WorldMatrix = WorldMatrix;
                                    //
                                    //    _WorldMatrix.M41 = xx;
                                    //    _WorldMatrix.M42 = yy;
                                    //    _WorldMatrix.M43 = zz;
                                    //
                                    //    _arrayOfCubes[0].Render(Device.ImmediateContext);
                                    //    ShaderManager.RenderTouchTextureShader(Device.ImmediateContext, _arrayOfCubes[0].IndexCount, _WorldMatrix, viewMatrix, _projectionMatrix, _vertexCount, 1, _worldMatrix);
                                    //}

                                    for (int xu = -ChunkWidth_L; xu <= ChunkWidth_R; xu++)
                                    {
                                        for (int yu = -ChunkHeight_L; yu <= ChunkHeight_R; yu++)
                                        {
                                            for (int zu = -ChunkDepth_L; zu <= ChunkDepth_R; zu++)
                                            {
                                                float posX = (xu);
                                                float posY = (yu);
                                                float posZ = (zu);

                                                var xxi = xu;
                                                var yyi = yu;
                                                var zzi = zu;

                                                if (xxi < 0)
                                                {
                                                    xxi *= -1;
                                                    xxi = (ChunkWidth_R) + xxi;
                                                }
                                                if (yyi < 0)
                                                {
                                                    yyi *= -1;
                                                    yyi = (ChunkHeight_R) + yyi;
                                                }
                                                if (zzi < 0)
                                                {
                                                    zzi *= -1;
                                                    zzi = (ChunkDepth_R) + zzi;
                                                }

                                                int _index = xxi + (ChunkWidth_L + ChunkWidth_R + 1) * (yyi + (ChunkHeight_L + ChunkHeight_R + 1) * zzi);

                                                if (arrayX[_index] == xx && arrayY[_index] == yy && arrayZ[_index] == zz)
                                                {
                                                    draw_dcontainmentgrid[_index] = 1;
                                                    //arrayX[_index] = 999;
                                                    //arrayY[_index] = 999;
                                                    //arrayZ[_index] = 999;

                                                    _WorldMatrix = WorldMatrix;

                                                    _WorldMatrix.M41 = xx;
                                                    _WorldMatrix.M42 = yy;
                                                    _WorldMatrix.M43 = zz;

                                                    //_arrayOfCubes._LASTPOSITIONS[_index] = _WorldMatrix;

                                                    //_arrayOfCubes.Render(D3D.device.ImmediateContext);
                                                    //_shaderManager.RenderTouchTextureShader(D3D.device.ImmediateContext, _arrayOfCubes.IndexCount, _WorldMatrix, viewMatrix, _projectionMatrix, _vertexCount, 1);

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

















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
                                    tempIndex = 0;


                                    //PHYSICS CUBES
                                    _current_indexed_cube = _world_cube_list[_index];
                                    _current_indexed_cube.Render(D3D.device.ImmediateContext);
                                    _shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _current_indexed_cube.IndexCount, _current_indexed_cube.InstanceCount, _current_indexed_cube._POSITION, viewMatrix, _projectionMatrix, _desktopFrame._ShaderResource, _DLightBuffer, new Vector3(0, 0, 1), _current_indexed_cube); // oculusRiftDir

                           
                                    _world_jitter_cloth_list[_index].Render(D3D.device.ImmediateContext);
                                    _shaderManager.RenderInstancedCloth(D3D.device.ImmediateContext, _world_jitter_cloth_list[_index].IndexCount, _world_jitter_cloth_list[_index].InstanceCount, _world_jitter_cloth_list[_index]._POSITION, viewMatrix, _projectionMatrix, _desktopFrame._ShaderResource, _DLightBufferSC_jitter_cloth, new Vector3(0, 0, 1), _world_jitter_cloth_list[_index]); // oculusRiftDir


                                    

                                    //_SystemTickPerformance.Restart();

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

                                    try
                                    {
                                        //PHYSICS SPECTRUM
                                        var _cuber_02 = _world_spectrum_list[0];
                                        _cuber_02.Render(D3D.device.ImmediateContext);
                                        _shaderManager.RenderInstancedObjectSpectrum(D3D.device.ImmediateContext, _cuber_02.IndexCount, _cuber_02.InstanceCount, _cuber_02._POSITION, viewMatrix, _projectionMatrix, null, _DLightBuffer_spectrum, oculusRiftDir, _cuber_02);

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox((IntPtr)0, ex.ToString() + "", "Oculus error", 0);
                                    }

                                    //COLLIDABLE PHYSICS TERRAIN TILES
                                    //_current_indexed_cube = _world_terrain_tile_list[_index];
                                    //_world_terrain_tile_list[_index].Render(D3D.device.ImmediateContext);
                                    //_shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _current_indexed_cube.IndexCount, _current_indexed_cube.InstanceCount, _current_indexed_cube._POSITION, viewMatrix, _projectionMatrix, null, _DLightBuffer, oculusRiftDir, _current_indexed_cube);


                                    ////////////////////
                                    /////HUMAN RIG////// 
                                    ////////////////////
                                    for (int _iterator = 0; _iterator < _player_rght_hnd[tempIndex]._arrayOfInstances.Length; _iterator++) //
                                    {
                                        lengthOfLowerArmRight = _player_rght_lower_arm[tempIndex]._total_torso_height * 2.25f;
                                        lengthOfUpperArmRight = _player_rght_upper_arm[tempIndex]._total_torso_height * 2.55f;
                                        totalArmLengthRight = lengthOfLowerArmRight + lengthOfUpperArmRight;

                                        var lengthOfLowerArmLeft = _player_lft_lower_arm[tempIndex]._total_torso_height * 2.25f;
                                        var lengthOfUpperArmLeft = _player_lft_upper_arm[tempIndex]._total_torso_height * 2.55f;
                                        var totalArmLengthLeft = lengthOfLowerArmLeft + lengthOfUpperArmLeft;

                                        var connectorOfUpperArmRightOffsetMul = 1.55f;
                                        var connectorOfLowerArmRightOffsetMul = 1.0f; //0.70f
                                        var connectorOfHandOffsetMul = 1.00123f;


                                        finalRotationMatrix = originRot * rotatingMatrix * rotatingMatrixForPelvis;
                                        //THE CURRENT PIVOT POINT OF THE TORSO IS RIGHT IN THE MIDDLE OF THE TORSO ITSELF
                                        Vector3 MOVINGPOINTER = new Vector3(_player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                                        //SAVING IN MEMORY THE ORIGINAL TORSO MATRIX NOT AFFECTED BY CURRENT POSITION AND ROTATION CHANGES.
                                        Matrix _rotMatrixer = _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION;// _player_torso[tempIndex]._ORIGINPOSITION;
                                        Quaternion forTest;
                                        Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                        //FROM THE MATRIX OF ROTATION/POSITION, I GET THE QUATERNION OUT OF THAT AND CREATE THE DIRECTIONS THAT THE OBJECTS ARE ORIGINALLY FACING.
                                        var direction_feet_forward_ori_torso = _getDirection(Vector3.ForwardRH, forTest);
                                        var direction_feet_right_ori_torso = _getDirection(Vector3.Right, forTest);
                                        var direction_feet_up_ori_torso = _getDirection(Vector3.Up, forTest);

                                        //SINCE THE PIVOT POINT IS CURRENTLY IN THE MIDDLE OF THE TORSO, IT CANNOT ROTATE AT THAT POINT OTHERWISE, IT WONT FOLLOW THE PELVIS ROTATION LATER ON.
                                        //SO WE CURRENTLY ONLY OFFSET THE TORSO "MIDDLE OF SPINE APPROX" TO HALF OF THE CURRENT HEIGHT IN ORDER TO MAKE THE PIVOT POINT, APPROX WHERE THE PELVIS IS.
                                        Vector3 TORSOPIVOT = MOVINGPOINTER + -(direction_feet_up_ori_torso * (_player_torso[tempIndex]._total_torso_height * 0.5f));
                                        Vector3 NECKPIVOTORIGINAL = MOVINGPOINTER + (direction_feet_up_ori_torso * (_player_torso[tempIndex]._total_torso_height * 0.5f)); ;
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
                                        diffNormPosX = (MOVINGPOINTER.X) - _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41; //_player_torso[tempIndex]._ORIGINPOSITION.M41;
                                        diffNormPosY = (MOVINGPOINTER.Y) - _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42; //_player_torso[tempIndex]._ORIGINPOSITION.M42;
                                        diffNormPosZ = (MOVINGPOINTER.Z) - _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43; //_player_torso[tempIndex]._ORIGINPOSITION.M43;

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

                                        worldMatrix_instances_torso[tempIndex][_iterator] = matrixerer;// _player_pelvis[tempIndex].current_pos;// translationMatrix;

                                        _player_torso[tempIndex]._arrayOfInstances[_iterator].current_pos = matrixerer;

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





















                                        //RIGHT HAND IK RIG AVOID HAND LOSING PIVOT
                                        //Vector3 tempDir = new Vector3(_rightTouchMatrix.M41, _rightTouchMatrix.M42, _rightTouchMatrix.M43) - newpoint;// 
                                        /*Vector3 tempDir = somePosOfRightHand - _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION; ;// _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                                        if (tempDir.Length() > lengthOfLowerArmRight)
                                        {
                                            tempDir.Normalize();
                                            Vector3 tempVect = _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION + (tempDir * lengthOfLowerArmRight);

                                            Matrix tempMater = _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos;
                                            tempMater.M41 = tempVect.X;
                                            tempMater.M42 = tempVect.Y;
                                            tempMater.M43 = tempVect.Z;
                                            _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos = tempMater;
                                            worldMatrix_instances_r_hand[tempIndex][_iterator] = tempMater;// _player_pelvis[tempIndex].current_pos;// translationMatrix;
                                        }*/



                                        ///////////
                                        //HANDRIGHT


                                        MOVINGPOINTER = new Vector3(_player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                                        Matrix someMatRight = _rightTouchMatrix;
                                        someMatRight.M41 = handPoseRight.Position.X + MOVINGPOINTER.X;
                                        someMatRight.M42 = handPoseRight.Position.Y;// + MOVINGPOINTER.Y;
                                        someMatRight.M43 = handPoseRight.Position.Z + MOVINGPOINTER.Z;

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

                                        MOVINGPOINTER.X += OFFSETPOS.X;// + _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                        MOVINGPOINTER.Y += OFFSETPOS.Y;// + _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                        MOVINGPOINTER.Z += OFFSETPOS.Z;//+ _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;


                                        var posRHand = new Vector3(_player_rght_hnd[tempIndex]._arrayOfInstances[_iterator]._LASTPOSITION.M41, _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator]._LASTPOSITION.M42, _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator]._LASTPOSITION.M43);

                                        Vector3 tempDir = posRHand - _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION;


                                        if (tempDir.Length() > lengthOfLowerArmRight * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
                                        {
                                            tempDir.Normalize();
                                            Vector3 tempVect = _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION + (tempDir * lengthOfLowerArmRight);

                                            //Matrix matrixerer = _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos;
                                            MOVINGPOINTER.X = tempVect.X;
                                            MOVINGPOINTER.Y = tempVect.Y;
                                            MOVINGPOINTER.Z = tempVect.Z;
                                        }

                                        matrixerer = Matrix.Identity;

                                        someMatRight = someMatRight * finalRotationMatrix;

                                        //someMatRight.M41 = MOVINGPOINTER.X;
                                        //someMatRight.M42 = MOVINGPOINTER.Y;
                                        //someMatRight.M43 = MOVINGPOINTER.Z;

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

                                        matrixerer.M41 = MOVINGPOINTER.X;
                                        matrixerer.M42 = MOVINGPOINTER.Y;
                                        matrixerer.M43 = MOVINGPOINTER.Z;
                                        matrixerer.M44 = 1;

                                        _body_pos = matrixerer;
                                        //Quaternion _quat;
                                        Quaternion.RotationMatrix(ref _body_pos, out _quat);

                                        _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                        matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                                        //body.Position = new JVector(matrixerer.M41, matrixerer.M42, matrixerer.M43);
                                        //body.Orientation = matrixIn;
                                        worldMatrix_instances_r_hand[tempIndex][_iterator] = matrixerer;// _player_pelvis[tempIndex].current_pos;// translationMatrix;
                                        _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos = matrixerer;
                                        _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator]._LASTPOSITION = matrixerer;

                                        _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator]._REALCENTERPOSITION = someMatRight;

                                        if (swtch_for_last_pos[tempIndex][_iterator] >= 10)
                                        {
                                            _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator]._LASTPOSITIONFORPHYSICS = matrixerer;
                                            //swtch_for_last_pos[tempIndex][_iterator] = 0;
                                        }
                                        //swtch_for_last_pos[tempIndex][_iterator]++;




                                        //_leftTouchMatrix.M41 = handPoseLeft.Position.X + originPos.X + movePos.X;// + _hmdPoser.X;
                                        //_leftTouchMatrix.M42 = handPoseLeft.Position.Y + originPos.Y + movePos.Y;// + _hmdPoser.Y;
                                        //_leftTouchMatrix.M43 = handPoseLeft.Position.Z + originPos.Z + movePos.Z;// + _hmdPoser.Z;
















                                        //HANDLEFT
                                        MOVINGPOINTER = new Vector3(_player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

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

                                        MOVINGPOINTER.X += OFFSETPOS.X;// + _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                        MOVINGPOINTER.Y += OFFSETPOS.Y;// + _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                        MOVINGPOINTER.Z += OFFSETPOS.Z;// + _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

                                        matrixerer = Matrix.Identity;

                                        var posLHand = new Vector3(_player_lft_hnd[tempIndex]._arrayOfInstances[_iterator]._LASTPOSITION.M41, _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator]._LASTPOSITION.M42, _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator]._LASTPOSITION.M43);

                                        tempDir = posLHand - _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION;


                                        if (tempDir.Length() > lengthOfLowerArmRight * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
                                        {
                                            tempDir.Normalize();
                                            Vector3 tempVect = _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION + (tempDir * lengthOfLowerArmRight);

                                            //Matrix matrixerer = _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos;
                                            MOVINGPOINTER.X = tempVect.X;
                                            MOVINGPOINTER.Y = tempVect.Y;
                                            MOVINGPOINTER.Z = tempVect.Z;
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

                                        matrixerer.M41 = MOVINGPOINTER.X;
                                        matrixerer.M42 = MOVINGPOINTER.Y;
                                        matrixerer.M43 = MOVINGPOINTER.Z;
                                        matrixerer.M44 = 1;

                                        _body_pos = matrixerer;
                                        //Quaternion _quat;
                                        Quaternion.RotationMatrix(ref _body_pos, out _quat);

                                        _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                        matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                                        //body.Position = new JVector(matrixerer.M41, matrixerer.M42, matrixerer.M43);
                                        //body.Orientation = matrixIn;
                                        worldMatrix_instances_l_hand[tempIndex][_iterator] = matrixerer;// _player_pelvis[tempIndex].current_pos;// translationMatrix;

                                        _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos = matrixerer;
                                        _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator]._LASTPOSITION = matrixerer;

                                        _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator]._REALCENTERPOSITION = someMatLeft;

                                        if (swtch_for_last_pos[tempIndex][_iterator] >= 10)
                                        {
                                            _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator]._LASTPOSITIONFORPHYSICS = matrixerer;
                                            swtch_for_last_pos[tempIndex][_iterator] = 0;
                                        }
                                        swtch_for_last_pos[tempIndex][_iterator]++;


                                        ///////////
                                        //SHOULDER RIGHT
                                        MOVINGPOINTER = new Vector3(_player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                                        _rotatingMatrix = finalRotationMatrix;

                                        //MOVINGPOINTER = new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                                        _rotMatrixer = _player_rght_shldr[tempIndex]._ORIGINPOSITION;

                                        Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);


                                        var direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                        var direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                        var direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                                        MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_rght_shldr[tempIndex]._total_torso_height * 0.5f));
                                        _rotatingMatrix = finalRotationMatrix;
                                        //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                                        //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                        //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                        //direction_feet_up = _getDirection(Vector3.Up, otherQuat);

                                        diffNormPosX = (MOVINGPOINTER.X) - _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                        diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                        diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

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
                                        _rotatingMatrix = _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._SHOULDERROT;


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
                                        worldMatrix_instances_r_shoulder[tempIndex][_iterator] = matrixerer;// _player_pelvis[tempIndex].current_pos;// translationMatrix;

                                        _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos = matrixerer;




                                        ///////////
                                        //SHOULDER RIGHT
                                        MOVINGPOINTER = new Vector3(_player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);

                                        _rotatingMatrix = finalRotationMatrix;

                                        //MOVINGPOINTER = new Vector3(TORSOPIVOT.X, TORSOPIVOT.Y, TORSOPIVOT.Z);

                                        _rotMatrixer = _player_lft_shldr[tempIndex]._ORIGINPOSITION;

                                        Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);


                                        direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                        direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                        direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                                        MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_lft_shldr[tempIndex]._total_torso_height * 0.5f));
                                        _rotatingMatrix = finalRotationMatrix;
                                        //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                                        //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                        //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                        //direction_feet_up = _getDirection(Vector3.Up, otherQuat);

                                        diffNormPosX = (MOVINGPOINTER.X) - _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                        diffNormPosY = (MOVINGPOINTER.Y) - _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                        diffNormPosZ = (MOVINGPOINTER.Z) - _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

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
                                        _rotatingMatrix = _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._SHOULDERROT;



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
                                        worldMatrix_instances_l_shoulder[tempIndex][_iterator] = matrixerer;// _player_pelvis[tempIndex].current_pos;// translationMatrix;


                                        _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos = matrixerer;












                                        //////////////////////
                                        //ELBOW TARGET RIGHT

                                        MOVINGPOINTER = new Vector3(_player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                                        TORSOPIVOT = MOVINGPOINTER;
                                        _rotMatrixer = _player_rght_elbow_target[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION;
                                        Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                        direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                        direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                        direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                                        MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_rght_elbow_target[tempIndex]._total_torso_height * 0.5f));

                                        _rotatingMatrix = finalRotationMatrix;
                                        //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                                        //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                        //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                        //direction_feet_up = _getDirection(Vector3.Up, otherQuat);


                                        diffNormPosX = (MOVINGPOINTER.X) - _player_rght_elbow_target[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                        diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_elbow_target[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                        diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_elbow_target[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

                                        MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                                        MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * (diffNormPosY));
                                        MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * (diffNormPosZ));

                                        MOVINGPOINTER.X += OFFSETPOS.X;// + _player_rght_elbow_target._ORIGINPOSITION.M41;
                                        MOVINGPOINTER.Y += OFFSETPOS.Y;// + _player_rght_elbow_target._ORIGINPOSITION.M42;// + _player_rght_elbow_target._ORIGINPOSITION.M42;
                                        MOVINGPOINTER.Z += OFFSETPOS.Z;// + _player_rght_elbow_target._ORIGINPOSITION.M43;

                                        var someDiffX = MOVINGPOINTER.X - _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                        var someDiffY = MOVINGPOINTER.Y - _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                        var someDiffZ = MOVINGPOINTER.Z - _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

                                        var somePosOfPivotUpperArm = new Vector3(_player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M43); //new Vector3(realPIVOTOfUpperArm.X, realPIVOTOfUpperArm.Y, realPIVOTOfUpperArm.Z); ;// new Vector3(_player_rght_shldr.current_pos.M41, _player_rght_shldr.current_pos.M42, _player_rght_shldr.current_pos.M43);
                                        var somePosOfRightHand = new Vector3(_player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);

                                        _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._UPPERARMPIVOT = somePosOfPivotUpperArm;

                                        var dirShoulderToHand = somePosOfRightHand - somePosOfPivotUpperArm;
                                        dirShoulderToHand *= -1;
                                        //dirShoulderToHand.X *= -1;
                                        //dirShoulderToHand.Z *= -1;
                                        //dirShoulderToHand.Y *= -1;



                                        //MOVINGPOINTER = somePosOfPivotUpperArm + (dirShoulderToHand * 2.5f); //2.5f
                                        //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_right * -0.15f);
                                        //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * .0f);
                                        MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * 3.0f);

                                        //MOVINGPOINTER= MOVINGPOINTER + (direction_feet_right * 1);
                                        //Vector3 someOtherOFFSETPOS = MOVINGPOINTER + (direction_feet_right * 5.25f);

                                        someNewPointer = MOVINGPOINTER;

                                        var diffNormPosXElbowRight = (_player_rght_elbow_target[tempIndex]._arrayOfInstances[_iterator].current_pos.M41) - (TORSOPIVOT.X);
                                        var diffNormPosYElbowRight = (_player_rght_elbow_target[tempIndex]._arrayOfInstances[_iterator].current_pos.M42) - (TORSOPIVOT.Y);
                                        var diffNormPosZElbowRight = (_player_rght_elbow_target[tempIndex]._arrayOfInstances[_iterator].current_pos.M43) - (TORSOPIVOT.Z);

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
                                        worldMatrix_instances_r_elbow_target[tempIndex][_iterator] = matrixerer;// _player_pelvis[tempIndex].current_pos;// translationMatrix;


                                        _player_rght_elbow_target[tempIndex]._arrayOfInstances[_iterator].current_pos = matrixerer;
















                                        //////////////////////////
                                        //ELBOW TARGET RIGHT TWO

                                        MOVINGPOINTER = new Vector3(_player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                                        TORSOPIVOT = MOVINGPOINTER;

                                        _rotMatrixer = _player_rght_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos;
                                        Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                        direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                        direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                        direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                                        MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_rght_elbow_target_two[tempIndex]._total_torso_height * 0.5f));
                                        _rotatingMatrix = finalRotationMatrix;
                                        //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                                        //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                        //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                        //direction_feet_up = _getDirection(Vector3.Up, otherQuat);


                                        diffNormPosX = (MOVINGPOINTER.X) - _player_rght_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos.M41;
                                        diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos.M42;
                                        diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos.M43;


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
                                        MOVINGPOINTER.X += OFFSETPOS.X;// + _player_rght_elbow_target[tempIndex]_two._ORIGINPOSITION.M41;
                                        MOVINGPOINTER.Y += OFFSETPOS.Y;// + _player_rght_elbow_target[tempIndex]_two._ORIGINPOSITION.M42;// + _player_rght_elbow_target[tempIndex]_two._ORIGINPOSITION.M42;
                                        MOVINGPOINTER.Z += OFFSETPOS.Z;// + _player_rght_elbow_target[tempIndex]_two._ORIGINPOSITION.M43;

                                        someDiffX = MOVINGPOINTER.X - _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M41;
                                        someDiffY = MOVINGPOINTER.Y - _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M42;
                                        someDiffZ = MOVINGPOINTER.Z - _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M43;

                                        somePosOfRightHand = new Vector3(_player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);

                                        //dirShoulderToHand = somePosOfRightHand - new Vector3(_player_rght_upper_arm[tempIndex].current_pos.M41, _player_rght_upper_arm[tempIndex].current_pos.M42, _player_rght_upper_arm[tempIndex].current_pos.M43);
                                        //------------------------------------------var dirShoulderToHand = somePosOfRightHand - new Vector3(_player_rght_shldr[tempIndex].current_pos.M41, _player_rght_shldr[tempIndex].current_pos.M42, _player_rght_shldr[tempIndex].current_pos.M43);
                                        dirShoulderToHand = somePosOfRightHand - _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._UPPERARMPIVOT;
                                        //var lengthof = dirShoulderToHand.Length();
                                        dirShoulderToHand.Normalize();

                                        MOVINGPOINTER = _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._UPPERARMPIVOT + (dirShoulderToHand * totalArmLengthRight * 2);

                                        var someOffsetter = somePosOfRightHand - OFFSETPOS;
                                        var someOtherPivotPoint = MOVINGPOINTER;

                                        //MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * 1.0f);
                                        //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_forward_ori * 1.0f);

                                        someNewPointer = MOVINGPOINTER;

                                        diffNormPosXElbowRight = (_player_rght_elbow_target_two[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41) - (TORSOPIVOT.X);
                                        diffNormPosYElbowRight = (_player_rght_elbow_target_two[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42) - (TORSOPIVOT.Y);
                                        diffNormPosZElbowRight = (_player_rght_elbow_target_two[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43) - (TORSOPIVOT.Z);

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
                                        worldMatrix_instances_r_elbow_target_two[tempIndex][_iterator] = matrixerer;// _player_pelvis[tempIndex].current_pos;// translationMatrix;

                                        _player_rght_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos = matrixerer;






                                        //////////////////
                                        //UPPER ARM RIGHT

                                        MOVINGPOINTER = new Vector3(_player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                                        TORSOPIVOT = MOVINGPOINTER;
                                        _rotMatrixer = _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos;
                                        Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                        direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                        direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                        direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                                        //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_up_ori * (_player_rght_shldr[tempIndex]._total_torso_height * 0.5f));
                                        //MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * (_player_rght_shldr[tempIndex]._total_torso_height * 0.5f));

                                        _rotatingMatrix = finalRotationMatrix;

                                        Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);
                                        direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                        direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                        direction_feet_up = _getDirection(Vector3.Up, otherQuat);

                                        diffNormPosX = (MOVINGPOINTER.X) - _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                        diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                        diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

                                        realPIVOTOfUpperArm = MOVINGPOINTER;

                                        realPositionOfUpperArm = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                                        realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_up * (diffNormPosY));
                                        realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_forward * (diffNormPosZ));

                                        realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_right * (diffNormPosX));
                                        realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_up * (diffNormPosY));
                                        realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_forward * (diffNormPosZ));
                                        //realPIVOTOfUpperArm = realPIVOTOfUpperArm + (direction_feet_up_ori * (_player_rght_shldr[tempIndex]._total_torso_height * 0.5f));

                                        realPIVOTOfUpperArm = realPIVOTOfUpperArm + (direction_feet_up_ori * (_player_rght_shldr[tempIndex]._total_torso_height * connectorOfUpperArmRightOffsetMul));



                                        realPIVOTOfUpperArm.X = realPIVOTOfUpperArm.X + OFFSETPOS.X;
                                        realPIVOTOfUpperArm.Y = realPIVOTOfUpperArm.Y + OFFSETPOS.Y;
                                        realPIVOTOfUpperArm.Z = realPIVOTOfUpperArm.Z + OFFSETPOS.Z;





                                        //realPIVOTOfUpperArm.X = _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M41;
                                        //realPIVOTOfUpperArm.Y = _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M42;
                                        //realPIVOTOfUpperArm.Z = _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M43;


                                        Vector3 currentFINALPIVOTUPPERARM = realPIVOTOfUpperArm;
                                        //Vector3 currentFINALPIVOTUPPERARM = new Vector3(_player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);// realPIVOTOfUpperArm;

                                        _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._UPPERARMPIVOT = currentFINALPIVOTUPPERARM;

                                        //Vector3 somePosOfRightShoulder = new Vector3(_player_rght_shldr[tempIndex].current_pos.M41, _player_rght_shldr[tempIndex].current_pos.M42, _player_rght_shldr[tempIndex].current_pos.M43);
                                        somePosOfRightHand = new Vector3(_player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);
                                        var somePosOfUpperElbowTargetTwo = new Vector3(_player_rght_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);
                                        var somePosOfUpperElbowTargetOne = new Vector3(_player_rght_elbow_target[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_elbow_target[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_elbow_target[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);

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

                                        /*lengthOfLowerArmRight = _player_rght_lower_arm[tempIndex]._total_torso_height * 2.45f;
                                        lengthOfUpperArmRight = _player_rght_upper_arm[tempIndex]._total_torso_height * 2.55f;
                                        totalArmLengthRight = lengthOfLowerArmRight + lengthOfUpperArmRight;*/

                                        _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ARMLENGTH = totalArmLengthRight;

                                        lengthOfDirFromPivotUpperToHand = Math.Min(lengthOfDirFromPivotUpperToHand, totalArmLengthRight - totalArmLengthRight * 0.001f);

                                        var upperEquationCirCirIntersect = (lengthOfDirFromPivotUpperToHand * lengthOfDirFromPivotUpperToHand) - (lengthOfLowerArmRight * lengthOfLowerArmRight) + (lengthOfUpperArmRight * lengthOfUpperArmRight);
                                        var adjacentSolvingForX = upperEquationCirCirIntersect / (2 * lengthOfDirFromPivotUpperToHand);
                                        adjacentSolvingForX = Math.Min(adjacentSolvingForX, lengthOfUpperArmRight - lengthOfUpperArmRight * 0.001f);





                                        var resulter = Math.Pow(lengthOfUpperArmRight, 2) - Math.Pow(adjacentSolvingForX, 2);
                                        if (resulter < 0)
                                        {
                                            resulter *= -1;
                                        }
                                        //resulter = Math.Min(resulter, lengthOfUpperArmRight - lengthOfUpperArmRight * 0.001f);

                                        var oppositeSolvingForHalfA = (float)Math.Sqrt(resulter);

                                        oppositeSolvingForHalfA = Math.Min(oppositeSolvingForHalfA, lengthOfUpperArmRight - lengthOfUpperArmRight * 0.001f);




                                        someNewPointer = realPIVOTOfUpperArm + (someDirFromPivotUpperToHand * adjacentSolvingForX);
                                        Vector3.Cross(ref someDirFromPivotUpperToA, ref someDirFromPivotUpperToHand, out crossRes);
                                        crossRes.Normalize();

                                        someNewPointer = someNewPointer + (crossRes * oppositeSolvingForHalfA);

                                        diffNormPosXElbowRight = (_player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator].current_pos.M41) - (TORSOPIVOT.X);
                                        diffNormPosYElbowRight = (_player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator].current_pos.M42) - (TORSOPIVOT.Y);
                                        diffNormPosZElbowRight = (_player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator].current_pos.M43) - (TORSOPIVOT.Z);

                                        MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                                        MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                                        MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));

                                        //someNewPointer.X = someNewPointer.X;// + MOVINGPOINTER.X;
                                        //someNewPointer.Y = someNewPointer.Y;// + MOVINGPOINTER.Y;
                                        //someNewPointer.Z = someNewPointer.Z;// + MOVINGPOINTER.Z;

                                        var elbowPositionRight = someNewPointer;
                                        _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION = elbowPositionRight;


                                        var dirPivotUpperRIghtToElbowRight = elbowPositionRight - currentFINALPIVOTUPPERARM;


                                        /*currentPositionOfRightHand = currentFINALPIVOTUPPERARM;

                                        var somedir = currentPositionOfRightHand - new Vector3(_player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._UPPERARMPIVOT.X, _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._UPPERARMPIVOT.Y, _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._UPPERARMPIVOT.Z);// new Vector3(_player_rght_shldr[tempIndex].current_pos.M41, _player_rght_shldr[tempIndex].current_pos.M42, _player_rght_shldr[tempIndex].current_pos.M43);;// new Vector3(_player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);// new Vector3(_player_rght_shldr[tempIndex].current_pos.M41, _player_rght_shldr[tempIndex].current_pos.M42, _player_rght_shldr[tempIndex].current_pos.M43);

                                        if (somedir.Length() > lengthOfUpperArmRight)
                                        {
                                            somedir.Normalize();
                                            currentPositionOfRightHand = new Vector3(_player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M43) + (somedir * totalArmLengthRight);
                                        }*/



                                        var currentPositionOfUPPERARMROTATION3DPOSITION = currentFINALPIVOTUPPERARM + (dirPivotUpperRIghtToElbowRight * 0.5f);

                                        var dirElbowRightToHand = somePosOfRightHand - elbowPositionRight;

                                        dirPivotUpperRIghtToElbowRight.Normalize();
                                        dirElbowRightToHand.Normalize();

                                        Vector3 someCross0;
                                        Vector3.Cross(ref dirPivotUpperRIghtToElbowRight, ref dirElbowRightToHand, out someCross0);
                                        someCross0.Normalize();

                                        _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWCROSSVEC = someCross0;

                                        Vector3 someCross1;
                                        Vector3.Cross(ref dirPivotUpperRIghtToElbowRight, ref someCross0, out someCross1);
                                        someCross1.Normalize();

                                        //Vector3 upper = someCross1;
                                        //Vector3 forward = dirPivotUpperRIghtToElbowRight;
                                        //Vector3 upperWORLD = Vector3.Up;

                                        shoulderRotationMatrixRight = Matrix.LookAtRH(currentFINALPIVOTUPPERARM, currentFINALPIVOTUPPERARM + someCross0, dirPivotUpperRIghtToElbowRight);
                                        shoulderRotationMatrixRight.Invert();
                                        matrixerer = shoulderRotationMatrixRight;


                                        _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._SHOULDERROT = shoulderRotationMatrixRight;


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
                                        worldMatrix_instances_r_upperarm[tempIndex][_iterator] = matrixerer;// _player_pelvis[tempIndex].current_pos;// translationMatrix;


                                        _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator].current_pos = matrixerer;






                                        //////////////////
                                        //RIGHT LOWER ARM
                                        //somePosOfRightHand = new Vector3(_player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);
                                        //var somePosererDir = somePosOfRightHand - _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                                        //var someLowerRightArmPos = _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION + (somePosererDir * 0.5f);
                                        var rShldrPos = new Vector3(_player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);

                                        var dirToLowerArm = _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION - rShldrPos;
                                        dirToLowerArm.Normalize();


                                        var newpoint = rShldrPos + (dirToLowerArm * lengthOfUpperArmRight);
                                        newpoint = newpoint + (direction_feet_up_ori * (_player_rght_shldr[tempIndex]._total_torso_height * connectorOfLowerArmRightOffsetMul));

                                        var newdir = somePosOfRightHand - newpoint;
                                        newdir.Normalize();

                                        newpoint = newpoint + (newdir * lengthOfLowerArmRight * 0.5f);



                                        _rotMatrixer = _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos;
                                        Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                        direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                        direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                        direction_feet_up_ori = _getDirection(Vector3.Up, forTest);






                                        somePosOfRightHand = new Vector3(_player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);

                                        var somePosererDir = somePosOfRightHand - _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                                        var someLowerRightArmPos = _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION + (somePosererDir * 0.5f);
                                        somePosererDir.Normalize();

                                        //someCross0.Z *= -1;
                                        //Vector3 someCross1;
                                        //Vector3 someCrossLArm01;
                                        //var someCrossLArm00 = _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWCROSSVEC;
                                        //Vector3.Cross(ref somePosererDir, ref someCrossLArm00, out someCrossLArm01);
                                        //someCrossLArm01.Normalize();

                                        //Vector3 someCross0;
                                        //Vector3 someCross1;


                                        someCross0 = _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWCROSSVEC;
                                        Vector3.Cross(ref somePosererDir, ref someCross0, out someCross1);
                                        someCross1.Normalize();


                                        var theLowerArmRotationMatrix = Matrix.LookAtRH(_player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION, _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION + someCross1, somePosererDir);
                                        theLowerArmRotationMatrix.Invert();
                                        matrixerer = theLowerArmRotationMatrix;

                                        matrixerer.M41 = newpoint.X;// + OFFSETPOS.X;
                                        matrixerer.M42 = newpoint.Y;// + OFFSETPOS.Y;
                                        matrixerer.M43 = newpoint.Z;// + OFFSETPOS.Z;
                                        matrixerer.M44 = 1;


                                        _body_pos = matrixerer;
                                        Quaternion.RotationMatrix(ref _body_pos, out _quat);

                                        _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                        matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                                        //body.Position = new JVector(matrixerer.M41, matrixerer.M42, matrixerer.M43);
                                        //body.Orientation = matrixIn;
                                        worldMatrix_instances_r_lowerarm[tempIndex][_iterator] = matrixerer;// _player_pelvis[tempIndex].current_pos;// translationMatrix;


                                        _player_rght_lower_arm[tempIndex]._arrayOfInstances[_iterator].current_pos = matrixerer;


                                        /*//RIGHT HAND IK RIG AVOID HAND LOSING PIVOT
                                        //Vector3 tempDir = new Vector3(_rightTouchMatrix.M41, _rightTouchMatrix.M42, _rightTouchMatrix.M43) - newpoint;// _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION;
                                        Vector3 tempDir = somePosOfRightHand - newpoint;// _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                                        if (tempDir.Length() > lengthOfLowerArmRight)
                                        {
                                            tempDir.Normalize();
                                            Vector3 tempVect = _player_rght_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION + (tempDir * lengthOfLowerArmRight);

                                            Matrix tempMater = _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos;
                                            tempMater.M41 = tempVect.X;
                                            tempMater.M42 = tempVect.Y;
                                            tempMater.M43 = tempVect.Z;
                                            _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos = tempMater;
                                            worldMatrix_instances_r_hand[tempIndex][_iterator] = tempMater;// _player_pelvis[tempIndex].current_pos;// translationMatrix;
                                        }*/



































                                        //////////////////////
                                        //ELBOW TARGET LEFT

                                        MOVINGPOINTER = new Vector3(_player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                                        TORSOPIVOT = MOVINGPOINTER;
                                        _rotMatrixer = _player_lft_elbow_target[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION;
                                        Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                        direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                        direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                        direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                                        MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_lft_elbow_target[tempIndex]._total_torso_height * 0.5f));

                                        _rotatingMatrix = finalRotationMatrix;
                                        //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                                        //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                        //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                        //direction_feet_up = _getDirection(Vector3.Up, otherQuat);


                                        diffNormPosX = (MOVINGPOINTER.X) - _player_lft_elbow_target[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                        diffNormPosY = (MOVINGPOINTER.Y) - _player_lft_elbow_target[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                        diffNormPosZ = (MOVINGPOINTER.Z) - _player_lft_elbow_target[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

                                        MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                                        MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * (diffNormPosY));
                                        MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * (diffNormPosZ));

                                        MOVINGPOINTER.X += OFFSETPOS.X;// + _player_lft_elbow_target._ORIGINPOSITION.M41;
                                        MOVINGPOINTER.Y += OFFSETPOS.Y;// + _player_lft_elbow_target._ORIGINPOSITION.M42;// + _player_lft_elbow_target._ORIGINPOSITION.M42;
                                        MOVINGPOINTER.Z += OFFSETPOS.Z;// + _player_lft_elbow_target._ORIGINPOSITION.M43;

                                        someDiffX = MOVINGPOINTER.X - _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                        someDiffY = MOVINGPOINTER.Y - _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                        someDiffZ = MOVINGPOINTER.Z - _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

                                        somePosOfPivotUpperArm = new Vector3(_player_lft_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M43); //new Vector3(realPIVOTOfUpperArm.X, realPIVOTOfUpperArm.Y, realPIVOTOfUpperArm.Z); ;// new Vector3(_player_rght_shldr.current_pos.M41, _player_rght_shldr.current_pos.M42, _player_rght_shldr.current_pos.M43);
                                        somePosOfRightHand = new Vector3(_player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);

                                        _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._UPPERARMPIVOT = somePosOfPivotUpperArm;

                                        dirShoulderToHand = somePosOfRightHand - somePosOfPivotUpperArm;
                                        dirShoulderToHand *= -1;
                                        //dirShoulderToHand.X *= -1;
                                        //dirShoulderToHand.Z *= -1;
                                        //dirShoulderToHand.Y *= -1;

                                        // MOVINGPOINTER = somePosOfPivotUpperArm + (dirShoulderToHand * 2.5f);
                                        //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_right * -0.15f);
                                        //MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * .0f);
                                        MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * 3.0f);

                                        //MOVINGPOINTER= MOVINGPOINTER + (direction_feet_right * 1);
                                        //Vector3 someOtherOFFSETPOS = MOVINGPOINTER + (direction_feet_right * 5.25f);

                                        someNewPointer = MOVINGPOINTER;

                                        diffNormPosXElbowRight = (_player_lft_elbow_target[tempIndex]._arrayOfInstances[_iterator].current_pos.M41) - (TORSOPIVOT.X);
                                        diffNormPosYElbowRight = (_player_lft_elbow_target[tempIndex]._arrayOfInstances[_iterator].current_pos.M42) - (TORSOPIVOT.Y);
                                        diffNormPosZElbowRight = (_player_lft_elbow_target[tempIndex]._arrayOfInstances[_iterator].current_pos.M43) - (TORSOPIVOT.Z);

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
                                        worldMatrix_instances_l_elbow_target[tempIndex][_iterator] = matrixerer;// _player_pelvis[tempIndex].current_pos;// translationMatrix;


                                        _player_lft_elbow_target[tempIndex]._arrayOfInstances[_iterator].current_pos = matrixerer;







                                        //////////////////////////
                                        //ELBOW TARGET LEFT TWO






                                        MOVINGPOINTER = new Vector3(_player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                                        TORSOPIVOT = MOVINGPOINTER;

                                        _rotMatrixer = _player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos;
                                        Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                        direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                        direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                        direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                                        MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_lft_elbow_target_two[tempIndex]._total_torso_height * 0.5f));
                                        _rotatingMatrix = finalRotationMatrix;
                                        //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                                        //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                        //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                        //direction_feet_up = _getDirection(Vector3.Up, otherQuat);


                                        diffNormPosX = (MOVINGPOINTER.X) - _player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos.M41;
                                        diffNormPosY = (MOVINGPOINTER.Y) - _player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos.M42;
                                        diffNormPosZ = (MOVINGPOINTER.Z) - _player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos.M43;


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
                                        MOVINGPOINTER.X += OFFSETPOS.X;// + _player_rght_elbow_target[tempIndex]_two._ORIGINPOSITION.M41;
                                        MOVINGPOINTER.Y += OFFSETPOS.Y;// + _player_rght_elbow_target[tempIndex]_two._ORIGINPOSITION.M42;// + _player_rght_elbow_target[tempIndex]_two._ORIGINPOSITION.M42;
                                        MOVINGPOINTER.Z += OFFSETPOS.Z;// + _player_rght_elbow_target[tempIndex]_two._ORIGINPOSITION.M43;

                                        someDiffX = MOVINGPOINTER.X - _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M41;
                                        someDiffY = MOVINGPOINTER.Y - _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M42;
                                        someDiffZ = MOVINGPOINTER.Z - _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M43;

                                        somePosOfRightHand = new Vector3(_player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);

                                        //dirShoulderToHand = somePosOfRightHand - new Vector3(_player_lft_upper_arm[tempIndex].current_pos.M41, _player_lft_upper_arm[tempIndex].current_pos.M42, _player_lft_upper_arm[tempIndex].current_pos.M43);
                                        //------------------------------------------var dirShoulderToHand = somePosOfRightHand - new Vector3(_player_rght_shldr[tempIndex].current_pos.M41, _player_rght_shldr[tempIndex].current_pos.M42, _player_rght_shldr[tempIndex].current_pos.M43);
                                        dirShoulderToHand = somePosOfRightHand - _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._UPPERARMPIVOT;
                                        //var lengthof = dirShoulderToHand.Length();
                                        dirShoulderToHand.Normalize();

                                        MOVINGPOINTER = _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._UPPERARMPIVOT + (dirShoulderToHand * totalArmLengthRight * 2);

                                        someOffsetter = somePosOfRightHand - OFFSETPOS;
                                        someOtherPivotPoint = MOVINGPOINTER;

                                        //MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * 1.0f);
                                        //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_forward_ori * 1.0f);

                                        someNewPointer = MOVINGPOINTER;

                                        diffNormPosXElbowRight = (_player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41) - (TORSOPIVOT.X);
                                        diffNormPosYElbowRight = (_player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42) - (TORSOPIVOT.Y);
                                        diffNormPosZElbowRight = (_player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43) - (TORSOPIVOT.Z);

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
                                        worldMatrix_instances_l_elbow_target_two[tempIndex][_iterator] = matrixerer;// _player_pelvis[tempIndex].current_pos;// translationMatrix;

                                        _player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos = matrixerer;




                                        /*
                                        MOVINGPOINTER = new Vector3(_player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                                        TORSOPIVOT = MOVINGPOINTER;

                                        _rotMatrixer = _player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos;
                                        Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                        direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                        direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                        direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                                        MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_lft_elbow_target_two[tempIndex]._total_torso_height * 0.5f));
                                        _rotatingMatrix = finalRotationMatrix;
                                        //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

                                        //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                        //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                        //direction_feet_up = _getDirection(Vector3.Up, otherQuat);


                                        diffNormPosX = (MOVINGPOINTER.X) - _player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos.M41;
                                        diffNormPosY = (MOVINGPOINTER.Y) - _player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos.M42;
                                        diffNormPosZ = (MOVINGPOINTER.Z) - _player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos.M43;


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
                                        MOVINGPOINTER.X += OFFSETPOS.X;// + _player_rght_elbow_target[tempIndex]_two._ORIGINPOSITION.M41;
                                        MOVINGPOINTER.Y += OFFSETPOS.Y;// + _player_rght_elbow_target[tempIndex]_two._ORIGINPOSITION.M42;// + _player_rght_elbow_target[tempIndex]_two._ORIGINPOSITION.M42;
                                        MOVINGPOINTER.Z += OFFSETPOS.Z;// + _player_rght_elbow_target[tempIndex]_two._ORIGINPOSITION.M43;

                                        someDiffX = MOVINGPOINTER.X - _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M41;
                                        someDiffY = MOVINGPOINTER.Y - _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M42;
                                        someDiffZ = MOVINGPOINTER.Z - _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M43;

                                        somePosOfRightHand = new Vector3(_player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);

                                        //dirShoulderToHand = somePosOfRightHand - new Vector3(_player_rght_upper_arm[tempIndex].current_pos.M41, _player_rght_upper_arm[tempIndex].current_pos.M42, _player_rght_upper_arm[tempIndex].current_pos.M43);
                                        //------------------------------------------var dirShoulderToHand = somePosOfRightHand - new Vector3(_player_rght_shldr[tempIndex].current_pos.M41, _player_rght_shldr[tempIndex].current_pos.M42, _player_rght_shldr[tempIndex].current_pos.M43);
                                        dirShoulderToHand = somePosOfRightHand - _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._UPPERARMPIVOT;




                                        //MOVINGPOINTER = _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._UPPERARMPIVOT + (dirShoulderToHand * totalArmLengthLeft * 2);

                                        someOffsetter = somePosOfRightHand - OFFSETPOS;
                                        someOtherPivotPoint = MOVINGPOINTER;

                                        //MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * 1.0f);
                                        //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_forward_ori * 1.0f);

                                        someNewPointer = MOVINGPOINTER;

                                        diffNormPosXElbowRight = (_player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41) - (TORSOPIVOT.X);
                                        diffNormPosYElbowRight = (_player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42) - (TORSOPIVOT.Y);
                                        diffNormPosZElbowRight = (_player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43) - (TORSOPIVOT.Z);

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
                                        worldMatrix_instances_l_elbow_target_two[tempIndex][_iterator] = matrixerer;// _player_pelvis[tempIndex].current_pos;// translationMatrix;

                                        _player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos = matrixerer;
                                        */





                                        //////////////////
                                        //UPPER ARM LEFT





                                        //////////////////
                                        //UPPER ARM RIGHT

                                        MOVINGPOINTER = new Vector3(_player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                                        TORSOPIVOT = MOVINGPOINTER;
                                        _rotMatrixer = _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos;
                                        Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                        direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                        direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                        direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                                        //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_up_ori * (_player_rght_shldr[tempIndex]._total_torso_height * 0.5f));
                                        //MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * (_player_rght_shldr[tempIndex]._total_torso_height * 0.5f));

                                        _rotatingMatrix = finalRotationMatrix;

                                        Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);
                                        direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                        direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                        direction_feet_up = _getDirection(Vector3.Up, otherQuat);

                                        diffNormPosX = (MOVINGPOINTER.X) - _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                        diffNormPosY = (MOVINGPOINTER.Y) - _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                        diffNormPosZ = (MOVINGPOINTER.Z) - _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

                                        realPIVOTOfUpperArm = MOVINGPOINTER;

                                        realPositionOfUpperArm = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                                        realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_up * (diffNormPosY));
                                        realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_forward * (diffNormPosZ));

                                        realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_right * (diffNormPosX));
                                        realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_up * (diffNormPosY));
                                        realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_forward * (diffNormPosZ));
                                        //realPIVOTOfUpperArm = realPIVOTOfUpperArm + (direction_feet_up_ori * (_player_rght_shldr[tempIndex]._total_torso_height * 0.5f));

                                        realPIVOTOfUpperArm = realPIVOTOfUpperArm + (direction_feet_up_ori * (_player_lft_shldr[tempIndex]._total_torso_height * connectorOfUpperArmRightOffsetMul));



                                        realPIVOTOfUpperArm.X = realPIVOTOfUpperArm.X + OFFSETPOS.X;
                                        realPIVOTOfUpperArm.Y = realPIVOTOfUpperArm.Y + OFFSETPOS.Y;
                                        realPIVOTOfUpperArm.Z = realPIVOTOfUpperArm.Z + OFFSETPOS.Z;





                                        //realPIVOTOfUpperArm.X = _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M41;
                                        //realPIVOTOfUpperArm.Y = _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M42;
                                        //realPIVOTOfUpperArm.Z = _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M43;


                                        currentFINALPIVOTUPPERARM = realPIVOTOfUpperArm;
                                        //Vector3 currentFINALPIVOTUPPERARM = new Vector3(_player_lft_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);// realPIVOTOfUpperArm;

                                        _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._UPPERARMPIVOT = currentFINALPIVOTUPPERARM;

                                        //Vector3 somePosOfRightShoulder = new Vector3(_player_lft_shldr[tempIndex].current_pos.M41, _player_lft_shldr[tempIndex].current_pos.M42, _player_lft_shldr[tempIndex].current_pos.M43);
                                        somePosOfRightHand = new Vector3(_player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);
                                        somePosOfUpperElbowTargetTwo = new Vector3(_player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);
                                        somePosOfUpperElbowTargetOne = new Vector3(_player_lft_elbow_target[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_elbow_target[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_elbow_target[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);

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

                                        /*lengthOfLowerArmRight = _player_rght_lower_arm[tempIndex]._total_torso_height * 2.45f;
                                        lengthOfUpperArmRight = _player_lft_upper_arm[tempIndex]._total_torso_height * 2.55f;
                                        totalArmLengthRight = lengthOfLowerArmRight + lengthOfUpperArmRight;*/

                                        _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ARMLENGTH = totalArmLengthRight;

                                        lengthOfDirFromPivotUpperToHand = Math.Min(lengthOfDirFromPivotUpperToHand, totalArmLengthRight - totalArmLengthRight * 0.001f);

                                        upperEquationCirCirIntersect = (lengthOfDirFromPivotUpperToHand * lengthOfDirFromPivotUpperToHand) - (lengthOfLowerArmRight * lengthOfLowerArmRight) + (lengthOfUpperArmRight * lengthOfUpperArmRight);
                                        adjacentSolvingForX = upperEquationCirCirIntersect / (2 * lengthOfDirFromPivotUpperToHand);
                                        adjacentSolvingForX = Math.Min(adjacentSolvingForX, lengthOfUpperArmRight - lengthOfUpperArmRight * 0.001f);





                                        resulter = Math.Pow(lengthOfUpperArmRight, 2) - Math.Pow(adjacentSolvingForX, 2);
                                        if (resulter < 0)
                                        {
                                            resulter *= -1;
                                        }
                                        //resulter = Math.Min(resulter, lengthOfUpperArmRight - lengthOfUpperArmRight * 0.001f);

                                        oppositeSolvingForHalfA = (float)Math.Sqrt(resulter);

                                        oppositeSolvingForHalfA = Math.Min(oppositeSolvingForHalfA, lengthOfUpperArmRight - lengthOfUpperArmRight * 0.001f);




                                        someNewPointer = realPIVOTOfUpperArm + (someDirFromPivotUpperToHand * adjacentSolvingForX);
                                        Vector3.Cross(ref someDirFromPivotUpperToA, ref someDirFromPivotUpperToHand, out crossRes);
                                        crossRes.Normalize();

                                        someNewPointer = someNewPointer + (crossRes * oppositeSolvingForHalfA);

                                        diffNormPosXElbowRight = (_player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator].current_pos.M41) - (TORSOPIVOT.X);
                                        diffNormPosYElbowRight = (_player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator].current_pos.M42) - (TORSOPIVOT.Y);
                                        diffNormPosZElbowRight = (_player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator].current_pos.M43) - (TORSOPIVOT.Z);

                                        MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                                        MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                                        MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));

                                        //someNewPointer.X = someNewPointer.X;// + MOVINGPOINTER.X;
                                        //someNewPointer.Y = someNewPointer.Y;// + MOVINGPOINTER.Y;
                                        //someNewPointer.Z = someNewPointer.Z;// + MOVINGPOINTER.Z;

                                        elbowPositionRight = someNewPointer;
                                        _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION = elbowPositionRight;


                                        dirPivotUpperRIghtToElbowRight = elbowPositionRight - currentFINALPIVOTUPPERARM;


                                        /*currentPositionOfRightHand = currentFINALPIVOTUPPERARM;

                                        var somedir = currentPositionOfRightHand - new Vector3(_player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._UPPERARMPIVOT.X, _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._UPPERARMPIVOT.Y, _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._UPPERARMPIVOT.Z);// new Vector3(_player_rght_shldr[tempIndex].current_pos.M41, _player_rght_shldr[tempIndex].current_pos.M42, _player_rght_shldr[tempIndex].current_pos.M43);;// new Vector3(_player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);// new Vector3(_player_rght_shldr[tempIndex].current_pos.M41, _player_rght_shldr[tempIndex].current_pos.M42, _player_rght_shldr[tempIndex].current_pos.M43);

                                        if (somedir.Length() > lengthOfUpperArmRight)
                                        {
                                            somedir.Normalize();
                                            currentPositionOfRightHand = new Vector3(_player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M43) + (somedir * totalArmLengthRight);
                                        }*/



                                        currentPositionOfUPPERARMROTATION3DPOSITION = currentFINALPIVOTUPPERARM + (dirPivotUpperRIghtToElbowRight * 0.5f);

                                        dirElbowRightToHand = somePosOfRightHand - elbowPositionRight;

                                        dirPivotUpperRIghtToElbowRight.Normalize();
                                        dirElbowRightToHand.Normalize();

                                        //Vector3 someCross0;
                                        Vector3.Cross(ref dirPivotUpperRIghtToElbowRight, ref dirElbowRightToHand, out someCross0);
                                        someCross0.Normalize();

                                        _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWCROSSVEC = someCross0;

                                        //Vector3 someCross1;
                                        Vector3.Cross(ref dirPivotUpperRIghtToElbowRight, ref someCross0, out someCross1);
                                        someCross1.Normalize();

                                        //Vector3 upper = someCross1;
                                        //Vector3 forward = dirPivotUpperRIghtToElbowRight;
                                        //Vector3 upperWORLD = Vector3.Up;

                                        shoulderRotationMatrixRight = Matrix.LookAtRH(currentFINALPIVOTUPPERARM, currentFINALPIVOTUPPERARM + someCross0, dirPivotUpperRIghtToElbowRight);
                                        shoulderRotationMatrixRight.Invert();
                                        matrixerer = shoulderRotationMatrixRight;


                                        _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._SHOULDERROT = shoulderRotationMatrixRight;


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
                                        worldMatrix_instances_l_upperarm[tempIndex][_iterator] = matrixerer;// _player_pelvis[tempIndex].current_pos;// translationMatrix;
                                        _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator].current_pos = matrixerer;





























                                        /*
                                        MOVINGPOINTER = new Vector3(_player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                                        TORSOPIVOT = MOVINGPOINTER;
                                        _rotMatrixer = _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos;
                                        Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                        direction_feet_forward_ori = _getDirection(Vector3.ForwardRH, forTest);
                                        direction_feet_right_ori = _getDirection(Vector3.Right, forTest);
                                        direction_feet_up_ori = _getDirection(Vector3.Up, forTest);

                                        //MOVINGPOINTER = MOVINGPOINTER + (direction_feet_up_ori * (_player_lft_shldr[tempIndex]._total_torso_height * 0.5f));
                                        //MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * (_player_lft_shldr[tempIndex]._total_torso_height * 0.5f));

                                        _rotatingMatrix = finalRotationMatrix;

                                        //Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);
                                        //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                                        //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                                        //direction_feet_up = _getDirection(Vector3.Up, otherQuat);

                                        diffNormPosX = (MOVINGPOINTER.X) - _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                                        diffNormPosY = (MOVINGPOINTER.Y) - _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                                        diffNormPosZ = (MOVINGPOINTER.Z) - _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;

                                        realPIVOTOfUpperArm = MOVINGPOINTER;

                                        realPositionOfUpperArm = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                                        realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_up * (diffNormPosY));
                                        realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_forward * (diffNormPosZ));

                                        realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_right * (diffNormPosX));
                                        realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_up * (diffNormPosY));
                                        realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_forward * (diffNormPosZ));
                                        //realPIVOTOfUpperArm = realPIVOTOfUpperArm + (direction_feet_up_ori * (_player_lft_shldr[tempIndex]._total_torso_height * 0.5f));

                                        realPIVOTOfUpperArm = realPIVOTOfUpperArm + (direction_feet_up_ori * (_player_lft_shldr[tempIndex]._total_torso_height * connectorOfUpperArmRightOffsetMul));


                                        realPIVOTOfUpperArm.X = realPIVOTOfUpperArm.X + OFFSETPOS.X;
                                        realPIVOTOfUpperArm.Y = realPIVOTOfUpperArm.Y + OFFSETPOS.Y;
                                        realPIVOTOfUpperArm.Z = realPIVOTOfUpperArm.Z + OFFSETPOS.Z;

                                        currentFINALPIVOTUPPERARM = realPIVOTOfUpperArm;
                                        //Vector3 currentFINALPIVOTUPPERARM = new Vector3(_player_lft_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);// realPIVOTOfUpperArm;

                                        _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._UPPERARMPIVOT = currentFINALPIVOTUPPERARM;

                                        //Vector3 somePosOfRightShoulder = new Vector3(_player_lft_shldr[tempIndex].current_pos.M41, _player_lft_shldr[tempIndex].current_pos.M42, _player_lft_shldr[tempIndex].current_pos.M43);
                                        somePosOfRightHand = new Vector3(_player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);
                                        somePosOfUpperElbowTargetTwo = new Vector3(_player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_elbow_target_two[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);
                                        somePosOfUpperElbowTargetOne = new Vector3(_player_lft_elbow_target[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_elbow_target[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_elbow_target[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);

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



                                        _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ARMLENGTH = totalArmLengthLeft;

                                        lengthOfDirFromPivotUpperToHand = Math.Min(lengthOfDirFromPivotUpperToHand, totalArmLengthLeft - totalArmLengthLeft * 0.001f);

                                        upperEquationCirCirIntersect = (lengthOfDirFromPivotUpperToHand * lengthOfDirFromPivotUpperToHand) - (lengthOfLowerArmLeft * lengthOfLowerArmLeft) + (lengthOfUpperArmLeft * lengthOfUpperArmLeft);
                                        adjacentSolvingForX = upperEquationCirCirIntersect / (2 * lengthOfDirFromPivotUpperToHand);

                                        adjacentSolvingForX = Math.Min(adjacentSolvingForX, lengthOfLowerArmLeft - lengthOfLowerArmLeft * 0.001f);


                                        resulter = Math.Pow(lengthOfUpperArmLeft, 2) - Math.Pow(adjacentSolvingForX, 2);
                                        if (resulter < 0)
                                        {
                                            resulter *= -1;
                                        }

                                        oppositeSolvingForHalfA = (float)Math.Sqrt(resulter);

                                        oppositeSolvingForHalfA = Math.Min(oppositeSolvingForHalfA, lengthOfUpperArmLeft - lengthOfUpperArmLeft * 0.001f);

                                        someNewPointer = realPIVOTOfUpperArm + (someDirFromPivotUpperToHand * adjacentSolvingForX);
                                        Vector3.Cross(ref someDirFromPivotUpperToA, ref someDirFromPivotUpperToHand, out crossRes);
                                        crossRes.Normalize();

                                        someNewPointer = someNewPointer + (crossRes * oppositeSolvingForHalfA);

                                        diffNormPosXElbowRight = (_player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator].current_pos.M41) - (TORSOPIVOT.X);
                                        diffNormPosYElbowRight = (_player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator].current_pos.M42) - (TORSOPIVOT.Y);
                                        diffNormPosZElbowRight = (_player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator].current_pos.M43) - (TORSOPIVOT.Z);

                                        MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                                        MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                                        MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));

                                        //someNewPointer.X = someNewPointer.X;// + MOVINGPOINTER.X;
                                        //someNewPointer.Y = someNewPointer.Y;// + MOVINGPOINTER.Y;
                                        //someNewPointer.Z = someNewPointer.Z;// + MOVINGPOINTER.Z;

                                        var elbowPositionLeft = someNewPointer;
                                        _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION = elbowPositionLeft;


                                        var dirPivotUpperRIghtToElbowLeft = elbowPositionLeft - currentFINALPIVOTUPPERARM;

                                        var currentPositionOfUPPERARMROTATION3DPOSITIONLeft = currentFINALPIVOTUPPERARM + (dirPivotUpperRIghtToElbowLeft * 0.5f);

                                       var dirElbowRightToHandLeft = somePosOfRightHand - elbowPositionLeft;

                                        dirPivotUpperRIghtToElbowLeft.Normalize();
                                        dirElbowRightToHandLeft.Normalize();
                                        //Vector3 someCross0;
                                        Vector3.Cross(ref dirPivotUpperRIghtToElbowLeft, ref dirElbowRightToHandLeft, out someCross0);
                                        someCross0.Normalize();

                                        _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWCROSSVEC = someCross0;
                                        //Vector3 someCross1;

                                        Vector3.Cross(ref dirPivotUpperRIghtToElbowLeft, ref someCross0, out someCross1);
                                        someCross1.Normalize();

                                        //Vector3 upper = someCross1;
                                        //Vector3 forward = dirPivotUpperRIghtToElbowRight;
                                        //Vector3 upperWORLD = Vector3.Up;

                                        shoulderRotationMatrixRight = Matrix.LookAtRH(currentFINALPIVOTUPPERARM, currentFINALPIVOTUPPERARM + someCross0, dirPivotUpperRIghtToElbowLeft);
                                        shoulderRotationMatrixRight.Invert();
                                        matrixerer = shoulderRotationMatrixRight;


                                        _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._SHOULDERROT = shoulderRotationMatrixRight;


                                        matrixerer.M41 = currentPositionOfUPPERARMROTATION3DPOSITIONLeft.X;// + OFFSETPOS.X;
                                        matrixerer.M42 = currentPositionOfUPPERARMROTATION3DPOSITIONLeft.Y;// + OFFSETPOS.Y;
                                        matrixerer.M43 = currentPositionOfUPPERARMROTATION3DPOSITIONLeft.Z;// + OFFSETPOS.Z;
                                        matrixerer.M44 = 1;


                                        _body_pos = matrixerer;
                                        Quaternion.RotationMatrix(ref _body_pos, out _quat);

                                        _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                        matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                                        //body.Position = new JVector(matrixerer.M41, matrixerer.M42, matrixerer.M43);
                                        //body.Orientation = matrixIn;
                                        worldMatrix_instances_l_upperarm[tempIndex][_iterator] = matrixerer;// _player_pelvis[tempIndex].current_pos;// translationMatrix;


                                        _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator].current_pos = matrixerer;
                                        */


                                        /////////////////
                                        //LEFT LOWER ARM
                                        rShldrPos = new Vector3(_player_lft_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_shldr[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);

                                        dirToLowerArm = _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION - rShldrPos;
                                        dirToLowerArm.Normalize();


                                        newpoint = rShldrPos + (dirToLowerArm * lengthOfUpperArmRight);

                                        newpoint = newpoint + (direction_feet_up_ori * (_player_lft_shldr[tempIndex]._total_torso_height * connectorOfLowerArmRightOffsetMul));



                                        newdir = somePosOfRightHand - newpoint;
                                        newdir.Normalize();

                                        newpoint = newpoint + (newdir * lengthOfLowerArmRight * 0.5f);


                                        somePosOfRightHand = new Vector3(_player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_hnd[tempIndex]._arrayOfInstances[_iterator].current_pos.M43);

                                        somePosererDir = somePosOfRightHand - _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                                        someLowerRightArmPos = _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION + (somePosererDir * 0.5f);
                                        somePosererDir.Normalize();

                                        someCross0 = _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWCROSSVEC;
                                        Vector3.Cross(ref somePosererDir, ref someCross0, out someCross1);
                                        someCross1.Normalize();


                                        theLowerArmRotationMatrix = Matrix.LookAtRH(_player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION, _player_lft_upper_arm[tempIndex]._arrayOfInstances[_iterator]._ELBOWPOSITION + someCross1, somePosererDir);
                                        theLowerArmRotationMatrix.Invert();
                                        matrixerer = theLowerArmRotationMatrix;

                                        matrixerer.M41 = newpoint.X;// + OFFSETPOS.X;
                                        matrixerer.M42 = newpoint.Y;// + OFFSETPOS.Y;
                                        matrixerer.M43 = newpoint.Z;// + OFFSETPOS.Z;
                                        matrixerer.M44 = 1;


                                        _body_pos = matrixerer;
                                        Quaternion.RotationMatrix(ref _body_pos, out _quat);

                                        _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                        matrixIn = JMatrix.CreateFromQuaternion(_other_quat);

                                        //body.Position = new JVector(matrixerer.M41, matrixerer.M42, matrixerer.M43);
                                        //body.Orientation = matrixIn;
                                        worldMatrix_instances_l_lowerarm[tempIndex][_iterator] = matrixerer;// _player_pelvis[tempIndex].current_pos;// translationMatrix;

                                        _player_lft_lower_arm[tempIndex]._arrayOfInstances[_iterator].current_pos = matrixerer;




























                                        //PELVIS
                                        var _cuber_pelvis = _player_pelvis[tempIndex];
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
                                        worldMatrix_instances_pelvis[tempIndex][_iterator] = matrixerer;// _player_pelvis[tempIndex].current_pos;// translationMatrix;

                                        _player_pelvis[tempIndex]._arrayOfInstances[_iterator].current_pos = matrixerer;

                                    }














































                                    #region
                                    ///////////
                                    //TORSO////
                                    ///////////
                                    var _cuber_001 = _player_torso[tempIndex];
                                    _cuber_001.Render(D3D.device.ImmediateContext);
                                    _shaderManager._rend_torso(D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_torso_BUFFER, oculusRiftDir, _cuber_001);

                                    //PLAYER PELVIS
                                    _cuber_001 = _player_pelvis[tempIndex];
                                    _cuber_001.Render(D3D.device.ImmediateContext);
                                    _shaderManager._rend_pelvis(D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_pelvis_BUFFER, oculusRiftDir, _cuber_001);

                                    _player_rght_hnd[tempIndex].Render(D3D.device.ImmediateContext);
                                    _shaderManager._rend_rgt_hnd(D3D.device.ImmediateContext, _player_rght_hnd[tempIndex].IndexCount, _player_rght_hnd[tempIndex].InstanceCount, _player_rght_hnd[tempIndex]._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_rght_hnd_BUFFER, oculusRiftDir, _player_rght_hnd[tempIndex]);

                                    _player_lft_hnd[tempIndex].Render(D3D.device.ImmediateContext);
                                    _shaderManager._rend_lft_hnd(D3D.device.ImmediateContext, _player_lft_hnd[tempIndex].IndexCount, _player_lft_hnd[tempIndex].InstanceCount, _player_lft_hnd[tempIndex]._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_hnd_BUFFER, oculusRiftDir, _player_lft_hnd[tempIndex]);

                                    _cuber_001 = _player_rght_shldr[tempIndex];
                                    _cuber_001.Render(D3D.device.ImmediateContext);
                                    _shaderManager._rend_rgt_shldr(D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_rght_shldr_BUFFER, oculusRiftDir, _cuber_001);

                                    _cuber_001 = _player_rght_lower_arm[tempIndex];
                                    _cuber_001.Render(D3D.device.ImmediateContext);
                                    _shaderManager._rend_rgt_lower_arm(D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_rght_lower_arm_BUFFER, oculusRiftDir, _cuber_001);

                                    _cuber_001 = _player_rght_upper_arm[tempIndex];
                                    _cuber_001.Render(D3D.device.ImmediateContext);
                                    _shaderManager._rend_rgt_upper_arm(D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_rght_upper_arm_BUFFER, oculusRiftDir, _cuber_001);


                                    _cuber_001 = _player_lft_shldr[tempIndex];
                                    _cuber_001.Render(D3D.device.ImmediateContext);
                                    _shaderManager._rend_lft_shldr(D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_shldr_BUFFER, oculusRiftDir, _cuber_001);

                                    _cuber_001 = _player_lft_lower_arm[tempIndex];
                                    _cuber_001.Render(D3D.device.ImmediateContext);
                                    _shaderManager._rend_lft_lower_arm(D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_lower_arm_BUFFER, oculusRiftDir, _cuber_001);

                                    _cuber_001 = _player_lft_upper_arm[tempIndex];
                                    _cuber_001.Render(D3D.device.ImmediateContext);
                                    _shaderManager._rend_lft_upper_arm(D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_upper_arm_BUFFER, oculusRiftDir, _cuber_001);
                                    #endregion


                                    /*
                                    var _cuber_01 = _player_rght_elbow_target[tempIndex];
                                    _cuber_01.Render(D3D.device.ImmediateContext);
                                    _shaderManager._rend_rgt_elbow_targ(D3D.device.ImmediateContext, _cuber_01.IndexCount, _cuber_01.InstanceCount, _cuber_01._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_hnd_BUFFER, oculusRiftDir, _cuber_01);
                                    
                                    _cuber_01 = _player_rght_elbow_target_two[tempIndex];
                                    _cuber_01.Render(D3D.device.ImmediateContext);
                                    _shaderManager._rend_rgt_elbow_targ_two(D3D.device.ImmediateContext, _cuber_01.IndexCount, _cuber_01.InstanceCount, _cuber_01._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_hnd_BUFFER, oculusRiftDir, _cuber_01);
                                    
                                    _cuber_01 = _player_lft_elbow_target[tempIndex];
                                    _cuber_01.Render(D3D.device.ImmediateContext);
                                    _shaderManager._rend_lft_elbow_targ(D3D.device.ImmediateContext, _cuber_01.IndexCount, _cuber_01.InstanceCount, _cuber_01._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_hnd_BUFFER, oculusRiftDir, _cuber_01);
                                    
                                    _cuber_01 = _player_lft_elbow_target_two[tempIndex];
                                    _cuber_01.Render(D3D.device.ImmediateContext);
                                    _shaderManager._rend_lft_elbow_targ_two(D3D.device.ImmediateContext, _cuber_01.IndexCount, _cuber_01.InstanceCount, _cuber_01._POSITION, viewMatrix, _projectionMatrix, null, _SC_modL_lft_hnd_BUFFER, oculusRiftDir, _cuber_01);
                                    */
















                                    /*
                                    //HEAD
                                    _SC_modL_head_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
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
                }
                catch (Exception ex)
                {
                    MessageBox((IntPtr)0, "" + ex.ToString(), "Oculus Error", 0);
                }



                //TERRAIN CUBE
                _terrain[0]._WORLDMATRIXINSTANCES = worldMatrix_instances_terrain[0];
                _terrain[0]._POSITION = worldMatrix_base[0];

                var cuber = _terrain[0];
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

                    var dirInstance = _newgetdirforward(_testQuater);
                    cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                    cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                    cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                    cuber.instancesDataForward[i].rotation.W = 1;

                    dirInstance = -_newgetdirleft(_testQuater);
                    cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                    cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                    cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                    cuber.instancesDataRIGHT[i].rotation.W = 1;

                    dirInstance = dirInstance = _newgetdirup(_testQuater);
                    cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                    cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                    cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                    cuber.instancesDataUP[i].rotation.W = 1;
                }
                //END OF


                /*
                //DZAXIS CUBE
                _zAxis[0]._WORLDMATRIXINSTANCES = worldMatrix_instances_DZgrid[0];
                _zAxis[0]._POSITION = worldMatrix_base[0];

                var cuberer = _zAxis[0];
                var instancerser = cuberer.instances;
                var sometesterer = cuberer._WORLDMATRIXINSTANCES;

                for (int i = 0; i < instancerser.Length; i++)
                {
                    float xxx = sometesterer[i].M41;
                    float yyy = sometesterer[i].M42;
                    float zzz = sometesterer[i].M43;

                    cuberer.instances[i].position.X = xxx;
                    cuberer.instances[i].position.Y = yyy;
                    cuberer.instances[i].position.Z = zzz;
                    cuberer.instances[i].position.W = 1;
                    Quaternion.RotationMatrix(ref sometesterer[i], out _testQuater);

                    var dirInstance = _newgetdirforward(_testQuater);
                    cuberer.instancesDataForward[i].rotation.X = dirInstance.X;
                    cuberer.instancesDataForward[i].rotation.Y = dirInstance.Y;
                    cuberer.instancesDataForward[i].rotation.Z = dirInstance.Z;
                    cuberer.instancesDataForward[i].rotation.W = 1;

                    dirInstance = -_newgetdirleft(_testQuater);
                    cuberer.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                    cuberer.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                    cuberer.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                    cuberer.instancesDataRIGHT[i].rotation.W = 1;

                    dirInstance = dirInstance = _newgetdirup(_testQuater);
                    cuberer.instancesDataUP[i].rotation.X = dirInstance.X;
                    cuberer.instancesDataUP[i].rotation.Y = dirInstance.Y;
                    cuberer.instancesDataUP[i].rotation.Z = dirInstance.Z;
                    cuberer.instancesDataUP[i].rotation.W = 1;
                }
                //END OF*/



                //PHYSICS SPECTRUM
                _world_spectrum_list[0]._WORLDMATRIXINSTANCES = worldMatrix_instances_spectrum[0];
                _world_spectrum_list[0]._POSITION = worldMatrix_base[0];
                //END OF 

                //PHYSICS SPECTRUM
                var _world_spectrum = _world_spectrum_list[0];
                var instances = _world_spectrum.instances;
                var worldmatinst = _world_spectrum._WORLDMATRIXINSTANCES;

                for (int i = 0; i < instances.Length; i++)
                {
                    float xxx = worldmatinst[i].M41;
                    float yyy = worldmatinst[i].M42;
                    float zzz = worldmatinst[i].M43;

                    _world_spectrum.instances[i].position.X = xxx;
                    _world_spectrum.instances[i].position.Y = yyy;
                    _world_spectrum.instances[i].position.Z = zzz;
                    _world_spectrum.instances[i].position.W = 1;
                    Quaternion.RotationMatrix(ref worldmatinst[i], out _testQuater);

                    var dirInstance = _newgetdirforward(_testQuater);
                    _world_spectrum.instancesDataForward[i].rotation.X = dirInstance.X;
                    _world_spectrum.instancesDataForward[i].rotation.Y = dirInstance.Y;
                    _world_spectrum.instancesDataForward[i].rotation.Z = dirInstance.Z;
                    _world_spectrum.instancesDataForward[i].rotation.W = 1;

                    dirInstance = -_newgetdirleft(_testQuater);
                    _world_spectrum.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                    _world_spectrum.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                    _world_spectrum.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                    _world_spectrum.instancesDataRIGHT[i].rotation.W = 1;

                    dirInstance = dirInstance = _newgetdirup(_testQuater);
                    _world_spectrum.instancesDataUP[i].rotation.X = dirInstance.X;
                    _world_spectrum.instancesDataUP[i].rotation.Y = dirInstance.Y;
                    _world_spectrum.instancesDataUP[i].rotation.Z = dirInstance.Z;
                    _world_spectrum.instancesDataUP[i].rotation.W = 1;
                }
                //END OF*







                //PHYSICS SCREENS
                _world_screen_list[0]._WORLDMATRIXINSTANCES = worldMatrix_instances_screens[0];
                _world_screen_list[0]._POSITION = worldMatrix_base[0];
                //END OF 
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

                    var dirInstance = _newgetdirforward(_testQuater);
                    cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                    cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                    cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                    cuber.instancesDataForward[i].rotation.W = 1;

                    dirInstance = -_newgetdirleft(_testQuater);
                    cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                    cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                    cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                    cuber.instancesDataRIGHT[i].rotation.W = 1;

                    dirInstance = dirInstance = _newgetdirup(_testQuater);
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

                    var xxx = sometester[i].M41;
                    var yyy = sometester[i].M42;
                    var zzz = sometester[i].M43;

                    cuber.instances[i].position.X = xxx;
                    cuber.instances[i].position.Y = yyy;
                    cuber.instances[i].position.Z = zzz;
                    cuber.instances[i].position.W = 1;
                    Quaternion.RotationMatrix(ref sometester[i], out _testQuater);

                    var dirInstance = _newgetdirforward(_testQuater);
                    cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                    cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                    cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                    cuber.instancesDataForward[i].rotation.W = 1;

                    dirInstance = -_newgetdirleft(_testQuater);
                    cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                    cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                    cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                    cuber.instancesDataRIGHT[i].rotation.W = 1;

                    dirInstance = dirInstance = _newgetdirup(_testQuater);
                    cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                    cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                    cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                    cuber.instancesDataUP[i].rotation.W = 1;
                }
















                for (int x = 0; x < _physics_engine_instance_x; x++)
                {
                    for (int y = 0; y < _physics_engine_instance_y; y++)
                    {
                        for (int z = 0; z < _physics_engine_instance_z; z++)
                        {
                            var indexer00 = x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z);
                            tempIndex = 0;









                            /*
                            Vector3 screenPos = new Vector3(worldMatrix_instances_screens[0][0].M41, worldMatrix_instances_screens[0][0].M42, worldMatrix_instances_screens[0][0].M43);
                            float distancer;
                            Vector3.Distance(ref screenPos, ref OFFSETPOS, out distancer);

                            //SCREEN CORNERS
                            //SETTING SCREEN CORNER POSITION
                            screen_mat = worldMatrix_instances_screens[0][0];


                            Quaternion.RotationMatrix(ref screen_mat, out _testQuater);
                            _testQuater.Normalize();

                            var screenNormalForwarder = _newgetdirforward(_testQuater);
                            screenNormalForwarder.Normalize();

                            var screenNormalRighter = -_newgetdirleft(_testQuater);
                            screenNormalRighter.Normalize();

                            var screenNormalToper = _newgetdirup(_testQuater);
                            screenNormalToper.Normalize();

                            for (int i = 0; i < _screenDirMatrix.Length; i++)
                            {
                                Vector3 vert = screenPos + (screenNormalForwarder * _screenDirMatrix[0][i].M43);
                                vert = vert + (screenNormalRighter * _screenDirMatrix[0][i].M41);
                                vert = vert + (screenNormalToper * _screenDirMatrix[0][i].M42);

                                _screenDirMatrix_correct_pos[0][i].M41 = vert.X;
                                _screenDirMatrix_correct_pos[0][i].M42 = vert.Y;
                                _screenDirMatrix_correct_pos[0][i].M43 = vert.Z;

                                worldMatrix_instances_screen_assets[0][i] = screen_mat;

                                worldMatrix_instances_screen_assets[0][i].M41 = _screenDirMatrix_correct_pos[0][i].M41;
                                worldMatrix_instances_screen_assets[0][i].M42 = _screenDirMatrix_correct_pos[0][i].M42;
                                worldMatrix_instances_screen_assets[0][i].M43 = _screenDirMatrix_correct_pos[0][i].M43;
                            }

                            screenNormal = -_newgetdirleft(_testQuater);
                            screenNormal.Normalize();

                            var screenNormalRight = new Vector3(screenNormal.X, screenNormal.Y, screenNormal.Z);

                            screenNormal  = _newgetdirup(_testQuater);
                            screenNormal.Normalize();

                            var screenNormalTop = new Vector3(screenNormal.X, screenNormal.Y, screenNormal.Z);

                            var currentScreenPos = new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43);




                            var newDirRight = (screenNormalRight) * sizeWidtherer; // + screenNormalTop
                            currentScreenPos -= newDirRight;

                            var newDirUp = (screenNormalTop) * sizeheighterer; // + screenNormalTop
                            currentScreenPos -= newDirUp;

                            Matrix resulter = matroxer2;
                            resulter.M41 = currentScreenPos.X;
                            resulter.M42 = currentScreenPos.Y;
                            resulter.M43 = currentScreenPos.Z;
                            _screenDirMatrix_correct_pos[0][0] = resulter;


                            
                            currentScreenPos = new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43);
                            newDirRight = (screenNormalRight) * sizeWidtherer; // + screenNormalTop
                            currentScreenPos -= newDirRight;

                            newDirUp = (screenNormalTop) * sizeheighterer; // + screenNormalTop
                            currentScreenPos += newDirUp;

                            resulter = matroxer2;
                            resulter.M41 = currentScreenPos.X;
                            resulter.M42 = currentScreenPos.Y;
                            resulter.M43 = currentScreenPos.Z;
                            _screenDirMatrix_correct_pos[0][1] = resulter;


                            currentScreenPos = new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43);
                            newDirRight = (screenNormalRight) * sizeWidtherer; // + screenNormalTop
                            currentScreenPos += newDirRight;

                            newDirUp = (screenNormalTop) * sizeheighterer; // + screenNormalTop
                            currentScreenPos -= newDirUp;

                            resulter = matroxer2;
                            resulter.M41 = currentScreenPos.X;
                            resulter.M42 = currentScreenPos.Y;
                            resulter.M43 = currentScreenPos.Z;
                            _screenDirMatrix_correct_pos[0][2] = resulter;

                            currentScreenPos = new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43);
                            newDirRight = (screenNormalRight) * sizeWidtherer; // + screenNormalTop
                            currentScreenPos += newDirRight;

                            newDirUp = (screenNormalTop) * sizeheighterer; // + screenNormalTop
                            currentScreenPos += newDirUp;

                            resulter = matroxer2;
                            resulter.M41 = currentScreenPos.X;
                            resulter.M42 = currentScreenPos.Y;
                            resulter.M43 = currentScreenPos.Z;
                            _screenDirMatrix_correct_pos[0][3] = resulter;*/


                            Vector3 tester00;
                            /*//SCREEN HMDPOINTER
                            if (_out_of_bounds_oculus_rift == 1 || distancer >= 4)
                            {
                                tester00 = new Vector3(_screenDirMatrix_correct_pos[0][2].M41, _screenDirMatrix_correct_pos[0][2].M42, _screenDirMatrix_correct_pos[0][2].M43);
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
                            //END OF */
                            /*
                            //OCULUS TOUCH RIGHT
                            if (_out_of_bounds_right == 1 || distancer >= 4)
                            {
                                tester00 = new Vector3(_screenDirMatrix_correct_pos[0][2].M41, _screenDirMatrix_correct_pos[0][2].M42, _screenDirMatrix_correct_pos[0][2].M43);
                                _intersectTouchRightMatrix = screen_mat;

                                _intersectTouchRightMatrix.M41 = tester00.X;
                                _intersectTouchRightMatrix.M42 = tester00.Y;
                                _intersectTouchRightMatrix.M43 = tester00.Z;
                                worldMatrix_instances_screen_assets[0][5] = _intersectTouchRightMatrix;

                            }
                            else
                            {
                                tester00 = new Vector3(intersectPointRight.X, intersectPointRight.Y, intersectPointRight.Z);
                                _intersectTouchRightMatrix = screen_mat;

                                _intersectTouchRightMatrix.M41 = tester00.X;
                                _intersectTouchRightMatrix.M42 = tester00.Y;
                                _intersectTouchRightMatrix.M43 = tester00.Z;
                                worldMatrix_instances_screen_assets[0][5] = _intersectTouchRightMatrix;
                            }
                            //END OF
                            tester00 = new Vector3(intersectPointRight.X, intersectPointRight.Y, intersectPointRight.Z);
                            _intersectTouchRightMatrix = screen_mat;

                            _intersectTouchRightMatrix.M41 = tester00.X;
                            _intersectTouchRightMatrix.M42 = tester00.Y;
                            _intersectTouchRightMatrix.M43 = tester00.Z;
                            worldMatrix_instances_screen_assets[0][5] = _intersectTouchRightMatrix;

                            tester00 = new Vector3(intersectPointLeft.X, intersectPointLeft.Y, intersectPointLeft.Z);
                            _intersectTouchLeftMatrix = screen_mat;

                            _intersectTouchLeftMatrix.M41 = tester00.X;
                            _intersectTouchLeftMatrix.M42 = tester00.Y;
                            _intersectTouchLeftMatrix.M43 = tester00.Z;
                            worldMatrix_instances_screen_assets[0][6] = _intersectTouchLeftMatrix;
                            
                            //OCULUS TOUCH LEFT
                            if (_out_of_bounds_left == 1 || distancer >= 4) //|| 
                            {
                                Vector3 tester00 = new Vector3(_screenDirMatrix_correct_pos[0][2].M41, _screenDirMatrix_correct_pos[0][2].M42, _screenDirMatrix_correct_pos[0][2].M43);
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
                            
                            worldMatrix_instances_screen_assets[0][7] = _screenDirMatrix_correct_pos[0][0];
                            worldMatrix_instances_screen_assets[0][8] = _screenDirMatrix_correct_pos[0][0];
                            */





























                            //TERRAIN TILES CUBES
                            //_world_terrain_tile_list[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_terrain_tiles[indexer00];
                            //_world_terrain_tile_list[indexer00]._POSITION = worldMatrix_base[0];
                            //END OF 

                            //PHYSICS CUBES
                            _world_cube_list[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_cubes[indexer00];
                            _world_cube_list[indexer00]._POSITION = worldMatrix_base[0];
                            //END OF 

                            //PHYSICS JITTER CLOTH
                            _world_jitter_cloth_list[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_jitter_cloth[indexer00];
                            _world_jitter_cloth_list[indexer00]._POSITION = worldMatrix_base[0];
                            //END OF 

                            
                            //PHYSICS VOXEL SPHEROID
                            _world_voxel_cube_lists[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_voxel_cube[indexer00];
                            _world_voxel_cube_lists[indexer00]._POSITION = worldMatrix_base[0];
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

                                var dirInstance = _newgetdirforward(_testQuater);
                                cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
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



                                var dirInstance = _newgetdirforward(_testQuater);
                                //var dirInstance = _newgetdirforward(_testQuater);
                                cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                //dirInstance = -_newgetdirleft(_testQuater);
                                cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = _newgetdirup(_testQuater);
                                //dirInstance = dirInstance = _newgetdirup(_testQuater);
                                cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                cuber.instancesDataUP[i].rotation.W = 1;
                            }
                            //END OF
                            
                            //PHYSICS JITTER CLOTH
                            var jittercloth = _world_jitter_cloth_list[indexer00];
                            var instances_cloth = jittercloth.instances;
                            sometester = jittercloth._WORLDMATRIXINSTANCES;

                            for (int i = 0; i < instances_cloth.Length; i++)
                            {
                                float xxx = sometester[i].M41;
                                float yyy = sometester[i].M42;
                                float zzz = sometester[i].M43;

                                jittercloth.instances[i].position.X = xxx;
                                jittercloth.instances[i].position.Y = yyy;
                                jittercloth.instances[i].position.Z = zzz;
                                jittercloth.instances[i].position.W = 1;
                                Quaternion.RotationMatrix(ref sometester[i], out _testQuater);

                                var dirInstance = _newgetdirforward(_testQuater);
                                //var dirInstance = _newgetdirforward(_testQuater);
                                jittercloth.instancesDataForward[i].rotation.X = dirInstance.X;
                                jittercloth.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                jittercloth.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                jittercloth.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                //dirInstance = -_newgetdirleft(_testQuater);
                                jittercloth.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                jittercloth.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                jittercloth.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                jittercloth.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = _newgetdirup(_testQuater);
                                //dirInstance = dirInstance = _newgetdirup(_testQuater);
                                jittercloth.instancesDataUP[i].rotation.X = dirInstance.X;
                                jittercloth.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                jittercloth.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                jittercloth.instancesDataUP[i].rotation.W = 1;
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

                                var dirInstance = _newgetdirforward(_testQuater);
                                voxel_cube.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cube.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cube.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cube.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cube.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cube.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cube.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cube.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cube.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cube.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cube.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cube.instancesDataUP[i].rotation.W = 1;
                            }
                            //END OF




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

                                var dirInstance = _newgetdirforward(_testQuater);
                                cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                cuber.instancesDataUP[i].rotation.W = 1;
                            }*/













                            //PHYSICS HAND RIGHT
                            _player_rght_hnd[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_hand[tempIndex];
                            _player_rght_hnd[tempIndex]._POSITION = worldMatrix_base[0];

                            //PHYSICS HAND LEFT
                            _player_lft_hnd[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_hand[tempIndex];
                            _player_lft_hnd[tempIndex]._POSITION = worldMatrix_base[0];

                            //PHYSICS UPPER ARM LEFT
                            _player_lft_upper_arm[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_upperarm[tempIndex];
                            _player_lft_upper_arm[tempIndex]._POSITION = worldMatrix_base[0];


                            //PHYSICS LOWER ARM LEFT
                            _player_lft_lower_arm[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_lowerarm[tempIndex];
                            _player_lft_lower_arm[tempIndex]._POSITION = worldMatrix_base[0];


                            //PHYSICS LOWER ARM LEFT ELBOWTARGET
                            _player_lft_elbow_target[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_elbow_target[tempIndex];
                            _player_lft_elbow_target[tempIndex]._POSITION = worldMatrix_base[0];

                            //PHYSICS LOWER ARM LEFT ELBOWTARGET TWO
                            _player_lft_elbow_target_two[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_elbow_target_two[tempIndex];
                            _player_lft_elbow_target_two[tempIndex]._POSITION = worldMatrix_base[0];

                            //PHYSICS LOWER ARM RIGHT
                            _player_rght_lower_arm[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_lowerarm[tempIndex];
                            _player_rght_lower_arm[tempIndex]._POSITION = worldMatrix_base[0];

                            //PHYSICS UPPER ARM RIGHT
                            _player_rght_upper_arm[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_upperarm[tempIndex];
                            _player_rght_upper_arm[tempIndex]._POSITION = worldMatrix_base[0];

                            //PHYSICS  RIGHT ELBOWTARGET
                            _player_rght_elbow_target[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_elbow_target[tempIndex];
                            _player_rght_elbow_target[tempIndex]._POSITION = worldMatrix_base[0];

                            //PHYSICS RIGHT ELBOWTARGET TWO
                            _player_rght_elbow_target_two[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_elbow_target_two[tempIndex];
                            _player_rght_elbow_target_two[tempIndex]._POSITION = worldMatrix_base[0];

                            //PHYSICS RIGHT SHOULDER
                            _player_rght_shldr[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_shoulder[tempIndex];
                            _player_rght_shldr[tempIndex]._POSITION = worldMatrix_base[0];


                            //PHYSICS PELVIS
                            _player_pelvis[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_pelvis[tempIndex];
                            _player_pelvis[tempIndex]._POSITION = worldMatrix_base[0];

                            //PHYSICS TORSO
                            _player_torso[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_torso[tempIndex];
                            _player_torso[tempIndex]._POSITION = worldMatrix_base[0];
                            //PHYSICS LEFT SHOULDER
                            _player_lft_shldr[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_shoulder[tempIndex];
                            _player_lft_shldr[tempIndex]._POSITION = worldMatrix_base[0];




                            //tick_perf_counter.Stop();
                            //tick_perf_counter.Reset();
                            //tick_perf_counter.Restart();







                            voxel_cuber_r_hnd = _player_rght_hnd[tempIndex];
                            //voxel_instancers_r_hnd = voxel_cuber_r_hnd.instances;
                            voxel_sometester_r_hnd = voxel_cuber_r_hnd._WORLDMATRIXINSTANCES;

                            voxel_cuber_l_hnd = _player_lft_hnd[tempIndex];
                            //voxel_instancers_l_hnd = voxel_cuber_l_hnd.instances;
                            voxel_sometester_l_hnd = voxel_cuber_l_hnd._WORLDMATRIXINSTANCES;

                            voxel_cuber_l_up_arm = _player_lft_upper_arm[tempIndex];
                            //voxel_instancers_l_up_arm = voxel_cuber_l_up_arm.instances;
                            voxel_sometester_l_up_arm = voxel_cuber_l_up_arm._WORLDMATRIXINSTANCES;

                            voxel_cuber_r_up_arm = _player_rght_upper_arm[tempIndex];
                            //voxel_instancers_r_up_arm = voxel_cuber_r_up_arm.instances;
                            voxel_sometester_r_up_arm = voxel_cuber_r_up_arm._WORLDMATRIXINSTANCES;

                            voxel_cuber_l_low_arm = _player_lft_lower_arm[tempIndex];
                            //voxel_instancers_l_low_arm = voxel_cuber_l_up_arm.instances;
                            voxel_sometester_l_low_arm = voxel_cuber_l_low_arm._WORLDMATRIXINSTANCES;

                            voxel_cuber_r_low_arm = _player_rght_lower_arm[tempIndex];
                            //voxel_instancers_r_low_arm = voxel_cuber_r_low_arm.instances;
                            voxel_sometester_r_low_arm = voxel_cuber_r_low_arm._WORLDMATRIXINSTANCES;

                            voxel_cuber_l_shld = _player_lft_shldr[tempIndex];
                            //voxel_instancers_l_shld = voxel_cuber_l_shld.instances;
                            voxel_sometester_l_shld = voxel_cuber_l_shld._WORLDMATRIXINSTANCES;

                            voxel_cuber_r_shld = _player_rght_shldr[tempIndex];
                            //voxel_instancers_r_shld = voxel_cuber_r_shld.instances;
                            voxel_sometester_r_shld = voxel_cuber_r_shld._WORLDMATRIXINSTANCES;

                            voxel_cuber_l_targ = _player_lft_elbow_target[tempIndex];
                            //voxel_instancers_l_targ = voxel_cuber_l_targ.instances;
                            voxel_sometester_l_targ = voxel_cuber_l_targ._WORLDMATRIXINSTANCES;

                            voxel_cuber_r_targ = _player_rght_elbow_target[tempIndex];
                            //voxel_instancers_r_targ = voxel_cuber_r_targ.instances;
                            voxel_sometester_r_targ = voxel_cuber_r_targ._WORLDMATRIXINSTANCES;

                            voxel_cuber_l_targ_two = _player_lft_elbow_target_two[tempIndex];
                            //voxel_instancers_l_targ_two = voxel_cuber_l_targ_two.instances;
                            voxel_sometester_l_targ_two = voxel_cuber_l_targ_two._WORLDMATRIXINSTANCES;

                            voxel_cuber_r_targ_two = _player_rght_elbow_target_two[tempIndex];
                            //voxel_instancers_r_targ_two = voxel_cuber_r_targ_two.instances;
                            voxel_sometester_r_targ_two = voxel_cuber_r_targ_two._WORLDMATRIXINSTANCES;

                            voxel_cuber_pelvis = _player_pelvis[tempIndex];
                            //voxel_instancers_pelvis = voxel_cuber_r_targ_two.instances;
                            voxel_sometester_pelvis = voxel_cuber_pelvis._WORLDMATRIXINSTANCES;

                            voxel_cuber_torso = _player_torso[tempIndex];
                            //voxel_instancers_torso = voxel_cuber_torso.instances;
                            voxel_sometester_torso = voxel_cuber_torso._WORLDMATRIXINSTANCES;



                            for (int i = 0; i < voxel_cuber_r_hnd.instances.Length; i++)
                            {
                                float xxx = voxel_sometester_r_hnd[i].M41;
                                float yyy = voxel_sometester_r_hnd[i].M42;
                                float zzz = voxel_sometester_r_hnd[i].M43;

                                voxel_cuber_r_hnd.instances[i].position.X = xxx;
                                voxel_cuber_r_hnd.instances[i].position.Y = yyy;
                                voxel_cuber_r_hnd.instances[i].position.Z = zzz;
                                voxel_cuber_r_hnd.instances[i].position.W = 1;
                                Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester_r_hnd[i], out _testQuater);

                                var dirInstance = -_newgetdirforward(_testQuater);
                                voxel_cuber_r_hnd.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber_r_hnd.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_r_hnd.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_r_hnd.instancesDataForward[i].rotation.W = 1;

                                dirInstance = _newgetdirleft(_testQuater);
                                voxel_cuber_r_hnd.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber_r_hnd.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_r_hnd.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_r_hnd.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber_r_hnd.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber_r_hnd.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_r_hnd.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_r_hnd.instancesDataUP[i].rotation.W = 1;





                                xxx = voxel_sometester_l_hnd[i].M41;
                                yyy = voxel_sometester_l_hnd[i].M42;
                                zzz = voxel_sometester_l_hnd[i].M43;

                                voxel_cuber_l_hnd.instances[i].position.X = xxx;
                                voxel_cuber_l_hnd.instances[i].position.Y = yyy;
                                voxel_cuber_l_hnd.instances[i].position.Z = zzz;
                                voxel_cuber_l_hnd.instances[i].position.W = 1;
                                // Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester_l_hnd[i], out _testQuater);

                                dirInstance = -_newgetdirforward(_testQuater);
                                voxel_cuber_l_hnd.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber_l_hnd.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_l_hnd.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_l_hnd.instancesDataForward[i].rotation.W = 1;

                                dirInstance = _newgetdirleft(_testQuater);
                                voxel_cuber_l_hnd.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber_l_hnd.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_l_hnd.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_l_hnd.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber_l_hnd.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber_l_hnd.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_l_hnd.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_l_hnd.instancesDataUP[i].rotation.W = 1;











                                //voxel_cuber_l_up_arm = _player_lft_upper_arm[tempIndex];
                                //voxel_instancers_l_up_arm = voxel_cuber_l_up_arm.instances;
                                //voxel_sometester_l_up_arm = voxel_cuber_l_up_arm._WORLDMATRIXINSTANCES;

                                xxx = voxel_sometester_l_up_arm[i].M41;
                                yyy = voxel_sometester_l_up_arm[i].M42;
                                zzz = voxel_sometester_l_up_arm[i].M43;

                                voxel_cuber_l_up_arm.instances[i].position.X = xxx;
                                voxel_cuber_l_up_arm.instances[i].position.Y = yyy;
                                voxel_cuber_l_up_arm.instances[i].position.Z = zzz;
                                voxel_cuber_l_up_arm.instances[i].position.W = 1;

                                Quaternion.RotationMatrix(ref voxel_sometester_l_up_arm[i], out _testQuater);

                                dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber_l_up_arm.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber_l_up_arm.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_l_up_arm.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_l_up_arm.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber_l_up_arm.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber_l_up_arm.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_l_up_arm.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_l_up_arm.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber_l_up_arm.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber_l_up_arm.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_l_up_arm.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_l_up_arm.instancesDataUP[i].rotation.W = 1;




                                //voxel_cuber_l_low_arm = _player_lft_lower_arm[tempIndex];
                                //voxel_instancers_l_low_arm = voxel_cuber_l_low_arm.instances;
                                //voxel_sometester_l_low_arm = voxel_cuber_l_low_arm._WORLDMATRIXINSTANCES;

                                xxx = voxel_sometester_l_low_arm[i].M41;
                                yyy = voxel_sometester_l_low_arm[i].M42;
                                zzz = voxel_sometester_l_low_arm[i].M43;

                                voxel_cuber_l_low_arm.instances[i].position.X = xxx;
                                voxel_cuber_l_low_arm.instances[i].position.Y = yyy;
                                voxel_cuber_l_low_arm.instances[i].position.Z = zzz;
                                voxel_cuber_l_low_arm.instances[i].position.W = 1;
                                // Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester_l_low_arm[i], out _testQuater);

                                dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber_l_low_arm.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber_l_low_arm.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_l_low_arm.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_l_low_arm.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber_l_low_arm.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber_l_low_arm.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_l_low_arm.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_l_low_arm.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber_l_low_arm.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber_l_low_arm.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_l_low_arm.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_l_low_arm.instancesDataUP[i].rotation.W = 1;




                                //voxel_cuber_l_targ = _player_lft_elbow_target[tempIndex];
                                //voxel_instancers_l_targ = voxel_cuber_l_targ.instances;
                                //voxel_sometester_l_targ = voxel_cuber_l_targ._WORLDMATRIXINSTANCES;


                                xxx = voxel_sometester_l_targ[i].M41;
                                yyy = voxel_sometester_l_targ[i].M42;
                                zzz = voxel_sometester_l_targ[i].M43;

                                voxel_cuber_l_targ.instances[i].position.X = xxx;
                                voxel_cuber_l_targ.instances[i].position.Y = yyy;
                                voxel_cuber_l_targ.instances[i].position.Z = zzz;
                                voxel_cuber_l_targ.instances[i].position.W = 1;
                                // Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester_l_targ[i], out _testQuater);

                                dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber_l_targ.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber_l_targ.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_l_targ.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_l_targ.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber_l_targ.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber_l_targ.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_l_targ.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_l_targ.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber_l_targ.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber_l_targ.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_l_targ.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_l_targ.instancesDataUP[i].rotation.W = 1;






                                //voxel_cuber_l_targ_two = _player_lft_elbow_target_two[tempIndex];
                                //voxel_instancers_l_targ_two = voxel_cuber_l_targ_two.instances;
                                //voxel_sometester_l_targ_two = voxel_cuber_l_targ_two._WORLDMATRIXINSTANCES;


                                xxx = voxel_sometester_l_targ_two[i].M41;
                                yyy = voxel_sometester_l_targ_two[i].M42;
                                zzz = voxel_sometester_l_targ_two[i].M43;

                                voxel_cuber_l_targ_two.instances[i].position.X = xxx;
                                voxel_cuber_l_targ_two.instances[i].position.Y = yyy;
                                voxel_cuber_l_targ_two.instances[i].position.Z = zzz;
                                voxel_cuber_l_targ_two.instances[i].position.W = 1;
                                // Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester_l_targ_two[i], out _testQuater);

                                dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber_l_targ_two.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber_l_targ_two.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_l_targ_two.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_l_targ_two.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber_l_targ_two.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber_l_targ_two.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_l_targ_two.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_l_targ_two.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber_l_targ_two.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber_l_targ_two.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_l_targ_two.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_l_targ_two.instancesDataUP[i].rotation.W = 1;


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

                                dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber_r_low_arm.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber_r_low_arm.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_r_low_arm.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_r_low_arm.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber_r_low_arm.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber_r_low_arm.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_r_low_arm.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_r_low_arm.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber_r_low_arm.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber_r_low_arm.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_r_low_arm.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_r_low_arm.instancesDataUP[i].rotation.W = 1;




                                xxx = voxel_sometester_l_shld[i].M41;
                                yyy = voxel_sometester_l_shld[i].M42;
                                zzz = voxel_sometester_l_shld[i].M43;

                                voxel_cuber_l_shld.instances[i].position.X = xxx;
                                voxel_cuber_l_shld.instances[i].position.Y = yyy;
                                voxel_cuber_l_shld.instances[i].position.Z = zzz;
                                voxel_cuber_l_shld.instances[i].position.W = 1;
                                // Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester_l_shld[i], out _testQuater);

                                dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber_l_shld.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber_l_shld.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_l_shld.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_l_shld.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber_l_shld.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber_l_shld.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_l_shld.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_l_shld.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber_l_shld.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber_l_shld.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_l_shld.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_l_shld.instancesDataUP[i].rotation.W = 1;



                                xxx = voxel_sometester_r_up_arm[i].M41;
                                yyy = voxel_sometester_r_up_arm[i].M42;
                                zzz = voxel_sometester_r_up_arm[i].M43;

                                voxel_cuber_r_up_arm.instances[i].position.X = xxx;
                                voxel_cuber_r_up_arm.instances[i].position.Y = yyy;
                                voxel_cuber_r_up_arm.instances[i].position.Z = zzz;
                                voxel_cuber_r_up_arm.instances[i].position.W = 1;
                                // Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester_r_up_arm[i], out _testQuater);

                                dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber_r_up_arm.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber_r_up_arm.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_r_up_arm.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_r_up_arm.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber_r_up_arm.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber_r_up_arm.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_r_up_arm.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_r_up_arm.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
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

                                dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber_r_targ.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber_r_targ.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_r_targ.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_r_targ.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber_r_targ.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber_r_targ.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_r_targ.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_r_targ.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
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

                                dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber_r_targ_two.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber_r_targ_two.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_r_targ_two.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_r_targ_two.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber_r_targ_two.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber_r_targ_two.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_r_targ_two.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_r_targ_two.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
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

                                dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber_r_shld.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber_r_shld.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_r_shld.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_r_shld.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber_r_shld.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber_r_shld.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_r_shld.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_r_shld.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber_r_shld.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber_r_shld.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_r_shld.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_r_shld.instancesDataUP[i].rotation.W = 1;




                                //voxel_cuber = _player_pelvis[tempIndex];
                                //voxel_instancers = voxel_cuber.instances;
                                //voxel_sometester = voxel_cuber._WORLDMATRIXINSTANCES;

                                xxx = voxel_sometester_pelvis[i].M41;
                                yyy = voxel_sometester_pelvis[i].M42;
                                zzz = voxel_sometester_pelvis[i].M43;

                                voxel_cuber_pelvis.instances[i].position.X = xxx;
                                voxel_cuber_pelvis.instances[i].position.Y = yyy;
                                voxel_cuber_pelvis.instances[i].position.Z = zzz;
                                voxel_cuber_pelvis.instances[i].position.W = 1;
                                // Quaternion _testQuater;
                                Quaternion.RotationMatrix(ref voxel_sometester_pelvis[i], out _testQuater);

                                dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber_pelvis.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber_pelvis.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_pelvis.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_pelvis.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber_pelvis.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber_pelvis.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_pelvis.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_pelvis.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
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

                                dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber_torso.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber_torso.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_torso.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_torso.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber_torso.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber_torso.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_torso.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_torso.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber_torso.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber_torso.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber_torso.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber_torso.instancesDataUP[i].rotation.W = 1;
                            }


                            // tick_perf_counter.Stop();
                            //Console.WriteLine(tick_perf_counter.Elapsed.Ticks);

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

                                var dirInstance = _newgetdirforward(_testQuater);
                                cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                cuber.instancesDataUP[i].rotation.W = 1;
                            }*/


















                            /*
                            //PHYSICS HAND RIGHT
                            _player_rght_hnd[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_hand[tempIndex];
                            _player_rght_hnd[tempIndex]._POSITION = worldMatrix_base[0];

                            var voxel_cuber = _player_rght_hnd[tempIndex];
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

                                var dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }


                            //PHYSICS HAND LEFT
                            _player_lft_hnd[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_hand[tempIndex];
                            _player_lft_hnd[tempIndex]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_lft_hnd[tempIndex];
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

                                var dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }








                            //PHYSICS UPPER ARM LEFT
                            _player_lft_upper_arm[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_upperarm[tempIndex];
                            _player_lft_upper_arm[tempIndex]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_lft_upper_arm[tempIndex];
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

                                var dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }


                            //PHYSICS LOWER ARM LEFT
                            _player_lft_lower_arm[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_lowerarm[tempIndex];
                            _player_lft_lower_arm[tempIndex]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_lft_lower_arm[tempIndex];
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

                                var dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }




                            //PHYSICS LOWER ARM LEFT ELBOWTARGET
                            _player_lft_elbow_target[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_elbow_target[tempIndex];
                            _player_lft_elbow_target[tempIndex]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_lft_elbow_target[tempIndex];
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

                                var dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }






                            //PHYSICS LOWER ARM LEFT ELBOWTARGET TWO
                            _player_lft_elbow_target_two[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_elbow_target_two[tempIndex];
                            _player_lft_elbow_target_two[tempIndex]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_lft_elbow_target_two[tempIndex];
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

                                var dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }





                            //PHYSICS LOWER ARM RIGHT
                            _player_rght_lower_arm[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_lowerarm[tempIndex];
                            _player_rght_lower_arm[tempIndex]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_rght_lower_arm[tempIndex];
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

                                var dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }

                            //PHYSICS UPPER ARM RIGHT
                            _player_rght_upper_arm[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_upperarm[tempIndex];
                            _player_rght_upper_arm[tempIndex]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_rght_upper_arm[tempIndex];
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

                                var dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }






                            //PHYSICS  RIGHT ELBOWTARGET
                            _player_rght_elbow_target[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_elbow_target[tempIndex];
                            _player_rght_elbow_target[tempIndex]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_rght_elbow_target[tempIndex];
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

                                var dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }






                            //PHYSICS RIGHT ELBOWTARGET TWO
                            _player_rght_elbow_target_two[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_elbow_target_two[tempIndex];
                            _player_rght_elbow_target_two[tempIndex]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_rght_elbow_target_two[tempIndex];
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

                                var dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }



                            //PHYSICS RIGHT SHOULDER
                            _player_rght_shldr[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_shoulder[tempIndex];
                            _player_rght_shldr[tempIndex]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_rght_shldr[tempIndex];
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

                                var dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }



























                            //PHYSICS PELVIS
                            _player_pelvis[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_pelvis[tempIndex];
                            _player_pelvis[tempIndex]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_pelvis[tempIndex];
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

                                var dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }


                            //PHYSICS TORSO
                            _player_torso[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_torso[tempIndex];
                            _player_torso[tempIndex]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_torso[tempIndex];
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

                                var dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }




                            //PHYSICS LEFT SHOULDER
                            _player_lft_shldr[tempIndex]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_shoulder[tempIndex];
                            _player_lft_shldr[tempIndex]._POSITION = worldMatrix_base[0];

                            voxel_cuber = _player_lft_shldr[tempIndex];
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

                                var dirInstance = _newgetdirforward(_testQuater);
                                voxel_cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataForward[i].rotation.W = 1;

                                dirInstance = -_newgetdirleft(_testQuater);
                                voxel_cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataRIGHT[i].rotation.W = 1;

                                dirInstance = dirInstance = _newgetdirup(_testQuater);
                                voxel_cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                voxel_cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                voxel_cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                voxel_cuber.instancesDataUP[i].rotation.W = 1;
                            }*/
                        }
                    }
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






                if (_has_locked_screen_pos == 0)
                {
                    /*if (had_locked_screen == 0)
                    {
                        matroxer2 = _player_lft_hnd[0]._arrayOfInstances[0].current_pos;

                        Vector3 savingPos = new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43);

                        Quaternion _testQuator;
                        Quaternion.RotationMatrix(ref matroxer2, out _testQuator);

                        float xq = _testQuator.X;
                        float yq = _testQuator.Y;
                        float zq = _testQuator.Z;
                        float wq = _testQuator.W;
                        float roll = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI));
                        float pitch = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI));
                        float yaw = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI));

                        //RotationScreenX = pitch;
                        //RotationScreenY = yaw;
                        //RotationScreenZ = roll;

                        _current_screen_pos = _direction_offsetter * matroxer2;

                        _current_screen_pos.M41 = savingPos.X;
                        _current_screen_pos.M42 = savingPos.Y;
                        _current_screen_pos.M43 = savingPos.Z;

                        _last_screen_pos = matroxer2;

                    }
                    else if (had_locked_screen == 1)
                    {
                        _last_screen_pos = _world_screen_list[0]._arrayOfInstances[0].current_pos;
                        had_locked_screen = 0;
                    }*/

                    _current_screen_pos = _world_screen_list[0]._arrayOfInstances[0].current_pos;
                    _last_screen_pos = _world_screen_list[0]._arrayOfInstances[0].current_pos;// worldMatrix_instances_screens[0][0];

                    /*Matrix _temp_mat = finalRotationMatrix;
                    Quaternion _testQuator;
                    Quaternion.RotationMatrix(ref _temp_mat, out _testQuator);

                    xq = _testQuator.X;
                    yq = _testQuator.Y;
                    zq = _testQuator.Z;
                    wq = _testQuator.W;*/

                    //temproll = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));
                    //pitch = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI));//
                    //tempyaw = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));

                    had_locked_screen = 0;

                }
                else if (_has_locked_screen_pos == 1)
                {
                    if (had_locked_screen == 0)
                    {
                        Quaternion _testQuator;
                        Quaternion.RotationMatrix(ref _last_screen_pos, out _testQuator);

                        float xq = _testQuator.X;
                        float yq = _testQuator.Y;
                        float zq = _testQuator.Z;
                        float wq = _testQuator.W;

                        float roll = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));
                        float pitch = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI));//
                        float yaw = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));

                        Quaternion.RotationMatrix(ref rotatingMatrixForPelvis, out _testQuator);

                        xq = _testQuator.X;
                        yq = _testQuator.Y;
                        zq = _testQuator.Z;
                        wq = _testQuator.W;

                        float roller = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));
                        float pitcher = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI));//
                        float yawer = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));

                        pitch = (float)((Math.PI * pitcher + 45) / 180);
                        roll = (float)(0);

                        matroxer2 = SharpDX.Matrix.RotationYawPitchRoll(pitch, 0, roll);

                        matroxer2.M41 = _last_screen_pos.M41;
                        matroxer2.M42 = _last_screen_pos.M42;
                        matroxer2.M43 = _last_screen_pos.M43;

                        _current_screen_pos =  matroxer2; //_direction_offsetter

                        _current_screen_pos.M41 = _last_screen_pos.M41;
                        _current_screen_pos.M42 = _last_screen_pos.M42;
                        _current_screen_pos.M43 = _last_screen_pos.M43;

                        had_locked_screen = 1;
                    }
                }

                screen_mat = _world_screen_list[0]._arrayOfInstances[0].current_pos;//_player_lft_hnd[0]._arrayOfInstances[0].current_pos; 

                //Quaternion _quat_screen;
                Quaternion.RotationMatrix(ref screen_mat, out _quat_screen);
                screenNormal = _getDirection(Vector3.ForwardRH, _quat_screen);
                screenNormal.Normalize();
                planer = new Plane(new Vector3(screen_mat.M41, screen_mat.M42, screen_mat.M43), screenNormal);

                Vector3 screenPos = new Vector3(_world_screen_list[0]._arrayOfInstances[0].current_pos.M41, _world_screen_list[0]._arrayOfInstances[0].current_pos.M42, _world_screen_list[0]._arrayOfInstances[0].current_pos.M43);
                float distancer;
                Vector3.Distance(ref screenPos, ref OFFSETPOS, out distancer);
                screen_mat = _world_screen_list[0]._arrayOfInstances[0].current_pos;

                /*//SCREEN HMDPOINTER
                if (_out_of_bounds_oculus_rift == 1)// || distancer >= 4)
                {
                    var tester00 = new Vector3(_screenDirMatrix_correct_pos[0][2].M41, _screenDirMatrix_correct_pos[0][2].M42, _screenDirMatrix_correct_pos[0][2].M43);
                    _oculusR_Cursor_matrix = screen_mat;

                    _oculusR_Cursor_matrix.M41 = tester00.X;
                    _oculusR_Cursor_matrix.M42 = tester00.Y;
                    _oculusR_Cursor_matrix.M43 = tester00.Z;
                    worldMatrix_instances_screen_assets[0][4] = _oculusR_Cursor_matrix;
                }
                else
                {
                    worldMatrix_instances_screen_assets[0][4] = _oculusR_Cursor_matrix;
                }*/
                //END OF 

                /*//OCULUS TOUCH RIGHT
                if (_out_of_bounds_right == 1)// || distancer >= 4)
                {
                    Vector3 tester00 = new Vector3(_screenDirMatrix_correct_pos[0][2].M41, _screenDirMatrix_correct_pos[0][2].M42, _screenDirMatrix_correct_pos[0][2].M43);
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
                }*/


                /*Vector3 tester01 = new Vector3(intersectPointRight.X, intersectPointRight.Y, intersectPointRight.Z);
                var newmatrix = screen_mat;
                newmatrix.M41 = tester01.X;
                newmatrix.M42 = tester01.Y;
                newmatrix.M43 = tester01.Z;*/
                worldMatrix_instances_screen_assets[0][5] = _intersectTouchRightMatrix;








                centerPosRight = new Vector3(final_hand_pos_right_locked.M41, final_hand_pos_right_locked.M42, final_hand_pos_right_locked.M43);
                Quaternion.RotationMatrix(ref final_hand_pos_right_locked, out _rightTouchQuat);
                rayDirRight = _getDirection(Vector3.ForwardRH, _rightTouchQuat);
                someRay = new Ray(centerPosRight, rayDirRight);
                intersecter = someRay.Intersects(ref planer, out intersectPointRight);



                centerPosLeft = new Vector3(final_hand_pos_left_locked.M41, final_hand_pos_left_locked.M42, final_hand_pos_left_locked.M43);
                Quaternion.RotationMatrix(ref final_hand_pos_left_locked, out _leftTouchQuat);
                rayDirLeft = _getDirection(Vector3.ForwardRH, _leftTouchQuat);
                someRayLeft = new Ray(centerPosLeft, rayDirLeft);
                intersecterLeft = someRayLeft.Intersects(ref planer, out intersectPointLeft);





                matroxer2 = worldMatrix_instances_screens[0][0];//_world_screen_list[0]._arrayOfInstances[0].current_pos;
                Quaternion.RotationMatrix(ref matroxer2, out _testQuater);
                screenNormal = _getDirection(Vector3.Right, _testQuater);
                screenNormal.Normalize();
                Vector3 screenNormalRight = new Vector3(screenNormal.X, screenNormal.Y, screenNormal.Z);
                screenNormal = _getDirection(Vector3.Up, _testQuater);
                screenNormal.Normalize();


                Vector3 screenNormalTop = new Vector3(screenNormal.X, screenNormal.Y, screenNormal.Z);
                Vector3 currentScreenPos = new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43); // new Vector3(xii, yii, zii);// new Vector3(_finalRotMatrixScreen.M41, _finalRotMatrixScreen.M42, _finalRotMatrixScreen.M43);
                Vector3 newDirRight = (screenNormalRight) * sizeWidtherer; // + screenNormalTop
                currentScreenPos -= newDirRight;
                Vector3 newDirUp = (screenNormalTop) * sizeheighterer; // + screenNormalTop
                currentScreenPos -= newDirUp;
                Matrix resulter0 = matroxer2;
                resulter0.M41 = currentScreenPos.X;
                resulter0.M42 = currentScreenPos.Y;
                resulter0.M43 = currentScreenPos.Z;
                _screenDirMatrix_correct_pos[0][0] = resulter0;

                currentScreenPos = new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43);
                newDirRight = (screenNormalRight) * sizeWidtherer;
                currentScreenPos -= newDirRight;
                newDirUp = (screenNormalTop) * sizeheighterer;
                currentScreenPos += newDirUp;
                resulter0 = matroxer2;
                resulter0.M41 = currentScreenPos.X;
                resulter0.M42 = currentScreenPos.Y;
                resulter0.M43 = currentScreenPos.Z;
                _screenDirMatrix_correct_pos[0][1] = resulter0;

                currentScreenPos = new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43);
                newDirRight = (screenNormalRight) * sizeWidtherer;
                currentScreenPos += newDirRight;
                newDirUp = (screenNormalTop) * sizeheighterer;
                currentScreenPos -= newDirUp;
                resulter0 = matroxer2;
                resulter0.M41 = currentScreenPos.X;
                resulter0.M42 = currentScreenPos.Y;
                resulter0.M43 = currentScreenPos.Z;
                _screenDirMatrix_correct_pos[0][2] = resulter0;

                currentScreenPos = new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43);
                newDirRight = (screenNormalRight) * sizeWidtherer;
                currentScreenPos += newDirRight;
                newDirUp = (screenNormalTop) * sizeheighterer;
                currentScreenPos += newDirUp;
                resulter0 = matroxer2;
                resulter0.M41 = currentScreenPos.X;
                resulter0.M42 = currentScreenPos.Y;
                resulter0.M43 = currentScreenPos.Z;
                _screenDirMatrix_correct_pos[0][3] = resulter0;

                /*for (int x = 0; x < _physics_engine_instance_x; x++)
                {
                    for (int y = 0; y < _physics_engine_instance_y; y++)
                    {
                        for (int z = 0; z < _physics_engine_instance_z; z++)
                        {
                            var _index = x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z);

                            for (int i = 0; i < worldMatrix_instances_screen_assets[0].Length; i++)
                            {
                                Matrix matrixor = _screenDirMatrix_correct_pos[0][i];
                                worldMatrix_instances_screen_assets[0][i] = matrixor;
                            }
                        }
                    }
                }*/
                for (int i = 0; i < 4; i++) //worldMatrix_instances_screen_assets[0].Length
                {
                    worldMatrix_instances_screen_assets[0][i] = _screenDirMatrix_correct_pos[0][i];
                }

                /*for (int i = 0; i < _screenDirMatrix_correct_pos.Length; i++)
                {
                    point3DCollection[0][i].X = _screenDirMatrix_correct_pos[0][i].M41;
                    point3DCollection[0][i].Y = _screenDirMatrix_correct_pos[0][i].M42;
                    point3DCollection[0][i].Z = _screenDirMatrix_correct_pos[0][i].M43;
                }*/

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

                    int j = 1;

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

                /*var vert0 = new Vector3(point3DCollection[0][0].X, point3DCollection[0][0].Y, point3DCollection[0][0].Z);
                var vert1= new Vector3(point3DCollection[0][1].X, point3DCollection[0][1].Y, point3DCollection[0][1].Z);
                var vert2 = new Vector3(point3DCollection[0][2].X, point3DCollection[0][2].Y, point3DCollection[0][2].Z);
                var vert3 = new Vector3(point3DCollection[0][3].X, point3DCollection[0][3].Y, point3DCollection[0][3].Z);
                */

                var vert0 = new Vector3(_screenDirMatrix_correct_pos[0][0].M41, _screenDirMatrix_correct_pos[0][0].M42, _screenDirMatrix_correct_pos[0][0].M43);
                var vert1 = new Vector3(_screenDirMatrix_correct_pos[0][1].M41, _screenDirMatrix_correct_pos[0][1].M42, _screenDirMatrix_correct_pos[0][1].M43);
                var vert2 = new Vector3(_screenDirMatrix_correct_pos[0][2].M41, _screenDirMatrix_correct_pos[0][2].M42, _screenDirMatrix_correct_pos[0][2].M43);
                var vert3 = new Vector3(_screenDirMatrix_correct_pos[0][3].M41, _screenDirMatrix_correct_pos[0][3].M42, _screenDirMatrix_correct_pos[0][3].M43);

                //var pointOnScreen = new Vector3(_intersectTouchRightMatrix.M41, _intersectTouchRightMatrix.M42, _intersectTouchRightMatrix.M43);
                var pointOnScreen = new Vector3(intersectPointRight.X, intersectPointRight.Y, intersectPointRight.Z);

                d = (vert2 - vert0).Length();
                widthLength = (vert2 - vert0).Length();
                heightLength = (vert1 - vert0).Length();
                r = (pointOnScreen - vert0).Length();
                R = (pointOnScreen - vert2).Length();
                x = ((d * d) - (r * r) + (R * R)) / (2 * d);
                d1 = x;
                d2 = d - x;

                //r is with d2
                //R is with d1
                //a2 + b2 = c2

                b = Math.Sqrt((r * r) - (d2 * d2));
                currentPosWidth = widthLength - d1; // 
                currentPosHeight = heightLength - b;
                percentXRight = currentPosWidth / widthLength;
                percentYRight = currentPosHeight / heightLength;
                percentXRight *= D3D.SurfaceWidth;
                percentYRight *= D3D.SurfaceHeight;


                if (percentXRight >= 0 && percentXRight < D3D.SurfaceWidth && percentYRight >= 0 && percentYRight < D3D.SurfaceHeight)
                {
                    _MicrosoftWindowsMouseRight(percentXRight, percentYRight, thumbStickRight, percentXLeft, percentYLeft, thumbStickLeft, percentXRight, percentYRight);
                }

                _oculus_touch_controls(percentXRight, percentYRight, thumbStickRight, percentXLeft, percentYLeft, thumbStickLeft, percentXRight, percentYRight);





                /*

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

                    int j = 1;
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
                */
                //intersectPointRight.Z;


                /*//CIRCLE CIRCLE INTERSECTION //http://mathworld.wolfram.com/Circle-CircleIntersection.html
                //d = (point3DCollection[2] - point3DCollection[0]).Length();
                //widthLength = (point3DCollection[2] - point3DCollection[0]).Length();
                //heightLength = (point3DCollection[1] - point3DCollection[0]).Length();
                r = (stabilizedIntersectionPosLeft - point3DCollection[0][0]).Length();
                R = (stabilizedIntersectionPosLeft - point3DCollection[0][2]).Length();

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
                






                double realMousePosX = 0;
                double realMousePosY = 0;

                r = (intersectPointRight - point3DCollection[0][0]).Length();
                R = (intersectPointRight - point3DCollection[0][2]).Length();


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

                realMousePosX = percentXRight;
                realMousePosY = percentYRight;*/













                /*
                //Console.WriteLine("x: " + _final_percentXRight + " y: " + _final_percentYRight);

                _MicrosoftWindowsMouseRight(_final_percentXRight, _final_percentYRight, thumbStickRight, percentXLeft, percentYLeft, thumbStickLeft, realMousePosX, realMousePosY);

                _oculus_touch_controls(_final_percentXRight, _final_percentYRight, thumbStickRight, percentXLeft, percentYLeft, thumbStickLeft, realMousePosX, realMousePosY);

                //var absoluteMoveX = Convert.ToUInt32((percentXRight * 65535) / D3D.SurfaceWidth);
                //var absoluteMoveY = Convert.ToUInt32((percentYRight * 65535) / D3D.SurfaceHeight);

                _mouseCursorMatrix.M41 = (float)((percentXRight * 65535) / D3D.SurfaceWidth);
                _mouseCursorMatrix.M42 = (float)((percentYRight * 65535) / D3D.SurfaceHeight);
                
                _last_final_hand_pos_right = new Vector3(_player_rght_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M43);
                _last_frame_handPos = new Vector3(_player_rght_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M43);

                //final_hand_pos_right_locked = _player_rght_hnd[0]._arrayOfInstances[0].current_pos;

                if (_grabbed_body_right != null)
                {
                    _last_frame_rigid_grab_pos = new Vector3(_grabbed_body_right.Position.X, _grabbed_body_right.Position.Y, _grabbed_body_right.Position.Z);
                    _last_frame_rigid_grab_rot = _grabbed_body_right.Orientation;//new JQuaternion();// new Vector3(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z);
                }*/

                /*
                lastHasUsedHandTriggerLeft = hasUsedHandTriggerLeft;
                lastbuttonPressedOculusTouchRight = buttonPressedOculusTouchRight;
                lastbuttonPressedOculusTouchLeft = buttonPressedOculusTouchLeft;
                */












































































                //DISCARDED TO REINSERT
                //DISCARDED TO REINSERT
                //DISCARDED TO REINSERT
                final_hand_pos_right_locked = _player_rght_hnd[0]._arrayOfInstances[0].current_pos;
                final_hand_pos_left_locked = _player_lft_hnd[0]._arrayOfInstances[0].current_pos;
                _last_final_hand_pos_right = new Vector3(_player_rght_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M43);
                _last_frame_handPos = new Vector3(_player_rght_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0]._arrayOfInstances[0].current_pos.M43);
                //DISCARDED TO REINSERT
                //DISCARDED TO REINSERT
                //DISCARDED TO REINSERT
                /*if (_grab_rigid_data._body != null)
                {
                    _last_frame_rigid_grab_pos = new Vector3(_grab_rigid_data._body.Position.X, _grab_rigid_data._body.Position.Y, _grab_rigid_data._body.Position.Z);
                    _last_frame_rigid_grab_rot = _grab_rigid_data._body.Orientation;//new JQuaternion();// new Vector3(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z);
                }*/





                /*_last_final_hand_pos_right = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);
                _last_frame_handPos = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);

                */



                /*for (int x = 0; x < _physics_engine_instance_x; x++)
                {
                    for (int y = 0; y < _physics_engine_instance_y; y++)
                    {
                        for (int z = 0; z < _physics_engine_instance_z; z++)
                        {
                            
                        }
                    }
                }*/


                /*
                LastRotationScreenX = RotationScreenX;
                LastRotationScreenY = RotationScreenY;
                LastRotationScreenZ = RotationScreenZ;
                */

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
























        //EXTERNACCESSORS
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };


        public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }
        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);


        private void _MicrosoftWindowsMouseRight(double percentXRight, double percentYRight, Vector2f[] thumbStickRight, double percentXLeft, double percentYLeft, Vector2f[] thumbStickLeft, double realMousePosX, double realMousePosY) //, double realOculusRiftCursorPosX, double realOculusRiftCursorPosY
        {
            try
            {


                //MessageBox((IntPtr)0, "percentXRight: " + percentXRight + " percentYRight: " + percentYRight, "mouse move", 0);
                //Console.WriteLine("percentXRight: " + percentXRight + " percentYRight: " + percentYRight);

                if (_indexMouseMove == 0)
                {
                    //MessageBox((IntPtr)0, "test0", "mouse move", 0);
                    /////////////RIGHT OCULUS TOUCH/////////////////////////////////////////////////////////////////////////////////////
                    if (percentXRight >= 0 && percentXRight <= D3D.SurfaceWidth && percentYRight >= 0 && percentYRight <= D3D.SurfaceHeight &&
                        realMousePosX >= 0 && realMousePosX <= D3D.SurfaceWidth && realMousePosY >= 0 && realMousePosY <= D3D.SurfaceHeight)
                    {
                        //MessageBox((IntPtr)0, "test1", "mouse move", 0);

                        //var absoluteMoveX = Convert.ToUInt32((percentXRight * 65535) / D3D.SurfaceWidth);
                        //var absoluteMoveY = Convert.ToUInt32((percentYRight * 65535) / D3D.SurfaceHeight);

                        var yo = _updateFunctionStopwatchRight.Elapsed.Milliseconds;

                        if (_hasLockedMouse == 0)
                        {
                            if (yo >= 5)
                            {


                                var absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535 - 1)) / D3D.SurfaceWidth);
                                var absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535 - 1)) / D3D.SurfaceHeight);

                                if (realMousePosX >= 0 && realMousePosX < D3D.SurfaceWidth)
                                {

                                }
                                else
                                {
                                    realMousePosX = D3D.SurfaceWidth;
                                    absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535 - 1)) / D3D.SurfaceWidth);
                                }

                                if (realMousePosY >= 0 && realMousePosY < D3D.SurfaceHeight)
                                {

                                }
                                else
                                {
                                    realMousePosY = D3D.SurfaceHeight;
                                    absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535 - 1)) / D3D.SurfaceHeight);
                                }


                                //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, absoluteMoveX, absoluteMoveY, 0, 0);
                                SetCursorPos((int)realMousePosX, (int)realMousePosY);


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
                                    var absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535)) / D3D.SurfaceWidth);
                                    var absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535)) / D3D.SurfaceHeight);

                                    if (realMousePosX >= 0 && realMousePosX < D3D.SurfaceWidth)
                                    {

                                    }
                                    else
                                    {
                                        realMousePosX = D3D.SurfaceWidth;
                                        absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535)) / D3D.SurfaceWidth);
                                    }

                                    if (realMousePosY >= 0 && realMousePosY < D3D.SurfaceHeight)
                                    {

                                    }
                                    else
                                    {
                                        realMousePosY = D3D.SurfaceHeight;
                                        absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535)) / D3D.SurfaceHeight);
                                    }


                                    if (_frameCounterTouchRight <= 20 && _canResetCounterTouchRightButtonA == true)
                                    {
                                        SetCursorPos((int)realMousePosX, (int)realMousePosY);
                                        //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, _lastMousePosXRight, _lastMousePosYRight, 0, 0);

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
                                    var absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535)) / D3D.SurfaceWidth);
                                    var absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535)) / D3D.SurfaceHeight);

                                    if (realMousePosX >= 0 && realMousePosX < D3D.SurfaceWidth)
                                    {

                                    }
                                    else
                                    {
                                        realMousePosX = D3D.SurfaceWidth;
                                        absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535)) / D3D.SurfaceWidth);
                                    }

                                    if (realMousePosY >= 0 && realMousePosY < D3D.SurfaceHeight)
                                    {

                                    }
                                    else
                                    {
                                        realMousePosY = D3D.SurfaceHeight;
                                        absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535)) / D3D.SurfaceHeight);

                                    }


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

                                //https://stackoverflow.com/questions/2929255/unable-to-launch-onscreen-keyboard-osk-exe-from-a-32-bit-process-on-win7-x64
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
                            /*if (thumbStickRight[1].Y <= -0.1f && hasUsedThumbStickRightE == false)
                            {
                                if (test < -0.99)
                                {
                                    test = (long)-0.99;
                                }

                                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -test, 0);
                                //Console.WriteLine("test");

                                hasUsedThumbStickRightE = true;
                            }
                            else if (hasUsedThumbStickRightE)
                            {
                                hasUsedThumbStickRightE = false;
                            }*/
                            _updateFunctionStopwatchRightThumbstickGoLeft.Stop();
                            _updateFunctionBoolRightThumbStickGoLeft = true;
                        }
                        ///////////////////////////////////////////////////////////////////////////

                        /////////RIGHT THUMBSTICK/////////////////////////////////////////////////////
                        var yo7 = _updateFunctionStopwatchRightThumbstickGoRight.Elapsed.Milliseconds;
                        if (yo7 >= 75)
                        {
                            /*if (thumbStickRight[1].Y >= 0.1f && hasUsedThumbStickRightQ == false)
                            {
                                if (test > 0.99f)
                                {
                                    test = (long)0.99;
                                }

                                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, test, 0);
                                hasUsedThumbStickRightQ = true;
                            }
                            else if (hasUsedThumbStickRightQ)
                            {
                                hasUsedThumbStickRightQ = false;
                            }*/
                            _updateFunctionStopwatchRightThumbstickGoRight.Stop();
                            _updateFunctionBoolRightThumbStickGoRight = true;
                        }
                    }


















                    /////////////LEFT OCULUS TOUCH/////////////////////////////////////////////////////////////////////////////////////
                    if (percentXLeft >= 0 && percentXLeft <= 1920 && percentYLeft >= 0 && percentYLeft <= 1080)
                    {
                        /*var absoluteMoveX = Convert.ToUInt32((0 * 65535) / D3D.SurfaceWidth);
                        var absoluteMoveY = Convert.ToUInt32((0 * 65535) / D3D.SurfaceHeight);

                        if (realMousePosX >= 0 && realMousePosX < D3D.SurfaceWidth)
                        {

                        }
                        else
                        {
                            realMousePosX = D3D.SurfaceWidth;
                            absoluteMoveX = Convert.ToUInt32((realMousePosX * 65535) / D3D.SurfaceWidth);
                        }

                        if (realMousePosY >= 0 && realMousePosY < D3D.SurfaceHeight)
                        {

                        }
                        else
                        {
                            realMousePosY = D3D.SurfaceHeight;
                            absoluteMoveY = Convert.ToUInt32((realMousePosY * 65535) / D3D.SurfaceHeight);
                        }*/


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

                        /*if (buttonPressedOculusTouchLeft != 0)
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
                                        _lastMousePosYLeft = absoluteMoveY;
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
                        }*/
                        _out_of_bounds_left = 0;
                    }
                    else
                    {
                        _out_of_bounds_left = 1;
                    }


                    /*//////////OCULUS TOUCH BUTTONS PRESSED////////////////////////////////////////
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
                    }*/

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
                                D3D.OVR.RecenterTrackingOrigin(D3D.sessionPtr);
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
            catch (Exception ex)
            {
                //MessageBox((IntPtr)0, ex.ToString(), "mouse move", 0);
            }
        }


        Matrix tempmat = Matrix.Identity;
        void _process_rigidbody_two(RigidBody rigidbody, int x, int y, int z, int count, int type_of_object)
        {




            //float distance = sc_maths.sc_check_distance_node_3d_geometry(current_start_move_pos.Value, new Vector3(xpos, ypos, zpos), _min_spike_end, _min_spike_end, _min_spike_end, _max_spike_end, _max_spike_end, _max_spike_end); //11.31415926535f








        }


        Matrix _process_stuff(Matrix originRotter, Matrix rotatingMatrixer, Matrix pelvisRotter, Matrix grabberRotter, Matrix righttouchmatrixer, Matrix rigidBodyMatrixer)
        {
            Matrix somerotfinal = Matrix.Identity;


            //somerotfinal = rigidBodyMatrixer * originRotter * rotatingMatrixer * grabberRotter;

            var someRightTouchMatrix = righttouchmatrixer;

            someRightTouchMatrix.M41 = 0;
            someRightTouchMatrix.M42 = 0;
            someRightTouchMatrix.M43 = 0;
            someRightTouchMatrix.M44 = 1;


            someRightTouchMatrix = someRightTouchMatrix * originRotter * rotatingMatrixer * pelvisRotter;
            somerotfinal = rigidBodyMatrixer; //  * originRotter * rotatingMatrixer * grabberRotter

            Matrix.Multiply(ref somerotfinal, ref someRightTouchMatrix, out somerotfinal);


            return somerotfinal;
        }


















        void _process_rigidbody_that_are_currently_activated_or_not(RigidBody rigidbody, int x, int y, int z, int count)
        {
            int _index = x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z);

            if (!rigidbody.AffectedByGravity)
            {

            }
            else
            {
                if (switch_4_started_physics == 1)
                {

                    if (rigidbody.IsActive)
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
                                            RigidBody rigibodyExtra = (RigidBody)enumerator1.Current;
                                            JVector currentLinearVelExtra = rigibodyExtra.LinearVelocity;
                                            JVector currentAngularVelExtra = rigibodyExtra.AngularVelocity;

                                            if (rigibodyExtra == rigidbody)
                                            {
                                                var somePosLastX = _array_of_last_frame_cube_pos[_index][count].X;
                                                var somePosLastY = _array_of_last_frame_cube_pos[_index][count].Y;
                                                var somePosLastZ = _array_of_last_frame_cube_pos[_index][count].Z;

                                                var rigidX = Math.Round(somePosLastX * 10) / 10;
                                                var rigidY = Math.Round(somePosLastY * 10) / 10;
                                                var rigidZ = Math.Round(somePosLastZ * 10) / 10;

                                                var diffX = Math.Abs(somePosLastX - rigidX);
                                                var diffY = Math.Abs(somePosLastY - rigidY);
                                                var diffZ = Math.Abs(somePosLastZ - rigidZ);

                                                //Console.WriteLine(Math.Round(somePosLastX * 10) / 10);

                                                if (Math.Round(diffX * 10) / 10 < 0.001f &&
                                                    Math.Round(diffY * 10) / 10 < 0.001f &&
                                                    Math.Round(diffZ * 10) / 10 < 0.001f)
                                                {
                                                    JVector currentLinearVel = rigidbody.LinearVelocity;
                                                    JVector currentAngularVel = rigidbody.AngularVelocity;

                                                    //Console.WriteLine(Math.Round(somePosLastX * 10) / 10);

                                                    if (currentLinearVel.Length() < 0.35f && currentAngularVel.Length() < 0.35f) //0.00035f == 400 approx => 0.00075f == 400 approx
                                                    {
                                                        if (rigidbody.IsActive && !rigidbody.IsStatic)
                                                        {

                                                        }



                                                        //_objects_inactive_counter_00[_index][count] = 0;
                                                    }
                                                    else
                                                    {
                                                        //rigidbody.IsActive = true;
                                                        //rigidbody.AffectedByGravity = true;
                                                        //body.IsActive = true;
                                                        //body.AffectedByGravity = true;
                                                    }
                                                }
                                                break;
                                            }
                                            else
                                            {

                                                var somePosLastXExtra = _array_of_last_frame_cube_pos[_index][count].X;
                                                var somePosLastYExtra = _array_of_last_frame_cube_pos[_index][count].Y;
                                                var somePosLastZExtra = _array_of_last_frame_cube_pos[_index][count].Z;

                                                var rigidXExtra = Math.Round(somePosLastXExtra * 10) / 10;
                                                var rigidYExtra = Math.Round(somePosLastYExtra * 10) / 10;
                                                var rigidZExtra = Math.Round(somePosLastZExtra * 10) / 10;

                                                var diffXExtra = Math.Abs(somePosLastXExtra - rigidXExtra);
                                                var diffYExtra = Math.Abs(somePosLastYExtra - rigidYExtra);
                                                var diffZExtra = Math.Abs(somePosLastZExtra - rigidZExtra);

                                                //Console.WriteLine(Math.Round(somePosLastX * 10) / 10);

                                                if (Math.Round(diffXExtra * 10) / 10 < 0.001f &&
                                                    Math.Round(diffYExtra * 10) / 10 < 0.001f &&
                                                    Math.Round(diffZExtra * 10) / 10 < 0.001f)
                                                {

                                                    if (currentLinearVelExtra.Length() < 0.35f && currentAngularVelExtra.Length() < 0.35f) //0.00035f == 400 approx => 0.00075f == 400 approx
                                                    {

                                                    }
                                                    else
                                                    {
                                                        //rigidbody.IsActive = true;
                                                        //rigidbody.AffectedByGravity = true;
                                                        //body.IsActive = true;
                                                        //body.AffectedByGravity = true;
                                                    }
                                                }


                                                var somePosLastX = _array_of_last_frame_cube_pos[_index][count].X;
                                                var somePosLastY = _array_of_last_frame_cube_pos[_index][count].Y;
                                                var somePosLastZ = _array_of_last_frame_cube_pos[_index][count].Z;

                                                var rigidX = Math.Round(somePosLastX * 10) / 10;
                                                var rigidY = Math.Round(somePosLastY * 10) / 10;
                                                var rigidZ = Math.Round(somePosLastZ * 10) / 10;

                                                var diffX = Math.Abs(somePosLastX - rigidX);
                                                var diffY = Math.Abs(somePosLastY - rigidY);
                                                var diffZ = Math.Abs(somePosLastZ - rigidZ);

                                                //Console.WriteLine(Math.Round(somePosLastX * 10) / 10);

                                                if (Math.Round(diffX * 10) / 10 < 0.001f &&
                                                    Math.Round(diffY * 10) / 10 < 0.001f &&
                                                    Math.Round(diffZ * 10) / 10 < 0.001f)
                                                {
                                                    JVector currentLinearVel = rigidbody.LinearVelocity;
                                                    JVector currentAngularVel = rigidbody.AngularVelocity;

                                                    //Console.WriteLine(Math.Round(somePosLastX * 10) / 10);

                                                    if (currentLinearVel.Length() < 0.35f && currentAngularVel.Length() < 0.35f) //0.00035f == 400 approx => 0.00075f == 400 approx
                                                    {
                                                        if (rigidbody.IsActive && !rigidbody.IsStatic)
                                                        {

                                                        }


                                                        //_objects_inactive_counter_00[_index][count] = 0;
                                                    }
                                                    else
                                                    {
                                                        //rigidbody.IsActive = true;
                                                        //rigidbody.AffectedByGravity = true;
                                                        //body.IsActive = true;
                                                        //body.AffectedByGravity = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        IEnumerator enumerator1 = rigidbody.CollisionIsland.Bodies.GetEnumerator();

                                        while (enumerator1.MoveNext())
                                        {
                                            RigidBody rigibodyExtra = (RigidBody)enumerator1.Current;
                                            JVector currentLinearVelExtra = rigibodyExtra.LinearVelocity;
                                            JVector currentAngularVelExtra = rigibodyExtra.AngularVelocity;

                                            if (rigibodyExtra == rigidbody)
                                            {
                                                var somePosLastX = _array_of_last_frame_cube_pos[_index][count].X;
                                                var somePosLastY = _array_of_last_frame_cube_pos[_index][count].Y;
                                                var somePosLastZ = _array_of_last_frame_cube_pos[_index][count].Z;

                                                var rigidX = Math.Round(somePosLastX * 10) / 10;
                                                var rigidY = Math.Round(somePosLastY * 10) / 10;
                                                var rigidZ = Math.Round(somePosLastZ * 10) / 10;

                                                var diffX = Math.Abs(somePosLastX - rigidX);
                                                var diffY = Math.Abs(somePosLastY - rigidY);
                                                var diffZ = Math.Abs(somePosLastZ - rigidZ);

                                                //Console.WriteLine(Math.Round(somePosLastX * 10) / 10);

                                                if (Math.Round(diffX * 10) / 10 < 0.001f &&
                                                    Math.Round(diffY * 10) / 10 < 0.001f &&
                                                    Math.Round(diffZ * 10) / 10 < 0.001f)
                                                {
                                                    JVector currentLinearVel = rigidbody.LinearVelocity;
                                                    JVector currentAngularVel = rigidbody.AngularVelocity;

                                                    //Console.WriteLine(Math.Round(somePosLastX * 10) / 10);

                                                    if (currentLinearVel.Length() < 0.35f && currentAngularVel.Length() < 0.35f) //0.00035f == 400 approx => 0.00075f == 400 approx
                                                    {
                                                        if (rigidbody.IsActive && !rigidbody.IsStatic)
                                                        {

                                                        }


                                                        //_objects_inactive_counter_00[_index][count] = 0;
                                                    }
                                                    else
                                                    {
                                                        //rigidbody.IsActive = true;
                                                        //rigidbody.AffectedByGravity = true;
                                                        //body.IsActive = true;
                                                        //body.AffectedByGravity = true;
                                                    }
                                                }
                                                //NO BREAK STATEMENT HERE
                                            }
                                            else
                                            {


                                                var somePosLastXExtra = _array_of_last_frame_cube_pos[_index][count].X;
                                                var somePosLastYExtra = _array_of_last_frame_cube_pos[_index][count].Y;
                                                var somePosLastZExtra = _array_of_last_frame_cube_pos[_index][count].Z;

                                                var rigidXExtra = Math.Round(somePosLastXExtra * 10) / 10;
                                                var rigidYExtra = Math.Round(somePosLastYExtra * 10) / 10;
                                                var rigidZExtra = Math.Round(somePosLastZExtra * 10) / 10;

                                                var diffXExtra = Math.Abs(somePosLastXExtra - rigidXExtra);
                                                var diffYExtra = Math.Abs(somePosLastYExtra - rigidYExtra);
                                                var diffZExtra = Math.Abs(somePosLastZExtra - rigidZExtra);

                                                //Console.WriteLine(Math.Round(somePosLastX * 10) / 10);

                                                if (Math.Round(diffXExtra * 10) / 10 < 0.001f &&
                                                    Math.Round(diffYExtra * 10) / 10 < 0.001f &&
                                                    Math.Round(diffZExtra * 10) / 10 < 0.001f)
                                                {
                                                    if (currentLinearVelExtra.Length() < 0.35f && currentAngularVelExtra.Length() < 0.35f) //0.00035f == 400 approx => 0.00075f == 400 approx
                                                    {

                                                    }
                                                    else
                                                    {
                                                        //rigidbody.IsActive = true;
                                                        //rigidbody.AffectedByGravity = true;
                                                        //body.IsActive = true;
                                                        //body.AffectedByGravity = true;
                                                    }
                                                }



                                                var somePosLastX = _array_of_last_frame_cube_pos[_index][count].X;
                                                var somePosLastY = _array_of_last_frame_cube_pos[_index][count].Y;
                                                var somePosLastZ = _array_of_last_frame_cube_pos[_index][count].Z;

                                                var rigidX = Math.Round(somePosLastX * 10) / 10;
                                                var rigidY = Math.Round(somePosLastY * 10) / 10;
                                                var rigidZ = Math.Round(somePosLastZ * 10) / 10;

                                                var diffX = Math.Abs(somePosLastX - rigidX);
                                                var diffY = Math.Abs(somePosLastY - rigidY);
                                                var diffZ = Math.Abs(somePosLastZ - rigidZ);

                                                //Console.WriteLine(Math.Round(somePosLastX * 10) / 10);

                                                if (Math.Round(diffX * 10) / 10 < 0.001f &&
                                                    Math.Round(diffY * 10) / 10 < 0.001f &&
                                                    Math.Round(diffZ * 10) / 10 < 0.001f)
                                                {
                                                    JVector currentLinearVel = rigidbody.LinearVelocity;
                                                    JVector currentAngularVel = rigidbody.AngularVelocity;

                                                    //Console.WriteLine(Math.Round(somePosLastX * 10) / 10);

                                                    if (currentLinearVel.Length() < 0.35f && currentAngularVel.Length() < 0.35f) //0.00035f == 400 approx => 0.00075f == 400 approx
                                                    {
                                                        if (rigidbody.IsActive && !rigidbody.IsStatic)
                                                        {

                                                        }

                                                    }
                                                    else
                                                    {
                                                        //rigidbody.IsActive = true;
                                                        //rigidbody.AffectedByGravity = true;
                                                        //body.IsActive = true;
                                                        //body.AffectedByGravity = true;
                                                    }
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

                    }
                }
                else
                {

                }

            }




            /*if (!rigidbody.AffectedByGravity)
                    {
                        /*if (rigidbody.CollisionIsland != null)
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
                                            }*/

        }

        int switch_4_started_physics = 0;
        int switch_4_started_physics_counter = 0;








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
        int _tier_logic_swtch_lock_screen = 0;

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


        const int _MaxArraySize0 = 10; //50
        const int _MaxArraySize1 = 9; //49

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

        Vector3 forward = Vector3.Zero;
        Vector3 left = Vector3.Zero;
        Vector3 up = Vector3.Zero;


        //https://www.gamedev.net/forums/topic/56471-extracting-direction-vectors-from-quaternion/
        public void _newgetDirectiontotal(SharpDX.Quaternion rotation, out Vector3 forward, out Vector3 left, out Vector3 up)
        {
            //forward vector
            forward.X = 2 * (rotation.X * rotation.Z + rotation.W * rotation.Y);
            forward.Y = 2 * (rotation.Y * rotation.Z - rotation.W * rotation.X);
            forward.Z = 1 - 2 * (rotation.X * rotation.X + rotation.Y * rotation.Y);

            //up vector
            up.X = 2 * (rotation.X * rotation.Y - rotation.W * rotation.Z);
            up.Y = 1 - 2 * (rotation.X * rotation.X + rotation.Z * rotation.Z);
            up.Z = 2 * (rotation.Y * rotation.Z + rotation.W * rotation.X);

            //left vector
            left.X = 1 - 2 * (rotation.Y * rotation.Y + rotation.Z * rotation.Z);
            left.Y = 2 * (rotation.X * rotation.Y + rotation.W * rotation.Z);
            left.Z = 2 * (rotation.X * rotation.Z - rotation.W * rotation.Y);
        }

        Vector3 dirforward;
        public Vector3 _newgetdirforward(SharpDX.Quaternion rotation)
        {
            //forward vector
            dirforward.X = 2 * (rotation.X * rotation.Z + rotation.W * rotation.Y);
            dirforward.Y = 2 * (rotation.Y * rotation.Z - rotation.W * rotation.X);
            dirforward.Z = 1 - 2 * (rotation.X * rotation.X + rotation.Y * rotation.Y);
            return dirforward;
        }

        Vector3 dirup;
        public Vector3 _newgetdirup(SharpDX.Quaternion rotation)
        {
            //up vector
            dirup.X = 2 * (rotation.X * rotation.Y - rotation.W * rotation.Z);
            dirup.Y = 1 - 2 * (rotation.X * rotation.X + rotation.Z * rotation.Z);
            dirup.Z = 2 * (rotation.Y * rotation.Z + rotation.W * rotation.X);
            return dirup;
        }

        Vector3 dirleft;
        public Vector3 _newgetdirleft(SharpDX.Quaternion rotation)
        {
            //left vector
            dirleft.X = 1 - 2 * (rotation.Y * rotation.Y + rotation.Z * rotation.Z);
            dirleft.Y = 2 * (rotation.X * rotation.Y + rotation.W * rotation.Z);
            dirleft.Z = 2 * (rotation.X * rotation.Z - rotation.W * rotation.Y);
            return dirleft;
        }












































        //HUMAN RIG VARIABLES
        JMatrix matrixer = JMatrix.Identity;
        JQuaternion resultQuat;
        JMatrix matrixIn = JMatrix.Identity;





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

        sc_voxel.DLightBuffer[] _SC_modL_head_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel[] _player_head;

        sc_voxel.DLightBuffer[] _SC_modL_pelvis_BUFFER = new sc_voxel.DLightBuffer[1];
        public sc_voxel[] _player_pelvis;

        sc_voxel.DLightBuffer[] _SC_modL_rght_hnd_BUFFER = new sc_voxel.DLightBuffer[1];
        public sc_voxel[] _player_rght_hnd;

        sc_voxel.DLightBuffer[] _SC_modL_lft_hnd_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel[] _player_lft_hnd;

        sc_voxel.DLightBuffer[] _SC_modL_torso_BUFFER = new sc_voxel.DLightBuffer[1];
        public sc_voxel[] _player_torso;

        sc_voxel.DLightBuffer[] _SC_modL_rght_shldr_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel[] _player_rght_shldr;

        sc_voxel.DLightBuffer[] _SC_modL_lft_shldr_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel[] _player_lft_shldr;

        sc_voxel.DLightBuffer[] _SC_modL_rght_elbow_target_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel[] _player_rght_elbow_target;

        sc_voxel.DLightBuffer[] _SC_modL_lft_elbow_target_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel[] _player_lft_elbow_target;

        sc_voxel.DLightBuffer[] _SC_modL_lft_lower_arm_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel[] _player_lft_lower_arm;

        sc_voxel.DLightBuffer[] _SC_modL_rght_lower_arm_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel[] _player_rght_lower_arm;

        sc_voxel.DLightBuffer[] _SC_modL_lft_upper_arm_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel[] _player_lft_upper_arm;

        sc_voxel.DLightBuffer[] _SC_modL_rght_upper_arm_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel[] _player_rght_upper_arm;

        sc_voxel.DLightBuffer[] _SC_modL_rght_elbow_target_two_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel[] _player_rght_elbow_target_two;

        sc_voxel.DLightBuffer[] _SC_modL_lft_elbow_target_two_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel[] _player_lft_elbow_target_two;


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












/*
//float pointx = (float)Math.Atan2(2f * quat.X * quat.W + 2f * quat.Y * quat.Z, 1 - 2f * ((quat.Z * quat.Z) + (quat.W * quat.W)));     // Yaw 
//float pointy = (float)Math.Asin(2f * (quat.X * quat.Z - quat.W * quat.Y));                             // Pitch 
//float pointz = (float)Math.Atan2(2f * quat.X * quat.Y + 2f * quat.Z * quat.W, 1 - 2f * ((quat.Y * quat.Y) + (quat.Z * quat.Z)));

/*float pitch;
float yaw;
float roll;
ToEulerAngles(quat, out pitch, out yaw, out roll);

float pointx = (float)(Math.Cos(yaw) * Math.Cos(pitch));
float pointy = (float)(Math.Sin(yaw) * Math.Cos(pitch));
float pointz = (float)(Math.Sin(pitch));

//cannot use the quaternion. gotta use angles.
//https://stackoverflow.com/questions/19673067/finding-point-on-sphere
float pointx = (float)(xxx + (1 * Math.Cos(pitch) * Math.Sin(yaw)));
float pointy = (float)(yyy + (1 * Math.Sin(pitch) * Math.Sin(yaw)));
float pointz = (float)(zzz + (1 * Math.Cos(yaw)));*
Vector3 newVec = new Vector3(pointx, pointy, pointz);
newVec.Normalize();

voxel_cuber_r_hnd.instancesDataForward[i].rotation.X = newVec.X - voxel_sometester_r_hnd[i].M41;
voxel_cuber_r_hnd.instancesDataForward[i].rotation.Y = newVec.Y - voxel_sometester_r_hnd[i].M42;
voxel_cuber_r_hnd.instancesDataForward[i].rotation.Z = newVec.Z - voxel_sometester_r_hnd[i].M43;
voxel_cuber_r_hnd.instancesDataForward[i].rotation.W = 1;
//var dirForward = new Vector3();
//https://en.wikipedia.org/wiki/Conversion_between_quaternions_and_Euler_angles
void ToEulerAngles(Quaternion q, out float pitch, out float yaw, out float roll)
{
float angles = 0;
pitch = 0;
yaw = 0;
roll = 0;

// roll (x-axis rotation)
double sinr_cosp = 2 * (q.W * q.X + q.Y * q.Z);
double cosr_cosp = 1 - 2 * (q.X * q.X + q.Y * q.Y);
roll = (float)Math.Atan2(sinr_cosp, cosr_cosp);

// pitch (y-axis rotation)
double sinp = 2 * (q.W * q.Y - q.Z * q.X);
if (Math.Abs(sinp) >= 1)
pitch = (float)Math.CopySign(Math.PI / 2, sinp); // use 90 degrees if out of range
else
pitch = (float)Math.Asin(sinp);

// yaw (z-axis rotation)
double siny_cosp = 2 * (q.W * q.Z + q.X * q.Y);
double cosy_cosp = 1 - 2 * (q.Y * q.Y + q.Z * q.Z);
yaw = (float)Math.Atan2(siny_cosp, cosy_cosp);
}

*/




/*bool _boundingBoxer = _world_list[_index].CollisionSystem.CheckBoundingBoxes(rigidbody, _world_screen_list[0]._arrayOfInstances[0].transform.Component.rigidbody);

if (!_boundingBoxer)
{
    if (currentLinearVel.Length() < 0.00035f && currentAngularVel.Length() < 0.00035f) //0.00035f == 400 approx => 0.00075f == 400 approx
    {
        hasbreakeder = 1;
        rigidbody.IsActive = false;
        rigidbody.AffectedByGravity = true;
        break;
    }
    else
    {
        rigidbody.IsActive = true;
        rigidbody.AffectedByGravity = true;
        hasbreakeder = 1;
        //body.IsActive = true;
        //body.AffectedByGravity = true;
        break;
    }
}
else
{
    if (currentLinearVel.Length() < 0.00035f && currentAngularVel.Length() < 0.00035f) //0.00035f == 400 approx => 0.00075f == 400 approx
    {
        hasbreakeder = 1;
        rigidbody.IsActive = true;
        rigidbody.AffectedByGravity = false;
        break;
    }
    else
    {
        rigidbody.IsActive = true;
        rigidbody.AffectedByGravity = false;

        hasbreakeder = 1;
        //body.IsActive = true;
        //body.AffectedByGravity = true;
        break;
    }
}*/
