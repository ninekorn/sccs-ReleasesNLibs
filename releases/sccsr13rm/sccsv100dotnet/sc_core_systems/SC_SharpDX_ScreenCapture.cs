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
//using Ab3d.Common;
//using Ab3d.Common.Cameras;
//using Ab3d.Controls;
//using Ab3d.DirectX;
//using Ab3d.DirectX.Controls;
//using Ab3d.OculusWrap;
//using Ab3d.Visuals;
//using Ab3d.DXEngine.OculusWrap;
//using Ab3d.DirectX.Materials;

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

namespace _sc_core_systems
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

        public SC_SharpDX_ScreenCapture(int adapter, int numOutput, SharpDX.Direct3D11.Device device_)
        {
            _frameCaptureData = new SC_SharpDX_ScreenFrame();

            _numAdapter = adapter;
            _numOutput = numOutput;


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
                //this._device = new Device(_adapter);
                //this._device = _sc_core_systems.SC_Console_DIRECTX._dxDevice.Device;

                this._device = device_;
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
            }
            catch //(SharpDXException ex)
            {
                //Console.WriteLine(ex.ToString());
            }

            if (_hasAcquiredFrame)
            {
                releaseFrame();
            }
            return _frameCaptureData;   
        }

        bool acquireFrame(int timeOut)
        {
            _screenResource = null;
            try
            {
                SharpDX.Result _result = _outputDuplication.TryAcquireNextFrame(timeOut, out _duplicateFrameInformation, out _screenResource);
            }
            catch (SharpDXException ex)
            {
               
            }

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
                    var texture = new Texture2D(_sc_core_systems.SC_Console_DIRECTX._dxDevice.Device, new Texture2DDescription()
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

                _device.ImmediateContext.CopyResource(_texture2D, _texture2DFinal);

                /*var dataBox = _device.ImmediateContext.MapSubresource(_texture2D, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                int memoryBitmapStride = _textureDescription.Width * 4;

                int columns = _textureDescription.Width;
                int rows = _textureDescription.Height;

                IntPtr interptr = dataBox.DataPointer;
                _arrayOfIntPTR.Add(interptr);

                _device.ImmediateContext.UnmapSubresource(_texture2D, 0);*/
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





                /*int _bytesToTransferWidth = wid * 4;
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
                 }*/


                //DeleteObject(interptr);
                //MessageBox((IntPtr)0, _SystemTickPerformance.Elapsed.Milliseconds + "", "Oculus Error", 0);

                shaderRes = new ShaderResourceView(_device, _texture2DFinal, resourceViewDescription);

                //shaderRes = new ShaderResourceView();





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

        



        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        ShaderResourceView shaderRes;
        ShaderResourceViewDescription resourceViewDescription;

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
                SC_Console_GRAPHICS._desktopDupe = new SC_SharpDX_ScreenCapture(0, 0, this._device); // not that good but let's leave it at that.
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
