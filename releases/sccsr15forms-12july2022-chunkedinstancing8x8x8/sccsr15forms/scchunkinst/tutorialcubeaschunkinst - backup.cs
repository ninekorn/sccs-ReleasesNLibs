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
    public class tutorialcubeaschunkinst : IDisposable
    {

        int last_bit(int number)
        {
            return number - (number >> 1 << 1);
        }

        int nth_bit(int number,int position)
        {
            return last_bit(number >> position);
        }



        public SharpDX.Direct3D11.Buffer ConstantLightBuffer;

        public sccslevelgen sccslevelgen;

        //public sclevelgen sclevelgen;
        //public LevelGenerator4 levelgen;
        public sclevelgenglobals somelevelgenprimglobals;

        //public SharpDX.Direct3D11.Buffer IndicesBuffer;
        public Buffer staticContantBuffer;
        public Buffer dynamicConstantBuffer;
        public InputLayout layout;
        public PixelShader pixelShader;
        public VertexShader vertexShader;
        //public Buffer verticesbuffer;
        //public Vector4[] vertices;


        //[StructLayout(LayoutKind.Explicit, Size = 80)]
        [StructLayout(LayoutKind.Explicit)]
        public struct DVertex
        {
            [FieldOffset(0)]
            public Vector4 position;
            [FieldOffset(16)]
            public Vector4 indexPos;
            [FieldOffset(32)]
            public Vector4 color;
            [FieldOffset(48)]
            public Vector3 normal;
            [FieldOffset(60)]
            public float padding0;
            [FieldOffset(64)]
            public Vector2 tex;
            [FieldOffset(72)]
            public float padding1;
            [FieldOffset(76)]
            public float padding2;
        }


        /*

        [StructLayout(LayoutKind.Explicit)]
        public struct DVertex
        {
            [FieldOffset(0)]
            public Vector4 position;
            [FieldOffset(16)]
            public Vector4 color;
            [FieldOffset(32)]
            public Vector4 normal;
        }*/


        [StructLayout(LayoutKind.Explicit)]
        public struct DLightBuffer
        {
            [FieldOffset(0)]
            public Vector4 ambientColor;
            [FieldOffset(16)]
            public Vector4 diffuseColor;
            [FieldOffset(32)]
            public Vector3 lightDirection;
            [FieldOffset(44)]
            public float padding0; // Added extra padding so structure is a multiple of 16.
            [FieldOffset(48)]
            public Vector3 lightPosition;
            [FieldOffset(60)]
            public float padding1; // Added extra padding so structure is a multiple of 16.
        }



        public DLightBuffer[] lightBuffer = new DLightBuffer[1];

        //sclevelgenmaps[] arraychunkmapslod0;
        //sclevelgenvert[] arraychunkvertslod0;


        public chunkdata[] arraychunkdatalod0;
        public chunkdata[] arraychunkdatalod1;
        public chunkdata[] arraychunkdatalod2;
        public chunkdata[] arraychunkdatalod3;
        public chunkdata[] arraychunkdatalod4;

        int[] arrayofindexes;
        public tutorialcubeaschunkinst(Device device)
        {
            // Compile Vertex and Pixel shaders 
            var bytecode = ShaderBytecode.CompileFromFile("MultiCube.fx", "VS", "vs_5_0");
            vertexShader = new VertexShader(device, bytecode);

            // Layout from VertexShader input signature 
            /*layout = new InputLayout(device, ShaderSignature.GetInputSignature(bytecode), new[]
            {
                new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0),
                new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 32, 0)
            });
            bytecode.Dispose();*/


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




                    //int maps
                    //int maps
                    //int maps
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
                    //int maps
                    //int maps
                    //int maps









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
                    //WORLDMATRICES FOR BYTE MAPS
                    //WORLDMATRICES FOR BYTE MAPS
                    //WORLDMATRICES FOR BYTE MAPS





                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 23,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 7,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 24,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 7,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 25,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 7,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 26,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 7,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },  
                    //WORLDMATRICES FOR BYTE MAPS
                    //WORLDMATRICES FOR BYTE MAPS
                    //WORLDMATRICES FOR BYTE MAPS





                    

        
                  

                };

            layout = new InputLayout(device, ShaderSignature.GetInputSignature(bytecode), inputElements);

            bytecode.Dispose();








            bytecode = ShaderBytecode.CompileFromFile("MultiCube.fx", "PS", "ps_5_0");
            pixelShader = new PixelShader(device, bytecode);
            bytecode.Dispose();




            Vector4 ambientColor = new Vector4(0.4f, 0.4f, 0.4f, 1.0f);
            Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
            Vector4 lightDirection = new Vector4(1, 0, 0, 1.0f);
            Vector3 lightpos0 = new Vector3(0, 1, 0);
            Vector3 dirLight0 = new Vector3(0, -1, 0);




            lightBuffer[0] = new DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight0,
                padding0 = 100,
                lightPosition = lightpos0,
                padding1 = 0,
                //padding1 = 100
            };


            BufferDescription lightBufferDesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Utilities.SizeOf<DLightBuffer>(),
                BindFlags = BindFlags.ConstantBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };


           ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);












            //VOXEL VIRTUAL DESKTOP
            //VOXEL VIRTUAL DESKTOP
            //VOXEL VIRTUAL DESKTOP
            somelevelgenprimglobals = new sclevelgenglobals();

            somelevelgenprimglobals.planeSize = 0.005f; // * 10

            /*
            somelevelgenprimglobals.widthlod0 = 7;
            somelevelgenprimglobals.heightlod0 = 7;
            somelevelgenprimglobals.depthlod0 = 7;

            somelevelgenprimglobals.widthlod1 = 3;
            somelevelgenprimglobals.heightlod1 = 3;
            somelevelgenprimglobals.depthlod1 = 3;

            somelevelgenprimglobals.widthlod2 = 2;
            somelevelgenprimglobals.heightlod2 = 2;
            somelevelgenprimglobals.depthlod2 = 2;

            somelevelgenprimglobals.widthlod3 = 1;
            somelevelgenprimglobals.heightlod3 = 1;
            somelevelgenprimglobals.depthlod3 = 1;*/

            /*
            somelevelgenprimglobals.widthlod0 = 6;
            somelevelgenprimglobals.heightlod0 = 6;
            somelevelgenprimglobals.depthlod0 = 6;

            somelevelgenprimglobals.widthlod1 = 3;
            somelevelgenprimglobals.heightlod1 = 3;
            somelevelgenprimglobals.depthlod1 = 3;

            somelevelgenprimglobals.widthlod2 = 2;
            somelevelgenprimglobals.heightlod2 = 2;
            somelevelgenprimglobals.depthlod2 = 2;

            somelevelgenprimglobals.widthlod3 = 1;
            somelevelgenprimglobals.heightlod3 = 1;
            somelevelgenprimglobals.depthlod3 = 1;*/


            /*somelevelgenprimglobals.widthlod0 = 20;
            somelevelgenprimglobals.heightlod0 = 20;
            somelevelgenprimglobals.depthlod0 = 20;

            somelevelgenprimglobals.widthlod1 = 10;
            somelevelgenprimglobals.heightlod1 = 10;
            somelevelgenprimglobals.depthlod1 = 10;

            somelevelgenprimglobals.widthlod2 = 6;
            somelevelgenprimglobals.heightlod2 = 6;
            somelevelgenprimglobals.depthlod2 = 6;

            somelevelgenprimglobals.widthlod3 = 3;
            somelevelgenprimglobals.heightlod3 = 3;
            somelevelgenprimglobals.depthlod3 = 3;

            somelevelgenprimglobals.widthlod4 = 1;
            somelevelgenprimglobals.heightlod4 = 1;
            somelevelgenprimglobals.depthlod4 = 1;*/

            /*
            somelevelgenprimglobals.widthlod0 = 10;
            somelevelgenprimglobals.heightlod0 = 10;
            somelevelgenprimglobals.depthlod0 = 10;

            somelevelgenprimglobals.widthlod1 = 5;
            somelevelgenprimglobals.heightlod1 = 5;
            somelevelgenprimglobals.depthlod1 = 5;

            somelevelgenprimglobals.widthlod2 = 3;
            somelevelgenprimglobals.heightlod2 = 3;
            somelevelgenprimglobals.depthlod2 = 3;

            somelevelgenprimglobals.widthlod3 = 2;
            somelevelgenprimglobals.heightlod3 = 2;
            somelevelgenprimglobals.depthlod3 = 2;

            somelevelgenprimglobals.widthlod4 = 1;
            somelevelgenprimglobals.heightlod4 = 1;
            somelevelgenprimglobals.depthlod4 = 1;*/

            somelevelgenprimglobals.widthlod0 = 4;
            somelevelgenprimglobals.heightlod0 = 4;
            somelevelgenprimglobals.depthlod0 = 4;



            //6w*6h*6d * 1 face per byte since we are building the faces in individual meshes here. = 216 * 2 = 432 digits needed for inserting the width and depth for the top face.

            //4w*4h*4d * 1 face per byte since we are building the faces in individual meshes here. = 64 * 2 = 128 digits needed for inserting the width and depth for the top face.
            //64 digits needed for each bytes that will give the index for the start of the first vertex of each face.

            //4 * 4 * 4 = 64 bytes total
            //minimum of 128 bytes to cover witdh and depth of faces//
            //we are going to start with the top face.


            //4 * 8 * 10 == 320
            //10*4*4 = 160 max chunk bytes included in 4 floats x 4 coordinates per 1 worldmatrix => 2 worldmatrices = 320
            //10*4*4*4 for 4 worldmatrices = 640 / 2 for width and height of the specific vertex set to 1 in the 5th/6th worldmatrix.

            //Not gonna work
            //8*6*10 == 480 for 3 vectors...
            //max chunk value for that is 4*5*8
            //int somefloat = nth_bit(11111011, 6);// (int)Math.Truncate(0111111111111111.1111111111111111f);
            //double somefloat = 11111111.011111111d;// - 11111111.0d;////nth_bit(11111011, 14);// (int)Math.Truncate(0111111111111111.1111111111111111f);

            //int somefloat = 1111111111; 
            //int somefloat = 4294967295;
            float somefloat = 9876543210f; //1111111111
            //double somefloatright = somefloat - (double)Math.Floor(somefloat);

            int someindex = 7;
            //var someData0 = somefloat;

            for (int i = 0; i < someindex; i++)
            {
                somefloat = (somefloat * 0.1f);
            }

            somefloat = somefloat - (float)Math.Floor(somefloat);
            somefloat = (float)Math.Floor(somefloat * 10.0f);
            //somefloat = (float)(somefloat * 1000000.0f);// - (float)Math.Truncate(somefloat * 1000000.0f);

            /*var before0 = (int)((somefloat));
            //https://stackoverflow.com/questions/46312893/how-do-you-use-bit-shift-operators-to-find-out-a-certain-digit-of-a-number-in-ba
            var someremains = before0 >> 1 << 1;
            var currentByte = before0 - someremains;*/

            //Console.WriteLine("byte: " + currentByte + " " + somefloat); // + " " + somefloatright.ToString()

            Console.WriteLine("byte: " + " " + somefloat); // + " " + somefloatright.ToString()

            //int somenthbit = nth_bit((int)somefloat, 0);
            //Console.WriteLine("byte: " + somefloat);

            /*
            uint someleft = (uint)(11111111);
            uint someright = (uint)(01111111);

            //BitConverter.

            byte[] array = BitConverter.GetBytes(somefloat);
            var someres = BitConverter.ToDouble(array, 0);

            Console.WriteLine(someres);*/


            //uint sometest = 0xFFFFFFFF;
            //Console.WriteLine(sometest);
            //4294967295











            /*levelgen = new LevelGenerator4();
            //adding more tiles means the level will be bigger
            levelgen.tileAmount = 250; //10500 //15000lag
            //15000lag => my computer specs 960gtx 2gb + Amd Ryzen 2600 + 8gb ram 
            //levelgen.chunkwidth = 1;
            //levelgen.chunkheight = 1;
            //levelgen.chunkdepth = 1;
            levelgen.planesize = 1.0f;
            levelgen.chanceUp = 0.25f;
            levelgen.chanceRight = 0.5f;
            levelgen.chanceDown = 0.75f;
            levelgen.SpawnerMoveWaitTime = 0.00000000001f;
            levelgen.BuildingWaitTime = 0.00000000001f;
            //levelgen.blockSize = 1.0f;
            levelgen.StartGeneratingVoxelLevel();*/

            //
            //sclevelgen = new sclevelgen();
            //sclevelgen.StartGeneratingVoxelLevel();

            sccslevelgen = new sccslevelgen();
            sccslevelgen.createlevel();


            int totaltilescounter = 0;

            int sometotalmaplength = sccslevelgen.somewidth * sccslevelgen.someheight * sccslevelgen.somedepth;

            for (var x = sccslevelgen.minx; x < sccslevelgen.maxx; x++)
            {
                for (var y = sccslevelgen.miny; y < sccslevelgen.maxy; y++)
                {
                    for (var z = sccslevelgen.minz; z < sccslevelgen.maxz; z++)
                    {
                        int xx = x;
                        int yy = y;
                        int zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = xx + (sccslevelgen.maxx - 1);
                        }

                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = yy + (sccslevelgen.maxy - 1);
                        }
                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = zz + (sccslevelgen.maxz - 1);
                        }

                        int indexinlevelarray = xx + sccslevelgen.somewidth * (yy + sccslevelgen.someheight * zz); //y is always 0 on floor tiles


                        if (indexinlevelarray < sccslevelgen.somewidth * sccslevelgen.someheight * sccslevelgen.somedepth)
                        {

                            int typeofterraintile = sccslevelgen.levelmap[indexinlevelarray];

                            if (typeofterraintile == 0 ||
                                typeofterraintile == 1101 ||
                                typeofterraintile == 1102 ||
                                typeofterraintile == 1103 ||
                                typeofterraintile == 1104 ||
                                typeofterraintile == 1105 ||
                                typeofterraintile == 1106 ||
                                typeofterraintile == 1107 ||
                                typeofterraintile == 1108 ||
                                typeofterraintile == 1109 ||
                                typeofterraintile == 1110 ||
                                typeofterraintile == 1111 ||
                                typeofterraintile == 1112 ||
                                typeofterraintile == -99 ||
                                typeofterraintile == 1115)
                            {
                                totaltilescounter++;
                            }
                        }
                    }
                }
            }

            //Console.WriteLine("originallength:" + sometotalmaplength + "/newlength:" + totaltilescounter);


            arrayofindexes = new int[sccslevelgen.levelmap.Length];
            //int[][] arrayofpositions = new int[sccslevelgen.levelmap.Length][];





            //arraychunkmapslod0 = new sclevelgenmaps[levelgen.typeoftiles.Count];
            //arraychunkvertslod0 = new sclevelgenvert[levelgen.typeoftiles.Count];
            arraychunkdatalod0 = new chunkdata[totaltilescounter];
            arraychunkdatalod1 = new chunkdata[totaltilescounter];
            arraychunkdatalod2 = new chunkdata[totaltilescounter];
            arraychunkdatalod3 = new chunkdata[totaltilescounter];
            arraychunkdatalod4 = new chunkdata[totaltilescounter];


            int[] somemap;
            Vector4[] arrayofverts;
            int[] arrayoftrigs;

            int facetype = 0;
            //List<Vector4> listofverts = new List<Vector4>();
            //List<int> listoftrigs = new List<int>();

            for (int i = 0; i < arrayofindexes.Length; i++)
            {
                arrayofindexes[i] = -1;
            }




            int somenewcounter = 0;
            for (var x = sccslevelgen.minx; x < sccslevelgen.maxx; x++)
            {
                for (var y = sccslevelgen.miny; y < sccslevelgen.maxy; y++)
                {
                    for (var z = sccslevelgen.minz; z < sccslevelgen.maxz; z++)
                    {
                        int xx = x;
                        int yy = y;
                        int zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = xx + (sccslevelgen.maxx - 1);
                        }

                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = yy + (sccslevelgen.maxy - 1);
                        }
                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = zz + (sccslevelgen.maxz - 1);
                        }

                        int indexinlevelarray = xx + sccslevelgen.somewidth * (yy + sccslevelgen.someheight * zz); //y is always 0 on floor tiles



                        if (indexinlevelarray < sccslevelgen.somewidth * sccslevelgen.someheight * sccslevelgen.somedepth)
                        {



                            int chunkposx = x;
                            int chunkposy = y;
                            int chunkposz = z;

                            int typeofterraintile = sccslevelgen.levelmap[indexinlevelarray];


                            float[] newchunkpos = new float[3];

                            newchunkpos[0] = (float)chunkposx;
                            newchunkpos[1] = (float)chunkposy;
                            newchunkpos[2] = (float)chunkposz;

                            newchunkpos[0] = newchunkpos[0] * (somelevelgenprimglobals.widthlod0 * somelevelgenprimglobals.planeSize);
                            newchunkpos[1] = newchunkpos[1] * (somelevelgenprimglobals.heightlod0 * somelevelgenprimglobals.planeSize);
                            newchunkpos[2] = newchunkpos[2] * (somelevelgenprimglobals.depthlod0 * somelevelgenprimglobals.planeSize);



                            if (typeofterraintile == 0 ||
                                typeofterraintile == 1101 ||
                                typeofterraintile == 1102 ||
                                typeofterraintile == 1103 ||
                                typeofterraintile == 1104 ||
                                typeofterraintile == 1105 ||
                                typeofterraintile == 1106 ||
                                typeofterraintile == 1107 ||
                                typeofterraintile == 1108 ||
                                typeofterraintile == 1109 ||
                                typeofterraintile == 1110 ||
                                typeofterraintile == 1111 ||
                                typeofterraintile == 1112 ||
                                typeofterraintile == -99 ||
                                typeofterraintile == 1115)
                            {
                                arrayofindexes[indexinlevelarray] = somenewcounter;

                                //lod1
                                arraychunkdatalod0[somenewcounter] = new chunkdata();
                                //arraychunkdatalod0[somenewcounter].arraychunkmapslod0 = new sclevelgenmaps();
                                //somemap = arraychunkdatalod0[somenewcounter].arraychunkmapslod0.buildchunkmaps(newchunkpos, somelevelgenprimglobals.planeSize, chunkPos, typeofterraintile, this, 1);
                                arraychunkdatalod0[somenewcounter].arraychunkvertslod0 = new tutorialchunkcubemap(chunkposx, chunkposy, chunkposz, newchunkpos);
                                arraychunkdatalod0[somenewcounter].realpos = newchunkpos;
                                arraychunkdatalod0[somenewcounter].chunkPos = new int[] { chunkposx, chunkposy, chunkposz };
                                //arraychunkdatalod0[somenewcounter].arraychunkvertslod0.buildchunkmaps(typeofterraintile, this, 1);

                                double m11a = 0;
                                double m12a = 0;
                                double m13a = 0;
                                double m14a = 0;
                                double m21a = 0;
                                double m22a = 0;
                                double m23a = 0;
                                double m24a = 0;
                                double m31a = 0;
                                double m32a = 0;
                                double m33a = 0;
                                double m34a = 0;
                                double m41a = 0;
                                double m42a = 0;
                                double m43a = 0;
                                double m44a = 0;

                                /*double m11b = 0;
                                double m12b = 0;
                                double m13b = 0;
                                double m14b = 0;
                                double m21b = 0;
                                double m22b = 0;
                                double m23b = 0;
                                double m24b = 0;
                                double m31b = 0;
                                double m32b = 0;
                                double m33b = 0;
                                double m34b = 0;
                                double m41b = 0;
                                double m42b = 0;
                                double m43b = 0;
                                double m44b = 0;

                                double m11c = 0;
                                double m12c = 0;
                                double m13c = 0;
                                double m14c = 0;
                                double m21c = 0;
                                double m22c = 0;
                                double m23c = 0;
                                double m24c = 0;
                                double m31c = 0;
                                double m32c = 0;
                                double m33c = 0;
                                double m34c = 0;
                                double m41c = 0;
                                double m42c = 0;
                                double m43c = 0;
                                double m44c = 0;

                                double m11d = 0;
                                double m12d = 0;
                                double m13d = 0;
                                double m14d = 0;
                                double m21d = 0;
                                double m22d = 0;
                                double m23d = 0;
                                double m24d = 0;
                                double m31d = 0;
                                double m32d = 0;
                                double m33d = 0;
                                double m34d = 0;
                                double m41d = 0;
                                double m42d = 0;
                                double m43d = 0;
                                double m44d = 0;*/


                                //arraychunkdatalod0[somenewcounter].arraychunkvertslod0.buildchunkmaps(typeofterraintile, this, 1);

                                //int chunkposx = 0;
                                //int chunkposy = 0; 
                                //int chunkposz = 0;
                                //float[] newchunkpos = new float[3];

                                //int typeofterraintile = 0;
                                arraychunkdatalod0[somenewcounter].arraychunkvertslod0 = new tutorialchunkcubemap(chunkposx, chunkposy, chunkposz, newchunkpos);
                                arraychunkdatalod0[somenewcounter].arraychunkvertslod0.buildchunkmaps(typeofterraintile, this, 1,
                                                            out m11a, out m12a, out m13a, out m14a, out m21a, out m22a, out m23a, out m24a, out m31a, out m32a, out m33a, out m34a, out m41a, out m42a, out m43a, out m44a); //, somechunkkeyboardpriminstanceindex_, chunkprimindex_, chunkinstindex






                                //arraychunkdatalod0[somenewcounter].indexinlevelgenmap = indexoftile;
                                arraychunkdatalod0[somenewcounter].indexintypeoftiles = indexinlevelarray;
                                arraychunkdatalod0[somenewcounter].X = chunkposx;
                                arraychunkdatalod0[somenewcounter].Y = chunkposy;
                                arraychunkdatalod0[somenewcounter].Z = chunkposz;
                                //arraychunkdatalod0[somenewcounter].arraychunkvertslod0.chunkPos = chunkPos;
                                //arraychunkdatalod0[somenewcounter].arraychunkvertslod0.newchunkpos = newchunkpos;
                                //arraychunkdatalod0[somenewcounter].arraychunkvertslod0.map = somemap;
                                arraychunkdatalod0[somenewcounter].typeofterraintile = typeofterraintile;
                                arraychunkdatalod0[somenewcounter].arraychunkvertslod0.X = chunkposx;
                                arraychunkdatalod0[somenewcounter].arraychunkvertslod0.Y = chunkposy;
                                arraychunkdatalod0[somenewcounter].arraychunkvertslod0.Z = chunkposz;


                                somenewcounter++;
                            }
                        }
                    }
                }
            }






            staticContantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
            dynamicConstantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);



            somenewcounter = 0;

            for (var x = sccslevelgen.minx; x < sccslevelgen.maxx; x++)
            {
                for (var y = sccslevelgen.miny; y < sccslevelgen.maxy; y++)
                {
                    for (var z = sccslevelgen.minz; z < sccslevelgen.maxz; z++)
                    {
                        int xx = x;
                        int yy = y;
                        int zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = xx + (sccslevelgen.maxx - 1);
                        }

                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = yy + (sccslevelgen.maxy - 1);
                        }
                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = zz + (sccslevelgen.maxz - 1);
                        }

                        int indexinlevelarray = xx + sccslevelgen.somewidth * (yy + sccslevelgen.someheight * zz); //y is always 0 on floor tiles


                        if (indexinlevelarray < sccslevelgen.somewidth * sccslevelgen.someheight * sccslevelgen.somedepth)
                        {
                            int chunkposx = x;
                            int chunkposy = y;
                            int chunkposz = z;

                            int typeofterraintile = sccslevelgen.levelmap[indexinlevelarray];

                            facetype = 0;

                            if (typeofterraintile == 0 ||
                               typeofterraintile == 1101 ||
                               typeofterraintile == 1102 ||
                               typeofterraintile == 1103 ||
                               typeofterraintile == 1104 ||
                               typeofterraintile == 1105 ||
                               typeofterraintile == 1106 ||
                               typeofterraintile == 1107 ||
                               typeofterraintile == 1108 ||
                               typeofterraintile == 1109 ||
                               typeofterraintile == 1110 ||
                               typeofterraintile == 1111 ||
                               typeofterraintile == 1112 ||
                                typeofterraintile == -99 ||
                                typeofterraintile == 1115)
                            {

                                //lod1
                                arraychunkdatalod0[somenewcounter].arraychunkvertslod0.startBuildingArray(facetype, this, 1);
                                //Console.WriteLine(arraychunkdatalod0[somenewcounter].arraychunkvertslod0.chunkPos);
                                if (arraychunkdatalod0[somenewcounter].arraychunkvertslod0.arrayofverts.Length > 0)
                                {
                                    // Create Constant Buffer 
                                    //staticContantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
                                    //dynamicConstantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

                                    arraychunkdatalod0[somenewcounter].verticesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, arraychunkdatalod0[somenewcounter].arraychunkvertslod0.arrayofverts);
                                    arraychunkdatalod0[somenewcounter].indicesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, arraychunkdatalod0[somenewcounter].arraychunkvertslod0.arrayoftrigs);
                                    arraychunkdatalod0[somenewcounter].staticContantBuffer = staticContantBuffer;
                                    arraychunkdatalod0[somenewcounter].dynamicConstantBuffer = dynamicConstantBuffer;

                                    arraychunkdatalod0[somenewcounter].vertexShader = vertexShader;
                                    arraychunkdatalod0[somenewcounter].pixelShader = pixelShader;
                                    arraychunkdatalod0[somenewcounter].layout = layout;










                                    double m11a = 0;
                                    double m12a = 0;
                                    double m13a = 0;
                                    double m14a = 0;
                                    double m21a = 0;
                                    double m22a = 0;
                                    double m23a = 0;
                                    double m24a = 0;
                                    double m31a = 0;
                                    double m32a = 0;
                                    double m33a = 0;
                                    double m34a = 0;
                                    double m41a = 0;
                                    double m42a = 0;
                                    double m43a = 0;
                                    double m44a = 0;

                                    double m11b = 0;
                                    double m12b = 0;
                                    double m13b = 0;
                                    double m14b = 0;
                                    double m21b = 0;
                                    double m22b = 0;
                                    double m23b = 0;
                                    double m24b = 0;
                                    double m31b = 0;
                                    double m32b = 0;
                                    double m33b = 0;
                                    double m34b = 0;
                                    double m41b = 0;
                                    double m42b = 0;
                                    double m43b = 0;
                                    double m44b = 0;

                                    double m11c = 0;
                                    double m12c = 0;
                                    double m13c = 0;
                                    double m14c = 0;
                                    double m21c = 0;
                                    double m22c = 0;
                                    double m23c = 0;
                                    double m24c = 0;
                                    double m31c = 0;
                                    double m32c = 0;
                                    double m33c = 0;
                                    double m34c = 0;
                                    double m41c = 0;
                                    double m42c = 0;
                                    double m43c = 0;
                                    double m44c = 0;

                                    double m11d = 0;
                                    double m12d = 0;
                                    double m13d = 0;
                                    double m14d = 0;
                                    double m21d = 0;
                                    double m22d = 0;
                                    double m23d = 0;
                                    double m24d = 0;
                                    double m31d = 0;
                                    double m32d = 0;
                                    double m33d = 0;
                                    double m34d = 0;
                                    double m41d = 0;
                                    double m42d = 0;
                                    double m43d = 0;
                                    double m44d = 0;


                                    //arraychunkdatalod0[somenewcounter].arraychunkvertslod0.buildchunkmaps(typeofterraintile, this, 1);

                                    //int chunkposx = 0;
                                    //int chunkposy = 0; 
                                    //int chunkposz = 0;
                                    //float[] newchunkpos = new float[3];

                                    //int typeofterraintile = 0;
                                    arraychunkdatalod0[somenewcounter].arraychunkvertslod0.insertdimensionsinint(typeofterraintile, this, 1, 8,
                                    out m11a, out m12a, out m13a, out m14a, out m21a, out m22a, out m23a, out m24a, out m31a, out m32a, out m33a, out m34a, out m41a, out m42a, out m43a, out m44a,
                                    out m11b, out m12b, out m13b, out m14b, out m21b, out m22b, out m23b, out m24b, out m31b, out m32b, out m33b, out m34b, out m41b, out m42b, out m43b, out m44b,
                                    out m11c, out m12c, out m13c, out m14c, out m21c, out m22c, out m23c, out m24c, out m31c, out m32c, out m33c, out m34c, out m41c, out m42c, out m43c, out m44c,
                                    out m11d, out m12d, out m13d, out m14d, out m21d, out m22d, out m23d, out m24d, out m31d, out m32d, out m33d, out m34d, out m41d, out m42d, out m43d, out m44d); //, somechunkkeyboardpriminstanceindex_, chunkprimindex_, chunkinstindex






                                   

                                    Matrix somechunkmap = Matrix.Identity;

                                    somechunkmap.M11 = (float)m11a;
                                    somechunkmap.M12 = (float)m12a;
                                    somechunkmap.M13 = (float)m13a;
                                    somechunkmap.M14 = (float)m14a;

                                    somechunkmap.M21 = (float)m21a;
                                    somechunkmap.M22 = (float)m22a;
                                    somechunkmap.M23 = (float)m23a;
                                    somechunkmap.M24 = (float)m24a;

                                    somechunkmap.M31 = (float)m31a;
                                    somechunkmap.M32 = (float)m32a;
                                    somechunkmap.M33 = (float)m33a;
                                    somechunkmap.M34 = (float)m34a;

                                    somechunkmap.M41 = (float)m41a;
                                    somechunkmap.M42 = (float)m42a;
                                    somechunkmap.M43 = (float)m43a;
                                    somechunkmap.M44 = (float)m44a;

                                    arraychunkdatalod0[somenewcounter].instancesmatrixa = new scinstancevertdimensions()
                                    {
                                        instancematrix = somechunkmap,
                                    };

                                    somechunkmap = Matrix.Identity;

                                    somechunkmap.M11 = (float)m11b;
                                    somechunkmap.M12 = (float)m12b;
                                    somechunkmap.M13 = (float)m13b;
                                    somechunkmap.M14 = (float)m14b;

                                    somechunkmap.M21 = (float)m21b;
                                    somechunkmap.M22 = (float)m22b;
                                    somechunkmap.M23 = (float)m23b;
                                    somechunkmap.M24 = (float)m24b;

                                    somechunkmap.M31 = (float)m31b;
                                    somechunkmap.M32 = (float)m32b;
                                    somechunkmap.M33 = (float)m33b;
                                    somechunkmap.M34 = (float)m34b;

                                    somechunkmap.M41 = (float)m41b;
                                    somechunkmap.M42 = (float)m42b;
                                    somechunkmap.M43 = (float)m43b;
                                    somechunkmap.M44 = (float)m44b;

                                    arraychunkdatalod0[somenewcounter].instancesmatrixb = new scinstancevertdimensions()
                                    {
                                        instancematrix = somechunkmap,
                                    };

                                    somechunkmap = Matrix.Identity;

                                    somechunkmap.M11 = (float)m11c;
                                    somechunkmap.M12 = (float)m12c;
                                    somechunkmap.M13 = (float)m13c;
                                    somechunkmap.M14 = (float)m14c;

                                    somechunkmap.M21 = (float)m21c;
                                    somechunkmap.M22 = (float)m22c;
                                    somechunkmap.M23 = (float)m23c;
                                    somechunkmap.M24 = (float)m24c;

                                    somechunkmap.M31 = (float)m31c;
                                    somechunkmap.M32 = (float)m32c;
                                    somechunkmap.M33 = (float)m33c;
                                    somechunkmap.M34 = (float)m34c;

                                    somechunkmap.M41 = (float)m41c;
                                    somechunkmap.M42 = (float)m42c;
                                    somechunkmap.M43 = (float)m43c;
                                    somechunkmap.M44 = (float)m44c;


                                    arraychunkdatalod0[somenewcounter].instancesmatrixc = new scinstancevertdimensions()
                                    {
                                        instancematrix = somechunkmap,
                                    };

                                    somechunkmap = Matrix.Identity;

                                    somechunkmap.M11 = (float)m11d;
                                    somechunkmap.M12 = (float)m12d;
                                    somechunkmap.M13 = (float)m13d;
                                    somechunkmap.M14 = (float)m14d;

                                    somechunkmap.M21 = (float)m21d;
                                    somechunkmap.M22 = (float)m22d;
                                    somechunkmap.M23 = (float)m23d;
                                    somechunkmap.M24 = (float)m24d;

                                    somechunkmap.M31 = (float)m31d;
                                    somechunkmap.M32 = (float)m32d;
                                    somechunkmap.M33 = (float)m33d;
                                    somechunkmap.M34 = (float)m34d;

                                    somechunkmap.M41 = (float)m41d;
                                    somechunkmap.M42 = (float)m42d;
                                    somechunkmap.M43 = (float)m43d;
                                    somechunkmap.M44 = (float)m44d;

                                    arraychunkdatalod0[somenewcounter].instancesmatrixd = new scinstancevertdimensions()
                                    {
                                        instancematrix = somechunkmap,
                                    };



                                    /*
                                    var matrixBufferDescriptionVertex = new BufferDescription()
                                    {
                                        Usage = ResourceUsage.Dynamic,
                                        SizeInBytes = Marshal.SizeOf(typeof(scinstancevertdimensions)) * arraychunkdatalod0[somenewcounter].instancesmatrixa.Length,
                                        BindFlags = BindFlags.VertexBuffer,
                                        CpuAccessFlags = CpuAccessFlags.Write,
                                        OptionFlags = ResourceOptionFlags.None,
                                        StructureByteStride = 0
                                    };

                                    var instancesmatrixbuffer = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescriptionVertex);*/






                                }
                                somenewcounter++;
                            }
                        }
                    }
                }
            }





            int maximuminstances = 4 * 4 * 4 * 4;


            for (int i = 0; i < maximuminstances; i++)
            {

            }















            //maybe calculate the different number of instances needed?































































            /*
            //unLOADING CHUNK to XML
            //unLOADING CHUNK to XML
            string pathofrelease = Directory.GetCurrentDirectory();
            //Console.WriteLine(pathofrelease);
            string pathofchunkmap = pathofrelease + @"\chunkmaps\";

            if (!Directory.Exists(pathofchunkmap))
            {
                //Console.WriteLine("created directory");
                Directory.CreateDirectory(pathofchunkmap);
            }

            //int writetofilecounter = 0;

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            var path = pathofchunkmap + @"\levelgenbytemap" + ".xml";

            var writer = new XmlTextWriter(path, System.Text.Encoding.UTF8);

            writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;

            writer.WriteStartElement("root"); // open 0

            for (int i = 0; i < arrayofchunkslod0.Length; i++)
            {                             
                //writer.WriteStartElement("bytemap"); //open 4
                writer.WriteStartElement("x" + arrayofchunkslod0[i].chunkPos.X + "y" + arrayofchunkslod0[i].chunkPos.Y + "z" + arrayofchunkslod0[i].chunkPos.Z); //open 4

                int[] somemapp = arrayofchunksmapslod0[i].map;//
                writer.WriteValue(somemapp);
                //writer.WriteEndElement(); //close 4    
                writer.WriteEndElement(); //close 4                    
            }
            //unLOADING CHUNK to XML
            //unLOADING CHUNK to XML
            writer.WriteEndElement(); //close 2
            writer.Close();*/






            //LOADING CHUNK BACK INTO MEMORY
            //LOADING CHUNK BACK INTO MEMORY
            /*
            //writetofilecounter = 0;
            for (int i = 0; i < arrayofchunks.Length; i++)
            {
                //https://stackoverflow.com/questions/18891207/how-to-get-value-from-a-specific-child-element-in-xml-using-xmlreader
                //var path = @"C:\Users\steve\Desktop\#chunkmaps\" + "chunkmap" + writetofilecounter + ".xml";
                var reader = new XmlTextReader(path);

                if (reader.ReadToDescendant("x" + arrayofchunks[i].chunkPos.X + "y" + arrayofchunks[i].chunkPos.X + "z" + arrayofchunks[i].chunkPos.Z))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    int[] ia = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
            }*/
            //LOADING CHUNK BACK INTO MEMORY
            //LOADING CHUNK BACK INTO MEMORY





            /*
            float x0 = (float)(Math.Round(10.1f / 10.0f) * 10.0f);
            float y0 = (float)(Math.Round(10.1f / 10.0f) * 10.0f);
            float z0 = (float)(Math.Round(10.1f / 10.0f) * 10.0f);


            Console.WriteLine(x0);*/
        }





        public struct scinstanceintmaps
        {
            public Matrix instanceintmap;
        };


        public struct scinstancevertdimensions
        {
            public Matrix instancematrix;
            public Matrix instancematrixb;
            public Matrix instancematrixc;
            public Matrix instancematrixd;
        };

        public struct chunkdata
        {
            public SharpDX.Direct3D11.Buffer indicesbuffer;
            public SharpDX.Direct3D11.Buffer verticesbuffer;
            public SharpDX.Direct3D11.Buffer staticContantBuffer;
            public SharpDX.Direct3D11.Buffer dynamicConstantBuffer;
            //public sclevelgenmaps arraychunkmapslod0;
            public tutorialchunkcubemap arraychunkvertslod0;

            //public sclevelgenmaps arraychunkmapslod1;
            //public sclevelgenvert arraychunkvertslod1;

            //public sclevelgenmaps arraychunkmapslod2;
            //public sclevelgenvert arraychunkvertslod2;

            //public sclevelgenmaps arraychunkmapslod3;
            //public sclevelgenvert arraychunkvertslod3;

            //public sclevelgenmaps arraychunkmapslod4;
            //public sclevelgenvert arraychunkvertslod4;
            //public Vector4[] arrayofverts;
            //public int[] arrayoftrigs;
            public PixelShader pixelShader;
            public VertexShader vertexShader;
            public InputLayout layout;

            public float distanceculling;
            public bool frustrumculldraw;
            public float[] realpos;
            public int[] chunkPos;
            //public int indexinlevelgenmap;
            public int indexintypeoftiles;
            public int typeofterraintile;

            public int X;
            public int Y;
            public int Z;


            public scinstanceintmaps instanceintmap;
            public SharpDX.Direct3D11.Buffer instanceintmapbuffer;

            public scinstancevertdimensions instancesmatrixa;
            public SharpDX.Direct3D11.Buffer instancesmatrixbuffera;

            public scinstancevertdimensions instancesmatrixb;
            public SharpDX.Direct3D11.Buffer instancesmatrixbufferb;

            public scinstancevertdimensions instancesmatrixc;
            public SharpDX.Direct3D11.Buffer instancesmatrixbufferc;

            public scinstancevertdimensions instancesmatrixd;
            public SharpDX.Direct3D11.Buffer instancesmatrixbufferd;

            //public SharpDX.Direct3D11.Buffer ConstantLightBuffer;
        }




        public tutorialchunkcubemap getchunkinlevelgenmap(int x, int y, int z, int levelofdetail)
        {
            int orix = x;
            int oriy = y;
            int oriz = z;

            if (x < 0)
            {
                x *= -1;
                x = x + (sccslevelgen.maxx - 1);
            }

            if (y < 0)
            {
                y *= -1;
                y = y + (sccslevelgen.maxy - 1);
            }
            if (z < 0)
            {
                z *= -1;
                z = z + (sccslevelgen.maxz - 1);
            }

            int indexinsclevelgenmap = x + sccslevelgen.somewidth * (y + sccslevelgen.someheight * z);

            if (indexinsclevelgenmap < sccslevelgen.somewidth * sccslevelgen.someheight * sccslevelgen.somedepth)
            {
                int typeofterraintile = sccslevelgen.levelmap[indexinsclevelgenmap];
               
                if (typeofterraintile == 0 ||
                    typeofterraintile == 1101 ||
                    typeofterraintile == 1102 ||
                    typeofterraintile == 1103 ||
                    typeofterraintile == 1104 ||
                    typeofterraintile == 1105 ||
                    typeofterraintile == 1106 ||
                    typeofterraintile == 1107 ||
                    typeofterraintile == 1108 ||
                    typeofterraintile == 1109 ||
                    typeofterraintile == 1110 ||
                    typeofterraintile == 1111 ||
                    typeofterraintile == 1112 ||
                    typeofterraintile == -99 ||
                    typeofterraintile == 1115)
                {

                    


                    if (arraychunkdatalod0[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod0 != null)
                    {
                        if (levelofdetail == 1)
                        {
                            return arraychunkdatalod0[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod0;
                        }
                        /*else if (levelofdetail == 2)
                        {
                            return arraychunkdatalod1[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod1;
                        }
                        else if (levelofdetail == 3)
                        {
                            return arraychunkdatalod2[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod2;
                        }
                        else if (levelofdetail == 4)
                        {
                            return arraychunkdatalod3[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod3;
                        }
                        else if (levelofdetail == 5)
                        {
                            return arraychunkdatalod4[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod4;
                        }*/
                    }
                }
            }

            return null;
        }


        /*
        public sclevelgenvert getchunkinlevelgenmap(int x, int y, int z, int levelofdetail) //, Vector3 chunkPos
        {
            int orix = x;
            int oriy = y;
            int oriz = z;

            if (x < 0)
            {
                x *= -1;
                x = x + (sclevelgen.maxx - 1);
            }

            if (y < 0)
            {
                y *= -1;
                y = y + (sclevelgen.maxy - 1);
            }
            if (z < 0)
            {
                z *= -1;
                z = z + (sclevelgen.maxz - 1);
            }
            //Console.WriteLine(y);

            //int indexinarray = xx * somewidth + (zz);
            int indexinsclevelgenmap = x + sclevelgen.somewidth * (y + sclevelgen.someheight * z);

            if (indexinsclevelgenmap < sclevelgen.somewidth * sclevelgen.someheight * sclevelgen.somedepth)
            {
                int indexinvectorarray = sclevelgen.levelmap[indexinsclevelgenmap]; //levelgen.levelindexmap[indexinsclevelgenmap]

                if (indexinvectorarray != -1)
                {
                    if (arraychunkdatalod0[indexinvectorarray].arraychunkvertslod0 != null)
                    {
                        //if (indexinsclevelgenmap == arraychunkdatalod0[indexinvectorarray].indexinlevelgenmap && indexinvectorarray == arraychunkdatalod0[indexinvectorarray].indexintypeoftiles)
                        if (indexinvectorarray == arraychunkdatalod0[indexinvectorarray].indexinlevelgenmap)
                        {
                            //if (arraychunkdatalod0[indexinvectorarray].X == orix &&
                            ///    arraychunkdatalod0[indexinvectorarray].Y == oriy &&
                            //    arraychunkdatalod0[indexinvectorarray].Z == oriz)


                            //if ((int)Math.Round(arraychunkdatalod0[indexinvectorarray].chunkPos.X) == (int)Math.Round(sclevelgen.listoftiles[indexinvectorarray].X) &&
                            //    (int)Math.Round(arraychunkdatalod0[indexinvectorarray].chunkPos.Y) == (int)Math.Round(sclevelgen.listoftiles[indexinvectorarray].Y) &&
                            //    (int)Math.Round(arraychunkdatalod0[indexinvectorarray].chunkPos.Z) == (int)Math.Round(sclevelgen.listoftiles[indexinvectorarray].Z))
                            {

                                //Console.WriteLine("same index");
                                if (levelofdetail == 1)
                                {
                                    return arraychunkdatalod0[indexinvectorarray].arraychunkvertslod0;
                                }
                                else if (levelofdetail == 2)
                                {
                                    return arraychunkdatalod1[indexinvectorarray].arraychunkvertslod1;
                                }
                                else if (levelofdetail == 3)
                                {
                                    return arraychunkdatalod2[indexinvectorarray].arraychunkvertslod2;
                                }
                                else if (levelofdetail == 4)
                                {
                                    return arraychunkdatalod3[indexinvectorarray].arraychunkvertslod3;
                                }
                                else if (levelofdetail == 5)
                                {
                                    return arraychunkdatalod4[indexinvectorarray].arraychunkvertslod4;
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("null chunk");
                    }
                }
            }
            else
            {
                return null;
                Console.WriteLine("Out of Range. The chunk is a border tile");
            }




            /*
            float x0 = (float)(Math.Round(x * 10.0f) / 10.0f);
            float y0 = (float)(Math.Round(y * 10.0f) / 10.0f);
            float z0 = (float)(Math.Round(z * 10.0f) / 10.0f);
            */
        /*//var enumerator0 = sclevelgenchunk.arraychunkdatalod0.GetEnumerator();
        for (int i = 0; i < levelgen.Length; i++)
        {

            float x1 = (float)(Math.Round(LevelGenerator4[i].chunkPos.X * 10.0f) / 10.0f);
            float y1 = (float)(Math.Round(arraychunkdatalod0[i].chunkPos.Y * 10.0f) / 10.0f);
            float z1 = (float)(Math.Round(arraychunkdatalod0[i].chunkPos.Z * 10.0f) / 10.0f);


            if (x0 == x1 && y0 == y1 && z0 == z1)
            {
                arrayindex = i;
                return arraychunkdatalod0[i].arraychunkvertslod0;
            }
        }

        //arrayindex = -1;
        //Console.WriteLine();
        return null;
    }



    public sclevelgenvert getchunkinlevelgenmaplevelgen4(int x, int y, int z, int levelofdetail) //, Vector3 chunkPos
    {
        int orix = x;
        int oriy = y;
        int oriz = z;

        if (x < 0)
        {
            x *= -1;
            x = x + (levelgen.maxx - 1);
        }

        if (y < 0)
        {
            y *= -1;
            y = y + (levelgen.maxy - 1);
        }
        if (z < 0)
        {
            z *= -1;
            z = z + (levelgen.maxz - 1);
        }
        //Console.WriteLine(y);

        //int indexinarray = xx * somewidth + (zz);
        int indexinsclevelgenmap = x + levelgen.somewidth * (y + levelgen.someheight * z);

        if (indexinsclevelgenmap < levelgen.somewidth * levelgen.someheight * levelgen.somedepth)
        {
            int indexinvectorarray = levelgen.levelmap[indexinsclevelgenmap]; //levelgen.levelindexmap[indexinsclevelgenmap]

            if (indexinvectorarray != -1)
            {
                if (arraychunkdatalod0[indexinvectorarray].arraychunkvertslod0 != null)
                {
                    //if (indexinsclevelgenmap == arraychunkdatalod0[indexinvectorarray].indexinlevelgenmap && indexinvectorarray == arraychunkdatalod0[indexinvectorarray].indexintypeoftiles)
                    //if (indexinvectorarray == arraychunkdatalod0[indexinvectorarray].indexintypeoftiles)
                    {
                        if (arraychunkdatalod0[indexinvectorarray].X == orix &&
                            arraychunkdatalod0[indexinvectorarray].Y == oriy &&
                            arraychunkdatalod0[indexinvectorarray].Z == oriz)


                        //if ((int)Math.Round(arraychunkdatalod0[indexinvectorarray].chunkPos.X) == (int)Math.Round(sclevelgen.listoftiles[indexinvectorarray].X) &&
                        //    (int)Math.Round(arraychunkdatalod0[indexinvectorarray].chunkPos.Y) == (int)Math.Round(sclevelgen.listoftiles[indexinvectorarray].Y) &&
                        //    (int)Math.Round(arraychunkdatalod0[indexinvectorarray].chunkPos.Z) == (int)Math.Round(sclevelgen.listoftiles[indexinvectorarray].Z))
                        {

                            //Console.WriteLine("same index");
                            if (levelofdetail == 1)
                            {
                                return arraychunkdatalod0[indexinvectorarray].arraychunkvertslod0;
                            }
                            else if (levelofdetail == 2)
                            {
                                return arraychunkdatalod1[indexinvectorarray].arraychunkvertslod1;
                            }
                            else if (levelofdetail == 3)
                            {
                                return arraychunkdatalod2[indexinvectorarray].arraychunkvertslod2;
                            }
                            else if (levelofdetail == 4)
                            {
                                return arraychunkdatalod3[indexinvectorarray].arraychunkvertslod3;
                            }
                            else if (levelofdetail == 5)
                            {
                                return arraychunkdatalod4[indexinvectorarray].arraychunkvertslod4;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            return null;
            Console.WriteLine("Out of Range. The chunk is a border tile");
        }




        /*
        float x0 = (float)(Math.Round(x * 10.0f) / 10.0f);
        float y0 = (float)(Math.Round(y * 10.0f) / 10.0f);
        float z0 = (float)(Math.Round(z * 10.0f) / 10.0f);
        */
        /*//var enumerator0 = sclevelgenchunk.arraychunkdatalod0.GetEnumerator();
        for (int i = 0; i < levelgen.Length; i++)
        {

            float x1 = (float)(Math.Round(LevelGenerator4[i].chunkPos.X * 10.0f) / 10.0f);
            float y1 = (float)(Math.Round(arraychunkdatalod0[i].chunkPos.Y * 10.0f) / 10.0f);
            float z1 = (float)(Math.Round(arraychunkdatalod0[i].chunkPos.Z * 10.0f) / 10.0f);


            if (x0 == x1 && y0 == y1 && z0 == z1)
            {
                arrayindex = i;
                return arraychunkdatalod0[i].arraychunkvertslod0;
            }
        }

        //arrayindex = -1;
        //Console.WriteLine();
        return null;
    }*/


        /*
        public sclevelgenvert getChunklod0(float x, float y, float z, out int arrayindex) //, Vector3 chunkPos
        {

            float x0 = (float)(Math.Round(x / 10.0f) * 10.0f);
            float y0 = (float)(Math.Round(y / 10.0f) * 10.0f);
            float z0 = (float)(Math.Round(z / 10.0f) * 10.0f);

            //var enumerator0 = sclevelgenchunk.arraychunkdatalod0.GetEnumerator();
            for (int i = 0; i < arraychunkdatalod0.Length; i++)
            {

                float x1 = (float)(Math.Round(arraychunkdatalod0[i].chunkPos.X / 10.0f) * 10.0f);
                float y1 = (float)(Math.Round(arraychunkdatalod0[i].chunkPos.Y / 10.0f) * 10.0f);
                float z1 = (float)(Math.Round(arraychunkdatalod0[i].chunkPos.Z / 10.0f) * 10.0f);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    arrayindex = i;
                    return arraychunkdatalod0[i].arraychunkvertslod0;
                }

            }

            arrayindex = -1;
            return null;

        }*/




        /*
        public sclevelgenvert getChunklod0(int x, int y, int z, out int arrayindex) //, Vector3 chunkPos
        {

            //int x0 = ((x));
            //int y0 = ((y));
            //int z0 = ((z));

            for (int i = 0; i < arraychunkdatalod0.Length; i++)
            {

                int x1 = ((arraychunkdatalod0[i].X));
                int y1 = ((arraychunkdatalod0[i].Y));
                int z1 = ((arraychunkdatalod0[i].Z));


                if (x == x1 && y == y1 && z == z1)
                {
                    arrayindex = i;
                    return arraychunkdatalod0[i].arraychunkvertslod0;
                }

            }

            arrayindex = -1;
            return null;

        }
        
        public sclevelgenvert getChunklod1(float x, float y, float z, out int arrayindex) //, Vector3 chunkPos
        {

            float x0 = (float)(Math.Round(x * 10.0f) / 10.0f);
            float y0 = (float)(Math.Round(y * 10.0f) / 10.0f);
            float z0 = (float)(Math.Round(z * 10.0f) / 10.0f);

            //var enumerator0 = sclevelgenchunk.arraychunkdatalod0.GetEnumerator();
            for (int i = 0; i < arraychunkdatalod1.Length; i++)
            {

                float x1 = (float)(Math.Round(arraychunkdatalod1[i].chunkPos.X * 10.0f) / 10.0f);
                float y1 = (float)(Math.Round(arraychunkdatalod1[i].chunkPos.Y * 10.0f) / 10.0f);
                float z1 = (float)(Math.Round(arraychunkdatalod1[i].chunkPos.Z * 10.0f) / 10.0f);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    arrayindex = i;
                    return arraychunkdatalod1[i].arraychunkvertslod1;
                }

            }

            arrayindex = -1;
            return null;

        }


        public sclevelgenvert getChunklod2(float x, float y, float z, out int arrayindex) //, Vector3 chunkPos
        {
            float x0 = (float)(Math.Round(x * 10.0f) / 10.0f);
            float y0 = (float)(Math.Round(y * 10.0f) / 10.0f);
            float z0 = (float)(Math.Round(z * 10.0f) / 10.0f);

            //var enumerator0 = sclevelgenchunk.arraychunkdatalod0.GetEnumerator();
            for (int i = 0; i < arraychunkdatalod2.Length; i++)
            {

                float x1 = (float)(Math.Round(arraychunkdatalod2[i].chunkPos.X * 10.0f) / 10.0f);
                float y1 = (float)(Math.Round(arraychunkdatalod2[i].chunkPos.Y * 10.0f) / 10.0f);
                float z1 = (float)(Math.Round(arraychunkdatalod2[i].chunkPos.Z * 10.0f) / 10.0f);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    arrayindex = i;
                    return arraychunkdatalod2[i].arraychunkvertslod2;
                }
            }

            arrayindex = -1;
            return null;

        }


        public sclevelgenvert getChunklod3(float x, float y, float z, out int arrayindex) //, Vector3 chunkPos
        {

            float x0 = (float)(Math.Round(x * 10.0f) / 10.0f);
            float y0 = (float)(Math.Round(y * 10.0f) / 10.0f);
            float z0 = (float)(Math.Round(z * 10.0f) / 10.0f);

            //var enumerator0 = sclevelgenchunk.arraychunkdatalod0.GetEnumerator();
            for (int i = 0; i < arraychunkdatalod3.Length; i++)
            {

                float x1 = (float)(Math.Round(arraychunkdatalod3[i].chunkPos.X * 10.0f) / 10.0f);
                float y1 = (float)(Math.Round(arraychunkdatalod3[i].chunkPos.Y * 10.0f) / 10.0f);
                float z1 = (float)(Math.Round(arraychunkdatalod3[i].chunkPos.Z * 10.0f) / 10.0f);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    arrayindex = i;
                    return arraychunkdatalod3[i].arraychunkvertslod3;
                }
            }

            arrayindex = -1;
            return null;
        }






        public sclevelgenvert getChunklod4(float x, float y, float z, out int arrayindex) //, Vector3 chunkPos
        {
            float x0 = (float)(Math.Round(x * 10.0f) / 10.0f);
            float y0 = (float)(Math.Round(y * 10.0f) / 10.0f);
            float z0 = (float)(Math.Round(z * 10.0f) / 10.0f);

            //var enumerator0 = sclevelgenchunk.arraychunkdatalod0.GetEnumerator();
            for (int i = 0; i < arraychunkdatalod4.Length; i++)
            {

                float x1 = (float)(Math.Round(arraychunkdatalod4[i].chunkPos.X * 10.0f) / 10.0f);
                float y1 = (float)(Math.Round(arraychunkdatalod4[i].chunkPos.Y * 10.0f) / 10.0f);
                float z1 = (float)(Math.Round(arraychunkdatalod4[i].chunkPos.Z * 10.0f) / 10.0f);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    arrayindex = i;
                    return arraychunkdatalod4[i].arraychunkvertslod4;
                }
            }

            arrayindex = -1;
            return null;
        }*/













        ~tutorialcubeaschunkinst()
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
                }

                if (arrayofverts != null)
                {
                    arrayofverts = null;
                }

                if (arrayoftrigs != null)
                {
                    arrayoftrigs = null;
                }*/





                // Dispose all owned managed objects
            }

            // Release unmanaged resources
        }
    }
}
