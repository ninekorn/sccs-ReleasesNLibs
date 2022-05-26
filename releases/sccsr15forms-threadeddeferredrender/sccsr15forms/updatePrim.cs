///////////////////////////////////////////////////////////////////////////////////////////////////
//DEVELOPED BY STEVE CHASSÉ using xoofx's sharpdx original deferred rendering sample.            //
//mix of rastertek c# github user dan6040's sample and xoofx samples and smartrak's architecture.//
///////////////////////////////////////////////////////////////////////////////////////////////////

// Copyright (c) 2010-2013 SharpDX - Alexandre Mutel 
//  
// Permission is hereby granted, free of charge, to any person obtaining a copy 
// of this software and associated documentation files (the "Software"), to deal 
// in the Software without restriction, including without limitation the rights 
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
// copies of the Software, and to permit persons to whom the Software is 
// furnished to do so, subject to the following conditions: 
//  
// The above copyright notice and this permission notice shall be included in 
// all copies or substantial portions of the Software. 
//  
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
// THE SOFTWARE. 


//The MIT License (MIT)
//
//Copyright(c) 2016 Smartrak

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.


//https://github.com/Dan6040/SharpDX-Rastertek-Tutorials
//https://github.com/Smartrak/WpfSharpDxControl
//https://github.com/sharpdx/SharpDX-Samples

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
