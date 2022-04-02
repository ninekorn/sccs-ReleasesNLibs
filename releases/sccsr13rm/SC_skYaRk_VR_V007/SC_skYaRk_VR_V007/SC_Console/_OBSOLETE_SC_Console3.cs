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
    public class SC_Console
    {
        public int numberOfLinePass = 0;
        int _anotherCounter = 0;
        int _thankYouCounter = 0;

        public static List<object[]> _someCustomizedSpeedMessageQueue = new List<object[]>();
        public static List<object[]> _someQueue = new List<object[]>();
        public static List<Program._someObject> _dispatchQueue_has_Main_Responded = new List<Program._someObject>();
        public static List<Program._someObject> _dispatchQueue_has_Sec_Responded = new List<Program._someObject>();
        public static List<object> _dispatchQueue = new List<object>();

        object _ResultsOfTasks0;
        Program._someObject _ResultsOfTasks1;

        int _counterTaskIsAlive = 0;
        Program._someObject _data0;
        int _xCurrentCursorPos = 0;
        int _yCurrentCursorPos = 0;

        int _startConsole = 1;
        object _mainObjectToReturn;

        Task task0;

        public int _console_hasINIT = 1;
        public int _console_ERROR = 0;

        public static Program._someObject[] _someReceivedObject0 = new Program._someObject[16];
        public static Program._someObject[] _someReceivedObject1 = new Program._someObject[16];
        public static Program._someObject[] _someReceivedObject2 = new Program._someObject[16];
        public static Program._someObject[] _someReceivedObject3 = new Program._someObject[16];
        public static Program._someObject[] _someReceivedObject4 = new Program._someObject[16];
        public static Program._someObject[] _someReceivedObject5 = new Program._someObject[16];
        public static Program._someObject[] _someReceivedObject6 = new Program._someObject[16];
        public static Program._someObject[] _someReceivedObject7 = new Program._someObject[16];
        public static Program._someObject[] _someReceivedObject8 = new Program._someObject[16];
        public static Program._someObject[] _someReceivedObject9 = new Program._someObject[16];


        //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("" + ex.ToString(), 0, 22);


        static Task task01 = null;
        int _Task00_init_console = 1;
        int _console_is_alive_00 = 0;

        public object _console_worker(object _main_object)
        {
            task01 = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (_Task00_init_console == 1)
                    {
                        _ResultsOfTasks0 = null;
                        if (_startConsole == 1)
                        {
                            try
                            {
                                _createConsole();
                            }
                            catch (Exception ex)
                            {
                                _console_ERROR = 1;
                                consoleMessageQueue _consoleMessageQueue00000 = new consoleMessageQueue("" + ex.ToString(), 0, 22);
                            }

                            if (_console_ERROR == 0)
                            {
                                _console_hasINIT = 1;
                            }

                            _startConsole = 0;
                        }
                        _Task00_init_console = 0;
                    }

                    if (_console_is_alive_00 > 100)
                    {
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("task alive", 0, 1);
                        _console_is_alive_00 = 0;
                    }

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
                    _console_is_alive_00++;
                    Thread.Sleep(1);
                }
            });
            return null;
        }





        public SC_Console(object _firstObject)
        {
            _console_worker(_firstObject);

            for (int i = 0; i < _someReceivedObject0.Length; i++)
            {
                _someReceivedObject0[i] = new Program._someObject();
                _someReceivedObject0[i]._received_switch_in = -1;
                _someReceivedObject0[i]._received_switch_out = -1;
                _someReceivedObject0[i]._sending_switch_in = -1;
                _someReceivedObject0[i]._sending_switch_out = -1;
                _someReceivedObject0[i]._chain_Of_Tasks0 = null;
                _someReceivedObject0[i]._timeOut0 = 1;
                _someReceivedObject0[i]._passTest = "";

                _someReceivedObject1[i] = new Program._someObject();
                _someReceivedObject1[i]._received_switch_in = -1;
                _someReceivedObject1[i]._received_switch_out = -1;
                _someReceivedObject1[i]._sending_switch_in = -1;
                _someReceivedObject1[i]._sending_switch_out = -1;
                _someReceivedObject1[i]._chain_Of_Tasks0 = null;
                _someReceivedObject1[i]._timeOut0 = 1;
                _someReceivedObject1[i]._passTest = "";

                _someReceivedObject2[i] = new Program._someObject();
                _someReceivedObject2[i]._received_switch_in = -1;
                _someReceivedObject2[i]._received_switch_out = -1;
                _someReceivedObject2[i]._sending_switch_in = -1;
                _someReceivedObject2[i]._sending_switch_out = -1;
                _someReceivedObject2[i]._chain_Of_Tasks0 = null;
                _someReceivedObject2[i]._timeOut0 = 1;
                _someReceivedObject2[i]._passTest = "";

                _someReceivedObject3[i] = new Program._someObject();
                _someReceivedObject3[i]._received_switch_in = -1;
                _someReceivedObject3[i]._received_switch_out = -1;
                _someReceivedObject3[i]._sending_switch_in = -1;
                _someReceivedObject3[i]._sending_switch_out = -1;
                _someReceivedObject3[i]._chain_Of_Tasks0 = null;
                _someReceivedObject3[i]._timeOut0 = 1;
                _someReceivedObject3[i]._passTest = "";

                _someReceivedObject4[i] = new Program._someObject();
                _someReceivedObject4[i]._received_switch_in = -1;
                _someReceivedObject4[i]._received_switch_out = -1;
                _someReceivedObject4[i]._sending_switch_in = -1;
                _someReceivedObject4[i]._sending_switch_out = -1;
                _someReceivedObject4[i]._chain_Of_Tasks0 = null;
                _someReceivedObject4[i]._timeOut0 = 1;
                _someReceivedObject4[i]._passTest = "";

                _someReceivedObject5[i] = new Program._someObject();
                _someReceivedObject5[i]._received_switch_in = -1;
                _someReceivedObject5[i]._received_switch_out = -1;
                _someReceivedObject5[i]._sending_switch_in = -1;
                _someReceivedObject5[i]._sending_switch_out = -1;
                _someReceivedObject5[i]._chain_Of_Tasks0 = null;
                _someReceivedObject5[i]._timeOut0 = 1;
                _someReceivedObject5[i]._passTest = "";

                _someReceivedObject6[i] = new Program._someObject();
                _someReceivedObject6[i]._received_switch_in = -1;
                _someReceivedObject6[i]._received_switch_out = -1;
                _someReceivedObject6[i]._sending_switch_in = -1;
                _someReceivedObject6[i]._sending_switch_out = -1;
                _someReceivedObject6[i]._chain_Of_Tasks0 = null;
                _someReceivedObject6[i]._timeOut0 = 1;
                _someReceivedObject6[i]._passTest = "";

                _someReceivedObject7[i] = new Program._someObject();
                _someReceivedObject7[i]._received_switch_in = -1;
                _someReceivedObject7[i]._received_switch_out = -1;
                _someReceivedObject7[i]._sending_switch_in = -1;
                _someReceivedObject7[i]._sending_switch_out = -1;
                _someReceivedObject7[i]._chain_Of_Tasks0 = null;
                _someReceivedObject7[i]._timeOut0 = 1;
                _someReceivedObject7[i]._passTest = "";
            }
        }

        Task task00 = null;
        int _InitTaskDispatch = 1;
        static string _user_name = "";
        public object _console_work(object _main_object)
        {

            //System.Windows.MessageBox.Show("_t0", "Console");
            _mainObjectToReturn = null;
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
                string _passTest = _data0._passTest;
                _someReceivedObject0[0]._passTest = _passTest;

                if (_InitTaskDispatch == 1)
                {
                    if (_received_switch_in == 0 &&
                        _received_switch_out == 0 &&
                        _sending_switch_in == 0 &&
                        _sending_switch_out == 0)
                    {
                        System.Windows.MessageBox.Show("Flow 00b", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x0", 20, 10);
                        _someReceivedObject0[0]._received_switch_in = 0;
                        _someReceivedObject0[0]._received_switch_out = 0;
                        _someReceivedObject0[0]._sending_switch_in = 0;
                        _someReceivedObject0[0]._sending_switch_out = 0;
                        return _someReceivedObject0[0];
                    }
                    else if (_received_switch_in == 1 &&
                             _received_switch_out == 0 &&
                             _sending_switch_in == 0 &&
                             _sending_switch_out == 0)
                    {
                        System.Windows.MessageBox.Show("Console is started. What is your name?", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x1", 20, 11);

                        _user_name = Console.ReadLine();
                        _someReceivedObject0[0]._passTest = _user_name;

                        if (_user_name.ToLower() == "nine" || _user_name.ToLower() == "ninekorn")
                        {
                            _someReceivedObject0[0]._received_switch_in = 1;
                            _someReceivedObject0[0]._received_switch_out = 1;
                            _someReceivedObject0[0]._sending_switch_in = 1;
                            _someReceivedObject0[0]._sending_switch_out = 1;
                            _someReceivedObject0[0]._passTest = _user_name;
                            System.Windows.MessageBox.Show("Welcome to skYaRk", "Console");
                            return _someReceivedObject0[0];

                        }
                        else
                        {
                            _someReceivedObject0[0]._received_switch_in = 0;
                            _someReceivedObject0[0]._received_switch_out = 0;
                            _someReceivedObject0[0]._sending_switch_in = 0;
                            _someReceivedObject0[0]._sending_switch_out = 0;
                            _someReceivedObject0[0]._passTest = _user_name;
                            System.Windows.MessageBox.Show("you are not Welcomed to skYaRk", "Console");


                        }

                        for (int j = 0; j < _main_cpu_count0; j++)
                        {
                            int timeOut = 1;
                            try
                            {
                                System.Windows.MessageBox.Show("user " + _user_name, "Console");

                                task00 = Task.Factory.StartNew(() =>
                                {
                                    while (true)
                                    {
                                        _mainObjectToReturn = _DoWork_hasTaskWorked(_main_object);
                                        _main_object = _mainObjectToReturn;

                                        _data0 = (Program._someObject)_mainObjectToReturn;
                                        int _received_switch_in00 = _data0._received_switch_in;   //1
                                        int _received_switch_out00 = _data0._received_switch_out; //1
                                        int _sending_switch_in00 = _data0._sending_switch_in;     //1
                                        int _sending_switch_out00 = _data0._sending_switch_out;   //1
                                        object[][] _chain_Of_Tasks00 = _data0._chain_Of_Tasks0;
                                        int _timeOut00 = _data0._timeOut0;
                                        int _ParentTaskThreadID00 = _data0._ParentTaskThreadID0;
                                        int _main_cpu_count00 = _data0._main_cpu_count;

                                        _someReceivedObject1[0] = _data0;
                                        _someReceivedObject1[0]._passTest = _data0._passTest;

                                        if (_received_switch_in00 == 0 &&
                                            _received_switch_out00 == 0 &&
                                            _sending_switch_in00 == 0 &&
                                            _sending_switch_out00 == 0)
                                        {
                                            _someReceivedObject1[0]._received_switch_in = 0;
                                            _someReceivedObject1[0]._received_switch_out = 0;
                                            _someReceivedObject1[0]._sending_switch_in = 0;
                                            _someReceivedObject1[0]._sending_switch_out = 0;
                                            System.Windows.MessageBox.Show("Gotta stop console", "Console");
                                            //consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("Flow m0", 0, 10);
                                            //release Console? => maybe... probably
                                        }
                                        else if (_received_switch_in00 == 1 &&
                                                 _received_switch_out00 == 0 &&
                                                 _sending_switch_in00 == 0 &&
                                                 _sending_switch_out00 == 0)
                                        {

                                            System.Windows.MessageBox.Show("Console is requested.", "Console");
                                            //consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("Start Console", 0, 11);
                                            _someReceivedObject1[0]._received_switch_in = 1;
                                            _someReceivedObject1[0]._received_switch_out = 1;
                                            _someReceivedObject1[0]._sending_switch_in = 0;
                                            _someReceivedObject1[0]._sending_switch_out = 0;
                                            //System.Windows.MessageBox.Show("Received", "Console");

                                            //_mainObjectToReturn = _DoWork_hasTaskWorked(_main_object);
                                            //_main_object = _mainObjectToReturn;

                                            //return _data0;
                                        }
                                        else if (_received_switch_in00 == 0 &&
                                                 _received_switch_out00 == 1 &&
                                                 _sending_switch_in00 == 0 &&
                                                 _sending_switch_out00 == 0)
                                        {
                                            _someReceivedObject1[0]._received_switch_in = 0;
                                            _someReceivedObject1[0]._received_switch_out = 0;
                                            _someReceivedObject1[0]._sending_switch_in = 0;
                                            _someReceivedObject1[0]._sending_switch_out = 0;
                                            //System.Windows.MessageBox.Show("Flow Line 02", "Console");
                                            consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("Flow m2", 0, 12);
                                        }
                                        else if (_received_switch_in00 == 0 &&
                                                 _received_switch_out00 == 0 &&
                                                 _sending_switch_in00 == 1 &&
                                                 _sending_switch_out00 == 0)
                                        {
                                            _someReceivedObject1[0]._received_switch_in = 0;
                                            _someReceivedObject1[0]._received_switch_out = 0;
                                            _someReceivedObject1[0]._sending_switch_in = 0;
                                            _someReceivedObject1[0]._sending_switch_out = 0;
                                            //System.Windows.MessageBox.Show("Flow Line 3", "Console");
                                            consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("Flow m3", 10, 13);
                                        }
                                        else if (_received_switch_in00 == 0 &&
                                               _received_switch_out00 == 0 &&
                                               _sending_switch_in00 == 0 &&
                                               _sending_switch_out00 == 1)
                                        {
                                            _someReceivedObject1[0]._received_switch_in = 0;
                                            _someReceivedObject1[0]._received_switch_out = 0;
                                            _someReceivedObject1[0]._sending_switch_in = 0;
                                            _someReceivedObject1[0]._sending_switch_out = 0;
                                            //System.Windows.MessageBox.Show("Flow Line 04", "Console");
                                            consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("Flow m4", 0, 14);
                                        }
                                        else if (_received_switch_in00 == 1 &&
                                                _received_switch_out00 == 1 &&
                                                _sending_switch_in00 == 0 &&
                                                _sending_switch_out00 == 0)
                                        {
                                            _someReceivedObject1[0]._received_switch_in = 0;
                                            _someReceivedObject1[0]._received_switch_out = 0;
                                            _someReceivedObject1[0]._sending_switch_in = 0;
                                            _someReceivedObject1[0]._sending_switch_out = 0;


                                            System.Windows.MessageBox.Show("Flow Line 543", "Console");
                                            consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("Flow m5", 10, 15);
                                        }
                                        else if (_received_switch_in00 == 1 &&
                                             _received_switch_out00 == 0 &&
                                             _sending_switch_in00 == 1 &&
                                             _sending_switch_out00 == 0)
                                        {
                                            _someReceivedObject1[0]._received_switch_in = 0;
                                            _someReceivedObject1[0]._received_switch_out = 0;
                                            _someReceivedObject1[0]._sending_switch_in = 0;
                                            _someReceivedObject1[0]._sending_switch_out = 0;
                                            //System.Windows.MesageBox.Show("Flow Line 6", "Console");
                                            consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("Flow m6", 0, 16);
                                        }
                                        else if (_received_switch_in00 == 1 &&
                                               _received_switch_out00 == 0 &&
                                               _sending_switch_in00 == 0 &&
                                               _sending_switch_out00 == 1)
                                        {
                                            _someReceivedObject1[0]._received_switch_in = 0;
                                            _someReceivedObject1[0]._received_switch_out = 0;
                                            _someReceivedObject1[0]._sending_switch_in = 0;
                                            _someReceivedObject1[0]._sending_switch_out = 0;
                                            consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("Flow m7", 0, 17);
                                            //System.Windows.MessageBox.Show("Flow Line 07", "Console");
                                            //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("ThreadStart Performance: " + _performanceWatch.Elapsed.Ticks, 0, 15);
                                        }
                                        else if (_received_switch_in00 == 0 &&
                                            _received_switch_out00 == 1 &&
                                            _sending_switch_in00 == 1 &&
                                            _sending_switch_out00 == 0)
                                        {
                                            _someReceivedObject1[0]._received_switch_in = 0;
                                            _someReceivedObject1[0]._received_switch_out = 0;
                                            _someReceivedObject1[0]._sending_switch_in = 0;
                                            _someReceivedObject1[0]._sending_switch_out = 0;
                                            //System.Windows.MessageBox.Show("Flow Line 08", "Console");
                                            consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("Flow m8", 0, 18);
                                        }
                                        else if (_received_switch_in00 == 0 &&
                                              _received_switch_out00 == 1 &&
                                              _sending_switch_in00 == 0 &&
                                              _sending_switch_out00 == 1)
                                        {
                                            _someReceivedObject1[0]._received_switch_in = 0;
                                            _someReceivedObject1[0]._received_switch_out = 0;
                                            _someReceivedObject1[0]._sending_switch_in = 0;
                                            _someReceivedObject1[0]._sending_switch_out = 0;
                                            //System.Windows.MessageBox.Show("Flow Line 09", "Console");
                                            consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("Flow m9", 0, 19);
                                        }
                                        else if (_received_switch_in00 == 0 &&
                                              _received_switch_out00 == 0 &&
                                              _sending_switch_in00 == 1 &&
                                              _sending_switch_out00 == 1)
                                        {
                                            _someReceivedObject1[0]._received_switch_in = 0;
                                            _someReceivedObject1[0]._received_switch_out = 0;
                                            _someReceivedObject1[0]._sending_switch_in = 0;
                                            _someReceivedObject1[0]._sending_switch_out = 0;
                                            System.Windows.MessageBox.Show("Flow Line faasfasasf", "Console");
                                            //consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("Flow m10", 0, 20);
                                        }
                                        else if (_received_switch_in00 == 1 &&
                                                   _received_switch_out00 == 0 &&
                                                   _sending_switch_in00 == 1 &&
                                                   _sending_switch_out00 == 1)
                                        {
                                            _someReceivedObject1[0]._received_switch_in = 0;
                                            _someReceivedObject1[0]._received_switch_out = 0;
                                            _someReceivedObject1[0]._sending_switch_in = 0;
                                            _someReceivedObject1[0]._sending_switch_out = 0;
                                            //System.Windows.MessageBox.Show("Flow Line 011", "Console");
                                            consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("Flow m11", 0, 21);
                                        }
                                        else if (_received_switch_in00 == 0 &&
                                                _received_switch_out00 == 1 &&
                                                _sending_switch_in00 == 1 &&
                                                _sending_switch_out00 == 1)
                                        {
                                            _someReceivedObject1[0]._received_switch_in = 0;
                                            _someReceivedObject1[0]._received_switch_out = 0;
                                            _someReceivedObject1[0]._sending_switch_in = 0;
                                            _someReceivedObject1[0]._sending_switch_out = 0;
                                            //System.Windows.MessageBox.Show("Flow Line 12", "Console");
                                            consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("Flow m12", 0, 22);
                                        }
                                        else if (_received_switch_in00 == 1 &&
                                                 _received_switch_out00 == 1 &&
                                                 _sending_switch_in00 == 1 &&
                                                 _sending_switch_out00 == 0)
                                        {
                                            _someReceivedObject1[0]._received_switch_in = 0;
                                            _someReceivedObject1[0]._received_switch_out = 0;
                                            _someReceivedObject1[0]._sending_switch_in = 0;
                                            _someReceivedObject1[0]._sending_switch_out = 0;
                                            //System.Windows.MessageBox.Show("Flow Line 013", "Console");
                                            consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("Flow m13", 0, 23);
                                        }
                                        else if (_received_switch_in00 == 1 &&
                                                _received_switch_out00 == 1 &&
                                                _sending_switch_in00 == 0 &&
                                                _sending_switch_out00 == 1)
                                        {
                                            _someReceivedObject1[0]._received_switch_in = 0;
                                            _someReceivedObject1[0]._received_switch_out = 0;
                                            _someReceivedObject1[0]._sending_switch_in = 0;
                                            _someReceivedObject1[0]._sending_switch_out = 0;
                                            //System.Windows.MessageBox.Show("Flow Line 014", "Console");
                                            consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("Flow m14", 0, 24);
                                        }
                                        else if (_received_switch_in00 == 1 &&
                                              _received_switch_out00 == 1 &&
                                              _sending_switch_in00 == 1 &&
                                              _sending_switch_out00 == 1)
                                        {
                                            _someReceivedObject1[0]._received_switch_in = 0;
                                            _someReceivedObject1[0]._received_switch_out = 0;
                                            _someReceivedObject1[0]._sending_switch_in = 0;
                                            _someReceivedObject1[0]._sending_switch_out = 0;
                                            //System.Windows.MessageBox.Show("Flow Line 015", "Console");
                                            consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("Flow m15", 0, 25);
                                        }
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

                        _data0._received_switch_out = 0;
                        //_dispatchQueue_has_Main_Responded.Add(_data0);
                        _data0._received_switch_out = 0;

                        //SECOND TASK

                        int _totalThreads = _main_cpu_count0;

                        for (int j = 0; j < _main_cpu_count0; j++)
                        {
                            int timeOut = 1;

                            try
                            {
                                Task task00 = null;
                                task00 = Task.Factory.StartNew(() =>
                                {
                                    //int sometest = 0;
                                    while (true)
                                    {
                                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("" + sometest, 0, 3);
                                        _someReceivedObject7[0] = _DoWork_MainTask(_main_object);
                                        //System.Windows.MessageBox.Show("Thread is Alive 00", "Console");
                                        _main_object = _someReceivedObject7[0];

                                        _data0 = (Program._someObject)_someReceivedObject7[0];
                                        int _received_switch_in00 = _data0._received_switch_in;   //1
                                        int _received_switch_out00 = _data0._received_switch_out; //1
                                        int _sending_switch_in00 = _data0._sending_switch_in;     //1
                                        int _sending_switch_out00 = _data0._sending_switch_out;   //1
                                        object[][] _chain_Of_Tasks00 = _data0._chain_Of_Tasks0;
                                        int _timeOut00 = _data0._timeOut0;
                                        int _ParentTaskThreadID00 = _data0._ParentTaskThreadID0;
                                        int _main_cpu_count00 = _data0._main_cpu_count;
                                        _passTest = _data0._passTest;


                                        _someReceivedObject7[0]._passTest = _passTest;

                                        if (_received_switch_in00 == 0 &&
                                        _received_switch_out00 == 0 &&
                                        _sending_switch_in00 == 0 &&
                                        _sending_switch_out00 == 0)
                                        {
                                            _someReceivedObject7[0]._received_switch_in = 0;
                                            _someReceivedObject7[0]._received_switch_out = 0;
                                            _someReceivedObject7[0]._sending_switch_in = 0;
                                            _someReceivedObject7[0]._sending_switch_out = 0;

                                            System.Windows.MessageBox.Show("Flow 00x", "Console");
                                            //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx0", 30, 10);
                                        }
                                        else if (_received_switch_in00 == 1 &&
                                                 _received_switch_out00 == 0 &&
                                                 _sending_switch_in00 == 0 &&
                                                 _sending_switch_out00 == 0)
                                        {
                                            _someReceivedObject7[0]._received_switch_in = 1;
                                            _someReceivedObject7[0]._received_switch_out = 1;
                                            _someReceivedObject7[0]._sending_switch_in = 1;
                                            _someReceivedObject7[0]._sending_switch_out = 1;
                                            System.Windows.MessageBox.Show("Flow 01a", "Console");
                                            //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx1", 30, 11);
                                        }
                                        else if (_received_switch_in00 == 0 &&
                                                 _received_switch_out00 == 1 &&
                                                 _sending_switch_in00 == 0 &&
                                                 _sending_switch_out00 == 0)
                                        {
                                            _someReceivedObject7[0]._received_switch_in = 1;
                                            _someReceivedObject7[0]._received_switch_out = 1;
                                            _someReceivedObject7[0]._sending_switch_in = 1;
                                            _someReceivedObject7[0]._sending_switch_out = 1;
                                            System.Windows.MessageBox.Show("Flow 02a", "Console");
                                            //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx2", 30, 12);
                                        }
                                        else if (_received_switch_in00 == 0 &&
                                                 _received_switch_out00 == 0 &&
                                                 _sending_switch_in00 == 1 &&
                                                 _sending_switch_out00 == 0)
                                        {
                                            _someReceivedObject7[0]._received_switch_in = 1;
                                            _someReceivedObject7[0]._received_switch_out = 1;
                                            _someReceivedObject7[0]._sending_switch_in = 1;
                                            _someReceivedObject7[0]._sending_switch_out = 1;
                                            System.Windows.MessageBox.Show("Flow 3", "Console");
                                            //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx3", 30, 13);
                                        }
                                        else if (_received_switch_in00 == 0 &&
                                               _received_switch_out00 == 0 &&
                                               _sending_switch_in00 == 0 &&
                                               _sending_switch_out00 == 1)
                                        {
                                            _someReceivedObject7[0]._received_switch_in = 1;
                                            _someReceivedObject7[0]._received_switch_out = 1;
                                            _someReceivedObject7[0]._sending_switch_in = 1;
                                            _someReceivedObject7[0]._sending_switch_out = 1;
                                            System.Windows.MessageBox.Show("Flow 04h", "Console");
                                            //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx4", 30, 14);
                                        }
                                        else if (_received_switch_in00 == 1 &&
                                                _received_switch_out00 == 1 &&
                                                _sending_switch_in00 == 0 &&
                                                _sending_switch_out00 == 0)
                                        {
                                            //Console Waiting for response? // logic is "knock knock, Console responds whos there"
                                            _someReceivedObject7[0]._received_switch_in = 1;
                                            _someReceivedObject7[0]._received_switch_out = 0;
                                            _someReceivedObject7[0]._sending_switch_in = 0;
                                            _someReceivedObject7[0]._sending_switch_out = 1;
                                            System.Windows.MessageBox.Show("Flow 05b", "Console");
                                            //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx5", 30, 15);
                                            return _someReceivedObject7[0];
                                        }
                                        else if (_received_switch_in00 == 1 &&
                                             _received_switch_out00 == 0 &&
                                             _sending_switch_in00 == 1 &&
                                             _sending_switch_out00 == 0)
                                        {
                                            _someReceivedObject7[0]._received_switch_in = 1;
                                            _someReceivedObject7[0]._received_switch_out = 1;
                                            _someReceivedObject7[0]._sending_switch_in = 1;
                                            _someReceivedObject7[0]._sending_switch_out = 1;
                                            System.Windows.MessageBox.Show("Flow 6", "Console");
                                            //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx6", 30, 16);
                                        }
                                        else if (_received_switch_in00 == 1 &&
                                               _received_switch_out00 == 0 &&
                                               _sending_switch_in00 == 0 &&
                                               _sending_switch_out00 == 1)
                                        {
                                            System.Windows.MessageBox.Show("name test", "Console");

                                            if (_user_name.ToLower() == "nine" || _user_name.ToLower() == "ninekorn")
                                            {
                                                System.Windows.MessageBox.Show("Your name is: " + _user_name, "Console");

                                                _someReceivedObject7[0]._received_switch_in = 1;
                                                _someReceivedObject7[0]._received_switch_out = 0;
                                                _someReceivedObject7[0]._sending_switch_in = 1;
                                                _someReceivedObject7[0]._sending_switch_out = 1;
                                                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx7", 30, 17);
                                                System.Windows.MessageBox.Show("Flow 07a", "Console");
                                                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("ThreadStart Performance: " + _performanceWatch.Elapsed.Ticks, 0, 15);
                                            }
                                            else
                                            {
                                                System.Windows.MessageBox.Show("Your name is: " + "squat" + " bitch", "Console");
                                                _someReceivedObject7[0]._received_switch_in = 0;
                                                _someReceivedObject7[0]._received_switch_out = 0;
                                                _someReceivedObject7[0]._sending_switch_in = 0;
                                                _someReceivedObject7[0]._sending_switch_out = 0;
                                            }
                                        }

                                        else if (_received_switch_in00 == 0 &&
                                            _received_switch_out00 == 1 &&
                                            _sending_switch_in00 == 1 &&
                                            _sending_switch_out00 == 0)
                                        {
                                            _someReceivedObject7[0]._received_switch_in = 1;
                                            _someReceivedObject7[0]._received_switch_out = 1;
                                            _someReceivedObject7[0]._sending_switch_in = 1;
                                            _someReceivedObject7[0]._sending_switch_out = 1;
                                            System.Windows.MessageBox.Show("Flow 08c", "Console");
                                            //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx8", 30, 18);
                                        }
                                        else if (_received_switch_in00 == 0 &&
                                              _received_switch_out00 == 1 &&
                                              _sending_switch_in00 == 0 &&
                                              _sending_switch_out00 == 1)
                                        {
                                            _someReceivedObject7[0]._received_switch_in = 1;
                                            _someReceivedObject7[0]._received_switch_out = 1;
                                            _someReceivedObject7[0]._sending_switch_in = 1;
                                            _someReceivedObject7[0]._sending_switch_out = 1;
                                            System.Windows.MessageBox.Show("Flow 09", "Console");
                                            //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx9", 30, 19);
                                        }
                                        else if (_received_switch_in00 == 0 &&
                                              _received_switch_out00 == 0 &&
                                              _sending_switch_in00 == 1 &&
                                              _sending_switch_out00 == 1)
                                        {
                                            _someReceivedObject7[0]._received_switch_in = 1;
                                            _someReceivedObject7[0]._received_switch_out = 1;
                                            _someReceivedObject7[0]._sending_switch_in = 1;
                                            _someReceivedObject7[0]._sending_switch_out = 1;
                                            System.Windows.MessageBox.Show("Flow 10", "Console");
                                            //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx10", 30, 20);
                                        }
                                        else if (_received_switch_in00 == 1 &&
                                                   _received_switch_out00 == 0 &&
                                                   _sending_switch_in00 == 1 &&
                                                   _sending_switch_out00 == 1)
                                        {
                                            _someReceivedObject7[0]._received_switch_in = 1;
                                            _someReceivedObject7[0]._received_switch_out = 1;
                                            _someReceivedObject7[0]._sending_switch_in = 1;
                                            _someReceivedObject7[0]._sending_switch_out = 1;
                                            System.Windows.MessageBox.Show("Flow 011g", "Console");
                                            //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx11", 30, 21);
                                        }
                                        else if (_received_switch_in00 == 0 &&
                                                _received_switch_out00 == 1 &&
                                                _sending_switch_in00 == 1 &&
                                                _sending_switch_out00 == 1)
                                        {
                                            _someReceivedObject7[0]._received_switch_in = 1;
                                            _someReceivedObject7[0]._received_switch_out = 1;
                                            _someReceivedObject7[0]._sending_switch_in = 1;
                                            _someReceivedObject7[0]._sending_switch_out = 1;
                                            System.Windows.MessageBox.Show("Flow 12", "Console");
                                            //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx12", 30, 22);
                                        }
                                        else if (_received_switch_in00 == 1 &&
                                                 _received_switch_out00 == 1 &&
                                                 _sending_switch_in00 == 1 &&
                                                 _sending_switch_out00 == 0)
                                        {
                                            _someReceivedObject7[0]._received_switch_in = 1;
                                            _someReceivedObject7[0]._received_switch_out = 1;
                                            _someReceivedObject7[0]._sending_switch_in = 1;
                                            _someReceivedObject7[0]._sending_switch_out = 1;
                                            System.Windows.MessageBox.Show("Flow 013c0", "Console");
                                            //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx13", 30, 23);
                                        }
                                        else if (_received_switch_in00 == 1 &&
                                                _received_switch_out00 == 1 &&
                                                _sending_switch_in00 == 0 &&
                                                _sending_switch_out00 == 1)
                                        {
                                            _someReceivedObject7[0]._received_switch_in = 1;
                                            _someReceivedObject7[0]._received_switch_out = 1;
                                            _someReceivedObject7[0]._sending_switch_in = 1;
                                            _someReceivedObject7[0]._sending_switch_out = 1;
                                            System.Windows.MessageBox.Show("Flow 014", "Console");
                                            //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx14", 30, 24);
                                        }
                                        else if (_received_switch_in00 == 1 &&
                                              _received_switch_out00 == 1 &&
                                              _sending_switch_in00 == 1 &&
                                              _sending_switch_out00 == 1)
                                        {
                                            _someReceivedObject7[0]._received_switch_in = 1;
                                            _someReceivedObject7[0]._received_switch_out = 1;
                                            _someReceivedObject7[0]._sending_switch_in = 1;
                                            _someReceivedObject7[0]._sending_switch_out = 1;
                                            System.Windows.MessageBox.Show("Flow 015e", "Console");
                                            //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx15", 30, 25);
                                        }
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
                    else if (_received_switch_in == 0 &&
                             _received_switch_out == 1 &&
                             _sending_switch_in == 0 &&
                             _sending_switch_out == 0)
                    {
                        //Maybe used to ask another question.
                        _someReceivedObject0[0]._received_switch_in = 1;
                        _someReceivedObject0[0]._received_switch_out = 0;
                        _someReceivedObject0[0]._sending_switch_in = 1;
                        _someReceivedObject0[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 02b", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x2", 20, 12);
                    }
                    else if (_received_switch_in == 0 &&
                             _received_switch_out == 0 &&
                             _sending_switch_in == 1 &&
                             _sending_switch_out == 0)
                    {
                        _someReceivedObject0[0]._received_switch_in = 1;
                        _someReceivedObject0[0]._received_switch_out = 1;
                        _someReceivedObject0[0]._sending_switch_in = 1;
                        _someReceivedObject0[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 3", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x3", 20, 13);
                    }
                    else if (_received_switch_in == 0 &&
                           _received_switch_out == 0 &&
                           _sending_switch_in == 0 &&
                           _sending_switch_out == 1)
                    {
                        _someReceivedObject0[0]._received_switch_in = 1;
                        _someReceivedObject0[0]._received_switch_out = 1;
                        _someReceivedObject0[0]._sending_switch_in = 1;
                        _someReceivedObject0[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 04", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x4", 20, 14);
                    }
                    else if (_received_switch_in == 1 &&
                            _received_switch_out == 1 &&
                            _sending_switch_in == 0 &&
                            _sending_switch_out == 0)
                    {
                        _someReceivedObject0[0]._received_switch_in = 1;
                        _someReceivedObject0[0]._received_switch_out = 1;
                        _someReceivedObject0[0]._sending_switch_in = 1;
                        _someReceivedObject0[0]._sending_switch_out = 1;
                        System.Windows.MessageBox.Show("Coming Back here", "Console");



                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x5", 20, 15);
                    }
                    else if (_received_switch_in == 1 &&
                         _received_switch_out == 0 &&
                         _sending_switch_in == 1 &&
                         _sending_switch_out == 0)
                    {
                        _someReceivedObject0[0]._received_switch_in = 1;
                        _someReceivedObject0[0]._received_switch_out = 1;
                        _someReceivedObject0[0]._sending_switch_in = 1;
                        _someReceivedObject0[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 6", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x6", 20, 16);
                    }
                    else if (_received_switch_in == 1 &&
                           _received_switch_out == 0 &&
                           _sending_switch_in == 0 &&
                           _sending_switch_out == 1)
                    {
                        _someReceivedObject0[0]._received_switch_in = 1;
                        _someReceivedObject0[0]._received_switch_out = 1;
                        _someReceivedObject0[0]._sending_switch_in = 1;
                        _someReceivedObject0[0]._sending_switch_out = 1;
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x7", 20, 17);
                        //System.Windows.MessageBox.Show("Flow 07", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("ThreadStart Performance: " + _performanceWatch.Elapsed.Ticks, 0, 15);
                    }
                    else if (_received_switch_in == 0 &&
                        _received_switch_out == 1 &&
                        _sending_switch_in == 1 &&
                        _sending_switch_out == 0)
                    {
                        _someReceivedObject0[0]._received_switch_in = 0;
                        _someReceivedObject0[0]._received_switch_out = 0;
                        _someReceivedObject0[0]._sending_switch_in = 0;
                        _someReceivedObject0[0]._sending_switch_out = 1;



                        System.Windows.MessageBox.Show("Flow 08 a ", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x8a", 20, 18);
                    }
                    else if (_received_switch_in == 0 &&
                          _received_switch_out == 1 &&
                          _sending_switch_in == 0 &&
                          _sending_switch_out == 1)
                    {
                        _someReceivedObject0[0]._received_switch_in = 1;
                        _someReceivedObject0[0]._received_switch_out = 1;
                        _someReceivedObject0[0]._sending_switch_in = 1;
                        _someReceivedObject0[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 09", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x9", 20, 19);
                    }
                    else if (_received_switch_in == 0 &&
                          _received_switch_out == 0 &&
                          _sending_switch_in == 1 &&
                          _sending_switch_out == 1)
                    {
                        _someReceivedObject0[0]._received_switch_in = 1;
                        _someReceivedObject0[0]._received_switch_out = 1;
                        _someReceivedObject0[0]._sending_switch_in = 1;
                        _someReceivedObject0[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 10", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x10", 20, 20);
                    }
                    else if (_received_switch_in == 1 &&
                               _received_switch_out == 0 &&
                               _sending_switch_in == 1 &&
                               _sending_switch_out == 1)
                    {
                        _someReceivedObject0[0]._received_switch_in = 1;
                        _someReceivedObject0[0]._received_switch_out = 1;
                        _someReceivedObject0[0]._sending_switch_in = 1;
                        _someReceivedObject0[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 011", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x11", 20, 21);
                    }
                    else if (_received_switch_in == 0 &&
                            _received_switch_out == 1 &&
                            _sending_switch_in == 1 &&
                            _sending_switch_out == 1)
                    {
                        _someReceivedObject0[0]._received_switch_in = 1;
                        _someReceivedObject0[0]._received_switch_out = 1;
                        _someReceivedObject0[0]._sending_switch_in = 1;
                        _someReceivedObject0[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 12", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x12", 20, 22);
                    }
                    else if (_received_switch_in == 1 &&
                             _received_switch_out == 1 &&
                             _sending_switch_in == 1 &&
                             _sending_switch_out == 0)
                    {
                        _someReceivedObject0[0]._received_switch_in = 1;
                        _someReceivedObject0[0]._received_switch_out = 1;
                        _someReceivedObject0[0]._sending_switch_in = 1;
                        _someReceivedObject0[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 013", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x13", 20, 23);
                    }
                    else if (_received_switch_in == 1 &&
                            _received_switch_out == 1 &&
                            _sending_switch_in == 0 &&
                            _sending_switch_out == 1)
                    {
                        _someReceivedObject0[0]._received_switch_in = 1;
                        _someReceivedObject0[0]._received_switch_out = 1;
                        _someReceivedObject0[0]._sending_switch_in = 1;
                        _someReceivedObject0[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 014", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x14", 20, 24);
                    }
                    else if (_received_switch_in == 1 &&
                          _received_switch_out == 1 &&
                          _sending_switch_in == 1 &&
                          _sending_switch_out == 1)
                    {
                        System.Windows.MessageBox.Show("Flow 015w", "Console");
                        /*_someReceivedObject0[0]._received_switch_in = 1;
                        _someReceivedObject0[0]._received_switch_out = 1;
                        _someReceivedObject0[0]._sending_switch_in = 1;
                        _someReceivedObject0[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 015", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x15", 20, 25);
                        */
                    }
                }
                else
                {
                    ////////////////////////////////////////////
                    ////////////////////////////////////////////
                    ////////////////////////////////////////////
                    if (_received_switch_in == 0 &&
                        _received_switch_out == 0 &&
                        _sending_switch_in == 0 &&
                        _sending_switch_out == 0)
                    {
                        System.Windows.MessageBox.Show("Flow 0000b", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x0", 20, 10);
                        _someReceivedObject3[0]._received_switch_in = 0;
                        _someReceivedObject3[0]._received_switch_out = 0;
                        _someReceivedObject3[0]._sending_switch_in = 0;
                        _someReceivedObject3[0]._sending_switch_out = 0;

                        return _someReceivedObject3[0];
                    }
                    else if (_received_switch_in == 1 &&
                             _received_switch_out == 0 &&
                             _sending_switch_in == 0 &&
                             _sending_switch_out == 0)
                    {
                        System.Windows.MessageBox.Show("Dual brains?", "Console");

                        _user_name = Console.ReadLine();
                        if (_user_name.ToLower() == "nine" || _user_name.ToLower() == "ninekorn")
                        {
                            _someReceivedObject3[0]._received_switch_in = 1;
                            _someReceivedObject3[0]._received_switch_out = 0;
                            _someReceivedObject3[0]._sending_switch_in = 0;
                            _someReceivedObject3[0]._sending_switch_out = 1;
                            System.Windows.MessageBox.Show("Dual brains?", "Console");
                        }




                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Console is. Requesting to start?", 0, 1);
                        /*System.Windows.MessageBox.Show("STOP HERE 994", "Console");
                        var name  = Console.ReadLine();

                        if (name.ToLower() == "yes" || name.ToLower() == "y")
                        {
                            _someReceivedObject3[0]._received_switch_in = 1;
                            _someReceivedObject3[0]._received_switch_out = 0;
                            _someReceivedObject3[0]._sending_switch_in = 0;
                            _someReceivedObject3[0]._sending_switch_out = 1;
                            System.Windows.MessageBox.Show("Welcome to skYaRk", "Console");
                        }*/

                        //System.Windows.MessageBox.Show("STOP HERE", "Console");

                        /*System.Windows.MessageBox.Show("Flow 1000", "Console");
                        _someReceivedObject3[0]._received_switch_in = 1;
                        _someReceivedObject3[0]._received_switch_out = 1;
                        _someReceivedObject3[0]._sending_switch_in = 1;
                        _someReceivedObject3[0]._sending_switch_out = 1;
                        */
                    }
                    else if (_received_switch_in == 0 &&
                             _received_switch_out == 1 &&
                             _sending_switch_in == 0 &&
                             _sending_switch_out == 0)
                    {
                        _someReceivedObject3[0]._received_switch_in = 1;
                        _someReceivedObject3[0]._received_switch_out = 1;
                        _someReceivedObject3[0]._sending_switch_in = 1;
                        _someReceivedObject3[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 02", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x2", 20, 12);
                    }
                    else if (_received_switch_in == 0 &&
                             _received_switch_out == 0 &&
                             _sending_switch_in == 1 &&
                             _sending_switch_out == 0)
                    {
                        _someReceivedObject3[0]._received_switch_in = 1;
                        _someReceivedObject3[0]._received_switch_out = 1;
                        _someReceivedObject3[0]._sending_switch_in = 1;
                        _someReceivedObject3[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 3", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x3", 20, 13);
                    }
                    else if (_received_switch_in == 0 &&
                           _received_switch_out == 0 &&
                           _sending_switch_in == 0 &&
                           _sending_switch_out == 1)
                    {
                        _someReceivedObject3[0]._received_switch_in = 1;
                        _someReceivedObject3[0]._received_switch_out = 1;
                        _someReceivedObject3[0]._sending_switch_in = 1;
                        _someReceivedObject3[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 04", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x4", 20, 14);
                    }
                    else if (_received_switch_in == 1 &&
                            _received_switch_out == 1 &&
                            _sending_switch_in == 0 &&
                            _sending_switch_out == 0)
                    {
                        System.Windows.MessageBox.Show("Flow 1100", "Console");
                        _someReceivedObject3[0]._received_switch_in = 1;
                        _someReceivedObject3[0]._received_switch_out = 1;
                        _someReceivedObject3[0]._sending_switch_in = 1;
                        _someReceivedObject3[0]._sending_switch_out = 1;

                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x5", 20, 15);
                    }
                    else if (_received_switch_in == 1 &&
                         _received_switch_out == 0 &&
                         _sending_switch_in == 1 &&
                         _sending_switch_out == 0)
                    {
                        _someReceivedObject3[0]._received_switch_in = 1;
                        _someReceivedObject3[0]._received_switch_out = 1;
                        _someReceivedObject3[0]._sending_switch_in = 1;
                        _someReceivedObject3[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 6", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x6", 20, 16);
                    }
                    else if (_received_switch_in == 1 &&
                           _received_switch_out == 0 &&
                           _sending_switch_in == 0 &&
                           _sending_switch_out == 1)
                    {
                        _someReceivedObject3[0]._received_switch_in = 1;
                        _someReceivedObject3[0]._received_switch_out = 1;
                        _someReceivedObject3[0]._sending_switch_in = 1;
                        _someReceivedObject3[0]._sending_switch_out = 1;
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x7", 20, 17);
                        //System.Windows.MessageBox.Show("Flow 07", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("ThreadStart Performance: " + _performanceWatch.Elapsed.Ticks, 0, 15);
                    }
                    else if (_received_switch_in == 0 &&
                        _received_switch_out == 1 &&
                        _sending_switch_in == 1 &&
                        _sending_switch_out == 0)
                    {
                        _someReceivedObject3[0]._received_switch_in = 1;
                        _someReceivedObject3[0]._received_switch_out = 1;
                        _someReceivedObject3[0]._sending_switch_in = 1;
                        _someReceivedObject3[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 08", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x8b", 20, 18);
                    }
                    else if (_received_switch_in == 0 &&
                          _received_switch_out == 1 &&
                          _sending_switch_in == 0 &&
                          _sending_switch_out == 1)
                    {
                        _someReceivedObject3[0]._received_switch_in = 1;
                        _someReceivedObject3[0]._received_switch_out = 1;
                        _someReceivedObject3[0]._sending_switch_in = 1;
                        _someReceivedObject3[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 09", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x9", 20, 19);
                    }
                    else if (_received_switch_in == 0 &&
                          _received_switch_out == 0 &&
                          _sending_switch_in == 1 &&
                          _sending_switch_out == 1)
                    {
                        _someReceivedObject3[0]._received_switch_in = 1;
                        _someReceivedObject3[0]._received_switch_out = 1;
                        _someReceivedObject3[0]._sending_switch_in = 1;
                        _someReceivedObject3[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 10", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x10", 20, 20);
                    }
                    else if (_received_switch_in == 1 &&
                               _received_switch_out == 0 &&
                               _sending_switch_in == 1 &&
                               _sending_switch_out == 1)
                    {
                        _someReceivedObject3[0]._received_switch_in = 1;
                        _someReceivedObject3[0]._received_switch_out = 1;
                        _someReceivedObject3[0]._sending_switch_in = 1;
                        _someReceivedObject3[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 011", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x11", 20, 21);
                    }
                    else if (_received_switch_in == 0 &&
                            _received_switch_out == 1 &&
                            _sending_switch_in == 1 &&
                            _sending_switch_out == 1)
                    {
                        _someReceivedObject3[0]._received_switch_in = 1;
                        _someReceivedObject3[0]._received_switch_out = 1;
                        _someReceivedObject3[0]._sending_switch_in = 1;
                        _someReceivedObject3[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 12", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x12", 20, 22);
                    }
                    else if (_received_switch_in == 1 &&
                             _received_switch_out == 1 &&
                             _sending_switch_in == 1 &&
                             _sending_switch_out == 0)
                    {
                        _someReceivedObject3[0]._received_switch_in = 1;
                        _someReceivedObject3[0]._received_switch_out = 1;
                        _someReceivedObject3[0]._sending_switch_in = 1;
                        _someReceivedObject3[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 013", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x13", 20, 23);
                    }
                    else if (_received_switch_in == 1 &&
                            _received_switch_out == 1 &&
                            _sending_switch_in == 0 &&
                            _sending_switch_out == 1)
                    {
                        _someReceivedObject3[0]._received_switch_in = 1;
                        _someReceivedObject3[0]._received_switch_out = 1;
                        _someReceivedObject3[0]._sending_switch_in = 1;
                        _someReceivedObject3[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 014", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x14", 20, 24);
                    }
                    else if (_received_switch_in == 1 &&
                          _received_switch_out == 1 &&
                          _sending_switch_in == 1 &&
                          _sending_switch_out == 1)
                    {
                        _someReceivedObject3[0]._received_switch_in = 1;
                        _someReceivedObject3[0]._received_switch_out = 1;
                        _someReceivedObject3[0]._sending_switch_in = 1;
                        _someReceivedObject3[0]._sending_switch_out = 1;
                        //System.Windows.MessageBox.Show("Flow 015", "Console");
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x15", 20, 25);
                    }


                    if (_dispatchQueue.Count > 0)
                    {
                        if (_dispatchQueue[0] is Program._someObject)
                        {
                            _data0 = (Program._someObject)_dispatchQueue[0];
                            object _receivedObj = _dispatchQueue[0];
                            _dispatchQueue.Remove(_dispatchQueue[0]);

                            int _received_switch_in00 = _data0._received_switch_in;   //1
                            int _received_switch_out00 = _data0._received_switch_out; //0
                            int _sending_switch_in00 = _data0._sending_switch_in;     //0
                            int _sending_switch_out00 = _data0._sending_switch_out;   //0
                            object[][] _chain_Of_Tasks00 = _data0._chain_Of_Tasks0;
                            int _timeOut00 = _data0._timeOut0;
                            int _ParentTaskThreadID00 = _data0._ParentTaskThreadID0;
                            int _main_cpu_count00 = _data0._main_cpu_count;
                            _passTest = _data0._passTest;

                            _data0._passTest = _passTest;


                            if (_received_switch_in == 0 &&
                                _received_switch_out == 0 &&
                                _sending_switch_in == 0 &&
                                _sending_switch_out == 0)
                            {
                                //THE FOLLOWING CAN BE USED TO REMOVE CONSOLE FLOW
                                //_data0._received_switch_in = 0;
                                System.Windows.MessageBox.Show("removing Flow 999", "Console");
                                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("testing", 0, 11);

                                _data0._received_switch_in = 0;
                                _data0._received_switch_out = 0;
                                _data0._sending_switch_in = 0;
                                _data0._sending_switch_out = 0;

                                _someReceivedObject6[0]._received_switch_in = 1;
                                _someReceivedObject6[0]._received_switch_out = 1;
                                _someReceivedObject6[0]._sending_switch_in = 1;
                                _someReceivedObject6[0]._sending_switch_out = 1;

                                //object _receivedObj = _dispatchQueue[0];
                                //_dispatchQueue.Remove(_dispatchQueue[0]);
                                //return _receivedObj;
                                //System.Windows.MessageBox.Show("Flow 00", "Console");
                                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x0", 70, 10);
                            }
                            else if (_received_switch_in == 1 &&
                                     _received_switch_out == 0 &&
                                     _sending_switch_in == 0 &&
                                     _sending_switch_out == 0)
                            {
                                _someReceivedObject6[0]._received_switch_in = 1;
                                _someReceivedObject6[0]._received_switch_out = 1;
                                _someReceivedObject6[0]._sending_switch_in = 1;
                                _someReceivedObject6[0]._sending_switch_out = 1;
                                //System.Windows.MessageBox.Show("Flow 01", "Console");
                                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x1a", 70, 11);
                            }
                            else if (_received_switch_in == 0 &&
                                     _received_switch_out == 1 &&
                                     _sending_switch_in == 0 &&
                                     _sending_switch_out == 0)
                            {
                                _someReceivedObject6[0]._received_switch_in = 1;
                                _someReceivedObject6[0]._received_switch_out = 1;
                                _someReceivedObject6[0]._sending_switch_in = 1;
                                _someReceivedObject6[0]._sending_switch_out = 1;
                                //System.Windows.MessageBox.Show("Flow 02", "Console");
                                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x2", 70, 12);
                            }
                            else if (_received_switch_in == 0 &&
                                     _received_switch_out == 0 &&
                                     _sending_switch_in == 1 &&
                                     _sending_switch_out == 0)
                            {
                                _someReceivedObject6[0]._received_switch_in = 1;
                                _someReceivedObject6[0]._received_switch_out = 1;
                                _someReceivedObject6[0]._sending_switch_in = 1;
                                _someReceivedObject6[0]._sending_switch_out = 1;
                                //System.Windows.MessageBox.Show("Flow 3", "Console");
                                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x3", 70, 13);
                            }
                            else if (_received_switch_in == 0 &&
                                   _received_switch_out == 0 &&
                                   _sending_switch_in == 0 &&
                                   _sending_switch_out == 1)
                            {
                                _someReceivedObject6[0]._received_switch_in = 1;
                                _someReceivedObject6[0]._received_switch_out = 1;
                                _someReceivedObject6[0]._sending_switch_in = 1;
                                _someReceivedObject6[0]._sending_switch_out = 1;
                                //System.Windows.MessageBox.Show("Flow 04", "Console");
                                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x4", 70, 14);
                            }
                            else if (_received_switch_in == 1 &&
                                    _received_switch_out == 1 &&
                                    _sending_switch_in == 0 &&
                                    _sending_switch_out == 0)
                            {
                                _someReceivedObject6[0]._received_switch_in = 1;
                                _someReceivedObject6[0]._received_switch_out = 1;
                                _someReceivedObject6[0]._sending_switch_in = 1;
                                _someReceivedObject6[0]._sending_switch_out = 1;
                                //System.Windows.MessageBox.Show("Flow 05", "Console");
                                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x5", 70, 15);
                            }
                            else if (_received_switch_in == 1 &&
                                 _received_switch_out == 0 &&
                                 _sending_switch_in == 1 &&
                                 _sending_switch_out == 0)
                            {
                                _someReceivedObject6[0]._received_switch_in = 1;
                                _someReceivedObject6[0]._received_switch_out = 1;
                                _someReceivedObject6[0]._sending_switch_in = 1;
                                _someReceivedObject6[0]._sending_switch_out = 1;
                                //System.Windows.MessageBox.Show("Flow 6", "Console");
                                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x6", 70, 16);
                            }
                            else if (_received_switch_in == 1 &&
                                   _received_switch_out == 0 &&
                                   _sending_switch_in == 0 &&
                                   _sending_switch_out == 1)
                            {
                                _someReceivedObject6[0]._received_switch_in = 1;
                                _someReceivedObject6[0]._received_switch_out = 1;
                                _someReceivedObject6[0]._sending_switch_in = 1;
                                _someReceivedObject6[0]._sending_switch_out = 1;
                                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x7", 70, 17);
                                //System.Windows.MessageBox.Show("Flow 07", "Console");
                                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("ThreadStart Performance: " + _performanceWatch.Elapsed.Ticks, 0, 15);
                            }
                            else if (_received_switch_in == 0 &&
                                _received_switch_out == 1 &&
                                _sending_switch_in == 1 &&
                                _sending_switch_out == 0)
                            {
                                _someReceivedObject6[0]._received_switch_in = 1;
                                _someReceivedObject6[0]._received_switch_out = 1;
                                _someReceivedObject6[0]._sending_switch_in = 1;
                                _someReceivedObject6[0]._sending_switch_out = 1;
                                //System.Windows.MessageBox.Show("Flow 08", "Console");
                                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x8c", 70, 18);
                            }
                            else if (_received_switch_in == 0 &&
                                  _received_switch_out == 1 &&
                                  _sending_switch_in == 0 &&
                                  _sending_switch_out == 1)
                            {
                                _someReceivedObject6[0]._received_switch_in = 1;
                                _someReceivedObject6[0]._received_switch_out = 1;
                                _someReceivedObject6[0]._sending_switch_in = 1;
                                _someReceivedObject6[0]._sending_switch_out = 1;
                                //System.Windows.MessageBox.Show("Flow 09", "Console");
                                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x9", 70, 19);
                            }
                            else if (_received_switch_in == 0 &&
                                  _received_switch_out == 0 &&
                                  _sending_switch_in == 1 &&
                                  _sending_switch_out == 1)
                            {
                                _someReceivedObject6[0]._received_switch_in = 1;
                                _someReceivedObject6[0]._received_switch_out = 1;
                                _someReceivedObject6[0]._sending_switch_in = 1;
                                _someReceivedObject6[0]._sending_switch_out = 1;
                                //System.Windows.MessageBox.Show("Flow 10", "Console");
                                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x10", 70, 20);
                            }
                            else if (_received_switch_in == 1 &&
                                       _received_switch_out == 0 &&
                                       _sending_switch_in == 1 &&
                                       _sending_switch_out == 1)
                            {
                                _someReceivedObject6[0]._received_switch_in = 1;
                                _someReceivedObject6[0]._received_switch_out = 1;
                                _someReceivedObject6[0]._sending_switch_in = 1;
                                _someReceivedObject6[0]._sending_switch_out = 1;
                                //System.Windows.MessageBox.Show("Flow 011", "Console");
                                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x11", 70, 21);
                            }
                            else if (_received_switch_in == 0 &&
                                    _received_switch_out == 1 &&
                                    _sending_switch_in == 1 &&
                                    _sending_switch_out == 1)
                            {
                                _someReceivedObject6[0]._received_switch_in = 1;
                                _someReceivedObject6[0]._received_switch_out = 1;
                                _someReceivedObject6[0]._sending_switch_in = 1;
                                _someReceivedObject6[0]._sending_switch_out = 1;
                                //System.Windows.MessageBox.Show("Flow 12", "Console");
                                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x12", 70, 22);
                            }
                            else if (_received_switch_in == 1 &&
                                     _received_switch_out == 1 &&
                                     _sending_switch_in == 1 &&
                                     _sending_switch_out == 0)
                            {
                                _someReceivedObject6[0]._received_switch_in = 1;
                                _someReceivedObject6[0]._received_switch_out = 1;
                                _someReceivedObject6[0]._sending_switch_in = 1;
                                _someReceivedObject6[0]._sending_switch_out = 1;
                                //System.Windows.MessageBox.Show("Flow 013", "Console");
                                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x13", 70, 23);
                            }
                            else if (_received_switch_in == 1 &&
                                    _received_switch_out == 1 &&
                                    _sending_switch_in == 0 &&
                                    _sending_switch_out == 1)
                            {
                                _someReceivedObject6[0]._received_switch_in = 1;
                                _someReceivedObject6[0]._received_switch_out = 1;
                                _someReceivedObject6[0]._sending_switch_in = 1;
                                _someReceivedObject6[0]._sending_switch_out = 1;
                                //System.Windows.MessageBox.Show("Flow 014", "Console");
                                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x14", 70, 24);
                            }
                            else if (_received_switch_in == 1 &&
                                  _received_switch_out == 1 &&
                                  _sending_switch_in == 1 &&
                                  _sending_switch_out == 1)
                            {
                                _someReceivedObject6[0]._received_switch_in = 1;
                                _someReceivedObject6[0]._received_switch_out = 1;
                                _someReceivedObject6[0]._sending_switch_in = 1;
                                _someReceivedObject6[0]._sending_switch_out = 1;
                                //System.Windows.MessageBox.Show("Flow 015", "Console");
                                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x15", 70, 25);
                            }
                        }
                    }
                }
            }

            /*for (int i = 0;i < _someReceivedObject0.Length;i++)
            {
                int _received_switch_in = _someReceivedObject0[i]._received_switch_in;
                int _received_switch_out = _someReceivedObject0[i]._received_switch_out;
                int _sending_switch_in = _someReceivedObject0[i]._sending_switch_in;
                int _sending_switch_out = _someReceivedObject0[i]._sending_switch_out;

                if (_received_switch_in == 1 &&
                    _received_switch_in == 1 &&
                    _received_switch_in == 1 &&
                    _received_switch_in == 1)
                {
                    //System.Windows.MessageBox.Show("Stop ", "Console");
                }
            }*/

            //int _received_switch_in000 = _someReceivedObject0[0]._received_switch_in;
            //int _received_switch_out000 = _someReceivedObject0[0]._received_switch_out;
            //int _sending_switch_in000 = _someReceivedObject0[0]._sending_switch_in;
            //int _sending_switch_out000 = _someReceivedObject0[0]._sending_switch_out;

            //System.Windows.MessageBox.Show("Thread is Alive 00", "Console");
            _data0 = _someReceivedObject7[0];
            int _received_switch_in000 = _data0._received_switch_in;   //1
            int _received_switch_out000 = _data0._received_switch_out; //1
            int _sending_switch_in000 = _data0._sending_switch_in;     //1
            int _sending_switch_out000 = _data0._sending_switch_out;   //1
            object[][] _chain_Of_Tasks000 = _data0._chain_Of_Tasks0;
            int _timeOut000 = _data0._timeOut0;
            int _ParentTaskThreadID000 = _data0._ParentTaskThreadID0;
            int _main_cpu_count000 = _data0._main_cpu_count;
            string _passTest000 = _data0._passTest;

            _someReceivedObject7[0]._passTest = _passTest000;

            if (_received_switch_in000 == 0 &&
            _received_switch_out000 == 0 &&
            _sending_switch_in000 == 0 &&
            _sending_switch_out000 == 0)
            {
                _someReceivedObject7[0]._received_switch_in = 0;
                _someReceivedObject7[0]._received_switch_out = 0;
                _someReceivedObject7[0]._sending_switch_in = 0;
                _someReceivedObject7[0]._sending_switch_out = 0;

                System.Windows.MessageBox.Show("Flow x00x0", "Console");
                _dispatchQueue_has_Main_Responded.Add(_someReceivedObject7[0]);
                _dispatchQueue_has_Sec_Responded.Add(_someReceivedObject7[0]);
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx0", 30, 10);
                return _someReceivedObject7[0];
            }
            else if (_received_switch_in000 == 1 &&
                     _received_switch_out000 == 0 &&
                     _sending_switch_in000 == 0 &&
                     _sending_switch_out000 == 0)
            {
                _someReceivedObject7[0]._received_switch_in = 1;
                _someReceivedObject7[0]._received_switch_out = 1;
                _someReceivedObject7[0]._sending_switch_in = 1;
                _someReceivedObject7[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 01b", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx1", 30, 11);
            }
            else if (_received_switch_in000 == 0 &&
                     _received_switch_out000 == 1 &&
                     _sending_switch_in000 == 0 &&
                     _sending_switch_out000 == 0)
            {
                _someReceivedObject7[0]._received_switch_in = 1;
                _someReceivedObject7[0]._received_switch_out = 1;
                _someReceivedObject7[0]._sending_switch_in = 1;
                _someReceivedObject7[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 02c", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx2", 30, 12);
            }
            else if (_received_switch_in000 == 0 &&
                     _received_switch_out000 == 0 &&
                     _sending_switch_in000 == 1 &&
                     _sending_switch_out000 == 0)
            {
                _someReceivedObject7[0]._received_switch_in = 1;
                _someReceivedObject7[0]._received_switch_out = 1;
                _someReceivedObject7[0]._sending_switch_in = 1;
                _someReceivedObject7[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 3", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx3", 30, 13);
            }
            else if (_received_switch_in000 == 0 &&
                   _received_switch_out000 == 0 &&
                   _sending_switch_in000 == 0 &&
                   _sending_switch_out000 == 1)
            {
                _someReceivedObject7[0]._received_switch_in = 1;
                _someReceivedObject7[0]._received_switch_out = 1;
                _someReceivedObject7[0]._sending_switch_in = 1;
                _someReceivedObject7[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 04a", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx4", 30, 14);
            }
            else if (_received_switch_in000 == 1 &&
                    _received_switch_out000 == 1 &&
                    _sending_switch_in000 == 0 &&
                    _sending_switch_out000 == 0)
            {
                //Console Waiting for response? // logic is "knock knock, Console responds whos there"
                _someReceivedObject7[0]._received_switch_in = 1;
                _someReceivedObject7[0]._received_switch_out = 0;
                _someReceivedObject7[0]._sending_switch_in = 0;
                _someReceivedObject7[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 05b", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx5", 30, 15);
                return _someReceivedObject7[0];
            }
            else if (_received_switch_in000 == 1 &&
                 _received_switch_out000 == 0 &&
                 _sending_switch_in000 == 1 &&
                 _sending_switch_out000 == 0)
            {
                _someReceivedObject7[0]._received_switch_in = 1;
                _someReceivedObject7[0]._received_switch_out = 1;
                _someReceivedObject7[0]._sending_switch_in = 1;
                _someReceivedObject7[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 6", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx6", 30, 16);
            }
            else if (_received_switch_in000 == 1 &&
                   _received_switch_out000 == 0 &&
                   _sending_switch_in000 == 0 &&
                   _sending_switch_out000 == 1)
            {
                System.Windows.MessageBox.Show("Flow 7 testing", "Console");
                _someReceivedObject7[0]._received_switch_in = 0;
                _someReceivedObject7[0]._received_switch_out = 0;
                _someReceivedObject7[0]._sending_switch_in = 0;
                _someReceivedObject7[0]._sending_switch_out = 0;
            }

            else if (_received_switch_in000 == 0 &&
                _received_switch_out000 == 1 &&
                _sending_switch_in000 == 1 &&
                _sending_switch_out000 == 0)
            {
                _someReceivedObject7[0]._received_switch_in = 1;
                _someReceivedObject7[0]._received_switch_out = 1;
                _someReceivedObject7[0]._sending_switch_in = 1;
                _someReceivedObject7[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 08c", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx8", 30, 18);
            }
            else if (_received_switch_in000 == 0 &&
                  _received_switch_out000 == 1 &&
                  _sending_switch_in000 == 0 &&
                  _sending_switch_out000 == 1)
            {
                _someReceivedObject7[0]._received_switch_in = 1;
                _someReceivedObject7[0]._received_switch_out = 1;
                _someReceivedObject7[0]._sending_switch_in = 1;
                _someReceivedObject7[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 09", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx9", 30, 19);
            }
            else if (_received_switch_in000 == 0 &&
                  _received_switch_out000 == 0 &&
                  _sending_switch_in000 == 1 &&
                  _sending_switch_out000 == 1)
            {
                _someReceivedObject7[0]._received_switch_in = 1;
                _someReceivedObject7[0]._received_switch_out = 1;
                _someReceivedObject7[0]._sending_switch_in = 1;
                _someReceivedObject7[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 10", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx10", 30, 20);
            }
            else if (_received_switch_in000 == 1 &&
                       _received_switch_out000 == 0 &&
                       _sending_switch_in000 == 1 &&
                       _sending_switch_out000 == 1)
            {
                _someReceivedObject7[0]._received_switch_in = 1;
                _someReceivedObject7[0]._received_switch_out = 1;
                _someReceivedObject7[0]._sending_switch_in = 1;
                _someReceivedObject7[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 011h", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx11", 30, 21);
            }
            else if (_received_switch_in000 == 0 &&
                    _received_switch_out000 == 1 &&
                    _sending_switch_in000 == 1 &&
                    _sending_switch_out000 == 1)
            {
                _someReceivedObject7[0]._received_switch_in = 1;
                _someReceivedObject7[0]._received_switch_out = 1;
                _someReceivedObject7[0]._sending_switch_in = 1;
                _someReceivedObject7[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 12", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx12", 30, 22);
            }
            else if (_received_switch_in000 == 1 &&
                     _received_switch_out000 == 1 &&
                     _sending_switch_in000 == 1 &&
                     _sending_switch_out000 == 0)
            {
                /*_someReceivedObject7[0]._received_switch_in = 1;
                _someReceivedObject7[0]._received_switch_out = 1;
                _someReceivedObject7[0]._sending_switch_in = 1;
                _someReceivedObject7[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 013c1", "Console");
                *///consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx13", 30, 23);
                System.Windows.MessageBox.Show("Flow 013c1", "Console");



            }
            else if (_received_switch_in000 == 1 &&
                    _received_switch_out000 == 1 &&
                    _sending_switch_in000 == 0 &&
                    _sending_switch_out000 == 1)
            {
                _someReceivedObject7[0]._received_switch_in = 1;
                _someReceivedObject7[0]._received_switch_out = 1;
                _someReceivedObject7[0]._sending_switch_in = 1;
                _someReceivedObject7[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 014", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx14", 30, 24);
            }
            else if (_received_switch_in000 == 1 &&
                  _received_switch_out000 == 1 &&
                  _sending_switch_in000 == 1 &&
                  _sending_switch_out000 == 1)
            {
                _someReceivedObject7[0]._received_switch_in = 1;
                _someReceivedObject7[0]._received_switch_out = 1;
                _someReceivedObject7[0]._sending_switch_in = 1;
                _someReceivedObject7[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 015e", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx15", 30, 25);
            }

            //_received_switch_in000 = _someReceivedObject1[0]._received_switch_in;
            //_received_switch_out000 = _someReceivedObject1[0]._received_switch_out;
            //_sending_switch_in000 = _someReceivedObject1[0]._sending_switch_in;
            //_sending_switch_out000 = _someReceivedObject1[0]._sending_switch_out;

            _data0 = (Program._someObject)_someReceivedObject1[0];
            _received_switch_in000 = _data0._received_switch_in;   //1
            _received_switch_out000 = _data0._received_switch_out; //1
            _sending_switch_in000 = _data0._sending_switch_in;     //1
            _sending_switch_out000 = _data0._sending_switch_out;   //1
            _chain_Of_Tasks000 = _data0._chain_Of_Tasks0;
            _timeOut000 = _data0._timeOut0;
            _ParentTaskThreadID000 = _data0._ParentTaskThreadID0;
            _main_cpu_count000 = _data0._main_cpu_count;
            _passTest000 = _data0._passTest;

            _someReceivedObject1[0]._passTest = _passTest000;

            if (_received_switch_in000 == 0 &&
            _received_switch_out000 == 0 &&
            _sending_switch_in000 == 0 &&
            _sending_switch_out000 == 0)
            {
                _someReceivedObject8[0]._received_switch_in = 0;
                _someReceivedObject8[0]._received_switch_out = 0;
                _someReceivedObject8[0]._sending_switch_in = 0;
                _someReceivedObject8[0]._sending_switch_out = 0;

                System.Windows.MessageBox.Show("Flow x00x", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx0", 30, 10);
            }
            else if (_received_switch_in000 == 1 &&
                     _received_switch_out000 == 0 &&
                     _sending_switch_in000 == 0 &&
                     _sending_switch_out000 == 0)
            {
                _someReceivedObject8[0]._received_switch_in = 1;
                _someReceivedObject8[0]._received_switch_out = 1;
                _someReceivedObject8[0]._sending_switch_in = 1;
                _someReceivedObject8[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 01c", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx1", 30, 11);
            }
            else if (_received_switch_in000 == 0 &&
                     _received_switch_out000 == 1 &&
                     _sending_switch_in000 == 0 &&
                     _sending_switch_out000 == 0)
            {
                _someReceivedObject8[0]._received_switch_in = 1;
                _someReceivedObject8[0]._received_switch_out = 1;
                _someReceivedObject8[0]._sending_switch_in = 1;
                _someReceivedObject8[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 02d", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx2", 30, 12);
            }
            else if (_received_switch_in000 == 0 &&
                     _received_switch_out000 == 0 &&
                     _sending_switch_in000 == 1 &&
                     _sending_switch_out000 == 0)
            {
                _someReceivedObject8[0]._received_switch_in = 1;
                _someReceivedObject8[0]._received_switch_out = 1;
                _someReceivedObject8[0]._sending_switch_in = 1;
                _someReceivedObject8[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 3", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx3", 30, 13);
            }
            else if (_received_switch_in000 == 0 &&
                   _received_switch_out000 == 0 &&
                   _sending_switch_in000 == 0 &&
                   _sending_switch_out000 == 1)
            {
                _someReceivedObject8[0]._received_switch_in = 1;
                _someReceivedObject8[0]._received_switch_out = 1;
                _someReceivedObject8[0]._sending_switch_in = 1;
                _someReceivedObject8[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 04b", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx4", 30, 14);
            }
            else if (_received_switch_in000 == 1 &&
                    _received_switch_out000 == 1 &&
                    _sending_switch_in000 == 0 &&
                    _sending_switch_out000 == 0)
            {
                //Console Waiting for response? // logic is "knock knock, Console responds whos there"
                _someReceivedObject8[0]._received_switch_in = 1;
                _someReceivedObject8[0]._received_switch_out = 0;
                _someReceivedObject8[0]._sending_switch_in = 0;
                _someReceivedObject8[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 05b", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx5", 30, 15);
                return _someReceivedObject7[0];
            }
            else if (_received_switch_in000 == 1 &&
                 _received_switch_out000 == 0 &&
                 _sending_switch_in000 == 1 &&
                 _sending_switch_out000 == 0)
            {
                _someReceivedObject8[0]._received_switch_in = 1;
                _someReceivedObject8[0]._received_switch_out = 1;
                _someReceivedObject8[0]._sending_switch_in = 1;
                _someReceivedObject8[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 6", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx6", 30, 16);
            }
            else if (_received_switch_in000 == 1 &&
                   _received_switch_out000 == 0 &&
                   _sending_switch_in000 == 0 &&
                   _sending_switch_out000 == 1)
            {
                System.Windows.MessageBox.Show("Flow 7 testing", "Console");
                _someReceivedObject8[0]._received_switch_in = 0;
                _someReceivedObject8[0]._received_switch_out = 0;
                _someReceivedObject8[0]._sending_switch_in = 0;
                _someReceivedObject8[0]._sending_switch_out = 0;
            }

            else if (_received_switch_in000 == 0 &&
                _received_switch_out000 == 1 &&
                _sending_switch_in000 == 1 &&
                _sending_switch_out000 == 0)
            {
                _someReceivedObject8[0]._received_switch_in = 1;
                _someReceivedObject8[0]._received_switch_out = 1;
                _someReceivedObject8[0]._sending_switch_in = 1;
                _someReceivedObject8[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 08c", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx8", 30, 18);
            }
            else if (_received_switch_in000 == 0 &&
                  _received_switch_out000 == 1 &&
                  _sending_switch_in000 == 0 &&
                  _sending_switch_out000 == 1)
            {
                _someReceivedObject8[0]._received_switch_in = 1;
                _someReceivedObject8[0]._received_switch_out = 1;
                _someReceivedObject8[0]._sending_switch_in = 1;
                _someReceivedObject8[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 09", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx9", 30, 19);
            }
            else if (_received_switch_in000 == 0 &&
                  _received_switch_out000 == 0 &&
                  _sending_switch_in000 == 1 &&
                  _sending_switch_out000 == 1)
            {
                _someReceivedObject8[0]._received_switch_in = 1;
                _someReceivedObject8[0]._received_switch_out = 1;
                _someReceivedObject8[0]._sending_switch_in = 1;
                _someReceivedObject8[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 10", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx10", 30, 20);
            }
            else if (_received_switch_in000 == 1 &&
                       _received_switch_out000 == 0 &&
                       _sending_switch_in000 == 1 &&
                       _sending_switch_out000 == 1)
            {
                _someReceivedObject8[0]._received_switch_in = 1;
                _someReceivedObject8[0]._received_switch_out = 1;
                _someReceivedObject8[0]._sending_switch_in = 1;
                _someReceivedObject8[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 011a", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx11", 30, 21);
            }
            else if (_received_switch_in000 == 0 &&
                    _received_switch_out000 == 1 &&
                    _sending_switch_in000 == 1 &&
                    _sending_switch_out000 == 1)
            {
                _someReceivedObject8[0]._received_switch_in = 1;
                _someReceivedObject8[0]._received_switch_out = 1;
                _someReceivedObject8[0]._sending_switch_in = 1;
                _someReceivedObject8[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 12", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx12", 30, 22);
            }
            else if (_received_switch_in000 == 1 &&
                     _received_switch_out000 == 1 &&
                     _sending_switch_in000 == 1 &&
                     _sending_switch_out000 == 0)
            {
                _someReceivedObject8[0]._received_switch_in = 1;
                _someReceivedObject8[0]._received_switch_out = 1;
                _someReceivedObject8[0]._sending_switch_in = 1;
                _someReceivedObject8[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 013c2", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx13", 30, 23);
            }
            else if (_received_switch_in000 == 1 &&
                    _received_switch_out000 == 1 &&
                    _sending_switch_in000 == 0 &&
                    _sending_switch_out000 == 1)
            {
                _someReceivedObject8[0]._received_switch_in = 1;
                _someReceivedObject8[0]._received_switch_out = 1;
                _someReceivedObject8[0]._sending_switch_in = 1;
                _someReceivedObject8[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 014", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx14", 30, 24);
            }
            else if (_received_switch_in000 == 1 &&
                  _received_switch_out000 == 1 &&
                  _sending_switch_in000 == 1 &&
                  _sending_switch_out000 == 1)
            {
                _someReceivedObject8[0]._received_switch_in = 1;
                _someReceivedObject8[0]._received_switch_out = 1;
                _someReceivedObject8[0]._sending_switch_in = 1;
                _someReceivedObject8[0]._sending_switch_out = 1;
                System.Windows.MessageBox.Show("Flow 015e", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx15", 30, 25);
            }


            _received_switch_in000 = _someReceivedObject6[0]._received_switch_in;
            _received_switch_out000 = _someReceivedObject6[0]._received_switch_out;
            _sending_switch_in000 = _someReceivedObject6[0]._sending_switch_in;
            _sending_switch_out000 = _someReceivedObject6[0]._sending_switch_out;

            if (_received_switch_in000 == 0 &&
                _received_switch_in000 == 0 &&
                _received_switch_in000 == 0 &&
                _received_switch_in000 == 0)
            {
                System.Windows.MessageBox.Show("Stop 998", "Console");

            }
            else if (_received_switch_in000 == 1 &&
                     _received_switch_in000 == 1 &&
                     _received_switch_in000 == 1 &&
                     _received_switch_in000 == 1)
            {
                System.Windows.MessageBox.Show("Stop 997", "Console");
            }
            else if (_received_switch_in000 == 1 &&
                  _received_switch_in000 == 1 &&
                  _received_switch_in000 == 0 &&
                  _received_switch_in000 == 0)
            {
                System.Windows.MessageBox.Show("Stop 996", "Console");
            }

            _data0 = (Program._someObject)_someReceivedObject3[0];

            _received_switch_in000 = _someReceivedObject3[0]._received_switch_in;
            _received_switch_out000 = _someReceivedObject3[0]._received_switch_out;
            _sending_switch_in000 = _someReceivedObject3[0]._sending_switch_in;
            _sending_switch_out000 = _someReceivedObject3[0]._sending_switch_out;
            _chain_Of_Tasks000 = _someReceivedObject3[0]._chain_Of_Tasks0;
            _timeOut000 = _someReceivedObject3[0]._timeOut0;
            _ParentTaskThreadID000 = _someReceivedObject3[0]._ParentTaskThreadID0;
            _main_cpu_count000 = _someReceivedObject3[0]._main_cpu_count;

            if (_received_switch_in000 == 0 &&
                _received_switch_out000 == 0 &&
                _sending_switch_in000 == 0 &&
                _sending_switch_out000 == 0)
            {
                //THE FOLLOWING CAN BE USED TO REMOVE CONSOLE FLOW
                //_data0._received_switch_in = 0;
                System.Windows.MessageBox.Show("STOPPPPPP", "Console");
                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("testing", 0, 11);
                return null;
            }
            else if (_received_switch_in000 == 1 &&
                     _received_switch_out000 == 0 &&
                     _sending_switch_in000 == 0 &&
                     _sending_switch_out000 == 0)
            {
                //System.Windows.MessageBox.Show("Flow 01", "Console");
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x1c", 70, 11);




            }
            else if (_received_switch_in000 == 0 &&
                     _received_switch_out000 == 1 &&
                     _sending_switch_in000 == 0 &&
                     _sending_switch_out000 == 0)
            {
                //System.Windows.MessageBox.Show("Flow 02", "Console");
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x2", 70, 12);
            }
            else if (_received_switch_in000 == 0 &&
                     _received_switch_out000 == 0 &&
                     _sending_switch_in000 == 1 &&
                     _sending_switch_out000 == 0)
            {
                //System.Windows.MessageBox.Show("Flow 3", "Console");
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x3", 70, 13);
            }
            else if (_received_switch_in000 == 0 &&
                   _received_switch_out000 == 0 &&
                   _sending_switch_in000 == 0 &&
                   _sending_switch_out000 == 1)
            {
                //System.Windows.MessageBox.Show("Flow 04", "Console");
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x4", 70, 14);
            }
            else if (_received_switch_in000 == 1 &&
                    _received_switch_out000 == 1 &&
                    _sending_switch_in000 == 0 &&
                    _sending_switch_out000 == 0)
            {
                //System.Windows.MessageBox.Show("Flow 05", "Console");
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x5", 70, 15);
            }
            else if (_received_switch_in000 == 1 &&
                 _received_switch_out000 == 0 &&
                 _sending_switch_in000 == 1 &&
                 _sending_switch_out000 == 0)
            {
                //System.Windows.MessageBox.Show("Flow 6", "Console");
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x6", 70, 16);
            }
            else if (_received_switch_in000 == 1 &&
                   _received_switch_out000 == 0 &&
                   _sending_switch_in000 == 0 &&
                   _sending_switch_out000 == 1)
            {
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x7", 70, 17);
                //System.Windows.MessageBox.Show("Flow 07", "Console");
                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("ThreadStart Performance: " + _performanceWatch.Elapsed.Ticks, 0, 15);
            }
            else if (_received_switch_in000 == 0 &&
                _received_switch_out000 == 1 &&
                _sending_switch_in000 == 1 &&
                _sending_switch_out000 == 0)
            {
                //System.Windows.MessageBox.Show("Flow 08", "Console");
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x8d", 70, 18);
            }
            else if (_received_switch_in000 == 0 &&
                  _received_switch_out000 == 1 &&
                  _sending_switch_in000 == 0 &&
                  _sending_switch_out000 == 1)
            {
                //System.Windows.MessageBox.Show("Flow 09", "Console");
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x9", 70, 19);
            }
            else if (_received_switch_in000 == 0 &&
                  _received_switch_out000 == 0 &&
                  _sending_switch_in000 == 1 &&
                  _sending_switch_out000 == 1)
            {
                //System.Windows.MessageBox.Show("Flow 10", "Console");
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x10", 70, 20);
            }
            else if (_received_switch_in000 == 1 &&
                       _received_switch_out000 == 0 &&
                       _sending_switch_in000 == 1 &&
                       _sending_switch_out000 == 1)
            {
                //System.Windows.MessageBox.Show("Flow 011", "Console");
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x11", 70, 21);
            }
            else if (_received_switch_in000 == 0 &&
                    _received_switch_out000 == 1 &&
                    _sending_switch_in000 == 1 &&
                    _sending_switch_out000 == 1)
            {
                //System.Windows.MessageBox.Show("Flow 12", "Console");
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x12", 70, 22);
            }
            else if (_received_switch_in000 == 1 &&
                     _received_switch_out000 == 1 &&
                     _sending_switch_in000 == 1 &&
                     _sending_switch_out000 == 0)
            {
                //System.Windows.MessageBox.Show("Flow 013", "Console");
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x13", 70, 23);
            }
            else if (_received_switch_in000 == 1 &&
                    _received_switch_out000 == 1 &&
                    _sending_switch_in000 == 0 &&
                    _sending_switch_out000 == 1)
            {
                //System.Windows.MessageBox.Show("Flow 014", "Console");
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x14", 70, 24);
            }
            else if (_received_switch_in000 == 1 &&
                  _received_switch_out000 == 1 &&
                  _sending_switch_in000 == 1 &&
                  _sending_switch_out000 == 1)
            {
                //System.Windows.MessageBox.Show("Flow 015", "Console");
                consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x15", 70, 25);
            }
            return _mainObjectToReturn;
        }



















        public void _createConsole()
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

            //System.Windows.MessageBox.Show("is NOT null", "Console");
            /////////////////////////////////////////////////////////
            ////////////////////CONSOLE CREATED//////////////////////
            /////////////////////////////////////////////////////////            
        }

        static string _answerYN = "";

        public object _DoWork_hasTaskWorked(object _received_object) //async Task 
        {

            //System.Windows.MessageBox.Show("_t1", "Console");
            SC_SkYaRk_VR_Editionv002.Program._someObject _data0;

            if (_received_object is Program._someObject)
            {
                _data0 = (Program._someObject)_received_object;
                int _received_switch_in00 = _data0._received_switch_in;   //1
                int _received_switch_out00 = _data0._received_switch_out; //1
                int _sending_switch_in00 = _data0._sending_switch_in;     //1
                int _sending_switch_out00 = _data0._sending_switch_out;   //1
                object[][] _chain_Of_Tasks00 = _data0._chain_Of_Tasks0;
                int _timeOut00 = _data0._timeOut0;
                int _ParentTaskThreadID00 = _data0._ParentTaskThreadID0;
                int _main_cpu_count00 = _data0._main_cpu_count;

                if (_received_switch_in00 == 0 &&
                    _received_switch_out00 == 0 &&
                    _sending_switch_in00 == 0 &&
                    _sending_switch_out00 == 0)
                {
                    _someReceivedObject5[0]._received_switch_in = 0;
                    _someReceivedObject5[0]._received_switch_out = 0;
                    _someReceivedObject5[0]._sending_switch_in = 0;
                    _someReceivedObject5[0]._sending_switch_out = 0;
                    System.Windows.MessageBox.Show("Flow 00y", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxx0", 40, 10);
                }
                else if (_received_switch_in00 == 1 &&
                         _received_switch_out00 == 0 &&
                         _sending_switch_in00 == 0 &&
                         _sending_switch_out00 == 0)
                {
                    _someReceivedObject5[0]._received_switch_in = 1;
                    _someReceivedObject5[0]._received_switch_out = 1;
                    _someReceivedObject5[0]._sending_switch_in = 0;
                    _someReceivedObject5[0]._sending_switch_out = 0;

                    /*//consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Console is. Request to start", 0, 0);
                    System.Windows.MessageBox.Show("quit_application_?" + " Respond with Yes or No", "Console"); // nope
                    //System.Windows.MessageBox.Show("Console is. Request to start", "Console");

                    //programStart
                    _answerYN = Console.ReadLine();

                    if (_answerYN.ToLower() == "yes" || _answerYN.ToLower() == "y")
                    {
                        //consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("Console is. Request to start. Aproved.", 0, 3);
                        //consoleMessageQueue _consoleMessageQueue01 = new consoleMessageQueue("Console is. Starting Work", 0, 4);
                        //_dispatchQueue_has_Main_Responded.Add(_data0);                           
                        //_ResultsOfTasks1 = _DoWork_MainTask(_data0);
                        _data0._received_switch_in = 0;
                        _data0._received_switch_out = 0;
                        _data0._sending_switch_in = 0;
                        _data0._sending_switch_out = 0;

                        _received_object = (object)_data0;
                        System.Windows.MessageBox.Show("CONSOLE WAIT 00", "Console");
                    }
                    else
                    {
                        _someReceivedObject5[0]._received_switch_in = 0;
                        _someReceivedObject5[0]._received_switch_out = 0;
                        _someReceivedObject5[0]._sending_switch_in = 0;
                        _someReceivedObject5[0]._sending_switch_out = 0;
                        System.Windows.MessageBox.Show("quitting_console?", "Console"); // nope
                    }*/
                }
                else if (_received_switch_in00 == 0 &&
                         _received_switch_out00 == 1 &&
                         _sending_switch_in00 == 0 &&
                         _sending_switch_out00 == 0)
                {
                    _someReceivedObject5[0]._received_switch_in = 1;
                    _someReceivedObject5[0]._received_switch_out = 1;
                    _someReceivedObject5[0]._sending_switch_in = 1;
                    _someReceivedObject5[0]._sending_switch_out = 1;
                    System.Windows.MessageBox.Show("Flow 02e", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxx2", 40, 12);
                }
                else if (_received_switch_in00 == 0 &&
                         _received_switch_out00 == 0 &&
                         _sending_switch_in00 == 1 &&
                         _sending_switch_out00 == 0)
                {
                    _someReceivedObject5[0]._received_switch_in = 1;
                    _someReceivedObject5[0]._received_switch_out = 1;
                    _someReceivedObject5[0]._sending_switch_in = 1;
                    _someReceivedObject5[0]._sending_switch_out = 1;
                    System.Windows.MessageBox.Show("Flow 3", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxx3", 40, 13);
                }
                else if (_received_switch_in00 == 0 &&
                       _received_switch_out00 == 0 &&
                       _sending_switch_in00 == 0 &&
                       _sending_switch_out00 == 1)
                {
                    _someReceivedObject5[0]._received_switch_in = 1;
                    _someReceivedObject5[0]._received_switch_out = 1;
                    _someReceivedObject5[0]._sending_switch_in = 1;
                    _someReceivedObject5[0]._sending_switch_out = 1;
                    System.Windows.MessageBox.Show("Flow 04c", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxx4", 40, 14);
                }
                else if (_received_switch_in00 == 1 &&
                        _received_switch_out00 == 1 &&
                        _sending_switch_in00 == 0 &&
                        _sending_switch_out00 == 0)
                {
                    _someReceivedObject5[0]._received_switch_in = 1;
                    _someReceivedObject5[0]._received_switch_out = 0;
                    _someReceivedObject5[0]._sending_switch_in = 1;
                    _someReceivedObject5[0]._sending_switch_out = 0;
                    //System.Windows.MessageBox.Show("ask question", "Console");

                    System.Windows.MessageBox.Show("Flow 05a", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxx5", 30, 15);
                }
                else if (_received_switch_in00 == 1 &&
                     _received_switch_out00 == 0 &&
                     _sending_switch_in00 == 1 &&
                     _sending_switch_out00 == 0)
                {
                    _someReceivedObject5[0]._received_switch_in = 1;
                    _someReceivedObject5[0]._received_switch_out = 1;
                    _someReceivedObject5[0]._sending_switch_in = 1;
                    _someReceivedObject5[0]._sending_switch_out = 1;
                    System.Windows.MessageBox.Show("Flow 6", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxx6", 40, 16);
                }
                else if (_received_switch_in00 == 1 &&
                       _received_switch_out00 == 0 &&
                       _sending_switch_in00 == 0 &&
                       _sending_switch_out00 == 1)
                {
                    _someReceivedObject5[0]._received_switch_in = 1;
                    _someReceivedObject5[0]._received_switch_out = 1;
                    _someReceivedObject5[0]._sending_switch_in = 1;
                    _someReceivedObject5[0]._sending_switch_out = 1;
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxx7", 40, 17);
                    System.Windows.MessageBox.Show("Flow 07b", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("ThreadStart Performance: " + _performanceWatch.Elapsed.Ticks, 0, 15);
                }
                else if (_received_switch_in00 == 0 &&
                    _received_switch_out00 == 1 &&
                    _sending_switch_in00 == 1 &&
                    _sending_switch_out00 == 0)
                {
                    _someReceivedObject5[0]._received_switch_in = 1;
                    _someReceivedObject5[0]._received_switch_out = 1;
                    _someReceivedObject5[0]._sending_switch_in = 1;
                    _someReceivedObject5[0]._sending_switch_out = 1;
                    System.Windows.MessageBox.Show("Flow 08d", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxx8", 40, 18);
                }
                else if (_received_switch_in00 == 0 &&
                      _received_switch_out00 == 1 &&
                      _sending_switch_in00 == 0 &&
                      _sending_switch_out00 == 1)
                {
                    _someReceivedObject5[0]._received_switch_in = 1;
                    _someReceivedObject5[0]._received_switch_out = 1;
                    _someReceivedObject5[0]._sending_switch_in = 1;
                    _someReceivedObject5[0]._sending_switch_out = 1;
                    System.Windows.MessageBox.Show("Flow 09", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxx9", 40, 19);
                }
                else if (_received_switch_in00 == 0 &&
                      _received_switch_out00 == 0 &&
                      _sending_switch_in00 == 1 &&
                      _sending_switch_out00 == 1)
                {
                    _someReceivedObject5[0]._received_switch_in = 1;
                    _someReceivedObject5[0]._received_switch_out = 1;
                    _someReceivedObject5[0]._sending_switch_in = 1;
                    _someReceivedObject5[0]._sending_switch_out = 1;
                    System.Windows.MessageBox.Show("Flow 10", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxx10", 40, 20);
                }
                else if (_received_switch_in00 == 1 &&
                           _received_switch_out00 == 0 &&
                           _sending_switch_in00 == 1 &&
                           _sending_switch_out00 == 1)
                {
                    _someReceivedObject5[0]._received_switch_in = 1;
                    _someReceivedObject5[0]._received_switch_out = 1;
                    _someReceivedObject5[0]._sending_switch_in = 1;
                    _someReceivedObject5[0]._sending_switch_out = 1;
                    System.Windows.MessageBox.Show("Flow 011b", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxx11", 40, 21);
                }
                else if (_received_switch_in00 == 0 &&
                        _received_switch_out00 == 1 &&
                        _sending_switch_in00 == 1 &&
                        _sending_switch_out00 == 1)
                {
                    _someReceivedObject5[0]._received_switch_in = 1;
                    _someReceivedObject5[0]._received_switch_out = 1;
                    _someReceivedObject5[0]._sending_switch_in = 1;
                    _someReceivedObject5[0]._sending_switch_out = 1;
                    System.Windows.MessageBox.Show("Flow 12", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxx12", 40, 22);
                }
                else if (_received_switch_in00 == 1 &&
                         _received_switch_out00 == 1 &&
                         _sending_switch_in00 == 1 &&
                         _sending_switch_out00 == 0)
                {
                    _someReceivedObject5[0]._received_switch_in = 1;
                    _someReceivedObject5[0]._received_switch_out = 1;
                    _someReceivedObject5[0]._sending_switch_in = 1;
                    _someReceivedObject5[0]._sending_switch_out = 1;
                    System.Windows.MessageBox.Show("Flow 013d", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxx13", 40, 23);
                }
                else if (_received_switch_in00 == 1 &&
                        _received_switch_out00 == 1 &&
                        _sending_switch_in00 == 0 &&
                        _sending_switch_out00 == 1)
                {
                    _someReceivedObject5[0]._received_switch_in = 1;
                    _someReceivedObject5[0]._received_switch_out = 1;
                    _someReceivedObject5[0]._sending_switch_in = 1;
                    _someReceivedObject5[0]._sending_switch_out = 1;
                    System.Windows.MessageBox.Show("Flow 014", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxx14", 40, 24);
                }
                else if (_received_switch_in00 == 1 &&
                      _received_switch_out00 == 1 &&
                      _sending_switch_in00 == 1 &&
                      _sending_switch_out00 == 1)
                {
                    _someReceivedObject5[0]._received_switch_in = 1;
                    _someReceivedObject5[0]._received_switch_out = 1;
                    _someReceivedObject5[0]._sending_switch_in = 1;
                    _someReceivedObject5[0]._sending_switch_out = 1;
                    System.Windows.MessageBox.Show("Flow 015a", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxx15", 40, 25);
                }

            }







        _taskLooper:

            consoleMessageQueue _consoleMessageQueue0000 = new consoleMessageQueue("▼", 10, 10);
            if (_dispatchQueue_has_Main_Responded.Count > 0)
            {
                _data0 = (Program._someObject)_dispatchQueue_has_Main_Responded[0];
                _dispatchQueue_has_Main_Responded.Remove(_dispatchQueue_has_Main_Responded[0]);

                int _received_switch_in00 = _data0._received_switch_in;   //1
                int _received_switch_out00 = _data0._received_switch_out; //1
                int _sending_switch_in00 = _data0._sending_switch_in;     //1
                int _sending_switch_out00 = _data0._sending_switch_out;   //1
                object[][] _chain_Of_Tasks00 = _data0._chain_Of_Tasks0;
                int _timeOut00 = _data0._timeOut0;
                int _ParentTaskThreadID00 = _data0._ParentTaskThreadID0;
                int _main_cpu_count00 = _data0._main_cpu_count;

                _someReceivedObject9[0]._passTest = _data0._passTest;

                if (_received_switch_in00 == 0 &&
                    _received_switch_out00 == 0 &&
                    _sending_switch_in00 == 0 &&
                    _sending_switch_out00 == 0)
                {
                    System.Windows.MessageBox.Show("Flow r00r", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxxx0", 50, 10);
                    _someReceivedObject9[0]._received_switch_in = 0;
                    _someReceivedObject9[0]._received_switch_out = 0;
                    _someReceivedObject9[0]._sending_switch_in = 1;
                    _someReceivedObject9[0]._sending_switch_out = 1;

                    //return _someReceivedObject9[0];
                    //_CLEARING_CONSOLE
                    //_dispatchQueue_has_Main_Responded.Clear();
                    //_dispatchQueue_has_Sec_Responded.Clear();
                }
                else if (_received_switch_in00 == 1 &&
                         _received_switch_out00 == 0 &&
                         _sending_switch_in00 == 0 &&
                         _sending_switch_out00 == 0)
                {
                    System.Windows.MessageBox.Show("Flow 01d", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxxx1", 50, 11);
                    _someReceivedObject9[0]._received_switch_in = 0;
                    _someReceivedObject9[0]._received_switch_out = 0;
                    _someReceivedObject9[0]._sending_switch_in = 0;
                    _someReceivedObject9[0]._sending_switch_out = 0;
                }
                else if (_received_switch_in00 == 0 &&
                         _received_switch_out00 == 1 &&
                         _sending_switch_in00 == 0 &&
                         _sending_switch_out00 == 0)
                {
                    System.Windows.MessageBox.Show("Flow 02f", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxxx2", 50, 12);
                    _someReceivedObject9[0]._received_switch_in = 0;
                    _someReceivedObject9[0]._received_switch_out = 0;
                    _someReceivedObject9[0]._sending_switch_in = 0;
                    _someReceivedObject9[0]._sending_switch_out = 0;
                }
                else if (_received_switch_in00 == 0 &&
                         _received_switch_out00 == 0 &&
                         _sending_switch_in00 == 1 &&
                         _sending_switch_out00 == 0)
                {
                    System.Windows.MessageBox.Show("Flow 3", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxxx3", 50, 13);
                    _someReceivedObject9[0]._received_switch_in = 0;
                    _someReceivedObject9[0]._received_switch_out = 0;
                    _someReceivedObject9[0]._sending_switch_in = 0;
                    _someReceivedObject9[0]._sending_switch_out = 0;
                }
                else if (_received_switch_in00 == 0 &&
                       _received_switch_out00 == 0 &&
                       _sending_switch_in00 == 0 &&
                       _sending_switch_out00 == 1)
                {
                    System.Windows.MessageBox.Show("Flow 04d", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxxx4", 50, 14);
                    _someReceivedObject9[0]._received_switch_in = 0;
                    _someReceivedObject9[0]._received_switch_out = 0;
                    _someReceivedObject9[0]._sending_switch_in = 0;
                    _someReceivedObject9[0]._sending_switch_out = 0;
                }
                else if (_received_switch_in00 == 1 &&
                        _received_switch_out00 == 1 &&
                        _sending_switch_in00 == 0 &&
                        _sending_switch_out00 == 0)
                {
                    System.Windows.MessageBox.Show("Flow 05", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxxx5", 50, 15);
                    _someReceivedObject9[0]._received_switch_in = 0;
                    _someReceivedObject9[0]._received_switch_out = 0;
                    _someReceivedObject9[0]._sending_switch_in = 0;
                    _someReceivedObject9[0]._sending_switch_out = 0;
                }
                else if (_received_switch_in00 == 1 &&
                     _received_switch_out00 == 0 &&
                     _sending_switch_in00 == 1 &&
                     _sending_switch_out00 == 0)
                {
                    System.Windows.MessageBox.Show("Flow 6", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxxx6", 50, 16);
                    _someReceivedObject9[0]._received_switch_in = 0;
                    _someReceivedObject9[0]._received_switch_out = 0;
                    _someReceivedObject9[0]._sending_switch_in = 0;
                    _someReceivedObject9[0]._sending_switch_out = 0;
                }
                else if (_received_switch_in00 == 1 &&
                       _received_switch_out00 == 0 &&
                       _sending_switch_in00 == 0 &&
                       _sending_switch_out00 == 1)
                {
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxxx7", 50, 17);
                    System.Windows.MessageBox.Show("Flow 07", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("ThreadStart Performance: " + _performanceWatch.Elapsed.Ticks, 0, 15);
                    _someReceivedObject9[0]._received_switch_in = 0;
                    _someReceivedObject9[0]._received_switch_out = 0;
                    _someReceivedObject9[0]._sending_switch_in = 0;
                    _someReceivedObject9[0]._sending_switch_out = 0;
                }
                else if (_received_switch_in00 == 0 &&
                    _received_switch_out00 == 1 &&
                    _sending_switch_in00 == 1 &&
                    _sending_switch_out00 == 0)
                {
                    System.Windows.MessageBox.Show("Flow 08", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxxx8", 50, 18);
                    _someReceivedObject9[0]._received_switch_in = 0;
                    _someReceivedObject9[0]._received_switch_out = 0;
                    _someReceivedObject9[0]._sending_switch_in = 0;
                    _someReceivedObject9[0]._sending_switch_out = 0;
                }
                else if (_received_switch_in00 == 0 &&
                      _received_switch_out00 == 1 &&
                      _sending_switch_in00 == 0 &&
                      _sending_switch_out00 == 1)
                {
                    System.Windows.MessageBox.Show("Flow 09", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxxx9", 50, 19);
                    _someReceivedObject9[0]._received_switch_in = 0;
                    _someReceivedObject9[0]._received_switch_out = 0;
                    _someReceivedObject9[0]._sending_switch_in = 0;
                    _someReceivedObject9[0]._sending_switch_out = 0;
                }
                else if (_received_switch_in00 == 0 &&
                      _received_switch_out00 == 0 &&
                      _sending_switch_in00 == 1 &&
                      _sending_switch_out00 == 1)
                {
                    System.Windows.MessageBox.Show("Flow 10", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxxx10", 50, 20);
                    _someReceivedObject9[0]._received_switch_in = 0;
                    _someReceivedObject9[0]._received_switch_out = 0;
                    _someReceivedObject9[0]._sending_switch_in = 0;
                    _someReceivedObject9[0]._sending_switch_out = 0;
                }
                else if (_received_switch_in00 == 1 &&
                           _received_switch_out00 == 0 &&
                           _sending_switch_in00 == 1 &&
                           _sending_switch_out00 == 1)
                {
                    System.Windows.MessageBox.Show("Flow 011c", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxxx11", 50, 21);
                    _someReceivedObject9[0]._received_switch_in = 0;
                    _someReceivedObject9[0]._received_switch_out = 0;
                    _someReceivedObject9[0]._sending_switch_in = 0;
                    _someReceivedObject9[0]._sending_switch_out = 0;
                }
                else if (_received_switch_in00 == 0 &&
                        _received_switch_out00 == 1 &&
                        _sending_switch_in00 == 1 &&
                        _sending_switch_out00 == 1)
                {
                    System.Windows.MessageBox.Show("Flow 12", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxxx12", 50, 22);
                    _someReceivedObject9[0]._received_switch_in = 0;
                    _someReceivedObject9[0]._received_switch_out = 0;
                    _someReceivedObject9[0]._sending_switch_in = 0;
                    _someReceivedObject9[0]._sending_switch_out = 0;
                }
                else if (_received_switch_in00 == 1 &&
                         _received_switch_out00 == 1 &&
                         _sending_switch_in00 == 1 &&
                         _sending_switch_out00 == 0)
                {
                    System.Windows.MessageBox.Show("Flow 013", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxxx13", 50, 23);
                    _someReceivedObject9[0]._received_switch_in = 0;
                    _someReceivedObject9[0]._received_switch_out = 0;
                    _someReceivedObject9[0]._sending_switch_in = 0;
                    _someReceivedObject9[0]._sending_switch_out = 0;
                }
                else if (_received_switch_in00 == 1 &&
                        _received_switch_out00 == 1 &&
                        _sending_switch_in00 == 0 &&
                        _sending_switch_out00 == 1)
                {
                    System.Windows.MessageBox.Show("Flow 014", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxxx14", 50, 24);
                    _someReceivedObject9[0]._received_switch_in = 0;
                    _someReceivedObject9[0]._received_switch_out = 0;
                    _someReceivedObject9[0]._sending_switch_in = 0;
                    _someReceivedObject9[0]._sending_switch_out = 0;
                }
                else if (_received_switch_in00 == 1 &&
                      _received_switch_out00 == 1 &&
                      _sending_switch_in00 == 1 &&
                      _sending_switch_out00 == 1)
                {
                    System.Windows.MessageBox.Show("Flow 015", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow xxxx15", 50, 25);
                    _someReceivedObject9[0]._received_switch_in = 0;
                    _someReceivedObject9[0]._received_switch_out = 0;
                    _someReceivedObject9[0]._sending_switch_in = 0;
                    _someReceivedObject9[0]._sending_switch_out = 0;


                    //_CLEARING_CONSOLE
                    //_dispatchQueue_has_Main_Responded.Clear();
                    //_dispatchQueue_has_Sec_Responded.Clear();
                }
            }

            if (_dispatchQueue.Count > 0)
            {
                if (_dispatchQueue[0] is Program._someObject)
                {
                    _data0 = (Program._someObject)_dispatchQueue[0];
                    _dispatchQueue.Remove(_dispatchQueue[0]);

                    int _received_switch_in00 = _data0._received_switch_in;   //1
                    int _received_switch_out00 = _data0._received_switch_out; //1
                    int _sending_switch_in00 = _data0._sending_switch_in;     //1
                    int _sending_switch_out00 = _data0._sending_switch_out;   //1
                    object[][] _chain_Of_Tasks00 = _data0._chain_Of_Tasks0;
                    int _timeOut00 = _data0._timeOut0;
                    int _ParentTaskThreadID00 = _data0._ParentTaskThreadID0;
                    int _main_cpu_count00 = _data0._main_cpu_count;

                    if (_received_switch_in00 == 0 &&
                        _received_switch_out00 == 0 &&
                        _sending_switch_in00 == 0 &&
                        _sending_switch_out00 == 0)
                    {
                        System.Windows.MessageBox.Show("Flow 00r", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("xxxxx0", 60, 10);
                    }
                    else if (_received_switch_in00 == 1 &&
                             _received_switch_out00 == 0 &&
                             _sending_switch_in00 == 0 &&
                             _sending_switch_out00 == 0)
                    {
                        System.Windows.MessageBox.Show("Flow 01e", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("xxxxx1", 60, 11);
                    }
                    else if (_received_switch_in00 == 0 &&
                             _received_switch_out00 == 1 &&
                             _sending_switch_in00 == 0 &&
                             _sending_switch_out00 == 0)
                    {
                        System.Windows.MessageBox.Show("Flow 02g", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("xxxxx2", 60, 12);
                    }
                    else if (_received_switch_in00 == 0 &&
                             _received_switch_out00 == 0 &&
                             _sending_switch_in00 == 1 &&
                             _sending_switch_out00 == 0)
                    {
                        System.Windows.MessageBox.Show("Flow 3", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("xxxxx3", 60, 13);
                    }
                    else if (_received_switch_in00 == 0 &&
                           _received_switch_out00 == 0 &&
                           _sending_switch_in00 == 0 &&
                           _sending_switch_out00 == 1)
                    {
                        System.Windows.MessageBox.Show("Flow 04e", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("xxxxx4", 60, 14);
                    }
                    else if (_received_switch_in00 == 1 &&
                            _received_switch_out00 == 1 &&
                            _sending_switch_in00 == 0 &&
                            _sending_switch_out00 == 0)
                    {
                        System.Windows.MessageBox.Show("Flow 055", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("xxxxx5", 60, 15);
                    }
                    else if (_received_switch_in00 == 1 &&
                         _received_switch_out00 == 0 &&
                         _sending_switch_in00 == 1 &&
                         _sending_switch_out00 == 0)
                    {
                        System.Windows.MessageBox.Show("Flow 6", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("xxxxx6", 60, 16);
                    }
                    else if (_received_switch_in00 == 1 &&
                           _received_switch_out00 == 0 &&
                           _sending_switch_in00 == 0 &&
                           _sending_switch_out00 == 1)
                    {
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("xxxxx7", 60, 17);
                        System.Windows.MessageBox.Show("Flow 07c", "Console");
                        ////consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("ThreadStart Performance: " + _performanceWatch.Elapsed.Ticks, 0, 15);
                    }
                    else if (_received_switch_in00 == 0 &&
                        _received_switch_out00 == 1 &&
                        _sending_switch_in00 == 1 &&
                        _sending_switch_out00 == 0)
                    {
                        System.Windows.MessageBox.Show("Flow 08e", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("xxxxx8", 60, 18);



                    }
                    else if (_received_switch_in00 == 0 &&
                          _received_switch_out00 == 1 &&
                          _sending_switch_in00 == 0 &&
                          _sending_switch_out00 == 1)
                    {
                        System.Windows.MessageBox.Show("Flow 09", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("xxxxx9", 60, 19);
                    }
                    else if (_received_switch_in00 == 0 &&
                          _received_switch_out00 == 0 &&
                          _sending_switch_in00 == 1 &&
                          _sending_switch_out00 == 1)
                    {
                        System.Windows.MessageBox.Show("Flow 10", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("xxxxx10", 60, 20);
                    }
                    else if (_received_switch_in00 == 1 &&
                               _received_switch_out00 == 0 &&
                               _sending_switch_in00 == 1 &&
                               _sending_switch_out00 == 1)
                    {
                        System.Windows.MessageBox.Show("Flow 011d", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("xxxxx11", 60, 21);
                    }
                    else if (_received_switch_in00 == 0 &&
                            _received_switch_out00 == 1 &&
                            _sending_switch_in00 == 1 &&
                            _sending_switch_out00 == 1)
                    {
                        System.Windows.MessageBox.Show("Flow 12", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("xxxxx12", 60, 22);
                    }
                    else if (_received_switch_in00 == 1 &&
                             _received_switch_out00 == 1 &&
                             _sending_switch_in00 == 1 &&
                             _sending_switch_out00 == 0)
                    {
                        System.Windows.MessageBox.Show("Flow 013e", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("xxxxx13", 60, 23);
                    }
                    else if (_received_switch_in00 == 1 &&
                            _received_switch_out00 == 1 &&
                            _sending_switch_in00 == 0 &&
                            _sending_switch_out00 == 1)
                    {
                        System.Windows.MessageBox.Show("Flow 014", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("xxxxx14", 60, 24);
                    }
                    else if (_received_switch_in00 == 1 &&
                          _received_switch_out00 == 1 &&
                          _sending_switch_in00 == 1 &&
                          _sending_switch_out00 == 1)
                    {
                        System.Windows.MessageBox.Show("Flow 015b", "Console");
                        //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("xxxxx15", 60, 25);
                    }
                }
            }








            /*//THIS FIRST SWITCH IS TO INITIALIZE THE PROGRAM. AT LEAST LETS START WITH THAT.
            if (_received_switch_in00 == 1 &&
                _received_switch_out00 == 0 &&
                _sending_switch_in00 == 1 &&
                _sending_switch_out00 == 1)
            {
                //THE FOLLOWING CAN BE USED TO REMOVE CONSOLE FLOW
                //_data0._received_switch_in = 0;
                //System.Windows.MessageBox.Show("removing Flow", "Console");
                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("testing", 0, 11);
                _data0._received_switch_in = 0;
                _data0._received_switch_out = 0;
                _data0._sending_switch_in = 0;
                _data0._sending_switch_out = 0;

                _received_object = (object)_data0;
                return _received_object;
            }
            else if (_received_switch_in00 == 1 &&
                     _received_switch_out00 == 1 &&
                     _sending_switch_in00 == 1 &&
                     _sending_switch_out00 == 1)
            {

                //THE FOLLOWING CAN BE USED TO REMOVE CONSOLE FLOW
                //_data0._received_switch_in = 0;
                //System.Windows.MessageBox.Show("removing Flow", "Console");
                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("testing", 0, 11);
                _data0._received_switch_in = 0;
                _data0._received_switch_out = 0;
                _data0._sending_switch_in = 0;
                _data0._sending_switch_out = 0;

                _received_object = (object)_data0;
                return _received_object;
            }*/



            /*lock (_dispatchQueue_has_Sec_Responded)
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

                    }
                }
            }*/
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
                        consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Choose VR", 0, 2);

                        //programStart
                        _user_name = Console.ReadLine();

                        if (_user_name.ToLower() == "yes" || _user_name.ToLower() == "y")
                        {
                            consoleMessageQueue _consoleMessageQueue00 = new consoleMessageQueue("entering VR", 0, 3);

                            //_dispatchQueue_has_Main_Responded.Add(_data0);                           
                            //_ResultsOfTasks1 = _DoWork_MainTask(_data0);
                            _data0._received_switch_in = 1;
                            _data0._received_switch_out = 1;
                            _data0._sending_switch_in = 1;
                            _data0._sending_switch_out = 0;

                            _received_object = (object)_data0;
                            _dispatchQueue_has_Sec_Responded.Remove(_dispatchQueue_has_Sec_Responded[0]);

                            return _received_object;
                        }
                    }
                    /*else if (_received_switch_in00 == 0 &&
                        _received_switch_out00 == 0 &&
                        _sending_switch_in00 == 0 &&
                        _sending_switch_out00 == 0)
                    {
                        _data0._received_switch_in = 1;
                        _data0._received_switch_out = 1;
                        _data0._sending_switch_in = 1;
                        _data0._sending_switch_out = 1;

                        _received_object = (object)_data0;
                        _dispatchQueue_has_Sec_Responded.Remove(_dispatchQueue_has_Sec_Responded[0]);

                        return _received_object;
                    }
                    else if (_received_switch_in00 == 1 &&
                     _received_switch_out00 == 1 &&
                     _sending_switch_in00 == 1 &&
                     _sending_switch_out00 == 1)
                    {
                        _data0._received_switch_in = 1;
                        _data0._received_switch_out = 1;
                        _data0._sending_switch_in = 1;
                        _data0._sending_switch_out = 1;

                        _received_object = (object)_data0;
                        _dispatchQueue_has_Sec_Responded.Remove(_dispatchQueue_has_Sec_Responded[0]);

                        return _received_object;
                    }

                    _dispatchQueue_has_Sec_Responded.Remove(_dispatchQueue_has_Sec_Responded[0]);
                    */
                }
            }
            //await Task.Delay(_DoWork_MainTask_timeOut);
            Thread.Sleep(1);
            goto _taskLooper;

        }


        public Program._someObject _DoWork_MainTask(object _received_object) //async Task 
        {
            //System.Windows.MessageBox.Show("_t2", "Console");
            _data0 = new Program._someObject();
            if (_received_object is Program._someObject) // Cast 2.
            {
                _someReceivedObject4[0] = (Program._someObject)_received_object;
                int _received_switch_in = _someReceivedObject4[0]._received_switch_in;   //1
                int _received_switch_out = _someReceivedObject4[0]._received_switch_out; //0
                int _sending_switch_in = _someReceivedObject4[0]._sending_switch_in;     //0
                int _sending_switch_out = _someReceivedObject4[0]._sending_switch_out;   //0
                object[][] _chain_Of_Tasks = _someReceivedObject4[0]._chain_Of_Tasks0;
                int _timeOut = _someReceivedObject4[0]._timeOut0;
                int _ParentTaskThreadID = _someReceivedObject4[0]._ParentTaskThreadID0;
                int _main_cpu_count = _someReceivedObject4[0]._main_cpu_count;


                /*//System.Windows.MessageBox.Show("Thank you", "Console");
                object[] test = new object[3];
                test[0] = "Thank you! " + _thankYouCounter;
                test[1] = 0;
                test[2] = 1;
                _someQueue.Add(test);

                _data0._sending_switch_out = 1;
                _dispatchQueue.Add(_data0);
                _thankYouCounter++;
                _dispatchQueue_has_Sec_Responded.Add(_data0);*/

                if (_received_switch_in == 0 &&
                    _received_switch_out == 0 &&
                    _sending_switch_in == 0 &&
                    _sending_switch_out == 0)
                {
                    System.Windows.MessageBox.Show("Flow 0000a", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x0", 20, 10);
                    _someReceivedObject4[0]._received_switch_in = 0;
                    _someReceivedObject4[0]._received_switch_out = 0;
                    _someReceivedObject4[0]._sending_switch_in = 0;
                    _someReceivedObject4[0]._sending_switch_out = 0;

                    //return _someReceivedObject4[0];
                }
                else if (_received_switch_in == 1 &&
                         _received_switch_out == 0 &&
                         _sending_switch_in == 0 &&
                         _sending_switch_out == 0)
                {

                    _user_name = _someReceivedObject4[0]._passTest;
                    if (_user_name.ToLower() == "nine" || _user_name.ToLower() == "ninekorn")
                    {
                        System.Windows.MessageBox.Show("recognized", "Console"); // registering the command but not failing whatever the response is... i think. lets try
                        System.Windows.MessageBox.Show("Thank you" + _user_name + " 00", "Console");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("unrecognized", "Console");
                        System.Windows.MessageBox.Show("Fuck you" + _user_name + " 01", "Console");
                    }

                    //123               




                    /*consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("option 0", 0, 10);
                    consoleMessageQueue _consoleMessageQueue1 = new consoleMessageQueue("option 1", 0, 11);
                    consoleMessageQueue _consoleMessageQueue2 = new consoleMessageQueue("option 2", 0, 12);
                    consoleMessageQueue _consoleMessageQueue3 = new consoleMessageQueue("option 3", 0, 13);
                    consoleMessageQueue _consoleMessageQueue4 = new consoleMessageQueue("option 4", 0, 14);
                    consoleMessageQueue _consoleMessageQueue5 = new consoleMessageQueue("option 5", 0, 15);
                    */


                    /*_someReceivedObject4[0]._received_switch_in = 1;
                    _someReceivedObject4[0]._received_switch_out = 0;
                    _someReceivedObject4[0]._sending_switch_in = 0;
                    _someReceivedObject4[0]._sending_switch_out = 0;
                    */



                    //return _someReceivedObject4[0];
                    //_data0._received_switch_in = 1;
                    //_data0._received_switch_out = 1;
                    //_data0._sending_switch_in = 1;
                    //_data0._sending_switch_out = 1;

                }
                else if (_received_switch_in == 0 &&
                         _received_switch_out == 1 &&
                         _sending_switch_in == 0 &&
                         _sending_switch_out == 0)
                {
                    _someReceivedObject4[0]._received_switch_in = 0;
                    _someReceivedObject4[0]._received_switch_out = 0;
                    _someReceivedObject4[0]._sending_switch_in = 0;
                    _someReceivedObject4[0]._sending_switch_out = 0;
                    System.Windows.MessageBox.Show("Flow 02h", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x2", 20, 12);
                }
                else if (_received_switch_in == 0 &&
                         _received_switch_out == 0 &&
                         _sending_switch_in == 1 &&
                         _sending_switch_out == 0)
                {
                    _someReceivedObject4[0]._received_switch_in = 0;
                    _someReceivedObject4[0]._received_switch_out = 0;
                    _someReceivedObject4[0]._sending_switch_in = 0;
                    _someReceivedObject4[0]._sending_switch_out = 0;
                    //System.Windows.MessageBox.Show("Flow 3", "Console");
                    System.Windows.MessageBox.Show("_current_wtf_task", "Console");

                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x3", 20, 13);
                }
                else if (_received_switch_in == 0 &&
                       _received_switch_out == 0 &&
                       _sending_switch_in == 0 &&
                       _sending_switch_out == 1)
                {
                    _someReceivedObject4[0]._received_switch_in = 0;
                    _someReceivedObject4[0]._received_switch_out = 0;
                    _someReceivedObject4[0]._sending_switch_in = 0;
                    _someReceivedObject4[0]._sending_switch_out = 0;
                    System.Windows.MessageBox.Show("Flow 04f", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x4", 20, 14);
                }
                else if (_received_switch_in == 1 &&
                        _received_switch_out == 1 &&
                        _sending_switch_in == 0 &&
                        _sending_switch_out == 0)
                {
                    /*System.Windows.MessageBox.Show("What is your name 0000?", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("What is your name?", 20, 15);

                    string someName = Console.ReadLine();

                    if (someName != "")
                    {
              
                        if (someName.ToLower() == "nine" || someName.ToLower() == "ninekorn")
                        {
                            _someReceivedObject4[0]._received_switch_in = 1;
                            _someReceivedObject4[0]._received_switch_out = 1;
                            _someReceivedObject4[0]._sending_switch_in = 1;
                            _someReceivedObject4[0]._sending_switch_out = 0;
                            
                            //return _someReceivedObject4[0];
                           
                        }
                    }
                    else
                    {
                        _someReceivedObject4[0]._received_switch_in = 1;
                        _someReceivedObject4[0]._received_switch_out = 1;
                        _someReceivedObject4[0]._sending_switch_in = 1;
                        _someReceivedObject4[0]._sending_switch_out = 0;
                        System.Windows.MessageBox.Show("Hi " + "wtf", "Console");
                        //return _someReceivedObject4[0];
                    }*/




                    System.Windows.MessageBox.Show("FLOW TEST", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x5", 20, 15);
                }
                else if (_received_switch_in == 1 &&
                     _received_switch_out == 0 &&
                     _sending_switch_in == 1 &&
                     _sending_switch_out == 0)
                {
                    _someReceivedObject4[0]._received_switch_in = 0;
                    _someReceivedObject4[0]._received_switch_out = 0;
                    _someReceivedObject4[0]._sending_switch_in = 0;
                    _someReceivedObject4[0]._sending_switch_out = 0;
                    System.Windows.MessageBox.Show("Flow 6", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x6", 20, 16);
                }
                else if (_received_switch_in == 1 &&
                       _received_switch_out == 0 &&
                       _sending_switch_in == 0 &&
                       _sending_switch_out == 1)
                {

                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x7", 20, 17);
                    System.Windows.MessageBox.Show("Flow 07d ___ " + _someReceivedObject4[0]._passTest, "Console");

                    if (_someReceivedObject4[0]._passTest.ToLower() == "nine" || _someReceivedObject4[0]._passTest.ToLower() == "ninekorn")
                    {
                        System.Windows.MessageBox.Show(" password accepted ___ " + _user_name, "Console");
                        _someReceivedObject4[0]._received_switch_in = 0;
                        _someReceivedObject4[0]._received_switch_out = 1;
                        _someReceivedObject4[0]._sending_switch_in = 1;
                        _someReceivedObject4[0]._sending_switch_out = 0;
                    }
                    else
                    {
                        _someReceivedObject4[0]._received_switch_in = 0;
                        _someReceivedObject4[0]._received_switch_out = 0;
                        _someReceivedObject4[0]._sending_switch_in = 0;
                        _someReceivedObject4[0]._sending_switch_out = 0;
                    }








                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("ThreadStart Performance: " + _performanceWatch.Elapsed.Ticks, 0, 15);
                }
                else if (_received_switch_in == 0 &&
                    _received_switch_out == 1 &&
                    _sending_switch_in == 1 &&
                    _sending_switch_out == 0)
                {
                    _someReceivedObject4[0]._received_switch_in = 0;
                    _someReceivedObject4[0]._received_switch_out = 0;
                    _someReceivedObject4[0]._sending_switch_in = 0;
                    _someReceivedObject4[0]._sending_switch_out = 0;
                    System.Windows.MessageBox.Show("Flow 08a", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x8", 20, 18);
                }
                else if (_received_switch_in == 0 &&
                      _received_switch_out == 1 &&
                      _sending_switch_in == 0 &&
                      _sending_switch_out == 1)
                {
                    _someReceivedObject4[0]._received_switch_in = 0;
                    _someReceivedObject4[0]._received_switch_out = 0;
                    _someReceivedObject4[0]._sending_switch_in = 0;
                    _someReceivedObject4[0]._sending_switch_out = 0;
                    System.Windows.MessageBox.Show("Flow 09", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x9", 20, 19);
                }
                else if (_received_switch_in == 0 &&
                      _received_switch_out == 0 &&
                      _sending_switch_in == 1 &&
                      _sending_switch_out == 1)
                {
                    _someReceivedObject4[0]._received_switch_in = 0;
                    _someReceivedObject4[0]._received_switch_out = 0;
                    _someReceivedObject4[0]._sending_switch_in = 0;
                    _someReceivedObject4[0]._sending_switch_out = 0;
                    System.Windows.MessageBox.Show("Flow 10", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x10", 20, 20);
                }
                else if (_received_switch_in == 1 &&
                           _received_switch_out == 0 &&
                           _sending_switch_in == 1 &&
                           _sending_switch_out == 1)
                {
                    _someReceivedObject4[0]._received_switch_in = 0;
                    _someReceivedObject4[0]._received_switch_out = 0;
                    _someReceivedObject4[0]._sending_switch_in = 0;
                    _someReceivedObject4[0]._sending_switch_out = 0;
                    System.Windows.MessageBox.Show("Flow 011e", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x11", 20, 21);
                }
                else if (_received_switch_in == 0 &&
                        _received_switch_out == 1 &&
                        _sending_switch_in == 1 &&
                        _sending_switch_out == 1)
                {
                    _someReceivedObject4[0]._received_switch_in = 0;
                    _someReceivedObject4[0]._received_switch_out = 0;
                    _someReceivedObject4[0]._sending_switch_in = 0;
                    _someReceivedObject4[0]._sending_switch_out = 0;
                    System.Windows.MessageBox.Show("Flow 12", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x12", 20, 22);
                }
                else if (_received_switch_in == 1 &&
                         _received_switch_out == 1 &&
                         _sending_switch_in == 1 &&
                         _sending_switch_out == 0)
                {
                    _someReceivedObject4[0]._received_switch_in = 0;
                    _someReceivedObject4[0]._received_switch_out = 0;
                    _someReceivedObject4[0]._sending_switch_in = 0;
                    _someReceivedObject4[0]._sending_switch_out = 0;
                    System.Windows.MessageBox.Show("Flow 013a", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x13", 20, 23);
                }
                else if (_received_switch_in == 1 &&
                        _received_switch_out == 1 &&
                        _sending_switch_in == 0 &&
                        _sending_switch_out == 1)
                {
                    _someReceivedObject4[0]._received_switch_in = 0;
                    _someReceivedObject4[0]._received_switch_out = 0;
                    _someReceivedObject4[0]._sending_switch_in = 0;
                    _someReceivedObject4[0]._sending_switch_out = 0;
                    System.Windows.MessageBox.Show("Flow 014", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x14", 20, 24);
                }
                else if (_received_switch_in == 1 &&
                      _received_switch_out == 1 &&
                      _sending_switch_in == 1 &&
                      _sending_switch_out == 1)
                {
                    _someReceivedObject4[0]._received_switch_in = 0;
                    _someReceivedObject4[0]._received_switch_out = 0;
                    _someReceivedObject4[0]._sending_switch_in = 0;
                    _someReceivedObject4[0]._sending_switch_out = 0;
                    System.Windows.MessageBox.Show("Flow 015c", "Console");
                    //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("Flow x15", 20, 25);
                }
            }

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


            int _received_switch_in00 = _someReceivedObject4[0]._received_switch_in;
            int _received_switch_out00 = _someReceivedObject4[0]._received_switch_out;
            int _sending_switch_in00 = _someReceivedObject4[0]._sending_switch_in;
            int _sending_switch_out00 = _someReceivedObject4[0]._sending_switch_out;
            object[][] _chain_Of_Tasks0 = _someReceivedObject4[0]._chain_Of_Tasks0;
            int _timeOut00 = _someReceivedObject4[0]._timeOut0;
            int _ParentTaskThreadID00 = _someReceivedObject4[0]._ParentTaskThreadID0;
            int _main_cpu_count00 = _someReceivedObject4[0]._main_cpu_count;

            if (_received_switch_in00 == 0 &&
                _received_switch_out00 == 0 &&
                _sending_switch_in00 == 0 &&
                _sending_switch_out00 == 0)
            {
                System.Windows.MessageBox.Show("Flow 00a", "Console");
                ////consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx0", 30, 10);
                _someReceivedObject4[0]._received_switch_in = 0;
                _someReceivedObject4[0]._received_switch_out = 0;
                _someReceivedObject4[0]._sending_switch_in = 0;
                _someReceivedObject4[0]._sending_switch_out = 0;
                //return _someReceivedObject4[0];
            }
            else if (_received_switch_in00 == 1 && ///-10123
                     _received_switch_out00 == 0 &&
                     _sending_switch_in00 == 0 &&
                     _sending_switch_out00 == 0)
            {
                System.Windows.MessageBox.Show("Flow 01f ", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx1", 30, 11);


            }
            else if (_received_switch_in00 == 0 &&
                     _received_switch_out00 == 1 &&
                     _sending_switch_in00 == 0 &&
                     _sending_switch_out00 == 0)
            {
                System.Windows.MessageBox.Show("Flow 02i", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx2", 30, 12);
            }
            else if (_received_switch_in00 == 0 &&
                     _received_switch_out00 == 0 &&
                     _sending_switch_in00 == 1 &&
                     _sending_switch_out00 == 0)
            {
                System.Windows.MessageBox.Show("Flow 3", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx3", 30, 13);
            }
            else if (_received_switch_in00 == 0 &&
                   _received_switch_out00 == 0 &&
                   _sending_switch_in00 == 0 &&
                   _sending_switch_out00 == 1)
            {
                System.Windows.MessageBox.Show("Flow 04g", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx4", 30, 14);
            }
            else if (_received_switch_in00 == 1 &&
                    _received_switch_out00 == 1 &&
                    _sending_switch_in00 == 0 &&
                    _sending_switch_out00 == 0)
            {
                System.Windows.MessageBox.Show("_current_task_wtf_00 TAsk 2", "Console");

                _someReceivedObject4[0]._received_switch_in = 1;
                _someReceivedObject4[0]._received_switch_out = 0;
                _someReceivedObject4[0]._sending_switch_in = 0;
                _someReceivedObject4[0]._sending_switch_out = 1;

                //return _someReceivedObject4[0];
                ///HERE
            }
            else if (_received_switch_in00 == 1 &&
                 _received_switch_out00 == 0 &&
                 _sending_switch_in00 == 1 &&
                 _sending_switch_out00 == 0)
            {
                System.Windows.MessageBox.Show("Flow 6", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx6", 30, 16);
            }
            else if (_received_switch_in00 == 1 &&
                   _received_switch_out00 == 0 &&
                   _sending_switch_in00 == 0 &&
                   _sending_switch_out00 == 1)
            {
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx7", 30, 17);
                System.Windows.MessageBox.Show("Flow 07e", "Console");
                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("ThreadStart Performance: " + _performanceWatch.Elapsed.Ticks, 0, 15);


            }
            else if (_received_switch_in00 == 0 &&
                _received_switch_out00 == 1 &&
                _sending_switch_in00 == 1 &&
                _sending_switch_out00 == 0)
            {
                System.Windows.MessageBox.Show("Flow 08b", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx8", 30, 18);
            }
            else if (_received_switch_in00 == 0 &&
                  _received_switch_out00 == 1 &&
                  _sending_switch_in00 == 0 &&
                  _sending_switch_out00 == 1)
            {
                System.Windows.MessageBox.Show("Flow 09", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx9", 30, 19);
            }
            else if (_received_switch_in00 == 0 &&
                  _received_switch_out00 == 0 &&
                  _sending_switch_in00 == 1 &&
                  _sending_switch_out00 == 1)
            {
                System.Windows.MessageBox.Show("Flow 10", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx10", 30, 20);
            }
            else if (_received_switch_in00 == 1 &&
                       _received_switch_out00 == 0 &&
                       _sending_switch_in00 == 1 &&
                       _sending_switch_out00 == 1)
            {
                System.Windows.MessageBox.Show("Flow 011f", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx11", 30, 21);
            }
            else if (_received_switch_in00 == 0 &&
                    _received_switch_out00 == 1 &&
                    _sending_switch_in00 == 1 &&
                    _sending_switch_out00 == 1)
            {
                System.Windows.MessageBox.Show("Flow 12", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx12", 30, 22);
            }
            else if (_received_switch_in00 == 1 &&
                     _received_switch_out00 == 1 &&
                     _sending_switch_in00 == 1 &&
                     _sending_switch_out00 == 0)
            {
                System.Windows.MessageBox.Show("Flow 013b", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx13", 30, 23);
            }
            else if (_received_switch_in00 == 1 &&
                    _received_switch_out00 == 1 &&
                    _sending_switch_in00 == 0 &&
                    _sending_switch_out00 == 1)
            {
                System.Windows.MessageBox.Show("Flow 014", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx14", 30, 24);
            }
            else if (_received_switch_in00 == 1 &&
                  _received_switch_out00 == 1 &&
                  _sending_switch_in00 == 1 &&
                  _sending_switch_out00 == 1)
            {
                System.Windows.MessageBox.Show("Flow 015d", "Console");
                //consoleMessageQueue _consoleMessageQueue0 = new consoleMessageQueue("Flow xx15", 30, 25);
            }




            if (_counterTaskIsAlive > 100)
            {
                _counterTaskIsAlive = 0;
            }
            //System.Windows.MessageBox.Show("Thread is Alive", "Console");
            _counterTaskIsAlive++;
            _anotherCounter++;

            return _someReceivedObject4[0];
        }


        public class consoleMessageQueue
        {
            public object[] _someObject;
            public string _message = "";
            public int _lineX = 0;
            public int _lineY = 0;

            public consoleMessageQueue(string message, int lineX, int lineY)
            {
                object[] test = new object[3];
                test[0] = message;
                test[1] = lineX;
                test[2] = lineY;
                _someQueue.Add(test);
            }
        }

        public void dispatchConsoleCommands(object[] someConsoleData)
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



        }





        public void HideConsoleWindow()
        {
            //var handle = GetConsoleWindow();
            //ShowWindow(handle, SwHide);
        }


        public void starterItems(string main, string secondary)
        {
            string currentTime = DateTime.Now.ToString("h:mm:ss tt");
            string currentDay = DateTime.Today.ToString();
        }


        public void clearConsole()
        {
            Console.Clear();
        }

        public void writeToConsole(string test)
        {
            Console.Write(test);
        }

        public void writeLineToConsole(string test)
        {
            Console.WriteLine(test);
        }


        public int cursorLeft()
        {
            int left = Console.CursorLeft;
            return left;
        }

        public int cursorTop()
        {
            int top = Console.CursorTop;
            return top;
        }

        public int consoleWidth()
        {
            int width = Console.WindowWidth;
            return width;
        }
        public int consoleHeight()
        {
            int height = Console.WindowHeight;
            return height;
        }






        public void consoleSwitchLine()
        {
            Console.Write("\n");
        }



        public void consoleResetCursor()
        {
            int top = Console.CursorTop;
            int left = Console.CursorLeft;
            Console.SetCursorPosition(left, top);
        }

        public void ClearCurrentConsoleLine(int x, int y)
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

        public void setCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }


        public void WriteAt(string s, int x, int y, bool canPassLine, int linePassNumber)
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

/*task0 = Task<object>.Factory.StartNew(() =>
{
    _mainObjectToReturn = _DoWork_hasTaskWorked(_main_object);

    return _mainObjectToReturn;
});*/



//"▬" gives out a Question Mark
//~←∟↓→§▬↨↓→╕╕╣╖╢╡┤│▓░○▒»«¡¼½
//"ALT1020++"g☺☺}☺☻♥♦♣♠≈•◘○◙♂♀♪
//ⁿ²■☺☻☺
