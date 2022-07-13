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
    public class tutorialfacemesh : IDisposable
    {

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
        public Vector4[] vertices;

        // Create Constant Buffer 
        /*public Buffer staticContantBufferuithread;
        public Buffer dynamicConstantBufferuithread;

        // Create Constant Buffer 
        public Buffer staticContantBuffersysthread;//
        public Buffer dynamicConstantBuffersysthread;*/

        public tutorialcubeaschunkinst somecubeaschunkinst;

        public tutorialfacemesh(Device device, int w, int h, int d, float planesize, tutorialcubeaschunkinst somecubeaschunkinst_, ShaderBytecode bytecodevert, ShaderBytecode bytecodepix)
        {
            somecubeaschunkinst = somecubeaschunkinst_;

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
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 5,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 2,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 6,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 2,
                        AlignedByteOffset =  0,
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
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 18,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 5,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },







                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 19,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 6,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 20,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 6,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 21,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 6,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 22,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 6,
                        AlignedByteOffset =   InputElement.AppendAligned,
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
            // bytecode.Dispose();




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














            if (w != 0 && h != 0 && d != 0)
            {
               
            }
            vertforinstances somevertforinstances = new vertforinstances();
            somevertforinstances.startBuildingArray(Vector4.Zero, out arrayofverts, out arrayoftrigs, planesize, w, h, d);

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
            }
        }


        tutorialcubeaschunkinst.instancetype[] instances;



        tutorialcubeaschunkinst.scinstanceintmaps[] arrayofmapints;


        tutorialcubeaschunkinst.scinstancevertdimensions[] arrayofdimensionsa;
        tutorialcubeaschunkinst.scinstancevertdimensions[] arrayofdimensionsb;
        //tutorialcubeaschunkinst.scinstancevertdimensions[] arrayofdimensionsc;
        //tutorialcubeaschunkinst.scinstancevertdimensions[] arrayofdimensionsd;

        public SharpDX.Direct3D11.Buffer dimensionsbuffera;
        public SharpDX.Direct3D11.Buffer dimensionsbufferb;
        //public SharpDX.Direct3D11.Buffer dimensionsbufferc;
        //public SharpDX.Direct3D11.Buffer dimensionsbufferd;



        tutorialcubeaschunkinst.scinstancevertdimensions[] arrayoffirstvertloca;
        tutorialcubeaschunkinst.scinstancevertdimensions[] arrayoffirstvertlocb;
        //tutorialcubeaschunkinst.scinstancevertdimensions[] arrayoffirstvertlocc;
        //tutorialcubeaschunkinst.scinstancevertdimensions[] arrayoffirstvertlocd;

        public SharpDX.Direct3D11.Buffer firstvertlocbuffera;
        public SharpDX.Direct3D11.Buffer firstvertlocbufferb;
        //public SharpDX.Direct3D11.Buffer firstvertlocbufferc;
        //public SharpDX.Direct3D11.Buffer firstvertlocbufferd;








        public SharpDX.Direct3D11.Buffer mapinstbuffer;
        public SharpDX.Direct3D11.Buffer someinstverticesbuffer;


        public SharpDX.Direct3D11.Buffer InstanceBuffer;
        int indexinmain;

        Vector4[] arrayofverticesinst;// = new Vector4[][];


        public void createinstances(SharpDX.Direct3D11.Device device, int indexinmain_, List<tutorialcubeaschunkinst.instancetype> instancetype, List<tutorialcubeaschunkinst.scinstanceintmaps> mapints, List<tutorialcubeaschunkinst.scinstancevertdimensions> dimensionsmapsa, List<tutorialcubeaschunkinst.scinstancevertdimensions> dimensionsmapsb, List<tutorialcubeaschunkinst.scinstancevertdimensions> firstvertloca, List<tutorialcubeaschunkinst.scinstancevertdimensions> firstvertlocb)
        {
            indexinmain = indexinmain_;

            /*instances = new tutorialcubeaschunkinst.instancetype[listofpositions.Count];

            for (int i = 0;i < listofpositions.Count;i++)
            {
                //+ listofverts[i][i * 4].position

                instances[i].instancePos = listofpositions[i];
            }*/
            instances = instancetype.ToArray();



            var matrixBufferDescriptionVertex00 = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Marshal.SizeOf(typeof(tutorialcubeaschunkinst.instancetype)) * instancetype.Count,
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

            arrayofmapints = mapints.ToArray();
            arrayofdimensionsa = dimensionsmapsa.ToArray();
            arrayofdimensionsb = dimensionsmapsb.ToArray();







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






            arrayoffirstvertloca = firstvertloca.ToArray();
            arrayoffirstvertlocb = firstvertlocb.ToArray();










            DataStream mappedResourceLight;
            device.ImmediateContext.MapSubresource(InstanceBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
            mappedResourceLight.WriteRange(instances, 0, instances.Length);
            device.ImmediateContext.UnmapSubresource(InstanceBuffer, 0);
            mappedResourceLight.Dispose();


            //DataStream mappedResourceLight;
            device.ImmediateContext.MapSubresource(mapinstbuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
            mappedResourceLight.WriteRange(arrayofmapints, 0, arrayofmapints.Length);
            device.ImmediateContext.UnmapSubresource(mapinstbuffer, 0);
            mappedResourceLight.Dispose();

            device.ImmediateContext.MapSubresource(dimensionsbuffera, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
            mappedResourceLight.WriteRange(arrayofdimensionsa, 0, arrayofdimensionsa.Length);
            device.ImmediateContext.UnmapSubresource(dimensionsbuffera, 0);
            mappedResourceLight.Dispose();

            device.ImmediateContext.MapSubresource(dimensionsbufferb, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
            mappedResourceLight.WriteRange(arrayofdimensionsb, 0, arrayofdimensionsb.Length);
            device.ImmediateContext.UnmapSubresource(dimensionsbufferb, 0);
            mappedResourceLight.Dispose();


            device.ImmediateContext.MapSubresource(firstvertlocbuffera, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
            mappedResourceLight.WriteRange(arrayoffirstvertloca, 0, arrayoffirstvertloca.Length);
            device.ImmediateContext.UnmapSubresource(firstvertlocbuffera, 0);
            mappedResourceLight.Dispose();

            device.ImmediateContext.MapSubresource(firstvertlocbufferb, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
            mappedResourceLight.WriteRange(arrayoffirstvertlocb, 0, arrayoffirstvertlocb.Length);
            device.ImmediateContext.UnmapSubresource(firstvertlocbufferb, 0);
            mappedResourceLight.Dispose();

        }


        public void createdataforinstances(SharpDX.Direct3D11.Device device, int indexinmain_, List<tutorialcubeaschunkinst.instancetype> instancetype, List<tutorialcubeaschunkinst.scinstanceintmaps> mapints, List<tutorialcubeaschunkinst.scinstancevertdimensions> dimensionsmapsa, List<tutorialcubeaschunkinst.scinstancevertdimensions> dimensionsmapsb, List<tutorialcubeaschunkinst.scinstancevertdimensions> firstvertloca, List<tutorialcubeaschunkinst.scinstancevertdimensions> firstvertlocb)
        {

        }





        public void render(SharpDX.Direct3D11.Device device, Matrix worldViewProj, tutorialcubeaschunkinst somecubeaschunkinst)
        {
            if (arrayoftrigs !=null)
            {
                if (arrayoftrigs.Length > 0)
                {

                    /*
                    device.ImmediateContext.MapSubresource(someinstverticesbuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                    mappedResourceLight.WriteRange(arrayofverticesinst, 0, arrayofverticesinst.Length);
                    device.ImmediateContext.UnmapSubresource(someinstverticesbuffer, 0);
                    mappedResourceLight.Dispose();*/








                    var dataBox = device.ImmediateContext.MapSubresource(somecubeaschunkinst.dynamicConstantBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
                    Utilities.Write(dataBox.DataPointer, ref worldViewProj);
                    device.ImmediateContext.UnmapSubresource(somecubeaschunkinst.dynamicConstantBuffer, 0);


                    device.ImmediateContext.InputAssembler.InputLayout = layout;
                    device.ImmediateContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
                    device.ImmediateContext.InputAssembler.SetIndexBuffer(IndicesBuffer, SharpDX.DXGI.Format.R32_UInt, 0);


                    device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
                     {
                new VertexBufferBinding(verticesbuffer,Marshal.SizeOf(typeof(tutorialcubeaschunkinst.DVertex)), 0),
                //new VertexBufferBinding(chunkdat.indexBuffer, Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceType)),0),
            });

                    device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
                    {
                new VertexBufferBinding(InstanceBuffer, Marshal.SizeOf(typeof(tutorialcubeaschunkinst.instancetype)),0),
            });


                    device.ImmediateContext.InputAssembler.SetVertexBuffers(2, new[]
                  {
                new VertexBufferBinding(mapinstbuffer, Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstanceintmaps)),0),
            });

                    device.ImmediateContext.InputAssembler.SetVertexBuffers(3, new[]
                  {
                new VertexBufferBinding(dimensionsbuffera, Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)),0),
            });

                    device.ImmediateContext.InputAssembler.SetVertexBuffers(4, new[]
                  {
                new VertexBufferBinding(dimensionsbufferb, Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)),0),
            });

                    device.ImmediateContext.InputAssembler.SetVertexBuffers(5, new[]
                  {
                new VertexBufferBinding(firstvertlocbuffera, Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)),0),
            });

                    device.ImmediateContext.InputAssembler.SetVertexBuffers(6, new[]
                  {
                new VertexBufferBinding(firstvertlocbufferb, Marshal.SizeOf(typeof(tutorialcubeaschunkinst.scinstancevertdimensions)),0),
            });


                    //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecubeaschunk.arraychunkdatalod0[chunkindex].verticesbuffer, Utilities.SizeOf<tutorialcubeaschunk.DVertex>(), 0));
                    //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(verticesbuffer, Utilities.SizeOf<tutorialcubeaschunkinst.DVertex>(), 0));
                    device.ImmediateContext.VertexShader.SetConstantBuffer(0, somecubeaschunkinst.dynamicConstantBuffer);
                    //device.ImmediateContext.PixelShader.SetConstantBuffer(1, ConstantLightBuffer);
                    //device.ImmediateContext.VertexShader.SetConstantBuffer(2, someinstverticesbuffer);


                    device.ImmediateContext.VertexShader.Set(vertexShader);
                    device.ImmediateContext.Rasterizer.SetViewport(0, 0, directx.D3D.SurfaceWidth, directx.D3D.SurfaceHeight);
                    device.ImmediateContext.PixelShader.Set(pixelShader);
                    device.ImmediateContext.OutputMerger.SetTargets(directx.D3D.DepthStencilView, directx.D3D.RenderTargetView);


                    device.ImmediateContext.DrawIndexedInstanced(arrayoftrigs.Length, instances.Length, 0, 0, 0);



                    //device.ImmediateContext.DrawIndexed(arrayoftrigs.Length, 0, 0);

                }
                //Console.WriteLine(arrayoftrigs.Length);
            }

      



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
                if (somecubeaschunkinst.staticContantBuffer != null)
                {
                    somecubeaschunkinst.staticContantBuffer.Dispose();
                    somecubeaschunkinst.staticContantBuffer = null;
                }
                if (somecubeaschunkinst.dynamicConstantBuffer != null)
                {
                    somecubeaschunkinst.dynamicConstantBuffer.Dispose();
                    somecubeaschunkinst.dynamicConstantBuffer = null;
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
