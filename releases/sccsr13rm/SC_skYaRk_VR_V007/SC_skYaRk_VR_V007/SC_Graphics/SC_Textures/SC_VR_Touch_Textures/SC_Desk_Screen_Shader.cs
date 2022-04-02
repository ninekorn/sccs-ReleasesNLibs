//using DSharpDXRastertek.Tut37.System;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using System;
using System.Runtime.InteropServices;
//using System.Windows.Forms;
//using System.Runtime.ex

namespace SC_skYaRk_VR_V007.SC_Graphics
{
    public class SC_Desk_Screen_Shader                 // 228 lines
    {
        // Structs
        [StructLayout(LayoutKind.Sequential)]
        internal struct DMatrixBuffer
        {
            public Matrix world;
            public Matrix view;
            public Matrix projection;
        }

        // Properties
        public VertexShader VertexShader { get; set; }
        public PixelShader PixelShader { get; set; }
        public InputLayout Layout { get; set; }
        public SharpDX.Direct3D11.Buffer ConstantMatrixBuffer { get; set; }
        public SamplerState SamplerState { get; set; }

        // Constructor
        public SC_Desk_Screen_Shader() { }

        public SharpDX.Direct3D11.Buffer _constantLightBuffer;
        DataStream mappedResourceLight;


        public SC_skYaRk_VR_V007.SC_Graphics.SC_Desk_Screen.DLightBuffer[] _DLightBuffer;
        // Methods
        public bool Initialize(Device device, IntPtr windowsHandler, SharpDX.Direct3D11.Buffer ConstantLightBuffer, SC_skYaRk_VR_V007.SC_Graphics.SC_Desk_Screen.DLightBuffer[] DLightBuffer)
        {
            this._DLightBuffer = DLightBuffer;
            this._constantLightBuffer = ConstantLightBuffer;
            // Initialize the vertex and pixel shaders.
            return InitializeShader(device, windowsHandler, "InstancedTexture.vs", "InstancedTexture.ps");
        }
        private bool InitializeShader(Device device, IntPtr windowsHandler, string vsFileName, string psFileName)
        {
            try
            {

                vsFileName = "../../../InstanceShader/" + "InstancedTexture.vs";
                psFileName = "../../../InstanceShader/" + "InstancedTexture.ps";

                //ShaderBytecode vertexShaderByteCode = ShaderBytecode.CompileFromFile(vsFileName, "TextureVertexShader", "vs_4_0", ShaderFlags.None, EffectFlags.None);
                //ShaderBytecode pixelShaderByteCode = ShaderBytecode.CompileFromFile(psFileName, "TexturePixelShader", "ps_4_0", ShaderFlags.None, EffectFlags.None);
                ShaderBytecode vertexShaderByteCode = ShaderBytecode.CompileFromFile(vsFileName, "TextureVertexShader", "vs_5_0", ShaderFlags.None, EffectFlags.None);
                ShaderBytecode pixelShaderByteCode = ShaderBytecode.CompileFromFile(psFileName, "TexturePixelShader", "ps_5_0", ShaderFlags.None, EffectFlags.None);



                VertexShader = new VertexShader(device, vertexShaderByteCode);
                PixelShader = new PixelShader(device, pixelShaderByteCode);



                //vsFileName = DSystemConfiguration.ShaderFilePath + vsFileName;
                //psFileName = DSystemConfiguration.ShaderFilePath + psFileName;
                //ShaderBytecode vertexShaderByteCode = ShaderBytecode.CompileFromFile(vsFileName, "TextureVertexShader", DSystemConfiguration.VertexShaderProfile, ShaderFlags.None, EffectFlags.None);
                //ShaderBytecode pixelShaderByteCode = ShaderBytecode.CompileFromFile(psFileName, "TexturePixelShader", DSystemConfiguration.PixelShaderProfile, ShaderFlags.None, EffectFlags.None);
                //VertexShader = new VertexShader(device, vertexShaderByteCode);
                //PixelShader = new PixelShader(device, pixelShaderByteCode);




                // Now setup the layout of the data that goes into the shader.
                // This setup needs to match the VertexType structure in the Model and in the shader.
                InputElement[] inputElements = new InputElement[]
                {
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
                        SemanticName = "TEXCOORD",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                };

                // Create the vertex input the layout. Kin dof like a Vertex Declaration.
                Layout = new InputLayout(device, ShaderSignature.GetInputSignature(vertexShaderByteCode), inputElements);

                // Release the vertex and pixel shader buffers, since they are no longer needed.
                vertexShaderByteCode.Dispose();
                pixelShaderByteCode.Dispose();

                // Setup the description of the dynamic matrix constant Matrix buffer that is in the vertex shader.
                BufferDescription matrixBufferDescription = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<DMatrixBuffer>(),
                    BindFlags = BindFlags.ConstantBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                // Create the constant buffer pointer so we can access the vertex shader constant buffer from within this class.
                ConstantMatrixBuffer = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescription);
                //ConstantMatrixBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<DMatrixBuffer>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

                /*// Create a texture sampler state description.
                SamplerStateDescription samplerDesc = new SamplerStateDescription()
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



                // Create a texture sampler state description.
                SamplerStateDescription samplerDesc = new SamplerStateDescription()
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













                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error initializing shader. Error is " + ex.Message);
                return false;
            }
        }
        public void ShutDown()
        {
            // Shutdown the vertex and pixel shaders as well as the related objects.
            ShuddownShader();
        }
        private void ShuddownShader()
        {
            // Release the sampler state.
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
        }


        SC_skYaRk_VR_V007.SC_Graphics.SC_Desk_Screen.DInstanceData[] instancesDataRIGHT;// = new SC_Desk_Screen.DInstanceData[];
        SC_skYaRk_VR_V007.SC_Graphics.SC_Desk_Screen.DInstanceData[] instancesDataUP;

        int _initDirectionsArray = 1;

        public bool Render(DeviceContext deviceContext, int vertexCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, SC_skYaRk_VR_V007.SC_Graphics.SC_Desk_Screen.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, SharpDX.Direct3D11.ShaderResourceView _ShaderResource)
        {
            // Set the shader parameters that it will use for rendering.
            if (!SetShaderParameters(deviceContext, worldMatrix, viewMatrix, projectionMatrix, _ShaderResource))
                return false;


            //if (_initDirectionsArray == 1)
            //{
                //instancesDataRIGHT = new SC_Desk_Screen.DInstanceData[instancesData.Length];
                //instancesDataUP = new SC_Desk_Screen.DInstanceData[instancesData.Length];
            //    _initDirectionsArray = 0;
            //}



            // Now render the prepared buffers with the shader.
            RenderShader(deviceContext, vertexCount, worldMatrix, viewMatrix, projectionMatrix, _DLightBuffer_, oculusRiftDir, _ShaderResource);

            return true;
        }



        int _switchOnce = 0;

        private bool SetShaderParameters(DeviceContext deviceContext, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, SharpDX.Direct3D11.ShaderResourceView _ShaderResource)
        {
            try
            {


                worldMatrix.Transpose();
                viewMatrix.Transpose();
                projectionMatrix.Transpose();

                // Lock the constant buffer so it can be written to.
                DataStream mappedResource;
                deviceContext.MapSubresource(ConstantMatrixBuffer, MapMode.WriteDiscard, MapFlags.None, out mappedResource);

                // Copy the passed in matrices into the constant buffer.
                DMatrixBuffer matrixBuffer = new DMatrixBuffer()
                {
                    world = worldMatrix,
                    view = viewMatrix,
                    projection = projectionMatrix
                };
                mappedResource.Write(matrixBuffer);

                // Unlock the constant buffer.
                deviceContext.UnmapSubresource(ConstantMatrixBuffer, 0);

                // Set the position of the constant buffer in the vertex shader.
                int bufferPositionNumber = 0;

                // Finally set the constant buffer in the vertex shader with the updated values.
                deviceContext.VertexShader.SetConstantBuffer(bufferPositionNumber, ConstantMatrixBuffer);

                // Set shader resource in the pixel shader.
                deviceContext.PixelShader.SetShaderResource(0, _ShaderResource);

                /*// Transpose the matrices to prepare them for shader.
                worldMatrix.Transpose();
                viewMatrix.Transpose();
                projectionMatrix.Transpose();
                DataStream mappedResource;

                /*if (_switchOnce == 0)
                {
                    try
                    {
                        deviceContext.MapSubresource(_constantLightBuffer, MapMode.WriteDiscard, MapFlags.None, out mappedResource);
                        mappedResource.WriteRange(_DLightBuffer, 0, _DLightBuffer.Length);
                        deviceContext.UnmapSubresource(_constantLightBuffer, 0);
                        mappedResource.Dispose();
                        // Lock the constant buffer so it can be written to.
                        //DataStream mappedResource;
                        //deviceContext.MapSubresource(_constantLightBuffer, MapMode.WriteDiscard, MapFlags.None, out mappedResource);
                        //deviceContext.UnmapSubresource(_constantLightBuffer, 0);
                        //mappedResource.Dispose();
                    }
                    catch (Exception ex)
                    {
                        SC_Console_GRAPHICS.MessageBox((IntPtr)0, ex.ToString() + "", "Oculus Error", 0);
                    }
                    _switchOnce = 1;
                }
                // Lock the constant buffer so it can be written to.



                // Lock the constant buffer so it can be written to.
                //DataStream mappedResource;
                deviceContext.MapSubresource(ConstantMatrixBuffer, MapMode.WriteDiscard, MapFlags.None, out mappedResource);

                // Copy the passed in matrices into the constant buffer.
                DMatrixBuffer matrixBuffer = new DMatrixBuffer()
                {
                    world = worldMatrix,
                    view = viewMatrix,
                    projection = projectionMatrix
                };
                mappedResource.Write(matrixBuffer);

                // Unlock the constant buffer.
                deviceContext.UnmapSubresource(ConstantMatrixBuffer, 0);
                mappedResource.Dispose();




                // Set the position of the constant buffer in the vertex shader.
                int bufferPositionNumber = 0;
                // Finally set the constant buffer in the vertex shader with the updated values.
                deviceContext.VertexShader.SetConstantBuffer(bufferPositionNumber, ConstantMatrixBuffer);*/



                //deviceContext.PixelShader.SetShaderResource(0, _ShaderResource);












                /*int instX = 3;
                int instY = 3;
                int instZ = 3;

                for (int x = 0; x < instX; x++)
                {
                    for (int y = 0; y < instY; y++)
                    {
                        for (int z = 0; z < instZ; z++)
                        {
                            //Vector3 position = new Vector3(x, y, z);
                            Vector3 position = new Vector3();
                            position.X = worldMatrix_instances[x + instX * (y + instY * z)].M41;
                            position.Y = worldMatrix_instances[x + instX * (y + instY * z)].M42;
                            position.Z = worldMatrix_instances[x + instX * (y + instY * z)].M43;

                            worldMatrix.M41 = position.X;
                            worldMatrix.M42 = position.Y;
                            worldMatrix.M43 = position.Z;

                            //if (_DLightBuffer[0] != null)
                            {
                                // Transpose the matrices to prepare them for shader.
                                //worldMatrix.Transpose();
                                viewMatrix.Transpose();
                                projectionMatrix.Transpose();
                                DataStream mappedResource;

                                if (_switchOnce == 0)
                                {
                                    try
                                    {
                                        deviceContext.MapSubresource(_constantLightBuffer, MapMode.WriteDiscard, MapFlags.None, out mappedResource);
                                        mappedResource.WriteRange(_DLightBuffer, 0, _DLightBuffer.Length);
                                        deviceContext.UnmapSubresource(_constantLightBuffer, 0);
                                        mappedResource.Dispose();
                                        // Lock the constant buffer so it can be written to.
                                        //DataStream mappedResource;
                                        //deviceContext.MapSubresource(_constantLightBuffer, MapMode.WriteDiscard, MapFlags.None, out mappedResource);
                                        //deviceContext.UnmapSubresource(_constantLightBuffer, 0);
                                        //mappedResource.Dispose();
                                    }
                                    catch (Exception ex)
                                    {
                                        SC_Console_GRAPHICS.MessageBox((IntPtr)0, ex.ToString() + "", "Oculus Error", 0);
                                    }
                                    _switchOnce = 1;
                                }
                                // Lock the constant buffer so it can be written to.



                                // Lock the constant buffer so it can be written to.
                                //DataStream mappedResource;
                                deviceContext.MapSubresource(ConstantMatrixBuffer, MapMode.WriteDiscard, MapFlags.None, out mappedResource);

                                // Copy the passed in matrices into the constant buffer.
                                DMatrixBuffer matrixBuffer = new DMatrixBuffer()
                                {
                                    world = worldMatrix,
                                    view = viewMatrix,
                                    projection = projectionMatrix
                                };
                                mappedResource.Write(matrixBuffer);

                                // Unlock the constant buffer.
                                deviceContext.UnmapSubresource(ConstantMatrixBuffer, 0);

                                // Set the position of the constant buffer in the vertex shader.
                                int bufferPositionNumber = 0;

                                // Finally set the constant buffer in the vertex shader with the updated values.
                                deviceContext.VertexShader.SetConstantBuffer(bufferPositionNumber, ConstantMatrixBuffer);
                            }
                        }
                    }
                    // Set shader resource in the pixel shader.
                    //deviceContext.PixelShader.SetShaderResource(0, texture);
                    _ShaderResource
                }*/




















                return true;
            }
            catch
            {
                return false;
            }
        }


        /*Quaternion ToQuaternion(double yaw, double pitch, double roll) // yaw (Z), pitch (Y), roll (X)
        {
            // Abbreviations for the various angular functions
            double cy = Math.Cos(yaw * 0.5);
            double sy = Math.Sin(yaw * 0.5);
            double cp = Math.Cos(pitch * 0.5);
            double sp = Math.Sin(pitch * 0.5);
            double cr = Math.Cos(roll * 0.5);
            double sr = Math.Sin(roll * 0.5);

            Quaternion q = new Quaternion();
            q.W = (float)(cy * cp * cr + sy * sp * sr);
            q.X = (float)(cy * cp * sr - sy * sp * cr);
            q.Y = (float)(sy * cp * sr + cy * sp * cr);
            q.Z = (float)(sy * cp * cr - cy * sp * sr);

            return q;
        }*/

        //https://en.wikipedia.org/wiki/Conversion_between_quaternions_and_Euler_angles
        /*Vector3 ToEulerAngles(Quaternion q)
        {
            /*Vector3 angles;

            // roll (x-axis rotation)
            double sinr_cosp = 2 * (q.W * q.X + q.Y * q.Z);
            double cosr_cosp = 1 - 2 * (q.X * q.X + q.Y * q.Y);
            angles.Z = (float)Math.Atan2(sinr_cosp, cosr_cosp);

            // pitch (y-axis rotation)
            double sinp = 2 * (q.W * q.Y - q.Z * q.X);
            if (Math.Abs(sinp) >= 1)
                //angles.X = (float)Math.copy(Math.PI / 2, sinp); // use 90 degrees if out of range
            else
                angles.X = (float)Math.Asin(sinp);

            // yaw (z-axis rotation)
            double siny_cosp = 2 * (q.W * q.Z + q.X * q.Y);
            double cosy_cosp = 1 - 2 * (q.Y * q.Y + q.Z * q.Z);
            angles.Y = (float)Math.Atan2(siny_cosp, cosy_cosp);
            return angles;
        }*/

        //if (x< 0.0 ) return y* (-1.0)
        /*double copysign(double x, double y)
        {
            typedef union

    {
                double d;
                unsigned short ds[4];
            }
            s;

            s xt, yt;
            xt.d = x;
            yt.d = y;
            xt.ds[3] = (yt.ds[3] & 0x8000) | (xt.ds[3] & 0x7fff);
            return (xt.d);
        }*/


        private void RenderShader(DeviceContext deviceContext, int indexCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix,SC_skYaRk_VR_V007.SC_Graphics.SC_Desk_Screen.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, SharpDX.Direct3D11.ShaderResourceView _ShaderResource)
        {



            /*device.ImmediateContext.MapSubresource(chunkdat.constantMatrixPosBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streamerTWO);
            streamerTWO.WriteRange(chunkdat.matrixBuffer, 0, chunkdat.matrixBuffer.Length);
            device.ImmediateContext.UnmapSubresource(chunkdat.constantMatrixPosBuffer, 0);
            streamerTWO.Dispose();

            device.ImmediateContext.VertexShader.SetConstantBuffer(0, chunkdat.constantMatrixPosBuffer);
            //device.ImmediateContext.VertexShader.SetConstantBuffer(1, chunkdat.mapBuffer);


            device.ImmediateContext.PixelShader.SetConstantBuffer(0, chunkdat.constantLightBuffer);

            device.ImmediateContext.PixelShader.SetShaderResource(0, shaderResourceView2D);

            device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new[]
            {
                new VertexBufferBinding(VertexBuffer,Marshal.SizeOf(typeof(SC_VR_Chunk.DVertex)), 0),
            });

            device.ImmediateContext.InputAssembler.SetVertexBuffers(1, new[]
            {
                new VertexBufferBinding(chunkdat.instanceBuffer, Marshal.SizeOf(typeof(SC_VR_Chunk.DInstanceType)),0),
            });

            device.ImmediateContext.DrawIndexedInstanced(indexArray.Length, SC_Globals.numberOfInstancesPerObjectInWidth * SC_Globals.numberOfInstancesPerObjectInHeight * SC_Globals.numberOfInstancesPerObjectInDepth, 0, 0, 0);
            */

            /**/

            //_instX = 3;
            //_instY = 3;
            //_instZ = 3;








            //deviceContext.PixelShader.SetConstantBuffer(0, _constantLightBuffer);
            // Set the vertex input layout.
            deviceContext.InputAssembler.InputLayout = Layout;

            // Set the vertex and pixel shaders that will be used to render this triangle.
            deviceContext.VertexShader.Set(VertexShader);
            //deviceContext.VertexShader.SetConstantBuffer(1, InstanceRotationBuffer);

            deviceContext.PixelShader.Set(PixelShader);
            //deviceContext.PixelShader.SetConstantBuffer(0, _constantLightBuffer);


            // Set the sampler state in the pixel shader.
            deviceContext.PixelShader.SetSampler(0, SamplerState);

            // Render the triangle.
            //deviceContext.DrawInstanced(vertexCount, instanceCount, 0, 0);
            //deviceContext.DrawIndexedInstanced(indexCount, instanceCount, 0, 0, 0);
            deviceContext.DrawIndexed(indexCount,0,0);



        }
    }
}