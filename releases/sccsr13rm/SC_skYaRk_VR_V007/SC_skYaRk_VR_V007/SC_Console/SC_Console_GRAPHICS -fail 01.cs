using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;
using System.Threading;


using Ab3d.DXEngine;
using Ab3d.OculusWrap;
using Ab3d.DXEngine.OculusWrap;
using Ab3d.OculusWrap.DemoDX11;

using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using SharpDX.DirectInput;

using SC_skYaRk_VR_V007.SC_Graphics.SC_ShaderManager;
//using SC_skYaRk_VR_V007.SC_Graphics.SC_Grid;


using Vector3D = SC_skYaRk_VR_V007.SC_Utilities.Vector3D;


using Jitter;
using Jitter.Dynamics;
using Jitter.Collision;
using Jitter.LinearMath;
using Jitter.Collision.Shapes;


using System.Collections.Generic;
using System.Collections;


using System.ComponentModel;
using SC_skYaRk_VR_V007.SC_Graphics;


using System.Threading.Tasks;



namespace SC_skYaRk_VR_V007
{
    public class SC_Console_GRAPHICS
    {



        //********************JITTER TRYOUT
        public static World World { get; set; }
        public static List<object> _world_objects { get; set; }
        CollisionSystem collision;

        //public static DModelClass4_cube _cube_before_instance;
        //public static List<SC_Cube_Shader> _arrayOfCubesBeforeInstance = new List<SC_Cube_Shader>();
        public static List<int> _arrayOfCubesBeforeInstanceIndex = new List<int>();




        public static SharpDX.Matrix rotatingMatrix = SharpDX.Matrix.Identity;
        public static SharpDX.Vector3 originPos = new SharpDX.Vector3(0, 2, 10);
        public static SharpDX.Vector3 movePos = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Matrix originRot = SharpDX.Matrix.Identity;
        public static double RotationY { get; set; }
        public static double RotationX { get; set; }
        public static double RotationZ { get; set; }
        public static Matrix WorldMatrix;

        public static SharpDX.Matrix rotatingMatrixScreen = SharpDX.Matrix.Identity;
        public static double RotationScreenY { get; set; }
        public static double RotationScreenX { get; set; }
        public static double RotationScreenZ { get; set; }

        public static double LastRotationScreenY { get; set; }
        public static double LastRotationScreenX { get; set; }
        public static double LastRotationScreenZ { get; set; }

        public static float oriRotationScreenY { get; set; }
        public static float oriRotationScreenX { get; set; }
        public static float oriRotationScreenZ { get; set; }

        public static float RotationTouchRightX { get; set; }
        public static float RotationTouchRightY { get; set; }
        public static float RotationTouchRightZ { get; set; }

        public static float RotationTouchLeftX { get; set; }
        public static float RotationTouchLeftY { get; set; }
        public static float RotationTouchLeftZ { get; set; }

        public static SharpDX.Vector3 originPosScreen = new SharpDX.Vector3(0, 2, -0.25f);
        public static SharpDX.Matrix originRotScreen = SharpDX.Matrix.Identity;

        public static DModel _screenModel;
        public static DShaderManager _shaderManager;
        public static SC_SharpDX_ScreenCapture _desktopDupe;
        public static SC_SharpDX_ScreenFrame _desktopFrame;

        //public static DTerrain _grid_X;
        //public static DTerrain _grid_Y;
        //public static DTerrain _grid_Z;
        //public static DTerrain_World_Axis_X _grid_X;
        //public static DTerrain_World_Axis_X _grid_Y;
        //public static DTerrain_World_Axis_X _grid_Z;





        /*public static DTerrain_Screen_Metric _grid_X;
        public static DTerrain_Screen_Metric _grid_Y;
        public static DTerrain_Screen_Metric _grid_Z;



        public static DTerrain_Screen _screen_grid_X;
        public static DTerrain_Screen _screen_grid_Y;
        public static DTerrain_Screen _screen_grid_Z;

        public static DTerrain_Screen_Metric _screen_metric_grid_X;
        public static DTerrain_Screen_Metric _screen_metric_grid_Y;
        public static DTerrain_Screen_Metric _screen_metric_grid_Z;

        public static DTerrain_Screen_Metric _WORLD_GRID_X;

        public static DTexture _basicTexture;*/
        private SC_skYaRk_VR_V007.DCamera Camera { get; set; }

        //int hasClickedBUTTONX = 0;
        //int hasClickedBUTTONY = 0;

        //public static DModeler _rightTouch;
        public static SharpDX.Matrix _rightTouchMatrix = SharpDX.Matrix.Identity;

        //public static DModeler _leftTouch;
        public static SharpDX.Matrix _leftTouchMatrix = SharpDX.Matrix.Identity;

        //public static DModeler _mouseCursor;
        public static SharpDX.Matrix _mouseCursorMatrix = SharpDX.Matrix.Identity;

        //public static DModelClass4_cube[] _screenCorners;
        public static SharpDX.Matrix[] _screenDirMatrix;

        public static SharpDX.Matrix[] _screenDirMatrix_correct_pos;

        //public static DModelClass4_grid_Tiles[] _FloorTiles;

        //public static DModeler _intersectTouchRight;
        public static SharpDX.Matrix _intersectTouchRightMatrix = SharpDX.Matrix.Identity;

        //public static DModeler _intersectTouchLeft;
        public static SharpDX.Matrix _intersectTouchLeftMatrix = SharpDX.Matrix.Identity;

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
        static bool _shaderQuality = true;
        public bool _stopWatchSwitch = true;
        static bool _startOnce = true;
        static bool _startOnce0 = true;
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


        Vector3[] point3DCollection = new Vector3[4];

        // Properties
        private SC_Console_DIRECTX D3D { get; set; }
        //private SC_skYaRk_VR_V007.DCamera Camera { get; set; }
        // Constructor
        public SC_Console_GRAPHICS() { }

        public static IntPtr Hwnd;

        SC_Console_WRITER _currentWriter;
        static DirectInput directInput;
        float sizeWidtherer;
        float sizeheighterer;


        float distancerDiagonal;



        //public static SC_skYaRk_VR_V007.SC_Graphics.SC_Cube_Shader _terrainSolid;// { get; set; }


        World _World;
        float _World_Step;
        DateTime startTime;







        DateTime time1;
        DateTime time2;
        float deltaTime;

        public async Task DoWork(int timeOut)
        {
            time1 = DateTime.Now;
            time2 = DateTime.Now;
        _threadLoop:

            //float startTime = (float)(timeStopWatch.ElapsedMilliseconds / 1000d);
            //float DeltaTimer = (float)Math.Abs((timeStopWatch.ElapsedMilliseconds / 1000d) - startTime);
            ///deltaTime += DeltaTimer;

            time2 = DateTime.Now;
            deltaTime = (time2.Ticks - time1.Ticks) / 1000000000f; //100000000f

            time1 = time2;

            await Task.Delay(timeOut);
            Thread.Sleep(1);
            goto _threadLoop;
        }

        Task tsk;


        // Methods
        public bool Initialize(SC_skYaRk_VR_V007.DSystemConfiguration.DSystemConfiguration configuration, IntPtr windowsHandle, SC_Console_WRITER _writer)
        {
            try
            {
                MessageBox((IntPtr)0, "test0", "Oculus error", 0);
                Hwnd = windowsHandle;
                _currentWriter = _writer;
                // Create the Direct3D object.
                D3D = new SC_Console_DIRECTX();

                // Initialize the Direct3D object.
                if (!D3D.Initialize(configuration, windowsHandle, _writer))
                    return false;

                ReadKeyboard();

                // Create the camera object
                Camera = new DCamera();

                collision = new CollisionSystemPersistentSAP();
                World = new World(collision);
                World.AllowDeactivation = false;
                World.Gravity = new JVector(0, -1.81f, 0);
                World.SetIterations(1, 1);
                World.ContactSettings.AllowedPenetration = 0.0001f;
                _World = World;

                //startTime = DateTime.Now;
                tsk = DoWork(1);
                WorldMatrix = Matrix.Identity;




                RotationX = 0;
                RotationY = 0;
                RotationZ = 0;

                float pitch = (float)(RotationX * 0.0174532925f);
                float yaw = (float)(RotationY * 0.0174532925f);
                float roll = (float)(RotationZ * 0.0174532925f);

                originRot = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                _shaderManager = new DShaderManager();
                _shaderManager.Initialize(D3D.device, windowsHandle);

                _desktopDupe = new SC_SharpDX_ScreenCapture(0, 0);


                float a = 1;
                float r = 0.15f;
                float g = 0.15f;
                float b = 0.15f;

                MessageBox((IntPtr)0, "test1", "Oculus error", 0);


                //float _size_screen = 0.0006f;
                //_screenModel = new DModel();
                //bool _hasinit0 = _screenModel.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, _size_screen, 1, 1, a, r, g, b, 0.1f, 0.1f, 0.1f); //, "terrainGrassDirt.bmp" //0.00035f



















                /*for (int i = 0; i < 10; i++)
                {
                    if (_World == null)
                    {
                        Console.WriteLine("tester00");
                    }
                }*/


                /*var backgroundWorker1 = new BackgroundWorker();
                backgroundWorker1.DoWork += (object sender, DoWorkEventArgs args) =>
                {
                _threadLoop:        
                    /*var refreshDXEngineAction = new Action(delegate
                    {

                    });

                    System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);


                    _World_Step = DateTime.Now.Second;

                    if (_World_Step > 1.0f / 100)
                    {
                        _World_Step = 1.0f / 100;
                    }

                    World.Step(_World_Step, true);

                    Thread.Sleep(1);
                    goto _threadLoop;
                };

                backgroundWorker1.RunWorkerAsync();*/
















                // Set the initial position of the camera.
                //Camera.SetPosition(0, 0, -10);

                /*RotationX = 0;
                RotationY = 0;
                RotationZ = 0;

                float pitch = (float)(RotationX * 0.0174532925f);
                float yaw = (float)(RotationY * 0.0174532925f);
                float roll = (float)(RotationZ * 0.0174532925f);

                originRot = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                _shaderManager = new DShaderManager();
                _shaderManager.Initialize(D3D.device, windowsHandle);


                int _dvX = 10;
                int _dvY = 10;

                float _size_one = 0.5f;
                float _size_two = 0.01f;
                float _size_screen = 0.0006f;


                int sizeWidther = (int)(configuration.Width * _size_one);
                int sizeHeighter = (int)(configuration.Height * _size_one);*/


                //int _divisionWidth = (int)Math.Round(sizeWidther * _size_two);
                //int _divisionHeight = (int)Math.Round(sizeHeighter * _size_two);

                //float _divisionWidthF = (float)Math.Round(configuration.Width * _size_screen);
                //float _divisionHeightF = (float)Math.Round(configuration.Height * _size_screen);
                //float _divisionDepthF = (float)Math.Round(configuration.Height * _size_screen);

                //Vector3 _tester = new Vector3(_divisionWidthF, _divisionHeightF, _divisionDepthF);




                //_screen_grid_X = new DTerrain_Screen();
                //_screen_grid_X.Initialize(D3D.device, _divisionWidth, _divisionHeight, _size_screen);


                //_screen_grid_Y = new DTerrain_Screen();
                //_screen_grid_Y.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, _size_screen, _dvX, _dvY, a, r, g, b);

                //_screen_grid_Z = new DTerrain_Screen();
                //_screen_grid_Z.Initialize(D3D.device, _divisionWidth, _divisionHeight, _size_screen);


                a = 1;
                r = 0.15f;
                g = 0.15f;
                b = 0.15f;

                //_grid_X = new DTerrain_Screen_Metric();
                //_grid_X.Initialize(D3D, 10, 10, 1, 10, 10, a, r, g, b);

                //_grid_Y = new DTerrain_World_Axis_Y();
                //_grid_Y.Initialize(D3D.device, 10, 10, 1,10,10, a, r, g, b);

                //_grid_Z = new DTerrain_World_Axis_Z();
                //_grid_Z.Initialize(D3D.device, 10, 10, 1, 10, 10, a, r, g, b);





                a = 1;
                r = 0.65f;
                g = 0.15f;
                b = 0.15f;
                //_size_screen = 0.0006f;




                //_screen_metric_grid_Y = new DTerrain_Screen_Metric();
                //_screen_metric_grid_Y.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, _size_screen, _dvX, _dvY, a, r, g, b);





                a = 1;
                r = 0.65f;
                g = 0.15f;
                b = 0.15f;











                //_WORLD_GRID_X = new DTerrain_Screen_Metric();
                //_WORLD_GRID_X.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, _size_screen, _dvX, _dvY, a, r, g, b);


                //_WORLD_GRID_X = new DTerrain_Screen_Metric();
                //_WORLD_GRID_X.Initialize(D3D, 100, 100, 0.1f, 10, 10, a, r, g, b);













                //_screen_grid_Y = new DTerrain_Screen();
                //_screen_grid_Y.Initialize(D3D.device, _divisionWidth, _divisionHeight );







                //screen dims => 1920 * 1080
                //test => 1/1920 = 5.2083333333333333333333333333333e-4
                //test => 
                //test =>
                //test =>
                //test =>
                //test =>
                //test =>
                //test =>











                /*oriRotationScreenX = 0;
                oriRotationScreenY = 0;
                oriRotationScreenZ = 0;

                RotationScreenX = oriRotationScreenX;
                RotationScreenY = oriRotationScreenY;
                RotationScreenZ = oriRotationScreenZ;

                float pitcher = oriRotationScreenX * 0.0174532925f;
                float yawer = oriRotationScreenY * 0.0174532925f;
                float roller = oriRotationScreenZ * 0.0174532925f;

                originRotScreen = SharpDX.Matrix.RotationYawPitchRoll(yawer, pitcher, roller);
                rotatingMatrixScreen = SharpDX.Matrix.RotationYawPitchRoll(yawer, pitcher, roller);


                _size_screen = 0.0006f;

                sizeWidtherer = (float)(((float)D3D.SurfaceWidth * 0.5f) * _size_screen);
                sizeheighterer = (float)((float)(D3D.SurfaceHeight * 0.5f) * _size_screen);









                a = 1;
                r = 0.65f;
                g = 0.15f;
                b = 0.15f;*/


               
                //_screenDirMatrix = new Matrix[4];
                //_screenDirMatrix_correct_pos = new Matrix[4];

                //MessageBox((IntPtr)0, _hasinit0 + "", "Oculus error", 0);




                /*_basicTexture = new DTexture();
                bool _hasinit1 = _basicTexture.Initialize(D3D.device, "../../../terrainGrassDirt.bmp");


                if (_basicTexture.TextureResource == null)
                {
                    MessageBox((IntPtr)0, "NULL", "Oculus error", 0);
                }*/


                /*if (!LoadTexture(device, textureFileName))
                    return false;

                private bool LoadTexture(SharpDX.Direct3D11.Device device, string textureFileName)
                {
                    // Create the texture object.
                    Texture = new DTexture();

                    // Initialize the texture object.
                    bool result = Texture.Initialize(device, textureFileName);

                    return result;
                }*/


                //MessageBox((IntPtr)0, _hasinit1 + "", "Oculus error", 0);

                /*_rightTouch = new DModeler();
                _rightTouch.Initialize(D3D.device, 0.05f, 0.05f, 0.05f);

                RotationTouchRightX = oriRotationScreenX;
                RotationTouchRightY = oriRotationScreenY;
                RotationTouchRightZ = oriRotationScreenZ;

                _leftTouch = new DModeler();
                _leftTouch.Initialize(D3D.device, 0.05f, 0.05f, 0.05f);

                RotationTouchLeftX = oriRotationScreenX;
                RotationTouchLeftY = oriRotationScreenY;
                RotationTouchLeftZ = oriRotationScreenZ;

                _screenCorners = new DModelClass4_cube[4];

                rotatingMatrixScreen.M41 = originPosScreen.X;
                rotatingMatrixScreen.M42 = originPosScreen.Y;
                rotatingMatrixScreen.M43 = originPosScreen.Z;


                for (int i = 0; i < _screenModel.vertices.Length; i++)
                {
                    _screenDirMatrix[i] = new Matrix();

                    _screenDirMatrix[i] = rotatingMatrixScreen;
                    _screenDirMatrix[i].M41 = _screenModel.vertices[i].position.X + originPosScreen.X;
                    _screenDirMatrix[i].M42 = _screenModel.vertices[i].position.Y + originPosScreen.Y;
                    _screenDirMatrix[i].M43 = _screenModel.vertices[i].position.Z + originPosScreen.Z;
                    point3DCollection[i] = _screenModel.vertices[i].position + originPosScreen;

                }*/




                /*for (int i = 0; i < _screenModel.vertices.Length; i++)
                {
                    //_screenDirMatrix[i] = new Matrix();

                    //_screenDirMatrix[i] = rotatingMatrixScreen;
                    //_screenDirMatrix[i].M41 = _screenModel.vertices[i].position.X;
                    //_screenDirMatrix[i].M42 = _screenModel.vertices[i].position.Y;
                    //_screenDirMatrix[i].M43 = _screenModel.vertices[i].position.Z;
                    //point3DCollection[i] = _screenModel.vertices[i].position;

                    //_screenDirMatrix[i] = rotatingMatrixScreen;
                    //_screenDirMatrix[i].M41 = _screenModel.vertices[i].position.X + originPosScreen.X;
                    //_screenDirMatrix[i].M42 = _screenModel.vertices[i].position.Y + originPosScreen.Y;
                    //_screenDirMatrix[i].M43 = _screenModel.vertices[i].position.Z + originPosScreen.Z;

                    //point3DCollection[i] = _screenModel.vertices[i].position + originPosScreen;
                    


                    _screenDirMatrix_correct_pos[i] = rotatingMatrixScreen;
                    _screenDirMatrix_correct_pos[i].M41 = _screenModel.vertices[i].position.X + originPosScreen.X;
                    _screenDirMatrix_correct_pos[i].M42 = _screenModel.vertices[i].position.Y + originPosScreen.Y;
                    _screenDirMatrix_correct_pos[i].M43 = _screenModel.vertices[i].position.Z + originPosScreen.Z;

                    ///point3DCollection[i] = _screenModel.vertices[i].position + originPosScreen;
                }*/

                vert0 = new Vector3(_screenDirMatrix[0].M41, _screenDirMatrix[0].M42, _screenDirMatrix[0].M43);
                vert1 = new Vector3(_screenDirMatrix[1].M41, _screenDirMatrix[1].M42, _screenDirMatrix[1].M43);
                vert2 = new Vector3(_screenDirMatrix[2].M41, _screenDirMatrix[2].M42, _screenDirMatrix[2].M43);
                vert3 = new Vector3(_screenDirMatrix[3].M41, _screenDirMatrix[3].M42, _screenDirMatrix[3].M43);

                Vector3.Distance(ref vert0, ref originPosScreen, out distancerDiagonal);











                /*for (int i = 0; i < _screenCorners.Length; i++)
                {
                    _screenCorners[i] = new DModelClass4_cube();
                    // _screenCorners[i].Initialize(D3D.device, 0.01f, 0.01f, 0.01f);
                    _screenCorners[i].Initialize(SC_Console_DIRECTX._device, 0.1f, 0.1f, 0.1f, 1, 1, 1, _screenCorners[i]._POSITION, 1, 1, 1);
                }





                _FloorTiles = new DModelClass4_grid_Tiles[4];

                for (int i = 0; i < _FloorTiles.Length; i++)
                {
                    _FloorTiles[i] = new DModelClass4_grid_Tiles();
                    _FloorTiles[i].Initialize(D3D.device, 0.1f, 0.1f, 0.1f, 2, 1, 10);
                }*/














                //Console.WriteLine(_screenCorners.Length);




                /*_intersectTouchRight = new DModeler();
                _intersectTouchRight.Initialize(D3D.device, 0.005f, 0.005f, 0.005f);

                _intersectTouchLeft = new DModeler();
                _intersectTouchLeft.Initialize(D3D.device, 0.005f, 0.005f, 0.005f);
                */




                _updateFunctionStopwatchRight = new Stopwatch();
                _updateFunctionStopwatchLeft = new Stopwatch();

                _updateFunctionStopwatchTouchRightButtonA = new Stopwatch();

                _updateFunctionStopwatchLeftHandTrigger = new Stopwatch();
                _updateFunctionStopwatchRightHandTrigger = new Stopwatch();

                _updateFunctionStopwatchLeftThumbstickGoRight = new Stopwatch();
                _updateFunctionStopwatchLeftThumbstickGoLeft = new Stopwatch();

                _updateFunctionStopwatchRightIndexTrigger = new Stopwatch();
                _updateFunctionStopwatchLeftIndexTrigger = new Stopwatch();

                _updateFunctionStopwatchRightThumbstickGoLeft = new Stopwatch();
                _updateFunctionStopwatchRightThumbstickGoRight = new Stopwatch();


                _updateFunctionStopwatchRightThumbstick = new Stopwatch();
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







                /*_mouseCursor = new DModeler();
                _mouseCursor.Initialize(D3D.device, 0.05f, 0.05f, 0.05f);
                */
























                int secondLayerX = 1;
                int secondLayerY = 1;
                int secondLayerZ = 1;

                SharpDX.Matrix _tempMatrix = WorldMatrix;


                _tempMatrix.M41 = 0;
                _tempMatrix.M42 = 0;
                _tempMatrix.M43 = 0;

                /*//TERRAIN
                _terrainSolid = new SC_Cube_Shader();
                _terrainSolid.Initialize(D3D.device, 20, 1, 20, new Vector4(0.15f,0.15f,0.15f,1), _tempMatrix); // 1, 1, 1,, 1, 1, 1

                _terrainSolid.transform.Component.rigidbody = new RigidBody(new BoxShape(0.1f, 0.1f, 0.1f));
                _terrainSolid.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                _terrainSolid.transform.Component.rigidbody.Orientation = Conversion.ToJitterMatrix(_tempMatrix);
                _terrainSolid.transform.Component.rigidbody.LinearVelocity = new Jitter.LinearMath.JVector(0, 0, 0);
                _terrainSolid.transform.Component.rigidbody.IsStatic = true;

                _terrainSolid.transform.Component.rigidbody.Tag = SC_Console_DIRECTX.BodyTag.Terrain;
                _terrainSolid.transform.Component.rigidbody.Material.Restitution = 0;
                _terrainSolid.transform.Component.rigidbody.Material.StaticFriction = 10;
                _terrainSolid.transform.Component.rigidbody.Material.KineticFriction = 10;
                _terrainSolid.transform.Component.rigidbody.Mass = 100;

                //_terrainSolid._POSITION = _tempMatrix;
                
                //_arrayOfCubes.Add(_cube_before_instance);
                World.AddBody(_terrainSolid.transform.Component.rigidbody);
                //_arrayOfCubesBeforeInstance.Add(_terrainSolid);
                _arrayOfCubesBeforeInstanceIndex.Add(World.RigidBodies.Count + 1);*/









                _WorldMatrix = WorldMatrix;


                /*_WorldMatrix = WorldMatrix;

                _terrainSolid = new SC_skYaRk_VR_V007.SC_Graphics.SC_Cube_Shader();


                _WorldMatrix.M42 -= 0.5f;


                //_terrainSolid.Initialize(D3D.device, 22.5f, 0.5f, 22.5f, new Vector4(0.25f, 0.25f, 0.25f, 1),_WorldMatrix);
                //_terrainSolid.transform.Component.rigidbody = new RigidBody(new BoxShape(45, 1, 45));

                _terrainSolid.Initialize(D3D.device, 10, 0.5f, 10, new Vector4(0.75f, 0.25f, 0.25f, 1), _WorldMatrix);
                _terrainSolid.transform.Component.rigidbody = new RigidBody(new BoxShape(20, 1, 20));

                _terrainSolid.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(_WorldMatrix.M41, _WorldMatrix.M42, _WorldMatrix.M43);
                _terrainSolid.transform.Component.rigidbody.Orientation = Conversion.ToJitterMatrix(_WorldMatrix);
                _terrainSolid.transform.Component.rigidbody.LinearVelocity = new Jitter.LinearMath.JVector(0, 0, 0);
                _terrainSolid.transform.Component.rigidbody.IsStatic = true;
                _terrainSolid.transform.Component.rigidbody.Tag = SC_Console_DIRECTX.BodyTag.Terrain;
                _terrainSolid.transform.Component.rigidbody.Material.Restitution = 0f;
                _terrainSolid.transform.Component.rigidbody.Material.StaticFriction = 50;
                _terrainSolid.transform.Component.rigidbody.Material.KineticFriction = 50;
                //_arrayOfCubes.Add(_terrain);
                _terrainSolid.Render(D3D.device.ImmediateContext);
                World.AddBody(_terrainSolid.transform.Component.rigidbody);
                */















                int width = 3;
                int heigth = 3;
                int depth = 3;

                int counter = 0;
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < heigth; y++)
                    {
                        for (int z = 0; z < depth; z++)
                        {


                            /*_WorldMatrix.M41 = x;
                            _WorldMatrix.M42 = y + 10;
                            _WorldMatrix.M43 = z;

                            SC_skYaRk_VR_V007.SC_Graphics.SC_Cube_Shader _cubeSolid = new SC_skYaRk_VR_V007.SC_Graphics.SC_Cube_Shader();
                            _cubeSolid.Initialize(D3D.device, 0.1f, 0.1f, 0.1f, new Vector4(0.85f, 0.25f, 0.25f, 1), _WorldMatrix);
                            _cubeSolid.transform.Component.rigidbody = new RigidBody(new BoxShape(0.2f, 0.2f, 0.2f));

                            _cubeSolid.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(_WorldMatrix.M41, _WorldMatrix.M42, _WorldMatrix.M43);
                            _cubeSolid.transform.Component.rigidbody.Orientation = Conversion.ToJitterMatrix(_WorldMatrix);
                            _cubeSolid.transform.Component.rigidbody.LinearVelocity = new Jitter.LinearMath.JVector(0, 0, 0);
                            _cubeSolid.transform.Component.rigidbody.IsStatic = false;
                            _cubeSolid.transform.Component.rigidbody.Tag = SC_Console_DIRECTX.BodyTag.physicsInstancedCube;
                            _cubeSolid.transform.Component.rigidbody.Material.Restitution = 0f;
                            _cubeSolid.transform.Component.rigidbody.Material.StaticFriction = 50;
                            _cubeSolid.transform.Component.rigidbody.Material.KineticFriction = 50;
                            //_arrayOfCubes.Add(_cube);

                            //_cubeSolid.Render(D3D.device.ImmediateContext);

                            World.AddBody(_cubeSolid.transform.Component.rigidbody);
                            _arrayOfCubesBeforeInstance.Add(_cubeSolid);
                            _arrayOfCubesBeforeInstanceIndex.Add(World.RigidBodies.Count + 1);
                            */























                            /*_WorldMatrix = WorldMatrix;

                            //Vector3 newVector = new Vector3(x, y, z);

                            _cube_before_instance = new DModelClass4_cube();
                            //_cube_before_instance.Initialize(Device, 0.05f, 0.05f, 0.05f, new Vector4(0.1f, 0.1f, 1f, 1)); //_cube_before_instance
                            _cube_before_instance.Initialize(D3D.device, 0.1f, 0.1f, 0.1f, 1, 1, 1, _WorldMatrix, 1, 1, 1);

                            _WorldMatrix.M41 = x;
                            _WorldMatrix.M42 = y + 10;
                            _WorldMatrix.M43 = z;

                            _cube_before_instance.transform.Component.rigidbody = new RigidBody(new BoxShape(0.25f, 0.25f, 0.25f));
                            _cube_before_instance.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(_WorldMatrix.M41, _WorldMatrix.M42, _WorldMatrix.M43);
                            _cube_before_instance.transform.Component.rigidbody.Orientation = Conversion.ToJitterMatrix(_WorldMatrix);
                            _cube_before_instance.transform.Component.rigidbody.LinearVelocity = new Jitter.LinearMath.JVector(0, 0, 0);
                            _cube_before_instance.transform.Component.rigidbody.IsStatic = false;

                            _cube_before_instance.transform.Component.rigidbody.Tag = SC_Console_DIRECTX.BodyTag.physicsInstancedCube;
                            _cube_before_instance.transform.Component.rigidbody.Material.Restitution = 0;
                            _cube_before_instance.transform.Component.rigidbody.Material.StaticFriction = 10;
                            _cube_before_instance.transform.Component.rigidbody.Material.KineticFriction = 10;
                            _cube_before_instance.transform.Component.rigidbody.Mass = 100;


                            _cube_before_instance._POSITION = _WorldMatrix;

                            //var currentCountRigid = World.RigidBodies.Count;


                    
                            //World.RigidBodies.Count
                            World.AddBody(_cube_before_instance.transform.Component.rigidbody);
                            _arrayOfCubesBeforeInstance.Add(_cube_before_instance);
                            _arrayOfCubesBeforeInstanceIndex.Add(World.RigidBodies.Count + 1);*/



                            //World.AddBody(_cube_before_instance.transform.Component.rigidbody);

                            /*SharpDX.Matrix translationMatrix;
                            SharpDX.Matrix _tempMatrix = WorldMatrix;
                            SharpDX.Matrix.Translation(x, y, z, out translationMatrix);
                            SharpDX.Matrix.Multiply(ref _tempMatrix, ref translationMatrix, out _tempMatrix);
                            _tempMatrix.M42 += 10;

                            _cube_before_instance.transform.Component.rigidbody = new RigidBody(new BoxShape(0.1f, 0.1f, 0.1f));
                            _cube_before_instance.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                            _cube_before_instance.transform.Component.rigidbody.Orientation = Conversion.ToJitterMatrix(_tempMatrix);
                            _cube_before_instance.transform.Component.rigidbody.LinearVelocity = new Jitter.LinearMath.JVector(0, 0, 0);
                            _cube_before_instance.transform.Component.rigidbody.IsStatic = false;

                            _cube_before_instance.transform.Component.rigidbody.Tag = BodyTag.physicsInstancedCube;
                            _cube_before_instance.transform.Component.rigidbody.Material.Restitution = 0;
                            _cube_before_instance.transform.Component.rigidbody.Material.StaticFriction = 10;
                            _cube_before_instance.transform.Component.rigidbody.Material.KineticFriction = 10;
                            _cube_before_instance.transform.Component.rigidbody.Mass = 100;

                            _arrayOfCubes.Add(_cube_before_instance);
                            World.AddBody(_cube_before_instance.transform.Component.rigidbody);
                            //_world_objects.Add(testObject);*/

                        }
                    }
                }




                for (int i = 0; i < 10; i++)
                {
                    if (World != null)
                    {
                        //Console.WriteLine("tester00");
                        Console.WriteLine(World.RigidBodies.Count);
                    }
                }










                _hasINITVR = 1;

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

        int _hasINITVR = 0;


        public SC_object_messenger_dispatcher.SC_message_object[] FrameVR(SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject0)
        {
            // Render the graphics scene.

            /*bool results = false;

            try
            {
                results = 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return results;*/

            _someReceivedObject0 = RenderVR(_someReceivedObject0);
            return _someReceivedObject0;
        }

        public SC_object_messenger_dispatcher.SC_message_object[] FrameVRTWO(SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject0)
        {
            // Render the graphics scene.

            /*bool results = false;

            try
            {
                results = 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return results;*/

            _someReceivedObject0 = RenderVRTWO(_someReceivedObject0);
            return _someReceivedObject0;
        }


        public bool FrameDX(SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject0)
        {
            // Render the graphics scene.

            bool results = false;

            try
            {
                results = RenderDX(_someReceivedObject0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return results;

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
        Posef[] eyePoses;
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
        Vector3 vert3;


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


        /*public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
        {
            return rotation * (point - pivot) + pivot;
        }*/











        private bool RenderDX(SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject0)
        {
            if (_lockedFrameCounter >= 50)
            {
                if (Program._KeyboardState.PressedKeys.Contains(Key.L))
                {
                    _hasLockedMouse = 1;
                }
                else
                {
                    _hasLockedMouse = 0;
                }
                _lockedFrameCounter = 0;
            }

            _lockedFrameCounter++;





            // Clear the buffer to begin the scene.
            D3D.BeginScene(0.1f, 0.25f, 0.5f, 1f, _someReceivedObject0);
            Camera.Render();

            if (_updateFunctionBoolRight)
            {
                _updateFunctionStopwatchRight.Stop();
                _updateFunctionStopwatchRight.Reset();
                _updateFunctionStopwatchRight.Start();
                _updateFunctionBoolRight = false;
            }

            if (_updateFunctionBoolLeft)
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


            if (_updateFunctionBoolRightThumbStick)
            {
                _updateFunctionStopwatchRightThumbstick.Stop();
                _updateFunctionStopwatchRightThumbstick.Reset();
                _updateFunctionStopwatchRightThumbstick.Start();
                _updateFunctionBoolRightThumbStick = false;
            }

            if (_updateFunctionBoolLeftThumbStick)
            {
                _updateFunctionStopwatchLeftThumbstick.Stop();
                _updateFunctionStopwatchLeftThumbstick.Reset();
                _updateFunctionStopwatchLeftThumbstick.Start();
                _updateFunctionBoolLeftThumbStick = false;
            }

            //HEADSET POSITION
            displayMidpoint = D3D.OVR.GetPredictedDisplayTime(D3D.sessionPtr, 0);
            trackingState = D3D.OVR.GetTrackingState(D3D.sessionPtr, displayMidpoint, true);
            latencyMark = false;
            trackState = D3D.OVR.GetTrackingState(D3D._oculusRiftVirtualRealityProvider.SessionPtr, 0.0f, latencyMark);
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
            _rightTouchQuat = new SharpDX.Quaternion(handPoseRight.Orientation.X, handPoseRight.Orientation.Y, handPoseRight.Orientation.Z, handPoseRight.Orientation.W);
            SharpDX.Matrix.RotationQuaternion(ref _rightTouchQuat, out _rightTouchMatrix);
            _rightTouchMatrix.M41 = handPoseRight.Position.X + originPos.X;
            _rightTouchMatrix.M42 = handPoseRight.Position.Y + originPos.Y;
            _rightTouchMatrix.M43 = handPoseRight.Position.Z + originPos.Z;

            //TOUCH CONTROLLER LEFT
            resultsLeft = D3D.OVR.GetInputState(D3D.sessionPtr, D3D.controllerTypeLTouch, ref D3D.inputStateLTouch);
            buttonPressedOculusTouchLeft = D3D.inputStateLTouch.Buttons;
            thumbStickLeft = D3D.inputStateLTouch.Thumbstick;
            handTriggerLeft = D3D.inputStateLTouch.HandTrigger;
            indexTriggerLeft = D3D.inputStateLTouch.IndexTrigger;
            handPoseLeft = trackingState.HandPoses[0].ThePose;
            _leftTouchQuat = new SharpDX.Quaternion(handPoseLeft.Orientation.X, handPoseLeft.Orientation.Y, handPoseLeft.Orientation.Z, handPoseLeft.Orientation.W);
            SharpDX.Matrix.RotationQuaternion(ref _leftTouchQuat, out _leftTouchMatrix);
            _leftTouchMatrix.M41 = handPoseLeft.Position.X + originPos.X;// + _hmdPoser.X;
            _leftTouchMatrix.M42 = handPoseLeft.Position.Y + originPos.Y;// + _hmdPoser.Y;
            _leftTouchMatrix.M43 = handPoseLeft.Position.Z + originPos.Z;// + _hmdPoser.Z;


            D3D.EndScene();
            return true;
        }


        private SC_object_messenger_dispatcher.SC_message_object[] RenderVRTWO(SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject0)
        {
            try
            {
                if (World != null)
                {
                    _World_Step = DateTime.Now.Second;//startTime.Now.Second; //deltaTime;//

                    if (_World_Step > 1.0f / 100)
                    {
                        _World_Step = 1.0f / 100;
                    }

                    World.Step(_World_Step, true);
                }

                //HEADSET POSITION
                displayMidpoint = D3D.OVR.GetPredictedDisplayTime(D3D.sessionPtr, 0);
                trackingState = D3D.OVR.GetTrackingState(D3D.sessionPtr, displayMidpoint, true);
                latencyMark = false;
                trackState = D3D.OVR.GetTrackingState(D3D._oculusRiftVirtualRealityProvider.SessionPtr, 0.0f, latencyMark);
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
                _rightTouchQuat = new SharpDX.Quaternion(handPoseRight.Orientation.X, handPoseRight.Orientation.Y, handPoseRight.Orientation.Z, handPoseRight.Orientation.W);
                SharpDX.Matrix.RotationQuaternion(ref _rightTouchQuat, out _rightTouchMatrix);
                _rightTouchMatrix.M41 = handPoseRight.Position.X + originPos.X;
                _rightTouchMatrix.M42 = handPoseRight.Position.Y + originPos.Y;
                _rightTouchMatrix.M43 = handPoseRight.Position.Z + originPos.Z;

                //TOUCH CONTROLLER LEFT
                resultsLeft = D3D.OVR.GetInputState(D3D.sessionPtr, D3D.controllerTypeLTouch, ref D3D.inputStateLTouch);
                buttonPressedOculusTouchLeft = D3D.inputStateLTouch.Buttons;
                thumbStickLeft = D3D.inputStateLTouch.Thumbstick;
                handTriggerLeft = D3D.inputStateLTouch.HandTrigger;
                indexTriggerLeft = D3D.inputStateLTouch.IndexTrigger;
                handPoseLeft = trackingState.HandPoses[0].ThePose;
                _leftTouchQuat = new SharpDX.Quaternion(handPoseLeft.Orientation.X, handPoseLeft.Orientation.Y, handPoseLeft.Orientation.Z, handPoseLeft.Orientation.W);
                SharpDX.Matrix.RotationQuaternion(ref _leftTouchQuat, out _leftTouchMatrix);
                _leftTouchMatrix.M41 = handPoseLeft.Position.X + originPos.X;// + _hmdPoser.X;
                _leftTouchMatrix.M42 = handPoseLeft.Position.Y + originPos.Y;// + _hmdPoser.Y;
                _leftTouchMatrix.M43 = handPoseLeft.Position.Z + originPos.Z;// + _hmdPoser.Z;













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
                        D3D.layerEyeFov.RenderPoseLeft = eyePoses[0];
                    else
                        D3D.layerEyeFov.RenderPoseRight = eyePoses[1];

                    // Update the render description at each frame, as the HmdToEyeOffset can change at runtime.
                    eyeTexture.RenderDescription = D3D.OVR.GetRenderDesc(D3D.sessionPtr, eye, D3D.hmdDesc.DefaultEyeFov[eyeIndex]);

                    // Retrieve the index of the active texture

                    D3D.result = eyeTexture.SwapTextureSet.GetCurrentIndex(out textureIndex);
                    D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to retrieve texture swap chain current index.");

                    D3D.device.ImmediateContext.OutputMerger.SetRenderTargets(eyeTexture.DepthStencilView, eyeTexture.RenderTargetViews[textureIndex]);
                    D3D.device.ImmediateContext.ClearRenderTargetView(eyeTexture.RenderTargetViews[textureIndex], SharpDX.Color.DimGray);
                    D3D.device.ImmediateContext.ClearDepthStencilView(eyeTexture.DepthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);
                    D3D.device.ImmediateContext.Rasterizer.SetViewport(eyeTexture.Viewport);

                    eyePos = SharpDX.Vector3.Transform(new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z), originRot * rotatingMatrix).ToVector3(); //*rotatingMatrix //new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z

                    eyeQuaternionMatrix = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X, eyePoses[eyeIndex].Orientation.Y, eyePoses[eyeIndex].Orientation.Z, eyePoses[eyeIndex].Orientation.W));
                    finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix; //*rotatingMatrix

                    lookUp = Vector3.Transform(new Vector3(0, 1, 0), finalRotationMatrix).ToVector3(); //new Vector3(0, 1, 0)
                    lookAt = Vector3.Transform(new Vector3(0, 0, -1), finalRotationMatrix).ToVector3(); //new Vector3(0, 0, -1)

                    //var test = Vector3.TransformCoordinate(lookAt, rotatingMatrix);

                    viewPosition = eyePos + originPos; //eyePoses[eyeIndex].Position.ToVector3()
                    viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);

                    _projectionMatrix = D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.01f, 100.0f, ProjectionModifier.None).ToMatrix();
                    _projectionMatrix.Transpose();

                    _WorldMatrix = WorldMatrix;






                    matroxer2 = _leftTouchMatrix;// Matrix.Multiply(_leftTouchMatrix, rotatingMatrixScreen);


                    //matroxer2 = Matrix.Identity;
                    //matroxer2.M41 = originPosScreen.X;
                    //matroxer2.M42 = originPosScreen.Y;
                    //matroxer2.M43 = originPosScreen.Z;

                    Quaternion _testQuat;

                    Quaternion.RotationMatrix(ref matroxer2, out _testQuat);

                    var xq = _testQuat.X;
                    var yq = _testQuat.Y;
                    var zq = _testQuat.Z;
                    var wq = _testQuat.W;


                    var roll = Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
                    var pitch = Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
                    var yaw = Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);

                    //2nd
                    //var roll = Math.Atan2(2 * yq * wq + 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
                    //var pitch = Math.Atan2(2 * xq * wq + 2 * yq * zq, 1 - 2 * xq * xq - 2 * zq * zq);
                    //var yaw = Math.Asin(2 * xq * yq + 2 * zq * wq);

                    //1st
                    //roll = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
                    //pitch = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
                    //yaw = Mathf.Asin(2 * x * y + 2 * z * w);

                    RotationScreenX = pitch;// / 0.01745327925f;
                    RotationScreenY = yaw;/// 0.0174532925f;
                    RotationScreenZ = roll;/// 0.0174532925f;

                    //_screenModel.Render(D3D.device.ImmediateContext);
                    //_shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _screenModel.IndexCount, _screenModel.InstanceCount, matroxer2, viewMatrix, _projectionMatrix, _desktopFrame._ShaderResource);

                    D3D.result = eyeTexture.SwapTextureSet.Commit();
                    D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to commit the swap chain texture.");
                }

                //Camera.SetPosition(realPos.X, realPos.Y, realPos.Z);
                //var yo = OVR.GetTrackerPose(sessionPtr, (uint)TrackedDeviceType.HMD);
                //var test = yo.Pose;
                //realPos = new Vector3(test.Position.X, test.Position.Y, test.Position.Z);

                D3D.result = D3D.OVR.SubmitFrame(D3D.sessionPtr, 0L, IntPtr.Zero, ref D3D.layerEyeFov);
                D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to submit the frame of the current layers.");


                D3D.deviceContext.CopyResource(D3D.mirrorTextureD3D, D3D.BackBuffer);
                D3D.SwapChain.Present(0, PresentFlags.None);

                /*if (SC_Console_CORE._SC_GLOB._Activate_Desktop_Image == 1)
                {
                    D3D.deviceContext.CopyResource(D3D.mirrorTextureD3D, D3D.BackBuffer);
                    D3D.SwapChain.Present(0, PresentFlags.None);
                }*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return _someReceivedObject0;
        }


        private SC_object_messenger_dispatcher.SC_message_object[] RenderVR(SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject0)
        {
            if (_hasINITVR == 1)
            {
                if (World != null)
                {
                    _World_Step = DateTime.Now.Second;//startTime.Now.Second; //deltaTime;//

                    if (_World_Step > 1.0f / 100)
                    {
                        _World_Step = 1.0f / 100;
                    }

                    World.Step(_World_Step, true);
                }

                //Thread.Sleep(1);

                if (_lockedFrameCounter >= 50)
                {
                    if (Program._KeyboardState.PressedKeys.Contains(Key.L))
                    {
                        _hasLockedMouse = 1;
                    }
                    else
                    {
                        _hasLockedMouse = 0;
                    }
                    _lockedFrameCounter = 0;
                }

                _lockedFrameCounter++;


                // Clear the buffer to begin the scene.
                //D3D.BeginScene(0.1f, 0.25f, 0.5f, 1f, _someReceivedObject0);
                Camera.Render();

                if (_updateFunctionBoolRight)
                {
                    _updateFunctionStopwatchRight.Stop();
                    _updateFunctionStopwatchRight.Reset();
                    _updateFunctionStopwatchRight.Start();
                    _updateFunctionBoolRight = false;
                }

                if (_updateFunctionBoolLeft)
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


                if (_updateFunctionBoolRightThumbStick)
                {
                    _updateFunctionStopwatchRightThumbstick.Stop();
                    _updateFunctionStopwatchRightThumbstick.Reset();
                    _updateFunctionStopwatchRightThumbstick.Start();
                    _updateFunctionBoolRightThumbStick = false;
                }

                if (_updateFunctionBoolLeftThumbStick)
                {
                    _updateFunctionStopwatchLeftThumbstick.Stop();
                    _updateFunctionStopwatchLeftThumbstick.Reset();
                    _updateFunctionStopwatchLeftThumbstick.Start();
                    _updateFunctionBoolLeftThumbStick = false;
                }

                //HEADSET POSITION
                displayMidpoint = D3D.OVR.GetPredictedDisplayTime(D3D.sessionPtr, 0);
                trackingState = D3D.OVR.GetTrackingState(D3D.sessionPtr, displayMidpoint, true);
                latencyMark = false;
                trackState = D3D.OVR.GetTrackingState(D3D._oculusRiftVirtualRealityProvider.SessionPtr, 0.0f, latencyMark);
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
                _rightTouchQuat = new SharpDX.Quaternion(handPoseRight.Orientation.X, handPoseRight.Orientation.Y, handPoseRight.Orientation.Z, handPoseRight.Orientation.W);
                SharpDX.Matrix.RotationQuaternion(ref _rightTouchQuat, out _rightTouchMatrix);
                _rightTouchMatrix.M41 = handPoseRight.Position.X + originPos.X;
                _rightTouchMatrix.M42 = handPoseRight.Position.Y + originPos.Y;
                _rightTouchMatrix.M43 = handPoseRight.Position.Z + originPos.Z;

                //TOUCH CONTROLLER LEFT
                resultsLeft = D3D.OVR.GetInputState(D3D.sessionPtr, D3D.controllerTypeLTouch, ref D3D.inputStateLTouch);
                buttonPressedOculusTouchLeft = D3D.inputStateLTouch.Buttons;
                thumbStickLeft = D3D.inputStateLTouch.Thumbstick;
                handTriggerLeft = D3D.inputStateLTouch.HandTrigger;
                indexTriggerLeft = D3D.inputStateLTouch.IndexTrigger;
                handPoseLeft = trackingState.HandPoses[0].ThePose;
                _leftTouchQuat = new SharpDX.Quaternion(handPoseLeft.Orientation.X, handPoseLeft.Orientation.Y, handPoseLeft.Orientation.Z, handPoseLeft.Orientation.W);
                SharpDX.Matrix.RotationQuaternion(ref _leftTouchQuat, out _leftTouchMatrix);
                _leftTouchMatrix.M41 = handPoseLeft.Position.X + originPos.X;// + _hmdPoser.X;
                _leftTouchMatrix.M42 = handPoseLeft.Position.Y + originPos.Y;// + _hmdPoser.Y;
                _leftTouchMatrix.M43 = handPoseLeft.Position.Z + originPos.Z;// + _hmdPoser.Z;
























                /*Vector3 vecOne = _screenModel.vertices[1].position - _screenModel.vertices[0].position;
                Vector3 vecTwo = _screenModel.vertices[2].position - _screenModel.vertices[0].position;
                Vector3 crossProd;
                Vector3.Cross(ref vecOne, ref vecTwo, out crossProd);
                var screenNormal = Vector3.Normalize(crossProd);
                var planer = new Plane();
                */

                //Matrix projectionMatrix = D3D.ProjectionMatrix;

                /*SharpDX.Matrix _WorldMatrix = D3D.WorldMatrix;
                if (Program._KeyboardState.PressedKeys.Contains(Key.Q))
                {
                    movePos.Y -= 0.01f;
                    _WorldMatrix.M42 = originPos.Y + movePos.Y;
                    Camera.SetPosition(_WorldMatrix.M41, _WorldMatrix.M42, _WorldMatrix.M43);
                }
                else if (Program._KeyboardState.PressedKeys.Contains(Key.Z))
                {
                    movePos.Y += 0.01f;
                    _WorldMatrix.M42 = originPos.Y + movePos.Y;
                    Camera.SetPosition(_WorldMatrix.M41, _WorldMatrix.M42, _WorldMatrix.M43);
                }*/

                /*if (buttonPressedOculusTouchLeft != 0)
                {
                    if (buttonPressedOculusTouchLeft == 256)
                    {
                        if (hasClickedBUTTONX == 0)
                        {
                            RotationScreenX = RotationScreenX + 0.1f;
                            RotationScreenY = RotationScreenY;
                            RotationScreenZ = RotationScreenZ;

                            float pitcher = RotationScreenX * 0.0174532925f;
                            float yawer = RotationScreenY * 0.0174532925f;
                            float roller = RotationScreenZ * 0.0174532925f;

                            rotatingMatrixScreen = SharpDX.Matrix.RotationYawPitchRoll(yawer, pitcher, roller);

                            rotatingMatrixScreen.M41 = originPosScreen.X;
                            rotatingMatrixScreen.M42 = originPosScreen.Y;
                            rotatingMatrixScreen.M43 = originPosScreen.Z;

                            //IF ALONG X AXIS => i am not sure but i think i can use this EVEN if the rotation would happen in the 3 axis. I just have to separately apply the rotations on each axis. pretty cool
                            Vector3 pointOne = new Vector3(_screenModel.vertices[0].position.X, _screenModel.vertices[0].position.Y, _screenModel.vertices[0].position.Z);
                            Vector3 midPosLeftOne = (_screenModel.vertices[1].position) - (_screenModel.vertices[0].position);
                            midPosLeftOne *= 0.5f;
                            Vector3 middleOffsetCenterRotOne = (_screenModel.vertices[0].position + midPosLeftOne);
                            Vector2 rotatePointOne = new Vector2(pointOne.Y, pointOne.Z);
                            Vector2 centerPointerOne = new Vector2(middleOffsetCenterRotOne.Y, middleOffsetCenterRotOne.Z);
                            Vector2 rotatedPointOne = RotatePoint(rotatePointOne, centerPointerOne, (RotationScreenX));
                            _screenDirMatrix[0] = rotatingMatrixScreen;
                            _screenDirMatrix[0].M41 = pointOne.X;
                            _screenDirMatrix[0].M42 = rotatedPointOne.X;
                            _screenDirMatrix[0].M43 = rotatedPointOne.Y;
                            _screenDirMatrix[0].M41 += originPosScreen.X;
                            _screenDirMatrix[0].M42 += originPosScreen.Y;
                            _screenDirMatrix[0].M43 += originPosScreen.Z;

                            Vector3 pointTwo = new Vector3(_screenModel.vertices[1].position.X, _screenModel.vertices[1].position.Y, _screenModel.vertices[1].position.Z);
                            Vector3 midPosLeftTwo = (_screenModel.vertices[0].position) - (_screenModel.vertices[1].position);
                            midPosLeftTwo *= 0.5f;
                            Vector3 middleOffsetCenterRotTwo = (_screenModel.vertices[1].position + midPosLeftTwo);
                            Vector2 rotatePointTwo = new Vector2(pointTwo.Y, pointTwo.Z);
                            Vector2 centerPointerTwo = new Vector2(middleOffsetCenterRotTwo.Y, middleOffsetCenterRotTwo.Z);
                            Vector2 rotatedPointTwo = RotatePoint(rotatePointTwo, centerPointerTwo, (RotationScreenX));
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
                            Vector2 rotatedPointThree = RotatePoint(rotatePointThree, centerPointerThree, (RotationScreenX));
                            _screenDirMatrix[2] = rotatingMatrixScreen;
                            _screenDirMatrix[2].M41 = pointThree.X;
                            _screenDirMatrix[2].M42 = rotatedPointThree.X;
                            _screenDirMatrix[2].M43 = rotatedPointThree.Y;
                            _screenDirMatrix[2].M41 += originPosScreen.X;
                            _screenDirMatrix[2].M42 += originPosScreen.Y;
                            _screenDirMatrix[2].M43 += originPosScreen.Z;










                            //hasClickedBUTTONX = 1;
                        }
                    }
                    else if (buttonPressedOculusTouchLeft == 512)
                    {
                        if (hasClickedBUTTONY == 0)
                        {
                            RotationScreenX = RotationScreenX - 0.1f;
                            RotationScreenY = RotationScreenY;
                            RotationScreenZ = RotationScreenZ;

                            float pitcher = RotationScreenX * 0.0174532925f;
                            float yawer = RotationScreenY * 0.0174532925f;
                            float roller = RotationScreenZ * 0.0174532925f;

                            rotatingMatrixScreen = SharpDX.Matrix.RotationYawPitchRoll(yawer, pitcher, roller);

                            rotatingMatrixScreen.M41 = originPosScreen.X;
                            rotatingMatrixScreen.M42 = originPosScreen.Y;
                            rotatingMatrixScreen.M43 = originPosScreen.Z;

                            //IF ALONG X AXIS => i am not sure but i think i can use this EVEN if the rotation would happen in the 3 axis. I just have to separately apply the rotations on each axis. pretty cool
                            Vector3 pointOne = new Vector3(_screenModel.vertices[0].position.X, _screenModel.vertices[0].position.Y, _screenModel.vertices[0].position.Z);
                            Vector3 midPosLeft = (_screenModel.vertices[1].position) - (_screenModel.vertices[0].position);
                            midPosLeft *= 0.5f;
                            Vector3 middleOffsetCenterRot = (_screenModel.vertices[0].position + midPosLeft);
                            Vector2 rotatePoint = new Vector2(pointOne.Y, pointOne.Z);
                            Vector2 centerPointer = new Vector2(middleOffsetCenterRot.Y, middleOffsetCenterRot.Z);
                            Vector2 rotatedPoint = RotatePoint(rotatePoint, centerPointer, (RotationScreenX));
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
                            Vector2 rotatedPointTwo = RotatePoint(rotatePointTwo, centerPointerTwo, (RotationScreenX));
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
                            Vector2 rotatedPointThree = RotatePoint(rotatePointThree, centerPointerThree, (RotationScreenX));
                            _screenDirMatrix[2] = rotatingMatrixScreen;
                            _screenDirMatrix[2].M41 = pointThree.X;
                            _screenDirMatrix[2].M42 = rotatedPointThree.X;
                            _screenDirMatrix[2].M43 = rotatedPointThree.Y;
                            _screenDirMatrix[2].M41 += originPosScreen.X;
                            _screenDirMatrix[2].M42 += originPosScreen.Y;
                            _screenDirMatrix[2].M43 += originPosScreen.Z;

                        }
                    }
                }*/




                try
                {

                    //_SystemTickPerformance.Stop();
                    //_SystemTickPerformance.Reset();
                    //_SystemTickPerformance.Start();


                    for (int i = 0; i < 10;)
                    {
                        _failed = 0;
                        try
                        {
                            _desktopFrame = _desktopDupe.ScreenCapture(0);
                        }
                        catch (Exception ex)
                        {
                            //Console.WriteLine(ex.ToString());
                            _desktopDupe = new SC_SharpDX_ScreenCapture(0, 0);
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
                            D3D.layerEyeFov.RenderPoseLeft = eyePoses[0];
                        else
                            D3D.layerEyeFov.RenderPoseRight = eyePoses[1];

                        // Update the render description at each frame, as the HmdToEyeOffset can change at runtime.
                        eyeTexture.RenderDescription = D3D.OVR.GetRenderDesc(D3D.sessionPtr, eye, D3D.hmdDesc.DefaultEyeFov[eyeIndex]);

                        // Retrieve the index of the active texture

                        D3D.result = eyeTexture.SwapTextureSet.GetCurrentIndex(out textureIndex);
                        D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to retrieve texture swap chain current index.");

                        D3D.device.ImmediateContext.OutputMerger.SetRenderTargets(eyeTexture.DepthStencilView, eyeTexture.RenderTargetViews[textureIndex]);
                        D3D.device.ImmediateContext.ClearRenderTargetView(eyeTexture.RenderTargetViews[textureIndex], SharpDX.Color.DimGray);
                        D3D.device.ImmediateContext.ClearDepthStencilView(eyeTexture.DepthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);
                        D3D.device.ImmediateContext.Rasterizer.SetViewport(eyeTexture.Viewport);

                        eyePos = SharpDX.Vector3.Transform(new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z), originRot * rotatingMatrix).ToVector3(); //*rotatingMatrix //new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z

                        eyeQuaternionMatrix = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X, eyePoses[eyeIndex].Orientation.Y, eyePoses[eyeIndex].Orientation.Z, eyePoses[eyeIndex].Orientation.W));
                        finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix; //*rotatingMatrix

                        lookUp = Vector3.Transform(new Vector3(0, 1, 0), finalRotationMatrix).ToVector3(); //new Vector3(0, 1, 0)
                        lookAt = Vector3.Transform(new Vector3(0, 0, -1), finalRotationMatrix).ToVector3(); //new Vector3(0, 0, -1)

                        //var test = Vector3.TransformCoordinate(lookAt, rotatingMatrix);

                        viewPosition = eyePos + originPos; //eyePoses[eyeIndex].Position.ToVector3()
                        viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);

                        _projectionMatrix = D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.01f, 100.0f, ProjectionModifier.None).ToMatrix();
                        _projectionMatrix.Transpose();

                        _WorldMatrix = WorldMatrix;



                        //_grid_X.Render(D3D.device.ImmediateContext);
                        //_shaderManager.RenderGrid(D3D.device.ImmediateContext, _grid_X.IndexCount, WorldMatrix, viewMatrix, _projectionMatrix);


                        //TERRAIN AND GRID RENDER 
                        //_grid_X.Render(D3D.device.ImmediateContext);



                        /*if (_shaderManager._DTerrain_World_Axis_X == null)
                        {
                            //MessageBox((IntPtr)0, "NULL XGRID", "Oculus Error", 0);
                        }*/




                        ///_shaderManager.RenderGridTerrain(D3D.device.ImmediateContext, _grid_X.VertexCount, 1, _WorldMatrix, viewMatrix, _projectionMatrix, _basicTexture.TextureResource); //_basicTexture.TextureResource

                        /*_rightTouch.Render(D3D.device.ImmediateContext);
                        _shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _rightTouch.IndexCount, _rightTouch.InstanceCount, _rightTouchMatrix, viewMatrix, _projectionMatrix, _basicTexture.TextureResource);

                        _leftTouch.Render(D3D.device.ImmediateContext);
                        _shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _leftTouch.IndexCount, _leftTouch.InstanceCount, _leftTouchMatrix, viewMatrix, _projectionMatrix, _basicTexture.TextureResource);
                        */






                        //_grid_Y.Render(D3D.device.ImmediateContext);
                        //_shaderManager.RenderGrid(D3D.device.ImmediateContext, _grid_Y.IndexCount, _WorldMatrix, viewMatrix, _projectionMatrix);


                        //_FloorTiles.Render(D3D.device.ImmediateContext);
                        //_shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _FloorTiles.IndexCount, _FloorTiles.InstanceCount, matroxer2, viewMatrix, _projectionMatrix, _desktopFrame._ShaderResource);


                        /*for (int i = 0;i < _FloorTiles.Length;i++)
                        {
                            _FloorTiles[i].Render(D3D.device.ImmediateContext);
                            _shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _FloorTiles[i].IndexCount, _FloorTiles[i].InstanceCount, _WorldMatrix, viewMatrix, _projectionMatrix, _basicTexture.TextureResource);

                        }*/






                        //_WORLD_GRID_X.Render(D3D.device.ImmediateContext);
                        //Console.WriteLine(_WORLD_GRID_X.IndexCount);
                        //_WorldMatrix = WorldMatrix;
                        //_shaderManager.RenderGrid(D3D.device.ImmediateContext, _WORLD_GRID_X.IndexCount, _WorldMatrix, viewMatrix, _projectionMatrix);











                        //_mouseCursor.Render(D3D.device.ImmediateContext);
                        //_shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _mouseCursor.IndexCount, _mouseCursor.InstanceCount, _mouseCursorMatrix, viewMatrix, _projectionMatrix, _basicTexture.TextureResource);

                        try
                        {
                            /*float pitch = (body.shape.gameObject.OriginRotationX) * 0.0174532925f;
                            float yaw = (body.shape.gameObject.OriginRotationY) * 0.0174532925f;
                            float roll = (body.shape.gameObject.OriginRotationZ) * 0.0174532925f;
                            Matrix currentMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                            float pitcher = (body.shape.gameObject.CurrentRotationX) * 0.0174532925f;
                            float yawer = (body.shape.gameObject.CurrentRotationY) * 0.0174532925f;
                            float roller = (angleDiff) * 0.0174532925f;
                            Matrix currentMatrixer = SharpDX.Matrix.RotationYawPitchRoll(yawer, pitcher, roller);*/








                            /*matroxer2 = _leftTouchMatrix;// Matrix.Multiply(_leftTouchMatrix, rotatingMatrixScreen);


                            //matroxer2 = Matrix.Identity;
                            //matroxer2.M41 = originPosScreen.X;
                            //matroxer2.M42 = originPosScreen.Y;
                            //matroxer2.M43 = originPosScreen.Z;

                            Quaternion _testQuat;

                            Quaternion.RotationMatrix(ref matroxer2, out _testQuat);

                            var xq = _testQuat.X;
                            var yq = _testQuat.Y;
                            var zq = _testQuat.Z;
                            var wq = _testQuat.W;


                            var roll = Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
                            var pitch = Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
                            var yaw = Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);

                            //2nd
                            //var roll = Math.Atan2(2 * yq * wq + 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
                            //var pitch = Math.Atan2(2 * xq * wq + 2 * yq * zq, 1 - 2 * xq * xq - 2 * zq * zq);
                            //var yaw = Math.Asin(2 * xq * yq + 2 * zq * wq);

                            //1st
                            //roll = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
                            //pitch = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
                            //yaw = Mathf.Asin(2 * x * y + 2 * z * w);


                            
                            RotationScreenX = pitch;// / 0.01745327925f;
                            RotationScreenY = yaw;/// 0.0174532925f;
                            RotationScreenZ = roll;/// 0.0174532925f;

                            */






                            //_screenModel.Render(D3D.device.ImmediateContext);
                            //_shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _screenModel.IndexCount, _screenModel.InstanceCount, matroxer2, viewMatrix, _projectionMatrix, _desktopFrame._ShaderResource);


                            /*Vector3 tester = new Vector3(_intersectTouchRightMatrix.M41, _intersectTouchRightMatrix.M42, _intersectTouchRightMatrix.M43);
                            _intersectTouchRightMatrix = matroxer2;

                            _intersectTouchRightMatrix.M41 = tester.X;
                            _intersectTouchRightMatrix.M42 = tester.Y;
                            _intersectTouchRightMatrix.M43 = tester.Z;


                            _intersectTouchRight.Render(D3D.device.ImmediateContext);
                            _shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _intersectTouchRight.IndexCount, _intersectTouchRight.InstanceCount, _intersectTouchRightMatrix, viewMatrix, _projectionMatrix, _basicTexture.TextureResource);



                            tester = new Vector3(_intersectTouchLeftMatrix.M41, _intersectTouchLeftMatrix.M42, _intersectTouchLeftMatrix.M43);
                            _intersectTouchLeftMatrix = matroxer2;

                            _intersectTouchLeftMatrix.M41 = tester.X;
                            _intersectTouchLeftMatrix.M42 = tester.Y;
                            _intersectTouchLeftMatrix.M43 = tester.Z;

                            _intersectTouchLeft.Render(D3D.device.ImmediateContext);
                            _shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _intersectTouchLeft.IndexCount, _intersectTouchLeft.InstanceCount, _intersectTouchLeftMatrix, viewMatrix, _projectionMatrix, _basicTexture.TextureResource);
                            */



















                            //_screen_grid_Y.Render(D3D.device.ImmediateContext);
                            //_shaderManager.RenderGrid(D3D.device.ImmediateContext, _screen_grid_Y.IndexCount, matroxer2, viewMatrix, _projectionMatrix);
                            /*
                            _screen_metric_grid_Y.Render(D3D.device.ImmediateContext);
                            _shaderManager.RenderGrid(D3D.device.ImmediateContext, _screen_metric_grid_Y.IndexCount, matroxer2, viewMatrix, _projectionMatrix);
                            */




                            //SCREEN GRID
                            //_screen_grid_X.Render(D3D.device.ImmediateContext);
                            //_shaderManager.RenderGrid(D3D.device.ImmediateContext, _screen_grid_Z.IndexCount, rotatingMatrixScreen, viewMatrix, _projectionMatrix);


                            //_screen_grid_Z.Render(D3D.device.ImmediateContext);
                            //_shaderManager.RenderGrid(D3D.device.ImmediateContext, _screen_grid_Z.IndexCount, rotatingMatrixScreen, viewMatrix, _projectionMatrix);










































                            //vecOne = vert1 - vert0;
                            //vecTwo = vert2 - vert0;

                            //Vector3.Cross(ref vecOne, ref vecTwo, out crossProd);
                            //screenNormal = Vector3.Normalize(crossProd);

                            /*Quaternion _testQuater;

                            Quaternion.RotationMatrix(ref matroxer2, out _testQuater);
                            //_testQuater.Normalize();

                            screenNormal = _getDirection(Vector3.ForwardRH, _testQuater);
                            screenNormal.Normalize();

                            planer = new Plane(new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43), screenNormal);


                            centerPosRight = new Vector3(_rightTouchMatrix.M41, _rightTouchMatrix.M42, _rightTouchMatrix.M43);
                            rayDirRight = _getDirection(Vector3.ForwardRH, _rightTouchQuat);
                            someRay = new Ray(centerPosRight, rayDirRight);

                            intersecter = someRay.Intersects(ref planer, out intersectPointRight);
                            _intersectTouchRightMatrix = matroxer2;
                            _intersectTouchRightMatrix.M41 = intersectPointRight.X;
                            _intersectTouchRightMatrix.M42 = intersectPointRight.Y;
                            _intersectTouchRightMatrix.M43 = intersectPointRight.Z;


                            centerPosLeft = new Vector3(_leftTouchMatrix.M41, _leftTouchMatrix.M42, _leftTouchMatrix.M43);
                            rayDirLeft = _getDirection(Vector3.ForwardRH, _leftTouchQuat);
                            someRayLeft = new Ray(centerPosLeft, rayDirLeft);

                            intersecterLeft = someRayLeft.Intersects(ref planer, out intersectPointLeft);
                            _intersectTouchLeftMatrix = matroxer2;
                            _intersectTouchLeftMatrix.M41 = intersectPointLeft.X;
                            _intersectTouchLeftMatrix.M42 = intersectPointLeft.Y;
                            _intersectTouchLeftMatrix.M43 = intersectPointLeft.Z;



                            */












                            /*screenNormal = _getDirection(Vector3.Right, _testQuater);
                            screenNormal.Normalize();

                            var screenNormalRight = new Vector3(screenNormal.X, screenNormal.Y, screenNormal.Z);

                            screenNormal = _getDirection(Vector3.Up, _testQuater);
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
                            _screenDirMatrix_correct_pos[0] = resulter;



                            currentScreenPos = new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43);
                            newDirRight = (screenNormalRight) * sizeWidtherer; // + screenNormalTop
                            currentScreenPos -= newDirRight;

                            newDirUp = (screenNormalTop) * sizeheighterer; // + screenNormalTop
                            currentScreenPos += newDirUp;

                            resulter = matroxer2;
                            resulter.M41 = currentScreenPos.X;
                            resulter.M42 = currentScreenPos.Y;
                            resulter.M43 = currentScreenPos.Z;
                            _screenDirMatrix_correct_pos[1] = resulter;


                            currentScreenPos = new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43);
                            newDirRight = (screenNormalRight) * sizeWidtherer; // + screenNormalTop
                            currentScreenPos += newDirRight;

                            newDirUp = (screenNormalTop) * sizeheighterer; // + screenNormalTop
                            currentScreenPos -= newDirUp;

                            resulter = matroxer2;
                            resulter.M41 = currentScreenPos.X;
                            resulter.M42 = currentScreenPos.Y;
                            resulter.M43 = currentScreenPos.Z;
                            _screenDirMatrix_correct_pos[2] = resulter;



                            currentScreenPos = new Vector3(matroxer2.M41, matroxer2.M42, matroxer2.M43);
                            newDirRight = (screenNormalRight) * sizeWidtherer; // + screenNormalTop
                            currentScreenPos += newDirRight;

                            newDirUp = (screenNormalTop) * sizeheighterer; // + screenNormalTop
                            currentScreenPos += newDirUp;

                            resulter = matroxer2;
                            resulter.M41 = currentScreenPos.X;
                            resulter.M42 = currentScreenPos.Y;
                            resulter.M43 = currentScreenPos.Z;
                            _screenDirMatrix_correct_pos[3] = resulter;


                            //for (int i = 0; i < _screenCorners.Length; i++)
                            //{
                            //    _screenCorners[i].Render(D3D.device.ImmediateContext);
                            //    _shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _screenCorners[i].IndexCount, _screenCorners[i].InstanceCount, _screenDirMatrix_correct_pos[i], viewMatrix, _projectionMatrix, _basicTexture.TextureResource);
                            //}





                            for (int i = 0; i < _screenDirMatrix_correct_pos.Length; i++)
                            {
                                //_screenDirMatrix_correct_pos[i] = _screenDirMatrix[i];

                                //_screenDirMatrix_correct_pos[i] += matroxer2;

                                point3DCollection[i] = new Vector3(_screenDirMatrix_correct_pos[i].M41, _screenDirMatrix_correct_pos[i].M42, _screenDirMatrix_correct_pos[i].M43);
                            }*/






















































                            /*//RIGHT CONTROLLER HITPOINT
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

                                arrayOfStabilizerPosXRight = new double[arrayOfStabilizerPosRight.Length];
                                arrayOfStabilizerPosDifferenceXRight = new double[arrayOfStabilizerPosXRight.Length - 1];

                                arrayOfStabilizerPosYRight = new double[arrayOfStabilizerPosRight.Length];
                                arrayOfStabilizerPosDifferenceYRight = new double[arrayOfStabilizerPosYRight.Length - 1];

                                arrayOfStabilizerPosZRight = new double[arrayOfStabilizerPosRight.Length];
                                arrayOfStabilizerPosDifferenceZRight = new double[arrayOfStabilizerPosZRight.Length - 1];

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


                            //var distance = stabilizedIntersectionPosRight - vert0;








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


                                arrayOfStabilizerPosXLeft = new double[arrayOfStabilizerPosLeft.Length];
                                arrayOfStabilizerPosDifferenceXLeft = new double[arrayOfStabilizerPosXLeft.Length - 1];

                                arrayOfStabilizerPosYLeft = new double[arrayOfStabilizerPosLeft.Length];
                                arrayOfStabilizerPosDifferenceYLeft = new double[arrayOfStabilizerPosYLeft.Length - 1];

                                arrayOfStabilizerPosZLeft = new double[arrayOfStabilizerPosLeft.Length];
                                arrayOfStabilizerPosDifferenceZLeft = new double[arrayOfStabilizerPosZLeft.Length - 1];

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

                            _MicrosoftWindowsMouseRight(percentXRight, percentYRight, thumbStickRight, percentXLeft, percentYLeft, thumbStickLeft);

                            lastHasUsedHandTriggerLeft = hasUsedHandTriggerLeft;
                            lastbuttonPressedOculusTouchRight = buttonPressedOculusTouchRight;
                            lastbuttonPressedOculusTouchLeft = buttonPressedOculusTouchLeft;


                            _mouseCursorMatrix.M41 = (float)((percentXRight * 65535) / D3D.SurfaceWidth);
                            _mouseCursorMatrix.M42 = (float)((percentYRight * 65535) / D3D.SurfaceHeight);




                            */

                            /*//CIRCLE CIRCLE INTERSECTION //http://mathworld.wolfram.com/Circle-CircleIntersection.html
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
                            */




                            //var absoluteMoveX = Convert.ToUInt32((percentXRight * 65535) / D3D.SurfaceWidth);
                            //var absoluteMoveY = Convert.ToUInt32((percentYRight * 65535) / D3D.SurfaceHeight);




                        }
                        catch (Exception ex)
                        {
                            MessageBox((IntPtr)0, ex.ToString(), "Oculus Error", 0);
                        }



                        Matrix translationMatrix;
                        int count = 0;
                        int clothCount = 0;
                        int clothCountCorners = 0;
                        //foreach (RigidBody body in World.RigidBodies)
                        //{
















                        /*for (int i = 0; i < _arrayOfCubesBeforeInstance.Count; i++)
                        {
                            _arrayOfCubesBeforeInstance[i].Render(D3D.device.ImmediateContext);
                        }*/








                        //Console.WriteLine(World.RigidBodies.Count);

                        /*
                        if (World.RigidBodies.Count > 0)
                        {
                            count = 0;
                            //Console.WriteLine(World.RigidBodies.Count);
                            IEnumerator enumerator = World.RigidBodies.GetEnumerator();
                            int testIndex = 0;
                            while (enumerator.MoveNext())
                            {
                                RigidBody body = (RigidBody)enumerator.Current;

                                if (body != null && body.Tag != null)
                                {
                                    //if (body.IsStaticOrInactive == false)
                                    {
                                        if ((SC_Console_DIRECTX.BodyTag)body.Tag == SC_Console_DIRECTX.BodyTag.physicsInstancedCube)
                                        {
                                            var _tempMatrix = Matrix.Identity;// WorldMatrix;

                                            //translationMatrix = Matrix.Identity;

                                            Matrix.Translation(body.Position.X, body.Position.Y, body.Position.Z, out translationMatrix);
                                            Matrix.Multiply(ref _tempMatrix, ref translationMatrix, out _tempMatrix);

                                            _tempMatrix.M11 = body.Orientation.M11;
                                            _tempMatrix.M12 = body.Orientation.M12;
                                            _tempMatrix.M13 = body.Orientation.M13;
                                            //_tempMatrix.M14 = body.Orientation.M14;

                                            _tempMatrix.M21 = body.Orientation.M21;
                                            _tempMatrix.M22 = body.Orientation.M22;
                                            _tempMatrix.M23 = body.Orientation.M23;
                                            //_tempMatrix.M24 = body.Orientation.M24;

                                            _tempMatrix.M31 = body.Orientation.M31;
                                            _tempMatrix.M32 = body.Orientation.M32;
                                            _tempMatrix.M33 = body.Orientation.M33;
                                            /*
                                            _tempMatrix.M41 = body.Position.X;
                                            _tempMatrix.M42 = body.Position.Y;
                                            _tempMatrix.M43 = body.Position.Z;
                                            _tempMatrix.M44 = 1;





                                            _arrayOfCubesBeforeInstance[count]._POSITION = _tempMatrix;
                                            _arrayOfCubesBeforeInstance[count].Render(D3D.device.ImmediateContext);
                                            _shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _arrayOfCubesBeforeInstance[count].IndexCount, _arrayOfCubesBeforeInstance[count].InstanceCount, _arrayOfCubesBeforeInstance[count]._POSITION, viewMatrix, _projectionMatrix, _basicTexture.TextureResource);

                                            count++;

                                        }
                                    }
                                }

                                //testIndex++;
                            }
                        }*/






                        /*LastRotationScreenX = RotationScreenX;
                        LastRotationScreenY = RotationScreenY;
                        LastRotationScreenZ = RotationScreenZ;
                        */

















                        //_terrainSolid.Render(D3D.device.ImmediateContext);
                        //_shaderManager.RenderInstancedObject(D3D.device.ImmediateContext, _terrainSolid.IndexCount, 1, _terrainSolid._POSITION, viewMatrix, _projectionMatrix, _basicTexture.TextureResource);











                        D3D.result = eyeTexture.SwapTextureSet.Commit();
                        D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to commit the swap chain texture.");
                    }

                    //Camera.SetPosition(realPos.X, realPos.Y, realPos.Z);
                    //var yo = OVR.GetTrackerPose(sessionPtr, (uint)TrackedDeviceType.HMD);
                    //var test = yo.Pose;
                    //realPos = new Vector3(test.Position.X, test.Position.Y, test.Position.Z);

                    D3D.result = D3D.OVR.SubmitFrame(D3D.sessionPtr, 0L, IntPtr.Zero, ref D3D.layerEyeFov);
                    D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to submit the frame of the current layers.");


                    D3D.deviceContext.CopyResource(D3D.mirrorTextureD3D, D3D.BackBuffer);
                    D3D.SwapChain.Present(0, PresentFlags.None);

                    /*if (SC_Console_CORE._SC_GLOB._Activate_Desktop_Image == 1)
                    {
                        D3D.deviceContext.CopyResource(D3D.mirrorTextureD3D, D3D.BackBuffer);
                        D3D.SwapChain.Present(0, PresentFlags.None);
                    }*/
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }



            /*if (_lastShaderResourceView != null)
            {
                _lastShaderResourceView.Dispose();
            }

            _lastShaderResourceView = _desktopFrame._ShaderResource;
            */

















            // Generate the view matrix based on the camera position.
            //Camera.Render();

            // Get the world, view, and projection matrices from camera and d3d objects.
            //Matrix viewMatrix = Camera.ViewMatrix;
            //Matrix worldMatrix = D3D.WorldMatrix;
            //Matrix projectionMatrix = D3D.ProjectionMatrix;

            // Put the model vertex and index buffers on the graphics pipeline to prepare them for drawing.
            /*Model.Render(D3D.deviceContext);

            // Render the model using the color shader.
            if (!TextureShader.Render(D3D.deviceContext, Model.IndexCount, worldMatrix, viewMatrix, projectionMatrix, Model.Texture.TextureResource))
                return false;
            */
            // Present the rendered scene to the screen.
            //D3D.EndScene();
            return _someReceivedObject0;
        }


        ShaderResourceView _lastShaderResourceView;



        static KeyboardState _KeyboardState;
        public static SharpDX.DirectInput.Keyboard _Keyboard;

        private static bool ReadKeyboard()
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




        public int _indexMouseMove = 0;
        Stopwatch _updateFunctionStopwatchLeftHandTrigger;
        Stopwatch _updateFunctionStopwatchRightHandTrigger;
        Stopwatch _updateFunctionStopwatchLeftThumbstickGoRight;
        Stopwatch _updateFunctionStopwatchLeftThumbstickGoLeft;
        Stopwatch _updateFunctionStopwatchRightThumbstickGoRight;
        Stopwatch _updateFunctionStopwatchRightThumbstickGoLeft;
        Stopwatch _updateFunctionStopwatchRightThumbstick;
        bool _updateFunctionBoolRightThumbStick = true;

        Stopwatch _updateFunctionStopwatchLeftThumbstick;
        bool _updateFunctionBoolLeftThumbStick = true;


        Stopwatch _updateFunctionStopwatchRightIndexTrigger;
        Stopwatch _updateFunctionStopwatchLeftIndexTrigger;
        Stopwatch _updateFunctionStopwatchRight;
        Stopwatch _updateFunctionStopwatchLeft;
        Stopwatch _updateFunctionStopwatchTouchRightButtonA;
        Stopwatch _newStopWatch = new Stopwatch();
        Stopwatch _mainThreadStopWatch = new Stopwatch();

        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_NOACTIVATE = 0x0010;
        private const int GWL_STYLE = -16;
        private const int WS_MINIMIZE = -131073;
        int _frameCounterTouchRight = 0;

        public static int _mainThreadFrameCounter = 0;
        private static int _frameCounter = 0;
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

                            //https://stackoverflow.com/questions/2929255/unable-to-launch-onscreen-keyboard-osk-exe-from-a-32-bit-process-on-win7-x64
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
                            }

                            hasClickedBUTTONX = 1;
                            _updateFunctionBoolLeftThumbStick = true;
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



        /*Matrix RotationNeededToAlignArbitraryVectorWithZAxis(Vector3 arbitraryVector)
        {
            arbitraryVector.Normalize();
            Vector3 axis = Vector3.Cross(arbitraryVector, Vector3.UnitZ);
            float partialAngle = (float)Math.Asin(axis.Length());
            float finalAngle = arbitraryVector.Z < 0f ? MathHelper.PiOver2 - partialAngle + MathHelper.PiOver2 : partialAngle;
            axis.Normalize();
            return Matrix.RotationAxis(axis,finalAngle);
            //return Matrix.CreateFromAxisAngle(axis, finalAngle);
        }*/






        Vector3 rotate(float pitch, float roll, float yaw, Vector3 pointer)
        {
            var cosa = Math.Cos(yaw);
            var sina = Math.Sin(yaw);

            var cosb = Math.Cos(pitch);
            var sinb = Math.Sin(pitch);

            var cosc = Math.Cos(roll);
            var sinc = Math.Sin(roll);

            var Axx = cosa * cosb;
            var Axy = cosa * sinb * sinc - sina * cosc;
            var Axz = cosa * sinb * cosc + sina * sinc;

            var Ayx = sina * cosb;
            var Ayy = sina * sinb * sinc + cosa * cosc;
            var Ayz = sina * sinb * cosc - cosa * sinc;

            var Azx = -sinb;
            var Azy = cosb * sinc;
            var Azz = cosb * cosc;

            var px = pointer.X;
            var py = pointer.Y;
            var pz = pointer.Z;

            pointer.X = (float)(Axx * px + Axy * py + Axz * pz);
            pointer.Y = (float)(Ayx * px + Ayy * py + Ayz * pz);
            pointer.Z = (float)(Azx * px + Azy * py + Azz * pz);


            return pointer;



            /*for (var i = 0; i < points.length; i++)
            {
                var px = points[i].x;
                var py = points[i].y;
                var pz = points[i].z;

                points[i].x = Axx * px + Axy * py + Axz * pz;
                points[i].y = Ayx * px + Ayy * py + Ayz * pz;
                points[i].z = Azx * px + Azy * py + Azz * pz;
            }*/
        }





        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd,
            UInt32 Msg,
            IntPtr wParam,
            IntPtr lParam);
        private const UInt32 WM_SYSCOMMAND = 0x112;
        private const UInt32 SC_RESTORE = 0xf120;

        private const string OnScreenKeyboardExe = "osk.exe";

        private static void ShowKeyboard()
        {
            var path64 = @"C:\Windows\winsxs\amd64_microsoft-windows-osk_31bf3856ad364e35_6.1.7600.16385_none_06b1c513739fb828\osk.exe";
            var path32 = @"C:\windows\system32\osk.exe";
            var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
            Process.Start(path);
        }

        static void StartOsk()
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




        public bool clearConsole(SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject0)
        {
            D3D.BeginScene(0.1f, 0.25f, 0.5f, 1f, _someReceivedObject0);
            D3D.EndScene();
            D3D.ClearSceneVisual();
            D3D.EndScene();

            return true;
        }

        public static double AngleBetween(Vector2 vector1, Vector2 vector2)
        {
            double sin = vector1.X * vector2.Y - vector2.X * vector1.Y;
            double cos = vector1.X * vector2.X + vector1.Y * vector2.Y;

            return Math.Atan2(sin, cos) * (180 / Math.PI);
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
            result = Scaling(scaling) * Translation(-rotationCenter) * RotationQuaternion(rotation) * Translation(rotationCenter) * Translation(translation);
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
