////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//DEVELOPED BY STEVE CHASSÉ using xoofx's sharpdx original deferred rendering sample. This is a software of mixed architecture//
//using rastertek c# github user dan6040's sample architecture and smartrak's sample architecture and xoofx sharpdx samples/////
//architecture./////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
using SharpDX.DXGI;
using SharpDX.Direct2D1;
using SharpDX.Direct3D11;
using System.Diagnostics;

using Ab3d.OculusWrap;
using Ab3d.OculusWrap.DemoDX11;

using Result = Ab3d.OculusWrap.Result;

namespace sccsr15forms
{
    public class directx : IDisposable //updateprim,
    {
        




        public enum BodyTag
        {

            DrawMe,
            DontDrawMe,
            Terrain,
            pseudoCloth,


            PlayerHandLeft,
            PlayerHandRight,
            PlayerShoulderLeft,
            PlayerShoulderRight,
            PlayerTorso,
            PlayerPelvis,
            PlayerUpperArmLeft,
            PlayerLowerArmLeft,
            PlayerUpperArmRight,
            PlayerLowerArmRight,
            PlayerUpperLegLeft,
            PlayerLowerLegLeft,
            PlayerUpperLegRight,
            PlayerLowerLegRight,
            PlayerFootRight,
            PlayerFootLeft,
            PlayerHead,
            PlayerLeftElbowTarget,
            PlayerRightHandGrabTarget,
            PlayerLeftHandGrabTarget,

            PlayerRightElbowTarget,
            PlayerLeftElbowTargettwo,
            PlayerRightElbowTargettwo,
            PlayerLeftTargetKnee,
            PlayerRightTargetKnee,
            PlayerLeftTargettwoKnee,
            PlayerRightTargettwoKnee,


            sc_containment_grid,
            sc_grid,

            Screen,
            sc_jitter_cloth,
            //someothertest,
            //testChunkCloth,
            //cloth_cube,
            //screen_corners,
            //screen_pointer_touch,
            //screen_pointer_HMD,
            _terrain_tiles,
            _terrain,
            _floor,
            //_icosphere,
            //_sphere,
            _spectrum,
            //_physics_cube_group_b,
            _screen_assets,


            physicsInstancedCube,
            physicsInstancedCone,
            physicsInstancedCylinder,
            physicsInstancedCapsule,
            physicsInstancedSphere,

            sc_perko_voxel,
            physicsInstancedScreen,
            sc_perko_voxel_planet_chunk,
            physicsinstancedvertexbindingchunk
        }



        //AB3D.OCULUSWRAP VARIABLES
        //AB3D.OCULUSWRAP VARIABLES
        //AB3D.OCULUSWRAP VARIABLES
        public Ab3d.OculusWrap.OvrWrap OVR;
        public IntPtr sessionPtr;
        MirrorTexture mirrorTexture = null;
        public HmdDesc hmdDesc;
        public Result result;
        public LayerEyeFov layerEyeFov;
        public EyeTexture[] eyeTextures;
        public SharpDX.Direct3D11.Texture2D mirrorTextureD3D;
        public ControllerType controllerTypeRTouch;
        public ControllerType controllerTypeLTouch;
        public Ab3d.OculusWrap.InputState inputStateLTouch;
        public Ab3d.OculusWrap.InputState inputStateRTouch;
        Guid textureInterfaceId = new Guid("6f15aaf2-d208-4e89-9ab4-489535d34f9c"); // Interface ID of the Direct3D Texture2D interface.
        //AB3D.OCULUSWRAP VARIABLES
        //AB3D.OCULUSWRAP VARIABLES
        //AB3D.OCULUSWRAP VARIABLES




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
        public Texture2D BackBuffer;
        public RenderTargetView RenderTargetView;
        Texture2D DepthStencilBuffer;
        public DepthStencilView DepthStencilView;
        public Matrix ProjectionMatrix;
        public Matrix WorldMatrix = Matrix.Identity;
        public RasterizerState RasterState;
        public DepthStencilState DepthStencilState;
        public Matrix OrthoMatrix;
        public DepthStencilState DepthDisabledStencilState;
        public BlendState AlphaEnableBlendingState;
        public BlendState AlphaDisableBlendingState;
        //SHARPDX DIRECTX VARIABLES
        //SHARPDX DIRECTX VARIABLES
        //SHARPDX DIRECTX VARIABLES


        //SHARPDX DEFERRED RENDERING VARIABLES
        //SHARPDX DEFERRED RENDERING VARIABLES
        //SHARPDX DEFERRED RENDERING VARIABLES
        //public State currentState;
        //public State nextState;
        /*public SharpDX.Direct3D11.DeviceContext[] deferredContexts;
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
        public int MaxNumberOfCubes = 1024;
        public const int MaxNumberOfThreads = 16;
        public const int BurnCpuFactor = 50;*/
        //SHARPDX DEFERRED RENDERING VARIABLES
        //SHARPDX DEFERRED RENDERING VARIABLES
        //SHARPDX DEFERRED RENDERING VARIABLES

        public IntPtr apphandle;


        public directx()// : base(updateprim_)
        {
            D3D = this;

            apphandle = sccsr15forms.Form1.theHandle;



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





            if (Program.useOculusRift == 0)
            {


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
                    OutputHandle = apphandle,
                    SampleDescription = new SampleDescription(1, 0),
                    SwapEffect = SwapEffect.Discard,
                    Usage = Usage.RenderTargetOutput
                };



                //SharpDX.Direct3D11.Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.BgraSupport, swapChainDesc, out someDevice, out someswap);
                SharpDX.Direct3D11.Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None, swapChainDesc, out Device, out SwapChain);

                DeviceContext = Device.ImmediateContext;



                /*
                currentState = new State
                {
                    CountCubes = 64, // 64
                    ThreadCount = 4,
                    Type = TestType.Deferred,
                    SimulateCpuUsage = false,
                    UseMap = true
                };
                nextState = currentState;*/

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














                /*deferredContexts = new SharpDX.Direct3D11.DeviceContext[MaxNumberOfThreads];
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
                */

















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
                BackBuffer.Dispose();



                /*
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
                */











                #region
                // Initialize and set up the description of the depth buffer.
                var depthBufferDesc = new Texture2DDescription()
                {
                    Width = SurfaceWidth,
                    Height = SurfaceHeight,
                    MipLevels = 1,
                    ArraySize = 1,
                    Format = Format.D24_UNorm_S8_UInt,
                    SampleDescription = new SampleDescription(1, 0),
                    Usage = ResourceUsage.Default,
                    BindFlags = BindFlags.DepthStencil,
                    CpuAccessFlags = CpuAccessFlags.None,
                    OptionFlags = ResourceOptionFlags.None
                };

                // Create the texture for the depth buffer using the filled out description.
                DepthStencilBuffer = new Texture2D(Device, depthBufferDesc);
                #endregion

                #region Initialize Depth Enabled Stencil
                // Initialize and set up the description of the stencil state.
                var depthStencilDesc = new DepthStencilStateDescription()
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
                        DepthFailOperation = StencilOperation.Decrement,
                        PassOperation = StencilOperation.Keep,
                        Comparison = Comparison.Always
                    }
                };

                // Create the depth stencil state.
                DepthStencilState = new DepthStencilState(Device, depthStencilDesc);
                #endregion

                #region Initialize Output Merger
                // Set the depth stencil state.
                DeviceContext.OutputMerger.SetDepthStencilState(DepthStencilState, 1);

                // Initialize and set up the depth stencil view.
                var depthStencilViewDesc = new DepthStencilViewDescription()
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
                #endregion

                #region Initialize Raster State
                // Setup the raster description which will determine how and what polygon will be drawn.
                var rasterDesc = new RasterizerStateDescription()
                {
                    IsAntialiasedLineEnabled = false,
                    CullMode = CullMode.Back,
                    DepthBias = 0,
                    DepthBiasClamp = .0f,
                    IsDepthClipEnabled = true,
                    FillMode = SharpDX.Direct3D11.FillMode.Solid,
                    IsFrontCounterClockwise = false,
                    IsMultisampleEnabled = false,
                    IsScissorEnabled = false,
                    SlopeScaledDepthBias = .0f
                };

                // Create the rasterizer state from the description we just filled out.
                RasterState = new RasterizerState(Device, rasterDesc);
                #endregion

                #region Initialize Rasterizer
                // Now set the rasterizer state.
                DeviceContext.Rasterizer.State = RasterState;

                // Setup and create the viewport for rendering.
                //DeviceContext.Rasterizer.SetViewport(0, 0, SurfaceWidth, SurfaceHeight, 0, 1);
                #endregion





                #region Initialize matrices
                // Setup and create the projection matrix.
                //ProjectionMatrix = Matrix.PerspectiveFovLH((float)(Math.PI / 4), ((float)SurfaceWidth / (float)SurfaceHeight), appconfiguration.ScreenNear, appconfiguration.ScreenDepth);

                // Initialize the world matrix to the identity matrix.
                //WorldMatrix = Matrix.Identity;

                // Create an orthographic projection matrix for 2D rendering.
                OrthoMatrix = Matrix.OrthoLH(SurfaceWidth, SurfaceHeight, appconfiguration.ScreenNear, appconfiguration.ScreenDepth);
                #endregion

                #region Initialize Depth Disabled Stencil
                // Now create a second depth stencil state which turns off the Z buffer for 2D rendering. Added in Tutorial 11
                // The difference is that DepthEnable is set to false.
                // All other parameters are the same as the other depth stencil state.
                var depthDisabledStencilDesc = new DepthStencilStateDescription()
                {
                    IsDepthEnabled = false,
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
                        DepthFailOperation = StencilOperation.Decrement,
                        PassOperation = StencilOperation.Keep,
                        Comparison = Comparison.Always
                    }
                };

                // Create the depth stencil state.
                DepthDisabledStencilState = new DepthStencilState(Device, depthDisabledStencilDesc);
                #endregion

                #region Initialize Blend States
                // Create an alpha enabled blend state description.
                var blendStateDesc = new BlendStateDescription();
                blendStateDesc.RenderTarget[0].IsBlendEnabled = true;
                blendStateDesc.RenderTarget[0].SourceBlend = BlendOption.One;
                blendStateDesc.RenderTarget[0].DestinationBlend = BlendOption.InverseSourceAlpha;
                blendStateDesc.RenderTarget[0].BlendOperation = SharpDX.Direct3D11.BlendOperation.Add;
                blendStateDesc.RenderTarget[0].SourceAlphaBlend = BlendOption.One;
                blendStateDesc.RenderTarget[0].DestinationAlphaBlend = BlendOption.Zero;
                blendStateDesc.RenderTarget[0].AlphaBlendOperation = SharpDX.Direct3D11.BlendOperation.Add;
                blendStateDesc.RenderTarget[0].RenderTargetWriteMask = ColorWriteMaskFlags.All;

                // Create the blend state using the description.
                AlphaEnableBlendingState = new BlendState(Device, blendStateDesc);

                // Modify the description to create an disabled blend state description.
                blendStateDesc.RenderTarget[0].IsBlendEnabled = false;

                // Create the blend state using the description.
                AlphaDisableBlendingState = new BlendState(Device, blendStateDesc);
                #endregion


















                viewport = new Viewport(0, 0, SurfaceWidth, SurfaceHeight, 0.0f, 1.0f);


                // Setup and create the viewport for rendering.
                DeviceContext.Rasterizer.SetViewport(viewport);

                // Setup and create the projection matrix.
                ProjectionMatrix = SharpDX.Matrix.PerspectiveFovLH((float)(Math.PI / 4), ((float)SurfaceWidth / (float)SurfaceHeight), 0.1f, 1000.0f);

                // Initialize the world matrix to the identity matrix.
                WorldMatrix = SharpDX.Matrix.Identity;
            }
            else if (Program.useOculusRift == 1)
            {

                controllerTypeRTouch = ControllerType.RTouch;
                controllerTypeLTouch = ControllerType.LTouch;

                //---------------------------------------------------------\\ OCULUS WRAP


                Result result;

                OVR = OvrWrap.Create();

                // Define initialization parameters with debug flag.
                InitParams initializationParameters = new InitParams();
                initializationParameters.Flags = InitFlags.RequestVersion; //InitFlags.Debug | 
                initializationParameters.RequestedMinorVersion = 17;

                // Initialize the Oculus runtime.
                string errorReason = null;
                try
                {
                    result = OVR.Initialize(initializationParameters);

                    if (result < Result.Success)
                        errorReason = result.ToString();
                }
                catch (Exception ex)
                {
                    errorReason = ex.Message;
                }

                if (errorReason != null)
                {
                    //MessageBox.Show("Failed to initialize the Oculus runtime library:\r\n" + errorReason, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Use the head mounted display.
                sessionPtr = IntPtr.Zero;
                var graphicsLuid = new GraphicsLuid();
                result = OVR.Create(ref sessionPtr, ref graphicsLuid);
                if (result < Result.Success)
                {
                    //MessageBox.Show("The HMD is not enabled: " + result.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                hmdDesc = OVR.GetHmdDesc(sessionPtr);


                try
                {
                    SwapChainDescription swapChainDescription = new SwapChainDescription();
                    swapChainDescription.BufferCount = 1;
                    swapChainDescription.IsWindowed = true;
                    swapChainDescription.OutputHandle = apphandle;
                    swapChainDescription.SampleDescription = new SampleDescription(1, 0);
                    swapChainDescription.Usage = Usage.RenderTargetOutput | Usage.ShaderInput;//Usage.RenderTargetOutput;// | Usage.ShaderInput;
                    swapChainDescription.SwapEffect = SwapEffect.Sequential;
                    swapChainDescription.Flags = SwapChainFlags.AllowModeSwitch;
                    swapChainDescription.ModeDescription.Width = SurfaceWidth;
                    swapChainDescription.ModeDescription.Height = SurfaceHeight;
                    swapChainDescription.ModeDescription.Format = Format.R8G8B8A8_UNorm;
                    swapChainDescription.ModeDescription.RefreshRate.Numerator = 0;
                    swapChainDescription.ModeDescription.RefreshRate.Denominator = 1;



                    /*
                    var rational = new Rational(0, 1);

                    // Initialize the swap chain description.
                    var swapChainDescription = new SwapChainDescription()
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
                        IsWindowed = !DSystemConfiguration.FullScreen,
                        // Don't set the advanced flags.
                        Flags = SwapChainFlags.None,
                        // Discard the back buffer content after presenting.
                        SwapEffect = SwapEffect.Discard
                    };*/









                    // Create a set of layers to submit.
                    eyeTextures = new EyeTexture[2];

                    // Create DirectX drawing Device.
                    //Device = new Device(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.Debug);
                    //Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None, swapChainDescription, out Device, out swapChain);

                    SwapChain someswap;
                    SharpDX.Direct3D11.Device someDevice;
                    // Create DirectX drawing Device.
                    //Device = new Device(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.Debug);
                    SharpDX.Direct3D11.Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None, swapChainDescription, out someDevice, out someswap);

                    SwapChain = someswap;
                    //Device = someDevice;
                    Device = someDevice;

                    // Create DirectX Graphics Interface factory, used to create the swap chain.
                    var factory = new SharpDX.DXGI.Factory4();

                    DeviceContext = Device.ImmediateContext;

                    /*// Define the properties of the swap chain.
                    SwapChainDescription swapChainDescription = new SwapChainDescription();
                    swapChainDescription.BufferCount = 1;
                    swapChainDescription.IsWindowed = true;
                    swapChainDescription.OutputHandle = apphandle;
                    swapChainDescription.SampleDescription = new SampleDescription(1, 0);
                    swapChainDescription.Usage = Usage.RenderTargetOutput | Usage.ShaderInput;//Usage.RenderTargetOutput;// | Usage.ShaderInput;
                    swapChainDescription.SwapEffect = SwapEffect.Sequential;
                    swapChainDescription.Flags = SwapChainFlags.AllowModeSwitch;
                    swapChainDescription.ModeDescription.Width = form.Width;
                    swapChainDescription.ModeDescription.Height = form.Height;
                    swapChainDescription.ModeDescription.Format = Format.R8G8B8A8_UNorm;
                    swapChainDescription.ModeDescription.RefreshRate.Numerator = 0;
                    swapChainDescription.ModeDescription.RefreshRate.Denominator = 1;

                    // Create the swap chain.
                    swapChain = new SwapChain(factory, Device, swapChainDescription);
                    */
                    // Retrieve the back buffer of the swap chain.
                    BackBuffer = SwapChain.GetBackBuffer<Texture2D>(0);
                    RenderTargetView = new RenderTargetView(Device, BackBuffer);

                    // Create a depth buffer, using the same width and height as the back buffer.
                    /*Texture2DDescription depthBufferDescription = new Texture2DDescription();
                    depthBufferDescription.Format = Format.D32_Float;
                    depthBufferDescription.ArraySize = 1;
                    depthBufferDescription.MipLevels = 1;
                    depthBufferDescription.Width = SurfaceWidth;
                    depthBufferDescription.Height = SurfaceHeight;
                    depthBufferDescription.SampleDescription = new SampleDescription(1, 0);
                    depthBufferDescription.Usage = ResourceUsage.Default;
                    depthBufferDescription.BindFlags = BindFlags.DepthStencil;
                    depthBufferDescription.CpuAccessFlags = CpuAccessFlags.None;
                    depthBufferDescription.OptionFlags = ResourceOptionFlags.None;

                    // Define how the depth buffer will be used to filter out objects, based on their distance from the viewer.
                    DepthStencilStateDescription depthStencilStateDescription = new DepthStencilStateDescription();
                    depthStencilStateDescription.IsDepthEnabled = true;
                    depthStencilStateDescription.DepthComparison = Comparison.Less;
                    depthStencilStateDescription.DepthWriteMask = DepthWriteMask.Zero;

                    // Create the depth buffer.
                    depthBuffer = new Texture2D(Device, depthBufferDescription);*/
                    //_depthStencilView = new DepthStencilView(Device, depthBuffer);
                    //depthStencilState = new DepthStencilState(Device, depthStencilStateDescription);

                    //var viewport = new Viewport(0, 0, hmdDesc.Resolution.Width, hmdDesc.Resolution.Height, 0.0f, 1.0f);

                    //DeviceContext.OutputMerger.SetDepthStencilState(depthStencilState);



                    /*// Initialize and set up the description of the depth buffer.
                    Texture2DDescription depthBufferDesc0 = new Texture2DDescription()
                    {
                        Width = SurfaceWidth,
                        Height = SurfaceHeight,
                        MipLevels = 1,
                        ArraySize = 1,
                        Format = Format.D24_UNorm_S8_UInt,
                        SampleDescription = new SampleDescription(1, 0),
                        Usage = ResourceUsage.Default,
                        BindFlags = BindFlags.DepthStencil,
                        CpuAccessFlags = CpuAccessFlags.None,
                        OptionFlags = ResourceOptionFlags.None
                    };

                    // Create the texture for the depth buffer using the filled out description.
                    DepthStencilBuffer = new Texture2D(Device, depthBufferDesc0);


                    // Initialize and set up the description of the stencil state.
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
                            DepthFailOperation = StencilOperation.Decrement,
                            PassOperation = StencilOperation.Keep,
                            Comparison = Comparison.Always
                        }
                    };

                    // Create the depth stencil state.
                    depthStencilState = new DepthStencilState(Device, depthStencilDesc);

                    // Set the depth stencil state.
                    Device.ImmediateContext.OutputMerger.SetDepthStencilState(depthStencilState, 1);

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
                    _depthStencilView = new DepthStencilView(Device, DepthStencilBuffer, depthStencilViewDesc);

                    Device.ImmediateContext.OutputMerger.SetRenderTargets(_depthStencilView, _renderTargetView);*/

                    var viewport = new Viewport(0, 0, hmdDesc.Resolution.Width, hmdDesc.Resolution.Height, 0.0f, 1.0f);








                    // Now set the rasterizer state.
                    //DeviceContext.Rasterizer.State = RasterState;
                    var rasterDesc = new RasterizerStateDescription()
                    {
                        IsAntialiasedLineEnabled = false,
                        CullMode = CullMode.None,
                        DepthBias = 0,
                        DepthBiasClamp = 0.0f,
                        IsDepthClipEnabled = true,
                        FillMode = SharpDX.Direct3D11.FillMode.Solid,
                        IsFrontCounterClockwise = false,
                        IsMultisampleEnabled = false,
                        IsScissorEnabled = false,
                        SlopeScaledDepthBias = 0.0f
                    };

                    // Create the rasterizer state from the description we just filled out.
                    RasterState = new RasterizerState(Device, rasterDesc);

                    // Now set the rasterizer state.
                    Device.ImmediateContext.Rasterizer.State = RasterState;




                    Device.ImmediateContext.Rasterizer.SetViewport(viewport);




                    // Retrieve the DXGI Device, in order to set the maximum frame latency.
                    using (SharpDX.DXGI.Device1 dxgiDevice = Device.QueryInterface<SharpDX.DXGI.Device1>())
                    {
                        dxgiDevice.MaximumFrameLatency = 1;
                    }

                    layerEyeFov = new LayerEyeFov();
                    layerEyeFov.Header.Type = LayerType.EyeFov;
                    layerEyeFov.Header.Flags = LayerFlags.None;

                    for (int eyeIndex = 0; eyeIndex < 2; eyeIndex++)
                    {
                        EyeType eye = (EyeType)eyeIndex;
                        var eyeTexture = new EyeTexture();
                        eyeTextures[eyeIndex] = eyeTexture;

                        // Retrieve size and position of the texture for the current eye.
                        eyeTexture.FieldOfView = hmdDesc.DefaultEyeFov[eyeIndex];
                        eyeTexture.TextureSize = OVR.GetFovTextureSize(sessionPtr, eye, hmdDesc.DefaultEyeFov[eyeIndex], 1.0f);
                        eyeTexture.RenderDescription = OVR.GetRenderDesc(sessionPtr, eye, hmdDesc.DefaultEyeFov[eyeIndex]);
                        eyeTexture.HmdToEyeViewOffset = eyeTexture.RenderDescription.HmdToEyePose.Position;
                        eyeTexture.ViewportSize.Position = new Vector2i(0, 0);
                        eyeTexture.ViewportSize.Size = eyeTexture.TextureSize;
                        eyeTexture.Viewport = new Viewport(0, 0, eyeTexture.TextureSize.Width, eyeTexture.TextureSize.Height, 0.0f, 1.0f);






                        // Define a texture at the size recommended for the eye texture.
                        eyeTexture.Texture2DDescription = new Texture2DDescription();
                        eyeTexture.Texture2DDescription.Width = eyeTexture.TextureSize.Width;
                        eyeTexture.Texture2DDescription.Height = eyeTexture.TextureSize.Height;
                        eyeTexture.Texture2DDescription.ArraySize = 1;
                        eyeTexture.Texture2DDescription.MipLevels = 1;
                        eyeTexture.Texture2DDescription.Format = Format.R8G8B8A8_UNorm;
                        eyeTexture.Texture2DDescription.SampleDescription = new SampleDescription(1, 0);
                        eyeTexture.Texture2DDescription.Usage = ResourceUsage.Default;
                        eyeTexture.Texture2DDescription.CpuAccessFlags = CpuAccessFlags.None;
                        eyeTexture.Texture2DDescription.BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget;

                        // Convert the SharpDX texture description to the Oculus texture swap chain description.
                        TextureSwapChainDesc textureSwapChainDesc = SharpDXHelpers.CreateTextureSwapChainDescription(eyeTexture.Texture2DDescription);

                        // Create a texture swap chain, which will contain the textures to render to, for the current eye.
                        IntPtr textureSwapChainPtr;

                        result = OVR.CreateTextureSwapChainDX(sessionPtr, Device.NativePointer, ref textureSwapChainDesc, out textureSwapChainPtr);
                        WriteErrorDetails(OVR, result, "Failed to create swap chain.");

                        eyeTexture.SwapTextureSet = new TextureSwapChain(OVR, sessionPtr, textureSwapChainPtr);


                        // Retrieve the number of buffers of the created swap chain.
                        int textureSwapChainBufferCount;
                        result = eyeTexture.SwapTextureSet.GetLength(out textureSwapChainBufferCount);
                        WriteErrorDetails(OVR, result, "Failed to retrieve the number of buffers of the created swap chain.");

                        // Create room for each DirectX texture in the SwapTextureSet.
                        eyeTexture.Textures = new Texture2D[textureSwapChainBufferCount];
                        eyeTexture.RenderTargetViews = new RenderTargetView[textureSwapChainBufferCount];

                        // Create a texture 2D and a render target view, for each unmanaged texture contained in the SwapTextureSet.
                        for (int textureIndex = 0; textureIndex < textureSwapChainBufferCount; textureIndex++)
                        {
                            // Retrieve the Direct3D texture contained in the Oculus TextureSwapChainBuffer.
                            IntPtr swapChainTextureComPtr = IntPtr.Zero;
                            result = eyeTexture.SwapTextureSet.GetBufferDX(textureIndex, textureInterfaceId, out swapChainTextureComPtr);
                            WriteErrorDetails(OVR, result, "Failed to retrieve a texture from the created swap chain.");

                            // Create a managed Texture2D, based on the unmanaged texture pointer.
                            eyeTexture.Textures[textureIndex] = new Texture2D(swapChainTextureComPtr);

                            // Create a render target view for the current Texture2D.
                            eyeTexture.RenderTargetViews[textureIndex] = new RenderTargetView(Device, eyeTexture.Textures[textureIndex]);





                            /*
                            // Initialize and set up the description of the depth buffer.
                            depthBufferDesc0 = new Texture2DDescription()
                            {
                                Width = SurfaceWidth,
                                Height = SurfaceHeight,
                                MipLevels = 1,
                                ArraySize = 1,
                                Format = Format.D24_UNorm_S8_UInt,
                                SampleDescription = new SampleDescription(1, 0),
                                Usage = ResourceUsage.Default,
                                BindFlags = BindFlags.DepthStencil,
                                CpuAccessFlags = CpuAccessFlags.None,
                                OptionFlags = ResourceOptionFlags.None
                            };

                            // Create the texture for the depth buffer using the filled out description.
                            DepthStencilBuffer = new Texture2D(Device, depthBufferDesc0);


                            // Initialize and set up the description of the stencil state.
                            depthStencilDesc = new DepthStencilStateDescription()
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
                            depthStencilState = new DepthStencilState(Device, depthStencilDesc);


                            eyeTexture.RenderTargetViews[textureIndex].Device.ImmediateContext.OutputMerger.SetDepthStencilState(depthStencilState, 1);*/


                            /*// Now set the rasterizer state.
                            //DeviceContext.Rasterizer.State = RasterState;
                            rasterDesc = new RasterizerStateDescription()
                            {
                                IsAntialiasedLineEnabled = false,
                                CullMode = CullMode.None,
                                DepthBias = 0,
                                DepthBiasClamp = 0.0f,
                                IsDepthClipEnabled = true,
                                FillMode = SharpDX.Direct3D11.FillMode.Solid,
                                IsFrontCounterClockwise = true,
                                IsMultisampleEnabled = false,
                                IsScissorEnabled = false,
                                SlopeScaledDepthBias = 0.0f
                            };

                            // Create the rasterizer state from the description we just filled out.
                            RasterState = new RasterizerState(Device, rasterDesc);

                            // Now set the rasterizer state.
                            //Device.ImmediateContext.Rasterizer.State = RasterState;

                            //Device.ImmediateContext.Rasterizer.SetViewport(viewport);

                            eyeTexture.RenderTargetViews[textureIndex].Device.ImmediateContext.Rasterizer.State = RasterState;*/



                            /*
                            // Initialize and set up the description of the stencil state.
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
                                    DepthFailOperation = StencilOperation.Decrement,
                                    PassOperation = StencilOperation.Keep,
                                    Comparison = Comparison.Always
                                }
                            };

                            // Create the depth stencil state.
                            depthStencilState = new DepthStencilState(Device, depthStencilDesc);

                            // Set the depth stencil state.
                            Device.ImmediateContext.OutputMerger.SetDepthStencilState(depthStencilState, eyeIndex); //1*/


                        }

                        // Define the depth buffer, at the size recommended for the eye texture.
                        eyeTexture.DepthBufferDescription = new Texture2DDescription();
                        eyeTexture.DepthBufferDescription.Format = Format.D32_Float;
                        eyeTexture.DepthBufferDescription.Width = eyeTexture.TextureSize.Width;
                        eyeTexture.DepthBufferDescription.Height = eyeTexture.TextureSize.Height;
                        eyeTexture.DepthBufferDescription.ArraySize = 1;
                        eyeTexture.DepthBufferDescription.MipLevels = 1;
                        eyeTexture.DepthBufferDescription.SampleDescription = new SampleDescription(1, 0);
                        eyeTexture.DepthBufferDescription.Usage = ResourceUsage.Default;
                        eyeTexture.DepthBufferDescription.BindFlags = BindFlags.DepthStencil;
                        eyeTexture.DepthBufferDescription.CpuAccessFlags = CpuAccessFlags.None;
                        eyeTexture.DepthBufferDescription.OptionFlags = ResourceOptionFlags.None;



                        // Create the depth buffer.
                        //eyeTexture.DepthBuffer = new Texture2D(Device, eyeTexture.DepthBufferDescription);
                        //depthStencilDesc
                        //eyeTexture.DepthStencilView = new DepthStencilView(Device, eyeTexture.DepthBuffer);




                        /*// Initialize and set up the description of the depth buffer.
                        Texture2DDescription depthBufferDesc0 = new Texture2DDescription()
                        {
                            Width = SurfaceWidth,
                            Height = SurfaceHeight,
                            MipLevels = 1,
                            ArraySize = 1,
                            Format = Format.D24_UNorm_S8_UInt,
                            SampleDescription = new SampleDescription(1, 0),
                            Usage = ResourceUsage.Default,
                            BindFlags = BindFlags.DepthStencil,
                            CpuAccessFlags = CpuAccessFlags.None,
                            OptionFlags = ResourceOptionFlags.None
                        };*/

                        // Create the texture for the depth buffer using the filled out description.
                        eyeTexture.DepthBuffer = new Texture2D(Device, eyeTexture.DepthBufferDescription);


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
                                DepthFailOperation = StencilOperation.Decrement,
                                PassOperation = StencilOperation.Keep,
                                Comparison = Comparison.Always
                            }
                        };

                        // Create the depth stencil state.
                        depthStencilState = new DepthStencilState(Device, depthStencilDesc);

                        // Set the depth stencil state.
                        Device.ImmediateContext.OutputMerger.SetDepthStencilState(depthStencilState, 1); //1*/



                        /*// Initialize and set up the depth stencil view.
                        DepthStencilViewDescription depthStencilViewDesc = new DepthStencilViewDescription()
                        {
                            Format = Format.D24_UNorm_S8_UInt,
                            Dimension = DepthStencilViewDimension.Texture2D,
                            Texture2D = new DepthStencilViewDescription.Texture2DResource()
                            {
                                MipSlice = 0
                            }
                        };*/

                        // Create the depth stencil view.
                        //_depthStencilView = new DepthStencilView(Device, eyeTexture.DepthBuffer, depthStencilViewDesc);

                        eyeTexture.DepthStencilView = new DepthStencilView(Device, eyeTexture.DepthBuffer);


                        Device.ImmediateContext.OutputMerger.SetRenderTargets(eyeTexture.DepthStencilView, eyeTexture.RenderTargetViews);








                        //eyeTexture.DepthStencilView = new DepthStencilView(Device, eyeTexture.DepthBuffer, depthStencilViewDesc);

                        //_depthStencilView = new DepthStencilView(Device, DepthStencilBuffer, depthStencilViewDesc);




                        // Specify the texture to show on the HMD.
                        if (eyeIndex == 0)
                        {
                            layerEyeFov.ColorTextureLeft = eyeTexture.SwapTextureSet.TextureSwapChainPtr;
                            layerEyeFov.ViewportLeft.Position = new Vector2i(0, 0);
                            layerEyeFov.ViewportLeft.Size = eyeTexture.TextureSize;
                            layerEyeFov.FovLeft = eyeTexture.FieldOfView;
                        }
                        else
                        {
                            layerEyeFov.ColorTextureRight = eyeTexture.SwapTextureSet.TextureSwapChainPtr;
                            layerEyeFov.ViewportRight.Position = new Vector2i(0, 0);
                            layerEyeFov.ViewportRight.Size = eyeTexture.TextureSize;
                            layerEyeFov.FovRight = eyeTexture.FieldOfView;
                        }
                    }

                    MirrorTextureDesc mirrorTextureDescription = new MirrorTextureDesc();
                    mirrorTextureDescription.Format = TextureFormat.R8G8B8A8_UNorm_SRgb;
                    mirrorTextureDescription.Width = SurfaceWidth;
                    mirrorTextureDescription.Height = SurfaceHeight;
                    mirrorTextureDescription.MiscFlags = TextureMiscFlags.None;

                    // Create the texture used to display the rendered result on the computer monitor.
                    IntPtr mirrorTexturePtr;
                    result = OVR.CreateMirrorTextureDX(sessionPtr, Device.NativePointer, ref mirrorTextureDescription, out mirrorTexturePtr);
                    WriteErrorDetails(OVR, result, "Failed to create mirror texture.");

                    mirrorTexture = new MirrorTexture(OVR, sessionPtr, mirrorTexturePtr);

                    // Retrieve the Direct3D texture contained in the Oculus MirrorTexture.
                    IntPtr mirrorTextureComPtr = IntPtr.Zero;
                    result = mirrorTexture.GetBufferDX(textureInterfaceId, out mirrorTextureComPtr);
                    WriteErrorDetails(OVR, result, "Failed to retrieve the texture from the created mirror texture buffer.");

                    // Create a managed Texture2D, based on the unmanaged texture pointer.
                    mirrorTextureD3D = new Texture2D(mirrorTextureComPtr);







                    /*// Initialize and set up the description of the depth buffer.
                    depthBufferDesc0 = new Texture2DDescription()
                    {
                        Width = SurfaceWidth,
                        Height = SurfaceHeight,
                        MipLevels = 1,
                        ArraySize = 1,
                        Format = Format.D24_UNorm_S8_UInt,
                        SampleDescription = new SampleDescription(1, 0),
                        Usage = ResourceUsage.Default,
                        BindFlags = BindFlags.DepthStencil,
                        CpuAccessFlags = CpuAccessFlags.None,
                        OptionFlags = ResourceOptionFlags.None
                    };

                    // Create the texture for the depth buffer using the filled out description.
                    DepthStencilBuffer = new Texture2D(Device, depthBufferDesc0);


                    // Initialize and set up the description of the stencil state.
                    depthStencilDesc = new DepthStencilStateDescription()
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
                            DepthFailOperation = StencilOperation.Decrement,
                            PassOperation = StencilOperation.Keep,
                            Comparison = Comparison.Always
                        }
                    };

                    // Create the depth stencil state.
                    depthStencilState = new DepthStencilState(Device, depthStencilDesc);

                    mirrorTextureD3D.Device.ImmediateContext.OutputMerger.SetDepthStencilState(depthStencilState, 1);


                    //DeviceContext.Rasterizer.State = RasterState;
                    rasterDesc = new RasterizerStateDescription()
                    {
                        IsAntialiasedLineEnabled = false,
                        CullMode = CullMode.None,
                        DepthBias = 0,
                        DepthBiasClamp = 0.0f,
                        IsDepthClipEnabled = true,
                        FillMode = SharpDX.Direct3D11.FillMode.Solid,
                        IsFrontCounterClockwise = true,
                        IsMultisampleEnabled = false,
                        IsScissorEnabled = false,
                        SlopeScaledDepthBias = 0.0f
                    };

                    // Create the rasterizer state from the description we just filled out.
                    RasterState = new RasterizerState(Device, rasterDesc);

                    // Now set the rasterizer state.
                    //Device.ImmediateContext.Rasterizer.State = RasterState;

                    //Device.ImmediateContext.Rasterizer.SetViewport(viewport);

                    mirrorTextureD3D.Device.ImmediateContext.Rasterizer.State = RasterState;*/





                    /*// Now set the rasterizer state.
                    //DeviceContext.Rasterizer.State = RasterState;
                    rasterDesc = new RasterizerStateDescription()
                    {
                        IsAntialiasedLineEnabled = false,
                        CullMode = CullMode.None,
                        DepthBias = 0,
                        DepthBiasClamp = 0.0f,
                        IsDepthClipEnabled = true,
                        FillMode = SharpDX.Direct3D11.FillMode.Solid,
                        IsFrontCounterClockwise = true,
                        IsMultisampleEnabled = false,
                        IsScissorEnabled = false,
                        SlopeScaledDepthBias = 0.0f
                    };

                    // Create the rasterizer state from the description we just filled out.
                    RasterState = new RasterizerState(Device, rasterDesc);

                    // Now set the rasterizer state.
                    Device.ImmediateContext.Rasterizer.State = RasterState;
                    DeviceContext.Rasterizer.State = RasterState;




                    DeviceContext.Rasterizer.SetViewport(viewport);*/
                }
                catch
                {

                }

                WorldMatrix = SharpDX.Matrix.Identity;

                DeviceContext.ClearState();
                DeviceContext.Flush();
            }



        }




        public void TurnOnAlphaBlending(SharpDX.Direct3D11.DeviceContext someDevicecontext)
        {
            // Setup the blend factor.
            var blendFactor = new Color4(0, 0, 0, 0);

            // Turn on the alpha blending.
            someDevicecontext.OutputMerger.SetBlendState(AlphaEnableBlendingState, blendFactor, -1);
        }
        public void TurnOffAlphaBlending(SharpDX.Direct3D11.DeviceContext someDevicecontext)
        {
            // Setup the blend factor.
            var blendFactor = new Color4(0, 0, 0, 0);

            // Turn on the alpha blending.
            someDevicecontext.OutputMerger.SetBlendState(AlphaDisableBlendingState, blendFactor, -1);
        }
        public void TurnZBufferOn(SharpDX.Direct3D11.DeviceContext someDevicecontext)
        {
            someDevicecontext.OutputMerger.SetDepthStencilState(DepthStencilState, 1);
        }
        public void TurnZBufferOff(SharpDX.Direct3D11.DeviceContext someDevicecontext)
        {
            someDevicecontext.OutputMerger.SetDepthStencilState(DepthDisabledStencilState, 1);
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

                if (adapter != null)
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

                OrthoMatrix = Matrix.Identity;

                if (RasterState != null)
                {
                    RasterState.Dispose();
                    RasterState = null;
                }

                if (DepthStencilState != null)
                {
                    DepthStencilState.Dispose();
                    DepthStencilState = null;
                }


                if (DepthDisabledStencilState != null)
                {
                    DepthDisabledStencilState.Dispose();
                    DepthDisabledStencilState = null;
                }


                if (AlphaEnableBlendingState != null)
                {
                    AlphaEnableBlendingState.Dispose();
                    AlphaEnableBlendingState = null;
                }


                if (AlphaDisableBlendingState != null)
                {
                    AlphaDisableBlendingState.Dispose();
                    AlphaDisableBlendingState = null;
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


        public void WriteErrorDetails(OvrWrap OVR, Ab3d.OculusWrap.Result result, string message)
        {


            if (result >= Ab3d.OculusWrap.Result.Success)
                return;

            ErrorInfo errorInformation = OVR.GetLastErrorInfo();

            string formattedMessage = string.Format("{0}. \nMessage: {1} (Error code={2})", message, errorInformation.ErrorString, errorInformation.Result);

            //Program.MessageBox((IntPtr)0, formattedMessage, "message", 0);


            //Trace.WriteLine(formattedMessage);
            //System.Windows.Forms.MessageBox.Show(formattedMessage, message);

            throw new Exception(formattedMessage);
        }

    }
}
