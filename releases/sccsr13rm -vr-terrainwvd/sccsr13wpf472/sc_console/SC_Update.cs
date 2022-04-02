using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

//using Ab3d.DXEngine;
using Ab3d.OculusWrap;
//using Ab3d.DXEngine.OculusWrap;
using Ab3d.OculusWrap.DemoDX11;

using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using SharpDX.DirectInput;

using SCCoreSystems.SC_Graphics;
using SCCoreSystems.SC_Graphics.SC_ShaderManager;

using Jitter;
using Jitter.Dynamics;
using Jitter.Collision;
using Jitter.LinearMath;
using Jitter.Collision.Shapes;
using Jitter.Forces;

using System.Collections.Generic;
using System.Collections;
using System.Runtime;
using System.Runtime.CompilerServices;

using System.ComponentModel;
using SharpDX.D3DCompiler;

using scmessageobject = SCCoreSystems.scmessageobject.scmessageobject;
using scmessageobjectjitter = SCCoreSystems.scmessageobject.scmessageobjectjitter;

using ISCCS_Jitter_Interface = Jitter.ISCCS_Jitter_Interface;
using Jitter;

using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;


using scgraphicssecpackage = SCCoreSystems.scmessageobject.scgraphicssecpackage;

//using Steamworks;

namespace SCCoreSystems.sc_console
{
    public class SC_Update : SC_console_directx
    {
        public static Vector4 dirikvoxelbodyInstanceRight0;
        public static Vector4 dirikvoxelbodyInstanceUp0;
        public static Vector4 dirikvoxelbodyInstanceForward0;




        int eyebufferthreadswtc = 0;


        public static Matrix tempmatter;
        //sc_graphics_sec graphicssec;


        public static Matrix oriProjectionMatrix;
        public static int[] arduinoDIYOculusTouchArrayOfData = new int[12];


        public scgraphicssecpackage scgraphicssecpackagemessage;

        public SC_Update()
        {
            scgraphicssecpackagemessage = new scgraphicssecpackage();

        }

        public static Matrix hmdmatrixRot = Matrix.Identity;

        BackgroundWorker BackgroundWorker_00;

        protected override void ShutDownGraphics()
        {
            if (BackgroundWorker_00 != null)
            {
                BackgroundWorker_00.Dispose();
            }
            /*if (_Keyboard != null)
            {
                _Keyboard.Dispose();
            }
            if (_KeyboardState != null)
            {
                _KeyboardState = null;
            }*/

            if (_desktopFrame.ShaderResource != null)
            {
                _desktopFrame.ShaderResource = null;
            }

            if (_desktopDupe._lastShaderResourceView != null)
            {
                _desktopDupe._lastShaderResourceView = null;
            }
            if (_desktopDupe != null)
            {
                _desktopDupe = null;
            }
        }


        /*protected override void SC_Init_DirectX()
        {

            base.SC_Init_DirectX();
        }*/
        /*public override scmessageobjectjitter[][] sc_write_to_buffer(scmessageobjectjitter[][] _sc_jitter_tasks)
        {
            return _sc_jitter_tasks;
            //base.sc_write_to_buffer(_sc_jitter_tasks);
        }

        public override scmessageobjectjitter[][] loop_worlds(scmessageobjectjitter[][] _sc_jitter_tasks)
        {
            return _sc_jitter_tasks;
            //base.sc_write_to_buffer(_sc_jitter_tasks);
        }
        public override scmessageobjectjitter[][] workOnSomething(scmessageobjectjitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft)
        {
            return _sc_jitter_tasks;
            //base.sc_write_to_buffer(_sc_jitter_tasks);
        }
        public override scmessageobjectjitter[][] _sc_create_world_objects(scmessageobjectjitter[][] _sc_jitter_tasks)
        {
            return _sc_jitter_tasks;
            //base.sc_write_to_buffer(_sc_jitter_tasks);
        }*/

        public static SharpDX.Vector3 originPos = new SharpDX.Vector3(0, 1, 0); //new SharpDX.Vector3(-10, 1, 10);
        public static SharpDX.Vector3 originPosScreen = new SharpDX.Vector3(0, 0, 0);

        float disco_sphere_rot_speed = 0.5f;

        float speedRot = 0.1f; //1.95f // 0.25f
        float speedRotArduino = 0.000001f;

        float speedPos = 0.025f; //0.025f // 1.5f
        float speedPosArduino = 0.001f;
        public static Matrix hmd_matrix;
        Matrix hmd_matrix_test;


        int _start_background_worker_01 = 0;
        sc_graphics_main _the_graphics;

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

        public void DoWork(int timeOut) //async Task
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
                        timeStopWatch00.Start();
                        time1 = DateTime.Now;
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
            _delta_timer_frame = (float)Math.Abs((timeStopWatch01.Elapsed.Ticks - timeStopWatch00.Elapsed.Ticks)); //10000000000f

            time2 = DateTime.Now;
            _delta_timer_time = (time2.Ticks - time1.Ticks); //100000000f //10000000000f
            time1 = time2;

            deltaTime = (float)Math.Abs(_delta_timer_time - _delta_timer_frame);

            //time1 = time2;
            //await Task.Delay(1);
            //Thread.Sleep(timeOut);

            _swtch_counter_00++;
            _swtch_counter_01++;
            _swtch_counter_02++;

            goto _threadLoop;
        }

        /*public SC_Console_GRAPHICS(sc_graphics_main _graphics)
        {
            _the_graphics = _graphics;
        }*/

        public void ShutDown()
        {

        }

        Matrix rotatingMatrixForGrabber = Matrix.Identity;
        int _sec_logic_swtch_grab = 0;
        int _swtch_hasRotated = 0;
        int _has_grabbed_right_swtch = 0;
        public static double RotationY = 0;//{ get; set; } { get; set; }
        public static double RotationX = 0;//{ get; set; } { get; set; }
        public static double RotationZ = 0;//{ get; set; } { get; set; }

        public static float RotationOriginY = 0;//{ get; set; }
        public static float RotationOriginX = 0;//{ get; set; }
        public static float RotationOriginZ = 0;//{ get; set; }

        float thumbstickIsRight;
        float thumbstickIsUp;

        int RotationGrabbedSwtch = 0;

        public static double RotationY4Pelvis;
        public static double RotationX4Pelvis;
        public static double RotationZ4Pelvis;

        public static double RotationY4PelvisTwo;
        public static double RotationX4PelvisTwo;
        public static double RotationZ4PelvisTwo;


        public static double RotationGrabbedYOff;
        public static double RotationGrabbedXOff;
        public static double RotationGrabbedZOff;


        public static double RotationGrabbedY;
        public static double RotationGrabbedX;
        public static double RotationGrabbedZ;

        //OCULUS TOUCH SETTINGS 
        Ab3d.OculusWrap.Result resultsRight;
        uint buttonPressedOculusTouchRight;
        Vector2f[] thumbStickRight;
        public static float[] handTriggerRight;
        float[] indexTriggerRight;
        Ab3d.OculusWrap.Result resultsLeft;
        uint buttonPressedOculusTouchLeft;
        Vector2f[] thumbStickLeft;
        public static float[] handTriggerLeft;
        public static float[] indexTriggerLeft;
        public static Posef handPoseLeft;
        SharpDX.Quaternion _leftTouchQuat;
        public static Posef handPoseRight;
        SharpDX.Quaternion _rightTouchQuat;
        public static Matrix _leftTouchMatrix = Matrix.Identity;
        public static Matrix _rightTouchMatrix = Matrix.Identity;
        //OCULUS TOUCH SETTINGS 

        double displayMidpoint;
        TrackingState trackingState;
        Posef[] eyePoses;

        public static Vector3 viewpositionorigin;

        EyeType eye;
        EyeTexture eyeTexture;
        bool latencyMark = false;
        TrackingState trackState;
        PoseStatef poseStatefer;
        Posef hmdPose;
        Quaternionf hmdRot;
        public static Vector3 _hmdPoser;
        Quaternion _hmdRoter;

        _sc_camera Camera;

        int _failed_screen_capture = 0;
        public static SC_ShaderManager _shaderManager;
        public int _can_work_physics_objects = 0;

        public static IntPtr HWND;
        SCCoreSystems.sc_core.sc_system_configuration _configuration;
        sc_console.sc_console_writer _currentWriter;
        public static sccsscreenframe _desktopFrame;

        int textureIndex;
        SharpDX.Matrix finalRotationMatrix;
        Vector3 lookUp;
        Vector3 lookAt;
        public static Vector3 viewPosition;
        public static Matrix viewMatrix;
        public static Matrix _projectionMatrix;
        public static Vector3 OFFSETPOS;

        public static SharpDX.Vector3 movePos = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Matrix originRot = SharpDX.Matrix.Identity;

        public static SharpDX.Matrix rotatingMatrixForPelvis = SharpDX.Matrix.Identity;
        public static SharpDX.Matrix rotatingMatrix = SharpDX.Matrix.Identity;
        float r = 0;
        float g = 0;
        float b = 0;
        float a = 1;

        Matrix WorldMatrix = Matrix.Identity;
        public static sccssharpdxscreencapture _desktopDupe;

        public static DateTime startTime;

        protected override scmessageobjectjitter[][] init_update_variables(scmessageobjectjitter[][] _sc_jitter_tasks, SCCoreSystems.sc_core.sc_system_configuration configuration, IntPtr hwnd, sc_console.sc_console_writer _writer)
        {
            try
            {

                float pitch = (float)(Math.PI * (RotationOriginX) / 180.0f);
                float yaw = (float)(Math.PI * (RotationOriginY) / 180.0f);
                float roll = (float)(Math.PI * (RotationOriginZ) / 180.0f);

                originRot = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);



                startTime = DateTime.Now;


                HWND = hwnd;

                _configuration = configuration;

                _currentWriter = _writer;


                //ReadKeyboard();
                Camera = new _sc_camera();



                if (!SC_console_directx._useOculusRift)
                {
                    originPos.Y += 2;
                    originPos.Z -= 2f;


                    Camera.SetPosition(originPos.X, originPos.Y, originPos.Z);

                    Camera.SetRotation(0, 180, 0);
                }

                //swtch_for_last_pos = new int[MainWindow._physics_engine_instance_x * MainWindow._physics_engine_instance_y * MainWindow._physics_engine_instance_z][];

                //RAYCAST STUFF
                //_some_reset_for_applying_force = new int[MainWindow._physics_engine_instance_x * MainWindow._physics_engine_instance_y * MainWindow._physics_engine_instance_z][];
                //_some_last_frame_vector = new JVector[MainWindow._physics_engine_instance_x * MainWindow._physics_engine_instance_y * MainWindow._physics_engine_instance_z][][];
                //_some_last_frame_rigibodies = new RigidBody[MainWindow._physics_engine_instance_x * MainWindow._physics_engine_instance_y * MainWindow._physics_engine_instance_z][][];

         

                if (MainWindow.usejitterphysics == 1)
                {


                    Thread main_thread_update = new Thread(() =>
                    {

                    _thread_looper:

                        try
                        {
                            DoWork(1);
                        }
                        catch (Exception ex)
                        {

                        }
                        Thread.Sleep(1);
                        goto _thread_looper;

                        //ShutDown();
                        //ShutDownGraphics();

                    }, 0);

                    main_thread_update.IsBackground = true;
                    main_thread_update.SetApartmentState(ApartmentState.STA);
                    main_thread_update.Start();

                }







                Camera = new _sc_camera();

                _shaderManager = new SC_ShaderManager();
                _shaderManager.Initialize(D3D.Device, HWND);

                _desktopDupe = new sccssharpdxscreencapture(0, 0, D3D.device);


                var directInput = new DirectInput();

                _Keyboard = new SharpDX.DirectInput.Keyboard(directInput);

                _Keyboard.Properties.BufferSize = 128;
                _Keyboard.Acquire();


                /*sc_graphics_sec graphicssec;
                graphicssec = new sc_graphics_sec();

                
                _sc_jitter_tasks = graphicssec._sc_create_world_objects(_sc_jitter_tasks);*/

                _sc_jitter_tasks[0][0].hasinit = 0;
            }
            catch
            {

            }
            return _sc_jitter_tasks;
        }

        public static SharpDX.DirectInput.Keyboard _Keyboard;


        protected override scmessageobjectjitter[][] Update(jitter_sc[] jitter_sc, scmessageobjectjitter[][] _sc_jitter_tasks)
        {
            if (_shaderManager != null)
            {
                //MessageBox((IntPtr)0, "_shaderManager !=  null SC_update.cs", "sc core systems message", 0);
                // Render the graphics scene.
                try
                {
                    _sc_jitter_tasks = _FrameVRTWO(jitter_sc, _sc_jitter_tasks);
                }
                catch (Exception ex)
                {
                    MainWindow.MessageBox((IntPtr)0, "" + ex.ToString(), "sc core systems message", 0);
                }
            }
            else
            {
                //MessageBox((IntPtr)0, "_shaderManager == null SC_update.cs", "sc core systems message", 0);
            }
            return _sc_jitter_tasks;
        }

        int _last_frame_init = 0;


        /*private void OnGameOverlayActivated(GameOverlayActivated_t pCallback)
        {
            if (pCallback.m_bActive != 0)
            {
                //Debug.Log("Steam Overlay has been activated");
                MessageBox((IntPtr)0, "Steam Overlay has been activated", "", 0);
            }
            else
            {
                //Debug.Log("Steam Overlay has been closed");
                MessageBox((IntPtr)0, "Steam Overlay has been closed", "", 0);
            }
        }*/


        //protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;
        int startsomeswtc = 0;

        private unsafe scmessageobjectjitter[][] _FrameVRTWO(jitter_sc[] jitter_sc, scmessageobjectjitter[][] _sc_jitter_tasks)
        {
            /*if (MainWindow.somesteammanager != null && startsomeswtc == 0)
            {

                MainWindow.somesteammanager.someinit();

                startsomeswtc = 1;
                /*if (SteamManager.Initialized && startsomeswtc == 0)
                {
                    m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
                    startsomeswtc = 1;
                }
            }*/
            



            if (SC_console_directx._useOculusRift)
            {
                //HEADSET POSITION
                displayMidpoint = D3D.OVR.GetPredictedDisplayTime(D3D.sessionPtr, 0);
                trackingState = D3D.OVR.GetTrackingState(D3D.sessionPtr, displayMidpoint, true);
                latencyMark = false;
                trackState = D3D.OVR.GetTrackingState(D3D.sessionPtr, 0.0f, latencyMark);
                poseStatefer = trackState.HeadPose;
                hmdPose = poseStatefer.ThePose;
                hmdRot = hmdPose.Orientation;
                _hmdPoser = new Vector3(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);
                _hmdRoter = new Quaternion(hmdPose.Orientation.X, hmdPose.Orientation.Y, hmdPose.Orientation.Z, hmdPose.Orientation.W);

                //SET CAMERA POSITION
                Camera.SetPosition(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);
                Quaternion quatter = new Quaternion(hmdRot.X, hmdRot.Y, hmdRot.Z, hmdRot.W);
                Vector3 oculusRiftDir = sc_maths._getDirection(Vector3.ForwardRH, quatter);


                Matrix.RotationQuaternion(ref quatter, out hmd_matrix);

                Matrix.RotationQuaternion(ref quatter, out hmd_matrix_test);

                hmd_matrix_test = hmd_matrix_test * finalRotationMatrix;

                //TOUCH CONTROLLER RIGHT
                resultsRight = D3D.OVR.GetInputState(D3D.sessionPtr, D3D.controllerTypeRTouch, ref D3D.inputStateRTouch);

                buttonPressedOculusTouchRight = D3D.inputStateRTouch.Buttons;

                /*if (buttonPressedOculusTouchRight != 0) //4 thumbstick button
                {
                    MainWindow.MessageBox((IntPtr)0, "" + buttonPressedOculusTouchRight, "sccs message", 0);
                }*/


                thumbStickRight = D3D.inputStateRTouch.Thumbstick;
                handTriggerRight = D3D.inputStateRTouch.HandTrigger;
                indexTriggerRight = D3D.inputStateRTouch.IndexTrigger;
                handPoseRight = trackingState.HandPoses[1].ThePose;

                _rightTouchQuat.X = handPoseRight.Orientation.X;
                _rightTouchQuat.Y = handPoseRight.Orientation.Y;
                _rightTouchQuat.Z = handPoseRight.Orientation.Z;
                _rightTouchQuat.W = handPoseRight.Orientation.W;

                _rightTouchQuat = new SharpDX.Quaternion(handPoseRight.Orientation.X, handPoseRight.Orientation.Y, handPoseRight.Orientation.Z, handPoseRight.Orientation.W);
                SharpDX.Matrix.RotationQuaternion(ref _rightTouchQuat, out _rightTouchMatrix);

                _rightTouchMatrix.M41 = handPoseRight.Position.X + originPos.X + movePos.X;
                _rightTouchMatrix.M42 = handPoseRight.Position.Y + originPos.Y + movePos.Y;
                _rightTouchMatrix.M43 = handPoseRight.Position.Z + originPos.Z + movePos.Z;

                //TOUCH CONTROLLER LEFT
                resultsLeft = D3D.OVR.GetInputState(D3D.sessionPtr, D3D.controllerTypeLTouch, ref D3D.inputStateLTouch);
                buttonPressedOculusTouchLeft = D3D.inputStateLTouch.Buttons;

                /*if (buttonPressedOculusTouchLeft != 0) //1024 thumbstick button
                {
                    MainWindow.MessageBox((IntPtr)0, "" + buttonPressedOculusTouchLeft, "sccs message", 0);
                }*/





                thumbStickLeft = D3D.inputStateLTouch.Thumbstick;
                handTriggerLeft = D3D.inputStateLTouch.HandTrigger;
                indexTriggerLeft = D3D.inputStateLTouch.IndexTrigger;
                handPoseLeft = trackingState.HandPoses[0].ThePose;

                _leftTouchQuat.X = handPoseLeft.Orientation.X;
                _leftTouchQuat.Y = handPoseLeft.Orientation.Y;
                _leftTouchQuat.Z = handPoseLeft.Orientation.Z;
                _leftTouchQuat.W = handPoseLeft.Orientation.W;

                _leftTouchQuat = new SharpDX.Quaternion(handPoseLeft.Orientation.X, handPoseLeft.Orientation.Y, handPoseLeft.Orientation.Z, handPoseLeft.Orientation.W);

                SharpDX.Matrix.RotationQuaternion(ref _leftTouchQuat, out _leftTouchMatrix);
                //_other_left_touch_matrix = _leftTouchMatrix;
                //_other_left_touch_matrix.M41 = handPoseLeft.Position.X;
                //_other_left_touch_matrix.M42 = handPoseLeft.Position.Y;
                //_other_left_touch_matrix.M43 = handPoseLeft.Position.Z;

                _leftTouchMatrix.M41 = handPoseLeft.Position.X + originPos.X + movePos.X;
                _leftTouchMatrix.M42 = handPoseLeft.Position.Y + originPos.Y + movePos.Y;
                _leftTouchMatrix.M43 = handPoseLeft.Position.Z + originPos.Z + movePos.Z;

            }
            else
            {
                Camera.Render();
                ReadKeyboard();

                float speed = 0.015f;
                float speedRot = 0.1f;

                float rotY = 0;

                if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Up))
                {
                    movePos.Z += speed;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Down))
                {
                    movePos.Z -= speed;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Q))
                {
                    movePos.Y += speed;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Z))
                {
                    movePos.Y -= speed;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Left))
                {
                    movePos.X -= speed;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Right))
                {
                    movePos.X += speed;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.A))
                {
                    rotY -= speedRot;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.D))
                {
                    rotY += speedRot;
                }




                var somerot = Camera.GetRotation();

                Camera.SetRotation(somerot.X, somerot.Y + rotY, somerot.Z);



                OFFSETPOS = originPos + movePos;

                Camera.SetPosition(OFFSETPOS.X, OFFSETPOS.Y, OFFSETPOS.Z);

            }



            for (int i = 0; i < 3;)
            {
                _failed_screen_capture = 0;
                try
                {
                    _desktopFrame = _desktopDupe.ScreenCapture(0);
                }
                catch (Exception ex)
                {
                    _desktopDupe = new sccssharpdxscreencapture(0, 0, D3D.device);
                    _failed_screen_capture = 1;
                }
                i++;
                if (_failed_screen_capture == 0)
                {
                    break;
                }
            }
































            //START OF PHYSICS ENGINE STEPS
            if (_start_background_worker_01 == 0)
            {
                if (_can_work_physics == 1)
                {


                    if (MainWindow.usejitterphysics == 0)
                    {

                    }
                    else if (MainWindow.usejitterphysics == 1)
                    {
                        //JITTER PHYSICS ENGINE STEP
                        var main_thread_update = new Thread(() =>
                        {
                            try
                            {
                                //MainWindow.MessageBox((IntPtr)0, "threadstart succes", "sc core systems message", 0);
                                Stopwatch _this_thread_ticks_01 = new Stopwatch();
                                _this_thread_ticks_01.Start();

                            _thread_looper:

                                for (int xx = 0; xx < MainWindow._physics_engine_instance_x; xx++)
                                {
                                    for (int yy = 0; yy < MainWindow._physics_engine_instance_y; yy++)
                                    {
                                        for (int zz = 0; zz < MainWindow._physics_engine_instance_z; zz++)
                                        {
                                            var indexer0 = xx + MainWindow._physics_engine_instance_x * (yy + MainWindow._physics_engine_instance_y * zz);

                                            try
                                            {
                                                for (int x = 0; x < MainWindow.world_width; x++)
                                                {
                                                    for (int y = 0; y < MainWindow.world_height; y++)
                                                    {
                                                        for (int z = 0; z < MainWindow.world_depth; z++)
                                                        {
                                                            var indexer1 = x + MainWindow.world_width * (y + MainWindow.world_height * z);
                                                            object _some_data_00 = (object)_sc_jitter_tasks[indexer0][indexer1]._world_data[0];
                                                            World _jitter_world = (World)_some_data_00;

                                                            if (_jitter_world != null)
                                                            {
                                                                deltaTime = timeStopWatch00.Elapsed.Seconds * 2;

                                                                if (deltaTime > 1.0f * 0.01f)
                                                                {
                                                                    deltaTime = 1.0f * 0.01f;
                                                                }

                                                                _jitter_world.Step(deltaTime, true);
                                                            }

                                                        }
                                                    }
                                                }
                                            }
                                            catch
                                            {

                                            }
                                        }
                                    }
                                }
                                //MessageBox((IntPtr)0, _this_thread_ticks.ElapsedTicks + "", "sc core systems message", 0);
                                //Console.WriteLine("ticks: " + _this_thread_ticks.ElapsedTicks);
                                Thread.Sleep(1);
                                goto _thread_looper;
                            }
                            catch (Exception ex)
                            {

                            }

                        }, 0);

                        //main_thread_update.IsBackground = true;
                        //main_thread_update.SetApartmentState(ApartmentState.STA);
                        //main_thread_update.Start();


                        main_thread_update.IsBackground = true;
                        main_thread_update.Priority = ThreadPriority.Lowest;
                        main_thread_update.SetApartmentState(ApartmentState.STA);
                        main_thread_update.Start();


                        /*BackgroundWorker_00 = new BackgroundWorker();
                        BackgroundWorker_00.DoWork += (object sender, DoWorkEventArgs argers) =>
                        {
                            //MainWindow.MessageBox((IntPtr)0, "threadstart succes", "sc core systems message", 0);
                            Stopwatch _this_thread_ticks_01 = new Stopwatch();
                            _this_thread_ticks_01.Start();

                        _thread_looper:

                            _sc_jitter_tasks = graphicssec.loop_worlds(_sc_jitter_tasks, originRot, rotatingMatrix, hmdmatrixRot, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix);

                            //_ticks_watch.Restart();

                            //Console.WriteLine(_ticks_watch.Elapsed.Ticks);
                            Thread.Sleep(1);
                            goto _thread_looper;
                        };
                        BackgroundWorker_00.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs argers)
                        {

                        };

                        BackgroundWorker_00.RunWorkerAsync();*/
                        //JITTER PHYSICS ENGINE STEP
                    }



                    _start_background_worker_01 = 1;
                }
            }
            //END OF PHYSICS ENGINE STEPS


            if (SC_console_directx._useOculusRift)
            {

                //MainWindow.MessageBox((IntPtr)0, "000", "sc core systems message", 0);
                try
                {
                    if (_can_work_physics == 1)
                    {
                        /*
                        if (graphicssec != null)
                        {
                            graphicssec.oculuscontrolsNRecordSoundNMousePointer();
                        }*/





                        if (MainWindow.useArduinoOVRTouchKeymapper == 0)
                        {
                            //if (_out_of_bounds_oculus_rift == 1)
                            {
                                if (thumbStickRight[1].X < 0 || thumbStickRight[1].X > 0 || thumbStickRight[1].Y < 0 || thumbStickRight[1].Y > 0)
                                {
                                    if (thumbStickRight[1].X < 0 && thumbStickRight[1].Y < 0 || thumbStickRight[1].X < 0 && thumbStickRight[1].Y > 0)
                                    {
                                        RotationGrabbedYOff = 0;
                                        RotationGrabbedXOff = 0;
                                        RotationGrabbedZOff = 0;

                                        RotationGrabbedSwtch = 1;

                                        thumbstickIsRight = thumbStickRight[1].X;
                                        thumbstickIsUp = thumbStickRight[1].Y;
                                        //newRotationY;

                                        float rotMax = 25;

                                        float rot0 = (float)((180 / Math.PI) * (Math.Atan(thumbstickIsUp / thumbstickIsRight))); // opp/adj
                                        float rot1 = (float)((180 / Math.PI) * (Math.Atan(thumbstickIsRight / thumbstickIsUp)));

                                        float newRotY = thumbstickIsRight * (rotMax) * -1;

                                        RotationY = newRotY;
                                        float someRotForPelvis = (float)RotationY;

                                        if (RotationY > rotMax * 0.99f)
                                        {
                                            RotationY = rotMax * 0.99f;
                                            RotationY4Pelvis += speedRot * 10;
                                            RotationY4PelvisTwo += speedRot * 10;
                                            RotationGrabbedY += speedRot * 10;
                                        }

                                        rotMax = 25;
                                        float newRotX = thumbstickIsUp * (rotMax) * -1;
                                        RotationX = newRotX;

                                        if (RotationX > rotMax * 0.99f)
                                        {
                                            RotationX = rotMax * 0.99f;
                                        }

                                        //float pitch = (float)(RotationX * 0.0174532925f);
                                        //float yaw = (float)(RotationY * 0.0174532925f);
                                        //float roll = (float)(RotationZ * 0.0174532925f);

                                        float pitch = (float)(Math.PI * (RotationX) / 180.0f);
                                        float yaw = (float)(Math.PI * (RotationY) / 180.0f);
                                        float roll = (float)(Math.PI * (RotationZ) / 180.0f);

                                        rotatingMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                                        //pitch = (float)(RotationX4Pelvis * 0.0174532925f);
                                        //yaw = (float)(RotationY4Pelvis * 0.0174532925f);
                                        //roll = (float)(RotationZ4Pelvis * 0.0174532925f);


                                        pitch = (float)(Math.PI * (RotationX4Pelvis) / 180.0f);
                                        yaw = (float)(Math.PI * (RotationY4Pelvis) / 180.0f);
                                        roll = (float)(Math.PI * (RotationZ4Pelvis) / 180.0f);


                                        rotatingMatrixForPelvis = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                                        if (_has_grabbed_right_swtch == 2)
                                        {
                                            _swtch_hasRotated = 1;
                                        }

                                        pitch = (float)(Math.PI * (RotationGrabbedX) / 180.0f);
                                        yaw = (float)(Math.PI * (RotationGrabbedY) / 180.0f);
                                        roll = (float)(Math.PI * (RotationGrabbedZ) / 180.0f);


                                        rotatingMatrixForGrabber = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);
                                    }
                                    if (thumbStickRight[1].X > 0 && thumbStickRight[1].Y < 0 || thumbStickRight[1].X > 0 && thumbStickRight[1].Y > 0)
                                    {
                                        RotationGrabbedYOff = 0;
                                        RotationGrabbedXOff = 0;
                                        RotationGrabbedZOff = 0;


                                        RotationGrabbedSwtch = 1;

                                        thumbstickIsRight = thumbStickRight[1].X;
                                        thumbstickIsUp = thumbStickRight[1].Y;

                                        float rotMax = 25;

                                        //for calculations
                                        float rot0 = (float)((180 / Math.PI) * (Math.Atan(thumbstickIsUp / thumbstickIsRight)));
                                        float rot1 = (float)((180 / Math.PI) * (Math.Atan(thumbstickIsRight / thumbstickIsUp)));

                                        if (rot0 > 0)
                                        {
                                            rot0 *= -1;
                                        }

                                        float newRotY = thumbstickIsRight * (-rotMax);

                                        RotationY = newRotY;
                                        float someRotForPelvis = (float)RotationY;

                                        if (RotationY < -rotMax * 0.99f)
                                        {
                                            RotationY = -rotMax * 0.99f;
                                            RotationY4Pelvis -= speedRot * 10;
                                            RotationY4PelvisTwo -= speedRot * 10;
                                            RotationGrabbedY -= speedRot * 10;
                                        }

                                        rotMax = 25;
                                        float newRotX = thumbstickIsUp * (rotMax) * -1;
                                        RotationX = newRotX;

                                        if (RotationX > rotMax * 0.99f)
                                        {
                                            RotationX = rotMax * 0.99f;
                                        }

                                        float pitch = (float)(Math.PI * (RotationX) / 180.0f);
                                        float yaw = (float)(Math.PI * (RotationY) / 180.0f);
                                        float roll = (float)(Math.PI * (RotationZ) / 180.0f);

                                        rotatingMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                                        pitch = (float)(Math.PI * (RotationX4Pelvis) / 180.0f);
                                        yaw = (float)(Math.PI * (RotationY4Pelvis) / 180.0f);
                                        roll = (float)(Math.PI * (RotationZ4Pelvis) / 180.0f);

                                        rotatingMatrixForPelvis = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);


                                        pitch = (float)(Math.PI * (RotationGrabbedX) / 180.0f);
                                        yaw = (float)(Math.PI * (RotationGrabbedY) / 180.0f);
                                        roll = (float)(Math.PI * (RotationGrabbedZ) / 180.0f);

                                        rotatingMatrixForGrabber = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);
                                        if (_has_grabbed_right_swtch == 2)
                                        {
                                            _swtch_hasRotated = 1;
                                        }

                                    }
                                }
                                else
                                {

                                    //RotationGrabbedY = RotationY4Pelvis;
                                    //RotationGrabbedX = RotationX4Pelvis;
                                    //RotationGrabbedZ = RotationZ4Pelvis;

                                    RotationGrabbedYOff = RotationY4Pelvis;
                                    RotationGrabbedXOff = RotationX4Pelvis;
                                    RotationGrabbedZOff = RotationZ4Pelvis;

                                    if (RotationGrabbedSwtch == 1)
                                    {
                                        RotationX4PelvisTwo = 0;
                                        RotationY4PelvisTwo = 0;
                                        RotationZ4PelvisTwo = 0;
                                        RotationGrabbedSwtch = 0;
                                    }

                                    //RotationGrabbedY = 0;
                                    //RotationGrabbedX = 0;
                                    //RotationGrabbedZ = 0;

                                    if (thumbStickRight[1].X == 0 && thumbStickRight[1].X == 0 && thumbStickRight[1].Y == 0 && thumbStickRight[1].Y == 0)
                                    {
                                        if (_swtch_hasRotated == 1)
                                        {

                                            _swtch_hasRotated = 2;
                                        }
                                        //_swtch_hasRotated = 0;

                                        RotationX = 0;
                                        RotationY = 0;
                                        RotationZ = 0;

                                        float pitch = (float)(RotationX * 0.0174532925f);
                                        float yaw = (float)(RotationY * 0.0174532925f);
                                        float roll = (float)(RotationZ * 0.0174532925f);

                                        rotatingMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                                        pitch = (float)(RotationX4Pelvis * 0.0174532925f);
                                        yaw = (float)(RotationY4Pelvis * 0.0174532925f);
                                        roll = (float)(RotationZ4Pelvis * 0.0174532925f);

                                        rotatingMatrixForPelvis = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                                        pitch = (float)(RotationGrabbedX * 0.0174532925f);
                                        yaw = (float)(RotationGrabbedY * 0.0174532925f);
                                        roll = (float)(RotationGrabbedZ * 0.0174532925f);

                                        rotatingMatrixForGrabber = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);
                                        if (_swtch_hasRotated == 0)
                                        {
                                            _sec_logic_swtch_grab = 0;
                                        }
                                    }
                                    else
                                    {

                                    }
                                }
                            }




                            //Vector3 resulter;
                            //Vector3.TransformCoordinate(ref _hmdPoser, ref WorldMatrix, out resulter);
                            //var someMatrix = hmd_matrix * finalRotationMatrix;


                            //OFFSETPOS.Y += _hmdPoser.Y;
                        }








                        Matrix tempmat = hmd_matrix * rotatingMatrixForPelvis * hmdmatrixRot;



                        Quaternion otherQuat;
                        Quaternion.RotationMatrix(ref tempmat, out otherQuat);

                        Vector3 direction_feet_forward;
                        Vector3 direction_feet_right;
                        Vector3 direction_feet_up;

                        direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                        direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                        direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);

                        if (thumbStickLeft[0].X > 0.15f)
                        {
                            movePos += direction_feet_right * speedPos * thumbStickLeft[0].X;
                        }
                        else if (thumbStickLeft[0].X < -0.15f)
                        {
                            movePos -= direction_feet_right * speedPos * -thumbStickLeft[0].X;
                        }

                        if (thumbStickLeft[0].Y > 0.15f)
                        {
                            movePos += direction_feet_forward * speedPos * thumbStickLeft[0].Y;
                        }
                        else if (thumbStickLeft[0].Y < -0.15f)
                        {
                            movePos -= direction_feet_forward * speedPos * -thumbStickLeft[0].Y;
                        }


                        OFFSETPOS = originPos + movePos;// + _hmdPoser; //_hmdPoser











                        /*if (writetobufferchunk == 0)
                        {

                            writetobufferchunk = 1;
                        }*/

                        //if (writetobufferikrig == 0)
                        {


                            /*var main_thread_update = new Thread(() =>
                            {
                                try
                                {
                                    //MainWindow.MessageBox((IntPtr)0, "threadstart succes", "sc core systems message", 0);
                                    Stopwatch _this_thread_ticks_01 = new Stopwatch();
                                    _this_thread_ticks_01.Start();

                                _thread_looper:

                                    Thread.Sleep(1);
                                    goto _thread_looper;
                                }
                                catch (Exception ex)
                                {

                                }

                            }, 3);

                            main_thread_update.IsBackground = true;
                            //main_thread_update.SetApartmentState(ApartmentState.STA);
                            main_thread_update.Start();*/


                            /*try
                            {
                                _sc_jitter_tasks = graphicssec.sccswriteikrigtobuffer(_sc_jitter_tasks, viewMatrix, _projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdmatrixRot, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix);

                            }
                            catch (Exception ex)
                            {
                               MainWindow.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                            }*/


                            //_sc_jitter_tasks = graphicssec.sccswriteikrigtobuffer(_sc_jitter_tasks);
                            //_sc_jitter_tasks = graphicssec.sccswritescreenassetstobuffer(_sc_jitter_tasks);
                        }
                        //writetobufferikrig = 1;

                    }





                }
                catch (Exception ex)
                {
                    MainWindow.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                }

            }
            Matrix someextrapelvismatrix = rotatingMatrixForPelvis; //originRot



            _ticks_watch.Restart();

            /*if (writetobuffer == 0)
            {
                if (_can_work_physics == 1)
                {
                    
                }

                writetobuffer = 1;
            }*/

            try
            {





                /*
                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                Matrix sometempmat2 = sc_graphics_sec.ikvoxelbody[0]._player_head[0][0]._arrayOfInstances[0].current_pos;
                Quaternion somedirquat2;
                Quaternion.RotationMatrix(ref sometempmat2, out somedirquat2);
                var dirikvoxelbodyInstanceRight2 = -sc_maths._newgetdirleft(somedirquat2);
                var dirikvoxelbodyInstanceUp2 = sc_maths._newgetdirup(somedirquat2);
                var dirikvoxelbodyInstanceForward2 = sc_maths._newgetdirforward(somedirquat2);


                Vector3 tempOffset = OFFSETPOS;

                //int usethirdpersonview = 1;

                if (MainWindow.usethirdpersonview == 0)
                {
                    tempOffset.X = SC_Update.viewPosition.X;
                    tempOffset.Y = SC_Update.viewPosition.Y;
                    tempOffset.Z = SC_Update.viewPosition.Z;

                    tempOffset.Y = tempOffset.Y - sc_graphics_sec.ikvoxelbody[0]._player_head[0][0]._arrayOfInstances[0]._TEMPPIVOT.M42;
                    tempOffset = tempOffset + (dirikvoxelbodyInstanceUp2 * (sc_graphics_sec.ikvoxelbody[0]._player_head[0][0]._total_torso_height*0.5f));
                }
                else if (MainWindow.usethirdpersonview == 1)
                {

                    //OFFSETPOS.X = SC_Update.viewPosition.X;
                    //OFFSETPOS.Y = SC_Update.viewPosition.Y;
                    //OFFSETPOS.Z = SC_Update.viewPosition.Z;

                    //OFFSETPOS = OFFSETPOS + (dirikvoxelbodyInstanceUp0 * -0.125f);
                    //SC_Update.viewPosition = SC_Update.viewPosition + (dirikvoxelbodyInstanceRight0 * -1.5f);





                    //tempmatter = hmd_matrix * rotatingMatrixForPelvis * hmdmatrixRot;
                    Quaternion quatt;
                    Quaternion.RotationMatrix(ref SC_Update.tempmatter, out quatt);
                    // quatt.Invert();

                    //THIRD PERSON VR VIEW. COMMENT THIS PART OUT TO HAVE FIRST PERSON VIEW
                    Vector3 forwardOVR = sc_maths._getDirection(Vector3.ForwardRH, quatt);
                    Vector3 upOVR = sc_maths._getDirection(Vector3.Up, quatt);
                    Vector3 rightOVR = sc_maths._getDirection(Vector3.Right, quatt);
                    upOVR.Normalize();
                    rightOVR.Normalize();
                    forwardOVR.Normalize();

                    forwardOVR *= -0.5f; // -1.0f

                    Vector3 thirdpersonview = OFFSETPOS + (-forwardOVR * 2.0f); //1.5f // + (upOVR * 0.25f)

                    OFFSETPOS.X = thirdpersonview.X;// SC_Update.viewPosition.X;
                    OFFSETPOS.Y = thirdpersonview.Y;// SC_Update.viewPosition.Y;
                    OFFSETPOS.Z = thirdpersonview.Z;// SC_Update.viewPosition.Z;
                }
                */


                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW






                //device.ImmediateContext.OutputMerger.SetDepthStencilState(D3D.depthStencilStateTWOSIDEDDRAW);


                /*
                device.ImmediateContext.Rasterizer.State = rasterState;
                device.ImmediateContext.OutputMerger.SetBlendState(blendState);
                device.ImmediateContext.OutputMerger.SetDepthStencilState(depthState);
                device.ImmediateContext.PixelShader.SetSampler(0, samplerState);*/

                /*
                Vector3f[] hmdToEyeViewOffsets = { D3D.eyeTextures[0].HmdToEyeViewOffset, D3D.eyeTextures[1].HmdToEyeViewOffset };
                displayMidpoint = D3D.OVR.GetPredictedDisplayTime(D3D.sessionPtr, 0);
                trackingState = D3D.OVR.GetTrackingState(D3D.sessionPtr, displayMidpoint, true);
                eyePoses = new Posef[2];
                D3D.OVR.CalcEyePoses(trackingState.HeadPose.ThePose, hmdToEyeViewOffsets, ref eyePoses);

                for (int eyeIndex = 0; eyeIndex < 2; eyeIndex++)
                {
                    eye = (EyeType)eyeIndex;
                    eyeTexture = D3D.eyeTextures[eyeIndex];

                    if (eyeIndex == 0)
                    {
                        D3D.layerEyeFov.RenderPoseLeft = eyePoses[0];
                    }
                    else
                    {
                        D3D.layerEyeFov.RenderPoseRight = eyePoses[1];
                    }

                    eyeTexture.RenderDescription = D3D.OVR.GetRenderDesc(D3D.sessionPtr, eye, D3D.hmdDesc.DefaultEyeFov[eyeIndex]);

                    D3D.result = eyeTexture.SwapTextureSet.GetCurrentIndex(out textureIndex);
                    D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to retrieve texture swap chain current index.");

                    D3D.device.ImmediateContext.OutputMerger.SetRenderTargets(eyeTexture.DepthStencilView, eyeTexture.RenderTargetViews[textureIndex]);
                    D3D.device.ImmediateContext.ClearRenderTargetView(eyeTexture.RenderTargetViews[textureIndex], SharpDX.Color.CornflowerBlue); //DimGray //Black
                    D3D.device.ImmediateContext.ClearDepthStencilView(eyeTexture.DepthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);
                    D3D.device.ImmediateContext.Rasterizer.SetViewport(eyeTexture.Viewport);
                */







                //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                /*SharpDX.Matrix eyeQuaternionMatrix = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X, eyePoses[eyeIndex].Orientation.Y, eyePoses[eyeIndex].Orientation.Z, eyePoses[eyeIndex].Orientation.W));
                SharpDX.Vector3 eyePos = SharpDX.Vector3.Transform(new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z), originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot).ToVector3();
                //finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix;
                finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot;
                lookUp = Vector3.Transform(new Vector3(0, 1, 0), finalRotationMatrix).ToVector3();
                lookAt = Vector3.Transform(new Vector3(0, 0, -1), finalRotationMatrix).ToVector3();
                viewpositionorigin = eyePos;
                viewPosition = eyePos + OFFSETPOS; // 
                tempmatter = hmd_matrix * rotatingMatrixForPelvis * hmdmatrixRot;

                Quaternion quatt;
                Quaternion.RotationMatrix(ref tempmatter, out quatt);

                if (MainWindow.usethirdpersonview == 0)
                {

                }
                else if (MainWindow.usethirdpersonview == 1)
                {
                    Quaternion somedirquat1;
                    Quaternion.RotationMatrix(ref tempmatter, out somedirquat1);
                    var dirikvoxelbodyInstanceRight0 = -sc_maths._newgetdirleft(somedirquat1);
                    var dirikvoxelbodyInstanceUp0 = sc_maths._newgetdirup(somedirquat1);
                    var dirikvoxelbodyInstanceForward0 = sc_maths._newgetdirforward(somedirquat1);
                    viewPosition = viewPosition + (dirikvoxelbodyInstanceForward0 * MainWindow.offsetthirdpersonview);
                }

                viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);
                _projectionMatrix = D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.001f, 1000.0f, ProjectionModifier.None).ToMatrix();
                oriProjectionMatrix = _projectionMatrix;
                _projectionMatrix.Transpose();*/
                //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.







                if (SC_console_directx._useOculusRift)
                {
                    if (scgraphicssecpackagemessage.scjittertasks != null)
                    {
                        if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                        {
                            if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                            {
                                scgraphicssecpackagemessage.scgraphicssec.oculuscontrolsNRecordSoundNMousePointer();
                            }
                        }
                    }
                }













                if (!SC_console_directx._useOculusRift)
                {
                    // Clear views
                    D3D.DeviceContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);
                    D3D.DeviceContext.ClearRenderTargetView(_renderTargetView, SharpDX.Color.LightGray);






                    float ratio = (float)SurfaceWidth / (float)SurfaceHeight;
                    _projectionMatrix  = Matrix.PerspectiveFovLH(3.14F / 3.0F, ratio, 0.001f, 1000);
                    //viewMatrix = Matrix.LookAtLH(new Vector3(0, 3, -10), new Vector3(), Vector3.UnitY);



                    viewMatrix = Camera.ViewMatrix;




                    //MainWindow.MessageBox((IntPtr)0, "0", "sc core systems message", 0);
                    if (_can_work_physics == 1)
                    {



                        //TO READD IF IT DOESNT WORK
                        //TO READD IF IT DOESNT WORK
                        //TO READD IF IT DOESNT WORK

                        /*if (graphicssec != null)
                        {
                            //MainWindow.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                            try
                            {
                                _sc_jitter_tasks = graphicssec.workOnSomething(_sc_jitter_tasks, viewMatrix, _projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdmatrixRot, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix, someextrapelvismatrix);

                                if (writetobufferchunk == 0)
                                {


                                    writetobufferchunk = 1;
                                }

                                _sc_jitter_tasks = graphicssec.sc_write_to_buffer(_sc_jitter_tasks);
                                _sc_jitter_tasks = graphicssec.sccswritescreenassetstobuffer(_sc_jitter_tasks);
                                _sc_jitter_tasks = graphicssec.sccswriteikrigtobuffer(_sc_jitter_tasks, viewMatrix, _projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdmatrixRot, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix);



                            }
                            catch (Exception ex)
                            {
                                MainWindow.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                            }
                        }*/
                        //TO READD IF IT DOESNT WORK
                        //TO READD IF IT DOESNT WORK
                        //TO READD IF IT DOESNT WORK

                        var somerotvec = Camera.GetRotation();
                        float pitch = somerotvec.X * 0.0174532925f;
                        float yaw = somerotvec.Y * 0.0174532925f; ;
                        float roll = somerotvec.Z * 0.0174532925f; ;
                        Matrix somerotmat = Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                        scgraphicssecpackagemessage.viewMatrix = viewMatrix;
                        scgraphicssecpackagemessage.projectionMatrix = _projectionMatrix;
                        scgraphicssecpackagemessage.originRot = originRot;
                        scgraphicssecpackagemessage.rotatingMatrix = somerotmat; //rotatingMatrix
                        scgraphicssecpackagemessage.hmdmatrixRot = hmdmatrixRot;
                        scgraphicssecpackagemessage.hmd_matrix = hmd_matrix;
                        scgraphicssecpackagemessage.rotatingMatrixForPelvis = rotatingMatrixForPelvis;
                        scgraphicssecpackagemessage.rightTouchMatrix = _rightTouchMatrix;
                        scgraphicssecpackagemessage.leftTouchMatrix = _leftTouchMatrix;
                        scgraphicssecpackagemessage.oriProjectionMatrix = oriProjectionMatrix;
                        scgraphicssecpackagemessage.someextrapelvismatrix = someextrapelvismatrix;
                        scgraphicssecpackagemessage.offsetpos = Camera.GetPosition();
                        scgraphicssecpackagemessage.handPoseRight = handPoseRight;
                        scgraphicssecpackagemessage.handPoseLeft = handPoseLeft;
                        //scgraphicssecpackagemessage.scgraphicssec = null;
                        scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;


                        if (eyebufferthreadswtc == 0)
                        {

                            Thread main_thread_update = new Thread(() =>
                            {
                                scgraphicssecpackagemessage.scgraphicssec = new sc_graphics_sec();


                                scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec._sc_create_world_objects(scgraphicssecpackagemessage.scjittertasks);

                                //scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;
                                //sc_graphics_sec graphicssec;

                                int bufferswtc = 0;
                            //MainWindow.MessageBox((IntPtr)0, "test", "sccsmsg", 0);

                            _thread_looper:


                                //scgraphicssecpackagemessage.scgraphicssec = graphicssec;

                                //MainWindow.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                                try
                                {
                                    if (bufferswtc == 0) // 0
                                    {
                                        scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workOnSomething(scgraphicssecpackagemessage);
                                        scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);
                                        scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswritescreenassetstobuffer(scgraphicssecpackagemessage.scjittertasks);
                                        scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);


                                        //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);
                                        //bufferswtc = 1;
                                    }


                                }
                                catch (Exception ex)
                                {
                                    MainWindow.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                                }


                          

                                Thread.Sleep(1);
                                goto _thread_looper;

                                //ShutDown();
                                //ShutDownGraphics();

                            }, 0);

                            main_thread_update.IsBackground = true;
                            main_thread_update.Priority = ThreadPriority.Lowest; //AboveNormal
                            main_thread_update.SetApartmentState(ApartmentState.STA);
                            main_thread_update.Start();


                            /*
                            var _console_worker_task = Task<object[]>.Factory.StartNew((sometaskmsg) =>
                            {
                            loopthread:
                                //MainWindow.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                                try
                                {
                                }
                                catch (Exception ex)
                                {
                                    MainWindow.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                                }
                                Thread.Sleep(1);
                                goto loopthread;
                            }, scgraphicssecpackagemessage);
                            */

                            eyebufferthreadswtc = 1;
                        }

                        //scgraphicssecpackagemessage.scjittertasks = graphicssec.workonshaders(scgraphicssecpackagemessage);


                        if (scgraphicssecpackagemessage.scjittertasks != null)
                        {
                            if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                            {
                                if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                {
                                    _sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);

                                    //_sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;
                                }
                            }
                        }




                    }
                    D3D.SwapChain.Present(0, PresentFlags.None);








                }
                else
                {

                    Vector3f[] hmdToEyeViewOffsets = { D3D.eyeTextures[0].HmdToEyeViewOffset, D3D.eyeTextures[1].HmdToEyeViewOffset };
                    displayMidpoint = D3D.OVR.GetPredictedDisplayTime(D3D.sessionPtr, 0);
                    trackingState = D3D.OVR.GetTrackingState(D3D.sessionPtr, displayMidpoint, true);
                    eyePoses = new Posef[2];
                    D3D.OVR.CalcEyePoses(trackingState.HeadPose.ThePose, hmdToEyeViewOffsets, ref eyePoses);

                    //float timeSinceStart = (float)(DateTime.Now - startTime).TotalSeconds;

                    for (int eyeIndex = 0; eyeIndex < 2; eyeIndex++)
                    {
                        eye = (EyeType)eyeIndex;
                        eyeTexture = D3D.eyeTextures[eyeIndex];

                        if (eyeIndex == 0)
                        {
                            D3D.layerEyeFov.RenderPoseLeft = eyePoses[0];
                        }
                        else
                        {
                            D3D.layerEyeFov.RenderPoseRight = eyePoses[1];
                        }
                        // Update the render description at each frame, as the HmdToEyeOffset can change at runtime.
                        eyeTexture.RenderDescription = OVR.GetRenderDesc(sessionPtr, eye, hmdDesc.DefaultEyeFov[eyeIndex]);

                        // Retrieve the index of the active texture
                        int textureIndex;
                        D3D.result = eyeTexture.SwapTextureSet.GetCurrentIndex(out textureIndex);
                        D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to retrieve texture swap chain current index.");

                        D3D.device.ImmediateContext.OutputMerger.SetRenderTargets(eyeTexture.DepthStencilView, eyeTexture.RenderTargetViews[textureIndex]);
                        D3D.device.ImmediateContext.ClearRenderTargetView(eyeTexture.RenderTargetViews[textureIndex], SharpDX.Color.CornflowerBlue);
                        D3D.device.ImmediateContext.ClearDepthStencilView(eyeTexture.DepthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);
                        D3D.device.ImmediateContext.Rasterizer.SetViewport(eyeTexture.Viewport);





                        /*
                        // Initialize and set up the description of the stencil state.
                        DepthStencilStateDescription depthStencilDesc = new DepthStencilStateDescription()
                        {
                            IsDepthEnabled = true,
                            DepthWriteMask = DepthWriteMask.All,
                            DepthComparison = Comparison.Less,
                            IsStencilEnabled = true,
                            StencilReadMask = 0xFF,
                            StencilWriteMask = 0xFF,

                            // Stencil operation if pixel front-facing.
                            FrontFace = new DepthStencilOperationDescription()
                            {
                                FailOperation = StencilOperation.Keep,
                                DepthFailOperation = StencilOperation.Increment,
                                PassOperation = StencilOperation.Keep,
                                Comparison = Comparison.Always
                            },
                            // Stencil operation if pixel is back-facing.
                            BackFace = new DepthStencilOperationDescription()
                            {
                                FailOperation = StencilOperation.Keep,
                                DepthFailOperation = StencilOperation.Decrement,
                                PassOperation = StencilOperation.Keep,
                                Comparison = Comparison.Always
                            }
                        };

                        // Create the depth stencil state.
                        var DepthStencilState = new DepthStencilState(D3D.device, depthStencilDesc);

                        // Set the depth stencil state.
                        //D3D.device.ImmediateContext.OutputMerger.SetDepthStencilState(DepthStencilState, 1);
                        eyeTexture.DepthStencilView.Device.ImmediateContext.OutputMerger.SetDepthStencilState(DepthStencilState, 1);*/







                        /*
                    // Create an alpha enabled blend state description.
                    var blendStateDesc = new BlendStateDescription();
                    blendStateDesc.RenderTarget[0].IsBlendEnabled = true;
                    blendStateDesc.RenderTarget[0].SourceBlend = BlendOption.;
                    blendStateDesc.RenderTarget[0].DestinationBlend = BlendOption.InverseSourceAlpha;
                    blendStateDesc.RenderTarget[0].BlendOperation = BlendOperation.Add;
                    blendStateDesc.RenderTarget[0].SourceAlphaBlend = BlendOption.One;
                    blendStateDesc.RenderTarget[0].DestinationAlphaBlend = BlendOption.Zero;
                    blendStateDesc.RenderTarget[0].AlphaBlendOperation = BlendOperation.Add;
                    blendStateDesc.RenderTarget[0].RenderTargetWriteMask = ColorWriteMaskFlags.All;

                    // Create the blend state using the description.
                    var AlphaEnableBlendingState = new BlendState(device, blendStateDesc);

                    // Modify the description to create an disabled blend state description.
                    blendStateDesc.RenderTarget[0].IsBlendEnabled = false;

                    // Create the blend state using the description.
                    var AlphaDisableBlendingState = new BlendState(device, blendStateDesc);

                    // Setup the blend factor.
                    var blendFactor = new Color4(0, 0, 0, 0);

                    // Turn on the alpha blending.
                    DeviceContext.OutputMerger.SetBlendState(AlphaDisableBlendingState, blendFactor, -1);*/





                        /*
                        //SharpDX.Matrix eyeQuaternionMatrix = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X, eyePoses[eyeIndex].Orientation.Y, eyePoses[eyeIndex].Orientation.Z, eyePoses[eyeIndex].Orientation.W));
                        SharpDX.Vector3 eyePos = SharpDX.Vector3.Transform(new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z), originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot).ToVector3();

                        //SharpDX.Vector3 eyePos = new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z);
                        Vector3 pos = ((eyePos + OFFSETPOS));


                        //pos.X -= eyePos.X;
                        //pos.Y += eyePos.Y;
                        //pos.Z -= eyePos.Z;

                        var eyeQuaternionMatrix = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X,
                                                                                                           eyePoses[eyeIndex].Orientation.Y * -1,
                                                                                                          eyePoses[eyeIndex].Orientation.Z,
                                                                                                          eyePoses[eyeIndex].Orientation.W * -1));



                        finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot;
                        //Matrix rot = eyeQuaternionMatrix;
                        //rot.Invert();
                        Vector3 finalUp = Vector3.Transform(new Vector3(0, 1, 0), finalRotationMatrix).ToVector3();
                        Vector3 finalForward = Vector3.Transform(new Vector3(0, 0, 1), finalRotationMatrix).ToVector3();

                        viewMatrix = SharpDX.Matrix.LookAtRH(pos, (pos + finalForward), finalUp);
                        _projectionMatrix = OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.1f, 100.0f, ProjectionModifier.None).ToMatrix();
                        _projectionMatrix.Transpose();*/





                        //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                        //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                        //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                        SharpDX.Matrix eyeQuaternionMatrix = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X, eyePoses[eyeIndex].Orientation.Y, eyePoses[eyeIndex].Orientation.Z, eyePoses[eyeIndex].Orientation.W));
                        SharpDX.Vector3 eyePos = SharpDX.Vector3.Transform(new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z), originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot).ToVector3();
                        //finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix;
                        finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot;
                        lookUp = Vector3.Transform(new Vector3(0, 1, 0), finalRotationMatrix).ToVector3();
                        lookAt = Vector3.Transform(new Vector3(0, 0, -1), finalRotationMatrix).ToVector3();
                        viewpositionorigin = eyePos;
                        viewPosition = eyePos + OFFSETPOS; // 
                        tempmatter = hmd_matrix * rotatingMatrixForPelvis * hmdmatrixRot;

                        Quaternion quatt;
                        Quaternion.RotationMatrix(ref tempmatter, out quatt);

                        if (MainWindow.usethirdpersonview == 0)
                        {

                            //FOR THE VERTEX SHADER
                            Quaternion somedirquat1;
                            Quaternion.RotationMatrix(ref tempmatter, out somedirquat1);
                            dirikvoxelbodyInstanceRight0 = new Vector4(-sc_maths._newgetdirleft(somedirquat1), 0);
                            dirikvoxelbodyInstanceUp0 = new Vector4(sc_maths._newgetdirup(somedirquat1), 0);
                            dirikvoxelbodyInstanceForward0 = new Vector4(sc_maths._newgetdirforward(somedirquat1), 0);
                        }
                        else if (MainWindow.usethirdpersonview == 1)
                        {
                            Quaternion somedirquat1;
                            Quaternion.RotationMatrix(ref tempmatter, out somedirquat1);
                            dirikvoxelbodyInstanceRight0 = new Vector4(-sc_maths._newgetdirleft(somedirquat1), 0);
                            dirikvoxelbodyInstanceUp0 = new Vector4(sc_maths._newgetdirup(somedirquat1), 0);
                            dirikvoxelbodyInstanceForward0 = new Vector4(sc_maths._newgetdirforward(somedirquat1), 0);

                            viewPosition = viewPosition + (new Vector3(dirikvoxelbodyInstanceForward0.X, dirikvoxelbodyInstanceForward0.Y, dirikvoxelbodyInstanceForward0.Z) * MainWindow.offsetthirdpersonview);
                        }





                        viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);
                        _projectionMatrix = D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.001f, 1000.0f, ProjectionModifier.None).ToMatrix();
                        oriProjectionMatrix = _projectionMatrix;
                        _projectionMatrix.Transpose();


                        //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                        //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                        //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.












                        //MainWindow.MessageBox((IntPtr)0, "0", "sc core systems message", 0);
                        if (_can_work_physics == 1)
                        {



                            //TO READD IF IT DOESNT WORK
                            //TO READD IF IT DOESNT WORK
                            //TO READD IF IT DOESNT WORK

                            /*if (graphicssec != null)
                            {
                                //MainWindow.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                                try
                                {
                                    _sc_jitter_tasks = graphicssec.workOnSomething(_sc_jitter_tasks, viewMatrix, _projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdmatrixRot, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix, someextrapelvismatrix);

                                    if (writetobufferchunk == 0)
                                    {


                                        writetobufferchunk = 1;
                                    }

                                    _sc_jitter_tasks = graphicssec.sc_write_to_buffer(_sc_jitter_tasks);
                                    _sc_jitter_tasks = graphicssec.sccswritescreenassetstobuffer(_sc_jitter_tasks);
                                    _sc_jitter_tasks = graphicssec.sccswriteikrigtobuffer(_sc_jitter_tasks, viewMatrix, _projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdmatrixRot, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix);



                                }
                                catch (Exception ex)
                                {
                                    MainWindow.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                                }
                            }*/
                            //TO READD IF IT DOESNT WORK
                            //TO READD IF IT DOESNT WORK
                            //TO READD IF IT DOESNT WORK




                            scgraphicssecpackagemessage.viewMatrix = viewMatrix;
                            scgraphicssecpackagemessage.projectionMatrix = _projectionMatrix;
                            scgraphicssecpackagemessage.originRot = originRot;
                            scgraphicssecpackagemessage.rotatingMatrix = rotatingMatrix;
                            scgraphicssecpackagemessage.hmdmatrixRot = hmdmatrixRot;
                            scgraphicssecpackagemessage.hmd_matrix = hmd_matrix;
                            scgraphicssecpackagemessage.rotatingMatrixForPelvis = rotatingMatrixForPelvis;
                            scgraphicssecpackagemessage.rightTouchMatrix = _rightTouchMatrix;
                            scgraphicssecpackagemessage.leftTouchMatrix = _leftTouchMatrix;
                            scgraphicssecpackagemessage.oriProjectionMatrix = oriProjectionMatrix;
                            scgraphicssecpackagemessage.someextrapelvismatrix = someextrapelvismatrix;
                            scgraphicssecpackagemessage.offsetpos = OFFSETPOS;
                            scgraphicssecpackagemessage.handPoseRight = handPoseRight;
                            scgraphicssecpackagemessage.handPoseLeft = handPoseLeft;
                            //scgraphicssecpackagemessage.scgraphicssec = null;
                            scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;


                            if (eyebufferthreadswtc == 0)
                            {

                                Thread main_thread_update = new Thread(() =>
                                {
                                    scgraphicssecpackagemessage.scgraphicssec = new sc_graphics_sec();


                                    scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec._sc_create_world_objects(scgraphicssecpackagemessage.scjittertasks);

                                    //scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;
                                    //sc_graphics_sec graphicssec;

                                    int bufferswtc = 0;
                                //MainWindow.MessageBox((IntPtr)0, "test", "sccsmsg", 0);

                                _thread_looper:


                                    //scgraphicssecpackagemessage.scgraphicssec = graphicssec;

                                    //MainWindow.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                                    try
                                    {

                                        //_ticks_watch.Stop();
                                        //_ticks_watch.Restart();


                                        if (bufferswtc == 0) //0
                                        {
                                            scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workOnSomething(scgraphicssecpackagemessage);
                                            scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);
                                            scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswritescreenassetstobuffer(scgraphicssecpackagemessage.scjittertasks);
                                            scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);


                                            //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);
                                            //bufferswtc = 1;
                                        }

                                        //Console.WriteLine(_ticks_watch.Elapsed.Milliseconds);


                                    }
                                    catch (Exception ex)
                                    {
                                        MainWindow.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                                    }





                                    Thread.Sleep(1);
                                    goto _thread_looper;

                                    //ShutDown();
                                    //ShutDownGraphics();

                                }, 0);

                                main_thread_update.IsBackground = true;
                                main_thread_update.Priority = ThreadPriority.Lowest; //AboveNormal
                                main_thread_update.SetApartmentState(ApartmentState.STA);
                                main_thread_update.Start();

                                /*
                                var _console_worker_task = Task<object[]>.Factory.StartNew((sometaskmsg) =>
                                {
                                loopthread:
                                    //MainWindow.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                                    try
                                    {
                                    }
                                    catch (Exception ex)
                                    {
                                        MainWindow.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                                    }
                                    Thread.Sleep(1);
                                    goto loopthread;
                                }, scgraphicssecpackagemessage);
                                */


                                eyebufferthreadswtc = 1;
                            }

                            //scgraphicssecpackagemessage.scjittertasks = graphicssec.workonshaders(scgraphicssecpackagemessage);


                            _ticks_watch.Stop();
                            _ticks_watch.Restart();
                            if (scgraphicssecpackagemessage.scjittertasks != null)
                            {
                                if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                {
                                    if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                    {
                                        _sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);

                                        //_sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;
                                    }
                                }
                            }

                            //Console.WriteLine(_ticks_watch.Elapsed.Milliseconds);


                        }





                        /*
                        _worldMatrix = _WorldMatrix;
                        _viewMatrix = viewMatrix;
                        _projectionMatrix = projectionMatrix;

                        _worldMatrix.Transpose();
                        _viewMatrix.Transpose();
                        _projectionMatrix.Transpose();

                        arrayOfMatrixBuff[0] = new DMatrixBuffer()
                        {
                            world = _worldMatrix,
                            view = _viewMatrix,
                            proj = _projectionMatrix,
                        };*/




                        //Console.WriteLine(timeWatch.Elapsed.Milliseconds);

                        D3D.result = eyeTexture.SwapTextureSet.Commit();
                        D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to commit the swap chain texture.");
                    }

                    //_ticks_watch.Restart();

                    D3D.result = D3D.OVR.SubmitFrame(D3D.sessionPtr, 0L, IntPtr.Zero, ref D3D.layerEyeFov);
                    D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to submit the frame of the current layers.");
                    D3D.DeviceContext.CopyResource(D3D.mirrorTextureD3D, D3D.BackBuffer);
                    D3D.SwapChain.Present(0, PresentFlags.None);
                }










































                /*
                Vector3f[] hmdToEyeViewOffsets = { D3D.eyeTextures[0].HmdToEyeViewOffset, D3D.eyeTextures[1].HmdToEyeViewOffset };
                displayMidpoint = D3D.OVR.GetPredictedDisplayTime(D3D.sessionPtr, 0);
                trackingState = D3D.OVR.GetTrackingState(D3D.sessionPtr, displayMidpoint, true);
                eyePoses = new Posef[2];
                D3D.OVR.CalcEyePoses(trackingState.HeadPose.ThePose, hmdToEyeViewOffsets, ref eyePoses);

                for (int eyeIndex = 0; eyeIndex < 2; eyeIndex++)
                {
                    eye = (EyeType)eyeIndex;
                    eyeTexture = D3D.eyeTextures[eyeIndex];

                    if (eyeIndex == 0)
                    {
                        D3D.layerEyeFov.RenderPoseLeft = eyePoses[0];
                    }
                    else
                    {
                        D3D.layerEyeFov.RenderPoseRight = eyePoses[1];
                    }

                    eyeTexture.RenderDescription = D3D.OVR.GetRenderDesc(D3D.sessionPtr, eye, D3D.hmdDesc.DefaultEyeFov[eyeIndex]);

                    D3D.result = eyeTexture.SwapTextureSet.GetCurrentIndex(out textureIndex);
                    D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to retrieve texture swap chain current index.");

                    D3D.device.ImmediateContext.OutputMerger.SetRenderTargets(eyeTexture.DepthStencilView, eyeTexture.RenderTargetViews[textureIndex]);
                    D3D.device.ImmediateContext.ClearRenderTargetView(eyeTexture.RenderTargetViews[textureIndex], SharpDX.Color.CornflowerBlue); //DimGray //Black
                    D3D.device.ImmediateContext.ClearDepthStencilView(eyeTexture.DepthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);
                    D3D.device.ImmediateContext.Rasterizer.SetViewport(eyeTexture.Viewport);






                    /*
                    SharpDX.Vector3 eyePos = new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z);
                    Vector3 pos = ((originPos + OFFSETPOS));

                    pos.X -= eyePos.X;
                    pos.Y += eyePos.Y;
                    pos.Z -= eyePos.Z;

                    var eyeQuaternionMatrix = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X,
                                                                                                       eyePoses[eyeIndex].Orientation.Y * -1,
                                                                                                      eyePoses[eyeIndex].Orientation.Z,
                                                                                                      eyePoses[eyeIndex].Orientation.W * -1));


                    finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot;
                    Matrix rot = finalRotationMatrix;
                    //rot.Invert();
                    Vector3 finalUp = Vector3.Transform(new Vector3(0, 1, 0), rot).ToVector3();
                    Vector3 finalForward = Vector3.Transform(new Vector3(0, 0, 1), rot).ToVector3();


                    viewMatrix = SharpDX.Matrix.LookAtRH(pos, (pos + finalForward), finalUp);
                    _projectionMatrix = D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.1f, 100.0f, ProjectionModifier.None).ToMatrix();
                    _projectionMatrix.Transpose();









                    //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                    //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                    //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                    SharpDX.Matrix eyeQuaternionMatrix = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X, eyePoses[eyeIndex].Orientation.Y, eyePoses[eyeIndex].Orientation.Z, eyePoses[eyeIndex].Orientation.W));
                    SharpDX.Vector3 eyePos = SharpDX.Vector3.Transform(new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z), originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot).ToVector3();
                    //finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix;
                    finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot;
                    lookUp = Vector3.Transform(new Vector3(0, 1, 0), finalRotationMatrix).ToVector3();
                    lookAt = Vector3.Transform(new Vector3(0, 0, -1), finalRotationMatrix).ToVector3();
                    viewpositionorigin = eyePos;
                    viewPosition = eyePos + OFFSETPOS; // 
                    tempmatter = hmd_matrix * rotatingMatrixForPelvis * hmdmatrixRot;

                    Quaternion quatt;
                    Quaternion.RotationMatrix(ref tempmatter, out quatt);

                    if (MainWindow.usethirdpersonview == 0)
                    {

                    }
                    else if (MainWindow.usethirdpersonview == 1)
                    {
                        Quaternion somedirquat1;
                        Quaternion.RotationMatrix(ref tempmatter, out somedirquat1);
                        var dirikvoxelbodyInstanceRight0 = -sc_maths._newgetdirleft(somedirquat1);
                        var dirikvoxelbodyInstanceUp0 = sc_maths._newgetdirup(somedirquat1);
                        var dirikvoxelbodyInstanceForward0 = sc_maths._newgetdirforward(somedirquat1);
                        viewPosition = viewPosition + (dirikvoxelbodyInstanceForward0 * MainWindow.offsetthirdpersonview);
                    }

                    viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);
                    _projectionMatrix = D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.001f, 1000.0f, ProjectionModifier.None).ToMatrix();
                    oriProjectionMatrix = _projectionMatrix;
                    _projectionMatrix.Transpose();
                    //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                    //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                    //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.











                    //IN DEVELOPMENT WIP BY STEVE CHASSÉ
                    //IN DEVELOPMENT WIP BY STEVE CHASSÉ
                    //IN DEVELOPMENT WIP BY STEVE CHASSÉ
                    /*
                    SharpDX.Vector3 eyePos = new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z);
                    Vector3 somepostrialanderrortestingsuccessbystevechassé = ((originPos + OFFSETPOS));

                    somepostrialanderrortestingsuccessbystevechassé.X -= eyePos.X;
                    somepostrialanderrortestingsuccessbystevechassé.Y += eyePos.Y;
                    somepostrialanderrortestingsuccessbystevechassé.Z -= eyePos.Z;

                    var someeyequaterniontrialanderrortestingsuccessbystevechassé = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X,
                                                                                                       eyePoses[eyeIndex].Orientation.Y * -1,
                                                                                                      eyePoses[eyeIndex].Orientation.Z,
                                                                                                      eyePoses[eyeIndex].Orientation.W * -1)); 

                    Matrix somevalueofsuccessfromtrialanderrortestingbystevechassé = someeyequaterniontrialanderrortestingsuccessbystevechassé * originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot;
                    //rot.Invert();
                    lookUp = Vector3.Transform(new Vector3(0, 1, 0), somevalueofsuccessfromtrialanderrortestingbystevechassé).ToVector3();
                    lookAt = Vector3.Transform(new Vector3(0, 0, 1), somevalueofsuccessfromtrialanderrortestingbystevechassé).ToVector3();

                    //FIRST/THIRD PERSON VR VIEW. COMMENT THIS PART OUT TO HAVE FIRST PERSON VIEW 
                    if (MainWindow.usethirdpersonview == 0)
                    {
                        eyePos = SharpDX.Vector3.Transform(new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z), originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot).ToVector3();
                        viewPosition = eyePos + OFFSETPOS;
                    }
                    else if (MainWindow.usethirdpersonview == 1)
                    {
                        Quaternion somedirquat1;
                        Quaternion.RotationMatrix(ref tempmatter, out somedirquat1);
                        var dirikvoxelbodyInstanceRight0 = -sc_maths._newgetdirleft(somedirquat1);
                        var dirikvoxelbodyInstanceUp0 = sc_maths._newgetdirup(somedirquat1);
                        var dirikvoxelbodyInstanceForward0 = sc_maths._newgetdirforward(somedirquat1);
                        viewPosition = somepostrialanderrortestingsuccessbystevechassé + (dirikvoxelbodyInstanceForward0 * MainWindow.offsetthirdpersonview);
                    }
                    //FIRST/THIRD PERSON VR VIEW. COMMENT THIS PART OUT TO HAVE FIRST PERSON VIEW 

                    viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);
                    _projectionMatrix = D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.001f, 1000.0f, ProjectionModifier.None).ToMatrix();
                    oriProjectionMatrix = _projectionMatrix;
                    _projectionMatrix.Transpose();*/
                //IN DEVELOPMENT WIP BY STEVE CHASSÉ
                //IN DEVELOPMENT WIP BY STEVE CHASSÉ
                //IN DEVELOPMENT WIP BY STEVE CHASSÉ






                //IN DEVELOPMENT WIP BY STEVE CHASSÉ
                //IN DEVELOPMENT WIP BY STEVE CHASSÉ
                //IN DEVELOPMENT WIP BY STEVE CHASSÉ
                /*
                SharpDX.Vector3 eyePos = new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z);
                Vector3 somepostrialanderrortestingsuccessbystevechassé = ((originPos + OFFSETPOS));

                somepostrialanderrortestingsuccessbystevechassé.X -= eyePos.X;
                somepostrialanderrortestingsuccessbystevechassé.Y += eyePos.Y;
                somepostrialanderrortestingsuccessbystevechassé.Z -= eyePos.Z;

                var someeyequaterniontrialanderrortestingsuccessbystevechassé = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X,
                                                                                                   eyePoses[eyeIndex].Orientation.Y * -1,
                                                                                                  eyePoses[eyeIndex].Orientation.Z,
                                                                                                  eyePoses[eyeIndex].Orientation.W * -1));

                /*var someeyequaterniontrialanderrortestingsuccessbystevechassé = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X,
                                                                                                eyePoses[eyeIndex].Orientation.Y,
                                                                                               eyePoses[eyeIndex].Orientation.Z,
                                                                                               eyePoses[eyeIndex].Orientation.W));

                //var eyeQuaternionMatrix = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X, eyePoses[eyeIndex].Orientation.Y, eyePoses[eyeIndex].Orientation.Z, eyePoses[eyeIndex].Orientation.W));

                Matrix somevalueofsuccessfromtrialanderrortestingbystevechassé = someeyequaterniontrialanderrortestingsuccessbystevechassé * originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot;
                //rot.Invert();
                lookUp = Vector3.Transform(new Vector3(0, 1, 0), somevalueofsuccessfromtrialanderrortestingbystevechassé).ToVector3();
                lookAt = Vector3.Transform(new Vector3(0, 0, 1), somevalueofsuccessfromtrialanderrortestingbystevechassé).ToVector3();

                //FIRST/THIRD PERSON VR VIEW. COMMENT THIS PART OUT TO HAVE FIRST PERSON VIEW 
                if (MainWindow.usethirdpersonview == 0)
                {
                    eyePos = SharpDX.Vector3.Transform(new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z), originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot).ToVector3();
                    viewPosition = eyePos + OFFSETPOS;
                }
                else if (MainWindow.usethirdpersonview == 1)
                {
                    Quaternion somedirquat1;
                    Quaternion.RotationMatrix(ref tempmatter, out somedirquat1);
                    var dirikvoxelbodyInstanceRight0 = -sc_maths._newgetdirleft(somedirquat1);
                    var dirikvoxelbodyInstanceUp0 = sc_maths._newgetdirup(somedirquat1);
                    var dirikvoxelbodyInstanceForward0 = sc_maths._newgetdirforward(somedirquat1);
                    viewPosition = somepostrialanderrortestingsuccessbystevechassé + (dirikvoxelbodyInstanceForward0 * MainWindow.offsetthirdpersonview);
                }
                //FIRST/THIRD PERSON VR VIEW. COMMENT THIS PART OUT TO HAVE FIRST PERSON VIEW 

                viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);

                ///Matrix.RotationQuaternion();
                Quaternion somedirquat2;
                Quaternion.RotationMatrix(ref viewMatrix, out somedirquat2);

                somedirquat2.Y *= -1;
                somedirquat2.W *= -1;

                Matrix sometempmat;
                Matrix.RotationQuaternion(ref somedirquat2,out sometempmat);
                sometempmat.M41 = viewMatrix.M41;
                sometempmat.M42 = viewMatrix.M42;
                sometempmat.M43 = viewMatrix.M43;

                viewMatrix = sometempmat;

                _projectionMatrix = D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.001f, 1000.0f, ProjectionModifier.None).ToMatrix();
                oriProjectionMatrix = _projectionMatrix;
                _projectionMatrix.Transpose();*/
                //IN DEVELOPMENT WIP BY STEVE CHASSÉ
                //IN DEVELOPMENT WIP BY STEVE CHASSÉ
                //IN DEVELOPMENT WIP BY STEVE CHASSÉ


                //IN DEVELOPMENT WIP BY STEVE CHASSÉ, THIS IS THE AB3D.OCULUSWRAP DIRECT11 MIT SAMPLE CODE THAT I CANNOT SEEM TO MAKE IT WORK. THE OBJECTS ARE DRAWN IN THE WRONG ORDER AND APPEAR IN FRONT WHEN THEY SHOULD BE IN THE BACK, AND THE EYES ARE UPSIDE DOWN AND THE TRIANGLES/FACES ARE INVERTED.
                //IN DEVELOPMENT WIP BY STEVE CHASSÉ, THIS IS THE AB3D.OCULUSWRAP DIRECT11 MIT SAMPLE CODE THAT I CANNOT SEEM TO MAKE IT WORK. THE OBJECTS ARE DRAWN IN THE WRONG ORDER AND APPEAR IN FRONT WHEN THEY SHOULD BE IN THE BACK, AND THE EYES ARE UPSIDE DOWN AND THE TRIANGLES/FACES ARE INVERTED.
                //IN DEVELOPMENT WIP BY STEVE CHASSÉ, THIS IS THE AB3D.OCULUSWRAP DIRECT11 MIT SAMPLE CODE THAT I CANNOT SEEM TO MAKE IT WORK. THE OBJECTS ARE DRAWN IN THE WRONG ORDER AND APPEAR IN FRONT WHEN THEY SHOULD BE IN THE BACK, AND THE EYES ARE UPSIDE DOWN AND THE TRIANGLES/FACES ARE INVERTED.
                /*Vector3 eyePos = SharpDX.Vector3.Transform(new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z), originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot).ToVector3();
                Matrix eyeQuaternionMatrix = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X, eyePoses[eyeIndex].Orientation.Y, eyePoses[eyeIndex].Orientation.Z, eyePoses[eyeIndex].Orientation.W));
                Vector3 positionplayer = eyePos + OFFSETPOS;
                Quaternion somequat = SharpDXHelpers.ToQuaternion(eyePoses[eyeIndex].Orientation);
                //Matrix rotationMatrix = Matrix.RotationQuaternion(somequat);
                finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot;
                lookUp = Vector3.Transform(new Vector3(0, -1, 0), finalRotationMatrix).ToVector3();
                lookAt = Vector3.Transform(new Vector3(0, 0, 1), finalRotationMatrix).ToVector3();
                viewPosition = positionplayer - eyePoses[eyeIndex].Position.ToVector3();
                //FIRST/THIRD PERSON VR VIEW. COMMENT THIS PART OUT TO HAVE FIRST PERSON VIEW 
                if (MainWindow.usethirdpersonview == 0)
                {
                    //eyePos = SharpDX.Vector3.Transform(new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z), originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot).ToVector3();
                    //viewPosition = eyePos + OFFSETPOS;
                }
                else if (MainWindow.usethirdpersonview == 1)
                {
                    Quaternion somedirquat1;
                    Quaternion.RotationMatrix(ref tempmatter, out somedirquat1);
                    var dirikvoxelbodyInstanceRight0 = -sc_maths._newgetdirleft(somedirquat1);
                    var dirikvoxelbodyInstanceUp0 = sc_maths._newgetdirup(somedirquat1);
                    var dirikvoxelbodyInstanceForward0 = sc_maths._newgetdirforward(somedirquat1);
                    viewPosition = viewPosition + (dirikvoxelbodyInstanceForward0 * MainWindow.offsetthirdpersonview);
                }
                //FIRST/THIRD PERSON VR VIEW. COMMENT THIS PART OUT TO HAVE FIRST PERSON VIEW 

                viewMatrix = Matrix.LookAtLH(viewPosition, viewPosition + lookAt, lookUp);
                _projectionMatrix = D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.001f, 1000.0f, ProjectionModifier.LeftHanded).ToMatrix();
                oriProjectionMatrix = _projectionMatrix;
                _projectionMatrix.Transpose();*/
                //IN DEVELOPMENT WIP BY STEVE CHASSÉ, THIS IS THE AB3D.OCULUSWRAP DIRECT11 MIT SAMPLE CODE THAT I CANNOT SEEM TO MAKE IT WORK. THE OBJECTS ARE DRAWN IN THE WRONG ORDER AND APPEAR IN FRONT WHEN THEY SHOULD BE IN THE BACK, AND THE EYES ARE UPSIDE DOWN AND THE TRIANGLES/FACES ARE INVERTED.
                //IN DEVELOPMENT WIP BY STEVE CHASSÉ, THIS IS THE AB3D.OCULUSWRAP DIRECT11 MIT SAMPLE CODE THAT I CANNOT SEEM TO MAKE IT WORK. THE OBJECTS ARE DRAWN IN THE WRONG ORDER AND APPEAR IN FRONT WHEN THEY SHOULD BE IN THE BACK, AND THE EYES ARE UPSIDE DOWN AND THE TRIANGLES/FACES ARE INVERTED.
                //IN DEVELOPMENT WIP BY STEVE CHASSÉ, THIS IS THE AB3D.OCULUSWRAP DIRECT11 MIT SAMPLE CODE THAT I CANNOT SEEM TO MAKE IT WORK. THE OBJECTS ARE DRAWN IN THE WRONG ORDER AND APPEAR IN FRONT WHEN THEY SHOULD BE IN THE BACK, AND THE EYES ARE UPSIDE DOWN AND THE TRIANGLES/FACES ARE INVERTED.























                //viewPosition.Y += eyePos.Y;


                /*
                viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);
                Quaternion quatt;
                Quaternion.RotationMatrix(ref viewMatrix, out quatt);
                quatt.Invert();
                Vector3 forwardOVR = sc_maths._newgetdirforward(quatt);
                forwardOVR.Normalize();
                forwardOVR *= 0.15f;
                viewPosition = viewPosition + (forwardOVR);
                viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);*/



                // quatt.Invert();





                /*//THIRD PERSON VR VIEW. COMMENT THIS PART OUT TO HAVE FIRST PERSON VIEW
                Vector3 forwardOVR = sc_maths._getDirection(Vector3.ForwardRH, quatt);
                Vector3 upOVR = sc_maths._getDirection(Vector3.Up, quatt);
                Vector3 rightOVR = sc_maths._getDirection(Vector3.Right, quatt);
                upOVR.Normalize();
                rightOVR.Normalize();
                forwardOVR.Normalize();

                forwardOVR *= -0.5f; // -1.0f
                viewPosition = viewPosition + (forwardOVR * 2.0f); //1.5f // + (upOVR * 0.25f)

                //OCULUS RIFT HEADSET OFFSET CALCULATIONS SO THAT THE HEAD MESH COVERS THE HEADSET IN FIRST PERSON VIEW
                //Matrix sometempmat = ikvoxelbody[0]._player_head[0][0]._arrayOfInstances[0].current_pos;





                //viewPosition = viewPosition + _hmdPoser;




                //MainWindow.MessageBox((IntPtr)0, "0", "sc core systems message", 0);
                if (_can_work_physics == 1)
                {



                    //TO READD IF IT DOESNT WORK
                    //TO READD IF IT DOESNT WORK
                    //TO READD IF IT DOESNT WORK
                    /*
                    if (graphicssec != null)
                    {
                        //MainWindow.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                        try
                        {
                            _sc_jitter_tasks = graphicssec.workOnSomething(_sc_jitter_tasks, viewMatrix, _projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdmatrixRot, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix, someextrapelvismatrix);

                            if (writetobufferchunk == 0)
                            {


                                writetobufferchunk = 1;
                            }

                            _sc_jitter_tasks = graphicssec.sc_write_to_buffer(_sc_jitter_tasks);
                            _sc_jitter_tasks = graphicssec.sccswritescreenassetstobuffer(_sc_jitter_tasks);
                            _sc_jitter_tasks = graphicssec.sccswriteikrigtobuffer(_sc_jitter_tasks, viewMatrix, _projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdmatrixRot, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix);



                        }
                        catch (Exception ex)
                        {
                            MainWindow.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                        }
                    }
                    //TO READD IF IT DOESNT WORK
                    //TO READD IF IT DOESNT WORK
                    //TO READD IF IT DOESNT WORK




                    scgraphicssecpackagemessage.viewMatrix = viewMatrix;
                    scgraphicssecpackagemessage.projectionMatrix = _projectionMatrix;
                    scgraphicssecpackagemessage.originRot = originRot;
                    scgraphicssecpackagemessage.rotatingMatrix = rotatingMatrix;
                    scgraphicssecpackagemessage.hmdmatrixRot = hmdmatrixRot;
                    scgraphicssecpackagemessage.hmd_matrix = hmd_matrix;
                    scgraphicssecpackagemessage.rotatingMatrixForPelvis = rotatingMatrixForPelvis;
                    scgraphicssecpackagemessage.rightTouchMatrix = _rightTouchMatrix;
                    scgraphicssecpackagemessage.leftTouchMatrix = _leftTouchMatrix;
                    scgraphicssecpackagemessage.oriProjectionMatrix = oriProjectionMatrix;
                    scgraphicssecpackagemessage.someextrapelvismatrix = someextrapelvismatrix;
                    scgraphicssecpackagemessage.offsetpos = OFFSETPOS;
                    scgraphicssecpackagemessage.handPoseRight = handPoseRight;
                    scgraphicssecpackagemessage.handPoseLeft = handPoseLeft;
                    //scgraphicssecpackagemessage.scgraphicssec = null;
                    scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;


                    if (eyebufferthreadswtc == 0)
                    {
                        scgraphicssecpackagemessage.scgraphicssec = new sc_graphics_sec();

                        _sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec._sc_create_world_objects(_sc_jitter_tasks);

                        Thread main_thread_update = new Thread(() =>
                        {


                            //sc_graphics_sec graphicssec;



                            int bufferswtc = 0;


                        _thread_looper:

                            if (scgraphicssecpackagemessage.scgraphicssec != null)
                            {
                                scgraphicssecpackagemessage.scgraphicssec.oculuscontrolsNRecordSoundNMousePointer();
                            }

                            //scgraphicssecpackagemessage.scgraphicssec = graphicssec;

                            //MainWindow.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                            try
                            {
                                if (bufferswtc == 0)
                                {
                                    scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workOnSomething(scgraphicssecpackagemessage);
                                    scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);
                                    scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswritescreenassetstobuffer(scgraphicssecpackagemessage.scjittertasks);
                                    scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);


                                    //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);

                                    //bufferswtc = 1;
                                }


                            }
                            catch (Exception ex)
                            {
                                MainWindow.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                            }
                            Thread.Sleep(1);
                            goto _thread_looper;

                            //ShutDown();
                            //ShutDownGraphics();

                        }, 0);

                        main_thread_update.IsBackground = true;
                        main_thread_update.Priority = ThreadPriority.Lowest; //AboveNormal
                        main_thread_update.SetApartmentState(ApartmentState.STA);
                        main_thread_update.Start();

                        /*
                        var _console_worker_task = Task<object[]>.Factory.StartNew((sometaskmsg) =>
                        {
                        loopthread:
                            //MainWindow.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                            try
                            {
                            }
                            catch (Exception ex)
                            {
                                MainWindow.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                            }
                            Thread.Sleep(1);
                            goto loopthread;
                        }, scgraphicssecpackagemessage);


                        eyebufferthreadswtc = 1;
                    }

                    //scgraphicssecpackagemessage.scjittertasks = graphicssec.workonshaders(scgraphicssecpackagemessage);

                    if (scgraphicssecpackagemessage.scgraphicssec != null)
                    {
                        scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);
                    }




                    _sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;




                }

                D3D.result = eyeTexture.SwapTextureSet.Commit();
                D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to commit the swap chain texture.");
            }






            //_ticks_watch.Restart();

            D3D.result = D3D.OVR.SubmitFrame(D3D.sessionPtr, 0L, IntPtr.Zero, ref D3D.layerEyeFov);

            //Console.WriteLine(_ticks_watch.Elapsed.Milliseconds);

            D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to submit the frame of the current layers.");
            D3D.DeviceContext.CopyResource(D3D.mirrorTextureD3D, D3D.BackBuffer);
            D3D.SwapChain.Present(0, PresentFlags.None);*/
            }
            catch (Exception ex)
            {
                MainWindow.MessageBox((IntPtr)0, "" + ex.ToString(), "sc core systems message", 0);
            }

            //Console.WriteLine(_ticks_watch.Elapsed.Ticks);


            //writetobufferchunk = 1;
















            /*
            if (MainWindow.useArduinoOVRTouchKeymapper == 1)
            {
                object somearduinodata = (object)_sc_jitter_tasks[0][0]._world_data[1];
                string arduinomsgcom3 = (string)somearduinodata;
                //Console.Write(arduinomsgcom3);


                //if (!SC_console_directx.serialPort.IsOpen)
                //{
                //    SC_console_directx.serialPort.Open();
                //}

                //SC_console_directx.serialPort.read


                if (arduinomsgcom3.Length > 0)
                {
                    var someSplitString = arduinomsgcom3.Split('|');


                    arduinoDIYOculusTouchArrayOfData = new int[someSplitString.Length];
                    for (int si = 0; si < someSplitString.Length; si++) //someSplitString.Length
                    {
                        if (someSplitString[si] != null)
                        {
                            int.TryParse(someSplitString[si], out arduinoDIYOculusTouchArrayOfData[si]);
                        }


                    }
                }

                //if (_out_of_bounds_oculus_rift == 1)
                {

                    //Console.WriteLine("x:" + arduinoDIYOculusTouchArrayOfData[7] + "/y:" + arduinoDIYOculusTouchArrayOfData[8]);


                    //Console.WriteLine("x:" + arduinoDIYOculusTouchArrayOfData[7]);
                    if (arduinoDIYOculusTouchArrayOfData[7] < 498 || arduinoDIYOculusTouchArrayOfData[7] >= 564 || arduinoDIYOculusTouchArrayOfData[8] < 499 || arduinoDIYOculusTouchArrayOfData[8] >= 565)
                    {
                        if (arduinoDIYOculusTouchArrayOfData[7] < 507 || arduinoDIYOculusTouchArrayOfData[8] < 499 || arduinoDIYOculusTouchArrayOfData[7] >= 564 || arduinoDIYOculusTouchArrayOfData[8] >= 565)
                        {
                            var tempDataArduinoOVRTouchY = arduinoDIYOculusTouchArrayOfData[7];
                            if (arduinoDIYOculusTouchArrayOfData[7] < 498)
                            {
                                tempDataArduinoOVRTouchY = Math.Abs(498 - arduinoDIYOculusTouchArrayOfData[7]);
                                if (RotationOriginX < -1) //360?
                                {
                                    RotationOriginX = -1;//360?
                                }

                                RotationOriginX -= 0.1f;
                            }
                            else if (arduinoDIYOculusTouchArrayOfData[7] >= 564)
                            {
                                tempDataArduinoOVRTouchY = -Math.Abs(1023 - arduinoDIYOculusTouchArrayOfData[7]);
                                if (RotationOriginX > 1)//360?
                                {
                                    RotationOriginX = 1;//360?
                                }
                                RotationOriginX += 0.1f;
                            }


                            //Console.WriteLine("RotationOriginX:" + RotationOriginX);

                            //public static double RotationOriginY { get; set; }
                            //public static double RotationOriginX { get; set; }
                            //public static double RotationOriginZ { get; set; }

                            RotationGrabbedYOff = 0;
                            RotationGrabbedXOff = 0;
                            RotationGrabbedZOff = 0;

                            RotationGrabbedSwtch = 1;

                            thumbstickIsRight = RotationOriginX;// tempDataArduinoOVRTouchX;
                            thumbstickIsUp = RotationOriginY;// tempDataArduinoOVRTouchY;

                            //newRotationY;

                            float rotMax = 25;



                            //float rot0 = (float)((180 / Math.PI) * (Math.Atan(thumbstickIsUp / thumbstickIsRight))); // opp/adj
                            //float rot1 = (float)((180 / Math.PI) * (Math.Atan(thumbstickIsRight / thumbstickIsUp)));

                            float newRotY = thumbstickIsRight * (rotMax) * -1;

                            RotationY = newRotY;
                            float someRotForPelvis = (float)RotationY;

                            if (RotationY > rotMax * 0.99f)
                            {
                                RotationY = rotMax * 0.99f;
                                //RotationY4Pelvis = rotMax * 0.99f;
                                //RotationY4PelvisTwo = rotMax * 0.99f;
                                //RotationGrabbedY = rotMax * 0.99f;
                                RotationY4Pelvis += speedRot * 100;
                                RotationY4PelvisTwo += speedRot * 100;
                                RotationGrabbedY += speedRot * 100;
                            }

                            if (RotationY < -rotMax * 0.99f)
                            {
                                RotationY = -rotMax * 0.99f;

                                //RotationY4Pelvis = -rotMax * 0.99f;
                                //RotationY4PelvisTwo = -rotMax * 0.99f;
                                //RotationGrabbedY = -rotMax * 0.99f;
                                RotationY4Pelvis += -speedRot * 100;
                                RotationY4PelvisTwo += -speedRot * 100;
                                RotationGrabbedY += -speedRot * 100;
                            }

                            rotMax = 25;
                            float newRotX = thumbstickIsUp * (rotMax) * -1;
                            RotationX = newRotX;

                            if (RotationX > rotMax * 0.99f)
                            {
                                RotationX = rotMax * 0.99f;
                            }

                            if (RotationX < -rotMax * 0.99f)
                            {
                                RotationX = -rotMax * 0.99f;
                            }

                            //float pitch = (float)(RotationX * 0.0174532925f);
                            //float yaw = (float)(RotationY * 0.0174532925f);
                            //float roll = (float)(RotationZ * 0.0174532925f);


                            float pitch = (float)(Math.PI * (RotationX) / 180.0f);
                            float yaw = (float)(Math.PI * (RotationY) / 180.0f);
                            float roll = (float)(Math.PI * (RotationZ) / 180.0f);

                            rotatingMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                            //pitch = (float)(RotationX4Pelvis * 0.0174532925f);
                            //yaw = (float)(RotationY4Pelvis * 0.0174532925f);
                            //roll = (float)(RotationZ4Pelvis * 0.0174532925f);

                            pitch = (float)(Math.PI * (RotationX4Pelvis) / 180.0f);
                            yaw = (float)(Math.PI * (RotationY4Pelvis) / 180.0f);
                            roll = (float)(Math.PI * (RotationZ4Pelvis) / 180.0f);


                            rotatingMatrixForPelvis = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                            if (_has_grabbed_right_swtch == 2)
                            {
                                _swtch_hasRotated = 1;
                            }

                            pitch = (float)(Math.PI * (RotationGrabbedX) / 180.0f);
                            yaw = (float)(Math.PI * (RotationGrabbedY) / 180.0f);
                            roll = (float)(Math.PI * (RotationGrabbedZ) / 180.0f);


                            rotatingMatrixForGrabber = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);



                        }

                        if (arduinoDIYOculusTouchArrayOfData[8] < 499 || arduinoDIYOculusTouchArrayOfData[8] >= 565)
                        {




                            var tempDataArduinoOVRTouchX = arduinoDIYOculusTouchArrayOfData[8];
                            if (arduinoDIYOculusTouchArrayOfData[8] < 499)
                            {
                                tempDataArduinoOVRTouchX = -Math.Abs(499 - arduinoDIYOculusTouchArrayOfData[8]);
                                if (RotationOriginY < -1) //360?
                                {
                                    RotationOriginY = -1;//360?
                                }

                                RotationOriginY += 0.1f;
                            }
                            else if (arduinoDIYOculusTouchArrayOfData[8] >= 565)
                            {
                                tempDataArduinoOVRTouchX = Math.Abs(1023 - arduinoDIYOculusTouchArrayOfData[8]);
                                if (RotationOriginY > 1)//360?
                                {
                                    RotationOriginY = 1;//360?
                                }
                                RotationOriginY -= 0.1f;
                            }


                            //Console.WriteLine("RotationOriginY:" + RotationOriginY);

                            //public static double RotationOriginY { get; set; }
                            //public static double RotationOriginX { get; set; }
                            //public static double RotationOriginZ { get; set; }

                            RotationGrabbedYOff = 0;
                            RotationGrabbedXOff = 0;
                            RotationGrabbedZOff = 0;

                            RotationGrabbedSwtch = 1;

                            thumbstickIsRight = RotationOriginY;// tempDataArduinoOVRTouchX;
                            thumbstickIsUp = RotationOriginX;// tempDataArduinoOVRTouchX;

                            //newRotationY;

                            rotMax = 25;

                            //float rot0 = (float)((180 / Math.PI) * (Math.Atan(thumbstickIsUp / thumbstickIsRight))); // opp/adj
                            //float rot1 = (float)((180 / Math.PI) * (Math.Atan(thumbstickIsRight / thumbstickIsUp)));

                            newRotX = thumbstickIsRight * (rotMax) * -1;

                            RotationX = newRotX;
                            someRotForPelvis = (float)RotationX;

                            if (RotationX > rotMax * 0.99f)
                            {
                                RotationX = rotMax * 0.99f;
                                //RotationY4Pelvis = rotMax * 0.99f;
                                //RotationY4PelvisTwo = rotMax * 0.99f;
                                //RotationGrabbedY = rotMax * 0.99f;
                                //RotationX4Pelvis += speedRot * 100;
                                //RotationX4PelvisTwo += speedRot * 100;
                                //RotationGrabbedX += speedRot * 100;
                            }

                            if (RotationX < -rotMax * 0.99f)
                            {
                                RotationX = -rotMax * 0.99f;

                                //RotationY4Pelvis = -rotMax * 0.99f;
                                //RotationY4PelvisTwo = -rotMax * 0.99f;
                                //RotationGrabbedY = -rotMax * 0.99f;
                                //RotationX4Pelvis += -speedRot * 100;
                                //RotationX4PelvisTwo += -speedRot * 100;
                                //RotationGrabbedX += -speedRot * 100;
                            }

                            rotMax = 25;
                            newRotY = thumbstickIsUp * (rotMax) * -1;
                            RotationY = newRotY;

                            if (RotationY > rotMax * 0.99f)
                            {
                                RotationY = rotMax * 0.99f;
                            }

                            if (RotationY < -rotMax * 0.99f)
                            {
                                RotationY = -rotMax * 0.99f;
                            }

                            //float pitch = (float)(RotationX * 0.0174532925f);
                            //float yaw = (float)(RotationY * 0.0174532925f);
                            //float roll = (float)(RotationZ * 0.0174532925f);


                            pitch = (float)(Math.PI * (RotationX) / 180.0f);
                            yaw = (float)(Math.PI * (RotationY) / 180.0f);
                            roll = (float)(Math.PI * (RotationZ) / 180.0f);

                            rotatingMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                            //pitch = (float)(RotationX4Pelvis * 0.0174532925f);
                            //yaw = (float)(RotationY4Pelvis * 0.0174532925f);
                            //roll = (float)(RotationZ4Pelvis * 0.0174532925f);

                            pitch = (float)(Math.PI * (RotationX4Pelvis) / 180.0f);
                            yaw = (float)(Math.PI * (RotationY4Pelvis) / 180.0f);
                            roll = (float)(Math.PI * (RotationZ4Pelvis) / 180.0f);


                            rotatingMatrixForPelvis = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                            if (_has_grabbed_right_swtch == 2)
                            {
                                _swtch_hasRotated = 1;
                            }

                            pitch = (float)(Math.PI * (RotationGrabbedX) / 180.0f);
                            yaw = (float)(Math.PI * (RotationGrabbedY) / 180.0f);
                            roll = (float)(Math.PI * (RotationGrabbedZ) / 180.0f);


                            rotatingMatrixForGrabber = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                        }
                    }
                    else
                    {
                        RotationOriginX = 0;
                        RotationOriginY = 0;
                        RotationOriginZ = 0;

                        RotationGrabbedYOff = RotationY4Pelvis;
                        RotationGrabbedXOff = RotationX4Pelvis;
                        RotationGrabbedZOff = RotationZ4Pelvis;

                        if (RotationGrabbedSwtch == 1)
                        {
                            RotationX4PelvisTwo = 0;
                            RotationY4PelvisTwo = 0;
                            RotationZ4PelvisTwo = 0;
                            RotationGrabbedSwtch = 0;
                        }

                        //RotationGrabbedY = 0;
                        //RotationGrabbedX = 0;
                        //RotationGrabbedZ = 0;

                        if (arduinoDIYOculusTouchArrayOfData[7] >= 498 && arduinoDIYOculusTouchArrayOfData[7] < 533) //&& arduinoDIYOculusTouchArrayOfData[8] >=499  && arduinoDIYOculusTouchArrayOfData[8] <= 560
                        {
                            if (_swtch_hasRotated == 1)
                            {

                                _swtch_hasRotated = 2;
                            }
                            //_swtch_hasRotated = 0;

                            RotationX = 0;
                            RotationY = 0;
                            RotationZ = 0;

                            float pitch = (float)(RotationX * 0.0174532925f);
                            float yaw = (float)(RotationY * 0.0174532925f);
                            float roll = (float)(RotationZ * 0.0174532925f);

                            rotatingMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                            pitch = (float)(RotationX4Pelvis * 0.0174532925f);
                            yaw = (float)(RotationY4Pelvis * 0.0174532925f);
                            roll = (float)(RotationZ4Pelvis * 0.0174532925f);

                            rotatingMatrixForPelvis = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                            pitch = (float)(RotationGrabbedX * 0.0174532925f);
                            yaw = (float)(RotationGrabbedY * 0.0174532925f);
                            roll = (float)(RotationGrabbedZ * 0.0174532925f);

                            rotatingMatrixForGrabber = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);
                            if (_swtch_hasRotated == 0)
                            {
                                _sec_logic_swtch_grab = 0;
                            }
                        }
                        else
                        {

                        }
                    }
                }


                Matrix tempmat = hmd_matrix * rotatingMatrixForPelvis * hmdmatrixRot;
                Quaternion otherQuat;
                Quaternion.RotationMatrix(ref tempmat, out otherQuat);

                Vector3 direction_feet_forward;
                Vector3 direction_feet_right;
                Vector3 direction_feet_up;

                direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);

                //Console.WriteLine("x:" + arduinoDIYOculusTouchArrayOfData[10] + "/y:" + arduinoDIYOculusTouchArrayOfData[11]);
                if (arduinoDIYOculusTouchArrayOfData != null)
                {
                    if (arduinoDIYOculusTouchArrayOfData[10] != null)
                    {
                        //Console.WriteLine("x:"+arduinoDIYOculusTouchArrayOfData[10]);
                        //Console.WriteLine("x:" + arduinoDIYOculusTouchArrayOfData[7] + "/y:" + arduinoDIYOculusTouchArrayOfData[8]);
                        //Console.WriteLine();
                        if (arduinoDIYOculusTouchArrayOfData[10] >= 550)
                        {
                            movePos += direction_feet_right * speedPos * arduinoDIYOculusTouchArrayOfData[10] * speedPosArduino;
                        }

                        if (arduinoDIYOculusTouchArrayOfData[10] < 507)
                        {
                            var tempData = -Math.Abs(1027 - arduinoDIYOculusTouchArrayOfData[10]);
                            movePos += direction_feet_right * speedPos * tempData * speedPosArduino;
                        }
                    }

                    if (arduinoDIYOculusTouchArrayOfData.Length >= 10)
                    {

                    }

                    if (arduinoDIYOculusTouchArrayOfData[11] != null)
                    {
                        //Console.WriteLine("y:" + arduinoDIYOculusTouchArrayOfData[11]);
                        //Console.WriteLine("TEST");
                        //Console.WriteLine("x:" + arduinoDIYOculusTouchArrayOfData[7] + "/y:" + arduinoDIYOculusTouchArrayOfData[8]);
                        //Console.WriteLine();
                        if (arduinoDIYOculusTouchArrayOfData[11] >= 565) // average 506 or 
                        {
                            movePos += direction_feet_forward * speedPos * -arduinoDIYOculusTouchArrayOfData[11] * speedPosArduino;
                        }

                        if (arduinoDIYOculusTouchArrayOfData[11] < 500)
                        {
                            var tempData = Math.Abs(1027 - arduinoDIYOculusTouchArrayOfData[11]);
                            movePos += direction_feet_forward * speedPos * tempData * speedPosArduino;
                        }
                    }
                    if (arduinoDIYOculusTouchArrayOfData.Length >= 11)
                    {

                    }
                }
            }
            OFFSETPOS = originPos + movePos;// + _hmdPoser; //_hmdPoser*/


            //calibrate device todo. 
            //1. ask the player to budge their thumbsticks in all directions so that it goes back to the middle idle position.
            //2. since the thumbstick in most cases will fall back to their middle idle position when they aren't broken, i can set an average, if the exercise is repeated 1 to 9 times and set the average as the origin IDLE position.
            //3. When the origin Idle position is set for the x and y axis, adding a +-5 difference so that minuscule changes over a certain period of frame is also set as the IDLE position average min/max values where it should be set as IDLE.
            //4. add if/else so that minuscule movement of the player isn't confused by the IDLE position, especially if everytime minuscule movement of the thumbstick is engaged and increases of +1 over 10 frames when the player uses the left thumbstick to strafe left, the arduino oculus touch emulator data would show the number from 513 to 515 and lowering to 0
            //   and if the player would turn right the data would show 513 to 515 right up to 1027-1023 max... So if minuscule changes are never counted, then the player would reach the far left or right of their thumbstick joystick and never even
            //   have strafe a single unit ingame, which in turns returns ranting and raging players which is never cool.










            /*
            for (int i = 0; i < 3;)
            {
                _failed_screen_capture = 0;
                try
                {
                    _desktopFrame = _desktopDupe.ScreenCapture(0);
                }
                catch (Exception ex)
                {
                    _desktopDupe = new sccssharpdxscreencapture(0, 0, D3D.device);
                    _failed_screen_capture = 1;
                }
                i++;
                if (_failed_screen_capture == 0)
                {
                    break;
                }
            }*/



            //var dataBox = D3D.device.ImmediateContext.MapSubresource(_desktopFrame.somebitmapforarduino, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);


            if (MainWindow.useSendScreenToArduino == 1)
            {
                if (framecounterforpctoarduinoscreen >= framecounterforpctoarduinoscreenMax)
                {
                    if (serialPort == null)
                    {
                        //Console.WriteLine("the serial port is null");

                    }
                    if (serialPort != null)
                    {
                        string somemsgforarduino = framecounterforpctoarduinoscreenFinal + "";
                        //serialPort.Write(somemsgforarduino);
                        //serialPort.DataBits = 8;
                        //serialPort.StopBits = System.IO.Ports.StopBits.One;
                        //serialPort.Parity = System.IO.Ports.Parity.None;
                        //serialPort.Open();
                        serialPort.WriteLine(somemsgforarduino);
                        //serialPort.Close();
                        //serialPort.
                        framecounterforpctoarduinoscreenFinal++;
                    }
                    /*if (_desktopFrame.somebitmapforarduino != null)
                    {
                        Console.WriteLine("the _desktopFrame.somebitmapforarduino is != null");
                    }*/

                    if (serialPort != null && _desktopFrame.somebitmapforarduino != null)
                    {
                        Console.WriteLine("test");

                        int memoryBitmapStride = _desktopFrame.somebitmapforarduino.Width * 4;

                        int columns = _desktopFrame.somebitmapforarduino.Width;
                        int rows = _desktopFrame.somebitmapforarduino.Height;

                        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, _desktopFrame.somebitmapforarduino.Width, _desktopFrame.somebitmapforarduino.Height);
                        var somebitmapdata = _desktopFrame.somebitmapforarduino.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, _desktopFrame.somebitmapforarduino.PixelFormat);
                        var bitmapscanptr = somebitmapdata.Scan0;

                        var _bytesTotal = Math.Abs(somebitmapdata.Stride) * somebitmapdata.Height;

                        byte[] somebytearray = new byte[_bytesTotal];

                        for (int y = 0; y < _desktopFrame.somebitmapforarduino.Height; y++)
                        {
                            somebytearray = new byte[_bytesTotal];
                            Marshal.Copy(bitmapscanptr + y * memoryBitmapStride, somebytearray, y * memoryBitmapStride, memoryBitmapStride);

                            string somemsgforarduino = "";

                            for (int i = 0; i < _bytesTotal; i++)
                            {
                                somemsgforarduino += somebytearray[i];
                            }
                            serialPort.Write(somemsgforarduino);

                        }

                        _desktopFrame.somebitmapforarduino.UnlockBits(somebitmapdata);
                    }
                    framecounterforpctoarduinoscreen = 0;
                }
                framecounterforpctoarduinoscreen++;
            }





            /*
            for (int i = 0; i < _desktopFrame.somebitmapforarduino.Height; i++)
            {
                IntPtr interptr = Marshal.UnsafeAddrOfPinnedArrayElement(_desktopFrame.somebitmapforarduino,0);// dataBox.DataPointer;

                // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (_width * 4)
                if (dataBox.RowPitch == memoryBitmapStride)
                {
                    // Stride is the same
                    Marshal.Copy(interptr, _textureByteArray, 0, _bytesTotal);
                }
                else
                {
                    // Stride not the same - copy line by line
                    for (int y = 0; y < _height; y++)
                    {
                        Marshal.Copy(interptr + y * dataBox.RowPitch, _textureByteArray, y * memoryBitmapStride, memoryBitmapStride);
                    }
                }

                _device.ImmediateContext.UnmapSubresource(_texture2D, 0);
                DeleteObject(interptr);

                string somestring = 
                for ()
                {

                }

                serialPort.Write();
            }*/



            //SC_console_directx.serialPort.Write();


            //###SC start physics on frame 1 instead of 0
            _can_work_physics = 1;
            _can_work_physics_objects = 1;
            //###SC start physics on frame 1 instead of 0
            return _sc_jitter_tasks;
        }

        int framecounterforpctoarduinoscreenFinal = 0;
        int framecounterforpctoarduinoscreen = 0;
        int framecounterforpctoarduinoscreenMax = 150;

        int writetobufferchunk = 0;


        int writetobufferikrig = 0;




        Stopwatch _ticks_watch = new Stopwatch();
        public int _can_work_physics = 0;




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








        /*public KeyboardState _KeyboardState;
        public SharpDX.DirectInput.Keyboard _Keyboard;
        DirectInput directInput;*/

        /*private bool ReadKeyboard()
        {
            directInput = new DirectInput();

            _Keyboard = new Keyboard(directInput);

            //Acquire the joystick
            _Keyboard.Properties.BufferSize = 128;

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
                { 
                
                }
                return true;
            }

            if (resultCode == SharpDX.DirectInput.ResultCode.Ok)
            {
                return true;
            }

            return false;
        }*/
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
    }
}

