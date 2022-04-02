using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace SC_skYaRk_VR_V007
{
    public interface SC_Globals
    {
        SC_skYaRk_VR_V007.SC_Console_CORE _SC_CONSOLE_CORE { get; set; }

        SC_skYaRk_VR_V007.SC_Console_WRITER _SC_CONSOLE_WRITER { get; set; }

        SC_skYaRk_VR_V007.SC_Console_READER _SC_CONSOLE_READER { get; set; }
        SC_skYaRk_VR_V007.SC_Systems _SC_SYSTEMS { get; set; }
        //SC_skYaRk_VR_V007.Program _PROGRAM { get; set; }


        int _Activate_Desktop_Image { get; set; }

    }
}
