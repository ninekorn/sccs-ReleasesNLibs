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

using sccs.scgraphics;
using sccs.scgraphics.scshadermanager;

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

using scmessageobject = sccs.scmessageobject.scmessageobject;
using scmessageobjectjitter = sccs.scmessageobject.scmessageobjectjitter;
using scgraphicssecpackage = sccs.scmessageobject.scgraphicssecpackage;


namespace sccs.scgraphics
{
    public class scupdate : scdirectx
    {

        public static int stopovr = 0;
        public Thread main_thread_update;
        public Thread heightmapthread;
        public int threadupdateswtc = 0;
        public static DateTime startTime;// = DateTime.Now;

        public static Matrix oriProjectionMatrix;
        public static int[] arduinoDIYOculusTouchArrayOfData = new int[12];

        int screencaptureresultswtc = 0;

        _sc_camera Camera;
        public scgraphicssecpackage scgraphicssecpackagemessage;
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
        public int canworkphysics = 0;
        public static Matrix hmdmatrixRot = Matrix.Identity;
        int updatethreadupdateswtc = 0;

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

        public static SharpDX.Vector3 originPos = new SharpDX.Vector3(0, 2, 0); //new SharpDX.Vector3(-10, 1, 10);
        public static SharpDX.Vector3 originPosScreen = new SharpDX.Vector3(0, 0, 1.5f);

        float disco_sphere_rot_speed = 0.5f;

        float speedRot = 0.75f; //1.95f // 0.25f
        float speedRotArduino = 0.000001f;

        float speedPos = 0.75f; //0.025f // 1.5f
        float speedPosArduino = 0.001f;
        public static Matrix hmd_matrix;
        Matrix hmd_matrix_test;


        public static double RotationY = 0;//{ get; set; } { get; set; }
        public static double RotationX = 0;//{ get; set; } { get; set; }
        public static double RotationZ = 0;//{ get; set; } { get; set; }

        public static float RotationOriginY = 0;//{ get; set; }
        public static float RotationOriginX = 0;//{ get; set; }
        public static float RotationOriginZ = 0;//{ get; set; }

        float thumbstickIsRight;
        float thumbstickIsUp;


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

        public static Matrix tempmatter;
        public static Vector4 dirikvoxelbodyInstanceRight0;
        public static Vector4 dirikvoxelbodyInstanceUp0;
        public static Vector4 dirikvoxelbodyInstanceForward0;

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

        int _swtch_hasRotated = 0;
        int _has_grabbed_right_swtch = 0;
        int RotationGrabbedSwtch = 0;
        int _sec_logic_swtch_grab = 0;
        Matrix rotatingMatrixForGrabber = Matrix.Identity;


        public static sccsscreenframe screencaptureframe;
        public static sccssharpdxscreencapture sharpdxscreencapture;

        public static SC_ShaderManager _shaderManager;


        public scupdate()
        {
            scgraphicssecpackagemessage = new scgraphicssecpackage();
            startTime = DateTime.Now;
        }

        protected override void ShutDownGraphics()
        {
            if (sharpdxscreencapture != null)
            {
                sharpdxscreencapture.Disposer();
            }

            sharpdxscreencapture = null;
            _shaderManager = null;



            /*D3D.OVR.Destroy(D3D.sessionPtr);
            if (D3D != null)
            {
                D3D = null;
            }*/

            Program.MessageBox((IntPtr)0, "ShutDownGraphics scupdate.cs", "sc core systems message", 0);
        }

        protected override scmessageobjectjitter[][] init_update_variables(scmessageobjectjitter[][] _sc_jitter_tasks, sccs.sccore.scsystemconfiguration configuration, IntPtr hwnd) //, scconsole.scconsolewriter _writer
        {
            try
            {
                Camera = new _sc_camera();

                if (Program._useOculusRift == 0)
                {
                    originPos.Y += 4;
                    //originPos.Z -= 2f;

                    //Program.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                    Camera.SetPosition(originPos.X, originPos.Y, originPos.Z);

                    Camera.SetRotation(0, 180, 0);
                }
                var directInput = new DirectInput();

                _Keyboard = new SharpDX.DirectInput.Keyboard(directInput);

                _Keyboard.Properties.BufferSize = 128;
                _Keyboard.Acquire();


                _shaderManager = new SC_ShaderManager();
                _shaderManager.Initialize(D3D.Device, Program.consoleHandle);

                sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, D3D.device);



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
            try
            {
                _sc_jitter_tasks = _FrameVRTWO(jitter_sc, _sc_jitter_tasks);
            }
            catch (Exception ex)
            {
                Program.MessageBox((IntPtr)0, "" + ex.ToString(), "sc core systems message", 0);
            }

            return _sc_jitter_tasks;
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

        private unsafe scmessageobjectjitter[][] _FrameVRTWO(jitter_sc[] jitter_sc, scmessageobjectjitter[][] _sc_jitter_tasks)
        {





            if (Program._useOculusRift == 1)
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
                    Program.MessageBox((IntPtr)0, "" + buttonPressedOculusTouchRight, "sccs message", 0);
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
                    Program.MessageBox((IntPtr)0, "" + buttonPressedOculusTouchLeft, "sccs message", 0);
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

                //float speed = 0.015f;
                //float speedRot = 0.1f;
                float rotY = 0;

                if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.A))
                {
                    rotY -= speedRot;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.D))
                {
                    rotY += speedRot;
                }




                var somerot = Camera.GetRotation();

                Camera.SetRotation(somerot.X, somerot.Y + rotY, somerot.Z);






                Matrix tempmat = Camera.rotationMatrix;
                Quaternion otherQuat;
                Quaternion.RotationMatrix(ref tempmat, out otherQuat);

                Vector3 direction_feet_forward;
                Vector3 direction_feet_right;
                Vector3 direction_feet_up;

                direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);



                if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Up))
                {
                    //direction_feet_forward.Z += speed * speedPos;
                    movePos -= direction_feet_forward * speedPos * speedPos;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Down))
                {
                    movePos += direction_feet_forward * speedPos * speedPos;
                    //direction_feet_forward.Z -= speed * speedPos;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Q))
                {
                    movePos += direction_feet_up * speedPos * speedPos;
                    //direction_feet_forward.Y += speed * speedPos;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Z))
                {
                    movePos -= direction_feet_up * speedPos * speedPos;
                    //direction_feet_forward.Y -= speed * speedPos;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Left))
                {
                    movePos -= direction_feet_right * speedPos * speedPos;
                    //direction_feet_forward.X -= speed * speedPos;
                }
                else if (_KeyboardState != null && _KeyboardState.PressedKeys.Contains(Key.Right))
                {
                    movePos += direction_feet_right * speedPos * speedPos;
                    //direction_feet_forward.X += speed * speedPos;
                }



                OFFSETPOS = originPos + movePos;

                Camera.SetPosition(OFFSETPOS.X, OFFSETPOS.Y, OFFSETPOS.Z);

            }




            if (canworkphysics == 1)
            {
                for (int i = 0; i < 1;)
                {
                    screencaptureresultswtc = 0;
                    try
                    {
                        screencaptureframe = sharpdxscreencapture.ScreenCapture(10000);
                    }
                    catch (Exception ex)
                    {
                        sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, D3D.device);
                        screencaptureresultswtc = 1;
                    }
                    i++;
                    if (screencaptureresultswtc == 0)
                    {
                        break;
                    }
                }
            }




            if (Program._useOculusRift == 1)
            {

                //Program.MessageBox((IntPtr)0, "000", "sc core systems message", 0);
                try
                {
                    if (canworkphysics == 1)
                    {
                        /*
                        if (graphicssec != null)
                        {
                            graphicssec.oculuscontrolsNRecordSoundNMousePointer();
                        }*/





                        if (Program.useArduinoOVRTouchKeymapper == 0)
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

                        if (thumbStickLeft[0].X > 0.5f)
                        {
                            movePos += direction_feet_right * speedPos * thumbStickLeft[0].X;
                        }
                        else if (thumbStickLeft[0].X < -0.5f)
                        {
                            movePos -= direction_feet_right * speedPos * -thumbStickLeft[0].X;
                        }

                        if (thumbStickLeft[0].Y > 0.5f)
                        {
                            movePos += direction_feet_forward * speedPos * thumbStickLeft[0].Y;
                        }
                        else if (thumbStickLeft[0].Y < -0.5f)
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
                                    //Program.MessageBox((IntPtr)0, "threadstart succes", "sc core systems message", 0);
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
                               Program.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                            }*/


                            //_sc_jitter_tasks = graphicssec.sccswriteikrigtobuffer(_sc_jitter_tasks);
                            //_sc_jitter_tasks = graphicssec.sccswritescreenassetstobuffer(_sc_jitter_tasks);
                        }
                        //writetobufferikrig = 1;

                    }





                }
                catch (Exception ex)
                {
                    Program.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                }

            }














            /*

                        if (Program._useOculusRift == 1)
                        {
                            if (scgraphicssecpackagemessage.scjittertasks != null)
                            {
                                if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                {
                                    if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1 && scgraphicssecpackagemessage.scgraphicssec.createikrig == 1)
                                    {
                                        scgraphicssecpackagemessage.scgraphicssec.oculuscontrolsNRecordSoundNMousePointer();
                                    }
                                }
                            }
                        }


                        */











            if (Program._useOculusRift == 0 && canworkphysics == 1)
            {
                Matrix someextrapelvismatrix = rotatingMatrixForPelvis; //originRot



                // Clear views
                D3D.DeviceContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);
                D3D.DeviceContext.ClearRenderTargetView(_renderTargetView, SharpDX.Color.Black); //LightGray






                float ratio = (float)SurfaceWidth / (float)SurfaceHeight;
                /// _projectionMatrix = Matrix.PerspectiveFovLH(3.14F / 3.0F, ratio, 0.001f, 1000);
                _projectionMatrix = Matrix.PerspectiveFovLH((float)(Math.PI / 4), ((float)SurfaceWidth / (float)SurfaceHeight), 0.001f, 1000);

                //viewMatrix = Matrix.LookAtLH(new Vector3(0, 3, 10), new Vector3(), Vector3.UnitY);
                viewMatrix = Camera.ViewMatrix;





                //Program.MessageBox((IntPtr)0, "0", "sc core systems message", 0);
                if (canworkphysics == 1)
                {



                    var somerotvec = Camera.GetRotation();
                    float pitch = somerotvec.X * 0.0174532925f;
                    float yaw = somerotvec.Y * 0.0174532925f; ;
                    float roll = somerotvec.Z * 0.0174532925f; ;
                    Matrix somerotmat = Camera.rotationMatrix;// Matrix.RotationYawPitchRoll(yaw, pitch, roll);

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


                    if (updatethreadupdateswtc == 0)
                    {


                        scgraphicssecpackagemessage.scgraphicssec = new sccs.scgraphics.scgraphicssec();
                        scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec._sc_create_world_objects(scgraphicssecpackagemessage.scjittertasks);




                        main_thread_update = new Thread(() =>
                        {

                            //scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;
                            //sc_graphics_sec graphicssec;

                            threadupdateswtc = 0;
                        //Program.MessageBox((IntPtr)0, "test", "sccsmsg", 0);

                        _thread_looper:


                            //scgraphicssecpackagemessage.scgraphicssec = graphicssec;

                            //Program.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                            try
                            {
                                if (threadupdateswtc == 0) // 0
                                {

                                    //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workOnDestroyingBytes(scgraphicssecpackagemessage);
                                    //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwriteinstancedVDbytestobuffer(scgraphicssecpackagemessage.scjittertasks);
                                    //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswritescreenassetstobuffer(scgraphicssecpackagemessage.scjittertasks);
                                    //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);


                                    if (scgraphicssecpackagemessage.scjittertasks != null)
                                    {
                                        if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                        {
                                            if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                            {
                                                //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswritescreenassetstobuffer(scgraphicssecpackagemessage.scjittertasks);

                                                scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritedirectiontobuffer(scgraphicssecpackagemessage.scjittertasks);

                                                scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwriteheightmaptobuffer(scgraphicssecpackagemessage.scjittertasks);

                                                //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                                //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                            }
                                        }
                                    }



                                    //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);
                                    //threadupdateswtc = 1;
                                }


                            }
                            catch (Exception ex)
                            {
                                Program.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
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
                            //Program.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                            try
                            {
                            }
                            catch (Exception ex)
                            {
                                Program.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                            }
                            Thread.Sleep(1);
                            goto loopthread;
                        }, scgraphicssecpackagemessage);*/


                        updatethreadupdateswtc = 1;
                    }

                    //scgraphicssecpackagemessage.scjittertasks = graphicssec.workonshaders(scgraphicssecpackagemessage);

                    /*viewMatrix.Transpose();
                    _projectionMatrix.Transpose();

                    scgraphicssecpackagemessage.viewMatrix = viewMatrix;
                    scgraphicssecpackagemessage.projectionMatrix = _projectionMatrix;*/

                    if (scgraphicssecpackagemessage.scjittertasks != null)
                    {
                        if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                        {
                            if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                            {
                                //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                _sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);
                                //_sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;
                            }
                        }
                    }
                }





                D3D.SwapChain.Present(0, PresentFlags.None);




                //GC.Collect();
            }
            else
            {

                if (D3D != null && Program.exitedprogram != 1)
                {
                    if (D3D.OVR != null && stopovr == 0)
                    {


                        /*if (canworkphysics == 1)
                        {
                        

                            if (updatethreadupdateswtc == 0)
                            {s3







                          

                                updatethreadupdateswtc = 1;
                            }
                        }*/





                        Vector3f[] hmdToEyeViewOffsets = { D3D.eyeTextures[0].HmdToEyeViewOffset, D3D.eyeTextures[1].HmdToEyeViewOffset };
                        displayMidpoint = D3D.OVR.GetPredictedDisplayTime(D3D.sessionPtr, 0);
                        trackingState = D3D.OVR.GetTrackingState(D3D.sessionPtr, displayMidpoint, true);
                        eyePoses = new Posef[2];
                        D3D.OVR.CalcEyePoses(trackingState.HeadPose.ThePose, hmdToEyeViewOffsets, ref eyePoses);

                        //float timeSinceStart = (float)(DateTime.Now - startTime).TotalSeconds;

                        for (int eyeIndex = 0; eyeIndex < 2; eyeIndex++) // 2
                        {
                            Matrix someextrapelvismatrix = rotatingMatrixForPelvis; //originRot



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
                            D3D.device.ImmediateContext.ClearRenderTargetView(eyeTexture.RenderTargetViews[textureIndex], SharpDX.Color.Black); //DimGray Black
                            D3D.device.ImmediateContext.ClearDepthStencilView(eyeTexture.DepthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);
                            D3D.device.ImmediateContext.Rasterizer.SetViewport(eyeTexture.Viewport);


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

                            if (Program.usethirdpersonview == 0)
                            {

                                //FOR THE VERTEX SHADER
                                Quaternion somedirquat1;
                                Quaternion.RotationMatrix(ref tempmatter, out somedirquat1);
                                dirikvoxelbodyInstanceRight0 = new Vector4(-sc_maths._newgetdirleft(somedirquat1), 0);
                                dirikvoxelbodyInstanceUp0 = new Vector4(sc_maths._newgetdirup(somedirquat1), 0);
                                dirikvoxelbodyInstanceForward0 = new Vector4(sc_maths._newgetdirforward(somedirquat1), 0);
                            }
                            else if (Program.usethirdpersonview == 1)
                            {
                                Quaternion somedirquat1;
                                Quaternion.RotationMatrix(ref tempmatter, out somedirquat1);
                                dirikvoxelbodyInstanceRight0 = new Vector4(-sc_maths._newgetdirleft(somedirquat1), 0);
                                dirikvoxelbodyInstanceUp0 = new Vector4(sc_maths._newgetdirup(somedirquat1), 0);
                                dirikvoxelbodyInstanceForward0 = new Vector4(sc_maths._newgetdirforward(somedirquat1), 0);

                                viewPosition = viewPosition + (new Vector3(dirikvoxelbodyInstanceForward0.X, dirikvoxelbodyInstanceForward0.Y, dirikvoxelbodyInstanceForward0.Z) * Program.offsetthirdpersonview);
                            }





                            viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);
                            _projectionMatrix = D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.001f, 1000.0f, ProjectionModifier.None).ToMatrix();
                            oriProjectionMatrix = _projectionMatrix;
                            _projectionMatrix.Transpose();


                            //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                            //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                            //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.




                            //Program.MessageBox((IntPtr)0, "0", "sc core systems message", 0);
                            if (canworkphysics == 1)
                            {
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

                                if (scgraphicssecpackagemessage.scjittertasks != null)
                                {
                                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                    {
                                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                        {
                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                            _sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);
                                        }
                                    }
                                }

                                if (updatethreadupdateswtc == 0)
                                {








                                    scgraphicssecpackagemessage.scgraphicssec = new sccs.scgraphics.scgraphicssec();
                                    scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec._sc_create_world_objects(scgraphicssecpackagemessage.scjittertasks);



                                    main_thread_update = new Thread(() =>
                                    {



                                        //scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;
                                        //sc_graphics_sec graphicssec;

                                        int threadupdateswtc = 0;

                                    //Program.MessageBox((IntPtr)0, "test", "sccsmsg", 0);

                                    _thread_looper:


                                        //scgraphicssecpackagemessage.scgraphicssec = graphicssec;

                                        //Program.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                                        try
                                        {

                                            //_ticks_watch.Stop();
                                            //_ticks_watch.Restart();


                                            if (threadupdateswtc == 0) //0
                                            {



                                                if (scgraphicssecpackagemessage.scjittertasks != null)
                                                {
                                                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                                    {
                                                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                                        {
                                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                                            scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritedirectiontobuffer(scgraphicssecpackagemessage.scjittertasks);
                                                        }
                                                    }
                                                }










                                                //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwriteinstancedVDbytestobuffer(scgraphicssecpackagemessage.scjittertasks);
                                                //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswritescreenassetstobuffer(scgraphicssecpackagemessage.scjittertasks);
                                                //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workOnDestroyingBytes(scgraphicssecpackagemessage);


                                                //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);
                                                threadupdateswtc = 1;
                                            }

                                            //Console.WriteLine(_ticks_watch.Elapsed.Milliseconds);


                                        }
                                        catch (Exception ex)
                                        {
                                            Program.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                                        }

                                        Thread.Sleep(0);
                                        goto _thread_looper;

                                        //ShutDown();
                                        //ShutDownGraphics();

                                    }, 0);

                                    main_thread_update.IsBackground = true;
                                    main_thread_update.Priority = ThreadPriority.Lowest; //AboveNormal
                                    main_thread_update.SetApartmentState(ApartmentState.STA);
                                    main_thread_update.Start();



                                    heightmapthread = new Thread(() =>
                                    {



                                    //Program.MessageBox((IntPtr)0, "test", "sccsmsg", 0);

                                    _thread_looper:

                                        if (threadupdateswtc == 0 && Program.exitedprogram != 1) //0
                                        {
                                            //scgraphicssecpackagemessage.scgraphicssec = graphicssec;

                                            //Program.MessageBox((IntPtr)0, "1", "sc core systems message", 0);
                                            try
                                            {

                                                if (scgraphicssecpackagemessage.scjittertasks != null)
                                                {
                                                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                                    {
                                                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                                        {
                                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                                            scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwriteheightmaptobuffer(scgraphicssecpackagemessage.scjittertasks);
                                                        }
                                                    }
                                                }


                                                //_ticks_watch.Stop();
                                                //_ticks_watch.Restart();

                                                //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswritescreenassetstobuffer(scgraphicssecpackagemessage.scjittertasks);
                                                //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workOnDestroyingBytes(scgraphicssecpackagemessage);


                                                //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);
                                                //threadupdateswtc = 1;


                                                //Console.WriteLine(_ticks_watch.Elapsed.Milliseconds);


                                            }
                                            catch (Exception ex)
                                            {
                                                Program.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                                            }

                                            Thread.Sleep(0);
                                            goto _thread_looper;
                                        }
                                        else
                                        {
                                            Thread.Sleep(1);
                                            goto _thread_looper;
                                        }
                                        //ShutDown();
                                        //ShutDownGraphics();

                                    }, 0);

                                    heightmapthread.IsBackground = true;
                                    heightmapthread.Priority = ThreadPriority.Lowest; //AboveNormal
                                    heightmapthread.SetApartmentState(ApartmentState.STA);
                                    heightmapthread.Start();


                                    updatethreadupdateswtc = 1;
                                }





                                //Program.MessageBox((IntPtr)0, "test", "sccsmsg", 0);

                                /*if (scgraphicssecpackagemessage.scjittertasks != null)
                                {
                                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                    {
                                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                        {
                                            _sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                            _sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);
                                        }
                                    }
                                }*/





                            }

                            D3D.result = eyeTexture.SwapTextureSet.Commit();
                            D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to commit the swap chain texture.");
                        }


                        D3D.result = D3D.OVR.SubmitFrame(D3D.sessionPtr, 0L, IntPtr.Zero, ref D3D.layerEyeFov);

                        if (D3D != null)
                        {
                            if (D3D.OVR != null)
                            {
                                /*if (D3D.result != null && Program.exitedprogram != 1)
                                {
                                    //D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to submit the frame of the current layers.");

                                }*/
                            }
                            D3D.DeviceContext.CopyResource(D3D.mirrorTextureD3D, D3D.BackBuffer);
                            D3D.SwapChain.Present(0, PresentFlags.None);
                        }






                    }
                }
            }



            if (canworkphysics != -1) //-1 to exit program safe
            {
                canworkphysics = 1;
            }

            //_can_work_physics_objects = 1;

            //Program.MessageBox((IntPtr)0, "001", "sc core systems message", 0);
            return _sc_jitter_tasks;
        }
    }
}

