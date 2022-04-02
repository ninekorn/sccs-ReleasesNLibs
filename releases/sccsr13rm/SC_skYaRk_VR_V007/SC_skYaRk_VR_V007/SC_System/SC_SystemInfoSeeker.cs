using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SC_skYaRk_VR_V007
{
    public static class SC_SystemInfoSeeker
    {
        //public static int processorCount = -1;

        public static int getSystemProcessorCount()
        {
            int processorCount = Environment.ProcessorCount;
            return processorCount;
        }
    }
}
