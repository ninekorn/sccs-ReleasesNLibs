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

using SC_skYaRk_VR_V007;

using Jitter.Forces;


//using SC_WPF_RENDER.SC_Graphics.SC_Models;
using SharpDX;

using System.Collections;

namespace Jitter.Forces
{
    public class PseudoCloth : SC_VR_Cloth, ITransform, IComponent
    {
        public ITransform transform { get; private set; }
        IComponent ITransform.Component
        {
            get => component;
        }
        IComponent component;
        RigidBody IComponent.rigidbody { get; set; }

        SoftBody IComponent.softbody { get; set; }


        public List<Constraint> constraints = new List<Constraint>();

        public class PseudoClothBody : RigidBody
        {
            //SC_VR_Cube tester = new SC_VR_Cube();

            public PseudoClothBody(float sphereRadius) : base(new SphereShape(sphereRadius))//new BoxShape(0.5f, 0.2f, 0.5f)) //new SphereShape(sphereRadius) float sphereRadius
            {
                //tester = test;
            }
        }

        int sizeX, sizeY;
        float scale;

        World world;

        PseudoClothBody[] bodies;
        //SC_VR_Cube[] bodies;



        public PseudoCloth(World world, int sizeX, int sizeY, float scale) //World world, 
        {
            this.world = world;

            transform = this;
            component = this;

            /*bodies = new PseudoClothBody[sizeX * sizeY];
            //bodies = new SC_VR_Cube[sizeX * sizeY];

            for (int i = 0; i < sizeX; i++)
            {
                for (int e = 0; e < sizeX; e++)
                {
                    //Console.WriteLine("fuck off");
                    //SC_VR_Cube test = new SC_VR_Cube();// new PseudoClothBody(0.1f);
                    //test.Initialize(SC_Console_DIRECTX._dxDevice.Device, 0.25f, 0.1f, 0.25f, new Vector4(0.1f, 0.1f, 0.1f, 1));
                    //test.transform.Component.rigidbody = new RigidBody(new BoxShape(0.5f, 0.2f, 0.5f));
                    //test.transform.Component.rigidbody.Position = new JVector(i * scale, 0, e * scale); //+ JVector.Up * 10.0f
                    //test.transform.Component.rigidbody.Material.KineticFriction = 50;
                    //test.transform.Component.rigidbody.Material.StaticFriction = 50;
                    //test.transform.Component.rigidbody.Mass = 0.1f; //1
                    //test.transform.Component.rigidbody.Tag = DirectXComponent.BodyTag.someTest;

                    /*List<JVector> vertices = new List<JVector>();


                    for (int j = 0; j < test.Vertices.Length; j++)
                    {
                        vertices.Add(new JVector(test.Vertices[j].position.X, test.Vertices[j].position.Y, test.Vertices[j].position.Z));
                    }

                    //test.transform.Component.rigidbody.Shape = new ConvexHullShape(vertices);

                    bodies[i + e * sizeY] = new PseudoClothBody();
                    bodies[i + e * sizeY].Position = new JVector(i * scale, 0, e * scale) + JVector.Up * 5; // + JVector.Up * 10.0f //i * scale, 0, e * scale
                    bodies[i + e * sizeY].Material.KineticFriction = 50;
                    bodies[i + e * sizeY].Material.StaticFriction = 50;
                    bodies[i + e * sizeY].Mass = 0.1f; //1
                    bodies[i + e * sizeY].Tag = SC_Console_DIRECTX.BodyTag.someothertest;
                    bodies[i + e * sizeY].Shape = new ConvexHullShape(vertices);
                   
                    //bodies[i + e * sizeY].IsStatic = true;

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

                    SC_Console_GRAPHICS._arrayOfClothCubes.Add(test);
                    //DirectXComponent.World.AddBody(bodies[i + e * sizeY].transform.Component.rigidbody);

                    world.AddBody(bodies[i + e * sizeY]);
                    //DirectXComponent.World.AddBody(test.transform.Component.rigidbody);
                }
            }*/






            /*List<TriangleVertexIndices> indicess = new List<TriangleVertexIndices>();
            List<JVector> verticess = new List<JVector>();

            for (int i = 0; i < Vertices.Length; i++)
            {
                verticess.Add(new JVector(Vertices[i].position.X, Vertices[i].position.Y, Vertices[i].position.Z));
            }

            for (int i = 0; i < triangleIndices.Length / 3; i++)
            {
                //var one = indices[(i * 3) + 0];
                //var two = indices[(i * 3) + 1];
                //var three = indices[(i * 3) + 2];
                indicess.Add(new TriangleVertexIndices(triangleIndices[(i * 3) + 0], triangleIndices[(i * 3) + 1], triangleIndices[(i * 3) + 2]));
            }

            transform.Component.softbody = new SoftBody(indicess, verticess);
            transform.Component.softbody.Mass = 0.01f;
            transform.Component.softbody.TriangleExpansion = 0.010f;
            transform.Component.softbody.VertexExpansion = 0.01f;

            world.AddBody(transform.Component.softbody);
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

                    if ((i + 1 < sizeX) && (e + 1 < sizeY))
                    {
                        AddDistance(e * sizeY + i, ((e + 1) * sizeY) + (i + 1));
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
            dc.Softness = 0.1f; //2
            dc.BiasFactor = 0.1f;
            //dc.Distance = 0.01f;


            world.AddConstraint(dc);
            //this.constraints.Add(dc);

            /*PointPointDistance dcer = new PointPointDistance(DirectXComponent._arrayOfClothCubes[p1].transform.Component.rigidbody, DirectXComponent._arrayOfClothCubes[p2].transform.Component.rigidbody, DirectXComponent._arrayOfClothCubes[p1].transform.Component.rigidbody.Position, DirectXComponent._arrayOfClothCubes[p2].transform.Component.rigidbody.Position);
            dcer.Softness = 0.0000001f; //2
            dcer.BiasFactor = 0.000000001f;
            dcer.Distance = 0.01f;

            world.AddConstraint(dcer);
            this.constraints.Add(dcer);*/
        }

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

        private bool CollisionSystem_PassedBroadphase(IBroadphaseEntity body1, IBroadphaseEntity body2) //RigidBody body1, RigidBody body2
        {
            //Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            // prevent PseudoClothBody,PseudoClothBody collisions
            return !(body1 is PseudoClothBody && body2 is PseudoClothBody); //PseudoClothBody
        }
    }
}
