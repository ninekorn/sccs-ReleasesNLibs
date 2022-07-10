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


namespace sccsr15forms
{
    public class updatePrim : IDisposable
    {
        public static updatePrim currentupdatePrim;
        public directx D3D;
        public Camera camera;

        public DTextClass Text;

        updateSec updatesec;
        public updatePrim(directx D3D_) //, updateSec updatesec_
        {
            currentupdatePrim = this;
            D3D = D3D_;

            //updatesec = updateSec?.currentupdatesec;
            updatesec = Program.updatesec;


            camera = new Camera();

            //camera.SetPosition(0, 0, -5);
            camera.SetPosition(0, 0, -1.0f);
            camera.SetRotation(0, 0, 0);


            camera.Render();
            var baseViewMatrix = camera.ViewMatrix;
            //camera.rotationMatrix = baseViewMatrix;
            // Create the text object.
            Text = new DTextClass();

            //if (!Text.Initialize(D3D.Device, D3D.DeviceContext, D3D.apphandle, D3D.SurfaceWidth, D3D.SurfaceHeight, baseViewMatrix))
            //    return false;
            Text.Initialize(D3D.Device, D3D.DeviceContext, D3D.apphandle, D3D.SurfaceWidth, D3D.SurfaceHeight, baseViewMatrix);

            //camera.SetPosition(0, 6, -9);
            camera.SetPosition(0, 1, 0);
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



        float speedRot = 0.075f;
        float speedPos = 0.0015f;
        float rotx = 0;
        float roty = 0;
        float rotz = 0;

        int canmovecamera = 1;
        public Vector3 movePos = Vector3.Zero;
        Vector3 originPos = new Vector3(0, 0, 0);
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











        public void startrender()
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

        public void stoprender()
        {
            if (D3D != null)
            {
                if (D3D.SwapChain != null)
                {
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
