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

namespace sccsr15forms
{
    public class tutorialfacemeshshader : IDisposable
    {

        SharpDX.Direct3D11.Buffer staticContantBuffer;//= new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
        SharpDX.Direct3D11.Buffer dynamicConstantBuffer;// = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

        //public vertforinstances somevertforinstances;
        public Vector3 position = Vector3.Zero;

        //public tutorialcubeaschunkinst.DVertex[] arrayofverts;
        //public int[] arrayoftrigs;
        //public SharpDX.Direct3D11.Buffer IndicesBuffer;
        //public Buffer staticContantBuffer;
        //public Buffer dynamicConstantBuffer;
        public InputLayout layout;
        public PixelShader pixelShader;
        public VertexShader vertexShader;
        //public Buffer verticesbuffer;
        //public Vector4[] vertices;

        // Create Constant Buffer 
        /*public Buffer staticContantBufferuithread;
        public Buffer dynamicConstantBufferuithread;

        // Create Constant Buffer 
        public Buffer staticContantBuffersysthread;//
        public Buffer dynamicConstantBuffersysthread;*/

        //public tutorialcubeaschunkinst somecubeaschunkinst;
        int facetype;
        public tutorialfacemeshshader(Device device, ShaderBytecode bytecodevert, ShaderBytecode bytecodepix)
        {
            //bytecodevert = ShaderBytecode.CompileFromFile("multicubenewovr.fx", "VS", "vs_5_0");
            //bytecodepix = ShaderBytecode.CompileFromFile("multicubenewovr.fx", "PS", "ps_5_0");
          
            // Compile Vertex and Pixel shaders 
            //var bytecode = ShaderBytecode.CompileFromFile("multicubenew.fx", "VS", "vs_5_0");
            vertexShader = new VertexShader(device, bytecodevert);

            //vertexShader = new ShaderBytecode(SharpDX.D3DCompiler.ShaderBytecode.CompileFromFile("shaders.hlsl", "VSMain", "vs_5_0"));


            InputElement[] inputElements = new InputElement[]
                {
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 1,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "COLOR",// 16
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset =InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "NORMAL", // 12
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification =InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement() // 2.2f
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "TEXCOORD", // 8
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32_Float,
                        Slot = 0,
                        AlignedByteOffset =InputElement.AppendAligned,
                        Classification =InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 1,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 2,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },

                    //instances positions
                    //instances positions
                    //instances positions
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 2,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 1,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    //instances positions
                    //instances positions
                    //instances positions

                    /*
                    //BYTEINTS maps
                    //BYTEINTS maps
                    //BYTEINTS maps                   
                    new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 3,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 2,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                     new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 4,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 2,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                       new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 5,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 2,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                         new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 6,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 2,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },



                    new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 7,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 2,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                     new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 8,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 2,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                       new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 9,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 2,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                         new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 10,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 2,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 11,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 2,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                     new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 12,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 2,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                       new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 13,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 2,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                         new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 14,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 2,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                          new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 15,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 2,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                     new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 12,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 2,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                       new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 16,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 2,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                         new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 17,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 2,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },*/
                    //BYTEINTS maps
                    //BYTEINTS maps
                    //BYTEINTS maps




















                         

                    /*
                    //WORLDMATRICES FOR BYTEINTS MAPS WIDTH/HEIGHT/DEPTH
                    //WORLDMATRICES FOR BYTEINTS MAPS WIDTH/HEIGHT/DEPTH
                    //WORLDMATRICES FOR BYTEINTS MAPS WIDTH/HEIGHT/DEPTH                 
                    new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 18,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 3,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },
                     new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 19,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 3,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },
                       new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 20,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 3,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },

                         new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 21,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 3,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },



                    new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 22,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 3,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },
                     new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 23,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 3,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },
                       new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 24,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 3,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },

                         new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 25,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 3,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },

                    new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 26,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 3,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },
                     new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 27,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 3,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },
                       new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 28,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 3,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },

                         new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 29,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 3,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },

                          new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 30,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 3,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },
                     new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 31,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 3,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },
                       new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 32,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 3,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },

                         new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 33,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 3,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },                     
                    //WORLDMATRICES FOR BYTEINTS MAPS WIDTH/HEIGHT/DEPTH
                    //WORLDMATRICES FOR BYTEINTS MAPS WIDTH/HEIGHT/DEPTH
                    //WORLDMATRICES FOR BYTEINTS MAPS WIDTH/HEIGHT/DEPTH





                         //WORLDMATRICES FOR BYTEINTS MAPS WIDTH/HEIGHT/DEPTH
                    //WORLDMATRICES FOR BYTEINTS MAPS WIDTH/HEIGHT/DEPTH
                    //WORLDMATRICES FOR BYTEINTS MAPS WIDTH/HEIGHT/DEPTH                 
                    new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 34,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 4,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },
                     new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 35,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 4,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },
                       new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 36,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 4,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },

                         new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 37,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 4,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },



                    new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 38,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 4,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },
                     new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 39,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 4,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },
                       new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 40,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 4,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },

                         new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 41,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 4,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },

                    new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 42,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 4,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },
                     new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 43,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 4,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },
                       new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 44,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 4,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },

                         new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 45,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 4,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },

                          new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 46,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 4,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },
                     new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 47,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 4,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },
                       new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 48,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 4,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },

                         new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 49,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 4,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 0
                    },                     */
                    //WORLDMATRICES FOR BYTEINTS MAPS WIDTH/HEIGHT/DEPTH
                    //WORLDMATRICES FOR BYTEINTS MAPS WIDTH/HEIGHT/DEPTH
                    //WORLDMATRICES FOR BYTEINTS MAPS WIDTH/HEIGHT/DEPTH










                    
                    //BYTEINTS maps
                    //BYTEINTS maps
                    //BYTEINTS maps
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 3,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 2,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 4,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 2,
                        AlignedByteOffset =  InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 5,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 2,
                        AlignedByteOffset =  InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 6,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 2,
                        AlignedByteOffset =  InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    //BYTEINTS maps
                    //BYTEINTS maps
                    //BYTEINTS maps








                    //WORLDMATRICES FOR BYTEINTS MAPS WIDTH/HEIGHT/DEPTH
                    //WORLDMATRICES FOR BYTEINTS MAPS WIDTH/HEIGHT/DEPTH
                    //WORLDMATRICES FOR BYTEINTS MAPS WIDTH/HEIGHT/DEPTH
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 7,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 3,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 8,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 3,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 9,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 3,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 10,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 3,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },



                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 11,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 4,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 12,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 4,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 13,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 4,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 14,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 4,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    
                    //WORLDMATRICES FOR BYTEINTS MAPS WIDTH/HEIGHT/DEPTH
                    //WORLDMATRICES FOR BYTEINTS MAPS WIDTH/HEIGHT/DEPTH
                    //WORLDMATRICES FOR BYTEINTS MAPS WIDTH/HEIGHT/DEPTH








                    //WORLDMATRIX FOR FIRST VERTEX BYTEINTS MAPS
                    //WORLDMATRIX FOR FIRST VERTEX BYTEINTS MAPS
                    //WORLDMATRIX FOR FIRST VERTEX BYTEINTS MAPS
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 15,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 5,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 16,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 5,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 17,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 5,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 18,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 5,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },







                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 19,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 6,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 20,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 6,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 21,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 6,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 22,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 6,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    //WORLDMATRIX FOR FIRST VERTEX BYTEINTS MAPS
                    //WORLDMATRIX FOR FIRST VERTEX BYTEINTS MAPS
                    //WORLDMATRIX FOR FIRST VERTEX BYTEINTS MAPS
                    

                };



            layout = new InputLayout(device, ShaderSignature.GetInputSignature(bytecodevert), inputElements);

           
          

            //bytecode.Dispose();
            //bytecode = ShaderBytecode.CompileFromFile("multicubenew.fx", "PS", "ps_5_0");
            pixelShader = new PixelShader(device, bytecodepix);
            //bytecodevert.Dispose();
            // bytecode.Dispose();
            //bytecodepix.Dispose();



            /*

            
            var bytecode = ShaderBytecode.CompileFromFile("MultiCube.fx", "VS", "vs_4_0");
            vertexShader = new VertexShader(device, bytecode);



            InputElement[] inputElements = new InputElement[]
                {
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "COLOR", // 16
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "NORMAL",// 16
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset =InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                };

            layout = new InputLayout(device, ShaderSignature.GetInputSignature(bytecode), inputElements);

            bytecode.Dispose();

            bytecode = ShaderBytecode.CompileFromFile("MultiCube.fx", "PS", "ps_4_0");
            pixelShader = new PixelShader(device, bytecode);
            bytecode.Dispose();



            

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
            };*/














            /*if (w != 0 && h != 0 && d != 0)
            {
               
            }*/

            //somevertforinstances = new vertforinstances();
            //somevertforinstances.startBuildingArray(Vector4.Zero, out arrayofverts, out arrayoftrigs, planesize, w, h, d, numberofmesh, facetype);

            //Console.WriteLine("w:" + w + "/h:" + h + "/d:" + d);









            /*if (arrayofverts != null)
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
            }*/

            staticContantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
            dynamicConstantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

        }












        public void createdataforinstances(SharpDX.Direct3D11.Device device, int indexinmain_, List<tutorialcubeaschunkinst.instancetype> instancetype, List<tutorialcubeaschunkinst.scinstanceintmaps> mapints, List<tutorialcubeaschunkinst.scinstancevertdimensions> dimensionsmapsa, List<tutorialcubeaschunkinst.scinstancevertdimensions> dimensionsmapsb, List<tutorialcubeaschunkinst.scinstancevertdimensions> firstvertloca, List<tutorialcubeaschunkinst.scinstancevertdimensions> firstvertlocb)
        {

        }


        DataStream mappedResourceLight;

        //updateSec.worldviewprobuffer[] worldViewProj




        DataStream mappedResource;



        public bool Render(SharpDX.Direct3D11.DeviceContext deviceContext, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, tutorialcubeaschunkinst somecubeaschunkinst, updatePrim.worldviewprobuffer worldViewProjbuffer, tutorialfacemesh tutorialfacemesh)
        {
            //Console.WriteLine("test00");
            if (!SetShaderParameters(deviceContext, worldMatrix, viewMatrix, projectionMatrix, texture, somecubeaschunkinst, worldViewProjbuffer))
            {
                //Console.WriteLine("test11");
                return false;
            }
               

            RenderShader(deviceContext, tutorialfacemesh);
            //Console.WriteLine("test22");
            return true;
        }

        public bool SetShaderParameters(SharpDX.Direct3D11.DeviceContext deviceContext, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, tutorialcubeaschunkinst somecubeaschunkinst, updatePrim.worldviewprobuffer worldViewProjbuffer)
        {
            try
            {
                worldMatrix.Transpose();
                viewMatrix.Transpose();
                projectionMatrix.Transpose();
               
                deviceContext.MapSubresource(dynamicConstantBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);

                worldViewProjbuffer.worldmatrix = worldMatrix;
                worldViewProjbuffer.viewmatrix = viewMatrix;
                worldViewProjbuffer.projectionmatrix = projectionMatrix;

                mappedResource.Write(worldViewProjbuffer);

                deviceContext.UnmapSubresource(dynamicConstantBuffer, 0);

                deviceContext.VertexShader.SetConstantBuffer(0, dynamicConstantBuffer);
                deviceContext.GeometryShader.SetConstantBuffer(0, dynamicConstantBuffer);

                deviceContext.PixelShader.SetShaderResource(0, null);// texture
                deviceContext.GeometryShader.SetShaderResource(0, null); // texture

                mappedResource.Dispose();

                return true;
            }
            catch
            {
                return false;
            }
        }


        public void RenderShader(SharpDX.Direct3D11.DeviceContext deviceContext, tutorialfacemesh tutorialfacemesh)
        {
            //Console.WriteLine("test0");
            if (tutorialfacemesh.arrayoftrigs != null)
            {
                //Console.WriteLine("test1");
                if (tutorialfacemesh.arrayoftrigs.Length > 0)
                {
                    //Console.WriteLine("test2");


                    if (tutorialfacemesh.writetobufferswtc == 0)
                    {

                        if (tutorialfacemesh.instances != null)
                        {


                            if (tutorialfacemesh.instances.Length > 0)
                            {

                                deviceContext.MapSubresource(tutorialfacemesh.InstanceBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                                mappedResourceLight.WriteRange(tutorialfacemesh.instances, 0, tutorialfacemesh.instances.Length);
                                deviceContext.UnmapSubresource(tutorialfacemesh.InstanceBuffer, 0);
                                mappedResourceLight.Dispose();

                                //DataStream mappedResourceLight;
                                deviceContext.MapSubresource(tutorialfacemesh.mapinstbuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                                mappedResourceLight.WriteRange(tutorialfacemesh.arrayofmapints, 0, tutorialfacemesh.arrayofmapints.Length);
                                deviceContext.UnmapSubresource(tutorialfacemesh.mapinstbuffer, 0);
                                mappedResourceLight.Dispose();

                                deviceContext.MapSubresource(tutorialfacemesh.dimensionsbuffera, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                                mappedResourceLight.WriteRange(tutorialfacemesh.arrayofdimensionsa, 0, tutorialfacemesh.arrayofdimensionsa.Length);
                                deviceContext.UnmapSubresource(tutorialfacemesh.dimensionsbuffera, 0);
                                mappedResourceLight.Dispose();

                                deviceContext.MapSubresource(tutorialfacemesh.dimensionsbufferb, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                                mappedResourceLight.WriteRange(tutorialfacemesh.arrayofdimensionsb, 0, tutorialfacemesh.arrayofdimensionsb.Length);
                                deviceContext.UnmapSubresource(tutorialfacemesh.dimensionsbufferb, 0);
                                mappedResourceLight.Dispose();

                                deviceContext.MapSubresource(tutorialfacemesh.firstvertlocbuffera, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                                mappedResourceLight.WriteRange(tutorialfacemesh.arrayoffirstvertloca, 0, tutorialfacemesh.arrayoffirstvertloca.Length);
                                deviceContext.UnmapSubresource(tutorialfacemesh.firstvertlocbuffera, 0);
                                mappedResourceLight.Dispose();

                                deviceContext.MapSubresource(tutorialfacemesh.firstvertlocbufferb, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                                mappedResourceLight.WriteRange(tutorialfacemesh.arrayoffirstvertlocb, 0, tutorialfacemesh.arrayoffirstvertlocb.Length);
                                deviceContext.UnmapSubresource(tutorialfacemesh.firstvertlocbufferb, 0);
                                mappedResourceLight.Dispose();

                                /*
                                tutorialfacemesh.arrayofdimensionsa = null;
                                tutorialfacemesh.arrayofdimensionsb = null;
                                tutorialfacemesh.arrayoffirstvertloca = null;
                                tutorialfacemesh.arrayoffirstvertlocb = null;*/


                                tutorialfacemesh.writetobufferswtc = 1;
                            }
                        }


                    }

                    /*var dataBox = deviceContext.MapSubresource(somecubeaschunkinst.dynamicConstantBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
                    Utilities.Write(dataBox.DataPointer, ref worldViewProj);
                    deviceContext.UnmapSubresource(somecubeaschunkinst.dynamicConstantBuffer, 0);
                    */
                    /*if (Program.useOculusRift == 0)
                    {
                        var dataBox = deviceContext.MapSubresource(somecubeaschunkinst.dynamicConstantBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
                        Utilities.Write(dataBox.DataPointer, ref worldViewProj);
                        deviceContext.UnmapSubresource(somecubeaschunkinst.dynamicConstantBuffer, 0);
                    }
                    else if (Program.useOculusRift == 1)
                    {
                        deviceContext.MapSubresource(somecubeaschunkinst.dynamicConstantBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                        mappedResourceLight.WriteRange(worldViewProjbuffer, 0, worldViewProjbuffer.Length);
                        deviceContext.UnmapSubresource(somecubeaschunkinst.dynamicConstantBuffer, 0);
                        mappedResourceLight.Dispose();

                        /*deviceContext.MapSubresource(somecubeaschunkinst.staticContantBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                        mappedResourceLight.WriteRange(worldViewProjbuffer, 0, worldViewProjbuffer.Length);
                        deviceContext.UnmapSubresource(somecubeaschunkinst.staticContantBuffer, 0);
                        mappedResourceLight.Dispose();
                    }*/




                    
                    if (tutorialfacemesh.instances != null )
                    {
                        if (tutorialfacemesh.instances.Length > 0)
                        {

                            deviceContext.InputAssembler.InputLayout = layout;
                            //deviceContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
                            //deviceContext.InputAssembler.SetIndexBuffer(tutorialfacemesh.IndicesBuffer, SharpDX.DXGI.Format.R32_UInt, 0);




                            /*
                            deviceContext.InputAssembler.SetVertexBuffers(0, new[]
                             {
                        new VertexBufferBinding(tutorialfacemesh.verticesbuffer,Marshal.SizeOf(typeof(tutorialcubeaschunkinst.DVertex)), 0),
                        //new VertexBufferBinding(chunkdat.indexBuffer, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceType)),0),
                    });

                            deviceContext.InputAssembler.SetVertexBuffers(1, new[]
                                    {
                        new VertexBufferBinding(tutorialfacemesh.InstanceBuffer, Marshal.SizeOf(typeof(tutorialcubeaschunkinst.instancetype)),0),
                    });


                            deviceContext.InputAssembler.SetVertexBuffers(2, new[]
                                  {
                        new VertexBufferBinding(tutorialfacemesh.mapinstbuffer, Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstanceintmaps)),0),
                    });

                            deviceContext.InputAssembler.SetVertexBuffers(3, new[]
                                  {
                        new VertexBufferBinding(tutorialfacemesh.dimensionsbuffera, Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)),0),
                    });

                            deviceContext.InputAssembler.SetVertexBuffers(4, new[]
                                  {
                        new VertexBufferBinding(tutorialfacemesh.dimensionsbufferb, Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)),0),
                    });

                            deviceContext.InputAssembler.SetVertexBuffers(5, new[]
                                  {
                        new VertexBufferBinding(tutorialfacemesh.firstvertlocbuffera, Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)),0),
                    });

                            deviceContext.InputAssembler.SetVertexBuffers(6, new[]
                                  {
                        new VertexBufferBinding(tutorialfacemesh.firstvertlocbufferb, Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)),0),
                    });
                            */

                            //deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecubeaschunk.arraychunkdatalod0[chunkindex].verticesbuffer, Utilities.SizeOf<tutorialcubeaschunk.DVertex>(), 0));
                            //deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(verticesbuffer, Utilities.SizeOf<tutorialcubeaschunkinst.DVertex>(), 0));
                            //deviceContext.VertexShader.SetConstantBuffer(0, somecubeaschunkinst.dynamicConstantBuffer);
                            //deviceContext.PixelShader.SetConstantBuffer(1, ConstantLightBuffer);
                            //deviceContext.VertexShader.SetConstantBuffer(2, someinstverticesbuffer);

                            if (Program.useOculusRift == 0)
                            {
                                deviceContext.VertexShader.Set(vertexShader);
                                //deviceContext.Rasterizer.SetViewport(0, 0, directx.D3D.SurfaceWidth, directx.D3D.SurfaceHeight);
                                deviceContext.PixelShader.Set(pixelShader);
                                //deviceContext.OutputMerger.SetTargets(directx.D3D.DepthStencilView, directx.D3D.RenderTargetView);
                                deviceContext.GeometryShader.Set(null);
                            }
                            else if (Program.useOculusRift == 1)
                            {
                                deviceContext.VertexShader.Set(vertexShader);
                                //deviceContext.Rasterizer.SetViewport(0, 0, directx.D3D.SurfaceWidth, directx.D3D.SurfaceHeight);
                                deviceContext.PixelShader.Set(pixelShader);
                                deviceContext.GeometryShader.Set(null);
                                //deviceContext.OutputMerger.SetTargets(directx.D3D.DepthStencilView, directx.D3D.RenderTargetView);
                            }

                            deviceContext.DrawIndexedInstanced(tutorialfacemesh.arrayoftrigs.Length, tutorialfacemesh.instances.Length, 0, 0, 0);
                        }
                    }
                    //Console.WriteLine("testdrawinstance");


                    //deviceContext.DrawIndexed(arrayoftrigs.Length, 0, 0);

                }
                //Console.WriteLine(arrayoftrigs.Length);
            }
        }

        ~tutorialfacemeshshader()
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

                /*if (verticesbuffer != null)
                {
                    verticesbuffer.Dispose();
                    verticesbuffer = null;
                }*/
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
