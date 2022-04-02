using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Runtime.InteropServices;

using DSystemConfiguration = SC_skYaRk_VR_V007.DSystemConfiguration.DSystemConfiguration;

using Ab3d.OculusWrap;
//using Ab3d.DXEngine.OculusWrap;
using Ab3d.OculusWrap.DemoDX11;
using Result = Ab3d.OculusWrap.Result;


using SC_skYaRk_VR_V007.SC_Graphics.SC_ShaderManager;
using SC_skYaRk_VR_V007.SC_Graphics.SC_Grid;



using SharpDX.XAudio2;



namespace SC_skYaRk_VR_V007
{
    public class SC_Console_DIRECTX                  // 269 lines
    {


        public enum BodyTag
        {

            DrawMe,
            DontDrawMe,
            Terrain,
            pseudoCloth,
            physicsInstancedCube,


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
            PlayerRightElbowTarget,

            Screen,

            someothertest,
            testChunkCloth,
            cloth_cube

        }



        public SharpDX.Direct3D11.Device device { get; set; }



        //https://stackoverflow.com/questions/42159066/dotnet-core-app-run-as-administrator
        /*public static void Main(string[] args)
        {
            RequireAdministrator();
        }

        [DllImport("libc")]
        public static extern uint getuid();

        /// <summary>
        /// Asks for administrator privileges upgrade if the platform supports it, otherwise does nothing
        /// </summary>
        public static void RequireAdministrator()
        {
            string name = System.AppDomain.CurrentDomain.FriendlyName;
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
                    {
                        WindowsPrincipal principal = new WindowsPrincipal(identity);
                        if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
                        {
                            throw new InvalidOperationException($"Application must be run as administrator. Right click the {name} file and select 'run as administrator'.");
                        }
                    }
                }
                else if (getuid() != 0)
                {
                    throw new InvalidOperationException($"Application must be run as root/sudo. From terminal, run the executable as 'sudo {name}'");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to determine administrator or root status", ex);
            }
        }*/








        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        //OCULUS RIFT
        public bool _useOculusRift = true;
        public int SurfaceWidth;
        public int SurfaceHeight;
        public DateTime startTime;
        //public static OvrWrap OVR;
        //public static EyeTexture[] eyeTextures;
        //public OculusWrapVirtualRealityProvider _oculusRiftVirtualRealityProvider;
        //public static Ab3d.DirectX.DXDevice _dxDevice;
        //public static HmdDesc hmdDesc;
        //public static IntPtr sessionPtr;
        //public static SharpDX.Direct3D11.DeviceContext DeviceContext => _deviceContext;
        //public static SharpDX.Direct3D11.DeviceContext _deviceContext;
        //public static SharpDX.Direct3D11.Device _device;
        //protected SharpDX.Direct3D11.Device Device => _device;
        //public static SwapChain _swapChain;
        //public static Texture2D _backBuffer;
        private RenderTargetView _renderTargetView;
        //protected SwapChain SwapChain => _swapChain;
        //protected Texture2D BackBuffer => _backBuffer;
        SharpDX.Direct3D11.Texture2D depthBuffer;
        //private static Texture2DDescription _depthBufferDesc;
        //protected static Texture2DDescription DepthBufferDesc => _depthBufferDesc;
        private  DepthStencilView _depthStencilView;
        protected  DepthStencilView DepthStencilView => _depthStencilView;
        SharpDX.Direct3D11.DepthStencilState depthStencilState;
        //public static SharpDX.Direct3D11.Texture2D mirrorTextureD3D = null;
        MirrorTexture mirrorTexture = null;
        //public static LayerEyeFov layerEyeFov;
        //public static Result result;
        Guid textureInterfaceId = new Guid("6f15aaf2-d208-4e89-9ab4-489535d34f9c"); // Interface ID of the Direct3D Texture2D interface.
        //public static SharpDX.Matrix _projectionMatrix;
        //public static SharpDX.Matrix _ProjectionMatrix => _projectionMatrix;





        // Properties.
        public bool VerticalSyncEnabled { get; set; }
        public int VideoCardMemory { get; private set; }
        public string VideoCardDescription { get; private set; }
        public SwapChain SwapChain { get; set; }
        public SharpDX.Direct3D11.Device Device { get; private set; }
        public DeviceContext DeviceContext { get; private set; }
        //private RenderTargetView RenderTargetView { get; set; }
        public Texture2D DepthStencilBuffer { get; set; }
        public DepthStencilState _depthStencilState { get; set; }
        //private DepthStencilView DepthStencilView { get; set; }
        public RasterizerState RasterState { get; set; }
        public Matrix ProjectionMatrix { get; private set; }
        public Matrix WorldMatrix { get; private set; }
        public OvrWrap OVR;// { get; private set; }
        public HmdDesc hmdDesc;// { get; private set; }
        public IntPtr sessionPtr;// { get; private set; }
        public Result result;// { get; private set; }
        public LayerEyeFov layerEyeFov;// { get; set; }
        public EyeTexture[] eyeTextures;// { get; set; }
        public Texture2D BackBuffer;
        public SharpDX.Direct3D11.Texture2D mirrorTextureD3D;



        /*public static DModel _screenModel;
        public static DShaderManager _shaderManager;
        public static SC_SharpDX_ScreenCapture _desktopDupe;
        public static SC_SharpDX_ScreenFrame _desktopFrame;
        public static DTerrain _grid;


        public static DTexture _basicTexture;
        public static DCamera Camera;// { get; set; }*/


        public ControllerType controllerTypeRTouch;
        public ControllerType controllerTypeLTouch;
        public Ab3d.OculusWrap.InputState inputStateLTouch;
        public Ab3d.OculusWrap.InputState inputStateRTouch;




        // Constructor
        public SC_Console_DIRECTX()
        {

        }

        // Methods
        public bool Initialize(SC_skYaRk_VR_V007.DSystemConfiguration.DSystemConfiguration configuration, IntPtr Hwnd, SC_Console_WRITER _writer)
        {
            try
            {
                /*startTime = DateTime.Now;
                //var dpiScale = GetDpiScale();
    
                using (var _factory = new Factory1())
                {
                    var _adapter = _factory.GetAdapter1(0);

                    using (var _output = _adapter.GetOutput(0))
                    {

                        //var ActualWidth = ((SharpDX.Rectangle)_output.Description.DesktopBounds).Width;
                        //var ActualHeight = ((SharpDX.Rectangle)_output.Description.DesktopBounds).Height;

                        SurfaceWidth = ((SharpDX.Rectangle)_output.Description.DesktopBounds).Width;
                        SurfaceHeight = ((SharpDX.Rectangle)_output.Description.DesktopBounds).Height;

                        //SurfaceWidth = (int)(ActualWidth < 0 ? 0 : Math.Ceiling(ActualWidth * dpiScale));
                        //SurfaceHeight = (int)(ActualHeight < 0 ? 0 : Math.Ceiling(ActualHeight * dpiScale));
                    }
                }


                // Store the vsync setting.s
                //VerticalSyncEnabled = SC_skYaRk_VR_V007.DSystemConfiguration.DSystemConfiguration.VerticalSyncEnabled;


                /*swapChainDesc = new SwapChainDescription()
                {
                    OutputHandle = Hwnd,
                    BufferCount = 1,
                    Flags = SwapChainFlags.AllowModeSwitch,
                    IsWindowed = true,
                    ModeDescription = new ModeDescription(configuration.Width, configuration.Height, new Rational(60, 1), Format.B8G8R8A8_UNorm),
                    SampleDescription = new SampleDescription(1, 0),
                    SwapEffect = SwapEffect.Discard,
                    Usage = Usage.RenderTargetOutput | Usage.Shared

                };*/

                /*var swapChainDesc = new SwapChainDescription();
                swapChainDesc.BufferCount = 1;
                swapChainDesc.IsWindowed = true;
                swapChainDesc.OutputHandle = Hwnd; //form.Handle
                swapChainDesc.SampleDescription = new SampleDescription(1, 0);
                swapChainDesc.Usage = Usage.RenderTargetOutput | Usage.ShaderInput; //| Usage.Shared
                swapChainDesc.SwapEffect = SwapEffect.Sequential;
                swapChainDesc.Flags = SwapChainFlags.AllowModeSwitch;
                swapChainDesc.ModeDescription.Width = SurfaceWidth; //form.Width
                swapChainDesc.ModeDescription.Height = SurfaceHeight; //form.Height
                swapChainDesc.ModeDescription.Format = Format.R8G8B8A8_UNorm;
                swapChainDesc.ModeDescription.RefreshRate.Numerator = 0;
                swapChainDesc.ModeDescription.RefreshRate.Denominator = 1;*/

                //SharpDX.Direct3D11.Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, swapChainDesc, out _device, out _swapChain); // none

                /*VerticalSyncEnabled = SC_skYaRk_VR_V007.DSystemConfiguration.DSystemConfiguration.VerticalSyncEnabled;

                // Create a DirectX graphics interface factory.
                var factory = new Factory1();

                // Use the factory to create an adapter for the primary graphics interface (video card).
                var adapter = factory.GetAdapter1(0);

                // Get the primary adapter output (monitor).
                var monitor = adapter.GetOutput(0);

                // Get modes that fit the DXGI_FORMAT_R8G8B8A8_UNORM display format for the adapter output (monitor).
                var modes = monitor.GetDisplayModeList(Format.R8G8B8A8_UNorm, DisplayModeEnumerationFlags.Interlaced);

                // Now go through all the display modes and find the one that matches the screen width and height.
                // When a match is found store the the refresh rate for that monitor, if vertical sync is enabled. 
                // Otherwise we use maximum refresh rate.
                var rational = new Rational(0, 1);
                if (VerticalSyncEnabled)
                {
                    foreach (var mode in modes)
                    {
                        if (mode.Width == configuration.Width && mode.Height == configuration.Height)
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
                factory.Dispose();

                // Initialize the swap chain description.
                var swapChainDesc = new SwapChainDescription()
                {
                    // Set to a single back buffer.
                    BufferCount = 1,
                    // Set the width and height of the back buffer.
                    ModeDescription = new ModeDescription(SurfaceWidth, SurfaceHeight, rational, Format.R8G8B8A8_UNorm),
                    // Set the usage of the back buffer.
                    Usage = Usage.RenderTargetOutput,
                    // Set the handle for the window to render to.
                    OutputHandle = Hwnd,
                    // Turn multisampling off.
                    SampleDescription = new SampleDescription(1, 0),
                    // Set to full screen or windowed mode.
                    IsWindowed = !SC_skYaRk_VR_V007.DSystemConfiguration.DSystemConfiguration.FullScreen,
                    // Don't set the advanced flags.
                    Flags = SwapChainFlags.None,
                    // Discard the back buffer content after presenting.
                    SwapEffect = SwapEffect.Discard
                };

                // Create the swap chain, Direct3D device, and Direct3D device context.
                SharpDX.Direct3D11.Device device;
                SwapChain swapChain;
                SharpDX.Direct3D11.Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, swapChainDesc, out device, out swapChain);

                Device = device;
                SwapChain = swapChain;
                DeviceContext = Device.ImmediateContext;
                //_deviceContext = device.ImmediateContext;


                // Get the pointer to the back buffer.
                BackBuffer = Texture2D.FromSwapChain<Texture2D>(SwapChain, 0);

                // Create the render target view with the back buffer pointer.
                _renderTargetView = new RenderTargetView(device, BackBuffer);

                // Release pointer to the back buffer as we no longer need it.
                BackBuffer.Dispose();

                // Initialize and set up the description of the depth buffer.
                Texture2DDescription depthBufferDesc = new Texture2DDescription()
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
                _depthStencilState = new DepthStencilState(Device, depthStencilDesc);

                // Set the depth stencil state.
                DeviceContext.OutputMerger.SetDepthStencilState(_depthStencilState, 1);

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

                // Bind the render target view and depth stencil buffer to the output render pipeline.
                DeviceContext.OutputMerger.SetTargets(_depthStencilView, _renderTargetView);

                // Setup the raster description which will determine how and what polygon will be drawn.
                RasterizerStateDescription rasterDesc = new RasterizerStateDescription()
                {
                    IsAntialiasedLineEnabled = false,
                    CullMode = CullMode.Back,
                    DepthBias = 0,
                    DepthBiasClamp = .0f,
                    IsDepthClipEnabled = true,
                    FillMode = FillMode.Solid,
                    IsFrontCounterClockwise = false,
                    IsMultisampleEnabled = true,
                    IsScissorEnabled = false,
                    SlopeScaledDepthBias = .0f
                };

                // Create the rasterizer state from the description we just filled out.
                RasterState = new RasterizerState(Device, rasterDesc);

                // Now set the rasterizer state.
                DeviceContext.Rasterizer.State = RasterState;

                // Setup and create the viewport for rendering.
                DeviceContext.Rasterizer.SetViewport(0, 0, SurfaceWidth, SurfaceHeight, 0, 1);

                // Setup and create the projection matrix.
                //ProjectionMatrix = Matrix.PerspectiveFovLH((float)(Math.PI / 4), SurfaceWidth/SurfaceHeight, SC_skYaRk_VR_V007.DSystemConfiguration.DSystemConfiguration.ScreenNear, SC_skYaRk_VR_V007.DSystemConfiguration.DSystemConfiguration.ScreenDepth);
                */
                // Initialize the world matrix to the identity matrix.
                /*WorldMatrix = Matrix.Identity;















                if (_useOculusRift)
                {

                    controllerTypeRTouch = ControllerType.RTouch;
                    controllerTypeLTouch = ControllerType.LTouch;

                    //---------------------------------------------------------\\ OCULUS WRAP

                    OVR = OvrWrap.Create(); //Ab3d.DXEngine.OculusWrap    

                    // Check if OVR service is running
                    var detectResult = OVR.Detect(0);

                    if (!detectResult.IsOculusServiceRunning)
                    {
                        MessageBox((IntPtr)0, "Oculus service is not running", "Oculus Error", 0);
                    }
                    else
                    {
                        //MessageBox((IntPtr)0, "Oculus service is running", "Oculus Message", 0);
                    }
                    // Check if Head Mounter Display is connected
                    if (!detectResult.IsOculusHMDConnected)
                    {
                        MessageBox((IntPtr)0, "Oculus HMD (Head Mounter Display) is not connected", "Oculus Error", 0);
                    }
                    else
                    {
                        //MessageBox((IntPtr)0, "Oculus HMD (Head Mounter Display) is connected", "Oculus Message", 0);
                    }
    
                    try
                    {
                        //------------------------------FOR AB3D DX ENGINE Device.
                        _oculusRiftVirtualRealityProvider = new OculusWrapVirtualRealityProvider(OVR, multisamplingCount: 4);
                        //hmdDesc = _oculusRiftVirtualRealityProvider.HmdDescription;
                        try
                        {
                            // Then we initialize Oculus OVR and create a new DXDevice that uses the same adapter (graphic card) as Oculus Rift
                            _dxDevice = _oculusRiftVirtualRealityProvider.InitializeOvrAndDXDevice(requestedOculusSdkMinorVersion: 17);
                        }
                        catch (Exception ex)
                        {
                            //System.Windows.MessageBox.Show("Failed to initialize the Oculus runtime library.\r\nError: " + ex.Message, "Oculus error", MessageBoxButton.OK, MessageBoxImage.Error);
                            //return;
                            //MessageBox((IntPtr)0, "Failed to initialize the Oculus runtime library.\r\nError: " + ex.Message, "Oculus error", 0);
                        }
                        OVR.RecenterTrackingOrigin(_oculusRiftVirtualRealityProvider.SessionPtr);

                        sessionPtr = _oculusRiftVirtualRealityProvider.SessionPtr;
                        hmdDesc = OVR.GetHmdDesc(sessionPtr);
                        //----------------------FOR AB3D DX ENGINE Device.


                        /*SharpDX.Direct3D11.Device _device;
                        SharpDX.DXGI.SwapChain _swapChain;
                        SwapChainDescription swapChainDescription = new SwapChainDescription();
                        swapChainDescription.BufferCount = 1;
                        swapChainDescription.IsWindowed = true;
                        swapChainDescription.OutputHandle = Hwnd; //form.Handle
                        swapChainDescription.SampleDescription = new SampleDescription(1, 0);
                        swapChainDescription.Usage = Usage.RenderTargetOutput | Usage.ShaderInput;
                        swapChainDescription.SwapEffect = SwapEffect.Sequential;
                        swapChainDescription.Flags = SwapChainFlags.AllowModeSwitch;
                        swapChainDescription.ModeDescription.Width = SurfaceWidth; //form.Width
                        swapChainDescription.ModeDescription.Height = SurfaceHeight; //form.Height
                        swapChainDescription.ModeDescription.Format = Format.R8G8B8A8_UNorm;
                        swapChainDescription.ModeDescription.RefreshRate.Numerator = 0;
                        swapChainDescription.ModeDescription.RefreshRate.Denominator = 1;
                        SharpDX.Direct3D11.Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, swapChainDescription, out _device, out _swapChain);
                        


                        Device = _dxDevice.Device;
                        device = Device;

                        //Device = _device;
                        DeviceContext = Device.ImmediateContext;

                        //Device = _device;
                        //Device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None);


                        //SwapChain = _swapChain;
                        // Create DirectX drawing device.
                        // Create DirectX Graphics Interface factory, used to create the swap chain.
                        //using (var factory = new SharpDX.DXGI.Factory1())
                        {
                            var factoryer = new SharpDX.DXGI.Factory1();
                            //factory.MakeWindowAssociation(Hwnd, WindowAssociationFlags.IgnoreAll);

                            //factory = new SharpDX.DXGI.Factory4();

                            //_device.ImmediateContext.Rasterizer.State = rasterStateSolid;

                            // Define the properties of the swap chain.
                            SwapChainDescription swapChainDescription = new SwapChainDescription();
                            swapChainDescription.BufferCount = 1;
                            swapChainDescription.IsWindowed = true;
                            swapChainDescription.OutputHandle = Hwnd; //form.Handle
                            swapChainDescription.SampleDescription = new SampleDescription(1, 0);
                            swapChainDescription.Usage = Usage.RenderTargetOutput | Usage.ShaderInput;
                            swapChainDescription.SwapEffect = SwapEffect.Sequential;
                            swapChainDescription.Flags = SwapChainFlags.AllowModeSwitch;
                            swapChainDescription.ModeDescription.Width = SurfaceWidth; //form.Width
                            swapChainDescription.ModeDescription.Height = SurfaceHeight; //form.Height
                            swapChainDescription.ModeDescription.Format = Format.R8G8B8A8_UNorm;
                            swapChainDescription.ModeDescription.RefreshRate.Numerator = 0;
                            swapChainDescription.ModeDescription.RefreshRate.Denominator = 1;
                            // Create the swap chain.
                            SwapChain = new SwapChain(factoryer, Device, swapChainDescription);
                            factoryer.Dispose();


                            // Retrieve the back buffer of the swap chain.
                            BackBuffer = SwapChain.GetBackBuffer<SharpDX.Direct3D11.Texture2D>(0);
                            _renderTargetView = new RenderTargetView(Device, BackBuffer);

                            // Create a depth buffer, using the same width and height as the back buffer.
                            Texture2DDescription depthBufferDescription = new Texture2DDescription();
                            depthBufferDescription.Format = Format.D32_Float;
                            depthBufferDescription.ArraySize = 1;
                            depthBufferDescription.MipLevels = 1;
                            depthBufferDescription.Width = SurfaceWidth;//form.Width
                            depthBufferDescription.Height = SurfaceHeight;//form.Height
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



                            // Initialize and set up the description of the stencil state.
                            //DepthStencilStateDescription depthStencilStateDescription = new DepthStencilStateDescription()
                            //{
                            //    IsDepthEnabled = true,
                            //    DepthWriteMask = DepthWriteMask.All,
                            //    DepthComparison = Comparison.Less,
                            //    IsStencilEnabled = true,
                            //    StencilReadMask = 0xFF,
                            //    StencilWriteMask = 0xFF,
                                // Stencil operation if pixel front-facing.
                            //    FrontFace = new DepthStencilOperationDescription()
                            //    {
                            //        FailOperation = StencilOperation.Keep,
                            //        DepthFailOperation = StencilOperation.Increment,
                            //        PassOperation = StencilOperation.Keep,
                            //        Comparison = Comparison.Always
                            //    },
                                // Stencil operation if pixel is back-facing.
                            //    BackFace = new DepthStencilOperationDescription()
                            //    {
                            //        FailOperation = StencilOperation.Keep,
                            //        DepthFailOperation = StencilOperation.Decrement,
                            //        PassOperation = StencilOperation.Keep,
                            //        Comparison = Comparison.Always
                            //    }
                            //};











                            depthBuffer = new SharpDX.Direct3D11.Texture2D(Device, depthBufferDescription);
                            _depthStencilView = new DepthStencilView(Device, depthBuffer);
                            depthStencilState = new SharpDX.Direct3D11.DepthStencilState(Device, depthStencilStateDescription);
                            var viewport = new SharpDX.Viewport(0, 0, hmdDesc.Resolution.Width, hmdDesc.Resolution.Height, 0.0f, 1.0f);


                            DeviceContext.OutputMerger.SetDepthStencilState(depthStencilState);
                            DeviceContext.OutputMerger.SetRenderTargets(_depthStencilView, _renderTargetView);



                            //DeviceContext.OutputMerger.SetTargets(_depthStencilView, _renderTargetView);

                            // Setup the raster description which will determine how and what polygon will be drawn.
                            RasterizerStateDescription rasterDesc = new RasterizerStateDescription()
                            {
                                IsAntialiasedLineEnabled = false,
                                CullMode = CullMode.Back,
                                DepthBias = 0,
                                DepthBiasClamp = .0f,
                                IsDepthClipEnabled = true,
                                FillMode = FillMode.Solid,
                                IsFrontCounterClockwise = false,
                                IsMultisampleEnabled = true,
                                IsScissorEnabled = false,
                                SlopeScaledDepthBias = .0f
                            };

                            // Create the rasterizer state from the description we just filled out.
                            RasterState = new RasterizerState(Device, rasterDesc);

                            // Now set the rasterizer state.
                            DeviceContext.Rasterizer.State = RasterState;

                            // Setup and create the viewport for rendering.
                            //DeviceContext.Rasterizer.SetViewport(0, 0, SurfaceWidth, SurfaceHeight, 0, 1);






















                            /*var rasterDesc = new RasterizerStateDescription()
                            {
                                IsAntialiasedLineEnabled = false,
                                CullMode = CullMode.Back,
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
                            RasterState = new RasterizerState(_device, rasterDesc);

                            // Now set the rasterizer state.
                            _device.ImmediateContext.Rasterizer.State = RasterState;
                            

                            //DeviceContext.Rasterizer.State.Description.CullMode = CullMode.Back;



                            DeviceContext.Rasterizer.SetViewport(viewport);

                            //var test = _device.ImmediateContext.Rasterizer.State.Description.CullMode;                  

                            // Retrieve the DXGI device, in order to set the maximum frame latency.
                            using (SharpDX.DXGI.Device1 dxgiDevice = Device.QueryInterface<SharpDX.DXGI.Device1>())
                            {
                                dxgiDevice.MaximumFrameLatency = 1;
                            }

                            layerEyeFov = new LayerEyeFov();
                            layerEyeFov.Header.Type = LayerType.EyeFov;
                            layerEyeFov.Header.Flags = LayerFlags.None;

                            // Create a set of layers to submit.
                            eyeTextures = new EyeTexture[2];

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
                                eyeTexture.Viewport = new SharpDX.Viewport(0, 0, eyeTexture.TextureSize.Width, eyeTexture.TextureSize.Height, 0.0f, 1.0f);

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
                                eyeTexture.Textures = new SharpDX.Direct3D11.Texture2D[textureSwapChainBufferCount];
                                eyeTexture.RenderTargetViews = new RenderTargetView[textureSwapChainBufferCount];

                                // Create a texture 2D and a render target view, for each unmanaged texture contained in the SwapTextureSet.
                                for (int textureIndex = 0; textureIndex < textureSwapChainBufferCount; textureIndex++)
                                {
                                    // Retrieve the Direct3D texture contained in the Oculus TextureSwapChainBuffer.
                                    IntPtr swapChainTextureComPtr = IntPtr.Zero;
                                    result = eyeTexture.SwapTextureSet.GetBufferDX(textureIndex, textureInterfaceId, out swapChainTextureComPtr);
                                    WriteErrorDetails(OVR, result, "Failed to retrieve a texture from the created swap chain.");

                                    // Create a managed Texture2D, based on the unmanaged texture pointer.
                                    eyeTexture.Textures[textureIndex] = new SharpDX.Direct3D11.Texture2D(swapChainTextureComPtr);

                                    // Create a render target view for the current Texture2D.
                                    eyeTexture.RenderTargetViews[textureIndex] = new RenderTargetView(Device, eyeTexture.Textures[textureIndex]);
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
                                eyeTexture.DepthBuffer = new SharpDX.Direct3D11.Texture2D(Device, eyeTexture.DepthBufferDescription);
                                eyeTexture.DepthStencilView = new DepthStencilView(Device, eyeTexture.DepthBuffer);

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
                            mirrorTextureDescription.Width = SurfaceWidth; //form.Width
                            mirrorTextureDescription.Height = SurfaceHeight;//form.Height
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
                            mirrorTextureD3D = new SharpDX.Direct3D11.Texture2D(mirrorTextureComPtr);


                            /*#region Vertex and pixel shader
                            // Create a constant buffer, to contain our WorldViewProjection matrix, that will be passed to the vertex shader.
                            var contantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<SharpDX.Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);

                            // Setup the immediate context to use the shaders and model we defined.
                            _deviceContext.InputAssembler.InputLayout = inputLayout;
                            _deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;

                            DeviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexBuffer, sizeof(float) * 4 * 2, 0));

                            _deviceContext.VertexShader.SetConstantBuffer(0, contantBuffer);

                            _deviceContext.VertexShader.Set(vertexShader);
                            _deviceContext.PixelShader.Set(pixelShader);
                            #endregion*/

                //startTime = DateTime.Now;


                /*var rasterDesc = new RasterizerStateDescription()
                {
                    IsAntialiasedLineEnabled = true,
                    CullMode = CullMode.Back,
                    DepthBias = 0,
                    DepthBiasClamp = 0.0f,
                    IsDepthClipEnabled = true,
                    FillMode = SharpDX.Direct3D11.FillMode.Solid,
                    IsFrontCounterClockwise = true,
                    IsMultisampleEnabled = true,
                    IsScissorEnabled = true,
                    SlopeScaledDepthBias = 0.0f
                };

                // Create the rasterizer state from the description we just filled out.
                RasterState = new RasterizerState(_device, rasterDesc);

                // Now set the rasterizer state.
                _device.ImmediateContext.Rasterizer.State = RasterState;
            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex.ToString());
        }
        DeviceContext.ClearState();
        DeviceContext.Flush();
    }*/













                /*// Store the vsync setting.
                VerticalSyncEnabled = SC_skYaRk_VR_V007.DSystemConfiguration.DSystemConfiguration.VerticalSyncEnabled;

                // Create a DirectX graphics interface factory.
                var factory = new Factory1();

                // Use the factory to create an adapter for the primary graphics interface (video card).
                var adapter = factory.GetAdapter1(0);

                // Get the primary adapter output (monitor).
                var monitor = adapter.GetOutput(0);

                // Get modes that fit the DXGI_FORMAT_R8G8B8A8_UNORM display format for the adapter output (monitor).
                var modes = monitor.GetDisplayModeList(Format.R8G8B8A8_UNorm, DisplayModeEnumerationFlags.Interlaced);

                // Now go through all the display modes and find the one that matches the screen width and height.
                // When a match is found store the the refresh rate for that monitor, if vertical sync is enabled. 
                // Otherwise we use maximum refresh rate.
                var rational = new Rational(0, 1);
                if (VerticalSyncEnabled)
                {
                    foreach (var mode in modes)
                    {
                        if (mode.Width == configuration.Width && mode.Height == configuration.Height)
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
                factory.Dispose();


                // Initialize the swap chain description.
                var swapChainDesc = new SwapChainDescription()
                {
                    // Set to a single back buffer.
                    BufferCount = 1,
                    // Set the width and height of the back buffer.
                    ModeDescription = new ModeDescription(configuration.Width, configuration.Height, rational, Format.R8G8B8A8_UNorm),
                    // Set the usage of the back buffer.
                    Usage = Usage.RenderTargetOutput,
                    // Set the handle for the window to render to.
                    OutputHandle = Hwnd,
                    // Turn multisampling off.
                    SampleDescription = new SampleDescription(1, 0),
                    // Set to full screen or windowed mode.
                    IsWindowed = !SC_skYaRk_VR_V007.DSystemConfiguration.DSystemConfiguration.FullScreen,
                    // Don't set the advanced flags.
                    Flags = SwapChainFlags.None,
                    // Discard the back buffer content after presenting.
                    SwapEffect = SwapEffect.Discard
                };



                // Create the swap chain, Direct3D device, and Direct3D device context.
                SharpDX.Direct3D11.Device device;
                SwapChain swapChain;
                SharpDX.Direct3D11.Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, swapChainDesc, out device, out swapChain);

                Device = device;
                SwapChain = swapChain;
                DeviceContext = Device.ImmediateContext;
                //_deviceContext = device.ImmediateContext;


                // Get the pointer to the back buffer.
                _backBuffer = Texture2D.FromSwapChain<Texture2D>(SwapChain, 0);

                // Create the render target view with the back buffer pointer.
                _renderTargetView = new RenderTargetView(device, _backBuffer);

                // Release pointer to the back buffer as we no longer need it.
                _backBuffer.Dispose();

                // Initialize and set up the description of the depth buffer.
                Texture2DDescription depthBufferDesc = new Texture2DDescription()
                {
                    Width = configuration.Width,
                    Height = configuration.Height,
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
                _depthStencilState = new DepthStencilState(Device, depthStencilDesc);

                // Set the depth stencil state.
                DeviceContext.OutputMerger.SetDepthStencilState(_depthStencilState, 1);

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

                // Bind the render target view and depth stencil buffer to the output render pipeline.
                DeviceContext.OutputMerger.SetTargets(_depthStencilView, _renderTargetView);

                // Setup the raster description which will determine how and what polygon will be drawn.
                RasterizerStateDescription rasterDesc = new RasterizerStateDescription()
                {
                    IsAntialiasedLineEnabled = false,
                    CullMode = CullMode.Back,
                    DepthBias = 0,
                    DepthBiasClamp = .0f,
                    IsDepthClipEnabled = true,
                    FillMode = FillMode.Solid,
                    IsFrontCounterClockwise = false,
                    IsMultisampleEnabled = false,
                    IsScissorEnabled = false,
                    SlopeScaledDepthBias = .0f
                };

                // Create the rasterizer state from the description we just filled out.
                RasterState = new RasterizerState(Device, rasterDesc);

                // Now set the rasterizer state.
                DeviceContext.Rasterizer.State = RasterState;

                // Setup and create the viewport for rendering.
                DeviceContext.Rasterizer.SetViewport(0, 0, configuration.Width, configuration.Height, 0, 1);

                // Setup and create the projection matrix.
                ProjectionMatrix = Matrix.PerspectiveFovLH((float)(Math.PI / 4), ((float)configuration.Width / (float)configuration.Height), SC_skYaRk_VR_V007.DSystemConfiguration.DSystemConfiguration.ScreenNear, SC_skYaRk_VR_V007.DSystemConfiguration.DSystemConfiguration.ScreenDepth);

                // Initialize the world matrix to the identity matrix.
                WorldMatrix = Matrix.Identity;
                */








                //Camera = new DCamera();
                //Camera.Render();
                //Camera.SetPosition(0, 0, 0);

                //originalCameraPos = Camera.GetPosition();



                /*_shaderManager = new DShaderManager();
                _shaderManager.Initialize(Device, Hwnd);

                //_desktopDupe = new SC_SharpDX_ScreenCapture(0, 0);

                _grid = new DTerrain();
                _grid.Initialize(Device, 20, 20, 1);

                _screenModel = new DModel();
                bool _hasinit0 = _screenModel.Initialize(Device); //, "terrainGrassDirt.bmp"


                //MessageBox((IntPtr)0, _hasinit0 + "", "Oculus error", 0);

                _basicTexture = new DTexture();
                bool _hasinit1 = _basicTexture.Initialize(Device, "../../../terrainGrassDirt.bmp");*/

                //MessageBox((IntPtr)0, _hasinit1 + "", "Oculus error", 0);



                startTime = DateTime.Now;
                //var dpiScale = GetDpiScale();

                using (var _factory = new Factory1())
                {
                    var _adapter = _factory.GetAdapter1(0);

                    using (var _output = _adapter.GetOutput(0))
                    {
                        SurfaceWidth = ((SharpDX.Rectangle)_output.Description.DesktopBounds).Width;
                        SurfaceHeight = ((SharpDX.Rectangle)_output.Description.DesktopBounds).Height;
                    }
                }

                WorldMatrix = Matrix.Identity;
                if (_useOculusRift)
                {



                    controllerTypeRTouch = ControllerType.RTouch;
                    controllerTypeLTouch = ControllerType.LTouch;

                    //---------------------------------------------------------\\ OCULUS WRAP

                    OVR = OvrWrap.Create(); //Ab3d.DXEngine.OculusWrap    

                    // Check if OVR service is running
                    var detectResult = OVR.Detect(0);

                    if (!detectResult.IsOculusServiceRunning)
                    {
                        MessageBox((IntPtr)0, "Oculus service is not running", "Oculus Error", 0);
                    }
                    else
                    {
                        //MessageBox((IntPtr)0, "Oculus service is running", "Oculus Message", 0);
                    }
                    // Check if Head Mounter Display is connected
                    if (!detectResult.IsOculusHMDConnected)
                    {
                        MessageBox((IntPtr)0, "Oculus HMD (Head Mounter Display) is not connected", "Oculus Error", 0);
                    }
                    else
                    {
                        //MessageBox((IntPtr)0, "Oculus HMD (Head Mounter Display) is connected", "Oculus Message", 0);
                    }


                    try
                    {
                        //gotta find a way to load the oculus rift service manually maybe.

                        //------------------------------FOR AB3D DX ENGINE Device.
                        /*_oculusRiftVirtualRealityProvider = new OculusWrapVirtualRealityProvider(OVR, multisamplingCount: 8);
                        //hmdDesc = _oculusRiftVirtualRealityProvider.HmdDescription;

                        try
                        {
                            // Then we initialize Oculus OVR and create a new DXDevice that uses the same adapter (graphic card) as Oculus Rift
                            _dxDevice = _oculusRiftVirtualRealityProvider.InitializeOvrAndDXDevice(requestedOculusSdkMinorVersion: 17);
                        }
                        catch (Exception ex)
                        {
                            //System.Windows.MessageBox.Show("Failed to initialize the Oculus runtime library.\r\nError: " + ex.Message, "Oculus error", MessageBoxButton.OK, MessageBoxImage.Error);
                            //return;
                            //MessageBox((IntPtr)0, "Failed to initialize the Oculus runtime library.\r\nError: " + ex.Message, "Oculus error", 0);
                        }


                        OVR.RecenterTrackingOrigin(_oculusRiftVirtualRealityProvider.SessionPtr);

                        sessionPtr = _oculusRiftVirtualRealityProvider.SessionPtr;
                        hmdDesc = OVR.GetHmdDesc(sessionPtr);
                        //----------------------FOR AB3D DX ENGINE Device.

                        Device = _dxDevice.Device;*/







                        // Define initialization parameters with debug flag.
                        InitParams initializationParameters = new InitParams();
                        initializationParameters.Flags = InitFlags.Debug | InitFlags.RequestVersion;
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
                            Program.MessageBox((IntPtr)0, "Failed to initialize the Oculus runtime library:\r\n" + errorReason, "sccoresystems", 0);//.Show("Failed to initialize the Oculus runtime library:\r\n" + errorReason, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //return;
                        }

                        // Use the head mounted display.
                        sessionPtr = IntPtr.Zero;


                        var graphicsLuid = new GraphicsLuid();
                        result = OVR.Create(ref sessionPtr, ref graphicsLuid);
                        if (result < Result.Success)
                        {
                            Program.MessageBox((IntPtr)0, "The HMD is not enabled: " + result.ToString(), "sccoresystems", 0);
                            //return;
                        }

                        hmdDesc = OVR.GetHmdDesc(sessionPtr);



                        /*var swapChainDesc = new SwapChainDescription();
                        swapChainDesc.BufferCount = 1;
                        swapChainDesc.IsWindowed = true;
                        swapChainDesc.OutputHandle = sccsr11wpf.MainWindow.consoleHandle; //form.Handle
                        swapChainDesc.SampleDescription = new SampleDescription(1, 0);
                        swapChainDesc.Usage = Usage.RenderTargetOutput | Usage.ShaderInput;
                        swapChainDesc.SwapEffect = SwapEffect.Sequential;
                        swapChainDesc.Flags = SwapChainFlags.AllowModeSwitch;
                        swapChainDesc.ModeDescription.Width = SurfaceWidth; //form.Width
                        swapChainDesc.ModeDescription.Height = SurfaceHeight; //form.Height
                        swapChainDesc.ModeDescription.Format = Format.R8G8B8A8_UNorm;
                        swapChainDesc.ModeDescription.RefreshRate.Numerator = 0;
                        swapChainDesc.ModeDescription.RefreshRate.Denominator = 1;


                        SharpDX.Direct3D11.Device device;
                        SharpDX.DXGI.SwapChain swapChain;

                        SharpDX.Direct3D11.Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None, swapChainDesc, out device, out swapChain); // none
                        */
                        //_deviceContext = _device.ImmediateContext;




                        device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None);

                        // Create DirectX Graphics Interface factory, used to create the swap chain.
                        SharpDX.DXGI.Factory4 factory = new SharpDX.DXGI.Factory4();

                        //immediateContext = device.ImmediateContext;

                        // Define the properties of the swap chain.
                        /*SwapChainDescription swapChainDescript = new SwapChainDescription();
                        swapChainDescript.BufferCount = 1;
                        swapChainDescript.IsWindowed = true;
                        swapChainDescript.OutputHandle = sccsr11wpf.MainWindow.consoleHandle;
                        swapChainDescript.SampleDescription = new SampleDescription(1, 0);
                        swapChainDescript.Usage = Usage.RenderTargetOutput | Usage.ShaderInput;
                        swapChainDescript.SwapEffect = SwapEffect.Sequential;
                        swapChainDescript.Flags = SwapChainFlags.AllowModeSwitch;
                        swapChainDescript.ModeDescription.Width = SurfaceWidth;
                        swapChainDescript.ModeDescription.Height = SurfaceHeight;
                        swapChainDescript.ModeDescription.Format = Format.R8G8B8A8_UNorm;
                        swapChainDescript.ModeDescription.RefreshRate.Numerator = 0;
                        swapChainDescript.ModeDescription.RefreshRate.Denominator = 1;

                        SwapChain swapChain;
                        // Create the swap chain.
                        swapChain = new SwapChain(factory, device, swapChainDescript);*/






                        //device = Device;
                        Device = device;
                        DeviceContext = Device.ImmediateContext;

                        if (Device == null)
                        {
                            MessageBox((IntPtr)0, "null", "SCCoreSystems Error", 0);
                        }




                        // Create DirectX drawing device.
                        // Create DirectX Graphics Interface factory, used to create the swap chain.
                        using (var factoryer = new SharpDX.DXGI.Factory1())
                        {
                            //factory.MakeWindowAssociation(Hwnd, WindowAssociationFlags.IgnoreAll);

                            // Define the properties of the swap chain.
                            SwapChainDescription swapChainDescription = new SwapChainDescription();
                            swapChainDescription.BufferCount = 1;
                            swapChainDescription.IsWindowed = true;
                            swapChainDescription.OutputHandle = Hwnd;
                            swapChainDescription.SampleDescription = new SampleDescription(1, 0);
                            swapChainDescription.Usage = Usage.RenderTargetOutput | Usage.ShaderInput;
                            swapChainDescription.SwapEffect = SwapEffect.Sequential;
                            swapChainDescription.Flags = SwapChainFlags.AllowModeSwitch;
                            swapChainDescription.ModeDescription.Width = SurfaceWidth;
                            swapChainDescription.ModeDescription.Height = SurfaceHeight;
                            swapChainDescription.ModeDescription.Format = Format.R8G8B8A8_UNorm;
                            swapChainDescription.ModeDescription.RefreshRate.Numerator = 0;
                            swapChainDescription.ModeDescription.RefreshRate.Denominator = 1;
                            // Create the swap chain.
                            SwapChain = new SwapChain(factoryer, Device, swapChainDescription);
                            factoryer.Dispose();


                            // Retrieve the back buffer of the swap chain.
                            BackBuffer = SwapChain.GetBackBuffer<SharpDX.Direct3D11.Texture2D>(0);
                            _renderTargetView = new RenderTargetView(Device, BackBuffer);

                            // Create a depth buffer, using the same width and height as the back buffer.
                            Texture2DDescription depthBufferDescription = new Texture2DDescription();
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

                            depthBuffer = new SharpDX.Direct3D11.Texture2D(Device, depthBufferDescription);
                            _depthStencilView = new DepthStencilView(Device, depthBuffer);
                            depthStencilState = new SharpDX.Direct3D11.DepthStencilState(Device, depthStencilStateDescription);
                            var viewport = new SharpDX.Viewport(0, 0, hmdDesc.Resolution.Width, hmdDesc.Resolution.Height, 0.0f, 1.0f);

                            DeviceContext.OutputMerger.SetDepthStencilState(depthStencilState);
                            DeviceContext.OutputMerger.SetRenderTargets(_depthStencilView, _renderTargetView);

                            // Setup the raster description which will determine how and what polygon will be drawn.
                            RasterizerStateDescription rasterDesc = new RasterizerStateDescription()
                            {
                                IsAntialiasedLineEnabled = false,
                                CullMode = CullMode.Front,
                                DepthBias = 0,
                                DepthBiasClamp = .0f,
                                IsDepthClipEnabled = true,
                                FillMode = FillMode.Solid,
                                IsFrontCounterClockwise = false,
                                IsMultisampleEnabled = true,
                                IsScissorEnabled = false,
                                SlopeScaledDepthBias = .0f
                            };

                            // Create the rasterizer state from the description we just filled out.
                            RasterState = new RasterizerState(Device, rasterDesc);

                            // Now set the rasterizer state.
                            DeviceContext.Rasterizer.State = RasterState;

                            /*var rasterDesc = new RasterizerStateDescription()
                            {
                                IsAntialiasedLineEnabled = false,
                                CullMode = CullMode.Back,
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
                            RasterState = new RasterizerState(_device, rasterDesc);

                            // Now set the rasterizer state.
                            _device.ImmediateContext.Rasterizer.State = RasterState;*/


                            DeviceContext.Rasterizer.SetViewport(viewport);

                            // Retrieve the DXGI device, in order to set the maximum frame latency.
                            using (SharpDX.DXGI.Device1 dxgiDevice = Device.QueryInterface<SharpDX.DXGI.Device1>())
                            {
                                dxgiDevice.MaximumFrameLatency = 1;
                            }

                            layerEyeFov = new LayerEyeFov();
                            layerEyeFov.Header.Type = LayerType.EyeFov;
                            layerEyeFov.Header.Flags = LayerFlags.None;

                            // Create a set of layers to submit.
                            eyeTextures = new EyeTexture[2];

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
                                eyeTexture.Viewport = new SharpDX.Viewport(0, 0, eyeTexture.TextureSize.Width, eyeTexture.TextureSize.Height, 0.0f, 1.0f);

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
                                eyeTexture.Textures = new SharpDX.Direct3D11.Texture2D[textureSwapChainBufferCount];
                                eyeTexture.RenderTargetViews = new RenderTargetView[textureSwapChainBufferCount];

                                // Create a texture 2D and a render target view, for each unmanaged texture contained in the SwapTextureSet.
                                for (int textureIndex = 0; textureIndex < textureSwapChainBufferCount; textureIndex++)
                                {
                                    // Retrieve the Direct3D texture contained in the Oculus TextureSwapChainBuffer.
                                    IntPtr swapChainTextureComPtr = IntPtr.Zero;
                                    result = eyeTexture.SwapTextureSet.GetBufferDX(textureIndex, textureInterfaceId, out swapChainTextureComPtr);
                                    WriteErrorDetails(OVR, result, "Failed to retrieve a texture from the created swap chain.");

                                    // Create a managed Texture2D, based on the unmanaged texture pointer.
                                    eyeTexture.Textures[textureIndex] = new SharpDX.Direct3D11.Texture2D(swapChainTextureComPtr);

                                    // Create a render target view for the current Texture2D.
                                    eyeTexture.RenderTargetViews[textureIndex] = new RenderTargetView(Device, eyeTexture.Textures[textureIndex]);
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
                                eyeTexture.DepthBuffer = new SharpDX.Direct3D11.Texture2D(Device, eyeTexture.DepthBufferDescription);
                                eyeTexture.DepthStencilView = new DepthStencilView(Device, eyeTexture.DepthBuffer);

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
                            mirrorTextureD3D = new SharpDX.Direct3D11.Texture2D(mirrorTextureComPtr);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    DeviceContext.ClearState();
                    DeviceContext.Flush();



                    /*
                    controllerTypeRTouch = ControllerType.RTouch;
                    controllerTypeLTouch = ControllerType.LTouch;

                    //---------------------------------------------------------\\ OCULUS WRAP

                    OVR = OvrWrap.Create(); //Ab3d.DXEngine.OculusWrap    

                    // Check if OVR service is running
                    var detectResult = OVR.Detect(0);

                    if (!detectResult.IsOculusServiceRunning)
                    {
                        MessageBox((IntPtr)0, "Oculus service is not running", "Oculus Error", 0);
                    }
                    else
                    {
                        //MessageBox((IntPtr)0, "Oculus service is running", "Oculus Message", 0);
                    }
                    // Check if Head Mounter Display is connected
                    if (!detectResult.IsOculusHMDConnected)
                    {
                        MessageBox((IntPtr)0, "Oculus HMD (Head Mounter Display) is not connected", "Oculus Error", 0);
                    }
                    else
                    {
                        //MessageBox((IntPtr)0, "Oculus HMD (Head Mounter Display) is connected", "Oculus Message", 0);
                    }


                    try
                    {
                        //gotta find a way to load the oculus rift service manually maybe.

                        //------------------------------FOR AB3D DX ENGINE Device.
                        _oculusRiftVirtualRealityProvider = new OculusWrapVirtualRealityProvider(OVR, multisamplingCount: 8);
                        //hmdDesc = _oculusRiftVirtualRealityProvider.HmdDescription;
                        try
                        {
                            // Then we initialize Oculus OVR and create a new DXDevice that uses the same adapter (graphic card) as Oculus Rift
                            _dxDevice = _oculusRiftVirtualRealityProvider.InitializeOvrAndDXDevice(requestedOculusSdkMinorVersion: 17);
                        }
                        catch (Exception ex)
                        {
                            //System.Windows.MessageBox.Show("Failed to initialize the Oculus runtime library.\r\nError: " + ex.Message, "Oculus error", MessageBoxButton.OK, MessageBoxImage.Error);
                            //return;
                            //MessageBox((IntPtr)0, "Failed to initialize the Oculus runtime library.\r\nError: " + ex.Message, "Oculus error", 0);
                        }
                        OVR.RecenterTrackingOrigin(_oculusRiftVirtualRealityProvider.SessionPtr);

                        sessionPtr = _oculusRiftVirtualRealityProvider.SessionPtr;
                        hmdDesc = OVR.GetHmdDesc(sessionPtr);
                        //----------------------FOR AB3D DX ENGINE Device.

                        Device = _dxDevice.Device;
                        device = Device;
                        DeviceContext = Device.ImmediateContext;

                        // Create DirectX drawing device.
                        // Create DirectX Graphics Interface factory, used to create the swap chain.
                        using (var factoryer = new SharpDX.DXGI.Factory1())
                        {
                            //factory.MakeWindowAssociation(Hwnd, WindowAssociationFlags.IgnoreAll);

                            // Define the properties of the swap chain.
                            SwapChainDescription swapChainDescription = new SwapChainDescription();
                            swapChainDescription.BufferCount = 1;
                            swapChainDescription.IsWindowed = true;
                            swapChainDescription.OutputHandle = Hwnd;
                            swapChainDescription.SampleDescription = new SampleDescription(1, 0);
                            swapChainDescription.Usage = Usage.RenderTargetOutput | Usage.ShaderInput;
                            swapChainDescription.SwapEffect = SwapEffect.Sequential;
                            swapChainDescription.Flags = SwapChainFlags.AllowModeSwitch;
                            swapChainDescription.ModeDescription.Width = SurfaceWidth;
                            swapChainDescription.ModeDescription.Height = SurfaceHeight;
                            swapChainDescription.ModeDescription.Format = Format.R8G8B8A8_UNorm;
                            swapChainDescription.ModeDescription.RefreshRate.Numerator = 0;
                            swapChainDescription.ModeDescription.RefreshRate.Denominator = 1;
                            // Create the swap chain.
                            SwapChain = new SwapChain(factoryer, Device, swapChainDescription);
                            factoryer.Dispose();


                            // Retrieve the back buffer of the swap chain.
                            BackBuffer = SwapChain.GetBackBuffer<SharpDX.Direct3D11.Texture2D>(0);
                            _renderTargetView = new RenderTargetView(Device, BackBuffer);

                            // Create a depth buffer, using the same width and height as the back buffer.
                            Texture2DDescription depthBufferDescription = new Texture2DDescription();
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

                            depthBuffer = new SharpDX.Direct3D11.Texture2D(Device, depthBufferDescription);
                            _depthStencilView = new DepthStencilView(Device, depthBuffer);
                            depthStencilState = new SharpDX.Direct3D11.DepthStencilState(Device, depthStencilStateDescription);
                            var viewport = new SharpDX.Viewport(0, 0, hmdDesc.Resolution.Width, hmdDesc.Resolution.Height, 0.0f, 1.0f);

                            DeviceContext.OutputMerger.SetDepthStencilState(depthStencilState);
                            DeviceContext.OutputMerger.SetRenderTargets(_depthStencilView, _renderTargetView);

                            // Setup the raster description which will determine how and what polygon will be drawn.
                            RasterizerStateDescription rasterDesc = new RasterizerStateDescription()
                            {
                                IsAntialiasedLineEnabled = false,
                                CullMode = CullMode.Front,
                                DepthBias = 0,
                                DepthBiasClamp = .0f,
                                IsDepthClipEnabled = true,
                                FillMode = FillMode.Solid,
                                IsFrontCounterClockwise = false,
                                IsMultisampleEnabled = true,
                                IsScissorEnabled = false,
                                SlopeScaledDepthBias = .0f
                            };

                            // Create the rasterizer state from the description we just filled out.
                            RasterState = new RasterizerState(Device, rasterDesc);

                            // Now set the rasterizer state.
                            DeviceContext.Rasterizer.State = RasterState;

                            /*var rasterDesc = new RasterizerStateDescription()
                            {
                                IsAntialiasedLineEnabled = false,
                                CullMode = CullMode.Back,
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
                            RasterState = new RasterizerState(_device, rasterDesc);

                            // Now set the rasterizer state.
                            _device.ImmediateContext.Rasterizer.State = RasterState;


                            DeviceContext.Rasterizer.SetViewport(viewport);

                            // Retrieve the DXGI device, in order to set the maximum frame latency.
                            using (SharpDX.DXGI.Device1 dxgiDevice = Device.QueryInterface<SharpDX.DXGI.Device1>())
                            {
                                dxgiDevice.MaximumFrameLatency = 1;
                            }

                            layerEyeFov = new LayerEyeFov();
                            layerEyeFov.Header.Type = LayerType.EyeFov;
                            layerEyeFov.Header.Flags = LayerFlags.None;

                            // Create a set of layers to submit.
                            eyeTextures = new EyeTexture[2];

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
                                eyeTexture.Viewport = new SharpDX.Viewport(0, 0, eyeTexture.TextureSize.Width, eyeTexture.TextureSize.Height, 0.0f, 1.0f);

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
                                eyeTexture.Textures = new SharpDX.Direct3D11.Texture2D[textureSwapChainBufferCount];
                                eyeTexture.RenderTargetViews = new RenderTargetView[textureSwapChainBufferCount];

                                // Create a texture 2D and a render target view, for each unmanaged texture contained in the SwapTextureSet.
                                for (int textureIndex = 0; textureIndex < textureSwapChainBufferCount; textureIndex++)
                                {
                                    // Retrieve the Direct3D texture contained in the Oculus TextureSwapChainBuffer.
                                    IntPtr swapChainTextureComPtr = IntPtr.Zero;
                                    result = eyeTexture.SwapTextureSet.GetBufferDX(textureIndex, textureInterfaceId, out swapChainTextureComPtr);
                                    WriteErrorDetails(OVR, result, "Failed to retrieve a texture from the created swap chain.");

                                    // Create a managed Texture2D, based on the unmanaged texture pointer.
                                    eyeTexture.Textures[textureIndex] = new SharpDX.Direct3D11.Texture2D(swapChainTextureComPtr);

                                    // Create a render target view for the current Texture2D.
                                    eyeTexture.RenderTargetViews[textureIndex] = new RenderTargetView(Device, eyeTexture.Textures[textureIndex]);
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
                                eyeTexture.DepthBuffer = new SharpDX.Direct3D11.Texture2D(Device, eyeTexture.DepthBufferDescription);
                                eyeTexture.DepthStencilView = new DepthStencilView(Device, eyeTexture.DepthBuffer);

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
                            mirrorTextureD3D = new SharpDX.Direct3D11.Texture2D(mirrorTextureComPtr);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    DeviceContext.ClearState();
                    DeviceContext.Flush();*/
                }


                return true;
            }
            catch
            {
                return false;
            }
        }
        public void ShutDown()
        {
            // Before shutting down set to windowed mode or when you release the swap chain it will throw an exception.   
            SwapChain?.SetFullscreenState(false, null);

            RasterState?.Dispose();
            RasterState = null;

            depthStencilState?.Dispose();
            depthStencilState = null;
            DepthStencilBuffer?.Dispose();
            DepthStencilBuffer = null;
            //RenderTargetView?.Dispose();
            //RenderTargetView = null;

            _depthStencilView?.Dispose();
            _depthStencilView = null;
            _renderTargetView?.Dispose();
            _renderTargetView = null;


            DeviceContext?.Dispose();
            //DeviceContext = null;
            Device?.Dispose();
            //Device = null;
            SwapChain?.Dispose();
            //SwapChain = null;

            _xaud?.Dispose();
            _xaud = null;



        }
        /*public void BeginScene(float red, float green, float blue, float alpha, Program._someObject[] _someReceivedObject0)
        {
            BeginScene(new Color4(red, green, blue, alpha), _someReceivedObject0);
        }*/


        XAudio2 _xaud;
        XAudio2 _xaudio_device;
        int _getXAudio = 0;
        /*private void BeginScene(Color4 givenColour, Program._someObject[] _someReceivedObject0)
        {
            // Clear the depth buffer.
            //DeviceContext.ClearDepthStencilView(_depthStencilView, DepthStencilClearFlags.Depth, 1, 0);

            // Clear the back buffer.
            //DeviceContext.ClearRenderTargetView(_renderTargetView, givenColour);

            /*if (_getXAudio == 0)
            {
                if (_someReceivedObject0[0]._someData != null)
                {
                    if (_someReceivedObject0[0]._someData[0]!= null)
                    {
                        _xaud = _someReceivedObject0[0]._someData[0] as XAudio2;
                        if (_xaud  != null)
                        {
                            _getXAudio = 1;
                        }
                    }
                }
            }
        }*/
    
        public void ClearSceneVisual() 
        {
            // Clear the depth buffer.
            //DeviceContext.ClearDepthStencilView(_depthStencilView, DepthStencilClearFlags.Depth, 1, 0);
            // Clear the back buffer.
            //DeviceContext.ClearRenderTargetView(_renderTargetView, new Color4(0, 0, 0, 1));
        }

        public void EndScene()
        {
            // Present the back buffer to the screen since rendering is complete.
            if (VerticalSyncEnabled)
                SwapChain.Present(1, PresentFlags.None); // Lock to screen refresh rate.
            else
                SwapChain.Present(0, PresentFlags.None); // Present as fast as possible.
        }


        //Program._someObject[] _someReceivedObject0;




        public void WriteErrorDetails(OvrWrap OVR, Ab3d.OculusWrap.Result result, string message)
        {
            if (result >= Ab3d.OculusWrap.Result.Success)
                return;

            // Retrieve the error message from the last occurring error.
            ErrorInfo errorInformation = OVR.GetLastErrorInfo();

            string formattedMessage = string.Format("{0}. \nMessage: {1} (Error code={2})", message, errorInformation.ErrorString, errorInformation.Result);
            //Trace.WriteLine(formattedMessage);
            //System.Windows.Forms.MessageBox.Show(formattedMessage, message);

            //throw new Exception(formattedMessage);
        }
    }
}







/*SC_Console_WRITER._messager _some_received_messages0 = new SC_Console_WRITER._messager();
_some_received_messages0._message = "not null device";
_some_received_messages0._originalMsg = _some_received_messages0._message;
_some_received_messages0._messageCut = _some_received_messages0._message;
_some_received_messages0._specialMessage = 0;
_some_received_messages0._specialMessageLineX = 0;
_some_received_messages0._specialMessageLineY = 0;
_some_received_messages0._lineX = 1;
_some_received_messages0._lineY = 20;
_some_received_messages0._count = 0;
_some_received_messages0._swtch0 = 1;
_some_received_messages0._swtch1 = 1;
_some_received_messages0._delay = 200;
_some_received_messages0._looping = 1;

_writer._message_to_pass_list.Add(_some_received_messages0);*/