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
    public class SC_Cloth_Shader_Final                 // 228 lines
    {
        DataStream mappedResource;
        DataStream mappedResourcer;
        DataStream mappedResourcer2;
        DataStream mappedResourcer3;
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
        public SC_Cloth_Shader_Final() { }

        public SharpDX.Direct3D11.Buffer _constantLightBuffer;
        DataStream mappedResourceLight;


        public SC_skYaRk_VR_V007.SC_Graphics.SC_cube.DLightBuffer[] _DLightBuffer;
        // Methods
        public bool Initialize(Device device, IntPtr windowsHandler, SharpDX.Direct3D11.Buffer ConstantLightBuffer, SC_skYaRk_VR_V007.SC_Graphics.SC_cube.DLightBuffer[] DLightBuffer)
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


        SC_skYaRk_VR_V007.SC_Graphics.SC_cube.DInstanceData[] instancesDataRIGHT;// = new SC_ShaderManager.DInstanceData[];
        SC_skYaRk_VR_V007.SC_Graphics.SC_cube.DInstanceData[] instancesDataUP;

        int _initDirectionsArray = 1;

        public bool Render(DeviceContext deviceContext, int vertexCount, int instanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture,SC_skYaRk_VR_V007.SC_Graphics.SC_cube.DInstanceType[] instances, SC_skYaRk_VR_V007.SC_Graphics.SC_cube.DInstanceData[] instancesData, SC_skYaRk_VR_V007.SC_Graphics.SC_cube.DInstanceDataMatrixRotter[] instancesDataRotter, SharpDX.Direct3D11.Buffer InstanceBuffer, SharpDX.Direct3D11.Buffer InstanceRotationBuffer, SharpDX.Direct3D11.Buffer InstanceRotationMatrixBuffer, Matrix[] worldMatrix_instances, int _instX, int _instY, int _instZ, SC_skYaRk_VR_V007.SC_Graphics.SC_cube.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, SharpDX.Direct3D11.Buffer InstanceRotationBufferRIGHT, SharpDX.Direct3D11.Buffer InstanceRotationBufferUP)
        {
            // Set the shader parameters that it will use for rendering.
            if (!SetShaderParameters(deviceContext, worldMatrix, viewMatrix, projectionMatrix, texture, worldMatrix_instances))
                return false;


            if (_initDirectionsArray == 1)
            {
                instancesDataRIGHT = new SC_cube.DInstanceData[instancesData.Length];
                instancesDataUP = new SC_cube.DInstanceData[instancesData.Length];
                _initDirectionsArray = 0;
            }



            // Now render the prepared buffers with the shader.
            RenderShader(deviceContext, vertexCount, instanceCount, instances, instancesData, instancesDataRotter, InstanceBuffer, InstanceRotationBuffer, InstanceRotationMatrixBuffer, worldMatrix_instances, worldMatrix, viewMatrix, projectionMatrix, _instX, _instY, _instZ, _DLightBuffer_, oculusRiftDir, InstanceRotationBufferRIGHT, InstanceRotationBufferUP);
            
            return true;
        }



        int _switchOnce = 0;
        int bufferPositionNumber = 0;
        DMatrixBuffer matrixBuffer;


        private bool SetShaderParameters(DeviceContext deviceContext, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, Matrix[] worldMatrix_instances)
        {
            try
            {




                // Transpose the matrices to prepare them for shader.
                worldMatrix.Transpose();
                viewMatrix.Transpose();
                projectionMatrix.Transpose();
    

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
                matrixBuffer = new DMatrixBuffer()
                {
                    world = worldMatrix,
                    view = viewMatrix,
                    projection = projectionMatrix
                };
                mappedResource.Write(matrixBuffer);

                // Unlock the constant buffer.
                deviceContext.UnmapSubresource(ConstantMatrixBuffer, 0);

                // Set the position of the constant buffer in the vertex shader.
                mappedResource.Dispose();

                // Finally set the constant buffer in the vertex shader with the updated values.
                deviceContext.VertexShader.SetConstantBuffer(bufferPositionNumber, ConstantMatrixBuffer);
                deviceContext.PixelShader.SetShaderResource(0, texture);







                //texture.Dispose();













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

        Quaternion _testQuater;
        Vector3 dirInstance;
        Vector4 position;// = new Vector4();
        private void RenderShader(DeviceContext deviceContext, int indexCount, int instanceCount, SC_skYaRk_VR_V007.SC_Graphics.SC_cube.DInstanceType[] instances, SC_skYaRk_VR_V007.SC_Graphics.SC_cube.DInstanceData[] instancesData, SC_skYaRk_VR_V007.SC_Graphics.SC_cube.DInstanceDataMatrixRotter[] instancesDataRotter, SharpDX.Direct3D11.Buffer InstanceBuffer, SharpDX.Direct3D11.Buffer InstanceRotationBuffer, SharpDX.Direct3D11.Buffer InstanceRotationMatrixBuffer, Matrix[] worldMatrix_instances, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix,int _instX, int _instY, int _instZ, SC_skYaRk_VR_V007.SC_Graphics.SC_cube.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, SharpDX.Direct3D11.Buffer InstanceRotationBufferRIGHT, SharpDX.Direct3D11.Buffer InstanceRotationBufferUP)
        {
            for (int x = 0; x < instances.Length; x++)
            {
                //Vector3 position = new Vector3(x, y, z);
        
                position.X = worldMatrix_instances[x].M41;
                position.Y = worldMatrix_instances[x].M42;
                position.Z = worldMatrix_instances[x].M43;
                position.W = 1;

                instances[x].position = position;
            }


            for (int x = 0; x < instancesData.Length; x++)
            {
         
                Quaternion.RotationMatrix(ref worldMatrix_instances[x], out _testQuater);



                _testQuater.Normalize();

                dirInstance = SC_Console_GRAPHICS._getDirection(Vector3.ForwardRH, _testQuater);
                dirInstance.Normalize();
                instancesData[x].rotation = new Vector4(dirInstance.X, dirInstance.Y, dirInstance.Z, 1);

                dirInstance = SC_Console_GRAPHICS._getDirection(Vector3.Right, _testQuater);
                dirInstance.Normalize();
                instancesDataRIGHT[x].rotation = new Vector4(dirInstance.X, dirInstance.Y, dirInstance.Z, 1);


                dirInstance = SC_Console_GRAPHICS._getDirection(Vector3.Up, _testQuater);
                dirInstance.Normalize();
                instancesDataUP[x].rotation = new Vector4(dirInstance.X, dirInstance.Y, dirInstance.Z, 1);           
            }


   
            deviceContext.MapSubresource(InstanceBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(instances, 0, instances.Length);
            deviceContext.UnmapSubresource(InstanceBuffer, 0);
            mappedResource.Dispose();


            deviceContext.MapSubresource(InstanceRotationBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourcer);
            mappedResourcer.WriteRange(instancesData, 0, instancesData.Length);
            deviceContext.UnmapSubresource(InstanceRotationBuffer, 0);
            mappedResourcer.Dispose();


            deviceContext.MapSubresource(InstanceRotationBufferRIGHT, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourcer2);
            mappedResourcer2.WriteRange(instancesDataRIGHT, 0, instancesDataRIGHT.Length);
            deviceContext.UnmapSubresource(InstanceRotationBufferRIGHT, 0);
            mappedResourcer2.Dispose();

            deviceContext.MapSubresource(InstanceRotationBufferUP, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourcer3);
            mappedResourcer3.WriteRange(instancesDataUP, 0, instancesDataUP.Length);
            deviceContext.UnmapSubresource(InstanceRotationBufferUP, 0);
            mappedResourcer3.Dispose();



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

            deviceContext.PixelShader.SetSampler(0, SamplerState);

            deviceContext.DrawIndexedInstanced(indexCount, instanceCount, 0, 0, 0);
        }
    }
}