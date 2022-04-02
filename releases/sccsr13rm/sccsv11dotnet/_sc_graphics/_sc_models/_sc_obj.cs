using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using System;
using Jitter;
using Jitter.Dynamics;
using Jitter.Collision.Shapes;
using System.Runtime.InteropServices;

using ObjLoader.Loader.Loaders;
using ObjLoader;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using FileFormatWavefront;
namespace _sc_core_systems.SC_Graphics
{
    public class _sc_obj : ITransform, IComponent
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);
        public ITransform transform { get; private set; }
        IComponent ITransform.Component
        {
            get => component;
        }
        IComponent component;
        RigidBody IComponent.rigidbody { get; set; }

        SoftBody IComponent.softbody { get; set; }

        public Matrix[] _WORLDMATRIXINSTANCES { get; set; }


        public _sc_core_systems.SC_Graphics._sc_obj.DInstanceData[] instancesDataRIGHT { get; set; }
        public _sc_core_systems.SC_Graphics._sc_obj.DInstanceData[] instancesDataUP { get; set; }

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

        public _sc_obj_shader_final _this_object_texture_shader { get; set; }

        DLightBuffer[] _DLightBuffer = new DLightBuffer[1];

        _sc_obj_instances[] _arrayOfInstances;

        public DInstanceType[] instances { get; set; }

        public DInstanceData[] instancesDataForward { get; set; }

        public int _instX;
        public int _instY;
        public int _instZ;

        public Matrix _ORIGINPOSITION { get; set; }
        public _sc_obj_instances _singleObjectOnly;


        World _the_world;

        public _sc_obj() { }

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

                /*Vertices = new[]
                {                                   
                    //TOP
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         color = _color,
                         normal = new Vector3(0, 1, 1),
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, 1*_sizeZ) ,
                         texture = new Vector2(0, 1),
                         color = _color,
                         normal = new Vector3(0, 1, 1),
                    },
                      new DVertex()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                         texture = new Vector2(1, 0),
                         color = _color,
                         normal = new Vector3(0, 1, 1),
                     },
                    new DVertex()
                    {
                        position = new Vector3(1*_sizeX, 1*_sizeY, 1*_sizeZ) ,
                        texture = new Vector2(1, 1),
                        color = _color,
                        normal = new Vector3(0, 1, 1),         
                    },
                    
                   
                     
                    //BOTTOM
                    new DVertex()
                    {
                        position = new Vector3(-1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                        texture = new Vector2(0, 0),
                        color = _color,
                        normal = new Vector3(1, 0, 1),                  
                    },
                    new DVertex()
                    {
                        position = new Vector3(-1*_sizeX, -1*_sizeY, 1*_sizeZ) ,
                        texture = new Vector2(0, 1),
                        color = _color,
                        normal = new Vector3(0, 1, 1),
                    },
                    new DVertex()
                    {
                        position = new Vector3(1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                        texture = new Vector2(1, 0),
                        color = _color,
                        normal = new Vector3(0, 1, 1),
                    },
                    new DVertex()
                    {
                        position = new Vector3(1*_sizeX, -1*_sizeY, 1*_sizeZ) ,
                        texture = new Vector2(1, 1),
                        color = _color,
                        normal = new Vector3(0, 1, 1),
                    },

                    
                    //FACE NEAR
                    new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                           texture = new Vector2(0, 0),
                        color = _color,
                        normal = new Vector3(1, 0, 1),   
                     },
                      new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                         texture = new Vector2(0, 1),
                        color = _color,
                        normal = new Vector3(0, 1, 1),
                     },
                        new DVertex()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                        texture = new Vector2(1, 0),
                        color = _color,
                        normal = new Vector3(0, 1, 1),
                     },
                    new DVertex()
                    {
                        position = new Vector3(1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                         texture = new Vector2(1, 1),
                        color = _color,
                        normal = new Vector3(0, 1, 1),
                    },

                     
                     //FACE FAR
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, 1*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         color = _color,
                         normal = new Vector3(1, 0, 1),
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, 1*_sizeZ) ,
                         texture = new Vector2(0, 1),
                         color = _color,
                         normal = new Vector3(0, 1, 1),
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, 1*_sizeZ) ,
                         texture = new Vector2(1, 0),
                         color = _color,
                         normal = new Vector3(0, 1, 1),
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, 1*_sizeZ) ,
                         texture = new Vector2(1, 1),
                         color = _color,
                         normal = new Vector3(0, 1, 1),
                     },





                     //FACE LEFT
                      new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, -1*_sizeZ),
                          texture = new Vector2(0, 0),
                         color = _color,
                         normal = new Vector3(1, 0, 1),
                     },
                       new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, 1*_sizeZ) ,
                          texture = new Vector2(0, 1),
                         color = _color,
                         normal = new Vector3(0, 1, 1),
                     },
                      new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                         texture = new Vector2(1, 0),
                         color = _color,
                         normal = new Vector3(0, 1, 1),
                     },
                     new DVertex()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, 1*_sizeZ),
                         texture = new Vector2(1, 1),
                         color = _color,
                         normal = new Vector3(0, 1, 1),
                     },






                     //FACE RIGHT
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                          texture = new Vector2(0, 0),
                         color = _color,
                         normal = new Vector3(1, 0, 1),
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, 1*_sizeZ) ,
                          texture = new Vector2(0, 1),
                         color = _color,
                         normal = new Vector3(0, 1, 1),
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                         texture = new Vector2(1, 0),
                         color = _color,
                         normal = new Vector3(0, 1, 1),
                     },
                     new DVertex()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, 1*_sizeZ) ,
                         texture = new Vector2(1, 1),
                         color = _color,
                         normal = new Vector3(0, 1, 1),
                     },         
                 };



                int[] triangles = new int[]
                {
                    1,2,3,2,1,0,
                    4,5,6,7,6,5,
                    9,10,11,10,9,8,
                    12,13,14,15,14,13,
                    17,18,19,18,17,16,
                    20,21,22,23,22,21

                 };*/



                //var path = @"C:\Users\ninekorn\Desktop\_usefull projects\_4 - ai - language - not working\_sc_core_systems\_sc_core_systems\_sc_core_systems\_sc_core_systems\" + "monkey" + ".obj";
                /*var path = @"C:\Users\ninekorn\Desktop\ + monkey.obj";
                bool loadTextureImages = false;
                var result = FileFormatObj.Load(path, loadTextureImages);

                if (result == null)
                {
                    MessageBox((IntPtr)0, "null", "Oculus error", 0);
                }
                else
                {
                    if (result.Model!= null)
                    {
                        if (result.Model.Vertices != null)
                        {
                            MessageBox((IntPtr)0, "count " + result.Model.Vertices.Count, "Oculus error", 0);
                        }
                        else
                        {
                            MessageBox((IntPtr)0, "null", "Oculus error", 0);
                        }
                    }
                    else
                    {
                        MessageBox((IntPtr)0, "null", "Oculus error", 0);
                    }
                }



                int[][][] _arrayOfTrigs = new int[result.Model.Groups.Count][][];
                List<int> trigs = new List<int>();

                for (int i = 0; i < result.Model.Groups.Count; i++)
                {
                    var single_group = result.Model.Groups[i];

                    _arrayOfTrigs[i] = new int[single_group.Faces.Count][];

                    for (int f = 0; f < single_group.Faces.Count; f++)
                    {
                        var face = single_group.Faces[f];
                        _arrayOfTrigs[i][f] = new int[face.Indices.Count];
                        for (int _f = 0; _f < face.Indices.Count; _f++)
                        {
                            var _data = face.Indices[f];
                            _arrayOfTrigs[i][f][_f] = _data.vertex;
                            trigs.Add(_data.vertex);
                        }
                    }
                }

                int[] triangles = new int[trigs.Count];

                for (int i = 0; i < trigs.Count; i++)
                {
                    triangles[i] = trigs[i];
                }




                Vertices = new DVertex[result.Model.Vertices.Count];
                for (int i = 0; i < result.Model.Vertices.Count; i++)
                {
                    Vertices[i] = new DVertex
                    {
                        position = new Vector3(result.Model.Vertices[i].x, result.Model.Vertices[i].y, result.Model.Vertices[i].z),
                        texture = new Vector2(result.Model.Uvs[i].u, result.Model.Uvs[i].v),
                        color = new Vector4(0.65f, 0.35f, 0.35f, 1),
                        normal = new Vector3(result.Model.Normals[i].x, result.Model.Normals[i].y, result.Model.Normals[i].z),
                    };
                }*/


                var objLoaderFactory = new ObjLoaderFactory();
                var objLoader = objLoaderFactory.Create();

                var path = @"C:\Users\ninekorn\Desktop\ + monkey.obj";
                FileStream fs = File.Create(path);// new FileStream("model.obj", FileAccess.ReadWrite);
                var result = objLoader.Load(fs);

                //var path = @"C:\Users\ninekorn\Desktop\#RECSOUND\" + "wave_audio_" + sc_can_start_rec_counter + ".xml";
                //var path = @"C:\Users\ninekorn\Desktop\_usefull projects\_4 - ai - language - not working\_sc_core_systems\_sc_core_systems\_sc_core_systems\_sc_core_systems\" + "monkey" + ".obj";
                //var path = "../../../" + "monkey" + ".obj";
                //var path = @"C:\Users\ninekorn\Desktop\_usefull projects\_4 - ai - language - not working\_sc_core_systems\_sc_core_systems\_sc_core_systems\_sc_core_systems\" + "monkey" + ".obj";


                if (result == null)
                {
                    MessageBox((IntPtr)0, "null", "Oculus error", 0);
                }
                else
                {
                    if (result.Vertices != null)
                    {
                        if (result.Vertices != null)
                        {
                            MessageBox((IntPtr)0, "count " + result.Vertices.Count, "Oculus error", 0);
                        }
                        else
                        {
                            MessageBox((IntPtr)0, "null", "Oculus error", 0);
                        }
                    }
                    else
                    {
                        MessageBox((IntPtr)0, "null", "Oculus error", 0);
                    }
                }








                /*var path = @"C:\Users\ninekorn\Desktop\ + monkey.obj";

                FileStream fs = File.Create(path);// new FileStream("model.obj", FileAccess.ReadWrite);
                var result = objLoader.Load(fs);

                int[][][] _arrayOfTrigs = new int[result.Groups.Count][][];
                List<int> trigs = new List<int>();

                for (int i = 0; i < result.Groups.Count; i++)
                {
                    var single_group = result.Groups[i];

                    _arrayOfTrigs[i] = new int[single_group.Faces.Count][];

                    for (int f = 0; f < single_group.Faces.Count; f++)
                    {
                        var face = single_group.Faces[f];
                        _arrayOfTrigs[i][f] = new int[face.Count];
                        for (int _f = 0; _f < face.Count; _f++)
                        {
                            var _data = face[f];
                            _arrayOfTrigs[i][f][_f] = _data.VertexIndex;
                            trigs.Add(_data.VertexIndex);
                        }
                    }
                }

                int[] triangles = new int[trigs.Count];

                for (int i = 0; i < trigs.Count; i++)
                {
                    triangles[i] = trigs[i];
                }*/


                int count = 0;
                //IndexCount = triangles.Length;
                VertexCount = Vertices.Length;

                instancesDataForward = new DInstanceData[instX * instY * instZ];
                instances = new DInstanceType[instX * instY * instZ];
                _arrayOfInstances = new _sc_obj_instances[instX * instY * instZ];
                instancesDataUP = new DInstanceData[instX * instY * instZ];
                instancesDataRIGHT = new DInstanceData[instX * instY * instZ];



                count = 0;
                for (int x = 0; x < instX; x++)
                {
                    for (int y = 0; y < instY; y++)
                    {
                        for (int z = 0; z < instZ; z++)
                        {

                            if (isTerrain == 0)
                            {
                                Vector3 position = new Vector3((x * _sizeX) + offsetPosX, (y * _sizeY) + offsetPosY, (z * _sizeZ) + offsetPosZ);
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

                                _sc_obj_instances _cube = new _sc_obj_instances();
                                _cube.transform.Component.rigidbody = new RigidBody(new BoxShape(_sizeX * 2, _sizeY * 2, _sizeZ * 2));
                                _cube.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                                _cube.transform.Component.rigidbody.Orientation = Conversion.ToJitterMatrix(_tempMatrix);
                                _cube.transform.Component.rigidbody.LinearVelocity = new Jitter.LinearMath.JVector(0, 0, 0);
                                _cube.transform.Component.rigidbody.IsStatic = true;
                                _cube.transform.Component.rigidbody.Tag = _sc_console_directx.BodyTag._terrain;


                                _cube.transform.Component.rigidbody.Material.Restitution = 0.015f;
                                _cube.transform.Component.rigidbody.Material.StaticFriction = 0.55f;
                                _cube.transform.Component.rigidbody.Material.KineticFriction = 0.55f;

                                _cube.transform.Component.rigidbody.Mass = _sizeX * 100;

                                _the_world.AddBody(_cube.transform.Component.rigidbody);
                                //_cube._POSITION = _tempMatrix;

                                _singleObjectOnly = _cube;
                            }
                            else if (isTerrain == 1)
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

                                _sc_obj_instances _cube = new _sc_obj_instances();
                                _cube.transform.Component.rigidbody = new RigidBody(new BoxShape(_sizeX * 2, _sizeY * 2, _sizeZ * 2));
                                _cube.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                                _cube.transform.Component.rigidbody.Orientation = Conversion.ToJitterMatrix(_tempMatrix);
                                _cube.transform.Component.rigidbody.LinearVelocity = new Jitter.LinearMath.JVector(0, 0, 0);
                                _cube.transform.Component.rigidbody.IsStatic = true;
                                _cube.transform.Component.rigidbody.Tag = _sc_console_directx.BodyTag._terrain_tiles;


                                _cube.transform.Component.rigidbody.Material.Restitution = 0.015f;
                                _cube.transform.Component.rigidbody.Material.StaticFriction = 0.55f;
                                _cube.transform.Component.rigidbody.Material.KineticFriction = 0.55f;

                                _cube.transform.Component.rigidbody.Mass = _sizeX * 100;

                                _the_world.AddBody(_cube.transform.Component.rigidbody);
                                //_cube._POSITION = _tempMatrix;

                                _singleObjectOnly = _cube;
                            }
                            else if (isTerrain == 2)
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

                                _sc_obj_instances _cube = new _sc_obj_instances();
                                _cube.transform.Component.rigidbody = new RigidBody(new BoxShape(_sizeX * 2, _sizeY * 2, _sizeZ * 2));
                                _cube.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                                _cube.transform.Component.rigidbody.Orientation = Conversion.ToJitterMatrix(_tempMatrix);
                                _cube.transform.Component.rigidbody.LinearVelocity = new Jitter.LinearMath.JVector(0, 0, 0);
                                _cube.transform.Component.rigidbody.IsStatic = false;
                                _cube.transform.Component.rigidbody.Tag = _sc_console_directx.BodyTag.physicsInstancedCube;

                                _cube.transform.Component.rigidbody.Material.Restitution = 0.015f;
                                _cube.transform.Component.rigidbody.Material.StaticFriction = 0.55f;
                                _cube.transform.Component.rigidbody.Material.KineticFriction = 0.55f;

                                _cube.transform.Component.rigidbody.Mass = _sizeX * 100;

                                _the_world.AddBody(_cube.transform.Component.rigidbody);
                                //_cube._POSITION = _tempMatrix;

                                _singleObjectOnly = _cube;
                            }

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

                                _sc_obj_instances _cube = new _sc_obj_instances();
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

                                _sc_obj_instances _cube = new _sc_obj_instances();
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

                                _sc_obj_instances _cube = new _sc_obj_instances();
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

                                _sc_obj_instances _cube = new _sc_obj_instances();
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
                //IndexBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.IndexBuffer, triangles);
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
