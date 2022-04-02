using System;
using System.Windows;
using System.Windows.Interop;

using Win32.DwmThumbnail.Interop;
using Win32.Shared;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace Win32.DwmThumbnail
{
    /// <summary>
    ///     MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private IntPtr _hThumbnail = IntPtr.Zero;
        private IntPtr _hWnd = IntPtr.Zero;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            SizeChanged += OnSizeChanged;
        }


        int bitmapcounter = 0;
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            do
            {
                _hWnd = new WindowPicker().PickCaptureTarget(new WindowInteropHelper(this).Handle);
            } while (_hWnd == IntPtr.Zero);

            var hr = NativeMethods.DwmRegisterThumbnail(new WindowInteropHelper(this).Handle, _hWnd, out _hThumbnail);
            if (hr != 0)
                return;
            UpdateThumbnailProperties();

            



            //var someimage = new Bitmap(0,0,(int)(Grid.ActualWidth * dpi.Y), (int)(Grid.ActualHeight * dpi.Y));



            //Bitmap somebitmap = System.Drawing.Image.FromHbitmap(_hWnd);
            //Image.GetThumbnailImageAbort myCallback = new Image().GetThumbnailImage
            /*
            var someimage = new Image((int)(Grid.ActualWidth * dpi.Y), (int)(Grid.ActualHeight * dpi.Y));

           var test = someimage.GetThumbnailImage((int)(Grid.ActualWidth * dpi.Y), (int)(Grid.ActualHeight * dpi.Y), myCallback, _hThumbnail);
            */

            ///Bitmap.get



        }

        public bool ThumbnailCallback()
        {
            return false;
        }
        //from microsoft getthumbnailimage.
        public void Example_GetThumb(PaintEventArgs e)
        {



            /*Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            Bitmap myBitmap = new Bitmap("Climber.jpg");
            Image myThumbnail = myBitmap.GetThumbnailImage(40, 40, myCallback, IntPtr.Zero);
            e.Graphics.DrawImage(myThumbnail, 150, 75);*/

        }



        /*
        HRESULT GetThumbnail(
          [in] IShellItem* pShellItem,
          [in] UINT cxyRequestedThumbSize,
          [in] WTS_FLAGS flags,
          [out, optional] ISharedBitmap** ppvThumb,
          [out, optional] WTS_CACHEFLAGS* pOutFlags,
          [out, optional] WTS_THUMBNAILID* pThumbnailID
        );
        */



        Guid IID_IUnknown = new Guid("00000000-0000-0000-C000-000000000046");
        Guid CLSID_LocalThumbnailCache = new Guid("50EF4544-AC9F-4A8E-B21B-8A26180DB13F");

        //IntPtr cachePointer;
        //CoCreateInstance(CLSID_LocalThumbnailCache, IntPtr.Zero, CLSCTX.CLSCTX_INPROC, IID_IUnknown, out cachePointer);
        //IThumbnailCache thumbnailCache = (IThumbnailCache)Marshal.GetObjectForIUnknown(cachePointer);

        /*[DllImport("ole32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
        static extern void CoCreateInstanceEx(
           [In, MarshalAs(UnmanagedType.LPStruct)] Guid rclsid,
           [MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter,
           CLSCTX dwClsCtx,
           IntPtr pServerInfo,
           uint cmq,
           [In, Out] MULTI_QI[] pResults);*/


        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_hThumbnail == IntPtr.Zero)
                return;

            /*var dpi = GetDpiScaleFactor();

            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            byte[] arrayimage = new byte[(int)(Grid.ActualWidth * dpi.Y) * (int)(Grid.ActualHeight * dpi.Y) * 4];

            var somebitmap = new System.Drawing.Bitmap((int)(Grid.ActualWidth * dpi.X), (int)(Grid.ActualHeight * dpi.Y), (int)(Grid.ActualWidth * dpi.Y) * 4, PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(arrayimage, 0));

            var test = somebitmap.GetThumbnailImage((int)(Grid.ActualWidth * dpi.X), (int)(Grid.ActualHeight * dpi.Y), myCallback, _hThumbnail);
            //myCallback.Invoke();

            test.Save(@"C:\Users\steve\Desktop\screenrecord\" + bitmapcounter + "_" + ".bmp");
            bitmapcounter++;*/

            //IThumbnailCache thumbnailcache = (IThumbnailCache)Marshal.GetObjectForIUnknown(_hThumbnail);
            //UpdateThumbnailProperties();
        }




        /*

        [ComImportAttribute()]
        [GuidAttribute("43826d1e-e718-42ee-bc55-a1e261c37bfe")]
        [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IShellItem
        {
            void BindToObject(
            IntPtr pbcReserved,
            Guid rbhid,
            ref Guid riid,
            ref IThumbnailProvider ppvOut);

            void Compare(
            IShellItem psi,
            uint hint,
            out int piorder);

            void GetAttributes(
            ulong sfgaoMask,
            out ulong psfgaoAttribs);

            void GetDisplayName(
            uint sigdnName,
            out string ppszName);

            void GetParent(
            ref IShellItem ppsi);
        }

        [ComImportAttribute()]
        [GuidAttribute("66742402-F9B9-11D1-A202-0000F81FEDEE")]
        [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IThumbnailProvider
        {
            void GetThumbnail(
            uint cx,
            out IntPtr phbmp,
            out uint pdwAlpha);
        }*/



        /*//https://stackoverflow.com/questions/12654835/accessing-thumbnails-that-dont-exist
        static void Main(string[] args)
        {
            // you can use any type of file supported by your windows installation.
            /*string path = @"c:\temp\whatever.pdf";
            using (Bitmap bmp = ExtractThumbnail(path, new System.Windows.Size(1024, 1024), SIIGBF.SIIGBF_RESIZETOFIT))
            {
                bmp.Save("whatever.png", ImageFormat.Png);
            }
        }*/

        public static Bitmap ExtractThumbnail(string filePath, System.Windows.Size size, SIIGBF flags)
        {
            if (filePath == null)
                throw new ArgumentNullException("filePath");

            // TODO: you might want to cache the factory for different types of files
            // as this simple call may trigger some heavy-load underground operations








            IShellItemImageFactory factory;
            int hr = SHCreateItemFromParsingName(filePath, IntPtr.Zero, typeof(IShellItemImageFactory).GUID, out factory);
            if (hr != 0)
                //throw new Win32Exception(hr);
                Console.WriteLine(hr.ToString());

            IntPtr bmp;
            hr = factory.GetImage(size, flags, out bmp);
            if (hr != 0)
                Console.WriteLine(hr.ToString());
                //throw new Win32Exception(hr);

            return Bitmap.FromHbitmap(bmp);
        }

        [Flags]
        public enum SIIGBF
        {
            SIIGBF_RESIZETOFIT = 0x00000000,
            SIIGBF_BIGGERSIZEOK = 0x00000001,
            SIIGBF_MEMORYONLY = 0x00000002,
            SIIGBF_ICONONLY = 0x00000004,
            SIIGBF_THUMBNAILONLY = 0x00000008,
            SIIGBF_INCACHEONLY = 0x00000010,
            SIIGBF_CROPTOSQUARE = 0x00000020,
            SIIGBF_WIDETHUMBNAILS = 0x00000040,
            SIIGBF_ICONBACKGROUND = 0x00000080,
            SIIGBF_SCALEUP = 0x00000100,
        }

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern int SHCreateItemFromParsingName(string path, IntPtr pbc, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IShellItemImageFactory factory);

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("bcc18b79-ba16-442f-80c4-8a59c30c463b")]
        private interface IShellItemImageFactory
        {
            [PreserveSig]
            int GetImage(System.Windows.Size size, SIIGBF flags, out IntPtr phbm);
        }


        /*
        [ComImportAttribute()]
        [GuidAttribute("bcc18b79-ba16-442f-80c4-8a59c30c463b")]
        [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IShellItemImageFactory
        {
            void GetImage(
            [In, MarshalAs(UnmanagedType.Struct)] SIZE size,
            [In] SIIGBF flags,
            [Out] out IntPtr phbm);
        }
        */

        private void UpdateThumbnailProperties()
        {
            var dpi = GetDpiScaleFactor();
            var props = new DWM_THUMBNAIL_PROPERTIES
            {
                fVisible = true,
                dwFlags = (int) (DWM_TNP.DWM_TNP_VISIBLE | DWM_TNP.DWM_TNP_OPACITY | DWM_TNP.DWM_TNP_RECTDESTINATION | DWM_TNP.DWM_TNP_SOURCECLIENTAREAONLY),
                opacity = 255,
                rcDestination = new RECT { left = 0, top = 0, bottom = (int) (Grid.ActualHeight * dpi.Y), right = (int) (Grid.ActualWidth * dpi.X) },
                fSourceClientAreaOnly = true
            };

            NativeMethods.DwmUpdateThumbnailProperties(_hThumbnail, ref props);





            //NativeMethods.DwmUnregisterThumbnail(_hThumbnail);


            //Console.WriteLine(this.image.Width);





            //var background = this.Grid.Background;
            //this.Grid.Background = null;
            //Bitmap bitm = new Bitmap(background);
            //var pixelformat = PixelFormat.Format32bppArgb;
            //stackoverflow 19570593
            //System.Drawing.Brush b = new System.Drawing.SolidBrush((System.Drawing.Color)new System.Drawing.ColorConverter().ConvertFromString(new System.Windows.Media.BrushConverter().ConvertToString(background)));






            //var res = this.Grid.Resources; //0
            //Console.WriteLine(res.Count);

            /*
            var source = PresentationSource.FromVisual(this);
            var comp = source.CompositionTarget;
            var dep =  source.RootVisual.DependencyObjectType;
            Console.WriteLine(dep.Name + " " + dep.Id);
            */







            /*NativeMethods.DwmUnregisterThumbnail(_hThumbnail);





            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            //var dpi = GetDpiScaleFactor();
            byte[] arrayimage = new byte[(int)(Grid.ActualWidth * dpi.Y) * (int)(Grid.ActualHeight * dpi.Y) * 4];


            var somebitmap = new System.Drawing.Bitmap((int)(Grid.ActualWidth * dpi.X), (int)(Grid.ActualHeight * dpi.Y), (int)(Grid.ActualWidth * dpi.Y) * 4, PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(arrayimage, 0));
            somebitmap.Save(@"C:\Users\steve\Desktop\screenrecord\" + bitmapcounter + "_" + ".bmp");
            bitmapcounter++;*/





            /*Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            byte[] arrayimage = new byte[(int)(Grid.ActualWidth * dpi.Y) * (int)(Grid.ActualHeight * dpi.Y) * 4];

            var somebitmap = new System.Drawing.Bitmap((int)(Grid.ActualWidth * dpi.X), (int)(Grid.ActualHeight * dpi.Y), (int)(Grid.ActualWidth * dpi.Y) * 4, PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(arrayimage, 0));

            var test = somebitmap.GetThumbnailImage((int)(Grid.ActualWidth * dpi.X), (int)(Grid.ActualHeight * dpi.Y), myCallback, _hThumbnail);
            //myCallback.Invoke();

            test.Save(@"C:\Users\steve\Desktop\screenrecord\" + bitmapcounter + "_" + ".bmp");
            bitmapcounter++;*/

            /*string path = @"C:\Users\steve\Desktop\screenrecord\" + bitmapcounter + "_" + ".jpg";
            Bitmap bmp = ExtractThumbnail(path, new System.Windows.Size(1024, 1024), SIIGBF.SIIGBF_RESIZETOFIT);
            bmp.Save(path);
            bitmapcounter++;*/



            //var somebitmap = new System.Drawing.Bitmap(1920, 1080, 1920 * 4, PixelFormat.Format32bppArgb, _hThumbnail);
            //somebitmap.Save(@"C:\Users\steve\Desktop\screenrecord\" + bitmapcounter + "_" + ".png");
            //bitmapcounter++;
        }

        private System.Windows.Point GetDpiScaleFactor()
        {
            var source = PresentationSource.FromVisual(this);
            return source?.CompositionTarget != null ? new System.Windows.Point(source.CompositionTarget.TransformToDevice.M11, source.CompositionTarget.TransformToDevice.M22) : new System.Windows.Point(1.0d, 1.0d);
       
        
        }
    }
}