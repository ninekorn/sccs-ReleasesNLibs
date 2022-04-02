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

namespace SCCoreSystems
{
    public class SC_Console_GRAPHICS
    {
        int _has_init_screen = 0;

        //PHYSICS ENGINE SETTINGS
        public static World[] _world_list;
        public const int _physics_engine_instance_x = 4; //4
        public const int _physics_engine_instance_y = 1; //1
        public const int _physics_engine_instance_z = 4; //4
        JVector _world_gravity = new JVector(0, 0, 0); //-9.81f base
        int _world_iterations = 10; // as high as possible normally for higher precision
        int _world_small_iterations = 10; // as high as possible normally for higher precision
        float _world_allowed_penetration = 0.01f;
        //END OF

        //HUMAN RIG
        const int _human_inst_rig_x = 2;
        const int _human_inst_rig_y = 1;
        const int _human_inst_rig_z = 2;
        const int _addToWorld = 0;

        //PHYSICS CUBES
        int _inst_cube_x = 4;
        int _inst_cube_y = 20;
        int _inst_cube_z = 4;
        float _cube_size_x = 0.015f; //0.0115f //1.5f
        float _cube_size_y = 0.015f; //0.0115f //1.5f
        float _cube_size_z = 0.015f;
        //END OF


        //static cubes 
        int _inst_terrain_tile_x = 1;
        int _inst_terrain_tile_y = 1;
        int _inst_terrain_tile_z = 1;
        //main terrain.
        float _terrain_size_x = 1000;
        float _terrain_size_y = 10;
        float _terrain_size_z = 1000;


        public static SharpDX.Vector3 originPos = new SharpDX.Vector3(0, 1, 1);
        SharpDX.Vector3 originPosScreen = new SharpDX.Vector3(0, 1, -0.25f);

        SC_cube[] _world_screen_list;
        Matrix[][] worldMatrix_instances_screens;
        Matrix[][] world_last_Matrix_instances_screens;

        SC_cube[] _world_cube_list;
        Matrix[][] worldMatrix_instances_cubes;

        //Vector3[][] _last_frame_force;

        SC_cube _world_terrain;
        SC_cube[] _world_terrain_tile_list;

        Matrix[][] worldMatrix_instances_terrain_tiles;
        Matrix[][] worldMatrix_instances_terrain;

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

        float speedRot = 0.085f;
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


        SCCoreSystems.SC_Graphics.SC_cube.DLightBuffer[] _DLightBuffer = new SC_cube.DLightBuffer[1];
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

            //FRAME DELTATIME
            _delta_timer_frame = (float)Math.Abs((timeStopWatch01.Elapsed.Ticks - timeStopWatch00.Elapsed.Ticks)) * 100000000f;

            time2 = DateTime.Now;
            _delta_timer_time = (time2.Ticks - time1.Ticks) * 100000000f; //100000000f
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



        public bool Initialize(SCCoreSystems.sc_core.sc_system_configuration configuration, IntPtr windowsHandle, sc_console.sc_console_writer _writer)
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


                worldMatrix_instances_terrain = new Matrix[1][]; //1 terrain only

                _world_list = new World[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
                _world_cube_list = new SC_cube[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];

                worldMatrix_instances_cubes = new Matrix[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                _array_of_colors = new Vector4[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];

                _objects_static_00 = new int[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                _objects_static_counter_00 = new int[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                _objects_rigid_static_00 = new RigidBody[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
                _switch_for_collision = new int[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];





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
                            World.AllowDeactivation = false;

                            World.Gravity = _world_gravity;
                            World.SetIterations(_world_iterations, _world_small_iterations);
                            World.ContactSettings.AllowedPenetration = _world_allowed_penetration;

                            _world_list[_index] = World;


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

                            var _hasinit02 = _cube.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, 0.1f, 1, 1, _cube_size_x, _cube_size_y, _cube_size_z, new Vector4(r, g, b, a), _inst_cube_x, _inst_cube_y, _inst_cube_z, Hwnd, _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, SC_console_directx.BodyTag.physicsInstancedCube, false, 1, 100, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                            _world_cube_list[_index] = _cube;

                            worldMatrix_instances_cubes[_index] = new Matrix[_inst_cube_x * _inst_cube_y * _inst_cube_z];

                            for (int i = 0; i < worldMatrix_instances_cubes[_index].Length; i++)
                            {
                                worldMatrix_instances_cubes[_index][i] = Matrix.Identity;
                            }
                        }
                    }
                }

                
                World = _world_list[0];
                











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

                }
                

                _basicTexture = new _sc_texture_loader();
                bool _hasinit1 = _basicTexture.Initialize(D3D.Device, "../../../terrainGrassDirt.bmp");
                _pink_texture = new _sc_texture_loader();
                _hasinit1 = _pink_texture.Initialize(D3D.Device, "../../../1x1_pink_color.png");

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
            var _indexer = 0;// x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z);

            //HEADSET POSITION
            displayMidpoint = D3D.OVR.GetPredictedDisplayTime(D3D.sessionPtr, 0);
            trackingState = D3D.OVR.GetTrackingState(D3D.sessionPtr, displayMidpoint, true);
            latencyMark = false;
            trackState = D3D.OVR.GetTrackingState(D3D._oculusRiftVirtualRealityProvider.SessionPtr, 0.0f, latencyMark);
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
                                Stopwatch _this_thread_ticks_01 = new Stopwatch();
                                _this_thread_ticks_01.Start();

                            _thread_looper:


                                var _delta_timer = (float)Math.Abs((_this_thread_ticks_01.Elapsed.Ticks - _this_thread_ticks_00.Elapsed.Ticks)) * 0.0000000001f; // very slow /100000000000

                                for (int x = 0; x < _physics_engine_instance_x; x++)
                                {
                                    for (int y = 0; y < _physics_engine_instance_y; y++)
                                    {
                                        for (int z = 0; z < _physics_engine_instance_z; z++)
                                        {
                                            if (_world_list[x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z)] != null)
                                            {
                                                if (_delta_timer > 1.0f * 0.01f)
                                                {
                                                    _delta_timer = 1.0f * 0.01f;
                                                }

                                                _world_list[x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z)].Step(_delta_timer, true);
                                            }
                                        }
                                    }
                                }
                                //MessageBox((IntPtr)0, _this_thread_ticks.ElapsedTicks + "", "Oculus error", 0);
                                //Console.WriteLine("ticks: " + _this_thread_ticks.ElapsedTicks);

                                Thread.Sleep(0);
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

                                                    
                                                    if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.Terrain)
                                                    {
                                                        Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                        quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                        tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                        Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                        Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                        worldMatrix_instances_terrain[0][0] = translationMatrix;
                                                        _terrain_count++;
                                                    }
                                                    
                                                    else if ((SC_console_directx.BodyTag)body.Tag == SC_console_directx.BodyTag.physicsInstancedCube)
                                                    {
                                                        Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                        quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                        tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                        Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                        Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                        worldMatrix_instances_cubes[_index][_cube_counter] = translationMatrix;
                                                        _cube_counter++;
                                                    }
                                                }
                                            }
                                        }
                                    }
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





                _DLightBuffer[0] = new SCCoreSystems.SC_Graphics.SC_cube.DLightBuffer()
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
                

                for (int x = 0; x < _physics_engine_instance_x; x++)
                {
                    for (int y = 0; y < _physics_engine_instance_y; y++)
                    {
                        for (int z = 0; z < _physics_engine_instance_z; z++)
                        {
                            var indexer00 = x + _physics_engine_instance_x * (y + _physics_engine_instance_y * z);

                            //PHYSICS CUBES
                            _world_cube_list[indexer00]._WORLDMATRIXINSTANCES = worldMatrix_instances_cubes[indexer00];
                            _world_cube_list[indexer00]._POSITION = worldMatrix_base[0];
                            //END OF 
                            
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
                        }
                    }
                }

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

        Vector3 dirLight = new Vector3(0, -1, 0);



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










