using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Ab3d.DXEngine;
using Ab3d.OculusWrap;
using Ab3d.DXEngine.OculusWrap;
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

using SC_message_object = _sc_message_object._sc_message_object;
using SC_message_object_jitter = _sc_message_object._sc_message_object_jitter;

namespace SCCoreSystems
{
    public class SC_Console_GRAPHICS
    {
        public static SharpDX.Vector3 originPos = new SharpDX.Vector3(0, 1, 3);
        public static SharpDX.Vector3 originPosScreen = new SharpDX.Vector3(0, 1, -0.25f);

        float disco_sphere_rot_speed = 0.5f;
        float force_4_voxel = 0.0015f;
        float force_4_cubes = 0.0015f;
        float force_4_screen = 0.0015f;

        float speedRot = 0.15f;
        float speedPos = 0.05f;

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
            _delta_timer_frame = (float)Math.Abs((timeStopWatch01.Elapsed.Ticks - timeStopWatch00.Elapsed.Ticks));

            time2 = DateTime.Now;
            _delta_timer_time = (time2.Ticks - time1.Ticks); //100000000f
            //time1 = time2;

            deltaTime = (float)Math.Abs(_delta_timer_frame - _delta_timer_time) * 1000;

            //time1 = time2;
            await Task.Delay(1);
            Thread.Sleep(timeOut);
            _swtch_counter_00++;
            _swtch_counter_01++;
            _swtch_counter_02++;

            goto _threadLoop;
        }
        public SC_Console_GRAPHICS(sc_graphics_main _graphics)
        {
            _the_graphics = _graphics;
        }

        public void ShutDown()
        {

        }

        Matrix rotatingMatrixForGrabber = Matrix.Identity;
        int _sec_logic_swtch_grab = 0;

        int _swtch_hasRotated = 0;
        int _has_grabbed_right_swtch = 0;
        public static double RotationY { get; set; }
        public static double RotationX { get; set; }
        public static double RotationZ { get; set; }
        float thumbstickIsRight;
        float thumbstickIsUp;


        double RotationY4Pelvis { get; set; }
        double RotationX4Pelvis { get; set; }
        double RotationZ4Pelvis { get; set; }

        double RotationGrabbedY { get; set; }
        double RotationGrabbedX { get; set; }
        double RotationGrabbedZ { get; set; }


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
        Posef handPoseLeft;
        SharpDX.Quaternion _leftTouchQuat;
        Posef handPoseRight;
        SharpDX.Quaternion _rightTouchQuat;
        Matrix _leftTouchMatrix = Matrix.Identity;
        Matrix _rightTouchMatrix = Matrix.Identity;
        //OCULUS TOUCH SETTINGS 

        float offsetPosX = 0.0f;
        float offsetPosY = 0.0f;
        float offsetPosZ = 0.0f;

        double displayMidpoint;
        TrackingState trackingState;
        Posef[] eyePoses;
        EyeType eye;
        EyeTexture eyeTexture;
        bool latencyMark = false;
        TrackingState trackState;
        PoseStatef poseStatefer;
        Posef hmdPose;
        Quaternionf hmdRot;
        Vector3 _hmdPoser;
        Quaternion _hmdRoter;

        Vector3 intersectPointRight;
        Vector3 intersectPointLeft;
        Matrix final_hand_pos_right_locked;
        Matrix final_hand_pos_left_locked;

        _sc_camera Camera;

        int _failed_screen_capture = 0;
        public static DShaderManager _shaderManager;
        public static int _can_work_physics_objects = 0;

        public static IntPtr HWND;
        SCCoreSystems.sc_core.sc_system_configuration _configuration;
        sc_console.sc_console_writer _currentWriter;
        public static SC_SharpDX_ScreenFrame _desktopFrame;

        float xq;//= otherQuat.X;
        float yq;//= otherQuat.Y;
        float zq;//= otherQuat.Z;
        float wq;//= otherQuat.W;
        float pitcha;//= (float) Math.Atan2(2 * yq* wq - 2 * xq* zq, 1 - 2 * yq* yq - 2 * zq* zq); //(float)(180 / Math.PI)
        float yawa;//= (float) Math.Atan2(2 * yq* wq - 2 * xq* zq, 1 - 2 * yq* yq - 2 * zq* zq); //(float)(180 / Math.PI) *
        float rolla;// = (float) Math.Atan2(2 * yq* wq - 2 * xq* zq, 1 - 2 * yq* yq - 2 * zq* zq); // (float)(180 / Math.PI) *
        float hyp;// = diffNormPosY / Math.Cos(pitcha);
    
        int textureIndex;
        SharpDX.Vector3 eyePos;
        SharpDX.Matrix eyeQuaternionMatrix;
        SharpDX.Matrix finalRotationMatrix;
        Vector3 lookUp;
        Vector3 lookAt;
        Vector3 viewPosition;
        Matrix viewMatrix;
        Matrix _projectionMatrix;
        public static Vector3 OFFSETPOS;

        public static SharpDX.Vector3 movePos = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Matrix originRot = SharpDX.Matrix.Identity;

        SharpDX.Matrix rotatingMatrixForPelvis = SharpDX.Matrix.Identity;
        public static SharpDX.Matrix rotatingMatrix = SharpDX.Matrix.Identity;
        float r = 0;
        float g = 0;
        float b = 0;
        float a = 1;

        Matrix WorldMatrix = Matrix.Identity;
        public static SC_SharpDX_ScreenCapture _desktopDupe;


        sc_graphics_main _main_graph;
        public static DateTime startTime;

        public SC_message_object_jitter[][]  Initialize(SC_message_object_jitter[][] _sc_jitter_tasks, SCCoreSystems.sc_core.sc_system_configuration configuration, IntPtr hwnd, sc_console.sc_console_writer _writer, sc_graphics_main main_graph)
        {
            try
            {
                startTime = DateTime.Now;
                _main_graph = main_graph;


                HWND = hwnd;

                _configuration = configuration;

                _currentWriter = _writer;


                ReadKeyboard();
                //Camera = new DCamera();

                //swtch_for_last_pos = new int[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][];

                //RAYCAST STUFF
                //_some_reset_for_applying_force = new int[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][];
                //_some_last_frame_vector = new JVector[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];
                //_some_last_frame_rigibodies = new RigidBody[Program._physics_engine_instance_x * Program._physics_engine_instance_y * Program._physics_engine_instance_z][][];


                DoWork(0);


                Camera = new _sc_camera();

                _shaderManager = new DShaderManager();
                _shaderManager.Initialize(Program.D3D.Device, HWND);

                _desktopDupe = new SC_SharpDX_ScreenCapture(0, 0, Program.D3D.device);

            }
            catch
            {

            }
            return _sc_jitter_tasks;
        }





        public SC_message_object_jitter[][] FrameVRTWO(SC_message_object_jitter[][] _sc_jitter_tasks)
        {
            if (_shaderManager != null)
            {
                // Render the graphics scene.
                try
                {
                    _sc_jitter_tasks = _FrameVRTWO(_sc_jitter_tasks);
                }
                catch (Exception ex)
                {
                    Program.MessageBox((IntPtr)0, "" + ex.ToString(), "sc core systems message", 0);
                }
            }
            else
            {
                //MessageBox((IntPtr)0, "" + ex.ToString(), "sc core systems message", 0);
            }
            return _sc_jitter_tasks;
        }

        int _last_frame_init = 0;

        private unsafe SC_message_object_jitter[][] _FrameVRTWO(SC_message_object_jitter[][] _sc_jitter_tasks)
        {
            //HEADSET POSITION
            displayMidpoint = Program.D3D.OVR.GetPredictedDisplayTime(Program.D3D.sessionPtr, 0);
            trackingState = Program.D3D.OVR.GetTrackingState(Program.D3D.sessionPtr, displayMidpoint, true);
            latencyMark = false;
            trackState = Program.D3D.OVR.GetTrackingState(Program.D3D.sessionPtr, 0.0f, latencyMark);
            poseStatefer = trackState.HeadPose;
            hmdPose = poseStatefer.ThePose;
            hmdRot = hmdPose.Orientation;
            _hmdPoser = new Vector3(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);
            _hmdRoter = new Quaternion(hmdPose.Orientation.X, hmdPose.Orientation.Y, hmdPose.Orientation.Z, hmdPose.Orientation.W);
            //SET CAMERA POSITION
            Camera.SetPosition(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);
            Quaternion quatter = new Quaternion(hmdRot.X, hmdRot.Y, hmdRot.Z, hmdRot.W);
            Vector3 oculusRiftDir = sc_maths._getDirection(Vector3.ForwardRH, quatter);

            Matrix hmd_matrix;
            Matrix.RotationQuaternion(ref quatter, out hmd_matrix);

            //TOUCH CONTROLLER RIGHT
            resultsRight = Program.D3D.OVR.GetInputState(Program.D3D.sessionPtr, Program.D3D.controllerTypeRTouch, ref Program.D3D.inputStateRTouch);
            buttonPressedOculusTouchRight = Program.D3D.inputStateRTouch.Buttons;
            thumbStickRight = Program.D3D.inputStateRTouch.Thumbstick;
            handTriggerRight = Program.D3D.inputStateRTouch.HandTrigger;
            indexTriggerRight = Program.D3D.inputStateRTouch.IndexTrigger;
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
            resultsLeft = Program.D3D.OVR.GetInputState(Program.D3D.sessionPtr, Program.D3D.controllerTypeLTouch, ref Program.D3D.inputStateLTouch);
            buttonPressedOculusTouchLeft = Program.D3D.inputStateLTouch.Buttons;
            thumbStickLeft = Program.D3D.inputStateLTouch.Thumbstick;
            handTriggerLeft = Program.D3D.inputStateLTouch.HandTrigger;
            indexTriggerLeft = Program.D3D.inputStateLTouch.IndexTrigger;
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


            /*

            //FOR WHEN THERE IS MY PHYSICS DESKTOP SCREEN

            Quaternion _quat_screen00;

            Matrix mater = worldMatrix_instances_screens[0][0][0];
            Quaternion.RotationMatrix(ref mater, out _quat_screen00);
            var screenNormal = sc_maths._getDirection(Vector3.ForwardRH, _quat_screen00);
            screenNormal.Normalize();

            var planer = new Plane(new Vector3(mater.M41, mater.M42, mater.M43), screenNormal);
            var screen_mat = worldMatrix_instances_screens[0][0][0];
            var somematroxer2 = screen_mat;
            Quaternion _quat_screen;
            Quaternion.RotationMatrix(ref screen_mat, out _quat_screen);
            screenNormal = sc_maths._getDirection(Vector3.ForwardRH, _quat_screen);
            screenNormal.Normalize();
            planer = new Plane(new Vector3(screen_mat.M41, screen_mat.M42, screen_mat.M43), screenNormal);

            //FOR SCREEN COLLISION DETECTION
            var centerPosRight = new Vector3(final_hand_pos_right_locked.M41, final_hand_pos_right_locked.M42, final_hand_pos_right_locked.M43);
            Quaternion.RotationMatrix(ref final_hand_pos_right_locked, out _rightTouchQuat);
            var rayDirRight = sc_maths._getDirection(Vector3.ForwardRH, _rightTouchQuat);
            var someRay = new Ray(centerPosRight, rayDirRight);
            var intersecter = someRay.Intersects(ref planer, out intersectPointRight);
            var _ray_dir_right = centerPosRight - intersectPointRight;
            var _length_of_ray_right = (_ray_dir_right).Length();

            var centerPosLeft = new Vector3(final_hand_pos_left_locked.M41, final_hand_pos_left_locked.M42, final_hand_pos_left_locked.M43);
            Quaternion.RotationMatrix(ref final_hand_pos_left_locked, out _leftTouchQuat);
            var rayDirLeft = sc_maths._getDirection(Vector3.ForwardRH, _leftTouchQuat);
            var someRayLeft = new Ray(centerPosLeft, rayDirLeft);
            var intersecterLeft = someRayLeft.Intersects(ref planer, out intersectPointLeft);
            var _ray_dir_left = centerPosLeft - intersectPointLeft;
            var _length_of_ray_left = (_ray_dir_left).Length();*/






            for (int i = 0; i < 3;)
            {
                _failed_screen_capture = 0;
                try
                {
                    _desktopFrame = _desktopDupe.ScreenCapture(0);
                }
                catch (Exception ex)
                {
                    _desktopDupe = new SC_SharpDX_ScreenCapture(0, 0, Program.D3D.device);
                    _failed_screen_capture = 1;
                }
                i++;
                if (_failed_screen_capture == 0)
                {
                    break;
                }
            }




            
            //if (_out_of_bounds_oculus_rift == 1)
            {
                if (thumbStickRight[1].X < 0 || thumbStickRight[1].X > 0 || thumbStickRight[1].Y < 0 || thumbStickRight[1].Y > 0)
                {
                    if (thumbStickRight[1].X < 0 && thumbStickRight[1].Y < 0 || thumbStickRight[1].X < 0 && thumbStickRight[1].Y > 0)
                    {
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

            Quaternion otherQuat;
            Quaternion.RotationMatrix(ref rotatingMatrixForPelvis, out otherQuat);


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


            OFFSETPOS = originPos + movePos;



            //START OF PHYSICS ENGINE STEPS
            if (_start_background_worker_01 == 0)
            {
                if (_can_work_physics == 1)
                {

                    BackgroundWorker threaders = new BackgroundWorker();
                    threaders.DoWork += (object sender, DoWorkEventArgs argers) =>
                    {                       
                        //Program.MessageBox((IntPtr)0, "threadstart succes", "sc core systems message", 0);
                        Stopwatch _this_thread_ticks_01 = new Stopwatch();
                        _this_thread_ticks_01.Start();

                    _thread_looper:

                        for (int xx = 0; xx < Program._physics_engine_instance_x; xx++)
                        {
                            for (int yy = 0; yy < Program._physics_engine_instance_y; yy++)
                            {
                                for (int zz = 0; zz < Program._physics_engine_instance_z; zz++)
                                {
                                    var indexer00 = xx + Program._physics_engine_instance_x * (yy + Program._physics_engine_instance_y * zz);

                                    try
                                    {
                                        for (int x = 0; x < Program.world_width; x++)
                                        {
                                            for (int y = 0; y < Program.world_height; y++)
                                            {
                                                for (int z = 0; z < Program.world_depth; z++)
                                                {
                                                    var indexer01 = x + Program.world_width * (y + Program.world_height * z);
                                                    object _some_data_00 = (object)_sc_jitter_tasks[indexer00][indexer01]._world_data[0];
                                                    World _jitter_world = (World)_some_data_00;

                                                    if (_jitter_world != null)
                                                    {
                                                        if (deltaTime > 1.0f * 0.01f)
                                                        {
                                                            deltaTime = 1.0f * 0.01f;
                                                        }
                                                        if (deltaTime < 0.01f * 0.01f)
                                                        {
                                                            deltaTime = 1.0f * 0.001f;
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

                        Thread.Sleep(0);
                        goto _thread_looper;
                    };

                    threaders.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs argers)
                    {

                    };

                    threaders.RunWorkerAsync();


                    _start_background_worker_01 = 1;
                }
            }
            //END OF PHYSICS ENGINE STEPS










            try
            {
                Vector3f[] hmdToEyeViewOffsets = { Program.D3D.eyeTextures[0].HmdToEyeViewOffset, Program.D3D.eyeTextures[1].HmdToEyeViewOffset };
                displayMidpoint = Program.D3D.OVR.GetPredictedDisplayTime(Program.D3D.sessionPtr, 0);
                trackingState = Program.D3D.OVR.GetTrackingState(Program.D3D.sessionPtr, displayMidpoint, true);
                eyePoses = new Posef[2];
                Program.D3D.OVR.CalcEyePoses(trackingState.HeadPose.ThePose, hmdToEyeViewOffsets, ref eyePoses);

                for (int eyeIndex = 0; eyeIndex < 2; eyeIndex++)
                {
                    eye = (EyeType)eyeIndex;
                    eyeTexture = Program.D3D.eyeTextures[eyeIndex];

                    if (eyeIndex == 0)
                    {
                        Program.D3D.layerEyeFov.RenderPoseLeft = eyePoses[0];
                    }
                    else
                    {
                        Program.D3D.layerEyeFov.RenderPoseRight = eyePoses[1];
                    }

                    eyeTexture.RenderDescription = Program.D3D.OVR.GetRenderDesc(Program.D3D.sessionPtr, eye, Program.D3D.hmdDesc.DefaultEyeFov[eyeIndex]);

                    Program.D3D.result = eyeTexture.SwapTextureSet.GetCurrentIndex(out textureIndex);
                    Program.D3D.WriteErrorDetails(Program.D3D.OVR, Program.D3D.result, "Failed to retrieve texture swap chain current index.");

                    Program.D3D.device.ImmediateContext.OutputMerger.SetRenderTargets(eyeTexture.DepthStencilView, eyeTexture.RenderTargetViews[textureIndex]);
                    Program.D3D.device.ImmediateContext.ClearRenderTargetView(eyeTexture.RenderTargetViews[textureIndex], SharpDX.Color.Black); //DimGray
                    Program.D3D.device.ImmediateContext.ClearDepthStencilView(eyeTexture.DepthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);
                    Program.D3D.device.ImmediateContext.Rasterizer.SetViewport(eyeTexture.Viewport);

                    eyeQuaternionMatrix = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X, eyePoses[eyeIndex].Orientation.Y, eyePoses[eyeIndex].Orientation.Z, eyePoses[eyeIndex].Orientation.W));

                    eyePos = SharpDX.Vector3.Transform(new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z), originRot * rotatingMatrix * rotatingMatrixForPelvis).ToVector3();

                    //finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix;
                    finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix * rotatingMatrixForPelvis;

                    lookUp = Vector3.Transform(new Vector3(0, 1, 0), finalRotationMatrix).ToVector3();
                    lookAt = Vector3.Transform(new Vector3(0, 0, -1), finalRotationMatrix).ToVector3();

                    viewPosition = eyePos + OFFSETPOS;
                    viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);

                    _projectionMatrix = Program.D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.01f, 100.0f, ProjectionModifier.None).ToMatrix();
                    _projectionMatrix.Transpose();

                    //somefunctioncaller to update
                    if (_can_work_physics == 1)
                    {
                        _sc_jitter_tasks = _main_graph.workOnSomething(_sc_jitter_tasks, viewMatrix, _projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft); // ?? i cant call the abstract function.
                                                                                                                                                                                                                                                          //_sc_jitter_tasks = Program._graphics_sec.workOnSomething(_sc_jitter_tasks, viewMatrix, _projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft); // ?? i cant call the abstract function.
                    }

                    Program.D3D.result = eyeTexture.SwapTextureSet.Commit();
                    Program.D3D.WriteErrorDetails(Program.D3D.OVR, Program.D3D.result, "Failed to commit the swap chain texture.");
                }

                Program.D3D.result = Program.D3D.OVR.SubmitFrame(Program.D3D.sessionPtr, 0L, IntPtr.Zero, ref Program.D3D.layerEyeFov);
                Program.D3D.WriteErrorDetails(Program.D3D.OVR, Program.D3D.result, "Failed to submit the frame of the current layers.");
                Program.D3D.DeviceContext.CopyResource(Program.D3D.mirrorTextureD3D, Program.D3D.BackBuffer);
                Program.D3D.SwapChain.Present(0, PresentFlags.None); //crap visuals returning to only spheroids.
            }
            catch (Exception ex)
            {
                Program.MessageBox((IntPtr)0, "" + ex.ToString(), "sc core systems message", 0);
            }
            if (_can_work_physics == 1)
            {
                _sc_jitter_tasks = Program._graphics_sec.loop_worlds(_sc_jitter_tasks);
                _sc_jitter_tasks = Program._graphics_sec.sc_write_to_buffer(_sc_jitter_tasks);
            }

            //###SC start physics on frame 1 instead of 0
            _can_work_physics = 1;
            _can_work_physics_objects = 1;
            //###SC start physics on frame 1 instead of 0
            return _sc_jitter_tasks;
        }

        public static int _can_work_physics = 0;

        KeyboardState _KeyboardState;
        public SharpDX.DirectInput.Keyboard _Keyboard;
        DirectInput directInput;
        private bool ReadKeyboard()
        {
            directInput = new DirectInput();

            _Keyboard = new Keyboard(directInput);

            // Acquire the joystick
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
                { }

                return true;
            }

            if (resultCode == SharpDX.DirectInput.ResultCode.Ok)
                return true;

            return false;
        }














    }
}

