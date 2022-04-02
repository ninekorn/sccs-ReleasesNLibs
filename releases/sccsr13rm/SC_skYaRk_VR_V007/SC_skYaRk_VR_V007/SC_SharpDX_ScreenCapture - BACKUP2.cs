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
        SC_SharpDX_ScreenFrame _lastFrameCaptureData;
        SC_SharpDX_ScreenFrame _frameCaptureData;
        int _width = 0;
        int _height = 0;
        readonly Output1 _output1;
        readonly Texture2DDescription _textureDescription;
        readonly Texture2DDescription _textureDescriptionFinal;
        readonly OutputDuplication _outputDuplication;
        Texture2D _texture2D;
        Texture2D _texture2DFinal;

        Texture2D _last_texture2D;
        Texture2D _last_texture2DFinal;

        SharpDX.DXGI.Resource _screenResource;
        SharpDX.Direct3D11.Device _device;
        public ShaderResourceView _lastShaderResourceView;
        OutputDuplicateFrameInformation _duplicateFrameInformation;

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




        /*
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




        int _bytesTotal;
        int _bytesTotalPlayerRect;



        SharpDX.DXGI.Resource _screenResource;
        OutputDuplicateFrameInformation _duplicateFrameInformation;
        OutputDuplicateFrameInformation _previousDuplicateFrameInformation;

            */
        //Bitmap desktopBMP;
        
        /*byte[] _emptyArrayPaste;
        byte[] _arrayPlayerPos;
        byte[] _arrayPreviousPlayerPos;
        byte[] _currentArrayPlayerPos;*/


        //[System.Runtime.InteropServices.DllImport("gdi32.dll")]
        //public static extern bool DeleteObject(IntPtr hObject);

        //static Thread _thread;
        //Action<string> fuckOff;



        UnmanagedMemoryStream _unmanagedMemoryStreamPlayerRect;// = new UnmanagedMemoryStream
        //int _size = Marshal.SizeOf(_textureByteArray[0] * _textureByteArray.Length);
        //var memIntPtr = Marshal.AllocHGlobal(_size);

        public SC_SharpDX_ScreenCapture(int adapter, int numOutput)
        {
            _frameCaptureData = new SC_SharpDX_ScreenFrame();

            int _numAdapter = adapter;
            int  _numOutput = numOutput;
            Adapter1 _adapter;



            /*_currentByteArray = new byte[1920 * 1080 * 4];

            for (int i = 0; i < _currentByteArray.Length; i++)
            {
                _currentByteArray[i] = 0;
            }

            _previousTextureByteArray = new byte[1920 * 1080 * 4];

            for (int i = 0; i < _previousTextureByteArray.Length; i++)
            {
                _previousTextureByteArray[i] = 0;
            }*/


            try
            {
                using (var _factory = new SharpDX.DXGI.Factory1())
                {
                    _adapter = _factory.GetAdapter1(_numAdapter);
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
                _device = SC_skYaRk_VR_V007.SC_Console_DIRECTX._dxDevice.Device;


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
                    _output1 = _output.QueryInterface<Output1>();
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
                _outputDuplication = _output1.DuplicateOutput(_device);
            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }

            try
            {
                //getTextureDescription();
                _textureDescription = new Texture2DDescription
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

                _textureDescriptionFinal = new Texture2DDescription
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
                /*_bitmap = new System.Drawing.Bitmap(_width, _height, PixelFormat.Format32bppArgb);
                var boundsRect = new System.Drawing.Rectangle(0, 0, _width, _height);
                var bmpData = _bitmap.LockBits(boundsRect, ImageLockMode.ReadOnly, _bitmap.PixelFormat);
                _bytesTotal = Math.Abs(bmpData.Stride) * _bitmap.Height;
                _bitmap.UnlockBits(bmpData);
                _textureByteArray = new byte[_bytesTotal];
                */
                /*_bitmapPlayerRect = new System.Drawing.Bitmap(widthOfRectanglePlayerShip, heightOfRectanglePlayerShip, PixelFormat.Format32bppArgb);
                var boundsRect0 = new System.Drawing.Rectangle(0, 0, widthOfRectanglePlayerShip, heightOfRectanglePlayerShip);
                var bmpData0 = _bitmapPlayerRect.LockBits(boundsRect0, ImageLockMode.ReadOnly, _bitmapPlayerRect.PixelFormat);
                _bytesTotalPlayerRect = Math.Abs(bmpData0.Stride) * _bitmapPlayerRect.Height;
                _bitmapPlayerRect.UnlockBits(bmpData0);*/


                /*string path = System.IO.Path.GetFullPath(@"..\..\..\screenRec0.png");
                _searchFor = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromFile(path);
                boundsRect = new System.Drawing.Rectangle(0, 0, _searchFor.Width, _searchFor.Height);
                bmpData = _bitmap.LockBits(boundsRect, ImageLockMode.ReadOnly, _bitmap.PixelFormat);
                var _bytesTotaler = Math.Abs(bmpData.Stride) * _bitmap.Height;
                ptrOfsearchFor = bmpData.Scan0;
                _bitmap.UnlockBits(bmpData);
                _searchForBytes = new byte[_bytesTotaler];


                ptrSearchFor = (byte*)ptrOfsearchFor.ToPointer();
                */















                /*wid = _textureDescriptionFinal.Width / 10;
                hgt = _textureDescriptionFinal.Height / 10;

                strider = wid * 4;


                for (int i = 0; i < arrayOfImage.Length; i++)
                {
                    arrayOfImage[i] = new int[wid * hgt * 4];
                }*/












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
                    //return _lastFrameCaptureData;
                    _frameCaptureData = _lastFrameCaptureData;
                }
                else
                {

                }
                /*if (!mapSubResource())
                {
                    Console.WriteLine("has NOT mapSubResource");
                    //return _frameCaptureData;
                    //_hasAcquiredFrame = false;
                }*/
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

        int _testCounter = 0;

        Stopwatch _testWatch = new Stopwatch();

        int _totalFrames = 0;
        bool _startStopWatch = true;
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
            //_bitmap.Dispose();
            //_bitmapPlayerRect.Dispose();
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




        bool copyResource()
        {
            try
            {
                //MessageBox((IntPtr)0, screenTexture2D.Description.BindFlags.ToString() + "", "Oculus Error", 0);
                if (_screenResource != null)
                {
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

                    _device.ImmediateContext.CopyResource(_texture2D, _texture2DFinal);

                    shaderRes = new ShaderResourceView(_device, _texture2DFinal, resourceViewDescription);



                    _device.ImmediateContext.GenerateMips(shaderRes);

                    if (shaderRes != null)
                    {
                        _frameCaptureData._ShaderResource = shaderRes;
                    }


                    /* (_lastShaderResourceView != null)
                    {
                        //_lastShaderResourceView.Dispose();
                        _lastShaderResourceView.Resource.Dispose();
                    }*/

           

                    _lastShaderResourceView = shaderRes;

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }




        ShaderResourceView shaderRes;
        ShaderResourceViewDescription resourceViewDescription;

        bool mapSubResource()
        {
            try
            {
                //https://stackoverflow.com/questions/24064837/resizing-a-dxgi-resource-or-texture2d-in-sharpdx/24070935#24070935


                //_device.ImmediateContext.CopySubresourceRegion(_texture2D, 0, null, _texture2DFinal, 0);
                //_device.ImmediateContext.CopyResource(_texture2D, _texture2DFinal);





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





                shaderRes = new ShaderResourceView(_device, _texture2DFinal, resourceViewDescription);

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




                _lastFrameCaptureData = _frameCaptureData;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                releaseFrame();
                return false;
            }
            return true;
        }

        bool releasedFrame = true;
        bool releaseFrame()
        {
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

