using SharpDX.Direct3D11;
using SharpDX.WIC;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System;

using SharpDX;
using SharpDX.D3DCompiler;
using System.Runtime.InteropServices;
//using System.Windows.Forms;
//using SharpHelper;
using SharpDX.DXGI;
using SharpDX.Direct3D;
//using System.Windows.Media;

using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Threading;
//using System.Windows.Media.Imaging;

using Matrix = SharpDX.Matrix;

using System.Windows;







//using System.Windows.Controls;
//using System.Windows.Interop;
using Device = SharpDX.Direct3D11.Device;
using Resource = SharpDX.Direct3D11.Resource;
using SharpDX.DirectInput;
using System.Reflection;
using System.ComponentModel;
using System.Runtime;
using System.Runtime.CompilerServices;

















namespace _sc_core_systems
{
    public class SC_VR_Chunk_Shader // : System.Windows.Forms.Application
    {


        public const int mapWidth = 4;
        public const int mapHeight = 4;
        public const int mapDepth = 4;

        public const int tinyChunkWidth = 4;
        public const int tinyChunkHeight = 4;
        public const int tinyChunkDepth = 4;

        public const int mapObjectInstanceWidth = 4;
        public const int mapObjectInstanceHeight = 4;
        public const int mapObjectInstanceDepth = 4;


        static SC_VR_Chunk_Shader chunkShader;

        public SharpDX.Direct3D11.Buffer constantBuffer;
        public SharpDX.Direct3D11.Device device;
        public static SharpDX.Direct3D11.Buffer instanceBuffer;
        public SharpDX.Direct3D11.Buffer ConstantLightBuffer;
        public SharpDX.Direct3D11.Buffer _someBuffer;

        float _planeSize = 0.1f;

        InputLayout Layout;
        VertexShader VertexShader;
        PixelShader PixelShader;
        SC_VR_Chunk.DVertex[] OriginalVertexArray;
        int[] OriginalIndicesArray;


        SharpDX.Direct3D11.Buffer VertexBuffer;
        SharpDX.Direct3D11.Buffer IndexBuffer;
        SharpDX.Direct3D11.Buffer MapBuffer;
        SC_VR_Chunk.DIndexType[] indexArray;

        public SC_VR_Chunk_Shader(SharpDX.Direct3D11.Device _device, SharpDX.Direct3D11.Buffer _constantBuffer, InputLayout _Layout, VertexShader _VertexShader, PixelShader _PixelShader, SharpDX.Direct3D11.Buffer _instanceBuffer, SharpDX.Direct3D11.Buffer _ConstantLightBuffer, SC_VR_Chunk.DVertex[] vertexArray, SC_VR_Chunk.DIndexType[] _indexArray, SharpDX.Direct3D11.Buffer vertexBuffer, SharpDX.Direct3D11.Buffer indexBuffer, SharpDX.Direct3D11.Buffer _mapBuffer)
        {
            chunkShader = this;
            this.constantBuffer = _constantBuffer;
            this.device = _device;
            instanceBuffer = _instanceBuffer;
            this.PixelShader = _PixelShader;
            this.VertexShader = _VertexShader;
            this.Layout = _Layout;
            this.ConstantLightBuffer = _ConstantLightBuffer;


            this.indexArray = _indexArray;

            //this.OriginalIndicesArray = triangleArray;


            this.OriginalVertexArray = vertexArray;
            this.VertexBuffer = vertexBuffer;
            this.IndexBuffer = indexBuffer;
            this.MapBuffer = _mapBuffer;
            shaderResourceView2D = LoadTextureFromFile(device, "../../terrainGrassDirt.bmp");

        }

        public int someCounter = 0;
        public int startOnce = 1;

        DataStream mappedResource;
        DataStream streamerTWO;
        DataStream mappedResourceLight;
        DataStream floatStreamY0;
        DataStream floatStreamY1;

        Vector4[][] arrayOfSomeColors = new Vector4[mapWidth * mapHeight * mapDepth][];
        Stopwatch timeCalculator = new Stopwatch();

        ShaderResourceView shaderResourceView2D;

        IEnumerable<object> Flatten(IList s)
        {
            return s == null ? null : s.Cast<object>().SelectMany(x => Flatten(x as IList) ?? new[] { x });
        }

        public _sc_console_graphics.chunkData Renderer(_sc_console_graphics.chunkData chunkdat) //async
        {
            //int[] byteMap = chunkdat.arrayOfSomeMap.SelectMany(a => a).ToArray();


  
            timeCalculator.Stop();
            timeCalculator.Reset();
            timeCalculator.Start();


            device.ImmediateContext.MapSubresource(constantBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streamerTWO);
            streamerTWO.WriteRange(chunkdat.matrixBuffer, 0, chunkdat.matrixBuffer.Length);
            device.ImmediateContext.UnmapSubresource(constantBuffer, 0);
            streamerTWO.Dispose();

            device.ImmediateContext.MapSubresource(ConstantLightBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
            mappedResourceLight.WriteRange(chunkdat.lightBuffer, 0, chunkdat.lightBuffer.Length);
            device.ImmediateContext.UnmapSubresource(ConstantLightBuffer, 0);
            mappedResourceLight.Dispose();

            device.ImmediateContext.MapSubresource(instanceBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(chunkdat.arrayOfInstance, 0, chunkdat.arrayOfInstance.Length);
            device.ImmediateContext.UnmapSubresource(instanceBuffer, 0);
            mappedResource.Dispose();


            /*var buffForInstanceIndexDesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Marshal.SizeOf(typeof(SC_VR_Chunk.DInstanceType)) * chunkdat.instancesIndex.Length,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            var instanceIndexBuff = new SharpDX.Direct3D11.Buffer(device, buffForInstanceIndexDesc);


            device.ImmediateContext.MapSubresource(instanceIndexBuff, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(chunkdat.instancesIndex, 0, chunkdat.instancesIndex.Length);
            device.ImmediateContext.UnmapSubresource(instanceIndexBuff, 0);
            mappedResource.Dispose();*/

            var Vector4BufferDesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Marshal.SizeOf(typeof(SC_VR_Chunk.DInstanceType)) * mapWidth * mapHeight * mapDepth,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            var Vector4Buffer = new SharpDX.Direct3D11.Buffer(device, Vector4BufferDesc);


            device.ImmediateContext.MapSubresource(Vector4Buffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out floatStreamY0);
            floatStreamY0.WriteRange(chunkdat.arrayOfDeVectorMapTemp, 0, chunkdat.arrayOfDeVectorMapTemp.Length);
            device.ImmediateContext.UnmapSubresource(Vector4Buffer, 0);
            floatStreamY0.Dispose();



            Vector4BufferDesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Marshal.SizeOf(typeof(SC_VR_Chunk.DInstanceType)) * mapWidth * mapHeight * mapDepth,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            var Vector4BufferTwo = new SharpDX.Direct3D11.Buffer(device, Vector4BufferDesc);


            device.ImmediateContext.MapSubresource(Vector4BufferTwo, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out floatStreamY1);
            floatStreamY1.WriteRange(chunkdat.arrayOfDeVectorMapTempTwo, 0, chunkdat.arrayOfDeVectorMapTempTwo.Length);
            device.ImmediateContext.UnmapSubresource(Vector4BufferTwo, 0);
            floatStreamY1.Dispose();




            











            /*var floatBufferY0Desc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Marshal.SizeOf(typeof(SC_VR_Chunk.DFloatArray)) * mapWidth * mapHeight * mapDepth,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            var floatBufferY0 = new SharpDX.Direct3D11.Buffer(device, floatBufferY0Desc);





            device.ImmediateContext.MapSubresource(floatBufferY0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out floatStreamY0);
            floatStreamY0.WriteRange(chunkdat.arrayOfFloatsY0, 0, chunkdat.arrayOfFloatsY0.Length);
            device.ImmediateContext.UnmapSubresource(floatBufferY0, 0);
            floatStreamY0.Dispose();



            var floatBufferY1Desc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Marshal.SizeOf(typeof(SC_VR_Chunk.DFloatArray)) * mapWidth * mapHeight * mapDepth,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            var floatBufferY1 = new SharpDX.Direct3D11.Buffer(device, floatBufferY1Desc);




            device.ImmediateContext.MapSubresource(floatBufferY1, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out floatStreamY1);
            floatStreamY1.WriteRange(chunkdat.arrayOfFloatsY1, 0, chunkdat.arrayOfFloatsY1.Length);
            device.ImmediateContext.UnmapSubresource(floatBufferY1, 0);
            floatStreamY1.Dispose();



            var floatBufferY2Desc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Marshal.SizeOf(typeof(SC_VR_Chunk.DFloatArray)) * mapWidth * mapHeight * mapDepth,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            var floatBufferY2 = new SharpDX.Direct3D11.Buffer(device, floatBufferY2Desc);



            device.ImmediateContext.MapSubresource(floatBufferY2, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out floatStreamY2);
            floatStreamY2.WriteRange(chunkdat.arrayOfFloatsY2, 0, chunkdat.arrayOfFloatsY2.Length);
            device.ImmediateContext.UnmapSubresource(floatBufferY2, 0);
            floatStreamY2.Dispose();



            var floatBufferY3Desc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Marshal.SizeOf(typeof(SC_VR_Chunk.DFloatArray)) * mapWidth * mapHeight * mapDepth,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            var floatBufferY3 = new SharpDX.Direct3D11.Buffer(device, floatBufferY3Desc);


            device.ImmediateContext.MapSubresource(floatBufferY3, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out floatStreamY3);
            floatStreamY3.WriteRange(chunkdat.arrayOfFloatsY3, 0, chunkdat.arrayOfFloatsY3.Length);
            device.ImmediateContext.UnmapSubresource(floatBufferY3, 0);
            floatStreamY3.Dispose();*/










            //int size = Marshal.SizeOf(typeof(Vector4)); 16
            //Console.WriteLine(size);

            //int size = Marshal.SizeOf(typeof(Vector3)); //12
            //Console.WriteLine(size);

            //int size2 = Marshal.SizeOf(typeof(Vector2)); //8
            //Console.WriteLine(size2);

            //int size = Marshal.SizeOf(typeof(Matrix)); //64
            //Console.WriteLine(size);

            //int size = Marshal.SizeOf(typeof(int)); //4
            //Console.WriteLine(size);

            //int size = Marshal.SizeOf(typeof(byte)); //1
            //Console.WriteLine(size);

            //int size = Marshal.SizeOf(typeof(bool)); //4
            //Console.WriteLine(size);

            //int size = Marshal.SizeOf(typeof(float)); //4
            //Console.WriteLine(size);

            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }



            /*int ii = 0;

            for (ii = 0;ii < 7;ii++)
            {

            }


            Console.WriteLine(ii);*/






            /*float tester = 911100100.0f;

            for (int i = 0;i <= 7;i++)
            {
                tester *= 0.1f;
                Console.WriteLine(tester);
            }*/
         









            device.ImmediateContext.InputAssembler.SetIndexBuffer(IndexBuffer, SharpDX.DXGI.Format.R32_UInt, 0);

            device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
            {
                new VertexBufferBinding(VertexBuffer,Marshal.SizeOf(typeof(SC_VR_Chunk.DVertex)), 0),
            });

            device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
            {
                //*mapWidth*mapHeight*mapDepth
                new VertexBufferBinding(Vector4Buffer, Marshal.SizeOf(typeof(SC_VR_Chunk.DInstanceType)),0),
            });

            device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
            {
                new VertexBufferBinding(Vector4BufferTwo, Marshal.SizeOf(typeof(SC_VR_Chunk.DInstanceType)),0),
            });
            
            device.ImmediateContext.InputAssembler.SetVertexBuffers(3, new[]
            {
               new VertexBufferBinding(instanceBuffer, Marshal.SizeOf(typeof(SC_VR_Chunk.DInstanceType)),0),
            });

            /*device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
            {
               new VertexBufferBinding(Vector4Buffer, Marshal.SizeOf(typeof(SC_VR_Chunk.DInstanceType)),0),
            });*/

            /*device.ImmediateContext.InputAssembler.SetVertexBuffers(3, new[]
            {
                new VertexBufferBinding(floatBufferY0, Marshal.SizeOf(typeof(SC_VR_Chunk.DFloatArray)),0),
                //new VertexBufferBinding(floatBufferY1, Marshal.SizeOf(typeof(SC_VR_Chunk.DFloatArray)),Marshal.SizeOf(typeof(SC_VR_Chunk.DFloatArray))*mapWidth*mapHeight*mapDepth),
                //new VertexBufferBinding(floatBufferY2, Marshal.SizeOf(typeof(SC_VR_Chunk.DFloatArray)),Marshal.SizeOf(typeof(SC_VR_Chunk.DFloatArray))*mapWidth*mapHeight*mapDepth+Marshal.SizeOf(typeof(SC_VR_Chunk.DFloatArray))*mapWidth*mapHeight*mapDepth),
                //new VertexBufferBinding(floatBufferY3, Marshal.SizeOf(typeof(SC_VR_Chunk.DFloatArray)),Marshal.SizeOf(typeof(SC_VR_Chunk.DFloatArray))*mapWidth*mapHeight*mapDepth+Marshal.SizeOf(typeof(SC_VR_Chunk.DFloatArray))*mapWidth*mapHeight*mapDepth+Marshal.SizeOf(typeof(SC_VR_Chunk.DFloatArray))*mapWidth*mapHeight*mapDepth),
            });

            device.ImmediateContext.InputAssembler.SetVertexBuffers(4, new[]
            {
               new VertexBufferBinding(floatBufferY1, Marshal.SizeOf(typeof(SC_VR_Chunk.DFloatArray)),0),
            });
            device.ImmediateContext.InputAssembler.SetVertexBuffers(5, new[]
            {
               new VertexBufferBinding(floatBufferY2, Marshal.SizeOf(typeof(SC_VR_Chunk.DFloatArray)),0),
            });
            device.ImmediateContext.InputAssembler.SetVertexBuffers(6, new[]
            {
               new VertexBufferBinding(floatBufferY3, Marshal.SizeOf(typeof(SC_VR_Chunk.DFloatArray)),0),
            });*/

            /*device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
            {
               new VertexBufferBinding(instanceBuffer, Marshal.SizeOf(typeof(SC_VR_Chunk.DInstanceType)),0),
            });*/



            device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            device.ImmediateContext.InputAssembler.InputLayout = Layout;
            device.ImmediateContext.VertexShader.Set(VertexShader);
            device.ImmediateContext.PixelShader.Set(PixelShader);
            device.ImmediateContext.GeometryShader.Set(null);

            device.ImmediateContext.VertexShader.SetConstantBuffer(0, constantBuffer);
            device.ImmediateContext.PixelShader.SetConstantBuffer(0, ConstantLightBuffer);
            //device.ImmediateContext.VertexShader.SetConstantBuffer(1, MapBuffer);
            //device.ImmediateContext.VertexShader.SetConstantBuffer(2, matrixMapBuff);

            //device.ImmediateContext.VertexShader.SetShaderResource(0, shaderResourceView2D);
            device.ImmediateContext.PixelShader.SetShaderResource(0, shaderResourceView2D);

            //if (indexBuilt == 0)
            {
                device.ImmediateContext.DrawIndexedInstanced(indexArray.Length, mapWidth * mapHeight * mapDepth, 0, 0, 0);
            }
            //else
            {
            //    device.ImmediateContext.DrawIndexedInstanced(36, mapWidth * mapHeight * mapDepth, 0, 0, 0);
            }

            //instanceIndexBuff.Dispose();


            Vector4Buffer.Dispose();
            Vector4BufferTwo.Dispose();


            //chunkdat.switchForSomething = 0;
            //chunkdat.switchForRender = 0;
            return chunkdat;
        }

        public static float Generate(Random prng)
        {
            var sign = prng.Next(2);
            var exponent = prng.Next((1 << 8) - 1); // do not generate 0xFF (infinities and NaN)
            var mantissa = prng.Next(1 << 23);

            var bits = (sign << 31) + (exponent << 23) + mantissa;
            return IntBitsToFloat(bits);
        }

        private static float IntBitsToFloat(int bits)
        {
            unsafe
            {
                return *(float*)&bits;
            }
        }

        public static ShaderResourceView LoadTextureFromFile(SharpDX.Direct3D11.Device device, string filename)
        {
            string ext = System.IO.Path.GetExtension(filename);
            return CreateTextureFromBitmap(device, device.ImmediateContext, filename);

            /*if (ext.ToLower() == ".dds")
            {
                bool isCube;
                return CreateTextureFromDDS(device.Device, device.DeviceContext, System.IO.File.ReadAllBytes(filename), out isCube);
            }
            else
            {
               
            }*/
        }
        private static ShaderResourceView CreateTextureFromBitmap(Device device, DeviceContext context, string filename)
        {
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(filename);

            int width = bitmap.Width;
            int height = bitmap.Height;

            // Describe and create a Texture2D.
            Texture2DDescription textureDesc = new Texture2DDescription()
            {
                MipLevels = 1,
                Format = Format.B8G8R8A8_UNorm,
                Width = width,
                Height = height,
                ArraySize = 1,
                BindFlags = BindFlags.ShaderResource,
                Usage = ResourceUsage.Default,
                SampleDescription = new SampleDescription(1, 0)
            };

            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            DataRectangle dataRectangle = new DataRectangle(data.Scan0, data.Stride);
            var buffer = new Texture2D(device, textureDesc, dataRectangle);
            bitmap.UnlockBits(data);


            var resourceView = new ShaderResourceView(device, buffer);
            buffer.Dispose();

            return resourceView;
        }












        public Texture2D LoadFromFile(Device device, ImagingFactory factory, string fileName)
        {
            using (var bs = LoadBitmap(factory, fileName))
                return CreateTexture2DFromBitmap(device, bs);
        }

        public SharpDX.WIC.BitmapSource LoadBitmap(ImagingFactory factory, string filename)
        {
            var bitmapDecoder = new SharpDX.WIC.BitmapDecoder(
                factory,
                filename,
                SharpDX.WIC.DecodeOptions.CacheOnDemand
                );

            var result = new SharpDX.WIC.FormatConverter(factory);

            result.Initialize(
                bitmapDecoder.GetFrame(0),
                SharpDX.WIC.PixelFormat.Format32bppPRGBA,
                SharpDX.WIC.BitmapDitherType.None,
                null,
                0.0,
                SharpDX.WIC.BitmapPaletteType.Custom);

            return result;
        }
        public Texture2D CreateTexture2DFromBitmap(Device device, SharpDX.WIC.BitmapSource bitmapSource)
        {
            // Allocate DataStream to receive the WIC image pixels
            int stride = bitmapSource.Size.Width * 4;
            using (var buffer = new SharpDX.DataStream(bitmapSource.Size.Height * stride, true, true))
            {
                // Copy the content of the WIC to the buffer
                bitmapSource.CopyPixels(stride, buffer);
                return new SharpDX.Direct3D11.Texture2D(device, new SharpDX.Direct3D11.Texture2DDescription()
                {
                    Width = bitmapSource.Size.Width,
                    Height = bitmapSource.Size.Height,
                    ArraySize = 1,
                    BindFlags = SharpDX.Direct3D11.BindFlags.ShaderResource | BindFlags.RenderTarget,
                    Usage = SharpDX.Direct3D11.ResourceUsage.Default,
                    CpuAccessFlags = SharpDX.Direct3D11.CpuAccessFlags.None,
                    Format = SharpDX.DXGI.Format.R8G8B8A8_UNorm,
                    MipLevels = 1,
                    OptionFlags = ResourceOptionFlags.GenerateMipMaps, // ResourceOptionFlags.GenerateMipMap
                    SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                }, new SharpDX.DataRectangle(buffer.DataPointer, stride));
            }
        }
    }
}



/*//SC_WPF_RENDER.DirectXComponent._device
BufferDescription matrixBufferDescription = new BufferDescription()
{
    Usage = ResourceUsage.Dynamic,
    SizeInBytes = Utilities.SizeOf<DMatrixBuffer>(),
    BindFlags = BindFlags.ConstantBuffer,
    CpuAccessFlags = CpuAccessFlags.Write,
    OptionFlags = ResourceOptionFlags.None,
    StructureByteStride = 0
};

var _constantMatrixBufferTWO = new SharpDX.Direct3D11.Buffer(_dev, matrixBufferDescription);
*/
