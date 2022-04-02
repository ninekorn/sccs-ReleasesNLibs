using System;
using System.Collections.Generic;
using System.Text;

using SharpDX;
using SCCoreSystems.SC_Graphics;
using Jitter;
using Jitter.Dynamics;
using Jitter.Collision;
using Jitter.LinearMath;
using Jitter.Collision.Shapes;
using Jitter.Forces;

using SC_message_object = sc_message_object.sc_message_object;
using SC_message_object_jitter = sc_message_object.sc_message_object_jitter;

using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Ab3d.OculusWrap;
using SC_WPF_RENDER;
using SC_WPF_RENDER.SC_Graphics;
using SC_WPF_RENDER.SC_Graphics.SC_Grid;

using SCCoreSystems.SC_Graphics;
using Ab3d.OculusWrap.DemoDX11;

using System.Runtime.InteropServices;

using System.IO;


using Jitter.DataStructures;
using SingleBodyConstraints = Jitter.Dynamics.Constraints.SingleBody;
//using Jitter.Dynamics.Constraints.SingleBody;
//using Jitter.LinearMath;
using Jitter.Dynamics.Constraints;
//using Jitter.Dynamics.Joints;
//using Jitter.Forces;

//using DeltaEngine.Datatypes;
//using DeltaEngine.Core;
//using DeltaEngine.Extensions;

using Vector2 = SharpDX.Vector2;
using Vector3 = SharpDX.Vector3;
using Vector4 = SharpDX.Vector4;
using Quaternion = SharpDX.Quaternion;
using Matrix = SharpDX.Matrix;
using Plane = SharpDX.Plane;
using Ray = SharpDX.Ray;

using SCCoreSystems.SC_Graphics.SC_Grid;
using System.Text;
using System.IO;
using SharpDX.Multimedia;
using SharpDX.IO;
using System.Xml;
using SharpDX.XAudio2;
using System.Linq;

//using System.Windows.Forms;
using System.IO.Ports;

namespace SCCoreSystems.sc_console
{
    public class sc_graphics_sec //: SC_Update//SC_Intermediate_Update
    {
        Quaternion _testQuater;

        int[] arrayOfPixData;
        int setArray = 0;

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        int writeProcess = 0;

        [StructLayout(LayoutKind.Sequential)]
        public struct DHeightMapType
        {
            public float x, y, z;
        }
        //[DllImport("user32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);
        //[DllImport("user32.dll", SetLastError = true)]
        //static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern long GetWindowRect(IntPtr hWnd, ref System.Drawing.Rectangle lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        Rectangle myRect = new Rectangle();


        IntPtr MSEdgeHandle;

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);


        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;


        //https://stackoverflow.com/questions/61451756/how-to-check-if-a-user-has-a-browser-open-in-c-sharp user Metzgermeister
        internal class BrowserDetector
        {
            private readonly Dictionary<string, string> browsers = new Dictionary<string, string>
            {
                {
                    "firefox", "Mozilla Firefox"
                },
                {
                    "chrome", "Google Chrome"
                },
                {
                    "iexplore", "Internet Explorer"
                },
                {
                    "MicrosoftEdgeCP", "Microsoft Edge"
                }
                ,
                {
                    "msedge", "Microsoft Edge"
                }
                 ,
                {
                    "MicrosoftEdge", "Microsoft Edge"
                }
                



                // add other browsers
            };

            public bool BrowserIsOpen()
            {
                return Process.GetProcesses().Any(this.IsBrowserWithWindow);
            }

            private bool IsBrowserWithWindow(Process process)
            {
                return this.browsers.TryGetValue(process.ProcessName, out var browserTitle) && process.MainWindowTitle.Contains(browserTitle);
            }
        }




        //[DllImport("user32.dll")]
        //public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);
        //Rect r = new Rect();
        //GetWindowRect(hwnd, ref r);




        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        //[DllImport("user32.dll")]
        //private static extern bool GetWindowRect(IntPtr hWnd, Rectangle rect);


        float heightmapscale = 0.001f;
        float heightmapscaleMin = 0.0001f;
        float heightmapscaleMax = 100f;


        public DColorShader ColorShader { get; set; }
        public DFontShader FontShader { get; set; }


        float totalDiffX = 0;
        float totalDiffY = 0;
        float totalDiffZ = 0;

        public static double touchRXLast = 0;
        public static double touchRYLast = 0;
        public static double touchRZLast = 0;


        public static double touchRX = 0;
        public static double touchRY = 0;
        public static double touchRZ = 0;

        Matrix grabbedBodyMatrix = Matrix.Identity;


        double pitchTouchRer = 0;
        double yawTouchRer = 0;
        double rollTouchRer = 0;

        Matrix rotMatForPelvis = SharpDX.Matrix.Identity;



        Vector3 current_rotation_of_torso_pivot_forward = Vector3.Zero;
        Vector3 current_rotation_of_torso_pivot_right = Vector3.Zero;
        Vector3 current_rotation_of_torso_pivot_up = Vector3.Zero;


        Vector3 rayDirUp = Vector3.Zero;
        Vector3 rayDirRight = Vector3.Zero;
        Vector3 rayDirForward = Vector3.Zero;


        double grabrotX = 0;
        double grabrotY = 0;
        double grabrotZ = 0;


        double grabrotDiffx = 0;
        double grabrotDiffy = 0;
        double grabrotDiffz = 0;


        /*protected override void SC_Init_DirectX() //DSystemConfiguration configuration, IntPtr Hwnd, sc_console.sc_console_writer _writer
        {
            base.SC_Init_DirectX(); //configuration, Hwnd, _writer
        }*/

        /*public override SC_message_object_jitter[][] sc_write_to_buffer(SC_message_object_jitter[][] _sc_jitter_tasks)
        {
            return _sc_jitter_tasks;
            //base.sc_write_to_buffer(_sc_jitter_tasks);
        }

        public override SC_message_object_jitter[][] loop_worlds(SC_message_object_jitter[][] _sc_jitter_tasks)
        {
            return _sc_jitter_tasks;
            //base.sc_write_to_buffer(_sc_jitter_tasks);
        }
        public override SC_message_object_jitter[][] workOnSomething(SC_message_object_jitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft)
        {
            return _sc_jitter_tasks;
            //base.sc_write_to_buffer(_sc_jitter_tasks);
        }
        public override SC_message_object_jitter[][] _sc_create_world_objects(SC_message_object_jitter[][] _sc_jitter_tasks)
        {
            return _sc_jitter_tasks;
            //base.sc_write_to_buffer(_sc_jitter_tasks);
        }


        protected override SC_message_object_jitter[][] init_update_variables(SC_message_object_jitter[][] _sc_jitter_tasks, SCCoreSystems.sc_core.sc_system_configuration configuration, IntPtr hwnd, sc_console.sc_console_writer _writer)
        {

        }
        protected override SC_message_object_jitter[][] Update(_sc_jitter_physics[] _sc_jitter_physics, SC_message_object_jitter[][] _sc_jitter_tasks)
        {
            base.Update(_sc_jitter_physics, _sc_jitter_tasks);
        }*/


        Vector3 direction_feet_forward_ori = Vector3.Zero;
        Vector3 direction_feet_right_ori = Vector3.Zero;
        Vector3 direction_feet_up_ori = Vector3.Zero;



        int sc_menu_scroller = 0;
        int sc_menu_scroller_counter = 0;

        Vector4 ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
        Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
        Vector3 lightDirection = new Vector3(0, -1, -1);
        Vector3 dirLight = new Vector3(0, -1, 0);
        Vector3 lightpos = new Vector3(0, 20, 0);

        SharpDX.Matrix _oculusR_Cursor_matrix = SharpDX.Matrix.Identity;
        Stopwatch _updateFunctionStopwatchRightThumbstickGoRight = new Stopwatch();
        Stopwatch _updateFunctionStopwatchRightThumbstickGoLeft = new Stopwatch();
        Stopwatch _updateFunctionStopwatchLeftThumbstick = new Stopwatch();
        Stopwatch _updateFunctionStopwatchRight = new Stopwatch();

        int frame_counter_4_buttonY = 45;
        int display_grid_type = 0;

        int gravity_swtch_counter = 45;
        int gravity_swtch = 0;

        Matrix hmd_matrix_current = Matrix.Identity;

        SharpDX.Matrix _intersectTouchRightMatrix = SharpDX.Matrix.Identity;
        SharpDX.Matrix _intersectTouchLeftMatrix = SharpDX.Matrix.Identity;

        Matrix final_hand_pos_right_locked;
        Matrix final_hand_pos_left_locked;
        Matrix tempMatrix = Matrix.Identity;// tempMatrix
        Matrix _last_screen_pos = Matrix.Identity;
        int had_locked_screen = -1;
        int _tier_logic_swtch_lock_screen = 0;
        Matrix _current_screen_pos = Matrix.Identity;

        //OCULUS TOUCH SETTINGS 
        Ab3d.OculusWrap.Result resultsRight;
        uint buttonPressedOculusTouchRight;
        Vector2f[] thumbStickRight;
        float[] handTriggerRight;
        float[] indexTriggerRight;

        float indexTriggerRightLastAbs;
        float indexTriggerLeftLastAbs;



        Ab3d.OculusWrap.Result resultsLeft;
        uint buttonPressedOculusTouchLeft;
        Vector2f[] thumbStickLeft;
        float[] handTriggerLeft;
        float[] indexTriggerLeft;
        Posef handPoseLeft;
        SharpDX.Quaternion _leftTouchQuat;
        Posef handPoseRight;
        SharpDX.Quaternion _rightTouchQuat;
        Matrix _leftTouchMatrix = Matrix.Identity;
        Matrix _rightTouchMatrix = Matrix.Identity;
        //OCULUS TOUCH SETTINGS

        float disco_sphere_rot_speed = 0.15f;
        float force_4_voxel = 0.0015f;
        float force_4_cubes = 0.0015f;
        float force_4_screen = 0.0015f;
        int _has_locked_screen_pos = 0;
        int _has_locked_screen_pos_counter = 0;
        Matrix _direction_offsetter;
        Matrix _screen_direction_offsetter_two;
        float sizeWidtherer = 0.0f;
        float sizeheighterer = 0.0f;
        Matrix[] worldMatrix_Cloth_instances;

        PseudoCloth sc_jitter_cloth;
        double RotationScreenY { get; set; }
        double RotationScreenX { get; set; }
        double RotationScreenZ { get; set; }

        Matrix originRotScreen;
        Matrix rotatingMatrixScreen;
        float oriRotationScreenY { get; set; }
        float oriRotationScreenX { get; set; }
        float oriRotationScreenZ { get; set; }

        struct _rigid_data
        {
            public RigidBody _body;
            public Matrix position;
            public Vector3 directionToGrabber;
            public Vector3 rayGrabDir;
            public float rayGrabDirLength;
            public Vector3 grabHitPoint;
            public float grabHitPointLength;
            public float dirDiffX;
            public float dirDiffY;
            public float dirDiffZ;

            public int _index;
            public int _physics_engine_index;
        }

        _rigid_data _grab_rigid_data;
        float _size_screen;
        int[][][] swtch_for_last_pos;
        int tempIndex = 0;
        int _inactive_counter_cubes = 0;
        int _inactive_counter_voxels = 0;

        int _static_counter = 0;
        Quaternion quat_buffers;

        //SCREEN SETTINGS
        int _inst_screen_x = 1;
        int _inst_screen_y = 1;
        int _inst_screen_z = 1;

        float _screen_size_x = 2; //0.0115f //1.5f
        float _screen_size_y = 2; //0.0115f //1.5f
        float _screen_size_z = 0.0035f; //0.0025f

        float mulScreen = 0.85f;

        int _inst_screen_assets_x = 3;
        int _inst_screen_assets_y = 1;
        int _inst_screen_assets_z = 3;

        float _screen_assets_size_x = 0.005f; //0.0115f //1.5f
        float _screen_assets_size_y = 0.005f; //0.0115f //1.5f
        float _screen_assets_size_z = 0.025f;

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
        int _inst_voxel_cube_x = 1;
        int _inst_voxel_cube_y = 1;
        int _inst_voxel_cube_z = 1;
        float _voxel_cube_size_x = 0.15f;//0.0115f //restitution
        float _voxel_cube_size_y = 0.15f;//0.0115f //static friction
        float _voxel_cube_size_z = 0.15f;//0.0015f //kinetic friction
        float voxel_general_size = 0.0025f;
        int voxel_type = -1;

        float _voxel_rig_cube_size_x = 0.15f;//0.0115f //restitution
        float _voxel_rig_cube_size_y = 0.15f;//0.0115f //static friction
        float _voxel_rig_cube_size_z = 0.15f;//0.0015f //kinetic friction


        //1,024‬
        //1,024‬
        //1,024‬
        //1,024‬
        //1,024‬
        //1,024‬

        //PHYSICS CUBES
        int _inst_cube_x = 2;
        int _inst_cube_y = 2;
        int _inst_cube_z = 2;

        int _inst_other_x = 1;
        int _inst_other_y = 1;
        int _inst_other_z = 1;


        float _cube_size_x = 0.025f; //0.0115f //1.5f
        float _cube_size_y = 0.025f; //0.0115f //1.5f
        float _cube_size_z = 0.025f;
        //END OF

        //PHYSICS GRID
        int _inst_grid_x = 1;
        int _inst_grid_y = 1;
        int _inst_grid_z = 1;
        float _grid_size_x = 10; //0.0115f //1.5f
        float _grid_size_y = 1; //0.0115f //1.5f
        float _grid_size_z = 10;
        //END OF

        //float _voxel_cube_size_x = 0.0515f;
        //float _voxel_cube_size_y = 0.0515f;
        //float _voxel_cube_size_z = 0.0515f;


        //SPECTRUM
        //SPECTRUM
        //SPECTRUM
        const int _inst_spectrum_x = 420; // 36 // 210 //75
        const int _inst_spectrum_y = 1;
        const int _inst_spectrum_z = 210; // 36 // 210 //75 //5625
        float _spectrum_size_x = 0.0015f; //0.001115f
        float _spectrum_size_y = 0.0015f;
        float _spectrum_size_z = 0.0015f;
        byte[] _sound_byte_array = new byte[_inst_spectrum_x * _inst_spectrum_z]; //44100
        byte[] _sound_byte_array_instant = new byte[_inst_spectrum_x * _inst_spectrum_z]; //44100 //176400
        int has_spoken_main = 0;
        int has_spoken_sec = 0;
        int has_spoken_tier = 0;
        int has_spoken_quart = 0;
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
        string short_path = "";
        string instant_short_path = "";
        float spectrum_noise_value = 0;
        SoundPlayer _sound_player = new SoundPlayer();
        Matrix spectrum_mat = Matrix.Identity;
        static XmlTextWriter writer = new XmlTextWriter(Console.Out);
        string path;
        int _records_counter = 0;
        int _records_instant_counter = 0;
        int _frames_to_access_counter = 0;
        int _spectrum_work = 0;
        int _spectrum_work_counter = 0;
        int _has_recorded = 0;

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
        int swtchinstantsound = -1;
        //END OF
        //END OF
        //END OF

        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);




        //static cubes 
        int _inst_terrain_tile_x = 1;
        int _inst_terrain_tile_y = 1;
        int _inst_terrain_tile_z = 1;
        float _terrain_tile_size_x = 0.015f;
        float _terrain_tile_size_y = 0.05f;
        float _terrain_tile_size_z = 0.015f;

        //main terrain.
        float _terrain_size_x = 3;
        float _terrain_size_y = 0.095f; //0.02f too small objects go through
        float _terrain_size_z = 3;


        //main terrain.
        float _platform_size_x = 3;
        float _platform_size_y = 2; //0.02f too small objects go through
        float _platform_size_z = 3;


        //main terrain.
        float _floor_size_x = 13;
        float _floor_size_y = 13;
        float _floor_size_z = 13;

        //float _size__neg_x = 1.175494351F - 38;
        //float _size__pos_x = 3.402823466F + 38;
        int _type_of_cube = 3;





        Matrix WorldMatrix = Matrix.Identity;
        Matrix _object_worldmatrix = Matrix.Identity;

        Matrix heightMapWorldMatrix = Matrix.Identity;


        //HUMAN RIG
        int _inst_p_upper_l_leg_x = _human_inst_rig_x;
        int _inst_p_upper_l_leg_y = _human_inst_rig_y;
        int _inst_p_upper_l_leg_z = _human_inst_rig_z;
        int _inst_p_upper_r_leg_x = _human_inst_rig_x;
        int _inst_p_upper_r_leg_y = _human_inst_rig_y;
        int _inst_p_upper_r_leg_z = _human_inst_rig_z;
        int _inst_p_lower_l_leg_x = _human_inst_rig_x;
        int _inst_p_lower_l_leg_y = _human_inst_rig_y;
        int _inst_p_lower_l_leg_z = _human_inst_rig_z;
        int _inst_p_lower_r_leg_x = _human_inst_rig_x;
        int _inst_p_lower_r_leg_y = _human_inst_rig_y;
        int _inst_p_lower_r_leg_z = _human_inst_rig_z;
        int _inst_p_l_foot_x = _human_inst_rig_x;
        int _inst_p_l_foot_y = _human_inst_rig_y;
        int _inst_p_l_foot_z = _human_inst_rig_z;
        int _inst_p_r_foot_x = _human_inst_rig_x;
        int _inst_p_r_foot_y = _human_inst_rig_y;
        int _inst_p_r_foot_z = _human_inst_rig_z;
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





        sc_voxel_pchunk[] arrayOfPlanetChunk;
        //Matrix[] worldMatrix_instances_voxel_pchunk;
        Matrix[][][][] worldMatrix_instances_voxel_pchunk;


        sc_voxel voxel_cuber_r_hand_grab;
        sc_voxel voxel_cuber_l_hand_grab;
        sc_voxel voxel_cuber_r_lower_leg;
        sc_voxel voxel_cuber_l_lower_leg;
        sc_voxel voxel_cuber_r_foot;
        sc_voxel voxel_cuber_l_foot;
        sc_voxel voxel_cuber_r_upper_leg;
        sc_voxel voxel_cuber_l_upper_leg;
        sc_voxel voxel_cuber_l_targ_knee;
        sc_voxel voxel_cuber_r_targ_knee;
        sc_voxel voxel_cuber_l_targ_two_knee;
        sc_voxel voxel_cuber_r_targ_two_knee;
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

        Matrix[] voxel_sometester_r_hand_grab;
        Matrix[] voxel_sometester_l_hand_grab;
        Matrix[] voxel_sometester_r_upper_leg;
        Matrix[] voxel_sometester_l_upper_leg;
        Matrix[] voxel_sometester_r_lower_leg;
        Matrix[] voxel_sometester_l_lower_leg;
        Matrix[] voxel_sometester_r_foot;
        Matrix[] voxel_sometester_l_foot;
        Matrix[] voxel_sometester_r_hnd;
        Matrix[] voxel_sometester_l_hnd;
        Matrix[] voxel_sometester_l_up_arm;
        Matrix[] voxel_sometester_r_up_arm;
        Matrix[] voxel_sometester_l_low_arm;
        Matrix[] voxel_sometester_r_low_arm;
        Matrix[] voxel_sometester_l_shld;
        Matrix[] voxel_sometester_r_shld;
        Matrix[] voxel_sometester_l_targ;
        Matrix[] voxel_sometester_r_targ;
        Matrix[] voxel_sometester_l_targ_two;
        Matrix[] voxel_sometester_r_targ_two;
        Matrix[] voxel_sometester_pelvis;
        Matrix[] voxel_sometester_torso;
        Matrix[] voxel_sometester_l_targ_knee;
        Matrix[] voxel_sometester_r_targ_knee;
        Matrix[] voxel_sometester_l_targ_two_knee;
        Matrix[] voxel_sometester_r_targ_two_knee;

        sc_voxel.DLightBuffer[] _SC_modL_r_hand_grab_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_l_hand_grab_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_r_upper_leg_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_l_upper_leg_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_r_lower_leg_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_l_lower_leg_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_r_foot_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_l_foot_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_head_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_pelvis_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_rght_hnd_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_lft_hnd_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_torso_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_rght_shldr_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_lft_shldr_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_rght_elbow_target_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_lft_elbow_target_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_lft_lower_arm_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_rght_lower_arm_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_lft_upper_arm_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_rght_upper_arm_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_rght_elbow_target_two_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_lft_elbow_target_two_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_rght_elbow_target_knee_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_lft_elbow_target_knee_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_rght_elbow_target_two_knee_BUFFER = new sc_voxel.DLightBuffer[1];
        sc_voxel.DLightBuffer[] _SC_modL_lft_elbow_target_two_knee_BUFFER = new sc_voxel.DLightBuffer[1];


        Vector3[][][] point3DCollection;

        Matrix[][][] worldMatrix_instances_l_hand_grab;
        Matrix[][][] worldMatrix_instances_r_hand_grab;
        Matrix[][][] _screenDirMatrix_correct_pos;
        Matrix[][][] worldMatrix_instances_player_ik;
        Matrix[][][] worldMatrix_instances_voxel_cube;
        Matrix[][][] worldMatrix_instances_spectrum;
        Matrix[][][] worldMatrix_instances_DZgrid;
        Matrix[][][] worldMatrix_instances_floor;
        Matrix[][][] worldMatrix_instances_terrain_tiles;
        Matrix[][][] worldMatrix_instances_terrain;
        Matrix[][][] worldMatrix_instances_screen_assets;
        Matrix[][][] _screenDirMatrix;
        Matrix[][][] worldMatrix_instances_screens;
        Matrix[][][] world_last_Matrix_instances_screens;
        Matrix[][][] worldMatrix_instances_cubes;
        Matrix[][][] worldMatrix_instances_r_elbow_target;
        Matrix[][][] worldMatrix_instances_l_elbow_target;
        Matrix[][][] worldMatrix_instances_r_elbow_target_two;
        Matrix[][][] worldMatrix_instances_l_elbow_target_two;
        Matrix[][][] worldMatrix_instances_r_target_knee;
        Matrix[][][] worldMatrix_instances_l_target_knee;
        Matrix[][][] worldMatrix_instances_r_target_two_knee;
        Matrix[][][] worldMatrix_instances_l_target_two_knee;
        Matrix[][][] worldMatrix_instances_r_upper_leg;
        Matrix[][][] worldMatrix_instances_l_upper_leg;
        Matrix[][][] worldMatrix_instances_r_lower_leg;
        Matrix[][][] worldMatrix_instances_l_lower_leg;
        Matrix[][][] worldMatrix_instances_r_foot;
        Matrix[][][] worldMatrix_instances_l_foot;
        Matrix[][][] worldMatrix_instances_head;
        Matrix[][][] worldMatrix_instances_torso;
        Matrix[][][] worldMatrix_instances_pelvis;
        Matrix[][][] worldMatrix_instances_r_hand;
        Matrix[][][] worldMatrix_instances_l_hand;
        Matrix[][][] worldMatrix_instances_r_shoulder;
        Matrix[][][] worldMatrix_instances_l_shoulder;
        Matrix[][][] worldMatrix_instances_r_upperarm;
        Matrix[][][] worldMatrix_instances_l_upperarm;
        Matrix[][][] worldMatrix_instances_r_lowerarm;
        Matrix[][][] worldMatrix_instances_l_lowerarm;
        Matrix[][][] worldMatrix_instances_grid;
        Matrix[][][] worldMatrix_instances_containment_grid_RH;
        Matrix[][][] worldMatrix_instances_containment_grid_LH;
        Matrix[][][] worldMatrix_instances_containment_grid_screen;
        Matrix[][][] worldMatrix_instances_cone;
        Matrix[][][] worldMatrix_instances_cylinder;
        Matrix[][][] worldMatrix_instances_capsule;
        Matrix[][][] worldMatrix_instances_sphere;

        sc_voxel[][] _player_r_hand_grab;
        sc_voxel[][] _player_l_hand_grab;
        sc_voxel[][] _player_r_upper_leg;
        sc_voxel[][] _player_l_upper_leg;
        sc_voxel[][] _player_r_lower_leg;
        sc_voxel[][] _player_l_lower_leg;
        sc_voxel[][] _player_r_foot;
        sc_voxel[][] _player_l_foot;
        sc_voxel[][] _player_head;
        sc_voxel[][] _player_pelvis;
        sc_voxel[][] _player_rght_hnd;
        sc_voxel[][] _player_lft_hnd;
        sc_voxel[][] _player_torso;
        sc_voxel[][] _player_rght_shldr;
        sc_voxel[][] _player_lft_shldr;
        sc_voxel[][] _player_rght_elbow_target;
        sc_voxel[][] _player_lft_elbow_target;
        sc_voxel[][] _player_lft_lower_arm;
        sc_voxel[][] _player_rght_lower_arm;
        sc_voxel[][] _player_lft_upper_arm;
        sc_voxel[][] _player_rght_upper_arm;
        sc_voxel[][] _player_rght_elbow_target_two;
        sc_voxel[][] _player_lft_elbow_target_two;
        sc_voxel[][] _player_rght_target_knee;
        sc_voxel[][] _player_lft_target_knee;
        sc_voxel[][] _player_rght_target_two_knee;
        sc_voxel[][] _player_lft_target_two_knee;
        sc_containment_grid[][] _world_containment_grid_screen;
        sc_containment_grid[][] _world_containment_grid_list_LH;
        sc_containment_grid[][] _world_containment_grid_list_RH;
        SC_grid[][] _world_grid_list;
        SC_cube[][] _world_screen_list;
        SC_cube[][] _world_cube_list;
        SC_cube[][] _world_cone_list;
        SC_cube[][] _world_cylinder_list;
        SC_cube[][] _world_capsule_list;
        SC_cube[][] _world_sphere_list;
        sc_spectrum[][] _world_spectrum_list;
        sc_voxel[][] _world_voxel_cube_lists;
        SC_cube[][] _world_terrain_tile_list;
        SC_cube[][] _world_screen_assets_list;
        SC_cube[][] _terrain;
        SC_cube[][] _floor;

        Vector4[][] _array_of_colors;
        Vector3[][][] _array_of_last_frame_voxel_pos;
        Vector3[][][] _array_of_last_frame_cube_pos;
        Vector3[][][] _array_of_last_frame_cone_pos;
        Vector3[][][] _array_of_last_frame_cylinder_pos;
        Vector3[][][] _array_of_last_frame_capsule_pos;
        Vector3[][][] _array_of_last_frame_sphere_pos;
        Matrix[] worldMatrix_base;

        int[][][] _some_frame_counter_grab_right_hand_swtch;
        int[][][] _some_frame_counter_grab_right_hand;
        int[][][] _some_frame_counter_raycast_00;
        int[][][] _some_frame_counter_raycast_01;

        float a = 0.0f;
        float r = 0.0f;
        float g = 0.0f;
        float b = 0.0f;
        float offsetPosX = 0.0f;
        float offsetPosY = 0.0f;
        float offsetPosZ = 0.0f;

        public static Jitter.Forces.Buoyancy _buo;
        Jitter.Forces.Buoyancy[] _buoyancy_area;
        int has_water_buo_effect = -1;
        bool containsCoord;
        JVector rh_attract_force = JVector.Zero;
        JVector lh_attract_force = JVector.Zero;

        //SC_console_directx D3D;
        //IntPtr HWND;

        Matrix translationMatrix = Matrix.Identity;
        JQuaternion quatterer = new JQuaternion(0, 0, 0, 1);
        Quaternion tester = Quaternion.Identity;
        Matrix rigidbody_matrix = Matrix.Identity;
        IEnumerator enumerator;
        RigidBody body;

        //SC_DRGrid _grid;
        //main terrain.
        //SC_VR_IcoSphere _icoSphere;
        int _icoVertexCount = 0;

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

        /*public int PlanetChunkWidth_L = 96;
        public int PlanetChunkWidth_R = 95;
        public int PlanetChunkHeight_L = 96;
        public int PlanetChunkHeight_R = 95;
        public int PlanetChunkDepth_L = 96;
        public int PlanetChunkDepth_R = 95;
        public int realplanetwidth = 4;*/

        /*public int PlanetChunkWidth_L = 80;
        public int PlanetChunkWidth_R = 79;
        public int PlanetChunkHeight_L = 80;
        public int PlanetChunkHeight_R = 79;
        public int PlanetChunkDepth_L = 80;
        public int PlanetChunkDepth_R = 79;
        public int realplanetwidth = 4;*/

        /*public int PlanetChunkWidth_L = 70;
        public int PlanetChunkWidth_R = 69;
        public int PlanetChunkHeight_L = 70;
        public int PlanetChunkHeight_R = 69;
        public int PlanetChunkDepth_L = 70;
        public int PlanetChunkDepth_R = 69;
        public int realplanetwidth = 8;*/

        /*public int PlanetChunkWidth_L = 60;
        public int PlanetChunkWidth_R = 59;
        public int PlanetChunkHeight_L = 60;
        public int PlanetChunkHeight_R = 59;
        public int PlanetChunkDepth_L = 60;
        public int PlanetChunkDepth_R = 59;
        public int realplanetwidth = 4;*/


        /*public int PlanetChunkWidth_L = 48;
         public int PlanetChunkWidth_R = 47;
         public int PlanetChunkHeight_L = 48;
         public int PlanetChunkHeight_R = 47;
         public int PlanetChunkDepth_L = 48;
         public int PlanetChunkDepth_R = 47;
         public int realplanetwidth = 4;*/


        public int PlanetChunkWidth_L = 12;
        public int PlanetChunkWidth_R = 11;
        public int PlanetChunkHeight_L = 12;
        public int PlanetChunkHeight_R = 11;
        public int PlanetChunkDepth_L = 12;
        public int PlanetChunkDepth_R = 11;
        public int realplanetwidth = 4;

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

        SCCoreSystems.SC_Graphics.SC_cube.DLightBuffer[] _DLightBuffer_cube = new SC_cube.DLightBuffer[1];
        SCCoreSystems.SC_Graphics.SC_grid.DLightBuffer[] _DLightBuffer_grid = new SC_grid.DLightBuffer[1];
        SCCoreSystems.SC_Graphics.sc_containment_grid.DLightBuffer[] _DLightBuffer_containment_grid = new sc_containment_grid.DLightBuffer[1];

        SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer[] _DLightBuffer_voxel_cube = new sc_voxel.DLightBuffer[1];

        SCCoreSystems.SC_Graphics.sc_spectrum.DLightBuffer[] _DLightBuffer_spectrum = new sc_spectrum.DLightBuffer[1];

        SCCoreSystems.SC_Graphics.sc_voxel_pchunk.DLightBuffer[] _DLightBuffer_voxel_pchunk_cube = new sc_voxel_pchunk.DLightBuffer[1];

        _sc_texture_loader _pink_texture;
        _sc_texture_loader _basicTexture;
        int _start_background_worker_00 = 0;
        int _start_background_worker_01 = 0;

        public static DTerrainHeightMap Terrain;

        public sc_graphics_sec() //SC_console_directx _SC_console_directx, IntPtr _HWND
        {
            //arrayOfPixData = new int[SC_Update._desktopFrame._texture2DFinal.Description.Width * SC_Update._desktopFrame._texture2DFinal.Description.Height * 3];



            //D3D = _SC_console_directx;
            //HWND = _HWND;

            _screenDirMatrix = new Matrix[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            _screenDirMatrix_correct_pos = new Matrix[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            point3DCollection = new Vector3[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            _world_screen_list = new SC_cube[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][];
            _world_screen_assets_list = new SC_cube[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][];
            worldMatrix_instances_screens = new Matrix[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            world_last_Matrix_instances_screens = new Matrix[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            worldMatrix_instances_screen_assets = new Matrix[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];

            _world_cube_list = new SC_cube[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][];
            _world_voxel_cube_lists = new sc_voxel[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][];
            _world_cone_list = new SC_cube[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][];
            _world_cylinder_list = new SC_cube[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][];
            _world_capsule_list = new SC_cube[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][];
            _world_sphere_list = new SC_cube[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][];

            _world_terrain_tile_list = new SC_cube[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][];

            worldMatrix_instances_voxel_pchunk = new Matrix[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][][];
            worldMatrix_instances_cubes = new Matrix[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            worldMatrix_instances_voxel_cube = new Matrix[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            worldMatrix_instances_cone = new Matrix[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            worldMatrix_instances_cylinder = new Matrix[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            worldMatrix_instances_capsule = new Matrix[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            worldMatrix_instances_sphere = new Matrix[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];

            worldMatrix_instances_terrain_tiles = new Matrix[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            _array_of_colors = new Vector4[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][];
            _array_of_last_frame_cube_pos = new Vector3[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            _array_of_last_frame_voxel_pos = new Vector3[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            _array_of_last_frame_cone_pos = new Vector3[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            _array_of_last_frame_cylinder_pos = new Vector3[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            _array_of_last_frame_capsule_pos = new Vector3[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            _array_of_last_frame_sphere_pos = new Vector3[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];


            //worldMatrix_instances_voxel_pchunk = new Matrix[1];

            //SINGLE OBJECTS
            //SINGLE OBJECTS
            //SINGLE OBJECTS
            _world_containment_grid_list_RH = new sc_containment_grid[1][];
            _world_containment_grid_list_LH = new sc_containment_grid[1][];
            _world_containment_grid_screen = new sc_containment_grid[1][];

            _world_grid_list = new SC_grid[1][];
            _terrain = new SC_cube[1][];
            _floor = new SC_cube[1][];

            worldMatrix_instances_terrain = new Matrix[1][][];

            _world_spectrum_list = new sc_spectrum[1][];
            worldMatrix_instances_spectrum = new Matrix[1][][];

            worldMatrix_instances_DZgrid = new Matrix[1][][];
            worldMatrix_instances_grid = new Matrix[1][][];
            worldMatrix_instances_containment_grid_RH = new Matrix[1][][];
            worldMatrix_instances_containment_grid_LH = new Matrix[1][][];
            worldMatrix_instances_containment_grid_screen = new Matrix[1][][];
            //SINGLE OBJECTS
            //SINGLE OBJECTS
            //SINGLE OBJECTS





            //HUMAN RIG STUFF
            var tempMultiInstancePhysicsTotal = 1;
            _player_r_upper_leg = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_l_upper_leg = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_r_lower_leg = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_l_lower_leg = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_r_foot = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_l_foot = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_lft_target_knee = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_target_knee = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_lft_target_two_knee = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_target_two_knee = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_hnd = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_lft_upper_arm = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_lft_hnd = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_torso = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_pelvis = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_shldr = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_lft_shldr = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_head = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_lower_arm = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_lft_lower_arm = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_upper_arm = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_elbow_target = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_lft_elbow_target = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_rght_elbow_target_two = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_lft_elbow_target_two = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_l_hand_grab = new sc_voxel[tempMultiInstancePhysicsTotal][];
            _player_r_hand_grab = new sc_voxel[tempMultiInstancePhysicsTotal][];




            worldMatrix_instances_l_hand_grab = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_hand_grab = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_upper_leg = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_l_upper_leg = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_lower_leg = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_l_lower_leg = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_foot = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_l_foot = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_target_knee = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_l_target_knee = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_target_two_knee = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_l_target_two_knee = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_elbow_target = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_l_elbow_target = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_elbow_target_two = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_l_elbow_target_two = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_head = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_torso = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_pelvis = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_hand = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_l_hand = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_shoulder = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_l_shoulder = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_upperarm = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_l_upperarm = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_lowerarm = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_l_lowerarm = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_r_foot = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_l_foot = new Matrix[tempMultiInstancePhysicsTotal][][];
            worldMatrix_instances_floor = new Matrix[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];



            _some_frame_counter_grab_right_hand_swtch = new int[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            _some_frame_counter_grab_right_hand = new int[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            _some_frame_counter_raycast_00 = new int[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            _some_frame_counter_raycast_01 = new int[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];


            _some_frame_counter_grab_right_hand_swtch[0] = new int[Program.world_width * Program.world_height * Program.world_depth][];
            _some_frame_counter_grab_right_hand[0] = new int[Program.world_width * Program.world_height * Program.world_depth][];
            _some_frame_counter_raycast_00[0] = new int[Program.world_width * Program.world_height * Program.world_depth][];
            _some_frame_counter_raycast_01[0] = new int[Program.world_width * Program.world_height * Program.world_depth][];


            _some_frame_counter_grab_right_hand_swtch[0][0] = new int[_human_inst_rig_x * _human_inst_rig_y * _human_inst_rig_z];
            _some_frame_counter_grab_right_hand[0][0] = new int[_human_inst_rig_x * _human_inst_rig_y * _human_inst_rig_z];
            //_some_frame_counter_raycast_00[0] = new int[Program.world_width * Program.world_height * Program.world_depth][];
            //_some_frame_counter_raycast_01[0] = new int[Program.world_width * Program.world_height * Program.world_depth][];



            swtch_for_last_pos = new int[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
            swtch_for_last_pos[0] = new int[Program.world_width * Program.world_height * Program.world_depth][];
            swtch_for_last_pos[0][0] = new int[_human_inst_rig_x * _human_inst_rig_y * _human_inst_rig_z];
            swtch_for_last_pos[0][0][0] = 0;

            for (int i = 0; i < _some_frame_counter_grab_right_hand[0][0].Length; i++)
            {
                _some_frame_counter_grab_right_hand[0][0][i] = 0;
                _some_frame_counter_grab_right_hand_swtch[0][0][i] = 0;
            }


            worldMatrix_base = new Matrix[1];
            worldMatrix_base[0] = Matrix.Identity;

            DoWork(0);
        }

        public SC_message_object_jitter[][] _sc_create_world_objects(SC_message_object_jitter[][] _sc_jitter_tasks)
        {
            try
            {
                _buoyancy_area = new Jitter.Forces.Buoyancy[1];

                //draw_dcontainmentgrid = new int[6 * 6 * 6];

                for (int i = 0; i < draw_dcontainmentgrid.Length; i++)
                {
                    draw_dcontainmentgrid[i] = 0;
                }

                for (int xx = 0; xx < Program._physics_engine_instance_x; xx++)
                {
                    for (int yy = 0; yy < Program._physics_engine_instance_y; yy++)
                    {
                        for (int zz = 0; zz < Program._physics_engine_instance_z; zz++)
                        {
                            var indexer00 = xx + Program._physics_engine_instance_x * (yy + Program._physics_engine_instance_y * zz);
                            Vector3 physics_engine_offset_pos = new Vector3(xx * 2, yy * 2, zz * 2);

                            worldMatrix_instances_voxel_pchunk[indexer00] = new Matrix[Program.world_width * Program.world_height * Program.world_depth][][];


                            //World[] _jitter_worlds = (World[])_sc_jitter_tasks[indexer00]._world_data;
                            _array_of_last_frame_voxel_pos[indexer00] = new Vector3[Program.world_width * Program.world_height * Program.world_depth][];
                            _array_of_last_frame_cube_pos[indexer00] = new Vector3[Program.world_width * Program.world_height * Program.world_depth][];
                            _array_of_last_frame_cone_pos[indexer00] = new Vector3[Program.world_width * Program.world_height * Program.world_depth][];
                            _array_of_last_frame_cylinder_pos[indexer00] = new Vector3[Program.world_width * Program.world_height * Program.world_depth][];
                            _array_of_last_frame_capsule_pos[indexer00] = new Vector3[Program.world_width * Program.world_height * Program.world_depth][];
                            _array_of_last_frame_sphere_pos[indexer00] = new Vector3[Program.world_width * Program.world_height * Program.world_depth][];

                            _world_cube_list[indexer00] = new SC_cube[Program.world_width * Program.world_height * Program.world_depth];
                            _world_voxel_cube_lists[indexer00] = new sc_voxel[Program.world_width * Program.world_height * Program.world_depth];
                            _world_cone_list[indexer00] = new SC_cube[Program.world_width * Program.world_height * Program.world_depth];
                            _world_cylinder_list[indexer00] = new SC_cube[Program.world_width * Program.world_height * Program.world_depth];
                            _world_capsule_list[indexer00] = new SC_cube[Program.world_width * Program.world_height * Program.world_depth];
                            _world_sphere_list[indexer00] = new SC_cube[Program.world_width * Program.world_height * Program.world_depth];

                            worldMatrix_instances_cubes[indexer00] = new Matrix[Program.world_width * Program.world_height * Program.world_depth][];
                            worldMatrix_instances_cone[indexer00] = new Matrix[Program.world_width * Program.world_height * Program.world_depth][];
                            worldMatrix_instances_cylinder[indexer00] = new Matrix[Program.world_width * Program.world_height * Program.world_depth][];
                            worldMatrix_instances_capsule[indexer00] = new Matrix[Program.world_width * Program.world_height * Program.world_depth][];
                            worldMatrix_instances_sphere[indexer00] = new Matrix[Program.world_width * Program.world_height * Program.world_depth][];

                            worldMatrix_instances_voxel_cube[indexer00] = new Matrix[Program.world_width * Program.world_height * Program.world_depth][];

                            float offsetCubeY = 0;
                            float offsetVoxelY = 0;
                            float offsetCapsuleY = 0;
                            float offsetConeY = 0;
                            float offsetCylinderY = 0;
                            float offsetSphereY = 0;

                            try
                            {
                                for (int x = 0; x < Program.world_width; x++)
                                {
                                    for (int y = 0; y < Program.world_height; y++)
                                    {
                                        for (int z = 0; z < Program.world_depth; z++)
                                        {
                                            var indexer01 = x + Program.world_width * (y + Program.world_height * z);

                                            Vector3 world_pos_offset = new Vector3(x * 2, y * 2, z * 2);

                                            object _some_data_00 = (object)_sc_jitter_tasks[indexer00][indexer01]._world_data[0];
                                            //World _jitter_worlds = (World)_some_data_00;
                                            World _jitter_world = (World)_some_data_00;//= _jitter_worlds[0];

                                            //PHYSICS CUBES
                                            offsetCubeY = 0;
                                            r = 0.75f;
                                            g = 0.15f;
                                            b = 0;
                                            a = 1;
                                            _object_worldmatrix = Matrix.Identity;
                                            offsetPosX = _cube_size_x * 1.15f; //x between each world instance
                                            offsetPosY = _cube_size_y * 1.15f; //y between each world instance
                                            offsetPosZ = _cube_size_z * 1.15f; //z between each world instance
                                            _object_worldmatrix = WorldMatrix;
                                            _object_worldmatrix.M41 = 0.5f + x + physics_engine_offset_pos.X + world_pos_offset.X;
                                            _object_worldmatrix.M42 = 0.5f + y + physics_engine_offset_pos.Y + world_pos_offset.Y + offsetCubeY;
                                            _object_worldmatrix.M43 = 0 + z + physics_engine_offset_pos.Z + world_pos_offset.Z;
                                            _object_worldmatrix.M44 = 1;

                                            var _size_screen = 0.00045f;
                                            var _cuber = new SC_cube();
                                            var _hasinit3 = _cuber.Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0.1f, 1, 1, _cube_size_x, _cube_size_y, _cube_size_z, new Vector4(r, g, b, a), _inst_cube_x, _inst_cube_y, _inst_cube_z, SC_Update.HWND, _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, _jitter_world, SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCube, false, 1, 100, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                                            _world_cube_list[indexer00][indexer01] = _cuber;
                                            _array_of_last_frame_cube_pos[indexer00][indexer01] = new Vector3[_inst_cube_x * _inst_cube_y * _inst_cube_z];
                                            worldMatrix_instances_cubes[indexer00][indexer01] = new Matrix[_inst_cube_x * _inst_cube_y * _inst_cube_z];
                                            for (int i = 0; i < worldMatrix_instances_cubes[indexer00][indexer01].Length; i++)
                                            {
                                                Vector3 poser = new Vector3(_world_cube_list[indexer00][indexer01]._arrayOfInstances[i].current_pos.M41,
                                                                            _world_cube_list[indexer00][indexer01]._arrayOfInstances[i].current_pos.M42,
                                                                            _world_cube_list[indexer00][indexer01]._arrayOfInstances[i].current_pos.M43);

                                                _array_of_last_frame_cube_pos[indexer00][indexer01][i] = poser;
                                                worldMatrix_instances_cubes[indexer00][indexer01][i] = _world_cube_list[indexer00][indexer01]._arrayOfInstances[i].current_pos;
                                            }

                                            offsetConeY = 10;
                                            //PHYSICS CONES
                                            r = 0.75f;
                                            g = 0.15f;
                                            b = 0;
                                            a = 1;
                                            _object_worldmatrix = Matrix.Identity;
                                            offsetPosX = _cube_size_x * 1.15f; //x between each world instance
                                            offsetPosY = _cube_size_y * 1.15f; //y between each world instance
                                            offsetPosZ = _cube_size_z * 1.15f; //z between each world instance
                                            _object_worldmatrix = WorldMatrix;
                                            _object_worldmatrix.M41 = 1.5f + x + physics_engine_offset_pos.X + world_pos_offset.X;
                                            _object_worldmatrix.M42 = 3 + y + physics_engine_offset_pos.Y + world_pos_offset.Y + offsetConeY;
                                            _object_worldmatrix.M43 = -0.5f + z + physics_engine_offset_pos.Z + world_pos_offset.Z;
                                            _object_worldmatrix.M44 = 1;
                                            _size_screen = 0.00045f;
                                            _cuber = new SC_cube();
                                            _hasinit3 = _cuber.Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0.1f, 1, 1, _cube_size_x, _cube_size_y, _cube_size_z, new Vector4(r, g, b, a), _inst_other_x, _inst_other_y, _inst_other_z, SC_Update.HWND, _object_worldmatrix, 6, offsetPosX, offsetPosY, offsetPosZ, _jitter_world, SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCone, false, 1, 100, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                                            _world_cone_list[indexer00][indexer01] = _cuber;

                                            _array_of_last_frame_cone_pos[indexer00][indexer01] = new Vector3[_inst_other_x * _inst_other_y * _inst_other_z];

                                            worldMatrix_instances_cone[indexer00][indexer01] = new Matrix[_inst_other_x * _inst_other_y * _inst_other_z];
                                            for (int i = 0; i < worldMatrix_instances_cone[indexer00][indexer01].Length; i++)
                                            {
                                                Vector3 poser = new Vector3(_world_cone_list[indexer00][indexer01]._arrayOfInstances[i].current_pos.M41,
                                                                            _world_cone_list[indexer00][indexer01]._arrayOfInstances[i].current_pos.M42,
                                                                            _world_cone_list[indexer00][indexer01]._arrayOfInstances[i].current_pos.M43);

                                                _array_of_last_frame_cone_pos[indexer00][indexer01][i] = poser;
                                                worldMatrix_instances_cone[indexer00][indexer01][i] = _world_cone_list[indexer00][indexer01]._arrayOfInstances[i].current_pos;
                                            }


                                            offsetCylinderY = 20;
                                            //PHYSICS CYLINDERS
                                            r = 0.75f;
                                            g = 0.15f;
                                            b = 0;
                                            a = 1;
                                            _object_worldmatrix = Matrix.Identity;
                                            offsetPosX = _cube_size_x * 1.15f; //x between each world instance
                                            offsetPosY = _cube_size_y * 1.15f; //y between each world instance
                                            offsetPosZ = _cube_size_z * 1.15f; //z between each world instance
                                            _object_worldmatrix = WorldMatrix;
                                            _object_worldmatrix.M41 = 1.5f + x + physics_engine_offset_pos.X + world_pos_offset.X;
                                            _object_worldmatrix.M42 = 3 + y + physics_engine_offset_pos.Y + world_pos_offset.Y + offsetCylinderY;
                                            _object_worldmatrix.M43 = -0.5f + z + physics_engine_offset_pos.Z + world_pos_offset.Z;
                                            _object_worldmatrix.M44 = 1;
                                            _size_screen = 0.00045f;
                                            _cuber = new SC_cube();
                                            _hasinit3 = _cuber.Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0.1f, 1, 1, _cube_size_x, _cube_size_y, _cube_size_z, new Vector4(r, g, b, a), _inst_other_x, _inst_other_y, _inst_other_z, SC_Update.HWND, _object_worldmatrix, 7, offsetPosX, offsetPosY, offsetPosZ, _jitter_world, SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCylinder, false, 1, 100, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                                            _world_cylinder_list[indexer00][indexer01] = _cuber;

                                            _array_of_last_frame_cylinder_pos[indexer00][indexer01] = new Vector3[_inst_other_x * _inst_other_y * _inst_other_z];

                                            worldMatrix_instances_cylinder[indexer00][indexer01] = new Matrix[_inst_other_x * _inst_other_y * _inst_other_z];
                                            for (int i = 0; i < worldMatrix_instances_cylinder[indexer00][indexer01].Length; i++)
                                            {
                                                Vector3 poser = new Vector3(_world_cylinder_list[indexer00][indexer01]._arrayOfInstances[i].current_pos.M41,
                                                                            _world_cylinder_list[indexer00][indexer01]._arrayOfInstances[i].current_pos.M42,
                                                                            _world_cylinder_list[indexer00][indexer01]._arrayOfInstances[i].current_pos.M43);

                                                _array_of_last_frame_cylinder_pos[indexer00][indexer01][i] = poser;
                                                worldMatrix_instances_cylinder[indexer00][indexer01][i] = _world_cylinder_list[indexer00][indexer01]._arrayOfInstances[i].current_pos;
                                            }


                                            offsetCapsuleY = 30;
                                            //PHYSICS CAPSULES
                                            r = 0.75f;
                                            g = 0.15f;
                                            b = 0;
                                            a = 1;
                                            _object_worldmatrix = Matrix.Identity;
                                            offsetPosX = _cube_size_x * 1.15f; //x between each world instance
                                            offsetPosY = _cube_size_y * 1.15f; //y between each world instance
                                            offsetPosZ = _cube_size_z * 1.15f; //z between each world instance
                                            _object_worldmatrix = WorldMatrix;
                                            _object_worldmatrix.M41 = 1.5f + x + physics_engine_offset_pos.X + world_pos_offset.X;
                                            _object_worldmatrix.M42 = 3 + y + physics_engine_offset_pos.Y + world_pos_offset.Y + offsetCapsuleY;
                                            _object_worldmatrix.M43 = -0.5f + z + physics_engine_offset_pos.Z + world_pos_offset.Z;
                                            _object_worldmatrix.M44 = 1;
                                            _size_screen = 0.00045f;
                                            _cuber = new SC_cube();
                                            _hasinit3 = _cuber.Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0.1f, 1, 1, _cube_size_x, _cube_size_y, _cube_size_z, new Vector4(r, g, b, a), _inst_other_x, _inst_other_y, _inst_other_z, SC_Update.HWND, _object_worldmatrix, 8, offsetPosX, offsetPosY, offsetPosZ, _jitter_world, SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCapsule, false, 1, 100, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                                            _world_capsule_list[indexer00][indexer01] = _cuber;

                                            _array_of_last_frame_capsule_pos[indexer00][indexer01] = new Vector3[_inst_other_x * _inst_other_y * _inst_other_z];

                                            worldMatrix_instances_capsule[indexer00][indexer01] = new Matrix[_inst_other_x * _inst_other_y * _inst_other_z];
                                            for (int i = 0; i < worldMatrix_instances_capsule[indexer00][indexer01].Length; i++)
                                            {
                                                Vector3 poser = new Vector3(_world_capsule_list[indexer00][indexer01]._arrayOfInstances[i].current_pos.M41,
                                                                            _world_capsule_list[indexer00][indexer01]._arrayOfInstances[i].current_pos.M42,
                                                                            _world_capsule_list[indexer00][indexer01]._arrayOfInstances[i].current_pos.M43);

                                                _array_of_last_frame_capsule_pos[indexer00][indexer01][i] = poser;
                                                worldMatrix_instances_capsule[indexer00][indexer01][i] = _world_capsule_list[indexer00][indexer01]._arrayOfInstances[i].current_pos;
                                            }



                                            offsetSphereY = 40;


                                            //PHYSICS SPHERES
                                            r = 0.75f;
                                            g = 0.15f;
                                            b = 0;
                                            a = 1;
                                            _object_worldmatrix = Matrix.Identity;
                                            offsetPosX = _cube_size_x * 1.15f; //x between each world instance
                                            offsetPosY = _cube_size_y * 1.15f; //y between each world instance
                                            offsetPosZ = _cube_size_z * 1.15f; //z between each world instance
                                            _object_worldmatrix = WorldMatrix;
                                            _object_worldmatrix.M41 = 1.5f + x + physics_engine_offset_pos.X + world_pos_offset.X;
                                            _object_worldmatrix.M42 = 3 + y + physics_engine_offset_pos.Y + world_pos_offset.Y + offsetSphereY;
                                            _object_worldmatrix.M43 = -0.5f + z + physics_engine_offset_pos.Z + world_pos_offset.Z;
                                            _object_worldmatrix.M44 = 1;
                                            _size_screen = 0.00045f;
                                            _cuber = new SC_cube();
                                            _hasinit3 = _cuber.Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0.1f, 1, 1, _cube_size_x, _cube_size_y, _cube_size_z, new Vector4(r, g, b, a), _inst_other_x, _inst_other_y, _inst_other_z, SC_Update.HWND, _object_worldmatrix, 1, offsetPosX, offsetPosY, offsetPosZ, _jitter_world, SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedSphere, false, 1, 100, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                                            _world_sphere_list[indexer00][indexer01] = _cuber;

                                            _array_of_last_frame_sphere_pos[indexer00][indexer01] = new Vector3[_inst_other_x * _inst_other_y * _inst_other_z];

                                            worldMatrix_instances_sphere[indexer00][indexer01] = new Matrix[_inst_other_x * _inst_other_y * _inst_other_z];
                                            for (int i = 0; i < worldMatrix_instances_sphere[indexer00][indexer01].Length; i++)
                                            {
                                                Vector3 poser = new Vector3(_world_sphere_list[indexer00][indexer01]._arrayOfInstances[i].current_pos.M41,
                                                                            _world_sphere_list[indexer00][indexer01]._arrayOfInstances[i].current_pos.M42,
                                                                            _world_sphere_list[indexer00][indexer01]._arrayOfInstances[i].current_pos.M43);

                                                _array_of_last_frame_sphere_pos[indexer00][indexer01][i] = poser;
                                                worldMatrix_instances_sphere[indexer00][indexer01][i] = _world_sphere_list[indexer00][indexer01]._arrayOfInstances[i].current_pos;
                                            }



                                            offsetVoxelY = 40;
                                            //VOXELS
                                            r = 0.95f; //0.75f
                                            g = 0.95f; //0.75f
                                            b = 0.95f; //0.75f
                                            a = 1;
                                            _object_worldmatrix = Matrix.Identity;
                                            offsetPosX = _voxel_cube_size_x * (1.15f); //x between each world instance
                                            offsetPosY = _voxel_cube_size_y * (1.15f); //y between each world instance
                                            offsetPosZ = _voxel_cube_size_z * (1.15f); //z between each world instance
                                            //_offsetPos = new Vector3(0, 0, 0);
                                            _object_worldmatrix = WorldMatrix;
                                            _object_worldmatrix.M41 = 0 + x + physics_engine_offset_pos.X + world_pos_offset.X;
                                            _object_worldmatrix.M42 = 3 + y + physics_engine_offset_pos.Y + world_pos_offset.Y + offsetVoxelY;
                                            _object_worldmatrix.M43 = 0 + z + physics_engine_offset_pos.Z + world_pos_offset.Z;
                                            _object_worldmatrix.M44 = 1;
                                            var sc_voxel_spheroid = new sc_voxel();
                                            voxel_general_size = 0.00075f; //0.0015f
                                            voxel_type = 1;
                                            is_static = false;
                                            _voxel_mass = 100;
                                            var _hasinit00 = sc_voxel_spheroid.Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1,
                                                _voxel_cube_size_x, _voxel_cube_size_y, _voxel_cube_size_z, new Vector4(r, g, b, a), _inst_voxel_cube_x, _inst_voxel_cube_y, _inst_voxel_cube_z, SC_Update.HWND,
                                                _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, _jitter_world, _voxel_mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.sc_perko_voxel,
                                                9, 9, 9, 9, 9, 9, 9, 9, 9, 35, 34, 40, 59, 23, 22,
                                                voxel_general_size, Vector3.Zero, 17, 0, 0, 0, 2, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                                                                                                               //9, 9, 9, 9, 9, 9, 20, 19, 20, 19, 20, 19
                                                                                                               //9, 9, 9, 9, 9, 9, 35, 34, 40, 59, 20, 19, 
                                                                                                               //FOR CUBES AND SET TO voxel_type = 1                  
                                                                                                               //var _hasinit00 = sc_voxel_spheroid.Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, _voxel_cube_size_x, _voxel_cube_size_y, _voxel_cube_size_z, new Vector4(r, g, b, a), _inst_voxel_cube_x, _inst_voxel_cube_y, _inst_voxel_cube_z, Hwnd, _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, _voxel_mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag._voxel_spheroid, 2, 2, 2, 2, 2, 2, 20, 19, 20, 19, 20, 19, voxel_general_size, Vector3.Zero, 250, 0, 0, 0, 2, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f                                  
                                                                                                               //FOR CUBES AND SET TO voxel_type = 1

                                            _array_of_last_frame_voxel_pos[indexer00][indexer01] = new Vector3[_inst_voxel_cube_x * _inst_voxel_cube_y * _inst_voxel_cube_z];

                                            _world_voxel_cube_lists[indexer00][indexer01] = sc_voxel_spheroid;
                                            worldMatrix_instances_voxel_cube[indexer00][indexer01] = new Matrix[_inst_voxel_cube_x * _inst_voxel_cube_y * _inst_voxel_cube_z];
                                            for (int i = 0; i < worldMatrix_instances_voxel_cube[indexer00][indexer01].Length; i++)
                                            {
                                                _array_of_last_frame_voxel_pos[indexer00][indexer01][i] = Vector3.Zero;
                                                worldMatrix_instances_voxel_cube[indexer00][indexer01][i] = Matrix.Identity;
                                            }















                                            offsetVoxelY = 40;

                                            //VOXELS
                                            r = 0.95f; //0.75f
                                            g = 0.95f; //0.75f
                                            b = 0.95f; //0.75f
                                            a = 1;

                                            _object_worldmatrix = Matrix.Identity;

                                            /*offsetPosX = _voxel_cube_size_x * (1.15f); //x between each world instance
                                            offsetPosY = _voxel_cube_size_y * (1.15f); //y between each world instance
                                            offsetPosZ = _voxel_cube_size_z * (1.15f); //z between each world instance

                                            //_offsetPos = new Vector3(0, 0, 0);

                                            _object_worldmatrix = Matrix.Identity;
                                            _object_worldmatrix.M41 = 0 + x + physics_engine_offset_pos.X + world_pos_offset.X;
                                            _object_worldmatrix.M42 = 3 + y + physics_engine_offset_pos.Y + world_pos_offset.Y + offsetVoxelY;
                                            _object_worldmatrix.M43 = 0 + z + physics_engine_offset_pos.Z + world_pos_offset.Z;
                                            _object_worldmatrix.M44 = 1;*/




                                            world_pos_offset.X = 0;
                                            world_pos_offset.Y = 10;
                                            world_pos_offset.Z = 0;

                                            //////////////TO READD
                                            //////////////TO READD
                                            //////////////TO READD
                                            var _sccsproceduralplanetbuilder = new sccsproceduralplanetbuilder();
                                            arrayOfPlanetChunk = _sccsproceduralplanetbuilder.buildplanetchunk(WorldMatrix, physics_engine_offset_pos, world_pos_offset, _jitter_world, SC_Update.HWND);

                                            //worldMatrix_instances_voxel_pchunk[1] = Matrix.Identity;

                                            //voxel_general_size = 0.00075f; //0.0015f
                                            //voxel_type = 1;
                                            //is_static = false;
                                            //_voxel_mass = 100;
                                            //var _hasinit00 = sc_voxel_spheroid.Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1,
                                            ///    _voxel_cube_size_x, _voxel_cube_size_y, _voxel_cube_size_z, new Vector4(r, g, b, a), _inst_voxel_cube_x, _inst_voxel_cube_y, _inst_voxel_cube_z, SC_Update.HWND,
                                            //    _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, _jitter_world, _voxel_mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.sc_perko_voxel,
                                            //    9, 9, 9, 9, 9, 9, 9, 9, 9, 35, 34, 40, 59, 23, 22,
                                            //    voxel_general_size, Vector3.Zero, 17, 0, 0, 0, 2, voxel_type);


                                            worldMatrix_instances_voxel_pchunk[indexer00][indexer01] = new Matrix[arrayOfPlanetChunk.Length][];



                                            //for (int pc = 0; pc < arrayOfPlanetChunk.Length; pc++)
                                            //{
                                            //    if (arrayOfPlanetChunk[pc] != null)
                                            //    {
                                            //        if (arrayOfPlanetChunk[pc].Vertices != null)
                                            //        {
                                            //            if (arrayOfPlanetChunk[pc].Vertices.Length > 0)
                                            //            {
                                            //                worldMatrix_instances_voxel_pchunk[indexer00][indexer01][pc] = new Matrix[1];
                                            //                worldMatrix_instances_voxel_pchunk[indexer00][indexer01][pc][0] = arrayOfPlanetChunk[pc].current_pos;
                                            //            }
                                            //        }
                                            //    }
                                            //}


                                            for (int yc = -PlanetChunkHeight_L; yc <= PlanetChunkHeight_R; yc += realplanetwidth)
                                            {
                                                for (int xc = -PlanetChunkWidth_L; xc <= PlanetChunkWidth_R; xc += realplanetwidth)
                                                {
                                                    for (int zc = -PlanetChunkDepth_L; zc <= PlanetChunkDepth_R; zc += realplanetwidth)
                                                    {
                                                        var xxc = xc;
                                                        var yyc = yc;
                                                        var zzc = zc;

                                                        if (xxc < 0)
                                                        {
                                                            xxc *= -1;
                                                            xxc = (PlanetChunkWidth_R) + xxc;
                                                        }
                                                        if (yyc < 0)
                                                        {
                                                            yyc *= -1;
                                                            yyc = (PlanetChunkHeight_R) + yyc;
                                                        }
                                                        if (zzc < 0)
                                                        {
                                                            zzc *= -1;
                                                            zzc = (PlanetChunkDepth_R) + zzc;
                                                        }

                                                        int _index = xxc + (PlanetChunkWidth_L + PlanetChunkWidth_R + 1) * (yyc + (PlanetChunkHeight_L + PlanetChunkHeight_R + 1) * zzc);

                                                        worldMatrix_instances_voxel_pchunk[indexer00][indexer01][_index] = new Matrix[1];
                                                        worldMatrix_instances_voxel_pchunk[indexer00][indexer01][_index][0] = arrayOfPlanetChunk[_index].current_pos;
                                                    }
                                                }
                                            }
                                            //////////////TO READD
                                            //////////////TO READD
                                            //////////////TO READD







                                        }
                                    }
                                }
                                //END OF LOOP FOR WORLDS
                            }
                            catch
                            {

                            }
                        }
                    }
                }

                //SETTING UP SINGLE WORLD OBJECTS
                //END OF LOOP FOR PHYSICS ENGINE INSTANCES
                object _some_data_0 = (object)_sc_jitter_tasks[0][0]._world_data[0];
                //World[] _jitter_worlds0 = (World[])_some_data_0;
                World _thejitter_world = (World)_some_data_0;

                r = 0.10f;
                g = 0.10f;
                b = 0.10f;
                a = 1.0f;


                //////////BOTTOM FLOOR//////
                ////////////////////////////
                _inst_terrain_tile_x = 1;
                _inst_terrain_tile_y = 1;
                _inst_terrain_tile_z = 1;
                r = 0.10f;
                g = 0.10f;
                b = 0.10f;
                a = 1.0f;
                _object_worldmatrix = Matrix.Identity;
                _object_worldmatrix = WorldMatrix;
                _object_worldmatrix.M41 = 0;
                _object_worldmatrix.M42 = (-_floor_size_y) - _platform_size_y - 3;
                _object_worldmatrix.M43 = 0;
                _object_worldmatrix.M44 = 1;
                offsetPosX = 0;
                offsetPosY = 0;
                offsetPosZ = 0;
                _floor[0] = new SC_cube[1];
                _floor[0][0] = new SC_cube();
                _floor[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0.05f, 1, 1, _floor_size_x, (_floor_size_y), _floor_size_z, new Vector4(r, g, b, a), _inst_terrain_tile_x, _inst_terrain_tile_y, _inst_terrain_tile_z, SC_Update.HWND, _object_worldmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, SCCoreSystems.sc_console.SC_console_directx.BodyTag._floor, true, 0, 10, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                worldMatrix_instances_floor[0] = new Matrix[1][]; //Program.world_width * Program.world_height * Program.world_depth
                worldMatrix_instances_floor[0][0] = new Matrix[1]; //_inst_terrain_tile_x * _inst_terrain_tile_y * _inst_terrain_tile_z
                for (int i = 0; i < worldMatrix_instances_floor[0][0].Length; i++)
                {
                    worldMatrix_instances_floor[0][0][i] = _floor[0][0]._arrayOfInstances[0]._POSITION;
                }
                ////////////////////////////
                //////////BOTTOM FLOOR//////
                ////////////////////////////








                //////////SPECTRUM//////////
                ////////////////////////////
                r = 0.10f;
                g = 0.10f;
                b = 0.10f;
                a = 1.0f;
                /*
                _object_worldmatrix = Matrix.Identity;

                _object_worldmatrix = WorldMatrix;

                _object_worldmatrix.M41 = 0;
                _object_worldmatrix.M42 = 0;
                _object_worldmatrix.M43 = 0;
                _object_worldmatrix.M44 = 1;

                offsetPosX = 0;
                offsetPosY = 0;
                offsetPosZ = 0;*/
                _object_worldmatrix = Matrix.Identity;
                offsetPosX = _spectrum_size_x * 1.15f; //x between each world instance
                offsetPosY = _spectrum_size_y * 1.15f; //y between each world instance
                offsetPosZ = _spectrum_size_z * 1.15f; //z between each world instance
                _object_worldmatrix = WorldMatrix;
                _object_worldmatrix.M41 = 0;// + 0 + physics_engine_offset_pos.X + world_pos_offset.X;
                _object_worldmatrix.M42 = 0.5f;// + 0 + physics_engine_offset_pos.Y + world_pos_offset.Y + offsetCubeY;
                _object_worldmatrix.M43 = 0;// + 0 + physics_engine_offset_pos.Z + world_pos_offset.Z;
                _object_worldmatrix.M44 = 1;


                _world_spectrum_list[0] = new sc_spectrum[1];
                _world_spectrum_list[0][0] = new sc_spectrum();
                _world_spectrum_list[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0.05f, 1, 1, _spectrum_size_x, _spectrum_size_y, _spectrum_size_z, new Vector4(r, g, b, a), _inst_spectrum_x, _inst_spectrum_y, _inst_spectrum_z, SC_Update.HWND, _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, SCCoreSystems.sc_console.SC_console_directx.BodyTag._spectrum, true, 0, 10, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_spectrum[0] = new Matrix[1][]; //Program.world_width * Program.world_height * Program.world_depth
                worldMatrix_instances_spectrum[0][0] = new Matrix[_inst_spectrum_x * _inst_spectrum_y * _inst_spectrum_z]; //_inst_terrain_tile_x * _inst_terrain_tile_y * _inst_terrain_tile_z

                for (int i = 0; i < worldMatrix_instances_spectrum[0][0].Length; i++)
                {
                    worldMatrix_instances_spectrum[0][0][i] = _world_spectrum_list[0][0]._arrayOfInstances[i]._POSITION;
                }
                _world_spectrum_list[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_spectrum[0][0];

                ////////////////////////////
                //////////SPECTRUM//////////
                ////////////////////////////












                ////////////////////////////
                //////////PLATFORM//////////
                ////////////////////////////
                _inst_terrain_tile_x = 1;
                _inst_terrain_tile_y = 1;
                _inst_terrain_tile_z = 1;
                r = 0.10f;
                g = 0.10f;
                b = 0.10f;
                a = 1.0f;
                _object_worldmatrix = Matrix.Identity;
                _object_worldmatrix = WorldMatrix;
                _object_worldmatrix.M41 = 0;
                _object_worldmatrix.M42 = -_platform_size_y;
                _object_worldmatrix.M43 = 0;
                _object_worldmatrix.M44 = 1;
                offsetPosX = 0;
                offsetPosY = 0;
                offsetPosZ = 0;

                _terrain[0] = new SC_cube[1];
                _terrain[0][0] = new SC_cube();
                _terrain[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0.1f, 1, 1, _platform_size_x, _platform_size_y, _platform_size_z, new Vector4(r, g, b, a), _inst_terrain_tile_x, _inst_terrain_tile_y, _inst_terrain_tile_z, SC_Update.HWND, _object_worldmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, SCCoreSystems.sc_console.SC_console_directx.BodyTag.Terrain, true, 0, 10, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_terrain[0] = new Matrix[1][];
                worldMatrix_instances_terrain[0][0] = new Matrix[1];
                for (int i = 0; i < worldMatrix_instances_terrain[0][0].Length; i++)
                {
                    worldMatrix_instances_terrain[0][0][i] = _terrain[0][0]._arrayOfInstances[0]._POSITION;
                }
                ////////////////////////////
                //////////PLATFORM//////////
                ////////////////////////////




                ////////////////////////////
                //////////GRIDS//////////
                ////////////////////////////
                r = 0.85f;
                g = 0.85f;
                b = 0.85f;
                a = 1;
                _object_worldmatrix = Matrix.Identity;
                //offsetPosX = _grid_size_x * 1.15f; //x between each world instance
                //offsetPosY = _grid_size_y * 1.15f; //y between each world instance
                //offsetPosZ = _grid_size_z * 1.15f; //z between each world instance
                _object_worldmatrix = WorldMatrix;
                _object_worldmatrix.M41 = 0;
                _size_screen = 0.30f;
                _object_worldmatrix.M42 = _terrain[0][0]._arrayOfInstances[0]._POSITION.M42 + (_terrain[0][0]._total_torso_height) + (1 * 0.001f); //_terrain_size_y + (_terrain_size_y * 0.501f)-5 //_terrain[0][0]._arrayOfInstances[0]._POSITION.M42
                _object_worldmatrix.M43 = 0;
                _object_worldmatrix.M44 = 1;


                _world_grid_list[0] = new SC_grid[1];
                _world_grid_list[0][0] = new SC_grid();
                _world_grid_list[0][0].Initialize(SC_console_directx.D3D, 10, 10, _size_screen, 10, 10, _grid_size_x, _grid_size_y, _grid_size_z, new Vector4(r, g, b, a), _inst_terrain_tile_x, _inst_terrain_tile_y, _inst_terrain_tile_z, SC_Update.HWND, _object_worldmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, SCCoreSystems.sc_console.SC_console_directx.BodyTag.sc_grid, true, 0, 10, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_grid[0] = new Matrix[1][];
                worldMatrix_instances_grid[0][0] = new Matrix[1];
                for (int i = 0; i < worldMatrix_instances_grid[0][0].Length; i++)
                {
                    worldMatrix_instances_grid[0][0][i] = _world_grid_list[0][0]._arrayOfInstances[i].current_pos;
                }
                ////////////////////////////
                //////////PLATFORM//////////
                ////////////////////////////




















                ////////////////////////////
                //////////BUOYANCY//////////
                ////////////////////////////
                _buo = new Jitter.Forces.Buoyancy(_thejitter_world);
                float _size__neg_x = 1.175494351F - 38;
                float _size__pos_x = 3.402823466F + 38;

                float _size__neg_y = 1.175494351F - 38;
                float _size__pos_y = -0.5f;

                float _size__neg_z = 1.175494351F - 38;
                float _size__pos_z = 3.402823466F + 38;

                JVector _min = new JVector(_size__neg_x, _size__neg_y, _size__neg_z);
                JVector _max = new JVector(_size__pos_x, _size__pos_y, _size__pos_z);

                JBBox _box = new JBBox(_min, _max);
                //_box.Min = new JVector(_size__neg_x, _size__neg_y, _size__neg_z);

                _box.AddPoint(new JVector(_size__neg_x, _size__neg_y, _size__neg_z));
                _box.AddPoint(new JVector(_size__pos_x, _size__neg_y, _size__neg_z));
                _box.AddPoint(new JVector(_size__neg_x, _size__neg_y, _size__pos_z));
                _box.AddPoint(new JVector(_size__pos_x, _size__neg_y, _size__pos_z));

                _box.AddPoint(new JVector(_size__neg_x, _size__pos_y, _size__neg_z));
                _box.AddPoint(new JVector(_size__pos_x, _size__pos_y, _size__neg_z));
                _box.AddPoint(new JVector(_size__neg_x, _size__pos_y, _size__pos_z));
                _box.AddPoint(new JVector(_size__pos_x, _size__pos_y, _size__pos_z));

                _buo.FluidBox = _box;

                //_buo.UseOwnFluidArea
                //Action _action = new Action();
                //JVector _new_vec = new JVector(0,0,0);
                //var refreshDXEngineAction = new Action(() =>
                //{
                //    _set_fluid_point(ref _new_vec);
                //});

                Jitter.Forces.Buoyancy.DefineFluidArea test = new Jitter.Forces.Buoyancy.DefineFluidArea(ref _set_fluid_point);
                _buo.UseOwnFluidArea(test);
                _buoyancy_area[0] = _buo;


                //_buo.FluidBox = JBBox.LargeBox;
                _buo.Density = 2.0f;
                _buo.Damping = 0.75f;
                ////////////////////////////
                //////////BUOYANCY//////////
                ////////////////////////////

                for (int phys = 0; phys < Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z; phys++)
                {
                    for (int i = 0; i < Program.world_width * Program.world_height * Program.world_depth; i++)
                    {
                        object _some_dator = (object)_sc_jitter_tasks[phys][i]._world_data[0];
                        World _the_current_world = (World)_some_dator;

                        _the_current_world.AddBody(_terrain[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_floor[0][0]._arrayOfInstances[0].transform.Component.rigidbody);

                        for (int cu = 0; cu < _world_cube_list[phys][i]._arrayOfInstances.Length; cu++)
                        {
                            _buo.Add(_world_cube_list[phys][i]._arrayOfInstances[cu].transform.Component.rigidbody, 3);
                        }

                        for (int cu = 0; cu < _world_cone_list[phys][i]._arrayOfInstances.Length; cu++)
                        {
                            _buo.Add(_world_cone_list[phys][i]._arrayOfInstances[cu].transform.Component.rigidbody, 3);
                        }

                        for (int cu = 0; cu < _world_cylinder_list[phys][i]._arrayOfInstances.Length; cu++)
                        {
                            _buo.Add(_world_cylinder_list[phys][i]._arrayOfInstances[cu].transform.Component.rigidbody, 3);
                        }

                        for (int cu = 0; cu < _world_capsule_list[phys][i]._arrayOfInstances.Length; cu++)
                        {
                            _buo.Add(_world_capsule_list[phys][i]._arrayOfInstances[cu].transform.Component.rigidbody, 3);
                        }

                        for (int cu = 0; cu < _world_sphere_list[phys][i]._arrayOfInstances.Length; cu++)
                        {
                            _buo.Add(_world_sphere_list[phys][i]._arrayOfInstances[cu].transform.Component.rigidbody, 3);
                        }

                        for (int cu = 0; cu < _world_voxel_cube_lists[phys][i]._arrayOfInstances.Length; cu++)
                        {
                            _buo.Add(_world_voxel_cube_lists[phys][i]._arrayOfInstances[cu].transform.Component.rigidbody, 3);
                        }
                    }
                }

                //OBJECTS CREATION
                //OBJECTS CREATION
                //OBJECTS CREATION
                //OBJECTS CREATION

                //HUMAN PHYSICS RIG

                //UPPER BODY
                _player_l_hand_grab[0] = new sc_voxel[1];
                worldMatrix_instances_l_hand_grab[0] = new Matrix[1][];
                _player_r_hand_grab[0] = new sc_voxel[1];
                worldMatrix_instances_r_hand_grab[0] = new Matrix[1][];
                _player_rght_hnd[0] = new sc_voxel[1];
                worldMatrix_instances_r_hand[0] = new Matrix[1][];
                _player_lft_hnd[0] = new sc_voxel[1];
                worldMatrix_instances_l_hand[0] = new Matrix[1][];
                _player_torso[0] = new sc_voxel[1];
                worldMatrix_instances_torso[0] = new Matrix[1][];
                _player_pelvis[0] = new sc_voxel[1];
                worldMatrix_instances_pelvis[0] = new Matrix[1][];
                _player_rght_shldr[0] = new sc_voxel[1];
                worldMatrix_instances_r_shoulder[0] = new Matrix[1][];
                _player_lft_shldr[0] = new sc_voxel[1];
                worldMatrix_instances_l_shoulder[0] = new Matrix[1][];
                _player_head[0] = new sc_voxel[1];
                worldMatrix_instances_head[0] = new Matrix[1][];
                _player_lft_lower_arm[0] = new sc_voxel[1];
                worldMatrix_instances_l_lowerarm[0] = new Matrix[1][];
                _player_lft_upper_arm[0] = new sc_voxel[1];
                worldMatrix_instances_l_upperarm[0] = new Matrix[1][];
                _player_rght_lower_arm[0] = new sc_voxel[1];
                worldMatrix_instances_r_lowerarm[0] = new Matrix[1][];
                _player_rght_upper_arm[0] = new sc_voxel[1];
                worldMatrix_instances_r_upperarm[0] = new Matrix[1][];
                _player_rght_elbow_target[0] = new sc_voxel[1];
                worldMatrix_instances_r_elbow_target[0] = new Matrix[1][];
                _player_lft_elbow_target[0] = new sc_voxel[1];
                worldMatrix_instances_l_elbow_target[0] = new Matrix[1][];
                _player_rght_elbow_target_two[0] = new sc_voxel[1];
                worldMatrix_instances_r_elbow_target_two[0] = new Matrix[1][];
                _player_lft_elbow_target_two[0] = new sc_voxel[1];
                worldMatrix_instances_l_elbow_target_two[0] = new Matrix[1][];



                //LOWER BODY
                _player_l_lower_leg[0] = new sc_voxel[1];
                worldMatrix_instances_l_lower_leg[0] = new Matrix[1][];
                _player_r_lower_leg[0] = new sc_voxel[1];
                worldMatrix_instances_r_lower_leg[0] = new Matrix[1][];
                _player_l_upper_leg[0] = new sc_voxel[1];
                worldMatrix_instances_l_upper_leg[0] = new Matrix[1][];
                _player_r_upper_leg[0] = new sc_voxel[1];
                worldMatrix_instances_r_upper_leg[0] = new Matrix[1][];
                _player_r_foot[0] = new sc_voxel[1];
                worldMatrix_instances_r_foot[0] = new Matrix[1][];
                _player_l_foot[0] = new sc_voxel[1];
                worldMatrix_instances_l_foot[0] = new Matrix[1][];
                _player_rght_target_knee[0] = new sc_voxel[1];
                worldMatrix_instances_r_target_knee[0] = new Matrix[1][];
                _player_lft_target_knee[0] = new sc_voxel[1];
                worldMatrix_instances_l_target_knee[0] = new Matrix[1][];
                _player_rght_target_two_knee[0] = new sc_voxel[1];
                worldMatrix_instances_r_target_two_knee[0] = new Matrix[1][];
                _player_lft_target_two_knee[0] = new sc_voxel[1];
                worldMatrix_instances_l_target_two_knee[0] = new Matrix[1][];









                tempIndex = 0;

                float vertoffsetx = 0;
                float vertoffsety = 0;
                float vertoffsetz = -(16 + 15) * 0.015f;// - 0.25f;;

                float _dist_between = 0.30f;

                ///////////////////////////////
                ///////////HUMAN RIG///////////
                ///////////////////////////////


                float somevoxelsize = 0.0025f; //0.0025f


                //PLAYER RIGHT HAND GRAB
                voxel_type = 1;
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                //instX = 1;
                //instY = 1;
                //instZ = 1;
                Matrix _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = 0;
                _tempMatroxer.M42 = 0;
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                var _mass = 100;
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
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;
                is_static = true;
                _player_r_hand_grab[0][0] = new sc_voxel();
                _player_r_hand_grab[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightHandGrabTarget, 3, 3, 3, 3, 3, 3, 3, 3, 3, 13, 12, 13, 12, 13, 12, voxel_general_size, new Vector3(0, 0, -0.1f), 30, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                worldMatrix_instances_r_hand_grab[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
                for (int i = 0; i < worldMatrix_instances_r_hand_grab[0][0].Length; i++)
                {
                    worldMatrix_instances_r_hand_grab[0][0][i] = Matrix.Identity;
                }





                //PLAYER LEFT HAND GRAB
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                //instX = 1;
                //instY = 1;
                //instZ = 1;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
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
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;
                is_static = true;
                _player_l_hand_grab[0][0] = new sc_voxel();
                _player_l_hand_grab[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLeftHandGrabTarget, 3, 3, 3, 3, 3, 3, 3, 3, 3, 13, 12, 13, 12, 13, 12, voxel_general_size, new Vector3(0, 0, -0.1f), 30, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                worldMatrix_instances_l_hand_grab[0][0] = new Matrix[_inst_p_l_hand_x * _inst_p_l_hand_y * _inst_p_l_hand_z];
                for (int i = 0; i < worldMatrix_instances_l_hand_grab[0][0].Length; i++)
                {
                    worldMatrix_instances_l_hand_grab[0][0][i] = Matrix.Identity;
                }












                //PLAYER RIGHT HAND
                voxel_type = 1;
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                //instX = 1;
                //instY = 1;
                //instZ = 1;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
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
                _player_rght_hnd[0][0] = new sc_voxel();
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;
                is_static = true;
                _player_rght_hnd[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerHandRight, 9, 9, 9, 18, 9, 9, 9, 9, 9, 4, 3, 13, 12, 18, 17, voxel_general_size, new Vector3(0, 0, -0.1f), 75, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                worldMatrix_instances_r_hand[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
                for (int i = 0; i < worldMatrix_instances_r_hand[0][0].Length; i++)
                {
                    worldMatrix_instances_r_hand[0][0][i] = Matrix.Identity;
                }




                ////////////////////////////////////////////////
                //////////CONTAINMENT GRIDS RIGHT HAND//////////
                ////////////////////////////////////////////////
                r = 0.85f;
                g = 0.85f;
                b = 0.85f;
                a = 1;
                _object_worldmatrix = Matrix.Identity;
                //offsetPosX = _grid_size_x * 1.15f; //x between each world instance
                //offsetPosY = _grid_size_y * 1.15f; //y between each world instance
                //offsetPosZ = _grid_size_z * 1.15f; //z between each world instance
                _object_worldmatrix = WorldMatrix;
                _object_worldmatrix.M41 = 0;
                _size_screen = 0.015f;
                _object_worldmatrix.M42 = _player_rght_hnd[0][0]._arrayOfInstances[0]._POSITION.M42 + (_player_rght_hnd[0][0]._total_torso_height) + (1 * 0.001f); //_terrain_size_y + (_terrain_size_y * 0.501f)-5 //_terrain[0][0]._arrayOfInstances[0]._POSITION.M42
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
                }
                ////////////////////////////////////////////////
                //////////CONTAINMENT GRIDS RIGHT HAND//////////
                ////////////////////////////////////////////////


                //PLAYER LEFT HAND
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
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
                _player_lft_hnd[0][0] = new sc_voxel();
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;
                is_static = true;
                _player_lft_hnd[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.0125f, 0.035f, 0.055f, new Vector4(r, g, b, a), _inst_p_l_hand_x, _inst_p_l_hand_y, _inst_p_l_hand_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerHandLeft, 9, 9, 9, 18, 9, 9, 9, 9, 9, 4, 3, 13, 12, 18, 17, voxel_general_size, new Vector3(0, 0, -0.1f), 75, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                worldMatrix_instances_l_hand[0][0] = new Matrix[_inst_p_l_hand_x * _inst_p_l_hand_y * _inst_p_l_hand_z];
                for (int i = 0; i < worldMatrix_instances_l_hand[0][0].Length; i++)
                {
                    worldMatrix_instances_l_hand[0][0][i] = Matrix.Identity;
                }




                ////////////////////////////////////////////////
                //////////CONTAINMENT GRIDS LEFT HAND//////////
                ////////////////////////////////////////////////
                r = 0.85f;
                g = 0.85f;
                b = 0.85f;
                a = 1;
                _object_worldmatrix = Matrix.Identity;
                //offsetPosX = _grid_size_x * 1.15f; //x between each world instance
                //offsetPosY = _grid_size_y * 1.15f; //y between each world instance
                //offsetPosZ = _grid_size_z * 1.15f; //z between each world instance
                _object_worldmatrix = WorldMatrix;
                _object_worldmatrix.M41 = 0;
                _size_screen = 0.015f;
                _object_worldmatrix.M42 = _player_lft_hnd[0][0]._arrayOfInstances[0]._POSITION.M42 + (_player_lft_hnd[0][0]._total_torso_height) + (1 * 0.001f); //_terrain_size_y + (_terrain_size_y * 0.501f)-5 //_terrain[0][0]._arrayOfInstances[0]._POSITION.M42
                _object_worldmatrix.M43 = 0;
                _object_worldmatrix.M44 = 1;


                _type_of_cube = 0;
                _world_containment_grid_list_LH[0] = new sc_containment_grid[1];
                _world_containment_grid_list_LH[0][0] = new sc_containment_grid();

                //_world_grid_list[0][0].Initialize(SC_console_directx.D3D, 10, 10, _size_screen, 10, 10, _grid_size_x, _grid_size_y, _grid_size_z, new Vector4(r, g, b, a), _inst_terrain_tile_x, _inst_terrain_tile_y, _inst_terrain_tile_z, SC_Update.HWND, _object_worldmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, SCCoreSystems.sc_console.SC_console_directx.BodyTag.sc_grid, true, 0, 10, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

                _world_containment_grid_list_LH[0][0].Initialize(SC_console_directx.D3D, 10, 10, _size_screen, 10, 10, _grid_size_x, _grid_size_y, _grid_size_z, new Vector4(r, g, b, a), _inst_terrain_tile_x, _inst_terrain_tile_y, _inst_terrain_tile_z, SC_Update.HWND, _object_worldmatrix, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, SCCoreSystems.sc_console.SC_console_directx.BodyTag.sc_containment_grid, true, 0, 10, 0, 0, 0, 0, 0, 0, false, true, false, false, false, false); //, "terrainGrassDirt.bmp" //0.00035f
                //_world_containment_grid_list_LH[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0.1f, 1, 1, _inst_terrain_tile_x, _inst_terrain_tile_y, _inst_terrain_tile_z, new Vector4(r, g, b, a), _inst_screen_x, _inst_screen_y, _inst_screen_z, SC_Update.HWND, _object_worldmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, SCCoreSystems.sc_console.SC_console_directx.BodyTag.sc_containment_grid, false, 1, 100, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_containment_grid_LH[0] = new Matrix[1][];
                worldMatrix_instances_containment_grid_LH[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
                for (int i = 0; i < worldMatrix_instances_containment_grid_LH[0][0].Length; i++)
                {
                    worldMatrix_instances_containment_grid_LH[0][0][i] = _player_lft_hnd[0][0]._arrayOfInstances[i].current_pos;
                }
                ////////////////////////////////////////////////
                //////////CONTAINMENT GRIDS LEFT HAND//////////
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



















                //TORSO
                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = 0 + 0;
                _tempMatroxer.M42 = -0.35f; // -0.1f
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_torso[0] = new sc_voxel();
                //_hasinit0 = _player_torso.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f                                                                                                                                                                                                                                                                                        //_hasinit0 = _player_torso.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_player_torso[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), _inst_p_torso_x, _inst_p_torso_y, _inst_p_torso_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerTorso, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;
                _mass = 100;
                _player_torso[0][0] = new sc_voxel();
                _player_torso[0][0].Initialize(
                    SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight,
                    0, 1, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a),
                    _inst_p_torso_x, _inst_p_torso_y, _inst_p_torso_z,
                    Program.consoleHandle, _tempMatroxer, _type_of_cube,
                    offsetPosX, offsetPosY, offsetPosZ,
                    _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerTorso,
                    9, 9, 9, 30, 9, 40, 30, 9, 40, 45, 44, 57, 56, 17, 16, voxel_general_size,
                    new Vector3(0, 0, 0), 43,
                    vertoffsetx, vertoffsety, vertoffsetz,
                    _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f



                //_player_torso[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.175f, 0.065f, new Vector4(r, g, b, a), _inst_p_torso_x, _inst_p_torso_y, _inst_p_torso_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerTorso, 2, 9, 2, 2, 2, 2, 45, 44, 60, 59, 10, 9, 0.0025f, new Vector3(0, 0, 0), 500); //, "terrainGrassDirt.bmp" //0.00035f
                worldMatrix_instances_torso[0][0] = new Matrix[_inst_p_torso_x * _inst_p_torso_y * _inst_p_torso_z];
                for (int i = 0; i < worldMatrix_instances_torso[0][0].Length; i++)
                {
                    worldMatrix_instances_torso[0][0][i] = Matrix.Identity;
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
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = 0;
                _tempMatroxer.M42 = -0.625f;
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                _mass = 100;
                //_player_pelvis[0] = new sc_voxel();
                //_hasinit0 = _player_pelvis.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 9, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_pelvis[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerPelvis, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;
                _mass = 100;
                _player_pelvis[0][0] = new sc_voxel();
                //_player_pelvis[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.125f, 0.05f, 0.065f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerPelvis, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                _player_pelvis[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight,
                    0, 1, 1, 1, 0.125f, 0.05f, 0.065f,
                    new Vector4(r, g, b, a), _inst_p_pelvis_x, _inst_p_pelvis_y, _inst_p_pelvis_z,
                    Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerPelvis,
                    9, 9, 9, 20, 9, 9, 9, 9, 9, 40, 39, 25, 24, 20, 19,
                    voxel_general_size, new Vector3(0, 0, 0), 24,
                    vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                worldMatrix_instances_pelvis[0][0] = new Matrix[_inst_p_pelvis_x * _inst_p_pelvis_y * _inst_p_pelvis_z];
                for (int i = 0; i < worldMatrix_instances_pelvis[0][0].Length; i++)
                {
                    worldMatrix_instances_pelvis[0][0][i] = Matrix.Identity;
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
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = 0.0875f;
                _tempMatroxer.M42 = -0.2f;
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_rght_shldr[0] = new sc_voxel();
                //_hasinit0 = _player_rght_shldr.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 9, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_rght_shldr[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_shoulder_x, _inst_p_r_shoulder_y, _inst_p_r_shoulder_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerShoulderRight, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                _mass = 100;
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;


                _player_rght_shldr[0][0] = new sc_voxel();
                //_player_rght_shldr[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerShoulderRight, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                _player_rght_shldr[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_l_shoulder_x, _inst_p_l_shoulder_y, _inst_p_l_shoulder_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerShoulderRight, 9, 9, 9, 9, 9, 9, 9, 9, 9, 20, 19, 20, 19, 20, 19, voxel_general_size, new Vector3(0, 0, 0), 17, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f


                worldMatrix_instances_r_shoulder[0][0] = new Matrix[_inst_p_r_shoulder_x * _inst_p_r_shoulder_y * _inst_p_r_shoulder_z];
                for (int i = 0; i < worldMatrix_instances_r_shoulder[0][0].Length; i++)
                {
                    worldMatrix_instances_r_shoulder[0][0][i] = Matrix.Identity;
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
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = -0.0875f;
                _tempMatroxer.M42 = -0.2f;
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                _mass = 100;
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;
                //_player_lft_shldr[0] = new sc_voxel();
                //_hasinit0 = _player_lft_shldr.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 9, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_lft_shldr[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_l_shoulder_x, _inst_p_l_shoulder_y, _inst_p_l_shoulder_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerShoulderLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f


                _player_lft_shldr[0][0] = new sc_voxel();
                //_player_lft_shldr[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerShoulderLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                _player_lft_shldr[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_l_shoulder_x, _inst_p_l_shoulder_y, _inst_p_l_shoulder_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerShoulderLeft, 9, 9, 9, 9, 9, 9, 9, 9, 9, 20, 19, 20, 19, 20, 19, voxel_general_size, new Vector3(0, 0, 0), 17, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_l_shoulder[0][0] = new Matrix[_inst_p_l_shoulder_x * _inst_p_l_shoulder_y * _inst_p_l_shoulder_z];
                for (int i = 0; i < worldMatrix_instances_l_shoulder[0][0].Length; i++)
                {
                    worldMatrix_instances_l_shoulder[0][0][i] = Matrix.Identity;
                }








                /////////////////
                ///////HEAD//////
                /////////////////
                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = 0;
                _tempMatroxer.M42 = 0.30f; // -0.1f
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                _player_head[0][0] = new sc_voxel();
                _player_head[0][0].Initialize(
                    SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight,
                    0, 1, 1, 1, 0.05f, 0.05f, 0.05f,
                    new Vector4(r, g, b, a),
                    _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z,
                    Program.consoleHandle,
                    _tempMatroxer,
                    _type_of_cube,
                    offsetPosX, offsetPosY, offsetPosZ,
                    _thejitter_world,
                    _mass,
                    is_static,
                    SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerHead,
                    //9, 9, 9, 9, 9, 15, 35, 20, 20, 40, 23, 22, BACKUP
                    9, 11, 9, 11, 9, 11, 11, 9, 11, 25, 24, 30, 28, 25, 24,
                    voxel_general_size,
                    new Vector3(0, 0, 0),
                    23,
                    vertoffsetx, vertoffsety, vertoffsetz,
                    _addToWorld,
                    voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_head[0][0] = new Matrix[_inst_p_l_shoulder_x * _inst_p_l_shoulder_y * _inst_p_l_shoulder_z];
                for (int i = 0; i < worldMatrix_instances_head[0][0].Length; i++)
                {
                    worldMatrix_instances_head[0][0][i] = Matrix.Identity;
                }
                /////////////////
                ///////HEAD//////
                /////////////////

                //chest looking like voxel settings 11, 12, 11, 12, 11, 14, 9, 9, 9, 35, 33, 35, 33, 35, 33,
                //same top japanese looking like voxel settings 11, 12, 11, 12, 11, 14, 9, 9, 9, 35, 33, 35, 33, 35, 33,
                //Elite Dangerous stations voxel type settings 9, 9, 9, 9, 9, 9, 35, 34, 40, 59, 20, 19, 
                //9, 9, 9, 9, 9, 9, 35, 34, 40, 59, 35, 34, 
                //RIDDICK 1 kinda monster head . id have to recheck the movie. 7, 9, 7, 30, 9, 40, 30, 9, 40, 30, 30, 30, 30, 30, 30,

                //LEFT LOWER ARM
                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = -0.25f;
                _tempMatroxer.M42 = -0.15f;
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;

                //_player_lft_lower_arm[0] = new sc_voxel();
                //_hasinit0 = _player_lft_lower_arm.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_lft_lower_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_l_lowerarm_x, _inst_p_l_lowerarm_y, _inst_p_l_lowerarm_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerArmLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;

                _player_lft_lower_arm[0][0] = new sc_voxel();
                // _player_lft_lower_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerArmLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                //_player_lft_lower_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerArmLeft, 9, 9, 9, 18, 13, 9, 9, 10, 33, 32, 11, 10, voxel_general_size, new Vector3(0, 0, 0), 74, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                _player_lft_lower_arm[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.035f, 0.10550f, 0.035f,
                    new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world,
                    _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerArmLeft,
                    8, 8, 8, 17, 17, 17, 17, 17, 17, 11, 10, 33, 32, 11, 10,
                    voxel_general_size, new Vector3(0, 0, 0), 50, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                //FOR VOXEL ARROW
                //_player_lft_lower_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerArmLeft, 9, 9, 9, 18, 17, 100, 3, 10, 33, 32, 11, 10, voxel_general_size, new Vector3(0, 0, 0), 70, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_l_lowerarm[0][0] = new Matrix[_inst_p_l_lowerarm_x * _inst_p_l_lowerarm_y * _inst_p_l_lowerarm_z];
                for (int i = 0; i < worldMatrix_instances_l_lowerarm[0][0].Length; i++)
                {
                    worldMatrix_instances_l_lowerarm[0][0][i] = Matrix.Identity;
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
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = -0.25f;
                _tempMatroxer.M42 = -0.375f;
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_lft_upper_arm[0] = new sc_voxel();
                //_hasinit0 = _player_lft_upper_arm.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_lft_upper_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;


                _player_lft_upper_arm[0][0] = new sc_voxel();
                //_player_lft_upper_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                _player_lft_upper_arm[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a),
                    _inst_p_l_hand_x, _inst_p_l_hand_y, _inst_p_l_hand_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static,
                    SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmLeft,
                    9, 9, 9, 17, 17, 17, 17, 17, 17, 13, 12, 40, 39, 13, 12,
                    voxel_general_size, new Vector3(0, 0, 0), 50, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_l_upperarm[0][0] = new Matrix[_inst_p_l_upperarm_x * _inst_p_l_upperarm_y * _inst_p_l_upperarm_z];
                for (int i = 0; i < worldMatrix_instances_l_upperarm[0][0].Length; i++)
                {
                    worldMatrix_instances_l_upperarm[0][0][i] = Matrix.Identity;
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
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = -0.25f;
                _tempMatroxer.M42 = -0.15f;
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;

                //_player_lft_lower_arm[0] = new sc_voxel();
                //_hasinit0 = _player_lft_lower_arm.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_lft_lower_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_l_lowerarm_x, _inst_p_l_lowerarm_y, _inst_p_l_lowerarm_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerArmLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;

                _player_rght_lower_arm[0][0] = new sc_voxel();
                // _player_lft_lower_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerArmLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                _player_rght_lower_arm[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a),
                    _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static,
                    SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerArmRight,
                    8, 8, 8, 17, 17, 17, 17, 17, 17, 11, 10, 33, 32, 11, 10,
                    voxel_general_size, new Vector3(0, 0, 0), 50, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                //_player_rght_lower_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerArmRight, 7, 7, 9, 18, 21, 25, 10, 9, 33, 32, 11, 10, voxel_general_size, new Vector3(0, 0, 0), 72, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                //FOR VOXEL ARROW
                //_player_lft_lower_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.08250f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerArmLeft, 9, 9, 9, 18, 17, 100, 3, 10, 33, 32, 11, 10, voxel_general_size, new Vector3(0, 0, 0), 70, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

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
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = 0.25f;
                _tempMatroxer.M42 = -0.375f;
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_rght_upper_arm[0] = new sc_voxel();
                //_hasinit0 = _player_rght_upper_arm.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_rght_upper_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_upperarm_x, _inst_p_r_upperarm_y, _inst_p_r_upperarm_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmRight, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;
                _player_rght_upper_arm[0][0] = new sc_voxel();
                //_player_rght_upper_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmRight, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                _player_rght_upper_arm[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.035f, 0.1055f, 0.035f, new Vector4(r, g, b, a),
                _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static,
                SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmRight,
                9, 9, 9, 17, 17, 17, 17, 17, 17, 13, 12, 40, 39, 13, 12,
                voxel_general_size, new Vector3(0, 0, 0), 50, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
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
                g = 0.19f;
                b = 0.19f;
                a = 1;
                //_tempMatroxer.M41 = -0.25f; /
                //_tempMatroxer.M42 = -0.2f;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = 0.25f;
                _tempMatroxer.M42 = (_player_rght_upper_arm[0][0]._POSITION.M42 + (_player_rght_upper_arm[0][0]._total_torso_height * 0.5f) + 0.45f);// - 0.25f;
                _tempMatroxer.M43 = -0.25f;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_rght_elbow_target[0] = new sc_voxel();
                //_hasinit0 = _player_rght_elbow_target.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_rght_elbow_target[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;
                _player_rght_elbow_target[0][0] = new sc_voxel();
                _player_rght_elbow_target[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightElbowTarget, 2, 2, 2, 2, 2, 2, 9, 9, 9, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 25, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                worldMatrix_instances_r_elbow_target[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
                for (int i = 0; i < worldMatrix_instances_r_elbow_target[0][0].Length; i++)
                {
                    worldMatrix_instances_r_elbow_target[0][0][i] = Matrix.Identity;
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
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = -0.25f;
                _tempMatroxer.M42 = (_player_lft_upper_arm[0][0]._POSITION.M42 + (_player_lft_upper_arm[0][0]._total_torso_height * 0.5f) + 0.45f);// - 0.25f;
                _tempMatroxer.M43 = -0.25f;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_lft_elbow_target[0] = new sc_voxel();
                //_hasinit0 = _player_lft_elbow_target.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_lft_elbow_target[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_l_hand_x, _inst_p_l_hand_y, _inst_p_l_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLeftElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;


                _player_lft_elbow_target[0][0] = new sc_voxel();
                _player_lft_elbow_target[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLeftElbowTarget, 2, 2, 2, 2, 2, 2, 9, 9, 9, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 25, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f



                worldMatrix_instances_l_elbow_target[0][0] = new Matrix[_inst_p_l_hand_x * _inst_p_l_hand_y * _inst_p_l_hand_z];
                for (int i = 0; i < worldMatrix_instances_l_elbow_target[0][0].Length; i++)
                {
                    worldMatrix_instances_l_elbow_target[0][0][i] = Matrix.Identity;
                }







                //RIGHT ELBOW TARGET TWO
                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = 1.5f;
                _tempMatroxer.M42 = (_player_rght_upper_arm[0][0]._POSITION.M42 + (_player_rght_upper_arm[0][0]._total_torso_height * 0.5f) + 1);
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_rght_elbow_target_two[0] = new sc_voxel();
                //_hasinit0 = _player_rght_elbow_target_two.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_rght_elbow_target_two[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;
                _player_rght_elbow_target_two[0][0] = new sc_voxel();
                _player_rght_elbow_target_two[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightElbowTargettwo, 2, 2, 2, 2, 2, 2, 9, 9, 9, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 75, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                worldMatrix_instances_r_elbow_target_two[0][0] = new Matrix[_inst_p_r_hand_x * _inst_p_r_hand_y * _inst_p_r_hand_z];
                for (int i = 0; i < worldMatrix_instances_r_elbow_target_two[0][0].Length; i++)
                {
                    worldMatrix_instances_r_elbow_target_two[0][0][i] = Matrix.Identity;
                }





                //LEFT ELBOW TARGET TWO
                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = -1.5f;
                _tempMatroxer.M42 = (_player_lft_upper_arm[0][0]._POSITION.M42 + (_player_lft_upper_arm[0][0]._total_torso_height * 0.5f) + 1);
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_lft_elbow_target_two[0] = new sc_voxel();
                //_hasinit0 = _player_lft_elbow_target_two.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_lft_elbow_target_two[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLeftElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;


                _player_lft_elbow_target_two[0][0] = new sc_voxel();
                _player_lft_elbow_target_two[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLeftElbowTargettwo, 2, 2, 2, 2, 2, 2, 9, 9, 9, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 75, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_l_elbow_target_two[0][0] = new Matrix[_inst_p_l_hand_x * _inst_p_l_hand_y * _inst_p_l_hand_z];
                for (int i = 0; i < worldMatrix_instances_l_elbow_target_two[0][0].Length; i++)
                {
                    worldMatrix_instances_l_elbow_target_two[0][0][i] = Matrix.Identity;
                }

                ////////////////////
                /////LOWER BODY/////
                ////////////////////

                //RIGHT UPPER LEG
                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = 0.09f;
                _tempMatroxer.M42 = -((((13 + 12 + 1) + (1 * 0.00123f)) * 0.10550f * 0.30f));//(_player_pelvis[0][0]._POSITION.M42 + (-_player_pelvis[0][0]._total_torso_height * 0.5f) + 1);// //0.0025f //0.10550f * 
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_r_upper_leg[0] = new sc_voxel();
                //_hasinit0 = _player_r_upper_leg.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_r_upper_leg[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;

                _player_r_upper_leg[0][0] = new sc_voxel();
                //_player_lft_upper_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                _player_r_upper_leg[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 0.1f, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a),
                    _inst_p_upper_r_leg_x, _inst_p_upper_r_leg_y, _inst_p_upper_r_leg_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static,
                    SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperLegRight,
                    9, 9, 9, 17, 17, 17, 17, 17, 17, 13, 12, 40, 39, 13, 12,
                    voxel_general_size, new Vector3(0, 0, 0), 50, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_r_upper_leg[0][0] = new Matrix[_inst_p_upper_r_leg_x * _inst_p_upper_r_leg_y * _inst_p_upper_r_leg_z];
                for (int i = 0; i < worldMatrix_instances_r_upper_leg[0][0].Length; i++)
                {
                    worldMatrix_instances_r_upper_leg[0][0][i] = Matrix.Identity;
                }




                //LEFT UPPER LEG
                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = -0.09f;
                _tempMatroxer.M42 = -((((13 + 12 + 1) + (1 * 0.00123f)) * 0.10550f * 0.30f));
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_r_upper_leg[0] = new sc_voxel();
                //_hasinit0 = _player_r_upper_leg.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_r_upper_leg[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;

                _player_l_upper_leg[0][0] = new sc_voxel();
                //_player_lft_upper_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                _player_l_upper_leg[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a),
                    _inst_p_upper_l_leg_x, _inst_p_upper_l_leg_y, _inst_p_upper_l_leg_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static,
                    SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperLegLeft,
                    9, 9, 9, 17, 17, 17, 17, 17, 17, 13, 12, 40, 39, 13, 12,
                    voxel_general_size, new Vector3(0, 0, 0), 50, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_l_upper_leg[0][0] = new Matrix[_inst_p_upper_l_leg_x * _inst_p_upper_l_leg_y * _inst_p_upper_l_leg_z];
                for (int i = 0; i < worldMatrix_instances_l_upper_leg[0][0].Length; i++)
                {
                    worldMatrix_instances_l_upper_leg[0][0][i] = Matrix.Identity;
                }


                //RIGHT LOWER LEG
                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = 0.09f;
                _tempMatroxer.M42 = -((((13 + 12 + 1) + (1 * 0.00123f)) * 0.10550f * 0.30f));
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_r_upper_leg[0] = new sc_voxel();
                //_hasinit0 = _player_r_upper_leg.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_r_upper_leg[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;

                _player_r_lower_leg[0][0] = new sc_voxel();
                //_player_lft_upper_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                _player_r_lower_leg[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a),
                    _inst_p_lower_r_leg_x, _inst_p_lower_r_leg_y, _inst_p_lower_r_leg_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static,
                    SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerLegRight,
                    9, 9, 9, 17, 17, 17, 17, 17, 17, 13, 12, 40, 39, 13, 12,
                    voxel_general_size, new Vector3(0, 0, 0), 50, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_r_lower_leg[0][0] = new Matrix[_inst_p_lower_r_leg_x * _inst_p_lower_r_leg_y * _inst_p_lower_r_leg_z];
                for (int i = 0; i < worldMatrix_instances_r_lower_leg[0][0].Length; i++)
                {
                    worldMatrix_instances_r_lower_leg[0][0][i] = Matrix.Identity;
                }





                //LEFT LOWER LEG
                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = 0.09f;
                _tempMatroxer.M42 = -((((13 + 12 + 1) + (1 * 0.00123f)) * 0.10550f * 0.30f));
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_r_upper_leg[0] = new sc_voxel();
                //_hasinit0 = _player_r_upper_leg.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_r_upper_leg[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;

                _player_l_lower_leg[0][0] = new sc_voxel();
                //_player_lft_upper_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                _player_l_lower_leg[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a),
                    _inst_p_lower_l_leg_x, _inst_p_lower_l_leg_y, _inst_p_lower_l_leg_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static,
                    SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerLegLeft,
                    9, 9, 9, 17, 17, 17, 17, 17, 17, 13, 12, 40, 39, 13, 12,
                    voxel_general_size, new Vector3(0, 0, 0), 50, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_l_lower_leg[0][0] = new Matrix[_inst_p_lower_l_leg_x * _inst_p_lower_l_leg_y * _inst_p_lower_l_leg_z];
                for (int i = 0; i < worldMatrix_instances_l_lower_leg[0][0].Length; i++)
                {
                    worldMatrix_instances_l_lower_leg[0][0][i] = Matrix.Identity;
                }







                //LEFT KNEE TARGET
                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                //_tempMatroxer.M41 = -0.25f; /
                //_tempMatroxer.M42 = -0.2f;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = 0.09f;
                _tempMatroxer.M42 = -((((13 + 12 + 1) + (1 * 0.00123f)) * 0.10550f * 0.30f));// - 0.25f;
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_rght_elbow_target[0] = new sc_voxel();
                //_hasinit0 = _player_rght_elbow_target.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_rght_elbow_target[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;

                //voxel_type = 0;

                _type_of_cube = 2;
                _player_lft_target_knee[0][0] = new sc_voxel();
                _player_lft_target_knee[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLeftTargetKnee, 2, 2, 2, 2, 2, 2, 9, 9, 9, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 25, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                worldMatrix_instances_l_target_knee[0][0] = new Matrix[_inst_p_upper_l_leg_x * _inst_p_upper_l_leg_y * _inst_p_upper_l_leg_z];
                for (int i = 0; i < worldMatrix_instances_l_target_knee[0][0].Length; i++)
                {
                    worldMatrix_instances_l_target_knee[0][0][i] = Matrix.Identity;
                }




                //RIGHT KNEE TARGET
                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                //_tempMatroxer.M41 = -0.25f;
                //_tempMatroxer.M42 = -0.2f;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = 0.09f;
                _tempMatroxer.M42 = -((((13 + 12 + 1) + (1 * 0.00123f)) * 0.10550f * 0.30f));// - 0.25f; //(_player_r_upper_leg[0][0]._POSITION.M42 + (_player_r_upper_leg[0][0]._total_torso_height * 0.5f) + 0.45f)
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_rght_elbow_target[0] = new sc_voxel();
                //_hasinit0 = _player_rght_elbow_target.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_rght_elbow_target[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;
                _player_rght_target_knee[0][0] = new sc_voxel();
                _player_rght_target_knee[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.15f, 0.15f, 0.15f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightTargetKnee, 2, 2, 2, 2, 2, 2, 9, 9, 9, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 25, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                worldMatrix_instances_r_target_knee[0][0] = new Matrix[_inst_p_upper_r_leg_x * _inst_p_upper_r_leg_y * _inst_p_upper_r_leg_z];
                for (int i = 0; i < worldMatrix_instances_r_target_knee[0][0].Length; i++)
                {
                    worldMatrix_instances_r_target_knee[0][0][i] = Matrix.Identity;
                }




                //LEFT KNEE TARGET TWO
                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                //_tempMatroxer.M41 = -0.25f; /
                //_tempMatroxer.M42 = -0.2f;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = 0.25f;
                _tempMatroxer.M42 = (_player_l_upper_leg[0][0]._POSITION.M42 + (_player_l_upper_leg[0][0]._total_torso_height * 0.5f) + 0.45f);// - 0.25f;
                _tempMatroxer.M43 = -0.25f;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_rght_elbow_target[0] = new sc_voxel();
                //_hasinit0 = _player_rght_elbow_target.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_rght_elbow_target[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;
                _player_lft_target_two_knee[0][0] = new sc_voxel();
                _player_lft_target_two_knee[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLeftTargettwoKnee, 2, 2, 2, 2, 2, 2, 9, 9, 9, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 25, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                worldMatrix_instances_l_target_two_knee[0][0] = new Matrix[_inst_p_upper_l_leg_x * _inst_p_upper_l_leg_y * _inst_p_upper_l_leg_z];
                for (int i = 0; i < worldMatrix_instances_l_target_two_knee[0][0].Length; i++)
                {
                    worldMatrix_instances_l_target_two_knee[0][0][i] = Matrix.Identity;
                }

                //RIGHT KNEE TARGET TWO
                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                //_tempMatroxer.M41 = -0.25f; /
                //_tempMatroxer.M42 = -0.2f;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = 0.25f;
                _tempMatroxer.M42 = (_player_r_upper_leg[0][0]._POSITION.M42 + (_player_r_upper_leg[0][0]._total_torso_height * 0.5f) + 0.45f);// - 0.25f;
                _tempMatroxer.M43 = -0.25f;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_rght_elbow_target[0] = new sc_voxel();
                //_hasinit0 = _player_rght_elbow_target.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_rght_elbow_target[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;
                _player_rght_target_two_knee[0][0] = new sc_voxel();
                _player_rght_target_two_knee[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightTargettwoKnee, 2, 2, 2, 2, 2, 2, 9, 9, 9, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 25, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                worldMatrix_instances_r_target_two_knee[0][0] = new Matrix[_inst_p_upper_r_leg_x * _inst_p_upper_r_leg_y * _inst_p_upper_r_leg_z];
                for (int i = 0; i < worldMatrix_instances_r_target_two_knee[0][0].Length; i++)
                {
                    worldMatrix_instances_r_target_two_knee[0][0][i] = Matrix.Identity;
                }

                //RIGHT KNEE TARGET TWO
                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                //_tempMatroxer.M41 = -0.25f; /
                //_tempMatroxer.M42 = -0.2f;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = 0.25f;
                _tempMatroxer.M42 = (_player_l_upper_leg[0][0]._POSITION.M42 + (_player_l_upper_leg[0][0]._total_torso_height * 0.5f) + 0.45f);// - 0.25f;
                _tempMatroxer.M43 = -0.25f;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_rght_elbow_target[0] = new sc_voxel();
                //_hasinit0 = _player_rght_elbow_target.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_rght_elbow_target[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.075f, 0.075f, 0.075f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightElbowTarget, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;
                _player_lft_target_two_knee[0][0] = new sc_voxel();
                _player_lft_target_two_knee[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightTargettwoKnee, 2, 2, 2, 2, 2, 2, 9, 9, 9, 10, 9, 10, 9, 10, 9, voxel_general_size, Vector3.Zero, 25, vertoffsetx, vertoffsety, vertoffsetz, 0, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                worldMatrix_instances_l_target_two_knee[0][0] = new Matrix[_inst_p_upper_l_leg_x * _inst_p_upper_l_leg_y * _inst_p_upper_l_leg_z];
                for (int i = 0; i < worldMatrix_instances_l_target_two_knee[0][0].Length; i++)
                {
                    worldMatrix_instances_l_target_two_knee[0][0][i] = Matrix.Identity;
                }








                //RIGHT FOOT
                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = 0.09f;
                _tempMatroxer.M42 = -((((13 + 12 + 1 + 13 + 12 + 1 + 13 + 12 + 1) + (1 * 0.00123f)) * 0.10550f * 0.30f));
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_r_upper_leg[0] = new sc_voxel();
                //_hasinit0 = _player_r_upper_leg.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_r_upper_leg[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;

                _player_r_foot[0][0] = new sc_voxel();
                //_player_lft_upper_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                _player_r_foot[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a),
                    _inst_p_r_foot_x, _inst_p_r_foot_y, _inst_p_r_foot_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static,
                    SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerFootRight,
                    9, 9, 9, 17, 17, 17, 17, 17, 17, 9, 8, 9, 8, 20, 19,
                    voxel_general_size, new Vector3(0, 0, 0), 50, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_r_foot[0][0] = new Matrix[_inst_p_r_foot_x * _inst_p_r_foot_y * _inst_p_r_foot_z];
                for (int i = 0; i < worldMatrix_instances_r_foot[0][0].Length; i++)
                {
                    worldMatrix_instances_r_foot[0][0][i] = Matrix.Identity;
                }


                //LEFT FOOT
                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                r = 0.19f;
                g = 0.19f;
                b = 0.19f;
                a = 1;
                _tempMatroxer = Matrix.Identity;
                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = 0.09f;
                _tempMatroxer.M42 = -((((13 + 12 + 1 + 13 + 12 + 1 + 13 + 12 + 1) + (1 * 0.00123f)) * 0.10550f * 0.30f));
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;
                offsetPosX = _dist_between * 2;
                offsetPosY = _dist_between * 2;
                offsetPosZ = _dist_between * 2;
                //_player_r_upper_leg[0] = new sc_voxel();
                //_hasinit0 = _player_r_upper_leg.Initialize(_SC_console_directx.D3D, _SC_console_directx.D3D.SurfaceWidth, _SC_console_directx.D3D.SurfaceHeight, _size_screen, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ, vertOffsetX, vertOffsetY, vertOffsetZ); //, "terrainGrassDirt.bmp" //0.00035f
                //_hasinit0 = _player_r_upper_leg[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, 2, offsetPosX, offsetPosY, offsetPosZ, World, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmLeft, _static, 1, _mass, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                voxel_general_size = somevoxelsize;
                //voxel_type = 0;
                _type_of_cube = 2;

                _player_l_foot[0][0] = new sc_voxel();
                //_player_lft_upper_arm[0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, 0.05f, 0.05f, 0.05f, new Vector4(r, g, b, a), _inst_p_r_hand_x, _inst_p_r_hand_y, _inst_p_r_hand_z, Hwnd, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, World, _mass, false, SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmLeft, 10, 10, 10, 10, 10, 10, 4, 3, 20, 19, 20, 19, 0.0025f, Vector3.Zero, 300); //, "terrainGrassDirt.bmp" //0.00035f
                _player_l_foot[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 0, 1, 1, 1, 0.035f, 0.10550f, 0.035f, new Vector4(r, g, b, a),
                    _inst_p_l_foot_x, _inst_p_l_foot_y, _inst_p_l_foot_z, Program.consoleHandle, _tempMatroxer, _type_of_cube, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, _mass, is_static,
                    SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerFootLeft,
                    9, 9, 9, 17, 17, 17, 17, 17, 17, 9, 8, 9, 8, 20, 19,
                    voxel_general_size, new Vector3(0, 0, 0), 50, vertoffsetx, vertoffsety, vertoffsetz, _addToWorld, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f

                worldMatrix_instances_l_foot[0][0] = new Matrix[_inst_p_l_foot_x * _inst_p_l_foot_y * _inst_p_l_foot_z];
                for (int i = 0; i < worldMatrix_instances_l_foot[0][0].Length; i++)
                {
                    worldMatrix_instances_l_foot[0][0][i] = Matrix.Identity;
                }














                for (int phys = 0; phys < Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z; phys++)
                {
                    for (int i = 0; i < Program.world_width * Program.world_height * Program.world_depth; i++)
                    {
                        object _some_dator = (object)_sc_jitter_tasks[phys][i]._world_data[0];
                        World _the_current_world = (World)_some_dator;

                        _the_current_world.AddBody(_player_rght_upper_arm[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_rght_lower_arm[0][0]._arrayOfInstances[0].transform.Component.rigidbody);

                        _the_current_world.AddBody(_player_lft_upper_arm[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_lft_lower_arm[0][0]._arrayOfInstances[0].transform.Component.rigidbody);

                        _the_current_world.AddBody(_player_pelvis[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_torso[0][0]._arrayOfInstances[0].transform.Component.rigidbody);

                        _the_current_world.AddBody(_player_lft_hnd[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_rght_hnd[0][0]._arrayOfInstances[0].transform.Component.rigidbody);

                        _the_current_world.AddBody(_player_lft_shldr[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_rght_shldr[0][0]._arrayOfInstances[0].transform.Component.rigidbody);



                        _the_current_world.AddBody(_player_l_foot[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_r_foot[0][0]._arrayOfInstances[0].transform.Component.rigidbody);

                        _the_current_world.AddBody(_player_l_upper_leg[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_r_upper_leg[0][0]._arrayOfInstances[0].transform.Component.rigidbody);

                        _the_current_world.AddBody(_player_l_lower_leg[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                        _the_current_world.AddBody(_player_r_lower_leg[0][0]._arrayOfInstances[0].transform.Component.rigidbody);
                    }
                }








































                //PHYSICS SCREENS
                _grab_rigid_data = new _rigid_data();
                _grab_rigid_data._body = null;
                _grab_rigid_data.position = Matrix.Identity;
                _grab_rigid_data._index = -1;
                _grab_rigid_data._physics_engine_index = -1;
                //SET TO 0 AND YOU HAVE USE A SHADERRESOURCE INSTEAD for the texture instead of using the color. cheap way for the moment as my switch wasnt working.
                r = 0;
                g = 0;
                b = 0;
                a = 0;
                _object_worldmatrix = Matrix.Identity;
                var _offsetPos = new Vector3(0.15f, 0.15f, 0.15f);
                _object_worldmatrix = WorldMatrix;
                _object_worldmatrix.M41 = -1.5f + 0;
                _object_worldmatrix.M42 = 0.5f + 0;
                _object_worldmatrix.M43 = -1.5f + 0;
                _object_worldmatrix.M44 = 1;
                _object_worldmatrix.M41 += _offsetPos.X;
                _object_worldmatrix.M42 += _offsetPos.Y;
                _object_worldmatrix.M43 += _offsetPos.Z;
                _size_screen = 0.0005f;
                var sizeWidth01 = (float)(((float)SC_console_directx.D3D.SurfaceWidth * mulScreen) * _size_screen) * _screen_size_x;
                var sizeheight01 = (float)((float)(SC_console_directx.D3D.SurfaceHeight * mulScreen) * _size_screen) * _screen_size_y;
                var sizedepth01 = 1 * _screen_size_z;
                float sizeWidther01 = (float)(sizeWidth01 * 0.5f);
                float sizeHeighter01 = (float)(sizeheight01 * 0.5f);
                float sizeDepther01 = (float)(sizedepth01 * 0.5f);
                offsetPosX = sizeWidth01 * 2;
                offsetPosY = sizeheight01 * 2;
                offsetPosZ = sizedepth01 * 2;
                _world_screen_list[0] = new SC_cube[1];
                _world_screen_list[0][0] = new SC_cube();
                worldMatrix_instances_screens[0] = new Matrix[1][];
                _world_screen_list[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, sizeWidther01, sizeHeighter01, sizeDepther01, new Vector4(r, g, b, a), _inst_screen_x, _inst_screen_y, _inst_screen_z, Program.consoleHandle, _object_worldmatrix, 3, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedScreen, true, 1, 100, 0, 0, 0); //, "terrainGrassDirt.bmp" //0.00035f
                _buo.Add(_world_screen_list[0][0]._arrayOfInstances[0].transform.Component.rigidbody, 3);
                _world_screen_list[0][0]._arrayOfInstances[0].transform.Component.rigidbody.AllowDeactivation = false;
                worldMatrix_instances_screens[0][0] = new Matrix[1];
                worldMatrix_instances_screens[0][0][0] = Matrix.Identity;





                /*int sizeX = (int)1;
                int sizeY = (int)(((SC_console_directx.D3D.SurfaceHeight * 0.0125f)) * _screen_size_y);
                int sizeZ = (int)(((SC_console_directx.D3D.SurfaceWidth * 0.0125f)) * _screen_size_x);
                int _dvX = 10;
                int _dvY = 10;
                a = 1;
                r = 0.65f;
                g = 0.15f;
                b = 0.15f;
                float offsetDepth = 3.0f;
                float depthScreen = _screen_size_z*2;
                _size_screen = 0.0006f;// * ((1 - mulScreen) + 1);
                _screen_grid_Y = new DTerrain_Screen();
                _screen_grid_Y.Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, _size_screen, 10, 10, a, r, g, b, depthScreen, offsetDepth);




                _dvX = (int)(SC_console_directx.D3D.SurfaceWidth * 0.5f * _screen_size_x * 0.01f * 0.55f);
                _dvY = (int)(SC_console_directx.D3D.SurfaceHeight * 0.5f * _screen_size_y * 0.01f); ;
                a = 1;
                r = 0.15f;
                g = 0.65f;
                b = 0.15f;
                offsetDepth = 3.0f;
                depthScreen = _screen_size_z*2;
                _size_screen = 0.0006f;// * ((1 - mulScreen) + 1);
                _screen_metric_grid_Y = new DTerrain_Screen_Metric();
                _screen_metric_grid_Y.Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, _size_screen, _dvX, _dvY, a, r, g, b, depthScreen, offsetDepth);
                */


                int _dvX = (int)(SC_console_directx.D3D.SurfaceWidth * 0.5f * _screen_size_x * 0.01f);
                int _dvY = (int)(SC_console_directx.D3D.SurfaceHeight * 0.5f * _screen_size_y * 0.01f);
                a = 1;
                r = 0.65f;
                g = 0.15f;
                b = 0.15f;
                _size_screen = 0.0005f;
                float maxwidthMul = 1.0f * 1.85f;
                float maxheightMul = 1.0f * 1.85f;
                _screen_grid_Y = new DTerrain_Screen();
                _screen_grid_Y.Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, _size_screen, 10, 10, a, r, g, b, 1, 1, maxwidthMul, maxheightMul);




                _dvX = (int)(SC_console_directx.D3D.SurfaceWidth * 0.5f * _screen_size_x * 0.01f);
                _dvY = (int)(SC_console_directx.D3D.SurfaceWidth * 0.5f * _screen_size_x * 0.01f);

                a = 1;
                r = 0.15f;
                g = 0.65f;
                b = 0.15f;
                _size_screen = 0.0005f;
                maxwidthMul = 0.95f;
                maxheightMul = 0.55f;
                _screen_metric_grid_Y = new DTerrain_Screen_Metric();
                _screen_metric_grid_Y.Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceWidth, _size_screen, _dvX, _dvY, a, r, g, b, 1, 1, maxwidthMul, maxheightMul);







                ////////////////////////////////////////////////
                //////////CONTAINMENT GRIDS SCREEN//////////////
                ////////////////////////////////////////////////
                int sizeX = (int)1;
                int sizeY = (int)(((SC_console_directx.D3D.SurfaceHeight * 0.0125f)) * _screen_size_y);
                int sizeZ = (int)(((SC_console_directx.D3D.SurfaceWidth * 0.0125f)) * _screen_size_x);
                r = 0.85f;
                g = 0.85f;
                b = 0.85f;
                a = 1;
                _object_worldmatrix = Matrix.Identity;
                _object_worldmatrix = WorldMatrix;
                _object_worldmatrix.M41 = 0;
                _object_worldmatrix.M42 = 0;
                _object_worldmatrix.M43 = 0;
                _object_worldmatrix.M44 = 1;
                _size_screen = 0.0006f * 10 * 2;
                _world_containment_grid_screen[0] = new sc_containment_grid[1];
                _world_containment_grid_screen[0][0] = new sc_containment_grid();
                _world_containment_grid_screen[0][0].Initialize(SC_console_directx.D3D, (int)(sizeX * 1), (int)(sizeY * 1), _size_screen, 10, 10, sizeWidther01, sizeHeighter01, sizeDepther01, new Vector4(r, g, b, a), _inst_screen_x, _inst_screen_y, _inst_screen_z, SC_Update.HWND, _object_worldmatrix, 0, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, SCCoreSystems.sc_console.SC_console_directx.BodyTag.sc_containment_grid, true, 0, 10, 0, 0, 0, 0, 0, 0, false, true, false, false, false, false);
                worldMatrix_instances_containment_grid_screen[0] = new Matrix[1][];
                worldMatrix_instances_containment_grid_screen[0][0] = new Matrix[1];
                for (int i = 0; i < worldMatrix_instances_containment_grid_screen[0][0].Length; i++)
                {
                    worldMatrix_instances_containment_grid_screen[0][0][i] = Matrix.Identity;
                }
                ////////////////////////////////////////////////
                //////////CONTAINMENT GRIDS SCREEN//////////////
                ////////////////////////////////////////////////



















                //_arrayOfCubes = new SC_VR_Cube();//[6 * 6 * 6];
                //_arrayOfCubes.Initialize(SC_console_directx.D3D.device, 0.05f, 0.05f, 0.05f, new Vector4(0.1f, 0.1f, 1f, 1), ChunkWidth_L, ChunkWidth_R, ChunkHeight_L, ChunkHeight_R, ChunkDepth_L, ChunkDepth_R);
                /*_tempMatroxer = WorldMatrix;
                _tempMatroxer.M42 = 1;
                sc_jitter_cloth = new PseudoCloth(_thejitter_world, SC_console_directx.D3D, Program.consoleHandle, 10, 10, 0.1f, _tempMatroxer);
                worldMatrix_Cloth_instances = new Matrix[10 * 10];
                */










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

                _size_screen = 0.0005f;
                sizeWidtherer = (float)(((float)SC_console_directx.D3D.SurfaceWidth * mulScreen) * _size_screen);
                sizeheighterer = (float)((float)(SC_console_directx.D3D.SurfaceHeight * mulScreen) * _size_screen);

                //float sizeWidther = (float)(sizeWidth * 0.5f);
                //float sizeHeighter = (float)(sizeheight * 0.5f);
                //float sizeDepther = (float)(sizedepth * 0.5f);







                //_screenCorners = new DModelClass4_cube[4];
                rotatingMatrixScreen.M41 = SC_Update.originPosScreen.X;
                rotatingMatrixScreen.M42 = SC_Update.originPosScreen.Y;
                rotatingMatrixScreen.M43 = SC_Update.originPosScreen.Z;
                _screenDirMatrix[0] = new Matrix[1][];
                point3DCollection[0] = new Vector3[1][];
                _screenDirMatrix_correct_pos[0] = new Matrix[1][];
                _screenDirMatrix[0][0] = new Matrix[4];
                point3DCollection[0][0] = new Vector3[4];
                _screenDirMatrix_correct_pos[0][0] = new Matrix[4];
                for (int i = 0; i < _screenDirMatrix[0][0].Length; i++)
                {
                    _screenDirMatrix[0][0][i] = new Matrix();
                    _screenDirMatrix[0][0][i] = rotatingMatrixScreen;
                }
                _screenDirMatrix[0][0][0].M41 = _world_screen_list[0][0].Vertices[16].position.X;// + originPosScreen.X;
                _screenDirMatrix[0][0][0].M42 = _world_screen_list[0][0].Vertices[16].position.Y;// + originPosScreen.Y;
                _screenDirMatrix[0][0][0].M43 = _world_screen_list[0][0].Vertices[16].position.Z;// + originPosScreen.Z;
                _screenDirMatrix[0][0][1].M41 = _world_screen_list[0][0].Vertices[13].position.X;// + originPosScreen.X;
                _screenDirMatrix[0][0][1].M42 = _world_screen_list[0][0].Vertices[13].position.Y;// + originPosScreen.Y;
                _screenDirMatrix[0][0][1].M43 = _world_screen_list[0][0].Vertices[13].position.Z;// + originPosScreen.Z;
                _screenDirMatrix[0][0][2].M41 = _world_screen_list[0][0].Vertices[15].position.X;// + originPosScreen.X;
                _screenDirMatrix[0][0][2].M42 = _world_screen_list[0][0].Vertices[15].position.Y;// + originPosScreen.Y;
                _screenDirMatrix[0][0][2].M43 = _world_screen_list[0][0].Vertices[15].position.Z;// + originPosScreen.Z;
                _screenDirMatrix[0][0][3].M41 = _world_screen_list[0][0].Vertices[17].position.X;// + originPosScreen.X;
                _screenDirMatrix[0][0][3].M42 = _world_screen_list[0][0].Vertices[17].position.Y;// + originPosScreen.Y;
                _screenDirMatrix[0][0][3].M43 = _world_screen_list[0][0].Vertices[17].position.Z;// + originPosScreen.Z;
                //16//13//15//17 
                //8//9//10//11
                for (int i = 0; i < _screenDirMatrix[0][0].Length; i++)
                {
                    point3DCollection[0][0][i] = new Vector3(_screenDirMatrix[0][0][i].M41, _screenDirMatrix[0][0][i].M42, _screenDirMatrix[0][0][i].M43);
                }




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
                //_array_of_colors[0][4] = new Vector4(r, g, b, a);

                vertoffsetx = 0;
                vertoffsety = 0;
                vertoffsetz = 0;
                r = 0.05f;
                g = 0.05f;
                b = 0.05f;
                a = 1;
                _object_worldmatrix = Matrix.Identity;
                _object_worldmatrix.M41 = 0;
                _object_worldmatrix.M42 = 0;
                _object_worldmatrix.M43 = 0;
                _object_worldmatrix.M44 = 1;
                offsetPosX = sizeWidth01 * 2;
                offsetPosY = sizeheight01 * 2;
                offsetPosZ = sizedepth01 * 2;
                _world_screen_assets_list[0] = new SC_cube[1];
                _world_screen_assets_list[0][0] = new SC_cube();
                worldMatrix_instances_screen_assets[0] = new Matrix[1][];
                _world_screen_assets_list[0][0].Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, _screen_assets_size_x, _screen_assets_size_y, _screen_assets_size_z, new Vector4(r, g, b, a), _inst_screen_assets_x, _inst_screen_assets_y, _inst_screen_assets_z, Program.consoleHandle, _object_worldmatrix, 3, offsetPosX, offsetPosY, offsetPosZ, _thejitter_world, SCCoreSystems.sc_console.SC_console_directx.BodyTag._screen_assets, true, 0, 10, vertoffsetx, vertoffsety, vertoffsetz); //, "terrainGrassDirt.bmp" //0.00035f
                worldMatrix_instances_screen_assets[0][0] = new Matrix[_inst_screen_assets_x * _inst_screen_assets_y * _inst_screen_assets_z];
                for (int i = 0; i < worldMatrix_instances_screen_assets[0][0].Length; i++)
                {
                    worldMatrix_instances_screen_assets[0][0][i] = Matrix.Identity;
                }













                //lightpos = new Vector3(0, 100, 0);
                ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
                diffuseColour = new Vector4(1, 1, 1, 1);
                lightDirection = new Vector3(0, -1, -1);


                _DLightBuffer_cube[0] = new SCCoreSystems.SC_Graphics.SC_cube.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 0,
                    lightPosition = lightpos,
                    padding1 = 100
                };
                _DLightBuffer_grid[0] = new SCCoreSystems.SC_Graphics.SC_grid.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 0,
                    lightPosition = lightpos,
                    padding1 = 100
                };

                _DLightBuffer_containment_grid[0] = new SCCoreSystems.SC_Graphics.sc_containment_grid.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 0,
                    lightPosition = lightpos,
                    padding1 = 100
                };

                _DLightBuffer_voxel_cube[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 0,
                    lightPosition = lightpos,
                    padding1 = 100
                };

                _DLightBuffer_spectrum[0] = new SCCoreSystems.SC_Graphics.sc_spectrum.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 0,
                    lightPosition = lightpos,
                    padding1 = 100
                };

                _DLightBuffer_voxel_pchunk_cube[0] = new SCCoreSystems.SC_Graphics.sc_voxel_pchunk.DLightBuffer()
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

                _SC_modL_rght_hnd_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
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

                _SC_modL_lft_hnd_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 0,
                    lightPosition = lightpos,
                    padding1 = 100
                };

                _SC_modL_lft_hnd_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 0,
                    lightPosition = lightpos,
                    padding1 = 100
                };

                _SC_modL_rght_shldr_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 0,
                    lightPosition = lightpos,
                    padding1 = 100
                };

                _SC_modL_lft_shldr_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
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
                _SC_modL_lft_elbow_target_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 0,
                    lightPosition = lightpos,
                    padding1 = 100
                };
                _SC_modL_lft_elbow_target_two_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 0,
                    lightPosition = lightpos,
                    padding1 = 100
                };
                _SC_modL_lft_upper_arm_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 0,
                    lightPosition = lightpos,
                    padding1 = 100
                };
                _SC_modL_lft_lower_arm_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
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


                _basicTexture = new _sc_texture_loader();
                bool _hasinit1 = _basicTexture.Initialize(SC_console_directx.D3D.device, "../../../terrainGrassDirt.bmp");
                _sc_texture_loader _pink_texture = new _sc_texture_loader();
                _hasinit1 = _pink_texture.Initialize(SC_console_directx.D3D.device, "../../../1x1_pink_color.png");

                // Create the model object.
                Terrain = new DTerrainHeightMap();
                // Initialize the terrain object.
                if (!Terrain.Initialize(SC_console_directx.D3D.Device, "../../../heightmap01.bmp"))
                    Program.MessageBox((IntPtr)0, "fail load heightmap", "sccs message", 0);
            }
            catch (Exception ex)
            {
                Program.MessageBox((IntPtr)0, "TESt" + ex.ToString(), "sc core systems message", 0);
            }

            return _sc_jitter_tasks;
        }

        bool _set_fluid_point(ref JVector test)
        {
            //test = new JVector(5, 5, 5);
            test = new JVector(0, 0, 0);
            return _buoyancy_area[0].FluidBox.Contains(ref test) != JBBox.ContainmentType.Disjoint;
        }











        public class chunkData
        {
            public SC_instancedChunk.DInstanceType[] SC_instancedChunk_Instances;
            public SC_instancedChunk.DInstanceShipData[] SC_instancedChunk_InstancesFORWARD;
            public SC_instancedChunk.DInstanceShipData[] SC_instancedChunk_InstancesRIGHT;
            public SC_instancedChunk.DInstanceShipData[] SC_instancedChunk_InstancesUP;

            public Matrix worldMatrix;
            public Matrix viewMatrix;
            public Matrix projectionMatrix;
            public SC_instancedChunk_shader_final chunkShader;
            public DMatrixBuffer[] matrixBuffer;
            public DLightBuffer[] lightBuffer;
            public int switchForRender;
            public SC_instancedChunk.DInstanceType[] instancesIndex;
            public SC_instancedChunk.DInstanceType[] arrayOfDeVectorMapTemp;
            public SC_instancedChunk.DInstanceTypeTwo[] arrayOfDeVectorMapTempTwo;
            public SharpDX.Direct3D11.Buffer instanceBuffer;
            public SharpDX.Direct3D11.Buffer constantLightBuffer;
            public SharpDX.Direct3D11.Buffer vertexBuffer;
            public SharpDX.Direct3D11.Buffer constantMatrixPosBuffer;
            public int[][] arrayOfSomeMap;
            public SharpDX.Direct3D11.Buffer mapBuffer;

            public DInstanceTypeLocW[] instancesLocationW;
            public DInstanceTypeLocH[] instancesLocationH;
            public DInstanceTypeLocD[] instancesLocationD;

            public SharpDX.Direct3D11.Buffer InstanceBufferLocW;
            public SharpDX.Direct3D11.Buffer InstanceBufferLocH;
            public SharpDX.Direct3D11.Buffer InstanceBufferLocD;
            public SharpDX.Direct3D11.Buffer indexBuffer;


            public SharpDX.Direct3D11.Buffer InstanceRotationBufferFORWARD;
            public SharpDX.Direct3D11.Buffer InstanceRotationBufferRIGHT;
            public SharpDX.Direct3D11.Buffer InstanceRotationBufferUP;
        }



        public DInstanceTypeLocW[] instancesLocationW { get; set; }
        public DInstanceTypeLocH[] instancesLocationH { get; set; }
        public DInstanceTypeLocD[] instancesLocationD { get; set; }

        [StructLayout(LayoutKind.Sequential)]
        public struct DInstanceTypeLocW
        {
            public int index;
        };
        [StructLayout(LayoutKind.Sequential)]
        public struct DInstanceTypeLocH
        {
            public int index;
        };
        [StructLayout(LayoutKind.Sequential)]
        public struct DInstanceTypeLocD
        {
            public int index;
        };


        [StructLayout(LayoutKind.Explicit)]
        public struct DMatrixBuffer
        {
            [FieldOffset(0)]
            public Matrix world;
            [FieldOffset(64)]
            public Matrix view;
            [FieldOffset(128)]
            public Matrix proj;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct DLightBuffer
        {
            [FieldOffset(0)]
            public Vector4 ambientColor;
            [FieldOffset(16)]
            public Vector4 diffuseColor;
            [FieldOffset(32)]
            public Vector3 lightDirection;
            [FieldOffset(44)]
            public float padding; // Added extra padding so structure is a multiple of 16.
        }









        RigidBody grabBody;
        JVector hitNormal;

        public SC_message_object_jitter[][] loop_worlds(SC_message_object_jitter[][] _sc_jitter_tasks, Matrix originRoter, Matrix rotatingMatrixer, Matrix hmdmatrixRoter, Matrix hmd_matrixer, Matrix rotatingMatrixForPelviser, Matrix _rightTouchMatrixer, Matrix _leftTouchMatrixer)
        {


            if (Program.useArduinoOVRTouchKeymapper == 1)
            {

                var OculusTouchRightThumbstickButton = SC_Update.arduinoDIYOculusTouchArrayOfData[6];//  SC_console_directx.D3D.inputStateRTouch.Buttons;


                if (OculusTouchRightThumbstickButton == 0)
                {

                    if (gravity_swtch_counter >= 250) //75 original
                    {
                        //Console.WriteLine(OculusTouchRightThumbstickButton);
                        for (int xx = 0; xx < Program._physics_engine_instance_x; xx++)
                        {
                            for (int yy = 0; yy < Program._physics_engine_instance_y; yy++)
                            {
                                for (int zz = 0; zz < Program._physics_engine_instance_z; zz++)
                                {
                                    var indexer00 = xx + Program._physics_engine_instance_x * (yy + Program._physics_engine_instance_y * zz);

                                    try
                                    {
                                        for (int x = 0; x < Program.world_width; x++)
                                        {
                                            for (int y = 0; y < Program.world_height; y++)
                                            {
                                                for (int z = 0; z < Program.world_depth; z++)
                                                {
                                                    var indexer01 = x + Program.world_width * (y + Program.world_height * z);

                                                    object _some_data_00 = (object)_sc_jitter_tasks[indexer00][indexer01]._world_data[0];
                                                    World _jitter_world = (World)_some_data_00;
                                                    if (_jitter_world != null)
                                                    {
                                                        if (_jitter_world.RigidBodies.Count > 0)
                                                        {
                                                            if (gravity_swtch == 0 || gravity_swtch == 2)
                                                            {
                                                                _jitter_world.Gravity = new JVector(0, 0, 0);
                                                            }
                                                            else if (gravity_swtch == 1)
                                                            {
                                                                _jitter_world.Gravity = new JVector(0, -9.81f, 0);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                            }
                        }

                        if (gravity_swtch == 0 || gravity_swtch == 2)
                        {
                            gravity_swtch_counter = 0;
                            gravity_swtch = 1;
                        }
                        else if (gravity_swtch == 1)
                        {
                            gravity_swtch_counter = 0;
                            gravity_swtch = 2;
                        }

                    }
                }





                for (int xx = 0; xx < Program._physics_engine_instance_x; xx++)
                {
                    for (int yy = 0; yy < Program._physics_engine_instance_y; yy++)
                    {
                        for (int zz = 0; zz < Program._physics_engine_instance_z; zz++)
                        {
                            var indexer00 = xx + Program._physics_engine_instance_x * (yy + Program._physics_engine_instance_y * zz);

                            try
                            {
                                for (int x = 0; x < Program.world_width; x++)
                                {
                                    for (int y = 0; y < Program.world_height; y++)
                                    {
                                        for (int z = 0; z < Program.world_depth; z++)
                                        {
                                            var indexer01 = x + Program.world_width * (y + Program.world_height * z);

                                            object _some_data_00 = (object)_sc_jitter_tasks[indexer00][indexer01]._world_data[0];
                                            World _jitter_world = (World)_some_data_00;
                                            if (_jitter_world != null)
                                            {
                                                if (_jitter_world.RigidBodies.Count > 0)
                                                {
                                                    if (OculusTouchRightThumbstickButton == 0)
                                                    {
                                                        if (gravity_swtch_counter >= 75)
                                                        {
                                                            if (gravity_swtch == 0 || gravity_swtch == 2)
                                                            {
                                                                _jitter_world.Gravity = new JVector(0, 0, 0);
                                                                gravity_swtch_counter = 0;
                                                                gravity_swtch = 1;
                                                            }
                                                            else if (gravity_swtch == 1)
                                                            {
                                                                _jitter_world.Gravity = new JVector(0, -9.81f, 0);
                                                                gravity_swtch_counter = 0;
                                                                gravity_swtch = 2;
                                                            }
                                                        }
                                                    }


                                                    _inactive_counter_cubes = 0;
                                                    _inactive_counter_voxels = 0;



                                                    int _terrain_count = 0;
                                                    int _floor_count = 0;
                                                    int _voxel_cube_counter = 0;
                                                    int _non_voxel_cube_counter = 0;
                                                    int _non_voxel_cone_counter = 0;
                                                    int _non_voxel_cylinder_counter = 0;
                                                    int _non_voxel_capsule_counter = 0;
                                                    int _non_voxel_sphere_counter = 0;


                                                    int clothCounter = 0;



                                                    int p_l_shldr_count = 0;
                                                    int p_r_shldr_count = 0;


                                                    int p_r_hnd_count = 0;
                                                    int p_l_hnd_count = 0;
                                                    int p_l_lowerA_count = 0;
                                                    int p_r_lowerA_count = 0;
                                                    int p_l_upperA_count = 0;
                                                    int p_r_upperA_count = 0;
                                                    int p_l_target_count = 0;
                                                    int p_r_target_count = 0;
                                                    int p_l_target_two_count = 0;
                                                    int p_r_target_two_count = 0;





                                                    int p_r_foot_count = 0;
                                                    int p_l_foot_count = 0;
                                                    int p_l_lowerL_count = 0;
                                                    int p_r_lowerL_count = 0;
                                                    int p_l_upperL_count = 0;
                                                    int p_r_upperL_count = 0;
                                                    int p_l_target_knee_count = 0;
                                                    int p_r_target_knee_count = 0;
                                                    int p_l_target_knee_two_count = 0;
                                                    int p_r_target_knee_two_count = 0;










                                                    int p_torso_count = 0;
                                                    int p_pelvis_count = 0;
                                                    int p_head_count = 0;

                                                    int _screen_asset_counter = 0;
                                                    int _screen_counter = 0;

                                                    has_water_buo_effect = -1;

                                                    enumerator = _jitter_world.RigidBodies.GetEnumerator();

                                                    while (enumerator.MoveNext())
                                                    {
                                                        body = (RigidBody)enumerator.Current;

                                                        if (body != null && body.Tag != null) //&& body != _grab_rigid_data._body
                                                        {
                                                            //TO READD
                                                            //TO READD
                                                            /*if (OculusTouchRightThumbstickButton == 2) // release grabbed object
                                                            {
                                                                //touchRX = 0;
                                                                //touchRY = 0;
                                                                //touchRZ = 0;

                                                                //SC_Update.RotationX4Pelvis = 0;
                                                                //SC_Update.RotationY4Pelvis = 0;
                                                                //SC_Update.RotationZ4Pelvis = 0;

                                                                SC_Update.RotationGrabbedX = 0;
                                                                SC_Update.RotationGrabbedY = 0;
                                                                SC_Update.RotationGrabbedZ = 0;

                                                                _grab_rigid_data.position = Matrix.Identity;
                                                                _grab_rigid_data._body = null;
                                                                _some_frame_counter_grab_right_hand_swtch[0][0][0] = 0;
                                                            }*/
                                                            //TO READD
                                                            //TO READD

                                                            if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedScreen ||
                                                                (SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCube ||
                                                                (SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCone ||
                                                                (SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCylinder ||
                                                                (SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCapsule ||
                                                                (SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedSphere)
                                                            {


                                                                /*if (_some_frame_counter_grab_right_hand_swtch[0][0][0] == 0) //keep counting up when switch is at 0
                                                                {
                                                                    if (_some_frame_counter_grab_right_hand[0][0][0] > 100)
                                                                    {
                                                                        SC_Update.RotationGrabbedX = 0;
                                                                        SC_Update.RotationGrabbedY = 0;
                                                                        SC_Update.RotationGrabbedZ = 0;
                                                                        Vector3 current_handposR = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41,
                                                                                                               _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42,
                                                                                                               _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);

                                                                        Matrix tempmatter = _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos;
                                                                        Quaternion quater;
                                                                        Quaternion.RotationMatrix(ref tempmatter, out quater);

                                                                        var centerPosRight = new SharpDX.Vector3(current_handposR.X, current_handposR.Y, current_handposR.Z);
                                                                        var rayDirForward = sc_maths._getDirection(SharpDX.Vector3.ForwardRH, quater);
                                                                        var _ray = new SharpDX.Ray(centerPosRight, rayDirForward);

                                                                        float fraction;

                                                                        JVector ray = new JVector(_ray.Direction.X, _ray.Direction.Y, _ray.Direction.Z);
                                                                        //JVector camp = Conversion.ToJitterVector(Camera.Position);
                                                                        ray = JVector.Normalize(ray) * 0.15f; // 0.25f

                                                                        var camp = new JVector(centerPosRight.X, centerPosRight.Y, centerPosRight.Z);

                                                                        bool resulter = _jitter_world.CollisionSystem.Raycast(camp, ray, RaycastCallback, out grabBody, out hitNormal, out fraction);

                                                                        if (buttonPressedOculusTouchRight == 1 && grabBody != _player_rght_hnd[0][0]._arrayOfInstances[0].transform.Component.rigidbody)
                                                                        {
                                                                            if (resulter)
                                                                            {
                                                                                JVector hitpointer = camp + fraction * ray;
                                                                                _grab_rigid_data.grabHitPoint = new Vector3(hitpointer.X, hitpointer.Y, hitpointer.Z);
                                                                                _grab_rigid_data.rayGrabDir = new Vector3(_ray.Direction.X, _ray.Direction.Y, _ray.Direction.Z);
                                                                                _grab_rigid_data.rayGrabDirLength = _grab_rigid_data.rayGrabDir.Length();

                                                                                Matrix.Translation(grabBody.Position.X, grabBody.Position.Y, grabBody.Position.Z, out translationMatrix);
                                                                                quatterer = JQuaternion.CreateFromMatrix(grabBody.Orientation);
                                                                                tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                                Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                                Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);

                                                                                var dirToGrabber = new Vector3(grabBody.Position.X, grabBody.Position.Y, grabBody.Position.Z) - current_handposR; //_grab_rigid_data.grabHitPoint
                                                                                //var dirToGrabber = new Vector3(hitpointer.X, hitpointer.Y, hitpointer.Z) - current_handposR; //_grab_rigid_data.grabHitPoint

                                                                                var rCenter = new SharpDX.Vector3(_player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION.M41, _player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION.M42, _player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION.M43);
                                                                                finalRotationMatrix.M41 = 0;
                                                                                finalRotationMatrix.M42 = 0;
                                                                                finalRotationMatrix.M43 = 0;
                                                                                Matrix centerPoser = _rightTouchMatrixer;// _player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION;
                                                                                centerPoser *= finalRotationMatrix;
                                                                                centerPoser.M41 = rCenter.X;
                                                                                centerPoser.M42 = rCenter.Y;
                                                                                centerPoser.M43 = rCenter.Z;
                                                                                _player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION = centerPoser;

                                                                                var differX = grabBody.Position.X - _player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION.M41;
                                                                                var differY = grabBody.Position.Y - _player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION.M42;
                                                                                var differZ = grabBody.Position.Z - _player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION.M43;

                                                                                _grab_rigid_data.grabHitPointLength = dirToGrabber.Length();

                                                                                _grab_rigid_data._body = grabBody;
                                                                                _grab_rigid_data.position = translationMatrix;

                                                                                _grab_rigid_data.dirDiffX = (differX);// dirToGrabber.X);
                                                                                _grab_rigid_data.dirDiffY = (differY);//Math.Abs(dirToGrabber.Y);
                                                                                _grab_rigid_data.dirDiffZ = (differZ);//Math.Abs(dirToGrabber.Z);

                                                                                Matrix grabbedBodyMatrix = translationMatrix;
                                                                                var xq = quatterer.X;
                                                                                var yq = quatterer.Y;
                                                                                var zq = quatterer.Z;
                                                                                var wq = quatterer.W;

                                                                                var pitcha = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
                                                                                var yawa = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
                                                                                var rolla = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);

                                                                                Matrix handMatrix = _rightTouchMatrixer;//_player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION * finalRotationMatrix; //_rightTouchMatrix * OriginRot * RotatingMatrix * RotatingMatrixForPelvis * hmdmatrixRot_


                                                                                Quaternion.RotationMatrix(ref handMatrix, out quater);
                                                                                xq = quater.X;
                                                                                yq = quater.Y;
                                                                                zq = quater.Z;
                                                                                wq = quater.W;

                                                                                pitcha = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
                                                                                yawa = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
                                                                                rolla = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);


                                                                                touchRX = pitcha;
                                                                                touchRY = yawa;
                                                                                touchRZ = rolla;


                                                                                grabrotX = pitcha;
                                                                                grabrotY = yawa;
                                                                                grabrotZ = rolla;

                                                                                double currentGrabrotDiffX = (pitcha);
                                                                                double currentGrabrotDiffY = (yawa);
                                                                                double currentGrabrotDiffZ = (rolla);


                                                                                Quaternion.RotationMatrix(ref grabbedBodyMatrix, out quater);
                                                                                xq = quater.X;
                                                                                yq = quater.Y;
                                                                                zq = quater.Z;
                                                                                wq = quater.W;
                                                                                pitcha = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
                                                                                yawa = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
                                                                                rolla = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);






                                                                                grabrotDiffx = pitcha - currentGrabrotDiffX;
                                                                                grabrotDiffy = yawa - currentGrabrotDiffY;
                                                                                grabrotDiffz = rolla - currentGrabrotDiffZ;

                                                                                dirToGrabber.Normalize();
                                                                                _grab_rigid_data.directionToGrabber = dirToGrabber;




                                                                                pitchTouchRer = (float)(Math.PI * (SC_Update.RotationX4Pelvis) / 180.0f);
                                                                                yawTouchRer = (float)(Math.PI * (SC_Update.RotationY4Pelvis) / 180.0f);
                                                                                rollTouchRer = (float)(Math.PI * (SC_Update.RotationZ4Pelvis) / 180.0f);

                                                                                rotMatForPelvis = SharpDX.Matrix.RotationYawPitchRoll((float)yawTouchRer, (float)pitchTouchRer, (float)rollTouchRer);

                                                                                totalDiffX = 0;
                                                                                totalDiffY = 0;
                                                                                totalDiffZ = 0;





                                                                                _some_frame_counter_grab_right_hand_swtch[0][0][0] = 1;
                                                                                _some_frame_counter_grab_right_hand[0][0][0] = 0;
                                                                            }
                                                                        }
                                                                    }

                                                                }*/

                                                                /*if (_distanceConstraintRight != null)
                                                                {
                                                                    _jitter_world.RemoveConstraint(_distanceConstraintRight);
                                                                }

                                                                //JVector lanchor = new JVector(current_handposR.X, current_handposR.Y, current_handposR.Z) - grabBody.Position; //hitPoint
                                                                //lanchor = JVector.Transform(lanchor, JMatrix.Transpose(grabBody.Orientation));

                                                                _distanceConstraintRight = new PointPointDistance(_player_rght_hnd[0][0]._arrayOfInstances[0].transform.Component.rigidbody, grabBody, camp, hitPoint);

                                                                _distanceConstraintRight.Softness = 0.0001f;
                                                                _distanceConstraintRight.BiasFactor = 0.1f;
                                                                _distanceConstraintRight.Distance = 0.001f;

                                                                _jitter_world.AddConstraint(_distanceConstraintRight);

                                                                _lastFraction = fraction;
                                                                _lastRigidGrab = grabBody;
                                                                _jitter_world.RemoveConstraint(_distanceConstraintRight);
                                                                */

                                                                if (_some_frame_counter_grab_right_hand_swtch[0][0][0] == 1) //keep counting up when switch is at 0
                                                                {
                                                                    /*if (_grab_rigid_data._body != null)
                                                                    {
                                                                        Vector3 current_handposR = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);

                                                                        Matrix tempmatter = _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos;
                                                                        Quaternion quater;
                                                                        Quaternion.RotationMatrix(ref tempmatter, out quater);
                                                                        var rayDirForward = sc_maths._getDirection(SharpDX.Vector3.ForwardRH, quater);
                                                                        var rayDirUp = sc_maths._getDirection(SharpDX.Vector3.Up, quater);
                                                                        var rayDirRight = sc_maths._getDirection(SharpDX.Vector3.Right, quater);

                                                                        Vector3 movingPointer = current_handposR + (rayDirForward * _grab_rigid_data.dirDiffZ);
                                                                        //movingPointer = movingPointer + (rayDirUp * _grab_rigid_data.dirDiffY);
                                                                        //movingPointer = movingPointer + (rayDirRight * _grab_rigid_data.dirDiffX);

                                                                        Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                        quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                        tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                        Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                        Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);

                                                                        //translationMatrix.M41 = movingPointer.X;
                                                                        //translationMatrix.M42 = movingPointer.Y;
                                                                        //translationMatrix.M43 = movingPointer.Z;
                                                                        body.Position = new JVector(current_handposR.X, current_handposR.Y, current_handposR.Z);

                                                                        //JQuaternion _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                                                        //var matrixIn = JMatrix.CreateFromQuaternion(_other_quat);
                                                                    }
                                                                    else
                                                                    {
                                                                        _some_frame_counter_grab_right_hand_swtch[0][0][0] = 0;
                                                                    }*/
                                                                }



















                                                                /*if (buttonPressedOculusTouchRight != 0)
                                                                {
                                                                    if (buttonPressedOculusTouchRight == 4)
                                                                    {
                                                                        if (body.IsActive == false)
                                                                        {
                                                                            body.AllowDeactivation = true;
                                                                            body.IsActive = true;
                                                                            body.AllowDeactivation = false;
                                                                        }
                                                                    }
                                                                }





                                                                has_water_buo_effect = -1;

                                                                if (_buo.FluidBox.Contains(body.BoundingBox) != JBBox.ContainmentType.Disjoint)
                                                                {
                                                                    _buo.fluidArea = null;
                                                                    containsCoord = false;

                                                                    JVector[] positions = _buo.samples[body.Shape];

                                                                    float frac = 0.0f;

                                                                    JVector currentCoord = JVector.Zero;
                                                                    for (int i = 0; i < positions.Length; i++)
                                                                    {
                                                                        currentCoord = JVector.Transform(positions[i], body.Orientation);
                                                                        currentCoord = JVector.Add(currentCoord, body.Position);

                                                                        containsCoord = _buo.FluidBox.Contains(ref currentCoord) != JBBox.ContainmentType.Disjoint;

                                                                        if (containsCoord)
                                                                        {
                                                                            has_water_buo_effect = 1;
                                                                            body.AddForce((1.0f / positions.Length) * body.Mass * _buo.Flow);
                                                                            body.AddForce(-(1.0f / positions.Length) * body.Shape.Mass * _buo.Density * _buo._world.Gravity, currentCoord);
                                                                            frac += 1.0f / positions.Length;
                                                                        }
                                                                    }

                                                                    var buo_modded = _buo.Damping;
                                                                    body.AngularVelocity *= buo_modded * 1.075f; //1.01f
                                                                    body.LinearVelocity *= buo_modded;
                                                                }*/
                                                            }

                                                            if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.Terrain)
                                                            {
                                                                Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                worldMatrix_instances_terrain[0][0][0] = translationMatrix;
                                                                _terrain_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag._floor)
                                                            {
                                                                Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                worldMatrix_instances_floor[0][0][0] = translationMatrix;
                                                                _floor_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCube ||
                                                                     (SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCone ||
                                                                     (SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCylinder ||
                                                                     (SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCapsule ||
                                                                     (SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedSphere)
                                                            {
                                                                //grabbedBodyMatrix = Matrix.Identity;
                                                                Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);

                                                                if (body == _grab_rigid_data._body)
                                                                {
                                                                    Matrix matrixerer = Matrix.Identity;

                                                                    grabbedBodyMatrix = _grab_rigid_data.position;
                                                                    Matrix handMatrix = _player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION * finalRotationMatrix;// _rightTouchMatrix * OriginRot * RotatingMatrix * RotatingMatrixForPelvis * hmdmatrixRot_;
                                                                    var MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M43);
                                                                    Matrix someMatRight = _rightTouchMatrix;// * OriginRot * RotatingMatrix * RotatingMatrixForPelvis * hmdmatrixRot_;
                                                                    someMatRight.M41 = handPoseRight.Position.X + MOVINGPOINTER.X;
                                                                    someMatRight.M42 = handPoseRight.Position.Y;// + MOVINGPOINTER.Y;
                                                                    someMatRight.M43 = handPoseRight.Position.Z + MOVINGPOINTER.Z;
                                                                    var diffNormPosX = (MOVINGPOINTER.X) - someMatRight.M41;
                                                                    var diffNormPosY = (MOVINGPOINTER.Y) - someMatRight.M42;
                                                                    var diffNormPosZ = (MOVINGPOINTER.Z) - someMatRight.M43;
                                                                    MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_right * (diffNormPosX));
                                                                    MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_up * (diffNormPosY));
                                                                    MOVINGPOINTER = MOVINGPOINTER + (current_rotation_of_torso_pivot_forward * (diffNormPosZ));

                                                                    MOVINGPOINTER.X += SC_Update.OFFSETPOS.X;
                                                                    MOVINGPOINTER.Y += SC_Update.OFFSETPOS.Y;
                                                                    MOVINGPOINTER.Z += SC_Update.OFFSETPOS.Z;

                                                                    Quaternion quater;
                                                                    Quaternion.RotationMatrix(ref handMatrix, out quater);

                                                                    var rayDirForward = sc_maths._getDirection(SharpDX.Vector3.ForwardRH, quater);
                                                                    rayDirForward.Normalize();
                                                                    var rayDirUp = sc_maths._getDirection(SharpDX.Vector3.Up, quater);
                                                                    rayDirUp.Normalize();
                                                                    var rayDirRight = sc_maths._getDirection(SharpDX.Vector3.Right, quater);
                                                                    rayDirRight.Normalize();


                                                                    var current_handposRR = new Vector3(MOVINGPOINTER.X,
                                                                                                        MOVINGPOINTER.Y,
                                                                                                        MOVINGPOINTER.Z);

                                                                    MOVINGPOINTER = current_handposRR + (rayDirForward * _grab_rigid_data.grabHitPointLength);

                                                                    var pitch = (float)(Math.PI * (-SC_Update.RotationGrabbedX) / 180.0f);
                                                                    var yaw = (float)(Math.PI * (SC_Update.RotationGrabbedY) / 180.0f);
                                                                    var roll = (float)(Math.PI * (SC_Update.RotationGrabbedZ) / 180.0f);
                                                                    var rotatingMatrixForPelvisReset = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                                                                    matrixerer = grabbedBodyMatrix * rotatingMatrixForPelvisReset * originRoter * rotatingMatrixer; //grabbedBodyMatrix * rotatingMatrixForPelvisReset * originRoter * rotatingMatrixer
                                                                    matrixerer.M41 = MOVINGPOINTER.X;
                                                                    matrixerer.M42 = MOVINGPOINTER.Y;
                                                                    matrixerer.M43 = MOVINGPOINTER.Z;
                                                                    matrixerer.M44 = 1;

                                                                    body.Position = new JVector(MOVINGPOINTER.X, MOVINGPOINTER.Y, MOVINGPOINTER.Z);
                                                                    Quaternion.RotationMatrix(ref matrixerer, out quater);
                                                                    JQuaternion _other_quat = new JQuaternion(quater.X, quater.Y, quater.Z, quater.W);
                                                                    var matrixIn = JMatrix.CreateFromQuaternion(_other_quat);
                                                                    body.Orientation = matrixIn;
                                                                    translationMatrix = matrixerer;
                                                                }
                                                                else
                                                                {

                                                                }

                                                                int tempIndex = 0;

                                                                if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCube)
                                                                {
                                                                    tempIndex = _non_voxel_cube_counter;
                                                                }
                                                                else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCone)
                                                                {
                                                                    tempIndex = _non_voxel_cone_counter;
                                                                }
                                                                else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCylinder)
                                                                {
                                                                    tempIndex = _non_voxel_cylinder_counter;
                                                                }
                                                                else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCapsule)
                                                                {
                                                                    tempIndex = _non_voxel_capsule_counter;
                                                                }
                                                                else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedSphere)
                                                                {
                                                                    tempIndex = _non_voxel_sphere_counter;
                                                                }


                                                                var somePosLastXExtra = _array_of_last_frame_cube_pos[indexer00][indexer01][tempIndex].X;
                                                                var somePosLastYExtra = _array_of_last_frame_cube_pos[indexer00][indexer01][tempIndex].Y;
                                                                var somePosLastZExtra = _array_of_last_frame_cube_pos[indexer00][indexer01][tempIndex].Z;

                                                                var rigidXExtra = Math.Round(somePosLastXExtra * 10) / 10;
                                                                var rigidYExtra = Math.Round(somePosLastYExtra * 10) / 10;
                                                                var rigidZExtra = Math.Round(somePosLastZExtra * 10) / 10;

                                                                var diffXExtra = Math.Abs(somePosLastXExtra - rigidXExtra);
                                                                var diffYExtra = Math.Abs(somePosLastYExtra - rigidYExtra);
                                                                var diffZExtra = Math.Abs(somePosLastZExtra - rigidZExtra);

                                                                //Console.WriteLine(Math.Round(somePosLastX * 10) / 10);

                                                                if (Math.Round(diffXExtra * 0.1f) * 10 < 0.00001f && //0.0000001f
                                                                    Math.Round(diffYExtra * 0.1f) * 10 < 0.00001f &&
                                                                    Math.Round(diffZExtra * 0.1f) * 10 < 0.00001f)
                                                                {
                                                                    var minvelocities = 0.00123f;

                                                                    if (has_water_buo_effect != 1)
                                                                    {
                                                                        minvelocities = 0.00123f;
                                                                    }
                                                                    else
                                                                    {
                                                                        minvelocities = 0.000923f;
                                                                    }

                                                                    JVector currentLinearVelExtra = body.LinearVelocity;
                                                                    JVector currentAngularVelExtra = body.AngularVelocity;
                                                                    if (currentLinearVelExtra.Length() < minvelocities && currentAngularVelExtra.Length() < minvelocities) //0.00035f == 400 approx => 0.00075f == 400 approx
                                                                    {
                                                                        /*if (body.CollisionIsland != null)
                                                                        {
                                                                            if (body.CollisionIsland.Bodies != null)
                                                                            {
                                                                                if (body.CollisionIsland.Bodies.Count <= 0)
                                                                                {
                                                                                    if (body.IsActive == true)
                                                                                    {
                                                                                        body.AllowDeactivation = true;
                                                                                        body.IsActive = false;
                                                                                        //body.AllowDeactivation = false;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }*/
                                                                    }
                                                                    else
                                                                    {
                                                                        /*if (body.IsActive == false)
                                                                        {
                                                                            body.AllowDeactivation = true;
                                                                            body.IsActive = true;
                                                                            body.AllowDeactivation = false;
                                                                        }*/
                                                                    }
                                                                }

                                                                if (SC_Update.handTriggerRight[1] >= 0.001f)
                                                                {
                                                                    if (!body.IsActive)
                                                                    {
                                                                        body.AllowDeactivation = true;
                                                                        body.IsActive = true;
                                                                        body.AllowDeactivation = false;
                                                                    }

                                                                    var dirToRightHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                    var lengthofdir = dirToRightHand.Length();

                                                                    //var moveRightHand = new JVector(_player_lft_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M43) - new JVector(_player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M41, _player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M42, _player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M43);

                                                                    if (dirToRightHand != null)
                                                                    {
                                                                        dirToRightHand.Normalize();
                                                                        lh_attract_force = new JVector(dirToRightHand.X, dirToRightHand.Y, dirToRightHand.Z) * 1;

                                                                        if (lh_attract_force != JVector.Zero && lh_attract_force != null && lh_attract_force.Length() > 0 && body != _grab_rigid_data._body)
                                                                        {
                                                                            //MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
                                                                            //MessageBox((IntPtr)0, "" + "not null force", "Oculus Error", 0);
                                                                            lh_attract_force.Normalize();

                                                                            if (has_water_buo_effect == 1)
                                                                            {
                                                                                lh_attract_force *= force_4_screen * 100;
                                                                            }
                                                                            else
                                                                            {
                                                                                lh_attract_force *= force_4_screen * 1;
                                                                            }

                                                                            //0.0045f


                                                                            //lh_attract_force *= force_4_screen*1000;
                                                                            body.LinearVelocity += lh_attract_force;
                                                                            body.AddForce(lh_attract_force);
                                                                            //body.AddTorque(force);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        //Console.WriteLine("null dir");
                                                                        Program.MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
                                                                    }

                                                                }

                                                                if (!body.IsActive)
                                                                {
                                                                    _inactive_counter_cubes++;
                                                                }

                                                                if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCube)
                                                                {
                                                                    worldMatrix_instances_cubes[indexer00][indexer01][tempIndex] = translationMatrix;
                                                                    _array_of_last_frame_cube_pos[indexer00][indexer01][tempIndex] = new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                    _non_voxel_cube_counter++;
                                                                }
                                                                else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCone)
                                                                {
                                                                    worldMatrix_instances_cone[indexer00][indexer01][tempIndex] = translationMatrix;
                                                                    _array_of_last_frame_cone_pos[indexer00][indexer01][tempIndex] = new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                    _non_voxel_cone_counter++;
                                                                }
                                                                else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCylinder)
                                                                {
                                                                    worldMatrix_instances_cylinder[indexer00][indexer01][tempIndex] = translationMatrix;
                                                                    _array_of_last_frame_cylinder_pos[indexer00][indexer01][tempIndex] = new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                    _non_voxel_cylinder_counter++;
                                                                }
                                                                else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCapsule)
                                                                {
                                                                    worldMatrix_instances_capsule[indexer00][indexer01][tempIndex] = translationMatrix;
                                                                    _array_of_last_frame_capsule_pos[indexer00][indexer01][tempIndex] = new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                    _non_voxel_capsule_counter++;
                                                                }
                                                                else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedSphere)
                                                                {
                                                                    worldMatrix_instances_sphere[indexer00][indexer01][tempIndex] = translationMatrix;
                                                                    _array_of_last_frame_sphere_pos[indexer00][indexer01][tempIndex] = new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                    _non_voxel_sphere_counter++;
                                                                }
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.sc_perko_voxel)
                                                            {
                                                                var somePosLastXExtra = _array_of_last_frame_voxel_pos[indexer00][indexer01][_voxel_cube_counter].X;
                                                                var somePosLastYExtra = _array_of_last_frame_voxel_pos[indexer00][indexer01][_voxel_cube_counter].Y;
                                                                var somePosLastZExtra = _array_of_last_frame_voxel_pos[indexer00][indexer01][_voxel_cube_counter].Z;

                                                                var rigidXExtra = Math.Round(somePosLastXExtra * 10) / 10;
                                                                var rigidYExtra = Math.Round(somePosLastYExtra * 10) / 10;
                                                                var rigidZExtra = Math.Round(somePosLastZExtra * 10) / 10;

                                                                var diffXExtra = Math.Abs(somePosLastXExtra - rigidXExtra);
                                                                var diffYExtra = Math.Abs(somePosLastYExtra - rigidYExtra);
                                                                var diffZExtra = Math.Abs(somePosLastZExtra - rigidZExtra);

                                                                //Console.WriteLine(Math.Round(somePosLastX * 10) / 10);

                                                                if (Math.Round(diffXExtra * 0.1f) * 10 < 0.00001f && //0.0000001f
                                                                    Math.Round(diffYExtra * 0.1f) * 10 < 0.00001f &&
                                                                    Math.Round(diffZExtra * 0.1f) * 10 < 0.00001f)
                                                                {
                                                                    float minvelocities = 0.00123f;
                                                                    if (has_water_buo_effect != 1)
                                                                    {
                                                                        minvelocities = 0.00123f;
                                                                    }
                                                                    else
                                                                    {
                                                                        minvelocities = 0.000923f;
                                                                    }

                                                                    JVector currentLinearVelExtra = body.LinearVelocity;
                                                                    JVector currentAngularVelExtra = body.AngularVelocity;
                                                                    if (currentLinearVelExtra.Length() < minvelocities && currentAngularVelExtra.Length() < minvelocities) //0.00035f == 400 approx => 0.00075f == 400 approx
                                                                    {
                                                                        if (body.CollisionIsland != null)
                                                                        {
                                                                            /*if (body.CollisionIsland.Bodies != null)
                                                                            {
                                                                                if (body.CollisionIsland.Bodies.Count <= 0)
                                                                                {
                                                                                    if (body.IsActive == true)
                                                                                    {
                                                                                        body.AllowDeactivation = true;
                                                                                        body.IsActive = false;
                                                                                        //body.AllowDeactivation = false;
                                                                                    }
                                                                                }
                                                                            }*/
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        /*if (body.IsActive == false)
                                                                        {
                                                                            body.AllowDeactivation = true;
                                                                            body.IsActive = true;
                                                                            body.AllowDeactivation = false;
                                                                        }*/
                                                                    }
                                                                }

                                                                if (SC_Update.handTriggerRight[1] >= 0.001f)
                                                                {
                                                                    if (!body.IsActive)
                                                                    {
                                                                        body.AllowDeactivation = true;
                                                                        body.IsActive = true;
                                                                        body.AllowDeactivation = false;
                                                                    }

                                                                    var dirToRightHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                    var lengthofdir = dirToRightHand.Length();

                                                                    //var moveRightHand = new JVector(_player_lft_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M43) - new JVector(_player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M41, _player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M42, _player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M43);

                                                                    if (dirToRightHand != null)
                                                                    {
                                                                        dirToRightHand.Normalize();
                                                                        lh_attract_force = new JVector(dirToRightHand.X, dirToRightHand.Y, dirToRightHand.Z) * 1;

                                                                        if (lh_attract_force != JVector.Zero && lh_attract_force != null && lh_attract_force.Length() > 0)
                                                                        {
                                                                            //MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
                                                                            //MessageBox((IntPtr)0, "" + "not null force", "Oculus Error", 0);
                                                                            lh_attract_force.Normalize();

                                                                            if (has_water_buo_effect == 1)
                                                                            {
                                                                                lh_attract_force *= force_4_screen * 100;
                                                                            }
                                                                            else
                                                                            {
                                                                                lh_attract_force *= force_4_screen * 1;
                                                                            }

                                                                            //0.0045f

                                                                            //lh_attract_force *= force_4_screen*1000;
                                                                            body.LinearVelocity += lh_attract_force;
                                                                            body.AddForce(lh_attract_force);
                                                                            //body.AddTorque(force);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        //Console.WriteLine("null dir");
                                                                        Program.MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
                                                                    }
                                                                }

                                                                worldMatrix_instances_voxel_cube[indexer00][indexer01][_voxel_cube_counter] = translationMatrix;
                                                                _array_of_last_frame_voxel_pos[indexer00][indexer01][_voxel_cube_counter] = new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                if (!body.IsActive)
                                                                {
                                                                    _inactive_counter_voxels++;
                                                                }


                                                                _voxel_cube_counter++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerHandRight)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_hand[0][0][p_r_hnd_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_hnd_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerHandLeft)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_hand[0][0][p_l_hnd_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_hnd_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerTorso)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_torso[0][0][p_torso_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_torso_count++;
                                                            }

                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerPelvis)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_pelvis[0][0][p_pelvis_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_pelvis_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerShoulderRight)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_shoulder[0][0][p_r_shldr_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_shldr_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerShoulderLeft)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_shoulder[0][0][p_l_shldr_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_shldr_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerHead)
                                                            {

                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_head[0][0][p_head_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);

                                                                p_head_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerArmRight)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_lowerarm[0][0][p_r_lowerA_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_lowerA_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerArmLeft)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_lowerarm[0][0][p_l_lowerA_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_lowerA_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmRight)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_upperarm[0][0][p_r_upperA_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_upperA_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmLeft)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_upperarm[0][0][p_l_upperA_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_upperA_count++;

                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLeftElbowTarget)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_elbow_target[0][0][p_l_target_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_target_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightElbowTarget)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_elbow_target[0][0][p_r_target_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_target_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLeftElbowTargettwo)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_elbow_target_two[0][0][p_l_target_two_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_target_two_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightElbowTargettwo)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_elbow_target_two[0][0][p_l_target_two_count]; ;
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_target_two_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerLegLeft)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_lower_leg[0][0][p_l_lowerL_count]; ;
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_lowerL_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperLegLeft)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_upper_leg[0][0][p_l_upperL_count]; ;
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_upperL_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerLegRight)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_lower_leg[0][0][p_r_lowerL_count]; ;
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_lowerL_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperLegRight)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_upper_leg[0][0][p_r_upperL_count]; ;
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_upperL_count++;
                                                            }

                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerFootLeft)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_foot[0][0][p_l_foot_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_foot_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerFootRight)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_foot[0][0][p_r_foot_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_foot_count++;
                                                            }


                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLeftTargetKnee)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_target_knee[0][0][p_l_target_knee_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_target_knee_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightTargetKnee)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_target_knee[0][0][p_r_target_knee_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_target_knee_count++;
                                                            }

                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLeftTargettwoKnee)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_target_two_knee[0][0][p_l_target_knee_two_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_target_knee_two_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightTargettwoKnee)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_target_two_knee[0][0][p_r_target_knee_two_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_target_knee_two_count++;
                                                            }

                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedScreen)
                                                            {
                                                                Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                worldMatrix_instances_screens[0][0][0] = translationMatrix;
                                                                _world_screen_list[0][0]._arrayOfInstances[0].current_pos = translationMatrix;

                                                                worldMatrix_instances_containment_grid_screen[0][0][0] = translationMatrix;
                                                                _world_containment_grid_screen[0][0]._arrayOfInstances[0].current_pos = translationMatrix;

                                                                _screen_counter++;

                                                                /*if (_has_locked_screen_pos == 0)
                                                                {
                                                                    if (!body.IsStatic)
                                                                    {
                                                                        if (SC_Update.handTriggerLeft[0] >= 0.001f)
                                                                        {
                                                                            body.IsActive = true;

                                                                            Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                            quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                            tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                            Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                            Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                            var dirToRightHand = new Vector3(_player_lft_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_lft_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_lft_hnd[0][0]._arrayOfInstances[0].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                            var lengthofdir = dirToRightHand.Length();

                                                                            //var moveRightHand = new JVector(_player_lft_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M43) - new JVector(_player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M41, _player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M42, _player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M43);

                                                                            if (dirToRightHand != null)
                                                                            {
                                                                                dirToRightHand.Normalize();
                                                                                lh_attract_force = new JVector(dirToRightHand.X, dirToRightHand.Y, dirToRightHand.Z) * 1;

                                                                                if (lh_attract_force != JVector.Zero && lh_attract_force != null && lh_attract_force.Length() > 0)
                                                                                {
                                                                                    //MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
                                                                                    //MessageBox((IntPtr)0, "" + "not null force", "Oculus Error", 0);
                                                                                    lh_attract_force.Normalize();

                                                                                    if (has_water_buo_effect == 1)
                                                                                    {
                                                                                        lh_attract_force *= force_4_screen * 100;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        lh_attract_force *= force_4_screen * 1;
                                                                                    }

                                                                                    //0.0045f

                                                                                    //lh_attract_force *= force_4_screen*1000;
                                                                                    body.LinearVelocity += lh_attract_force;
                                                                                    body.AddForce(lh_attract_force);
                                                                                    //body.AddTorque(force);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                //Console.WriteLine("null dir");
                                                                                Program.MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
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
                                                                    /*Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                    quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                    tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                    Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                    Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                    worldMatrix_instances_screens[0][0][0] = translationMatrix;
                                                                    _world_screen_list[0][0]._arrayOfInstances[0].current_pos = translationMatrix;

                                                                }
                                                                else
                                                                {
                                                                    if (_tier_logic_swtch_lock_screen == 0)
                                                                    {
                                                                        if (had_locked_screen == 1)
                                                                        {
                                                                            body.AngularVelocity = JVector.Zero;
                                                                            body.LinearVelocity = JVector.Zero;

                                                                            worldMatrix_instances_screens[0][0][0] = _current_screen_pos;
                                                                            _world_screen_list[0][0]._arrayOfInstances[0].current_pos = _current_screen_pos;// worldMatrix_instances_screens[0][0];                                             

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

                                                                        worldMatrix_instances_screens[0][0][0] = _current_screen_pos;
                                                                        _world_screen_list[0][0]._arrayOfInstances[0].current_pos = _current_screen_pos;// worldMatrix_instances_screens[0][0];
                                                                    }
                                                                }




                                                                Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                worldMatrix_instances_screens[0][0][0] = translationMatrix;
                                                                _world_screen_list[0][0]._arrayOfInstances[0].current_pos = translationMatrix;// worldMatrix_instances_screens[0][0];

                                                                worldMatrix_instances_containment_grid_screen[0][0][0] = translationMatrix;// _world_screen_list[0][0]._arrayOfInstances[0].current_pos;
                                                                _world_containment_grid_screen[0][0]._arrayOfInstances[0].current_pos = translationMatrix;// _world_screen_list[0][0]._arrayOfInstances[0].current_pos;
                                                                _screen_counter++;*/
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.pseudoCloth)
                                                            {
                                                                /*var _tempMatrix = Matrix.Identity;

                                                                Matrix translationMatrix = Matrix.Identity;
                                                                Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);

                                                                Matrix rotationMatrix = Matrix.Identity;


                                                                JQuaternion quatterer = JQuaternion.CreateFromMatrix(body.Orientation);

                                                                Quaternion tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

                                                                Matrix.RotationQuaternion(ref tester, out rotationMatrix);

                                                                Matrix.Multiply(ref rotationMatrix, ref translationMatrix, out _tempMatrix);

                                                                var pos = new Vector3(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                                                                _tempMatrix = WorldMatrix;
                                                                _tempMatrix.M41 = pos.X;
                                                                _tempMatrix.M42 = pos.Y;
                                                                _tempMatrix.M43 = pos.Z;

                                                                Quaternion temp_quat;
                                                                Quaternion.RotationMatrix(ref _tempMatrix, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;

                                                                sc_jitter_cloth._cube._arrayOfInstances[clothCounter].current_pos = _tempMatrix;
                                                                //_arrayOfClothCubes[clothCounter]._arrayOfInstances[clothCounter].current_pos = _tempMatrix;
                                                                worldMatrix_Cloth_instances[clothCounter] = _tempMatrix;

                                                                clothCounter++;*/
                                                            }

                                                        }
                                                    }
                                                }
                                                //Console.Title = Program._MainWindow_name + " ### " + " Made by Steve Chassé" + " ### " + " => " + "disabled cubes: " + _inactive_counter_cubes + " disabled voxels: " + _inactive_counter_voxels;
                                                Console.Title = Program._MainWindow_name + " ### " + " Made by ninekorn" + " ### " + " => " + "disabled cubes: " + _inactive_counter_cubes + " disabled voxels: " + _inactive_counter_voxels;

                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("test0" + ex.ToString());
                            }
                        }
                    }
                }
                gravity_swtch_counter++;
                _some_frame_counter_grab_right_hand[0][0][0]++;
                //END OF



                /*Matrix tempmat = _icoSphere._TEMPPOSITION; //_player_rght_hnd[0]._arrayOfInstances[0].current_pos
                Quaternion otherQuat;
                Quaternion.RotationMatrix(ref tempmat, out otherQuat);
                dirLight = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                //Vector3 dirLight = new Vector3(0, -1, 0); //lightDirection;// new Vector3(0, -1, 0);
                //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                //direction_feet_up = _getDirection(Vector3.Up, otherQuat);
                lightpos = new Vector3(0, _icoSphere._TEMPPOSITION.M42, 0);
                */

            }
            else if (Program.useArduinoOVRTouchKeymapper == 0)
            {

                buttonPressedOculusTouchRight = SC_console_directx.D3D.inputStateRTouch.Buttons;


                if (buttonPressedOculusTouchRight != 0)
                {
                    if (buttonPressedOculusTouchRight == 4)
                    {
                        if (gravity_swtch_counter >= 75)
                        {
                            for (int xx = 0; xx < Program._physics_engine_instance_x; xx++)
                            {
                                for (int yy = 0; yy < Program._physics_engine_instance_y; yy++)
                                {
                                    for (int zz = 0; zz < Program._physics_engine_instance_z; zz++)
                                    {
                                        var indexer00 = xx + Program._physics_engine_instance_x * (yy + Program._physics_engine_instance_y * zz);

                                        try
                                        {
                                            for (int x = 0; x < Program.world_width; x++)
                                            {
                                                for (int y = 0; y < Program.world_height; y++)
                                                {
                                                    for (int z = 0; z < Program.world_depth; z++)
                                                    {
                                                        var indexer01 = x + Program.world_width * (y + Program.world_height * z);

                                                        object _some_data_00 = (object)_sc_jitter_tasks[indexer00][indexer01]._world_data[0];
                                                        World _jitter_world = (World)_some_data_00;
                                                        if (_jitter_world != null)
                                                        {
                                                            if (_jitter_world.RigidBodies.Count > 0)
                                                            {

                                                                if (gravity_swtch == 0 || gravity_swtch == 2)
                                                                {
                                                                    _jitter_world.Gravity = new JVector(0, 0, 0);
                                                                }
                                                                else if (gravity_swtch == 1)
                                                                {
                                                                    _jitter_world.Gravity = new JVector(0, -9.81f, 0);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                    }
                                }
                            }

                            if (gravity_swtch == 0 || gravity_swtch == 2)
                            {
                                gravity_swtch_counter = 0;
                                gravity_swtch = 1;
                            }
                            else if (gravity_swtch == 1)
                            {
                                gravity_swtch_counter = 0;
                                gravity_swtch = 2;
                            }

                        }
                    }
                }







                for (int xx = 0; xx < Program._physics_engine_instance_x; xx++)
                {
                    for (int yy = 0; yy < Program._physics_engine_instance_y; yy++)
                    {
                        for (int zz = 0; zz < Program._physics_engine_instance_z; zz++)
                        {
                            var indexer00 = xx + Program._physics_engine_instance_x * (yy + Program._physics_engine_instance_y * zz);

                            try
                            {
                                for (int x = 0; x < Program.world_width; x++)
                                {
                                    for (int y = 0; y < Program.world_height; y++)
                                    {
                                        for (int z = 0; z < Program.world_depth; z++)
                                        {
                                            var indexer01 = x + Program.world_width * (y + Program.world_height * z);



                                            object _some_data_00 = (object)_sc_jitter_tasks[indexer00][indexer01]._world_data[0];
                                            World _jitter_world = (World)_some_data_00;
                                            if (_jitter_world != null)
                                            {
                                                if (_jitter_world.RigidBodies.Count > 0)
                                                {
                                                    if (buttonPressedOculusTouchRight != 0)
                                                    {
                                                        if (buttonPressedOculusTouchRight == 4)
                                                        {
                                                            if (gravity_swtch_counter >= 75)
                                                            {
                                                                if (gravity_swtch == 0 || gravity_swtch == 2)
                                                                {
                                                                    _jitter_world.Gravity = new JVector(0, 0, 0);
                                                                    gravity_swtch_counter = 0;
                                                                    gravity_swtch = 1;
                                                                }
                                                                else if (gravity_swtch == 1)
                                                                {
                                                                    _jitter_world.Gravity = new JVector(0, -9.81f, 0);
                                                                    gravity_swtch_counter = 0;
                                                                    gravity_swtch = 2;
                                                                }
                                                            }
                                                        }
                                                    }


                                                    _inactive_counter_cubes = 0;
                                                    _inactive_counter_voxels = 0;



                                                    int _terrain_count = 0;
                                                    int _floor_count = 0;
                                                    int _voxel_cube_counter = 0;
                                                    int _non_voxel_cube_counter = 0;
                                                    int _non_voxel_cone_counter = 0;
                                                    int _non_voxel_cylinder_counter = 0;
                                                    int _non_voxel_capsule_counter = 0;
                                                    int _non_voxel_sphere_counter = 0;


                                                    int clothCounter = 0;



                                                    int p_l_shldr_count = 0;
                                                    int p_r_shldr_count = 0;


                                                    int p_r_hnd_count = 0;
                                                    int p_l_hnd_count = 0;
                                                    int p_l_lowerA_count = 0;
                                                    int p_r_lowerA_count = 0;
                                                    int p_l_upperA_count = 0;
                                                    int p_r_upperA_count = 0;
                                                    int p_l_target_count = 0;
                                                    int p_r_target_count = 0;
                                                    int p_l_target_two_count = 0;
                                                    int p_r_target_two_count = 0;





                                                    int p_r_foot_count = 0;
                                                    int p_l_foot_count = 0;
                                                    int p_l_lowerL_count = 0;
                                                    int p_r_lowerL_count = 0;
                                                    int p_l_upperL_count = 0;
                                                    int p_r_upperL_count = 0;
                                                    int p_l_target_knee_count = 0;
                                                    int p_r_target_knee_count = 0;
                                                    int p_l_target_knee_two_count = 0;
                                                    int p_r_target_knee_two_count = 0;










                                                    int p_torso_count = 0;
                                                    int p_pelvis_count = 0;
                                                    int p_head_count = 0;

                                                    int _screen_asset_counter = 0;
                                                    int _screen_counter = 0;

                                                    has_water_buo_effect = -1;

                                                    enumerator = _jitter_world.RigidBodies.GetEnumerator();

                                                    while (enumerator.MoveNext())
                                                    {
                                                        body = (RigidBody)enumerator.Current;

                                                        if (body != null && body.Tag != null) //&& body != _grab_rigid_data._body
                                                        {

                                                            if (buttonPressedOculusTouchRight == 2) // release grabbed object
                                                            {
                                                                //touchRX = 0;
                                                                //touchRY = 0;
                                                                //touchRZ = 0;

                                                                //SC_Update.RotationX4Pelvis = 0;
                                                                //SC_Update.RotationY4Pelvis = 0;
                                                                //SC_Update.RotationZ4Pelvis = 0;

                                                                SC_Update.RotationGrabbedX = 0;
                                                                SC_Update.RotationGrabbedY = 0;
                                                                SC_Update.RotationGrabbedZ = 0;

                                                                _grab_rigid_data.position = Matrix.Identity;
                                                                _grab_rigid_data._body = null;
                                                                _some_frame_counter_grab_right_hand_swtch[0][0][0] = 0;
                                                            }

                                                            if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedScreen ||
                                                                (SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCube ||
                                                                (SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCone ||
                                                                (SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCylinder ||
                                                                (SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCapsule ||
                                                                (SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedSphere)
                                                            {


                                                                /*if (_some_frame_counter_grab_right_hand_swtch[0][0][0] == 0) //keep counting up when switch is at 0
                                                                {
                                                                    if (_some_frame_counter_grab_right_hand[0][0][0] > 100)
                                                                    {
                                                                        SC_Update.RotationGrabbedX = 0;
                                                                        SC_Update.RotationGrabbedY = 0;
                                                                        SC_Update.RotationGrabbedZ = 0;
                                                                        Vector3 current_handposR = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41,
                                                                                                               _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42,
                                                                                                               _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);

                                                                        Matrix tempmatter = _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos;
                                                                        Quaternion quater;
                                                                        Quaternion.RotationMatrix(ref tempmatter, out quater);

                                                                        var centerPosRight = new SharpDX.Vector3(current_handposR.X, current_handposR.Y, current_handposR.Z);
                                                                        var rayDirForward = sc_maths._getDirection(SharpDX.Vector3.ForwardRH, quater);
                                                                        var _ray = new SharpDX.Ray(centerPosRight, rayDirForward);

                                                                        float fraction;

                                                                        JVector ray = new JVector(_ray.Direction.X, _ray.Direction.Y, _ray.Direction.Z);
                                                                        //JVector camp = Conversion.ToJitterVector(Camera.Position);
                                                                        ray = JVector.Normalize(ray) * 0.15f; // 0.25f

                                                                        var camp = new JVector(centerPosRight.X, centerPosRight.Y, centerPosRight.Z);

                                                                        bool resulter = _jitter_world.CollisionSystem.Raycast(camp, ray, RaycastCallback, out grabBody, out hitNormal, out fraction);

                                                                        if (buttonPressedOculusTouchRight == 1 && grabBody != _player_rght_hnd[0][0]._arrayOfInstances[0].transform.Component.rigidbody)
                                                                        {
                                                                            if (resulter)
                                                                            {
                                                                                JVector hitpointer = camp + fraction * ray;
                                                                                _grab_rigid_data.grabHitPoint = new Vector3(hitpointer.X, hitpointer.Y, hitpointer.Z);
                                                                                _grab_rigid_data.rayGrabDir = new Vector3(_ray.Direction.X, _ray.Direction.Y, _ray.Direction.Z);
                                                                                _grab_rigid_data.rayGrabDirLength = _grab_rigid_data.rayGrabDir.Length();

                                                                                Matrix.Translation(grabBody.Position.X, grabBody.Position.Y, grabBody.Position.Z, out translationMatrix);
                                                                                quatterer = JQuaternion.CreateFromMatrix(grabBody.Orientation);
                                                                                tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                                Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                                Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);

                                                                                var dirToGrabber = new Vector3(grabBody.Position.X, grabBody.Position.Y, grabBody.Position.Z) - current_handposR; //_grab_rigid_data.grabHitPoint
                                                                                //var dirToGrabber = new Vector3(hitpointer.X, hitpointer.Y, hitpointer.Z) - current_handposR; //_grab_rigid_data.grabHitPoint

                                                                                var rCenter = new SharpDX.Vector3(_player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION.M41, _player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION.M42, _player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION.M43);
                                                                                finalRotationMatrix.M41 = 0;
                                                                                finalRotationMatrix.M42 = 0;
                                                                                finalRotationMatrix.M43 = 0;
                                                                                Matrix centerPoser = _rightTouchMatrixer;// _player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION;
                                                                                centerPoser *= finalRotationMatrix;
                                                                                centerPoser.M41 = rCenter.X;
                                                                                centerPoser.M42 = rCenter.Y;
                                                                                centerPoser.M43 = rCenter.Z;
                                                                                _player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION = centerPoser;

                                                                                var differX = grabBody.Position.X - _player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION.M41;
                                                                                var differY = grabBody.Position.Y - _player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION.M42;
                                                                                var differZ = grabBody.Position.Z - _player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION.M43;

                                                                                _grab_rigid_data.grabHitPointLength = dirToGrabber.Length();

                                                                                _grab_rigid_data._body = grabBody;
                                                                                _grab_rigid_data.position = translationMatrix;

                                                                                _grab_rigid_data.dirDiffX = (differX);// dirToGrabber.X);
                                                                                _grab_rigid_data.dirDiffY = (differY);//Math.Abs(dirToGrabber.Y);
                                                                                _grab_rigid_data.dirDiffZ = (differZ);//Math.Abs(dirToGrabber.Z);

                                                                                Matrix grabbedBodyMatrix = translationMatrix;
                                                                                var xq = quatterer.X;
                                                                                var yq = quatterer.Y;
                                                                                var zq = quatterer.Z;
                                                                                var wq = quatterer.W;

                                                                                var pitcha = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
                                                                                var yawa = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
                                                                                var rolla = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);

                                                                                Matrix handMatrix = _rightTouchMatrixer;//_player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION * finalRotationMatrix; //_rightTouchMatrix * OriginRot * RotatingMatrix * RotatingMatrixForPelvis * hmdmatrixRot_


                                                                                Quaternion.RotationMatrix(ref handMatrix, out quater);
                                                                                xq = quater.X;
                                                                                yq = quater.Y;
                                                                                zq = quater.Z;
                                                                                wq = quater.W;

                                                                                pitcha = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
                                                                                yawa = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
                                                                                rolla = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);


                                                                                touchRX = pitcha;
                                                                                touchRY = yawa;
                                                                                touchRZ = rolla;


                                                                                grabrotX = pitcha;
                                                                                grabrotY = yawa;
                                                                                grabrotZ = rolla;

                                                                                double currentGrabrotDiffX = (pitcha);
                                                                                double currentGrabrotDiffY = (yawa);
                                                                                double currentGrabrotDiffZ = (rolla);


                                                                                Quaternion.RotationMatrix(ref grabbedBodyMatrix, out quater);
                                                                                xq = quater.X;
                                                                                yq = quater.Y;
                                                                                zq = quater.Z;
                                                                                wq = quater.W;
                                                                                pitcha = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
                                                                                yawa = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
                                                                                rolla = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);






                                                                                grabrotDiffx = pitcha - currentGrabrotDiffX;
                                                                                grabrotDiffy = yawa - currentGrabrotDiffY;
                                                                                grabrotDiffz = rolla - currentGrabrotDiffZ;

                                                                                dirToGrabber.Normalize();
                                                                                _grab_rigid_data.directionToGrabber = dirToGrabber;




                                                                                pitchTouchRer = (float)(Math.PI * (SC_Update.RotationX4Pelvis) / 180.0f);
                                                                                yawTouchRer = (float)(Math.PI * (SC_Update.RotationY4Pelvis) / 180.0f);
                                                                                rollTouchRer = (float)(Math.PI * (SC_Update.RotationZ4Pelvis) / 180.0f);

                                                                                rotMatForPelvis = SharpDX.Matrix.RotationYawPitchRoll((float)yawTouchRer, (float)pitchTouchRer, (float)rollTouchRer);

                                                                                totalDiffX = 0;
                                                                                totalDiffY = 0;
                                                                                totalDiffZ = 0;





                                                                                _some_frame_counter_grab_right_hand_swtch[0][0][0] = 1;
                                                                                _some_frame_counter_grab_right_hand[0][0][0] = 0;
                                                                            }
                                                                        }
                                                                    }

                                                                }*/

                                                                /*if (_distanceConstraintRight != null)
                                                                {
                                                                    _jitter_world.RemoveConstraint(_distanceConstraintRight);
                                                                }

                                                                //JVector lanchor = new JVector(current_handposR.X, current_handposR.Y, current_handposR.Z) - grabBody.Position; //hitPoint
                                                                //lanchor = JVector.Transform(lanchor, JMatrix.Transpose(grabBody.Orientation));

                                                                _distanceConstraintRight = new PointPointDistance(_player_rght_hnd[0][0]._arrayOfInstances[0].transform.Component.rigidbody, grabBody, camp, hitPoint);

                                                                _distanceConstraintRight.Softness = 0.0001f;
                                                                _distanceConstraintRight.BiasFactor = 0.1f;
                                                                _distanceConstraintRight.Distance = 0.001f;

                                                                _jitter_world.AddConstraint(_distanceConstraintRight);

                                                                _lastFraction = fraction;
                                                                _lastRigidGrab = grabBody;
                                                                _jitter_world.RemoveConstraint(_distanceConstraintRight);
                                                                */

                                                                if (_some_frame_counter_grab_right_hand_swtch[0][0][0] == 1) //keep counting up when switch is at 0
                                                                {
                                                                    /*if (_grab_rigid_data._body != null)
                                                                    {
                                                                        Vector3 current_handposR = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);

                                                                        Matrix tempmatter = _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos;
                                                                        Quaternion quater;
                                                                        Quaternion.RotationMatrix(ref tempmatter, out quater);
                                                                        var rayDirForward = sc_maths._getDirection(SharpDX.Vector3.ForwardRH, quater);
                                                                        var rayDirUp = sc_maths._getDirection(SharpDX.Vector3.Up, quater);
                                                                        var rayDirRight = sc_maths._getDirection(SharpDX.Vector3.Right, quater);

                                                                        Vector3 movingPointer = current_handposR + (rayDirForward * _grab_rigid_data.dirDiffZ);
                                                                        //movingPointer = movingPointer + (rayDirUp * _grab_rigid_data.dirDiffY);
                                                                        //movingPointer = movingPointer + (rayDirRight * _grab_rigid_data.dirDiffX);

                                                                        Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                        quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                        tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                        Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                        Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);

                                                                        //translationMatrix.M41 = movingPointer.X;
                                                                        //translationMatrix.M42 = movingPointer.Y;
                                                                        //translationMatrix.M43 = movingPointer.Z;
                                                                        body.Position = new JVector(current_handposR.X, current_handposR.Y, current_handposR.Z);

                                                                        //JQuaternion _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                                                                        //var matrixIn = JMatrix.CreateFromQuaternion(_other_quat);
                                                                    }
                                                                    else
                                                                    {
                                                                        _some_frame_counter_grab_right_hand_swtch[0][0][0] = 0;
                                                                    }*/
                                                                }



















                                                                /*if (buttonPressedOculusTouchRight != 0)
                                                                {
                                                                    if (buttonPressedOculusTouchRight == 4)
                                                                    {
                                                                        if (body.IsActive == false)
                                                                        {
                                                                            body.AllowDeactivation = true;
                                                                            body.IsActive = true;
                                                                            body.AllowDeactivation = false;
                                                                        }
                                                                    }
                                                                }





                                                                has_water_buo_effect = -1;

                                                                if (_buo.FluidBox.Contains(body.BoundingBox) != JBBox.ContainmentType.Disjoint)
                                                                {
                                                                    _buo.fluidArea = null;
                                                                    containsCoord = false;

                                                                    JVector[] positions = _buo.samples[body.Shape];

                                                                    float frac = 0.0f;

                                                                    JVector currentCoord = JVector.Zero;
                                                                    for (int i = 0; i < positions.Length; i++)
                                                                    {
                                                                        currentCoord = JVector.Transform(positions[i], body.Orientation);
                                                                        currentCoord = JVector.Add(currentCoord, body.Position);

                                                                        containsCoord = _buo.FluidBox.Contains(ref currentCoord) != JBBox.ContainmentType.Disjoint;

                                                                        if (containsCoord)
                                                                        {
                                                                            has_water_buo_effect = 1;
                                                                            body.AddForce((1.0f / positions.Length) * body.Mass * _buo.Flow);
                                                                            body.AddForce(-(1.0f / positions.Length) * body.Shape.Mass * _buo.Density * _buo._world.Gravity, currentCoord);
                                                                            frac += 1.0f / positions.Length;
                                                                        }
                                                                    }

                                                                    var buo_modded = _buo.Damping;
                                                                    body.AngularVelocity *= buo_modded * 1.075f; //1.01f
                                                                    body.LinearVelocity *= buo_modded;
                                                                }*/
                                                            }

                                                            if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.Terrain)
                                                            {
                                                                Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                worldMatrix_instances_terrain[0][0][0] = translationMatrix;
                                                                _terrain_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag._floor)
                                                            {
                                                                Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                worldMatrix_instances_floor[0][0][0] = translationMatrix;
                                                                _floor_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCube ||
                                                                     (SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCone ||
                                                                     (SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCylinder ||
                                                                     (SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCapsule ||
                                                                     (SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedSphere)
                                                            {
                                                                //grabbedBodyMatrix = Matrix.Identity;
                                                                Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);

                                                                if (body == _grab_rigid_data._body)
                                                                {
                                                                    Matrix matrixerer = Matrix.Identity;

                                                                    grabbedBodyMatrix = _grab_rigid_data.position;
                                                                    Matrix handMatrix = _player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION * finalRotationMatrix;// _rightTouchMatrix * OriginRot * RotatingMatrix * RotatingMatrixForPelvis * hmdmatrixRot_;
                                                                    var MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M43);
                                                                    Matrix someMatRight = _rightTouchMatrix;// * OriginRot * RotatingMatrix * RotatingMatrixForPelvis * hmdmatrixRot_;
                                                                    someMatRight.M41 = handPoseRight.Position.X + MOVINGPOINTER.X;
                                                                    someMatRight.M42 = handPoseRight.Position.Y;// + MOVINGPOINTER.Y;
                                                                    someMatRight.M43 = handPoseRight.Position.Z + MOVINGPOINTER.Z;
                                                                    var diffNormPosX = (MOVINGPOINTER.X) - someMatRight.M41;
                                                                    var diffNormPosY = (MOVINGPOINTER.Y) - someMatRight.M42;
                                                                    var diffNormPosZ = (MOVINGPOINTER.Z) - someMatRight.M43;
                                                                    MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_right * (diffNormPosX));
                                                                    MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_up * (diffNormPosY));
                                                                    MOVINGPOINTER = MOVINGPOINTER + (current_rotation_of_torso_pivot_forward * (diffNormPosZ));

                                                                    MOVINGPOINTER.X += SC_Update.OFFSETPOS.X;
                                                                    MOVINGPOINTER.Y += SC_Update.OFFSETPOS.Y;
                                                                    MOVINGPOINTER.Z += SC_Update.OFFSETPOS.Z;

                                                                    Quaternion quater;
                                                                    Quaternion.RotationMatrix(ref handMatrix, out quater);

                                                                    var rayDirForward = sc_maths._getDirection(SharpDX.Vector3.ForwardRH, quater);
                                                                    rayDirForward.Normalize();
                                                                    var rayDirUp = sc_maths._getDirection(SharpDX.Vector3.Up, quater);
                                                                    rayDirUp.Normalize();
                                                                    var rayDirRight = sc_maths._getDirection(SharpDX.Vector3.Right, quater);
                                                                    rayDirRight.Normalize();


                                                                    var current_handposRR = new Vector3(MOVINGPOINTER.X,
                                                                                                        MOVINGPOINTER.Y,
                                                                                                        MOVINGPOINTER.Z);

                                                                    MOVINGPOINTER = current_handposRR + (rayDirForward * _grab_rigid_data.grabHitPointLength);

                                                                    var pitch = (float)(Math.PI * (-SC_Update.RotationGrabbedX) / 180.0f);
                                                                    var yaw = (float)(Math.PI * (SC_Update.RotationGrabbedY) / 180.0f);
                                                                    var roll = (float)(Math.PI * (SC_Update.RotationGrabbedZ) / 180.0f);
                                                                    var rotatingMatrixForPelvisReset = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                                                                    matrixerer = grabbedBodyMatrix * rotatingMatrixForPelvisReset * originRoter * rotatingMatrixer; //grabbedBodyMatrix * rotatingMatrixForPelvisReset * originRoter * rotatingMatrixer
                                                                    matrixerer.M41 = MOVINGPOINTER.X;
                                                                    matrixerer.M42 = MOVINGPOINTER.Y;
                                                                    matrixerer.M43 = MOVINGPOINTER.Z;
                                                                    matrixerer.M44 = 1;

                                                                    body.Position = new JVector(MOVINGPOINTER.X, MOVINGPOINTER.Y, MOVINGPOINTER.Z);
                                                                    Quaternion.RotationMatrix(ref matrixerer, out quater);
                                                                    JQuaternion _other_quat = new JQuaternion(quater.X, quater.Y, quater.Z, quater.W);
                                                                    var matrixIn = JMatrix.CreateFromQuaternion(_other_quat);
                                                                    body.Orientation = matrixIn;
                                                                    translationMatrix = matrixerer;
                                                                }
                                                                else
                                                                {

                                                                }

                                                                int tempIndex = 0;

                                                                if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCube)
                                                                {
                                                                    tempIndex = _non_voxel_cube_counter;
                                                                }
                                                                else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCone)
                                                                {
                                                                    tempIndex = _non_voxel_cone_counter;
                                                                }
                                                                else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCylinder)
                                                                {
                                                                    tempIndex = _non_voxel_cylinder_counter;
                                                                }
                                                                else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCapsule)
                                                                {
                                                                    tempIndex = _non_voxel_capsule_counter;
                                                                }
                                                                else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedSphere)
                                                                {
                                                                    tempIndex = _non_voxel_sphere_counter;
                                                                }


                                                                var somePosLastXExtra = _array_of_last_frame_cube_pos[indexer00][indexer01][tempIndex].X;
                                                                var somePosLastYExtra = _array_of_last_frame_cube_pos[indexer00][indexer01][tempIndex].Y;
                                                                var somePosLastZExtra = _array_of_last_frame_cube_pos[indexer00][indexer01][tempIndex].Z;

                                                                var rigidXExtra = Math.Round(somePosLastXExtra * 10) / 10;
                                                                var rigidYExtra = Math.Round(somePosLastYExtra * 10) / 10;
                                                                var rigidZExtra = Math.Round(somePosLastZExtra * 10) / 10;

                                                                var diffXExtra = Math.Abs(somePosLastXExtra - rigidXExtra);
                                                                var diffYExtra = Math.Abs(somePosLastYExtra - rigidYExtra);
                                                                var diffZExtra = Math.Abs(somePosLastZExtra - rigidZExtra);

                                                                //Console.WriteLine(Math.Round(somePosLastX * 10) / 10);

                                                                if (Math.Round(diffXExtra * 0.1f) * 10 < 0.00001f && //0.0000001f
                                                                    Math.Round(diffYExtra * 0.1f) * 10 < 0.00001f &&
                                                                    Math.Round(diffZExtra * 0.1f) * 10 < 0.00001f)
                                                                {
                                                                    var minvelocities = 0.00123f;

                                                                    if (has_water_buo_effect != 1)
                                                                    {
                                                                        minvelocities = 0.00123f;
                                                                    }
                                                                    else
                                                                    {
                                                                        minvelocities = 0.000923f;
                                                                    }

                                                                    JVector currentLinearVelExtra = body.LinearVelocity;
                                                                    JVector currentAngularVelExtra = body.AngularVelocity;
                                                                    if (currentLinearVelExtra.Length() < minvelocities && currentAngularVelExtra.Length() < minvelocities) //0.00035f == 400 approx => 0.00075f == 400 approx
                                                                    {
                                                                        /*if (body.CollisionIsland != null)
                                                                        {
                                                                            if (body.CollisionIsland.Bodies != null)
                                                                            {
                                                                                if (body.CollisionIsland.Bodies.Count <= 0)
                                                                                {
                                                                                    if (body.IsActive == true)
                                                                                    {
                                                                                        body.AllowDeactivation = true;
                                                                                        body.IsActive = false;
                                                                                        //body.AllowDeactivation = false;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }*/
                                                                    }
                                                                    else
                                                                    {
                                                                        /*if (body.IsActive == false)
                                                                        {
                                                                            body.AllowDeactivation = true;
                                                                            body.IsActive = true;
                                                                            body.AllowDeactivation = false;
                                                                        }*/
                                                                    }
                                                                }

                                                                if (SC_Update.handTriggerRight[1] >= 0.001f)
                                                                {
                                                                    if (!body.IsActive)
                                                                    {
                                                                        body.AllowDeactivation = true;
                                                                        body.IsActive = true;
                                                                        body.AllowDeactivation = false;
                                                                    }

                                                                    var dirToRightHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                    var lengthofdir = dirToRightHand.Length();

                                                                    //var moveRightHand = new JVector(_player_lft_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M43) - new JVector(_player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M41, _player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M42, _player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M43);

                                                                    if (dirToRightHand != null)
                                                                    {
                                                                        dirToRightHand.Normalize();
                                                                        lh_attract_force = new JVector(dirToRightHand.X, dirToRightHand.Y, dirToRightHand.Z) * 1;

                                                                        if (lh_attract_force != JVector.Zero && lh_attract_force != null && lh_attract_force.Length() > 0 && body != _grab_rigid_data._body)
                                                                        {
                                                                            //MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
                                                                            //MessageBox((IntPtr)0, "" + "not null force", "Oculus Error", 0);
                                                                            lh_attract_force.Normalize();

                                                                            if (has_water_buo_effect == 1)
                                                                            {
                                                                                lh_attract_force *= force_4_screen * 100;
                                                                            }
                                                                            else
                                                                            {
                                                                                lh_attract_force *= force_4_screen * 1;
                                                                            }

                                                                            //0.0045f


                                                                            //lh_attract_force *= force_4_screen*1000;
                                                                            body.LinearVelocity += lh_attract_force;
                                                                            body.AddForce(lh_attract_force);
                                                                            //body.AddTorque(force);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        //Console.WriteLine("null dir");
                                                                        Program.MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
                                                                    }

                                                                }

                                                                if (!body.IsActive)
                                                                {
                                                                    _inactive_counter_cubes++;
                                                                }

                                                                if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCube)
                                                                {
                                                                    worldMatrix_instances_cubes[indexer00][indexer01][tempIndex] = translationMatrix;
                                                                    _array_of_last_frame_cube_pos[indexer00][indexer01][tempIndex] = new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                    _non_voxel_cube_counter++;
                                                                }
                                                                else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCone)
                                                                {
                                                                    worldMatrix_instances_cone[indexer00][indexer01][tempIndex] = translationMatrix;
                                                                    _array_of_last_frame_cone_pos[indexer00][indexer01][tempIndex] = new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                    _non_voxel_cone_counter++;
                                                                }
                                                                else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCylinder)
                                                                {
                                                                    worldMatrix_instances_cylinder[indexer00][indexer01][tempIndex] = translationMatrix;
                                                                    _array_of_last_frame_cylinder_pos[indexer00][indexer01][tempIndex] = new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                    _non_voxel_cylinder_counter++;
                                                                }
                                                                else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedCapsule)
                                                                {
                                                                    worldMatrix_instances_capsule[indexer00][indexer01][tempIndex] = translationMatrix;
                                                                    _array_of_last_frame_capsule_pos[indexer00][indexer01][tempIndex] = new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                    _non_voxel_capsule_counter++;
                                                                }
                                                                else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedSphere)
                                                                {
                                                                    worldMatrix_instances_sphere[indexer00][indexer01][tempIndex] = translationMatrix;
                                                                    _array_of_last_frame_sphere_pos[indexer00][indexer01][tempIndex] = new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                    _non_voxel_sphere_counter++;
                                                                }
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.sc_perko_voxel)
                                                            {
                                                                var somePosLastXExtra = _array_of_last_frame_voxel_pos[indexer00][indexer01][_voxel_cube_counter].X;
                                                                var somePosLastYExtra = _array_of_last_frame_voxel_pos[indexer00][indexer01][_voxel_cube_counter].Y;
                                                                var somePosLastZExtra = _array_of_last_frame_voxel_pos[indexer00][indexer01][_voxel_cube_counter].Z;

                                                                var rigidXExtra = Math.Round(somePosLastXExtra * 10) / 10;
                                                                var rigidYExtra = Math.Round(somePosLastYExtra * 10) / 10;
                                                                var rigidZExtra = Math.Round(somePosLastZExtra * 10) / 10;

                                                                var diffXExtra = Math.Abs(somePosLastXExtra - rigidXExtra);
                                                                var diffYExtra = Math.Abs(somePosLastYExtra - rigidYExtra);
                                                                var diffZExtra = Math.Abs(somePosLastZExtra - rigidZExtra);

                                                                //Console.WriteLine(Math.Round(somePosLastX * 10) / 10);

                                                                if (Math.Round(diffXExtra * 0.1f) * 10 < 0.00001f && //0.0000001f
                                                                    Math.Round(diffYExtra * 0.1f) * 10 < 0.00001f &&
                                                                    Math.Round(diffZExtra * 0.1f) * 10 < 0.00001f)
                                                                {
                                                                    float minvelocities = 0.00123f;
                                                                    if (has_water_buo_effect != 1)
                                                                    {
                                                                        minvelocities = 0.00123f;
                                                                    }
                                                                    else
                                                                    {
                                                                        minvelocities = 0.000923f;
                                                                    }

                                                                    JVector currentLinearVelExtra = body.LinearVelocity;
                                                                    JVector currentAngularVelExtra = body.AngularVelocity;
                                                                    if (currentLinearVelExtra.Length() < minvelocities && currentAngularVelExtra.Length() < minvelocities) //0.00035f == 400 approx => 0.00075f == 400 approx
                                                                    {
                                                                        if (body.CollisionIsland != null)
                                                                        {
                                                                            /*if (body.CollisionIsland.Bodies != null)
                                                                            {
                                                                                if (body.CollisionIsland.Bodies.Count <= 0)
                                                                                {
                                                                                    if (body.IsActive == true)
                                                                                    {
                                                                                        body.AllowDeactivation = true;
                                                                                        body.IsActive = false;
                                                                                        //body.AllowDeactivation = false;
                                                                                    }
                                                                                }
                                                                            }*/
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        /*if (body.IsActive == false)
                                                                        {
                                                                            body.AllowDeactivation = true;
                                                                            body.IsActive = true;
                                                                            body.AllowDeactivation = false;
                                                                        }*/
                                                                    }
                                                                }

                                                                if (SC_Update.handTriggerRight[1] >= 0.001f)
                                                                {
                                                                    if (!body.IsActive)
                                                                    {
                                                                        body.AllowDeactivation = true;
                                                                        body.IsActive = true;
                                                                        body.AllowDeactivation = false;
                                                                    }

                                                                    var dirToRightHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                    var lengthofdir = dirToRightHand.Length();

                                                                    //var moveRightHand = new JVector(_player_lft_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M43) - new JVector(_player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M41, _player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M42, _player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M43);

                                                                    if (dirToRightHand != null)
                                                                    {
                                                                        dirToRightHand.Normalize();
                                                                        lh_attract_force = new JVector(dirToRightHand.X, dirToRightHand.Y, dirToRightHand.Z) * 1;

                                                                        if (lh_attract_force != JVector.Zero && lh_attract_force != null && lh_attract_force.Length() > 0)
                                                                        {
                                                                            //MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
                                                                            //MessageBox((IntPtr)0, "" + "not null force", "Oculus Error", 0);
                                                                            lh_attract_force.Normalize();

                                                                            if (has_water_buo_effect == 1)
                                                                            {
                                                                                lh_attract_force *= force_4_screen * 100;
                                                                            }
                                                                            else
                                                                            {
                                                                                lh_attract_force *= force_4_screen * 1;
                                                                            }

                                                                            //0.0045f

                                                                            //lh_attract_force *= force_4_screen*1000;
                                                                            body.LinearVelocity += lh_attract_force;
                                                                            body.AddForce(lh_attract_force);
                                                                            //body.AddTorque(force);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        //Console.WriteLine("null dir");
                                                                        Program.MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
                                                                    }
                                                                }

                                                                worldMatrix_instances_voxel_cube[indexer00][indexer01][_voxel_cube_counter] = translationMatrix;
                                                                _array_of_last_frame_voxel_pos[indexer00][indexer01][_voxel_cube_counter] = new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                                if (!body.IsActive)
                                                                {
                                                                    _inactive_counter_voxels++;
                                                                }


                                                                _voxel_cube_counter++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerHandRight)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_hand[0][0][p_r_hnd_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_hnd_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerHandLeft)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_hand[0][0][p_l_hnd_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_hnd_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerTorso)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_torso[0][0][p_torso_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_torso_count++;
                                                            }

                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerPelvis)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_pelvis[0][0][p_pelvis_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_pelvis_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerShoulderRight)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_shoulder[0][0][p_r_shldr_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_shldr_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerShoulderLeft)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_shoulder[0][0][p_l_shldr_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_shldr_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerHead)
                                                            {

                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_head[0][0][p_head_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);

                                                                p_head_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerArmRight)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_lowerarm[0][0][p_r_lowerA_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_lowerA_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerArmLeft)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_lowerarm[0][0][p_l_lowerA_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_lowerA_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmRight)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_upperarm[0][0][p_r_upperA_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_upperA_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperArmLeft)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_upperarm[0][0][p_l_upperA_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_upperA_count++;

                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLeftElbowTarget)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_elbow_target[0][0][p_l_target_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_target_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightElbowTarget)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_elbow_target[0][0][p_r_target_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_target_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLeftElbowTargettwo)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_elbow_target_two[0][0][p_l_target_two_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_target_two_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightElbowTargettwo)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_elbow_target_two[0][0][p_l_target_two_count]; ;
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_target_two_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerLegLeft)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_lower_leg[0][0][p_l_lowerL_count]; ;
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_lowerL_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperLegLeft)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_upper_leg[0][0][p_l_upperL_count]; ;
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_upperL_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLowerLegRight)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_lower_leg[0][0][p_r_lowerL_count]; ;
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_lowerL_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerUpperLegRight)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_upper_leg[0][0][p_r_upperL_count]; ;
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_upperL_count++;
                                                            }

                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerFootLeft)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_foot[0][0][p_l_foot_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_foot_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerFootRight)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_foot[0][0][p_r_foot_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_foot_count++;
                                                            }


                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLeftTargetKnee)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_target_knee[0][0][p_l_target_knee_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_target_knee_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightTargetKnee)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_target_knee[0][0][p_r_target_knee_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_target_knee_count++;
                                                            }

                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerLeftTargettwoKnee)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_l_target_two_knee[0][0][p_l_target_knee_two_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_l_target_knee_two_count++;
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.PlayerRightTargettwoKnee)
                                                            {
                                                                Quaternion temp_quat;
                                                                Matrix temp_mat = worldMatrix_instances_r_target_two_knee[0][0][p_r_target_knee_two_count];
                                                                Quaternion.RotationMatrix(ref temp_mat, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;
                                                                body.Position = new JVector(temp_mat.M41, temp_mat.M42, temp_mat.M43);
                                                                p_r_target_knee_two_count++;
                                                            }

                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedScreen)
                                                            {
                                                                Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                worldMatrix_instances_screens[0][0][0] = translationMatrix;
                                                                _world_screen_list[0][0]._arrayOfInstances[0].current_pos = translationMatrix;

                                                                worldMatrix_instances_containment_grid_screen[0][0][0] = translationMatrix;
                                                                _world_containment_grid_screen[0][0]._arrayOfInstances[0].current_pos = translationMatrix;

                                                                _screen_counter++;

                                                                /*if (_has_locked_screen_pos == 0)
                                                                {
                                                                    if (!body.IsStatic)
                                                                    {
                                                                        if (SC_Update.handTriggerLeft[0] >= 0.001f)
                                                                        {
                                                                            body.IsActive = true;

                                                                            Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                            quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                            tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                            Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                            Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                            var dirToRightHand = new Vector3(_player_lft_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_lft_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_lft_hnd[0][0]._arrayOfInstances[0].current_pos.M43) - new Vector3(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                                            var lengthofdir = dirToRightHand.Length();

                                                                            //var moveRightHand = new JVector(_player_lft_hnd[0]._arrayOfInstances[0].current_pos.M41, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M42, _player_lft_hnd[0]._arrayOfInstances[0].current_pos.M43) - new JVector(_player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M41, _player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M42, _player_lft_hnd[0]._arrayOfInstances[0]._LASTPOSITIONFORPHYSICS.M43);

                                                                            if (dirToRightHand != null)
                                                                            {
                                                                                dirToRightHand.Normalize();
                                                                                lh_attract_force = new JVector(dirToRightHand.X, dirToRightHand.Y, dirToRightHand.Z) * 1;

                                                                                if (lh_attract_force != JVector.Zero && lh_attract_force != null && lh_attract_force.Length() > 0)
                                                                                {
                                                                                    //MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
                                                                                    //MessageBox((IntPtr)0, "" + "not null force", "Oculus Error", 0);
                                                                                    lh_attract_force.Normalize();

                                                                                    if (has_water_buo_effect == 1)
                                                                                    {
                                                                                        lh_attract_force *= force_4_screen * 100;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        lh_attract_force *= force_4_screen * 1;
                                                                                    }

                                                                                    //0.0045f

                                                                                    //lh_attract_force *= force_4_screen*1000;
                                                                                    body.LinearVelocity += lh_attract_force;
                                                                                    body.AddForce(lh_attract_force);
                                                                                    //body.AddTorque(force);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                //Console.WriteLine("null dir");
                                                                                Program.MessageBox((IntPtr)0, "" + "null dir 01", "Oculus Error", 0);
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
                                                                    /*Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                    quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                    tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                    Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                    Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                    worldMatrix_instances_screens[0][0][0] = translationMatrix;
                                                                    _world_screen_list[0][0]._arrayOfInstances[0].current_pos = translationMatrix;

                                                                }
                                                                else
                                                                {
                                                                    if (_tier_logic_swtch_lock_screen == 0)
                                                                    {
                                                                        if (had_locked_screen == 1)
                                                                        {
                                                                            body.AngularVelocity = JVector.Zero;
                                                                            body.LinearVelocity = JVector.Zero;

                                                                            worldMatrix_instances_screens[0][0][0] = _current_screen_pos;
                                                                            _world_screen_list[0][0]._arrayOfInstances[0].current_pos = _current_screen_pos;// worldMatrix_instances_screens[0][0];                                             

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

                                                                        worldMatrix_instances_screens[0][0][0] = _current_screen_pos;
                                                                        _world_screen_list[0][0]._arrayOfInstances[0].current_pos = _current_screen_pos;// worldMatrix_instances_screens[0][0];
                                                                    }
                                                                }




                                                                Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                                                quatterer = JQuaternion.CreateFromMatrix(body.Orientation);
                                                                tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);
                                                                Matrix.RotationQuaternion(ref tester, out rigidbody_matrix);
                                                                Matrix.Multiply(ref rigidbody_matrix, ref translationMatrix, out translationMatrix);
                                                                worldMatrix_instances_screens[0][0][0] = translationMatrix;
                                                                _world_screen_list[0][0]._arrayOfInstances[0].current_pos = translationMatrix;// worldMatrix_instances_screens[0][0];

                                                                worldMatrix_instances_containment_grid_screen[0][0][0] = translationMatrix;// _world_screen_list[0][0]._arrayOfInstances[0].current_pos;
                                                                _world_containment_grid_screen[0][0]._arrayOfInstances[0].current_pos = translationMatrix;// _world_screen_list[0][0]._arrayOfInstances[0].current_pos;
                                                                _screen_counter++;*/
                                                            }
                                                            else if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.pseudoCloth)
                                                            {
                                                                /*var _tempMatrix = Matrix.Identity;

                                                                Matrix translationMatrix = Matrix.Identity;
                                                                Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);

                                                                Matrix rotationMatrix = Matrix.Identity;


                                                                JQuaternion quatterer = JQuaternion.CreateFromMatrix(body.Orientation);

                                                                Quaternion tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

                                                                Matrix.RotationQuaternion(ref tester, out rotationMatrix);

                                                                Matrix.Multiply(ref rotationMatrix, ref translationMatrix, out _tempMatrix);

                                                                var pos = new Vector3(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                                                                _tempMatrix = WorldMatrix;
                                                                _tempMatrix.M41 = pos.X;
                                                                _tempMatrix.M42 = pos.Y;
                                                                _tempMatrix.M43 = pos.Z;

                                                                Quaternion temp_quat;
                                                                Quaternion.RotationMatrix(ref _tempMatrix, out temp_quat);
                                                                JQuaternion quat = new JQuaternion(temp_quat.X, temp_quat.Y, temp_quat.Z, temp_quat.W);
                                                                JMatrix jmat = JMatrix.CreateFromQuaternion(quat);
                                                                body.Orientation = jmat;

                                                                sc_jitter_cloth._cube._arrayOfInstances[clothCounter].current_pos = _tempMatrix;
                                                                //_arrayOfClothCubes[clothCounter]._arrayOfInstances[clothCounter].current_pos = _tempMatrix;
                                                                worldMatrix_Cloth_instances[clothCounter] = _tempMatrix;

                                                                clothCounter++;*/
                                                            }

                                                        }
                                                    }
                                                }
                                                //Console.Title = Program._MainWindow_name + " ### " + " Made by Steve Chassé" + " ### " + " => " + "disabled cubes: " + _inactive_counter_cubes + " disabled voxels: " + _inactive_counter_voxels;
                                                Console.Title = Program._MainWindow_name + " ### " + " Made by ninekorn" + " ### " + " => " + "disabled cubes: " + _inactive_counter_cubes + " disabled voxels: " + _inactive_counter_voxels;

                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("test0" + ex.ToString());
                            }
                        }
                    }
                }
                gravity_swtch_counter++;
                _some_frame_counter_grab_right_hand[0][0][0]++;
                //END OF



                /*Matrix tempmat = _icoSphere._TEMPPOSITION; //_player_rght_hnd[0]._arrayOfInstances[0].current_pos
                Quaternion otherQuat;
                Quaternion.RotationMatrix(ref tempmat, out otherQuat);
                dirLight = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                //Vector3 dirLight = new Vector3(0, -1, 0); //lightDirection;// new Vector3(0, -1, 0);
                //direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
                //direction_feet_right = _getDirection(Vector3.Right, otherQuat);
                //direction_feet_up = _getDirection(Vector3.Up, otherQuat);
                lightpos = new Vector3(0, _icoSphere._TEMPPOSITION.M42, 0);
                */

            }
            return _sc_jitter_tasks;
        }

        Matrix finalRotationMatrix = Matrix.Identity;

        PointPointDistance _distanceConstraintRight;
        PointPointDistance _distanceConstraintLeft;
        RigidBody _lastRigidGrab;
        float _lastFraction = 0;
        JVector _lastHitPoint;
        //bool _hasGrabbed = false;

        private bool RaycastCallback(RigidBody body, JVector normal, float fraction)
        {
            if (body.IsStatic) return false;
            else return true;
        }

        Matrix hmdmatrixRot_;
        Matrix OriginRot = Matrix.Identity;
        Matrix RotatingMatrix = Matrix.Identity;
        Matrix RotatingMatrixForPelvis = Matrix.Identity;
        Matrix viewMatrix_ = Matrix.Identity;

        public unsafe SC_message_object_jitter[][] workOnSomething(SC_message_object_jitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix hmdrotMatrix, Matrix hmd_matrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft)
        {
            viewMatrix_ = viewMatrix;

            /*
            if (_some_frame_counter_grab_right_hand[0][0][0] > 1)
            {
                Vector3 current_handposR = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);
                var centerPosRighthandposR = new Vector3(final_hand_pos_right_locked.M41, final_hand_pos_right_locked.M42, final_hand_pos_right_locked.M43);
                Quaternion.RotationMatrix(ref final_hand_pos_right_locked, out _rightTouchQuat);
                var rayDirFront = sc_maths._getDirection(Vector3.ForwardRH, _rightTouchQuat);
                someRay = new Ray(centerPosRighthandposR, rayDirFront);


                if ((SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag == SCCoreSystems.sc_console.SC_console_directx.BodyTag.physicsInstancedScreen)
                {
                    Quaternion _quat_screen000;
                    Matrix mater = _world_screen_list[0][0]._arrayOfInstances[0].current_pos;
                    Quaternion.RotationMatrix(ref mater, out _quat_screen000);
                    var screenNormal = sc_maths._getDirection(Vector3.ForwardRH, _quat_screen000);
                    screenNormal.Normalize();

                    var planer = new Plane(new Vector3(mater.M41, mater.M42, mater.M43), screenNormal);
                    intersecter = someRay.Intersects(ref planer, out intersectPointRight);

                    var handToScreenNormalDistance = sc_maths.sc_check_distance_node_3d(current_handposR, intersectPointRight, 2, 2, 2, 2, 2, 2, 2, 2, 2);

                    if (handToScreenNormalDistance < 0.25f && intersectPointRight != Vector3.Zero)
                    {
                        Program.MessageBox((IntPtr)0, "" + "resulter", "sc core systems Error", 0);


                        if (buttonPressedOculusTouchRight == 1) // grabObject
                        {
                            Quaternion.RotationMatrix(ref WorldMatrix, out quaterr);

                            var centerPosRight = new SharpDX.Vector3(current_handposR.X, current_handposR.Y, current_handposR.Z);    //Point3D                                                                                                                            //var rayoriginLeft = centerPosLeft;
                            //var rayDirForward = sc_maths._getDirection(SharpDX.Vector3.ForwardRH, quaterr);
                            var _ray = new SharpDX.Ray(centerPosRight, rayDirFront);


                            JVector ray = new JVector(_ray.Direction.X, _ray.Direction.Y, _ray.Direction.Z);
                            //JVector camp = Conversion.ToJitterVector(Camera.Position);
                            ray = JVector.Normalize(ray) * 0.25f;


                            var camp = new JVector(centerPosRight.X, centerPosRight.Y, centerPosRight.Z);

                            bool resulter = _jitter_world.CollisionSystem.Raycast(camp, ray, RaycastCallback, out grabBody, out hitNormal, out fraction);

                            if (resulter)
                            {
                                Program.MessageBox((IntPtr)0, "" + "resulter", "sc core systems Error", 0);
                                var hitPoint = camp + fraction * ray;

                                if (_distanceConstraintRight != null)
                                {
                                    _jitter_world.RemoveConstraint(_distanceConstraintRight);
                                }


                                JVector lanchor = new JVector(current_handposR.X, current_handposR.Y, current_handposR.Z) - grabBody.Position; //hitPoint
                                lanchor = JVector.Transform(lanchor, JMatrix.Transpose(grabBody.Orientation));

                                _distanceConstraintRight = new PointPointDistance(_player_rght_hnd[0][0]._arrayOfInstances[0].transform.Component.rigidbody, grabBody, camp, hitPoint);

                                _distanceConstraintRight.Softness = 0.0001f;
                                _distanceConstraintRight.BiasFactor = 0.1f;
                                _distanceConstraintRight.Distance = 0.001f;

                                _jitter_world.AddConstraint(_distanceConstraintRight);

                                _lastFraction = fraction;
                                _lastRigidGrab = grabBody;
                                _some_frame_counter_grab_right_hand[0][0][0] = 0;
                                _some_frame_counter_grab_right_hand_swtch[0][0][0] = 1;
                            }
                            else
                            {

                            }
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                   bool _boundingBoxer = _jitter_world.CollisionSystem.CheckBoundingBoxes(body, _player_rght_hnd[0][0]._arrayOfInstances[0].transform.Component.rigidbody);

                    if (_boundingBoxer)
                    {
                        if (_distanceConstraintRight != null)
                        {
                            _jitter_world.RemoveConstraint(_distanceConstraintRight);
                        }

                        var centerPosRight = new JVector(current_handposR.X, current_handposR.Y, current_handposR.Z);
                        JVector lanchor = new JVector(current_handposR.X, current_handposR.Y, current_handposR.Z) - grabBody.Position; //hitPoint
                        lanchor = JVector.Transform(lanchor, JMatrix.Transpose(grabBody.Orientation));

                        _distanceConstraintRight = new PointPointDistance(_player_rght_hnd[0][0]._arrayOfInstances[0].transform.Component.rigidbody, grabBody, centerPosRight, lanchor);

                        _distanceConstraintRight.Softness = 0.0001f;
                        _distanceConstraintRight.BiasFactor = 0.1f;
                        _distanceConstraintRight.Distance = 0.001f;

                        _jitter_world.AddConstraint(_distanceConstraintRight);

                        _lastFraction = fraction;
                        _lastRigidGrab = grabBody;
                        _some_frame_counter_grab_right_hand[0][0][0] = 0;
                        _some_frame_counter_grab_right_hand_swtch[0][0][0] = 1;

                    }
                    else
                    {

                    }
                }

            }

            if (_some_frame_counter_grab_right_hand_swtch[0][0][0] == 0) //keep counting up when switch is at 0
            {
                _some_frame_counter_grab_right_hand[0][0][0]++;
            }
            else
            {
                if (buttonPressedOculusTouchRight == 2) // release grabbed object
                {
                    if (_distanceConstraintRight != null)
                    {
                        _jitter_world.RemoveConstraint(_distanceConstraintRight);
                    }
                    grabBody = null;
                    _lastRigidGrab = null;
                    _some_frame_counter_grab_right_hand_swtch[0][0][0] = 0;
                }
            }*/




            /*Vector3 current_handposR = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);
            var centerPosRighthandposR = new Vector3(final_hand_pos_right_locked.M41, final_hand_pos_right_locked.M42, final_hand_pos_right_locked.M43);
            Quaternion.RotationMatrix(ref final_hand_pos_right_locked, out _rightTouchQuat);
            var rayDirFront = sc_maths._getDirection(Vector3.ForwardRH, _rightTouchQuat);
            someRay = new Ray(centerPosRighthandposR, rayDirFront);

            Quaternion _quat_screen000;
            Matrix mater = worldMatrix_instances_screens[0][0][0];
            Quaternion.RotationMatrix(ref mater, out _quat_screen000);
            var screenNormal = sc_maths._getDirection(Vector3.ForwardRH, _quat_screen000);
            screenNormal.Normalize();

            var planer = new Plane(new Vector3(mater.M41, mater.M42, mater.M43), screenNormal);
            intersecter = someRay.Intersects(ref planer, out intersectPointRight);


            var handToScreenNormalDistance = sc_maths.sc_check_distance_node_3d(current_handposR, intersectPointRight, 2, 2, 2, 2, 2, 2, 2, 2, 2);


            if (handToScreenNormalDistance < 0.1f && intersectPointRight != Vector3.Zero)
            {

            }*/





            /*Vector3 current_handposR = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);

            Quaternion quater;
            Quaternion.RotationMatrix(ref WorldMatrix, out quater);

            var centerPosRight = new SharpDX.Vector3(current_handposR.X, current_handposR.Y, current_handposR.Z);    //Point3D                                                                                                                            //var rayoriginLeft = centerPosLeft;
            var rayDirForward = sc_maths._getDirection(SharpDX.Vector3.ForwardRH, quater);
            var _ray = new SharpDX.Ray(centerPosRight, rayDirForward);

            float fraction;

            JVector ray = new JVector(_ray.Direction.X, _ray.Direction.Y, _ray.Direction.Z);
            //JVector camp = Conversion.ToJitterVector(Camera.Position);
            ray = JVector.Normalize(ray) * 0.25f;

            RigidBody grabBody;
            JVector hitNormal;
            var camp = new JVector(centerPosRight.X, centerPosRight.Y, centerPosRight.Z);

            bool resulter = _jitter_world.CollisionSystem.Raycast(camp, ray, RaycastCallback, out grabBody, out hitNormal, out fraction);

            if (buttonPressedOculusTouchRight == 1)
            {
                if (resulter)
                {
                    var hitPoint = camp + fraction * ray;

                    if (_distanceConstraintRight != null)
                    {
                        _jitter_world.RemoveConstraint(_distanceConstraintRight);
                    }


                    JVector lanchor = new JVector(current_handposR.X, current_handposR.Y, current_handposR.Z) - grabBody.Position; //hitPoint
                    lanchor = JVector.Transform(lanchor, JMatrix.Transpose(grabBody.Orientation));

                    _distanceConstraintRight = new PointPointDistance(_player_rght_hnd[0][0]._arrayOfInstances[0].transform.Component.rigidbody, grabBody, camp, hitPoint);

                    _distanceConstraintRight.Softness = 0.0001f;
                    _distanceConstraintRight.BiasFactor = 0.1f;
                    _distanceConstraintRight.Distance = 0.001f;

                    _jitter_world.AddConstraint(_distanceConstraintRight);

                    _lastFraction = fraction;
                    _lastRigidGrab = grabBody;
                    _hasGrabbed = true;
                }
            }

            if (_hasGrabbed)
            {

            }

            if (buttonPressedOculusTouchRight == 2)
            {
                if (_distanceConstraintRight != null)
                {
                    _jitter_world.RemoveConstraint(_distanceConstraintRight);
                }
                grabBody = null;
                _lastRigidGrab = null;
                _hasGrabbed = false;
            }*/















            float timeSinceStart = (float)(DateTime.Now - SC_Update.startTime).TotalSeconds;
            Matrix worldmatlightrot = Matrix.Scaling(1.0f) * Matrix.RotationX(timeSinceStart * disco_sphere_rot_speed) * Matrix.RotationY(timeSinceStart * 2 * disco_sphere_rot_speed) * Matrix.RotationZ(timeSinceStart * 3 * disco_sphere_rot_speed);

            Quaternion worldmatlightquat;
            SharpDX.Quaternion.RotationMatrix(ref worldmatlightrot, out worldmatlightquat);
            Vector3 dirLight = new Vector3(0, -1, 0);// sc_maths._getDirection(Vector3.ForwardRH, worldmatlightquat);







            //lightpos = new Vector3(0, 20, 0);
            ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
            diffuseColour = new Vector4(1, 1, 1, 1);
            lightDirection = new Vector3(0, -1, -1);




            _DLightBuffer_voxel_pchunk_cube[0].lightPosition = lightpos;
            _DLightBuffer_voxel_pchunk_cube[0].lightDirection = dirLight;
            _DLightBuffer_cube[0].lightPosition = lightpos;
            _DLightBuffer_cube[0].lightDirection = dirLight;
            _DLightBuffer_grid[0].lightPosition = lightpos;
            _DLightBuffer_grid[0].lightDirection = dirLight;
            _DLightBuffer_containment_grid[0].lightPosition = lightpos;
            _DLightBuffer_containment_grid[0].lightDirection = dirLight;
            _DLightBuffer_voxel_cube[0].lightPosition = lightpos;
            _DLightBuffer_voxel_cube[0].lightDirection = dirLight;
            _DLightBuffer_spectrum[0].lightPosition = lightpos;
            _DLightBuffer_spectrum[0].lightDirection = dirLight;
            _SC_modL_torso_BUFFER[0].lightPosition = lightpos;
            _SC_modL_torso_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_hnd_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_hnd_BUFFER[0].lightDirection = dirLight;
            _SC_modL_lft_hnd_BUFFER[0].lightPosition = lightpos;
            _SC_modL_lft_hnd_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_shldr_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_shldr_BUFFER[0].lightDirection = dirLight;
            _SC_modL_lft_shldr_BUFFER[0].lightPosition = lightpos;
            _SC_modL_lft_shldr_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_elbow_target_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_elbow_target_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_elbow_target_two_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_elbow_target_two_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_upper_arm_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_upper_arm_BUFFER[0].lightDirection = dirLight;
            _SC_modL_rght_lower_arm_BUFFER[0].lightPosition = lightpos;
            _SC_modL_rght_lower_arm_BUFFER[0].lightDirection = dirLight;
            _SC_modL_lft_elbow_target_BUFFER[0].lightPosition = lightpos;
            _SC_modL_lft_elbow_target_BUFFER[0].lightDirection = dirLight;
            _SC_modL_lft_elbow_target_two_BUFFER[0].lightPosition = lightpos;
            _SC_modL_lft_elbow_target_two_BUFFER[0].lightDirection = dirLight;
            _SC_modL_lft_upper_arm_BUFFER[0].lightPosition = lightpos;
            _SC_modL_lft_upper_arm_BUFFER[0].lightDirection = dirLight;
            _SC_modL_lft_lower_arm_BUFFER[0].lightPosition = lightpos;
            _SC_modL_lft_lower_arm_BUFFER[0].lightDirection = dirLight;
            _SC_modL_pelvis_BUFFER[0].lightPosition = lightpos;
            _SC_modL_pelvis_BUFFER[0].lightDirection = dirLight;

            hmdmatrixRot_ = hmdrotMatrix;

            OriginRot = originRot;
            RotatingMatrix = rotatingMatrix;
            RotatingMatrixForPelvis = rotatingMatrixForPelvis;

            //TERRAIN SINGLEOBJECT
            _terrain[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager.RenderInstancedObject(SC_console_directx.D3D.device.ImmediateContext, _terrain[0][0].IndexCount, _terrain[0][0].InstanceCount, _terrain[0][0]._POSITION, viewMatrix, projectionMatrix, _basicTexture.TextureResource, _DLightBuffer_cube, _terrain[0][0]);
            //END OF

            //SPECTRUM SINGLEOBJECT
            _world_spectrum_list[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager.RenderInstancedObjectSpectrum(SC_console_directx.D3D.device.ImmediateContext, _world_spectrum_list[0][0].IndexCount, _world_spectrum_list[0][0].InstanceCount, _world_spectrum_list[0][0]._POSITION, viewMatrix, projectionMatrix, _basicTexture.TextureResource, _DLightBuffer_spectrum, _world_spectrum_list[0][0]);
            //END OF

            //TERRAIN SINGLEOBJECT
            _floor[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager.RenderInstancedObject(SC_console_directx.D3D.device.ImmediateContext, _floor[0][0].IndexCount, _floor[0][0].InstanceCount, _floor[0][0]._POSITION, viewMatrix, projectionMatrix, _basicTexture.TextureResource, _DLightBuffer_cube, _floor[0][0]);
            //END OF

            //PHYSICS GRID
            _world_grid_list[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager.RenderInstancedGrid(SC_console_directx.D3D.device.ImmediateContext, _world_grid_list[0][0].IndexCount, _world_grid_list[0][0].InstanceCount, _world_grid_list[0][0]._POSITION, viewMatrix, projectionMatrix, SC_Update._desktopFrame._ShaderResource, _DLightBuffer_grid, _world_grid_list[0][0]); // oculusRiftDir



            /*float timeSinceStart = (float)(DateTime.Now - SC_Update.startTime).TotalSeconds;
            Matrix world = Matrix.Scaling(1.0f) * Matrix.RotationX(timeSinceStart * disco_sphere_rot_speed) * Matrix.RotationY(timeSinceStart * 2 * disco_sphere_rot_speed) * Matrix.RotationZ(timeSinceStart * 3 * disco_sphere_rot_speed);
            _icoSphere._TEMPPOSITION = world;// _icoSphere._TEMPPOSITION;
            _icoSphere._TEMPPOSITION.M42 = 5;
            _icoSphere.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager.RenderIcoShader(SC_console_directx.D3D.device.ImmediateContext, _icoSphere.IndexCount, _icoSphere._TEMPPOSITION, viewMatrix, projectionMatrix, _icoVertexCount, 1);
            */
















            /*
            // Turn off the Z buffer to begin all 2D rendering.
            SC_console_directx.D3D.TurnZBufferOff();

            // Turn on the alpha blending before rendering the text.
            SC_console_directx.D3D.TurnOnAlphaBlending();

            // Render the text user interface elements.
            //if (!Text.Render(D3D.DeviceContext, FontShader, worldMatrix, orthoD3DMatrix))
            //    return false;

            // Turn off alpha blending after rendering the text.
            SC_console_directx.D3D.TurnOffAlphaBlending();

            // Turn the Z buffer back on now that all 2D rendering has completed.
            SC_console_directx.D3D.TurnZBufferOn();*/























            Quaternion quater;

            Matrix Matter = _player_rght_hnd[0][0]._arrayOfInstances[0]._LASTPOSITION;// _world_screen_list[0]._arrayOfInstances[0].current_pos;
            SharpDX.Quaternion.RotationMatrix(ref Matter, out quater);
            var matrixRight = _player_rght_hnd[0][0]._arrayOfInstances[0]._LASTPOSITION;
            Vector3 dirTo = sc_maths._getDirection(Vector3.ForwardRH, quater);
            Vector3 tempvec0 = new Vector3(matrixRight.M41, matrixRight.M42, matrixRight.M43);
            tempvec0 = tempvec0 + (dirTo * _player_rght_hnd[0][0]._total_torso_height * 3.5f);
            matrixRight.M41 = tempvec0.X;
            matrixRight.M42 = tempvec0.Y;
            matrixRight.M43 = tempvec0.Z;

            Matter = _player_lft_hnd[0][0]._arrayOfInstances[0]._LASTPOSITION;// _world_screen_list[0]._arrayOfInstances[0].current_pos;
            SharpDX.Quaternion.RotationMatrix(ref Matter, out quater);
            var matrixLeft = _player_lft_hnd[0][0]._arrayOfInstances[0]._LASTPOSITION;
            dirTo = sc_maths._getDirection(Vector3.ForwardRH, quater);
            Vector3 tempvec1 = new Vector3(matrixLeft.M41, matrixLeft.M42, matrixLeft.M43);
            tempvec1 = tempvec1 + (dirTo * _player_lft_hnd[0][0]._total_torso_height * 3.5f);
            matrixLeft.M41 = tempvec1.X;
            matrixLeft.M42 = tempvec1.Y;
            matrixLeft.M43 = tempvec1.Z;

            SharpDX.Quaternion.RotationMatrix(ref matrixLeft, out quater);

            worldMatrix_instances_containment_grid_RH[0][0][0] = matrixRight;
            _world_containment_grid_list_RH[0][0]._arrayOfInstances[0].current_pos = matrixRight;

            worldMatrix_instances_containment_grid_LH[0][0][0] = matrixLeft;
            _world_containment_grid_list_LH[0][0]._arrayOfInstances[0].current_pos = matrixLeft;







            //CONTAINMENT BOX TEST
            var _WorldMatrixContainer = _world_screen_list[0][0]._arrayOfInstances[0].current_pos;
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
                        var _WorldMatrix = WorldMatrix;

                        _WorldMatrix.M41 = xxx;
                        _WorldMatrix.M42 = yyy;
                        _WorldMatrix.M43 = zzz;

                        //SharpDX.Matrix.Multiply(ref _WorldMatrix, ref translationMatrix, out _WorldMatrix);
                        SharpDX.Matrix.Multiply(ref _WorldMatrix, ref matrixLeft, out WorldMatrix);

                        //arrayX[xii + (_widther + _widther) * (yii + (_widther + _widther) * zii)] = _WorldMatrix.M41;
                        //arrayY[xii + (_heighter + _heighter) * (yii + (_heighter + _heighter) * zii)] = _WorldMatrix.M42;
                        //arrayZ[xii + (_depther + _depther) * (yii + (_depther + _depther) * zii)] = _WorldMatrix.M43;

                        arrayX[xxi + (ChunkWidth_L + ChunkWidth_R + 1) * (yyi + (ChunkHeight_L + ChunkHeight_R + 1) * zzi)] = (float)(Math.Round(WorldMatrix.M41, 1));
                        arrayY[xxi + (ChunkWidth_L + ChunkWidth_R + 1) * (yyi + (ChunkHeight_L + ChunkHeight_R + 1) * zzi)] = (float)(Math.Round(WorldMatrix.M42, 1));
                        arrayZ[xxi + (ChunkWidth_L + ChunkWidth_R + 1) * (yyi + (ChunkHeight_L + ChunkHeight_R + 1) * zzi)] = (float)(Math.Round(WorldMatrix.M43, 1));

                    }
                }
            }


            if (display_grid_type == 0)
            {

            }
            else if (display_grid_type == 1)
            {
                Matrix screengridmatrix = _world_screen_list[0][0]._arrayOfInstances[0].current_pos;

                Vector3 tempVec = new Vector3(screengridmatrix.M41, screengridmatrix.M42, screengridmatrix.M43);
                tempVec = tempVec + (-oculusRiftDir * ((_world_screen_list[0][0]._total_torso_depth * 0.5f) + (1 * 0.00123f)));
                screengridmatrix.M41 = tempVec.X;
                screengridmatrix.M42 = tempVec.Y;
                screengridmatrix.M43 = tempVec.Z;
                //screengridmatrix.M43 += _world_screen_list[0][0]._total_torso_depth * 1.25f;
                _screen_grid_Y.Render(SC_console_directx.D3D.Device.ImmediateContext);
                SC_Update._shaderManager.RenderGrid(SC_console_directx.D3D.Device.ImmediateContext, _screen_grid_Y.IndexCount, screengridmatrix, viewMatrix, projectionMatrix);
            }
            else if (display_grid_type == 2)
            {
                Matrix screengrid_metricmatrix = _world_screen_list[0][0]._arrayOfInstances[0].current_pos;
                Vector3 tempVec = new Vector3(screengrid_metricmatrix.M41, screengrid_metricmatrix.M42, screengrid_metricmatrix.M43);
                tempVec = tempVec + (-oculusRiftDir * ((_world_screen_list[0][0]._total_torso_depth * 0.5f) + (1 * 0.00123f)));
                screengrid_metricmatrix.M41 = tempVec.X;
                screengrid_metricmatrix.M42 = tempVec.Y;
                screengrid_metricmatrix.M43 = tempVec.Z;

                //screengrid_metricmatrix.M43 += _world_screen_list[0][0]._total_torso_depth * 1.275f;
                _screen_metric_grid_Y.Render(SC_console_directx.D3D.Device.ImmediateContext);
                SC_Update._shaderManager.RenderGrid(SC_console_directx.D3D.Device.ImmediateContext, _screen_metric_grid_Y.IndexCount, screengrid_metricmatrix, viewMatrix, projectionMatrix);
            }
            else if (display_grid_type == 3)
            {

                //PHYSICS GRID
                _world_containment_grid_screen[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
                SC_Update._shaderManager.RenderInstancedContainmentGrid(SC_console_directx.D3D.device.ImmediateContext, _world_containment_grid_screen[0][0].IndexCount, _world_containment_grid_screen[0][0].InstanceCount, _world_containment_grid_screen[0][0]._POSITION, viewMatrix, projectionMatrix, SC_Update._desktopFrame._ShaderResource, _DLightBuffer_containment_grid, _world_containment_grid_screen[0][0]); // oculusRiftDir


                //PHYSICS GRID
                //_world_containment_grid_screen[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
                //SC_Update._shaderManager.RenderInstancedContainmentGrid(SC_console_directx.D3D.device.ImmediateContext, _world_containment_grid_screen[0][0].IndexCount, _world_containment_grid_screen[0][0].InstanceCount, _world_containment_grid_screen[0][0]._POSITION, viewMatrix, projectionMatrix, SC_Update._desktopFrame._ShaderResource, _DLightBuffer_containment_grid, _world_containment_grid_screen[0][0]); // oculusRiftDir

                //_dContainer.Render(SC_console_directx.D3D.device.ImmediateContext);
                //SC_Update._shaderManager.RenderObjectGrid(SC_console_directx.D3D.device.ImmediateContext, _dContainer.IndexCount, _WorldMatrixContainer, viewMatrix, projectionMatrix);
            }


            if (display_grid_type == 1 || display_grid_type == 2 || display_grid_type == 3)
            {
                //PHYSICS GRID
                _world_containment_grid_list_RH[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
                SC_Update._shaderManager.RenderInstancedContainmentGrid(SC_console_directx.D3D.device.ImmediateContext, _world_containment_grid_list_RH[0][0].IndexCount, _world_containment_grid_list_RH[0][0].InstanceCount, _world_containment_grid_list_RH[0][0]._POSITION, viewMatrix, projectionMatrix, SC_Update._desktopFrame._ShaderResource, _DLightBuffer_containment_grid, _world_containment_grid_list_RH[0][0]); // oculusRiftDir

                //PHYSICS GRID
                _world_containment_grid_list_LH[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
                SC_Update._shaderManager.RenderInstancedContainmentGrid(SC_console_directx.D3D.device.ImmediateContext, _world_containment_grid_list_LH[0][0].IndexCount, _world_containment_grid_list_LH[0][0].InstanceCount, _world_containment_grid_list_LH[0][0]._POSITION, viewMatrix, projectionMatrix, SC_Update._desktopFrame._ShaderResource, _DLightBuffer_containment_grid, _world_containment_grid_list_LH[0][0]); // oculusRiftDir

                //_dTouchRightContainer.Render(SC_console_directx.D3D.device.ImmediateContext);
                //SC_Update._shaderManager.RenderObjectGrid(SC_console_directx.D3D.device.ImmediateContext, _dTouchRightContainer.IndexCount, matrixRight, viewMatrix, projectionMatrix);
                //_dTouchLeftContainer.Render(SC_console_directx.D3D.device.ImmediateContext);
                //SC_Update._shaderManager.RenderObjectGrid(SC_console_directx.D3D.device.ImmediateContext, _dTouchLeftContainer.IndexCount, matrixLeft, viewMatrix, projectionMatrix);
            }


            /*sc_jitter_cloth._cube.Render(SC_console_directx.D3D.device.ImmediateContext);
            //_arrayOfClothCubes[i].RenderInstancedObject(SC_console_directx.D3D.device.ImmediateContext, _arrayOfClothCubes[i].IndexCount, _arrayOfClothCubes[i].InstanceCount, _arrayOfClothCubes[i]._POSITION, viewMatrix, _projectionMatrix, null, worldMatrix_Terrain_instances, _DLightBuffer, oculusRiftDir);
            SC_Update._shaderManager.RenderInstancedObject(SC_console_directx.D3D.device.ImmediateContext, sc_jitter_cloth._cube.IndexCount, sc_jitter_cloth._cube.InstanceCount, sc_jitter_cloth._cube._POSITION, viewMatrix, projectionMatrix, SC_Update._desktopFrame._ShaderResource, _DLightBuffer_cube, sc_jitter_cloth._cube); // oculusRiftDir
            */

            //PHYSICS SCREENS
            _world_screen_list[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager.RenderInstancedObject(SC_console_directx.D3D.device.ImmediateContext, _world_screen_list[0][0].IndexCount, _world_screen_list[0][0].InstanceCount, _world_screen_list[0][0]._POSITION, viewMatrix, projectionMatrix, SC_Update._desktopFrame._ShaderResource, _DLightBuffer_cube, _world_screen_list[0][0]);
            //END OF 

            //PHYSICS SCREEN ASSETS
            _world_screen_assets_list[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager.RenderInstancedObject(SC_console_directx.D3D.device.ImmediateContext, _world_screen_assets_list[0][0].IndexCount, _world_screen_assets_list[0][0].InstanceCount, _world_screen_assets_list[0][0]._POSITION, viewMatrix, projectionMatrix, null, _DLightBuffer_cube, _world_screen_assets_list[0][0]);
            //END OF




            for (int xx = 0; xx < Program._physics_engine_instance_x; xx++)
            {
                for (int yy = 0; yy < Program._physics_engine_instance_y; yy++)
                {
                    for (int zz = 0; zz < Program._physics_engine_instance_z; zz++)
                    {
                        var indexer00 = xx + Program._physics_engine_instance_x * (yy + Program._physics_engine_instance_y * zz);

                        try
                        {
                            for (int x = 0; x < Program.world_width; x++)
                            {
                                for (int y = 0; y < Program.world_height; y++)
                                {
                                    for (int z = 0; z < Program.world_depth; z++)
                                    {
                                        var indexer01 = x + Program.world_width * (y + Program.world_height * z);

                                        Vector3 playerPos = new Vector3(_player_torso[0][0]._arrayOfInstances[0].current_pos.M41, _player_torso[0][0]._arrayOfInstances[0].current_pos.M42, _player_torso[0][0]._arrayOfInstances[0].current_pos.M43);



                                        //PHYSICS CUBES
                                        _world_cube_list[indexer00][indexer01].Render(SC_console_directx.D3D.device.ImmediateContext);
                                        SC_Update._shaderManager.RenderInstancedObject(SC_console_directx.D3D.device.ImmediateContext, _world_cube_list[indexer00][indexer01].IndexCount, _world_cube_list[indexer00][indexer01].InstanceCount, _world_cube_list[indexer00][indexer01]._POSITION, viewMatrix, projectionMatrix, SC_Update._desktopFrame._ShaderResource, _DLightBuffer_cube, _world_cube_list[indexer00][indexer01]); // oculusRiftDir

                                        //PHYSICS CONES
                                        _world_cone_list[indexer00][indexer01].Render(SC_console_directx.D3D.device.ImmediateContext);
                                        SC_Update._shaderManager.RenderInstancedObject(SC_console_directx.D3D.device.ImmediateContext, _world_cone_list[indexer00][indexer01].IndexCount, _world_cone_list[indexer00][indexer01].InstanceCount, _world_cone_list[indexer00][indexer01]._POSITION, viewMatrix, projectionMatrix, SC_Update._desktopFrame._ShaderResource, _DLightBuffer_cube, _world_cone_list[indexer00][indexer01]); // oculusRiftDir

                                        //PHYSICS CYLINDERS
                                        _world_cylinder_list[indexer00][indexer01].Render(SC_console_directx.D3D.device.ImmediateContext);
                                        SC_Update._shaderManager.RenderInstancedObject(SC_console_directx.D3D.device.ImmediateContext, _world_cylinder_list[indexer00][indexer01].IndexCount, _world_cylinder_list[indexer00][indexer01].InstanceCount, _world_cylinder_list[indexer00][indexer01]._POSITION, viewMatrix, projectionMatrix, SC_Update._desktopFrame._ShaderResource, _DLightBuffer_cube, _world_cylinder_list[indexer00][indexer01]); // oculusRiftDir

                                        //PHYSICS CAPSULES
                                        _world_capsule_list[indexer00][indexer01].Render(SC_console_directx.D3D.device.ImmediateContext);
                                        SC_Update._shaderManager.RenderInstancedObject(SC_console_directx.D3D.device.ImmediateContext, _world_capsule_list[indexer00][indexer01].IndexCount, _world_capsule_list[indexer00][indexer01].InstanceCount, _world_capsule_list[indexer00][indexer01]._POSITION, viewMatrix, projectionMatrix, SC_Update._desktopFrame._ShaderResource, _DLightBuffer_cube, _world_capsule_list[indexer00][indexer01]); // oculusRiftDir

                                        //PHYSICS SPHERES
                                        _world_sphere_list[indexer00][indexer01].Render(SC_console_directx.D3D.device.ImmediateContext);
                                        SC_Update._shaderManager.RenderInstancedObject(SC_console_directx.D3D.device.ImmediateContext, _world_sphere_list[indexer00][indexer01].IndexCount, _world_sphere_list[indexer00][indexer01].InstanceCount, _world_sphere_list[indexer00][indexer01]._POSITION, viewMatrix, projectionMatrix, SC_Update._desktopFrame._ShaderResource, _DLightBuffer_cube, _world_sphere_list[indexer00][indexer01]); // oculusRiftDir

                                        //distance = sc_maths.sc_check_distance_node_3d_geometry(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, maxx, maxy, maxz);

                                        /*try
                                        {
                                            //PHYSICS VOXEL CUBES 
                                            //////////////////////about 100 ticks more per loop compared to simple physics cubes? will investigate later as when i do 
                                            //////////////////////simple cubes with the chunk it lags more even though the number of vertices are the same as the physics cube up above
                                            //////////////////////todo: culling of faces by distance from player. etc.

                                            _world_voxel_cube_lists[indexer00][indexer01].Render(SC_console_directx.D3D.device.ImmediateContext);
                                            SC_Update._shaderManager.RenderInstancedObjectsc_perko_voxel(SC_console_directx.D3D.device.ImmediateContext, _world_voxel_cube_lists[indexer00][indexer01].IndexCount, _world_voxel_cube_lists[indexer00][indexer01].InstanceCount, _world_voxel_cube_lists[indexer00][indexer01]._POSITION, viewMatrix, projectionMatrix, SC_Update._desktopFrame._ShaderResource, _DLightBuffer_voxel_cube, _world_voxel_cube_lists[indexer00][indexer01]);
                                            ///Console.WriteLine(_SystemTickPerformance.ElapsedTicks);
                                        }
                                        catch (Exception ex)
                                        {
                                            Program.MessageBox((IntPtr)0, ex.ToString() + "", "Oculus error", 0);
                                        }*/





                                        //////////////TO READD
                                        //////////////TO READD
                                        //////////////TO READD
                                        for (int yc = -PlanetChunkHeight_L; yc <= PlanetChunkHeight_R; yc += realplanetwidth)
                                        {
                                            for (int xc = -PlanetChunkWidth_L; xc <= PlanetChunkWidth_R; xc += realplanetwidth)
                                            {
                                                for (int zc = -PlanetChunkDepth_L; zc <= PlanetChunkDepth_R; zc += realplanetwidth)
                                                {
                                                    var xxc = xc;
                                                    var yyc = yc;
                                                    var zzc = zc;

                                                    if (xxc < 0)
                                                    {
                                                        xxc *= -1;
                                                        xxc = (PlanetChunkWidth_R) + xxc;
                                                    }
                                                    if (yyc < 0)
                                                    {
                                                        yyc *= -1;
                                                        yyc = (PlanetChunkHeight_R) + yyc;
                                                    }
                                                    if (zzc < 0)
                                                    {
                                                        zzc *= -1;
                                                        zzc = (PlanetChunkDepth_R) + zzc;
                                                    }
                                                    int pc = xxc + (PlanetChunkWidth_L + PlanetChunkWidth_R + 1) * (yyc + (PlanetChunkHeight_L + PlanetChunkHeight_R + 1) * zzc);

                                                    Vector3 chunkPos = new Vector3(arrayOfPlanetChunk[pc].current_pos.M41, arrayOfPlanetChunk[pc].current_pos.M42, arrayOfPlanetChunk[pc].current_pos.M43);


                                                    //if (Vector3.Distance(chunkPos, playerPos) < 30)
                                                    var dist = sc_maths.sc_check_distance_node_3d_geometry(chunkPos, playerPos, 350, 350, 350, 350, 350, 350);
                                                    //Console.WriteLine(dist);
                                                    if (dist < 7500)
                                                    {
                                                        if (arrayOfPlanetChunk[pc] != null)
                                                        {
                                                            if (arrayOfPlanetChunk[pc].Vertices != null)
                                                            {
                                                                if (arrayOfPlanetChunk[pc].Vertices.Length > 0)
                                                                {
                                                                    arrayOfPlanetChunk[pc].Render(SC_console_directx.D3D.device.ImmediateContext);
                                                                    //SC_Update._shaderManager.RenderInstancedObjectsc_voxel_pchunk(SC_console_directx.D3D.device.ImmediateContext, _world_voxel_cube_lists[indexer00][indexer01].IndexCount, _world_voxel_cube_lists[indexer00][indexer01].InstanceCount, _world_voxel_cube_lists[indexer00][indexer01]._POSITION, viewMatrix, projectionMatrix, SC_Update._desktopFrame._ShaderResource, _DLightBuffer_voxel_pchunk_cube, arrayOfPlanetChunk[pc]);
                                                                    SC_Update._shaderManager.RenderInstancedObjectsc_voxel_pchunk(SC_console_directx.D3D.device.ImmediateContext, arrayOfPlanetChunk[pc].IndexCount, arrayOfPlanetChunk[pc].InstanceCount, arrayOfPlanetChunk[pc]._POSITION, viewMatrix, projectionMatrix, SC_Update._desktopFrame._ShaderResource, _DLightBuffer_voxel_pchunk_cube, arrayOfPlanetChunk[pc]);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        //////////////TO READD
                                        //////////////TO READD
                                        //////////////TO READD



                                        /*for (int pc = 0; pc < arrayOfPlanetChunk.Length; pc++)
                                        {
                                            if (arrayOfPlanetChunk[pc] != null)
                                            {
                                                if (arrayOfPlanetChunk[pc].Vertices != null)
                                                {
                                                    if (arrayOfPlanetChunk[pc].Vertices.Length > 0)
                                                    {
                                                        arrayOfPlanetChunk[pc].Render(SC_console_directx.D3D.device.ImmediateContext);
                                                        //SC_Update._shaderManager.RenderInstancedObjectsc_voxel_pchunk(SC_console_directx.D3D.device.ImmediateContext, _world_voxel_cube_lists[indexer00][indexer01].IndexCount, _world_voxel_cube_lists[indexer00][indexer01].InstanceCount, _world_voxel_cube_lists[indexer00][indexer01]._POSITION, viewMatrix, projectionMatrix, SC_Update._desktopFrame._ShaderResource, _DLightBuffer_voxel_pchunk_cube, arrayOfPlanetChunk[pc]);
                                                        SC_Update._shaderManager.RenderInstancedObjectsc_voxel_pchunk(SC_console_directx.D3D.device.ImmediateContext, arrayOfPlanetChunk[pc].IndexCount, arrayOfPlanetChunk[pc].InstanceCount, arrayOfPlanetChunk[pc]._POSITION, viewMatrix, projectionMatrix, SC_Update._desktopFrame._ShaderResource, _DLightBuffer_voxel_pchunk_cube, arrayOfPlanetChunk[pc]);

                                                    }
                                                }
                                            }
                                        }*/
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
            }









            finalRotationMatrix = originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdrotMatrix;

            ////////////////////
            /////HUMAN RIG////// 
            ////////////////////
            for (int _iterator = 0; _iterator < _player_rght_hnd[0][0]._arrayOfInstances.Length; _iterator++) //
            {
                var lengthOfLowerArmRight = _player_rght_lower_arm[0][0]._total_torso_height * 2.0f;
                var lengthOfUpperArmRight = _player_rght_upper_arm[0][0]._total_torso_height * 2.25f;
                var totalArmLengthRight = lengthOfLowerArmRight + lengthOfUpperArmRight;

                var lengthOfLowerArmLeft = _player_lft_lower_arm[0][0]._total_torso_height * 2.0f;
                var lengthOfUpperArmLeft = _player_lft_upper_arm[0][0]._total_torso_height * 2.25f;
                var totalArmLengthLeft = lengthOfLowerArmLeft + lengthOfUpperArmLeft;

                var connectorOfUpperArmRightOffsetMul = 1.55f; //1.55f
                var connectorOfLowerArmRightOffsetMul = 1.55f; //0.70f
                var connectorOfHandOffsetMul = 1.00123f; // 1.00123f

                var connectorOfUpperLegOffsetMul = 0.75f;
                var connectorOfLowerLegOffsetMul = 0.75f;

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

                //I AM USING THE NEW PIVOT POINT AT THE BOTTOM OF THE SPINE AND ADDING THE FRONT/RIGHT/UP VECTOR OF THE ROTATION OF THAT SPINE AND THEN ADDING THE DIFFERENCE X/W/Z BETWEEN ORIGINAL POS AND THE NEW PIVOT POS
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right_torso * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_torso * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward_torso * (diffNormPosZ));

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


                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;

                var matrixerer = finalRotationMatrix;

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

                ///////////
                //SOMETESTS
                Quaternion.RotationMatrix(ref finalRotationMatrix, out otherQuat);
                var direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                var direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                var direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);

                current_rotation_of_torso_pivot_forward = direction_feet_forward;
                current_rotation_of_torso_pivot_right = direction_feet_right;
                current_rotation_of_torso_pivot_up = direction_feet_up;
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
                realPosOfHead.X += OFFSETPOS.X;
                realPosOfHead.Y += OFFSETPOS.Y;
                realPosOfHead.Z += OFFSETPOS.Z;
                finalHMDMatrix = hmd_matrix_current * finalRotationMatrix;
                Quaternion.RotationMatrix(ref finalHMDMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                var theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right, direction_head_up);
                theheadrotmatrix.Invert();
                matrixerer = theheadrotmatrix;
                realPosOfHead = realPosOfHead + (direction_head_up * (_player_head[0][0]._total_torso_depth * 4));
                realPosOfHead = realPosOfHead + (-current_rotation_of_torso_pivot_up * (_player_head[0][0]._total_torso_depth * 4));
                matrixerer.M41 = realPosOfHead.X;
                matrixerer.M42 = realPosOfHead.Y;
                matrixerer.M43 = realPosOfHead.Z;
                worldMatrix_instances_head[0][0][_iterator] = matrixerer;
                _player_head[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;








                /////////////////////
                //////HANDRIGHT//////
                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                Matrix someMatRight = _rightTouchMatrix;
                someMatRight.M41 = handPoseRight.Position.X + MOVINGPOINTER.X;
                someMatRight.M42 = handPoseRight.Position.Y;// + MOVINGPOINTER.Y;
                someMatRight.M43 = handPoseRight.Position.Z + MOVINGPOINTER.Z;
                diffNormPosX = (MOVINGPOINTER.X) - someMatRight.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - someMatRight.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - someMatRight.M43;
                MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_right * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_up * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + (current_rotation_of_torso_pivot_forward * (diffNormPosZ));
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
                matrixerer = someMatRight * finalRotationMatrix;


                someMatRight.M41 += OFFSETPOS.X;
                someMatRight.M42 += OFFSETPOS.Y;
                someMatRight.M43 += OFFSETPOS.Z;

                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;
                matrixerer.M44 = 1;
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
                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                someMatRight = _rightTouchMatrix;
                someMatRight.M41 = handPoseRight.Position.X + MOVINGPOINTER.X;
                someMatRight.M42 = handPoseRight.Position.Y;// + MOVINGPOINTER.Y;
                someMatRight.M43 = handPoseRight.Position.Z + MOVINGPOINTER.Z;
                diffNormPosX = (MOVINGPOINTER.X) - someMatRight.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - someMatRight.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - someMatRight.M43;
                MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_right * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_up * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + (current_rotation_of_torso_pivot_forward * (diffNormPosZ));
                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;
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
                matrixerer = someMatRight * finalRotationMatrix;
                //someMatRight.M41 = MOVINGPOINTER.X;
                //someMatRight.M42 = MOVINGPOINTER.Y;
                //someMatRight.M43 = MOVINGPOINTER.Z;
                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;
                matrixerer.M44 = 1;
                worldMatrix_instances_r_hand_grab[0][0][_iterator] = matrixerer;// _player_pelvis[0][0].current_pos;// translationMatrix;
                _player_r_hand_grab[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
                _player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION = matrixerer;
                _player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._REALCENTERPOSITION = someMatRight;
                _player_r_hand_grab[0][0]._arrayOfInstances[_iterator]._TEMPPOSITION = someMatRight;










                /////////////////////////
                //////HANDLEFTGRAB//////
                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                someMatRight = _leftTouchMatrix;
                //someMatRight.M41 = handPoseLeft.Position.X + MOVINGPOINTER.X;
                //someMatRight.M42 = handPoseLeft.Position.Y;// + MOVINGPOINTER.Y;
                //someMatRight.M43 = handPoseLeft.Position.Z + MOVINGPOINTER.Z;
                diffNormPosX = (MOVINGPOINTER.X) - someMatRight.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - someMatRight.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - someMatRight.M43;
                MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_right * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_up * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + (current_rotation_of_torso_pivot_forward * (diffNormPosZ));
                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;
                posRHand = new Vector3(_player_l_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M41, _player_l_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M42, _player_l_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M43);
                tempDir = posRHand - _player_lft_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;
                if (tempDir.Length() > lengthOfLowerArmRight * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
                {
                    tempDir.Normalize();
                    var somePosOfSHLDR = new Vector3(_player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                    Vector3 tempVect = somePosOfSHLDR + (tempDir * ((lengthOfLowerArmRight * 1.0923f) + (lengthOfUpperArmRight * 1.0923f)));
                    MOVINGPOINTER.X = tempVect.X;
                    MOVINGPOINTER.Y = tempVect.Y;
                    MOVINGPOINTER.Z = tempVect.Z;
                }
                matrixerer = someMatRight * finalRotationMatrix;
                someMatRight.M41 = MOVINGPOINTER.X;
                someMatRight.M42 = MOVINGPOINTER.Y;
                someMatRight.M43 = MOVINGPOINTER.Z;
                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;
                matrixerer.M44 = 1;
                worldMatrix_instances_l_hand_grab[0][0][_iterator] = matrixerer;// _player_pelvis[0][0].current_pos;// translationMatrix;
                _player_l_hand_grab[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
                _player_l_hand_grab[0][0]._arrayOfInstances[_iterator]._LASTPOSITION = matrixerer;
                _player_l_hand_grab[0][0]._arrayOfInstances[_iterator]._REALCENTERPOSITION = someMatRight;
                _player_l_hand_grab[0][0]._arrayOfInstances[_iterator]._TEMPPOSITION = someMatRight;























                //HANDLEFT
                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                someMatRight = _leftTouchMatrix;
                someMatRight.M41 = handPoseLeft.Position.X + MOVINGPOINTER.X;
                someMatRight.M42 = handPoseLeft.Position.Y;
                someMatRight.M43 = handPoseLeft.Position.Z + MOVINGPOINTER.Z;
                diffNormPosX = (MOVINGPOINTER.X) - someMatRight.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - someMatRight.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - someMatRight.M43;
                MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_right * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_up * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + (current_rotation_of_torso_pivot_forward * (diffNormPosZ));
                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;
                posRHand = new Vector3(_player_lft_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M41, _player_lft_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M42, _player_lft_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION.M43);
                tempDir = posRHand - _player_lft_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;
                if (tempDir.Length() > lengthOfLowerArmRight * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
                {
                    tempDir.Normalize();
                    var somePosOfSHLDR = new Vector3(_player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                    Vector3 tempVect = somePosOfSHLDR + (tempDir * ((lengthOfLowerArmRight * 1.0923f) + (lengthOfUpperArmRight * 1.0923f)));
                    MOVINGPOINTER.X = tempVect.X;
                    MOVINGPOINTER.Y = tempVect.Y;
                    MOVINGPOINTER.Z = tempVect.Z;
                }
                matrixerer = Matrix.Identity;
                someMatRight = someMatRight * finalRotationMatrix;
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
                Quaternion.RotationMatrix(ref _body_pos, out _quat);
                _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                matrixIn = JMatrix.CreateFromQuaternion(_other_quat);
                worldMatrix_instances_l_hand[0][0][_iterator] = matrixerer;
                _player_lft_hnd[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
                _player_lft_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITION = matrixerer;
                _player_lft_hnd[0][0]._arrayOfInstances[_iterator]._REALCENTERPOSITION = someMatRight;
                //if (swtch_for_last_pos[0][0][_iterator] > 0)
                //{
                //    _player_lft_hnd[0][0]._arrayOfInstances[_iterator]._LASTPOSITIONFORPHYSICS = matrixerer;
                //    swtch_for_last_pos[0][0][_iterator] = 0;
                //}
                //swtch_for_last_pos[0][0][_iterator]++;





                ///////////
                //LEFT SHOULDER
                _rotMatrixer = _player_lft_shldr[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                _SC_modL_lft_shldr_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_lft_shldr[0][0]._POSITION.M41, _player_lft_shldr[0][0]._POSITION.M42, _player_lft_shldr[0][0]._POSITION.M43),
                    padding1 = 100
                };
                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                torsooripos = MOVINGPOINTER;
                _rotMatrixer = _player_lft_shldr[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                diffNormPosX = (MOVINGPOINTER.X) - _player_lft_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_lft_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_lft_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                tempPoint = MOVINGPOINTER;
                tempPoint = tempPoint + -(direction_feet_right * (diffNormPosX));
                tempPoint = tempPoint + -(direction_feet_up * (diffNormPosY));
                tempPoint = tempPoint + -(direction_feet_forward * (diffNormPosZ));
                dirToPoint = tempPoint - torsooripos;
                dirToPoint.Normalize();
                var realPosOfLS = TORSOPIVOT + (dirToPoint * ((_player_torso[0][0]._total_torso_height + (_player_lft_shldr[0][0]._total_torso_height * 4))));
                pivotOfHead = TORSOPIVOT + (dirToPoint * ((_player_torso[0][0]._total_torso_height)));
                realPosOfLS.X += OFFSETPOS.X;
                realPosOfLS.Y += OFFSETPOS.Y;
                realPosOfLS.Z += OFFSETPOS.Z;
                var shoulderRot = _player_lft_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT;
                Quaternion.RotationMatrix(ref shoulderRot, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right, direction_head_up);
                theheadrotmatrix.Invert();
                matrixerer = theheadrotmatrix;
                realPosOfLS = realPosOfLS + (-current_rotation_of_torso_pivot_right * (_player_lft_shldr[0][0]._total_torso_width));
                realPosOfLS = realPosOfLS + (-current_rotation_of_torso_pivot_up * (_player_lft_shldr[0][0]._total_torso_height * 0.75f));
                matrixerer.M41 = realPosOfLS.X;
                matrixerer.M42 = realPosOfLS.Y;
                matrixerer.M43 = realPosOfLS.Z;
                worldMatrix_instances_l_shoulder[0][0][_iterator] = matrixerer;
                _player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;










                //RIGHT SHOULDER
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
                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                torsooripos = MOVINGPOINTER;
                _rotMatrixer = _player_rght_shldr[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                diffNormPosX = (MOVINGPOINTER.X) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_shldr[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                tempPoint = MOVINGPOINTER;
                tempPoint = tempPoint + -(direction_feet_right * (diffNormPosX));
                tempPoint = tempPoint + -(direction_feet_up * (diffNormPosY));
                tempPoint = tempPoint + -(direction_feet_forward * (diffNormPosZ));
                dirToPoint = tempPoint - torsooripos;
                dirToPoint.Normalize();
                var realPosOfRS = TORSOPIVOT + (dirToPoint * ((_player_torso[0][0]._total_torso_height + (_player_rght_shldr[0][0]._total_torso_height * 4))));
                pivotOfHead = TORSOPIVOT + (dirToPoint * ((_player_torso[0][0]._total_torso_height)));
                realPosOfRS.X += OFFSETPOS.X;
                realPosOfRS.Y += OFFSETPOS.Y;
                realPosOfRS.Z += OFFSETPOS.Z;

                var shoulderMatrix = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT;
                Quaternion.RotationMatrix(ref shoulderMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                theheadrotmatrix = Matrix.LookAtRH(pivotOfHead, pivotOfHead + direction_head_right, direction_head_up);
                theheadrotmatrix.Invert();
                matrixerer = theheadrotmatrix;
                realPosOfRS = realPosOfRS + (current_rotation_of_torso_pivot_right * (_player_rght_shldr[0][0]._total_torso_width));
                realPosOfRS = realPosOfRS + (-current_rotation_of_torso_pivot_up * (_player_rght_shldr[0][0]._total_torso_height * 0.75f));
                matrixerer.M41 = realPosOfRS.X;
                matrixerer.M42 = realPosOfRS.Y;
                matrixerer.M43 = realPosOfRS.Z;
                worldMatrix_instances_r_shoulder[0][0][_iterator] = matrixerer;
                _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;






                //////////////////////
                //ELBOW TARGET RIGHT
                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                TORSOPIVOT = MOVINGPOINTER;
                _rotMatrixer = _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_rght_elbow_target[0][0]._total_torso_height * 0.5f));
                diffNormPosX = (MOVINGPOINTER.X) - _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * (diffNormPosZ));
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
                var someNewPointer = MOVINGPOINTER;
                var diffNormPosXElbowRight = (_player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M41) - (TORSOPIVOT.X);
                var diffNormPosYElbowRight = (_player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M42) - (TORSOPIVOT.Y);
                var diffNormPosZElbowRight = (_player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M43) - (TORSOPIVOT.Z);
                MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));
                matrixerer = finalRotationMatrix;
                matrixerer.M41 = someNewPointer.X;
                matrixerer.M42 = someNewPointer.Y;
                matrixerer.M43 = someNewPointer.Z;
                matrixerer.M44 = 1;
                worldMatrix_instances_r_elbow_target[0][0][_iterator] = matrixerer;
                _player_rght_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;


                //////////////////////////
                //ELBOW TARGET RIGHT TWO
                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                TORSOPIVOT = MOVINGPOINTER;
                _rotMatrixer = _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_rght_elbow_target_two[0][0]._total_torso_height * 0.5f));
                diffNormPosX = (MOVINGPOINTER.X) - _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M43;
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * (diffNormPosZ));
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
                someDiffX = MOVINGPOINTER.X - _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41;
                someDiffY = MOVINGPOINTER.Y - _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42;
                someDiffZ = MOVINGPOINTER.Z - _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43;
                somePosOfRightHand = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                dirShoulderToHand = somePosOfRightHand - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT;
                dirShoulderToHand.Normalize();
                MOVINGPOINTER = _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT + (dirShoulderToHand * totalArmLengthRight * 2);
                var someOffsetter = somePosOfRightHand - OFFSETPOS;
                var someOtherPivotPoint = MOVINGPOINTER;
                someNewPointer = MOVINGPOINTER;
                diffNormPosXElbowRight = (_player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41) - (TORSOPIVOT.X);
                diffNormPosYElbowRight = (_player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42) - (TORSOPIVOT.Y);
                diffNormPosZElbowRight = (_player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43) - (TORSOPIVOT.Z);
                MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));
                matrixerer = finalRotationMatrix;
                matrixerer.M41 = someNewPointer.X;
                matrixerer.M42 = someNewPointer.Y;
                matrixerer.M43 = someNewPointer.Z;
                matrixerer.M44 = 1;
                _body_pos = matrixerer;
                Quaternion.RotationMatrix(ref _body_pos, out _quat);
                _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                matrixIn = JMatrix.CreateFromQuaternion(_other_quat);
                worldMatrix_instances_r_elbow_target_two[0][0][_iterator] = matrixerer;
                _player_rght_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;






                //////////////////
                //UPPER ARM RIGHT
                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
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
                diffNormPosXElbowRight = (_player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41) - (TORSOPIVOT.X);
                diffNormPosYElbowRight = (_player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42) - (TORSOPIVOT.Y);
                diffNormPosZElbowRight = (_player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43) - (TORSOPIVOT.Z);
                MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));
                var elbowPositionRight = someNewPointer;
                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION = elbowPositionRight;
                var dirPivotUpperRIghtToElbowRight = elbowPositionRight - currentFINALPIVOTUPPERARM;
                var currentPositionOfUPPERARMROTATION3DPOSITION = currentFINALPIVOTUPPERARM + (dirPivotUpperRIghtToElbowRight * 0.5f);
                var dirElbowRightToHand = somePosOfRightHand - elbowPositionRight;
                dirPivotUpperRIghtToElbowRight.Normalize();
                dirElbowRightToHand.Normalize();
                Vector3 someCross0;
                Vector3.Cross(ref dirPivotUpperRIghtToElbowRight, ref dirElbowRightToHand, out someCross0);
                someCross0.Normalize();
                _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWCROSSVEC = someCross0;
                Vector3 someCross1;
                Vector3.Cross(ref dirPivotUpperRIghtToElbowRight, ref someCross0, out someCross1);
                someCross1.Normalize();
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
                newpoint = newpoint + (newdir * lengthOfLowerArmRight * 0.5f);
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



                //////////////////////
                //ELBOW TARGET LEFT
                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                TORSOPIVOT = MOVINGPOINTER;
                _rotMatrixer = _player_lft_elbow_target[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_lft_elbow_target[0][0]._total_torso_height * 0.5f));
                diffNormPosX = (MOVINGPOINTER.X) - _player_lft_elbow_target[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_lft_elbow_target[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_lft_elbow_target[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * (diffNormPosZ));
                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;
                someDiffX = MOVINGPOINTER.X - _player_lft_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                someDiffY = MOVINGPOINTER.Y - _player_lft_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                someDiffZ = MOVINGPOINTER.Z - _player_lft_hnd[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                somePosOfPivotUpperArm = new Vector3(_player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43); //new Vector3(realPIVOTOfUpperArm.X, realPIVOTOfUpperArm.Y, realPIVOTOfUpperArm.Z); ;// new Vector3(_player_rght_shldr.current_pos.M41, _player_rght_shldr.current_pos.M42, _player_rght_shldr.current_pos.M43);
                somePosOfRightHand = new Vector3(_player_lft_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                _player_lft_upper_arm[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT = somePosOfPivotUpperArm;
                dirShoulderToHand = somePosOfRightHand - somePosOfPivotUpperArm;
                dirShoulderToHand *= -1;
                MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * 3.0f);
                someNewPointer = MOVINGPOINTER;
                diffNormPosXElbowRight = (_player_lft_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M41) - (TORSOPIVOT.X);
                diffNormPosYElbowRight = (_player_lft_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M42) - (TORSOPIVOT.Y);
                diffNormPosZElbowRight = (_player_lft_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M43) - (TORSOPIVOT.Z);
                MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));
                matrixerer = finalRotationMatrix;
                matrixerer.M41 = someNewPointer.X;
                matrixerer.M42 = someNewPointer.Y;
                matrixerer.M43 = someNewPointer.Z;
                matrixerer.M44 = 1;
                _body_pos = matrixerer;
                Quaternion.RotationMatrix(ref _body_pos, out _quat);
                _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                matrixIn = JMatrix.CreateFromQuaternion(_other_quat);
                worldMatrix_instances_l_elbow_target[0][0][_iterator] = matrixerer;
                _player_lft_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;






                //////////////////////////
                //ELBOW TARGET LEFT TWO
                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                TORSOPIVOT = MOVINGPOINTER;
                _rotMatrixer = _player_lft_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_ori * (_player_lft_elbow_target_two[0][0]._total_torso_height * 0.5f));
                diffNormPosX = (MOVINGPOINTER.X) - _player_lft_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_lft_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_lft_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M43;
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up * (diffNormPosY));
                MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_forward * (diffNormPosZ));
                xq = otherQuat.X;
                yq = otherQuat.Y;
                zq = otherQuat.Z;
                wq = otherQuat.W;
                pitcha = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
                yawa = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
                rolla = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
                hyp = (float)(diffNormPosY / Math.Cos(pitcha));
                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;
                someDiffX = MOVINGPOINTER.X - _player_lft_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41;
                someDiffY = MOVINGPOINTER.Y - _player_lft_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42;
                someDiffZ = MOVINGPOINTER.Z - _player_lft_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43;
                somePosOfRightHand = new Vector3(_player_lft_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43); dirShoulderToHand = somePosOfRightHand - _player_lft_upper_arm[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT;
                dirShoulderToHand.Normalize();
                MOVINGPOINTER = _player_lft_upper_arm[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT + (dirShoulderToHand * totalArmLengthRight * 2);
                someOffsetter = somePosOfRightHand - OFFSETPOS;
                someOtherPivotPoint = MOVINGPOINTER;
                someNewPointer = MOVINGPOINTER;
                diffNormPosXElbowRight = (_player_lft_elbow_target_two[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41) - (TORSOPIVOT.X);
                diffNormPosYElbowRight = (_player_lft_elbow_target_two[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42) - (TORSOPIVOT.Y);
                diffNormPosZElbowRight = (_player_lft_elbow_target_two[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43) - (TORSOPIVOT.Z);
                MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));
                matrixerer = finalRotationMatrix;
                matrixerer.M41 = someNewPointer.X;
                matrixerer.M42 = someNewPointer.Y;
                matrixerer.M43 = someNewPointer.Z;
                matrixerer.M44 = 1;
                _body_pos = matrixerer;
                Quaternion.RotationMatrix(ref _body_pos, out _quat);
                _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                matrixIn = JMatrix.CreateFromQuaternion(_other_quat);
                worldMatrix_instances_l_elbow_target_two[0][0][_iterator] = matrixerer;
                _player_lft_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;












                //////////////////
                //UPPER ARM LEFT
                MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                TORSOPIVOT = MOVINGPOINTER;
                _rotMatrixer = _player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                Quaternion.RotationMatrix(ref finalRotationMatrix, out otherQuat);
                direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                test = MOVINGPOINTER + OFFSETPOS;
                diffNormPosX = (test.X) - _player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41;
                diffNormPosY = (test.Y) - _player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42;
                diffNormPosZ = (test.Z) - _player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43;
                realPIVOTOfUpperArm = MOVINGPOINTER;
                realPositionOfUpperArm = MOVINGPOINTER + -(direction_feet_right * (diffNormPosX));
                realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_up * (diffNormPosY));
                realPositionOfUpperArm = realPositionOfUpperArm + -(direction_feet_forward * (diffNormPosZ));
                realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_right * (diffNormPosX));
                realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_up * (diffNormPosY));
                realPIVOTOfUpperArm = realPIVOTOfUpperArm + -(direction_feet_forward * (diffNormPosZ));
                realPIVOTOfUpperArm = realPIVOTOfUpperArm + (direction_feet_up_ori * (_player_lft_shldr[0][0]._total_torso_height * connectorOfUpperArmRightOffsetMul));
                realPIVOTOfUpperArm.X = realPIVOTOfUpperArm.X + OFFSETPOS.X;
                realPIVOTOfUpperArm.Y = realPIVOTOfUpperArm.Y + OFFSETPOS.Y;
                realPIVOTOfUpperArm.Z = realPIVOTOfUpperArm.Z + OFFSETPOS.Z;
                currentFINALPIVOTUPPERARM = new Vector3(_player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43) + (direction_feet_up_ori * (_player_lft_shldr[0][0]._total_torso_height * connectorOfUpperArmRightOffsetMul));
                realPIVOTOfUpperArm = currentFINALPIVOTUPPERARM;
                _player_lft_upper_arm[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT = currentFINALPIVOTUPPERARM;
                somePosOfRightHand = new Vector3(_player_lft_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                somePosOfUpperElbowTargetTwo = new Vector3(_player_lft_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_elbow_target_two[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                somePosOfUpperElbowTargetOne = new Vector3(_player_lft_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_elbow_target[0][0]._arrayOfInstances[_iterator].current_pos.M43);
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
                _player_lft_upper_arm[0][0]._arrayOfInstances[_iterator]._ARMLENGTH = totalArmLengthRight;
                lengthOfDirFromPivotUpperToHand = Math.Min(lengthOfDirFromPivotUpperToHand, totalArmLengthRight - totalArmLengthRight * 0.001f);
                upperEquationCirCirIntersect = (lengthOfDirFromPivotUpperToHand * lengthOfDirFromPivotUpperToHand) - (lengthOfLowerArmRight * lengthOfLowerArmRight) + (lengthOfUpperArmRight * lengthOfUpperArmRight);
                adjacentSolvingForX = upperEquationCirCirIntersect / (2 * lengthOfDirFromPivotUpperToHand);
                adjacentSolvingForX = Math.Min(adjacentSolvingForX, lengthOfUpperArmRight - lengthOfUpperArmRight * 0.001f);
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
                diffNormPosXElbowRight = (_player_lft_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos.M41) - (TORSOPIVOT.X);
                diffNormPosYElbowRight = (_player_lft_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos.M42) - (TORSOPIVOT.Y);
                diffNormPosZElbowRight = (_player_lft_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos.M43) - (TORSOPIVOT.Z);
                MOVINGPOINTER = TORSOPIVOT.X + -(current_rotation_of_torso_pivot_right * (diffNormPosXElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Y + -(current_rotation_of_torso_pivot_up * (diffNormPosYElbowRight));
                MOVINGPOINTER = TORSOPIVOT.Z + -(current_rotation_of_torso_pivot_forward * (diffNormPosZElbowRight));
                elbowPositionRight = someNewPointer;
                _player_lft_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION = elbowPositionRight;
                dirPivotUpperRIghtToElbowRight = elbowPositionRight - currentFINALPIVOTUPPERARM;
                currentPositionOfUPPERARMROTATION3DPOSITION = currentFINALPIVOTUPPERARM + (dirPivotUpperRIghtToElbowRight * 0.5f);
                dirElbowRightToHand = somePosOfRightHand - elbowPositionRight;
                dirPivotUpperRIghtToElbowRight.Normalize();
                dirElbowRightToHand.Normalize();
                Vector3.Cross(ref dirPivotUpperRIghtToElbowRight, ref dirElbowRightToHand, out someCross0);
                someCross0.Normalize();
                _player_lft_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWCROSSVEC = someCross0;
                Vector3.Cross(ref dirPivotUpperRIghtToElbowRight, ref someCross0, out someCross1);
                someCross1.Normalize();
                shoulderRotationMatrixRight = Matrix.LookAtRH(currentFINALPIVOTUPPERARM, currentFINALPIVOTUPPERARM + someCross0, dirPivotUpperRIghtToElbowRight);
                shoulderRotationMatrixRight.Invert();
                matrixerer = shoulderRotationMatrixRight;
                _player_lft_upper_arm[0][0]._arrayOfInstances[_iterator]._SHOULDERROT = shoulderRotationMatrixRight;
                matrixerer.M41 = currentPositionOfUPPERARMROTATION3DPOSITION.X;
                matrixerer.M42 = currentPositionOfUPPERARMROTATION3DPOSITION.Y;
                matrixerer.M43 = currentPositionOfUPPERARMROTATION3DPOSITION.Z;
                matrixerer.M44 = 1;
                _body_pos = matrixerer;
                Quaternion.RotationMatrix(ref _body_pos, out _quat);
                _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                matrixIn = JMatrix.CreateFromQuaternion(_other_quat);
                worldMatrix_instances_l_upperarm[0][0][_iterator] = matrixerer;
                _player_lft_upper_arm[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;






                /////////////////
                //LEFT LOWER ARM
                rShldrPos = new Vector3(_player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                dirToLowerArm = _player_lft_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION - rShldrPos;
                dirToLowerArm.Normalize();
                newpoint = rShldrPos + (dirToLowerArm * lengthOfUpperArmRight);
                newpoint = newpoint + (direction_feet_up_ori * (_player_lft_shldr[0][0]._total_torso_height * connectorOfLowerArmRightOffsetMul));
                newdir = somePosOfRightHand - newpoint;
                newdir.Normalize();
                newpoint = newpoint + (newdir * lengthOfLowerArmRight * 0.5f);
                somePosOfRightHand = new Vector3(_player_lft_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_lft_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_lft_hnd[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                somePosererDir = somePosOfRightHand - _player_lft_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;
                someLowerRightArmPos = _player_lft_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION + (somePosererDir * 0.5f);
                somePosererDir.Normalize();
                someCross0 = _player_lft_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWCROSSVEC;
                Vector3.Cross(ref somePosererDir, ref someCross0, out someCross1);
                someCross1.Normalize();
                theLowerArmRotationMatrix = Matrix.LookAtRH(_player_lft_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION, _player_lft_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION + someCross1, somePosererDir);
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
                worldMatrix_instances_l_lowerarm[0][0][_iterator] = matrixerer;
                _player_lft_lower_arm[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;




























                //PELVIS
                _SC_modL_pelvis_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_pelvis[0][0]._POSITION.M41, _player_pelvis[0][0]._POSITION.M42, _player_pelvis[0][0]._POSITION.M43),
                    padding1 = 100
                };
                var _cuber_pelvis = _player_pelvis[0][0];
                var _spine_upper_body_pos = new Vector3(_cuber_pelvis._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _cuber_pelvis._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _cuber_pelvis._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                var MOVINGPOINTERPELVIS = _spine_upper_body_pos;
                MOVINGPOINTERPELVIS.X += OFFSETPOS.X;
                MOVINGPOINTERPELVIS.Y += OFFSETPOS.Y;
                MOVINGPOINTERPELVIS.Z += OFFSETPOS.Z;
                matrixerer = originRot * rotatingMatrixForPelvis * hmdrotMatrix;
                matrixerer.M41 = MOVINGPOINTERPELVIS.X;
                matrixerer.M42 = MOVINGPOINTERPELVIS.Y;
                matrixerer.M43 = MOVINGPOINTERPELVIS.Z;
                matrixerer.M44 = 1;
                _body_pos = matrixerer;
                Quaternion.RotationMatrix(ref _body_pos, out _quat);
                _other_quat = new JQuaternion(_quat.X, _quat.Y, _quat.Z, _quat.W);
                matrixIn = JMatrix.CreateFromQuaternion(_other_quat);
                worldMatrix_instances_pelvis[0][0][_iterator] = matrixerer;
                _player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;




                //UPPER RIGHT LEG
                _rotMatrixer = _player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref _rotMatrixer, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                _SC_modL_r_upper_leg_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_r_upper_leg[0][0]._POSITION.M41, _player_r_upper_leg[0][0]._POSITION.M42, _player_r_upper_leg[0][0]._POSITION.M43),
                    padding1 = 100
                };
                MOVINGPOINTER = new Vector3(_player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                torsooripos = MOVINGPOINTER;
                _rotMatrixer = _player_r_upper_leg[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                diffNormPosX = (MOVINGPOINTER.X) - _player_r_upper_leg[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_r_upper_leg[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_r_upper_leg[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                var realPosOfLegUpR = torsooripos + (direction_head_right * _player_pelvis[0][0]._total_torso_width * connectorOfUpperLegOffsetMul);
                realPosOfLegUpR = realPosOfLegUpR + (-direction_head_up * (_player_r_upper_leg[0][0]._total_torso_height * 1));
                _player_r_upper_leg[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT = realPosOfLegUpR;
                realPosOfLegUpR = realPosOfLegUpR + (-direction_head_up * (_player_r_upper_leg[0][0]._total_torso_height * 1));
                realPosOfLegUpR.X += OFFSETPOS.X;
                realPosOfLegUpR.Y += OFFSETPOS.Y;
                realPosOfLegUpR.Z += OFFSETPOS.Z;
                var _rotatingMatrix = originRot * rotatingMatrixForPelvis * hmdrotMatrix;
                Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                matrixerer = _rotatingMatrix;
                matrixerer.M41 = realPosOfLegUpR.X;
                matrixerer.M42 = realPosOfLegUpR.Y;
                matrixerer.M43 = realPosOfLegUpR.Z;
                worldMatrix_instances_r_upper_leg[0][0][_iterator] = matrixerer;
                _player_r_upper_leg[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;






                //LOWER RIGHT LEG
                _rotMatrixer = _player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref _rotMatrixer, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                _SC_modL_r_lower_leg_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_r_lower_leg[0][0]._POSITION.M41, _player_r_lower_leg[0][0]._POSITION.M42, _player_r_lower_leg[0][0]._POSITION.M43),
                    padding1 = 100
                };
                MOVINGPOINTER = new Vector3(_player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                torsooripos = MOVINGPOINTER;
                _rotMatrixer = _player_r_lower_leg[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                diffNormPosX = (MOVINGPOINTER.X) - _player_r_lower_leg[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_r_lower_leg[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_r_lower_leg[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                var realPosOfLegLowR = torsooripos + (direction_head_right * _player_pelvis[0][0]._total_torso_width * connectorOfLowerLegOffsetMul);
                realPosOfLegLowR = realPosOfLegLowR + (-direction_head_up * (_player_pelvis[0][0]._total_torso_height * 2 + _player_r_upper_leg[0][0]._total_torso_height * 4));
                realPosOfLegLowR.X += OFFSETPOS.X;
                realPosOfLegLowR.Y += OFFSETPOS.Y;
                realPosOfLegLowR.Z += OFFSETPOS.Z;
                _rotatingMatrix = originRot * rotatingMatrixForPelvis * hmdrotMatrix;
                Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                matrixerer = _rotatingMatrix;
                matrixerer.M41 = realPosOfLegLowR.X;
                matrixerer.M42 = realPosOfLegLowR.Y;
                matrixerer.M43 = realPosOfLegLowR.Z;
                worldMatrix_instances_r_lower_leg[0][0][_iterator] = matrixerer;
                _player_r_lower_leg[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;






                //UPPER LEFT LEG
                _rotMatrixer = _player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref _rotMatrixer, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                _SC_modL_l_upper_leg_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_l_upper_leg[0][0]._POSITION.M41, _player_l_upper_leg[0][0]._POSITION.M42, _player_l_upper_leg[0][0]._POSITION.M43),
                    padding1 = 100
                };
                MOVINGPOINTER = new Vector3(_player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                torsooripos = MOVINGPOINTER;
                _rotMatrixer = _player_l_upper_leg[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                diffNormPosX = (MOVINGPOINTER.X) - _player_l_upper_leg[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_l_upper_leg[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_l_upper_leg[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                var realPosOfLegUpL = torsooripos + (-direction_head_right * _player_pelvis[0][0]._total_torso_width * connectorOfUpperLegOffsetMul);
                realPosOfLegUpL = realPosOfLegUpL + (-direction_head_up * (_player_l_upper_leg[0][0]._total_torso_height * 1));
                _player_l_upper_leg[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT = realPosOfLegUpL;
                realPosOfLegUpL = realPosOfLegUpL + (-direction_head_up * (_player_l_upper_leg[0][0]._total_torso_height * 1));
                realPosOfLegUpL.X += OFFSETPOS.X;
                realPosOfLegUpL.Y += OFFSETPOS.Y;
                realPosOfLegUpL.Z += OFFSETPOS.Z;
                _rotatingMatrix = originRot * rotatingMatrixForPelvis * hmdrotMatrix;
                Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                matrixerer = _rotatingMatrix;
                matrixerer.M41 = realPosOfLegUpL.X;
                matrixerer.M42 = realPosOfLegUpL.Y;
                matrixerer.M43 = realPosOfLegUpL.Z;
                worldMatrix_instances_l_upper_leg[0][0][_iterator] = matrixerer;
                _player_l_upper_leg[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;









                //LOWER LEFT LEG
                _rotMatrixer = _player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref _rotMatrixer, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                _SC_modL_l_lower_leg_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_l_lower_leg[0][0]._POSITION.M41, _player_l_lower_leg[0][0]._POSITION.M42, _player_l_lower_leg[0][0]._POSITION.M43),
                    padding1 = 100
                };
                MOVINGPOINTER = new Vector3(_player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                torsooripos = MOVINGPOINTER;
                _rotMatrixer = _player_l_lower_leg[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                diffNormPosX = (MOVINGPOINTER.X) - _player_l_lower_leg[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_l_lower_leg[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_l_lower_leg[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                var realPosOfLegLowL = torsooripos + (-direction_head_right * _player_pelvis[0][0]._total_torso_width * connectorOfLowerLegOffsetMul);
                realPosOfLegLowL = realPosOfLegLowL + (-direction_head_up * (_player_pelvis[0][0]._total_torso_height * 2 + _player_l_upper_leg[0][0]._total_torso_height * 4));
                realPosOfLegLowL.X += OFFSETPOS.X;
                realPosOfLegLowL.Y += OFFSETPOS.Y;
                realPosOfLegLowL.Z += OFFSETPOS.Z;
                _rotatingMatrix = originRot * rotatingMatrixForPelvis * hmdrotMatrix;
                Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                matrixerer = _rotatingMatrix;
                matrixerer.M41 = realPosOfLegLowL.X;
                matrixerer.M42 = realPosOfLegLowL.Y;
                matrixerer.M43 = realPosOfLegLowL.Z;
                worldMatrix_instances_l_lower_leg[0][0][_iterator] = matrixerer;
                _player_l_lower_leg[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;




                //LEFT FOOT
                _rotMatrixer = _player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref _rotMatrixer, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                _SC_modL_l_foot_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_l_foot[0][0]._POSITION.M41, _player_l_foot[0][0]._POSITION.M42, _player_l_foot[0][0]._POSITION.M43),
                    padding1 = 100
                };
                MOVINGPOINTER = new Vector3(_player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                torsooripos = MOVINGPOINTER;
                _rotMatrixer = _player_l_foot[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                diffNormPosX = (MOVINGPOINTER.X) - _player_l_foot[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_l_foot[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_l_foot[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                var realPosOfFootL = torsooripos + (-direction_head_right * _player_pelvis[0][0]._total_torso_width * connectorOfLowerLegOffsetMul);
                realPosOfFootL = realPosOfFootL + (-direction_head_up * (_player_pelvis[0][0]._total_torso_height * 2 + _player_l_foot[0][0]._total_torso_height * 4 + _player_l_upper_leg[0][0]._total_torso_height * 4));
                realPosOfFootL.X += OFFSETPOS.X;
                realPosOfFootL.Y += OFFSETPOS.Y;
                realPosOfFootL.Z += OFFSETPOS.Z;
                _rotatingMatrix = originRot * rotatingMatrixForPelvis * hmdrotMatrix;
                Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                matrixerer = _rotatingMatrix;
                matrixerer.M41 = realPosOfFootL.X;
                matrixerer.M42 = realPosOfFootL.Y;
                matrixerer.M43 = realPosOfFootL.Z;
                worldMatrix_instances_l_foot[0][0][_iterator] = matrixerer;
                _player_l_foot[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;




                //RIGHT FOOT
                _rotMatrixer = _player_pelvis[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref _rotMatrixer, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                _SC_modL_r_foot_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_r_foot[0][0]._POSITION.M41, _player_r_foot[0][0]._POSITION.M42, _player_r_foot[0][0]._POSITION.M43),
                    padding1 = 100
                };
                MOVINGPOINTER = new Vector3(_player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                torsooripos = MOVINGPOINTER;
                _rotMatrixer = _player_r_foot[0][0]._ORIGINPOSITION;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                diffNormPosX = (MOVINGPOINTER.X) - _player_r_foot[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41;
                diffNormPosY = (MOVINGPOINTER.Y) - _player_r_foot[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42;
                diffNormPosZ = (MOVINGPOINTER.Z) - _player_r_foot[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43;
                var realPosOfFootR = torsooripos + (direction_head_right * _player_pelvis[0][0]._total_torso_width * connectorOfLowerLegOffsetMul);
                realPosOfFootR = realPosOfFootR + (-direction_head_up * (_player_pelvis[0][0]._total_torso_height * 2));
                realPosOfFootR = realPosOfFootR + (-direction_head_up * (_player_r_upper_leg[0][0]._total_torso_height * 4));
                realPosOfFootR = realPosOfFootR + (-direction_head_up * (_player_r_foot[0][0]._total_torso_height * 4));
                realPosOfFootR.X += OFFSETPOS.X;
                realPosOfFootR.Y += OFFSETPOS.Y;
                realPosOfFootR.Z += OFFSETPOS.Z;
                _rotatingMatrix = originRot * rotatingMatrixForPelvis * hmdrotMatrix;
                Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);
                direction_head_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_head_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_head_up = sc_maths._getDirection(Vector3.Up, otherQuat);
                matrixerer = _rotatingMatrix;
                matrixerer.M41 = realPosOfFootR.X;
                matrixerer.M42 = realPosOfFootR.Y;
                matrixerer.M43 = realPosOfFootR.Z;
                worldMatrix_instances_r_foot[0][0][_iterator] = matrixerer;
                _player_r_foot[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;






                ////////////////////
                //KNEE TARGET RIGHT
                _SC_modL_rght_elbow_target_knee_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_rght_target_knee[0][0]._POSITION.M41, _player_rght_target_knee[0][0]._POSITION.M42, _player_rght_target_knee[0][0]._POSITION.M43),
                    padding1 = 100
                };
                //MOVINGPOINTER = new Vector3(_player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                MOVINGPOINTER = new Vector3(_player_r_upper_leg[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT.X, _player_r_upper_leg[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT.Y, _player_r_upper_leg[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT.Z);
                _rotMatrixer = _player_r_upper_leg[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * (_player_r_upper_leg[0][0]._total_torso_height + (_player_r_upper_leg[0][0]._total_torso_height * 0.5f)));
                MOVINGPOINTER = MOVINGPOINTER + (direction_feet_forward_ori * ((_player_r_upper_leg[0][0]._total_torso_height * 2) + (_player_r_lower_leg[0][0]._total_torso_height * 2)));
                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;
                matrixerer = finalRotationMatrix;
                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;
                matrixerer.M44 = 1;
                worldMatrix_instances_r_target_knee[0][0][_iterator] = matrixerer;// _player_pelvis[0][0].current_pos;// translationMatrix;
                _player_rght_target_knee[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;




                ///////////////////
                //KNEE TARGET LEFT
                _SC_modL_lft_elbow_target_knee_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_lft_target_knee[0][0]._POSITION.M41, _player_lft_target_knee[0][0]._POSITION.M42, _player_lft_target_knee[0][0]._POSITION.M43),
                    padding1 = 100
                };
                //MOVINGPOINTER = new Vector3(_player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                MOVINGPOINTER = new Vector3(_player_l_upper_leg[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT.X, _player_l_upper_leg[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT.Y, _player_l_upper_leg[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT.Z);
                _rotMatrixer = _player_l_upper_leg[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * (_player_l_upper_leg[0][0]._total_torso_height + (_player_l_upper_leg[0][0]._total_torso_height * 0.5f)));
                MOVINGPOINTER = MOVINGPOINTER + (direction_feet_forward_ori * ((_player_l_upper_leg[0][0]._total_torso_height * 2) + (_player_l_upper_leg[0][0]._total_torso_height * 2)));
                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;
                matrixerer = finalRotationMatrix;
                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;
                matrixerer.M44 = 1;
                worldMatrix_instances_l_target_knee[0][0][_iterator] = matrixerer;// _player_pelvis[0][0].current_pos;// translationMatrix;
                _player_lft_target_knee[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;




                ///////////////////////
                //KNEE TARGET RIGHT TWO
                _SC_modL_rght_elbow_target_two_knee_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_rght_target_two_knee[0][0]._POSITION.M41, _player_rght_target_two_knee[0][0]._POSITION.M42, _player_rght_target_two_knee[0][0]._POSITION.M43),
                    padding1 = 100
                };
                //MOVINGPOINTER = new Vector3(_player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                MOVINGPOINTER = new Vector3(_player_r_upper_leg[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT.X, _player_r_upper_leg[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT.Y, _player_r_upper_leg[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT.Z);
                _rotMatrixer = _player_rght_target_two_knee[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * (_player_rght_target_two_knee[0][0]._total_torso_height + (_player_rght_target_two_knee[0][0]._total_torso_height * 0.5f)));
                MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_forward_ori * ((_player_rght_target_two_knee[0][0]._total_torso_height * 2) + (_player_r_lower_leg[0][0]._total_torso_height * 2)));
                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;
                matrixerer = finalRotationMatrix;
                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;
                matrixerer.M44 = 1;
                worldMatrix_instances_r_target_two_knee[0][0][_iterator] = matrixerer;// _player_pelvis[0][0].current_pos;// translationMatrix;
                _player_rght_target_two_knee[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;




                ///////////////////////
                //KNEE TARGET LEFT TWO
                _SC_modL_lft_elbow_target_two_knee_BUFFER[0] = new SCCoreSystems.SC_Graphics.sc_voxel.DLightBuffer()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight,
                    padding0 = 7,
                    lightPosition = new Vector3(_player_lft_target_two_knee[0][0]._POSITION.M41, _player_lft_target_two_knee[0][0]._POSITION.M42, _player_lft_target_two_knee[0][0]._POSITION.M43),
                    padding1 = 100
                };
                //MOVINGPOINTER = new Vector3(_player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M41, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M42, _player_pelvis[0][0]._arrayOfInstances[_iterator]._ORIGINPOSITION.M43);
                MOVINGPOINTER = new Vector3(_player_l_upper_leg[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT.X, _player_l_upper_leg[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT.Y, _player_l_upper_leg[0][0]._arrayOfInstances[_iterator]._UPPERPIVOT.Z);
                _rotMatrixer = _player_lft_target_two_knee[0][0]._arrayOfInstances[_iterator].current_pos;
                Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);
                direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, forTest);
                direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, forTest);
                direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, forTest);
                MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_up_ori * (_player_lft_target_two_knee[0][0]._total_torso_height + (_player_lft_target_two_knee[0][0]._total_torso_height * 0.5f)));
                MOVINGPOINTER = MOVINGPOINTER + (-direction_feet_forward_ori * ((_player_lft_target_two_knee[0][0]._total_torso_height * 2) + (_player_r_lower_leg[0][0]._total_torso_height * 2)));
                MOVINGPOINTER.X += OFFSETPOS.X;
                MOVINGPOINTER.Y += OFFSETPOS.Y;
                MOVINGPOINTER.Z += OFFSETPOS.Z;
                matrixerer = finalRotationMatrix;
                matrixerer.M41 = MOVINGPOINTER.X;
                matrixerer.M42 = MOVINGPOINTER.Y;
                matrixerer.M43 = MOVINGPOINTER.Z;
                matrixerer.M44 = 1;
                worldMatrix_instances_l_target_two_knee[0][0][_iterator] = matrixerer;// _player_pelvis[0][0].current_pos;// translationMatrix;
                _player_lft_target_two_knee[0][0]._arrayOfInstances[_iterator].current_pos = matrixerer;
            }



            //#region
            //UPPERBODY
            var _cuber_001 = _player_torso[0][0];
            _cuber_001.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_torso(SC_console_directx.D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_torso_BUFFER, _cuber_001);
            _cuber_001 = _player_pelvis[0][0];
            _cuber_001.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_pelvis(SC_console_directx.D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_pelvis_BUFFER, _cuber_001);
            _player_head[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_torso(SC_console_directx.D3D.device.ImmediateContext, _player_head[0][0].IndexCount, _player_head[0][0].InstanceCount, _player_head[0][0]._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_torso_BUFFER, _player_head[0][0]);
            _player_rght_hnd[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_rgt_hnd(SC_console_directx.D3D.device.ImmediateContext, _player_rght_hnd[0][0].IndexCount, _player_rght_hnd[0][0].InstanceCount, _player_rght_hnd[0][0]._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_hnd_BUFFER, _player_rght_hnd[0][0]);
            _player_lft_hnd[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_lft_hnd(SC_console_directx.D3D.device.ImmediateContext, _player_lft_hnd[0][0].IndexCount, _player_lft_hnd[0][0].InstanceCount, _player_lft_hnd[0][0]._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_lft_hnd_BUFFER, _player_lft_hnd[0][0]);
            _cuber_001 = _player_rght_shldr[0][0];
            _cuber_001.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_rgt_shldr(SC_console_directx.D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_shldr_BUFFER, _cuber_001);
            _cuber_001 = _player_rght_lower_arm[0][0];
            _cuber_001.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_rgt_lower_arm(SC_console_directx.D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_lower_arm_BUFFER, _cuber_001);
            _cuber_001 = _player_rght_upper_arm[0][0];
            _cuber_001.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_rgt_upper_arm(SC_console_directx.D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_upper_arm_BUFFER, _cuber_001);
            _cuber_001 = _player_lft_shldr[0][0];
            _cuber_001.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_lft_shldr(SC_console_directx.D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_lft_shldr_BUFFER, _cuber_001);
            _cuber_001 = _player_lft_lower_arm[0][0];
            _cuber_001.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_lft_lower_arm(SC_console_directx.D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_lft_lower_arm_BUFFER, _cuber_001);
            _cuber_001 = _player_lft_upper_arm[0][0];
            _cuber_001.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_lft_upper_arm(SC_console_directx.D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_lft_upper_arm_BUFFER, _cuber_001);
            //#endregion



            //LOWER BODY
            _cuber_001 = _player_r_upper_leg[0][0];
            _cuber_001.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_torso(SC_console_directx.D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_r_upper_leg_BUFFER, _cuber_001);
            _cuber_001 = _player_l_upper_leg[0][0];
            _cuber_001.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_torso(SC_console_directx.D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_l_upper_leg_BUFFER, _cuber_001);
            _cuber_001 = _player_r_lower_leg[0][0];
            _cuber_001.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_torso(SC_console_directx.D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_r_upper_leg_BUFFER, _cuber_001);
            _cuber_001 = _player_l_lower_leg[0][0];
            _cuber_001.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_torso(SC_console_directx.D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_l_upper_leg_BUFFER, _cuber_001);
            _cuber_001 = _player_r_foot[0][0];
            _cuber_001.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_torso(SC_console_directx.D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_r_upper_leg_BUFFER, _cuber_001);
            _cuber_001 = _player_l_foot[0][0];
            _cuber_001.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_torso(SC_console_directx.D3D.device.ImmediateContext, _cuber_001.IndexCount, _cuber_001.InstanceCount, _cuber_001._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_l_upper_leg_BUFFER, _cuber_001);







            //IK TARGETS LOWER BODY.
            var _cuber_01 = _player_rght_target_knee[0][0];
            _cuber_01.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_rgt_elbow_targ(SC_console_directx.D3D.device.ImmediateContext, _cuber_01.IndexCount, _cuber_01.InstanceCount, _cuber_01._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_elbow_target_knee_BUFFER, _cuber_01);
            _cuber_01 = _player_rght_target_two_knee[0][0];
            _cuber_01.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_rgt_elbow_targ_two(SC_console_directx.D3D.device.ImmediateContext, _cuber_01.IndexCount, _cuber_01.InstanceCount, _cuber_01._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_elbow_target_two_knee_BUFFER, _cuber_01);
            _cuber_01 = _player_lft_target_knee[0][0];
            _cuber_01.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_lft_elbow_targ(SC_console_directx.D3D.device.ImmediateContext, _cuber_01.IndexCount, _cuber_01.InstanceCount, _cuber_01._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_lft_elbow_target_knee_BUFFER, _cuber_01);
            _cuber_01 = _player_lft_target_two_knee[0][0];
            _cuber_01.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_lft_elbow_targ_two(SC_console_directx.D3D.device.ImmediateContext, _cuber_01.IndexCount, _cuber_01.InstanceCount, _cuber_01._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_lft_elbow_target_two_knee_BUFFER, _cuber_01);

            //IK TARGETS UPPER BODY.
            _cuber_01 = _player_rght_elbow_target[0][0];
            _cuber_01.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_rgt_elbow_targ(SC_console_directx.D3D.device.ImmediateContext, _cuber_01.IndexCount, _cuber_01.InstanceCount, _cuber_01._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_lft_hnd_BUFFER, _cuber_01);
            _cuber_01 = _player_rght_elbow_target_two[0][0];
            _cuber_01.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_rgt_elbow_targ_two(SC_console_directx.D3D.device.ImmediateContext, _cuber_01.IndexCount, _cuber_01.InstanceCount, _cuber_01._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_lft_hnd_BUFFER, _cuber_01);
            _cuber_01 = _player_lft_elbow_target[0][0];
            _cuber_01.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_lft_elbow_targ(SC_console_directx.D3D.device.ImmediateContext, _cuber_01.IndexCount, _cuber_01.InstanceCount, _cuber_01._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_lft_hnd_BUFFER, _cuber_01);
            _cuber_01 = _player_lft_elbow_target_two[0][0];
            _cuber_01.Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_lft_elbow_targ_two(SC_console_directx.D3D.device.ImmediateContext, _cuber_01.IndexCount, _cuber_01.InstanceCount, _cuber_01._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_lft_hnd_BUFFER, _cuber_01);


            /*
            _player_r_hand_grab[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
            SC_Update._shaderManager._rend_torso(SC_console_directx.D3D.device.ImmediateContext, _player_r_hand_grab[0][0].IndexCount, _player_r_hand_grab[0][0].InstanceCount, _player_r_hand_grab[0][0]._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_rght_hnd_BUFFER, _player_r_hand_grab[0][0]);
            */






            /*if (writeProcess == 0)
            {

                Process[] processlist = Process.GetProcesses();

                foreach (Process process in processlist)
                {

                    //Console.WriteLine(process.ProcessName);
                    if (process.ProcessName == "msedge" && process.MainWindowHandle != IntPtr.Zero)
                    {
                        //Console.WriteLine("Process: {0} ID: {1} Window title: {2}", process.ProcessName, process.Id, process.MainWindowTitle);
                        Console.WriteLine("msedge" + " " + process.MainWindowHandle);
                        MSEdgeHandle = process.MainWindowHandle;

                        //MessageBox((IntPtr)0, "ED" + " " + process.MainWindowHandle, "sccoresystems0", 0);

                    }
                }
                //WebBrowser youtubePlayer = new WebBrowser();
                //youtubePlayer.Navigate("http://www.youtube.com/embed/M7lc1UVf-VE");
                writeProcess = 1;
            }*/


            //Rectangle bonds = new Rectangle();
            //GetWindowRect(handle, bonds);
            //Bitmap bmp = new Bitmap(bonds.Width, bonds.Height);


            /*var detector = new BrowserDetector();
            if (detector.BrowserIsOpen())
            {
                //MessageBox.Show(this, "Browser Was Detected As Open", "Success", MessageBoxButtons.OK);
                //Program.MessageBox((IntPtr)0, "Browser opened", "sccoresystems0", 0);
                Console.WriteLine("Browser opened");
            }
            else
            {
                //MessageBox.Show(this, "Please Open Browser", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Program.MessageBox((IntPtr)0, "Browser not opened", "sccoresystems0", 0);
            }



            IntPtr hWnd = FindWindowByCaption(IntPtr.Zero, "msedge.exe");

            if (hWnd != IntPtr.Zero)
            {
                Console.WriteLine(hWnd + "test0");
                //Program.MessageBox((IntPtr)0, hWnd + "test0", "sccoresystems0", 0);
            }*/



            /*MSEdgeHandle = IntPtr.Zero;
            // Enumerate over windows.
            EnumWindows((handle, param) =>
            {
                // Get the class name. We are looking for ApplicationFrameWindow.
                var className = new StringBuilder(256);
                GetClassName(handle, className, className.Capacity);

                // Get the window text. We're looking for Microsoft Edge.
                int windowTextSize = GetWindowTextLength(handle);
                var windowText = new StringBuilder(windowTextSize + 1);
                GetWindowText(handle, windowText, windowText.Capacity);
 
                // Check if we have a match. If we do, minimize that window.
                if (windowText.ToString().Contains("Microsoft Edge")) //className.ToString().Contains("ApplicationFrameWindow") &&
                {
                    MSEdgeHandle = handle;
                    //ShowWindow(handle, SW_SHOWMINIMIZED);
                    //Console.WriteLine("edge is opened");
                    //Rectangle bonds = new Rectangle();
                    //GetWindowRect(handle, bonds);
                    //Console.WriteLine(" h: " + bonds.Height + " w: " + bonds.Width + " edge is opened");
                }

                // Return true so that we continue enumerating,
                // in case there are multiple instances.
                return true;
            }, IntPtr.Zero);*/



            /*if (SC_Update._desktopFrame._texture2DFinal != null && setArray == 0)
            {
                arrayOfPixData = new int[SC_Update._desktopFrame._texture2DFinal.Description.Width * SC_Update._desktopFrame._texture2DFinal.Description.Height * 3];
                setArray = 1;
            }*/


            /*if (MSEdgeHandle != IntPtr.Zero && SC_Update._desktopFrame._texture2DFinal != null)
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle();

                // Then we call the GetWindowRect function, passing in a reference to the rect object.
                GetWindowRect(MSEdgeHandle, ref rect);

                // And then we get the resulting rectangle. The tricky part here is that this rectangle includes
                // not only the location of the window, but also the size, but not in the form we're used to.
                //Console.WriteLine(rect.ToString());

                /*RECT rct;

                if (!GetWindowRect(MSEdgeHandle, out rct))
                {
                    Console.WriteLine("failed to get rect");
                }
                //MessageBox.Show(rct.ToString());
                //Console.WriteLine(rct.ToString() + " edge is opened");
                
                //myRect.X = rect.Left;
                //myRect.Y = rect.Top;
                //myRect.Width = rect.Right - rect.Left + 1;
                //myRect.Height = rect.Bottom - rect.Top + 1;

                //Rectangle bonds = new Rectangle();
                //GetWindowRect(MSEdgeHandle, bonds);
                Console.WriteLine(" h: " + rect.Top + " w: " + rect.Left + " edge is opened");
                //Console.WriteLine(" h: " + myRect.Width + " w: " + myRect.Height + " edge is opened");
            }*/











            /*
            if (Terrain.vertices != null)
            {
                //Console.WriteLine("test0");
                //Program.MessageBox((IntPtr)0, "!null","msg", 0);

                if (Terrain.vertices.Length > 0 && SC_Update._desktopDupe.arrayOfPixData.Length > 0)
                {
                    if (SC_Update._desktopFrame._texture2DFinal != null && SC_Update._desktopDupe != null)
                    {
                        //SC_Update._desktopFrame.
                        //var HeightMapSobel = new List<DHeightMapType>(); //SC_Update._desktopFrame._texture2DFinal.Description.Width * SC_Update._desktopFrame._texture2DFinal.Description.Height

                        //var dataBox = SC_console_directx.D3D.device.ImmediateContext.MapSubresource(SC_Update._desktopFrame._texture2DFinal, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);
                        //IntPtr interptr = dataBox.DataPointer;
                        //SC_console_directx.D3D.device.ImmediateContext.UnmapSubresource(SC_Update._desktopFrame._texture2DFinal, 0);






                        /*for (var j = 0; j < SC_Update._desktopFrame._texture2DFinal.Description.Height; j++)
                        {
                            for (var i = 0; i < SC_Update._desktopFrame._texture2DFinal.Description.Width; i++)
                            {
                                var bytePoser = ((j * SC_Update._desktopFrame._texture2DFinal.Description.Width) + i) * 3;

                                /*HeightMapSobel.Add(new DHeightMapType()
                                {
                                    x = i,
                                    y = SC_Update._desktopDupe._textureByteArray[bytePoser],// image.GetPixel(i, j).R,
                                    z = j
                                });

                                arrayOfPixData[bytePoser + 0] = i;
                                arrayOfPixData[bytePoser + 1] = SC_Update._desktopDupe._textureByteArray[bytePoser];
                                arrayOfPixData[bytePoser + 2] = j;
                            }
                        }*/




            //int memoryBitmapStride = SC_Update._desktopFrame._texture2DFinal.Description.Width * 4;

            /*var image = new System.Drawing.Bitmap(SC_Update._desktopFrame._texture2DFinal.Description.Width, SC_Update._desktopFrame._texture2DFinal.Description.Height, memoryBitmapStride, System.Drawing.Imaging.PixelFormat.Format32bppArgb, interptr);

            // Read the image data into the height map
            for (var j = 0; j < SC_Update._desktopFrame._texture2DFinal.Description.Height; j++)
            {
                for (var i = 0; i < SC_Update._desktopFrame._texture2DFinal.Description.Width; i++)
                {
                    HeightMapSobel.Add(new DHeightMapType()
                    {
                        x = i,
                        y = image.GetPixel(i, j).R,
                        z = j
                    });
                }
            }*/

            /*byte* ptr = (byte*)interptr.ToPointer();
            int _pixelSize = 3;
            int _nWidth = SC_Update._desktopFrame._texture2DFinal.Description.Width * _pixelSize;
            int _nHeight = SC_Update._desktopFrame._texture2DFinal.Description.Height;


            for (int y = 0; y < _nHeight; y++)
            {
                for (int x = 0; x < _nWidth; x++)
                {
                    if (x % _pixelSize == 0 || x == 0)
                    {
                        var bytePoser = ((y * _nWidth) + x);

                        var test0 = ptr[bytePoser + 0];
                        var test1 = ptr[bytePoser + 1];
                        var test2 = ptr[bytePoser + 2];

                        try
                        {
                            HeightMapSobel.Add(new DHeightMapType()
                            {
                                x = x,
                                y = test0,
                                z = y
                            });
                        }
                        catch (Exception ex)
                        {
                            Program.MessageBox((IntPtr)0, " _ " + ex.ToString(), "sccs message", 0);
                        }

                        ptr++;
                    }
                }
            }
            */






            /*int memoryBitmapStride = _width * 4;

            // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (_width * 4)
            if (dataBox.RowPitch == memoryBitmapStride)
            {
                // Stride is the same
                Marshal.Copy(sourcePtr, _textureByteArray, 0, _bytesTotal);
            }
            else
            {
                // Stride not the same - copy line by line
                for (int y = 0; y < _height; y++)
                {
                    Marshal.Copy(sourcePtr + y * dataBox.RowPitch, _textureByteArray, y * memoryBitmapStride, memoryBitmapStride);
                }
            }*/



            //image.Dispose();
            //DeleteObject(interptr);
            /*
            int index = 0;

            for (int j = 0; j < (SC_Update._desktopFrame._texture2DFinal.Description.Height - 1); j++)
            {
                for (int i = 0; i < (SC_Update._desktopFrame._texture2DFinal.Description.Width - 1); i++)
                {
                    /*int indexBottomLeft1 = ((SC_Update._desktopFrame._texture2DFinal.Description.Height * j) + i);          // Bottom left.
                    int indexBottomRight2 = ((SC_Update._desktopFrame._texture2DFinal.Description.Height * j) + (i + 1));      // Bottom right.
                    int indexUpperLeft3 = ((SC_Update._desktopFrame._texture2DFinal.Description.Height * (j + 1)) + i);      // Upper left.
                    int indexUpperRight4 = ((SC_Update._desktopFrame._texture2DFinal.Description.Height * (j + 1)) + (i + 1));  // Upper right.
                    */

            //SharpDX.Color.Lerp(SharpDX.Color.Lerp(bl, br, xLerp), SharpDX.Color.Lerp(tl, tr, xLerp), y * ratioY - yy);

            /*Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexUpperLeft3].x, SC_Update._desktopDupe.arrayOfPixData[indexUpperLeft3].y, SC_Update._desktopDupe.arrayOfPixData[indexUpperLeft3].z);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4].x, SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4].y, SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4].z);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4].x, SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4].y, SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4].z);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1].x, SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1].y, SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1].z);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1].x, SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1].y, SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1].z);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexUpperLeft3].x, SC_Update._desktopDupe.arrayOfPixData[indexUpperLeft3].y, SC_Update._desktopDupe.arrayOfPixData[indexUpperLeft3].z);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1].x, SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1].y, SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1].z);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4].x, SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4].y, SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4].z);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4].x, SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4].y, SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4].z);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexBottomRight2].x, SC_Update._desktopDupe.arrayOfPixData[indexBottomRight2].y, SC_Update._desktopDupe.arrayOfPixData[indexBottomRight2].z);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexBottomRight2].x, SC_Update._desktopDupe.arrayOfPixData[indexBottomRight2].y, SC_Update._desktopDupe.arrayOfPixData[indexBottomRight2].z);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1].x, SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1].y, SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1].z);
            */

            /*Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexUpperLeft3], SC_Update._desktopDupe.arrayOfPixData[indexUpperLeft3], SC_Update._desktopDupe.arrayOfPixData[indexUpperLeft3]);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4], SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4], SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4]);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4], SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4], SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4]);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1], SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1], SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1]);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1], SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1], SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1]);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexUpperLeft3], SC_Update._desktopDupe.arrayOfPixData[indexUpperLeft3], SC_Update._desktopDupe.arrayOfPixData[indexUpperLeft3]);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1], SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1], SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1]);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4], SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4], SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4]);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4], SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4], SC_Update._desktopDupe.arrayOfPixData[indexUpperRight4]);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexBottomRight2], SC_Update._desktopDupe.arrayOfPixData[indexBottomRight2], SC_Update._desktopDupe.arrayOfPixData[indexBottomRight2]);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexBottomRight2], SC_Update._desktopDupe.arrayOfPixData[indexBottomRight2], SC_Update._desktopDupe.arrayOfPixData[indexBottomRight2]);
            Terrain.vertices[index].position = new Vector3(SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1], SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1], SC_Update._desktopDupe.arrayOfPixData[indexBottomLeft1]);

            index++;
        }
    }
}

//Program.MessageBox((IntPtr)0, Terrain.vertices.Length + "!null", "msg", 0);

//Console.WriteLine("test0");

//IntPtr ptrVert = Marshal.UnsafeAddrOfPinnedArrayElement(Terrain.vertices, 0);

//Guid guid = new Guid();

//Terrain.VertexBuffer.SetPrivateData(guid, Terrain.vertices.Length, ptrVert);

Terrain.VertexBuffer = SharpDX.Direct3D11.Buffer.Create(SC_console_directx.D3D.device, SharpDX.Direct3D11.BindFlags.VertexBuffer, Terrain.vertices);

//Terrain.VertexBuffer.SetPrivateData

// Render the terrain GRID buffers.
Terrain.Render(SC_console_directx.D3D.DeviceContext);
// Render the model using the color shader.
SC_Update._shaderManager.RenderGrid(SC_console_directx.D3D.DeviceContext, Terrain.IndexCount, sc_maths.Scaling(heightmapscale) * worldMatrix_instances_r_hand_grab[0][0][0], viewMatrix, projectionMatrix);
//DeleteObject(ptrVert);
}
}
else
{

}*/




            //////////////TO READD
            //////////////TO READD
            //////////////TO READD
            Terrain.Render(SC_console_directx.D3D.DeviceContext);
            // Render the model using the color shader.
            SC_Update._shaderManager.RenderGrid(SC_console_directx.D3D.DeviceContext, Terrain.IndexCount, sc_maths.Scaling(heightmapscale) * worldMatrix_instances_r_hand_grab[0][0][0], viewMatrix, projectionMatrix);
            //////////////TO READD
            //////////////TO READD
            //////////////TO READD

            //_player_l_hand_grab[0][0].Render(SC_console_directx.D3D.device.ImmediateContext);
            //SC_Update._shaderManager._rend_torso(SC_console_directx.D3D.device.ImmediateContext, _player_l_hand_grab[0][0].IndexCount, _player_l_hand_grab[0][0].InstanceCount, _player_l_hand_grab[0][0]._POSITION, viewMatrix, projectionMatrix, null, _SC_modL_lft_hnd_BUFFER, _player_l_hand_grab[0][0]);

            return _sc_jitter_tasks;
        }

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
        Quaternion _hmdRoter;
        Vector3 oculusRiftDir = Vector3.Zero;

        public SC_message_object_jitter[][] sc_write_to_buffer(SC_message_object_jitter[][] _sc_jitter_tasks)
        {
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









            //HEADSET POSITION
            displayMidpoint = SC_console_directx.D3D.OVR.GetPredictedDisplayTime(SC_console_directx.D3D.sessionPtr, 0);
            trackingState = SC_console_directx.D3D.OVR.GetTrackingState(SC_console_directx.D3D.sessionPtr, displayMidpoint, true);
            latencyMark = false;
            trackState = SC_console_directx.D3D.OVR.GetTrackingState(SC_console_directx.D3D.sessionPtr, 0.0f, latencyMark);
            poseStatefer = trackState.HeadPose;
            hmdPose = poseStatefer.ThePose;
            hmdRot = hmdPose.Orientation;
            _hmdPoser = new Vector3(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);
            _hmdRoter = new Quaternion(hmdPose.Orientation.X, hmdPose.Orientation.Y, hmdPose.Orientation.Z, hmdPose.Orientation.W);
            //SET CAMERA POSITION
            //Camera.SetPosition(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);
            Quaternion quatter = new Quaternion(hmdRot.X, hmdRot.Y, hmdRot.Z, hmdRot.W);
            oculusRiftDir = sc_maths._getDirection(Vector3.ForwardRH, quatter);

            Matrix.RotationQuaternion(ref quatter, out hmd_matrix_current);

            //TOUCH CONTROLLER RIGHT
            resultsRight = SC_console_directx.D3D.OVR.GetInputState(SC_console_directx.D3D.sessionPtr, SC_console_directx.D3D.controllerTypeRTouch, ref SC_console_directx.D3D.inputStateRTouch);
            buttonPressedOculusTouchRight = SC_console_directx.D3D.inputStateRTouch.Buttons;
            thumbStickRight = SC_console_directx.D3D.inputStateRTouch.Thumbstick;
            handTriggerRight = SC_console_directx.D3D.inputStateRTouch.HandTrigger;
            indexTriggerRight = SC_console_directx.D3D.inputStateRTouch.IndexTrigger;
            handPoseRight = trackingState.HandPoses[1].ThePose;

            _rightTouchQuat.X = handPoseRight.Orientation.X;
            _rightTouchQuat.Y = handPoseRight.Orientation.Y;
            _rightTouchQuat.Z = handPoseRight.Orientation.Z;
            _rightTouchQuat.W = handPoseRight.Orientation.W;

            _rightTouchQuat = new SharpDX.Quaternion(handPoseRight.Orientation.X, handPoseRight.Orientation.Y, handPoseRight.Orientation.Z, handPoseRight.Orientation.W);
            SharpDX.Matrix.RotationQuaternion(ref _rightTouchQuat, out _rightTouchMatrix);

            _rightTouchMatrix.M41 = handPoseRight.Position.X + SC_Update.originPos.X + SC_Update.movePos.X;
            _rightTouchMatrix.M42 = handPoseRight.Position.Y + SC_Update.originPos.Y + SC_Update.movePos.Y;
            _rightTouchMatrix.M43 = handPoseRight.Position.Z + SC_Update.originPos.Z + SC_Update.movePos.Z;

            //TOUCH CONTROLLER LEFT
            resultsLeft = SC_console_directx.D3D.OVR.GetInputState(SC_console_directx.D3D.sessionPtr, SC_console_directx.D3D.controllerTypeLTouch, ref SC_console_directx.D3D.inputStateLTouch);
            buttonPressedOculusTouchLeft = SC_console_directx.D3D.inputStateLTouch.Buttons;
            thumbStickLeft = SC_console_directx.D3D.inputStateLTouch.Thumbstick;
            handTriggerLeft = SC_console_directx.D3D.inputStateLTouch.HandTrigger;
            indexTriggerLeft = SC_console_directx.D3D.inputStateLTouch.IndexTrigger;
            handPoseLeft = trackingState.HandPoses[0].ThePose;

            _leftTouchQuat.X = handPoseLeft.Orientation.X;
            _leftTouchQuat.Y = handPoseLeft.Orientation.Y;
            _leftTouchQuat.Z = handPoseLeft.Orientation.Z;
            _leftTouchQuat.W = handPoseLeft.Orientation.W;

            _leftTouchQuat = new SharpDX.Quaternion(handPoseLeft.Orientation.X, handPoseLeft.Orientation.Y, handPoseLeft.Orientation.Z, handPoseLeft.Orientation.W);

            SharpDX.Matrix.RotationQuaternion(ref _leftTouchQuat, out _leftTouchMatrix);
            //_other_left_touch_matrix = _leftTouchMatrix;
            //_other_left_touch_matrix.M41 = handPoseLeft.Position.X;
            //_other_left_touch_matrix.M42 = handPoseLeft.Position.Y;
            //_other_left_touch_matrix.M43 = handPoseLeft.Position.Z;

            _leftTouchMatrix.M41 = handPoseLeft.Position.X + SC_Update.originPos.X + SC_Update.movePos.X;
            _leftTouchMatrix.M42 = handPoseLeft.Position.Y + SC_Update.originPos.Y + SC_Update.movePos.Y;
            _leftTouchMatrix.M43 = handPoseLeft.Position.Z + SC_Update.originPos.Z + SC_Update.movePos.Z;




            //FOR WHEN THERE IS MY PHYSICS DESKTOP SCREEN

            Quaternion _quat_screen00;

            Matrix mater = worldMatrix_instances_screens[0][0][0];
            Quaternion.RotationMatrix(ref mater, out _quat_screen00);
            var screenNormal = sc_maths._getDirection(Vector3.ForwardRH, _quat_screen00);
            screenNormal.Normalize();

            var planer = new Plane(new Vector3(mater.M41, mater.M42, mater.M43), screenNormal);
            var screen_mat = worldMatrix_instances_screens[0][0][0];
            var somematroxer2 = screen_mat;
            Quaternion _quat_screen;
            Quaternion.RotationMatrix(ref screen_mat, out _quat_screen);
            screenNormal = sc_maths._getDirection(Vector3.ForwardRH, _quat_screen);
            screenNormal.Normalize();
            planer = new Plane(new Vector3(screen_mat.M41, screen_mat.M42, screen_mat.M43), screenNormal);

            //FOR SCREEN COLLISION DETECTION
            var centerPosRight = new Vector3(final_hand_pos_right_locked.M41, final_hand_pos_right_locked.M42, final_hand_pos_right_locked.M43);
            Quaternion.RotationMatrix(ref final_hand_pos_right_locked, out _rightTouchQuat);
            var rayDirRighter = sc_maths._getDirection(Vector3.ForwardRH, _rightTouchQuat);
            var someRay = new Ray(centerPosRight, rayDirRighter);
            var intersecter = someRay.Intersects(ref planer, out intersectPointRight);
            var _ray_dir_right = centerPosRight - intersectPointRight;
            var _length_of_ray_right = (_ray_dir_right).Length();

            var centerPosLeft = new Vector3(final_hand_pos_left_locked.M41, final_hand_pos_left_locked.M42, final_hand_pos_left_locked.M43);
            Quaternion.RotationMatrix(ref final_hand_pos_left_locked, out _leftTouchQuat);
            var rayDirLeft = sc_maths._getDirection(Vector3.ForwardRH, _leftTouchQuat);
            var someRayLeft = new Ray(centerPosLeft, rayDirLeft);
            var intersecterLeft = someRayLeft.Intersects(ref planer, out intersectPointLeft);
            var _ray_dir_left = centerPosLeft - intersectPointLeft;
            var _length_of_ray_left = (_ray_dir_left).Length();

            //SPECTRUM AND SOUND RECORDING

            if (has_spoken_sec == 1)
            {

                //Program.MessageBox((IntPtr)0, "" + "0", "sccoresystems", 0);
                //_sound_byte_array.OrderBy(x => String.Join(String.Empty, x));
                //_sound_byte_array.OrderBy(x > x).FirstOrDefault();
                //_sound_byte_array = _sound_byte_array.ToList().OrderBy(x => x).ToArray(); // new List<byte>();
                //_sound_byte_array = list.OrderBy(x => x).ToArray();
                //var test = _sound_byte_array.OrderBy(x => x, new ByteListComparer());

                int count_spec = 0;

                var widthL = (int)(_inst_spectrum_x / 2);
                var widthR = (int)((_inst_spectrum_x / 2) - 1);

                var heightL = _inst_spectrum_y;
                var heightR = _inst_spectrum_y;

                var depthL = (int)(_inst_spectrum_z / 2);
                var depthR = (int)((_inst_spectrum_z / 2) - 1);

                //var depthL = _inst_spectrum_z;// (int)(_inst_spectrum_z / 2);
                //var depthR = _inst_spectrum_z;// (int)((_inst_spectrum_z / 2) - 1);

                // wL5 and <=wR4  = -5 to 5 ... 210/2 = 105 and 105 so 105-1 = 104 for wL105 and <=wR104   

                for (int x = -widthL; x <= widthR; x++)
                {
                    for (int y = -heightL; y <= heightR; y++)
                    {
                        for (int z = -depthL; z <= depthR; z++)
                        {
                            //Program.MessageBox((IntPtr)0, "" + "0", "sccoresystems", 0);

                            //the higher the closer to center middle.

                            float posX = (x);
                            float posY = (y);
                            float posZ = (z);

                            var xx = x;
                            var yy = y;
                            var zz = z;

                            if (xx < 0)
                            {
                                xx *= -1;
                                xx = widthR + xx;
                            }
                            if (yy < 0)
                            {
                                yy *= -1;
                                yy = heightR + yy;
                            }
                            if (zz < 0)
                            {
                                zz *= -1;
                                zz = depthR + zz;
                            }

                            var _index = xx + _inst_spectrum_x * (yy + _inst_spectrum_y * zz);

                            Vector3 pos = new Vector3(posX, posY, posZ);

                            if (_index < _sound_byte_array.Length)
                            {
                                //Program.MessageBox((IntPtr)0, "" + _index, "sccoresystems", 0);
                            }
                            else
                            {
                                //Program.MessageBox((IntPtr)0, "" + _index, "sccoresystems", 0);
                            }

                            //var dist = sc_maths.sc_check_distance_node_3d_geometry(Vector3.Zero, pos, 9, 9, 9, 9, 9, 9);

                            try
                            {
                                if (_index < _world_spectrum_list[0][0]._arrayOfInstances.Length && _index < _sound_byte_array.Length) //_inst_spectrum_x * _inst_spectrum_y * _inst_spectrum_z)
                                {
                                    if (_sound_byte_array != null)
                                    {
                                        if (_sound_byte_array.Length > 0)
                                        {
                                            var _left_touch_pos = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43);
                                            /*
                                            if (count_spec < _sound_byte_array.Length && count_spec < _world_spectrum_list[0]._arrayOfInstances.Length) // 
                                            {
                                             
                                                /*if (_has_recorded == 1)
                                                {
                                                    var planeSize = 0.01f;
                                                    var detailscale = 10;
                                                    var heightmul = 20;
                                                    var seed = 3420;
                                                    var fastNoise = new FastNoise(seed);
                                                    float noise = fastNoise.GetPerlin((((spectrum_mat.M41 * planeSize) + seed) / detailscale) * heightmul, (((spectrum_mat.M42 * planeSize) + seed) / detailscale) * heightmul, (((spectrum_mat.M43 * planeSize) + seed) / detailscale) * heightmul);
                                                    _has_recorded = 2;
                                                }
                                                else
                                                {
                                                    //spectrum_mat.M41 = _left_touch_pos.X + _world_spectrum_list[0]._arrayOfInstances[s]._POSITION.M41;
                                                    //spectrum_mat.M42 = _left_touch_pos.Y + _world_spectrum_list[0]._arrayOfInstances[s]._POSITION.M42;
                                                    //spectrum_mat.M43 = _left_touch_pos.Z + _world_spectrum_list[0]._arrayOfInstances[s]._POSITION.M43;
                                                }
                                            }
                                            else
                                            {
                                                spectrum_mat.M41 = _left_touch_pos.X + _world_spectrum_list[0]._arrayOfInstances[_index]._POSITION.M41;
                                                spectrum_mat.M42 = _left_touch_pos.Y + _world_spectrum_list[0]._arrayOfInstances[_index]._POSITION.M42 + spectrum_noise_value; //0.0015f // + (_sound_byte_array[count_spec] * 0.0005f)
                                                spectrum_mat.M43 = _left_touch_pos.Z + _world_spectrum_list[0]._arrayOfInstances[_index]._POSITION.M43;
                                                //count_spec = 0;
                                            }*/
                                            spectrum_mat.M41 = _world_spectrum_list[0][0]._arrayOfInstances[_index]._POSITION.M41;
                                            spectrum_mat.M42 = _world_spectrum_list[0][0]._arrayOfInstances[_index]._POSITION.M42 + spectrum_noise_value + (_sound_byte_array[_index] * 0.005f); //0.0015f
                                            spectrum_mat.M43 = _world_spectrum_list[0][0]._arrayOfInstances[_index]._POSITION.M43;

                                            worldMatrix_instances_spectrum[0][0][_index] = spectrum_mat;
                                            count_spec++;

                                            /*else
                                            {
                                                var _left_touch_pos = new Vector3(WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43);

                                                spectrum_mat.M41 = _left_touch_pos.X + _world_spectrum_list[0]._arrayOfInstances[_index]._POSITION.M41;
                                                spectrum_mat.M42 = _left_touch_pos.Y + _world_spectrum_list[0]._arrayOfInstances[_index]._POSITION.M42 + spectrum_noise_value + (_sound_byte_array[0] * 0.0015f);
                                                spectrum_mat.M43 = _left_touch_pos.Z + _world_spectrum_list[0]._arrayOfInstances[_index]._POSITION.M43;

                                                if (_has_recorded == 1)
                                                {
                                                    var planeSize = 0.01f;
                                                    var detailscale = 100;
                                                    var heightmul = 20;
                                                    var seed = 3420;
                                                    var fastNoise = new FastNoise(3420);
                                                    float noise = fastNoise.GetPerlin((((spectrum_mat.M41 * planeSize) + seed) / detailscale) * heightmul, (((spectrum_mat.M42 * planeSize) + seed) / detailscale) * heightmul, (((spectrum_mat.M43 * planeSize) + seed) / detailscale) * heightmul);
                                                    _has_recorded = 2;
                                                }
                                                else
                                                {
                                                    //spectrum_mat.M41 = _left_touch_pos.X + _world_spectrum_list[0]._arrayOfInstances[s]._POSITION.M41;
                                                    //spectrum_mat.M42 = _left_touch_pos.Y + _world_spectrum_list[0]._arrayOfInstances[s]._POSITION.M42;
                                                    //spectrum_mat.M43 = _left_touch_pos.Z + _world_spectrum_list[0]._arrayOfInstances[s]._POSITION.M43;
                                                }
                                                worldMatrix_instances_spectrum[0][_index] = spectrum_mat;
                                            }*/
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Program.MessageBox((IntPtr)0, "" + ex.ToString(), "sccoresystems", 0);
                            }
                            //for (int sba = 0; sba < _sound_byte_array.Length; sba++)
                            //{
                            //
                            //}
                        }
                    }
                }

                //_sound_byte_array = list.OrderBy(x => x).ThenBy().ToArray();

                /*for (int s = 0; s < worldMatrix_instances_spectrum[0].Length; s++)
                {

                }*/
                has_spoken_sec = 0;
                has_spoken_main = 0;
            }

            //Console.WriteLine("test0");

            //RECORD SOUND
            if (Program._keyboard_input._KeyboardState.PressedKeys.Contains(SharpDX.DirectInput.Key.R))
            {
                if (sc_start_recording == 0)
                {
                    _records_instant_counter = 0;
                    _time_of_recording_start = DateTime.Now;

                    mciSendString("open new Type waveaudio Alias recsound", null, 0, IntPtr.Zero);
                    mciSendString("record recsound", null, 0, IntPtr.Zero);

                    sc_start_recording = 1;
                    has_spoken_main = 1;
                    //has_spoken_sec = 1;
                }
            }

            if (swtchinstantsound == 1)
            {
                if (has_spoken_tier == 1 && has_spoken_main == 1)
                {
                    instant_short_path = "wave_instant_audio_" + _records_instant_counter;
                    var filename = @"C:\Users\ninekorn\Desktop\#RECINSTANTSOUND\" + "wave_instant_audio_" + _records_instant_counter + ".wav";
                    //var filename = @"D:\#RECINSTANTSOUND\" + "wave_instant_audio_" + _records_instant_counter + ".wav";

                    mciSendString("save recinstantsound " + filename, null, 0, IntPtr.Zero);
                    mciSendString("close recinstantsound", null, 0, IntPtr.Zero);

                    /*Process p = new Process();
                    p.StartInfo = new ProcessStartInfo()
                    {
                        FileName = short_path
                    };
                    p.Start();
                    p.Refresh();
                    p.Close();*/

                    DirectoryInfo dir = new DirectoryInfo(filename);
                    dir.Refresh();
                    if (!File.Exists(filename))
                    {
                        FileInfo filinfo = new FileInfo(filename);
                        filinfo.Refresh();
                    }

                    var nativeFileStream = new NativeFileStream(filename, NativeFileMode.Open, NativeFileAccess.Read, NativeFileShare.Read);
                    SoundStream soundStream = new SoundStream(nativeFileStream);
                    MemoryStream ms = new MemoryStream();

                    soundStream.CopyTo(ms);
                    _sound_byte_array_instant = ms.ToArray();

                    soundStream.Dispose();

                    ms.Dispose();
                    dir.Refresh();

                    if (File.Exists(filename))
                    {
                        File.Delete(filename);
                    }

                    _records_instant_counter++;
                    has_spoken_tier = 2;
                }


                if (has_spoken_main == 1)
                {
                    mciSendString("open new Type waveaudio Alias recinstantsound", null, 0, IntPtr.Zero);
                    mciSendString("record recinstantsound", null, 0, IntPtr.Zero);
                    //has_spoken_main = 1;
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
            if (Program._keyboard_input._KeyboardState.PressedKeys.Contains(SharpDX.DirectInput.Key.S))
            {
                if (sc_start_recording == 1)
                {
                    if (sc_save_file == 0)
                    {

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

                        /*var nativeFileStream = new NativeFileStream(last_wave_filepath, NativeFileMode.Open, NativeFileAccess.Read, NativeFileShare.Read);
                        SoundStream soundStream = new SoundStream(nativeFileStream);
                        MemoryStream ms = new MemoryStream();

                        soundStream.CopyTo(ms);
                        _sound_byte_array = ms.ToArray();*/

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
                        has_spoken_main = 1;
                        has_spoken_sec = 1;
                        //has_spoken_tier = 0;
                        //has_spoken_quart = 0;
                    }
                }
            }

            //PLAY SOUND AT INDEX
            if (Program._keyboard_input._KeyboardState.PressedKeys.Contains(SharpDX.DirectInput.Key.P))
            {
                if (sc_play_file == 0)
                {
                    if (File.Exists(last_wave_filepath))
                    {
                        _sound_player.Play((_records_counter - 1) + "");

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
                        p.Close();*/

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
            }















            //PHYSICS FOOT RIGHT
            _player_r_foot[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_foot[0][0];
            _player_r_foot[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS FOOT LEFT
            _player_l_foot[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_foot[0][0];
            _player_l_foot[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS UPPER LEG RIGHT
            _player_r_upper_leg[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_upper_leg[0][0];
            _player_r_upper_leg[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS UPPER LEG LEFT
            _player_l_upper_leg[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_upper_leg[0][0];
            _player_l_upper_leg[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS LOWER LEG RIGHT
            _player_r_lower_leg[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_lower_leg[0][0];
            _player_r_lower_leg[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS LOWER LEG LEFT
            _player_l_lower_leg[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_lower_leg[0][0];
            _player_l_lower_leg[0][0]._POSITION = worldMatrix_base[0];


            //PHYSICS LOWER LEG RIGHT
            _player_rght_target_knee[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_target_knee[0][0];
            _player_rght_target_knee[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS LOWER LEG LEFT
            _player_lft_target_knee[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_target_knee[0][0];
            _player_lft_target_knee[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS LOWER LEG RIGHT
            _player_rght_target_two_knee[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_target_two_knee[0][0];
            _player_rght_target_two_knee[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS LOWER LEG LEFT
            _player_lft_target_two_knee[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_target_two_knee[0][0];
            _player_lft_target_two_knee[0][0]._POSITION = worldMatrix_base[0];


            //PHYSICS HAND RIGHT GRAB
            _player_r_hand_grab[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_hand_grab[0][0];
            _player_r_hand_grab[0][0]._POSITION = worldMatrix_base[0];


            //PHYSICS HAND LEFT GRAB
            _player_l_hand_grab[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_hand_grab[0][0];
            _player_l_hand_grab[0][0]._POSITION = worldMatrix_base[0];


            //PHYSICS HAND RIGHT
            _player_rght_hnd[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_hand[0][0];
            _player_rght_hnd[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS HAND LEFT
            _player_lft_hnd[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_hand[0][0];
            _player_lft_hnd[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS UPPER ARM LEFT
            _player_lft_upper_arm[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_upperarm[0][0];
            _player_lft_upper_arm[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS LOWER ARM LEFT
            _player_lft_lower_arm[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_lowerarm[0][0];
            _player_lft_lower_arm[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS LOWER ARM LEFT ELBOWTARGET
            _player_lft_elbow_target[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_elbow_target[0][0];
            _player_lft_elbow_target[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS LOWER ARM LEFT ELBOWTARGET TWO
            _player_lft_elbow_target_two[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_elbow_target_two[0][0];
            _player_lft_elbow_target_two[0][0]._POSITION = worldMatrix_base[0];

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

            //PHYSICS PELVIS
            _player_pelvis[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_pelvis[0][0];
            _player_pelvis[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS TORSO
            _player_torso[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_torso[0][0];
            _player_torso[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS TORSO
            _player_head[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_head[0][0];
            _player_head[0][0]._POSITION = worldMatrix_base[0];

            //PHYSICS LEFT SHOULDER
            _player_lft_shldr[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_shoulder[0][0];
            _player_lft_shldr[0][0]._POSITION = worldMatrix_base[0];

            //tick_perf_counter.Stop();
            //tick_perf_counter.Reset();
            //tick_perf_counter.Restart();




            //LOWER BODY
            voxel_cuber_r_foot = _player_r_foot[0][0];
            voxel_sometester_r_foot = voxel_cuber_r_foot._WORLDMATRIXINSTANCES;
            voxel_cuber_l_foot = _player_l_foot[0][0];
            voxel_sometester_l_foot = voxel_cuber_l_foot._WORLDMATRIXINSTANCES;
            voxel_cuber_r_upper_leg = _player_r_upper_leg[0][0];
            voxel_sometester_r_upper_leg = voxel_cuber_r_upper_leg._WORLDMATRIXINSTANCES;
            voxel_cuber_l_upper_leg = _player_l_upper_leg[0][0];
            voxel_sometester_l_upper_leg = voxel_cuber_l_upper_leg._WORLDMATRIXINSTANCES;
            voxel_cuber_r_lower_leg = _player_r_lower_leg[0][0];
            voxel_sometester_r_lower_leg = voxel_cuber_r_lower_leg._WORLDMATRIXINSTANCES;
            voxel_cuber_l_lower_leg = _player_l_lower_leg[0][0];
            voxel_sometester_l_lower_leg = voxel_cuber_l_lower_leg._WORLDMATRIXINSTANCES;
            voxel_cuber_l_targ_knee = _player_lft_target_knee[0][0];
            voxel_sometester_l_targ_knee = voxel_cuber_l_targ_knee._WORLDMATRIXINSTANCES;
            voxel_cuber_r_targ_knee = _player_rght_target_knee[0][0];
            voxel_sometester_r_targ_knee = voxel_cuber_r_targ_knee._WORLDMATRIXINSTANCES;
            voxel_cuber_l_targ_two_knee = _player_lft_target_two_knee[0][0];
            voxel_sometester_l_targ_two_knee = voxel_cuber_l_targ_two_knee._WORLDMATRIXINSTANCES;
            voxel_cuber_r_targ_two_knee = _player_rght_target_two_knee[0][0];
            voxel_sometester_r_targ_two_knee = voxel_cuber_r_targ_two_knee._WORLDMATRIXINSTANCES;


            //UPPER BODY
            voxel_cuber_r_hand_grab = _player_r_hand_grab[0][0];
            voxel_sometester_r_hand_grab = voxel_cuber_r_hand_grab._WORLDMATRIXINSTANCES;
            voxel_cuber_l_hand_grab = _player_l_hand_grab[0][0];
            voxel_sometester_l_hand_grab = voxel_cuber_l_hand_grab._WORLDMATRIXINSTANCES;

            voxel_cuber_r_hnd = _player_rght_hnd[0][0];
            voxel_sometester_r_hnd = voxel_cuber_r_hnd._WORLDMATRIXINSTANCES;
            voxel_cuber_l_hnd = _player_lft_hnd[0][0];
            voxel_sometester_l_hnd = voxel_cuber_l_hnd._WORLDMATRIXINSTANCES;
            voxel_cuber_l_up_arm = _player_lft_upper_arm[0][0];
            voxel_sometester_l_up_arm = voxel_cuber_l_up_arm._WORLDMATRIXINSTANCES;
            voxel_cuber_r_up_arm = _player_rght_upper_arm[0][0];
            voxel_sometester_r_up_arm = voxel_cuber_r_up_arm._WORLDMATRIXINSTANCES;
            voxel_cuber_l_low_arm = _player_lft_lower_arm[0][0];
            voxel_sometester_l_low_arm = voxel_cuber_l_low_arm._WORLDMATRIXINSTANCES;
            voxel_cuber_r_low_arm = _player_rght_lower_arm[0][0];
            voxel_sometester_r_low_arm = voxel_cuber_r_low_arm._WORLDMATRIXINSTANCES;
            voxel_cuber_l_shld = _player_lft_shldr[0][0];
            voxel_sometester_l_shld = voxel_cuber_l_shld._WORLDMATRIXINSTANCES;
            voxel_cuber_r_shld = _player_rght_shldr[0][0];
            voxel_sometester_r_shld = voxel_cuber_r_shld._WORLDMATRIXINSTANCES;
            voxel_cuber_l_targ = _player_lft_elbow_target[0][0];
            voxel_sometester_l_targ = voxel_cuber_l_targ._WORLDMATRIXINSTANCES;
            voxel_cuber_r_targ = _player_rght_elbow_target[0][0];
            voxel_sometester_r_targ = voxel_cuber_r_targ._WORLDMATRIXINSTANCES;
            voxel_cuber_l_targ_two = _player_lft_elbow_target_two[0][0];
            voxel_sometester_l_targ_two = voxel_cuber_l_targ_two._WORLDMATRIXINSTANCES;
            voxel_cuber_r_targ_two = _player_rght_elbow_target_two[0][0];
            voxel_sometester_r_targ_two = voxel_cuber_r_targ_two._WORLDMATRIXINSTANCES;
            voxel_cuber_pelvis = _player_pelvis[0][0];
            voxel_sometester_pelvis = voxel_cuber_pelvis._WORLDMATRIXINSTANCES;
            voxel_cuber_torso = _player_torso[0][0];
            voxel_sometester_torso = voxel_cuber_torso._WORLDMATRIXINSTANCES;




            var voxel_cuber_head = _player_head[0][0];
            var voxel_sometester_head = voxel_cuber_head._WORLDMATRIXINSTANCES;



            for (int i = 0; i < voxel_cuber_pelvis.instances.Length; i++)
            {


                //RIGHT HAND
                float xxx = voxel_sometester_r_hnd[i].M41;
                float yyy = voxel_sometester_r_hnd[i].M42;
                float zzz = voxel_sometester_r_hnd[i].M43;
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



                //LEFT HAND
                xxx = voxel_sometester_l_hnd[i].M41;
                yyy = voxel_sometester_l_hnd[i].M42;
                zzz = voxel_sometester_l_hnd[i].M43;
                voxel_cuber_l_hnd.instances[i].position.X = xxx;
                voxel_cuber_l_hnd.instances[i].position.Y = yyy;
                voxel_cuber_l_hnd.instances[i].position.Z = zzz;
                voxel_cuber_l_hnd.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_l_hnd[i], out _testQuater);
                dirInstance = -sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_l_hnd.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_l_hnd.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_hnd.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_hnd.instancesDataForward[i].rotation.W = 1;
                dirInstance = sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_l_hnd.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_l_hnd.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_hnd.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_hnd.instancesDataRIGHT[i].rotation.W = 1;
                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_l_hnd.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_l_hnd.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_hnd.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_hnd.instancesDataUP[i].rotation.W = 1;



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

                //LEFT HAND GRAB
                xxx = voxel_sometester_l_hand_grab[i].M41;
                yyy = voxel_sometester_l_hand_grab[i].M42;
                zzz = voxel_sometester_l_hand_grab[i].M43;
                voxel_cuber_l_hand_grab.instances[i].position.X = xxx;
                voxel_cuber_l_hand_grab.instances[i].position.Y = yyy;
                voxel_cuber_l_hand_grab.instances[i].position.Z = zzz;
                voxel_cuber_l_hand_grab.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_l_hand_grab[i], out _testQuater);
                dirInstance = -sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_l_hand_grab.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_l_hand_grab.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_hand_grab.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_hand_grab.instancesDataForward[i].rotation.W = 1;
                dirInstance = sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_l_hand_grab.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_l_hand_grab.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_hand_grab.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_hand_grab.instancesDataRIGHT[i].rotation.W = 1;
                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_l_hand_grab.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_l_hand_grab.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_hand_grab.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_hand_grab.instancesDataUP[i].rotation.W = 1;







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

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_l_up_arm.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_l_up_arm.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_up_arm.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_up_arm.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_l_up_arm.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_l_up_arm.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_up_arm.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_up_arm.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
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

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_l_low_arm.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_l_low_arm.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_low_arm.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_low_arm.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_l_low_arm.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_l_low_arm.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_low_arm.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_low_arm.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
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

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_l_targ.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_l_targ.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_targ.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_targ.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_l_targ.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_l_targ.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_targ.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_targ.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
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

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_l_targ_two.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_l_targ_two.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_targ_two.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_targ_two.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_l_targ_two.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_l_targ_two.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_targ_two.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_targ_two.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
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




                xxx = voxel_sometester_l_shld[i].M41;
                yyy = voxel_sometester_l_shld[i].M42;
                zzz = voxel_sometester_l_shld[i].M43;

                voxel_cuber_l_shld.instances[i].position.X = xxx;
                voxel_cuber_l_shld.instances[i].position.Y = yyy;
                voxel_cuber_l_shld.instances[i].position.Z = zzz;
                voxel_cuber_l_shld.instances[i].position.W = 1;
                // Quaternion _testQuater;
                Quaternion.RotationMatrix(ref voxel_sometester_l_shld[i], out _testQuater);

                dirInstance = sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_l_shld.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_l_shld.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_shld.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_shld.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_l_shld.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_l_shld.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_shld.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_shld.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
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

                dirInstance = sc_maths._newgetdirforward(_testQuater);
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









                //LEFT FOOT
                xxx = voxel_sometester_l_foot[i].M41;
                yyy = voxel_sometester_l_foot[i].M42;
                zzz = voxel_sometester_l_foot[i].M43;
                voxel_cuber_l_foot.instances[i].position.X = xxx;
                voxel_cuber_l_foot.instances[i].position.Y = yyy;
                voxel_cuber_l_foot.instances[i].position.Z = zzz;
                voxel_cuber_l_foot.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_l_foot[i], out _testQuater);
                dirInstance = -sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_l_foot.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_l_foot.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_foot.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_foot.instancesDataForward[i].rotation.W = 1;
                dirInstance = sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_l_foot.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_l_foot.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_foot.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_foot.instancesDataRIGHT[i].rotation.W = 1;
                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_l_foot.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_l_foot.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_foot.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_foot.instancesDataUP[i].rotation.W = 1;

                //RIGHT FOOT
                xxx = voxel_sometester_r_foot[i].M41;
                yyy = voxel_sometester_r_foot[i].M42;
                zzz = voxel_sometester_r_foot[i].M43;
                voxel_cuber_r_foot.instances[i].position.X = xxx;
                voxel_cuber_r_foot.instances[i].position.Y = yyy;
                voxel_cuber_r_foot.instances[i].position.Z = zzz;
                voxel_cuber_r_foot.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_r_foot[i], out _testQuater);
                dirInstance = -sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_foot.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_foot.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_foot.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_foot.instancesDataForward[i].rotation.W = 1;
                dirInstance = sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_foot.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_foot.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_foot.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_foot.instancesDataRIGHT[i].rotation.W = 1;
                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_foot.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_foot.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_foot.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_foot.instancesDataUP[i].rotation.W = 1;




                //LEFT LOWER LEG
                xxx = voxel_sometester_l_lower_leg[i].M41;
                yyy = voxel_sometester_l_lower_leg[i].M42;
                zzz = voxel_sometester_l_lower_leg[i].M43;
                voxel_cuber_l_lower_leg.instances[i].position.X = xxx;
                voxel_cuber_l_lower_leg.instances[i].position.Y = yyy;
                voxel_cuber_l_lower_leg.instances[i].position.Z = zzz;
                voxel_cuber_l_lower_leg.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_l_lower_leg[i], out _testQuater);
                dirInstance = -sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_l_lower_leg.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_l_lower_leg.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_lower_leg.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_lower_leg.instancesDataForward[i].rotation.W = 1;
                dirInstance = sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_l_lower_leg.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_l_lower_leg.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_lower_leg.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_lower_leg.instancesDataRIGHT[i].rotation.W = 1;
                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_l_lower_leg.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_l_lower_leg.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_lower_leg.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_lower_leg.instancesDataUP[i].rotation.W = 1;


                //RIGHT LOWER LEG
                xxx = voxel_sometester_r_lower_leg[i].M41;
                yyy = voxel_sometester_r_lower_leg[i].M42;
                zzz = voxel_sometester_r_lower_leg[i].M43;
                voxel_cuber_r_lower_leg.instances[i].position.X = xxx;
                voxel_cuber_r_lower_leg.instances[i].position.Y = yyy;
                voxel_cuber_r_lower_leg.instances[i].position.Z = zzz;
                voxel_cuber_r_lower_leg.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_r_lower_leg[i], out _testQuater);
                dirInstance = -sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_lower_leg.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_lower_leg.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_lower_leg.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_lower_leg.instancesDataForward[i].rotation.W = 1;
                dirInstance = sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_lower_leg.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_lower_leg.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_lower_leg.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_lower_leg.instancesDataRIGHT[i].rotation.W = 1;
                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_lower_leg.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_lower_leg.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_lower_leg.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_lower_leg.instancesDataUP[i].rotation.W = 1;






                //LEFT UPPER LEG
                xxx = voxel_sometester_l_upper_leg[i].M41;
                yyy = voxel_sometester_l_upper_leg[i].M42;
                zzz = voxel_sometester_l_upper_leg[i].M43;
                voxel_cuber_l_upper_leg.instances[i].position.X = xxx;
                voxel_cuber_l_upper_leg.instances[i].position.Y = yyy;
                voxel_cuber_l_upper_leg.instances[i].position.Z = zzz;
                voxel_cuber_l_upper_leg.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_l_upper_leg[i], out _testQuater);
                dirInstance = -sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_l_upper_leg.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_l_upper_leg.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_upper_leg.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_upper_leg.instancesDataForward[i].rotation.W = 1;
                dirInstance = sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_l_upper_leg.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_l_upper_leg.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_upper_leg.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_upper_leg.instancesDataRIGHT[i].rotation.W = 1;
                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_l_upper_leg.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_l_upper_leg.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_upper_leg.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_upper_leg.instancesDataUP[i].rotation.W = 1;


                //RIGHT UPPER LEG
                xxx = voxel_sometester_r_upper_leg[i].M41;
                yyy = voxel_sometester_r_upper_leg[i].M42;
                zzz = voxel_sometester_r_upper_leg[i].M43;
                voxel_cuber_r_upper_leg.instances[i].position.X = xxx;
                voxel_cuber_r_upper_leg.instances[i].position.Y = yyy;
                voxel_cuber_r_upper_leg.instances[i].position.Z = zzz;
                voxel_cuber_r_upper_leg.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_r_upper_leg[i], out _testQuater);
                dirInstance = -sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_upper_leg.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_upper_leg.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_upper_leg.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_upper_leg.instancesDataForward[i].rotation.W = 1;
                dirInstance = sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_upper_leg.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_upper_leg.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_upper_leg.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_upper_leg.instancesDataRIGHT[i].rotation.W = 1;
                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_upper_leg.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_upper_leg.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_upper_leg.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_upper_leg.instancesDataUP[i].rotation.W = 1;







                //RIGHT TARGET KNEE
                xxx = voxel_sometester_l_targ_knee[i].M41;
                yyy = voxel_sometester_l_targ_knee[i].M42;
                zzz = voxel_sometester_l_targ_knee[i].M43;
                voxel_cuber_l_targ_knee.instances[i].position.X = xxx;
                voxel_cuber_l_targ_knee.instances[i].position.Y = yyy;
                voxel_cuber_l_targ_knee.instances[i].position.Z = zzz;
                voxel_cuber_l_targ_knee.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_l_targ_knee[i], out _testQuater);
                dirInstance = -sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_l_targ_knee.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_l_targ_knee.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_targ_knee.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_targ_knee.instancesDataForward[i].rotation.W = 1;
                dirInstance = sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_l_targ_knee.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_l_targ_knee.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_targ_knee.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_targ_knee.instancesDataRIGHT[i].rotation.W = 1;
                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_l_targ_knee.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_l_targ_knee.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_targ_knee.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_targ_knee.instancesDataUP[i].rotation.W = 1;



                //LEFT TARGET KNEE
                xxx = voxel_sometester_r_targ_knee[i].M41;
                yyy = voxel_sometester_r_targ_knee[i].M42;
                zzz = voxel_sometester_r_targ_knee[i].M43;
                voxel_cuber_r_targ_knee.instances[i].position.X = xxx;
                voxel_cuber_r_targ_knee.instances[i].position.Y = yyy;
                voxel_cuber_r_targ_knee.instances[i].position.Z = zzz;
                voxel_cuber_r_targ_knee.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_r_targ_knee[i], out _testQuater);
                dirInstance = -sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_targ_knee.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_knee.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_knee.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_knee.instancesDataForward[i].rotation.W = 1;
                dirInstance = sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_targ_knee.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_knee.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_knee.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_knee.instancesDataRIGHT[i].rotation.W = 1;
                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_targ_knee.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_knee.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_knee.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_knee.instancesDataUP[i].rotation.W = 1;







                //RIGHT TARGET KNEE TWO
                xxx = voxel_sometester_l_targ_two_knee[i].M41;
                yyy = voxel_sometester_l_targ_two_knee[i].M42;
                zzz = voxel_sometester_l_targ_two_knee[i].M43;
                voxel_cuber_l_targ_two_knee.instances[i].position.X = xxx;
                voxel_cuber_l_targ_two_knee.instances[i].position.Y = yyy;
                voxel_cuber_l_targ_two_knee.instances[i].position.Z = zzz;
                voxel_cuber_l_targ_two_knee.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_l_targ_two_knee[i], out _testQuater);
                dirInstance = -sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_l_targ_two_knee.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_l_targ_two_knee.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_targ_two_knee.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_targ_two_knee.instancesDataForward[i].rotation.W = 1;
                dirInstance = sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_l_targ_two_knee.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_l_targ_two_knee.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_targ_two_knee.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_targ_two_knee.instancesDataRIGHT[i].rotation.W = 1;
                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_l_targ_two_knee.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_l_targ_two_knee.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_l_targ_two_knee.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_l_targ_two_knee.instancesDataUP[i].rotation.W = 1;


                //RIGHT TARGET KNEE TWO
                xxx = voxel_sometester_r_targ_two_knee[i].M41;
                yyy = voxel_sometester_r_targ_two_knee[i].M42;
                zzz = voxel_sometester_r_targ_two_knee[i].M43;
                voxel_cuber_r_targ_two_knee.instances[i].position.X = xxx;
                voxel_cuber_r_targ_two_knee.instances[i].position.Y = yyy;
                voxel_cuber_r_targ_two_knee.instances[i].position.Z = zzz;
                voxel_cuber_r_targ_two_knee.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref voxel_sometester_r_targ_two_knee[i], out _testQuater);
                dirInstance = -sc_maths._newgetdirforward(_testQuater);
                voxel_cuber_r_targ_two_knee.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_two_knee.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_two_knee.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_two_knee.instancesDataForward[i].rotation.W = 1;
                dirInstance = sc_maths._newgetdirleft(_testQuater);
                voxel_cuber_r_targ_two_knee.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_two_knee.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_two_knee.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_two_knee.instancesDataRIGHT[i].rotation.W = 1;
                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                voxel_cuber_r_targ_two_knee.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cuber_r_targ_two_knee.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cuber_r_targ_two_knee.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cuber_r_targ_two_knee.instancesDataUP[i].rotation.W = 1;
            }























            //SC BUFFERS
            //SC BUFFERS
            //SC BUFFERS
            //TERRAIN CUBE
            _terrain[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_terrain[0][0];
            _terrain[0][0]._POSITION = worldMatrix_base[0];

            var cuber = _terrain[0][0];
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
                Quaternion.RotationMatrix(ref sometester[i], out quat_buffers);

                var dirInstance = sc_maths._newgetdirforward(quat_buffers);
                cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(quat_buffers);
                cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(quat_buffers);
                cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataUP[i].rotation.W = 1;
            }
            //END OF




            //SPECTRUM
            _world_spectrum_list[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_spectrum[0][0];
            _world_spectrum_list[0][0]._POSITION = worldMatrix_base[0];

            var cuber_spectrum = _world_spectrum_list[0][0];
            var instancers_spectrum = cuber_spectrum.instances;
            var sometester_spectrum = cuber_spectrum._WORLDMATRIXINSTANCES;

            for (int i = 0; i < instancers_spectrum.Length; i++)
            {
                float xxx = sometester_spectrum[i].M41;
                float yyy = sometester_spectrum[i].M42;
                float zzz = sometester_spectrum[i].M43;

                cuber_spectrum.instances[i].position.X = xxx;
                cuber_spectrum.instances[i].position.Y = yyy;
                cuber_spectrum.instances[i].position.Z = zzz;
                cuber_spectrum.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref sometester_spectrum[i], out quat_buffers);

                var dirInstance = sc_maths._newgetdirforward(quat_buffers);
                cuber_spectrum.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber_spectrum.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber_spectrum.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber_spectrum.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(quat_buffers);
                cuber_spectrum.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber_spectrum.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber_spectrum.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber_spectrum.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(quat_buffers);
                cuber_spectrum.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber_spectrum.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber_spectrum.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber_spectrum.instancesDataUP[i].rotation.W = 1;
            }
            //END OF












            //TERRAIN FLOOR
            _floor[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_floor[0][0];
            _floor[0][0]._POSITION = worldMatrix_base[0];

            var cuber_floor = _floor[0][0];
            var instancers_floor = cuber_floor.instances;
            var sometester_floor = cuber_floor._WORLDMATRIXINSTANCES;

            for (int i = 0; i < instancers_floor.Length; i++)
            {
                float xxx = sometester_floor[i].M41;
                float yyy = sometester_floor[i].M42;
                float zzz = sometester_floor[i].M43;

                cuber_floor.instances[i].position.X = xxx;
                cuber_floor.instances[i].position.Y = yyy;
                cuber_floor.instances[i].position.Z = zzz;
                cuber_floor.instances[i].position.W = 1;

                Quaternion.RotationMatrix(ref sometester_floor[i], out quat_buffers);

                var dirInstance = sc_maths._newgetdirforward(quat_buffers);
                cuber_floor.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber_floor.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber_floor.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber_floor.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(quat_buffers);
                cuber_floor.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber_floor.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber_floor.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber_floor.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(quat_buffers);
                cuber_floor.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber_floor.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber_floor.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber_floor.instancesDataUP[i].rotation.W = 1;
            }
            //END OF




            //PHYSICS GRID
            _world_grid_list[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_grid[0][0]; // 
            _world_grid_list[0][0]._POSITION = worldMatrix_base[0];

            var cuber_grid = _world_grid_list[0][0];
            var instancers_grid = cuber_grid.instances;
            var sometester_grid = cuber_grid._WORLDMATRIXINSTANCES;

            for (int i = 0; i < instancers_grid.Length; i++)
            {
                float xxx = sometester_grid[i].M41;
                float yyy = sometester_grid[i].M42;
                float zzz = sometester_grid[i].M43;

                cuber_grid.instances[i].position.X = xxx;
                cuber_grid.instances[i].position.Y = yyy;
                cuber_grid.instances[i].position.Z = zzz;
                cuber_grid.instances[i].position.W = 1;

                Quaternion.RotationMatrix(ref sometester_grid[i], out quat_buffers);

                var dirInstance = sc_maths._newgetdirforward(quat_buffers);
                //var dirInstance = _newgetdirforward(_testQuater);
                cuber_grid.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber_grid.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber_grid.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber_grid.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(quat_buffers);
                //dirInstance = -_newgetdirleft(_testQuater);
                cuber_grid.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber_grid.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber_grid.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber_grid.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = sc_maths._newgetdirup(quat_buffers);
                //dirInstance = dirInstance = _newgetdirup(_testQuater);
                cuber_grid.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber_grid.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber_grid.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber_grid.instancesDataUP[i].rotation.W = 1;
            }
            //END OF


            //PHYSICS GRID
            _world_containment_grid_screen[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_containment_grid_screen[0][0]; // 
            _world_containment_grid_screen[0][0]._POSITION = worldMatrix_base[0];

            var cuber_grid_screen = _world_containment_grid_screen[0][0];
            var instancers_grid_screen = cuber_grid_screen.instances;
            var sometester_grid_screen = cuber_grid_screen._WORLDMATRIXINSTANCES;

            for (int i = 0; i < instancers_grid_screen.Length; i++)
            {
                float xxx = sometester_grid_screen[i].M41;
                float yyy = sometester_grid_screen[i].M42;
                float zzz = sometester_grid_screen[i].M43;

                cuber_grid_screen.instances[i].position.X = xxx;
                cuber_grid_screen.instances[i].position.Y = yyy;
                cuber_grid_screen.instances[i].position.Z = zzz;
                cuber_grid_screen.instances[i].position.W = 1;

                Quaternion.RotationMatrix(ref sometester_grid_screen[i], out quat_buffers);

                var dirInstance = sc_maths._newgetdirforward(quat_buffers);
                //var dirInstance = _newgetdirforward(_testQuater);
                cuber_grid_screen.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber_grid_screen.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber_grid_screen.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber_grid_screen.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(quat_buffers);
                //dirInstance = -_newgetdirleft(_testQuater);
                cuber_grid_screen.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber_grid_screen.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber_grid_screen.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber_grid_screen.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = sc_maths._newgetdirup(quat_buffers);
                //dirInstance = dirInstance = _newgetdirup(_testQuater);
                cuber_grid_screen.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber_grid_screen.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber_grid_screen.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber_grid_screen.instancesDataUP[i].rotation.W = 1;
            }
            //END OF

            //PHYSICS CONTAINMENT GRID HANDRIGHT
            _world_containment_grid_list_RH[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_r_hand[0][0];// worldMatrix_instances_containment_grid_RH[0][0];// _world_containment_grid_list_RH[0][0]._arrayOfInstances[_iterator].current_pos;// worldMatrix_instances_containment_grid_RH[0][0]; // 
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
            //END OF

            //PHYSICS CONTAINMENT GRID HANDLEFT
            _world_containment_grid_list_LH[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_l_hand[0][0];// worldMatrix_instances_containment_grid_LH[0][0];// _world_containment_grid_list_RH[0][0]._arrayOfInstances[_iterator].current_pos;// worldMatrix_instances_containment_grid_RH[0][0]; // 
            _world_containment_grid_list_LH[0][0]._POSITION = worldMatrix_base[0];

            var cuber_containment_grid_LH = _world_containment_grid_list_LH[0][0];
            var instancers_containment_grid_LH = cuber_containment_grid_LH.instances;
            var sometester_containment_grid_LH = cuber_containment_grid_LH._WORLDMATRIXINSTANCES;

            for (int i = 0; i < instancers_containment_grid_LH.Length; i++)
            {
                float xxx = sometester_containment_grid_LH[i].M41;
                float yyy = sometester_containment_grid_LH[i].M42;
                float zzz = sometester_containment_grid_LH[i].M43;

                cuber_containment_grid_LH.instances[i].position.X = xxx;
                cuber_containment_grid_LH.instances[i].position.Y = yyy;
                cuber_containment_grid_LH.instances[i].position.Z = zzz;
                cuber_containment_grid_LH.instances[i].position.W = 1;

                Quaternion.RotationMatrix(ref sometester_containment_grid_LH[i], out quat_buffers);

                var dirInstance = sc_maths._newgetdirforward(quat_buffers);
                //var dirInstance = _newgetdirforward(_testQuater);
                cuber_containment_grid_LH.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber_containment_grid_LH.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber_containment_grid_LH.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber_containment_grid_LH.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(quat_buffers);
                //dirInstance = -_newgetdirleft(_testQuater);
                cuber_containment_grid_LH.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber_containment_grid_LH.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber_containment_grid_LH.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber_containment_grid_LH.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = sc_maths._newgetdirup(quat_buffers);
                //dirInstance = dirInstance = _newgetdirup(_testQuater);
                cuber_containment_grid_LH.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber_containment_grid_LH.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber_containment_grid_LH.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber_containment_grid_LH.instancesDataUP[i].rotation.W = 1;
            }
            //END OF

            //PHYSICS SCREENS
            _world_screen_list[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_screens[0][0];
            _world_screen_list[0][0]._POSITION = worldMatrix_base[0];
            //END OF 

            //PHYSICS SCREEN ASSETS
            _world_screen_assets_list[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_screen_assets[0][0];
            _world_screen_assets_list[0][0]._POSITION = worldMatrix_base[0];
            //END OF

            //PHYSICS SCREENS
            cuber = _world_screen_list[0][0];
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

                var dirInstance = sc_maths._newgetdirforward(_testQuater);
                cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataUP[i].rotation.W = 1;
            }
            //END OF

            //PHYSICS SCREEN ASSETS
            cuber = _world_screen_assets_list[0][0];
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

                var dirInstance = sc_maths._newgetdirforward(_testQuater);
                cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataUP[i].rotation.W = 1;
            }

            for (int xx = 0; xx < Program._physics_engine_instance_x; xx++)
            {
                for (int yy = 0; yy < Program._physics_engine_instance_y; yy++)
                {
                    for (int zz = 0; zz < Program._physics_engine_instance_z; zz++)
                    {
                        var indexer00 = xx + Program._physics_engine_instance_x * (yy + Program._physics_engine_instance_y * zz);

                        try
                        {
                            for (int x = 0; x < Program.world_width; x++)
                            {
                                for (int y = 0; y < Program.world_height; y++)
                                {
                                    for (int z = 0; z < Program.world_depth; z++)
                                    {
                                        var indexer01 = x + Program.world_width * (y + Program.world_height * z);

                                        //PHYSICS CUBES
                                        _world_cube_list[indexer00][indexer01]._WORLDMATRIXINSTANCES = worldMatrix_instances_cubes[indexer00][indexer01]; // 
                                        _world_cube_list[indexer00][indexer01]._POSITION = worldMatrix_base[0];

                                        cuber = _world_cube_list[indexer00][indexer01];
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

                                            Quaternion.RotationMatrix(ref sometester[i], out quat_buffers);

                                            var dirInstance = sc_maths._newgetdirforward(quat_buffers);
                                            //var dirInstance = _newgetdirforward(_testQuater);
                                            cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                            cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                            cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                            cuber.instancesDataForward[i].rotation.W = 1;

                                            dirInstance = -sc_maths._newgetdirleft(quat_buffers);
                                            //dirInstance = -_newgetdirleft(_testQuater);
                                            cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                            cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                            cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                            cuber.instancesDataRIGHT[i].rotation.W = 1;

                                            dirInstance = sc_maths._newgetdirup(quat_buffers);
                                            //dirInstance = dirInstance = _newgetdirup(_testQuater);
                                            cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                            cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                            cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                            cuber.instancesDataUP[i].rotation.W = 1;
                                        }
                                        //END OF

                                        //PHYSICS CONES
                                        _world_cone_list[indexer00][indexer01]._WORLDMATRIXINSTANCES = worldMatrix_instances_cone[indexer00][indexer01]; // 
                                        _world_cone_list[indexer00][indexer01]._POSITION = worldMatrix_base[0];
                                        cuber = _world_cone_list[indexer00][indexer01];
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

                                            Quaternion.RotationMatrix(ref sometester[i], out quat_buffers);

                                            var dirInstance = sc_maths._newgetdirforward(quat_buffers);
                                            //var dirInstance = _newgetdirforward(_testQuater);
                                            cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                            cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                            cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                            cuber.instancesDataForward[i].rotation.W = 1;

                                            dirInstance = -sc_maths._newgetdirleft(quat_buffers);
                                            //dirInstance = -_newgetdirleft(_testQuater);
                                            cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                            cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                            cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                            cuber.instancesDataRIGHT[i].rotation.W = 1;

                                            dirInstance = sc_maths._newgetdirup(quat_buffers);
                                            //dirInstance = dirInstance = _newgetdirup(_testQuater);
                                            cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                            cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                            cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                            cuber.instancesDataUP[i].rotation.W = 1;
                                        }
                                        //END OF



                                        //PHYSICS CYLINDERS
                                        _world_cylinder_list[indexer00][indexer01]._WORLDMATRIXINSTANCES = worldMatrix_instances_cylinder[indexer00][indexer01]; // 
                                        _world_cylinder_list[indexer00][indexer01]._POSITION = worldMatrix_base[0];
                                        cuber = _world_cylinder_list[indexer00][indexer01];
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

                                            Quaternion.RotationMatrix(ref sometester[i], out quat_buffers);

                                            var dirInstance = sc_maths._newgetdirforward(quat_buffers);
                                            //var dirInstance = _newgetdirforward(_testQuater);
                                            cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                            cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                            cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                            cuber.instancesDataForward[i].rotation.W = 1;

                                            dirInstance = -sc_maths._newgetdirleft(quat_buffers);
                                            //dirInstance = -_newgetdirleft(_testQuater);
                                            cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                            cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                            cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                            cuber.instancesDataRIGHT[i].rotation.W = 1;

                                            dirInstance = sc_maths._newgetdirup(quat_buffers);
                                            //dirInstance = dirInstance = _newgetdirup(_testQuater);
                                            cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                            cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                            cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                            cuber.instancesDataUP[i].rotation.W = 1;
                                        }
                                        //END OF




                                        //PHYSICS CAPSULES
                                        _world_capsule_list[indexer00][indexer01]._WORLDMATRIXINSTANCES = worldMatrix_instances_capsule[indexer00][indexer01]; // 
                                        _world_capsule_list[indexer00][indexer01]._POSITION = worldMatrix_base[0];
                                        cuber = _world_capsule_list[indexer00][indexer01];
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

                                            Quaternion.RotationMatrix(ref sometester[i], out quat_buffers);

                                            var dirInstance = sc_maths._newgetdirforward(quat_buffers);
                                            //var dirInstance = _newgetdirforward(_testQuater);
                                            cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                            cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                            cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                            cuber.instancesDataForward[i].rotation.W = 1;

                                            dirInstance = -sc_maths._newgetdirleft(quat_buffers);
                                            //dirInstance = -_newgetdirleft(_testQuater);
                                            cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                            cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                            cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                            cuber.instancesDataRIGHT[i].rotation.W = 1;

                                            dirInstance = sc_maths._newgetdirup(quat_buffers);
                                            //dirInstance = dirInstance = _newgetdirup(_testQuater);
                                            cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                            cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                            cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                            cuber.instancesDataUP[i].rotation.W = 1;
                                        }
                                        //END OF




                                        //PHYSICS SPHERES
                                        _world_sphere_list[indexer00][indexer01]._WORLDMATRIXINSTANCES = worldMatrix_instances_sphere[indexer00][indexer01]; // 
                                        _world_sphere_list[indexer00][indexer01]._POSITION = worldMatrix_base[0];
                                        cuber = _world_sphere_list[indexer00][indexer01];
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

                                            Quaternion.RotationMatrix(ref sometester[i], out quat_buffers);

                                            var dirInstance = sc_maths._newgetdirforward(quat_buffers);
                                            //var dirInstance = _newgetdirforward(_testQuater);
                                            cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                                            cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                            cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                            cuber.instancesDataForward[i].rotation.W = 1;

                                            dirInstance = -sc_maths._newgetdirleft(quat_buffers);
                                            //dirInstance = -_newgetdirleft(_testQuater);
                                            cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                            cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                            cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                            cuber.instancesDataRIGHT[i].rotation.W = 1;

                                            dirInstance = sc_maths._newgetdirup(quat_buffers);
                                            //dirInstance = dirInstance = _newgetdirup(_testQuater);
                                            cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                                            cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                            cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                            cuber.instancesDataUP[i].rotation.W = 1;
                                        }
                                        //END OF









                                        //PHYSICS VOXEL SPHEROID
                                        _world_voxel_cube_lists[indexer00][indexer01]._WORLDMATRIXINSTANCES = worldMatrix_instances_voxel_cube[indexer00][indexer01];
                                        _world_voxel_cube_lists[indexer00][indexer01]._POSITION = worldMatrix_base[0];
                                        //END OF

                                        //PHYSICS VOXEL THINGS
                                        var voxel_cube = _world_voxel_cube_lists[indexer00][indexer01];
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
                                            Quaternion.RotationMatrix(ref _voxel_cube_Worldmatrix_of_instances[i], out quat_buffers);

                                            var dirInstance = sc_maths._newgetdirforward(quat_buffers);
                                            voxel_cube.instancesDataForward[i].rotation.X = dirInstance.X;
                                            voxel_cube.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                            voxel_cube.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                            voxel_cube.instancesDataForward[i].rotation.W = 1;

                                            dirInstance = -sc_maths._newgetdirleft(quat_buffers);
                                            voxel_cube.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                            voxel_cube.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                            voxel_cube.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                            voxel_cube.instancesDataRIGHT[i].rotation.W = 1;

                                            dirInstance = sc_maths._newgetdirup(quat_buffers);
                                            voxel_cube.instancesDataUP[i].rotation.X = dirInstance.X;
                                            voxel_cube.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                            voxel_cube.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                            voxel_cube.instancesDataUP[i].rotation.W = 1;
                                        }
                                        //END OF

                                        Vector3 playerPos = new Vector3(_player_torso[0][0]._arrayOfInstances[0].current_pos.M41, _player_torso[0][0]._arrayOfInstances[0].current_pos.M42, _player_torso[0][0]._arrayOfInstances[0].current_pos.M43);






                                        //////////////TO READD
                                        //////////////TO READD
                                        //////////////TO READD

                                        for (int yc = -PlanetChunkHeight_L; yc <= PlanetChunkHeight_R; yc += realplanetwidth)
                                        {
                                            for (int xc = -PlanetChunkWidth_L; xc <= PlanetChunkWidth_R; xc += realplanetwidth)
                                            {
                                                for (int zc = -PlanetChunkDepth_L; zc <= PlanetChunkDepth_R; zc += realplanetwidth)
                                                {
                                                    var xxc = xc;
                                                    var yyc = yc;
                                                    var zzc = zc;

                                                    if (xxc < 0)
                                                    {
                                                        xxc *= -1;
                                                        xxc = (PlanetChunkWidth_R) + xxc;
                                                    }
                                                    if (yyc < 0)
                                                    {
                                                        yyc *= -1;
                                                        yyc = (PlanetChunkHeight_R) + yyc;
                                                    }
                                                    if (zzc < 0)
                                                    {
                                                        zzc *= -1;
                                                        zzc = (PlanetChunkDepth_R) + zzc;
                                                    }

                                                    int _index = xxc + (PlanetChunkWidth_L + PlanetChunkWidth_R + 1) * (yyc + (PlanetChunkHeight_L + PlanetChunkHeight_R + 1) * zzc);


                                                    Vector3 chunkPos = new Vector3(arrayOfPlanetChunk[_index].current_pos.M41, arrayOfPlanetChunk[_index].current_pos.M42, arrayOfPlanetChunk[_index].current_pos.M43);

                                                    //if (Vector3.Distance(chunkPos, playerPos) < 30)
                                                    //var dist = sc_maths.sc_check_distance_node_3d_geometry(chunkPos, playerPos, 100, 100, 100, 100, 100, 100);
                                                    //Console.WriteLine(dist);
                                                    //if (dist < 5000)
                                                    {
                                                        if (arrayOfPlanetChunk[_index] != null)
                                                        {
                                                            if (arrayOfPlanetChunk[_index].Vertices != null)
                                                            {
                                                                if (arrayOfPlanetChunk[_index].Vertices.Length > 0)
                                                                {
                                                                    //float posX = (xc);
                                                                    //float posY = (yc);
                                                                    //float posZ = (zc);

                                                                    //Vector3 planetchunkpos = new Vector3(posX, posY, posZ) + new Vector3(0,5,0);

                                                                    //Matrix[] matArray = new Matrix[1];

                                                                    //matArray[0] = new Matrix();
                                                                    ///matArray[0].M41 = arrayOfPlanetChunk[_index]._ORIGINPOSITION.M41; //planetchunkpos.X;// 
                                                                    //matArray[0].M42 = arrayOfPlanetChunk[_index]._ORIGINPOSITION.M42; //planetchunkpos.Y;//
                                                                    //matArray[0].M43 = arrayOfPlanetChunk[_index]._ORIGINPOSITION.M43; //planetchunkpos.Z;//

                                                                    //PHYSICS VOXEL SPHEROID
                                                                    arrayOfPlanetChunk[_index]._WORLDMATRIXINSTANCES = worldMatrix_instances_voxel_pchunk[indexer00][indexer01][_index]; //matArray;// 
                                                                    arrayOfPlanetChunk[_index]._POSITION = worldMatrix_base[0];
                                                                    //END OF

                                                                    //PHYSICS VOXEL THINGS
                                                                    var voxel_pchunk = arrayOfPlanetChunk[_index];
                                                                    var instances_voxel_pchunk = voxel_pchunk.instances;
                                                                    var _voxel_pchunk_Worldmatrix_of_instancespchunk = voxel_pchunk._WORLDMATRIXINSTANCES;

                                                                    for (int i = 0; i < instances_voxel_pchunk.Length; i++)
                                                                    {
                                                                        float xxx = _voxel_pchunk_Worldmatrix_of_instancespchunk[i].M41;
                                                                        float yyy = _voxel_pchunk_Worldmatrix_of_instancespchunk[i].M42;
                                                                        float zzz = _voxel_pchunk_Worldmatrix_of_instancespchunk[i].M43;

                                                                        voxel_pchunk.instances[i].position.X = xxx;
                                                                        voxel_pchunk.instances[i].position.Y = yyy;
                                                                        voxel_pchunk.instances[i].position.Z = zzz;
                                                                        voxel_pchunk.instances[i].position.W = 1;
                                                                        Quaternion.RotationMatrix(ref _voxel_pchunk_Worldmatrix_of_instancespchunk[i], out quat_buffers);

                                                                        var dirInstance = sc_maths._newgetdirforward(quat_buffers);
                                                                        voxel_pchunk.instancesDataForward[i].rotation.X = dirInstance.X;
                                                                        voxel_pchunk.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                                                        voxel_pchunk.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                                                        voxel_pchunk.instancesDataForward[i].rotation.W = 1;

                                                                        dirInstance = -sc_maths._newgetdirleft(quat_buffers);
                                                                        voxel_pchunk.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                                                        voxel_pchunk.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                                                        voxel_pchunk.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                                                        voxel_pchunk.instancesDataRIGHT[i].rotation.W = 1;

                                                                        dirInstance = sc_maths._newgetdirup(quat_buffers);
                                                                        voxel_pchunk.instancesDataUP[i].rotation.X = dirInstance.X;
                                                                        voxel_pchunk.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                                                        voxel_pchunk.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                                                        voxel_pchunk.instancesDataUP[i].rotation.W = 1;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        //////////////TO READD
                                        //////////////TO READD
                                        //////////////TO READD




                                        /*
                                        for (int pc = 0; pc < arrayOfPlanetChunk.Length; pc++)
                                        {
                                            if (arrayOfPlanetChunk[pc] != null)
                                            {
                                                if (arrayOfPlanetChunk[pc].Vertices != null)
                                                {
                                                    if (arrayOfPlanetChunk[pc].Vertices.Length > 0)
                                                    {
                                                        //float posX = (xc);
                                                        //float posY = (yc);
                                                        //float posZ = (zc);

                                                        //Vector3 planetchunkpos = new Vector3(posX, posY, posZ) + new Vector3(0,5,0);

                                                        //Matrix[] matArray = new Matrix[1];

                                                        //matArray[0] = new Matrix();
                                                        ///matArray[0].M41 = arrayOfPlanetChunk[_index]._ORIGINPOSITION.M41; //planetchunkpos.X;// 
                                                        //matArray[0].M42 = arrayOfPlanetChunk[_index]._ORIGINPOSITION.M42; //planetchunkpos.Y;//
                                                        //matArray[0].M43 = arrayOfPlanetChunk[_index]._ORIGINPOSITION.M43; //planetchunkpos.Z;//

                                                        //PHYSICS VOXEL SPHEROID
                                                        arrayOfPlanetChunk[pc]._WORLDMATRIXINSTANCES = worldMatrix_instances_voxel_pchunk[indexer00][indexer01][pc]; //matArray;// 
                                                        arrayOfPlanetChunk[pc]._POSITION = worldMatrix_base[0];

                                                        //END OF
                                                        //END OF
                                                        //PHYSICS VOXEL THINGS
                                                        var voxel_pchunk = arrayOfPlanetChunk[pc];
                                                        var instances_voxel_pchunk = voxel_pchunk.instances;
                                                        var _voxel_pchunk_Worldmatrix_of_instancespchunk = voxel_pchunk._WORLDMATRIXINSTANCES;

                                                        for (int i = 0; i < instances_voxel_pchunk.Length; i++)
                                                        {
                                                            float xxx = _voxel_pchunk_Worldmatrix_of_instancespchunk[i].M41;
                                                            float yyy = _voxel_pchunk_Worldmatrix_of_instancespchunk[i].M42;
                                                            float zzz = _voxel_pchunk_Worldmatrix_of_instancespchunk[i].M43;

                                                            voxel_pchunk.instances[i].position.X = xxx;
                                                            voxel_pchunk.instances[i].position.Y = yyy;
                                                            voxel_pchunk.instances[i].position.Z = zzz;
                                                            voxel_pchunk.instances[i].position.W = 1;
                                                            Quaternion.RotationMatrix(ref _voxel_pchunk_Worldmatrix_of_instancespchunk[i], out quat_buffers);

                                                            var dirInstance = sc_maths._newgetdirforward(quat_buffers);
                                                            voxel_pchunk.instancesDataForward[i].rotation.X = dirInstance.X;
                                                            voxel_pchunk.instancesDataForward[i].rotation.Y = dirInstance.Y;
                                                            voxel_pchunk.instancesDataForward[i].rotation.Z = dirInstance.Z;
                                                            voxel_pchunk.instancesDataForward[i].rotation.W = 1;

                                                            dirInstance = -sc_maths._newgetdirleft(quat_buffers);
                                                            voxel_pchunk.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                                                            voxel_pchunk.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                                                            voxel_pchunk.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                                                            voxel_pchunk.instancesDataRIGHT[i].rotation.W = 1;

                                                            dirInstance = sc_maths._newgetdirup(quat_buffers);
                                                            voxel_pchunk.instancesDataUP[i].rotation.X = dirInstance.X;
                                                            voxel_pchunk.instancesDataUP[i].rotation.Y = dirInstance.Y;
                                                            voxel_pchunk.instancesDataUP[i].rotation.Z = dirInstance.Z;
                                                            voxel_pchunk.instancesDataUP[i].rotation.W = 1;
                                                        }
                                                    }
                                                }
                                            }
                                            //END OF
                                        }*/
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
            }
            //SC BUFFERS
            //SC BUFFERS
            //SC BUFFERS











            if (_has_locked_screen_pos == 0)
            {
                /*if (had_locked_screen == 0)
                {
                    tempMatrix = _player_lft_hnd[0]._arrayOfInstances[0].current_pos;

                    Vector3 savingPos = new Vector3(tempMatrix.M41, tempMatrix.M42, tempMatrix.M43);

                    Quaternion _testQuator;
                    Quaternion.RotationMatrix(ref tempMatrix, out _testQuator);

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

                    _current_screen_pos = _direction_offsetter * tempMatrix;

                    _current_screen_pos.M41 = savingPos.X;
                    _current_screen_pos.M42 = savingPos.Y;
                    _current_screen_pos.M43 = savingPos.Z;

                    _last_screen_pos = tempMatrix;

                }
                else if (had_locked_screen == 1)
                {
                    _last_screen_pos = _world_screen_list[0]._arrayOfInstances[0].current_pos;
                    had_locked_screen = 0;
                }*/

                _current_screen_pos = _world_screen_list[0][0]._arrayOfInstances[0].current_pos;
                _last_screen_pos = _world_screen_list[0][0]._arrayOfInstances[0].current_pos;// worldMatrix_instances_screens[0][0];

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



                    hmd_matrix_current = hmd_matrix_current * OriginRot * RotatingMatrix * RotatingMatrixForPelvis;
                    Quaternion.RotationMatrix(ref hmd_matrix_current, out _testQuator);

                    xq = _testQuator.X;
                    yq = _testQuator.Y;
                    zq = _testQuator.Z;
                    wq = _testQuator.W;

                    float roller = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));
                    float pitcher = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI));//
                    float yawer = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));

                    pitch = (float)((Math.PI * pitcher + 45) / 180);
                    roll = (float)(0);

                    tempMatrix = SharpDX.Matrix.RotationYawPitchRoll(pitch, 0, roll);

                    tempMatrix.M41 = _last_screen_pos.M41;
                    tempMatrix.M42 = _last_screen_pos.M42;
                    tempMatrix.M43 = _last_screen_pos.M43;

                    _current_screen_pos = tempMatrix; //_direction_offsetter

                    _current_screen_pos.M41 = _last_screen_pos.M41;
                    _current_screen_pos.M42 = _last_screen_pos.M42;
                    _current_screen_pos.M43 = _last_screen_pos.M43;

                    had_locked_screen = 1;
                }
            }













            /*screen_mat = _world_screen_list[0][0]._arrayOfInstances[0].current_pos;//_player_lft_hnd[0]._arrayOfInstances[0].current_pos; 

            
            //Quaternion _quat_screen;
            Quaternion.RotationMatrix(ref screen_mat, out _quat_screen);
             screenNormal = sc_maths._getDirection(Vector3.ForwardRH, _quat_screen);
            screenNormal.Normalize();
             planer = new Plane(new Vector3(screen_mat.M41, screen_mat.M42, screen_mat.M43), screenNormal);

            Vector3 screenPos = new Vector3(_world_screen_list[0][0]._arrayOfInstances[0].current_pos.M41, _world_screen_list[0][0]._arrayOfInstances[0].current_pos.M42, _world_screen_list[0][0]._arrayOfInstances[0].current_pos.M43);
            float distancer;
            Vector3.Distance(ref screenPos, ref SC_Update.OFFSETPOS, out distancer);
            screen_mat = _world_screen_list[0][0]._arrayOfInstances[0].current_pos;*/

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

            //OCULUS TOUCH RIGHT
            /*if (_out_of_bounds_right == 1)// || distancer >= 4)
            {
                Vector3 tester00 = new Vector3(_screenDirMatrix_correct_pos[0][0][2].M41, _screenDirMatrix_correct_pos[0][0][2].M42, _screenDirMatrix_correct_pos[0][0][2].M43);
                _intersectTouchRightMatrix = screen_mat;

                _intersectTouchRightMatrix.M41 = tester00.X;
                _intersectTouchRightMatrix.M42 = tester00.Y;
                _intersectTouchRightMatrix.M43 = tester00.Z;
                worldMatrix_instances_screen_assets[0][0][5] = _intersectTouchRightMatrix;

            }
            else
            {
                Vector3 tester00 = new Vector3(intersectPointRight.X, intersectPointRight.Y, intersectPointRight.Z);
                _intersectTouchRightMatrix = screen_mat;

                _intersectTouchRightMatrix.M41 = tester00.X;
                _intersectTouchRightMatrix.M42 = tester00.Y;
                _intersectTouchRightMatrix.M43 = tester00.Z;
                worldMatrix_instances_screen_assets[0][0][5] = _intersectTouchRightMatrix;
            }*/


            /*Vector3 tester01 = new Vector3(intersectPointRight.X, intersectPointRight.Y, intersectPointRight.Z);
            var newmatrix = screen_mat;
            newmatrix.M41 = tester01.X;
            newmatrix.M42 = tester01.Y;
            newmatrix.M43 = tester01.Z;*/










            //NORMAL SCREEN CALCULATION
            tempMatrix = worldMatrix_instances_screens[0][0][0];
            Quaternion.RotationMatrix(ref tempMatrix, out _testQuater);
            var screenNormalRight = sc_maths._getDirection(Vector3.Right, _testQuater);
            screenNormalRight.Normalize();
            var screenNormalUp = sc_maths._getDirection(Vector3.Up, _testQuater);
            screenNormalUp.Normalize();









            var tempMatRight = _intersectTouchRightMatrix;


            worldMatrix_instances_screen_assets[0][0][5] = _intersectTouchRightMatrix;
            worldMatrix_instances_screen_assets[0][0][6] = _intersectTouchLeftMatrix;
            worldMatrix_instances_screen_assets[0][0][7] = _oculusR_Cursor_matrix;





            centerPosRight = new Vector3(final_hand_pos_right_locked.M41, final_hand_pos_right_locked.M42, final_hand_pos_right_locked.M43);
            Quaternion.RotationMatrix(ref final_hand_pos_right_locked, out _rightTouchQuat);
            rayDirRighter = sc_maths._getDirection(Vector3.ForwardRH, _rightTouchQuat);
            someRay = new Ray(centerPosRight, rayDirRighter);
            intersecter = someRay.Intersects(ref planer, out intersectPointRight);



            centerPosLeft = new Vector3(final_hand_pos_left_locked.M41, final_hand_pos_left_locked.M42, final_hand_pos_left_locked.M43);
            Quaternion.RotationMatrix(ref final_hand_pos_left_locked, out _leftTouchQuat);
            rayDirLeft = sc_maths._getDirection(Vector3.ForwardRH, _leftTouchQuat);
            someRayLeft = new Ray(centerPosLeft, rayDirLeft);
            intersecterLeft = someRayLeft.Intersects(ref planer, out intersectPointLeft);






















            Vector3 currentScreenPos = new Vector3(tempMatrix.M41, tempMatrix.M42, tempMatrix.M43); // new Vector3(xii, yii, zii);// new Vector3(_finalRotMatrixScreen.M41, _finalRotMatrixScreen.M42, _finalRotMatrixScreen.M43);
            Vector3 newDirRight = (screenNormalRight) * sizeWidtherer; // + screenNormalTop
            currentScreenPos -= newDirRight;
            Vector3 newDirUp = (screenNormalUp) * sizeheighterer; // + screenNormalTop
            currentScreenPos -= newDirUp;
            Matrix resulter0 = tempMatrix;
            resulter0.M41 = currentScreenPos.X;
            resulter0.M42 = currentScreenPos.Y;
            resulter0.M43 = currentScreenPos.Z;
            _screenDirMatrix_correct_pos[0][0][0] = resulter0;

            currentScreenPos = new Vector3(tempMatrix.M41, tempMatrix.M42, tempMatrix.M43);
            newDirRight = (screenNormalRight) * sizeWidtherer;
            currentScreenPos -= newDirRight;
            newDirUp = (screenNormalUp) * sizeheighterer;
            currentScreenPos += newDirUp;
            resulter0 = tempMatrix;
            resulter0.M41 = currentScreenPos.X;
            resulter0.M42 = currentScreenPos.Y;
            resulter0.M43 = currentScreenPos.Z;
            _screenDirMatrix_correct_pos[0][0][1] = resulter0;

            currentScreenPos = new Vector3(tempMatrix.M41, tempMatrix.M42, tempMatrix.M43);
            newDirRight = (screenNormalRight) * sizeWidtherer;
            currentScreenPos += newDirRight;
            newDirUp = (screenNormalUp) * sizeheighterer;
            currentScreenPos -= newDirUp;
            resulter0 = tempMatrix;
            resulter0.M41 = currentScreenPos.X;
            resulter0.M42 = currentScreenPos.Y;
            resulter0.M43 = currentScreenPos.Z;
            _screenDirMatrix_correct_pos[0][0][2] = resulter0;

            currentScreenPos = new Vector3(tempMatrix.M41, tempMatrix.M42, tempMatrix.M43);
            newDirRight = (screenNormalRight) * sizeWidtherer;
            currentScreenPos += newDirRight;
            newDirUp = (screenNormalUp) * sizeheighterer;
            currentScreenPos += newDirUp;
            resulter0 = tempMatrix;
            resulter0.M41 = currentScreenPos.X;
            resulter0.M42 = currentScreenPos.Y;
            resulter0.M43 = currentScreenPos.Z;
            _screenDirMatrix_correct_pos[0][0][3] = resulter0;

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
                worldMatrix_instances_screen_assets[0][0][i] = _screenDirMatrix_correct_pos[0][0][i];
            }

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

            _intersectTouchRightMatrix = _world_screen_list[0][0]._arrayOfInstances[0].current_pos;
            _intersectTouchRightMatrix.M41 = stabilizedIntersectionPosRight.X;
            _intersectTouchRightMatrix.M42 = stabilizedIntersectionPosRight.Y;
            _intersectTouchRightMatrix.M43 = stabilizedIntersectionPosRight.Z;

            var vert0 = new Vector3(_screenDirMatrix_correct_pos[0][0][0].M41, _screenDirMatrix_correct_pos[0][0][0].M42, _screenDirMatrix_correct_pos[0][0][0].M43);
            var vert1 = new Vector3(_screenDirMatrix_correct_pos[0][0][1].M41, _screenDirMatrix_correct_pos[0][0][1].M42, _screenDirMatrix_correct_pos[0][0][1].M43);
            var vert2 = new Vector3(_screenDirMatrix_correct_pos[0][0][2].M41, _screenDirMatrix_correct_pos[0][0][2].M42, _screenDirMatrix_correct_pos[0][0][2].M43);
            var vert3 = new Vector3(_screenDirMatrix_correct_pos[0][0][3].M41, _screenDirMatrix_correct_pos[0][0][3].M42, _screenDirMatrix_correct_pos[0][0][3].M43);

            //var pointOnScreen = new Vector3(_intersectTouchRightMatrix.M41, _intersectTouchRightMatrix.M42, _intersectTouchRightMatrix.M43);
            var pointOnScreen = new Vector3(intersectPointRight.X, intersectPointRight.Y, intersectPointRight.Z);

            var d = (vert2 - vert0).Length();
            widthLength = (vert2 - vert0).Length();
            heightLength = (vert1 - vert0).Length();
            r = (pointOnScreen - vert0).Length();
            var R = (pointOnScreen - vert2).Length();
            var xcirccirc = ((d * d) - (r * r) + (R * R)) / (2 * d);
            var d1 = xcirccirc;
            var d2 = d - xcirccirc;

            //r is with d2
            //R is with d1
            //a2 + b2 = c2

            b = (float)Math.Sqrt((r * r) - (d2 * d2));
            currentPosWidth = widthLength - d1; // 
            currentPosHeight = heightLength - b;
            percentXRight = currentPosWidth / widthLength;
            percentYRight = currentPosHeight / heightLength;
            percentXRight *= SC_console_directx.D3D.SurfaceWidth;
            percentYRight *= SC_console_directx.D3D.SurfaceHeight;






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

            _intersectTouchLeftMatrix = _world_screen_list[0][0]._arrayOfInstances[0].current_pos;
            _intersectTouchLeftMatrix.M41 = stabilizedIntersectionPosLeft.X;
            _intersectTouchLeftMatrix.M42 = stabilizedIntersectionPosLeft.Y;
            _intersectTouchLeftMatrix.M43 = stabilizedIntersectionPosLeft.Z;

            pointOnScreen = new Vector3(intersectPointLeft.X, intersectPointLeft.Y, intersectPointLeft.Z);

            d = (vert2 - vert0).Length();
            widthLength = (vert2 - vert0).Length();
            heightLength = (vert1 - vert0).Length();
            r = (pointOnScreen - vert0).Length();
            R = (pointOnScreen - vert2).Length();
            xcirccirc = ((d * d) - (r * r) + (R * R)) / (2 * d);
            d1 = xcirccirc;
            d2 = d - xcirccirc;

            //r is with d2
            //R is with d1
            //a2 + b2 = c2

            b = (float)Math.Sqrt((r * r) - (d2 * d2));
            currentPosWidth = widthLength - d1; // 
            currentPosHeight = heightLength - b;
            percentXLeft = currentPosWidth / widthLength;
            percentYLeft = currentPosHeight / heightLength;
            percentXLeft *= SC_console_directx.D3D.SurfaceWidth;
            percentYLeft *= SC_console_directx.D3D.SurfaceHeight;

















            //HMD POINTER
            somematroxer2 = _world_screen_list[0][0]._arrayOfInstances[0].current_pos;
            var screenNormaler = sc_maths._getDirection(Vector3.ForwardRH, _testQuater);
            var planor = new Plane(new Vector3(_world_screen_list[0][0]._arrayOfInstances[0].current_pos.M41, _world_screen_list[0][0]._arrayOfInstances[0].current_pos.M42, _world_screen_list[0][0]._arrayOfInstances[0].current_pos.M43), screenNormaler);
            var centerPosOculusRift = new Vector3(_hmdPoser.X, _hmdPoser.Y, _hmdPoser.Z) + SC_Update.OFFSETPOS;
            Matrix oculusRifter;
            Matrix.RotationQuaternion(ref _hmdRoter, out oculusRifter);
            oculusRifter = oculusRifter * OriginRot * RotatingMatrix * RotatingMatrixForPelvis;
            Quaternion some_oculus_quat;
            Quaternion.RotationMatrix(ref oculusRifter, out some_oculus_quat);
            var rayDirRighterer = sc_maths._getDirection(Vector3.ForwardRH, some_oculus_quat);
            rayDirRighterer.Normalize();
            var someRayer = new Ray(centerPosOculusRift, rayDirRighterer);
            Vector3 intersectPointHMD;
            var intersecterHMD = someRayer.Intersects(ref planor, out intersectPointHMD);
            somematroxer2.M41 = intersectPointHMD.X;
            somematroxer2.M42 = intersectPointHMD.Y;
            somematroxer2.M43 = intersectPointHMD.Z;
            //CIRCLE CIRCLE INTERSECTION //http://mathworld.wolfram.com/Circle-CircleIntersection.html
            pointOnScreen = new Vector3(intersectPointHMD.X, intersectPointHMD.Y, intersectPointHMD.Z);
            d = (vert2 - vert0).Length();
            widthLength = (vert2 - vert0).Length();
            heightLength = (vert1 - vert0).Length();
            r = (pointOnScreen - vert0).Length();
            R = (pointOnScreen - vert2).Length();
            xcirccirc = ((d * d) - (r * r) + (R * R)) / (2 * d);
            d1 = xcirccirc;
            d2 = d - xcirccirc;
            //r is with d2
            //R is with d1
            //a2 + b2 = c2
            b = (float)Math.Sqrt((r * r) - (d2 * d2));
            currentPosWidth = widthLength - d1; // 
            currentPosHeight = heightLength - b;
            var percentXRift = currentPosWidth / widthLength;
            var percentYRift = currentPosHeight / heightLength;
            percentXRift *= SC_console_directx.D3D.SurfaceWidth;
            percentYRift *= SC_console_directx.D3D.SurfaceHeight;
            var realOculusRiftCursorPosX = percentXRift;
            var realOculusRiftCursorPosY = percentYRift;
            if (realOculusRiftCursorPosX >= 0 && realOculusRiftCursorPosX <= SC_console_directx.D3D.SurfaceWidth && realOculusRiftCursorPosY >= 0 && realOculusRiftCursorPosY <= SC_console_directx.D3D.SurfaceHeight)
            {
                _oculusR_Cursor_matrix = somematroxer2;
                _out_of_bounds_oculus_rift = 0;
            }
            else
            {
                _out_of_bounds_oculus_rift = 1;
            }

            if (percentXRight >= 0 && percentXRight < SC_console_directx.D3D.SurfaceWidth && percentYRight >= 0 && percentYRight < SC_console_directx.D3D.SurfaceHeight)
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
            percentXLeft *= SC_console_directx.D3D.SurfaceWidth;
            percentYLeft *= SC_console_directx.D3D.SurfaceHeight;







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
            percentXRight *= SC_console_directx.D3D.SurfaceWidth;
            percentYRight *= SC_console_directx.D3D.SurfaceHeight;

            realMousePosX = percentXRight;
            realMousePosY = percentYRight;*/













            /*
            //Console.WriteLine("x: " + _final_percentXRight + " y: " + _final_percentYRight);

            _MicrosoftWindowsMouseRight(_final_percentXRight, _final_percentYRight, thumbStickRight, percentXLeft, percentYLeft, thumbStickLeft, realMousePosX, realMousePosY);

            _oculus_touch_controls(_final_percentXRight, _final_percentYRight, thumbStickRight, percentXLeft, percentYLeft, thumbStickLeft, realMousePosX, realMousePosY);

            //var absoluteMoveX = Convert.ToUInt32((percentXRight * 65535) / SC_console_directx.D3D.SurfaceWidth);
            //var absoluteMoveY = Convert.ToUInt32((percentYRight * 65535) / SC_console_directx.D3D.SurfaceHeight);

            _mouseCursorMatrix.M41 = (float)((percentXRight * 65535) / SC_console_directx.D3D.SurfaceWidth);
            _mouseCursorMatrix.M42 = (float)((percentYRight * 65535) / SC_console_directx.D3D.SurfaceHeight);

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
            lastHasUsedHandTriggerLeft = hasUsedHandTriggerLeft;
            lastbuttonPressedOculusTouchRight = buttonPressedOculusTouchRight;
            lastbuttonPressedOculusTouchLeft = buttonPressedOculusTouchLeft;

            final_hand_pos_right_locked = _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos;
            final_hand_pos_left_locked = _player_lft_hnd[0][0]._arrayOfInstances[0].current_pos;
            _last_final_hand_pos_right = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);
            _last_frame_handPos = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);
            //DISCARDED TO REINSERT
            //DISCARDED TO REINSERT
            //DISCARDED TO REINSERT

            return _sc_jitter_tasks;
        }

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

        private void _oculus_touch_controls(double percentXRight, double percentYRight, Vector2f[] thumbStickRight, double percentXLeft, double percentYLeft, Vector2f[] thumbStickLeft, double realMousePosX, double realMousePosY) //
        {
            if (Program.useArduinoOVRTouchKeymapper == 1)
            {
                var homebuttonLeftTouchControllerArduino = SC_Update.arduinoDIYOculusTouchArrayOfData[5];

                //Console.WriteLine("homebuttonLeftTouchControllerArduino:" + homebuttonLeftTouchControllerArduino);
                if (homebuttonLeftTouchControllerArduino == 1)//buttonPressedOculusTouchLeft == 1048576)
                {
                    if (hasClickedHomeButtonTouchLeft == 0)
                    {
                        SC_console_directx.D3D.OVR.RecenterTrackingOrigin(SC_console_directx.D3D.sessionPtr);

                        //hmdrotMatrix

                        Quaternion currentRot;// = hmd_matrix_current;
                        Quaternion.RotationMatrix(ref hmd_matrix_current, out currentRot);

                        hmd_matrix_current = hmd_matrix_current * OriginRot * RotatingMatrix * RotatingMatrixForPelvis * hmdmatrixRot_; //viewMatrix_;


                        //Quaternion currentRotAfter;
                        //Quaternion.RotationMatrix(ref hmd_matrix_current, out currentRotAfter);
                        //Quaternion.Lerp(ref currentRot, ref currentRotAfter, 0.001f, out currentRotAfter);
                        //Matrix.RotationQuaternion(ref currentRotAfter,out hmd_matrix_current);

                        //var timeSinceStart = (float)(DateTime.Now - SC_Update.startTime).TotalSeconds;
                        //Matrix worldmatlightrot = Matrix.Scaling(1.0f) * Matrix.RotationX(timeSinceStart * disco_sphere_rot_speed) * Matrix.RotationY(timeSinceStart * 2 * disco_sphere_rot_speed) * Matrix.RotationZ(timeSinceStart * 3 * disco_sphere_rot_speed);



                        Quaternion _testQuator;
                        Quaternion.RotationMatrix(ref hmd_matrix_current, out _testQuator);

                        var xq = _testQuator.X;
                        var yq = _testQuator.Y;
                        var zq = _testQuator.Z;
                        var wq = _testQuator.W;

                        float roller = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));
                        float pitcher = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));//
                        float yawer = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));

                        //RotationY4Pelvis
                        //Matrix tempMat = RotatingMatrixForPelvis * hmdmatrixRot_;
                        Quaternion.RotationMatrix(ref hmd_matrix_current, out _testQuator);

                        xq = _testQuator.X;
                        yq = _testQuator.Y;
                        zq = _testQuator.Z;
                        wq = _testQuator.W;

                        float rollerPelvis = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));
                        float pitcherPelvis = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));//
                        float yawerPelvis = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));

                        //var pitch = (float)((Math.PI * pitcher + 45) / 180);
                        //var roll = (float)(0);
                        //var yaw = (float)(0);

                        SC_Update.RotationX4Pelvis = pitcher;
                        SC_Update.RotationY4Pelvis = 0;
                        SC_Update.RotationZ4Pelvis = 0;

                        SC_Update.rotatingMatrixForPelvis = SharpDX.Matrix.RotationYawPitchRoll(pitcher, 0, 0);
                        SC_Update.hmdmatrixRot = SharpDX.Matrix.RotationYawPitchRoll(pitcher, 0, 0);

                        hasClickedHomeButtonTouchLeft = 1;
                    }
                }


                if (hasClickedHomeButtonTouchLeft == 1)
                {
                    if (hasClickedHomeButtonTouchLeftCounter > 20)
                    {
                        hasClickedHomeButtonTouchLeft = 0;
                        hasClickedHomeButtonTouchLeftCounter = 0;
                    }
                    hasClickedHomeButtonTouchLeftCounter++;
                }



                var XLeftTouchControllerArduino = SC_Update.arduinoDIYOculusTouchArrayOfData[4];

                //Console.WriteLine("XLeftTouchControllerArduino:" + XLeftTouchControllerArduino);
                if (XLeftTouchControllerArduino == 1)
                {
                    if (sc_menu_scroller_counter >= 75)
                    {
                        if (sc_menu_scroller == 0)
                        {
                            sc_menu_scroller = 1;
                        }
                        else if (sc_menu_scroller == 1)
                        {
                            sc_menu_scroller = 2;
                        }
                        else if (sc_menu_scroller == 2)
                        {
                            sc_menu_scroller = 0;
                        }
                        sc_menu_scroller_counter = 0;
                    }
                }
                sc_menu_scroller_counter++;



                var YLeftTouchControllerArduino = SC_Update.arduinoDIYOculusTouchArrayOfData[3];


                if (_has_locked_screen_pos_counter >= 50)
                {
                    if (YLeftTouchControllerArduino == 1)
                    {
                        if (sc_menu_scroller == 0)
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
                        }
                        else if (sc_menu_scroller == 1)
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
                        }
                    }
                }
                frame_counter_4_buttonY++;
                _has_locked_screen_pos_counter++;






                if (sc_menu_scroller == 2)
                {
                    /////////////LEFT OCULUS TOUCH/////////////////////////////////////////////////////////////////////////////////////
                    if (percentXLeft >= 0 && percentXLeft <= 1920 && percentYLeft >= 0 && percentYLeft <= 1080)
                    {
                        var absoluteMoveX = Convert.ToUInt32((percentXLeft * (65535 - 1)) / SC_console_directx.D3D.SurfaceWidth);
                        var absoluteMoveY = Convert.ToUInt32((percentYLeft * (65535 - 1)) / SC_console_directx.D3D.SurfaceHeight);

                        if (percentXLeft >= 0 && percentXLeft < SC_console_directx.D3D.SurfaceWidth)
                        {

                        }
                        else
                        {
                            percentXLeft = SC_console_directx.D3D.SurfaceWidth;
                            absoluteMoveX = Convert.ToUInt32((percentXLeft * (65535 - 1)) / SC_console_directx.D3D.SurfaceWidth);
                        }

                        if (percentYLeft >= 0 && percentYLeft < SC_console_directx.D3D.SurfaceHeight)
                        {

                        }
                        else
                        {
                            percentYLeft = SC_console_directx.D3D.SurfaceHeight;
                            absoluteMoveY = Convert.ToUInt32((percentYLeft * (65535 - 1)) / SC_console_directx.D3D.SurfaceHeight);
                        }


                        //MOUSE DOUBLE CLICK LOGIC. IF the PLAYER clicked at one location then it stores the location, and if the player re-clicks inside of 20 frames, then click at the first click location.
                        //It's very basic, and at least I should implement also a certain "radius" of distance from the first click and the second click... If the second click is too far from the first click,
                        //then disregard the first click location.
                        if (_frameCounterTouchLeft >= 50)
                        {

                            if (YLeftTouchControllerArduino == 1)
                            {
                                if (hasClickedBUTTONX == 0)
                                {
                                    absoluteMoveX = Convert.ToUInt32((percentXLeft * 65535 - 1) / SC_console_directx.D3D.SurfaceWidth);
                                    absoluteMoveY = Convert.ToUInt32((percentYLeft * 65535 - 1) / SC_console_directx.D3D.SurfaceHeight);

                                    //if (_out_of_bounds_left == 0)
                                    //{
                                    //  
                                    //}

                                    SetCursorPos((int)percentXLeft, (int)percentYLeft);
                                    //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, _lastMousePosXRight, _lastMousePosYRight, 0, 0);
                                    _frameCounterTouchLeft = 0;

                                    //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, absoluteMoveX, absoluteMoveY, 0, 0);
                                    //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, absoluteMoveX, absoluteMoveY, 0, 0);


                                    Program.mousesim.LeftButtonDown();
                                    //Program.mousesim.MoveMouseToPositionOnVirtualDesktop();
                                    //Program.mousesim.LeftButtonClick();

                                    //_lastMousePosXLeft = absoluteMoveX;
                                    //_lastMousePosYLeft = absoluteMoveY;
                                    _canResetCounterTouchLeftButtonX = true;
                                    hasClickedBUTTONX = 1;
                                }
                            }
                            /*else if (YLeftTouchControllerArduino == 512)
                            {
                                if (hasClickedBUTTONY == 0)
                                {
                                    if (_out_of_bounds_right == 0)
                                    {
                                        absoluteMoveX = Convert.ToUInt32((realMousePosX * 65535 - 1) / SC_console_directx.D3D.SurfaceWidth);
                                        absoluteMoveY = Convert.ToUInt32((realMousePosY * 65535 - 1) / SC_console_directx.D3D.SurfaceHeight);
                                    }
                                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                    //_lastMousePosX = absoluteMoveX;
                                    //_lastMousePosY = absoluteMoveY;
                                    //_canResetCounterTouchRight = true;
                                    hasClickedBUTTONY = 1;
                                }
                            }*/


                        }
                        _out_of_bounds_left = 0;
                    }
                    else
                    {
                        _out_of_bounds_left = 1;
                    }
                    if (hasClickedBUTTONX == 1)
                    {
                        if (_frameCounterTouchLeft >= 10)
                        {
                            hasClickedBUTTONX = 0;
                        }
                    }
                }
                _frameCounterTouchLeft++;

                //TO READD WHEN I ORDER THE ARDUINO TRIGGERS OR NOT
                //TO READD WHEN I ORDER THE ARDUINO TRIGGERS OR NOT
                /*
                if (indexTriggerRight[1] > 0.0001f)
                {
                    if (heightmapscale > heightmapscaleMax)
                    {
                        heightmapscale = heightmapscaleMax;
                    }
                    else
                    {
                        heightmapscale += 0.00001f;
                    }
                }

                if (indexTriggerLeft[0] > 0.0001f)
                {
                    if (heightmapscale < heightmapscaleMin)
                    {
                        heightmapscale = heightmapscaleMin;
                    }
                    else
                    {
                        heightmapscale -= 0.00001f;
                    }
                }
                //if (Math.Abs(Math.Abs(indexTriggerRightLastAbs) - Math.Abs(indexTriggerRight[1])) > 0.0001f)
                //if (Math.Abs(Math.Abs(indexTriggerLeftLastAbs) - Math.Abs(indexTriggerLeft[0])) > 0.0001f)
                indexTriggerRightLastAbs = indexTriggerRight[1];
                indexTriggerLeftLastAbs = indexTriggerLeft[0];*/
                //TO READD WHEN I ORDER THE ARDUINO TRIGGERS OR NOT
                //TO READD WHEN I ORDER THE ARDUINO TRIGGERS OR NOT




            }
            else if (Program.useArduinoOVRTouchKeymapper == 0)
            {


                if (buttonPressedOculusTouchLeft == 1048576)
                {
                    if (hasClickedHomeButtonTouchLeft == 0)
                    {
                        SC_console_directx.D3D.OVR.RecenterTrackingOrigin(SC_console_directx.D3D.sessionPtr);

                        //hmdrotMatrix

                        Quaternion currentRot;// = hmd_matrix_current;
                        Quaternion.RotationMatrix(ref hmd_matrix_current, out currentRot);

                        hmd_matrix_current = hmd_matrix_current * OriginRot * RotatingMatrix * RotatingMatrixForPelvis * hmdmatrixRot_; //viewMatrix_;


                        //Quaternion currentRotAfter;
                        //Quaternion.RotationMatrix(ref hmd_matrix_current, out currentRotAfter);
                        //Quaternion.Lerp(ref currentRot, ref currentRotAfter, 0.001f, out currentRotAfter);
                        //Matrix.RotationQuaternion(ref currentRotAfter,out hmd_matrix_current);

                        //var timeSinceStart = (float)(DateTime.Now - SC_Update.startTime).TotalSeconds;
                        //Matrix worldmatlightrot = Matrix.Scaling(1.0f) * Matrix.RotationX(timeSinceStart * disco_sphere_rot_speed) * Matrix.RotationY(timeSinceStart * 2 * disco_sphere_rot_speed) * Matrix.RotationZ(timeSinceStart * 3 * disco_sphere_rot_speed);



                        Quaternion _testQuator;
                        Quaternion.RotationMatrix(ref hmd_matrix_current, out _testQuator);

                        var xq = _testQuator.X;
                        var yq = _testQuator.Y;
                        var zq = _testQuator.Z;
                        var wq = _testQuator.W;

                        float roller = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));
                        float pitcher = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));//
                        float yawer = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));

                        //RotationY4Pelvis
                        //Matrix tempMat = RotatingMatrixForPelvis * hmdmatrixRot_;
                        Quaternion.RotationMatrix(ref hmd_matrix_current, out _testQuator);

                        xq = _testQuator.X;
                        yq = _testQuator.Y;
                        zq = _testQuator.Z;
                        wq = _testQuator.W;

                        float rollerPelvis = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));
                        float pitcherPelvis = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));//
                        float yawerPelvis = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180 / Math.PI));

                        //var pitch = (float)((Math.PI * pitcher + 45) / 180);
                        //var roll = (float)(0);
                        //var yaw = (float)(0);

                        SC_Update.RotationX4Pelvis = pitcher;
                        SC_Update.RotationY4Pelvis = 0;
                        SC_Update.RotationZ4Pelvis = 0;

                        SC_Update.rotatingMatrixForPelvis = SharpDX.Matrix.RotationYawPitchRoll(pitcher, 0, 0);
                        SC_Update.hmdmatrixRot = SharpDX.Matrix.RotationYawPitchRoll(pitcher, 0, 0);

                        hasClickedHomeButtonTouchLeft = 1;
                    }
                }

                if (hasClickedHomeButtonTouchLeft == 1)
                {
                    if (hasClickedHomeButtonTouchLeftCounter > 20)
                    {
                        hasClickedHomeButtonTouchLeft = 0;
                        hasClickedHomeButtonTouchLeftCounter = 0;
                    }
                    hasClickedHomeButtonTouchLeftCounter++;
                }

                if (buttonPressedOculusTouchLeft != 0)
                {
                    if (buttonPressedOculusTouchLeft == 512)
                    {
                        if (sc_menu_scroller_counter >= 75)
                        {
                            if (sc_menu_scroller == 0)
                            {
                                sc_menu_scroller = 1;
                            }
                            else if (sc_menu_scroller == 1)
                            {
                                sc_menu_scroller = 2;
                            }
                            else if (sc_menu_scroller == 2)
                            {
                                sc_menu_scroller = 0;
                            }
                            sc_menu_scroller_counter = 0;
                        }
                    }
                }
                sc_menu_scroller_counter++;

                if (_has_locked_screen_pos_counter >= 50)
                {
                    if (buttonPressedOculusTouchLeft == 256)
                    {
                        if (sc_menu_scroller == 0)
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
                        }
                        else if (sc_menu_scroller == 1)
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
                        }
                    }
                }
                frame_counter_4_buttonY++;
                _has_locked_screen_pos_counter++;






                if (sc_menu_scroller == 2)
                {
                    /////////////LEFT OCULUS TOUCH/////////////////////////////////////////////////////////////////////////////////////
                    if (percentXLeft >= 0 && percentXLeft <= 1920 && percentYLeft >= 0 && percentYLeft <= 1080)
                    {
                        var absoluteMoveX = Convert.ToUInt32((percentXLeft * (65535 - 1)) / SC_console_directx.D3D.SurfaceWidth);
                        var absoluteMoveY = Convert.ToUInt32((percentYLeft * (65535 - 1)) / SC_console_directx.D3D.SurfaceHeight);

                        if (percentXLeft >= 0 && percentXLeft < SC_console_directx.D3D.SurfaceWidth)
                        {

                        }
                        else
                        {
                            percentXLeft = SC_console_directx.D3D.SurfaceWidth;
                            absoluteMoveX = Convert.ToUInt32((percentXLeft * (65535 - 1)) / SC_console_directx.D3D.SurfaceWidth);
                        }

                        if (percentYLeft >= 0 && percentYLeft < SC_console_directx.D3D.SurfaceHeight)
                        {

                        }
                        else
                        {
                            percentYLeft = SC_console_directx.D3D.SurfaceHeight;
                            absoluteMoveY = Convert.ToUInt32((percentYLeft * (65535 - 1)) / SC_console_directx.D3D.SurfaceHeight);
                        }


                        //MOUSE DOUBLE CLICK LOGIC. IF the PLAYER clicked at one location then it stores the location, and if the player re-clicks inside of 20 frames, then click at the first click location.
                        //It's very basic, and at least I should implement also a certain "radius" of distance from the first click and the second click... If the second click is too far from the first click,
                        //then disregard the first click location.
                        if (_frameCounterTouchLeft >= 50)
                        {
                            if (buttonPressedOculusTouchLeft != 0)
                            {
                                if (buttonPressedOculusTouchLeft == 256)
                                {
                                    if (hasClickedBUTTONX == 0)
                                    {
                                        absoluteMoveX = Convert.ToUInt32((percentXLeft * 65535 - 1) / SC_console_directx.D3D.SurfaceWidth);
                                        absoluteMoveY = Convert.ToUInt32((percentYLeft * 65535 - 1) / SC_console_directx.D3D.SurfaceHeight);

                                        //if (_out_of_bounds_left == 0)
                                        //{
                                        //  
                                        //}

                                        SetCursorPos((int)percentXLeft, (int)percentYLeft);
                                        //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, _lastMousePosXRight, _lastMousePosYRight, 0, 0);
                                        _frameCounterTouchLeft = 0;

                                        //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, absoluteMoveX, absoluteMoveY, 0, 0);
                                        //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, absoluteMoveX, absoluteMoveY, 0, 0);


                                        Program.mousesim.LeftButtonDown();
                                        //Program.mousesim.MoveMouseToPositionOnVirtualDesktop();
                                        //Program.mousesim.LeftButtonClick();

                                        //_lastMousePosXLeft = absoluteMoveX;
                                        //_lastMousePosYLeft = absoluteMoveY;
                                        _canResetCounterTouchLeftButtonX = true;
                                        hasClickedBUTTONX = 1;
                                    }
                                }
                                /*else if (buttonPressedOculusTouchLeft == 512)
                                {
                                    if (hasClickedBUTTONY == 0)
                                    {
                                        if (_out_of_bounds_right == 0)
                                        {
                                            absoluteMoveX = Convert.ToUInt32((realMousePosX * 65535 - 1) / SC_console_directx.D3D.SurfaceWidth);
                                            absoluteMoveY = Convert.ToUInt32((realMousePosY * 65535 - 1) / SC_console_directx.D3D.SurfaceHeight);
                                        }
                                        mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                        //_lastMousePosX = absoluteMoveX;
                                        //_lastMousePosY = absoluteMoveY;
                                        //_canResetCounterTouchRight = true;
                                        hasClickedBUTTONY = 1;
                                    }
                                }*/

                            }
                        }
                        _out_of_bounds_left = 0;
                    }
                    else
                    {
                        _out_of_bounds_left = 1;
                    }
                    if (hasClickedBUTTONX == 1)
                    {
                        if (_frameCounterTouchLeft >= 10)
                        {
                            hasClickedBUTTONX = 0;
                        }
                    }
                }
                _frameCounterTouchLeft++;




                if (indexTriggerRight[1] > 0.0001f)
                {
                    if (heightmapscale > heightmapscaleMax)
                    {
                        heightmapscale = heightmapscaleMax;
                    }
                    else
                    {
                        heightmapscale += 0.00001f;
                    }
                }

                if (indexTriggerLeft[0] > 0.0001f)
                {
                    if (heightmapscale < heightmapscaleMin)
                    {
                        heightmapscale = heightmapscaleMin;
                    }
                    else
                    {
                        heightmapscale -= 0.00001f;
                    }
                }
                //if (Math.Abs(Math.Abs(indexTriggerRightLastAbs) - Math.Abs(indexTriggerRight[1])) > 0.0001f)
                //if (Math.Abs(Math.Abs(indexTriggerLeftLastAbs) - Math.Abs(indexTriggerLeft[0])) > 0.0001f)
                indexTriggerRightLastAbs = indexTriggerRight[1];
                indexTriggerLeftLastAbs = indexTriggerLeft[0];





                if (buttonPressedOculusTouchLeft != 0)
                {
                    var yo = _updateFunctionStopwatchLeftThumbstick.Elapsed.Milliseconds;

                    if (yo >= 200)
                    {
                        if (buttonPressedOculusTouchLeft == 1024 && hasClickedBUTTONX == 0)
                        {
                            //ShowKeyboard();

                            //https://stackoverflow.com/questions/2929255/unable-to-launch-onscreen-keyboard-osk-exe-from-a-32-bit-process-on-win7-x64
                            /*Process[] p = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(OnScreenKeyboardExe));

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
                            }*/

                            /*string windowsKeyboard = "osk";

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
                            }*/


                            /*ProcessStartInfo startInfo = new ProcessStartInfo();
                            startInfo.CreateNoWindow = false;
                            startInfo.UseShellExecute = false;
                            startInfo.WorkingDirectory = @"c:\WINDOWS\system32\";
                            startInfo.FileName = "osk.exe";
                            startInfo.Verb = "runas";
                            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            try
                            {
                                using (Process process = Process.Start(startInfo))
                                {
                                    process.WaitForExit();
                                }
                            }
                            catch (Exception)
                            {

                                //throw;
                            }*/

                            /*string windowsKeyboard = "osk";

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
                        }*/



                            /*string windir = Environment.GetEnvironmentVariable("windir");

                            Process p = new Process();
                            p.StartInfo.FileName = windir + @"\System32\cmd.exe";
                            p.StartInfo.Arguments = "/C " + windir + @"\System32\osk.exe";
                            p.StartInfo.CreateNoWindow = true;
                            p.StartInfo.UseShellExecute = false;
                            p.Start();
                            p.Dispose();*/



                            /*ProcessStartInfo startInfo = new ProcessStartInfo();
                            startInfo.CreateNoWindow = false;
                            startInfo.UseShellExecute = true;
                            startInfo.WorkingDirectory = @"c:\WINDOWS\system32\";
                            startInfo.FileName = "osk.exe";
                            startInfo.Verb = "runas";
                            startInfo.WindowStyle = ProcessWindowStyle.Normal;

                            try
                            {
                                using (Process process = Process.Start(startInfo))
                                {
                                    process.WaitForExit();
                                }
                            }
                            catch (Exception)
                            {
                                //throw;
                            }*/



                            //System.Diagnostics.Process.Start("osk.exe");
                            /*string windir = Environment.GetEnvironmentVariable("windir");

                            Process p = new Process();
                            p.StartInfo.FileName = windir + @"\System32\cmd.exe";
                            p.StartInfo.Arguments = "/C " + windir + @"\System32\osk.exe";
                            p.StartInfo.CreateNoWindow = true;
                            p.StartInfo.UseShellExecute = false;
                            p.Start();
                            p.Dispose();*/

                            //var path64 = @"c:\windows\sysnative\osk.exe"; //@"C:\Windows\winsxs\amd64_microsoft-windows-osk_31bf3856ad364e35_6.1.7600.16385_none_06b1c513739fb828\osk.exe";
                            //var path32 = @"c:\windows\sysnative\osk.exe";// @"C:\windows\system32\osk.exe"; 
                            //var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;

                            //string somestr = getOskPath(@"c:\windows\system32\");




                            /*string[] dirs = Directory.GetFiles(@"c:\Windows\System32\", "c*");

                            Console.WriteLine("The number of files starting with c is {0}.", dirs.Length);

                            foreach (string dir in dirs)
                            {

                                Console.WriteLine(dir);
                                if (dir == @"c:\Windows\System32\osk.exe")
                                {
                                    Program.MessageBox((IntPtr)0, "", "sccs error", 0);
                                    Console.WriteLine(dir);
                                    string somestr = getOskPath(dir);

                                    Program.MessageBox((IntPtr)0, "" + somestr, "sccs error", 0);
                                }
                            }*/





                            /*string somestr = @"c:\windows\system32\osk.exe";// getOskPath(@"c:\windows\system32\osk.exe");
                            var permissionSet = new PermissionSet(PermissionState.None);
                            var writePermission = new FileIOPermission(FileIOPermissionAccess.Read, somestr); //write
                            permissionSet.AddPermission(writePermission);

                            if (permissionSet.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet))
                            {
                                Program.MessageBox((IntPtr)0, "" + somestr, "sccs error", 0);

                                Process[] p = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(OnScreenKeyboardExe));

                                if (p.Length == 0)
                                {
                                    // we must start osk from an MTA thread
                                    if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
                                    {
                                        ThreadStart start = new ThreadStart(ShowKeyboard);// StartOsk);
                                        Thread thread = new Thread(start);
                                        thread.SetApartmentState(ApartmentState.MTA);
                                        thread.Start();
                                        thread.Join();
                                    }
                                    else
                                    {
                                        ShowKeyboard();//StartOsk();
                                    }
                                }
                                else
                                {
                                    // there might be a race condition if the process terminated 
                                    // meanwhile -> proper exception handling should be added
                                    //
                                    SendMessage(p[0].MainWindowHandle, WM_SYSCOMMAND, new IntPtr(SC_RESTORE), new IntPtr(0)); //MainWindowHandle
                                }
                            }
                            else
                            {

                            }*/


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












                            //Program.MessageBox((IntPtr)0, "fuck you", "sccs message", 0);
                            /*string windowsKeyboard = "osk";

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
                            }*/




                            hasClickedBUTTONX = 1;
                            _updateFunctionBoolLeftThumbStick = true;
                        }
                        else if (buttonPressedOculusTouchLeft == 1048576 && buttonPressedOculusTouchLeft != lastbuttonPressedOculusTouchLeft)
                        {
                            if (hasClickedHomeButtonTouchLeft == 0)
                            {
                                //SC_console_directx.D3D.OVR.RecenterTrackingOrigin(SC_console_directx.D3D._oculusRiftVirtualRealityProvider.SessionPtr);
                                SC_console_directx.D3D.OVR.RecenterTrackingOrigin(SC_console_directx.D3D.sessionPtr);// _oculusRiftVirtualRealityProvider.SessionPtr);

                                hasClickedHomeButtonTouchLeft = 1;
                            }
                        }
                    }
                }
                else if (buttonPressedOculusTouchLeft == 0 && hasClickedHomeButtonTouchLeft == 1 || buttonPressedOculusTouchLeft == 0 && hasClickedBUTTONX == 1)
                {
                    if (buttonPressedOculusTouchLeft == 0 && hasClickedHomeButtonTouchLeft == 1)
                    {
                        hasClickedHomeButtonTouchLeft = 0;
                    }
                    else if (buttonPressedOculusTouchLeft == 0 && hasClickedBUTTONX == 1)
                    {
                        hasClickedBUTTONX = 0;
                    }
                }




                /*
                if (buttonPressedOculusTouchLeft != 0)
                {
                    //Program.MessageBox((IntPtr)0, buttonPressedOculusTouchLeft + "", "sccs message", 0);

                    if (buttonPressedOculusTouchLeft == 1024 && hasClickedBUTTONX == 0)
                    {
                        /*Process[] p = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(OnScreenKeyboardExe));

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
                            SendMessage(p[0].MainWindowHandle, WM_SYSCOMMAND, new IntPtr(SC_RESTORE), new IntPtr(0)); //MainWindowHandle
                        }*/
                //Program.MessageBox((IntPtr)0, "fuck you", "sccs message", 0);
                /*string windowsKeyboard = "osk";

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
                //hasClickedBUTTONX = 1;
            }
        }
        else if (buttonPressedOculusTouchLeft == 0 && hasClickedHomeButtonTouchLeft == 1 || buttonPressedOculusTouchLeft == 0 && hasClickedBUTTONX == 1)
        {
            if (buttonPressedOculusTouchLeft == 0 && hasClickedHomeButtonTouchLeft == 1)
            {
                hasClickedHomeButtonTouchLeft = 0;
            }
            else if (buttonPressedOculusTouchLeft == 0 && hasClickedBUTTONX == 1)
            {
                hasClickedBUTTONX = 0;
            }

        }*/



                /*if (buttonPressedOculusTouchLeft != 0)
                {
                    var yo = _updateFunctionStopwatchLeftThumbstick.Elapsed.Ticks;

                    if (yo >= 100)
                    {
                        if (buttonPressedOculusTouchLeft == 1024)
                        {

                            _updateFunctionBoolLeftThumbStick = true;
                        }
                    }
                }*/
            }
        }


        /*static string getOskPath(string dir)
        {
            string path = Path.Combine(dir, "osk.exe");
            /*if (File.Exists(path))
            {
                Process p = System.Diagnostics.Process.Start(path);
                if (p.IsWin64Emulator())
                {
                    path = string.Empty;
                }
                p.Kill();
                return path;
            }


            DirectoryInfo di = new DirectoryInfo(dir);
            foreach (DirectoryInfo subDir in di.GetDirectories().Reverse())
            {
                path = getOskPath(Path.Combine(dir, subDir.Name));
                if (!string.IsNullOrWhiteSpace(path))
                {
                    return path;
                }
            }
            return string.Empty;
        }*/






        uint lastbuttonPressedOculusTouchRight = 0;
        uint lastbuttonPressedOculusTouchLeft = 0;
        public int _indexMouseMove = 0;
        bool restartFrameCounterRight = false;

        int hasClickedHomeButtonTouchLeft = 0;
        int hasClickedHomeButtonTouchLeftCounter = 0;

        bool isHoldingBUTTONA = false;
        bool hasClickedBUTTONB = false;
        int hasClickedBUTTONX = 0;
        int hasClickedBUTTONY = 0;
        bool restartFrameCounterLeft = false;

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
                    if (percentXRight >= 0 && percentXRight <= SC_console_directx.D3D.SurfaceWidth && percentYRight >= 0 && percentYRight <= SC_console_directx.D3D.SurfaceHeight &&
                        realMousePosX >= 0 && realMousePosX <= SC_console_directx.D3D.SurfaceWidth && realMousePosY >= 0 && realMousePosY <= SC_console_directx.D3D.SurfaceHeight)
                    {
                        //MessageBox((IntPtr)0, "test1", "mouse move", 0);

                        //var absoluteMoveX = Convert.ToUInt32((percentXRight * 65535) / SC_console_directx.D3D.SurfaceWidth);
                        //var absoluteMoveY = Convert.ToUInt32((percentYRight * 65535) / SC_console_directx.D3D.SurfaceHeight);

                        var yo = _updateFunctionStopwatchRight.Elapsed.Ticks;

                        if (_hasLockedMouse == 0)
                        {
                            if (yo >= 250)
                            {
                                var absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535 - 1)) / SC_console_directx.D3D.SurfaceWidth);
                                var absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535 - 1)) / SC_console_directx.D3D.SurfaceHeight);

                                if (realMousePosX >= 0 && realMousePosX < SC_console_directx.D3D.SurfaceWidth)
                                {

                                }
                                else
                                {
                                    realMousePosX = SC_console_directx.D3D.SurfaceWidth;
                                    absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535 - 1)) / SC_console_directx.D3D.SurfaceWidth);
                                }

                                if (realMousePosY >= 0 && realMousePosY < SC_console_directx.D3D.SurfaceHeight)
                                {

                                }
                                else
                                {
                                    realMousePosY = SC_console_directx.D3D.SurfaceHeight;
                                    absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535 - 1)) / SC_console_directx.D3D.SurfaceHeight);
                                }


                                //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, absoluteMoveX, absoluteMoveY, 0, 0);
                                if (_frameCounterTouchRight <= 20)
                                {
                                    SetCursorPos((int)realMousePosX, (int)realMousePosY);
                                    //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, _lastMousePosXRight, _lastMousePosYRight, 0, 0);
                                    _frameCounterTouchRight = 0;
                                }

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
                                    var absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535)) / SC_console_directx.D3D.SurfaceWidth);
                                    var absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535)) / SC_console_directx.D3D.SurfaceHeight);

                                    if (realMousePosX >= 0 && realMousePosX < SC_console_directx.D3D.SurfaceWidth)
                                    {

                                    }
                                    else
                                    {
                                        realMousePosX = SC_console_directx.D3D.SurfaceWidth;
                                        absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535)) / SC_console_directx.D3D.SurfaceWidth);
                                    }

                                    if (realMousePosY >= 0 && realMousePosY < SC_console_directx.D3D.SurfaceHeight)
                                    {

                                    }
                                    else
                                    {
                                        realMousePosY = SC_console_directx.D3D.SurfaceHeight;
                                        absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535)) / SC_console_directx.D3D.SurfaceHeight);
                                    }


                                    if (_frameCounterTouchRight <= 20 && _canResetCounterTouchRightButtonA == true)
                                    {
                                        SetCursorPos((int)realMousePosX, (int)realMousePosY);
                                        //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, _lastMousePosXRight, _lastMousePosYRight, 0, 0);
                                        _frameCounterTouchRight = 0;
                                    }

                                    //mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0); //| MOUSEEVENTF_ABSOLUTE

                                    Program.mousesim.LeftButtonDown();

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
                                    var absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535)) / SC_console_directx.D3D.SurfaceWidth);
                                    var absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535)) / SC_console_directx.D3D.SurfaceHeight);

                                    if (realMousePosX >= 0 && realMousePosX < SC_console_directx.D3D.SurfaceWidth)
                                    {

                                    }
                                    else
                                    {
                                        realMousePosX = SC_console_directx.D3D.SurfaceWidth;
                                        absoluteMoveX = Convert.ToUInt32((realMousePosX * (65535)) / SC_console_directx.D3D.SurfaceWidth);
                                    }

                                    if (realMousePosY >= 0 && realMousePosY < SC_console_directx.D3D.SurfaceHeight)
                                    {

                                    }
                                    else
                                    {
                                        realMousePosY = SC_console_directx.D3D.SurfaceHeight;
                                        absoluteMoveY = Convert.ToUInt32((realMousePosY * (65535)) / SC_console_directx.D3D.SurfaceHeight);

                                    }


                                    //mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                    Program.mousesim.RightButtonDown();
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

                    if (hasClickedBUTTONACounter >= 25)
                    {
                        //////////OCULUS TOUCH BUTTONS PRESSED////////////////////////////////////////
                        if (hasClickedBUTTONA == 1 && buttonPressedOculusTouchRight == 0 || hasClickedBUTTONB && buttonPressedOculusTouchRight == 0)
                        {
                            if (hasClickedBUTTONA == 1 && buttonPressedOculusTouchRight == 0)
                            {
                                //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                Program.mousesim.LeftButtonUp();
                                hasClickedBUTTONACounter = 0;
                                hasClickedBUTTONA = 0;
                            }
                            else if (hasClickedBUTTONB && buttonPressedOculusTouchRight == 0)
                            {
                                //mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                                Program.mousesim.RightButtonUp();
                                hasClickedBUTTONACounter = 0;
                                hasClickedBUTTONB = false;
                            }
                        }
                    }
                    //if (_canResetCounterTouchRightButtonA)
                    //{
                    //  
                    //}
                    _frameCounterTouchRight++;
                    if (_frameCounterTouchRight >= 30)
                    {
                        _frameCounterTouchRight = 0;
                        _canResetCounterTouchRightButtonA = false;
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

                    //if (_canResetCounterTouchLeftButtonX)
                    //{
                    //
                    //}



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
                    if (percentXRight >= 0 && percentXRight <= SC_console_directx.D3D.SurfaceWidth && percentYRight >= 0 && percentYRight <= SC_console_directx.D3D.SurfaceHeight)
                    {
                        var absoluteMoveX = Convert.ToUInt32((percentXRight * 65535) / SC_console_directx.D3D.SurfaceWidth);
                        var absoluteMoveY = Convert.ToUInt32((percentYRight * 65535) / SC_console_directx.D3D.SurfaceHeight);

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

                                    //mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, absoluteMoveX, absoluteMoveY, 0, 0);
                                    //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, absoluteMoveX, absoluteMoveY, 0, 0);

                                    SetCursorPos((int)absoluteMoveX, (int)absoluteMoveY);


                                    Program.mousesim.LeftButtonDown();
                                    Program.mousesim.LeftButtonUp();

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
                                    //mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                    Program.mousesim.RightButtonDown();
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
                            //mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                            Program.mousesim.RightButtonUp();
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











            hasClickedBUTTONACounter++;
        }

        int hasClickedBUTTONA = 0;
        int hasClickedBUTTONACounter = 0;

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
        bool _updateFunctionBoolLeftThumbStick = true;
        int _frameCounterTouchRight = 0;

        Plane planer;

        Vector3 centerPosRight;
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
        double differenceX = 0;
        double differenceY = 0;
        double differenceZ = 0;
        double percentXLeft;
        double percentYLeft;

        float widthLength;
        float heightLength;
        double currentPosWidth;
        double currentPosHeight;

        double percentXRight;
        double percentYRight;

        double currentX;
        double currentY;
        double currentZ;

        int _has_init_ray;
        JMatrix _last_frame_rigid_grab_rot;
        Vector3 _last_frame_rigid_grab_pos;
        Vector3 _last_frame_handPos = Vector3.Zero;
        Vector3 _last_final_hand_pos_right;

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

        int _hasLockedMouse = 0;

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
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd,
                                 UInt32 Msg,
                                 IntPtr wParam,
                                 IntPtr lParam);
        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);


        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);
        private const UInt32 WM_SYSCOMMAND = 0x112;
        private const UInt32 SC_RESTORE = 0xf120;

        private const string OnScreenKeyboardExe = "osk.exe";

        private void ShowKeyboard()
        {
            var path64 = @"c:\windows\sysnative\osk.exe"; //@"C:\Windows\winsxs\amd64_microsoft-windows-osk_31bf3856ad364e35_6.1.7600.16385_none_06b1c513739fb828\osk.exe";
            var path32 = @"c:\windows\sysnative\osk.exe";// @"C:\windows\system32\osk.exe"; 
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
            psi.Verb = "runas";

            Process.Start(psi);

            // Re-enable directory virtualisation if it was disabled.
            if (System.Environment.Is64BitOperatingSystem)
                if (sucessfullyDisabledWow64Redirect)
                    Wow64RevertWow64FsRedirection(ptr);
        }










        SharpDX.Matrix _mouseCursorMatrix = SharpDX.Matrix.Identity;
        int _out_of_bounds_oculus_rift = 0;
        int _out_of_bounds_right = 0;
        int _out_of_bounds_left = 0;
        uint _lastMousePosXRight = 9999;
        uint _lastMousePosYRight = 9999;



        float _delta_timer_frame = 0;
        float _delta_timer_time = 0;
        DateTime time1;
        DateTime time2;
        float deltaTime;
        Stopwatch timeStopWatch00 = new Stopwatch();
        Stopwatch timeStopWatch01 = new Stopwatch();
        int _swtch = 0;
        int _swtch_counter_00 = 0;
        int _swtch_counter_01 = 0;
        int _swtch_counter_02 = 0;
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
            _delta_timer_frame = (float)Math.Abs((timeStopWatch01.Elapsed.Ticks - timeStopWatch00.Elapsed.Ticks));

            time2 = DateTime.Now;
            _delta_timer_time = (time2.Ticks - time1.Ticks); //100000000f
            //time1 = time2;

            deltaTime = (float)Math.Abs(_delta_timer_frame - _delta_timer_time);

            //time1 = time2;
            await Task.Delay(1);
            Thread.Sleep(timeOut);
            _swtch_counter_00++;
            _swtch_counter_01++;
            _swtch_counter_02++;

            goto _threadLoop;
        }


    }
}





/*
Vector3 current_handposR = new Vector3(_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);

Matrix tempmatter = _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos;
Quaternion quater;
Quaternion.RotationMatrix(ref tempmatter, out quater);

var rayDirForward = sc_maths._getDirection(SharpDX.Vector3.ForwardRH, quater);
rayDirForward.Normalize();
var rayDirUp = sc_maths._getDirection(SharpDX.Vector3.Up, quater);
rayDirUp.Normalize();
var rayDirRight = sc_maths._getDirection(SharpDX.Vector3.Right, quater);
rayDirRight.Normalize();

Vector3 movingPointer = current_handposR + (-rayDirForward * _grab_rigid_data.dirDiffZ);
movingPointer = movingPointer + (rayDirRight * _grab_rigid_data.dirDiffX);
//movingPointer = movingPointer + (-rayDirUp * _grab_rigid_data.dirDiffY);

Matrix tempMat = _grab_rigid_data.position;// translationMatrix;
tempMat.M41 = 0;
tempMat.M42 = 0;
tempMat.M43 = 0;
tempMat.M44 = 1;

Quaternion.RotationMatrix(ref tempmatter, out quater);
JQuaternion _other_quatter = new JQuaternion(quater.X, quater.Y, quater.Z, quater.W);

Matrix anothertempmat = _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos;
anothertempmat.M41 = 0;
anothertempmat.M42 = 0;
anothertempmat.M43 = 0;
anothertempmat.M44 = 1;

var xq = _other_quatter.X;
var yq = _other_quatter.Y;
var zq = _other_quatter.Z;
var wq = _other_quatter.W;

var pitcha = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
var yawa = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
var rolla = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);

double currentGrabrotDiffX = (pitcha - grabrotX);
double currentGrabrotDiffY = (yawa - grabrotY);
double currentGrabrotDiffZ =  (rolla - grabrotZ);

tempMatrix = SharpDX.Matrix.RotationYawPitchRoll((float)currentGrabrotDiffY, (float)currentGrabrotDiffX, (float)currentGrabrotDiffZ);

//_grab_rigid_data.position.Invert();

tempMatrix = _grab_rigid_data.position;// tempMatrix * _grab_rigid_data.position;


tempMatrix.M41 = movingPointer.X;
tempMatrix.M42 = movingPointer.Y;
tempMatrix.M43 = movingPointer.Z;

//anothertempmat.Invert();
//Matrix addMat = _grab_rigid_data.position;
//Matrix addresultMat;
//Matrix.Add(ref anothertempmat, ref addMat, out addresultMat);

Quaternion.RotationMatrix(ref tempMatrix, out quater);
body.Position = new JVector(movingPointer.X, movingPointer.Y, movingPointer.Z);
JQuaternion _other_quat = new JQuaternion(quater.X, quater.Y, quater.Z, quater.W);
var matrixIn = JMatrix.CreateFromQuaternion(_other_quat);
body.Orientation = matrixIn;*/





















/*
Matrix grabbedBodyMatrix = _grab_rigid_data.position;

var MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M43);
Matrix someMatRight = _rightTouchMatrix;
someMatRight.M41 = handPoseRight.Position.X + MOVINGPOINTER.X;
someMatRight.M42 = handPoseRight.Position.Y;// + MOVINGPOINTER.Y;
someMatRight.M43 = handPoseRight.Position.Z + MOVINGPOINTER.Z;
var diffNormPosX = (MOVINGPOINTER.X) - someMatRight.M41;
var diffNormPosY = (MOVINGPOINTER.Y) - someMatRight.M42;
var diffNormPosZ = (MOVINGPOINTER.Z) - someMatRight.M43;
MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_right* (diffNormPosX));
MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_up* (diffNormPosY));
MOVINGPOINTER = MOVINGPOINTER + (current_rotation_of_torso_pivot_forward* (diffNormPosZ));
Matrix finalHRMat = _rightTouchMatrix * OriginRot * RotatingMatrix * RotatingMatrixForPelvis;// ; //finalRotationMatrix
MOVINGPOINTER.X += SC_Update.OFFSETPOS.X;
MOVINGPOINTER.Y += SC_Update.OFFSETPOS.Y;
MOVINGPOINTER.Z += SC_Update.OFFSETPOS.Z;
Matrix handMatrix = _rightTouchMatrix;// _rightTouchMatrix * OriginRot * RotatingMatrix * RotatingMatrixForPelvis;
Quaternion quater;
Quaternion.RotationMatrix(ref handMatrix, out quater);
var xq = quater.X;
var yq = quater.Y;
var zq = quater.Z;
var wq = quater.W;
var pitchaHand = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
var yawaHand = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
var rollaHand = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
var pitchTouchR = (float)(Math.PI * (grabrotX - pitchaHand) / 180.0f);
var yawTouchR = (float)(Math.PI * (grabrotY - yawaHand) / 180.0f);
var rollTouchR = (float)(Math.PI * (grabrotZ - rollaHand) / 180.0f);
var rotatingMatrixForTouchR = SharpDX.Matrix.RotationYawPitchRoll(yawTouchR, pitchTouchR, rollTouchR);
var pitch = (float)(Math.PI * (SC_Update.RotationGrabbedX) / 180.0f);
var yaw = (float)(Math.PI * (SC_Update.RotationGrabbedY) / 180.0f);
var roll = (float)(Math.PI * (SC_Update.RotationGrabbedZ) / 180.0f);
var rotatingMatrixForGrabber = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);
var matrixerer = _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos * grabbedBodyMatrix; //rotatingMatrixForGrabber
matrixerer.M41 = MOVINGPOINTER.X;
matrixerer.M42 = MOVINGPOINTER.Y;
matrixerer.M43 = MOVINGPOINTER.Z;
matrixerer.M44 = 1;
body.Position = new JVector(MOVINGPOINTER.X, MOVINGPOINTER.Y, MOVINGPOINTER.Z);
Quaternion.RotationMatrix(ref matrixerer, out quater);
JQuaternion _other_quat = new JQuaternion(quater.X, quater.Y, quater.Z, quater.W);
var matrixIn = JMatrix.CreateFromQuaternion(_other_quat);
body.Orientation = matrixIn;
*/



/*
Matrix grabbedBodyMatrix = _grab_rigid_data.position;
var MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M43);
Matrix someMatRight = _rightTouchMatrix;
someMatRight.M41 = handPoseRight.Position.X + MOVINGPOINTER.X;
someMatRight.M42 = handPoseRight.Position.Y;// + MOVINGPOINTER.Y;
someMatRight.M43 = handPoseRight.Position.Z + MOVINGPOINTER.Z;
var diffNormPosX = (MOVINGPOINTER.X) - someMatRight.M41;
var diffNormPosY = (MOVINGPOINTER.Y) - someMatRight.M42;
var diffNormPosZ = (MOVINGPOINTER.Z) - someMatRight.M43;
MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_right * (diffNormPosX));
MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_up * (diffNormPosY));
MOVINGPOINTER = MOVINGPOINTER + (current_rotation_of_torso_pivot_forward * (diffNormPosZ));
Matrix finalHRMat = _rightTouchMatrix * OriginRot * RotatingMatrix * RotatingMatrixForPelvis;// ; //finalRotationMatrix
MOVINGPOINTER.X += SC_Update.OFFSETPOS.X;
MOVINGPOINTER.Y += SC_Update.OFFSETPOS.Y;
MOVINGPOINTER.Z += SC_Update.OFFSETPOS.Z;
Matrix handMatrix = _rightTouchMatrix;// _rightTouchMatrix * OriginRot * RotatingMatrix * RotatingMatrixForPelvis;
Quaternion quater;
Quaternion.RotationMatrix(ref handMatrix, out quater);
var xq = quater.X;
var yq = quater.Y;
var zq = quater.Z;
var wq = quater.W;
var pitchaHand = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
var yawaHand = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
var rollaHand = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
var pitchTouchR = (float)(Math.PI * (grabrotX - pitchaHand) / 180.0f);
var yawTouchR = (float)(Math.PI * (grabrotY - yawaHand) / 180.0f);
var rollTouchR = (float)(Math.PI * (grabrotZ - rollaHand) / 180.0f);
var rotatingMatrixForTouchR = SharpDX.Matrix.RotationYawPitchRoll(yawTouchR, pitchTouchR, rollTouchR);
var pitch = (float)(Math.PI * (SC_Update.RotationGrabbedX) / 180.0f);
var yaw = (float)(Math.PI * (SC_Update.RotationGrabbedY) / 180.0f);
var roll = (float)(Math.PI * (SC_Update.RotationGrabbedZ) / 180.0f);
var rotatingMatrixForGrabber = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);
var matrixerer = grabbedBodyMatrix * rotatingMatrixForGrabber* _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos; //rotatingMatrixForGrabber //_player_rght_hnd[0][0]._arrayOfInstances[0].current_pos * 
matrixerer.M41 = MOVINGPOINTER.X;
matrixerer.M42 = MOVINGPOINTER.Y;
matrixerer.M43 = MOVINGPOINTER.Z;
matrixerer.M44 = 1;
body.Position = new JVector(MOVINGPOINTER.X, MOVINGPOINTER.Y, MOVINGPOINTER.Z);
Quaternion.RotationMatrix(ref matrixerer, out quater);
JQuaternion _other_quat = new JQuaternion(quater.X, quater.Y, quater.Z, quater.W);
var matrixIn = JMatrix.CreateFromQuaternion(_other_quat);
body.Orientation = matrixIn;*/






/*
Matrix grabbedBodyMatrix = _grab_rigid_data.position;
Matrix handMatrix = _player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION;// _rightTouchMatrix * OriginRot * RotatingMatrix * RotatingMatrixForPelvis * hmdmatrixRot_;
//_player_rght_hnd[0][0]._arrayOfInstances[0]._TEMPPOSITION; 

handMatrix.M41 = 0;
handMatrix.M42 = 0;
handMatrix.M43 = 0;
handMatrix.M44 = 1;

grabbedBodyMatrix.M41 = 0;
grabbedBodyMatrix.M42 = 0;
grabbedBodyMatrix.M43 = 0;
grabbedBodyMatrix.M44 = 1;

finalRotationMatrix.M41 = 0;
finalRotationMatrix.M42 = 0;
finalRotationMatrix.M43 = 0;
finalRotationMatrix.M44 = 1;

var MOVINGPOINTER = new Vector3(_player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M41, _player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M42, _player_torso[0][0]._arrayOfInstances[0]._ORIGINPOSITION.M43);
Matrix someMatRight = _rightTouchMatrix;// * OriginRot * RotatingMatrix * RotatingMatrixForPelvis * hmdmatrixRot_;
someMatRight.M41 = handPoseRight.Position.X + MOVINGPOINTER.X;
someMatRight.M42 = handPoseRight.Position.Y;// + MOVINGPOINTER.Y;
someMatRight.M43 = handPoseRight.Position.Z + MOVINGPOINTER.Z;
var diffNormPosX = (MOVINGPOINTER.X) - someMatRight.M41;
var diffNormPosY = (MOVINGPOINTER.Y) - someMatRight.M42;
var diffNormPosZ = (MOVINGPOINTER.Z) - someMatRight.M43;
MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_right* (diffNormPosX));
MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_up* (diffNormPosY));
MOVINGPOINTER = MOVINGPOINTER + (current_rotation_of_torso_pivot_forward* (diffNormPosZ));

MOVINGPOINTER.X += SC_Update.OFFSETPOS.X;
MOVINGPOINTER.Y += SC_Update.OFFSETPOS.Y;
MOVINGPOINTER.Z += SC_Update.OFFSETPOS.Z;

//Matrix matrixerer = _rightTouchMatrix;
//matrixerer.Invert();

Quaternion quater;
Quaternion.RotationMatrix(ref handMatrix, out quater);
var xq = quater.X;
var yq = quater.Y;
var zq = quater.Z;
var wq = quater.W;

var pitchaHand = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
var yawaHand = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
var rollaHand = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);

var pitchTouchR = (float)(Math.PI * (grabrotX - pitchaHand) / 180.0f);
var yawTouchR = (float)(Math.PI * (grabrotY - yawaHand) / 180.0f);
var rollTouchR = (float)(Math.PI * (grabrotZ - rollaHand) / 180.0f);

Matrix rotatingMatrixForTouchR = SharpDX.Matrix.RotationYawPitchRoll((float)yawTouchR, (float)pitchTouchR, (float)rollTouchR);
//Matrix rotatingMatrixForTouchR = Matrix.Scaling(1.0f) * Matrix.RotationX(pitchTouchR) * Matrix.RotationY(yawTouchR) * Matrix.RotationZ(rollTouchR);

var pitch = (float)(Math.PI * (-SC_Update.RotationGrabbedX) / 180.0f);
var yaw = (float)(Math.PI * (SC_Update.RotationGrabbedY) / 180.0f);
var roll = (float)(Math.PI * (SC_Update.RotationGrabbedZ) / 180.0f);

var rotatingMatrixForGrabber = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);




handMatrix = _player_r_hand_grab[0][0]._arrayOfInstances[0].current_pos;// * finalRotationMatrix;
//Quaternion quater;
Quaternion.RotationMatrix(ref handMatrix, out quater);

var rayDirForward = sc_maths._getDirection(SharpDX.Vector3.ForwardRH, quater);
rayDirForward.Normalize();
var rayDirUp = sc_maths._getDirection(SharpDX.Vector3.Up, quater);
rayDirUp.Normalize();
var rayDirRight = sc_maths._getDirection(SharpDX.Vector3.Right, quater);
rayDirRight.Normalize();

//handMatrix = _player_rght_hnd[0][0]._arrayOfInstances[0].current_pos;
var current_handposRR = new Vector3(MOVINGPOINTER.X,//_player_r_hand_grab[0][0]._arrayOfInstances[0].current_pos.M41,
MOVINGPOINTER.Y,//_player_r_hand_grab[0][0]._arrayOfInstances[0].current_pos.M42,
MOVINGPOINTER.Z);                //_player_r_hand_grab[0][0]._arrayOfInstances[0].current_pos.M43);

MOVINGPOINTER = current_handposRR + (rayDirForward* _grab_rigid_data.grabHitPointLength);
handMatrix = _player_r_hand_grab[0][0]._arrayOfInstances[0]._TEMPPOSITION* finalRotationMatrix;
var pitchTouchRer = (float)(Math.PI * ((float)SC_Update.RotationX4Pelvis) / 180.0f);
var yawTouchRer = (float)(Math.PI * ((float)SC_Update.RotationY4Pelvis) / 180.0f);
var rollTouchRer = (float)(Math.PI * ((float)SC_Update.RotationZ4Pelvis) / 180.0f);

var rotter = SharpDX.Matrix.RotationYawPitchRoll((float)yawTouchRer, (float)pitchTouchRer, (float)rollTouchRer);
Matrix matrixerer = handMatrix;
matrixerer.M41 = MOVINGPOINTER.X;
matrixerer.M42 = MOVINGPOINTER.Y;
matrixerer.M43 = MOVINGPOINTER.Z;
matrixerer.M44 = 1;

body.Position = new JVector(MOVINGPOINTER.X, MOVINGPOINTER.Y, MOVINGPOINTER.Z);
Quaternion.RotationMatrix(ref matrixerer, out quater);
JQuaternion _other_quat = new JQuaternion(quater.X, quater.Y, quater.Z, quater.W);
var matrixIn = JMatrix.CreateFromQuaternion(_other_quat);
body.Orientation = matrixIn;*/




//Matrix matrixerer = _rightTouchMatrix;
//matrixerer.Invert();





/*Quaternion.RotationMatrix(ref grabbedBodyMatrix, out quater);
xq = quater.X;
yq = quater.Y;
zq = quater.Z;
wq = quater.W;

var pitchaHand1 = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI);
var yawaHand1 = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq)  * (180 / Math.PI);
var rollaHand1 = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq)  * (180 / Math.PI);

var pitchTouchR = (float)(Math.PI * (pitchaHand1) / 180.0f); // - (grabrotX- pitchaHand)
var yawTouchR = (float)(Math.PI * (yawaHand1) / 180.0f);
var rollTouchR = (float)(Math.PI * (rollaHand1) / 180.0f); // - (grabrotZ - rollaHand)

Matrix rotatingMatrixForTouchR = SharpDX.Matrix.RotationYawPitchRoll((float)yawTouchR, (float)pitchTouchR, (float)rollTouchR);
//Matrix rotatingMatrixForTouchR = Matrix.Scaling(1.0f) * Matrix.RotationX(pitchTouchR) * Matrix.RotationY(yawTouchR) * Matrix.RotationZ(rollTouchR);
*/
//var pitch = (float)(Math.PI * (-SC_Update.RotationGrabbedX) / 180.0f);
//var yaw = (float)(Math.PI * (SC_Update.RotationGrabbedY) / 180.0f);
//var roll = (float)(Math.PI * (SC_Update.RotationGrabbedZ) / 180.0f);
//var rotatingMatrixForGrabber = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);




//handMatrix = _player_r_hand_grab[0][0]._arrayOfInstances[0].current_pos;// * finalRotationMatrix;
//Quaternion quater;






/*float timeSinceStart = (float)(DateTime.Now - SC_Update.startTime).TotalSeconds;


var pitcher = (float)(Math.PI * (pitchaHand - touchRX) / 180.0f);
var yawer = (float)(Math.PI * (yawaHand - touchRY) / 180.0f);
var roller = (float)(Math.PI * (rollaHand - touchRZ) / 180.0f);
var rotatingMatrixF = SharpDX.Matrix.RotationYawPitchRoll(yawer, pitcher, roller);

totalDiffX = pitcher;
totalDiffY = yawer;
totalDiffZ = roller;

//rotatingMatrixF *= RotatingMatrixForPelvis;

var pitch = (float)(Math.PI * (-SC_Update.RotationGrabbedX) / 180.0f);
var yaw = (float)(Math.PI * (SC_Update.RotationGrabbedY) / 180.0f);
var roll = (float)(Math.PI * (SC_Update.RotationGrabbedZ) / 180.0f);
var rotatingMatrixForGrabber = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

///Quaternion.RotationMatrix(ref rotatingMatrixF, out quaterNion);

Quaternion.RotationMatrix(ref handMatrix, out quater);

var rayDirForwardGrab = sc_maths._getDirection(SharpDX.Vector3.ForwardRH, quater);
rayDirForwardGrab.Normalize();
var rayDirUpGrab = sc_maths._getDirection(SharpDX.Vector3.Up, quater);
rayDirUpGrab.Normalize();
var rayDirRightGrab = sc_maths._getDirection(SharpDX.Vector3.Right, quater);
rayDirRightGrab.Normalize();

Vector3 grabPos = new Vector3(grabbedBodyMatrix.M41,
grabbedBodyMatrix.M42,
grabbedBodyMatrix.M43);

Vector3 lookAt = Vector3.TransformCoordinate(Vector3.ForwardRH, rotatingMatrixHand);
Vector3 up = Vector3.TransformCoordinate(Vector3.Up, rotatingMatrixHand);

Quaternion currentRot;
Quaternion.RotationMatrix(ref grabbedBodyMatrix, out currentRot);
matrixerer = Matrix.Scaling(1.0f)* grabbedBodyMatrix * Matrix.RotationY(totalDiffY);
matrixerer.Invert();
*/
/*Quaternion quatYaw = new Quaternion(0, , 0,1);
quatYaw.Normalize();
//quatYaw *= currentRot;
//quatYaw.Normalize();
currentRot *= quatYaw;
currentRot.Normalize();


//Quaternion quatPitch = new Quaternion(pitchaHand, 0, 0, 1);
//currentRot *= quatPitch;
//currentRot.Normalize();
                                                                
Matrix.RotationQuaternion(ref currentRot, out matrixerer);*/

//Vector3 lookAt = Vector3.TransformCoordinate(rayDirForwardGrab, rotatingMatrixHand);
//Vector3 up = Vector3.TransformCoordinate(rayDirUpGrab, rotatingMatrixHand);

//Vector3 positionDisplacement = Vector3.TransformCoordinate(MOVINGPOINTER, rotatingMatrixHand);

// Finally create the view matrix from the three updated vectors.

//matrixerer = Matrix.LookAtRH(MOVINGPOINTER, MOVINGPOINTER + lookAt, up);
//matrixerer.Invert();

/*//https://stackoverflow.com/questions/29571093/sharpdx-vector3-transform-method-doesnt-seem-to-rotate-vector-correctly
Vector3 eyePos = MOVINGPOINTER;// new Vector3(0, 1, 0);
Vector3 target = MOVINGPOINTER + rayDirForward; //Vector3.Zero;



Quaternion lookAt = Quaternion.LookAtRH(eyePos, target, rayDirUp);
lookAt.Normalize();

Vector3 newForward = Vector3.Transform(rayDirForward, lookAt);
newForward.Normalize();

Vector3 newUp = Vector3.Transform(rayDirUp, lookAt);
newUp.Normalize();


//MOVINGPOINTER += newForward;

Matrix matrixerer = Matrix.LookAtRH(MOVINGPOINTER, MOVINGPOINTER + newForward, newUp);*/

//matrixerer *= _rightTouchMatrix * finalRotationMatrix;





//JUNK OF MICROSOFT NOT WORKING - the piece of shits of microsoft.
//https://stackoverflow.com/questions/2929255/unable-to-launch-onscreen-keyboard-osk-exe-from-a-32-bit-process-on-win7-x64
//https://www.dreamincode.net/forums/topic/174949-open-on-screen-keyboard-in-c%23/
/*Process[] p = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(OnScreenKeyboardExe));

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
    SendMessage(p[0].MainWindowHandle, WM_SYSCOMMAND, new IntPtr(SC_RESTORE), new IntPtr(0)); //MainWindowHandle
}*/


//StartOsk();

/*string windowsKeyboard = "osk";

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
}*/

/* ProcessStartInfo startInfo = new ProcessStartInfo();
 startInfo.CreateNoWindow = false;
 startInfo.UseShellExecute = true;
 startInfo.WorkingDirectory = @"c:\WINDOWS\system32\";
 startInfo.FileName = "osk.exe";
 startInfo.Verb = "runas";
 startInfo.WindowStyle = ProcessWindowStyle.Normal;

 try
 {
     using (Process process = Process.Start(startInfo))
     {
         process.WaitForExit();
     }
 }
 catch (Exception)
 {
     //throw;
 }*/



//System.Diagnostics.Process.Start("osk.exe");
/*string windir = Environment.GetEnvironmentVariable("windir");

Process p = new Process();
p.StartInfo.FileName = windir + @"\System32\cmd.exe";
p.StartInfo.Arguments = "/C " + windir + @"\System32\osk.exe";
p.StartInfo.CreateNoWindow = true;
p.StartInfo.UseShellExecute = false;
p.Start();
p.Dispose();*/
