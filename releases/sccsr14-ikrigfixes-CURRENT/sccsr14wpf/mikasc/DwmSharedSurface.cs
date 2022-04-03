using System;

using SharpDX.Direct3D11;
using SharpDX.DXGI;

using Win32.DwmSharedSurface.Interop;
using Win32.Shared;
using Win32.Shared.Interfaces;

using Device = SharpDX.Direct3D11.Device;

using System.Runtime.InteropServices;
using Win32.DwmSharedSurface; 



namespace Win32.DwmSharedSurface
{
    public class DwmSharedSurface : ICaptureMethod
    {
        //[DllImport("User32.dll", CharSet = CharSet.Unicode)]
        //public static extern int MessageBox(IntPtr h, string m, string c, int type);
        public IntPtr _hWnd;

        //MainWindow mainwindow;
        public DwmSharedSurface()
        {
            //mainwindow = mainwindow_;
            IsCapturing = false;
        }

        public void Dispose()
        {
            StopCapture();
        }

        public bool IsCapturing { get; private set; }

        public void StartCapture(IntPtr hWnd, Device device, Factory factory)
        {
            WindowPicker picker = new WindowPicker();
            _hWnd = picker.PickCaptureTarget(hWnd);
            //MessageBox((IntPtr)0, "id " + hWnd, "sccoresystems0", 0);
            //MessageBox((IntPtr)0, "id " + _hWnd, "sccoresystems0", 0);
            if (_hWnd == IntPtr.Zero)
                return;

            IsCapturing = true;
        }
        Texture2DDescription texture2dDescription;
        int swtc = 0;
        Texture2D texture2d;

        public Texture2D TryGetNextFrameAsTexture2D(Device device)
        {
            if (_hWnd == IntPtr.Zero)
                return null;

            NativeMethods.DwmGetDxSharedSurface(_hWnd, out var phSurface, out _, out _, out _, out _);
            if (phSurface == IntPtr.Zero)
            {
                // Window Lost
                StopCapture();
                return null;
            }

            using (var surfaceTexture = device.OpenSharedResource<Texture2D>(phSurface))
            {
                if (swtc == 0)
                {
                    /*texture2dDescription = new Texture2DDescription
                    {
                        ArraySize = 1,
                        BindFlags = BindFlags.None, // ShaderResource | BindFlags.RenderTarget,
                        CpuAccessFlags = CpuAccessFlags.Read,
                        Format = Format.B8G8R8A8_UNorm,
                        Height = surfaceTexture.Description.Height,
                        MipLevels = 1,
                        SampleDescription = new SampleDescription(1, 0),
                        Usage = ResourceUsage.Staging,
                        Width = surfaceTexture.Description.Width
                    };

                    texture2d = new Texture2D(device, texture2dDescription);*/

                    // using var surfaceTexture = new Texture2D(phSurface);

                    texture2dDescription = new Texture2DDescription
                    {
                        ArraySize = 1,
                        BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                        CpuAccessFlags = CpuAccessFlags.None,
                        Format = Format.B8G8R8A8_UNorm,
                        Height = surfaceTexture.Description.Height,
                        MipLevels = 1,
                        SampleDescription = new SampleDescription(1, 0),
                        Usage = ResourceUsage.Default,
                        Width = surfaceTexture.Description.Width
                    };
                    texture2d = new Texture2D(device, texture2dDescription);
                    swtc = 1;
                }


                /*texture2dDescription = new Texture2DDescription
                {
                    ArraySize = 1,
                    BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                    CpuAccessFlags = CpuAccessFlags.None,
                    Format = Format.B8G8R8A8_UNorm,
                    Height = surfaceTexture.Description.Height,
                    MipLevels = 1,
                    SampleDescription = new SampleDescription(1, 0),
                    Usage = ResourceUsage.Default,
                    Width = surfaceTexture.Description.Width
                };
                texture2d = new Texture2D(device, texture2dDescription);*/



                device.ImmediateContext.CopyResource(surfaceTexture, texture2d);
            }





            return texture2d;
        }

        public void StopCapture()
        {
            _hWnd = IntPtr.Zero;
            IsCapturing = false;
        }

        /*

        bool releasedFrame = true;
        bool releaseFrame()
        {
            //_texture2D.Dispose(); // lags like fucking hell
            for (int i = 0; i < 2; i++)
            {
                releasedFrame = true;
                try
                {
                    duplication.ReleaseFrame();
                }
                catch (SharpDXException ex)
                {
                    releasedFrame = false;
                    Console.WriteLine(ex.ToString());
                }

                if (releasedFrame)
                {
                    break;
                }
            }
            if (releasedFrame)
            {
                return true;
            }
            else
            {
                //sccs.scgraphics.scupdate.sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, this._device); // not that good but let's leave it at that.
                return false;
            }
        }*/

    }
}