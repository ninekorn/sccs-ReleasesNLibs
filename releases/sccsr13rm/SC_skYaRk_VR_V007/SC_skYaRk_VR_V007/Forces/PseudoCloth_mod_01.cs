using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Jitter.Dynamics;
using Jitter.Collision.Shapes;
using Jitter.LinearMath;
using Jitter.Dynamics.Constraints;
using Jitter.Dynamics.Joints;
using Jitter;
using Jitter.Collision;

using Jitter.Forces;


using SC_skYaRk_VR_V007.SC_Graphics;
using SC_skYaRk_VR_V007;

using SharpDX;

using System.Collections;

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



namespace Jitter.Forces
{
    public class PseudoCloth
    {
        private int VertexCount { get; set; }
        public int IndexCount { get; set; }
        public DVertex[] Vertices { get; set; }
        public int[] indices { get; set; }
     

        private SharpDX.Direct3D11.Buffer VertexBuffer { get; set; }
        private SharpDX.Direct3D11.Buffer IndexBuffer { get; set; }

        public List<Constraint> constraints = new List<Constraint>();
        [StructLayout(LayoutKind.Sequential)]
        public struct DVertex
        {
            public Vector3 position;
            public Vector2 texture;
            public Vector4 color;
            public Vector3 normal;
        };

        public class PseudoClothBody : RigidBody
        {
            public PseudoClothBody(float sphereRadius) : base(new SphereShape(sphereRadius)) 
            { 
            
            }
        }

        int sizeX, sizeY;
        float scale;

        World world;

        PseudoClothBody[] bodies;

        public PseudoCloth(SharpDX.Direct3D11.Device _device, World world, int sizeX, int sizeY,int sizeZ, float scale,Vector4 _color)
        {
            this.world = world;
       
            var xSize = sizeX;
            var ySize = sizeY;


            VertexCount = (xSize + 1) * (ySize + 1);
            IndexCount = xSize * ySize * 6;
            //bodies = new PseudoClothBody[sizeX * sizeY];
            bodies = new PseudoClothBody[VertexCount];



            Vector3[] vertices = new Vector3[(xSize + 1) * (ySize + 1)];
            //Vector2[] uv = new Vector2[vertices.Length];

            Vertices = new DVertex[(xSize + 1) * (ySize + 1)];

             indices = new int[xSize * ySize * 6];

            for (int i = 0, y = 0; y <= ySize; y++)
            {
                for (int x = 0; x <= xSize; x++, i++)
                {
                    vertices[i] = new Vector3(x * xSize, 1 * ySize, y * sizeZ);
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
                Vertices[i] = new DVertex()
                {
                    position = vertices[i],
                    color = _color,// new Vector4(1, 0, 0, 1),
                    normal = new Vector3(0, 0, 0)
                };

                bodies[i] = new PseudoClothBody(0.1f); //new SphereShape(0.1f)
                bodies[i].Position = new JVector(Vertices[i].position.X, Vertices[i].position.Y - 5, Vertices[i].position.Z);// new JVector(i * scale, 0, e * scale) + JVector.Up * 1;
                
                bodies[i].Material.StaticFriction = 0.5f;
                bodies[i].Material.KineticFriction = 0.5f;
                bodies[i].Mass = 0.1f;
                bodies[i].Tag = SC_Console_DIRECTX.BodyTag.testChunkCloth;
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





            /*for (int i = 0; i < sizeX; i++)
            {
                for (int e = 0; e < sizeX; e++)
                {
                    bodies[i + e * sizeY] = new PseudoClothBody(0.1f); //new SphereShape(0.1f)
                    bodies[i + e * sizeY].Position = new JVector(i * scale, 0, e * scale) + JVector.Up * 1;
                    bodies[i + e * sizeY].Material.StaticFriction = 0.5f;
                    bodies[i + e * sizeY].Material.KineticFriction = 0.5f;
                    bodies[i + e * sizeY].Mass = 0.1f;
                    bodies[i + e * sizeY].Tag = SC_Console_DIRECTX.BodyTag.testChunkCloth;
                    world.AddBody(bodies[i + e * sizeY]);

                    if (i == 0 && e == 0)
                    {
                        bodies[i + e * sizeY].IsStatic = true;
                    }
                    else if (i == sizeX - 1 && e == 0)
                    {
                        bodies[i + e * sizeY].IsStatic = true;
                    }
                    else if (i == 0 && e == sizeX - 1)
                    {
                        bodies[i + e * sizeY].IsStatic = true;
                    }
                    else if (i == sizeX - 1 && e == sizeX - 1)
                    {
                        bodies[i + e * sizeY].IsStatic = true;
                    }
                }
            }*/

            world.CollisionSystem.PassedBroadphase += new Collision.PassedBroadphaseHandler(CollisionSystem_PassedBroadphase);
            world.Events.PostStep += new World.WorldStep(world_PostStep);




            /*for (int i = 0; i < sizeX; i++)
            {
                for (int e = 0; e < sizeY; e++)
                {
                    if (i + 1 < sizeX)
                    {
                        AddDistance(e * sizeY + i, (i + 1) + e * sizeY);
                        // (i,e) and (i+1,e)
                    }

                    if (e + 1 < sizeY)
                    {
                        AddDistance(e * sizeY + i, ((e + 1) * sizeY) + i);
                        // (e,i) and (e+1,i)

                    }

                    if( (i + 1 < sizeX) && (e + 1 < sizeY))
                    {
                        AddDistance(e * sizeY + i, ((e + 1) * sizeY) +( i+1));
                    }


                    if ((i > 0) && (e + 1 < sizeY))
                    {
                        AddDistance(e * sizeY + i, ((e + 1) * sizeY) + (i - 1));
                    }
                }
            }*/

            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.scale = scale;


            VertexBuffer = SharpDX.Direct3D11.Buffer.Create(_device, BindFlags.VertexBuffer, Vertices);

            // Create the index buffer.
            IndexBuffer = SharpDX.Direct3D11.Buffer.Create(_device, BindFlags.IndexBuffer, indices);

        }

        void world_PostStep(float timeStep)
        {
            CheckConstraints();
        }
        public RigidBody GetCorner(int e, int i)
        {
            return bodies[e * sizeY + i];
        }

        private void AddDistance(int p1, int p2)
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
            this.constraints.Add(dcer);*/
        }
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
            return !(body1 is PseudoClothBody && body2 is PseudoClothBody); //PseudoClothBody
        }

        public void Render(DeviceContext deviceContext, SharpDX.Direct3D11.Device device)
        {
            // Put the vertex and index buffers on the graphics pipeline to prepare for drawings.
            RenderBuffers(deviceContext, device);
        }


        private void RenderBuffers(DeviceContext deviceContext, SharpDX.Direct3D11.Device device)
        {
            // Create the vertex buffer.
            //
            //VertexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, Vertices);
            // Create the index buffer.
            //IndexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, indices);

            // Set the vertex buffer to active in the input assembler so it can be rendered.
            deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, Utilities.SizeOf<SC_VR_Touch_Shader.DVertex>(), 0));

            // Set the index buffer to active in the input assembler so it can be rendered.
            deviceContext.InputAssembler.SetIndexBuffer(IndexBuffer, SharpDX.DXGI.Format.R32_UInt, 0);

            // Set the type of the primitive that should be rendered from this vertex buffer, in this case triangles.
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
        }
    }
}
