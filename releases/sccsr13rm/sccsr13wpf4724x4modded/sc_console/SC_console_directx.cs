using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Runtime.InteropServices;
using DSystemConfiguration = SCCoreSystems.sc_core.sc_system_configuration;
using Ab3d.OculusWrap;
//using Ab3d.DXEngine.OculusWrap;
using Ab3d.OculusWrap.DemoDX11;
using Result = Ab3d.OculusWrap.Result;


using System.Threading;
using System.Threading.Tasks;
using scmessageobject = SCCoreSystems.scmessageobject.scmessageobject;
using scmessageobjectjitter = SCCoreSystems.scmessageobject.scmessageobjectjitter;

using ISCCS_Jitter_Interface = Jitter.ISCCS_Jitter_Interface;
using Jitter;
using Jitter.LinearMath;
using System.Diagnostics;
using System.IO.Ports;

namespace SCCoreSystems.sc_console
{
    public abstract class SC_console_directx : ISCCS_Jitter_Interface
    {
        public SerialPort serialPort;

        jitter_sc[] jitter_sc;
        scmessageobjectjitter[][] sccsjittertasks;

        public jitter_sc sc_create_jitter_instances(sc_jitter_data _sc_jitter_data)
        {


            return null;
        }

        static jitter_sc instance = null;

        public static jitter_sc Instance
        {
            get
            {
                /*if (instance == null)
                {
                    instance = new jitter_sc();
                }*/
                //instance = new jitter_sc();

                return instance;
            }
        }



        public jitter_sc[] create_jitter_instances(jitter_sc[] sc_jitter_physics, sc_jitter_data _sc_jitter_data)
        {
            for (int xx = 0; xx < MainWindow._physics_engine_instance_x; xx++)
            {
                for (int yy = 0; yy < MainWindow._physics_engine_instance_y; yy++)
                {
                    for (int zz = 0; zz < MainWindow._physics_engine_instance_z; zz++)
                    {
                        var indexer00 = xx + MainWindow._physics_engine_instance_x * (yy + MainWindow._physics_engine_instance_y * zz);

                        //instance = new jitter_sc();

                        instance = new jitter_sc();
                        sc_jitter_physics[indexer00] = instance.sc_create_jitter_instances(_sc_jitter_data);
                    }
                }
            }

            return sc_jitter_physics;
        }

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


        Thread mainthreadupdate;

        public Matrix OrthoMatrix { get; private set; }
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


        public static SC_console_directx D3D;

        public DepthStencilState DepthDisabledStencilState { get; private set; }
        public BlendState AlphaEnableBlendingState { get; private set; }
        public BlendState AlphaDisableBlendingState { get; private set; }
        public DepthStencilState DepthStencilState { get; private set; }


        // Constructor
        /*public SC_console_directx()
        {

        }*/

        protected SC_console_directx() //DSystemConfiguration configuration, IntPtr Hwnd, sc_console.sc_console_writer _writer
        {
            D3D = this;
            //Update();
            SC_Init_DirectX(); //configuration, Hwnd, _writer 
        }




        protected virtual void SC_Init_DirectX() //DSystemConfiguration configuration, IntPtr Hwnd, sc_console.sc_console_writer _writer
        {
            //MessageBox((IntPtr)0,"" + MainWindow.handler, "SCCoreSystems Error", 0);

            //sc_graphics_sec _graphics_sec
            // Methods
            //public bool Initialize(DSystemConfiguration configuration, IntPtr Hwnd,sc_console.sc_console_writer _writer)
            //{
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

                if (_useOculusRift)
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
                        swapChainDescription.OutputHandle = MainWindow.consoleHandle;
                        swapChainDescription.SampleDescription = new SampleDescription(1, 0);
                        swapChainDescription.Usage = Usage.RenderTargetOutput | Usage.ShaderInput;//Usage.RenderTargetOutput;// | Usage.ShaderInput;
                        swapChainDescription.SwapEffect = SwapEffect.Sequential;
                        swapChainDescription.Flags = SwapChainFlags.AllowModeSwitch;
                        swapChainDescription.ModeDescription.Width = SurfaceWidth;
                        swapChainDescription.ModeDescription.Height = SurfaceHeight;
                        swapChainDescription.ModeDescription.Format = Format.R8G8B8A8_UNorm;
                        swapChainDescription.ModeDescription.RefreshRate.Numerator = 0;
                        swapChainDescription.ModeDescription.RefreshRate.Denominator = 1;
                        // Create a set of layers to submit.
                        eyeTextures = new EyeTexture[2];

                        // Create DirectX drawing device.
                        //device = new Device(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.Debug);
                        //Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None, swapChainDescription, out device, out swapChain);

                        SwapChain someswap;
                        SharpDX.Direct3D11.Device somedevice;
                        // Create DirectX drawing device.
                        //device = new Device(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.Debug);
                        SharpDX.Direct3D11.Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None, swapChainDescription, out somedevice, out someswap);

                        SwapChain = someswap;
                        device = somedevice;
                        Device = somedevice;





                        // Create DirectX Graphics Interface factory, used to create the swap chain.
                        var factory = new SharpDX.DXGI.Factory4();

                        DeviceContext = device.ImmediateContext;

                        /*// Define the properties of the swap chain.
                        SwapChainDescription swapChainDescription = new SwapChainDescription();
                        swapChainDescription.BufferCount = 1;
                        swapChainDescription.IsWindowed = true;
                        swapChainDescription.OutputHandle = form.Handle;
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
                        swapChain = new SwapChain(factory, device, swapChainDescription);
                        */
                        // Retrieve the back buffer of the swap chain.
                        BackBuffer = SwapChain.GetBackBuffer<Texture2D>(0);
                        _renderTargetView = new RenderTargetView(device, BackBuffer);

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

                        // Create the depth buffer.
                        depthBuffer = new Texture2D(device, depthBufferDescription);
                        _depthStencilView = new DepthStencilView(device, depthBuffer);
                        depthStencilState = new DepthStencilState(device, depthStencilStateDescription);

                        var viewport = new Viewport(0, 0, hmdDesc.Resolution.Width, hmdDesc.Resolution.Height, 0.0f, 1.0f);

                        DeviceContext.OutputMerger.SetDepthStencilState(depthStencilState);
                        DeviceContext.OutputMerger.SetRenderTargets(_depthStencilView, _renderTargetView);
                        DeviceContext.Rasterizer.SetViewport(viewport);

                        // Retrieve the DXGI device, in order to set the maximum frame latency.
                        using (SharpDX.DXGI.Device1 dxgiDevice = device.QueryInterface<SharpDX.DXGI.Device1>())
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

                            result = OVR.CreateTextureSwapChainDX(sessionPtr, device.NativePointer, ref textureSwapChainDesc, out textureSwapChainPtr);
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
                                eyeTexture.RenderTargetViews[textureIndex] = new RenderTargetView(device, eyeTexture.Textures[textureIndex]);
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
                            eyeTexture.DepthBuffer = new Texture2D(device, eyeTexture.DepthBufferDescription);
                            eyeTexture.DepthStencilView = new DepthStencilView(device, eyeTexture.DepthBuffer);

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
                        result = OVR.CreateMirrorTextureDX(sessionPtr, device.NativePointer, ref mirrorTextureDescription, out mirrorTexturePtr);
                        WriteErrorDetails(OVR, result, "Failed to create mirror texture.");

                        mirrorTexture = new MirrorTexture(OVR, sessionPtr, mirrorTexturePtr);


                        // Retrieve the Direct3D texture contained in the Oculus MirrorTexture.
                        IntPtr mirrorTextureComPtr = IntPtr.Zero;
                        result = mirrorTexture.GetBufferDX(textureInterfaceId, out mirrorTextureComPtr);
                        WriteErrorDetails(OVR, result, "Failed to retrieve the texture from the created mirror texture buffer.");

                        // Create a managed Texture2D, based on the unmanaged texture pointer.
                        mirrorTextureD3D = new Texture2D(mirrorTextureComPtr);

                    }
                    catch
                    {

                    }


                    DeviceContext.ClearState();
                    DeviceContext.Flush();







                    //IN DEVELOPMENT WIP BY STEVE CHASSÉ, THIS IS THE AB3D.OCULUSWRAP DIRECT11 MIT SAMPLE CODE THAT I CANNOT SEEM TO MAKE IT WORK. THE OBJECTS ARE DRAWN IN THE WRONG ORDER AND APPEAR IN FRONT WHEN THEY SHOULD BE IN THE BACK, AND THE EYES ARE UPSIDE DOWN AND THE TRIANGLES/FACES ARE INVERTED.
                    //IN DEVELOPMENT WIP BY STEVE CHASSÉ, THIS IS THE AB3D.OCULUSWRAP DIRECT11 MIT SAMPLE CODE THAT I CANNOT SEEM TO MAKE IT WORK. THE OBJECTS ARE DRAWN IN THE WRONG ORDER AND APPEAR IN FRONT WHEN THEY SHOULD BE IN THE BACK, AND THE EYES ARE UPSIDE DOWN AND THE TRIANGLES/FACES ARE INVERTED.
                    //IN DEVELOPMENT WIP BY STEVE CHASSÉ, THIS IS THE AB3D.OCULUSWRAP DIRECT11 MIT SAMPLE CODE THAT I CANNOT SEEM TO MAKE IT WORK. THE OBJECTS ARE DRAWN IN THE WRONG ORDER AND APPEAR IN FRONT WHEN THEY SHOULD BE IN THE BACK, AND THE EYES ARE UPSIDE DOWN AND THE TRIANGLES/FACES ARE INVERTED.
                    /*Result result;

                    OVR = OvrWrap.Create();

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
                        MessageBox((IntPtr)0, "Failed to initialize the Oculus runtime library:\r\n" + errorReason, "Oculus Message", 0);
                        //MessageBox.Show("Failed to initialize the Oculus runtime library:\r\n" + errorReason, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Use the head mounted display.
                    sessionPtr = IntPtr.Zero;
                    var graphicsLuid = new GraphicsLuid();
                    result = OVR.Create(ref sessionPtr, ref graphicsLuid);
                    if (result < Result.Success)
                    {
                        MessageBox((IntPtr)0, "The HMD is not enabled: " + result.ToString(), "Oculus Message", 0);
                        //MessageBox.Show("The HMD is not enabled: " + result.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    hmdDesc = OVR.GetHmdDesc(sessionPtr);




                    try
                    {
                        SwapChainDescription swapChainDescription = new SwapChainDescription();
                        swapChainDescription.BufferCount = 1;
                        swapChainDescription.IsWindowed = true;
                        swapChainDescription.OutputHandle = MainWindow.consoleHandle;
                        swapChainDescription.SampleDescription = new SampleDescription(1, 0);
                        swapChainDescription.Usage = Usage.RenderTargetOutput | Usage.ShaderInput;//Usage.RenderTargetOutput;// | Usage.ShaderInput;
                        swapChainDescription.SwapEffect = SwapEffect.Sequential;
                        swapChainDescription.Flags = SwapChainFlags.AllowModeSwitch;
                        swapChainDescription.ModeDescription.Width = SurfaceWidth;
                        swapChainDescription.ModeDescription.Height = SurfaceHeight;
                        swapChainDescription.ModeDescription.Format = Format.R8G8B8A8_UNorm;
                        swapChainDescription.ModeDescription.RefreshRate.Numerator = 0;
                        swapChainDescription.ModeDescription.RefreshRate.Denominator = 1;
                        // Create a set of layers to submit.
                        eyeTextures = new EyeTexture[2];


                        SwapChain someswap;
                        SharpDX.Direct3D11.Device somedevice;
                        // Create DirectX drawing device.
                        //device = new Device(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.Debug);
                        SharpDX.Direct3D11.Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None, swapChainDescription, out somedevice, out someswap);

                        SwapChain = someswap;
                        device = somedevice;
                        Device = somedevice;


                        // Create DirectX Graphics Interface factory, used to create the swap chain.
                        var factory = new SharpDX.DXGI.Factory4();

                        DeviceContext = device.ImmediateContext;

                        // Define the properties of the swap chain.
                        //SwapChainDescription swapChainDescription = new SwapChainDescription();
                        //swapChainDescription.BufferCount = 1;
                        ///swapChainDescription.IsWindowed = true;
                        //swapChainDescription.OutputHandle = form.Handle;
                        //swapChainDescription.SampleDescription = new SampleDescription(1, 0);
                        //swapChainDescription.Usage = Usage.RenderTargetOutput | Usage.ShaderInput;//Usage.RenderTargetOutput;// | Usage.ShaderInput;
                        //swapChainDescription.SwapEffect = SwapEffect.Sequential;
                        //swapChainDescription.Flags = SwapChainFlags.AllowModeSwitch;
                        //swapChainDescription.ModeDescription.Width = form.Width;
                        //swapChainDescription.ModeDescription.Height = form.Height;
                        //swapChainDescription.ModeDescription.Format = Format.R8G8B8A8_UNorm;
                        //swapChainDescription.ModeDescription.RefreshRate.Numerator = 0;
                        //swapChainDescription.ModeDescription.RefreshRate.Denominator = 1;

                        // Create the swap chain.
                        //swapChain = new SwapChain(factory, device, swapChainDescription);

                        // Retrieve the back buffer of the swap chain.
                        BackBuffer = SwapChain.GetBackBuffer<Texture2D>(0);
                        _renderTargetView = new RenderTargetView(device, BackBuffer);

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

                        // Create the depth buffer.
                        depthBuffer = new Texture2D(device, depthBufferDescription);
                        _depthStencilView = new DepthStencilView(device, depthBuffer);
                        depthStencilState = new DepthStencilState(device, depthStencilStateDescription);

                        var viewport = new Viewport(0, 0, hmdDesc.Resolution.Width, hmdDesc.Resolution.Height, 0.0f, 1.0f);

                        DeviceContext.OutputMerger.SetDepthStencilState(depthStencilState);
                        DeviceContext.OutputMerger.SetRenderTargets(_depthStencilView, _renderTargetView);
                        DeviceContext.Rasterizer.SetViewport(viewport);

                        // Retrieve the DXGI device, in order to set the maximum frame latency.
                        using (SharpDX.DXGI.Device1 dxgiDevice = device.QueryInterface<SharpDX.DXGI.Device1>())
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

                            result = OVR.CreateTextureSwapChainDX(sessionPtr, device.NativePointer, ref textureSwapChainDesc, out textureSwapChainPtr);
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
                                eyeTexture.RenderTargetViews[textureIndex] = new RenderTargetView(device, eyeTexture.Textures[textureIndex]);
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
                            eyeTexture.DepthBuffer = new Texture2D(device, eyeTexture.DepthBufferDescription);
                            eyeTexture.DepthStencilView = new DepthStencilView(device, eyeTexture.DepthBuffer);

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
                        result = OVR.CreateMirrorTextureDX(sessionPtr, device.NativePointer, ref mirrorTextureDescription, out mirrorTexturePtr);
                        WriteErrorDetails(OVR, result, "Failed to create mirror texture.");

                        mirrorTexture = new MirrorTexture(OVR, sessionPtr, mirrorTexturePtr);


                        // Retrieve the Direct3D texture contained in the Oculus MirrorTexture.
                        IntPtr mirrorTextureComPtr = IntPtr.Zero;
                        result = mirrorTexture.GetBufferDX(textureInterfaceId, out mirrorTextureComPtr);
                        WriteErrorDetails(OVR, result, "Failed to retrieve the texture from the created mirror texture buffer.");

                        // Create a managed Texture2D, based on the unmanaged texture pointer.
                        mirrorTextureD3D = new Texture2D(mirrorTextureComPtr);



                    }
                    catch
                    {

                    }*/

                    //IN DEVELOPMENT WIP BY STEVE CHASSÉ, THIS IS THE AB3D.OCULUSWRAP DIRECT11 MIT SAMPLE CODE THAT I CANNOT SEEM TO MAKE IT WORK. THE OBJECTS ARE DRAWN IN THE WRONG ORDER AND APPEAR IN FRONT WHEN THEY SHOULD BE IN THE BACK, AND THE EYES ARE UPSIDE DOWN AND THE TRIANGLES/FACES ARE INVERTED.
                    //IN DEVELOPMENT WIP BY STEVE CHASSÉ, THIS IS THE AB3D.OCULUSWRAP DIRECT11 MIT SAMPLE CODE THAT I CANNOT SEEM TO MAKE IT WORK. THE OBJECTS ARE DRAWN IN THE WRONG ORDER AND APPEAR IN FRONT WHEN THEY SHOULD BE IN THE BACK, AND THE EYES ARE UPSIDE DOWN AND THE TRIANGLES/FACES ARE INVERTED.
                    //IN DEVELOPMENT WIP BY STEVE CHASSÉ, THIS IS THE AB3D.OCULUSWRAP DIRECT11 MIT SAMPLE CODE THAT I CANNOT SEEM TO MAKE IT WORK. THE OBJECTS ARE DRAWN IN THE WRONG ORDER AND APPEAR IN FRONT WHEN THEY SHOULD BE IN THE BACK, AND THE EYES ARE UPSIDE DOWN AND THE TRIANGLES/FACES ARE INVERTED.




















                    /*
                    try
                    {

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




                        //gotta find a way to load the oculus rift service manually maybe.

                        //------------------------------FOR AB3D DX ENGINE Device.
                        _oculusRiftVirtualRealityProvider = new OculusWrapVirtualRealityProvider(OVR, multisamplingCount: 16);
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


                       //OVR.GetTextureSwapChainBufferDX(_oculusRiftVirtualRealityProvider.SessionPtr,);

                       sessionPtr = _oculusRiftVirtualRealityProvider.SessionPtr;
                       hmdDesc = OVR.GetHmdDesc(sessionPtr);
                       //----------------------FOR AB3D DX ENGINE Device.

                       SessionStatus sessionStat = _oculusRiftVirtualRealityProvider.LastSessionStatus;
                       var res = OVR.GetSessionStatus(sessionPtr, ref sessionStat);

                       //if (sessionStat.foc)
                       //{
                       //
                       //}


                       //WriteErrorDetails(OVR, res, "Session Status");



                       Device = _dxDevice.Device;
                       device = Device;
                       DeviceContext = Device.ImmediateContext;

                       if (Device == null)
                       {
                           MessageBox((IntPtr)0, "null", "SCCoreSystems Error", 0);
                       }




                       // Create DirectX drawing device.
                       // Create DirectX Graphics Interface factory, used to create the swap chain.
                       using (var factory = new SharpDX.DXGI.Factory1())
                       {
                           //factory.MakeWindowAssociation(MainWindow.GameHandle, WindowAssociationFlags.IgnoreAll);

                           // Define the properties of the swap chain.
                           SwapChainDescription swapChainDescription = new SwapChainDescription();
                           swapChainDescription.BufferCount = 1;
                           swapChainDescription.IsWindowed = true;
                           swapChainDescription.OutputHandle = MainWindow.consoleHandle;
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
                           SwapChain = new SwapChain(factory, Device, swapChainDescription);
                           factory.Dispose();


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
                           //RasterizerStateDescription rasterDesc = new RasterizerStateDescription()
                           //{
                           //    IsAntialiasedLineEnabled = false,
                           //    CullMode = CullMode.Front,
                           //    DepthBias = 0,
                           //    DepthBiasClamp = .0f,
                           //    IsDepthClipEnabled = true,
                           //    FillMode = FillMode.Solid,
                           //    IsFrontCounterClockwise = true,
                           //    IsMultisampleEnabled = true,
                           //    IsScissorEnabled = false,
                           //    SlopeScaledDepthBias = .0f
                           //};

                           // Create the rasterizer state from the description we just filled out.
                           //RasterState = new RasterizerState(Device, rasterDesc);

                           // Now set the rasterizer state.
                           //DeviceContext.Rasterizer.State = RasterState;
                           //var rasterDesc = new RasterizerStateDescription()
                           //{
                           //    IsAntialiasedLineEnabled = false,
                           //    CullMode = CullMode.Back,
                           //    DepthBias = 0,
                           //    DepthBiasClamp = 0.0f,
                           //    IsDepthClipEnabled = true,
                           //    FillMode = SharpDX.Direct3D11.FillMode.Solid,
                           //    IsFrontCounterClockwise = false,
                           //    IsMultisampleEnabled = false,
                           //    IsScissorEnabled = false,
                           //    SlopeScaledDepthBias = 0.0f
                           //};

                           // Create the rasterizer state from the description we just filled out.
                           //RasterState = new RasterizerState(_device, rasterDesc);

                           // Now set the rasterizer state.
                           //_device.ImmediateContext.Rasterizer.State = RasterState;


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
                //return true;
            }
            catch
            {
                //return false;
            }








            /*
            // Initialize and set up the description of the depth buffer.
            var depthBufferDesc = new Texture2DDescription()
            {
                Width = MainWindow.config.Width,
                Height = MainWindow.config.Height,
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
            DepthStencilBuffer = new Texture2D(device, depthBufferDesc);


            
            
            
            
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








            //STRAIGHT COPY PASTE FROM C# RASTERTEK DAN6040. ALL CREDITS TO HIM. WOW HE IS SUCH A GOOD SCRIPTER. I AM MISSING TIME.

            // Create an orthographic projection matrix for 2D rendering.
            OrthoMatrix = Matrix.OrthoLH(MainWindow.config.Width, MainWindow.config.Height, DSystemConfiguration.ScreenNear, DSystemConfiguration.ScreenDepth);



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














            // Create an alpha enabled blend state description.
            var blendStateDesc = new BlendStateDescription();
            blendStateDesc.RenderTarget[0].IsBlendEnabled = true;
            blendStateDesc.RenderTarget[0].SourceBlend = BlendOption.SourceAlpha;
            blendStateDesc.RenderTarget[0].DestinationBlend = BlendOption.InverseSourceAlpha;
            blendStateDesc.RenderTarget[0].BlendOperation = BlendOperation.Add;
            blendStateDesc.RenderTarget[0].SourceAlphaBlend = BlendOption.One;
            blendStateDesc.RenderTarget[0].DestinationAlphaBlend = BlendOption.Zero;
            blendStateDesc.RenderTarget[0].AlphaBlendOperation = BlendOperation.Add;
            blendStateDesc.RenderTarget[0].RenderTargetWriteMask = ColorWriteMaskFlags.All;

            // Create the blend state using the description.
            AlphaEnableBlendingState = new BlendState(device, blendStateDesc);

            // Modify the description to create an disabled blend state description.
            blendStateDesc.RenderTarget[0].IsBlendEnabled = false;

            // Create the blend state using the description.
            AlphaDisableBlendingState = new BlendState(device, blendStateDesc);
            */


















            try
            {
                mainthreadupdate = new Thread(() =>
                {

                    if (MainWindow.usejitterphysics == 1)
                    {
                        jitter_sc = new jitter_sc[MainWindow._physics_engine_instance_x * MainWindow._physics_engine_instance_y * MainWindow._physics_engine_instance_z];
                        sccsjittertasks = new scmessageobjectjitter[MainWindow._physics_engine_instance_x * MainWindow._physics_engine_instance_y * MainWindow._physics_engine_instance_z][];

                        sc_jitter_data _sc_jitter_data = new sc_jitter_data();
                        _sc_jitter_data.alloweddeactivation = MainWindow._allow_deactivation;
                        _sc_jitter_data.allowedpenetration = MainWindow._world_allowed_penetration;
                        _sc_jitter_data.width = MainWindow.world_width;
                        _sc_jitter_data.height = MainWindow.world_height;
                        _sc_jitter_data.depth = MainWindow.world_depth;
                        _sc_jitter_data.gravity = MainWindow._world_gravity;
                        _sc_jitter_data.smalliterations = MainWindow._world_small_iterations;
                        _sc_jitter_data.iterations = MainWindow._world_iterations;


                        for (int xx = 0; xx < MainWindow._physics_engine_instance_x; xx++)
                        {
                            for (int yy = 0; yy < MainWindow._physics_engine_instance_y; yy++)
                            {
                                for (int zz = 0; zz < MainWindow._physics_engine_instance_z; zz++)
                                {
                                    var indexer00 = xx + MainWindow._physics_engine_instance_x * (yy + MainWindow._physics_engine_instance_y * zz);
                                    //_jitter_physics[indexer00] = DoSpecialThing();
                                    sccsjittertasks[indexer00] = new scmessageobjectjitter[MainWindow.world_width * MainWindow.world_height * MainWindow.world_depth];

                                    for (int x = 0; x < MainWindow.world_width; x++)
                                    {
                                        for (int y = 0; y < MainWindow.world_height; y++)
                                        {
                                            for (int z = 0; z < MainWindow.world_depth; z++)
                                            {
                                                var indexer01 = x + MainWindow.world_width * (y + MainWindow.world_height * z);
                                                sccsjittertasks[indexer00][indexer01] = new scmessageobjectjitter();
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        //Console.WriteLine("built0");
                        jitter_sc = create_jitter_instances(jitter_sc, _sc_jitter_data);

                        for (int xx = 0; xx < MainWindow._physics_engine_instance_x; xx++)
                        {
                            for (int yy = 0; yy < MainWindow._physics_engine_instance_y; yy++)
                            {
                                for (int zz = 0; zz < MainWindow._physics_engine_instance_z; zz++)
                                {
                                    var indexer00 = xx + MainWindow._physics_engine_instance_x * (yy + MainWindow._physics_engine_instance_y * zz);


                                    //if (jitter_sc.Length > 0)
                                    //{
                                    //    Console.WriteLine("built00");
                                    //}
                                    //
                                    //Console.WriteLine("index: " + indexer00);
                                    jitter_sc[indexer00]._sc_create_jitter_world(_sc_jitter_data);


                                    for (int x = 0; x < MainWindow.world_width; x++)
                                    {
                                        for (int y = 0; y < MainWindow.world_height; y++)
                                        {
                                            for (int z = 0; z < MainWindow.world_depth; z++)
                                            {
                                                var indexer1 = x + MainWindow.world_width * (y + MainWindow.world_height * z);

                                                var world = jitter_sc[indexer00].return_world(indexer1);

                                                if (world == null)
                                                {
                                                    Console.WriteLine("null");
                                                }
                                                else
                                                {
                                                    //Console.WriteLine("!null");

                                                    sccsjittertasks[indexer00][indexer1]._world_data = new object[2];
                                                    sccsjittertasks[indexer00][indexer1]._work_index = -1;
                                                    sccsjittertasks[indexer00][indexer1]._world_data[0] = world;
                                                    //Console.WriteLine("index: " + indexer1);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        sccsjittertasks = init_update_variables(sccsjittertasks, MainWindow.config, MainWindow.consoleHandle, MainWindow.SC_GLOBALS_ACCESSORS.SC_CONSOLE_WRITER);
                    }
                    else if (MainWindow.usejitterphysics == 0)
                    {
                        sccsjittertasks = new scmessageobjectjitter[MainWindow._physics_engine_instance_x * MainWindow._physics_engine_instance_y * MainWindow._physics_engine_instance_z][];

                        for (int xx = 0; xx < MainWindow._physics_engine_instance_x; xx++)
                        {
                            for (int yy = 0; yy < MainWindow._physics_engine_instance_y; yy++)
                            {
                                for (int zz = 0; zz < MainWindow._physics_engine_instance_z; zz++)
                                {
                                    var indexer00 = xx + MainWindow._physics_engine_instance_x * (yy + MainWindow._physics_engine_instance_y * zz);
                                    //_jitter_physics[indexer00] = DoSpecialThing();
                                    sccsjittertasks[indexer00] = new scmessageobjectjitter[MainWindow.world_width * MainWindow.world_height * MainWindow.world_depth];

                                    for (int x = 0; x < MainWindow.world_width; x++)
                                    {
                                        for (int y = 0; y < MainWindow.world_height; y++)
                                        {
                                            for (int z = 0; z < MainWindow.world_depth; z++)
                                            {
                                                var indexer01 = x + MainWindow.world_width * (y + MainWindow.world_height * z);
                                                sccsjittertasks[indexer00][indexer01] = new scmessageobjectjitter();
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        sccsjittertasks = init_update_variables(sccsjittertasks, MainWindow.config, MainWindow.consoleHandle, MainWindow.SC_GLOBALS_ACCESSORS.SC_CONSOLE_WRITER);
                    }


                    //Console.WriteLine("built1");




                    //nine's alternative oculus touch arduino key mapping for broken oculus touch devices that are too pricey even when used and purchased online.
                    //https://instructables.com/C-Serial-Communication-With-Arduino/
                    //string currentSerialPortInput = serialPort.ReadExisting();
                    //Console.WriteLine(currentSerialPortInput);

                    if (MainWindow.useArduinoOVRTouchKeymapper == 1)
                    {
                        serialPort = new SerialPort();
                        serialPort.PortName = "COM3";
                        serialPort.BaudRate = 9600;
                        serialPort.DataBits = 8;
                        serialPort.StopBits = System.IO.Ports.StopBits.One;
                        serialPort.Parity = System.IO.Ports.Parity.None;
                        serialPort.Handshake = Handshake.None;
                        serialPort.Open();
                        if (serialPort­.IsOpen)
                        {
                            Console.WriteLine("0status: " + serialPort­.IsOpen);
                        }
                        else
                        {
                            Console.WriteLine("0status: " + "NOT OPENED");
                        }
                    }

                //serialPort.Open();

                //Console.WriteLine("1status: " + serialPort­.IsOpen);

                _thread_looper:

                    try
                    {

                        /*if (MainWindow.useArduinoOVRTouchKeymapper == -2)
                        {
                            if (serialPort != null)
                            {
                                string msgg = serialPort.ReadTo("#");
                                //Console.Write(msgg);


                                if (msgg!= "" && msgg!= null)
                                {
                                    sccsjittertasks[0][0]._world_data[1] = msgg;
                                }
           
                            }
                            else
                            {
                                sccsjittertasks[0][0]._world_data[1] = "";
                                MessageBox((IntPtr)0, "arduino device disconnected on COM3", "msg", 0);
                            }

                        }*/


                        if (MainWindow.usejitterphysics == 0)
                        {
                            sccsjittertasks = Update(null, sccsjittertasks);
                        }
                        else if (MainWindow.usejitterphysics == 1)
                        {
                            sccsjittertasks = Update(jitter_sc, sccsjittertasks);
                        }

                        //MainWindow.MessageBox((IntPtr)0, "0", "sc core systems message", 0);
                        //sccsjittertasks = Update(jitter_sc, sccsjittertasks);







                    }
                    catch (Exception ex)
                    {

                    }
                    Thread.Sleep(1);
                    goto _thread_looper;

                    //ShutDown();
                    //ShutDownGraphics();

                }, 0); //100000

                mainthreadupdate.IsBackground = true;
                mainthreadupdate.Priority = ThreadPriority.Lowest;
                mainthreadupdate.SetApartmentState(ApartmentState.STA);
                mainthreadupdate.Start();


            }
            catch
            {

            }
            /*finally
            {

            }*/
        }

        public void TurnOnAlphaBlending()
        {
            // Setup the blend factor.
            var blendFactor = new Color4(0, 0, 0, 0);

            // Turn on the alpha blending.
            DeviceContext.OutputMerger.SetBlendState(AlphaEnableBlendingState, blendFactor, -1);
        }

        public void TurnOffAlphaBlending()
        {
            // Setup the blend factor.
            var blendFactor = new Color4(0, 0, 0, 0);

            // Turn on the alpha blending.
            DeviceContext.OutputMerger.SetBlendState(AlphaDisableBlendingState, blendFactor, -1);
        }

        public void TurnZBufferOn()
        {
            DeviceContext.OutputMerger.SetDepthStencilState(DepthStencilState, 1);
        }

        public void TurnZBufferOff()
        {
            DeviceContext.OutputMerger.SetDepthStencilState(DepthDisabledStencilState, 1);
        }

        //ARDUINO DEBUG
        /*_sec_received_messages[18]._message = msgg;
        _sec_received_messages[18]._originalMsg = msgg;
        _sec_received_messages[18]._messageCut = msgg;
        _sec_received_messages[18]._specialMessage = 2;
        _sec_received_messages[18]._specialMessageLineX = 0;
        _sec_received_messages[18]._specialMessageLineY = 5;
        _sec_received_messages[18]._orilineX = 0;
        _sec_received_messages[18]._orilineY = 5;
        _sec_received_messages[18]._lineX = 0;
        _sec_received_messages[18]._lineY = 5;
        _sec_received_messages[18]._count = 0;
        _sec_received_messages[18]._swtch0 = 1;b
        _sec_received_messages[18]._swtch1 = 1;
        _sec_received_messages[18]._delay = 11;
        _sec_received_messages[18]._looping = 0;*/

        /*_sec_received_messages[5]._message = msgg;
        _sec_received_messages[5]._originalMsg = msgg;
        _sec_received_messages[5]._messageCut = msgg;
        _sec_received_messages[5]._specialMessage = 2;
        _sec_received_messages[5]._specialMessageLineX = 0;
        _sec_received_messages[5]._specialMessageLineY = 0;
        _sec_received_messages[5]._lineX = _initX0;
        _sec_received_messages[5]._lineY = _initY0;
        _sec_received_messages[5]._count = 0;
        _sec_received_messages[5]._swtch0 = 1;
        _sec_received_messages[5]._swtch1 = 0;
        _sec_received_messages[5]._delay = 0;
        _sec_received_messages[5]._looping = 0;*/
        //ARDUINO DEBUG




        //VOID EXPANSE DEBUG
        /*_sec_received_messages[18]._message = "existingdata: " + existingdata;
        _sec_received_messages[18]._originalMsg = "existingdata: " + existingdata;
        _sec_received_messages[18]._messageCut = "existingdata: " + existingdata;
        _sec_received_messages[18]._specialMessage = 2;
        _sec_received_messages[18]._specialMessageLineX = 0;
        _sec_received_messages[18]._specialMessageLineY = 0;
        _sec_received_messages[18]._orilineX = 1;// _sec_received_messages[18]._orilineX + msgVoidExpanseDisabled.Length + 1;
        _sec_received_messages[18]._orilineY = 13;// _sec_received_messages[18]._orilineY;
        _sec_received_messages[18]._lineX = 1;// _sec_received_messages[18]._orilineX + msgVoidExpanseDisabled.Length + 1;
        _sec_received_messages[18]._lineY = 13;//_sec_received_messages[18]._orilineY;
        _sec_received_messages[18]._count = 0;
        _sec_received_messages[18]._swtch0 = 1;
        _sec_received_messages[18]._swtch1 = 1;
        _sec_received_messages[18]._delay = 25;
        _sec_received_messages[18]._looping = 1;*/

        /*_sec_received_messages[18]._message = msgg;
        _sec_received_messages[18]._originalMsg = msgg;
        _sec_received_messages[18]._messageCut = msgg;
        _sec_received_messages[18]._specialMessage = 2;
        _sec_received_messages[18]._specialMessageLineX = 0;
        _sec_received_messages[18]._specialMessageLineY = 0;
        _sec_received_messages[18]._orilineX = 0;
        _sec_received_messages[18]._orilineY = 12;
        _sec_received_messages[18]._lineX = 0;
        _sec_received_messages[18]._lineY = 12;
        _sec_received_messages[18]._count = 0;
        _sec_received_messages[18]._swtch0 = 1;
        _sec_received_messages[18]._swtch1 = 1;
        _sec_received_messages[18]._delay = 11;
        _sec_received_messages[18]._looping = 1;*/
        //VOID EXPANSE DEBUG




        protected abstract scmessageobjectjitter[][] init_update_variables(scmessageobjectjitter[][] sccsjittertasks, SCCoreSystems.sc_core.sc_system_configuration configuration, IntPtr hwnd, sc_console.sc_console_writer _writer); //void Update();
        protected abstract scmessageobjectjitter[][] Update(jitter_sc[] jitter_sc, scmessageobjectjitter[][] sccsjittertasks); //void Update();
        protected abstract void ShutDownGraphics();


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



            AlphaEnableBlendingState?.Dispose();
            AlphaEnableBlendingState = null;
            AlphaDisableBlendingState?.Dispose();
            AlphaDisableBlendingState = null;
            DepthDisabledStencilState?.Dispose();
            DepthDisabledStencilState = null;
            //DepthStencilView?.Dispose();
            //DepthStencilView = null;
            DepthStencilState?.Dispose();
            DepthStencilState = null;
            DepthStencilBuffer?.Dispose();
            DepthStencilBuffer = null;


            if (mainthreadupdate != null)
            {
                //mainthreadupdate.Suspend();
                mainthreadupdate = null;
            }
            ShutDownGraphics();
        }






        public void WriteErrorDetails(OvrWrap OVR, Ab3d.OculusWrap.Result result, string message)
        {


            if (result >= Ab3d.OculusWrap.Result.Success)
                return;

            ErrorInfo errorInformation = OVR.GetLastErrorInfo();

            string formattedMessage = string.Format("{0}. \nMessage: {1} (Error code={2})", message, errorInformation.ErrorString, errorInformation.Result);

            //MainWindow.MessageBox((IntPtr)0, formattedMessage, "message", 0);


            //Trace.WriteLine(formattedMessage);
            //System.Windows.Forms.MessageBox.Show(formattedMessage, message);

            throw new Exception(formattedMessage);
        }
    }
}