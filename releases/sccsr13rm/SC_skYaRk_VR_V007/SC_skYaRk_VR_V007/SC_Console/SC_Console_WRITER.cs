using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Runtime.InteropServices;


namespace SC_skYaRk_VR_V007
{
    public class SC_Console_WRITER
    {

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);
        /*: SC_IComponent
        public static SC_IComponent _SC_ICOMPONENT;
        SC_Globals SC_IComponent._SC_Globals
        {
            get => _SC_GLOB;
        }
        public static SC_Globals _SC_GLOB;*/
        //public SC_Console_CORE _CONSOLE_CORE;

        /*public SC_SkYaRk_VR_Editionv002.SC_Console_CORE _SC_CONSOLE_CORE { get; set; }

        public SC_SkYaRk_VR_Editionv002.SC_Console_WRITER _SC_CONSOLE_WRITER { get; set; }

        public SC_SkYaRk_VR_Editionv002.SC_Console_READER _SC_CONSOLE_READER { get; set; }
        public SC_SkYaRk_VR_Editionv002.SC_Systems _SC_SYSTEMS { get; set; }
        public SC_SkYaRk_VR_Editionv002.Program _PROGRAM { get; set; }*/

        /*public ITransform transform { get; private set; }
        IComponent ITransform.Component
        {
            get => component;
        }
        IComponent component;
        */

        /*public Program _PROGRAM { get; set; }
        public IComponent _icomponent { get; private set; }

        public SC_Console_CORE _SC_CONSOLE_CORE { get; set; }
        public SC_Console_READER _SC_CONSOLE_READER { get; set; }
        public SC_Console_WRITER _SC_CONSOLE_WRITER { get; set; }
        */

        /*public IComponent _SC_Glob { get; private set; }
        IComponent.SC_Globals ITransform.Component
        {
            get => component;
        }
        IComponent.SC_Globals component;*/

        /*IComponent _SC_Globals
        {
            get => _SC_Globals;
        }
        IComponent.SC_Globals _SC_Globals;*/

        /*public ITransform transform { get; private set; }
        IComponent ITransform.Component
        {
            get => component;
        }
        IComponent component;

        IRigidbody IComponent.rigidbody { get; set; }
        //SoftBody IComponent.softbody { get; set; }
        */

        /*public SC_Globals data { get; private set; }
        SC_Globals Component
        {
            get => component;
        }
        SC_Globals component;*/

        /*public SC_SkYaRk_VR_Editionv002.Program _PROGRAM { get; set; }

        public SC_SkYaRk_VR_Editionv002.SC_Console_CORE _SC_Console_CORE { get; set; }

        public SC_SkYaRk_VR_Editionv002.SC_Console_WRITER _SC_Console_WRITER { get; set; }

        public SC_SkYaRk_VR_Editionv002.SC_Console_READER _SC_Console_READER { get; set; }

        public SC_Systems _SC_SYSTEMS { get; set; }
        */
        static _messager[] _message_to_pass = new _messager[16];
        public static List<_messager> _message_to_pass_list = new List<_messager>();

        _messager _dummyMessage = new _messager();

        FastNoise _fastNoise;

        int _console_width = 0;
        int _console_height = 0;

        static int[] _map_array_last;
        static int[] _map_array;// = new int[];
        static int[] _map_array_dirty;// = new int[];

        int seed = 3420; // 3420
        float staticPlaneSize = 1;
        private int _detailScale = 10; // 10
        private int _HeightScale = 200; //200
        float noiseXZ = 20;

        int _original_width = 0;
        int _original_height = 0;

        string _program_name = "skYaRk";
        public static SC_Console_WRITER _CONSOLE_WRITER;
        public List<object[]> _TASK_00_WR_QUEUE = new List<object[]>();
        public object _ResultsOfTasks0;
        public int _Task00_init_console = 1;
        public Task _TASK_00_WR;
        public int _console_is_alive_00_WR = 0;
        public int _console_ERROR = -1;
        public int _console_hasINIT = 0;
        public int _xCurrentCursorPos;
        public int _yCurrentCursorPos;

        string _lastConsoleMessage = "";

        int mainMessageCursorPosSwitchCounter = 0;

        int main_switch_counter = 0;

        int _xLastCursorPos = 0;
        int _yLastCursorPos = 0;


        //string _task_working_00 = @"\";
        //string _task_working_01 = @"/";
        //string _task_working_02 = @"_";

        string _task_working_00 = @"";
        string _task_working_01 = @"";
        string _task_working_02 = @"";

        int tempX = 0;
        int tempXX = 0;
        int tempY = 0;
        int tempYY = 0;


        int totalX = 0;
        int xx = 0;
        int xxi = 0;
        int maxX = 0;
        int thresholdX = 0;
        int totalY = 0;
        int yy = 0;
        int yyi = 0;
        int maxY = 0;
        int thresholdY = 0;

        int _lastFrameWidth = 0;
        int _lastFrameHeight = 0;
        int _switchForChangeByteArray = 0;

        int currentWidthLast = 0;
        int currentHeightLast = 0;

        int cursorMoveSwitch = 0;

        int _counter_for_console = 0;
        int _switch_for_console = 0;





        public SC_Console_WRITER(SC_object_messenger_dispatcher.SC_message_object[] tester)// : base(tester)
        {
            _console_width = Console.WindowWidth;
            _console_height = Console.WindowHeight;
            _original_width = _console_width;
            _original_height = _console_height;

            _initX = (_console_width / 2) - (_program_name.Length / 2);
            _initY = (_console_height / 2);

            _map_array = new int[_console_width * _console_height];
            _map_array_last = new int[_console_width * _console_height];
            _map_array_dirty = new int[_console_width * _console_height];

            //_fastNoise = new FastNoise();

            for (int x = 0; x < _console_width; x++)
            {
                for (int y = 0; y < _console_height; y++)
                {
                    if (x == 0 && y > 0 && y < _original_height - 1)
                    {
                        try
                        {
                            Draw(x, y, "│");
                            _map_array[y * _console_width + x] = 2;
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                    else if (y == 0 && x > 0 && x < _original_width - 1)
                    {
                        try
                        {
                            Draw(x, y, "─");
                            _map_array[y * _console_width + x] = 2;
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                    else if (x == _original_width - 1 && y > 0 && y < _original_height - 1)
                    {
                        try
                        {
                            Draw(x, y, "│");
                            _map_array[y * _console_width + x] = 2;
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                    else if (y == _original_height - 1 && x > 0 && x < _original_width - 1)
                    {
                        try
                        {
                            Draw(x, y, "─");
                            _map_array[y * _console_width + x] = 2;
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                    else if (y == 0 && x == 0)
                    {
                        try
                        {
                            Draw(x, y, "┌");
                            _map_array[y * _console_width + x] = 2;
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                    else if (y == _original_height - 1 && x == 0)
                    {
                        try
                        {
                            Draw(x, y, "└");
                            _map_array[y * _console_width + x] = 2;
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                    else if (y == 0 && x == _original_width - 1)
                    {
                        try
                        {
                            Draw(x, y, "┐");
                            _map_array[y * _console_width + x] = 2;
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                    else if (y == _original_height - 1 && x == _original_width - 1)
                    {
                        try
                        {
                            Draw(x, y, "┘");
                            _map_array[y * _console_width + x] = 2;
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                    else
                    {
                        try
                        {
                            Draw(x, y, " ");
                            _map_array[y * _console_width + x] = 0;
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                }
            }

            for (int i = 0; i < _map_array.Length; i++)
            {
                _map_array_last[i] = _map_array[i];
                _map_array_dirty[i] = _map_array[i];
            }

            currentWidthLast = _console_width;
            currentHeightLast = _console_height;

        }



        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
        string fileName,
        [MarshalAs(UnmanagedType.U4)] uint fileAccess,
        [MarshalAs(UnmanagedType.U4)] uint fileShare,
        IntPtr securityAttributes,
        [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
        [MarshalAs(UnmanagedType.U4)] int flags,
        IntPtr template);

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            public short X;
            public short Y;

            public Coord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutputCharacter(
            SafeFileHandle hConsoleOutput,
            string lpCharacter,
            int nLength,
            Coord dwWriteCoord,
            ref int lpumberOfCharsWritten);

        public void Draw(int x, int y, string renderingChar)
        {
            // The handle to the output buffer of the console
            SafeFileHandle consoleHandle = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);

            // Draw with this native method because this method does NOT move the cursor.
            int n = 0;
            WriteConsoleOutputCharacter(consoleHandle, renderingChar, 1, new Coord((short)x, (short)y), ref n);
        }

        //CHAR_INFO chiBuffer[160];

        int _initX = 0;
        int _initY = 0;

        public _messager[] _console_writer(_messager[] _main_object)  //object[]
        {
            int currentWidth = Console.WindowWidth;
            int currentHeight = Console.WindowHeight;

            _xCurrentCursorPos = Console.CursorLeft;
            _yCurrentCursorPos = Console.CursorTop;

            mainMessageCursorPosSwitchCounter = 0;

            for (int i = 0; i < _main_object.Length; i++)
            {
                //_message_to_pass = new _messager[16];
                _message_to_pass[i] = (_messager)_main_object[i];
                //System.Windows.MessageBox.Show("_message_to_pass[i] " + _message_to_pass[i]._count, "Console");

                if (_message_to_pass[i]._specialMessage == 0)
                {
                    if (_message_to_pass[i]._swtch0 == 1)
                    {
                        //System.Windows.MessageBox.Show(_message_to_pass[i]._swtch + "", "Console");
                        if (_message_to_pass[i]._count >= _message_to_pass[i]._delay)
                        {
                            //System.Windows.MessageBox.Show("TEST1", "Console");
                            if (_message_to_pass[i]._messageCut != "")
                            {
                                //System.Windows.MessageBox.Show("TEST2", "Console");
                                string _char = _message_to_pass[i]._messageCut.Substring(0, 1);

                                //string _current_msg = _message_to_pass[i]._messageCut.Substring(0, _message_to_pass[i]._messageCut.Length - 1);
                                string _cut_msg = _message_to_pass[i]._messageCut.Substring(1, _message_to_pass[i]._messageCut.Length - 1);
                                _message_to_pass[i]._messageCut = _cut_msg;
                                _message_to_pass[i]._message = _char;

                                int _targetLineX = (int)_message_to_pass[i]._lineX;
                                int _targetLineY = (int)_message_to_pass[i]._lineY;

                                Draw(_targetLineX, _targetLineY, _char);
                                _map_array[_targetLineY * _console_width + _targetLineX] = 2;
                                _map_array_dirty[_targetLineY * _console_width + _targetLineX] = _message_to_pass[i]._delay * 10;

                                _lastConsoleMessage = _char.ToString();

                                _message_to_pass[i]._count = 0;

                                _targetLineX = (int)_message_to_pass[i]._lineX + 1;
                                _targetLineY = (int)_message_to_pass[i]._lineY;
                                _message_to_pass[i]._lineX = _targetLineX;
                                _message_to_pass[i]._lineY = _targetLineY;
                            }
                            else
                            {
                                //_message_to_pass[i] = new _messager();
                                _message_to_pass[i]._message = "";
                                _message_to_pass[i]._originalMsg = "";
                                _message_to_pass[i]._messageCut = "";
                                _message_to_pass[i]._specialMessage = -1;
                                _message_to_pass[i]._specialMessageLineX = 0;
                                _message_to_pass[i]._specialMessageLineY = 0;
                                _message_to_pass[i]._lineX = 0;
                                _message_to_pass[i]._lineY = 0;
                                _message_to_pass[i]._count = 0;
                                _message_to_pass[i]._swtch0 = 0;
                            }
                        }
                        _message_to_pass[i]._count = _message_to_pass[i]._count + 1;
                        //System.Windows.MessageBox.Show(_message_to_pass[i]._count + "", "Console");
                        mainMessageCursorPosSwitchCounter++;
                    }
                    else
                    {
                        mainMessageCursorPosSwitchCounter--;
                    }
                }
                else if (_message_to_pass[i]._specialMessage == 2)
                {
                    if (_message_to_pass[i]._swtch0 == 1)
                    {
                        _message_to_pass[i]._count = _message_to_pass[i]._delay;
                        _message_to_pass[i]._swtch0 = 2;
                    }
                    else if (_message_to_pass[i]._swtch0 == 2)
                    {
                        if (_message_to_pass[i]._messageCut != "")
                        {
                            if (_message_to_pass[i]._count <= 0)
                            {
                                string _char = _message_to_pass[i]._messageCut.Substring(0, 1);

                                string _cut_msg = _message_to_pass[i]._messageCut.Substring(1, _message_to_pass[i]._messageCut.Length - 1);
                                _message_to_pass[i]._messageCut = _cut_msg;
                                _message_to_pass[i]._message = _char;

                                int _targetLineX = (int)_message_to_pass[i]._lineX;
                                int _targetLineY = (int)_message_to_pass[i]._lineY;

                                Draw(_targetLineX, _targetLineY, _char);
                                _map_array[_targetLineY * _console_width + _targetLineX] = 1;
                                _map_array_dirty[_targetLineY * _console_width + _targetLineX] = _message_to_pass[i]._delay * 10;

                                _lastConsoleMessage = _char.ToString();

                                _message_to_pass[i]._count = 0;

                                _targetLineX = (int)_message_to_pass[i]._lineX + 1;
                                _targetLineY = (int)_message_to_pass[i]._lineY;
                                _message_to_pass[i]._lineX = _targetLineX;
                                _message_to_pass[i]._lineY = _targetLineY;
                                _message_to_pass[i]._count = _message_to_pass[i]._delay;
                            }
                            else
                            {
                                _message_to_pass[i]._count--;
                            }
                        }
                        else
                        {
                            if (_message_to_pass[i]._swtch1 == 1)
                            {
                                _message_to_pass[i]._count = _message_to_pass[i]._delay * 15;

                                _message_to_pass[i]._swtch1 = 2;
                            }
                            else if (_message_to_pass[i]._swtch1 == 2)
                            {
                                if (_message_to_pass[i]._count <= 0)
                                {
                                    if (_message_to_pass[i]._looping == 1)
                                    {
                                        _message_to_pass[i]._message = _message_to_pass[i]._originalMsg;
                                        _message_to_pass[i]._originalMsg = _message_to_pass[i]._originalMsg;
                                        _message_to_pass[i]._messageCut = _message_to_pass[i]._originalMsg;
                                        _message_to_pass[i]._specialMessage = 2;
                                        _message_to_pass[i]._specialMessageLineX = 0;
                                        _message_to_pass[i]._specialMessageLineY = 0;
                                        _message_to_pass[i]._lineX = _message_to_pass[i]._orilineX;
                                        _message_to_pass[i]._lineY = _message_to_pass[i]._orilineY;
                                        _message_to_pass[i]._count = 0;
                                        _message_to_pass[i]._swtch0 = 1;
                                        _message_to_pass[i]._swtch1 = 1;
                                    }
                                    else
                                    {
                                        _message_to_pass[i]._message = _message_to_pass[i]._originalMsg;
                                        _message_to_pass[i]._originalMsg = _message_to_pass[i]._originalMsg;
                                        _message_to_pass[i]._messageCut = _message_to_pass[i]._originalMsg;
                                        _message_to_pass[i]._specialMessage = -1;
                                        _message_to_pass[i]._specialMessageLineX = 0;
                                        _message_to_pass[i]._specialMessageLineY = 0;
                                        _message_to_pass[i]._lineX = 0;
                                        _message_to_pass[i]._lineY = 0;
                                        _message_to_pass[i]._count = 0;
                                        _message_to_pass[i]._swtch0 = 0;
                                        _message_to_pass[i]._swtch1 = 0;
                                    }
                                }
                                else
                                {
                                    _message_to_pass[i]._count--;
                                }
                            }
                        }
                    }
                }


                if (_message_to_pass[i]._messager_list != null)
                {

                    for (int c = 0; c < _message_to_pass[i]._messager_list.Length; c++)
                    {
                        if (_message_to_pass[i]._messager_list[c]._specialMessage == 0)
                        {
                            if (_message_to_pass[i]._messager_list[c]._swtch0 == 1)
                            {
                                //System.Windows.MessageBox.Show(_message_to_pass[i]._messager_list[c]._swtch + "", "Console");
                                if (_message_to_pass[i]._messager_list[c]._count >= _message_to_pass[i]._messager_list[c]._delay)
                                {
                                    //System.Windows.MessageBox.Show("TEST1", "Console");
                                    if (_message_to_pass[i]._messager_list[c]._messageCut != "")
                                    {
                                        //System.Windows.MessageBox.Show("TEST2", "Console");
                                        string _char = _message_to_pass[i]._messager_list[c]._messageCut.Substring(0, 1);

                                        //string _current_msg = _message_to_pass[i]._messager_list[c]._messageCut.Substring(0, _message_to_pass[i]._messager_list[c]._messageCut.Length - 1);
                                        string _cut_msg = _message_to_pass[i]._messager_list[c]._messageCut.Substring(1, _message_to_pass[i]._messager_list[c]._messageCut.Length - 1);
                                        _message_to_pass[i]._messager_list[c]._messageCut = _cut_msg;
                                        _message_to_pass[i]._messager_list[c]._message = _char;

                                        int _targetLineX = (int)_message_to_pass[i]._messager_list[c]._lineX;
                                        int _targetLineY = (int)_message_to_pass[i]._messager_list[c]._lineY;

                                        Draw(_targetLineX, _targetLineY, _char);
                                        _map_array[_targetLineY * _console_width + _targetLineX] = 2;
                                        _map_array_dirty[_targetLineY * _console_width + _targetLineX] = _message_to_pass[i]._messager_list[c]._delay * 10;

                                        _lastConsoleMessage = _char.ToString();

                                        _message_to_pass[i]._messager_list[c]._count = 0;

                                        _targetLineX = (int)_message_to_pass[i]._messager_list[c]._lineX + 1;
                                        _targetLineY = (int)_message_to_pass[i]._messager_list[c]._lineY;
                                        _message_to_pass[i]._messager_list[c]._lineX = _targetLineX;
                                        _message_to_pass[i]._messager_list[c]._lineY = _targetLineY;
                                    }
                                    else
                                    {
                                        //_message_to_pass[i]._messager_list[c] = new _messager();
                                        _message_to_pass[i]._messager_list[c]._message = "";
                                        _message_to_pass[i]._messager_list[c]._originalMsg = "";
                                        _message_to_pass[i]._messager_list[c]._messageCut = "";
                                        _message_to_pass[i]._messager_list[c]._specialMessage = -1;
                                        _message_to_pass[i]._messager_list[c]._specialMessageLineX = 0;
                                        _message_to_pass[i]._messager_list[c]._specialMessageLineY = 0;
                                        _message_to_pass[i]._messager_list[c]._lineX = 0;
                                        _message_to_pass[i]._messager_list[c]._lineY = 0;
                                        _message_to_pass[i]._messager_list[c]._count = 0;
                                        _message_to_pass[i]._messager_list[c]._swtch0 = 0;
                                    }
                                }
                                _message_to_pass[i]._messager_list[c]._count = _message_to_pass[i]._messager_list[c]._count + 1;
                                //System.Windows.MessageBox.Show(_message_to_pass[i]._messager_list[c]._count + "", "Console");
                                mainMessageCursorPosSwitchCounter++;
                            }
                            else
                            {
                                mainMessageCursorPosSwitchCounter--;
                            }
                        }
                        else if (_message_to_pass[i]._messager_list[c]._specialMessage == 2)
                        {
                            if (_message_to_pass[i]._messager_list[c]._swtch0 == 1)
                            {
                                _message_to_pass[i]._messager_list[c]._count = _message_to_pass[i]._messager_list[c]._delay;
                                _message_to_pass[i]._messager_list[c]._swtch0 = 2;
                            }
                            else if (_message_to_pass[i]._messager_list[c]._swtch0 == 2)
                            {
                                if (_message_to_pass[i]._messager_list[c]._messageCut != "")
                                {
                                    if (_message_to_pass[i]._messager_list[c]._count <= 0)
                                    {
                                        string _char = _message_to_pass[i]._messager_list[c]._messageCut.Substring(0, 1);

                                        string _cut_msg = _message_to_pass[i]._messager_list[c]._messageCut.Substring(1, _message_to_pass[i]._messager_list[c]._messageCut.Length - 1);
                                        _message_to_pass[i]._messager_list[c]._messageCut = _cut_msg;
                                        _message_to_pass[i]._messager_list[c]._message = _char;

                                        int _targetLineX = (int)_message_to_pass[i]._messager_list[c]._lineX;
                                        int _targetLineY = (int)_message_to_pass[i]._messager_list[c]._lineY;

                                        Draw(_targetLineX, _targetLineY, _char);
                                        _map_array[_targetLineY * _console_width + _targetLineX] = 1;
                                        _map_array_dirty[_targetLineY * _console_width + _targetLineX] = _message_to_pass[i]._messager_list[c]._delay * 10;

                                        _lastConsoleMessage = _char.ToString();

                                        _message_to_pass[i]._messager_list[c]._count = 0;

                                        _targetLineX = (int)_message_to_pass[i]._messager_list[c]._lineX + 1;
                                        _targetLineY = (int)_message_to_pass[i]._messager_list[c]._lineY;
                                        _message_to_pass[i]._messager_list[c]._lineX = _targetLineX;
                                        _message_to_pass[i]._messager_list[c]._lineY = _targetLineY;
                                        _message_to_pass[i]._messager_list[c]._count = _message_to_pass[i]._messager_list[c]._delay;
                                    }
                                    else
                                    {
                                        _message_to_pass[i]._messager_list[c]._count--;
                                    }
                                }
                                else
                                {
                                    if (_message_to_pass[i]._messager_list[c]._swtch1 == 1)
                                    {
                                        _message_to_pass[i]._messager_list[c]._count = _message_to_pass[i]._messager_list[c]._delay * 15;

                                        _message_to_pass[i]._messager_list[c]._swtch1 = 2;
                                    }
                                    else if (_message_to_pass[i]._messager_list[c]._swtch1 == 2)
                                    {
                                        if (_message_to_pass[i]._messager_list[c]._count <= 0)
                                        {
                                            if (_message_to_pass[i]._messager_list[c]._looping == 1)
                                            {
                                                _message_to_pass[i]._messager_list[c]._message = _message_to_pass[i]._messager_list[c]._originalMsg;
                                                _message_to_pass[i]._messager_list[c]._originalMsg = _message_to_pass[i]._messager_list[c]._originalMsg;
                                                _message_to_pass[i]._messager_list[c]._messageCut = _message_to_pass[i]._messager_list[c]._originalMsg;
                                                _message_to_pass[i]._messager_list[c]._specialMessage = 2;
                                                _message_to_pass[i]._messager_list[c]._specialMessageLineX = 0;
                                                _message_to_pass[i]._messager_list[c]._specialMessageLineY = 0;
                                                _message_to_pass[i]._messager_list[c]._lineX = _message_to_pass[i]._messager_list[c]._orilineX;
                                                _message_to_pass[i]._messager_list[c]._lineY = _message_to_pass[i]._messager_list[c]._orilineY;
                                                _message_to_pass[i]._messager_list[c]._count = 0;
                                                _message_to_pass[i]._messager_list[c]._swtch0 = 1;
                                                _message_to_pass[i]._messager_list[c]._swtch1 = 1;
                                            }
                                            else
                                            {
                                                _message_to_pass[i]._messager_list[c]._message = _message_to_pass[i]._messager_list[c]._originalMsg;
                                                _message_to_pass[i]._messager_list[c]._originalMsg = _message_to_pass[i]._messager_list[c]._originalMsg;
                                                _message_to_pass[i]._messager_list[c]._messageCut = _message_to_pass[i]._messager_list[c]._originalMsg;
                                                _message_to_pass[i]._messager_list[c]._specialMessage = -1;
                                                _message_to_pass[i]._messager_list[c]._specialMessageLineX = 0;
                                                _message_to_pass[i]._messager_list[c]._specialMessageLineY = 0;
                                                _message_to_pass[i]._messager_list[c]._lineX = 0;
                                                _message_to_pass[i]._messager_list[c]._lineY = 0;
                                                _message_to_pass[i]._messager_list[c]._count = 0;
                                                _message_to_pass[i]._messager_list[c]._swtch0 = 0;
                                                _message_to_pass[i]._messager_list[c]._swtch1 = 0;
                                            }
                                        }
                                        else
                                        {
                                            _message_to_pass[i]._messager_list[c]._count--;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }




            













            /*
            if (_message_to_pass[i]._messager_list. <= 0)
            {

                if (_message_to_pass[i]._specialMessage == 0)
                {
                    if (_message_to_pass[i]._swtch0 == 1)
                    {
                        //System.Windows.MessageBox.Show(_message_to_pass[i]._swtch + "", "Console");
                        if (_message_to_pass[i]._count >= _message_to_pass[i]._delay)
                        {
                            //System.Windows.MessageBox.Show("TEST1", "Console");
                            if (_message_to_pass[i]._messageCut != "")
                            {
                                //System.Windows.MessageBox.Show("TEST2", "Console");
                                string _char = _message_to_pass[i]._messageCut.Substring(0, 1);

                                //string _current_msg = _message_to_pass[i]._messageCut.Substring(0, _message_to_pass[i]._messageCut.Length - 1);
                                string _cut_msg = _message_to_pass[i]._messageCut.Substring(1, _message_to_pass[i]._messageCut.Length - 1);
                                _message_to_pass[i]._messageCut = _cut_msg;
                                _message_to_pass[i]._message = _char;

                                int _targetLineX = (int)_message_to_pass[i]._lineX;
                                int _targetLineY = (int)_message_to_pass[i]._lineY;

                                Draw(_targetLineX, _targetLineY, _char);
                                _map_array[_targetLineY * _console_width + _targetLineX] = 2;
                                _map_array_dirty[_targetLineY * _console_width + _targetLineX] = _message_to_pass[i]._delay * 10;

                                _lastConsoleMessage = _char.ToString();

                                _message_to_pass[i]._count = 0;

                                _targetLineX = (int)_message_to_pass[i]._lineX + 1;
                                _targetLineY = (int)_message_to_pass[i]._lineY;
                                _message_to_pass[i]._lineX = _targetLineX;
                                _message_to_pass[i]._lineY = _targetLineY;
                            }
                            else
                            {
                                //_message_to_pass[i] = new _messager();
                                _message_to_pass[i]._message = "";
                                _message_to_pass[i]._originalMsg = "";
                                _message_to_pass[i]._messageCut = "";
                                _message_to_pass[i]._specialMessage = -1;
                                _message_to_pass[i]._specialMessageLineX = 0;
                                _message_to_pass[i]._specialMessageLineY = 0;
                                _message_to_pass[i]._lineX = 0;
                                _message_to_pass[i]._lineY = 0;
                                _message_to_pass[i]._count = 0;
                                _message_to_pass[i]._swtch0 = 0;
                            }
                        }
                        _message_to_pass[i]._count = _message_to_pass[i]._count + 1;
                        //System.Windows.MessageBox.Show(_message_to_pass[i]._count + "", "Console");
                        mainMessageCursorPosSwitchCounter++;
                    }
                    else
                    {
                        mainMessageCursorPosSwitchCounter--;
                    }
                }
                else if (_message_to_pass[i]._specialMessage == 2)
                {
                    if (_message_to_pass[i]._swtch0 == 1)
                    {
                        _message_to_pass[i]._count = _message_to_pass[i]._delay;
                        _message_to_pass[i]._swtch0 = 2;
                    }
                    else if (_message_to_pass[i]._swtch0 == 2)
                    {
                        if (_message_to_pass[i]._messageCut != "")
                        {
                            if (_message_to_pass[i]._count <= 0)
                            {
                                string _char = _message_to_pass[i]._messageCut.Substring(0, 1);

                                string _cut_msg = _message_to_pass[i]._messageCut.Substring(1, _message_to_pass[i]._messageCut.Length - 1);
                                _message_to_pass[i]._messageCut = _cut_msg;
                                _message_to_pass[i]._message = _char;

                                int _targetLineX = (int)_message_to_pass[i]._lineX;
                                int _targetLineY = (int)_message_to_pass[i]._lineY;

                                Draw(_targetLineX, _targetLineY, _char);
                                _map_array[_targetLineY * _console_width + _targetLineX] = 1;
                                _map_array_dirty[_targetLineY * _console_width + _targetLineX] = _message_to_pass[i]._delay * 10;

                                _lastConsoleMessage = _char.ToString();

                                _message_to_pass[i]._count = 0;

                                _targetLineX = (int)_message_to_pass[i]._lineX + 1;
                                _targetLineY = (int)_message_to_pass[i]._lineY;
                                _message_to_pass[i]._lineX = _targetLineX;
                                _message_to_pass[i]._lineY = _targetLineY;
                                _message_to_pass[i]._count = _message_to_pass[i]._delay;
                            }
                            else
                            {
                                _message_to_pass[i]._count--;
                            }
                        }
                        else
                        {
                            if (_message_to_pass[i]._swtch1 == 1)
                            {
                                _message_to_pass[i]._count = _message_to_pass[i]._delay * 15;

                                _message_to_pass[i]._swtch1 = 2;
                            }
                            else if (_message_to_pass[i]._swtch1 == 2)
                            {
                                if (_message_to_pass[i]._count <= 0)
                                {
                                    if (_message_to_pass[i]._looping == 1)
                                    {
                                        _message_to_pass[i]._message = _message_to_pass[i]._originalMsg;
                                        _message_to_pass[i]._originalMsg = _message_to_pass[i]._originalMsg;
                                        _message_to_pass[i]._messageCut = _message_to_pass[i]._originalMsg;
                                        _message_to_pass[i]._specialMessage = 2;
                                        _message_to_pass[i]._specialMessageLineX = 0;
                                        _message_to_pass[i]._specialMessageLineY = 0;
                                        _message_to_pass[i]._lineX = _message_to_pass[i]._orilineX;
                                        _message_to_pass[i]._lineY = _message_to_pass[i]._orilineY;
                                        _message_to_pass[i]._count = 0;
                                        _message_to_pass[i]._swtch0 = 1;
                                        _message_to_pass[i]._swtch1 = 1;
                                    }
                                    else
                                    {
                                        _message_to_pass[i]._message = _message_to_pass[i]._originalMsg;
                                        _message_to_pass[i]._originalMsg = _message_to_pass[i]._originalMsg;
                                        _message_to_pass[i]._messageCut = _message_to_pass[i]._originalMsg;
                                        _message_to_pass[i]._specialMessage = -1;
                                        _message_to_pass[i]._specialMessageLineX = 0;
                                        _message_to_pass[i]._specialMessageLineY = 0;
                                        _message_to_pass[i]._lineX = 0;
                                        _message_to_pass[i]._lineY = 0;
                                        _message_to_pass[i]._count = 0;
                                        _message_to_pass[i]._swtch0 = 0;
                                        _message_to_pass[i]._swtch1 = 0;
                                    }
                                }
                                else
                                {
                                    _message_to_pass[i]._count--;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                /*for (int c = 0;c < _message_to_pass[i]._messager_list.Count;c++)
                {
                    if (_message_to_pass[i]._messager_list[c]._specialMessage == 0)
                    {
                        if (_message_to_pass[i]._messager_list[c]._swtch0 == 1)
                        {
                            //System.Windows.MessageBox.Show(_message_to_pass[i]._messager_list[c]._swtch + "", "Console");
                            if (_message_to_pass[i]._messager_list[c]._count >= _message_to_pass[i]._messager_list[c]._delay)
                            {
                                //System.Windows.MessageBox.Show("TEST1", "Console");
                                if (_message_to_pass[i]._messager_list[c]._messageCut != "")
                                {
                                    //System.Windows.MessageBox.Show("TEST2", "Console");
                                    string _char = _message_to_pass[i]._messager_list[c]._messageCut.Substring(0, 1);

                                    //string _current_msg = _message_to_pass[i]._messager_list[c]._messageCut.Substring(0, _message_to_pass[i]._messager_list[c]._messageCut.Length - 1);
                                    string _cut_msg = _message_to_pass[i]._messager_list[c]._messageCut.Substring(1, _message_to_pass[i]._messager_list[c]._messageCut.Length - 1);
                                    _message_to_pass[i]._messager_list[c]._messageCut = _cut_msg;
                                    _message_to_pass[i]._messager_list[c]._message = _char;

                                    int _targetLineX = (int)_message_to_pass[i]._messager_list[c]._lineX;
                                    int _targetLineY = (int)_message_to_pass[i]._messager_list[c]._lineY;

                                    Draw(_targetLineX, _targetLineY, _char);
                                    _map_array[_targetLineY * _console_width + _targetLineX] = 2;
                                    _map_array_dirty[_targetLineY * _console_width + _targetLineX] = _message_to_pass[i]._messager_list[c]._delay * 10;

                                    _lastConsoleMessage = _char.ToString();

                                    _message_to_pass[i]._messager_list[c]._count = 0;

                                    _targetLineX = (int)_message_to_pass[i]._messager_list[c]._lineX + 1;
                                    _targetLineY = (int)_message_to_pass[i]._messager_list[c]._lineY;
                                    _message_to_pass[i]._messager_list[c]._lineX = _targetLineX;
                                    _message_to_pass[i]._messager_list[c]._lineY = _targetLineY;
                                }
                                else
                                {
                                    //_message_to_pass[i]._messager_list[c] = new _messager();
                                    _message_to_pass[i]._messager_list[c]._message = "";
                                    _message_to_pass[i]._messager_list[c]._originalMsg = "";
                                    _message_to_pass[i]._messager_list[c]._messageCut = "";
                                    _message_to_pass[i]._messager_list[c]._specialMessage = -1;
                                    _message_to_pass[i]._messager_list[c]._specialMessageLineX = 0;
                                    _message_to_pass[i]._messager_list[c]._specialMessageLineY = 0;
                                    _message_to_pass[i]._messager_list[c]._lineX = 0;
                                    _message_to_pass[i]._messager_list[c]._lineY = 0;
                                    _message_to_pass[i]._messager_list[c]._count = 0;
                                    _message_to_pass[i]._messager_list[c]._swtch0 = 0;
                                }
                            }
                            _message_to_pass[i]._messager_list[c]._count = _message_to_pass[i]._messager_list[c]._count + 1;
                            //System.Windows.MessageBox.Show(_message_to_pass[i]._messager_list[c]._count + "", "Console");
                            mainMessageCursorPosSwitchCounter++;
                        }
                        else
                        {
                            mainMessageCursorPosSwitchCounter--;
                        }
                    }
                    else if (_message_to_pass[i]._messager_list[c]._specialMessage == 2)
                    {
                        if (_message_to_pass[i]._messager_list[c]._swtch0 == 1)
                        {
                            _message_to_pass[i]._messager_list[c]._count = _message_to_pass[i]._messager_list[c]._delay;
                            _message_to_pass[i]._messager_list[c]._swtch0 = 2;
                        }
                        else if (_message_to_pass[i]._messager_list[c]._swtch0 == 2)
                        {
                            if (_message_to_pass[i]._messager_list[c]._messageCut != "")
                            {
                                if (_message_to_pass[i]._messager_list[c]._count <= 0)
                                {
                                    string _char = _message_to_pass[i]._messager_list[c]._messageCut.Substring(0, 1);

                                    string _cut_msg = _message_to_pass[i]._messager_list[c]._messageCut.Substring(1, _message_to_pass[i]._messager_list[c]._messageCut.Length - 1);
                                    _message_to_pass[i]._messager_list[c]._messageCut = _cut_msg;
                                    _message_to_pass[i]._messager_list[c]._message = _char;

                                    int _targetLineX = (int)_message_to_pass[i]._messager_list[c]._lineX;
                                    int _targetLineY = (int)_message_to_pass[i]._messager_list[c]._lineY;

                                    Draw(_targetLineX, _targetLineY, _char);
                                    _map_array[_targetLineY * _console_width + _targetLineX] = 1;
                                    _map_array_dirty[_targetLineY * _console_width + _targetLineX] = _message_to_pass[i]._messager_list[c]._delay * 10;

                                    _lastConsoleMessage = _char.ToString();

                                    _message_to_pass[i]._messager_list[c]._count = 0;

                                    _targetLineX = (int)_message_to_pass[i]._messager_list[c]._lineX + 1;
                                    _targetLineY = (int)_message_to_pass[i]._messager_list[c]._lineY;
                                    _message_to_pass[i]._messager_list[c]._lineX = _targetLineX;
                                    _message_to_pass[i]._messager_list[c]._lineY = _targetLineY;
                                    _message_to_pass[i]._messager_list[c]._count = _message_to_pass[i]._messager_list[c]._delay;
                                }
                                else
                                {
                                    _message_to_pass[i]._messager_list[c]._count--;
                                }
                            }
                            else
                            {
                                if (_message_to_pass[i]._messager_list[c]._swtch1 == 1)
                                {
                                    _message_to_pass[i]._messager_list[c]._count = _message_to_pass[i]._messager_list[c]._delay * 15;

                                    _message_to_pass[i]._messager_list[c]._swtch1 = 2;
                                }
                                else if (_message_to_pass[i]._messager_list[c]._swtch1 == 2)
                                {
                                    if (_message_to_pass[i]._messager_list[c]._count <= 0)
                                    {
                                        if (_message_to_pass[i]._messager_list[c]._looping == 1)
                                        {
                                            _message_to_pass[i]._messager_list[c]._message = _message_to_pass[i]._messager_list[c]._originalMsg;
                                            _message_to_pass[i]._messager_list[c]._originalMsg = _message_to_pass[i]._messager_list[c]._originalMsg;
                                            _message_to_pass[i]._messager_list[c]._messageCut = _message_to_pass[i]._messager_list[c]._originalMsg;
                                            _message_to_pass[i]._messager_list[c]._specialMessage = 2;
                                            _message_to_pass[i]._messager_list[c]._specialMessageLineX = 0;
                                            _message_to_pass[i]._messager_list[c]._specialMessageLineY = 0;
                                            _message_to_pass[i]._messager_list[c]._lineX = _message_to_pass[i]._messager_list[c]._orilineX;
                                            _message_to_pass[i]._messager_list[c]._lineY = _message_to_pass[i]._messager_list[c]._orilineY;
                                            _message_to_pass[i]._messager_list[c]._count = 0;
                                            _message_to_pass[i]._messager_list[c]._swtch0 = 1;
                                            _message_to_pass[i]._messager_list[c]._swtch1 = 1;
                                        }
                                        else
                                        {
                                            _message_to_pass[i]._messager_list[c]._message = _message_to_pass[i]._messager_list[c]._originalMsg;
                                            _message_to_pass[i]._messager_list[c]._originalMsg = _message_to_pass[i]._messager_list[c]._originalMsg;
                                            _message_to_pass[i]._messager_list[c]._messageCut = _message_to_pass[i]._messager_list[c]._originalMsg;
                                            _message_to_pass[i]._messager_list[c]._specialMessage = -1;
                                            _message_to_pass[i]._messager_list[c]._specialMessageLineX = 0;
                                            _message_to_pass[i]._messager_list[c]._specialMessageLineY = 0;
                                            _message_to_pass[i]._messager_list[c]._lineX = 0;
                                            _message_to_pass[i]._messager_list[c]._lineY = 0;
                                            _message_to_pass[i]._messager_list[c]._count = 0;
                                            _message_to_pass[i]._messager_list[c]._swtch0 = 0;
                                            _message_to_pass[i]._messager_list[c]._swtch1 = 0;
                                        }
                                    }
                                    else
                                    {
                                        _message_to_pass[i]._messager_list[c]._count--;
                                    }
                                }
                            }
                        }
                    }

                }
            }*/





            //lock (_message_to_pass_list)
            {
                if (_message_to_pass_list.Count > 0)
                {
                    _dummyMessage = _message_to_pass_list[0];
                    if (_dummyMessage._count >= _dummyMessage._delay)
                    {
                        if (_dummyMessage._messageCut != "")
                        {
                            string _char = _dummyMessage._messageCut.Substring(0, 1);
                            string _cut_msg = _dummyMessage._messageCut.Substring(1, _dummyMessage._messageCut.Length - 1);
                            _dummyMessage._messageCut = _cut_msg;

                            int _targetLineX = (int)_dummyMessage._lineX;
                            int _targetLineY = (int)_dummyMessage._lineY;
                            Draw(_targetLineX, _targetLineY, _char);

                            //MessageBox((IntPtr)0, _char + "", "Console", 0);

                            _dummyMessage._count = 0;
                            _targetLineX = (int)_dummyMessage._lineX + 1;
                            _targetLineY = (int)_dummyMessage._lineY;
                            _dummyMessage._lineX = _targetLineX;
                            _dummyMessage._lineY = _targetLineY;

                            _message_to_pass_list[0] = _dummyMessage;
                        }
                        else
                        {
                            _message_to_pass_list[0] = _dummyMessage;
                            _message_to_pass_list.Remove(_message_to_pass_list[0]);
                        }
                    }
                    else
                    {
                        _dummyMessage._count = _dummyMessage._count + 1;
                        _message_to_pass_list[0] = _dummyMessage;
                    }
                }
                else
                {
                    //Draw(1, 6, _message_to_pass_list.Count + "");
                }
            }

            for (int x = 0; x < _console_width; x++)
            {
                for (int y = 0; y < _console_height; y++)
                {
                    if (_map_array[y * _console_width + x] == 1)
                    {
                        if (_map_array_dirty[y * _console_width + x] != 0)
                        {
                            _map_array_dirty[y * _console_width + x]--;

                        }
                        else
                        {
                            Draw(x, y, " ");
                            _map_array[(y * _console_width) + x] = 0;
                            _map_array_dirty[(y * _console_width) + x] = 0;
                        }
                    }
                    else
                    {

                    }
                }
            }
            return _message_to_pass;
        }










        /*if (mainMessageCursorPosSwitch == 0)
        {
            if (mainMessageCursorPosSwitchCounter <= 0)
            {
                if (main_switch_counter >= 1000)
                {

                    _message_to_pass[2] = (_messager)_main_object[2];
                    _message_to_pass[2]._swtch = 1;

                    Console.SetCursorPosition(0, 3);

                    mainMessageCursorPosSwitch = 1;
                }
                mainMessageCursorPosSwitchCounter = 0;
                main_switch_counter = 0;
            }
            mainMessageCursorPosSwitch = 1;
        }
        else
        {
            //where is cursor

        }*/

        //main_switch_counter++;


        public struct _messager
        {
            public _messager[] _messager_list;
            public int _specialMessage;
            public int _specialMessageLineX;
            public int _specialMessageLineY;

            public string _message;
            public string _messageCut;
            public string _originalMsg;

            public int _lineX;
            public int _lineY;
            public int _orilineX;
            public int _orilineY;

            public int _delay;

            public int _swtch0;
            public int _swtch1;


            public int _count;

            public int _looping;
            //public int ;
        }










        //_TASK_00_WR = Task.Factory.StartNew(() =>
        //{
        /*_Console_writer_message_queue_title = new List<_console_writer_message_queue_title>();
        _msg_wr_title = new _console_writer_message_queue_title();

        _Console_writer_message_queue = new List<_console_writer_message_queue>();
        _msg_wr = new _console_writer_message_queue();

        _Console_writer_message_queue_title_delay = new List<_console_writer_message_queue_title_delay>();
        _msg_wr_title_delay = new _console_writer_message_queue_title_delay();
        */
        //System.Windows.MessageBox.Show("_IN_1000_Console_WRITER", "Console");

        /*_msg_wr._message = "_IN_1000 starting Console";
        _msg_wr._lineX = 0;
        _msg_wr._lineY = 11;
        _Console_writer_message_queue.Add(_msg_wr);*/

        /*while (true)
        {
                if (_Task00_init_console == 1)
                {
                    //System.Windows.MessageBox.Show("_IN_1000_Console_WRITER", "Console");
                    _ResultsOfTasks0 = null;
                    try
                    {
                        //_createConsole();
                        //check main console core
                    }
                    catch (Exception ex)
                    {
                        _console_ERROR = 1;

                        /*_msg_wr._message = ex.ToString();
                        _msg_wr._lineX = 0;
                        _msg_wr._lineY = 20;
                        _Console_writer_message_queue.Add(_msg_wr);
                        System.Windows.MessageBox.Show(ex.ToString(), "Console");
                    }

                    //System.Windows.MessageBox.Show("_IN_1000_Console_WRITER", "Console");
                    if (SC_Systems._SC_GLOB._SC_CONSOLE_CORE._console_hasINIT == 1)
                    {
                        _console_hasINIT = 1;
                        /*_msg_wr._message = "_IN_1000_Console_WRITER";
                        _msg_wr._lineX = 0;
                        _msg_wr._lineY = 11;
                        //lock (_Console_writer_message_queue)
                        //{
                            _Console_writer_message_queue.Add(_msg_wr);
                        //}
                        //System.Windows.MessageBox.Show("_IN_1000_Console_WRITER", "Console");

                        _Task00_init_console = 0;
                    }
                }*/

        /*if (_console_is_alive_00_WR > 10)
        {
            //System.Windows.MessageBox.Show(_Console_writer_message_queue_title_delay.Count + "", "Console");

            //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("task alive", 0, 1);
            _console_is_alive_00_WR = 0;
        }*/

        /*lock (_someCustomizedSpeedMessageQueue)
        {
            if (_someCustomizedSpeedMessageQueue.Count > 0)
            {
                object[] test = _someCustomizedSpeedMessageQueue[0];
                test[0] = _someCustomizedSpeedMessageQueue[0][0];
                test[1] = _someCustomizedSpeedMessageQueue[0][1];
                test[2] = _someCustomizedSpeedMessageQueue[0][2];

                //if (_lastConsoleMessage != (string)test[0])
                //{
                //    dispatchConsoleCommands(test);
                //}

                dispatchConsoleCommands(test);
                _someCustomizedSpeedMessageQueue.Remove(_someCustomizedSpeedMessageQueue[0]);
            }
        }*/

        /*if (_console_hasINIT == 1)
        {
            /*if (_Console_writer_message_queue.Count > 0)
            {
                string consoleMessage = (string)_Console_writer_message_queue[0]._message;
                int _targetLineX = (int)_Console_writer_message_queue[0]._lineX;
                int _targetLineY = (int)_Console_writer_message_queue[0]._lineY;

                _xCurrentCursorPos = Console.CursorLeft;
                _yCurrentCursorPos = Console.CursorTop;

                //Console.SetCursorPosition(0, _yCurrentCursorPos);
                //Console.SetCursorPosition(0, _targetLineY);
                Console.SetCursorPosition(_targetLineX, _targetLineY);

                Console.Write(consoleMessage);
                _lastConsoleMessage = consoleMessage;
                _Console_writer_message_queue.Remove(_Console_writer_message_queue[0]);

                /*if (_lastConsoleMessage != consoleMessage)
                {

                }
            }

            //lock (_Console_writer_message_queue_title_delay)
            {
                //System.Windows.MessageBox.Show("!NULL", "Console");
                /*if (_Console_writer_message_queue_title_delay.Count > 0)
                {
                    string consoleMessage = (string)_Console_writer_message_queue_title_delay[0]._message;
                    int _targetLineX = (int)_Console_writer_message_queue_title_delay[0]._lineX;
                    int _targetLineY = (int)_Console_writer_message_queue_title_delay[0]._lineY;

                    _xCurrentCursorPos = Console.CursorLeft;
                    _yCurrentCursorPos = Console.CursorTop;

                    //Console.SetCursorPosition(0, _yCurrentCursorPos);
                    //Console.SetCursorPosition(0, _targetLineY);
                    Console.SetCursorPosition(_targetLineX, _targetLineY);

                    if (Console.CursorLeft == _targetLineX && Console.CursorTop == _targetLineY)
                    {
                        Console.Write(consoleMessage);
                        _lastConsoleMessage = consoleMessage;
                        _Console_writer_message_queue_title_delay.Remove(_Console_writer_message_queue_title_delay[0]);
                    }



                    /*_dummyObject = _Console_writer_message_queue_title_delay[0];

                    int _targetLineX = (int)_dummyObject._lineX;
                    int _targetLineY = (int)_dummyObject._lineY;
                    Console.SetCursorPosition(_targetLineX, _targetLineY);
                    Console.Write(_dummyObject._message); // + "\r\n"
                    _Console_writer_message_queue_title_delay.Remove(_Console_writer_message_queue_title_delay[0]);
                    */

        //System.Windows.MessageBox.Show("!NULL", "Console");

        /*if (_dummyObject._swtch == 1)
        {
            if (_dummyObject._count >= _dummyObject._delay)
            {
                if (_dummyObject._messageCut != "")
                {
                    string _char = _dummyObject._messageCut.Substring(0, 1);

                    string _current_msg = _dummyObject._messageCut.Substring(0, _dummyObject._messageCut.Length - 1);
                    string _cut_msg = _dummyObject._messageCut.Substring(1, _dummyObject._messageCut.Length - 1);
                    _dummyObject._messageCut = _cut_msg;
                    _dummyObject._message = _char;

                    int _targetLineX = (int)_dummyObject._lineX;
                    int _targetLineY = (int)_dummyObject._lineY;

                    _xCurrentCursorPos = Console.CursorLeft;
                    _yCurrentCursorPos = Console.CursorTop;

                    Console.SetCursorPosition(_targetLineX, _targetLineY);
                    //Console.SetCursorPosition(0, _targetLineY);
                    //Console.SetCursorPosition(_targetLineX, _targetLineY);

                    Console.Write(_char);

                    _lastConsoleMessage = _char.ToString();

                    _dummyObject._count = 0;

                    _targetLineX = (int)_dummyObject._lineX + 1;
                    _targetLineY = (int)_dummyObject._lineY;
                    _dummyObject._lineX = _targetLineX;
                    _dummyObject._lineY = _targetLineY;


                    /*if ((int)_dummyObject._lineX + 1 < Console.LargestWindowWidth - 1)
                    {
                        _targetLineX = (int)_dummyObject._lineX + 1;
                        _targetLineY = (int)_dummyObject._lineY;

                        //System.Windows.MessageBox.Show(_dummyObject._message, "Console");
                        //System.Windows.MessageBox.Show(_dummyObject._message, "Console");

                        _dummyObject._count = 0;
                        _dummyObject._lineX = _targetLineX;
                        _dummyObject._lineY = _targetLineY;

                        _Console_writer_message_queue_title_delay[0] = _dummyObject;
                    }*/

        /*else
        {
            _dummyObject._lineX = _targetLineX;
            _dummyObject._lineY = _targetLineY;

            _dummyObject._message.Remove(0);

            _dummyObject._count = 0;
            _Console_writer_message_queue_title_delay[0] = _dummyObject;

        }
    }
    else
    {

        //_dummyObject = null;
        //_Console_writer_message_queue_title_delay[0] = _dummyObject;
        _Console_writer_message_queue_title_delay.Remove(_Console_writer_message_queue_title_delay[0]);
    }

    _dummyObject._count = 0;
}

_dummyObject._count++;

_Console_writer_message_queue_title_delay[0] = _dummyObject;
//System.Windows.MessageBox.Show(_Console_writer_message_queue_title_delay[0]._count + "", "Console");
}
else
{

}
        _Console_writer_message_queue_title_delay[0] = _dummyObject;

    }*/

        /*string consoleMessage = (string)_Console_writer_message_queue_title_delay[0]._message;
        int _targetLineX = (int)_Console_writer_message_queue_title_delay[0]._lineX;
        int _targetLineY = (int)_Console_writer_message_queue_title_delay[0]._lineY;

        _xCurrentCursorPos = Console.CursorLeft;
        _yCurrentCursorPos = Console.CursorTop;

        Console.SetCursorPosition(0, _yCurrentCursorPos);
        Console.SetCursorPosition(0, _targetLineY);
        Console.SetCursorPosition(_targetLineX, _targetLineY);

        Console.Write(consoleMessage);
        _lastConsoleMessage = consoleMessage;
        _Console_writer_message_queue_title_delay.Remove(_Console_writer_message_queue_title_delay[0]);

    }
    _console_is_alive_00_WR++;
}
//Thread.Sleep(1);
}*/
        //});

        //(_messager)_main_object








        /*public struct _console_writer_message_queue_title_delay
        {
            public int _specialMessage;
            public int _specialMessageLine;
            public string _message;
            public string _messageCut;
            public string _originalMsg;
            public int _lineX;
            public int _lineY;
            public int _delay;
            public int _swtch;
            public int _count;
        }


        public struct _console_writer_message_queue_title
        {
            public string _message;
            public int _lineX;
            public int _lineY;
        }

        public struct _console_writer_message_queue
        {
            public string _message;
            public int _lineX;
            public int _lineY;
        }

        public struct _console_writer_message_queue_delayed
        {
            public string _message;
            public int _lineX;
            public int _lineY;
            public int _delay;
        }*/
    }
}
/*////////////////
            //TO GET WIDTH//
            ////////////////
            totalX = 0;
            xx = 0;
            xxi = 0;
            maxX = currentWidth;
            thresholdX = _console_width;

            for (xx = 0; xx < maxX; xx++)
            {
                if (xxi >= (thresholdX - 1))
                {
                    xxi = 0; //reset
                    totalX++;
                }
                xxi += 1;
            }*/

/*////////////////
//TO GET HEIGHT/
////////////////
totalY = 0;
yy = 0;
yyi = 0;
maxY = currentWidth;
thresholdY = _console_height;

for (yy = 0; yy < maxY; yy++)
{
    if (yyi >= (thresholdY - 1))
    {
        yyi = 0; //reset
        totalY++;
    }
    yyi += 1;
}*/


/*for (int x = 0; x < _console_width; x++)
{
    for (int y = 0; y < _console_height; y++)
    {
        noiseXZ *= _fastNoise.GetNoise((((x * staticPlaneSize) + (0 * staticPlaneSize) + seed) / _detailScale) * _HeightScale, (((y * staticPlaneSize) + (0 * staticPlaneSize) + seed) / _detailScale) * _HeightScale);

        if (noiseXZ >= 0.1f)
        {
            _map_array[(y * _console_width) + x] = 1;
        }
        else
        {
            _map_array[(y * _console_width) + x] = 0;
        }                   
    }
}*/
