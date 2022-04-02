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
using SC_skYaRk_VR_V007.SC_Graphics;

namespace Jitter.Forces
{
    public class PseudoCloth
    {
        public List<Constraint> constraints = new List<Constraint>();

        public class PseudoClothBody : RigidBody
        {
            //SC_VR_Cube tester = new SC_VR_Cube();

            public PseudoClothBody() : base(new BoxShape(0.5f, 0.2f, 0.5f)) //new SphereShape(sphereRadius) float sphereRadius
            {
                //tester = test;
            }
        }

        int sizeX, sizeY;
        float scale;

        World world;

        PseudoClothBody[] bodies;
        //SC_VR_Cube[] bodies;

        public SC_cube _cube;

        public PseudoCloth(World world, SC_Console_DIRECTX D3D, IntPtr windowsHandle, int sizeX, int sizeY, float scale, Matrix matter,int instX, int instY, int instZ) //World world, 
        {
            this.world = world;
            bodies = new PseudoClothBody[sizeX * sizeY];
            //bodies = new SC_VR_Cube[sizeX * sizeY];

            for (int i = 0; i < sizeX; i++)
            {
                for (int e = 0; e < sizeX; e++)
                {
                    //Console.WriteLine("fuck off");
                    _cube = new SC_cube();// new PseudoClothBody(0.1f);


                    //_cube.Initialize(SC_Console_DIRECTX._device, 0.1f, 0.1f, 0.1f,1,1,1, matter, 1, 1, 1);

                    float r = 0.05f;
                    float g = 0.05f;
                    float b = 0.05f;
                    float a = 1;

                     instX = 1;
                     instY = 1;
                     instZ = 1;

                    float offsetPosX = 0;
                    float offsetPosY = 0;
                    float offsetPosZ = 0;

                    Matrix _tempMatroxer = matter;
                    //_tempMatroxer.M42 = 0;


                    float _size_screen = 0.0006f;
                    bool _hasinit0 = _cube.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, _size_screen, 1, 1, 0.05f, 0.01f, 0.05f, new Vector4(r, g, b, a), instX, instY, instZ, windowsHandle, _tempMatroxer, 3, offsetPosX, offsetPosY, offsetPosZ); //, "terrainGrassDirt.bmp" //0.00035f


                    //float _size_screen = 0.0006f;
                    //bool _hasinit0 = _cube.Initialize(D3D, D3D.SurfaceWidth, D3D.SurfaceHeight, _size_screen, 1, 1, r, g, b, a); //, "terrainGrassDirt.bmp" //0.00035f //, instX, instY, instZ, windowsHandle, _tempMatroxer, 3, offsetPosX, offsetPosY, offsetPosZ









                    //test.Initialize(DirectXComponent._device, 0.25f, 0.1f, 0.25f, new Vector4(0.1f, 0.1f, 0.1f, 1));

                    //SharpDX.Direct3D11.Device device, float _sizeX, float _sizeY, float _sizeZ

                    //test.transform.Component.rigidbody = new RigidBody(new BoxShape(0.5f, 0.2f, 0.5f));
                    //test.transform.Component.rigidbody.Position = new JVector(i * scale, 0, e * scale); //+ JVector.Up * 10.0f
                    //test.transform.Component.rigidbody.Material.KineticFriction = 50;
                    //test.transform.Component.rigidbody.Material.StaticFriction = 50;
                    //test.transform.Component.rigidbody.Mass = 0.1f; //1
                    //test.transform.Component.rigidbody.Tag = DirectXComponent.BodyTag.someTest;

                    List<JVector> vertices = new List<JVector>();


                    for (int j = 0;j < _cube.Vertices.Length; j++) //_cube.Vertices.Length
                    {
                        
                        vertices.Add(new JVector(_cube.Vertices[j].position.X, _cube.Vertices[j].position.Y, _cube.Vertices[j].position.Z));
                        //vertices.Add(new JVector(_cube.instances[j].position.X, _cube.instances[j].position.Y, _cube.instances[j].position.Z));
                    }




















                    //test.transform.Component.rigidbody.Shape = new ConvexHullShape(vertices);

                    bodies[i + e * sizeY] = new PseudoClothBody();
                    bodies[i + e * sizeY].Position = new JVector(i * scale, 0, e * scale) + new JVector(_tempMatroxer.M41, _tempMatroxer.M42, _tempMatroxer.M43); //  + JVector.Up * 5 + JVector.Up * 10.0f //i * scale, 0, e * scale                  
                    bodies[i + e * sizeY].Material.StaticFriction = 0.13f;
                    //bodies[i + e * sizeY].Material.KineticFriction = 0.13f;
                    bodies[i + e * sizeY].Mass = 10f; //1
                    bodies[i + e * sizeY].Tag = SC_Console_DIRECTX.BodyTag.pseudoCloth;
                    bodies[i + e * sizeY].Shape = new ConvexHullShape(vertices);



                    //bodies[i + e * sizeY].IsStatic = true;

                    if (i == 0 && e ==0)
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


                    //TO READADD OTHERWISE IT WONT WORK.
                    SC_Console_GRAPHICS._arrayOfClothCubes.Add(_cube);
                    //TO READADD OTHERWISE IT WONT WORK.
                






                    //DirectXComponent.World.AddBody(bodies[i + e * sizeY].transform.Component.rigidbody);*/

                    world.AddBody(bodies[i + e * sizeY]);
                    //DirectXComponent.World.AddBody(test.transform.Component.rigidbody);
                }
            }

            world.CollisionSystem.PassedBroadphase += new Collision.PassedBroadphaseHandler(CollisionSystem_PassedBroadphase);
            world.Events.PostStep += new World.WorldStep(world_PostStep);

  

            for (int i = 0; i < sizeX; i++)
            {
                for (int e = 0; e < sizeY; e++)
                {
                    //i *= 0.1f;

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
            }

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
