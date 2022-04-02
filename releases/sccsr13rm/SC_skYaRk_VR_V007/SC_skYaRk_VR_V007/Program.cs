using System;

//using SharpDX;
using SharpDX.DirectInput;
using System.Threading;
using System.Threading.Tasks;

using _messager = SC_skYaRk_VR_V007.SC_Console_WRITER._messager;


using System.Collections.Generic;
using System.Runtime;
using System.Runtime.CompilerServices;

using System.Runtime.InteropServices;

namespace SC_skYaRk_VR_V007
{
    public static class Program
    {


        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        const int _max_array_size = 6;

        static SC_object_messenger_dispatcher.SC_message_object[] _someObject = new SC_object_messenger_dispatcher.SC_message_object[_max_array_size];

        static SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject0 = new SC_object_messenger_dispatcher.SC_message_object[_max_array_size];

        static SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject1 = new SC_object_messenger_dispatcher.SC_message_object[_max_array_size];


        static int init_directX_main_swtch = 2;
        static int init_vr_main_swtch = 2;

        public static SharpDX.DirectInput.Keyboard _Keyboard;
        static DirectInput directInput = new DirectInput();
        public static KeyboardState _KeyboardState;


        static void Main(string[] args)
        {

            string _program_name0;
            string _program_name;

            Task _console_worker_tasker;
            int _welcomePackage00;
            int _current_menu00;

            IntPtr handler;
            int _processorCount = 1;
            int _someInit = 1;
            //Task _mainTasker00;
            int _voiceRecognition = 1;



            int _Max_Size_00 = 6;
            _messager[] _some_received_messages;// = new _messager[_Max_Size_00];
            SC_object_messenger_dispatcher.SC_message_object _data00_IN = new SC_object_messenger_dispatcher.SC_message_object();


            _data00_IN = new SC_object_messenger_dispatcher.SC_message_object();
            _data00_IN._received_switch_in = -1;
            _data00_IN._received_switch_out = -1;
            _data00_IN._sending_switch_in = -1;
            _data00_IN._sending_switch_out = -1;
            _data00_IN._chain_Of_Tasks0 = null;
            _data00_IN._timeOut0 = -1;
            _data00_IN._ParentTaskThreadID0 = -1;
            _data00_IN._main_cpu_count = _processorCount;
            _data00_IN._passTest = "";
            _data00_IN._welcomePackage = -1;
            _data00_IN._reset_event = new ManualResetEvent(false);
            _data00_IN._work_done = -1;
            _data00_IN._current_menu = -1;
            _data00_IN._last_current_menu = -1;
            _data00_IN._main_menu = -1;
            _data00_IN._menuOption = "";
            _data00_IN._voRecSwtc = -1;
            _data00_IN._voRecMsg = "";





            SC_object_messenger_dispatcher.SC_message_object _data00_OUT;

            _data00_OUT = new SC_object_messenger_dispatcher.SC_message_object();
            _data00_OUT._received_switch_in = -1;
            _data00_OUT._received_switch_out = -1;
            _data00_OUT._sending_switch_in = -1;
            _data00_OUT._sending_switch_out = -1;
            _data00_OUT._chain_Of_Tasks0 = null;
            _data00_OUT._timeOut0 = -1;
            _data00_OUT._ParentTaskThreadID0 = -1;
            _data00_OUT._main_cpu_count = _processorCount;
            _data00_OUT._passTest = "";
            _data00_OUT._welcomePackage = -1;
            _data00_OUT._reset_event = new ManualResetEvent(false);
            _data00_OUT._work_done = -1;
            _data00_OUT._current_menu = -1;
            _data00_OUT._last_current_menu = -1;
            _data00_OUT._main_menu = -1;
            _data00_OUT._menuOption = "";
            _data00_OUT._voRecSwtc = -1;
            _data00_OUT._voRecMsg = "";



            // int _init_directX_standard = 1;
            // int _init_directX_vr = 1;


            //int init_directX_main_swtch = -1;
            //int init_vr_main_swtch = -1;


            ManualResetEvent _manual_reset_event_reader = new ManualResetEvent(false);
            ManualResetEvent _manual_reset_event_writer = new ManualResetEvent(false);
            ManualResetEvent _manual_reset_event_worker = new ManualResetEvent(false);

            int _consoleKeyCounter = 0;
            int startThread = 0;

            string _username = "";
            string _lastUsername = "";
            string _lastMenuOption = "";

            Task _console_worker_task;

            int _lastMenu = -2;
            int has_init_directx = 0;
            int _consoleKeySwitch = 0;

            int _some_other_swtch = 0;










            GCLatencyMode oldMode = GCSettings.LatencyMode;

            // Make sure we can always go to the catch block, 
            // so we can set the latency mode back to `oldMode`
            RuntimeHelpers.PrepareConstrainedRegions();

            try
            {
                GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency; //Interactive

                // Generation 2 garbage collection is now
                // deferred, except in extremely low-memory situations
            }
            finally
            {
                // ALWAYS set the latency mode back
                GCSettings.LatencyMode = oldMode;
            }



            GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;


            /*Console.WriteLine("Total available memory before collection: {0:N0}", System.GC.GetTotalMemory(false));

            System.GC.Collect();

            Console.WriteLine("Total available memory collection: {0:N0}", System.GC.GetTotalMemory(true));
            */



            ////////////////////
            /////PROCESSOR COUNT
            ////////////////////
            for (int j = 0; j < 1; j++)
            {
                try
                {
                    _processorCount = SC_SystemInfoSeeker.getSystemProcessorCount();
                }
                catch //(Exception ex)
                {
                    break;
                }
            }
            ////////////////////
            /////PROCESSOR COUNT
            ////////////////////





            //BRAIN START
            //object[] tester000 = new object[_Max_Size_00];

            _InitializeKeyboard();
            _KeyboardState = new KeyboardState();

            //object[] tester001 = new object[_Max_Size_00];


            _some_received_messages = new _messager[_Max_Size_00];




            //int _consoleKeySwitch = 0;
            //int _init_directX_standard = 2;



            //PREPARING MAIN MESSAGING SYSTEM THAT CONSISTS OF THE STRUCT <<SC_object_messenger_dispatcher.SC_message_object>>
            _someReceivedObject0 = new SC_object_messenger_dispatcher.SC_message_object[_Max_Size_00];
            for (int i = 0; i < _someReceivedObject0.Length; i++)
            {
                _someReceivedObject0[i] = new SC_object_messenger_dispatcher.SC_message_object();
                _someReceivedObject0[i]._received_switch_in = -1;
                _someReceivedObject0[i]._received_switch_out = -1;
                _someReceivedObject0[i]._sending_switch_in = -1;
                _someReceivedObject0[i]._sending_switch_out = -1;
                _someReceivedObject0[i]._chain_Of_Tasks0 = null;
                _someReceivedObject0[i]._timeOut0 = -1;
                _someReceivedObject0[i]._ParentTaskThreadID0 = -1;
                _someReceivedObject0[i]._main_cpu_count = _processorCount;
                _someReceivedObject0[i]._passTest = "";
                _someReceivedObject0[i]._welcomePackage = -1;
                _someReceivedObject0[i]._reset_event = new ManualResetEvent(false);
                _someReceivedObject0[i]._work_done = -1;
                _someReceivedObject0[i]._current_menu = -1;
                _someReceivedObject0[i]._last_current_menu = -1;
                _someReceivedObject0[i]._main_menu = -1;
                _someReceivedObject0[i]._menuOption = "";
                _someReceivedObject0[i]._voRecSwtc = -1;
                _someReceivedObject0[i]._voRecMsg = "";
                //_someReceivedObject0[i]._someData = new object[_Max_Size_00];

                /*for (int j = 0; j < _someReceivedObject0[i]._someData.Length; j++)
                {
                    _someReceivedObject0[i]._someData[j] = new object();
                }


                if (i == _Max_Size_00 - 1)
                {
                    _someReceivedObject0[i]._someData[0] = _KeyboardState;
                    _someReceivedObject0[i]._voRecSwtc = 1;
                    //tester001[i]._someData[0] = _KeyboardState;
                }*/









                //tester000[i] = _someReceivedObject0[i];
                //tester001[i] = _someReceivedObject0[i];

            }









            //SC_skYaRk_VR_V007.SC_Console.SC_Console_VoRec _voRec = new SC_Console.SC_Console_VoRec();


            Task _console_voice_recognition;


            Task _console_writer_task;
            int _start_thread_console_writer = 1;
            int _start_console_ui_menu = 0;

            int _worker_000_has_init = 0;


            Task _console_reader_task;
            int _console_reader_canWork = 1;
            object _console_reader_object;
            SC_Console_READER._console_reader_data _console_reader_string;


            SC_Systems _SC_SYSTEMS = new SC_Systems(_someReceivedObject0);

            if (_SC_SYSTEMS == null)
            {
                ////System.Windows.MessageBox.Show("_SC_SYSTEMS NULL", "Console");
            }
            else
            {
                ////System.Windows.MessageBox.Show("_SC_SYSTEMS !NULL", "Console");
            }


            SC_skYaRk_VR_V007.SC_Console_WRITER._messager objecter;


            _some_received_messages[4]._messager_list = new _messager[_Max_Size_00];
            _some_received_messages[4]._message = "";
            _some_received_messages[4]._originalMsg = "";
            _some_received_messages[4]._messageCut = "";
            _some_received_messages[4]._specialMessage = -1;
            _some_received_messages[4]._specialMessageLineX = 0;
            _some_received_messages[4]._specialMessageLineY = 0;
            _some_received_messages[4]._orilineX = 0;
            _some_received_messages[4]._orilineY = 0;
            _some_received_messages[4]._lineX = 0;
            _some_received_messages[4]._lineY = 0;
            _some_received_messages[4]._count = 0;
            _some_received_messages[4]._swtch0 = 1;
            _some_received_messages[4]._swtch1 = 1;
            _some_received_messages[4]._delay = 50;
            _some_received_messages[4]._looping = 1;


            _console_reader_string._has_message_to_display = 0;
            _console_reader_string._console_reader_message = "";
            _console_reader_string._has_init = 0;
            _console_reader_object = _console_reader_string;





            SC_Console_GRAPHICS consoleGraphics = null;


            Thread _mainTasker00;

        _main_thread_Loop_x00:
            if (_someInit == 1)
            {



                _mainTasker00 = new Thread((tester0000) =>
                {

                // _mainTasker00 = Task<object[]>.Factory.StartNew((tester0000) =>
                //{
                _thread_main_loop:

                    for (int bk = 0; bk < 1; bk++) //_totalThreads
                    {

                        //////ACTIVATE KEYBOARD=>
                        ReadKeyboard();
                        //////ACTIVATE KEYBOARD<=



                        /*//////VOICE RECOGNITION=>
                        if (_voiceRecognition == 1)
                        {
                            _console_voice_recognition = Task<object[]>.Factory.StartNew((tester0001) =>
                            {
                            _thread_voRec_loop:
                                if (_someReceivedObject0[_Max_Size_00 - 1]._voRecSwtc == 1)
                                {
                           
                                    _someReceivedObject0[_Max_Size_00 - 1] = SC_Console.SC_Console_VoRec._work_rec(_someReceivedObject0[_Max_Size_00 - 1], _some_received_messages[4], out objecter);
                                    _some_received_messages[4] = objecter;

                                    ///_voRec._work_rec(_someReceivedObject0[_Max_Size_00 - 1]);
                                    //_someReceivedObject0[_Max_Size_00 - 1] = _voRec._work_rec(_someReceivedObject0[_Max_Size_00 - 1]);
                                }
                                Thread.Sleep(1);
                                goto _thread_voRec_loop;
                            }, tester001);
                        }
                        //////VOICE RECOGNITION=>*/




                        if (_start_thread_console_writer == 1) // 1
                        {
                            _console_writer_task = Task<object[]>.Factory.StartNew((tester0001) =>
                            {                                                                                           //Gates of Sky`s Cemetery // Gates of heaven in a grim way.
                                _program_name = "SC Core Systems";//AND => A new Dawn... blablabla //Alice in wonderland // hack face - hak face  TDHF // sKYaRk //Tore Des Himmel Friedhof //SC_pandoras_box //SC Core Systems

                                _some_received_messages[0]._message = _program_name;
                                _some_received_messages[0]._originalMsg = _program_name;
                                _some_received_messages[0]._messageCut = _program_name;
                                _some_received_messages[0]._specialMessage = 2;
                                _some_received_messages[0]._specialMessageLineX = 0;
                                _some_received_messages[0]._specialMessageLineY = 0;
                                _some_received_messages[0]._orilineX = (Console.WindowWidth / 2) - (_program_name.Length / 2);
                                _some_received_messages[0]._orilineY = (Console.WindowHeight / 2);
                                _some_received_messages[0]._lineX = (Console.WindowWidth / 2) - (_program_name.Length / 2);
                                _some_received_messages[0]._lineY = (Console.WindowHeight / 2);
                                _some_received_messages[0]._count = 0;
                                _some_received_messages[0]._swtch0 = 1;
                                _some_received_messages[0]._swtch1 = 1;
                                _some_received_messages[0]._delay = 10;
                                _some_received_messages[0]._looping = 1;
                                _worker_000_has_init = 1;

                            //////CONSOLE WRITER=>
                            _thread_loop_console:

                                _some_received_messages = _SC_SYSTEMS._SC_CONSOLE_WRITER._console_writer(_some_received_messages);

                                _start_console_ui_menu = 1;
                                Thread.Sleep(1);

                                goto _thread_loop_console;
                                //////CONSOLE WRITER <=


                                //if (_shutoff_console_writer == 1)
                                //{
                                //    _manual_reset_event_writer.WaitOne();
                                //    _shutoff_console_writer = 2;
                                //}


                            }, _someReceivedObject0);
                            _start_thread_console_writer = 2;
                        }




                        //CONFIRM CONSOLE WRITER IS WORKING=>
                        if (_worker_000_has_init == 1)
                        {
                            if (_SC_SYSTEMS != null)
                            {
                                if (_SC_SYSTEMS._SC_CONSOLE_WRITER != null)
                                {
                                    _some_received_messages[1]._message = "core C-WR" + " ENABLED";
                                    _some_received_messages[1]._originalMsg = "core C-WR" + " ENABLED";
                                    _some_received_messages[1]._messageCut = "core C-WR" + " ENABLED";
                                    _some_received_messages[1]._specialMessage = 2;
                                    _some_received_messages[1]._specialMessageLineX = 0;
                                    _some_received_messages[1]._specialMessageLineY = 0;
                                    _some_received_messages[1]._orilineX = 1;
                                    _some_received_messages[1]._orilineY = Console.WindowHeight - 2;
                                    _some_received_messages[1]._lineX = 1;
                                    _some_received_messages[1]._lineY = Console.WindowHeight - 2;
                                    _some_received_messages[1]._count = 0;
                                    _some_received_messages[1]._swtch0 = 1;
                                    _some_received_messages[1]._swtch1 = 0;
                                    _some_received_messages[1]._delay = 500;
                                    _some_received_messages[1]._looping = 0;

                                    _worker_000_has_init = 2;
                                }
                            }
                        }
                        //CONFIRM CONSOLE WRITER IS WORKING<=


                        //CONFIRM CONSOLE READER IS WORKING=>
                        if (_worker_000_has_init == 2)
                        {
                            if (_SC_SYSTEMS != null)
                            {
                                if (_SC_SYSTEMS._SC_CONSOLE_READER != null)
                                {
                                    _some_received_messages[2]._message = "core C-RE" + " ENABLED";
                                    _some_received_messages[2]._originalMsg = "core C-RE" + " ENABLED";
                                    _some_received_messages[2]._messageCut = "core C-RE" + " ENABLED";
                                    _some_received_messages[2]._specialMessage = 2;
                                    _some_received_messages[2]._specialMessageLineX = 0;
                                    _some_received_messages[2]._specialMessageLineY = 1;
                                    _some_received_messages[2]._orilineX = _some_received_messages[2]._message.Length + 3;
                                    _some_received_messages[2]._orilineY = Console.WindowHeight - 2;
                                    _some_received_messages[2]._lineX = _some_received_messages[2]._message.Length + 3;
                                    _some_received_messages[2]._lineY = Console.WindowHeight - 2;
                                    _some_received_messages[2]._count = 0;
                                    _some_received_messages[2]._swtch0 = 1;
                                    _some_received_messages[2]._swtch1 = 0;
                                    _some_received_messages[2]._delay = 500;
                                    _some_received_messages[2]._looping = 0;

                                    _worker_000_has_init = 3;
                                }
                            }
                        }
                        //CONFIRM CONSOLE READER IS WORKING<=

                        if (_worker_000_has_init == 3)
                        {
                            if (_SC_SYSTEMS != null)
                            {
                                if (_SC_SYSTEMS._SC_CONSOLE_READER != null)
                                {

                                    _program_name0 = "Press Enter";


                                    _some_received_messages[3]._message = _program_name0;
                                    _some_received_messages[3]._originalMsg = _program_name0;
                                    _some_received_messages[3]._messageCut = _program_name0;
                                    _some_received_messages[3]._specialMessage = 2;
                                    _some_received_messages[3]._specialMessageLineX = 0;
                                    _some_received_messages[3]._specialMessageLineY = 0;
                                    _some_received_messages[3]._orilineX = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                    _some_received_messages[3]._orilineY = (Console.WindowHeight / 2) + 1;
                                    _some_received_messages[3]._lineX = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                    _some_received_messages[3]._lineY = (Console.WindowHeight / 2) + 1;
                                    _some_received_messages[3]._count = 0;
                                    _some_received_messages[3]._swtch0 = 1;
                                    _some_received_messages[3]._swtch1 = 1;
                                    _some_received_messages[3]._delay = 500;
                                    _some_received_messages[3]._looping = 1;
                                    //Console.SetCursorPosition(_initX0 + _program_name0.Length, _initY0);
                                    _worker_000_has_init = 4;
                                }
                            }
                        }



                        if (_worker_000_has_init == 4)
                        {
                            _console_reader_task = Task<SC_object_messenger_dispatcher.SC_message_object[]>.Factory.StartNew((tester0001) =>
                            {
                            _thread_looper:
                                /*if (_shutoff_console_reader == 1)
                                {
                                    _manual_reset_event_reader.WaitOne();
                                    _shutoff_console_reader = 2;
                                }*/

                                if (_console_reader_canWork == 1)
                                {
                                    _console_reader_string = _SC_SYSTEMS._SC_CONSOLE_READER._console_reader(_console_reader_object);
                                }

                                if (_SC_SYSTEMS._SC_CONSOLE_READER._main_has_init == 1)
                                {
                                    _console_reader_string._console_reader_message = "";
                                    _console_reader_string._has_message_to_display = 0;
                                    //startThread = 3;
                                    _program_name0 = "WELCOME";

                                    _some_received_messages[4]._message = _program_name0;
                                    _some_received_messages[4]._originalMsg = _program_name0;
                                    _some_received_messages[4]._messageCut = _program_name0;
                                    _some_received_messages[4]._specialMessage = 2;
                                    _some_received_messages[4]._specialMessageLineX = 0;
                                    _some_received_messages[4]._specialMessageLineY = 0;
                                    _some_received_messages[4]._orilineX = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                    _some_received_messages[4]._orilineY = (Console.WindowHeight / 2) + 1;
                                    _some_received_messages[4]._lineX = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                    _some_received_messages[4]._lineY = (Console.WindowHeight / 2) - 1;
                                    _some_received_messages[4]._count = 0;
                                    _some_received_messages[4]._swtch0 = 1;
                                    _some_received_messages[4]._swtch1 = 0;
                                    _some_received_messages[4]._delay = 200;
                                    _some_received_messages[4]._looping = 0;

                                    _some_received_messages[0]._swtch0 = 0;
                                    _some_received_messages[0]._swtch1 = 0;
                                    _some_received_messages[3]._swtch0 = 0;
                                    _some_received_messages[3]._swtch1 = 0;

                                    _program_name0 = "Please Enter your Username: ";

                                    _some_received_messages[6]._message = _program_name0;
                                    _some_received_messages[6]._originalMsg = _program_name0;
                                    _some_received_messages[6]._messageCut = _program_name0;
                                    _some_received_messages[6]._specialMessage = 2;
                                    _some_received_messages[6]._specialMessageLineX = 0;
                                    _some_received_messages[6]._specialMessageLineY = 0;
                                    _some_received_messages[6]._orilineX = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                    _some_received_messages[6]._orilineY = (Console.WindowHeight / 2) + 1;
                                    _some_received_messages[6]._lineX = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                    _some_received_messages[6]._lineY = (Console.WindowHeight / 2) + 2;
                                    _some_received_messages[6]._count = 0;
                                    _some_received_messages[6]._swtch0 = 1;
                                    _some_received_messages[6]._swtch1 = 1;
                                    _some_received_messages[6]._delay = 50;
                                    _some_received_messages[6]._looping = 1;

                                    Console.SetCursorPosition((Console.WindowWidth / 2) - (_program_name0.Length / 2) + _program_name0.Length, (Console.WindowHeight / 2) + 2);

                                    startThread = 3;
                                    _SC_SYSTEMS._SC_CONSOLE_READER._main_has_init = 2;
                                }

                                if (startThread == 3 && _console_reader_string._has_message_to_display == 1)
                                {
                                    if (_console_reader_string._console_reader_message.ToLower() == "nine" || _console_reader_string._console_reader_message.ToLower() == "ninekorn" || _console_reader_string._console_reader_message.ToLower() == "9")
                                    {
                                        _program_name0 = "Access Authorized";


                                        _some_received_messages[6]._message = _program_name0;
                                        _some_received_messages[6]._originalMsg = _program_name0;
                                        _some_received_messages[6]._messageCut = _program_name0;
                                        _some_received_messages[6]._specialMessage = 2;
                                        _some_received_messages[6]._specialMessageLineX = 0;
                                        _some_received_messages[6]._specialMessageLineY = 0;
                                        _some_received_messages[6]._orilineX = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                        _some_received_messages[6]._orilineY = (Console.WindowHeight / 2) + 1;
                                        _some_received_messages[6]._lineX = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                        _some_received_messages[6]._lineY = (Console.WindowHeight / 2) + 2;
                                        _some_received_messages[6]._count = 0;
                                        _some_received_messages[6]._swtch0 = 1;
                                        _some_received_messages[6]._swtch1 = 0;
                                        _some_received_messages[6]._delay = 50;
                                        _some_received_messages[6]._looping = 0;

                                        for (int L0_IN = 0; L0_IN < _someReceivedObject0.Length; L0_IN++) //_main_thread_lOOp_object.Length
                                        {
                                            _someReceivedObject0[L0_IN]._passTest = _console_reader_string._console_reader_message.ToLower();
                                            //tester001[L0_IN] = _someReceivedObject0[L0_IN];
                                        }

                                        _lastUsername = _console_reader_string._console_reader_message;
                                        _console_reader_string._console_reader_message = "";
                                        startThread = 4;
                                    }
                                    else if (_console_reader_string._console_reader_message.ToLower() != " " || _console_reader_string._console_reader_message.ToLower() != "")
                                    {
                                        _program_name0 = "Access Denied";


                                        _some_received_messages[6]._message = _program_name0;
                                        _some_received_messages[6]._originalMsg = _program_name0;
                                        _some_received_messages[6]._messageCut = _program_name0;
                                        _some_received_messages[6]._specialMessage = 2;
                                        _some_received_messages[6]._specialMessageLineX = 0;
                                        _some_received_messages[6]._specialMessageLineY = 0;
                                        _some_received_messages[6]._orilineX = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                        _some_received_messages[6]._orilineY = (Console.WindowHeight / 2) + 1;
                                        _some_received_messages[6]._lineX = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                        _some_received_messages[6]._lineY = (Console.WindowHeight / 2) + 2;
                                        _some_received_messages[6]._count = 0;
                                        _some_received_messages[6]._swtch0 = 1;
                                        _some_received_messages[6]._swtch1 = 0;
                                        _some_received_messages[6]._delay = 50;
                                        _some_received_messages[6]._looping = 0;

                                        _lastUsername = "";
                                        _console_reader_string._console_reader_message = "";

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
                                            _lastMenuOption = _console_reader_string._console_reader_message.ToLower();
                                            _console_reader_string._console_reader_message = "";

                                            _someReceivedObject0[0]._received_switch_in = 1;
                                            _someReceivedObject0[0]._received_switch_out = 1;
                                            _someReceivedObject0[0]._sending_switch_in = 1;
                                            _someReceivedObject0[0]._sending_switch_out = 1;
                                            _someReceivedObject0[0]._welcomePackage = 999;

                                            //tester001[0] = _someReceivedObject0[0];

                                            _someReceivedObject0 = _console_worker_one(_someReceivedObject0);

                                            _some_other_swtch = 1;
                                        }
                                        else if (_console_reader_string._console_reader_message.ToLower() == "standard" ||
                                                _console_reader_string._console_reader_message.ToLower() == "std")
                                        {
                                            _lastMenuOption = _console_reader_string._console_reader_message.ToLower();
                                            _console_reader_string._console_reader_message = "";

                                            _someReceivedObject0[0]._received_switch_in = 1;
                                            _someReceivedObject0[0]._received_switch_out = 1;
                                            _someReceivedObject0[0]._sending_switch_in = 1;
                                            _someReceivedObject0[0]._sending_switch_out = 1;
                                            _someReceivedObject0[0]._welcomePackage = 998;

                                            //tester001[0] = _someReceivedObject0[0];

                                            _someReceivedObject0 = _console_worker_one(_someReceivedObject0);
                                            _some_other_swtch = 1;
                                        }
                                    }
                                    else
                                    {
                                        _program_name0 = "Option Not Implemented";

                                        _some_received_messages[6]._message = _program_name0;
                                        _some_received_messages[6]._originalMsg = _program_name0;
                                        _some_received_messages[6]._messageCut = _program_name0;
                                        _some_received_messages[6]._specialMessage = 2;
                                        _some_received_messages[6]._specialMessageLineX = 0;
                                        _some_received_messages[6]._specialMessageLineY = 0;
                                        _some_received_messages[6]._orilineX = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                        _some_received_messages[6]._orilineY = (Console.WindowHeight / 2) + 1;
                                        _some_received_messages[6]._lineX = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                        _some_received_messages[6]._lineY = (Console.WindowHeight / 2) + 2;
                                        _some_received_messages[6]._count = 0;
                                        _some_received_messages[6]._swtch0 = 1;
                                        _some_received_messages[6]._swtch1 = 0;
                                        _some_received_messages[6]._delay = 50;
                                        _some_received_messages[6]._looping = 0;

                                        _lastMenuOption = "";
                                        _console_reader_string._console_reader_message = "";
                                    }
                                }

                                Thread.Sleep(1);
                                goto _thread_looper;
                            }, _someReceivedObject0);
                            _worker_000_has_init = 5;
                        }

                        if (_worker_000_has_init == 5)
                        {
                            _console_worker_task = Task<SC_object_messenger_dispatcher.SC_message_object[]>.Factory.StartNew((tester0001) =>
                            {

                            _thread_looper:


                                if (_worker_000_has_init == 2) //2
                                {
                                    _welcomePackage00 = _someReceivedObject0[0]._welcomePackage;

                                    if (_welcomePackage00 == 0)
                                    {
                                        _someReceivedObject0 = _console_worker_one(_someReceivedObject0);
                                    }
                                    else if (_welcomePackage00 == 1)
                                    {
                                        _current_menu00 = _data00_OUT._current_menu;

                                        if (_lastMenu != _current_menu00)
                                        {
                                            _program_name0 = _current_menu00 + "";


                                            _some_received_messages[5]._message = _program_name0;
                                            _some_received_messages[5]._originalMsg = _program_name0;
                                            _some_received_messages[5]._messageCut = _program_name0;
                                            _some_received_messages[5]._specialMessage = 2;
                                            _some_received_messages[5]._specialMessageLineX = 0;
                                            _some_received_messages[5]._specialMessageLineY = 0;
                                            _some_received_messages[5]._orilineX = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                            _some_received_messages[5]._orilineY = (Console.WindowHeight / 2) + 1;
                                            _some_received_messages[5]._lineX = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                            _some_received_messages[5]._lineY = (Console.WindowHeight / 2) + 9;
                                            _some_received_messages[5]._count = 0;
                                            _some_received_messages[5]._swtch0 = 1;
                                            _some_received_messages[5]._swtch1 = 0;
                                            _some_received_messages[5]._delay = 50;
                                            _some_received_messages[5]._looping = 0;
                                        }

                                        if (_current_menu00 == -1)
                                        {
                                            _data00_IN._received_switch_in = 0;
                                            _data00_IN._received_switch_out = 0;
                                            _data00_IN._sending_switch_in = 0;
                                            _data00_IN._sending_switch_out = 0;

                                            _data00_IN._current_menu = _data00_OUT._current_menu;
                                            _data00_IN._menuOption = _lastMenuOption;

                                            _data00_OUT = SC_Console_Menu.SC_Console_Menu._console_worker_menu(_data00_IN);

                                            _lastMenu = _data00_OUT._current_menu;
                                            _lastMenuOption = "";
                                        }
                                        else if (_current_menu00 == 0)
                                        {
                                            _program_name0 = _current_menu00 + "";

                                            _some_received_messages[5]._message = _program_name0;
                                            _some_received_messages[5]._originalMsg = _program_name0;
                                            _some_received_messages[5]._messageCut = _program_name0;
                                            _some_received_messages[5]._specialMessage = 2;
                                            _some_received_messages[5]._specialMessageLineX = 0;
                                            _some_received_messages[5]._specialMessageLineY = 0;
                                            _some_received_messages[5]._orilineX = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                            _some_received_messages[5]._orilineY = (Console.WindowHeight / 2) + 1;
                                            _some_received_messages[5]._lineX = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                            _some_received_messages[5]._lineY = (Console.WindowHeight / 2) + 9;
                                            _some_received_messages[5]._count = 0;
                                            _some_received_messages[5]._swtch0 = 1;
                                            _some_received_messages[5]._swtch1 = 0;
                                            _some_received_messages[5]._delay = 50;
                                            _some_received_messages[5]._looping = 0;

                                            _data00_IN._received_switch_in = 0;
                                            _data00_IN._received_switch_out = 0;
                                            _data00_IN._sending_switch_in = 0;
                                            _data00_IN._sending_switch_out = 0;

                                            _data00_IN._current_menu = _data00_OUT._current_menu;
                                            _data00_IN._menuOption = _lastMenuOption;

                                            SC_object_messenger_dispatcher.SC_message_object objecterer = _data00_IN;
                                            _data00_OUT = SC_Console_Menu.SC_Console_Menu._console_worker_menu(objecterer);
                                            _lastMenu = _data00_OUT._current_menu;
                                            _lastMenuOption = "";
                                        }
                                        else if (_current_menu00 == 1)
                                        {
                                            _program_name0 = _current_menu00 + "";

                                            _some_received_messages[5]._message = _program_name0;
                                            _some_received_messages[5]._originalMsg = _program_name0;
                                            _some_received_messages[5]._messageCut = _program_name0;
                                            _some_received_messages[5]._specialMessage = 2;
                                            _some_received_messages[5]._specialMessageLineX = 0;
                                            _some_received_messages[5]._specialMessageLineY = 0;
                                            _some_received_messages[5]._orilineX = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                            _some_received_messages[5]._orilineY = (Console.WindowHeight / 2) + 1;
                                            _some_received_messages[5]._lineX = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                            _some_received_messages[5]._lineY = (Console.WindowHeight / 2) + 9;

                                            _some_received_messages[5]._count = 0;
                                            _some_received_messages[5]._swtch0 = 1;
                                            _some_received_messages[5]._swtch1 = 0;
                                            _some_received_messages[5]._delay = 50;
                                            _some_received_messages[5]._looping = 0;

                                            _data00_IN._received_switch_in = 0;
                                            _data00_IN._received_switch_out = 0;
                                            _data00_IN._sending_switch_in = 0;
                                            _data00_IN._sending_switch_out = 0;

                                            _data00_IN._current_menu = _data00_OUT._current_menu;
                                            _data00_IN._menuOption = _lastMenuOption;

                                            SC_object_messenger_dispatcher.SC_message_object objecterer = _data00_IN;
                                            _data00_OUT = SC_Console_Menu.SC_Console_Menu._console_worker_menu(objecterer);
                                            _lastMenu = _data00_OUT._current_menu;
                                            _lastMenuOption = "";
                                        }
                                    }
                                }
                                Thread.Sleep(1);
                                goto _thread_looper;

                            }, _someReceivedObject0);
                            _worker_000_has_init = 6;
                        }

                        /*for (int L0_IN = 0; L0_IN < _main_object.Length; L0_IN++) //_main_thread_lOOp_object.Length
                        {
                            _main_object[L0_IN] = null;
                            _main_object[L0_IN] = (_someObject)_someReceivedObject0[L0_IN];
                        }*/




                        /*if (has_init_directx == 1)
                        {
                            if (_KeyboardState != null)
                            {
                                if (_consoleKeySwitch == 0)
                                {
                                    if (_KeyboardState.PressedKeys.Contains(Key.Grave))
                                    {
                                        if (_init_directX_standard == 2)
                                        {
                                            consoleGraphics.clearConsole(_someReceivedObject0);

                                            //Console.CursorVisible = true;

                                            //_shutoff_console_writer = 0;
                                            //_shutoff_console_reader = 0;

                                            //_manual_reset_event_worker.Set();
                                            //_manual_reset_event_reader.Set();
                                            //_manual_reset_event_writer.Set();                                                  

                                            _init_directX_standard = 3;
                                        }
                                        else if (_init_directX_standard == 3)
                                        {
                                            //Console.CursorVisible = false;
                                            //_shutoff_console_writer = 1;
                                            //_shutoff_console_reader = 1;

                                            //_manual_reset_event_worker.WaitOne();
                                            _init_directX_standard = 2;
                                        }
                                        _consoleKeySwitch = 1;
                                    }
                                    else
                                    {

                                    }
                                }
                            }
                        }*/



                        if (_consoleKeySwitch == 1)
                        {
                            if (_consoleKeyCounter >= 75)
                            {
                                _consoleKeySwitch = 0;
                                _consoleKeyCounter = 0;
                            }
                            _consoleKeyCounter++;
                        }




                        if (_some_other_swtch == 1)
                        {
                            //Console.WriteLine("tester");
                            if (has_init_directx == 0)
                            {
                                if (init_directX_main_swtch == 2 || init_vr_main_swtch == 2)
                                {
                                    SC_skYaRk_VR_V007.DSystemConfiguration.DSystemConfiguration config = new SC_skYaRk_VR_V007.DSystemConfiguration.DSystemConfiguration("skYaRk", 1920, 1080, false, false);
                                    handler = _SC_SYSTEMS._SC_CONSOLE_CORE.handle;// SC_Console_CORE.handle;

                                    for (int x = 0; x < Console.WindowWidth; x++)
                                    {
                                        for (int y = 0; y < Console.WindowWidth; y++)
                                        {
                                            _SC_SYSTEMS._SC_CONSOLE_WRITER.Draw(x, y, " ");
                                        }
                                    }



                                    _console_worker_tasker = Task<SC_object_messenger_dispatcher.SC_message_object[]>.Factory.StartNew((tester0001) =>
                                    {
                                        //directxwindow = new SC_Console_DIRECTX();
                                        //directxwindow.Initialize(config, handler, _SC_SYSTEMS._SC_CONSOLE_WRITER);

                                        consoleGraphics = new SC_Console_GRAPHICS();
                                        consoleGraphics.Initialize(config, handler, _SC_SYSTEMS._SC_CONSOLE_WRITER);

                                    //_manual_reset_event_writer.Set();
                                    //_shutoff_console_writer = 2;

                                    _threaLooper:

                                        if (has_init_directx == 1)
                                        {

                                            if (init_directX_main_swtch == 3)
                                            {
                                                //consoleGraphics.FrameDX(_someReceivedObject0);
                                            }

                                            if (init_vr_main_swtch == 3)
                                            {
                                                try
                                                {
                                                    //Console.WriteLine("test000");
                                                    _someReceivedObject0 = consoleGraphics.FrameVRTWO(_someReceivedObject0);
                                                }
                                                catch (Exception ex)
                                                {
                                                    //Console.WriteLine(ex.ToString());
                                                }


                                                /*

                                                init_vr_main_swtch = 4;*/

                                            }
                                        }
                                        //_manual_reset_event_writer.Set();
                                        //_shutoff_console_writer = 2;
                                        //_init_directX_vr = 0;
                                        Thread.Sleep(1);
                                        goto _threaLooper;


                                    }, _someReceivedObject0);





                                    if (init_directX_main_swtch == 2)
                                    {
                                        init_directX_main_swtch = 3;
                                    }

                                    if (init_vr_main_swtch == 2)
                                    {
                                        init_vr_main_swtch = 3;
                                    }
                                    has_init_directx = 1;

                                }
                            }
                        }
















                        /*for (int i = 0; i < _someReceivedObject0.Length; i++)
                        {
                            tester001[i] = _someReceivedObject0[i];
                        }*/


                        /* Thread.Sleep(1);
                     }

                     goto _thread_main_loop;
                 }, _someReceivedObject0);*/


                        Thread.Sleep(1);
                        goto _thread_main_loop;
                    }
                    //_thread_start:
                }, 0); //100000 //999999999

                _mainTasker00.IsBackground = true;
                _mainTasker00.Priority = ThreadPriority.Normal; //AboveNormal
                _mainTasker00.SetApartmentState(ApartmentState.STA);
                _mainTasker00.Start();




                _someInit = 2;
            }

            if (_someInit == 2)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    goto _main_thread_Loop_x00;
                }
                else
                {
                    goto _main_thread_Loop_x00;
                }
            }
            else
            {
                ////System.Windows.MessageBox.Show("lOOp", "CONSOLE");
                goto _main_thread_Loop_x00;
            }


            Console.WriteLine("Hello World!");
        }










        public static SC_object_messenger_dispatcher.SC_message_object[] _console_worker_one(SC_object_messenger_dispatcher.SC_message_object[] _main_object) //CHAIN 1
        {

            ////System.Windows.MessageBox.Show("worker one is still working? ", "CONSOLE");
            /*if (_isFirstTaskAliveCounter >= 0)
            {
                ////////System.Windows.MessageBox.Show("_IN_1000_Console_WRITER", "Console");
                _msg_prog._message = "_mainTask " + _isFirstTaskAlive;
                _msg_prog._lineX = 0;
                _msg_prog._lineY = 1;
                _SC_SYSTEMS._SC_CONSOLE_WRITER._Console_writer_message_queue.Add(_msg_prog);

                _isFirstTaskAlive++;
                _isFirstTaskAliveCounter = 0;
            }
            _isFirstTaskAliveCounter++;

            for (int L0_IN = 0; L0_IN < 1; L0_IN++) //_main_thread_lOOp_object.Length
            {
                _someReceivedObject1[L0_IN] = (_someObject)_main_object[L0_IN];
            }

            return _someReceivedObject1;*/

            try
            {
                for (int L0_IN = 0; L0_IN < _main_object.Length; L0_IN++) //_main_thread_lOOp_object.Length
                {


                    int _work_doner = _main_object[L0_IN]._work_done;

                    //if (_work_doner == 0)
                    {
                        int _received_switch_in00 = _main_object[L0_IN]._received_switch_in;   //1
                        int _received_switch_out00 = _main_object[L0_IN]._received_switch_out; //0
                        int _sending_switch_in00 = _main_object[L0_IN]._sending_switch_in;     //0
                        int _sending_switch_out00 = _main_object[L0_IN]._sending_switch_out;   //0
                        List<int[]> _chain_Of_Tasks00 = _main_object[L0_IN]._chain_Of_Tasks0;
                        int _timeOut00 = _main_object[L0_IN]._timeOut0;
                        int _ParentTaskThreadID00 = _main_object[L0_IN]._ParentTaskThreadID0;
                        int _main_cpu_count00 = _main_object[L0_IN]._main_cpu_count;
                        string _passTest00 = _main_object[L0_IN]._passTest;
                        int _welcomePackage00 = _main_object[L0_IN]._welcomePackage;
                        int _current_menu00 = _main_object[L0_IN]._current_menu;
                        int _last_current_menu00 = _main_object[L0_IN]._last_current_menu;
                        /*if (_passTest00.ToLower() == "go fuck yourself")
                        {
                            //System.Windows.MessageBox.Show("go fuck yourself " , "Console");
                        }*/
                        _someReceivedObject1[L0_IN] = _main_object[L0_IN];

                        if (_received_switch_in00 == 0 &&
                            _received_switch_out00 == 0 &&
                            _sending_switch_in00 == 0 &&
                            _sending_switch_out00 == 0)
                        {
                            _someReceivedObject1[L0_IN]._received_switch_in = 0;
                            _someReceivedObject1[L0_IN]._received_switch_out = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_in = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_out = 0;
                        }
                        else if (_received_switch_in00 == 1 &&
                                 _received_switch_out00 == 0 &&
                                 _sending_switch_in00 == 0 &&
                                 _sending_switch_out00 == 0)
                        {
                            if (_welcomePackage00 == -1)
                            {
                                //System.Windows.MessageBox.Show("here 00", "Console");
                                _someReceivedObject1[L0_IN]._received_switch_in = 0;
                                _someReceivedObject1[L0_IN]._received_switch_out = 0;
                                _someReceivedObject1[L0_IN]._sending_switch_in = 0;
                                _someReceivedObject1[L0_IN]._sending_switch_out = 0;
                            }
                            else if (_welcomePackage00 == 0)
                            {
                                //_user_name = Console.ReadLine();

                                //_event_manual_reset_worker_one.WaitOne();

                                if (_passTest00.ToLower() == "nine" || _passTest00.ToLower() == "ninekorn" || _passTest00.ToLower() == "9")
                                {
                                    _someReceivedObject1[L0_IN]._received_switch_in = 1;
                                    _someReceivedObject1[L0_IN]._received_switch_out = 1;
                                    _someReceivedObject1[L0_IN]._sending_switch_in = 0;
                                    _someReceivedObject1[L0_IN]._sending_switch_out = 0;
                                    _someReceivedObject1[L0_IN]._passTest = _passTest00.ToLower();
                                    _someReceivedObject1[L0_IN]._welcomePackage = 1;
                                    _someReceivedObject1[L0_IN]._work_done = 1;
                                    _someReceivedObject1[L0_IN]._main_menu = 0;
                                }
                                else
                                {
                                    _someReceivedObject1[L0_IN]._received_switch_in = 1;
                                    _someReceivedObject1[L0_IN]._received_switch_out = 0;
                                    _someReceivedObject1[L0_IN]._sending_switch_in = 0;
                                    _someReceivedObject1[L0_IN]._sending_switch_out = 0;
                                    _someReceivedObject1[L0_IN]._welcomePackage = 0;
                                    _someReceivedObject1[L0_IN]._work_done = 1;

                                    _someReceivedObject1[L0_IN]._passTest = "";
                                }
                            }
                        }
                        else if (_received_switch_in00 == 0 &&
                                 _received_switch_out00 == 1 &&
                                 _sending_switch_in00 == 0 &&
                                 _sending_switch_out00 == 0)
                        {
                            _someReceivedObject1[L0_IN]._received_switch_in = 0;
                            _someReceivedObject1[L0_IN]._received_switch_out = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_in = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_out = 0;
                        }
                        else if (_received_switch_in00 == 0 &&
                                 _received_switch_out00 == 0 &&
                                 _sending_switch_in00 == 1 &&
                                 _sending_switch_out00 == 0)
                        {
                            _someReceivedObject1[L0_IN]._received_switch_in = 0;
                            _someReceivedObject1[L0_IN]._received_switch_out = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_in = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_out = 0;
                        }
                        else if (_received_switch_in00 == 0 &&
                               _received_switch_out00 == 0 &&
                               _sending_switch_in00 == 0 &&
                               _sending_switch_out00 == 1)
                        {

                            string _optionCommand = Console.ReadLine();

                            if (_optionCommand.ToLower() == "option" ||
                                _optionCommand.ToLower() == "command" ||
                                _optionCommand.ToLower() == "options" ||
                                _optionCommand.ToLower() == "commands")
                            {

                                _someReceivedObject1[L0_IN]._received_switch_in = 0;
                                _someReceivedObject1[L0_IN]._received_switch_out = 0;
                                _someReceivedObject1[L0_IN]._sending_switch_in = 0;
                                _someReceivedObject1[L0_IN]._sending_switch_out = 0;
                            }
                            else
                            {
                                _someReceivedObject1[L0_IN]._received_switch_in = 0;
                                _someReceivedObject1[L0_IN]._received_switch_out = 0;
                                _someReceivedObject1[L0_IN]._sending_switch_in = 0;
                                _someReceivedObject1[L0_IN]._sending_switch_out = 0;
                            }
                        }
                        else if (_received_switch_in00 == 1 &&
                                _received_switch_out00 == 1 &&
                                _sending_switch_in00 == 0 &&
                                _sending_switch_out00 == 0)
                        {
                            _someReceivedObject1[L0_IN]._received_switch_in = 0;
                            _someReceivedObject1[L0_IN]._received_switch_out = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_in = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_out = 0;
                            //System.Windows.MessageBox.Show("CHAIN01_IN_1100 =>CHAIN01_OUT_0000", "Console");
                        }
                        else if (_received_switch_in00 == 1 &&
                             _received_switch_out00 == 0 &&
                             _sending_switch_in00 == 1 &&
                             _sending_switch_out00 == 0)
                        {
                            _someReceivedObject1[L0_IN]._received_switch_in = 0;
                            _someReceivedObject1[L0_IN]._received_switch_out = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_in = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_out = 0;
                            //System.Windows.MessageBox.Show("CHAIN01_IN_1010 =>CHAIN01_OUT_0000", "Console");
                        }
                        else if (_received_switch_in00 == 1 &&
                               _received_switch_out00 == 0 &&
                               _sending_switch_in00 == 0 &&
                               _sending_switch_out00 == 1)
                        {
                            _someReceivedObject1[L0_IN]._received_switch_in = 0;
                            _someReceivedObject1[L0_IN]._received_switch_out = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_in = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_out = 0;
                            //System.Windows.MessageBox.Show("CHAIN01_IN_1001 =>CHAIN01_OUT_0000", "Console");
                        }
                        else if (_received_switch_in00 == 0 &&
                            _received_switch_out00 == 1 &&
                            _sending_switch_in00 == 1 &&
                            _sending_switch_out00 == 0)
                        {
                            _someReceivedObject1[L0_IN]._received_switch_in = 0;
                            _someReceivedObject1[L0_IN]._received_switch_out = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_in = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_out = 0;
                            //System.Windows.MessageBox.Show("CHAIN01_IN_0110 =>CHAIN01_OUT_0000", "Console");
                        }
                        else if (_received_switch_in00 == 0 &&
                              _received_switch_out00 == 1 &&
                              _sending_switch_in00 == 0 &&
                              _sending_switch_out00 == 1)
                        {
                            _someReceivedObject1[L0_IN]._received_switch_in = 0;
                            _someReceivedObject1[L0_IN]._received_switch_out = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_in = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_out = 0;
                            //System.Windows.MessageBox.Show("CHAIN01_IN_0101 =>CHAIN01_OUT_0000", "Console");
                        }
                        else if (_received_switch_in00 == 0 &&
                              _received_switch_out00 == 0 &&
                              _sending_switch_in00 == 1 &&
                              _sending_switch_out00 == 1)
                        {
                            _someReceivedObject1[L0_IN]._received_switch_in = 0;
                            _someReceivedObject1[L0_IN]._received_switch_out = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_in = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_out = 0;
                            //System.Windows.MessageBox.Show("CHAIN01_IN_0011 =>CHAIN01_OUT_0000", "Console");
                        }
                        else if (_received_switch_in00 == 1 &&
                                   _received_switch_out00 == 0 &&
                                   _sending_switch_in00 == 1 &&
                                   _sending_switch_out00 == 1)
                        {
                            _someReceivedObject1[L0_IN]._received_switch_in = 0;
                            _someReceivedObject1[L0_IN]._received_switch_out = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_in = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_out = 0;

                            //System.Windows.MessageBox.Show("CHAIN01_IN_1011 =>CHAIN01_OUT_0000", "Console");
                        }
                        else if (_received_switch_in00 == 0 &&
                                _received_switch_out00 == 1 &&
                                _sending_switch_in00 == 1 &&
                                _sending_switch_out00 == 1)
                        {
                            _someReceivedObject1[L0_IN]._received_switch_in = 0;
                            _someReceivedObject1[L0_IN]._received_switch_out = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_in = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_out = 0;
                            //System.Windows.MessageBox.Show("CHAIN01_IN_0111 =>CHAIN01_OUT_0000", "Console");
                        }

                        else if (_received_switch_in00 == 1 &&
                                _received_switch_out00 == 1 &&
                                _sending_switch_in00 == 0 &&
                                _sending_switch_out00 == 1)
                        {
                            _someReceivedObject1[L0_IN]._received_switch_in = 0;
                            _someReceivedObject1[L0_IN]._received_switch_out = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_in = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_out = 0;
                            //System.Windows.MessageBox.Show("CHAIN01_IN_1101 =>CHAIN01_OUT_0000", "Console");
                        }
                        else if (_received_switch_in00 == 1 &&
                                _received_switch_out00 == 1 &&
                                _sending_switch_in00 == 1 &&
                                _sending_switch_out00 == 0)
                        {
                            _someReceivedObject1[L0_IN]._received_switch_in = 0;
                            _someReceivedObject1[L0_IN]._received_switch_out = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_in = 0;
                            _someReceivedObject1[L0_IN]._sending_switch_out = 0;
                            //System.Windows.MessageBox.Show("CHAIN01_IN_1110 =>CHAIN01_OUT_0000", "Console");
                        }
                        else if (_received_switch_in00 == 1 &&
                              _received_switch_out00 == 1 &&
                              _sending_switch_in00 == 1 &&
                              _sending_switch_out00 == 1)
                        {
                            //System.Windows.MessageBox.Show("END OF LINE TEST", "Console");

                            ////System.Windows.MessageBox.Show("CHAIN01_IN_1111 =>CHAIN01_OUT_0000", "Console");

                            //_someReceivedObject1[L0_IN]._received_switch_in = 0;
                            //_someReceivedObject1[L0_IN]._received_switch_out = 0;
                            //_someReceivedObject1[L0_IN]._sending_switch_in = 0;
                            //_someReceivedObject1[L0_IN]._sending_switch_out = 0;

                            if (_welcomePackage00 == 998)
                            {
                                //_shutoff_console_writer = 1;
                                //_shutoff_console_reader = 1;

                                init_directX_main_swtch = 2;
                                //_manual_reset_event_worker.WaitOne();
                            }
                            else if (_welcomePackage00 == 999)
                            {
                                //_shutoff_console_writer = 1;
                                //_shutoff_console_reader = 1;

                                init_vr_main_swtch = 2;
                                //_manual_reset_event_worker.WaitOne();

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return _someReceivedObject1;// _TASK_00_WK.Result;
        }

















        static int _InitializeKeyboard()
        {
            int _InitializeKeyboardAuth = 1;
            try
            {
                directInput = new DirectInput();
                _Keyboard = new SharpDX.DirectInput.Keyboard(directInput);
                _Keyboard.Properties.BufferSize = 128;
                _Keyboard.Acquire();
            }
            catch
            {

                _InitializeKeyboardAuth = 0;
            }
            return _InitializeKeyboardAuth;
        }


        private static bool ReadKeyboard()
        {
            var resultCode = SharpDX.DirectInput.ResultCode.Ok;
            //_KeyboardState = new KeyboardState();

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
    }
}
