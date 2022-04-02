using System;
using System.Runtime.InteropServices;

using _sc_core_systems._sc_core;
using _sc_core_systems._sc_console;
using _sc_core_systems._sc_console_menu;
using _sc_core_systems._sc_message_object;
using _sc_core_systems.SC_Graphics;

using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using SharpDX.DirectInput;

using System.Text;

//using System.Collections.Generic;
//using SharpDX.XAudio2;
using System.IO;
using SharpDX.Multimedia;
using System.Diagnostics;

//using Microsoft.ReportingServices.QueryDesigners.Interop;

//[DllImport("Shell32.dll")]
//private  extern int SHChangeNotify(int eventId, uint flags, IntPtr dwItem1, IntPtr dwItem2);
//using System.Diagnostics;
//using SharpDX.XAudio2;


//using _sc_core_systems.sound;



//using SharpDX.DirectSound;
//using SharpDX.XAudio2;
using SharpDX;
using SharpDX.IO;
//using System.Globalization;
using _sc_core_systems.sound;

//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Windows.Forms;
using System.Xml;
//using System.Drawing.Imaging;
//using System.Drawing.Drawing2D;
//using System.Reflection;
//using Microsoft.Win32.SafeHandles;

using SharpDX.XAudio2;


namespace _sc_core_systems
{
    internal class Program
    {
        static byte[] _sound_byte_array;
        static AudioBuffer sc_current_sound_buffer;

        static XmlDocument doc;
        static XmlTextWriter writer = new XmlTextWriter(Console.Out);
        static XmlElement root;

        static int _frames_to_access = 0;
        static int _frames_to_access_counter = 0;

        static int _records_counter = 0;

        /*
        public static string Beautify(XmlDocument doc)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }
            return sb.ToString();
        }*/



        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);





        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        static int _processorCount = 1;
        static int _Max_Size_00 = 16;
        static _sc_message_object._sc_message_object[] _main_received_messages;//

        static _messager[] _sec_received_messages;

        public static _sc_console._sc_console_keyboard_input _keyboard_input;
        static string _program_name = "sc core systems";
        static _sc_message_object._sc_message_object _data00_IN;
        static _sc_message_object._sc_message_object _data00_OUT;
        public static int init_directX_main_swtch = 2;
        public static int init_vr_main_swtch = 2;
        static int has_init_directx = 0;


        static void Main(string[] args)
        {
            ////////////////////
            ///PROCESSOR COUNT//
            ////////////////////
            for (int j = 0; j < 1; j++)
            {
                try
                {
                    _processorCount = _sc_system_info.getSystemProcessorCount();
                }
                catch (Exception ex)
                {
                    MessageBox((IntPtr)0, "cannot get processor info: " + ex.ToString() + "", "_sc_core_systems error", 0);
                    //something is wrong, todo something else later. but not implemented yet.
                    break;
                }
            }
            if (_processorCount < 1)
            {
                _processorCount = 1;
            }
            ////////////////////
            ///PROCESSOR COUNT//
            ////////////////////
            ///
            ////////////////////
            ///KEYBOARD INPUT///
            ////////////////////
            for (int j = 0; j < 1; j++)
            {
                try
                {
                    _keyboard_input = new _sc_console._sc_console_keyboard_input();
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
            ///
            /////////////////////////////// : But i am not totally sure. I certainly can use this struct and send it to an "async" task and retrieve the data
            ///////////////////////////////   right away even if i sometimes or always "modify" the data before sending it to a task and then right after the task
            ///////////////////////////////   finishes, i am retrieving the data from that async task. But i am not sure yet if my shit works "completely". as i am
            ///////////////////////////////   not "declaring" the task as "async" BUT i am only starting that task once and let it run whatever it has to run in a loop.
            ///message_thread_safe_kinda///   everybody does that it's for sure anyway, i didn't invent anything here. It's just that this struct is very important
            ///////////////////////////////   for the rest of the program to run and this first struct will be able to communicate with all of my program in INTs
            ///////////////////////////////   used as binary code "kinda" for every option that i want to implement in order to alleviate the load of menu mecanics.
            ///////////////////////////////   the namespace to get it really can really be simplified. i mean really. wtheck am i doing.
            ///////////////////////////////
            _main_received_messages = new _sc_message_object._sc_message_object[_Max_Size_00];


            for (int i = 0; i < _main_received_messages.Length; i++)
            {
                _main_received_messages[i] = new _sc_message_object._sc_message_object();
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

            //SYSTEMS
            _sc_systems _SC_SYSTEMS = new _sc_systems(_main_received_messages);
            if (_SC_SYSTEMS == null)
            {
                ////System.Windows.MessageBox.Show("_SC_SYSTEMS NULL", "Console");
            }
            else
            {
                ////System.Windows.MessageBox.Show("_SC_SYSTEMS !NULL", "Console");
            }
            //SYSTEMS

            //console reader object
            _sc_console._sc_console_reader._console_reader_data _console_reader_string;
            //console reader object

            ///////////////////////////////   
            ///////////////////////////////   
            ///////////////////////////////  
            ///////MAIN CONSOLE LOOP///////   
            ///////////////////////////////   
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

            SC_Console_GRAPHICS consoleGraphics = null;

            int _init_main_thread_in_out = 0;
            int _init_main = 1;
            int _worker_000_has_init = 0;
            int _start_thread_console_writer = 1;
            Task _console_writer_task;
            Task _console_reader_task;
            int _console_reader_canWork = 1;
            int startThread = 0;


            int _last_console_width = Console.WindowWidth;
            int _last_console_height = Console.WindowHeight;

            int _counter_reset_console_borders = 0;

            int _lastMenu = -2;
            string _lastMenuOption = "";
            string _lastUsername = "";
            int _some_other_swtch = 0;







            string _lastpath = "";


             
            //DSound _sound;// = new DSound();
            //SoundPlayer _sound_player = new SoundPlayer();
            string _index = "";



            Random newRandom = new Random();
            Random rnd = new Random();
            Random getrandom = new Random();
            object syncLock = new object();
            int numberOfXMLToCreate = 1;
            string path = "";
            int _has_pressed_w = 0;



            int _next_frame = 0;

            string last_xml_filepath = "";
            string last_wave_filepath = "";

            DateTime _time_of_recording_start =  DateTime.Now;
            DateTime _time_of_recording_end = DateTime.Now;


            int sc_can_start_rec_counter = 0;
            int sc_can_start_rec_counter_before_add_index = 0;

            int sc_play_file = 0;
            int sc_play_file_counter = 0;

            int sc_save_file = 0;
            int sc_save_file_counter = 0;

            int sc_start_recording = 0;
            int sc_start_recording_counter = 0;

            _sound_byte_array = new byte[44100];

        _main_thread_Loop_x00:

            if (_init_main == 1)
            {
                try
                {
                    Thread _mainTasker00 = new Thread((tester0000) =>
                    {
                    _thread_main_loop:


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
                                _SC_SYSTEMS._SC_CONSOLE_CORE._reset_console_borders();
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

                                _sec_received_messages = _SC_SYSTEMS._SC_CONSOLE_WRITER._console_writer(_sec_received_messages);

                                Thread.Sleep(1);

                                goto _thread_loop_console;
                                //////CONSOLE WRITER <=
                            }, _main_received_messages);
                            _start_thread_console_writer = 2;
                        }

                        //CONFIRM CONSOLE WRITER IS WORKING=>
                        if (_worker_000_has_init == 1)
                        {
                            if (_SC_SYSTEMS != null)
                            {
                                if (_SC_SYSTEMS._SC_CONSOLE_WRITER != null)
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
                            if (_SC_SYSTEMS != null)
                            {
                                if (_SC_SYSTEMS._SC_CONSOLE_READER != null)
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
                            if (_SC_SYSTEMS != null)
                            {
                                if (_SC_SYSTEMS._SC_CONSOLE_READER != null)
                                {
                                    var _program_name0 = "Press Enter";
                                    var _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                    var _initY0 = (Console.WindowHeight / 2) + 1;

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
                                        _console_reader_string = _SC_SYSTEMS._SC_CONSOLE_READER._console_reader(_console_reader_object);
                                    }

                                    if (_SC_SYSTEMS._SC_CONSOLE_READER._main_has_init == 1)
                                    {
                                        _console_reader_string._console_reader_message = "";
                                        _console_reader_string._has_message_to_display = 0;

                                        var _program_name0 = "WELCOME";
                                        var _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                        var _initY0 = (Console.WindowHeight / 2) - 1;

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
                                        _SC_SYSTEMS._SC_CONSOLE_READER._main_has_init = 2;
                                    }

                                    if (startThread == 3 && _console_reader_string._has_message_to_display == 1)
                                    {
                                        if (_console_reader_string._console_reader_message.ToLower() == "nine" || _console_reader_string._console_reader_message.ToLower() == "ninekorn" || _console_reader_string._console_reader_message.ToLower() == "9")
                                        {
                                            var _program_name0 = "Access Authorized";
                                            var _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                            var _initY0 = (Console.WindowHeight / 2) + 2;

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

                                            _lastUsername = _console_reader_string._console_reader_message;
                                            _console_reader_string._console_reader_message = "";
                                            startThread = 4;
                                        }
                                        else if (_console_reader_string._console_reader_message.ToLower() != " " || _console_reader_string._console_reader_message.ToLower() != "")
                                        {
                                            var _program_name0 = "Access Denied";
                                            var _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                            var _initY0 = (Console.WindowHeight / 2) + 2;

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
                                                var _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                                var _initY0 = (Console.WindowHeight / 2) + 2;

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

                                                _main_received_messages = _sc_console_menu._sc_console_menu_00._console_menu(_main_received_messages);

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
                                                _main_received_messages = _sc_console_menu._sc_console_menu_00._console_menu(_main_received_messages);
                                                _some_other_swtch = 1;
                                            }
                                        }
                                        else
                                        {
                                            var _program_name0 = "Option Not Implemented";
                                            var _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                            var _initY0 = (Console.WindowHeight / 2) + 2;

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
                                        }
                                    }

                                    Thread.Sleep(1);
                                }
                            }, _main_received_messages);
                            _worker_000_has_init = 5;
                        }

                        if (_worker_000_has_init == 5)
                        {
                            Task _console_worker_task = Task<object[]>.Factory.StartNew((tester0001) =>
                            {
                                while (true)
                                {
                                    if (_worker_000_has_init == 2)
                                    {
                                        int _welcomePackage00 = _main_received_messages[0]._welcomePackage;

                                        if (_welcomePackage00 == 0)
                                        {
                                            _main_received_messages = _sc_console_menu._sc_console_menu_00._console_menu(_main_received_messages);
                                        }
                                        else if (_welcomePackage00 == 1)
                                        {
                                            int _current_menu00 = _data00_OUT._current_menu;

                                            if (_lastMenu != _current_menu00)
                                            {
                                                var _program_name0 = _current_menu00 + "";
                                                var _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                                var _initY0 = (Console.WindowHeight / 2) + 9;
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
                                                _data00_OUT = _sc_console_menu._sc_console_menu_01._console_menu(objecterer);

                                                _lastMenu = _data00_OUT._current_menu;
                                                _lastMenuOption = "";
                                            }
                                            else if (_current_menu00 == 0)
                                            {
                                                var _program_name0 = _current_menu00 + "";
                                                var _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                                var _initY0 = (Console.WindowHeight / 2) + 9;
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
                                                _data00_OUT = _sc_console_menu._sc_console_menu_01._console_menu(objecterer);
                                                _lastMenu = _data00_OUT._current_menu;
                                                _lastMenuOption = "";
                                            }
                                            else if (_current_menu00 == 1)
                                            {
                                                var _program_name0 = _current_menu00 + "";
                                                var _initX0 = (Console.WindowWidth / 2) - (_program_name0.Length / 2);
                                                var _initY0 = (Console.WindowHeight / 2) + 9;
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
                                                _data00_OUT = _sc_console_menu._sc_console_menu_01._console_menu(objecterer);
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
                                    _sc_system_configuration config = new _sc_system_configuration("sc core systems", 1920, 1080, false, false);
                                    IntPtr handler = _SC_SYSTEMS._SC_CONSOLE_CORE.handle;

                                    if (handler == IntPtr.Zero)
                                    {
                                        MessageBox((IntPtr)0, "test 00: ", "_sc_core_systems error", 0);
                                    }

                                    for (int x = 0; x < Console.WindowWidth; x++)
                                    {
                                        for (int y = 0; y < Console.WindowWidth; y++)
                                        {
                                            _SC_SYSTEMS._SC_CONSOLE_WRITER.Draw(x, y, " ");
                                        }
                                    }

                                    try
                                    {
                                        consoleGraphics = new SC_Console_GRAPHICS();
                                        consoleGraphics.Initialize(config, handler, _SC_SYSTEMS._SC_CONSOLE_WRITER);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox((IntPtr)0, "CONSOLE GRAPHICS" + ex.ToString(), "_sc_core_systems error", 0);
                                    }

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


                            if (has_init_directx == 1)
                            {

                                if (init_directX_main_swtch == 3)
                                {

                                }

                                if (init_vr_main_swtch == 3)
                                {

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
                                                _main_received_messages = consoleGraphics.FrameVRTWO(_main_received_messages);
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox((IntPtr)0,"" + ex.ToString(), "Oculus Error", 0);
                                            }
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                        Thread.Sleep(1);
                                        goto _thread_looper;
                                    };

                                    threaders.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs argers)
                                    {

                                    };

                                    threaders.RunWorkerAsync();

                                    init_vr_main_swtch = 4;
                                }
                            }
                        }



                        Thread.Sleep(1);
                        goto _thread_main_loop;
                    }, 100000);

                    _mainTasker00.IsBackground = false;
                    _mainTasker00.SetApartmentState(ApartmentState.STA);
                    _mainTasker00.Start();
                }
                catch
                {
                    MessageBox((IntPtr)0, "stack overflow possible. no clue what it is anyway ", "Oculus Error", 0);
                }





                _init_main = 2;
            }

            if (_init_main == 2)
            {
                //found with a google search that when using this option it at least makes you able to have a "prompt" or something in order for the console 
                //to have a looping "main" thread so i just added a goto loop to "catch" this first frame and to loop it.
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
                //System.Windows.MessageBox.Show("lOOp", "CONSOLE");
                goto _main_thread_Loop_x00;
            }

            Console.WriteLine("nope... no world!");
        }



    }
}












/*Process p = new Process();
p.StartInfo = new ProcessStartInfo()
{
    FileName = filename
};
p.Start();
p.Refresh();
p.Close();*/

// 1
//SPECTRUM WITHOUT ASSOCIATION TO WORDS
//ive got bytes.
//ive got direction forward/right/up for all vertices of all objects of all instances of all physics engines i can spawn.
//i need a sound spectrum and i think the easiest way in order to alleviate performance issues is to use shaders.
//the sound spectrum will be a visual grid, i would prefer a x * z or x * y and small spectrum. I have it think 44100 bytes max allowance so i get 44100 static cubes in there with vertices y modifications of top
//face and ive got the spectrum. 

// 2
//SPECTRUM WITH ASSOCIATION TO WORDS AND ASSOCIATION TO MOVEMENT AND DIRECTIONS AND OBJECTIVES AND ETC.
//I PRESS RECORD AND I SAY "ONE"
//AFTER I HAVE SAID IT, I PRESS SAVE
//THE WORD ONE OF MY VOICE IS STORED IN MEMORY. I CAN EVEN WRITE THIS TO FILE IN XML AS I HAVE ALSO XML CREATION PROGRAM GENERATORS FOR VOID EXPANSE.
//I CAN ASK THE COMPUTER TO EVEN SAY IT BACK . why the caps i dont know lets get back to small letters. but for it to just say it back is meaningless.
//the computer has to understand from one word that it can lower the volume of that word sound, and even ... if i go anal, in each languages. But i don't have system.speech so it's a manual thing again.
//Void Expanse does not have morse code based on frames and distance between keyboard keypresses activated with voice. this is what i want to code for Void Expanse. It will be sooo awesome to play. 
//Drones do this: "yes boss"
//Drones do that: "f*** y** boss" etc.
//Drones stop agression,
//Drones surround target//
//Drones compress target
//Drones push target
//Drones direct target to point of interest /waypoint
//Drones escort target to point of interest /waypoint
//Drones engage static charge on part etc..

//Drones search etc. it's just that i don't have the time to work 24/7. But i just cannot believe that my 4-5 years of coding is resulting in me having to shit out all i've got. i don't get that part.

//MessageBox((IntPtr)0, "_sound_byte_array " + _sound_byte_array.Length, "_sc_core_systems error", 0);

/*SpeechSynthesizer synth = new SpeechSynthesizer();

// Configure the audio output.   
synth.SetOutputToDefaultAudioDevice();

// Speak a string.  
synth.Speak("This example demonstrates a basic use of Speech Synthesizer");
*/



//byte[] buffer = new byte[16 * 1024];
/*using (var soundStream = new SoundStream(nativeFileStream))
{
    int read;
    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
    {
        ms.Write(buffer, 0, read);
    }
    return ms.ToArray();
}*/


//string playCommand = "open " + _lastpath + " type waveaudio alias recsound";
//mciSendString(playCommand, null, 0, IntPtr.Zero);
//mciSendString("play recsound", null, 0, IntPtr.Zero);

////const string FORMAT = @"open ""{0}"" type waveaudio alias wave";
//string command = String.Format(FORMAT, _lastpath);
//mciSendString(command, null, 0, IntPtr.Zero);

//PLaySoundFile(_audio_device, _lastpath);


//const string FORMAT = @"open ""{0}"" type waveaudio alias wave";
//string command = String.Format(FORMAT, _lastpath);
//mciSendString(command, null, 0, IntPtr.Zero);

//var command = "play recsound";
//mciSendString(command, null, 0, IntPtr.Zero);
//mciSendString(string.Format("open \"{0}\" type waveaudio alias wave", _lastpath), null, 0, IntPtr.Zero);


//string playCommand = "play " + "recsound" + " notify";
//mciSendString(playCommand, null, 0, IntPtr.Zero);

//string playCommand = "open " + _lastpath + " type waveaudio alias recsound";
//mciSendString(playCommand, null, 0, IntPtr.Zero);
//mciSendString("play recsound", null, 0, IntPtr.Zero);

//_sound = new DSound(_lastpath);
//_sound.Play(1000);
