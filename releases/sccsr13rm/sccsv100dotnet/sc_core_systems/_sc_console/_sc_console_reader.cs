using System;

namespace _sc_core_systems._sc_console
{
    public class _sc_console_reader
    {
        public _sc_console_writer _SC_CONSOLE_WRITER;
        _console_reader_data _current_console_reader_data;
        public int _main_has_init = 0;

        public _sc_console_reader(object tester)
        {
            _SC_CONSOLE_WRITER = _sc_core_systems._sc_core._sc_systems._SC_GLOB._SC_CONSOLE_WRITER;
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
