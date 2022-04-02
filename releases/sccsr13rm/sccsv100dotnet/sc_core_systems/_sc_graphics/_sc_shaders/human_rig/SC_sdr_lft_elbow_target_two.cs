//using DSharpDXRastertek.Tut37.System;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using System;
using System.Runtime.InteropServices;
//using System.Windows.Forms;
//using System.Runtime.ex

namespace _sc_core_systems.SC_Graphics
{
    public class SC_sdr_lft_elbow_target_two                 // 228 lines
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
        public SC_sdr_lft_elbow_target_two() { }

        public SharpDX.Direct3D11.Buffer _constantLightBuffer;
        DataStream mappedResourceLight;


        public _sc_core_systems.SC_Graphics.SC_cube.DLightBuffer[] _DLightBuffer;
        // Methods
        public bool Initialize(Device device, IntPtr windowsHandler, SharpDX.Direct3D11.Buffer ConstantLightBuffer, _sc_core_systems.SC_Graphics.SC_cube.DLightBuffer[] DLightBuffer)
        {
            this._DLightBuffer = DLightBuffer;
            this._constantLightBuffer = ConstantLightBuffer;
            // Initialize the vertex and pixel shaders.
            return InitializeShader(device, windowsHandler, "texture.vs", "texture.ps");
        }
        private bool InitializeShader(Device device, IntPtr windowsHandler, string vsFileName, string psFileName)
        {
            try
            {

                vsFileName = "../../../InstanceShader/" + "texture.vs";
                psFileName = "../../../InstanceShader/" + "texture.ps";

                //ShaderBytecode vertexShaderByteCode = ShaderBytecode.CompileFromFile(vsFileName, "TextureVertexShader", "vs_4_0", ShaderFlags.None, EffectFlags.None);
                //ShaderBytecode pixelShaderByteCode = ShaderBytecode.CompileFromFile(psFileName, "TexturePixelShader", "ps_4_0", ShaderFlags.None, EffectFlags.None);
                ShaderBytecode vertexShaderByteCode = ShaderBytecode.CompileFromFile(vsFileName, "TextureVertexShader", "vs_4_0", ShaderFlags.None, EffectFlags.None);
                ShaderBytecode pixelShaderByteCode = ShaderBytecode.CompileFromFile(psFileName, "TexturePixelShader", "ps_4_0", ShaderFlags.None, EffectFlags.None);



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
                        SemanticName = "POSITION",
                        SemanticIndex = 1,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 1,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION", // FORWARD
                        SemanticIndex = 2,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 2,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION", // RIGHT
                        SemanticIndex = 3,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 3,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION", // UP
                        SemanticIndex = 4,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 4,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },


                    /*new InputElement()
                    {
                        SemanticName = "POSITION",
                        SemanticIndex = 3,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 3,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION",
                        SemanticIndex = 4,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 3,
                        AlignedByteOffset =  16,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION",
                        SemanticIndex = 5,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 3,
                        AlignedByteOffset =  32,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION",
                        SemanticIndex = 6,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 3,
                        AlignedByteOffset =  48,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },*/













                    /*new InputElement()
                    {
                        SemanticName = "DEPTH",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 1,
                        AlignedByteOffset =  InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION",
                        SemanticIndex = 2,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 1,
                        AlignedByteOffset =  InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "DEPTH",
                        SemanticIndex = 1,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 1,
                        AlignedByteOffset =  InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },*/
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


        _sc_core_systems.SC_Graphics.SC_modL_lft_elbow_target_two.DInstanceData[] instancesDataRIGHT;// = new SC_modL_lft_elbow_target_two.DInstanceData[];
        _sc_core_systems.SC_Graphics.SC_modL_lft_elbow_target_two.DInstanceData[] instancesDataUP;

        int _initDirectionsArray = 1;

        public bool Render(DeviceContext deviceContext, int vertexCount, int instanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture,_sc_core_systems.SC_Graphics.SC_modL_lft_elbow_target_two.DInstanceType[] instances, _sc_core_systems.SC_Graphics.SC_modL_lft_elbow_target_two.DInstanceData[] instancesData, _sc_core_systems.SC_Graphics.SC_modL_lft_elbow_target_two.DInstanceDataMatrixRotter[] instancesDataRotter, SharpDX.Direct3D11.Buffer InstanceBuffer, SharpDX.Direct3D11.Buffer InstanceRotationBuffer, SharpDX.Direct3D11.Buffer InstanceRotationMatrixBuffer, Matrix[] worldMatrix_instances, int _instX, int _instY, int _instZ, _sc_core_systems.SC_Graphics.SC_cube.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, SharpDX.Direct3D11.Buffer InstanceRotationBufferRIGHT, SharpDX.Direct3D11.Buffer InstanceRotationBufferUP)
        {
            // Set the shader parameters that it will use for rendering.
            if (!SetShaderParameters(deviceContext, worldMatrix, viewMatrix, projectionMatrix, texture, worldMatrix_instances))
                return false;


            if (_initDirectionsArray == 1)
            {
                instancesDataRIGHT = new SC_modL_lft_elbow_target_two.DInstanceData[instancesData.Length];
                instancesDataUP = new SC_modL_lft_elbow_target_two.DInstanceData[instancesData.Length];
                _initDirectionsArray = 0;
            }



            // Now render the prepared buffers with the shader.
            RenderShader(deviceContext, vertexCount, instanceCount, instances, instancesData, instancesDataRotter, InstanceBuffer, InstanceRotationBuffer, InstanceRotationMatrixBuffer, worldMatrix_instances, worldMatrix, viewMatrix, projectionMatrix, _instX, _instY, _instZ, _DLightBuffer_, oculusRiftDir, InstanceRotationBufferRIGHT, InstanceRotationBufferUP);

            return true;
        }



        int _switchOnce = 0;

        private bool SetShaderParameters(DeviceContext deviceContext, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, Matrix[] worldMatrix_instances)
        {
            try
            {




                // Transpose the matrices to prepare them for shader.
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
                }*/
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

                mappedResource.Dispose();
















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


        private void RenderShader(DeviceContext deviceContext, int indexCount, int instanceCount, _sc_core_systems.SC_Graphics.SC_modL_lft_elbow_target_two.DInstanceType[] instances, _sc_core_systems.SC_Graphics.SC_modL_lft_elbow_target_two.DInstanceData[] instancesData, _sc_core_systems.SC_Graphics.SC_modL_lft_elbow_target_two.DInstanceDataMatrixRotter[] instancesDataRotter, SharpDX.Direct3D11.Buffer InstanceBuffer, SharpDX.Direct3D11.Buffer InstanceRotationBuffer, SharpDX.Direct3D11.Buffer InstanceRotationMatrixBuffer, Matrix[] worldMatrix_instances, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix,int _instX, int _instY, int _instZ, _sc_core_systems.SC_Graphics.SC_cube.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, SharpDX.Direct3D11.Buffer InstanceRotationBufferRIGHT, SharpDX.Direct3D11.Buffer InstanceRotationBufferUP)
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

            for (int x = 0; x < instances.Length; x++)
            {
                //Vector3 position = new Vector3(x, y, z);
                Vector4 position = new Vector4();
                position.X = worldMatrix_instances[x].M41;
                position.Y = worldMatrix_instances[x].M42;
                position.Z = worldMatrix_instances[x].M43;
                position.W = 1;

                instances[x].position = position;

                //Quaternion _testQuater;
                //Quaternion.RotationMatrix(ref worldMatrix_instances[x], out _testQuater);
                //_testQuater.Normalize();

                //Vector3 dirInstance = SC_Console_GRAPHICS._getDirection(Vector3.ForwardRH, _testQuater);
                //dirInstance.Normalize();

                //var xq = _testQuater.X;
                //var yq = _testQuater.Y;
                //var zq = _testQuater.Z;
                //var wq = _testQuater.W;

                //var pitch = Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
                //var yaw = Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
                //var roll = Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
               
                //float RotationScreenX = (float)pitch;/// 0.01745327925f;
                //float RotationScreenY = (float)yaw;/// 0.0174532925f;
                //float RotationScreenZ = (float)roll;/// 0.0174532925f;

                //instances[x].direction = new Vector4(RotationScreenX, RotationScreenY, RotationScreenZ, 1);*/
            }


            for (int x = 0; x < instancesData.Length; x++)
            {

                Quaternion _testQuater;
                Quaternion.RotationMatrix(ref worldMatrix_instances[x], out _testQuater);
                _testQuater.Normalize();

                //Matrix rotMatrix;// = worldMatrix_instances[x];
                //Matrix.RotationQuaternion(ref _testQuater, out rotMatrix);


                Vector3 dirInstance = SC_Console_GRAPHICS._getDirection(Vector3.ForwardRH, _testQuater);
                dirInstance.Normalize();
                instancesData[x].rotation = new Vector4(dirInstance.X, dirInstance.Y, dirInstance.Z, 1);

                dirInstance = SC_Console_GRAPHICS._getDirection(Vector3.Right, _testQuater);
                dirInstance.Normalize();
                instancesDataRIGHT[x].rotation = new Vector4(dirInstance.X, dirInstance.Y, dirInstance.Z, 1);


                dirInstance = SC_Console_GRAPHICS._getDirection(Vector3.Up, _testQuater);
                dirInstance.Normalize();
                instancesDataUP[x].rotation = new Vector4(dirInstance.X, dirInstance.Y, dirInstance.Z, 1);

                //var xq = _testQuater.X;
                //var yq = _testQuater.Y;
                //var zq = _testQuater.Z;
                //var wq = _testQuater.W;

                //instancesData[x].rotation = new Vector4(_testQuater.X, _testQuater.Y, _testQuater.Z, _testQuater.W);

                //var pitch = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq); //(float)(180 / Math.PI)
                //var yaw = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq); //(float)(180 / Math.PI) *
                //var roll = (float)Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq); // (float)(180 / Math.PI) *

                //https://www.codeproject.com/Questions/324240/Determining-yaw-pitch-and-roll
                /*public void ExtractYawPitchRoll(out float yaw, out float pitch, out float roll)
                {
                    yaw = (float)Math.Atan2(V02, V22);
                    pitch = (float)Math.Asin(-V12);
                    roll = (float)Math.Atan2(V10, V11);
                }*/
                //Matrix matrix = Matrix.CreateLookAt(source, target, Vector3.Up);


                //float yaw = (float)Math.Atan2(worldMatrix_instances[x].M13, worldMatrix_instances[x].M33); //(float)(180 / Math.PI) * 
                //float pitch = (float)Math.Asin(-worldMatrix_instances[x].M23);
                //float roll = (float)Math.Atan2(worldMatrix_instances[x].M21, worldMatrix_instances[x].M22);



                //instancesData[x].rotation = new Vector4(pitch, yaw, roll, 1);



                //instancesDataRotter[x].rotationMatrix = worldMatrix_instances[x];

                /*Matrix rotMatrix3;

                rotMatrix3.M11 = 1;
                rotMatrix3.M12 = 0;
                rotMatrix3.M13 = 0;
                rotMatrix3.M14 = 0;

                rotMatrix3.M21 = 0;
                rotMatrix3.M22 = 1;
                rotMatrix3.M23 = 0;
                rotMatrix3.M24 = 0;

                rotMatrix3.M31 = 0;
                rotMatrix3.M32 = 0;
                rotMatrix3.M33 = 1;
                rotMatrix3.M34 = 0;

                rotMatrix3.M41 = -currentVertexPos.x;
                rotMatrix3.M42 = -currentVertexPos.y;
                rotMatrix3.M43 = -currentVertexPos.z;
                rotMatrix3.M44 = 1;*/


                //instancesDataRotter[x].instanceRot0 = new Vector4(worldMatrix_instances[x].M11, worldMatrix_instances[x].M12, worldMatrix_instances[x].M13, worldMatrix_instances[x].M14);
                //instancesDataRotter[x].instanceRot1 = new Vector4(worldMatrix_instances[x].M21, worldMatrix_instances[x].M22, worldMatrix_instances[x].M23, worldMatrix_instances[x].M24);
                //instancesDataRotter[x].instanceRot2 = new Vector4(worldMatrix_instances[x].M31, worldMatrix_instances[x].M32, worldMatrix_instances[x].M33, worldMatrix_instances[x].M34);
                //instancesDataRotter[x].instanceRot3 = new Vector4(worldMatrix_instances[x].M41, worldMatrix_instances[x].M42, worldMatrix_instances[x].M43, worldMatrix_instances[x].M44);


                //instancesDataRotter[x].position3 = new Vector4(rotMatrix.M11, rotMatrix.M21, rotMatrix.M31, rotMatrix.M41);
                //instancesDataRotter[x].position4 = new Vector4(rotMatrix.M12, rotMatrix.M22, rotMatrix.M32, rotMatrix.M42);
                //instancesDataRotter[x].position5 = new Vector4(rotMatrix.M13, rotMatrix.M23, rotMatrix.M33, rotMatrix.M43);
                //instancesDataRotter[x].position6 = new Vector4(rotMatrix.M14, rotMatrix.M24, rotMatrix.M34, rotMatrix.M44);


                //instancesData[x].rotation = new Vector4(_testQuater.X, _testQuater.Y, _testQuater.Z, _testQuater.W);
                //instancesData[x].rotation = new Vector4(pitch, yaw, roll, 1);
                //instancesData[x].rotation = new Vector4(dirInstance.X, dirInstance.Y, dirInstance.Z, 1);
                //instancesData[x].rotation = new Vector4(dirInstance.X, dirInstance.Y, dirInstance.Z, 1);
            }


            DataStream mappedResource;
            deviceContext.MapSubresource(InstanceBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(instances, 0, instances.Length);
            deviceContext.UnmapSubresource(InstanceBuffer, 0);
            mappedResource.Dispose();

            DataStream mappedResourcer;
            deviceContext.MapSubresource(InstanceRotationBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourcer);
            mappedResourcer.WriteRange(instancesData, 0, instancesData.Length);
            deviceContext.UnmapSubresource(InstanceRotationBuffer, 0);
            mappedResourcer.Dispose();



            DataStream mappedResourcer2;
            deviceContext.MapSubresource(InstanceRotationBufferRIGHT, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourcer2);
            mappedResourcer2.WriteRange(instancesDataRIGHT, 0, instancesDataRIGHT.Length);
            deviceContext.UnmapSubresource(InstanceRotationBufferRIGHT, 0);
            mappedResourcer2.Dispose();


            DataStream mappedResourcer3;
            deviceContext.MapSubresource(InstanceRotationBufferUP, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourcer3);
            mappedResourcer3.WriteRange(instancesDataUP, 0, instancesDataUP.Length);
            deviceContext.UnmapSubresource(InstanceRotationBufferUP, 0);
            mappedResourcer3.Dispose();




            /*DataStream mappedResourcerer;
            deviceContext.MapSubresource(InstanceRotationMatrixBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourcerer);
            mappedResourcerer.WriteRange(instancesDataRotter, 0, instancesDataRotter.Length);
            deviceContext.UnmapSubresource(InstanceRotationMatrixBuffer, 0);
            mappedResourcerer.Dispose();*/




            /*for (int x = 0; x < instancesData.Length; x++)
            {
                Quaternion _testQuater;
                Quaternion.RotationMatrix(ref worldMatrix_instances[x], out _testQuater);
                _testQuater.Normalize();

                //Vector3 dirInstance = SC_Console_GRAPHICS._getDirection(Vector3.ForwardRH, _testQuater);
                //dirInstance.Normalize();

                var xq = _testQuater.X;
                var yq = _testQuater.Y;
                var zq = _testQuater.Z;
                var wq = _testQuater.W;

                var pitch = Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
                var yaw = Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);
                var roll = Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq);

                float RotationScreenX = (float)pitch;// / 0.01745327925f;
                float RotationScreenY = (float)yaw;/// 0.0174532925f;
                float RotationScreenZ = (float)roll;/// 0.0174532925f;

                instancesData[x].position = new Vector4(RotationScreenX, RotationScreenY, RotationScreenZ, 1);
            }*/



            /*deviceContext.InputAssembler.SetVertexBuffers(1, new[]
            {
                new VertexBufferBinding(InstanceBuffer, Marshal.SizeOf(typeof(SC_modL_lft_elbow_target_two.DInstanceType)),0),
            });
            */






            /*deviceContext.InputAssembler.SetVertexBuffers(2, new[]
            {  
                new VertexBufferBinding(InstanceRotationBuffer, Marshal.SizeOf(typeof(SC_modL_lft_elbow_target_two.DInstanceData)),0),
            });*/





            /*for (int x = 0; x < _instX; x++)
            {
                for (int y = 0; y < _instY; y++)
                {
                    for (int z = 0; z < _instZ; z++)
                    {
                        //Vector3 position = new Vector3(x, y, z);
                        Vector3 position = new Vector3();
                        position.X = worldMatrix_instances[x + _instX * (y + _instY * z)].M41;
                        position.Y = worldMatrix_instances[x + _instX * (y + _instY * z)].M42;
                        position.Z = worldMatrix_instances[x + _instX * (y + _instY * z)].M43;
                        instances[x + _instX * (y + _instY * z)].position = position;
                    }
                }
            }*/



            //Console.WriteLine(_instX);



            if (_switchOnce == 0)
            {
                try
                {         
                    deviceContext.MapSubresource(_constantLightBuffer, MapMode.WriteDiscard, MapFlags.None, out mappedResource);
                    mappedResource.WriteRange(_DLightBuffer_, 0, _DLightBuffer_.Length);
                    deviceContext.UnmapSubresource(_constantLightBuffer, 0);
                    mappedResource.Dispose();
                }
                catch (Exception ex)
                {
                    SC_Console_GRAPHICS.MessageBox((IntPtr)0, ex.ToString() + "", "Oculus Error", 0);
                }
                _switchOnce = 0;
            }



      


         


            //deviceContext.PixelShader.SetConstantBuffer(0, _constantLightBuffer);
            // Set the vertex input layout.
            deviceContext.InputAssembler.InputLayout = Layout;

            // Set the vertex and pixel shaders that will be used to render this triangle.
            deviceContext.VertexShader.Set(VertexShader);
            //deviceContext.VertexShader.SetConstantBuffer(1, InstanceRotationBuffer);

            deviceContext.PixelShader.Set(PixelShader);
            deviceContext.PixelShader.SetConstantBuffer(0, _constantLightBuffer);


            deviceContext.GeometryShader.Set(null);

            // Set the sampler state in the pixel shader.
            deviceContext.PixelShader.SetSampler(0, SamplerState);

            // Render the triangle.
            //deviceContext.DrawInstanced(vertexCount, instanceCount, 0, 0);
            deviceContext.DrawIndexedInstanced(indexCount, instanceCount, 0, 0, 0);

        }
    }
}