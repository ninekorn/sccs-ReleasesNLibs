using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;



namespace SC_skYaRk_VR_V007
{
    public class SC_Systems : SC_console_component, SC_Globals
    {
        public static SC_console_component _SC_ICOMPONENT;
        SC_Globals SC_console_component._SC_Globals
        {
            get => _SC_GLOB;
        }
        public static SC_Globals _SC_GLOB;
        public virtual SC_skYaRk_VR_V007.SC_Console_CORE _SC_CONSOLE_CORE { get; set; }

        public virtual SC_skYaRk_VR_V007.SC_Console_WRITER _SC_CONSOLE_WRITER { get; set; }

        public virtual SC_skYaRk_VR_V007.SC_Console_READER _SC_CONSOLE_READER { get; set; }
        public virtual SC_skYaRk_VR_V007.SC_Systems _SC_SYSTEMS { get; set; }
        //public virtual SC_skYaRk_VR_V007.Program _PROGRAM { get; set; }

        public virtual int _Activate_Desktop_Image { get; set; }

        /*public SC_Globals_chains _SC_GLOBAL_CHAINS { get; private set; }

        SC_IComponent SC_Globals_chains._SC_IComponent
        {
            get => _SC_ICOMPONENT;
        }*/


        //IRigidbody IComponent.rigidbody { get; set; }


        private SC_object_messenger_dispatcher.SC_message_object testingInit(SC_object_messenger_dispatcher.SC_message_object _main_object)
        {
            /*System.Windows.MessageBox.Show("_SC_GLOB NULL", "Console");

            _SC_GLOB = this._SC_Globals_chains._SC_IComponent._SC_Globals;
            */

          
            /*_SC_GLOB = this._SC_Globals_chains._SC_IComponent._SC_Globals;


            if (_SC_GLOB == null)
            {
                System.Windows.MessageBox.Show("_SC_GLOB NULL", "Console");
            }
            else
            {
                System.Windows.MessageBox.Show("_SC_GLOB !NULL", "Console");
            }*/

            /*if (_SC_GLOB._SC_CONSOLE_CORE == null)
            {
                _SC_GLOB._SC_CONSOLE_CORE = new SC_Console_CORE(_main_object);
            }*/

            return _main_object;
        }

        public static int _init_main_Task = 1;
        static object _OBJECT_MAIN = new object();
        static Task<object> task3;
        static SC_object_messenger_dispatcher.SC_message_object _main_someObject;

        int _totalThreads = -1;


        //_SC ._SC_Globals SC_IComponent._SC_Globals.sc { get; set; }
        public SC_Systems(SC_object_messenger_dispatcher.SC_message_object[] _main_object)
        {
            _SC_GLOB = this;
            _SC_ICOMPONENT = this;

            //FOR DEBUG
            /*if (_SC_ICOMPONENT == null)
            {
                System.Windows.MessageBox.Show("_SC_ICOMPONENT NULL", "Console");
            }
            else
            {
                System.Windows.MessageBox.Show("_SC_ICOMPONENT !NULL", "Console");
            }
            if (_SC_GLOB == null)
            {
                System.Windows.MessageBox.Show("_SC_Globals NULL", "Console");
            }
            else
            {
                System.Windows.MessageBox.Show("_SC_Globals !NULL", "Console");
            }*/
            //System.Windows.MessageBox.Show("!null SYSTEMS", "Console");
            _SC_CONSOLE_CORE = new SC_Console_CORE(_main_object);
            _SC_CONSOLE_WRITER = new SC_Console_WRITER(_main_object);
            _SC_CONSOLE_READER = new SC_Console_READER(_main_object);

            _SC_GLOB._SC_CONSOLE_CORE = _SC_CONSOLE_CORE;
            _SC_GLOB._SC_CONSOLE_WRITER = _SC_CONSOLE_WRITER;
            _SC_GLOB._SC_CONSOLE_READER = _SC_CONSOLE_READER;       




            //_main_someObject = (Program._someObject)_main_object;
            //_totalThreads = _main_someObject._main_cpu_count;

            /*_main_someObject._received_switch_in = 0;
            _main_someObject._received_switch_out = 0;
            _main_someObject._sending_switch_in = 0;
            _main_someObject._sending_switch_out = 0;
            _main_someObject._chain_Of_Tasks0 = null;
            _main_someObject._timeOut0 = 1;
            _main_someObject._ParentTaskThreadID0 = -1;
            _main_someObject._main_cpu_count = _totalThreads;
            _main_someObject._passTest = "";*/

            //_main_Task_static_free(_main_object);
        }

        public object _main_Task_static_free(object _main_object)
        {
            _main_object = null;
            task3 = Task<object>.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (_init_main_Task == 1)
                    {
                        //System.Windows.MessageBox.Show("_main_Task_static_free !NULL", "Console");
                        /*_main_someObject = new Program._someObject();
                        _main_someObject._received_switch_in = 0;
                        _main_someObject._received_switch_out = 0;
                        _main_someObject._sending_switch_in = 0;
                        _main_someObject._sending_switch_out = 0;
                        _main_someObject._chain_Of_Tasks0 = null;
                        _main_someObject._timeOut0 = 1;
                        _main_someObject._ParentTaskThreadID0 = -1;
                        _main_someObject._main_cpu_count = _totalThreads;
                        _main_someObject._passTest = "";

                        _main_object = _main_someObject;
                        _main_object = testingInit(_main_object);
                        _main_object = _main_someObject;*/

                        _init_main_Task = 0;
                    }
                    Thread.Sleep(1);
                    //Thread.Sleep(1);
                    //await Task.Yield();
                    //_tme.WaitOne();
                }
            });
            return _main_object;
        }
    }
}
