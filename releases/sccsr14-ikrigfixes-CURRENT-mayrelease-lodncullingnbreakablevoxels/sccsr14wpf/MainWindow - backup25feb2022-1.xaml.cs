using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Windows.Forms;
using System;
using System.Diagnostics;

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
//using System.Windows.Controls;
//using Win32.DesktopDuplication;
using System;

using Win32.Shared.Interfaces;
//using Win32.Shared.Interop;


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



//using SharpDX;
using SharpDX.DirectInput;
using System.Threading;
using System.Runtime;
using System.Runtime.CompilerServices;
using sccs.scmessageobject;
using System.ComponentModel;
using Jitter.DataStructures;
using Jitter.Forces;
using System.Windows.Interop;
using SharpDX.Windows;
using Win32.DwmSharedSurface.Interop;
using Win32.Shared.Interop;

//using sccsr14sc;

namespace sccs
{
    //internal static unsafe class Program
    public partial class MainWindow : Window
    {
        private void WindowsOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Close();
            //this.Hide();

        }

        //static byte* srcPointer;
        //static byte* dstPointer;
        public static int _useOculusRift = 0;

        public static IntPtr consoleHandle;
        //public static int exitedProgram = -1;

        public static int usethirdpersonview = 0;
        public static float offsetthirdpersonview = 0.35f;//at or over 1 to get a decent ootb working 3rdpersonview.
        public static int usetypeofvoxel = 0;
        public static int usejitterphysics = 0;
        public static int usejitterphysicsbuo = 0;
        public static int useArduinoOVRTouchKeymapper = 0;
        public static int useSendScreenToArduino = 0;

        public static JVector _world_gravity = new JVector(0, -9.81f, 0); //-9.81f base
        public static int worlditerations = 3; // as high as possible normally for higher precision
        public static int worldsmalliterations = 3; // as high as possible normally for higher precision
        public static float worldallowedpenetration = 0.00123f; //0.00123f  _world_gravity = new JVector(0, -9.81f, 0);
        public static bool allowdeactivation = true;

        public static int physicsengineinstancex = 1; //4
        public static int physicsengineinstancey = 1; //1
        public static int physicsengineinstancez = 1; //4

        public static int worldwidth = 1;
        public static int worldheight = 1;
        public static int worlddepth = 1;


        public static int exitedprogram = -1;
        public static scupdate updatescript;
        public static scsystemconfiguration config;
        public static int initdirectXmainswtch = -1;
        public static int initvrmainswtch = 2;
        public static int has_init_directx = 0;


        public static SharpDX.DirectInput.KeyboardState keyboardstate;
        public static keyboardinput keyboardinput;
        public static InputSimulator inputsim;
        public static KeyboardSimulator keyboardsim;
        public static MouseSimulator mousesim;


        static int MaxSizeMainObject = 16;
        public static int MaxSizeMessageObject = 32;
        static scmessageobject.scmessageobject[] mainreceivedmessages;//
        static scmessageobjectjitter[][] sccsjittertasks = null;
        static jitter_sc[] jitter_sc;



        static ShaderResourceView shaderResourceView;
        static int startmainthread = 0;
        static int bitmapcounter = 0;
        static int somecounter = 0;
        static System.Drawing.Bitmap _bitmap;
        static System.Drawing.Rectangle boundsRect;
        static System.Drawing.Imaging.BitmapData bmpData;
        static int _bytesTotal;
        static Texture2DDescription _textureDescription;
        static byte[] _textureByteArray;
        static Texture2D _texture2D;

        static ShaderResourceView lastshaderresourceview;












        //https://www.unknowncheats.me/forum/c/62019-c-non-hooked-external-directx-overlay.html

        private static Margins marg;

        internal struct Margins
        {
            public int Left, Right, Top, Bottom;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);


        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_LAYERED = 0x00080000; //0x80000
        //public const int WS_EX_TRANSPARENT = 0x20;

        public const int LWA_ALPHA = 0x2;
        public const int LWA_COLORKEY = 0x1;
        public const long WS_EX_TOPMOST = 0x00000008L;
        public const long WS_NOACTIVATE = 0x08000000L;
        const long WS_EX_WINDOWEDGE = 0x00000100L;
        public const long WS_EX_TRANSPARENT = 0x00000020L;
        public const long WS_EX_CLIENTEDGE = 0x00000200L;
        const long WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE;


        [DllImport("user32.dll")]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref Margins pMargins);



        [DllImport("USER32.DLL")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool DrawMenuBar(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32", ExactSpelling = true, SetLastError = true)]
        internal static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In, Out] ref RECT rect, [MarshalAs(UnmanagedType.U4)] int cPoints);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left, top, bottom, right;
        }
        public static RenderForm form;


        public static readonly string WINDOW_NAME = "voidexpanse";  //name of the window
        public const int GWL_STYLE = -16;              //hex constant for style changing
        public const int WS_BORDER = 0x00800000;       //window with border
        public const int WS_CAPTION = 0x00C00000;      //window with a title bar
        public const int WS_SYSMENU = 0x00080000;      //window with no borders etc.
        public const int WS_MINIMIZEBOX = 0x00020000;  //window with minimizebox



        public static IntPtr window;
        public static RECT rect;

        //static int _init_main = 1;
        public static IntPtr HWND_DESKTOP;

        //[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        //static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        public static IntPtr vewindowsfoundedz;

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        public delegate int Callback(int hWnd, int lParam);
        static Callback myCallBack = new Callback(EnumChildGetValue);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        private static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                EnumWindowsProc childProc = new EnumWindowsProc(EnumWindow);
                EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        private static bool EnumWindow(IntPtr hWnd, IntPtr lParam)
        {
            GCHandle gcChildhandlesList = GCHandle.FromIntPtr(lParam);

            if (gcChildhandlesList == null || gcChildhandlesList.Target == null)
            {
                return false;
            }

            List<IntPtr> childHandles = gcChildhandlesList.Target as List<IntPtr>;
            childHandles.Add(hWnd);

            return true;
        }
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnableWindow(IntPtr hWnd, bool bEnable);




        public static int EnumChildGetValue(int hWnd, int lParam)
        {
            StringBuilder formDetails = new StringBuilder(256);
            string editText = "";
            StringBuilder ClassName = new StringBuilder(256);
            var nRet = GetClassName(new IntPtr(hWnd), ClassName, ClassName.Capacity);
            Console.WriteLine("Control Caption : " + editText + " hWnd : " + hWnd.ToString("X") + " Class Name : " + ClassName);
            Trace.WriteLine("Class Name : " + ClassName);
            Console.ReadLine();

            if (ClassName.ToString().Equals("Edit"))
            {
                Console.WriteLine("Edit Control Found");
                Console.WriteLine("Current Control : " + hWnd.ToString("X"));
                Console.WriteLine("Disabling Notepad Edit Component");
                EnableWindow(new IntPtr(hWnd), true);
            }
            return 1;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };




        public static IntPtr capturedwindow;

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            FindWindows();


        }

        public IntPtr PickCaptureTarget(IntPtr hWnd)
        {
            new WindowInteropHelper(this).Owner = hWnd;
            ShowDialog();

            //return ((CapturableWindow?)Windows.SelectedItem)?.Handle ?? IntPtr.Zero;

            capturedwindow = ((CapturableWindow)Windows.SelectedItem).Handle;
            return capturedwindow;

        }

        private void FindWindows()
        {
            var wih = new WindowInteropHelper(this);
            Win32.Shared.Interop.NativeMethods.EnumWindows((hWnd, lParam) =>
            {
                // ignore invisible windows
                if (!Win32.Shared.Interop.NativeMethods.IsWindowVisible(hWnd))
                    return true;

                // ignore untitled windows
                var title = new StringBuilder(1024);
                Win32.Shared.Interop.NativeMethods.GetWindowText(hWnd, title, title.Capacity);
                if (string.IsNullOrWhiteSpace(title.ToString()))
                    return true;

                // ignore me
                if (wih.Handle == hWnd)
                    return true;

                Win32.Shared.Interop.NativeMethods.GetWindowThreadProcessId(hWnd, out var processId);

                // ignore by process name
                var process = Process.GetProcessById((int)processId);
                if (_ignoreProcesses.Contains(process.ProcessName.ToLower()))
                    return true;

                if (process.ProcessName.ToLower() == "voidexpanse")
                {
                    Windows.Items.Add(new CapturableWindow
                    {
                        Handle = hWnd,
                        Name = $"{title} ({process.ProcessName}.exe)"
                    });

                }
                return true;
            }, IntPtr.Zero);
        }

        private readonly string[] _ignoreProcesses = { "applicationframehost", "shellexperiencehost", "systemsettings", "winstore.app", "searchui" };
        

        static void ProcessExitHandler(object sender, EventArgs e)
        {
            exitedprogram = 1;

            //updatescript.heightmapthread.Abort();
            //updatescript.heightmapthread.Suspend();


            if (updatescript != null)
            {
                updatescript.threadupdateswtc = -1;
                updatescript.canworkphysics = -1;
                scupdate.stopovr = -1;

                if (updatescript.scgraphicssecpackagemessage.scgraphicssec != null)
                {
                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop != null)
                    {
                        for (int i = 0; i < updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop.Length; i++)
                        {

                            for (int j = 0; j < updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop.Length; j++)
                            {
                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayofindexzeromesh[j].ShutDown();
                            }

                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].ShutDown();
                        }
                    }
                }
                updatescript.heightmapthread = null;

            }


            //updatescript.scgraphicssecpackagemessage.scgraphicssec.
            scdirectx.D3D.ShutDown();
            if (updatescript != null)
            {
                updatescript = null;
            }

            MessageBox((IntPtr)0, "exiting", "scmsg", 0);
            //throw new NotImplementedException("program has exited");
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        static Thread _mainThread;
        static int initthread = 0;
        // static int initform = 0;

        static SharpDX.DXGI.Factory factoryy;

        public static int exitedMainWindow = 1;

        public MainWindow mainwindow;
        Texture2D texture2d;

        IntPtr handleOfWpf;


        //[STAThread]
        public MainWindow()
        //static void Main()
        {
            //var somewindow = new WindowInteropHelper(System.Windows.Application.Current.MainWindow);
            //handleOfWpf = somewindow.EnsureHandle();

            InitializeComponent();
      

            //Loaded += OnLoaded;

            mainwindow = this;



            int initform = 0;
        mainthreadloop:

            if (initthread == -1)
            {
                _mainThread = new Thread((tester0000) =>
                {
                //sccs.MainWindow.MessageBox((IntPtr)0, "Form1", "scmsg", 0);



                _thread_main_loop:

                    if (initform == 1) // && sccsr14sc.Form1.initForm == 1
                    {
                        //consoleHandle = sccsr14sc.Form1.theHandle;

                        mainreceivedmessages = new scmessageobject.scmessageobject[MaxSizeMainObject];

                        for (int i = 0; i < mainreceivedmessages.Length; i++)
                        {
                            mainreceivedmessages[i] = new scmessageobject.scmessageobject();
                            mainreceivedmessages[i]._received_switch_in = -1;
                            mainreceivedmessages[i]._received_switch_out = -1;
                            mainreceivedmessages[i]._sending_switch_in = -1;
                            mainreceivedmessages[i]._sending_switch_out = -1;
                            mainreceivedmessages[i]._timeOut0 = -1;
                            mainreceivedmessages[i]._ParentTaskThreadID0 = -1;
                            mainreceivedmessages[i]._main_cpu_count = 1;
                            mainreceivedmessages[i]._passTest = "";
                            mainreceivedmessages[i]._welcomePackage = -1;
                            mainreceivedmessages[i]._work_done = -1;
                            mainreceivedmessages[i]._current_menu = -1;
                            mainreceivedmessages[i]._last_current_menu = -1;
                            mainreceivedmessages[i]._main_menu = -1;
                            mainreceivedmessages[i]._menuOption = "";
                            mainreceivedmessages[i]._voRecSwtc = -1;
                            mainreceivedmessages[i]._voRecMsg = "";
                            mainreceivedmessages[i]._someData = new object[MaxSizeMainObject];

                            for (int j = 0; j < mainreceivedmessages[i]._someData.Length; j++)
                            {
                                mainreceivedmessages[i]._someData[j] = new object();
                            }


                            //mainreceivedmessages[0]._someData[0] = new object();


                            //VOICE RECOGNITION. NOT IMPLEMENTED YET.
                            //if (i == MaxSizeMainObject - 1)
                            //{
                            //    mainreceivedmessages[i]._someData[0] = _keyboard_input._KeyboardState;
                            //    mainreceivedmessages[i]._voRecSwtc = 1;
                            //}
                        }
                        ///////////////////////////////
                        ///////////////////////////////   
                        ///////////////////////////////   
                        ///////////////////////////////  
                        ///message_thread_safe_kinda///   
                        ///////////////////////////////   
                        ///////////////////////////////   
                        ///////////////////////////////

                        //SCGLOBALSACCESSORS AND GLOBALS CREATOR
                        sccs.sccore.scglobalsaccessor SCGLOBALSACCESSORS = new sccs.sccore.scglobalsaccessor(mainreceivedmessages);
                        if (SCGLOBALSACCESSORS == null)
                        {
                            //System.Windows.MessageBox.Show("SCGLOBALSACCESSORS NULL", "Console");
                        }
                        else
                        {
                            //System.Windows.MessageBox.Show("SCGLOBALSACCESSORS !NULL", "Console");
                        }
                        //borderlessconsole console_ = new borderlessconsole();
                        //SCGLOBALSACCESSORS AND GLOBALS CREATOR



                        //consoleHandle = sccsr14sc.Form1.theHandle;
                        consoleHandle = scconsolecore.handle;

                        updatescript = new scupdate();

                        //sccs.MainWindow.MessageBox((IntPtr)0, "scupdate initiated", "scmsg", 0);


                        if (usejitterphysics == 1)
                        {
                            jitter_sc = new jitter_sc[1];

                            //jitter_sc = new jitter_sc[physicsengineinstancex * physicsengineinstancey * physicsengineinstancez];
                            //sccsjittertasks = new scmessageobjectjitter[physicsengineinstancex * physicsengineinstancey * physicsengineinstancez][];
                            sccsjittertasks = new scmessageobjectjitter[1][];
                            sccsjittertasks[0] = new scmessageobjectjitter[1];
                            sccsjittertasks[0][0] = new scmessageobjectjitter();

                            sc_jitter_data _sc_jitter_data = new sc_jitter_data();
                            _sc_jitter_data.alloweddeactivation = allowdeactivation;
                            _sc_jitter_data.allowedpenetration = worldallowedpenetration;
                            _sc_jitter_data.width = worldwidth;
                            _sc_jitter_data.height = worldheight;
                            _sc_jitter_data.depth = worlddepth;
                            _sc_jitter_data.gravity = _world_gravity;
                            _sc_jitter_data.smalliterations = worldsmalliterations;
                            _sc_jitter_data.iterations = worlditerations;

                            for (int xx = 0; xx < physicsengineinstancex; xx++)
                            {
                                for (int yy = 0; yy < physicsengineinstancey; yy++)
                                {
                                    for (int zz = 0; zz < physicsengineinstancez; zz++)
                                    {
                                        var indexer00 = xx + physicsengineinstancex * (yy + physicsengineinstancey * zz);
                                        //_jitter_physics[indexer00] = DoSpecialThing();
                                        sccsjittertasks[indexer00] = new scmessageobjectjitter[worldwidth * worldheight * worlddepth];

                                        for (int x = 0; x < worldwidth; x++)
                                        {
                                            for (int y = 0; y < worldheight; y++)
                                            {
                                                for (int z = 0; z < worlddepth; z++)
                                                {
                                                    var indexer01 = x + worldwidth * (y + worldheight * z);
                                                    sccsjittertasks[indexer00][indexer01] = new scmessageobjectjitter();
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            //Console.WriteLine("built0");
                            //jitter_sc = create_jitter_instances(jitter_sc, _sc_jitter_data);

                            for (int xx = 0; xx < physicsengineinstancex; xx++)
                            {
                                for (int yy = 0; yy < physicsengineinstancey; yy++)
                                {
                                    for (int zz = 0; zz < physicsengineinstancez; zz++)
                                    {
                                        var indexer00 = xx + physicsengineinstancex * (yy + physicsengineinstancey * zz);


                                        //if (jitter_sc.Length > 0)
                                        //{
                                        //    Console.WriteLine("built00");
                                        //}
                                        //
                                        //Console.WriteLine("index: " + indexer00);
                                        jitter_sc[indexer00]._sc_create_jitter_world(_sc_jitter_data);


                                        for (int x = 0; x < worldwidth; x++)
                                        {
                                            for (int y = 0; y < worldheight; y++)
                                            {
                                                for (int z = 0; z < worlddepth; z++)
                                                {
                                                    var indexer1 = x + worldwidth * (y + worldheight * z);

                                                    var world = jitter_sc[indexer00].return_world(indexer1);

                                                    if (world == null)
                                                    {
                                                        Console.WriteLine("null");
                                                    }
                                                    else
                                                    {
                                                        //Console.WriteLine("!null");

                                                        sccsjittertasks[indexer00][indexer1]._world_data = new object[2];
                                                        sccsjittertasks[indexer00][indexer1]._work_index = -1;
                                                        sccsjittertasks[indexer00][indexer1]._world_data[0] = world;
                                                        //Console.WriteLine("index: " + indexer1);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            //, consoleHandle
                            sccsjittertasks = updatescript.init_update_variables(sccsjittertasks, config); //, SCGLOBALSACCESSORS.SCCONSOLEWRITER
                        }
                        else if (usejitterphysics == 0)
                        {
                            sccsjittertasks = new scmessageobjectjitter[1][];
                            sccsjittertasks[0] = new scmessageobjectjitter[1];
                            sccsjittertasks[0][0] = new scmessageobjectjitter();
                            /*for (int xx = 0; xx < physicsengineinstancex; xx++)
                            {
                                for (int yy = 0; yy < physicsengineinstancey; yy++)
                                {
                                    for (int zz = 0; zz < physicsengineinstancez; zz++)
                                    {
                                        var indexer00 = xx + physicsengineinstancex * (yy + physicsengineinstancey * zz);
                                        //_jitter_physics[indexer00] = DoSpecialThing();
                                        sccsjittertasks[indexer00] = new scmessageobjectjitter[worldwidth * worldheight * worlddepth];

                                        for (int x = 0; x < worldwidth; x++)
                                        {
                                            for (int y = 0; y < worldheight; y++)
                                            {
                                                for (int z = 0; z < worlddepth; z++)
                                                {
                                                    var indexer01 = x + worldwidth * (y + worldheight * z);
                                                    sccsjittertasks[indexer00][indexer01] = new scmessageobjectjitter();
                                                }
                                            }
                                        }
                                    }
                                }
                            }*/

                            //, consoleHandle
                            updatescript.init_update_variables(sccsjittertasks, config); //, SCGLOBALSACCESSORS.SCCONSOLEWRITER
                        }

                        //sccsjittertasks[0][0].device = updatescript.device;


                        var swapChain1 = updatescript.SwapChain.QueryInterface<SwapChain1>();
                        // ignore all Windows events
                        factoryy = swapChain1.GetParent<SharpDX.DXGI.Factory>();
                        factoryy.MakeWindowAssociation(MainWindow.consoleHandle, WindowAssociationFlags.IgnoreAll);



                        //MessageBox((IntPtr)0, "initform 1", "scmsg", 0);
                        initform = 2;
                    }



                    if (initform == 2)
                    {
                        //MessageBox((IntPtr)0, "initform 2 ", "scmsg", 0);


                        if (!updatescript._captureMethod.IsCapturing)
                        {
                            updatescript._captureMethod.StartCapture(MainWindow.consoleHandle, updatescript.device, factoryy);
                            //MessageBox((IntPtr)0, "_thread_looper1", "scmsg", 0);
                        }

                        /*var refreshDXEngineAction = new Action(delegate
                        {
                            //MessageBox((IntPtr)0, "_thread_looper0", "scmsg", 0);
                            // ReSharper disable AccessToDisposedClosure

                            if (!updatescript._captureMethod.IsCapturing)
                            {
                                updatescript._captureMethod.StartCapture(MainWindow.consoleHandle, updatescript.device, factoryy);
                                //MessageBox((IntPtr)0, "_thread_looper1", "scmsg", 0);
                            }

                        });

                        Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                        */



                        //MessageBox((IntPtr)0, "_thread_looper2", "scmsg", 0);

                        /*
                        if (isResized)
                        {
                            Utilities.Dispose(ref BackBuffer);
                            Utilities.Dispose(ref _renderTargetView);

                            swapChain1.ResizeBuffers(swapChainDesc.BufferCount, SurfaceWidth, SurfaceHeight, Format.Unknown, SwapChainFlags.None);
                            BackBuffer = SharpDX.Direct3D11.Resource.FromSwapChain<Texture2D>(swapChain1, 0);
                            _renderTargetView = new RenderTargetView(device, BackBuffer);

                            device.ImmediateContext.Rasterizer.SetViewport(0, 0, SurfaceWidth, SurfaceHeight);
                            device.ImmediateContext.OutputMerger.SetTargets(_renderTargetView);

                            isResized = false;
                        }

                        // clear view
                        //device.ImmediateContext.ClearRenderTargetView(_renderTargetView, SharpDX.Color.Black);// new SharpDX.RawColor4(1.0f, 1.0f, 1.0f, 1.0f));

                        //using ()
                        {

                        */




                        texture2d = updatescript._captureMethod.TryGetNextFrameAsTexture2D(updatescript.device);


                        if (texture2d != null)
                        {
                            if (somecounter == 0)
                            {
                                _textureDescription = new Texture2DDescription
                                {
                                    CpuAccessFlags = CpuAccessFlags.Read,
                                    BindFlags = BindFlags.None,// ShaderResource | BindFlags.RenderTarget,
                                    Format = Format.B8G8R8A8_UNorm,
                                    Width = texture2d.Description.Width,
                                    Height = texture2d.Description.Height,
                                    OptionFlags = ResourceOptionFlags.None,
                                    MipLevels = 1,
                                    ArraySize = 1,
                                    SampleDescription = { Count = 1, Quality = 0 },
                                    Usage = ResourceUsage.Staging
                                };
                                _texture2D = new Texture2D(updatescript.device, _textureDescription);



                                _bitmap = new System.Drawing.Bitmap(_texture2D.Description.Width, _texture2D.Description.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                                boundsRect = new System.Drawing.Rectangle(0, 0, _texture2D.Description.Width, _texture2D.Description.Height);
                                bmpData = _bitmap.LockBits(boundsRect, System.Drawing.Imaging.ImageLockMode.ReadOnly, _bitmap.PixelFormat);
                                _bytesTotal = Math.Abs(bmpData.Stride) * _bitmap.Height;
                                _bitmap.UnlockBits(bmpData);
                                _textureByteArray = new byte[_bytesTotal];

                                somecounter = 1;
                            }


                            updatescript.device.ImmediateContext.CopyResource(texture2d, _texture2D);
                        }



                        /*
                        var refreshDXEngineAction = new Action(delegate
                        {
                            


                        });

                        Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                        */







                        if (texture2d != null)
                        {


                            // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (_width * 4)
                            // Stride not the same - copy line by line
                            // If buffers have same size, than we can copy it directly

                            /* IntPtr someptr = Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0);

                             if (dataBox1.RowPitch == memoryBitmapStride) // memoryBitmapStride)
                             {
                                 Utilities.CopyMemory(interptr1, someptr, memoryBitmapStride);
                             }
                             else
                             {


                                 srcPointer = (byte*)dataBox1.DataPointer;
                                 dstPointer = (byte*)someptr;
                                 var rowStride = Math.Min(dataBox1.RowPitch, memoryBitmapStride);

                                 // Copy per scanline
                                 for (int i = 0; i < rows; i++)
                                 {
                                     Utilities.CopyMemory(new IntPtr(dstPointer), new IntPtr(srcPointer), rowStride);
                                     srcPointer += dataBox1.RowPitch;
                                     dstPointer += memoryBitmapStride;
                                 }
                             }

                             Marshal.Copy(someptr, _textureByteArray, 0, _bytesTotal);

                             updatescript.device.ImmediateContext.UnmapSubresource(_texture2D, 0);
                             DeleteObject(interptr1);
                             DeleteObject((IntPtr)dstPointer);
                             DeleteObject((IntPtr)srcPointer);
                            */


                            /*
                            if (this.BufferStride == pixelBuffer.BufferStride)
                            {
                                Utilities.CopyMemory(pixelBuffer.DataPointer, this.DataPointer, this.BufferStride);
                            }
                            else
                            {
                                var srcPointer = (byte*)this.DataPointer;
                                var dstPointer = (byte*)pixelBuffer.DataPointer;
                                var rowStride = Math.Min(RowStride, pixelBuffer.RowStride);

                                // Copy per scanline
                                for (int i = 0; i < Height; i++)
                                {
                                    Utilities.CopyMemory(new IntPtr(dstPointer), new IntPtr(srcPointer), rowStride);
                                    srcPointer += this.RowStride;
                                    dstPointer += pixelBuffer.RowStride;
                                }
                            }*/



                            //DISCARDED
                            //DISCARDED
                            //DISCARDED
                            var dataBox1 = updatescript.device.ImmediateContext.MapSubresource(_texture2D, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                            int memoryBitmapStride = _textureDescription.Width * 4;
                            //8801024
                            //MessageBox((IntPtr)0, "fail " + memoryBitmapStride, "scmsg", 0); 
                            //int memoryBitmapStridey = _textureDescription.Height * 4;
                            int columns = _textureDescription.Width;
                            int rows = _textureDescription.Height;
                            IntPtr interptr1 = dataBox1.DataPointer;

                            if (dataBox1.RowPitch == memoryBitmapStride)
                            {
                                Marshal.Copy(interptr1, _textureByteArray, 0, _bytesTotal);
                                //Utilities.CopyMemory(interptr1, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0), _textureByteArray.Length);
                                // Stride not the same - copy line by line
                                // Stride is the same
                                //MessageBox((IntPtr)0, "fail0", "scmsg", 0);
                            }
                            else
                            {
                                //7704 // memorymapstride 4*1920
                                //7936 // databox.rowpitch
                                //8801024 // databox.slicepitch


                                //var rowStride = Math.Min(dataBox1.RowPitch, memoryBitmapStride);
                                //_textureByteArray = new byte[rowStride * rows];
                                //MessageBox((IntPtr)0, "fail " + memoryBitmapStride + " " + rowStride + " " + dataBox1.RowPitch + " " + dataBox1.SlicePitch, "scmsg", 0);

                                //Utilities.CopyMemory(interptr1, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0), _textureByteArray.Length);
                                for (int y = 0; y < rows; y++)
                                {
                                    Marshal.Copy(interptr1 + y * dataBox1.RowPitch, _textureByteArray, y * memoryBitmapStride, memoryBitmapStride);
                                    //Utilities.CopyMemory(interptr1 + y , Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0)+y, memoryBitmapStride);
                                }

                                //MessageBox((IntPtr)0, "fail1", "scmsg", 0);
                                //Marshal.Copy(interptr1, _textureByteArray, 0, _bytesTotal);
                            }


                            //var somebitmap = new System.Drawing.Bitmap(columns, rows, columns * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray,0));
                            //somebitmap.Save(@"C:\Users\steve\Desktop\screenrecord\" + bitmapcounter + "_" + rows.ToString("00") + columns.ToString("00") + ".png");
                            //bitmapcounter++;
                            updatescript.device.ImmediateContext.UnmapSubresource(_texture2D, 0);
                            DeleteObject(interptr1);
                            //DISCARDED
                            //DISCARDED
                            //DISCARDED



                            //updatescript.device.ImmediateContext.CopyResource(texture2d, _texture2D);
                            shaderResourceView = new ShaderResourceView(updatescript.device, texture2d);


                            if (lastshaderresourceview != null)
                            {
                                lastshaderresourceview.Dispose();
                                lastshaderresourceview = null;
                            }
                            lastshaderresourceview = shaderResourceView;


















                            //device.ImmediateContext.PixelShader.SetShaderResource(0, shaderResourceView);
                            //device.ImmediateContext.Draw(4, 0);
                            //swapChain1.Present(1, PresentFlags.None, new PresentParameters());




                            if (sccsjittertasks != null)
                            {
                                if (sccsjittertasks[0] != null)
                                {
                                    if (sccsjittertasks[0].Length > 0)
                                    {
                                        sccsjittertasks[0][0].frameByteArray = _textureByteArray;
                                        sccsjittertasks[0][0].shaderresource = shaderResourceView;
                                    }
                                }
                            }





                            if (exitedprogram != 1)
                            {
                                if (usejitterphysics == 0)
                                {
                                    // Clear the depth buffer.
                                    //DeviceContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth, 1, 0);
                                    // Clear the back buffer.
                                    //DeviceContext.ClearRenderTargetView(_renderTargetView, givenColour);


                                    sccsjittertasks = updatescript.Update(null, sccsjittertasks);
                                }
                                else if (usejitterphysics == 1)
                                {
                                    // Clear the depth buffer.
                                    //DeviceContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth, 1, 0);
                                    // Clear the back buffer.
                                    //DeviceContext.ClearRenderTargetView(_renderTargetView, givenColour);


                                    sccsjittertasks = updatescript.Update(jitter_sc, sccsjittertasks);
                                }

                            }


                        }
                        else
                        {

                        }
                    }
















                    Thread.Sleep(1);
                    //MessageBox((IntPtr)0, "threadm0", "scmsg", 0);
                    goto _thread_main_loop;
                    //MessageBox((IntPtr)0, "threadm -1", "scmsg", 0);
                    //_thread_start:
                }, 0); //100000 //999999999

                _mainThread.IsBackground = true;
                _mainThread.Priority = ThreadPriority.Normal; //AboveNormal
                _mainThread.SetApartmentState(ApartmentState.STA);
                _mainThread.Start();



                //MessageBox((IntPtr)0, "thread initiated", "scmsg", 0);
                initthread = 2;
            }


            if (initthread == 2)
            {
                if (initform == 0)
                {
                    //Close();
                    //FindWindows();
                    //MessageBox((IntPtr)0, "form initiated0", "scmsg", 0);
                    //System.Windows.Forms.Application.EnableVisualStyles();
                    //System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                    initform = 1;
                    //System.Windows.Forms.Application.Run(new Form1());

                    //MessageBox((IntPtr)0, "form initiated1", "scmsg", 0);

                }

                if (System.Diagnostics.Debugger.IsAttached)
                {
                    //MessageBox((IntPtr)0, "threadm1", "scmsg", 0);
                    Thread.Sleep(1);
                    goto mainthreadloop;
                }
                else
                {
                    //MessageBox((IntPtr)0, "threadm2", "scmsg", 0);
                    Thread.Sleep(1);
                    goto mainthreadloop;
                }
            }
            else if(initthread != -1)
            {
                //MessageBox((IntPtr)0, "threadm3", "scmsg", 0);
                //AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;


                Thread.Sleep(1);
                goto mainthreadloop;
            }


            MessageBox((IntPtr)0, "end of program", "scmsg", 0);
            //Console.WriteLine("end of program");
        }
        //public static RenderForm form;


        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);
    }
}
