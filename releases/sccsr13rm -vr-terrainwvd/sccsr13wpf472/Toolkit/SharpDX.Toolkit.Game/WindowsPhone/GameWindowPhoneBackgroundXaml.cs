// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
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

#if WP8
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using SharpDX.Mathematics;
using SharpDX.Direct3D11;
using SharpDX.Toolkit.Graphics;

namespace SharpDX.Toolkit
{
    internal class GameWindowPhoneBackgroundXaml : GameWindow,
                                                   IDrawingSurfaceBackgroundContentProviderNative,
                                                   IInspectable,
                                                   ICustomQueryInterface
    {
        #region Fields

        private readonly IntPtr thisComObjectPtr;

        private RenderTarget2D backBuffer;

        private Device currentDevice;

        private DeviceContext currentDeviceContext;

        private RenderTargetView currentRenderTargetView;

        private Exception drawException;

        private DrawingSurfaceBackgroundGrid drawingSurfaceBackgroundGrid;

        private GraphicsDevice graphicsDevice;

        private IGraphicsDeviceManager graphicsDeviceManager;

        private DrawingSurfaceRuntimeHost host;

        private bool isInitialized;

        private int nextRenderTarget;

        #endregion

        #region Constructors and Destructors

        internal GameWindowPhoneBackgroundXaml()
        {
            thisComObjectPtr = CppObject.ToCallbackPtr<IDrawingSurfaceBackgroundContentProviderNative>(this);
        }

        #endregion

        #region Public Properties

        public override bool AllowUserResizing
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public override Rectangle ClientBounds
        {
            get
            {
                if (backBuffer != null)
                {
                    return new Rectangle(0, 0, backBuffer.Width, backBuffer.Height);
                }
                return Rectangle.Empty;
            }
        }

        public override DisplayOrientation CurrentOrientation
        {
            get
            {
                return DisplayOrientation.Default;
            }
        }

        public override bool IsMinimized
        {
            get
            {
                return false;
            }
        }

        public override bool IsMouseVisible { get; set; }

        public override object NativeWindow
        {
            get
            {
                return drawingSurfaceBackgroundGrid;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="GameWindow" /> is visible.
        /// </summary>
        /// <value>
        ///     <c>true</c> if visible; otherwise, <c>false</c>.
        /// </value>
        public override bool Visible
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        internal override bool IsBlockingRun { get { return false; } }

        #endregion

        #region Explicit Interface Properties

        IDisposable ICallbackable.Shadow { get; set; }

        #endregion

        #region Public Methods and Operators

        public override void BeginScreenDeviceChange(bool willBeFullScreen)
        {
        }

        public override void EndScreenDeviceChange(int clientWidth, int clientHeight)
        {
        }

        #endregion

        #region Explicit Interface Methods

        CustomQueryInterfaceResult ICustomQueryInterface.GetInterface(ref Guid iid, out IntPtr ppv)
        {
            ppv = thisComObjectPtr;
            return CustomQueryInterfaceResult.Handled;
        }

        void IDrawingSurfaceBackgroundContentProviderNative.Connect(DrawingSurfaceRuntimeHost host, Device device)
        {
            this.host = host;
        }

        void IDrawingSurfaceBackgroundContentProviderNative.Disconnect()
        {
            // Don't perform any dispose here, as It seems that we are unable to get back to the application after (program access violation)
        }

        void IDrawingSurfaceBackgroundContentProviderNative.Draw(
            Device device,
            DeviceContext context,
            RenderTargetView renderTargetView)
        {
            try
            {
                if (!Exiting)
                {
                    // Sets the current RenderTargetView
                    currentRenderTargetView = renderTargetView;

                    // Create the GraphicsDevice here
                    if (!ComObject.EqualsComObject(device, currentDevice) ||
                        !ComObject.EqualsComObject(context, currentDeviceContext))
                    {
                        currentDevice = device;
                        currentDeviceContext = context;

                        // Dispose all previously allocated device/backbuffers
                        DisposeAll();

                        if (!isInitialized)
                        {
                            // The InitCallback will call us back on EnsureDevice method
                            InitCallback();
                            isInitialized = true;
                        }
                        else
                        {
                            Utilities.Dispose(ref graphicsDevice);


                            // Call the graphics device to call us back on EnsureDevice method
                            graphicsDeviceManager.CreateDevice();
                        }
                    }

                    EnsurePresenter(false);

                    // Call the main Game.Run loop
                    RunCallback();

                    // Ask the host for additional frame
                    host.RequestAdditionalFrame();
                }
            }
            catch (Exception ex)
            {
                // TODO: As we are in a callback from a native code, we cannot pass back this exception,
                // so how to pass back this exception to the user at an appropriate time?
                drawException = ex;
                Debug.WriteLine(drawException);
            }
        }

        private int frameCount = 0;

        void IDrawingSurfaceBackgroundContentProviderNative.PrepareResources(
            DateTime presentTargetTime,
            ref Size2F desiredRenderTargetSize)
        {
        }

        #endregion

        #region Methods

        internal override bool CanHandle(GameContext gameContext)
        {
            return gameContext.ContextType == GameContextType.WindowsPhoneBackgroundXaml;
        }

        public DepthFormat RequestDepthFormat;

        /// <summary>
        /// Creates a <see cref="GraphicsDevice"/> if not already created based from latest internal device.
        /// </summary>
        /// <returns>a instance of GraphicsDevice.</returns>
        internal GraphicsDevice EnsureDevice()
        {
            if (graphicsDevice == null)
            {
                // Creates a GraphicsDevice from the current GraphicsDevice
                graphicsDevice = GraphicsDevice.New(currentDevice);

                // Reset the presenter
                EnsurePresenter(true);
            }

            return graphicsDevice;
        }

        /// <summary>
        /// Creates a <see cref="RenderTargetGraphicsPresenter" /> if not already created based from latest internal device.
        /// </summary>
        /// <param name="resetPresenter">if set to <c>true</c> [reset presenter].</param>
        internal void EnsurePresenter(bool resetPresenter)
        {
            // Creates the backbuffer
            Utilities.Dispose(ref backBuffer);
            backBuffer = RenderTarget2D.New(graphicsDevice, currentRenderTargetView, true);
            currentRenderTargetView.Tag = backBuffer;
            
            if(graphicsDevice.Presenter != null)
            {
                graphicsDevice.Presenter.Dispose();
                graphicsDevice.Presenter = null;
            }

            graphicsDevice.Presenter = new RenderTargetGraphicsPresenter(graphicsDevice, backBuffer, RequestDepthFormat);
        }

        internal override void Initialize(GameContext gameContext)
        {
            drawingSurfaceBackgroundGrid = (DrawingSurfaceBackgroundGrid)gameContext.Control;
            graphicsDeviceManager = (IGraphicsDeviceManager)Services.GetService(typeof(IGraphicsDeviceManager));
        }

        internal override void Resize(int width, int height)
        {
        }

        internal override void Run()
        {
            BindDrawingSurfaceBackgroundGridEvents();
        }

        internal override void Switch(GameContext context)
        {
            isInitialized = false;

            drawingSurfaceBackgroundGrid.Loaded -= DrawingSurfaceBackgroundGridOnLoaded;
            drawingSurfaceBackgroundGrid.Unloaded -= drawingSurfaceBackgroundGrid_Unloaded;
            drawingSurfaceBackgroundGrid.SetBackgroundContentProvider(null);

            drawingSurfaceBackgroundGrid = (DrawingSurfaceBackgroundGrid)context.Control;

            BindDrawingSurfaceBackgroundGridEvents();
            // TODO: check if this can cause issues as event "Loaded" never gets called
            drawingSurfaceBackgroundGrid.SetBackgroundContentProvider(this);

            currentDevice = null;
            currentDeviceContext = null;
        }

        protected internal override void SetSupportedOrientations(DisplayOrientation orientations)
        {
        }

        protected override void Dispose(bool disposeManagedResources)
        {
            drawingSurfaceBackgroundGrid.SetBackgroundContentProvider(null);

            base.Dispose(disposeManagedResources);
        }

        protected override void SetTitle(string title)
        {
        }

        private void BindDrawingSurfaceBackgroundGridEvents()
        {
            drawingSurfaceBackgroundGrid.Loaded += DrawingSurfaceBackgroundGridOnLoaded;
            drawingSurfaceBackgroundGrid.Unloaded += drawingSurfaceBackgroundGrid_Unloaded;
        }

        private void DisposeAll()
        {
            // Dispose the graphics device
            Utilities.Dispose(ref graphicsDevice);

            // Dispose the backbuffer
            Utilities.Dispose(ref backBuffer);
        }

        private void DrawingSurfaceBackgroundGridOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            // Set the background to run this instance 
            drawingSurfaceBackgroundGrid.SetBackgroundContentProvider(this);
        }

        private void drawingSurfaceBackgroundGrid_Unloaded(object sender, RoutedEventArgs e)
        {
            // Never called??
            Exiting = true;
            // Set the background to run this instance 
            drawingSurfaceBackgroundGrid.SetBackgroundContentProvider(null);
        }

        #endregion

        private struct RenderTargetLocal
        {
            #region Fields

            public IntPtr NativePointer;

            public RenderTarget2D RenderTarget;

            #endregion

            #region Public Methods and Operators

            public void Dispose()
            {
                Utilities.Dispose(ref RenderTarget);
                NativePointer = IntPtr.Zero;
            }

            #endregion
        }
    }
}

#endif