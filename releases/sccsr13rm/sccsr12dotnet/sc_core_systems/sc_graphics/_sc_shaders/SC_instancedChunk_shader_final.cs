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

        public SharpDX.Direct3D11.Buffer constantBuffer;
        public SharpDX.Direct3D11.Device device;
        public static SharpDX.Direct3D11.Buffer instanceBuffer;
        public SharpDX.Direct3D11.Buffer ConstantLightBuffer;
        public SharpDX.Direct3D11.Buffer _someBuffer;

        InputLayout Layout;
        VertexShader VertexShader;
        PixelShader PixelShader;
        SC_instancedChunk.DVertex[] OriginalVertexArray;

        SharpDX.Direct3D11.Buffer VertexBuffer;
        SharpDX.Direct3D11.Buffer IndexBuffer;
        SharpDX.Direct3D11.Buffer IndicesBuffer;
        
        SharpDX.Direct3D11.Buffer MapBuffer;
        SC_instancedChunk.DIndexType[] indexArray;

        public SC_instancedChunk_shader_final(SharpDX.Direct3D11.Device _device, SharpDX.Direct3D11.Buffer _constantBuffer, InputLayout _Layout, VertexShader _VertexShader, PixelShader _PixelShader, SharpDX.Direct3D11.Buffer indexBuffer, SharpDX.Direct3D11.Buffer _ConstantLightBuffer, SC_instancedChunk.DVertex[] vertexArray, SC_instancedChunk.DIndexType[] _indexArray, SharpDX.Direct3D11.Buffer vertexBuffer, SharpDX.Direct3D11.Buffer indicesBuffer, SharpDX.Direct3D11.Buffer _instanceBuffer )
        {
            chunkShader = this;
            this.constantBuffer = _constantBuffer;
            this.device = _device;
 
            this.PixelShader = _PixelShader;
            this.VertexShader = _VertexShader;
            this.Layout = _Layout;
            this.ConstantLightBuffer = _ConstantLightBuffer;

            this.indexArray = _indexArray;

            this.OriginalVertexArray = vertexArray;
            this.VertexBuffer = vertexBuffer;
            this.IndexBuffer = indexBuffer;
            this.IndicesBuffer = indicesBuffer;
            instanceBuffer = _instanceBuffer;

            shaderResourceView2D = LoadTextureFromFile(device, "../../terrainGrassDirt.bmp");
        }

        public int someCounter = 0;
        public int startOnce = 1;

        DataStream mappedResource;
        DataStream streamerTWO;
        DataStream mappedResourceLight;

        Vector4[][] arrayOfSomeColors = new Vector4[SC_Globals.numberOfInstancesPerObjectInWidth * SC_Globals.numberOfInstancesPerObjectInHeight * SC_Globals.numberOfInstancesPerObjectInDepth][];
        Stopwatch timeCalculator = new Stopwatch();

       public ShaderResourceView shaderResourceView2D;

        public SCCoreSystems.sc_console.sc_graphics_sec.chunkData Renderer(SCCoreSystems.sc_console.sc_graphics_sec.chunkData chunkdat, int indexBuilt) //async
        {
            timeCalculator.Stop();
            timeCalculator.Reset();
            timeCalculator.Start();

            //int[] byteMap = chunkdat.arrayOfSomeMap.SelectMany(a => a).ToArray();

            /*
            //THE POSITION OF THE INSTANCES
            device.ImmediateContext.MapSubresource(chunkdat.instanceBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(chunkdat.SC_instancedChunk_Instances, 0, chunkdat.SC_instancedChunk_Instances.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.instanceBuffer, 0);
            mappedResource.Dispose();
            //THE POSITION OF THE INSTANCES


            //I am not changing the indexes of the bytes per frame so it's commented out.
            //I AM NOT SURE WHY I WASN'T USING THE INSTANCE POSITION AS THE INDEX, BUT IT MIGHT BE BECAUSE THE INSTANCE POSITION WASN'T A WHOLE NUMBER ANYMORE, SO I DECIDED TO SEND THE INDEX OF THE X/Y/Z OF THE INSTANCE WHEN CREATING THE ARRAY
            //SO THAT INSIDE THE SHADER THE INDEX IS EASILY ACCESSIBLE. SO BEFORE THAT I WAS USING THE INSTANCE POSITION ALSO AS THE INDEX BECAUSE THE INSTANCE POSITION WAS WHOLE NUMBERS.
            /*device.ImmediateContext.MapSubresource(chunkdat.indexBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(chunkdat.SC_instancedChunk_Instances, 0, chunkdat.SC_instancedChunk_Instances.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.indexBuffer, 0);
            mappedResource.Dispose();
            //I AM NOT SURE WHY I WASN'T USING THE INSTANCE POSITION AS THE INDEX, BUT IT MIGHT BE BECAUSE THE INSTANCE POSITION WASN'T A WHOLE NUMBER ANYMORE, SO I DECIDED TO SEND THE INDEX OF THE X/Y/Z OF THE INSTANCE WHEN CREATING THE ARRAY
            //SO THAT INSIDE THE SHADER THE INDEX IS EASILY ACCESSIBLE. SO BEFORE THAT I WAS USING THE INSTANCE POSITION ALSO AS THE INDEX BECAUSE THE INSTANCE POSITION WAS WHOLE NUMBERS.


            //WORLD MATRIX POSITION OF THE MAIN OBJECT (THAT THE INSTANCES ARE BASING THEIR ORIGIN POSITION ON)
            device.ImmediateContext.MapSubresource(chunkdat.constantMatrixPosBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streamerTWO);
            streamerTWO.WriteRange(chunkdat.matrixBuffer, 0, chunkdat.matrixBuffer.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.constantMatrixPosBuffer, 0);
            streamerTWO.Dispose();
            //WORLD MATRIX POSITION OF THE MAIN OBJECT (THAT THE INSTANCES ARE BASING THEIR ORIGIN POSITION ON)


            //light buffer
            device.ImmediateContext.MapSubresource(chunkdat.constantLightBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
            mappedResourceLight.WriteRange(chunkdat.lightBuffer, 0, chunkdat.lightBuffer.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.constantLightBuffer, 0);
            mappedResourceLight.Dispose();
            //light buffer


            if (chunkdat.switchForRender == 1)
            {
            

        

                //var matrixBufferDescriptionVertex00 = new BufferDescription()
                //{
                //    Usage = ResourceUsage.Dynamic,
                //    SizeInBytes = Marshal.SizeOf(typeof(int)) * tinyChunkWidth * tinyChunkHeight * tinyChunkDepth * numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth,
                //    BindFlags = BindFlags.ConstantBuffer,
                //    CpuAccessFlags = CpuAccessFlags.Write,
                //    OptionFlags = ResourceOptionFlags.None,
                //    StructureByteStride = 0
                //};
                //var mapBuffer = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionVertex00);
                //chunkdat.mapBuffer = mapBuffer;

                //device.ImmediateContext.InputAssembler.SetIndexBuffer(IndicesBuffer, SharpDX.DXGI.Format.R32_UInt, 0);
                //device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                //device.ImmediateContext.InputAssembler.InputLayout = Layout;
                //device.ImmediateContext.VertexShader.Set(VertexShader);
                //device.ImmediateContext.PixelShader.Set(PixelShader);
                //device.ImmediateContext.GeometryShader.Set(null);

                try
                {

                    //LOCATION OF THE BYTE IN THE WIDTH FOR TEXTURE ALIGNMENT
                    device.ImmediateContext.MapSubresource(chunkdat.InstanceBufferLocW, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                    mappedResource.WriteRange(chunkdat.instancesLocationW, 0, chunkdat.instancesLocationW.Length);
                    device.ImmediateContext.UnmapSubresource(chunkdat.InstanceBufferLocW, 0);
                    mappedResource.Dispose();
                    //LOCATION OF THE BYTE IN THE WIDTH FOR TEXTURE ALIGNMENT

                    //LOCATION OF THE BYTE IN THE HEIGHT FOR TEXTURE ALIGNMENT
                    device.ImmediateContext.MapSubresource(chunkdat.InstanceBufferLocH, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                    mappedResource.WriteRange(chunkdat.instancesLocationH, 0, chunkdat.instancesLocationH.Length);
                    device.ImmediateContext.UnmapSubresource(chunkdat.InstanceBufferLocH, 0);
                    mappedResource.Dispose();
                    //LOCATION OF THE BYTE IN THE HEIGHT FOR TEXTURE ALIGNMENT


                    //3D TEXTURE?
                    //device.ImmediateContext.MapSubresource(chunkdat.InstanceBufferLocD, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                    //mappedResource.WriteRange(chunkdat.instancesLocationD, 0, chunkdat.instancesLocationD.Length);
                    //device.ImmediateContext.UnmapSubresource(chunkdat.InstanceBufferLocD, 0);
                    //mappedResource.Dispose();
                    //3D TEXTURE?

                }
                catch (Exception ex)
                {
                    MainWindow.MessageBox((IntPtr)0, ex.ToString() + "", "Oculus Error", 0);
                }

                //chunkdat.switchForRender = 0;
            }


            /*
            //WHEN ACTIVATING THE PHYSICS ENGINE FOR THE INSTANCED CHUNKS, THEIR DIRECTION FORWARD/UP/RIGHT HAS TO BE SENT TO THE SHADER SO THAT THEY ARE TREATED AS INDIVIDUAL OBJECTS WITH POSITIONS AND ROTATIONS.
            //FORWARD DIRECTION WRITING TO BUFFER 4 THE SHADER
            device.ImmediateContext.MapSubresource(chunkdat.InstanceRotationBufferFORWARD, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(chunkdat.SC_instancedChunk_InstancesFORWARD, 0, chunkdat.SC_instancedChunk_InstancesFORWARD.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.InstanceRotationBufferFORWARD, 0);
            mappedResource.Dispose();
            //RIGHT DIRECTION WRITING TO BUFFER 4 THE SHADER
            device.ImmediateContext.MapSubresource(chunkdat.InstanceRotationBufferRIGHT, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(chunkdat.SC_instancedChunk_InstancesRIGHT, 0, chunkdat.SC_instancedChunk_InstancesRIGHT.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.InstanceRotationBufferRIGHT, 0);
            mappedResource.Dispose();
            //UP DIRECTION WRITING TO BUFFER 4 THE SHADER
            device.ImmediateContext.MapSubresource(chunkdat.InstanceRotationBufferUP, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(chunkdat.SC_instancedChunk_InstancesUP, 0, chunkdat.SC_instancedChunk_InstancesUP.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.InstanceRotationBufferUP, 0);
            mappedResource.Dispose();
            //WHEN ACTIVATING THE PHYSICS ENGINE FOR THE INSTANCED CHUNKS, THEIR DIRECTION FORWARD/UP/RIGHT HAS TO BE SENT TO THE SHADER SO THAT THEY ARE TREATED AS INDIVIDUAL OBJECTS WITH POSITIONS AND ROTATIONS.
            


            //device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
            //{
            //    new VertexBufferBinding(InstanceBuffer, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceType)),0),
            //});

            device.ImmediateContext.VertexShader.SetConstantBuffer(0, chunkdat.constantMatrixPosBuffer);
            //device.ImmediateContext.VertexShader.SetConstantBuffer(1, chunkdat.mapBuffer);

            device.ImmediateContext.PixelShader.SetConstantBuffer(0, chunkdat.constantLightBuffer);

            device.ImmediateContext.PixelShader.SetShaderResource(0, shaderResourceView2D);

            device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
            {
                new VertexBufferBinding(VertexBuffer,Marshal.SizeOf(typeof(SC_instancedChunk.DVertex)), 0),
                //new VertexBufferBinding(chunkdat.indexBuffer, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceType)),0),
            });



            //device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
            //{
            //    new VertexBufferBinding(chunkdat.indexBuffer, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceType)),0),
            //});

            device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
            {
                new VertexBufferBinding(chunkdat.instanceBuffer, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceType)),0),
            });




            
            //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
            //{
            //    new VertexBufferBinding(VertexBuffer,Marshal.SizeOf(typeof(SC_instancedChunk.DVertex)), 0),
            //});


            device.ImmediateContext.InputAssembler.SetVertexBuffers(8, new[]
            {
                new VertexBufferBinding(chunkdat.InstanceRotationBufferFORWARD, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceShipData)),0),
            });
            device.ImmediateContext.InputAssembler.SetVertexBuffers(9, new[]
            {
                new VertexBufferBinding(chunkdat.InstanceRotationBufferRIGHT, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceShipData)),0),
            });
            device.ImmediateContext.InputAssembler.SetVertexBuffers(10, new[]
            {
                new VertexBufferBinding(chunkdat.InstanceRotationBufferUP, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceShipData)),0),
            });


            device.ImmediateContext.InputAssembler.SetVertexBuffers(5, new[]
            {
                new VertexBufferBinding(chunkdat.InstanceBufferLocW, Marshal.SizeOf(typeof( SCCoreSystems.sc_console.sc_graphics_sec.DInstanceTypeLocW)),0),
            });

            device.ImmediateContext.InputAssembler.SetVertexBuffers(6, new[]
            {
                new VertexBufferBinding(chunkdat.InstanceBufferLocH, Marshal.SizeOf(typeof( SCCoreSystems.sc_console.sc_graphics_sec.DInstanceTypeLocH)),0),
            });
            device.ImmediateContext.InputAssembler.SetVertexBuffers(7, new[]
            {
                new VertexBufferBinding(chunkdat.InstanceBufferLocD, Marshal.SizeOf(typeof( SCCoreSystems.sc_console.sc_graphics_sec.DInstanceTypeLocD)),0),
            });



            //device.ImmediateContext.VertexShader.SetConstantBuffer(2, chunkdat.instanceBuffer);


            device.ImmediateContext.InputAssembler.SetIndexBuffer(IndicesBuffer, SharpDX.DXGI.Format.R32_UInt, 0);
            device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            device.ImmediateContext.InputAssembler.InputLayout = Layout;
            device.ImmediateContext.VertexShader.Set(VertexShader);
            device.ImmediateContext.PixelShader.Set(PixelShader);
            device.ImmediateContext.GeometryShader.Set(null);

            device.ImmediateContext.DrawIndexedInstanced(indexArray.Length, SC_Globals.numberOfInstancesPerObjectInWidth * SC_Globals.numberOfInstancesPerObjectInHeight * SC_Globals.numberOfInstancesPerObjectInDepth, 0, 0, 0);

            //Console.WriteLine(timeCalculator.Elapsed.Ticks);*/




            device.ImmediateContext.MapSubresource(chunkdat.constantMatrixPosBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streamerTWO);
            streamerTWO.WriteRange(chunkdat.matrixBuffer, 0, chunkdat.matrixBuffer.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.constantMatrixPosBuffer, 0);
            streamerTWO.Dispose();







            if (chunkdat.switchForRender == 1)
            {          
                
                //LOCATION OF THE BYTE IN THE WIDTH FOR TEXTURE ALIGNMENT
                device.ImmediateContext.MapSubresource(chunkdat.InstanceBufferLocW, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                mappedResource.WriteRange(chunkdat.instancesLocationW, 0, chunkdat.instancesLocationW.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.InstanceBufferLocW, 0);
                mappedResource.Dispose();
                //LOCATION OF THE BYTE IN THE WIDTH FOR TEXTURE ALIGNMENT

                //LOCATION OF THE BYTE IN THE HEIGHT FOR TEXTURE ALIGNMENT
                device.ImmediateContext.MapSubresource(chunkdat.InstanceBufferLocH, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                mappedResource.WriteRange(chunkdat.instancesLocationH, 0, chunkdat.instancesLocationH.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.InstanceBufferLocH, 0);
                mappedResource.Dispose();
                //LOCATION OF THE BYTE IN THE HEIGHT FOR TEXTURE ALIGNMENT





                device.ImmediateContext.MapSubresource(chunkdat.instanceBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                mappedResource.WriteRange(chunkdat.SC_instancedChunk_Instances, 0, chunkdat.SC_instancedChunk_Instances.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.instanceBuffer, 0);
                mappedResource.Dispose();

                device.ImmediateContext.MapSubresource(chunkdat.constantLightBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                mappedResourceLight.WriteRange(chunkdat.lightBuffer, 0, chunkdat.lightBuffer.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.constantLightBuffer, 0);
                mappedResourceLight.Dispose();


   




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


                device.ImmediateContext.InputAssembler.SetIndexBuffer(IndexBuffer, SharpDX.DXGI.Format.R32_UInt, 0);

                device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                device.ImmediateContext.InputAssembler.InputLayout = Layout;
                device.ImmediateContext.VertexShader.Set(VertexShader);
                device.ImmediateContext.PixelShader.Set(PixelShader);
                device.ImmediateContext.GeometryShader.Set(null);


                device.ImmediateContext.InputAssembler.SetVertexBuffers(5, new[]
                {
                    new VertexBufferBinding(chunkdat.InstanceBufferLocW, Marshal.SizeOf(typeof( SCCoreSystems.sc_console.sc_graphics_sec.DInstanceTypeLocW)),0),
                });

                device.ImmediateContext.InputAssembler.SetVertexBuffers(6, new[]
                {
                    new VertexBufferBinding(chunkdat.InstanceBufferLocH, Marshal.SizeOf(typeof( SCCoreSystems.sc_console.sc_graphics_sec.DInstanceTypeLocH)),0),
                });

            



                chunkdat.switchForRender = 0;
            }




            device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
            {
                    new VertexBufferBinding(VertexBuffer,Marshal.SizeOf(typeof(SC_instancedChunk.DVertex)), 0),
                });

            device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
            {
                    new VertexBufferBinding(chunkdat.instanceBuffer, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceType)),0),
                });



            device.ImmediateContext.VertexShader.SetConstantBuffer(0, chunkdat.constantMatrixPosBuffer);
            //device.ImmediateContext.VertexShader.SetConstantBuffer(1, chunkdat.mapBuffer);


            device.ImmediateContext.PixelShader.SetConstantBuffer(0, chunkdat.constantLightBuffer);

            device.ImmediateContext.PixelShader.SetShaderResource(0, shaderResourceView2D);



            //device.ImmediateContext.DrawIndexedInstanced(indexArray.Length, SC_Globals.numberOfInstancesPerObjectInWidth * SC_Globals.numberOfInstancesPerObjectInHeight * SC_Globals.numberOfInstancesPerObjectInDepth, 0, 0, 0);
            //Console.WriteLine(timeCalculator.Elapsed.Ticks);







            device.ImmediateContext.InputAssembler.SetIndexBuffer(IndicesBuffer, SharpDX.DXGI.Format.R32_UInt, 0);
            device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            device.ImmediateContext.InputAssembler.InputLayout = Layout;
            device.ImmediateContext.VertexShader.Set(VertexShader);
            device.ImmediateContext.PixelShader.Set(PixelShader);
            device.ImmediateContext.GeometryShader.Set(null);

            device.ImmediateContext.DrawIndexedInstanced(indexArray.Length, SC_Globals.numberOfInstancesPerObjectInWidth * SC_Globals.numberOfInstancesPerObjectInHeight * SC_Globals.numberOfInstancesPerObjectInDepth, 0, 0, 0);









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