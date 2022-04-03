using SharpDX.Direct3D11;
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


            /*Adapter1 someadapter;
            int _numAdapter = 0;

            using (var _factory = new SharpDX.DXGI.Factory2())
            {
                someadapter = _factory.GetAdapter1(_numAdapter);
            }

            somestruct.thedevice = new Device(someadapter);*/

            somestruct.thedevice = _device;//  new Device(someadapter);



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

       public ShaderResourceView shaderResourceView2D;


        int updatebufferthread = 0;

        threadbufferstruct somestruct = new threadbufferstruct();

        struct threadbufferstruct
        {
            public SharpDX.Direct3D11.Buffer constantMatrixPosBuffer;
            public SharpDX.Direct3D11.Buffer constantLightBuffer;
            public SharpDX.Direct3D11.Buffer InstanceBufferLocW;
            public SharpDX.Direct3D11.Buffer InstanceBufferLocH;

            public SC_instancedChunk.DInstanceTypeLocW[] instancesLocationW;
            public SC_instancedChunk.DInstanceTypeLocH[] instancesLocationH;
            public SC_instancedChunkPrim.DLightBufferEr[] lightBuffer;
            public SC_instancedChunkPrim.DMatrixBuffer[] matrixBuffer;

            public SharpDX.Direct3D11.Device thedevice;
        }




        public SC_instancedChunkPrim.chunkData Renderer(SC_instancedChunkPrim.chunkData chunkdat, int indexBuilt, ShaderResourceView shaderResourceView2D_,int voxeltype_) //async
        {


            // Lock the tessellation constant buffer so it can be written to.
            /*device.ImmediateContext.MapSubresource(chunkdat.ConstantTessellationBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(chunkdat.tessellationBuffer);
            device.ImmediateContext.UnmapSubresource(chunkdat.ConstantTessellationBuffer, 0);
            device.ImmediateContext.HullShader.SetConstantBuffer(0, chunkdat.ConstantTessellationBuffer);
            */






            //int[] byteMap = chunkdat.arrayOfSomeMap.SelectMany(a => a).ToArray();

            //timeCalculator.Stop();
            //timeCalculator.Reset();
            //timeCalculator.Start();


            //TOREADD 
            //TOREADD 
            device.ImmediateContext.MapSubresource(chunkdat.someoculusdirbuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
            mappedResourceLight.WriteRange(chunkdat.someovrdir, 0, chunkdat.someovrdir.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.someoculusdirbuffer, 0);
            mappedResourceLight.Dispose();
            //TOREADD 



            //TOREADD
            /*
            somestruct.constantMatrixPosBuffer = chunkdat.constantMatrixPosBuffer;
            somestruct.matrixBuffer = chunkdat.matrixBuffer;

            somestruct.constantLightBuffer = chunkdat.constantLightBuffer;
            somestruct.lightBuffer = chunkdat.lightBuffer;

            somestruct.InstanceBufferLocW = chunkdat.InstanceBufferLocW;
            somestruct.instancesLocationW = chunkdat.instancesLocationW;

            somestruct.InstanceBufferLocH = chunkdat.InstanceBufferLocH;
            somestruct.instancesLocationH = chunkdat.instancesLocationH;



            
            if (updatebufferthread == 0)
            {

                /*Thread main_thread_update = new Thread(() =>
                {
                    int bufferswtc = 0;

                    DataStream streamerthread;
                    DataStream streamlight;
                    DataStream streaminstanceloc;

                _thread_looper:

                    //Program.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                    try
                    {
                        
                        somestruct.thedevice.ImmediateContext.MapSubresource(somestruct.constantMatrixPosBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streamerthread);
                        streamerthread.WriteRange(somestruct.matrixBuffer, 0, somestruct.matrixBuffer.Length);
                        somestruct.thedevice.ImmediateContext.UnmapSubresource(somestruct.constantMatrixPosBuffer, 0);
                        streamerthread.Dispose();

                        somestruct.thedevice.ImmediateContext.MapSubresource(somestruct.constantLightBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streamlight);
                        streamlight.WriteRange(somestruct.lightBuffer, 0, somestruct.lightBuffer.Length);
                        somestruct.thedevice.ImmediateContext.UnmapSubresource(somestruct.constantLightBuffer, 0);
                        streamlight.Dispose();

                        //TOREADD 

                        somestruct.thedevice.ImmediateContext.MapSubresource(somestruct.InstanceBufferLocW, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streaminstanceloc);
                        streaminstanceloc.WriteRange(somestruct.instancesLocationW, 0, somestruct.instancesLocationW.Length);
                        somestruct.thedevice.ImmediateContext.UnmapSubresource(somestruct.InstanceBufferLocW, 0);
                        streaminstanceloc.Dispose();

                        somestruct.thedevice.ImmediateContext.MapSubresource(somestruct.InstanceBufferLocH, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streaminstanceloc);
                        streaminstanceloc.WriteRange(somestruct.instancesLocationH, 0, somestruct.instancesLocationH.Length);
                        somestruct.thedevice.ImmediateContext.UnmapSubresource(somestruct.InstanceBufferLocH, 0);
                        streaminstanceloc.Dispose();
                        //TOREADD
                        //TOREADD 

                    }
                    catch (Exception ex)
                    {
                        Program.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                    }


                    Thread.Sleep(1);
                    goto _thread_looper;

                    //ShutDown();
                    //ShutDownGraphics();

                }, 0);

                main_thread_update.IsBackground = true;
                main_thread_update.Priority = ThreadPriority.Lowest; //AboveNormal
                main_thread_update.SetApartmentState(ApartmentState.STA);
                main_thread_update.Start();*/



            /*Program.shadertask = Task<object[]>.Factory.StartNew((tester0001) =>
            {

                DataStream streamerthread;
                DataStream streamlight;
                DataStream streaminstanceloc;

            //////CONSOLE WRITER=>
            _thread_loop_console:
                somestruct.thedevice.ImmediateContext.MapSubresource(somestruct.constantMatrixPosBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streamerthread);
                streamerthread.WriteRange(somestruct.matrixBuffer, 0, somestruct.matrixBuffer.Length);
                somestruct.thedevice.ImmediateContext.UnmapSubresource(somestruct.constantMatrixPosBuffer, 0);
                streamerthread.Dispose();

                somestruct.thedevice.ImmediateContext.MapSubresource(somestruct.constantLightBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streamlight);
                streamlight.WriteRange(somestruct.lightBuffer, 0, somestruct.lightBuffer.Length);
                somestruct.thedevice.ImmediateContext.UnmapSubresource(somestruct.constantLightBuffer, 0);
                streamlight.Dispose();

                //TOREADD 

                somestruct.thedevice.ImmediateContext.MapSubresource(somestruct.InstanceBufferLocW, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streaminstanceloc);
                streaminstanceloc.WriteRange(somestruct.instancesLocationW, 0, somestruct.instancesLocationW.Length);
                somestruct.thedevice.ImmediateContext.UnmapSubresource(somestruct.InstanceBufferLocW, 0);
                streaminstanceloc.Dispose();

                somestruct.thedevice.ImmediateContext.MapSubresource(somestruct.InstanceBufferLocH, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streaminstanceloc);
                streaminstanceloc.WriteRange(somestruct.instancesLocationH, 0, somestruct.instancesLocationH.Length);
                somestruct.thedevice.ImmediateContext.UnmapSubresource(somestruct.InstanceBufferLocH, 0);
                streaminstanceloc.Dispose();
                //TOREADD
                //TOREADD 

                Thread.Sleep(1);

                goto _thread_loop_console;
                //////CONSOLE WRITER <=
            }, somestruct);

            updatebufferthread = 1;
        }



        chunkdat.constantMatrixPosBuffer = somestruct.constantMatrixPosBuffer;
        chunkdat.matrixBuffer = somestruct.matrixBuffer ;

        chunkdat.constantLightBuffer =  somestruct.constantLightBuffer ;
        chunkdat.lightBuffer = somestruct.lightBuffer ;

        chunkdat.InstanceBufferLocW = somestruct.InstanceBufferLocW ;
        chunkdat.instancesLocationW =  somestruct.instancesLocationW ;

        chunkdat.InstanceBufferLocH = somestruct.InstanceBufferLocH ;
        chunkdat.instancesLocationH =  somestruct.instancesLocationH ;

        */













            //TOREADD 
            //TOREADD 
            /*device.ImmediateContext.MapSubresource(chunkdat.instancesbytemapsbuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
            mappedResourceLight.WriteRange(chunkdat.instancesbytemaps, 0, chunkdat.instancesbytemaps.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.instancesbytemapsbuffer, 0);
            mappedResourceLight.Dispose();*/
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



            if (chunkdat.switchForRender == 1)
            {






                device.ImmediateContext.MapSubresource(chunkdat.instanceBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                mappedResource.WriteRange(chunkdat.SC_instancedChunk_Instances, 0, chunkdat.SC_instancedChunk_Instances.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.instanceBuffer, 0);
                mappedResource.Dispose();
                //TOREADD 



                //TOREADD









                /*
                device.ImmediateContext.MapSubresource(chunkdat.indexBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                //mappedResource.WriteRange(chunkdat, 0, chunkdat.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.indexBuffer, 0);
                mappedResource.Dispose();*/








                /*device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
                {
                    new VertexBufferBinding(chunkdat.indexBuffer, Marshal.SizeOf(typeof(SC_instancedChunk.DVertex)),0),
                });*/
              
         

                /*
                device.ImmediateContext.InputAssembler.SetVertexBuffers(6, new[]
                {
                    new VertexBufferBinding(chunkdat.instancesbytemapsbuffer, Marshal.SizeOf(typeof(SC_instancedChunk.DInstancesByteMap)),0),
                });
                */
         





            
                //TOREADD 
                //TOREADD 



                if (voxeltype_ == 0)
                {
                    device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                }
                else if (voxeltype_ == 1)
                {
                    device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.LineList;
                }


      


                device.ImmediateContext.InputAssembler.SetVertexBuffers(5, new[]
             {
                new VertexBufferBinding(chunkdat.instanceBufferHeightmap, Marshal.SizeOf(typeof(SC_instancedChunk.heightmapinstance)),0),
            });

                //TOREADD*/
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



                try
                {

                }
                catch (Exception ex)
                {
                    Program.MessageBox((IntPtr)0, ex.ToString() + "", "Oculus Error", 0);
                }

                chunkdat.switchForRender = 0;
            }


            device.ImmediateContext.InputAssembler.SetVertexBuffers(10, new[]
            {
                    new VertexBufferBinding(chunkdat.InstanceBufferLocW, Marshal.SizeOf(typeof( sccs.SC_instancedChunk.DInstanceTypeLocW)),0),
                });

            device.ImmediateContext.InputAssembler.SetVertexBuffers(11, new[]
            {
                    new VertexBufferBinding(chunkdat.InstanceBufferLocH, Marshal.SizeOf(typeof( sccs.SC_instancedChunk.DInstanceTypeLocH)),0),
                });

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











            if (scgraphics.scgraphicssec.activatevrheightmapfeature == 1 && scgraphics.scgraphicssec.somevoxelvirtualdesktopglobals.tinyChunkWidth == 8)
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


                //TOREADD 
                //TOREADD 
                device.ImmediateContext.InputAssembler.SetVertexBuffers(6, new[]
                {
                    new VertexBufferBinding(chunkdat.instancesmatrixbuffer, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceMatrix)),0),
                });

                device.ImmediateContext.InputAssembler.SetVertexBuffers(7, new[]
                {
                    new VertexBufferBinding(chunkdat.instancesmatrixbufferb, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceMatrix)),0),
                });
                device.ImmediateContext.InputAssembler.SetVertexBuffers(8, new[]
               {
                    new VertexBufferBinding(chunkdat.instancesmatrixbufferc, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceMatrix)),0),
                });
                device.ImmediateContext.InputAssembler.SetVertexBuffers(9, new[]
               {
                    new VertexBufferBinding(chunkdat.instancesmatrixbufferd, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceMatrix)),0),
                });



            }






            if (scgraphics.scgraphicssec.activatevrheightmapfeature == 1 && scgraphics.scgraphicssec.somevoxelvirtualdesktopglobals.tinyChunkWidth == 4)
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
                device.ImmediateContext.MapSubresource(chunkdat.instanceBufferHeightmap, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                mappedResourceLight.WriteRange(chunkdat.heightmapmatrix, 0, chunkdat.heightmapmatrix.Length);
                device.ImmediateContext.UnmapSubresource(chunkdat.instanceBufferHeightmap, 0);
                mappedResourceLight.Dispose();
                //TOREADD 

       

            }

            device.ImmediateContext.PixelShader.SetShaderResource(0, shaderResourceView2D_);

            //device.ImmediateContext.DomainShader.SetConstantBuffer(0, chunkdat.constantMatrixPosBuffer);

            device.ImmediateContext.VertexShader.SetConstantBuffer(0, chunkdat.constantMatrixPosBuffer);
            //device.ImmediateContext.VertexShader.SetConstantBuffer(1, chunkdat.mapBuffer);
            device.ImmediateContext.VertexShader.SetConstantBuffer(2, chunkdat.someoculusdirbuffer);
            device.ImmediateContext.PixelShader.SetConstantBuffer(0, chunkdat.constantMatrixPosBuffer);
            device.ImmediateContext.PixelShader.SetConstantBuffer(1, chunkdat.constantLightBuffer);
            //device.ImmediateContext.PixelShader.SetConstantBuffer(2, chunkdat.someoculusdirbuffer);











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







  



            //device.ImmediateContext.Rasterizer.State = sc_console.SC_console_directx.D3D.RasterstateCullNone;
            device.ImmediateContext.InputAssembler.SetIndexBuffer(chunkdat.IndicesBuffer, SharpDX.DXGI.Format.R32_UInt, 0);
            device.ImmediateContext.InputAssembler.InputLayout = chunkdat.Layout;
           
            
            
            device.ImmediateContext.VertexShader.Set(chunkdat.VertexShader);
            //device.ImmediateContext.HullShader.Set(chunkdat.HullShader);
            //device.ImmediateContext.DomainShader.Set(chunkdat.DomainShader);
            device.ImmediateContext.PixelShader.Set(chunkdat.PixelShader);




            if (usegeometryshader == 0)
            {
                device.ImmediateContext.GeometryShader.Set(null); //GeometryShader
            }
            else if (usegeometryshader == 1)
            {
                device.ImmediateContext.GeometryShader.Set(chunkdat.GeometryShader); //GeometryShader
            }

            // Set the sampler state in the pixel shader.
            device.ImmediateContext.PixelShader.SetSampler(0, chunkdat.samplerState);
            //device.ImmediateContext.Rasterizer = 




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