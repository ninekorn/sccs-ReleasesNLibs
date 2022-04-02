using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _sc_core_systems._sc_core
{
    public static class _sc_system_info
    {
        //public static int processorCount = -1;

        public static int getSystemProcessorCount()
        {
            int processorCount = Environment.ProcessorCount;
            return processorCount;
        }
    }
}
