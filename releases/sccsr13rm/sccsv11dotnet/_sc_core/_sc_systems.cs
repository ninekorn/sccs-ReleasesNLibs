using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;



namespace _sc_core_systems._sc_core
{
    public class _sc_systems : _sc_icomponent, _sc_globals
    {
        public static _sc_icomponent _SC_ICOMPONENT;
        _sc_globals _sc_icomponent._SC_Globals
        {
            get => _SC_GLOB;
        }
        public static _sc_globals _SC_GLOB;


        public virtual _sc_core_systems._sc_console._sc_console_core _SC_CONSOLE_CORE { get; set; }
        public virtual _sc_core_systems._sc_console._sc_console_writer _SC_CONSOLE_WRITER { get; set; }
        public virtual _sc_core_systems._sc_console._sc_console_reader _SC_CONSOLE_READER { get; set; }
        public virtual _sc_core_systems._sc_core._sc_systems _SC_SYSTEMS { get; set; }
        public virtual int _Activate_Desktop_Image { get; set; }

        private _sc_core_systems._sc_message_object._sc_message_object testingInit(_sc_core_systems._sc_message_object._sc_message_object _main_object)
        {   
            return _main_object;
        }

        public static int _init_main_Task = 1;

        public _sc_systems(_sc_core_systems._sc_message_object._sc_message_object[] _main_object)
        {
            _SC_GLOB = this;
            _SC_ICOMPONENT = this;

            _SC_CONSOLE_CORE = new _sc_console._sc_console_core(_main_object);
            _SC_CONSOLE_WRITER = new _sc_console._sc_console_writer(_main_object);
            _SC_CONSOLE_READER = new _sc_console._sc_console_reader(_main_object);

            _SC_GLOB._SC_CONSOLE_CORE = _SC_CONSOLE_CORE;
            _SC_GLOB._SC_CONSOLE_WRITER = _SC_CONSOLE_WRITER;
            _SC_GLOB._SC_CONSOLE_READER = _SC_CONSOLE_READER;       
        }
    }
}
