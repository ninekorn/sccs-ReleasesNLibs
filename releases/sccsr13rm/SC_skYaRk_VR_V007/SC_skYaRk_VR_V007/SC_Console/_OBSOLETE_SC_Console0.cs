using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;

using System.Windows;
using System.Windows.Forms;

using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;






namespace SC_SkYaRk_VR_Editionv002
{
    //[SuppressUnmanagedCodeSecurity]
    public static class SC_Console
    {
        [DllImport(@"kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport(@"kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport(@"user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        const int SwHide = 0;
        const int SwShow = 5;


        /*

//#pragma warning disable CS0414 // The field 'SC_Console.startItems' is assigned but its value is never used
        static bool startItems = true;
//#pragma warning restore CS0414 // The field 'SC_Console.startItems' is assigned but its value is never used
//#pragma warning disable CS0414 // The field 'SC_Console.startWatch' is assigned but its value is never used
        static bool startWatch = true;
//#pragma warning restore CS0414 // The field 'SC_Console.startWatch' is assigned but its value is never used

//#pragma warning disable CS0169 // The field 'SC_Console.stopWatch' is never used
        static Stopwatch stopWatch;
//#pragma warning restore CS0169 // The field 'SC_Console.stopWatch' is never used

//#pragma warning disable CS0414 // The field 'SC_Console.seconds' is assigned but its value is never used
        static int seconds = 0;
//#pragma warning restore CS0414 // The field 'SC_Console.seconds' is assigned but its value is never used
//#pragma warning disable CS0414 // The field 'SC_Console.lastFrameCounter' is assigned but its value is never used
        static int lastFrameCounter = 0;
//#pragma warning restore CS0414 // The field 'SC_Console.lastFrameCounter' is assigned but its value is never used
//#pragma warning disable CS0414 // The field 'SC_Console.lastSecondFrameCounter' is assigned but its value is never used
        static int lastSecondFrameCounter = 0;
//#pragma warning restore CS0414 // The field 'SC_Console.lastSecondFrameCounter' is assigned but its value is never used

//#pragma warning disable CS0414 // The field 'SC_Console.frameRate' is assigned but its value is never used
        static int frameRate = 0;
//#pragma warning restore CS0414 // The field 'SC_Console.frameRate' is assigned but its value is never used
//#pragma warning disable CS0414 // The field 'SC_Console.functionStarter' is assigned but its value is never used
        static bool functionStarter = false;
//#pragma warning restore CS0414 // The field 'SC_Console.functionStarter' is assigned but its value is never used
//#pragma warning disable CS0414 // The field 'SC_Console._isConsoleStarted' is assigned but its value is never used
        static bool _isConsoleStarted = false;
//#pragma warning restore CS0414 // The field 'SC_Console._isConsoleStarted' is assigned but its value is never used
*/

        //static SC_Console _currentConsole; 
        /*const int ENABLE_MOUSE_INPUT = 0x0010;

        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);
        const int STD_INPUT_HANDLE = -10;
        const uint ENABLE_QUICK_EDIT = 0x0040;

        public static uint _originalConsoleModeWithMouseInput;
        public static uint _originalConsoleModeWithoutMouseInput;
        public static uint _modifiedConsoleMode;
        */






        /*HWND GetConsoleHwnd(void)
        {
            #define MY_BUFSIZE 1024 // Buffer size for console window titles.
            HWND hwndFound;         // This is what is returned to the caller.
            char pszNewWindowTitle[MY_BUFSIZE]; // Contains fabricated
                                                // WindowTitle.
            char pszOldWindowTitle[MY_BUFSIZE]; // Contains original
                                                // WindowTitle.

            // Fetch current window title.

            GetConsoleTitle(pszOldWindowTitle, MY_BUFSIZE);

            // Format a "unique" NewWindowTitle.

            wsprintf(pszNewWindowTitle, "%d/%d",
                        GetTickCount(),
                        GetCurrentProcessId());

            // Change current window title.

            SetConsoleTitle(pszNewWindowTitle);

            // Ensure window title has been updated.

            Sleep(40);

            // Look for NewWindowTitle.

            hwndFound = FindWindow(NULL, pszNewWindowTitle);

            // Restore original window title.

            SetConsoleTitle(pszOldWindowTitle);

            return (hwndFound);
        }*/



        /*[DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AttachConsole(uint dwProcessId);
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool FreeConsole();


        [DllImport("kernel32.dll")]
        public static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate HandlerRoutine,bool Add);
        public delegate Boolean ConsoleCtrlDelegate(CtrlTypes CtrlType);
        public enum CtrlTypes : uint
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        public static bool is_attached = false;

        public static ConsoleCtrlDelegate ConsoleCtrlDelegateDetach = delegate (CtrlType) 
        {
            if (is_attached = !FreeConsole())
            {
                //Trace.Error('FreeConsole on ' + CtrlTypes + ': ' + new Win32Exception());
            }
            return true;
        };*/

        //GetConsoleWindow

        public static void createConsole()
        {
            //System.Windows.MessageBox.Show("test", "test");



            var handle = GetConsoleWindow();

            if (handle == IntPtr.Zero)
            {
                AllocConsole();
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("IntPtr.Zero", 0, 0);
            }
            else
            {
                ShowWindow(handle, SwShow);
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("!IntPtr.Zero", 0, 0);

            }



            //startConsoleThreadManager(1);



            /*var handle = GetConsoleWindow();

            if (handle == IntPtr.Zero)
            {
                AllocConsole();


                //int workerThreads;
                ////int portThreads;
                //ThreadPool.GetMaxThreads(out workerThreads, out portThreads);
                //ThreadPool.GetAvailableThreads(out workerThreads,out portThreads);

                //int totalThreads = (int)(portThreads * 0.1f);
                //startMainThreadDispatcher(1);
            }
            else
            {
                ShowWindow(handle, SwShow);
            }





            IntPtr consoleHandle = GetStdHandle(STD_INPUT_HANDLE);

            if (!GetConsoleMode(consoleHandle, out _originalConsoleModeWithMouseInput))
            {
                // ERROR: Unable to get console mode.
                //return false;
                //Console.WriteLine("null GetConsoleMode");
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("null GetConsoleMode", 0, 0);
            }
            else
            {
                //Console.WriteLine("not null GetConsoleMode");
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("not null GetConsoleMode", 0, 0);
            }

            _modifiedConsoleMode = _originalConsoleModeWithMouseInput;

            _modifiedConsoleMode &= ~ENABLE_QUICK_EDIT;

            _originalConsoleModeWithoutMouseInput = _modifiedConsoleMode;


            // set the new mode
            if (!SetConsoleMode(consoleHandle, _modifiedConsoleMode))
            {
                // ERROR: Unable to set console mode
                //Console.WriteLine("null SetConsoleMode");
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("null SetConsoleMode", 0, 0);
            }
            else
            {
                //Console.WriteLine("not null SetConsoleMode");
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("not null SetConsoleMode", 0, 0);
            }

            //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("test", 0, 0);
            startConsoleThreadManager(1);
            */











            //GC.Collect();          
            //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("test", 0, 0);


            //starterItems(main, secondary);

            /*if (startItems)
            {
                stopWatch = new Stopwatch();
                startItems = false;
            }

            if (startWatch)
            {
                stopWatch.Start();
                startWatch = false;
            }

            TimeSpan currentTimeSpan = stopWatch.Elapsed;
            seconds = currentTimeSpan.Seconds;

            if (seconds >= 2147483645) //2 147 483 647      //hexadécimal 0x7FFFFFFF.
            {
                stopWatch.Restart();
            }

            if (lastSecondFrameCounter != seconds)
            {
                //int dataX = SC_Console.cursorLeft();
                //int dataY = SC_Console.cursorTop();
                //SC_Console.setCursorPosition(0,0);
                SC_Console.WriteAt(seconds + "", 1, 1, false, 0);
                //SC_Console.WriteAt(dataY + "", 0, 1, false, 0);
                lastFrameCounter = frameRate;
            }
            lastSecondFrameCounter = currentTimeSpan.Seconds;


            if (frameRate >= 2147483645) //2 147 483 647      //hexadécimal 0x7FFFFFFF.
            {
                frameRate = 0;
            }

            if (functionStarter)
            {
                //objectives();
                functionStarter = false;
            }

            frameRate++;*/
        }

        //Queue<>

        //public static Queue<consoleMessageQueue> _someQueue = new Queue<consoleMessageQueue>();
        public static List<object[]> _someQueue = new List<object[]>();
        


        //public static object[] array1;

        public class consoleMessageQueue
        {
            //public static object _dummyObject = new object();
            public static object[] _someObject;
            public static string _message = "";
            public static int _lineX = 0;
            public static int _lineY = 0;

            //public static object[][] someTesting;

            public consoleMessageQueue(string message, int lineX, int lineY)
            {
                object[] test = new object[3];
                test[0] = message;
                test[1] = lineX;
                test[2] = lineY;


                _someQueue.Add(test);
                


                //someTesting = new object[][];
                //string newString = "";
                //newString += _message;
                //newString += ""





                /*this._message = message;
                this._lineX = lineX;
                this._lineY = lineY;*/

                //_someQueue.Add(this);
                /*lock (SC_test_to_delete.SC_Console._someQueue)
                {
                    SC_test_to_delete.SC_Console._someQueue.Enqueue(this);
                }*/
            }
        }



        //public static byte[] test






















        public static bool _hasConsole = false;
        public static int _xCurrentCursorPos = 0;
        public static int _yCurrentCursorPos = 0;

        public static int _xLastCursorPos = 0;
        public static int _yLastCursorPos = 0;

        //VERTICAL AND HORIZONTAL ONLY - I WILL BE IMPLEMENTING DIAGONAL BUT ILL NEED TO TEST IF THE CURSOR CAN MOVE DIAGONALLY OR MOVES UP THEN RIGHT FOR EXAMPLE...
        public static void dispatchConsoleCommands(object[] someConsoleData)
        {
            string consoleMessage = (string)someConsoleData[0];
            int _targetLineX = (int)someConsoleData[1];
            int _targetLineY = (int)someConsoleData[2];

            _xCurrentCursorPos = SC_Console.cursorLeft();
            _yCurrentCursorPos = SC_Console.cursorTop();



            Console.SetCursorPosition(0, _yCurrentCursorPos);
            Console.SetCursorPosition(0, _targetLineY);
            Console.SetCursorPosition(_targetLineX, _targetLineY);

            Console.Write(consoleMessage);
            //Console.SetCursorPosition(_targetLineX, _targetLineY);
            //Console.Write(consoleMessage);














            /*if (_targetLineY > _yCurrentCursorPos)
            {
                int _yMove = _targetLineY - _yCurrentCursorPos;

                for (int y = 0; y < _yMove; y++)
                {
                    Console.SetCursorPosition(_xCurrentCursorPos, _yCurrentCursorPos + y);
                }
            }
            else// (_targetLineY < _yCurrentCursorPos)
            {
                int _yMove = _targetLineY - _yCurrentCursorPos;
                for (int y = 0; y < _yMove; y++)
                {
                    Console.SetCursorPosition(_xCurrentCursorPos, _yCurrentCursorPos - y);
                }
            }






            if (_targetLineX > _xCurrentCursorPos) //  8  3
            {
                int _xMove = _targetLineX - _xCurrentCursorPos; //int _xmove = 8 - 3 = 5

                for (int x = 0; x < _xMove; x++) // x < 5 x++    => 0 1 2 3 4
                {
                    Console.SetCursorPosition(_xCurrentCursorPos + x, _yCurrentCursorPos);
                }
            }

            else// if (_targetLineX < _xCurrentCursorPos)
            {
                int _xMove = _targetLineX - _xCurrentCursorPos; // 8-3 = 5
                for (int x = 0; x < _xMove; x++)
                {
                    Console.SetCursorPosition(_xCurrentCursorPos - x, _yCurrentCursorPos);
                }
            }*/



















            //Console.WriteLine("\b" + message + "\b");
            //\bhello\b


            //Console.WriteLine(message);


            /*if (_targetLineX > _xCurrentCursorPos)
            {
                int _xMove = _targetLineX - _xCurrentCursorPos;

                if (_targetLineY > _yCurrentCursorPos)
                {
                    int _yMove = _targetLineY - _yCurrentCursorPos;

                    for (int x = 0; x < _xMove; x++)
                    {
                        for (int y = 0; y < _yMove; y++)
                        {
                            Console.SetCursorPosition(_xCurrentCursorPos + x, _yCurrentCursorPos + y);
                        }
                    }
                }
                else if (_targetLineY < _yCurrentCursorPos)
                {
                    int _yMove = _targetLineY - _yCurrentCursorPos;

                    for (int x = 0; x < _xMove; x++)
                    {
                        for (int y = 0; y < _yMove; y++)
                        {
                            Console.SetCursorPosition(_xCurrentCursorPos + x, _yCurrentCursorPos - y);
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < _xMove; x++)
                    {
                        Console.SetCursorPosition(_xCurrentCursorPos + x, _yCurrentCursorPos);
                    }
                }
            }
            else if (_targetLineX < _xCurrentCursorPos)
            {
                int _xMove = _targetLineX - _xCurrentCursorPos;

                if (_targetLineY > _yCurrentCursorPos)
                {
                    int _yMove = _targetLineY - _yCurrentCursorPos;

                    for (int x = 0; x < _xMove; x++)
                    {
                        for (int y = 0; y < _yMove; y++)
                        {
                            Console.SetCursorPosition(_xCurrentCursorPos - x, _yCurrentCursorPos + y);
                        }
                    }
                }
                else if (_targetLineY < _yCurrentCursorPos)
                {
                    int _yMove = _targetLineY - _yCurrentCursorPos;

                    for (int x = 0; x < _xMove; x++)
                    {
                        for (int y = 0; y < _yMove; y++)
                        {
                            Console.SetCursorPosition(_xCurrentCursorPos - x, _yCurrentCursorPos - y);
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < _xMove; x++)
                    {
                        Console.SetCursorPosition(_xCurrentCursorPos - x, _yCurrentCursorPos);
                    }
                }
            }
            else
            {

                if (_targetLineY > _yCurrentCursorPos)
                {
                    int _yMove = _targetLineY - _yCurrentCursorPos;

                    for (int y = 0; y < _yMove; y++)
                    {
                        Console.SetCursorPosition(_xCurrentCursorPos, _yCurrentCursorPos + y);
                    }
                }
                else if (_targetLineY < _yCurrentCursorPos)
                {
                    int _yMove = _targetLineY - _yCurrentCursorPos;

                    for (int y = 0; y < _yMove; y++)
                    {
                        Console.SetCursorPosition(_xCurrentCursorPos, _yCurrentCursorPos - y);
                    }
                }
                else
                {
                    Console.SetCursorPosition(_xCurrentCursorPos, _yCurrentCursorPos);
                }
            }


            _xCurrentCursorPos = SC_Console.cursorLeft();
            _yCurrentCursorPos = SC_Console.cursorTop();*/

            /*for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
            }*/
            //Console.Write(message);
            //SC_Console.WriteAt(message, _xCurrentCursorPos, _yCurrentCursorPos,false,0);










            //_xCurrentCursorPos = SC_Console.cursorLeft();
            //_yCurrentCursorPos = SC_Console.cursorTop();





            //_xLastCursorPos = _xCurrentCursorPos;
            //_yLastCursorPos = _yCurrentCursorPos;


















            /*if (_xCursorPos != lineX && _yCursorPos != lineY)
            {
                int x = 1;
                int y = 1;

                int _TargetX = 0;
                int _TargetY = 0;

                if (lineX > _xCursorPos)
                {
                    _TargetX = lineX - _xCursorPos;
                    x *= 1;
                }
                else if (lineX < _xCursorPos)
                {
                    _TargetX = _xCursorPos - lineX;
                    x *= -1;
                }
                else
                {
                    _TargetX = lineX;
                    x *= 1;
                }


                if (lineY > _yCursorPos)
                {
                    _TargetY = lineY - _yCursorPos;
                    y *= 1;
                }
                else if (lineY < _yCursorPos)
                {
                    _TargetY = _yCursorPos - lineY;
                    y *= -1;
                }
                else
                {
                    _TargetY = lineY;
                   
                    y *= 1;

                }

                _TargetX *= x;
                _TargetY *= y;

                int ix = _xCursorPos;
                int iy = _yCursorPos;

                for (int i = 0; i < _TargetX; i++)
                {
                    int xii = i;
                    xii *= x;                  
                    for (int j = 0; j < _TargetX; j++)
                    {
                        int yii = j;
                        yii *= y;

                        SC_Console.setCursorPosition(ix, iy);

                        iy += yii;
                    }
                    ix += xii;
                }
            }*/

            //SC_Console.WriteAt(message, lineX, lineY, false, 0);


        }










//#pragma warning disable CS0414 // The field 'SC_Console.mainStarterItems' is assigned but its value is never used
        static bool mainStarterItems = true;
//#pragma warning restore CS0414 // The field 'SC_Console.mainStarterItems' is assigned but its value is never used
        static int mainFrameCounter = 0;
        static ThreadStart threadOne;

        public static void startConsoleThreadManager(int totalThreads)
        {
            threadOne = delegate
            {
                threadConsole(totalThreads);
            };
            Thread threaderr = new Thread(threadOne);
//#pragma warning disable CS0618 // 'Thread.ApartmentState' is obsolete: 'The ApartmentState property has been deprecated.  Use GetApartmentState, SetApartmentState or TrySetApartmentState instead.'
            threaderr.ApartmentState = ApartmentState.STA;
//#pragma warning restore CS0618 // 'Thread.ApartmentState' is obsolete: 'The ApartmentState property has been deprecated.  Use GetApartmentState, SetApartmentState or TrySetApartmentState instead.'
            threaderr.IsBackground = true;
            threaderr.Start();
        }






//#pragma warning disable CS0414 // The field 'SC_Console.frameCounterForGoingLeft' is assigned but its value is never used
        static int frameCounterForGoingLeft = 0;
//#pragma warning restore CS0414 // The field 'SC_Console.frameCounterForGoingLeft' is assigned but its value is never used
//#pragma warning disable CS0414 // The field 'SC_Console.consoleCursorSpeed' is assigned but its value is never used
        static int consoleCursorSpeed = 100;
//#pragma warning restore CS0414 // The field 'SC_Console.consoleCursorSpeed' is assigned but its value is never used
        public static bool canResetCursor = true;

        public static string _lastConsoleMessage = "";
        public static void threadConsole(int totalThreads)
        {
            try
            {
                threadTester:

                //SC_Console.consoleMessageQueue messageQueue00 = new SC_Console.consoleMessageQueue(_someQueue.Count.ToString(), 10, 1);

                lock (_someQueue)
                {
                    if (_someQueue.Count > 0)
                    {
                        object[] test = _someQueue[0];
                        test[0] = _someQueue[0][0];
                        test[1] = _someQueue[0][1];
                        test[2] = _someQueue[0][2];

                        /*if (_lastConsoleMessage != (string)test[0])
                        {
                            dispatchConsoleCommands(test);
                        }*/
                        dispatchConsoleCommands(test);
                        //_lastConsoleMessage = (string)test[0];
                        _someQueue.Remove(_someQueue[0]);





                      //var data = _someQueue.Dequeue();
                      //var message = data._message;
                      //var _lineX = data._lineX;
                      //var _lineY = data._lineY;



                        /*GC.Collect();
                        GC.Collect(0);
                        GC.Collect(1);
                        GC.Collect(9999999);*/
                    }
                }

                //dispatchConsoleCommands(mainFrameCounter + "", 50, 0);

                mainFrameCounter++;
                //GC.Collect();
                Thread.Sleep(1);
                goto threadTester;
            }
            catch
            {

            }
        }




        public static void consoleMouseInput(uint _modeToSet)
        {

            /*IntPtr consoleHandle = GetStdHandle(STD_INPUT_HANDLE);
            // set the new mode
            if (!SetConsoleMode(consoleHandle, _modeToSet))
            {
                // ERROR: Unable to set console mode
                //Console.WriteLine("null");
            }
            else
            {
                //Console.WriteLine("not null");

            }*/
            //Console.Write("not null"+ consoleMode);


        }













        public static void HideConsoleWindow()
        {
            //var handle = GetConsoleWindow();
            //ShowWindow(handle, SwHide);
        }

        public static int numberOfLinePass = 0;

        public static void starterItems(string main, string secondary)
        {
            string currentTime = DateTime.Now.ToString("h:mm:ss tt");
            string currentDay = DateTime.Today.ToString();
        }


        public static void clearConsole()
        {
            Console.Clear();
        }

        public static void writeToConsole(string test)
        {
            Console.Write(test);
        }

        public static void writeLineToConsole(string test)
        {
            Console.WriteLine(test);
        }


        public static int cursorLeft()
        {
            int left = Console.CursorLeft;
            return left;
        }

        public static int cursorTop()
        {
            int top = Console.CursorTop;
            return top;
        }

        public static int consoleWidth()
        {
            int width = Console.WindowWidth;
            return width;
        }
        public static int consoleHeight()
        {
            int height = Console.WindowHeight;
            return height;
        }






        public static void consoleSwitchLine()
        {
            Console.Write("\n");
        }



        public static void consoleResetCursor()
        {
            int top = Console.CursorTop;
            int left = Console.CursorLeft;
            Console.SetCursorPosition(left, top);
        }

        public static void ClearCurrentConsoleLine(int x, int y)
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

        public static void setCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }


        public static void WriteAt(string s, int x, int y, bool canPassLine, int linePassNumber)
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
            canResetCursor = true;
        }
    }
}










/////////////////////////////////
//CONSOLE DISPLAY NEED///////////
/////////////////////////////////
/*
 * 

                int width = SC_Console.consoleWidth();
                int height = SC_Console.consoleHeight();

                int xPos = SC_Console.cursorLeft();
                int yPos = SC_Console.cursorTop();

                if (consoleEffectFrame00 >= 1000)
                {
                    lastXPos = xPos;

                    if (xPos < (width/2) && startSomething == false)
                    {
                        SC_Console.WriteAt("0", consoleEffectFrame01, 10, false, 0);                                   
                    }
                    else
                    {
                        consoleEffectFrame01 = 0;
                        startSomething = true;
                    }
                    consoleEffectFrame01++;
                    consoleEffectFrame00 = 0;
                }
*/
/////////////////////////////////
/////////////////////////////////
/////////////////////////////////










/*
const ConsoleColor HERO_COLOR = ConsoleColor.DarkBlue;
const ConsoleColor BACKGROUND_COLOR = ConsoleColor.Green;

public static Coordinate Hero { get; set; } //Will represent our here that's moving around :P/>

static void Main(string[] args)
{
    InitGame();

    ConsoleKeyInfo keyInfo;
    while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                MoveHero(0, -1);
                break;

            case ConsoleKey.RightArrow:
                MoveHero(1, 0);
                break;

            case ConsoleKey.DownArrow:
                MoveHero(0, 1);
                break;

            case ConsoleKey.LeftArrow:
                MoveHero(-1, 0);
                break;
        }
    }
}

/// <summary>
/// Paint the new hero
/// </summary>
static void MoveHero(int x, int y)
{
    Coordinate newHero = new Coordinate()
    {
        X = Hero.X + x,
        Y = Hero.Y + y
    };

    if (CanMove(newHero))
    {
        RemoveHero();

        Console.BackgroundColor = HERO_COLOR;
        Console.SetCursorPosition(newHero.X, newHero.Y);
        Console.Write(" ");

        Hero = newHero;
    }
}

/// <summary>
/// Overpaint the old hero
/// </summary>
static void RemoveHero()
{
    Console.BackgroundColor = BACKGROUND_COLOR;
    Console.SetCursorPosition(Hero.X, Hero.Y);
    Console.Write(" ");
}

/// <summary>
/// Make sure that the new coordinate is not placed outside the
/// console window (since that will cause a runtime crash
/// </summary>
static bool CanMove(Coordinate c)
{
    if (c.X < 0 || c.X >= Console.WindowWidth)
        return false;

    if (c.Y < 0 || c.Y >= Console.WindowHeight)
        return false;

    return true;
}

/// <summary>
/// Paint a background color
/// </summary>
/// <remarks>
/// It is very important that you run the Clear() method after
/// changing the background color since this causes a repaint of the background
/// </remarks>
static void SetBackgroundColor()
{
    Console.BackgroundColor = BACKGROUND_COLOR;
    Console.Clear(); //Important!
}

/// <summary>
/// Initiates the game by painting the background
/// and initiating the hero
/// </summary>
static void InitGame()
{
    SetBackgroundColor();

    Hero = new Coordinate()
    {
        X = 0,
        Y = 0
    };

    MoveHero(0, 0);

}
    }

    /// <summary>
    /// Represents a map coordinate
    /// </summary>
    class Coordinate
{
    public int X { get; set; } //Left
    public int Y { get; set; } //Top
}*/
