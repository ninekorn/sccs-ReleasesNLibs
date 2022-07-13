////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//DEVELOPED BY STEVE CHASSÉ using xoofx's sharpdx original deferred rendering sample. This is a software of mixed architecture//
//using rastertek c# github user dan6040's sample architecture and smartrak's sample architecture and xoofx sharpdx samples/////
//architecture./////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Copyright (c) 2010-2013 SharpDX - Alexandre Mutel 
//  
// Permission is hereby granted, free of charge, to any person obtaining a copy 
// of this software and associated documentation files (the "Software"), to deal 
// in the Software without restriction, including without limitation the rights 
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
// copies of the Software, and to permit persons to whom the Software is 
// furnished to do so, subject to the following conditions: 
//  
// The above copyright notice and this permission notice shall be included in 
// all copies or substantial portions of the Software. 
//  
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
// THE SOFTWARE. 


//The MIT License (MIT)
//
//Copyright(c) 2016 Smartrak

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.


//https://github.com/Dan6040/SharpDX-Rastertek-Tutorials
//https://github.com/Smartrak/WpfSharpDxControl
//https://github.com/sharpdx/SharpDX-Samples

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using System.Diagnostics;
using Key = SharpDX.DirectInput.Key;

//using DSharpDXRastertek.Tut12.Graphics;
//using DSharpDXRastertek.Tut12.Graphics.TextFont;
//using DSharpDXRastertek.Tut13.Graphics.TextFont;
using SharpDX.Direct3D11;

using Ab3d.OculusWrap;
using Ab3d.OculusWrap.DemoDX11;

using System.Runtime.InteropServices;

namespace sccsr15forms
{
    public class updatePrim : IDisposable
    {
        public Matrix oriProjectionMatrix;


        [StructLayout(LayoutKind.Explicit)]
        public struct worldviewprobuffer
        {
            [FieldOffset(0)]
            public Matrix worldmatrix;
            [FieldOffset(64)]
            public Matrix viewmatrix;
            [FieldOffset(128)]
            public Matrix projectionmatrix;
        }

        public worldviewprobuffer[] arrayofworldmatrix = new worldviewprobuffer[1];


        public Vector3 viewpositionorigin = Vector3.Zero;
        public Vector3 viewPosition;
        public Vector3 lookUp;
        public Vector3 lookAt;

        public int canworkphysics = 0;

        public static Vector4 dirikvoxelbodyInstanceRight0;
        public static Vector4 dirikvoxelbodyInstanceUp0;
        public static Vector4 dirikvoxelbodyInstanceForward0;
        Quaternion quatt;

        public static updatePrim currentupdatePrim;
        public directx D3D;
        public Camera camera;

        public DTextClass Text;

        updateSec updatesec;

        Vector3 originPos = Vector3.Zero;


        
        public updatePrim(directx D3D_) //, updateSec updatesec_
        {
            currentupdatePrim = this;
            D3D = D3D_;

            //updatesec = updateSec?.currentupdatesec;
            updatesec = Program.updatesec;


            camera = new Camera();

            
            if (Program.useOculusRift == 0)
            {

                //originPos.Y += 4;
                //originPos.Z -= 2f;
                speedRot = 0.015f;
                speedPos = 0.0015f;

                //camera.SetPosition(0, 0, -5);
                camera.SetPosition(0, 0, -1.0f);
                camera.SetRotation(0, 0, 0);

                originPos = camera.GetPosition();

            }
            else if (Program.useOculusRift == 1)
            {
                speedRot = 0.45f;
                speedPos = 0.025f;

                RotationX = 0;
                RotationY = 0; //180
                RotationZ = 0;

                RotationX4Pelvis = 0;
                RotationY4Pelvis = 0; //180
                RotationZ4Pelvis = 0;

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



                RotationOriginX = 0;
                RotationOriginY = 0;
                RotationOriginZ = 0;

                pitch = (float)(Math.PI * (RotationOriginX) / 180.0f);
                yaw = (float)(Math.PI * (RotationOriginY) / 180.0f);
                roll = (float)(Math.PI * (RotationOriginZ) / 180.0f);


                originRot = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                originPos = Vector3.Zero;
            }



            //camera.SetPosition(0, 0, -1.0f);
            //camera.SetRotation(0, 0, 0);


            camera.Render();
            var baseViewMatrix = camera.ViewMatrix;
            //camera.rotationMatrix = baseViewMatrix;
            // Create the text object.
            Text = new DTextClass();

            //if (!Text.Initialize(D3D.Device, D3D.DeviceContext, D3D.apphandle, D3D.SurfaceWidth, D3D.SurfaceHeight, baseViewMatrix))
            //    return false;
            Text.Initialize(D3D.Device, D3D.DeviceContext, D3D.apphandle, D3D.SurfaceWidth, D3D.SurfaceHeight, baseViewMatrix);

            //camera.SetPosition(0, 6, -9);
            camera.SetPosition(0, 0, 0);
            movePos = camera.GetPosition();
            //movePos = camera.GetPosition();
            //camera.SetRotation(90, 0, 0);

            Console.WriteLine("created updatePrim");
        }

        //TEST FOR UI THREAD VS SYSTEM.THREAD PERFORMANCE. SYSTEM.THREAD IS FASTER.
        /*public int counteruithread;
        public int countersystemthread;

        public void updatescriptsprimUIThread()
        {
            counteruithread++;
        }
        public void updatescriptsprimSystemThread()
        {
            countersystemthread++;
        }*/

        //public DSharpDXRastertek.Tut12.Graphics.TextFont.DTextClass.DSentence[] sentences = new DSharpDXRastertek.Tut12.Graphics.TextFont.DTextClass.DSentence[3];


        public bool Frame(float playerx, float playery, float playerz, string newText, DeviceContext somedevicecontext)
        {
            bool resultMouse = true, resultKeyboard = true;


            //Text.UpdateSentece(ref Text.sentences[0], "TEST", 100, 100, 1, 1, 1, D3D.DeviceContext);
            //Text.SetNewSenctenceDataVertex("" + 0, somedevicecontext);
            //Text.SetNewSenctenceDataTriangle("" + 1, somedevicecontext);

            // Set the location of the mouse.
            if (!Text.setoverlaydata(playerx, playery, playerz, Program.updatesec, somedevicecontext))
                resultMouse = false;

    






            // Set the position of the camera.
            //camera.SetPosition(0, 0, -10f);

            return (resultMouse | resultKeyboard);
        }














        public Matrix viewMatrix;
        public Matrix worldMatrix;
        public Matrix projectionMatrix;
        public Matrix orthoMatrix;


        int counterforupdateoverlaytext = 0;
        int counterforupdateoverlaytextmax = 10;

        public void updatescriptsupdatetext(DeviceContext somedevicecontext)
        {
            //float posx = (float)Math.Round(OFFSETPOS.X / 100) * 100;
            //float posz = (float)Math.Round(OFFSETPOS.Y / 100) * 100;
            float posx = (float)Math.Round(camera.GetPosition().X * 1000) / 1000;
            float posy = (float)Math.Round(camera.GetPosition().Y * 1000) / 1000;
            float posz = (float)Math.Round(camera.GetPosition().Z * 1000) / 1000;

            if (counterforupdateoverlaytext > counterforupdateoverlaytextmax)
            {
                //Frame(Program.fpsCounter, Program.fpsCounter, "fpsCounter:" + Program.fpsCounter, somedevicecontext);
                Frame(posx, posy, posz, "", somedevicecontext); //"fpsCounter:" + Program.fpsCounter


                counterforupdateoverlaytext = 0;
            }


            counterforupdateoverlaytext++;


            viewMatrix = camera.ViewMatrix;
            worldMatrix = D3D.WorldMatrix;
            projectionMatrix = D3D.ProjectionMatrix;
            orthoMatrix = D3D.OrthoMatrix;


            //startrender();
            // Turn off the Z buffer to begin all 2D rendering.
            D3D.TurnZBufferOff(somedevicecontext);
            // Turn on the alpha blending before rendering the text.
            D3D.TurnOnAlphaBlending(somedevicecontext);

            //Text.Render(D3D.DeviceContext, worldMatrix, orthoMatrix);
            Text.Render(somedevicecontext, worldMatrix, orthoMatrix);

            // Turn off the alpha blending before rendering the text.
            D3D.TurnOffAlphaBlending(somedevicecontext);

            // Turn on the Z buffer to begin all 2D rendering.
            D3D.TurnZBufferOn(somedevicecontext);
        }

        /*
        public void updatescriptsprimstoprender()
        {
            stoprender();
        }*/

        float speedRot = 0.0275f;
        float speedPos = 0.00015f;
        float rotx = 0;
        float roty = 0;
        float rotz = 0;

        int canmovecamera = 1;
        public Vector3 movePos = Vector3.Zero;
        //Vector3 originPos = new Vector3(0, 0, 0);

   

        public Vector3 OFFSETPOS = Vector3.Zero;
        public Vector3 dircamr = Vector3.Zero;
        public Vector3 dircamu = Vector3.Zero;
        public Vector3 dircamf = Vector3.Zero;
        Quaternion somedirquat1;
        Matrix cammatrix;

        public void updatecamera()
        {

            camera.Render();


            cammatrix = camera.rotationMatrix;
            Quaternion.RotationMatrix(ref cammatrix, out somedirquat1);
            dircamr = (-sc_maths._newgetdirleft(somedirquat1));
            dircamu = (sc_maths._newgetdirup(somedirquat1));
            dircamf = (sc_maths._newgetdirforward(somedirquat1));




            if (canmovecamera == 1)
            {
                if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.A))
                {
                    //Console.WriteLine("pressed A");
                    roty -= speedRot;
                }
                else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.D))
                {
                    //Console.WriteLine("pressed D");
                    roty += speedRot;
                }
                else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.R))
                {
                    //Console.WriteLine("pressed R");
                    rotx -= speedRot;
                }
                else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.F))
                {
                    //Console.WriteLine("pressed F");
                    rotx += speedRot;
                }

                var somerot = camera.GetRotation();
                camera.SetRotation(rotx, roty, somerot.Z);



                //Matrix tempmater = camera.rotationMatrix;
                //Quaternion otherQuat;
                //Quaternion.RotationMatrix(ref tempmater, out otherQuat);

                /*Vector3 oricampos = camera.GetPosition();

                float xpos = oricampos.X;
                float ypos = oricampos.Y;
                float zpos = oricampos.Z;*/


                if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Up))
                {
                    //Program.MessageBox((IntPtr)0, "000", "sc core systems message", 0);
                    //direction_feet_forward.Z += speed * speedPos;
                    movePos += dircamf * speedPos;
                }
                else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Down))
                {
                    movePos -= dircamf * speedPos;
                    //direction_feet_forward.Z -= speed * speedPos;
                }
                else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Q))
                {
                    movePos += dircamu * speedPos;
                    //direction_feet_forward.Y += speed * speedPos;
                }
                else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Z))
                {
                    movePos -= dircamu * speedPos;
                    //direction_feet_forward.Y -= speed * speedPos;
                }
                else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Left))
                {
                    movePos += dircamr * speedPos;
                    //direction_feet_forward.X -= speed * speedPos;
                }
                else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Right))
                {
                    movePos -= dircamr * speedPos;
                    //direction_feet_forward.X += speed * speedPos;
                }
                else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Add))
                {

                    speedPos += 0.001f;
                    //direction_feet_forward.X -= speed * speedPos;
                }
                else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Subtract))
                {
                    if (speedPos > 0)
                    {
                        speedPos -= 0.001f;
                    }
                    if (speedPos < 0)
                    {
                        speedPos = 0;
                    }

                    //direction_feet_forward.X += speed * speedPos;
                }



                //Vector3 somecurrentcampos = updateprim_.camera.GetPosition();
                OFFSETPOS = movePos; //OFFSETPOS.X, OFFSETPOS.Y, OFFSETPOS.Z
               
                //OFFSETPOS = camera.GetPosition();
                //OFFSETPOS += movePos;
                camera.SetPosition(OFFSETPOS.X, OFFSETPOS.Y, OFFSETPOS.Z);
            }

           

        }



        //OVR VARIABLES 
        public Matrix tempmatter;
        double displayMidpoint;
        TrackingState trackingState;
        Posef[] eyePoses;
        EyeType eye;
        public EyeTexture eyeTexture;
        bool latencyMark = false;
        TrackingState trackState;
        PoseStatef poseStatefer;
        Posef hmdPose;
        Quaternionf hmdRot;
        public Vector3 _hmdPoser;
        Quaternion _hmdRoter;
        public Matrix hmd_matrix;
        Matrix hmd_matrix_test;
        public SharpDX.Matrix finalRotationMatrix;
    
        public SharpDX.Matrix hmdmatrixRot = SharpDX.Matrix.Identity;
        public SharpDX.Matrix originRot = SharpDX.Matrix.Identity;
        public SharpDX.Matrix rotatingMatrixForPelvis = SharpDX.Matrix.Identity;
        public SharpDX.Matrix rotatingMatrix = SharpDX.Matrix.Identity;
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
        public static double Rotationscreenx;
        public static double Rotationscreeny;
        public static double Rotationscreenz;
        double RotationX;
        double RotationY;
        double RotationZ;
        double RotationOriginX;
        double RotationOriginY;
        double RotationOriginZ;
        Matrix rotatingMatrixForGrabber;
        //OVR VARIABLES


        //OCULUS TOUCH SETTINGS 
        int _sec_logic_swtch_grab = 0;
        int _swtch_hasRotated = 0;
        int _has_grabbed_right_swtch = 0;
        int RotationGrabbedSwtch = 0;
        float thumbstickIsRight;
        float thumbstickIsUp;
        public static uint typeofsensortouchL;
        uint lasttypeofsensortouchL;
        public static uint typeofsensortouchR;
        uint lasttypeofsensortouchR;
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
        public Posef handPoseLeft;
        SharpDX.Quaternion _leftTouchQuat;
        public Posef handPoseRight;
        SharpDX.Quaternion _rightTouchQuat;
        public Matrix _leftTouchMatrix = Matrix.Identity;
        public Matrix _rightTouchMatrix = Matrix.Identity;
        //OCULUS TOUCH SETTINGS 

   


        public void updatecontrolsovr()
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
            camera.SetPosition(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);
            Quaternion quatter = new Quaternion(hmdRot.X, hmdRot.Y, hmdRot.Z, hmdRot.W);
            Vector3 oculusRiftDir = sc_maths._getDirection(Vector3.ForwardRH, quatter);


            Matrix.RotationQuaternion(ref quatter, out hmd_matrix);

            Matrix.RotationQuaternion(ref quatter, out hmd_matrix_test);

            hmd_matrix_test = hmd_matrix_test * finalRotationMatrix;

            //TOUCH CONTROLLER RIGHT
            resultsRight = D3D.OVR.GetInputState(D3D.sessionPtr, D3D.controllerTypeRTouch, ref D3D.inputStateRTouch);

            buttonPressedOculusTouchRight = D3D.inputStateRTouch.Buttons;

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

            //Console.WriteLine(handTriggerRight[1] + " " + handTriggerLeft[0]);

            TrackedDeviceType[] sometrackeddevice = new TrackedDeviceType[1];

            sometrackeddevice[0] = TrackedDeviceType.LTouch;

            //PoseStatef[] someposstate = new PoseStatef[1];
            //D3D.OVR.GetDevicePoses(D3D.sessionPtr, sometrackeddevice, 0, someposstate);
            //Console.WriteLine(someposstate[0].ThePose.)




            //0 no index touch nothing
            //4352 index touch
            //8448 no index touch
            //4864 index touch
            //20480 index touch
            //5120 index touch
            //9216 no index touch
            //24576 no index touch
            //8960 no index touch
            //8704 no index touch
            //768 index touch
            //4096 index touch
            //8192 no index touch
            //11008 no index touch
            //256 no index touch
            //9728 no index touch - thumbstick movement
            //16384
            //1024
            //10496
            //10240
            //9984



            //index is being pressed.
            if (D3D.inputStateLTouch.Touches == 10496
                || D3D.inputStateLTouch.Touches == 1024
                || D3D.inputStateLTouch.Touches == 16384
                || D3D.inputStateLTouch.Touches == 8448
                || D3D.inputStateLTouch.Touches == 9216
                || D3D.inputStateLTouch.Touches == 24576
                || D3D.inputStateLTouch.Touches == 8960
                || D3D.inputStateLTouch.Touches == 8704
                || D3D.inputStateLTouch.Touches == 256
                || D3D.inputStateLTouch.Touches == 0
                || D3D.inputStateLTouch.Touches == 8192
                || D3D.inputStateLTouch.Touches == 11008
                || D3D.inputStateLTouch.Touches == 9728
                || D3D.inputStateLTouch.Touches == 10240
                || D3D.inputStateLTouch.Touches == 9984)
            {
                typeofsensortouchL = D3D.inputStateLTouch.Touches;
            }
            else
            {
                typeofsensortouchL = 9999999;
            }

            if (lasttypeofsensortouchL != D3D.inputStateLTouch.Touches)
            {
                Console.WriteLine(D3D.inputStateLTouch.Touches);
            }


            lasttypeofsensortouchL = D3D.inputStateLTouch.Touches;


            //1
            //33
            //96
            //35
            //32
            //43
            //34
            //36
            //37
            //39
            //41
            //64
            //0
            //3
            //20

            //index is being pressed.
            if (D3D.inputStateRTouch.Touches == 1
                || D3D.inputStateRTouch.Touches == 33
                || D3D.inputStateRTouch.Touches == 96
                || D3D.inputStateRTouch.Touches == 35
                || D3D.inputStateRTouch.Touches == 32
                || D3D.inputStateRTouch.Touches == 43
                || D3D.inputStateRTouch.Touches == 34
                || D3D.inputStateRTouch.Touches == 36
                || D3D.inputStateRTouch.Touches == 37
                || D3D.inputStateRTouch.Touches == 39
                || D3D.inputStateRTouch.Touches == 41
                || D3D.inputStateRTouch.Touches == 64
                || D3D.inputStateRTouch.Touches == 0
                || D3D.inputStateRTouch.Touches == 3
                || D3D.inputStateRTouch.Touches == 20)
            {
                typeofsensortouchR = D3D.inputStateRTouch.Touches;
            }
            else
            {
                typeofsensortouchR = 9999999;
            }

            if (lasttypeofsensortouchR != D3D.inputStateRTouch.Touches)
            {
                Console.WriteLine(D3D.inputStateRTouch.Touches);
            }


            lasttypeofsensortouchR = D3D.inputStateRTouch.Touches;

















            if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.A))
            {
                roty -= speedRot;
            }
            else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.D))
            {
                roty += speedRot;
            }




            if (Program.useOculusRift == 1)
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

            canworkphysics = 1;
        }








        public void startrenderdirectx()
        {
            if (D3D != null)
            {
                if (D3D.DepthStencilView != null)
                {
                    //Console.WriteLine("DepthStencilView != null");
                    D3D.DeviceContext.ClearDepthStencilView(D3D.DepthStencilView, SharpDX.Direct3D11.DepthStencilClearFlags.Depth, 1.0f, 0); //new SharpDX.Color(255, 15, 15, 255)

                }
                if (D3D.RenderTargetView != null)
                {
                    //Console.WriteLine("RenderTargetView != null");
                    D3D.DeviceContext.ClearRenderTargetView(D3D.RenderTargetView, SharpDX.Color.LightGray); //SharpDX.Color.LightGray //Black //new SharpDX.Color(255, 15,

                }
            }
        }

        public void stoprenderdirectx()
        {
            if (D3D != null)
            {
                if (D3D.SwapChain != null)
                {
                    D3D.SwapChain.Present(0, SharpDX.DXGI.PresentFlags.None);
                }
            }
        }






        
        public void startrenderdirectxovr()
        {
            if (D3D != null)
            {

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
                    eyeTexture.RenderDescription = D3D.OVR.GetRenderDesc(D3D.sessionPtr, eye, D3D.hmdDesc.DefaultEyeFov[eyeIndex]);

                    // Retrieve the index of the active texture
                    int textureIndex;
                    D3D.result = eyeTexture.SwapTextureSet.GetCurrentIndex(out textureIndex);
                    D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to retrieve texture swap chain current index.");

                    D3D.Device.ImmediateContext.OutputMerger.SetRenderTargets(eyeTexture.DepthStencilView, eyeTexture.RenderTargetViews[textureIndex]);
                    D3D.Device.ImmediateContext.ClearRenderTargetView(eyeTexture.RenderTargetViews[textureIndex], SharpDX.Color.LightGray); //DimGray Black //LightGray
                    D3D.Device.ImmediateContext.ClearDepthStencilView(eyeTexture.DepthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);
                    D3D.Device.ImmediateContext.Rasterizer.SetViewport(eyeTexture.Viewport);

                    SharpDX.Matrix eyeQuaternionMatrix = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X, eyePoses[eyeIndex].Orientation.Y, eyePoses[eyeIndex].Orientation.Z, eyePoses[eyeIndex].Orientation.W));
                    SharpDX.Vector3 eyePos = SharpDX.Vector3.Transform(new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z), originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot).ToVector3();

                    //finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix;
                    finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot;
                    lookUp = Vector3.Transform(new Vector3(0, 1, 0), finalRotationMatrix).ToVector3();
                    lookAt = Vector3.Transform(new Vector3(0, 0, -1), finalRotationMatrix).ToVector3();
                    viewpositionorigin = eyePos;
                    viewPosition = eyePos + OFFSETPOS; // 
                    tempmatter = hmd_matrix * rotatingMatrixForPelvis * hmdmatrixRot;

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

                    //Console.WriteLine(OFFSETPOS);

                    //Console.WriteLine(viewPosition);

                    //viewPosition = camera.GetPosition();
                    Matrix viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);
                    Matrix _projectionMatrix = D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.001f, 1000.0f, ProjectionModifier.None).ToMatrix();
                    oriProjectionMatrix = _projectionMatrix;
                    _projectionMatrix.Transpose();


                    //Program.MessageBox((IntPtr)0, "0", "sc core systems message", 0);
                    if (canworkphysics == 1)
                    {


                        //var view = camera.ViewMatrix;// Matrix.LookAtLH(new Vector3(0, 0, -viewZ), new Vector3(0, 0, 0), Vector3.UnitY);
                        //var proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, D3D.SurfaceWidth / (float)D3D.SurfaceHeight, 0.1f, 1000.0f);          
                        //var viewProj = Matrix.Multiply(view, D3D.ProjectionMatrix);

                        var viewProj = Matrix.Multiply(viewMatrix, _projectionMatrix);
                        Matrix worldViewProj;

                        Matrix rotateMatrix = Matrix.Identity;
                        //rotateMatrix.Transpose();
                        Matrix.Multiply(ref rotateMatrix, ref viewProj, out worldViewProj);

                        /*viewMatrix.Transpose();
                        var viewProj = Matrix.Multiply(viewMatrix, _projectionMatrix);
                        Matrix rotateMatrix = Matrix.Identity;
                        Matrix worldViewProj;
                        rotateMatrix.Transpose();
                        Matrix.Multiply(ref rotateMatrix, ref viewProj, out worldViewProj);
                        //worldViewProj.Transpose();*/


                        arrayofworldmatrix[0].viewmatrix = viewMatrix;
                        arrayofworldmatrix[0].projectionmatrix = oriProjectionMatrix;
                        arrayofworldmatrix[0].worldmatrix = D3D.WorldMatrix;

                        //arrayofworldmatrix[0].projectionmatrix.Transpose();
                        arrayofworldmatrix[0].viewmatrix.Transpose();
                        //arrayofworldmatrix[0].worldmatrix.Transpose();


                        if (updatesec!= null)
                        {
                            int hasfinishedworksystemthread = updatesec.updatescriptssec(Program.somerunapptype, worldViewProj, arrayofworldmatrix);
                            ///Console.WriteLine("drawing");

                        }






                        //arrayofworldmatrix[0].viewmatrix.Transpose();

                        //Console.WriteLine("rendered");
                        /*
                        Matrix someviewmat = viewMatrix;
                        //someviewmat.Transpose();

                        Matrix sometranspose = _projectionMatrix;
                        sometranspose.Transpose();

                        scgraphicssecpackagemessage.viewMatrix = someviewmat;
                        scgraphicssecpackagemessage.projectionMatrix = sometranspose;
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

                        workonikshaders(scgraphicssecpackagemessage);

                        */




                    }


                    //_sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;

                    D3D.result = eyeTexture.SwapTextureSet.Commit();
                    D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to commit the swap chain texture.");


                }


            }
        }

        public void stoprenderdirectxovr()
        {
            if (D3D != null)
            {
                D3D.result = D3D.OVR.SubmitFrame(D3D.sessionPtr, 0L, IntPtr.Zero, ref D3D.layerEyeFov);

                if (D3D != null)
                {
                    if (D3D.OVR != null)
                    {
                        if (D3D.result != null) // && Program.exitedprogram != 1
                        {
                            D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to submit the frame of the current layers.");

                        }
                    }
                    D3D.DeviceContext.CopyResource(D3D.mirrorTextureD3D, D3D.BackBuffer);
                    D3D.SwapChain.Present(0, SharpDX.DXGI.PresentFlags.None);
                }
            }
        }










        ~updatePrim()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // so that Dispose(false) isn't called later
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose all owned managed objects
            }

            // Release unmanaged resources
        }      
    }
}
