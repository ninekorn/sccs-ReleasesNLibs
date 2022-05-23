//slightly modified from the rastertek tutorial. steve chassé aka ninekorn

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sccs.sccore
{
    public static class scsysteminfo
    {
        //public static int processorCount = -1;

        public static int getSystemProcessorCount()
        {
            int processorCount = Environment.ProcessorCount;
            return processorCount;
        }
    }
}
