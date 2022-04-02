using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;
//using System.Windows.Threading;

using System.Runtime.InteropServices;
using System.Windows;
//using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

//using _console_writer_message_queue = SC_SkYaRk_VR_Editionv002.SC_Console_WRITER._console_writer_message_queue;
//using _CONSOLE_WRITER = SC_SkYaRk_VR_Editionv002.SC_Console_WRITER._CONSOLE_WRITER;

namespace SC_skYaRk_VR_V007
{
    public class SC_Console_CORE : SC_console_component //SC_Globals
    {

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);


        public static SC_console_component _SC_ICOMPONENT;
        SC_Globals SC_console_component._SC_Globals
        {
            get => _SC_GLOB;
        }
        public static SC_Globals _SC_GLOB;
        /*public SC_SkYaRk_VR_Editionv002.SC_Console_CORE _SC_CONSOLE_CORE { get; set; }

        public SC_SkYaRk_VR_Editionv002.SC_Console_WRITER _SC_CONSOLE_WRITER { get; set; }

        public SC_SkYaRk_VR_Editionv002.SC_Console_READER _SC_CONSOLE_READER { get; set; }
        public SC_SkYaRk_VR_Editionv002.SC_Systems _SC_SYSTEMS { get; set; }
        public SC_SkYaRk_VR_Editionv002.Program _PROGRAM { get; set; }*/

        /*public Program _PROGRAM { get; set; }
        public IComponent _icomponent { get; private set; }

        public SC_Console_CORE _SC_CONSOLE_CORE { get; set; }
        public SC_Console_READER _SC_CONSOLE_READER { get; set; }
        public SC_Console_WRITER _SC_CONSOLE_WRITER { get; set; }

        public SC_Systems _SC_SYSTEMS { get; set; }*/

        //public SC_SkYaRk_VR_Editionv002.SC_Console_WRITER _CONSOLE_WRITER;
        //public SC_SkYaRk_VR_Editionv002.SC_Console_READER _CONSOLE_READER;

        public int _console_hasINIT = 1;
        //static public List<_console_writer_message_queue> _Console_writer_message_queue;
        //static public _console_writer_message_queue _msg_wr = new _console_writer_message_queue();

        public SC_Console_CORE(SC_object_messenger_dispatcher.SC_message_object[] tester)
        {
            _createConsole(tester);

            /*_Console_writer_message_queue = _SC_Console_WRITER._Console_writer_message_queue;
            _msg_wr = new _console_writer_message_queue();

            _msg_wr._message = "_IN_1000_y00 starting Console";
            _msg_wr._lineX = 0;
            _msg_wr._lineY = 5;

            _Console_writer_message_queue.Add(_msg_wr);*/

            /*lock (_Console_writer_message_queue)
            {
                _Console_writer_message_queue.Add(_msg_wr);
            }*/
            ////System.Windows.MessageBox.Show("Console TEST", "Console");
        }

        public int numberOfLinePass = 0;
        int _anotherCounter = 0;
        int _thankYouCounter = 0;

        public List<object[]> _someCustomizedSpeedMessageQueue = new List<object[]>();
        public  List<object[]> _someQueue = new List<object[]>();
        public  List<SC_object_messenger_dispatcher.SC_message_object> _dispatchQueue_has_Main_Responded = new List<SC_object_messenger_dispatcher.SC_message_object>();
        public  List<SC_object_messenger_dispatcher.SC_message_object> _dispatchQueue_has_Sec_Responded = new List<SC_object_messenger_dispatcher.SC_message_object>();
        public  List<object> _dispatchQueue = new List<object>();

         object _ResultsOfTasks0;
         SC_object_messenger_dispatcher.SC_message_object _ResultsOfTasks1;

         int _counterTaskIsAlive = 0;
         SC_object_messenger_dispatcher.SC_message_object _data0;
         int _xCurrentCursorPos = 0;
         int _yCurrentCursorPos = 0;

         int _startConsole = 1;
         object _mainObjectToReturn;

         Task task0;

         public int _console_ERROR = 0;

        public static SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject0 = new SC_object_messenger_dispatcher.SC_message_object[16];
        public static SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject1 = new SC_object_messenger_dispatcher.SC_message_object[16];
        public static SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject2 = new SC_object_messenger_dispatcher.SC_message_object[16];
        public static SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject3 = new SC_object_messenger_dispatcher.SC_message_object[16];
        public static SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject4 = new SC_object_messenger_dispatcher.SC_message_object[16];
        public static SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject5 = new SC_object_messenger_dispatcher.SC_message_object[16];
        public static SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject6 = new SC_object_messenger_dispatcher.SC_message_object[16];
        public static SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject7 = new SC_object_messenger_dispatcher.SC_message_object[16];
        public static SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject8 = new SC_object_messenger_dispatcher.SC_message_object[16];
        public static SC_object_messenger_dispatcher.SC_message_object[] _someReceivedObject9 = new SC_object_messenger_dispatcher.SC_message_object[16];

         Task task01 = null;
         int _Task00_init_console = 1;
         int _console_is_alive_00 = 0;

        public IntPtr handle;

        //bool ShowScrollBar( HWND hWnd,int wBar,BOOL bShow);
        [DllImport("user32.dll")]
        static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);
        private enum ScrollBarDirection
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }
        public DockingHelper _dockHelper;

        public class DockingHelper
        {
            private readonly uint m_processId, m_threadId;

            private readonly IntPtr m_target;

            // Needed to prevent the GC from sweeping up our callback
            private readonly WinEventDelegate m_winEventDelegate;
            public IntPtr m_hook;

            private Timer m_timer;

            public DockingHelper() //string windowName, string className
            {
                
                //if (windowName == null && className == null) throw new ArgumentException("Either windowName or className must have a value");

                //m_target = FindWindow(className, windowName);
                //ThrowOnWin32Error("Failed to get target window");

                m_target = GetConsoleWindow();

                m_threadId = GetWindowThreadProcessId(m_target, out m_processId);
                ThrowOnWin32Error("Failed to get process id");

                m_winEventDelegate = WhenWindowMoveStartsOrEnds;
            }

            [DllImport("user32.dll", SetLastError = true)]
            private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

            [DllImport("user32.dll", SetLastError = true)]
            private static extern bool UnhookWinEvent(IntPtr hWinEventHook);

            [DllImport("user32.dll")]
            private static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [DllImport("user32.dll", SetLastError = true)]
            private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

            private void ThrowOnWin32Error(string message)
            {
                int err = Marshal.GetLastWin32Error();
                if (err != 0)
                    throw new Win32Exception(err, message);
            }

            private RECT GetWindowLocation()
            {
                RECT loc;
                GetWindowRect(m_target, out loc);
                if (Marshal.GetLastWin32Error() != 0)
                {
                    // Do something useful with this to handle if the target window closes, etc.
                }
                return loc;
            }

            public void Subscribe()
            {
                // 10 = window move start, 11 = window move end, 0 = fire out of context
                m_hook = SetWinEventHook(10, 11, m_target, m_winEventDelegate, m_processId, m_threadId, 0);
            }

            private void PollWindowLocation(object state)
            {
                var location = GetWindowLocation();
                // TODO: Reposition your window with the values from location (or fire an event with it attached)
            }

            public void Unsubscribe()
            {
                UnhookWinEvent(m_hook);
            }

            private void WhenWindowMoveStartsOrEnds(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
            {
                
                if (hwnd != m_target) // We only want events from our target window, not other windows owned by the thread.
                    return;

                if (eventType == 10) // Starts
                {
                    m_timer = new Timer(PollWindowLocation, null, 10, Timeout.Infinite);
                    // This is always the original position of the window, so we don't need to do anything, yet.
                }
                else if (eventType == 11)
                {
                    m_timer.Dispose();
                    m_timer = null;
                    var location = GetWindowLocation();
                    // TODO: Reposition your window with the values from location (or fire an event with it attached)
                }
                
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct RECT
            {
                public int Left, Top, Right, Bottom;
            }

            private delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
        }








        private delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);
        public IntPtr hHook;
        uint processHandle;

        HookProc PaintHookProcedure;

        [DllImport("user32.dll", EntryPoint = "SetWindowsHookEx", SetLastError = true)]
        static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        private const int WM_PAINT = 15;
        private const int WH_GETMESSAGE = 3;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);



        

        /*protected override void WndProc(ref Message m)
        {
            Boolean handled = false; m.Result = IntPtr.Zero;
            if (m.Msg == NativeCalls.APIAttach && (uint)m.Param == NativeCalls.SKYPECONTROLAPI_ATTACH_SUCCESS)
            {
                // Get the current handle to the Skype window
                NativeCalls.HWND_BROADCAST = m.WParam;
                handled = true;
                m.Result = new IntPtr(1);
            }

            // Skype sends our program messages using WM_COPYDATA. the data is in lParam
            if (m.Msg == NativeCalls.WM_COPYDATA && m.WParam == NativeCalls.HWND_BROADCAST)
            {
                COPYDATASTRUCT data = (COPYDATASTRUCT)Marshal.PtrToStructure(m.LParam, typeof(COPYDATASTRUCT));
                StatusTextBox.AppendText(data.lpData + Environment.NewLine);

                // Check for connection
                if (data.lpData.IndexOf("CONNSTATUS ONLINE") > -1)
                    ConnectButton.IsEnabled = false;

                // Check for calls
                IsCallInProgress(data.lpData);
                handled = true;
                m.Result = new IntPtr(1);
            }

            if (handled) DefWndProc(ref m); else base.WndProc(ref m);
        }*/
        //LRESULT CALLBACK WindowProc(_In_ HWND   hwnd,_In_ UINT   uMsg,_In_ WPARAM wParam,_In_ LPARAM lParam);

        protected virtual void WndProc(ref Message m)
        {
            Console.WriteLine(m.ToString());

        }
        //protected override void WndProc(ref Message m);


        public IntPtr hMod;
        public int PaintHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // Do some painting here.
            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);


        public void _createConsole(SC_object_messenger_dispatcher.SC_message_object[] tester)
        {
            handle = GetConsoleWindow();
            if (handle == IntPtr.Zero)
            {
                AllocConsole();
                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("IntPtr.Zero", 0, 0);
            }
            else
            {
                ShowWindow(handle, SwShow);
                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("!IntPtr.Zero", 0, 0);
            }

            IntPtr consoleHandle = GetStdHandle(STD_INPUT_HANDLE);

            if (!GetConsoleMode(consoleHandle, out _originalConsoleModeWithMouseInput))
            {
                // ERROR: Unable to get console mode.
                //return false;
                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("null GetConsoleMode", 0, 0);
            }
            else
            {
                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("not null GetConsoleMode", 0, 0);
            }      

            _modifiedConsoleMode = _originalConsoleModeWithMouseInput;

            _modifiedConsoleMode &= ~ENABLE_QUICK_EDIT;

            _originalConsoleModeWithoutMouseInput = _modifiedConsoleMode;

            // set the new mode
            if (!SetConsoleMode(consoleHandle, _modifiedConsoleMode))
            {
                // ERROR: Unable to set console mode
                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("null SetConsoleMode", 0, 0);
            }
            else
            {
                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("not null SetConsoleMode", 0, 0);
            }
            //Console.SetBufferSize(Console.BufferWidth, Console.BufferHeight);



            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {

                //DeleteMenu(sysMenu, SC_CLOSE, MF_BYCOMMAND);
                //DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
                //DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                //DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }

            Console.OutputEncoding = System.Text.Encoding.GetEncoding(28591); //System.Text.Encoding.UTF8;//  65001
            Console.SetCursorPosition(0, 0);

            int origWidth = Console.WindowWidth;
            int origHeight = Console.WindowHeight;
            int width = origWidth;
            int height = origHeight;
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);

            ShowScrollBar(handle, (int)ScrollBarDirection.SB_VERT, false);
            ShowScrollBar(handle, (int)ScrollBarDirection.SB_HORZ, false);
            ShowScrollBar(handle, (int)ScrollBarDirection.SB_BOTH, false);
        

            //_dockHelper = new DockingHelper();
            //_dockHelper.Subscribe();

            //PaintHookProcedure = new HookProc(PaintHookProc);
            //uint threadID = GetWindowThreadProcessId(handle, out processHandle);
            //hMod = System.Runtime.InteropServices.Marshal.GetHINSTANCE(typeof(SC_skYaRk_VR_Edition_v005.Program).Module);
            //hHook = SetWindowsHookEx(WH_GETMESSAGE, PaintHookProcedure, hMod, threadID);






            //Console.SetBufferSize(800, 600);
            //SetWindowPosition(handle, 0, 0, 800, 600);

            //Console.Read();

            //Console.SetWindowSize(20, 20);

            // setting buffer size  
            //Console.SetBufferSize(80, 80);

            // using the method 

            //Console.SetWindowSize(20, 20);
            //Console.SetBufferSize(80, 80);
            //Console.SetWindowPosition(0, 1);

            //var screen = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            //var width = screen.Width;
            //var height = screen.Height;

            //Console.SetCursorPosition(_original_width - 2, y);
            //Console.WriteLine(Console.BufferWidth);
            //Console.WriteLine(Console.BufferHeight);

            /////////////////////////
            ////CURSOR VISIBILITY////
            /////////////////////////
            //Console.CursorVisible = false;
            /////////////////////////
            ////CURSOR VISIBILITY////
            /////////////////////////
            //Console.Clear();
            //Console.CursorSize = 1;

            _console_hasINIT = 1;
            ////System.Windows.MessageBox.Show("is NOT null", "Console");
            /////////////////////////////////////////////////////////
            ////////////////////CONSOLE CREATED//////////////////////
            /////////////////////////////////////////////////////////            
        }

        //SC_Console_READER _SC_Console_READER = new SC_Console_READER();

        public  void _Console_CORE(SC_object_messenger_dispatcher.SC_message_object[] tester)
        {
            /*for (int i = 0; i < _someReceivedObject0.Length; i++)
            {
                _someReceivedObject0[i] = new SC_object_messenger_dispatcher.SC_message_object();
                _someReceivedObject0[i]._received_switch_in = -1;
                _someReceivedObject0[i]._received_switch_out = -1;
                _someReceivedObject0[i]._sending_switch_in = -1;
                _someReceivedObject0[i]._sending_switch_out = -1;
                _someReceivedObject0[i]._chain_Of_Tasks0 = null;
                _someReceivedObject0[i]._timeOut0 = 1;
                _someReceivedObject0[i]._passTest = "";
                _someReceivedObject0[i]._welcomePackage = -1;

                _someReceivedObject1[i] = new SC_object_messenger_dispatcher.SC_message_object();
                _someReceivedObject1[i]._received_switch_in = -1;
                _someReceivedObject1[i]._received_switch_out = -1;
                _someReceivedObject1[i]._sending_switch_in = -1;
                _someReceivedObject1[i]._sending_switch_out = -1;
                _someReceivedObject1[i]._chain_Of_Tasks0 = null;
                _someReceivedObject1[i]._timeOut0 = 1;
                _someReceivedObject1[i]._passTest = "";
                _someReceivedObject1[i]._welcomePackage = -1;

                _someReceivedObject2[i] = new SC_object_messenger_dispatcher.SC_message_object();
                _someReceivedObject2[i]._received_switch_in = -1;
                _someReceivedObject2[i]._received_switch_out = -1;
                _someReceivedObject2[i]._sending_switch_in = -1;
                _someReceivedObject2[i]._sending_switch_out = -1;
                _someReceivedObject2[i]._chain_Of_Tasks0 = null;
                _someReceivedObject2[i]._timeOut0 = 1;
                _someReceivedObject2[i]._passTest = "";
                _someReceivedObject2[i]._welcomePackage = -1;

                _someReceivedObject3[i] = new SC_object_messenger_dispatcher.SC_message_object();
                _someReceivedObject3[i]._received_switch_in = -1;
                _someReceivedObject3[i]._received_switch_out = -1;
                _someReceivedObject3[i]._sending_switch_in = -1;
                _someReceivedObject3[i]._sending_switch_out = -1;
                _someReceivedObject3[i]._chain_Of_Tasks0 = null;
                _someReceivedObject3[i]._timeOut0 = 1;
                _someReceivedObject3[i]._passTest = "";
                _someReceivedObject3[i]._welcomePackage = -1;

                _someReceivedObject4[i] = new SC_object_messenger_dispatcher.SC_message_object();
                _someReceivedObject4[i]._received_switch_in = -1;
                _someReceivedObject4[i]._received_switch_out = -1;
                _someReceivedObject4[i]._sending_switch_in = -1;
                _someReceivedObject4[i]._sending_switch_out = -1;
                _someReceivedObject4[i]._chain_Of_Tasks0 = null;
                _someReceivedObject4[i]._timeOut0 = 1;
                _someReceivedObject4[i]._passTest = "";
                _someReceivedObject4[i]._welcomePackage = -1;

                _someReceivedObject5[i] = new SC_object_messenger_dispatcher.SC_message_object();
                _someReceivedObject5[i]._received_switch_in = -1;
                _someReceivedObject5[i]._received_switch_out = -1;
                _someReceivedObject5[i]._sending_switch_in = -1;
                _someReceivedObject5[i]._sending_switch_out = -1;
                _someReceivedObject5[i]._chain_Of_Tasks0 = null;
                _someReceivedObject5[i]._timeOut0 = 1;
                _someReceivedObject5[i]._passTest = "";
                _someReceivedObject5[i]._welcomePackage = -1;

                _someReceivedObject6[i] = new SC_object_messenger_dispatcher.SC_message_object();
                _someReceivedObject6[i]._received_switch_in = -1;
                _someReceivedObject6[i]._received_switch_out = -1;
                _someReceivedObject6[i]._sending_switch_in = -1;
                _someReceivedObject6[i]._sending_switch_out = -1;
                _someReceivedObject6[i]._chain_Of_Tasks0 = null;
                _someReceivedObject6[i]._timeOut0 = 1;
                _someReceivedObject6[i]._passTest = "";
                _someReceivedObject6[i]._welcomePackage = -1;

                _someReceivedObject7[i] = new SC_object_messenger_dispatcher.SC_message_object();
                _someReceivedObject7[i]._received_switch_in = -1;
                _someReceivedObject7[i]._received_switch_out = -1;
                _someReceivedObject7[i]._sending_switch_in = -1;
                _someReceivedObject7[i]._sending_switch_out = -1;
                _someReceivedObject7[i]._chain_Of_Tasks0 = null;
                _someReceivedObject7[i]._timeOut0 = 1;
                _someReceivedObject7[i]._passTest = "";
                _someReceivedObject7[i]._welcomePackage = -1;

                _someReceivedObject8[i] = new SC_object_messenger_dispatcher.SC_message_object();
                _someReceivedObject8[i]._received_switch_in = -1;
                _someReceivedObject8[i]._received_switch_out = -1;
                _someReceivedObject8[i]._sending_switch_in = -1;
                _someReceivedObject8[i]._sending_switch_out = -1;
                _someReceivedObject8[i]._chain_Of_Tasks0 = null;
                _someReceivedObject8[i]._timeOut0 = 1;
                _someReceivedObject8[i]._passTest = "";
                _someReceivedObject8[i]._welcomePackage = -1;
            }*/

            /*object objectForLoop = new object();

            SC_object_messenger_dispatcher.SC_message_object _objForLoop = new SC_object_messenger_dispatcher.SC_message_object();
            _objForLoop._received_switch_in = -1;
            _objForLoop._received_switch_out = -1;
            _objForLoop._sending_switch_in = -1;
            _objForLoop._sending_switch_out = -1;
            _objForLoop._chain_Of_Tasks0 = null;
            _objForLoop._timeOut0 = 1;
            _objForLoop._passTest = "";
            _objForLoop._welcomePackage = -1;

            int _taskInitSwitch = 1;

        _threadlOOper:
            if (_taskInitSwitch== 1)
            {
                _mainTask = Task<object>.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        testingTasks = _console_worker_two(tester, out objectForLoop);
                        Thread.Sleep(1);
                    }
                    //return testingTasks;
                });
                _taskInitSwitch = 0;
            }

            goto _threadlOOper; */




            /*int _taskInitSwitch = 1;
        _threadlOOper:

            if (_taskInitSwitch == 1)
            {
                _mainTask = Task<object>.Factory.StartNew(() =>
                {
                        testingTasks = _console_worker_two(tester, out objectForLoop);

                    return testingTasks;
                });
                _taskInitSwitch = 0;
            }

            SC_object_messenger_dispatcher.SC_message_object _data00_inner = (SC_object_messenger_dispatcher.SC_message_object)testingTasks;
            int _received_switch_in0 = _data00_inner._received_switch_in;   //1
            int _received_switch_out0 = _data00_inner._received_switch_out; //0
            int _sending_switch_in0 = _data00_inner._sending_switch_in;     //0
            int _sending_switch_out0 = _data00_inner._sending_switch_out;   //0
            List<int[]> _chain_Of_Tasks0 = _data00_inner._chain_Of_Tasks0;
            int _timeOut0 = _data00_inner._timeOut0;
            int _ParentTaskThreadID0 = _data00_inner._ParentTaskThreadID0;
            int _main_cpu_count0 = _data00_inner._main_cpu_count;
            string _passTest0 = _data00_inner._passTest;
            int _welcomePackage0 = _data00_inner._welcomePackage;

            if (_passTest0.ToLower() == "nine" || _passTest0.ToLower() == "ninekorn")
            {
                System.Windows.MessageBox.Show("_pass: " + _passTest0 + "_8", "Console");
            }
            Thread.Sleep(1);
            goto _threadlOOper;*/
        }
        Task<object> _mainTask;
        object testingTasks;

        static SC_object_messenger_dispatcher.SC_message_object _data00_IN;

        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;
        //private const int WM_SIZE = 0x0005;




        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
     int x, int y, int cx, int cy, int flags);

        const int SWP_NOZORDER = 0x4;
        const int SWP_NOACTIVATE = 0x10;
        public static void SetWindowPosition(IntPtr Handle, int x, int y, int width, int height)
        {
            SetWindowPos(Handle, IntPtr.Zero, x, y, width, height, SWP_NOZORDER | SWP_NOACTIVATE);
        }







        public  void HideConsoleWindow()
        {
            //var handle = GetConsoleWindow();
            //ShowWindow(handle, SwHide);
        }

        public  void starterItems(string main, string secondary)
        {
            string currentTime = DateTime.Now.ToString("h:mm:ss tt");
            string currentDay = DateTime.Today.ToString();
        }

        public  void clearConsole()
        {
            Console.Clear();
        }

        public  void writeToConsole(string test)
        {
            Console.Write(test);
        }

        public  void writeLineToConsole(string test)
        {
            Console.WriteLine(test);
        }
        public  int cursorLeft()
        {
            int left = Console.CursorLeft;
            return left;
        }
        public  int cursorTop()
        {
            int top = Console.CursorTop;
            return top;
        }
        public  int consoleWidth()
        {
            int width = Console.WindowWidth;
            return width;
        }
        public  int consoleHeight()
        {
            int height = Console.WindowHeight;
            return height;
        }

        public  void consoleSwitchLine()
        {
            Console.Write("\n");
        }

        public  void consoleResetCursor()
        {
            int top = Console.CursorTop;
            int left = Console.CursorLeft;
            Console.SetCursorPosition(left, top);
        }
        public  void ClearCurrentConsoleLine(int x, int y)
        {
            //int currentLineCursor = Console.CursorTop;
            //Console.SetCursorPosition(0, Console.CursorTop);
            Console.SetCursorPosition(0, y);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                //Console.SetCursorPosition(i, y);
                Console.Write(new string(' ', Console.WindowWidth));
            }
        }
        public  void setCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        public  void WriteAt(string s, int x, int y, bool canPassLine, int linePassNumber)
        {
            string numberOfLines = "";
            for (int i = 0; i < linePassNumber; i++)
            {
                if (canPassLine)
                {
                    numberOfLines += "\n";
                }
            }

            Console.SetCursorPosition(x, y);
            Console.Write(numberOfLines + s);
        }

        [DllImport(@"kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        //[DllImport(@"kernel32.dll")]
        //static extern IntPtr GetConsoleWindow();

        [DllImport(@"user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        const int SwHide = 0;
        const int SwShow = 5;

        const int ENABLE_MOUSE_INPUT = 0x0010;

        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);
        [DllImport("kernel32.dll")]
        static  extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        const int STD_INPUT_HANDLE = -10;
        const uint ENABLE_QUICK_EDIT = 0x0040;

        public  uint _originalConsoleModeWithMouseInput;
        public   uint _originalConsoleModeWithoutMouseInput;
        public  uint _modifiedConsoleMode;
    }
}

/*task0 = Task<object>.Factory.StartNew(() =>
{
    _mainObjectToReturn = _DoWork_hasTaskWorked(_main_object);

    return _mainObjectToReturn;
});*/



//"▬" gives out a Question Mark
//~←∟↓→§▬↨↓→╕╕╣╖╢╡┤│▓░○▒»«¡¼½
//"ALT1020++"g☺☺}☺☻♥♦♣♠≈•◘○◙♂♀♪
//ⁿ²■☺☻☺











/*
public class DockingHelper
{
    private readonly uint m_processId, m_threadId;

    private readonly IntPtr m_target;

    // Needed to prevent the GC from sweeping up our callback
    private readonly WinEventDelegate m_winEventDelegate;
    private IntPtr m_hook;

    private Timer m_timer;

    public DockingHelper(string windowName, string className)
    {
        if (windowName == null && className == null) throw new ArgumentException("Either windowName or className must have a value");

        m_target = FindWindow(className, windowName);
        ThrowOnWin32Error("Failed to get target window");

        m_threadId = GetWindowThreadProcessId(m_target, out m_processId);
        ThrowOnWin32Error("Failed to get process id");

        m_winEventDelegate = WhenWindowMoveStartsOrEnds;
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool UnhookWinEvent(IntPtr hWinEventHook);

    [DllImport("user32.dll")]
    private static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    private void ThrowOnWin32Error(string message)
    {
        int err = Marshal.GetLastWin32Error();
        if (err != 0)
            throw new Win32Exception(err, message);
    }

    private RECT GetWindowLocation()
    {
        RECT loc;
        GetWindowRect(m_target, out loc);
        if (Marshal.GetLastWin32Error() != 0)
        {
            // Do something useful with this to handle if the target window closes, etc.
        }
        return loc;
    }

    public void Subscribe()
    {
        // 10 = window move start, 11 = window move end, 0 = fire out of context
        m_hook = SetWinEventHook(10, 11, m_target, m_winEventDelegate, m_processId, m_threadId, 0);
    }

    private void PollWindowLocation(object state)
    {
        var location = GetWindowLocation();
        // TODO: Reposition your window with the values from location (or fire an event with it attached)
    }

    public void Unsubscribe()
    {
        UnhookWinEvent(m_hook);
    }

    private void WhenWindowMoveStartsOrEnds(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
    {
        if (hwnd != m_target) // We only want events from our target window, not other windows owned by the thread.
            return;

        if (eventType == 10) // Starts
        {
            m_timer = new Timer(PollWindowLocation, null, 10, Timeout.Infinite);
            // This is always the original position of the window, so we don't need to do anything, yet.
        }
        else if (eventType == 11)
        {
            m_timer.Dispose();
            m_timer = null;
            var location = GetWindowLocation();
            // TODO: Reposition your window with the values from location (or fire an event with it attached)
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left, Top, Right, Bottom;
    }

    private delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
}*/