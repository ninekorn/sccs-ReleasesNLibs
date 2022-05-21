using SharpDX.Direct3D11;
using SharpDX.WIC;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System;

using SharpDX;
using SharpDX.D3DCompiler;
using System.Runtime.InteropServices;
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

















namespace sccs
{
    public class sclevelgenchunkshader // : System.Windows.Forms.Application
    {
        public SharpDX.Direct3D11.Buffer constantBuffer;
        public SharpDX.Direct3D11.Device device;
        public SharpDX.Direct3D11.Buffer instanceBuffer;
        public SharpDX.Direct3D11.Buffer ConstantLightBuffer;
        
        InputLayout Layout;
        VertexShader VertexShader;
        PixelShader PixelShader;

        GeometryShader geoShader;

        //, SharpDX.Direct3D11.Buffer _instanceBuffer
        public sclevelgenchunkshader(SharpDX.Direct3D11.Device _device, SharpDX.Direct3D11.Buffer _constantBuffer, InputLayout _Layout, VertexShader _VertexShader, PixelShader _PixelShader,GeometryShader _geoShader, SharpDX.Direct3D11.Buffer _instanceBuffer, SharpDX.Direct3D11.Buffer _ConstantLightBuffer)
        {
            this.constantBuffer = _constantBuffer;
            this.device = _device;
            this.instanceBuffer = _instanceBuffer;
            this.PixelShader = _PixelShader;
            this.VertexShader = _VertexShader;
            this.Layout = _Layout;
            this.ConstantLightBuffer = _ConstantLightBuffer;
            this.geoShader = _geoShader;

            mapWidth = sclevelgenchunk.mapWidth;
            mapHeight = sclevelgenchunk.mapHeight;
            mapDepth = sclevelgenchunk.mapDepth;

            tinyChunkWidth = sclevelgenchunk.tinyChunkWidth;
            tinyChunkHeight = sclevelgenchunk.tinyChunkHeight;
            tinyChunkDepth = sclevelgenchunk.tinyChunkDepth;

            mapObjectInstanceWidth = sclevelgenchunk.mapObjectInstanceWidth;
            mapObjectInstanceHeight = sclevelgenchunk.mapObjectInstanceHeight;
            mapObjectInstanceDepth = sclevelgenchunk.mapObjectInstanceDepth;
            total = mapWidth * mapHeight * mapDepth;
        }

        public int mapWidth = 20;
        public int mapHeight = 1;
        public int mapDepth = 20;

        public int tinyChunkWidth = 20;
        public int tinyChunkHeight = 20;
        public int tinyChunkDepth = 20;

        public int mapObjectInstanceWidth = 1;
        public int mapObjectInstanceHeight = 1;
        public int mapObjectInstanceDepth = 1;

        /*public const int mapWidth = 30;
        public const int mapHeight = 1;
        public const int mapDepth = 30;

        public const int tinyChunkWidth = 20;
        public const int tinyChunkHeight = 20;
        public const int tinyChunkDepth = 20;


        public const int mapObjectInstanceWidth = 2;
        public const int mapObjectInstanceHeight = 1;
        public const int mapObjectInstanceDepth = 2;*/




        public int startOnce = 1;

        float planeSize = 0.1f;


        BufferDescription matrixBufferDescriptionTHREE;


        BufferDescription matrixBufferDescriptionVertex0;
        BufferDescription matrixBufferDescriptionVertex1;
        BufferDescription matrixBufferDescriptionVertex2;
        BufferDescription matrixBufferDescriptionVertex3;
        BufferDescription matrixBufferDescriptionVertex4;
        BufferDescription matrixBufferDescriptionVertex5;

        BufferDescription matrixBufferDescriptiontex0;
        BufferDescription matrixBufferDescriptiontex1;
        BufferDescription matrixBufferDescriptiontex2;
        BufferDescription matrixBufferDescriptiontex3;
        BufferDescription matrixBufferDescriptiontex4;
        BufferDescription matrixBufferDescriptiontex5;

        BufferDescription matrixBufferDescriptionnormal0;
        BufferDescription matrixBufferDescriptionnormal1;
        BufferDescription matrixBufferDescriptionnormal2;
        BufferDescription matrixBufferDescriptionnormal3;
        BufferDescription matrixBufferDescriptionnormal4;
        BufferDescription matrixBufferDescriptionnormal5;
        
                        




        SharpDX.Direct3D11.Buffer VertexBuffer0;
        SharpDX.Direct3D11.Buffer NormalBuffer0;
        SharpDX.Direct3D11.Buffer TextureBuffer0;
        SharpDX.Direct3D11.Buffer IndexBuffer0;

        SharpDX.Direct3D11.Buffer VertexBuffer1;
        SharpDX.Direct3D11.Buffer NormalBuffer1;
        SharpDX.Direct3D11.Buffer TextureBuffer1;
        SharpDX.Direct3D11.Buffer IndexBuffer1;

        SharpDX.Direct3D11.Buffer VertexBuffer2;
        SharpDX.Direct3D11.Buffer NormalBuffer2;
        SharpDX.Direct3D11.Buffer TextureBuffer2;
        SharpDX.Direct3D11.Buffer IndexBuffer2;

        SharpDX.Direct3D11.Buffer VertexBuffer3;
        SharpDX.Direct3D11.Buffer NormalBuffer3;
        SharpDX.Direct3D11.Buffer TextureBuffer3;
        SharpDX.Direct3D11.Buffer IndexBuffer3;

        SharpDX.Direct3D11.Buffer VertexBuffer4;
        SharpDX.Direct3D11.Buffer NormalBuffer4;
        SharpDX.Direct3D11.Buffer TextureBuffer4;
        SharpDX.Direct3D11.Buffer IndexBuffer4;

        SharpDX.Direct3D11.Buffer VertexBuffer5;
        SharpDX.Direct3D11.Buffer NormalBuffer5;
        SharpDX.Direct3D11.Buffer TextureBuffer5;
        SharpDX.Direct3D11.Buffer IndexBuffer5;







        SharpDX.Direct3D11.Buffer ColorBuffer;
        DataStream mappedResource0;
 

        DataStream mappedResource;
        DataStream streamerTWO;
        int total = 0;
        int xx = 0;
        int yy = 0;
        int zz = 0;

        int switchXX = 0;
        int switchYY = 0;
        int switchZZ = 0;


        public void Renderer(sccs.sclevelgenchunk.chunkData chunkdat, int typeofface) //async
        {
            try
            {
                //timeCalculator.Stop();
                //timeCalculator.Reset();
                //timeCalculator.Start();

                device.ImmediateContext.MapSubresource(constantBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streamerTWO);
                streamerTWO.WriteRange(chunkdat.matrixBuffer, 0, chunkdat.matrixBuffer.Length);
                device.ImmediateContext.UnmapSubresource(constantBuffer, 0);
                streamerTWO.Dispose();







                DataStream mappedResourceLight;
                device.ImmediateContext.MapSubresource(ConstantLightBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);

                mappedResourceLight.WriteRange(chunkdat.lightBuffer, 0, chunkdat.lightBuffer.Length);
                //mappedResourceLight.Write(chunkdat.lightBuffer);
                device.ImmediateContext.UnmapSubresource(ConstantLightBuffer, 0);
                device.ImmediateContext.PixelShader.SetConstantBuffer(0, ConstantLightBuffer);



























                /*BufferDescription instanceBuffDesc = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(SC_VR_Chunk.DInstanceType)) * chunkdat.arrayOfInstancePos.Length, // * chunkdat.arrayOfInstancePos.Length
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                instanceBuffer = new SharpDX.Direct3D11.Buffer(device, instanceBuffDesc);
                */
                device.ImmediateContext.MapSubresource(instanceBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                //mappedResource.WriteRange(chunkdat.arrayOfInstancePos, 0, chunkdat.arrayOfInstancePos.Length); // (int)memoryStream.Length
                //mappedResource.WriteRange(chunkdat.arrayOfInstancePos, 0, chunkdat.arrayOfInstancePos.Length);
                mappedResource.WriteRange(chunkdat.arrayOfInstancePos, 0, chunkdat.arrayOfInstancePos.Length);

                device.ImmediateContext.UnmapSubresource(instanceBuffer, 0);
                mappedResource.Dispose();


                /*for (int x = 0; x < mapWidth; x++)
                {
                    for (int y = 0; y < mapHeight; y++)
                    {
                        for (int z = 0; z < mapDepth; z++)
                        {
                            var xx = x;
                            var yy = y;//  (mapHeight - 1) - y;
                            var zz = z;


                            Vector3 position = new Vector3(x, y, z);

                            position.X *= ((tinyChunkWidth * _planeSize));
                            position.Y *= ((tinyChunkHeight * _planeSize));
                            position.Z *= ((tinyChunkDepth * _planeSize));

                            //position.X = position.X + (_chunkPos.X); //*1.05f
                            //position.Y = position.Y + (_chunkPos.Y);
                            //position.Z = position.Z + (_chunkPos.Z);

                            instances[xx + mapWidth * (yy + mapHeight * zz)] = new SC_DirectX.DInstanceType()
                            {
                                position = position,
                            };
                        }
                    }
                }*/

                //device.ImmediateContext.MapSubresource(instanceBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                //mappedResource.WriteRange(chunkdat.arrayOfInstancePos, 0, chunkdat.arrayOfInstancePos.Length); // (int)memoryStream.Length
                //device.ImmediateContext.UnmapSubresource(instanceBuffer, 0);
                //mappedResource.Dispose();


                //queueOfFunctions

                //Func<int> formatDelegate = () =>
                //{
                total = mapWidth * mapHeight * mapDepth;
                xx = 0;
                yy = 0;
                zz = 0;

                switchXX = 0;
                switchYY = 0;
                switchZZ = 0;

                device.ImmediateContext.VertexShader.SetConstantBuffer(0, constantBuffer);
                device.ImmediateContext.GeometryShader.SetConstantBuffer(0, constantBuffer);

                device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                device.ImmediateContext.InputAssembler.InputLayout = Layout;
                device.ImmediateContext.VertexShader.Set(VertexShader);
                device.ImmediateContext.PixelShader.Set(PixelShader);
                device.ImmediateContext.GeometryShader.Set(null); //geoShader


                device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
                {
                    new VertexBufferBinding(instanceBuffer, Utilities.SizeOf<sclevelgenchunk.DInstanceType>(),0), //chunkdat.instanceBuffers[xx+mapWidth*(yy+mapHeight*zz)]
                });


                //device.ImmediateContext.MapSubresource(instanceBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                //mappedResource.WriteRange(chunkdat.arrayOfInstancePos, 0, chunkdat.arrayOfInstancePos.Length); // (int)memoryStream.Length
                //device.ImmediateContext.UnmapSubresource(instanceBuffer, 0);
                //mappedResource.Dispose();
                /*device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
                {
                    new VertexBufferBinding(instanceBuffer, Utilities.SizeOf<SC_VR_Chunk.DInstanceType>(),0),
                });*/


                //instanceBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer,chunkdat.arrayOfInstancePos[xx + mapWidth * (yy + mapHeight * zz)]);

                /*device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
                {
                    new VertexBufferBinding(chunkdat.colorBuffers[xx+mapWidth*(yy+mapHeight*zz)], Marshal.SizeOf(typeof(Vector4)), 0),
                });*/

                //Console.WriteLine("write to buffer0");

                for (int t = 0; t < total; t++)
                {




                    if (chunkdat.startOnce == 1)
                    {

                        if (typeofface == 0)
                        {
                            if (chunkdat.arrayOfInstanceVertex0[xx + mapWidth * (yy + mapHeight * zz)].Length > 0)
                            {
                                //MainWindow.MessageBox((IntPtr)0, "test2", "scmsg", 0);

                                matrixBufferDescriptionVertex0 = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(sclevelgenchunk.DVertex)) * chunkdat.dVertexData0[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                    BindFlags = BindFlags.VertexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };
                                VertexBuffer0 = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionVertex0);

                                //VertexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)]);

                                device.ImmediateContext.MapSubresource(VertexBuffer0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.dVertexData0[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.dVertexData0[xx + mapWidth * (yy + mapHeight * zz)].Length);
                                //mappedResource0.Write(chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)]);
                                device.ImmediateContext.UnmapSubresource(VertexBuffer0, 0);
                                mappedResource0.Dispose();


                                matrixBufferDescriptionnormal0 = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(Vector4)) * chunkdat.arrayOfInstanceNormals0[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                    BindFlags = BindFlags.VertexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };
                                NormalBuffer0 = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionnormal0);


                                device.ImmediateContext.MapSubresource(NormalBuffer0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.arrayOfInstanceNormals0[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceNormals0[xx + mapWidth * (yy + mapHeight * zz)].Length);
                                device.ImmediateContext.UnmapSubresource(NormalBuffer0, 0);
                                mappedResource0.Dispose();




                                matrixBufferDescriptiontex0 = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(Vector4)) * chunkdat.arrayOfInstanceTextureCoordinates0[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                    BindFlags = BindFlags.VertexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };
                                TextureBuffer0 = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptiontex0);


                                device.ImmediateContext.MapSubresource(TextureBuffer0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.arrayOfInstanceTextureCoordinates0[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceTextureCoordinates0[xx + mapWidth * (yy + mapHeight * zz)].Length);
                                device.ImmediateContext.UnmapSubresource(TextureBuffer0, 0);
                                mappedResource0.Dispose();

                                IndexBuffer0 = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, chunkdat.arrayOfInstanceIndices0[xx + mapWidth * (yy + mapHeight * zz)]);





                                chunkdat.dVertBuffers0[xx + mapWidth * (yy + mapHeight * zz)] = VertexBuffer0;

                                chunkdat.indexBuffers0[xx + mapWidth * (yy + mapHeight * zz)] = IndexBuffer0;



                                //chunkdat.colorBuffers[xx + mapWidth * (yy + mapHeight * zz)] = ColorBuffer;

                                chunkdat.normalBuffers0[xx + mapWidth * (yy + mapHeight * zz)] = NormalBuffer0;
                                chunkdat.texBuffers0[xx + mapWidth * (yy + mapHeight * zz)] = TextureBuffer0;



                                //chunkdat.instanceBuffers[xx + mapWidth * (yy + mapHeight * zz)] = instanceBuff;

                                //ColorBuffer.Dispose();
                                //IndexBuffer.Dispose();
                            }

                            device.ImmediateContext.InputAssembler.SetIndexBuffer(chunkdat.indexBuffers0[xx + mapWidth * (yy + mapHeight * zz)], SharpDX.DXGI.Format.R32_UInt, 0);
                            //var vertexBufferBinding = new VertexBufferBinding(chunkdat.dVertBuffers[xx + mapWidth * (yy + mapHeight * zz)], Utilities.SizeOf<SC_VR_Chunk.DVertex>(), 0);
                            //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, vertexBufferBinding);
                            device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
                            {
                            new VertexBufferBinding(chunkdat.dVertBuffers0[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(sclevelgenchunk.DVertex)), 0),
                        });








                            /*device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
                            {
                                new VertexBufferBinding(chunkdat.normalBuffers[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(Vector3)), 0),
                            });


                            device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
                            {
                                new VertexBufferBinding(chunkdat.texBuffers[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(Vector2)), 0),
                            });*/



                            //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(chunkdat.dVertBuffers[xx + mapWidth * (yy + mapHeight * zz)], Utilities.SizeOf<SC_VR_Chunk.DVertex>(), 0));

                            /*device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
                            {
                                new VertexBufferBinding(chunkdat.colorBuffers[xx+mapWidth*(yy+mapHeight*zz)], Marshal.SizeOf(typeof(Vector4)), 0),
                            });*/














                            device.ImmediateContext.DrawIndexedInstanced(chunkdat.arrayOfInstanceIndices0[xx + mapWidth * (yy + mapHeight * zz)].Length, 1, 0, 0, xx + mapWidth * (yy + mapHeight * zz));
                            //device.ImmediateContext.DrawIndexed(chunkdat.arrayOfInstanceIndices[xx + mapWidth * (yy + mapHeight * zz)].Length, 0, 0);

                        }
                        else if (typeofface == 1)
                        {
                            if (chunkdat.arrayOfInstanceVertex1[xx + mapWidth * (yy + mapHeight * zz)].Length > 0)
                            {
                                //MainWindow.MessageBox((IntPtr)0, "test2", "scmsg", 0);

                                matrixBufferDescriptionVertex1 = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(sclevelgenchunk.DVertex)) * chunkdat.dVertexData1[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                    BindFlags = BindFlags.VertexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };
                                VertexBuffer1 = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionVertex1);

                                //VertexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)]);

                                device.ImmediateContext.MapSubresource(VertexBuffer1, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.dVertexData1[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.dVertexData1[xx + mapWidth * (yy + mapHeight * zz)].Length);
                                //mappedResource0.Write(chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)]);
                                device.ImmediateContext.UnmapSubresource(VertexBuffer1, 0);
                                mappedResource0.Dispose();


                                matrixBufferDescriptionnormal1 = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(Vector4)) * chunkdat.arrayOfInstanceNormals1[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                    BindFlags = BindFlags.VertexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };
                                NormalBuffer1 = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionnormal1);


                                device.ImmediateContext.MapSubresource(NormalBuffer1, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.arrayOfInstanceNormals1[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceNormals1[xx + mapWidth * (yy + mapHeight * zz)].Length);
                                device.ImmediateContext.UnmapSubresource(NormalBuffer1, 0);
                                mappedResource0.Dispose();




                                matrixBufferDescriptiontex1 = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(Vector4)) * chunkdat.arrayOfInstanceTextureCoordinates1[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                    BindFlags = BindFlags.VertexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };
                                TextureBuffer1 = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptiontex1);


                                device.ImmediateContext.MapSubresource(TextureBuffer1, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.arrayOfInstanceTextureCoordinates1[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceTextureCoordinates1[xx + mapWidth * (yy + mapHeight * zz)].Length);
                                device.ImmediateContext.UnmapSubresource(TextureBuffer1, 0);
                                mappedResource0.Dispose();

                                IndexBuffer1 = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, chunkdat.arrayOfInstanceIndices1[xx + mapWidth * (yy + mapHeight * zz)]);





                                chunkdat.dVertBuffers1[xx + mapWidth * (yy + mapHeight * zz)] = VertexBuffer1;

                                chunkdat.indexBuffers1[xx + mapWidth * (yy + mapHeight * zz)] = IndexBuffer1;



                                //chunkdat.colorBuffers[xx + mapWidth * (yy + mapHeight * zz)] = ColorBuffer;

                                chunkdat.normalBuffers1[xx + mapWidth * (yy + mapHeight * zz)] = NormalBuffer1;
                                chunkdat.texBuffers1[xx + mapWidth * (yy + mapHeight * zz)] = TextureBuffer1;



                                //chunkdat.instanceBuffers[xx + mapWidth * (yy + mapHeight * zz)] = instanceBuff;

                                //ColorBuffer.Dispose();
                                //IndexBuffer.Dispose();
                            }

                            device.ImmediateContext.InputAssembler.SetIndexBuffer(chunkdat.indexBuffers1[xx + mapWidth * (yy + mapHeight * zz)], SharpDX.DXGI.Format.R32_UInt, 0);
                            //var vertexBufferBinding = new VertexBufferBinding(chunkdat.dVertBuffers[xx + mapWidth * (yy + mapHeight * zz)], Utilities.SizeOf<SC_VR_Chunk.DVertex>(), 0);
                            //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, vertexBufferBinding);
                            device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
                            {
                            new VertexBufferBinding(chunkdat.dVertBuffers1[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(sclevelgenchunk.DVertex)), 0),
                        });








                            /*device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
                            {
                                new VertexBufferBinding(chunkdat.normalBuffers[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(Vector3)), 0),
                            });


                            device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
                            {
                                new VertexBufferBinding(chunkdat.texBuffers[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(Vector2)), 0),
                            });*/



                            //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(chunkdat.dVertBuffers[xx + mapWidth * (yy + mapHeight * zz)], Utilities.SizeOf<SC_VR_Chunk.DVertex>(), 0));

                            /*device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
                            {
                                new VertexBufferBinding(chunkdat.colorBuffers[xx+mapWidth*(yy+mapHeight*zz)], Marshal.SizeOf(typeof(Vector4)), 0),
                            });*/














                            device.ImmediateContext.DrawIndexedInstanced(chunkdat.arrayOfInstanceIndices1[xx + mapWidth * (yy + mapHeight * zz)].Length, 1, 0, 0, xx + mapWidth * (yy + mapHeight * zz));
                            //device.ImmediateContext.DrawIndexed(chunkdat.arrayOfInstanceIndices[xx + mapWidth * (yy + mapHeight * zz)].Length, 0, 0);

                        }
                        else if (typeofface == 2)
                        {
                            if (chunkdat.arrayOfInstanceVertex2[xx + mapWidth * (yy + mapHeight * zz)].Length > 0)
                            {
                                //MainWindow.MessageBox((IntPtr)0, "test2", "scmsg", 0);

                                matrixBufferDescriptionVertex2 = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(sclevelgenchunk.DVertex)) * chunkdat.dVertexData2[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                    BindFlags = BindFlags.VertexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };
                                VertexBuffer2 = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionVertex2);

                                //VertexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)]);

                                device.ImmediateContext.MapSubresource(VertexBuffer2, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.dVertexData2[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.dVertexData2[xx + mapWidth * (yy + mapHeight * zz)].Length);
                                //mappedResource0.Write(chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)]);
                                device.ImmediateContext.UnmapSubresource(VertexBuffer2, 0);
                                mappedResource0.Dispose();


                                matrixBufferDescriptionnormal2 = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(Vector4)) * chunkdat.arrayOfInstanceNormals2[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                    BindFlags = BindFlags.VertexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };
                                NormalBuffer2 = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionnormal2);


                                device.ImmediateContext.MapSubresource(NormalBuffer2, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.arrayOfInstanceNormals2[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceNormals2[xx + mapWidth * (yy + mapHeight * zz)].Length);
                                device.ImmediateContext.UnmapSubresource(NormalBuffer2, 0);
                                mappedResource0.Dispose();




                                matrixBufferDescriptiontex2 = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(Vector4)) * chunkdat.arrayOfInstanceTextureCoordinates2[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                    BindFlags = BindFlags.VertexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };
                                TextureBuffer2 = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptiontex2);


                                device.ImmediateContext.MapSubresource(TextureBuffer2, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.arrayOfInstanceTextureCoordinates2[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceTextureCoordinates2[xx + mapWidth * (yy + mapHeight * zz)].Length);
                                device.ImmediateContext.UnmapSubresource(TextureBuffer2, 0);
                                mappedResource0.Dispose();

                                IndexBuffer2 = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, chunkdat.arrayOfInstanceIndices2[xx + mapWidth * (yy + mapHeight * zz)]);





                                chunkdat.dVertBuffers2[xx + mapWidth * (yy + mapHeight * zz)] = VertexBuffer2;

                                chunkdat.indexBuffers2[xx + mapWidth * (yy + mapHeight * zz)] = IndexBuffer2;



                                //chunkdat.colorBuffers[xx + mapWidth * (yy + mapHeight * zz)] = ColorBuffer;

                                chunkdat.normalBuffers2[xx + mapWidth * (yy + mapHeight * zz)] = NormalBuffer2;
                                chunkdat.texBuffers2[xx + mapWidth * (yy + mapHeight * zz)] = TextureBuffer2;



                                //chunkdat.instanceBuffers[xx + mapWidth * (yy + mapHeight * zz)] = instanceBuff;

                                //ColorBuffer.Dispose();
                                //IndexBuffer.Dispose();
                            }

                            device.ImmediateContext.InputAssembler.SetIndexBuffer(chunkdat.indexBuffers2[xx + mapWidth * (yy + mapHeight * zz)], SharpDX.DXGI.Format.R32_UInt, 0);
                            //var vertexBufferBinding = new VertexBufferBinding(chunkdat.dVertBuffers[xx + mapWidth * (yy + mapHeight * zz)], Utilities.SizeOf<SC_VR_Chunk.DVertex>(), 0);
                            //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, vertexBufferBinding);
                            device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
                            {
                            new VertexBufferBinding(chunkdat.dVertBuffers2[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(sclevelgenchunk.DVertex)), 0),
                        });








                            /*device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
                            {
                                new VertexBufferBinding(chunkdat.normalBuffers[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(Vector3)), 0),
                            });


                            device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
                            {
                                new VertexBufferBinding(chunkdat.texBuffers[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(Vector2)), 0),
                            });*/



                            //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(chunkdat.dVertBuffers[xx + mapWidth * (yy + mapHeight * zz)], Utilities.SizeOf<SC_VR_Chunk.DVertex>(), 0));

                            /*device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
                            {
                                new VertexBufferBinding(chunkdat.colorBuffers[xx+mapWidth*(yy+mapHeight*zz)], Marshal.SizeOf(typeof(Vector4)), 0),
                            });*/














                            device.ImmediateContext.DrawIndexedInstanced(chunkdat.arrayOfInstanceIndices2[xx + mapWidth * (yy + mapHeight * zz)].Length, 1, 0, 0, xx + mapWidth * (yy + mapHeight * zz));
                            //device.ImmediateContext.DrawIndexed(chunkdat.arrayOfInstanceIndices[xx + mapWidth * (yy + mapHeight * zz)].Length, 0, 0);

                        }
                        else if (typeofface == 3)
                        {
                            if (chunkdat.arrayOfInstanceVertex3[xx + mapWidth * (yy + mapHeight * zz)].Length > 0)
                            {
                                //MainWindow.MessageBox((IntPtr)0, "test2", "scmsg", 0);

                                matrixBufferDescriptionVertex3 = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(sclevelgenchunk.DVertex)) * chunkdat.dVertexData3[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                    BindFlags = BindFlags.VertexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };
                                VertexBuffer3 = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionVertex3);

                                //VertexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)]);

                                device.ImmediateContext.MapSubresource(VertexBuffer3, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.dVertexData3[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.dVertexData3[xx + mapWidth * (yy + mapHeight * zz)].Length);
                                //mappedResource0.Write(chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)]);
                                device.ImmediateContext.UnmapSubresource(VertexBuffer3, 0);
                                mappedResource0.Dispose();


                                matrixBufferDescriptionnormal3 = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(Vector4)) * chunkdat.arrayOfInstanceNormals3[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                    BindFlags = BindFlags.VertexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };
                                NormalBuffer3 = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionnormal3);


                                device.ImmediateContext.MapSubresource(NormalBuffer3, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.arrayOfInstanceNormals3[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceNormals3[xx + mapWidth * (yy + mapHeight * zz)].Length);
                                device.ImmediateContext.UnmapSubresource(NormalBuffer3, 0);
                                mappedResource0.Dispose();




                                matrixBufferDescriptiontex3 = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(Vector4)) * chunkdat.arrayOfInstanceTextureCoordinates3[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                    BindFlags = BindFlags.VertexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };
                                TextureBuffer3 = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptiontex3);


                                device.ImmediateContext.MapSubresource(TextureBuffer3, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.arrayOfInstanceTextureCoordinates3[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceTextureCoordinates3[xx + mapWidth * (yy + mapHeight * zz)].Length);
                                device.ImmediateContext.UnmapSubresource(TextureBuffer3, 0);
                                mappedResource0.Dispose();

                                IndexBuffer3 = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, chunkdat.arrayOfInstanceIndices3[xx + mapWidth * (yy + mapHeight * zz)]);





                                chunkdat.dVertBuffers3[xx + mapWidth * (yy + mapHeight * zz)] = VertexBuffer3;

                                chunkdat.indexBuffers3[xx + mapWidth * (yy + mapHeight * zz)] = IndexBuffer3;



                                //chunkdat.colorBuffers[xx + mapWidth * (yy + mapHeight * zz)] = ColorBuffer;

                                chunkdat.normalBuffers3[xx + mapWidth * (yy + mapHeight * zz)] = NormalBuffer3;
                                chunkdat.texBuffers3[xx + mapWidth * (yy + mapHeight * zz)] = TextureBuffer3;



                                //chunkdat.instanceBuffers[xx + mapWidth * (yy + mapHeight * zz)] = instanceBuff;

                                //ColorBuffer.Dispose();
                                //IndexBuffer.Dispose();
                            }

                            device.ImmediateContext.InputAssembler.SetIndexBuffer(chunkdat.indexBuffers3[xx + mapWidth * (yy + mapHeight * zz)], SharpDX.DXGI.Format.R32_UInt, 0);
                            //var vertexBufferBinding = new VertexBufferBinding(chunkdat.dVertBuffers[xx + mapWidth * (yy + mapHeight * zz)], Utilities.SizeOf<SC_VR_Chunk.DVertex>(), 0);
                            //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, vertexBufferBinding);
                            device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
                            {
                            new VertexBufferBinding(chunkdat.dVertBuffers3[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(sclevelgenchunk.DVertex)), 0),
                        });








                            /*device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
                            {
                                new VertexBufferBinding(chunkdat.normalBuffers[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(Vector3)), 0),
                            });


                            device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
                            {
                                new VertexBufferBinding(chunkdat.texBuffers[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(Vector2)), 0),
                            });*/



                            //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(chunkdat.dVertBuffers[xx + mapWidth * (yy + mapHeight * zz)], Utilities.SizeOf<SC_VR_Chunk.DVertex>(), 0));

                            /*device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
                            {
                                new VertexBufferBinding(chunkdat.colorBuffers[xx+mapWidth*(yy+mapHeight*zz)], Marshal.SizeOf(typeof(Vector4)), 0),
                            });*/














                            device.ImmediateContext.DrawIndexedInstanced(chunkdat.arrayOfInstanceIndices3[xx + mapWidth * (yy + mapHeight * zz)].Length, 1, 0, 0, xx + mapWidth * (yy + mapHeight * zz));
                            //device.ImmediateContext.DrawIndexed(chunkdat.arrayOfInstanceIndices[xx + mapWidth * (yy + mapHeight * zz)].Length, 0, 0);

                        }
                        else if (typeofface == 4)
                        {
                            if (chunkdat.arrayOfInstanceVertex4[xx + mapWidth * (yy + mapHeight * zz)].Length > 0)
                            {
                                //MainWindow.MessageBox((IntPtr)0, "test2", "scmsg", 0);

                                matrixBufferDescriptionVertex4 = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(sclevelgenchunk.DVertex)) * chunkdat.dVertexData4[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                    BindFlags = BindFlags.VertexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };
                                VertexBuffer4 = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionVertex4);

                                //VertexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)]);

                                device.ImmediateContext.MapSubresource(VertexBuffer4, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.dVertexData4[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.dVertexData4[xx + mapWidth * (yy + mapHeight * zz)].Length);
                                //mappedResource0.Write(chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)]);
                                device.ImmediateContext.UnmapSubresource(VertexBuffer4, 0);
                                mappedResource0.Dispose();


                                matrixBufferDescriptionnormal4 = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(Vector4)) * chunkdat.arrayOfInstanceNormals4[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                    BindFlags = BindFlags.VertexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };
                                NormalBuffer4 = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionnormal4);


                                device.ImmediateContext.MapSubresource(NormalBuffer4, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.arrayOfInstanceNormals4[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceNormals4[xx + mapWidth * (yy + mapHeight * zz)].Length);
                                device.ImmediateContext.UnmapSubresource(NormalBuffer4, 0);
                                mappedResource0.Dispose();




                                matrixBufferDescriptiontex4 = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(Vector4)) * chunkdat.arrayOfInstanceTextureCoordinates4[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                    BindFlags = BindFlags.VertexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };
                                TextureBuffer4 = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptiontex4);


                                device.ImmediateContext.MapSubresource(TextureBuffer4, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.arrayOfInstanceTextureCoordinates4[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceTextureCoordinates4[xx + mapWidth * (yy + mapHeight * zz)].Length);
                                device.ImmediateContext.UnmapSubresource(TextureBuffer4, 0);
                                mappedResource0.Dispose();

                                IndexBuffer4 = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, chunkdat.arrayOfInstanceIndices4[xx + mapWidth * (yy + mapHeight * zz)]);





                                chunkdat.dVertBuffers4[xx + mapWidth * (yy + mapHeight * zz)] = VertexBuffer4;

                                chunkdat.indexBuffers4[xx + mapWidth * (yy + mapHeight * zz)] = IndexBuffer4;



                                //chunkdat.colorBuffers[xx + mapWidth * (yy + mapHeight * zz)] = ColorBuffer;

                                chunkdat.normalBuffers4[xx + mapWidth * (yy + mapHeight * zz)] = NormalBuffer4;
                                chunkdat.texBuffers4[xx + mapWidth * (yy + mapHeight * zz)] = TextureBuffer4;



                                //chunkdat.instanceBuffers[xx + mapWidth * (yy + mapHeight * zz)] = instanceBuff;

                                //ColorBuffer.Dispose();
                                //IndexBuffer.Dispose();
                            }

                            device.ImmediateContext.InputAssembler.SetIndexBuffer(chunkdat.indexBuffers4[xx + mapWidth * (yy + mapHeight * zz)], SharpDX.DXGI.Format.R32_UInt, 0);
                            //var vertexBufferBinding = new VertexBufferBinding(chunkdat.dVertBuffers[xx + mapWidth * (yy + mapHeight * zz)], Utilities.SizeOf<SC_VR_Chunk.DVertex>(), 0);
                            //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, vertexBufferBinding);
                            device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
                            {
                            new VertexBufferBinding(chunkdat.dVertBuffers4[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(sclevelgenchunk.DVertex)), 0),
                        });








                            /*device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
                            {
                                new VertexBufferBinding(chunkdat.normalBuffers[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(Vector3)), 0),
                            });


                            device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
                            {
                                new VertexBufferBinding(chunkdat.texBuffers[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(Vector2)), 0),
                            });*/



                            //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(chunkdat.dVertBuffers[xx + mapWidth * (yy + mapHeight * zz)], Utilities.SizeOf<SC_VR_Chunk.DVertex>(), 0));

                            /*device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
                            {
                                new VertexBufferBinding(chunkdat.colorBuffers[xx+mapWidth*(yy+mapHeight*zz)], Marshal.SizeOf(typeof(Vector4)), 0),
                            });*/














                            device.ImmediateContext.DrawIndexedInstanced(chunkdat.arrayOfInstanceIndices4[xx + mapWidth * (yy + mapHeight * zz)].Length, 1, 0, 0, xx + mapWidth * (yy + mapHeight * zz));
                            //device.ImmediateContext.DrawIndexed(chunkdat.arrayOfInstanceIndices[xx + mapWidth * (yy + mapHeight * zz)].Length, 0, 0);

                        }
                        else if (typeofface == 5)
                        {
                            if (chunkdat.arrayOfInstanceVertex5[xx + mapWidth * (yy + mapHeight * zz)].Length > 0)
                            {
                                //MainWindow.MessageBox((IntPtr)0, "test2", "scmsg", 0);

                                matrixBufferDescriptionVertex5 = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(sclevelgenchunk.DVertex)) * chunkdat.dVertexData5[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                    BindFlags = BindFlags.VertexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };
                                VertexBuffer5 = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionVertex5);

                                //VertexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)]);

                                device.ImmediateContext.MapSubresource(VertexBuffer5, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.dVertexData5[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.dVertexData5[xx + mapWidth * (yy + mapHeight * zz)].Length);
                                //mappedResource0.Write(chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)]);
                                device.ImmediateContext.UnmapSubresource(VertexBuffer5, 0);
                                mappedResource0.Dispose();


                                matrixBufferDescriptionnormal5 = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(Vector4)) * chunkdat.arrayOfInstanceNormals5[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                    BindFlags = BindFlags.VertexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };
                                NormalBuffer5 = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionnormal5);


                                device.ImmediateContext.MapSubresource(NormalBuffer5, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.arrayOfInstanceNormals5[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceNormals5[xx + mapWidth * (yy + mapHeight * zz)].Length);
                                device.ImmediateContext.UnmapSubresource(NormalBuffer5, 0);
                                mappedResource0.Dispose();




                                matrixBufferDescriptiontex5 = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(Vector4)) * chunkdat.arrayOfInstanceTextureCoordinates5[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                    BindFlags = BindFlags.VertexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };
                                TextureBuffer5 = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptiontex5);


                                device.ImmediateContext.MapSubresource(TextureBuffer5, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.arrayOfInstanceTextureCoordinates5[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceTextureCoordinates5[xx + mapWidth * (yy + mapHeight * zz)].Length);
                                device.ImmediateContext.UnmapSubresource(TextureBuffer5, 0);
                                mappedResource0.Dispose();

                                IndexBuffer5 = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, chunkdat.arrayOfInstanceIndices5[xx + mapWidth * (yy + mapHeight * zz)]);





                                chunkdat.dVertBuffers5[xx + mapWidth * (yy + mapHeight * zz)] = VertexBuffer5;

                                chunkdat.indexBuffers5[xx + mapWidth * (yy + mapHeight * zz)] = IndexBuffer5;



                                //chunkdat.colorBuffers[xx + mapWidth * (yy + mapHeight * zz)] = ColorBuffer;

                                chunkdat.normalBuffers5[xx + mapWidth * (yy + mapHeight * zz)] = NormalBuffer5;
                                chunkdat.texBuffers5[xx + mapWidth * (yy + mapHeight * zz)] = TextureBuffer5;



                                //chunkdat.instanceBuffers[xx + mapWidth * (yy + mapHeight * zz)] = instanceBuff;

                                //ColorBuffer.Dispose();
                                //IndexBuffer.Dispose();
                            }

                            device.ImmediateContext.InputAssembler.SetIndexBuffer(chunkdat.indexBuffers5[xx + mapWidth * (yy + mapHeight * zz)], SharpDX.DXGI.Format.R32_UInt, 0);
                            //var vertexBufferBinding = new VertexBufferBinding(chunkdat.dVertBuffers[xx + mapWidth * (yy + mapHeight * zz)], Utilities.SizeOf<SC_VR_Chunk.DVertex>(), 0);
                            //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, vertexBufferBinding);
                            device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
                            {
                            new VertexBufferBinding(chunkdat.dVertBuffers5[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(sclevelgenchunk.DVertex)), 0),
                        });








                            /*device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
                            {
                                new VertexBufferBinding(chunkdat.normalBuffers[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(Vector3)), 0),
                            });


                            device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
                            {
                                new VertexBufferBinding(chunkdat.texBuffers[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(Vector2)), 0),
                            });*/



                            //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(chunkdat.dVertBuffers[xx + mapWidth * (yy + mapHeight * zz)], Utilities.SizeOf<SC_VR_Chunk.DVertex>(), 0));

                            /*device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
                            {
                                new VertexBufferBinding(chunkdat.colorBuffers[xx+mapWidth*(yy+mapHeight*zz)], Marshal.SizeOf(typeof(Vector4)), 0),
                            });*/














                            device.ImmediateContext.DrawIndexedInstanced(chunkdat.arrayOfInstanceIndices5[xx + mapWidth * (yy + mapHeight * zz)].Length, 1, 0, 0, xx + mapWidth * (yy + mapHeight * zz));
                            //device.ImmediateContext.DrawIndexed(chunkdat.arrayOfInstanceIndices[xx + mapWidth * (yy + mapHeight * zz)].Length, 0, 0);

                        }

                    }




























                    zz++;
                    if (zz >= mapDepth)
                    {
                        yy++;
                        zz = 0;
                        switchYY = 1;
                    }
                    if (yy >= mapHeight && switchYY == 1)
                    {
                        xx++;
                        yy = 0;
                        switchYY = 0;
                        switchXX = 1;
                    }
                    if (xx >= mapWidth && switchXX == 1)
                    {
                        xx = 0;
                        switchXX = 0;
                        break;
                    }
                }


                chunkdat.startOnce = 0;



                //Console.WriteLine("write to buffer");

                //   return 1;
                //};
                //Console.WriteLine("write to buffer");
                //await Task.Delay(1);


                //SC_DirectX.queueOfFunctions.Add(formatDelegate);
                //SC_DirectX.queueOfFunctions.Enqueue(formatDelegate);

                //var t2 = new Task<int>(formatDelegate);
                //t2.RunSynchronously();
                //t2.Dispose();
                //SC_DirectX.queueOfFunctions.Add(formatDelegate);
                //await Task.Delay(1);
                //SC_DirectX.queueOfFunctions.Enqueue(formatDelegate);
                /*var test = SC_DirectX.MainControl;

                if (test.InvokeRequired== true)
                {
                    Console.WriteLine("required");
                }
                else
                {
                    Console.WriteLine("!required");

                }*/


                //var t2 = new Task<bool>(formatDelegate);
                ///t2.RunSynchronously();


                //SC_DirectX.queueOfFunctions.Push(formatDelegate);

                /*Parallel.Invoke(() =>
                {

                });*/

                /*var refreshDXEngineAction = new Action(delegate
                {
                    //Console.WriteLine("test");
                    device.ImmediateContext.DrawIndexedInstanced(chunkdat.arrayOfInstanceIndices[xx + mapWidth * (yy + mapHeight * zz)].Length, 1, 0, 0, xx + mapWidth * (yy + mapHeight * zz));

                });

                sccs.Program.MainDispatch.BeginInvoke(System.Windows.Threading.DispatcherPriority.Render, refreshDXEngineAction);
                */

                //System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);


                //System.Windows.Forms.Control.

                /*var refreshDXEngineAction = new Action(delegate
                {
                    //Console.WriteLine("test");
                    SC_DirectX.queueOfFunctions.Push(t2);

                });

                sccs.Program.MainDispatch.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                */



                //t2.RunSynchronously();

                //await Task.Delay(1);
                /// Console.WriteLine("writeToBuffer");

                //timeCalculator.Stop();
                //Console.WriteLine(timeCalculator.Elapsed.Ticks);








                /*//if (startOnce == 1)
                {
                    for (int x = 0; x < mapWidth; x++)
                    {
                        for (int y = 0; y < mapHeight; y++)
                        {
                            for (int z = 0; z < mapDepth; z++)
                            {
                                var xx = x;
                                var yy = y;// (mapHeight - 1) - y;
                                var zz = z;

                                device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                                device.ImmediateContext.InputAssembler.InputLayout = Layout;
                                device.ImmediateContext.VertexShader.Set(VertexShader);
                                device.ImmediateContext.PixelShader.Set(PixelShader);
                                device.ImmediateContext.GeometryShader.Set(null);
                                device.ImmediateContext.VertexShader.SetConstantBuffer(0, constantBuffer);

                                if (chunkdat.arrayOfInstanceVertex[xx + mapWidth * (yy + mapHeight * zz)].Length > 0)
                                {
                                    if (startOnce == 1)
                                    {
                                        BufferDescription vertBufferDesc = new BufferDescription()
                                        {
                                            Usage = ResourceUsage.Dynamic,
                                            SizeInBytes = Marshal.SizeOf(typeof(Vector4)) * chunkdat.arrayOfInstanceVertex[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                            BindFlags = BindFlags.VertexBuffer,
                                            CpuAccessFlags = CpuAccessFlags.Write,
                                            OptionFlags = ResourceOptionFlags.None,
                                            StructureByteStride = 0
                                        };

                                        //var VertexBuffer = SharpDX.Direct3D11.Buffer.Create(_device, BindFlags.VertexBuffer, Vertices, Utilities.SizeOf<Vector4>() * Vertices.Length, ResourceUsage.Dynamic, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

                                        var VertexBuffer = new SharpDX.Direct3D11.Buffer(device, vertBufferDesc);

                                        var IndexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, chunkdat.arrayOfInstanceIndices[xx + mapWidth * (yy + mapHeight * zz)]);


                                        BufferDescription matrixBufferDescriptionTHREE = new BufferDescription()
                                        {
                                            Usage = ResourceUsage.Dynamic,
                                            SizeInBytes = Marshal.SizeOf(typeof(Vector4)) * chunkdat.arrayOfInstanceVertex[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                            BindFlags = BindFlags.VertexBuffer,
                                            CpuAccessFlags = CpuAccessFlags.Write,
                                            OptionFlags = ResourceOptionFlags.None,
                                            StructureByteStride = 0
                                        };

                                        var ColorBuffer = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionTHREE);

                                        DataStream mappedResource0;
                                        //ColorBuffer = SharpDX.Direct3D11.Buffer.Create(_device, BindFlags.VertexBuffer, someFuckingColorData, Marshal.SizeOf(typeof(someColorData)), ResourceUsage.Dynamic, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);
                                        device.ImmediateContext.MapSubresource(ColorBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                        //arrayOfSomeColors[i] = arrayOfInstanceVertex[i];
                                        mappedResource0.WriteRange(chunkdat.arrayOfInstanceVertex[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceVertex[xx + mapWidth * (yy + mapHeight * zz)].Length);
                                        //mappedResource0.Write(arrayOfSomeColors);
                                        device.ImmediateContext.UnmapSubresource(ColorBuffer, 0);
                                        mappedResource0.Dispose();
                                        chunkdat.colorBuffers[xx + mapWidth * (yy + mapHeight * zz)] = ColorBuffer;
                                        chunkdat.indexBuffers[xx + mapWidth * (yy + mapHeight * zz)] = IndexBuffer;
                                        chunkdat.vertBuffers[xx + mapWidth * (yy + mapHeight * zz)] = VertexBuffer;
                                    }
                                }
                                device.ImmediateContext.InputAssembler.SetIndexBuffer(chunkdat.indexBuffers[xx + mapWidth * (yy + mapHeight * zz)], SharpDX.DXGI.Format.R32_UInt, 0);

                                device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
                                {
                                     new VertexBufferBinding(chunkdat.vertBuffers[xx+mapWidth*(yy+mapHeight*zz)], Utilities.SizeOf<SC_VR_Chunk.DVertex>(), 0),
                                 });

                                device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
                                {
                                     new VertexBufferBinding(instanceBuffer, Utilities.SizeOf<SC_VR_Chunk.DInstanceType>(),0),
                                 });
                                device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
                                {
                                     new VertexBufferBinding(chunkdat.colorBuffers[xx+mapWidth*(yy+mapHeight*zz)], Utilities.SizeOf<Vector4>(), 0),
                                     //new VertexBufferBinding(ColorBuffer, Utilities.SizeOf<someColorData>(),0), //*instanceCount
                                 });

                                device.ImmediateContext.DrawIndexedInstanced(chunkdat.arrayOfInstanceIndices[xx + mapWidth * (yy + mapHeight * zz)].Length, 1, 0, 0, xx + mapWidth * (yy + mapHeight * zz));

                                //Console.WriteLine("x: " + xx + " y: " + yy + " z: " + zz);
                            }
                        }
                    }
                }


                startOnce = 0;*/






                //deviceContext.DrawInstanced(vertexCount, instanceCount,0,0);
                //deviceContext.Draw(4, 0);
                //deviceContext.DrawIndexed(indexCount, 0, 0);

                //return t2;
                //return true;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
                //return false;
            }
            //await Task.Delay(1);
            //Result tester = new Result();

            //return tester;
            //return null;

           // MainWindow.MessageBox((IntPtr)0, "test2", "scmsg", 0);
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
