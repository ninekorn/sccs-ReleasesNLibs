using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

//using _console_writer_message_queue = SC_SkYaRk_VR_Editionv002.SC_Console_WRITER._console_writer_message_queue;

namespace SC_skYaRk_VR_V007
{
    public class SC_Console_READER
    {
        public SC_Console_WRITER _SC_CONSOLE_WRITER;
        public SC_Console_CORE _SC_CONSOLE_CORE;

        _console_reader_data _current_console_reader_data;


        public SC_Console_READER(object tester)//; : base(tester) //SC_object_messenger_dispatcher.SC_message_object[]
        {
            _SC_CONSOLE_WRITER = SC_Systems._SC_GLOB._SC_CONSOLE_WRITER;

            ////System.Windows.MessageBox.Show("!null READER", "Console");
            //_console_reader(tester);
        }

        public List<object[]> _TASK_00_RE_QUEUE = new List<object[]>();
        public object _ResultsOfTasks0;
        public int _Task00_init_console = 1;
        public Task _TASK_00_RE;
        public int _console_is_alive_00_RE = 0;
        public int _console_ERROR = -1;
        public int _console_hasINIT = 0;
        public int _xCurrentCursorPos;
        public int _yCurrentCursorPos;

        //public _console_writer_message_queue _MSG_CONSOLE;
        //public List<_console_writer_message_queue> _Console_writer_message_queue = new List<_console_writer_message_queue>();

        public int _main_has_init = 0;


        string _lastConsoleMessage = "";
        public _console_reader_data _console_reader(object _main_object)// SC_object_messenger_dispatcher.SC_message_object _main_object
        {

            if (_main_has_init == 0)
            {

                string tester = Console.ReadLine();
                _current_console_reader_data._console_reader_message = "nothing ";
                _current_console_reader_data._has_message_to_display = 0;
                _main_has_init = 1;
            }
            else if(_main_has_init == 1 || _main_has_init == 2)
            {
                string tester = Console.ReadLine();
                _current_console_reader_data._console_reader_message = tester;
                _current_console_reader_data._has_message_to_display = 1;
            }

            return _current_console_reader_data;
        }

        public struct _console_reader_data
        {
            public int _has_init;
            public int _has_message_to_display;
            public string _console_reader_message;

        }
    }
}
