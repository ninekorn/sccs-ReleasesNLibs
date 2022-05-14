using System;

using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using SharpDX.Windows;

using Win32.Shared.Interfaces;

using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;
using Resource = SharpDX.Direct3D11.Resource;

using System.Diagnostics;

using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Win32.Shared
{
    public class DxWindow : IDisposable
    {
        int bitmapcounter = 0;
        int somecounter = 0;
        System.Drawing.Bitmap _bitmap;
        System.Drawing.Rectangle boundsRect;
        System.Drawing.Imaging.BitmapData bmpData;
        int _bytesTotal;
        Texture2DDescription _textureDescription;
        byte[] _textureByteArray;
        Texture2D _texture2D;


        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        IntPtr vewindowsfoundedz;
        private readonly ICaptureMethod _captureMethod;
        private readonly string _title;

        public DxWindow(string title, ICaptureMethod captureMethod)
        {
            _title = title;
            _captureMethod = captureMethod;
        }

        public void Dispose()
        {
            _captureMethod?.Dispose();
        }

        public void Show()
        {
            SharpDX.Windows.RenderForm form = new SharpDX.Windows.RenderForm(_title);

            // create a Device and SwapChain
            var swapChainDescription = new SwapChainDescription
            {
                BufferCount = 2,
                Flags = SwapChainFlags.None,
                IsWindowed = true,
                ModeDescription = new ModeDescription(form.ClientSize.Width, form.ClientSize.Height, new Rational(60, 1), Format.B8G8R8A8_UNorm),
                OutputHandle = form.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport, swapChainDescription, out var device, out var swapChain);
            var swapChain1 = swapChain.QueryInterface<SwapChain1>();

            // ignore all Windows events
            var factory = swapChain1.GetParent<Factory>();
            factory.MakeWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAll);

            var vertexShaderByteCode = ShaderBytecode.CompileFromFile("./Shader.fx", "VS", "vs_5_0");
            var vertexShader = new VertexShader(device, vertexShaderByteCode);

            var pixelShaderByteCode = ShaderBytecode.CompileFromFile("./Shader.fx", "PS", "ps_5_0");
            var pixelShader = new PixelShader(device, pixelShaderByteCode);

            var layout = new InputLayout(device, vertexShaderByteCode, new[]
            {
                new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                new InputElement("TEXCOORD", 0, Format.R32G32_Float, 12, 0)
            });

            var vertexes = Buffer.Create(device, BindFlags.VertexBuffer, new[]
            {
                new Vertex { Position = new RawVector3(-1.0f, 1.0f, 0.5f), TexCoord = new RawVector2(0.0f, 0.0f) },
                new Vertex { Position = new RawVector3(1.0f, 1.0f, 0.5f), TexCoord = new RawVector2(1.0f, 0.0f) },
                new Vertex { Position = new RawVector3(-1.0f, -1.0f, 0.5f), TexCoord = new RawVector2(0.0f, 1.0f) },
                new Vertex { Position = new RawVector3(1.0f, -1.0f, 0.5f), TexCoord = new RawVector2(1.0f, 1.0f) }
            });

            var samplerStateDescription = new SamplerStateDescription
            {
                AddressU = TextureAddressMode.Wrap,
                AddressV = TextureAddressMode.Wrap,
                AddressW = TextureAddressMode.Wrap,
                Filter = Filter.MinMagMipLinear
            };

            device.ImmediateContext.InputAssembler.InputLayout = layout;
            device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleStrip;
            device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexes, Utilities.SizeOf<Vertex>(), 0));
            device.ImmediateContext.VertexShader.Set(vertexShader);
            device.ImmediateContext.PixelShader.SetSampler(0, new SamplerState(device, samplerStateDescription));
            device.ImmediateContext.PixelShader.Set(pixelShader);

            // create a first views
            var backBuffer = Resource.FromSwapChain<Texture2D>(swapChain1, 0);
            var renderView = new RenderTargetView(device, backBuffer);

            device.ImmediateContext.Rasterizer.SetViewport(0, 0, form.ClientSize.Width, form.ClientSize.Height);
            device.ImmediateContext.OutputMerger.SetTargets(renderView);

            // listen events (but processed in render loop)
            var isResized = false;
            form.UserResized += (_, __) => isResized = true;


            /*Process[] processlist = Process.GetProcesses();

            foreach (Process process in processlist)
            {
                if (process.ProcessName.ToLower() == "voidexpanse")
                {
                    //Console.WriteLine("Process: {0} ID: {1} Window title: {2}", process.ProcessName, process.Id, process.MainWindowTitle);
                    //MessageBox((IntPtr)0, "voidexpanse" + " " + process.MainWindowHandle, "sccoresystems0", 0);
                    vewindowsfoundedz = process.MainWindowHandle;
                }
            }*/


            //_captureMethod.StartCapture(vewindowsfoundedz, device, factory);








            //var factoryy = swapChain.GetParent<Factory>();
            //factory.MakeWindowAssociation(MainWindow.consoleHandle, WindowAssociationFlags.IgnoreAll);
            //var swapChain1 = swapChain.QueryInterface<SwapChain1>();

            /*var _textureDescription = new Texture2DDescription
            {
                CpuAccessFlags = CpuAccessFlags.None,
                BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                Format = Format.B8G8R8A8_UNorm,
                Width = form.Width,
                Height = form.Height,
                OptionFlags = ResourceOptionFlags.None,
                MipLevels = 1,
                ArraySize = 1,
                SampleDescription = { Count = 1, Quality = 0 },
                Usage = ResourceUsage.Default
            };
            var _texture2D = new Texture2D(device, _textureDescription);
            byte[] _textureByteArray;

            var _bitmap = new System.Drawing.Bitmap(form.Width, form.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var boundsRect = new System.Drawing.Rectangle(0, 0, form.Width, form.Height);
            var bmpData = _bitmap.LockBits(boundsRect, System.Drawing.Imaging.ImageLockMode.ReadOnly, _bitmap.PixelFormat);
            var _bytesTotal = Math.Abs(bmpData.Stride) * _bitmap.Height;
            _bitmap.UnlockBits(bmpData);
            _textureByteArray = new byte[_bytesTotal];

            int bitmapcounter = 0;*/





            RenderLoop.Run(form, () =>
            {
                // ReSharper disable AccessToDisposedClosure
                if (!_captureMethod.IsCapturing)
                    _captureMethod.StartCapture(form.Handle, device, factory);

                if (isResized)
                {
                    Utilities.Dispose(ref backBuffer);
                    Utilities.Dispose(ref renderView);

                    swapChain1.ResizeBuffers(swapChainDescription.BufferCount, form.ClientSize.Width, form.ClientSize.Height, Format.Unknown, SwapChainFlags.None);
                    backBuffer = Resource.FromSwapChain<Texture2D>(swapChain1, 0);
                    renderView = new RenderTargetView(device, backBuffer);

                    device.ImmediateContext.Rasterizer.SetViewport(0, 0, form.ClientSize.Width, form.ClientSize.Height);
                    device.ImmediateContext.OutputMerger.SetTargets(renderView);

                    isResized = false;
                }

                // clear view
                device.ImmediateContext.ClearRenderTargetView(renderView, new RawColor4(1.0f, 1.0f, 1.0f, 1.0f));

                var texture2d = _captureMethod.TryGetNextFrameAsTexture2D(device);



             
                if (texture2d != null)
                {
                    //device.ImmediateContext.CopyResource(texture2d, _texture2D);

                    if (somecounter == 0)
                    {
                      


                        _textureDescription = new Texture2DDescription
                        {
                            CpuAccessFlags = CpuAccessFlags.None,
                            BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                            Format = Format.B8G8R8A8_UNorm,
                            Width = texture2d.Description.Width,
                            Height = texture2d.Description.Height,
                            OptionFlags = ResourceOptionFlags.None,
                            MipLevels = 1,
                            ArraySize = 1,
                            SampleDescription = { Count = 1, Quality = 0 },
                            Usage = ResourceUsage.Default
                        };
                        _texture2D = new Texture2D(device, _textureDescription);



                        _bitmap = new System.Drawing.Bitmap(texture2d.Description.Width, texture2d.Description.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        boundsRect = new System.Drawing.Rectangle(0, 0, texture2d.Description.Width, texture2d.Description.Height);
                        bmpData = _bitmap.LockBits(boundsRect, System.Drawing.Imaging.ImageLockMode.ReadOnly, _bitmap.PixelFormat);
                        _bytesTotal = Math.Abs(bmpData.Stride) * _bitmap.Height;
                        _bitmap.UnlockBits(bmpData);
                        _textureByteArray = new byte[_bytesTotal];
                        somecounter = 1;
                    }




                    //DISCARDED
                    //DISCARDED
                    //DISCARDED
                    var dataBox1 = device.ImmediateContext.MapSubresource(texture2d, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                    int memoryBitmapStride = _textureDescription.Width * 4;

                    int columns = _textureDescription.Width;
                    int rows = _textureDescription.Height;
                    IntPtr interptr1 = dataBox1.DataPointer;

                    // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (_width * 4)

                    //Marshal.Copy(interptr1, _textureByteArray, 0, _bytesTotal);
                    // Stride not the same - copy line by line
                    for (int y = 0; y < rows; y++)
                    {
                        Marshal.Copy(interptr1 + y * dataBox1.RowPitch, _textureByteArray, y * memoryBitmapStride, memoryBitmapStride);
                    }
                    /*if (dataBox1.RowPitch == memoryBitmapStride)
                    {
                        // Stride is the same
                        Marshal.Copy(interptr1, _textureByteArray, 0, _bytesTotal);
                    }
                    else
                    {
                        // Stride not the same - copy line by line
                        for (int y = 0; y < rows; y++)
                        {
                            Marshal.Copy(interptr1 + y * dataBox1.RowPitch, _textureByteArray, y * memoryBitmapStride, memoryBitmapStride);
                        }
                    }*/
                    //var somebitmap = new System.Drawing.Bitmap(columns, rows, columns * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray,0));
                    //somebitmap.Save(@"C:\Users\steve\Desktop\screenrecord\" + bitmapcounter + "_" + rows.ToString("00") + columns.ToString("00") + ".png");
                    //bitmapcounter++;
                    device.ImmediateContext.UnmapSubresource(texture2d, 0);
                    DeleteObject(interptr1);
                    //DISCARDED
                    //DISCARDED
                    //DISCARDED






                    device.ImmediateContext.CopyResource(texture2d, _texture2D);




                    var shaderResourceView = new ShaderResourceView(device, _texture2D);
                    device.ImmediateContext.PixelShader.SetShaderResource(0, shaderResourceView);

                }






                // draw it
                device.ImmediateContext.Draw(4, 0);
                swapChain1.Present(1, PresentFlags.None, new PresentParameters());

                // ReSharper restore AccessToDisposedClosure
            });

            renderView.Dispose();
            backBuffer.Dispose();
            swapChain.Dispose();
            device.Dispose();
        }
    }
}