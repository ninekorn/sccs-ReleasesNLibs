using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SharpDX.XAudio2;
using System.Threading;
using System.Threading.Tasks;

using System.IO;
using SharpDX.Multimedia;

//using Microsoft.ReportingServices.QueryDesigners.Interop;

//[DllImport("Shell32.dll")]
//private  extern int SHChangeNotify(int eventId, uint flags, IntPtr dwItem1, IntPtr dwItem2);


using SharpDX.DirectInput;


using System.Diagnostics;

namespace SC_skYaRk_VR_V007.SC_Console
{
    public static class SC_Console_VoRec
    {
        static int _startRecCounter = 0;


        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);


        static SC_object_messenger_dispatcher.SC_message_object _data00_IN = new SC_object_messenger_dispatcher.SC_message_object();


        static int startupItems = 1;

        static XAudio2 _xaudio2 = null;
        static MasteringVoice masteringVoice;

        static string _lastpath = "";

        static object _xAudioDevice = new object();


        static SC_object_messenger_dispatcher.SC_message_object _incoming_data = new SC_object_messenger_dispatcher.SC_message_object();

        static KeyboardState _assigned_keyboard;

        static float _keyboard_frame_adjuster = 0;
        static float _keyboard_frame_adjuster1 = 0;
        static float _keyboard_frame_max_frame = 100;
        static float _keyboard_frame_countdown0 = 1000;

        static int _keyboard_record_swtch = 0;

        static int _keyboard_other_swtch0 = 0;
        static int _keyboard_other_swtch1 = 1;


        static List<int> _adder = new List<int>();

        static Stopwatch _stopWatch_thread_time = new Stopwatch();

        static int _stopWatch_thread_swtch = 0;
        static int _stopWatch_thread_reset_swtch = 1;

        static int _main_counter_task = 0;



        static int _main_starter_items = 1;

        static Stopwatch _stopWatch_thread_secondary_time = new Stopwatch();
        static int _stopWatch_thread_secondary_swtch = 0;
        static int _stopWatch_thread_secondary_reset_swtch = 1;

        //static SC_skYaRk_VR_V007.SC_Console_WRITER._messager[] _some_received_messages;


        public static SC_object_messenger_dispatcher.SC_message_object _work_rec(object _main_object_in, SC_skYaRk_VR_V007.SC_Console_WRITER._messager _some_received_messages_in,out SC_skYaRk_VR_V007.SC_Console_WRITER._messager _some_received_messages_out) //CHAIN 1
        {
            _incoming_data = (SC_object_messenger_dispatcher.SC_message_object)_main_object_in;

            var tester0 = _incoming_data._someData[0];

            var tester1 = (KeyboardState)tester0;

            if (tester1 != null)
            {

            _thread_loop:


                if (_main_starter_items == 1)
                {
                    _stopWatch_thread_time.Stop();
                    _stopWatch_thread_time.Reset();
                    _stopWatch_thread_time.Start();

                    _main_starter_items = 0;
                }

                if (_keyboard_record_swtch == 1)
                {
                    if (_main_counter_task > 0)
                    {




                    }
                    else
                    {
                        if (_stopWatch_thread_time.Elapsed.TotalMilliseconds >= 5000)
                        {
                            //Console.WriteLine("Tim00: " + _stopWatch_thread_time.Elapsed.TotalMilliseconds);

                            if (_stopWatch_thread_secondary_time.Elapsed.Milliseconds >= 0)
                            {
                                //Console.WriteLine("you are allowed to type");

                                _some_received_messages_in._message = "recording is on";
                                _some_received_messages_in._originalMsg = "recording is on";
                                _some_received_messages_in._messageCut = "recording is on";
                                _some_received_messages_in._specialMessage = 2;
                                _some_received_messages_in._specialMessageLineX = 0;
                                _some_received_messages_in._specialMessageLineY = 0;
                                _some_received_messages_in._orilineX = 1;
                                _some_received_messages_in._orilineY = Console.WindowHeight - 4;
                                _some_received_messages_in._lineX = 1;
                                _some_received_messages_in._lineY = Console.WindowHeight - 4;
                                _some_received_messages_in._count = 0;
                                _some_received_messages_in._swtch0 = 1;
                                _some_received_messages_in._swtch1 = 1;
                                _some_received_messages_in._delay = 50;
                                _some_received_messages_in._looping = 1;

                                _some_received_messages_in._messager_list[0] = _some_received_messages_in;
                            }





                            _main_counter_task = 0;

                            _stopWatch_thread_time.Stop();
                            _stopWatch_thread_time.Reset();
                            _stopWatch_thread_time.Start();

                            _stopWatch_thread_secondary_time.Stop();
                            _stopWatch_thread_secondary_time.Reset();
                            _stopWatch_thread_secondary_time.Start();


                            _keyboard_record_swtch = 0;
                        }
                    }





                    _keyboard_frame_adjuster1++;
                }






                if (_keyboard_record_swtch == 0)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        if (_keyboard_frame_adjuster >= _keyboard_frame_max_frame)
                        {
                            if (_stopWatch_thread_time.Elapsed.Milliseconds >= 0)
                            {
                                if (_main_counter_task == 0)
                                {
                                    if (tester1.PressedKeys.Count > 0)
                                    {
                                        for (int j = 0; j < tester1.PressedKeys.Count; j++)
                                        {
                                            if (tester1.PressedKeys[j] == Key.R)
                                            {
                                                //Console.WriteLine("record start");

                                                _some_received_messages_in._message = "record start";
                                                _some_received_messages_in._originalMsg = "record start";
                                                _some_received_messages_in._messageCut = "record start";
                                                _some_received_messages_in._specialMessage = 2;
                                                _some_received_messages_in._specialMessageLineX = 0;
                                                _some_received_messages_in._specialMessageLineY = 0;
                                                _some_received_messages_in._orilineX = 1;
                                                _some_received_messages_in._orilineY = Console.WindowHeight - 3;
                                                _some_received_messages_in._lineX = 1;
                                                _some_received_messages_in._lineY = Console.WindowHeight - 3;
                                                _some_received_messages_in._count = 0;
                                                _some_received_messages_in._swtch0 = 1;
                                                _some_received_messages_in._swtch1 = 1;
                                                _some_received_messages_in._delay = 50;
                                                _some_received_messages_in._looping = -1;

                                                _some_received_messages_in._messager_list[1] = _some_received_messages_in;

                                                _keyboard_record_swtch = 1;
                                                _keyboard_frame_adjuster1 = 0;
                                                _keyboard_frame_adjuster = 0;
                                                _keyboard_other_swtch0 = 1;
                                                _adder.Add(1);
                                                _main_counter_task++;

                                                _stopWatch_thread_secondary_time.Stop();
                                                _stopWatch_thread_secondary_time.Reset();
                                                _stopWatch_thread_secondary_time.Start();

                                                goto _thread_loop;
                                                //break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    
                                }

                            }
                        }
                        _keyboard_frame_adjuster++;
                    }                 
                }
            }
            _some_received_messages_out = _some_received_messages_in;
            return _incoming_data;
        }



        //int _array_length = _incoming_data._someData.Length;
        //Console.WriteLine(_adder.Count);


        /*if (_incoming_data._someData[0].GetType() == typeof(SC_object_messenger_dispatcher.SC_message_object))
        {
            Console.WriteLine("keypress00");
        }*/

        /*if (_incoming_data._someData[0] is KeyboardState)
        {
            Console.WriteLine("keypress00");

            _assigned_keyboard = (KeyboardState)_incoming_data._someData[0];


            if (_assigned_keyboard != null)
            {
                if (_assigned_keyboard.PressedKeys.Contains(Key.T))
                {
                    Console.WriteLine("keypress");
                }
            }
        }*/



        /*if (Type.GetType(_main_object) == SC_object_messenger_dispatcher.SC_message_object)
        {
            _incoming_data._someData

        }*/

        /*
        if (_KeyboardState != null)
        {
            if (_consoleKeySwitch == 0)
            {
                if (_KeyboardState.PressedKeys.Contains(Key.Grave))
                {

                }
            }
        }*/






















        /*if (startupItems ==1)
        {
            //_xaudio2 = new XAudio2();
            //masteringVoice = new MasteringVoice(_xaudio2);

            //_xAudioDevice = _xaudio2;

            //xaudio2 = _xaudio2;
            startupItems = 0;
        }
        else
        {

        }*/

        /*mciSendString("open new Type waveaudio alias recsound", null, 0, IntPtr.Zero);
        var somedata = mciSendString("record recsound", null, 0, IntPtr.Zero);

        var folderpath = @"C:\Users\znyN3k0RnZ\Desktop\#RECSOUND\";

        var filename = "rec_" + _startRecCounter + ".wav";
        mciSendString("save recsound " + folderpath + filename, null, 0, IntPtr.Zero);
        mciSendString("close recsound", null, 0, IntPtr.Zero);

        var test = new DirectoryInfo(folderpath);
        test.Refresh();*/




        /*string fileName = Path.GetFileName(filename);
        string destinationPath = TransfersPath;

        string sourceFile = System.IO.Path.Combine(sourcePath);
        string destFile = System.IO.Path.Combine(destinationPath, fileName);
        System.IO.File.Move(sourceFile, destFile);*/

        //PLaySoundFile(xaudio2, "1) Playing a standard WAV file", path);



        /*MediaPlayer Sound1 = new MediaPlayer();
        Sound1.Open(new Uri(@"Sound/Sound1.wav"));
        Sound1.Play();

        MediaPlayer Sound2 = new MediaPlayer();
        Sound2.Open(new Uri(@"Sound/Sound2.wav"));
        Sound2.Play();*/

        //Player suodnsPlayer = new Player();


        //var path = @"C:\Users\steve\OneDrive\Desktop\#RECSOUND\" + "rec_" + _startRecCounter + ".wav";
        //mciSendString("save recsound " + path, null, 0, IntPtr.Zero);

        //mciSendString("close recsound", null, 0, IntPtr.Zero);

        /*if (_lastpath != "")
        {
            //var command = "play recsound";
            //mciSendString(command, null, 0, IntPtr.Zero);
            //mciSendString(string.Format("open \"{0}\" type waveaudio alias wave", _lastpath), null, 0, IntPtr.Zero);

            //var command = "play recsound";
            //mciSendString(command, null, 0, IntPtr.Zero);
            //string FORMAT = "open new Type waveaudio alias MediaSound";
            //string command = String.Format(FORMAT, _lastpath);
            //mciSendString(command, null, 0, IntPtr.Zero);
            //mciSendString(string.Format("open \"{0}\" type waveaudio alias wave", _lastpath), null, 0, IntPtr.Zero);

            var command = "play recsound";

            if (Repeat == 1)
            {
                command += " REPEAT";
            }
            mciSendString(command, null, 0, IntPtr.Zero);

            //MessageBox((IntPtr)0, "test completed", "My Message Box", 0);
            //mciSendString("close recsound", null, 0, IntPtr.Zero);
        }*/


        //"open \"{0}\" type waveaudio alias wave"

        //MessageBox((IntPtr)0, "test completed", "My Message Box", 0);


        /*mciSendString("open new Type waveaudio alias recsound", null,0,IntPtr.Zero);

        mciSendString("record recsound",null,0,IntPtr.Zero);

        mciSendString("save recsound " + @"C:\Users\steve\OneDrive\Desktop\#RECSOUND\" + "rec_" + _startRecCounter + ".wav",null,0,IntPtr.Zero);

        mciSendString("close recsound", null, 0, IntPtr.Zero);*/

        //_lastpath = path;
        /*_startRecCounter++;

        if (_startRecCounter >= 10)
        {
            _startRecCounter = 0;
        }
        try
        {
            _data00_IN = (SC_object_messenger_dispatcher.SC_message_object)_main_object;
            _data00_IN._voRecMsg = "test complete";

            int _received_switch_in00 = _data00_IN._received_switch_in;   //1
            int _received_switch_out00 = _data00_IN._received_switch_out; //0
            int _sending_switch_in00 = _data00_IN._sending_switch_in;     //0
            int _sending_switch_out00 = _data00_IN._sending_switch_out;   //0
            List<int[]> _chain_Of_Tasks00 = _data00_IN._chain_Of_Tasks0;
            int _timeOut00 = _data00_IN._timeOut0;
            int _ParentTaskThreadID00 = _data00_IN._ParentTaskThreadID0;
            int _main_cpu_count00 = _data00_IN._main_cpu_count;
            string _passTest00 = _data00_IN._passTest;
            int _welcomePackage00 = _data00_IN._welcomePackage;
            int _current_menu00 = _data00_IN._current_menu;
            int _last_current_menu00 = _data00_IN._last_current_menu;
            string _menuOption = _data00_IN._menuOption;
            object _someData = _xAudioDevice;// _data00_IN._someData;



            /*for (int i = 0; i < _main_object.Length; i++)
            {
                int _received_switch_in00 = _data00_IN[i]._received_switch_in;   //1
                int _received_switch_out00 = _data00_IN[i]._received_switch_out; //0
                int _sending_switch_in00 = _data00_IN[i]._sending_switch_in;     //0
                int _sending_switch_out00 = _data00_IN[i]._sending_switch_out;   //0
                List<int[]> _chain_Of_Tasks00 = _data00_IN[i]._chain_Of_Tasks0;
                int _timeOut00 = _data00_IN[i]._timeOut0;
                int _ParentTaskThreadID00 = _data00_IN[i]._ParentTaskThreadID0;
                int _main_cpu_count00 = _data00_IN[i]._main_cpu_count;
                string _passTest00 = _data00_IN[i]._passTest;
                int _welcomePackage00 = _data00_IN[i]._welcomePackage;
                int _current_menu00 = _data00_IN[i]._current_menu;
                int _last_current_menu00 = _data00_IN[i]._last_current_menu;
                string _menuOption = _data00_IN[i]._menuOption;
            }
        }
        catch
        {

        }*/

        //string pathFile = @"C:\Users\steve\OneDrive\Desktop\#RECSOUND\" + "rec_" + _startRecCounter + ".mp3";



        static void PLaySoundFile(XAudio2 device, string text, string fileName)
        {
            //Console.WriteLine("{0} => {1} (Press esc to skip)", text, fileName);
            var stream = new SoundStream(File.OpenRead(fileName));
            var waveFormat = stream.Format;
            var buffer = new AudioBuffer
            {
                Stream = stream.ToDataStream(),
                AudioBytes = (int)stream.Length,
                Flags = BufferFlags.EndOfStream
            };
            stream.Close();

            var sourceVoice = new SourceVoice(device, waveFormat, true);
            // Adds a sample callback to check that they are working on source voices
            sourceVoice.BufferEnd += (context) => Console.WriteLine(" => event received: end of buffer");
            sourceVoice.SubmitSourceBuffer(buffer, stream.DecodedPacketsInfo);
            sourceVoice.Start();

            int count = 0;
            while (sourceVoice.State.BuffersQueued > 0) //&& !IsKeyPressed(ConsoleKey.Escape)
            {
                if (count == 50)
                {
                    //Console.Write(".");
                    Console.Out.Flush();
                    count = 0;
                }
                Thread.Sleep(10);
                count++;
            }
            //Console.WriteLine();

            sourceVoice.DestroyVoice();
            sourceVoice.Dispose();
            buffer.Stream.Dispose();
        }


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




        //how in the fuck is this an mp3player? i found this bs on stackoverflow. about to try it. fucking stupid shit of stackoverflow.
        public static void Mp3Player(string filename)
        {
            const string FORMAT = @"open ""{0}"" type mpegvideo alias MediaFile";
            string command = String.Format(FORMAT, filename);
            mciSendString(command, null, 0, IntPtr.Zero);
        }

        static int Repeat = 1;

        public static void Play()
        {

            string command = "play MediaFile";
            if (Repeat == 1)
            {
                command += " REPEAT";
            }
            mciSendString(command, null, 0, IntPtr.Zero);
        }

        public static void Stop()
        {

            string command = "stop MediaFile";
            mciSendString(command, null, 0, IntPtr.Zero);
        }

        public static void Dispose()
        {
            string command = "close MediaFile";
            mciSendString(command, null, 0, IntPtr.Zero);
        }










        /*public  void NotifyItemChanged(string fullPath)
        {
            NativeMethods.SHChangeNotify(
                SHChangeNotifyEvents.UpdateItem,
                SHChangeNotifyFlags.PathW | SHChangeNotifyFlags.NotifyRecursive,
                fullPath,
                null);
        }

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public  extern void SHChangeNotify(SHChangeNotifyEvents eventID, SHChangeNotifyFlags flags, string path, string path2);

        [Flags]
        public enum SHChangeNotifyEvents : uint
        {
            RenameItem = 0x00000001,
            Create = 0x00000002,
            Delete = 0x00000004,
            MkDir = 0x00000008,
            RmDir = 0x00000010,
            MediaInserted = 0x00000020,
            MediaRemoved = 0x00000040,
            DriveRemoved = 0x00000080,
            DriveAdd = 0x00000100,
            NetShare = 0x00000200,
            NetUnshare = 0x00000400,
            Attributes = 0x00000800,
            UpdateDir = 0x00001000,
            UpdateItem = 0x00002000,
            ServerDisconnect = 0x00004000,
            UpdateImage = 0x00008000,
            DriveAddGui = 0x00010000,
            RenameFolder = 0x00020000,
            FreeSpace = 0x00040000,
            ExtendedEvent = 0x04000000,
            AssocChanged = 0x08000000,
            DiskEvents = 0x0002381F,
            GlobalEvents = 0x0C0581E0,
            AllEvents = 0x7FFFFFFF,
            Interrupt = 0x80000000
        }

        public enum SHChangeNotifyFlags : uint
        {
            IdList = 0x0000,
            PathA = 0x0001,
            PrinterA = 0x0002,
            Dword = 0x0003,
            PathW = 0x0005,
            PrinterW = 0x0006,
            Type = 0x00FF,
            Flush = 0x1000,
            FlushNoWait = 0x3000,
            NotifyRecursive = 0x10000
        }*/



        /*public class Recorder
        {
            WaveIn sourceStream;
            WaveFileWriter waveWriter;
            readonly String FilePath;
            readonly String FileName;
            readonly int InputDeviceIndex;

            public Recorder(int inputDeviceIndex, String filePath, String fileName)
            {
                InitializeComponent();
                this.InputDeviceIndex = inputDeviceIndex;
                this.FileName = fileName;
                this.FilePath = filePath;
            }

            public void StartRecording(object sender, EventArgs e)
            {
                sourceStream = new WaveIn
                {
                    DeviceNumber = this.InputDeviceIndex,
                    WaveFormat =
                        new WaveFormat(44100, WaveIn.GetCapabilities(this.InputDeviceIndex).Channels)
                };

                sourceStream.DataAvailable += this.SourceStreamDataAvailable;

                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }

                waveWriter = new WaveFileWriter(FilePath + FileName, sourceStream.WaveFormat);
                sourceStream.StartRecording();
            }

            public void SourceStreamDataAvailable(object sender, WaveInEventArgs e)
            {
                if (waveWriter == null) return;
                waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
                waveWriter.Flush();
            }

            private void RecordEnd(object sender, EventArgs e)
            {
                if (sourceStream != null)
                {
                    sourceStream.StopRecording();
                    sourceStream.Dispose();
                    sourceStream = null;
                }
                if (this.waveWriter == null)
                {
                    return;
                }
                this.waveWriter.Dispose();
                this.waveWriter = null;
                recordEndButton.Enabled = false;
                Application.Exit();
                Environment.Exit(0);
            }
        }*/

    }
}
