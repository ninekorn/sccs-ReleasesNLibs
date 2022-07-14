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
        public static float bundlechunkplanesize = 0.02f;
        public static float chunkplanesize = 0.006666666666666666666667f;

        public static int chunkwidthsim = 0;
        public static int chunkheightsim = 0;
        public static int chunkdepthsim = 0;


        public static float chunkwidthsimfloat = 0;
        public static float chunkheightsimfloat = 0;
        public static float chunkdepthsimfloat = 0;


        public static int chunkwidthdiv = 0;
        public static int chunkheightdiv = 0;
        public static int chunkdepthdiv = 0;


        public static float chunkwidthdivfloat = 0;
        public static float chunkheightdivfloat = 0;
        public static float chunkdepthdivfloat = 0;


        public static int thechunkdivx = 3;
        public static int thechunkdivy = 3;
        public static int thechunkdivz = 3;

        int posmainx = 0;
        int posmainy = 0;
        int posmainz = 0;


        int somemainx = 0;
        int somemainy = 0;
        int somemainz = 0;

        int someindexmain = 0;
        tutorialchunkcubemap somechunk;
        updatePrim.worldviewprobuffer worldViewProjbuffer = new updatePrim.worldviewprobuffer();
        Matrix[][][] matrixchangelevelgenbytes;
        SC_cube[][] worldlevelgenbytesassets;
        SC_cube.DLightBuffer[] _DLightBuffer_cube = new SC_cube.DLightBuffer[1];

        /*public tutorialcubeaschunkinst somecubeaschunkinsttop;
        public tutorialcubeaschunkinst somecubeaschunkinstleft;
        public tutorialcubeaschunkinst somecubeaschunkinstright;
        public tutorialcubeaschunkinst somecubeaschunkinstfront;
        public tutorialcubeaschunkinst somecubeaschunkinstback;
        public tutorialcubeaschunkinst somecubeaschunkinstbottom;*/


        public Vector3 viewpositionorigin = Vector3.Zero;
        public Vector3 viewPosition;

        public Matrix oriProjectionMatrix;
        public scgraphicssecpackage scgraphicssecpackagemessage = new scgraphicssecpackage();

        public int _human_inst_rig_x = 1;
        public int _human_inst_rig_y = 1;
        public int _human_inst_rig_z = 1;
        int tempMultiInstancePhysicsTotal = 1;

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

        float _voxel_mass = 100;

        int _inst_voxel_cube_x = 1;
        int _inst_voxel_cube_y = 1;
        int _inst_voxel_cube_z = 1;

        float _voxel_cube_size_x = 0.15f;//0.0115f //restitution
        float _voxel_cube_size_y = 0.15f;//0.0115f //static friction
        float _voxel_cube_size_z = 0.15f;//0.0015f //kinetic friction

        float voxel_general_size = 0.0025f;
        int voxel_type = -1;
        sc_voxel[][] _world_voxel_cube_lists;
        Matrix[][][] worldMatrix_instances_voxel_cube;
        Matrix[] worldMatrix_base;
        sc_voxel.DLightBuffer[] _DLightBuffer_voxel_cube = new sc_voxel.DLightBuffer[1];


        public static SC_ShaderManager _shaderManager;


        sccslevelgen sccslevelgen = new sccslevelgen();

        int incrementsdivx;
        int incrementsdivy;
        int incrementsdivz;

        int divx;
        int divy;
        int divz;


        tutorialcubeaschunkinst[] mainchunkdivtop;
        tutorialcubeaschunkinst[] mainchunkdivbottom;
        tutorialcubeaschunkinst[] mainchunkdivfront;
        tutorialcubeaschunkinst[] mainchunkdivback;
        tutorialcubeaschunkinst[] mainchunkdivleft;
        tutorialcubeaschunkinst[] mainchunkdivright;

        //SharpDX.Direct3D11.Buffer staticContantBuffer;//= new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
        //SharpDX.Direct3D11.Buffer dynamicConstantBuffer;// = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

        public updatePrim(directx D3D_) //, updateSec updatesec_
        {
            currentupdatePrim = this;
            D3D = D3D_;

            //updatesec = updateSec?.currentupdatesec;
            updatesec = Program.updatesec;


            camera = new Camera();



            _shaderManager = new SC_ShaderManager();
            _shaderManager.Initialize(D3D.Device, Program.consoleHandle, Program.createikrig);


            if (Program.useOculusRift == 0)
            {

                //originPos.Y += 4;
                //originPos.Z -= 2f;
                speedRot = 0.25f;
                speedPos = 0.0015f;

                //camera.SetPosition(0, 0, -5);
                camera.SetPosition(0, 0, -1.0f);
                camera.SetRotation(0, 0, 0);

                originPos = camera.GetPosition();

            }
            else if (Program.useOculusRift == 1)
            {
                speedRot = 0.045f;
                speedPos = 0.0025f;

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

                originPos = new Vector3(0, 0.5f, 0);// Vector3.Zero;
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




            var ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
            var diffuseColour = new Vector4(1, 1, 1, 1);
            var lightDirection = new Vector3(0, -1, -1);
            var lightpos = new Vector3(0, 5, 0);
            var dirLight = new Vector3(0, -1, 0);


            _DLightBuffer_voxel_cube[0] = new sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };


            Vector3 physics_engine_offset_pos = Vector3.Zero;
            Vector3 world_pos_offset = Vector3.Zero;

            _inst_voxel_cube_x = 1;
            _inst_voxel_cube_y = 1;
            _inst_voxel_cube_z = 1;

            var offsetVoxelY = 0; // 40
            //VOXELS
            var r = 0.95f; //0.75f
            var g = 0.35f; //0.75f
            var b = 0.35f; //0.75f
            var a = 1;
            var _object_worldmatrix = Matrix.Identity;
            var offsetPosX = _voxel_cube_size_x * (1.15f); //x between each world instance
            var offsetPosY = _voxel_cube_size_y * (1.15f); //y between each world instance
            var offsetPosZ = _voxel_cube_size_z * (1.15f); //z between each world instance
                                                           //_offsetPos = new Vector3(0, 0, 0);
                                                           //_object_worldmatrix = _object_worldmatrix;
            _object_worldmatrix.M41 = 0 + 0 + physics_engine_offset_pos.X + world_pos_offset.X;
            _object_worldmatrix.M42 = 3 + 0 + physics_engine_offset_pos.Y + world_pos_offset.Y + offsetVoxelY;
            _object_worldmatrix.M43 = 0 + 0 + physics_engine_offset_pos.Z + world_pos_offset.Z;
            _object_worldmatrix.M44 = 1;
            var sc_voxel_spheroid = new sc_voxel();
            voxel_general_size = 0.01f * 0.01f; //0.0015f //0.00075f // somevoxelvirtualdesktopglobals.planeSize
            voxel_type = 1;
            var is_static = false;
            _voxel_mass = 100;

            var _hasinit00 = sc_voxel_spheroid.Initialize(directx.D3D, directx.D3D.SurfaceWidth, directx.D3D.SurfaceHeight, 0, 1, 1, 1,
                _voxel_cube_size_x, _voxel_cube_size_y, _voxel_cube_size_z, new Vector4(r, g, b, a), _inst_voxel_cube_x, _inst_voxel_cube_y, _inst_voxel_cube_z, sccsr15forms.Form1.theHandle,
                _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, null, _voxel_mass, is_static, directx.BodyTag.sc_perko_voxel,
                9, 9, 9, 9, 9, 9, 9, 9, 9, 60, 60, 60, 60, 60, 60,
                //9, 9, 9, 9, 9, 9, 9, 9, 9, 35, 34, 40, 59, 23, 22,
                voxel_general_size, Vector3.Zero, 7, 0, 0, 0, 2, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f
                                                                              //9, 9, 9, 9, 9, 9, 20, 19, 20, 19, 20, 19
                                                                              //9, 9, 9, 9, 9, 9, 35, 34, 40, 59, 20, 19, 
                                                                              //FOR CUBES AND SET TO voxel_type = 1                  
                                                                              //var _hasinit00 = sc_voxel_spheroid.Initialize(SC_console_directx.D3D, SC_console_directx.D3D.SurfaceWidth, SC_console_directx.D3D.SurfaceHeight, 1, 1, 1, _voxel_cube_size_x, _voxel_cube_size_y, _voxel_cube_size_z, new Vector4(r, g, b, a), _inst_voxel_cube_x, _inst_voxel_cube_y, _inst_voxel_cube_z, Hwnd, _object_worldmatrix, 2, offsetPosX, offsetPosY, offsetPosZ, World, _voxel_mass, is_static, SCCoreSystems.sc_console.SC_console_directx.BodyTag._voxel_spheroid, 2, 2, 2, 2, 2, 2, 20, 19, 20, 19, 20, 19, voxel_general_size, Vector3.Zero, 250, 0, 0, 0, 2, voxel_type); //, "terrainGrassDirt.bmp" //0.00035f                                  
                                                                              //FOR CUBES AND SET TO voxel_type = 1

            //_array_of_last_frame_voxel_pos[indexer00][indexer01] = new Vector3[_inst_voxel_cube_x * _inst_voxel_cube_y * _inst_voxel_cube_z];
            _world_voxel_cube_lists = new sc_voxel[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][];

            _world_voxel_cube_lists[0] = new sc_voxel[Program.worldwidth * Program.worldheight * Program.worlddepth];

            _world_voxel_cube_lists[0][0] = sc_voxel_spheroid;

            worldMatrix_instances_voxel_cube = new Matrix[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][][];
            worldMatrix_instances_voxel_cube[0] = new Matrix[Program.worldwidth * Program.worldheight * Program.worlddepth][];
            worldMatrix_instances_voxel_cube[0][0] = new Matrix[_inst_voxel_cube_x * _inst_voxel_cube_y * _inst_voxel_cube_z];

            /*for (int i = 0; i < worldMatrix_instances_voxel_cube[0][0].Length; i++)
            {
                //_array_of_last_frame_voxel_pos[indexer00][indexer01][i] = Vector3.Zero;
                worldMatrix_instances_voxel_cube[0][0][i] = Matrix.Identity;
                worldMatrix_instances_voxel_cube[0][0][i].M41 = i * 0.01f;
            }*/


            for (int x = 0; x < _inst_voxel_cube_x; x++)
            {
                for (int y = 0; y < _inst_voxel_cube_y; y++)
                {
                    for (int z = 0; z < _inst_voxel_cube_z; z++)
                    {
                        var currentByte = (((x * _inst_voxel_cube_x) + (y)) * _inst_voxel_cube_y) + z;

                        worldMatrix_instances_voxel_cube[0][0][currentByte] = Matrix.Identity;
                        worldMatrix_instances_voxel_cube[0][0][currentByte].M41 = x * 0.01f;
                        worldMatrix_instances_voxel_cube[0][0][currentByte].M42 = y * 0.01f;
                        worldMatrix_instances_voxel_cube[0][0][currentByte].M43 = z * 0.01f;
                    }
                }
            }




            worldMatrix_base = new Matrix[1];
            worldMatrix_base[0] = Matrix.Identity;



            directionvectoroffsets = new Vector3[4];

            for (int i = 0; i < directionvectoroffsets.Length; i++)
            {
                directionvectoroffsets[i] = Vector3.Zero;
            }


            if (Program.useOculusRift == 1)
            {
                directx.D3D.OVR.RecenterTrackingOrigin(directx.D3D.sessionPtr);

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
            }


            if (Program.createikrig == 1)
            {
                try
                {

                    _human_inst_rig_x = 1; //30 //10
                    _human_inst_rig_y = 1;
                    _human_inst_rig_z = 1; //30 //10

                    ikvoxelbody = new sccsikvoxellimbs[(somechunkpriminstancesikvoxelbodywidthR)];

                    Vector3 originpositionikvoxelbody = new Vector3(0, 0, 0); //around 0.165f to 0.17f

                    int grabtargetitem = 1;

                    Matrix worldmatofobj = Matrix.Identity;
                    //worldmatofobj.M41 = originpositionikvoxelbody.X;
                    //worldmatofobj.M42 = originpositionikvoxelbody.Y;
                    //worldmatofobj.M43 = originpositionikvoxelbody.Z;

                    //float pelvisvaluey = -0.625f;
                    //int realtorsowidth = 4;
                    float ikvoxelrigbodysize = 0.1f;

                    Vector3 somechunkpriminstanceikvoxelbodypos = Vector3.Zero;

                    for (int xxx = 0; xxx < somechunkpriminstancesikvoxelbodywidthR; xxx++)
                    {

                        float posX = (xxx);
                        float posY = (0);
                        float posZ = (0);

                        var xxi = xxx;
                        var yyi = 0;
                        var zzi = 0;

                        if (xxi < 0)
                        {
                            xxi *= -1;
                            xxi = (somechunkpriminstancesikvoxelbodywidthR) + xxi;
                        }
                        if (yyi < 0)
                        {
                            yyi *= -1;
                            yyi = (somechunkpriminstancesikvoxelbodyheightR) + yyi;
                        }
                        if (zzi < 0)
                        {
                            zzi *= -1;
                            zzi = (somechunkpriminstancesikvoxelbodydepthR) + zzi;
                        }

                        int somechunkpriminstanceikvoxelbodyindex = xxi;

                        somechunkpriminstanceikvoxelbodypos = ((new Vector3(posX, posY, posZ)) + originpositionikvoxelbody);

                        ikvoxelbody[somechunkpriminstanceikvoxelbodyindex] = new sccsikvoxellimbs(this);


                        Matrix finalRotationMatrix = originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot;

                        ikvoxelbody[somechunkpriminstanceikvoxelbodyindex].createikbody(null, tempMultiInstancePhysicsTotal, somechunkpriminstanceikvoxelbodypos, null, worldmatofobj, _human_inst_rig_x, _human_inst_rig_y, _human_inst_rig_z, grabtargetitem, finalRotationMatrix);
                    }





                    grabtargetitem = 0;
                    ikarmvoxel = new sccsikvoxellimbs[(somechunkpriminstancesikarmvoxelwidthR) + (somechunkpriminstancesikarmvoxelheightR)];
                    ikfingervoxel = new sccsikvoxellimbs[(somechunkpriminstancesikarmvoxelwidthR) + (somechunkpriminstancesikarmvoxelheightR)][];

                    Vector3 originpositionikarmvoxel = new Vector3(0, 0, 0);

                    //float pelvisvaluey = -0.625f;
                    //int realtorsowidth = 4;
                    //float ikvoxelrigsize = 0.1f;

                    Vector3 somechunkpriminstanceikarmvoxelpos = Vector3.Zero;

                    for (int xxx = 0; xxx < somechunkpriminstancesikarmvoxelwidthR; xxx++)
                    {
                        for (int yyy = 0; yyy < somechunkpriminstancesikarmvoxelheightR; yyy++)
                        {
                            float posX = (xxx);
                            float posY = (yyy);
                            float posZ = (0);

                            var xxi = xxx;
                            var yyi = yyy;
                            var zzi = 0;

                            if (xxi < 0)
                            {
                                xxi *= -1;
                                xxi = (somechunkpriminstancesikarmvoxelwidthR) + xxi;
                            }
                            if (yyi < 0)
                            {
                                yyi *= -1;
                                yyi = (somechunkpriminstancesikarmvoxelheightR) + yyi;
                            }
                            if (zzi < 0)
                            {
                                zzi *= -1;
                                zzi = (somechunkpriminstancesikarmvoxeldepthR) + zzi;
                            }

                            int somechunkpriminstanceikarmvoxelindex = xxi + (yyi * (somechunkpriminstancesikarmvoxelheightR));

                            somechunkpriminstanceikarmvoxelpos = ((new Vector3(posX, posY, posZ)) + originpositionikarmvoxel);


                            if (somechunkpriminstanceikarmvoxelindex == 0) // bottom left
                            {

                                somechunkpriminstanceikarmvoxelpos.X = 0;
                                somechunkpriminstanceikarmvoxelpos.Y = 0;

                            }
                            else if (somechunkpriminstanceikarmvoxelindex == 1) // top right
                            {

                                somechunkpriminstanceikarmvoxelpos.X = 0;
                                somechunkpriminstanceikarmvoxelpos.Y = 0;

                            }
                            else if (somechunkpriminstanceikarmvoxelindex == 2)
                            {
                                somechunkpriminstanceikarmvoxelpos.X = 0;
                                somechunkpriminstanceikarmvoxelpos.Y = 0;
                            }
                            else if (somechunkpriminstanceikarmvoxelindex == 3)
                            {
                                somechunkpriminstanceikarmvoxelpos.X = 0;
                                somechunkpriminstanceikarmvoxelpos.Y = 0;
                            }

                            //somechunkpriminstanceikarmvoxelpos = ((new Vector3(posX, posY, posZ)) + originpositionikarmvoxel);
                            Matrix finalRotationMatrix = originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot;

                            ikarmvoxel[somechunkpriminstanceikarmvoxelindex] = new sccsikvoxellimbs(this);
                            ikarmvoxel[somechunkpriminstanceikarmvoxelindex].createikarm(null, tempMultiInstancePhysicsTotal, somechunkpriminstanceikarmvoxelpos, ikvoxelbody[0], somechunkpriminstanceikarmvoxelindex, _human_inst_rig_x, _human_inst_rig_y, _human_inst_rig_z, grabtargetitem, 0, null, finalRotationMatrix);

                            grabtargetitem = 0;
                            ikfingervoxel[somechunkpriminstanceikarmvoxelindex] = new sccsikvoxellimbs[(somechunkpriminstancesikfingervoxelwidthR) + (somechunkpriminstancesikfingervoxelheightR)];

                            Vector3 originpositionikfingervoxel = new Vector3(0, 0, 0);

                            //float pelvisvaluey = -0.625f;
                            //int realtorsowidth = 4;
                            //float ikvoxelrigsize = 0.1f;

                            Vector3 somechunkpriminstanceikfingervoxelpos = Vector3.Zero;

                            for (int xxxx = 0; xxxx < somechunkpriminstancesikfingervoxelwidthR; xxxx++)
                            {
                                for (int yyyy = 0; yyyy < somechunkpriminstancesikfingervoxelheightR; yyyy++)
                                {
                                    float posXx = (xxxx);
                                    float posYy = (yyyy);
                                    float posZz = (0);

                                    var xxxi = xxxx;
                                    var yyyi = yyyy;
                                    var zzzi = 0;

                                    if (xxxi < 0)
                                    {
                                        xxxi *= -1;
                                        xxxi = (somechunkpriminstancesikfingervoxelwidthR) + xxxi;
                                    }
                                    if (yyyi < 0)
                                    {
                                        yyyi *= -1;
                                        yyyi = (somechunkpriminstancesikfingervoxelheightR) + yyyi;
                                    }
                                    if (zzzi < 0)
                                    {
                                        zzzi *= -1;
                                        zzzi = (somechunkpriminstancesikfingervoxeldepthR) + zzzi;
                                    }

                                    int somechunkpriminstanceikfingervoxelindex = xxxi + (yyyi * (somechunkpriminstancesikfingervoxelheightR));

                                    somechunkpriminstanceikfingervoxelpos = ((new Vector3(posXx, posYy, posZz)) + originpositionikfingervoxel);


                                    if (somechunkpriminstanceikfingervoxelindex == 0) // bottom left
                                    {

                                        somechunkpriminstanceikfingervoxelpos.X = 0;
                                        somechunkpriminstanceikfingervoxelpos.Y = 0;

                                    }
                                    else if (somechunkpriminstanceikfingervoxelindex == 1) // top right
                                    {

                                        somechunkpriminstanceikfingervoxelpos.X = 0;
                                        somechunkpriminstanceikfingervoxelpos.Y = 0;

                                    }
                                    else if (somechunkpriminstanceikfingervoxelindex == 2)
                                    {
                                        somechunkpriminstanceikfingervoxelpos.X = 0;
                                        somechunkpriminstanceikfingervoxelpos.Y = 0;
                                    }
                                    else if (somechunkpriminstanceikfingervoxelindex == 3)
                                    {
                                        somechunkpriminstanceikfingervoxelpos.X = 0;
                                        somechunkpriminstanceikfingervoxelpos.Y = 0;
                                    }

                                    //somechunkpriminstanceikfingervoxelpos = ((new Vector3(posX, posY, posZ)) + originpositionikfingervoxel);

                                    ikfingervoxel[somechunkpriminstanceikarmvoxelindex][somechunkpriminstanceikfingervoxelindex] = new sccsikvoxellimbs(this);
                                    ikfingervoxel[somechunkpriminstanceikarmvoxelindex][somechunkpriminstanceikfingervoxelindex].createikfingers(null, tempMultiInstancePhysicsTotal, somechunkpriminstanceikfingervoxelpos, ikvoxelbody[0], somechunkpriminstanceikarmvoxelindex, _human_inst_rig_x, _human_inst_rig_y, _human_inst_rig_z, grabtargetitem, 1, ikarmvoxel[somechunkpriminstanceikarmvoxelindex], somechunkpriminstanceikfingervoxelindex);

                                    /*for (int zzz = -somechunkpriminstancesikfingervoxeldepthL; zzz <= somechunkpriminstancesikfingervoxeldepthR; zzz++)
                                    {

                                    }*/
                                }
                            }

                            /*
                            for (int zzz = -somechunkpriminstancesikarmvoxeldepthL; zzz <= somechunkpriminstancesikarmvoxeldepthR; zzz++)
                            {

                            }*/
                        }
                    }


                }
                catch (Exception ex)
                {
                    Program.MessageBox((IntPtr)0, "create" + ex.ToString(), "scmsg", 0);

                }
            }


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
            scgraphicssecpackagemessage.someextrapelvismatrix = rotatingMatrixForPelvis;
            scgraphicssecpackagemessage.offsetpos = OFFSETPOS;
            scgraphicssecpackagemessage.handPoseRight = handPoseRight;
            scgraphicssecpackagemessage.handPoseLeft = handPoseLeft;

            sccswriteikrigtobuffer(scgraphicssecpackagemessage);





            //doesnt work LOSS OF PRECISION
            //doesnt work LOSS OF PRECISION
            //doesnt work LOSS OF PRECISION
            /*
            chunkwidth = 24;
            chunkheight = 24;
            chunkdepth = 24;

            thechunkdivx = 6;
            thechunkdivy = 6;
            thechunkdivz = 6;

            sccslevelgen.maxarraychunkdataval = 216;*/

            /*chunkwidth = 28;
            chunkheight = 28;
            chunkdepth = 28;
            thechunkdivx = 7;
            thechunkdivy = 7;
            thechunkdivz = 7;
            sccslevelgen.maxarraychunkdataval = 343;*/

            /*
            chunkwidth = 20;
            chunkheight = 20;
            chunkdepth = 20;
            thechunkdivx = 5;
            thechunkdivy = 5;
            thechunkdivz = 5;
            sccslevelgen.maxarraychunkdataval = 125;*/

            //doesnt work LOSS OF PRECISION
            //doesnt work LOSS OF PRECISION
            //doesnt work LOSS OF PRECISION



        

            chunkwidthsim = 8;
            chunkheightsim = 8;
            chunkdepthsim = 8;


            chunkwidthsimfloat = (float)chunkwidthsim;
            chunkheightsimfloat = (float)chunkheightsim;
            chunkdepthsimfloat = (float)chunkdepthsim;

            chunkwidthdiv = 4;
            chunkheightdiv = 4;
            chunkdepthdiv = 4;

            chunkwidthdivfloat = (float)chunkwidthdiv;
            chunkheightdivfloat = (float)chunkheightdiv;
            chunkdepthdivfloat = (float)chunkdepthdiv;


            float someratio =(float)((float)chunkwidthdiv / (float)chunkwidthsim);

            //Console.WriteLine("ratio:" + someratio);
            bundlechunkplanesize = 0.01f;
            chunkplanesize = bundlechunkplanesize * someratio;



            thechunkdivx = 2;
            thechunkdivy = 2;
            thechunkdivz = 2;



            sccslevelgen.maxarraychunkdataval = 8;

            sccslevelgen.minw = 10;
            sccslevelgen.minh = 4;
            sccslevelgen.mind = 10;
            
            sccslevelgen.maxw = 10;
            sccslevelgen.maxh = 4;
            sccslevelgen.maxd = 10;

            sccslevelgen.createlevel();

            int levelminx = sccslevelgen.minx;
            int levelminy = sccslevelgen.miny;
            int levelminz = sccslevelgen.minz;

            int levelmaxx = sccslevelgen.maxx;
            int levelmaxy = sccslevelgen.maxy;
            int levelmaxz = sccslevelgen.maxz;

            //more than 12 buffers per class of instanced meshes... so 3200 * 12
            //5x2x5 == 50 x 64 meshes = 3200 classes of instanced meshes... compared to 1 x 64 = 64 class of instanced meshes == 4.4gb ram approx visual studio DiagTool - 20 fps
            //2x5x2 == 20 x 64 meshes = 1280 classes of instanced meshes... compared to 1 x 64 = 64 class of instanced meshes == 3.4gb ram approx - 45 fps
            //2x2x2 == 8 x 64 meshes = 512 classes of instanced meshes... compared to 1 x 64 = 64 class of instanced meshes == 2.9gb ram approx - 90fps.
            //-error-3x2x3 == 18 x 64 meshes = 1152 classes of instanced meshes... compared to 1 x 64 = 64 class of instanced meshes == 
            //4x1x4 == 16 x 64 meshes = 1024 classes of instanced meshes... compared to 1 x 64 = 64 class of instanced meshes == 3.4gb ram approx - 45 fps
            //4x2x4 == 32 * 64 meshes = 2048 classes of instanced meshes... 


            //HAS TO BE AN EVEN NUMBER. AND HAS TO BE A DIVISION/FRACTION OF SCCSLEVELGEN.SOMEWIDTH AND SCCSLEVELGEN.SOMEHEIGHT AND SCCSLEVELGEN.SOMEDEPTH WHERE SCCSLEVELGEN.SOMEWIDTH == SCCSLEVELGEN.MINW + SCCSLEVELGEN.MAXW
            //HAS TO BE AN EVEN NUMBER. AND HAS TO BE A DIVISION/FRACTION OF SCCSLEVELGEN.SOMEWIDTH AND SCCSLEVELGEN.SOMEHEIGHT AND SCCSLEVELGEN.SOMEDEPTH WHERE SCCSLEVELGEN.SOMEWIDTH == SCCSLEVELGEN.MINW + SCCSLEVELGEN.MAXW
            //HAS TO BE AN EVEN NUMBER. AND HAS TO BE A DIVISION/FRACTION OF SCCSLEVELGEN.SOMEWIDTH AND SCCSLEVELGEN.SOMEHEIGHT AND SCCSLEVELGEN.SOMEDEPTH WHERE SCCSLEVELGEN.SOMEWIDTH == SCCSLEVELGEN.MINW + SCCSLEVELGEN.MAXW
            divx = 1;
            divy = 1; //sccsl5evelgen.wallheightsize
            divz = 1;
            //HAS TO BE AN EVEN NUMBER. AND HAS TO BE A DIVISION/FRACTION OF SCCSLEVELGEN.SOMEWIDTH AND SCCSLEVELGEN.SOMEHEIGHT AND SCCSLEVELGEN.SOMEDEPTH WHERE SCCSLEVELGEN.SOMEWIDTH == SCCSLEVELGEN.MINW + SCCSLEVELGEN.MAXW
            //HAS TO BE AN EVEN NUMBER. AND HAS TO BE A DIVISION/FRACTION OF SCCSLEVELGEN.SOMEWIDTH AND SCCSLEVELGEN.SOMEHEIGHT AND SCCSLEVELGEN.SOMEDEPTH WHERE SCCSLEVELGEN.SOMEWIDTH == SCCSLEVELGEN.MINW + SCCSLEVELGEN.MAXW
            //HAS TO BE AN EVEN NUMBER. AND HAS TO BE A DIVISION/FRACTION OF SCCSLEVELGEN.SOMEWIDTH AND SCCSLEVELGEN.SOMEHEIGHT AND SCCSLEVELGEN.SOMEDEPTH WHERE SCCSLEVELGEN.SOMEWIDTH == SCCSLEVELGEN.MINW + SCCSLEVELGEN.MAXW





            incrementsdivx = sccslevelgen.somewidth / divx;
            incrementsdivy = sccslevelgen.someheight / divy;
            incrementsdivz = sccslevelgen.somedepth / divz;

            //50

            //int halfincrementsdivx = incrementsdivx / 2;
            //int halfincrementsdivy = incrementsdivy / 2;
            //int halfincrementsdivz = incrementsdivz / 2;

            //int incrementsdivxc = 0;
            //int incrementsdivyc = 0;
            //int incrementsdivzc = 0;

            mainchunkdivtop = new tutorialcubeaschunkinst[divx * divy * divz];
            mainchunkdivbottom = new tutorialcubeaschunkinst[divx * divy * divz];
            mainchunkdivfront = new tutorialcubeaschunkinst[divx * divy * divz];
            mainchunkdivback = new tutorialcubeaschunkinst[divx * divy * divz];
            mainchunkdivleft = new tutorialcubeaschunkinst[divx * divy * divz];
            mainchunkdivright = new tutorialcubeaschunkinst[divx * divy * divz];

            //staticContantBuffer = new SharpDX.Direct3D11.Buffer(D3D.Device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
            //dynamicConstantBuffer = new SharpDX.Direct3D11.Buffer(D3D.Device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);


            /*for (int i = 0; i < mainchunkdivtop.Length; i++)
            {
                mainchunkdivtop[i] = new tutorialcubeaschunkinst[incrementsdivx * incrementsdivy * incrementsdivz];
                mainchunkdivbottom[i] = new tutorialcubeaschunkinst[incrementsdivx * incrementsdivy * incrementsdivz];
                mainchunkdivfront[i] = new tutorialcubeaschunkinst[incrementsdivx * incrementsdivy * incrementsdivz];
                mainchunkdivback[i] = new tutorialcubeaschunkinst[incrementsdivx * incrementsdivy * incrementsdivz];
                mainchunkdivleft[i] = new tutorialcubeaschunkinst[incrementsdivx * incrementsdivy * incrementsdivz];
                mainchunkdivright[i] = new tutorialcubeaschunkinst[incrementsdivx * incrementsdivy * incrementsdivz];
            }*/


            int counteroftiles = 0;

            int someindexmain = 0;

            int someindexsec = 0;


            int somemaincounter = 0;
            int somemaincounter_ = 0;

            int somemaincounterl = 0;
            int somemaincounter_l = 0;

            int somemaincounterr = 0;
            int somemaincounter_r = 0;

            int somemaincounterf = 0;
            int somemaincounter_f = 0;

            int somemaincounterba = 0;
            int somemaincounter_ba = 0;

            int somemaincounterbo = 0;
            int somemaincounter_bo = 0;


            int counternull = 0;

            int maincounter = 0;

            for (int x = sccslevelgen.minx, xe = 0; x < sccslevelgen.maxx; x += incrementsdivx, xe++)
            {
                for (int y = sccslevelgen.miny, ye = 0; y < sccslevelgen.maxy; y += incrementsdivy, ye++)
                {
                    for (int z = sccslevelgen.minz, ze = 0; z < sccslevelgen.maxz; z += incrementsdivz, ze++)
                    {
                        int xx = x;
                        int yy = y;
                        int zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = xx + (sccslevelgen.maxx - 1);
                        }

                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = yy + (sccslevelgen.maxy - 1);
                        }
                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = zz + (sccslevelgen.maxz - 1);
                        }



                        int posmainx = x / incrementsdivx;
                        int posmainy = y / incrementsdivy;
                        int posmainz = z / incrementsdivz;

                        if (posmainx < 0)
                        {
                            posmainx *= -1;
                            posmainx = posmainx + ((divx/2) -1);
                        }

                        if (posmainy < 0)
                        {
                            posmainy *= -1;
                            posmainy = posmainy + ((divy / 2) - 1);
                        }
                        if (posmainz < 0)
                        {
                            posmainz *= -1;
                            posmainz = posmainz + ((divz / 2) - 1);
                        }

                      
                        int somemainx = xe;
                        int somemainy = ye;
                        int somemainz = ze;

                        someindexmain = posmainx + (divx) * (posmainy + (divy) * posmainz);


                        //Console.WriteLine("x:" + posmainx + "/y:" + posmainy + "/z:" + posmainz + "/m:" + someindexmain);

                        //Console.WriteLine("indexmain:" + someindexmain);

                        int facetype = 0;
                        mainchunkdivtop[someindexmain] = new tutorialcubeaschunkinst(D3D.Device, facetype, sccslevelgen, x, y, z, x + incrementsdivx, y + incrementsdivy, z + incrementsdivz, somemaincounter, out somemaincounter_);
                        somemaincounter = somemaincounter_;

                       
                        facetype = 1;
                        mainchunkdivleft[someindexmain] = new tutorialcubeaschunkinst(D3D.Device, facetype, sccslevelgen, x, y, z, x + incrementsdivx, y + incrementsdivy, z + incrementsdivz, somemaincounterl, out somemaincounter_l);
                        somemaincounterl = somemaincounter_l;

                        facetype = 2;
                        mainchunkdivright[someindexmain] = new tutorialcubeaschunkinst(D3D.Device, facetype, sccslevelgen, x, y, z, x + incrementsdivx, y + incrementsdivy, z + incrementsdivz, somemaincounterr, out somemaincounter_r);
                        somemaincounterr = somemaincounter_r;
                        
                        
                        
                        facetype = 3;
                        mainchunkdivfront[someindexmain] = new tutorialcubeaschunkinst(D3D.Device, facetype, sccslevelgen, x, y, z, x + incrementsdivx, y + incrementsdivy, z + incrementsdivz, somemaincounterf, out somemaincounter_f);
                        somemaincounterf = somemaincounter_f;

                        facetype = 4;
                        mainchunkdivback[someindexmain] = new tutorialcubeaschunkinst(D3D.Device, facetype, sccslevelgen, x, y, z, x + incrementsdivx, y + incrementsdivy, z + incrementsdivz, somemaincounterba, out somemaincounter_ba);
                        somemaincounterba = somemaincounter_ba;
                        
                        facetype = 5;
                        mainchunkdivbottom[someindexmain] = new tutorialcubeaschunkinst(D3D.Device, facetype, sccslevelgen, x, y, z, x + incrementsdivx, y + incrementsdivy, z + incrementsdivz, somemaincounterbo, out somemaincounter_bo);
                        somemaincounterbo = somemaincounter_bo;


                    }
                }
            }







            
            int someseccounter = 0;
            int someseccounter_ = 0;

            int someseccounterl = 0;
            int someseccounter_l = 0;

            int someseccounterr = 0;
            int someseccounter_r = 0;

            int someseccounterf = 0;
            int someseccounter_f = 0;

            int someseccounterba = 0;
            int someseccounter_ba = 0;

            int someseccounterbo = 0;
            int someseccounter_bo = 0;

            for (int x = sccslevelgen.minx, xe = 0; x < sccslevelgen.maxx; x += incrementsdivx, xe++)
            {
                for (int y = sccslevelgen.miny, ye = 0; y < sccslevelgen.maxy; y += incrementsdivy, ye++)
                {
                    for (int z = sccslevelgen.minz, ze = 0; z < sccslevelgen.maxz; z += incrementsdivz, ze++)
                    {
                        int xx = x;
                        int yy = y;
                        int zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = xx + (sccslevelgen.maxx - 1);
                        }

                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = yy + (sccslevelgen.maxy - 1);
                        }
                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = zz + (sccslevelgen.maxz - 1);
                        }

                        int posmainx = x / incrementsdivx;
                        int posmainy = y / incrementsdivy;
                        int posmainz = z / incrementsdivz;

                        if (posmainx < 0)
                        {
                            posmainx *= -1;
                            posmainx = posmainx + ((divx / 2) - 1);
                        }

                        if (posmainy < 0)
                        {
                            posmainy *= -1;
                            posmainy = posmainy + ((divy / 2) - 1);
                        }
                        if (posmainz < 0)
                        {
                            posmainz *= -1;
                            posmainz = posmainz + ((divz / 2) - 1);
                        }

                        //Console.WriteLine("x:"+ posmainx + "/y:" + posmainy + "/z:" + posmainz);



                        int somemainx = xe;
                        int somemainy = ye;
                        int somemainz = ze;

                        someindexmain = posmainx + (divx) * (posmainy + (divy) * posmainz);

                        int novalue;

                        
                        int facetype = 0;
                        mainchunkdivtop[someindexmain].createthechunks(x, y, z, x + incrementsdivx, y + incrementsdivy, z + incrementsdivz, facetype, someseccounter, out someseccounter_);
                        someseccounter = someseccounter_;
                       
                        mainchunkdivtop[someindexmain].createinstances(facetype,0, out novalue);
                                  
                        facetype = 1;
                        mainchunkdivleft[someindexmain].createthechunks(x, y, z, x + incrementsdivx, y + incrementsdivy, z + incrementsdivz, facetype, someseccounterl, out someseccounter_l);
                        mainchunkdivleft[someindexmain].createinstances(facetype,0, out novalue);
                        someseccounterl = someseccounter_l;
                      
                        
                        facetype = 2;
                        mainchunkdivright[someindexmain].createthechunks(x, y, z, x + incrementsdivx, y + incrementsdivy, z + incrementsdivz, facetype, someseccounterr, out someseccounter_r);
                        mainchunkdivright[someindexmain].createinstances(facetype, 0, out novalue);
                        someseccounterr = someseccounter_r;
                        
                        facetype = 3;
                        mainchunkdivfront[someindexmain].createthechunks(x, y, z, x + incrementsdivx, y + incrementsdivy, z + incrementsdivz, facetype, someseccounterf, out someseccounter_f);
                        mainchunkdivfront[someindexmain].createinstances(facetype, 0, out novalue);
                        someseccounterf = someseccounter_f;
                        
                        facetype = 4;
                        mainchunkdivback[someindexmain].createthechunks(x, y, z, x + incrementsdivx, y + incrementsdivy, z + incrementsdivz, facetype, someseccounterba, out someseccounter_ba);
                        mainchunkdivback[someindexmain].createinstances(facetype, 0, out novalue);
                        someseccounterba = someseccounter_ba;
                        
                        facetype = 5;
                        mainchunkdivbottom[someindexmain].createthechunks(x, y, z, x + incrementsdivx, y + incrementsdivy, z + incrementsdivz, facetype, someseccounterbo, out someseccounter_bo);
                        mainchunkdivbottom[someindexmain].createinstances(facetype, 0, out novalue);
                        someseccounterbo = someseccounter_bo;
                    }
                }
            }
            























            /*
            if (sccslevelgen.arraychunkdatalod0 != null)
            {
                for (int ii = 0; ii < sccslevelgen.arraychunkdatalod0.Length; ii++)
                {
                    if (sccslevelgen.arraychunkdatalod0[ii] != null)
                    {
                        for (int i = 0; i < sccslevelgen.arraychunkdatalod0[ii].Length; i++)
                        {
                            if (sccslevelgen.arraychunkdatalod0[ii][i].arraychunkvertslod0 != null)
                            {
                                //sccslevelgen.arraychunkdatalod0[facetype][i].arraychunkvertslod0.cleararrays();
                                sccslevelgen.arraychunkdatalod0[ii][i].vertexcount = 0;//.cleararrays();

                            }
                        }
                        sccslevelgen.arraychunkdatalod0[ii] = null;
                    }
                }
            }

            sccslevelgen.arraychunkdatalod0 = null;*/





            /*
            sccslevelgen.arraychunkdatalod0 = null;
            sccslevelgen.levelmap = null;
            //sccslevelgen = null;

            //GC.SuppressFinalize(sccslevelgen);*/
















            /*
            int facetype = 0;
            somecubeaschunkinsttop = new tutorialcubeaschunkinst(D3D.Device, facetype, sccslevelgen);

            facetype = 1;
            somecubeaschunkinstleft = new tutorialcubeaschunkinst(D3D.Device, facetype, sccslevelgen);

            facetype = 2;
            somecubeaschunkinstright = new tutorialcubeaschunkinst(D3D.Device, facetype, sccslevelgen);

            facetype = 3;
            somecubeaschunkinstfront = new tutorialcubeaschunkinst(D3D.Device, facetype, sccslevelgen);

            facetype = 4;
            somecubeaschunkinstback = new tutorialcubeaschunkinst(D3D.Device, facetype, sccslevelgen);

            facetype = 5;
            somecubeaschunkinstbottom = new tutorialcubeaschunkinst(D3D.Device, facetype, sccslevelgen);
            
            _planeSize = somecubeaschunkinsttop.somelevelgenprimglobals.planeSize;*/











            matrixchangelevelgenbytes = new Matrix[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][][];
            worldlevelgenbytesassets = new SC_cube[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][];

            float levelgenbytesassetsizex = 0.0025f; //0.0115f //1.5f
            float levelgenbytesassetsizey = 0.0025f; //0.0115f //1.5f
            float levelgenbytesassetsizez = 0.0025f;

            var vertoffsetx = 0;
            var vertoffsety = 0;
            var vertoffsetz = 0; //(-0.1f) * 2
            r = 0.25f;
            g = 0.95f;// g = 0.75f;
            b = 0.25f;
            a = 1;

            var sizeWidth01 = 1;
            var sizeheight01 = 1;
            var sizedepth01 = 1;

            var instdestroybytesx = 3;
            var instdestroybytesy = 3;
            var instdestroybytesz = 3;

            _object_worldmatrix = Matrix.Identity;
            _object_worldmatrix.M41 = 0;
            _object_worldmatrix.M42 = 0;
            _object_worldmatrix.M43 = 0;
            _object_worldmatrix.M44 = 1;
            offsetPosX = 1;// sizeWidth01 * 2;
            offsetPosY = 1;//sizeheight01 * 2;
            offsetPosZ = 1;//sizedepth01 * 2;
            worldlevelgenbytesassets[0] = new SC_cube[1];
            worldlevelgenbytesassets[0][0] = new SC_cube();
            worldlevelgenbytesassets[0][0].Initialize(directx.D3D, directx.D3D.SurfaceWidth, directx.D3D.SurfaceHeight, 1, 1, 1,
                levelgenbytesassetsizex, levelgenbytesassetsizey, levelgenbytesassetsizez, new Vector4(r, g, b, a), instdestroybytesx, instdestroybytesy, instdestroybytesz,
                Program.consoleHandle, _object_worldmatrix, 3, offsetPosX, offsetPosY, offsetPosZ, null, directx.BodyTag._screen_assets, true, 0, 10,
                vertoffsetx, vertoffsety, vertoffsetz); //, "terrainGrassDirt.bmp" //0.00035f
            worldlevelgenbytesassets[0][0].indexofshader = _shaderManager.createcubeshaders(directx.D3D.Device, sccsr15forms.Form1.theHandle, 0, 1);

            matrixchangelevelgenbytes[0] = new Matrix[1][];
            matrixchangelevelgenbytes[0][0] = new Matrix[instdestroybytesx * instdestroybytesy * instdestroybytesz];

            for (int i = 0; i < matrixchangelevelgenbytes[0][0].Length; i++)
            {
                matrixchangelevelgenbytes[0][0][i] = Matrix.Identity;
            }



            //lightpos = new Vector3(0, 100, 0);
            //ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
            ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
            diffuseColour = new Vector4(1, 1, 1, 1);
            lightDirection = new Vector3(0, -1, -1);


            _DLightBuffer_cube[0] = new SC_cube.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight,
                padding0 = 0,
                lightPosition = lightpos,
                padding1 = 100
            };

            hasgeneratedmeshes = 1;
        }


        int hasgeneratedmeshes = 0;





        public static float _planeSize;
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
                //Console.WriteLine(D3D.inputStateLTouch.Touches);
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
                //Console.WriteLine(D3D.inputStateRTouch.Touches);
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



        Matrix worldViewProj;
        Matrix rotateMatrix;
        Matrix proj;
        Matrix viewProj;

        public void renderthemeshes()
        {



            viewMatrix = camera.ViewMatrix;
            proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, D3D.SurfaceWidth / (float)D3D.SurfaceHeight, 0.001f, 1000.0f);


            viewProj = Matrix.Multiply(viewMatrix, proj);

            rotateMatrix = Matrix.Identity;
           
            Matrix.Multiply(ref rotateMatrix, ref viewProj, out worldViewProj);
            worldViewProj.Transpose();

            /*arrayofworldmatrix = new worldviewprobuffer[1];
            arrayofworldmatrix[0].viewmatrix = viewMatrix;
            arrayofworldmatrix[0].projectionmatrix = proj;
            arrayofworldmatrix[0].worldmatrix = D3D.WorldMatrix;

            arrayofworldmatrix[0].projectionmatrix.Transpose();
            arrayofworldmatrix[0].viewmatrix.Transpose();
            arrayofworldmatrix[0].worldmatrix.Transpose();*/


            
            //arrayofworldmatrix[0] = new updatePrim.worldviewprobuffer();
            arrayofworldmatrix[0].viewmatrix = viewMatrix;
            arrayofworldmatrix[0].projectionmatrix = proj;
            arrayofworldmatrix[0].worldmatrix = D3D.WorldMatrix;


            //arrayofworldmatrix[0].worldmatrix.Transpose();
            //arrayofworldmatrix[0].projectionmatrix.Transpose();
            //arrayofworldmatrix[0].viewmatrix.Transpose();


            //worldViewProjbuffer = new updatePrim.worldviewprobuffer();
            worldViewProjbuffer.worldmatrix = arrayofworldmatrix[0].worldmatrix;
            worldViewProjbuffer.viewmatrix = arrayofworldmatrix[0].viewmatrix;
            worldViewProjbuffer.projectionmatrix = arrayofworldmatrix[0].projectionmatrix;

            //var someviewmat = viewMatrix;
            //someviewmat.Transpose();

            /*var sometranspose = arrayofworldmatrix[0].projectionmatrix;
            arrayofworldmatrix[0].viewmatrix = viewMatrix;
            arrayofworldmatrix[0].projectionmatrix = sometranspose;
            arrayofworldmatrix[0].worldmatrix = D3D.WorldMatrix;*/

            //Console.WriteLine("topface");
            //if (updatesec != null)
            {
                


                /*
                writevoxelstobuffer();
                writecubestobuffer();

                
                if (hasgeneratedmeshes == 1)
                {
                    workonLevelGenChangeBytes();
                }
               */




                for (int x = sccslevelgen.minx, xe = 0; x < sccslevelgen.maxx; x += incrementsdivx, xe++)
                {
                    for (int y = sccslevelgen.miny, ye = 0; y < sccslevelgen.maxy; y += incrementsdivy, ye++)
                    {
                        for (int z = sccslevelgen.minz, ze = 0; z < sccslevelgen.maxz; z += incrementsdivz, ze++)
                        {
                            int xx = x;
                            int yy = y;
                            int zz = z;

                            if (xx < 0)
                            {
                                xx *= -1;
                                xx = xx + (sccslevelgen.maxx - 1);
                            }

                            if (yy < 0)
                            {
                                yy *= -1;
                                yy = yy + (sccslevelgen.maxy - 1);
                            }
                            if (zz < 0)
                            {
                                zz *= -1;
                                zz = zz + (sccslevelgen.maxz - 1);
                            }


                            posmainx = x / incrementsdivx;
                             posmainy = y / incrementsdivy;
                            posmainz = z / incrementsdivz;

                            if (posmainx < 0)
                            {
                                posmainx *= -1;
                                posmainx = posmainx + ((divx / 2) - 1);
                            }

                            if (posmainy < 0)
                            {
                                posmainy *= -1;
                                posmainy = posmainy + ((divy / 2) - 1);
                            }
                            if (posmainz < 0)
                            {
                                posmainz *= -1;
                                posmainz = posmainz + ((divz / 2) - 1);
                            }


                             somemainx = xe;
                             somemainy = ye;
                             somemainz = ze;

                             someindexmain = posmainx + (divx) * (posmainy + (divy) * posmainz);


                            for (int i = 0; i < mainchunkdivtop[someindexmain].somefacemeshlisttodraw.Count; i++)
                            {
                                //Console.WriteLine("topface");
                                mainchunkdivtop[someindexmain].somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                                _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, null, mainchunkdivtop[someindexmain], mainchunkdivtop[someindexmain].somefacemeshlisttodraw[i], worldViewProjbuffer);
                                //somecubeaschunkinsttop.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinsttop, worldViewProjbuffer);
                                //somecubeaschunkinsttop.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinsttop, arrayofworldmatrix);
                            }
                            
                            for (int i = 0; i < mainchunkdivleft[someindexmain].somefacemeshlisttodraw.Count; i++)
                            {

                                mainchunkdivleft[someindexmain].somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                                _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, null, mainchunkdivleft[someindexmain], mainchunkdivleft[someindexmain].somefacemeshlisttodraw[i], worldViewProjbuffer);
                                //somecubeaschunkinstleft.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstleft, worldViewProjbuffer);
                                //somecubeaschunkinstleft.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstleft, arrayofworldmatrix);
                            }
                            
                            for (int i = 0; i < mainchunkdivright[someindexmain].somefacemeshlisttodraw.Count; i++)
                            {

                                mainchunkdivright[someindexmain].somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                                _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, null, mainchunkdivright[someindexmain], mainchunkdivright[someindexmain].somefacemeshlisttodraw[i], worldViewProjbuffer);
                                //somecubeaschunkinstright.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstright, worldViewProjbuffer);
                                //somecubeaschunkinstright.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstright, arrayofworldmatrix);
                            }

                            for (int i = 0; i < mainchunkdivfront[someindexmain].somefacemeshlisttodraw.Count; i++)
                            {

                                mainchunkdivfront[someindexmain].somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                                _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, null, mainchunkdivfront[someindexmain], mainchunkdivfront[someindexmain].somefacemeshlisttodraw[i], worldViewProjbuffer);
                                //somecubeaschunkinstfront.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstfront, worldViewProjbuffer);
                                //somecubeaschunkinstfront.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstfront, arrayofworldmatrix);
                            }

                            for (int i = 0; i < mainchunkdivback[someindexmain].somefacemeshlisttodraw.Count; i++)
                            {

                                mainchunkdivback[someindexmain].somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                                _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, null, mainchunkdivback[someindexmain], mainchunkdivback[someindexmain].somefacemeshlisttodraw[i], worldViewProjbuffer);
                                //somecubeaschunkinstback.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstback, worldViewProjbuffer);
                                //somecubeaschunkinstback.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstback, arrayofworldmatrix);
                            }



                            for (int i = 0; i < mainchunkdivbottom[someindexmain].somefacemeshlisttodraw.Count; i++)
                            {

                                mainchunkdivbottom[someindexmain].somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                                _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, null, mainchunkdivbottom[someindexmain], mainchunkdivbottom[someindexmain].somefacemeshlisttodraw[i], worldViewProjbuffer);
                                //somecubeaschunkinstbottom.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstbottom, worldViewProjbuffer);
                                //somecubeaschunkinstbottom.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstbottom, arrayofworldmatrix);
                            }

                        }
                    }
                }

            }





            /*
            rendervoxels();

            //matrixchangelevelgenbytes
            //PHYSICS SCREEN ASSETS
            worldlevelgenbytesassets[0][0].Render(directx.D3D.Device.ImmediateContext);
            _shaderManager.RenderInstancedObject(directx.D3D.Device.ImmediateContext, worldlevelgenbytesassets[0][0].IndexCount, worldlevelgenbytesassets[0][0].InstanceCount, worldlevelgenbytesassets[0][0]._POSITION, viewMatrix, _projectionMatrix, null, _DLightBuffer_cube, worldlevelgenbytesassets[0][0], 1, worldlevelgenbytesassets[0][0].indexofshader);
            //END OF
            */


            /*
            if (Program.createikrig == 1)
            {
                Matrix someextrapelvismatrix = rotatingMatrixForPelvis; //originRot

                Matrix someviewmat = viewMatrix;
                //someviewmat.Transpose();

                Matrix sometranspose = D3D.ProjectionMatrix;
                //sometranspose.Transpose();

                scgraphicssecpackagemessage.viewMatrix = someviewmat;
                scgraphicssecpackagemessage.projectionMatrix = sometranspose;
                scgraphicssecpackagemessage.originRot = originRot;
                scgraphicssecpackagemessage.rotatingMatrix = rotatingMatrix;
                scgraphicssecpackagemessage.hmdmatrixRot = hmdmatrixRot;
                scgraphicssecpackagemessage.hmd_matrix = hmd_matrix;
                scgraphicssecpackagemessage.rotatingMatrixForPelvis = rotatingMatrixForPelvis;
                scgraphicssecpackagemessage.rightTouchMatrix = _rightTouchMatrix;
                scgraphicssecpackagemessage.leftTouchMatrix = _leftTouchMatrix;
                scgraphicssecpackagemessage.oriProjectionMatrix = D3D.ProjectionMatrix;
                scgraphicssecpackagemessage.someextrapelvismatrix = someextrapelvismatrix;
                scgraphicssecpackagemessage.offsetpos = OFFSETPOS;
                scgraphicssecpackagemessage.handPoseRight = handPoseRight;
                scgraphicssecpackagemessage.handPoseLeft = handPoseLeft;


                sccswriteikrigtobuffer(scgraphicssecpackagemessage);

                someviewmat = viewMatrix;
                //someviewmat.Transpose();

                sometranspose = D3D.ProjectionMatrix;
                //sometranspose.Transpose();

                scgraphicssecpackagemessage.viewMatrix = someviewmat;
                scgraphicssecpackagemessage.projectionMatrix = sometranspose;
                scgraphicssecpackagemessage.originRot = originRot;
                scgraphicssecpackagemessage.rotatingMatrix = rotatingMatrix;
                scgraphicssecpackagemessage.hmdmatrixRot = hmdmatrixRot;
                scgraphicssecpackagemessage.hmd_matrix = hmd_matrix;
                scgraphicssecpackagemessage.rotatingMatrixForPelvis = rotatingMatrixForPelvis;
                scgraphicssecpackagemessage.rightTouchMatrix = _rightTouchMatrix;
                scgraphicssecpackagemessage.leftTouchMatrix = _leftTouchMatrix;
                scgraphicssecpackagemessage.oriProjectionMatrix = D3D.ProjectionMatrix;
                scgraphicssecpackagemessage.someextrapelvismatrix = someextrapelvismatrix;
                scgraphicssecpackagemessage.offsetpos = OFFSETPOS;
                scgraphicssecpackagemessage.handPoseRight = handPoseRight;
                scgraphicssecpackagemessage.handPoseLeft = handPoseLeft;


                workonikshaders(scgraphicssecpackagemessage);
                

            }*/





            if (cancleararrays == 0)
            {
                clearsomearrays();
                cancleararrays = 1;
            }

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




        Matrix _projectionMatrix;
        Matrix hmd_matrix_current = Matrix.Identity;
        Matrix hmdmatrixcurrentforpelvis = Matrix.Identity;

        public sccsikvoxellimbs[] ikarmvoxel;
        //Matrix[][][][] worldMatrix_instances_ikarmvoxel;
        int somechunkpriminstancesikarmvoxelwidthR = 2;
        int somechunkpriminstancesikarmvoxelheightR = 2;
        int somechunkpriminstancesikarmvoxeldepthR = 0;
        int somechunkpriminstancesikarmvoxelwidthL = 0;
        int somechunkpriminstancesikarmvoxelheightL = 0;
        int somechunkpriminstancesikarmvoxeldepthL = 0;

        public sccsikvoxellimbs[] ikvoxelbody;
        // Matrix[][][][] worldMatrix_instances_ikvoxelbody;
        int somechunkpriminstancesikvoxelbodywidthR = 1;
        int somechunkpriminstancesikvoxelbodyheightR = 0;
        int somechunkpriminstancesikvoxelbodydepthR = 0;
        int somechunkpriminstancesikvoxelbodywidthL = 0;
        int somechunkpriminstancesikvoxelbodyheightL = 0;
        int somechunkpriminstancesikvoxelbodydepthL = 0;

        public sccsikvoxellimbs[][] ikfingervoxel;
        //Matrix[][][][] worldMatrix_instances_ikarmvoxel;
        int somechunkpriminstancesikfingervoxelwidthR = 5;
        int somechunkpriminstancesikfingervoxelheightR = 1;
        int somechunkpriminstancesikfingervoxeldepthR = 0;
        int somechunkpriminstancesikfingervoxelwidthL = 0;
        int somechunkpriminstancesikfingervoxelheightL = 0;
        int somechunkpriminstancesikfingervoxeldepthL = 0;


        public void startrenderdirectxovr()
        {
            if (D3D != null)
            {



                Matrix someviewmat = viewMatrix;
                //someviewmat.Transpose();

                Matrix sometranspose = _projectionMatrix;
                //sometranspose.Transpose();

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
                scgraphicssecpackagemessage.someextrapelvismatrix = rotatingMatrixForPelvis;
                scgraphicssecpackagemessage.offsetpos = OFFSETPOS;
                scgraphicssecpackagemessage.handPoseRight = handPoseRight;
                scgraphicssecpackagemessage.handPoseLeft = handPoseLeft;

               sccswriteikrigtobuffer(scgraphicssecpackagemessage);



                writevoxelstobuffer();
                writecubestobuffer();
                
                if (hasgeneratedmeshes == 1)
                {
                    workonLevelGenChangeBytes();
                }
             

                oculustouchcontrols();


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
                    SharpDX.Vector3 eyePos = SharpDX.Vector3.Transform(new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z), originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot).ToVector3(); // 

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
                    _projectionMatrix = D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.001f, 1000.0f, ProjectionModifier.None).ToMatrix();
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


                        ///arrayofworldmatrix[0].viewmatrix = viewMatrix;
                        //arrayofworldmatrix[0].projectionmatrix = oriProjectionMatrix;
                        //arrayofworldmatrix[0].worldmatrix = D3D.WorldMatrix;

                        //arrayofworldmatrix[0].projectionmatrix.Transpose();
                        //arrayofworldmatrix[0].viewmatrix.Transpose();
                        //arrayofworldmatrix[0].worldmatrix.Transpose();


                        /*try
                        {
                            //PHYSICS VOXEL CUBES 
                            //////////////////////about 100 ticks more per loop compared to simple physics cubes? will investigate later as when i do 
                            //////////////////////simple cubes with the chunk it lags more even though the number of vertices are the same as the physics cube up above
                            //////////////////////todo: culling of faces by distance from player. etc.

                            _world_voxel_cube_lists[0][0].Render(directx.D3D.Device.ImmediateContext);
                            _shaderManager.RenderInstancedObjectsc_perko_voxel(directx.D3D.Device.ImmediateContext, _world_voxel_cube_lists[0][0].IndexCount, _world_voxel_cube_lists[0][0].InstanceCount, _world_voxel_cube_lists[0][0]._POSITION, viewMatrix, _projectionMatrix, null, _DLightBuffer_voxel_cube, _world_voxel_cube_lists[0][0]);
                            ///Console.WriteLine(_SystemTickPerformance.ElapsedTicks);
                        }
                        catch (Exception ex)
                        {
                            Program.MessageBox((IntPtr)0, ex.ToString() + "", "Oculus error", 0);
                        }
                        */

                        //matrixchangelevelgenbytes
                        //PHYSICS SCREEN ASSETS
                        worldlevelgenbytesassets[0][0].Render(directx.D3D.Device.ImmediateContext);
                        _shaderManager.RenderInstancedObject(directx.D3D.Device.ImmediateContext, worldlevelgenbytesassets[0][0].IndexCount, worldlevelgenbytesassets[0][0].InstanceCount, worldlevelgenbytesassets[0][0]._POSITION, viewMatrix, _projectionMatrix, null, _DLightBuffer_cube, worldlevelgenbytesassets[0][0], 1, worldlevelgenbytesassets[0][0].indexofshader);
                        //END OF



                       if (Program.createikrig == 1)
                        {


                            someviewmat = viewMatrix;
                            //someviewmat.Transpose();

                            sometranspose = _projectionMatrix;
                            //sometranspose.Transpose();

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
                        }

                        






                        worldViewProjbuffer = new updatePrim.worldviewprobuffer();
                        worldViewProjbuffer.worldmatrix = D3D.WorldMatrix;
                        worldViewProjbuffer.viewmatrix = viewMatrix;
                        worldViewProjbuffer.projectionmatrix = _projectionMatrix;
                        //someviewmat = viewMatrix;
                        //someviewmat.Transpose();

                        sometranspose = _projectionMatrix;
                        arrayofworldmatrix[0].viewmatrix = viewMatrix;
                        arrayofworldmatrix[0].projectionmatrix = sometranspose;
                        arrayofworldmatrix[0].worldmatrix = D3D.WorldMatrix;
                        //arrayofworldmatrix[0].viewmatrix.Transpose();

                        //if (updatesec!= null)
                        {
        






                            for (int x = sccslevelgen.minx, xe = 0; x < sccslevelgen.maxx; x += incrementsdivx, xe++)
                            {
                                for (int y = sccslevelgen.miny, ye = 0; y < sccslevelgen.maxy; y += incrementsdivy, ye++)
                                {
                                    for (int z = sccslevelgen.minz, ze = 0; z < sccslevelgen.maxz; z += incrementsdivz, ze++)
                                    {
                                        int xx = x;
                                        int yy = y;
                                        int zz = z;

                                        if (xx < 0)
                                        {
                                            xx *= -1;
                                            xx = xx + (sccslevelgen.maxx - 1);
                                        }

                                        if (yy < 0)
                                        {
                                            yy *= -1;
                                            yy = yy + (sccslevelgen.maxy - 1);
                                        }
                                        if (zz < 0)
                                        {
                                            zz *= -1;
                                            zz = zz + (sccslevelgen.maxz - 1);
                                        }



                                        int posmainx = x / incrementsdivx;
                                        int posmainy = y / incrementsdivy;
                                        int posmainz = z / incrementsdivz;

                                        if (posmainx < 0)
                                        {
                                            posmainx *= -1;
                                            posmainx = posmainx + ((divx / 2) - 1);
                                        }

                                        if (posmainy < 0)
                                        {
                                            posmainy *= -1;
                                            posmainy = posmainy + ((divy / 2) - 1);
                                        }
                                        if (posmainz < 0)
                                        {
                                            posmainz *= -1;
                                            posmainz = posmainz + ((divz / 2) - 1);
                                        }


                                        int somemainx = xe;
                                        int somemainy = ye;
                                        int somemainz = ze;

                                        int someindexmain = posmainx + (divx) * (posmainy + (divy) * posmainz);


                                        //Console.WriteLine("mainindex:" + someindexmain);
                                        for (int i = 0; i < mainchunkdivtop[someindexmain].somefacemeshlisttodraw.Count; i++)
                                        {
                                            //Console.WriteLine("topface");
                                            mainchunkdivtop[someindexmain].somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                                            _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, null, mainchunkdivtop[someindexmain], mainchunkdivtop[someindexmain].somefacemeshlisttodraw[i], worldViewProjbuffer);
                                            //somecubeaschunkinsttop.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinsttop, worldViewProjbuffer);
                                            //somecubeaschunkinsttop.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinsttop, arrayofworldmatrix);
                                        }
                                        

                                        
                                        for (int i = 0; i < mainchunkdivleft[someindexmain].somefacemeshlisttodraw.Count; i++)
                                        {

                                            mainchunkdivleft[someindexmain].somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                                            _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, null, mainchunkdivleft[someindexmain], mainchunkdivleft[someindexmain].somefacemeshlisttodraw[i], worldViewProjbuffer);
                                            //somecubeaschunkinstleft.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstleft, worldViewProjbuffer);
                                            //somecubeaschunkinstleft.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstleft, arrayofworldmatrix);
                                        }
                                        
                                        
                                        for (int i = 0; i < mainchunkdivright[someindexmain].somefacemeshlisttodraw.Count; i++)
                                        {

                                            mainchunkdivright[someindexmain].somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                                            _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, null, mainchunkdivright[someindexmain], mainchunkdivright[someindexmain].somefacemeshlisttodraw[i], worldViewProjbuffer);
                                            //somecubeaschunkinstright.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstright, worldViewProjbuffer);
                                            //somecubeaschunkinstright.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstright, arrayofworldmatrix);
                                        }
                                        

                                        
                                        for (int i = 0; i < mainchunkdivfront[someindexmain].somefacemeshlisttodraw.Count; i++)
                                        {

                                            mainchunkdivfront[someindexmain].somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                                            _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, null, mainchunkdivfront[someindexmain], mainchunkdivfront[someindexmain].somefacemeshlisttodraw[i], worldViewProjbuffer);
                                            //somecubeaschunkinstfront.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstfront, worldViewProjbuffer);
                                            //somecubeaschunkinstfront.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstfront, arrayofworldmatrix);
                                        }

                                        for (int i = 0; i < mainchunkdivback[someindexmain].somefacemeshlisttodraw.Count; i++)
                                        {

                                            mainchunkdivback[someindexmain].somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                                            _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, null, mainchunkdivback[someindexmain], mainchunkdivback[someindexmain].somefacemeshlisttodraw[i], worldViewProjbuffer);
                                            //somecubeaschunkinstback.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstback, worldViewProjbuffer);
                                            //somecubeaschunkinstback.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstback, arrayofworldmatrix);
                                        }
                                        for (int i = 0; i < mainchunkdivbottom[someindexmain].somefacemeshlisttodraw.Count; i++)
                                        {

                                            mainchunkdivbottom[someindexmain].somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                                            _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, null, mainchunkdivbottom[someindexmain], mainchunkdivbottom[someindexmain].somefacemeshlisttodraw[i], worldViewProjbuffer);
                                            //somecubeaschunkinstbottom.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstbottom, worldViewProjbuffer);
                                            //somecubeaschunkinstbottom.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstbottom, arrayofworldmatrix);
                                        }

                                    }
                                }
                            }
                        }

                    }


                    //_sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;

                    D3D.result = eyeTexture.SwapTextureSet.Commit();
                    D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to commit the swap chain texture.");


                }


            }

            

            if (cancleararrays == 0)
            {
                clearsomearrays();
                cancleararrays = 1;
            }
        }






        /*
        public void workonculling()
        {

            var chunkpos = somecubeaschunk.arraychunkdatalod0[c].realpos;

            float miny = 1;
            float minz = 10;

            float diagmaxx = 1;
            float diagmaxy = 1;
            float diagmaxz = 1;

            float diagminx = 1;
            float diagminy = 1;
            float diagminz = 1;


            float chunkwidthl = 7;
            float chunkwidthr = 6;

            float chunkheightl = 7;
            float chunkheightr = 6;

            float chunkdepthl = 31;
            float chunkdepthr = 30;

            var dirf = new Vector3(updateprim.dircamf.X, updateprim.dircamf.Y, updateprim.dircamf.Z);
            Vector3 dirr = new Vector3(updateprim.dircamr.X, updateprim.dircamr.Y, updateprim.dircamr.Z);


            Vector3 temppos = new Vector3(chunkpos[0], chunkpos[1], chunkpos[2]);
            distsquared = sc_maths.distancebasedondirection(out somedotprodlr, out somedotprodfb, dirf, dirr, posplayer, temppos, minx, miny, minz, diagminx, diagminy, diagminz, diagmaxx, diagmaxy, diagmaxz); // chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr

            if (somedotprodfb < -0.75f) //-0.50f
            {
                somedraw = true;
            }
            else
            {
                somedraw = false;
            }

        }*/









        public int countmeshculledlod0 = 0;
        public int countmeshtrigculledlod0 = 0;
        public int countmeshvertculledlod0 = 0;
        public int countmeshdistculledlod0 = 0;
        public int countmeshfrustculledlod0 = 0;
        public int countmeshtotallod0 = 0;
        public int countmeshtrigtotallod0 = 0;
        public int countmeshverttotallod0 = 0;

        public int countmeshculledlod1 = 0;
        public int countmeshtrigculledlod1 = 0;
        public int countmeshvertculledlod1 = 0;
        public int countmeshdistculledlod1 = 0;
        public int countmeshfrustculledlod1 = 0;
        public int countmeshtotallod1 = 0;
        public int countmeshtrigtotallod1 = 0;
        public int countmeshverttotallod1 = 0;

        public int countmeshculledlod2 = 0;
        public int countmeshtrigculledlod2 = 0;
        public int countmeshvertculledlod2 = 0;
        public int countmeshdistculledlod2 = 0;
        public int countmeshfrustculledlod2 = 0;
        public int countmeshtotallod2 = 0;
        public int countmeshtrigtotallod2 = 0;
        public int countmeshverttotallod2 = 0;

        public int countmeshculledlod3 = 0;
        public int countmeshtrigculledlod3 = 0;
        public int countmeshvertculledlod3 = 0;
        public int countmeshdistculledlod3 = 0;
        public int countmeshfrustculledlod3 = 0;
        public int countmeshtotallod3 = 0;
        public int countmeshtrigtotallod3 = 0;
        public int countmeshverttotallod3 = 0;

        public int countmeshculledlod4 = 0;
        public int countmeshtrigculledlod4 = 0;
        public int countmeshvertculledlod4 = 0;
        public int countmeshdistculledlod4 = 0;
        public int countmeshfrustculledlod4 = 0;
        public int countmeshtotallod4 = 0;
        public int countmeshtrigtotallod4 = 0;
        public int countmeshverttotallod4 = 0;


        Vector3 posplayer;

        int writetobufferinit;

        public int updatescriptssec(bool runapptype, Matrix worldViewProj, updatePrim.worldviewprobuffer[] arrayofworldmatrix)
        {
            countmeshdistculledlod0 = 0;
            countmeshfrustculledlod0 = 0;
            countmeshtrigculledlod0 = 0;
            countmeshvertculledlod0 = 0;
            countmeshculledlod0 = 0;

            countmeshdistculledlod1 = 0;
            countmeshfrustculledlod1 = 0;
            countmeshtrigculledlod1 = 0;
            countmeshvertculledlod1 = 0;
            countmeshculledlod1 = 0;

            countmeshdistculledlod2 = 0;
            countmeshfrustculledlod2 = 0;
            countmeshtrigculledlod2 = 0;
            countmeshvertculledlod2 = 0;
            countmeshculledlod2 = 0;

            countmeshdistculledlod3 = 0;
            countmeshfrustculledlod3 = 0;
            countmeshtrigculledlod3 = 0;
            countmeshvertculledlod3 = 0;
            countmeshculledlod3 = 0;

            countmeshdistculledlod4 = 0;
            countmeshfrustculledlod4 = 0;
            countmeshtrigculledlod4 = 0;
            countmeshvertculledlod4 = 0;
            countmeshculledlod4 = 0;


            if (Program.useOculusRift == 0)
            {
                posplayer = camera.GetPosition();
            }
            else
            {
                posplayer = OFFSETPOS;
            }

            //Console.WriteLine(posplayer);

            //hasfinishedwork = 0;
            //if (currentState.Exit)
            //    sccsr15forms.Form1.currentform.Close();




            /*cullingwatch.Stop();
            cullingwatch.Restart();*/
            /*
            if (threadswitch == 0)
            {
                main_thread_update = new Thread((sometest) =>
                {
                    try
                    {

                    _thread_looper:
                        // Setup the pipeline before any rendering
                        SetupPipeline();
                        workonculling();

                        Thread.Sleep(1);
                        goto _thread_looper;
                    }
                    catch (Exception ex)
                    {

                    }

                }, 0);

                main_thread_update.IsBackground = true;
                main_thread_update.Priority = ThreadPriority.Normal; //AboveNormal
                main_thread_update.SetApartmentState(ApartmentState.STA);
                main_thread_update.Start();

                threadswitch = 1;
            }*/









            /*
            if (threadswitchtwo == 0)
            {
                main_thread_update = new Thread(() =>
                {
                    try
                    {

                    _thread_looper:
                        


                        Thread.Sleep(1);
                        goto _thread_looper;
                    }
                    catch (Exception ex)
                    {

                    }

                }, 0);

                main_thread_update.IsBackground = true;
                main_thread_update.Priority = ThreadPriority.Normal; //AboveNormal
                main_thread_update.SetApartmentState(ApartmentState.STA);
                main_thread_update.Start();

                threadswitchtwo = 1;
            }*/


            //Console.WriteLine(cullingwatch.Elapsed.TotalMilliseconds);











            /*

            // Execute on the rendering thread when ThreadCount == 1 or No deferred rendering is selected
            if (currentState.Type == TestType.Immediate || (currentState.Type == TestType.Deferred && currentState.ThreadCount == 1))
            {
                renderrow(0, 0, MaxNumberOfCubes); //currentState.CountCubes
                ///

                /*somedataforthread.contextIndex = 0;
                somedataforthread.fromY = 0;
                somedataforthread.toY = D3D.MaxNumberOfCubes;
                somedataforthread.rendertherow = renderrow;


                if (threadswitchtwo == 0)
                {
                    var _console_writer_task = Task<object[]>.Factory.StartNew((tester0001) =>
                    {
                    //////CONSOLE WRITER=>
                    _thread_loop_console:

                        somedataforthread.rendertherow(0, 0, D3D.MaxNumberOfCubes);


                        Thread.Sleep(1);

                        goto _thread_loop_console;
                        //////CONSOLE WRITER <=
                    }, somedataforthread);

                    hreadswitchtwo = 1;
                }

            }


            // In case of deferred context, use of FinishCommandList / ExecuteCommandList
            if (currentState.Type != TestType.Immediate)
            {
                //counternumberofchunks = 0;
                if (currentState.Type == TestType.FrozenDeferred)
                {
                    //Console.WriteLine("frozendeferred");
                    if (commandsList[0] == null)
                    {
                        RenderDeferred(1);


                    }
               
                }
                else if (currentState.ThreadCount > 1)
                {
                    RenderDeferred(currentState.ThreadCount);


                    /*
                    if (threadswitchtwo == 1)
                    {
                        for (int i = 0; i < currentState.ThreadCount; i++)
                        {
                            //arrayoftasks[i]
                            Task sometask = Task<object[]>.Factory.StartNew((tester0001) =>
                            {

                                int sometaskindex = i;
                            //////CONSOLE WRITER=>
                            _thread_loop_console:

                                //Console.WriteLine(sometaskindex);


                                //renderrow(somedataforthread[i].contextIndex, somedataforthread[i].fromY, somedataforthread[i].toY);

                                //renderrowvoid(somedataforthread[i].contextIndex, somedataforthread[i].fromY, somedataforthread[i].toY);


                                /*if (somedataforthread[i].rendertherow != null)
                                {
                                    somedataforthread[i].rendertherow(somedataforthread[i].contextIndex, somedataforthread[i].fromY, somedataforthread[i].toY);
                                }

                                //commandsList[contextIndex]
                                // contextPerThread[contextIndex]


                                Thread.Sleep(1);

                                goto _thread_loop_console;
                                //////CONSOLE WRITER <=
                            }, commandsList[somedataforthread[i].contextIndex]);// somedataforthread[i]);


                            if (i >= currentState.ThreadCount)
                            {

                                threadswitchtwo = 2;
                            }

                        }
                    }

                }

                for (int i = 0; i < currentState.ThreadCount; i++)
                {
                    var commandList = commandsList[i];

                    if (D3D != null)
                    {
                        if (D3D.DeviceContext!= null)
                        {
                            if (commandList != null)
                            {
                                // Execute the deferred command list on the immediate context
                                D3D.DeviceContext.ExecuteCommandList(commandList, false);
                                // For classic deferred we release the command list. Not for frozen
                                if (currentState.Type == TestType.Deferred)
                                {
                                    if (commandList != null)
                                    {
                                        // Release the command list
                                        commandList.Dispose();
                                    }

                                    commandsList[i] = null;
                                }
                            }
                        }
                    }                        
                }
                //Console.WriteLine(counternumberofchunks);
            }
            if (switchToNextState)
            {
                currentState = nextState;
                switchToNextState = false;
            }
            */






            /*
            //////////////////////////////////
            //////////////////////////////////
            //////////////////////////////////
            D3D.DeviceContext.InputAssembler.InputLayout = somecube.layout;
            D3D.DeviceContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
            D3D.DeviceContext.InputAssembler.SetIndexBuffer(somecube.IndicesBuffer, SharpDX.DXGI.Format.R32_UInt, 0);

            //renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecube.verticesbuffer, Utilities.SizeOf<tutorialcubeaschunk.DVertex>(), 0));
            D3D.DeviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecube.verticesbuffer, Utilities.SizeOf<Vector4>() * 2, 0));
            D3D.DeviceContext.VertexShader.SetConstantBuffer(0, currentState.UseMap ? somecube.dynamicConstantBuffer : somecube.staticContantBuffer);
            D3D.DeviceContext.VertexShader.Set(somecube.vertexShader);
            D3D.DeviceContext.Rasterizer.SetViewport(0, 0, D3D.SurfaceWidth, D3D.SurfaceHeight);
            D3D.DeviceContext.PixelShader.Set(somecube.pixelShader);
            D3D.DeviceContext.OutputMerger.SetTargets(D3D.DepthStencilView, D3D.RenderTargetView);

            var view = updateprim.camera.ViewMatrix;// Matrix.LookAtLH(new Vector3(0, 0, -viewZ), new Vector3(0, 0, 0), Vector3.UnitY);
            //var proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, D3D.SurfaceWidth / (float)D3D.SurfaceHeight, 0.1f, 1000.0f);
            var viewProj = Matrix.Multiply(view, D3D.ProjectionMatrix);


            Matrix rotateMatrix = Matrix.Identity; //Matrix.Identity; //Matrix.Scaling(1.0f / 1.0f) * 
            Matrix worldViewProj;
            Matrix.Multiply(ref rotateMatrix, ref viewProj, out worldViewProj);
            worldViewProj.Transpose();

            if (currentState.UseMap)
            {
                var dataBox = D3D.DeviceContext.MapSubresource(somecube.dynamicConstantBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
                Utilities.Write(dataBox.DataPointer, ref worldViewProj);
                D3D.DeviceContext.UnmapSubresource(somecube.dynamicConstantBuffer, 0);
            }
            else
            {
                D3D.DeviceContext.UpdateSubresource(ref worldViewProj, somecube.staticContantBuffer);
            }

            // Draw the cube 
            //renderingContext.Draw(36, 0);
            //D3D.DeviceContext.DrawIndexed(somecube.arrayoftrigs.Length, 0, 0);
            D3D.DeviceContext.Draw(somecube.vertices.Length, 0);
            //////////////////////////////////*/
            //////////////////////////////////
            //////////////////////////////////



            /*
            float minx = 1;
            float miny = 1;
            float minz = 10;

            float diagmaxx = 1;
            float diagmaxy = 1;
            float diagmaxz = 1;

            float diagminx = 1;
            float diagminy = 1;
            float diagminz = 1;


            float chunkwidthl = 7;
            float chunkwidthr = 6;

            float chunkheightl = 7;
            float chunkheightr = 6;

            float chunkdepthl = 31;
            float chunkdepthr = 30;


            //float xview;
            //float yview;

            //Vector3 dirf = new Vector3(scupdate.dirikvoxelbodyInstanceForward0.X, scupdate.dirikvoxelbodyInstanceForward0.Y, scupdate.dirikvoxelbodyInstanceForward0.Z);
            //Vector3 dirr = new Vector3(scupdate.dirikvoxelbodyInstanceRight0.X, scupdate.dirikvoxelbodyInstanceRight0.Y, scupdate.dirikvoxelbodyInstanceRight0.Z);
            //Vector3 diru = new Vector3(scupdate.dirikvoxelbodyInstanceUp0.X, scupdate.dirikvoxelbodyInstanceUp0.Y, scupdate.dirikvoxelbodyInstanceUp0.Z);
            

            var dirf = new Vector3(updateprim.dircamf.X, updateprim.dircamf.Y, updateprim.dircamf.Z);
            Vector3 dirr = new Vector3(updateprim.dircamr.X, updateprim.dircamr.Y, updateprim.dircamr.Z);

            distsquared = sc_maths.distancebasedondirection(out somedotprodlr, out somedotprodfb, dirf, dirr, posplayer, somecube.position, minx, miny, minz, diagminx, diagminy, diagminz, diagmaxx, diagmaxy, diagmaxz); // chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr

            Console.WriteLine(somedotprodfb);
            */





            //totalmeshdistculled = countmeshdistculledlod0 + countmeshdistculledlod1 + countmeshdistculledlod2 + countmeshdistculledlod3 + countmeshdistculledlod4;
            //totalmeshfrustculled = countmeshfrustculledlod0 + countmeshfrustculledlod1 + countmeshfrustculledlod2 + countmeshfrustculledlod3 + countmeshfrustculledlod4;




            if (Program.useOculusRift == 0)
            {
                if (writetobufferinit == 0)
                {
                    writevoxelstobuffer();

                    writetobufferinit = 1;
                }

                rendervoxels();






















                Matrix tempmat = camera.rotationMatrix;
                Quaternion quatt;
                Quaternion.RotationMatrix(ref tempmat, out quatt);

                //var viewMatrix = updateprim.camera.ViewMatrix;
                //viewPosition = updateprim.camera.GetPosition() + (new Vector3(dirikvoxelbodyInstanceForward0.X, dirikvoxelbodyInstanceForward0.Y, dirikvoxelbodyInstanceForward0.Z) * Program.offsetthirdpersonview);

                //updateprim.camera.GetPosition()
                Matrix viewMatrix = camera.ViewMatrix;//Matrix.Identity;//= Matrix.LookAtRH(viewPosition, viewPosition + lookat, lookup);

                /*if (Program.usethirdpersonview == 0)
                {

                    //FOR THE VERTEX SHADER
                    Quaternion somedirquat1;
                    Quaternion.RotationMatrix(ref tempmat, out somedirquat1);
                    dirikvoxelbodyInstanceRight0 = new Vector4(-sc_maths._newgetdirleft(somedirquat1), 0);
                    dirikvoxelbodyInstanceUp0 = new Vector4(sc_maths._newgetdirup(somedirquat1), 0);
                    dirikvoxelbodyInstanceForward0 = new Vector4(sc_maths._newgetdirforward(somedirquat1), 0);
                    Vector3 lookat = new Vector3(dirikvoxelbodyInstanceForward0.X, dirikvoxelbodyInstanceForward0.Y, dirikvoxelbodyInstanceForward0.Z);
                    Vector3 lookup = new Vector3(dirikvoxelbodyInstanceUp0.X, dirikvoxelbodyInstanceUp0.Y, dirikvoxelbodyInstanceUp0.Z);


                    viewPosition = updateprim.camera.GetPosition();
                    viewMatrix = updateprim.camera.ViewMatrix; // Matrix.LookAtRH(viewPosition, viewPosition + lookat, lookup);


                }
                else if (Program.usethirdpersonview == 1)
                {
                    Quaternion somedirquat1;
                    Quaternion.RotationMatrix(ref tempmat, out somedirquat1);
                    dirikvoxelbodyInstanceRight0 = new Vector4(-sc_maths._newgetdirleft(somedirquat1), 0);
                    dirikvoxelbodyInstanceUp0 = new Vector4(sc_maths._newgetdirup(somedirquat1), 0);
                    dirikvoxelbodyInstanceForward0 = new Vector4(sc_maths._newgetdirforward(somedirquat1), 0);
                    Vector3 lookat = new Vector3(dirikvoxelbodyInstanceForward0.X, dirikvoxelbodyInstanceForward0.Y, dirikvoxelbodyInstanceForward0.Z);
                    Vector3 lookup = new Vector3(dirikvoxelbodyInstanceUp0.X, dirikvoxelbodyInstanceUp0.Y, dirikvoxelbodyInstanceUp0.Z);


                    viewPosition = updateprim.camera.GetPosition() + (new Vector3(dirikvoxelbodyInstanceForward0.X, dirikvoxelbodyInstanceForward0.Y, dirikvoxelbodyInstanceForward0.Z) * Program.offsetthirdpersonview);
                    viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookat, lookup);

                }
                */

                //var view = updateprim.camera.ViewMatrix;// Matrix.LookAtLH(new Vector3(0, 0, -viewZ), new Vector3(0, 0, 0), Vector3.UnitY);
                //var proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, D3D.SurfaceWidth / (float)D3D.SurfaceHeight, 0.1f, 1000.0f);
                //var viewProj = Matrix.Multiply(view, D3D.ProjectionMatrix);


                //viewMatrix =  Matrix.LookAtLH(new Vector3(0, 0, -1), new Vector3(0, 0, 0), Vector3.UnitY);
                var proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, D3D.SurfaceWidth / (float)D3D.SurfaceHeight, 0.001f, 1000.0f);


                var viewProj = Matrix.Multiply(viewMatrix, D3D.ProjectionMatrix);

                Matrix rotateMatrix = Matrix.Identity;
                //Matrix worldViewProj;
                Matrix.Multiply(ref rotateMatrix, ref viewProj, out worldViewProj);
                worldViewProj.Transpose();

                /*arrayofworldmatrix = new worldviewprobuffer[1];
                arrayofworldmatrix[0].viewmatrix = viewMatrix;
                arrayofworldmatrix[0].projectionmatrix = proj;
                arrayofworldmatrix[0].worldmatrix = D3D.WorldMatrix;

                arrayofworldmatrix[0].projectionmatrix.Transpose();
                arrayofworldmatrix[0].viewmatrix.Transpose();
                arrayofworldmatrix[0].worldmatrix.Transpose();*/


                arrayofworldmatrix = new updatePrim.worldviewprobuffer[1];
                arrayofworldmatrix[0] = new updatePrim.worldviewprobuffer();
                arrayofworldmatrix[0].viewmatrix = viewMatrix;
                arrayofworldmatrix[0].projectionmatrix = proj;
                arrayofworldmatrix[0].worldmatrix = D3D.WorldMatrix;


                arrayofworldmatrix[0].worldmatrix.Transpose();
                arrayofworldmatrix[0].projectionmatrix.Transpose();
                arrayofworldmatrix[0].viewmatrix.Transpose();

                /*for (int i = 0; i < somecubeaschunkinsttop.somefacemeshlisttodraw.Count; i++)
                {
                    somecubeaschunkinsttop.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinsttop, arrayofworldmatrix);
                }


                for (int i = 0; i < somecubeaschunkinstleft.somefacemeshlisttodraw.Count; i++)
                {
                    somecubeaschunkinstleft.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstleft, arrayofworldmatrix);
                }



                for (int i = 0; i < somecubeaschunkinstright.somefacemeshlisttodraw.Count; i++)
                {
                    somecubeaschunkinstright.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstright, arrayofworldmatrix);
                }

                //Console.WriteLine(somecubeaschunkinstright.somefacemeshlisttodraw.Count);


                for (int i = 0; i < somecubeaschunkinstfront.somefacemeshlisttodraw.Count; i++)
                {
                    somecubeaschunkinstfront.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstfront, arrayofworldmatrix);
                }


                for (int i = 0; i < somecubeaschunkinstback.somefacemeshlisttodraw.Count; i++)
                {
                    somecubeaschunkinstback.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstback, arrayofworldmatrix);
                }


                for (int i = 0; i < somecubeaschunkinstbottom.somefacemeshlisttodraw.Count; i++)
                {
                    somecubeaschunkinstbottom.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstbottom, arrayofworldmatrix);
                }
                */


                updatePrim.worldviewprobuffer worldViewProjbuffer = new updatePrim.worldviewprobuffer();
                worldViewProjbuffer.worldmatrix = D3D.WorldMatrix;
                worldViewProjbuffer.viewmatrix = viewMatrix;
                worldViewProjbuffer.projectionmatrix = D3D.ProjectionMatrix;
                //someviewmat = viewMatrix;
                //someviewmat.Transpose();

                var sometranspose = D3D.ProjectionMatrix;
                arrayofworldmatrix[0].viewmatrix = viewMatrix;
                arrayofworldmatrix[0].projectionmatrix = sometranspose;
                arrayofworldmatrix[0].worldmatrix = D3D.WorldMatrix;
                /*
                for (int i = 0; i < somecubeaschunkinsttop.somefacemeshlisttodraw.Count; i++)
                {

                    somecubeaschunkinsttop.somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                    _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, D3D.WorldMatrix, viewMatrix, sometranspose, null, somecubeaschunkinsttop, somecubeaschunkinsttop.somefacemeshlisttodraw[i], worldViewProjbuffer);
                    //somecubeaschunkinsttop.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, D3D.WorldMatrix, viewMatrix, _projectionMatrix, somecubeaschunkinsttop, worldViewProjbuffer);
                    //somecubeaschunkinsttop.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinsttop, arrayofworldmatrix);
                }

               for (int i = 0; i < somecubeaschunkinstleft.somefacemeshlisttodraw.Count; i++)
                {

                    somecubeaschunkinstleft.somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                    _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, D3D.WorldMatrix, viewMatrix, sometranspose, null, somecubeaschunkinstleft, somecubeaschunkinstleft.somefacemeshlisttodraw[i], worldViewProjbuffer);
                    //somecubeaschunkinstleft.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, D3D.WorldMatrix, viewMatrix, _projectionMatrix, somecubeaschunkinstleft, worldViewProjbuffer);
                    //somecubeaschunkinstleft.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstleft, arrayofworldmatrix);
                }
                for (int i = 0; i < somecubeaschunkinstright.somefacemeshlisttodraw.Count; i++)
                {

                    somecubeaschunkinstright.somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                    _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, D3D.WorldMatrix, viewMatrix, sometranspose, null, somecubeaschunkinstright, somecubeaschunkinstright.somefacemeshlisttodraw[i], worldViewProjbuffer);
                    //somecubeaschunkinstright.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, D3D.WorldMatrix, viewMatrix, _projectionMatrix, somecubeaschunkinstright, worldViewProjbuffer);
                    //somecubeaschunkinstright.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstright, arrayofworldmatrix);
                }

                for (int i = 0; i < somecubeaschunkinstfront.somefacemeshlisttodraw.Count; i++)
                {

                    somecubeaschunkinstfront.somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                    _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, D3D.WorldMatrix, viewMatrix, sometranspose, null, somecubeaschunkinstfront, somecubeaschunkinstfront.somefacemeshlisttodraw[i], worldViewProjbuffer);
                    //somecubeaschunkinstfront.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, D3D.WorldMatrix, viewMatrix, _projectionMatrix, somecubeaschunkinstfront, worldViewProjbuffer);
                    //somecubeaschunkinstfront.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstfront, arrayofworldmatrix);
                }

                for (int i = 0; i < somecubeaschunkinstback.somefacemeshlisttodraw.Count; i++)
                {

                    somecubeaschunkinstback.somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                    _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, D3D.WorldMatrix, viewMatrix, sometranspose, null, somecubeaschunkinstback, somecubeaschunkinstback.somefacemeshlisttodraw[i], worldViewProjbuffer);
                    //somecubeaschunkinstback.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, D3D.WorldMatrix, viewMatrix, _projectionMatrix, somecubeaschunkinstback, worldViewProjbuffer);
                    //somecubeaschunkinstback.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstback, arrayofworldmatrix);
                }
                for (int i = 0; i < somecubeaschunkinstbottom.somefacemeshlisttodraw.Count; i++)
                {

                    somecubeaschunkinstbottom.somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                    _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, D3D.WorldMatrix, viewMatrix, sometranspose, null, somecubeaschunkinstbottom, somecubeaschunkinstbottom.somefacemeshlisttodraw[i], worldViewProjbuffer);
                    //somecubeaschunkinstbottom.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, D3D.WorldMatrix, viewMatrix, _projectionMatrix, somecubeaschunkinstbottom, worldViewProjbuffer);
                    //somecubeaschunkinstbottom.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstbottom, arrayofworldmatrix);
                }*/



                for (int x = sccslevelgen.minx, xe = 0; x < sccslevelgen.maxx; x += incrementsdivx, xe++)
                {
                    for (int y = sccslevelgen.miny, ye = 0; y < sccslevelgen.maxy; y += incrementsdivy, ye++)
                    {
                        for (int z = sccslevelgen.minz, ze = 0; z < sccslevelgen.maxz; z += incrementsdivz, ze++)
                        {
                            int xx = x;
                            int yy = y;
                            int zz = z;

                            if (xx < 0)
                            {
                                xx *= -1;
                                xx = xx + (sccslevelgen.maxx - 1);
                            }

                            if (yy < 0)
                            {
                                yy *= -1;
                                yy = yy + (sccslevelgen.maxy - 1);
                            }
                            if (zz < 0)
                            {
                                zz *= -1;
                                zz = zz + (sccslevelgen.maxz - 1);
                            }

                            int posmainx = x / incrementsdivx;
                            int posmainy = y / incrementsdivy;
                            int posmainz = z / incrementsdivz;

                            if (posmainx < 0)
                            {
                                posmainx *= -1;
                                posmainx = posmainx + ((divx / 2) - 1);
                            }

                            if (posmainy < 0)
                            {
                                posmainy *= -1;
                                posmainy = posmainy + ((divy / 2) - 1);
                            }
                            if (posmainz < 0)
                            {
                                posmainz *= -1;
                                posmainz = posmainz + ((divz / 2) - 1);
                            }


                            int somemainx = xe;
                            int somemainy = ye;
                            int somemainz = ze;

                            int someindexmain = posmainx + (divx) * (posmainy + (divy) * posmainz);

                            for (int i = 0; i < mainchunkdivtop[someindexmain].somefacemeshlisttodraw.Count; i++)
                            {
                                //Console.WriteLine("topface");
                                mainchunkdivtop[someindexmain].somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                                _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, null, mainchunkdivtop[someindexmain], mainchunkdivtop[someindexmain].somefacemeshlisttodraw[i], worldViewProjbuffer);
                                //somecubeaschunkinsttop.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinsttop, worldViewProjbuffer);
                                //somecubeaschunkinsttop.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinsttop, arrayofworldmatrix);
                            }


                            
                            
                            for (int i = 0; i < mainchunkdivleft[someindexmain].somefacemeshlisttodraw.Count; i++)
                            {

                                mainchunkdivleft[someindexmain].somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                                _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, null, mainchunkdivleft[someindexmain], mainchunkdivleft[someindexmain].somefacemeshlisttodraw[i], worldViewProjbuffer);
                                //somecubeaschunkinstleft.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstleft, worldViewProjbuffer);
                                //somecubeaschunkinstleft.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstleft, arrayofworldmatrix);
                            }
                            
                            for (int i = 0; i < mainchunkdivright[someindexmain].somefacemeshlisttodraw.Count; i++)
                            {

                                mainchunkdivright[someindexmain].somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                                _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, null, mainchunkdivright[someindexmain], mainchunkdivright[someindexmain].somefacemeshlisttodraw[i], worldViewProjbuffer);
                                //somecubeaschunkinstright.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstright, worldViewProjbuffer);
                                //somecubeaschunkinstright.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstright, arrayofworldmatrix);
                            }

                            for (int i = 0; i < mainchunkdivfront[someindexmain].somefacemeshlisttodraw.Count; i++)
                            {

                                mainchunkdivfront[someindexmain].somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                                _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, null, mainchunkdivfront[someindexmain], mainchunkdivfront[someindexmain].somefacemeshlisttodraw[i], worldViewProjbuffer);
                                //somecubeaschunkinstfront.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstfront, worldViewProjbuffer);
                                //somecubeaschunkinstfront.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstfront, arrayofworldmatrix);
                            }

                            for (int i = 0; i < mainchunkdivback[someindexmain].somefacemeshlisttodraw.Count; i++)
                            {

                                mainchunkdivback[someindexmain].somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                                _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, null, mainchunkdivback[someindexmain], mainchunkdivback[someindexmain].somefacemeshlisttodraw[i], worldViewProjbuffer);
                                //somecubeaschunkinstback.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstback, worldViewProjbuffer);
                                //somecubeaschunkinstback.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstback, arrayofworldmatrix);
                            }


                            for (int i = 0; i < mainchunkdivbottom[someindexmain].somefacemeshlisttodraw.Count; i++)
                            {

                                mainchunkdivbottom[someindexmain].somefacemeshlisttodraw[i].Render(D3D.Device.ImmediateContext);
                                _shaderManager.Rendertutorialchunkinstmesh(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, null, mainchunkdivbottom[someindexmain], mainchunkdivbottom[someindexmain].somefacemeshlisttodraw[i], worldViewProjbuffer);
                                //somecubeaschunkinstbottom.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstbottom, worldViewProjbuffer);
                                //somecubeaschunkinstbottom.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstbottom, arrayofworldmatrix);
                            }


                        }
                    }
                }

                if (Program.createikrig == 1)
                {
                    /*Matrix someextrapelvismatrix = updateprim.rotatingMatrixForPelvis; //originRot

                    Matrix someviewmat = viewMatrix;
                    //someviewmat.Transpose();

                    Matrix sometranspose = D3D.ProjectionMatrix;
                    //sometranspose.Transpose();

                    scgraphicssecpackagemessage.viewMatrix = someviewmat;
                    scgraphicssecpackagemessage.projectionMatrix = sometranspose;
                    scgraphicssecpackagemessage.originRot = updateprim.originRot;
                    scgraphicssecpackagemessage.rotatingMatrix = updateprim.rotatingMatrix;
                    scgraphicssecpackagemessage.hmdmatrixRot = updateprim.hmdmatrixRot;
                    scgraphicssecpackagemessage.hmd_matrix = updateprim.hmd_matrix;
                    scgraphicssecpackagemessage.rotatingMatrixForPelvis = updateprim.rotatingMatrixForPelvis;
                    scgraphicssecpackagemessage.rightTouchMatrix = updateprim._rightTouchMatrix;
                    scgraphicssecpackagemessage.leftTouchMatrix = updateprim._leftTouchMatrix;
                    scgraphicssecpackagemessage.oriProjectionMatrix = D3D.ProjectionMatrix;
                    scgraphicssecpackagemessage.someextrapelvismatrix = someextrapelvismatrix;
                    scgraphicssecpackagemessage.offsetpos = updateprim.OFFSETPOS;
                    scgraphicssecpackagemessage.handPoseRight = updateprim.handPoseRight;
                    scgraphicssecpackagemessage.handPoseLeft = updateprim.handPoseLeft;


                    sccswriteikrigtobuffer(scgraphicssecpackagemessage);

                    someviewmat = viewMatrix;
                    //someviewmat.Transpose();

                    sometranspose = D3D.ProjectionMatrix;
                    //sometranspose.Transpose();

                    scgraphicssecpackagemessage.viewMatrix = someviewmat;
                    scgraphicssecpackagemessage.projectionMatrix = sometranspose;
                    scgraphicssecpackagemessage.originRot = updateprim.originRot;
                    scgraphicssecpackagemessage.rotatingMatrix = updateprim.rotatingMatrix;
                    scgraphicssecpackagemessage.hmdmatrixRot = updateprim.hmdmatrixRot;
                    scgraphicssecpackagemessage.hmd_matrix = updateprim.hmd_matrix;
                    scgraphicssecpackagemessage.rotatingMatrixForPelvis = updateprim.rotatingMatrixForPelvis;
                    scgraphicssecpackagemessage.rightTouchMatrix = updateprim._rightTouchMatrix;
                    scgraphicssecpackagemessage.leftTouchMatrix = updateprim._leftTouchMatrix;
                    scgraphicssecpackagemessage.oriProjectionMatrix = D3D.ProjectionMatrix;
                    scgraphicssecpackagemessage.someextrapelvismatrix = someextrapelvismatrix;
                    scgraphicssecpackagemessage.offsetpos = updateprim.OFFSETPOS;
                    scgraphicssecpackagemessage.handPoseRight = updateprim.handPoseRight;
                    scgraphicssecpackagemessage.handPoseLeft = updateprim.handPoseLeft;


                    workonikshaders(scgraphicssecpackagemessage);
                    */

                }



            }
            else if (Program.useOculusRift == 1)
            {




                //////////////////////////////////
                //////////////////////////////////
                //////////////////////////////////
                /*D3D.DeviceContext.InputAssembler.InputLayout = somecube.layout;
                D3D.DeviceContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
                D3D.DeviceContext.InputAssembler.SetIndexBuffer(somecube.IndicesBuffer, SharpDX.DXGI.Format.R32_UInt, 0);

                //renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecube.verticesbuffer, Utilities.SizeOf<tutorialcubeaschunk.DVertex>(), 0));
                D3D.DeviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecube.verticesbuffer, Utilities.SizeOf<Vector4>() * 2, 0));
                D3D.DeviceContext.VertexShader.SetConstantBuffer(0, currentState.UseMap ? somecube.dynamicConstantBuffer : somecube.staticContantBuffer);
                D3D.DeviceContext.VertexShader.Set(somecube.vertexShader);
                //D3D.DeviceContext.Rasterizer.SetViewport(0, 0, D3D.SurfaceWidth, D3D.SurfaceHeight);
                D3D.DeviceContext.PixelShader.Set(somecube.pixelShader);
                //D3D.DeviceContext.OutputMerger.SetTargets(D3D.DepthStencilView, D3D.RenderTargetView);

                //var view = arrayofworldmatrix[0].viewmatrix;// updateprim.camera.ViewMatrix;// Matrix.LookAtLH(new Vector3(0, 0, -viewZ), new Vector3(0, 0, 0), Vector3.UnitY);
                                                            //var proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, D3D.SurfaceWidth / (float)D3D.SurfaceHeight, 0.1f, 1000.0f);
                //var viewProj = Matrix.Multiply(view, arrayofworldmatrix[0].projectionmatrix);


                //Matrix rotateMatrix = Matrix.Identity; //Matrix.Identity; //Matrix.Scaling(1.0f / 1.0f) * 
                //Matrix worldViewProj;
                //Matrix.Multiply(ref rotateMatrix, ref viewProj, out worldViewProj);
                //worldViewProj.Transpose();

                if (currentState.UseMap)
                {
                    var dataBox = D3D.DeviceContext.MapSubresource(somecube.dynamicConstantBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
                    Utilities.Write(dataBox.DataPointer, ref worldViewProj);
                    D3D.DeviceContext.UnmapSubresource(somecube.dynamicConstantBuffer, 0);
                }
                else
                {
                    D3D.DeviceContext.UpdateSubresource(ref worldViewProj, somecube.staticContantBuffer);
                }

                // Draw the cube 
                //renderingContext.Draw(36, 0);
                //D3D.DeviceContext.DrawIndexed(somecube.arrayoftrigs.Length, 0, 0);
                D3D.DeviceContext.Draw(somecube.vertices.Length, 0);*/
                //////////////////////////////////
                //////////////////////////////////
                //////////////////////////////////













                /*
                _projectionMatrix.Transpose();

                Matrix someviewmat = viewMatrix;
                //someviewmat.Transpose();

                Matrix sometranspose = _projectionMatrix;
                sometranspose.Transpose();

                scgraphicssecpackagemessage.viewMatrix = someviewmat;
                scgraphicssecpackagemessage.projectionMatrix = sometranspose;
                scgraphicssecpackagemessage.originRot = updateprim.originRot;
                scgraphicssecpackagemessage.rotatingMatrix = updateprim.rotatingMatrix;
                scgraphicssecpackagemessage.hmdmatrixRot = updateprim.hmdmatrixRot;
                scgraphicssecpackagemessage.hmd_matrix = updateprim.hmd_matrix;
                scgraphicssecpackagemessage.rotatingMatrixForPelvis = updateprim.rotatingMatrixForPelvis;
                scgraphicssecpackagemessage.rightTouchMatrix = updateprim._rightTouchMatrix;
                scgraphicssecpackagemessage.leftTouchMatrix = updateprim._leftTouchMatrix;
                scgraphicssecpackagemessage.oriProjectionMatrix = D3D.ProjectionMatrix;
                scgraphicssecpackagemessage.someextrapelvismatrix = someextrapelvismatrix;
                scgraphicssecpackagemessage.offsetpos = updateprim.OFFSETPOS;
                scgraphicssecpackagemessage.handPoseRight = updateprim.handPoseRight;
                scgraphicssecpackagemessage.handPoseLeft = updateprim.handPoseLeft;*/


                //sccswriteikrigtobuffer(scgraphicssecpackagemessage);

                /*arrayofworldmatrix = new updatePrim.worldviewprobuffer[1];
                arrayofworldmatrix[0] = new updatePrim.worldviewprobuffer();
                arrayofworldmatrix[0].viewmatrix = viewMatrix;
                arrayofworldmatrix[0].projectionmatrix = proj;
                arrayofworldmatrix[0].worldmatrix = D3D.WorldMatrix;


                arrayofworldmatrix[0].worldmatrix.Transpose();
                arrayofworldmatrix[0].projectionmatrix.Transpose();
                arrayofworldmatrix[0].viewmatrix.Transpose();*/




                /*worldViewProjbuffer.worldmatrix = arrayofworldmatrix[0].worldmatrix;
                worldViewProjbuffer.viewmatrix = arrayofworldmatrix[0].viewmatrix;
                worldViewProjbuffer.projectionmatrix = arrayofworldmatrix[0].projectionmatrix;*/


                /*
                for (int i = 0; i < somecubeaschunkinsttop.somefacemeshlisttodraw.Count; i++)
                {
                    somecubeaschunkinsttop.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinsttop, worldViewProjbuffer);
                    somecubeaschunkinsttop.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinsttop, arrayofworldmatrix);
                }

                for (int i = 0; i < somecubeaschunkinstleft.somefacemeshlisttodraw.Count; i++)
                {
                    somecubeaschunkinstleft.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstleft, worldViewProjbuffer);

                    somecubeaschunkinstleft.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstleft, arrayofworldmatrix);
                }

                for (int i = 0; i < somecubeaschunkinstright.somefacemeshlisttodraw.Count; i++)
                {
                    somecubeaschunkinstright.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstright, worldViewProjbuffer);

                    somecubeaschunkinstright.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstright, arrayofworldmatrix);
                }

                for (int i = 0; i < somecubeaschunkinstfront.somefacemeshlisttodraw.Count; i++)
                {
                    somecubeaschunkinstfront.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstfront, worldViewProjbuffer);

                    somecubeaschunkinstfront.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstfront, arrayofworldmatrix);
                }

                for (int i = 0; i < somecubeaschunkinstback.somefacemeshlisttodraw.Count; i++)
                {
                    somecubeaschunkinstback.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstback, worldViewProjbuffer);

                    somecubeaschunkinstback.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstback, arrayofworldmatrix);
                }

                for (int i = 0; i < somecubeaschunkinstbottom.somefacemeshlisttodraw.Count; i++)
                {
                    somecubeaschunkinstbottom.somefacemeshlisttodraw[i].SetShaderParameters(D3D.DeviceContext, arrayofworldmatrix[0].worldmatrix, arrayofworldmatrix[0].viewmatrix, arrayofworldmatrix[0].projectionmatrix, somecubeaschunkinstbottom, worldViewProjbuffer);

                    somecubeaschunkinstbottom.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinstbottom, arrayofworldmatrix);
                }*/



                /*
               */
            }



            //Console.WriteLine(somecubeaschunkinst.somefacemeshlisttodraw.Count);



            /*
            /////////////////////////////////////////////
            /////////////////////////////////////////////
            D3D.DeviceContext.Rasterizer.SetViewport(0, 0, D3D.SurfaceWidth, D3D.SurfaceHeight);
            D3D.DeviceContext.OutputMerger.SetTargets(D3D.DepthStencilView, D3D.RenderTargetView);
            updateprim.updatescriptsupdatetext(D3D.DeviceContext);
            /////////////////////////////////////////////*/
            /////////////////////////////////////////////




            hasfinishedwork = 1;
            return hasfinishedwork;
        }

        int hasfinishedwork = 0;


        int cancleararrays = 0;
        
        public void clearsomearrays()
        {



            if (sccslevelgen.arraychunkdatalod0 != null)
            {
                for (int ii = 0; ii < sccslevelgen.arraychunkdatalod0.Length; ii++)
                {
                    if (sccslevelgen.arraychunkdatalod0[ii] != null)
                    {
                        for (int i = 0; i < sccslevelgen.arraychunkdatalod0[ii].Length; i++)
                        {
                            if (sccslevelgen.arraychunkdatalod0[ii][i].arraychunkvertslod0 != null)
                            {
                                sccslevelgen.arraychunkdatalod0[ii][i].arraychunkvertslod0.cleararrays();
                                sccslevelgen.arraychunkdatalod0[ii][i].vertexcount = 0;//.cleararrays();

                            }
                        }
                        //sccslevelgen.arraychunkdatalod0[ii] = null;
                    }
                }
            }

            //sccslevelgen.arraychunkdatalod0 = null;






            /*

            for (int x = sccslevelgen.minx, xe = 0; x < sccslevelgen.maxx; x += incrementsdivx, xe++)
            {
                for (int y = sccslevelgen.miny, ye = 0; y < sccslevelgen.maxy; y += incrementsdivy, ye++)
                {
                    for (int z = sccslevelgen.minz, ze = 0; z < sccslevelgen.maxz; z += incrementsdivz, ze++)
                    {
                        int xx = x;
                        int yy = y;
                        int zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = xx + (sccslevelgen.maxx - 1);
                        }

                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = yy + (sccslevelgen.maxy - 1);
                        }
                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = zz + (sccslevelgen.maxz - 1);
                        }

                        int posmainx = x / incrementsdivx;
                        int posmainy = y / incrementsdivy;
                        int posmainz = z / incrementsdivz;

                        int somemainx = xe;
                        int somemainy = ye;
                        int somemainz = ze;

                        var someindexmain = somemainx + (divx) * (somemainy + (divy) * somemainz);


                        for (int i = 0; i < mainchunkdivtop[someindexmain].arraychunkdatalod0.Length; i++)
                        {
                            mainchunkdivtop[someindexmain].arraychunkdatalod0[i].arraychunkvertslod0.cleararrays();
                        }

                        for (int i = 0; i < mainchunkdivleft[someindexmain].arraychunkdatalod0.Length; i++)
                        {
                            mainchunkdivleft[someindexmain].arraychunkdatalod0[i].arraychunkvertslod0.cleararrays();

                        }


                        for (int i = 0; i < mainchunkdivright[someindexmain].arraychunkdatalod0.Length; i++)
                        {
                            mainchunkdivright[someindexmain].arraychunkdatalod0[i].arraychunkvertslod0.cleararrays();
                        }

                        for (int i = 0; i < mainchunkdivfront[someindexmain].arraychunkdatalod0.Length; i++)
                        {
                            mainchunkdivfront[someindexmain].arraychunkdatalod0[i].arraychunkvertslod0.cleararrays();
                        }

                        for (int i = 0; i < mainchunkdivback[someindexmain].arraychunkdatalod0.Length; i++)
                        {
                            mainchunkdivback[someindexmain].arraychunkdatalod0[i].arraychunkvertslod0.cleararrays();

                        }
                        for (int i = 0; i < mainchunkdivbottom[someindexmain].arraychunkdatalod0.Length; i++)
                        {
                            mainchunkdivbottom[someindexmain].arraychunkdatalod0[i].arraychunkvertslod0.cleararrays();

                        }
                    }
                }
            }*/

            /*
            sccslevelgen.arraychunkdatalod0 = null;
            sccslevelgen.levelmap = null;*/
        }
        








        Vector3 oculusRiftDir = Vector3.Zero;
        Vector3[] directionvectoroffsets;





        public scmessageobjectjitter[][] sccswriteikrigtobuffer(scgraphicssecpackage scgraphicssecpackagemessage)
        {


            scmessageobjectjitter[][] _sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;
            Matrix viewMatrix = scgraphicssecpackagemessage.viewMatrix;
            Matrix projectionMatrix = scgraphicssecpackagemessage.projectionMatrix; //_projectionMatrix;
            Matrix originRot = scgraphicssecpackagemessage.originRot; // originRot;
            Matrix rotatingMatrix = scgraphicssecpackagemessage.rotatingMatrix; //rotatingMatrix;
            Matrix hmdrotMatrix = scgraphicssecpackagemessage.hmdmatrixRot; //hmdmatrixRot;
            Matrix hmd_matrix = scgraphicssecpackagemessage.hmd_matrix; //hmd_matrix;
            Matrix rotatingMatrixForPelvis = scgraphicssecpackagemessage.rotatingMatrixForPelvis; //rotatingMatrixForPelvis;
            Matrix _rightTouchMatrix = scgraphicssecpackagemessage.rightTouchMatrix; //_rightTouchMatrix;
            Matrix _leftTouchMatrix = scgraphicssecpackagemessage.leftTouchMatrix; //_leftTouchMatrix;
            Matrix oriProjectionMatrix = scgraphicssecpackagemessage.oriProjectionMatrix; //oriProjectionMatrix;
            Matrix extramatrix = scgraphicssecpackagemessage.someextrapelvismatrix; //someextrapelvismatrix;
            Vector3 OFFSETPOS = scgraphicssecpackagemessage.offsetpos; // OFFSETPOS;
            Posef handPoseRight = scgraphicssecpackagemessage.handPoseRight; //handPoseRight;
            Posef handPoseLeft = scgraphicssecpackagemessage.handPoseLeft; //handPoseLeft;







            //TO CHANGE HEIGHTMAPS SIZE
            //TO CHANGE HEIGHTMAPS SIZE
            //var heightmapmatrix = sc_maths.Scaling(heightmapscale) * ikarmvoxel[0].worldMatrix_instances_r_hand_grab[0][0][0];// worldMatrix_instances_r_hand_grab[0][0][0];
            //heightmapmatrix.M41 = sccs.scgraphics.scupdate.OFFSETPOS.X;
            //heightmapmatrix.M42 = sccs.scgraphics.scupdate.OFFSETPOS.Y;
            //heightmapmatrix.M43 = sccs.scgraphics.scupdate.OFFSETPOS.Z;






            if (Program.useOculusRift == 1)
            {

                hmd_matrix_current = Matrix.Identity;
                hmdmatrixcurrentforpelvis = Matrix.Identity;
                //HEADSET POSITION
                displayMidpoint = sccsr15forms.directx.D3D.OVR.GetPredictedDisplayTime(sccsr15forms.directx.D3D.sessionPtr, 0);
                trackingState = sccsr15forms.directx.D3D.OVR.GetTrackingState(sccsr15forms.directx.D3D.sessionPtr, displayMidpoint, true);
                latencyMark = false;
                trackState = sccsr15forms.directx.D3D.OVR.GetTrackingState(sccsr15forms.directx.D3D.sessionPtr, 0.0f, latencyMark);
                poseStatefer = trackState.HeadPose;
                hmdPose = poseStatefer.ThePose;
                hmdRot = hmdPose.Orientation;
                _hmdPoser = new Vector3(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);
                _hmdRoter = new Quaternion(hmdPose.Orientation.X, hmdPose.Orientation.Y, hmdPose.Orientation.Z, hmdPose.Orientation.W);
                //SET CAMERA POSITION
                //Camera.SetPosition(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);
                Quaternion quatter = new Quaternion(hmdRot.X, hmdRot.Y, hmdRot.Z, hmdRot.W);
                oculusRiftDir = sc_maths._getDirection(Vector3.ForwardRH, quatter);

                Matrix.RotationQuaternion(ref quatter, out hmd_matrix_current);

            }

            Matrix finalRotationMatrix = originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdrotMatrix;






            for (int xxx = 0; xxx < somechunkpriminstancesikvoxelbodywidthR; xxx++)
            {

                float posX = (xxx);
                float posY = (0);
                float posZ = (0);


                var xxi = xxx;
                var yyi = 0;
                var zzi = 0;

                if (xxi < 0)
                {
                    xxi *= -1;
                    xxi = (somechunkpriminstancesikvoxelbodywidthR) + xxi;
                }
                if (yyi < 0)
                {
                    yyi *= -1;
                    yyi = (somechunkpriminstancesikvoxelbodyheightR) + yyi;
                }
                if (zzi < 0)
                {
                    zzi *= -1;
                    zzi = (somechunkpriminstancesikvoxelbodydepthR) + zzi;
                }

                int somechunkpriminstanceikvoxelbodyindex = xxi;// + (somechunkpriminstancesikvoxelbodywidthL) * (yyi + (somechunkpriminstancesikvoxelbodyheightL) * zzi);

                scgraphicssecpackagemessage.scjittertasks = ikvoxelbody[somechunkpriminstanceikvoxelbodyindex].writeikbodytobuffer(scgraphicssecpackagemessage.scjittertasks, viewMatrix, projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdrotMatrix, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix);



                //
                //for (int yyy = -somechunkpriminstancesikvoxelbodyheightL; yyy <= somechunkpriminstancesikvoxelbodyheightR; yyy++)
                //{
                //    for (int zzz = -somechunkpriminstancesikvoxelbodydepthL; zzz <= somechunkpriminstancesikvoxelbodydepthR; zzz++)
                //    {
                //    }
                //}
            }



            /*

            for (int xxx = 0; xxx < somechunkpriminstancesikvoxelbodywidthR; xxx++)
            {
                for (int yyy = 0; yyy < somechunkpriminstancesikvoxelbodyheightR; yyy++)
                {

                    float posX = (xxx);
                    float posY = (yyy);
                    float posZ = (0);


                    var xxi = xxx;
                    var yyi = yyy;
                    var zzi = 0;

                    if (xxi < 0)
                    {
                        xxi *= -1;
                        xxi = (somechunkpriminstancesikvoxelbodywidthR) + xxi;
                    }
                    if (yyi < 0)
                    {
                        yyi *= -1;
                        yyi = (somechunkpriminstancesikvoxelbodyheightR) + yyi;
                    }
                    if (zzi < 0)
                    {
                        zzi *= -1;
                        zzi = (somechunkpriminstancesikvoxelbodydepthR) + zzi;
                    }

                    //int somechunkpriminstanceikarmvoxelindex = xxi + (somechunkpriminstancesikarmvoxelwidthL) * (yyi + (somechunkpriminstancesikarmvoxelheightL) * zzi);
                    int somechunkpriminstanceikvoxelbodyindex = xxi + (yyi * (somechunkpriminstancesikvoxelbodyheightR));



                    scgraphicssecpackagemessage.scjittertasks = ikvoxelbody[somechunkpriminstanceikvoxelbodyindex].writeikbodytobuffer(scgraphicssecpackagemessage.scjittertasks, viewMatrix, projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdrotMatrix, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix);
                    //scgraphicssecpackagemessage.scjittertasks = ikvoxelbody[somechunkpriminstanceikvoxelbodyindex].setikbodytargetnlimbspositionsNrotations(scgraphicssecpackagemessage.scjittertasks, viewMatrix, projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdrotMatrix, hmd_matrix, ikvoxelbody[somechunkpriminstanceikvoxelbodyindex]._player_pelvis[0][0]._arrayOfInstances[0].current_pos, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix, ikvoxelbody[somechunkpriminstanceikvoxelbodyindex]._player_torso[0][0], lightpos, dirlight, finalRotationMatrix, ikvoxelbody[somechunkpriminstanceikvoxelbodyindex]._player_pelvis[0][0], hmd_matrix_current, extramatrix, hmdmatrixcurrentforpelvis);



                    /*
                    for (int zzz = -somechunkpriminstancesikarmvoxeldepthL; zzz <= somechunkpriminstancesikarmvoxeldepthR; zzz++)
                    {

                    }
                }
            }*/


















            for (int xxx = 0; xxx < somechunkpriminstancesikarmvoxelwidthR; xxx++)
            {
                for (int yyy = 0; yyy < somechunkpriminstancesikarmvoxelheightR; yyy++)
                {

                    float posX = (xxx);
                    float posY = (yyy);
                    float posZ = (0);


                    var xxi = xxx;
                    var yyi = yyy;
                    var zzi = 0;

                    if (xxi < 0)
                    {
                        xxi *= -1;
                        xxi = (somechunkpriminstancesikarmvoxelwidthR) + xxi;
                    }
                    if (yyi < 0)
                    {
                        yyi *= -1;
                        yyi = (somechunkpriminstancesikarmvoxelheightR) + yyi;
                    }
                    if (zzi < 0)
                    {
                        zzi *= -1;
                        zzi = (somechunkpriminstancesikarmvoxeldepthR) + zzi;
                    }

                    //int somechunkpriminstanceikarmvoxelindex = xxi + (somechunkpriminstancesikarmvoxelwidthL) * (yyi + (somechunkpriminstancesikarmvoxelheightL) * zzi);
                    int somechunkpriminstanceikarmvoxelindex = xxi + (yyi * (somechunkpriminstancesikarmvoxelheightR));
                    scgraphicssecpackagemessage.scjittertasks = ikarmvoxel[somechunkpriminstanceikarmvoxelindex].writeikarmtobuffer(scgraphicssecpackagemessage.scjittertasks, viewMatrix, projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdrotMatrix, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix);
                    //scgraphicssecpackagemessage.scjittertasks = ikarmvoxel[somechunkpriminstanceikarmvoxelindex].setiktargetnlimbspositionsNrotations(scgraphicssecpackagemessage.scjittertasks, viewMatrix, projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdrotMatrix, hmd_matrix, ikvoxelbody[0]._player_pelvis[0][0]._arrayOfInstances[0].current_pos, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix, ikvoxelbody[0]._player_torso[0][0], lightpos, dirlight, finalRotationMatrix, ikvoxelbody[0]._player_pelvis[0][0], ikvoxelbody[0], somechunkpriminstanceikarmvoxelindex, rotatingMatrixForPelvis, extramatrix, directionvectoroffsets, Vector3.Zero, Vector3.Zero);



                    for (int xxxx = 0; xxxx < somechunkpriminstancesikfingervoxelwidthR; xxxx++)
                    {
                        for (int yyyy = 0; yyyy < somechunkpriminstancesikfingervoxelheightR; yyyy++)
                        {

                            float posXx = (xxxx);
                            float posYy = (yyyy);
                            float posZz = (0);


                            var xxxi = xxxx;
                            var yyyi = yyyy;
                            var zzzi = 0;

                            if (xxxi < 0)
                            {
                                xxxi *= -1;
                                xxxi = (somechunkpriminstancesikfingervoxelwidthR) + xxxi;
                            }
                            if (yyi < 0)
                            {
                                yyyi *= -1;
                                yyyi = (somechunkpriminstancesikfingervoxelheightR) + yyyi;
                            }
                            if (zzzi < 0)
                            {
                                zzzi *= -1;
                                zzzi = (somechunkpriminstancesikfingervoxeldepthR) + zzzi;
                            }

                            //int somechunkpriminstanceikfingervoxelindex = xxi + (somechunkpriminstancesikfingervoxelwidthL) * (yyi + (somechunkpriminstancesikfingervoxelheightL) * zzi);
                            int somechunkpriminstanceikfingervoxelindex = xxxi + (yyyi * (somechunkpriminstancesikfingervoxelheightR));
                            scgraphicssecpackagemessage.scjittertasks = ikfingervoxel[somechunkpriminstanceikarmvoxelindex][somechunkpriminstanceikfingervoxelindex].writeikfingertobuffer(scgraphicssecpackagemessage.scjittertasks, viewMatrix, projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdrotMatrix, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix);
                            //scgraphicssecpackagemessage.scjittertasks = ikarmvoxel[somechunkpriminstanceikarmvoxelindex].setiktargetnlimbspositionsNrotations(scgraphicssecpackagemessage.scjittertasks, viewMatrix, projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdrotMatrix, hmd_matrix, ikvoxelbody[0]._player_pelvis[0][0]._arrayOfInstances[0].current_pos, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix, ikvoxelbody[0]._player_torso[0][0], lightpos, dirlight, finalRotationMatrix, ikvoxelbody[0]._player_pelvis[0][0], ikvoxelbody[0], somechunkpriminstanceikarmvoxelindex, rotatingMatrixForPelvis, extramatrix, directionvectoroffsets, Vector3.Zero, Vector3.Zero);



                            /*
                            for (int zzz = -somechunkpriminstancesikarmvoxeldepthL; zzz <= somechunkpriminstancesikarmvoxeldepthR; zzz++)
                            {

                            }*/
                        }
                    }
                    /*
                    for (int zzz = -somechunkpriminstancesikarmvoxeldepthL; zzz <= somechunkpriminstancesikarmvoxeldepthR; zzz++)
                    {

                    }*/
                }
            }


            return scgraphicssecpackagemessage.scjittertasks;
        }







        public scmessageobjectjitter[][] workonikshaders(scgraphicssecpackage scgraphicssecpackagemessage)
        {
            //Program.MessageBox((IntPtr)0, "workonshaders", "scmsg", 0);

            var _sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;
            var viewMatrix = scgraphicssecpackagemessage.viewMatrix;
            var projectionMatrix = scgraphicssecpackagemessage.projectionMatrix; //_projectionMatrix;
            var originRot = scgraphicssecpackagemessage.originRot; // originRot;
            var rotatingMatrix = scgraphicssecpackagemessage.rotatingMatrix; //rotatingMatrix;
            var hmdrotMatrix = scgraphicssecpackagemessage.hmdmatrixRot; //hmdmatrixRot;
            var hmd_matrix = scgraphicssecpackagemessage.hmd_matrix; //hmd_matrix;
            var rotatingMatrixForPelvis = scgraphicssecpackagemessage.rotatingMatrixForPelvis; //rotatingMatrixForPelvis;
            var _rightTouchMatrix = scgraphicssecpackagemessage.rightTouchMatrix; //_rightTouchMatrix;
            var _leftTouchMatrix = scgraphicssecpackagemessage.leftTouchMatrix; //_leftTouchMatrix;
            var oriProjectionMatrix = scgraphicssecpackagemessage.oriProjectionMatrix; //oriProjectionMatrix;
            var extramatrix = scgraphicssecpackagemessage.someextrapelvismatrix; //someextrapelvismatrix;
            var OFFSETPOS = scgraphicssecpackagemessage.offsetpos; // OFFSETPOS;
            var handPoseRight = scgraphicssecpackagemessage.handPoseRight; //handPoseRight;
            var handPoseLeft = scgraphicssecpackagemessage.handPoseLeft; //handPoseLeft;

            var _worldMatrix = sccsr15forms.directx.D3D.WorldMatrix;
            var _viewMatrix = viewMatrix;
            var _projectionMatrix = oriProjectionMatrix;
            //_worldMatrix.Transpose();

            /*
            if (Program.useOculusRift == 1)
            {
                //_projectionMatrix = oriProjectionMatrix;
                _viewMatrix.Transpose();
            }
            else
            {
                _worldMatrix.Transpose();
                _viewMatrix.Transpose();
                _projectionMatrix = scgraphicssecpackagemessage.projectionMatrix;
                _projectionMatrix.Transpose();
            }*/



            /*if (Program.useOculusRift == 1)
            {

                Matrix hmd_matrix_current = Matrix.Identity;
                Matrix hmdmatrixcurrentforpelvis = Matrix.Identity;
                //HEADSET POSITION
                displayMidpoint = sccsr15forms.directx.D3D.OVR.GetPredictedDisplayTime(sccsr15forms.directx.D3D.sessionPtr, 0);
                trackingState = sccsr15forms.directx.D3D.OVR.GetTrackingState(sccsr15forms.directx.D3D.sessionPtr, displayMidpoint, true);
                latencyMark = false;
                trackState = sccsr15forms.directx.D3D.OVR.GetTrackingState(sccsr15forms.directx.D3D.sessionPtr, 0.0f, latencyMark);
                poseStatefer = trackState.HeadPose;
                hmdPose = poseStatefer.ThePose;
                hmdRot = hmdPose.Orientation;
                _hmdPoser = new Vector3(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);
                _hmdRoter = new Quaternion(hmdPose.Orientation.X, hmdPose.Orientation.Y, hmdPose.Orientation.Z, hmdPose.Orientation.W);
                //SET CAMERA POSITION
                //Camera.SetPosition(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);
                Quaternion quatter = new Quaternion(hmdRot.X, hmdRot.Y, hmdRot.Z, hmdRot.W);
                oculusRiftDir = sc_maths._getDirection(Vector3.ForwardRH, quatter);

                Matrix.RotationQuaternion(ref quatter, out hmd_matrix_current);

            }*/

            Matrix finalRotationMatrix = originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdrotMatrix;



            Vector3 lightpos = new Vector3(0, 5, 0);
            Vector3 dirlight = new Vector3(0, -1, 0);


            if (Program.createikrig == 1)
            {


                /*
                for (int xxx = 0; xxx < somechunkpriminstancesikvoxelbodywidthR; xxx++)
                {
                    for (int yyy = 0; yyy < somechunkpriminstancesikvoxelbodyheightR; yyy++)
                    {

                        float posX = (xxx);
                        float posY = (yyy);
                        float posZ = (0);


                        var xxi = xxx;
                        var yyi = yyy;
                        var zzi = 0;

                        if (xxi < 0)
                        {
                            xxi *= -1;
                            xxi = (somechunkpriminstancesikvoxelbodywidthR) + xxi;
                        }
                        if (yyi < 0)
                        {
                            yyi *= -1;
                            yyi = (somechunkpriminstancesikvoxelbodyheightR) + yyi;
                        }
                        if (zzi < 0)
                        {
                            zzi *= -1;
                            zzi = (somechunkpriminstancesikvoxelbodydepthR) + zzi;
                        }

                        //int somechunkpriminstanceikarmvoxelindex = xxi + (somechunkpriminstancesikarmvoxelwidthL) * (yyi + (somechunkpriminstancesikarmvoxelheightL) * zzi);
                        int somechunkpriminstanceikvoxelbodyindex = xxi + (yyi * (somechunkpriminstancesikvoxelbodyheightR));



                        scgraphicssecpackagemessage.scjittertasks = ikvoxelbody[somechunkpriminstanceikvoxelbodyindex].setikbodytargetnlimbspositionsNrotations(scgraphicssecpackagemessage.scjittertasks, viewMatrix, projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdrotMatrix, hmd_matrix, ikvoxelbody[somechunkpriminstanceikvoxelbodyindex]._player_pelvis[0][0]._arrayOfInstances[0].current_pos, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix, ikvoxelbody[somechunkpriminstanceikvoxelbodyindex]._player_torso[0][0], lightpos, dirlight, finalRotationMatrix, ikvoxelbody[somechunkpriminstanceikvoxelbodyindex]._player_pelvis[0][0], hmd_matrix_current, extramatrix, hmdmatrixcurrentforpelvis);
                        scgraphicssecpackagemessage.scjittertasks = ikvoxelbody[somechunkpriminstanceikvoxelbodyindex].ikbodyrender(scgraphicssecpackagemessage.scjittertasks, viewMatrix, projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdrotMatrix, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix);




                        /*
                        for (int zzz = -somechunkpriminstancesikarmvoxeldepthL; zzz <= somechunkpriminstancesikarmvoxeldepthR; zzz++)
                        {

                        }
                    }
                }*/





                for (int xxx = 0; xxx < somechunkpriminstancesikvoxelbodywidthR; xxx++)
                {
                    float posX = (xxx);
                    float posY = (0);
                    float posZ = (0);

                    var xxi = xxx;
                    var yyi = 0;
                    var zzi = 0;

                    if (xxi < 0)
                    {
                        xxi *= -1;
                        xxi = (somechunkpriminstancesikvoxelbodywidthR) + xxi;
                    }
                    if (yyi < 0)
                    {
                        yyi *= -1;
                        yyi = (somechunkpriminstancesikvoxelbodyheightR) + yyi;
                    }
                    if (zzi < 0)
                    {
                        zzi *= -1;
                        zzi = (somechunkpriminstancesikvoxelbodydepthR) + zzi;
                    }

                    int somechunkpriminstanceikvoxelbodyindex = xxi;// + (somechunkpriminstancesikvoxelbodywidthL ) * (yyi + (somechunkpriminstancesikvoxelbodyheightL) * zzi);


                    scgraphicssecpackagemessage.scjittertasks = ikvoxelbody[somechunkpriminstanceikvoxelbodyindex].setikbodytargetnlimbspositionsNrotations(scgraphicssecpackagemessage.scjittertasks, viewMatrix, projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdrotMatrix, hmd_matrix, ikvoxelbody[somechunkpriminstanceikvoxelbodyindex]._player_pelvis[0][0]._arrayOfInstances[0].current_pos, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix, ikvoxelbody[somechunkpriminstanceikvoxelbodyindex]._player_torso[0][0], lightpos, dirlight, finalRotationMatrix, ikvoxelbody[somechunkpriminstanceikvoxelbodyindex]._player_pelvis[0][0], hmd_matrix_current, extramatrix, hmdmatrixcurrentforpelvis);
                    scgraphicssecpackagemessage.scjittertasks = ikvoxelbody[somechunkpriminstanceikvoxelbodyindex].ikbodyrender(scgraphicssecpackagemessage.scjittertasks, viewMatrix, projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdrotMatrix, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix);





                    //for (int yyy = -somechunkpriminstancesikvoxelbodyheightL; yyy <= somechunkpriminstancesikvoxelbodyheightR; yyy++)
                    //{
                    //    for (int zzz = -somechunkpriminstancesikvoxelbodydepthL; zzz <= somechunkpriminstancesikvoxelbodydepthR; zzz++)
                    //    {
                    //
                    //    }
                    //}
                }





                for (int xxx = 0; xxx < somechunkpriminstancesikarmvoxelwidthR; xxx++)
                {
                    for (int yyy = 0; yyy < somechunkpriminstancesikarmvoxelheightR; yyy++)
                    {

                        float posX = (xxx);
                        float posY = (yyy);
                        float posZ = (0);

                        var xxi = xxx;
                        var yyi = yyy;
                        var zzi = 0;

                        if (xxi < 0)
                        {
                            xxi *= -1;
                            xxi = (somechunkpriminstancesikarmvoxelwidthR) + xxi;
                        }
                        if (yyi < 0)
                        {
                            yyi *= -1;
                            yyi = (somechunkpriminstancesikarmvoxelheightR) + yyi;
                        }
                        if (zzi < 0)
                        {
                            zzi *= -1;
                            zzi = (somechunkpriminstancesikarmvoxeldepthR) + zzi;
                        }

                        int somechunkpriminstanceikarmvoxelindex = xxi + (yyi * (somechunkpriminstancesikarmvoxelheightR));


                        var sometest = ikarmvoxel[somechunkpriminstanceikarmvoxelindex];


                        scgraphicssecpackagemessage.scjittertasks = ikarmvoxel[somechunkpriminstanceikarmvoxelindex].setiktargetnlimbspositionsNrotations(scgraphicssecpackagemessage.scjittertasks, viewMatrix, projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdrotMatrix, hmd_matrix, ikvoxelbody[0]._player_pelvis[0][0]._arrayOfInstances[0].current_pos, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix, ikvoxelbody[0]._player_torso[0][0], lightpos, dirlight, finalRotationMatrix, ikvoxelbody[0]._player_pelvis[0][0], ikvoxelbody[0], somechunkpriminstanceikarmvoxelindex, rotatingMatrixForPelvis, extramatrix, directionvectoroffsets, Vector3.Zero, Vector3.Zero, 0);
                        scgraphicssecpackagemessage.scjittertasks = ikarmvoxel[somechunkpriminstanceikarmvoxelindex].ikarmrender(scgraphicssecpackagemessage.scjittertasks, viewMatrix, projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdrotMatrix, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix);



                        for (int xxxx = 0; xxxx < somechunkpriminstancesikfingervoxelwidthR; xxxx++)
                        {
                            for (int yyyy = 0; yyyy < somechunkpriminstancesikfingervoxelheightR; yyyy++)
                            {

                                float posXx = (xxxx);
                                float posYy = (yyyy);
                                float posZz = (0);


                                var xxxi = xxxx;
                                var yyyi = yyyy;
                                var zzzi = 0;

                                if (xxxi < 0)
                                {
                                    xxxi *= -1;
                                    xxxi = (somechunkpriminstancesikfingervoxelwidthR) + xxxi;
                                }
                                if (yyi < 0)
                                {
                                    yyyi *= -1;
                                    yyyi = (somechunkpriminstancesikfingervoxelheightR) + yyyi;
                                }
                                if (zzzi < 0)
                                {
                                    zzzi *= -1;
                                    zzzi = (somechunkpriminstancesikfingervoxeldepthR) + zzzi;
                                }

                                //int somechunkpriminstanceikfingervoxelindex = xxi + (somechunkpriminstancesikfingervoxelwidthL) * (yyi + (somechunkpriminstancesikfingervoxelheightL) * zzi);
                                int somechunkpriminstanceikfingervoxelindex = xxxi + (yyyi * (somechunkpriminstancesikfingervoxelheightR));


                                scgraphicssecpackagemessage.scjittertasks = ikfingervoxel[somechunkpriminstanceikarmvoxelindex][somechunkpriminstanceikfingervoxelindex].setiktargetnfingerspositionsNrotations(scgraphicssecpackagemessage.scjittertasks, viewMatrix, projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdrotMatrix, hmd_matrix, ikarmvoxel[0]._player_rght_hnd[0][0]._arrayOfInstances[0].current_pos, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix, ikarmvoxel[0]._player_rght_hnd[0][0], lightpos, dirlight, finalRotationMatrix, ikarmvoxel[0]._player_rght_hnd[0][0], ikvoxelbody[0], somechunkpriminstanceikarmvoxelindex, rotatingMatrixForPelvis, extramatrix, directionvectoroffsets, Vector3.Zero, Vector3.Zero, ikarmvoxel[somechunkpriminstanceikarmvoxelindex], somechunkpriminstanceikfingervoxelindex);
                                scgraphicssecpackagemessage.scjittertasks = ikfingervoxel[somechunkpriminstanceikarmvoxelindex][somechunkpriminstanceikfingervoxelindex].ikfingerrender(scgraphicssecpackagemessage.scjittertasks, viewMatrix, projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdrotMatrix, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix);



                                /*
                                for (int zzz = -somechunkpriminstancesikarmvoxeldepthL; zzz <= somechunkpriminstancesikarmvoxeldepthR; zzz++)
                                {

                                }*/
                            }
                        }


                    }
                }
            }

            return scgraphicssecpackagemessage.scjittertasks;
        }





        //scmessageobjectjitter[][] 
        public void workonLevelGenChangeBytes() //scgraphicssecpackage scgraphicssecpackagemessage
        {
            int fingerindex = 0;
            int armindex = 0;

            for (int insti = 0; insti < 1; insti++) // ikarmvoxel[3]._player_r_hand_grab[0][0]._arrayOfInstances.Length //ikarmvoxel[3]._player_r_hand_grab[0][0]._arrayOfInstances.Length
            {
                Vector3 temppickaxetiplocation = Vector3.Zero;
                if (insti == 0)
                {
                    fingerindex = 3;
                    armindex = 0;
                    temppickaxetiplocation = new Vector3(ikfingervoxel[armindex][fingerindex]._player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, ikfingervoxel[armindex][fingerindex]._player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, ikfingervoxel[armindex][fingerindex]._player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);

                }
                else if (insti == 1)
                {
                    fingerindex = 3;
                    armindex = 3;
                    temppickaxetiplocation = new Vector3(ikfingervoxel[armindex][fingerindex]._player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M41, ikfingervoxel[armindex][fingerindex]._player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M42, ikfingervoxel[armindex][fingerindex]._player_rght_hnd[0][0]._arrayOfInstances[0].current_pos.M43);

                }
                //Vector3 pivotpositionpickaxe = new Vector3(ikarmvoxel[3]._player_r_hand_grab[0][0]._arrayOfInstances[insti].current_pos.M41, ikarmvoxel[3]._player_r_hand_grab[0][0]._arrayOfInstances[insti].current_pos.M42, ikarmvoxel[3]._player_r_hand_grab[0][0]._arrayOfInstances[insti].current_pos.M43);
                //Vector3 temppickaxetiplocation = new Vector3(ikarmvoxel[3]._player_r_hand_grab[0][0]._chunk.listofpickaxetipbytes[0].X, ikarmvoxel[3]._player_r_hand_grab[0][0]._chunk.listofpickaxetipbytes[0].Y, ikarmvoxel[3]._player_r_hand_grab[0][0]._chunk.listofpickaxetipbytes[0].Z);
                //var MOVINGPOINTER = new Vector3(ikvoxelbody[0]._player_torso[0][0]._arrayOfInstances[insti]._ORIGINPOSITION.M41, ikvoxelbody[0]._player_torso[0][0]._arrayOfInstances[insti]._ORIGINPOSITION.M42, ikvoxelbody[0]._player_torso[0][0]._arrayOfInstances[insti]._ORIGINPOSITION.M43);

                //Vector3 temppickaxetiplocation = new Vector3(worldMatrix_instances_screen_assets[0][0][19].M41, worldMatrix_instances_screen_assets[0][0][19].M42, worldMatrix_instances_screen_assets[0][0][19].M43);
                var MOVINGPOINTER = temppickaxetiplocation;// new Vector3(ikvoxelbody[0]._player_torso[0][0]._arrayOfInstances[insti]._ORIGINPOSITION.M41, ikvoxelbody[0]._player_torso[0][0]._arrayOfInstances[insti]._ORIGINPOSITION.M42, ikvoxelbody[0]._player_torso[0][0]._arrayOfInstances[insti]._ORIGINPOSITION.M43);

                //worldMatrix_instances_screen_assets[0][0][19].M41

                var rotposmatrix = ikfingervoxel[armindex][fingerindex]._player_rght_hnd[0][0]._arrayOfInstances[0].current_pos;// Matrix.Identity;// somevoxelvirtualdesktop[0].currentWorldMatrix;//  worldMatrix_instances_screen_assets[0][0][19];// ikarmvoxel[3]._player_r_hand_grab[0][0]._arrayOfInstances[insti].current_pos;// _player_rght_shldr[0][0]._ORIGINPOSITION;

                Quaternion somequat;
                Quaternion.RotationMatrix(ref rotposmatrix, out somequat);

                var direction_feet_forward_ori = sc_maths._getDirection(Vector3.ForwardRH, somequat);
                var direction_feet_right_ori = sc_maths._getDirection(Vector3.Right, somequat);
                var direction_feet_up_ori = sc_maths._getDirection(Vector3.Up, somequat);

                var diffNormPosX = (MOVINGPOINTER.X) - temppickaxetiplocation.X;// ikarmvoxel[3]._player_r_hand_grab[0][0]._arrayOfInstances[insti].current_pos.M41;
                var diffNormPosY = (MOVINGPOINTER.Y) - temppickaxetiplocation.Y;//ikarmvoxel[3]._player_r_hand_grab[0][0]._arrayOfInstances[insti].current_pos.M42;
                var diffNormPosZ = (MOVINGPOINTER.Z) - temppickaxetiplocation.Z;//ikarmvoxel[3]._player_r_hand_grab[0][0]._arrayOfInstances[insti].current_pos.M43;

                var pickaxetippoint = MOVINGPOINTER;

                //var tempPoint = pickaxetippoint + (direction_feet_right_ori * (diffNormPosX));
                //tempPoint = tempPoint + (direction_feet_up_ori * (diffNormPosY));
                //tempPoint = tempPoint + (direction_feet_forward_ori * (diffNormPosZ));

                //var MOVINGPOINTER1 = tempPoint + new Vector3(ikarmvoxel[3]._player_r_hand_grab[0][0]._arrayOfInstances[insti].current_pos.M41, ikarmvoxel[3]._player_r_hand_grab[0][0]._arrayOfInstances[insti].current_pos.M42, ikarmvoxel[3]._player_r_hand_grab[0][0]._arrayOfInstances[insti].current_pos.M43);
                //var MOVINGPOINTER1 = new Vector3(ikarmvoxel[3]._player_r_hand_grab[0][0]._arrayOfInstances[insti].current_pos.M41, ikarmvoxel[3]._player_r_hand_grab[0][0]._arrayOfInstances[insti].current_pos.M42, ikarmvoxel[3]._player_r_hand_grab[0][0]._arrayOfInstances[insti].current_pos.M43);
                var MOVINGPOINTER1 = temppickaxetiplocation;// new Vector3(ikarmvoxel[3]._player_r_hand_grab[0][0]._arrayOfInstances[insti].current_pos.M41, ikarmvoxel[3]._player_r_hand_grab[0][0]._arrayOfInstances[insti].current_pos.M42, ikarmvoxel[3]._player_r_hand_grab[0][0]._arrayOfInstances[insti].current_pos.M43);

                //float somezvalue = ikarmvoxel[3]._player_r_hand_grab[0][0]._total_torso_height;

                //pickaxetippoint = MOVINGPOINTER1 + (direction_feet_up_ori * (-temppickaxetiplocation.Y * levelgenplanesize));
                //pickaxetippoint = pickaxetippoint + (direction_feet_forward_ori * (-temppickaxetiplocation.Z * levelgenplanesize));

                //SET THE BYTE LOCATOR OBJECT TO THE LOCATION OF THE BYTE ON THE PICKAXE TIP IN ORDER TO HAVE A VISUAL ON WHERE THE ROUNDED BYTE LOCATOR IS
                //SET THE BYTE LOCATOR OBJECT TO THE LOCATION OF THE BYTE ON THE PICKAXE TIP IN ORDER TO HAVE A VISUAL ON WHERE THE ROUNDED BYTE LOCATOR IS
                matrixchangelevelgenbytes[0][0][0].M41 = pickaxetippoint.X;
                matrixchangelevelgenbytes[0][0][0].M42 = pickaxetippoint.Y;
                matrixchangelevelgenbytes[0][0][0].M43 = pickaxetippoint.Z;


                /*
                matrixchangelevelgenbytes[0][0][1].M41 = rotposmatrix.M41;
                matrixchangelevelgenbytes[0][0][1].M42 = rotposmatrix.M42;
                matrixchangelevelgenbytes[0][0][1].M43 = rotposmatrix.M43;

                matrixchangelevelgenbytes[0][0][2].M41 = rotposmatrix.M41;
                matrixchangelevelgenbytes[0][0][2].M42 = rotposmatrix.M42;
                matrixchangelevelgenbytes[0][0][2].M43 = rotposmatrix.M43;

                matrixchangelevelgenbytes[0][0][3].M41 = rotposmatrix.M41;
                matrixchangelevelgenbytes[0][0][3].M42 = rotposmatrix.M42;
                matrixchangelevelgenbytes[0][0][3].M43 = rotposmatrix.M43;*/


                //Console.WriteLine(pickaxetippoint);

                //Console.WriteLine("test");


                //int somefx = (int)Math.Abs(Math.Round(((pickaxetippoint.X) - somevoxelvirtualdesktop[0].worldmatofobj.M41) / planeSize)); // 35 + 1 = 36
                //int somefy = (int)Math.Abs(Math.Round(((pickaxetippoint.Y) - somevoxelvirtualdesktop[0].worldmatofobj.M42) / planeSize)); // 
                //int somefz = (int)Math.Abs(Math.Round(((pickaxetippoint.Z) - somevoxelvirtualdesktop[0].worldmatofobj.M43) / planeSize)); //


                float somevaldiv = 1.0f / ((bundlechunkplanesize));


                //Console.WriteLine(somevaldiv);
                //0.01f == 4 face == 1 chunk => 0.0025f for 1 face

                //float someposfootx = (float)((Math.Floor(pickaxetippoint.X / somevaldiv) * somevaldiv)) / (4);
                //float someposfooty = (float)((Math.Floor(pickaxetippoint.Y / somevaldiv) * somevaldiv)) / (4);
                //float someposfootz = (float)((Math.Floor(pickaxetippoint.Z / somevaldiv) * somevaldiv)) / (4);


                int someposfootx = (int)(((float)Math.Floor(pickaxetippoint.X * somevaldiv) / somevaldiv) / (bundlechunkplanesize));
                int someposfooty = (int)(((float)Math.Floor(pickaxetippoint.Y * somevaldiv) / somevaldiv) / (bundlechunkplanesize));
                int someposfootz = (int)(((float)Math.Floor(pickaxetippoint.Z * somevaldiv) / somevaldiv) / (bundlechunkplanesize));

                //Console.WriteLine("/ix:" + ((someposfootx / 4) / 2) + "/iy:" + ((someposfooty / 4) / 2) + "/iz:" + ((someposfootz / 4) / 2));

                //int someposfootx = (int)(((float)Math.Floor(pickaxetippoint.X * somevaldiv) / somevaldiv) / (tutorialcubeaschunkinst.currenttutorialcubeaschunkinst.somelevelgenprimglobals.planeSize));
                //int someposfooty = (int)(((float)Math.Floor(pickaxetippoint.Y * somevaldiv) / somevaldiv) / (tutorialcubeaschunkinst.currenttutorialcubeaschunkinst.somelevelgenprimglobals.planeSize));
                //int someposfootz = (int)(((float)Math.Floor(pickaxetippoint.Z * somevaldiv) / somevaldiv) / (tutorialcubeaschunkinst.currenttutorialcubeaschunkinst.somelevelgenprimglobals.planeSize));
                //someposfooty -= 1;

                int totalTimesx = 0;
                int totalTimesy = 0;
                int totalTimesz = 0;

                int someremainsx = 0;
                int someremainsy = 0;
                int someremainsz = 0;

                if (pickaxetippoint.X >= 0)
                {
                    someremainsx = (int)Math.Floor((someposfootx / chunkwidthdivfloat)) * chunkwidthdiv;
                    totalTimesx = (int)(someposfootx - someremainsx);
                }
                else
                {
                    someremainsx = (int)Math.Floor((someposfootx / chunkwidthdivfloat)) * chunkwidthdiv;
                    totalTimesx = -chunkwidthdiv + (int)(someremainsx - someposfootx) + chunkwidthdiv;
                    totalTimesx *= -1;
                }

                if (pickaxetippoint.Y >= 0)
                {
                    someremainsy = (int)Math.Floor((someposfooty / chunkheightdivfloat)) * chunkheightdiv;
                    totalTimesy = (int)(someposfooty - someremainsy);
                }
                else
                {
                    someremainsy = (int)Math.Floor((someposfooty / chunkheightdivfloat)) * chunkheightdiv;
                    totalTimesy = -chunkheightdiv + (int)(someremainsy - someposfooty) + chunkheightdiv;
                    totalTimesy *= -1;
                }

                if (pickaxetippoint.Z >= 0)
                {
                    someremainsz = (int)Math.Floor((someposfootz / chunkdepthdivfloat)) * chunkdepthdiv;
                    totalTimesz = (int)(someposfootz - someremainsz);
                }
                else
                {
                    someremainsz = (int)Math.Floor((someposfootz / chunkdepthdivfloat)) * chunkdepthdiv;
                    //Console.WriteLine(someremainsz);

                    totalTimesz = -chunkdepthdiv + (int)(someremainsz - someposfootz) + chunkdepthdiv;
                    totalTimesz *= -1;
                }

                int totaltimesforonepartschunksx = (someremainsx / chunkwidthdiv) ;
                int totaltimesforonepartschunksy = (someremainsy / chunkheightdiv) ;
                int totaltimesforonepartschunksz = (someremainsz / chunkdepthdiv) ;

                //Console.WriteLine("x:" + totaltimesforonepartschunksx + "/y:" + totaltimesforonepartschunksy + ":z/" + totaltimesforonepartschunksz);

                /*
                int totaltimesforonepartschunksx = (someposfootx / 4) / 2;// (someremainsx / 8);
                int totaltimesforonepartschunksy = (someposfooty / 4) / 2;//(someremainsy / 8);
                int totaltimesforonepartschunksz = (someposfootz / 4) / 2;//(someremainsz / 8);*/

                //Console.WriteLine("x:" + someremainsx + "/y:" + someremainsy + ":z/" + someremainsz);





     
                int thechunkinthebundlex = (someremainsx / chunkwidthdiv);
                int thechunkinthebundley = (someremainsy / chunkheightdiv);
                int thechunkinthebundlez = (someremainsz / chunkdepthdiv);


                int chunkposx = thechunkinthebundlex ;
                int chunkposy = thechunkinthebundley ;
                int chunkposz = thechunkinthebundlez;













                somevaldiv = 1.0f / ((chunkplanesize));
                //Console.WriteLine(somevaldiv);
                //bundlechunkplanesize == 4 face == 1 chunk => 0.0025f for 1 face

                //float someposfootx = (float)((Math.Floor(pickaxetippoint.X / somevaldiv) * somevaldiv)) / (4);
                //float someposfooty = (float)((Math.Floor(pickaxetippoint.Y / somevaldiv) * somevaldiv)) / (4);
                //float someposfootz = (float)((Math.Floor(pickaxetippoint.Z / somevaldiv) * somevaldiv)) / (4);

                someposfootx = (int)(((float)Math.Floor(pickaxetippoint.X * somevaldiv) / somevaldiv) / (chunkplanesize));
                someposfooty = (int)(((float)Math.Floor(pickaxetippoint.Y * somevaldiv) / somevaldiv) / (chunkplanesize));
                someposfootz = (int)(((float)Math.Floor(pickaxetippoint.Z * somevaldiv) / somevaldiv) / (chunkplanesize));

                int maptotaltimesx = 0;
                int maptotaltimesy = 0;
                int maptotaltimesz = 0;

                if (pickaxetippoint.X >= 0)
                {
                    someremainsx = (int)Math.Floor((someposfootx / chunkwidthsimfloat)) * chunkwidthsim;
                    maptotaltimesx = (int)(someposfootx - someremainsx);
                }
                else
                {
                    someremainsx = (int)Math.Floor((someposfootx / chunkwidthsimfloat)) * chunkwidthsim;
                    maptotaltimesx = -chunkwidthsim + (int)(someremainsx - someposfootx) + chunkwidthsim;
                    maptotaltimesx *= -1;
                }

                if (pickaxetippoint.Y >= 0)
                {
                    someremainsy = (int)Math.Floor((someposfooty / chunkheightsimfloat)) * chunkheightsim;
                    maptotaltimesy = (int)(someposfooty - someremainsy);
                }
                else
                {
                    someremainsy = (int)Math.Floor((someposfooty / chunkheightsimfloat)) * chunkheightsim;
                    maptotaltimesy = -chunkheightsim + (int)(someremainsy - someposfooty) + chunkheightsim;
                    maptotaltimesy *= -1;
                }

                if (pickaxetippoint.Z >= 0)
                {
                    someremainsz = (int)Math.Floor((someposfootz / chunkdepthsimfloat)) * chunkdepthsim;
                    maptotaltimesz = (int)(someposfootz - someremainsz);
                }
                else
                {
                    someremainsz = (int)Math.Floor((someposfootz / chunkdepthsimfloat)) * chunkdepthsim;
                    //Console.WriteLine(someremainsz);

                    maptotaltimesz = -chunkdepthsim + (int)(someremainsz - someposfootz) + chunkdepthsim;
                    maptotaltimesz *= -1;
                }

                int indexx = maptotaltimesx;
                int indexy = maptotaltimesy;
                int indexz = maptotaltimesz;

                int sometotaltimesx = someremainsx / chunkwidthsim;// totalTimesx;
                int sometotaltimesy = someremainsy / chunkheightsim;//totalTimesy;
                int sometotaltimesz = someremainsz / chunkdepthsim;//totalTimesz;

              



                int someindexmap = indexx + chunkwidthsim * ((indexy) + chunkheightsim * indexz);

                //Console.WriteLine("/ix:" + indexx + "/iy:" + indexy + "/iz:" + indexz);
                //Console.WriteLine("/0x:" + someposfootx + "/0y:" + someposfooty + "/0z:" + someposfootz);

                //Console.WriteLine("index:" + someindexmap + "/0x:" + someposfootx + "/0y:" + someposfooty + "/0z:" + someposfootz + "/ix:" + indexx + "/iy:" + indexy + "/iz:" + indexz + " " + sometotaltimesx + " " + sometotaltimesy + " " + sometotaltimesz + "/ttx:" + totalTimesx + "/tty:" + totalTimesy + "/ttz:" + totalTimesz + "/index:" + someindexmap);

                int somearrayindex = 0;


                sometotaltimesx = someremainsx / chunkwidthsim;// totalTimesx;
                sometotaltimesy = someremainsy / chunkheightsim;//totalTimesy;
                sometotaltimesz = someremainsz / chunkdepthsim;//totalTimesz;

                float sometotaltx = sometotaltimesx;
                float sometotalty = sometotaltimesy;
                float sometotaltz = sometotaltimesz;


                sometotaltx /= incrementsdivx;
                sometotaltx = (float)Math.Floor(sometotaltx) * incrementsdivx;
                sometotaltimesx = (int)sometotaltx / incrementsdivx;


                sometotalty /= incrementsdivy;
                sometotalty = (float)Math.Floor(sometotalty) * incrementsdivy;
                sometotaltimesy = (int)sometotalty / incrementsdivy;


                sometotaltz /= incrementsdivz;
                sometotaltz = (float)Math.Floor(sometotaltz) * incrementsdivz;
                sometotaltimesz = (int)sometotaltz / incrementsdivz;

                if (sometotaltimesx < 0)
                {
                    sometotaltimesx *= -1;
                    sometotaltimesx = sometotaltimesx + ((divx / 2) - 1);
                }

                if (sometotaltimesy < 0)
                {
                    sometotaltimesy *= -1;
                    sometotaltimesy = sometotaltimesy + ((divy / 2) - 1);
                }
                if (sometotaltimesz < 0)
                {
                    sometotaltimesz *= -1;
                    sometotaltimesz = sometotaltimesz + ((divz / 2) - 1);
                }

                //Console.WriteLine("x:" + sometotaltimesx + "/y:" + sometotaltimesy + ":z/" + sometotaltimesz);

                int someindexmain = sometotaltimesx + divx * ((sometotaltimesy) + divy * sometotaltimesz);

                //Console.WriteLine("mainindex:" + someindexmain);

                if (someindexmain < divx * divy * divz)
                {
                    int arrayindexmap;

                    int someposx = totaltimesforonepartschunksx;
                    int someposy = totaltimesforonepartschunksy;
                    int someposz = totaltimesforonepartschunksz;

                    /*if (totaltimesforonepartschunksx < 0)
                    {
                        totaltimesforonepartschunksx *= -1;
                        totaltimesforonepartschunksx = totaltimesforonepartschunksx + ((incrementsdivx));
                    }

                    if (totaltimesforonepartschunksy < 0)
                    {
                        totaltimesforonepartschunksy *= -1;
                        totaltimesforonepartschunksy = totaltimesforonepartschunksy + ((incrementsdivy));
                    }
                    if (totaltimesforonepartschunksz < 0)
                    {
                        totaltimesforonepartschunksz *= -1;
                        totaltimesforonepartschunksz = totaltimesforonepartschunksz + ((incrementsdivz));
                    }
                    */

                    //int someindex 

                    //int thechunkinthebundleindex = totaltimesforonepartschunksx + sccslevelgen.somewidth * ((totaltimesforonepartschunksy) + sccslevelgen.someheight * totaltimesforonepartschunksz)


                    int someindexchunkpart1 = totaltimesforonepartschunksx + sccslevelgen.somewidth * ((totaltimesforonepartschunksy) + sccslevelgen.someheight * totaltimesforonepartschunksz);

                    if (someindexchunkpart1 < sccslevelgen.somewidth* sccslevelgen.someheight * sccslevelgen.somedepth)
                    {


                        somechunk = sccslevelgen.getchunkinlevelgenmap(totaltimesforonepartschunksx, totaltimesforonepartschunksy, totaltimesforonepartschunksz, 1, out arrayindexmap );

                        if (somechunk != null)
                        {

                            

                            if (somechunk.map != null)
                            {
                                //Console.WriteLine("x:" + totaltimesforonepartschunksx + "/y:" + totaltimesforonepartschunksy + ":z/" + totaltimesforonepartschunksz + "/xpos:" + sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[0] + "/ypos:" + sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[1] + "/zpos:" + sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[2]);
                                //Console.WriteLine("!found chunk");
                                if (sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[0] == totaltimesforonepartschunksx && sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[1] == totaltimesforonepartschunksy && sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[2] == totaltimesforonepartschunksz)
                                {
                                    //Console.WriteLine("found chunk");
                                    if (someindexmap >= 0 && someindexmap < chunkwidthsim * chunkheightsim * chunkdepthsim)
                                    {
                                        if (somechunk.map[someindexmap] == 1)
                                        {
                                            //Console.WriteLine(somechunk.map[someindexmap]);
                                            somechunk.map[someindexmap] = 0;

                                            int realindexofchunkinbundle = arrayindexmap;

                                            for (int xi = 0; xi < thechunkdivx; xi++) 
                                            {
                                                for (int yi = 0; yi < thechunkdivy; yi++)
                                                {
                                                    for (int zi = 0; zi < thechunkdivz; zi++)
                                                    {

                                                        int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[0][realindexofchunkinbundle].vertexcount;// somechunk.vertexcountermemory;

                                                        if (xi == 0 && yi == 0 && zi == 0)
                                                        {
                                                            sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(0, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                        }
                                                        else
                                                        {
                                                            sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(0, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                        }

                                                        somechunk.setvertex(0);

                                                        mainchunkdivtop[someindexmain].removefromarray(whatsthecurrentvertexcount, 0, realindexofchunkinbundle);

                                                        if (somechunk.arrayofvertstop.Length > 0)
                                                        {
                                                            int vertexlength = mainchunkdivtop[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 0, arrayindexmap);
                                                            mainchunkdivtop[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunk.arrayofvertstop.Length, 0);

                                                            somechunk.cleararrays();
                                                        }
                                                        sccslevelgen.arraychunkdatalod0[0][realindexofchunkinbundle].vertexcount = somechunk.arrayofvertstop.Length;


                                                        realindexofchunkinbundle++;
                                                    }
                                                }
                                            }

                                            
                                            realindexofchunkinbundle = arrayindexmap;
                                            for (int xi = 0; xi < thechunkdivx; xi++) 
                                            {
                                                for (int yi = 0; yi < thechunkdivy; yi++)
                                                {
                                                    for (int zi = 0; zi < thechunkdivz; zi++)
                                                    {

                                                        int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[1][realindexofchunkinbundle].vertexcount;// somechunk.vertexcountermemory;

                                                        if (xi == 0 && yi == 0 && zi == 0)
                                                        {
                                                            sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(1, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                        }
                                                        else
                                                        {
                                                            sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(1, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                        }

                                                        somechunk.setvertex(1);

                                                        mainchunkdivleft[someindexmain].removefromarray(whatsthecurrentvertexcount, 1, realindexofchunkinbundle);

                                                        if (somechunk.arrayofvertstop.Length > 0)
                                                        {
                                                            int vertexlength = mainchunkdivleft[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 1, arrayindexmap);
                                                            mainchunkdivleft[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunk.arrayofvertstop.Length, 1);

                                                            somechunk.cleararrays();
                                                        }
                                                        sccslevelgen.arraychunkdatalod0[1][realindexofchunkinbundle].vertexcount = somechunk.arrayofvertstop.Length;


                                                        realindexofchunkinbundle++;
                                                    }
                                                }
                                            }


                                            realindexofchunkinbundle = arrayindexmap;
                                            for (int xi = 0; xi < thechunkdivx; xi++) 
                                            {
                                                for (int yi = 0; yi < thechunkdivy; yi++)
                                                {
                                                    for (int zi = 0; zi < thechunkdivz; zi++)
                                                    {

                                                        int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[2][realindexofchunkinbundle].vertexcount;// somechunk.vertexcountermemory;

                                                        if (xi == 0 && yi == 0 && zi == 0)
                                                        {
                                                            sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(2, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                        }
                                                        else
                                                        {
                                                            sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(2, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                        }

                                                        somechunk.setvertex(2);

                                                        mainchunkdivright[someindexmain].removefromarray(whatsthecurrentvertexcount, 2, realindexofchunkinbundle);

                                                        if (somechunk.arrayofvertstop.Length > 0)
                                                        {
                                                            int vertexlength = mainchunkdivright[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 2, arrayindexmap);
                                                            mainchunkdivright[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunk.arrayofvertstop.Length, 2);

                                                            somechunk.cleararrays();
                                                        }
                                                        sccslevelgen.arraychunkdatalod0[2][realindexofchunkinbundle].vertexcount = somechunk.arrayofvertstop.Length;



                                                        realindexofchunkinbundle++;
                                                    }
                                                }
                                            }



                                            realindexofchunkinbundle = arrayindexmap;
                                            for (int xi = 0; xi < thechunkdivx; xi++) 
                                            {
                                                for (int yi = 0; yi < thechunkdivy; yi++)
                                                {
                                                    for (int zi = 0; zi < thechunkdivz; zi++)
                                                    {

                                                        int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[3][realindexofchunkinbundle].vertexcount;// somechunk.vertexcountermemory;

                                                        if (xi == 0 && yi == 0 && zi == 0)
                                                        {
                                                            sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(3, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                        }
                                                        else
                                                        {
                                                            sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(3, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                        }

                                                        somechunk.setvertex(3);

                                                        mainchunkdivfront[someindexmain].removefromarray(whatsthecurrentvertexcount, 3, realindexofchunkinbundle);

                                                        if (somechunk.arrayofvertstop.Length > 0)
                                                        {
                                                            int vertexlength = mainchunkdivfront[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 3, arrayindexmap);
                                                            mainchunkdivfront[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunk.arrayofvertstop.Length, 3);

                                                            somechunk.cleararrays();
                                                        }
                                                        sccslevelgen.arraychunkdatalod0[3][realindexofchunkinbundle].vertexcount = somechunk.arrayofvertstop.Length;



                                                        realindexofchunkinbundle++;
                                                    }
                                                }
                                            }



                                            realindexofchunkinbundle = arrayindexmap;
                                            for (int xi = 0; xi < thechunkdivx; xi++) 
                                            {
                                                for (int yi = 0; yi < thechunkdivy; yi++)
                                                {
                                                    for (int zi = 0; zi < thechunkdivz; zi++)
                                                    {

                                                        int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[4][realindexofchunkinbundle].vertexcount;// somechunk.vertexcountermemory;

                                                        if (xi == 0 && yi == 0 && zi == 0)
                                                        {
                                                            sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(4, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                        }
                                                        else
                                                        {
                                                            sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(4, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                        }

                                                        somechunk.setvertex(4);

                                                        mainchunkdivback[someindexmain].removefromarray(whatsthecurrentvertexcount, 4, realindexofchunkinbundle);

                                                        if (somechunk.arrayofvertstop.Length > 0)
                                                        {
                                                            int vertexlength = mainchunkdivback[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 4, arrayindexmap);
                                                            mainchunkdivback[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunk.arrayofvertstop.Length, 4);

                                                            somechunk.cleararrays();
                                                        }
                                                        sccslevelgen.arraychunkdatalod0[4][realindexofchunkinbundle].vertexcount = somechunk.arrayofvertstop.Length;


                                                        realindexofchunkinbundle++;
                                                    }
                                                }
                                            }




                                            realindexofchunkinbundle = arrayindexmap;
                                            for (int xi = 0; xi < thechunkdivx; xi++) 
                                            {
                                                for (int yi = 0; yi < thechunkdivy; yi++)
                                                {
                                                    for (int zi = 0; zi < thechunkdivz; zi++)
                                                    {

                                                        int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[5][realindexofchunkinbundle].vertexcount;// somechunk.vertexcountermemory;

                                                        if (xi == 0 && yi == 0 && zi == 0)
                                                        {
                                                            sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(5, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                        }
                                                        else
                                                        {
                                                            sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(5, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                        }

                                                        somechunk.setvertex(5);

                                                        mainchunkdivbottom[someindexmain].removefromarray(whatsthecurrentvertexcount, 5, realindexofchunkinbundle);

                                                        if (somechunk.arrayofvertstop.Length > 0)
                                                        {
                                                            int vertexlength = mainchunkdivbottom[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 5, arrayindexmap);
                                                            mainchunkdivbottom[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunk.arrayofvertstop.Length, 5);

                                                            somechunk.cleararrays();
                                                        }
                                                        sccslevelgen.arraychunkdatalod0[5][realindexofchunkinbundle].vertexcount = somechunk.arrayofvertstop.Length;

                                                        realindexofchunkinbundle++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

















                //ADJACENT CHUNKS 
                //ADJACENT CHUNKS 
                //ADJACENT CHUNKS 



                somevaldiv = 1.0f / ((0.01f * 0.5f));
                //xxxxxxxxxxxxxxxxxxxxxxxxxxxx

                someposfootx = (int)(((float)Math.Floor(pickaxetippoint.X * somevaldiv) / somevaldiv) / (0.01f * 0.5f)) - 1;
                someposfooty = (int)(((float)Math.Floor(pickaxetippoint.Y * somevaldiv) / somevaldiv) / (0.01f * 0.5f));
                someposfootz = (int)(((float)Math.Floor(pickaxetippoint.Z * somevaldiv) / somevaldiv) / (0.01f * 0.5f));

                maptotaltimesx = 0;
                maptotaltimesy = 0;
                maptotaltimesz = 0;

                if (pickaxetippoint.X >= 0)
                {
                    someremainsx = (int)Math.Floor((someposfootx / chunkwidthsimfloat)) * chunkwidthsim;
                    maptotaltimesx = (int)(someposfootx - someremainsx);
                }
                else
                {
                    someremainsx = (int)Math.Floor((someposfootx / chunkwidthsimfloat)) * chunkwidthsim;
                    maptotaltimesx = -chunkwidthsim + (int)(someremainsx - someposfootx) + chunkwidthsim;
                    maptotaltimesx *= -1;
                }

                if (pickaxetippoint.Y >= 0)
                {
                    someremainsy = (int)Math.Floor((someposfooty / chunkheightsimfloat)) * chunkheightsim;
                    maptotaltimesy = (int)(someposfooty - someremainsy);
                }
                else
                {
                    someremainsy = (int)Math.Floor((someposfooty / chunkheightsimfloat)) * chunkheightsim;
                    maptotaltimesy = -chunkheightsim + (int)(someremainsy - someposfooty) + chunkheightsim;
                    maptotaltimesy *= -1;
                }

                if (pickaxetippoint.Z >= 0)
                {
                    someremainsz = (int)Math.Floor((someposfootz / chunkdepthsimfloat)) * chunkdepthsim;
                    maptotaltimesz = (int)(someposfootz - someremainsz);
                }
                else
                {
                    someremainsz = (int)Math.Floor((someposfootz / chunkdepthsimfloat)) * chunkdepthsim;
                    //Console.WriteLine(someremainsz);

                    maptotaltimesz = -chunkdepthsim + (int)(someremainsz - someposfootz) + chunkdepthsim;
                    maptotaltimesz *= -1;
                }

                //indexx = maptotaltimesx;
                //indexy = maptotaltimesy;
                //indexz = maptotaltimesz;

                chunkposx = someremainsx / chunkwidthsim;
                chunkposy = someremainsy / chunkheightsim;
                chunkposz = someremainsz / chunkdepthsim;



                someindexmap = indexx + chunkwidthsim * ((indexy) + chunkheightsim * indexz);

                somearrayindex = 0;


                sometotaltimesx = someremainsx / chunkwidthsim;// totalTimesx;
                sometotaltimesy = someremainsy / chunkheightsim;//totalTimesy;
                sometotaltimesz = someremainsz / chunkdepthsim;//totalTimesz;

                sometotaltx = sometotaltimesx;
                sometotalty = sometotaltimesy;
                sometotaltz = sometotaltimesz;


                sometotaltx /= incrementsdivx;
                sometotaltx = (float)Math.Floor(sometotaltx) * incrementsdivx;
                sometotaltimesx = (int)sometotaltx / incrementsdivx;


                sometotalty /= incrementsdivy;
                sometotalty = (float)Math.Floor(sometotalty) * incrementsdivy;
                sometotaltimesy = (int)sometotalty / incrementsdivy;


                sometotaltz /= incrementsdivz;
                sometotaltz = (float)Math.Floor(sometotaltz) * incrementsdivz;
                sometotaltimesz = (int)sometotaltz / incrementsdivz;

                if (sometotaltimesx < 0)
                {
                    sometotaltimesx *= -1;
                    sometotaltimesx = sometotaltimesx + ((divx / 2) - 1);
                }

                if (sometotaltimesy < 0)
                {
                    sometotaltimesy *= -1;
                    sometotaltimesy = sometotaltimesy + ((divy / 2) - 1);
                }
                if (sometotaltimesz < 0)
                {
                    sometotaltimesz *= -1;
                    sometotaltimesz = sometotaltimesz + ((divz / 2) - 1);
                }

                //Console.WriteLine("x:" + sometotaltimesx + "/y:" + sometotaltimesy + ":z/" + sometotaltimesz);

                someindexmain = sometotaltimesx + divx * ((sometotaltimesy) + divy * sometotaltimesz);

                //someindexmain = 0;
                var somewidth = chunkwidthsim - 1;
                var someheight = chunkheightsim - 1;
                var somedepth = chunkdepthsim - 1;

                if (indexx == 0)
                {
                    int arrayindexmap;
                    int somearrayindexadjacent;
                    var somechunkadjacent = sccslevelgen.getchunkinlevelgenmap(totaltimesforonepartschunksx - 1, totaltimesforonepartschunksy, totaltimesforonepartschunksz, 1, out arrayindexmap); ;

                    if (somechunkadjacent != null)
                    {
                        if (somechunkadjacent.map != null)
                        {
                            if (sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[0] == totaltimesforonepartschunksx - 1 && sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[1] == totaltimesforonepartschunksy && sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[2] == totaltimesforonepartschunksz)
                            //if (somechunkadjacent.chunkPos[0] == totaltimesforonepartschunksx-1 && somechunkadjacent.chunkPos[1] == totaltimesforonepartschunksy && somechunkadjacent.chunkPos[2] == totaltimesforonepartschunksz)
                            {

                                someindexmap = (somewidth) + chunkwidthsim * ((indexy) + chunkheightsim * indexz);
                                //someindexmap = (indexx) + 8 * ((indexy) + 8 * indexz);
                                if (somechunkadjacent.map != null)
                                {
                                    if (somechunkadjacent.map[someindexmap] == 1)
                                    {
                                        int realindexofchunkinbundle = arrayindexmap;

                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[0][realindexofchunkinbundle].vertexcount;// somechunk.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(0, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(0, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(0);

                                                    mainchunkdivtop[someindexmain].removefromarray(whatsthecurrentvertexcount, 0, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivtop[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 0, arrayindexmap);
                                                        mainchunkdivtop[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 0);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[0][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;


                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }





                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[1][realindexofchunkinbundle].vertexcount;// somechunk.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(1, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(1, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(1);

                                                    mainchunkdivleft[someindexmain].removefromarray(whatsthecurrentvertexcount, 1, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivleft[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 1, arrayindexmap);
                                                        mainchunkdivleft[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 1);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[1][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;


                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }


                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[2][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(2, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(2, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(2);

                                                    mainchunkdivright[someindexmain].removefromarray(whatsthecurrentvertexcount, 2, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivright[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 2, arrayindexmap);
                                                        mainchunkdivright[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 2);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[2][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;



                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }



                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[3][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(3, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(3, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(3);

                                                    mainchunkdivfront[someindexmain].removefromarray(whatsthecurrentvertexcount, 3, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivfront[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 3, arrayindexmap);
                                                        mainchunkdivfront[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 3);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[3][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;



                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }



                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[4][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(4, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(4, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(4);

                                                    mainchunkdivback[someindexmain].removefromarray(whatsthecurrentvertexcount, 4, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivback[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 4, arrayindexmap);
                                                        mainchunkdivback[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 4);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[4][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;


                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }




                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[5][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(5, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(5, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(5);

                                                    mainchunkdivbottom[someindexmain].removefromarray(whatsthecurrentvertexcount, 5, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivbottom[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 5, arrayindexmap);
                                                        mainchunkdivbottom[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 5);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[5][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;

                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


















                someposfootx = (int)(((float)Math.Floor(pickaxetippoint.X * somevaldiv) / somevaldiv) / (0.01f * 0.5f)) + 1;
                someposfooty = (int)(((float)Math.Floor(pickaxetippoint.Y * somevaldiv) / somevaldiv) / (0.01f * 0.5f));
                someposfootz = (int)(((float)Math.Floor(pickaxetippoint.Z * somevaldiv) / somevaldiv) / (0.01f * 0.5f));

                maptotaltimesx = 0;
                maptotaltimesy = 0;
                maptotaltimesz = 0;

                if (pickaxetippoint.X >= 0)
                {
                    someremainsx = (int)Math.Floor((someposfootx / chunkwidthsimfloat)) * chunkwidthsim;
                    maptotaltimesx = (int)(someposfootx - someremainsx);
                }
                else
                {
                    someremainsx = (int)Math.Floor((someposfootx / chunkwidthsimfloat)) * chunkwidthsim;
                    maptotaltimesx = -chunkwidthsim + (int)(someremainsx - someposfootx) + chunkwidthsim;
                    maptotaltimesx *= -1;
                }

                if (pickaxetippoint.Y >= 0)
                {
                    someremainsy = (int)Math.Floor((someposfooty / chunkheightsimfloat)) * chunkheightsim;
                    maptotaltimesy = (int)(someposfooty - someremainsy);
                }
                else
                {
                    someremainsy = (int)Math.Floor((someposfooty / chunkheightsimfloat)) * chunkheightsim;
                    maptotaltimesy = -chunkheightsim + (int)(someremainsy - someposfooty) + chunkheightsim;
                    maptotaltimesy *= -1;
                }

                if (pickaxetippoint.Z >= 0)
                {
                    someremainsz = (int)Math.Floor((someposfootz / chunkdepthsimfloat)) * chunkdepthsim;
                    maptotaltimesz = (int)(someposfootz - someremainsz);
                }
                else
                {
                    someremainsz = (int)Math.Floor((someposfootz / chunkdepthsimfloat)) * chunkdepthsim;
                    //Console.WriteLine(someremainsz);

                    maptotaltimesz = -chunkdepthsim + (int)(someremainsz - someposfootz) + chunkdepthsim;
                    maptotaltimesz *= -1;
                }

                //indexx = maptotaltimesx;
                //indexy = maptotaltimesy;
                //indexz = maptotaltimesz;

                chunkposx = someremainsx / chunkwidthsim;
                chunkposy = someremainsy / chunkheightsim;
                chunkposz = someremainsz / chunkdepthsim;



                someindexmap = indexx + chunkwidthsim * ((indexy) + chunkheightsim * indexz);

                somearrayindex = 0;


                sometotaltimesx = someremainsx / chunkwidthsim;// totalTimesx;
                sometotaltimesy = someremainsy / chunkheightsim;//totalTimesy;
                sometotaltimesz = someremainsz / chunkdepthsim;//totalTimesz;

                sometotaltx = sometotaltimesx;
                sometotalty = sometotaltimesy;
                sometotaltz = sometotaltimesz;


                sometotaltx /= incrementsdivx;
                sometotaltx = (float)Math.Floor(sometotaltx) * incrementsdivx;
                sometotaltimesx = (int)sometotaltx / incrementsdivx;


                sometotalty /= incrementsdivy;
                sometotalty = (float)Math.Floor(sometotalty) * incrementsdivy;
                sometotaltimesy = (int)sometotalty / incrementsdivy;


                sometotaltz /= incrementsdivz;
                sometotaltz = (float)Math.Floor(sometotaltz) * incrementsdivz;
                sometotaltimesz = (int)sometotaltz / incrementsdivz;

                if (sometotaltimesx < 0)
                {
                    sometotaltimesx *= -1;
                    sometotaltimesx = sometotaltimesx + ((divx / 2) - 1);
                }

                if (sometotaltimesy < 0)
                {
                    sometotaltimesy *= -1;
                    sometotaltimesy = sometotaltimesy + ((divy / 2) - 1);
                }
                if (sometotaltimesz < 0)
                {
                    sometotaltimesz *= -1;
                    sometotaltimesz = sometotaltimesz + ((divz / 2) - 1);
                }

                //Console.WriteLine("x:" + sometotaltimesx + "/y:" + sometotaltimesy + ":z/" + sometotaltimesz);

                someindexmain = sometotaltimesx + divx * ((sometotaltimesy) + divy * sometotaltimesz);

                //someindexmain = 0;
                 somewidth = chunkwidthsim - 1;
                 someheight = chunkheightsim - 1;
                 somedepth = chunkdepthsim - 1;


                if (indexx == somewidth)
                {
                    int arrayindexmap;
                    int somearrayindexadjacent;
                    var somechunkadjacent = sccslevelgen.getchunkinlevelgenmap(totaltimesforonepartschunksx + 1, totaltimesforonepartschunksy, totaltimesforonepartschunksz, 1, out arrayindexmap); ;

                    if (somechunkadjacent != null)
                    {
                        if (somechunkadjacent.map != null)
                        {
                            if (sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[0] == totaltimesforonepartschunksx + 1 && sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[1] == totaltimesforonepartschunksy && sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[2] == totaltimesforonepartschunksz)

                            //if (somechunkadjacent.chunkPos[0] == totaltimesforonepartschunksx+1 && somechunkadjacent.chunkPos[1] == totaltimesforonepartschunksy && somechunkadjacent.chunkPos[2] == totaltimesforonepartschunksz)
                            {
                                //someindexmap = (indexx) + 8 * ((indexy) + 8 * indexz);
                                someindexmap = (0) + chunkwidthsim * ((indexy) + chunkheightsim * indexz);
                                if (somechunkadjacent.map != null)
                                {
                                    if (somechunkadjacent.map[someindexmap] == 1)
                                    {
                                        int realindexofchunkinbundle = arrayindexmap;

                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[0][realindexofchunkinbundle].vertexcount;// somechunk.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(0, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(0, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(0);

                                                    mainchunkdivtop[someindexmain].removefromarray(whatsthecurrentvertexcount, 0, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivtop[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 0, arrayindexmap);
                                                        mainchunkdivtop[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 0);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[0][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;


                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }





                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[1][realindexofchunkinbundle].vertexcount;// somechunk.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(1, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(1, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(1);

                                                    mainchunkdivleft[someindexmain].removefromarray(whatsthecurrentvertexcount, 1, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivleft[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 1, arrayindexmap);
                                                        mainchunkdivleft[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 1);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[1][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;


                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }


                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[2][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(2, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(2, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(2);

                                                    mainchunkdivright[someindexmain].removefromarray(whatsthecurrentvertexcount, 2, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivright[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 2, arrayindexmap);
                                                        mainchunkdivright[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 2);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[2][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;



                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }



                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[3][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(3, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(3, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(3);

                                                    mainchunkdivfront[someindexmain].removefromarray(whatsthecurrentvertexcount, 3, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivfront[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 3, arrayindexmap);
                                                        mainchunkdivfront[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 3);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[3][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;



                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }



                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[4][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(4, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(4, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(4);

                                                    mainchunkdivback[someindexmain].removefromarray(whatsthecurrentvertexcount, 4, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivback[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 4, arrayindexmap);
                                                        mainchunkdivback[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 4);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[4][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;


                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }




                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[5][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(5, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(5, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(5);

                                                    mainchunkdivbottom[someindexmain].removefromarray(whatsthecurrentvertexcount, 5, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivbottom[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 5, arrayindexmap);
                                                        mainchunkdivbottom[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 5);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[5][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;

                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }








                //ZZZZZZZZZZZZZZZZZZZZZZZ


                someposfootx = (int)(((float)Math.Floor(pickaxetippoint.X * somevaldiv) / somevaldiv) / (0.01f * 0.5f));
                someposfooty = (int)(((float)Math.Floor(pickaxetippoint.Y * somevaldiv) / somevaldiv) / (0.01f * 0.5f));
                someposfootz = (int)(((float)Math.Floor(pickaxetippoint.Z * somevaldiv) / somevaldiv) / (0.01f * 0.5f)) - 1;

                maptotaltimesx = 0;
                maptotaltimesy = 0;
                maptotaltimesz = 0;

                if (pickaxetippoint.X >= 0)
                {
                    someremainsx = (int)Math.Floor((someposfootx / chunkwidthsimfloat)) * chunkwidthsim;
                    maptotaltimesx = (int)(someposfootx - someremainsx);
                }
                else
                {
                    someremainsx = (int)Math.Floor((someposfootx / chunkwidthsimfloat)) * chunkwidthsim;
                    maptotaltimesx = -chunkwidthsim + (int)(someremainsx - someposfootx) + chunkwidthsim;
                    maptotaltimesx *= -1;
                }

                if (pickaxetippoint.Y >= 0)
                {
                    someremainsy = (int)Math.Floor((someposfooty / chunkheightsimfloat)) * chunkheightsim;
                    maptotaltimesy = (int)(someposfooty - someremainsy);
                }
                else
                {
                    someremainsy = (int)Math.Floor((someposfooty / chunkheightsimfloat)) * chunkheightsim;
                    maptotaltimesy = -chunkheightsim + (int)(someremainsy - someposfooty) + chunkheightsim;
                    maptotaltimesy *= -1;
                }

                if (pickaxetippoint.Z >= 0)
                {
                    someremainsz = (int)Math.Floor((someposfootz / chunkdepthsimfloat)) * chunkdepthsim;
                    maptotaltimesz = (int)(someposfootz - someremainsz);
                }
                else
                {
                    someremainsz = (int)Math.Floor((someposfootz / chunkdepthsimfloat)) * chunkdepthsim;
                    //Console.WriteLine(someremainsz);

                    maptotaltimesz = -chunkdepthsim + (int)(someremainsz - someposfootz) + chunkdepthsim;
                    maptotaltimesz *= -1;
                }

                //indexx = maptotaltimesx;
                //indexy = maptotaltimesy;
                //indexz = maptotaltimesz;

                chunkposx = someremainsx / chunkwidthsim;
                chunkposy = someremainsy / chunkheightsim;
                chunkposz = someremainsz / chunkdepthsim;



                someindexmap = indexx + chunkwidthsim * ((indexy) + chunkheightsim * indexz);

                somearrayindex = 0;


                sometotaltimesx = someremainsx / chunkwidthsim;// totalTimesx;
                sometotaltimesy = someremainsy / chunkheightsim;//totalTimesy;
                sometotaltimesz = someremainsz / chunkdepthsim;//totalTimesz;

                sometotaltx = sometotaltimesx;
                sometotalty = sometotaltimesy;
                sometotaltz = sometotaltimesz;


                sometotaltx /= incrementsdivx;
                sometotaltx = (float)Math.Floor(sometotaltx) * incrementsdivx;
                sometotaltimesx = (int)sometotaltx / incrementsdivx;


                sometotalty /= incrementsdivy;
                sometotalty = (float)Math.Floor(sometotalty) * incrementsdivy;
                sometotaltimesy = (int)sometotalty / incrementsdivy;


                sometotaltz /= incrementsdivz;
                sometotaltz = (float)Math.Floor(sometotaltz) * incrementsdivz;
                sometotaltimesz = (int)sometotaltz / incrementsdivz;

                if (sometotaltimesx < 0)
                {
                    sometotaltimesx *= -1;
                    sometotaltimesx = sometotaltimesx + ((divx / 2) - 1);
                }

                if (sometotaltimesy < 0)
                {
                    sometotaltimesy *= -1;
                    sometotaltimesy = sometotaltimesy + ((divy / 2) - 1);
                }
                if (sometotaltimesz < 0)
                {
                    sometotaltimesz *= -1;
                    sometotaltimesz = sometotaltimesz + ((divz / 2) - 1);
                }

                //Console.WriteLine("x:" + sometotaltimesx + "/y:" + sometotaltimesy + ":z/" + sometotaltimesz);

                someindexmain = sometotaltimesx + divx * ((sometotaltimesy) + divy * sometotaltimesz);

                //someindexmain = 0;
                somewidth = chunkwidthsim - 1;
                someheight = chunkheightsim - 1;
                somedepth = chunkdepthsim - 1;


                if (indexz == 0)
                {
                    int arrayindexmap;
                    int somearrayindexadjacent;
                    var somechunkadjacent = sccslevelgen.getchunkinlevelgenmap(totaltimesforonepartschunksx, totaltimesforonepartschunksy, totaltimesforonepartschunksz - 1, 1, out arrayindexmap); ;

                    if (somechunkadjacent != null)
                    {
                        if (somechunkadjacent.map != null)
                        {

                            if (sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[0] == totaltimesforonepartschunksx && sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[1] == totaltimesforonepartschunksy && sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[2] == totaltimesforonepartschunksz - 1)

                            //if (somechunkadjacent.chunkPos[0] == totaltimesforonepartschunksx && somechunkadjacent.chunkPos[1] == totaltimesforonepartschunksy && somechunkadjacent.chunkPos[2] == totaltimesforonepartschunksz -1)
                            {
                                //someindexmap = (indexx) + 8 * ((indexy) + 8 * indexz);
                                someindexmap = (indexx) + chunkwidthsim * ((indexy) + chunkheightsim * somedepth);
                                //someindexmap = (somewidth) + 8 * ((indexy) + 8 * indexz);
                                if (somechunkadjacent.map != null)
                                {
                                    if (somechunkadjacent.map[someindexmap] == 1)
                                    {
                                        int realindexofchunkinbundle = arrayindexmap;

                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[0][realindexofchunkinbundle].vertexcount;// somechunk.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(0, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(0, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(0);

                                                    mainchunkdivtop[someindexmain].removefromarray(whatsthecurrentvertexcount, 0, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivtop[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 0, arrayindexmap);
                                                        mainchunkdivtop[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 0);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[0][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;


                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }





                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[1][realindexofchunkinbundle].vertexcount;// somechunk.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(1, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(1, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(1);

                                                    mainchunkdivleft[someindexmain].removefromarray(whatsthecurrentvertexcount, 1, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivleft[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 1, arrayindexmap);
                                                        mainchunkdivleft[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 1);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[1][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;


                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }


                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[2][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(2, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(2, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(2);

                                                    mainchunkdivright[someindexmain].removefromarray(whatsthecurrentvertexcount, 2, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivright[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 2, arrayindexmap);
                                                        mainchunkdivright[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 2);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[2][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;



                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }



                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[3][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(3, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(3, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(3);

                                                    mainchunkdivfront[someindexmain].removefromarray(whatsthecurrentvertexcount, 3, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivfront[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 3, arrayindexmap);
                                                        mainchunkdivfront[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 3);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[3][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;



                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }



                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[4][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(4, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(4, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(4);

                                                    mainchunkdivback[someindexmain].removefromarray(whatsthecurrentvertexcount, 4, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivback[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 4, arrayindexmap);
                                                        mainchunkdivback[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 4);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[4][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;


                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }




                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[5][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(5, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(5, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(5);

                                                    mainchunkdivbottom[someindexmain].removefromarray(whatsthecurrentvertexcount, 5, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivbottom[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 5, arrayindexmap);
                                                        mainchunkdivbottom[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 5);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[5][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;

                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


















                someposfootx = (int)(((float)Math.Floor(pickaxetippoint.X * somevaldiv) / somevaldiv) / (0.01f * 0.5f));
                someposfooty = (int)(((float)Math.Floor(pickaxetippoint.Y * somevaldiv) / somevaldiv) / (0.01f * 0.5f));
                someposfootz = (int)(((float)Math.Floor(pickaxetippoint.Z * somevaldiv) / somevaldiv) / (0.01f * 0.5f)) + 1;

                maptotaltimesx = 0;
                maptotaltimesy = 0;
                maptotaltimesz = 0;

                if (pickaxetippoint.X >= 0)
                {
                    someremainsx = (int)Math.Floor((someposfootx / chunkwidthsimfloat)) * chunkwidthsim;
                    maptotaltimesx = (int)(someposfootx - someremainsx);
                }
                else
                {
                    someremainsx = (int)Math.Floor((someposfootx / chunkwidthsimfloat)) * chunkwidthsim;
                    maptotaltimesx = -chunkwidthsim + (int)(someremainsx - someposfootx) + chunkwidthsim;
                    maptotaltimesx *= -1;
                }

                if (pickaxetippoint.Y >= 0)
                {
                    someremainsy = (int)Math.Floor((someposfooty / chunkheightsimfloat)) * chunkheightsim;
                    maptotaltimesy = (int)(someposfooty - someremainsy);
                }
                else
                {
                    someremainsy = (int)Math.Floor((someposfooty / chunkheightsimfloat)) * chunkheightsim;
                    maptotaltimesy = -chunkheightsim + (int)(someremainsy - someposfooty) + chunkheightsim;
                    maptotaltimesy *= -1;
                }

                if (pickaxetippoint.Z >= 0)
                {
                    someremainsz = (int)Math.Floor((someposfootz / chunkdepthsimfloat)) * chunkdepthsim;
                    maptotaltimesz = (int)(someposfootz - someremainsz);
                }
                else
                {
                    someremainsz = (int)Math.Floor((someposfootz / chunkdepthsimfloat)) * chunkdepthsim;
                    //Console.WriteLine(someremainsz);

                    maptotaltimesz = -chunkdepthsim + (int)(someremainsz - someposfootz) + chunkdepthsim;
                    maptotaltimesz *= -1;
                }

                //indexx = maptotaltimesx;
                //indexy = maptotaltimesy;
                //indexz = maptotaltimesz;

                chunkposx = someremainsx / chunkwidthsim;
                chunkposy = someremainsy / chunkheightsim;
                chunkposz = someremainsz / chunkdepthsim;



                someindexmap = indexx + chunkwidthsim * ((indexy) + chunkheightsim * indexz);

                somearrayindex = 0;


                sometotaltimesx = someremainsx / chunkwidthsim;// totalTimesx;
                sometotaltimesy = someremainsy / chunkheightsim;//totalTimesy;
                sometotaltimesz = someremainsz / chunkdepthsim;//totalTimesz;

                sometotaltx = sometotaltimesx;
                sometotalty = sometotaltimesy;
                sometotaltz = sometotaltimesz;


                sometotaltx /= incrementsdivx;
                sometotaltx = (float)Math.Floor(sometotaltx) * incrementsdivx;
                sometotaltimesx = (int)sometotaltx / incrementsdivx;


                sometotalty /= incrementsdivy;
                sometotalty = (float)Math.Floor(sometotalty) * incrementsdivy;
                sometotaltimesy = (int)sometotalty / incrementsdivy;


                sometotaltz /= incrementsdivz;
                sometotaltz = (float)Math.Floor(sometotaltz) * incrementsdivz;
                sometotaltimesz = (int)sometotaltz / incrementsdivz;

                if (sometotaltimesx < 0)
                {
                    sometotaltimesx *= -1;
                    sometotaltimesx = sometotaltimesx + ((divx / 2) - 1);
                }

                if (sometotaltimesy < 0)
                {
                    sometotaltimesy *= -1;
                    sometotaltimesy = sometotaltimesy + ((divy / 2) - 1);
                }
                if (sometotaltimesz < 0)
                {
                    sometotaltimesz *= -1;
                    sometotaltimesz = sometotaltimesz + ((divz / 2) - 1);
                }

                //Console.WriteLine("x:" + sometotaltimesx + "/y:" + sometotaltimesy + ":z/" + sometotaltimesz);

                someindexmain = sometotaltimesx + divx * ((sometotaltimesy) + divy * sometotaltimesz);

                //someindexmain = 0;
                somewidth = chunkwidthsim - 1;
                someheight = chunkheightsim - 1;
                somedepth = chunkdepthsim - 1;

                if (indexz == somedepth)
                {
                    int arrayindexmap;
                    int somearrayindexadjacent;
                    var somechunkadjacent = sccslevelgen.getchunkinlevelgenmap(totaltimesforonepartschunksx, totaltimesforonepartschunksy, totaltimesforonepartschunksz + 1, 1, out arrayindexmap); ;

                    if (somechunkadjacent != null)
                    {
                        if (somechunkadjacent.map != null)
                        {

                            if (sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[0] == totaltimesforonepartschunksx && sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[1] == totaltimesforonepartschunksy && sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[2] == totaltimesforonepartschunksz + 1)

                            //if (somechunkadjacent.chunkPos[0] == totaltimesforonepartschunksx && somechunkadjacent.chunkPos[1] == totaltimesforonepartschunksy && somechunkadjacent.chunkPos[2] == totaltimesforonepartschunksz + 1)
                            {
                                //someindexmap = (indexx) + 8 * ((indexy) + 8 * indexz);
                                someindexmap = (indexx) + chunkwidthsim * ((indexy) + chunkheightsim * 0);
                                //someindexmap = (somewidth) + 8 * ((indexy) + 8 * indexz);
                                if (somechunkadjacent.map != null)
                                {
                                    if (somechunkadjacent.map[someindexmap] == 1)
                                    {
                                        int realindexofchunkinbundle = arrayindexmap;

                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[0][realindexofchunkinbundle].vertexcount;// somechunk.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(0, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(0, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(0);

                                                    mainchunkdivtop[someindexmain].removefromarray(whatsthecurrentvertexcount, 0, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivtop[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 0, arrayindexmap);
                                                        mainchunkdivtop[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 0);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[0][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;


                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }





                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[1][realindexofchunkinbundle].vertexcount;// somechunk.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(1, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(1, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(1);

                                                    mainchunkdivleft[someindexmain].removefromarray(whatsthecurrentvertexcount, 1, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivleft[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 1, arrayindexmap);
                                                        mainchunkdivleft[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 1);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[1][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;


                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }


                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[2][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(2, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(2, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(2);

                                                    mainchunkdivright[someindexmain].removefromarray(whatsthecurrentvertexcount, 2, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivright[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 2, arrayindexmap);
                                                        mainchunkdivright[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 2);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[2][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;



                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }



                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[3][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(3, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(3, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(3);

                                                    mainchunkdivfront[someindexmain].removefromarray(whatsthecurrentvertexcount, 3, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivfront[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 3, arrayindexmap);
                                                        mainchunkdivfront[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 3);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[3][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;



                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }



                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[4][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(4, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(4, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(4);

                                                    mainchunkdivback[someindexmain].removefromarray(whatsthecurrentvertexcount, 4, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivback[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 4, arrayindexmap);
                                                        mainchunkdivback[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 4);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[4][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;


                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }




                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[5][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(5, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(5, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(5);

                                                    mainchunkdivbottom[someindexmain].removefromarray(whatsthecurrentvertexcount, 5, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivbottom[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 5, arrayindexmap);
                                                        mainchunkdivbottom[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 5);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[5][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;

                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }



                //ZZZZZZZZZZZZZZZZZZZZZZ













                //YYYYYYYYYYYYYYYYYY

                someposfootx = (int)(((float)Math.Floor(pickaxetippoint.X * somevaldiv) / somevaldiv) / (0.01f * 0.5f));
                someposfooty = (int)(((float)Math.Floor(pickaxetippoint.Y * somevaldiv) / somevaldiv) / (0.01f * 0.5f)) - 1;
                someposfootz = (int)(((float)Math.Floor(pickaxetippoint.Z * somevaldiv) / somevaldiv) / (0.01f * 0.5f));

                maptotaltimesx = 0;
                maptotaltimesy = 0;
                maptotaltimesz = 0;

                if (pickaxetippoint.X >= 0)
                {
                    someremainsx = (int)Math.Floor((someposfootx / chunkwidthsimfloat)) * chunkwidthsim;
                    maptotaltimesx = (int)(someposfootx - someremainsx);
                }
                else
                {
                    someremainsx = (int)Math.Floor((someposfootx / chunkwidthsimfloat)) * chunkwidthsim;
                    maptotaltimesx = -chunkwidthsim + (int)(someremainsx - someposfootx) + chunkwidthsim;
                    maptotaltimesx *= -1;
                }

                if (pickaxetippoint.Y >= 0)
                {
                    someremainsy = (int)Math.Floor((someposfooty / chunkheightsimfloat)) * chunkheightsim;
                    maptotaltimesy = (int)(someposfooty - someremainsy);
                }
                else
                {
                    someremainsy = (int)Math.Floor((someposfooty / chunkheightsimfloat)) * chunkheightsim;
                    maptotaltimesy = -chunkheightsim + (int)(someremainsy - someposfooty) + chunkheightsim;
                    maptotaltimesy *= -1;
                }

                if (pickaxetippoint.Z >= 0)
                {
                    someremainsz = (int)Math.Floor((someposfootz / chunkdepthsimfloat)) * chunkdepthsim;
                    maptotaltimesz = (int)(someposfootz - someremainsz);
                }
                else
                {
                    someremainsz = (int)Math.Floor((someposfootz / chunkdepthsimfloat)) * chunkdepthsim;
                    //Console.WriteLine(someremainsz);

                    maptotaltimesz = -chunkdepthsim + (int)(someremainsz - someposfootz) + chunkdepthsim;
                    maptotaltimesz *= -1;
                }

                //indexx = maptotaltimesx;
                //indexy = maptotaltimesy;
                //indexz = maptotaltimesz;

                chunkposx = someremainsx / chunkwidthsim;
                chunkposy = someremainsy / chunkheightsim;
                chunkposz = someremainsz / chunkdepthsim;



                someindexmap = indexx + chunkwidthsim * ((indexy) + chunkheightsim * indexz);

                somearrayindex = 0;


                sometotaltimesx = someremainsx / chunkwidthsim;// totalTimesx;
                sometotaltimesy = someremainsy / chunkheightsim;//totalTimesy;
                sometotaltimesz = someremainsz / chunkdepthsim;//totalTimesz;

                sometotaltx = sometotaltimesx;
                sometotalty = sometotaltimesy;
                sometotaltz = sometotaltimesz;


                sometotaltx /= incrementsdivx;
                sometotaltx = (float)Math.Floor(sometotaltx) * incrementsdivx;
                sometotaltimesx = (int)sometotaltx / incrementsdivx;


                sometotalty /= incrementsdivy;
                sometotalty = (float)Math.Floor(sometotalty) * incrementsdivy;
                sometotaltimesy = (int)sometotalty / incrementsdivy;


                sometotaltz /= incrementsdivz;
                sometotaltz = (float)Math.Floor(sometotaltz) * incrementsdivz;
                sometotaltimesz = (int)sometotaltz / incrementsdivz;

                if (sometotaltimesx < 0)
                {
                    sometotaltimesx *= -1;
                    sometotaltimesx = sometotaltimesx + ((divx / 2) - 1);
                }

                if (sometotaltimesy < 0)
                {
                    sometotaltimesy *= -1;
                    sometotaltimesy = sometotaltimesy + ((divy / 2) - 1);
                }
                if (sometotaltimesz < 0)
                {
                    sometotaltimesz *= -1;
                    sometotaltimesz = sometotaltimesz + ((divz / 2) - 1);
                }

                //Console.WriteLine("x:" + sometotaltimesx + "/y:" + sometotaltimesy + ":z/" + sometotaltimesz);

                someindexmain = sometotaltimesx + divx * ((sometotaltimesy) + divy * sometotaltimesz);

                //someindexmain = 0;
                somewidth = chunkwidthsim - 1;
                someheight = chunkheightsim - 1;
                somedepth = chunkdepthsim - 1;

                if (indexy == 0)
                {
                    int arrayindexmap;
                    int somearrayindexadjacent;
                    var somechunkadjacent = sccslevelgen.getchunkinlevelgenmap(totaltimesforonepartschunksx, totaltimesforonepartschunksy - 1, totaltimesforonepartschunksz, 1, out arrayindexmap); ;

                    if (somechunkadjacent != null)
                    {
                        if (somechunkadjacent.map != null)
                        {

                            if (sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[0] == totaltimesforonepartschunksx && sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[1] == totaltimesforonepartschunksy - 1 && sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[2] == totaltimesforonepartschunksz)
                            // if (somechunkadjacent.chunkPos[0] == totaltimesforonepartschunksx && somechunkadjacent.chunkPos[1] == totaltimesforonepartschunksy -1&& somechunkadjacent.chunkPos[2] == totaltimesforonepartschunksz)
                            {
                                //someindexmap = (indexx) + 8 * ((indexy) + 8 * indexz);
                                someindexmap = (indexx) + chunkwidthsim * ((someheight) + chunkheightsim * indexz);
                                //someindexmap = (somewidth) + 8 * ((indexy) + 8 * indexz);
                                if (somechunkadjacent.map != null)
                                {
                                    if (somechunkadjacent.map[someindexmap] == 1)
                                    {
                                        int realindexofchunkinbundle = arrayindexmap;

                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[0][realindexofchunkinbundle].vertexcount;// somechunk.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(0, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(0, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(0);

                                                    mainchunkdivtop[someindexmain].removefromarray(whatsthecurrentvertexcount, 0, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivtop[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 0, arrayindexmap);
                                                        mainchunkdivtop[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 0);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[0][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;


                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }





                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[1][realindexofchunkinbundle].vertexcount;// somechunk.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(1, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(1, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(1);

                                                    mainchunkdivleft[someindexmain].removefromarray(whatsthecurrentvertexcount, 1, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivleft[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 1, arrayindexmap);
                                                        mainchunkdivleft[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 1);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[1][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;


                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }


                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[2][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(2, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(2, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(2);

                                                    mainchunkdivright[someindexmain].removefromarray(whatsthecurrentvertexcount, 2, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivright[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 2, arrayindexmap);
                                                        mainchunkdivright[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 2);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[2][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;



                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }



                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[3][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(3, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(3, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(3);

                                                    mainchunkdivfront[someindexmain].removefromarray(whatsthecurrentvertexcount, 3, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivfront[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 3, arrayindexmap);
                                                        mainchunkdivfront[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 3);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[3][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;



                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }



                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[4][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(4, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(4, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(4);

                                                    mainchunkdivback[someindexmain].removefromarray(whatsthecurrentvertexcount, 4, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivback[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 4, arrayindexmap);
                                                        mainchunkdivback[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 4);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[4][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;


                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }




                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[5][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(5, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(5, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(5);

                                                    mainchunkdivbottom[someindexmain].removefromarray(whatsthecurrentvertexcount, 5, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivbottom[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 5, arrayindexmap);
                                                        mainchunkdivbottom[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 5);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[5][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;

                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


















                someposfootx = (int)(((float)Math.Floor(pickaxetippoint.X * somevaldiv) / somevaldiv) / (0.01f * 0.5f));
                someposfooty = (int)(((float)Math.Floor(pickaxetippoint.Y * somevaldiv) / somevaldiv) / (0.01f * 0.5f)) + 1;
                someposfootz = (int)(((float)Math.Floor(pickaxetippoint.Z * somevaldiv) / somevaldiv) / (0.01f * 0.5f));

                maptotaltimesx = 0;
                maptotaltimesy = 0;
                maptotaltimesz = 0;

                if (pickaxetippoint.X >= 0)
                {
                    someremainsx = (int)Math.Floor((someposfootx / chunkwidthsimfloat)) * chunkwidthsim;
                    maptotaltimesx = (int)(someposfootx - someremainsx);
                }
                else
                {
                    someremainsx = (int)Math.Floor((someposfootx / chunkwidthsimfloat)) * chunkwidthsim;
                    maptotaltimesx = -chunkwidthsim + (int)(someremainsx - someposfootx) + chunkwidthsim;
                    maptotaltimesx *= -1;
                }

                if (pickaxetippoint.Y >= 0)
                {
                    someremainsy = (int)Math.Floor((someposfooty / chunkheightsimfloat)) * chunkheightsim;
                    maptotaltimesy = (int)(someposfooty - someremainsy);
                }
                else
                {
                    someremainsy = (int)Math.Floor((someposfooty / chunkheightsimfloat)) * chunkheightsim;
                    maptotaltimesy = -chunkheightsim + (int)(someremainsy - someposfooty) + chunkheightsim;
                    maptotaltimesy *= -1;
                }

                if (pickaxetippoint.Z >= 0)
                {
                    someremainsz = (int)Math.Floor((someposfootz / chunkdepthsimfloat)) * chunkdepthsim;
                    maptotaltimesz = (int)(someposfootz - someremainsz);
                }
                else
                {
                    someremainsz = (int)Math.Floor((someposfootz / chunkdepthsimfloat)) * chunkdepthsim;
                    //Console.WriteLine(someremainsz);

                    maptotaltimesz = -chunkdepthsim + (int)(someremainsz - someposfootz) + chunkdepthsim;
                    maptotaltimesz *= -1;
                }

                //indexx = maptotaltimesx;
                //indexy = maptotaltimesy;
                //indexz = maptotaltimesz;

                chunkposx = someremainsx / chunkwidthsim;
                chunkposy = someremainsy / chunkheightsim;
                chunkposz = someremainsz / chunkdepthsim;



                someindexmap = indexx + chunkwidthsim * ((indexy) + chunkheightsim * indexz);

                somearrayindex = 0;


                sometotaltimesx = someremainsx / chunkwidthsim;// totalTimesx;
                sometotaltimesy = someremainsy / chunkheightsim;//totalTimesy;
                sometotaltimesz = someremainsz / chunkdepthsim;//totalTimesz;

                sometotaltx = sometotaltimesx;
                sometotalty = sometotaltimesy;
                sometotaltz = sometotaltimesz;


                sometotaltx /= incrementsdivx;
                sometotaltx = (float)Math.Floor(sometotaltx) * incrementsdivx;
                sometotaltimesx = (int)sometotaltx / incrementsdivx;


                sometotalty /= incrementsdivy;
                sometotalty = (float)Math.Floor(sometotalty) * incrementsdivy;
                sometotaltimesy = (int)sometotalty / incrementsdivy;


                sometotaltz /= incrementsdivz;
                sometotaltz = (float)Math.Floor(sometotaltz) * incrementsdivz;
                sometotaltimesz = (int)sometotaltz / incrementsdivz;

                if (sometotaltimesx < 0)
                {
                    sometotaltimesx *= -1;
                    sometotaltimesx = sometotaltimesx + ((divx / 2) - 1);
                }

                if (sometotaltimesy < 0)
                {
                    sometotaltimesy *= -1;
                    sometotaltimesy = sometotaltimesy + ((divy / 2) - 1);
                }
                if (sometotaltimesz < 0)
                {
                    sometotaltimesz *= -1;
                    sometotaltimesz = sometotaltimesz + ((divz / 2) - 1);
                }

                //Console.WriteLine("x:" + sometotaltimesx + "/y:" + sometotaltimesy + ":z/" + sometotaltimesz);

                someindexmain = sometotaltimesx + divx * ((sometotaltimesy) + divy * sometotaltimesz);

                //someindexmain = 0;
                somewidth = chunkwidthsim - 1;
                someheight = chunkheightsim - 1;
                somedepth = chunkdepthsim - 1;

                if (indexy == someheight)
                {
                    int arrayindexmap;
                    int somearrayindexadjacent;
                    var somechunkadjacent = sccslevelgen.getchunkinlevelgenmap(totaltimesforonepartschunksx, totaltimesforonepartschunksy + 1, totaltimesforonepartschunksz, 1, out arrayindexmap); ;

                    if (somechunkadjacent != null)
                    {
                        if (somechunkadjacent.map != null)
                        {
                            if (sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[0] == totaltimesforonepartschunksx && sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[1] == totaltimesforonepartschunksy + 1 && sccslevelgen.arraychunkdatalod0[0][arrayindexmap].chunkPos[2] == totaltimesforonepartschunksz)

                            //if (somechunkadjacent.chunkPos[0] == totaltimesforonepartschunksx && somechunkadjacent.chunkPos[1] == totaltimesforonepartschunksy + 1 && somechunkadjacent.chunkPos[2] == totaltimesforonepartschunksz)
                            {
                                //someindexmap = (indexx) + 8 * ((indexy) + 8 * indexz);
                                someindexmap = (indexx) + chunkwidthsim * ((0) + chunkheightsim * indexz);
                                //someindexmap = (somewidth) + 8 * ((indexy) + 8 * indexz);
                                if (somechunkadjacent.map != null)
                                {
                                    if (somechunkadjacent.map[someindexmap] == 1)
                                    {
                                        int realindexofchunkinbundle = arrayindexmap;

                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[0][realindexofchunkinbundle].vertexcount;// somechunk.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(0, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(0, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(0);

                                                    mainchunkdivtop[someindexmain].removefromarray(whatsthecurrentvertexcount, 0, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivtop[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 0, arrayindexmap);
                                                        mainchunkdivtop[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 0);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[0][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;


                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }





                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[1][realindexofchunkinbundle].vertexcount;// somechunk.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(1, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(1, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(1);

                                                    mainchunkdivleft[someindexmain].removefromarray(whatsthecurrentvertexcount, 1, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivleft[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 1, arrayindexmap);
                                                        mainchunkdivleft[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 1);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[1][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;


                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }


                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[2][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(2, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(2, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(2);

                                                    mainchunkdivright[someindexmain].removefromarray(whatsthecurrentvertexcount, 2, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivright[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 2, arrayindexmap);
                                                        mainchunkdivright[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 2);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[2][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;



                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }



                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[3][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(3, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(3, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(3);

                                                    mainchunkdivfront[someindexmain].removefromarray(whatsthecurrentvertexcount, 3, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivfront[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 3, arrayindexmap);
                                                        mainchunkdivfront[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 3);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[3][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;



                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }



                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[4][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(4, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(4, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(4);

                                                    mainchunkdivback[someindexmain].removefromarray(whatsthecurrentvertexcount, 4, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivback[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 4, arrayindexmap);
                                                        mainchunkdivback[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 4);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[4][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;


                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }




                                        realindexofchunkinbundle = arrayindexmap;
                                        for (int xi = 0; xi < 2; xi++)
                                        {
                                            for (int yi = 0; yi < 2; yi++)
                                            {
                                                for (int zi = 0; zi < 2; zi++)
                                                {

                                                    int whatsthecurrentvertexcount = sccslevelgen.arraychunkdatalod0[5][realindexofchunkinbundle].vertexcount;// somechunkadjacent.vertexcountermemory;

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.startBuildingArray(5, null, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                    }
                                                    else
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[0][arrayindexmap].arraychunkvertslod0.newregenerate(5, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                    }

                                                    somechunkadjacent.setvertex(5);

                                                    mainchunkdivbottom[someindexmain].removefromarray(whatsthecurrentvertexcount, 5, realindexofchunkinbundle);

                                                    if (somechunkadjacent.arrayofvertstop.Length > 0)
                                                    {
                                                        int vertexlength = mainchunkdivbottom[someindexmain].insertdatainbufferstructs(realindexofchunkinbundle, 5, arrayindexmap);
                                                        mainchunkdivbottom[someindexmain].findinstancemeshtoinsertinto(realindexofchunkinbundle, somechunkadjacent.arrayofvertstop.Length, 5);

                                                        somechunkadjacent.cleararrays();
                                                    }
                                                    sccslevelgen.arraychunkdatalod0[5][realindexofchunkinbundle].vertexcount = somechunkadjacent.arrayofvertstop.Length;

                                                    realindexofchunkinbundle++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //YYYYYYYYYYYYYYYYYYYYYY

                //ADJACENT CHUNKS 
                //ADJACENT CHUNKS 
                //ADJACENT CHUNKS 











































































                //2022 june 20th notes:
                //set the maps into the integers map buffers.
                //get the vertex number out of the modified now broken face.
                //remove this current instance from it's earlier association to a mesh of another number of vertex. lets say it had 8 vertex so 2 faces. but we broke 1 face. 
                //associate this map/chunk to the proper mesh instance that has the correct number of vertex. so we sort the 8 vertex chunk into a 4 vertex chunk because we broke 1 face.
                //create a new instance for the new brokwn type of mesh already existing in the 64 types of mesh, but not enough instances are spawned.
                //write to buffer the corrections.










                /*
                somecubeaschunkinsttop.arraychunkdatalod0[somearrayindex].arrayofverts = somechunk.vertexlist.ToArray();
                somecubeaschunkinsttop.arraychunkdatalod0[somearrayindex].arrayofindices = somechunk.listOfTriangleIndices.ToArray();

                somecubeaschunkinsttop.arraychunkdatalod0[somearrayindex].vertexBuffer.Dispose();
                somecubeaschunkinsttop.arraychunkdatalod0[somearrayindex].vertexBuffer = SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.VertexBuffer, somecubeaschunkinsttop.arrayOfChunkDatalod0[somearrayindex].arrayofverts);

                somecubeaschunkinsttop.arraychunkdatalod0[somearrayindex].IndicesBuffer.Dispose();
                somecubeaschunkinsttop.arraychunkdatalod0[somearrayindex].IndicesBuffer = SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.IndexBuffer, somecubeaschunkinsttop.arrayOfChunkDatalod0[somearrayindex].arrayofindices);// SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.IndexBuffer, somecubeaschunkinsttop.arrayOfChunkData[somearrayindex].arrayOfInstanceIndices[somearrayindex]);
                */




                //var somechunk = (sccstriglevelchunk)sclevelgenchunk.thesclevelgenchunk.getChunk(sometotaltimesx, sometotaltimesy, sometotaltimesz);
                /*var somechunk = (sclevelgenvert)somelevelgenprim[0].getChunklod0(sometotaltimesx, sometotaltimesy, sometotaltimesz, out somearrayindex);


                if (somechunk != null)
                {
                    if (somechunk.map != null)
                    {
                        if (someindexmap >= 0 && someindexmap < 10 * 10 * 10)
                        {
                            //Console.WriteLine("byte:" + somechunk.map[someindexmap]);

                            if (somechunk.map[someindexmap] == 1)
                            {

                                somechunk.map[someindexmap] = 0;

                                //somechunk.startBuildingArray();
                                //somechunk.sccsSetMapMOD();
                                //somechunk.RegenerateNotReduced();
                                somechunk.sccsSetMap();
                                somechunk.Regenerate();

                                somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindex].arrayofverts = somechunk.vertexlist.ToArray();
                                somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindex].arrayofindices = somechunk.listOfTriangleIndices.ToArray();

                                somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindex].vertexBuffer.Dispose();
                                somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindex].vertexBuffer = SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.VertexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindex].arrayofverts);

                                somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindex].IndicesBuffer.Dispose();
                                somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindex].IndicesBuffer = SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.IndexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindex].arrayofindices);// SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.IndexBuffer, somelevelgenprim[0].arrayOfChunkData[somearrayindex].arrayOfInstanceIndices[somearrayindex]);
                            }
                            somechunk.cleararrays();
                        }
                    }
                }


                //////////////////////ADJACENT CHUNK LOD 0 
                //////////////////////ADJACENT CHUNK LOD 0 
                //////////////////////ADJACENT CHUNK LOD 0 

                var somewidth = 10 - 1;
                var someheight = 10 - 1;
                var somedepth = 10 - 1;

                if (indexx == 0)
                {
                    int somearrayindexadjacent;
                    var somechunkadjacent = (sclevelgenvert)somelevelgenprim[0].getChunklod0(sometotaltimesx - 1.0f, sometotaltimesy, sometotaltimesz, out somearrayindexadjacent);
                    if (somechunkadjacent != null)
                    {
                        if (somechunkadjacent.map != null)
                        {
                            someindexmap = (somewidth) + 10 * ((indexy) + 10 * indexz);

                            if (someindexmap >= 0 && someindexmap < 10 * 10 * 10)
                            {
                                if (somechunkadjacent.map[someindexmap] == 1)
                                {
                                    somechunkadjacent.sccsSetMap();
                                    somechunkadjacent.Regenerate();
                                    if (somechunkadjacent.vertexlist.Count > 0)
                                    {
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofverts = somechunkadjacent.vertexlist.ToArray();
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofindices = somechunkadjacent.listOfTriangleIndices.ToArray();

                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].vertexBuffer.Dispose();
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].vertexBuffer = SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.VertexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofverts);

                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].IndicesBuffer.Dispose();
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].IndicesBuffer = SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.IndexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofindices);// SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.IndexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindex].arrayOfInstanceIndices[somearrayindex]);
                                    }
                                    somechunkadjacent.cleararrays();

                                }
                            }
                        }
                    }
                }

                if (indexx == somewidth)
                {
                    int somearrayindexadjacent;
                    var somechunkadjacent = (sclevelgenvert)somelevelgenprim[0].getChunklod0(sometotaltimesx + 1.0f, sometotaltimesy, sometotaltimesz, out somearrayindexadjacent);
                    if (somechunkadjacent != null)
                    {
                        if (somechunkadjacent.map != null)
                        {
                            someindexmap = (0) + 10 * ((indexy) + 10 * indexz);

                            if (someindexmap >= 0 && someindexmap < 10 * 10 * 10)
                            {
                                if (somechunkadjacent.map[someindexmap] == 1)
                                {
                                    somechunkadjacent.sccsSetMap();
                                    somechunkadjacent.Regenerate();
                                    if (somechunkadjacent.vertexlist.Count > 0)
                                    {
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofverts = somechunkadjacent.vertexlist.ToArray();
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofindices = somechunkadjacent.listOfTriangleIndices.ToArray();

                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].vertexBuffer.Dispose();
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].vertexBuffer = SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.VertexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofverts);

                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].IndicesBuffer.Dispose();
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].IndicesBuffer = SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.IndexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofindices);// SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.IndexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindex].arrayOfInstanceIndices[somearrayindex]);
                                    }
                                    somechunkadjacent.cleararrays();
                                }
                            }
                        }
                    }
                }





                if (indexz == 0)
                {
                    int somearrayindexadjacent;
                    var somechunkadjacent = (sclevelgenvert)somelevelgenprim[0].getChunklod0(sometotaltimesx, sometotaltimesy, sometotaltimesz - 1.0f, out somearrayindexadjacent);
                    if (somechunkadjacent != null)
                    {
                        if (somechunkadjacent.map != null)
                        {
                            someindexmap = (indexx) + 10 * ((indexy) + 10 * somedepth);

                            if (someindexmap >= 0 && someindexmap < 10 * 10 * 10)
                            {
                                if (somechunkadjacent.map[someindexmap] == 1)
                                {
                                    somechunkadjacent.sccsSetMap();
                                    somechunkadjacent.Regenerate();
                                    if (somechunkadjacent.vertexlist.Count > 0)
                                    {
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofverts = somechunkadjacent.vertexlist.ToArray();
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofindices = somechunkadjacent.listOfTriangleIndices.ToArray();

                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].vertexBuffer.Dispose();
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].vertexBuffer = SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.VertexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofverts);

                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].IndicesBuffer.Dispose();
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].IndicesBuffer = SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.IndexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofindices);// SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.IndexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindex].arrayOfInstanceIndices[somearrayindex]);
                                    }
                                    somechunkadjacent.cleararrays();
                                }
                            }
                        }
                    }
                }

                if (indexz == somedepth)
                {
                    int somearrayindexadjacent;
                    var somechunkadjacent = (sclevelgenvert)somelevelgenprim[0].getChunklod0(sometotaltimesx, sometotaltimesy, sometotaltimesz + 1.0f, out somearrayindexadjacent);
                    if (somechunkadjacent != null)
                    {
                        if (somechunkadjacent.map != null)
                        {
                            someindexmap = (indexx) + 10 * ((indexy) + 10 * 0);

                            if (someindexmap >= 0 && someindexmap < 10 * 10 * 10)
                            {
                                if (somechunkadjacent.map[someindexmap] == 1)
                                {
                                    somechunkadjacent.sccsSetMap();
                                    somechunkadjacent.Regenerate();
                                    if (somechunkadjacent.vertexlist.Count > 0)
                                    {
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofverts = somechunkadjacent.vertexlist.ToArray();
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofindices = somechunkadjacent.listOfTriangleIndices.ToArray();

                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].vertexBuffer.Dispose();
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].vertexBuffer = SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.VertexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofverts);

                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].IndicesBuffer.Dispose();
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].IndicesBuffer = SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.IndexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofindices);// SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.IndexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindex].arrayOfInstanceIndices[somearrayindex]);
                                    }
                                    somechunkadjacent.cleararrays();
                                }
                            }
                        }
                    }
                }

                if (indexy == 0)
                {
                    int somearrayindexadjacent;
                    var somechunkadjacent = (sclevelgenvert)somelevelgenprim[0].getChunklod0(sometotaltimesx, sometotaltimesy - 1.0f, sometotaltimesz, out somearrayindexadjacent);
                    if (somechunkadjacent != null)
                    {
                        if (somechunkadjacent.map != null)
                        {
                            someindexmap = (indexx) + 10 * ((someheight) + 10 * indexz);

                            if (someindexmap >= 0 && someindexmap < 10 * 10 * 10)
                            {
                                if (somechunkadjacent.map[someindexmap] == 1)
                                {
                                    somechunkadjacent.sccsSetMap();
                                    somechunkadjacent.Regenerate();
                                    if (somechunkadjacent.vertexlist.Count > 0)
                                    {
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofverts = somechunkadjacent.vertexlist.ToArray();
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofindices = somechunkadjacent.listOfTriangleIndices.ToArray();

                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].vertexBuffer.Dispose();
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].vertexBuffer = SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.VertexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofverts);

                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].IndicesBuffer.Dispose();
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].IndicesBuffer = SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.IndexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofindices);// SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.IndexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindex].arrayOfInstanceIndices[somearrayindex]);
                                    }
                                    somechunkadjacent.cleararrays();
                                }
                            }
                        }
                    }
                }

                if (indexy == someheight)
                {
                    int somearrayindexadjacent;
                    var somechunkadjacent = (sclevelgenvert)somelevelgenprim[0].getChunklod0(sometotaltimesx, sometotaltimesy + 1.0f, sometotaltimesz, out somearrayindexadjacent);
                    if (somechunkadjacent != null)
                    {
                        if (somechunkadjacent.map != null)
                        {
                            someindexmap = (indexx) + 10 * ((0) + 10 * indexz);

                            if (someindexmap >= 0 && someindexmap < 10 * 10 * 10)
                            {
                                if (somechunkadjacent.map[someindexmap] == 1)
                                {
                                    somechunkadjacent.sccsSetMap();
                                    somechunkadjacent.Regenerate();
                                    if (somechunkadjacent.vertexlist.Count > 0)
                                    {
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofverts = somechunkadjacent.vertexlist.ToArray();
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofindices = somechunkadjacent.listOfTriangleIndices.ToArray();

                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].vertexBuffer.Dispose();
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].vertexBuffer = SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.VertexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofverts);

                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].IndicesBuffer.Dispose();
                                        somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].IndicesBuffer = SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.IndexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindexadjacent].arrayofindices);// SharpDX.Direct3D11.Buffer.Create(directx.D3D.Device, BindFlags.IndexBuffer, somelevelgenprim[0].arrayOfChunkDatalod0[somearrayindex].arrayOfInstanceIndices[somearrayindex]);
                                    }
                                    somechunkadjacent.cleararrays();
                                }
                            }
                        }
                    }
                }
                //////////////////////ADJACENT CHUNK LOD 0 
                //////////////////////ADJACENT CHUNK LOD 0 
                //////////////////////ADJACENT CHUNK LOD 0 
                */






            }

            //return scgraphicssecpackagemessage.scjittertasks;
        }





        private void oculustouchcontrols() //
        {
            if (buttonPressedOculusTouchLeft == 1048576)
            {
                directx.D3D.OVR.RecenterTrackingOrigin(directx.D3D.sessionPtr);

            }
        }









        public void rendervoxels()
        {
            var viewmatrix = camera.ViewMatrix;// Matrix.LookAtLH(new Vector3(0, 0, -viewZ), new Vector3(0, 0, 0), Vector3.UnitY);
            var projectionMatrix = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, D3D.SurfaceWidth / (float)D3D.SurfaceHeight, 0.1f, 1000.0f);
            var viewProj = Matrix.Multiply(viewmatrix, D3D.ProjectionMatrix);


            try
            {
                //PHYSICS VOXEL CUBES 
                //////////////////////about 100 ticks more per loop compared to simple physics cubes? will investigate later as when i do 
                //////////////////////simple cubes with the chunk it lags more even though the number of vertices are the same as the physics cube up above
                //////////////////////todo: culling of faces by distance from player. etc.

                _world_voxel_cube_lists[0][0].Render(directx.D3D.Device.ImmediateContext);
                _shaderManager.RenderInstancedObjectsc_perko_voxel(directx.D3D.Device.ImmediateContext, _world_voxel_cube_lists[0][0].IndexCount, _world_voxel_cube_lists[0][0].InstanceCount, _world_voxel_cube_lists[0][0]._POSITION, viewmatrix, projectionMatrix, null, _DLightBuffer_voxel_cube, _world_voxel_cube_lists[0][0]);
                ///Console.WriteLine(_SystemTickPerformance.ElapsedTicks);
            }
            catch (Exception ex)
            {
                Program.MessageBox((IntPtr)0, ex.ToString() + "", "Oculus error", 0);
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



        public void writevoxelstobuffer()
        {
            Quaternion quat_buffers;
            //PHYSICS VOXEL SPHEROID
            _world_voxel_cube_lists[0][0]._WORLDMATRIXINSTANCES = worldMatrix_instances_voxel_cube[0][0];
            _world_voxel_cube_lists[0][0]._POSITION = worldMatrix_base[0];
            //END OF

            //PHYSICS VOXEL THINGS
            var voxel_cube = _world_voxel_cube_lists[0][0];
            var instances_voxel_cube = voxel_cube.instances;
            var _voxel_cube_Worldmatrix_of_instances = voxel_cube._WORLDMATRIXINSTANCES;

            for (int i = 0; i < instances_voxel_cube.Length; i++)
            {
                float xxx = _voxel_cube_Worldmatrix_of_instances[i].M41;
                float yyy = _voxel_cube_Worldmatrix_of_instances[i].M42;
                float zzz = _voxel_cube_Worldmatrix_of_instances[i].M43;

                voxel_cube.instances[i].position.X = xxx;
                voxel_cube.instances[i].position.Y = yyy;
                voxel_cube.instances[i].position.Z = zzz;
                voxel_cube.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref _voxel_cube_Worldmatrix_of_instances[i], out quat_buffers);

                var dirInstance = sc_maths._newgetdirforward(quat_buffers);
                voxel_cube.instancesDataForward[i].rotation.X = dirInstance.X;
                voxel_cube.instancesDataForward[i].rotation.Y = dirInstance.Y;
                voxel_cube.instancesDataForward[i].rotation.Z = dirInstance.Z;
                voxel_cube.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(quat_buffers);
                voxel_cube.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                voxel_cube.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                voxel_cube.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                voxel_cube.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = sc_maths._newgetdirup(quat_buffers);
                voxel_cube.instancesDataUP[i].rotation.X = dirInstance.X;
                voxel_cube.instancesDataUP[i].rotation.Y = dirInstance.Y;
                voxel_cube.instancesDataUP[i].rotation.Z = dirInstance.Z;
                voxel_cube.instancesDataUP[i].rotation.W = 1;
            }
            //END OF
        }



        public void writecubestobuffer()
        {


            //PHYSICS SCREEN ASSETS
            worldlevelgenbytesassets[0][0]._WORLDMATRIXINSTANCES = matrixchangelevelgenbytes[0][0];
            worldlevelgenbytesassets[0][0]._POSITION = worldMatrix_base[0];
            //END OF


            Quaternion _testQuater;
            //PHYSICS SCREEN ASSETS
            var cuber = worldlevelgenbytesassets[0][0];
            var instancers = cuber.instances;
            var sometester = cuber._WORLDMATRIXINSTANCES;

            for (int i = 0; i < instancers.Length; i++)
            {
                var xxx = sometester[i].M41;
                var yyy = sometester[i].M42;
                var zzz = sometester[i].M43;

                cuber.instances[i].position.X = xxx;
                cuber.instances[i].position.Y = yyy;
                cuber.instances[i].position.Z = zzz;
                cuber.instances[i].position.W = 1;
                Quaternion.RotationMatrix(ref sometester[i], out _testQuater);

                var dirInstance = sc_maths._newgetdirforward(_testQuater);
                cuber.instancesDataForward[i].rotation.X = dirInstance.X;
                cuber.instancesDataForward[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataForward[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataForward[i].rotation.W = 1;

                dirInstance = -sc_maths._newgetdirleft(_testQuater);
                cuber.instancesDataRIGHT[i].rotation.X = dirInstance.X;
                cuber.instancesDataRIGHT[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataRIGHT[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataRIGHT[i].rotation.W = 1;

                dirInstance = dirInstance = sc_maths._newgetdirup(_testQuater);
                cuber.instancesDataUP[i].rotation.X = dirInstance.X;
                cuber.instancesDataUP[i].rotation.Y = dirInstance.Y;
                cuber.instancesDataUP[i].rotation.Z = dirInstance.Z;
                cuber.instancesDataUP[i].rotation.W = 1;
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
