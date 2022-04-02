//using DSharpDXRastertek.Tut37.Graphics.Data;
//using DSharpDXRastertek.Tut37.System;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using System.Runtime.InteropServices;


using Jitter.Collision;
using Jitter;
using Jitter.Dynamics;
using Jitter.DataStructures;
using Jitter.Collision.Shapes;



namespace SC_skYaRk_VR_V007
{
    public class DModelClass2: ITransform, IComponent
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


        public Matrix _ORIGINPOSITION { get; set; }
        public DModelClass2_instances _singleObjectOnly;// = new SC_cube_instances();





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



        public Vector3 screenNormal { get; set; }

        public DVertexType[] vertices { get; set; }

        // Constructor
        public DModelClass2() { }
        // Methods.
        public bool Initialize(SharpDX.Direct3D11.Device device, int sizeWidth, int sizeHeight, float totalSize, Matrix matroxer, float _sizeX, float _sizeY, float _sizeZ, float offsetPosX, float offsetPosY, float offsetPosZ) //, string textureFileName
        {
            //transform = this;
            //component = this;

            _ORIGINPOSITION = matroxer;
            _POSITION = matroxer;



            // Initialize the vertex and index buffer that hold the geometry for the triangle.
            if (!InitializeBuffers(device, sizeWidth, sizeHeight, totalSize, matroxer,  _sizeX,  _sizeY,  _sizeZ,  offsetPosX,  offsetPosY,  offsetPosZ))
                return false;

            //if (!LoadTexture(device, textureFileName))
            //    return false;

            return true;
        }
        private bool InitializeBuffers(SharpDX.Direct3D11.Device device, int sizeWidth, int sizeHeight, float totalSize, Matrix matroxer, float _sizeX, float _sizeY, float _sizeZ, float offsetPosX, float offsetPosY, float offsetPosZ)
        {
            try
            {

                float sizeWidther = (float)(sizeWidth * 0.5f);
                float sizeHeighter = (float)(sizeHeight * 0.5f);
                float sizeDepther = (float)(_sizeZ * 0.5f);
                //int sizeRight = -(sizeWidth / 2);

                //int sizeTop = (sizeHeight / 2);
                //int sizeBottom = -(sizeHeight / 2);


                //-sizeWidther, -sizeHeighter
                //sizeWidther *= 0.01f;
                //sizeHeighter *= 0.01f;


                //_sizeX = sizeWidther;
                //_sizeY = sizeHeighter;
                //_sizeZ = sizeDepther;

                _sizeX *= 0.00030f;
                _sizeY *= 0.00030f;
                _sizeZ *= 0.0025f;


                // Create the vertex array and load it with data.
                vertices = new[]
                {
					//TOP
                    new DVertexType()
                    {
                        position = new Vector3(1*_sizeX, 1*_sizeY, (1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                            texture = new Vector2(1,1)
                        //normal = new Vector3(0, 1, 1),
                        //color = _//color,
                    },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, (-1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(0, 1, 1),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, (-1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(0, 1, 1),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, (-1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(0, 1, 1),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, (1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(0, 1, 1),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, (1*_sizeZ)+ (1*_sizeZ * 1.0f)),
                             texture = new Vector2(1,1)
                         //normal = new Vector3(0, 1, 1),
                         //color = _//color,
                     },

                     //BOTTOM
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, (-1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(1, 0, 1),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, (-1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(1, 0, 1),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, (1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(1, 0, 1),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, (1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(1, 0, 1),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, (1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(1, 0, 1),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, (-1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(1, 0, 1),
                         //color = _//color,
                     },

                    
                    
                    //FACE NEAR
                    new DVertexType()
                    {
                        position = new Vector3(1*_sizeX, 1*_sizeY, (-1*_sizeZ)+ (1*_sizeZ * 1.0f)) , //
                        texture = new Vector2(0,0)
                        //normal = new Vector3(1, 0, 0),
                        //color = _//color,
                    },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, (-1*_sizeZ)+ (1*_sizeZ * 1.0f)) , // 13 = 2
                         texture = new Vector2(0,1)
                         //normal = new Vector3(1, 0, 0),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, (-1*_sizeZ)+ (1*_sizeZ * 1.0f)) , // 
                         texture = new Vector2(1,1)
                         //normal = new Vector3(1, 0, 0),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, (-1*_sizeZ)+ (1*_sizeZ * 1.0f)) , // 15 = 1
                         texture = new Vector2(1,0)
                         //normal = new Vector3(1, 0, 0),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, (-1*_sizeZ)+ (1*_sizeZ * 1.0f)) , //  16 = 3
                         texture = new Vector2(0,0)
                         //normal = new Vector3(1, 0, 0),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, (-1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,//17 = 0
                         texture = new Vector2(1,1)
                         //normal = new Vector3(1, 0, 0),
                         //color = _//color,
                     },









                    /* //FACE NEAR
                    new DVertexType()
                    {
                        position = new Vector3(1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                        texture = new Vector2(0,1)
                        //normal = new Vector3(1, 0, 0),
                        //color = _//color,
                    },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                         texture = new Vector2(0,0)
                         //normal = new Vector3(1, 0, 0),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                         texture = new Vector2(1,0)
                         //normal = new Vector3(1, 0, 0),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                         texture = new Vector2(1,1)
                         //normal = new Vector3(1, 0, 0),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                         texture = new Vector2(0,1)
                         //normal = new Vector3(1, 0, 0),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                         texture = new Vector2(1,0)
                         //normal = new Vector3(1, 0, 0),
                         //color = _//color,
                     },*/















                     /*
                         new DVertexType()
                    {
                        position = new Vector3(1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                        texture = new Vector2(1,0)
                        //normal = new Vector3(1, 0, 0),
                        //color = _//color,
                    },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                         texture = new Vector2(1,1)
                         //normal = new Vector3(1, 0, 0),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                         texture = new Vector2(0,1)
                         //normal = new Vector3(1, 0, 0),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                         texture = new Vector2(0,0)
                         //normal = new Vector3(1, 0, 0),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, -1*_sizeZ) ,
                         texture = new Vector2(1,0)
                         //normal = new Vector3(1, 0, 0),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, -1*_sizeZ) ,
                         texture = new Vector2(0,1)
                         //normal = new Vector3(1, 0, 0),
                         //color = _//color,
                     },*/












                     //FACE FAR
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, (1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(0, 1, 0),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, (1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(0, 1, 0),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, (1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(0, 1, 0),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, (1*_sizeZ)+ (1*_sizeZ * 1.0f)),
                             texture = new Vector2(1,1)
                         //normal = new Vector3(0, 1, 0),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, (1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(0, 1, 0),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, (1*_sizeZ)+ (1*_sizeZ * 1.0f)),
                             texture = new Vector2(1,1)
                         //normal = new Vector3(0, 1, 0),
                         //color = _//color,
                     },






                     //FACE LEFT
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, (1*_sizeZ)+ (1*_sizeZ * 1.0f)),
                             texture = new Vector2(1,1)
                         //normal = new Vector3(0, 0, 1),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, (-1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(0, 0, 1),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, (-1*_sizeZ)+ (1*_sizeZ * 1.0f)),
                             texture = new Vector2(1,1)
                         //normal = new Vector3(0, 0, 1),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, (-1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(0, 0, 1),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, -1*_sizeY, (1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(0, 0, 1),
                         //color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(-1*_sizeX, 1*_sizeY, (1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(0, 0, 1),
                         //color = _//color,
                     },




                     //FACE RIGHT
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, (-1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(1, 1, 0),
                         ////color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, (-1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(1, 1, 0),
                         ////color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, (1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(1, 1, 0),
                         ////color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, 1*_sizeY, (1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(1, 1, 0),
                         ////color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, (1*_sizeZ)+(1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(1, 1, 0),
                         ////color = _//color,
                     },
                     new DVertexType()
                     {
                         position = new Vector3(1*_sizeX, -1*_sizeY, (-1*_sizeZ)+ (1*_sizeZ * 1.0f)) ,
                             texture = new Vector2(1,1)
                         //normal = new Vector3(1, 1, 0),
                         ////color = _//color,
                     },
                };



                int[] indices = new int[]
                {
                        5,4,3,2,1,0,
                        11,10,9,8,7,6,
                        17,16,15,14,13,12,
                        23,22,21,20,19,18,
                        29,28,27,26,25,24,
                        35,34,33,32,31,30,
                 };




                VertexCount = vertices.Length;
                // Set the number of instances in the array.


                Vector3 vecOne = vertices[1].position - vertices[0].position;
                Vector3 vecTwo = vertices[2].position - vertices[0].position;
                Vector3 crossProd;

                Vector3.Cross(ref vecOne, ref vecTwo, out crossProd);
                screenNormal = Vector3.Normalize(crossProd);











                DInstanceType[] instances = new DInstanceType[]
                {
                    new DInstanceType()
                    {
                        position = new Vector3(0, 0, 0)
                    },
                    /*new DInstanceType()
                    {
                        position = new Vector3(-1.5f, -1.5f, 0)
                    },
                    new DInstanceType()
                    {
                        position = new Vector3(-1.5f,  1.5f, 0)
                    },
                    new DInstanceType()
                    {
                        position = new Vector3( 1.5f, -1.5f, 0)
                    },
                    new DInstanceType()
                    {
                        position = new Vector3( 1.5f,  1.5f, 0)
                    }*/
                };
                InstanceCount = instances.Length;

                /*int[] indices = new int[]
                {
                    0, // Bottom left.
					1, // Top middle.
					2,  // Bottom right.
                    3,//1
                    2,//2
                    1 //3
                };*/
                /*int[] indices = new int[]
                {
                     2, // Bottom left.
                     1, // Top middle.
                     0,  // Bottom right.
                     1,//1
                     2,//2
                     3 //3
                };*/








                Vector3 position = Vector3.Zero;// new Vector3(matroxer.M41 + offsetPosX, matroxer.M42 + offsetPosY, matroxer.M43 + offsetPosZ);
                //Matrix _tempMatrix = matroxer;
                //position.X += matroxer.M41;
                //position.Y += matroxer.M42;
                //position.Z += matroxer.M43;

                /*instances[count] = new DInstanceType()
                {
                    position = new Vector4(position.X, position.Y, position.Z, 1)
                };

                instancesData[count] = new DInstanceData()
                {
                    rotation = new Vector4(0, 0, 0, 1)
                };*/

                Matrix _tempMatrix = matroxer;
                //_tempMatrix.M41 = position.X;
                //_tempMatrix.M42 = position.Y;
                //_tempMatrix.M43 = position.Z;


                float sizerXer = (sizeWidther * totalSize) + (sizeWidther * totalSize);
                float sizerYer = (sizeHeighter * totalSize) + (sizeHeighter * totalSize);
                float sizerZer = (sizeDepther * totalSize) + (sizeDepther * totalSize); //(1*_sizeZ)+ (1*_sizeZ * 1.0f)) 0.1f

                //(sizeWidther * totalSize) + (sizeWidther * totalSize), (sizeHeighter * totalSize) + (sizeHeighter * totalSize), 1)
                //RigidBody rigidBodier;



                DModelClass2_instances _cube = new DModelClass2_instances();
                _cube.transform.Component.rigidbody = new RigidBody(new BoxShape(sizerXer, sizerYer, sizerZer));
                _cube.transform.Component.rigidbody.Position = new Jitter.LinearMath.JVector(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43);
                _cube.transform.Component.rigidbody.Orientation = Conversion.ToJitterMatrix(_tempMatrix);
                _cube.transform.Component.rigidbody.LinearVelocity = new Jitter.LinearMath.JVector(0, 0, 0);
                _cube.transform.Component.rigidbody.IsStatic = true;
                _cube.transform.Component.rigidbody.Tag = SC_Console_DIRECTX.BodyTag.Screen;
                _cube.transform.Component.rigidbody.IsActive = true;

                _cube.transform.Component.rigidbody.Material.Restitution = 0.25f;
                _cube.transform.Component.rigidbody.Material.StaticFriction = 0.45f;
                //_cube.transform.Component.rigidbody.Material.KineticFriction = 0.45f;

                _cube.transform.Component.rigidbody.Mass = 100;

                SC_Console_GRAPHICS.World.AddBody(_cube.transform.Component.rigidbody);
                _singleObjectOnly = _cube;


              























                IndexCount = indices.Length;

                // Create the vertex buffer.
                VertexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, vertices);

                // Create the Instance instead of an Index Buffer buffer.
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




/*new DVertexType()
{
position = new Vector3(-1, 1, 0),
texture = new Vector2(0, 0)
},
new DVertexType()
{
position = new Vector3(1, 1, 0),
texture = new Vector2(1, 0)
},
new DVertexType()
{
position = new Vector3(1, -1, 0),
texture = new Vector2(1, 1)
},
new DVertexType()
{
position = new Vector3(-1, -1, 0),
texture = new Vector2(0, 1)
},*/



/*// Bottom left.
new DVertex()
{
    position = new Vector3(-sizeWidther, -sizeHeighter, 0) * tileSize,
                        texture = new Vector2(0, 1),
                        color = _color,
                        normal = new Vector3(1, 0, 0),
                         
                    },
					// Top middle.
					new DVertex()
{
    position = new Vector3(-sizeWidther, sizeHeighter, 0) * tileSize,
                        texture = new Vector2(0, 0),
                        color = _color,
                        normal = new Vector3(1, 0, 0),
                    },
					// Bottom right.
					new DVertex()
{
    position = new Vector3(sizeWidther, -sizeHeighter, 0) * tileSize,
                        texture = new Vector2(1, 1),
                        color = _color,
                        normal = new Vector3(1, 0, 0),
                    },
                    new DVertex()
{
    position = new Vector3(sizeWidther, sizeHeighter, 0) * tileSize,
                        texture = new Vector2(1, 0),
                        color = _color,
                        normal = new Vector3(1, 0, 0),
                    }*/