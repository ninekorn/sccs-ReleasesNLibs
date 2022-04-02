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
using System.Windows.Threading;

using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;


namespace SC_SkYaRk_VR_Editionv002
{
    public  class SC_Console
    {
        [DllImport(@"kernel32.dll", SetLastError = true)]
         static extern bool AllocConsole();

        [DllImport(@"kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport(@"user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        const int SwHide = 0;
        const int SwShow = 5;

        const int ENABLE_MOUSE_INPUT = 0x0010;

        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        const int STD_INPUT_HANDLE = -10;
        const uint ENABLE_QUICK_EDIT = 0x0040;

        public  uint _originalConsoleModeWithMouseInput;
        public  uint _originalConsoleModeWithoutMouseInput;
        public  uint _modifiedConsoleMode;

        public  object[][] _DoWork_MainTaskCheck(object[][] _received_object)
        {
            return _received_object;
        }

        public object _DoWork_hasTaskWorked(object _received_object) //async Task 
        {
        _taskLooper:

            SC_SkYaRk_VR_Editionv002.Program._someObject _data0;
            if (_received_object is Program._someObject)
            {
                _data0 = (Program._someObject)_received_object;
                int _received_switch0 = _data0._received_switch0;
                int _sending_switch0 = _data0._sending_switch0;
                object[][] _chain_Of_Tasks0 = _data0._chain_Of_Tasks0;
                int _timeOut0 = _data0._timeOut0;
                int _ParentTaskThreadID0 = _data0._ParentTaskThreadID0;
                int _main_cpu_count0 = _data0._main_cpu_count;
                
                if (_received_switch0 == 0 && _sending_switch0 == 0)
                {
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("hook on task" + _hookOnTask, 1, 20);
                    //System.Windows.MessageBox.Show("tester","Console");
                    //SC_SkYaRk_VR_Editionv002.Program._someObject _data00;

                    //_data00 = (Program._someObject)_ResultsOfTasks1;
                    int _received_switch00 = _ResultsOfTasks1._received_switch0;
                    int _sending_switch00 = _ResultsOfTasks1._sending_switch0;
                    object[][] _chain_Of_Tasks00 = _ResultsOfTasks1._chain_Of_Tasks0;
                    int _timeOut00 = _ResultsOfTasks1._timeOut0;
                    int _ParentTaskThreadID00 = _ResultsOfTasks1._ParentTaskThreadID0;
                    int _main_cpu_count00 = _ResultsOfTasks1._main_cpu_count;

                    //consoleMessageQueue _consoleMessageQueue000 = new consoleMessageQueue("tester", 1, 20);
                    if (_received_switch00 == 1 && _sending_switch00 == 1)
                    {
                        _hookOnTask++;
                           consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("hook on task" + _hookOnTask, 1, 20);
                    }
                    else
                    {
                        _hookOnTaskNot++;
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("no data" + _hookOnTaskNot, 0, 19);
                    }

                    /*if (_ResultsOfTasks1 is Program._someObject) // Cast 2.
                    {
                        _data00 = (Program._someObject)_ResultsOfTasks1;
                        int _received_switch00 = _data00._received_switch0;
                        int _sending_switch00 = _data00._sending_switch0;
                        object[][] _chain_Of_Tasks00 = _data00._chain_Of_Tasks0;
                        int _timeOut00 = _data00._timeOut0;
                        int _ParentTaskThreadID00 = _data00._ParentTaskThreadID0;
                        int _main_cpu_count00 = _data00._main_cpu_count;

                        //consoleMessageQueue _consoleMessageQueue000 = new consoleMessageQueue("tester", 1, 20);
                        if (_received_switch00 == 1 && _sending_switch00 == 1)
                        {
                            consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("hook on task", 1, 20);
                        }
                        else
                        {
                            consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("no data", 0, 20);
                        }
                    }*/
                }
            }
            //await Task.Delay(_DoWork_MainTask_timeOut);
            Thread.Sleep(1);
            goto _taskLooper;
        }
        int _hookOnTask = 0;
        int _hookOnTaskNot = 0;



        Task _currentTask;
        Task _SC_Console_Task;
        object _ResultsOfTasks0;
        Program._someObject _ResultsOfTasks1;

        object _sending_object0;
        object[] _currentTask_ArrayOfChain;

        public SC_Console(object _main_object)
        {
            SC_SkYaRk_VR_Editionv002.Program._someObject _data0;
            if (_main_object is Program._someObject)
            {
                //System.Windows.MessageBox.Show("000is NOT null", "Console");
                _data0 = (Program._someObject)_main_object;
                int _received_switch0 = _data0._received_switch0;
                int _sending_switch0 = _data0._sending_switch0;
                object[][] _chain_Of_Tasks0 = _data0._chain_Of_Tasks0;
                int _timeOut0 = _data0._timeOut0;
                int _ParentTaskThreadID0 = _data0._ParentTaskThreadID0;
                int _main_cpu_count0 = _data0._main_cpu_count;

                var handle = GetConsoleWindow();
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
                    //Console.WriteLine("null GetConsoleMode");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("null GetConsoleMode", 0, 0);
                }
                else
                {
                    //Console.WriteLine("not null GetConsoleMode");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("not null GetConsoleMode", 0, 0);
                }

                _modifiedConsoleMode = _originalConsoleModeWithMouseInput;

                _modifiedConsoleMode &= ~ENABLE_QUICK_EDIT;

                _originalConsoleModeWithoutMouseInput = _modifiedConsoleMode;

                // set the new mode
                if (!SetConsoleMode(consoleHandle, _modifiedConsoleMode))
                {
                    // ERROR: Unable to set console mode
                    //Console.WriteLine("null SetConsoleMode");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("null SetConsoleMode", 0, 0);
                }
                else
                {
                    ///Console.WriteLine("not null SetConsoleMode");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("not null SetConsoleMode", 0, 0);
                }
                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Console", 0, 0);

                //_currentTask = _DoWork_Tester(_main_object);
                Task<object> task0 = null;
                task0 = Task<object>.Factory.StartNew(() =>
                {
                    _ResultsOfTasks0 = _DoWork_hasTaskWorked(_main_object);

                    return _ResultsOfTasks0;
                });

                System.Windows.MessageBox.Show("is NOT null", "Console");
            }
            else
            {
                System.Windows.MessageBox.Show("is null", "Console");
            }

            /*SC_SkYaRk_VR_Editionv002.Program._someObject testing;

            testing = (SC_SkYaRk_VR_Editionv002.Program._someObject)_main_object;

            int _received_switch1 = testing._received_switch0;
            int _sending_switch1 = testing._sending_switch0;
            object[][] _chain_Of_Tasks1 = testing._chain_Of_Tasks0;
            int _timeOut1 = testing._timeOut0;
            int _ParentTaskThreadID1 = testing._ParentTaskThreadID0;
            int _main_cpu_count1 = testing._main_cpu_count;

            if (_chain_Of_Tasks0.Length > 0)
            {

            }
            else
            {

            }*/

            /*_sending_object0 = new object();


            _sending_object0._received_switch0 = _received_switch0;


            int _received_switch0 = ;
            int _sending_switch0 = _sending_object0._sending_switch0;
            object[][] _chain_Of_Tasks0 = _sending_object0._chain_Of_Tasks0;
            int _timeOut0 = _sending_object0._timeOut0;
            int _ParentTaskThreadID0 = _sending_object0._ParentTaskThreadID0;
            int _main_cpu_count0 = _sending_object0._main_cpu_count;

            _sending_object0[0] = 
            */

            //System.Reflection.PropertyInfo[] _somePropInfo = _main_object.GetType().GetProperties();
            //obj.GetType().GetMethod("MyFunction").Invoke(obj, null);






            /*int _received_switch01 = (int)_sending_object0[0][0];
            int _sending_switch01 = (int)_sending_object0[1][0];
            object[][] _chain_Of_Tasks01 = (object[][])_sending_object0[2][0];
            int _timeOut01 = (int)_sending_object0[3][0];
            int _ParentTaskThreadID01 = (int)_sending_object0[4][0];

            if (_chain_Of_Tasks0.Length > 0)
            {
                _sending_object0 = new object[5][];
                _sending_object0[0][0] = 0;
                _sending_object0[1][0] = 0;
                _currentTask_ArrayOfChain = new object[1];
                _sending_object0[2][1] = _currentTask_ArrayOfChain;
                _currentTask = _DoWork_Tester(_sending_object0);

            }
            else
            {
                _chain_Of_Tasks0 = new object[1][];
                _chain_Of_Tasks0[0][0] = _SC_Console_Task;
                _currentTask = _DoWork_Tester(_sending_object0);
            }*/

            /*SC_SkYaRk_VR_Editionv002.Program._someObject _data00;
            if (_data00 is Program._someObject) // Cast 2.
            {
                _data0 = (Program._someObject)_main_object;
                int _received_switch0 = _data0._received_switch0;
                int _sending_switch0 = _data0._sending_switch0;
                object[][] _chain_Of_Tasks0 = _data0._chain_Of_Tasks0;
                int _timeOut0 = _data0._timeOut0;
                int _ParentTaskThreadID0 = _data0._ParentTaskThreadID0;
                int _main_cpu_count0 = _data0._main_cpu_count;
            }*/



            SC_SkYaRk_VR_Editionv002.Program._someObject _data00;
            if (_main_object is Program._someObject) // Cast 2.
            {
                _data00 = (Program._someObject)_main_object;
                int _received_switch0 = _data00._received_switch0;
                int _sending_switch0 = _data00._sending_switch0;
                object[][] _chain_Of_Tasks0 = _data00._chain_Of_Tasks0;
                int _timeOut0 = _data00._timeOut0;
                int _ParentTaskThreadID0 = _data00._ParentTaskThreadID0;
                int _main_cpu_count0 = _data00._main_cpu_count;

                int _InitTaskDispatch = 1;
                int _totalThreads = _main_cpu_count0;

                if (_InitTaskDispatch == 1)
                {
                    for (int j = 0; j < _main_cpu_count0; j++)
                    {
                        int timeOut = 1;

                        try
                        {
                            //_SCConsole_Create(_sending_object);
                            Task task0 = null;
                            task0 = Task.Factory.StartNew(() =>
                            {
                                while (true)
                                {
                                    _ResultsOfTasks1 = DoWork_MainTask(_main_object);
                                    //System.Windows.MessageBox.Show("Thread is Alive 00", "Console");
                                    Thread.Sleep(1);
                                }
                            });
                            //_tasksToKill.Add(someObject);
                        }
                        catch
                        {

                        }
                    }
                    _InitTaskDispatch = 0;
                }
            }
        }


      


        public  void _SCConsole_Create(object[][] _received_object)
        {
            //_InitConsole(_received_object);

            /*int _received_switch0 = (int)_received_object[0][0];
            int _sending_switch0 = (int)_received_object[1][0];
            object[][] _chain_Of_Tasks0 = (object[][])_received_object[2][0];
            int _timeOut0 = (int)_received_object[3][0];
            int _ParentTaskThreadID0 = (int)_received_object[4][0];

            if (_chain_Of_Tasks0.Length > 0)
            {
                _sending_object0 = new object[5][];
                _sending_object0[0][0] = 0;
                _sending_object0[1][0] = 0;
                _currentTask_ArrayOfChain = new object[1];
                _sending_object0[2][1] = _currentTask_ArrayOfChain;

                _currentTask = _DoWork_Tester(_sending_object0);
            }
            else
            {
                _chain_Of_Tasks0 = new object[1][];
                _chain_Of_Tasks0[0][0] = _SC_Console_Task;


                _currentTask = _DoWork_Tester(_sending_object0);
            }*/

            /*Task<object[][]> task0 = Task<object[][]>.Factory.StartNew(() =>
            {
            _taskLooper:


                object[][] resultsOfTask = _DoWork_MainTaskCheck(_received_object);

                await Task.Delay(_DoWork_MainTask_timeOut);
                goto _taskLooper;

                return resultsOfTask;
            });*/






            /*int _received_switch = (int)_received_object[0][0];
            int _sending_switch = (int)_received_object[1][0];
            object[][] _chain_Of_Tasks = (object[][])_received_object[2][0];
            int _timeOut = (int)_received_object[3][0];
            int _ParentTaskThreadID = (int)_received_object[4][0];

            if (_chain_Of_Tasks.Length > 0)
            {
                object[][] _sending_object = new object[5][];
                _sending_object[0][0] = -1;
                _sending_object[1][0] = -1;
                object[] _currentTask_ArrayOfChain = new object[1];
                _sending_object[2][1] = _currentTask_ArrayOfChain;

                Task<object[][]> task3 = Task<object[][]>.Factory.StartNew(() =>
                {
                    _ResultsOfTask = DoWork_MainTask(_sending_object);

                    return _ResultsOfTask;
                });

                //data returned from creation of the task = task3.Result;
                /*foreach (var name in task3.Result)
                {
                    //Console.WriteLine(name);
                    consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue(name, 10, 30);

                }*/




                //_sending_object[3][0] = _timeOut; // will change it later


                /*int _total_chain_length = _chain_Of_Tasks.Length;

                if (_total_chain_length > 1)
                {

                }
                else
                {

                }*/




                /*int _total_chain_length = _chain_Of_Tasks.Length;

                for (int _tC0 = 0; _tC0 < _total_chain_length; _tC0++)
                {

                }*/

                /*for (int _tC0 = 0; _tC0 < _chain_Of_Tasks.Length; _tC0++)
                {
                    object[][] _sending_object = new object[4][];
                    _sending_object[0][0] = 1;
                    _sending_object[1][0] = 1;


                    //_sending_object[2][0]


                    /*_sending_object[0][0] = 1;
                    _sending_object[1][0] = 0;
                    _sending_object[2][0] = _mainTask;
                    _sending_object[3][0] = timeOut;
                    _sending_object[4][0] = j;
                    _SC_Console_Task = DoWork_MainTask(_received_object);
                    _chain_Of_Tasks = (object[][])_received_object[2][0];
                }
            }
            else
            {
                _chain_Of_Tasks = new object[1][];
                _chain_Of_Tasks[0][0] = _SC_Console_Task;
            }*/










            int timeOut = 1;

            try
            {
                /*int _received_switch = (int)_received_object[0][0];
                int _sending_switch = (int)_received_object[1][0];
                object[][] _chain_Of_Tasks = (object[][])_received_object[2][0];
                int _timeOut = (int)_received_object[3][0];
                int _ParentTaskThreadID = (int)_received_object[4][0];
                */
                /*if (_chain_Of_Tasks.Length > 0)
                {
                    object[][] _sending_object = new object[5][];
                    _sending_object[0][0] = -1;
                    _sending_object[1][0] = -1;
                    object[] _currentTask_ArrayOfChain = new object[1];
                    _sending_object[2][1] = _currentTask_ArrayOfChain;    

                    Task<object[][]> task3 = Task<object[][]>.Factory.StartNew(() =>
                    {
                        _ResultsOfTask = DoWork_MainTask(_sending_object);

                        return _ResultsOfTask;
                    });

                    //data returned from creation of the task = task3.Result;
                    //foreach (var name in task3.Result)
                    //{
                        //Console.WriteLine(name);
                    //    consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue(name, 10, 30);

                    //}




                    //_sending_object[3][0] = _timeOut; // will change it later


                    /*int _total_chain_length = _chain_Of_Tasks.Length;

                    if (_total_chain_length > 1)
                    {

                    }
                    else
                    {

                    }*/




                    /*int _total_chain_length = _chain_Of_Tasks.Length;

                    for (int _tC0 = 0; _tC0 < _total_chain_length; _tC0++)
                    {

                    }*/

                    /*for (int _tC0 = 0; _tC0 < _chain_Of_Tasks.Length; _tC0++)
                    {
                        object[][] _sending_object = new object[4][];
                        _sending_object[0][0] = 1;
                        _sending_object[1][0] = 1;


                        //_sending_object[2][0]


                        /*_sending_object[0][0] = 1;
                        _sending_object[1][0] = 0;
                        _sending_object[2][0] = _mainTask;
                        _sending_object[3][0] = timeOut;
                        _sending_object[4][0] = j;
                        _SC_Console_Task = DoWork_MainTask(_received_object);
                        _chain_Of_Tasks = (object[][])_received_object[2][0];
                    }
                }
                else
                {
                    _chain_Of_Tasks = new object[1][];
                    _chain_Of_Tasks[0][0] = _SC_Console_Task;
                }*/


                /*for (int i = 0; i < _chain_Of_Tasks.Length;i++)
                {

                }
                _currentTask = DoWork_MainTask(_received_object);
                */



                /*object[] someObject = new object[4];
                someObject[0] = _mainTask;
                someObject[1] = j;
                someObject[2] = timeOut;
                //someObject[3] = _TME;*/

                //SC_SkYaRk_VR_Editionv002.Program._tasksToKill.Add(someObject);
            }
            catch
            {

            }
            /*for (int j = 0; j < _totalThreads; j++)
            {
                int timeOut = 1;

                try
                {
                    object[] _sending_object = new object[4];
                    _sending_object[0] = 1;
                    _sending_object[1] = 0;
                    _sending_object[2] = _mainTask;
                    _sending_object[3] = timeOut;
                    _sending_object[4] = j;

                    _mainTask = DoWork_MainTask(_sending_object);

                    object[] someObject = new object[4];
                    someObject[0] = _mainTask;
                    someObject[1] = j;
                    someObject[2] = timeOut;
                    //someObject[3] = _TME;

                    _tasksToKill.Add(someObject);
                }
                catch
                {

                }
            }*/
            //_InitConsole(_received_object);
        }

        public static void _InitConsole(object[] _received_object)
        {
            var handle = GetConsoleWindow();
            Console.WriteLine("test");
            System.Windows.MessageBox.Show("ptr:" + handle, "test");


            /*for (int j = 0; j < _totalThreads; j++)
            {
                int timeOut = 1;

                try
                {
                    object[] _sending_object = new object[4];
                    _sending_object[0] = 1;
                    _sending_object[1] = 0;
                    _sending_object[2] = _mainTask;
                    _sending_object[3] = timeOut;
                    _sending_object[4] = j;

                    _mainTask = DoWork_MainTask(_sending_object);

                    object[] someObject = new object[4];
                    someObject[0] = _mainTask;
                    someObject[1] = j;
                    someObject[2] = timeOut;
                    //someObject[3] = _TME;

                    _tasksToKill.Add(someObject);
                }
                catch
                {

                }
            }*/

            //await(new Func<object, Task>(async (p) => await Task.Run(() => DoWork_MainTask(_received_object)))).Invoke(_received_object);


            /*if (handle != IntPtr.Zero)
            {
                consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("-OculusPTR- " + handle, 0, 0);
            }
            else
            {
                consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("-OculusPTR- " + "null or zero", 0, 0);
            }*/
        }

         ThreadStart threadOne;
         Task _DoWork_MainTask_Task;
        int _DoWork_MainTask_timeOut = 0;
         int _DoWork_MainTask_threadID;


         int _counterTaskIsAlive = 0;
        static SC_SkYaRk_VR_Editionv002.Program._someObject _data00;
        public Program._someObject DoWork_MainTask(object _received_object) //async Task 
        {
            /*SC_SkYaRk_VR_Editionv002.Program._someObject _data00;
            if (_received_object is Program._someObject) // Cast 2.
            {
                _data00 = (Program._someObject)_received_object;
                int _received_switch0 = _data00._received_switch0;
                int _sending_switch0 = _data00._sending_switch0;
                object[][] _chain_Of_Tasks0 = _data00._chain_Of_Tasks0;
                int _timeOut0 = _data00._timeOut0;
                int _ParentTaskThreadID0 = _data00._ParentTaskThreadID0;
                int _main_cpu_count0 = _data00._main_cpu_count;

                int _InitTaskDispatch = 1;
                int _totalThreads = _main_cpu_count0;

                if (_InitTaskDispatch == 1)
                {
                    for (int j = 0; j < _main_cpu_count0; j++)
                    {
                        int timeOut = 1;

                        try
                        {
                            //_SCConsole_Create(_sending_object);
                            Task<object> task0 = null;
                            task0 = Task<object>.Factory.StartNew(() =>
                            {
                                _ResultsOfTask = DoWork_MainTask(_received_object);
                                return _ResultsOfTask;
                            });
                            //_tasksToKill.Add(someObject);
                        }
                        catch
                        {

                        }
                    }
                    _InitTaskDispatch = 0;
                }
            }*/

            //_threadLooper:


            if (_received_object is Program._someObject) // Cast 2.
            {
                _ResultsOfTasks1 = new Program._someObject();
                _ResultsOfTasks1._received_switch0 = 1;// _data00._received_switch0;
                _ResultsOfTasks1._sending_switch0 = 1;// _data00._sending_switch0;
                _ResultsOfTasks1._chain_Of_Tasks0 = _data00._chain_Of_Tasks0;
                _ResultsOfTasks1._timeOut0 = _data00._timeOut0;
                _ResultsOfTasks1._ParentTaskThreadID0 = _data00._ParentTaskThreadID0;
                _ResultsOfTasks1._main_cpu_count = _data00._main_cpu_count;
                //_data00 = (Program._someObject)_received_object;
                //_data00 = (Program._someObject)_received_object;
            }

            lock (_received_object)
            {
                lock (_someQueue)
                {
                    if (_someQueue.Count > 0)
                    {                  
                       
                        object[] test = _someQueue[0];
                        test[0] = _someQueue[0][0];
                        test[1] = _someQueue[0][1];
                        test[2] = _someQueue[0][2];

                        //if (_lastConsoleMessage != (string)test[0])
                        //{
                        //    dispatchConsoleCommands(test);
                        //}
                        dispatchConsoleCommands(test);
                        _someQueue.Remove(_someQueue[0]);
                    }
                }
            }
            if (_counterTaskIsAlive > 100)
            {
                //System.Windows.MessageBox.Show("Thread is Alive 00", "Console");
                /*int _received_switch01 = (int)_received_object[0][0];
                int _sending_switch01 = (int)_received_object[1][0];
                object[][] _chain_Of_Tasks01 = (object[][])_received_object[2][0];
                int _timeOut01 = (int)_received_object[3][0];
                int _ParentTaskThreadID01 = (int)_received_object[4][0];

                //SC_Console.consoleMessageQueue messageQueue00 = new SC_Console.consoleMessageQueue(_someQueue.Count.ToString(), 10, 1);
                object[][] _sending_object = new object[5][];
                _sending_object[0][0] = _received_switch0;
                _sending_object[1][0] = _sending_switch0;
                _sending_object[2] = _chain_Of_Tasks0;
                _sending_object[3][0] = _timeOut0;
                _sending_object[4][0] = _ParentTaskThreadID0;*/

                //System.Windows.MessageBox.Show("Thread is Alive 00", "Console");

                //consoleMessageQueue messageQueue00 = new consoleMessageQueue("" + _anotherCounter, 20, 0);
    
                _counterTaskIsAlive = 0;
                //someObject[3] = _TME;
            }
            //System.Windows.MessageBox.Show("Thread is Alive", "Console");
            _counterTaskIsAlive++;
            _anotherCounter++;
            //await Task.Delay(1);
            return _ResultsOfTasks1;
            //goto _threadLooper;
        }

        int _anotherCounter = 0;





        /*//await Task.Delay(_timeOut0);
        int _received_switch0 = (int)_received_object.;
        int _sending_switch0 = (int)_received_object[1][0];
        object[][] _chain_Of_Tasks0 = (object[][])_received_object[2][0];
        int _timeOut0 = (int)_received_object[3][0];
        int _ParentTaskThreadID0 = (int)_received_object[4][0];
        */
        /*if (_received_switch0 == 1)
        {

        }*/

        //_received_object[0][0] = 0;

        /*if (_counterTaskIsAlive > 10000)
        {

            int _received_switch01 = (int)_received_object[0][0];
            int _sending_switch01 = (int)_received_object[1][0];
            object[][] _chain_Of_Tasks01 = (object[][])_received_object[2][0];
            int _timeOut01 = (int)_received_object[3][0];
            int _ParentTaskThreadID01 = (int)_received_object[4][0];

            //SC_Console.consoleMessageQueue messageQueue00 = new SC_Console.consoleMessageQueue(_someQueue.Count.ToString(), 10, 1);
            object[][] _sending_object = new object[5][];
            _sending_object[0][0] = _received_switch0;
            _sending_object[1][0] = _sending_switch0;
            _sending_object[2] = _chain_Of_Tasks0;
            _sending_object[3][0] = _timeOut0;
            _sending_object[4][0] = _ParentTaskThreadID0;



            _counterTaskIsAlive = 0;
            return _sending_object;
            //someObject[3] = _TME;
        }*/



        //_threadLoop:

        /*if ((int)_received_object[0][0] == 0 && (int)_received_object[1][0] == 0)
        {
            GC.SuppressFinalize(_received_object);
            _DoWork_MainTask_timeOut = 3; //1 is max performance. to be adjusted later
        }
        else
        {

        }*/

        //Thread.Sleep(1);
        //await Task.Yield();
        //_tme.WaitOne();


        /*threadOne = delegate
        {
            threadConsole(totalThreads);
        };
        Thread threaderr = new Thread(threadOne);
        //#pragma warning disable CS0618 // 'Thread.ApartmentState' is obsolete: 'The ApartmentState property has been deprecated.  Use GetApartmentState, SetApartmentState or TrySetApartmentState instead.'
        threaderr.ApartmentState = ApartmentState.STA;
        //#pragma warning restore CS0618 // 'Thread.ApartmentState' is obsolete: 'The ApartmentState property has been deprecated.  Use GetApartmentState, SetApartmentState or TrySetApartmentState instead.'
        threaderr.IsBackground = true;
        threaderr.Start();*/

        /*Task _mainTask = null;
        int timeOut = 3;

        for (int _tC = 0; _tC < 1;_tC++)
        {
            object[] _sending_object = new object[4];
            _sending_object[0] = 1;
            _sending_object[1] = 0;
            _sending_object[2] = _mainTask;
            _sending_object[3] = timeOut;
            _sending_object[4] = _tC;

            _mainTask = DoWork_MainTask(_sending_object);

            object[] someObject = new object[4];
            someObject[0] = _mainTask;
            someObject[1] = _tC;
            someObject[2] = timeOut;
            //someObject[3] = _TME;
            _tasksToKill.Add(someObject);

        }*/

        /*lock (_someQueue)
        {
            if (_someQueue.Count > 0)
            {
                object[] test = _someQueue[0];
                test[0] = _someQueue[0][0];
                test[1] = _someQueue[0][1];
                test[2] = _someQueue[0][2];

                //if (_lastConsoleMessage != (string)test[0])
                //{
                //    dispatchConsoleCommands(test);
                //}
                dispatchConsoleCommands(test);
                _someQueue.Remove(_someQueue[0]);
            }
        }*/
        //await Task.Delay(_DoWork_MainTask_timeOut);
        //goto _threadLoop;


        public static List<object[]> _someQueue = new List<object[]>();
        public class consoleMessageQueue
        {
            public  object[] _someObject;
            public  string _message = "";
            public  int _lineX = 0;
            public  int _lineY = 0;

            public consoleMessageQueue(string message, int lineX, int lineY)
            {
                object[] test = new object[3];
                test[0] = message;
                test[1] = lineX;
                test[2] = lineY;
                _someQueue.Add(test);
            }
        }
        //int mainFrameCounter = 0;
        public  void threadConsole(int totalThreads)
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
                        _someQueue.Remove(_someQueue[0]);
                    }
                }

                //mainFrameCounter++;
                //GC.Collect();
                Thread.Sleep(1);
                goto threadTester;
            }
            catch
            {

            }
        }

         int _xCurrentCursorPos = 0;
         int _yCurrentCursorPos = 0;
        public  void dispatchConsoleCommands(object[] someConsoleData)
        {
            string consoleMessage = (string)someConsoleData[0];
            int _targetLineX = (int)someConsoleData[1];
            int _targetLineY = (int)someConsoleData[2];

            _xCurrentCursorPos = cursorLeft();
            _yCurrentCursorPos = cursorTop();

            Console.SetCursorPosition(0, _yCurrentCursorPos);
            Console.SetCursorPosition(0, _targetLineY);
            Console.SetCursorPosition(_targetLineX, _targetLineY);

            Console.Write(consoleMessage);


            //"▬" gives out a Question Mark
             
            //~←∟↓→§▬↨↓→╕╕╣╖╢╡┤│▓░○▒»«¡¼½
        }

        //"ALT1020++"g☺☺}☺☻♥♦♣♠≈•◘○◙♂♀♪
        //ⁿ²■☺☻☺




        public  void HideConsoleWindow()
        {
            //var handle = GetConsoleWindow();
            //ShowWindow(handle, SwHide);
        }

        public  int numberOfLinePass = 0;

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


         int canResetCursor = 0;
        public   void WriteAt(string s, int x, int y, bool canPassLine, int linePassNumber)
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
            canResetCursor = 1;
        }

    }
}
