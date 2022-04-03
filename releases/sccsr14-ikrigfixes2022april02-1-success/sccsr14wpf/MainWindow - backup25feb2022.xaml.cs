using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

using System.Runtime.InteropServices;

using sccs.scmessageobject;
using sccs.scconsole;

using sccs.scgraphics;
using System.ComponentModel;
using WindowsInput;

using sccs.sccore;

using Jitter.Dynamics;
using Jitter.DataStructures;
using Jitter;
using Jitter.Forces;
using Jitter.LinearMath;

using System.Diagnostics;
using System.Windows.Interop;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

using Win32.Shared.Interop;
using Win32.Shared;

using System.Windows.Controls;

using SharpDX;
using SharpDX.Windows;

namespace sccs
{
    public struct CapturableWindow
    {
        public string Name { get; set; }
        public IntPtr Handle { get; set; }
    }
    public partial class MainWindow : Window
    //public static class Program
    {
        public static int exitedMainWindow = -1;


        public static int _useOculusRift = 0;
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


        int initmainthread = 0;

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        public static IntPtr consoleHandle;
        public static scsystemconfiguration config;
        public static SharpDX.DirectInput.KeyboardState keyboardstate;
        public static keyboardinput keyboardinput;
        public static InputSimulator inputsim;
        public static KeyboardSimulator keyboardsim;
        public static MouseSimulator mousesim;


        public static RenderForm form;

        public static int initdirectXmainswtch = -1;
        public static int initvrmainswtch = 2;

        int has_init_directx = 0;

        static int MaxSizeMainObject = 16;
        public static int MaxSizeMessageObject = 32;
        Thread mainthread;

        static scupdate updatescript;
        static scmessageobject.scmessageobject[] mainreceivedmessages;//
        //static _messager[] secreceivedmessages;
        //static scconsole.scconsolereader._console_reader_data consolereaderstring;

        public static sccs.sccore.scglobalsaccessor SCGLOBALSACCESSORS;
        public static int processorCount = 0;
        public static int workerThreadsTotal;
        public static int portThreadsTotal;
        static string speechcaptured = "Speech Captured";
        static string msgenabled = "Speech Recognit ON";
        static string msgdisabled = "Speech Recognit OFF";
        public static string _program_name = "SC Core Systems";

        int _worker_000_has_init = 0;
        int _start_thread_console_writer = 1;
        int _console_reader_canWork = 1;
        int startThread = 0;
        int _counter_reset_console_borders = 0;
        int _lastMenu = -2;
        string _lastMenuOption = "";
        string _lastUsername = "";
        int _some_other_swtch = 0;
        int loop_main_thread = 0;
        //static scconsole.scconsolereader._console_reader_data _console_reader_string;
        static int _initX0 = 0;
        static int _initY0 = 0;
        static Task _console_worker_task;
        static Task _console_reader_task;
        static Task _console_writer_task;
        static scmessageobject.scmessageobject data00IN;
        static scmessageobject.scmessageobject data00OUT;



        /*
        private Process myProcess;
        private TaskCompletionSource<bool> eventHandled;

        public async Task PrintDpoc(string filename)
        {
            eventHandled = new TaskCompletionSource<bool>();

            using (myProcess = new Process())
            {
                try
                {
                    myProcess.StartInfo.FileName = filename;
                    myProcess.StartInfo.Verb = "Print";
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.EnableRaisingEvents = true;
                    myProcess.Exited += new EventHandler(myProcess_Exited);
                    myProcess.Start();
                }
                catch(Exception ex)
                {
                    MessageBox((IntPtr)0, ""+ ex.ToString(), "scmsg", 0);
                    return;
                }
                await Task.WhenAny(eventHandled.Task, Task.Delay(30000));
            }
        }



        private void myProcess_Exited(object sender, System.EventArgs e)
        {
            Console.WriteLine(
                $"Exit time : { myProcess.ExitTime}\n" +
                $"Exit code : { myProcess.ExitCode}\n" +
                $"Elapsed time : { Math.Round((myProcess.ExitTime - myProcess.StartTime).TotalMilliseconds)}\n");

            eventHandled.TrySetResult(true);
        }*/

        public static int exitedprogram = -1;

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
            }



            //updatescript.heightmapthread = null;
            //updatescript.scgraphicssecpackagemessage.scgraphicssec.
            scdirectx.D3D.ShutDown();


            MessageBox((IntPtr)0, "exiting", "scmsg", 0);
            //throw new NotImplementedException("program has exited");


        }


        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            FindWindows();
        }

        public IntPtr PickCaptureTarget(IntPtr hWnd)
        {
            new WindowInteropHelper(this).Owner = hWnd;
            ShowDialog();

            return ((CapturableWindow?)Windows.SelectedItem)?.Handle ?? IntPtr.Zero;
        }

        private void FindWindows()
        {
            var wih = new WindowInteropHelper(this);
            NativeMethods.EnumWindows((hWnd, lParam) =>
            {
                // ignore invisible windows
                if (!NativeMethods.IsWindowVisible(hWnd))
                    return true;

                // ignore untitled windows
                var title = new StringBuilder(1024);
                NativeMethods.GetWindowText(hWnd, title, title.Capacity);
                if (string.IsNullOrWhiteSpace(title.ToString()))
                    return true;

                // ignore me
                if (wih.Handle == hWnd)
                    return true;

                NativeMethods.GetWindowThreadProcessId(hWnd, out var processId);

                // ignore by process name
                var process = Process.GetProcessById((int)processId);
                if (_ignoreProcesses.Contains(process.ProcessName.ToLower()))
                    return true;

                Windows.Items.Add(new CapturableWindow
                {
                    Handle = hWnd,
                    Name = $"{title} ({process.ProcessName}.exe)"
                });

                return true;
            }, IntPtr.Zero);
        }

        private void WindowsOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private readonly string[] _ignoreProcesses = { "applicationframehost", "shellexperiencehost", "systemsettings", "winstore.app", "searchui" };

     // = new DwmSharedSurface(this);

        public static MainWindow mainwindow;

        public MainWindow() //string[] args
        {
            //mainwindow = this;
            //AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;

            /*if (args.Length <= 0)
            {
                Console.WriteLine("Enter a file name.");
                return;
            }

            
            await(PrintDpoc)*/
            InitializeComponent();
            Loaded += OnLoaded;

       

            form = new RenderForm("sccsr14");
            form.Size = new System.Drawing.Size(1920, 1080);
            form.CreateControl();
            form.Activate();
            form.TransparencyKey = System.Drawing.Color.Black;
            //form.Hide();

            consoleHandle = form.Handle;
            RenderLoop.Run(form, () =>
            {
                updatescript = new scupdate();
            });














            //DxWindow somewin = new DxWindow("test", somesurf);
            //somewin.Show();






            /*this.OnClosed += delegate (EventArgs args)
            {

            };*/

            /*
            this.Loaded += delegate
            {
                MessageBox((IntPtr)0, "exiting0", "scmsg", 0);
            };

            this.Closing += delegate (object sender, CancelEventArgs args)
            {
                MessageBox((IntPtr)0, "exiting1", "scmsg", 0);
            };*/



            /*var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (object sender, DoWorkEventArgs args) =>
            {
            _thread_loop:


                Thread.Sleep(1);
                goto _thread_loop;
            };

            /*backgroundWorker.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args)
            {

            };

            backgroundWorker.RunWorkerAsync();*/



            /*

            for (int j = 0; j < 1; j++)
            {
                try
                {
                    processorCount = Environment.ProcessorCount;// SC_SystemInfoSeeker.getSystemProcessorCount();
                }
                catch //(Exception ex)
                {
                    break;
                }
            }

            for (int j = 0; j < 1; j++)
            {
                try
                {
                    ThreadPool.GetMaxThreads(out workerThreadsTotal, out portThreadsTotal);
                    ThreadPool.GetAvailableThreads(out workerThreadsTotal, out portThreadsTotal);
                }
                catch
                {
                    break;
                }
            }
            */




            /*
            //SCGLOBALSACCESSORS AND GLOBALS CREATOR
            SCGLOBALSACCESSORS = new sccs.sccore.scglobalsaccessor(mainreceivedmessages);
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
            
            */





            /*
            ////////////////////
            ///KEYBOARD INPUT///
            ////////////////////
            for (int j = 0; j < 1; j++)
            {
                try
                {

                    keyboardinput = new scconsole.keyboardinput();
                    //keyboardstate = _keyboard_input._KeyboardState;

                    inputsim = new InputSimulator();
                    keyboardsim = new KeyboardSimulator(inputsim);
                    mousesim = new MouseSimulator(inputsim);

                }
                catch (Exception ex)
                {
                    MessageBox((IntPtr)0, "cannot get keyboard info main 00: " + ex.ToString() + "", "_sc_core_systems error", 0);
                    //something is wrong, todo something else later. but not implemented yet. maybe get raw input instead from SharpDX i dont know
                    break;
                }
            }


            for (int j = 0; j < 1; j++)
            {
                try
                {
                    if (keyboardinput != null)
                    {
                        keyboardinput._InitializeKeyboard();
                        keyboardinput._KeyboardState = new SharpDX.DirectInput.KeyboardState();
                    }
                    else
                    {
                        MessageBox((IntPtr)0, "cannot get keyboard info main 01: ", "_sc_core_systems error", 0);
                        //something is wrong, todo something else later. but not implemented yet. maybe get raw input instead from SharpDX i dont know
                    }
                }
                catch (Exception ex)
                {
                    MessageBox((IntPtr)0, "cannot get keyboard info main 02: " + ex.ToString() + "", "_sc_core_systems error", 0);
                    //something is wrong, todo something else later. but not implemented yet. maybe get raw input instead from SharpDX i dont know
                    break;
                }
            }
            ////////////////////
            ///KEYBOARD INPUT///
            ////////////////////
            **/





            /*
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
                mainreceivedmessages[i]._main_cpu_count = processorCount;
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
                /*if (i == MaxSizeMainObject - 1)
                {
                    mainreceivedmessages[i]._someData[0] = _keyboard_input._KeyboardState;
                    mainreceivedmessages[i]._voRecSwtc = 1;
                }
            }
            ///////////////////////////////
            ///////////////////////////////   
            ///////////////////////////////   
            ///////////////////////////////  
            ///message_thread_safe_kinda///   
            ///////////////////////////////   
            ///////////////////////////////   
            ///////////////////////////////
            */










            /*
            object _console_reader_object;
            secreceivedmessages = new _messager[MaxSizeMessageObject];



            for (int i = 0; i < secreceivedmessages.Length; i++)
            {
                secreceivedmessages[i]._swtch0 = -1;
            }



            secreceivedmessages[4]._messager_list = new _messager[MaxSizeMainObject];
            secreceivedmessages[4]._message = "";
            secreceivedmessages[4]._originalMsg = "";
            secreceivedmessages[4]._messageCut = "";
            secreceivedmessages[4]._specialMessage = -1;
            secreceivedmessages[4]._specialMessageLineX = 0;
            secreceivedmessages[4]._specialMessageLineY = 0;
            secreceivedmessages[4]._orilineX = 0;
            secreceivedmessages[4]._orilineY = 0;
            secreceivedmessages[4]._lineX = 0;
            secreceivedmessages[4]._lineY = 0;
            secreceivedmessages[4]._lastOrilineX = 0;
            secreceivedmessages[4]._lastOrilineY = 0;
            secreceivedmessages[4]._count = 0;
            secreceivedmessages[4]._swtch0 = 1;
            secreceivedmessages[4]._swtch1 = 1;
            secreceivedmessages[4]._delay = 50;
            secreceivedmessages[4]._looping = 1;

            consolereaderstring._has_message_to_display = 0;
            consolereaderstring._console_reader_message = "";
            consolereaderstring._has_init = 0;
            _console_reader_object = consolereaderstring;

            */
            //FOR SPEECH RECOGNITION
            //secreceivedmessages[15]._swtch0 = 1;




            /*
            //Console.WriteLine("test");

            secreceivedmessages[12]._message = msgdisabled;
            secreceivedmessages[12]._originalMsg = msgdisabled;
            secreceivedmessages[12]._messageCut = msgdisabled;
            secreceivedmessages[12]._specialMessage = 3;
            secreceivedmessages[12]._specialMessageLineX = 0;
            secreceivedmessages[12]._specialMessageLineY = 0;
            secreceivedmessages[12]._orilineX = Console.WindowWidth - 3 - msgdisabled.Length;
            secreceivedmessages[12]._orilineY = 1;
            secreceivedmessages[12]._lineX = secreceivedmessages[12]._orilineX;
            secreceivedmessages[12]._lineY = secreceivedmessages[12]._orilineY;
            secreceivedmessages[12]._count = 0;
            secreceivedmessages[12]._swtch0 = 1;
            secreceivedmessages[12]._swtch1 = 1;
            secreceivedmessages[12]._delay = 11;
            secreceivedmessages[12]._looping = 1;

            //Console.WriteLine("test");

            secreceivedmessages[13]._message = msgenabled;
            secreceivedmessages[13]._originalMsg = msgenabled;
            secreceivedmessages[13]._messageCut = msgenabled;
            secreceivedmessages[13]._specialMessage = 3;
            secreceivedmessages[13]._specialMessageLineX = 0;
            secreceivedmessages[13]._specialMessageLineY = 0;
            secreceivedmessages[13]._orilineX = Console.WindowWidth - 3 - msgdisabled.Length;
            secreceivedmessages[13]._orilineY = 0;
            secreceivedmessages[13]._lineX = secreceivedmessages[13]._orilineX;
            secreceivedmessages[13]._lineY = secreceivedmessages[13]._orilineY;
            secreceivedmessages[13]._count = 0;
            secreceivedmessages[13]._swtch0 = 1;
            secreceivedmessages[13]._swtch1 = 1;
            secreceivedmessages[13]._delay = 11;
            secreceivedmessages[13]._looping = 1;

            secreceivedmessages[10]._message = speechcaptured;
            secreceivedmessages[10]._originalMsg = speechcaptured;
            secreceivedmessages[10]._messageCut = speechcaptured;
            secreceivedmessages[10]._specialMessage = 3;
            secreceivedmessages[10]._specialMessageLineX = 0;
            secreceivedmessages[10]._specialMessageLineY = 0;
            secreceivedmessages[10]._orilineX = Console.WindowWidth - 3 - msgdisabled.Length;
            secreceivedmessages[10]._orilineY = 2;
            secreceivedmessages[10]._lineX = secreceivedmessages[10]._orilineX;
            secreceivedmessages[10]._lineY = secreceivedmessages[10]._orilineY;
            secreceivedmessages[10]._count = 0;
            secreceivedmessages[10]._swtch0 = 1;
            secreceivedmessages[10]._swtch1 = 1;
            secreceivedmessages[10]._delay = 11;
            secreceivedmessages[10]._looping = 1;*/










            if (has_init_directx == 0)
            {
                //MessageBox((IntPtr)0, "has_init_directx", "scmsg", 0);
                config = new scsystemconfiguration("sc core systems", 1920, 1080, false, false);
                //consoleHandle = scconsolecore.handle;

                // Application.Current.MainWindow;
                //MessageBox((IntPtr)0, "" + Application.Current.Windows.Count, "scmsg", 0);

                /*var enumwin = Application.Current.Windows.GetEnumerator();

                 while (enumwin.MoveNext())
                 {
                     consoleHandle = (IntPtr)enumwin.Current;

                 }*/

                /*
                var somewindow = new WindowInteropHelper(Application.Current.MainWindow);
                consoleHandle = somewindow.EnsureHandle();



                //consoleHandle = somewindow.Handle;
                //this.RootGrid.Parent
                //consoleHandle = Process.GetCurrentProcess().MainWindowHandle;

                    */

                //updatescript = new scupdate();
                has_init_directx = 1;
            }



            /*form = new RenderForm("sccsr14");
            /*form.Size = new System.Drawing.Size(1920, 1080);
            form.CreateControl();
            form.Activate();
            form.TransparencyKey = System.Drawing.Color.Black;
            //form.Hide();
            consoleHandle = form.Handle;
       
            updatescript = new scupdate();*/
 

            /*
            RenderLoop.Run(form, () =>
            {
                // ReSharper disable AccessToDisposedClosure
                if (!_captureMethod.IsCapturing)
                    _captureMethod.StartCapture(form.Handle, device, factory);

                if (isResized)
                {
                    Utilities.Dispose(ref backBuffer);
                    Utilities.Dispose(ref renderView);

                    swapChain1.ResizeBuffers(swapChainDescription.BufferCount, form.ClientSize.Width, form.ClientSize.Height, Format.Unknown, SwapChainFlags.None);
                    backBuffer = Resource.FromSwapChain<Texture2D>(swapChain1, 0);
                    renderView = new RenderTargetView(device, backBuffer);

                    device.ImmediateContext.Rasterizer.SetViewport(0, 0, form.ClientSize.Width, form.ClientSize.Height);
                    device.ImmediateContext.OutputMerger.SetTargets(renderView);

                    isResized = false;
                }

                // clear view
                device.ImmediateContext.ClearRenderTargetView(renderView, new RawColor4(1.0f, 1.0f, 1.0f, 1.0f));

                using var texture2d = _captureMethod.TryGetNextFrameAsTexture2D(device);
                if (texture2d != null)
                {
                    using var shaderResourceView = new ShaderResourceView(device, texture2d);
                    device.ImmediateContext.PixelShader.SetShaderResource(0, shaderResourceView);
                }

                // draw it
                device.ImmediateContext.Draw(4, 0);
                swapChain1.Present(1, PresentFlags.None, new PresentParameters());

                // ReSharper restore AccessToDisposedClosure

            });*/






            if (initmainthread == -5)
            {

                mainthread = new Thread((tester0000) =>
                {




                mainthreadloop:



                    /*if (Process.GetCurrentProcess().HasExited)
                    {
                        MessageBox((IntPtr)0, "exited", "scmsg", 0);
                    }*/








                    keyboardinput.ReadKeyboard();

                    /*if (_start_thread_console_writer == 1)
                    {
                        _console_writer_task = Task<object[]>.Factory.StartNew((tester0001) =>
                        {
                            var _initX = (Console.WindowWidth / 2) - (_program_name.Length / 2);
                            var _initY = (Console.WindowHeight / 2);

                            secreceivedmessages[0]._message = _program_name;
                            secreceivedmessages[0]._originalMsg = _program_name;
                            secreceivedmessages[0]._messageCut = _program_name;
                            secreceivedmessages[0]._specialMessage = 0;
                            secreceivedmessages[0]._specialMessageLineX = 0;
                            secreceivedmessages[0]._specialMessageLineY = 0;
                            secreceivedmessages[0]._orilineX = _initX;
                            secreceivedmessages[0]._orilineY = _initY;
                            secreceivedmessages[0]._lineX = _initX;
                            secreceivedmessages[0]._lineY = _initY;
                            secreceivedmessages[0]._lastOrilineX = _initX;
                            secreceivedmessages[0]._lastOrilineY = _initY;
                            secreceivedmessages[0]._count = 0;
                            secreceivedmessages[0]._swtch0 = 1;
                            secreceivedmessages[0]._swtch1 = 1;
                            secreceivedmessages[0]._delay = 5;
                            secreceivedmessages[0]._looping = 1;

                            _worker_000_has_init = 1;

                        //////CONSOLE WRITER=>
                        _thread_loop_console:

                            secreceivedmessages = SCGLOBALSACCESSORS.SCCONSOLEWRITER._console_writer(secreceivedmessages);

                            Thread.Sleep(1);

                            goto _thread_loop_console;
                            //////CONSOLE WRITER <=
                        }, mainreceivedmessages);
                        _start_thread_console_writer = 2;
                    }

                    //CONFIRM CONSOLE WRITER IS WORKING=>
                    if (_worker_000_has_init == 1)
                    {
                        if (SCGLOBALSACCESSORS != null)
                        {
                            if (SCGLOBALSACCESSORS.SCCONSOLEWRITER != null)
                            {
                                /*secreceivedmessages[1]._message = "C-WR" + " ENABLED";
                                secreceivedmessages[1]._originalMsg = "C-WR" + " ENABLED";
                                secreceivedmessages[1]._messageCut = "C-WR" + " ENABLED";
                                secreceivedmessages[1]._specialMessage = 2;
                                secreceivedmessages[1]._specialMessageLineX = 0;
                                secreceivedmessages[1]._specialMessageLineY = 0;
                                secreceivedmessages[1]._orilineX = 1;
                                secreceivedmessages[1]._orilineY = Console.WindowHeight - 2;
                                secreceivedmessages[1]._lineX = 1;
                                secreceivedmessages[1]._lineY = Console.WindowHeight - 2;
                                secreceivedmessages[1]._lastOrilineX = secreceivedmessages[1]._lineX;
                                secreceivedmessages[1]._lastOrilineY = secreceivedmessages[1]._lineY;
                                secreceivedmessages[1]._count = 0;
                                secreceivedmessages[1]._swtch0 = 1;
                                secreceivedmessages[1]._swtch1 = 0;
                                secreceivedmessages[1]._delay = 10;
                                secreceivedmessages[1]._looping = 0;

                                _worker_000_has_init = 2;
                            }
                        }
                    }
                    //CONFIRM CONSOLE WRITER IS WORKING<=

                    //CONFIRM CONSOLE READER IS WORKING=>
                    if (_worker_000_has_init == 2)
                    {
                        if (SCGLOBALSACCESSORS != null)
                        {
                            if (SCGLOBALSACCESSORS.SCCONSOLEREADER != null)
                            {
                                /*secreceivedmessages[2]._message = "core C-RE" + " ENABLED";
                                secreceivedmessages[2]._originalMsg = "core C-RE" + " ENABLED";
                                secreceivedmessages[2]._messageCut = "core C-RE" + " ENABLED";
                                secreceivedmessages[2]._specialMessage = 2;
                                secreceivedmessages[2]._specialMessageLineX = 0;
                                secreceivedmessages[2]._specialMessageLineY = 1;
                                secreceivedmessages[2]._orilineX = secreceivedmessages[2]._message.Length + 3;
                                secreceivedmessages[2]._orilineY = Console.WindowHeight - 2;
                                secreceivedmessages[2]._lineX = secreceivedmessages[2]._message.Length + 3;
                                secreceivedmessages[2]._lineY = Console.WindowHeight - 2;
                                secreceivedmessages[2]._count = 0;
                                secreceivedmessages[2]._swtch0 = 1;
                                secreceivedmessages[2]._swtch1 = 0;
                                secreceivedmessages[2]._delay = 10;
                                secreceivedmessages[2]._looping = 0;

                                _worker_000_has_init = 3;
                            }
                        }
                    }
                    //CONFIRM CONSOLE READER IS WORKING<=





                    if (_worker_000_has_init == 3)
                    {
                        if (SCGLOBALSACCESSORS != null)
                        {
                            if (SCGLOBALSACCESSORS.SCCONSOLEREADER != null)
                            {
                                var _program_name0 = "Press Enter";
                                _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                _initY0 = (Console.WindowHeight / 2) + 1;

                                secreceivedmessages[3]._message = _program_name0;
                                secreceivedmessages[3]._originalMsg = _program_name0;
                                secreceivedmessages[3]._messageCut = _program_name0;
                                secreceivedmessages[3]._specialMessage = 2;
                                secreceivedmessages[3]._specialMessageLineX = 0;
                                secreceivedmessages[3]._specialMessageLineY = 0;
                                secreceivedmessages[3]._orilineX = _initX0;
                                secreceivedmessages[3]._orilineY = _initY0;
                                secreceivedmessages[3]._lineX = _initX0;
                                secreceivedmessages[3]._lineY = _initY0;
                                secreceivedmessages[3]._count = 0;
                                secreceivedmessages[3]._swtch0 = 1;
                                secreceivedmessages[3]._swtch1 = 1;
                                secreceivedmessages[3]._delay = 100;
                                secreceivedmessages[3]._looping = 1;
                                _worker_000_has_init = 4;
                            }
                        }
                    }*/


                    /*
                    if (_worker_000_has_init == 4)
                    {

                        _console_reader_task = Task<object[]>.Factory.StartNew((tester0001) =>
                        {
                            while (true)
                            {
                                if (_console_reader_canWork == 1)
                                {
                                    _console_reader_string = SCGLOBALSACCESSORS.SCCONSOLEREADER._console_reader(_console_reader_object);
                                }

                                if (SCGLOBALSACCESSORS.SCCONSOLEREADER._main_has_init == 1)
                                {
                                    _console_reader_string._console_reader_message = "";
                                    _console_reader_string._has_message_to_display = 0;

                                    var _program_name0 = "WELCOME";
                                    _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                    _initY0 = (Console.WindowHeight / 2) - 1;

                                    secreceivedmessages[4]._message = _program_name0;
                                    secreceivedmessages[4]._originalMsg = _program_name0;
                                    secreceivedmessages[4]._messageCut = _program_name0;
                                    secreceivedmessages[4]._specialMessage = 2;
                                    secreceivedmessages[4]._specialMessageLineX = 0;
                                    secreceivedmessages[4]._specialMessageLineY = 0;
                                    secreceivedmessages[4]._orilineX = _initX0;
                                    secreceivedmessages[4]._orilineY = _initY0;
                                    secreceivedmessages[4]._lineX = _initX0;
                                    secreceivedmessages[4]._lineY = _initY0;
                                    secreceivedmessages[4]._count = 0;
                                    secreceivedmessages[4]._swtch0 = 1;
                                    secreceivedmessages[4]._swtch1 = 0;
                                    secreceivedmessages[4]._delay = 200;
                                    secreceivedmessages[4]._looping = 0;

                                    secreceivedmessages[0]._swtch0 = 0;
                                    secreceivedmessages[0]._swtch1 = 0;
                                    secreceivedmessages[3]._swtch0 = 0;
                                    secreceivedmessages[3]._swtch1 = 0;

                                    _program_name0 = "Please Enter your Username: ";
                                    _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                    _initY0 = (Console.WindowHeight / 2) + 2;

                                    secreceivedmessages[6]._message = _program_name0;
                                    secreceivedmessages[6]._originalMsg = _program_name0;
                                    secreceivedmessages[6]._messageCut = _program_name0;
                                    secreceivedmessages[6]._specialMessage = 2;
                                    secreceivedmessages[6]._specialMessageLineX = 0;
                                    secreceivedmessages[6]._specialMessageLineY = 0;
                                    secreceivedmessages[6]._orilineX = _initX0;
                                    secreceivedmessages[6]._orilineY = _initY0;
                                    secreceivedmessages[6]._lineX = _initX0;
                                    secreceivedmessages[6]._lineY = _initY0;
                                    secreceivedmessages[6]._count = 0;
                                    secreceivedmessages[6]._swtch0 = 1;
                                    secreceivedmessages[6]._swtch1 = 1;
                                    secreceivedmessages[6]._delay = 50;
                                    secreceivedmessages[6]._looping = 1;

                                    Console.SetCursorPosition(_initX0 + _program_name0.Length, _initY0);

                                    startThread = 3;
                                    SCGLOBALSACCESSORS.SCCONSOLEREADER._main_has_init = 2;
                                }

                                if (startThread == 3 && _console_reader_string._has_message_to_display == 1)
                                {

                                    if (_console_reader_string._console_reader_message.ToLower() == "nine" || _console_reader_string._console_reader_message.ToLower() == "ninekorn" || _console_reader_string._console_reader_message.ToLower() == "9")
                                    {

                                        var _program_name0 = "Access Authorized";
                                        _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                        _initY0 = (Console.WindowHeight / 2) + 2;

                                        secreceivedmessages[6]._message = _program_name0;
                                        secreceivedmessages[6]._originalMsg = _program_name0;
                                        secreceivedmessages[6]._messageCut = _program_name0;
                                        secreceivedmessages[6]._specialMessage = 2;
                                        secreceivedmessages[6]._specialMessageLineX = 0;
                                        secreceivedmessages[6]._specialMessageLineY = 0;
                                        secreceivedmessages[6]._lineX = _initX0;
                                        secreceivedmessages[6]._lineY = _initY0;
                                        secreceivedmessages[6]._count = 0;
                                        secreceivedmessages[6]._swtch0 = 1;
                                        secreceivedmessages[6]._swtch1 = 0;
                                        secreceivedmessages[6]._delay = 50;
                                        secreceivedmessages[6]._looping = 0;

                                        for (int L0_IN = 0; L0_IN < mainreceivedmessages.Length; L0_IN++)
                                        {
                                            mainreceivedmessages[L0_IN]._passTest = _console_reader_string._console_reader_message.ToLower();
                                        }
                                        Console.SetCursorPosition(_initX0, _initY0 + 1);
                                        _lastUsername = _console_reader_string._console_reader_message;
                                        _console_reader_string._console_reader_message = "";
                                        startThread = 4;
                                    }
                                    else if (_console_reader_string._console_reader_message.ToLower() != " " || _console_reader_string._console_reader_message.ToLower() != "")
                                    {

                                        var _program_name0 = "Access Denied";
                                        _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                        _initY0 = (Console.WindowHeight / 2) + 2;

                                        secreceivedmessages[6]._message = _program_name0;
                                        secreceivedmessages[6]._originalMsg = _program_name0;
                                        secreceivedmessages[6]._messageCut = _program_name0;
                                        secreceivedmessages[6]._specialMessage = 2;
                                        secreceivedmessages[6]._specialMessageLineX = 0;
                                        secreceivedmessages[6]._specialMessageLineY = 0;
                                        secreceivedmessages[6]._lineX = _initX0;
                                        secreceivedmessages[6]._lineY = _initY0;
                                        secreceivedmessages[6]._count = 0;
                                        secreceivedmessages[6]._swtch0 = 1;
                                        secreceivedmessages[6]._swtch1 = 0;
                                        secreceivedmessages[6]._delay = 50;
                                        secreceivedmessages[6]._looping = 0;

                                        _lastUsername = "";
                                        _console_reader_string._console_reader_message = "";
                                        Console.SetCursorPosition(_initX0, _initY0);
                                        startThread = 3;
                                    }
                                }
                                else if (startThread == 4)
                                {
                                    if (_console_reader_string._console_reader_message.ToLower() == "vr" ||
                                         _console_reader_string._console_reader_message.ToLower() == "standard" ||
                                          _console_reader_string._console_reader_message.ToLower() == "std")
                                    {
                                        if (_console_reader_string._console_reader_message.ToLower() == "vr")
                                        {

                                            var _program_name0 = "creating VR mecanics";
                                            _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                            _initY0 = (Console.WindowHeight / 2) + 2;

                                            secreceivedmessages[6]._message = _program_name0;
                                            secreceivedmessages[6]._originalMsg = _program_name0;
                                            secreceivedmessages[6]._messageCut = _program_name0;
                                            secreceivedmessages[6]._specialMessage = 2;
                                            secreceivedmessages[6]._specialMessageLineX = 0;
                                            secreceivedmessages[6]._specialMessageLineY = 0;
                                            secreceivedmessages[6]._lineX = _initX0;
                                            secreceivedmessages[6]._lineY = _initY0;
                                            secreceivedmessages[6]._count = 0;
                                            secreceivedmessages[6]._swtch0 = 1;
                                            secreceivedmessages[6]._swtch1 = 0;
                                            secreceivedmessages[6]._delay = 50;
                                            secreceivedmessages[6]._looping = 0;


                                            _lastMenuOption = _console_reader_string._console_reader_message.ToLower();
                                            _console_reader_string._console_reader_message = "";

                                            mainreceivedmessages[0]._received_switch_in = 1;
                                            mainreceivedmessages[0]._received_switch_out = 1;
                                            mainreceivedmessages[0]._sending_switch_in = 1;
                                            mainreceivedmessages[0]._sending_switch_out = 1;
                                            mainreceivedmessages[0]._welcomePackage = 999;

                                            mainreceivedmessages = sc_console_menu.sc_console_menu_00._console_menu(mainreceivedmessages);
                                            Console.SetCursorPosition(_initX0, _initY0 + 1);
                                            _some_other_swtch = 1;
                                        }
                                        else if (_console_reader_string._console_reader_message.ToLower() == "standard" ||
                                                _console_reader_string._console_reader_message.ToLower() == "std")
                                        {

                                            _lastMenuOption = _console_reader_string._console_reader_message.ToLower();
                                            _console_reader_string._console_reader_message = "";

                                            mainreceivedmessages[0]._received_switch_in = 1;
                                            mainreceivedmessages[0]._received_switch_out = 1;
                                            mainreceivedmessages[0]._sending_switch_in = 1;
                                            mainreceivedmessages[0]._sending_switch_out = 1;
                                            mainreceivedmessages[0]._welcomePackage = 998;
                                            mainreceivedmessages = sc_console_menu.sc_console_menu_00._console_menu(mainreceivedmessages);
                                            Console.SetCursorPosition(_initX0, _initY0 + 1);
                                            _some_other_swtch = 1;
                                        }
                                    }
                                    else
                                    {

                                        var _program_name0 = "Option Not Implemented";
                                        _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                        _initY0 = (Console.WindowHeight / 2) + 2;

                                        secreceivedmessages[6]._message = _program_name0;
                                        secreceivedmessages[6]._originalMsg = _program_name0;
                                        secreceivedmessages[6]._messageCut = _program_name0;
                                        secreceivedmessages[6]._specialMessage = 2;
                                        secreceivedmessages[6]._specialMessageLineX = 0;
                                        secreceivedmessages[6]._specialMessageLineY = 0;
                                        secreceivedmessages[6]._lineX = _initX0;
                                        secreceivedmessages[6]._lineY = _initY0;
                                        secreceivedmessages[6]._count = 0;
                                        secreceivedmessages[6]._swtch0 = 1;
                                        secreceivedmessages[6]._swtch1 = 0;
                                        secreceivedmessages[6]._delay = 50;
                                        secreceivedmessages[6]._looping = 0;

                                        _lastMenuOption = "";
                                        _console_reader_string._console_reader_message = "";

                                        Console.SetCursorPosition(_initX0, _initY0);
                                    }
                                }

                                Thread.Sleep(1);
                            }
                        }, mainreceivedmessages);
                        _worker_000_has_init = 5;
                    }



                    if (_worker_000_has_init == 5)
                    {
                        _console_worker_task = Task<object[]>.Factory.StartNew((tester0001) =>
                        {
                            while (true)
                            {
                                if (_worker_000_has_init == 2)
                                {
                                    int _welcomePackage00 = mainreceivedmessages[0]._welcomePackage;

                                    if (_welcomePackage00 == 0)
                                    {
                                        mainreceivedmessages = sc_console_menu.sc_console_menu_00._console_menu(mainreceivedmessages);
                                    }
                                    else if (_welcomePackage00 == 1)
                                    {
                                        int _current_menu00 = data00OUT._current_menu;

                                        if (_lastMenu != _current_menu00)
                                        {
                                            var _program_name0 = _current_menu00 + "";
                                            _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                            _initY0 = (Console.WindowHeight / 2) + 9;
                                            secreceivedmessages[5]._message = _program_name0;
                                            secreceivedmessages[5]._originalMsg = _program_name0;
                                            secreceivedmessages[5]._messageCut = _program_name0;
                                            secreceivedmessages[5]._specialMessage = 2;
                                            secreceivedmessages[5]._specialMessageLineX = 0;
                                            secreceivedmessages[5]._specialMessageLineY = 0;
                                            secreceivedmessages[5]._lineX = _initX0;
                                            secreceivedmessages[5]._lineY = _initY0;
                                            secreceivedmessages[5]._count = 0;
                                            secreceivedmessages[5]._swtch0 = 1;
                                            secreceivedmessages[5]._swtch1 = 0;
                                            secreceivedmessages[5]._delay = 50;
                                            secreceivedmessages[5]._looping = 0;
                                        }

                                        if (_current_menu00 == -1)
                                        {
                                            data00IN._received_switch_in = 0;
                                            data00IN._received_switch_out = 0;
                                            data00IN._sending_switch_in = 0;
                                            data00IN._sending_switch_out = 0;

                                            data00IN._current_menu = data00OUT._current_menu;
                                            data00IN._menuOption = _lastMenuOption;

                                            var objecterer = data00IN;
                                            data00OUT = sc_console_menu.sc_console_menu_01._console_menu(objecterer);

                                            _lastMenu = data00OUT._current_menu;
                                            _lastMenuOption = "";
                                        }
                                        else if (_current_menu00 == 0)
                                        {
                                            var _program_name0 = _current_menu00 + "";
                                            _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                            _initY0 = (Console.WindowHeight / 2) + 9;
                                            secreceivedmessages[5]._message = _program_name0;
                                            secreceivedmessages[5]._originalMsg = _program_name0;
                                            secreceivedmessages[5]._messageCut = _program_name0;
                                            secreceivedmessages[5]._specialMessage = 2;
                                            secreceivedmessages[5]._specialMessageLineX = 0;
                                            secreceivedmessages[5]._specialMessageLineY = 0;
                                            secreceivedmessages[5]._lineX = _initX0;
                                            secreceivedmessages[5]._lineY = _initY0;
                                            secreceivedmessages[5]._count = 0;
                                            secreceivedmessages[5]._swtch0 = 1;
                                            secreceivedmessages[5]._swtch1 = 0;
                                            secreceivedmessages[5]._delay = 50;
                                            secreceivedmessages[5]._looping = 0;

                                            data00IN._received_switch_in = 0;
                                            data00IN._received_switch_out = 0;
                                            data00IN._sending_switch_in = 0;
                                            data00IN._sending_switch_out = 0;

                                            data00IN._current_menu = data00OUT._current_menu;
                                            data00IN._menuOption = _lastMenuOption;

                                            var objecterer = data00IN;
                                            data00OUT = sc_console_menu.sc_console_menu_01._console_menu(objecterer);
                                            _lastMenu = data00OUT._current_menu;
                                            _lastMenuOption = "";
                                        }
                                        else if (_current_menu00 == 1)
                                        {
                                            var _program_name0 = _current_menu00 + "";
                                            _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                            _initY0 = (Console.WindowHeight / 2) + 9;
                                            secreceivedmessages[5]._message = _program_name0;
                                            secreceivedmessages[5]._originalMsg = _program_name0;
                                            secreceivedmessages[5]._messageCut = _program_name0;
                                            secreceivedmessages[5]._specialMessage = 2;
                                            secreceivedmessages[5]._specialMessageLineX = 0;
                                            secreceivedmessages[5]._specialMessageLineY = 0;
                                            secreceivedmessages[5]._lineX = _initX0;
                                            secreceivedmessages[5]._lineY = _initY0;
                                            secreceivedmessages[5]._count = 0;
                                            secreceivedmessages[5]._swtch0 = 1;
                                            secreceivedmessages[5]._swtch1 = 0;
                                            secreceivedmessages[5]._delay = 50;
                                            secreceivedmessages[5]._looping = 0;

                                            data00IN._received_switch_in = 0;
                                            data00IN._received_switch_out = 0;
                                            data00IN._sending_switch_in = 0;
                                            data00IN._sending_switch_out = 0;

                                            data00IN._current_menu = data00OUT._current_menu;
                                            data00IN._menuOption = _lastMenuOption;

                                            var objecterer = data00IN;
                                            data00OUT = sc_console_menu.sc_console_menu_01._console_menu(objecterer);
                                            _lastMenu = data00OUT._current_menu;
                                            _lastMenuOption = "";
                                        }
                                    }
                                }
                                Thread.Sleep(1);
                            }
                        }, mainreceivedmessages);
                        _worker_000_has_init = 6;
                    }




                    if (_some_other_swtch == 1)  //_some_other_swtch == 1
                    {
                        if (has_init_directx == 0)
                        {
                            if (initdirectXmainswtch == 2 || initvrmainswtch == 2)
                            {
                                if (initdirectXmainswtch == 2)
                                {
                                    /*config = new sc_system_configuration("sc core systems", 1920, 1080, false, false);

                                    handler = sc_console_core.handle;// SC_GLOBALS_ACCESSORS.SC_CONSOLE_CORE.handle;

                                    if (handler == IntPtr.Zero)
                                    {
                                        //MessageBox((IntPtr)0, "null console ", "_sc_core_systems error", 0);
                                    }
                                    else
                                    {
                                        //MessageBox((IntPtr)0, "!null console ", "_sc_core_systems error", 0);
                                    }
                                    sc_update = new SC_Update();*/

                    /*for (int x = 0; x < Console.WindowWidth; x++)
                    {
                        for (int y = 0; y < Console.WindowWidth; y++)
                        {
                            SC_GLOBALS_ACCESSORS.SC_CONSOLE_WRITER.Draw(x, y, " ");
                        }
                    }
                    sc_update = new SC_Update();
                    initdirectXmainswtch = 3;
                }

                if (initvrmainswtch == 2)
                {
                    config = new scsystemconfiguration("sc core systems", 1920, 1080, false, false);
                    consoleHandle = scconsolecore.handle;

                    updatescript = new scupdate();

                    /*MainViewModel vm = new MainViewModel
                    {
                        Content = updatescript
                    };*/





                    /*var updateMainUITitle = new Action(() =>
                    {
                        threadOneGrammarLoad();
                    });

                    System.Windows.Application.Current.Dispatcher.Invoke(updateMainUITitle);
                    initvrmainswtch = 3;
                }

                has_init_directx = 1;
            }
        }
    }*/


                    if (has_init_directx == 0)
                    {
                        //MessageBox((IntPtr)0, "has_init_directx", "scmsg", 0);
                        //config = new scsystemconfiguration("sc core systems", 1920, 1080, false, false);
                        //consoleHandle = scconsolecore.handle;

                        //updatescript = new scupdate();
                        has_init_directx = 1;
                    }







                    Thread.Sleep(1);
                    goto mainthreadloop;

                    //MainWindow.MessageBox((IntPtr)0, "program quit1", "sc core systems message", 0);

                }, 0); //100000 //999999999

                mainthread.IsBackground = true;
                mainthread.Priority = ThreadPriority.Lowest;
                mainthread.SetApartmentState(ApartmentState.STA);
                mainthread.Start();

                initmainthread = 2;
            }



            /*
            this.Loaded += delegate
            {
                MessageBox((IntPtr)0, "exiting0", "scmsg", 0);
            };

            this.Closing += delegate (object sender, CancelEventArgs args)
            {
                MessageBox((IntPtr)0, "exiting1", "scmsg", 0);
            };*/



            /*MainViewModel vm = new MainViewModel
            {
                Content = updatescript
            };*/


            /*
        mainthreadloopx0:

            if (initmainthread == -2)
            {
                updatescript = new scupdate();

                mainthread = new Thread((tester0000) =>
                {

                    config = new scsystemconfiguration("sc core systems", 1920, 1080, false, false);

                //consoleHandle = scconsolecore.handle;

                mainthreadloop:


                    keyboardinput.ReadKeyboard();

                    if (_start_thread_console_writer == 1)
                    {
                        _console_writer_task = Task<object[]>.Factory.StartNew((tester0001) =>
                        {
                            var _initX = (Console.WindowWidth / 2) - (_program_name.Length / 2);
                            var _initY = (Console.WindowHeight / 2);

                            secreceivedmessages[0]._message = _program_name;
                            secreceivedmessages[0]._originalMsg = _program_name;
                            secreceivedmessages[0]._messageCut = _program_name;
                            secreceivedmessages[0]._specialMessage = 0;
                            secreceivedmessages[0]._specialMessageLineX = 0;
                            secreceivedmessages[0]._specialMessageLineY = 0;
                            secreceivedmessages[0]._orilineX = _initX;
                            secreceivedmessages[0]._orilineY = _initY;
                            secreceivedmessages[0]._lineX = _initX;
                            secreceivedmessages[0]._lineY = _initY;
                            secreceivedmessages[0]._lastOrilineX = _initX;
                            secreceivedmessages[0]._lastOrilineY = _initY;
                            secreceivedmessages[0]._count = 0;
                            secreceivedmessages[0]._swtch0 = 1;
                            secreceivedmessages[0]._swtch1 = 1;
                            secreceivedmessages[0]._delay = 5;
                            secreceivedmessages[0]._looping = 1;

                            _worker_000_has_init = 1;

                        //////CONSOLE WRITER=>
                        _thread_loop_console:

                            secreceivedmessages = SCGLOBALSACCESSORS.SCCONSOLEWRITER._console_writer(secreceivedmessages);

                            Thread.Sleep(1);

                            goto _thread_loop_console;
                            //////CONSOLE WRITER <=
                        }, mainreceivedmessages);
                        _start_thread_console_writer = 2;
                    }

                    //CONFIRM CONSOLE WRITER IS WORKING=>
                    if (_worker_000_has_init == 1)
                    {
                        if (SCGLOBALSACCESSORS != null)
                        {
                            if (SCGLOBALSACCESSORS.SCCONSOLEWRITER != null)
                            {
                                /*secreceivedmessages[1]._message = "C-WR" + " ENABLED";
                                secreceivedmessages[1]._originalMsg = "C-WR" + " ENABLED";
                                secreceivedmessages[1]._messageCut = "C-WR" + " ENABLED";
                                secreceivedmessages[1]._specialMessage = 2;
                                secreceivedmessages[1]._specialMessageLineX = 0;
                                secreceivedmessages[1]._specialMessageLineY = 0;
                                secreceivedmessages[1]._orilineX = 1;
                                secreceivedmessages[1]._orilineY = Console.WindowHeight - 2;
                                secreceivedmessages[1]._lineX = 1;
                                secreceivedmessages[1]._lineY = Console.WindowHeight - 2;
                                secreceivedmessages[1]._lastOrilineX = secreceivedmessages[1]._lineX;
                                secreceivedmessages[1]._lastOrilineY = secreceivedmessages[1]._lineY;
                                secreceivedmessages[1]._count = 0;
                                secreceivedmessages[1]._swtch0 = 1;
                                secreceivedmessages[1]._swtch1 = 0;
                                secreceivedmessages[1]._delay = 10;
                                secreceivedmessages[1]._looping = 0;

                                _worker_000_has_init = 2;
                            }
                        }
                    }
                    //CONFIRM CONSOLE WRITER IS WORKING<=

                    //CONFIRM CONSOLE READER IS WORKING=>
                    if (_worker_000_has_init == 2)
                    {
                        if (SCGLOBALSACCESSORS != null)
                        {
                            if (SCGLOBALSACCESSORS.SCCONSOLEREADER != null)
                            {
                                /*secreceivedmessages[2]._message = "core C-RE" + " ENABLED";
                                secreceivedmessages[2]._originalMsg = "core C-RE" + " ENABLED";
                                secreceivedmessages[2]._messageCut = "core C-RE" + " ENABLED";
                                secreceivedmessages[2]._specialMessage = 2;
                                secreceivedmessages[2]._specialMessageLineX = 0;
                                secreceivedmessages[2]._specialMessageLineY = 1;
                                secreceivedmessages[2]._orilineX = secreceivedmessages[2]._message.Length + 3;
                                secreceivedmessages[2]._orilineY = Console.WindowHeight - 2;
                                secreceivedmessages[2]._lineX = secreceivedmessages[2]._message.Length + 3;
                                secreceivedmessages[2]._lineY = Console.WindowHeight - 2;
                                secreceivedmessages[2]._count = 0;
                                secreceivedmessages[2]._swtch0 = 1;
                                secreceivedmessages[2]._swtch1 = 0;
                                secreceivedmessages[2]._delay = 10;
                                secreceivedmessages[2]._looping = 0;

                                _worker_000_has_init = 3;
                            }
                        }
                    }
                    //CONFIRM CONSOLE READER IS WORKING<=





                    if (_worker_000_has_init == 3)
                    {
                        if (SCGLOBALSACCESSORS != null)
                        {
                            if (SCGLOBALSACCESSORS.SCCONSOLEREADER != null)
                            {
                                var _program_name0 = "Press Enter";
                                _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                _initY0 = (Console.WindowHeight / 2) + 1;

                                secreceivedmessages[3]._message = _program_name0;
                                secreceivedmessages[3]._originalMsg = _program_name0;
                                secreceivedmessages[3]._messageCut = _program_name0;
                                secreceivedmessages[3]._specialMessage = 2;
                                secreceivedmessages[3]._specialMessageLineX = 0;
                                secreceivedmessages[3]._specialMessageLineY = 0;
                                secreceivedmessages[3]._orilineX = _initX0;
                                secreceivedmessages[3]._orilineY = _initY0;
                                secreceivedmessages[3]._lineX = _initX0;
                                secreceivedmessages[3]._lineY = _initY0;
                                secreceivedmessages[3]._count = 0;
                                secreceivedmessages[3]._swtch0 = 1;
                                secreceivedmessages[3]._swtch1 = 1;
                                secreceivedmessages[3]._delay = 100;
                                secreceivedmessages[3]._looping = 1;
                                _worker_000_has_init = 4;
                            }
                        }
                    }



                    if (_worker_000_has_init == 4)
                    {

                        _console_reader_task = Task<object[]>.Factory.StartNew((tester0001) =>
                        {
                            while (true)
                            {
                                if (_console_reader_canWork == 1)
                                {
                                    _console_reader_string = SCGLOBALSACCESSORS.SCCONSOLEREADER._console_reader(_console_reader_object);
                                }

                                if (SCGLOBALSACCESSORS.SCCONSOLEREADER._main_has_init == 1)
                                {
                                    _console_reader_string._console_reader_message = "";
                                    _console_reader_string._has_message_to_display = 0;

                                    var _program_name0 = "WELCOME";
                                    _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                    _initY0 = (Console.WindowHeight / 2) - 1;

                                    secreceivedmessages[4]._message = _program_name0;
                                    secreceivedmessages[4]._originalMsg = _program_name0;
                                    secreceivedmessages[4]._messageCut = _program_name0;
                                    secreceivedmessages[4]._specialMessage = 2;
                                    secreceivedmessages[4]._specialMessageLineX = 0;
                                    secreceivedmessages[4]._specialMessageLineY = 0;
                                    secreceivedmessages[4]._orilineX = _initX0;
                                    secreceivedmessages[4]._orilineY = _initY0;
                                    secreceivedmessages[4]._lineX = _initX0;
                                    secreceivedmessages[4]._lineY = _initY0;
                                    secreceivedmessages[4]._count = 0;
                                    secreceivedmessages[4]._swtch0 = 1;
                                    secreceivedmessages[4]._swtch1 = 0;
                                    secreceivedmessages[4]._delay = 200;
                                    secreceivedmessages[4]._looping = 0;

                                    secreceivedmessages[0]._swtch0 = 0;
                                    secreceivedmessages[0]._swtch1 = 0;
                                    secreceivedmessages[3]._swtch0 = 0;
                                    secreceivedmessages[3]._swtch1 = 0;

                                    _program_name0 = "Please Enter your Username: ";
                                    _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                    _initY0 = (Console.WindowHeight / 2) + 2;

                                    secreceivedmessages[6]._message = _program_name0;
                                    secreceivedmessages[6]._originalMsg = _program_name0;
                                    secreceivedmessages[6]._messageCut = _program_name0;
                                    secreceivedmessages[6]._specialMessage = 2;
                                    secreceivedmessages[6]._specialMessageLineX = 0;
                                    secreceivedmessages[6]._specialMessageLineY = 0;
                                    secreceivedmessages[6]._orilineX = _initX0;
                                    secreceivedmessages[6]._orilineY = _initY0;
                                    secreceivedmessages[6]._lineX = _initX0;
                                    secreceivedmessages[6]._lineY = _initY0;
                                    secreceivedmessages[6]._count = 0;
                                    secreceivedmessages[6]._swtch0 = 1;
                                    secreceivedmessages[6]._swtch1 = 1;
                                    secreceivedmessages[6]._delay = 50;
                                    secreceivedmessages[6]._looping = 1;

                                    Console.SetCursorPosition(_initX0 + _program_name0.Length, _initY0);

                                    startThread = 3;
                                    SCGLOBALSACCESSORS.SCCONSOLEREADER._main_has_init = 2;
                                }

                                if (startThread == 3 && _console_reader_string._has_message_to_display == 1)
                                {

                                    if (_console_reader_string._console_reader_message.ToLower() == "nine" || _console_reader_string._console_reader_message.ToLower() == "ninekorn" || _console_reader_string._console_reader_message.ToLower() == "9")
                                    {

                                        var _program_name0 = "Access Authorized";
                                        _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                        _initY0 = (Console.WindowHeight / 2) + 2;

                                        secreceivedmessages[6]._message = _program_name0;
                                        secreceivedmessages[6]._originalMsg = _program_name0;
                                        secreceivedmessages[6]._messageCut = _program_name0;
                                        secreceivedmessages[6]._specialMessage = 2;
                                        secreceivedmessages[6]._specialMessageLineX = 0;
                                        secreceivedmessages[6]._specialMessageLineY = 0;
                                        secreceivedmessages[6]._lineX = _initX0;
                                        secreceivedmessages[6]._lineY = _initY0;
                                        secreceivedmessages[6]._count = 0;
                                        secreceivedmessages[6]._swtch0 = 1;
                                        secreceivedmessages[6]._swtch1 = 0;
                                        secreceivedmessages[6]._delay = 50;
                                        secreceivedmessages[6]._looping = 0;

                                        for (int L0_IN = 0; L0_IN < mainreceivedmessages.Length; L0_IN++)
                                        {
                                            mainreceivedmessages[L0_IN]._passTest = _console_reader_string._console_reader_message.ToLower();
                                        }
                                        Console.SetCursorPosition(_initX0, _initY0 + 1);
                                        _lastUsername = _console_reader_string._console_reader_message;
                                        _console_reader_string._console_reader_message = "";
                                        startThread = 4;
                                    }
                                    else if (_console_reader_string._console_reader_message.ToLower() != " " || _console_reader_string._console_reader_message.ToLower() != "")
                                    {

                                        var _program_name0 = "Access Denied";
                                        _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                        _initY0 = (Console.WindowHeight / 2) + 2;

                                        secreceivedmessages[6]._message = _program_name0;
                                        secreceivedmessages[6]._originalMsg = _program_name0;
                                        secreceivedmessages[6]._messageCut = _program_name0;
                                        secreceivedmessages[6]._specialMessage = 2;
                                        secreceivedmessages[6]._specialMessageLineX = 0;
                                        secreceivedmessages[6]._specialMessageLineY = 0;
                                        secreceivedmessages[6]._lineX = _initX0;
                                        secreceivedmessages[6]._lineY = _initY0;
                                        secreceivedmessages[6]._count = 0;
                                        secreceivedmessages[6]._swtch0 = 1;
                                        secreceivedmessages[6]._swtch1 = 0;
                                        secreceivedmessages[6]._delay = 50;
                                        secreceivedmessages[6]._looping = 0;

                                        _lastUsername = "";
                                        _console_reader_string._console_reader_message = "";
                                        Console.SetCursorPosition(_initX0, _initY0);
                                        startThread = 3;
                                    }
                                }
                                else if (startThread == 4)
                                {
                                    if (_console_reader_string._console_reader_message.ToLower() == "vr" ||
                                         _console_reader_string._console_reader_message.ToLower() == "standard" ||
                                          _console_reader_string._console_reader_message.ToLower() == "std")
                                    {
                                        if (_console_reader_string._console_reader_message.ToLower() == "vr")
                                        {

                                            var _program_name0 = "creating VR mecanics";
                                            _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                            _initY0 = (Console.WindowHeight / 2) + 2;

                                            secreceivedmessages[6]._message = _program_name0;
                                            secreceivedmessages[6]._originalMsg = _program_name0;
                                            secreceivedmessages[6]._messageCut = _program_name0;
                                            secreceivedmessages[6]._specialMessage = 2;
                                            secreceivedmessages[6]._specialMessageLineX = 0;
                                            secreceivedmessages[6]._specialMessageLineY = 0;
                                            secreceivedmessages[6]._lineX = _initX0;
                                            secreceivedmessages[6]._lineY = _initY0;
                                            secreceivedmessages[6]._count = 0;
                                            secreceivedmessages[6]._swtch0 = 1;
                                            secreceivedmessages[6]._swtch1 = 0;
                                            secreceivedmessages[6]._delay = 50;
                                            secreceivedmessages[6]._looping = 0;


                                            _lastMenuOption = _console_reader_string._console_reader_message.ToLower();
                                            _console_reader_string._console_reader_message = "";

                                            mainreceivedmessages[0]._received_switch_in = 1;
                                            mainreceivedmessages[0]._received_switch_out = 1;
                                            mainreceivedmessages[0]._sending_switch_in = 1;
                                            mainreceivedmessages[0]._sending_switch_out = 1;
                                            mainreceivedmessages[0]._welcomePackage = 999;

                                            mainreceivedmessages = sc_console_menu.sc_console_menu_00._console_menu(mainreceivedmessages);
                                            Console.SetCursorPosition(_initX0, _initY0 + 1);
                                            _some_other_swtch = 1;
                                        }
                                        else if (_console_reader_string._console_reader_message.ToLower() == "standard" ||
                                                _console_reader_string._console_reader_message.ToLower() == "std")
                                        {

                                            _lastMenuOption = _console_reader_string._console_reader_message.ToLower();
                                            _console_reader_string._console_reader_message = "";

                                            mainreceivedmessages[0]._received_switch_in = 1;
                                            mainreceivedmessages[0]._received_switch_out = 1;
                                            mainreceivedmessages[0]._sending_switch_in = 1;
                                            mainreceivedmessages[0]._sending_switch_out = 1;
                                            mainreceivedmessages[0]._welcomePackage = 998;
                                            mainreceivedmessages = sc_console_menu.sc_console_menu_00._console_menu(mainreceivedmessages);
                                            Console.SetCursorPosition(_initX0, _initY0 + 1);
                                            _some_other_swtch = 1;
                                        }
                                    }
                                    else
                                    {

                                        var _program_name0 = "Option Not Implemented";
                                        _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                        _initY0 = (Console.WindowHeight / 2) + 2;

                                        secreceivedmessages[6]._message = _program_name0;
                                        secreceivedmessages[6]._originalMsg = _program_name0;
                                        secreceivedmessages[6]._messageCut = _program_name0;
                                        secreceivedmessages[6]._specialMessage = 2;
                                        secreceivedmessages[6]._specialMessageLineX = 0;
                                        secreceivedmessages[6]._specialMessageLineY = 0;
                                        secreceivedmessages[6]._lineX = _initX0;
                                        secreceivedmessages[6]._lineY = _initY0;
                                        secreceivedmessages[6]._count = 0;
                                        secreceivedmessages[6]._swtch0 = 1;
                                        secreceivedmessages[6]._swtch1 = 0;
                                        secreceivedmessages[6]._delay = 50;
                                        secreceivedmessages[6]._looping = 0;

                                        _lastMenuOption = "";
                                        _console_reader_string._console_reader_message = "";

                                        Console.SetCursorPosition(_initX0, _initY0);
                                    }
                                }

                                Thread.Sleep(1);
                            }
                        }, mainreceivedmessages);
                        _worker_000_has_init = 5;
                    }



                    if (_worker_000_has_init == 5)
                    {
                        _console_worker_task = Task<object[]>.Factory.StartNew((tester0001) =>
                        {
                            while (true)
                            {
                                if (_worker_000_has_init == 2)
                                {
                                    int _welcomePackage00 = mainreceivedmessages[0]._welcomePackage;

                                    if (_welcomePackage00 == 0)
                                    {
                                        mainreceivedmessages = sc_console_menu.sc_console_menu_00._console_menu(mainreceivedmessages);
                                    }
                                    else if (_welcomePackage00 == 1)
                                    {
                                        int _current_menu00 = data00OUT._current_menu;

                                        if (_lastMenu != _current_menu00)
                                        {
                                            var _program_name0 = _current_menu00 + "";
                                            _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                            _initY0 = (Console.WindowHeight / 2) + 9;
                                            secreceivedmessages[5]._message = _program_name0;
                                            secreceivedmessages[5]._originalMsg = _program_name0;
                                            secreceivedmessages[5]._messageCut = _program_name0;
                                            secreceivedmessages[5]._specialMessage = 2;
                                            secreceivedmessages[5]._specialMessageLineX = 0;
                                            secreceivedmessages[5]._specialMessageLineY = 0;
                                            secreceivedmessages[5]._lineX = _initX0;
                                            secreceivedmessages[5]._lineY = _initY0;
                                            secreceivedmessages[5]._count = 0;
                                            secreceivedmessages[5]._swtch0 = 1;
                                            secreceivedmessages[5]._swtch1 = 0;
                                            secreceivedmessages[5]._delay = 50;
                                            secreceivedmessages[5]._looping = 0;
                                        }

                                        if (_current_menu00 == -1)
                                        {
                                            data00IN._received_switch_in = 0;
                                            data00IN._received_switch_out = 0;
                                            data00IN._sending_switch_in = 0;
                                            data00IN._sending_switch_out = 0;

                                            data00IN._current_menu = data00OUT._current_menu;
                                            data00IN._menuOption = _lastMenuOption;

                                            var objecterer = data00IN;
                                            data00OUT = sc_console_menu.sc_console_menu_01._console_menu(objecterer);

                                            _lastMenu = data00OUT._current_menu;
                                            _lastMenuOption = "";
                                        }
                                        else if (_current_menu00 == 0)
                                        {
                                            var _program_name0 = _current_menu00 + "";
                                            _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                            _initY0 = (Console.WindowHeight / 2) + 9;
                                            secreceivedmessages[5]._message = _program_name0;
                                            secreceivedmessages[5]._originalMsg = _program_name0;
                                            secreceivedmessages[5]._messageCut = _program_name0;
                                            secreceivedmessages[5]._specialMessage = 2;
                                            secreceivedmessages[5]._specialMessageLineX = 0;
                                            secreceivedmessages[5]._specialMessageLineY = 0;
                                            secreceivedmessages[5]._lineX = _initX0;
                                            secreceivedmessages[5]._lineY = _initY0;
                                            secreceivedmessages[5]._count = 0;
                                            secreceivedmessages[5]._swtch0 = 1;
                                            secreceivedmessages[5]._swtch1 = 0;
                                            secreceivedmessages[5]._delay = 50;
                                            secreceivedmessages[5]._looping = 0;

                                            data00IN._received_switch_in = 0;
                                            data00IN._received_switch_out = 0;
                                            data00IN._sending_switch_in = 0;
                                            data00IN._sending_switch_out = 0;

                                            data00IN._current_menu = data00OUT._current_menu;
                                            data00IN._menuOption = _lastMenuOption;

                                            var objecterer = data00IN;
                                            data00OUT = sc_console_menu.sc_console_menu_01._console_menu(objecterer);
                                            _lastMenu = data00OUT._current_menu;
                                            _lastMenuOption = "";
                                        }
                                        else if (_current_menu00 == 1)
                                        {
                                            var _program_name0 = _current_menu00 + "";
                                            _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                            _initY0 = (Console.WindowHeight / 2) + 9;
                                            secreceivedmessages[5]._message = _program_name0;
                                            secreceivedmessages[5]._originalMsg = _program_name0;
                                            secreceivedmessages[5]._messageCut = _program_name0;
                                            secreceivedmessages[5]._specialMessage = 2;
                                            secreceivedmessages[5]._specialMessageLineX = 0;
                                            secreceivedmessages[5]._specialMessageLineY = 0;
                                            secreceivedmessages[5]._lineX = _initX0;
                                            secreceivedmessages[5]._lineY = _initY0;
                                            secreceivedmessages[5]._count = 0;
                                            secreceivedmessages[5]._swtch0 = 1;
                                            secreceivedmessages[5]._swtch1 = 0;
                                            secreceivedmessages[5]._delay = 50;
                                            secreceivedmessages[5]._looping = 0;

                                            data00IN._received_switch_in = 0;
                                            data00IN._received_switch_out = 0;
                                            data00IN._sending_switch_in = 0;
                                            data00IN._sending_switch_out = 0;

                                            data00IN._current_menu = data00OUT._current_menu;
                                            data00IN._menuOption = _lastMenuOption;

                                            var objecterer = data00IN;
                                            data00OUT = sc_console_menu.sc_console_menu_01._console_menu(objecterer);
                                            _lastMenu = data00OUT._current_menu;
                                            _lastMenuOption = "";
                                        }
                                    }
                                }
                                Thread.Sleep(1);
                            }
                        }, mainreceivedmessages);
                        _worker_000_has_init = 6;
                    }


                    Thread.Sleep(0);
                    goto mainthreadloop;

                }, 3); //100000 //999999999

                mainthread.IsBackground = true;
                mainthread.Priority = ThreadPriority.Lowest;
                mainthread.SetApartmentState(ApartmentState.STA);
                mainthread.Start();

                initmainthread = 2;
            }

            if (initmainthread == 2)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    Thread.Sleep(0);
                    goto mainthreadloopx0;
                }
                else
                {
                    Thread.Sleep(0);
                    goto mainthreadloopx0;
                }
            }
            else
            {
                ////System.Windows.MessageBox.Show("lOOp", "CONSOLE");
                Thread.Sleep(0);
                goto mainthreadloopx0;
            }*/


            //Console.WriteLine("Hello World!");
        }
    }
}
