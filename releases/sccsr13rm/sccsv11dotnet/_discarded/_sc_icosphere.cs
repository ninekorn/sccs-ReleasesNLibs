using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using System;
using Jitter;
using Jitter.Dynamics;
using Jitter.Collision.Shapes;
using System.Runtime.InteropServices;


using System.Collections;
using System.Collections.Generic;


namespace _sc_core_systems.SC_Graphics
{
    public class _sc_icosphere : ITransform, IComponent
    {
        public ITransform transform { get; private set; }
        IComponent ITransform.Component
        {
            get => component;
        }
        IComponent component;
        RigidBody IComponent.rigidbody { get; set; }

        SoftBody IComponent.softbody { get; set; }

        public Matrix[] _WORLDMATRIXINSTANCES { get; set; }


        public _sc_core_systems.SC_Graphics._sc_icosphere.DInstanceData[] instancesDataRIGHT { get; set; }
        public _sc_core_systems.SC_Graphics._sc_icosphere.DInstanceData[] instancesDataUP { get; set; }

        public Matrix _POSITION { get; set; }

        public float RotationY { get; set; }
        public float RotationX { get; set; }
        public float RotationZ { get; set; }
        public int IndexCount { get; set; }
        public int VertexCount { get; set; }

        public SharpDX.Quaternion Rotation { get; set; }
        public DVertex[] Vertices { get; set; }

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
            public Vector4 instanceRot0;
            public Vector4 instanceRot1;
            public Vector4 instanceRot2;
            public Vector4 instanceRot3;
        }

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
            public float padding0;
            [FieldOffset(48)]
            public Vector3 lightPosition;
            [FieldOffset(60)]
            public float padding1;
        }

        public int InstanceCount { get; private set; }
        private SharpDX.Direct3D11.Buffer VertexBuffer { get; set; }
        private SharpDX.Direct3D11.Buffer IndexBuffer { get; set; }
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

        private int m_TerrainWidth, m_TerrainHeight;

        public Vector4 _color;

        public _sc_icosphere_shader_final _this_object_texture_shader { get; set; }

        DLightBuffer[] _DLightBuffer = new DLightBuffer[1];

        _sc_icosphere_instances[] _arrayOfInstances;

        public DInstanceType[] instances { get; set; }

        public DInstanceData[] instancesDataForward { get; set; }

        public int _instX;
        public int _instY;
        public int _instZ;

        public Matrix _ORIGINPOSITION { get; set; }
        public _sc_icosphere_instances _singleObjectOnly;


        World _the_world;

        public _sc_icosphere() { }

        public bool Initialize(_sc_console_directx D3D, int width, int height, float tileSize, int divX, int divY, float _sizeX, float _sizeY, float _sizeZ, Vector4 color, int instX, int instY, int instZ, IntPtr windowsHandle, Matrix matroxer, int isTerrain, float offsetPosX, float offsetPosY, float offsetPosZ, World the_world)
        {
            _the_world = the_world;
            _ORIGINPOSITION = matroxer;
            _POSITION = matroxer;

            transform = this;
            component = this;

            this._color = color;
            this._sizeX = _sizeX;
            this._sizeY = _sizeY;
            this._sizeZ = _sizeZ;

            _tileSize = tileSize;
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

            if (!InitializeBuffer(D3D, _sizeX, _sizeY, _sizeZ, tileSize, instX, instY, instZ, windowsHandle, matroxer, isTerrain, offsetPosX, offsetPosY, offsetPosZ))
                return false;

            return true;
        }



        SharpDX.Direct3D11.Buffer ConstantLightBuffer;

        private float _sizeX = 0;
        private float _sizeY = 0;
        private float _sizeZ = 0;

        public void ShutDown()
        {
            ShutDownBuffers();
        }
        private bool InitializeBuffer(_sc_console_directx D3D, float _sizeX, float _sizeY, float _sizeZ, float tileSize, int instX, int instY, int instZ, IntPtr windowsHandle, Matrix matroxer, int isTerrain, float offsetPosX, float offsetPosY, float offsetPosZ)
        {
            try
            {
                int sizeWidther = (int)(m_TerrainWidth * 0.5f);
                int sizeHeighter = (int)(m_TerrainHeight * 0.5f);

                sizeWidther /= 10;
                sizeHeighter /= 10;

                var someOffsetPos = new Vector3(offsetPosX, offsetPosY, offsetPosZ);







                /*var vectors = new List<Vector3>();
                var indices = new List<int>();

                GeometryProvider.Icosahedron(vectors, indices);

                for (var i = 0; i < 10; i++)
                {
                    GeometryProvider.Subdivide(vectors, indices, true);
                }
                /// normalize vectors to "inflate" the icosahedron into a sphere.
                for (var i = 0; i < vectors.Count; i++)
                {
                    vectors[i] = Vector3.Normalize(vectors[i]);
                }



                Vertices = new DVertex[vectors.Count];
                var triangles = new int[indices.Count];

                for (int i = 0; i < vectors.Count; i++)
                {
                    Vertices[i] = new DVertex()
                    {
                        position = vectors[i],
                        normal = new Vector3(0, 1, 1), //new Vector3(0, 1, 1),
                        color = _color,
                    };
                }
                for (int i = 0; i < indices.Count; i++)
                {
                    triangles[i] = indices[i]; //trigs[i]; //
                }*/








                CreateIcoSphere sphere = new CreateIcoSphere();

                List<Vector3> verts;// = new List<Vector3>();
                List<int> trigs;// = new List<int>();
                List<Vector3> normals;

                sphere.OnWizardCreate(0.01f, 1, 3, out verts, out trigs, out normals);
                //Vertices = new SC_VR_Touch_Shader.DVertex[verts.Count];
                //indices = new int[trigs.Count];
                Vertices = new DVertex[verts.Count];
                var triangles = new int[trigs.Count];


                for (int i = 0; i < verts.Count; i++)
                {
                    Vertices[i] = new DVertex()
                    {
                        position = verts[i],
                        normal = normals[i],//normals[i], //new Vector3(0, 1, 1),
                        color = _color,
                    };
                }

                for (int i = 0; i < trigs.Count; i++)
                {
                    triangles[i] = trigs[i]; //trigs[i]; //trigs.Count - i - 1
                }

                //Set number of vertices in the vertex array.
                VertexCount = Vertices.Length;
                // Set number of vertices in the index array.
                IndexCount = triangles.Length;

                /*
                CreateIcoSphere sphere = new CreateIcoSphere();

                List<Vector3> verts;// = new List<Vector3>();
                List<int> trigs;// = new List<int>();
                List<Vector3> normals;

                sphere.OnWizardCreate(0.05f, 1, 3, out verts, out trigs, out normals);

                Vertices = new DVertex[verts.Count];
                var triangles = new int[trigs.Count];

                for (int i = 0;i < verts.Count;i++)
                {
                    Vertices[i] = new DVertex()
                    {
                        position = verts[i],
                        normal = normals[i], //new Vector3(0, 1, 1),
                        color = _color,
                    };
                }

                for (int i = 0; i < trigs.Count; i++)
                {
                    triangles[i] = trigs[i]; //trigs[i]; //trigs.Count - i - 1
                }*/


                /*Vertices = new[]
                {

                }

                verts*/


                /*indices = new int[trigs.Count];

                for (int i = 0; i < verts.Count; i++)
                {
                    Vertices[i] = new SC_VR_Touch_Shader.DVertex()
                    {
                        position = verts[i],
                        color = _color,
                    };
                }

                for (int i = 0; i < trigs.Count; i++)
                {
                    indices[i] = trigs[trigs.Count - i - 1]; //trigs[i]; //
                }*/

                //Set number of vertices in the vertex array.
                //VertexCount = Vertices.Length;
                // Set number of vertices in the index array.
                //IndexCount = triangles.Length;

















                /*

                Vertices = new[]
                {                                   
                    //TOP
                    new DVertex()
                    {
                        position = new Vector3(1*_sizeX, 1*_sizeY, 1*_sizeZ) ,
                        normal = new Vector3(0, 1, 1),
                        color = _color,
                    },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                         normal = new Vector3(0, 1, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                         normal = new Vector3(0, 1, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                         normal = new Vector3(0, 1, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, 1*_sizeZ) ,
                         normal = new Vector3(0, 1, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, 1*_sizeZ),
                         normal = new Vector3(0, 1, 1),
                         color = _color,
                     },

                     //BOTTOM
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                         normal = new Vector3(1, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                         normal = new Vector3(1, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, 1*_sizeZ) ,
                         normal = new Vector3(1, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, 1*_sizeZ) ,
                         normal = new Vector3(1, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, 1*_sizeZ) ,
                         normal = new Vector3(1, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                         normal = new Vector3(1, 0, 1),
                         color = _color,
                     },

                    
                    
                    //FACE NEAR
                    new DVertex()
                    {
                        position = new Vector3(1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                        normal = new Vector3(1, 0, 0),
                        color = _color,
                    },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                         normal = new Vector3(1, 0, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                         normal = new Vector3(1, 0, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                         normal = new Vector3(1, 0, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                         normal = new Vector3(1, 0, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                         normal = new Vector3(1, 0, 0),
                         color = _color,
                     },



                     //FACE FAR
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, 1*_sizeZ) ,
                         normal = new Vector3(0, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, 1*_sizeZ) ,
                         normal = new Vector3(0, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, 1*_sizeZ) ,
                         normal = new Vector3(0, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, 1*_sizeZ),
                         normal = new Vector3(0, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, 1*_sizeZ) ,
                         normal = new Vector3(0, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, 1*_sizeZ),
                         normal = new Vector3(0, 1, 0),
                         color = _color,
                     },






                     //FACE LEFT
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, 1*_sizeZ),
                         normal = new Vector3(0, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                         normal = new Vector3(0, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, -1*_sizeZ),
                         normal = new Vector3(0, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                         normal = new Vector3(0, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, 1*_sizeZ) ,
                         normal = new Vector3(0, 0, 1),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, 1*_sizeZ) ,
                         normal = new Vector3(0, 0, 1),
                         color = _color,
                     },




                     //FACE RIGHT
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                         normal = new Vector3(1, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                         normal = new Vector3(1, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, 1*_sizeZ) ,
                         normal = new Vector3(1, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, 1*_sizeZ) ,
                         normal = new Vector3(1, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, 1*_sizeZ) ,
                         normal = new Vector3(1, 1, 0),
                         color = _color,
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                         normal = new Vector3(1, 1, 0),
                         color = _color,
                     },                   
                 };



                int[] triangles = new int[]
                {
                    5,4,3,2,1,0,
                    11,10,9,8,7,6,
                    17,16,15,14,13,12,
                    23,22,21,20,19,18,
                    29,28,27,26,25,24,
                    35,34,33,32,31,30,     
                 };*/






                int count = 0;
                IndexCount = triangles.Length;
                VertexCount = Vertices.Length;

                instancesDataForward = new DInstanceData[instX * instY * instZ];
                instances = new DInstanceType[instX * instY * instZ];
                _arrayOfInstances = new _sc_icosphere_instances[instX * instY * instZ];
                instancesDataUP = new DInstanceData[instX * instY * instZ];
                instancesDataRIGHT = new DInstanceData[instX * instY * instZ];



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

                            instancesDataForward[count] = new DInstanceData()
                            {
                                rotation = new Vector4(0, 0, 0, 1)
                            };
                            _tempMatrix.M41 = position.X;
                            _tempMatrix.M42 = position.Y;
                            _tempMatrix.M43 = position.Z;

                            _sc_icosphere_instances _cube = new _sc_icosphere_instances();
                            _cube.transform.Component.rigidbody = new RigidBody(new BoxShape(_sizeX * 2, _sizeY * 2, _sizeZ * 2));
                            _cube.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                            _cube.transform.Component.rigidbody.Orientation = Conversion.ToJitterMatrix(_tempMatrix);
                            _cube.transform.Component.rigidbody.LinearVelocity = new Jitter.LinearMath.JVector(0, 0, 0);
                            _cube.transform.Component.rigidbody.IsStatic = false;
                            _cube.transform.Component.rigidbody.Tag = _sc_console_directx.BodyTag._icosphere;

                            _cube.transform.Component.rigidbody.Material.Restitution = 0.015f;
                            _cube.transform.Component.rigidbody.Material.StaticFriction = 0.55f;
                            _cube.transform.Component.rigidbody.Material.KineticFriction = 0.55f;

                            _cube.transform.Component.rigidbody.Mass = _sizeX * 100;

                            _the_world.AddBody(_cube.transform.Component.rigidbody);
                            //_cube._POSITION = _tempMatrix;

                            _singleObjectOnly = _cube;
                            /*else if (isTerrain == 2)
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

                                _sc_icosphere_instances _cube = new _sc_icosphere_instances();
                                _cube.transform.Component.rigidbody = new RigidBody(new BoxShape(_sizeX * 2, _sizeY * 2, _sizeZ * 2));
                                _cube.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                                _cube.transform.Component.rigidbody.Orientation = Conversion.ToJitterMatrix(_tempMatrix);
                                _cube.transform.Component.rigidbody.LinearVelocity = new Jitter.LinearMath.JVector(0, 0, 0);
                                _cube.transform.Component.rigidbody.IsStatic = true;
                                _cube.transform.Component.rigidbody.Tag = _sc_console_directx.BodyTag.cloth_cube;


                                _cube.transform.Component.rigidbody.Material.Restitution = 0.001f;
                                _cube.transform.Component.rigidbody.Material.StaticFriction = 0.65f;
                                _cube.transform.Component.rigidbody.Material.KineticFriction = 0.65f;

                                _cube.transform.Component.rigidbody.Mass = _sizeX;

                                _the_world.AddBody(_cube.transform.Component.rigidbody);
                                //_cube._POSITION = _tempMatrix;

                                _singleObjectOnly = _cube;
                            }
                            else if (isTerrain == 3)
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

                                _sc_icosphere_instances _cube = new _sc_icosphere_instances();
                                _cube.transform.Component.rigidbody = new RigidBody(new BoxShape(_sizeX * 2, _sizeY * 2, _sizeZ * 2));
                                _cube.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                                _cube.transform.Component.rigidbody.Orientation = Conversion.ToJitterMatrix(_tempMatrix);
                                _cube.transform.Component.rigidbody.LinearVelocity = new Jitter.LinearMath.JVector(0, 0, 0);
                                _cube.transform.Component.rigidbody.IsStatic = true;
                                _cube.transform.Component.rigidbody.Tag = _sc_console_directx.BodyTag.screen_corners;


                                _cube.transform.Component.rigidbody.Material.Restitution = 0.05f;
                                _cube.transform.Component.rigidbody.Material.StaticFriction = 0.55f;
                                _cube.transform.Component.rigidbody.Material.KineticFriction = 0.55f;

                                _cube.transform.Component.rigidbody.Mass = _sizeX * 5;

                                _the_world.AddBody(_cube.transform.Component.rigidbody);
                                //_cube._POSITION = _tempMatrix;

                                _singleObjectOnly = _cube;
                            }
                            else if (isTerrain == 4)
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

                                _sc_icosphere_instances _cube = new _sc_icosphere_instances();
                                _cube.transform.Component.rigidbody = new RigidBody(new BoxShape(_sizeX * 2, _sizeY * 2, _sizeZ * 2));
                                _cube.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                                _cube.transform.Component.rigidbody.Orientation = Conversion.ToJitterMatrix(_tempMatrix);
                                _cube.transform.Component.rigidbody.LinearVelocity = new Jitter.LinearMath.JVector(0, 0, 0);
                                _cube.transform.Component.rigidbody.IsStatic = true;
                                _cube.transform.Component.rigidbody.Tag = _sc_console_directx.BodyTag.screen_pointer_touch;


                                _cube.transform.Component.rigidbody.Material.Restitution = 0.05f;
                                _cube.transform.Component.rigidbody.Material.StaticFriction = 0.55f;
                                _cube.transform.Component.rigidbody.Material.KineticFriction = 0.55f;

                                _cube.transform.Component.rigidbody.Mass = _sizeX * 5;

                                _the_world.AddBody(_cube.transform.Component.rigidbody);
                                //_cube._POSITION = _tempMatrix;

                                _singleObjectOnly = _cube;
                            }
                            else if (isTerrain == 5)
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

                                _sc_icosphere_instances _cube = new _sc_icosphere_instances();
                                _cube.transform.Component.rigidbody = new RigidBody(new BoxShape(_sizeX * 2, _sizeY * 2, _sizeZ * 2));
                                _cube.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                                _cube.transform.Component.rigidbody.Orientation = Conversion.ToJitterMatrix(_tempMatrix);
                                _cube.transform.Component.rigidbody.LinearVelocity = new Jitter.LinearMath.JVector(0, 0, 0);
                                _cube.transform.Component.rigidbody.IsStatic = true;
                                _cube.transform.Component.rigidbody.Tag = _sc_console_directx.BodyTag.screen_pointer_HMD;


                                _cube.transform.Component.rigidbody.Material.Restitution = 0.05f;
                                _cube.transform.Component.rigidbody.Material.StaticFriction = 0.55f;
                                _cube.transform.Component.rigidbody.Material.KineticFriction = 0.55f;

                                _cube.transform.Component.rigidbody.Mass = _sizeX * 5;

                                _the_world.AddBody(_cube.transform.Component.rigidbody);

                                _singleObjectOnly = _cube;
                            }*/
                            count++;
                        }
                    }
                }

                InstanceCount = instances.Length;
                VertexBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, Vertices);
                IndexBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.IndexBuffer, triangles);
                InstanceBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, instances);
                InstanceRotationBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, instancesDataForward);
                InstanceRotationBufferRIGHT = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, instancesDataForward);
                InstanceRotationBufferUP = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, instancesDataForward);

                InstanceRotationMatrixBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, instancesDataForward);

                BufferDescription matrixBufferDescription = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<DInstanceType>() * instances.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                InstanceBuffer = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescription);

                matrixBufferDescription = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<DInstanceData>() * instancesDataForward.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                InstanceRotationBuffer = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescription);

                matrixBufferDescription = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<DInstanceData>() * instancesDataForward.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                InstanceRotationBufferRIGHT = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescription);

                matrixBufferDescription = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<DInstanceData>() * instancesDataForward.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                InstanceRotationBufferUP = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescription);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void ShutDownBuffers()
        {
            IndexBuffer?.Dispose();
            IndexBuffer = null;
            VertexBuffer?.Dispose();
            VertexBuffer = null;
        }
        public void Render(DeviceContext deviceContext)
        {
            RenderBuffers(deviceContext);
        }
        private void RenderBuffers(DeviceContext deviceContext)
        {
            deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, Utilities.SizeOf<DVertex>(), 0));                                                           
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
            deviceContext.InputAssembler.SetIndexBuffer(IndexBuffer, SharpDX.DXGI.Format.R32_UInt, 0);
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
        }
    }
}
