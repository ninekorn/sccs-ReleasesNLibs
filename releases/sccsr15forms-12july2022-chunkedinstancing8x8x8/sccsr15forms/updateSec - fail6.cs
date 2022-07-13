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
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Direct2D1;
using Key = SharpDX.DirectInput.Key;

using System.Runtime.InteropServices;

namespace sccsr15forms
{
    internal class updateSec : IDisposable
    {
        public static updateSec currentupdatesec;// = Instance;

        public tutorialcube somecube;
        private directx D3D;
        public updatePrim updateprim;




        Action<int> RenderDeferredchunk;
        Action<int, int, int> RenderRowchunk;
        Action SetupPipelinechunk;



        Func<int, int, int, int> renderrow;
        Action<int> RenderDeferred;
        //Action<int, int, int> RenderRow;
        Action SetupPipeline;

        public bool switchToNextState = false;

        float speedRot = 0.015f;
        float speedPos = 0.015f;
        float rotx = 0;
        float roty = 0;
        float rotz = 0;

        int canmovecamera = 1;
        Vector3 movePos = Vector3.Zero;
        Vector3 originPos = new Vector3(0,0,0);
        Vector3 OFFSETPOS = Vector3.Zero;



        public int somelevelgenprimw = 1;
        public int somelevelgenprimh = 1;
        public int somelevelgenprimd = 1;
        public sclevelgenclassPrim[] somelevelgenprim;
        public sclevelgenglobals somelevelgenprimglobals;


        int activatelevelgen = 1;
        float RotationInstScreenx = 0;
        float RotationInstScreeny = 0;
        float RotationInstScreenz = 0;
        float levelgenplanesize = 1.0f;

        Vector4 ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
        Vector4 diffuseColour = new Vector4(1, 1, 1, 1);


        public updateSec(updatePrim updateprim_, directx D3D_)
        {
            D3D = D3D_;
            currentupdatesec = this;
            updateprim = updateprim_;

            Console.WriteLine("created updatesec");

            somecube = new tutorialcube(D3D.Device);






            //

            if (activatelevelgen == 1)
            {

                RotationInstScreenx = 0;
                RotationInstScreeny = 0;
                RotationInstScreenz = 0;
                //pitcher = oriRotationScreenX0 * 0.0174532925f;
                //yawer = oriRotationScreenY0 * 0.0174532925f;
                //roller = oriRotationScreenZ0 * 0.0174532925f;
                var pitcher0 = (float)(Math.PI * (RotationInstScreenx) / 180.0f);
                var yawer0 = (float)(Math.PI * (RotationInstScreeny) / 180.0f);
                var roller0 = (float)(Math.PI * (RotationInstScreenz) / 180.0f);
                var somematrixroter = SharpDX.Matrix.RotationYawPitchRoll(yawer0, pitcher0, roller0);

                //VOXEL VIRTUAL DESKTOP
                //VOXEL VIRTUAL DESKTOP
                //VOXEL VIRTUAL DESKTOP
                somelevelgenprimglobals = new sclevelgenglobals();

                somelevelgenprimglobals.planeSize = 0.01f; // * 10

                levelgenplanesize = somelevelgenprimglobals.planeSize;



                /*
                somelevelgenprimglobals.widthlod0 = 7;
                somelevelgenprimglobals.heightlod0 = 7;
                somelevelgenprimglobals.depthlod0 = 7;

                somelevelgenprimglobals.widthlod1 = 3;
                somelevelgenprimglobals.heightlod1 = 3;
                somelevelgenprimglobals.depthlod1 = 3;

                somelevelgenprimglobals.widthlod2 = 2;
                somelevelgenprimglobals.heightlod2 = 2;
                somelevelgenprimglobals.depthlod2 = 2;

                somelevelgenprimglobals.widthlod3 = 1;
                somelevelgenprimglobals.heightlod3 = 1;
                somelevelgenprimglobals.depthlod3 = 1;*/






                /*
                somelevelgenprimglobals.widthlod0 = 6;
                somelevelgenprimglobals.heightlod0 = 6;
                somelevelgenprimglobals.depthlod0 = 6;

                somelevelgenprimglobals.widthlod1 = 3;
                somelevelgenprimglobals.heightlod1 = 3;
                somelevelgenprimglobals.depthlod1 = 3;

                somelevelgenprimglobals.widthlod2 = 2;
                somelevelgenprimglobals.heightlod2 = 2;
                somelevelgenprimglobals.depthlod2 = 2;

                somelevelgenprimglobals.widthlod3 = 1;
                somelevelgenprimglobals.heightlod3 = 1;
                somelevelgenprimglobals.depthlod3 = 1;*/



                /*
                somelevelgenprimglobals.widthlod0 = 20;
                somelevelgenprimglobals.heightlod0 = 20;
                somelevelgenprimglobals.depthlod0 = 20;

                somelevelgenprimglobals.widthlod1 = 10;
                somelevelgenprimglobals.heightlod1 = 10;
                somelevelgenprimglobals.depthlod1 = 10;

                somelevelgenprimglobals.widthlod2 = 6;
                somelevelgenprimglobals.heightlod2 = 6;
                somelevelgenprimglobals.depthlod2 = 6;

                somelevelgenprimglobals.widthlod3 = 5;
                somelevelgenprimglobals.heightlod3 = 5;
                somelevelgenprimglobals.depthlod3 = 5;*/




                somelevelgenprimglobals.widthlod0 = 10;
                somelevelgenprimglobals.heightlod0 = 10;
                somelevelgenprimglobals.depthlod0 = 10;

                somelevelgenprimglobals.widthlod1 = 5;
                somelevelgenprimglobals.heightlod1 = 5;
                somelevelgenprimglobals.depthlod1 = 5;

                somelevelgenprimglobals.widthlod2 = 3;
                somelevelgenprimglobals.heightlod2 = 3;
                somelevelgenprimglobals.depthlod2 = 3;

                somelevelgenprimglobals.widthlod3 = 2;
                somelevelgenprimglobals.heightlod3 = 2;
                somelevelgenprimglobals.depthlod3 = 2;



                //somelevelgenprimglobals.tinyChunkWidth = 10; // 4  // 8 // 4  // 8
                //somelevelgenprimglobals.tinyChunkHeight = 10; // 4 // 8 // 4 // 8
                //somelevelgenprimglobals.tinyChunkDepth = 10; // 4 // 8 // 1 // 1
                //somelevelgenprimglobals.numberOfInstancesPerObjectInWidth = 1; //256 // 512 // 128 //480 //120 // 240
                //somelevelgenprimglobals.numberOfInstancesPerObjectInHeight = 1; //128 // 256 // 72 //270 //72 // 135
                //somelevelgenprimglobals.numberOfInstancesPerObjectInDepth = 1;
                //somelevelgenprimglobals.numberOfObjectInWidth = 1;
                //somelevelgenprimglobals.numberOfObjectInHeight = 1;
                //somelevelgenprimglobals.numberOfObjectInDepth = 1;

                somelevelgenprim = new sclevelgenclassPrim[somelevelgenprimw * somelevelgenprimh * somelevelgenprimd];

                //Vector3 originvirtualdesktoppos = new Vector3(0, 0.5f, 15);
                //Vector3 originsomechunksceneposition = new Vector3(0, 0, 0);

                var pitch = (float)(Math.PI * (0) / 180.0f); //25 //5
                var yaw = (float)(Math.PI * (0) / 180.0f);
                var roll = (float)(Math.PI * (0) / 180.0f);

                var somematrixrot = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                var somematrixrotfake = SharpDX.Matrix.RotationYawPitchRoll(0, 0, 0);
                //somematrixrot = Matrix.Identity;

                //Vector3 originvirtualdesktoppos = new Vector3(0, 0.5f, 15);

                //var posoffsetx = somelevelgenprimglobals.numberOfInstancesPerObjectInWidth * somelevelgenprimglobals.tinyChunkWidth * somelevelgenprimglobals.planeSize * 0.5f;
                //var posoffsety = somelevelgenprimglobals.numberOfInstancesPerObjectInHeight * somelevelgenprimglobals.tinyChunkHeight * somelevelgenprimglobals.planeSize * 0.5f;

                var originvirtualdesktoppos = new Vector3(0, 0, 0);
                //Vector3 originvirtualdesktoppos = new Vector3(0, 0, 0);

                //originvirtualdesktoppos.X += sccs.scgraphics.scupdate.originPosScreen.X;
                //originvirtualdesktoppos.Y += sccs.scgraphics.scupdate.originPosScreen.Y - 0.05f;
                //originvirtualdesktoppos.Z += -sccs.scgraphics.scupdate.originPosScreen.Z - 0.15f;

                //originvirtualdesktoppos.X = 0.25f; // somevoxelvirtualdesktop[0].worldmatofobj.M41 - originvirtualdesktoppos.X + 0.5f
                //originvirtualdesktoppos.Y = 0; //somevoxelvirtualdesktop[0].worldmatofobj.M42 - 0.05f
                //originvirtualdesktoppos.Z = 0.15f; //somevoxelvirtualdesktop[0].worldmatofobj.M43 - 0.15f

                //Vector3 originvirtualdesktoppos = new Vector3(-0.4f, 0.4f, 0.4f);
                //Vector3 originvirtualdesktoppos = new Vector3(1.333333f * somelevelgenprimglobals.numberOfInstancesPerObjectInWidth * somelevelgenprimglobals.numberOfObjectInWidth * somelevelgenprimglobals.tinyChunkWidth * somelevelgenprimglobals.planeSize, 0, 5); // somelevelgenprimglobals.numberOfInstancesPerObjectInDepth * somelevelgenprimglobals.numberOfObjectInDepth * somelevelgenprimglobals.tinyChunkDepth * somelevelgenprimglobals.planeSize * 10
                //Vector3 originvirtualdesktoppos = new Vector3(-1 * 1.25f * somelevelgenprimglobals.numberOfInstancesPerObjectInWidth * somelevelgenprimglobals.numberOfObjectInWidth * somelevelgenprimglobals.tinyChunkWidth * somelevelgenprimglobals.planeSize, 1, somelevelgenprimglobals.numberOfInstancesPerObjectInDepth * somelevelgenprimglobals.numberOfObjectInDepth * somelevelgenprimglobals.tinyChunkDepth * somelevelgenprimglobals.planeSize * 10f);

                for (int xxx = 0; xxx < somelevelgenprimw; xxx++)
                {
                    for (int yyy = 0; yyy < somelevelgenprimh; yyy++)
                    {
                        for (int zzz = 0; zzz < somelevelgenprimd; zzz++)
                        {
                            int somevoxelindex = xxx + somelevelgenprimw * (yyy + somelevelgenprimh * zzz);

                            //Vector3 somechunkpriminstancepos = new Vector3(xxx, yyy, zzz) * SC_Globals.numberOfInstancesPerObjectInWidth * SC_Globals.numberOfObjectInWidth * SC_Globals.tinyChunkWidth * SC_Globals.planeSize*4;
                            Vector3 somevoxelposition = new Vector3(xxx, yyy, zzz);

                            //somevoxelposition.X *= somelevelgenprimglobals.numberOfInstancesPerObjectInWidth * somelevelgenprimglobals.numberOfObjectInWidth * somelevelgenprimglobals.tinyChunkWidth * somelevelgenprimglobals.planeSize;
                            //somevoxelposition.Y *= somelevelgenprimglobals.numberOfInstancesPerObjectInHeight * somelevelgenprimglobals.numberOfObjectInHeight * somelevelgenprimglobals.tinyChunkHeight * somelevelgenprimglobals.planeSize;
                            //somevoxelposition.Z *= somelevelgenprimglobals.numberOfInstancesPerObjectInDepth * somelevelgenprimglobals.numberOfObjectInDepth * somelevelgenprimglobals.tinyChunkDepth * somelevelgenprimglobals.planeSize;

                            somevoxelposition += originvirtualdesktoppos;


                            float tempwidth = 0.1f;
                            float tempheight = 0.1f;

                            somematrixrot.M41 = somevoxelposition.X;
                            somematrixrot.M42 = somevoxelposition.Y;
                            somematrixrot.M43 = somevoxelposition.Z;

                            /*Vector3 originvirtualdesktopposother = new Vector3(-posoffsetx, -posoffsety, 0);
                            originvirtualdesktopposother.X += sccs.scgraphics.scupdate.originPosScreen.X;
                            originvirtualdesktopposother.Y += sccs.scgraphics.scupdate.originPosScreen.Y;
                            originvirtualdesktopposother.Z += -sccs.scgraphics.scupdate.originPosScreen.Z;

                            somematrixrot.M41 += originvirtualdesktopposother.X;
                            somematrixrot.M42 += originvirtualdesktopposother.Y;
                            somematrixrot.M43 += originvirtualdesktopposother.Z;*/

                            somematrixrotfake.M41 = somevoxelposition.X;
                            somematrixrotfake.M42 = somevoxelposition.Y;
                            somematrixrotfake.M43 = somevoxelposition.Z;


                            int shaderswtc = 0;
                            int voxeltype = 0;

                            /*if (Program._useOculusRift == 1 && somelevelgenprimglobals.tinyChunkWidth == 8 && somevoxelvirtualdesktopw == 1)
                            {
                                shaderswtc = 3;
                                voxeltype = 0;
                            }
                            else if (Program._useOculusRift == 0 && somelevelgenprimglobals.tinyChunkWidth == 8 && somevoxelvirtualdesktopw == 1)
                            {
                                shaderswtc = 3;
                                voxeltype = 0;
                            }
                            else if (Program._useOculusRift == 1 && somelevelgenprimglobals.tinyChunkWidth == 4 && somevoxelvirtualdesktopw == 1)
                            {
                                shaderswtc = 3;
                                voxeltype = 0;
                            }
                            else if (Program._useOculusRift == 0 && somelevelgenprimglobals.tinyChunkWidth == 4 && somevoxelvirtualdesktopw == 1)
                            {
                                shaderswtc = 2;
                                voxeltype = 0;
                            }
                            else if (Program._useOculusRift == 1 && somelevelgenprimglobals.tinyChunkWidth == 8 && somevoxelvirtualdesktopw == 2)
                            {
                                shaderswtc = 3;
                                voxeltype = 3;
                            }
                            else if (Program._useOculusRift == 0 && somelevelgenprimglobals.tinyChunkWidth == 8 && somevoxelvirtualdesktopw == 2)
                            {
                                shaderswtc = 3;
                                voxeltype = 3;
                            }
                            else if (Program._useOculusRift == 1 && somelevelgenprimglobals.tinyChunkWidth == 4 && somevoxelvirtualdesktopw == 2)
                            {
                                shaderswtc = 3;
                                voxeltype = 3;
                            }
                            else if (Program._useOculusRift == 0 && somelevelgenprimglobals.tinyChunkWidth == 4 && somevoxelvirtualdesktopw == 2)
                            {
                                shaderswtc = 2;
                                voxeltype = 3;
                            }*/

                            /*if (Program._useOculusRift == 1 && somelevelgenprimglobals.tinyChunkWidth == 8 && activatevrheightmapfeature == 1)
                            {
                                shaderswtc = 3;
                                voxeltype = 0;
                            }
                            else*/
                            /*if (Program._useOculusRift == 0 && somelevelgenprimglobals.tinyChunkWidth == 8 && activatevrheightmapfeature == 1)
                            {
                                shaderswtc = 3;
                                voxeltype = 0;
                            }
                            else if (Program._useOculusRift == 1 && somelevelgenprimglobals.tinyChunkWidth == 4 && activatevrheightmapfeature == 1)
                            {
                                shaderswtc = 3;
                                voxeltype = 0;
                            }
                            else if (Program._useOculusRift == 0 && somelevelgenprimglobals.tinyChunkWidth == 4 && activatevrheightmapfeature == 1)
                            {
                                shaderswtc = 2;
                                voxeltype = 0;
                            }
                            else if (Program._useOculusRift == 1 && somelevelgenprimglobals.tinyChunkWidth == 16 && activatevrheightmapfeature == 1) //NOT DONE YET
                            {
                                shaderswtc = 3; //5
                                voxeltype = 0;
                            }
                            else if (Program._useOculusRift == 0 && somelevelgenprimglobals.tinyChunkWidth == 16 && activatevrheightmapfeature == 1)
                            {
                                shaderswtc = 3; //4
                                voxeltype = 0;
                            }
                            else if (Program._useOculusRift == 0 && somelevelgenprimglobals.tinyChunkWidth == 8 && activatevrheightmapfeature == 0)
                            {
                                shaderswtc = 5; //4
                                voxeltype = 0;
                            }
                            else if (Program._useOculusRift == 1 && somelevelgenprimglobals.tinyChunkWidth == 8 && activatevrheightmapfeature == 0 ||
                                Program._useOculusRift == 1 && somelevelgenprimglobals.tinyChunkWidth == 8 && activatevrheightmapfeature == 1)
                            {
                                shaderswtc = 6; //4
                                voxeltype = 0;
                            }*/

                            voxeltype = 0;
                            shaderswtc = 0;
                            int swtcsetneighboors = -1;
                            float offsetinstances = 1.0f;
                            int typeofvoxelmesh = 1;
                            int fullface = 1;
                            //benchmarking and instancing chunked virtual desktops.
                            //benchmarking and instancing chunked virtual desktops.
                            //benchmarking and instancing chunked virtual desktops.
                            somelevelgenprim[somevoxelindex] = new sclevelgenclassPrim();
                            somelevelgenprim[somevoxelindex].createChunk(D3D, tempwidth, tempheight, new Vector3(0, 10, 0), new Vector3(0, -1, 0), somevoxelposition, somevoxelindex, 0, 100, null, false, directx.BodyTag.physicsinstancedvertexbindingchunk, somelevelgenprimw, somelevelgenprimh, somelevelgenprimd, somelevelgenprimglobals.planeSize, shaderswtc, offsetinstances, fullface, 1, somevoxelindex, voxeltype, somematrixrot, 1, xxx, yyy, zzz, typeofvoxelmesh, swtcsetneighboors);
                            //benchmarking and instancing chunked virtual desktops.
                            //benchmarking and instancing chunked virtual desktops.
                            //benchmarking and instancing chunked virtual desktops.


                            //SET ROTATION BASED ON SHADER ROTATION; //FAKE ROTATION FOR CPU WORK INTERSECT
                            somelevelgenprim[somevoxelindex].currentWorldMatrix = somematrixrotfake;// somelevelgenprim[somevoxelindex].worldmatofobj;
                            somelevelgenprim[somevoxelindex].currentWorldMatrix.M41 = somelevelgenprim[somevoxelindex].worldmatofobj.M41;
                            somelevelgenprim[somevoxelindex].currentWorldMatrix.M42 = somelevelgenprim[somevoxelindex].worldmatofobj.M42;
                            somelevelgenprim[somevoxelindex].currentWorldMatrix.M43 = somelevelgenprim[somevoxelindex].worldmatofobj.M43;


                            /*
                            //originvirtualdesktoppos = new Vector3(-posoffsetx, -posoffsety, 0);
                            somelevelgenprim[somevoxelindex].currentWorldMatrix.M41 = somevoxelposition.X + posoffsetx; // somelevelgenprim[somevoxelindex].worldmatofobj.M41;
                            somelevelgenprim[somevoxelindex].currentWorldMatrix.M42 = somevoxelposition.Y + posoffsety; // somelevelgenprim[somevoxelindex].worldmatofobj.M42;
                            somelevelgenprim[somevoxelindex].currentWorldMatrix.M43 = somevoxelposition.Z ; // somelevelgenprim[somevoxelindex].worldmatofobj.M43;
                            */
                            /*
                            somelevelgenprim[somevoxelindex].currentWorldMatrix.M41 = somelevelgenprim[somevoxelindex].worldmatofobj.M41;
                            somelevelgenprim[somevoxelindex].currentWorldMatrix.M42 = somelevelgenprim[somevoxelindex].worldmatofobj.M42;
                            somelevelgenprim[somevoxelindex].currentWorldMatrix.M43 = somelevelgenprim[somevoxelindex].worldmatofobj.M43;
                            */
                            Matrix someinitmat = somelevelgenprim[somevoxelindex].worldmatofobj;
                            Quaternion somedirquat;
                            Quaternion.RotationMatrix(ref someinitmat, out somedirquat);

                            var dirInstanceRight = sc_maths._newgetdirleft(somedirquat);
                            var dirInstanceUp = sc_maths._newgetdirup(somedirquat);
                            var dirInstanceForward = sc_maths._newgetdirforward(somedirquat);

                            //position = position - (new Vector4(dirInstanceRight.X, dirInstanceRight.Y, dirInstanceRight.Z, 1.0f) * (tinyChunkWidth * planeSize * 0.5f));
                            //position = position - (new Vector4(dirInstanceUp.X, dirInstanceUp.Y, dirInstanceUp.Z, 1.0f) * (tinyChunkWidth * planeSize * 0.5f));
                            //position = position + (new Vector4(dirInstanceForward.X, dirInstanceForward.Y, dirInstanceForward.Z, 1.0f) * (tinyChunkWidth * planeSize * 0.5f));

                            Vector4 somevecx = new Vector4(dirInstanceRight.X, dirInstanceRight.Y, dirInstanceRight.Z, 1.0f);
                            Vector4 somevecy = new Vector4(dirInstanceUp.X, dirInstanceUp.Y, dirInstanceUp.Z, 1.0f);
                            Vector4 somevecz = new Vector4(dirInstanceForward.X, dirInstanceForward.Y, dirInstanceForward.Z, 1.0f);

                            Vector4 somemainpos = new Vector4(someinitmat.M41, someinitmat.M42, someinitmat.M43, 1.0f);
                            Vector4 someinstancepostest = somemainpos;// Vector4.Zero;

                            //WARNING - USING THE SAME LOOP AS THE CHUNK CREATION IS DANGEROUS SINCE THIS MAIN LOOP X/Y/Z IS FOR THE ENTIRE POSITION SETUP AND NOT THE TINYCHUNKWIDTH/BYTECHUNKWIDTH SO IF ADDING ANYTHING OTHER THAN +1 ON THIS LOOP ITERATOR, IT WILL BREAK PROBABLY. SO JUST DON'T CHANGE THE MAIN LOOP AND IT SHOULD BE FINE, INSTEAD IF YOU WANT TO CHANGE THE POSITION, DO IT SO FROM THE VECTOR somechunkmeshpos. STEVE CHASSÉ AKA NINEKORN AKA NINE AKA 9 - 2021-AUGUST-18 NOTES
                            //WARNING - USING THE SAME LOOP AS THE CHUNK CREATION IS DANGEROUS SINCE THIS MAIN LOOP X/Y/Z IS FOR THE ENTIRE POSITION SETUP AND NOT THE TINYCHUNKWIDTH/BYTECHUNKWIDTH SO IF ADDING ANYTHING OTHER THAN +1 ON THIS LOOP ITERATOR, IT WILL BREAK PROBABLY. SO JUST DON'T CHANGE THE MAIN LOOP AND IT SHOULD BE FINE, INSTEAD IF YOU WANT TO CHANGE THE POSITION, DO IT SO FROM THE VECTOR somechunkmeshpos. STEVE CHASSÉ AKA NINEKORN AKA NINE AKA 9 - 2021-AUGUST-18 NOTES
                            //WARNING - USING THE SAME LOOP AS THE CHUNK CREATION IS DANGEROUS SINCE THIS MAIN LOOP X/Y/Z IS FOR THE ENTIRE POSITION SETUP AND NOT THE TINYCHUNKWIDTH/BYTECHUNKWIDTH SO IF ADDING ANYTHING OTHER THAN +1 ON THIS LOOP ITERATOR, IT WILL BREAK PROBABLY. SO JUST DON'T CHANGE THE MAIN LOOP AND IT SHOULD BE FINE, INSTEAD IF YOU WANT TO CHANGE THE POSITION, DO IT SO FROM THE VECTOR somechunkmeshpos. STEVE CHASSÉ AKA NINEKORN AKA NINE AKA 9 - 2021-AUGUST-18 NOTES
                            someinstancepostest = someinstancepostest + (somevecx * (1.0f * somelevelgenprimglobals.widthlod0 * somelevelgenprimglobals.planeSize)); //x
                            someinstancepostest = someinstancepostest + (somevecy * (1.0f * somelevelgenprimglobals.heightlod0 * somelevelgenprimglobals.planeSize)); //y
                            someinstancepostest = someinstancepostest + (somevecz * (1.0f * somelevelgenprimglobals.depthlod0 * somelevelgenprimglobals.planeSize)); //z
                            //WARNING - USING THE SAME LOOP AS THE CHUNK CREATION IS DANGEROUS SINCE THIS MAIN LOOP X/Y/Z IS FOR THE ENTIRE POSITION SETUP AND NOT THE TINYCHUNKWIDTH/BYTECHUNKWIDTH SO IF ADDING ANYTHING OTHER THAN +1 ON THIS LOOP ITERATOR, IT WILL BREAK PROBABLY. SO JUST DON'T CHANGE THE MAIN LOOP AND IT SHOULD BE FINE, INSTEAD IF YOU WANT TO CHANGE THE POSITION, DO IT SO FROM THE VECTOR somechunkmeshpos. STEVE CHASSÉ AKA NINEKORN AKA NINE AKA 9 - 2021-AUGUST-18 NOTES
                            //WARNING - USING THE SAME LOOP AS THE CHUNK CREATION IS DANGEROUS SINCE THIS MAIN LOOP X/Y/Z IS FOR THE ENTIRE POSITION SETUP AND NOT THE TINYCHUNKWIDTH/BYTECHUNKWIDTH SO IF ADDING ANYTHING OTHER THAN +1 ON THIS LOOP ITERATOR, IT WILL BREAK PROBABLY. SO JUST DON'T CHANGE THE MAIN LOOP AND IT SHOULD BE FINE, INSTEAD IF YOU WANT TO CHANGE THE POSITION, DO IT SO FROM THE VECTOR somechunkmeshpos. STEVE CHASSÉ AKA NINEKORN AKA NINE AKA 9 - 2021-AUGUST-18 NOTES
                            //WARNING - USING THE SAME LOOP AS THE CHUNK CREATION IS DANGEROUS SINCE THIS MAIN LOOP X/Y/Z IS FOR THE ENTIRE POSITION SETUP AND NOT THE TINYCHUNKWIDTH/BYTECHUNKWIDTH SO IF ADDING ANYTHING OTHER THAN +1 ON THIS LOOP ITERATOR, IT WILL BREAK PROBABLY. SO JUST DON'T CHANGE THE MAIN LOOP AND IT SHOULD BE FINE, INSTEAD IF YOU WANT TO CHANGE THE POSITION, DO IT SO FROM THE VECTOR somechunkmeshpos. STEVE CHASSÉ AKA NINEKORN AKA NINE AKA 9 - 2021-AUGUST-18 NOTES

                            someinitmat.M41 = someinstancepostest.X;
                            someinitmat.M42 = someinstancepostest.Y;
                            someinitmat.M43 = someinstancepostest.Z;

                            somelevelgenprim[somevoxelindex].worldmatofobj = someinitmat;

                            somelevelgenprim[somevoxelindex].lightBufferInstChunk[0] = new sclevelgenclassPrim.DLightBufferEr()
                            {
                                ambientColor = ambientColor,
                                diffuseColor = diffuseColour,
                                lightDirection = Vector4.Zero,
                                //padding0 = 0,
                                lightPosition = Vector4.Zero// lightpos,
                                                            //padding1 = 100
                            };

                            somelevelgenprim[somevoxelindex].arrayOfMatrixBuff[0] = new sclevelgenclassPrim.DMatrixBuffer()
                            {
                                world = Matrix.Identity,
                                view = Matrix.Identity,
                                proj = Matrix.Identity,
                            };


                            //somevoxelvirtualdesktop[somevoxelindex].somechunk = null;
                            //somevoxelvirtualdesktop[somevoxelindex].sccstrigvertbuilder = null;
                        }
                    }
                }
                //VOXEL VIRTUAL DESKTOP
                //VOXEL VIRTUAL DESKTOP
                //VOXEL VIRTUAL DESKTOP

            }


            D3D.currentState.CountCubes = somelevelgenprim[0].arrayOfChunkDatalod0.Length;




























            movePos = updateprim_.camera.GetPosition();

            const float viewZ = 5.0f;
            //updateprim_.camera.ViewMatrix;//
            // Prepare matrices 
            /*var view = updateprim_.camera.ViewMatrix;// Matrix.LookAtLH(new Vector3(0, 0, -viewZ), new Vector3(0, 0, 0), Vector3.UnitY);
            var proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, D3D.SurfaceWidth / (float)D3D.SurfaceHeight, 0.1f, 1000.0f);

            var viewProj = Matrix.Multiply(view, D3D.ProjectionMatrix);*/

            // --------------------------------------------------------------------------------------
            // Register KeyDown event handler on the form
            // --------------------------------------------------------------------------------------
            switchToNextState = false;







            
            // --------------------------------------------------------------------------------------
            // Function used to setup the pipeline
            // --------------------------------------------------------------------------------------
            SetupPipeline = () =>
            {
                hasfinishedSetupPipeline = 0;
                int threadCount = 1;
                if (D3D.currentState.Type != directx.TestType.Immediate)
                {
                    threadCount = D3D.currentState.Type == directx.TestType.Deferred ? D3D.currentState.ThreadCount : 1;
                    Array.Copy(D3D.deferredContexts, D3D.contextPerThread, D3D.contextPerThread.Length);
                }
                else
                {
                    D3D.contextPerThread[0] = D3D.DeviceContext;
                }
                for (int i = 0; i < threadCount; i++)
                {
                    var renderingContext = D3D.contextPerThread[i];
                    // Prepare All the stages 
                    renderingContext.InputAssembler.InputLayout = somecube.layout;
                    renderingContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
                    renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecube.verticesbuffer, Utilities.SizeOf<Vector4>() * 2, 0));
                    renderingContext.VertexShader.SetConstantBuffer(0, D3D.currentState.UseMap ? somecube.dynamicConstantBuffer : somecube.staticContantBuffer);
                    renderingContext.VertexShader.Set(somecube.vertexShader);
                    renderingContext.Rasterizer.SetViewport(0, 0, D3D.SurfaceWidth, D3D.SurfaceHeight);
                    renderingContext.PixelShader.Set(somecube.pixelShader);
                    renderingContext.OutputMerger.SetTargets(D3D.DepthStencilView, D3D.RenderTargetView);
                }
                hasfinishedSetupPipeline = 1;
            };

            renderrow = (contextIndex, fromY, toY) =>
            {

                hasfinishedRenderRow = 0;
                var renderingContext = D3D.contextPerThread[contextIndex];
                var time = Program.clock.ElapsedMilliseconds / 1000.0f;

                if (contextIndex == 0)
                {
                    D3D.contextPerThread[0].ClearDepthStencilView(D3D.DepthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);
                    D3D.contextPerThread[0].ClearRenderTargetView(D3D.RenderTargetView, SharpDX.Color.Black);
                }

                updateprim_.camera.Render();

                var view = updateprim_.camera.ViewMatrix;// Matrix.LookAtLH(new Vector3(0, 0, -viewZ), new Vector3(0, 0, 0), Vector3.UnitY);
                var proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, D3D.SurfaceWidth / (float)D3D.SurfaceHeight, 0.1f, 1000.0f);


                var viewProj = Matrix.Multiply(view, D3D.ProjectionMatrix);


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

                    var somerot = updateprim_.camera.GetRotation();
                    updateprim_.camera.SetRotation(rotx, roty, somerot.Z);



                    Matrix tempmater = updateprim_.camera.rotationMatrix;
                    Quaternion otherQuat;
                    Quaternion.RotationMatrix(ref tempmater, out otherQuat);





                    Vector3 direction_feet_forward;
                    Vector3 direction_feet_right;
                    Vector3 direction_feet_up;

                    direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                    direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);



                    if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Up))
                    {
                        //Program.MessageBox((IntPtr)0, "000", "sc core systems message", 0);
                        //direction_feet_forward.Z += speed * speedPos;
                        movePos -= direction_feet_forward * speedPos;
                    }
                    else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Down))
                    {
                        movePos += direction_feet_forward * speedPos;
                        //direction_feet_forward.Z -= speed * speedPos;
                    }
                    else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Q))
                    {
                        movePos += direction_feet_up * speedPos;
                        //direction_feet_forward.Y += speed * speedPos;
                    }
                    else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Z))
                    {
                        movePos -= direction_feet_up * speedPos;
                        //direction_feet_forward.Y -= speed * speedPos;
                    }
                    else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Left))
                    {
                        movePos -= direction_feet_right * speedPos;
                        //direction_feet_forward.X -= speed * speedPos;
                    }
                    else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Right))
                    {
                        movePos += direction_feet_right * speedPos;
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
                    OFFSETPOS = originPos + movePos;
                    updateprim_.camera.SetPosition(OFFSETPOS.X, OFFSETPOS.Y, OFFSETPOS.Z);
                }





                int count = D3D.currentState.CountCubes;
                float divCubes = (float)count / (viewZ - 1);

                var rotateMatrix = Matrix.Scaling(1.0f / count) * Matrix.RotationX(time) * Matrix.RotationY(time * 2) * Matrix.RotationZ(time * .7f);

                for (int y = fromY; y < toY; y++)
                {
                    for (int x = 0; x < count; x++)
                    {
                        rotateMatrix.M41 = (x + .5f - count * .5f) / divCubes;
                        rotateMatrix.M42 = (y + .5f - count * .5f) / divCubes;

                        // Update WorldViewProj Matrix 
                        Matrix worldViewProj;
                        Matrix.Multiply(ref rotateMatrix, ref viewProj, out worldViewProj);
                        worldViewProj.Transpose();
                        // Simulate CPU usage in order to see benefits of worlViewProj

                        if (D3D.currentState.SimulateCpuUsage)
                        {
                            for (int i = 0; i < directx.BurnCpuFactor; i++)
                            {
                                Matrix.Multiply(ref rotateMatrix, ref viewProj, out worldViewProj);
                                worldViewProj.Transpose();
                            }
                        }

                        if (D3D.currentState.UseMap)
                        {
                            var dataBox = renderingContext.MapSubresource(somecube.dynamicConstantBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
                            Utilities.Write(dataBox.DataPointer, ref worldViewProj);
                            renderingContext.UnmapSubresource(somecube.dynamicConstantBuffer, 0);
                        }
                        else
                        {
                            renderingContext.UpdateSubresource(ref worldViewProj, somecube.staticContantBuffer);
                        }

                        // Draw the cube 
                        renderingContext.Draw(36, 0);
                    }
                }



                if (D3D.currentState.Type != directx.TestType.Immediate)
                    D3D.commandsList[contextIndex] = renderingContext.FinishCommandList(false);

                hasfinishedRenderRow = 1;
                return hasfinishedRenderRow;
            };

            RenderDeferred = (int threadCount) =>
            {
                hasfinishedRenderDeferred = 0;
                int deltaCube = D3D.currentState.CountCubes / threadCount;
                if (deltaCube == 0) deltaCube = 1;
                int nextStartingRow = 0;
                tasks = new Task[threadCount];
                for (int i = 0; i < threadCount; i++)
                {
                    var threadIndex = i;
                    int fromRow = nextStartingRow;
                    int toRow = (i + 1) == threadCount ? D3D.currentState.CountCubes : fromRow + deltaCube;
                    if (toRow > D3D.currentState.CountCubes)
                        toRow = D3D.currentState.CountCubes;
                    nextStartingRow = toRow;

                    tasks[i] = new Task(() => renderrow(threadIndex, fromRow, toRow));
                    tasks[i].Start();
                }

                int somenullval = 0;
                for (int i = 0; i < tasks.Length; i++)
                {
                    if (tasks[i] == null)
                    {
                        somenullval++;
                    }
                }

                if (somenullval == 0)
                {
                    Task.WaitAll(tasks);
                }
                else
                {
                    for (int i = 0; i < tasks.Length; i++)
                    {
                        if (tasks[i] != null)
                        {
                            tasks[i].Wait();
                            tasks[i].Dispose();
                        }

                    }
                }
                //Task.WaitAll(tasks);
                hasfinishedRenderDeferred = 1;
            };

            


            // --------------------------------------------------------------------------------------
            // Function used to setup the pipeline
            // --------------------------------------------------------------------------------------
            SetupPipelinechunk = () =>
            {
                int threadCount = 1;
                if (D3D.currentState.Type != directx.TestType.Immediate)
                {
                    threadCount = D3D.currentState.Type == directx.TestType.Deferred ? D3D.currentState.ThreadCount : 1;
                    Array.Copy(D3D.deferredContexts, D3D.contextPerThread, D3D.contextPerThread.Length);
                }
                else
                {
                    D3D.contextPerThread[0] = D3D.Device.ImmediateContext;
                }


                //int chunknumber = 6500;
                //chunks[] chunks = new chunks[6500];


                for (int ck = 0; ck < somelevelgenprim[0].arrayOfChunkDatalod0.Length; ck++)
                {
                    //chunks[ck] = new chunks();

                    for (int i = 0; i < threadCount; i++)
                    {
                        var renderingContext = D3D.contextPerThread[i];
                        // Prepare All the stages 
                        renderingContext.InputAssembler.InputLayout = somelevelgenprim[0].arrayOfChunkDatalod0[ck].Layout;
                        renderingContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;

                        D3D.Device.ImmediateContext.InputAssembler.SetIndexBuffer(somelevelgenprim[0].arrayOfChunkDatalod0[ck].IndicesBuffer, SharpDX.DXGI.Format.R32_UInt, 0);
                        //renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somelevelgenprim[0].arrayOfChunkDatalod0[ck].vertexBuffer, Utilities.SizeOf<Vector4>() * 2, 0));
                        renderingContext.InputAssembler.SetVertexBuffers(0, new[]
                        {
                            new VertexBufferBinding(somelevelgenprim[0].arrayOfChunkDatalod0[ck].vertexBuffer, Marshal.SizeOf(typeof(sclevelgenclass.DVertex)), 0),
                            //new VertexBufferBinding(chunkdat.indexBuffer, Marshal.SizeOf(typeof(sclevelgenclass.DInstanceType)),0),
                        });
                        renderingContext.VertexShader.SetConstantBuffer(0, D3D.currentState.UseMap ? somelevelgenprim[0].arrayOfChunkDatalod0[ck].dynamicConstantBuffer : somelevelgenprim[0].arrayOfChunkDatalod0[ck].staticConstantBuffer);
                        //renderingContext.VertexShader.SetConstantBuffer(0, somelevelgenprim[0].arrayOfChunkDatalod0[ck].constantMatrixPosBuffer);

                        renderingContext.VertexShader.Set(somelevelgenprim[0].arrayOfChunkDatalod0[ck].VertexShader);
                        renderingContext.Rasterizer.SetViewport(0, 0, sccsr15forms.Form1.currentform.ClientSize.Width, sccsr15forms.Form1.currentform.ClientSize.Height);
                        renderingContext.PixelShader.Set(somelevelgenprim[0].arrayOfChunkDatalod0[ck].PixelShader);
                        renderingContext.GeometryShader.Set(null);

                        renderingContext.OutputMerger.SetTargets(D3D.DepthStencilView, D3D.RenderTargetView);
                    }
                }
            };




            // --------------------------------------------------------------------------------------
            // Function used to render a row of cubes
            // --------------------------------------------------------------------------------------
            RenderRowchunk = (int contextIndex, int fromY, int toY) =>
            {
                var renderingContext = D3D.contextPerThread[contextIndex];
                //var time = clock.ElapsedMilliseconds / 1000.0f;

                if (contextIndex == 0)
                {
                    D3D.contextPerThread[0].ClearDepthStencilView(D3D.DepthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);
                    D3D.contextPerThread[0].ClearRenderTargetView(D3D.RenderTargetView, SharpDX.Color.LightGray);
                }


                var viewMatrix = updateprim_.camera.ViewMatrix;

                D3D.WorldMatrix.Transpose();
                viewMatrix.Transpose();
                D3D.ProjectionMatrix.Transpose();


                somelevelgenprim[0].arrayOfMatrixBuff[0].world = D3D.WorldMatrix;
                somelevelgenprim[0].arrayOfMatrixBuff[0].view = viewMatrix;
                somelevelgenprim[0].arrayOfMatrixBuff[0].proj = D3D.ProjectionMatrix;

                for (int c =0;c < somelevelgenprim[0].arrayOfChunkDatalod0.Length;c++)
                {


                    somelevelgenprim[0].arrayOfChunkDatalod0[c].worldMatrix = D3D.WorldMatrix;
                    somelevelgenprim[0].arrayOfChunkDatalod0[c].viewMatrix = viewMatrix;
                    somelevelgenprim[0].arrayOfChunkDatalod0[c].projectionMatrix = D3D.ProjectionMatrix;
                    somelevelgenprim[0].arrayOfChunkDatalod0[c].matrixBuffer = somelevelgenprim[0].arrayOfMatrixBuff;
                    somelevelgenprim[0].arrayOfChunkDatalod0[c].switchForRender = 2;

                    /*if (somelevelgenprim[0].arrayOfChunkDatalod1 != null)
                    {
                        if (somelevelgenprim[0].arrayOfChunkDatalod1[c] != null)
                        {
                            if (somelevelgenprim[0].arrayOfChunkDatalod1[c].arrayofverts != null)
                            {
                                if (somelevelgenprim[0].arrayOfChunkDatalod1[c].arrayofverts.Length > 0)
                                {
                                  

                                    somelevelgenprim[0].arrayOfChunkDatalod1[c].worldMatrix = D3D.WorldMatrix;
                                    somelevelgenprim[0].arrayOfChunkDatalod1[c].viewMatrix = viewMatrix;
                                    somelevelgenprim[0].arrayOfChunkDatalod1[c].projectionMatrix = D3D.ProjectionMatrix;
                                    somelevelgenprim[0].arrayOfChunkDatalod1[c].matrixBuffer = somelevelgenprim[0].arrayOfMatrixBuff;
                                    somelevelgenprim[0].arrayOfChunkDatalod1[c].switchForRender = 2;
                                }
                            }
                        }
                    }


                    if (somelevelgenprim[0].arrayOfChunkDatalod2 != null)
                    {
                        if (somelevelgenprim[0].arrayOfChunkDatalod2[c] != null)
                        {
                            if (somelevelgenprim[0].arrayOfChunkDatalod2[c].arrayofverts != null)
                            {
                                if (somelevelgenprim[0].arrayOfChunkDatalod2[c].arrayofverts.Length > 0)
                                {
                                    somelevelgenprim[0].arrayOfChunkDatalod2[c].worldMatrix = D3D.WorldMatrix;
                                    somelevelgenprim[0].arrayOfChunkDatalod2[c].viewMatrix = viewMatrix;
                                    somelevelgenprim[0].arrayOfChunkDatalod2[c].projectionMatrix = D3D.ProjectionMatrix;
                                    somelevelgenprim[0].arrayOfChunkDatalod2[c].matrixBuffer = somelevelgenprim[0].arrayOfMatrixBuff;
                                    somelevelgenprim[0].arrayOfChunkDatalod2[c].switchForRender = 2;
                                }
                            }
                        }
                    }



                    if (somelevelgenprim[0].arrayOfChunkDatalod3 != null)
                    {
                        if (somelevelgenprim[0].arrayOfChunkDatalod3[c] != null)
                        {
                            if (somelevelgenprim[0].arrayOfChunkDatalod3[c].arrayofverts != null)
                            {
                                if (somelevelgenprim[0].arrayOfChunkDatalod3[c].arrayofverts.Length > 0)
                                {
                                    somelevelgenprim[0].arrayOfChunkDatalod3[c].worldMatrix = D3D.WorldMatrix;
                                    somelevelgenprim[0].arrayOfChunkDatalod3[c].viewMatrix = viewMatrix;
                                    somelevelgenprim[0].arrayOfChunkDatalod3[c].projectionMatrix = D3D.ProjectionMatrix;
                                    somelevelgenprim[0].arrayOfChunkDatalod3[c].matrixBuffer = somelevelgenprim[0].arrayOfMatrixBuff;
                                    somelevelgenprim[0].arrayOfChunkDatalod3[c].switchForRender = 2;
                                }
                            }
                        }
                    }*/
                }
                //const float viewZ = 5.0f;
                //var view = Matrix.LookAtLH(new Vector3(0, 0, -viewZ), new Vector3(0, 0, 0), Vector3.UnitY);
                //int count = somelevelgenprim[0].arrayOfChunkDatalod0.Length;// currentState.CountCubes;
                //float divCubes = (float)count / (viewZ - 1);

                //var rotateMatrix = Matrix.Scaling(1.0f / count) * Matrix.RotationX(time) * Matrix.RotationY(time * 2) * Matrix.RotationZ(time * .7f);


                //var viewProj = Matrix.Multiply(updateprim.camera.ViewMatrix, D3D.ProjectionMatrix);












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

                    var somerot = updateprim_.camera.GetRotation();
                    updateprim_.camera.SetRotation(rotx, roty, somerot.Z);



                    Matrix tempmater = updateprim_.camera.rotationMatrix;
                    Quaternion otherQuat;
                    Quaternion.RotationMatrix(ref tempmater, out otherQuat);





                    Vector3 direction_feet_forward;
                    Vector3 direction_feet_right;
                    Vector3 direction_feet_up;

                    direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                    direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);



                    if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Up))
                    {
                        //Program.MessageBox((IntPtr)0, "000", "sc core systems message", 0);
                        //direction_feet_forward.Z += speed * speedPos;
                        movePos -= direction_feet_forward * speedPos;
                    }
                    else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Down))
                    {
                        movePos += direction_feet_forward * speedPos;
                        //direction_feet_forward.Z -= speed * speedPos;
                    }
                    else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Q))
                    {
                        movePos += direction_feet_up * speedPos;
                        //direction_feet_forward.Y += speed * speedPos;
                    }
                    else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Z))
                    {
                        movePos -= direction_feet_up * speedPos;
                        //direction_feet_forward.Y -= speed * speedPos;
                    }
                    else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Left))
                    {
                        movePos -= direction_feet_right * speedPos;
                        //direction_feet_forward.X -= speed * speedPos;
                    }
                    else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Right))
                    {
                        movePos += direction_feet_right * speedPos;
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
                    OFFSETPOS = originPos + movePos;
                    updateprim_.camera.SetPosition(OFFSETPOS.X, OFFSETPOS.Y, OFFSETPOS.Z);
                }











                //Console.WriteLine("arraylength:" + somelevelgenprim[0].arrayOfChunkDatalod0.Length);
                int someindex = 0;
                Matrix rotateMatrix = Matrix.Identity;


                for (int y = fromY; y < toY; y++)
                {

                    //Matrix worldViewProj;
                    //Matrix.Multiply(ref rotateMatrix, ref viewProj, out worldViewProj);
                    //worldViewProj.Transpose();


                    int chunkindex = y;
                    if (D3D.currentState.UseMap)
                    {

                        /*
                        DataStream streamerTWO;
                        renderingContext.MapSubresource(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].dynamicConstantBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streamerTWO);
                        streamerTWO.WriteRange(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].matrixBuffer, 0, somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].matrixBuffer.Length);
                        renderingContext.UnmapSubresource(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].dynamicConstantBuffer, 0);
                        streamerTWO.Dispose();*/

                        var someval = somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].matrixBuffer[0];
                        var dataBox = renderingContext.MapSubresource(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].dynamicConstantBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
                        Utilities.Write(dataBox.DataPointer, ref someval);
                        renderingContext.UnmapSubresource(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].dynamicConstantBuffer, 0);
                    }
                    else
                    {
                        
                        /*DataStream streamerTWO;
                        renderingContext.MapSubresource(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].staticConstantBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streamerTWO);
                        streamerTWO.WriteRange(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].matrixBuffer, 0, somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].matrixBuffer.Length);
                        renderingContext.UnmapSubresource(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].staticConstantBuffer, 0);
                        streamerTWO.Dispose();*/
                        var someval = somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].matrixBuffer[0];
                        renderingContext.UpdateSubresource(ref someval, somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].staticConstantBuffer);
                    }
               

                    /*
                    int chunkindex = y;
                    DataStream streamerTWO;
                    D3D.Device.ImmediateContext.MapSubresource(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].constantMatrixPosBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streamerTWO);
                    streamerTWO.WriteRange(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].matrixBuffer, 0, somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].matrixBuffer.Length);
                    D3D.Device.ImmediateContext.UnmapSubresource(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].constantMatrixPosBuffer, 0);
                    streamerTWO.Dispose();

                    DataStream mappedResourceLight;
                    D3D.Device.ImmediateContext.MapSubresource(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].constantLightBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                    mappedResourceLight.WriteRange(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].lightBuffer, 0, somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].lightBuffer.Length);
                    D3D.Device.ImmediateContext.UnmapSubresource(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].constantLightBuffer, 0);
                    mappedResourceLight.Dispose();*/


                    renderingContext.DrawIndexed(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].arrayofindices.Length, 0, 0);
              
                    //renderingContext.Draw(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].arrayofverts.Length, 0);



                    /*
                    D3D.Device.ImmediateContext.MapSubresource(somelevelgenprim[0].arrayOfChunkDatalod1[chunkindex].constantMatrixPosBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streamerTWO);
                    streamerTWO.WriteRange(somelevelgenprim[0].arrayOfChunkDatalod1[chunkindex].matrixBuffer, 0, somelevelgenprim[0].arrayOfChunkDatalod1[chunkindex].matrixBuffer.Length);
                    D3D.Device.ImmediateContext.UnmapSubresource(somelevelgenprim[0].arrayOfChunkDatalod1[chunkindex].constantMatrixPosBuffer, 0);
                    streamerTWO.Dispose();

                    D3D.Device.ImmediateContext.MapSubresource(somelevelgenprim[0].arrayOfChunkDatalod1[chunkindex].constantLightBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                    mappedResourceLight.WriteRange(somelevelgenprim[0].arrayOfChunkDatalod1[chunkindex].lightBuffer, 0, somelevelgenprim[0].arrayOfChunkDatalod1[chunkindex].lightBuffer.Length);
                    D3D.Device.ImmediateContext.UnmapSubresource(somelevelgenprim[0].arrayOfChunkDatalod1[chunkindex].constantLightBuffer, 0);
                    mappedResourceLight.Dispose();


                    //renderingContext.DrawIndexed(somelevelgenprim[0].arrayOfChunkDatalod1[chunkindex].arrayofindices.Length, 0, 0);
                    // Draw the cube 
                    renderingContext.Draw(somelevelgenprim[0].arrayOfChunkDatalod1[chunkindex].arrayofverts.Length, 0);*/















                    /* for (int x = 0; x < count; x++)
                     {
                         //to review flat2d index
                         int chunkindex = y;
                         //Console.WriteLine("chunkindex:" + chunkindex);


                         //to review flat2d index

                         DataStream streamerTWO;
                         device.ImmediateContext.MapSubresource(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].constantMatrixPosBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out streamerTWO);
                         streamerTWO.WriteRange(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].matrixBuffer, 0, somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].matrixBuffer.Length);
                         device.ImmediateContext.UnmapSubresource(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].constantMatrixPosBuffer, 0);
                         streamerTWO.Dispose();

                         DataStream mappedResourceLight;
                         device.ImmediateContext.MapSubresource(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].constantLightBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                         mappedResourceLight.WriteRange(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].lightBuffer, 0, somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].lightBuffer.Length);
                         device.ImmediateContext.UnmapSubresource(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].constantLightBuffer, 0);
                         mappedResourceLight.Dispose();

                         // Draw the cube 
                         renderingContext.Draw(somelevelgenprim[0].arrayOfChunkDatalod0[chunkindex].arrayofverts.Length, 0);
                     }*/
                    someindex++;
                }

                if (D3D.currentState.Type != directx.TestType.Immediate)
                {
                    D3D.commandsList[contextIndex] = renderingContext.FinishCommandList(false);

                }
                // Console.WriteLine("max:" + someindex);
            };











            RenderDeferredchunk = (int threadCount) =>
            {
                int deltaCube = somelevelgenprim[0].arrayOfChunkDatalod0.Length / threadCount;
                if (deltaCube == 0) deltaCube = 1;
                int nextStartingRow = 0;
                var tasks = new Task[threadCount];
                for (int i = 0; i < threadCount; i++)
                {
                    var threadIndex = i;
                    int fromRow = nextStartingRow;
                    int toRow = (i + 1) == threadCount ? somelevelgenprim[0].arrayOfChunkDatalod0.Length : fromRow + deltaCube;
                    if (toRow > somelevelgenprim[0].arrayOfChunkDatalod0.Length)
                        toRow = somelevelgenprim[0].arrayOfChunkDatalod0.Length;
                    nextStartingRow = toRow;

                    tasks[i] = new Task(() => RenderRowchunk(threadIndex, fromRow, toRow));
                    tasks[i].Start();
                }
                Task.WaitAll(tasks);
            };



        }

        public Task[] tasks;

        public int hasfinishedSetupPipeline = 1;
        public int hasfinishedRenderRow = 1;
        public int hasfinishedRenderDeferred = 1;

        int hasfinishedwork = 0;

        public int updatescriptssec(bool runapptype)
        {
            hasfinishedwork = 0;
            if (D3D.currentState.Exit)
                sccsr15forms.Form1.currentform.Close();













            
            updateprim.camera.Render();

            var view = updateprim.camera.ViewMatrix;// Matrix.LookAtLH(new Vector3(0, 0, -viewZ), new Vector3(0, 0, 0), Vector3.UnitY);
            var proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, D3D.SurfaceWidth / (float)D3D.SurfaceHeight, 0.1f, 1000.0f);


            var viewProj = Matrix.Multiply(view, D3D.ProjectionMatrix);


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

                var somerot = updateprim.camera.GetRotation();
                updateprim.camera.SetRotation(rotx, roty, somerot.Z);



                Matrix tempmater = updateprim.camera.rotationMatrix;
                Quaternion otherQuat;
                Quaternion.RotationMatrix(ref tempmater, out otherQuat);





                Vector3 direction_feet_forward;
                Vector3 direction_feet_right;
                Vector3 direction_feet_up;

                direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);



                if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Up))
                {
                    //Program.MessageBox((IntPtr)0, "000", "sc core systems message", 0);
                    //direction_feet_forward.Z += speed * speedPos;
                    movePos -= direction_feet_forward * speedPos;
                }
                else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Down))
                {
                    movePos += direction_feet_forward * speedPos;
                    //direction_feet_forward.Z -= speed * speedPos;
                }
                else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Q))
                {
                    movePos += direction_feet_up * speedPos;
                    //direction_feet_forward.Y += speed * speedPos;
                }
                else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Z))
                {
                    movePos -= direction_feet_up * speedPos;
                    //direction_feet_forward.Y -= speed * speedPos;
                }
                else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Left))
                {
                    movePos -= direction_feet_right * speedPos;
                    //direction_feet_forward.X -= speed * speedPos;
                }
                else if (Program.keyboardinput._KeyboardState != null && Program.keyboardinput._KeyboardState.PressedKeys.Contains(Key.Right))
                {
                    movePos += direction_feet_right * speedPos;
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



                //Vector3 somecurrentcampos = updateprim.camera.GetPosition();
                OFFSETPOS = originPos + movePos;
                updateprim.camera.SetPosition(OFFSETPOS.X, OFFSETPOS.Y, OFFSETPOS.Z);
            }










            /*
            var viewMatrix = updateprim.camera.ViewMatrix;

            var worldmat = D3D.WorldMatrix;
            worldmat.Transpose();
            viewMatrix.Transpose();
            var projmat = D3D.ProjectionMatrix;
            projmat.Transpose();


            somelevelgenprim[0].arrayOfMatrixBuff[0].world = worldmat;
            somelevelgenprim[0].arrayOfMatrixBuff[0].view = viewMatrix;
            somelevelgenprim[0].arrayOfMatrixBuff[0].proj = projmat;
            
            
            if (somelevelgenprim[0].arrayOfChunkDatalod0 != null)
            {
                for (int c = 0; c < somelevelgenprim[0].arrayOfChunkDatalod0.Length; c++)
                {
                    if (somelevelgenprim[0].arrayOfChunkDatalod0[c] != null)
                    {
                        if (somelevelgenprim[0].arrayOfChunkDatalod0[c].arrayofverts != null)
                        {
                            if (somelevelgenprim[0].arrayOfChunkDatalod0[c].arrayofverts.Length > 0)
                            {
                                //Console.WriteLine("test");
                                somelevelgenprim[0].arrayOfChunkDatalod0[c].worldMatrix = worldmat;
                                somelevelgenprim[0].arrayOfChunkDatalod0[c].viewMatrix = viewMatrix;
                                somelevelgenprim[0].arrayOfChunkDatalod0[c].projectionMatrix = projmat;
                                somelevelgenprim[0].arrayOfChunkDatalod0[c].matrixBuffer = somelevelgenprim[0].arrayOfMatrixBuff;
                                somelevelgenprim[0].arrayOfChunkDatalod0[c].switchForRender = 2;


                                // Prepare All the stages 
                                D3D.DeviceContext.InputAssembler.InputLayout = somelevelgenprim[0].arrayOfChunkDatalod0[c].Layout;
                                D3D.DeviceContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;

                                D3D.Device.ImmediateContext.InputAssembler.SetIndexBuffer(somelevelgenprim[0].arrayOfChunkDatalod0[c].IndicesBuffer, SharpDX.DXGI.Format.R32_UInt, 0);
                                //D3D.DeviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somelevelgenprim[0].arrayOfChunkDatalod0[c].vertexBuffer, Utilities.SizeOf<Vector4>() * 2, 0));
                                D3D.DeviceContext.InputAssembler.SetVertexBuffers(0, new[]
                                {
                                    new VertexBufferBinding(somelevelgenprim[0].arrayOfChunkDatalod0[c].vertexBuffer, Marshal.SizeOf(typeof(sclevelgenclass.DVertex)), 0),
                                    //new VertexBufferBinding(chunkdat.indexBuffer, Marshal.SizeOf(typeof(sclevelgenclass.DInstanceType)),0),
                                });
                                D3D.DeviceContext.VertexShader.SetConstantBuffer(0, D3D.currentState.UseMap ? somelevelgenprim[0].arrayOfChunkDatalod0[c].dynamicConstantBuffer : somelevelgenprim[0].arrayOfChunkDatalod0[c].staticConstantBuffer);
                                //D3D.DeviceContext.VertexShader.SetConstantBuffer(0, somelevelgenprim[0].arrayOfChunkDatalod0[c].constantMatrixPosBuffer);

                                D3D.DeviceContext.VertexShader.Set(somelevelgenprim[0].arrayOfChunkDatalod0[c].VertexShader);
                                D3D.DeviceContext.Rasterizer.SetViewport(0, 0, sccsr15forms.Form1.currentform.ClientSize.Width, sccsr15forms.Form1.currentform.ClientSize.Height);
                                D3D.DeviceContext.PixelShader.Set(somelevelgenprim[0].arrayOfChunkDatalod0[c].PixelShader);
                                D3D.DeviceContext.GeometryShader.Set(null);

                                D3D.DeviceContext.OutputMerger.SetTargets(D3D.DepthStencilView, D3D.RenderTargetView);



                                D3D.DeviceContext.DrawIndexed(somelevelgenprim[0].arrayOfChunkDatalod0[c].arrayofindices.Length, 0, 0);




                                //somelevelgenprim[0].arrayOfChunkDatalod0[c] = somelevelgenprim[0].shaderOfChunk.Renderer(somelevelgenprim[0].arrayOfChunkDatalod0[c], c, null, 0);
                            }
                        }
                    }
                }
            }*/

















            /*
            fpsCounter++;
            if (fpsTimer.ElapsedMilliseconds > 1000)
            {
                var typeStr = currentState.Type.ToString();
                if (currentState.Type != TestType.Immediate && !supportCommandList) typeStr += "*";

                sccsr14sc.Form1.someform.Text = string.Format("SharpDX - MultiCube D3D11 - (F1) {0} - (F2) {1} - (F3) {2} - Threads ↑↓{3} - Count ←{4}→ - FPS: {5:F2} ({6:F2}ms)", typeStr, currentState.UseMap ? "Map/UnMap" : "UpdateSubresource", currentState.SimulateCpuUsage ? "BurnCPU On" : "BurnCpu Off", currentState.Type == TestType.Deferred ? currentState.ThreadCount : 1, currentState.CountCubes * currentState.CountCubes, 1000.0 * fpsCounter / fpsTimer.ElapsedMilliseconds, (float)fpsTimer.ElapsedMilliseconds / fpsCounter);
                fpsTimer.Reset();
                fpsTimer.Stop();
                fpsTimer.Start();
                fpsCounter = 0;
            }*/




            /*
            SetupPipelinechunk();


            // Execute on the rendering thread when ThreadCount == 1 or No deferred rendering is selected
            if (D3D.currentState.Type == directx.TestType.Immediate || (D3D.currentState.Type == directx.TestType.Deferred && D3D.currentState.ThreadCount == 1))
            {
                RenderRowchunk(0, 0, D3D.currentState.CountCubes);
            }

            // In case of deferred context, use of FinishCommandList / ExecuteCommandList
            if (D3D.currentState.Type != directx.TestType.Immediate)
            {
                if (D3D.currentState.Type == directx.TestType.FrozenDeferred)
                {
                    if (D3D.commandsList[0] == null)
                        RenderDeferredchunk(1);
                }
                else if (D3D.currentState.ThreadCount > 1)
                {
                    RenderDeferredchunk(D3D.currentState.ThreadCount);
                }

                for (int i = 0; i < D3D.currentState.ThreadCount; i++)
                {
                    var commandList = D3D.commandsList[i];
                    // Execute the deferred command list on the immediate context
                    D3D.Device.ImmediateContext.ExecuteCommandList(commandList, false);

                    // For classic deferred we release the command list. Not for frozen
                    if (D3D.currentState.Type == directx.TestType.Deferred)
                    {
                        // Release the command list
                        commandList.Dispose();
                        D3D.commandsList[i] = null;
                    }
                }
            }

            if (switchToNextState)
            {
                D3D.currentState = D3D.nextState;
                switchToNextState = false;
            }*/

















            
            // Setup the pipeline before any rendering
            SetupPipeline();

            // Execute on the rendering thread when ThreadCount == 1 or No deferred rendering is selected
            if (D3D.currentState.Type == directx.TestType.Immediate || (D3D.currentState.Type == directx.TestType.Deferred && D3D.currentState.ThreadCount == 1))
            {
                renderrow(0, 0, D3D.currentState.CountCubes);
            }

            // In case of deferred context, use of FinishCommandList / ExecuteCommandList
            if (D3D.currentState.Type != directx.TestType.Immediate)
            {
                if (D3D.currentState.Type == directx.TestType.FrozenDeferred)
                {
                    if (D3D.commandsList[0] == null)
                        RenderDeferred(1);
                }
                else if (D3D.currentState.ThreadCount > 1)
                {
                    RenderDeferred(D3D.currentState.ThreadCount);
                }

                for (int i = 0; i < D3D.currentState.ThreadCount; i++)
                {
                    var commandList = D3D.commandsList[i];
                    // Execute the deferred command list on the immediate context
                    D3D.DeviceContext.ExecuteCommandList(commandList, false);

                    // For classic deferred we release the command list. Not for frozen
                    if (D3D.currentState.Type == directx.TestType.Deferred)
                    {
                        if (commandList != null)
                        {
                            // Release the command list
                            commandList.Dispose();
                        }

                        D3D.commandsList[i] = null;
                    }
                }
            }

            if (switchToNextState)
            {
                D3D.currentState = D3D.nextState;
                switchToNextState = false;
            }







            hasfinishedwork = 1;
            return hasfinishedwork;
        }











        ~updateSec()
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
                if (somecube != null)
                {
                    somecube.Dispose();
                    somecube = null;
                }
                // Dispose all owned managed objects
            }

            // Release unmanaged resources
        }


    }
}
