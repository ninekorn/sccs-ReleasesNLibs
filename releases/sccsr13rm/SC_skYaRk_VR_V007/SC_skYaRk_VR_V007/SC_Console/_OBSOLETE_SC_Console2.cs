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
        public static List<object[]> _someQueue = new List<object[]>();
        public static List<Program._someObject> _dispatchQueue_has_Main_Responded = new List<Program._someObject>();
        public static List<Program._someObject> _dispatchQueue_has_Sec_Responded = new List<Program._someObject>();
        public static List<object> _dispatchQueue = new List<object>();

        int _0Swtc = 0;
        public static string name;

        public object _DoWork_hasTaskWorked(object _received_object) //async Task 
        {
            SC_SkYaRk_VR_Editionv002.Program._someObject _data0;

            if (_received_object is Program._someObject)
            {
                _data0 = (Program._someObject)_received_object;
                int _received_switch_in0 = _data0._received_switch_in;
                int _received_switch_out0 = _data0._received_switch_out;
                int _sending_switch_in0 = _data0._sending_switch_in;
                int _sending_switch_out0 = _data0._sending_switch_out;
                object[][] _chain_Of_Tasks0 = _data0._chain_Of_Tasks0;
                int _timeOut0 = _data0._timeOut0;
                int _ParentTaskThreadID0 = _data0._ParentTaskThreadID0;
                int _main_cpu_count0 = _data0._main_cpu_count;

                if (_received_switch_in0 == 1 &&
                    _received_switch_out0 == 0 &&
                    _sending_switch_in0 == 0 &&
                    _sending_switch_out0 == 0)
                {
                    consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("What is your name: ", 0, 0);

                    //programStart
                    name = Console.ReadLine();

                    if (name != "")
                    {
                        //consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("Thank you: ", 0, 2);
                        _data0._sending_switch_in = 1;
                        //_dispatchQueue_has_Main_Responded.Add(_data0);                           
                        _ResultsOfTasks1 = _DoWork_MainTask(_data0);
                    }
                }
            }


        _taskLooper:

            lock (_dispatchQueue_has_Sec_Responded)
            {
                if (_dispatchQueue_has_Sec_Responded.Count > 0)
                {
                    _data0 = _dispatchQueue_has_Sec_Responded[0];

                    int _received_switch_in00 = _data0._received_switch_in;   //1
                    int _received_switch_out00 = _data0._received_switch_out; //0
                    int _sending_switch_in00 = _data0._sending_switch_in;     //0
                    int _sending_switch_out00 = _data0._sending_switch_out;   //0
                    object[][] _chain_Of_Tasks00 = _data0._chain_Of_Tasks0;
                    int _timeOut00 = _data0._timeOut0;
                    int _ParentTaskThreadID00 = _data0._ParentTaskThreadID0;
                    int _main_cpu_count00 = _data0._main_cpu_count;

                    //THIS FIRST SWITCH IS TO INITIALIZE THE PROGRAM. AT LEAST LETS START WITH THAT.
                    if (_received_switch_in00 == 1 &&
                        _received_switch_out00 == 0 &&
                        _sending_switch_in00  == 1 &&
                        _sending_switch_out00 == 1)
                    {
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Choose VR", 0, 2);

                        //programStart
                        name = Console.ReadLine();

                        if (name.ToLower() == "yes" || name.ToLower() == "y")
                        {
                            consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("entering VR", 0, 3);
                            _data0._sending_switch_in = 1;
                            //_dispatchQueue_has_Main_Responded.Add(_data0);                           
                            _ResultsOfTasks1 = _DoWork_MainTask(_data0);
                        }
                    }
                }
            }

            if (_dispatchQueue.Count > 0)
            {
                if (_dispatchQueue[0] is Program._someObject)
                {
                    _data0 = (Program._someObject)_dispatchQueue[0];

                    int _received_switch_in00 = _data0._received_switch_in;   //1
                    int _received_switch_out00 = _data0._received_switch_out; //0
                    int _sending_switch_in00 = _data0._sending_switch_in;     //0
                    int _sending_switch_out00 = _data0._sending_switch_out;   //0
                    object[][] _chain_Of_Tasks00 = _data0._chain_Of_Tasks0;
                    int _timeOut00 = _data0._timeOut0;
                    int _ParentTaskThreadID00 = _data0._ParentTaskThreadID0;
                    int _main_cpu_count00 = _data0._main_cpu_count;

                    //THIS FIRST SWITCH IS TO INITIALIZE THE PROGRAM. AT LEAST LETS START WITH THAT.
                    if (_received_switch_in00 == 1 &&
                        _received_switch_out00 == 0 &&
                        _sending_switch_in00 == 1 &&
                        _sending_switch_out00 == 1)
                    {
                        //_data0._received_switch_in = 0;
                        //System.Windows.MessageBox.Show("testing", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("testing", 0, 11);
                    }
                }
            }

            lock (_dispatchQueue_has_Sec_Responded)
            {
                if (_dispatchQueue_has_Sec_Responded.Count > 0)
                {
                    _data0 = _dispatchQueue_has_Sec_Responded[0];

                    int _received_switch_in00 = _data0._received_switch_in;   //1
                    int _received_switch_out00 = _data0._received_switch_out; //0
                    int _sending_switch_in00 = _data0._sending_switch_in;     //0
                    int _sending_switch_out00 = _data0._sending_switch_out;   //0
                    object[][] _chain_Of_Tasks00 = _data0._chain_Of_Tasks0;
                    int _timeOut00 = _data0._timeOut0;
                    int _ParentTaskThreadID00 = _data0._ParentTaskThreadID0;
                    int _main_cpu_count00 = _data0._main_cpu_count;

                    //THIS FIRST SWITCH IS TO INITIALIZE THE PROGRAM. AT LEAST LETS START WITH THAT.
                    if (_received_switch_in00 == 1 &&
                       _received_switch_out00 == 0 &&
                         _sending_switch_in00 == 1 &&
                        _sending_switch_out00 == 1)
                    {

                    }
                    /*else if (_received_switch_in00 == 1 &&
                            _received_switch_out00 == 1 &&
                              _sending_switch_in00 == 1 &&
                             _sending_switch_out00 == 1)
                    {

                    }*/
                }
            }




                /*else if ()
                {
                    if (_received_switch_in0 == 1 &&
                        _received_switch_out0 == 0 &&
                        _sending_switch_in0 == 0 &&
                        _sending_switch_out0 == 0)
                    {
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("What is your name: ", 0, 0);

                        //programStart
                        name = Console.ReadLine();

                        if (name != "")
                        {
                            _data0._sending_switch_in = 1;
                            _dispatchQueue_has_Main_Responded.Add(_data0);
                        }
                    }
                }*/






            //_dispatchQueue_has_Main_Responded.Add()



            /*if (_received_switch0 == 1 && _sending_switch0 == 1)
            {

                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("hook on task" + _hookOnTask, 1, 20);
                //System.Windows.MessageBox.Show("tester","Console");
                //SC_SkYaRk_VR_Editionv002.Program._someObject _data00;

                //_data00 = (Program._someObject)_ResultsOfTasks1;
                /*int _received_switch00 = _ResultsOfTasks1._received_switch0;
                int _sending_switch00 = _ResultsOfTasks1._sending_switch0;
                object[][] _chain_Of_Tasks00 = _ResultsOfTasks1._chain_Of_Tasks0;
                int _timeOut00 = _ResultsOfTasks1._timeOut0;
                int _ParentTaskThreadID00 = _ResultsOfTasks1._ParentTaskThreadID0;
                int _main_cpu_count00 = _ResultsOfTasks1._main_cpu_count;


                //consoleMessageQueue _consoleMessageQueue000 = new consoleMessageQueue("tester", 1, 20);
                if (_received_switch00 == 1 && _sending_switch00 == 1)
                {
                    //_hookOnTask++;
                       consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("1/1", 0, 11);
                }
                else if (_received_switch00 == 0 && _sending_switch00 == 1)
                {
                    //_hookOnTaskNot++;
                    consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("0/1", 0, 12);
                }
                else if (_received_switch00 == 1 && _sending_switch00 == 0)
                {
                    //_hookOnTaskNot++;
                    consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("1/0", 0, 13);
                }
                else if (_received_switch00 == 0 && _sending_switch00 == 0)
                {
                    //_hookOnTaskNot++;
                    consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("0/0", 0, 14);
                }*/
            /*if (_ResultsOfTasks1 is Program._someObject)
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
            }
        }
        }*/
            //await Task.Delay(_DoWork_MainTask_timeOut);
            Thread.Sleep(1);
            goto _taskLooper;
        }
        object _ResultsOfTasks0;
        Program._someObject _ResultsOfTasks1;

        public SC_Console(object _main_object)
        {

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

            System.Windows.MessageBox.Show("is NOT null", "Console");
            /////////////////////////////////////////////////////////
            ////////////////////CONSOLE CREATED//////////////////////
            /////////////////////////////////////////////////////////


            /////////////////////////////////////////////////////////
            ///////////CREATING TASKS FOR FAST WORKFLOW//////////////
            /////////////////////////////////////////////////////////
            SC_SkYaRk_VR_Editionv002.Program._someObject _data0;
            if (_main_object is Program._someObject)
            {
                //System.Windows.MessageBox.Show("000is NOT null", "Console");
                _data0 = (Program._someObject)_main_object;
                int _received_switch_in = _data0._received_switch_in;   //1
                int _received_switch_out = _data0._received_switch_out; //0
                int _sending_switch_in = _data0._sending_switch_in;     //0
                int _sending_switch_out = _data0._sending_switch_out;   //0
                object[][] _chain_Of_Tasks0 = _data0._chain_Of_Tasks0;
                int _timeOut0 = _data0._timeOut0;
                int _ParentTaskThreadID0 = _data0._ParentTaskThreadID0;
                int _main_cpu_count0 = _data0._main_cpu_count;

                //THIS FIRST SWITCH IS TO INITIALIZE THE PROGRAM. AT LEAST LETS START WITH THAT.
                if (_received_switch_in == 1 &&
                    _received_switch_out == 0 &&
                    _sending_switch_in == 0 &&
                    _sending_switch_out == 0)
                {
                    //FIRST TASK
                    //1. used for capturing input of user in console and switching stuff.
                    //2. used for sending the message back to the console based on the response.
                    //1. but im not sure yet about capturing this thread "results" of task
                    Task<object> task0 = null;
                    task0 = Task<object>.Factory.StartNew(() =>
                    {
                        _ResultsOfTasks0 = _DoWork_hasTaskWorked(_main_object);

                        return _ResultsOfTasks0;
                    });

                    _data0._received_switch_out = 0;
                    _dispatchQueue_has_Main_Responded.Add(_data0);
                    _data0._received_switch_out = 0;


                    //SECOND TASK
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
                                Task task00 = null;
                                task00 = Task.Factory.StartNew(() =>
                                {
                                    //int sometest = 0;
                                    while (true)
                                    {
                                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("" + sometest, 0, 3);
                                        _ResultsOfTasks1 = _DoWork_MainTask(_main_object);
                                        //System.Windows.MessageBox.Show("Thread is Alive 00", "Console");
                                        Thread.Sleep(1);
                                        
                                        
                                        //sometest++;
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



            /*SC_SkYaRk_VR_Editionv002.Program._someObject _data00;
            if (_main_object is Program._someObject) // Cast 2.
            {
                _data00 = (Program._someObject)_main_object;
                int _received_switch_in = _data00._received_switch_in;
                int _received_switch_out = _data00._received_switch_out;
                int _sending_switch_in = _data00._sending_switch_in;
                int _sending_switch_out = _data00._sending_switch_out;

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
                            Task task00 = null;
                            task00 = Task.Factory.StartNew(() =>
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
            }*/
        }


         int _counterTaskIsAlive = 0;
        static SC_SkYaRk_VR_Editionv002.Program._someObject _data00;
        Program._someObject _data0;
        public Program._someObject _DoWork_MainTask(object _received_object) //async Task 
        {
            _data0 = new Program._someObject();
            if (_received_object is Program._someObject) // Cast 2.
            {
                _data0 = (Program._someObject)_received_object;
                int _received_switch_in = _data0._received_switch_in;   //1
                int _received_switch_out = _data0._received_switch_out; //0
                int _sending_switch_in = _data0._sending_switch_in;     //0
                int _sending_switch_out = _data0._sending_switch_out;   //0
                object[][] _chain_Of_Tasks0 = _data0._chain_Of_Tasks0;
                int _timeOut0 = _data0._timeOut0;
                int _ParentTaskThreadID0 = _data0._ParentTaskThreadID0;
                int _main_cpu_count0 = _data0._main_cpu_count;

                if (_received_switch_in == 1 &&
                    _received_switch_out == 0 &&
                    _sending_switch_in == 1 &&
                    _sending_switch_out == 0)
                {
                    //System.Windows.MessageBox.Show("Thank you", "Console");
                    //System.Windows.MessageBox.Show("Thank you", "Console");

                    //string consoleMessage = (string)someConsoleData[0];
                    //int _targetLineX = (int)someConsoleData[1];
                    //int _targetLineY = (int)someConsoleData[2];

                    /*object[] tester = new object[0];
                    tester[0] = "Thank you! ";
                    tester[1] = 0;
                    tester[2] = 1;

                    _someQueue.Add(tester);*/

                    //dispatchConsoleCommands(tester);

                    object[] test = new object[3];
                    test[0] = "Thank you! " + _thankYouCounter;
                    test[1] = 0;
                    test[2] = 1;
                    _someQueue.Add(test);


                    _data0._sending_switch_out = 1;
                    _dispatchQueue.Add(_data0);
                    _thankYouCounter++;
                    _dispatchQueue_has_Sec_Responded.Add(_data0);
                    //_dispatchQueue_has_Main_Responded.Remove(_dispatchQueue_has_Main_Responded[0]);
                }
                else
                {
                    //_dispatchQueue_has_Main_Responded.Remove(_dispatchQueue_has_Main_Responded[0]);
                }
            }

            /*lock (_dispatchQueue_has_Main_Responded)
            {
                if (_dispatchQueue_has_Main_Responded.Count > 0)
                {
                    //System.Windows.MessageBox.Show("Thread is Alive", "Console");
                    Program._someObject _data0 = _dispatchQueue_has_Main_Responded[0];
                    int _received_switch_in = _data0._received_switch_in;   //1
                    int _received_switch_out = _data0._received_switch_out; //0
                    int _sending_switch_in = _data0._sending_switch_in;     //0
                    int _sending_switch_out = _data0._sending_switch_out;   //0
                    object[][] _chain_Of_Tasks0 = _data0._chain_Of_Tasks0;
                    int _timeOut0 = _data0._timeOut0;
                    int _ParentTaskThreadID0 = _data0._ParentTaskThreadID0;
                    int _main_cpu_count0 = _data0._main_cpu_count;

                    //THIS FIRST SWITCH IS TO INITIALIZE THE PROGRAM. AT LEAST LETS START WITH THAT.
                    if (_received_switch_in == 1 &&
                        _received_switch_out == 0 &&
                        _sending_switch_in == 1 &&
                        _sending_switch_out == 0)
                    {
                        //System.Windows.MessageBox.Show("Thank you", "Console");
                        object[] tester = new object[0];
                        tester[0] = "Thank you! ";// _someQueue[0][0];
                        tester[1] = 0;
                        tester[2] = 1;

                        _someQueue.Add(tester);

                        //dispatchConsoleCommands(tester);
                        _data0._sending_switch_out = 1;

                        _dispatchQueue_has_Sec_Responded.Add(_data0);
                        _dispatchQueue_has_Main_Responded.Remove(_dispatchQueue_has_Main_Responded[0]);            
                    }
                    else
                    {
                        _dispatchQueue_has_Main_Responded.Remove(_dispatchQueue_has_Main_Responded[0]);
                    }
                }
            }*/

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

            if (_counterTaskIsAlive > 100)
            {
                _counterTaskIsAlive = 0;
            }
            //System.Windows.MessageBox.Show("Thread is Alive", "Console");
            _counterTaskIsAlive++;
            _anotherCounter++;

            return _data0;
        }

        int _anotherCounter = 0;
        int _thankYouCounter = 0;

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

        public uint _originalConsoleModeWithMouseInput;
        public uint _originalConsoleModeWithoutMouseInput;
        public uint _modifiedConsoleMode;

    }
}
