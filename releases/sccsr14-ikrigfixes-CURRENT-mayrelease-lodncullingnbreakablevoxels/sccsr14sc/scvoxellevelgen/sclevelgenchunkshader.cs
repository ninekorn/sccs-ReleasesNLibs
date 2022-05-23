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

using DSharpDXRastertek.Tut16.Graphics.Data;

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
        DFrustum somefrustrum;//= new DFrustum();

        //, SharpDX.Direct3D11.Buffer _instanceBuffer
        public sclevelgenchunkshader(SharpDX.Direct3D11.Device _device, SharpDX.Direct3D11.Buffer _constantBuffer, InputLayout _Layout, VertexShader _VertexShader, PixelShader _PixelShader,GeometryShader _geoShader, SharpDX.Direct3D11.Buffer _instanceBuffer, SharpDX.Direct3D11.Buffer _ConstantLightBuffer,int instancecount)
        {
            this.constantBuffer = _constantBuffer;
            this.device = _device;
            this.instanceBuffer = _instanceBuffer;
            this.PixelShader = _PixelShader;
            this.VertexShader = _VertexShader;
            this.Layout = _Layout;
            this.ConstantLightBuffer = _ConstantLightBuffer;
            this.geoShader = _geoShader;

            /*mapWidth = sclevelgenchunk.mapWidth;
            mapHeight = sclevelgenchunk.mapHeight;
            mapDepth = sclevelgenchunk.mapDepth;

            tinyChunkWidth = sclevelgenchunk.tinyChunkWidth;
            tinyChunkHeight = sclevelgenchunk.tinyChunkHeight;
            tinyChunkDepth = sclevelgenchunk.tinyChunkDepth;

            mapObjectInstanceWidth = sclevelgenchunk.mapObjectInstanceWidth;
            mapObjectInstanceHeight = sclevelgenchunk.mapObjectInstanceHeight;
            mapObjectInstanceDepth = sclevelgenchunk.mapObjectInstanceDepth;*/
            total = instancecount; //mapWidth * mapHeight * mapDepth
            somefrustrum = new DFrustum();


        }

        /*public int mapWidth = 20;
        public int mapHeight = 1;
        public int mapDepth = 20;

        public int tinyChunkWidth = 20;
        public int tinyChunkHeight = 20;
        public int tinyChunkDepth = 20;

        public int mapObjectInstanceWidth = 1;
        public int mapObjectInstanceHeight = 1;
        public int mapObjectInstanceDepth = 1;*/

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
        Vector3 rightdir;
        Vector3 updir;
        Vector3 forwarddir;
     

        float someradius = 1.15f;
        float distsquared = 31;
        bool somedraw = true;


        public sccs.sclevelgenchunk.chunkData Renderer(sccs.sclevelgenchunk.chunkData chunkdat) //async
        {
            try
            {

                device.ImmediateContext.MapSubresource(constantBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streamerTWO);
                streamerTWO.WriteRange(chunkdat.matrixBuffer, 0, chunkdat.matrixBuffer.Length);
                device.ImmediateContext.UnmapSubresource(constantBuffer, 0);
                streamerTWO.Dispose();


                device.ImmediateContext.MapSubresource(ConstantLightBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                mappedResourceLight.WriteRange(chunkdat.lightBuffer, 0, chunkdat.lightBuffer.Length);
                //mappedResourceLight.Write(chunkdat.lightBuffer);
                device.ImmediateContext.UnmapSubresource(ConstantLightBuffer, 0);
                mappedResourceLight.Dispose();

                
                device.ImmediateContext.VertexShader.SetConstantBuffer(0, constantBuffer);
                //device.ImmediateContext.GeometryShader.SetConstantBuffer(0, constantBuffer);
                device.ImmediateContext.PixelShader.SetConstantBuffer(0, ConstantLightBuffer);

                device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                device.ImmediateContext.InputAssembler.InputLayout = Layout;
                device.ImmediateContext.VertexShader.Set(VertexShader);
                device.ImmediateContext.PixelShader.Set(PixelShader);
                device.ImmediateContext.GeometryShader.Set(null); //geoShader


                device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
                {
                    new VertexBufferBinding(instanceBuffer, Utilities.SizeOf<sclevelgenchunk.DInstanceType>(),0), //chunkdat.instanceBuffers[xx+mapWidth*(yy+mapHeight*zz)]
                });


                //rightdir = new Vector3(scgraphics.scupdate.dirikvoxelbodyInstanceRight0.X, scgraphics.scupdate.dirikvoxelbodyInstanceRight0.Y, scgraphics.scupdate.dirikvoxelbodyInstanceRight0.Z);
                //updir = new Vector3(scgraphics.scupdate.dirikvoxelbodyInstanceUp0.X, scgraphics.scupdate.dirikvoxelbodyInstanceUp0.Y, scgraphics.scupdate.dirikvoxelbodyInstanceUp0.Z);
                //forwarddir = new Vector3(scgraphics.scupdate.dirikvoxelbodyInstanceForward0.X, scgraphics.scupdate.dirikvoxelbodyInstanceForward0.Y, scgraphics.scupdate.dirikvoxelbodyInstanceForward0.Z);

                somefrustrum.ConstructFrustum(1000.0f, chunkdat.projectionMatrix, chunkdat.viewMatrix);


                for (int t = 0; t < chunkdat.arrayOfInstancePos.Length; t++)
                {
                    Vector3 chunkpos = chunkdat.arrayOfInstancePos[t].position;

                    

                    //Console.WriteLine(somedraw);
                    Vector3 posplayer = scgraphics.scupdate.OFFSETPOS;
                    //float distsquared = 0;
                    //Vector3.DistanceSquared(ref posplayer, ref chunkpos, out distsquared);


                    if (chunkdat.startOnce == 1)
                    {
                        distsquared = sc_maths.trying_ellipsoid_with_sc_sebastian_lague_check_distanceconvertedto3dkinda(posplayer, chunkpos);

                        if (distsquared < 30)
                        {
                            somedraw = somefrustrum.CheckSphere(chunkpos, someradius);
                        }
                        else
                        {
                            continue;
                        }
                    }



                    if (somedraw)
                    {
                        if (chunkdat.dVertexData[t].Length > 0) ;// chunkdat.arrayOfInstanceVertex[t].Length > 0)
                        {
                            if (chunkdat.someswitches[t] == 0)
                            {
                                //MainWindow.MessageBox((IntPtr)0, "test2", "scmsg", 0);

                                /*matrixBufferDescriptionVertex = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(sclevelgenchunk.DVertex)) * chunkdat.dVertexData[t].Length,
                                    BindFlags = BindFlags.VertexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };
                                VertexBuffer = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionVertex);*/

                                VertexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, chunkdat.dVertexData[t]);


                                //VertexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, chunkdat.dVertexData[t]);
                                /*
                                device.ImmediateContext.MapSubresource(VertexBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.dVertexData[t], 0, chunkdat.dVertexData[t].Length);
                                //mappedResource0.Write(chunkdat.dVertexData[t]);
                                device.ImmediateContext.UnmapSubresource(VertexBuffer, 0);
                                mappedResource0.Dispose();*/


                                /*
                                matrixBufferDescriptionVertex = new BufferDescription()
                                {
                                    Usage = ResourceUsage.Dynamic,
                                    SizeInBytes = Marshal.SizeOf(typeof(int)) * chunkdat.arrayOfInstanceIndices[t].Length,
                                    BindFlags = BindFlags.IndexBuffer,
                                    CpuAccessFlags = CpuAccessFlags.Write,
                                    OptionFlags = ResourceOptionFlags.None,
                                    StructureByteStride = 0
                                };

                                IndexBuffer = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionVertex);*/

                                IndexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, chunkdat.arrayOfInstanceIndices[t]);
                                /*
                                device.ImmediateContext.MapSubresource(IndexBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource0);
                                mappedResource0.WriteRange(chunkdat.arrayOfInstanceIndices[t], 0, chunkdat.arrayOfInstanceIndices[t].Length);
                                device.ImmediateContext.UnmapSubresource(IndexBuffer, 0);
                                mappedResource0.Dispose();*/


                                //IndexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, chunkdat.arrayOfInstanceIndices[t]);

                                chunkdat.dVertBuffers[t] = VertexBuffer;

                                chunkdat.indexBuffers[t] = IndexBuffer;



                                device.ImmediateContext.MapSubresource(instanceBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                                mappedResource.WriteRange(chunkdat.arrayOfInstancePos, 0, chunkdat.arrayOfInstancePos.Length);
                                device.ImmediateContext.UnmapSubresource(instanceBuffer, 0);
                                mappedResource.Dispose();



                            }



                            device.ImmediateContext.InputAssembler.SetIndexBuffer(chunkdat.indexBuffers[t], SharpDX.DXGI.Format.R32_UInt, 0);


                            //var vertexBufferBinding = new VertexBufferBinding(chunkdat.dVertBuffers[t], Utilities.SizeOf<SC_VR_Chunk.DVertex>(), 0);
                            //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, vertexBufferBinding);

                            device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
                            {
                                new VertexBufferBinding(chunkdat.dVertBuffers[t],Marshal.SizeOf(typeof(sclevelgenchunk.DVertex)), 0),
                            });


                            /*device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
                            {
                                new VertexBufferBinding(chunkdat.normalBuffers[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(Vector3)), 0),
                            });


                            device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
                            {
                                new VertexBufferBinding(chunkdat.texBuffers[xx+mapWidth*(yy+mapHeight*zz)],Marshal.SizeOf(typeof(Vector2)), 0),
                            });*/



                            //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(chunkdat.dVertBuffers[t], Utilities.SizeOf<SC_VR_Chunk.DVertex>(), 0));

                            /*device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
                            {
                                new VertexBufferBinding(chunkdat.colorBuffers[xx+mapWidth*(yy+mapHeight*zz)], Marshal.SizeOf(typeof(Vector4)), 0),
                            });*/


                            device.ImmediateContext.DrawIndexedInstanced(chunkdat.arrayOfInstanceIndices[t].Length, 1, 0, 0, t);
                            //device.ImmediateContext.DrawIndexed(chunkdat.arrayOfInstanceIndices[t].Length, 0, 0);
                        }
                    }
                    chunkdat.someswitches[t] = 1;
                }


                //chunkdat.startOnce = 1;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
                //return false;
            }
            return chunkdat;
        }
    }
}