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
    public class SC_VR_Chunk_Shader // : System.Windows.Forms.Application
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



        public SharpDX.Direct3D11.Buffer[] vertBuffers;
        public SharpDX.Direct3D11.Buffer[] colorBuffers;
        public SharpDX.Direct3D11.Buffer[] indexBuffers;
        public SharpDX.Direct3D11.Buffer[] instanceBuffers;
        public SharpDX.Direct3D11.Buffer[] normalBuffers;
        public SharpDX.Direct3D11.Buffer[] texBuffers;
        public SharpDX.Direct3D11.Buffer[] dVertBuffers;
        public SharpDX.Direct3D11.Buffer[] lightBuffers;


        public SharpDX.Direct3D11.Buffer[] constantBuffers;

        public SC_VR_Chunk_Shader(SharpDX.Direct3D11.Device _device, SharpDX.Direct3D11.Buffer _constantBuffer, InputLayout _Layout, VertexShader _VertexShader, PixelShader _PixelShader,GeometryShader _geoShader, SharpDX.Direct3D11.Buffer _instanceBuffer, SharpDX.Direct3D11.Buffer _ConstantLightBuffer)
        {
            this.constantBuffer = _constantBuffer;
            this.device = _device;
            this.instanceBuffer = _instanceBuffer;
            this.PixelShader = _PixelShader;
            this.VertexShader = _VertexShader;
            this.Layout = _Layout;
            this.ConstantLightBuffer = _ConstantLightBuffer;
            this.geoShader = _geoShader;

            mapWidth = SC_VR_Chunk.mapWidth;
            mapHeight = SC_VR_Chunk.mapHeight;
            mapDepth = SC_VR_Chunk.mapDepth;

            tinyChunkWidth = SC_VR_Chunk.tinyChunkWidth;
            tinyChunkHeight = SC_VR_Chunk.tinyChunkHeight;
            tinyChunkDepth = SC_VR_Chunk.tinyChunkDepth;

            mapObjectInstanceWidth = SC_VR_Chunk.mapObjectInstanceWidth;
            mapObjectInstanceHeight = SC_VR_Chunk.mapObjectInstanceHeight;
            mapObjectInstanceDepth = SC_VR_Chunk.mapObjectInstanceDepth;
            total = mapWidth * mapHeight * mapDepth;

            vertBuffers = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            colorBuffers = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            indexBuffers = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            instanceBuffers = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            normalBuffers = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            texBuffers = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            dVertBuffers = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];

            lightBuffers = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            constantBuffers = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];


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

        BufferDescription matrixBufferDescriptionVertex;




        SharpDX.Direct3D11.Buffer VertexBuffer;
        SharpDX.Direct3D11.Buffer NormalBuffer;
        SharpDX.Direct3D11.Buffer TextureBuffer;


        SharpDX.Direct3D11.Buffer ColorBuffer;
        DataStream mappedResource0;
        SharpDX.Direct3D11.Buffer IndexBuffer;

        DataStream mappedResource;
        DataStream streamerTWO;
        int total = 0;
        int xx = 0;
        int yy = 0;
        int zz = 0;

        int switchXX = 0;
        int switchYY = 0;
        int switchZZ = 0;
        DataStream mappedResourceLight;

        public void Renderer(sccs.SC_VR_Chunk.chunkData chunkdat) //async
        {
            try
            {
                //timeCalculator.Stop();
                //timeCalculator.Reset();
                //timeCalculator.Start();





























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
                    if (chunkdat.arrayOfInstanceVertex[xx + mapWidth * (yy + mapHeight * zz)].Length > 0)
                    {
                        if (startOnce == 1)
                        {
                            //MainWindow.MessageBox((IntPtr)0, "test2", "scmsg", 0);

                            matrixBufferDescriptionVertex = new BufferDescription()
                            {
                                Usage = ResourceUsage.Dynamic,
                                SizeInBytes = Marshal.SizeOf(typeof(SC_VR_Chunk.DVertex)) * chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                BindFlags = BindFlags.VertexBuffer,
                                CpuAccessFlags = CpuAccessFlags.Write,
                                OptionFlags = ResourceOptionFlags.None,
                                StructureByteStride = 0
                            };
                            /*VertexBuffer = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionVertex);
                            
                            device.ImmediateContext.MapSubresource(VertexBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                            mappedResource0.WriteRange(chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)].Length);
                            //mappedResource0.Write(chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)]);
                            device.ImmediateContext.UnmapSubresource(VertexBuffer, 0);
                            mappedResource0.Dispose();
                            */

                            //VertexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)]);
                            
                            
                            vertBuffers[xx + mapWidth * (yy + mapHeight * zz)] = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionVertex);
                            
                            
                            device.ImmediateContext.MapSubresource(vertBuffers[xx + mapWidth * (yy + mapHeight * zz)], MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                            mappedResource0.WriteRange(chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)].Length);
                            //mappedResource0.Write(chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)]);
                            device.ImmediateContext.UnmapSubresource(vertBuffers[xx + mapWidth * (yy + mapHeight * zz)], 0);
                            mappedResource0.Dispose();


                           
                            
                            matrixBufferDescriptionVertex = new BufferDescription()
                            {
                                Usage = ResourceUsage.Dynamic,
                                SizeInBytes = Marshal.SizeOf(typeof(Vector4)) * chunkdat.arrayOfInstanceNormals[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                BindFlags = BindFlags.VertexBuffer,
                                CpuAccessFlags = CpuAccessFlags.Write,
                                OptionFlags = ResourceOptionFlags.None,
                                StructureByteStride = 0
                            };
                            /*NormalBuffer = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionVertex);
                            device.ImmediateContext.MapSubresource(NormalBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                            mappedResource0.WriteRange(chunkdat.arrayOfInstanceNormals[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceNormals[xx + mapWidth * (yy + mapHeight * zz)].Length);
                            device.ImmediateContext.UnmapSubresource(NormalBuffer, 0);
                            mappedResource0.Dispose();*/

                            normalBuffers[xx + mapWidth * (yy + mapHeight * zz)] = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionVertex);
                            device.ImmediateContext.MapSubresource(normalBuffers[xx + mapWidth * (yy + mapHeight * zz)], MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                            mappedResource0.WriteRange(chunkdat.arrayOfInstanceNormals[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceNormals[xx + mapWidth * (yy + mapHeight * zz)].Length);
                            device.ImmediateContext.UnmapSubresource(normalBuffers[xx + mapWidth * (yy + mapHeight * zz)], 0);
                            mappedResource0.Dispose();
                            

                           

                            
                            matrixBufferDescriptionVertex = new BufferDescription()
                            {
                                Usage = ResourceUsage.Dynamic,
                                SizeInBytes = Marshal.SizeOf(typeof(Vector4)) * chunkdat.arrayOfInstanceTextureCoordinates[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                BindFlags = BindFlags.VertexBuffer,
                                CpuAccessFlags = CpuAccessFlags.Write,
                                OptionFlags = ResourceOptionFlags.None,
                                StructureByteStride = 0
                            };
                            /*TextureBuffer = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionVertex);
                            device.ImmediateContext.MapSubresource(TextureBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                            mappedResource0.WriteRange(chunkdat.arrayOfInstanceTextureCoordinates[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceTextureCoordinates[xx + mapWidth * (yy + mapHeight * zz)].Length);
                            device.ImmediateContext.UnmapSubresource(TextureBuffer, 0);
                            mappedResource0.Dispose();*/


                            /*device.ImmediateContext.MapSubresource(chunkdat.texBuffers[xx + mapWidth * (yy + mapHeight * zz)], MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                            mappedResource0.WriteRange(chunkdat.arrayOfInstanceTextureCoordinates[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceTextureCoordinates[xx + mapWidth * (yy + mapHeight * zz)].Length);
                            device.ImmediateContext.UnmapSubresource(chunkdat.texBuffers[xx + mapWidth * (yy + mapHeight * zz)], 0);
                            mappedResource0.Dispose();*/

                            texBuffers[xx + mapWidth * (yy + mapHeight * zz)] = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionVertex);
                            device.ImmediateContext.MapSubresource(texBuffers[xx + mapWidth * (yy + mapHeight * zz)], MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                            mappedResource0.WriteRange(chunkdat.arrayOfInstanceTextureCoordinates[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceTextureCoordinates[xx + mapWidth * (yy + mapHeight * zz)].Length);
                            device.ImmediateContext.UnmapSubresource(texBuffers[xx + mapWidth * (yy + mapHeight * zz)], 0);
                            mappedResource0.Dispose();






                            BufferDescription lightBufferDesc = new BufferDescription()
                            {
                                Usage = ResourceUsage.Dynamic,
                                SizeInBytes = Utilities.SizeOf<SC_VR_Chunk.DLightBuffer>(), // Must be divisable by 16 bytes, so this is equated to 32.
                                BindFlags = BindFlags.ConstantBuffer,
                                CpuAccessFlags = CpuAccessFlags.Write,
                                OptionFlags = ResourceOptionFlags.None,
                                StructureByteStride = 0
                            };

                            // Create the constant buffer pointer so we can access the vertex shader constant buffer from within this class.
                            lightBuffers[xx + mapWidth * (yy + mapHeight * zz)] = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);



                            constantBuffers[xx + mapWidth * (yy + mapHeight * zz)] = new SharpDX.Direct3D11.Buffer(sccs.scgraphics.scdirectx.D3D.device, Utilities.SizeOf<SC_VR_Chunk.DMatrixBuffer>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);
                            instanceBuffers[xx + mapWidth * (yy + mapHeight * zz)] = new SharpDX.Direct3D11.Buffer(sccs.scgraphics.scdirectx.D3D.device, Utilities.SizeOf<SC_VR_Chunk.DInstanceType>() * chunkdat.instanceslength, ResourceUsage.Dynamic, BindFlags.VertexBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

                            chunkdat.instanceBuffers[xx + mapWidth * (yy + mapHeight * zz)] = instanceBuffers[xx + mapWidth * (yy + mapHeight * zz)];


                            /*
                            SharpDX.Direct3D11.Buffer VertexBuffer;
                            SharpDX.Direct3D11.Buffer NormalBuffer;
                            SharpDX.Direct3D11.Buffer TextureBuffer;*/



                            /*matrixBufferDescriptionTHREE = new BufferDescription()
                            {
                                Usage = ResourceUsage.Dynamic,
                                SizeInBytes = Marshal.SizeOf(typeof(Vector4)) * chunkdat.arrayOfInstanceColors[xx + mapWidth * (yy + mapHeight * zz)].Length,
                                BindFlags = BindFlags.VertexBuffer,
                                CpuAccessFlags = CpuAccessFlags.Write,
                                OptionFlags = ResourceOptionFlags.None,
                                StructureByteStride = 0
                            };
                            ColorBuffer = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionTHREE);

                            device.ImmediateContext.MapSubresource(ColorBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                            mappedResource0.WriteRange(chunkdat.arrayOfInstanceVertex[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceVertex[xx + mapWidth * (yy + mapHeight * zz)].Length);
                            device.ImmediateContext.UnmapSubresource(ColorBuffer, 0);
                            mappedResource0.Dispose();*/


                            /*
                            IndexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, chunkdat.arrayOfInstanceIndices[xx + mapWidth * (yy + mapHeight * zz)]);
                            */



                            //IndexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, chunkdat.arrayOfInstanceIndices[xx + mapWidth * (yy + mapHeight * zz)]);

                            indexBuffers[xx + mapWidth * (yy + mapHeight * zz)] = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, chunkdat.arrayOfInstanceIndices[xx + mapWidth * (yy + mapHeight * zz)]);



                            chunkdat.dVertBuffers[xx + mapWidth * (yy + mapHeight * zz)] = vertBuffers[xx + mapWidth * (yy + mapHeight * zz)];

                            chunkdat.indexBuffers[xx + mapWidth * (yy + mapHeight * zz)] = indexBuffers[xx + mapWidth * (yy + mapHeight * zz)];



                            //chunkdat.colorBuffers[xx + mapWidth * (yy + mapHeight * zz)] = ColorBuffer;

                            chunkdat.normalBuffers[xx + mapWidth * (yy + mapHeight * zz)] = normalBuffers[xx + mapWidth * (yy + mapHeight * zz)];
                            chunkdat.texBuffers[xx + mapWidth * (yy + mapHeight * zz)] = texBuffers[xx + mapWidth * (yy + mapHeight * zz)];




                   



                            /*
                            chunkdat.dVertBuffers[xx + mapWidth * (yy + mapHeight * zz)] = VertexBuffer;
                            chunkdat.indexBuffers[xx + mapWidth * (yy + mapHeight * zz)] = IndexBuffer;
                            //chunkdat.colorBuffers[xx + mapWidth * (yy + mapHeight * zz)] = ColorBuffer;
                            chunkdat.normalBuffers[xx + mapWidth * (yy + mapHeight * zz)] = NormalBuffer;
                            chunkdat.texBuffers[xx + mapWidth * (yy + mapHeight * zz)] = TextureBuffer;
                            
                            */

                            //chunkdat.instanceBuffers[xx + mapWidth * (yy + mapHeight * zz)] = instanceBuff;

                            //ColorBuffer.Dispose();
                            //IndexBuffer.Dispose();
                        }
                        /*
                        device.ImmediateContext.MapSubresource(vertBuffers[xx + mapWidth * (yy + mapHeight * zz)], MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                        mappedResource0.WriteRange(chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)].Length);
                        //mappedResource0.Write(chunkdat.dVertexData[xx + mapWidth * (yy + mapHeight * zz)]);
                        device.ImmediateContext.UnmapSubresource(vertBuffers[xx + mapWidth * (yy + mapHeight * zz)], 0);
                        mappedResource0.Dispose();

                        device.ImmediateContext.MapSubresource(normalBuffers[xx + mapWidth * (yy + mapHeight * zz)], MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                        mappedResource0.WriteRange(chunkdat.arrayOfInstanceNormals[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceNormals[xx + mapWidth * (yy + mapHeight * zz)].Length);
                        device.ImmediateContext.UnmapSubresource(normalBuffers[xx + mapWidth * (yy + mapHeight * zz)], 0);
                        mappedResource0.Dispose();


                        device.ImmediateContext.MapSubresource(texBuffers[xx + mapWidth * (yy + mapHeight * zz)], MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                        mappedResource0.WriteRange(chunkdat.arrayOfInstanceTextureCoordinates[xx + mapWidth * (yy + mapHeight * zz)], 0, chunkdat.arrayOfInstanceTextureCoordinates[xx + mapWidth * (yy + mapHeight * zz)].Length);
                        device.ImmediateContext.UnmapSubresource(texBuffers[xx + mapWidth * (yy + mapHeight * zz)], 0);
                        mappedResource0.Dispose();*/



                        device.ImmediateContext.MapSubresource(lightBuffers[xx + mapWidth * (yy + mapHeight * zz)], MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                        mappedResourceLight.WriteRange(chunkdat.lightBuffer, 0, chunkdat.lightBuffer.Length);
                        device.ImmediateContext.UnmapSubresource(lightBuffers[xx + mapWidth * (yy + mapHeight * zz)], 0);
                        mappedResourceLight.Dispose();


                        device.ImmediateContext.MapSubresource(chunkdat.instanceBuffers[xx + mapWidth * (yy + mapHeight * zz)], MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                        mappedResource.WriteRange(chunkdat.arrayOfInstancePos, 0, chunkdat.arrayOfInstancePos.Length);
                        device.ImmediateContext.UnmapSubresource(chunkdat.instanceBuffers[xx + mapWidth * (yy + mapHeight * zz)], 0);
                        mappedResource.Dispose();



                        device.ImmediateContext.MapSubresource(constantBuffers[xx + mapWidth * (yy + mapHeight * zz)], MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streamerTWO);
                        streamerTWO.WriteRange(chunkdat.matrixBuffer, 0, chunkdat.matrixBuffer.Length);
                        device.ImmediateContext.UnmapSubresource(constantBuffers[xx + mapWidth * (yy + mapHeight * zz)], 0);
                        streamerTWO.Dispose();





                        /*
                        device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
                     {
                            new VertexBufferBinding(instanceBuffer, Utilities.SizeOf<SC_VR_Chunk.DInstanceType>(),0), //chunkdat.instanceBuffers[xx+mapWidth*(yy+mapHeight*zz)]
                        });

                        device.ImmediateContext.InputAssembler.SetIndexBuffer(chunkdat.indexBuffers[xx + mapWidth * (yy + mapHeight * zz)], SharpDX.DXGI.Format.R32_UInt, 0);


                        //var vertexBufferBinding = new VertexBufferBinding(chunkdat.dVertBuffers[xx + mapWidth * (yy + mapHeight * zz)], Utilities.SizeOf<SC_VR_Chunk.DVertex>(), 0);
                        //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, vertexBufferBinding);

                        device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
                        {
                            new VertexBufferBinding(chunkdat.dVertBuffers[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(SC_VR_Chunk.DVertex)), 0),
                        });*/



                        device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
                        {
                            new VertexBufferBinding(chunkdat.instanceBuffers[xx + mapWidth * (yy + mapHeight * zz)], Utilities.SizeOf<SC_VR_Chunk.DInstanceType>(),0), //chunkdat.instanceBuffers[xx+mapWidth*(yy+mapHeight*zz)]
                        });

                        device.ImmediateContext.InputAssembler.SetIndexBuffer(chunkdat.indexBuffers[xx + mapWidth * (yy + mapHeight * zz)], SharpDX.DXGI.Format.R32_UInt, 0);


                        //var vertexBufferBinding = new VertexBufferBinding(chunkdat.dVertBuffers[xx + mapWidth * (yy + mapHeight * zz)], Utilities.SizeOf<SC_VR_Chunk.DVertex>(), 0);
                        //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, vertexBufferBinding);

                        device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
                        {
                            new VertexBufferBinding(chunkdat.dVertBuffers[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(SC_VR_Chunk.DVertex)), 0),
                        });





                        device.ImmediateContext.VertexShader.SetConstantBuffer(0, constantBuffers[xx + mapWidth * (yy + mapHeight * zz)]);
                        device.ImmediateContext.GeometryShader.SetConstantBuffer(0, constantBuffers[xx + mapWidth * (yy + mapHeight * zz)]);

                        device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                        device.ImmediateContext.InputAssembler.InputLayout = Layout;

                        device.ImmediateContext.VertexShader.Set(VertexShader);
                        device.ImmediateContext.PixelShader.Set(PixelShader);


                        device.ImmediateContext.GeometryShader.Set(geoShader);
                        device.ImmediateContext.PixelShader.SetConstantBuffer(0, lightBuffers[xx + mapWidth * (yy + mapHeight * zz)]);




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

                        //device.ImmediateContext.DrawIndexed(chunkdat.arrayOfInstanceIndices[xx + mapWidth * (yy + mapHeight * zz)].Length,0,0);
                        device.ImmediateContext.DrawIndexedInstanced(chunkdat.arrayOfInstanceIndices[xx + mapWidth * (yy + mapHeight * zz)].Length, 1, 0, 0, xx + mapWidth * (yy + mapHeight * zz));
                        //device.ImmediateContext.DrawIndexed(chunkdat.arrayOfInstanceIndices[xx + mapWidth * (yy + mapHeight * zz)].Length, 0, 0);
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


                startOnce = 0;





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
