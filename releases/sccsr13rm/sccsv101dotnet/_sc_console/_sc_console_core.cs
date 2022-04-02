using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharpDX.Direct3D11;
using System.Runtime.InteropServices;
using System.Threading;
using System.ComponentModel;

namespace _sc_core_systems._sc_console
{
    public class _sc_console_core : _sc_icomponent
    {
        public static _sc_icomponent _SC_ICOMPONENT;
        _sc_globals _sc_icomponent._SC_Globals
        {
            get => _SC_GLOB;
        }
        public static _sc_globals _SC_GLOB;
        public int _console_hasINIT = 1;

        [DllImport("user32.dll")]
        static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);
        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        [DllImport(@"kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport(@"user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("user32")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);

        const int SWP_NOZORDER = 0x4;
        const int SWP_NOACTIVATE = 0x10;
        public static void SetWindowPosition(IntPtr Handle, int x, int y, int width, int height)
        {
            SetWindowPos(Handle, IntPtr.Zero, x, y, width, height, SWP_NOZORDER | SWP_NOACTIVATE);
        }

        const int SwHide = 0;
        const int SwShow = 5;

        const int ENABLE_MOUSE_INPUT = 0x0010;

        const int STD_INPUT_HANDLE = -10;
        const uint ENABLE_QUICK_EDIT = 0x0040;

        public uint _originalConsoleModeWithMouseInput;
        public uint _originalConsoleModeWithoutMouseInput;
        public uint _modifiedConsoleMode;


        public _sc_console_core(sc_message_object.sc_message_object[] tester)
        {
            _createConsole(tester);
        }


        public IntPtr handle;

        private enum ScrollBarDirection
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }

        public IntPtr hMod;

        public void _reset_console_borders()
        {
            //IntPtr sysMenu = GetSystemMenu(handle, false);
            if (handle != IntPtr.Zero)
            {
                //DeleteMenu(sysMenu, SC_CLOSE, MF_BYCOMMAND);
                //DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
                //DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                //DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
                ShowScrollBar(handle, (int)ScrollBarDirection.SB_VERT, false);
                ShowScrollBar(handle, (int)ScrollBarDirection.SB_HORZ, false);
                ShowScrollBar(handle, (int)ScrollBarDirection.SB_BOTH, false);

            }
        }


        public void _createConsole(sc_message_object.sc_message_object[] tester)
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
  
            _console_hasINIT = 1;

            /////////////////////////////////////////////////////////
            ////////////////////CONSOLE CREATED//////////////////////
            /////////////////////////////////////////////////////////            
        }


        static sc_message_object.sc_message_object _data00_IN;

        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;
        //private const int WM_SIZE = 0x0005;






            /*
        public  void HideConsoleWindow()
        {

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
            Console.SetCursorPosition(0, y);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
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
        }*/
    }
}