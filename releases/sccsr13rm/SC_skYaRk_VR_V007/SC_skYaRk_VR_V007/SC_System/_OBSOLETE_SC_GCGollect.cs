using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SC_skYaRk_VR_Edition_v003
{
    public static class SC_GCGollect
    {
        #region GCCollect

        static int _tickForGcCollect = 0;
        public static void GCCollectUtility(int tickForGcCollect)
        {
            //_tickForGcCollect = tickForGcCollect;
            if (_tickForGcCollect > 100)
            {
                //SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue("#" + _tickForGcCollect + "", 5, 1 + _tickForGcCollect);
                GC.Collect();
                _tickForGcCollect = 0;
            }
            _tickForGcCollect++;
        }

        #endregion
    }
}
