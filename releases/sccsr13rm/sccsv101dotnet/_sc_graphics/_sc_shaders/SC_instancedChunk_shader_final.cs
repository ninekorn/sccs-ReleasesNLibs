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


namespace SCCoreSystems
{
    public class SC_instancedChunk_shader_final
    {
        static SC_instancedChunk_shader_final chunkShader;

        public SharpDX.Direct3D11.Device device;
        public SharpDX.Direct3D11.Buffer _someBuffer;

        SharpDX.Direct3D11.Buffer MapBuffer;
 
        int usegeometryshader;

        public SC_instancedChunk_shader_final(SharpDX.Direct3D11.Device _device)
        {


            chunkShader = this;
            this.device = _device;

            var _textureDescriptionFinal = new Texture2DDescription
            {
                CpuAccessFlags = CpuAccessFlags.None,
                BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                Format = Format.B8G8R8A8_UNorm,
                Width = 1,
                Height = 1,
                OptionFlags = ResourceOptionFlags.GenerateMipMaps,
                MipLevels = 1,
                ArraySize = 1,
                SampleDescription = { Count = 1, Quality = 0 },
                Usage = ResourceUsage.Default
            };

            var _texture2DFinal = new Texture2D(_device, _textureDescriptionFinal);

            var resourceViewDescription = new ShaderResourceViewDescription
            {
                Format = _texture2DFinal.Description.Format,
                Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D,
                Texture2D = new ShaderResourceViewDescription.Texture2DResource
                {
                    MipLevels = -1,
                    MostDetailedMip = 0
                }
            };
            ///shaderResourceView2D = LoadTextureFromFile(device, "../../1x1_pink_color.png"); //1x1_pink_color.png //terrainGrassDirt.bmp
            ///
            shaderResourceView2D = new ShaderResourceView(_device, _texture2DFinal, resourceViewDescription);
            



        }

        public int someCounter = 0;
        public int startOnce = 1;

        DataStream mappedResource;
        DataStream streamerTWO;
        DataStream mappedResourceLight;

        Stopwatch timeCalculator = new Stopwatch();

       public ShaderResourceView shaderResourceView2D;


        public SC_instancedChunkPrim.chunkData Renderer(SC_instancedChunkPrim.chunkData chunkdat, int indexBuilt, ShaderResourceView shaderResourceView2D_,int voxeltype_) //async
        {

            timeCalculator.Stop();
            timeCalculator.Reset();
            timeCalculator.Start();

            //int[] byteMap = chunkdat.arrayOfSomeMap.SelectMany(a => a).ToArray();







            device.ImmediateContext.MapSubresource(chunkdat.constantMatrixPosBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streamerTWO);
            streamerTWO.WriteRange(chunkdat.matrixBuffer, 0, chunkdat.matrixBuffer.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.constantMatrixPosBuffer, 0);
            streamerTWO.Dispose();




            

           //TOREADD 
            //TOREADD 

            device.ImmediateContext.MapSubresource(chunkdat.instanceBufferColorsNFaces, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
            mappedResourceLight.WriteRange(chunkdat.colorsNFaces, 0, chunkdat.colorsNFaces.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.instanceBufferColorsNFaces, 0);
            mappedResourceLight.Dispose();
            //TOREADD 
            //TOREADD*/



            device.ImmediateContext.MapSubresource(chunkdat.constantLightBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
            mappedResourceLight.WriteRange(chunkdat.lightBuffer, 0, chunkdat.lightBuffer.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.constantLightBuffer, 0);
            mappedResourceLight.Dispose();

            device.ImmediateContext.MapSubresource(chunkdat.instanceBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(chunkdat.SC_instancedChunk_Instances, 0, chunkdat.SC_instancedChunk_Instances.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.instanceBuffer, 0);
            mappedResource.Dispose();









            //TOREADD 
            
            device.ImmediateContext.MapSubresource(chunkdat.InstanceBufferLocW, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(chunkdat.instancesLocationW, 0, chunkdat.instancesLocationW.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.InstanceBufferLocW, 0);
            mappedResource.Dispose();

            device.ImmediateContext.MapSubresource(chunkdat.InstanceBufferLocH, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(chunkdat.instancesLocationH, 0, chunkdat.instancesLocationH.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.InstanceBufferLocH, 0);
            mappedResource.Dispose();
            //TOREADD
            //TOREADD 


            device.ImmediateContext.MapSubresource(chunkdat.InstanceRotationBufferFORWARD, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(chunkdat.SC_instancedChunk_InstancesFORWARD, 0, chunkdat.SC_instancedChunk_InstancesFORWARD.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.InstanceRotationBufferFORWARD, 0);
            mappedResource.Dispose();

            device.ImmediateContext.MapSubresource(chunkdat.InstanceRotationBufferRIGHT, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(chunkdat.SC_instancedChunk_InstancesRIGHT, 0, chunkdat.SC_instancedChunk_InstancesRIGHT.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.InstanceRotationBufferRIGHT, 0);
            mappedResource.Dispose();

            device.ImmediateContext.MapSubresource(chunkdat.InstanceRotationBufferUP, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(chunkdat.SC_instancedChunk_InstancesUP, 0, chunkdat.SC_instancedChunk_InstancesUP.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.InstanceRotationBufferUP, 0);
            mappedResource.Dispose();


            //TOREADD 
            //TOREADD 
            /*device.ImmediateContext.MapSubresource(chunkdat.instancesbytemapsbuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
            mappedResourceLight.WriteRange(chunkdat.instancesbytemaps, 0, chunkdat.instancesbytemaps.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.instancesbytemapsbuffer, 0);
            mappedResourceLight.Dispose();*/
            //TOREADD
            //TOREADD

            //TOREADD 
            //TOREADD 
            device.ImmediateContext.MapSubresource(chunkdat.instancesmatrixbuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
            mappedResourceLight.WriteRange(chunkdat.instancesmatrix, 0, chunkdat.instancesmatrix.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.instancesmatrixbuffer, 0);
            mappedResourceLight.Dispose();
            //TOREADD
            //TOREADD


            if (chunkdat.switchForRender == 1)
            {


               


                /*
                device.ImmediateContext.MapSubresource(chunkdat.indexBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                //mappedResource.WriteRange(chunkdat, 0, chunkdat.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.indexBuffer, 0);
                mappedResource.Dispose();*/






                //TOREADD 












                /*var matrixBufferDescriptionVertex00 = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(int)) * tinyChunkWidth * tinyChunkHeight * tinyChunkDepth * numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth,
                    BindFlags = BindFlags.ConstantBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };
                var mapBuffer = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionVertex00);
                chunkdat.mapBuffer = mapBuffer;*/
                /*
                device.ImmediateContext.InputAssembler.SetIndexBuffer(IndicesBuffer, SharpDX.DXGI.Format.R32_UInt, 0);
                device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                device.ImmediateContext.InputAssembler.InputLayout = Layout;
                device.ImmediateContext.VertexShader.Set(VertexShader);
                device.ImmediateContext.PixelShader.Set(PixelShader);
                device.ImmediateContext.GeometryShader.Set(GeometryShader);*/





                try
                {

                }
                catch (Exception ex)
                {
                    Program.MessageBox((IntPtr)0, ex.ToString() + "", "Oculus Error", 0);
                }

                chunkdat.switchForRender = 0;
            }




            device.ImmediateContext.VertexShader.SetConstantBuffer(0, chunkdat.constantMatrixPosBuffer);
            //device.ImmediateContext.VertexShader.SetConstantBuffer(1, chunkdat.mapBuffer);


            device.ImmediateContext.PixelShader.SetConstantBuffer(0, chunkdat.constantMatrixPosBuffer);
            device.ImmediateContext.PixelShader.SetConstantBuffer(1, chunkdat.constantLightBuffer);

            device.ImmediateContext.PixelShader.SetShaderResource(0, shaderResourceView2D_);



            //shaderResourceView2D





            /*device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
            {
                new VertexBufferBinding(chunkdat.indexBuffer, Marshal.SizeOf(typeof(SC_instancedChunk.DVertex)),0),
            });*/
            device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
           {
                new VertexBufferBinding(chunkdat.vertexBuffer,Marshal.SizeOf(typeof(SC_instancedChunk.DVertex)), 0),
                //new VertexBufferBinding(chunkdat.indexBuffer, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceType)),0),
            });

            device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
            {
                new VertexBufferBinding(chunkdat.instanceBuffer, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceType)),0),
            });




            
            device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
            {
                new VertexBufferBinding(chunkdat.InstanceRotationBufferFORWARD, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceShipData)),0),
            });
            device.ImmediateContext.InputAssembler.SetVertexBuffers(3, new[]
            {
                new VertexBufferBinding(chunkdat.InstanceRotationBufferRIGHT, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceShipData)),0),
            });
            device.ImmediateContext.InputAssembler.SetVertexBuffers(4, new[]
            {
                new VertexBufferBinding(chunkdat.InstanceRotationBufferUP, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceShipData)),0),
            });
            device.ImmediateContext.InputAssembler.SetVertexBuffers(5, new[]
            {
                new VertexBufferBinding(chunkdat.instanceBufferColorsNFaces, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceColorNFace)),0),
            });

            /*
            device.ImmediateContext.InputAssembler.SetVertexBuffers(6, new[]
            {
                new VertexBufferBinding(chunkdat.instancesbytemapsbuffer, Marshal.SizeOf(typeof(SC_instancedChunk.DInstancesByteMap)),0),
            });
            */
            //TOREADD 
            //TOREADD 
            device.ImmediateContext.InputAssembler.SetVertexBuffers(6, new[]
            {
                new VertexBufferBinding(chunkdat.instancesmatrixbuffer, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceMatrix)),0),
            });
            

            
            device.ImmediateContext.InputAssembler.SetVertexBuffers(7, new[]
            {
                new VertexBufferBinding(chunkdat.InstanceBufferLocW, Marshal.SizeOf(typeof( SCCoreSystems.SC_instancedChunk.DInstanceTypeLocW)),0),
            });

            device.ImmediateContext.InputAssembler.SetVertexBuffers(8, new[]
            {
                new VertexBufferBinding(chunkdat.InstanceBufferLocH, Marshal.SizeOf(typeof( SCCoreSystems.SC_instancedChunk.DInstanceTypeLocH)),0),
            });
                
            //TOREADD 
            //TOREADD 
            











            device.ImmediateContext.InputAssembler.SetIndexBuffer(chunkdat.IndicesBuffer, SharpDX.DXGI.Format.R32_UInt, 0);

            if (voxeltype_ == 0)
            {
                device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            }
            else if (voxeltype_ == 1)
            {
                device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.LineList;
            }




            device.ImmediateContext.InputAssembler.InputLayout = chunkdat.Layout;
            device.ImmediateContext.VertexShader.Set(chunkdat.VertexShader);
            device.ImmediateContext.PixelShader.Set(chunkdat.PixelShader);

            if (usegeometryshader == 0)
            {
                device.ImmediateContext.GeometryShader.Set(null); //GeometryShader
            }
            else if (usegeometryshader == 1)
            {
                device.ImmediateContext.GeometryShader.Set(chunkdat.GeometryShader); //GeometryShader
            }




            device.ImmediateContext.DrawIndexedInstanced(chunkdat.originalArrayOfIndices.Length, chunkdat.numberOfInstancesPerObjectInWidth * chunkdat.numberOfInstancesPerObjectInHeight * chunkdat.numberOfInstancesPerObjectInDepth, 0, 0, 0);

            //Console.WriteLine(timeCalculator.Elapsed.Ticks);

            return chunkdat;
        }

        public static ShaderResourceView LoadTextureFromFile(SharpDX.Direct3D11.Device device, string filename)
        {
            string ext = System.IO.Path.GetExtension(filename);
            return CreateTextureFromBitmap(device, device.ImmediateContext, filename);
        }

        private static ShaderResourceView CreateTextureFromBitmap(Device device, DeviceContext context, string filename)
        {
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(filename);

            int width = bitmap.Width;
            int height = bitmap.Height;

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