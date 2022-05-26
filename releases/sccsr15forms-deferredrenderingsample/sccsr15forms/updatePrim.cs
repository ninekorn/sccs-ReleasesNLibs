//DEVELOPED BY STEVE CHASSÉ

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using System.Diagnostics;


namespace sccsr15forms
{
    internal class updatePrim : IDisposable
    {
        public static updatePrim currentupdatePrim;
        public directx D3D;
        public Camera camera;

 


        public updatePrim(directx D3D_)
        {
            currentupdatePrim = this;
            D3D = D3D_;

            camera = new Camera();

            camera.SetPosition(0,0,-1);
            camera.SetRotation(0, 180, 0);

            Console.WriteLine("created updatePrim");
        }

        //TEST FOR UI THREAD VS SYSTEM.THREAD PERFORMANCE. SYSTEM.THREAD IS FASTER.
        /*public int counteruithread;
        public int countersystemthread;

        public void updatescriptsprimUIThread()
        {
            counteruithread++;
        }
        public void updatescriptsprimSystemThread()
        {
            countersystemthread++;
        }*/

        public void updatescriptsprimstartrender()
        {
            //startrender();
            //camera.Render();

        }
        public void updatescriptsprimstoprender()
        {
          
            stoprender();
        }




        public void startrender()
        {
            if (D3D != null)
            {
                if (D3D.DepthStencilView != null)
                {
                    D3D.DeviceContext.ClearDepthStencilView(D3D.DepthStencilView, SharpDX.Direct3D11.DepthStencilClearFlags.Depth, 1.0f, 0); //new SharpDX.Color(255, 15, 15, 255)

                }
                if (D3D.RenderTargetView != null)
                {
                    D3D.DeviceContext.ClearRenderTargetView(D3D.RenderTargetView, SharpDX.Color.CornflowerBlue); //SharpDX.Color.LightGray //Black //new SharpDX.Color(255, 15,

                }
            }
        }

        public void stoprender()
        {
            if (D3D != null)
            {
                if (D3D.SwapChain != null)
                {
                    D3D.SwapChain.Present(0, SharpDX.DXGI.PresentFlags.None);
                }
            }
        }


        ~updatePrim()
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
