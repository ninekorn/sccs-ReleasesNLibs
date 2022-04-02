using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SC_skYaRk_VR_V007.SC_Console_Menu
{
    public static class SC_Console_Menu
    {
        static SC_object_messenger_dispatcher.SC_message_object _someReceivedObject0 = new SC_object_messenger_dispatcher.SC_message_object();
        static SC_object_messenger_dispatcher.SC_message_object _data00_IN;
        static SC_object_messenger_dispatcher.SC_message_object _toReturnObject;

        static int _work_doner = 0;
        public static SC_object_messenger_dispatcher.SC_message_object _console_worker_menu(object _main_object) //CHAIN 1
        {
            try
            {
                _data00_IN = (SC_object_messenger_dispatcher.SC_message_object)_main_object;

                //_work_doner = _data00_IN._work_done;

                //if (_work_doner == 0)
                {
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

                    /*if (_passTest00.ToLower() == "go fuck yourself")
                    {
                        //System.Windows.MessageBox.Show("go fuck yourself " , "Console");
                    }*/
                    _someReceivedObject0 = _data00_IN;
                    //_someReceivedObject0._work_done = 2;
                    /*_some_received_messages[3]._message = "[";
                    _some_received_messages[3]._originalMsg = "]";
                    _some_received_messages[3]._messageCut = "";
                    _some_received_messages[3]._specialMessage = 2;
                    _some_received_messages[3]._specialMessageLineX = 0;
                    _some_received_messages[3]._specialMessageLineY = 0;
                    _some_received_messages[3]._lineX = 0;
                    _some_received_messages[3]._lineY = 0;
                    _some_received_messages[3]._count = 0;
                    _some_received_messages[3]._swtch = 1;
                    _some_received_messages[3]._delay = 100;*/

                    if (_received_switch_in00 == 0 &&
                        _received_switch_out00 == 0 &&
                        _sending_switch_in00 == 0 &&
                        _sending_switch_out00 == 0)
                    {
                        ////System.Windows.MessageBox.Show("CHAIN01_IN_0000 =>CHAIN01_OUT_0000", "Console");
                        //System.Windows.MessageBox.Show("Choose option", "Console");
                        //_optionCommand = Console.ReadLine();

                        if (_menuOption.ToLower() == "vr" ||
                            _menuOption.ToLower() == "std" ||
                            _menuOption.ToLower() == "standard" ||
                            _menuOption.ToLower() == "command" ||
                            _menuOption.ToLower() == "commands" ||
                            _menuOption.ToLower() == "credit" ||
                            _menuOption.ToLower() == "credits" ||
                            _menuOption.ToLower() == "singleplayer" ||
                            _menuOption.ToLower() == "multiplayer" ||
                            _menuOption.ToLower() == "single" ||
                            _menuOption.ToLower() == "multi")
                        {
                            if (_menuOption.ToLower() == "vr")
                            {
                                _someReceivedObject0._current_menu = 0;

                                _toReturnObject = _someReceivedObject0;
                                return _toReturnObject;
                            }
                            else if (_menuOption.ToLower() == "standard" || _menuOption.ToLower() == "std")
                            {
                                _someReceivedObject0._current_menu = 1;
                                _toReturnObject = _someReceivedObject0;
                                return _toReturnObject;
                            }
                           
                            /*//System.Windows.MessageBox.Show("this option is implemented", "Console");

                            _someReceivedObject0._received_switch_in = 0;
                            _someReceivedObject0._received_switch_out = 0;
                            _someReceivedObject0._sending_switch_in = 0;
                            _someReceivedObject0._sending_switch_out = 0;
               |             _someReceivedObject0._welcomePackage = _welcomePackage00;
                            _someReceivedObject0._work_done = 1;
                            _someReceivedObject0._current_menu = 0;*/
                        }
                        else
                        {
                            //System.Windows.MessageBox.Show("this option is not implemented yet", "Console");
                            _someReceivedObject0._received_switch_in = 0;
                            _someReceivedObject0._received_switch_out = 0;
                            _someReceivedObject0._sending_switch_in = 0;
                            _someReceivedObject0._sending_switch_out = 0;
                            _someReceivedObject0._welcomePackage = 1;
                            _someReceivedObject0._work_done = 0;
                            _someReceivedObject0._current_menu = -1;
                        }
                        _toReturnObject = _someReceivedObject0;
                        return _toReturnObject;

                        /*else if (_current_menu00 == 0)
                        {
                            _someReceivedObject0._received_switch_in = 0;
                            _someReceivedObject0._received_switch_out = 0;
                            _someReceivedObject0._sending_switch_in = 0;
                            _someReceivedObject0._sending_switch_out = 0;

                            _toReturnObject = _someReceivedObject0;
                            return _toReturnObject;
                        }*/
                    }
                    else if (_received_switch_in00 == 1 &&
                             _received_switch_out00 == 0 &&
                             _sending_switch_in00 == 0 &&
                             _sending_switch_out00 == 0)
                    {


                        _someReceivedObject0._received_switch_in = 0;
                        _someReceivedObject0._received_switch_out = 0;
                        _someReceivedObject0._sending_switch_in = 0;
                        _someReceivedObject0._sending_switch_out = 0;
                        //System.Windows.MessageBox.Show("CHAIN01_IN_1000 =>CHAIN01_OUT_1100", "Console");
                        _toReturnObject = _someReceivedObject0;
                        return _toReturnObject;
                    }
                    else if (_received_switch_in00 == 0 &&
                             _received_switch_out00 == 1 &&
                             _sending_switch_in00 == 0 &&
                             _sending_switch_out00 == 0)
                    {
                        _someReceivedObject0._received_switch_in = 0;
                        _someReceivedObject0._received_switch_out = 0;
                        _someReceivedObject0._sending_switch_in = 0;
                        _someReceivedObject0._sending_switch_out = 0;

                        //System.Windows.MessageBox.Show("CHAIN01_IN_0100 =>CHAIN01_OUT_0000", "Console");
                        _toReturnObject = _someReceivedObject0;
                        return _toReturnObject;
                    }
                    else if (_received_switch_in00 == 0 &&
                             _received_switch_out00 == 0 &&
                             _sending_switch_in00 == 1 &&
                             _sending_switch_out00 == 0)
                    {
                        _someReceivedObject0._received_switch_in = 0;
                        _someReceivedObject0._received_switch_out = 0;
                        _someReceivedObject0._sending_switch_in = 0;
                        _someReceivedObject0._sending_switch_out = 0;
                        _toReturnObject = _someReceivedObject0;
                        //System.Windows.MessageBox.Show("CHAIN01_IN_0010 =>CHAIN01_OUT_0000", "Console");
                        _toReturnObject = _someReceivedObject0;
                        return _toReturnObject;
                    }
                    else if (_received_switch_in00 == 0 &&
                           _received_switch_out00 == 0 &&
                           _sending_switch_in00 == 0 &&
                           _sending_switch_out00 == 1)
                    {
                        //System.Windows.MessageBox.Show("CHAIN01_IN_0001 =>CHAIN01_OUT_0000", "Console");
                        _someReceivedObject0._received_switch_in = 0;
                        _someReceivedObject0._received_switch_out = 0;
                        _someReceivedObject0._sending_switch_in = 0;
                        _someReceivedObject0._sending_switch_out = 0;
                        _toReturnObject = _someReceivedObject0;
                        return _toReturnObject;

                    }
                    else if (_received_switch_in00 == 1 &&
                            _received_switch_out00 == 1 &&
                            _sending_switch_in00 == 0 &&
                            _sending_switch_out00 == 0)
                    {

                        _someReceivedObject0._received_switch_in = 0;
                        _someReceivedObject0._received_switch_out = 0;
                        _someReceivedObject0._sending_switch_in = 0;
                        _someReceivedObject0._sending_switch_out = 0;
                        //System.Windows.MessageBox.Show("CHAIN01_IN_1100 =>CHAIN01_OUT_0000", "Console");
                        _toReturnObject = _someReceivedObject0;
                        return _toReturnObject;
                    }
                    else if (_received_switch_in00 == 1 &&
                         _received_switch_out00 == 0 &&
                         _sending_switch_in00 == 1 &&
                         _sending_switch_out00 == 0)
                    {
                        _someReceivedObject0._received_switch_in = 0;
                        _someReceivedObject0._received_switch_out = 0;
                        _someReceivedObject0._sending_switch_in = 0;
                        _someReceivedObject0._sending_switch_out = 0;
                        //System.Windows.MessageBox.Show("CHAIN01_IN_1010 =>CHAIN01_OUT_0000", "Console");
                        _toReturnObject = _someReceivedObject0;
                        return _toReturnObject;
                    }
                    else if (_received_switch_in00 == 1 &&
                           _received_switch_out00 == 0 &&
                           _sending_switch_in00 == 0 &&
                           _sending_switch_out00 == 1)
                    {
                        _someReceivedObject0._received_switch_in = 0;
                        _someReceivedObject0._received_switch_out = 0;
                        _someReceivedObject0._sending_switch_in = 0;
                        _someReceivedObject0._sending_switch_out = 0;
                        //System.Windows.MessageBox.Show("CHAIN01_IN_1001 =>CHAIN01_OUT_0000", "Console");
                        _toReturnObject = _someReceivedObject0;
                        return _toReturnObject;
                    }
                    else if (_received_switch_in00 == 0 &&
                        _received_switch_out00 == 1 &&
                        _sending_switch_in00 == 1 &&
                        _sending_switch_out00 == 0)
                    {
                        _someReceivedObject0._received_switch_in = 0;
                        _someReceivedObject0._received_switch_out = 0;
                        _someReceivedObject0._sending_switch_in = 0;
                        _someReceivedObject0._sending_switch_out = 0;
                        //System.Windows.MessageBox.Show("CHAIN01_IN_0110 =>CHAIN01_OUT_0000", "Console");
                        _toReturnObject = _someReceivedObject0;
                        return _toReturnObject;
                    }
                    else if (_received_switch_in00 == 0 &&
                          _received_switch_out00 == 1 &&
                          _sending_switch_in00 == 0 &&
                          _sending_switch_out00 == 1)
                    {
                        _someReceivedObject0._received_switch_in = 0;
                        _someReceivedObject0._received_switch_out = 0;
                        _someReceivedObject0._sending_switch_in = 0;
                        _someReceivedObject0._sending_switch_out = 0;
                        //System.Windows.MessageBox.Show("CHAIN01_IN_0101 =>CHAIN01_OUT_0000", "Console");
                        _toReturnObject = _someReceivedObject0;
                        return _toReturnObject;
                    }
                    else if (_received_switch_in00 == 0 &&
                          _received_switch_out00 == 0 &&
                          _sending_switch_in00 == 1 &&
                          _sending_switch_out00 == 1)
                    {
                        _someReceivedObject0._received_switch_in = 0;
                        _someReceivedObject0._received_switch_out = 0;
                        _someReceivedObject0._sending_switch_in = 0;
                        _someReceivedObject0._sending_switch_out = 0;
                        //System.Windows.MessageBox.Show("CHAIN01_IN_0011 =>CHAIN01_OUT_0000", "Console");
                        _toReturnObject = _someReceivedObject0;
                        return _toReturnObject;
                    }
                    else if (_received_switch_in00 == 1 &&
                               _received_switch_out00 == 0 &&
                               _sending_switch_in00 == 1 &&
                               _sending_switch_out00 == 1)
                    {
                        _someReceivedObject0._received_switch_in = 0;
                        _someReceivedObject0._received_switch_out = 0;
                        _someReceivedObject0._sending_switch_in = 0;
                        _someReceivedObject0._sending_switch_out = 0;
                        //System.Windows.MessageBox.Show("CHAIN01_IN_1011 =>CHAIN01_OUT_0000", "Console");
                        _toReturnObject = _someReceivedObject0;
                        return _toReturnObject;
                    }
                    else if (_received_switch_in00 == 0 &&
                            _received_switch_out00 == 1 &&
                            _sending_switch_in00 == 1 &&
                            _sending_switch_out00 == 1)
                    {
                        _someReceivedObject0._received_switch_in = 0;
                        _someReceivedObject0._received_switch_out = 0;
                        _someReceivedObject0._sending_switch_in = 0;
                        _someReceivedObject0._sending_switch_out = 0;
                        //System.Windows.MessageBox.Show("CHAIN01_IN_0111 =>CHAIN01_OUT_0000", "Console");
                        _toReturnObject = _someReceivedObject0;
                        return _toReturnObject;
                    }
                    
                    else if (_received_switch_in00 == 1 &&
                            _received_switch_out00 == 1 &&
                            _sending_switch_in00 == 0 &&
                            _sending_switch_out00 == 1)
                    {
                        _someReceivedObject0._received_switch_in = 0;
                        _someReceivedObject0._received_switch_out = 0;
                        _someReceivedObject0._sending_switch_in = 0;
                        _someReceivedObject0._sending_switch_out = 0;
                        //System.Windows.MessageBox.Show("CHAIN01_IN_1101 =>CHAIN01_OUT_0000", "Console");
                        _toReturnObject = _someReceivedObject0;
                        return _toReturnObject;
                    }
                    else if (_received_switch_in00 == 1 &&
                             _received_switch_out00 == 1 &&
                             _sending_switch_in00 == 1 &&
                             _sending_switch_out00 == 0)
                    {
                        _someReceivedObject0._received_switch_in = 0;
                        _someReceivedObject0._received_switch_out = 0;
                        _someReceivedObject0._sending_switch_in = 0;
                        _someReceivedObject0._sending_switch_out = 0;
                        //System.Windows.MessageBox.Show("CHAIN01_IN_1110 =>CHAIN01_OUT_0000", "Console");
                        _toReturnObject = _someReceivedObject0;
                        return _toReturnObject;
                    }
                    else if (_received_switch_in00 == 1 &&
                          _received_switch_out00 == 1 &&
                          _sending_switch_in00 == 1 &&
                          _sending_switch_out00 == 1)
                    {
                        //System.Windows.MessageBox.Show("CHAIN01_IN_1111 =>CHAIN01_OUT_0000", "Console");
                        _someReceivedObject0._received_switch_in = 0;
                        _someReceivedObject0._received_switch_out = 0;
                        _someReceivedObject0._sending_switch_in = 0;
                        _someReceivedObject0._sending_switch_out = 0;
                        _toReturnObject = _someReceivedObject0;
                        return _toReturnObject;
                    }
                }
            }
            catch (Exception ex)
            {
                //_msg_prog._message = ex.ToString();
                //_msg_prog._lineX = 0;
                //_msg_prog._lineY = 20;
                //_Console_writer_message_queue.Add(_msg_prog);
                //////System.Windows.MessageBox.Show(ex.ToString(), "Console");
            }
            return _toReturnObject;
        }
    }
}
