using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

using System.Linq;
using System;



using Jitter.Collision;
using Jitter;
using Jitter.Dynamics;
using Jitter.DataStructures;
using Jitter.Collision.Shapes;
using Jitter.LinearMath;

using System.Collections.Generic;
using System.Collections;
using System.Runtime.InteropServices;



using System.Text;


using Jitter.Dynamics.Constraints;
using Jitter.Dynamics.Joints;

using Jitter.Forces;


using SC_skYaRk_VR_V007.SC_Graphics;
using SC_skYaRk_VR_V007;




namespace SC_skYaRk_VR_V007
{

    public class SC_VR_Cloth : ITransform, IComponent
    {









        public List<Constraint> constraints = new List<Constraint>();

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

        private SharpDX.Direct3D11.Buffer VertexBuffer { get; set; }
        private SharpDX.Direct3D11.Buffer IndexBuffer { get; set; }
        private int VertexCount { get; set; }
        public int IndexCount { get; set; }

        public SC_VR_Touch_Shader.DVertex[] Vertices { get; set; }

        public SharpDX.Vector3 Position { get; set; }
        public SharpDX.Quaternion Rotation { get; set; }
        public SharpDX.Vector3 Forward { get; set; }

        private float _sizeX = 0;
        private float _sizeY = 0;
        private float _sizeZ = 0;

        // Constructor
        public SC_VR_Cloth() { }

        public Vector4 _color;
        public int[] indices { get; set; }
        //public SoftBody softBody;

        int _width = 0;
        int _height = 0;
        int _depth = 0;


        public int xSize, ySize;

        private Vector3[] vertices;
        public SC_VR_Cloth_instances _singleObjectOnly;// = new SC_cube_instances();


        World _world;

        // Methods.
        public bool Initialize(SharpDX.Direct3D11.Device device, float x, float y, float z, Vector4 color, int width, int height, int depth, World world) //, int sizeX, int sizeY, int sizeZ
        {

            this._world = world;

            this.transform = this;
            this.component = this;




            this._color = color;
            this._sizeX = x;
            this._sizeY = y;
            this._sizeZ = z;


            this._width = width;
            this._height = height;
            this._depth = depth;

            this.xSize = width;
            this.ySize = depth;



            // Initialize the vertex and index buffer that hold the geometry for the triangle.
            return InitializeBuffer(device);
        }
        public void ShutDown()
        {
            // Release the vertex and index buffers.
            ShutDownBuffers();
        }
        public void Render(DeviceContext deviceContext, SharpDX.Direct3D11.Device device)
        {
            // Put the vertex and index buffers on the graphics pipeline to prepare for drawings.
            RenderBuffers(deviceContext, device);
        }



        private bool InitializeBuffer(SharpDX.Direct3D11.Device device)
        {
            try
            {
                VertexCount = (xSize + 1) * (ySize + 1);
                IndexCount = xSize * ySize * 6;

                vertices = new Vector3[(xSize + 1) * (ySize + 1)];
                //Vector2[] uv = new Vector2[vertices.Length];

                Vertices = new SC_VR_Touch_Shader.DVertex[(xSize + 1) * (ySize + 1)];

                indices = new int[xSize * ySize * 6];

                for (int i = 0, y = 0; y <= ySize; y++)
                {
                    for (int x = 0; x <= xSize; x++, i++)
                    {
                        vertices[i] = new Vector3(x * _sizeX, 1 * _sizeY, y * _sizeZ);
                        //uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
                    }
                }

                int[] trigs = new int[xSize * ySize * 6];
                for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
                {
                    for (int x = 0; x < xSize; x++, ti += 6, vi++)
                    {


                        trigs[ti] = vi;
                        trigs[ti + 3] = trigs[ti + 2] = vi + 1;
                        trigs[ti + 4] = trigs[ti + 1] = vi + xSize + 1;
                        trigs[ti + 5] = vi + xSize + 2;
                    }
                }
                int[] triangles = new int[xSize * ySize * 6];

                for (int i = 0; i < triangles.Length; i++)
                {
                    triangles[i] = trigs[triangles.Length - 1 - i];
                }

                for (int i = 0; i < vertices.Length; i++)
                {
                    Vertices[i] = new SC_VR_Touch_Shader.DVertex()
                    {
                        position = vertices[i],
                        color = _color,// new Vector4(1, 0, 0, 1),
                        normal = new Vector3(0, 0, 0)
                    };
                }

                for (int i = 0; i < triangles.Length; i++)
                {
                    //var one = indices[(i * 3) + 0];
                    //var two = indices[(i * 3) + 1];
                    //var three = indices[(i * 3) + 2];
                    indices[i] = triangles[i];
                    //indices[(i * 3) + 1] = triangles[(i * 3) + 1];
                    //indices[(i * 3) + 2] = triangles[(i * 3) + 2];

                }










                List<TriangleVertexIndices> indicess = new List<TriangleVertexIndices>();
                List<JVector> verticess = new List<JVector>();

                for (int i = 0; i < Vertices.Length; i++)
                {
                    verticess.Add(new JVector(Vertices[i].position.X, Vertices[i].position.Y, Vertices[i].position.Z));
                }

                for (int i = 0; i < indices.Length / 3; i++)
                {
                    indicess.Add(new TriangleVertexIndices(indices[(i * 3) + 0], indices[(i * 3) + 1], indices[(i * 3) + 2]));
                }

                //_singleObjectOnly = new SC_VR_Cloth_instances();
                this.transform.Component.rigidbody = null;
                this.transform.Component.softbody = new SoftBody(indicess, verticess);

                this.transform.Component.softbody.Mass = 1f;
                this.transform.Component.softbody.Pressure = 15; //0.00075f

                this.transform.Component.softbody.Material.KineticFriction = 10;
                this.transform.Component.softbody.Material.StaticFriction = 10;
                this.transform.Component.softbody.Material.Restitution = 0;




                this.transform.Component.softbody.Tag = SC_Console_DIRECTX.BodyTag.testChunkCloth;

                //SC_Console_GRAPHICS.World.AddBody(this.transform.Component.softbody);



                /*_singleObjectOnly.transform.Component.softbody.Mass = 0.01f;
                _singleObjectOnly.transform.Component.softbody.TriangleExpansion = 0.010f;
                _singleObjectOnly.transform.Component.softbody.VertexExpansion = 0.01f;

                _singleObjectOnly.transform.Component.softbody.Pressure = 15; //0.00075f

                _singleObjectOnly.transform.Component.softbody.Material.KineticFriction = 10;
                _singleObjectOnly.transform.Component.softbody.Material.StaticFriction = 10;
                _singleObjectOnly.transform.Component.softbody.Material.Restitution = 0;

                _singleObjectOnly.transform.Component.softbody.SetSpringValues(SoftBody.SpringType.EdgeSpring, 0.1f, 0.001f);
                _singleObjectOnly.transform.Component.softbody.SetSpringValues(SoftBody.SpringType.ShearSpring, 0.1f, 0.001f);
                _singleObjectOnly.transform.Component.softbody.SetSpringValues(SoftBody.SpringType.BendSpring, 0.1f, 0.001f);
                */

                /*try
                {
                    _world.AddBody(this.transform.Component.softbody);
                }
                catch (Exception ex)
                {
                    MessageBox((IntPtr)0, ex.ToString(), "Oculus error", 0);
                }*/

                _world.CollisionSystem.PassedBroadphase += new Jitter.Collision.PassedBroadphaseHandler(CollisionSystem_PassedBroadphase);
                _world.Events.PostStep += new World.WorldStep(world_PostStep);





                /*if (SC_Console_GRAPHICS.World == null)
                {
                    MessageBox((IntPtr)0, "null", "Oculus error", 0);
                }*/















                /*transform.Component.softbody = new SoftBody(indicess, verticess);
                transform.Component.softbody.Mass = 0.01f;
                transform.Component.softbody.TriangleExpansion = 0.010f;
                transform.Component.softbody.VertexExpansion = 0.01f;

                transform.Component.softbody.Pressure = 15; //0.00075f

                transform.Component.softbody.Material.KineticFriction = 10;
                transform.Component.softbody.Material.StaticFriction = 10;
                transform.Component.softbody.Material.Restitution = 0;

                transform.Component.softbody.Tag = SC_Console_DIRECTX.BodyTag.testChunkCloth;
                transform.Component.softbody.SetSpringValues(SoftBody.SpringType.EdgeSpring, 0.1f, 0.001f);
                transform.Component.softbody.SetSpringValues(SoftBody.SpringType.ShearSpring, 0.1f, 0.001f);
                transform.Component.softbody.SetSpringValues(SoftBody.SpringType.BendSpring, 0.1f, 0.001f);
                //transform.Component.softbody.SelfCollision = false;
                var _vertexBodies = transform.Component.softbody.VertexBodies;

                for (int i = 0; i < _vertexBodies.Count; i++)
                {
                    var singleVertexBody = _vertexBodies[i];
                    singleVertexBody.Mass = 0.5f;
                    //singleVertexBody.Position += new JVector(posX, posY, posZ);
                }

                SC_Console_GRAPHICS.World.AddBody(transform.Component.softbody);*/







                /*Vertices = new SC_VR_Touch_Shader.DVertex[_width * _depth];

                int counter = 0;
                for (int x = 0; x < _width; x++)
                {
                    for (int z = 0; z < _depth; z++)
                    {
                        Vertices[counter] = new SC_VR_Touch_Shader.DVertex()
                        {
                            position = new Vector3(x * _sizeX, 1 * _sizeY, z * _sizeZ),
                            color = _color,// new Vector4(1, 0, 0, 1),
                            normal = new Vector3(0, 0, 0)
                        };
                        counter++;
                        //Console.WriteLine(counter);
                    }
                }

                indices = new int[]
                {
                    0, // Bottom left.
                    1, // Top middle.
                    2,  // Bottom right.
                    3,
                    2,
                    1,

                    1,
                    4,
                    3,
                    5,
                    3,
                    4,

                    /*4,
                    6,
                    5,
                    7,
                    5,
                    6,

                    2,
                    3,
                    8,
                    9,
                    8,
                    3,
                };*/

                /*List<TriangleVertexIndices> indicess = new List<TriangleVertexIndices>();
                List<JVector> verticess = new List<JVector>();

                for (int i = 0;i < Vertices.Length;i++)
                {
                    verticess.Add(new JVector(Vertices[i].position.X, Vertices[i].position.Y, Vertices[i].position.Z));
                }

                for (int i = 0; i < indices.Length/3; i++)
                {
                    //var one = indices[(i * 3) + 0];
                    //var two = indices[(i * 3) + 1];
                    //var three = indices[(i * 3) + 2];
                    indicess.Add(new TriangleVertexIndices(indices[(i * 3) + 0], indices[(i * 3) + 1], indices[(i * 3) + 2]));
                }*/

                //this.transform.Component.softbody = new SoftBody(indicess, verticess);

















                //Console.WriteLine("CLOTH BUILTTTTTTTTTTTTTTTTTTTTTTTTTTTTT");
                //DirectXComponent.World.AddBody(this.transform.Component.softbody);


















                /*Vertices = new[]
                {
                    new SC_VR_Touch_Shader.DVertex()
                    {
                        position = new Vector3(-1*_sizeX, -1*_sizeY, 1*_sizeZ),
                        color = _color,// new Vector4(1, 0, 0, 1),
                        normal = new Vector3(0, 0, 0)

                    },
                    new SC_VR_Touch_Shader.DVertex()
                    {
                        position = new Vector3(-1*_sizeX, 1*_sizeY, 1*_sizeZ),
                        color = _color,//color = new Vector4(1, 0, 0, 1),
                        normal = new Vector3(0, 0, 0)

                    },
                    new SC_VR_Touch_Shader.DVertex()
                    {
                        position = new Vector3(1*_sizeX, -1*_sizeY, 1*_sizeZ),
                        color = _color,//color = new Vector4(1, 0, 0, 1),
                        normal = new Vector3(0, 0, 0)
                    },
                    new SC_VR_Touch_Shader.DVertex()
                    {
                        position = new Vector3(1*_sizeX, 1*_sizeY, 1*_sizeZ),
                        color = _color,//color = new Vector4(1, 0, 0, 1),
                        normal = new Vector3(0, 0, 0)
                    },


                    new SC_VR_Touch_Shader.DVertex()
                    {
                        position = new Vector3(-1*_sizeX, -1*_sizeY, -1*_sizeZ),
                        color = _color,//color = new Vector4(1, 0, 0, 1),
                        normal = new Vector3(0, 0, 0)
                    },
                    new SC_VR_Touch_Shader.DVertex()
                    {
                        position = new Vector3(-1*_sizeX, 1*_sizeY, -1*_sizeZ),
                        color = _color,//color = new Vector4(1, 0, 0, 1),
                        normal = new Vector3(0, 0, 0)
                    },
                    new SC_VR_Touch_Shader.DVertex()
                    {
                        position = new Vector3(1*_sizeX, -1*_sizeY, -1*_sizeZ),
                        color = _color,//color = new Vector4(1, 0, 0, 1),
                        normal = new Vector3(0, 0, 0)
                    },
                    new SC_VR_Touch_Shader.DVertex()
                    {
                        position = new Vector3(1*_sizeX, 1*_sizeY, -1*_sizeZ),
                        color = _color,//color = new Vector4(1, 0, 0, 1),
                        normal = new Vector3(0, 0, 0)
                    },                
                };

                // Create Indicies to load into the IndexBuffer.
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

                // Create the vertex buffer.
                VertexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, Vertices);

                // Create the index buffer.
                IndexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, indices);

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
        }

        private void RenderBuffers(DeviceContext deviceContext, SharpDX.Direct3D11.Device device)
        {
            // Create the vertex buffer.
            //

            // Create the index buffer.
            //IndexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, indices);

            // Set the vertex buffer to active in the input assembler so it can be rendered.
            deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, Utilities.SizeOf<SC_VR_Touch_Shader.DVertex>(), 0));

            // Set the index buffer to active in the input assembler so it can be rendered.
            deviceContext.InputAssembler.SetIndexBuffer(IndexBuffer, SharpDX.DXGI.Format.R32_UInt, 0);

            // Set the type of the primitive that should be rendered from this vertex buffer, in this case triangles.
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
        }

        void world_PostStep(float timeStep)
        {
            CheckConstraints();
        }
        /*public RigidBody GetCorner(int e, int i)
        {
            return bodies[e * sizeY + i];
        }*/

        /*private void AddDistance(int p1, int p2)
        {
            //Console.WriteLine("AddDistance");
            PointPointDistance dc = new PointPointDistance(bodies[p1], bodies[p2], bodies[p1].Position, bodies[p2].Position);
            //Console.WriteLine(bodies[p1].Position + " _ " + bodies[p2].Position);

            //DistanceConstraint dc = new DistanceConstraint(bodies[p1], bodies[p2], bodies[p1].position, bodies[p2].position);
            dc.Softness = 0.0001f; //2
            dc.BiasFactor = 0.75f;
            //dc.Distance *= 0.01f;
            dc.Behavior = PointPointDistance.DistanceBehavior.LimitDistance;

            world.AddConstraint(dc);
            this.constraints.Add(dc);

            /*PointPointDistance dcer = new PointPointDistance(DirectXComponent._arrayOfClothCubes[p1].transform.Component.rigidbody, DirectXComponent._arrayOfClothCubes[p2].transform.Component.rigidbody, DirectXComponent._arrayOfClothCubes[p1].transform.Component.rigidbody.Position, DirectXComponent._arrayOfClothCubes[p2].transform.Component.rigidbody.Position);
            dcer.Softness = 0.0000001f; //2
            dcer.BiasFactor = 0.000000001f;
            dcer.Distance = 0.01f;

            world.AddConstraint(dcer);
            this.constraints.Add(dcer);
        }*/
        /*public RigidBody GetCorner(int e,int i)
        {
            return bodies[e * sizeY + i];
        }

       

        private void AddDistance(int p1, int p2)
        {
            DistanceConstraint dc = new DistanceConstraint(bodies[p1], bodies[p2], bodies[p1].position, bodies[p2].position);
            dc.Softness = 2f;
            dc.BiasFactor = 0.1f;
            world.AddConstraint(dc);
            this.constraints.Add(dc);
        }*/

        /*public void CheckConstraints()
        {
            foreach (Constraint c in constraints)
            {
                if ((c as DistanceConstraint).AppliedImpulse.Length() > 1.8f)
                {
                    world.constraints.Remove(c);
                }
            }
        }*/

        public void CheckConstraints()
        {
            foreach (Constraint c in constraints)
            {
                /*if ((c as PointPointDistance).AppliedImpulse > 0.0001)
                {
                    world.RemoveConstraint(c);
                }*/
            }
        }





        /*private bool CollisionSystem_PassedBroadphase(RigidBody body1, RigidBody body2)
        {
            // prevent PseudoClothBody,PseudoClothBody collisions
            return !(body1 is PseudoClothBody && body2 is PseudoClothBody);
        }*/


        private bool CollisionSystem_PassedBroadphase(IBroadphaseEntity body1, IBroadphaseEntity body2) //RigidBody body1, RigidBody body2
        {
            //Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            // prevent PseudoClothBody,PseudoClothBody collisions
            return !(body1 is SC_VR_Cloth && body2 is SC_VR_Cloth); //PseudoClothBody
        }




    }
}