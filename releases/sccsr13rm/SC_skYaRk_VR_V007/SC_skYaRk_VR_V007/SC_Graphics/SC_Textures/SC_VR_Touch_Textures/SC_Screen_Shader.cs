//using DSharpDXRastertek.Tut37.System;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using System;
using System.Runtime.InteropServices;
//using System.Windows.Forms;

namespace SC_skYaRk_VR_V007
{
    public class SC_Screen_Shader                 // 228 lines
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);


        public static ShaderResourceView LoadTextureFromFile(SharpDX.Direct3D11.Device device, IntPtr windowsHandle, string filename)
        {
            string ext = System.IO.Path.GetExtension(filename);
            return CreateTextureFromBitmap(device, device.ImmediateContext, filename);
        }
        private static ShaderResourceView CreateTextureFromBitmap(Device device, DeviceContext context, string filename)
        {
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(filename);

            int width = bitmap.Width;
            int height = bitmap.Height;

            Texture2DDescription textureDesc = new Texture2DDescription()
            {
                MipLevels = 1,
                Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm,
                Width = width,
                Height = height,
                ArraySize = 1,
                BindFlags = BindFlags.ShaderResource,
                Usage = ResourceUsage.Default,
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0)
            };

            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            DataRectangle dataRectangle = new DataRectangle(data.Scan0, data.Stride);
            var buffer = new Texture2D(device, textureDesc, dataRectangle);
            bitmap.UnlockBits(data);


            var resourceView = new ShaderResourceView(device, buffer);
            buffer.Dispose();

            return resourceView;
        }

        public Texture2D CreateTexture2DFromBitmap(Device device, SharpDX.WIC.BitmapSource bitmapSource)
        {
            // Allocate DataStream to receive the WIC image pixels
            int stride = bitmapSource.Size.Width * 4;
            using (var buffer = new SharpDX.DataStream(bitmapSource.Size.Height * stride, true, true))
            {
                // Copy the content of the WIC to the buffer
                bitmapSource.CopyPixels(stride, buffer);
                return new SharpDX.Direct3D11.Texture2D(device, new SharpDX.Direct3D11.Texture2DDescription()
                {
                    Width = bitmapSource.Size.Width,
                    Height = bitmapSource.Size.Height,
                    ArraySize = 1,
                    BindFlags = SharpDX.Direct3D11.BindFlags.ShaderResource | BindFlags.RenderTarget,
                    Usage = SharpDX.Direct3D11.ResourceUsage.Default,
                    CpuAccessFlags = SharpDX.Direct3D11.CpuAccessFlags.None,
                    Format = SharpDX.DXGI.Format.R8G8B8A8_UNorm,
                    MipLevels = 1,
                    OptionFlags = ResourceOptionFlags.GenerateMipMaps, // ResourceOptionFlags.GenerateMipMap
                    SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                }, new SharpDX.DataRectangle(buffer.DataPointer, stride));
            }
        }
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
        public ShaderResourceView shaderResourceView2D { get; set; }
        // Constructor
        public SC_Screen_Shader() { }

        // Methods
        public bool Initialize(Device device, IntPtr windowsHandler)
        {
            // Initialize the vertex and pixel shaders.
            return InitializeShader(device, windowsHandler); //, "texture.vs", "texture.ps"
        }
        private bool InitializeShader(Device device, IntPtr windowsHandler) //, string vsFileName, string psFileName
        {
            try
            {

                //shaderResourceView2D = LoadTextureFromFile(device, "../../terrainGrassDirt.bmp");
                // Setup full pathes

                // Compile the Vertex Shader & Pixel Shader code.
                //ShaderBytecode vertexShaderByteCode = ShaderBytecode.CompileFromFile(vsFileName, "TextureVertexShader", DSystemConfiguration.VertexShaderProfile, ShaderFlags.None, EffectFlags.None);
                //ShaderBytecode pixelShaderByteCode = ShaderBytecode.CompileFromFile(psFileName, "TexturePixelShader", DSystemConfiguration.PixelShaderProfile, ShaderFlags.None, EffectFlags.None);



                //vsFileName = SC_skYaRk_VR_Edition_v005.Properties.Resources.InstancedTexture;
                //psFileName = SC_skYaRk_VR_Edition_v005.Properties.Resources.InstancedTexture1;
                //var gsFileNameByteArray = SC_WPF_RENDER.Properties.Resources.HLSL;

                //ShaderBytecode vertexShaderByteCode = ShaderBytecode.Compile(vsFileName, "TextureVertexShader", "vs_5_0", ShaderFlags.None, EffectFlags.None);
                //ShaderBytecode pixelShaderByteCode = ShaderBytecode.Compile(psFileName, "TexturePixelShader", "ps_5_0", ShaderFlags.None, EffectFlags.None);

                try
                {
                    string vsFileName = "../../../InstanceShader/" + "textureScreen.vs";
                    string psFileName = "../../../InstanceShader/" + "textureScreen.ps";

                    ShaderBytecode vertexShaderByteCode = ShaderBytecode.CompileFromFile(vsFileName, "TextureVertexShader", "vs_4_0", ShaderFlags.None, EffectFlags.None);
                    ShaderBytecode pixelShaderByteCode = ShaderBytecode.CompileFromFile(psFileName, "TexturePixelShader", "ps_4_0", ShaderFlags.None, EffectFlags.None);

                    VertexShader = new VertexShader(device, vertexShaderByteCode);
                    PixelShader = new PixelShader(device, pixelShaderByteCode);

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
                        SemanticName = "TEXCOORD",
                        SemanticIndex = 1,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 1,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    }
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

                }
                catch (Exception ex)
                {
                    MessageBox((IntPtr)0, ex.ToString(), "Oculus Error", 0);
                }












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
        public bool Render(DeviceContext deviceContext, int vertexCount, int instanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture)
        {
            // Set the shader parameters that it will use for rendering.
            if (!SetShaderParameters(deviceContext, worldMatrix, viewMatrix, projectionMatrix, texture))
                return false;

            // Now render the prepared buffers with the shader.
            RenderShader(deviceContext, vertexCount, instanceCount);

            return true;
        }
        private bool SetShaderParameters(DeviceContext deviceContext, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture)
        {
            try
            {
                // Transpose the matrices to prepare them for shader.
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
                deviceContext.PixelShader.SetShaderResource(0, texture);

                return true;
            }
            catch
            {
                return false;
            }
        }
        private void RenderShader(DeviceContext deviceContext, int vertexCount, int instanceCount)
        {
            // Set the vertex input layout.
            deviceContext.InputAssembler.InputLayout = Layout;

            // Set the vertex and pixel shaders that will be used to render this triangle.
            deviceContext.VertexShader.Set(VertexShader);
            deviceContext.PixelShader.Set(PixelShader);
            deviceContext.GeometryShader.Set(null);
            // Set the sampler state in the pixel shader.
            deviceContext.PixelShader.SetSampler(0, SamplerState);

            // Render the triangle.
            //deviceContext.DrawInstanced(vertexCount, instanceCount, 0, 0);
            deviceContext.DrawIndexedInstanced(vertexCount, instanceCount, 0, 0, 0);
        }
    }
}