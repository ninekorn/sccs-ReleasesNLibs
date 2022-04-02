using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

//using SC_skYaRk_VR_V007.SC_Graphics.SC_Textures.SC_VR_Touch_Textures;

using System.Linq;
using System;



using Jitter.Collision;
using Jitter;
using Jitter.Dynamics;
using Jitter.DataStructures;
using Jitter.Collision.Shapes;



using System.Runtime.InteropServices;

namespace SC_skYaRk_VR_V007.SC_Graphics
{
    public class SC_Cube_Shader : ITransform, IComponent
    {
        public ITransform transform { get; private set; }
        IComponent ITransform.Component
        {
            get => component;
        }
        IComponent component;
        RigidBody IComponent.rigidbody { get; set; }

        SoftBody IComponent.softbody { get; set; }

        public Matrix _POSITION { get; set; }

        public float RotationY { get; set; }
        public float RotationX { get; set; }
        public float RotationZ { get; set; }


        private SharpDX.Direct3D11.Buffer InstanceBuffer { get; set; }


        private SharpDX.Direct3D11.Buffer VertexBuffer { get; set; }
        private SharpDX.Direct3D11.Buffer IndexBuffer { get; set; }
        private int VertexCount { get; set; }
        public int IndexCount { get; set; }

        //public SC_VR_Cube.DVertex[] Vertices { get; set; }
        public int[] indices;


        [StructLayout(LayoutKind.Sequential)]
        public struct DVertex
        {
            public static int AppendAlignedElement = 12;
            public Vector3 position;
            public Vector4 color;
            public Vector3 normal;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct DMatrixBuffer
        {
            public Matrix world;
            public Matrix view;
            public Matrix projection;
        }


        public DVertex[] Vertices { get; set; }
        
        public SharpDX.Vector3 Position { get; set; }
        public SharpDX.Quaternion Rotation { get; set; }
        public SharpDX.Vector3 Forward { get; set; }

        private float _sizeX = 0;
        private float _sizeY = 0;
        private float _sizeZ = 0;

        // Constructor
        public SC_Cube_Shader() { }

        public Vector4 _color;

        [StructLayout(LayoutKind.Sequential)]
        public struct DInstanceType
        {
            public Vector3 position;
        };

        //public float InstanceCount = 0;
        public int InstanceCount { get; set; }


        // Methods.
        public bool Initialize(SharpDX.Direct3D11.Device device, float x, float y, float z, Vector4 color, Matrix matter)
        {
            //transform = this;
            //component = this;

            //_instX = secondLayerX;
            //_instY = secondLayerY;
            //_instZ = secondLayerZ;

            //_this_original_cube = this;
            //_tranform_instances = new List<SC_Transform>();

            _POSITION = matter;
            transform = this;
            component = this;


            this._color = color;
            this._sizeX = x;
            this._sizeY = y;
            this._sizeZ = z;


            // Initialize the vertex and index buffer that hold the geometry for the triangle.
            return InitializeBuffer(device);
        }
        public void ShutDown()
        {
            // Release the vertex and index buffers.
            ShutDownBuffers();
        }
        public void Render(DeviceContext deviceContext)
        {
            // Put the vertex and index buffers on the graphics pipeline to prepare for drawings.
            RenderBuffers(deviceContext);
        }



        private bool InitializeBuffer(SharpDX.Direct3D11.Device device)
        {
            try
            {



                /*  // Near
                  new Vector4(1, 1, -1, 1), new Vector4(1, 0, 0, 1),	
              new Vector4(1, -1, -1, 1), new Vector4(1, 0, 0, 1),	
              new Vector4(-1, -1, -1, 1), new Vector4(1, 0, 0, 1),	
              new Vector4(-1, 1, -1, 1), new Vector4(1, 0, 0, 1),	
              new Vector4(1, 1, -1, 1), new Vector4(1, 0, 0, 1),	
              new Vector4(-1, -1, -1, 1), new Vector4(1, 0, 0, 1),	







              // Far
              new Vector4(-1, -1, 1, 1), new Vector4(0, 1, 0, 1),	
              new Vector4(1, -1, 1, 1), new Vector4(0, 1, 0, 1),	
              new Vector4(1, 1, 1, 1), new Vector4(0, 1, 0, 1),	
              new Vector4(1, 1, 1, 1), new Vector4(0, 1, 0, 1),	
              new Vector4(-1, 1, 1, 1), new Vector4(0, 1, 0, 1),	
              new Vector4(-1, -1, 1, 1), new Vector4(0, 1, 0, 1),	








              // Left
              new Vector4(-1, 1, 1, 1), new Vector4(0, 0, 1, 1),	
              new Vector4(-1, 1, -1, 1), new Vector4(0, 0, 1, 1),	
              new Vector4(-1, -1, -1, 1), new Vector4(0, 0, 1, 1),	
              new Vector4(-1, -1, -1, 1), new Vector4(0, 0, 1, 1),	
              new Vector4(-1, -1, 1, 1), new Vector4(0, 0, 1, 1),	
              new Vector4(-1, 1, 1, 1), new Vector4(0, 0, 1, 1),	

              // Right
              new Vector4(1, -1, -1, 1), new Vector4(1, 1, 0, 1),	
              new Vector4(1, 1, -1, 1), new Vector4(1, 1, 0, 1),	
              new Vector4(1, 1, 1, 1), new Vector4(1, 1, 0, 1),	
              new Vector4(1, 1, 1, 1), new Vector4(1, 1, 0, 1),	
              new Vector4(1, -1, 1, 1), new Vector4(1, 1, 0, 1),	
              new Vector4(1, -1, -1, 1), new Vector4(1, 1, 0, 1),	

              // Bottom
              new Vector4(-1, -1, -1, 1), new Vector4(1, 0, 1, 1),	
              new Vector4(1, -1, -1, 1), new Vector4(1, 0, 1, 1),	
              new Vector4(1, -1, 1, 1), new Vector4(1, 0, 1, 1),	
              new Vector4(1, -1, 1, 1), new Vector4(1, 0, 1, 1),	
              new Vector4(-1, -1, 1, 1), new Vector4(1, 0, 1, 1),	
              new Vector4(-1, -1, -1, 1), new Vector4(1, 0, 1, 1),	

              // Top
              new Vector4(1, 1, 1, 1), new Vector4(0, 1, 1, 1),	
              new Vector4(1, 1, -1, 1), new Vector4(0, 1, 1, 1),	
              new Vector4(-1, 1, -1, 1), new Vector4(0, 1, 1, 1),	
              new Vector4(-1, 1, -1, 1), new Vector4(0, 1, 1, 1),	
              new Vector4(-1, 1, 1, 1), new Vector4(0, 1, 1, 1),
              new Vector4(1, 1, 1, 1), new Vector4(0, 1, 1, 1)
              */





                Vertices = new[]
                {
                    //FACE NEAR
                    new DVertex()
                    {
                        position = new Vector3(1, 1, -1) * _sizeX,
                        normal = new Vector3(1, 0, 0),
                        color = _color,
                    },
                     new DVertex()
                     {
                         position = new Vector3(1, -1, -1) * _sizeX,
                          normal = new Vector3(1, 0, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1, -1, -1) * _sizeX,
                          normal = new Vector3(1, 0, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1, 1, -1) * _sizeX,
                          normal = new Vector3(1, 0, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1, 1, -1) * _sizeX,
                          normal = new Vector3(1, 0, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1, -1, -1) * _sizeX,
                          normal = new Vector3(1, 0, 0),
                         color = _color,
                     },



                     //FACE FAR
                     new DVertex()
                     {
                         position = new Vector3(-1, -1, 1) * _sizeX,
                         normal = new Vector3(0, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1, -1, 1) * _sizeX,
                                        normal = new Vector3(0, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1, 1, 1) * _sizeX,
                                        normal = new Vector3(0, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1, 1, 1) * _sizeX,
                                        normal = new Vector3(0, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1, 1, 1) * _sizeX,
                                        normal = new Vector3(0, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1, -1, 1) * _sizeX,
                                        normal = new Vector3(0, 1, 0),
                         color = _color,
                     },






                     //FACE LEFT
                     new DVertex()
                     {
                         position = new Vector3(-1, 1, 1) * _sizeX,
                         normal = new Vector3(0, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1, 1, -1) * _sizeX,
                          normal = new Vector3(0, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1, -1, -1) * _sizeX,
                          normal = new Vector3(0, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1, -1, -1) * _sizeX,
                          normal = new Vector3(0, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1, -1, 1) * _sizeX,
                          normal = new Vector3(0, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1, 1, 1) * _sizeX,
                          normal = new Vector3(0, 0, 1),
                         color = _color,
                     },




                     //FACE RIGHT
                     new DVertex()
                     {
                         position = new Vector3(1, -1, -1) * _sizeX,
                         normal = new Vector3(1, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1, 1, -1) * _sizeX,
                                  normal = new Vector3(1, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1, 1, 1) * _sizeX,
                                  normal = new Vector3(1, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1, 1, 1) * _sizeX,
                                  normal = new Vector3(1, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1, -1, 1) * _sizeX,
                                  normal = new Vector3(1, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1, -1, -1) * _sizeX,
                                  normal = new Vector3(1, 1, 0),
                         color = _color,
                     },




                     //BOTTOM
                     new DVertex()
                     {
                         position = new Vector3(-1, -1, -1) * _sizeX,
                         normal = new Vector3(1, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1, -1, -1) * _sizeX,
                                      normal = new Vector3(1, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1, -1, 1) * _sizeX,
                                      normal = new Vector3(1, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1, -1, 1) * _sizeX,
                                      normal = new Vector3(1, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1, -1, 1) * _sizeX,
                                      normal = new Vector3(1, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1, -1, -1) * _sizeX,
                                      normal = new Vector3(1, 0, 1),
                         color = _color,
                     },


                     //TOP
                      new DVertex()
                      {
                          position = new Vector3(1, 1, 1) * _sizeX,
                          normal = new Vector3(0, 1, 1),
                          color = _color,
                      },
                     new DVertex()
                     {
                         position = new Vector3(1, 1, -1) * _sizeX,
                         normal = new Vector3(0, 1, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1, 1, -1) * _sizeX,
                         normal = new Vector3(0, 1, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1, 1, -1) * _sizeX,
                         normal = new Vector3(0, 1, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1, 1, 1) * _sizeX,
                         normal = new Vector3(0, 1, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1, 1, 1) * _sizeX,
                         normal = new Vector3(0, 1, 1),
                         color = _color,
                     },
                    /*//FACE NEAR
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, 1*_sizeZ),
                         color = _color,

                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, 1*_sizeZ),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, 1*_sizeZ),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, 1*_sizeZ),
                         color = _color,
                     },

                     //FACE FAR
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, -1*_sizeZ),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, -1*_sizeZ),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, -1*_sizeZ),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, -1*_sizeZ),
                         color = _color,
                     },


                     //FACE RIGHT
                    new DVertex()
                    {
                        position = new Vector3(1, 1, -1) * _sizeX,
                        normal = new Vector3(1, 0, 0),
                        color = _color,
                    },
                     new DVertex()
                     {
                         position = new Vector3(1, -1, -1) * _sizeX,
                          normal = new Vector3(1, 0, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1, -1, 1) * _sizeX,
                          normal = new Vector3(1, 0, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1, 1, 1) * _sizeX,
                          normal = new Vector3(1, 0, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1, 1, -1) * _sizeX,
                          normal = new Vector3(1, 0, 0),
                         color = _color,
                     },*/
                 };







                /*new DVertex()
                {
                    position = new Vector3(1, 1, -1) * _sizeX,
                    normal = new Vector3(1, 0, 0),
                    color = _color,
                },
                 new DVertex()
                 {
                     position = new Vector3(1, -1, -1) * _sizeX,
                      normal = new Vector3(1, 0, 0),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(-1, -1, -1) * _sizeX,
                      normal = new Vector3(1, 0, 0),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(-1, 1, -1) * _sizeX,
                      normal = new Vector3(1, 0, 0),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(1, 1, -1) * _sizeX,
                      normal = new Vector3(1, 0, 0),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(-1, -1, -1) * _sizeX,
                      normal = new Vector3(1, 0, 0),
                     color = _color,
                 },




                 new DVertex()
                 {
                     position = new Vector3(-1, -1, 1) * _sizeX,
                     normal = new Vector3(0, 0, 1),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(1, -1, 1) * _sizeX,
                                    normal = new Vector3(0, 1, 0),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(1, 1, 1) * _sizeX,
                                    normal = new Vector3(0, 1, 0),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(1, 1, 1) * _sizeX,
                                    normal = new Vector3(0, 1, 0),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(-1, 1, 1) * _sizeX,
                                    normal = new Vector3(0, 1, 0),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(-1, -1, 1) * _sizeX,
                                    normal = new Vector3(0, 1, 0),
                     color = _color,
                 },


                 new DVertex()
                 {
                     position = new Vector3(-1, 1, 1) * _sizeX,
                     normal = new Vector3(0, 0, 1),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(-1, 1, -1) * _sizeX,
                      normal = new Vector3(0, 0, 1),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(-1, -1, -1) * _sizeX,
                      normal = new Vector3(0, 0, 1),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(-1, -1, -1) * _sizeX,
                      normal = new Vector3(0, 0, 1),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(-1, -1, 1) * _sizeX,
                      normal = new Vector3(0, 0, 1),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(-1, 1, 1) * _sizeX,
                      normal = new Vector3(0, 0, 1),
                     color = _color,
                 },


                 new DVertex()
                 {
                     position = new Vector3(1, -1, -1) * _sizeX,
                     normal = new Vector3(1, 1, 0),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(1, 1, -1) * _sizeX,
                              normal = new Vector3(1, 1, 0),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(1, 1, 1) * _sizeX,
                              normal = new Vector3(1, 1, 0),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(1, 1, 1) * _sizeX,
                              normal = new Vector3(1, 1, 0),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(1, -1, 1) * _sizeX,
                              normal = new Vector3(1, 1, 0),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(1, -1, -1) * _sizeX,
                              normal = new Vector3(1, 1, 0),
                     color = _color,
                 },




                 //BOTTOM
                 new DVertex()
                 {
                     position = new Vector3(-1, -1, -1) * _sizeX,
                     normal = new Vector3(1, 0, 1),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(1, -1, -1) * _sizeX,
                                  normal = new Vector3(1, 0, 1),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(1, -1, 1) * _sizeX,
                                  normal = new Vector3(1, 0, 1),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(1, -1, 1) * _sizeX,
                                  normal = new Vector3(1, 0, 1),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(-1, -1, 1) * _sizeX,
                                  normal = new Vector3(1, 0, 1),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(-1, -1, -1) * _sizeX,
                                  normal = new Vector3(1, 0, 1),
                     color = _color,
                 },


                 //TOP
                  new DVertex()
                  {
                      position = new Vector3(1, 1, 1) * _sizeX,
                      normal = new Vector3(0, 1, 1),
                      color = _color,
                  },
                 new DVertex()
                 {
                     position = new Vector3(1, 1, -1) * _sizeX,
                     normal = new Vector3(0, 1, 1),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(-1, 1, -1) * _sizeX,
                     normal = new Vector3(0, 1, 1),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(-1, 1, -1) * _sizeX,
                     normal = new Vector3(0, 1, 1),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(-1, 1, 1) * _sizeX,
                     normal = new Vector3(0, 1, 1),
                     color = _color,
                 },
                 new DVertex()
                 {
                     position = new Vector3(1, 1, 1) * _sizeX,
                     normal = new Vector3(0, 1, 1),
                     color = _color,
                 },
            };*/






                // Set number of vertices in the vertex array.


                // Create the vertex array and load it with data.
                /*Vertices = new[]
                {
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, 1*_sizeZ),
                         color = _color,

                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, 1*_sizeZ),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, 1*_sizeZ),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, 1*_sizeZ),
                         color = _color,
                     },


                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, -1*_sizeZ),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, -1*_sizeZ),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, -1*_sizeZ),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, -1*_sizeZ),
                         color = _color,
                     },
                 };*/
                VertexCount = Vertices.Length;
                // Set number of vertices in the index array.

                // Create Indicies to load into the IndexBuffer.
                /*indices = new int[]
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

                indices = new int[]
                {
                     0, // Bottom left.
                     1, // Top middle.
                     2,  // Bottom right.
                     3,
                     4,
                     5,

                     6,
                     7,
                     8,
                     9,
                     10,
                     11,

                     12,
                     13,
                     14,
                     15,
                     16,
                     17,

                     18,
                     19,
                     20,
                     21,
                     22,
                     23,

                     24,
                     25,
                     26,
                     27,
                     28,
                     29,

                     30,
                     31,
                     32,
                     33,
                     34,
                     35
                 };






                IndexCount = indices.Length;
                /*VertexCount = 3;

                // Create the vertex array and load it with data.
                Vertices = new[]
                  {
					// Bottom left.
					    new SC_VR_Touch_Shader.DVertex()
                    {
                        position = new Vector3(-1, -1, 0),
                         color = _color,
                    },
					// Top middle.
					    new SC_VR_Touch_Shader.DVertex()
                    {
                        position = new Vector3(0, 1, 0),
                         color = _color,
                    },
					// Bottom right.
				    new SC_VR_Touch_Shader.DVertex()
                    {
                        position = new Vector3(1, -1, 0),
                         color = _color,
                    }
                };*/





                int width = 1;
                int heigth = 1;
                int depth = 1;

                InstanceCount = width * heigth * depth;
                DInstanceType[] instances = new DInstanceType[width * heigth * depth];

                int counter = 0;
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < heigth; y++)
                    {
                        for (int z = 0; z < depth; z++)
                        {
                            instances[counter] = new DInstanceType()
                            {
                                position = new Vector3(x, y, z),
                            };
                            counter++;
                        }
                    }
                }












                /* DInstanceType[] instances = new DInstanceType[]
                 {
                     new DInstanceType()
                     {
                         position = new Vector3(-0.5f, -0.5f, -3)
                     },
                     new DInstanceType()
                     {
                         position = new Vector3(-0.5f,  0.5f, -3)
                     },
                     new DInstanceType()
                     {
                         position = new Vector3( 0.5f, -0.5f, -3)
                     },
                     new DInstanceType()
                     {
                         position = new Vector3( 0.5f,  0.5f, -3)
                     }
                 };*/



                // Create the vertex buffer.
                VertexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, Vertices);

                // Create the index buffer.
                IndexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, indices);

                InstanceBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, instances);

                // Delete arrays now that they are in their respective vertex and index buffers.
                //Vertices = null;
                //indices = null;

                return true;
            }
            catch
            {
                return false;
            }
        }
        private void ShutDownBuffers()
        {
            // Release the index buffer.
            IndexBuffer?.Dispose();
            IndexBuffer = null;
            // Release the vertex buffer.
            VertexBuffer?.Dispose();
            VertexBuffer = null;

            InstanceBuffer?.Dispose();
            InstanceBuffer = null;
        }
        private void RenderBuffers(DeviceContext deviceContext)
        {

            deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, Utilities.SizeOf<DVertex>(), 0), new VertexBufferBinding(InstanceBuffer, Utilities.SizeOf<DInstanceType>(), 0));

            // Set the vertex buffer to active in the input assembler so it can be rendered.
            //deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, Utilities.SizeOf<SC_VR_Touch_Shader.DVertex>(), 0));

            // Set the index buffer to active in the input assembler so it can be rendered.
            deviceContext.InputAssembler.SetIndexBuffer(IndexBuffer, SharpDX.DXGI.Format.R32_UInt, 0);

            // Set the type of the primitive that should be rendered from this vertex buffer, in this case triangles.
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
        }
    }
}