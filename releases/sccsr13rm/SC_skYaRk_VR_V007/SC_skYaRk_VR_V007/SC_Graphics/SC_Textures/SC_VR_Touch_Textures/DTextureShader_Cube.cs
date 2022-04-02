﻿using SharpDX.Direct3D11;
using SharpDX.WIC;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System;

using SharpDX;
using SharpDX.D3DCompiler;
using System.Runtime.InteropServices;
//using System.Windows.Forms;
//using SharpHelper;



namespace SC_skYaRk_VR_V007.SC_Graphics
{
    public class DTextureShader_Cube                   // 199 lines
    {
        // Structures.
        [StructLayout(LayoutKind.Sequential)]
        public struct DVertex
        {
            public static int AppendAlignedElement = 12;
            public Vector3 position;
            public Vector4 color;
            //public Vector3 normal;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct DMatrixBuffer
        {
            public Matrix world;
            public Matrix view;
            public Matrix projection;
        }

        // Properties.
        private VertexShader VertexShader { get; set; }
        private PixelShader PixelShader { get; set; }
        private GeometryShader GeometryShader { get; set; }
        private SamplerState SamplerState { get; set; }

        private InputLayout Layout { get; set; }
        private SharpDX.Direct3D11.Buffer ConstantMatrixBuffer { get; set; }

        // Constructor
        public DTextureShader_Cube() { }

        // Methods.
        public bool Initialize(Device device, IntPtr windowsHandle) //, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix
        {
            // Initialize the vertex and pixel shaders.
            return InitializeShader(device, windowsHandle); // @"Tut04\Shaders\Color.ps" //, "Color.vs", "Color.ps"
        }
        private bool InitializeShader(Device device, IntPtr windowsHandle) //, string vsFileName, string psFileName
        {
            try
            {
                // Setup full pathes
                //vsFileName = SC_System.DSystemConfiguration.ShaderFilePath + vsFileName;
                //psFileName = SC_System.DSystemConfiguration.ShaderFilePath + psFileName;

                // Compile the vertex shader code.
                //ShaderBytecode vertexShaderByteCode = ShaderBytecode.CompileFromFile(vsFileName, "ColorVertexShader", "vs_4_0", ShaderFlags.None, EffectFlags.None);
                // Compile the pixel shader code.
                //ShaderBytecode pixelShaderByteCode = ShaderBytecode.CompileFromFile(psFileName, "ColorPixelShader", "ps_4_0", ShaderFlags.None, EffectFlags.None);
                /*var vsFileNameByteArray = SC_SkYaRk_Clean.Properties.Resources.Color1;
                var psFileNameByteArray = SC_SkYaRk_Clean.Properties.Resources.Color;
                var gsFileNameByteArray = SC_SkYaRk_Clean.Properties.Resources.HLSL;



                SharpShader shader = new SharpShader(device, gsFileNameByteArray,
                  new SharpShaderDescription() { VertexShaderFunction = "VS", PixelShaderFunction = "PS", GeometryShaderFunction = "GS" },
                  new InputElement[] {

                        new InputElement()
                        {
                            SemanticName = "POSITION",
                            SemanticIndex = 0,
                            Format = SharpDX.DXGI.Format.R32G32B32_Float,
                            Slot = 0,
                            AlignedByteOffset = 0,
                            Classification = InputClassification.PerVertexData,
                            InstanceDataStepRate = 0
                        },
                  });
                  */



                string vsFileNameByteArray = "../../../Resources/" + "red.vs";
                string psFileNameByteArray = "../../../Resources/" + "red.ps";
                //string gsFileNameByteArray = "../../../Resources/" + "HLSL.gs";

                //var vsFileNameByteArray = SC_skYaRk_VR_V007.Properties..red1;
                //var psFileNameByteArray = SC_skYaRk_VR_V007.Properties.Resources.red;
                //var gsFileNameByteArray = SC_skYaRk_VR_V007.Properties.Resources.HLSL;

                ShaderBytecode vertexShaderByteCode = ShaderBytecode.CompileFromFile(vsFileNameByteArray, "ColorVertexShader", "vs_5_0", ShaderFlags.None, EffectFlags.None);
                ShaderBytecode pixelShaderByteCode = ShaderBytecode.CompileFromFile(psFileNameByteArray, "ColorPixelShader", "ps_5_0", ShaderFlags.None, EffectFlags.None);
                //ShaderBytecode geometryShaderByteCode = ShaderBytecode.CompileFromFile(gsFileNameByteArray, "GS", "gs_5_0", ShaderFlags.None, EffectFlags.None);

                VertexShader = new VertexShader(device, vertexShaderByteCode);
                PixelShader = new PixelShader(device, pixelShaderByteCode);
                //GeometryShader = new GeometryShader(device, geometryShaderByteCode);






                //new InputElement("POSITION", 0, SharpDX.DXGI.Format.R32G32B32_Float, 0, 0)

                //var vsFileNameByteArray = SC_WPF_RENDER.Properties.Resources.red1;
                //var psFileNameByteArray = SC_WPF_RENDER.Properties.Resources.red;
                //var gsFileNameByteArray = SC_WPF_RENDER.Properties.Resources.HLSL;
                //ShaderBytecode vertexShaderByteCode = ShaderBytecode.Compile(vsFileNameByteArray, "ColorVertexShader", "vs_5_0", ShaderFlags.None, EffectFlags.None);
                //ShaderBytecode pixelShaderByteCode = ShaderBytecode.Compile(psFileNameByteArray, "ColorPixelShader", "ps_5_0", ShaderFlags.None, EffectFlags.None);
                //ShaderBytecode geometryShaderByteCode = ShaderBytecode.Compile(gsFileNameByteArray, "GS", "gs_5_0", ShaderFlags.None, EffectFlags.None);

                //ShaderBytecode vertexShaderByteCode = ShaderBytecode.Compile(gsFileNameByteArray, "VS", "vs_5_0", ShaderFlags.None, EffectFlags.None);
                //ShaderBytecode pixelShaderByteCode = ShaderBytecode.Compile(gsFileNameByteArray, "PS", "ps_5_0", ShaderFlags.None, EffectFlags.None);


                // Create the vertex shader from the buffer.
                //VertexShader = new VertexShader(device, vertexShaderByteCode);
                // Create the pixel shader from the buffer.
                //PixelShader = new PixelShader(device, pixelShaderByteCode);

                //GeometryShader = new GeometryShader(device, geometryShaderByteCode);
                // Now setup the layout of the data that goes into the shader.
                // This setup needs to match the VertexType structure in the Model and in the shader.



                InputElement[] inputElements = new InputElement[]
                {
                    //new InputElement("POSITION", 0, SharpDX.DXGI.Format.R32G32B32_Float, 0, 0)

                    new InputElement()
                    {
                        SemanticName = "POSITION",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 0,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "COLOR",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "NORMAL",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },


                    new InputElement()
                    {
                        SemanticName = "TEXCOORD",
                        SemanticIndex = 1,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 1,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    }


                   /*new InputElement()
                    {
                         SemanticName = "TEXCOORD",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },*/

               
                };





                /*
                new InputElement()
                {
                    SemanticName = "POSITION",
                    SemanticIndex = 0,
                    Format = SharpDX.DXGI.Format.R32G32B32_Float,
                    Slot = 0,
                    AlignedByteOffset = 0,
                    Classification = InputClassification.PerVertexData,
                    InstanceDataStepRate = 0
                },
                    new InputElement()
                    {
                        SemanticName = "NORMAL",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "TEXCOORD",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },             
                    new InputElement()
                    {
                        SemanticName = "TEXCOORD",
                        SemanticIndex = 1,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 1,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    }*/
















                // Create the vertex input the layout. Kin dof like a Vertex Declaration.
                Layout = new InputLayout(device, ShaderSignature.GetInputSignature(vertexShaderByteCode), inputElements);

                // Release the vertex and pixel shader buffers, since they are no longer needed.
                vertexShaderByteCode.Dispose();
                pixelShaderByteCode.Dispose();

                // Setup the description of the dynamic matrix constant Matrix buffer that is in the vertex shader.
                /*BufferDescription matrixBufferDescription = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<DMatrixBuffer>(),
                    BindFlags = BindFlags.ConstantBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };*/


                BufferDescription matrixBufferDescription = new BufferDescription()
                {
                    Usage = ResourceUsage.Default,
                    SizeInBytes = Utilities.SizeOf<DMatrixBuffer>(),
                    BindFlags = BindFlags.ConstantBuffer,
                    CpuAccessFlags = CpuAccessFlags.None,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };



                // Create the constant buffer pointer so we can access the vertex shader constant buffer from within this class.
                ConstantMatrixBuffer = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescription);

















                //device.ImmediateContext.InputAssembler.InputLayout = Layout;
                //device.ImmediateContext.VertexShader.Set(VertexShader);
                //device.ImmediateContext.PixelShader.Set(PixelShader);
                //deviceContext.PixelShader.SetSampler(0, SamplerState);
                //device.ImmediateContext.GeometryShader.Set(GeometryShader);





                /*worldMatrix.Transpose();
                viewMatrix.Transpose();
                projectionMatrix.Transpose();

                DataStream mappedResource;
                device.ImmediateContext.MapSubresource(ConstantMatrixBuffer, MapMode.WriteDiscard, MapFlags.None, out mappedResource);

                DMatrixBuffer matrixBuffer = new DMatrixBuffer()
                {
                    world = worldMatrix,
                    view = viewMatrix,
                    projection = projectionMatrix
                };
                mappedResource.Write(matrixBuffer);
                device.ImmediateContext.UnmapSubresource(ConstantMatrixBuffer, 0);
                */

                //int bufferSlotNumber = 0;
                //device.ImmediateContext.VertexShader.SetConstantBuffer(bufferSlotNumber, ConstantMatrixBuffer);




                //int bufferSlotNumber = 0;
                //device.ImmediateContext.VertexShader.SetConstantBuffer(bufferSlotNumber, ConstantMatrixBuffer);


                //SharpDX.Direct3D11.Buffer bufferWorld = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<SharpDX.Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
                //device.ImmediateContext.VertexShader.SetConstantBuffer(1, bufferWorld);
                //device.ImmediateContext.PixelShader.SetConstantBuffer(1, bufferWorld);

                /*int bufferSlotNumber = 0;
                device.ImmediateContext.VertexShader.SetConstantBuffer(bufferSlotNumber, ConstantMatrixBuffer);

                device.ImmediateContext.InputAssembler.InputLayout = Layout;
                device.ImmediateContext.VertexShader.Set(VertexShader);
                device.ImmediateContext.PixelShader.Set(PixelShader);
                //deviceContext.PixelShader.SetSampler(0, SamplerState);
                device.ImmediateContext.GeometryShader.Set(GeometryShader);
                */


                // Create a texture sampler state description.
                /*SamplerStateDescription samplerDesc = new SamplerStateDescription()
                {
                    Filter = Filter.MinMagMipLinear,
                    AddressU = TextureAddressMode.Wrap,
                    AddressV = TextureAddressMode.Wrap,
                    AddressW = TextureAddressMode.Wrap,
                    MipLodBias = 0,
                    MaximumAnisotropy = 1,
                    ComparisonFunction = Comparison.Always,
                    BorderColor = new Color4(0, 0, 0, 0),  // Black Border.
                    MinimumLod = 0,
                    MaximumLod = float.MaxValue
                };

                // Create the texture sampler state.
                SamplerState = new SamplerState(device, samplerDesc);
                */

                //int bufferSlotNumber = 0;
                //device.ImmediateContext.VertexShader.SetConstantBuffer(bufferSlotNumber, ConstantMatrixBuffer);

                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error initializing shader. Error is " + ex.Message);
                return false;
            }
        }
        inData data;
        SharpDX.Direct3D11.Buffer bufferWorld;

        [StructLayout(LayoutKind.Sequential, Pack = 16)]
        public struct inData
        {
            public SharpDX.Matrix worldMatrix;
            public SharpDX.Matrix viewMatrix;
            public SharpDX.Matrix projectionMatrix;


        }
        public void ShutDown()
        {
            // Shutdown the vertex and pixel shaders as well as the related objects.
            ShuddownShader();
        }
        private void ShuddownShader()
        {
            SamplerState?.Dispose();
            SamplerState = null;
            // Release the matrix constant buffer.
            ConstantMatrixBuffer?.Dispose();
            ConstantMatrixBuffer = null;
            // Release the layout.
            Layout?.Dispose();
            Layout = null;
            // Release the pixel shader.
            PixelShader?.Dispose();
            PixelShader = null;
            // Release the vertex shader.
            VertexShader?.Dispose();
            VertexShader = null;

            GeometryShader?.Dispose();
            GeometryShader = null;

        }

        public bool Render(DeviceContext deviceContext, int indexCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, int vertexCount, int instanceCount)
        {
            if (!SetShaderParameters(deviceContext, worldMatrix, viewMatrix, projectionMatrix))
                return false;

            RenderShader(deviceContext, indexCount, vertexCount, instanceCount);

            return true;
        }

        private bool SetShaderParameters(DeviceContext deviceContext, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix)
        {
            try
            {
                /*worldMatrix.Transpose();
                viewMatrix.Transpose();
                projectionMatrix.Transpose();

                DataStream mappedResource;
                deviceContext.MapSubresource(ConstantMatrixBuffer, MapMode.WriteDiscard, MapFlags.None, out mappedResource);

                DMatrixBuffer matrixBuffer = new DMatrixBuffer()
                {
                    world = worldMatrix,
                    view = viewMatrix,
                    projection = projectionMatrix
                };
                mappedResource.Write(matrixBuffer);
                deviceContext.UnmapSubresource(ConstantMatrixBuffer, 0);
                int bufferSlotNumber = 0;
                deviceContext.VertexShader.SetConstantBuffer(bufferSlotNumber, ConstantMatrixBuffer);

                mappedResource.Dispose();*/

                /*inData MatrixBuffer = new inData();
                worldMatrix.Transpose();
                viewMatrix.Transpose();
                projectionMatrix.Transpose();

                MatrixBuffer.worldMatrix = worldMatrix;
                MatrixBuffer.viewMatrix = viewMatrix;
                MatrixBuffer.projectionMatrix = projectionMatrix;

                deviceContext.UpdateSubresource(ref MatrixBuffer, ConstantMatrixBuffer);*/



                /*data = new inData();
                worldMatrix.Transpose();
                viewMatrix.Transpose();
                projectionMatrix.Transpose();

                data.worldMatrix = worldMatrix;
                data.viewMatrix = viewMatrix;
                data.projectionMatrix = projectionMatrix;

                deviceContext.UpdateSubresource(ref data, bufferWorld);*/



                inData MatrixBuffer = new inData();
                worldMatrix.Transpose();
                viewMatrix.Transpose();
                projectionMatrix.Transpose();

                MatrixBuffer.worldMatrix = worldMatrix;
                MatrixBuffer.viewMatrix = viewMatrix;
                MatrixBuffer.projectionMatrix = projectionMatrix;

                deviceContext.UpdateSubresource(ref MatrixBuffer, ConstantMatrixBuffer);



                //int bufferSlotNumber = 0;
                //deviceContext.VertexShader.SetConstantBuffer(bufferSlotNumber, ConstantMatrixBuffer);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void RenderShader(DeviceContext deviceContext, int indexCount, int vertexCount, int instanceCount)
        {
          
            //device.ImmediateContext.InputAssembler.InputLayout = Layout;
            //device.ImmediateContext.VertexShader.Set(VertexShader);
            //device.ImmediateContext.PixelShader.Set(PixelShader);
            //deviceContext.PixelShader.SetSampler(0, SamplerState);
            //device.ImmediateContext.GeometryShader.Set(GeometryShader);




            deviceContext.InputAssembler.InputLayout = Layout;
            deviceContext.VertexShader.Set(VertexShader);
            deviceContext.PixelShader.Set(PixelShader);
            //deviceContext.PixelShader.SetSampler(0, SamplerState);
            deviceContext.GeometryShader.Set(GeometryShader);


            deviceContext.DrawIndexedInstanced(indexCount, instanceCount, 0, 0, 0);
        }

        public bool updateWorld(DeviceContext deviceContext, int indexCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, int vertexCount, int instanceCount)
        {
            inData data = new inData();
            data.worldMatrix = worldMatrix;
            data.viewMatrix = viewMatrix;
            data.projectionMatrix = projectionMatrix;
            deviceContext.UpdateSubresource(ref data, bufferWorld);

            return true;
        }
    }
}