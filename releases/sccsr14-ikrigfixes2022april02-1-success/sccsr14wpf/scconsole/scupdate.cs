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

using sccs.scgraphics;
//using sccs.scgraphics.scshadermanager;

using Jitter;
using Jitter.Dynamics;
using Jitter.Collision;
using Jitter.LinearMath;
using Jitter.Collision.Shapes;
using Jitter.Forces;

using System.Collections.Generic;
using System.Collections;
using System.Runtime;
using System.Runtime.CompilerServices;

using System.ComponentModel;
using SharpDX.D3DCompiler;

using scmessageobject = sccs.scmessageobject.scmessageobject;
using scmessageobjectjitter = sccs.scmessageobject.scmessageobjectjitter;
using scgraphicssecpackage = sccs.scmessageobject.scgraphicssecpackage;

using System.Drawing;
using System.Drawing.Imaging;
using System;
using System.Windows;
//using System.Windows.Interop;

//using Win32.DwmThumbnail.Interop;
using Win32.Shared;
//using System.Windows.Controls;
using Win32.Shared.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Diagnostics;
using System.Windows.Forms;

using SharpDX;
//using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
//using SharpDX.Windows;
using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;
//using System.Windows.Threading;

using System;

//using Win32.Shared;
using System.Diagnostics;
using SharpDX.DXGI;

using SharpDX.Direct2D1;
using SharpDX.Mathematics;
using System.Runtime.InteropServices;

/*
using Jitter.LinearMath;
using sccs.scgraphics;
using sccs.sccore;
using sccs.scconsole;
using WindowsInput;*/
using System.Threading.Tasks;
//using System.Speech.Recognition;
//using System.Speech.Synthesis;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Text;
using System;
using System;
using System.Windows;
//using System.Windows.Interop;

//using Win32.DwmThumbnail.Interop;
//using Win32.Shared;
//using System.Windows.Controls;
//using Win32.DesktopDuplication;
using System;

//using Win32.Shared;
//using Win32.Shared.Interfaces;
//using Win32.Shared.Interop;
//using Win32;
using Win32.DwmSharedSurface;

using Jitter;
using Jitter.Dynamics;
using Jitter.LinearMath;

using sccs.scgraphics;
using sccs.sccore;
using sccs.scconsole;

using WindowsInput;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



using sccs.scmessageobject;
using Jitter.DataStructures;
using System.Windows.Interop;
using SharpDX.Windows;


using Matrix = SharpDX.Matrix;
using Vector3 = SharpDX.Vector3;
using Key = SharpDX.DirectInput.Key;
using Bitmap = System.Drawing.Bitmap;

using sccs.scgraphics.scshadermanager;

using WinRT.GraphicsCapture;

namespace sccs.scgraphics
{
    public class scupdate : scdirectx
    {


        public int haspressedheightmapvalueincrease = 0;
        public int haspressedheightmapvaluedecrease = 0;
        public int haspressedheightmapkey = 0;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public static int stopovr = 0;
        public Thread main_thread_update;
        public Thread heightmapthread;
        public int threadupdateswtc = 0;
        public static DateTime startTime;// = DateTime.Now;

        public static Matrix oriProjectionMatrix;
        public static int[] arduinoDIYOculusTouchArrayOfData = new int[12];
        ShaderResourceView _lastShaderResourceView;
        ShaderResourceView shaderRes;
        int screencaptureresultswtc = 0;
        Texture2DDescription _textureDescriptionFinal;

        public static _sc_camera Camera;
        public scgraphicssecpackage scgraphicssecpackagemessage;
        SharpDX.Matrix finalRotationMatrix;
        Vector3 lookUp;
        Vector3 lookAt;
        public static Vector3 viewPosition;
        public static Matrix viewMatrix;
        public static Matrix _projectionMatrix;
        public static Vector3 OFFSETPOS;

        public static SharpDX.Vector3 movePos = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Matrix originRot = SharpDX.Matrix.Identity;

        public static SharpDX.Matrix rotatingMatrixForPelvis = SharpDX.Matrix.Identity;
        public static SharpDX.Matrix rotatingMatrix = SharpDX.Matrix.Identity;
        public int canworkphysics = 0;
        public static Matrix hmdmatrixRot = Matrix.Identity;
        int updatethreadupdateswtc0 = 0;
        int updatethreadupdateswtc1 = 0;
        Thread[] threads = new Thread[2];

        //OCULUS TOUCH SETTINGS 
        Ab3d.OculusWrap.Result resultsRight;
        uint buttonPressedOculusTouchRight;
        Vector2f[] thumbStickRight;
        public static float[] handTriggerRight;
        float[] indexTriggerRight;
        Ab3d.OculusWrap.Result resultsLeft;
        uint buttonPressedOculusTouchLeft;
        Vector2f[] thumbStickLeft;
        public static float[] handTriggerLeft;
        public static float[] indexTriggerLeft;
        public static Posef handPoseLeft;
        SharpDX.Quaternion _leftTouchQuat;
        public static Posef handPoseRight;
        SharpDX.Quaternion _rightTouchQuat;
        public static Matrix _leftTouchMatrix = Matrix.Identity;
        public static Matrix _rightTouchMatrix = Matrix.Identity;
        //OCULUS TOUCH SETTINGS 

        public static SharpDX.Vector3 originPos = new SharpDX.Vector3(0, 0, 0); //new SharpDX.Vector3(-10, 1, 10); //0.645f
        public static SharpDX.Vector3 originPosScreen = new SharpDX.Vector3(0, 0, -0.365f);

        public static SharpDX.Vector3 savedposition7 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedposition8 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedposition9 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedposition4 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedposition5 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedposition6 = new SharpDX.Vector3(0, 0, 0);

        public static SharpDX.Vector3 savedrotation7 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedrotation8 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedrotation9 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedrotation4 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedrotation5 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedrotation6 = new SharpDX.Vector3(0, 0, 0);

        public static SharpDX.Vector3 savedpositiontosetto = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedrotationtosetto = new SharpDX.Vector3(0, 0, 0);

        public Stopwatch savedposrotwatch = new Stopwatch();
        int savedposrotswtc = 0;
        int savedposrotswtcoptionsaveorselect = 0;






        float disco_sphere_rot_speed = 0.5f;

        float speedRot = 1.25f; //1.95f // 0.25f
        float speedRotArduino = 0.000001f;

        float speedPos = 0.15f; //0.025f // 1.5f
        float speedPosArduino = 0.001f;
        public static Matrix hmd_matrix;
        Matrix hmd_matrix_test;


        public static double RotationY = 0;//{ get; set; } { get; set; }
        public static double RotationX = 0;//{ get; set; } { get; set; }
        public static double RotationZ = 0;//{ get; set; } { get; set; }

        public static float RotationOriginY = 0;//{ get; set; }
        public static float RotationOriginX = 0;//{ get; set; }
        public static float RotationOriginZ = 0;//{ get; set; }

        float thumbstickIsRight;
        float thumbstickIsUp;


        double displayMidpoint;
        TrackingState trackingState;
        Posef[] eyePoses;
        public static Vector3 viewpositionorigin;
        EyeType eye;
        EyeTexture eyeTexture;
        bool latencyMark = false;
        TrackingState trackState;
        PoseStatef poseStatefer;
        Posef hmdPose;
        Quaternionf hmdRot;
        public static Vector3 _hmdPoser;
        Quaternion _hmdRoter;

        public static Matrix tempmatter;
        public static Vector4 dirikvoxelbodyInstanceRight0;
        public static Vector4 dirikvoxelbodyInstanceUp0;
        public static Vector4 dirikvoxelbodyInstanceForward0;

        public static double RotationY4Pelvis;
        public static double RotationX4Pelvis;
        public static double RotationZ4Pelvis;

        public static double RotationY4PelvisTwo;
        public static double RotationX4PelvisTwo;
        public static double RotationZ4PelvisTwo;


        public static double RotationGrabbedYOff;
        public static double RotationGrabbedXOff;
        public static double RotationGrabbedZOff;


        public static double RotationGrabbedY;
        public static double RotationGrabbedX;
        public static double RotationGrabbedZ;


        public static double Rotationscreenx;
        public static double Rotationscreeny;
        public static double Rotationscreenz;


        int _swtch_hasRotated = 0;
        int _has_grabbed_right_swtch = 0;
        int RotationGrabbedSwtch = 0;
        int _sec_logic_swtch_grab = 0;
        Matrix rotatingMatrixForGrabber = Matrix.Identity;

        //public static sccsscreenframe copiedframe;
        //public static sccsscreenframe screencaptureframe;
        //public static sccssharpdxscreencapture sharpdxscreencapture;
        public static sccsscreenframe screencaptureframe;
        public static sccssharpdxscreencapture sharpdxscreencapture;

        public static SC_ShaderManager _shaderManager;


        Thread proct0;
        Thread proct1;
        private static ManualResetEvent somereseteventt0 = new ManualResetEvent(false);
        private static ManualResetEvent somereseteventt1 = new ManualResetEvent(false);
        private static ManualResetEvent mre = new ManualResetEvent(false);
        ManualResetEvent currentWaitHandle;

        int[] somearray = new int[1];
        someintegerlock[] someintegerlockingstruct = new someintegerlock[2];

        struct someintegerlock
        {
            public int someintegerlocking;
            public ManualResetEvent somereseteventt00;
            public ManualResetEvent somereseteventt01;
        }

        //  public scupdate() : base(new DwmSharedSurface())
        public scupdate() : base(new GraphicsCapture())
        {

            /*inputsim = new InputSimulator();
            mousesim = new MouseSimulator(inputsim);
            keyboardsim = new KeyboardSimulator(inputsim);
            
            keynmouseinput = new DInput();
            keynmouseinput.Initialize(MainWindow.config, sccsr14sc.Form1.someform.Handle); //sccsr14sc.Form1.someform.SCGLOBALSACCESSORS.SCCONSOLECORE.handle
            */
        }
        //override
        protected void ShutDownGraphics()
        {
            /*if (sharpdxscreencapture != null)
            {
                sharpdxscreencapture.Disposer();
            }

            sharpdxscreencapture = null;*/
            //_shaderManager = null;



            /*D3D.OVR.Destroy(D3D.sessionPtr);
            if (D3D != null)
            {
                D3D = null;
            }*/

            MainWindow.MessageBox((IntPtr)0, "ShutDownGraphics scupdate.cs", "sc core systems message", 0);
        }


        public static DInput keynmouseinput;
        public static InputSimulator inputsim;
        public static KeyboardSimulator keyboardsim;
        public static MouseSimulator mousesim;



        public struct RECT
        {
            public int left, top, bottom, right;
        }
        Texture2D _texture2DFinal;
        ShaderResourceViewDescription resourceViewDescription;
        public static SharpDX.Direct3D11.Device deviceforscreencap;

        Stopwatch cammovementwatch = new Stopwatch();
        int cammovementswtc = 0;
        int canmovecamera = 0;

        //protected override //override
        public scmessageobjectjitter[][] init_update_variables(scmessageobjectjitter[][] _sc_jitter_tasks, sccs.sccore.scsystemconfiguration configuration) //, scconsole.scconsolewriter _writer //, IntPtr hwnd
        {
            try
            {

                Camera = new _sc_camera();


                if (MainWindow._useOculusRift == 0)
                {
                   
                    //originPos.Y += 4;
                    //originPos.Z -= 2f;

                    //MainWindow.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                    Camera.SetPosition(originPos.X, originPos.Y, originPos.Z);

                    Camera.SetRotation(0, 0, 0);
                }
                else if (MainWindow._useOculusRift == 1)
                {
                    speedRot = 1.75f;
                    speedPos = 1.25f;
                    RotationX = 0;
                    RotationY = 0;
                    RotationZ = 0;

                    RotationX4Pelvis = 0;
                    RotationY4Pelvis = 180;
                    RotationZ4Pelvis = 0;

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
                }



                /*var directInput = new DirectInput();

                _Keyboard = new SharpDX.DirectInput.Keyboard(directInput);

                _Keyboard.Properties.BufferSize = 128;
                _Keyboard.Acquire();*/


                scgraphicssecpackagemessage = new scgraphicssecpackage();
                startTime = DateTime.Now;



                _shaderManager = new SC_ShaderManager();
                _shaderManager.Initialize(D3D.Device, MainWindow.consoleHandle);

                
                /*_textureDescriptionFinal = new Texture2DDescription
                {
                    CpuAccessFlags = CpuAccessFlags.None,
                    BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                    Format = Format.B8G8R8A8_UNorm,
                    Width = 1920,
                    Height = 1080,
                    OptionFlags = ResourceOptionFlags.GenerateMipMaps,
                    MipLevels = 1,
                    ArraySize = 1,
                    SampleDescription = { Count = 1, Quality = 0 },
                    Usage = ResourceUsage.Default
                };
                _texture2DFinal = new Texture2D(device, _textureDescriptionFinal);


                resourceViewDescription = new ShaderResourceViewDescription
                {
                    Format = _texture2DFinal.Description.Format,
                    Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D,
                    Texture2D = new ShaderResourceViewDescription.Texture2DResource
                    {
                        MipLevels = -1,
                        MostDetailedMip = 0
                    }
                };

                SwapChainDescription swapChainDescription = new SwapChainDescription();
                swapChainDescription.BufferCount = 1;
                swapChainDescription.IsWindowed = true;
                swapChainDescription.OutputHandle = MainWindow.consoleHandle; //stackoverflow 59595739
                swapChainDescription.SampleDescription = new SampleDescription(1, 0);
                swapChainDescription.Usage = Usage.RenderTargetOutput | Usage.ShaderInput | Usage.BackBuffer;//Usage.RenderTargetOutput;// | Usage.ShaderInput;//| Usage.BackBuffer
                swapChainDescription.SwapEffect = SwapEffect.Sequential;
                swapChainDescription.Flags = SwapChainFlags.AllowModeSwitch;
                swapChainDescription.ModeDescription.Width = SurfaceWidth;
                swapChainDescription.ModeDescription.Height = SurfaceHeight;
                swapChainDescription.ModeDescription.Format = Format.R8G8B8A8_UNorm;
                swapChainDescription.ModeDescription.RefreshRate.Numerator = 0;
                swapChainDescription.ModeDescription.RefreshRate.Denominator = 1;

                SwapChain someswap;
                //SharpDX.Direct3D11.Device somedevice;
                // Create DirectX drawing device.
                //device = new Device(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.Debug);
                SharpDX.Direct3D11.Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None, swapChainDescription, out deviceforscreencap, out someswap);
                */
                // ignore all Windows events
                //using var factory = someswap.GetParent<Factory>();
                //factory.MakeWindowAssociation(MainWindow.consoleHandle, WindowAssociationFlags.IgnoreAll);

                //const int numAdapter = 0;
                //const int numOutput = 0;
                //var factory = new Factory1();
                //factory.MakeWindowAssociation(MainWindow.vewindowsfoundedz, WindowAssociationFlags.Valid);
                //var adapter = factory.GetAdapter1(numAdapter);
                //deviceforscreencap = new SharpDX.Direct3D11.Device(adapter);

                //rect = new RECT();
                //GetWindowRect(MainWindow.vewindowsfoundedz,out rect);


                someintegerlockingstruct[0].somereseteventt00 = new ManualResetEvent(true);
                someintegerlockingstruct[0].somereseteventt01 = new ManualResetEvent(false);


                if (MainWindow.usesharpdxscreencapture == 1)
                {
                    sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, device);
                }



                //g = MainWindow.form.CreateGraphics();
                //bmp = new System.Drawing.Bitmap(MainWindow.form.Size.Width, MainWindow.form.Size.Height, g);
                //memgraph = Graphics.FromImage(bmp);

                //windowbounds = new System.Drawing.Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);

                //somebitmap = new Bitmap(windowbounds.Width, windowbounds.Height);


                //DwmSharedSurface somesurf = new DwmSharedSurface(this);

                //somesurf.StartCapture();



                //sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, device);



                //_sc_jitter_tasks[0][0].hasinit = 0;
            }
            catch
            {

            }
            return _sc_jitter_tasks;
        }



        IntPtr sometest;
        Graphics memgraph;
        Graphics g;
        System.Drawing.Bitmap bmp;
        static RECT rect;
        //public static SharpDX.DirectInput.Keyboard _Keyboard;
        static System.Drawing.Rectangle windowbounds ;// new System.Drawing.Rectangle(rect.left, rect.top,rect.right-rect.left,rect.bottom-rect.top);
        static Bitmap somebitmap;
        static int bitmapcounter = 0;

        public static Bitmap CaptureWindow(IntPtr handle)
        {
            using (var g = Graphics.FromImage(somebitmap))
            {
                g.CopyFromScreen(new System.Drawing.Point(windowbounds.Left, windowbounds.Top), System.Drawing.Point.Empty, windowbounds.Size);


            }
            somebitmap.Save(@"C:\Users\steve\Desktop\screenRecord\" + bitmapcounter + "_" + ".png");
            bitmapcounter++;
            return somebitmap;
        }



        //protected override
        public  scmessageobjectjitter[][] Update(jitter_sc[] jitter_sc, scmessageobjectjitter[][] _sc_jitter_tasks)
        {
            try
            {
                _sc_jitter_tasks = _FrameVRTWO(jitter_sc, _sc_jitter_tasks);
           
            }
            catch (Exception ex)
            {
                MainWindow.MessageBox((IntPtr)0, "" + ex.ToString(), "sc core systems message", 0);
            }

            return _sc_jitter_tasks;
        }



        public static void createinputs(IntPtr thehandle)
        {
            inputsim = new InputSimulator();
            mousesim = new MouseSimulator(inputsim);
            keyboardsim = new KeyboardSimulator(inputsim);

            keynmouseinput = new DInput();
            keynmouseinput.Initialize(MainWindow.config, MainWindow.SCGLOBALSACCESSORS.SCCONSOLECORE.handle); //sccsr14sc.Form1.someform.SCGLOBALSACCESSORS.SCCONSOLECORE.handle //thehandle
        }



        Stopwatch canpressfmenukeystopwatch = new Stopwatch();
        int canpressfmenukeyswtc = 0;


        Stopwatch resetvaluesstopwatch = new Stopwatch();
        int resetvaluesswtc = 0;



        int initinputs = 0;

        float rotx = 0;
        float roty = 0;
        float rotz = 0;
        private unsafe scmessageobjectjitter[][] _FrameVRTWO(jitter_sc[] jitter_sc, scmessageobjectjitter[][] _sc_jitter_tasks)
        {


            /*if (initinputs == 0)
            {
                var refreshDXEngineAction = new Action(delegate
                {
                    if (sccsr14sc.Form1.someform != null)
                    {
                        if (sccsr14sc.Form1.someform.Handle != IntPtr.Zero)
                        {

                            inputsim = new InputSimulator();
                            mousesim = new MouseSimulator(inputsim);
                            keyboardsim = new KeyboardSimulator(inputsim);

                            keynmouseinput = new DInput();
                            keynmouseinput.Initialize(MainWindow.config, sccsr14sc.Form1.someform.Handle); //sccsr14sc.Form1.someform.SCGLOBALSACCESSORS.SCCONSOLECORE.handle

                            //sccsr14sc.Form1.someform.haspressedf9 = 2;
                            initinputs = 1;
                        }
                    }
                });
                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
            }
            else
            {
                keynmouseinput.Frame();
            }*/


            /*
            if (keynmouseinput != null)
            {
                keynmouseinput.Frame();
            }

        


            if (cammovementswtc == 0)
            {
                cammovementwatch.Stop();
                cammovementwatch.Reset();
                cammovementwatch.Restart();
                cammovementswtc = 1;
            }


            if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad0))
            {
                if (cammovementwatch.Elapsed.Milliseconds >= 10)
                {
                    if (canmovecamera == 0) 
                    {
                        canmovecamera = 1;
                    }
                    else if (canmovecamera == 1)
                    {
                        canmovecamera = 0;
                    }
                    cammovementswtc = 0;
                }
            }

            if (canpressfmenukeyswtc == 0)
            {
                canpressfmenukeystopwatch.Stop();
                canpressfmenukeystopwatch.Reset();
                canpressfmenukeystopwatch.Restart();
                canpressfmenukeyswtc = 1;
            }
            */





            /*
            if (canpressfmenukeystopwatch.Elapsed.Milliseconds >= 100 && canpressfmenukeyswtc == 1)
            {
                if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.F9))
                {
                    if (sccsr14sc.Form1.thepanel.Visible == false)
                    {
                        if (sccsr14sc.Form1.someform.haspressedf9 == 1)
                        {
                            sccsr14sc.Form1.someform.haspressedf9 = 0;
                            sccsr14sc.Form1.someform.haspressedsomekeyboardkey = 1;
                        }
                    }
                    else if (sccsr14sc.Form1.thepanel.Visible == true)
                    {
                        if (sccsr14sc.Form1.someform.haspressedf9 == 0)
                        {
                            sccsr14sc.Form1.someform.haspressedf9 = 1;
                            sccsr14sc.Form1.someform.haspressedsomekeyboardkey = 1;                
                        }
                    }

                    //else if (sccsr14sc.Form1.someform.haspressedf9 == 2)
                    //{
                    //    canpressfmenukeyswtc = 0;
                    //}

                    canpressfmenukeystopwatch.Stop();
                    canpressfmenukeyswtc = 0;
                }
            }*/





            


            /*
            if (resetvaluesswtc == 0)
            {
                resetvaluesstopwatch.Stop();
                resetvaluesstopwatch.Reset();
                resetvaluesstopwatch.Restart();
                resetvaluesswtc = 1;
            }


            if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPadEnter))
            {
                if (resetvaluesstopwatch.Elapsed.Milliseconds >= 20)
                {
                    OFFSETPOS = Vector3.Zero;
                    movePos = Vector3.Zero;// new Vector3(0, 0, 0);
                    rotx = 0;
                    roty = 0;
                    rotz = 0;
                    Camera.SetPosition(originPos.X,originPos.Y,originPos.Z);
                    Camera.SetRotation(0,0,0);
                    resetvaluesswtc = 0;
                }
            }





            if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad1))
            {
                if (haspressedheightmapvaluedecrease == 0)
                {

                    haspressedheightmapvaluedecrease = 1;
                }
            }

            if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad3))
            {
                if (haspressedheightmapvalueincrease == 0)
                {
                    haspressedheightmapvalueincrease = 1;
                }
            }



            if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad7))
            {
                if (resetvaluesstopwatch.Elapsed.Milliseconds >= 10)
                {
                    if (savedposrotswtcoptionsaveorselect == 2)
                    {
                        savedposition7 = OFFSETPOS;
                        savedrotation7 = Camera.GetRotation();
                        savedposrotswtcoptionsaveorselect = 0;
                    }
                    else if (savedposrotswtcoptionsaveorselect == 1)
                    {
                        Camera.SetPosition(savedposition7.X, savedposition7.Y, savedposition7.Z);
                        Camera.SetRotation(savedrotation7.X, savedrotation7.Y, savedrotation7.Z);
                        //OFFSETPOS = savedposition7;
                        rotx = savedrotation7.X;
                        roty = savedrotation7.Y;
                        rotz = savedrotation7.Z;
                        movePos = savedposition7;
                        savedposrotswtcoptionsaveorselect = 0;
                    }

                    resetvaluesswtc = 0;
                }
            }



            if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad8))
            {
                if (resetvaluesstopwatch.Elapsed.Milliseconds >= 10)
                {
                    if (savedposrotswtcoptionsaveorselect == 2)
                    {
                        savedposition8 = OFFSETPOS;
                        savedrotation8 = Camera.GetRotation();
                        savedposrotswtcoptionsaveorselect = 0;
                    }
                    else if (savedposrotswtcoptionsaveorselect == 1)
                    {
                        Camera.SetPosition(savedposition8.X, savedposition8.Y, savedposition8.Z);
                        Camera.SetRotation(savedrotation8.X, savedrotation8.Y, savedrotation8.Z);
                        //OFFSETPOS = savedposition8;
                        rotx = savedrotation8.X;
                        roty = savedrotation8.Y;
                        rotz = savedrotation8.Z;
                        movePos = savedposition8;// Vector3.Zero;
                        savedposrotswtcoptionsaveorselect = 0;
                    }

                    resetvaluesswtc = 0;
                }
            }


            if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad9))
            {
                if (resetvaluesstopwatch.Elapsed.Milliseconds >= 10)
                {
                    if (savedposrotswtcoptionsaveorselect == 2)
                    {
                        savedposition9 = OFFSETPOS;
                        savedrotation9 = Camera.GetRotation();
                        savedposrotswtcoptionsaveorselect = 0;
                    }
                    else if (savedposrotswtcoptionsaveorselect == 1)
                    {
                        Camera.SetPosition(savedposition9.X, savedposition9.Y, savedposition9.Z);
                        Camera.SetRotation(savedrotation9.X, savedrotation9.Y, savedrotation9.Z);
                        //OFFSETPOS = savedposition9;
                        rotx = savedrotation9.X;
                        roty = savedrotation9.Y;
                        rotz = savedrotation9.Z;
                        movePos = savedposition9;// Vector3.Zero;
                        savedposrotswtcoptionsaveorselect = 0;
                    }

                    //OFFSETPOS = Vector3.Zero;
                    //movePos = Vector3.Zero;// new Vector3(0, 0, 0);
                    //rotx = 0;
                    //roty = 0;
                    //rotz = 0;
                    //Camera.SetPosition(originPos.X, originPos.Y, originPos.Z);
                    //Camera.SetRotation(0, 0, 0);
                    resetvaluesswtc = 0;
                }
            }


            if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad4))
            {
                if (resetvaluesstopwatch.Elapsed.Milliseconds >= 10)
                {
                    savedposrotswtcoptionsaveorselect = 2;
                    resetvaluesswtc = 0;
                }
            }

            if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad5))
            {
                if (resetvaluesstopwatch.Elapsed.Milliseconds >= 10)
                {
                    savedposrotswtcoptionsaveorselect = 1;
                    resetvaluesswtc = 0;
                }
            }
            */



            /*
            if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad4))
            {
                if (resetvaluesstopwatch.Elapsed.Milliseconds >= 20)
                {
                    savedposition4 = OFFSETPOS;
                    savedrotation4 = Camera.GetRotation();

                    //OFFSETPOS = Vector3.Zero;
                    //movePos = Vector3.Zero;// new Vector3(0, 0, 0);
                    //rotx = 0;
                    //roty = 0;
                    //rotz = 0;
                    //Camera.SetPosition(originPos.X, originPos.Y, originPos.Z);
                    //Camera.SetRotation(0, 0, 0);
                    resetvaluesswtc = 0;
                }
            }
            if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad5))
            {
                if (resetvaluesstopwatch.Elapsed.Milliseconds >= 20)
                {
                    savedposition5 = OFFSETPOS;
                    savedrotation5= Camera.GetRotation();

                    //OFFSETPOS = Vector3.Zero;
                    //movePos = Vector3.Zero;// new Vector3(0, 0, 0);
                    //rotx = 0;
                    //roty = 0;
                    //rotz = 0;
                    //Camera.SetPosition(originPos.X, originPos.Y, originPos.Z);
                    //Camera.SetRotation(0, 0, 0);
                    resetvaluesswtc = 0;
                }
            }
            if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad6))
            {
                if (resetvaluesstopwatch.Elapsed.Milliseconds >= 20)
                {
                    savedposition6 = OFFSETPOS;
                    savedrotation6 = Camera.GetRotation();

                    //OFFSETPOS = Vector3.Zero;
                    //movePos = Vector3.Zero;// new Vector3(0, 0, 0);
                    //rotx = 0;
                    //roty = 0;
                    //rotz = 0;
                    //Camera.SetPosition(originPos.X, originPos.Y, originPos.Z);
                    //Camera.SetRotation(0, 0, 0);
                    resetvaluesswtc = 0;
                }
            }*/


            /*if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad))
            {
                if (resetvaluesstopwatch.Elapsed.Milliseconds >= 20)
                {
                    savedposition6 = OFFSETPOS;
                    savedrotation6 = Camera.GetRotation();

                    //OFFSETPOS = Vector3.Zero;
                    //movePos = Vector3.Zero;// new Vector3(0, 0, 0);
                    //rotx = 0;
                    //roty = 0;
                    //rotz = 0;
                    //Camera.SetPosition(originPos.X, originPos.Y, originPos.Z);
                    //Camera.SetRotation(0, 0, 0);
                    resetvaluesswtc = 0;
                }
            }*/





            /*
            if (savedposrotswtc == 0)
            {
                savedposrotwatch.Stop();
                savedposrotwatch.Reset();
                savedposrotwatch.Restart();
                savedposrotswtc = 1;
            }


            if (savedposrotswtcoptionsaveorselect)
            {

            }*/













            /*         public int haspressedheightmapvalueincrease = 0;
         public int haspressedheightmapvaluedecrease = 0;
         public int haspressedheightmapkey = 0;
            */








            /*if (MainWindow._useOculusRift == 0)
            {
                Camera.Render();

                OFFSETPOS = originPos + movePos;

                Camera.SetPosition(OFFSETPOS.X, OFFSETPOS.Y, OFFSETPOS.Z);
            }*/




            if (MainWindow._useOculusRift == 1)
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
                Vector3 oculusRiftDir = sc_maths._getDirection(Vector3.ForwardRH, quatter);


                Matrix.RotationQuaternion(ref quatter, out hmd_matrix);

                Matrix.RotationQuaternion(ref quatter, out hmd_matrix_test);

                hmd_matrix_test = hmd_matrix_test * finalRotationMatrix;

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
                //_other_left_touch_matrix = _leftTouchMatrix;
                //_other_left_touch_matrix.M41 = handPoseLeft.Position.X;
                //_other_left_touch_matrix.M42 = handPoseLeft.Position.Y;
                //_other_left_touch_matrix.M43 = handPoseLeft.Position.Z;

                _leftTouchMatrix.M41 = handPoseLeft.Position.X + originPos.X + movePos.X;
                _leftTouchMatrix.M42 = handPoseLeft.Position.Y + originPos.Y + movePos.Y;
                _leftTouchMatrix.M43 = handPoseLeft.Position.Z + originPos.Z + movePos.Z;







                if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.A))
                {
                    roty -= speedRot;
                }
                else if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.D))
                {
                    roty += speedRot;
                }
                /*else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.R))
                {
                    rotx -= speedRot;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.F))
                {
                    rotx += speedRot;
                }*/


                /*if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.R))
                {
                    Rotationscreenx -= speedRot;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.F))
                {
                    Rotationscreenx += speedRot;
                }


                float pitch = (float)(Math.PI * (Rotationscreenx) / 180.0f);
                float yaw = (float)(Math.PI * (Rotationscreeny) / 180.0f);
                float roll = (float)(Math.PI * (Rotationscreenz) / 180.0f);

                var rotMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                //scgraphics.scgraphicssec.somevoxelvirtualdesktopglobals

                if (scgraphicssecpackagemessage.scgraphicssec != null)
                {
                    if (scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop != null)
                    {
                        if (scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[0] != null)
                        {
                            /*for (int i = 0;i < scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[0].arrayofindexzeromesh.Length;i++)
                            {
                                for (int j = 0; j < scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[0].arrayofindexzeromesh[i].arrayofzeromeshinstances.Length;j++)
                                {
                                    //scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[0].arrayofindexzeromesh[i].arrayofzeromeshinstances[j].current_pos = rotMatrix;
                                    Matrix somemat = scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[0].arrayofindexzeromesh[i].arrayofzeromeshinstances[j].current_pos;

                                    Matrix resmat;
                                    Matrix.Multiply(ref somemat, ref rotMatrix,out resmat);

                                    //scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[0].worldmatofobj = rotMatrix;
                                    scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[0].arrayofindexzeromesh[i].arrayofzeromeshinstances[j].current_pos = resmat;
                                }
                            }
                            //scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[0].worldmatofobj = rotMatrix;
                            //Console.WriteLine("test");

                        }
                    }
                }*/



           

                //Matrix tempmat = rotatingMatrixForPelvis;
                Matrix tempmat = hmd_matrix * rotatingMatrixForPelvis * hmdmatrixRot;
                Quaternion otherQuat;
                Quaternion.RotationMatrix(ref tempmat, out otherQuat);

                Vector3 direction_feet_forward;
                Vector3 direction_feet_right;
                Vector3 direction_feet_up;

                direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);



                if (keynmouseinput._KeyboardState  != null && keynmouseinput._KeyboardState .PressedKeys.Contains(Key.Up))
                {
                    //direction_feet_forward.Z += speed * speedPos;
                    movePos += direction_feet_forward * speedPos * speedPos;
                }
                else if (keynmouseinput._KeyboardState  != null && keynmouseinput._KeyboardState .PressedKeys.Contains(Key.Down))
                {
                    movePos -= direction_feet_forward * speedPos * speedPos;
                    //direction_feet_forward.Z -= speed * speedPos;
                }
                else if (keynmouseinput._KeyboardState  != null && keynmouseinput._KeyboardState .PressedKeys.Contains(Key.Q))
                {
                    movePos += direction_feet_up * speedPos * speedPos;
                    //direction_feet_forward.Y += speed * speedPos;
                }
                else if (keynmouseinput._KeyboardState  != null && keynmouseinput._KeyboardState .PressedKeys.Contains(Key.Z))
                {
                    movePos -= direction_feet_up * speedPos * speedPos;
                    //direction_feet_forward.Y -= speed * speedPos;
                }
                else if (keynmouseinput._KeyboardState  != null && keynmouseinput._KeyboardState .PressedKeys.Contains(Key.Left))
                {
                    movePos -= direction_feet_right * speedPos * speedPos;
                    //direction_feet_forward.X -= speed * speedPos;
                }
                else if (keynmouseinput._KeyboardState  != null && keynmouseinput._KeyboardState .PressedKeys.Contains(Key.Right))
                {
                    movePos += direction_feet_right * speedPos * speedPos;
                    //direction_feet_forward.X += speed * speedPos;
                }



                OFFSETPOS = originPos + movePos + _hmdPoser;


            }
            else if (MainWindow._useOculusRift == 0 && keynmouseinput != null)
            {





                Camera.Render();
                //keynmouseinput.Frame();

                //float speed = 0.015f;
                //float speedRot = 0.1f;

                if (canmovecamera == 1)
                {
                    if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.A))
                    {
                        roty -= speedRot;
                    }
                    else if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.D))
                    {
                        roty += speedRot;
                    }
                    else if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.R))
                    {
                        rotx -= speedRot;
                    }
                    else if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.F))
                    {
                        rotx += speedRot;
                    }

                    var somerot = Camera.GetRotation();
                    Camera.SetRotation(rotx, roty, somerot.Z);
                }






                /*
                if (keynmouseinput._KeyboardState  != null && keynmouseinput._KeyboardState .PressedKeys.Contains(Key.R))
                {
                    Rotationscreenx -= speedRot;
                }
                else if (keynmouseinput._KeyboardState  != null && keynmouseinput._KeyboardState .PressedKeys.Contains(Key.F))
                {
                    Rotationscreenx += speedRot;
                }

                
                float pitch = (float)(Math.PI * (Rotationscreenx) / 180.0f);
                float yaw = (float)(Math.PI * (Rotationscreeny) / 180.0f);
                float roll = (float)(Math.PI * (Rotationscreenz) / 180.0f);

                var rotMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                //scgraphics.scgraphicssec.somevoxelvirtualdesktopglobals

                if (scgraphicssecpackagemessage.scgraphicssec!= null)
                {
                    if (scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop != null)
                    {
                        if (scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[0] != null)
                        {
                            /*for (int i = 0;i < scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[0].arrayofindexzeromesh.Length;i++)
                            {
                                for (int j = 0; j < scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[0].arrayofindexzeromesh[i].arrayofzeromeshinstances.Length;j++)
                                {
                                    //scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[0].arrayofindexzeromesh[i].arrayofzeromeshinstances[j].current_pos = rotMatrix;
                                    Matrix somemat = scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[0].arrayofindexzeromesh[i].arrayofzeromeshinstances[j].current_pos;

                                    Matrix resmat;
                                    Matrix.Multiply(ref somemat, ref rotMatrix,out resmat);

                                    //scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[0].worldmatofobj = rotMatrix;
                                    scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[0].arrayofindexzeromesh[i].arrayofzeromeshinstances[j].current_pos = resmat;
                                }
                            }
                            //scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[0].worldmatofobj = rotMatrix;
                            //Console.WriteLine("test");

                        }
                    }
                }
                */






                if (canmovecamera == 1)
                {
                    Matrix tempmat = Camera.rotationMatrix;
                    Quaternion otherQuat;
                    Quaternion.RotationMatrix(ref tempmat, out otherQuat);





                    Vector3 direction_feet_forward;
                    Vector3 direction_feet_right;
                    Vector3 direction_feet_up;

                    direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                    direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);



                    if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Up))
                    {
                        //MainWindow.MessageBox((IntPtr)0, "000", "sc core systems message", 0);
                        //direction_feet_forward.Z += speed * speedPos;
                        movePos -= direction_feet_forward * speedPos * speedPos;
                    }
                    else if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Down))
                    {
                        movePos += direction_feet_forward * speedPos * speedPos;
                        //direction_feet_forward.Z -= speed * speedPos;
                    }
                    else if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Q))
                    {
                        movePos += direction_feet_up * speedPos * speedPos;
                        //direction_feet_forward.Y += speed * speedPos;
                    }
                    else if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Z))
                    {
                        movePos -= direction_feet_up * speedPos * speedPos;
                        //direction_feet_forward.Y -= speed * speedPos;
                    }
                    else if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Left))
                    {
                        movePos -= direction_feet_right * speedPos * speedPos;
                        //direction_feet_forward.X -= speed * speedPos;
                    }
                    else if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Right))
                    {
                        movePos += direction_feet_right * speedPos * speedPos;
                        //direction_feet_forward.X += speed * speedPos;
                    }
                    else if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Add))
                    {

                        speedPos += 0.1f;
                        //direction_feet_forward.X -= speed * speedPos;
                    }
                    else if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Subtract))
                    {
                        if (speedPos > 0)
                        {
                            speedPos -= 0.1f;
                        }

                        //direction_feet_forward.X += speed * speedPos;
                    }



                    OFFSETPOS = originPos + movePos;

                    Camera.SetPosition(OFFSETPOS.X, OFFSETPOS.Y, OFFSETPOS.Z);
                }
            }









            /*
            if (MainWindow._useOculusRift == 1)
            {

                //MainWindow.MessageBox((IntPtr)0, "000", "sc core systems message", 0);
                try
                {
                    if (canworkphysics == 1)
                    {
                        if (MainWindow.useArduinoOVRTouchKeymapper == 0)
                        {
                            //if (_out_of_bounds_oculus_rift == 1)
                            {
                                if (thumbStickRight[1].X < 0 || thumbStickRight[1].X > 0 || thumbStickRight[1].Y < 0 || thumbStickRight[1].Y > 0)
                                {
                                    if (thumbStickRight[1].X < 0 && thumbStickRight[1].Y < 0 || thumbStickRight[1].X < 0 && thumbStickRight[1].Y > 0)
                                    {
                                        RotationGrabbedYOff = 0;
                                        RotationGrabbedXOff = 0;
                                        RotationGrabbedZOff = 0;

                                        RotationGrabbedSwtch = 1;

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
                                            RotationY4PelvisTwo += speedRot * 10;
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
                                        RotationGrabbedYOff = 0;
                                        RotationGrabbedXOff = 0;
                                        RotationGrabbedZOff = 0;


                                        RotationGrabbedSwtch = 1;

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
                                            RotationY4PelvisTwo -= speedRot * 10;
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

                                    //RotationGrabbedY = RotationY4Pelvis;
                                    //RotationGrabbedX = RotationX4Pelvis;
                                    //RotationGrabbedZ = RotationZ4Pelvis;

                                    RotationGrabbedYOff = RotationY4Pelvis;
                                    RotationGrabbedXOff = RotationX4Pelvis;
                                    RotationGrabbedZOff = RotationZ4Pelvis;

                                    if (RotationGrabbedSwtch == 1)
                                    {
                                        RotationX4PelvisTwo = 0;
                                        RotationY4PelvisTwo = 0;
                                        RotationZ4PelvisTwo = 0;
                                        RotationGrabbedSwtch = 0;
                                    }

                                    //RotationGrabbedY = 0;
                                    //RotationGrabbedX = 0;
                                    //RotationGrabbedZ = 0;

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




                            //Vector3 resulter;
                            //Vector3.TransformCoordinate(ref _hmdPoser, ref WorldMatrix, out resulter);
                            //var someMatrix = hmd_matrix * finalRotationMatrix;


                            //OFFSETPOS.Y += _hmdPoser.Y;
                        }








                        Matrix tempmat = hmd_matrix * rotatingMatrixForPelvis * hmdmatrixRot;



                        Quaternion otherQuat;
                        Quaternion.RotationMatrix(ref tempmat, out otherQuat);

                        Vector3 direction_feet_forward;
                        Vector3 direction_feet_right;
                        Vector3 direction_feet_up;

                        direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                        direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                        direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);

                        if (thumbStickLeft[0].X > 0.5f)
                        {
                            movePos += direction_feet_right * speedPos * thumbStickLeft[0].X;
                        }
                        else if (thumbStickLeft[0].X < -0.5f)
                        {
                            movePos -= direction_feet_right * speedPos * -thumbStickLeft[0].X;
                        }

                        if (thumbStickLeft[0].Y > 0.5f)
                        {
                            movePos += direction_feet_forward * speedPos * thumbStickLeft[0].Y;
                        }
                        else if (thumbStickLeft[0].Y < -0.5f)
                        {
                            movePos -= direction_feet_forward * speedPos * -thumbStickLeft[0].Y;
                        }


                        OFFSETPOS = originPos + movePos;// + _hmdPoser; //_hmdPoser
                    }
                }
                catch (Exception ex)
                {
                    MainWindow.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                }

            }*/














            
            /*
            if (MainWindow._useOculusRift == 1)
            {
                if (scgraphicssecpackagemessage.scjittertasks != null)
                {
                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                    {
                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1 && scgraphicssecpackagemessage.scgraphicssec.createikrig == 1)
                        {
                            scgraphicssecpackagemessage.scgraphicssec.oculuscontrolsNRecordSoundNMousePointer();
                        }
                    }
                }
            }
            */










            if (MainWindow._useOculusRift == 0 && canworkphysics == 1)
            {



                Matrix tempmat = Camera.rotationMatrix;
                Quaternion quatt;
                Quaternion.RotationMatrix(ref tempmat, out quatt);


                if (MainWindow.usethirdpersonview == 0)
                {

                    //FOR THE VERTEX SHADER
                    Quaternion somedirquat1;
                    Quaternion.RotationMatrix(ref tempmat, out somedirquat1);
                    dirikvoxelbodyInstanceRight0 = new Vector4(-sc_maths._newgetdirleft(somedirquat1), 0);
                    dirikvoxelbodyInstanceUp0 = new Vector4(sc_maths._newgetdirup(somedirquat1), 0);
                    dirikvoxelbodyInstanceForward0 = new Vector4(sc_maths._newgetdirforward(somedirquat1), 0);
                }
                else if (MainWindow.usethirdpersonview == 1)
                {
                    Quaternion somedirquat1;
                    Quaternion.RotationMatrix(ref tempmat, out somedirquat1);
                    dirikvoxelbodyInstanceRight0 = new Vector4(-sc_maths._newgetdirleft(somedirquat1), 0);
                    dirikvoxelbodyInstanceUp0 = new Vector4(sc_maths._newgetdirup(somedirquat1), 0);
                    dirikvoxelbodyInstanceForward0 = new Vector4(sc_maths._newgetdirforward(somedirquat1), 0);
                }






                scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;




                if (MainWindow.usesharpdxscreencapture == 1)
                {
                    if (sharpdxscreencapture != null)
                    {
                        // MainWindow.MessageBox((IntPtr)0, "test0", "sccsmsg", 0);



                        for (int i = 0; i < 1;)
                        {
                            screencaptureresultswtc = 0;
                            try
                            {
                                screencaptureframe = sharpdxscreencapture.ScreenCapture(3);




                                if (scgraphicssecpackagemessage.scjittertasks != null)
                                {
                                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                    {
                                        if (scgraphicssecpackagemessage.scjittertasks[0].Length > 0)
                                        {
                                            scgraphicssecpackagemessage.scjittertasks[0][0].frameByteArray = screencaptureframe.frameByteArray;
                                            scgraphicssecpackagemessage.scjittertasks[0][0].shaderresource = screencaptureframe.ShaderResource;

                                            //MainWindow.MessageBox((IntPtr)0, "test2", "sccsmsg", 0);
                                        }
                                    }
                                }


                            }
                            catch (Exception ex)
                            {
                                sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, device);
                                screencaptureresultswtc = 1;
                            }
                            i++;
                            if (screencaptureresultswtc == 0)
                            {
                                break;
                            }
                        }

                        if (sharpdxscreencapture.hasinit == 2)
                        {

                            //MainWindow.MessageBox((IntPtr)0, "test1", "sccsmsg", 0);


                        }
                    }
                }



                // Clear views
                D3D.DeviceContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);
                D3D.DeviceContext.ClearRenderTargetView(_renderTargetView, SharpDX.Color.Black); //LightGray //Black
                                                                                                     //D3D.DeviceContext.clea

                
                float ratio = (float)SurfaceWidth / (float)SurfaceHeight;
                //_projectionMatrix = Matrix.PerspectiveFovLH(3.14F / 3.0F, ratio, 0.001f, 1000);
                _projectionMatrix = Matrix.PerspectiveFovLH((float)(Math.PI / 4), ((float)SurfaceWidth / (float)SurfaceHeight), 0.001f, 1000);
                



                /*
                //WORKING
                float somescreensizew = 1920; // 240 * 8 * 0.0005f
                float somescreensizeh = 1080; // 135 * 8 * 0.0005f
                Matrix.OrthoOffCenterLH(0, somescreensizew, 0, somescreensizeh, 0.001f, 1000, out _projectionMatrix);
                ///WORKING
                */


                //WORKING
                /*float somescreensizew = 240 * 8 * 0.0005f; // 240 * 8 * 0.0005f
                float somescreensizeh = 135 * 8 * 0.0005f; // 135 * 8 * 0.0005f

                float somescreensizewh = somescreensizew * 0.5f; // 240 * 8 * 0.0005f
                float somescreensizehh = somescreensizeh * 0.5f; // 135 * 8 * 0.0005f
                Matrix.OrthoOffCenterLH(0, somescreensizew - somescreensizewh, 0, somescreensizeh - somescreensizehh, 0.01f, 1000, out _projectionMatrix);
                ///WORKING
                */

                //Matrix.OrthoLH(1920, 1080, 0.001f, 1000, out _projectionMatrix);




                /*
                if (MainWindow._useOculusRift == 0)
                {
                    if (scgraphicssecpackagemessage.scjittertasks != null)
                    {
                        if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                        {
                            if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                            {
                                scgraphicssecpackagemessage.scgraphicssec.oculuscontrolsNRecordSoundNMousePointer(_projectionMatrix, tempmat);
                                scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswritescreenassetstobuffer(scgraphicssecpackagemessage.scjittertasks);
                                
                            }
                        }
                    }
                }*/


                //NOT WORKING
                //float ratio = (float)SurfaceWidth / (float)SurfaceHeight;
                /// _projectionMatrix = Matrix.PerspectiveFovLH(3.14F / 3.0F, ratio, 0.001f, 1000);
                //Matrix.PerspectiveOffCenterLH(0, (float)SurfaceWidth, 0, (float)SurfaceHeight, 0.001f, 1000, out _projectionMatrix);
                //NOT WORKING


            

                //Matrix.PerspectiveOffCenterRH(0, (float)SurfaceWidth, 0, (float)SurfaceHeight, 0.001f, 1000, out _projectionMatrix);

                //viewMatrix = Matrix.LookAtLH(new Vector3(0, 3, 10), new Vector3(), Vector3.UnitY);
                viewMatrix = Camera.ViewMatrix;
                //Matrix.PerspectiveOffCenterRH(100, (float)SurfaceWidth, 100, (float)SurfaceHeight, 0.001f, 1000, out _projectionMatrix);

                /*float somescreensizew = 240 * 8 * 0.005f;
                float somescreensizeh = 135 * 8 * 0.005f;

                Matrix.OrthoOffCenterLH(0, somescreensizew, 0, somescreensizeh, 0.001f, 1000, out _projectionMatrix);





                //Matrix.OrthoOffCenterLH(0, SurfaceWidth/10, 0, SurfaceHeight/14, 0.001f, 1000, out _projectionMatrix);




                //Matrix.OrthoOffCenterLH(0, (float)MainWindow.form.Size.Width, 0, (float)MainWindow.form.Size.Height, 0.001f, 1000, out _projectionMatrix);
                //Matrix.OrthoOffCenterLH(0, (float)MainWindow.form.Size.Width, 0, (float)MainWindow.form.Size.Height, 0.001f, 1000, out _projectionMatrix);



                //Matrix.PerspectiveLH((float)SurfaceWidth, (float)SurfaceHeight, 0.01f, 10, out _projectionMatrix);


                //ProjectionMatrix = Matrix.PerspectiveFovLH((float)(Math.PI / 4), ((float)configuration.Width / (float)configuration.Height), DSystemConfiguration.ScreenNear, DSystemConfiguration.ScreenDepth);
                //WorldMatrix = Matrix.Identity;
                //_projectionMatrix = Matrix.OrthoLH((float)SurfaceWidth, (float)SurfaceHeight, 0.1f, 10);
                */



                //MainWindow.MessageBox((IntPtr)0, "0", "sc core systems message", 0);
                if (canworkphysics == 1)
                {
                    var somerotvec = Camera.GetRotation();
                    float pitch = somerotvec.X * 0.0174532925f;
                    float yaw = somerotvec.Y * 0.0174532925f; ;
                    float roll = somerotvec.Z * 0.0174532925f; ;
                    Matrix somerotmat = Camera.rotationMatrix;// Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                    scgraphicssecpackagemessage.viewMatrix = viewMatrix;
                    scgraphicssecpackagemessage.projectionMatrix = _projectionMatrix;
                    scgraphicssecpackagemessage.originRot = originRot;
                    scgraphicssecpackagemessage.rotatingMatrix = somerotmat; //rotatingMatrix
                    scgraphicssecpackagemessage.hmdmatrixRot = hmdmatrixRot;
                    scgraphicssecpackagemessage.hmd_matrix = hmd_matrix;
                    scgraphicssecpackagemessage.rotatingMatrixForPelvis = rotatingMatrixForPelvis;
                    scgraphicssecpackagemessage.rightTouchMatrix = _rightTouchMatrix;
                    scgraphicssecpackagemessage.leftTouchMatrix = _leftTouchMatrix;
                    scgraphicssecpackagemessage.oriProjectionMatrix = oriProjectionMatrix;
                    scgraphicssecpackagemessage.someextrapelvismatrix = rotatingMatrixForPelvis;
                    scgraphicssecpackagemessage.offsetpos = Camera.GetPosition();
                    scgraphicssecpackagemessage.handPoseRight = handPoseRight;
                    scgraphicssecpackagemessage.handPoseLeft = handPoseLeft;
                    //scgraphicssecpackagemessage.scgraphicssec = null;
                    //scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;






                    if (updatethreadupdateswtc1 == 0)
                    {

                        scgraphicssecpackagemessage.scgraphicssec = new sccs.scgraphics.scgraphicssec();
                        scgraphicssecpackagemessage.scgraphicssec._sc_create_world_objects(scgraphicssecpackagemessage.scjittertasks);
                        
                        
                        
                        
                        
                        
                        main_thread_update = new Thread(() =>
                        {
                            int threadupdateswtc = 0;

                        _thread_looper:
                            try
                            {

                                //_ticks_watch.Stop();
                                //_ticks_watch.Restart();


                                if (threadupdateswtc == 0) //0
                                {
                                    if (scgraphicssecpackagemessage.scjittertasks != null)
                                    {
                                        if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                        {
                                            if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                                            {
                                                //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                                //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                                // scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritedirectiontobuffer(scgraphicssecpackagemessage.scjittertasks);



                                                scgraphicssecpackagemessage.scgraphicssec.oculuscontrolsNRecordSoundNMousePointer(scgraphicssecpackagemessage.projectionMatrix, tempmat);
                                                scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswritescreenassetstobuffer(scgraphicssecpackagemessage.scjittertasks);

                                            }
                                        }
                                    }
                                }

                                //Console.WriteLine(_ticks_watch.Elapsed.Milliseconds);


                            }
                            catch (Exception ex)
                            {
                                MainWindow.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                            }

                            Thread.Sleep(1);
                            goto _thread_looper;

                            //ShutDown();
                            //ShutDownGraphics();

                        }, 0);

                        main_thread_update.IsBackground = true;
                        main_thread_update.Priority = ThreadPriority.Lowest; //AboveNormal
                        main_thread_update.SetApartmentState(ApartmentState.STA);
                        main_thread_update.Start();



                        


                        main_thread_update = new Thread(() =>
                        {
                            int threadupdateswtc = 0;

                        _thread_looper:
                            try
                            {

                                //_ticks_watch.Stop();
                                //_ticks_watch.Restart();


                                if (threadupdateswtc == 0) //0
                                {
                                    if (scgraphicssecpackagemessage.scjittertasks != null)
                                    {
                                        if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                        {
                                            if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                                            {
                                                //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                                //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                                // scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritedirectiontobuffer(scgraphicssecpackagemessage.scjittertasks);
                                                scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);
                                                //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workOnDestroyingBytes(scgraphicssecpackagemessage);
                                            }
                                        }
                                    }
                                }

                                //Console.WriteLine(_ticks_watch.Elapsed.Milliseconds);


                            }
                            catch (Exception ex)
                            {
                                MainWindow.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                            }

                            Thread.Sleep(1);
                            goto _thread_looper;

                            //ShutDown();
                            //ShutDownGraphics();

                        }, 0);

                        main_thread_update.IsBackground = true;
                        main_thread_update.Priority = ThreadPriority.Lowest; //AboveNormal
                        main_thread_update.SetApartmentState(ApartmentState.STA);
                        main_thread_update.Start();
                        

                        updatethreadupdateswtc1 = 1;
                    }
                }
            }
            /*
            if (threads[0] != null)
            {
                if (!threads[0].IsAlive)
                {
                    MainWindow.MessageBox((IntPtr)0, "THREAD QUIT1", "sc core systems message", 0);

                }
            }*/




            //someintegerlockingstruct[0].somereseteventt01.Set();


            if (MainWindow._useOculusRift == 0 && canworkphysics == 1)
            {


                //scgraphicssecpackagemessage.viewMatrix = viewMatrix;
                //scgraphicssecpackagemessage.projectionMatrix = _projectionMatrix;




                if (scgraphicssecpackagemessage.scgraphicssec != null)
                {

                    if (scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                    {
                        if (scgraphicssecpackagemessage.scjittertasks != null)
                        {
                            if (scgraphicssecpackagemessage.scjittertasks.Length > 0)
                            {
                                if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                {
                                    //if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                    {
                                        //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);

                                        //scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);

                                        //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                        //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                        scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);
                                        //_sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;

                                    }
                                }
                            }
                        }
                    }
                }


                D3D.SwapChain.Present(1, PresentFlags.None);

                //someintegerlockingstruct[0].somereseteventt00.Reset();
                //someintegerlockingstruct[0].somereseteventt00.Set();
                //someintegerlockingstruct[0].somereseteventt00.Set();
                //someintegerlockingstruct[0].somereseteventt00.Reset();                
                //someintegerlockingstruct[0].somereseteventt01.Reset();
                //someintegerlockingstruct[0].somereseteventt00.Reset();

            }
            else if (MainWindow._useOculusRift == 1 && canworkphysics == 1)
            {

                scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;


                if (sharpdxscreencapture != null)
                {
                   // MainWindow.MessageBox((IntPtr)0, "test0", "sccsmsg", 0);



                    for (int i = 0; i < 1;)
                    {
                        screencaptureresultswtc = 0;
                        try
                        {
                            screencaptureframe = sharpdxscreencapture.ScreenCapture(3);




                            if (scgraphicssecpackagemessage.scjittertasks != null)
                            {
                                if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                {
                                    if (scgraphicssecpackagemessage.scjittertasks[0].Length > 0)
                                    {
                                        scgraphicssecpackagemessage.scjittertasks[0][0].frameByteArray = screencaptureframe.frameByteArray;
                                        scgraphicssecpackagemessage.scjittertasks[0][0].shaderresource = screencaptureframe.ShaderResource;

                                        //MainWindow.MessageBox((IntPtr)0, "test2", "sccsmsg", 0);
                                    }
                                }
                            }


                        }
                        catch (Exception ex)
                        {
                            sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, deviceforscreencap);
                            screencaptureresultswtc = 1;
                        }
                        i++;
                        if (screencaptureresultswtc == 0)
                        {
                            break;
                        }
                    }

                    if (sharpdxscreencapture.hasinit == 2)
                    {

                        //MainWindow.MessageBox((IntPtr)0, "test1", "sccsmsg", 0);

                        
                    }
                }

                //MainWindow.MessageBox((IntPtr)0, "test0", "sccsmsg", 0);

                if (D3D != null && MainWindow.exitedprogram != 1)
                {
                    if (D3D.OVR != null && stopovr == 0)
                    {


                        /*if (canworkphysics == 1)
                        {
                        

                            if (updatethreadupdateswtc == 0)
                            {s3







                          

                                updatethreadupdateswtc = 1;
                            }
                        }*/


                        if (updatethreadupdateswtc1 == 0)
                        {


                            
                            main_thread_update = new Thread(() =>
                            {
                                int threadupdateswtc = 0;

                            _thread_looper:
                                try
                                {

                                    //_ticks_watch.Stop();
                                    //_ticks_watch.Restart();


                                    if (threadupdateswtc == 0) //0
                                    {
                                        if (scgraphicssecpackagemessage.scjittertasks != null)
                                        {
                                            if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                            {
                                                if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                                                {
                                                    //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                                    //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                                    // scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritedirectiontobuffer(scgraphicssecpackagemessage.scjittertasks);
                                                    scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);


                                                }
                                            }
                                        }
                                    }

                                    //Console.WriteLine(_ticks_watch.Elapsed.Milliseconds);


                                }
                                catch (Exception ex)
                                {
                                    MainWindow.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                                }

                                Thread.Sleep(0);
                                goto _thread_looper;

                                //ShutDown();
                                //ShutDownGraphics();

                            }, 0);

                            main_thread_update.IsBackground = true;
                            main_thread_update.Priority = ThreadPriority.Lowest; //AboveNormal
                            main_thread_update.SetApartmentState(ApartmentState.STA);
                            main_thread_update.Start();

                            updatethreadupdateswtc1 = 1;
                        }




                        Vector3f[] hmdToEyeViewOffsets = { D3D.eyeTextures[0].HmdToEyeViewOffset, D3D.eyeTextures[1].HmdToEyeViewOffset };
                        displayMidpoint = D3D.OVR.GetPredictedDisplayTime(D3D.sessionPtr, 0);
                        trackingState = D3D.OVR.GetTrackingState(D3D.sessionPtr, displayMidpoint, true);
                        eyePoses = new Posef[2];
                        D3D.OVR.CalcEyePoses(trackingState.HeadPose.ThePose, hmdToEyeViewOffsets, ref eyePoses);

                        //float timeSinceStart = (float)(DateTime.Now - startTime).TotalSeconds;

                        for (int eyeIndex = 0; eyeIndex < 2; eyeIndex++) // 2
                        {
                            Matrix someextrapelvismatrix = rotatingMatrixForPelvis; //originRot



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
                            // Update the render description at each frame, as the HmdToEyeOffset can change at runtime.
                            eyeTexture.RenderDescription = OVR.GetRenderDesc(sessionPtr, eye, hmdDesc.DefaultEyeFov[eyeIndex]);

                            // Retrieve the index of the active texture
                            int textureIndex;
                            D3D.result = eyeTexture.SwapTextureSet.GetCurrentIndex(out textureIndex);
                            D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to retrieve texture swap chain current index.");

                            D3D.device.ImmediateContext.OutputMerger.SetRenderTargets(eyeTexture.DepthStencilView, eyeTexture.RenderTargetViews[textureIndex]);
                            D3D.device.ImmediateContext.ClearRenderTargetView(eyeTexture.RenderTargetViews[textureIndex], SharpDX.Color.Black); //DimGray Black //LightGray
                            D3D.device.ImmediateContext.ClearDepthStencilView(eyeTexture.DepthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);
                            D3D.device.ImmediateContext.Rasterizer.SetViewport(eyeTexture.Viewport);


                            //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                            //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                            //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                            SharpDX.Matrix eyeQuaternionMatrix = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X, eyePoses[eyeIndex].Orientation.Y, eyePoses[eyeIndex].Orientation.Z, eyePoses[eyeIndex].Orientation.W));
                            SharpDX.Vector3 eyePos = SharpDX.Vector3.Transform(new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z), originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot).ToVector3();
                            //finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix;
                            finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot;
                            lookUp = Vector3.Transform(new Vector3(0, 1, 0), finalRotationMatrix).ToVector3();
                            lookAt = Vector3.Transform(new Vector3(0, 0, -1), finalRotationMatrix).ToVector3();
                            viewpositionorigin = eyePos;
                            viewPosition = eyePos + OFFSETPOS; // 
                            tempmatter = hmd_matrix * rotatingMatrixForPelvis * hmdmatrixRot;

                            Quaternion quatt;
                            Quaternion.RotationMatrix(ref tempmatter, out quatt);

                            if (MainWindow.usethirdpersonview == 0)
                            {

                                //FOR THE VERTEX SHADER
                                Quaternion somedirquat1;
                                Quaternion.RotationMatrix(ref tempmatter, out somedirquat1);
                                dirikvoxelbodyInstanceRight0 = new Vector4(-sc_maths._newgetdirleft(somedirquat1), 0);
                                dirikvoxelbodyInstanceUp0 = new Vector4(sc_maths._newgetdirup(somedirquat1), 0);
                                dirikvoxelbodyInstanceForward0 = new Vector4(sc_maths._newgetdirforward(somedirquat1), 0);
                            }
                            else if (MainWindow.usethirdpersonview == 1)
                            {
                                Quaternion somedirquat1;
                                Quaternion.RotationMatrix(ref tempmatter, out somedirquat1);
                                dirikvoxelbodyInstanceRight0 = new Vector4(-sc_maths._newgetdirleft(somedirquat1), 0);
                                dirikvoxelbodyInstanceUp0 = new Vector4(sc_maths._newgetdirup(somedirquat1), 0);
                                dirikvoxelbodyInstanceForward0 = new Vector4(sc_maths._newgetdirforward(somedirquat1), 0);

                                viewPosition = viewPosition + (new Vector3(dirikvoxelbodyInstanceForward0.X, dirikvoxelbodyInstanceForward0.Y, dirikvoxelbodyInstanceForward0.Z) * MainWindow.offsetthirdpersonview);
                            }





                            viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);
                            _projectionMatrix = D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.001f, 1000.0f, ProjectionModifier.None).ToMatrix();
                            oriProjectionMatrix = _projectionMatrix;
                            _projectionMatrix.Transpose();


                            //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                            //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                            //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.




                            //MainWindow.MessageBox((IntPtr)0, "0", "sc core systems message", 0);
                            if (canworkphysics == 1)
                            {
                                scgraphicssecpackagemessage.viewMatrix = viewMatrix;
                                scgraphicssecpackagemessage.projectionMatrix = _projectionMatrix;
                                scgraphicssecpackagemessage.originRot = originRot;
                                scgraphicssecpackagemessage.rotatingMatrix = rotatingMatrix;
                                scgraphicssecpackagemessage.hmdmatrixRot = hmdmatrixRot;
                                scgraphicssecpackagemessage.hmd_matrix = hmd_matrix;
                                scgraphicssecpackagemessage.rotatingMatrixForPelvis = rotatingMatrixForPelvis;
                                scgraphicssecpackagemessage.rightTouchMatrix = _rightTouchMatrix;
                                scgraphicssecpackagemessage.leftTouchMatrix = _leftTouchMatrix;
                                scgraphicssecpackagemessage.oriProjectionMatrix = oriProjectionMatrix;
                                scgraphicssecpackagemessage.someextrapelvismatrix = someextrapelvismatrix;
                                scgraphicssecpackagemessage.offsetpos = OFFSETPOS;
                                scgraphicssecpackagemessage.handPoseRight = handPoseRight;
                                scgraphicssecpackagemessage.handPoseLeft = handPoseLeft;
                                //scgraphicssecpackagemessage.scgraphicssec = null;
                                //scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;

                                
                                if (updatethreadupdateswtc0 == 0)
                                {


                                    scgraphicssecpackagemessage.scgraphicssec = new sccs.scgraphics.scgraphicssec();
                                    scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec._sc_create_world_objects(scgraphicssecpackagemessage.scjittertasks);

                                    
                                    /*
                                    main_thread_update = new Thread(() =>
                                    {



                                        //scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;
                                        //sc_graphics_sec graphicssec;

                                        int threadupdateswtc = 0;

                                    //MainWindow.MessageBox((IntPtr)0, "test", "sccsmsg", 0);

                                    _thread_looper:


                                        //scgraphicssecpackagemessage.scgraphicssec = graphicssec;

                                        //MainWindow.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                                        try
                                        {

                                            //_ticks_watch.Stop();
                                            //_ticks_watch.Restart();


                                            if (threadupdateswtc == 0) //0
                                            {



                                                if (scgraphicssecpackagemessage.scjittertasks != null)
                                                {
                                                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                                    {
                                                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                                        {
                                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                                            // scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritedirectiontobuffer(scgraphicssecpackagemessage.scjittertasks);
                                                            //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);

                                                        }
                                                    }
                                                }










                                                //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwriteinstancedVDbytestobuffer(scgraphicssecpackagemessage.scjittertasks);
                                                //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswritescreenassetstobuffer(scgraphicssecpackagemessage.scjittertasks);
                                                //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workOnDestroyingBytes(scgraphicssecpackagemessage);


                                                //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);
                                                //threadupdateswtc = 1;
                                            }

                                            //Console.WriteLine(_ticks_watch.Elapsed.Milliseconds);


                                        }
                                        catch (Exception ex)
                                        {
                                            MainWindow.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                                        }

                                        Thread.Sleep(1);
                                        goto _thread_looper;

                                        //ShutDown();
                                        //ShutDownGraphics();

                                    }, 0);

                                    main_thread_update.IsBackground = true;
                                    main_thread_update.Priority = ThreadPriority.Lowest; //AboveNormal
                                    main_thread_update.SetApartmentState(ApartmentState.STA);
                                    main_thread_update.Start();
                                    */
                                    
                                    /*
                                    heightmapthread = new Thread(() =>
                                    {



                                    //MainWindow.MessageBox((IntPtr)0, "test", "sccsmsg", 0);

                                    _thread_looper:

                                        if (threadupdateswtc == 0 && MainWindow.exitedprogram != 1) //0
                                        {
                                            //scgraphicssecpackagemessage.scgraphicssec = graphicssec;

                                            //MainWindow.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                                            try
                                            {

                                                if (scgraphicssecpackagemessage.scjittertasks != null)
                                                {
                                                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                                    {
                                                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                                        {
                                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                                            //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwriteheightmaptobuffer(scgraphicssecpackagemessage.scjittertasks);
                                                        }
                                                    }
                                                }


                                                //_ticks_watch.Stop();
                                                //_ticks_watch.Restart();

                                                //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswritescreenassetstobuffer(scgraphicssecpackagemessage.scjittertasks);
                                                //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workOnDestroyingBytes(scgraphicssecpackagemessage);


                                                //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);
                                                //threadupdateswtc = 1;


                                                //Console.WriteLine(_ticks_watch.Elapsed.Milliseconds);


                                            }
                                            catch (Exception ex)
                                            {
                                                MainWindow.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                                            }

                                            Thread.Sleep(0);
                                            goto _thread_looper;
                                        }
                                        else
                                        {
                                            Thread.Sleep(1);
                                            goto _thread_looper;
                                        }
                                        //ShutDown();
                                        //ShutDownGraphics();

                                    }, 0);

                                    heightmapthread.IsBackground = true;
                                    heightmapthread.Priority = ThreadPriority.Lowest; //AboveNormal
                                    heightmapthread.SetApartmentState(ApartmentState.STA);
                                    heightmapthread.Start();
                                    */

                                    updatethreadupdateswtc0 = 1;
                                }





                              

                                if (scgraphicssecpackagemessage.scjittertasks != null)
                                {
                                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                    {
                                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                                        {
                                            //MainWindow.MessageBox((IntPtr)0, "test", "sccsmsg", 0);
                                            //scgraphicssecpackagemessage.scjittertasks = 
                                            //scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);

                                            //scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);
                                            //scgraphicssecpackagemessage.scjittertasks= scgraphicssecpackagemessage.scgraphicssec.writetobufferagain(scgraphicssecpackagemessage.scjittertasks);

                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                            scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);
                                        }
                                    }
                                }
                            }


                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;

                            D3D.result = eyeTexture.SwapTextureSet.Commit();
                            D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to commit the swap chain texture.");
                        }


                        D3D.result = D3D.OVR.SubmitFrame(D3D.sessionPtr, 0L, IntPtr.Zero, ref D3D.layerEyeFov);

                        if (D3D != null)
                        {
                            if (D3D.OVR != null)
                            {
                                /*if (D3D.result != null && MainWindow.exitedprogram != 1)
                                {
                                    //D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to submit the frame of the current layers.");

                                }*/
                            }
                            D3D.DeviceContext.CopyResource(D3D.mirrorTextureD3D, D3D.BackBuffer);
                            D3D.SwapChain.Present(0, PresentFlags.None);
                        }
                    }
                }
            }



            if (canworkphysics != -1) //-1 to exit program safe
            {
                canworkphysics = 1;
            }

            //_can_work_physics_objects = 1;

            //MainWindow.MessageBox((IntPtr)0, "001", "sc core systems message", 0);


            return _sc_jitter_tasks;
        }


        /*
        public void Execute()
        {

            //someintegerlockingstruct[0].somereseteventt00.Reset();
            //someintegerlockingstruct[0].somereseteventt00.Set();

            //someintegerlockingstruct[0].somereseteventt00.Reset();
            //currentTaskIndex = 0;
            //waitingThreadCount = 0;

            //currentWaitHandle.Set();
            //PumpTasks();

            //while (waitingThreadCount < threads.Length - 1) Thread.Sleep(0);

            //currentWaitHandle.Reset();
            //currentWaitHandle = (currentWaitHandle == someintegerlockingstruct[0].somereseteventt00) ? someintegerlockingstruct[0].somereseteventt01 : someintegerlockingstruct[0].somereseteventt00;

            //tasks.Clear();
            //parameters.Clear();
        }*/


        /*
        private void ThreadProc(int threadindex, someintegerlock[] someintegerlockingstruct)
        {
            if (threadindex == 0)
            {
                someintegerlockingstruct[0].somereseteventt00.WaitOne();

                lock (sharpdxscreencapture)
                {
                    if (canworkphysics == 1)
                    {
                        for (int i = 0; i < 1;)
                        {
                            screencaptureresultswtc = 0;
                            try
                            {
                                screencaptureframe = sharpdxscreencapture.ScreenCapture(10);
                            }
                            catch (Exception ex)
                            {
                                sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, deviceforscreencap);
                                screencaptureresultswtc = 1;
                            }
                            i++;
                            if (screencaptureresultswtc == 0)
                            {
                                break;
                            }
                        }
                    }
                }
  
            }


            if (threadindex == 1)
            {
                someintegerlockingstruct[0].somereseteventt01.WaitOne();
                if (scgraphicssecpackagemessage.scjittertasks != null)
                {
                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                    {
                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                        {
                            //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswritescreenassetstobuffer(scgraphicssecpackagemessage.scjittertasks);

                            scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritedirectiontobuffer(scgraphicssecpackagemessage.scjittertasks);

                            scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwriteheightmaptobuffer(scgraphicssecpackagemessage.scjittertasks);

                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                        }
                    }
                }

            }
        }*/


    }
}

