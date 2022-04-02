using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

//using _sc_core_systems.SC_Graphics.SC_Textures.SC_VR_Touch_Textures;

using System.Linq;
using System;



using Jitter.Collision;
using Jitter;
using Jitter.Dynamics;
using Jitter.DataStructures;
using Jitter.Collision.Shapes;



using System.Runtime.InteropServices;


using _sc_core_systems.SC_Graphics;

namespace _sc_core_systems.SC_Graphics
{
    public class SC_modL_rght_elbow_target : ITransform, IComponent
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

        /*struct Spatial
        {
            vec4 pos, rot;
        }; //rotate */


        /*Vector3 qrot(Vector4 q, Vector3 v)
        {
            return v + 2.0f * Vector3.Cross(new Vector3(q.X,q.Y,q.Z), Vector3.Cross(new Vector3(q.X, q.Y, q.Z), v) + q.W * v);
        }*/

        /*Vector3 rotate_vertex_position(Vector3 position, Vector3 axis, float angle)
        {
            Vector4 q = quat_from_axis_angle(axis, angle);
            Vector3 v = position.xyz;
            return v + 2.0 * cross(q.xyz, cross(q.xyz, v) + q.w * v);
        }*/


        // Properties
        private SharpDX.Direct3D11.Buffer VertexBuffer { get; set; }
        private SharpDX.Direct3D11.Buffer IndexBuffer { get; set; }
        private int VertexCount { get; set; }
        public int IndexCount { get; set; }

        private float _touchSize = 1f;

        //public SharpDX.Vector3 Position { get; set; }
        public SharpDX.Quaternion Rotation { get; set; }
        public SharpDX.Vector3 Forward { get; set; }

        public DVertex[] Vertices { get; set; }

        /*[StructLayout(LayoutKind.Sequential)]
        public struct DVertex
        {
            public static int AppendAlignedElement = 12;
            public Vector3 position;
            public Vector4 color;
            public Vector3 normal;
        }*/

        [StructLayout(LayoutKind.Sequential)]
        public struct DVertex
        {
            public Vector3 position;
            public Vector2 texture;
            public Vector4 color;
            public Vector3 normal;
        };





        [StructLayout(LayoutKind.Sequential)]
        public struct DMatrixBuffer
        {
            public Matrix world;
            public Matrix view;
            public Matrix projection;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DInstanceType
        {
            public Vector4 position;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct DInstanceData
        {
            public Vector4 rotation;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DInstanceDataMatrixRotter
        {
            //public Matrix rotationMatrix;
            public Vector4 instanceRot0;
            public Vector4 instanceRot1;
            public Vector4 instanceRot2;
            public Vector4 instanceRot3;
        }




        public int InstanceCount { get; private set; }

        public SharpDX.Direct3D11.Buffer InstanceBuffer { get; set; }
        public SharpDX.Direct3D11.Buffer InstanceRotationBuffer { get; set; }
        public SharpDX.Direct3D11.Buffer InstanceRotationBufferRIGHT { get; set; }
        public SharpDX.Direct3D11.Buffer InstanceRotationBufferUP { get; set; }



        public SharpDX.Direct3D11.Buffer InstanceRotationMatrixBuffer { get; set; }

        float _tileSize = 0;
        int _divX;
        int _divY;


        float _a;
        float _r;
        float _g;
        float _b;
        // Variables
        private int m_TerrainWidth, m_TerrainHeight;

        public Vector4 _color;




        //LIGHTS
        [StructLayout(LayoutKind.Explicit)]
        public struct DLightBuffer
        {
            [FieldOffset(0)]
            public Vector4 ambientColor; //16
            [FieldOffset(16)]
            public Vector4 diffuseColor; //16
            [FieldOffset(32)]
            public Vector3 lightDirection; //12
            [FieldOffset(44)]
            public float padding0;
            [FieldOffset(48)]
            public Vector3 lightPosition; //12
            [FieldOffset(60)]
            public float padding1;
        }
        //[FieldOffset(44)]
        //public Vector3 lightPosition;







        DLightBuffer[] _DLightBuffer = new DLightBuffer[1];


        SC_cube_instances[] _arrayOfInstances;// = new SC_cube_instances[];


        public DInstanceType[] instances;

        public DInstanceData[] instancesData;

        public DInstanceDataMatrixRotter[] instancesDataRotter;
        public int _instX;
        public int _instY;
        public int _instZ;

        public Matrix _ORIGINPOSITION { get; set; }
        public SC_cube_instances _singleObjectOnly;// = new SC_cube_instances();


        public SC_sdr_rght_elbow_target _this_object_texture_shader { get; set; }


        //public Vector3 _y_top_pivot;
        //public Vector3 _y_bottom_pivot;

        public float _total_torso_height = -1;
        public float _total_torso_depth = -1;
        public float _total_torso_width = -1;


        //int _isTerrain;
        // Constructor
        public SC_modL_rght_elbow_target() { }

        public bool Initialize(SC_console_directx D3D, int width, int height, float tileSize, int divX, int divY, float _sizeX, float _sizeY, float _sizeZ, Vector4 color, int instX, int instY, int instZ, IntPtr windowsHandle, Matrix matroxer, int isTerrain, float offsetPosX, float offsetPosY, float offsetPosZ, float offsetVertX, float offsetVertY, float offsetVertZ)
        {
            _ORIGINPOSITION = matroxer;
            _POSITION = matroxer;





            transform = this;
            component = this;
            //_isTerrain = isTerrain;

            this._color = color;
            this._sizeX = _sizeX;
            this._sizeY = _sizeY;
            this._sizeZ = _sizeZ;

            _tileSize = tileSize;
            // Manually set the width and height of the terrain.
            m_TerrainWidth = width;
            m_TerrainHeight = height;

            this._divX = divX;
            this._divY = divY;

            this._a = color.W;
            this._r = color.X;
            this._g = color.Y;
            this._b = color.Z;


            this._instX = instX;
            this._instX = instY;
            this._instX = instZ;

            // Initialize the vertex and index buffer that hold the geometry for the terrain.
            if (!InitializeBuffer(D3D, _sizeX, _sizeY, _sizeZ, tileSize, instX, instY, instZ, windowsHandle, matroxer, isTerrain, offsetPosX, offsetPosY, offsetPosZ, offsetVertX, offsetVertY, offsetVertZ))
                return false;

            return true;
        }



        SharpDX.Direct3D11.Buffer ConstantLightBuffer;

        /*public bool _initTexture(SharpDX.Direct3D11.Device device, IntPtr windowsHandle)
        {

            Vector4 ambientColor = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);
            Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
            Vector3 lightDirection = new Vector3(1, 0, 0);
            Vector3 lightPosition = new Vector3(0, 0, 0);

            _DLightBuffer[0] = new DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };

            _this_object_texture_shader = new SC_sdr_rght_elbow_target();


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

            _this_object_texture_shader.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer);

            // Initialize the texture shader object.
            if (!_this_object_texture_shader.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer))
            {
                return false;
            }
            return true;
        }*/

        /*public bool RenderInstancedObject(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, Matrix[] worldMatrix_instances, DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir)
        {
            /*Vector4 ambientColor = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);
            Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
            Vector3 lightDirection = new Vector3(1, 0, 0);

            _DLightBuffer[0] = new DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding = 0
            };

            // Render the model using the texture shader.

            _this_object_texture_shader.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, instances, instancesData, instancesDataRotter, InstanceBuffer, InstanceRotationBuffer, InstanceRotationMatrixBuffer, worldMatrix_instances, _instX, _instY, _instZ, _DLightBuffer_, oculusRiftDir, InstanceRotationBufferRIGHT, InstanceRotationBufferUP);

            return true;
        }*/

        private float _sizeX = 0;
        private float _sizeY = 0;
        private float _sizeZ = 0;

        public void ShutDown()
        {
            // Release the vertex and index buffers.
            ShutDownBuffers();
        }
        private bool InitializeBuffer(SC_console_directx D3D, float _sizeX, float _sizeY, float _sizeZ, float tileSize, int instX, int instY, int instZ, IntPtr windowsHandle, Matrix matroxer, int isTerrain, float offsetPosX, float offsetPosY, float offsetPosZ, float offsetVertX, float offsetVertY, float offsetVertZ)
        {
            try
            {
                int sizeWidther = (int)(m_TerrainWidth * 0.5f);
                int sizeHeighter = (int)(m_TerrainHeight * 0.5f);

                sizeWidther /= 10;
                sizeHeighter /= 10;

                // Set number of vertices in the vertex array.
                //VertexCount = 8;
                // Set number of vertices in the index array.
                //IndexCount = 36;

                // Create the vertex array and load it with data.

                var someOffsetPos = new Vector3(offsetPosX, offsetPosY, offsetPosZ);

                Vertices = new[]
                {                                   
                    //TOP
                    new DVertex()
                    {
                        position = new Vector3((1*_sizeX) + (offsetVertX * _sizeX), (1*_sizeY) + (offsetVertY * _sizeY), (1*_sizeZ) + (offsetVertZ * _sizeZ)) ,
                        normal = new Vector3(0, 1, 1),
                        color = _color,
                    },
                     new DVertex()
                     {
                         position = new Vector3((1*_sizeX) + (offsetVertX * _sizeX), (1*_sizeY) + (offsetVertY * _sizeY), -(1*_sizeZ) - (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(0, 1, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-(1*_sizeX) - (offsetVertX * _sizeX), (1*_sizeY) + (offsetVertY * _sizeY), -(1*_sizeZ) - (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(0, 1, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-(1*_sizeX) - (offsetVertX * _sizeX), (1*_sizeY) + (offsetVertY * _sizeY), -(1*_sizeZ) - (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(0, 1, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-(1*_sizeX) - (offsetVertX * _sizeX), (1*_sizeY) + (offsetVertY * _sizeY), (1*_sizeZ) + (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(0, 1, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3((1*_sizeX) + (offsetVertX * _sizeX), (1*_sizeY) + (offsetVertY * _sizeY), (1*_sizeZ) + (offsetVertZ * _sizeZ)),
                         normal = new Vector3(0, 1, 1),
                         color = _color,
                     },

                     //BOTTOM
                     new DVertex()
                     {
                         position = new Vector3(-(1*_sizeX) - (offsetVertX * _sizeX), -(1*_sizeY) - (offsetVertY * _sizeY), -(1*_sizeZ) - (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(1, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3((1*_sizeX) + (offsetVertX * _sizeX), -(1*_sizeY) - (offsetVertY * _sizeY), -(1*_sizeZ) - (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(1, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3((1*_sizeX) + (offsetVertX * _sizeX), -(1*_sizeY) - (offsetVertY * _sizeY), (1*_sizeZ) + (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(1, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3((1*_sizeX) + (offsetVertX * _sizeX), -(1*_sizeY) - (offsetVertY * _sizeY), (1*_sizeZ) + (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(1, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-(1*_sizeX) - (offsetVertX * _sizeX), -(1*_sizeY) - (offsetVertY * _sizeY), (1*_sizeZ) + (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(1, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-(1*_sizeX) - (offsetVertX * _sizeX), -(1*_sizeY) - (offsetVertY * _sizeY), -(1*_sizeZ) - (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(1, 0, 1),
                         color = _color,
                     },

                    
                    
                    //FACE NEAR
                    new DVertex()
                    {
                        position = new Vector3((1*_sizeX) + (offsetVertX * _sizeX), (1*_sizeY) + (offsetVertY * _sizeY), -(1*_sizeZ) - (offsetVertZ * _sizeZ)) ,
                        normal = new Vector3(1, 0, 0),
                        color = _color,
                    },
                     new DVertex()
                     {
                         position = new Vector3((1*_sizeX) + (offsetVertX * _sizeX), -(1*_sizeY) - (offsetVertY * _sizeY), -(1*_sizeZ) - (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(1, 0, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-(1*_sizeX) - (offsetVertX * _sizeX), -(1*_sizeY) - (offsetVertY * _sizeY), -(1*_sizeZ) - (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(1, 0, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-(1*_sizeX) - (offsetVertX * _sizeX), (1*_sizeY) + (offsetVertY * _sizeY), -(1*_sizeZ) - (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(1, 0, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3((1*_sizeX) + (offsetVertX * _sizeX), (1*_sizeY) + (offsetVertY * _sizeY), -(1*_sizeZ) - (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(1, 0, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-(1*_sizeX) - (offsetVertX * _sizeX), -(1*_sizeY) - (offsetVertY * _sizeY), -(1*_sizeZ) - (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(1, 0, 0),
                         color = _color,
                     },



                     //FACE FAR
                     new DVertex()
                     {
                         position = new Vector3(-(1*_sizeX) - (offsetVertX * _sizeX), -(1*_sizeY) - (offsetVertY * _sizeY), (1*_sizeZ) + (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(0, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3((1*_sizeX) + (offsetVertX * _sizeX), -(1*_sizeY) - (offsetVertY * _sizeY), (1*_sizeZ) + (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(0, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3((1*_sizeX) + (offsetVertX * _sizeX), (1*_sizeY) + (offsetVertY * _sizeY), (1*_sizeZ) + (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(0, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3((1*_sizeX) + (offsetVertX * _sizeX), (1*_sizeY) + (offsetVertY * _sizeY), (1*_sizeZ) + (offsetVertZ * _sizeZ)),
                         normal = new Vector3(0, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-(1*_sizeX) - (offsetVertX * _sizeX), (1*_sizeY) + (offsetVertY * _sizeY), (1*_sizeZ) + (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(0, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-(1*_sizeX) - (offsetVertX * _sizeX), -(1*_sizeY) - (offsetVertY * _sizeY), (1*_sizeZ) + (offsetVertZ * _sizeZ)),
                         normal = new Vector3(0, 1, 0),
                         color = _color,
                     },






                     //FACE LEFT
                     new DVertex()
                     {
                         position = new Vector3(-(1*_sizeX) - (offsetVertX * _sizeX), (1*_sizeY) + (offsetVertY * _sizeY), (1*_sizeZ) + (offsetVertZ * _sizeZ)),
                         normal = new Vector3(0, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-(1*_sizeX) - (offsetVertX * _sizeX), (1*_sizeY) + (offsetVertY * _sizeY), -(1*_sizeZ) - (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(0, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-(1*_sizeX) - (offsetVertX * _sizeX), -(1*_sizeY) - (offsetVertY * _sizeY), -(1*_sizeZ) - (offsetVertZ * _sizeZ)),
                         normal = new Vector3(0, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-(1*_sizeX) - (offsetVertX * _sizeX), -(1*_sizeY) - (offsetVertY * _sizeY), -(1*_sizeZ) - (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(0, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-(1*_sizeX) - (offsetVertX * _sizeX), -(1*_sizeY) - (offsetVertY * _sizeY), (1*_sizeZ) + (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(0, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-(1*_sizeX) + (offsetVertX * _sizeX), (1*_sizeY) + (offsetVertY * _sizeY), (1*_sizeZ) + (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(0, 0, 1),
                         color = _color,
                     },




                     //FACE RIGHT
                     new DVertex()
                     {
                         position = new Vector3((1*_sizeX) + (offsetVertX * _sizeX), -(1*_sizeY) - (offsetVertY * _sizeY), -(1*_sizeZ) - (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(1, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3((1*_sizeX) + (offsetVertX * _sizeX), (1*_sizeY) + (offsetVertY * _sizeY), -(1*_sizeZ) - (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(1, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3((1*_sizeX) + (offsetVertX * _sizeX), (1*_sizeY) + (offsetVertY * _sizeY), (1*_sizeZ) + (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(1, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3((1*_sizeX) + (offsetVertX * _sizeX), (1*_sizeY) + (offsetVertY * _sizeY), (1*_sizeZ) + (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(1, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3((1*_sizeX) + (offsetVertX * _sizeX), -(1*_sizeY) - (offsetVertY * _sizeY), (1*_sizeZ) + (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(1, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3((1*_sizeX) + (offsetVertX * _sizeX), -(1*_sizeY) - (offsetVertY * _sizeY), -(1*_sizeZ) - (offsetVertZ * _sizeZ)) ,
                         normal = new Vector3(1, 1, 0),
                         color = _color,
                     },
                 };



                Vector3[] sorterList = new Vector3[Vertices.Length];

                for (int i = 0; i < Vertices.Length; i++)
                {
                    sorterList[i] = Vertices[i].position;
                }

                var lowestX = sorterList.OrderBy(x => x.X).FirstOrDefault();
                var highestX = sorterList.OrderBy(x => x.X).Last();
                var lowestY = sorterList.OrderBy(y => y.X).FirstOrDefault();
                var highestY = sorterList.OrderBy(y => y.X).Last();
                var lowestZ = sorterList.OrderBy(z => z.X).FirstOrDefault();
                var highestZ = sorterList.OrderBy(z => z.X).Last();

                //_total_torso_width = highestX.X - lowestX.X;
                //_total_torso_height = highestY.Y - lowestY.Y;
                //_total_torso_depth = highestZ.Z - lowestZ.Z;

                //_y_top_pivot = highestY;
                //_y_bottom_pivot = lowestY;

                _total_torso_width = ((1 * _sizeX) + (offsetVertX * _sizeX) * 2);
                _total_torso_height = ((1 * _sizeY) + (offsetVertY * _sizeY) * 2);
                _total_torso_depth = ((1 * _sizeZ) + (offsetVertZ * _sizeZ) * 2);








                int[] triangles = new int[]
                {
                    5,4,3,2,1,0,
                    11,10,9,8,7,6,
                    17,16,15,14,13,12,
                    23,22,21,20,19,18,
                    29,28,27,26,25,24,
                    35,34,33,32,31,30,
                 };




                int count = 0;
                IndexCount = triangles.Length;
                VertexCount = Vertices.Length;

                instancesDataRotter = new DInstanceDataMatrixRotter[instX * instY * instZ];
                instancesData = new DInstanceData[instX * instY * instZ];
                instances = new DInstanceType[instX * instY * instZ];
                _arrayOfInstances = new SC_cube_instances[instX * instY * instZ];

                count = 0;
                for (int x = 0; x < instX; x++)
                {
                    for (int y = 0; y < instY; y++)
                    {
                        for (int z = 0; z < instZ; z++)
                        {
                            Vector3 position = new Vector3(x * offsetPosX, y * offsetPosY, z * offsetPosZ);
                            Matrix _tempMatrix = matroxer;

                            position.X += matroxer.M41;
                            position.Y += matroxer.M42;
                            position.Z += matroxer.M43;

                            instances[count] = new DInstanceType()
                            {
                                position = new Vector4(position.X, position.Y, position.Z, 1)
                            };

                            instancesData[count] = new DInstanceData()
                            {
                                rotation = new Vector4(0, 0, 0, 1)
                            };

                            _tempMatrix.M41 = position.X;
                            _tempMatrix.M42 = position.Y;
                            _tempMatrix.M43 = position.Z;

                            SC_cube_instances _cube = new SC_cube_instances();
                            _cube.transform.Component.rigidbody = new RigidBody(new BoxShape(_sizeX * 2, _sizeY * 2, _sizeZ * 2));
                            _cube.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                            _cube.transform.Component.rigidbody.Orientation = Conversion.ToJitterMatrix(_tempMatrix);
                            _cube.transform.Component.rigidbody.LinearVelocity = new Jitter.LinearMath.JVector(0, 0, 0);
                            _cube.transform.Component.rigidbody.IsStatic = true;
                            _cube.transform.Component.rigidbody.Tag = SC_console_directx.BodyTag.PlayerTorso;


                            _cube.transform.Component.rigidbody.Material.Restitution = 0.25f;
                            _cube.transform.Component.rigidbody.Material.StaticFriction = 0.45f;
                            //_cube.transform.Component.rigidbody.Material.KineticFriction = 0.45f;
                            _cube.transform.Component.rigidbody.Mass = 100;

                            SC_Console_GRAPHICS.World.AddBody(_cube.transform.Component.rigidbody);
                            //_cube._POSITION = _tempMatrix;

                            _singleObjectOnly = _cube;
                        }
                    }
                }

                InstanceCount = instances.Length;

                // Create the vertex buffer.
                VertexBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, Vertices);

                IndexBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.IndexBuffer, triangles);

                // Create the Instance instead of an Index Buffer.
                InstanceBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, instances);

                InstanceRotationBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, instancesData);

                InstanceRotationBufferRIGHT = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, instancesData);
                InstanceRotationBufferUP = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, instancesData);

                InstanceRotationMatrixBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, instancesData);
                //SC_Console_GRAPHICS.MessageBox((IntPtr)0, InstanceBuffer.Description.Usage + "", "Oculus Error", 0);


                // Setup the description of the dynamic matrix constant Matrix buffer that is in the vertex shader.
                BufferDescription matrixBufferDescription = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<DInstanceType>() * instances.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                // Create the constant buffer pointer so we can access the vertex shader constant buffer from within this class.
                InstanceBuffer = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescription);

                matrixBufferDescription = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<DInstanceData>() * instancesData.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                // Create the constant buffer pointer so we can access the vertex shader constant buffer from within this class.
                InstanceRotationBuffer = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescription);


                matrixBufferDescription = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<DInstanceData>() * instancesData.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                // Create the constant buffer pointer so we can access the vertex shader constant buffer from within this class.
                InstanceRotationBufferRIGHT = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescription);


                matrixBufferDescription = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<DInstanceData>() * instancesData.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                // Create the constant buffer pointer so we can access the vertex shader constant buffer from within this class.
                InstanceRotationBufferUP = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescription);



                //_initTexture(D3D.device, windowsHandle);
















                // Create the vertex buffer.
                //VertexBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, Vertices);

                // Delete arrays now that they are in their respective vertex and index buffers.
                //Vertices = null;
                //indices = null;

                // Set the vertex buffer to active in the input assembler so it can be rendered.
                //device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, Utilities.SizeOf<SC_Desk_Screen_Shader.DVertex>(), 0));

                // Set the index buffer to active in the input assembler so it can be rendered.
                //device.ImmediateContext.InputAssembler.SetIndexBuffer(IndexBuffer, SharpDX.DXGI.Format.R32_UInt, 0);

                // Set the type of the primitive that should be rendered from this vertex buffer, in this case triangles.
                //device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;

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



        }
        public void Render(DeviceContext deviceContext)
        {
            // Put the vertex and index buffers on the graphics pipeline to prepare for drawings.
            RenderBuffers(deviceContext);
        }
        private void RenderBuffers(DeviceContext deviceContext)
        {
            deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, Utilities.SizeOf<DVertex>(), 0)); //, new VertexBufferBinding(InstanceBuffer, Utilities.SizeOf<DInstanceType>(), 0)  ////// , new VertexBufferBinding(InstanceBuffer, Utilities.SizeOf<DInstanceType>(), 0), new VertexBufferBinding(InstanceRotationBuffer, Utilities.SizeOf<DInstanceData>(), 0)
                                                                                                                                     //deviceContext.InputAssembler.SetVertexBuffers(1, new VertexBufferBinding(InstanceBuffer, Utilities.SizeOf<DInstanceType>(), 0));

            deviceContext.InputAssembler.SetVertexBuffers(1, new[]
            {
                new VertexBufferBinding(InstanceBuffer, Marshal.SizeOf(typeof(DInstanceType)),0),
            });

            deviceContext.InputAssembler.SetVertexBuffers(2, new[]
            {
                new VertexBufferBinding(InstanceRotationBuffer, Marshal.SizeOf(typeof(DInstanceData)),0),
            });

            deviceContext.InputAssembler.SetVertexBuffers(3, new[]
            {
                new VertexBufferBinding(InstanceRotationBufferRIGHT, Marshal.SizeOf(typeof(DInstanceData)),0),
            });
            deviceContext.InputAssembler.SetVertexBuffers(4, new[]
            {
                new VertexBufferBinding(InstanceRotationBufferUP, Marshal.SizeOf(typeof(DInstanceData)),0),
            });
            /*deviceContext.InputAssembler.SetVertexBuffers(3, new[]
            {
                new VertexBufferBinding(InstanceRotationMatrixBuffer, Marshal.SizeOf(typeof(DInstanceDataMatrixRotter)),0),
            });*/



            deviceContext.InputAssembler.SetIndexBuffer(IndexBuffer, SharpDX.DXGI.Format.R32_UInt, 0);
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;


            // Set the vertex buffer to active in the input assembler so it can be rendered.
            //deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, Utilities.SizeOf<DVertex>(), 0), new VertexBufferBinding(InstanceBuffer, Utilities.SizeOf<DInstanceType>(), 0));

            //deviceContext.InputAssembler.SetIndexBuffer(IndexBuffer, SharpDX.DXGI.Format.R32_UInt, 0);
            // Set the type of the primitive that should be rendered from this vertex buffer, in this case triangles.
            //deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
        }
    }
}













/*new DVertex()
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
                   },*/
