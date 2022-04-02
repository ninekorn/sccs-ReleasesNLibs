using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Runtime.InteropServices;
using DSystemConfiguration = _sc_core_systems._sc_core._sc_system_configuration;
using Ab3d.OculusWrap;
//using Ab3d.DXEngine.OculusWrap;
using Ab3d.OculusWrap.DemoDX11;
using Result = Ab3d.OculusWrap.Result;
using sccsr11wpf;
namespace _sc_core_systems
{
    public class SC_console_directx
    {
        public enum BodyTag
        {

            DrawMe,
            DontDrawMe,
            Terrain,
            pseudoCloth,
            physicsInstancedCube,
            physicsInstancedScreen,

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
            PlayerLeftElbowTargettwo,
            PlayerRightElbowTargettwo,

            sc_perko_voxel,
            Screen,

            //someothertest,
            //testChunkCloth,
            //cloth_cube,
            //screen_corners,
            //screen_pointer_touch,
            //screen_pointer_HMD,
            _terrain_tiles,
            _terrain,
            //_icosphere,
            //_sphere,
            _spectrum,
            //_physics_cube_group_b,
            _voxel_spheroid,
            _screen_assets
        }

        public SharpDX.Direct3D11.Device device { get; set; }

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        //OCULUS RIFT
        public bool _useOculusRift = true;
        public int SurfaceWidth;
        public int SurfaceHeight;
        public DateTime startTime;
        //public OculusWrapVirtualRealityProvider _oculusRiftVirtualRealityProvider;
        //public static Ab3d.DirectX.DXDevice _dxDevice;
        private RenderTargetView _renderTargetView;
        SharpDX.Direct3D11.Texture2D depthBuffer;
        private DepthStencilView _depthStencilView;
        protected DepthStencilView DepthStencilView => _depthStencilView;
        SharpDX.Direct3D11.DepthStencilState depthStencilState;
        MirrorTexture mirrorTexture = null;
        Guid textureInterfaceId = new Guid("6f15aaf2-d208-4e89-9ab4-489535d34f9c"); // Interface ID of the Direct3D Texture2D interface.

        // Properties.
        public bool VerticalSyncEnabled { get; set; }
        public int VideoCardMemory { get; private set; }
        public string VideoCardDescription { get; private set; }
        public SwapChain SwapChain { get; set; }
        public SharpDX.Direct3D11.Device Device { get; private set; }
        public DeviceContext DeviceContext { get; private set; }
        public Texture2D DepthStencilBuffer { get; set; }
        public DepthStencilState _depthStencilState { get; set; }
        public RasterizerState RasterState { get; set; }
        public Matrix ProjectionMatrix { get; private set; }
        public Matrix WorldMatrix { get; private set; }
        public OvrWrap OVR;
        public HmdDesc hmdDesc;
        public IntPtr sessionPtr;
        public Result result;
        public LayerEyeFov layerEyeFov;
        public EyeTexture[] eyeTextures;
        public Texture2D BackBuffer;
        public SharpDX.Direct3D11.Texture2D mirrorTextureD3D;

        public ControllerType controllerTypeRTouch;
        public ControllerType controllerTypeLTouch;
        public Ab3d.OculusWrap.InputState inputStateLTouch;
        public Ab3d.OculusWrap.InputState inputStateRTouch;

        // Constructor
        public SC_console_directx()
        {

        }

        // Methods
        public bool Initialize(DSystemConfiguration configuration, IntPtr Hwnd,_sc_console._sc_console_writer _writer)
        {
            try
            {
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
                            MainWindow.MessageBox((IntPtr)0, "Failed to initialize the Oculus runtime library:\r\n" + errorReason, "sccoresystems", 0);//.Show("Failed to initialize the Oculus runtime library:\r\n" + errorReason, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //return;
                        }

                        // Use the head mounted display.
                        sessionPtr = IntPtr.Zero;


                        var graphicsLuid = new GraphicsLuid();
                        result = OVR.Create(ref sessionPtr, ref graphicsLuid);
                        if (result < Result.Success)
                        {
                            MainWindow.MessageBox((IntPtr)0, "The HMD is not enabled: " + result.ToString(), "sccoresystems", 0);
                            //return;
                        }

                        hmdDesc = OVR.GetHmdDesc(sessionPtr);



                        /*var swapChainDesc = new SwapChainDescription();
                        swapChainDesc.BufferCount = 1;
                        swapChainDesc.IsWindowed = true;
                        swapChainDesc.OutputHandle = MainWindow.consoleHandle; //form.Handle
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
                        swapChainDescript.OutputHandle = MainWindow.consoleHandle;
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
            _depthStencilView?.Dispose();
            _depthStencilView = null;
            _renderTargetView?.Dispose();
            _renderTargetView = null;
            DeviceContext?.Dispose();
            Device?.Dispose();
            SwapChain?.Dispose();
        }

        public void WriteErrorDetails(OvrWrap OVR, Ab3d.OculusWrap.Result result, string message)
        {
            if (result >= Ab3d.OculusWrap.Result.Success)
                return;

            ErrorInfo errorInformation = OVR.GetLastErrorInfo();

            string formattedMessage = string.Format("{0}. \nMessage: {1} (Error code={2})", message, errorInformation.ErrorString, errorInformation.Result);
            //Trace.WriteLine(formattedMessage);
            //System.Windows.Forms.MessageBox.Show(formattedMessage, message);

            throw new Exception(formattedMessage);
        }
    }
}