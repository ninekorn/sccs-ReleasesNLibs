//DEVELOPED BY STEVE CHASSÉ

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct2D1;
using SharpDX.Direct3D11;
using System.Diagnostics;

namespace sccsr15forms
{
    internal class directx : IDisposable //updateprim,
    {



        //SHARPDX DIRECTX VARIABLES
        //SHARPDX DIRECTX VARIABLES
        //SHARPDX DIRECTX VARIABLES
        public int SurfaceWidth;
        public int SurfaceHeight;
        public DateTime startTime;
        private Adapter1 adapter;
        private Adapter1 _adapter;
        private Output monitor;
        private SharpDX.DXGI.Factory1 factory;
        public appconfiguration appconfiguration;
        public SharpDX.Direct3D11.Device Device;
        public SharpDX.Direct3D11.DeviceContext DeviceContext;
        public SharpDX.DXGI.SwapChain SwapChain;
        public SharpDX.Viewport viewport;
        Texture2D BackBuffer;
        public RenderTargetView RenderTargetView;
        Texture2D DepthStencilBuffer;
        public DepthStencilView DepthStencilView;
        public Matrix ProjectionMatrix;
        public Matrix WorldMatrix;
        //SHARPDX DIRECTX VARIABLES
        //SHARPDX DIRECTX VARIABLES
        //SHARPDX DIRECTX VARIABLES



        //SHARPDX DEFERRED RENDERING VARIABLES
        //SHARPDX DEFERRED RENDERING VARIABLES
        //SHARPDX DEFERRED RENDERING VARIABLES
        /*public Stopwatch clock;
        public Stopwatch fpsTimer;
        public int fpsCounter = 0;
        public bool switchToNextState = false;*/
        public State currentState;
        public State nextState;
        /*public SharpDX.Direct3D11.DeviceContext[] deferredContextsUIThread;
        public SharpDX.Direct3D11.DeviceContext[] contextPerThreadUIThread;
        public SharpDX.Direct3D11.CommandList[] commandsListUIThread;
        public SharpDX.Direct3D11.CommandList[] frozenCommandListsUIThread;

        public SharpDX.Direct3D11.DeviceContext[] deferredContextsSysThread;
        public SharpDX.Direct3D11.DeviceContext[] contextPerThreadSysThread;
        public SharpDX.Direct3D11.CommandList[] commandsListSysThread;
        public SharpDX.Direct3D11.CommandList[] frozenCommandListsSysThread;*/

        public SharpDX.Direct3D11.DeviceContext[] deferredContexts;
        public SharpDX.Direct3D11.DeviceContext[] contextPerThread;
        public SharpDX.Direct3D11.CommandList[] commandsList;
        public SharpDX.Direct3D11.CommandList[] frozenCommandLists;


        public bool supportConcurentResources;
        public bool supportCommandList;
        public enum TestType
        {
            Immediate = 0,
            Deferred = 1,
            FrozenDeferred = 2
        }
        public struct State
        {
            public bool Exit;
            public int CountCubes;
            public int ThreadCount;
            public TestType Type;
            public bool SimulateCpuUsage;
            public bool UseMap;
        }

        public const int MaxNumberOfCubes = 256;
        public const int MaxNumberOfThreads = 16;
        public const int BurnCpuFactor = 50;
        //SHARPDX DEFERRED RENDERING VARIABLES
        //SHARPDX DEFERRED RENDERING VARIABLES
        //SHARPDX DEFERRED RENDERING VARIABLES




        public directx()// : base(updateprim_)
        {

            D3D = this;

       

            Console.WriteLine("created directx");

            appconfiguration = new appconfiguration(false);

            startTime = DateTime.Now;
            //var dpiScale = GetDpiScale();



            using (var _factory = new SharpDX.DXGI.Factory1())
            {
                _adapter = _factory.GetAdapter1(0);

                using (var _output = _adapter.GetOutput(0))
                {
                    SurfaceWidth = ((SharpDX.Rectangle)_output.Description.DesktopBounds).Width;
                    SurfaceHeight = ((SharpDX.Rectangle)_output.Description.DesktopBounds).Height;
                }
            }

            // Store the vsync setting.
            //VerticalSyncEnabled = SC_skYaRk_VR_V007.DSystemConfiguration.DSystemConfiguration.VerticalSyncEnabled;

            // Create a DirectX graphics interface factory.
            factory = new SharpDX.DXGI.Factory1();

            // Use the factory to create an adapter for the primary graphics interface (video card).
            adapter = factory.GetAdapter1(0);
            



            // Get the primary adapter output (monitor).
            monitor = adapter.GetOutput(0);
         
            // Get modes that fit the DXGI_FORMAT_R8G8B8A8_UNORM display format for the adapter output (monitor).
            var modes = monitor.GetDisplayModeList(Format.R8G8B8A8_UNorm, DisplayModeEnumerationFlags.Interlaced);

            // Now go through all the display modes and find the one that matches the screen width and height.
            // When a match is found store the the refresh rate for that monitor, if vertical sync is enabled. 
            // Otherwise we use maximum refresh rate.
            var rational = new Rational(0, 1);
            if (appconfiguration.verticalsync)
            {
                foreach (var mode in modes)
                {
                    if (mode.Width == SurfaceWidth && mode.Height == SurfaceHeight)
                    {
                        rational = new Rational(mode.RefreshRate.Numerator, mode.RefreshRate.Denominator);
                        break;
                    }
                }
            }


            // Get the adapter (video card) description.
            //var adapterDescription = adapter.Description;

            // Store the dedicated video card memory in megabytes.
            //VideoCardMemory = adapterDescription.DedicatedVideoMemory >> 10 >> 10;

            // Convert the name of the video card to a character array and store it.
            //VideoCardDescription = adapterDescription.Description.Trim('\0');

            // Release the adapter output.
            monitor.Dispose();
            // Release the adapter.
            adapter.Dispose();
            // Release the factory.
            //factory.Dispose();


            // Initialize the swap chain description.
            /*swapChainDesc = new SwapChainDescription()
            {
                // Set to a single back buffer.
                BufferCount = 1,
                // Set the width and height of the back buffer.
                ModeDescription = new ModeDescription(SurfaceWidth, SurfaceHeight, rational, Format.R8G8B8A8_UNorm),
                // Set the usage of the back buffer.
                Usage = Usage.RenderTargetOutput,
                // Set the handle for the window to render to.
                OutputHandle = sccsr14sc.Form1.theHandle,
                // Turn multisampling off.
                SampleDescription = new SampleDescription(1, 0),
                // Set to full screen or windowed mode.
                IsWindowed = true, // !SC_skYaRk_VR_V007.DSystemConfiguration.DSystemConfiguration.FullScreen,
                // Don't set the advanced flags.
                Flags = SwapChainFlags.None,
                // Discard the back buffer content after presenting.
                SwapEffect = SwapEffect.Discard
            };*/




            var swapChainDesc = new SwapChainDescription
            {
                BufferCount = 2, // 2
                Flags = SwapChainFlags.None,
                IsWindowed = true,
                ModeDescription = new ModeDescription(SurfaceWidth, SurfaceHeight, new Rational(60, 1), Format.B8G8R8A8_UNorm),
                OutputHandle = sccsr15forms.Form1.theHandle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };



            //SharpDX.Direct3D11.Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.BgraSupport, swapChainDesc, out somedevice, out someswap);
            SharpDX.Direct3D11.Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None, swapChainDesc, out Device, out SwapChain);
            
            DeviceContext = Device.ImmediateContext;




            currentState = new State
            {
                CountCubes = 64,
                ThreadCount = 4,
                Type = TestType.Deferred,
                SimulateCpuUsage = false,
                UseMap = true
            };
            nextState = currentState;

            /*
            deferredContextsUIThread = new SharpDX.Direct3D11.DeviceContext[MaxNumberOfThreads];
            for (int i = 0; i < deferredContextsUIThread.Length; i++)
            {
                deferredContextsUIThread[i] = new SharpDX.Direct3D11.DeviceContext(Device);
            }
            contextPerThreadUIThread = new SharpDX.Direct3D11.DeviceContext[MaxNumberOfThreads];
            contextPerThreadUIThread[0] = Device.ImmediateContext;
            commandsListUIThread = new SharpDX.Direct3D11.CommandList[MaxNumberOfThreads]
            frozenCommandListsUIThread = null;



            deferredContextsSysThread = new SharpDX.Direct3D11.DeviceContext[MaxNumberOfThreads];
            for (int i = 0; i < deferredContextsSysThread.Length; i++)
            {
                deferredContextsSysThread[i] = new SharpDX.Direct3D11.DeviceContext(Device);
            }
            contextPerThreadSysThread = new SharpDX.Direct3D11.DeviceContext[MaxNumberOfThreads];
            contextPerThreadSysThread[0] = Device.ImmediateContext;
            commandsListSysThread = new SharpDX.Direct3D11.CommandList[MaxNumberOfThreads]
            frozenCommandListsSysThread = null;*/



            deferredContexts = new SharpDX.Direct3D11.DeviceContext[MaxNumberOfThreads];
            for (int i = 0; i < deferredContexts.Length; i++)
            {
                deferredContexts[i] = new SharpDX.Direct3D11.DeviceContext(Device);
            }
            contextPerThread = new SharpDX.Direct3D11.DeviceContext[MaxNumberOfThreads];
            contextPerThread[0] = Device.ImmediateContext;
            commandsList = new SharpDX.Direct3D11.CommandList[MaxNumberOfThreads];
            frozenCommandLists = null;





            Device.CheckThreadingSupport(out supportConcurentResources, out supportCommandList);


            Console.WriteLine("supportConcurentResources:" + supportConcurentResources + "/supportCommandList:" + supportCommandList);

            /*
            // Use clock 
            clock = new Stopwatch();
            clock.Start();

            fpsTimer = new Stopwatch();
            fpsTimer.Start();
            fpsCounter = 0;*/





            /*switchToNextState = false;

            // Install keys handlers 
            sccsr15forms.Form1.currentform.KeyDown += (target, arg) =>
            {
                if (arg.KeyCode == Keys.Left && nextState.CountCubes > 1)
                    nextState.CountCubes--;
                if (arg.KeyCode == Keys.Right && nextState.CountCubes < MaxNumberOfCubes)
                    nextState.CountCubes++;

                if (arg.KeyCode == Keys.F1)
                    nextState.Type = (TestType)((((int)nextState.Type) + 1) % 3);
                if (arg.KeyCode == Keys.F2)
                    nextState.UseMap = !nextState.UseMap;
                if (arg.KeyCode == Keys.F3)
                    nextState.SimulateCpuUsage = !nextState.SimulateCpuUsage;

                if (nextState.Type == TestType.Deferred)
                {
                    if (arg.KeyCode == Keys.Down && nextState.ThreadCount > 1)
                        nextState.ThreadCount--;
                    if (arg.KeyCode == Keys.Up && nextState.ThreadCount < MaxNumberOfThreads)
                        nextState.ThreadCount++;
                }
                if (arg.KeyCode == Keys.Escape)
                    nextState.Exit = true;
                switchToNextState = true;
            };*/





            // Get the pointer to the back buffer.
            BackBuffer = Texture2D.FromSwapChain<Texture2D>(SwapChain, 0);

            // Create the render target view with the back buffer pointer.
            RenderTargetView = new RenderTargetView(Device, BackBuffer);

            // Release pointer to the back buffer as we no longer need it.
            //BackBuffer.Dispose();

            // Initialize and set up the description of the depth buffer.
            Texture2DDescription depthBufferDesc1 = new Texture2DDescription()
            {
                Width = SurfaceWidth,
                Height = SurfaceHeight,
                MipLevels = 1,
                ArraySize = 1,
                Format = Format.D24_UNorm_S8_UInt, // Format.D24_UNorm_S8_UInt,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            };




            // Create the texture for the depth buffer using the filled out description.
             DepthStencilBuffer = new Texture2D(Device, depthBufferDesc1);

            /*// Initialize and set up the description of the stencil state.
            DepthStencilStateDescription depthStencilDesc = new DepthStencilStateDescription()
            {
                IsDepthEnabled = true,
                DepthWriteMask = DepthWriteMask.All,
                DepthComparison = Comparison.Less,
                IsStencilEnabled = true,
                StencilReadMask = 0xFF,
                StencilWriteMask = 0xFF,
                // Stencil operation if pixel front-facing.
                FrontFace = new DepthStencilOperationDescription()
                {
                    FailOperation = StencilOperation.Keep,
                    DepthFailOperation = StencilOperation.Increment,
                    PassOperation = StencilOperation.Keep,
                    Comparison = Comparison.Always
                },
                // Stencil operation if pixel is back-facing.
                BackFace = new DepthStencilOperationDescription()
                {
                    FailOperation = StencilOperation.Keep,
                    DepthFailOperation = StencilOperation.Increment,
                    PassOperation = StencilOperation.Keep,
                    Comparison = Comparison.Always
                }
            };

            // Create the depth stencil state.
            _depthStencilState = new DepthStencilState(device, depthStencilDesc);

            // Set the depth stencil state.
            DeviceContext.OutputMerger.SetDepthStencilState(_depthStencilState, 1);*/





            // Initialize and set up the depth stencil view.
            DepthStencilViewDescription depthStencilViewDesc = new DepthStencilViewDescription()
            {
                Format = Format.D24_UNorm_S8_UInt,
                Dimension = DepthStencilViewDimension.Texture2D,
                Texture2D = new DepthStencilViewDescription.Texture2DResource()
                {
                    MipSlice = 0
                }
            };

            // Create the depth stencil view.
            DepthStencilView = new DepthStencilView(Device, DepthStencilBuffer, depthStencilViewDesc);

            // Bind the render target view and depth stencil buffer to the output render pipeline.
            DeviceContext.OutputMerger.SetTargets(DepthStencilView, RenderTargetView);

            /*// Setup the raster description which will determine how and what polygon will be drawn.
            var rasterDesc = new RasterizerStateDescription()
            {
                IsAntialiasedLineEnabled = false,
                CullMode = CullMode.Back,
                DepthBias = 0,
                DepthBiasClamp = .0f,
                IsDepthClipEnabled = true,
                FillMode = SharpDX.Direct3D11.FillMode.Solid,
                IsFrontCounterClockwise = true,
                IsMultisampleEnabled = false,
                IsScissorEnabled = false,
                SlopeScaledDepthBias = .0f
            };

            // Create the rasterizer state from the description we just filled out.
            RasterstateCullBack = new RasterizerState(Device, rasterDesc);

            // Now set the rasterizer state.
            DeviceContext.Rasterizer.State = RasterstateCullBack;
            */



            /*
            // Setup the raster description which will determine how and what polygon will be drawn.
            rasterDesc = new RasterizerStateDescription()
            {
                IsAntialiasedLineEnabled = false,
                CullMode = CullMode.None,
                DepthBias = 0,
                DepthBiasClamp = .0f,
                IsDepthClipEnabled = true,
                FillMode = FillMode.Solid,
                IsFrontCounterClockwise = true,
                IsMultisampleEnabled = false,
                IsScissorEnabled = false,
                SlopeScaledDepthBias = .0f
            };

            // Create the rasterizer state from the description we just filled out.
             RasterstateCullNone = new RasterizerState(device, rasterDesc);


            // Setup the raster description which will determine how and what polygon will be drawn.
            rasterDesc = new RasterizerStateDescription()
            {
                IsAntialiasedLineEnabled = false,
                CullMode = CullMode.Front,
                DepthBias = 0,
                DepthBiasClamp = .0f,
                IsDepthClipEnabled = true,
                FillMode = FillMode.Solid,
                IsFrontCounterClockwise = true,
                IsMultisampleEnabled = false,
                IsScissorEnabled = false,
                SlopeScaledDepthBias = .0f
            };

            // Create the rasterizer state from the description we just filled out.
             RasterstateCullFront = new RasterizerState(device, rasterDesc);*/



            viewport = new Viewport(0, 0, SurfaceWidth, SurfaceHeight, 0.0f, 1.0f);


            // Setup and create the viewport for rendering.
            DeviceContext.Rasterizer.SetViewport(viewport);

            // Setup and create the projection matrix.
            ProjectionMatrix = SharpDX.Matrix.PerspectiveFovLH((float)(Math.PI / 4), ((float)SurfaceWidth / (float)SurfaceHeight), 0.1f, 1000.0f);

            // Initialize the world matrix to the identity matrix.
            WorldMatrix = SharpDX.Matrix.Identity;
        }


        public static directx D3D;// = Instance;


        ~directx()
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
                SurfaceWidth = 0;
                SurfaceHeight = 0;
                //startTime = null;

                if (adapter!= null)
                {
                    adapter.Dispose();
                    adapter = null;
                }

                if (appconfiguration != null)
                {
                    appconfiguration.Dispose();
                    appconfiguration = null;
                }

                if (Device != null)
                {
                    Device.Dispose();
                    Device = null;
                }

                if (DeviceContext != null)
                {
                    DeviceContext.Dispose();
                    DeviceContext = null;
                }

                if (SwapChain != null)
                {
                    SwapChain.Dispose();
                    SwapChain = null;
                }

                //cannot be null. cannot be disposed of.
                //if (viewport != null)
                //{
                //    viewport.Dispose();
                //    viewport = null;
                //}

                if (BackBuffer != null)
                {
                    BackBuffer.Dispose();
                    BackBuffer = null;
                }

                if (RenderTargetView != null)
                {
                    RenderTargetView.Dispose();
                    RenderTargetView = null;
                }

                if (DepthStencilBuffer != null)
                {
                    DepthStencilBuffer.Dispose();
                    DepthStencilBuffer = null;
                }

                if (DepthStencilView != null)
                {
                    DepthStencilView.Dispose();
                    DepthStencilView = null;
                }
                if (_adapter != null)
                {
                    _adapter.Dispose();
                    _adapter = null;
                }

                if (factory != null)
                {
                    factory.Dispose();
                    factory = null;
                }

                if (monitor != null)
                {
                    monitor.Dispose();
                    monitor = null;
                }
                


                ProjectionMatrix = Matrix.Zero;
                WorldMatrix = Matrix.Zero;


                /*
                private Adapter1 adapter;
                public appconfiguration appconfiguration;
                public SharpDX.Direct3D11.Device Device;
                public SharpDX.Direct3D11.DeviceContext DeviceContext;
                public SharpDX.DXGI.SwapChain SwapChain;
                public SharpDX.Viewport viewport;
                Texture2D BackBuffer;
                RenderTargetView RenderTargetView;
                Texture2D DepthStencilBuffer;
                DepthStencilView DepthStencilView;
                Matrix ProjectionMatrix;
                Matrix WorldMatrix;*/
                // Dispose all owned managed objects
            }

            // Release unmanaged resources
        }

    }
}
