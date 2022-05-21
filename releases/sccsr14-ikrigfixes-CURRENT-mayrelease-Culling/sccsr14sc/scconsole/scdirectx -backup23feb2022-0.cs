using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Runtime.InteropServices;
using DSystemConfiguration = sccs.sccore.scsystemconfiguration;
using Ab3d.OculusWrap;
//using Ab3d.DXEngine.OculusWrap;
using Ab3d.OculusWrap.DemoDX11;
using Result = Ab3d.OculusWrap.Result;


using System.Threading;
using System.Threading.Tasks;
using scmessageobject = sccs.scmessageobject.scmessageobject;
using scmessageobjectjitter = sccs.scmessageobject.scmessageobjectjitter;

using ISCCS_Jitter_Interface = Jitter.ISCCS_Jitter_Interface;
using Jitter;
using Jitter.LinearMath;
using System.Diagnostics;
using System.IO.Ports;

//using System.Windows.Media;
//using System.Windows.Interop;
using System.Windows;

using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/*
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;*/



//using SharpDX;
using SharpDX.DirectInput;
using System.Threading;
using System.Runtime;
using System.Runtime.CompilerServices;

using System.Runtime.InteropServices;

using sccs.scmessageobject;
using sccs.scconsole;

using sccs.scgraphics;
using System.ComponentModel;
using WindowsInput;

using sccs.sccore;

using Jitter.Dynamics;
using Jitter.DataStructures;
using Jitter;
using Jitter.Forces;
using Jitter.LinearMath;

using System.Diagnostics;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
//using System.Windows.Interop;

using Win32.Shared.Interop;
using Win32.Shared;
using Win32.Shared.Interfaces;

//using System.Windows.Controls;

using SharpDX.Windows;



namespace sccs.scgraphics
{
    public abstract class scdirectx : ISCCS_Jitter_Interface //abstract
    {
        int startmainthread = 0;
        int bitmapcounter = 0;
        int somecounter = 0;
        System.Drawing.Bitmap _bitmap;
        System.Drawing.Rectangle boundsRect;
        System.Drawing.Imaging.BitmapData bmpData;
        int _bytesTotal;
        Texture2DDescription _textureDescription;
        byte[] _textureByteArray;
        Texture2D _texture2D;
        public SwapChain1 swapChain1;

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        IntPtr vewindowsfoundedz;
        private readonly ICaptureMethod _captureMethod;
        private readonly string _title;


        //public DwmSharedSurface somesurf;


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
                }
                //instance = new jitter_sc();
                */
                return instance;
            }
        }



        public jitter_sc[] create_jitter_instances(jitter_sc[] sc_jitter_physics, sc_jitter_data _sc_jitter_data)
        {


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

        int canworkphysics = 0;
        int screencaptureresultswtc = 0;


        Thread mainthreadupdate;

        public SharpDX.Matrix OrthoMatrix { get; private set; }
        public SharpDX.Direct3D11.Device device { get; set; }


        //OCULUS RIFT
        public int SurfaceWidth;
        public int SurfaceHeight;
        public DateTime startTime;
        //public OculusWrapVirtualRealityProvider _oculusRiftVirtualRealityProvider;
        //public static Ab3d.DirectX.DXDevice _dxDevice;
        public RenderTargetView _renderTargetView;
        SharpDX.Direct3D11.Texture2D depthBuffer;
        private DepthStencilView _depthStencilView;
        protected DepthStencilView DepthStencilView => _depthStencilView;
        SharpDX.Direct3D11.DepthStencilState depthStencilState;

        public SharpDX.Direct3D11.DepthStencilState depthStencilStateTWOSIDEDDRAW;

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
        public SharpDX.Matrix ProjectionMatrix { get; private set; }
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


        public static scdirectx D3D;

        public DepthStencilState DepthDisabledStencilState { get; private set; }
        public BlendState AlphaEnableBlendingState { get; private set; }
        public BlendState AlphaDisableBlendingState { get; private set; }
        public DepthStencilState DepthStencilState { get; private set; }

        public static SharpDX.Matrix WorldMatrix;
        RasterizerState RasterstateCullBack;

        bool Rendering = false;

        //IntPtr windowHandle = new WindowInteropHelper(Application.Current.Program).Handle;

        //private readonly ICaptureMethod _captureMethod;
        //ICaptureMethod captureMethod;

        RenderForm form;
        protected scdirectx(ICaptureMethod captureMethod) //DSystemConfiguration configuration, IntPtr Hwnd, sc_console.SCCONSOLEWRITER _writer
        {
            _captureMethod = captureMethod;

            D3D = this;

            //Update();


            //somesurf = new DwmSharedSurface(Program);

            form = new RenderForm("sccsr14");
            /*form.Size = new System.Drawing.Size(1920, 1080);
            form.CreateControl();
            form.Activate();
            form.TransparencyKey = System.Drawing.Color.Black;*/
            //form.Hide();


            SC_Init_DirectX(); //configuration, Hwnd, _writer 


        }

        SwapChainDescription swapChainDesc;

        /*
        protected override sealed void Initialize()
        {
            InternalInitialize();

            Rendering = true;
            CompositionTarget.Rendering += OnCompositionTargetRendering;
        }

        protected override sealed void Uninitialize()
        {
            Rendering = false;
            CompositionTarget.Rendering -= OnCompositionTargetRendering;

            InternalUninitialize();
        }

        private void OnCompositionTargetRendering(object sender, EventArgs eventArgs)
        {
            //if (!Rendering)
            //    return;

            try
            {
                //BeginRender();
                //Render();
                //EndRender();
            }
            catch (SharpDXException e)
            {
                if (e.HResult == HResults.D2DERR_RECREATE_TARGET || e.HResult == HResults.DXGI_ERROR_DEVICE_REMOVED)
                {
                    Uninitialize();
                    Initialize();
                }
                else throw;
            }
        }*/

        //protected virtual void InternalInitialize()
        protected virtual void SC_Init_DirectX() //DSystemConfiguration configuration, IntPtr Hwnd, sc_console.SCCONSOLEWRITER _writer
        {


            //MessageBox((IntPtr)0, "initdirectx" , "scmsg", 0);

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

                if (Program._useOculusRift == 1)
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
                        swapChainDescription.OutputHandle = form.Handle;
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
                            OutputHandle = form.Handle,
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
                        depthBuffer = new Texture2D(device, depthBufferDescription);*/
                        //_depthStencilView = new DepthStencilView(device, depthBuffer);
                        //depthStencilState = new DepthStencilState(device, depthStencilStateDescription);

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
                        DepthStencilBuffer = new Texture2D(device, depthBufferDesc0);


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
                        depthStencilState = new DepthStencilState(device, depthStencilDesc);

                        // Set the depth stencil state.
                        device.ImmediateContext.OutputMerger.SetDepthStencilState(depthStencilState, 1);

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
                        _depthStencilView = new DepthStencilView(device, DepthStencilBuffer, depthStencilViewDesc);
                        
                        device.ImmediateContext.OutputMerger.SetRenderTargets(_depthStencilView, _renderTargetView);*/

                        var viewport = new Viewport(0, 0, hmdDesc.Resolution.Width, hmdDesc.Resolution.Height, 0.0f, 1.0f);








                        // Now set the rasterizer state.
                        //DeviceContext.Rasterizer.State = RasterState;
                        var rasterDesc = new RasterizerStateDescription()
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
                        RasterState = new RasterizerState(device, rasterDesc);

                        // Now set the rasterizer state.
                        device.ImmediateContext.Rasterizer.State = RasterState;




                        device.ImmediateContext.Rasterizer.SetViewport(viewport);




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
                                DepthStencilBuffer = new Texture2D(device, depthBufferDesc0);


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
                                depthStencilState = new DepthStencilState(device, depthStencilDesc);


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
                                RasterState = new RasterizerState(device, rasterDesc);

                                // Now set the rasterizer state.
                                //device.ImmediateContext.Rasterizer.State = RasterState;

                                //device.ImmediateContext.Rasterizer.SetViewport(viewport);

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
                                depthStencilState = new DepthStencilState(device, depthStencilDesc);

                                // Set the depth stencil state.
                                device.ImmediateContext.OutputMerger.SetDepthStencilState(depthStencilState, eyeIndex); //1*/


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
                            //eyeTexture.DepthBuffer = new Texture2D(device, eyeTexture.DepthBufferDescription);
                            //depthStencilDesc
                            //eyeTexture.DepthStencilView = new DepthStencilView(device, eyeTexture.DepthBuffer);




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
                            eyeTexture.DepthBuffer = new Texture2D(device, eyeTexture.DepthBufferDescription);


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
                            depthStencilState = new DepthStencilState(device, depthStencilDesc);

                            // Set the depth stencil state.
                            device.ImmediateContext.OutputMerger.SetDepthStencilState(depthStencilState, 1); //1*/



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
                            //_depthStencilView = new DepthStencilView(device, eyeTexture.DepthBuffer, depthStencilViewDesc);

                            eyeTexture.DepthStencilView = new DepthStencilView(device, eyeTexture.DepthBuffer);


                            device.ImmediateContext.OutputMerger.SetRenderTargets(eyeTexture.DepthStencilView, eyeTexture.RenderTargetViews);








                            //eyeTexture.DepthStencilView = new DepthStencilView(device, eyeTexture.DepthBuffer, depthStencilViewDesc);

                            //_depthStencilView = new DepthStencilView(device, DepthStencilBuffer, depthStencilViewDesc);




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
                        DepthStencilBuffer = new Texture2D(device, depthBufferDesc0);


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
                        depthStencilState = new DepthStencilState(device, depthStencilDesc);

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
                        RasterState = new RasterizerState(device, rasterDesc);

                        // Now set the rasterizer state.
                        //device.ImmediateContext.Rasterizer.State = RasterState;

                        //device.ImmediateContext.Rasterizer.SetViewport(viewport);

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
                        RasterState = new RasterizerState(device, rasterDesc);

                        // Now set the rasterizer state.
                        device.ImmediateContext.Rasterizer.State = RasterState;
                        DeviceContext.Rasterizer.State = RasterState;




                        DeviceContext.Rasterizer.SetViewport(viewport);*/
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
                        swapChainDescription.OutputHandle = form.Handle;
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
                           //factory.MakeWindowAssociation(Program.GameHandle, WindowAssociationFlags.IgnoreAll);

                           // Define the properties of the swap chain.
                           SwapChainDescription swapChainDescription = new SwapChainDescription();
                           swapChainDescription.BufferCount = 1;
                           swapChainDescription.IsWindowed = true;
                           swapChainDescription.OutputHandle = form.Handle;
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
                else //without oculus rift
                {
                    // Store the vsync setting.
                    //VerticalSyncEnabled = SC_skYaRk_VR_V007.DSystemConfiguration.DSystemConfiguration.VerticalSyncEnabled;

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
                    factory.Dispose();


                    /*// Initialize the swap chain description.
                    swapChainDesc = new SwapChainDescription()
                    {
                        // Set to a single back buffer.
                        BufferCount = 1,
                        // Set the width and height of the back buffer.
                        ModeDescription = new ModeDescription(SurfaceWidth, SurfaceHeight, rational, Format.R8G8B8A8_UNorm),
                        // Set the usage of the back buffer.
                        Usage = Usage.RenderTargetOutput,
                        // Set the handle for the window to render to.
                        OutputHandle = form.Handle,
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
                        BufferCount = 2,
                        Flags = SwapChainFlags.None,
                        IsWindowed = true,
                        ModeDescription = new ModeDescription(form.ClientSize.Width, form.ClientSize.Height, new Rational(60, 1), Format.B8G8R8A8_UNorm),
                        OutputHandle = form.Handle,
                        SampleDescription = new SampleDescription(1, 0),
                        SwapEffect = SwapEffect.Discard,
                        Usage = Usage.RenderTargetOutput
                    };


                    // Create the swap chain, Direct3D device, and Direct3D device context.

                    SwapChain someswap;
                    SharpDX.Direct3D11.Device somedevice;
                    SharpDX.Direct3D11.Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.BgraSupport, swapChainDesc, out somedevice, out someswap);

                    device = somedevice;
                    Device = device;
                    SwapChain = someswap;
                    //DeviceContext = Device.ImmediateContext;
                    DeviceContext = device.ImmediateContext;
                    // Get the pointer to the back buffer.
                    BackBuffer = Texture2D.FromSwapChain<Texture2D>(SwapChain, 0);

                    // Create the render target view with the back buffer pointer.
                    _renderTargetView = new RenderTargetView(device, BackBuffer);

                    // Release pointer to the back buffer as we no longer need it.
                    //BackBuffer.Dispose();

                    // Initialize and set up the description of the depth buffer.
                    Texture2DDescription depthBufferDesc1 = new Texture2DDescription()
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
                    DepthStencilBuffer = new Texture2D(device, depthBufferDesc1);

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
                    _depthStencilView = new DepthStencilView(Device, DepthStencilBuffer, depthStencilViewDesc);

                    // Bind the render target view and depth stencil buffer to the output render pipeline.
                    DeviceContext.OutputMerger.SetTargets(_depthStencilView, _renderTargetView);

                    // Setup the raster description which will determine how and what polygon will be drawn.
                    var rasterDesc = new RasterizerStateDescription()
                    {
                        IsAntialiasedLineEnabled = false,
                        CullMode = CullMode.Back,
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
                    RasterstateCullBack = new RasterizerState(device, rasterDesc);

                    // Now set the rasterizer state.
                    DeviceContext.Rasterizer.State = RasterstateCullBack;




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





                    // Setup and create the viewport for rendering.
                    DeviceContext.Rasterizer.SetViewport(0, 0, SurfaceWidth, SurfaceHeight, 0, 1);

                    // Setup and create the projection matrix.
                    ProjectionMatrix = SharpDX.Matrix.PerspectiveFovLH((float)(Math.PI / 4), ((float)SurfaceWidth / (float)SurfaceHeight), 0.1f, 100);

                    // Initialize the world matrix to the identity matrix.


                }

                WorldMatrix = SharpDX.Matrix.Identity;
            }
            catch
            {

            }




            /*
            // Initialize and set up the description of the stencil state.
            DepthStencilStateDescription depthStencilDesc2 = new DepthStencilStateDescription()
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
            depthStencilStateTWOSIDEDDRAW = new DepthStencilState(device, depthStencilDesc2);
            */












            /*

            // Initialize and set up the description of the depth buffer.
            var depthBufferDesc = new Texture2DDescription()
            {
                Width = Program.config.Width,
                Height = Program.config.Height,
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
            */



            /*

            // Initialize and set up the description of the stencil state.
            var depthStencilDesc1 = new DepthStencilStateDescription()
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
            DepthStencilState = new DepthStencilState(Device, depthStencilDesc1);
            */






            /*
            //STRAIGHT COPY PASTE FROM C# RASTERTEK DAN6040. ALL CREDITS TO HIM. WOW HE IS SUCH A GOOD SCRIPTER. I AM MISSING TIME.

            // Create an orthographic projection matrix for 2D rendering.
            OrthoMatrix = SharpDX.Matrix.OrthoLH(Program.config.Width, Program.config.Height, DSystemConfiguration.ScreenNear, DSystemConfiguration.ScreenDepth);



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

            */










            /*

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





            swapChain1 = SwapChain.QueryInterface<SwapChain1>();
            // ignore all Windows events
            var factoryy = SwapChain.GetParent<Factory>();
            factoryy.MakeWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAll);


            var isResized = false;
            form.UserResized += (_, __) => isResized = true;
            
            
            RenderLoop.Run(form, () =>
            {
                // ReSharper disable AccessToDisposedClosure
                if (!_captureMethod.IsCapturing)
                    _captureMethod.StartCapture(form.Handle, device, factoryy);

                if (isResized)
                {
                    Utilities.Dispose(ref BackBuffer);
                    Utilities.Dispose(ref _renderTargetView);

                    swapChain1.ResizeBuffers(swapChainDesc.BufferCount, form.ClientSize.Width, form.ClientSize.Height, Format.Unknown, SwapChainFlags.None);
                    BackBuffer = SharpDX.Direct3D11.Resource.FromSwapChain<Texture2D>(swapChain1, 0);
                    _renderTargetView = new RenderTargetView(device, BackBuffer);

                    device.ImmediateContext.Rasterizer.SetViewport(0, 0, form.ClientSize.Width, form.ClientSize.Height);
                    device.ImmediateContext.OutputMerger.SetTargets(_renderTargetView);

                    isResized = false;
                }

                // clear view
                //device.ImmediateContext.ClearRenderTargetView(_renderTargetView, SharpDX.Color.Black);// new SharpDX.RawColor4(1.0f, 1.0f, 1.0f, 1.0f));

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
                    //if (dataBox1.RowPitch == memoryBitmapStride)
                    //{
                    //    // Stride is the same
                    //    Marshal.Copy(interptr1, _textureByteArray, 0, _bytesTotal);
                    //}
                    //else
                    //{
                    //    // Stride not the same - copy line by line
                    //    for (int y = 0; y < rows; y++)
                    //    {
                    //        Marshal.Copy(interptr1 + y * dataBox1.RowPitch, _textureByteArray, y * memoryBitmapStride, memoryBitmapStride);
                    //    }
                    //}
                    var somebitmap = new System.Drawing.Bitmap(columns, rows, columns * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray,0));
                    somebitmap.Save(@"C:\Users\steve\Desktop\screenrecord\" + bitmapcounter + "_" + rows.ToString("00") + columns.ToString("00") + ".png");
                    bitmapcounter++;
                    device.ImmediateContext.UnmapSubresource(texture2d, 0);
                    DeleteObject(interptr1);
                    //DISCARDED
                    //DISCARDED
                    //DISCARDED
                    device.ImmediateContext.CopyResource(texture2d, _texture2D);

                    var shaderResourceView = new ShaderResourceView(device, _texture2D);
                    //device.ImmediateContext.PixelShader.SetShaderResource(0, shaderResourceView);
                }




                
                if (startmainthread == 0)
                {



                    mainthreadupdate = new Thread(() =>
                    {
                        if (Program.usejitterphysics == 1)
                        {
                            jitter_sc = new jitter_sc[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez];
                            sccsjittertasks = new scmessageobjectjitter[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][];

                            sc_jitter_data _sc_jitter_data = new sc_jitter_data();
                            _sc_jitter_data.alloweddeactivation = Program.allowdeactivation;
                            _sc_jitter_data.allowedpenetration = Program.worldallowedpenetration;
                            _sc_jitter_data.width = Program.worldwidth;
                            _sc_jitter_data.height = Program.worldheight;
                            _sc_jitter_data.depth = Program.worlddepth;
                            _sc_jitter_data.gravity = Program._world_gravity;
                            _sc_jitter_data.smalliterations = Program.worldsmalliterations;
                            _sc_jitter_data.iterations = Program.worlditerations;


                            for (int xx = 0; xx < Program.physicsengineinstancex; xx++)
                            {
                                for (int yy = 0; yy < Program.physicsengineinstancey; yy++)
                                {
                                    for (int zz = 0; zz < Program.physicsengineinstancez; zz++)
                                    {
                                        var indexer00 = xx + Program.physicsengineinstancex * (yy + Program.physicsengineinstancey * zz);
                                        //_jitter_physics[indexer00] = DoSpecialThing();
                                        sccsjittertasks[indexer00] = new scmessageobjectjitter[Program.worldwidth * Program.worldheight * Program.worlddepth];

                                        for (int x = 0; x < Program.worldwidth; x++)
                                        {
                                            for (int y = 0; y < Program.worldheight; y++)
                                            {
                                                for (int z = 0; z < Program.worlddepth; z++)
                                                {
                                                    var indexer01 = x + Program.worldwidth * (y + Program.worldheight * z);
                                                    sccsjittertasks[indexer00][indexer01] = new scmessageobjectjitter();
                                                }
                                            }
                                        }
                                    }
                                }
                            }


                            //Console.WriteLine("built0");
                            //jitter_sc = create_jitter_instances(jitter_sc, _sc_jitter_data);

                            for (int xx = 0; xx < Program.physicsengineinstancex; xx++)
                            {
                                for (int yy = 0; yy < Program.physicsengineinstancey; yy++)
                                {
                                    for (int zz = 0; zz < Program.physicsengineinstancez; zz++)
                                    {
                                        var indexer00 = xx + Program.physicsengineinstancex * (yy + Program.physicsengineinstancey * zz);


                                        //if (jitter_sc.Length > 0)
                                        //{
                                        //    Console.WriteLine("built00");
                                        //}
                                        //
                                        //Console.WriteLine("index: " + indexer00);
                                        jitter_sc[indexer00]._sc_create_jitter_world(_sc_jitter_data);


                                        for (int x = 0; x < Program.worldwidth; x++)
                                        {
                                            for (int y = 0; y < Program.worldheight; y++)
                                            {
                                                for (int z = 0; z < Program.worlddepth; z++)
                                                {
                                                    var indexer1 = x + Program.worldwidth * (y + Program.worldheight * z);

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
                            //, form.Handle
                            sccsjittertasks = init_update_variables(sccsjittertasks, Program.config); //, Program.SCGLOBALSACCESSORS.SCCONSOLEWRITER
                        }
                        else if (Program.usejitterphysics == 0)
                        {
                            sccsjittertasks = new scmessageobjectjitter[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][];

                            for (int xx = 0; xx < Program.physicsengineinstancex; xx++)
                            {
                                for (int yy = 0; yy < Program.physicsengineinstancey; yy++)
                                {
                                    for (int zz = 0; zz < Program.physicsengineinstancez; zz++)
                                    {
                                        var indexer00 = xx + Program.physicsengineinstancex * (yy + Program.physicsengineinstancey * zz);
                                        //_jitter_physics[indexer00] = DoSpecialThing();
                                        sccsjittertasks[indexer00] = new scmessageobjectjitter[Program.worldwidth * Program.worldheight * Program.worlddepth];

                                        for (int x = 0; x < Program.worldwidth; x++)
                                        {
                                            for (int y = 0; y < Program.worldheight; y++)
                                            {
                                                for (int z = 0; z < Program.worlddepth; z++)
                                                {
                                                    var indexer01 = x + Program.worldwidth * (y + Program.worldheight * z);
                                                    sccsjittertasks[indexer00][indexer01] = new scmessageobjectjitter();
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            //, form.Handle
                            sccsjittertasks = init_update_variables(sccsjittertasks, Program.config); //, Program.SCGLOBALSACCESSORS.SCCONSOLEWRITER

                        }

                        sccsjittertasks[0][0].device = Device;


                    //var factory = SwapChain.GetParent<Factory>();
                    //factory.MakeWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAll);
                    //var swapChain1 = SwapChain.QueryInterface<SwapChain1>();





                    _thread_looper:

                        canworkphysics = 1;

                        
                        if (Program.exitedprogram != 1)
                        {
                            if (Program.usejitterphysics == 0)
                            {
                                // Clear the depth buffer.
                                //DeviceContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth, 1, 0);
                                // Clear the back buffer.
                                //DeviceContext.ClearRenderTargetView(_renderTargetView, givenColour);


                                sccsjittertasks = Update(null, sccsjittertasks);
                            }
                            else if (Program.usejitterphysics == 1)
                            {
                                // Clear the depth buffer.
                                //DeviceContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth, 1, 0);
                                // Clear the back buffer.
                                //DeviceContext.ClearRenderTargetView(_renderTargetView, givenColour);


                                sccsjittertasks = Update(jitter_sc, sccsjittertasks);
                            }

                        }


                        //GC.Collect();
                        Thread.Sleep(1);
                        goto _thread_looper;
                        //Program.MessageBox((IntPtr)0, "program quit0", "sc core systems message", 0);
                        //ShutDown();
                        //ShutDownGraphics();

                    }, 0); //100000

                    //Process.GetCurrentProcess().Exited

                    mainthreadupdate.IsBackground = true;
                    mainthreadupdate.Priority = ThreadPriority.Lowest;
                    mainthreadupdate.SetApartmentState(ApartmentState.STA);
                    mainthreadupdate.Start();



                    startmainthread = 1;
                }











                // draw it
                //device.ImmediateContext.Draw(4, 0);
                //swapChain1.Present(1, PresentFlags.None, new PresentParameters());

                // ReSharper restore AccessToDisposedClosure
            }); 












 






            try
            {

                //sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, Device);




                /*RenderLoop.Run(form, () =>
                {
                    // Present!

                });

            RenderForm form = new RenderForm("SharpDX - MiniCube Direct3D11 Sample");

            form.CreateControl();
            form.Activate();*/





















                /*

                var backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += (object sender, DoWorkEventArgs args) =>
                {
                    if (Program.usejitterphysics == 1)
                    {
                        jitter_sc = new jitter_sc[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez];
                        sccsjittertasks = new scmessageobjectjitter[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][];

                        sc_jitter_data _sc_jitter_data = new sc_jitter_data();
                        _sc_jitter_data.alloweddeactivation = Program.allowdeactivation;
                        _sc_jitter_data.allowedpenetration = Program.worldallowedpenetration;
                        _sc_jitter_data.width = Program.worldwidth;
                        _sc_jitter_data.height = Program.worldheight;
                        _sc_jitter_data.depth = Program.worlddepth;
                        _sc_jitter_data.gravity = Program._world_gravity;
                        _sc_jitter_data.smalliterations = Program.worldsmalliterations;
                        _sc_jitter_data.iterations = Program.worlditerations;


                        for (int xx = 0; xx < Program.physicsengineinstancex; xx++)
                        {
                            for (int yy = 0; yy < Program.physicsengineinstancey; yy++)
                            {
                                for (int zz = 0; zz < Program.physicsengineinstancez; zz++)
                                {
                                    var indexer00 = xx + Program.physicsengineinstancex * (yy + Program.physicsengineinstancey * zz);
                                    //_jitter_physics[indexer00] = DoSpecialThing();
                                    sccsjittertasks[indexer00] = new scmessageobjectjitter[Program.worldwidth * Program.worldheight * Program.worlddepth];

                                    for (int x = 0; x < Program.worldwidth; x++)
                                    {
                                        for (int y = 0; y < Program.worldheight; y++)
                                        {
                                            for (int z = 0; z < Program.worlddepth; z++)
                                            {
                                                var indexer01 = x + Program.worldwidth * (y + Program.worldheight * z);
                                                sccsjittertasks[indexer00][indexer01] = new scmessageobjectjitter();
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        //Console.WriteLine("built0");
                        jitter_sc = create_jitter_instances(jitter_sc, _sc_jitter_data);

                        for (int xx = 0; xx < Program.physicsengineinstancex; xx++)
                        {
                            for (int yy = 0; yy < Program.physicsengineinstancey; yy++)
                            {
                                for (int zz = 0; zz < Program.physicsengineinstancez; zz++)
                                {
                                    var indexer00 = xx + Program.physicsengineinstancex * (yy + Program.physicsengineinstancey * zz);

                
                                    //if (jitter_sc.Length > 0)
                                    //{
                                    //    Console.WriteLine("built00");
                                    //}
                                    //
                                    //Console.WriteLine("index: " + indexer00);
                                    jitter_sc[indexer00]._sc_create_jitter_world(_sc_jitter_data);


                                    for (int x = 0; x < Program.worldwidth; x++)
                                    {
                                        for (int y = 0; y < Program.worldheight; y++)
                                        {
                                            for (int z = 0; z < Program.worlddepth; z++)
                                            {
                                                var indexer1 = x + Program.worldwidth * (y + Program.worldheight * z);

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

                        sccsjittertasks = init_update_variables(sccsjittertasks, Program.config, form.Handle); //, Program.SCGLOBALSACCESSORS.SCCONSOLEWRITER
                    }
                    else if (Program.usejitterphysics == 0)
                    {
                        sccsjittertasks = new scmessageobjectjitter[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][];

                        for (int xx = 0; xx < Program.physicsengineinstancex; xx++)
                        {
                            for (int yy = 0; yy < Program.physicsengineinstancey; yy++)
                            {
                                for (int zz = 0; zz < Program.physicsengineinstancez; zz++)
                                {
                                    var indexer00 = xx + Program.physicsengineinstancex * (yy + Program.physicsengineinstancey * zz);
                                    //_jitter_physics[indexer00] = DoSpecialThing();
                                    sccsjittertasks[indexer00] = new scmessageobjectjitter[Program.worldwidth * Program.worldheight * Program.worlddepth];

                                    for (int x = 0; x < Program.worldwidth; x++)
                                    {
                                        for (int y = 0; y < Program.worldheight; y++)
                                        {
                                            for (int z = 0; z < Program.worlddepth; z++)
                                            {
                                                var indexer01 = x + Program.worldwidth * (y + Program.worldheight * z);
                                                sccsjittertasks[indexer00][indexer01] = new scmessageobjectjitter();
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        sccsjittertasks = init_update_variables(sccsjittertasks, Program.config, form.Handle); //, Program.SCGLOBALSACCESSORS.SCCONSOLEWRITER

                    }

                    sccsjittertasks[0][0].device = Device;

                _thread_looper:

                    if (Program.exitedprogram != 1)
                    {
                        if (Program.usejitterphysics == 0)
                        {
                            // Clear the depth buffer.
                            //DeviceContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth, 1, 0);
                            // Clear the back buffer.
                            //DeviceContext.ClearRenderTargetView(RenderTargetView, givenColour);


                            sccsjittertasks = Update(null, sccsjittertasks);
                        }
                        else if (Program.usejitterphysics == 1)
                        {
                            // Clear the depth buffer.
                            //DeviceContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth, 1, 0);
                            // Clear the back buffer.
                            //DeviceContext.ClearRenderTargetView(RenderTargetView, givenColour);


                            sccsjittertasks = Update(jitter_sc, sccsjittertasks);
                        }
                        Thread.Sleep(1);
                        goto _thread_looper;

                    }
                    else
                    {
                        Thread.Sleep(1);
                        goto _thread_looper;
                    }
                };

                backgroundWorker.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args)
                {

                };

                backgroundWorker.RunWorkerAsync();*/















                if (startmainthread == 1)
                {


                    /*
                    mainthreadupdate = new Thread(() =>
                    {
                        if (Program.usejitterphysics == 1)
                        {
                            jitter_sc = new jitter_sc[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez];
                            sccsjittertasks = new scmessageobjectjitter[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][];

                            sc_jitter_data _sc_jitter_data = new sc_jitter_data();
                            _sc_jitter_data.alloweddeactivation = Program.allowdeactivation;
                            _sc_jitter_data.allowedpenetration = Program.worldallowedpenetration;
                            _sc_jitter_data.width = Program.worldwidth;
                            _sc_jitter_data.height = Program.worldheight;
                            _sc_jitter_data.depth = Program.worlddepth;
                            _sc_jitter_data.gravity = Program._world_gravity;
                            _sc_jitter_data.smalliterations = Program.worldsmalliterations;
                            _sc_jitter_data.iterations = Program.worlditerations;


                            for (int xx = 0; xx < Program.physicsengineinstancex; xx++)
                            {
                                for (int yy = 0; yy < Program.physicsengineinstancey; yy++)
                                {
                                    for (int zz = 0; zz < Program.physicsengineinstancez; zz++)
                                    {
                                        var indexer00 = xx + Program.physicsengineinstancex * (yy + Program.physicsengineinstancey * zz);
                                        //_jitter_physics[indexer00] = DoSpecialThing();
                                        sccsjittertasks[indexer00] = new scmessageobjectjitter[Program.worldwidth * Program.worldheight * Program.worlddepth];

                                        for (int x = 0; x < Program.worldwidth; x++)
                                        {
                                            for (int y = 0; y < Program.worldheight; y++)
                                            {
                                                for (int z = 0; z < Program.worlddepth; z++)
                                                {
                                                    var indexer01 = x + Program.worldwidth * (y + Program.worldheight * z);
                                                    sccsjittertasks[indexer00][indexer01] = new scmessageobjectjitter();
                                                }
                                            }
                                        }
                                    }
                                }
                            }


                            //Console.WriteLine("built0");
                            jitter_sc = create_jitter_instances(jitter_sc, _sc_jitter_data);

                            for (int xx = 0; xx < Program.physicsengineinstancex; xx++)
                            {
                                for (int yy = 0; yy < Program.physicsengineinstancey; yy++)
                                {
                                    for (int zz = 0; zz < Program.physicsengineinstancez; zz++)
                                    {
                                        var indexer00 = xx + Program.physicsengineinstancex * (yy + Program.physicsengineinstancey * zz);


                                        //if (jitter_sc.Length > 0)
                                        //{
                                        //    Console.WriteLine("built00");
                                        //}
                                        //
                                        //Console.WriteLine("index: " + indexer00);
                                        jitter_sc[indexer00]._sc_create_jitter_world(_sc_jitter_data);


                                        for (int x = 0; x < Program.worldwidth; x++)
                                        {
                                            for (int y = 0; y < Program.worldheight; y++)
                                            {
                                                for (int z = 0; z < Program.worlddepth; z++)
                                                {
                                                    var indexer1 = x + Program.worldwidth * (y + Program.worldheight * z);

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

                            sccsjittertasks = init_update_variables(sccsjittertasks, Program.config, form.Handle); //, Program.SCGLOBALSACCESSORS.SCCONSOLEWRITER
                        }
                        else if (Program.usejitterphysics == 0)
                        {
                            sccsjittertasks = new scmessageobjectjitter[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][];

                            for (int xx = 0; xx < Program.physicsengineinstancex; xx++)
                            {
                                for (int yy = 0; yy < Program.physicsengineinstancey; yy++)
                                {
                                    for (int zz = 0; zz < Program.physicsengineinstancez; zz++)
                                    {
                                        var indexer00 = xx + Program.physicsengineinstancex * (yy + Program.physicsengineinstancey * zz);
                                        //_jitter_physics[indexer00] = DoSpecialThing();
                                        sccsjittertasks[indexer00] = new scmessageobjectjitter[Program.worldwidth * Program.worldheight * Program.worlddepth];

                                        for (int x = 0; x < Program.worldwidth; x++)
                                        {
                                            for (int y = 0; y < Program.worldheight; y++)
                                            {
                                                for (int z = 0; z < Program.worlddepth; z++)
                                                {
                                                    var indexer01 = x + Program.worldwidth * (y + Program.worldheight * z);
                                                    sccsjittertasks[indexer00][indexer01] = new scmessageobjectjitter();
                                                }
                                            }
                                        }
                                    }
                                }
                            }


                            sccsjittertasks = init_update_variables(sccsjittertasks, Program.config, form.Handle); //, Program.SCGLOBALSACCESSORS.SCCONSOLEWRITER

                        }

                        sccsjittertasks[0][0].device = Device;

                    
                    //var factory = SwapChain.GetParent<Factory>();
                    //factory.MakeWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAll);
                    //var swapChain1 = SwapChain.QueryInterface<SwapChain1>();





                    _thread_looper:


                        // ReSharper disable AccessToDisposedClosure
                        //if (!_captureMethod.IsCapturing)
                        //    _captureMethod.StartCapture(form.Handle, device, factory);

                        /*if (isResized)
                        {
                            Utilities.Dispose(ref BackBuffer);
                            Utilities.Dispose(ref _renderTargetView);

                            swapChain1.ResizeBuffers(swapChainDesc.BufferCount, Program.Width, Program.Height, Format.Unknown, SwapChainFlags.None);
                            backBuffer = Resource.FromSwapChain<Texture2D>(swapChain1, 0);
                            renderView = new RenderTargetView(device, backBuffer);

                            device.ImmediateContext.Rasterizer.SetViewport(0, 0, form.ClientSize.Width, form.ClientSize.Height);
                            device.ImmediateContext.OutputMerger.SetTargets(renderView);

                            isResized = false;
                        }*/

                        // clear view
                        /*device.ImmediateContext.ClearRenderTargetView(renderView, new RawColor4(1.0f, 1.0f, 1.0f, 1.0f));

                        using var texture2d = _captureMethod.TryGetNextFrameAsTexture2D(device);
                        if (texture2d != null)
                        {
                            using var shaderResourceView = new ShaderResourceView(device, texture2d);
                            device.ImmediateContext.PixelShader.SetShaderResource(0, shaderResourceView);
                        }

                        // draw it
                        device.ImmediateContext.Draw(4, 0);
                        swapChain1.Present(1, PresentFlags.None, new PresentParameters());*/

                        // ReSharper restore AccessToDisposedClosure




                        /*if (canworkphysics == 1)
                        {
                            for (int i = 0; i < 3;)
                            {
                                screencaptureresultswtc = 0;
                                try
                                {
                                    screencaptureframe = sharpdxscreencapture.ScreenCapture(10000);
                                }
                                catch (Exception ex)
                                {
                                    sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, Device);
                                    screencaptureresultswtc = 1;
                                }
                                i++;
                                if (screencaptureresultswtc == 0)
                                {
                                    break;
                                }
                            }
                        }

                        canworkphysics = 1;


                        if (Program.exitedprogram != 1)
                        {
                            if (Program.usejitterphysics == 0)
                            {
                                // Clear the depth buffer.
                                //DeviceContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth, 1, 0);
                                // Clear the back buffer.
                                //DeviceContext.ClearRenderTargetView(_renderTargetView, givenColour);


                                sccsjittertasks = Update(null, sccsjittertasks);
                            }
                            else if (Program.usejitterphysics == 1)
                            {
                                // Clear the depth buffer.
                                //DeviceContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth, 1, 0);
                                // Clear the back buffer.
                                //DeviceContext.ClearRenderTargetView(_renderTargetView, givenColour);


                                sccsjittertasks = Update(jitter_sc, sccsjittertasks);
                            }

                        }


                        //GC.Collect();
                        Thread.Sleep(0);
                        goto _thread_looper;
                        //Program.MessageBox((IntPtr)0, "program quit0", "sc core systems message", 0);
                        //ShutDown();
                        //ShutDownGraphics();

                    }, 0); //100000

                    //Process.GetCurrentProcess().Exited

                    mainthreadupdate.IsBackground = true;
                    mainthreadupdate.Priority = ThreadPriority.Lowest;
                    mainthreadupdate.SetApartmentState(ApartmentState.STA);
                    mainthreadupdate.Start();*/




                }





















            }
            catch
            {

            }
            finally
            {
                //Program.MessageBox((IntPtr)0, "program quit", "sc core systems message", 0);
            }
        }

        //sccssharpdxscreencapture sharpdxscreencapture;// = new sccssharpdxscreencapture(0, 0, Device);
        //sccsscreenframe screencaptureframe = new sccsscreenframe();








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

        public void SetBackBufferRenderTarget()
        {
            // Bind the render target view and depth stencil buffer to the output render pipeline.
            DeviceContext.OutputMerger.SetRenderTargets(DepthStencilView, _renderTargetView);
        }



        //, scconsole.scconsolewriter _writer
        protected abstract scmessageobjectjitter[][] init_update_variables(scmessageobjectjitter[][] sccsjittertasks, sccs.sccore.scsystemconfiguration configuration); //void Update();
        protected abstract scmessageobjectjitter[][] Update(jitter_sc[] jitter_sc, scmessageobjectjitter[][] sccsjittertasks); //void Update();
        protected abstract void ShutDownGraphics();
        

        public void ShutDown()
        {
            Program.MessageBox((IntPtr)0, "program quit", "sc core systems message", 0);
            scupdate.stopovr = -1;
            //ShutDownGraphics();
            OVR.Destroy(sessionPtr);
            if (OVR != null)
            {
                OVR = null;
            }


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