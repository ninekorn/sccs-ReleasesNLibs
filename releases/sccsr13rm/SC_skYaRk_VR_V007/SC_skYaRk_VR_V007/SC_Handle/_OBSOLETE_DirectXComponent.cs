using System;
using System.Windows;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device = SharpDX.Direct3D11.Device;
using Resource = SharpDX.Direct3D11.Resource;

using SharpDX.DirectInput;

using System.Reflection;

using System.Diagnostics;

using System.IO;


using System.ComponentModel;
using System.Threading;

using Ab3d.OculusWrap;
//using SC_WPF_RENDER.SC_Graphics.SC_DX11;
using ovrSession = System.IntPtr;
using ovrTextureSwapChain = System.IntPtr;
using ovrMirrorTexture = System.IntPtr;
using Result = Ab3d.OculusWrap.Result;

using System.Collections.Generic;
using System.Collections;
using System.Linq;

using Matrix = SharpDX.Matrix;

using System.Threading.Tasks;

using System.Runtime.InteropServices;

using SharpDX.D3DCompiler;
using Buffer = SharpDX.Direct3D11.Buffer;

using Ab3d.DXEngine.OculusWrap;

using Ab3d.OculusWrap.DemoDX11;



namespace SC_WPF_RENDER
{
    /// <summary>
    /// Create SharpDx swapchain hosted in the controls parent Hwnd
    /// Resources created on Loaded, destroyed on Unloaded. 
    /// </summary>
    public abstract class DirectXComponent : _OBSOLETE_Win32HwndControl
    {
        public OculusWrapVirtualRealityProvider _oculusRiftVirtualRealityProvider;
        public static Ab3d.DirectX.DXDevice _dxDevice;
        public int RenderAt90Fps = 1;

        public static Device _device;
        private SwapChain _swapChain;
        public static Texture2D _backBuffer;
        private RenderTargetView _renderTargetView;

        protected Device Device => _device;
        protected SwapChain SwapChain => _swapChain;
        protected Texture2D BackBuffer => _backBuffer;
        protected RenderTargetView RenderTargetView => _renderTargetView;
        SharpDX.Direct3D11.DepthStencilState depthStencilState;
        protected int SurfaceWidth { get; private set; }
        protected int SurfaceHeight { get; private set; }

        public bool Rendering { get; private set; }

        public static SharpDX.Direct3D11.DeviceContext _context;

        private static DepthStencilView _depthStencilView;

        protected static DepthStencilView DepthStencilView => _depthStencilView;

        SharpDX.Direct3D11.Texture2D depthBuffer;
        private static Texture2DDescription _depthBufferDesc;
        protected static Texture2DDescription DepthBufferDesc => _depthBufferDesc;

        private static Texture2D _depthStencilBuffer;
        protected static Texture2D DepthStencilBuffer => _depthStencilBuffer;

        private bool VerticalSyncEnabled { get; set; }
        public int VideoCardMemory { get; private set; }
        public string VideoCardDescription { get; private set; }
        public static SharpDX.Direct3D11.DeviceContext DeviceContext => _deviceContext;
        private static RasterizerState RasterState { get; set; }


        public static SharpDX.Matrix _projectionMatrix;
        public static SharpDX.Matrix ProjectionMatrix => _projectionMatrix;

        public static SharpDX.Matrix _worldMatrix;
        public static SharpDX.Matrix WorldMatrix => _worldMatrix;

        //public static SharpDX.Matrix WorldMatrix { get; set; }



        public static ViewportF ViewPort { get; set; }
        static SC_skYaRk_VR_Edition_v005.DSystemConfiguration.DSystemConfiguration _DSystemConfiguration;

        public static SwapChainDescription swapChainDesc { get; set; }

        public static SC_skYaRk_VR_Edition_v005.DCamera Camera;// { get; set; }

        public static SharpDX.DirectInput.Keyboard keyboard;// { get; set; }

        public static bool _hasInit = false;

        public static SharpDX.Direct3D11.DeviceContext _deviceContext;


        public static Vector3 originalCameraPos;

        //--------------------OCULUS
        public static IntPtr sessionPtr;
        public static OvrWrap OVR;
        public static HmdDesc hmdDesc;
        public static Result result;
        Guid textureInterfaceId = new Guid("6f15aaf2-d208-4e89-9ab4-489535d34f9c"); // Interface ID of the Direct3D Texture2D interface.
        public static EyeTexture[] eyeTextures = null;


        public static SharpDX.Direct3D11.Texture2D mirrorTextureD3D = null;
        MirrorTexture mirrorTexture = null;
        public static LayerEyeFov layerEyeFov;

        SharpDX.Direct3D11.Buffer contantBuffer = null;
        SharpDX.Direct3D11.Buffer vertexBuffer = null;
        VertexShader vertexShader = null;
        PixelShader pixelShader = null;
        //Factory factory = null;
        InputLayout inputLayout = null;
        public static ControllerType controllerTypeRTouch;
        public static ControllerType controllerTypeLTouch;
        public static Ab3d.OculusWrap.InputState inputStateLTouch;
        public static ControllerType headset;

        public static int _vertexCount = 8;

        public static Ab3d.OculusWrap.InputState inputStateRTouch;

        DateTime startTime;

        public static  SharpDX.Matrix rotatingMatrix = SharpDX.Matrix.Identity;

        public static SharpDX.Vector3 originPos = new SharpDX.Vector3(0, 1.25f, 0);
        public static SharpDX.Matrix originRot = SharpDX.Matrix.Identity;

        public static float RotationY { get; set; }
        public static float RotationX { get; set; }
        public static float RotationZ { get; set; }

        public static Stopwatch _StartWatch = new Stopwatch();

        public static long totalTicks;

        //public SC_VR_Chunk.DInstanceType[] instances { get; set; }

        //public SC_VR_Chunk[] arrayOfChunks;

        public static Device device;

        public static DeviceContext context;

        public static SharpDX.DirectInput.Keyboard _Keyboard;

        Vector3 VRPos = new Vector3(0, 0, 0);

        public VertexShader VertexShader;
        public PixelShader PixelShader;

        public InputLayout Layout;
        //SC_ThreadPool threadPool;
        public Matrix _viewMatrix;
        public Matrix _WorldMatrix = Matrix.Identity;
        public DMatrixBuffer[] arrayOfMatrixBuff = new DMatrixBuffer[1];
        //public SC_VR_Chunk_Shader shaderOfChunk;

        public DLightBuffer[] lightBuffer = new DLightBuffer[1];

        RasterizerState rasterState;
        DepthStencilState depthState;
        SamplerState samplerState;
        BlendState blendState;


        [StructLayout(LayoutKind.Explicit)]
        public struct DMatrixBuffer
        {
            [FieldOffset(0)]
            public Matrix world;
            [FieldOffset(64)]
            public Matrix view;
            [FieldOffset(128)]
            public Matrix proj;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct DLightBuffer
        {
            [FieldOffset(0)]
            public Vector4 ambientColor;
            [FieldOffset(16)]
            public Vector4 diffuseColor;
            [FieldOffset(32)]
            public Vector3 lightDirection;
            [FieldOffset(44)]
            public float padding; // Added extra padding so structure is a multiple of 16.
        }
        /*public class chunkData
        {
            public SC_VR_Chunk.DInstanceType[] arrayOfInstance;
            public SharpDX.Matrix worldMatrix;
            public Matrix viewMatrix;
            public Matrix projectionMatrix;
            public SC_VR_Chunk_Shader chunkShader;
            public DMatrixBuffer[] matrixBuffer;
            public DLightBuffer[] lightBuffer;
            public int switchForRender;
            public SC_VR_Chunk.DInstanceType[] instancesIndex;
            public SC_VR_Chunk.DInstanceType[] arrayOfDeVectorMapTemp;
            public SC_VR_Chunk.DInstanceTypeTwo[] arrayOfDeVectorMapTempTwo;
            public SharpDX.Direct3D11.Buffer instanceBuffer;
            public SharpDX.Direct3D11.Buffer constantLightBuffer;
            public SharpDX.Direct3D11.Buffer vertexBuffer;
            public SharpDX.Direct3D11.Buffer constantMatrixPosBuffer;
            public int[][] arrayOfSomeMap;
            public SharpDX.Direct3D11.Buffer mapBuffer;
        }*/
        static void RotateY(float axis)
        {
            RotationY += (float)Math.PI * 0.001f * axis;
            if (RotationY > 360)
                RotationY -= 360;
        }


        static void RotateX(float axis)
        {
            RotationX += (float)Math.PI * 0.001f * axis;
            if (RotationX > 360)
                RotationX -= 360;
        }

        static void RotateZ(float axis)
        {
            RotationZ += (float)Math.PI * 0.001f * axis;
            if (RotationZ > 360)
                RotationZ -= 360;
        }


        //public static World _world;


        protected DirectXComponent()
        {

        }

        protected override sealed void Initialize()
        {
            InternalInitialize();

            Rendering = true;
            //CompositionTarget.Rendering += OnCompositionTargetRendering;
        }

        protected override sealed void Uninitialize()
        {
            Rendering = false;
            //CompositionTarget.Rendering -= OnCompositionTargetRendering;

            InternalUninitialize();
        }

        protected sealed override void Resized()
        {
            //InternalUninitialize();
            //InternalInitialize();
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
        }

        /*private double GetDpiScale()
        {
            PresentationSource source = PresentationSource.FromVisual(this);

            return source.CompositionTarget.TransformToDevice.M11;
        }*/



        static bool _useOculusRift = true;

        /// <summary>
        /// Create required DirectX resources.
        /// Derived calls should begin with base.InternalInitialize()
        /// </summary>
        protected virtual void InternalInitialize()
        {
            startTime = DateTime.Now;
            //SurfaceWidth = (int)(ActualWidth < 0 ? 0 : Math.Ceiling(ActualWidth * dpiScale));
            //SurfaceHeight = (int)(ActualHeight < 0 ? 0 : Math.Ceiling(ActualHeight * dpiScale));


            //_DSystemConfiguration = new SC_skYaRk_VR_Edition_v005.DSystemConfiguration.DSystemConfiguration("test", (int)System.Windows.SystemParameters.PrimaryScreenWidth, (int)System.Windows.SystemParameters.PrimaryScreenHeight, false, false);


            SurfaceWidth = 1920;
            SurfaceHeight = 1080;

            // Store the vsync setting.
            //VerticalSyncEnabled = _DSystemConfiguration.VerticalSyncEnabled;


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
            swapChainDesc.ModeDescription.RefreshRate.Denominator = 1;




            SharpDX.Direct3D11.Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, swapChainDesc, out _device, out _swapChain); // none
            */

            if (_useOculusRift)
            {

                controllerTypeRTouch = ControllerType.RTouch;
                controllerTypeLTouch = ControllerType.LTouch;

                //---------------------------------------------------------\\ OCULUS WRAP
                // Create Oculus OVR Wrapper
                OVR = OvrWrap.Create(); //Ab3d.DXEngine.OculusWrap    

                // Check if OVR service is running
                var detectResult = OVR.Detect(0);

                if (!detectResult.IsOculusServiceRunning)
                {
                    ////System.Windows.MessageBox.Show("Oculus service is not running", "Oculus error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    ////System.Windows.MessageBox.Show("Oculus service is running", "Oculus Message", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                // Check if Head Mounter Display is connected
                if (!detectResult.IsOculusHMDConnected)
                {
                    ////System.Windows.MessageBox.Show("Oculus HMD (Head Mounter Display) is not connected", "Oculus error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    ////System.Windows.MessageBox.Show("Oculus HMD (Head Mounter Display) is connected", "Oculus Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }

       
                /*// Define initialization parameters with debug flag.
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
                    System.Windows.Forms.MessageBox.Show("Failed to initialize the Oculus runtime library:\r\n" + errorReason, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                }

                // Use the head mounted display.
                sessionPtr = IntPtr.Zero;
                var graphicsLuid = new GraphicsLuid();
                result = OVR.Create(ref sessionPtr, ref graphicsLuid);
                if (result < Result.Success)
                {
                    System.Windows.Forms.MessageBox.Show("The HMD is not enabled: " + result.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                }*/

                //hmdDesc = OVR.GetHmdDesc(sessionPtr);

                try
                {
                    // Create a set of layers to submit.
                    eyeTextures = new EyeTexture[2];


                    //------------------------------FOR AB3D DX ENGINE Device.
                    _oculusRiftVirtualRealityProvider = new OculusWrapVirtualRealityProvider(OVR, multisamplingCount: 4); // All of that just for this multisamplingCount and also for ab3d.directx.create a shaderresourceview with the ab3d engine just to test it out.
                    //hmdDesc = _oculusRiftVirtualRealityProvider.HmdDescription;
                    try
                    {
                        // Then we initialize Oculus OVR and create a new DXDevice that uses the same adapter (graphic card) as Oculus Rift
                       _dxDevice = _oculusRiftVirtualRealityProvider.InitializeOvrAndDXDevice(requestedOculusSdkMinorVersion: 17);
                    }
                    catch (Exception ex)
                    {
                        ////System.Windows.MessageBox.Show("Failed to initialize the Oculus runtime library.\r\nError: " + ex.Message, "Oculus error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    OVR.RecenterTrackingOrigin(_oculusRiftVirtualRealityProvider.SessionPtr);

                    sessionPtr = _oculusRiftVirtualRealityProvider.SessionPtr;
                    hmdDesc = OVR.GetHmdDesc(sessionPtr);
                    //----------------------FOR AB3D DX ENGINE Device.













                    _device = _dxDevice.Device;
                    //_device = new Device(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None);

                    _deviceContext = _device.ImmediateContext;

                    // Create DirectX drawing device.
                    // Create DirectX Graphics Interface factory, used to create the swap chain.
                    //using (var factory = new SharpDX.DXGI.Factory1())
                    {
                        var factory = new SharpDX.DXGI.Factory1();
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
                        _swapChain = new SwapChain(factory, _device, swapChainDescription);
                        factory.Dispose();


                        // Retrieve the back buffer of the swap chain.
                        _backBuffer = _swapChain.GetBackBuffer<SharpDX.Direct3D11.Texture2D>(0);
                        _renderTargetView = new RenderTargetView(_device, _backBuffer);

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



                        /*// Initialize and set up the description of the stencil state.
                        DepthStencilStateDescription depthStencilStateDescription = new DepthStencilStateDescription()
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
                        };*/











                        depthBuffer = new SharpDX.Direct3D11.Texture2D(_device, depthBufferDescription);
                        _depthStencilView = new DepthStencilView(_device, depthBuffer);
                         depthStencilState = new SharpDX.Direct3D11.DepthStencilState(_device, depthStencilStateDescription);
                        var viewport = new SharpDX.Viewport(0, 0, hmdDesc.Resolution.Width, hmdDesc.Resolution.Height, 0.0f, 1.0f);


                        _deviceContext.OutputMerger.SetDepthStencilState(depthStencilState);
                        _deviceContext.OutputMerger.SetRenderTargets(_depthStencilView, _renderTargetView);



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
                        */


                        //DeviceContext.Rasterizer.State.Description.CullMode = CullMode.Back;



                        _deviceContext.Rasterizer.SetViewport(viewport);

                        //var test = _device.ImmediateContext.Rasterizer.State.Description.CullMode;                  

                        // Retrieve the DXGI device, in order to set the maximum frame latency.
                        using (SharpDX.DXGI.Device1 dxgiDevice = _device.QueryInterface<SharpDX.DXGI.Device1>())
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

                            result = OVR.CreateTextureSwapChainDX(sessionPtr, _device.NativePointer, ref textureSwapChainDesc, out textureSwapChainPtr);
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
                                eyeTexture.RenderTargetViews[textureIndex] = new RenderTargetView(_device, eyeTexture.Textures[textureIndex]);
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
                            eyeTexture.DepthBuffer = new SharpDX.Direct3D11.Texture2D(_device, eyeTexture.DepthBufferDescription);
                            eyeTexture.DepthStencilView = new DepthStencilView(_device, eyeTexture.DepthBuffer);

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
                        result = OVR.CreateMirrorTextureDX(sessionPtr, _device.NativePointer, ref mirrorTextureDescription, out mirrorTexturePtr);
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
                        _device.ImmediateContext.Rasterizer.State = RasterState;*/
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                _deviceContext.ClearState();
                _deviceContext.Flush();

            }
            else
            {

            }

            _StartWatch.Stop();
            _StartWatch.Reset();
            _StartWatch.Start();

            _worldMatrix = SharpDX.Matrix.Identity;

            _hasInit = true;

            RotationX = 0;
            RotationY = 180;
            RotationZ = 0;


            float pitch = RotationX * 0.0174532925f;
            float yaw = RotationY * 0.0174532925f;
            float roll = RotationZ * 0.0174532925f;

            originRot = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);


            System.DateTime previousFrameStartTime = DateTime.UtcNow;
            double desiredFrameLength = 1.0 / 60.0;
            var backgroundWorker0 = new BackgroundWorker();
            backgroundWorker0.DoWork += (object sender, DoWorkEventArgs args) =>
            {
                //World.initWorld();
                //_world = new World();
                //_world.initWorld();

                Camera = new SC_skYaRk_VR_Edition_v005.DCamera();
                Camera.Render();
                Camera.SetPosition(0, 0, 0);

                originalCameraPos = Camera.GetPosition();

                var directInput = new DirectInput();

                keyboard = new SharpDX.DirectInput.Keyboard(directInput);

                keyboard.Properties.BufferSize = 128;
                keyboard.Acquire();



                SharpDX.Matrix _WorldMatrix = WorldMatrix;



                //RotationX = 45;
                //RotationY = 45;
                //RotationZ = 45;

                /*SharpDX.Matrix rotationMatrix;
                RotateX(45);
                SharpDX.Matrix.RotationX(RotationX, out rotationMatrix);
                SharpDX.Matrix.Multiply(ref _WorldMatrix, ref rotationMatrix, out _WorldMatrix);
                RotateY(45);
                SharpDX.Matrix.RotationY(RotationY, out rotationMatrix);
                SharpDX.Matrix.Multiply(ref _WorldMatrix, ref rotationMatrix, out _WorldMatrix);
                RotateZ(45);
                SharpDX.Matrix.RotationZ(RotationZ, out rotationMatrix);
                SharpDX.Matrix.Multiply(ref _WorldMatrix, ref rotationMatrix, out _WorldMatrix);*/

                SharpDX.Matrix rotationMatrix;
                pitch = RotationX * 0.0174532925f;
                 yaw = RotationY * 0.0174532925f;
                roll = RotationZ * 0.0174532925f;

                rotationMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);
                SharpDX.Matrix.Multiply(ref _WorldMatrix, ref rotationMatrix, out _WorldMatrix);

                int startOnce = 1;



            _threadLoop:
                try
                {

                    if (startOnce == 1)
                    {
                        
                        //backgroundWorker0.RunWorkerAsync();
                        startOnce = 0;
                    }




































      

                    //if (Rendering)
                    {
                        try
                        {

                            /*// Start rendering
                            if (RenderAt90Fps == 1)
                            {
                                // WPF do not support rendering at more the 60 FPS.
                                // But with a trick where a rendering loop is created in a background thread, it is possible to achieve more than 60 FPS.
                                // In case of submitting frames to Oculus Rift at higher FPS, the ovr.SubmitFrame method will limit rendering to 90 FPS.
                                // 
                                // NOTE:
                                // When using DXEngine, it is also possible to render the scene in a background thread. 
                                // This requires that the 3D scene is also created in the background thread and that the events and other messages are 
                                // passed between UI and background thread in a thread safe way. This is too complicated for this simple sample project.
                                // To see one possible implementation of background rendering, see the BackgroundRenderingSample in the Ab3d.DXEngine.Wpf.Samples project.
                                var backgroundWorker = new BackgroundWorker();
                                backgroundWorker.DoWork += (object sender, DoWorkEventArgs args) =>
                                {
                                    // Create an action that will be called by Dispatcher
                                    var refreshDXEngineAction = new Action(() =>
                                    {
                                        UpdateScene();

                                        // Render DXEngine's 3D scene again
                                        if (_dxViewportView != null)
                                            _dxViewportView.Refresh();
                                    });

                                    while (_dxViewportView != null && !_dxViewportView.IsDisposed) // Render until window is closed
                                    {
                                        if (_oculusRiftVirtualRealityProvider != null && _oculusRiftVirtualRealityProvider.LastSessionStatus.ShouldQuit) // Stop rendering - this will call RunWorkerCompleted where we can quit the application
                                            break;

                                        // Sleep for 1 ms to allow WPF tasks to complete (for example handling XBOX controller events)
                                        System.Threading.Thread.Sleep(1);

                                        // Call Refresh to render the DXEngine's scene
                                        // This is a synchronous call and will wait until the scene is rendered. 
                                        // Because Oculus is limited to 90 fps, the call to ovr.SubmitFrame will limit rendering to 90 FPS.
                                        Dispatcher.Invoke(refreshDXEngineAction);
                                    }
                                };

                                backgroundWorker.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args)
                                {
                                    if (_oculusRiftVirtualRealityProvider != null && _oculusRiftVirtualRealityProvider.LastSessionStatus.ShouldQuit)
                                        this.Close(); // Exit the application
                                };

                                backgroundWorker.RunWorkerAsync();
                            }
                            else
                            {
                                // Subscribe to WPF rendering event (called approximately 60 times per second)
                                //CompositionTarget.Rendering += CompositionTargetOnRendering;
                            }*/








                            BeginRender();
                            Render();
                            EndRender();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }

                        //DateTime beforeFrameTime = DateTime.UtcNow;
                        //float elapsedSinceLastFrame = (float)(beforeFrameTime - previousFrameStartTime).TotalSeconds;
                        //Time.SetDeltaTime(elapsedSinceLastFrame);

                        //previousFrameStartTime = beforeFrameTime;

                        // DateTime afterFrameTime = DateTime.UtcNow;
                        // double elapsed = (afterFrameTime - beforeFrameTime).TotalSeconds;
                        //double sleepTime = desiredFrameLength - elapsed;


                        //Console.WriteLine(elapsedSinceLastFrame);

                        /*if (elapsedSinceLastFrame > 0.01f)
                        {
                            Thread.Yield();
                            //Console.WriteLine("Running Slow");
                        }
                        else
                        {
                            //Console.WriteLine("Running Fast");
                            Thread.Sleep(1);
                        }*/


                        /* if (sleepTime > 0.0)
                         {
                             //Console.WriteLine("Running fast bullshit");
                             DateTime finishTime = afterFrameTime + TimeSpan.FromSeconds(sleepTime);
                             while (DateTime.UtcNow < finishTime)
                             {
                                 Thread.Yield();
                             }
                             Thread.Sleep(1);
                         }
                         else
                         {
                             Console.WriteLine("Running slowly, no sleep time.");
                         }*/













                    }                
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
               
                /*var refreshDXEngineAction = new Action(delegate
                {
                  
                });

                System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);*/

               


                goto _threadLoop;
            };

            backgroundWorker0.RunWorkerAsync();





            /*var backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += (object sender, DoWorkEventArgs args) =>
            {
                _threadLoop:
                /*var refreshDXEngineAction = new Action(delegate
                {
                  
                });

                System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                
                totalTicks = _StartWatch.ElapsedTicks;

                Thread.Sleep(1);
                goto _threadLoop;
            };

            backgroundWorker1.RunWorkerAsync();*/
            
        }

        protected virtual void InternalUninitialize()
        {
            Console.WriteLine("test");
            SwapChain?.SetFullscreenState(false, null);

            // Dispose of all objects.
            RasterState?.Dispose();
            RasterState = null;
            DepthStencilView?.Dispose();
            //_DepthStencilView = null;
            DepthStencilBuffer?.Dispose();
            //DepthStencilBuffer = null;
            RenderTargetView?.Dispose();
            _renderTargetView = null;
            DeviceContext?.Dispose();
            //DeviceContext = null;
            //Device?.Dispose();
            //Device = null;
            //SwapChain?.Dispose();
            //SwapChain = null;

            if (DeviceContext != null)
            {
                DeviceContext.ClearState();
                DeviceContext.Flush();
            }

            // Release all resources
            Dispose(inputLayout);
            Dispose(contantBuffer);
            Dispose(vertexBuffer);
            //Dispose(_shaderSignature);
            Dispose(pixelShader);
            //Dispose(_pixelShaderByteCode);
            Dispose(vertexShader);
            //Dispose(_vertexShaderByteCode);
            Dispose(mirrorTextureD3D);
            Dispose(mirrorTexture);
            Dispose(eyeTextures[0]);
            Dispose(eyeTextures[1]);
            Dispose(DeviceContext);
            Dispose(DepthStencilBuffer);
            Dispose(_depthStencilView);
            Dispose(depthBuffer);
            //Dispose(_backBufferRenderTargetView);
            Dispose(_backBuffer);
            Dispose(_swapChain);
            //Dispose(factory);
            Dispose(depthStencilState);

            OVR.Destroy(sessionPtr);
            //Utilities.Dispose(ref backBufferRenderTargetView);
            //Utilities.Dispose(ref backBuffer);
            //Utilities.Dispose(ref swapChain);

            // This is a workaround for an issue in SharpDx3.0.2 (https://github.com/sharpdx/SharpDX/issues/731)
            // Will need to be removed when fixed in next SharpDx release
            ((IUnknown)Device).Release();
            Utilities.Dispose(ref _device);



            // Disposing the device, before the hmd, will cause the hmd to fail when disposing.
            // Disposing the device, after the hmd, will cause the dispose of the device to fail.
            // It looks as if the hmd steals ownership of the device and destroys it, when it's shutting down.
            // device.Dispose();


            GC.Collect(2, GCCollectionMode.Forced);
        }



        public static void Dispose(IDisposable disposable)
        {
            if (disposable != null)
                disposable.Dispose();
        }






        float widther = 1920;
        float heighter = 1080;

        int lastWidth = 0;
        int lastHeight = 0;

        public static float currentWidth;
        public static float currentHeight;
        float divider;




        public static float step =0;
        private int activeBodies = 0;
        protected virtual void BeginRender()
        {
           

          
            /*if (SC_WPF_RENDER.MainWindow._SizeChanged) //lastWidth!= SurfaceWidth && lastHeight != SurfaceHeight
            {
                // Dispose all previous allocated resources
                Utilities.Dispose(ref _backBuffer);
                Utilities.Dispose(ref _renderTargetView);
                Utilities.Dispose(ref _depthStencilView);
                Utilities.Dispose(ref _depthStencilBuffer);


                //currentWidth = (float)(System.Windows.SystemParameters.PrimaryScreenWidth);
                //currentHeight = (float)(System.Windows.SystemParameters.PrimaryScreenHeight);
                currentWidth = (int)(SC_WPF_RENDER.MainWindow._realWidth);
                currentHeight = (int)(SC_WPF_RENDER.MainWindow._realHeight);

                SwapChain.ResizeBuffers(1, (int)currentWidth, (int)currentHeight, Format.R8G8B8A8_UNorm, SwapChainFlags.AllowModeSwitch);

                using (var backBufferTexture = SwapChain.GetBackBuffer<Texture2D>(0))
                {
                    _renderTargetView = new RenderTargetView(Device, backBufferTexture);
                }

                using (var zbufferTexture = new Texture2D(Device, new Texture2DDescription()
                {
                    Format = Format.D24_UNorm_S8_UInt,
                    ArraySize = 1,
                    MipLevels = 1,
                    Width = Math.Max(1, (int)currentWidth),
                    Height = Math.Max(1, (int)currentHeight),
                    SampleDescription = new SampleDescription(1, 0),
                    Usage = ResourceUsage.Default,
                    BindFlags = BindFlags.DepthStencil,
                    CpuAccessFlags = CpuAccessFlags.None,
                    OptionFlags = ResourceOptionFlags.None
                }))
                {
                    // Create the depth buffer view
                    _depthStencilView = new DepthStencilView(Device, zbufferTexture);
                }

                Device.ImmediateContext.Rasterizer.SetViewport(0, 0, currentWidth, currentHeight);

                _projectionMatrix = SharpDX.Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, currentWidth / currentHeight, DSystemConfiguration.ScreenNear, DSystemConfiguration.ScreenDepth);
               Render(); ///***************************************************************************************************************************************************
                SC_WPF_RENDER.MainWindow._SizeChanged = false;
            }*/





            //Device.ImmediateContext.Rasterizer.SetViewport(0, 0, currentWidth, currentHeight);
            //Device.ImmediateContext.ClearDepthStencilView(_depthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);
            //Device.ImmediateContext.ClearRenderTargetView(_renderTargetView, SharpDX.Color.CornflowerBlue);
            //Device.ImmediateContext.OutputMerger.SetTargets(_depthStencilView, _renderTargetView);
        }

        /// <summary>
        /// Finish render.
        /// Derived methods must call base.EndRender() 
        /// </summary>
        protected virtual void EndRender()
        {
            _swapChain.Present(0, PresentFlags.None);
        }

        /// <summary>
        /// Perform render.
        /// </summary>
        protected abstract void Render();

        public static void WriteErrorDetails(OvrWrap OVR, Result result, string message)
        {
            if (result >= Result.Success)
                return;

            // Retrieve the error message from the last occurring error.
            ErrorInfo errorInformation = OVR.GetLastErrorInfo();

            string formattedMessage = string.Format("{0}. \nMessage: {1} (Error code={2})", message, errorInformation.ErrorString, errorInformation.Result);
            Trace.WriteLine(formattedMessage);
            //System.Windows.Forms.MessageBox.Show(formattedMessage, message);

            throw new Exception(formattedMessage);
        }
    }







    internal class HResults
    {
        // ReSharper disable InconsistentNaming
        public const int D2DERR_RECREATE_TARGET = unchecked((int)0x8899000C);
        public const int DXGI_ERROR_DEVICE_REMOVED = unchecked((int)0x887A0005);
        // ReSharper restore InconsistentNaming
    }
}
