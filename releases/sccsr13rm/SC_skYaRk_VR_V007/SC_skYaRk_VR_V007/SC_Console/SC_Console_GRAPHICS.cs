using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

//using Ab3d.DXEngine;
using Ab3d.OculusWrap;
//using Ab3d.DXEngine.OculusWrap;
using Ab3d.OculusWrap.DemoDX11;

using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using SharpDX.DirectInput;

//using SC_skYaRk_VR_V007.SC_Graphics.SC_ShaderManager;
//using SC_skYaRk_VR_V007.SC_Graphics.SC_Grid;





using SC_skYaRk_VR_V007.SC_Graphics;
using SC_skYaRk_VR_V007.SC_Graphics.SC_Grid;
using SC_skYaRk_VR_V007.SC_Graphics.SC_Models;

using SC_skYaRk_VR_V007.SC_Graphics.SC_ShaderManager;




using Jitter;
using Jitter.Dynamics;
using Jitter.Collision;
using Jitter.LinearMath;
using Jitter.Collision.Shapes;
using Jitter.Forces;

using System.Collections.Generic;
using System.Collections;

using System.Threading.Tasks;
using System.Runtime;
using System.Runtime.CompilerServices;

using System.ComponentModel;


namespace SC_skYaRk_VR_V007
{
    public class SC_Console_GRAPHICS
    {


        const int sccsmaxmousecursorarrayframestabiliserleft = 100;
        const int sccsmaxmousecursorarrayframestabiliserright = 99;



        float _body_collision_fraction;
        RigidBody _body_collision;
        JVector _body_collision_normal;









        Jitter.Collision.RaycastCallback _last_frame_ray;
        int _has_init_ray;


        float _last_current_hand_float_for_d = 0;
        Vector3 _last_current_hand_pos_for_d;
        Matrix _last_grabbed_object_matrix;

        float _current_hand_float_for_dX = 0;
        float _current_hand_float_for_dY = 0;
        float _current_hand_float_for_dZ = 0;

        Vector3 _current_hand_pos_for_d;
        Matrix _grabbed_object_matrix;

        int _sec_logic_swtch_grab = 0;
        int _tier_logic_swtch_grab = 0;

        Vector3 _last_offset_grabbed_pos_norm;
        float _last_offset_grabbed_pos_norm_dist;


        float _last_min_distX = 0;
        float _last_min_distY = 0;
        float _last_min_distZ = 0;

        Vector3 _grab_hand_pos;
        Vector3 _grab_body_pos;
        Vector3 _last_final_hand_pos_right;

        JMatrix _last_frame_rigid_grab_rot;
        Vector3 _last_frame_rigid_grab_pos;
        Matrix rigidBodyMatrix;
        Matrix humanBodyRotation;
        Matrix someRotationFinal;

        Vector3 _last_frame_handPos = Vector3.Zero;


        float _grabbed_diff_x;
        float _grabbed_diff_y;
        float _grabbed_diff_z;


        int _last_swtch_hasRotated = -1;
        int _swtch_hasRotated = 0;
        int _swtch_hasRotated_init_rot = 0;
        int _swtch_can_access_object = 0;

        Matrix rotatingMatrixForGrabber;

        JMatrix _grabbed_body_pos_rot;


        int _has_grabbed_right = -1;
        Vector3 _offset_grabbed_pos_norm = new Vector3(0, 0, 0);
        Vector3 _offset_grabbed_pos = new Vector3(0, 0, 0);
        float _offset_grabbed_pos_dist = 0;


        float _offset_grabbed_pos_distX = 0;
        float _offset_grabbed_pos_distY = 0;
        float _offset_grabbed_pos_distZ = 0;



        int _has_grabbed_right_swtch = 0;
        float speedRot = 0.085f;
        float speedPos = 0.05f;


        PseudoCloth _pseudo_cloth;

        int _test = 0;

        //SC_VR_Cloth clothRect;
        JVector _screen_model_pos = new JVector();
        DModeler _mouseCursor;
        SharpDX.Matrix _mouseCursorMatrix = SharpDX.Matrix.Identity;


        DModelClass4_cube[] _screenCorners;


        Stopwatch _mainThreadStopWatch = new Stopwatch();

        //PseudoCloth clother;

        float sizeWidtherer;
        float sizeheighterer;
        DTexture _basicTexture;
        int _out_of_bounds_right = 0;
        int _out_of_bounds_left = 0;

        DModeler _intersectTouchRight;
        SharpDX.Matrix _intersectTouchRightMatrix = SharpDX.Matrix.Identity;

        DModeler _intersectTouchLeft;
        SharpDX.Matrix _intersectTouchLeftMatrix = SharpDX.Matrix.Identity;

        Matrix WorldMatrix;

        JVector[][][] _some_last_frame_vector;
        RigidBody[][][] _some_last_frame_rigibodies;
        JVector[] _arrayOfVecs;
        RigidBody[] _arrayOfBodies;
        SharpDX.Matrix[] _screenDirMatrix_correct_pos;
        Matrix _direction_offsetter = Matrix.Identity;
        Matrix final_hand_pos_left;
        Matrix final_hand_pos_right;

        Matrix original_left_touch_matrix;
        Matrix original_right_touch_matrix;
        SC_visual_object_manager _SC_visual_object_manager;

        Random _randNum = new Random();
        int _console_display_counter = 0;

        int _console_display_frame_counter = 0;

        Matrix _finalRotMatrixScreen = Matrix.Identity;
        Matrix rotationMatrix;


        int[][] _switch_for_collision;
        float _World_Step;

        Matrix[][] worldMatrix_instances_cloth;

        Matrix[][] worldMatrix_instances;
        World[] _world_list;

        int[][] _objects_static_00;
        int[][] _objects_static_counter_00;
        RigidBody[][] _objects_rigid_static_00;

        Matrix[] worldMatrix_Cloth_instances;




        SC_cube[] _world_cube_list;
        //SC_VR_Cloth[] _world_cloth_list;
        SC_cube[] _world_terrain_list;







        Matrix[] worldMatrix_Terrain_instances;
        SC_skYaRk_VR_V007.SC_Graphics.SC_cube.DLightBuffer[] _DLightBuffer = new SC_cube.DLightBuffer[1];
        int width = 1;
        int height = 1;
        int depth = 1;

        SC_SharpDX_ScreenFrame _desktopFrame;
        public static SC_SharpDX_ScreenCapture _desktopDupe;
        public static List<SC_cube> _arrayOfClothCubes = new List<SC_cube>();


        public static DShaderManager _shaderManager;
        public static World World { get; set; }

        SharpDX.Vector3 originPos = new SharpDX.Vector3(0, 1, 5);
        SharpDX.Vector3 movePos = new SharpDX.Vector3(0, 0, 0);
        SharpDX.Matrix originRot = SharpDX.Matrix.Identity;
        int[][] _some_frame_counter_raycast_00;
        int[][] _some_frame_counter_raycast_01;

        int _some_frame_counter_raycast_00_max_counter = 50;
        int _some_frame_counter_raycast_01_max_counter = 1000;


        int _some_frame_counter_randomizer_switch = 0;
        int _some_frame_counter_randomizer_switch_counter = 0;


        DModelClass2 _screenModel;

        public int _indexMouseMove = 0;
        /*Stopwatch _updateFunctionStopwatchLeftHandTrigger;
        Stopwatch _updateFunctionStopwatchRightHandTrigger;
        Stopwatch _updateFunctionStopwatchLeftThumbstickGoRight;
        Stopwatch _updateFunctionStopwatchLeftThumbstickGoLeft;*/
        Stopwatch _updateFunctionStopwatchRightThumbstickGoRight;
        Stopwatch _updateFunctionStopwatchRightThumbstickGoLeft;
        //Stopwatch _updateFunctionStopwatchRightThumbstick;
        bool _updateFunctionBoolRightThumbStick = true;

        Stopwatch _updateFunctionStopwatchLeftThumbstick;
        bool _updateFunctionBoolLeftThumbStick = true;


        /*Stopwatch _updateFunctionStopwatchRightIndexTrigger;
        Stopwatch _updateFunctionStopwatchLeftIndexTrigger;*/
        Stopwatch _updateFunctionStopwatchRight;
        //Stopwatch _updateFunctionStopwatchLeft;
        //Stopwatch _updateFunctionStopwatchTouchRightButtonA;
        //Stopwatch _newStopWatch = new Stopwatch();

        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_NOACTIVATE = 0x0010;
        private const int GWL_STYLE = -16;
        private const int WS_MINIMIZE = -131073;
        int _frameCounterTouchRight = 0;

        public int _mainThreadFrameCounter = 0;
        private int _frameCounter = 0;
        int strider;
        int _updateSceneFrameCounter = 0;
        //int currentFrameRight = 0;
        //int currentFrameLeft = 0;

        uint _lastMousePosXRight = 9999;
        uint _lastMousePosYRight = 9999;
        uint _lastMousePosXLeft = 9999;
        uint _lastMousePosYLeft = 9999;
        uint lastbuttonPressedOculusTouchRight = 0;
        //uint buttonPressedOculusTouchRight = 0;
        uint lastbuttonPressedOculusTouchLeft = 0;
        //uint buttonPressedOculusTouchLeft = 0;
        const uint ENABLE_QUICK_EDIT = 0x0040;
        const int STD_INPUT_HANDLE = -10;











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
            psi.Verb = "runas";

            Process.Start(psi);

            // Re-enable directory virtualisation if it was disabled.
            if (System.Environment.Is64BitOperatingSystem)
                if (sucessfullyDisabledWow64Redirect)
                    Wow64RevertWow64FsRedirection(ptr);

            _desktopDupe = new SC_SharpDX_ScreenCapture(0,0, D3D.Device);


        }



        double RotationY4Pelvis { get; set; }
        double RotationX4Pelvis { get; set; }
        double RotationZ4Pelvis { get; set; }
        double RotationY { get; set; }
        double RotationX { get; set; }
        double RotationZ { get; set; }
        //  Matrix WorldMatrix;
        double RotationScreenY { get; set; }
        double RotationScreenX { get; set; }
        double RotationScreenZ { get; set; }

        double RotationGrabbedY { get; set; }
        double RotationGrabbedX { get; set; }
        double RotationGrabbedZ { get; set; }

        double startnGrabbedY { get; set; }
        double startnGrabbedX { get; set; }
        double startnGrabbedZ { get; set; }


        double diffGrabbedY { get; set; }
        double diffGrabbedX { get; set; }
        double diffGrabbedZ { get; set; }








        double LastRotationScreenY { get; set; }
        double LastRotationScreenX { get; set; }
        double LastRotationScreenZ { get; set; }


        float oriRotationScreenY { get; set; }
        float oriRotationScreenX { get; set; }
        float oriRotationScreenZ { get; set; }


        float RotationTouchRightX { get; set; }
        float RotationTouchRightY { get; set; }
        float RotationTouchRightZ { get; set; }

        float RotationTouchLeftX { get; set; }
        float RotationTouchLeftY { get; set; }
        float RotationTouchLeftZ { get; set; }
        private SC_skYaRk_VR_V007.DCamera Camera { get; set; }









        int _has_started_vr = 0;






        private bool RaycastCallback(RigidBody body, JVector normal, float fraction)
        {
            if (body.IsStatic) return false;
            else return true;
        }



        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

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









        //HERE IS THE MOUSE STABILIZER ARRAYS - THE BIGGER THE ARRAYS THE SLOWER AND MORE STABLE THE MOUSE IS ON THE SCREEN.
        Vector3[] arrayOfStabilizerPosRight = new Vector3[sccsmaxmousecursorarrayframestabiliserleft];
        double[] arrayOfStabilizerPosXRight = new double[sccsmaxmousecursorarrayframestabiliserleft];
        double[] arrayOfStabilizerPosDifferenceXRight = new double[sccsmaxmousecursorarrayframestabiliserright];
        double[] arrayOfStabilizerPosYRight = new double[sccsmaxmousecursorarrayframestabiliserleft];
        double[] arrayOfStabilizerPosDifferenceYRight = new double[sccsmaxmousecursorarrayframestabiliserright];

        double[] arrayOfStabilizerPosZRight = new double[sccsmaxmousecursorarrayframestabiliserleft];
        double[] arrayOfStabilizerPosDifferenceZRight = new double[sccsmaxmousecursorarrayframestabiliserright];



        Vector3[] arrayOfStabilizerPosLeft = new Vector3[sccsmaxmousecursorarrayframestabiliserleft];
        double[] arrayOfStabilizerPosXLeft = new double[sccsmaxmousecursorarrayframestabiliserleft];
        double[] arrayOfStabilizerPosDifferenceXLeft = new double[sccsmaxmousecursorarrayframestabiliserright];
        double[] arrayOfStabilizerPosYLeft = new double[sccsmaxmousecursorarrayframestabiliserleft];
        double[] arrayOfStabilizerPosDifferenceYLeft = new double[sccsmaxmousecursorarrayframestabiliserright];

        double[] arrayOfStabilizerPosZLeft = new double[sccsmaxmousecursorarrayframestabiliserleft];
        double[] arrayOfStabilizerPosDifferenceZLeft = new double[sccsmaxmousecursorarrayframestabiliserright];

        //
        Vector3[] _arrayOfStabilizerPosRight = new Vector3[sccsmaxmousecursorarrayframestabiliserleft];
        double[] _arrayOfStabilizerPosXRight = new double[sccsmaxmousecursorarrayframestabiliserleft];
        double[] _arrayOfStabilizerPosDifferenceXRight = new double[sccsmaxmousecursorarrayframestabiliserright];
        double[] _arrayOfStabilizerPosYRight = new double[sccsmaxmousecursorarrayframestabiliserleft];
        double[] _arrayOfStabilizerPosDifferenceYRight = new double[sccsmaxmousecursorarrayframestabiliserright];

        Vector3[] _arrayOfStabilizerPosLeft = new Vector3[sccsmaxmousecursorarrayframestabiliserleft];
        double[] _arrayOfStabilizerPosXLeft = new double[sccsmaxmousecursorarrayframestabiliserleft];
        double[] _arrayOfStabilizerPosDifferenceXLeft = new double[sccsmaxmousecursorarrayframestabiliserright];
        double[] _arrayOfStabilizerPosYLeft = new double[sccsmaxmousecursorarrayframestabiliserleft];
        double[] _arrayOfStabilizerPosDifferenceYLeft = new double[sccsmaxmousecursorarrayframestabiliserright];



        /*
        arrayOfStabilizerPosXRight = new double[arrayOfStabilizerPosRight.Length];
        arrayOfStabilizerPosDifferenceXRight = new double[arrayOfStabilizerPosXRight.Length - 1];

        arrayOfStabilizerPosYRight = new double[arrayOfStabilizerPosRight.Length];
        arrayOfStabilizerPosDifferenceYRight = new double[arrayOfStabilizerPosYRight.Length - 1];

        arrayOfStabilizerPosZRight = new double[arrayOfStabilizerPosRight.Length];
        arrayOfStabilizerPosDifferenceZRight = new double[arrayOfStabilizerPosZRight.Length - 1];
        arrayOfStabilizerPosXLeft = new double[arrayOfStabilizerPosLeft.Length];
        arrayOfStabilizerPosYLeft = new double[arrayOfStabilizerPosLeft.Length];
        arrayOfStabilizerPosZLeft = new double[arrayOfStabilizerPosLeft.Length];
        arrayOfStabilizerPosDifferenceXLeft = new double[arrayOfStabilizerPosXLeft.Length - 1];
        arrayOfStabilizerPosDifferenceYLeft = new double[arrayOfStabilizerPosYLeft.Length - 1];
        arrayOfStabilizerPosDifferenceZLeft = new double[arrayOfStabilizerPosZLeft.Length - 1];*/



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

        //bool _failed = false;
        bool _createdSceneObjects = false;
        bool _shaderQuality = true;
        public bool _stopWatchSwitch = true;
        bool _startOnce = true;
        bool _startOnce0 = true;
        bool restartFrameCounterRight = false;
        bool hasClickedHomeButtonTouchLeft = false;
        bool isHoldingBUTTONA = false;
        bool hasClickedBUTTONA = false;
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


        Vector3 theCenterLeft = new Vector3(0, 0, 0);

        //Vector3 stabilizerPosRight = new Vector3(0, 0, 0);
        //Vector3 theCenterRight = new Vector3(0, 0, 0);
        //Vector3 _theCenterRight = new Vector3(0, 0, 0);
        Vector3 _theCenterLeft = new Vector3(0, 0, 0);
        Vector3 lowestX;
        Vector3 highestX;
        Vector3 lowestY;
        Vector3 highestY;
        Vector3 lowestZ;
        Vector3 highestZ;


        Vector3[] point3DCollection;// = new Vector3[4];

        // Properties
        private SC_Console_DIRECTX D3D { get; set; }
        //private SC_skYaRk_VR_V007.DCamera Camera { get; set; }
        // Constructor
        public SC_Console_GRAPHICS() { }

        public IntPtr Hwnd;

        SC_Console_WRITER _currentWriter;
        DirectInput directInput;


        int _has_locked_screen_pos = 0;
        int _has_locked_screen_pos_counter = 0;
        Matrix _last_screen_pos;
        Matrix _current_screen_pos;

        RigidBody _grabbed_body_right;
        SharpDX.Matrix rotatingMatrixForPelvis = SharpDX.Matrix.Identity;



        SharpDX.Matrix rotatingMatrix = SharpDX.Matrix.Identity;


        SharpDX.Matrix rotatingMatrixScreen = SharpDX.Matrix.Identity;
        SharpDX.Vector3 originPosScreen = new SharpDX.Vector3(0, 1, -0.25f);
        SharpDX.Matrix originRotScreen = SharpDX.Matrix.Identity;

        DateTime time1;
        DateTime time2;
        float deltaTime;

        Stopwatch timeStopWatch = new Stopwatch();

        public async Task DoWork(int timeOut)
        {
            timeStopWatch.Start();

            time1 = DateTime.Now;
            time2 = DateTime.Now;
        _threadLoop:

            //float startTime = (float)(timeStopWatch.ElapsedMilliseconds);
            //float DeltaTimer = (float)Math.Abs((timeStopWatch.ElapsedMilliseconds ) - startTime);
            //deltaTime += DeltaTimer;

            time2 = DateTime.Now;
            deltaTime = (time2.Ticks - time1.Ticks) / 1000000000f; //100000000f
            //time1 = time2;

            await Task.Delay(timeOut);
            Thread.Sleep(1);
            goto _threadLoop;
        }


        DateTime time01;
        DateTime time02;

        Task tsk;
        // Methods
        public bool Initialize(SC_skYaRk_VR_V007.DSystemConfiguration.DSystemConfiguration configuration, IntPtr windowsHandle, SC_Console_WRITER _writer)
        {
            try
            {


        


                //List<string> obj = new List<string>() { "wtf", "wtf" };


                float newRotationY;
                float clamped = 0;
                float rotMax = 25;
                float rot0;
                float rot1;
                float newRotY;
                float someRotForPelvis;
                float newRotX;

                Quaternion otherQuat;

                IEnumerator enumerator0;
                IEnumerator enumerator1;
                IEnumerator enumerator2;
                bool _boundingBox;
                int hasbreakeder = 0;

                RigidBody someCurrentData;
                JVector currentLinearVel;
                JVector currentAngularVel;
                bool _boundingBoxer;
                JQuaternion jquat;

                JMatrix jmat;
                Vector3f[] hmdToEyeViewOffsets;

                Vector3 oculusRiftDir;
                Quaternion quatter;

                Quaternion _testQuater;




                Vector3 savingPos;
                Quaternion _testQuator;
                float xq;
                float yq;
                float zq;
                float wq;

                float yaw;
                float pitch;
                float roll;

                double randomer;

                Vector3 tester01;
                Vector3 tester00;
                Vector3 screenNormalRight;
                Vector3 screenNormalTop;
                Vector3 currentScreenPos;
                Vector3 newDirRight;
                Vector3 newDirUp;
                Matrix resulter;

                Matrix matrixor;













                int _inactive_counter = 0;



                float pitcher;
                float yawer;
                float roller;

                SharpDX.Matrix[] _screenDirMatrix;




                Matrix _last_frame_left_hand_pos = Matrix.Identity;




                Vector3 _last_intersectPointLeft;
                Vector3 _last_intersectPointRight;


                //int _has_grabbed_right = 0;


                Vector3 _grabbed_offset_dir;
                float _grabbed_offset_length;

                float fraction;
                JVector _collision_against_vec;
                RigidBody _collision_against_rigid;






















                CollisionSystemPersistentSAP collision;

                int instX;
                int instY;
                int instZ;

                float offsetPosX;
                float offsetPosY;
                float offsetPosZ;
                Matrix _tempMatroxer;


                SC_cube _cube;
                SC_cube _terrain;





                Matrix _screen_swap = Matrix.Identity;





                int count;
                Matrix translationMatrix;

                RigidBody body;
                JQuaternion quatterer;
                Quaternion tester;

                Vector3 OFFSETPOS;















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





                //int hasClickedBUTTONX = 0;
                //int hasClickedBUTTONY = 0;


                DModeler _rightTouch;
                SharpDX.Matrix _rightTouchMatrix = SharpDX.Matrix.Identity;

                DModeler _leftTouch;
                SharpDX.Matrix _leftTouchMatrix = SharpDX.Matrix.Identity;







                DModelClass4_grid_Tiles[] _FloorTiles;



















                Hwnd = windowsHandle;
                _currentWriter = _writer;
                _mainThreadStopWatch.Start();


                DoWork(1);



                D3D = new SC_Console_DIRECTX();

                // Initialize the Direct3D object.
                if (!D3D.Initialize(configuration, windowsHandle, _writer))
                    return false;

                _desktopDupe = new SC_SharpDX_ScreenCapture(0, 0, D3D.Device);



                ReadKeyboard();



                WorldMatrix = Matrix.Identity;


                _world_list = new World[width * height * depth];
                _world_cube_list = new SC_cube[width * height * depth];
                _world_terrain_list = new SC_cube[width * height * depth];

                // _world_cloth_list = new SC_VR_Cloth[width * height * depth];

                worldMatrix_instances = new Matrix[width * height * depth][];

                worldMatrix_instances_cloth = new Matrix[width * height * depth][];

                _objects_static_00 = new int[width * height * depth][];
                _objects_static_counter_00 = new int[width * height * depth][];
                _objects_rigid_static_00 = new RigidBody[width * height * depth][];


                _some_frame_counter_raycast_00 = new int[width * height * depth][];
                _some_frame_counter_raycast_01 = new int[width * height * depth][];

                _switch_for_collision = new int[width * height * depth][];

                _some_last_frame_vector = new JVector[width * height * depth][][];
                _some_last_frame_rigibodies = new RigidBody[width * height * depth][][];

                // Create the camera object
                Camera = new DCamera();

                // Set the initial position of the camera.
                //Camera.SetPosition(0, 0, -10);

                RotationX = 0;
                RotationY = 0;
                RotationZ = 0;

                pitch = (float)(RotationX * 0.0174532925f);
                yaw = (float)(RotationY * 0.0174532925f);
                roll = (float)(RotationZ * 0.0174532925f);

                originRot = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                _shaderManager = new DShaderManager();
                _shaderManager.Initialize(D3D.Device, windowsHandle);

          

                _grid_X = new DTerrain();
                _grid_X.Initialize(D3D.Device, 10, 10, 1);

                _grid_Y = new DTerrain();
                _grid_Y.Initialize(D3D.Device, 10, 10, 1);

                _grid_Z = new DTerrain();
                _grid_Z.Initialize(D3D.Device, 10, 10, 1);



                int _dvX = 10;
                int _dvY = 10;

                float _size_one = 0.5f;
                float _size_two = 0.01f;
                float _size_screen = 0.0006f;


                int sizeWidther = (int)(configuration.Width * _size_one);
                int sizeHeighter = (int)(configuration.Height * _size_one);


                //int _divisionWidth = (int)Math.Round(sizeWidther * _size_two);
                //int _divisionHeight = (int)Math.Round(sizeHeighter * _size_two);

                //float _divisionWidthF = (float)Math.Round(configuration.Width * _size_screen);
                //float _divisionHeightF = (float)Math.Round(configuration.Height * _size_screen);
                //float _divisionDepthF = (float)Math.Round(configuration.Height * _size_screen);

                //Vector3 _tester = new Vector3(_divisionWidthF, _divisionHeightF, _divisionDepthF);




                //_screen_grid_X = new DTerrain_Screen();
                //_screen_grid_X.Initialize(D3D.Device, _divisionWidth, _divisionHeight, _size_screen);

                float a = 1;
                float r = 0.15f;
                float g = 0.65f;
                float b = 0.15f;

                _screen_grid_Y = new DTerrain_Screen();
                _screen_grid_Y.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, _size_screen, _dvX, _dvY, a, r, g, b);

                //_screen_grid_Z = new DTerrain_Screen();
                //_screen_grid_Z.Initialize(D3D.Device, _divisionWidth, _divisionHeight, _size_screen);


                a = 1;
                r = 0.65f;
                g = 0.15f;
                b = 0.15f;
                _size_screen = 0.0006f;




                _screen_metric_grid_Y = new DTerrain_Screen_Metric();
                _screen_metric_grid_Y.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, _size_screen, _dvX, _dvY, a, r, g, b);





                a = 1;
                r = 0.65f;
                g = 0.15f;
                b = 0.15f;

                //_WORLD_GRID_X = new DTerrain_Screen_Metric();
                //_WORLD_GRID_X.Initialize(D3D, 100, 100, 0.1f, 2, 2, a, r, g, b);


                //_screenDirMatrix_correct_pos = new Matrix[4];











                //_screen_grid_Y = new DTerrain_Screen();
                //_screen_grid_Y.Initialize(D3D.Device, _divisionWidth, _divisionHeight );







                //screen dims => 1920 * 1080
                //test => 1/1920 = 5.2083333333333333333333333333333e-4
                //test => 
                //test =>
                //test =>
                //test =>
                //test =>
                //test =>
                //test =>





                bool _hasinit0;



















                //Console.WriteLine(_screenCorners.Length);




                _intersectTouchRight = new DModeler();
                _intersectTouchRight.Initialize(D3D.Device, 0.005f, 0.005f, 0.005f);

                _intersectTouchLeft = new DModeler();
                _intersectTouchLeft.Initialize(D3D.Device, 0.005f, 0.005f, 0.005f);





                _updateFunctionStopwatchRight = new Stopwatch();
                //_updateFunctionStopwatchLeft = new Stopwatch();

                //_updateFunctionStopwatchTouchRightButtonA = new Stopwatch();

                //_updateFunctionStopwatchLeftHandTrigger = new Stopwatch();
                //_updateFunctionStopwatchRightHandTrigger = new Stopwatch();

                //_updateFunctionStopwatchLeftThumbstickGoRight = new Stopwatch();
                //_updateFunctionStopwatchLeftThumbstickGoLeft = new Stopwatch();

                //_updateFunctionStopwatchRightIndexTrigger = new Stopwatch();
                //_updateFunctionStopwatchLeftIndexTrigger = new Stopwatch();

                _updateFunctionStopwatchRightThumbstickGoLeft = new Stopwatch();
                _updateFunctionStopwatchRightThumbstickGoRight = new Stopwatch();


                //_updateFunctionStopwatchRightThumbstick = new Stopwatch();
                _updateFunctionStopwatchLeftThumbstick = new Stopwatch();





                /*//IF ALONG X AXIS => i am not sure but i think i can use this EVEN if the rotation would happen in the 3 axis. I just have to separately apply the rotations on each axis. pretty cool
                Vector3 pointOne = new Vector3(_screenModel.vertices[0].position.X, _screenModel.vertices[0].position.Y, _screenModel.vertices[0].position.Z);
                Vector3 midPosLeft = (_screenModel.vertices[1].position) - (_screenModel.vertices[0].position);
                midPosLeft *= 0.5f;
                Vector3 middleOffsetCenterRot = (_screenModel.vertices[0].position + midPosLeft);
                Vector2 rotatePoint = new Vector2(pointOne.Y, pointOne.Z);
                Vector2 centerPointer = new Vector2(middleOffsetCenterRot.Y, middleOffsetCenterRot.Z);
                Vector2 rotatedPoint = RotatePoint(rotatePoint, centerPointer, ((float)RotationScreenX));
                _screenDirMatrix[0] = rotatingMatrixScreen;
                _screenDirMatrix[0].M41 = pointOne.X;
                _screenDirMatrix[0].M42 = rotatedPoint.X;
                _screenDirMatrix[0].M43 = rotatedPoint.Y;
                _screenDirMatrix[0].M41 += originPosScreen.X;
                _screenDirMatrix[0].M42 += originPosScreen.Y;
                _screenDirMatrix[0].M43 += originPosScreen.Z;
                //hasClickedBUTTONY = 1;

                Vector3 pointTwo = new Vector3(_screenModel.vertices[1].position.X, _screenModel.vertices[1].position.Y, _screenModel.vertices[1].position.Z);
                Vector3 midPosLeftTwo = (_screenModel.vertices[0].position) - (_screenModel.vertices[1].position);
                midPosLeftTwo *= 0.5f;
                Vector3 middleOffsetCenterRotTwo = (_screenModel.vertices[1].position + midPosLeftTwo);
                Vector2 rotatePointTwo = new Vector2(pointTwo.Y, pointTwo.Z);
                Vector2 centerPointerTwo = new Vector2(middleOffsetCenterRotTwo.Y, middleOffsetCenterRotTwo.Z);
                Vector2 rotatedPointTwo = RotatePoint(rotatePointTwo, centerPointerTwo, ((float)RotationScreenX));
                _screenDirMatrix[1] = rotatingMatrixScreen;
                _screenDirMatrix[1].M41 = pointTwo.X;
                _screenDirMatrix[1].M42 = rotatedPointTwo.X;
                _screenDirMatrix[1].M43 = rotatedPointTwo.Y;
                _screenDirMatrix[1].M41 += originPosScreen.X;
                _screenDirMatrix[1].M42 += originPosScreen.Y;
                _screenDirMatrix[1].M43 += originPosScreen.Z;

                Vector3 pointThree = new Vector3(_screenModel.vertices[2].position.X, _screenModel.vertices[2].position.Y, _screenModel.vertices[2].position.Z);
                Vector3 midPosLeftThree = (_screenModel.vertices[3].position) - (_screenModel.vertices[2].position);
                midPosLeftThree *= 0.5f;
                Vector3 middleOffsetCenterRotThree = (_screenModel.vertices[2].position + midPosLeftThree);
                Vector2 rotatePointThree = new Vector2(pointThree.Y, pointThree.Z);
                Vector2 centerPointerThree = new Vector2(middleOffsetCenterRotThree.Y, middleOffsetCenterRotThree.Z);
                Vector2 rotatedPointThree = RotatePoint(rotatePointThree, centerPointerThree, ((float)RotationScreenX));
                _screenDirMatrix[2] = rotatingMatrixScreen;
                _screenDirMatrix[2].M41 = pointThree.X;
                _screenDirMatrix[2].M42 = rotatedPointThree.X;
                _screenDirMatrix[2].M43 = rotatedPointThree.Y;
                _screenDirMatrix[2].M41 += originPosScreen.X;
                _screenDirMatrix[2].M42 += originPosScreen.Y;
                _screenDirMatrix[2].M43 += originPosScreen.Z;
                */







                _mouseCursor = new DModeler();
                _mouseCursor.Initialize(D3D.Device, 0.05f, 0.05f, 0.05f);











                oriRotationScreenX = 0;
                oriRotationScreenY = 0;
                oriRotationScreenZ = 0;

                RotationScreenX = oriRotationScreenX;
                RotationScreenY = oriRotationScreenY;
                RotationScreenZ = oriRotationScreenZ;

                pitcher = oriRotationScreenX * 0.0174532925f;
                yawer = oriRotationScreenY * 0.0174532925f;
                roller = oriRotationScreenZ * 0.0174532925f;

                originRotScreen = SharpDX.Matrix.RotationYawPitchRoll(yawer, pitcher, roller);
                rotatingMatrixScreen = SharpDX.Matrix.RotationYawPitchRoll(yawer, pitcher, roller);





                float oriRotationScreenX0 = 0;
                float oriRotationScreenY0 = 180;
                float oriRotationScreenZ0 = 0;

                pitcher = oriRotationScreenX0 * 0.0174532925f;
                yawer = oriRotationScreenY0 * 0.0174532925f;
                roller = oriRotationScreenZ0 * 0.0174532925f;

                _direction_offsetter = SharpDX.Matrix.RotationYawPitchRoll(yawer, pitcher, roller);




                _size_screen = 0.0006f;

                sizeWidtherer = (float)(((float)D3D.SurfaceWidth * 0.5f) * _size_screen);
                sizeheighterer = (float)((float)(D3D.SurfaceHeight * 0.5f) * _size_screen);













































                Vector3 _offsetPos;

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int z = 0; z < depth; z++)
                        {


                            //JITTER PHYSICS
                            collision = new CollisionSystemPersistentSAP();
                            World = new World(collision);
                            World.AllowDeactivation = true;

                            World.Gravity = new JVector(0, -9.81f, 0);
                            World.SetIterations(10, 10);
                            World.ContactSettings.AllowedPenetration = 0.001f;



                            //_World = World;
                            _world_list[x + width * (y + height * z)] = World;

                            _offsetPos = new Vector3(x * 10, 0, z * 10);






                            //HUMAN IK RIG
                            _size_screen = 0.0006f;

                            r = 0.05f;
                            g = 0.05f;
                            b = 0.05f;
                            a = 1;

                            instX = 1;
                            instY = 1;
                            instZ = 1;

                            offsetPosX = 0;
                            offsetPosY = 0;
                            offsetPosZ = 0;

                            _tempMatroxer = Matrix.Identity;

                            _tempMatroxer = WorldMatrix;
                            _tempMatroxer.M41 = 0;
                            _tempMatroxer.M42 = 0;
                            _tempMatroxer.M43 = 0;
                            _tempMatroxer.M44 = 1;

                            _SC_visual_object_manager = new SC_visual_object_manager();
                            _SC_visual_object_manager._create_human_rig(D3D, Hwnd, World, _tempMatroxer, _size_screen, r, g, b, a, instX, instY, instZ, offsetPosX, offsetPosY, offsetPosZ);












                            //PHYSICS FALLING CUBES
                            r = 0.045f;
                            g = 0.045f;
                            b = 0.045f;
                            a = 1;

                            instX = 10;
                            instY = 10;
                            instZ = 10;

                            offsetPosX = 1; // 1
                            offsetPosY = 1; // 1
                            offsetPosZ = 1; // 1

                            RotationX = 0;
                            RotationY = 0;
                            RotationZ = 0;

                            pitch = (float)(RotationX * 0.0174532925f);
                            yaw = (float)(RotationY * 0.0174532925f);
                            roll = (float)(RotationZ * 0.0174532925f);

                            originRot = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                            //tsk = DoWork(1);
                            _tempMatroxer = Matrix.Identity;

                            _tempMatroxer = WorldMatrix;
                            _tempMatroxer.M41 = -(instX * offsetPosX) * 0.5f; //0.5f
                            _tempMatroxer.M42 = 2.5f;
                            _tempMatroxer.M43 = -(instZ * offsetPosZ) * 0.5f; //0.5f
                            _tempMatroxer.M44 = 1;


                            _tempMatroxer.M41 += _offsetPos.X;
                            _tempMatroxer.M42 += _offsetPos.Y;
                            _tempMatroxer.M43 += _offsetPos.Z;

                            _cube = new SC_cube();

                            worldMatrix_instances[x + width * (y + height * z)] = new Matrix[instX * instY * instZ];

                            _objects_static_00[x + width * (y + height * z)] = new int[instX * instY * instZ];
                            _objects_static_counter_00[x + width * (y + height * z)] = new int[instX * instY * instZ];
                            _objects_rigid_static_00[x + width * (y + height * z)] = new RigidBody[instX * instY * instZ];
                            //worldMatrix_instances.Add(matrixered);

                            _hasinit0 = _cube.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, _size_screen, 1, 1, 0.1f, 0.1f, 0.1f, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 1, offsetPosX, offsetPosY, offsetPosZ); //, "terrainGrassDirt.bmp" //0.00015f
                            _world_cube_list[x + width * (y + height * z)] = _cube;




                            _some_frame_counter_raycast_00[x + width * (y + height * z)] = new int[instX * instY * instZ];
                            _some_frame_counter_raycast_01[x + width * (y + height * z)] = new int[instX * instY * instZ];
                            _switch_for_collision[x + width * (y + height * z)] = new int[instX * instY * instZ];
                            _some_last_frame_vector[x + width * (y + height * z)] = new JVector[instX * instY * instZ][];
                            _some_last_frame_rigibodies[x + width * (y + height * z)] = new RigidBody[instX * instY * instZ][];







                            for (int i = 0; i < _some_frame_counter_raycast_00[x + width * (y + height * z)].Length; i++)
                            {
                                _objects_rigid_static_00[x + width * (y + height * z)][i] = null;
                                _objects_static_00[x + width * (y + height * z)][i] = 0;
                                _objects_static_counter_00[x + width * (y + height * z)][i] = 0;
                                worldMatrix_instances[x + width * (y + height * z)][i] = Matrix.Identity;

                                _randNum = new Random();
                                randomer = _randNum.NextDouble(0, _some_frame_counter_raycast_00_max_counter);

                                _some_frame_counter_raycast_00[x + width * (y + height * z)][i] = (int)randomer;

                                _randNum = new Random();
                                randomer = _randNum.NextDouble(0, _some_frame_counter_raycast_01_max_counter);
                                _some_frame_counter_raycast_01[x + width * (y + height * z)][i] = (int)randomer;


                                //_randNum = new Random();
                                //randomer = _randNum.NextDouble(0, _some_frame_counter_raycast_01_max_counter);
                                //_switch_for_collision[x + width * (y + height * z)][i] = (int)randomer;
                                _switch_for_collision[x + width * (y + height * z)][i] = 0;
                                //_some_last_frame_vector[x + width * (y + height * z)][i] = JVector.Zero;
                                _some_last_frame_rigibodies[x + width * (y + height * z)][i] = null;
                            }






                            //TERRAIN
                            r = 0.025f;
                            g = 0.025f;
                            b = 0.025f;
                            a = 1;

                            instX = 1;
                            instY = 1;
                            instZ = 1;

                            worldMatrix_Terrain_instances = new Matrix[1];
                            _tempMatroxer = Matrix.Identity;
                            _tempMatroxer = WorldMatrix;

                            _tempMatroxer.M41 = 0;
                            _tempMatroxer.M42 = -5;
                            _tempMatroxer.M43 = 0;
                            _tempMatroxer.M44 = 1;

                            _offsetPos = new Vector3(x * 10, 0, z * 10);

                            _tempMatroxer.M41 += _offsetPos.X;
                            _tempMatroxer.M42 += _offsetPos.Y;
                            _tempMatroxer.M43 += _offsetPos.Z;

                            offsetPosX = 0;
                            offsetPosY = 0;
                            offsetPosZ = 0;

                            _terrain = new SC_cube();
                            _hasinit0 = _terrain.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, _size_screen, 1, 1, 100, 5, 100, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ); //, "terrainGrassDirt.bmp" //0.00015f
                            _world_terrain_list[x + width * (y + height * z)] = _terrain;








                            r = 1;
                            g = 1;
                            b = 1;
                            a = 1;


                            //_pseudo_cloth = new PseudoCloth(D3D.device, World, 10, 10, 1, 1, new Vector4(r, g, b, a));



                            _tempMatroxer = Matrix.Identity;
                            _tempMatroxer = WorldMatrix;

                            _tempMatroxer.M41 = -0.55f;
                            _tempMatroxer.M42 = 0.5f;
                            _tempMatroxer.M43 = -0.55f;
                            _tempMatroxer.M44 = 1;


                            _pseudo_cloth = new PseudoCloth(World, D3D, windowsHandle, 7, 7, 1, 0.1f, _tempMatroxer);
                            //worldMatrix_Cloth_instances = new Matrix[10 * 10];
                            worldMatrix_instances_cloth[x + width * (y + height * z)] = new Matrix[_pseudo_cloth.bodies.Length];


                            for (int i = 0; i < worldMatrix_instances_cloth[x + width * (y + height * z)].Length; i++)
                            {
                                worldMatrix_instances_cloth[x + width * (y + height * z)][i] = Matrix.Identity;
                            }









                            /*_tempMatroxer.M41 = 0;
                            _tempMatroxer.M42 = -1;
                            _tempMatroxer.M43 = 0;
                            _tempMatroxer.M44 = 1;

                            float posX = 0;
                            float posY = 0;
                            float posZ = 0;

                            clother = new PseudoCloth(D3D.device, 1, 1, 1, new Vector4(1, 0.1f, 0.1f, 1), 10, 1, 10, posX, posY, posZ, World, _tempMatroxer);
                            */


                            /*//CLOTH
                            r = 0.025f;
                            g = 0.025f;
                            b = 0.025f;
                            a = 1;

                            instX = 1;
                            instY = 1;
                            instZ = 1;

                            float posX = 0;
                            float posY = 0;
                            float posZ = 0;

                            _tempMatroxer = Matrix.Identity;
                            _tempMatroxer = WorldMatrix;

                            _tempMatroxer.M41 = 0;
                            _tempMatroxer.M42 = 10;
                            _tempMatroxer.M43 = 0;
                            _tempMatroxer.M44 = 1;

                            clothRect = new SC_VR_Cloth();
                            //clothRect.Initialize(D3D.device, 0.045f, 0.0035f, 0.045f, new Vector4(1, 0.1f, 0.1f, 1), 10, 1, 10, posX, posY, posZ, _tempMatroxer, World);
                            _hasinit0 = clothRect.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, _size_screen, 1, 1, 5, 0.5f, 1, new Vector4(r, g, b, a), instX, instY, instZ, Hwnd, _tempMatroxer, 0, offsetPosX, offsetPosY, offsetPosZ,World); //, "terrainGrassDirt.bmp" //0.00015f
                            _world_cloth_list[x + width * (y + height * z)] = clothRect;
                            worldMatrix_instances_cloth[x + width * (y + height * z)] = new Matrix[instX * instY * instZ];

                            for (int i = 0; i < worldMatrix_instances_cloth[x + width * (y + height * z)].Length; i++)
                            {
                                worldMatrix_instances_cloth[x + width * (y + height * z)][i] = Matrix.Identity;
                            }*/




                            /*clothRect.transform.Component.softbody.Pressure = 15; //0.00075f

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






                            /*_task_list[x + width * (y + height * z)] = Task<object[]>.Factory.StartNew((tester00001) =>
                            {
                                while (true)
                                {

                                    if (_world_list[x + width * (y + height * z)] != null)
                                    {
                                        _World_Step = DateTime.Now.Second;//startTime.Now.Second; //deltaTime;//
                                        if (_World_Step > 1.0f * 0.01f)
                                        {
                                            _World_Step = 1.0f * 0.01f;
                                        }
                                        _world_list[x + width * (y + height * z)].Step(_World_Step, true);
                                    }

                                    if (_world_list[x + width * (y + height * z)] != null)
                                    {
                                        //_World = (World)tester001[0];

                                        if (_world_list[x + width * (y + height * z)].RigidBodies.Count > 0)
                                        {
                                            count = 0;

                                            enumerator = _world_list[x + width * (y + height * z)].RigidBodies.GetEnumerator();

                                            while (enumerator.MoveNext())
                                            {
                                                body = (RigidBody)enumerator.Current;

                                                if (body != null && body.Tag != null)
                                                {
                                                    if ((SC_Console_DIRECTX.BodyTag)body.Tag == SC_Console_DIRECTX.BodyTag.physicsInstancedCube)
                                                    {
                                                        Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);

                                                        quatterer = JQuaternion.CreateFromMatrix(body.Orientation);

                                                        tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

                                                        Matrix.RotationQuaternion(ref tester, out rotationMatrix);

                                                        Matrix.Multiply(ref rotationMatrix, ref translationMatrix, out translationMatrix);
                                                        worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix;




                                                        count++;
                                                    }
                                                    else if ((SC_Console_DIRECTX.BodyTag)body.Tag == SC_Console_DIRECTX.BodyTag.Terrain)
                                                    {
                                                        translationMatrix = _world_terrain_list[x + width * (y + height * z)]._POSITION;
                                                        Quaternion.RotationMatrix(ref translationMatrix, out tester);

                                                        JQuaternion jquat = new JQuaternion(tester.X, tester.Y, tester.Z, tester.W);

                                                        JMatrix jmat = JMatrix.CreateFromQuaternion(jquat);

                                                        //_terrain._singleObjectOnly.transform.Component.rigidbody.Orientation = jmat;
                                                        //_terrain._singleObjectOnly.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                        _world_terrain_list[x + width * (y + height * z)]._singleObjectOnly.transform.Component.rigidbody.Orientation = jmat;
                                                        _world_terrain_list[x + width * (y + height * z)]._singleObjectOnly.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                    }
                                                    if (//body.Is == false)
                                                    {

                                                    }
                                                }
                                            }
                                        }
                                    }

                                    Thread.Sleep(1);
                                }
                            }, tester001);*/
                        }
                    }
                }



                //_WorldMatrix = WorldMatrix;
                //clothRect = new SC_VR_Cloth();
                //clothRect.Initialize(D3D.device, 0.05f, 0.05f, 0.05f, new Vector4(1, 1, 1, 1), 10, 1, 10, World);
                //World.AddBody(clothRect.transform.Component.softbody);




                /*for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int z = 0; z < depth; z++)
                        {
                            _world_list[x + width * (y + height * z)].AddBody(clothRect.transform.Component.softbody);
                        }
                    }
                }*/






                //World.AddBody(clothRect.transform.Component.softbody);



            

















                oriRotationScreenX = 0;
                oriRotationScreenY = 0;
                oriRotationScreenZ = 0;

                RotationScreenX = oriRotationScreenX;
                RotationScreenY = oriRotationScreenY;
                RotationScreenZ = oriRotationScreenZ;

                pitcher = oriRotationScreenX * 0.0174532925f;
                yawer = oriRotationScreenY * 0.0174532925f;
                roller = oriRotationScreenZ * 0.0174532925f;

                originRotScreen = SharpDX.Matrix.RotationYawPitchRoll(yawer, pitcher, roller);
                rotatingMatrixScreen = SharpDX.Matrix.RotationYawPitchRoll(yawer, pitcher, roller);


                _size_screen = 0.0006f;

                r = 0.05f;
                g = 0.05f;
                b = 0.05f;
                a = 1;

                instX = 1;
                instY = 1;
                instZ = 1;

                offsetPosX = 0;
                offsetPosY = 0;
                offsetPosZ = 0;

                _tempMatroxer = Matrix.Identity;

                _tempMatroxer = WorldMatrix;
                _tempMatroxer.M41 = 0;
                _tempMatroxer.M42 = 0;
                _tempMatroxer.M43 = 0;
                _tempMatroxer.M44 = 1;

                _screenModel = new DModelClass2();
                _hasinit0 = _screenModel.Initialize(D3D.device, D3D.SurfaceWidth, D3D.SurfaceHeight, _size_screen, _tempMatroxer, D3D.SurfaceWidth, D3D.SurfaceHeight, 1, offsetPosX, offsetPosY, offsetPosZ); //, "terrainGrassDirt.bmp" //0.00015f










                //MessageBox((IntPtr)0, _hasinit0 + "", "Oculus error", 0);

                _basicTexture = new DTexture();
                bool _hasinit1 = _basicTexture.Initialize(D3D.Device, "../../../terrainGrassDirt.bmp");

                _rightTouch = new DModeler();
                _rightTouch.Initialize(D3D.Device, 0.05f, 0.05f, 0.05f);

                RotationTouchRightX = oriRotationScreenX;
                RotationTouchRightY = oriRotationScreenY;
                RotationTouchRightZ = oriRotationScreenZ;

                _leftTouch = new DModeler();
                _leftTouch.Initialize(D3D.Device, 0.05f, 0.05f, 0.05f);

                RotationTouchLeftX = oriRotationScreenX;
                RotationTouchLeftY = oriRotationScreenY;
                RotationTouchLeftZ = oriRotationScreenZ;

                _screenCorners = new DModelClass4_cube[4];

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

                _screenDirMatrix[0].M41 = _screenModel.vertices[16].position.X + originPosScreen.X;
                _screenDirMatrix[0].M42 = _screenModel.vertices[16].position.Y + originPosScreen.Y;
                _screenDirMatrix[0].M43 = _screenModel.vertices[16].position.Z + originPosScreen.Z;

                _screenDirMatrix[1].M41 = _screenModel.vertices[13].position.X + originPosScreen.X;
                _screenDirMatrix[1].M42 = _screenModel.vertices[13].position.Y + originPosScreen.Y;
                _screenDirMatrix[1].M43 = _screenModel.vertices[13].position.Z + originPosScreen.Z;

                _screenDirMatrix[2].M41 = _screenModel.vertices[15].position.X + originPosScreen.X;
                _screenDirMatrix[2].M42 = _screenModel.vertices[15].position.Y + originPosScreen.Y;
                _screenDirMatrix[2].M43 = _screenModel.vertices[15].position.Z + originPosScreen.Z;

                _screenDirMatrix[3].M41 = _screenModel.vertices[17].position.X + originPosScreen.X;
                _screenDirMatrix[3].M42 = _screenModel.vertices[17].position.Y + originPosScreen.Y;
                _screenDirMatrix[3].M43 = _screenModel.vertices[17].position.Z + originPosScreen.Z;


                for (int i = 0; i < _screenDirMatrix.Length; i++)
                {
                    point3DCollection[i] = new Vector3(_screenDirMatrix[i].M41, _screenDirMatrix[i].M42, _screenDirMatrix[i].M43);
                }








                /*lowestX = point3DCollection.OrderBy(x => x.X).FirstOrDefault();
                highestX = point3DCollection.OrderBy(x => x.X).Last();

                lowestY = point3DCollection.OrderBy(y => y.X).FirstOrDefault();
                highestY = point3DCollection.OrderBy(y => y.X).Last();

                lowestZ = point3DCollection.OrderBy(z => z.X).FirstOrDefault();
                highestZ = point3DCollection.OrderBy(z => z.X).Last();
                */














                for (int i = 0; i < _screenCorners.Length; i++)
                {
                    _screenCorners[i] = new DModelClass4_cube();
                    _screenCorners[i].Initialize(D3D.Device, 0.01f, 0.01f, 0.01f);
                }

                _FloorTiles = new DModelClass4_grid_Tiles[4];

                for (int i = 0; i < _FloorTiles.Length; i++)
                {
                    _FloorTiles[i] = new DModelClass4_grid_Tiles();
                    _FloorTiles[i].Initialize(D3D.Device, 0.1f, 0.1f, 0.1f, 2, 1, 10);
                }























                _has_started_vr = 1;

                return true;
            }
            catch
            {
                return false;
            }
        }
        public void ShutDown()
        {
            // Release the camera object.
            Camera = null;
            // Release the Direct3D object.
            D3D?.ShutDown();
            D3D = null;
        }


        Stopwatch _SystemTickPerformance = new Stopwatch();
        int _failed = 0;

        SC_Console_WRITER._messager _messager = new SC_Console_WRITER._messager();


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


        int textureIndex;



        SharpDX.Vector3 eyePos;

        SharpDX.Matrix eyeQuaternionMatrix;
        SharpDX.Matrix finalRotationMatrix;

        Vector3 lookUp;
        Vector3 lookAt;

        //var test = Vector3.TransformCoordinate(lookAt, rotatingMatrix);

        Vector3 viewPosition;
        Matrix viewMatrix;

        Matrix _projectionMatrix;

        SharpDX.Matrix _WorldMatrix;

        double displayMidpoint;
        TrackingState trackingState;
        Posef[] eyePoses = new Posef[2];
        EyeType eye;
        EyeTexture eyeTexture;


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

        int j;
        bool latencyMark = false;
        TrackingState trackState;
        PoseStatef poseStatefer;
        Posef hmdPose;
        Quaternionf hmdRot;
        Vector3 _hmdPoser;



        Posef handPoseLeft;
        SharpDX.Quaternion _leftTouchQuat;
        Posef handPoseRight;
        SharpDX.Quaternion _rightTouchQuat;









        Vector3 vert0;
        Vector3 vert1;
        Vector3 vert2;

        Vector3 vecOne;
        Vector3 vecTwo;
        Vector3 crossProd;

        Vector3 screenNormal;
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

        Matrix matroxer2;


        int _hasLockedMouse = 0;
        int _switchingLockedMouse = 1;
        int _lockedFrameCounter = 0;

        /*Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
        {
            //Vector3 dir = point - pivot; // get point direction relative to pivot
            //dir = Quaternion.Euler(angles) * dir; // rotate it
            //point = dir + pivot; // calculate rotated point
            //return point; // return it

            Vector3 dir = point - pivot;
            return point;
        }*/


        /*public  Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
        {
            return rotation * (point - pivot) + pivot;
        }*/
        public SC_object_messenger_dispatcher.SC_message_object[] FrameVRTWO(SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject0)
        {
            // Render the graphics scene.
            return Render(_someReceivedObject0);
        }


        int _last_frame_init = 0;

        Matrix _leftTouchMatrix = Matrix.Identity;
        Matrix _rightTouchMatrix = Matrix.Identity;
        private SC_object_messenger_dispatcher.SC_message_object[] Render(SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject0)
        {
            if (_has_started_vr == 1)
            {


                if (_lockedFrameCounter >= 50)
                {
                    /*if (Program._KeyboardState.PressedKeys.Contains(Key.L))
                    {
                        _hasLockedMouse = 1;
                    }
                    else
                    {
                        _hasLockedMouse = 0;
                    }*/
                    _lockedFrameCounter = 0;
                }

                _lockedFrameCounter++;






                // Clear the buffer to begin the scene.
                //D3D.BeginScene(0.1f, 0.25f, 0.5f, 1f, _someReceivedObject0);
                //Camera.Render();

                if (_updateFunctionBoolRight)
                {
                    _updateFunctionStopwatchRight.Stop();
                    _updateFunctionStopwatchRight.Reset();
                    _updateFunctionStopwatchRight.Start();
                    _updateFunctionBoolRight = false;
                }

                /*if (_updateFunctionBoolLeft)
                {
                    _updateFunctionStopwatchLeft.Stop();
                    _updateFunctionStopwatchLeft.Reset();
                    _updateFunctionStopwatchLeft.Start();
                    _updateFunctionBoolLeft = false;
                }

                if (_updateFunctionBoolLeftHandTrigger)
                {
                    _updateFunctionStopwatchLeftHandTrigger.Stop();
                    _updateFunctionStopwatchLeftHandTrigger.Reset();
                    _updateFunctionStopwatchLeftHandTrigger.Start();
                    _updateFunctionBoolLeftHandTrigger = false;
                }

                if (_updateFunctionBoolRightHandTrigger)
                {
                    _updateFunctionStopwatchRightHandTrigger.Stop();
                    _updateFunctionStopwatchRightHandTrigger.Reset();
                    _updateFunctionStopwatchRightHandTrigger.Start();
                    _updateFunctionBoolRightHandTrigger = false;
                }

                if (_updateFunctionBoolLeftThumbStickGoRight)
                {
                    _updateFunctionStopwatchLeftThumbstickGoRight.Stop();
                    _updateFunctionStopwatchLeftThumbstickGoRight.Reset();
                    _updateFunctionStopwatchLeftThumbstickGoRight.Start();
                    _updateFunctionBoolLeftThumbStickGoRight = false;
                }

                if (_updateFunctionBoolLeftThumbStickGoLeft)
                {
                    _updateFunctionStopwatchLeftThumbstickGoLeft.Stop();
                    _updateFunctionStopwatchLeftThumbstickGoLeft.Reset();
                    _updateFunctionStopwatchLeftThumbstickGoLeft.Start();
                    _updateFunctionBoolLeftThumbStickGoLeft = false;
                }

                if (_updateFunctionBoolRightIndexTrigger)
                {
                    _updateFunctionStopwatchRightIndexTrigger.Stop();
                    _updateFunctionStopwatchRightIndexTrigger.Reset();
                    _updateFunctionStopwatchRightIndexTrigger.Start();
                    _updateFunctionBoolRightIndexTrigger = false;
                }

                if (_updateFunctionBoolLeftIndexTrigger)
                {
                    _updateFunctionStopwatchLeftIndexTrigger.Stop();
                    _updateFunctionStopwatchLeftIndexTrigger.Reset();
                    _updateFunctionStopwatchLeftIndexTrigger.Start();
                    _updateFunctionBoolLeftIndexTrigger = false;
                }

                if (_updateFunctionBoolTouchRightButtonA)
                {
                    _updateFunctionStopwatchTouchRightButtonA.Stop();
                    _updateFunctionStopwatchTouchRightButtonA.Reset();
                    _updateFunctionStopwatchTouchRightButtonA.Start();
                    _updateFunctionBoolTouchRightButtonA = false;
                }*/

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


                /*if (_updateFunctionBoolRightThumbStick)
                {
                    _updateFunctionStopwatchRightThumbstick.Stop();
                    _updateFunctionStopwatchRightThumbstick.Reset();
                    _updateFunctionStopwatchRightThumbstick.Start();
                    _updateFunctionBoolRightThumbStick = false;
                }*/

                if (_updateFunctionBoolLeftThumbStick)
                {
                    _updateFunctionStopwatchLeftThumbstick.Stop();
                    _updateFunctionStopwatchLeftThumbstick.Reset();
                    _updateFunctionStopwatchLeftThumbstick.Start();
                    _updateFunctionBoolLeftThumbStick = false;
                }


                float thumbstickIsRight;
                float thumbstickIsUp;

                //HEADSET POSITION
                displayMidpoint = D3D.OVR.GetPredictedDisplayTime(D3D.sessionPtr, 0);
                trackingState = D3D.OVR.GetTrackingState(D3D.sessionPtr, displayMidpoint, true);
                latencyMark = false;
                trackState = D3D.OVR.GetTrackingState(D3D.sessionPtr, 0.0f, latencyMark);
                poseStatefer = trackState.HeadPose;
                hmdPose = poseStatefer.ThePose;
                hmdRot = hmdPose.Orientation;
                _hmdPoser = new Vector3(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);

                //SET CAMERA POSITION
                Camera.SetPosition(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);

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
                _leftTouchMatrix.M41 = handPoseLeft.Position.X + originPos.X + movePos.X;// + _hmdPoser.X;
                _leftTouchMatrix.M42 = handPoseLeft.Position.Y + originPos.Y + movePos.Y;// + _hmdPoser.Y;
                _leftTouchMatrix.M43 = handPoseLeft.Position.Z + originPos.Z + movePos.Z;// + _hmdPoser.Z;



                //Console.WriteLine(buttonPressedOculusTouchLeft.ToString());



                if (handTriggerRight[1] >= 0.01f)
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
                    if (_grabbed_body_right != null)
                    {
                        _grabbed_body_right.AffectedByGravity = true;
                        _grabbed_body_right.IsActive = true;
                        _grabbed_body_right.IsStatic = false;
                        _grabbed_body_right = null;
                    }

                    RotationGrabbedX = 0;
                    RotationGrabbedY = 0;
                    RotationGrabbedZ = 0;

                    _swtch_hasRotated = 0;
                    _has_grabbed_right = 0;
                    _has_grabbed_right_swtch = 0;
                    _sec_logic_swtch_grab = 0;
                    _tier_logic_swtch_grab = 0;
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


                        float pitch = (float)(RotationX * 0.0174532925f);
                        float yaw = (float)(RotationY * 0.0174532925f);
                        float roll = (float)(RotationZ * 0.0174532925f);

                        rotatingMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                        pitch = (float)(RotationX4Pelvis * 0.0174532925f);
                        yaw = (float)(RotationY4Pelvis * 0.0174532925f);
                        roll = (float)(RotationZ4Pelvis * 0.0174532925f);

                        rotatingMatrixForPelvis = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                        /*if (_swtch_hasRotated == 0)
                        {
                            RotationGrabbedX = RotationGrabbedX;
                            RotationGrabbedY = RotationGrabbedY;
                            RotationGrabbedZ = RotationGrabbedZ;

                            _swtch_hasRotated = 1;
                        }
                        else if (_swtch_hasRotated == 1)
                        {
                            RotationGrabbedX = RotationGrabbedX;
                            RotationGrabbedY = RotationGrabbedY;
                            RotationGrabbedZ = RotationGrabbedZ;

                            if (_swtch_hasRotated_init_rot == 0)
                            {
                                RotationGrabbedX = RotationX4Pelvis;
                                RotationGrabbedY = RotationY4Pelvis;
                                RotationGrabbedZ = RotationZ4Pelvis;
                                _swtch_hasRotated_init_rot = 1;
                            }
                        }
                        else
                        {

                        }*/
                        if (_has_grabbed_right_swtch == 2)
                        {
                            _swtch_hasRotated = 1;
                        }
                        pitch = (float)(RotationGrabbedX * 0.0174532925f);
                        yaw = (float)(RotationGrabbedY * 0.0174532925f);
                        roll = (float)(RotationGrabbedZ * 0.0174532925f);
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




                        //RotationGrabbedX = 0;
                        //RotationGrabbedY = 0;
                        //RotationGrabbedZ = 0;








                        pitch = (float)(RotationGrabbedX * 0.0174532925f);
                        yaw = (float)(RotationGrabbedY * 0.0174532925f);
                        roll = (float)(RotationGrabbedZ * 0.0174532925f);

                        rotatingMatrixForGrabber = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);
                        if (_swtch_hasRotated == 0)
                        {
                            _sec_logic_swtch_grab = 0;
                        }



                        /*else if (_swtch_hasRotated == 2)
                        {
                            
                        }
                        else if (_swtch_hasRotated == 1)
                        {
                      
                        }*/



                    }
                    else
                    {

                    }

                }

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

                if (thumbStickLeft[0].X > 0.1f)
                {
                    movePos += direction_feet_right * speedPos * thumbStickLeft[0].X;
                }
                else if (thumbStickLeft[0].X < -0.1f)
                {
                    movePos -= direction_feet_right * speedPos * -thumbStickLeft[0].X;
                }

                if (thumbStickLeft[0].Y > 0.1f)
                {
                    movePos += direction_feet_forward * speedPos * thumbStickLeft[0].Y;
                }
                else if (thumbStickLeft[0].Y < -0.1f)
                {
                    movePos -= direction_feet_forward * speedPos * -thumbStickLeft[0].Y;
                }

                Vector3 OFFSETPOS;
                OFFSETPOS = originPos + movePos;



                try
                {

                    //_SystemTickPerformance.Stop();
                    //_SystemTickPerformance.Reset();
                    //_SystemTickPerformance.Start();


                    for (int i = 0; i < 3;)
                    {
                        _failed = 0;
                        try
                        {
                            _desktopFrame = _desktopDupe.ScreenCapture(0);
                            //Console.WriteLine("scap");
                        }
                        catch (Exception ex)
                        {
                            //Console.WriteLine(ex.ToString());
                            _desktopDupe = new SC_SharpDX_ScreenCapture(0, 0, D3D.Device);
                            _failed = 1;
                        }
                        i++;
                        if (_failed == 0)
                        {
                            //Console.WriteLine("test");
                            break;
                        }
                    }

                    //_SystemTickPerformance.Stop();







                    //MessageBox((IntPtr)0, _SystemTickPerformance.Elapsed.Ticks +"", "Oculus Error", 0);

                    /*var ticks = _SystemTickPerformance.Elapsed.Ticks;
                    _messager = new SC_Console_WRITER._messager();
                    _messager._message = ticks + "";
                    _messager._originalMsg = _messager._message;
                    _messager._messageCut = _messager._message;
                    _messager._specialMessage = 0;
                    _messager._specialMessageLineX = 0;
                    _messager._specialMessageLineY = 0;
                    _messager._orilineX = 1;
                    _messager._orilineY = 13;
                    _messager._lineX = 1;
                    _messager._lineY = 13;
                    _messager._count = 0;
                    _messager._swtch0 = 1;
                    _messager._swtch1 = 0;
                    _messager._delay = 0;
                    _messager._looping = 0;

                    SC_Console_WRITER._message_to_pass_list.Add(_messager);*/
                    //_currentWriter._message_to_pass_list.Add(_messager);
                    /*lock (SC_Console_WRITER._message_to_pass_list)
                    {

                    }*/








                    /*if (Program._KeyboardState.PressedKeys.Contains(Key.NumberPadEnter))
                    {

                        for (int x = 0; x < width; x++)
                        {
                            for (int y = 0; y < height; y++)
                            {
                                for (int z = 0; z < depth; z++)
                                {

                                    if (_world_list[x + width * (y + height * z)] != null)
                                    {
                                        if (_world_list[x + width * (y + height * z)].RigidBodies.Count > 0)
                                        {
                                            count = 0;

                                            enumerator0 = _world_list[x + width * (y + height * z)].RigidBodies.GetEnumerator();

                                            while (enumerator0.MoveNext())
                                            {
                                                body = (RigidBody)enumerator0.Current;
                                                body.IsActive = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {

                    }*/































                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            for (int z = 0; z < depth; z++)
                            {
                                for (int i = 0; i < _objects_static_00[x + width * (y + height * z)].Length; i++)
                                {
                                    if (_objects_static_00[x + width * (y + height * z)][i] == 2)
                                    {
                                        _objects_rigid_static_00[x + width * (y + height * z)][i].IsStatic = true;
                                        _objects_static_00[x + width * (y + height * z)][i] = 0;
                                    }
                                    else if (_objects_static_00[x + width * (y + height * z)][i] == 1)
                                    {
                                        _objects_rigid_static_00[x + width * (y + height * z)][i].IsStatic = false;
                                        _objects_static_00[x + width * (y + height * z)][i] = 0;
                                    }
                                }
                            }
                        }
                    }






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








                        //Console.SetCursorPosition(1, 3);
                        //Console.WriteLine(_swtch_hasRotated);















                        for (int x = 0; x < width; x++)
                        {
                            for (int y = 0; y < height; y++)
                            {
                                for (int z = 0; z < depth; z++)
                                {
                                    //x + width * (y + height * z)
                                    // DateTime.Now.Second;

                                    _World_Step = deltaTime;
                                    if (_world_list[x + width * (y + height * z)] != null)
                                    {
                                        //startTime.Now.Second; //deltaTime;//
                                        if (_World_Step > 1.0f * 0.01f)
                                        {
                                            _World_Step = 1.0f * 0.01f;
                                        }
                                        _world_list[x + width * (y + height * z)].Step(_World_Step, true);
                                    }

                                    int _inactive_counter = 0;

                                    if (_world_list[x + width * (y + height * z)] != null)
                                    {
                                        if (_world_list[x + width * (y + height * z)].RigidBodies != null && _world_list[x + width * (y + height * z)].RigidBodies.Count > 0)
                                        {
                                            int count = 0;
                                            int clothCounter = 0;
                                            //IEnumerator enumerator0 = _world_list[x + width * (y + height * z)].RigidBodies.GetEnumerator();

                                            //while (enumerator0.MoveNext())
                                            foreach (RigidBody rigidbody in _world_list[x + width * (y + height * z)].RigidBodies)
                                            {
                                       

                                                //RigidBody rigidbody = (RigidBody)enumerator0.Current;

                                                if (rigidbody != null && rigidbody.Tag != null)
                                                {
                                                    if ((SC_Console_DIRECTX.BodyTag)rigidbody.Tag == SC_Console_DIRECTX.BodyTag.physicsInstancedCube)
                                                    {
                                                        if (rigidbody.IsStatic)
                                                        {
                                                            if (rigidbody == _grabbed_body_right)
                                                            {
                                                                if (_has_grabbed_right_swtch == 2)
                                                                {
                                                                    

                                                                    //var rigibody_pos = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (_offset_grabbed_pos_norm * _offset_grabbed_pos_dist);

                                                                    var handPoser0 = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (_offset_grabbed_pos_norm * _offset_grabbed_pos_dist);

                                                                    if (_swtch_hasRotated == 0)
                                                                    {

                                                                        Matrix humanBodyRotation = originRot * rotatingMatrix * rotatingMatrixForPelvis; //originRot
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

                                                                        var someRotationFinal = rigidBodyMatrix * originRot * rotatingMatrix * rotatingMatrixForGrabber;

                                                                        //TORSO PIVOT
                                                                        Vector3 MOVINGPOINTER = new Vector3(_SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M41, _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M42, _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M43);

                                                                        //SAVING IN MEMORY THE ORIGINAL TORSO MATRIX NOT AFFECTED BY CURRENT POSITION AND ROTATION CHANGES.
                                                                        Matrix _rotMatrixer = _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION;
                                                                        Quaternion forTest;
                                                                        Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                                                        //FROM THE MATRIX OF ROTATION/POSITION, I GET THE QUATERNION OUT OF THAT AND CREATE THE DIRECTIONS THAT THE OBJECTS ARE ORIGINALLY FACING.
                                                                        var direction_feet_forward_ori_torso = _getDirection(Vector3.ForwardRH, forTest);
                                                                        var direction_feet_right_ori_torso = _getDirection(Vector3.Right, forTest);
                                                                        var direction_feet_up_ori_torso = _getDirection(Vector3.Up, forTest);

                                                                        //SINCE THE PIVOT POINT IS CURRENTLY IN THE MIDDLE OF THE TORSO, IT CANNOT ROTATE AT THAT POINT OTHERWISE, IT WONT FOLLOW THE PELVIS ROTATION LATER ON.
                                                                        //SO WE CURRENTLY ONLY OFFSET THE TORSO "MIDDLE OF SPINE APPROX" TO HALF OF THE CURRENT HEIGHT IN ORDER TO MAKE THE PIVOT POINT, APPROX WHERE THE PELVIS IS.
                                                                        Vector3 TORSOPIVOT = MOVINGPOINTER + -(direction_feet_up_ori_torso * (_SC_visual_object_manager._humRig._player_torso._total_torso_height * 0.5f));


                                                                        _rightTouchMatrix.M41 = handPoseRight.Position.X;
                                                                        _rightTouchMatrix.M42 = handPoseRight.Position.Y;
                                                                        _rightTouchMatrix.M43 = handPoseRight.Position.Z;



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

                                                                        //MOVINGPOINTER.X += OFFSETPOS.X;
                                                                        //MOVINGPOINTER.Y += OFFSETPOS.Y;
                                                                        //MOVINGPOINTER.Z += OFFSETPOS.Z;



                                                                        var _rightTouchMatrix_00 = _rightTouchMatrix;
                                                                        _rightTouchMatrix_00.M41 = 0;
                                                                        _rightTouchMatrix_00.M42 = 0;
                                                                        _rightTouchMatrix_00.M43 = 0;
                                                                        _rightTouchMatrix_00.M44 = 1;
                                                                        _rightTouchMatrix_00 = _rightTouchMatrix_00 * originRot * rotatingMatrix * rotatingMatrixForPelvis;
                                                                        Quaternion.RotationMatrix(ref _rightTouchMatrix_00, out otherQuat);

                                                                        //CONVERTING THE QUATERNION OF THAT TO THE DIRECTION OF ITS ROTATION
                                                                        var direction_feet_forward_hand = _getDirection(Vector3.ForwardRH, otherQuat);
                                                                        var direction_feet_right_hand = _getDirection(Vector3.Right, otherQuat);
                                                                        var direction_feet_up_hand = _getDirection(Vector3.Up, otherQuat);


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


                                                                        someRotationFinal = _process_stuff(originRot, rotatingMatrix, rotatingMatrixForPelvis, rotatingMatrixForGrabber, _rightTouchMatrix, rigidBodyMatrix);


                                                                        someRotationFinal.M41 = handPoser.X;
                                                                        someRotationFinal.M42 = handPoser.Y;
                                                                        someRotationFinal.M43 = handPoser.Z;

                                                                        worldMatrix_instances[x + width * (y + height * z)][count] = someRotationFinal;

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

                                                                        _last_current_hand_pos_for_d = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);

                                                                        _last_current_hand_float_for_d = (handPoser - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).Z;





                                                                        //var rigibody_pos = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (_offset_grabbed_pos_norm * _offset_grabbed_pos_dist);
                                                                        var rigibody_pos = new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z) + (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist);
                                                                        //_last_min_distX = (rigibody_pos - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).X;
                                                                        //_last_min_distY = (rigibody_pos - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).Y;
                                                                        //_last_min_distZ = (rigibody_pos - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).Z;
                                                                        _last_min_distX = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z)).X;
                                                                        _last_min_distY = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z)).Y;
                                                                        _last_min_distZ = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z)).Length() * 0.667f;

                                                                        if (_has_grabbed_right == 0)
                                                                        {
                                                                            _has_grabbed_right = 1;
                                                                        }
                                                                    }



                                                                    if (_swtch_hasRotated == 2)
                                                                    {
                                                                        //_grabbed_object_matrix = originRot * rotatingMatrix * rotatingMatrixForPelvis;
                                                                        //_grabbed_object_matrix.M41 = 0;
                                                                        //_grabbed_object_matrix.M42 = 0;
                                                                        //_grabbed_object_matrix.M43 = 0;
                                                                        //_grabbed_object_matrix.M44 = 1;


                                                                        JQuaternion quatterer02 = JQuaternion.CreateFromMatrix(_grabbed_body_pos_rot); //_grabbed_body_pos_rot

                                                                        Quaternion tester02;
                                                                        tester02.X = quatterer02.X;
                                                                        tester02.Y = quatterer02.Y;
                                                                        tester02.Z = quatterer02.Z;
                                                                        tester02.W = quatterer02.W;

                                                                        Matrix.RotationQuaternion(ref tester02, out rotationMatrix);

                                                                        _grabbed_object_matrix = rotationMatrix * _grabbed_object_matrix;

                                                                        var _rightTouchMatrix_00 = _rightTouchMatrix;
                                                                        _rightTouchMatrix_00.M41 = 0;
                                                                        _rightTouchMatrix_00.M42 = 0;
                                                                        _rightTouchMatrix_00.M43 = 0;
                                                                        _rightTouchMatrix_00.M44 = 1;
                                                                        _rightTouchMatrix_00 = _rightTouchMatrix_00 * originRot * rotatingMatrix * rotatingMatrixForPelvis;
                                                                        Quaternion.RotationMatrix(ref _rightTouchMatrix_00, out otherQuat);

                                                                        //CONVERTING THE QUATERNION OF THAT TO THE DIRECTION OF ITS ROTATION
                                                                        var direction_feet_forward_hand = _getDirection(Vector3.ForwardRH, otherQuat);
                                                                        var direction_feet_right_hand = _getDirection(Vector3.Right, otherQuat);
                                                                        var direction_feet_up_hand = _getDirection(Vector3.Up, otherQuat);


                                                                        //var handPoser = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (direction_feet_forward_hand * _last_min_distZ);
                                                                        var handPoser = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (_offset_grabbed_pos_norm * _offset_grabbed_pos_dist);// (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist);



                                                                        _grabbed_object_matrix.M41 = handPoser.X;
                                                                        _grabbed_object_matrix.M42 = handPoser.Y;
                                                                        _grabbed_object_matrix.M43 = handPoser.Z;

                                                                        //_current_hand_pos_for_d = new Vector3(_last_current_hand_pos_for_d.X, _last_current_hand_pos_for_d.Y, _last_current_hand_pos_for_d.Z);

                                                                        _current_hand_float_for_dZ = (new Vector3(_grabbed_object_matrix.M41, _grabbed_object_matrix.M42, _grabbed_object_matrix.M43) - new Vector3(_last_current_hand_pos_for_d.X, _last_current_hand_pos_for_d.Y, _last_current_hand_pos_for_d.Z)).Z;//*_last_offset_grabbed_pos_norm_dist);//.Z;// (new Vector3(_grabbed_object_matrix.M41, _grabbed_object_matrix.M42, _grabbed_object_matrix.M43) - _current_hand_pos_for_d).Length(); //                             
                                                                        _current_hand_float_for_dY = (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist).Y;
                                                                        _current_hand_float_for_dX = (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist).X;



                                                                        //Console.SetCursorPosition(1, 6);
                                                                        //Console.WriteLine(_current_hand_float_for_dZ);



                                                                        if (_tier_logic_swtch_grab == 0)
                                                                        {
                                                                            Matrix humanBodyRotation = originRot * rotatingMatrix * rotatingMatrixForPelvis; //originRot
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

                                                                            var someRotationFinal = rigidBodyMatrix * originRot * rotatingMatrix * rotatingMatrixForGrabber;

                                                                            //TORSO PIVOT
                                                                            Vector3 MOVINGPOINTER = new Vector3(_SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M41, _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M42, _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M43);

                                                                            //SAVING IN MEMORY THE ORIGINAL TORSO MATRIX NOT AFFECTED BY CURRENT POSITION AND ROTATION CHANGES.
                                                                            Matrix _rotMatrixer = _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION;
                                                                            Quaternion forTest;
                                                                            Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                                                            //FROM THE MATRIX OF ROTATION/POSITION, I GET THE QUATERNION OUT OF THAT AND CREATE THE DIRECTIONS THAT THE OBJECTS ARE ORIGINALLY FACING.
                                                                            var direction_feet_forward_ori_torso = _getDirection(Vector3.ForwardRH, forTest);
                                                                            var direction_feet_right_ori_torso = _getDirection(Vector3.Right, forTest);
                                                                            var direction_feet_up_ori_torso = _getDirection(Vector3.Up, forTest);

                                                                            //SINCE THE PIVOT POINT IS CURRENTLY IN THE MIDDLE OF THE TORSO, IT CANNOT ROTATE AT THAT POINT OTHERWISE, IT WONT FOLLOW THE PELVIS ROTATION LATER ON.
                                                                            //SO WE CURRENTLY ONLY OFFSET THE TORSO "MIDDLE OF SPINE APPROX" TO HALF OF THE CURRENT HEIGHT IN ORDER TO MAKE THE PIVOT POINT, APPROX WHERE THE PELVIS IS.
                                                                            Vector3 TORSOPIVOT = MOVINGPOINTER + -(direction_feet_up_ori_torso * (_SC_visual_object_manager._humRig._player_torso._total_torso_height * 0.5f));


                                                                            _rightTouchMatrix.M41 = handPoseRight.Position.X;
                                                                            _rightTouchMatrix.M42 = handPoseRight.Position.Y;
                                                                            _rightTouchMatrix.M43 = handPoseRight.Position.Z;



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

                                                                            //MOVINGPOINTER.X += OFFSETPOS.X;
                                                                            //MOVINGPOINTER.Y += OFFSETPOS.Y;
                                                                            //MOVINGPOINTER.Z += OFFSETPOS.Z;



                                                                            _rightTouchMatrix_00 = _rightTouchMatrix;
                                                                            _rightTouchMatrix_00.M41 = 0;
                                                                            _rightTouchMatrix_00.M42 = 0;
                                                                            _rightTouchMatrix_00.M43 = 0;
                                                                            _rightTouchMatrix_00.M44 = 1;
                                                                            _rightTouchMatrix_00 = _rightTouchMatrix_00 * originRot * rotatingMatrix * rotatingMatrixForPelvis;
                                                                            Quaternion.RotationMatrix(ref _rightTouchMatrix_00, out otherQuat);

                                                                            //CONVERTING THE QUATERNION OF THAT TO THE DIRECTION OF ITS ROTATION
                                                                            direction_feet_forward_hand = _getDirection(Vector3.ForwardRH, otherQuat);
                                                                            direction_feet_right_hand = _getDirection(Vector3.Right, otherQuat);
                                                                            direction_feet_up_hand = _getDirection(Vector3.Up, otherQuat);


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


                                                                            handPoser = MOVINGPOINTER;




                                                                            someRotationFinal = _process_stuff(originRot, rotatingMatrix, rotatingMatrixForPelvis, rotatingMatrixForGrabber, _rightTouchMatrix, rigidBodyMatrix);


                                                                            someRotationFinal.M41 = handPoser.X;
                                                                            someRotationFinal.M42 = handPoser.Y;
                                                                            someRotationFinal.M43 = handPoser.Z;

                                                                            worldMatrix_instances[x + width * (y + height * z)][count] = someRotationFinal;

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

                                                                            _last_current_hand_pos_for_d = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);

                                                                            _last_current_hand_float_for_d = (handPoser - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).Z;

                                                                        }
                                                                        else if (_tier_logic_swtch_grab == 1)
                                                                        {
                                                                            Matrix humanBodyRotation = originRot * rotatingMatrix * rotatingMatrixForPelvis; //originRot
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

                                                                            var someRotationFinal = rigidBodyMatrix * originRot * rotatingMatrix * rotatingMatrixForGrabber;

                                                                            //TORSO PIVOT
                                                                            Vector3 MOVINGPOINTER = new Vector3(_SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M41, _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M42, _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M43);

                                                                            //SAVING IN MEMORY THE ORIGINAL TORSO MATRIX NOT AFFECTED BY CURRENT POSITION AND ROTATION CHANGES.
                                                                            Matrix _rotMatrixer = _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION;
                                                                            Quaternion forTest;
                                                                            Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                                                            //FROM THE MATRIX OF ROTATION/POSITION, I GET THE QUATERNION OUT OF THAT AND CREATE THE DIRECTIONS THAT THE OBJECTS ARE ORIGINALLY FACING.
                                                                            var direction_feet_forward_ori_torso = _getDirection(Vector3.ForwardRH, forTest);
                                                                            var direction_feet_right_ori_torso = _getDirection(Vector3.Right, forTest);
                                                                            var direction_feet_up_ori_torso = _getDirection(Vector3.Up, forTest);

                                                                            //SINCE THE PIVOT POINT IS CURRENTLY IN THE MIDDLE OF THE TORSO, IT CANNOT ROTATE AT THAT POINT OTHERWISE, IT WONT FOLLOW THE PELVIS ROTATION LATER ON.
                                                                            //SO WE CURRENTLY ONLY OFFSET THE TORSO "MIDDLE OF SPINE APPROX" TO HALF OF THE CURRENT HEIGHT IN ORDER TO MAKE THE PIVOT POINT, APPROX WHERE THE PELVIS IS.
                                                                            Vector3 TORSOPIVOT = MOVINGPOINTER + -(direction_feet_up_ori_torso * (_SC_visual_object_manager._humRig._player_torso._total_torso_height * 0.5f));


                                                                            _rightTouchMatrix.M41 = handPoseRight.Position.X;
                                                                            _rightTouchMatrix.M42 = handPoseRight.Position.Y;
                                                                            _rightTouchMatrix.M43 = handPoseRight.Position.Z;



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

                                                                            //MOVINGPOINTER.X += OFFSETPOS.X;
                                                                            //MOVINGPOINTER.Y += OFFSETPOS.Y;
                                                                            //MOVINGPOINTER.Z += OFFSETPOS.Z;



                                                                            _rightTouchMatrix_00 = _rightTouchMatrix;
                                                                            _rightTouchMatrix_00.M41 = 0;
                                                                            _rightTouchMatrix_00.M42 = 0;
                                                                            _rightTouchMatrix_00.M43 = 0;
                                                                            _rightTouchMatrix_00.M44 = 1;
                                                                            _rightTouchMatrix_00 = _rightTouchMatrix_00 * originRot * rotatingMatrix * rotatingMatrixForPelvis;
                                                                            Quaternion.RotationMatrix(ref _rightTouchMatrix_00, out otherQuat);

                                                                            //CONVERTING THE QUATERNION OF THAT TO THE DIRECTION OF ITS ROTATION
                                                                            direction_feet_forward_hand = _getDirection(Vector3.ForwardRH, otherQuat);
                                                                            direction_feet_right_hand = _getDirection(Vector3.Right, otherQuat);
                                                                            direction_feet_up_hand = _getDirection(Vector3.Up, otherQuat);




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


                                                                            handPoser = MOVINGPOINTER;



                                                                            someRotationFinal = _process_stuff(originRot, rotatingMatrix, rotatingMatrixForPelvis, rotatingMatrixForGrabber, _rightTouchMatrix, rigidBodyMatrix);


                                                                            someRotationFinal.M41 = handPoser.X;
                                                                            someRotationFinal.M42 = handPoser.Y;
                                                                            someRotationFinal.M43 = handPoser.Z;

                                                                            worldMatrix_instances[x + width * (y + height * z)][count] = someRotationFinal;

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

                                                                            _last_current_hand_pos_for_d = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);

                                                                            _last_current_hand_float_for_d = (handPoser - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).Z;

                                                                        }
                                                                    }








                                                                    if (_swtch_hasRotated == 1)
                                                                    {
                                                                        Matrix humanBodyRotation = originRot * rotatingMatrix * rotatingMatrixForPelvis; //originRot
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

                                                                        var someRotationFinal = rigidBodyMatrix * originRot * rotatingMatrix * rotatingMatrixForGrabber;

                                                                        //TORSO PIVOT
                                                                        Vector3 MOVINGPOINTER = new Vector3(_SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M41, _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M42, _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M43);

                                                                        //SAVING IN MEMORY THE ORIGINAL TORSO MATRIX NOT AFFECTED BY CURRENT POSITION AND ROTATION CHANGES.
                                                                        Matrix _rotMatrixer = _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION;
                                                                        Quaternion forTest;
                                                                        Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                                                        //FROM THE MATRIX OF ROTATION/POSITION, I GET THE QUATERNION OUT OF THAT AND CREATE THE DIRECTIONS THAT THE OBJECTS ARE ORIGINALLY FACING.
                                                                        var direction_feet_forward_ori_torso = _getDirection(Vector3.ForwardRH, forTest);
                                                                        var direction_feet_right_ori_torso = _getDirection(Vector3.Right, forTest);
                                                                        var direction_feet_up_ori_torso = _getDirection(Vector3.Up, forTest);

                                                                        //SINCE THE PIVOT POINT IS CURRENTLY IN THE MIDDLE OF THE TORSO, IT CANNOT ROTATE AT THAT POINT OTHERWISE, IT WONT FOLLOW THE PELVIS ROTATION LATER ON.
                                                                        //SO WE CURRENTLY ONLY OFFSET THE TORSO "MIDDLE OF SPINE APPROX" TO HALF OF THE CURRENT HEIGHT IN ORDER TO MAKE THE PIVOT POINT, APPROX WHERE THE PELVIS IS.
                                                                        Vector3 TORSOPIVOT = MOVINGPOINTER + -(direction_feet_up_ori_torso * (_SC_visual_object_manager._humRig._player_torso._total_torso_height * 0.5f));


                                                                        _rightTouchMatrix.M41 = handPoseRight.Position.X;
                                                                        _rightTouchMatrix.M42 = handPoseRight.Position.Y;
                                                                        _rightTouchMatrix.M43 = handPoseRight.Position.Z;



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

                                                                        //MOVINGPOINTER.X += OFFSETPOS.X;
                                                                        //MOVINGPOINTER.Y += OFFSETPOS.Y;
                                                                        //MOVINGPOINTER.Z += OFFSETPOS.Z;



                                                                        var _rightTouchMatrix_00 = _rightTouchMatrix;
                                                                        _rightTouchMatrix_00.M41 = 0;
                                                                        _rightTouchMatrix_00.M42 = 0;
                                                                        _rightTouchMatrix_00.M43 = 0;
                                                                        _rightTouchMatrix_00.M44 = 1;
                                                                        _rightTouchMatrix_00 = _rightTouchMatrix_00 * originRot * rotatingMatrix * rotatingMatrixForPelvis;
                                                                        Quaternion.RotationMatrix(ref _rightTouchMatrix_00, out otherQuat);

                                                                        //CONVERTING THE QUATERNION OF THAT TO THE DIRECTION OF ITS ROTATION
                                                                        var direction_feet_forward_hand = _getDirection(Vector3.ForwardRH, otherQuat);
                                                                        var direction_feet_right_hand = _getDirection(Vector3.Right, otherQuat);
                                                                        var direction_feet_up_hand = _getDirection(Vector3.Up, otherQuat);


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

                                                                        someRotationFinal = _process_stuff(originRot, rotatingMatrix, rotatingMatrixForPelvis, rotatingMatrixForGrabber, _rightTouchMatrix, rigidBodyMatrix);

                                                                        someRotationFinal.M41 = handPoser.X;
                                                                        someRotationFinal.M42 = handPoser.Y;
                                                                        someRotationFinal.M43 = handPoser.Z;

                                                                        worldMatrix_instances[x + width * (y + height * z)][count] = someRotationFinal;

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

                                                                        _last_current_hand_pos_for_d = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);

                                                                        _last_current_hand_float_for_d = (handPoser - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).Z;

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
                                                                rigidbody.AffectedByGravity = false;
                                                                rigidbody.IsActive = false;

                                                                Matrix translationMatrix0;
                                                                Matrix.Translation(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z, out translationMatrix0);

                                                                JQuaternion quatterer0 = JQuaternion.CreateFromMatrix(rigidbody.Orientation);

                                                                Quaternion tester0;
                                                                tester0.X = quatterer0.X;
                                                                tester0.Y = quatterer0.Y;
                                                                tester0.Z = quatterer0.Z;
                                                                tester0.W = quatterer0.W;

                                                                //Quaternion tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

                                                                Matrix.RotationQuaternion(ref tester0, out rotationMatrix);

                                                                Matrix.Multiply(ref rotationMatrix, ref translationMatrix0, out translationMatrix0);
                                                                worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix0;

                                                                _objects_static_00[x + width * (y + height * z)][count] = 1;
                                                                _objects_rigid_static_00[x + width * (y + height * z)][count] = rigidbody;

                                                                //_swtch_hasRotated = 0;
                                                                //_has_grabbed_right = 0;
                                                                //_has_grabbed_right_swtch = 0;
                                                                //_grabbed_body_right = null;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            _process_rigidbody_two(rigidbody, x, y, z, count);

                                                        }

                                                        _some_last_frame_vector[x + width * (y + height * z)][count] = _arrayOfVecs;
                                                        _some_last_frame_rigibodies[x + width * (y + height * z)][count] = _arrayOfBodies;

                                                        if (!rigidbody.IsActive) // || body.Is
                                                        {
                                                            _inactive_counter++;
                                                        }

                                                        count++;
                                                    }
                                                    else if ((SC_Console_DIRECTX.BodyTag)rigidbody.Tag == SC_Console_DIRECTX.BodyTag.Terrain)
                                                    {
                                                        Matrix translationMatrix = _world_terrain_list[x + width * (y + height * z)]._POSITION;
                                                        Quaternion tester;
                                                        Quaternion.RotationMatrix(ref translationMatrix, out tester);


                                                        JQuaternion jquat;
                                                        jquat.X = tester.X;
                                                        jquat.Y = tester.Y;
                                                        jquat.Z = tester.Z;
                                                        jquat.W = tester.W;


                                                        //JQuaternion jquat = new JQuaternion(tester.X, tester.Y, tester.Z, tester.W);

                                                        JMatrix jmat = JMatrix.CreateFromQuaternion(jquat);

                                                        //_terrain._singleObjectOnly.transform.Component.rigidbody.Orientation = jmat;
                                                        //_terrain._singleObjectOnly.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                        _world_terrain_list[x + width * (y + height * z)]._singleObjectOnly.transform.Component.rigidbody.Orientation = jmat;
                                                        _world_terrain_list[x + width * (y + height * z)]._singleObjectOnly.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                    }
                                                    else if ((SC_Console_DIRECTX.BodyTag)rigidbody.Tag == SC_Console_DIRECTX.BodyTag.Screen)
                                                    {
                                                        Matrix translationMatrix = _screenModel._POSITION;
                                                        Quaternion tester;
                                                        Quaternion.RotationMatrix(ref translationMatrix, out tester);

                                                        JQuaternion jquat;
                                                        jquat.X = tester.X;
                                                        jquat.Y = tester.Y;
                                                        jquat.Z = tester.Z;
                                                        jquat.W = tester.W;
                                                        //JQuaternion jquat = new JQuaternion(tester.X, tester.Y, tester.Z, tester.W);
                                                        JMatrix jmat = JMatrix.CreateFromQuaternion(jquat);

                                                        _screenModel._singleObjectOnly.transform.Component.rigidbody.Orientation = jmat; //new JVector(OFFSETPOS.X, OFFSETPOS.Y, OFFSETPOS.Z);

                                                        _screen_model_pos.X = translationMatrix.M41;
                                                        _screen_model_pos.Y = translationMatrix.M42;
                                                        _screen_model_pos.Z = translationMatrix.M43;


                                                        _screenModel._singleObjectOnly.transform.Component.rigidbody.Position = _screen_model_pos;
                                                        //body.Position = new Jitter.LinearMath.JVector(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                        //body.Orientation = jmat;                
                                                    }
                                                    else if ((SC_Console_DIRECTX.BodyTag)rigidbody.Tag == SC_Console_DIRECTX.BodyTag.testChunkCloth)
                                                    {
                                                        var _tempMatrix = Matrix.Identity;

                                                        Matrix translationMatrix = Matrix.Identity;
                                                        Matrix.Translation(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z, out translationMatrix);
                                                        Matrix rotationMatrixOfObject = _arrayOfClothCubes[clothCounter]._ORIGINPOSITION;
                                                        Quaternion quaterer;
                                                        Quaternion.RotationMatrix(ref rotationMatrixOfObject, out quaterer);

                                                        JQuaternion quatterer = new JQuaternion(quaterer.X, quaterer.Y, quaterer.Z, quaterer.W);
                                                        JMatrix matrixer = JMatrix.CreateFromQuaternion(quatterer);

                                                        rigidbody.Orientation = matrixer;

                                                        _arrayOfClothCubes[clothCounter]._POSITION = translationMatrix;
                                                        //orldMatrix_Cloth_instances[clothCounter] = _tempMatrix;
                                                        worldMatrix_instances_cloth[x + width * (y + height * z)][clothCounter] = WorldMatrix;
                                                        clothCounter++;
                                                    }
                                                }
                                            }







                                            if (_console_display_frame_counter > 50)
                                            {


                                                /*for (int xx= 0; xx < 50; xx++)
                                                {
                                                    for (int yy = 0; yy < 13; yy++)
                                                    {
                                                        Console.SetCursorPosition(xx, yy);
                                                        Console.Write("");

                                                    }
                                                }





                                                Console.SetCursorPosition(1, 2);
                                                Console.WriteLine("Steve Chassé's VR SC_VR_CORE_SYSTEMS");
                                                Console.SetCursorPosition(1, 3);
                                                Console.WriteLine("using DOTNET 2.1 (C# barebone)");
                                                Console.SetCursorPosition(1, 4);
                                                Console.WriteLine("using Zoosk's SharpDX Libraries V4.2 (using DirectX11)");
                                                Console.SetCursorPosition(1, 5);
                                                Console.WriteLine("using Andrej Benedik's Ab4d's DXEngine.OculusWrap and DXEngine Library)");
                                                Console.SetCursorPosition(1, 6);
                                                Console.WriteLine("using The Jitter Physics Engine (current working demo)");
                                                Console.SetCursorPosition(1, 7);
                                                Console.WriteLine("using The BEPU V1 Physics Engine (working demo not incorporated in main program yet)");
                                                Console.SetCursorPosition(1, 8);
                                                Console.WriteLine("using The BEPU V2 Physics Engine (working demo not incorporated in main program yet)");
                                                Console.SetCursorPosition(1, 9);
                                                Console.WriteLine("disabled objects: ");*/






                                                _console_display_frame_counter = 0;
                                            }



                                            if (_console_display_counter > 10)
                                            {
                                                string testr = "disabled objects: ";


                                                Console.SetCursorPosition(testr.Length + 2, 4);
                                                Console.WriteLine(_inactive_counter);
                                            }
                                        }
                                    }

                                    /*Func<int> formatDelegate = () =>
                                    {

                                        return 1;
                                    };

                                    var t2 = new Task<int>(formatDelegate);
                                    t2.RunSynchronously();
                                    t2.Dispose();*/
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox((IntPtr)0, ex.ToString() + "", "Oculus error", 0);
                    }





























                    /*
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            for (int z = 0; z < depth; z++)
                            {
                                //x + width * (y + height * z)
                                // DateTime.Now.Second;

                                _World_Step = deltaTime;
                                if (_world_list[x + width * (y + height * z)] != null)
                                {
                                    //startTime.Now.Second; //deltaTime;//
                                    if (_World_Step > 1.0f * 0.01f)
                                    {
                                        _World_Step = 1.0f * 0.01f;
                                    }
                                    _world_list[x + width * (y + height * z)].Step(_World_Step, true);
                                }

                                int _inactive_counter = 0;

                                if (_world_list[x + width * (y + height * z)] != null)
                                {
                                    if (_world_list[x + width * (y + height * z)].RigidBodies != null && _world_list[x + width * (y + height * z)].RigidBodies.Count > 0)
                                    {
                                        int count = 0;
                                        int clothCounter = 0;
                                        //IEnumerator enumerator0 = _world_list[x + width * (y + height * z)].RigidBodies.GetEnumerator();

                                        //while (enumerator0.MoveNext())
                                        foreach (RigidBody rigidbody in _world_list[x + width * (y + height * z)].RigidBodies)
                                        {
                                            //RigidBody rigidbody = (RigidBody)enumerator0.Current;

                                            //if (rigidbody != null) //&& body.Tag != null
                                            {
                                                if ((SC_Console_DIRECTX.BodyTag)rigidbody.Tag == SC_Console_DIRECTX.BodyTag.physicsInstancedCube)
                                                {
                                                    if (rigidbody.IsStatic)
                                                    {
                                                        if (rigidbody == _grabbed_body_right)
                                                        {
                                                            if (_has_grabbed_right_swtch == 2)
                                                            {
                                                                //var rigibody_pos = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (_offset_grabbed_pos_norm * _offset_grabbed_pos_dist);

                                                                var handPoser0 = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (_offset_grabbed_pos_norm * _offset_grabbed_pos_dist);

                                                                if (_swtch_hasRotated == 0)
                                                                {
                                                                    _grabbed_object_matrix = originRot * rotatingMatrix;
                                                                    _grabbed_object_matrix.M41 = 0;
                                                                    _grabbed_object_matrix.M42 = 0;
                                                                    _grabbed_object_matrix.M43 = 0;
                                                                    _grabbed_object_matrix.M44 = 1;


                                                                    JQuaternion quatterer01 = JQuaternion.CreateFromMatrix(_grabbed_body_pos_rot); //_grabbed_body_pos_rot

                                                                    Quaternion tester01;
                                                                    tester01.X = quatterer01.X;
                                                                    tester01.Y = quatterer01.Y;
                                                                    tester01.Z = quatterer01.Z;
                                                                    tester01.W = quatterer01.W;

                                                                    Matrix.RotationQuaternion(ref tester01, out rotationMatrix);

                                                                    _grabbed_object_matrix = rotationMatrix * _grabbed_object_matrix;


                                                                    //var handPoser = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (direction_feet_forward_hand * _last_min_distZ);
                                                                    var handPoser = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist);




                                                                    _grabbed_object_matrix.M41 = handPoser.X;
                                                                    _grabbed_object_matrix.M42 = handPoser.Y;
                                                                    _grabbed_object_matrix.M43 = handPoser.Z;

                                                                    _current_hand_pos_for_d = new Vector3(_last_current_hand_pos_for_d.X, _last_current_hand_pos_for_d.Y, _last_current_hand_pos_for_d.Z);


                                                                    _current_hand_float_for_dZ = (new Vector3(_grabbed_object_matrix.M41, _grabbed_object_matrix.M42, _grabbed_object_matrix.M43) - new Vector3(_last_current_hand_pos_for_d.X, _last_current_hand_pos_for_d.Y, _last_current_hand_pos_for_d.Z)).Z;//*_last_offset_grabbed_pos_norm_dist);//.Z;// (new Vector3(_grabbed_object_matrix.M41, _grabbed_object_matrix.M42, _grabbed_object_matrix.M43) - _current_hand_pos_for_d).Length(); //

                                                                    //_current_hand_float_for_dZ = (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist).Z;// (new Vector3(_grabbed_object_matrix.M41, _grabbed_object_matrix.M42, _grabbed_object_matrix.M43) - _current_hand_pos_for_d).Length(); //
                                                                    _current_hand_float_for_dY = (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist).Y;
                                                                    _current_hand_float_for_dX = (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist).X;





                                                                    worldMatrix_instances[x + width * (y + height * z)][count] = _grabbed_object_matrix;


                                                                    Quaternion quat;
                                                                    Quaternion.RotationMatrix(ref _grabbed_object_matrix, out quat);

                                                                    JQuaternion quatterer0;
                                                                    quatterer0.X = quat.X;
                                                                    quatterer0.Y = quat.Y;
                                                                    quatterer0.Z = quat.Z;
                                                                    quatterer0.W = quat.W;

                                                                    rigidbody.Orientation = JMatrix.CreateFromQuaternion(quatterer0);
                                                                    rigidbody.Position = new JVector(handPoser.X, handPoser.Y, handPoser.Z);


                                                                    //var rigibody_pos = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (_offset_grabbed_pos_norm * _offset_grabbed_pos_dist);
                                                                    var rigibody_pos = new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z) + (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist);
                                                                    //_last_min_distX = (rigibody_pos - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).X;
                                                                    //_last_min_distY = (rigibody_pos - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).Y;
                                                                    //_last_min_distZ = (rigibody_pos - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).Z;
                                                                    _last_min_distX = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z)).X;
                                                                    _last_min_distY = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z)).Y;
                                                                    _last_min_distZ = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z)).Length() * 0.667f;

                                                                    if (_has_grabbed_right == 0)
                                                                    {
                                                                        _has_grabbed_right = 1;
                                                                    }
                                                                }



                                                                if (_swtch_hasRotated == 2)
                                                                {
                                                                    //_grabbed_object_matrix = originRot * rotatingMatrix * rotatingMatrixForPelvis;
                                                                    //_grabbed_object_matrix.M41 = 0;
                                                                    //_grabbed_object_matrix.M42 = 0;
                                                                    //_grabbed_object_matrix.M43 = 0;
                                                                    //_grabbed_object_matrix.M44 = 1;


                                                                    JQuaternion quatterer02 = JQuaternion.CreateFromMatrix(_grabbed_body_pos_rot); //_grabbed_body_pos_rot

                                                                    Quaternion tester02;
                                                                    tester02.X = quatterer02.X;
                                                                    tester02.Y = quatterer02.Y;
                                                                    tester02.Z = quatterer02.Z;
                                                                    tester02.W = quatterer02.W;

                                                                    Matrix.RotationQuaternion(ref tester02, out rotationMatrix);

                                                                    _grabbed_object_matrix = rotationMatrix * _grabbed_object_matrix;

                                                                    var _rightTouchMatrix_00 = _rightTouchMatrix;
                                                                    _rightTouchMatrix_00.M41 = 0;
                                                                    _rightTouchMatrix_00.M42 = 0;
                                                                    _rightTouchMatrix_00.M43 = 0;
                                                                    _rightTouchMatrix_00.M44 = 1;
                                                                    _rightTouchMatrix_00 = _rightTouchMatrix_00 * originRot * rotatingMatrix * rotatingMatrixForPelvis;
                                                                    Quaternion.RotationMatrix(ref _rightTouchMatrix_00, out otherQuat);

                                                                    //CONVERTING THE QUATERNION OF THAT TO THE DIRECTION OF ITS ROTATION
                                                                    var direction_feet_forward_hand = _getDirection(Vector3.ForwardRH, otherQuat);
                                                                    var direction_feet_right_hand = _getDirection(Vector3.Right, otherQuat);
                                                                    var direction_feet_up_hand = _getDirection(Vector3.Up, otherQuat);


                                                                    //var handPoser = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (direction_feet_forward_hand * _last_min_distZ);
                                                                    var handPoser = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (_offset_grabbed_pos_norm * _offset_grabbed_pos_dist);// (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist);



                                                                    _grabbed_object_matrix.M41 = handPoser.X;
                                                                    _grabbed_object_matrix.M42 = handPoser.Y;
                                                                    _grabbed_object_matrix.M43 = handPoser.Z;

                                                                    //_current_hand_pos_for_d = new Vector3(_last_current_hand_pos_for_d.X, _last_current_hand_pos_for_d.Y, _last_current_hand_pos_for_d.Z);

                                                                    _current_hand_float_for_dZ = (new Vector3(_grabbed_object_matrix.M41, _grabbed_object_matrix.M42, _grabbed_object_matrix.M43) - new Vector3(_last_current_hand_pos_for_d.X, _last_current_hand_pos_for_d.Y, _last_current_hand_pos_for_d.Z)).Z;//*_last_offset_grabbed_pos_norm_dist);//.Z;// (new Vector3(_grabbed_object_matrix.M41, _grabbed_object_matrix.M42, _grabbed_object_matrix.M43) - _current_hand_pos_for_d).Length(); //                             
                                                                    _current_hand_float_for_dY = (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist).Y;
                                                                    _current_hand_float_for_dX = (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist).X;



                                                                    Console.SetCursorPosition(1, 6);
                                                                    Console.WriteLine(_current_hand_float_for_dZ);



                                                                    if (_tier_logic_swtch_grab == 0)
                                                                    {
                                                                        Matrix humanBodyRotation = originRot * rotatingMatrix * rotatingMatrixForPelvis; //originRot
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

                                                                        var someRotationFinal = rigidBodyMatrix * originRot * rotatingMatrix * rotatingMatrixForGrabber;

                                                                        //TORSO PIVOT
                                                                        Vector3 MOVINGPOINTER = new Vector3(_SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M41, _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M42, _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M43);

                                                                        //SAVING IN MEMORY THE ORIGINAL TORSO MATRIX NOT AFFECTED BY CURRENT POSITION AND ROTATION CHANGES.
                                                                        Matrix _rotMatrixer = _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION;
                                                                        Quaternion forTest;
                                                                        Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                                                        //FROM THE MATRIX OF ROTATION/POSITION, I GET THE QUATERNION OUT OF THAT AND CREATE THE DIRECTIONS THAT THE OBJECTS ARE ORIGINALLY FACING.
                                                                        var direction_feet_forward_ori_torso = _getDirection(Vector3.ForwardRH, forTest);
                                                                        var direction_feet_right_ori_torso = _getDirection(Vector3.Right, forTest);
                                                                        var direction_feet_up_ori_torso = _getDirection(Vector3.Up, forTest);

                                                                        //SINCE THE PIVOT POINT IS CURRENTLY IN THE MIDDLE OF THE TORSO, IT CANNOT ROTATE AT THAT POINT OTHERWISE, IT WONT FOLLOW THE PELVIS ROTATION LATER ON.
                                                                        //SO WE CURRENTLY ONLY OFFSET THE TORSO "MIDDLE OF SPINE APPROX" TO HALF OF THE CURRENT HEIGHT IN ORDER TO MAKE THE PIVOT POINT, APPROX WHERE THE PELVIS IS.
                                                                        Vector3 TORSOPIVOT = MOVINGPOINTER + -(direction_feet_up_ori_torso * (_SC_visual_object_manager._humRig._player_torso._total_torso_height * 0.5f));


                                                                        _rightTouchMatrix.M41 = handPoseRight.Position.X;
                                                                        _rightTouchMatrix.M42 = handPoseRight.Position.Y;
                                                                        _rightTouchMatrix.M43 = handPoseRight.Position.Z;



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

                                                                        //MOVINGPOINTER.X += OFFSETPOS.X;
                                                                        //MOVINGPOINTER.Y += OFFSETPOS.Y;
                                                                        //MOVINGPOINTER.Z += OFFSETPOS.Z;



                                                                        _rightTouchMatrix_00 = _rightTouchMatrix;
                                                                        _rightTouchMatrix_00.M41 = 0;
                                                                        _rightTouchMatrix_00.M42 = 0;
                                                                        _rightTouchMatrix_00.M43 = 0;
                                                                        _rightTouchMatrix_00.M44 = 1;
                                                                        _rightTouchMatrix_00 = _rightTouchMatrix_00 * originRot * rotatingMatrix * rotatingMatrixForPelvis;
                                                                        Quaternion.RotationMatrix(ref _rightTouchMatrix_00, out otherQuat);

                                                                        //CONVERTING THE QUATERNION OF THAT TO THE DIRECTION OF ITS ROTATION
                                                                        direction_feet_forward_hand = _getDirection(Vector3.ForwardRH, otherQuat);
                                                                        direction_feet_right_hand = _getDirection(Vector3.Right, otherQuat);
                                                                        direction_feet_up_hand = _getDirection(Vector3.Up, otherQuat);


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


                                                                        handPoser = MOVINGPOINTER;



                                                                        someRotationFinal = rigidBodyMatrix * originRot * rotatingMatrix * rotatingMatrixForGrabber;// * originRot * rotatingMatrix * rotatingMatrixForGrabber;


                                                                        someRotationFinal.M41 = handPoser.X;
                                                                        someRotationFinal.M42 = handPoser.Y;
                                                                        someRotationFinal.M43 = handPoser.Z;

                                                                        worldMatrix_instances[x + width * (y + height * z)][count] = someRotationFinal;

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

                                                                        _last_current_hand_pos_for_d = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);

                                                                        _last_current_hand_float_for_d = (handPoser - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).Z;

                                                                    }
                                                                    else if (_tier_logic_swtch_grab == 1)
                                                                    {
                                                                        Matrix humanBodyRotation = originRot * rotatingMatrix * rotatingMatrixForPelvis; //originRot
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

                                                                        var someRotationFinal = rigidBodyMatrix * originRot * rotatingMatrix * rotatingMatrixForGrabber;

                                                                        //TORSO PIVOT
                                                                        Vector3 MOVINGPOINTER = new Vector3(_SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M41, _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M42, _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M43);

                                                                        //SAVING IN MEMORY THE ORIGINAL TORSO MATRIX NOT AFFECTED BY CURRENT POSITION AND ROTATION CHANGES.
                                                                        Matrix _rotMatrixer = _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION;
                                                                        Quaternion forTest;
                                                                        Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                                                        //FROM THE MATRIX OF ROTATION/POSITION, I GET THE QUATERNION OUT OF THAT AND CREATE THE DIRECTIONS THAT THE OBJECTS ARE ORIGINALLY FACING.
                                                                        var direction_feet_forward_ori_torso = _getDirection(Vector3.ForwardRH, forTest);
                                                                        var direction_feet_right_ori_torso = _getDirection(Vector3.Right, forTest);
                                                                        var direction_feet_up_ori_torso = _getDirection(Vector3.Up, forTest);

                                                                        //SINCE THE PIVOT POINT IS CURRENTLY IN THE MIDDLE OF THE TORSO, IT CANNOT ROTATE AT THAT POINT OTHERWISE, IT WONT FOLLOW THE PELVIS ROTATION LATER ON.
                                                                        //SO WE CURRENTLY ONLY OFFSET THE TORSO "MIDDLE OF SPINE APPROX" TO HALF OF THE CURRENT HEIGHT IN ORDER TO MAKE THE PIVOT POINT, APPROX WHERE THE PELVIS IS.
                                                                        Vector3 TORSOPIVOT = MOVINGPOINTER + -(direction_feet_up_ori_torso * (_SC_visual_object_manager._humRig._player_torso._total_torso_height * 0.5f));


                                                                        _rightTouchMatrix.M41 = handPoseRight.Position.X;
                                                                        _rightTouchMatrix.M42 = handPoseRight.Position.Y;
                                                                        _rightTouchMatrix.M43 = handPoseRight.Position.Z;



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

                                                                        //MOVINGPOINTER.X += OFFSETPOS.X;
                                                                        //MOVINGPOINTER.Y += OFFSETPOS.Y;
                                                                        //MOVINGPOINTER.Z += OFFSETPOS.Z;



                                                                        _rightTouchMatrix_00 = _rightTouchMatrix;
                                                                        _rightTouchMatrix_00.M41 = 0;
                                                                        _rightTouchMatrix_00.M42 = 0;
                                                                        _rightTouchMatrix_00.M43 = 0;
                                                                        _rightTouchMatrix_00.M44 = 1;
                                                                        _rightTouchMatrix_00 = _rightTouchMatrix_00 * originRot * rotatingMatrix * rotatingMatrixForPelvis;
                                                                        Quaternion.RotationMatrix(ref _rightTouchMatrix_00, out otherQuat);

                                                                        //CONVERTING THE QUATERNION OF THAT TO THE DIRECTION OF ITS ROTATION
                                                                        direction_feet_forward_hand = _getDirection(Vector3.ForwardRH, otherQuat);
                                                                        direction_feet_right_hand = _getDirection(Vector3.Right, otherQuat);
                                                                        direction_feet_up_hand = _getDirection(Vector3.Up, otherQuat);




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


                                                                        handPoser = MOVINGPOINTER;



                                                                        someRotationFinal = rigidBodyMatrix * originRot * rotatingMatrix * rotatingMatrixForGrabber;// * originRot * rotatingMatrix * rotatingMatrixForGrabber;


                                                                        someRotationFinal.M41 = handPoser.X;
                                                                        someRotationFinal.M42 = handPoser.Y;
                                                                        someRotationFinal.M43 = handPoser.Z;

                                                                        worldMatrix_instances[x + width * (y + height * z)][count] = someRotationFinal;

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

                                                                        _last_current_hand_pos_for_d = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);

                                                                        _last_current_hand_float_for_d = (handPoser - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).Z;

                                                                    }
                                                                }








                                                                if (_swtch_hasRotated == 1)
                                                                {
                                                                    Matrix humanBodyRotation = originRot * rotatingMatrix * rotatingMatrixForPelvis; //originRot
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

                                                                    var someRotationFinal = rigidBodyMatrix * originRot * rotatingMatrix * rotatingMatrixForGrabber;

                                                                    //TORSO PIVOT
                                                                    Vector3 MOVINGPOINTER = new Vector3(_SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M41, _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M42, _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M43);

                                                                    //SAVING IN MEMORY THE ORIGINAL TORSO MATRIX NOT AFFECTED BY CURRENT POSITION AND ROTATION CHANGES.
                                                                    Matrix _rotMatrixer = _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION;
                                                                    Quaternion forTest;
                                                                    Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

                                                                    //FROM THE MATRIX OF ROTATION/POSITION, I GET THE QUATERNION OUT OF THAT AND CREATE THE DIRECTIONS THAT THE OBJECTS ARE ORIGINALLY FACING.
                                                                    var direction_feet_forward_ori_torso = _getDirection(Vector3.ForwardRH, forTest);
                                                                    var direction_feet_right_ori_torso = _getDirection(Vector3.Right, forTest);
                                                                    var direction_feet_up_ori_torso = _getDirection(Vector3.Up, forTest);

                                                                    //SINCE THE PIVOT POINT IS CURRENTLY IN THE MIDDLE OF THE TORSO, IT CANNOT ROTATE AT THAT POINT OTHERWISE, IT WONT FOLLOW THE PELVIS ROTATION LATER ON.
                                                                    //SO WE CURRENTLY ONLY OFFSET THE TORSO "MIDDLE OF SPINE APPROX" TO HALF OF THE CURRENT HEIGHT IN ORDER TO MAKE THE PIVOT POINT, APPROX WHERE THE PELVIS IS.
                                                                    Vector3 TORSOPIVOT = MOVINGPOINTER + -(direction_feet_up_ori_torso * (_SC_visual_object_manager._humRig._player_torso._total_torso_height * 0.5f));


                                                                    _rightTouchMatrix.M41 = handPoseRight.Position.X;
                                                                    _rightTouchMatrix.M42 = handPoseRight.Position.Y;
                                                                    _rightTouchMatrix.M43 = handPoseRight.Position.Z;



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

                                                                    //MOVINGPOINTER.X += OFFSETPOS.X;
                                                                    //MOVINGPOINTER.Y += OFFSETPOS.Y;
                                                                    //MOVINGPOINTER.Z += OFFSETPOS.Z;



                                                                    var _rightTouchMatrix_00 = _rightTouchMatrix;
                                                                    _rightTouchMatrix_00.M41 = 0;
                                                                    _rightTouchMatrix_00.M42 = 0;
                                                                    _rightTouchMatrix_00.M43 = 0;
                                                                    _rightTouchMatrix_00.M44 = 1;
                                                                    _rightTouchMatrix_00 = _rightTouchMatrix_00 * originRot * rotatingMatrix * rotatingMatrixForPelvis;
                                                                    Quaternion.RotationMatrix(ref _rightTouchMatrix_00, out otherQuat);

                                                                    //CONVERTING THE QUATERNION OF THAT TO THE DIRECTION OF ITS ROTATION
                                                                    var direction_feet_forward_hand = _getDirection(Vector3.ForwardRH, otherQuat);
                                                                    var direction_feet_right_hand = _getDirection(Vector3.Right, otherQuat);
                                                                    var direction_feet_up_hand = _getDirection(Vector3.Up, otherQuat);


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



                                                                    someRotationFinal = rigidBodyMatrix * originRot * rotatingMatrix * rotatingMatrixForGrabber;// * originRot * rotatingMatrix * rotatingMatrixForGrabber;


                                                                    someRotationFinal.M41 = handPoser.X;
                                                                    someRotationFinal.M42 = handPoser.Y;
                                                                    someRotationFinal.M43 = handPoser.Z;

                                                                    worldMatrix_instances[x + width * (y + height * z)][count] = someRotationFinal;

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

                                                                    _last_current_hand_pos_for_d = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);

                                                                    _last_current_hand_float_for_d = (handPoser - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).Z;

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
                                                            rigidbody.AffectedByGravity = false;
                                                            rigidbody.IsActive = false;

                                                            Matrix translationMatrix0;
                                                            Matrix.Translation(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z, out translationMatrix0);

                                                            JQuaternion quatterer0 = JQuaternion.CreateFromMatrix(rigidbody.Orientation);

                                                            Quaternion tester0;
                                                            tester0.X = quatterer0.X;
                                                            tester0.Y = quatterer0.Y;
                                                            tester0.Z = quatterer0.Z;
                                                            tester0.W = quatterer0.W;

                                                            //Quaternion tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

                                                            Matrix.RotationQuaternion(ref tester0, out rotationMatrix);

                                                            Matrix.Multiply(ref rotationMatrix, ref translationMatrix0, out translationMatrix0);
                                                            worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix0;

                                                            _objects_static_00[x + width * (y + height * z)][count] = 1;
                                                            _objects_rigid_static_00[x + width * (y + height * z)][count] = rigidbody;

                                                            //_swtch_hasRotated = 0;
                                                            //_has_grabbed_right = 0;
                                                            //_has_grabbed_right_swtch = 0;
                                                            //_grabbed_body_right = null;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        _process_rigidbody(rigidbody, x, y, z, count);

                                                    }

                                                    _some_last_frame_vector[x + width * (y + height * z)][count] = _arrayOfVecs;
                                                    _some_last_frame_rigibodies[x + width * (y + height * z)][count] = _arrayOfBodies;

                                                    if (!rigidbody.IsActive) // || body.Is
                                                    {
                                                        _inactive_counter++;
                                                    }

                                                    count++;
                                                }
                                                else if ((SC_Console_DIRECTX.BodyTag)rigidbody.Tag == SC_Console_DIRECTX.BodyTag.Terrain)
                                                {
                                                    Matrix translationMatrix = _world_terrain_list[x + width * (y + height * z)]._POSITION;
                                                    Quaternion tester;
                                                    Quaternion.RotationMatrix(ref translationMatrix, out tester);


                                                    JQuaternion jquat;
                                                    jquat.X = tester.X;
                                                    jquat.Y = tester.Y;
                                                    jquat.Z = tester.Z;
                                                    jquat.W = tester.W;


                                                    //JQuaternion jquat = new JQuaternion(tester.X, tester.Y, tester.Z, tester.W);

                                                    JMatrix jmat = JMatrix.CreateFromQuaternion(jquat);

                                                    //_terrain._singleObjectOnly.transform.Component.rigidbody.Orientation = jmat;
                                                    //_terrain._singleObjectOnly.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                    _world_terrain_list[x + width * (y + height * z)]._singleObjectOnly.transform.Component.rigidbody.Orientation = jmat;
                                                    _world_terrain_list[x + width * (y + height * z)]._singleObjectOnly.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                                                }
                                                else if ((SC_Console_DIRECTX.BodyTag)rigidbody.Tag == SC_Console_DIRECTX.BodyTag.Screen)
                                                {
                                                    Matrix translationMatrix = _screenModel._POSITION;
                                                    Quaternion tester;
                                                    Quaternion.RotationMatrix(ref translationMatrix, out tester);

                                                    JQuaternion jquat;
                                                    jquat.X = tester.X;
                                                    jquat.Y = tester.Y;
                                                    jquat.Z = tester.Z;
                                                    jquat.W = tester.W;
                                                    //JQuaternion jquat = new JQuaternion(tester.X, tester.Y, tester.Z, tester.W);
                                                    JMatrix jmat = JMatrix.CreateFromQuaternion(jquat);

                                                    _screenModel._singleObjectOnly.transform.Component.rigidbody.Orientation = jmat; //new JVector(OFFSETPOS.X, OFFSETPOS.Y, OFFSETPOS.Z);

                                                    _screen_model_pos.X = translationMatrix.M41;
                                                    _screen_model_pos.Y = translationMatrix.M42;
                                                    _screen_model_pos.Z = translationMatrix.M43;


                                                    _screenModel._singleObjectOnly.transform.Component.rigidbody.Position = _screen_model_pos;
                                                    //body.Position = new Jitter.LinearMath.JVector(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);
                                                    //body.Orientation = jmat;                
                                                }
                                                else if ((SC_Console_DIRECTX.BodyTag)rigidbody.Tag == SC_Console_DIRECTX.BodyTag.testChunkCloth)
                                                {
                                                    var _tempMatrix = Matrix.Identity;

                                                    Matrix translationMatrix = Matrix.Identity;
                                                    Matrix.Translation(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z, out translationMatrix);
                                                    Matrix rotationMatrixOfObject = _arrayOfClothCubes[clothCounter]._ORIGINPOSITION;
                                                    Quaternion quaterer;
                                                    Quaternion.RotationMatrix(ref rotationMatrixOfObject, out quaterer);

                                                    JQuaternion quatterer = new JQuaternion(quaterer.X, quaterer.Y, quaterer.Z, quaterer.W);
                                                    JMatrix matrixer = JMatrix.CreateFromQuaternion(quatterer);

                                                    rigidbody.Orientation = matrixer;

                                                    _arrayOfClothCubes[clothCounter]._POSITION = translationMatrix;
                                                    //orldMatrix_Cloth_instances[clothCounter] = _tempMatrix;
                                                    worldMatrix_instances_cloth[x + width * (y + height * z)][clothCounter] = WorldMatrix;
                                                    clothCounter++;
                                                }
                                            }
                                        }

                                        if (_console_display_counter > 10)
                                        {
                                            string testr = "disabled objects: ";
                                            Console.SetCursorPosition(testr.Length + 2, 4);
                                            Console.WriteLine(_inactive_counter);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox((IntPtr)0, ex.ToString() + "", "Oculus error", 0);
                }*/






                    /*
if (_objects_static_00[x + width * (y + height * z)][count] == 1) // has set to !static from being static before.
{
if (_objects_static_counter_00[x + width * (y + height * z)][count] > 10)
{
    _process_rigidbody(rigidbody, x, y, z, count);
    _objects_static_00[x + width * (y + height * z)][count] = 3;
    _objects_static_counter_00[x + width * (y + height * z)][count] = 0;
}

_objects_static_counter_00[x + width * (y + height * z)][count]++;
}
else if (_objects_static_00[x + width * (y + height * z)][count] == 2) // has set to static - should be okay and accessible right away.
{
_process_rigidbody(rigidbody, x, y, z, count);
}
else if(_objects_static_00[x + width * (y + height * z)][count] == 3)  // has set from static to !static and 10 frames have passed - checking if this works or not
{
_process_rigidbody(rigidbody, x, y, z, count);
_objects_static_00[x + width * (y + height * z)][count] = 0;
}
else
{
_process_rigidbody(rigidbody, x, y, z, count);
}*/







                    /*if (_console_display_frame_counter > 50)
                    {
                        for (int xx= 0; xx < 50; xx++)
                        {
                            for (int yy = 0; yy < 13; yy++)
                            {
                                Console.SetCursorPosition(xx, yy);
                                Console.Write("");

                            }
                        }

                        Console.SetCursorPosition(1, 2);
                        Console.WriteLine("Steve Chassé's VR SC_VR_CORE_SYSTEMS");
                        Console.SetCursorPosition(1, 3);
                        Console.WriteLine("using DOTNET 2.1 (C# barebone)");
                        Console.SetCursorPosition(1, 4);
                        Console.WriteLine("using Zoosk's SharpDX Libraries V4.2 (using DirectX11)");
                        Console.SetCursorPosition(1, 5);
                        Console.WriteLine("using Andrej Benedik's Ab4d's DXEngine.OculusWrap and DXEngine Library)");
                        Console.SetCursorPosition(1, 6);
                        Console.WriteLine("using The Jitter Physics Engine (current working demo)");
                        Console.SetCursorPosition(1, 7);
                        Console.WriteLine("using The BEPU V1 Physics Engine (working demo not incorporated in main program yet)");
                        Console.SetCursorPosition(1, 8);
                        Console.WriteLine("using The BEPU V2 Physics Engine (working demo not incorporated in main program yet)");
                        Console.SetCursorPosition(1, 9);
                        Console.WriteLine("disabled objects: ");

                        _console_display_frame_counter = 0;
                    }*/






                    /*if (_some_frame_counter_randomizer_switch_counter > 1000)
                    {
                        for (int i = 0; i < _some_frame_counter_raycast_00.Length; i++)
                        {
                            for (int j = 0; j < _some_frame_counter_raycast_00[i].Length; j++)
                            {
                                _randNum = new Random();
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








                    //Console.WriteLine(World.SoftBodies.Count);




                    _last_swtch_hasRotated = _swtch_hasRotated;




                    /*if (_body_collision != null)
                    {
                        Console.SetCursorPosition(20, 3);
                        Console.WriteLine("dist?: " + _body_collision_fraction);
                    }*/





                    if (_has_locked_screen_pos_counter >= 20)
                    {
                        if (buttonPressedOculusTouchLeft == 256)//Program._KeyboardState.PressedKeys.Contains(Key.Space))
                        {
                            if (_has_locked_screen_pos == 0)
                            {
                                _has_locked_screen_pos = 1;
                            }
                            else
                            {
                                _has_locked_screen_pos = 0;
                            }
                        }
                        _has_locked_screen_pos_counter = 0;
                    }

                    _has_locked_screen_pos_counter++;





                    Vector3f[] hmdToEyeViewOffsets = new Vector3f[]
                    {
                        D3D.eyeTextures[0].HmdToEyeViewOffset, D3D.eyeTextures[1].HmdToEyeViewOffset
                    };


                    displayMidpoint = D3D.OVR.GetPredictedDisplayTime(D3D.sessionPtr, 0);
                    trackingState = D3D.OVR.GetTrackingState(D3D.sessionPtr, displayMidpoint, true);


                    D3D.OVR.CalcEyePoses(trackingState.HeadPose.ThePose, hmdToEyeViewOffsets, ref eyePoses);

                    for (int eyeIndex = 0; eyeIndex < 2; eyeIndex++)
                    {
                        eye = (EyeType)eyeIndex;
                        eyeTexture = D3D.eyeTextures[eyeIndex];

                        if (eyeIndex == 0)
                            D3D.layerEyeFov.RenderPoseLeft = eyePoses[0];
                        else
                            D3D.layerEyeFov.RenderPoseRight = eyePoses[1];

                        // Update the render description at each frame, as the HmdToEyeOffset can change at runtime.
                        eyeTexture.RenderDescription = D3D.OVR.GetRenderDesc(D3D.sessionPtr, eye, D3D.hmdDesc.DefaultEyeFov[eyeIndex]);

                        // Retrieve the index of the active texture

                        D3D.result = eyeTexture.SwapTextureSet.GetCurrentIndex(out textureIndex);
                        D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to retrieve texture swap chain current index.");

                        D3D.device.ImmediateContext.OutputMerger.SetRenderTargets(eyeTexture.DepthStencilView, eyeTexture.RenderTargetViews[textureIndex]);
                        D3D.device.ImmediateContext.ClearRenderTargetView(eyeTexture.RenderTargetViews[textureIndex], SharpDX.Color.DimGray); //DimGray
                        D3D.device.ImmediateContext.ClearDepthStencilView(eyeTexture.DepthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);
                        D3D.device.ImmediateContext.Rasterizer.SetViewport(eyeTexture.Viewport);



                        eyeQuaternionMatrix = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X, eyePoses[eyeIndex].Orientation.Y, eyePoses[eyeIndex].Orientation.Z, eyePoses[eyeIndex].Orientation.W));

                        eyePos = SharpDX.Vector3.Transform(new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z), originRot * rotatingMatrix * rotatingMatrixForPelvis).ToVector3(); //*rotatingMatrix //new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z

                        //Vector3 inverter = eyePos;
                        //eyePos.X = eyePos.Z;
                        //eyePos.Z = inverter.X;

                        finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix * rotatingMatrixForPelvis;// rotatingMatrix;


                        lookUp = Vector3.Transform(new Vector3(0, 1, 0), finalRotationMatrix).ToVector3(); //new Vector3(0, 1, 0)
                        lookAt = Vector3.Transform(new Vector3(0, 0, -1), finalRotationMatrix).ToVector3(); //new Vector3(0, 0, -1)

                        //var test = Vector3.TransformCoordinate(lookAt, rotatingMatrix);

                        viewPosition = eyePos + OFFSETPOS; //eyePoses[eyeIndex].Position.ToVector3()
                        viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);


                        //OFFSETPOS.X = viewMatrix.M41;
                        //OFFSETPOS.Y = viewMatrix.M42;
                        //OFFSETPOS.Z = viewMatrix.M43;


                        _projectionMatrix = D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.01f, 100.0f, ProjectionModifier.None).ToMatrix();
                        _projectionMatrix.Transpose();

                        //_WorldMatrix = WorldMatrix;
                        _WorldMatrix = D3D.WorldMatrix;



                        Quaternion quatter = new Quaternion(hmdRot.X, hmdRot.Y, hmdRot.Z, hmdRot.W);

                        Vector3 oculusRiftDir = _getDirection(Vector3.ForwardRH, quatter);

                        finalRotationMatrix = originRot * rotatingMatrix * rotatingMatrixForPelvis;


                        _rightTouchMatrix.M41 = handPoseRight.Position.X;
                        _rightTouchMatrix.M42 = handPoseRight.Position.Y;
                        _rightTouchMatrix.M43 = handPoseRight.Position.Z;
                        _leftTouchMatrix.M41 = handPoseLeft.Position.X;
                        _leftTouchMatrix.M42 = handPoseLeft.Position.Y;
                        _leftTouchMatrix.M43 = handPoseLeft.Position.Z;

                        _SC_visual_object_manager._humRig.update_human_rig(_rightTouchMatrix, _leftTouchMatrix, viewMatrix, _projectionMatrix, oculusRiftDir, finalRotationMatrix, OFFSETPOS, rotatingMatrixForPelvis, out final_hand_pos_left, out final_hand_pos_right);
                        _SC_visual_object_manager._humRig._update_human_rig_physics(_rightTouchMatrix, _leftTouchMatrix, OFFSETPOS);


                        _rightTouchMatrix.M41 = handPoseRight.Position.X + originPos.X + movePos.X;// + _hmdPoser.X;
                        _rightTouchMatrix.M42 = handPoseRight.Position.Y + originPos.Y + movePos.Y;// + _hmdPoser.Y;
                        _rightTouchMatrix.M43 = handPoseRight.Position.Z + originPos.Z + movePos.Z;// + _hmdPoser.Z;

                        _leftTouchMatrix.M41 = handPoseLeft.Position.X + originPos.X + movePos.X;// + _hmdPoser.X;
                        _leftTouchMatrix.M42 = handPoseLeft.Position.Y + originPos.Y + movePos.Y;// + _hmdPoser.Y;
                        _leftTouchMatrix.M43 = handPoseLeft.Position.Z + originPos.Z + movePos.Z;// + _hmdPoser.Z;


                        original_left_touch_matrix = _leftTouchMatrix;
                        original_right_touch_matrix = _rightTouchMatrix;

                        original_left_touch_matrix.M41 = handPoseLeft.Position.X;
                        original_left_touch_matrix.M42 = handPoseLeft.Position.Y;
                        original_left_touch_matrix.M43 = handPoseLeft.Position.Z;

                        original_right_touch_matrix.M41 = handPoseRight.Position.X;
                        original_right_touch_matrix.M42 = handPoseRight.Position.Y;
                        original_right_touch_matrix.M43 = handPoseRight.Position.Z;



                        //TERRAIN AND GRID RENDER 
                        //_grid_X.Render(D3D.Device.ImmediateContext);
                        //_shaderManager.RenderGrid(D3D.Device.ImmediateContext, _grid_X.IndexCount, _WorldMatrix, viewMatrix, _projectionMatrix);

                        //_grid_Y.Render(D3D.Device.ImmediateContext);
                        //_shaderManager.RenderGrid(D3D.Device.ImmediateContext, _grid_Y.IndexCount, _WorldMatrix, viewMatrix, _projectionMatrix);

                        //_FloorTiles.Render(D3D.Device.ImmediateContext);
                        //_shaderManager.RenderInstancedObject(D3D.Device.ImmediateContext, _FloorTiles.IndexCount, _FloorTiles.InstanceCount, matroxer2, viewMatrix, _projectionMatrix, _desktopFrame._ShaderResource);





                        /*foreach (SoftBody body in World.SoftBodies)
                        {

                            Matrix _tempMatrix = WorldMatrix;
                            _tempMatrix.M42 = 5;
                            for (int i = 0; i < body.VertexBodies.Count; i++)
                            {
                                var pos1 = body.VertexBodies[i].Position;

                                _pseudo_cloth.Vertices[i].position = new Vector3(pos1.X, pos1.Y, pos1.Z);
                            }

                            _pseudo_cloth.Render(D3D.device.ImmediateContext, D3D.device);
                            _shaderManager.RenderTouchTextureShader(D3D.device.ImmediateContext, _pseudo_cloth.IndexCount, _tempMatrix, viewMatrix, _projectionMatrix);
                        }*/







                        for (int j = 0; j < worldMatrix_instances_cloth.Length; j++)
                        {
                            for (int i = 0; i < _arrayOfClothCubes.Count; i++)
                            {
                                //worldMatrix_Terrain_instances[0] = WorldMatrix;
                                //_arrayOfClothCubes[i]._POSITION = worldMatrix_Terrain_instances[0];

                                _arrayOfClothCubes[i].Render(D3D.device.ImmediateContext);
                                _shaderManager.RenderInstancedCloth(D3D.device.ImmediateContext, _arrayOfClothCubes[i].IndexCount, _arrayOfClothCubes[i].InstanceCount, _arrayOfClothCubes[i]._POSITION, viewMatrix, _projectionMatrix, _basicTexture.TextureResource, worldMatrix_instances_cloth[j], _DLightBuffer, oculusRiftDir, _arrayOfClothCubes[i]);
                            }
                        }


                        Quaternion _testQuater;
                        Quaternion.RotationMatrix(ref matroxer2, out _testQuater);
                        //_testQuater.Normalize();
                        Vector3 dirLight = _getDirection(Vector3.ForwardRH, _testQuater);
                        dirLight.Normalize();

                        Vector4 ambientColor = new Vector4(0.75f, 0.75f, 0.75f, 1.0f);
                        Vector4 diffuseColour = new Vector4(0.95f, 0.95f, 0.95f, 1);
                        Vector3 lightDirection = new Vector3(0, -1, -1);


                        _DLightBuffer[0] = new SC_skYaRk_VR_V007.SC_Graphics.SC_cube.DLightBuffer()
                        {
                            ambientColor = ambientColor,
                            diffuseColor = diffuseColour,
                            lightDirection = dirLight,
                            padding0 = 7,
                            lightPosition = new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43),
                            padding1 = 100
                        };







                        for (int x = 0; x < width; x++)
                        {
                            for (int y = 0; y < height; y++)
                            {
                                for (int z = 0; z < depth; z++)
                                {
                                    int indexer = x + width * (y + height * z);

                                    //PHYSICS CUBES
                                    worldMatrix_Terrain_instances[0] = WorldMatrix;
                                    _world_cube_list[x + width * (y + height * z)]._POSITION = worldMatrix_Terrain_instances[0];
                                    _world_cube_list[x + width * (y + height * z)].Render(D3D.device.ImmediateContext);
                                    _shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _world_cube_list[x + width * (y + height * z)].IndexCount, _world_cube_list[x + width * (y + height * z)].InstanceCount, _world_cube_list[x + width * (y + height * z)]._POSITION, viewMatrix, _projectionMatrix, _basicTexture.TextureResource, worldMatrix_instances[indexer], _DLightBuffer, oculusRiftDir, _world_cube_list[x + width * (y + height * z)]);





                                    /*//COLLIDABLE PHYSICS TERRAIN
                                    worldMatrix_Terrain_instances[0] = WorldMatrix;
                                    _world_terrain_list[x + width * (y + height * z)].Render(D3D.device.ImmediateContext);
                                    _shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _world_terrain_list[x + width * (y + height * z)].IndexCount, _world_terrain_list[x + width * (y + height * z)].InstanceCount, _world_terrain_list[x + width * (y + height * z)]._POSITION, viewMatrix, _projectionMatrix, null, worldMatrix_Terrain_instances, _DLightBuffer, oculusRiftDir, _world_terrain_list[x + width * (y + height * z)]);
                                    */

                                }
                            }
                        }




























                        /*JQuaternion jquat;
                        jquat.X = tester.X;
                        jquat.Y = tester.Y;
                        jquat.Z = tester.Z;
                        jquat.W = tester.W;


                        //JQuaternion jquat = new JQuaternion(tester.X, tester.Y, tester.Z, tester.W);

                        JMatrix jmat = JMatrix.CreateFromQuaternion(jquat);

                        //_terrain._singleObjectOnly.transform.Component.rigidbody.Orientation = jmat;
                        //_terrain._singleObjectOnly.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);

                        _world_cloth_list[x + width * (y + height * z)]._singleObjectOnly.transform.Component.rigidbody.Orientation = jmat;
                        _world_cloth_list[x + width * (y + height * z)]._singleObjectOnly.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(translationMatrix.M41, translationMatrix.M42, translationMatrix.M43);



                        //SoftBody body = rigidbody as SoftBody;

                        for (int i = 0; i < softbody.VertexBodies.Count; i++)
                        {
                            var pos1 = softbody.VertexBodies[i].Position;
                            //var oriPos = clothRect.Vertices[i].position + new Vector3(0,0,0);
                            _world_cloth_list[x + width * (y + height * z)].Vertices[i].position = new Vector3(pos1.X, pos1.Y, pos1.Z);
                        }


                        //_world_cloth_list[x + width * (y + height * z)]._singleObjectOnly.transform.Component.softbody.Update(_World_Step);


                        /*foreach (SoftBody body in World.SoftBodies)
                        {
                            //clothRect.Render(D3D.device.ImmediateContext, D3D.device);
                            //_shaderManager.RenderTouchTextureShader(D3D.device.ImmediateContext, clothRect.IndexCount, clothRect._POSITION, viewMatrix, _projectionMatrix);
                        }*/
                        //clothRect.transform.Component.softbody.Update(_World_Step);

                        /*for (int i = 0;i < _FloorTiles.Length;i++)
                        {
                            _FloorTiles[i].Render(D3D.Device.ImmediateContext);
                            _shaderManager.RenderInstancedObject(D3D.Device.ImmediateContext, _FloorTiles[i].IndexCount, _FloorTiles[i].InstanceCount, _WorldMatrix, viewMatrix, _projectionMatrix, _basicTexture.TextureResource);
                        }*/

                        //_WORLD_GRID_X.Render(D3D.Device.ImmediateContext);
                        //_shaderManager.RenderGrid(D3D.Device.ImmediateContext, _WORLD_GRID_X.IndexCount, _WorldMatrix, viewMatrix, _projectionMatrix);

                        _mouseCursor.Render(D3D.Device.ImmediateContext);
                        _shaderManager.RenderInstancedObjecter(D3D.Device.ImmediateContext, _mouseCursor.IndexCount, _mouseCursor.InstanceCount, _mouseCursorMatrix, viewMatrix, _projectionMatrix, _basicTexture.TextureResource);


                        //_rightTouch.Render(D3D.Device.ImmediateContext);
                        //_shaderManager.RenderInstancedObject(D3D.Device.ImmediateContext, _rightTouch.IndexCount, _rightTouch.InstanceCount, _rightTouchMatrix, viewMatrix, _projectionMatrix, _basicTexture.TextureResource);


                        //_leftTouch.Render(D3D.Device.ImmediateContext);
                        //_shaderManager.RenderInstancedObject(D3D.Device.ImmediateContext, _leftTouch.IndexCount, _leftTouch.InstanceCount, _leftTouchMatrix, viewMatrix, _projectionMatrix, _basicTexture.TextureResource);


                        try
                        {
                            //matroxer2 = Matrix.Multiply(_leftTouchMatrix, rotatingMatrixScreen);
                            //matroxer2 = Matrix.Multiply(matroxer2, finalRotationMatrix);

                            if (_has_locked_screen_pos == 0)
                            {
                                matroxer2 = final_hand_pos_left;

                                Vector3 savingPos = new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43);

                                _finalRotMatrixScreen = matroxer2;
                                Quaternion _testQuator;
                                Quaternion.RotationMatrix(ref _finalRotMatrixScreen, out _testQuator);



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

                                _current_screen_pos = _direction_offsetter * _finalRotMatrixScreen;

                                _current_screen_pos.M41 = savingPos.X;
                                _current_screen_pos.M42 = savingPos.Y;
                                _current_screen_pos.M43 = savingPos.Z;

                                _last_screen_pos = _finalRotMatrixScreen;
                            }
                            else
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

                                //2nd
                                //var roll = Math.Atan2(2 * yq * wq + 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
                                //var pitch = Math.Atan2(2 * xq * wq + 2 * yq * zq, 1 - 2 * xq * xq - 2 * zq * zq);
                                //var yaw = Math.Asin(2 * xq * yq + 2 * zq * wq);

                                //1st   
                                //roll = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
                                //pitch = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
                                //yaw = Mathf.Asin(2 * x * y + 2 * z * w);

                                //RotationScreenX = pitch / 0.01745327925f;
                                //RotationScreenY = yaw;/// 0.0174532925f;
                                //RotationScreenZ = 45;/// 0.0174532925f;

                                pitch = (float)((Math.PI * -45) / 180);// (float)(45 * 0.0174532925f);// (float)(RotationScreenX * 0.0174532925f);
                                                                       //yaw = (float)(yaw * 0.0174532925f); ;// (float)(RotationScreenY * 0.0174532925f);
                                roll = (float)(0 * 0.0174532925f); ;// (float)(RotationScreenZ * 0.0174532925f);

                                _finalRotMatrixScreen = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                                _finalRotMatrixScreen.M41 = _last_screen_pos.M41;
                                _finalRotMatrixScreen.M42 = _last_screen_pos.M42;
                                _finalRotMatrixScreen.M43 = _last_screen_pos.M43;

                                _current_screen_pos = _direction_offsetter * _finalRotMatrixScreen;

                                _current_screen_pos.M41 = _last_screen_pos.M41;
                                _current_screen_pos.M42 = _last_screen_pos.M42;
                                _current_screen_pos.M43 = _last_screen_pos.M43;

                            }











                            if (_has_locked_screen_pos == 1)
                            {
                                _screenModel._POSITION = _current_screen_pos;
                                _screenModel.Render(D3D.Device.ImmediateContext);
                                _shaderManager.RenderInstancedObjectScreen(D3D.Device.ImmediateContext, _screenModel.IndexCount, _screenModel.InstanceCount, _screenModel._POSITION, viewMatrix, _projectionMatrix, _desktopFrame._ShaderResource);
                                //_last_screen_pos = _screenModel._POSITION;
                                //Console.WriteLine("test0");
                            }
                            else
                            {
                                _screenModel._POSITION = _current_screen_pos;
                                _screenModel.Render(D3D.Device.ImmediateContext);
                                _shaderManager.RenderInstancedObjectScreen(D3D.Device.ImmediateContext, _screenModel.IndexCount, _screenModel.InstanceCount, _screenModel._POSITION, viewMatrix, _projectionMatrix, _desktopFrame._ShaderResource);
                                //_last_screen_pos = _screenModel._POSITION;
                                //Console.WriteLine("test1");
                            }



                            //SCREEN GRID

                            /*_screen_grid_Y.Render(D3D.Device.ImmediateContext);
                           _shaderManager.RenderGrid(D3D.Device.ImmediateContext, _screen_grid_Y.IndexCount, _screenModel._POSITION, viewMatrix, _projectionMatrix);

                           _screen_metric_grid_Y.Render(D3D.Device.ImmediateContext);
                           _shaderManager.RenderGrid(D3D.Device.ImmediateContext, _screen_metric_grid_Y.IndexCount, _screenModel._POSITION, viewMatrix, _projectionMatrix);
                           */


                            //_screen_grid_X.Render(D3D.Device.ImmediateContext);
                            //_shaderManager.RenderGrid(D3D.Device.ImmediateContext, _screen_grid_Z.IndexCount, rotatingMatrixScreen, viewMatrix, _projectionMatrix);

                            //_grid_Z.Render(D3D.Device.ImmediateContext);
                            //_shaderManager.RenderGrid(D3D.Device.ImmediateContext, _screen_grid_Z.IndexCount, rotatingMatrixScreen, viewMatrix, _projectionMatrix);













                            _rightTouchMatrix.M41 = handPoseRight.Position.X;
                            _rightTouchMatrix.M42 = handPoseRight.Position.Y;
                            _rightTouchMatrix.M43 = handPoseRight.Position.Z;

                            _leftTouchMatrix.M41 = handPoseLeft.Position.X;
                            _leftTouchMatrix.M42 = handPoseLeft.Position.Y;
                            _leftTouchMatrix.M43 = handPoseLeft.Position.Z;


                            matroxer2 = _finalRotMatrixScreen;

                            Quaternion.RotationMatrix(ref matroxer2, out _testQuater);
                            //_testQuater.Normalize();

                            screenNormal = _getDirection(Vector3.ForwardRH, _testQuater);
                            screenNormal.Normalize();

                            planer = new Plane(new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43), screenNormal);


                            centerPosRight = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);


                            Quaternion.RotationMatrix(ref final_hand_pos_right, out _rightTouchQuat);

                            rayDirRight = _getDirection(Vector3.ForwardRH, _rightTouchQuat);
                            someRay = new Ray(centerPosRight, rayDirRight);

                            intersecter = someRay.Intersects(ref planer, out intersectPointRight);

                            centerPosLeft = new Vector3(final_hand_pos_left.M41, final_hand_pos_left.M42, final_hand_pos_left.M43);

                            Quaternion.RotationMatrix(ref final_hand_pos_left, out _leftTouchQuat);
                            rayDirLeft = _getDirection(Vector3.ForwardRH, _leftTouchQuat);
                            someRayLeft = new Ray(centerPosLeft, rayDirLeft);

                            intersecterLeft = someRayLeft.Intersects(ref planer, out intersectPointLeft);

                            _leftTouchMatrix = original_left_touch_matrix;
                            _rightTouchMatrix = original_right_touch_matrix;



                            if (_out_of_bounds_right == 1)
                            {
                                Vector3 tester00 = new Vector3(_screenDirMatrix_correct_pos[2].M41, _screenDirMatrix_correct_pos[2].M42, _screenDirMatrix_correct_pos[2].M43);
                                _intersectTouchRightMatrix = _current_screen_pos;

                                _intersectTouchRightMatrix.M41 = tester00.X;// + OFFSETPOS.X;
                                _intersectTouchRightMatrix.M42 = tester00.Y;// + OFFSETPOS.Y;
                                _intersectTouchRightMatrix.M43 = tester00.Z;// + OFFSETPOS.Z;

                                _intersectTouchRight.Render(D3D.Device.ImmediateContext);
                                _shaderManager.RenderInstancedObjecter(D3D.Device.ImmediateContext, _intersectTouchRight.IndexCount, _intersectTouchRight.InstanceCount, _intersectTouchRightMatrix, viewMatrix, _projectionMatrix, _basicTexture.TextureResource);
                            }
                            else
                            {
                                Vector3 tester00 = new Vector3(intersectPointRight.X, intersectPointRight.Y, intersectPointRight.Z);
                                _intersectTouchRightMatrix = _current_screen_pos;

                                _intersectTouchRightMatrix.M41 = tester00.X;// + OFFSETPOS.X;
                                _intersectTouchRightMatrix.M42 = tester00.Y;// + OFFSETPOS.Y;
                                _intersectTouchRightMatrix.M43 = tester00.Z;// + OFFSETPOS.Z;

                                _intersectTouchRight.Render(D3D.Device.ImmediateContext);
                                _shaderManager.RenderInstancedObjecter(D3D.Device.ImmediateContext, _intersectTouchRight.IndexCount, _intersectTouchRight.InstanceCount, _intersectTouchRightMatrix, viewMatrix, _projectionMatrix, _basicTexture.TextureResource);
                                //_last_intersectPointRight = intersectPointRight;

                            }



                            if (_out_of_bounds_left == 1)
                            {
                                Vector3 tester01 = new Vector3(_screenDirMatrix_correct_pos[2].M41, _screenDirMatrix_correct_pos[2].M42, _screenDirMatrix_correct_pos[2].M43);
                                _intersectTouchLeftMatrix = _current_screen_pos;

                                _intersectTouchLeftMatrix.M41 = tester01.X;// + OFFSETPOS.X;       
                                _intersectTouchLeftMatrix.M42 = tester01.Y;// + OFFSETPOS.Y;
                                _intersectTouchLeftMatrix.M43 = tester01.Z;//+ OFFSETPOS.Z;

                                _intersectTouchLeft.Render(D3D.Device.ImmediateContext);
                                _shaderManager.RenderInstancedObjecter(D3D.Device.ImmediateContext, _intersectTouchLeft.IndexCount, _intersectTouchLeft.InstanceCount, _intersectTouchLeftMatrix, viewMatrix, _projectionMatrix, _basicTexture.TextureResource);

                            }
                            else
                            {
                                Vector3 tester01 = new Vector3(intersectPointLeft.X, intersectPointLeft.Y, intersectPointLeft.Z);
                                _intersectTouchLeftMatrix = _current_screen_pos;

                                _intersectTouchLeftMatrix.M41 = tester01.X;// + OFFSETPOS.X;       
                                _intersectTouchLeftMatrix.M42 = tester01.Y;// + OFFSETPOS.Y;
                                _intersectTouchLeftMatrix.M43 = tester01.Z;//+ OFFSETPOS.Z;

                                _intersectTouchLeft.Render(D3D.Device.ImmediateContext);
                                _shaderManager.RenderInstancedObjecter(D3D.Device.ImmediateContext, _intersectTouchLeft.IndexCount, _intersectTouchLeft.InstanceCount, _intersectTouchLeftMatrix, viewMatrix, _projectionMatrix, _basicTexture.TextureResource);

                                //intersectPointRight = new Vector3(_intersectTouchRightMatrix.M41, _intersectTouchRightMatrix.M42, _intersectTouchRightMatrix.M43);
                                //intersectPointLeft = new Vector3(_intersectTouchLeftMatrix.M41, _intersectTouchLeftMatrix.M42, _intersectTouchLeftMatrix.M43);
                                //_last_intersectPointLeft = intersectPointLeft;

                            }




                            /*foreach (SoftBody body in World.SoftBodies)
                            {
                                Matrix _tempMatrix = WorldMatrix;
                                for (int i = 0; i < body.VertexBodies.Count; i++)
                                {
                                    var pos1 = body.VertexBodies[i].Position;

                                    clothRect.Vertices[i].position = new Vector3(pos1.X, pos1.Y, pos1.Z);
                                }
                                _tempMatrix = WorldMatrix;
                                clothRect.Render(D3D.device.ImmediateContext, D3D.device);
                                _shaderManager.RenderClother(D3D.device.ImmediateContext, clothRect.IndexCount, _tempMatrix, viewMatrix, _projectionMatrix);
                            }*/








                            /*_rightTouchMatrix.M41 = handPoseRight.Position.X;// + originPos.X + movePos.X;// + _hmdPoser.X;
                            _rightTouchMatrix.M42 = handPoseRight.Position.Y;//  + originPos.Y + movePos.Y;// + _hmdPoser.Y;
                            _rightTouchMatrix.M43 = handPoseRight.Position.Z;//  + originPos.Z + movePos.Z;// + _hmdPoser.Z;

                            _leftTouchMatrix.M41 = handPoseLeft.Position.X;//  + originPos.X + movePos.X;// + _hmdPoser.X;
                            _leftTouchMatrix.M42 = handPoseLeft.Position.Y;//  + originPos.Y + movePos.Y;// + _hmdPoser.Y;
                            _leftTouchMatrix.M43 = handPoseLeft.Position.Z;//  + originPos.Z + movePos.Z;// + _hmdPoser.Z;
                            */





                            /*_rightTouchMatrix.M41 = handPoseRight.Position.X;// + originPos.X + movePos.X;// + _hmdPoser.X;
                            _rightTouchMatrix.M42 = handPoseRight.Position.Y;// + originPos.Y + movePos.Y;// + _hmdPoser.Y;
                            _rightTouchMatrix.M43 = handPoseRight.Position.Z;// + originPos.Z + movePos.Z;// + _hmdPoser.Z;

                            _leftTouchMatrix.M41 = handPoseLeft.Position.X;// + originPos.X + movePos.X;// + _hmdPoser.X;
                            _leftTouchMatrix.M42 = handPoseLeft.Position.Y;// + originPos.Y + movePos.Y;// + _hmdPoser.Y;
                            _leftTouchMatrix.M43 = handPoseLeft.Position.Z;// + originPos.Z + movePos.Z;// + _hmdPoser.Z;
                            Matrix _some_test = _leftTouchMatrix;

                            float xii = _leftTouchMatrix.M41 + originPos.X + movePos.X;// + _hmdPoser.X;
                            float yii = _leftTouchMatrix.M42 + originPos.Y + movePos.Y;// + _hmdPoser.Y;
                            float zii = _leftTouchMatrix.M43 + originPos.Z + movePos.Z;// 


                            matroxer2 = _leftTouchMatrix * originRot * rotatingMatrix * rotatingMatrixForPelvis;
                            Quaternion.RotationMatrix(ref _current_screen_pos, out _testQuater);
                            */

                            //_rightTouchMatrix = final_hand_pos_right;
                            //_leftTouchMatrix = final_hand_pos_left;






                            matroxer2 = _finalRotMatrixScreen;


                            Quaternion.RotationMatrix(ref matroxer2, out _testQuater);
                            //_testQuater.Normalize();

                            //screenNormal = _getDirection(Vector3.ForwardRH, _testQuater);
                            //screenNormal.Normalize();
                            //planer = new Plane(new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43), screenNormal);


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

                            Matrix resulter = matroxer2;
                            resulter.M41 = currentScreenPos.X;
                            resulter.M42 = currentScreenPos.Y;
                            resulter.M43 = currentScreenPos.Z;
                            _screenDirMatrix_correct_pos[0] = resulter;


                            currentScreenPos = new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43);
                            newDirRight = (screenNormalRight) * sizeWidtherer;
                            currentScreenPos -= newDirRight;

                            newDirUp = (screenNormalTop) * sizeheighterer;
                            currentScreenPos += newDirUp;

                            resulter = matroxer2;
                            resulter.M41 = currentScreenPos.X;
                            resulter.M42 = currentScreenPos.Y;
                            resulter.M43 = currentScreenPos.Z;
                            _screenDirMatrix_correct_pos[1] = resulter;


                            currentScreenPos = new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43);
                            newDirRight = (screenNormalRight) * sizeWidtherer;
                            currentScreenPos += newDirRight;

                            newDirUp = (screenNormalTop) * sizeheighterer;
                            currentScreenPos -= newDirUp;

                            resulter = matroxer2;
                            resulter.M41 = currentScreenPos.X;
                            resulter.M42 = currentScreenPos.Y;
                            resulter.M43 = currentScreenPos.Z;
                            _screenDirMatrix_correct_pos[2] = resulter;


                            currentScreenPos = new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43);
                            newDirRight = (screenNormalRight) * sizeWidtherer;
                            currentScreenPos += newDirRight;

                            newDirUp = (screenNormalTop) * sizeheighterer;
                            currentScreenPos += newDirUp;

                            resulter = matroxer2;
                            resulter.M41 = currentScreenPos.X;
                            resulter.M42 = currentScreenPos.Y;
                            resulter.M43 = currentScreenPos.Z;
                            _screenDirMatrix_correct_pos[3] = resulter;

                            for (int i = 0; i < _screenCorners.Length; i++)
                            {

                                Matrix matrixor = _screenDirMatrix_correct_pos[i];

                                _screenCorners[i]._POSITION = matrixor;
                                _screenCorners[i].Render(D3D.device.ImmediateContext);
                                _shaderManager.RenderInstancedObjecter(D3D.device.ImmediateContext, _screenCorners[i].IndexCount, _screenCorners[i].InstanceCount, _screenCorners[i]._POSITION, viewMatrix, _projectionMatrix, _basicTexture.TextureResource);
                            }





                            _rightTouchMatrix.M41 = handPoseRight.Position.X;// + originPos.X + movePos.X;// + _hmdPoser.X;
                            _rightTouchMatrix.M42 = handPoseRight.Position.Y;//  + originPos.Y + movePos.Y;// + _hmdPoser.Y;
                            _rightTouchMatrix.M43 = handPoseRight.Position.Z;//  + originPos.Z + movePos.Z;// + _hmdPoser.Z;

                            _leftTouchMatrix.M41 = handPoseLeft.Position.X;//  + originPos.X + movePos.X;// + _hmdPoser.X;
                            _leftTouchMatrix.M42 = handPoseLeft.Position.Y;//  + originPos.Y + movePos.Y;// + _hmdPoser.Y;
                            _leftTouchMatrix.M43 = handPoseLeft.Position.Z;//  + originPos.Z + movePos.Z;// + _hmdPoser.Z;















































                            for (int i = 0; i < _screenDirMatrix_correct_pos.Length; i++)
                            {
                                point3DCollection[i].X = _screenDirMatrix_correct_pos[i].M41;
                                point3DCollection[i].Y = _screenDirMatrix_correct_pos[i].M42;
                                point3DCollection[i].Z = _screenDirMatrix_correct_pos[i].M43;

                                //point3DCollection[i] = new Vector3(_screenDirMatrix_correct_pos[i].M41, _screenDirMatrix_correct_pos[i].M42, _screenDirMatrix_correct_pos[i].M43);
                            }

                            _leftTouchMatrix.M41 = handPoseLeft.Position.X;
                            _leftTouchMatrix.M42 = handPoseLeft.Position.Y;
                            _leftTouchMatrix.M43 = handPoseLeft.Position.Z;

                            _rightTouchMatrix.M41 = handPoseRight.Position.X;
                            _rightTouchMatrix.M42 = handPoseRight.Position.Y;
                            _rightTouchMatrix.M43 = handPoseRight.Position.Z;







































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

                                j = 1;

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

                                j = 1;
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



                            _MicrosoftWindowsMouseRight(percentXRight, percentYRight, thumbStickRight, percentXLeft, percentYLeft, thumbStickLeft);

                            lastHasUsedHandTriggerLeft = hasUsedHandTriggerLeft;
                            lastbuttonPressedOculusTouchRight = buttonPressedOculusTouchRight;
                            lastbuttonPressedOculusTouchLeft = buttonPressedOculusTouchLeft;




                            //var absoluteMoveX = Convert.ToUInt32((percentXRight * 65535) / D3D.SurfaceWidth);
                            //var absoluteMoveY = Convert.ToUInt32((percentYRight * 65535) / D3D.SurfaceHeight);

                            _mouseCursorMatrix.M41 = (float)((percentXRight * 65535) / D3D.SurfaceWidth);
                            _mouseCursorMatrix.M42 = (float)((percentYRight * 65535) / D3D.SurfaceHeight);



                        }
                        catch (Exception ex)
                        {
                            MessageBox((IntPtr)0, ex.ToString(), "Oculus Error", 0);
                        }






                        LastRotationScreenX = RotationScreenX;
                        LastRotationScreenY = RotationScreenY;
                        LastRotationScreenZ = RotationScreenZ;




                        //_leftTouchMatrix = original_left_touch_matrix;
                        //_rightTouchMatrix = original_right_touch_matrix;









                        D3D.result = eyeTexture.SwapTextureSet.Commit();
                        D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to commit the swap chain texture.");
                    }

                    //Camera.SetPosition(realPos.X, realPos.Y, realPos.Z);
                    //var yo = OVR.GetTrackerPose(sessionPtr, (uint)TrackedDeviceType.HMD);
                    //var test = yo.Pose;
                    //realPos = new Vector3(test.Position.X, test.Position.Y, test.Position.Z);

                    D3D.result = D3D.OVR.SubmitFrame(D3D.sessionPtr, 0L, IntPtr.Zero, ref D3D.layerEyeFov);
                    D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to submit the frame of the current layers.");


                    D3D.DeviceContext.CopyResource(D3D.mirrorTextureD3D, D3D.BackBuffer);
                    D3D.SwapChain.Present(0, PresentFlags.None);
                    /*if (SC_Console_CORE._SC_GLOB._Activate_Desktop_Image == 1)
                    {
                        D3D.DeviceContext.CopyResource(D3D.mirrorTextureD3D, D3D.BackBuffer);
                        D3D.SwapChain.Present(0, PresentFlags.None);
                    }*/
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }


                _World_Step = deltaTime;



                /*if (_lastShaderResourceView != null)
                {
                    _lastShaderResourceView.Dispose();
                }

                _lastShaderResourceView = _desktopFrame._ShaderResource;
                */




                //Console.WriteLine(World.SoftBodies.Count);

                _last_final_hand_pos_right = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);
                _last_frame_handPos = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);

                if (_grabbed_body_right != null)
                {
                    _last_frame_rigid_grab_pos = new Vector3(_grabbed_body_right.Position.X, _grabbed_body_right.Position.Y, _grabbed_body_right.Position.Z);
                    _last_frame_rigid_grab_rot = _grabbed_body_right.Orientation;//new JQuaternion();// new Vector3(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z);
                }






                // Generate the view matrix based on the camera position.
                //Camera.Render();

                // Get the world, view, and projection matrices from camera and d3d objects.
                //Matrix viewMatrix = Camera.ViewMatrix;
                //Matrix worldMatrix = D3D.WorldMatrix;
                //Matrix projectionMatrix = D3D.ProjectionMatrix;

                // Put the model vertex and index buffers on the graphics pipeline to prepare them for drawing.
                /*Model.Render(D3D.DeviceContext);

                // Render the model using the color shader.
                if (!TextureShader.Render(D3D.DeviceContext, Model.IndexCount, worldMatrix, viewMatrix, projectionMatrix, Model.Texture.TextureResource))
                    return false;
                */
                // Present the rendered scene to the screen.
                //D3D.EndScene();



                _frameCounter = _mainThreadStopWatch.Elapsed.Seconds;
                if (_frameCounter >= 1)
                {
                    _mainThreadStopWatch.Stop();
                    //Console.SetCursorPosition(1, 3);
                    //Console.Write("FPS counter: " + _updateSceneFrameCounter);

                    _mainThreadStopWatch.Reset();
                    _mainThreadStopWatch.Start();
                    //_stopWatchSwitch = true;
                }



                if (_updateSceneFrameCounter > 1)
                {

                    /*Console.WriteLine(System.GC.GetGeneration(obj));

                    System.GC.Collect();

                    Console.WriteLine(System.GC.GetGeneration(obj));

                    System.GC.Collect();

                    Console.WriteLine(System.GC.GetGeneration(obj));
                    */


                    /*long test0 = System.GC.GetTotalMemory(false);

                    long test1 = System.GC.GetTotalMemory(true);


                    if (Math.Abs(test0 - test1) > 750000) //500k happens in the beginning
                    {
                        MessageBox((IntPtr)0, "test", "mouse move", 0);yh

                        Console.SetCursorPosition(1, 3);
                        Console.WriteLine("Total available memory before collection: {0:N0}", test0);

                        System.GC.Collect();
                        Console.SetCursorPosition(1, 4);
                        Console.WriteLine("Total available memory collection: {0:N0}", test1);
                    }*/

                    //GC.Collect();
                    _updateSceneFrameCounter = 0;
                }




                _updateSceneFrameCounter++;
                _console_display_frame_counter++;
                _console_display_counter++;
            }
            return _someReceivedObject0;
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

            Matrix.Multiply(ref somerotfinal,ref someRightTouchMatrix, out somerotfinal);


            return somerotfinal;
        }









        void _process_rigidbody_two(RigidBody rigidbody, int x, int y, int z, int count)
        {
            Matrix translationMatrix;
            Matrix.Translation(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z, out translationMatrix);

            JQuaternion quatterer = JQuaternion.CreateFromMatrix(rigidbody.Orientation);

            Quaternion tester;
            tester.X = quatterer.X;
            tester.Y = quatterer.Y;
            tester.Z = quatterer.Z;
            tester.W = quatterer.W;

            //Quaternion tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

            Matrix.RotationQuaternion(ref tester, out rotationMatrix);

            Matrix.Multiply(ref rotationMatrix, ref translationMatrix, out translationMatrix);
            worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix;








            if (!rigidbody.IsActive)
            {
                if (_has_grabbed_right_swtch == 1)
                {
                    //Vector3 poser = new Vector3(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z); //OFFSETPOS
                    Vector3 handPos = _last_final_hand_pos_right;// new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);
                    Vector3 poser = new Vector3(worldMatrix_instances[x + width * (y + height * z)][count].M41, worldMatrix_instances[x + width * (y + height * z)][count].M42, worldMatrix_instances[x + width * (y + height * z)][count].M43);


                    float dist;
                    Vector3.Distance(ref poser, ref handPos, out dist);




                    if (dist < 1)
                    {
                        _offset_grabbed_pos_norm = poser - handPos;
                        _offset_grabbed_pos = _offset_grabbed_pos_norm;

                        _offset_grabbed_pos_dist = dist;// _offset_grabbed_pos_norm.Length();
                        _offset_grabbed_pos_norm.Normalize();



                        bool _boundingBox = _world_list[x + width * (y + height * z)].CollisionSystem.CheckBoundingBoxes(rigidbody, _SC_visual_object_manager._humRig._player_right_hnd._singleObjectOnly.transform.Component.rigidbody);

                        if (!_boundingBox)
                        {
                            Matrix translationMatrix0;
                            Matrix.Translation(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z, out translationMatrix0);

                            JQuaternion quatterer0 = JQuaternion.CreateFromMatrix(rigidbody.Orientation);

                            Quaternion tester0;
                            tester0.X = quatterer0.X;
                            tester0.Y = quatterer0.Y;
                            tester0.Z = quatterer0.Z;
                            tester0.W = quatterer0.W;

                            //Quaternion tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

                            Matrix.RotationQuaternion(ref tester0, out rotationMatrix);

                            Matrix.Multiply(ref rotationMatrix, ref translationMatrix0, out translationMatrix0);
                            worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix0;


                            _process_rigidbody_that_are_currently_activated_or_not(rigidbody, x, y, z, count);
                        }
                        else
                        {
                            rigidbody.AngularVelocity = JVector.Zero;
                            rigidbody.LinearVelocity = JVector.Zero;

                            var rigibody_pos = new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z) + (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist);

                            var _last_min_dist = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z));
                            _last_min_dist.Normalize();
                            //Jitter.Collision.RaycastCallback someRay = new Jitter.Collision.RaycastCallback(rigidbody, lengtherDir, _last_offset_grabbed_pos_norm_dist);

                            JVector _ray_origin = new JVector(handPos.X, handPos.Y, handPos.Z);
                            JVector _ray_dir = new JVector(_last_min_dist.X, _last_min_dist.Y, _last_min_dist.Z);

                            World.CollisionSystem.Raycast(_ray_origin, _ray_dir, RaycastCallback, out _body_collision, out _body_collision_normal, out _body_collision_fraction);

                            //if (rigidbody == _body_collision)
                            {
                                rigidbody.AffectedByGravity = false;
                                rigidbody.IsActive = false;
                                _grab_hand_pos = handPos;
                                _grab_body_pos = poser;

                                _offset_grabbed_pos_norm = poser - handPos;
                                _offset_grabbed_pos = _offset_grabbed_pos_norm;

                                _offset_grabbed_pos_dist = dist;// _offset_grabbed_pos_norm.Length();
                                _offset_grabbed_pos_norm.Normalize();

                                _grabbed_body_right = rigidbody;

                                startnGrabbedX = 0;
                                startnGrabbedY = 0;
                                startnGrabbedZ = 0;

                                _grabbed_diff_x = poser.X - handPos.X;
                                _grabbed_diff_y = poser.Y - handPos.Y;
                                _grabbed_diff_z = poser.Z - handPos.Z;

                                //diffGrabbedX

                                Quaternion quattt;
                                Quaternion.RotationMatrix(ref final_hand_pos_right, out quattt);

                                _grabbed_body_pos_rot = rigidbody.Orientation;
                                _objects_static_00[x + width * (y + height * z)][count] = 2;
                                _objects_rigid_static_00[x + width * (y + height * z)][count] = rigidbody;

                                /*if (_has_grabbed_right == 0)
                                {
                                    //var rigibody_pos = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (_offset_grabbed_pos_norm * _offset_grabbed_pos_dist);
                                    var rigibody_pos = new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z) + (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist);
                                    //_last_min_distX = (rigibody_pos - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).X;
                                    //_last_min_distY = (rigibody_pos - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).Y;
                                    //_last_min_distZ = (rigibody_pos - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).Z;
                                    _last_min_distX = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z)).X;
                                    _last_min_distY = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z)).Y;
                                    _last_min_distZ = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z)).Z;
                                    _has_grabbed_right = 1;
                                }*/



                                _has_grabbed_right_swtch = 2;

                            }

                            _last_offset_grabbed_pos_norm = _offset_grabbed_pos_norm;
                            _last_offset_grabbed_pos_norm_dist = _offset_grabbed_pos_dist;
                        }
                    }
                    else
                    {
                        Matrix translationMatrix0;
                        Matrix.Translation(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z, out translationMatrix0);

                        JQuaternion quatterer0 = JQuaternion.CreateFromMatrix(rigidbody.Orientation);

                        Quaternion tester0;
                        tester0.X = quatterer0.X;
                        tester0.Y = quatterer0.Y;
                        tester0.Z = quatterer0.Z;
                        tester0.W = quatterer0.W;

                        Matrix.RotationQuaternion(ref tester0, out rotationMatrix);

                        Matrix.Multiply(ref rotationMatrix, ref translationMatrix0, out translationMatrix0);
                        worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix0;


                        _process_rigidbody_that_are_currently_activated_or_not(rigidbody, x, y, z, count);
                        _has_init_ray = 0;
                    }
                }
                else
                {
                    Matrix translationMatrix0;
                    Matrix.Translation(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z, out translationMatrix0);

                    JQuaternion quatterer0 = JQuaternion.CreateFromMatrix(rigidbody.Orientation);

                    Quaternion tester0;
                    tester0.X = quatterer0.X;
                    tester0.Y = quatterer0.Y;
                    tester0.Z = quatterer0.Z;
                    tester0.W = quatterer0.W;

                    //Quaternion tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

                    Matrix.RotationQuaternion(ref tester0, out rotationMatrix);

                    Matrix.Multiply(ref rotationMatrix, ref translationMatrix0, out translationMatrix0);
                    worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix0;


                    _process_rigidbody_that_are_currently_activated_or_not(rigidbody, x, y, z, count);
                }
            }
            else
            {
                if (_has_grabbed_right_swtch == 1)
                {
                    //Vector3 poser = new Vector3(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z); //OFFSETPOS
                    Vector3 handPos = _last_final_hand_pos_right;// new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);
                    Vector3 poser = new Vector3(worldMatrix_instances[x + width * (y + height * z)][count].M41, worldMatrix_instances[x + width * (y + height * z)][count].M42, worldMatrix_instances[x + width * (y + height * z)][count].M43);


                    float dist;
                    Vector3.Distance(ref poser, ref handPos, out dist);




                    if (dist < 1)
                    {
                        _offset_grabbed_pos_norm = poser - handPos;
                        _offset_grabbed_pos = _offset_grabbed_pos_norm;

                        _offset_grabbed_pos_dist = dist;// _offset_grabbed_pos_norm.Length();
                        _offset_grabbed_pos_norm.Normalize();



                        bool _boundingBox = _world_list[x + width * (y + height * z)].CollisionSystem.CheckBoundingBoxes(rigidbody, _SC_visual_object_manager._humRig._player_right_hnd._singleObjectOnly.transform.Component.rigidbody);

                        if (!_boundingBox)
                        {
                            Matrix translationMatrix0;
                            Matrix.Translation(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z, out translationMatrix0);

                            JQuaternion quatterer0 = JQuaternion.CreateFromMatrix(rigidbody.Orientation);

                            Quaternion tester0;
                            tester0.X = quatterer0.X;
                            tester0.Y = quatterer0.Y;
                            tester0.Z = quatterer0.Z;
                            tester0.W = quatterer0.W;

                            //Quaternion tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

                            Matrix.RotationQuaternion(ref tester0, out rotationMatrix);

                            Matrix.Multiply(ref rotationMatrix, ref translationMatrix0, out translationMatrix0);
                            worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix0;


                            _process_rigidbody_that_are_currently_activated_or_not(rigidbody, x, y, z, count);
                        }
                        else
                        {
                            rigidbody.AngularVelocity = JVector.Zero;
                            rigidbody.LinearVelocity = JVector.Zero;

                            var rigibody_pos = new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z) + (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist);

                            var _last_min_dist = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z));
                            _last_min_dist.Normalize();
                            //Jitter.Collision.RaycastCallback someRay = new Jitter.Collision.RaycastCallback(rigidbody, lengtherDir, _last_offset_grabbed_pos_norm_dist);

                            JVector _ray_origin = new JVector(handPos.X, handPos.Y, handPos.Z);

                            JVector _ray_dir = new JVector(_last_min_dist.X, _last_min_dist.Y, _last_min_dist.Z);

                            World.CollisionSystem.Raycast(_ray_origin, _ray_dir, RaycastCallback, out _body_collision, out _body_collision_normal, out _body_collision_fraction);

                            //if (rigidbody == _body_collision)
                            {
                                rigidbody.AffectedByGravity = false;
                                rigidbody.IsActive = false;
                                _grab_hand_pos = handPos;
                                _grab_body_pos = poser;

                                _offset_grabbed_pos_norm = poser - handPos;
                                _offset_grabbed_pos = _offset_grabbed_pos_norm;

                                _offset_grabbed_pos_dist = dist;// _offset_grabbed_pos_norm.Length();
                                _offset_grabbed_pos_norm.Normalize();

                                _grabbed_body_right = rigidbody;

                                startnGrabbedX = 0;
                                startnGrabbedY = 0;
                                startnGrabbedZ = 0;

                                _grabbed_diff_x = poser.X - handPos.X;
                                _grabbed_diff_y = poser.Y - handPos.Y;
                                _grabbed_diff_z = poser.Z - handPos.Z;

                                //diffGrabbedX

                                Quaternion quattt;
                                Quaternion.RotationMatrix(ref final_hand_pos_right, out quattt);

                                _grabbed_body_pos_rot = rigidbody.Orientation;
                                _objects_static_00[x + width * (y + height * z)][count] = 2;
                                _objects_rigid_static_00[x + width * (y + height * z)][count] = rigidbody;

                                /*if (_has_grabbed_right == 0)
                                {
                                    //var rigibody_pos = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (_offset_grabbed_pos_norm * _offset_grabbed_pos_dist);
                                    var rigibody_pos = new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z) + (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist);
                                    //_last_min_distX = (rigibody_pos - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).X;
                                    //_last_min_distY = (rigibody_pos - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).Y;
                                    //_last_min_distZ = (rigibody_pos - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).Z;
                                    _last_min_distX = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z)).X;
                                    _last_min_distY = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z)).Y;
                                    _last_min_distZ = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z)).Z;
                                    _has_grabbed_right = 1;
                                }*/



                                _has_grabbed_right_swtch = 2;

                            }

                            _last_offset_grabbed_pos_norm = _offset_grabbed_pos_norm;
                            _last_offset_grabbed_pos_norm_dist = _offset_grabbed_pos_dist;
                        }
                    }
                    else
                    {
                        Matrix translationMatrix0;
                        Matrix.Translation(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z, out translationMatrix0);

                        JQuaternion quatterer0 = JQuaternion.CreateFromMatrix(rigidbody.Orientation);

                        Quaternion tester0;
                        tester0.X = quatterer0.X;
                        tester0.Y = quatterer0.Y;
                        tester0.Z = quatterer0.Z;
                        tester0.W = quatterer0.W;

                        //Quaternion tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

                        Matrix.RotationQuaternion(ref tester0, out rotationMatrix);

                        Matrix.Multiply(ref rotationMatrix, ref translationMatrix0, out translationMatrix0);
                        worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix0;


                        _process_rigidbody_that_are_currently_activated_or_not(rigidbody, x, y, z, count);
                    }
                }
                else
                {
                    Matrix translationMatrix0;
                    Matrix.Translation(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z, out translationMatrix0);

                    JQuaternion quatterer0 = JQuaternion.CreateFromMatrix(rigidbody.Orientation);

                    Quaternion tester0;
                    tester0.X = quatterer0.X;
                    tester0.Y = quatterer0.Y;
                    tester0.Z = quatterer0.Z;
                    tester0.W = quatterer0.W;

                    //Quaternion tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

                    Matrix.RotationQuaternion(ref tester0, out rotationMatrix);

                    Matrix.Multiply(ref rotationMatrix, ref translationMatrix0, out translationMatrix0);
                    worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix0;


                    _process_rigidbody_that_are_currently_activated_or_not(rigidbody, x, y, z, count);
                }
            }



































            /*
            rigidbody.IsStatic = false;
            if (!rigidbody.IsActive)
            {
                bool _boundingBox = _world_list[x + width * (y + height * z)].CollisionSystem.CheckBoundingBoxes(rigidbody, _screenModel._singleObjectOnly.transform.Component.rigidbody);

                if (!_boundingBox)
                {
                    rigidbody.AffectedByGravity = false;
                    rigidbody.IsActive = false;
                    ////body.Is = true;
                }
                else
                {
                    rigidbody.AffectedByGravity = false;
                    rigidbody.IsActive = true;
                    //body.Is = false;
                    //Console.WriteLine("collision screen");
                    _switch_for_collision[x + width * (y + height * z)][count] = 1;
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
                                            int hasbreakeder = 0;
                                            bool _boundingBoxer = _world_list[x + width * (y + height * z)].CollisionSystem.CheckBoundingBoxes(rigidbody, _screenModel._singleObjectOnly.transform.Component.rigidbody);

                                            if (!_boundingBoxer)
                                            {
                                                if (currentLinearVel.Length() < 0.00035f && currentAngularVel.Length() < 0.00035f) //0.00035f
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
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                if (currentLinearVel.Length() < 0.00035f && currentAngularVel.Length() < 0.00035f) //0.00035f
                                                {
                                                    hasbreakeder = 1;
                                                    rigidbody.IsActive = true;
                                                    rigidbody.AffectedByGravity = false;
                                                    break;
                                                }
                                                else
                                                {
                                                    rigidbody.IsActive = true;
                                                    rigidbody.AffectedByGravity = true;

                                                    hasbreakeder = 1;
                                                    break;
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

                                        bool _boundingBoxer = _world_list[x + width * (y + height * z)].CollisionSystem.CheckBoundingBoxes(rigidbody, _screenModel._singleObjectOnly.transform.Component.rigidbody);

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
                                        }
                                    }
                                }
                            }
                            else
                            {
                                JVector currentLinearVel = rigidbody.LinearVelocity;
                                JVector currentAngularVel = rigidbody.AngularVelocity;



                                if (currentLinearVel.Length() < 0.00035f && currentAngularVel.Length() < 0.00035f) //0.00035f == 400 approx => 0.00075f == 400 approx
                                {
                                    rigidbody.IsActive = true;
                                    rigidbody.AffectedByGravity = true;
                                }
                                else
                                {
                                    rigidbody.IsActive = true;
                                    rigidbody.AffectedByGravity = true;

                                    //body.IsActive = true;
                                    //body.AffectedByGravity = true;
                                }
                            }

                        }
                        else
                        {
                            JVector currentLinearVel = rigidbody.LinearVelocity;
                            JVector currentAngularVel = rigidbody.AngularVelocity;

                            if (currentLinearVel.Length() < 0.00035f && currentAngularVel.Length() < 0.00035f) //0.00035f == 400 approx => 0.00075f == 400 approx
                            {
                                rigidbody.IsActive = true;
                                rigidbody.AffectedByGravity = true;
                            }
                            else
                            {
                                rigidbody.IsActive = true;
                                rigidbody.AffectedByGravity = true;
                                //body.IsActive = true;
                                //body.AffectedByGravity = true;
                            }

                        }
                    }
                    else
                    {
                        rigidbody.IsActive = true;
                        rigidbody.AffectedByGravity = true;
                    }
                }
                else
                {
                    rigidbody.IsActive = true;
                    rigidbody.AffectedByGravity = true;
                }

            }
        }








        void _process_rigidbody(RigidBody rigidbody, int x, int y, int z, int count)
        {


            if (!rigidbody.IsActive)
            {
                if (_has_grabbed_right_swtch == 1)
                {
                    //Vector3 poser = new Vector3(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z); //OFFSETPOS
                    Vector3 handPos = _last_final_hand_pos_right;// new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);
                    Vector3 poser = new Vector3(worldMatrix_instances[x + width * (y + height * z)][count].M41, worldMatrix_instances[x + width * (y + height * z)][count].M42, worldMatrix_instances[x + width * (y + height * z)][count].M43);


                    float dist;
                    Vector3.Distance(ref poser, ref handPos, out dist);




                    if (dist < 1)
                    {
                        _offset_grabbed_pos_norm = poser - handPos;
                        _offset_grabbed_pos = _offset_grabbed_pos_norm;

                        _offset_grabbed_pos_dist = dist;// _offset_grabbed_pos_norm.Length();
                        _offset_grabbed_pos_norm.Normalize();



                        bool _boundingBox = _world_list[x + width * (y + height * z)].CollisionSystem.CheckBoundingBoxes(rigidbody, _SC_visual_object_manager._humRig._player_right_hnd._singleObjectOnly.transform.Component.rigidbody);

                        if (!_boundingBox)
                        {
                            Matrix translationMatrix0;
                            Matrix.Translation(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z, out translationMatrix0);

                            JQuaternion quatterer0 = JQuaternion.CreateFromMatrix(rigidbody.Orientation);

                            Quaternion tester0;
                            tester0.X = quatterer0.X;
                            tester0.Y = quatterer0.Y;
                            tester0.Z = quatterer0.Z;
                            tester0.W = quatterer0.W;

                            //Quaternion tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

                            Matrix.RotationQuaternion(ref tester0, out rotationMatrix);

                            Matrix.Multiply(ref rotationMatrix, ref translationMatrix0, out translationMatrix0);
                            worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix0;


                            _process_rigidbody_that_are_currently_activated_or_not(rigidbody, x, y, z, count);
                        }
                        else
                        {

                            var rigibody_pos = new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z) + (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist);

                            var _last_min_dist = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z));
                            _last_min_dist.Normalize();
                            //Jitter.Collision.RaycastCallback someRay = new Jitter.Collision.RaycastCallback(rigidbody, lengtherDir, _last_offset_grabbed_pos_norm_dist);

                            JVector _ray_origin = new JVector(handPos.X, handPos.Y, handPos.Z);
                            JVector _ray_dir = new JVector(_last_min_dist.X, _last_min_dist.Y, _last_min_dist.Z);

                            World.CollisionSystem.Raycast(_ray_origin, _ray_dir, RaycastCallback, out _body_collision, out _body_collision_normal, out _body_collision_fraction);

                            //if (rigidbody == _body_collision)
                            {
                                rigidbody.AffectedByGravity = false;
                                rigidbody.IsActive = false;
                                _grab_hand_pos = handPos;
                                _grab_body_pos = poser;

                                _offset_grabbed_pos_norm = poser - handPos;
                                _offset_grabbed_pos = _offset_grabbed_pos_norm;

                                _offset_grabbed_pos_dist = dist;// _offset_grabbed_pos_norm.Length();
                                _offset_grabbed_pos_norm.Normalize();

                                _grabbed_body_right = rigidbody;

                                startnGrabbedX = 0;
                                startnGrabbedY = 0;
                                startnGrabbedZ = 0;

                                _grabbed_diff_x = poser.X - handPos.X;
                                _grabbed_diff_y = poser.Y - handPos.Y;
                                _grabbed_diff_z = poser.Z - handPos.Z;

                                //diffGrabbedX

                                Quaternion quattt;
                                Quaternion.RotationMatrix(ref final_hand_pos_right, out quattt);

                                _grabbed_body_pos_rot = rigidbody.Orientation;
                                _objects_static_00[x + width * (y + height * z)][count] = 2;
                                _objects_rigid_static_00[x + width * (y + height * z)][count] = rigidbody;

                                //if (_has_grabbed_right == 0)
                                //{
                                    //var rigibody_pos = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (_offset_grabbed_pos_norm * _offset_grabbed_pos_dist);
                                //    var rigibody_pos = new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z) + (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist);
                                    //_last_min_distX = (rigibody_pos - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).X;
                                    //_last_min_distY = (rigibody_pos - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).Y;
                                    //_last_min_distZ = (rigibody_pos - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).Z;
                                //    _last_min_distX = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z)).X;
                                //    _last_min_distY = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z)).Y;
                                //    _last_min_distZ = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z)).Z;
                                //    _has_grabbed_right = 1;
                                //}



                                _has_grabbed_right_swtch = 2;

                            }

                            _last_offset_grabbed_pos_norm = _offset_grabbed_pos_norm;
                            _last_offset_grabbed_pos_norm_dist = _offset_grabbed_pos_dist;
                        }
                    }
                    else
                    {
                        Matrix translationMatrix0;
                        Matrix.Translation(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z, out translationMatrix0);

                        JQuaternion quatterer0 = JQuaternion.CreateFromMatrix(rigidbody.Orientation);

                        Quaternion tester0;
                        tester0.X = quatterer0.X;
                        tester0.Y = quatterer0.Y;
                        tester0.Z = quatterer0.Z;
                        tester0.W = quatterer0.W;

                        Matrix.RotationQuaternion(ref tester0, out rotationMatrix);

                        Matrix.Multiply(ref rotationMatrix, ref translationMatrix0, out translationMatrix0);
                        worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix0;


                        _process_rigidbody_that_are_currently_activated_or_not(rigidbody, x, y, z, count);
                        _has_init_ray = 0;
                    }
                }
                else
                {
                    Matrix translationMatrix0;
                    Matrix.Translation(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z, out translationMatrix0);

                    JQuaternion quatterer0 = JQuaternion.CreateFromMatrix(rigidbody.Orientation);

                    Quaternion tester0;
                    tester0.X = quatterer0.X;
                    tester0.Y = quatterer0.Y;
                    tester0.Z = quatterer0.Z;
                    tester0.W = quatterer0.W;

                    //Quaternion tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

                    Matrix.RotationQuaternion(ref tester0, out rotationMatrix);

                    Matrix.Multiply(ref rotationMatrix, ref translationMatrix0, out translationMatrix0);
                    worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix0;


                    _process_rigidbody_that_are_currently_activated_or_not(rigidbody, x, y, z, count);
                }
            }
            else
            {
                if (_has_grabbed_right_swtch == 1)
                {
                    //Vector3 poser = new Vector3(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z); //OFFSETPOS
                    Vector3 handPos = _last_final_hand_pos_right;// new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);
                    Vector3 poser = new Vector3(worldMatrix_instances[x + width * (y + height * z)][count].M41, worldMatrix_instances[x + width * (y + height * z)][count].M42, worldMatrix_instances[x + width * (y + height * z)][count].M43);


                    float dist;
                    Vector3.Distance(ref poser, ref handPos, out dist);




                    if (dist < 1)
                    {
                        _offset_grabbed_pos_norm = poser - handPos;
                        _offset_grabbed_pos = _offset_grabbed_pos_norm;

                        _offset_grabbed_pos_dist = dist;// _offset_grabbed_pos_norm.Length();
                        _offset_grabbed_pos_norm.Normalize();



                        bool _boundingBox = _world_list[x + width * (y + height * z)].CollisionSystem.CheckBoundingBoxes(rigidbody, _SC_visual_object_manager._humRig._player_right_hnd._singleObjectOnly.transform.Component.rigidbody);

                        if (!_boundingBox)
                        {
                            Matrix translationMatrix0;
                            Matrix.Translation(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z, out translationMatrix0);

                            JQuaternion quatterer0 = JQuaternion.CreateFromMatrix(rigidbody.Orientation);

                            Quaternion tester0;
                            tester0.X = quatterer0.X;
                            tester0.Y = quatterer0.Y;
                            tester0.Z = quatterer0.Z;
                            tester0.W = quatterer0.W;

                            //Quaternion tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

                            Matrix.RotationQuaternion(ref tester0, out rotationMatrix);

                            Matrix.Multiply(ref rotationMatrix, ref translationMatrix0, out translationMatrix0);
                            worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix0;


                            _process_rigidbody_that_are_currently_activated_or_not(rigidbody, x, y, z, count);
                        }
                        else
                        {

                            var rigibody_pos = new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z) + (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist);

                            var _last_min_dist = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z));
                            _last_min_dist.Normalize();
                            //Jitter.Collision.RaycastCallback someRay = new Jitter.Collision.RaycastCallback(rigidbody, lengtherDir, _last_offset_grabbed_pos_norm_dist);

                            JVector _ray_origin = new JVector(handPos.X, handPos.Y, handPos.Z);

                            JVector _ray_dir = new JVector(_last_min_dist.X, _last_min_dist.Y, _last_min_dist.Z);

                            World.CollisionSystem.Raycast(_ray_origin, _ray_dir, RaycastCallback, out _body_collision, out _body_collision_normal, out _body_collision_fraction);

                            //if (rigidbody == _body_collision)
                            {
                                rigidbody.AffectedByGravity = false;
                                rigidbody.IsActive = false;
                                _grab_hand_pos = handPos;
                                _grab_body_pos = poser;

                                _offset_grabbed_pos_norm = poser - handPos;
                                _offset_grabbed_pos = _offset_grabbed_pos_norm;

                                _offset_grabbed_pos_dist = dist;// _offset_grabbed_pos_norm.Length();
                                _offset_grabbed_pos_norm.Normalize();

                                _grabbed_body_right = rigidbody;

                                startnGrabbedX = 0;
                                startnGrabbedY = 0;
                                startnGrabbedZ = 0;

                                _grabbed_diff_x = poser.X - handPos.X;
                                _grabbed_diff_y = poser.Y - handPos.Y;
                                _grabbed_diff_z = poser.Z - handPos.Z;

                                //diffGrabbedX

                                Quaternion quattt;
                                Quaternion.RotationMatrix(ref final_hand_pos_right, out quattt);

                                _grabbed_body_pos_rot = rigidbody.Orientation;
                                _objects_static_00[x + width * (y + height * z)][count] = 2;
                                _objects_rigid_static_00[x + width * (y + height * z)][count] = rigidbody;

                                //if (_has_grabbed_right == 0)
                                //{
                                //    //var rigibody_pos = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (_offset_grabbed_pos_norm * _offset_grabbed_pos_dist);
                                //    var rigibody_pos = new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z) + (_last_offset_grabbed_pos_norm * _last_offset_grabbed_pos_norm_dist);
                                    //_last_min_distX = (rigibody_pos - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).X;
                                    //_last_min_distY = (rigibody_pos - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).Y;
                                    //_last_min_distZ = (rigibody_pos - new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43)).Z;
                                //    _last_min_distX = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z)).X;
                                //    _last_min_distY = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z)).Y;
                                //    _last_min_distZ = (rigibody_pos - new Vector3(_last_final_hand_pos_right.X, _last_final_hand_pos_right.Y, _last_final_hand_pos_right.Z)).Z;
                                //    _has_grabbed_right = 1;
                                //}



                                _has_grabbed_right_swtch = 2;

                            }

                            _last_offset_grabbed_pos_norm = _offset_grabbed_pos_norm;
                            _last_offset_grabbed_pos_norm_dist = _offset_grabbed_pos_dist;
                        }
                    }
                    else
                    {
                        Matrix translationMatrix0;
                        Matrix.Translation(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z, out translationMatrix0);

                        JQuaternion quatterer0 = JQuaternion.CreateFromMatrix(rigidbody.Orientation);

                        Quaternion tester0;
                        tester0.X = quatterer0.X;
                        tester0.Y = quatterer0.Y;
                        tester0.Z = quatterer0.Z;
                        tester0.W = quatterer0.W;

                        //Quaternion tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

                        Matrix.RotationQuaternion(ref tester0, out rotationMatrix);

                        Matrix.Multiply(ref rotationMatrix, ref translationMatrix0, out translationMatrix0);
                        worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix0;


                        _process_rigidbody_that_are_currently_activated_or_not(rigidbody, x, y, z, count);
                    }
                }
                else
                {
                    Matrix translationMatrix0;
                    Matrix.Translation(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z, out translationMatrix0);

                    JQuaternion quatterer0 = JQuaternion.CreateFromMatrix(rigidbody.Orientation);

                    Quaternion tester0;
                    tester0.X = quatterer0.X;
                    tester0.Y = quatterer0.Y;
                    tester0.Z = quatterer0.Z;
                    tester0.W = quatterer0.W;

                    //Quaternion tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

                    Matrix.RotationQuaternion(ref tester0, out rotationMatrix);

                    Matrix.Multiply(ref rotationMatrix, ref translationMatrix0, out translationMatrix0);
                    worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix0;


                    _process_rigidbody_that_are_currently_activated_or_not(rigidbody, x, y, z, count);
                }
            }*/




        }





        void _process_rigidbody_that_are_currently_activated_or_not(RigidBody rigidbody, int x, int y, int z, int count)
        {
            if (!rigidbody.IsActive)
            {
                bool _boundingBox = _world_list[x + width * (y + height * z)].CollisionSystem.CheckBoundingBoxes(rigidbody, _screenModel._singleObjectOnly.transform.Component.rigidbody);

                if (!_boundingBox)
                {
                    rigidbody.AffectedByGravity = false;
                    rigidbody.IsActive = false;
                    ////body.Is = true;
                }
                else
                {
                    rigidbody.AffectedByGravity = false;
                    rigidbody.IsActive = true;
                    //body.Is = false;
                    //Console.WriteLine("collision screen");
                    _switch_for_collision[x + width * (y + height * z)][count] = 1;
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
                                            int hasbreakeder = 0;
                                            bool _boundingBoxer = _world_list[x + width * (y + height * z)].CollisionSystem.CheckBoundingBoxes(rigidbody, _screenModel._singleObjectOnly.transform.Component.rigidbody);

                                            if (!_boundingBoxer)
                                            {
                                                if (currentLinearVel.Length() < 0.00035f && currentAngularVel.Length() < 0.00035f) //0.00035f
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
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                if (currentLinearVel.Length() < 0.00035f && currentAngularVel.Length() < 0.00035f) //0.00035f
                                                {
                                                    hasbreakeder = 1;
                                                    rigidbody.IsActive = true;
                                                    rigidbody.AffectedByGravity = false;
                                                    break;
                                                }
                                                else
                                                {
                                                    rigidbody.IsActive = true;
                                                    rigidbody.AffectedByGravity = true;

                                                    hasbreakeder = 1;
                                                    break;
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

                                        bool _boundingBoxer = _world_list[x + width * (y + height * z)].CollisionSystem.CheckBoundingBoxes(rigidbody, _screenModel._singleObjectOnly.transform.Component.rigidbody);

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
                                        }
                                    }
                                }
                            }
                            else
                            {
                                JVector currentLinearVel = rigidbody.LinearVelocity;
                                JVector currentAngularVel = rigidbody.AngularVelocity;



                                if (currentLinearVel.Length() < 0.00035f && currentAngularVel.Length() < 0.00035f) //0.00035f == 400 approx => 0.00075f == 400 approx
                                {
                                    rigidbody.IsActive = true;
                                    rigidbody.AffectedByGravity = true;
                                }
                                else
                                {
                                    rigidbody.IsActive = true;
                                    rigidbody.AffectedByGravity = true;

                                    //body.IsActive = true;
                                    //body.AffectedByGravity = true;
                                }
                            }

                        }
                        else
                        {
                            JVector currentLinearVel = rigidbody.LinearVelocity;
                            JVector currentAngularVel = rigidbody.AngularVelocity;

                            if (currentLinearVel.Length() < 0.00035f && currentAngularVel.Length() < 0.00035f) //0.00035f == 400 approx => 0.00075f == 400 approx
                            {
                                rigidbody.IsActive = true;
                                rigidbody.AffectedByGravity = true;
                            }
                            else
                            {
                                rigidbody.IsActive = true;
                                rigidbody.AffectedByGravity = true;
                                //body.IsActive = true;
                                //body.AffectedByGravity = true;
                            }

                        }
                    }
                    else
                    {
                        rigidbody.IsActive = true;
                        rigidbody.AffectedByGravity = true;
                    }
                }
                else
                {
                    rigidbody.IsActive = true;
                    rigidbody.AffectedByGravity = true;
                }

            }
















            /*
            //just let this object go and don't do anything with it.
            if (!rigidbody.AffectedByGravity)
            {
                if (rigidbody.CollisionIsland != null)
                {
                    if (rigidbody.CollisionIsland.Bodies != null)
                    {
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
                                        bool _boundingBoxer = _world_list[x + width * (y + height * z)].CollisionSystem.CheckBoundingBoxes(rigidbody, _screenModel._singleObjectOnly.transform.Component.rigidbody);

                                        if (!_boundingBoxer)
                                        {
                                            if (currentLinearVel.Length() < 0.00015f && currentAngularVel.Length() < 0.00015f) //0.00015f
                                            {

                                                rigidbody.IsActive = false;
                                                rigidbody.AffectedByGravity = false;
                                                break;
                                            }
                                            else
                                            {
                                                rigidbody.IsActive = true;
                                                rigidbody.AffectedByGravity = true;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (currentLinearVel.Length() < 0.00015f && currentAngularVel.Length() < 0.00015f) //0.00015f
                                            {
                                                rigidbody.IsActive = false;
                                                rigidbody.AffectedByGravity = false;
                                                break;
                                            }
                                            else
                                            {
                                                rigidbody.IsActive = true;
                                                rigidbody.AffectedByGravity = true;
                                                break;
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
                                    RigidBody someCurrentData = (RigidBody)enumerator1.Current;
                                    JVector currentLinearVel = someCurrentData.LinearVelocity;
                                    JVector currentAngularVel = someCurrentData.AngularVelocity;

                                    bool _boundingBoxer = _world_list[x + width * (y + height * z)].CollisionSystem.CheckBoundingBoxes(rigidbody, _screenModel._singleObjectOnly.transform.Component.rigidbody);

                                    if (!_boundingBoxer)
                                    {
                                        if (currentLinearVel.Length() < 0.00015f && currentAngularVel.Length() < 0.00015f) //0.00015f == 400 approx => 0.00075f == 400 approx
                                        {
                                            rigidbody.IsActive = false;
                                            rigidbody.AffectedByGravity = false;
                                            break;
                                        }
                                        else
                                        {
                                            rigidbody.IsActive = true;
                                            rigidbody.AffectedByGravity = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (currentLinearVel.Length() < 0.00015f && currentAngularVel.Length() < 0.00015f) //0.00015f == 400 approx => 0.00075f == 400 approx
                                        {
                                            rigidbody.IsActive = false;
                                            rigidbody.AffectedByGravity = false;
                                            break;
                                        }
                                        else
                                        {
                                            rigidbody.IsActive = true;
                                            rigidbody.AffectedByGravity = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            JVector currentLinearVel = rigidbody.LinearVelocity;
                            JVector currentAngularVel = rigidbody.AngularVelocity;

                            if (currentLinearVel.Length() < 0.00015f && currentAngularVel.Length() < 0.00015f) //0.00015f == 400 approx => 0.00075f == 400 approx
                            {
                                rigidbody.IsActive = false;
                                rigidbody.AffectedByGravity = false;
                            }
                            else
                            {
                                rigidbody.IsActive = true;
                                rigidbody.AffectedByGravity = true;
                            }
                        }
                    }
                    else
                    {
                        JVector currentLinearVel = rigidbody.LinearVelocity;
                        JVector currentAngularVel = rigidbody.AngularVelocity;

                        if (currentLinearVel.Length() < 0.00015f && currentAngularVel.Length() < 0.00015f) //0.00015f == 400 approx => 0.00075f == 400 approx
                        {
                            rigidbody.IsActive = false;
                            rigidbody.AffectedByGravity = false;
                        }
                        else
                        {
                            rigidbody.IsActive = true;
                            rigidbody.AffectedByGravity = true;
                        }

                    }
                }
                else
                {
                    JVector currentLinearVel = rigidbody.LinearVelocity;
                    JVector currentAngularVel = rigidbody.AngularVelocity;

                    if (currentLinearVel.Length() < 0.00015f && currentAngularVel.Length() < 0.00015f) //0.00015f == 400 approx => 0.00075f == 400 approx
                    {
                        rigidbody.IsActive = false;
                        rigidbody.AffectedByGravity = false;
                    }
                    else
                    {
                        rigidbody.IsActive = true;
                        rigidbody.AffectedByGravity = true;
                    }
                }
            }
            else
            {
                JVector currentLinearVel = rigidbody.LinearVelocity;
                JVector currentAngularVel = rigidbody.AngularVelocity;

                if (currentLinearVel.Length() < 0.00015f && currentAngularVel.Length() < 0.00015f) //0.00015f == 400 approx => 0.00075f == 400 approx
                {
                    rigidbody.IsActive = false;
                    rigidbody.AffectedByGravity = false;
                }
                else
                {
                    rigidbody.IsActive = true;
                    rigidbody.AffectedByGravity = true;
                }
            }*/
        }










































        private void CollisionSystem_CollisionDetected(RigidBody body1, RigidBody body2, JVector point1, JVector point2, JVector normal, float penetration)
        {
            throw new NotImplementedException();
        }

        ShaderResourceView _lastShaderResourceView;



        KeyboardState _KeyboardState;
        public SharpDX.DirectInput.Keyboard _Keyboard;

        private bool ReadKeyboard()
        {
            directInput = new DirectInput();

            _Keyboard = new Keyboard(directInput);

            // Acquire the joystick
            _Keyboard.Properties.BufferSize = 128;






            var resultCode = SharpDX.DirectInput.ResultCode.Ok;
            _KeyboardState = new KeyboardState();

            try
            {
                // Read the keyboard device.
                _Keyboard.GetCurrentState(ref _KeyboardState);
            }
            catch (SharpDX.SharpDXException ex)
            {
                resultCode = ex.Descriptor; // ex.ResultCode;
            }
            catch (Exception)
            {
                return false;
            }

            // If the mouse lost focus or was not acquired then try to get control back.
            if (resultCode == SharpDX.DirectInput.ResultCode.InputLost || resultCode == SharpDX.DirectInput.ResultCode.NotAcquired)
            {
                try
                {
                    _Keyboard.Acquire();

                }
                catch
                { }

                return true;
            }

            if (resultCode == SharpDX.DirectInput.ResultCode.Ok)
                return true;

            return false;
        }


























































        private void _MicrosoftWindowsMouseRight(double percentXRight, double percentYRight, Vector2f[] thumbStickRight, double percentXLeft, double percentYLeft, Vector2f[] thumbStickLeft) //
        {
            if (_indexMouseMove == 0)
            {

                //Console.WriteLine(percentXRight + "_" + percentYRight);

                /////////////RIGHT OCULUS TOUCH/////////////////////////////////////////////////////////////////////////////////////
                if (percentXRight >= 0 && percentXRight <= D3D.SurfaceWidth && percentYRight >= 0 && percentYRight <= D3D.SurfaceHeight)
                {

                    var absoluteMoveX = Convert.ToUInt32((percentXRight * 65535) / D3D.SurfaceWidth);
                    var absoluteMoveY = Convert.ToUInt32((percentYRight * 65535) / D3D.SurfaceHeight);

                    var yo = _updateFunctionStopwatchRight.Elapsed.Milliseconds;

                    if (_hasLockedMouse == 0)
                    {
                        if (yo >= 5)
                        {
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
                            if (hasClickedBUTTONA == false)
                            {
                                if (_frameCounterTouchRight <= 20 && _canResetCounterTouchRightButtonA == true)
                                {
                                    mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, _lastMousePosXRight, _lastMousePosYRight, 0, 0);
                                    _frameCounterTouchRight = 0;
                                }

                                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                                _lastMousePosXRight = absoluteMoveX;
                                _lastMousePosYRight = absoluteMoveY;
                                _canResetCounterTouchRightButtonA = true;
                                hasClickedBUTTONA = true;
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
                    _out_of_bounds_right = 0;
                }
                else
                {
                    _out_of_bounds_right = 1;
                }


                //////////OCULUS TOUCH BUTTONS PRESSED////////////////////////////////////////
                if (hasClickedBUTTONA && buttonPressedOculusTouchRight == 0 || hasClickedBUTTONB && buttonPressedOculusTouchRight == 0)
                {
                    if (hasClickedBUTTONA && buttonPressedOculusTouchRight == 0)
                    {
                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                        hasClickedBUTTONA = false;
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



                            string windowsKeyboard = "osk";
                            string path;

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
                        else if (buttonPressedOculusTouchLeft == 1048576 && buttonPressedOculusTouchLeft != lastbuttonPressedOculusTouchLeft)
                        {
                            if (hasClickedHomeButtonTouchLeft == false)
                            {
                                D3D.OVR.RecenterTrackingOrigin(D3D.sessionPtr);
                                hasClickedHomeButtonTouchLeft = true;
                            }
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
                /////////////RIGHT OCULUS TOUCH////////////////////////////////////////////

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
                            if (hasClickedBUTTONA == false)
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
                                hasClickedBUTTONA = true;
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
                if (hasClickedBUTTONA && buttonPressedOculusTouchRight == 0 || hasClickedBUTTONB && buttonPressedOculusTouchRight == 0)
                {
                    if (hasClickedBUTTONA && buttonPressedOculusTouchRight == 0)
                    {
                        //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                        hasClickedBUTTONA = false;
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


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd,
            UInt32 Msg,
            IntPtr wParam,
            IntPtr lParam);























        /*public bool clearConsole(SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject0)
        {
            D3D.BeginScene(0.1f, 0.25f, 0.5f, 1f, _someReceivedObject0);
            D3D.EndScene();
            D3D.ClearSceneVisual();
            D3D.EndScene();

            return true;
        }*/

        public double AngleBetween(Vector2 vector1, Vector2 vector2)
        {
            double sin = vector1.X * vector2.Y - vector2.X * vector1.Y;
            double cos = vector1.X * vector2.X + vector1.Y * vector2.Y;

            return Math.Atan2(sin, cos) * (180 / Math.PI);
        }





        static Vector3 vector;
        static double num12;
        static double num2;
        static double num;
        static double num11;
        static double num10;
        static double num9;
        static double num8;
        static double num7;
        static double num6;
        static double num5;
        static double num4;
        static double num3;
        static double num15;
        static double num14;
        static double num13;





        //https://pastebin.com/fAFp6NnN
        public static Vector3 _getDirection(Vector3 value, SharpDX.Quaternion rotation)
        {
            num12 = rotation.X + rotation.X;
            num2 = rotation.Y + rotation.Y;
            num = rotation.Z + rotation.Z;
            num11 = rotation.W * num12;
            num10 = rotation.W * num2;
            num9 = rotation.W * num;
            num8 = rotation.X * num12;
            num7 = rotation.X * num2;
            num6 = rotation.X * num;
            num5 = rotation.Y * num2;
            num4 = rotation.Y * num;
            num3 = rotation.Z * num;
            num15 = ((value.X * ((1f - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
            num14 = ((value.X * (num7 + num9)) + (value.Y * ((1f - num8) - num3))) + (value.Z * (num4 - num11));
            num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((1f - num8) - num5));
            vector.X = (float)num15;
            vector.Y = (float)num14;
            vector.Z = (float)num13;
            return vector;
        }
















        public Vector2 RotatePoint(Vector2 pointToRotate, Vector2 centerPoint, float angleInDegrees)
        {
            var angleInRadians = angleInDegrees * (Math.PI / 180);
            var cosTheta = Math.Cos(angleInRadians);
            var sinTheta = Math.Sin(angleInRadians);
            //var tanTheta = Math.Tan(angleInRadians);

            var newX = (cosTheta * (pointToRotate.X - centerPoint.X) - sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X);
            var newY = (sinTheta * (pointToRotate.X - centerPoint.X) + cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y);
            //var newZ = (tanTheta * (pointToRotate.Z - centerPoint.Z) + cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Z);

            Vector2 newPos = new Vector2((float)newX, (float)newY);

            return newPos;
        }



        public void AffineTransformation(float scaling, ref Vector3 rotationCenter, ref Quaternion rotation, ref Vector3 translation, out Matrix result)
        {
            result = Scaling(scaling) * Translation(-rotationCenter) * RotationQuaternion(rotation) *
                Translation(rotationCenter) * Translation(translation);
        }

        public Matrix Scaling(float scale)
        {
            Matrix result = Matrix.Identity;
            result.M11 = result.M22 = result.M33 = scale;
            return result;
        }
        public Matrix Translation(Vector3 value)
        {
            Matrix result = Translation(ref value);
            return result;
        }

        public Matrix Translation(ref Vector3 value)
        {
            Matrix result;
            Translation(value.X, value.Y, value.Z, out result);
            return result;
        }
        public void Translation(float x, float y, float z, out Matrix result)
        {
            result = Matrix.Identity;
            result.M41 = x;
            result.M42 = y;
            result.M43 = z;
        }

        public Matrix RotationQuaternion(Quaternion rotation)
        {
            Matrix result;
            float xx = rotation.X * rotation.X;
            float yy = rotation.Y * rotation.Y;
            float zz = rotation.Z * rotation.Z;
            float xy = rotation.X * rotation.Y;
            float zw = rotation.Z * rotation.W;
            float zx = rotation.Z * rotation.X;
            float yw = rotation.Y * rotation.W;
            float yz = rotation.Y * rotation.Z;
            float xw = rotation.X * rotation.W;

            result = Matrix.Identity;
            result.M11 = 1.0f - (2.0f * (yy + zz));
            result.M12 = 2.0f * (xy + zw);
            result.M13 = 2.0f * (zx - yw);
            result.M21 = 2.0f * (xy - zw);
            result.M22 = 1.0f - (2.0f * (zz + xx));
            result.M23 = 2.0f * (yz + xw);
            result.M31 = 2.0f * (zx + yw);
            result.M32 = 2.0f * (yz - xw);
            result.M33 = 1.0f - (2.0f * (yy + xx));
            return result;
        }
    }
}











/*if (!body.IsActive)
{
    Vector3 bodyPos = new Vector3(body.Position.X, body.Position.Y, body.Position.Z);
    float outerDistancer;

    //Vector3 handposer = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);

    Vector3 handposer = new Vector3(OFFSETPOS.X, OFFSETPOS.Y, OFFSETPOS.Z);
    Vector3.Distance(ref handposer, ref bodyPos, out outerDistancer);

    if (outerDistancer <= 5f)
    {
        Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);

        quatterer = JQuaternion.CreateFromMatrix(body.Orientation);

        tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

        Matrix.RotationQuaternion(ref tester, out rotationMatrix);

        Matrix.Multiply(ref rotationMatrix, ref translationMatrix, out translationMatrix);
        worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix;

        //body.Is = false;
        body.IsActive = true;
    }
    else
    {
        body.IsActive = false;
        ////body.Is = true;

        Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);

        quatterer = JQuaternion.CreateFromMatrix(body.Orientation);

        tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

        Matrix.RotationQuaternion(ref tester, out rotationMatrix);

        Matrix.Multiply(ref rotationMatrix, ref translationMatrix, out translationMatrix);
        worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix;
    }
}
else
{
    body.IsActive = true;
    //body.Is = false;

    Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);

    quatterer = JQuaternion.CreateFromMatrix(body.Orientation);

    tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

    Matrix.RotationQuaternion(ref tester, out rotationMatrix);

    Matrix.Multiply(ref rotationMatrix, ref translationMatrix, out translationMatrix);
    worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix;
}*/





/*_messager = new SC_Console_WRITER._messager();
_messager._message = buttonPressedOculusTouchRight + "";
_messager._originalMsg = _messager._message;
_messager._messageCut = _messager._message;
_messager._specialMessage = 0;
_messager._specialMessageLineX = 0;
_messager._specialMessageLineY = 0;
_messager._orilineX = 1;
_messager._orilineY = 1;
_messager._lineX = 1;
_messager._lineY = 1;
_messager._count = 0;
_messager._swtch0 = 1;
_messager._swtch1 = 0;
_messager._delay = 1;
_messager._looping = 0;

_currentWriter._message_to_pass_list.Add(_messager);

_messager = new SC_Console_WRITER._messager();
_messager._message = buttonPressedOculusTouchLeft + "";
_messager._originalMsg = _messager._message;
_messager._messageCut = _messager._message;
_messager._specialMessage = 0;
_messager._specialMessageLineX = 0;
_messager._specialMessageLineY = 0;
_messager._orilineX = 1;
_messager._orilineY = 2;
_messager._lineX = 1;
_messager._lineY = 2;
_messager._count = 0;
_messager._swtch0 = 1;
_messager._swtch1 = 0;
_messager._delay = 1;
_messager._looping = 0;

_currentWriter._message_to_pass_list.Add(_messager);*/










/*if (_test == 0)
                          {            
                              Task _mainTasker00 = Task<object[]>.Factory.StartNew((tester0000) =>
                              {                                    
                                  time01 = DateTime.Now;
                                  time02 = DateTime.Now;

                              _thread_looper:

                                  time02 = DateTime.Now;
                                  deltaTime = (time02.Ticks - time01.Ticks) / 1000000000f;
                                  _World_Step = deltaTime;



                                  Thread.Sleep(0);

                                  goto _thread_looper;
                              }, _someReceivedObject0);

                              var backgroundWorker = new BackgroundWorker();
                              backgroundWorker.DoWork += (object sender, DoWorkEventArgs args) =>
                              {
                              _thread_loop:
                                  if (_world_list[x + width * (y + height * z)] != null)
                                  {
                                      //startTime.Now.Second; //deltaTime;//
                                      if (_World_Step > 1.0f * 0.01f)
                                      {
                                          _World_Step = 1.0f * 0.01f;
                                      }
                                      _world_list[x + width * (y + height * z)].Step(_World_Step, true);
                                  }
                                  Thread.Sleep(0);
                                  goto _thread_loop;
                              };

                              backgroundWorker.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args)
                              {

                              };

                              backgroundWorker.RunWorkerAsync();
                              _test = 1;
                          }*/



/*Func<int> formatDelegate = () =>
{

    return 1;
};

var t2 = new Task<int>(formatDelegate);
t2.RunSynchronously();
t2.Dispose();*/












































/*
if (_swtch_hasRotated == 1)
{


    var pitch = (float)(RotationGrabbedX * 0.0174532925f);
    var yaw = (float)(RotationGrabbedY * 0.0174532925f);
    var roll = (float)(RotationGrabbedZ * 0.0174532925f);

    rotatingMatrixForGrabber = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);




    Matrix humanBodyRotation =  originRot * rotatingMatrix * rotatingMatrixForGrabber;
    humanBodyRotation.M41 = 0;
    humanBodyRotation.M42 = 0;
    humanBodyRotation.M43 = 0;
    humanBodyRotation.M44 = 1;


    JQuaternion quatterer = JQuaternion.CreateFromMatrix(_grabbed_body_pos_rot);

    Quaternion tester;
    tester.X = quatterer.X;
    tester.Y = quatterer.Y;
    tester.Z = quatterer.Z;
    tester.W = quatterer.W;

    Matrix rigidBodyMatrix;
    Matrix.RotationQuaternion(ref tester, out rigidBodyMatrix);


    var someRotationFinal = rigidBodyMatrix * humanBodyRotation;


    //TORSO PIVOT
    Vector3 MOVINGPOINTER = new Vector3(_SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M41, _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M42, _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M43);

    //SAVING IN MEMORY THE ORIGINAL TORSO MATRIX NOT AFFECTED BY CURRENT POSITION AND ROTATION CHANGES.
    Matrix _rotMatrixer = _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION;
    Quaternion forTest;
    Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

    //FROM THE MATRIX OF ROTATION/POSITION, I GET THE QUATERNION OUT OF THAT AND CREATE THE DIRECTIONS THAT THE OBJECTS ARE ORIGINALLY FACING.
    var direction_feet_forward_ori_torso = _getDirection(Vector3.ForwardRH, forTest);
    var direction_feet_right_ori_torso = _getDirection(Vector3.Right, forTest);
    var direction_feet_up_ori_torso = _getDirection(Vector3.Up, forTest);

    //SINCE THE PIVOT POINT IS CURRENTLY IN THE MIDDLE OF THE TORSO, IT CANNOT ROTATE AT THAT POINT OTHERWISE, IT WONT FOLLOW THE PELVIS ROTATION LATER ON.
    //SO WE CURRENTLY ONLY OFFSET THE TORSO "MIDDLE OF SPINE APPROX" TO HALF OF THE CURRENT HEIGHT IN ORDER TO MAKE THE PIVOT POINT, APPROX WHERE THE PELVIS IS.
    Vector3 TORSOPIVOT = MOVINGPOINTER + -(direction_feet_up_ori_torso * (_SC_visual_object_manager._humRig._player_torso._total_torso_height * 0.5f));





    _rightTouchMatrix.M41 = handPoseRight.Position.X;
    _rightTouchMatrix.M42 = handPoseRight.Position.Y;
    _rightTouchMatrix.M43 = handPoseRight.Position.Z;


    //Vector3 fuckoff = new Vector3(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z) - OFFSETPOS;





    Quaternion.RotationMatrix(ref humanBodyRotation, out otherQuat);

    //CONVERTING THE QUATERNION OF THAT TO THE DIRECTION OF ITS ROTATION
    var direction_feet_forward_torso = _getDirection(Vector3.ForwardRH, otherQuat);
    var direction_feet_right_torso = _getDirection(Vector3.Right, otherQuat);
    var direction_feet_up_torso = _getDirection(Vector3.Up, otherQuat);

    //I AM CALCULATING THE DIFFERENCE IN THE MOVEMENT FROM THE ORIGINAL POSITION TO THE CURRENT OFFSET AT THE BOTTOM OF THE SPINE WHERE I MOVED THAT POINT.
    var someTesting = (_offset_grabbed_pos * _offset_grabbed_pos_dist);



    var diffNormPosX = (MOVINGPOINTER.X) - (_rightTouchMatrix.M41 + someTesting.X);
    var diffNormPosY = (MOVINGPOINTER.Y) - (_rightTouchMatrix.M42 + someTesting.Y);
    var diffNormPosZ = (MOVINGPOINTER.Z) - (_rightTouchMatrix.M43 + someTesting.Z);

    //I AM USING THE NEW PIVOT POINT AT THE BOTTOM OF THE SPINE AND ADDING THE FRONT/RIGHT/UP VECTOR OF THE ROTATION OF THAT SPINE AND THEN ADDING THE DIFFERENCE X/W/Z BETWEEN ORIGINAL POS AND THE NEW PIVOT POS
    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right_torso * (diffNormPosX));
    MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_torso * (diffNormPosY));
    MOVINGPOINTER = MOVINGPOINTER + (direction_feet_forward_torso * (diffNormPosZ));

    MOVINGPOINTER.X += OFFSETPOS.X;
    MOVINGPOINTER.Y += OFFSETPOS.Y;
    MOVINGPOINTER.Z += OFFSETPOS.Z;

    var handPoser = MOVINGPOINTER;

    //var handPoser = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (_offset_grabbed_pos * _offset_grabbed_pos_dist);





    someRotationFinal.M41 = handPoser.X;
    someRotationFinal.M42 = handPoser.Y;
    someRotationFinal.M43 = handPoser.Z;


    worldMatrix_instances[x + width * (y + height * z)][count] = someRotationFinal;

    Quaternion quat;
    Quaternion.RotationMatrix(ref someRotationFinal, out quat);

    JQuaternion quatterer0;
    quatterer0.X = quat.X;
    quatterer0.Y = quat.Y;
    quatterer0.Z = quat.Z;
    quatterer0.W = quat.W;



    rigidbody.Orientation = JMatrix.CreateFromQuaternion(quatterer0);
    rigidbody.Position = new JVector(handPoser.X, handPoser.Y, handPoser.Z);



    _swtch_hasRotated = 2;
}
else
{
    if (_swtch_hasRotated == 0)
    {
        Matrix humanRigRotation = originRot * rotatingMatrix;
        humanRigRotation.M41 = 0;
        humanRigRotation.M42 = 0;
        humanRigRotation.M43 = 0;
        humanRigRotation.M44 = 1;
        JQuaternion quatterer = JQuaternion.CreateFromMatrix(_grabbed_body_pos_rot); //_grabbed_body_pos_rot

        Quaternion tester;
        tester.X = quatterer.X;
        tester.Y = quatterer.Y;
        tester.Z = quatterer.Z;
        tester.W = quatterer.W;

        Matrix.RotationQuaternion(ref tester, out rotationMatrix);


        //var someRotationFinal = rotationMatrix * humanRigRotation;


        var someRotationFinal = rotationMatrix * humanRigRotation;


        var handPoser = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (_offset_grabbed_pos * _offset_grabbed_pos_dist);


        //humanRigRotation = humanRigRotation * rotatingMatrixForPelvis;


        someRotationFinal.M41 = handPoser.X;
        someRotationFinal.M42 = handPoser.Y;
        someRotationFinal.M43 = handPoser.Z;


        worldMatrix_instances[x + width * (y + height * z)][count] = someRotationFinal;

        Quaternion quat;
        Quaternion.RotationMatrix(ref someRotationFinal, out quat);

        JQuaternion quatterer0;
        quatterer0.X = quat.X;
        quatterer0.Y = quat.Y;
        quatterer0.Z = quat.Z;
        quatterer0.W = quat.W;



        rigidbody.Orientation = JMatrix.CreateFromQuaternion(quatterer0);
        rigidbody.Position = new JVector(handPoser.X, handPoser.Y, handPoser.Z);

    }
    else if (_swtch_hasRotated == 2)
    {












        Matrix humanBodyRotation = originRot * rotatingMatrix * rotatingMatrixForGrabber;
        humanBodyRotation.M41 = 0;
        humanBodyRotation.M42 = 0;
        humanBodyRotation.M43 = 0;
        humanBodyRotation.M44 = 1;


        JQuaternion quatterer = JQuaternion.CreateFromMatrix(_grabbed_body_pos_rot);

        Quaternion tester;
        tester.X = quatterer.X;
        tester.Y = quatterer.Y;
        tester.Z = quatterer.Z;
        tester.W = quatterer.W;

        Matrix rigidBodyMatrix;
        Matrix.RotationQuaternion(ref tester, out rigidBodyMatrix);


        var someRotationFinal = rigidBodyMatrix * humanBodyRotation;


        //TORSO PIVOT
        Vector3 MOVINGPOINTER = new Vector3(_SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M41, _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M42, _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION.M43);

        //SAVING IN MEMORY THE ORIGINAL TORSO MATRIX NOT AFFECTED BY CURRENT POSITION AND ROTATION CHANGES.
        Matrix _rotMatrixer = _SC_visual_object_manager._humRig._player_torso._ORIGINPOSITION;
        Quaternion forTest;
        Quaternion.RotationMatrix(ref _rotMatrixer, out forTest);

        //FROM THE MATRIX OF ROTATION/POSITION, I GET THE QUATERNION OUT OF THAT AND CREATE THE DIRECTIONS THAT THE OBJECTS ARE ORIGINALLY FACING.
        var direction_feet_forward_ori_torso = _getDirection(Vector3.ForwardRH, forTest);
        var direction_feet_right_ori_torso = _getDirection(Vector3.Right, forTest);
        var direction_feet_up_ori_torso = _getDirection(Vector3.Up, forTest);

        //SINCE THE PIVOT POINT IS CURRENTLY IN THE MIDDLE OF THE TORSO, IT CANNOT ROTATE AT THAT POINT OTHERWISE, IT WONT FOLLOW THE PELVIS ROTATION LATER ON.
        //SO WE CURRENTLY ONLY OFFSET THE TORSO "MIDDLE OF SPINE APPROX" TO HALF OF THE CURRENT HEIGHT IN ORDER TO MAKE THE PIVOT POINT, APPROX WHERE THE PELVIS IS.
        Vector3 TORSOPIVOT = MOVINGPOINTER + -(direction_feet_up_ori_torso * (_SC_visual_object_manager._humRig._player_torso._total_torso_height * 0.5f));





        _rightTouchMatrix.M41 = handPoseRight.Position.X;
        _rightTouchMatrix.M42 = handPoseRight.Position.Y;
        _rightTouchMatrix.M43 = handPoseRight.Position.Z;


        //Vector3 fuckoff = new Vector3(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z) - OFFSETPOS;





        Quaternion.RotationMatrix(ref humanBodyRotation, out otherQuat);

        //CONVERTING THE QUATERNION OF THAT TO THE DIRECTION OF ITS ROTATION
        var direction_feet_forward_torso = _getDirection(Vector3.ForwardRH, otherQuat);
        var direction_feet_right_torso = _getDirection(Vector3.Right, otherQuat);
        var direction_feet_up_torso = _getDirection(Vector3.Up, otherQuat);

        //I AM CALCULATING THE DIFFERENCE IN THE MOVEMENT FROM THE ORIGINAL POSITION TO THE CURRENT OFFSET AT THE BOTTOM OF THE SPINE WHERE I MOVED THAT POINT.
        var someTesting = (_offset_grabbed_pos * _offset_grabbed_pos_dist);



        var diffNormPosX = (MOVINGPOINTER.X) - (_rightTouchMatrix.M41 + someTesting.X);
        var diffNormPosY = (MOVINGPOINTER.Y) - (_rightTouchMatrix.M42 + someTesting.Y);
        var diffNormPosZ = (MOVINGPOINTER.Z) - (_rightTouchMatrix.M43 + someTesting.Z);

        //I AM USING THE NEW PIVOT POINT AT THE BOTTOM OF THE SPINE AND ADDING THE FRONT/RIGHT/UP VECTOR OF THE ROTATION OF THAT SPINE AND THEN ADDING THE DIFFERENCE X/W/Z BETWEEN ORIGINAL POS AND THE NEW PIVOT POS
        MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_right_torso * (diffNormPosX));
        MOVINGPOINTER = MOVINGPOINTER + -(direction_feet_up_torso * (diffNormPosY));
        MOVINGPOINTER = MOVINGPOINTER + (direction_feet_forward_torso * (diffNormPosZ));

        MOVINGPOINTER.X += OFFSETPOS.X;
        MOVINGPOINTER.Y += OFFSETPOS.Y;
        MOVINGPOINTER.Z += OFFSETPOS.Z;

        var handPoser = MOVINGPOINTER;

        //var handPoser = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43) + (_offset_grabbed_pos * _offset_grabbed_pos_dist);





        someRotationFinal.M41 = handPoser.X;
        someRotationFinal.M42 = handPoser.Y;
        someRotationFinal.M43 = handPoser.Z;


        worldMatrix_instances[x + width * (y + height * z)][count] = someRotationFinal;

        Quaternion quat;
        Quaternion.RotationMatrix(ref someRotationFinal, out quat);

        JQuaternion quatterer0;
        quatterer0.X = quat.X;
        quatterer0.Y = quat.Y;
        quatterer0.Z = quat.Z;
        quatterer0.W = quat.W;



        rigidbody.Orientation = JMatrix.CreateFromQuaternion(quatterer0);
        rigidbody.Position = new JVector(handPoser.X, handPoser.Y, handPoser.Z);



        _swtch_hasRotated = 2;

    }
}*/










//Quaternion quattt;
//Quaternion.RotationMatrix(ref someRotMatrix, out quattt);

//Matrix rotationMatrixHand;
//Matrix.RotationQuaternion(ref quattt, out rotationMatrixHand);

//JQuaternion quatterer = JQuaternion.CreateFromMatrix(_grabbed_body_pos_rot);


//JQuaternion quatterer = JQuaternion.CreateFromMatrix(_grabbed_body_pos_rot);

/*Quaternion tester;
tester.X = quatterer.X;
tester.Y = quatterer.Y;
tester.Z = quatterer.Z;
tester.W = quatterer.W;

Matrix.RotationQuaternion(ref tester, out rotationMatrix);*/




/*float xq = quatterer.X;
float yq = quatterer.Y;
float zq = quatterer.Z;
float wq = quatterer.W;

float roll = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI));
float pitch = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI));
float yaw = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI));

RotationGrabbedX = pitch;
RotationGrabbedY = yaw;
RotationGrabbedZ = roll;



Quaternion quattt;
Quaternion.RotationMatrix(ref rotationMatrixHand, out quattt);

xq = quattt.X;
yq = quattt.Y;
zq = quattt.Z;
wq = quattt.W;

roll = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI));
pitch = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI));
yaw = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Math.PI));

var RotationHandX = pitch;
var RotationHandY = yaw;
var RotationHandZ = roll;


var totalpitchX = -(RotationHandX - diffGrabbedX);
var totalyawY = -(RotationHandY - diffGrabbedY);
var totalrollZ = -(RotationHandZ - diffGrabbedZ);

RotationGrabbedX += totalpitchX;
RotationGrabbedY += totalyawY;
RotationGrabbedZ += totalrollZ;

RotationGrabbedX = Math.PI * RotationGrabbedX / 180.0f;
RotationGrabbedY = Math.PI * RotationGrabbedY / 180.0f;
RotationGrabbedZ = Math.PI * RotationGrabbedZ / 180.0f;


var finalRotter = SharpDX.Matrix.RotationYawPitchRoll((float)RotationGrabbedY, (float)RotationGrabbedX, (float)RotationGrabbedZ);


var handPoser = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);
finalRotter.M41 = handPoser.X;
finalRotter.M42 = handPoser.Y;
finalRotter.M43 = handPoser.Z;




worldMatrix_instances[x + width * (y + height * z)][count] = finalRotter;




Quaternion quat;
Quaternion.RotationMatrix(ref finalRotter, out quat);

JQuaternion quatterer0;
quatterer0.X = quat.X;
quatterer0.Y = quat.Y;
quatterer0.Z = quat.Z;
quatterer0.W = quat.W;



rigidbody.Orientation = JMatrix.CreateFromQuaternion(quatterer0);
rigidbody.Position = new JVector(handPoser.X, handPoser.Y, handPoser.Z);*/






















//rotationMatrixHand = rotationMatrixHand * humanRigRotation * rotationMatrix;

/*var handPoser = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);

rotationMatrixHand.M41 = handPoser.X;
rotationMatrixHand.M42 = handPoser.Y;
rotationMatrixHand.M43 = handPoser.Z;


worldMatrix_instances[x + width * (y + height * z)][count] = rotationMatrixHand;




Quaternion quat;
Quaternion.RotationMatrix(ref rotationMatrixHand, out quat);

JQuaternion quatterer0;
quatterer0.X = quat.X;
quatterer0.Y = quat.Y;
quatterer0.Z = quat.Z;
quatterer0.W = quat.W;

rigidbody.Orientation = JMatrix.CreateFromQuaternion(quatterer0);
rigidbody.Position = new JVector(handPoser.X, handPoser.Y, handPoser.Z);*/






///Matrix.Multiply(ref rotationMatrix, ref rotationMatrixHand, out rotationMatrixHand);









//translationMatrix0 = rotationMatrix * translationMatrix0;
//Matrix.Multiply(ref rotationMatrix, ref rotationMatrixHand, out rotationMatrixHand);










/*var handPoser = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);// + (_offset_grabbed_pos * _offset_grabbed_pos_dist);


rotationMatrixHand =  rotationMatrixHand * humanRigRotation;

rotationMatrixHand.M41 = handPoser.X;
rotationMatrixHand.M42 = handPoser.Y;
rotationMatrixHand.M43 = handPoser.Z;

Quaternion quat0;
Quaternion.RotationMatrix(ref rotationMatrixHand, out quat0);

JQuaternion quatterer0;
quatterer0.X = quat0.X;
quatterer0.Y = quat0.Y;
quatterer0.Z = quat0.Z;
quatterer0.W = quat0.W;

JMatrix fuckoff = JMatrix.CreateFromQuaternion(quatterer0);

//_grabbed_body_pos_rot = fuckoff;




worldMatrix_instances[x + width * (y + height * z)][count] = rotationMatrixHand;





Quaternion quat;
Quaternion.RotationMatrix(ref rotationMatrixHand, out quat);

//JQuaternion quatterer0;
quatterer0.X = quat.X;
quatterer0.Y = quat.Y;
quatterer0.Z = quat.Z;
quatterer0.W = quat.W;

rigidbody.Orientation = JMatrix.CreateFromQuaternion(quatterer0);
rigidbody.Position = new JVector(handPoser.X, handPoser.Y, handPoser.Z);*/








/*Matrix _rotatingMatrix = translationMatrix0;

Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);
direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
direction_feet_right = _getDirection(Vector3.Right, otherQuat);
direction_feet_up = _getDirection(Vector3.Up, otherQuat);

Vector3 current_rotation_of_torso_pivot_forward = direction_feet_forward;
Vector3 current_rotation_of_torso_pivot_right = direction_feet_right;
Vector3 current_rotation_of_torso_pivot_up = direction_feet_up;*/











/*Matrix someRotMatrix = _SC_visual_object_manager._humRig._player_right_hnd._singleObjectOnly._POSITION;

Quaternion quattt;
Quaternion.RotationMatrix(ref someRotMatrix, out quattt);

Matrix translationMatrix0;
Matrix.RotationQuaternion(ref quattt, out translationMatrix0);



Matrix _rotatingMatrix = translationMatrix0;

Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);
direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
direction_feet_right = _getDirection(Vector3.Right, otherQuat);
direction_feet_up = _getDirection(Vector3.Up, otherQuat);

Vector3 current_rotation_of_torso_pivot_forward = direction_feet_forward;
Vector3 current_rotation_of_torso_pivot_right = direction_feet_right;
Vector3 current_rotation_of_torso_pivot_up = direction_feet_up;

var MOVINGPOINTER = new Vector3(someRotMatrix.M41, someRotMatrix.M42, someRotMatrix.M43);

var testerer = _offset_grabbed_pos * _offset_grabbed_pos_dist;
var diffNormPosX = testerer.X;
var diffNormPosY = testerer.Y;
var diffNormPosZ = testerer.Z;

MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_right * (diffNormPosX));
MOVINGPOINTER = MOVINGPOINTER + -(current_rotation_of_torso_pivot_up * (diffNormPosY));
MOVINGPOINTER = MOVINGPOINTER + (current_rotation_of_torso_pivot_forward * (diffNormPosZ));

var handPoser = MOVINGPOINTER;// (_offset_grabbed_pos * _offset_grabbed_pos_dist);


Matrix.Translation(handPoser.X, handPoser.Y, handPoser.Z, out translationMatrix0);




JQuaternion quatterer = JQuaternion.CreateFromMatrix(rigidbody.Orientation);

Quaternion tester;
tester.X = quatterer.X;
tester.Y = quatterer.Y;
tester.Z = quatterer.Z;
tester.W = quatterer.W;

Matrix.RotationQuaternion(ref tester, out rotationMatrix);



Quaternion.RotationMatrix(ref rotationMatrix, out quattt);

Matrix.RotationQuaternion(ref quattt, out _rotatingMatrix);

Matrix.Multiply(ref _rotatingMatrix, ref translationMatrix0, out translationMatrix0);


worldMatrix_instances[x + width * (y + height * z)][count] = translationMatrix0;






//rigidbody.Orientation = JMatrix.CreateFromQuaternion(quatterer);
rigidbody.Position = new JVector(handPoser.X, handPoser.Y, handPoser.Z);*/



//MessageBox((IntPtr)0, "COLLIDING WITH RIGHT HAND01", "Oculus error", 0);

/* JQuaternion quatterer = JQuaternion.CreateFromMatrix(rigidbody.Orientation);

 Quaternion tester;
 tester.X = quatterer.X;
 tester.Y = quatterer.Y;
 tester.Z = quatterer.Z;
 tester.W = quatterer.W;

 Matrix.RotationQuaternion(ref tester, out rotationMatrix);
 */

/*Matrix _rotatingMatrix = final_hand_pos_right; //_SC_visual_object_manager._humRig._player_right_hnd._POSITION


//_rotatingMatrix = _rotatingMatrix * final_hand_pos_right;


Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);
direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
direction_feet_right = _getDirection(Vector3.Right, otherQuat);
direction_feet_up = _getDirection(Vector3.Up, otherQuat);

Vector3 current_rotation_of_torso_pivot_forward = direction_feet_forward;
Vector3 current_rotation_of_torso_pivot_right = direction_feet_right;
Vector3 current_rotation_of_torso_pivot_up = direction_feet_up;


var MOVINGPOINTER = new Vector3(final_hand_pos_right.M41, final_hand_pos_right.M42, final_hand_pos_right.M43);

//_rotatingMatrix = rotatingMatrix;
//Quaternion.RotationMatrix(ref _rotatingMatrix, out otherQuat);

//direction_feet_forward = _getDirection(Vector3.ForwardRH, otherQuat);
//direction_feet_right = _getDirection(Vector3.Right, otherQuat);
//direction_feet_up = _getDirection(Vector3.Up, otherQuat);
var testerer = _offset_grabbed_pos * _offset_grabbed_pos_dist;
var diffNormPosX = testerer.X;// (MOVINGPOINTER.X) - _rightTouchMatrix.M41;
var diffNormPosY = testerer.Y;// (MOVINGPOINTER.Y) - _rightTouchMatrix.M42;
var diffNormPosZ = testerer.Z;// (MOVINGPOINTER.Z) - _rightTouchMatrix.M43;

MOVINGPOINTER = MOVINGPOINTER + (current_rotation_of_torso_pivot_right * (diffNormPosX));
MOVINGPOINTER = MOVINGPOINTER + (current_rotation_of_torso_pivot_up * (diffNormPosY));
MOVINGPOINTER = MOVINGPOINTER + (current_rotation_of_torso_pivot_forward * (diffNormPosZ));

var handPoser = MOVINGPOINTER;// (_offset_grabbed_pos * _offset_grabbed_pos_dist);

JQuaternion quatterer1 = JQuaternion.CreateFromMatrix(rigidbody.Orientation);

Quaternion tester1;
tester1.X = quatterer1.X;
tester1.Y = quatterer1.Y;
tester1.Z = quatterer1.Z;
tester1.W = quatterer1.W;*/

/*Matrix.RotationQuaternion(ref tester1, out rotationMatrix);

//Matrix.Multiply(ref rotationMatrix, ref final_hand_pos_right, out rotationMatrix);


//Matrix translationMatrix0;
//Matrix.Translation(handPos.X, handPos.Y, handPos.Z, out translationMatrix0);

//Matrix.Multiply(ref rotationMatrix, ref translationMatrix0, out translationMatrix0);

//rotationMatrix.M41 = handPos.X;
//rotationMatrix.M42 = handPos.Y;
//rotationMatrix.M43 = handPos.Z;


//Quaternion quatar;
//Quaternion.RotationMatrix(ref rotationMatrix, out quatar);





Quaternion quat;
Quaternion.RotationMatrix(ref rotationMatrix, out quat);

JQuaternion quatterer;
quatterer.X = quat.X;
quatterer.Y = quat.Y;
quatterer.Z = quat.Z;
quatterer.W = quat.W;



rigidbody.Orientation = JMatrix.CreateFromQuaternion(quatterer);
rigidbody.Position = new JVector(handPoser.X, handPoser.Y, handPoser.Z);

worldMatrix_instances[x + width * (y + height * z)][count] = rotationMatrix;
//quatterer = new JQuaternion(quatar.X, quatar.Y, quatar.Z, quatar.W);*/







/*rigidbody.Position = new JVector(handPos.X, handPos.Y, handPos.Z);
JQuaternion quatterer = JQuaternion.CreateFromMatrix(rigidbody.Orientation);

Quaternion tester;
tester.X = quatterer.X;
tester.Y = quatterer.Y;
tester.Z = quatterer.Z;
tester.W = quatterer.W;

//Quaternion tester = new Quaternion(quatterer.X, quatterer.Y, quatterer.Z, quatterer.W);

Matrix.RotationQuaternion(ref tester, out rotationMatrix);

Matrix someMatter;// = rotationMatrix * final_hand_pos_right;

Matrix translationMatrix0;
Matrix.Translation(rigidbody.Position.X, rigidbody.Position.Y, rigidbody.Position.Z, out translationMatrix0);



someMatter = rotationMatrix * final_hand_pos_right;



//Matrix.Multiply(ref rotationMatrix, ref final_hand_pos_right, out someMatter);

//Matrix.Multiply(ref someMatter, ref translationMatrix0, out translationMatrix0);

//Quaternion quatar;
//Quaternion.RotationMatrix(ref someMatter, out quatar);
//quatterer = new JQuaternion(quatar.X, quatar.Y, quatar.Z, quatar.W);
//rigidbody.Orientation = JMatrix.CreateFromQuaternion(quatterer);



someMatter.M41 = handPos.X;
someMatter.M42 = handPos.Y;
someMatter.M43 = handPos.Z;

worldMatrix_instances[x + width * (y + height * z)][count] = someMatter;*/
