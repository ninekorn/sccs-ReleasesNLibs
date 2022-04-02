
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using SharpDX;
using SharpDX.DirectInput;
using System.Windows.Media.Media3D;
using System.Windows;
using System.Windows.Media;

using Matrix = SharpDX.Matrix;
using Quaternion = SharpDX.Quaternion;
using Segment = SC_Console_APP.SC_Segment.Segment;
using System.Threading;

namespace SC_Console_APP
{
    public class SC_Update : SC_Intermediate_Update
    {
        //List<DModelClassCircle> circle_object_list = new List<DModelClassCircle>();

        SC_Console_APP.SC_Graphics.SC_cube.DLightBuffer[] _DLightBuffer_cube = new SC_Console_APP.SC_Graphics.SC_cube.DLightBuffer[1];


        Stopwatch timeWatch = new Stopwatch();


        Matrix _viewMatrix;
        Matrix _projectionMatrix;
        DMatrixBuffer[] arrayOfMatrixBuff = new DMatrixBuffer[1];
        float speed = 0.01f;

        int someAngle;
        float gravity = -9.81f;
        float theGoalDist = 0;
        SC_RigidInfo obj1;
        SC_RigidInfo obj2;

        SC_RigidInfo obj11;
        SC_RigidInfo obj22;
        float highExtent = 0.9999f;
        float lowExtent = 0.0001f;

        float theTimer = 0;

        Stopwatch timeStopWatch = new Stopwatch();
        int watchSwitch = 0;

        float lastTheTimer = 0;
        Vector2? intersectorPoint;



        int killTask = 0;
        Task tsk;


        static float? lastAngle;



        protected override void SC_Init_DirectX()
        {
            timeStopWatch00.Start();
            time1 = DateTime.Now;

            if (watchSwitch == 0)
            {
                timeStopWatch.Stop();
                timeStopWatch.Reset();
                timeStopWatch.Start();
                watchSwitch = 1;
            }
            lastTheTimer = timeStopWatch.Elapsed.Milliseconds;
            tsk = DoWork(1);


            base.SC_Init_DirectX();
        }



        float _delta_timer_frame = 0;
        float _delta_timer_time = 0;
        DateTime time1;
        DateTime time2;
        float deltaTime;
        Stopwatch timeStopWatch00 = new Stopwatch();
        Stopwatch timeStopWatch01 = new Stopwatch();
        int _swtch = 0;
        int _swtch_counter_00 = 0;
        int _swtch_counter_01 = 0;
        int _swtch_counter_02 = 0;

        public async Task DoWork(int timeOut)
        {
            float startTime = (float)(timeStopWatch00.ElapsedMilliseconds);
        _threadLoop:

            if (_swtch == 0 || _swtch == 1)
            {
                if (_swtch == 0)
                {
                    if (_swtch_counter_00 >= 0)
                    {
                        ////////////////////
                        //UPGRADED DELTATIME
                        ////////////////////
                        //IMPORTANT PHYSICS TIME 

                        ////////////////////
                        //UPGRADED DELTATIME
                        ////////////////////
                        _swtch = 1;
                        _swtch_counter_00 = 0;
                    }
                }
                else if (_swtch == 1)
                {
                    if (_swtch_counter_01 >= 0)
                    {
                        ////////////////////
                        //UPGRADED DELTATIME
                        ////////////////////
                        timeStopWatch01.Start();
                        time2 = DateTime.Now;
                        ////////////////////
                        //UPGRADED DELTATIME
                        ////////////////////
                        _swtch = 2;
                        _swtch_counter_01 = 0;
                    }
                }
                else if (_swtch == 2)
                {

                }
            }

            /*//FRAME DELTATIME
            _delta_timer_frame = (float)Math.Abs((timeStopWatch01.Elapsed.Ticks - timeStopWatch00.Elapsed.Ticks)) * 100000000f;

            time2 = DateTime.Now;
            _delta_timer_time = (time2.Ticks - time1.Ticks) * 100000000f; //100000000f
            //time1 = time2;

            deltaTime = (float)Math.Abs(_delta_timer_time - _delta_timer_frame);
            */

            //FRAME DELTATIME
            _delta_timer_frame = (float)Math.Abs((timeStopWatch01.Elapsed.Ticks - timeStopWatch00.Elapsed.Ticks)) * 0.00000000001f;

            time2 = DateTime.Now;
            _delta_timer_time = (time2.Ticks - time1.Ticks) * 0.00000000001f; //100000000f
            //time1 = time2;

            deltaTime = (float)Math.Abs(_delta_timer_time - _delta_timer_frame);

            //time1 = time2;
            await Task.Delay(1);
            Thread.Sleep(timeOut);
            _swtch_counter_00++;
            _swtch_counter_01++;
            _swtch_counter_02++;

            goto _threadLoop;
        }


        Matrix worldmatrix_circle = Matrix.Identity;
        Matrix[] worldmatrix_polygons = new Matrix[7];

        int start_thread = 0;
        protected override void Update()
        {
            theTimer = deltaTime;

            if (theTimer > 1.0f * 0.01f)
            {
                theTimer = 1.0f * 0.01f;
            }

            impulse.dt = theTimer;

            Camera.Render();
            _viewMatrix = Camera.ViewMatrix;
            _projectionMatrix = proj;

            if (start_thread == 0)
            {
                Thread _mainTasker00 = new Thread((tester0000) =>
                {
                _thread_main_loop:



                    UpdateRigidBodies(theTimer);

                    Thread.Sleep(0);
                    goto _thread_main_loop;
                }, 0); //100000 //999999999

                _mainTasker00.IsBackground = false;
                _mainTasker00.SetApartmentState(ApartmentState.STA);
                _mainTasker00.Start();

                start_thread = 1;
            }


            Vector4 ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
            Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
            Vector3 lightDirection = new Vector3(0, -1, -1);
            Vector3 dirLight = new Vector3(0, -1, 0);
            Vector3 lightpos = new Vector3(0, 10, 0);



            _DLightBuffer_cube[0] = new SC_Console_APP.SC_Graphics.SC_cube.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };

            int counter_poly = 0;



            Program.MessageBox((IntPtr)0, "" + worldMatrix_instances_cubes.Count, "sc core systems", 0);
            Program.MessageBox((IntPtr)0, "" + _world_cube_list.Count, "sc core systems", 0);

            for (int c = 0; c < _world_cube_list.Count; c++)
            {
                //PHYSICS CUBES
                _world_cube_list[c]._WORLDMATRIXINSTANCES = worldMatrix_instances_cubes.ToArray(); // 
                _world_cube_list[c]._POSITION = Matrix.Identity;
                var cuber = _world_cube_list[c];
                var instancers = cuber.instances;
                var sometester = cuber._WORLDMATRIXINSTANCES;

                for (int i = 0; i < instancers.Length; i++)
                {
                    float xxx = sometester[i].M41;
                    float yyy = sometester[i].M42;
                    float zzz = sometester[i].M43;

                    cuber.instances[i].position.X = xxx;
                    cuber.instances[i].position.Y = yyy;
                    cuber.instances[i].position.Z = zzz;
                    cuber.instances[i].position.W = 1;

                    Quaternion quat_buffers;
                    Quaternion.RotationMatrix(ref sometester[i], out quat_buffers);

                    var dirInstance = sc_maths._newgetdirforward(quat_buffers);
                    cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                    cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                    cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                    cuber.instancesDataForward[i].rotation.W = 1;

                    dirInstance = -sc_maths._newgetdirleft(quat_buffers);
                    cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                    cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                    cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                    cuber.instancesDataRIGHT[i].rotation.W = 1;

                    dirInstance = sc_maths._newgetdirup(quat_buffers);
                    cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                    cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                    cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                    cuber.instancesDataUP[i].rotation.W = 1;
                }
                //END OF
            }


            counter_poly = 0;

            for (int i = 0; i < impulse.bodies.Count; i++)
            {
                Body body = impulse.bodies[i];

                if (body.shape.getType() == Shape.Type.Circle)
                {
                    //body.shape.gameObjectCirc.Render(device.ImmediateContext);
                    //ColorShader.Render(context, body.shape.gameObjectCirc.IndexCount, worldmatrix_circle, _viewMatrix, _projectionMatrix);
                }
                else if (body.shape.getType() == Shape.Type.Poly)
                {
                    _world_cube_list[counter_poly]._POSITION = Matrix.Identity;
                    _world_cube_list[counter_poly].Render(device.ImmediateContext);
                    shaderManager.RenderInstancedObject(context, _world_cube_list[counter_poly].IndexCount, _world_cube_list[counter_poly].InstanceCount, _world_cube_list[counter_poly]._POSITION, _viewMatrix, _projectionMatrix, null, _DLightBuffer_cube, _world_cube_list[counter_poly]); // oculusRiftDir
                    counter_poly++;
                }
            }

            ReadKeyboard();

            if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.NumberPadEnter))
            {
                /*var someAngle = 0;
                var circA = impulse.add(new Circle(0.5f), 0, 0);
                circA.position.set(0, 25);
                someAngle = 0;
                circA.setOrient((float)(Math.PI * someAngle / 180.0)); //(float)(0 * (180.0 / Math.PI))
                circA.setOriginOrient((float)(Math.PI * 0 / 180.0));
                circA.setLastOrient((float)(Math.PI * someAngle / 180.0));
                circA.lastAngle = someAngle;
                circA.dynamicFriction = 0.85f;
                circA.staticFriction = 0.85f;
                circA.restitution = 0.001f;
                circA.mass = 10;
                var ModelCirc = new DModelClassCircle();
                if (!ModelCirc.Initialize(device, 0.5f, 20, new Vector4(0.65f, 0.25f, 0.25f, 1), 0, 0, someAngle))
                {
                    return;
                }
                ModelCirc.position = new Vector2(circA.position.x, circA.position.y);
                circA.shape.gameObjectCirc = ModelCirc;
                //circA.shape.rigidInfo = new SC_RigidInfo();
                //circA.shape.rigidInfo.Model = ModelCirc;
                //circle_object_list.Add(ModelCirc);*/
            }














            if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Up))
            {
                viewerPos.Z += speed;
            }
            else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Down))
            {
                viewerPos.Z -= speed;
            }
            else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Q))
            {
                viewerPos.Y += speed;
            }
            else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Z))
            {
                viewerPos.Y -= speed;
            }
            else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Left))
            {
                viewerPos.X -= speed;
            }
            else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Right))
            {
                viewerPos.X += speed;
            }
            Camera.SetPosition(viewerPos.X, viewerPos.Y, viewerPos.Z);
            lastTheTimer = timeStopWatch.Elapsed.Milliseconds;
        }

        void UpdateRigidBodies(float theTime)
        {
            accumulator += impulse.dt;// Time.deltaTime;// state.seconds;

            if (accumulator >= impulse.dt)
            {
                impulse.step();
                accumulator -= impulse.dt;
            }

            int counter_poly = 0;

            for (int i = 0; i < impulse.bodies.Count; i++)
            {
                Body body = impulse.bodies[i];

                if (body.shape.getType() == Shape.Type.Circle)
                {
                    /*if (body.shape.body.isStatic == 0)
                    {
                        body.shape.gameObjectCirc.position = new Vector2(body.position.x, body.position.y);

                        float rx = (float)Math.Cos(body.orient) * 2;
                        float ry = (float)Math.Sin(body.orient) * 2;
                        Vector2 dirToPoint = new Vector2(body.shape.gameObjectCirc.position.X + rx, body.shape.gameObjectCirc.position.Y + ry) - new Vector2(body.shape.gameObjectCirc.position.X, body.shape.gameObjectCirc.position.Y);
                        dirToPoint.Normalize();

                        Vector2 orientPos = new Vector2(body.shape.gameObjectCirc.position.X + rx, body.shape.gameObjectCirc.position.Y + ry);

                        rx = (float)Math.Cos(body.originOrient) * 2;
                        ry = (float)Math.Sin(body.originOrient) * 2;

                        Vector2 originDir = new Vector2(body.shape.gameObjectCirc.position.X + rx, body.shape.gameObjectCirc.position.Y + ry) - new Vector2(body.shape.gameObjectCirc.position.X, body.shape.gameObjectCirc.position.Y);
                        originDir.Normalize();

                        rx = (float)Math.Cos(body.lastOrient) * 2;
                        ry = (float)Math.Sin(body.lastOrient) * 2;

                        Vector2 lastOrient = new Vector2(body.shape.gameObjectCirc.position.X + rx, body.shape.gameObjectCirc.position.Y + ry);

                        Vector3 a_normalized = new Vector3(dirToPoint.X, dirToPoint.Y, 0);
                        Vector3 b_normalized = new Vector3(originDir.X, originDir.Y, 0);
                        Vector2 a_normalized2 = new Vector2(dirToPoint.X, dirToPoint.Y);
                        Vector2 b_normalized2 = new Vector2(originDir.X, originDir.Y);

                        float angleNorm = -(float)AngleBetween(a_normalized2, b_normalized2);

                        var angleDiff = angleNorm - body.shape.gameObjectCirc.OriginRotationZ;

                        float pitch = (body.shape.gameObjectCirc.OriginRotationX) * 0.0174532925f;
                        float yaw = (body.shape.gameObjectCirc.OriginRotationY) * 0.0174532925f;
                        float roll = (body.shape.gameObjectCirc.OriginRotationZ) * 0.0174532925f;
                        Matrix currentMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                        float pitcher = (body.shape.gameObjectCirc.CurrentRotationX) * 0.0174532925f;
                        float yawer = (body.shape.gameObjectCirc.CurrentRotationY) * 0.0174532925f;
                        float roller = (angleDiff) * 0.0174532925f;
                        Matrix currentMatrixer = SharpDX.Matrix.RotationYawPitchRoll(yawer, pitcher, roller);

                        Matrix matroxer2 = Matrix.Multiply(currentMatrix, currentMatrixer);


                        matroxer2.M41 = body.shape.gameObjectCirc.position.X;
                        matroxer2.M42 = body.shape.gameObjectCirc.position.Y;

                        body.shape.gameObjectCirc.originRot = matroxer2;

                        Matrix worldMatrix = Matrix.Identity;
                        worldmatrix_circle = body.shape.gameObjectCirc.originRot;                        
                    }*/
                }
                else if (body.shape.getType() == Shape.Type.Poly)
                {/*
                    body.shape.gameObject.position = new Vector2(body.position.x, body.position.y);

                    float rx = (float)Math.Cos(body.orient) * 2;
                    float ry = (float)Math.Sin(body.orient) * 2;
                    Vector2 dirToPoint = new Vector2(body.shape.gameObject.position.X + rx, body.shape.gameObject.position.Y + ry) - new Vector2(body.shape.gameObject.position.X, body.shape.gameObject.position.Y);
                    dirToPoint.Normalize();

                    Vector2 orientPos = new Vector2(body.shape.gameObject.position.X + rx, body.shape.gameObject.position.Y + ry);

                    rx = (float)Math.Cos(body.originOrient) * 2;
                    ry = (float)Math.Sin(body.originOrient) * 2;

                    Vector2 originDir = new Vector2(body.shape.gameObject.position.X + rx, body.shape.gameObject.position.Y + ry) - new Vector2(body.shape.gameObject.position.X, body.shape.gameObject.position.Y);
                    originDir.Normalize();
             
                    rx = (float)Math.Cos(body.lastOrient) * 2;
                    ry = (float)Math.Sin(body.lastOrient) * 2;

                    Vector2 lastOrient = new Vector2(body.shape.gameObject.position.X + rx, body.shape.gameObject.position.Y + ry);

                    Vector3 a_normalized = new Vector3(dirToPoint.X, dirToPoint.Y,0);
                    Vector3 b_normalized = new Vector3(originDir.X, originDir.Y, 0);
                    Vector2 a_normalized2 = new Vector2(dirToPoint.X, dirToPoint.Y);
                    Vector2 b_normalized2 = new Vector2(originDir.X, originDir.Y);

                    float angleNorm = -(float)AngleBetween(a_normalized2, b_normalized2);

                    var angleDiff = angleNorm - body.shape.gameObject.OriginRotationZ;

                    float pitch = (body.shape.gameObject.OriginRotationX) * 0.0174532925f;
                    float yaw = (body.shape.gameObject.OriginRotationY) * 0.0174532925f;
                    float roll = (body.shape.gameObject.OriginRotationZ) * 0.0174532925f;
                    Matrix currentMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                    float pitcher = (body.shape.gameObject.CurrentRotationX) * 0.0174532925f;
                    float yawer = (body.shape.gameObject.CurrentRotationY) * 0.0174532925f;
                    float roller = (angleDiff) * 0.0174532925f;
                    Matrix currentMatrixer = SharpDX.Matrix.RotationYawPitchRoll(yawer, pitcher, roller);

                    Matrix matroxer2 = Matrix.Multiply(currentMatrix, currentMatrixer);


                    matroxer2.M41 = body.shape.gameObject.position.X;
                    matroxer2.M42 = body.shape.gameObject.position.Y;

                    body.shape.gameObject.originRot = matroxer2;

                    body.setLastOrient(body.orient);
                    body.lastAngle = angleNorm;

                    Matrix worldMatrix = Matrix.Identity;

                    worldMatrix_instances_cubes[counter_poly] = matroxer2;
                    counter_poly++;*/
                }
            }
        }

        KeyboardState _KeyboardState;
        private bool ReadKeyboard()
        {
            var resultCode = SharpDX.DirectInput.ResultCode.Ok;
            _KeyboardState = new KeyboardState();

            try
            {
                // Read the keyboard device.
                _Keyboard.GetCurrentState(ref _KeyboardState);
            }
            catch (SharpDX.SharpDXException ex)
            {
                resultCode = ex.Descriptor; // ex.ResultCode;
            }
            catch (Exception)
            {
                return false;
            }

            // If the mouse lost focus or was not acquired then try to get control back.
            if (resultCode == SharpDX.DirectInput.ResultCode.InputLost || resultCode == SharpDX.DirectInput.ResultCode.NotAcquired)
            {
                try
                {
                    _Keyboard.Acquire();
                }
                catch
                { }

                return true;
            }

            if (resultCode == SharpDX.DirectInput.ResultCode.Ok)
                return true;

            return false;
        }


        public static float signedAngle(Vector2 a, Vector2 b)
        {
            return (float)(Math.Atan2(b.Y - a.Y, b.X - a.X) * (180 / Math.PI));
        }


        public static double AngleBetween(Vector2 vector1, Vector2 vector2)
        {
            double sin = vector1.X * vector2.Y - vector2.X * vector1.Y;
            double cos = vector1.X * vector2.X + vector1.Y * vector2.Y;

            return Math.Atan2(sin, cos) * (180 / Math.PI);
        }

        public double GetSignedAngleBetweenTwoVectors(Vector2 Source, Vector2 Dest, Vector2 DestsRight)
        {
            // We make sure all of our vectors are unit length 
            Source.Normalize();
            Dest.Normalize();
            DestsRight.Normalize();

            //float forwardDot = Vector3.Dot(Source, Dest);
            //float rightDot = Vector3.Dot(Source, DestsRight);

            float forwardDot = sc_maths.Dot(Source.X, Source.Y, Dest.X, Dest.Y);
            float rightDot = sc_maths.Dot(Source.X, Source.Y, DestsRight.X, DestsRight.Y);



            // Make sure we stay in range no matter what, so Acos doesn't fail later 
            forwardDot = sc_maths.Clamp(forwardDot, -1.0f, 1.0f);

            double angleBetween = Math.Acos((float)forwardDot);

            if (rightDot < 0.0f)
                angleBetween *= -1.0f;

            return angleBetween;
        }
    }
}

/*Func<bool> formatDelegate = () =>
          {
              return true;
          };
          var t2 = new Task<bool>(formatDelegate);
          t2.RunSynchronously();
          t2.Dispose();*/
