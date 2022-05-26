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


namespace sccsr15forms
{
    internal class tutorialcube: IDisposable
    {
        //public Buffer staticContantBuffer;
        //public Buffer dynamicConstantBuffer;
        public InputLayout layout;
        public PixelShader pixelShader;
        public VertexShader vertexShader;
        public Buffer verticesbuffer;
        public Vector4[] vertices;

        // Create Constant Buffer 
        public Buffer staticContantBufferuithread;
        public Buffer dynamicConstantBufferuithread;

        // Create Constant Buffer 
        public Buffer staticContantBuffersysthread;//
        public Buffer dynamicConstantBuffersysthread;


        public tutorialcube(Device device)
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


            // Instantiate Vertex buiffer from vertex data 
            verticesbuffer = Buffer.Create(device, BindFlags.VertexBuffer, vertices);

            // Create Constant Buffer 
            //staticContantBuffer = new Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
            //dynamicConstantBuffer = new Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

            // Create Constant Buffer 
            staticContantBufferuithread = new Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
            dynamicConstantBufferuithread = new Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

            // Create Constant Buffer 
            staticContantBuffersysthread = new Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
            dynamicConstantBuffersysthread = new Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);



        }


        ~tutorialcube()
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
                /*if (staticContantBuffer != null)
                {
                    staticContantBuffer.Dispose();
                    staticContantBuffer = null;
                }
                if (dynamicConstantBuffer != null)
                {
                    dynamicConstantBuffer.Dispose();
                    dynamicConstantBuffer = null;
                }*/

                if (staticContantBufferuithread != null)
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
