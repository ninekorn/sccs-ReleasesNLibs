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

using System;
using System.Drawing.Imaging;
using System.Drawing;
//using System.Windows.Media.Imaging;

using System.IO;

using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.WIC;

using Device = SharpDX.Direct3D11.Device;
using MapFlags = SharpDX.Direct3D11.MapFlags;


using System.Runtime.InteropServices;

using System.Windows.Input;
//using System.Windows.Forms;


using SharpDX.Mathematics.Interop;
//using Device = SharpDX.Direct3D11.Device;
//using MapFlags = SharpDX.Direct3D11.MapFlags;
using Resource = SharpDX.DXGI.Resource;
using ResultCode = SharpDX.DXGI.ResultCode;

using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

using System.Linq;
using System.Collections.Generic;

using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Media;
//using System.Windows.Media.Media3D;
//using Ab3d.Cameras;
using Ab3d.Common;
//using Ab3d.Common.Cameras;
//using Ab3d.Controls;
using Ab3d.DirectX;
//using Ab3d.DirectX.Controls;
using Ab3d.OculusWrap;
//using Ab3d.Visuals;
using Ab3d.DXEngine.OculusWrap;

using Ab3d.DirectX.Materials;

using Ab3d;
using SharpDX.Direct3D;
using System.Reflection;
//using System.Windows.Interop;
//using Ab3d.DirectX.Models;


using PixelFormat = System.Drawing.Imaging.PixelFormat;
//using Windows.Storage.Streams;



using System;
using System.Diagnostics;
using System.IO;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.DXGI;
using SharpDX.IO;
using SharpDX.WIC;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
//using Bitmap = SharpDX.WIC.Bitmap;
using D2DPixelFormat = SharpDX.Direct2D1.PixelFormat;
using WicPixelFormat = SharpDX.WIC.PixelFormat;
using SharpDX.Win32;

using Bitmap = System.Drawing.Bitmap;

//http://csharphelper.com/blog/2016/06/split-image-files-in-c/
//https://stackoverflow.com/questions/15975972/copy-data-from-from-intptr-to-intptr

namespace SC_skYaRk_VR_V007
{
    /// <summary>
    ///   Screen capture of the desktop using DXGI OutputDuplication.
    /// </summary>
    public unsafe class SC_SharpDX_ScreenCapture
    {

        public ShaderResourceView _lastShaderResourceView;


        // # of graphics card adapter
        static int _numAdapter = 0;
        // # of output device (i.e. monitor)
        static int _numOutput = 0;

        readonly Adapter1 _adapter;
        //static Factory1 _factory;

        readonly SharpDX.Direct3D11.Device _device;
        //static Output _output;



        readonly Output1 _output1;
        static Texture2D _texture2D;
        static Texture2D _texture2DFinal;
        readonly OutputDuplication _outputDuplication;
        readonly Texture2DDescription _textureDescription;
        readonly Texture2DDescription _textureDescriptionFinal;
        System.Drawing.Bitmap _bitmap;

        System.Drawing.Bitmap _bitmapPlayerRect;




        static int _width = 0;
        static int _height = 0;
        int _bytesTotal;
        int _bytesTotalPlayerRect;



        SharpDX.DXGI.Resource _screenResource;
        OutputDuplicateFrameInformation _duplicateFrameInformation;
        OutputDuplicateFrameInformation _previousDuplicateFrameInformation;

        SC_SharpDX_ScreenFrame _frameCaptureData;
        //Bitmap desktopBMP;

        byte[] _emptyArrayPaste;
        byte[] _arrayPlayerPos;
        byte[] _arrayPreviousPlayerPos;
        byte[] _currentArrayPlayerPos;


        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        static Thread _thread;
        Action<string> fuckOff;



        UnmanagedMemoryStream _unmanagedMemoryStreamPlayerRect;// = new UnmanagedMemoryStream
        //int _size = Marshal.SizeOf(_textureByteArray[0] * _textureByteArray.Length);
        //var memIntPtr = Marshal.AllocHGlobal(_size);

        public SC_SharpDX_ScreenCapture(int adapter, int numOutput)
        {
            _frameCaptureData = new SC_SharpDX_ScreenFrame();

            _numAdapter = adapter;
            _numOutput = numOutput;

            _currentByteArray = new byte[1920 * 1080 * 4];

            for (int i = 0; i < _currentByteArray.Length; i++)
            {
                _currentByteArray[i] = 0;
            }

            _previousTextureByteArray = new byte[1920 * 1080 * 4];

            for (int i = 0; i < _previousTextureByteArray.Length; i++)
            {
                _previousTextureByteArray[i] = 0;
            }


            try
            {
                using (var _factory = new SharpDX.DXGI.Factory1())
                {
                    this._adapter = _factory.GetAdapter1(_numAdapter);
                }
            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }

            try
            {
                //initializeDevice();
                //this._device = new Device(_adapter);

                this._device = SC_skYaRk_VR_V007.SC_Console_DIRECTX._dxDevice.Device;


            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }

            try
            {
                //initializeOutput();
                using (var _output = _adapter.GetOutput(_numOutput))
                {
                    // Width/Height of desktop to capture
                    //getDesktopBoundaries();
                    _width = ((SharpDX.Rectangle)_output.Description.DesktopBounds).Width;
                    _height = ((SharpDX.Rectangle)_output.Description.DesktopBounds).Height;
                    _frameCaptureData.width = _width;
                    _frameCaptureData.height = _height;
                    this._output1 = _output.QueryInterface<Output1>();
                }
            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }

            try
            {
                //duplicateOutput();
                this._outputDuplication = _output1.DuplicateOutput(_device);
            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }

            try
            {
                //getTextureDescription();
                this._textureDescription = new Texture2DDescription
                {
                    CpuAccessFlags = CpuAccessFlags.Read,
                    BindFlags = BindFlags.None,//BindFlags.None, //| BindFlags.RenderTarget
                    Format = Format.B8G8R8A8_UNorm,
                    Width = _width,
                    Height = _height,
                    OptionFlags = ResourceOptionFlags.None,
                    MipLevels = 1,
                    ArraySize = 1,
                    SampleDescription = { Count = 1, Quality = 0 },
                    Usage = ResourceUsage.Staging
                };

                this._textureDescriptionFinal = new Texture2DDescription
                {
                    CpuAccessFlags = CpuAccessFlags.None,
                    BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                    Format = Format.B8G8R8A8_UNorm,
                    Width = _width,
                    Height = _height,
                    OptionFlags = ResourceOptionFlags.GenerateMipMaps,
                    MipLevels = 1,
                    ArraySize = 1,
                    SampleDescription = { Count = 1, Quality = 0 },
                    Usage = ResourceUsage.Default
                };             
            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }

            _texture2D = new Texture2D(_device, _textureDescription);
            _texture2DFinal = new Texture2D(_device, _textureDescriptionFinal);

            resourceViewDescription = new ShaderResourceViewDescription
            {
                Format = _texture2DFinal.Description.Format,
                Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D,
                Texture2D = new ShaderResourceViewDescription.Texture2DResource
                {
                    MipLevels = -1,
                    MostDetailedMip = 0
                }
            };

            try
            {
                _bitmap = new System.Drawing.Bitmap(_width, _height, PixelFormat.Format32bppArgb);
                var boundsRect = new System.Drawing.Rectangle(0, 0, _width, _height);
                var bmpData = _bitmap.LockBits(boundsRect, ImageLockMode.ReadOnly, _bitmap.PixelFormat);
                _bytesTotal = Math.Abs(bmpData.Stride) * _bitmap.Height;
                _bitmap.UnlockBits(bmpData);
                _textureByteArray = new byte[_bytesTotal];

                /*_bitmapPlayerRect = new System.Drawing.Bitmap(widthOfRectanglePlayerShip, heightOfRectanglePlayerShip, PixelFormat.Format32bppArgb);
                var boundsRect0 = new System.Drawing.Rectangle(0, 0, widthOfRectanglePlayerShip, heightOfRectanglePlayerShip);
                var bmpData0 = _bitmapPlayerRect.LockBits(boundsRect0, ImageLockMode.ReadOnly, _bitmapPlayerRect.PixelFormat);
                _bytesTotalPlayerRect = Math.Abs(bmpData0.Stride) * _bitmapPlayerRect.Height;
                _bitmapPlayerRect.UnlockBits(bmpData0);*/


                string path = System.IO.Path.GetFullPath(@"..\..\..\screenRec0.png");
                _searchFor = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromFile(path);
                boundsRect = new System.Drawing.Rectangle(0, 0, _searchFor.Width, _searchFor.Height);
                bmpData = _bitmap.LockBits(boundsRect, ImageLockMode.ReadOnly, _bitmap.PixelFormat);
                var _bytesTotaler = Math.Abs(bmpData.Stride) * _bitmap.Height;
                ptrOfsearchFor = bmpData.Scan0;
                _bitmap.UnlockBits(bmpData);
                _searchForBytes = new byte[_bytesTotaler];


                ptrSearchFor = (byte*)ptrOfsearchFor.ToPointer();
















                wid = _textureDescriptionFinal.Width / 10;
                hgt = _textureDescriptionFinal.Height / 10;

                strider = wid * 4;


                for (int i = 0; i < arrayOfImage.Length; i++)
                {
                    arrayOfImage[i] = new int[wid * hgt * 4];
                }












            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }
        }
        void writeTo(string test)
        {
            Console.WriteLine(test);
        }

        System.Drawing.Bitmap _searchFor;
        byte[] _searchForBytes;
        IntPtr ptrOfsearchFor;
        byte* ptrSearchFor;



        System.Drawing.Bitmap imager00;

        IntPtr memIntPtr;
        // Get a byte pointer from the IntPtr object.
        byte* memBytePtr;
        bool _hasAcquiredFrame = false;

        [STAThread]
        public SC_SharpDX_ScreenFrame ScreenCapture(int timeOut)
        {
            _hasAcquiredFrame = false;
            try
            {
                if (!acquireFrame(timeOut))
                {
                    _hasAcquiredFrame = false;
                    return _frameCaptureData;
                }
                else
                {
                    //releaseFrame();
                    _hasAcquiredFrame = true;
                }

                if (!copyResource())
                {
                    //Console.WriteLine("has NOT copyResource");
                    //_hasAcquiredFrame = false;
                    //return _frameCaptureData;
                }

                if (!mapSubResource())
                {
                    Console.WriteLine("has NOT mapSubResource");
                    //return _frameCaptureData;
                    //_hasAcquiredFrame = false;
                }
            }
            catch //(SharpDXException ex)
            {
                //Console.WriteLine(ex.ToString());
            }

            if (_hasAcquiredFrame)
            {
                releaseFrame();
            }
            //Console.WriteLine("has aquired " + " = " + _hasAcquiredFrame);



            return _frameCaptureData;
            /*finally
            {
                if (_hasAcquiredFrame)
                {

                }
            }*/

            /*if (!releaseFrame())
            {
                return _frameCaptureData;
            }

            return _frameCaptureData;*/
            // Try to get duplicated frame within given time        
        }

        int _testCounter = 0;

        Stopwatch _testWatch = new Stopwatch();

        int _totalFrames = 0;
        bool _startStopWatch = true;
        bool acquireFrame(int timeOut)
        {
            _screenResource = null;
            try
            {
                /*if (_startStopWatch)
                {
                    _testWatch.Stop();
                    _testWatch.Reset();
                    _testWatch.Start();
                    _startStopWatch = false;
                }*/

                //_duplicateFrameInformation = new OutputDuplicateFrameInformation();
                //_screenResource = new Resource(IntPtr.Zero);

                //var result = this.TryAcquireNextFrame(timeoutInMilliseconds, out frameInfoRef, out desktopResourceOut);result.CheckError();

                //_outputDuplication.AcquireNextFrame(timeOut, out _duplicateFrameInformation, out _screenResource);
                //Console.WriteLine(_totalFrames);
                /*if (_totalFrames >= 0)
                {
                    //_testWatch.Stop();
                    //int _milli = _testWatch.Elapsed.Milliseconds;
                    //Console.WriteLine("_milli" + "***" + _milli);

                    //Console.WriteLine(_result.Success);
                    _totalFrames = 0;
                }*/

                SharpDX.Result _result = _outputDuplication.TryAcquireNextFrame(timeOut, out _duplicateFrameInformation, out _screenResource);




                //if (_milli <= 0)
                //{
                //    Thread.Sleep(100);
                //}

                //if (_testCounter >= 100)
                //{
                //    _testCounter = 0;
                //}
                //Thread.Sleep(10);
                //Console.WriteLine("_testCounter" + "***" + _testCounter );
                //_testCounter++;
            }
            catch (SharpDXException ex)
            {
                /*if (ex.ResultCode.Code == SharpDX.DXGI.ResultCode.WaitTimeout.Result.Code)
                {

                    return true;
                }*/

                //Console.WriteLine(ex.ToString());

                /*if (ex.ResultCode.Failure)
                {
                    //var yo = SharpDX.DXGI.DXGIDebug.;
                    //Configuration.EnableObjectTracking = false;
                    //SharpDX.DXGI.DXGIDebug.LogMemoryLeakWarning = fuckOff;
                    //SharpDX.DXGI.DXGIDebug1
                    //Thread.Sleep(10);
                    //Console.WriteLine("_testCounter" + "***" + _testCounter );                
                    _testCounter++;
                }*/
                //GC.Collect();
            }
            _totalFrames++;

            if (_screenResource != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        public void Disposer()
        {

            _device.Dispose();
            _output1.Dispose();
            _texture2D.Dispose();
            _outputDuplication.Dispose();
            //_textureDescription.Dispose();
            _bitmap.Dispose();
            _bitmapPlayerRect.Dispose();
            _screenResource.Dispose();
            //_frameCaptureData.Dispose();
            GC.Collect();
        }




        IntPtr ptrIntInit;
        //UnmanagedMemoryStream _unmanagedMemoryStreamPlayerRect = new UnmanagedMemoryStream(0, 0,0);


        /*void SetColor(System.Drawing.Color color)
        {
            _current[0] = color.R;
            _current[1] = color.G;
            _current[2] = color.B;
        }*/

        Texture2D _lastTexture2DFinal;
        Texture2D smallerTexture;
        ShaderResourceView smallerTextureView;
        Stopwatch _SystemTickPerformance = new Stopwatch();


        const int num_cols = 10;// image.Width / wid;
        const int num_rows = 10;// image.Height / hgt;
        const int totalDimension = num_cols * num_rows;

        int[][] arrayOfImage = new int[totalDimension][];


        int wid = 0;
        int hgt = 0;

        int strider = 0;


        bool copyResource()
        {
            try
            {
                //MessageBox((IntPtr)0, screenTexture2D.Description.BindFlags.ToString() + "", "Oculus Error", 0);
                using (var screenTexture2D = _screenResource.QueryInterface<Texture2D>())
                {
                    _device.ImmediateContext.CopyResource(screenTexture2D, _texture2D);

                    /*var dataBox = _device.ImmediateContext.MapSubresource(screenTexture2D, 0, SharpDX.Direct3D11.MapMode.Write, SharpDX.Direct3D11.MapFlags.None);

                    int memoryBitmapStride = _textureDescription.Width * 4;

                    int columns = _textureDescription.Width;
                    int rows = _textureDescription.Height;
                    IntPtr interptr = dataBox.DataPointer;

                    if (dataBox.RowPitch == memoryBitmapStride)
                    {
                        // Stride is the same
                        Marshal.Copy(interptr, _textureByteArray, 0, _bytesTotal);
                    }
                    else
                    {
                        // Stride not the same - copy line by line
                        for (int y = 0; y < _textureDescription.Height; y++)
                        {
                            Marshal.Copy(interptr + y * dataBox.RowPitch, _textureByteArray, y * memoryBitmapStride, memoryBitmapStride);
                        }
                    }


                    System.Drawing.Bitmap image = new System.Drawing.Bitmap(columns, rows, memoryBitmapStride, PixelFormat.Format32bppArgb, interptr);
                    var texture = new Texture2D(SC_skYaRk_VR_V007.SC_Console_DIRECTX._dxDevice.Device, new Texture2DDescription()
                    {
                        CpuAccessFlags = CpuAccessFlags.None,
                        BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                        Format = Format.B8G8R8A8_UNorm,
                        Width = columns,
                        Height = rows,
                        OptionFlags = ResourceOptionFlags.GenerateMipMaps,
                        MipLevels = 1,
                        ArraySize = 1,
                        SampleDescription = { Count = 1, Quality = 0 },
                        Usage = ResourceUsage.Default
                    }, new DataRectangle(interptr, memoryBitmapStride));
                    
                    _device.ImmediateContext.UnmapSubresource(screenTexture2D, 0);*/
                }

                /*_device.ImmediateContext.CopyResource(_texture2D, _texture2DFinal);

                var dataBox = _device.ImmediateContext.MapSubresource(_texture2D, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                int memoryBitmapStride = _textureDescription.Width * 4;

                int columns = _textureDescription.Width;
                int rows = _textureDescription.Height;

                IntPtr interptr = dataBox.DataPointer;
                _arrayOfIntPTR.Add(interptr);

                _device.ImmediateContext.UnmapSubresource(_texture2D, 0);
                //if (dataBox.RowPitch == memoryBitmapStride)
                //{
                //    // Stride is the same
                //    Marshal.Copy(interptr, _textureByteArray, 0, _bytesTotal);
                //}
                //else
                ///{
                //    // Stride not the same - copy line by line
                //    for (int y = 0; y < _textureDescription.Height; y++)
                //    {
                //        Marshal.Copy(interptr + y * dataBox.RowPitch, _textureByteArray, y * memoryBitmapStride, memoryBitmapStride);
                //    }
                //}



                //var ptr = (byte*)interptr.ToPointer();
                //byte[] _ptr = new byte[_textureByteArray.Length];

       
             
               

                int _bytesToTransferWidth = wid * 4;
                int _bytesToTransferHeight = hgt * 4;
                int _cIndex = 0;
                int byteIndexPos = 0;
                int byteOffsetterHeight = (_textureDescriptionFinal.Height - hgt) * 4;
                int byteOffsetterWidth = (_textureDescriptionFinal.Width - wid) * 4;
                int totalBytesOffsetSrc = 0;
                int totalBytesOffsetDest = _bytesToTransferWidth;
                



                int x = 0;
                int y = 0;
                int ii = 0;
                int* i_ptr;
                int* xx = &x;
                int* yy = &y;
                int* _w = &columns;
                int* _h = &rows;
                int _zero = 0;
                int* _z = &_zero;
                int* _loopH;
                int* tboss = &_zero;



                //int* _ww = &wid;
                //int* _hh = &hgt;

                int iii = 1;
                int* _zz = &iii;

                int x_ = 0;
                int y_ = 0;
                int _lh = 0;


                _SystemTickPerformance.Stop();
                _SystemTickPerformance.Reset();
                _SystemTickPerformance.Start();


                for (i_ptr = &ii; *i_ptr < 3; (*i_ptr)++)
                {
                    iii = 0;

                    if (*xx >= *_w)
                    {
                        *yy += hgt; //_hh
                        *xx = *_z;
                    }

                    for (_loopH = &iii; *_loopH < hgt - 1; (*_loopH)++)
                    {
                        x_ = *xx;
                        y_ = *yy;
                        _lh = *_loopH;

                        totalBytesOffsetDest = _bytesToTransferWidth * (_lh);
                        totalBytesOffsetSrc = ((x_) + (((y_) + (_lh)) * _textureDescriptionFinal.Width)) * 4;
                        Marshal.Copy(interptr + totalBytesOffsetSrc, (int[])arrayOfImage[(*i_ptr)], totalBytesOffsetDest, _bytesToTransferWidth);
                    }
                    if (*xx < *_w)
                    {
                        *xx += wid; //_ww
                    }
                }

                
                //DeleteObject(interptr);
                //MessageBox((IntPtr)0, _SystemTickPerformance.Elapsed.Milliseconds + "", "Oculus Error", 0);

                shaderRes = new ShaderResourceView(SC_skYaRk_VR_V007.SC_Console_DIRECTX._dxDevice.Device, _texture2DFinal, resourceViewDescription);

                _device.ImmediateContext.GenerateMips(shaderRes);

                if (shaderRes != null)
                {
                    _frameCaptureData._ShaderResource = shaderRes;
                }

                if (_lastShaderResourceView != null)
                {
                    _lastShaderResourceView.Dispose();
                }
                _lastShaderResourceView = shaderRes;
                
                */




               




                /*if (_arrayOfIntPTR.Count > 0)
                {
                    if (_counterForPTR > 10)
                    {
                        for (int i = 0; i < (int)Math.Ceiling((double)_arrayOfIntPTR.Count * _howFast); i++)
                        {
                            DeleteObject(_arrayOfIntPTR[i]);
                        }
                        _counterForPTR = 0;
                    }                 
                }

                _counterForPTR++;*/
                return true;       
                //image.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }

        List<IntPtr> _arrayOfIntPTR = new List<IntPtr>();
        int _counterForPTR = 0;
        float _howFast = 0.1f;



        int _someCounter = 0;

        private bool IsInCapture(IntPtr searchForPTR, IntPtr searchInPTR)
        {
            var ptrSearchIn = (byte*)searchInPTR.ToPointer();

            for (int x = 0; x < _textureDescriptionFinal.Width; x++)
            {
                for (int y = 0; y < _textureDescriptionFinal.Height; y++)
                {
                    var bytePoser0 = (((y) * _textureDescriptionFinal.Width) + (x)) * 4;

                    bool invalid = false;
                    int k = x, l = y;
                    for (int a = 0; a < _searchFor.Width; a++)
                    {
                        l = y;
                        for (int b = 0; b < _searchFor.Height; b++)
                        {
                            var bytePoser1 = (((a) * _searchFor.Width) + (b)) * 4;

                            if (ptrSearchIn[bytePoser0] != ptrSearchFor[bytePoser1])
                            {
                                invalid = true;
                                break;
                            }
                            else
                                l++;
                            /*if (searchFor.GetPixel(a, b) != searchIn.GetPixel(k, l))
                            {
                                invalid = true;
                                break;
                            }
                            else
                                l++;*/
                        }
                        if (invalid)
                            break;
                        else
                            k++;
                    }
                    if (!invalid)
                        return true;
                }
            }
            return false;
        }


        //https://stackoverflow.com/questions/15975972/copy-data-from-from-intptr-to-intptr
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);






        static byte[] PadLines(byte[] bytes, int rows, int columns)
        {
            int currentStride = columns; // 3
            int newStride = columns;  // 4
            byte[] newBytes = new byte[newStride * rows];
            for (int i = 0; i < rows; i++)
            {
                System.Buffer.BlockCopy(bytes, currentStride * i, newBytes, newStride * i, currentStride);
            }
            return newBytes;
        }




        static unsafe void CustomCopy(void* dest, void* src, int count)
        {
            int block;

            block = count >> 3;

            long* pDest = (long*)dest;
            long* pSrc = (long*)src;

            for (int i = 0; i < block; i++)
            {
                *pDest = *pSrc; pDest++; pSrc++;
            }
            dest = pDest;
            src = pSrc;
            count = count - (block << 3);

            if (count > 0)
            {
                byte* pDestB = (byte*)dest;
                byte* pSrcB = (byte*)src;
                for (int i = 0; i < count; i++)
                {
                    *pDestB = *pSrcB; pDestB++; pSrcB++;
                }
            }
        }

        /*void Copy(int itemCount,IntPtr<object^> firstItemArray0, IntPtr<object^> firstItemArray1)
        {
            for (int i = 0; i < itemCount; i++)
            {
                firstItemArray1[i] = firstItemArray0[i];
            }
        }*/






        int imageCounter = 0;

        public byte[] _textureByteArray;
        public byte[] _previousTextureByteArray;
        public byte[] _dummyTextureByteArray;


        public byte[] _currentByteArray;

        int i = 0;



        Stopwatch _countingTime = new Stopwatch();


        byte[] _totalArray = new byte[460 * 400 * 4];
        byte[] _widthScreen = new byte[1920 * 4];


        MemoryStream _memoryStream = new MemoryStream();

        MemoryStream _lastMemoryStream = new MemoryStream();

        byte[] _buffer;
        byte[] _bufferPlayerShipRect;

        int counter = 0;

        

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        ShaderResourceView shaderRes;
        ShaderResourceViewDescription resourceViewDescription;

        bool mapSubResource()
        {
            try
            {
                //https://stackoverflow.com/questions/24064837/resizing-a-dxgi-resource-or-texture2d-in-sharpdx/24070935#24070935


                //_device.ImmediateContext.CopySubresourceRegion(_texture2D, 0, null, _texture2DFinal, 0);
                _device.ImmediateContext.CopyResource(_texture2D, _texture2DFinal);
                
                resourceViewDescription = new ShaderResourceViewDescription
                {
                    Format = _texture2DFinal.Description.Format,
                    Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D,
                    Texture2D = new ShaderResourceViewDescription.Texture2DResource
                    {
                        MipLevels = -1,
                        MostDetailedMip = 0
                    }
                };

                shaderRes = new ShaderResourceView(SC_skYaRk_VR_V007.SC_Console_DIRECTX._dxDevice.Device, _texture2DFinal, resourceViewDescription);

                _device.ImmediateContext.GenerateMips(shaderRes);

                if (shaderRes != null)
                {
                    //Console.WriteLine("test0");
                    _frameCaptureData._ShaderResource = shaderRes;
                }
                if (_lastShaderResourceView != null)
                {
                    _lastShaderResourceView.Dispose();
                }
                _lastShaderResourceView = shaderRes;






                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                releaseFrame();
                return false;
            }
            return true;
        }



            


















        

        private void ProcessModifiedRegions()
        {
            var numberOfBytes = 0;
            var rectangles = new RawRectangle[_duplicateFrameInformation.TotalMetadataBufferSize];

            if (_duplicateFrameInformation.TotalMetadataBufferSize > 0)
            {
                _outputDuplication.GetFrameDirtyRects(rectangles.Length, rectangles, out numberOfBytes);
            }

            ScreenFrameRectangle[] test = new ScreenFrameRectangle[numberOfBytes / Marshal.SizeOf(typeof(RawRectangle))];

            /*var numberOfBytes = 0;
            var rectangles = new OutputDuplicateMoveRectangle[_duplicateFrameInformation.TotalMetadataBufferSize];

            Console.WriteLine(rectangles.Length);

            var numberOfBytes = 0;
            var rectangles = new OutputDuplicateMoveRectangle[_duplicateFrameInformation.TotalMetadataBufferSize];

            if (_duplicateFrameInformation.TotalMetadataBufferSize > 0)
            {
                _outputDuplication.GetFrameMoveRects(rectangles.Length, rectangles, out numberOfBytes);
            }
            ScreenFrameRegion[] test = new ScreenFrameRegion[numberOfBytes / Marshal.SizeOf(typeof(OutputDuplicateMoveRectangle))];


            Console.WriteLine(test.Length);*/


            //Console.WriteLine(numberOfBytes);
            //Console.WriteLine(rectangles[0].Bottom + "_" + rectangles[1].Left + "_" + rectangles[2].Right + "_" + rectangles[3].Top);
            //_frame.ModifiedRegions = new ScreenFrameRectangle[numberOfBytes / Marshal.SizeOf(typeof(RawRectangle))];
            /*for (var i = 0; i < rectangles.Length; i++)
            {
                rectangles[i].Bottom
                rectangles[i].Left;
                rectangles[i].Right;
                rectangles[i].Top;
            }*/
        }


        public struct ScreenFrameRegion
        {
            public ScreenFrameRectangle Destination;
            public int X;
            public int Y;
        }


        public struct ScreenFrameRectangle
        {
            public int Bottom;
            public int Left;
            public int Right;
            public int Top;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        //https://stackoverflow.com/questions/9668872/how-to-get-windows-position
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        Process _voidExpanse = null;
        Process _SCSkyArk = null;

        string targetProcessName0 = "voidexpanse";
        string targetProcessName2 = "server";
        string targetProcessName1 = "SC_SkyArk";


        IntPtr handleVoidExpanse = IntPtr.Zero;
        IntPtr handleSCSkyArk = IntPtr.Zero;

        bool releasedFrame = true;
        bool releaseFrame()
        {
            //_texture2D.Dispose(); // lags like fucking hell
            for (int i = 0; i < 2; i++)
            {
                releasedFrame = true;
                try
                {
                    _outputDuplication.ReleaseFrame();
                }
                catch (SharpDXException ex)
                {
                    releasedFrame = false;
                    Console.WriteLine(ex.ToString());
                }

                if (releasedFrame)
                {
                    break;
                }
            }
            if (releasedFrame)
            {
                return true;
            }
            else
            {
                SC_skYaRk_VR_V007.SC_Console_GRAPHICS._desktopDupe = new SC_SharpDX_ScreenCapture(0, 0);
                return false;
            }
        }
    }
}



/*int wid = _textureDescriptionFinal.Width / 10;
int hgt = _textureDescriptionFinal.Height / 10;
System.Drawing.Bitmap piece = new System.Drawing.Bitmap(wid, hgt);
System.Drawing.Rectangle dest_rect = new System.Drawing.Rectangle(0, 0, wid, hgt);
using (Graphics gr = Graphics.FromImage(piece))
{
int num_rows = image.Height / hgt;
int num_cols = image.Width / wid;
System.Drawing.Rectangle source_rect = new System.Drawing.Rectangle(0, 0, wid, hgt);
for (int row = 0; row < num_rows; row++)
{
    source_rect.X = 0;
    for (int col = 0; col < num_cols; col++)
    {
        gr.DrawImage(image, dest_rect, source_rect,GraphicsUnit.Pixel);
        piece.Save(@"C:\Users\steve\OneDrive\Desktop\screenRecord\" + row.ToString("00") + col.ToString("00") + ".png");
        source_rect.X += wid;
    }
    source_rect.Y += hgt;
}
}*/


//FastMemCopy.FastMemoryCopy(interptr + totalBytesOffsetSrc, arrayOfImagePTR[(*i_ptr)] + totalBytesOffsetDest, _bytesToTransferWidth);
