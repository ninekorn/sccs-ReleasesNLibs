//DEVELOPED BY STEVE CHASSÉ

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccsr15forms
{
    internal class appconfiguration: IDisposable
    {
        public bool verticalsync;

        public appconfiguration(bool verticalsync_)
        {
            verticalsync = verticalsync_;
            Console.WriteLine("created appconfiguration");
        }

        ~appconfiguration()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // so that Dispose(false) isn't called later
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose all owned managed objects
            }

            // Release unmanaged resources
        }
    }
}
