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

using ObjLoader;
using ObjLoader.Loader;
using ObjLoader.Loader.Common;
using ObjLoader.Loader.Data;
using ObjLoader.Loader.Loaders;
using ObjLoader.Loader.TypeParsers;
using System.IO;

namespace sccsr15forms
{
    internal class sctutorialobj : IDisposable
    {
        public int[] arrayoftrigs;
        public SharpDX.Direct3D11.Buffer IndicesBuffer;
        public Buffer staticContantBuffer;
        public Buffer dynamicConstantBuffer;
        public InputLayout layout;
        public PixelShader pixelShader;
        public VertexShader vertexShader;
        public Buffer verticesbuffer;
        public Vector4[] vertices;

        // Create Constant Buffer 
        /*public Buffer staticContantBufferuithread;
        public Buffer dynamicConstantBufferuithread;

        // Create Constant Buffer 
        public Buffer staticContantBuffersysthread;//
        public Buffer dynamicConstantBuffersysthread;*/


        public sctutorialobj(Device device)
        {



            // Compile Vertex and Pixel shaders 
            var bytecode = ShaderBytecode.CompileFromFile("MultiCube.fx", "VS", "vs_4_0");
            vertexShader = new VertexShader(device, bytecode);

            // Layout from VertexShader input signature 
            layout = new InputLayout(device, ShaderSignature.GetInputSignature(bytecode), new[]
            {
                new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0)
            });
            bytecode.Dispose();

            bytecode = ShaderBytecode.CompileFromFile("MultiCube.fx", "PS", "ps_4_0");
            pixelShader = new PixelShader(device, bytecode);
            bytecode.Dispose();

            /*
            vertices = new Vector4[]
            {
                new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f), // Front 
                new Vector4(-1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),

                new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f), // BACK 
                new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),

                new Vector4(-1.0f, 1.0f, -1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f), // Top 
                new Vector4(-1.0f, 1.0f,  1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                new Vector4( 1.0f, 1.0f,  1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                new Vector4(-1.0f, 1.0f, -1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                new Vector4( 1.0f, 1.0f,  1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                new Vector4( 1.0f, 1.0f, -1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),

                new Vector4(-1.0f,-1.0f, -1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f), // Bottom 
                new Vector4( 1.0f,-1.0f,  1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                new Vector4(-1.0f,-1.0f,  1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                new Vector4(-1.0f,-1.0f, -1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                new Vector4( 1.0f,-1.0f, -1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                new Vector4( 1.0f,-1.0f,  1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),

                new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f), // Left 
                new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                new Vector4(-1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),

                new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f), // Right 
                new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
            };





            arrayoftrigs = new int[]
            {

                0,1,2,3,4,5,
                6,7,8,9,10,11,
                12,13,14,15,16,17,
                18,19,20,21,22,23,
                24,25,26,27,28,29,
                30,31,32,33,34,35,
              
                /*
                5,4,3,2,1,0,
                11,10,9,8,7,6,
                17,16,15,14,13,12,
                23,22,21,20,19,18,
                29,28,27,26,25,24,
                35,34,33,32,31,30,
            };*/





            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();
            //With the signature Func<string, Stream>
            //var objLoaderFactory = new ObjLoaderFactory();
            //var objLoader = objLoaderFactory.Create(materialFileName => File.Open(materialFileName);

            string pathofrelease = Directory.GetCurrentDirectory() + @"\";

            var fileStream = new FileStream(pathofrelease + "blendercube.obj", System.IO.FileMode.Open); //monkey
            var result = objLoader.Load(fileStream);



            arrayoftrigs = new int[result.Vertices.Count * 6];
            //vertices = new Vector4[result.Vertices.Count * 2];

            //Console.WriteLine("count:" + result.Vertices.Count);
         
            int index0 = 0;
            int index1 = 0;
            int index2 = 0;
            int index3 = 0;

            int somecountertrig = 0;
            int somecountervert = 0;

            /*for (int i = 0; i < result.Vertices.Count; )
            {
                if (somecountervert >= result.Vertices.Count)
                {
                    break;
                }
                index0 = somecountervert + 0;
                vertices[somecountervert + 0] = new Vector4(result.Vertices[i + 0].X, result.Vertices[i + 0].Y, result.Vertices[i + 0].Z, 1.0f);
                vertices[somecountervert + 1] = new Vector4(0.5f, 0, 1.0f, 1.0f);
                //somecountervert += 2;
               

                index1 = somecountervert + 2;
                vertices[somecountervert + 2] = new Vector4(result.Vertices[i + 1].X, result.Vertices[i + 1].Y, result.Vertices[i + 1].Z, 1.0f);
                vertices[somecountervert + 3] = new Vector4(0.5f, 0, 1.0f, 1.0f);
                //somecountervert += 2;

                index2 = somecountervert + 4;
                vertices[somecountervert + 4] = new Vector4(result.Vertices[i + 2].X, result.Vertices[i + 2].Y, result.Vertices[i + 2].Z, 1.0f);
                vertices[somecountervert + 5] = new Vector4(0.5f, 0, 1.0f, 1.0f);
                //somecountervert += 2;

                index3 = somecountervert + 6;
                vertices[somecountervert + 6] = new Vector4(result.Vertices[i + 3].X, result.Vertices[i + 3].Y, result.Vertices[i + 3].Z, 1.0f);
                vertices[somecountervert + 7] = new Vector4(0.5f, 0, 1.0f, 1.0f);
                //somecountervert += 2;





                arrayoftrigs[somecountertrig + 0] = index0;
                arrayoftrigs[somecountertrig + 1] = index1;
                arrayoftrigs[somecountertrig + 2] = index2;

                arrayoftrigs[somecountertrig + 0] = index0;
                arrayoftrigs[somecountertrig + 2] = index2;
                arrayoftrigs[somecountertrig + 3] = index3;
                /*
                arrayoftrigs[somecountertrig] = 

                somecountervert += 2;
                somecountertrig += 6;

                somecountervert += 4;
                i += 4;
            }*/

            int somecountervertex = 0;


            //vertices = new Vector4[someface.Count * 4 * 2 ];



            for (int g = 0; g < result.Groups.Count; g++)
            {
                var somegroup = result.Groups[g];

                vertices = new Vector4[somegroup.Faces.Count * 4 * 2];
                for (int f = 0; f < somegroup.Faces.Count; f++)
                {
                    var someface = somegroup.Faces[f];

                    
                    



                    for (int ff = 0; ff < someface.Count;ff++)
                    {



                        var somefaces = someface[ff];
                        var index = somefaces.VertexIndex-1;
                        Console.WriteLine(index);

                        //var testvert = new Vector4(result.Vertices[index + 0].X, result.Vertices[index + 0].Y, result.Vertices[index + 0].Z, 1.0f);

                        vertices[somecountervert + 0] = new Vector4(result.Vertices[index + 0].X, result.Vertices[index + 0].Y, result.Vertices[index + 0].Z, 1.0f);
                        vertices[somecountervert + 1] = new Vector4(0.5f, 0, 1.0f, 1.0f);
                        //somecountervert += 2;

                        /*
                        index1 = somecountervert + 2;
                        vertices[somecountervert + 2] = new Vector4(result.Vertices[index + 1].X, result.Vertices[index + 1].Y, result.Vertices[index + 1].Z, 1.0f);
                        vertices[somecountervert + 3] = new Vector4(0.5f, 0, 1.0f, 1.0f);
                        //somecountervert += 2;

                        index2 = somecountervert + 4;
                        vertices[somecountervert + 4] = new Vector4(result.Vertices[index + 2].X, result.Vertices[index + 2].Y, result.Vertices[index + 2].Z, 1.0f);
                        vertices[somecountervert + 5] = new Vector4(0.5f, 0, 1.0f, 1.0f);
                        //somecountervert += 2;

                        index3 = somecountervert + 6;
                        vertices[somecountervert + 6] = new Vector4(result.Vertices[index + 3].X, result.Vertices[index + 3].Y, result.Vertices[index + 3].Z, 1.0f);
                        vertices[somecountervert + 7] = new Vector4(0.5f, 0, 1.0f, 1.0f);
                        */

                        somecountervert +=2;
                        /*//somecountervert += 2;
                        
                        somecountervert += 4;
                        ff += 4;*/
                  
                    }
                }
            }


            //Console.WriteLine(somecountervertex);





















            
            //IndicesBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, arrayoftrigs);
            // Instantiate Vertex buiffer from vertex data 
            verticesbuffer = Buffer.Create(device, BindFlags.VertexBuffer, vertices);


            // Create Constant Buffer 
            staticContantBuffer = new Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
            dynamicConstantBuffer = new Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);
            
            // Create Constant Buffer 
            /* staticContantBufferuithread = new Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
             dynamicConstantBufferuithread = new Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

             // Create Constant Buffer 
             staticContantBuffersysthread = new Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
             dynamicConstantBuffersysthread = new Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);
            */


        }


        ~sctutorialobj()
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
                if (staticContantBuffer != null)
                {
                    staticContantBuffer.Dispose();
                    staticContantBuffer = null;
                }
                if (dynamicConstantBuffer != null)
                {
                    dynamicConstantBuffer.Dispose();
                    dynamicConstantBuffer = null;
                }

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

                if (vertices != null)
                {
                    vertices = null;
                }






                // Dispose all owned managed objects
            }

            // Release unmanaged resources
        }
    }
}
