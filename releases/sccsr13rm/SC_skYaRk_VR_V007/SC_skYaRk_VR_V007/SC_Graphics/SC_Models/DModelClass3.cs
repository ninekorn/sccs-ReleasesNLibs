//using DSharpDXRastertek.Tut37.Graphics.Data;
//using DSharpDXRastertek.Tut37.System;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using System.Runtime.InteropServices;

namespace SC_skYaRk_VR_V007
{
    public class DModeler                 // 161 lines
    {


        public float RotationY { get; set; }
        public float RotationX { get; set; }
        public float RotationZ { get; set; }











        [StructLayout(LayoutKind.Sequential)]
        public struct DVertexType
        {
            public Vector3 position;
            public Vector2 texture;
        };
        [StructLayout(LayoutKind.Sequential)]
        public struct DInstanceType
        {
            public Vector3 position;
        };

        // Properties
        private SharpDX.Direct3D11.Buffer VertexBuffer { get; set; }
        private SharpDX.Direct3D11.Buffer InstanceBuffer { get; set; }
        public int VertexCount { get; set; }
        public int InstanceCount { get; private set; }

        public int IndexCount { get; private set; }
        //public DTexture Texture { get; private set; }
        private SharpDX.Direct3D11.Buffer IndexBuffer { get; set; }
        // Constructor
        public DModeler() { }

        // Methods.
        public bool Initialize(SharpDX.Direct3D11.Device device, float _sizeX, float _sizeY, float _sizeZ) //, string textureFileName
        {





            // Initialize the vertex and index buffer that hold the geometry for the triangle.
            if (!InitializeBuffers(device, _sizeX, _sizeY, _sizeZ))
                return false;

            //if (!LoadTexture(device, textureFileName))
            //    return false;

            return true;
        }

        private bool InitializeBuffers(SharpDX.Direct3D11.Device device, float _sizeX, float _sizeY,float _sizeZ)
        {
            try
            {

                RotationX = 0;
                RotationY = 0;
                RotationZ = 0;

                DVertexType[] vertices = new[]
                {
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, 1*_sizeZ),
                         texture = new Vector2(0, 1),
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, 1*_sizeZ),
                         texture = new Vector2(0, 1),
                     },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, 1*_sizeZ),
                         texture = new Vector2(0, 1),
                     },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, 1*_sizeZ),
                         texture = new Vector2(0, 1),
                     },

                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, -1*_sizeZ),
                         texture = new Vector2(0, 1),
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, -1*_sizeZ),
                         texture = new Vector2(0, 1),
                     },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX,-1*_sizeY, -1*_sizeZ),
                         texture = new Vector2(0, 1),
                     },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, -1*_sizeZ),
                         texture = new Vector2(0, 1),
                     }
                };

                VertexCount = vertices.Length;
                // Set the number of instances in the array.

                DInstanceType[] instances = new DInstanceType[]
                {
                    new DInstanceType()
                    {
                        position = new Vector3(0, 0, 0)
                    },
                };
 
                InstanceCount = instances.Length;

                // Create Indicies to load into the IndexBuffer
                int[] indices = new int[]
                {
                     0, 
                     1, 
                     2,
                     3,
                     2,
                     1,

                     1,
                     5,
                     3,
                     7,
                     3,
                     5,

                     2,
                     3,
                     6,
                     7,
                     6,
                     3,

                     6,
                     7,
                     4,
                     5,
                     4,
                     7,

                     4,
                     5,
                     0,
                     1,
                     0,
                     5,

                     4,
                     0,
                     6,
                     2,
                     6,
                     0
                 };



                IndexCount = indices.Length;

                VertexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, vertices);             
                InstanceBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, instances);
                IndexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, indices);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /*private bool LoadTexture(SharpDX.Direct3D11.Device device, string textureFileName)
        {
            textureFileName = "../../" + textureFileName;// terrainGrassDirt.bmp";// DSystemConfiguration.DataFilePath + textureFileName;

            // Create the texture object.
            Texture = new DTexture();

            // Initialize the texture object.
            bool result = Texture.Initialize(device, textureFileName);

            return result;
        }*/



        public void ShutDown()
        {
            // Release the model texture.
            ReleaseTexture();

            // Release the vertex and index buffers.
            ShutdownBuffers();
        }
        private void ReleaseTexture()
        {
            // Release the texture object.
            //Texture?.ShutDown();
            //Texture = null;
        }
        private void ShutdownBuffers()
        {
            // Release the Instance buffer.
            InstanceBuffer?.Dispose();
            InstanceBuffer = null;
            // Release the vertex buffer.
            VertexBuffer?.Dispose();
            VertexBuffer = null;

            IndexBuffer?.Dispose();
            IndexBuffer = null;
        }
        public void Render(DeviceContext deviceContext)
        {
            // Put the vertex and index buffers on the graphics pipeline to prepare for drawings.
            RenderBuffers(deviceContext);
        }
        private void RenderBuffers(DeviceContext deviceContext)
        {
            // Set the vertex buffer to active in the input assembler so it can be rendered.
            deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, Utilities.SizeOf<DVertexType>(), 0), new VertexBufferBinding(InstanceBuffer, Utilities.SizeOf<DInstanceType>(), 0));

            deviceContext.InputAssembler.SetIndexBuffer(IndexBuffer, SharpDX.DXGI.Format.R32_UInt, 0);
            // Set the type of the primitive that should be rendered from this vertex buffer, in this case triangles.
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
        }

        static Vector4[] m_vertices = new Vector4[]
        {
			// Near
			new Vector4( 1,  1, -1, 1), new Vector4(1, 0, 0, 1),
            new Vector4( 1, -1, -1, 1), new Vector4(1, 0, 0, 1),
            new Vector4(-1, -1, -1, 1), new Vector4(1, 0, 0, 1),
            new Vector4(-1,  1, -1, 1), new Vector4(1, 0, 0, 1),
            new Vector4( 1,  1, -1, 1), new Vector4(1, 0, 0, 1),
            new Vector4(-1, -1, -1, 1), new Vector4(1, 0, 0, 1),	
			
			// Far
			new Vector4(-1, -1,  1, 1), new Vector4(0, 1, 0, 1),
            new Vector4( 1, -1,  1, 1), new Vector4(0, 1, 0, 1),
            new Vector4( 1,  1,  1, 1), new Vector4(0, 1, 0, 1),
            new Vector4( 1,  1,  1, 1), new Vector4(0, 1, 0, 1),
            new Vector4(-1,  1,  1, 1), new Vector4(0, 1, 0, 1),
            new Vector4(-1, -1,  1, 1), new Vector4(0, 1, 0, 1),	

			// Left
			new Vector4(-1,  1,  1, 1), new Vector4(0, 0, 1, 1),
            new Vector4(-1,  1, -1, 1), new Vector4(0, 0, 1, 1),
            new Vector4(-1, -1, -1, 1), new Vector4(0, 0, 1, 1),
            new Vector4(-1, -1, -1, 1), new Vector4(0, 0, 1, 1),
            new Vector4(-1, -1,  1, 1), new Vector4(0, 0, 1, 1),
            new Vector4(-1,  1,  1, 1), new Vector4(0, 0, 1, 1),	

			// Right
			new Vector4( 1, -1, -1, 1), new Vector4(1, 1, 0, 1),
            new Vector4( 1,  1, -1, 1), new Vector4(1, 1, 0, 1),
            new Vector4( 1,  1,  1, 1), new Vector4(1, 1, 0, 1),
            new Vector4( 1,  1,  1, 1), new Vector4(1, 1, 0, 1),
            new Vector4( 1, -1,  1, 1), new Vector4(1, 1, 0, 1),
            new Vector4( 1, -1, -1, 1), new Vector4(1, 1, 0, 1),	

			// Bottom
			new Vector4(-1, -1, -1, 1), new Vector4(1, 0, 1, 1),
            new Vector4( 1, -1, -1, 1), new Vector4(1, 0, 1, 1),
            new Vector4( 1, -1,  1, 1), new Vector4(1, 0, 1, 1),
            new Vector4( 1, -1,  1, 1), new Vector4(1, 0, 1, 1),
            new Vector4(-1, -1,  1, 1), new Vector4(1, 0, 1, 1),
            new Vector4(-1, -1, -1, 1), new Vector4(1, 0, 1, 1),	

			// Top
			new Vector4( 1,  1,  1, 1), new Vector4(0, 1, 1, 1),
            new Vector4( 1,  1, -1, 1), new Vector4(0, 1, 1, 1),
            new Vector4(-1,  1, -1, 1), new Vector4(0, 1, 1, 1),
            new Vector4(-1,  1, -1, 1), new Vector4(0, 1, 1, 1),
            new Vector4(-1,  1,  1, 1), new Vector4(0, 1, 1, 1),
            new Vector4( 1,  1,  1, 1), new Vector4(0, 1, 1, 1)
        };
    }
}

/*
int[] indices = new int[]
                {
                    0, // Bottom left.
					1, // Top middle.
					2,  // Bottom right.
                    3,
                    2,
                    1,

                    1,
                    5,
                    3,
                    7,
                    3,
                    5,

                    2,
                    3,
                    6,
                    7,
                    6,
                    3,

                    6,
                    7,
                    4,
                    5,
                    4,
                    7,

                    4,
                    5,
                    0,
                    1,
                    0,
                    5,

                    4,
                    0,
                    6,
                    2,
                    6,
                    0
                };*/