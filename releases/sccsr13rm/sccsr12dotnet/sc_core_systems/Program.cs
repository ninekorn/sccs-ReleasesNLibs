using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Linq;
using System.Windows;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading.Tasks;
//using System.Speech.Recognition;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Globalization;

using Jitter;
using Jitter.LinearMath;
using Jitter.Dynamics;
using Jitter.Collision;
using Jitter.Collision.Shapes;
using Jitter.Forces;

using SCCoreSystems.sc_core;
using SCCoreSystems.sc_console;
using _messager = SCCoreSystems.sc_console._messager;
using SC_message_object = sc_message_object.sc_message_object;
using SC_message_object_jitter = sc_message_object.sc_message_object_jitter;

//using System.Windows.Input;
///using Microsoft.VisualBasic;
//using System.Windows.Forms;
//using Microsoft.VisualBasic.Devices;
//using System.Speech.Recognition;
//using System.Speech.Synthesis; ///For program to speak back
//using AI.Perceptron;
//using System.Threading.Tasks;
//using System.Speech;
//using System.Speech.Recognition;
//using _messager = SCCoreSystems.sc_console._messager;
//using SC_message_object = sc_message_object.sc_message_object;
//using SC_message_object_jitter = sc_message_object.sc_message_object_jitter;
//using System.Windows.Threading;
//using System.Linq;
//using System.Windows;
//using System.Windows.Input;
//using System.IO;
//using Microsoft.VisualBasic;
//using System.Reflection;
//using System.Text.RegularExpressions;
//using System.Xml;
//using SCCoreSystems.sc_console;
//using System.ComponentModel;
//using Jitter.Dynamics;
//using Jitter.Collision;
//using Jitter.Collision.Shapes;
//using Jitter.Forces;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using Microsoft.VisualBasic.Devices;
//using AI.Perceptron;
//using System.Threading.Tasks;
//using System.Speech;
//using System.Speech.Recognition;
//using System.Speech.Recognition;
//using System.Speech.Synthesis; ///For program to speak back
//using System.Windows.Forms;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

using WindowsInput;

namespace SCCoreSystems
{
    //internal partial class MainWindow : Window
    internal static class Program
    {
        public static string _MainWindow_name = "";
        public static int useArduinoOVRTouchKeymapper = 0;

        public static InputSimulator inputsim;
        public static KeyboardSimulator keyboardsim;
        public static MouseSimulator mousesim;

        //static SpeechRecognitionEngine recEngine;// = new SpeechRecognitionEngine();
        public static SC_Update sc_update;
        static Task _console_worker_task;
        static Task _console_reader_task;
        static Task _console_writer_task;
        static Thread _mainTasker00;

        public static SCCoreSystems.sc_core.sc_globals_accessor SC_GLOBALS_ACCESSORS;
        public static sc_system_configuration config;
        public static JVector _world_gravity = new JVector(0, -9.81f, 0); //-9.81f base
        public static int _world_iterations = 3; // as high as possible normally for higher precision
        public static int _world_small_iterations = 3; // as high as possible normally for higher precision
        public static float _world_allowed_penetration = 0.00123f; //0.00123f  _world_gravity = new JVector(0, -9.81f, 0);
        public static bool _allow_deactivation = true;


        public static int _physics_engine_instance_x = 1; //4
        public static int _physics_engine_instance_y = 1; //1
        public static int _physics_engine_instance_z = 1; //4

        public static int world_width = 1;
        public static int world_height = 1;
        public static int world_depth = 1;


        public static float _delta_timer_frame = 0;
        public static float _delta_timer_time = 0;
        public static DateTime time1;
        public static DateTime time2;
        public static Stopwatch timeStopWatch00 = new Stopwatch();
        public static Stopwatch timeStopWatch01 = new Stopwatch();
        public static int _swtch = 0;
        public static int _swtch_counter_00 = 0;
        public static int _swtch_counter_01 = 0;
        public static int _swtch_counter_02 = 0;

        static float deltaTime = 0.0f;
        static float[] _array_stop_watch_tick;

        public static async Task sc_deltatime(int timeOut, int _taskID)
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
            var _delta_timer_frame = (float)Math.Abs((timeStopWatch01.Elapsed.Ticks - timeStopWatch00.Elapsed.Ticks)) * 0.0000000001f;

            time2 = DateTime.Now;
            var _delta_timer_time = (time2.Ticks - time1.Ticks); //100000000f
            //time1 = time2;

            deltaTime = (float)Math.Abs(_delta_timer_frame - _delta_timer_time) * 0.0000000001f;

            _array_stop_watch_tick[0] = deltaTime;

            //time1 = time2;
            await Task.Delay(1);
            Thread.Sleep(timeOut);
            _swtch_counter_00++;
            _swtch_counter_01++;
            _swtch_counter_02++;

            goto _threadLoop;
        }

        /*typeof(int),
        typeof(int),
        typeof(float),
        typeof(bool),
        typeof(JVector),
        typeof(int),
        typeof(int),
        typeof(int),*/

        static int _initX0 = 0;
        static int _initY0 = 0;

        public static IntPtr consoleHandle;
        //public static SC_Console_GRAPHICS consoleGraphics = null;
        public static sc_graphics_sec _graphics_sec = null;
        //public static SC_console_directx D3D;// = new SC_console_directx();


        //EXTERNACCESSORS
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        /*public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }*/

        //MAINPROGRAMCARIABLES
        //public static MainWindow _mainWindow;
        //static bool _isComponents;
        public static int _processorCount = 0;
        public static int _workerThreadsTotal;
        public static int _portThreadsTotal;
        //static Thread _thread;
        public static int _totalThreads = 0;
        public static bool _mainFrameStarterItemz = true;


        //static int _swtch4consoleborders = 0;
        public static int _init_main = 1;
        //static int _counter_reset_console_borders = 0;


        public static sc_console.sc_console_keyboard_input _keyboard_input;
        static int _Max_Size_00 = 16;
        static sc_message_object.sc_message_object[] _main_received_messages;//
        static _messager[] _sec_received_messages;
        public static string _program_name = "SC Core Systems";
        static sc_message_object.sc_message_object _data00_IN;
        static sc_message_object.sc_message_object _data00_OUT;
        public static int init_directX_main_swtch = 2;
        public static int init_vr_main_swtch = -1; //2
        static int has_init_directx = 0;
        static sc_console.sc_console_reader._console_reader_data _console_reader_string;

        //Window _mainWindow;
        //public MainWindow()
        static void Main(string[] args)
        {
            //_mainWindow = this;
            /*_mainWindow = this;
            //INITIALIZING COMPONENTS//
            for (int j = 0; j < 1; j++)
            {
                try
                {
                    //InitializeComponent();
                    _isComponents = true;
                }
                catch
                {
                    _isComponents = false;
                    break;
                }
            }*/
            //INITIALIZING COMPONENTS//

            //CREATING SC_Console//
            for (int j = 0; j < 1; j++)
            {
                try
                {
                    //SC_Console.createConsole();
                    //SC_GCGollect collector = new SC_GCGollect();
                }
                catch
                {
                    break;
                }
            }
            //CREATING SC_Console//

            for (int j = 0; j < 1; j++)
            {
                try
                {
                    _processorCount = Environment.ProcessorCount;// SC_SystemInfoSeeker.getSystemProcessorCount();
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
                    ThreadPool.GetMaxThreads(out _workerThreadsTotal, out _portThreadsTotal);
                    ThreadPool.GetAvailableThreads(out _workerThreadsTotal, out _portThreadsTotal);
                }
                catch
                {
                    break;
                }
            }

            //_totalThreads = (int)(_portThreadsTotal * 0.01f);
            _totalThreads = 1;

            //SC_Console.consoleMessageQueue messageQueue1 = new SC_Console.consoleMessageQueue(_totalThreads + "", 0, 0);

            //ContentFrame2.Source = new Uri("Customizations/SC_AI_VR.xaml", UriKind.Relative);
            //ContentFrame1.Source = new Uri("Customizations/SC_desktopDupe.xaml", UriKind.Relative);


            int _last_console_width = Console.WindowWidth;
            int _last_console_height = Console.WindowHeight;




            //SC_GLOBALS_ACCESSORS AND GLOBALS CREATOR
            SC_GLOBALS_ACCESSORS = new SCCoreSystems.sc_core.sc_globals_accessor(_main_received_messages);
            if (SC_GLOBALS_ACCESSORS == null)
            {
                //System.Windows.MessageBox.Show("SC_GLOBALS_ACCESSORS NULL", "Console");
            }
            else
            {
                //System.Windows.MessageBox.Show("SC_GLOBALS_ACCESSORS !NULL", "Console");
            }
            //borderlessconsole console_ = new borderlessconsole();
            //SC_GLOBALS_ACCESSORS AND GLOBALS CREATOR

            ////////////////////
            ///KEYBOARD INPUT///
            ////////////////////
            for (int j = 0; j < 1; j++)
            {
                try
                {
                    _keyboard_input = new sc_console.sc_console_keyboard_input();
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
                    if (_keyboard_input != null)
                    {
                        _keyboard_input._InitializeKeyboard();
                        _keyboard_input._KeyboardState = new SharpDX.DirectInput.KeyboardState();
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


            /*if (_keyboard_input._KeyboardState != null)
            {
                MessageBox((IntPtr)0, "could get keyboard info main 03: ", "_sc_core_systems error", 0);
            }
            else
            {
                MessageBox((IntPtr)0, "cannot get keyboard info main 04: ", "_sc_core_systems error", 0);
                //something is wrong, todo something else later. but not implemented yet. maybe get raw input instead from SharpDX i dont know
            }*/

            ////////////////////
            ///KEYBOARD INPUT///
            ////////////////////












            /////////////////////////////// : But i am not totally sure. I certainly can use this struct and send it to an "async" task and retrieve the data
            ///////////////////////////////   right away even if i sometimes or always "modify" the data before sending it to a task and then right after the task
            ///////////////////////////////   finishes, i am retrieving the data from that async task. But i am not sure yet if my shit works "completely". as i am
            ///////////////////////////////   not "declaring" the task as "async" BUT i am only starting that task once and let it run whatever it has to run in a loop.
            ///message_thread_safe_kinda///   everybody does that it's for sure anyway, i didn't invent anything here. It's just that this struct is very important
            ///////////////////////////////   for the rest of the program to run and this first struct will be able to communicate with all of my program in INTs
            ///////////////////////////////   used as binary code "kinda" for every option that i want to implement in order to alleviate the load of menu mecanics.
            ///////////////////////////////   the namespace to get it really can really be simplified. i mean really. wtheck am i doing.
            ///////////////////////////////
            _main_received_messages = new sc_message_object.sc_message_object[_Max_Size_00];


            for (int i = 0; i < _main_received_messages.Length; i++)
            {
                _main_received_messages[i] = new sc_message_object.sc_message_object();
                _main_received_messages[i]._received_switch_in = -1;
                _main_received_messages[i]._received_switch_out = -1;
                _main_received_messages[i]._sending_switch_in = -1;
                _main_received_messages[i]._sending_switch_out = -1;
                _main_received_messages[i]._timeOut0 = -1;
                _main_received_messages[i]._ParentTaskThreadID0 = -1;
                _main_received_messages[i]._main_cpu_count = _processorCount;
                _main_received_messages[i]._passTest = "";
                _main_received_messages[i]._welcomePackage = -1;
                _main_received_messages[i]._work_done = -1;
                _main_received_messages[i]._current_menu = -1;
                _main_received_messages[i]._last_current_menu = -1;
                _main_received_messages[i]._main_menu = -1;
                _main_received_messages[i]._menuOption = "";
                _main_received_messages[i]._voRecSwtc = -1;
                _main_received_messages[i]._voRecMsg = "";
                _main_received_messages[i]._someData = new object[_Max_Size_00];

                for (int j = 0; j < _main_received_messages[i]._someData.Length; j++)
                {
                    _main_received_messages[i]._someData[j] = new object();
                }

                //VOICE RECOGNITION. NOT IMPLEMENTED YET.
                /*if (i == _Max_Size_00 - 1)
                {
                    _main_received_messages[i]._someData[0] = _keyboard_input._KeyboardState;
                    _main_received_messages[i]._voRecSwtc = 1;
                }*/
            }
            ///////////////////////////////
            ///////////////////////////////   
            ///////////////////////////////   
            ///////////////////////////////  
            ///message_thread_safe_kinda///   
            ///////////////////////////////   
            ///////////////////////////////   
            ///////////////////////////////












            object _console_reader_object;
            _sec_received_messages = new _messager[_Max_Size_00];


            _sec_received_messages[4]._messager_list = new _messager[_Max_Size_00];
            _sec_received_messages[4]._message = "";
            _sec_received_messages[4]._originalMsg = "";
            _sec_received_messages[4]._messageCut = "";
            _sec_received_messages[4]._specialMessage = -1;
            _sec_received_messages[4]._specialMessageLineX = 0;
            _sec_received_messages[4]._specialMessageLineY = 0;
            _sec_received_messages[4]._orilineX = 0;
            _sec_received_messages[4]._orilineY = 0;
            _sec_received_messages[4]._lineX = 0;
            _sec_received_messages[4]._lineY = 0;
            _sec_received_messages[4]._lastOrilineX = 0;
            _sec_received_messages[4]._lastOrilineY = 0;
            _sec_received_messages[4]._count = 0;
            _sec_received_messages[4]._swtch0 = 1;
            _sec_received_messages[4]._swtch1 = 1;
            _sec_received_messages[4]._delay = 50;
            _sec_received_messages[4]._looping = 1;

            _console_reader_string._has_message_to_display = 0;
            _console_reader_string._console_reader_message = "";
            _console_reader_string._has_init = 0;
            _console_reader_object = _console_reader_string;



            //int _init_main_thread_in_out = 0;
            //int _init_main = 1;
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

            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            /*if (recEngine == null)
            {
                Console.WriteLine("null rec engine");
            }
            else
            {
                //Console.WriteLine("!null rec engine");

                //threadOneGrammarLoad();
                /*var updateMainUITitle = new Action(() =>
                {
                    if (_UIStarterItemz)
                    {
                        threadOneGrammarLoad();
                        _UIStarterItemz = false;
                    }

                    _mainUpdateThread();
                });

                Dispatcher.Invoke(updateMainUITitle);*/




            /*foreach (RecognizerInfo ri in SpeechRecognitionEngine.InstalledRecognizers())
            {
                //System.Diagnostics.Debug.WriteLine(ri.Culture.Name);

                Console.WriteLine(ri.Culture.Name);

            }
        }*/







            for (int j = 0; j < 1; j++)
            {
                try
                {
                    if (_mainFrameStarterItemz)
                    {
                        //Creating a thread individually... It has access to the UI by using System.Windows.Application.Current.Dispatcher.Invoke and is infinite in Time.
                        //STRICTLY CURRENTLY RESERVED FOR SPEECH RECOGNITION

                        // sc_ai_language = new SC_AI_Language();



                        /*var _thread = new Thread(() => _mainThreadStarter());
                        _thread.IsBackground = true;
                        _thread.Start();
                        _mainFrameStarterItemz = false;*/




                        // Some biolerplate to react to close window event, CTRL-C, kill, etc
                        _handler += new EventHandler(Handler);
                        SetConsoleCtrlHandler(_handler, true);

                        //start your multi threaded program here
                        //Process p = new Process();
                        //p.Start();

                        _mainTasker00 = new Thread((tester0000) =>
                        {
                            if (loop_main_thread == 0)
                            {

                            _thread_main_loop:

                                //hold the console so it doesn’t run off the end
                                if (exitSystem)
                                {
                                    //MessageBox((IntPtr)0,"shutting off", "SCCoreSystems",0);
                                    //Thread.Sleep(500);
                                    //loop_main_thread = 1;
                                    _mainFrameStarterItemz = false;
                                }

                                /*if (_main_received_messages[0]._welcomePackage == 998 || _main_received_messages[0]._welcomePackage == 999)
                                {

                                }*/

                                //////READ KEYBOARD=>
                                _keyboard_input.ReadKeyboard();
                                //////READ KEYBOARD<=

                                //Console.SetCursorPosition(1, 1);
                                //Console.WriteLine("test");

                                if (Console.WindowWidth != _last_console_width || Console.WindowHeight != _last_console_height)
                                {
                                    if (_counter_reset_console_borders > 50)
                                    {
                                        SC_GLOBALS_ACCESSORS.SC_CONSOLE_CORE._reset_console_borders();
                                        _last_console_width = Console.WindowWidth;
                                        _last_console_height = Console.WindowHeight;
                                        _counter_reset_console_borders = 0;
                                    }
                                    _counter_reset_console_borders++;
                                }

                                if (_start_thread_console_writer == 1)
                                {

                                    _console_writer_task = Task<object[]>.Factory.StartNew((tester0001) =>
                                    {

                                        var _initX = (Console.WindowWidth / 2) - (_program_name.Length / 2);
                                        var _initY = (Console.WindowHeight / 2);

                                        _sec_received_messages[0]._message = _program_name;
                                        _sec_received_messages[0]._originalMsg = _program_name;
                                        _sec_received_messages[0]._messageCut = _program_name;
                                        _sec_received_messages[0]._specialMessage = 0;
                                        _sec_received_messages[0]._specialMessageLineX = 0;
                                        _sec_received_messages[0]._specialMessageLineY = 0;
                                        _sec_received_messages[0]._orilineX = _initX;
                                        _sec_received_messages[0]._orilineY = _initY;
                                        _sec_received_messages[0]._lineX = _initX;
                                        _sec_received_messages[0]._lineY = _initY;
                                        _sec_received_messages[0]._lastOrilineX = _initX;
                                        _sec_received_messages[0]._lastOrilineY = _initY;
                                        _sec_received_messages[0]._count = 0;
                                        _sec_received_messages[0]._swtch0 = 1;
                                        _sec_received_messages[0]._swtch1 = 1;
                                        _sec_received_messages[0]._delay = 5;
                                        _sec_received_messages[0]._looping = 1;

                                        _worker_000_has_init = 1;

                                    //////CONSOLE WRITER=>
                                    _thread_loop_console:

                                        _sec_received_messages = SC_GLOBALS_ACCESSORS.SC_CONSOLE_WRITER._console_writer(_sec_received_messages);

                                        Thread.Sleep(1);

                                        goto _thread_loop_console;
                                        //////CONSOLE WRITER <=
                                    }, _main_received_messages);
                                    _start_thread_console_writer = 2;
                                }

                                //CONFIRM CONSOLE WRITER IS WORKING=>
                                if (_worker_000_has_init == 1)
                                {
                                    if (SC_GLOBALS_ACCESSORS != null)
                                    {
                                        if (SC_GLOBALS_ACCESSORS.SC_CONSOLE_WRITER != null)
                                        {
                                            /*_sec_received_messages[1]._message = "C-WR" + " ENABLED";
                                            _sec_received_messages[1]._originalMsg = "C-WR" + " ENABLED";
                                            _sec_received_messages[1]._messageCut = "C-WR" + " ENABLED";
                                            _sec_received_messages[1]._specialMessage = 2;
                                            _sec_received_messages[1]._specialMessageLineX = 0;
                                            _sec_received_messages[1]._specialMessageLineY = 0;
                                            _sec_received_messages[1]._orilineX = 1;
                                            _sec_received_messages[1]._orilineY = Console.WindowHeight - 2;
                                            _sec_received_messages[1]._lineX = 1;
                                            _sec_received_messages[1]._lineY = Console.WindowHeight - 2;
                                            _sec_received_messages[1]._lastOrilineX = _sec_received_messages[1]._lineX;
                                            _sec_received_messages[1]._lastOrilineY = _sec_received_messages[1]._lineY;
                                            _sec_received_messages[1]._count = 0;
                                            _sec_received_messages[1]._swtch0 = 1;
                                            _sec_received_messages[1]._swtch1 = 0;
                                            _sec_received_messages[1]._delay = 10;
                                            _sec_received_messages[1]._looping = 0;*/

                                            _worker_000_has_init = 2;
                                        }
                                    }
                                }
                                //CONFIRM CONSOLE WRITER IS WORKING<=

                                //CONFIRM CONSOLE READER IS WORKING=>
                                if (_worker_000_has_init == 2)
                                {
                                    if (SC_GLOBALS_ACCESSORS != null)
                                    {
                                        if (SC_GLOBALS_ACCESSORS.SC_CONSOLE_READER != null)
                                        {
                                            /*_sec_received_messages[2]._message = "core C-RE" + " ENABLED";
                                            _sec_received_messages[2]._originalMsg = "core C-RE" + " ENABLED";
                                            _sec_received_messages[2]._messageCut = "core C-RE" + " ENABLED";
                                            _sec_received_messages[2]._specialMessage = 2;
                                            _sec_received_messages[2]._specialMessageLineX = 0;
                                            _sec_received_messages[2]._specialMessageLineY = 1;
                                            _sec_received_messages[2]._orilineX = _sec_received_messages[2]._message.Length + 3;
                                            _sec_received_messages[2]._orilineY = Console.WindowHeight - 2;
                                            _sec_received_messages[2]._lineX = _sec_received_messages[2]._message.Length + 3;
                                            _sec_received_messages[2]._lineY = Console.WindowHeight - 2;
                                            _sec_received_messages[2]._count = 0;
                                            _sec_received_messages[2]._swtch0 = 1;
                                            _sec_received_messages[2]._swtch1 = 0;
                                            _sec_received_messages[2]._delay = 10;
                                            _sec_received_messages[2]._looping = 0;*/

                                            _worker_000_has_init = 3;
                                        }
                                    }
                                }
                                //CONFIRM CONSOLE READER IS WORKING<=





                                if (_worker_000_has_init == 3)
                                {
                                    if (SC_GLOBALS_ACCESSORS != null)
                                    {
                                        if (SC_GLOBALS_ACCESSORS.SC_CONSOLE_READER != null)
                                        {
                                            var _program_name0 = "Press Enter";
                                            _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                            _initY0 = (Console.WindowHeight / 2) + 1;

                                            _sec_received_messages[3]._message = _program_name0;
                                            _sec_received_messages[3]._originalMsg = _program_name0;
                                            _sec_received_messages[3]._messageCut = _program_name0;
                                            _sec_received_messages[3]._specialMessage = 2;
                                            _sec_received_messages[3]._specialMessageLineX = 0;
                                            _sec_received_messages[3]._specialMessageLineY = 0;
                                            _sec_received_messages[3]._orilineX = _initX0;
                                            _sec_received_messages[3]._orilineY = _initY0;
                                            _sec_received_messages[3]._lineX = _initX0;
                                            _sec_received_messages[3]._lineY = _initY0;
                                            _sec_received_messages[3]._count = 0;
                                            _sec_received_messages[3]._swtch0 = 1;
                                            _sec_received_messages[3]._swtch1 = 1;
                                            _sec_received_messages[3]._delay = 100;
                                            _sec_received_messages[3]._looping = 1;
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
                                                _console_reader_string = SC_GLOBALS_ACCESSORS.SC_CONSOLE_READER._console_reader(_console_reader_object);
                                            }

                                            if (SC_GLOBALS_ACCESSORS.SC_CONSOLE_READER._main_has_init == 1)
                                            {
                                                _console_reader_string._console_reader_message = "";
                                                _console_reader_string._has_message_to_display = 0;

                                                var _program_name0 = "WELCOME";
                                                _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                                _initY0 = (Console.WindowHeight / 2) - 1;

                                                _sec_received_messages[4]._message = _program_name0;
                                                _sec_received_messages[4]._originalMsg = _program_name0;
                                                _sec_received_messages[4]._messageCut = _program_name0;
                                                _sec_received_messages[4]._specialMessage = 2;
                                                _sec_received_messages[4]._specialMessageLineX = 0;
                                                _sec_received_messages[4]._specialMessageLineY = 0;
                                                _sec_received_messages[4]._orilineX = _initX0;
                                                _sec_received_messages[4]._orilineY = _initY0;
                                                _sec_received_messages[4]._lineX = _initX0;
                                                _sec_received_messages[4]._lineY = _initY0;
                                                _sec_received_messages[4]._count = 0;
                                                _sec_received_messages[4]._swtch0 = 1;
                                                _sec_received_messages[4]._swtch1 = 0;
                                                _sec_received_messages[4]._delay = 200;
                                                _sec_received_messages[4]._looping = 0;

                                                _sec_received_messages[0]._swtch0 = 0;
                                                _sec_received_messages[0]._swtch1 = 0;
                                                _sec_received_messages[3]._swtch0 = 0;
                                                _sec_received_messages[3]._swtch1 = 0;

                                                _program_name0 = "Please Enter your Username: ";
                                                _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                                _initY0 = (Console.WindowHeight / 2) + 2;

                                                _sec_received_messages[6]._message = _program_name0;
                                                _sec_received_messages[6]._originalMsg = _program_name0;
                                                _sec_received_messages[6]._messageCut = _program_name0;
                                                _sec_received_messages[6]._specialMessage = 2;
                                                _sec_received_messages[6]._specialMessageLineX = 0;
                                                _sec_received_messages[6]._specialMessageLineY = 0;
                                                _sec_received_messages[6]._orilineX = _initX0;
                                                _sec_received_messages[6]._orilineY = _initY0;
                                                _sec_received_messages[6]._lineX = _initX0;
                                                _sec_received_messages[6]._lineY = _initY0;
                                                _sec_received_messages[6]._count = 0;
                                                _sec_received_messages[6]._swtch0 = 1;
                                                _sec_received_messages[6]._swtch1 = 1;
                                                _sec_received_messages[6]._delay = 50;
                                                _sec_received_messages[6]._looping = 1;

                                                Console.SetCursorPosition(_initX0 + _program_name0.Length, _initY0);

                                                startThread = 3;
                                                SC_GLOBALS_ACCESSORS.SC_CONSOLE_READER._main_has_init = 2;
                                            }

                                            if (startThread == 3 && _console_reader_string._has_message_to_display == 1)
                                            {

                                                if (_console_reader_string._console_reader_message.ToLower() == "nine" || _console_reader_string._console_reader_message.ToLower() == "ninekorn" || _console_reader_string._console_reader_message.ToLower() == "9")
                                                {

                                                    var _program_name0 = "Access Authorized";
                                                    _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                                    _initY0 = (Console.WindowHeight / 2) + 2;

                                                    _sec_received_messages[6]._message = _program_name0;
                                                    _sec_received_messages[6]._originalMsg = _program_name0;
                                                    _sec_received_messages[6]._messageCut = _program_name0;
                                                    _sec_received_messages[6]._specialMessage = 2;
                                                    _sec_received_messages[6]._specialMessageLineX = 0;
                                                    _sec_received_messages[6]._specialMessageLineY = 0;
                                                    _sec_received_messages[6]._lineX = _initX0;
                                                    _sec_received_messages[6]._lineY = _initY0;
                                                    _sec_received_messages[6]._count = 0;
                                                    _sec_received_messages[6]._swtch0 = 1;
                                                    _sec_received_messages[6]._swtch1 = 0;
                                                    _sec_received_messages[6]._delay = 50;
                                                    _sec_received_messages[6]._looping = 0;

                                                    for (int L0_IN = 0; L0_IN < _main_received_messages.Length; L0_IN++)
                                                    {
                                                        _main_received_messages[L0_IN]._passTest = _console_reader_string._console_reader_message.ToLower();
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

                                                    _sec_received_messages[6]._message = _program_name0;
                                                    _sec_received_messages[6]._originalMsg = _program_name0;
                                                    _sec_received_messages[6]._messageCut = _program_name0;
                                                    _sec_received_messages[6]._specialMessage = 2;
                                                    _sec_received_messages[6]._specialMessageLineX = 0;
                                                    _sec_received_messages[6]._specialMessageLineY = 0;
                                                    _sec_received_messages[6]._lineX = _initX0;
                                                    _sec_received_messages[6]._lineY = _initY0;
                                                    _sec_received_messages[6]._count = 0;
                                                    _sec_received_messages[6]._swtch0 = 1;
                                                    _sec_received_messages[6]._swtch1 = 0;
                                                    _sec_received_messages[6]._delay = 50;
                                                    _sec_received_messages[6]._looping = 0;

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

                                                        _sec_received_messages[6]._message = _program_name0;
                                                        _sec_received_messages[6]._originalMsg = _program_name0;
                                                        _sec_received_messages[6]._messageCut = _program_name0;
                                                        _sec_received_messages[6]._specialMessage = 2;
                                                        _sec_received_messages[6]._specialMessageLineX = 0;
                                                        _sec_received_messages[6]._specialMessageLineY = 0;
                                                        _sec_received_messages[6]._lineX = _initX0;
                                                        _sec_received_messages[6]._lineY = _initY0;
                                                        _sec_received_messages[6]._count = 0;
                                                        _sec_received_messages[6]._swtch0 = 1;
                                                        _sec_received_messages[6]._swtch1 = 0;
                                                        _sec_received_messages[6]._delay = 50;
                                                        _sec_received_messages[6]._looping = 0;


                                                        _lastMenuOption = _console_reader_string._console_reader_message.ToLower();
                                                        _console_reader_string._console_reader_message = "";

                                                        _main_received_messages[0]._received_switch_in = 1;
                                                        _main_received_messages[0]._received_switch_out = 1;
                                                        _main_received_messages[0]._sending_switch_in = 1;
                                                        _main_received_messages[0]._sending_switch_out = 1;
                                                        _main_received_messages[0]._welcomePackage = 999;

                                                        _main_received_messages = sc_console_menu.sc_console_menu_00._console_menu(_main_received_messages);
                                                        Console.SetCursorPosition(_initX0, _initY0 + 1);
                                                        _some_other_swtch = 1;
                                                    }
                                                    else if (_console_reader_string._console_reader_message.ToLower() == "standard" ||
                                                            _console_reader_string._console_reader_message.ToLower() == "std")
                                                    {

                                                        _lastMenuOption = _console_reader_string._console_reader_message.ToLower();
                                                        _console_reader_string._console_reader_message = "";

                                                        _main_received_messages[0]._received_switch_in = 1;
                                                        _main_received_messages[0]._received_switch_out = 1;
                                                        _main_received_messages[0]._sending_switch_in = 1;
                                                        _main_received_messages[0]._sending_switch_out = 1;
                                                        _main_received_messages[0]._welcomePackage = 998;
                                                        _main_received_messages = sc_console_menu.sc_console_menu_00._console_menu(_main_received_messages);
                                                        Console.SetCursorPosition(_initX0, _initY0 + 1);
                                                        _some_other_swtch = 1;
                                                    }
                                                }
                                                else
                                                {

                                                    var _program_name0 = "Option Not Implemented";
                                                    _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                                    _initY0 = (Console.WindowHeight / 2) + 2;

                                                    _sec_received_messages[6]._message = _program_name0;
                                                    _sec_received_messages[6]._originalMsg = _program_name0;
                                                    _sec_received_messages[6]._messageCut = _program_name0;
                                                    _sec_received_messages[6]._specialMessage = 2;
                                                    _sec_received_messages[6]._specialMessageLineX = 0;
                                                    _sec_received_messages[6]._specialMessageLineY = 0;
                                                    _sec_received_messages[6]._lineX = _initX0;
                                                    _sec_received_messages[6]._lineY = _initY0;
                                                    _sec_received_messages[6]._count = 0;
                                                    _sec_received_messages[6]._swtch0 = 1;
                                                    _sec_received_messages[6]._swtch1 = 0;
                                                    _sec_received_messages[6]._delay = 50;
                                                    _sec_received_messages[6]._looping = 0;

                                                    _lastMenuOption = "";
                                                    _console_reader_string._console_reader_message = "";

                                                    Console.SetCursorPosition(_initX0, _initY0);
                                                }
                                            }

                                            Thread.Sleep(1);
                                        }
                                    }, _main_received_messages);
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
                                                int _welcomePackage00 = _main_received_messages[0]._welcomePackage;

                                                if (_welcomePackage00 == 0)
                                                {
                                                    _main_received_messages = sc_console_menu.sc_console_menu_00._console_menu(_main_received_messages);
                                                }
                                                else if (_welcomePackage00 == 1)
                                                {
                                                    int _current_menu00 = _data00_OUT._current_menu;

                                                    if (_lastMenu != _current_menu00)
                                                    {
                                                        var _program_name0 = _current_menu00 + "";
                                                        _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                                        _initY0 = (Console.WindowHeight / 2) + 9;
                                                        _sec_received_messages[5]._message = _program_name0;
                                                        _sec_received_messages[5]._originalMsg = _program_name0;
                                                        _sec_received_messages[5]._messageCut = _program_name0;
                                                        _sec_received_messages[5]._specialMessage = 2;
                                                        _sec_received_messages[5]._specialMessageLineX = 0;
                                                        _sec_received_messages[5]._specialMessageLineY = 0;
                                                        _sec_received_messages[5]._lineX = _initX0;
                                                        _sec_received_messages[5]._lineY = _initY0;
                                                        _sec_received_messages[5]._count = 0;
                                                        _sec_received_messages[5]._swtch0 = 1;
                                                        _sec_received_messages[5]._swtch1 = 0;
                                                        _sec_received_messages[5]._delay = 50;
                                                        _sec_received_messages[5]._looping = 0;
                                                    }

                                                    if (_current_menu00 == -1)
                                                    {
                                                        _data00_IN._received_switch_in = 0;
                                                        _data00_IN._received_switch_out = 0;
                                                        _data00_IN._sending_switch_in = 0;
                                                        _data00_IN._sending_switch_out = 0;

                                                        _data00_IN._current_menu = _data00_OUT._current_menu;
                                                        _data00_IN._menuOption = _lastMenuOption;

                                                        var objecterer = _data00_IN;
                                                        _data00_OUT = sc_console_menu.sc_console_menu_01._console_menu(objecterer);

                                                        _lastMenu = _data00_OUT._current_menu;
                                                        _lastMenuOption = "";
                                                    }
                                                    else if (_current_menu00 == 0)
                                                    {
                                                        var _program_name0 = _current_menu00 + "";
                                                        _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                                        _initY0 = (Console.WindowHeight / 2) + 9;
                                                        _sec_received_messages[5]._message = _program_name0;
                                                        _sec_received_messages[5]._originalMsg = _program_name0;
                                                        _sec_received_messages[5]._messageCut = _program_name0;
                                                        _sec_received_messages[5]._specialMessage = 2;
                                                        _sec_received_messages[5]._specialMessageLineX = 0;
                                                        _sec_received_messages[5]._specialMessageLineY = 0;
                                                        _sec_received_messages[5]._lineX = _initX0;
                                                        _sec_received_messages[5]._lineY = _initY0;
                                                        _sec_received_messages[5]._count = 0;
                                                        _sec_received_messages[5]._swtch0 = 1;
                                                        _sec_received_messages[5]._swtch1 = 0;
                                                        _sec_received_messages[5]._delay = 50;
                                                        _sec_received_messages[5]._looping = 0;

                                                        _data00_IN._received_switch_in = 0;
                                                        _data00_IN._received_switch_out = 0;
                                                        _data00_IN._sending_switch_in = 0;
                                                        _data00_IN._sending_switch_out = 0;

                                                        _data00_IN._current_menu = _data00_OUT._current_menu;
                                                        _data00_IN._menuOption = _lastMenuOption;

                                                        var objecterer = _data00_IN;
                                                        _data00_OUT = sc_console_menu.sc_console_menu_01._console_menu(objecterer);
                                                        _lastMenu = _data00_OUT._current_menu;
                                                        _lastMenuOption = "";
                                                    }
                                                    else if (_current_menu00 == 1)
                                                    {
                                                        var _program_name0 = _current_menu00 + "";
                                                        _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                                        _initY0 = (Console.WindowHeight / 2) + 9;
                                                        _sec_received_messages[5]._message = _program_name0;
                                                        _sec_received_messages[5]._originalMsg = _program_name0;
                                                        _sec_received_messages[5]._messageCut = _program_name0;
                                                        _sec_received_messages[5]._specialMessage = 2;
                                                        _sec_received_messages[5]._specialMessageLineX = 0;
                                                        _sec_received_messages[5]._specialMessageLineY = 0;
                                                        _sec_received_messages[5]._lineX = _initX0;
                                                        _sec_received_messages[5]._lineY = _initY0;
                                                        _sec_received_messages[5]._count = 0;
                                                        _sec_received_messages[5]._swtch0 = 1;
                                                        _sec_received_messages[5]._swtch1 = 0;
                                                        _sec_received_messages[5]._delay = 50;
                                                        _sec_received_messages[5]._looping = 0;

                                                        _data00_IN._received_switch_in = 0;
                                                        _data00_IN._received_switch_out = 0;
                                                        _data00_IN._sending_switch_in = 0;
                                                        _data00_IN._sending_switch_out = 0;

                                                        _data00_IN._current_menu = _data00_OUT._current_menu;
                                                        _data00_IN._menuOption = _lastMenuOption;

                                                        var objecterer = _data00_IN;
                                                        _data00_OUT = sc_console_menu.sc_console_menu_01._console_menu(objecterer);
                                                        _lastMenu = _data00_OUT._current_menu;
                                                        _lastMenuOption = "";
                                                    }
                                                }
                                            }
                                            Thread.Sleep(1);
                                        }
                                    }, _main_received_messages);
                                    _worker_000_has_init = 6;
                                }






                                if (_some_other_swtch == 1)
                                {
                                    if (has_init_directx == 0)
                                    {
                                        if (init_directX_main_swtch == 2 || init_vr_main_swtch == 2)
                                        {
                                            if (init_directX_main_swtch == 2)
                                            {
                                                config = new sc_system_configuration("sc core systems", 1920, 1080, false, false);

                                                consoleHandle = sc_console_core.handle;// SC_GLOBALS_ACCESSORS.SC_CONSOLE_CORE.handle;

                                                if (consoleHandle == IntPtr.Zero)
                                                {
                                                    //MessageBox((IntPtr)0, "null console ", "_sc_core_systems error", 0);
                                                }
                                                else
                                                {
                                                    //MessageBox((IntPtr)0, "!null console ", "_sc_core_systems error", 0);
                                                }
                                                sc_update = new SC_Update();
                                                /*
                                                for (int x = 0; x < Console.WindowWidth; x++)
                                                {
                                                    for (int y = 0; y < Console.WindowWidth; y++)
                                                    {
                                                        SC_GLOBALS_ACCESSORS.SC_CONSOLE_WRITER.Draw(x, y, " ");
                                                    }
                                                }

                                                sc_update = new SC_Update();*/
                                            }



                                            if (init_vr_main_swtch == 2)
                                            {
                                                config = new sc_system_configuration("sc core systems", 1920, 1080, false, false);
                                                consoleHandle = sc_console_core.handle;// SC_GLOBALS_ACCESSORS.SC_CONSOLE_CORE.handle;

                                                if (consoleHandle == IntPtr.Zero)
                                                {
                                                    MessageBox((IntPtr)0, "null console ", "_sc_core_systems error", 0);
                                                }
                                                else
                                                {
                                                    MessageBox((IntPtr)0, "!null console ", "_sc_core_systems error", 0);
                                                }
                                                sc_update = new SC_Update();

                                                /*handler = SC_GLOBALS_ACCESSORS.SC_CONSOLE_CORE.handle;

                                                if (handler == IntPtr.Zero)
                                                {
                                                    MessageBox((IntPtr)0, "test 00: ", "_sc_core_systems error", 0);
                                                }

                                                for (int x = 0; x < Console.WindowWidth; x++)
                                                {
                                                    for (int y = 0; y < Console.WindowWidth; y++)
                                                    {
                                                        SC_GLOBALS_ACCESSORS.SC_CONSOLE_WRITER.Draw(x, y, " ");
                                                    }
                                                }

                                                sc_update = new SC_Update();*/
                                            }

                                            has_init_directx = 1;
                                        }
                                    }


                                    if (has_init_directx == 1)
                                    {

                                    }
                                }
                                Thread.Sleep(0);
                                goto _thread_main_loop;
                            }

                            //_thread_start:


                        }, 0); //100000 //999999999

                        _mainTasker00.IsBackground = false;
                        _mainTasker00.SetApartmentState(ApartmentState.STA);
                        _mainTasker00.Start();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    //SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue(ex.ToString(), 0, 20);
                }
                finally
                {

                    if (_console_worker_task != null)
                    {
                        _console_worker_task.Dispose();
                        _console_worker_task = null;
                    }
                    if (_console_reader_task != null)
                    {
                        _console_reader_task.Dispose();
                        _console_reader_task = null;
                    }
                    if (_console_writer_task != null)
                    {
                        _console_writer_task.Dispose();
                        _console_writer_task = null;
                    }
                    if (_mainTasker00 != null)
                    {
                        //_mainTasker00.Suspend();
                        //_mainTasker00.Abort();
                        _mainTasker00 = null;
                    }
                }
            }

            //ARE COMPONENTS LOADED???
            /*if (_isComponents == false)
            {
                //the main documents/page wpf cannot be loaded. // we continue the program anyway. Later on, my program will start from DOS... fuck it.
            }
            else
            {
                //the main documents/page wpf is loaded. // 
            }*/
        }










        //https://stackoverflow.com/questions/1119841/net-console-application-exit-event
        static bool exitSystem = false;
        //#region Trap application termination
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);
        private delegate bool EventHandler(CtrlType sig);
        static EventHandler _handler;
        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        private static bool Handler(CtrlType sig)
        {
            //Console.WriteLine("Exiting system due to external CTRL-C, or process kill, or shutdown");

            //do your cleanup here
            //Thread.Sleep(5000); //simulate some cleanup delay

            //Console.WriteLine("Cleanup complete");

            //allow main to run off
            if (sc_update != null)
            {
                sc_update.ShutDown();
                sc_update = null;
            }

            //MessageBox((IntPtr)0, "shutting off", "SCCoreSystems", 0);
            exitSystem = true;

            //shutdown right away so there are no lingering threads
            Environment.Exit(-1);




            return true;
        }
        //#endregion










        public static bool _mainThreadStarterItemsBool = true;
        public static int _mainThreadFrameCounter = 0;

        public static workerThreads _workThread;

        public static void _mainThreadStarter()
        {
            try
            {
                while (true)
                {

                    /*var updateMainUITitle = new Action(() =>
                    {
                        if (_UIStarterItemz == 1)
                        {
                            threadOneGrammarLoad();
                            _UIStarterItemz = 0;
                        }

                        _mainUpdateThread();
                    });

                    Dispatcher.Invoke(updateMainUITitle);*/ //System.Windows.Application.Current.

                    if (_mainThreadStarterItemsBool)
                    {
                        var _threadID = 0;
                        //threadOneGrammarLoad();

                        //Creating multiple threads. Can be sent with invoke or without it seems. If created inside of another thread, I think it would be best.
                        /*for (int i = 0; i < _totalThreads; i++)
                        {
                            _workThread = new workerThreads(i);
                        }*/

                        /*var updateMainUITitle = new Action(() =>
                        {
                            if (_UIStarterItemz == 1)
                            {
                                threadOneGrammarLoad();
                                _UIStarterItemz = 0;
                            }

                            _mainUpdateThread();
                        });

                        Dispatcher.Invoke(updateMainUITitle);*/
                        //threadOneGrammarLoad();
                        _mainThreadStarterItemsBool = false;
                    }

                    _mainThreadFrameCounter++;
                    Thread.Sleep(1);
                }
            }
            catch
            {

            }
        }

        public static int _UIStarterItemz = 1;

        //UI THREAD TEST
        //////////////////////////////////
        //////////////////////////////////
        public static void _mainUpdateThread()
        {


            Console.Title = "" + _mainThreadFrameCounter.ToString();

            //SC_GCGollect.GCCollectUtility(100);
        }
        //////////////////////////////////
        //////////////////////////////////



        public class workerThreads
        {
            public int _mainFrameCounterThreadOne = 0;
            public int _availableThreads = 0;
            public ThreadStart _threadStart;
            public Thread[] _listOfThreads;
            public int _threadID;
            public Thread _thread;


            public workerThreads(int threadID)
            {

                this._threadID = threadID;

                _threadStart = new ThreadStart(() =>
                {
                    threadOneMainDispatcherUpdate(_threadID);
                });

                _thread = new Thread(_threadStart);
                _thread.IsBackground = true;
                _thread.SetApartmentState(ApartmentState.STA);
                _thread.Start();
            }


            public static int _whosFirst = 0;
            public static int _threadCreationCounter = 0;
            public static bool _canLoadStarterItems = true;
            //[STAThread]
            void threadOneMainDispatcherUpdate(int threadIndex)
            {
                int _threadIndex = threadIndex;
                int _workerFrame = 0;

                try
                {
                    //SC_Console.consoleMessageQueue messageQueue00 = new SC_Console.consoleMessageQueue("SC_AvailableThreads#" + _totalThreads, 0, 0);
                    //SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue("#" + _threadIndex + "", 0, 3 + _threadIndex);


                    while (true)
                    {
                        //List of functions that the multithreaded app will start every frames.
                        ////////////////////
                        //_threadUpdateTest();
                        ////////////////////
                        if (_canLoadStarterItems)
                        {
                            //Console.WriteLine("grammar load");
                            //threadOneGrammarLoad();
                            _canLoadStarterItems = false;
                        }


                        if (_workerFrame > -1)
                        {
                            _threadCreationCounter++;
                            _workerFrame = 0;
                        }

                        _workerFrame++;
                        Thread.Sleep(1);
                    }
                }
                //#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
                //#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {
                    SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue(ex.ToString(), 0, 20);
                }
            }
        }


        //static SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();

        /*static bool mathSwitch = false;
        static string[] stringOfMathOps;
        static bool firstInput = false;
        static bool secondInput = false;*/
        static List<string> currentListOfCommands = new List<string>();

        /*static int functionCounter00 = 1;
        static int functionCounter01 = 1;
        static int functionCounter02 = 1;*/
        static int functionCounter03 = 1;

        static string lastWord = "";

        //static string totalCombination = "";

        //static string currentWord = "";

        static int counterTotalWords = 0;

        //static int frameCounterForVoiceRecognition = 0;
        static int frameCounterForVoiceRecognitionRecognizedWords = 0;


        //https://docs.microsoft.com/en-us/dotnet/api/system.speech.recognition.speechrecognitionengine.recognizeasync?view=netframework-4.8
        static bool completed;


        //private static SpeechRecognitionEngine recEnginer =new SpeechRecognitionEngine("en-GB");
        /*
        public static void threadOneGrammarLoad()
        {
            /*SpeechRecognitionEngine recEnginer = new SpeechRecognitionEngine(new CultureInfo("en-US"));
            //Console.WriteLine("test0");

            Choices mychoices = new Choices();
            mychoices.Add(new string[] { "Ok", "Test", "Hello" });
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(mychoices);
            Grammar mygrammar = new Grammar(gb);
            recEnginer.LoadGrammarAsync(mygrammar);

            recEnginer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recEngine_SpeechRecognized);

            recEnginer.SetInputToDefaultAudioDevice();
            recEnginer.RecognizeAsync(RecognizeMode.Multiple);*/
            /*using (SpeechRecognitionEngine recognizer =
           new SpeechRecognitionEngine(new CultureInfo("en-US")))
            {
                // Create a grammar for choosing cities for a flight.  
                Choices cities = new Choices(new string[]
                { "Los Angeles", "New York", "Chicago", "San Francisco", "Miami", "Dallas" });

                GrammarBuilder gb = new GrammarBuilder();
                gb.Append("I want to fly from");
                gb.Append(cities);
                gb.Append("to");
                gb.Append(cities);

                // Construct a Grammar object and load it to the recognizer.  
                Grammar cityChooser = new Grammar(gb);
                cityChooser.Name = ("City Chooser");
                recognizer.LoadGrammarAsync(cityChooser);

                // Attach event handlers.  
                recognizer.SpeechDetected +=
                  new EventHandler<SpeechDetectedEventArgs>(
                    SpeechDetectedHandler);
                recognizer.SpeechHypothesized +=
                  new EventHandler<SpeechHypothesizedEventArgs>(
                    SpeechHypothesizedHandler);
                recognizer.SpeechRecognitionRejected +=
                  new EventHandler<SpeechRecognitionRejectedEventArgs>(
                    SpeechRecognitionRejectedHandler);
                recognizer.SpeechRecognized +=
                  new EventHandler<SpeechRecognizedEventArgs>(
                    SpeechRecognizedHandler);
                recognizer.RecognizeCompleted +=
                  new EventHandler<RecognizeCompletedEventArgs>(
                    RecognizeCompletedHandler);

                // Assign input to the recognizer and start an asynchronous  
                // recognition operation.  
                recognizer.SetInputToDefaultAudioDevice();

                completed = false;
                Console.WriteLine("Starting asynchronous recognition...");
                recognizer.RecognizeAsync();

                // Wait for the operation to complete.  
                while (!completed)
                {
                    Thread.Sleep(333);
                }
                Console.WriteLine("Done.");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine(new CultureInfo("en-US"));
            recEngine = new SpeechRecognitionEngine();
            Choices commands = new Choices();
            commands.Add(new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" });
            //commands.Add(new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" });

            //"letter", "letters", "alphabet", 

            //commands.Add(new string[] { "number", "numbers", "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" });
            //commands.Add(new string[] { "hundred", "thousand", "million", "billion", "trillion" });
            //commands.Add(new string[] { "math", "maths", "plus", "minus", "divide", "multiply", "multiplied", "equal", "square", "square root", "cos", "sin", "tan" });
            //commands.Add(new string[] { "console", "program", "directory", "start", "plus" });

            ////////////////COMMANDS FOR CALCULATOR/////////////////////
            ////////////////////////////////////////////////////////////
            //commands.Add(new string[] { "calculations", "calculator" });
            ////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////
            //List<string> words = System.IO.File.ReadAllText("MyWords.txt").Split(new string[] { Environment.NewLine }).ToList();
            ///ok wow. i dont need to build my own spectrum crap and how many days/weeks/months/years would it take me to code it... but damn i learned a ton
            ///in the process anyway. Next step. How do we load a c# library in Unity and use it? OMG this stuff is powerfull.
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar grammar = new Grammar(gBuilder);

            recEngine.LoadGrammarAsync(grammar);

            recEngine.SetInputToDefaultAudioDevice();
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
            Console.WriteLine("test1");
        }
        // Handle the SpeechDetected event.  
        static void SpeechDetectedHandler(object sender, SpeechDetectedEventArgs e)
        {
            Console.WriteLine(" In SpeechDetectedHandler:");
            Console.WriteLine(" - AudioPosition = {0}", e.AudioPosition);
        }

        // Handle the SpeechHypothesized event.  
        static void SpeechHypothesizedHandler(
          object sender, SpeechHypothesizedEventArgs e)
        {
            Console.WriteLine(" In SpeechHypothesizedHandler:");

            string grammarName = "<not available>";
            string resultText = "<not available>";
            if (e.Result != null)
            {
                if (e.Result.Grammar != null)
                {
                    grammarName = e.Result.Grammar.Name;
                }
                resultText = e.Result.Text;
            }

            Console.WriteLine(" - Grammar Name = {0}; Result Text = {1}",
              grammarName, resultText);
        }

        // Handle the SpeechRecognitionRejected event.  
        static void SpeechRecognitionRejectedHandler(
          object sender, SpeechRecognitionRejectedEventArgs e)
        {
            Console.WriteLine(" In SpeechRecognitionRejectedHandler:");

            string grammarName = "<not available>";
            string resultText = "<not available>";
            if (e.Result != null)
            {
                if (e.Result.Grammar != null)
                {
                    grammarName = e.Result.Grammar.Name;
                }
                resultText = e.Result.Text;
            }

            Console.WriteLine(" - Grammar Name = {0}; Result Text = {1}",
              grammarName, resultText);
        }

        // Handle the SpeechRecognized event.  
        static void SpeechRecognizedHandler(
          object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine(" In SpeechRecognizedHandler.");

            string grammarName = "<not available>";
            string resultText = "<not available>";
            if (e.Result != null)
            {
                if (e.Result.Grammar != null)
                {
                    grammarName = e.Result.Grammar.Name;
                }
                resultText = e.Result.Text;
            }

            Console.WriteLine(" - Grammar Name = {0}; Result Text = {1}",
              grammarName, resultText);
        }

        // Handle the RecognizeCompleted event.  
        static void RecognizeCompletedHandler(
          object sender, RecognizeCompletedEventArgs e)
        {
            Console.WriteLine(" In RecognizeCompletedHandler.");

            if (e.Error != null)
            {
                Console.WriteLine(
                  " - Error occurred during recognition: {0}", e.Error);
                return;
            }
            if (e.InitialSilenceTimeout || e.BabbleTimeout)
            {
                Console.WriteLine(
                  " - BabbleTimeout = {0}; InitialSilenceTimeout = {1}",
                  e.BabbleTimeout, e.InitialSilenceTimeout);
                return;
            }
            if (e.InputStreamEnded)
            {
                Console.WriteLine(
                  " - AudioPosition = {0}; InputStreamEnded = {1}",
                  e.AudioPosition, e.InputStreamEnded);
            }
            if (e.Result != null)
            {
                Console.WriteLine(
                  " - Grammar = {0}; Text = {1}; Confidence = {2}",
                  e.Result.Grammar.Name, e.Result.Text, e.Result.Confidence);
                Console.WriteLine(" - AudioPosition = {0}", e.AudioPosition);
            }
            else
            {
                Console.WriteLine(" - No result.");
            }

            completed = true;
        }
        private static int _hitCounter = 0;
        static void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //currentWord = e.Result.Text;
            Console.WriteLine("test" + _hitCounter);


            _hitCounter++;
            switch (e.Result.Text)
            {
                case "a":
                    lastWord = "a";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "b":
                    lastWord = "b";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "c":
                    lastWord = "c";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "d":
                    lastWord = "d";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "e":
                    lastWord = "e";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "f":
                    lastWord = "f";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "g":
                    lastWord = "g";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "h":
                    lastWord = "h";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "i":
                    lastWord = "i";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "j":
                    lastWord = "j";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "k":
                    lastWord = "k";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "l":
                    lastWord = "l";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "m":
                    lastWord = "m";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "n":
                    lastWord = "n";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "o":
                    lastWord = "o";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "p":
                    lastWord = "p";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "q":
                    lastWord = "q";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "r":
                    lastWord = "r";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "s":
                    lastWord = "s";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "t":
                    lastWord = "t";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "u":
                    lastWord = "u";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "v":
                    lastWord = "v";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "w":
                    lastWord = "w";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "x":
                    lastWord = "x";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "y":
                    lastWord = "y";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "z":
                    lastWord = "z";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
            }
        }*/

















        //SC_GCGollect _GCollector;

        public static void _threadUpdateTest()
        {



            //lookForKeyPress();
            //SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue("V" + "", 0, 1);
        }
















        //KEY PRESS AND VOICE RECORDER/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //********************************
        //********************************
        //********************************
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        //static bool mainCaptureSwitch = true;
        //static Computer c = new Computer();
        //static Computer a = new Computer();
        /*static string fullSubFolderPath = "";
        static string folderName = "";
        static int lastFrame = -1;
        static Process p;
        static int numberOfStartedRecordings = 0;

        static bool _pressOnce = true;
        static int _iterations = 0;*/

        /*
        public static void lookForKeyPress()
        {

            for (int i = 0; i < 1; i++)
            {

                if (System.Windows.Input.Keyboard.IsKeyDown(Key.R))
                {
                    if (_iterations > 0)
                    {
                        break;
                    }
                    SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue("test" + _iterations, 0, 15 + _iterations);
                    //SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue("#", 0, 15);


                    // training set for an NAND function
                    TrainingItem[] trainingSet =
                    {
                          new TrainingItem(true, 1, 0, 0),
                          new TrainingItem(true, 1, 0, 1),
                          new TrainingItem(true, 1, 1, 0),
                          new TrainingItem(false, 1, 1, 1)
                    };
                    // create a perceptron with 3 inputs
                    var perceptron = new BinaryPerceptron(3);

                    int attemptCount = 0;
                    // teach the neural network until all the inputs are correctly clasified
                    int _numberOfIterations = 0;

                    int errorCount = 0;

                    /*for (int j = 0; j < trainingSet.Length; j++)
                    {
                        var output = perceptron.Learn(trainingSet[j].Output, trainingSet[j].Inputs);
                        SC_Console.consoleMessageQueue messageQueue00 = new SC_Console.consoleMessageQueue("Fail\t {0} & {1} & {2} != {3}" + trainingSet[j].Inputs[0] + trainingSet[j].Inputs[1] + trainingSet[j].Inputs[2] + output, 0, 15 + errorCount + _numberOfIterations);
                        errorCount++;
                    }




                    _sometest:

                }             
            }


           





















            /*if (System.Windows.Input.Keyboard.IsKeyDown(Key.Enter))
            {
                GC.Collect(0);
                GC.Collect(1);
                GC.Collect(2);
                GC.Collect(10);
            }*/














        /*if (_iterations > 0)
        {
            _pressOnce = true;
        }*/



        /*if (System.Windows.Input.Keyboard.IsKeyDown(Key.R))
        {
            if (_pressOnce)
            {
                //SC_Console.WriteAt("r", 1, 1, false, 0);
                SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue("#", 0, 1);

                mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
                mciSendString("record recsound", "", 0, 0);
                numberOfStartedRecordings++;
                _pressOnce = false;
            }
        }


        else if (System.Windows.Input.Keyboard.IsKeyDown(Key.Y))
        {
            if (mainCaptureSwitch)
            {
                string strWorkingDirectory = Directory.GetCurrentDirectory();

                string soundDirectory = "\\SC_AI_AudioSaves";

                fullSubFolderPath = strWorkingDirectory + soundDirectory;

                //Directory.GetFiles(fullSubFolderPath);

                if (!Directory.Exists(fullSubFolderPath))
                {
                    Directory.CreateDirectory(fullSubFolderPath);
                }


                //SC_Console.WriteAt("y", 1, 1, false, 0);
                SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue("y", 0, 2);
                folderName = fullSubFolderPath + "\\" + "SC_AI_AudioSaves" + numberOfStartedRecordings + ".wav";

                mciSendString("save recsound" + " " + folderName, "", 0, 0);
                mciSendString("close recsound ", "", 0, 0);
                //Computer a = new Computer();
                c.Audio.Stop();
                lastFrame = numberOfStartedRecordings;

                p = new Process();
                p.StartInfo = new ProcessStartInfo()
                {
                    FileName = fullSubFolderPath //"c:\\"
                };

                p.Start();
                p.Refresh();
                _pressOnce = true;
                mainCaptureSwitch = false;
            }
        }

        else if (System.Windows.Input.Keyboard.IsKeyDown(Key.P))
        {
            //SC_Console.WriteAt("p", 1, 1, false, 0);
            SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue("p", 0, 3);

            if (Directory.Exists(fullSubFolderPath))
            {
                //string[] dirs = Directory.GetFiles(@"c:\", "c*");
                //string[] dirs = Directory.GetFiles(fullSubFolderPath, "*SC_AI_AudioSaves" + frameRate + "*");
                //string[] dirs = Directory.GetFiles(fullSubFolderPath, "*SC_AI_AudioSaves" + frameRate+".wav");
                string[] dirs = Directory.GetFiles(fullSubFolderPath, "*" + lastFrame + "*" + ".wav");
                SC_Console.WriteAt(dirs.Length.ToString(), 0, 4, false, 0);
                //Computer aa = new Computer();
                c.Audio.Play(dirs[0]);

            }
            mainCaptureSwitch = true;
        }
    }*/
        //********************************
        //********************************
        //********************************
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /*private void ContentFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            // Prevent navigation (for example clicking back button) because our ListBox is not updated when this navigation occurs
            // We prevent navigation with clearing the navigation history each time navigation item changes
            //ContentFrame0.NavigationService.RemoveBackEntry();
            //ContentFrame1.NavigationService.RemoveBackEntry();
        }*/

    }
}









//D3D.ini
//_graphics_sec = new sc_graphics_sec();
/*if (!D3D.Initialize(config, handler, SC_GLOBALS_ACCESSORS.SC_CONSOLE_WRITER))
{

}*/


//_graphics_sec = new sc_graphics_sec();
//consoleGraphics = new SC_Console_GRAPHICS(_graphics_sec);
//_sc_jitter_task = _graphics_sec._sc_create_world_objects(_sc_jitter_task);
//_sc_jitter_task = consoleGraphics.Initialize(_sc_jitter_task,config, handler, SC_GLOBALS_ACCESSORS.SC_CONSOLE_WRITER, _graphics_sec);


//consoleGraphics = new SC_Console_GRAPHICS(_graphics_sec);

/*_sc_jitter_physics = consoleGraphics.create_jitter_instances(_sc_jitter_physics, _sc_jitter_data);

for (int xx = 0; xx < _physics_engine_instance_x; xx++)
{
    for (int yy = 0; yy < _physics_engine_instance_y; yy++)
    {
        for (int zz = 0; zz < _physics_engine_instance_z; zz++)
        {
            var indexer00 = xx + _physics_engine_instance_x * (yy + _physics_engine_instance_y * zz);

            _sc_jitter_physics[indexer00]._sc_create_jitter_world(_sc_jitter_data[indexer00]);


            for (int x = 0; x < world_width; x++)
            {
                for (int y = 0; y < world_height; y++)
                {
                    for (int z = 0; z < world_depth; z++)
                    {

                        var indexer1 = x + world_width * (y + world_height * z);

                        var world = _sc_jitter_physics[indexer00].return_world(indexer1);

                        if (world == null)
                        {
                            Console.WriteLine("null world");
                        }
                        else
                        {
                            Console.WriteLine("!null world");
                            _sc_jitter_task[indexer00][indexer1]._world_data = new object[2];
                            _sc_jitter_task[indexer00][indexer1]._work_index = -1;
                            _sc_jitter_task[indexer00][indexer1]._world_data[0] = world;
                        }
                    }
                }
            }
        }
    }
}


_sc_jitter_task = _graphics_sec._sc_create_world_objects(_sc_jitter_task);
_sc_jitter_task = consoleGraphics.Initialize(_sc_jitter_task, config, handler, SC_GLOBALS_ACCESSORS.SC_CONSOLE_WRITER, _graphics_sec);*/



















/*
_sc_jitter_physics = consoleGraphics.create_jitter_instances(_sc_jitter_physics, _sc_jitter_data);

for (int xx = 0; xx < _physics_engine_instance_x; xx++)
{
    for (int yy = 0; yy < _physics_engine_instance_y; yy++)
    {
        for (int zz = 0; zz < _physics_engine_instance_z; zz++)
        {
            var indexer00 = xx + _physics_engine_instance_x * (yy + _physics_engine_instance_y * zz);

            _sc_jitter_physics[indexer00]._sc_create_jitter_world(_sc_jitter_data);


            for (int x = 0; x < world_width; x++)
            {
                for (int y = 0; y < world_height; y++)
                {
                    for (int z = 0; z < world_depth; z++)
                    {

                        var indexer1 = x + world_width * (y + world_height * z);

                        var world = _sc_jitter_physics[indexer00].return_world(indexer1);

                        if (world == null)
                        {
                            Console.WriteLine("null");
                        }
                        else
                        {
                            //Console.WriteLine("!null");

                            _sc_jitter_task[indexer00][indexer1]._world_data = new object[2];
                            _sc_jitter_task[indexer00][indexer1]._work_index = -1;
                            _sc_jitter_task[indexer00][indexer1]._world_data[0] = world;
                            //Console.WriteLine("index: " + indexer1);
                        }
                    }
                }
            }
        }
    }
}

_sc_jitter_task = _graphics_sec._sc_create_world_objects(_sc_jitter_task);
_sc_jitter_task = consoleGraphics.Initialize(_sc_jitter_task, config, handler, SC_GLOBALS_ACCESSORS.SC_CONSOLE_WRITER, _graphics_sec);
*/



/*
var thread = new Thread(() =>
 {
 _thread_looper:

     try
     {
         for (int xx = 0; xx < _physics_engine_instance_x; xx++)
         {
             for (int yy = 0; yy < _physics_engine_instance_y; yy++)
             {
                 for (int zz = 0; zz < _physics_engine_instance_z; zz++)
                 {
                     var indexer00 = xx + _physics_engine_instance_x * (yy + _physics_engine_instance_y * zz);

                     _sc_jitter_physics[indexer00]._sc_jitter_world_input();

                     for (int x = 0; x < world_width; x++)
                     {
                         for (int y = 0; y < world_height; y++)
                         {
                             for (int z = 0; z < world_depth; z++)
                             {
                                 var indexer1 = x + world_width * (y + world_height * z);
                                 _sc_jitter_data[indexer00].worlds[indexer1].Step(_array_stop_watch_tick[0], true);
                                 //var world = _sc_jitter_physics[indexer00].return_world(indexer1);
                                 _sc_jitter_task[indexer00][indexer1]._world_data[0] = _sc_jitter_data[indexer00].worlds[indexer1];
                             }
                         }
                     }
                 }
             }
         }*/

/*for (int xx = 0; xx < _physics_engine_instance_x; xx++)
{
    for (int yy = 0; yy < _physics_engine_instance_y; yy++)
    {
        for (int zz = 0; zz < _physics_engine_instance_z; zz++)
        {
            var indexer00 = xx + _physics_engine_instance_x * (yy + _physics_engine_instance_y * zz);

            //_sc_jitter_physics[indexer00]._sc_create_jitter_world(_sc_jitter_data);

            for (int x = 0; x < world_width; x++)
            {
                for (int y = 0; y < world_height; y++)
                {
                    for (int z = 0; z < world_depth; z++)
                    {

                        var indexer1 = x + world_width * (y + world_height * z);

                        var world = _sc_jitter_physics[indexer00].return_world(indexer1);

                        if (world == null)
                        {
                            Console.WriteLine("null");
                        }
                        else
                        {
                            //Console.WriteLine("!null");
                            _sc_jitter_task[indexer00][indexer1]._world_data[0] = world;
                        }
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
Thread.Sleep(0);
goto _thread_looper;
},0);

thread.IsBackground = true;
thread.SetApartmentState(ApartmentState.STA);
thread.Start();*/



/*
BackgroundWorker threaders = new BackgroundWorker();
threaders.DoWork += (object sender, DoWorkEventArgs argers) =>
{
    //object[] parametors = args.Argument as object[];
    //var workor = (_task_worker)parametors[0];
    //var parameters[] = _array_stop_watch_tick[_index];
    //int _indexer = workor._worker_task_id;
    int _start_once = 0;

_thread_looper:
    try
    {
        try
        {

            for (int xx = 0; xx < _physics_engine_instance_x; xx++)
            {
                for (int yy = 0; yy < _physics_engine_instance_y; yy++)
                {
                    for (int zz = 0; zz < _physics_engine_instance_z; zz++)
                    {
                        var indexer00 = xx + _physics_engine_instance_x * (yy + _physics_engine_instance_y * zz);

                        _sc_jitter_physics[indexer00]._sc_jitter_world_input();

                        for (int x = 0; x < world_width; x++)
                        {
                            for (int y = 0; y < world_height; y++)
                            {
                                for (int z = 0; z < world_depth; z++)
                                {
                                    var indexer1 = x + world_width * (y + world_height * z);
                                    _sc_jitter_data[indexer00].worlds[indexer1].Step(_array_stop_watch_tick[0],true);
                                    //var world = _sc_jitter_physics[indexer00].return_world(indexer1);
                                    _sc_jitter_task[indexer00][indexer1]._world_data[0] = _sc_jitter_data[indexer00].worlds[indexer1];
                                }
                            }
                        }
                    }
                }
            }



        }
        catch (Exception ex)
        {
            MessageBox((IntPtr)0, "" + ex.ToString(), "Oculus Error", 0);
        }
    }
    catch (Exception ex)
    {

    }
    Thread.Sleep(0);
    goto _thread_looper;
};

threaders.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs argers)
{
    MessageBox((IntPtr)0, "worker has completed", "Oculus Error", 0);
};

threaders.RunWorkerAsync();*/




/*
 * 
                                        try
                                        {

                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox((IntPtr)0, "CONSOLE GRAPHICS" + ex.ToString(), "_sc_core_systems error", 0);
                                        }
*/



//SC_message_object_jitter[][] _sc_jitter_task = new SC_message_object_jitter[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
//sc_jitter_data _sc_jitter_data = new sc_jitter_data();

/*try
{
    var pather = @"C:\Users\ninekorn\Desktop\Jitter Physics 0.1.7.0\Jitter Physics 0.1.7.0\sc_core_systems\sc_core_systems\_dll_sc\jitter.dll";
    var DLL = Assembly.LoadFile(pather);

    _sc_jitter_data.alloweddeactivation = true;
    _sc_jitter_data.allowedpenetration = 0.00123f;
    _sc_jitter_data.depth = 1;
    _sc_jitter_data.width = 1;
    _sc_jitter_data.height = 1;
    _sc_jitter_data.gravity = new JVector(0, -9.81f, 0);
    _sc_jitter_data.smalliterations = 10;
    _sc_jitter_data.iterations = 10;

    Type[] typeMainMethod = new Type[]
    {
    typeof(int),
    typeof(int),
    typeof(float),
    typeof(bool),
    typeof(JVector),
    typeof(int),
    typeof(int),
    typeof(int),
    };

    Type t = DLL.GetType(pather + "_sc_jitter_physics");
    var methodInfo = t.GetMethod("_sc_create_jitter_world", typeMainMethod);
}
catch (Exception ex)
{

}*/






//var o = Activator.CreateInstance(Type.GetType(pather, true));
//Type t = DLL.GetType(pather + "_sc_jitter_physics");
//var o = Activator.CreateInstance(t);

/*var methodInfo = o.GetMethod("_sc_create_jitter_world", new Type[]
  {
        typeof(int),
        typeof(int),
        typeof(float),
        typeof(bool),
        typeof(JVector),
        typeof(int),
        typeof(int),
        typeof(int),
  });
object[] somedata = new object[1];
somedata[0] = _sc_jitter_data;
var result = methodInfo.Invoke(o, somedata);*/



/*foreach (Type type in DLL.GetExportedTypes())
{
    //Console.WriteLine(type);
    var o = Activator.CreateInstance(type);
    object[] somedata = new object[1];
    somedata[0] = _sc_jitter_data;
    var result = methodInfo.Invoke(o, somedata);
}*/







/*for (int xx = 0; xx < _physics_engine_instance_x; xx++)
{
    for (int yy = 0; yy < _physics_engine_instance_y; yy++)
    {
        for (int zz = 0; zz < _physics_engine_instance_z; zz++)
        {
            var indexer00 = xx + _physics_engine_instance_x * (yy + _physics_engine_instance_y * zz);

            _sc_jitter_task[indexer00] = new SC_message_object_jitter[world_width * world_height * world_depth];

            for (int x = 0; x < world_width; x++)
            {
                for (int y = 0; y < world_height; y++)
                {
                    for (int z = 0; z < world_depth; z++)
                    {
                        var indexer01 = x + world_width * (y + world_height * z);

                    }
                }
            }
        }
    }
}*/












/*
Type t = DLL.GetType(pather + "_sc_jitter_physics");  //Classname is class to be instantiated
var methodInfo = t.GetMethod("_jitter_hook", new Type[]
{
    typeof(int),
    typeof(int),
    typeof(float),
    typeof(bool),
    typeof(JVector),
    typeof(int),
    typeof(int),
    typeof(int),
});//myMethod is method to be instantiated*/


/*object obj = Activator.CreateInstance(Type.GetType(pather, true));

foreach (Type type in DLL.GetExportedTypes())
{
    //dynamic c = Activator.CreateInstance(type);
    //c.Output(@"Hello");
}*/
//object obj = Activator.CreateInstance(Type.GetType("DALL.LoadClass, DALL", true));


/*_sc_jitter_physics[] _sc_jitter_physics = new _sc_jitter_physics[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
SC_message_object_jitter[][] _sc_jitter_task = new SC_message_object_jitter[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];
sc_jitter_data[] _sc_jitter_data = new sc_jitter_data[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];


CollisionSystemPersistentSAP[] _collisionSAP = new CollisionSystemPersistentSAP[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
World[][] worlds_ = new World[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];

_array_stop_watch_tick = new float[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];

sc_deltatime(0, 0);

for (int xx = 0; xx < _physics_engine_instance_x; xx++)
{
    for (int yy = 0; yy < _physics_engine_instance_y; yy++)
    {
        for (int zz = 0; zz < _physics_engine_instance_z; zz++)
        {

            var indexer00 = xx + _physics_engine_instance_x * (yy + _physics_engine_instance_y * zz);
            //_jitter_physics[indexer00] = DoSpecialThing();
            _sc_jitter_data[indexer00] = new sc_jitter_data();
            _sc_jitter_task[indexer00] = new SC_message_object_jitter[world_width * world_height * world_depth];
            worlds_[indexer00] = new World[world_width * world_height * world_depth];
            //_sc_jitter_physics[indexer00] = new _sc_jitter_physics();

            _collisionSAP[indexer00] = new CollisionSystemPersistentSAP();


            for (int x = 0; x < world_width; x++)
            {
                for (int y = 0; y < world_height; y++)
                {
                    for (int z = 0; z < world_depth; z++)
                    {
                        var indexer01 = x + world_width * (y + world_height * z);
                        _sc_jitter_task[indexer00][indexer01] = new SC_message_object_jitter();
                        worlds_[indexer00][indexer01] = new World(_collisionSAP[indexer00]);
                        worlds_[indexer00][indexer01].AllowDeactivation = false;
                        worlds_[indexer00][indexer01].Gravity = _world_gravity;
                        worlds_[indexer00][indexer01].SetIterations(_world_iterations, _world_small_iterations);
                        worlds_[indexer00][indexer01].ContactSettings.AllowedPenetration = _world_allowed_penetration;
                    }
                }
            }

            _sc_jitter_data[indexer00]._collisionSAP = _collisionSAP;
            _sc_jitter_data[indexer00].worlds = worlds_[indexer00];

        }
    }
}*/

/*sc_jitter_data _sc_jitter_data = new sc_jitter_data();
/*_sc_jitter_data.alloweddeactivation = true;
_sc_jitter_data.allowedpenetration = 0.00123f;
_sc_jitter_data.width = world_width;
_sc_jitter_data.height = world_height;
_sc_jitter_data.depth = world_depth;
_sc_jitter_data.gravity = new JVector(0, 0, 0);
_sc_jitter_data.smalliterations = 3;
_sc_jitter_data.iterations = 3;*/








/*
_sc_jitter_physics[] _sc_jitter_physics = new _sc_jitter_physics[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z];
SC_message_object_jitter[][] _sc_jitter_task = new SC_message_object_jitter[_physics_engine_instance_x * _physics_engine_instance_y * _physics_engine_instance_z][];

sc_jitter_data _sc_jitter_data = new sc_jitter_data();
_sc_jitter_data.alloweddeactivation = false;
_sc_jitter_data.allowedpenetration = 0.00123f;
_sc_jitter_data.width = world_width;
_sc_jitter_data.height = world_height;
_sc_jitter_data.depth = world_depth;
_sc_jitter_data.gravity = new JVector(0, 0, 0);
_sc_jitter_data.smalliterations = 10;
_sc_jitter_data.iterations = 10;


for (int xx = 0; xx < _physics_engine_instance_x; xx++)
{
    for (int yy = 0; yy < _physics_engine_instance_y; yy++)
    {
        for (int zz = 0; zz < _physics_engine_instance_z; zz++)
        {
            var indexer00 = xx + _physics_engine_instance_x * (yy + _physics_engine_instance_y * zz);
            //_jitter_physics[indexer00] = DoSpecialThing();
            _sc_jitter_task[indexer00] = new SC_message_object_jitter[world_width * world_height * world_depth];

            for (int x = 0; x < world_width; x++)
            {
                for (int y = 0; y < world_height; y++)
                {
                    for (int z = 0; z < world_depth; z++)
                    {
                        var indexer01 = x + world_width * (y + world_height * z);
                        _sc_jitter_task[indexer00][indexer01] = new SC_message_object_jitter();


                    }
                }
            }
        }
    }
}*/















/*
for (int xx = 0; xx < _physics_engine_instance_x; xx++)
{
    for (int yy = 0; yy < _physics_engine_instance_y; yy++)
    {
        for (int zz = 0; zz < _physics_engine_instance_z; zz++)
        {
            var indexer00 = xx + _physics_engine_instance_x * (yy + _physics_engine_instance_y * zz);

            _sc_jitter_task[indexer00] = new SC_message_object_jitter[world_width * world_height * world_depth];

            for (int x = 0; x < world_width; x++)
            {
                for (int y = 0; y < world_height; y++)
                {
                    for (int z = 0; z < world_depth; z++)
                    {
                        var indexer01 = x + world_width * (y + world_height * z);

                        //JITTER PHYSICS
                        var collision = new CollisionSystemPersistentSAP();
                        var World = new World(collision);
                        World.AllowDeactivation = true;

                        World.Gravity = _world_gravity;
                        World.SetIterations(_world_iterations, _world_small_iterations);
                        World.ContactSettings.AllowedPenetration = _world_allowed_penetration;

                        //_sc_jitter_physics[indexer00][indexer01] = World;

                        _sc_jitter_task[indexer00][indexer01]._world_data = new object[2];
                        _sc_jitter_task[indexer00][indexer01]._work_index = -1;
                        _sc_jitter_task[indexer00][indexer01]._world_data[0] = World;
                    }
                }
            }                           
        }
    }
}*/
