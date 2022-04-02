namespace sccs
{
    public interface sc_globals
    {
        sccs.sc.sc_console_core SC_CONSOLE_CORE { get; set; }
        sccs.sc_console.sc_console_writer SC_CONSOLE_WRITER { get; set; }
        sccs.sc_console.sc_console_reader SC_CONSOLE_READER { get; set; }
        sccs.sc_core.sc_globals_accessor SC_GLOBALS_ACCESSORS { get; set; }
        int _Activate_Desktop_Image { get; set; }

    }
}
