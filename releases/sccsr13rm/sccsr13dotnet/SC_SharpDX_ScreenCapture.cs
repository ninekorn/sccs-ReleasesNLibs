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
using System.IO;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.WIC;
using Device = SharpDX.Direct3D11.Device;
using MapFlags = SharpDX.Direct3D11.MapFlags;
using System.Runtime.InteropServices;
using System.Windows.Input;
using SharpDX.Mathematics.Interop;
using Resource = SharpDX.DXGI.Resource;
using ResultCode = SharpDX.DXGI.ResultCode;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using Ab3d;
using SharpDX.Direct3D;
using System.Reflection;
using SharpDX.WIC;
using System;
using System.Diagnostics;
using System.IO;
using SharpDX;
using SharpDX.DXGI;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.IO;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using D2DPixelFormat = SharpDX.Direct2D1.PixelFormat;
using WicPixelFormat = SharpDX.WIC.PixelFormat;
using SharpDX.Win32;
using Bitmap = System.Drawing.Bitmap;
using SCCoreSystems.sc_console;

//http://csharphelper.com/blog/2016/06/split-image-files-in-c/
//https://stackoverflow.com/questions/15975972/copy-data-from-from-intptr-to-intptr

//using System.Windows.Media.Imaging;
//using System.Windows.Forms;
//using Device = SharpDX.Direct3D11.Device;
//using MapFlags = SharpDX.Direct3D11.MapFlags;
//using System.Windows.Interop;
//using Ab3d.DirectX.Models;
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
//using Windows.Storage.Streams;
//using Bitmap = SharpDX.WIC.Bitmap;


namespace SCCoreSystems
{
    /// <summary>
    ///   Screen capture of the desktop using DXGI OutputDuplication.
    /// </summary>
    public unsafe class SC_SharpDX_ScreenCapture
    {
        public DTerrainHeightMap.DHeightMapType[] arrayOfPixData;

        public ShaderResourceView _lastShaderResourceView;
        public ShaderResourceView[] _lastShaderResourceViewArray;
        public ShaderResourceView[] _ShaderResourceViewArray;

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
        public static Texture2DDescription _textureDescriptionFinal;
        readonly Texture2DDescription _textureDescriptionFinalFrac;
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

        public byte[] _textureByteArray;//= new byte[1];

        UnmanagedMemoryStream _unmanagedMemoryStreamPlayerRect;// = new UnmanagedMemoryStream
                                                               //int _size = Marshal.SizeOf(_textureByteArray[0] * _textureByteArray.Length);
                                                               //var memIntPtr = Marshal.AllocHGlobal(_size);

        int[] pastearray;// = new int[1];
        int[] pastearrayTwo;

        IntPtr[] imageptrList;
        System.Drawing.Bitmap piece;
        System.Drawing.Rectangle dest_rect;
        System.Drawing.Rectangle source_rect;

        Graphics gr;// = Graphics.FromImage(piece)

        Texture2D[] arrayOfTexture2DFrac;



        public SC_SharpDX_ScreenCapture(int adapter, int numOutput, SharpDX.Direct3D11.Device device_)
        {
            //_textureByteArray[0] = 0;
            imageptrList = new IntPtr[num_cols * num_rows];
            _frameCaptureData = new SC_SharpDX_ScreenFrame();

            arrayOfTexture2DFrac = new Texture2D[num_cols * num_rows];

            pastearray = new int[num_cols * num_rows];
            pastearrayTwo = new int[num_cols * num_rows];

            arrayOfBytesTwo = new byte[_textureDescriptionFinal.Width * _textureDescriptionFinal.Height];

            _lastShaderResourceViewArray = new ShaderResourceView[num_cols * num_rows];
            _ShaderResourceViewArray = new ShaderResourceView[num_cols * num_rows];


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
                //this._device = SCCoreSystems.SC_Console_DIRECTX._dxDevice.Device;

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




                wid = _textureDescriptionFinal.Width / num_cols;
                hgt = _textureDescriptionFinal.Height / num_rows;

                /*this._textureDescriptionFinalFrac = new Texture2DDescription
                {
                    CpuAccessFlags = CpuAccessFlags.None,
                    BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                    Format = Format.B8G8R8A8_UNorm,
                    Width = wid,
                    Height = hgt,
                    OptionFlags = ResourceOptionFlags.GenerateMipMaps,
                    MipLevels = 1,
                    ArraySize = 1,
                    SampleDescription = { Count = 1, Quality = 0 },
                    Usage = ResourceUsage.Default
                };*/

                this._textureDescriptionFinalFrac = new Texture2DDescription
                {
                    CpuAccessFlags = CpuAccessFlags.Read,
                    BindFlags = BindFlags.None,//BindFlags.None, //| BindFlags.RenderTarget
                    Format = Format.B8G8R8A8_UNorm,
                    Width = wid,
                    Height = hgt,
                    OptionFlags = ResourceOptionFlags.None,
                    MipLevels = 1,
                    ArraySize = 1,
                    SampleDescription = { Count = 1, Quality = 0 },
                    Usage = ResourceUsage.Staging
                };










                piece = new Bitmap(wid, hgt);
                gr = Graphics.FromImage(piece);
                dest_rect = new System.Drawing.Rectangle(0, 0, wid, hgt);

                strider = wid * 4;

                for (int i = 0; i < arrayOfImage.Length; i++)
                {
                    arrayOfImage[i] = new int[wid * hgt * 4];
                }

                for (int i = 0; i < arrayOfBytes.Length; i++)
                {
                    arrayOfBytes[i] = new byte[wid * hgt * 4];
                }


                piece = new System.Drawing.Bitmap(wid, hgt);
                dest_rect = new System.Drawing.Rectangle(0, 0, wid, hgt);

                //int num_rows = _textureDescriptionFinal.Height / hgt;
                //int num_cols = _textureDescriptionFinal.Width / wid;
                source_rect = new System.Drawing.Rectangle(0, 0, wid, hgt);


                for (int tex2D = 0; tex2D < 10 * 10; tex2D++)
                {
                    arrayOfTexture2DFrac[tex2D] = new Texture2D(_device, _textureDescriptionFinalFrac);
                }



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

            _bitmap = new System.Drawing.Bitmap(_width, _height, PixelFormat.Format32bppArgb);
            var boundsRect = new System.Drawing.Rectangle(0, 0, _width, _height);
            var bmpData = _bitmap.LockBits(boundsRect, ImageLockMode.ReadOnly, _bitmap.PixelFormat);
            _bytesTotal = Math.Abs(bmpData.Stride) * _bitmap.Height;
            _bitmap.UnlockBits(bmpData);
            _textureByteArray = new byte[_bytesTotal];


            arrayOfPixData = new DTerrainHeightMap.DHeightMapType[_bytesTotal];// _width * _height];


            for (int i = 0; i < arrayOfPixData.Length; i++)
            {
                arrayOfPixData[i] = new DTerrainHeightMap.DHeightMapType();

            }






            /*try
            {

            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }*/
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
                    return _frameCaptureData;
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


        Bitmap _lastBitmap;

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



        public static int num_cols = 10;// image.Width / wid;
        public static int num_rows = 10;// image.Height / hgt;
        public static int totalDimension = num_cols * num_rows;

        int[][] arrayOfImage = new int[totalDimension][];
        byte[][] arrayOfBytes = new byte[totalDimension][];


        byte[] arrayOfBytesTwo;// = new byte[];

        public static int wid = 0;
        public static int hgt = 0;

        int strider = 0;
        int imageCounter = 0;

        System.Drawing.Bitmap image;

        //https://stackoverflow.com/questions/15975972/copy-data-from-from-intptr-to-intptr
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        int looponce = 0;
        int index = 0;
        IntPtr interptr0;


        bool copyResource()
        {
            try
            {
                //MessageBox((IntPtr)0, screenTexture2D.Description.BindFlags.ToString() + "", "Oculus Error", 0);
                using (var screenTexture2D = _screenResource.QueryInterface<Texture2D>())
                {
                    /*var texture = new Texture2D(SCCoreSystems.SC_Console_DIRECTX._dxDevice.Device, new Texture2DDescription()
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

                    _device.ImmediateContext.CopyResource(screenTexture2D, _texture2D);
                }




                //DISCARDED TO READD FOR TESTING A NORMAL CREATION OF A BITMAP IMMEDIATELY SHRINKED ON CREATION TO THE SIZE THE USER WANTS... IT WASN'T WORKING.
                //DISCARDED TO READD FOR TESTING A NORMAL CREATION OF A BITMAP IMMEDIATELY SHRINKED ON CREATION TO THE SIZE THE USER WANTS... IT WASN'T WORKING.
                //DISCARDED TO READD FOR TESTING A NORMAL CREATION OF A BITMAP IMMEDIATELY SHRINKED ON CREATION TO THE SIZE THE USER WANTS... IT WASN'T WORKING.
                /*var dataBox1 = _device.ImmediateContext.MapSubresource(_texture2D, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                int memoryBitmapStride = _textureDescription.Width * 4;

                int columns = _textureDescription.Width;
                int rows = _textureDescription.Height;
                IntPtr interptr1 = dataBox1.DataPointer;

                // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (_width * 4)
                /*if (dataBox1.RowPitch == memoryBitmapStride)
                {
                    // Stride is the same
                    Marshal.Copy(interptr1, _textureByteArray, 0, _bytesTotal);
                }
                else
                {
                    // Stride not the same - copy line by line
                    for (int y = 0; y < _height; y++)
                    {
                        Marshal.Copy(interptr1 + y * dataBox1.RowPitch, _textureByteArray, y * memoryBitmapStride, memoryBitmapStride);
                    }
                }

                var somebitmap = new System.Drawing.Bitmap(160, 128, 160 * 4, PixelFormat.Format32bppArgb, interptr1);
                _device.ImmediateContext.UnmapSubresource(_texture2D, 0);
                DeleteObject(interptr1);*/
                //DISCARDED TO READD FOR TESTING A NORMAL CREATION OF A BITMAP IMMEDIATELY SHRINKED ON CREATION TO THE SIZE THE USER WANTS... IT WASN'T WORKING.
                //DISCARDED TO READD FOR TESTING A NORMAL CREATION OF A BITMAP IMMEDIATELY SHRINKED ON CREATION TO THE SIZE THE USER WANTS... IT WASN'T WORKING.
                //DISCARDED TO READD FOR TESTING A NORMAL CREATION OF A BITMAP IMMEDIATELY SHRINKED ON CREATION TO THE SIZE THE USER WANTS... IT WASN'T WORKING.






                //DISCARDED TO READD FOR TESTING COLORS IMMEDIATELLY UNWRAPPED FROM THE TEXTURE2D AND SENT IN THE VERTICES ARRAY FOR USING WITH A HEIGHTMAPS MESH. IT WASN'T WORKING.
                //DISCARDED TO READD FOR TESTING COLORS IMMEDIATELLY UNWRAPPED FROM THE TEXTURE2D AND SENT IN THE VERTICES ARRAY FOR USING WITH A HEIGHTMAPS MESH. IT WASN'T WORKING.
                //DISCARDED TO READD FOR TESTING COLORS IMMEDIATELLY UNWRAPPED FROM THE TEXTURE2D AND SENT IN THE VERTICES ARRAY FOR USING WITH A HEIGHTMAPS MESH. IT WASN'T WORKING.
                /*
                index = 0;
                for (var j = 0; j < rows - 1; j++)
                {
                    for (var i = 0; i < columns - 1; i++)
                    {
                        var bytePoser = ((j * columns) + i);

                        //HeightMapSobel.Add(new DHeightMapType()
                        //{
                        //    x = i,
                        //    y = SC_Update._desktopDupe._textureByteArray[bytePoser],// image.GetPixel(i, j).R,
                        //    z = j
                        //});

                        arrayOfPixData[bytePoser].x = i;
                        arrayOfPixData[bytePoser].y = _textureByteArray[bytePoser];
                        arrayOfPixData[bytePoser].z = j;

                        int indexBottomLeft1 = ((rows * j) + i);          // Bottom left.
                        int indexBottomRight2 = ((rows * j) + (i + 1));      // Bottom right.
                        int indexUpperLeft3 = ((rows * (j + 1)) + i);      // Upper left.
                        int indexUpperRight4 = ((rows * (j + 1)) + (i + 1));  // Upper right.

                        if (sc_graphics_sec.Terrain != null)
                        {
                            if (sc_graphics_sec.Terrain.vertices != null)
                            {
                                if (sc_graphics_sec.Terrain.vertices.Length > 0)
                                {

                                    if (index < sc_graphics_sec.Terrain.vertices.Length)
                                    {
                                        sc_graphics_sec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexUpperLeft3].x, arrayOfPixData[indexUpperLeft3].y, arrayOfPixData[indexUpperLeft3].z);
                                        sc_graphics_sec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexUpperRight4].x, arrayOfPixData[indexUpperRight4].y, arrayOfPixData[indexUpperRight4].z);
                                        sc_graphics_sec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexUpperRight4].x, arrayOfPixData[indexUpperRight4].y, arrayOfPixData[indexUpperRight4].z);
                                        sc_graphics_sec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexBottomLeft1].x, arrayOfPixData[indexBottomLeft1].y, arrayOfPixData[indexBottomLeft1].z);
                                        sc_graphics_sec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexBottomLeft1].x, arrayOfPixData[indexBottomLeft1].y, arrayOfPixData[indexBottomLeft1].z);
                                        sc_graphics_sec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexUpperLeft3].x, arrayOfPixData[indexUpperLeft3].y, arrayOfPixData[indexUpperLeft3].z);
                                        sc_graphics_sec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexBottomLeft1].x, arrayOfPixData[indexBottomLeft1].y, arrayOfPixData[indexBottomLeft1].z);
                                        sc_graphics_sec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexUpperRight4].x, arrayOfPixData[indexUpperRight4].y, arrayOfPixData[indexUpperRight4].z);
                                        sc_graphics_sec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexUpperRight4].x, arrayOfPixData[indexUpperRight4].y, arrayOfPixData[indexUpperRight4].z);
                                        sc_graphics_sec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexBottomRight2].x, arrayOfPixData[indexBottomRight2].y, arrayOfPixData[indexBottomRight2].z);
                                        sc_graphics_sec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexBottomRight2].x, arrayOfPixData[indexBottomRight2].y, arrayOfPixData[indexBottomRight2].z);
                                        sc_graphics_sec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexBottomLeft1].x, arrayOfPixData[indexBottomLeft1].y, arrayOfPixData[indexBottomLeft1].z);

                                    }

                                    index++;
                                    
                                }
                            }
                        }
                    }
                }*/

                //DISCARDED TO READD FOR TESTING COLORS IMMEDIATELLY UNWRAPPED FROM THE TEXTURE2D AND SENT IN THE VERTICES ARRAY FOR USING WITH A HEIGHTMAPS MESH. IT WASN'T WORKING.
                //DISCARDED TO READD FOR TESTING COLORS IMMEDIATELLY UNWRAPPED FROM THE TEXTURE2D AND SENT IN THE VERTICES ARRAY FOR USING WITH A HEIGHTMAPS MESH. IT WASN'T WORKING.
                //DISCARDED TO READD FOR TESTING COLORS IMMEDIATELLY UNWRAPPED FROM THE TEXTURE2D AND SENT IN THE VERTICES ARRAY FOR USING WITH A HEIGHTMAPS MESH. IT WASN'T WORKING.






                //TO READD FOR TESTING UNSAFE POINTERS TO GET THE COLORS OUT OF THE BYTES. NOT SURE IF IT WAS WORKING OT NOT, BUT WHEN I WAS USING THE AB3D.DXENGINE WPF, I WAS ABLE TO MAKE A SIMILAR THING WORK BUT I ONLY FOUND AND RELEASED ON GITHUB THE PROJECT WITH THE COMMENTED UNSAFE POINTER CODE SNIPPETS PARTS OUT.
                //TO READD FOR TESTING UNSAFE POINTERS TO GET THE COLORS OUT OF THE BYTES. NOT SURE IF IT WAS WORKING OT NOT, BUT WHEN I WAS USING THE AB3D.DXENGINE WPF, I WAS ABLE TO MAKE A SIMILAR THING WORK BUT I ONLY FOUND AND RELEASED ON GITHUB THE PROJECT WITH THE COMMENTED UNSAFE POINTER CODE SNIPPETS PARTS OUT.
                //TO READD FOR TESTING UNSAFE POINTERS TO GET THE COLORS OUT OF THE BYTES. NOT SURE IF IT WAS WORKING OT NOT, BUT WHEN I WAS USING THE AB3D.DXENGINE WPF, I WAS ABLE TO MAKE A SIMILAR THING WORK BUT I ONLY FOUND AND RELEASED ON GITHUB THE PROJECT WITH THE COMMENTED UNSAFE POINTER CODE SNIPPETS PARTS OUT.

                /*if (sc_graphics_sec.Terrain != null)
                {
                    if (sc_graphics_sec.Terrain.vertices.Length > 0)
                    {
                        Console.WriteLine(sc_graphics_sec.Terrain.vertices.Length);
                    }
                }*/
                /*byte* ptr = (byte*)interptr.ToPointer();

                int _pixelSize = 3;
                int _nWidth = _textureDescriptionFinal.Width * _pixelSize;
                int _nHeight = _textureDescriptionFinal.Height;
                int counterY = 0;
                int counterX = 0;

                int _nWidthDIV = (_textureDescriptionFinal.Width * _pixelSize) / num_cols;
                int _nWidthDIVTWO = (_textureDescriptionFinal.Width * 4) / num_cols;
                int _nHeightDIV = _textureDescriptionFinal.Height / num_rows;
                int mainArrayIndex = 0;

                int ycount = 0;
                int xcount = 0;*/

                /* byte* ptr = (byte*)interptr.ToPointer();

                 int _pixelSize = 3;
                 int _nWidth = _textureDescriptionFinal.Width * _pixelSize;
                 int _nHeight = _textureDescriptionFinal.Height;
                 int counterY = 0;
                 int counterX = 0;
                 int mainArrayIndex = 0;

                 int someIndexX = 0;
                 int someIndexY = 0;

                 int _nWidthDIV = (_textureDescriptionFinal.Width * _pixelSize) / num_cols;
                 int _nWidthDIVTWO = (_textureDescriptionFinal.Width * 4) / num_cols;
                 int _nHeightDIV = _textureDescriptionFinal.Height / num_rows;

                 for (int y = 0; y < _nHeight; y++)
                 {
                     for (int x = 0; x < _nWidth; x++)
                     {
                         if (x % _pixelSize == 0 || x == 0)
                         {                       
                             var bytePoser = ((y * _nWidth) + x);
                             mainArrayIndex = (counterY * num_cols) + counterX;

                             var test0 = ptr[bytePoser + 0];
                             var test1 = ptr[bytePoser + 1];
                             var test2 = ptr[bytePoser + 2];

                             var indexOfFracturedImageBytes = ((someIndexY) * _nWidthDIV) + someIndexX;

                             try
                             {
                                 arrayOfBytes[mainArrayIndex][indexOfFracturedImageBytes + 0] = test0; //b
                                 arrayOfBytes[mainArrayIndex][indexOfFracturedImageBytes + 1] = test1; //g
                                 arrayOfBytes[mainArrayIndex][indexOfFracturedImageBytes + 2] = test2; //r
                                 arrayOfBytes[mainArrayIndex][indexOfFracturedImageBytes + 3] = 1;     //a
                             }
                             catch (Exception ex)
                             {
                                 MainWindow.MessageBox((IntPtr)0, "index: " + mainArrayIndex + " _ " + indexOfFracturedImageBytes + " _ " + ex.ToString(), "sccs message", 0);
                             }

                             ptr++;
                         }



                         /*if (someIndexY % _nHeightDIV == 0 && someIndexY != 0)
                         {
                             someIndexY = 0;
                         }

                         if (x % _nWidthDIV == 0 && x != 0 && counterX < 9)
                         {
                             counterX++;
                         }

                         someIndexX++;
                         if (someIndexX % _nWidthDIV == 0 && someIndexX != 0)
                         {
                             someIndexX = 0;
                         }
                     }

                     if (y % _nHeightDIV == 0 && y != 0 && counterY < 9)
                     {
                         someIndexY = 0;
                         counterY++;
                         counterX = 0;
                     }
                     someIndexY++;
                 }*/
                //TO READD FOR TESTING UNSAFE POINTERS TO GET THE COLORS OUT OF THE BYTES. NOT SURE IF IT WAS WORKING OT NOT, BUT WHEN I WAS USING THE AB3D.DXENGINE WPF, I WAS ABLE TO MAKE A SIMILAR THING WORK BUT I ONLY FOUND AND RELEASED ON GITHUB THE PROJECT WITH THE COMMENTED UNSAFE POINTER CODE SNIPPETS PARTS OUT.
                //TO READD FOR TESTING UNSAFE POINTERS TO GET THE COLORS OUT OF THE BYTES. NOT SURE IF IT WAS WORKING OT NOT, BUT WHEN I WAS USING THE AB3D.DXENGINE WPF, I WAS ABLE TO MAKE A SIMILAR THING WORK BUT I ONLY FOUND AND RELEASED ON GITHUB THE PROJECT WITH THE COMMENTED UNSAFE POINTER CODE SNIPPETS PARTS OUT.
                //TO READD FOR TESTING UNSAFE POINTERS TO GET THE COLORS OUT OF THE BYTES. NOT SURE IF IT WAS WORKING OT NOT, BUT WHEN I WAS USING THE AB3D.DXENGINE WPF, I WAS ABLE TO MAKE A SIMILAR THING WORK BUT I ONLY FOUND AND RELEASED ON GITHUB THE PROJECT WITH THE COMMENTED UNSAFE POINTER CODE SNIPPETS PARTS OUT.

                _device.ImmediateContext.CopyResource(_texture2D, _texture2DFinal);










                //TO READD FOR DIVIDING THE TEXTURE2D IN MULTIPLE TEXTURE2D OR/AND MULTIPLE BYTE ARRAYS. LAGGY
                //TO READD FOR DIVIDING THE TEXTURE2D IN MULTIPLE TEXTURE2D OR/AND MULTIPLE BYTE ARRAYS. LAGGY
                //TO READD FOR DIVIDING THE TEXTURE2D IN MULTIPLE TEXTURE2D OR/AND MULTIPLE BYTE ARRAYS. LAGGY
                /*source_rect = new System.Drawing.Rectangle(0, 0, _textureDescriptionFinal.Width, _textureDescriptionFinal.Height);

                var region = new ResourceRegion(0, 0, 0, wid, hgt, 1);

                region = new ResourceRegion(source_rect.X, source_rect.Y, 0, _textureDescriptionFinal.Width, _textureDescriptionFinal.Height, 1);

                for (int row = 0; row < num_rows; row++)
                {
                    source_rect.X = 0;

                    for (int col = 0; col < num_cols; col++)
                    {
                        var mainArrayIndex = (row * num_cols) + col;

                        region.Left = source_rect.X;

                        region.Top = source_rect.Y;

                        _device.ImmediateContext.CopySubresourceRegion(_texture2DFinal, 0, region, arrayOfTexture2DFrac[mainArrayIndex], 0);
                        //_ShaderResourceViewArray[mainArrayIndex] = new ShaderResourceView(_device, _texture2DFinal, resourceViewDescription);

                        //_device.ImmediateContext.GenerateMips(_ShaderResourceViewArray[mainArrayIndex]);

                        source_rect.X += wid;
                    }
                    source_rect.Y += hgt;
                }

                source_rect.X = 0;// = new System.Drawing.Rectangle(0, 0, _textureDescriptionFinal.Width, _textureDescriptionFinal.Height);
                source_rect.Y = 0;//
                region.Left = source_rect.X;
                region.Top = source_rect.Y;
                //region = new ResourceRegion(0, 0, 0, wid, hgt, 1);
                //region = new ResourceRegion(source_rect.X, source_rect.Y, 0, _textureDescriptionFinal.Width, _textureDescriptionFinal.Height, 1);


                for (int row = 0; row < num_rows; row++)
                {
                    source_rect.X = 0;

                    for (int col = 0; col < num_cols; col++)
                    {
                        var mainArrayIndex = (row * num_cols) + col;

                        region.Left = source_rect.X;

                        region.Top = source_rect.Y;

                        //_device.ImmediateContext.CopySubresourceRegion(_texture2DFinal, 0, region, arrayOfTexture2DFrac[mainArrayIndex], 0);
                        //_ShaderResourceViewArray[mainArrayIndex] = new ShaderResourceView(_device, _texture2DFinal, resourceViewDescription);
                        //_device.ImmediateContext.GenerateMips(_ShaderResourceViewArray[mainArrayIndex]);

                        //for (int tex2D = 0; tex2D < 10 * 10; tex2D++)
                        //{
                        //    arrayOfTexture2DFrac[tex2D] = new Texture2D(_device, _textureDescriptionFinalFrac);
                        //}

                        var dataBox = _device.ImmediateContext.MapSubresource(arrayOfTexture2DFrac[mainArrayIndex], 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                        int memoryBitmapStride0 = wid * 4;// _textureDescription.Width * 4;

                        int columns0 = wid;// _textureDescription.Width;
                        int rows0 = hgt;// _textureDescription.Height;
                        IntPtr interptr = dataBox.DataPointer;

                        // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (_width * 4)
                        if (dataBox.RowPitch == memoryBitmapStride0)
                        {
                            // Stride is the same
                            Marshal.Copy(interptr, arrayOfBytes[mainArrayIndex], 0, wid * hgt * 4);
                        }
                        else
                        {
                            // Stride not the same - copy line by line
                            for (int y = 0; y < _height; y++)
                            {
                                Marshal.Copy(interptr + y * dataBox.RowPitch, arrayOfBytes[mainArrayIndex], y * memoryBitmapStride0, memoryBitmapStride0);
                            }
                        }

                        _device.ImmediateContext.UnmapSubresource(arrayOfTexture2DFrac[mainArrayIndex], 0);

                        DeleteObject(interptr);

                        source_rect.X += wid;
                    }
                    source_rect.Y += hgt;
                }*/
                //TO READD FOR DIVIDING THE TEXTURE2D IN MULTIPLE TEXTURE2D OR/AND MULTIPLE BYTE ARRAYS. LAGGY
                //TO READD FOR DIVIDING THE TEXTURE2D IN MULTIPLE TEXTURE2D OR/AND MULTIPLE BYTE ARRAYS. LAGGY
                //TO READD FOR DIVIDING THE TEXTURE2D IN MULTIPLE TEXTURE2D OR/AND MULTIPLE BYTE ARRAYS. LAGGY













                /*var dataBox0 = _device.ImmediateContext.MapSubresource(_texture2DFinal, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                //int memoryBitmapStride = _textureDescription.Width * 4;

                //int columns = _textureDescription.Width;
                //int rows = _textureDescription.Height;
                IntPtr interptr0 = dataBox0.DataPointer;

                // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (_width * 4)
                /*if (dataBox0.RowPitch == memoryBitmapStride)
                {
                    // Stride is the same
                    Marshal.Copy(interptr, _textureByteArray, 0, _bytesTotal);
                }
                else
                {
                    // Stride not the same - copy line by line
                    for (int y = 0; y < _height; y++)
                    {
                        Marshal.Copy(interptr + y * dataBox.RowPitch, _textureByteArray, y * memoryBitmapStride, memoryBitmapStride);
                    }
                }
                //var somebitmap = new System.Drawing.Bitmap(160, 128, 160 * 4, PixelFormat.Format32bppArgb, interptr0);
                DeleteObject(interptr0);
                _device.ImmediateContext.UnmapSubresource(_texture2DFinal, 0);*/










                shaderRes = new ShaderResourceView(_device, _texture2DFinal, resourceViewDescription);
                _device.ImmediateContext.GenerateMips(shaderRes);

                if (shaderRes != null)
                {
                    _frameCaptureData._ShaderResource = shaderRes;
                    _frameCaptureData._texture2DFinal = _texture2D;


                    //TO READD
                    //TO READD
                    //TO READD
                    //_frameCaptureData.arrayOfFRACSCREENSPECTRUMBytes = arrayOfBytes;
                    //_frameCaptureData.somebitmapforarduino = somebitmap;
                    //TO READD
                    //TO READD
                    //TO READD


                    //_frameCaptureData.frameByteArray = _textureByteArray;


                }

                if (_lastShaderResourceView != null)
                {
                    _lastShaderResourceView.Dispose();
                }

                _lastShaderResourceView = shaderRes;

                if (_lastBitmap != null)
                {
                    _lastBitmap.Dispose();
                }

                //TO READD
                //TO READD
                //_lastBitmap = somebitmap;
                //TO READD
                //TO READD
















                //arrayOfTexture2DFrac
                //Copy(_device, _texture2DFinal, arrayOfTexture2DFrac);
                //device.ImmediateContext.CopySubresourceRegion(source, 0, region, target, 0);

                //_SystemTickPerformance.Stop();
                //_SystemTickPerformance.Reset();
                //_SystemTickPerformance.Start();

                //image = new System.Drawing.Bitmap(columns, rows, memoryBitmapStride, PixelFormat.Format32bppArgb, interptr);

                //source_rect = new System.Drawing.Rectangle(0, 0, wid, hgt);








                /*if (_ShaderResourceViewArray != null)
                {
                    _frameCaptureData._ShaderResourceArray = _ShaderResourceViewArray;
                }

                if (_lastShaderResourceViewArray != null)
                {
                    for (int i = 0; i < _lastShaderResourceViewArray.Length; i++)
                    {
                        if (_lastShaderResourceViewArray[i] != null)
                        {
                            _lastShaderResourceViewArray[i].Dispose();
                        }
                    }
                }

                _lastShaderResourceViewArray = _ShaderResourceViewArray;*/















                //source_rect.X = 0;// = new System.Drawing.Rectangle(0, 0, _textureDescriptionFinal.Width, _textureDescriptionFinal.Height);
                //source_rect.Y = 0;//
                //region.Left = source_rect.X;
                //region.Top = source_rect.Y;
                //source_rect = new System.Drawing.Rectangle(0, 0, _textureDescriptionFinal.Width, _textureDescriptionFinal.Height);
                //region = new ResourceRegion(0, 0, 0, wid, hgt, 1);
                //region = new ResourceRegion(source_rect.X, source_rect.Y, 0, _textureDescriptionFinal.Width, _textureDescriptionFinal.Height, 1);






                /*
                
                source_rect.X = 0;// = new System.Drawing.Rectangle(0, 0, _textureDescriptionFinal.Width, _textureDescriptionFinal.Height);
                source_rect.Y = 0;//
                region.Left = source_rect.X;
                region.Top = source_rect.Y;
                int _nWidth = wid;
                int _nHeight = hgt;

                for (int row = 0; row < num_rows; row++)
                {
                    source_rect.X = 0;

                    for (int col = 0; col < num_cols; col++)
                    {
                        var mainArrayIndex = (row * num_cols) + col;

                        region.Left = source_rect.X;

                        region.Top = source_rect.Y;

                        for (int y = 0; y < _nHeight; y++)
                        {
                            for (int x = 0; x < _nWidth; x++)
                            {
                                var bytePoser = (((y) * _nWidth) + (x)) * 4;

                                SharpDX.Color color = new SharpDX.Color(arrayOfBytes[mainArrayIndex][bytePoser + 0], arrayOfBytes[mainArrayIndex][bytePoser + 1], arrayOfBytes[mainArrayIndex][bytePoser + 2], arrayOfBytes[mainArrayIndex][bytePoser + 3]);

                                //System.Drawing.Color color = System.Drawing.Color.Black;

                                //if (color.B <= 10 && color.G <= 10 && color.R <= 10 && color.A == 255 || color.B <= 10 && color.G <= 10 && color.R <= 10 && color.A >= 235
                                //    || color.B <= 10 && color.G <= 10 && color.R <= 10 && color.A >= 10)
                                if (color.B == 255 && color.G == 255 && color.R == 255 && color.A == 255)
                                {
                                    //float test = 0.5f;
                                    //MainWindow.MessageBox((IntPtr)0, "b: " + arrayOfBytes[mainArrayIndex][bytePoser + 0] + " g: " + arrayOfBytes[mainArrayIndex][bytePoser + 1] + " r: " + arrayOfBytes[mainArrayIndex]
                                    //[bytePoser + 2] + " a: " + arrayOfBytes[mainArrayIndex][bytePoser + 3], "sc core systems", 0);
                                    arrayOfBytes[mainArrayIndex][bytePoser + 0] = 0;
                                    arrayOfBytes[mainArrayIndex][bytePoser + 1] = 0;
                                    arrayOfBytes[mainArrayIndex][bytePoser + 2] = 0;
                                    arrayOfBytes[mainArrayIndex][bytePoser + 3] = 0;
                                    //a = 0;
                                }
                                source_rect

                                /*if (color.B == 66 && color.G == 66 && color.R == 66 && color.A == 255)
                                {
                                    //MainWindow.MessageBox((IntPtr)0, "b: " + arrayOfBytes[mainArrayIndex][bytePoser + 0] + " g: " + arrayOfBytes[mainArrayIndex][bytePoser + 1] + " r: " + arrayOfBytes[mainArrayIndex]
                                    arrayOfBytes[mainArrayIndex][bytePoser + 0] = 255;
                                    arrayOfBytes[mainArrayIndex][bytePoser + 1] = 255;
                                    arrayOfBytes[mainArrayIndex][bytePoser + 2] = 255;
                                    arrayOfBytes[mainArrayIndex][bytePoser + 3] = 0;
                                    //a = 0;
                                }
                            }
                        }
                        
                        source_rect.X += wid;
                    }
                    source_rect.Y += hgt;
                }
                



                source_rect = new System.Drawing.Rectangle(0, 0, _textureDescriptionFinal.Width, _textureDescriptionFinal.Height);

                region = new ResourceRegion(0, 0, 0, wid, hgt, 1);

                region = new ResourceRegion(source_rect.X, source_rect.Y, 0, _textureDescriptionFinal.Width, _textureDescriptionFinal.Height, 1);



                _nWidth = wid;
                _nHeight = hgt;*/


                /*for (int i = 0; i < _textureByteArray.Length; i+= arrayOfBytes[0].Length)
                {
                    int length = arrayOfBytes[0].Length;
                    if (i + arrayOfBytes[0].Length >= _textureByteArray.Length)
                    {
                        length = arrayOfBytes[0].Length - i;
                    }
                    Array.Copy(arrayOfBytes[0], 0, _textureByteArray, i, length);
                }*/




                /*
                var dataBox0 = _device.ImmediateContext.MapSubresource(_texture2D, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                int memoryBitmapStride1 = _textureDescription.Width * 4;

                int columns1 = _textureDescription.Width;
                int rows1 = _textureDescription.Height;
                interptr0 = dataBox0.DataPointer;

                // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (_width * 4)
                if (dataBox0.RowPitch == memoryBitmapStride1)
                {
                    // Stride is the same
                    Marshal.Copy(interptr0, _textureByteArray, 0, _bytesTotal);
                }
                else
                {
                    // Stride not the same - copy line by line
                    for (int y = 0; y < _height; y++)
                    {
                        Marshal.Copy(interptr0 + y * dataBox0.RowPitch, _textureByteArray, y * memoryBitmapStride1, memoryBitmapStride1);
                    }
                }

                _device.ImmediateContext.UnmapSubresource(_texture2D, 0);


                DeleteObject(interptr0);*/




                //var bytePoser = (((y) * _nWidth) + (x)) * 4;


                /*for (int y = 0; y < _height; y++)
                {
                    for (int x = 0; x < _width; x++)
                    {
                        //arrayOfBytes[mainArrayIndex][bytePoser + 3] = 0;
                        var bytePoser = ((((y) * _width) + (x)) * 4);

                        SharpDX.Color color = new SharpDX.Color(_textureByteArray[bytePoser + 0], _textureByteArray[bytePoser + 1], _textureByteArray[bytePoser + 2], _textureByteArray[bytePoser + 3]);

                        if (color.B == 255 && color.G == 255 && color.R == 255 && color.A == 255)
                        {
                            //float test = 0.5f;
                            //MainWindow.MessageBox((IntPtr)0, "b: " + arrayOfBytes[mainArrayIndex][bytePoser + 0] + " g: " + arrayOfBytes[mainArrayIndex][bytePoser + 1] + " r: " + arrayOfBytes[mainArrayIndex]
                            //[bytePoser + 2] + " a: " + arrayOfBytes[mainArrayIndex][bytePoser + 3], "sc core systems", 0);
                            _textureByteArray[bytePoser + 0] = 0;
                            _textureByteArray[bytePoser + 1] = 0;
                            _textureByteArray[bytePoser + 2] = 0;
                            _textureByteArray[bytePoser + 3] = 0;
                            //a = 0;
                        }
                    }
                }*/





                /*
                for (int row = 0; row < num_rows; row++)
                {
                    //source_rect.X = 0;

                    for (int col = 0; col < num_cols; col++)
                    {
                        var mainArrayIndex = (row * num_cols) + col;

                        var someTest = (((source_rect.Y) * 1920) + (source_rect.X)) * 4;

                        //region.Left = source_rect.X;
                        //region.Top = source_rect.Y;

                        //var bytePoser = ((((0) * _nWidth) + (0)) * 4);

                        //for (int y = 0; y < _nHeight; y++)
                        //{
                        //    for (int x = 0; x < _nWidth; x++)
                        //    {
                        //        //arrayOfBytes[mainArrayIndex][bytePoser + 3] = 0;
                                var bytePoser = ((((y) * _nWidth) + (x)) * 4);
                        //
                        //        //_textureByteArray[((mainArrayIndex) * (bytePoser + 0))] = arrayOfBytes[mainArrayIndex][bytePoser + 0];
                        //        //_textureByteArray[((mainArrayIndex) * (bytePoser + 1))] = arrayOfBytes[mainArrayIndex][bytePoser + 1];
                        //        //_textureByteArray[((mainArrayIndex) * (bytePoser + 2))] = arrayOfBytes[mainArrayIndex][bytePoser + 2];
                        //        //_textureByteArray[((mainArrayIndex) * (bytePoser + 3))] = arrayOfBytes[mainArrayIndex][bytePoser + 3];                    
                        //    }
                        //}




                        /*var dataBox = _device.ImmediateContext.MapSubresource(arrayOfTexture2DFrac[mainArrayIndex], 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                        int memoryBitmapStride0 = wid * 4;// _textureDescription.Width * 4;

                        int columns0 = wid;// _textureDescription.Width;
                        int rows0 = hgt;// _textureDescription.Height;
                        IntPtr interptr = dataBox.DataPointer;

                        // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (_width * 4)
                        if (dataBox.RowPitch == memoryBitmapStride0)
                        {
                            // Stride is the same
                            //Marshal.Copy(interptr, arrayOfBytes[mainArrayIndex], 0, wid * hgt * 4);
                            Array.Copy(arrayOfBytes[mainArrayIndex], 0, _textureByteArray, someTest, wid * hgt * 4);
                        }
                        else
                        {
                            // Stride not the same - copy line by line
                            for (int y = 0; y < _height; y++)
                            {
                                //Marshal.Copy(interptr + y * dataBox.RowPitch, arrayOfBytes[mainArrayIndex], y * memoryBitmapStride0, memoryBitmapStride0);
                            }
                        }

                        _device.ImmediateContext.UnmapSubresource(arrayOfTexture2DFrac[mainArrayIndex], 0);


                        DeleteObject(interptr);*/

                //System.Buffer.BlockCopy(_bufferPlayerShipRect, 0, _currentByteArray, bytePoser, _bufferPlayerShipRect.Length);
                //System.Buffer.BlockCopy(arrayOfBytes[mainArrayIndex], 0, _textureByteArray, someTest, arrayOfBytes[mainArrayIndex].Length);

                //Array.Copy(arrayOfBytes[mainArrayIndex], 0, _textureByteArray, someTest + 0, wid * hgt * 4);

                /*for (int y = 0; y < _height; y++)
                {
                    Marshal.Copy(interptr + y * (), arrayOfBytes[mainArrayIndex], y * memoryBitmapStride0, memoryBitmapStride0);
                }*/

                /*for (int x = 0; x < _nWidth; x++)
                {
                    //arrayOfBytes[mainArrayIndex][bytePoser + 3] = 0;
                    var bytePoser = ((_nWidth) + x) * 4;
                    Array.Copy(arrayOfBytes[mainArrayIndex], 0, _textureByteArray, someTest + bytePoser, wid * 4);
                }


                //Marshal.Copy(Marshal.UnsafeAddrOfPinnedArrayElement(arrayOfBytes[mainArrayIndex], 0), _textureByteArray, 0, wid * hgt * 4);

                source_rect.X += wid;
            }
            source_rect.Y += hgt;
        }*/





                /*
                for (int row = 0; row < num_rows; row++)
                {
                    source_rect.X = 0;

                    for (int col = 0; col < num_cols; col++)
                    {
                        var mainArrayIndex = (row * num_cols) + col;

                        region.Left = source_rect.X;

                        region.Top = source_rect.Y;

                        var someTest = ((source_rect.Y*4) * num_cols) + (source_rect.X*4);

                        /*for (int y = 0; y < _nHeight; y++)
                        {
                            for (int x = 0; x < _nWidth; x++)
                            {
                                //arrayOfBytes[mainArrayIndex][bytePoser + 3] = 0;
                                var bytePoser = (((y) * _nWidth) + (x)) * 4;

                                //_textureByteArray[((mainArrayIndex) * bytePoser) + 0] = arrayOfBytes[mainArrayIndex][bytePoser + 0];
                                //_textureByteArray[((mainArrayIndex) * bytePoser) + 1] = arrayOfBytes[mainArrayIndex][bytePoser + 1];
                                //_textureByteArray[((mainArrayIndex) * bytePoser) + 2] = arrayOfBytes[mainArrayIndex][bytePoser + 2];
                                //_textureByteArray[((mainArrayIndex) * bytePoser) + 3] = arrayOfBytes[mainArrayIndex][bytePoser + 3];
                            }
                        }


                        for (int i = 0; i < _textureByteArray.Length; i++)
                        {
                            _textureByteArray[i] = arrayOfBytes[mainArrayIndex][i % arrayOfBytes.Length];
                        }



                        //Array.Copy(arrayOfBytes[mainArrayIndex], 0, _textureByteArray, someTest, wid * hgt * 4);

                        //Marshal.Copy(Marshal.UnsafeAddrOfPinnedArrayElement(arrayOfBytes[mainArrayIndex], 0), _textureByteArray, 0, wid * hgt * 4);

                        source_rect.X += wid;
                    }
                    source_rect.Y += hgt;
                }*/



                /*
                var ycount = 0;
                var xcount = 0;
                var xreset = 0;
                var yreset = 0;

                for (int y = 0; y < _height; y++)
                {
                    if (yreset == hgt)
                    {
                        ycount++;
                        yreset = 0;
                    }

                    for (int x = 0; x < _width; x++)
                    {
                        if (xreset == wid)
                        {
                            xcount++;
                            xreset = 0;
                        }

                        var mainArrayIndex = ((y * _width) + x) * 4;

                        var secArrayIndex = (ycount * num_cols) + xcount;

                        if (secArrayIndex < num_cols * num_rows)
                        {
                            _textureByteArray[mainArrayIndex] = arrayOfBytes[secArrayIndex][mainArrayIndex % arrayOfBytes.Length];
                        }

                        /*for (int i = 0; i < _textureByteArray.Length; i++)
                        {
                            _textureByteArray[i] = arrayOfBytes[mainArrayIndex][i % arrayOfBytes.Length];
                        }

                        xreset++;
                    }
                    yreset++;
                }*/




















                /*var dataBox0 = _device.ImmediateContext.MapSubresource(_texture2D, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                int memoryBitmapStride1 = _textureDescription.Width * 4;

                int columns1 = _textureDescription.Width;
                int rows1 = _textureDescription.Height;
                IntPtr interptr0 = dataBox0.DataPointer;

                // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (_width * 4)
                if (dataBox0.RowPitch == memoryBitmapStride1)
                {
                    // Stride is the same
                    Marshal.Copy(interptr0, _textureByteArray, 0, _bytesTotal);
                }
                else
                {
                    // Stride not the same - copy line by line
                    for (int y = 0; y < _height; y++)
                    {
                        Marshal.Copy(interptr0 + y * dataBox0.RowPitch, _textureByteArray, y * memoryBitmapStride1, memoryBitmapStride1);
                    }
                }

                _device.ImmediateContext.UnmapSubresource(_texture2D, 0);


                DeleteObject(interptr0);*/




                //int memoryBitmapStride = _textureDescription.Width * 4;

                //int columns = _textureDescription.Width;
                //int rows = _textureDescription.Height;
                //IntPtr interptr = dataBox.DataPointer;

                //shaderRes = Ab3d.DirectX.TextureLoader.CreateShaderResourceView(_device, _textureByteArray, columns, rows, memoryBitmapStride, Format.B8G8R8A8_UNorm, true);
















                /*
                 image = new System.Drawing.Bitmap(columns, rows, memoryBitmapStride, PixelFormat.Format32bppArgb, interptr);

                 using (Graphics gr = Graphics.FromImage(piece))
                 {               
                     for (int row = 0; row < num_rows; row++)
                     {
                         source_rect.X = 0;
                         for (int col = 0; col < num_cols; col++)
                         {
                             //gr.DrawImage(image, dest_rect, source_rect, GraphicsUnit.Pixel);

                             //piece.Save(@"C:\Users\steve\OneDrive\Desktop\screenRecord\" + row.ToString("00") + col.ToString("00") + ".png");
                             source_rect.X += wid;
                         }
                         source_rect.Y += hgt;
                     }
                 }
                 MessageBox((IntPtr)0, _SystemTickPerformance.Elapsed.Milliseconds + "", "Oculus Error", 0);*/

                //_SystemTickPerformance.Stop();
                //_SystemTickPerformance.Reset();
                //_SystemTickPerformance.Start();

                /*for (int i = 0; i < arrayOfBytes.Length; i++)
                {
                    if (arrayOfBytes[i] != null)
                    {
                        //var dataBoxer = _device.ImmediateContext.MapSubresource(arrayOfTexture2DFrac[i], 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);
                        //memoryBitmapStride = _textureDescription.Width * 4;
                        //columns = _textureDescription.Width;
                        //rows = _textureDescription.Height;
                        //var interptr2 = dataBoxer.DataPointer;

                        //_device.ImmediateContext.UnmapSubresource(arrayOfTexture2DFrac[i], 0);

                        System.Drawing.Bitmap image = new System.Drawing.Bitmap(wid, hgt, strider, PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(arrayOfBytes[i], 0)); //arrayOfBytes[i]);// Marshal.UnsafeAddrOfPinnedArrayElement(arrayOfTexture2DFrac[i], 0));

                        string filePathVE = "desktop capture";
                        var exportedToFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                        filePathVE = exportedToFolderPath + "\\" + filePathVE;// @"LAYERS\PNG\";

                        if (!Directory.Exists(filePathVE))
                        {
                            Directory.CreateDirectory(filePathVE);
                        }

                        var fi = new FileInfo(filePathVE);
                        fi.Refresh();

                        image.Save(filePathVE + "\\" + imageCounter + ".jpg");
                        imageCounter++;
                    }
                }*/
                //MessageBox((IntPtr)0, _SystemTickPerformance.Elapsed.Milliseconds + "", "Oculus Error", 0);

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
            }
            catch (Exception ex)
            {
                MainWindow.MessageBox((IntPtr)0, index + " " + ex.ToString(), "sccs error message", 0);
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
                SC_Update._desktopDupe = new SC_SharpDX_ScreenCapture(0, 0, this._device); // not that good but let's leave it at that.
                return false;
            }
        }

        public static void Copy(Device device, Texture2D source, Texture2D target)
        {
            if (device == null)
                throw new ArgumentNullException("device");
            if (source == null)
                throw new ArgumentNullException("source");
            if (target == null)
                throw new ArgumentNullException("target");

            int sourceWidth = source.Description.Width;
            int sourceHeight = source.Description.Height;
            int targetWidth = target.Description.Width;
            int targetHeight = target.Description.Height;

            if (sourceWidth == targetWidth && sourceHeight == targetHeight)
            {
                device.ImmediateContext.CopyResource(source, target);
            }
            else
            {
                int width = Math.Min(sourceWidth, targetWidth);
                int height = Math.Min(sourceHeight, targetHeight);
                var region = new ResourceRegion(0, 0, 0, width, height, 1);
                device.ImmediateContext.CopySubresourceRegion(source, 0, region, target, 0);
            }
        }
    }
}


/*
byte* ptr = (byte*)interptr.ToPointer();

int _pixelSize = 3;
int _nWidth = _textureDescriptionFinal.Width * _pixelSize;
int _nHeight = _textureDescriptionFinal.Height;

                for (int y = 0; y<_nHeight; y++)
                {
                    for (int x = 0; x<_nWidth; x++)
                    {
                        if (x % _pixelSize == 0 || x == 0)
                        {
                            var bytePoser = (((y) * _nWidth) + (x));

var test0 = ptr[bytePoser + 0];
var test1 = ptr[bytePoser + 1];
var test2 = ptr[bytePoser + 2];

ptr++;
                        }
                    }
                }*/










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












//sure lets use this and lag the whole program // 1000% WORKING // 1000% FAILING PERFORMANCE
/*
_SystemTickPerformance.Stop();
_SystemTickPerformance.Reset();
_SystemTickPerformance.Start();

int num_rows = hgt;
int num_cols = wid;
System.Drawing.Rectangle source_rect = new System.Drawing.Rectangle(0, 0, wid, hgt);
for (int row = 0; row < 10; row++)
{
    source_rect.X = 0;
    for (int col = 0; col < 10; col++)
    {
        // Copy the piece of the image.
        gr.DrawImage(image, dest_rect, source_rect, GraphicsUnit.Pixel);

        string filePathVE = "desktop capture";
        var exportedToFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        filePathVE = exportedToFolderPath + "\\" + filePathVE;// @"LAYERS\PNG\";

        if (!Directory.Exists(filePathVE))
        {
            Directory.CreateDirectory(filePathVE);
        }

        var fi = new FileInfo(filePathVE);
        fi.Refresh();

        piece.Save(filePathVE + "\\" + imageCounter + ".jpg");
        imageCounter++;


        source_rect.X += wid;
    }
    source_rect.Y += hgt;
}



//Console.WriteLine(_SystemTickPerformance.Elapsed.Milliseconds);
MainWindow.MessageBox((IntPtr)0, _SystemTickPerformance.Elapsed.Milliseconds + "", "sccs error message", 0);*/














/* // not working yet - to test later for performance after it works if it ever will.
for (i_ptr = &ii; *i_ptr < 3; (*i_ptr)++) //, (*arrayOfImageIterator)++
{
    iii = 0;

    if (*xx >= *_w)
    {
        *yy += hgt; //_hh
        *xx = *_z;
    }

    for (_loopH = &iii; *_loopH < hgt - 1; (*_loopH)++)
    {
        if (*arrayOfImageIterator < hgt)
        {

            //Console.WriteLine(*_loopH + "");
            x_ = *xx;
            y_ = *yy;
            _lh = *_loopH;

            totalBytesOffsetDest = _bytesToTransferWidth * (_lh);
            totalBytesOffsetSrc = ((x_) + (((y_) + (_lh)) * _textureDescriptionFinal.Width)) * 4;

            Marshal.Copy(interptr + totalBytesOffsetSrc, (int[])arrayOfImage[(*arrayOfImageIterator)], totalBytesOffsetDest, _bytesToTransferWidth);



            //MainWindow.MessageBox((IntPtr)0, *arrayOfImageIterator + "", "sccs error message", 0);
            //Console.WriteLine(*arrayOfImageIterator);
            arrayOfImageIteratorTwo
        }
        (*arrayOfImageIterator)++;
    }
    if (*xx < *_w)
    {
        *xx += wid;
    }
}*/
/*if (*i_ptr < 10)
      {
          //Console.WriteLine(*_loopH + "");
          x_ = *xx;
          y_ = *yy;
          _lh = *_loopH;

          totalBytesOffsetDest = _bytesToTransferWidth * (_lh);

          //(Y coordinate * width) + X coordinate
          totalBytesOffsetSrc = ((y_) * _bytesToTransferWidth) + x_;

          Marshal.Copy(interptr + totalBytesOffsetSrc, (int[])arrayOfImage[(*i_ptr)++], totalBytesOffsetDest, _bytesToTransferWidth);

          //totalBytesOffsetSrc = ((x_) + (((y_) + (_lh)) * _textureDescriptionFinal.Width)) * 4;

          //var indexer01 = x + MainWindow.world_width * (y + MainWindow.world_height * z);

          //MainWindow.MessageBox((IntPtr)0, *arrayOfImageIterator + "", "sccs error message", 0);
          //Console.WriteLine(*i_ptr);
          //arrayOfImageIteratorTwo
      }*/
