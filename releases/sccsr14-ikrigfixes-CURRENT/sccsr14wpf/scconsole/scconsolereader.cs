using System;

namespace sccs.scconsole
{
    public class scconsolereader
    {
        public scconsolewriter _SC_CONSOLE_WRITER;
        _console_reader_data _current_console_reader_data;
        public int _main_has_init = 0;

        public scconsolereader(object tester)
        {
            _SC_CONSOLE_WRITER = sccs.sccore.scglobalsaccessor.SCGLOB.SCCONSOLEWRITER;
        }

        public _console_reader_data _console_reader(object _main_object)
        {
            if (_main_has_init == 0)
            {

                string tester = Console.ReadLine();
                _current_console_reader_data._console_reader_message = "nothing ";
                _current_console_reader_data._has_message_to_display = 0;
                _main_has_init = 1;
            }
            else if (_main_has_init == 1 || _main_has_init == 2)
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
