////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//DEVELOPED BY STEVE CHASSÉ using xoofx's sharpdx original deferred rendering sample. This is a software of mixed architecture//
//using rastertek c# github user dan6040's sample architecture and smartrak's sample architecture and xoofx sharpdx samples/////
//architecture./////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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


//The MIT License (MIT)
//
//Copyright(c) 2016 Smartrak

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

//https://github.com/Dan6040/SharpDX-Rastertek-Tutorials
//https://github.com/Smartrak/WpfSharpDxControl
//https://github.com/sharpdx/SharpDX-Samples

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.Direct2D1;
using SharpDX.DXGI;

using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Windows;
using Buffer = SharpDX.Direct3D11.Buffer;
using Color = SharpDX.Color;
using Device = SharpDX.Direct3D11.Device;
using MapFlags = SharpDX.Direct3D11.MapFlags;

using InputElement = SharpDX.Direct3D11.InputElement;

using System.Runtime.InteropServices;

using DeviceContext = SharpDX.Direct3D11.DeviceContext;


namespace sccsr15forms
{

    public class tutorialfacemesh : IDisposable
    {
        public int writetobufferswtc = 0;


        public vertforinstances somevertforinstances;
        public Vector3 position = Vector3.Zero;

        public tutorialcubeaschunkinst.DVertex[] arrayofverts;
        public int[] arrayoftrigs;
        public SharpDX.Direct3D11.Buffer IndicesBuffer;
        //public Buffer staticContantBuffer;
        //public Buffer dynamicConstantBuffer;
        public InputLayout layout;
        public PixelShader pixelShader;
        public VertexShader vertexShader;
        public Buffer verticesbuffer;
        //public Vector4[] vertices;

        // Create Constant Buffer 
        /*public Buffer staticContantBufferuithread;
        public Buffer dynamicConstantBufferuithread;

        // Create Constant Buffer 
        public Buffer staticContantBuffersysthread;//
        public Buffer dynamicConstantBuffersysthread;*/

        public tutorialcubeaschunkinst somecubeaschunkinst;
        int facetype;

        //, tutorialcubeaschunkinst somecubeaschunkinst_
        public tutorialfacemesh(Device device, int w, int h, int d, float planesize, int numberofmesh, int facetype_)
        {
            //somecubeaschunkinst = somecubeaschunkinst_;

            facetype = facetype_;


            /*if (w != 0 && h != 0 && d != 0)
            {
               
            }*/

            somevertforinstances = new vertforinstances();
            somevertforinstances.startBuildingArray(Vector4.Zero, out arrayofverts, out arrayoftrigs, planesize, w, h, d, numberofmesh, facetype);

            //Console.WriteLine("w:" + w + "/h:" + h + "/d:" + d);









            if (arrayofverts != null)
            {
                if (arrayofverts.Length > 0)
                {
                    //int instancecount = 20;

                    IndicesBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, arrayoftrigs);
                    // Instantiate Vertex buiffer from vertex data 
                    verticesbuffer = Buffer.Create(device, BindFlags.VertexBuffer, arrayofverts);

                    // Create Constant Buffer 
                    //staticContantBuffer = new Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
                    //dynamicConstantBuffer = new Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

                }
                else
                {
                    Console.WriteLine("no vertex1");
                }
            }
            else
            {
                Console.WriteLine("no vertex0");
            }
        }


        public tutorialcubeaschunkinst.instancetype[] instances;



        public tutorialcubeaschunkinst.scinstanceintmaps[] arrayofmapints;


        public tutorialcubeaschunkinst.scinstancevertdimensions[] arrayofdimensionsa;
        public tutorialcubeaschunkinst.scinstancevertdimensions[] arrayofdimensionsb;
        //tutorialcubeaschunkinst.scinstancevertdimensions[] arrayofdimensionsc;
        //tutorialcubeaschunkinst.scinstancevertdimensions[] arrayofdimensionsd;

        public SharpDX.Direct3D11.Buffer dimensionsbuffera;
        public SharpDX.Direct3D11.Buffer dimensionsbufferb;
        //public SharpDX.Direct3D11.Buffer dimensionsbufferc;
        //public SharpDX.Direct3D11.Buffer dimensionsbufferd;



        public tutorialcubeaschunkinst.scinstancevertdimensions[] arrayoffirstvertloca;
        public tutorialcubeaschunkinst.scinstancevertdimensions[] arrayoffirstvertlocb;
        //tutorialcubeaschunkinst.scinstancevertdimensions[] arrayoffirstvertlocc;
        //tutorialcubeaschunkinst.scinstancevertdimensions[] arrayoffirstvertlocd;

        public SharpDX.Direct3D11.Buffer firstvertlocbuffera;
        public SharpDX.Direct3D11.Buffer firstvertlocbufferb;
        //public SharpDX.Direct3D11.Buffer firstvertlocbufferc;
        //public SharpDX.Direct3D11.Buffer firstvertlocbufferd;








        public SharpDX.Direct3D11.Buffer mapinstbuffer;
        //public SharpDX.Direct3D11.Buffer someinstverticesbuffer;


        public SharpDX.Direct3D11.Buffer InstanceBuffer;
        int indexinmain;

        //Vector4[] arrayofverticesinst;// = new Vector4[][];
        
        /*public List<tutorialcubeaschunkinst.instancetype> instancetypelist = new List<tutorialcubeaschunkinst.instancetype>();

        public List<tutorialcubeaschunkinst.scinstanceintmaps> mapints = new List<tutorialcubeaschunkinst.scinstanceintmaps>();

        public List<tutorialcubeaschunkinst.scinstancevertdimensions> dimensionsmapsa = new List<tutorialcubeaschunkinst.scinstancevertdimensions>();
        public List<tutorialcubeaschunkinst.scinstancevertdimensions> dimensionsmapsb = new List<tutorialcubeaschunkinst.scinstancevertdimensions>();

        public List<tutorialcubeaschunkinst.scinstancevertdimensions> firstvertloca = new List<tutorialcubeaschunkinst.scinstancevertdimensions>();
        public List<tutorialcubeaschunkinst.scinstancevertdimensions> firstvertlocb = new List<tutorialcubeaschunkinst.scinstancevertdimensions>();
        */

        public Dictionary<int, tutorialcubeaschunkinst.instancetype> instancetypelist = new Dictionary<int, tutorialcubeaschunkinst.instancetype>();

        public Dictionary<int, tutorialcubeaschunkinst.scinstanceintmaps> mapints = new Dictionary<int, tutorialcubeaschunkinst.scinstanceintmaps>();

        public Dictionary<int, tutorialcubeaschunkinst.scinstancevertdimensions> dimensionsmapsa = new Dictionary<int, tutorialcubeaschunkinst.scinstancevertdimensions>();
        public Dictionary<int, tutorialcubeaschunkinst.scinstancevertdimensions> dimensionsmapsb = new Dictionary<int, tutorialcubeaschunkinst.scinstancevertdimensions>();

        public Dictionary<int, tutorialcubeaschunkinst.scinstancevertdimensions> firstvertloca = new Dictionary<int, tutorialcubeaschunkinst.scinstancevertdimensions>();
        public Dictionary<int, tutorialcubeaschunkinst.scinstancevertdimensions> firstvertlocb = new Dictionary<int,tutorialcubeaschunkinst.scinstancevertdimensions>();

        //public List<int> listofindexes = new List<int>();







        /*public void removefromarrays(int indextoremoveat)
        {
            //instancetype
        }
        */


        ///  

        //, List<tutorialcubeaschunkinst.instancetype> instancetypelist, List<tutorialcubeaschunkinst.scinstanceintmaps> mapints, List<tutorialcubeaschunkinst.scinstancevertdimensions> dimensionsmapsa, List<tutorialcubeaschunkinst.scinstancevertdimensions> dimensionsmapsb, List<tutorialcubeaschunkinst.scinstancevertdimensions> firstvertloca, List<tutorialcubeaschunkinst.scinstancevertdimensions> firstvertlocb


        //https://stackoverflow.com/questions/7555690/how-to-get-dictionary-values-as-a-generic-list
        public void createinstances(SharpDX.Direct3D11.Device device, int indexinmain_)
        {

            //Console.WriteLine(dimensionsmapsa.Count);


            if (InstanceBuffer != null)
            {
                InstanceBuffer.Dispose();
                InstanceBuffer = null;
            }
            if (mapinstbuffer != null)
            {
                mapinstbuffer.Dispose();
                mapinstbuffer = null;
            }
            if (dimensionsbuffera != null)
            {
                dimensionsbuffera.Dispose();
                dimensionsbuffera = null;
            }
            if (dimensionsbufferb != null)
            {
                dimensionsbufferb.Dispose();
                dimensionsbufferb = null;
            }
            if (firstvertlocbuffera != null)
            {
                firstvertlocbuffera.Dispose();
                firstvertlocbuffera = null;
            }
            if (firstvertlocbufferb != null)
            {
                firstvertlocbufferb.Dispose();
                firstvertlocbufferb = null;
            }










            indexinmain = indexinmain_;

            /*instances = new tutorialcubeaschunkinst.instancetype[listofpositions.Count];

            for (int i = 0;i < listofpositions.Count;i++)
            {
                //+ listofverts[i][i * 4].position

                instances[i].instancePos = listofpositions[i];
            }*/
            instances = instancetypelist.Values.ToArray();

            var matrixBufferDescriptionVertex00 = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Marshal.SizeOf(typeof(tutorialcubeaschunkinst.instancetype)) * instancetypelist.Count,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            InstanceBuffer = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionVertex00);

            var mapinstdesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstanceintmaps)) * mapints.Count,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            mapinstbuffer = new SharpDX.Direct3D11.Buffer(device, mapinstdesc);



            var dimensionsmapsdesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)) * dimensionsmapsa.Count,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            dimensionsbuffera = new SharpDX.Direct3D11.Buffer(device, dimensionsmapsdesc);

            dimensionsmapsdesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)) * dimensionsmapsb.Count,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            dimensionsbufferb = new SharpDX.Direct3D11.Buffer(device, dimensionsmapsdesc);


            /*
            dimensionsmapsdesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)) * dimensionsmapsc.Count,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            dimensionsbufferc = new SharpDX.Direct3D11.Buffer(device, dimensionsmapsdesc);

            dimensionsmapsdesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)) * dimensionsmapsd.Count,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            dimensionsbufferd = new SharpDX.Direct3D11.Buffer(device, dimensionsmapsdesc);
            */
            arrayofmapints = mapints.Values.ToArray();

            arrayofdimensionsa = dimensionsmapsa.Values.ToArray();
            arrayofdimensionsb = dimensionsmapsb.Values.ToArray();
            //arrayofdimensionsc = dimensionsmapsc.ToArray();
            //arrayofdimensionsd = dimensionsmapsd.ToArray();






            dimensionsmapsdesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)) * firstvertloca.Count,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            firstvertlocbuffera = new SharpDX.Direct3D11.Buffer(device, dimensionsmapsdesc);

            dimensionsmapsdesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)) * firstvertlocb.Count,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            firstvertlocbufferb = new SharpDX.Direct3D11.Buffer(device, dimensionsmapsdesc);


            /*
            dimensionsmapsdesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)) * firstvertlocc.Count,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            firstvertlocbufferc = new SharpDX.Direct3D11.Buffer(device, dimensionsmapsdesc);

            dimensionsmapsdesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)) * firstvertlocd.Count,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            firstvertlocbufferd = new SharpDX.Direct3D11.Buffer(device, dimensionsmapsdesc);
            */




            arrayoffirstvertloca = firstvertloca.Values.ToArray();
            arrayoffirstvertlocb = firstvertlocb.Values.ToArray();
            //arrayoffirstvertlocc = firstvertlocc.ToArray();
            //arrayoffirstvertlocd = firstvertlocd.ToArray();




        }


        public void createdataforinstances(SharpDX.Direct3D11.Device device, int indexinmain_, List<tutorialcubeaschunkinst.instancetype> instancetype, List<tutorialcubeaschunkinst.scinstanceintmaps> mapints, List<tutorialcubeaschunkinst.scinstancevertdimensions> dimensionsmapsa, List<tutorialcubeaschunkinst.scinstancevertdimensions> dimensionsmapsb, List<tutorialcubeaschunkinst.scinstancevertdimensions> firstvertloca, List<tutorialcubeaschunkinst.scinstancevertdimensions> firstvertlocb)
        {

        }


        DataStream mappedResourceLight;

        //updateSec.worldviewprobuffer[] worldViewProj




        DataStream mappedResource;


        public void Render(DeviceContext deviceContext)
        {
            RenderBuffers(deviceContext);
        }
        private void RenderBuffers(DeviceContext deviceContext)
        {
            //deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(verticesbuffer, Utilities.SizeOf<tutorialcubeaschunkinst.DVertex>(), 0));



            deviceContext.InputAssembler.SetVertexBuffers(0, new[]
                  {
                        new VertexBufferBinding(verticesbuffer,Marshal.SizeOf(typeof(tutorialcubeaschunkinst.DVertex)), 0),
                        //new VertexBufferBinding(chunkdat.indexBuffer, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceType)),0),
                    });

            deviceContext.InputAssembler.SetVertexBuffers(1, new[]
                    {
                        new VertexBufferBinding(InstanceBuffer, Marshal.SizeOf(typeof(tutorialcubeaschunkinst.instancetype)),0),
                    });


            deviceContext.InputAssembler.SetVertexBuffers(2, new[]
                  {
                        new VertexBufferBinding(mapinstbuffer, Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstanceintmaps)),0),
                    });

            deviceContext.InputAssembler.SetVertexBuffers(3, new[]
                  {
                        new VertexBufferBinding(dimensionsbuffera, Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)),0),
                    });

            deviceContext.InputAssembler.SetVertexBuffers(4, new[]
                  {
                        new VertexBufferBinding(dimensionsbufferb, Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)),0),
                    });

            deviceContext.InputAssembler.SetVertexBuffers(5, new[]
                  {
                        new VertexBufferBinding(firstvertlocbuffera, Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)),0),
                    });

            deviceContext.InputAssembler.SetVertexBuffers(6, new[]
                  {
                        new VertexBufferBinding(firstvertlocbufferb, Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)),0),
                    });





            deviceContext.InputAssembler.SetIndexBuffer(IndicesBuffer, SharpDX.DXGI.Format.R32_UInt, 0);
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
        }



        ~tutorialfacemesh()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // so that Dispose(false) isn't called later
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                /*if (somecubeaschunkinst.staticContantBuffer != null)
                {
                    somecubeaschunkinst.staticContantBuffer.Dispose();
                    somecubeaschunkinst.staticContantBuffer = null;
                }
                if (somecubeaschunkinst.dynamicConstantBuffer != null)
                {
                    somecubeaschunkinst.dynamicConstantBuffer.Dispose();
                    somecubeaschunkinst.dynamicConstantBuffer = null;
                }*/

                /*if (staticContantBufferuithread != null)
                {
                    staticContantBufferuithread.Dispose();
                    staticContantBufferuithread = null;
                }
                if (dynamicConstantBufferuithread != null)
                {
                    dynamicConstantBufferuithread.Dispose();
                    dynamicConstantBufferuithread = null;
                }
                if (staticContantBuffersysthread != null)
                {
                    staticContantBuffersysthread.Dispose();
                    staticContantBuffersysthread = null;
                }
                if (dynamicConstantBuffersysthread != null)
                {
                    dynamicConstantBuffersysthread.Dispose();
                    dynamicConstantBuffersysthread = null;
                }*/

                if (layout != null)
                {
                    layout.Dispose();
                    layout = null;
                }
                if (pixelShader != null)
                {
                    pixelShader.Dispose();
                    pixelShader = null;
                }

                if (vertexShader != null)
                {
                    vertexShader.Dispose();
                    vertexShader = null;
                }

                if (verticesbuffer != null)
                {
                    verticesbuffer.Dispose();
                    verticesbuffer = null;
                }
                /*
                if (vertices != null)
                {
                    vertices = null;
                }*/






                // Dispose all owned managed objects
            }

            // Release unmanaged resources
        }
    }
}
