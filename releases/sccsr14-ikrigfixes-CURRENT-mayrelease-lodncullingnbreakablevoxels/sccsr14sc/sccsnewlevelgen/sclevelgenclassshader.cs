﻿using SharpDX.Direct3D11;
using SharpDX.WIC;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System;

using SharpDX;
using SharpDX.D3DCompiler;
using System.Runtime.InteropServices;
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

using System.Threading.Tasks;

namespace sccs
{
    public class sclevelgenclassshader : IDisposable
    {

        public void Dispose()
        {
            if (device != null)
            {
                device.Dispose();
                device = null;
            }
        }


        //static sclevelgenclassshader chunkShader;

        public SharpDX.Direct3D11.Device device;
        //public SharpDX.Direct3D11.Buffer _someBuffer;

        //SharpDX.Direct3D11.Buffer MapBuffer;

        int usegeometryshader;

        public sclevelgenclassshader(SharpDX.Direct3D11.Device _device)
        {


            //chunkShader = this;
            this.device = _device;


            /*Adapter1 someadapter;
            int _numAdapter = 0;

            using (var _factory = new SharpDX.DXGI.Factory2())
            {
                someadapter = _factory.GetAdapter1(_numAdapter);
            }

            somestruct.thedevice = new Device(someadapter);*/

            //somestruct.thedevice = _device;//  new Device(someadapter);

            //var somepath = System.IO.Directory.GetCurrentDirectory();
            //var somepath = Environment.CurrentDirectory;
            //Program.MessageBox((IntPtr)0, "" + somepath, "scmsg", 0);


            // shaderResourceView2D = LoadTextureFromFile(device, "../../../terrainGrassDirt.bmp");
        }

        public int someCounter = 0;
        public int startOnce = 1;

        DataStream mappedResource;
        DataStream streamerTWO;
        DataStream mappedResourceLight;

        Stopwatch timeCalculator = new Stopwatch();


        int updatebufferthread = 0;




        public sclevelgenclassPrim.chunkData Renderer(sclevelgenclassPrim.chunkData chunkdat, int indexBuilt, ShaderResourceView shaderResourceView2D_, int voxeltype_) //async
        {
            //TOREADD 
            //TOREADD 
            /*device.ImmediateContext.MapSubresource(chunkdat.instancesbytemapsbuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
            mappedResourceLight.WriteRange(chunkdat.instancesbytemaps, 0, chunkdat.instancesbytemaps.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.instancesbytemapsbuffer, 0);
            //mappedResourceLight.Dispose();*/
            //TOREADD
            //TOREADD





            device.ImmediateContext.MapSubresource(chunkdat.constantMatrixPosBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streamerTWO);
            streamerTWO.WriteRange(chunkdat.matrixBuffer, 0, chunkdat.matrixBuffer.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.constantMatrixPosBuffer, 0);
            streamerTWO.Dispose();






            device.ImmediateContext.MapSubresource(chunkdat.constantLightBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
            mappedResourceLight.WriteRange(chunkdat.lightBuffer, 0, chunkdat.lightBuffer.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.constantLightBuffer, 0);
            mappedResourceLight.Dispose();



            /*
            if (chunkdat.istypeofmesh == 1 && chunkdat.copytobuffer == 1)
            {

                //TOREADD 
                //TOREADD 
                device.ImmediateContext.MapSubresource(chunkdat.instancesmatrixbuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                mappedResourceLight.WriteRange(chunkdat.instancesmatrix, 0, chunkdat.instancesmatrix.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.instancesmatrixbuffer, 0);
                mappedResourceLight.Dispose();
                //TOREADD
                //TOREADD

                //TOREADD 
                //TOREADD 
                device.ImmediateContext.MapSubresource(chunkdat.instancesmatrixbufferb, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                mappedResourceLight.WriteRange(chunkdat.instancesmatrixb, 0, chunkdat.instancesmatrixb.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.instancesmatrixbufferb, 0);
                mappedResourceLight.Dispose();
                //TOREADD
                //TOREADD


                //TOREADD 
                //TOREADD 
                device.ImmediateContext.MapSubresource(chunkdat.instancesmatrixbufferc, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                mappedResourceLight.WriteRange(chunkdat.instancesmatrixc, 0, chunkdat.instancesmatrixc.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.instancesmatrixbufferc, 0);
                mappedResourceLight.Dispose();
                //TOREADD
                //TOREADD


                //TOREADD 
                //TOREADD 
                device.ImmediateContext.MapSubresource(chunkdat.instancesmatrixbufferd, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                mappedResourceLight.WriteRange(chunkdat.instancesmatrixd, 0, chunkdat.instancesmatrixd.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.instancesmatrixbufferd, 0);
                mappedResourceLight.Dispose();
                //TOREADD
                //shaderResourceView2D
                chunkdat.copytobuffer = 0;
            }*/


            if (chunkdat.switchForRender == 3)
            {
                /*
                //TOREADD 
                //TOREADD 
                device.ImmediateContext.MapSubresource(chunkdat.somerandomvaluebuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                mappedResourceLight.WriteRange(chunkdat.randomvaluearray, 0, chunkdat.randomvaluearray.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.somerandomvaluebuffer, 0);
                mappedResourceLight.Dispose();
                //TOREADD 




                //TOREADD 
                //TOREADD 
                device.ImmediateContext.MapSubresource(chunkdat.someoculusdirbuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                mappedResourceLight.WriteRange(chunkdat.someovrdir, 0, chunkdat.someovrdir.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.someoculusdirbuffer, 0);
                mappedResourceLight.Dispose();
                //TOREADD */

                /*
                device.ImmediateContext.MapSubresource(chunkdat.indexBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                //mappedResource.WriteRange(chunkdat, 0, chunkdat.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.indexBuffer, 0);
                //mappedResource.Dispose();*/


                /*device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
                {
                    new VertexBufferBinding(chunkdat.indexBuffer, Marshal.SizeOf(typeof(sclevelgenclass.DVertex)),0),
                });*/



                /*
                device.ImmediateContext.InputAssembler.SetVertexBuffers(6, new[]
                {
                    new VertexBufferBinding(chunkdat.instancesbytemapsbuffer, Marshal.SizeOf(typeof(sclevelgenclass.DInstancesByteMap)),0),
                });
                */







                //TOREADD 
                //TOREADD 








                /*
                device.ImmediateContext.InputAssembler.SetVertexBuffers(5, new[]
             {
                new VertexBufferBinding(chunkdat.instanceBufferHeightmap, Marshal.SizeOf(typeof(sclevelgenclass.heightmapinstance)),0),
            });*/





                /*
                device.ImmediateContext.MapSubresource(chunkdat.instanceBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                mappedResource.WriteRange(chunkdat.sclevelgenclass_Instances, 0, chunkdat.sclevelgenclass_Instances.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.instanceBuffer, 0);
                mappedResource.Dispose();
                //TOREADD 



                //TOREADD 
                device.ImmediateContext.MapSubresource(chunkdat.InstanceBufferLocW, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                mappedResource.WriteRange(chunkdat.instancesLocationW, 0, chunkdat.instancesLocationW.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.InstanceBufferLocW, 0);
                mappedResource.Dispose();

                device.ImmediateContext.MapSubresource(chunkdat.InstanceBufferLocH, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                mappedResource.WriteRange(chunkdat.instancesLocationH, 0, chunkdat.instancesLocationH.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.InstanceBufferLocH, 0);
                mappedResource.Dispose();
                //TOREADD*/



                /*
                //TOREADD
                device.ImmediateContext.MapSubresource(chunkdat.InstanceRotationBufferFORWARD, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                mappedResource.WriteRange(chunkdat.sclevelgenclass_InstancesFORWARD, 0, chunkdat.sclevelgenclass_InstancesFORWARD.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.InstanceRotationBufferFORWARD, 0);
                mappedResource.Dispose();

                device.ImmediateContext.MapSubresource(chunkdat.InstanceRotationBufferRIGHT, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                mappedResource.WriteRange(chunkdat.sclevelgenclass_InstancesRIGHT, 0, chunkdat.sclevelgenclass_InstancesRIGHT.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.InstanceRotationBufferRIGHT, 0);
                mappedResource.Dispose();

                device.ImmediateContext.MapSubresource(chunkdat.InstanceRotationBufferUP, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                mappedResource.WriteRange(chunkdat.sclevelgenclass_InstancesUP, 0, chunkdat.sclevelgenclass_InstancesUP.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.InstanceRotationBufferUP, 0);
                mappedResource.Dispose();*/



                try
                {

                }
                catch (Exception ex)
                {
                    Program.MessageBox((IntPtr)0, ex.ToString() + "", "Oculus Error", 0);
                }




                chunkdat.switchForRender = 4;

                /*if (chunkdat.switchForRender == 1)
                {
                    chunkdat.switchForRender = 2;
                }
                else if (chunkdat.switchForRender == 2)
                {
                    chunkdat.switchForRender = 3;
                }*/
            }
            else if (chunkdat.switchForRender == 0)
            {




                //chunkdat.switchForRender = 2;
            }


            /*
            if (chunkdat.istypeofmesh == 0)
            {

                //TOREADD 
                //TOREADD 
                device.ImmediateContext.MapSubresource(chunkdat.instancesmatrixbuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                mappedResourceLight.WriteRange(chunkdat.instancesmatrix, 0, chunkdat.instancesmatrix.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.instancesmatrixbuffer, 0);
                mappedResourceLight.Dispose();
                //TOREADD
                //TOREADD

                //TOREADD 
                //TOREADD 
                device.ImmediateContext.MapSubresource(chunkdat.instancesmatrixbufferb, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                mappedResourceLight.WriteRange(chunkdat.instancesmatrixb, 0, chunkdat.instancesmatrixb.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.instancesmatrixbufferb, 0);
                mappedResourceLight.Dispose();
                //TOREADD
                //TOREADD

                //TOREADD 
                //TOREADD 
                device.ImmediateContext.MapSubresource(chunkdat.instancesmatrixbufferc, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                mappedResourceLight.WriteRange(chunkdat.instancesmatrixc, 0, chunkdat.instancesmatrixc.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.instancesmatrixbufferc, 0);
                mappedResourceLight.Dispose();
                //TOREADD
                //TOREADD

                //TOREADD 
                //TOREADD 
                device.ImmediateContext.MapSubresource(chunkdat.instancesmatrixbufferd, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                mappedResourceLight.WriteRange(chunkdat.instancesmatrixd, 0, chunkdat.instancesmatrixd.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.instancesmatrixbufferd, 0);
                mappedResourceLight.Dispose();
                //TOREADD
                //shaderResourceView2D

            }*/






            /*
            if (scgraphics.scgraphicssec.currentscgraphicssec.activatevrheightmapfeature == 1 && scgraphics.scgraphicssec.somevoxelvirtualdesktopglobals.tinyChunkWidth == 8 || scgraphics.scgraphicssec.currentscgraphicssec.activatevrheightmapfeature == 1 && scgraphics.scgraphicssec.somevoxelvirtualdesktopglobals.tinyChunkWidth == 16 ||
                scgraphics.scgraphicssec.currentscgraphicssec.activatevrheightmapfeature == 0 && scgraphics.scgraphicssec.somevoxelvirtualdesktopglobals.tinyChunkWidth == 8 || scgraphics.scgraphicssec.currentscgraphicssec.activatevrheightmapfeature == 0 && scgraphics.scgraphicssec.somevoxelvirtualdesktopglobals.tinyChunkWidth == 16)
            {




            }*/





            /*
            if (scgraphics.scgraphicssec.currentscgraphicssec.activatevrheightmapfeature == 1 && scgraphics.scgraphicssec.somevoxelvirtualdesktopglobals.tinyChunkWidth == 4)
            {
                //TOREADD 
                //TOREADD 
                device.ImmediateContext.MapSubresource(chunkdat.instancesmatrixbuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                mappedResourceLight.WriteRange(chunkdat.instancesmatrix, 0, chunkdat.instancesmatrix.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.instancesmatrixbuffer, 0);
                mappedResourceLight.Dispose();
                //TOREADD
                //TOREADD


                /*
                //TOREADD 
                device.ImmediateContext.MapSubresource(chunkdat.instanceBufferHeightmap, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                mappedResourceLight.WriteRange(chunkdat.heightmapmatrix, 0, chunkdat.heightmapmatrix.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.instanceBufferHeightmap, 0);
                //mappedResourceLight.Dispose();
                //TOREADD 

            }*/


            //device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;

            if (voxeltype_ == 0 || voxeltype_ == 2)
            {
                device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            }
            else if (voxeltype_ == 1)
            {
                device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.LineList;
            }








            device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
             {
                new VertexBufferBinding(chunkdat.vertexBuffer,Marshal.SizeOf(typeof(sclevelgenclass.DVertex)), 0),
                //new VertexBufferBinding(chunkdat.indexBuffer, Marshal.SizeOf(typeof(sclevelgenclass.DInstanceType)),0),
            });
            /*
            device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
            {
                new VertexBufferBinding(chunkdat.instanceBuffer, Marshal.SizeOf(typeof(sclevelgenclass.DInstanceType)),0),
            });*/

            /*
            device.ImmediateContext.InputAssembler.SetVertexBuffers(9, new[]
            {
                    new VertexBufferBinding(chunkdat.InstanceBufferLocW, Marshal.SizeOf(typeof( sccs.sclevelgenclass.DInstanceTypeLocW)),0),
                });

            device.ImmediateContext.InputAssembler.SetVertexBuffers(10, new[]
            {
                    new VertexBufferBinding(chunkdat.InstanceBufferLocH, Marshal.SizeOf(typeof( sccs.sclevelgenclass.DInstanceTypeLocH)),0),
                });
            */
            /*
            device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
            {
                new VertexBufferBinding(chunkdat.InstanceRotationBufferFORWARD, Marshal.SizeOf(typeof(sclevelgenclass.DInstanceShipData)),0),
            });
            device.ImmediateContext.InputAssembler.SetVertexBuffers(3, new[]
            {
                new VertexBufferBinding(chunkdat.InstanceRotationBufferRIGHT, Marshal.SizeOf(typeof(sclevelgenclass.DInstanceShipData)),0),
            });
            device.ImmediateContext.InputAssembler.SetVertexBuffers(4, new[]
            {
                new VertexBufferBinding(chunkdat.InstanceRotationBufferUP, Marshal.SizeOf(typeof(sclevelgenclass.DInstanceShipData)),0),
            });*/


            /*
            //TOREADD 
            //TOREADD 
            device.ImmediateContext.InputAssembler.SetVertexBuffers(5, new[]
            {
                    new VertexBufferBinding(chunkdat.instancesmatrixbuffer, Marshal.SizeOf(typeof(sclevelgenclass.DInstanceMatrix)),0),
             });
            device.ImmediateContext.InputAssembler.SetVertexBuffers(6, new[]
            {
                    new VertexBufferBinding(chunkdat.instancesmatrixbufferb, Marshal.SizeOf(typeof(sclevelgenclass.DInstanceMatrix)),0),
             });
            device.ImmediateContext.InputAssembler.SetVertexBuffers(7, new[]
           {
                    new VertexBufferBinding(chunkdat.instancesmatrixbufferc, Marshal.SizeOf(typeof(sclevelgenclass.DInstanceMatrix)),0),
            });
            device.ImmediateContext.InputAssembler.SetVertexBuffers(8, new[]
           {
                    new VertexBufferBinding(chunkdat.instancesmatrixbufferd, Marshal.SizeOf(typeof(sclevelgenclass.DInstanceMatrix)),0),
            });*/




            //mappedResource.Dispose();
            //mappedResourceLight.Dispose();
            device.ImmediateContext.PixelShader.SetShaderResource(0, shaderResourceView2D_);

            //device.ImmediateContext.DomainShader.SetConstantBuffer(0, chunkdat.constantMatrixPosBuffer);

            device.ImmediateContext.VertexShader.SetConstantBuffer(0, chunkdat.constantMatrixPosBuffer);
            //device.ImmediateContext.VertexShader.SetConstantBuffer(1, chunkdat.mapBuffer);
            //device.ImmediateContext.VertexShader.SetConstantBuffer(2, chunkdat.someoculusdirbuffer);

            //device.ImmediateContext.PixelShader.SetConstantBuffer(0, chunkdat.constantMatrixPosBuffer);

            device.ImmediateContext.VertexShader.SetConstantBuffer(1, chunkdat.constantLightBuffer);
            device.ImmediateContext.PixelShader.SetConstantBuffer(0, chunkdat.constantLightBuffer);

            //device.ImmediateContext.PixelShader.SetConstantBuffer(2, chunkdat.someoculusdirbuffer);


            //device.ImmediateContext.VertexShader.SetConstantBuffer(1, chunkdat.constantLightBuffer);
            //device.ImmediateContext.PixelShader.SetConstantBuffer(1, chunkdat.somerandomvaluebuffer);

            //device.ImmediateContext.PixelShader.SetConstantBuffer(2, chunkdat.someoculusdirbuffer);
















            //device.ImmediateContext.Rasterizer.State = sc_console.SC_console_directx.D3D.RasterstateCullNone;
            device.ImmediateContext.InputAssembler.SetIndexBuffer(chunkdat.IndicesBuffer, SharpDX.DXGI.Format.R32_UInt, 0);
            device.ImmediateContext.InputAssembler.InputLayout = chunkdat.Layout;

            device.ImmediateContext.VertexShader.Set(chunkdat.VertexShader);
            //device.ImmediateContext.HullShader.Set(chunkdat.HullShader);
            //device.ImmediateContext.DomainShader.Set(chunkdat.DomainShader);
            device.ImmediateContext.PixelShader.Set(chunkdat.PixelShader);

            //device.ImmediateContext.GeometryShader.Set(null); //GeometryShader

            //Console.WriteLine(usegeometryshader);
            if (usegeometryshader == 0)
            {
                device.ImmediateContext.GeometryShader.Set(null); //GeometryShader
            }
            else if (usegeometryshader == 1)
            {
                device.ImmediateContext.GeometryShader.Set(chunkdat.GeometryShader); //GeometryShader
            }

            // Set the sampler state in the pixel shader.
            //device.ImmediateContext.PixelShader.SetSampler(0, chunkdat.samplerState);
            //device.ImmediateContext.Rasterizer = 




            //device.ImmediateContext.Draw(chunkdat.arrayofverts.Length, 0);
            //device.ImmediateContext.DrawIndexedInstanced(chunkdat.arrayofindices.Length, 1, 0, 0, 0);

            //Console.WriteLine(chunkdat.arrayofindices.Length);
            //device.ImmediateContext.DrawIndexed(chunkdat.arrayofindices.Length,0,0);
            //Console.WriteLine(timeCalculator.Elapsed.Ticks);

            device.ImmediateContext.DrawIndexed(chunkdat.arrayofindices.Length, 0, 0);

            return chunkdat;
        }

        /*
        public sclevelgenclassPrim.chunkData renderthemesh(sclevelgenclassPrim.chunkData chunkdat, int indexBuilt, ShaderResourceView shaderResourceView2D_, int voxeltype_) //async
        {

            device.ImmediateContext.DrawIndexedInstanced(chunkdat.originalArrayOfIndices.Length, chunkdat.numberOfInstancesPerObjectInWidth * chunkdat.numberOfInstancesPerObjectInHeight * chunkdat.numberOfInstancesPerObjectInDepth, 0, 0, 0);


            //Console.WriteLine(timeCalculator.Elapsed.Ticks);

            return chunkdat;
        }*/



        /*
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
        }*/
    }
}