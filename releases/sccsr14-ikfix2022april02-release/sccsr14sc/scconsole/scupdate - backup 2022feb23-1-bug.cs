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

using Win32.Shared;
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
using System.Windows;
//using System.Windows.Interop;

//using Win32.DwmThumbnail.Interop;
using Win32.Shared;
//using System.Windows.Controls;
//using Win32.DesktopDuplication;
using System;

using Win32.Shared;
using Win32.Shared.Interfaces;
using Win32.Shared.Interop;
using Win32;
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

namespace sccs.scgraphics
{
    public class scupdate : scdirectx
    {
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


  
        _sc_camera Camera;
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

        public static SharpDX.Vector3 originPos = new SharpDX.Vector3(0, 0, -5.5f); //new SharpDX.Vector3(-10, 1, 10);
        public static SharpDX.Vector3 originPosScreen = new SharpDX.Vector3(0, 0, 0);

        float disco_sphere_rot_speed = 0.5f;

        float speedRot = 0.25f; //1.95f // 0.25f
        float speedRotArduino = 0.000001f;

        float speedPos = 0.1f; //0.025f // 1.5f
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

        //public static SC_ShaderManager _shaderManager;


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

        public scupdate() : base(new DwmSharedSurface())
        {
            scgraphicssecpackagemessage = new scgraphicssecpackage();
            startTime = DateTime.Now;
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

            Program.MessageBox((IntPtr)0, "ShutDownGraphics scupdate.cs", "sc core systems message", 0);
        }

        public struct RECT
        {
            public int left, top, bottom, right;
        }
        Texture2D _texture2DFinal;
        ShaderResourceViewDescription resourceViewDescription;
        public static SharpDX.Direct3D11.Device deviceforscreencap;

        //protected override //override
        public scmessageobjectjitter[][] init_update_variables(scmessageobjectjitter[][] _sc_jitter_tasks, sccs.sccore.scsystemconfiguration configuration) //, scconsole.scconsolewriter _writer //, IntPtr hwnd
        {
            try
            {

                Camera = new _sc_camera();


                if (Program._useOculusRift == 0)
                {
                   
                    //originPos.Y += 4;
                    //originPos.Z -= 2f;

                    //Program.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                    Camera.SetPosition(originPos.X, originPos.Y, originPos.Z);

                    Camera.SetRotation(0, 0, 0);
                }
                else if (Program._useOculusRift == 1)
                {
                    speedRot = 1.75f;
                    speedPos = 1.25f;
                    RotationX = 0;
                    RotationY = 180;
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



                var directInput = new DirectInput();

                _Keyboard = new SharpDX.DirectInput.Keyboard(directInput);

                _Keyboard.Properties.BufferSize = 128;
                _Keyboard.Acquire();


                //_shaderManager = new SC_ShaderManager();
                //_shaderManager.Initialize(D3D.Device, Program.consoleHandle);

                /*
                _textureDescriptionFinal = new Texture2DDescription
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
                swapChainDescription.OutputHandle = Program.consoleHandle; //stackoverflow 59595739
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

                // ignore all Windows events
                using var factory = someswap.GetParent<Factory>();
                factory.MakeWindowAssociation(Program.consoleHandle, WindowAssociationFlags.IgnoreAll);

                //const int numAdapter = 0;
                //const int numOutput = 0;
                //var factory = new Factory1();
                //factory.MakeWindowAssociation(Program.vewindowsfoundedz, WindowAssociationFlags.Valid);
                //var adapter = factory.GetAdapter1(numAdapter);
                //deviceforscreencap = new SharpDX.Direct3D11.Device(adapter);

                //rect = new RECT();
                //GetWindowRect(Program.vewindowsfoundedz,out rect);


                someintegerlockingstruct[0].somereseteventt00 = new ManualResetEvent(true);
                someintegerlockingstruct[0].somereseteventt01 = new ManualResetEvent(false);

                sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, device);

                //g = Program.form.CreateGraphics();
                //bmp = new System.Drawing.Bitmap(Program.form.Size.Width, Program.form.Size.Height, g);
                //memgraph = Graphics.FromImage(bmp);

                //windowbounds = new System.Drawing.Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);

                //somebitmap = new Bitmap(windowbounds.Width, windowbounds.Height);


                //DwmSharedSurface somesurf = new DwmSharedSurface(this);

                //somesurf.StartCapture();

                */



                //sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, device);



                _sc_jitter_tasks[0][0].hasinit = 0;
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
        public static SharpDX.DirectInput.Keyboard _Keyboard;
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
                Program.MessageBox((IntPtr)0, "" + ex.ToString(), "sc core systems message", 0);
            }

            return _sc_jitter_tasks;
        }


        KeyboardState _KeyboardState;
        private bool ReadKeyboard()
        {
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
                        float rotx = 0;
                float roty = 0;
                float rotz = 0;
        private unsafe scmessageobjectjitter[][] _FrameVRTWO(jitter_sc[] jitter_sc, scmessageobjectjitter[][] _sc_jitter_tasks)
        {



            /*

            if (Program._useOculusRift == 1)
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







                if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.A))
                {
                    roty -= speedRot;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.D))
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
                }





                ReadKeyboard();

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



                if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Up))
                {
                    //direction_feet_forward.Z += speed * speedPos;
                    movePos += direction_feet_forward * speedPos * speedPos;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Down))
                {
                    movePos -= direction_feet_forward * speedPos * speedPos;
                    //direction_feet_forward.Z -= speed * speedPos;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Q))
                {
                    movePos += direction_feet_up * speedPos * speedPos;
                    //direction_feet_forward.Y += speed * speedPos;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Z))
                {
                    movePos -= direction_feet_up * speedPos * speedPos;
                    //direction_feet_forward.Y -= speed * speedPos;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Left))
                {
                    movePos -= direction_feet_right * speedPos * speedPos;
                    //direction_feet_forward.X -= speed * speedPos;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Right))
                {
                    movePos += direction_feet_right * speedPos * speedPos;
                    //direction_feet_forward.X += speed * speedPos;
                }



                OFFSETPOS = originPos + movePos;


            }
            else if (Program._useOculusRift == 1)
            {





                Camera.Render();
                ReadKeyboard();

                //float speed = 0.015f;
                //float speedRot = 0.1f;

                if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.A))
                {
                    roty -= speedRot;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.D))
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
                }

                var somerot = Camera.GetRotation();
                Camera.SetRotation(rotx, roty, somerot.Z);



                if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.R))
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








                Matrix tempmat = Camera.rotationMatrix;
                Quaternion otherQuat;
                Quaternion.RotationMatrix(ref tempmat, out otherQuat);

                Vector3 direction_feet_forward;
                Vector3 direction_feet_right;
                Vector3 direction_feet_up;

                direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);



                if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Up))
                {
                    //direction_feet_forward.Z += speed * speedPos;
                    movePos -= direction_feet_forward * speedPos * speedPos;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Down))
                {
                    movePos += direction_feet_forward * speedPos * speedPos;
                    //direction_feet_forward.Z -= speed * speedPos;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Q))
                {
                    movePos += direction_feet_up * speedPos * speedPos;
                    //direction_feet_forward.Y += speed * speedPos;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Z))
                {
                    movePos -= direction_feet_up * speedPos * speedPos;
                    //direction_feet_forward.Y -= speed * speedPos;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Left))
                {
                    movePos -= direction_feet_right * speedPos * speedPos;
                    //direction_feet_forward.X -= speed * speedPos;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Right))
                {
                    movePos += direction_feet_right * speedPos * speedPos;
                    //direction_feet_forward.X += speed * speedPos;
                }



                OFFSETPOS = originPos + movePos;

                Camera.SetPosition(OFFSETPOS.X, OFFSETPOS.Y, OFFSETPOS.Z);

            }*/








            
            if (canworkphysics == 1)
            {
                for (int i = 0; i < 1;)
                {
                    
                    /*
                    screencaptureresultswtc = 0;
                    try
                    {

                        
                        //somebitmap = new System.Drawing.Bitmap(1920, 1080, 1920*4, PixelFormat.Format32bppArgb, _hWnd);
                        //somebitmap.Save(@"C:\Users\steve\Desktop\screenRecord\" + bitmapcounter + "_" + ".png");
                        //bitmapcounter++;
                        //DeleteObject(_hWnd);





                        //sometest = memgraph.GetHdc();
                        //DeleteObject(sometest);
                        
                        screencaptureframe = sharpdxscreencapture.ScreenCapture(3);

                        if (scgraphicssecpackagemessage.scjittertasks != null)
                        {
                            if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                            {
                                if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                {
                                    if (_sc_jitter_tasks != null)
                                    {
                                        if (_sc_jitter_tasks[0] != null)
                                        {
                                            if (_sc_jitter_tasks[0].Length > 0)
                                            {
                                                _sc_jitter_tasks[0][0].frameByteArray = screencaptureframe.frameByteArray;
                                                //_sc_jitter_tasks[0][0].shaderresource = shaderResourceView;
                                            }
                                        }
                                    }
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
                    }*/
                }
            }















            if (Program._useOculusRift == 1)
            {

                //Program.MessageBox((IntPtr)0, "000", "sc core systems message", 0);
                try
                {
                    if (canworkphysics == 1)
                    {
                        if (Program.useArduinoOVRTouchKeymapper == 0)
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
                    Program.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                }

            }














            /*

            if (Program._useOculusRift == 1)
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



            /*
            if (canworkphysics == 1)
            {
                if (sharpdxscreencapture != null)
                {
                    if (sharpdxscreencapture.hasinit == 2)
                    {
                        for (int i = 0; i < 1;)
                        {
                            screencaptureresultswtc = 0;
                            try
                            {
                                scgraphicssecpackagemessage.scjittertasks = sharpdxscreencapture.ScreenCapture(scgraphicssecpackagemessage.scjittertasks, 3);
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
                    }
                }
            }*/








            if (Program._useOculusRift == 0 && canworkphysics == 1)
            {

                // Clear views
                D3D.DeviceContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);
                D3D.DeviceContext.ClearRenderTargetView(_renderTargetView, SharpDX.Color.LightGray); //LightGray
                //D3D.DeviceContext.clea




                float ratio = (float)SurfaceWidth / (float)SurfaceHeight;
                /// _projectionMatrix = Matrix.PerspectiveFovLH(3.14F / 3.0F, ratio, 0.001f, 1000);
                _projectionMatrix = Matrix.PerspectiveFovLH((float)(Math.PI / 4), ((float)SurfaceWidth / (float)SurfaceHeight), 0.0001f, 10000);

                //Matrix.PerspectiveOffCenterRH(0, (float)SurfaceWidth, 0, (float)SurfaceHeight, 0.001f, 1000, out _projectionMatrix);

                //viewMatrix = Matrix.LookAtLH(new Vector3(0, 3, 10), new Vector3(), Vector3.UnitY);
                viewMatrix = Camera.ViewMatrix;
                //Matrix.PerspectiveOffCenterRH(100, (float)SurfaceWidth, 100, (float)SurfaceHeight, 0.001f, 1000, out _projectionMatrix);

                /*float somescreensizew = 240 * 8 * 0.005f;
                float somescreensizeh = 135 * 8 * 0.005f;

                Matrix.OrthoOffCenterLH(0, somescreensizew, 0, somescreensizeh, 0.001f, 1000, out _projectionMatrix);
                */




                //Matrix.OrthoOffCenterLH(0, SurfaceWidth/10, 0, SurfaceHeight/14, 0.001f, 1000, out _projectionMatrix);




                //Matrix.OrthoOffCenterLH(0, (float)Program.form.Size.Width, 0, (float)Program.form.Size.Height, 0.001f, 1000, out _projectionMatrix);
                //Matrix.OrthoOffCenterLH(0, (float)Program.form.Size.Width, 0, (float)Program.form.Size.Height, 0.001f, 1000, out _projectionMatrix);



                //Matrix.PerspectiveLH((float)SurfaceWidth, (float)SurfaceHeight, 0.01f, 10, out _projectionMatrix);


                //ProjectionMatrix = Matrix.PerspectiveFovLH((float)(Math.PI / 4), ((float)configuration.Width / (float)configuration.Height), DSystemConfiguration.ScreenNear, DSystemConfiguration.ScreenDepth);
                //WorldMatrix = Matrix.Identity;
                //_projectionMatrix = Matrix.OrthoLH((float)SurfaceWidth, (float)SurfaceHeight, 0.1f, 10);




                //Program.MessageBox((IntPtr)0, "0", "sc core systems message", 0);
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
                    scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;




                    if (updatethreadupdateswtc0 == 0)
                    {


                        scgraphicssecpackagemessage.scgraphicssec = new sccs.scgraphics.scgraphicssec();
                        scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec._sc_create_world_objects(scgraphicssecpackagemessage.scjittertasks);



                        someintegerlockingstruct[0].someintegerlocking = 0;
                        someintegerlockingstruct[1].someintegerlocking = 0;
                        
                         






                        //ThreadProc();
              

                        /*
                        proct0 = new Thread(() =>
                        {
                            sccssharpdxscreencapture sharpdxscreencapture;

                            sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, deviceforscreencap);

                        _thread_looper:

                            //Console.WriteLine("t0 before WaitOne");
                            //Console.WriteLine("t0 before work");
                            //someintegerlockingstruct[0].somereseteventt01.Reset();
                            //someintegerlockingstruct[0].somereseteventt00.Set();
                            //someintegerlockingstruct[0].somereseteventt00.Reset();
                            //someintegerlockingstruct[0].somereseteventt00.Reset();
                            //someintegerlockingstruct[0].somereseteventt00.WaitOne();
                            //Console.WriteLine("t0 after WaitOne");

                            //Console.WriteLine("t0 after WaitOne");
                            //if (someintegerlockingstruct[0].someintegerlocking == 1)
                            //{
                            //    Console.WriteLine("t0 working");
                            //    //somereseteventt0.WaitOne();
                            //    someintegerlockingstruct[0].someintegerlocking = 0;
                            //}




                            if (canworkphysics == 1)
                            {
                                for (int i = 0; i < 1;)
                                {
                                    screencaptureresultswtc = 0;
                                    try
                                    {
                                        scgraphicssecpackagemessage.scjittertasks = sharpdxscreencapture.ScreenCapture(scgraphicssecpackagemessage.scjittertasks, 3);
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
                            //Console.WriteLine("t0 after work");
                            //Console.WriteLine("t0 before WaitOne");
                            //someintegerlockingstruct[0].somereseteventt01.Set();
                            //someintegerlockingstruct[0].somereseteventt01.Reset();
                            //someintegerlockingstruct[0].somereseteventt00.WaitOne();



                            //if (someintegerlockingstruct[0].someintegerlocking == 0)
                            //{
                            //    Console.WriteLine("t0 locking");
                            //    //somereseteventt1.Set();
                            //    somereseteventt0.Reset();
                            //    someintegerlockingstruct[0].someintegerlocking = 1;
                            //}






                            //GC.Collect();
                            Thread.Sleep(1);
                            //Console.WriteLine("t0 lock inneficient");
                            goto _thread_looper;

                        }, 0); //100000

                        //Process.GetCurrentProcess().Exited

                        proct0.IsBackground = true;
                        proct0.Priority = ThreadPriority.Lowest;
                        proct0.SetApartmentState(ApartmentState.STA);
                        proct0.Start();
                        */


                        /*
                        proct1 = new Thread(() =>
                        {

                        _thread_looper:

                            if (scgraphicssecpackagemessage.scjittertasks != null)
                            {
                                if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                {
                                    if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                    {
                                        //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswritescreenassetstobuffer(scgraphicssecpackagemessage.scjittertasks);

                                        //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritedirectiontobuffer(scgraphicssecpackagemessage.scjittertasks);
                                        // scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwriteheightmaptobuffer(scgraphicssecpackagemessage.scjittertasks);
                                        scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer();

                                        //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                        //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                    }
                                }
                            }
                            //GC.Collect();
                            Thread.Sleep(1);
                            goto _thread_looper;

                        }, 0); //100000

                        proct1.IsBackground = true;
                        proct1.Priority = ThreadPriority.Lowest;
                        proct1.SetApartmentState(ApartmentState.STA);
                        proct1.Start();
                        */






                        //var waitHandleA = new ManualResetEvent(false);
                        //var waitHandleB = new ManualResetEvent(false);








                        /*
                        var _console_worker_task = Task<object[]>.Factory.StartNew((sometaskmsg) =>
                        {
                            //someintegerlockingstruct[0].somereseteventt00.Set();
                        loopthread:
                            //Program.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                            try
                            {

                                if (scgraphicssecpackagemessage.scjittertasks != null)
                                {
                                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                    {
                                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                        {
                                            //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswritescreenassetstobuffer(scgraphicssecpackagemessage.scjittertasks);

                                            //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritedirectiontobuffer(scgraphicssecpackagemessage.scjittertasks);
                                            // scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwriteheightmaptobuffer(scgraphicssecpackagemessage.scjittertasks);
                                            scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer();

                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                        }
                                    }
                                }
                     
                                //GC.Collect();
                                //Thread.Sleep(1);
                            }
                            catch (Exception ex)
                            {
                                Program.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                            }
                            Thread.Sleep(1);
                            goto loopthread;
                        }, screencaptureframe);*/


                        //initWaitHandle.WaitOne();

















                        //currentWaitHandle = someintegerlockingstruct[0].somereseteventt00;
                        //AutoResetEvent initWaitHandle = new AutoResetEvent(false);




                        /*
                        threads[0] = new Thread(() =>
                        {
                            someintegerlockingstruct[0].somereseteventt00.Set();
                            //someintegerlockingstruct[0].somereseteventt00.Set();
                            //initWaitHandle.Set();
                            //scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;
                            //sc_graphics_sec graphicssec;

                            threadupdateswtc = 0;
                            //Program.MessageBox((IntPtr)0, "test", "sccsmsg", 0);


                            SwapChain someswap;
                            SharpDX.Direct3D11.Device deviceforscreencap;
                            SwapChainDescription swapChainDescription = new SwapChainDescription();
                            swapChainDescription.BufferCount = 1;
                            swapChainDescription.IsWindowed = true;
                            swapChainDescription.OutputHandle = Program.consoleHandle;
                            swapChainDescription.SampleDescription = new SampleDescription(1, 0);
                            swapChainDescription.Usage = Usage.RenderTargetOutput | Usage.ShaderInput;//Usage.RenderTargetOutput;// | Usage.ShaderInput;
                            swapChainDescription.SwapEffect = SwapEffect.Sequential;
                            swapChainDescription.Flags = SwapChainFlags.AllowModeSwitch;
                            swapChainDescription.ModeDescription.Width = SurfaceWidth;
                            swapChainDescription.ModeDescription.Height = SurfaceHeight;
                            swapChainDescription.ModeDescription.Format = Format.R8G8B8A8_UNorm;
                            swapChainDescription.ModeDescription.RefreshRate.Numerator = 0;
                            swapChainDescription.ModeDescription.RefreshRate.Denominator = 1;


                            //SharpDX.Direct3D11.Device somedevice;
                            // Create DirectX drawing device.
                            //device = new Device(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.Debug);
                            SharpDX.Direct3D11.Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None, swapChainDescription, out deviceforscreencap, out someswap);
                            sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, deviceforscreencap);
                        _thread_looper:

                            //Console.WriteLine("t0 task pumped");
                            //someintegerlockingstruct[0].somereseteventt01.WaitOne();


                            //Console.WriteLine("t1 after WaitOne");

                            //scgraphicssecpackagemessage.scgraphicssec = graphicssec;

                            //Program.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                            try
                            {



                                if (canworkphysics == 1)
                                {
                                    for (int i = 0; i < 1;)
                                    {
                                        screencaptureresultswtc = 0;
                                        try
                                        {
                                            scgraphicssecpackagemessage.scjittertasks = sharpdxscreencapture.ScreenCapture(scgraphicssecpackagemessage.scjittertasks,3);
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

                                /*lock (sharpdxscreencapture)
                                {
                                }
                                //someintegerlockingstruct[0].somereseteventt01.Reset();
                                //someintegerlockingstruct[0].somereseteventt01.Set();
                                //someintegerlockingstruct[0].somereseteventt01.Set();
                                someintegerlockingstruct[0].somereseteventt00.WaitOne();
                                //someintegerlockingstruct[0].somereseteventt01.Set();
                            }
                            catch (Exception ex)
                            {
                                Program.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                            }



                            Thread.Sleep(1);
                            goto _thread_looper;

                            //ShutDown();
                            //ShutDownGraphics();
                            //Program.MessageBox((IntPtr)0, "THREAD QUIT0", "sc core systems message", 0);
                            if (someswap != null)
                            {
                                someswap.Dispose();
                                someswap = null;
                            }

                            if (deviceforscreencap != null)
                            {
                                deviceforscreencap.Dispose();
                                deviceforscreencap = null;
                            }


                        }, 0);

                        threads[0].IsBackground = true;
                        threads[0].Priority = ThreadPriority.Lowest; //AboveNormal
                        threads[0].SetApartmentState(ApartmentState.STA);
                        threads[0].Start();*/
                        //initWaitHandle.WaitOne();


                        updatethreadupdateswtc0 = 1;
                    }
                }
            }

            if (threads[0] != null)
            {
                if (!threads[0].IsAlive)
                {
                    Program.MessageBox((IntPtr)0, "THREAD QUIT1", "sc core systems message", 0);

                }
            }

            /*
            if (sharpdxscreencapture != null)
            {
                if (sharpdxscreencapture.hasinit == 2)
                {
                    try
                    {
                        //device.ImmediateContext.CopyResource(_sc_jitter_tasks[0][0].theframe._texture2DFinal, _texture2DFinal);
                        /*shaderRes = new ShaderResourceView(device, _texture2DFinal, resourceViewDescription);
                        deviceforscreencap.ImmediateContext.GenerateMips(shaderRes);
                        //copiedframe.ShaderResource = shaderRes;
                        _sc_jitter_tasks[0][0].shaderresource = shaderRes;
                        //_sc_jitter_tasks[0][0].frameByteArray = scgraphicssecpackagemessage.scjittertasks[0][0].theframe.frameByteArray;



                        //var columns0 = 1920;
                        //var rows0 = 1080;
                        //var image = new System.Drawing.Bitmap(1920, 1080, 1920*4, PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(screencaptureframe.frameByteArray, 0));
                        //image.Save(@"C:\Users\steve\Desktop\screenrecord\" + bitmapcounter + "_" + rows0.ToString("00") + columns0.ToString("00") + ".png");
                        //bitmapcounter++;

                        //Console.WriteLine("sometest");
                    }
                    catch (Exception ex)
                    {
                        Program.MessageBox((IntPtr)0, "error" + ex.ToString(), "scmsg", 0);
                    }
                    /*
                    lock (screencaptureframe._texture2DFinal)
                    {

                    }
                }
            }*/













            //someintegerlockingstruct[0].somereseteventt01.Set();

            if (Program._useOculusRift == 0 && canworkphysics == 1)
            {

                //scgraphicssecpackagemessage.scjittertasks[0][0].theframe.frameByteArray = _sc_jitter_tasks[0][0].frameByteArray;



                if (updatethreadupdateswtc1 == 0)
                {


                    /*
                    threads[1] = new Thread(() =>
                    {
                        //someintegerlockingstruct[0].somereseteventt01.Set();
                        //initWaitHandle.Set();
                        //scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;
                        //sc_graphics_sec graphicssec;

                        threadupdateswtc = 0;
                    //Program.MessageBox((IntPtr)0, "test", "sccsmsg", 0);
                    //someintegerlockingstruct[0].somereseteventt01.Set();

                    _thread_looper:

                        //someintegerlockingstruct[0].somereseteventt00.Reset();
                        // Console.WriteLine("t1 task pumped");
                        //someintegerlockingstruct[0].somereseteventt01.WaitOne();
                        //Console.WriteLine("t1 after WaitOne");

                        //scgraphicssecpackagemessage.scgraphicssec = graphicssec;

                        //Program.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                        try
                        {
                            if (scgraphicssecpackagemessage.scjittertasks != null)
                            {
                                if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                {
                                    if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                    {
                                        //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswritescreenassetstobuffer(scgraphicssecpackagemessage.scjittertasks);

                                        //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritedirectiontobuffer(scgraphicssecpackagemessage.scjittertasks);
                                        // scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwriteheightmaptobuffer(scgraphicssecpackagemessage.scjittertasks);
                                        scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer();

                                        //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                        //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Program.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                        }


                        //someintegerlockingstruct[0].somereseteventt00.Reset();
                        //someintegerlockingstruct[0].somereseteventt00.Set();
                        //someintegerlockingstruct[0].somereseteventt01.Reset();
                        //someintegerlockingstruct[0].somereseteventt00.Set();
                        //someintegerlockingstruct[0].somereseteventt01.WaitOne();

                        //someintegerlockingstruct[0].somereseteventt01.WaitOne();

                        Thread.Sleep(0);

                        goto _thread_looper;

                        //ShutDown();
                        //ShutDownGraphics();

                    }, 0);

                    threads[1].IsBackground = true;
                    threads[1].Priority = ThreadPriority.Lowest; //AboveNormal
                    threads[1].SetApartmentState(ApartmentState.STA);
                    threads[1].Start();
                    //initWaitHandle.WaitOne();*/



                    updatethreadupdateswtc1 = 1;
                }
            }
            //someintegerlockingstruct[0].somereseteventt01.Reset();

            /*
            threads[i] = new Thread(() =>
            {
                //initWaitHandle.Set();
                //ThreadProc(i, someintegerlockingstruct);
            });

            threads[i].IsBackground = true;
            threads[i].Start();
            initWaitHandle.WaitOne();*/





            /*
            for (int i = 1; i < threads.Length; i++)
            {

                threads[i] = new Thread(() =>
                {
                    initWaitHandle.Set();
                    //scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;
                    //sc_graphics_sec graphicssec;

                    threadupdateswtc = 0;
                //Program.MessageBox((IntPtr)0, "test", "sccsmsg", 0);

                _thread_looper:

                    Console.WriteLine("t1 task pumped");
                    //someintegerlockingstruct[0].somereseteventt01.WaitOne();
                    //Console.WriteLine("t1 after WaitOne");

                    //scgraphicssecpackagemessage.scgraphicssec = graphicssec;

                    //Program.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                    try
                    {

                        if (i == 0)
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


                        if (i == 1)
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
                    }
                    catch (Exception ex)
                    {
                        Program.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                    }



                    Thread.Sleep(1);
                    goto _thread_looper;

                    //ShutDown();
                    //ShutDownGraphics();

                }, 0);

                threads[i].IsBackground = true;
                threads[i].Priority = ThreadPriority.Lowest; //AboveNormal
                threads[i].SetApartmentState(ApartmentState.STA);
                threads[i].Start();
                initWaitHandle.WaitOne();
            }*/




            /*
            var _console_worker_task = Task<object[]>.Factory.StartNew((sometaskmsg) =>
            {
            loopthread:
                //Program.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                try
                {
                }
                catch (Exception ex)
                {
                    Program.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                }
                Thread.Sleep(1);
                goto loopthread;
            }, scgraphicssecpackagemessage);*/
            /*

                                    updatethreadupdateswtc = 1;
                                }

                                //scgraphicssecpackagemessage.scjittertasks = graphicssec.workonshaders(scgraphicssecpackagemessage);

                                /*viewMatrix.Transpose();
                                _projectionMatrix.Transpose();

                                scgraphicssecpackagemessage.viewMatrix = viewMatrix;
                                scgraphicssecpackagemessage.projectionMatrix = _projectionMatrix;



                                if (screencaptureframe._texture2DFinal != null)
                                {
                                    lock (screencaptureframe._texture2DFinal)
                                    {
                                        try
                                        {
                                            device.ImmediateContext.CopyResource(screencaptureframe._texture2DFinal, _texture2DFinal);
                                            shaderRes = new ShaderResourceView(device, _texture2DFinal, resourceViewDescription);
                                            device.ImmediateContext.GenerateMips(shaderRes);
                                            //copiedframe.ShaderResource = shaderRes;
                                            _sc_jitter_tasks[0][0].shaderresource = shaderRes;
                                            _sc_jitter_tasks[0][0].frameByteArray = screencaptureframe.frameByteArray;

                                            //var columns0 = 1920;
                                            //var rows0 = 1080;
                                            //var image = new System.Drawing.Bitmap(1920, 1080, 1920*4, PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(screencaptureframe.frameByteArray, 0));
                                            //image.Save(@"C:\Users\steve\Desktop\screenrecord\" + bitmapcounter + "_" + rows0.ToString("00") + columns0.ToString("00") + ".png");
                                            //bitmapcounter++;

                                            //Console.WriteLine("sometest");
                                        }
                                        catch (Exception ex)
                                        {
                                            Program.MessageBox((IntPtr)0, "error" + ex.ToString(), "scmsg", 0);
                                        }
                                    }
                                }




                                if (scgraphicssecpackagemessage.scjittertasks != null)
                                {
                                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                    {
                                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                        {
                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                            _sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);
                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;
                                        }
                                    }
                                }



                                if (_lastShaderResourceView != null)
                                {
                                    _lastShaderResourceView.Dispose();
                                }

                                _lastShaderResourceView = shaderRes;

                                /* lock (someintegerlockingstruct[0].somereseteventt00)
                                 {
                                     //someintegerlockingstruct[0].somereseteventt01.Reset();
                                     someintegerlockingstruct[0].somereseteventt00.Set();
                                     someintegerlockingstruct[0].somereseteventt00.Reset();
                                 }
                                Execute();

                                //Console.WriteLine("t0 somereseteventt0 reseted");
                            }





                            D3D.SwapChain.Present(0, PresentFlags.None);
                            //GC.Collect();
                        }*/



            if (Program._useOculusRift == 0 && canworkphysics == 1)
            {
                //Program.MessageBox((IntPtr)0, "0", "sc core systems message", 0);
                if (canworkphysics == 1)
                {
                    scgraphicssecpackagemessage.viewMatrix = viewMatrix;
                    scgraphicssecpackagemessage.projectionMatrix = _projectionMatrix;




                    
                    
                    if (scgraphicssecpackagemessage.scjittertasks != null)
                    {
                        if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                        {
                            if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                            {
                                scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);

                                //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);
                                //_sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;
                            }
                        }
                    }
                    

                    /*
                    if (_lastShaderResourceView != null)
                    {
                        _lastShaderResourceView.Dispose();
                    }

                    _lastShaderResourceView = shaderRes;*/

                    //D3D.SwapChain.Present(0, PresentFlags.None);

                    D3D.swapChain1.Present(0, PresentFlags.None);
                    
                    //someintegerlockingstruct[0].somereseteventt00.Reset();
                    //someintegerlockingstruct[0].somereseteventt00.Set();
                    //someintegerlockingstruct[0].somereseteventt00.Set();
                    //someintegerlockingstruct[0].somereseteventt00.Reset();                
                    //someintegerlockingstruct[0].somereseteventt01.Reset();
                    //someintegerlockingstruct[0].somereseteventt00.Reset();
                }
            }
            else if (Program._useOculusRift == 1 && canworkphysics == 1)
            {

                Program.MessageBox((IntPtr)0, "test0", "sccsmsg", 0);

                if (D3D != null && Program.exitedprogram != 1)
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


                            /*
                            main_thread_update = new Thread(() =>
                            {



                                //scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;
                                //sc_graphics_sec graphicssec;

                                int threadupdateswtc = 0;

                            //Program.MessageBox((IntPtr)0, "test", "sccsmsg", 0);

                            _thread_looper:


                                //scgraphicssecpackagemessage.scgraphicssec = graphicssec;

                                //Program.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
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
                                                    scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);

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
                                    Program.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                                }

                                Thread.Sleep(1);
                                goto _thread_looper;

                                //ShutDown();
                                //ShutDownGraphics();

                            }, 0);

                            main_thread_update.IsBackground = true;
                            main_thread_update.Priority = ThreadPriority.Lowest; //AboveNormal
                            main_thread_update.SetApartmentState(ApartmentState.STA);
                            main_thread_update.Start();*/

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
                            D3D.device.ImmediateContext.ClearRenderTargetView(eyeTexture.RenderTargetViews[textureIndex], SharpDX.Color.LightGray); //DimGray Black
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

                            if (Program.usethirdpersonview == 0)
                            {

                                //FOR THE VERTEX SHADER
                                Quaternion somedirquat1;
                                Quaternion.RotationMatrix(ref tempmatter, out somedirquat1);
                                dirikvoxelbodyInstanceRight0 = new Vector4(-sc_maths._newgetdirleft(somedirquat1), 0);
                                dirikvoxelbodyInstanceUp0 = new Vector4(sc_maths._newgetdirup(somedirquat1), 0);
                                dirikvoxelbodyInstanceForward0 = new Vector4(sc_maths._newgetdirforward(somedirquat1), 0);
                            }
                            else if (Program.usethirdpersonview == 1)
                            {
                                Quaternion somedirquat1;
                                Quaternion.RotationMatrix(ref tempmatter, out somedirquat1);
                                dirikvoxelbodyInstanceRight0 = new Vector4(-sc_maths._newgetdirleft(somedirquat1), 0);
                                dirikvoxelbodyInstanceUp0 = new Vector4(sc_maths._newgetdirup(somedirquat1), 0);
                                dirikvoxelbodyInstanceForward0 = new Vector4(sc_maths._newgetdirforward(somedirquat1), 0);

                                viewPosition = viewPosition + (new Vector3(dirikvoxelbodyInstanceForward0.X, dirikvoxelbodyInstanceForward0.Y, dirikvoxelbodyInstanceForward0.Z) * Program.offsetthirdpersonview);
                            }





                            viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);
                            _projectionMatrix = D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.001f, 1000.0f, ProjectionModifier.None).ToMatrix();
                            oriProjectionMatrix = _projectionMatrix;
                            _projectionMatrix.Transpose();


                            //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                            //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                            //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.




                            //Program.MessageBox((IntPtr)0, "0", "sc core systems message", 0);
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
                                scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;

                                
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

                                    //Program.MessageBox((IntPtr)0, "test", "sccsmsg", 0);

                                    _thread_looper:


                                        //scgraphicssecpackagemessage.scgraphicssec = graphicssec;

                                        //Program.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
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
                                            Program.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
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



                                    //Program.MessageBox((IntPtr)0, "test", "sccsmsg", 0);

                                    _thread_looper:

                                        if (threadupdateswtc == 0 && Program.exitedprogram != 1) //0
                                        {
                                            //scgraphicssecpackagemessage.scgraphicssec = graphicssec;

                                            //Program.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
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
                                                Program.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
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
                                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                        {
                                            Program.MessageBox((IntPtr)0, "test", "sccsmsg", 0);
                                            //scgraphicssecpackagemessage.scjittertasks = 
                                            scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);

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
                                /*if (D3D.result != null && Program.exitedprogram != 1)
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

            //Program.MessageBox((IntPtr)0, "001", "sc core systems message", 0);


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

