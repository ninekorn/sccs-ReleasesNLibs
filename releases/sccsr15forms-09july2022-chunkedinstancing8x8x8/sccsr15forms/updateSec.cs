﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

using DSharpDXRastertek.Tut16.Graphics.Data;

using Ab3d.OculusWrap;
using Ab3d.OculusWrap.DemoDX11;


namespace sccsr15forms
{
    public class updateSec : IDisposable
    {

        public Vector3 lookUp;
        public Vector3 lookAt;

        /*public struct worldviewproj
        {
            public Matrix worldmatrix ;
            public Matrix viewmatrix ;
            public Matrix projectionmatrix;
        }*/

        

        Quaternion quatt;
        public static Vector4 dirikvoxelbodyInstanceRight0;
        public static Vector4 dirikvoxelbodyInstanceUp0;
        public static Vector4 dirikvoxelbodyInstanceForward0;
        public scgraphicssecpackage scgraphicssecpackagemessage;

        Matrix tempmatter;

        public static float _planeSize;
      

















      


        double displayMidpoint;
        TrackingState trackingState;
        Posef[] eyePoses;
        EyeType eye;
        //EyeTexture eyeTexture;
        bool latencyMark = false;
        TrackingState trackState;
        PoseStatef poseStatefer;
        Posef hmdPose;
        Quaternionf hmdRot;
        Vector3 _hmdPoser;
        Quaternion _hmdRoter;
       















        //SHARPDX DEFERRED RENDERING VARIABLES
        //SHARPDX DEFERRED RENDERING VARIABLES
        //SHARPDX DEFERRED RENDERING VARIABLES
        //public State currentState;
        //public State nextState;
        public SharpDX.Direct3D11.DeviceContext[] deferredContexts;
        public SharpDX.Direct3D11.DeviceContext[] contextPerThread;
        public SharpDX.Direct3D11.CommandList[] commandsList;
        public SharpDX.Direct3D11.CommandList[] frozenCommandLists;
        public bool supportConcurentResources;
        public bool supportCommandList;
        public enum TestType
        {
            Immediate = 0,
            Deferred = 1,
            FrozenDeferred = 2
        }
        public struct State
        {
            public bool Exit;
            public int CountCubes;
            public int ThreadCount;
            public TestType Type;
            public bool SimulateCpuUsage;
            public bool UseMap;
        }
        public int MaxNumberOfCubes = 1024;
        public const int MaxNumberOfThreads = 16;
        public const int BurnCpuFactor = 50;
        //SHARPDX DEFERRED RENDERING VARIABLES
        //SHARPDX DEFERRED RENDERING VARIABLES
        //SHARPDX DEFERRED RENDERING VARIABLES






        Task[] arrayoftasks;



        int threadswitch = 0;
        int threadswitchtwo = 0;

        Thread main_thread_update;
        Stopwatch cullingwatch = new Stopwatch();
        DataStream mappedResourceLight;

        DFrustum dfrustrum = new DFrustum();

        public int totalmeshdistculled = 0;
        public int totalmeshfrustculled = 0;


        public int totalmesh;
        public int totalvert;
        public int totaltrigs;

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




        public static updateSec currentupdatesec;// = Instance;

        int counternumberofchunks = 0;

        /*public tutorialcubeaschunkinst somecubeaschunkinsttop;
        public tutorialcubeaschunkinst somecubeaschunkinstleft;
        public tutorialcubeaschunkinst somecubeaschunkinstright;
        public tutorialcubeaschunkinst somecubeaschunkinstfront;
        public tutorialcubeaschunkinst somecubeaschunkinstback;
        public tutorialcubeaschunkinst somecubeaschunkinstbottom;*/

        //public tutorialcubeaschunk somecubeaschunk;
        /*public tutorialcubeaschunk somecubeaschunk;
        public tutorialcubeaschunkright somecubeaschunkright;
        public tutorialcubeaschunktop somecubeaschunktop;
        public tutorialcubeaschunkbottom somecubeaschunkbottom;
        public tutorialcubeaschunkfront somecubeaschunkfront;
        public tutorialcubeaschunkback somecubeaschunkback;*/

        public tutorialcube somecube;
        //public sctutorialobj tutorialobj;


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

        public int somelevelgenprimw = 1;
        public int somelevelgenprimh = 1;
        public int somelevelgenprimd = 1;
        //public sclevelgenclassPrim[] somelevelgenprim;
        //public sclevelgenglobals somelevelgenprimglobals;

        int activatelevelgen = 1;
        float RotationInstScreenx = 0;
        float RotationInstScreeny = 0;
        float RotationInstScreenz = 0;
        float levelgenplanesize = 1.0f;

        Vector4 ambientColor = new Vector4(0.45f, 0.45f, 0.45f, 1.0f);
        Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
        public State currentState;
        public State nextState;


        /*const float minlod0 = 4.0f;

        const float minlod1 = 4.0f;
        const float maxlod1 = 6.0f;

        const float minlod2 = 6.0f;
        const float maxlod2 = 8.0f;

        const float minlod3 = 8.0f;
        const float maxlod3 = 10.0f;

        const float minlod4 = 10.0f;
        const float maxlod4 = 12.0f;
        float maindist = maxlod4;*/


        /*const float minlod0 = 4.0f;

        const float minlod1 = 4.0f;
        const float maxlod1 = 6.0f;

        const float minlod2 = 6.0f;
        const float maxlod2 = 8.0f;

        const float minlod3 = 8.0f;
        const float maxlod3 = 10.0f;

        const float minlod4 = 10.0f;
        const float maxlod4 = 12.0f;
        float maindist = maxlod4;*/


        /*const float minlod0 = 4.0f;

        const float minlod1 = 4.0f;
        const float maxlod1 = 6.0f;

        const float minlod2 = 6.0f;
        const float maxlod2 = 12.0f;

        const float minlod3 = 12.0f;
        const float maxlod3 = 40.0f;

        const float minlod4 = 40.0f;
        const float maxlod4 = 80.0f;
        float maindist = maxlod4;*/


        /*const float minlod0 = 4.0f;

        const float minlod1 = 4.0f;
        const float maxlod1 = 12.0f;

        const float minlod2 = 12.0f;
        const float maxlod2 = 20.0f;

        const float minlod3 = 20.0f;
        const float maxlod3 = 40.0f;

        const float minlod4 = 40.0f;
        const float maxlod4 = 80.0f;
        float maindist = maxlod4;*/



        /*const float minlod0 = 3.0f;

        const float minlod1 = 3.0f;
        const float maxlod1 = 8.0f;

        const float minlod2 = 8.0f;
        const float maxlod2 = 16.0f;

        const float minlod3 = 16.0f;
        const float maxlod3 = 20.0f;

        const float minlod4 = 20.0f;
        const float maxlod4 = 80.0f;
        float maindist = maxlod4;*/


        const float minlod0 = 3.0f;

        const float minlod1 = 3.0f;
        const float maxlod1 = 6.0f;

        const float minlod2 = 6.0f;
        const float maxlod2 = 10.0f;

        const float minlod3 = 10.0f;
        const float maxlod3 = 20.0f;

        const float minlod4 = 20.0f;
        const float maxlod4 = 80.0f;
        float maindist = maxlod4;



        public updateSec(updatePrim updateprim_, directx D3D_)
        {
            D3D = D3D_;
            currentupdatesec = this;
            updateprim = updateprim_;



           // _shaderManager = new SC_ShaderManager();
           // _shaderManager.Initialize(D3D.Device, Program.consoleHandle, Program.createikrig);

            scgraphicssecpackagemessage = new scgraphicssecpackage();




            Console.WriteLine("created updatesec");


            somecube = new tutorialcube(D3D.Device);
            //tutorialobj = new sctutorialobj(D3D.Device);
            //somecubeaschunk = new tutorialcubeaschunk(D3D.Device);


            /*sccslevelgen sccslevelgen = new sccslevelgen();
            sccslevelgen.createlevel();

            int facetype = 0;
            somecubeaschunkinsttop = new tutorialcubeaschunkinst(D3D.Device,facetype, sccslevelgen);

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

            _planeSize = somecubeaschunkinsttop.somelevelgenprimglobals.planeSize;
            */






















            /*somecubeaschunkright = new tutorialcubeaschunkright(D3D.Device);
            somecubeaschunktop = new tutorialcubeaschunktop(D3D.Device);
            somecubeaschunkbottom = new tutorialcubeaschunkbottom(D3D.Device);
            somecubeaschunkfront = new tutorialcubeaschunkfront(D3D.Device);
            somecubeaschunkback = new tutorialcubeaschunkback(D3D.Device);*/


            /*
            for (int i = 0;i < somecubeaschunk.arraychunkdatalod0.Length;i++)
            {
                if (somecubeaschunk.arraychunkdatalod0[i].arraychunkvertslod0.arrayofverts.Length > 0)
                {
                    countmeshtotallod0++;
                    countmeshtrigtotallod0 += somecubeaschunk.arraychunkdatalod0[i].arraychunkvertslod0.arrayoftrigs.Length;
                    countmeshverttotallod0 += somecubeaschunk.arraychunkdatalod0[i].arraychunkvertslod0.arrayofverts.Length;
                }
            }*/



            /*
            for (int i = 0; i < somecubeaschunk.arraychunkdatalod1.Length; i++)
            {
                if (somecubeaschunk.arraychunkdatalod1[i].arraychunkvertslod1.arrayofverts.Length > 0)
                {
                    countmeshtotallod1++;
                    countmeshtrigtotallod1 += somecubeaschunk.arraychunkdatalod1[i].arraychunkvertslod1.arrayoftrigs.Length;
                    countmeshverttotallod1 += somecubeaschunk.arraychunkdatalod1[i].arraychunkvertslod1.arrayofverts.Length;
                }
            }
            for (int i = 0; i < somecubeaschunk.arraychunkdatalod2.Length; i++)
            {
                if (somecubeaschunk.arraychunkdatalod2[i].arraychunkvertslod2.arrayofverts.Length > 0)
                {
                    countmeshtotallod2++;
                    countmeshtrigtotallod2 += somecubeaschunk.arraychunkdatalod2[i].arraychunkvertslod2.arrayoftrigs.Length;
                    countmeshverttotallod2 += somecubeaschunk.arraychunkdatalod2[i].arraychunkvertslod2.arrayofverts.Length;
                }
            }
            for (int i = 0; i < somecubeaschunk.arraychunkdatalod3.Length; i++)
            {
                if (somecubeaschunk.arraychunkdatalod3[i].arraychunkvertslod3.arrayofverts.Length > 0)
                {
                    countmeshtotallod3++;
                    countmeshtrigtotallod3 += somecubeaschunk.arraychunkdatalod3[i].arraychunkvertslod3.arrayoftrigs.Length;
                    countmeshverttotallod3 += somecubeaschunk.arraychunkdatalod3[i].arraychunkvertslod3.arrayofverts.Length;
                }
            }
            for (int i = 0; i < somecubeaschunk.arraychunkdatalod4.Length; i++)
            {
                if (somecubeaschunk.arraychunkdatalod4[i].arraychunkvertslod4.arrayofverts.Length > 0)
                {
                    countmeshtotallod4++;
                    countmeshtrigtotallod4 += somecubeaschunk.arraychunkdatalod4[i].arraychunkvertslod4.arrayoftrigs.Length;
                    countmeshverttotallod4 += somecubeaschunk.arraychunkdatalod4[i].arraychunkvertslod4.arrayofverts.Length;
                }
            }*/


            totalmesh = countmeshtotallod0 + countmeshtotallod1 + countmeshtotallod2 + countmeshtotallod3 + countmeshtotallod4;
            totalvert = countmeshverttotallod4 + countmeshverttotallod1 + countmeshverttotallod2 + countmeshverttotallod3 + countmeshverttotallod4;
            totaltrigs = countmeshtrigtotallod0 + countmeshtrigtotallod1 + countmeshtrigtotallod2 + countmeshtrigtotallod3 + countmeshtrigtotallod4;




            cullingwatch.Start();



            deferredContexts = new SharpDX.Direct3D11.DeviceContext[MaxNumberOfThreads];
            for (int i = 0; i < deferredContexts.Length; i++)
            {
                deferredContexts[i] = new SharpDX.Direct3D11.DeviceContext(D3D.Device);
            }
            contextPerThread = new SharpDX.Direct3D11.DeviceContext[MaxNumberOfThreads];
            contextPerThread[0] = D3D.Device.ImmediateContext;
            commandsList = new SharpDX.Direct3D11.CommandList[MaxNumberOfThreads];
            frozenCommandLists = null;


            D3D.Device.CheckThreadingSupport(out supportConcurentResources, out supportCommandList);


            Console.WriteLine("supportConcurentResources:" + supportConcurentResources + "/supportCommandList:" + supportCommandList);


            //somecubeaschunk.arraychunkdatalod0.Length
            currentState = new State
            {
                CountCubes = 64, // 64
                ThreadCount = 4,
                Type = TestType.Deferred,
                SimulateCpuUsage = false,
                UseMap = true
            };
            nextState = currentState;

            //MaxNumberOfCubes = somecubeaschunk.arraychunkdatalod0.Length;


            if (activatelevelgen == 1)
            {

            }

            //currentState.CountCubes = somelevelgenprim[0].arraychunkdatalod0.Length;

            //movePos = updateprim_.camera.GetPosition();

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
                if (currentState.Type != TestType.Immediate)
                {
                    threadCount = currentState.Type == TestType.Deferred ? currentState.ThreadCount : 1;
                    Array.Copy(deferredContexts, contextPerThread, contextPerThread.Length);
                }
                else
                {
                    contextPerThread[0] = D3D.DeviceContext;
                }


       
                /*
                for (int chunkindex = 0; chunkindex < somecubeaschunk.arraychunkdatalod0.Length; chunkindex++)
                {
                    for (int i = 0; i < threadCount; i++)
                    {
                        var renderingContext = D3D.contextPerThread[i];
                        renderingContext.InputAssembler.InputLayout = somecubeaschunk.arraychunkdatalod0[chunkindex].layout;
                        renderingContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
                        renderingContext.InputAssembler.SetIndexBuffer(somecubeaschunk.arraychunkdatalod0[chunkindex].indicesbuffer, SharpDX.DXGI.Format.R32_UInt, 0);

                        //renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecubeaschunk.arraychunkdatalod0[chunkindex].verticesbuffer, Utilities.SizeOf<tutorialcubeaschunk.DVertex>(), 0));
                        renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecubeaschunk.arraychunkdatalod0[chunkindex].verticesbuffer, Utilities.SizeOf<Vector4>() * 2, 0));
                        renderingContext.VertexShader.SetConstantBuffer(0, currentState.UseMap ? somecubeaschunk.arraychunkdatalod0[chunkindex].dynamicConstantBuffer : somecubeaschunk.arraychunkdatalod0[chunkindex].staticContantBuffer);

                        renderingContext.VertexShader.Set(somecubeaschunk.arraychunkdatalod0[chunkindex].vertexShader);
                        renderingContext.Rasterizer.SetViewport(0, 0, D3D.SurfaceWidth, D3D.SurfaceHeight);
                        renderingContext.PixelShader.Set(somecubeaschunk.arraychunkdatalod0[chunkindex].pixelShader);
                        renderingContext.OutputMerger.SetTargets(D3D.DepthStencilView, D3D.RenderTargetView);
                    }
                }*/






                /*
                for (int i = 0; i < threadCount; i++)
                {
                    var renderingContext = D3D.contextPerThread[i];
                    // Prepare All the stages 
                  
                    renderingContext.InputAssembler.InputLayout = somecubeaschunk.arraychunkdatalod0[0].layout;
                    renderingContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
                    renderingContext.InputAssembler.SetIndexBuffer(somecubeaschunk.arraychunkdatalod0[0].indicesbuffer, SharpDX.DXGI.Format.R32_UInt, 0);

                    //renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecubeaschunk.arraychunkdatalod0[0].verticesbuffer, Utilities.SizeOf<tutorialcubeaschunk.DVertex>(), 0));
                    renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecubeaschunk.arraychunkdatalod0[0].verticesbuffer, Utilities.SizeOf<Vector4>()* 2, 0));
                    renderingContext.VertexShader.SetConstantBuffer(0, currentState.UseMap ? somecubeaschunk.arraychunkdatalod0[0].dynamicConstantBuffer : somecubeaschunk.arraychunkdatalod0[0].staticContantBuffer);
                    
                    
                    renderingContext.VertexShader.Set(somecubeaschunk.arraychunkdatalod0[0].vertexShader);
                    renderingContext.Rasterizer.SetViewport(0, 0, D3D.SurfaceWidth, D3D.SurfaceHeight);
                    renderingContext.PixelShader.Set(somecubeaschunk.arraychunkdatalod0[0].pixelShader);
                    renderingContext.OutputMerger.SetTargets(D3D.DepthStencilView, D3D.RenderTargetView);
                    


                    /*
                    renderingContext.InputAssembler.InputLayout = somecube.layout;
                    renderingContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;

                    renderingContext.InputAssembler.SetIndexBuffer(somecube.IndicesBuffer, SharpDX.DXGI.Format.R32_UInt, 0);
                    renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecube.verticesbuffer, Utilities.SizeOf<Vector4>()*2, 0));

                    renderingContext.VertexShader.SetConstantBuffer(0, currentState.UseMap ? somecube.dynamicConstantBuffer : somecube.staticContantBuffer);
                    renderingContext.VertexShader.Set(somecube.vertexShader);
                    renderingContext.Rasterizer.SetViewport(0, 0, D3D.SurfaceWidth, D3D.SurfaceHeight);
                    renderingContext.PixelShader.Set(somecube.pixelShader);
                    renderingContext.OutputMerger.SetTargets(D3D.DepthStencilView, D3D.RenderTargetView);
                    

                }*/
                hasfinishedSetupPipeline = 1;
            };





            renderrow = (contextIndex, fromY, toY) =>
            {

               // someactionloop:


                hasfinishedRenderRow = 0;
                var renderingContext = contextPerThread[contextIndex];
                var time = Program.clock.ElapsedMilliseconds / 1000.0f;




                //Console.WriteLine(contextIndex);
                if (contextIndex == 0)
                {
                    
                    contextPerThread[0].ClearDepthStencilView(D3D.DepthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);
                    contextPerThread[0].ClearRenderTargetView(D3D.RenderTargetView, SharpDX.Color.LightGray);
                    //Console.WriteLine("clear");

                }

             



                var view = updateprim.camera.ViewMatrix;// Matrix.LookAtLH(new Vector3(0, 0, -viewZ), new Vector3(0, 0, 0), Vector3.UnitY);
                var proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, D3D.SurfaceWidth / (float)D3D.SurfaceHeight, 0.1f, 1000.0f);
                var viewProj = Matrix.Multiply(view, D3D.ProjectionMatrix);

                

                //var view = Matrix.Identity;
                //var proj = Matrix.Identity;
                //var viewProj = Matrix.Identity;














                //int count = currentState.CountCubes;
                //float divCubes = (float)count / (viewZ - 1);
                //Matrix.Scaling(1.0f / count) *
                var rotateMatrix =  Matrix.Identity; //Matrix.Scaling(1.0f / count) * Matrix.RotationX(time) * Matrix.RotationY(time * 2) * Matrix.RotationZ(time * .7f);  /// Matrix.Identity; //

                for (int y = fromY; y < toY; y++)
                {
                    int chunkindex = y;
                    //rotateMatrix.M41 = (x + .5f - count * .5f) / divCubes;
                    //rotateMatrix.M42 = (y + .5f - count * .5f) / divCubes;

                    // Update WorldViewProj Matrix 

                    // Simulate CPU usage in order to see benefits of worlViewProj
                    Matrix worldViewProj;
                    Matrix.Multiply(ref rotateMatrix, ref viewProj, out worldViewProj);
                    worldViewProj.Transpose();
                    if (currentState.SimulateCpuUsage)
                    {
                        for (int i = 0; i < BurnCpuFactor; i++)
                        {
                            Matrix.Multiply(ref rotateMatrix, ref viewProj, out worldViewProj);
                            worldViewProj.Transpose();
                        }
                    }





                    /*
                    if (somecubeaschunk != null)
                    {

                        somecubeaschunk.lightBuffer[0].lightPosition = new Vector3(0, 1, 0);


                        /*somecubeaschunk.lightBuffer[0].lightPosition = new Vector3(0, 1, 0);
                        renderingContext.MapSubresource(somecubeaschunk.ConstantLightBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                        mappedResourceLight.WriteRange(somecubeaschunk.lightBuffer, 0, somecubeaschunk.lightBuffer.Length);
                        renderingContext.UnmapSubresource(somecubeaschunk.ConstantLightBuffer, 0);
                        mappedResourceLight.Dispose();*/


                        //distsquaredlod0 = somecubeaschunk.arraychunkdatalod0[chunkindex].distanceculling;
                        //frustrumculllod0 = somecubeaschunk.arraychunkdatalod0[chunkindex].frustrumculldraw;



                        /*
                        if (somecubeaschunk.arraychunkdatalod1 != null)
                        {
                            if (somecubeaschunk.arraychunkdatalod1[chunkindex].arraychunkvertslod1 != null)
                            {
                                if (somecubeaschunk.arraychunkdatalod1[chunkindex].arraychunkvertslod1.arrayofverts != null)
                                {
                                    if (somecubeaschunk.arraychunkdatalod1[chunkindex].arraychunkvertslod1.arrayofverts.Length > 0)
                                    {
                                        //distsquaredlod1 = somecubeaschunk.arraychunkdatalod1[chunkindex].distanceculling;
                                        //frustrumculllod1 = somecubeaschunk.arraychunkdatalod1[chunkindex].frustrumculldraw;

                                        if (somecubeaschunk.arraychunkdatalod1[chunkindex].distanceculling >= minlod1 && somecubeaschunk.arraychunkdatalod1[chunkindex].distanceculling < maxlod1 && somecubeaschunk.arraychunkdatalod1[chunkindex].frustrumculldraw )
                                        {
                                            //if (frustrumculllod0)
                                            {
                                                 renderingContext.InputAssembler.InputLayout = somecubeaschunk.arraychunkdatalod1[chunkindex].layout;
                                                renderingContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
                                                renderingContext.InputAssembler.SetIndexBuffer(somecubeaschunk.arraychunkdatalod1[chunkindex].indicesbuffer, SharpDX.DXGI.Format.R32_UInt, 0);

                                                //renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecubeaschunk.arraychunkdatalod1[chunkindex].verticesbuffer, Utilities.SizeOf<tutorialcubeaschunk.DVertex>(), 0));
                                                renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecubeaschunk.arraychunkdatalod1[chunkindex].verticesbuffer, Utilities.SizeOf<tutorialcubeaschunk.DVertex>(), 0));
                                                renderingContext.VertexShader.SetConstantBuffer(0, currentState.UseMap ? somecubeaschunk.arraychunkdatalod1[chunkindex].dynamicConstantBuffer : somecubeaschunk.arraychunkdatalod1[chunkindex].staticContantBuffer);
                                                renderingContext.PixelShader.SetConstantBuffer(1, somecubeaschunk.ConstantLightBuffer);

                                                renderingContext.VertexShader.Set(somecubeaschunk.arraychunkdatalod1[chunkindex].vertexShader);
                                                renderingContext.Rasterizer.SetViewport(0, 0, D3D.SurfaceWidth, D3D.SurfaceHeight);
                                                renderingContext.PixelShader.Set(somecubeaschunk.arraychunkdatalod1[chunkindex].pixelShader);
                                                renderingContext.OutputMerger.SetTargets(D3D.DepthStencilView, D3D.RenderTargetView);

                                                if (currentState.UseMap)
                                                {

                                                    var dataBox0 = renderingContext.MapSubresource(somecubeaschunk.ConstantLightBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
                                                    Utilities.Write(dataBox0.DataPointer, ref somecubeaschunk.lightBuffer[0]);
                                                    renderingContext.UnmapSubresource(somecubeaschunk.ConstantLightBuffer, 0);

                                                    //somecubeaschunk.lightBuffer[0].lightPosition = new Vector3(0, 1, 0);
                                                    //renderingContext.MapSubresource(somecubeaschunk.ConstantLightBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResourceLight);
                                                    //mappedResourceLight.WriteRange(somecubeaschunk.lightBuffer, 0, somecubeaschunk.lightBuffer.Length);
                                                    //renderingContext.UnmapSubresource(somecubeaschunk.ConstantLightBuffer, 0);
                                                   // mappedResourceLight.Dispose();



                                                    var dataBox = renderingContext.MapSubresource(somecubeaschunk.arraychunkdatalod1[chunkindex].dynamicConstantBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
                                                    Utilities.Write(dataBox.DataPointer, ref worldViewProj);
                                                    renderingContext.UnmapSubresource(somecubeaschunk.arraychunkdatalod1[chunkindex].dynamicConstantBuffer, 0);
                                                }
                                                else
                                                {
                                                    renderingContext.UpdateSubresource(ref worldViewProj, somecubeaschunk.arraychunkdatalod1[chunkindex].staticContantBuffer);
                                                }

                                                // Draw the cube 
                                                //renderingContext.Draw(36, 0);
                                                renderingContext.DrawIndexed(somecubeaschunk.arraychunkdatalod1[chunkindex].arraychunkvertslod1.arrayoftrigs.Length, 0, 0);
                                                //renderingContext.Draw(somecubeaschunk.arraychunkdatalod1[chunkindex].arraychunkvertslod1.arraychunkvertslod1.Length, 0);
                                            }
                                        }
                                        else
                                        {
                                            if (somecubeaschunk.arraychunkdatalod1[chunkindex].distanceculling >= minlod1)
                                            {
                                                countmeshdistculledlod1++;
                                            }
                                            if (!somecubeaschunk.arraychunkdatalod1[chunkindex].frustrumculldraw)
                                            {
                                                countmeshfrustculledlod1++;
                                            }

                                            countmeshtrigculledlod1 += somecubeaschunk.arraychunkdatalod1[chunkindex].arraychunkvertslod1.arrayoftrigs.Length;
                                            countmeshvertculledlod1 += somecubeaschunk.arraychunkdatalod1[chunkindex].arraychunkvertslod1.arrayofverts.Length;
                                            countmeshculledlod1++;
                                        }
                                    }
                                }
                            }
                        }

                        
                        if (somecubeaschunk.arraychunkdatalod2 != null)
                        {
                            if (somecubeaschunk.arraychunkdatalod2[chunkindex].arraychunkvertslod2 != null)
                            {
                                if (somecubeaschunk.arraychunkdatalod2[chunkindex].arraychunkvertslod2.arrayofverts != null)
                                {
                                    if (somecubeaschunk.arraychunkdatalod2[chunkindex].arraychunkvertslod2.arrayofverts.Length > 0)
                                    {
                                        //distsquaredlod2 = somecubeaschunk.arraychunkdatalod2[chunkindex].distanceculling;
                                        //frustrumculllod2 = somecubeaschunk.arraychunkdatalod2[chunkindex].frustrumculldraw;

                                        if (somecubeaschunk.arraychunkdatalod2[chunkindex].distanceculling >= minlod2 && somecubeaschunk.arraychunkdatalod2[chunkindex].distanceculling < maxlod2 && somecubeaschunk.arraychunkdatalod2[chunkindex].frustrumculldraw)
                                        {
                                            //if (frustrumculllod0)
                                            {
                                                renderingContext.InputAssembler.InputLayout = somecubeaschunk.arraychunkdatalod2[chunkindex].layout;
                                                renderingContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
                                                renderingContext.InputAssembler.SetIndexBuffer(somecubeaschunk.arraychunkdatalod2[chunkindex].indicesbuffer, SharpDX.DXGI.Format.R32_UInt, 0);

                                                //renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecubeaschunk.arraychunkdatalod2[chunkindex].verticesbuffer, Utilities.SizeOf<tutorialcubeaschunk.DVertex>(), 0));
                                                renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecubeaschunk.arraychunkdatalod2[chunkindex].verticesbuffer, Utilities.SizeOf<tutorialcubeaschunk.DVertex>(), 0));
                                                renderingContext.VertexShader.SetConstantBuffer(0, currentState.UseMap ? somecubeaschunk.arraychunkdatalod2[chunkindex].dynamicConstantBuffer : somecubeaschunk.arraychunkdatalod2[chunkindex].staticContantBuffer);
                                                renderingContext.PixelShader.SetConstantBuffer(1, somecubeaschunk.ConstantLightBuffer);


                                                renderingContext.VertexShader.Set(somecubeaschunk.arraychunkdatalod2[chunkindex].vertexShader);
                                                renderingContext.Rasterizer.SetViewport(0, 0, D3D.SurfaceWidth, D3D.SurfaceHeight);
                                                renderingContext.PixelShader.Set(somecubeaschunk.arraychunkdatalod2[chunkindex].pixelShader);
                                                renderingContext.OutputMerger.SetTargets(D3D.DepthStencilView, D3D.RenderTargetView);

                                                if (currentState.UseMap)
                                                {
                                                    var dataBox0 = renderingContext.MapSubresource(somecubeaschunk.ConstantLightBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
                                                    Utilities.Write(dataBox0.DataPointer, ref somecubeaschunk.lightBuffer[0]);
                                                    renderingContext.UnmapSubresource(somecubeaschunk.ConstantLightBuffer, 0);

                                                    var dataBox = renderingContext.MapSubresource(somecubeaschunk.arraychunkdatalod2[chunkindex].dynamicConstantBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
                                                    Utilities.Write(dataBox.DataPointer, ref worldViewProj);
                                                    renderingContext.UnmapSubresource(somecubeaschunk.arraychunkdatalod2[chunkindex].dynamicConstantBuffer, 0);
                                                }
                                                else
                                                {
                                                    renderingContext.UpdateSubresource(ref worldViewProj, somecubeaschunk.arraychunkdatalod2[chunkindex].staticContantBuffer);
                                                }

                                                // Draw the cube 
                                                //renderingContext.Draw(36, 0);
                                                renderingContext.DrawIndexed(somecubeaschunk.arraychunkdatalod2[chunkindex].arraychunkvertslod2.arrayoftrigs.Length, 0, 0);
                                                //renderingContext.Draw(somecubeaschunk.arraychunkdatalod2[chunkindex].arraychunkvertslod2.arraychunkvertslod2.Length, 0);
                                            }
                                        }
                                        else
                                        {
                                            if (somecubeaschunk.arraychunkdatalod2[chunkindex].distanceculling >= minlod2)
                                            {
                                                countmeshdistculledlod2++;
                                            }
                                            if (!somecubeaschunk.arraychunkdatalod2[chunkindex].frustrumculldraw)
                                            {
                                                countmeshfrustculledlod2++;
                                            }

                                            countmeshtrigculledlod2 += somecubeaschunk.arraychunkdatalod2[chunkindex].arraychunkvertslod2.arrayoftrigs.Length;
                                            countmeshvertculledlod2 += somecubeaschunk.arraychunkdatalod2[chunkindex].arraychunkvertslod2.arrayofverts.Length;
                                            countmeshculledlod2++;
                                        }
                                    }
                                }
                            }
                        }*/


                        /*
                        if (somecubeaschunk.arraychunkdatalod3 != null)
                        {
                            if (somecubeaschunk.arraychunkdatalod3[chunkindex].arraychunkvertslod3 != null)
                            {
                                if (somecubeaschunk.arraychunkdatalod3[chunkindex].arraychunkvertslod3.arrayofverts != null)
                                {
                                    if (somecubeaschunk.arraychunkdatalod3[chunkindex].arraychunkvertslod3.arrayofverts.Length > 0)
                                    {
                                        //distsquaredlod3 = somecubeaschunk.arraychunkdatalod3[chunkindex].distanceculling;
                                        //frustrumculllod3 = somecubeaschunk.arraychunkdatalod3[chunkindex].frustrumculldraw;

                                        if (somecubeaschunk.arraychunkdatalod3[chunkindex].distanceculling >= minlod3 && somecubeaschunk.arraychunkdatalod3[chunkindex].distanceculling < maxlod3 && somecubeaschunk.arraychunkdatalod3[chunkindex].frustrumculldraw)
                                        {
                                            //if (frustrumculllod0)
                                            {
                                                renderingContext.InputAssembler.InputLayout = somecubeaschunk.arraychunkdatalod3[chunkindex].layout;
                                                renderingContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
                                                renderingContext.InputAssembler.SetIndexBuffer(somecubeaschunk.arraychunkdatalod3[chunkindex].indicesbuffer, SharpDX.DXGI.Format.R32_UInt, 0);

                                                //renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecubeaschunk.arraychunkdatalod3[chunkindex].verticesbuffer, Utilities.SizeOf<tutorialcubeaschunk.DVertex>(), 0));
                                                renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecubeaschunk.arraychunkdatalod3[chunkindex].verticesbuffer, Utilities.SizeOf<tutorialcubeaschunk.DVertex>(), 0));
                                                renderingContext.VertexShader.SetConstantBuffer(0, currentState.UseMap ? somecubeaschunk.arraychunkdatalod3[chunkindex].dynamicConstantBuffer : somecubeaschunk.arraychunkdatalod3[chunkindex].staticContantBuffer);
                                                renderingContext.PixelShader.SetConstantBuffer(1, somecubeaschunk.ConstantLightBuffer);


                                                renderingContext.VertexShader.Set(somecubeaschunk.arraychunkdatalod3[chunkindex].vertexShader);
                                                renderingContext.Rasterizer.SetViewport(0, 0, D3D.SurfaceWidth, D3D.SurfaceHeight);
                                                renderingContext.PixelShader.Set(somecubeaschunk.arraychunkdatalod3[chunkindex].pixelShader);
                                                renderingContext.OutputMerger.SetTargets(D3D.DepthStencilView, D3D.RenderTargetView);

                                                if (currentState.UseMap)
                                                {
                                                    var dataBox0 = renderingContext.MapSubresource(somecubeaschunk.ConstantLightBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
                                                    Utilities.Write(dataBox0.DataPointer, ref somecubeaschunk.lightBuffer[0]);
                                                    renderingContext.UnmapSubresource(somecubeaschunk.ConstantLightBuffer, 0);

                                                    var dataBox = renderingContext.MapSubresource(somecubeaschunk.arraychunkdatalod3[chunkindex].dynamicConstantBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
                                                    Utilities.Write(dataBox.DataPointer, ref worldViewProj);
                                                    renderingContext.UnmapSubresource(somecubeaschunk.arraychunkdatalod3[chunkindex].dynamicConstantBuffer, 0);
                                                }
                                                else
                                                {
                                                    renderingContext.UpdateSubresource(ref worldViewProj, somecubeaschunk.arraychunkdatalod3[chunkindex].staticContantBuffer);
                                                }

                                                // Draw the cube 
                                                //renderingContext.Draw(36, 0);
                                                renderingContext.DrawIndexed(somecubeaschunk.arraychunkdatalod3[chunkindex].arraychunkvertslod3.arrayoftrigs.Length, 0, 0);
                                                //renderingContext.Draw(somecubeaschunk.arraychunkdatalod3[chunkindex].arraychunkvertslod3.arraychunkvertslod3.Length, 0);
                                            }
                                        }
                                        else
                                        {
                                            if (somecubeaschunk.arraychunkdatalod3[chunkindex].distanceculling >= minlod3)
                                            {
                                                countmeshdistculledlod3++;
                                            }
                                            if (!somecubeaschunk.arraychunkdatalod3[chunkindex].frustrumculldraw)
                                            {
                                                countmeshfrustculledlod3++;
                                            }

                                            countmeshtrigculledlod3 += somecubeaschunk.arraychunkdatalod3[chunkindex].arraychunkvertslod3.arrayoftrigs.Length;
                                            countmeshvertculledlod3 += somecubeaschunk.arraychunkdatalod3[chunkindex].arraychunkvertslod3.arrayofverts.Length;
                                            countmeshculledlod3++;
                                        }
                                    }
                                }
                            }
                        }



                        if (somecubeaschunk.arraychunkdatalod4 != null)
                        {
                            if (somecubeaschunk.arraychunkdatalod4[chunkindex].arraychunkvertslod4 != null)
                            {
                                if (somecubeaschunk.arraychunkdatalod4[chunkindex].arraychunkvertslod4.arrayofverts != null)
                                {
                                    if (somecubeaschunk.arraychunkdatalod4[chunkindex].arraychunkvertslod4.arrayofverts.Length > 0)
                                    {
                                        //distsquaredlod4 = somecubeaschunk.arraychunkdatalod4[chunkindex].distanceculling;
                                        //frustrumculllod4 = somecubeaschunk.arraychunkdatalod4[chunkindex].frustrumculldraw;

                                        if (somecubeaschunk.arraychunkdatalod4[chunkindex].distanceculling >= minlod4 && somecubeaschunk.arraychunkdatalod4[chunkindex].distanceculling < maindist && somecubeaschunk.arraychunkdatalod4[chunkindex].frustrumculldraw)
                                        {
                                            //if (frustrumculllod0)
                                            {
                                                renderingContext.InputAssembler.InputLayout = somecubeaschunk.arraychunkdatalod4[chunkindex].layout;
                                                renderingContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
                                                renderingContext.InputAssembler.SetIndexBuffer(somecubeaschunk.arraychunkdatalod4[chunkindex].indicesbuffer, SharpDX.DXGI.Format.R32_UInt, 0);

                                                //renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecubeaschunk.arraychunkdatalod4[chunkindex].verticesbuffer, Utilities.SizeOf<tutorialcubeaschunk.DVertex>(), 0));
                                                renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecubeaschunk.arraychunkdatalod4[chunkindex].verticesbuffer, Utilities.SizeOf<tutorialcubeaschunk.DVertex>(), 0));
                                                renderingContext.VertexShader.SetConstantBuffer(0, currentState.UseMap ? somecubeaschunk.arraychunkdatalod4[chunkindex].dynamicConstantBuffer : somecubeaschunk.arraychunkdatalod4[chunkindex].staticContantBuffer);
                                                renderingContext.PixelShader.SetConstantBuffer(1, somecubeaschunk.ConstantLightBuffer);

                                                renderingContext.VertexShader.Set(somecubeaschunk.arraychunkdatalod4[chunkindex].vertexShader);
                                                renderingContext.Rasterizer.SetViewport(0, 0, D3D.SurfaceWidth, D3D.SurfaceHeight);
                                                renderingContext.PixelShader.Set(somecubeaschunk.arraychunkdatalod4[chunkindex].pixelShader);
                                                renderingContext.OutputMerger.SetTargets(D3D.DepthStencilView, D3D.RenderTargetView);

                                                if (currentState.UseMap)
                                                {
                                                    var dataBox0 = renderingContext.MapSubresource(somecubeaschunk.ConstantLightBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
                                                    Utilities.Write(dataBox0.DataPointer, ref somecubeaschunk.lightBuffer[0]);
                                                    renderingContext.UnmapSubresource(somecubeaschunk.ConstantLightBuffer, 0);

                                                    var dataBox = renderingContext.MapSubresource(somecubeaschunk.arraychunkdatalod4[chunkindex].dynamicConstantBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
                                                    Utilities.Write(dataBox.DataPointer, ref worldViewProj);
                                                    renderingContext.UnmapSubresource(somecubeaschunk.arraychunkdatalod4[chunkindex].dynamicConstantBuffer, 0);
                                                }
                                                else
                                                {
                                                    renderingContext.UpdateSubresource(ref worldViewProj, somecubeaschunk.arraychunkdatalod4[chunkindex].staticContantBuffer);
                                                }

                                                // Draw the cube 
                                                //renderingContext.Draw(36, 0);
                                                renderingContext.DrawIndexed(somecubeaschunk.arraychunkdatalod4[chunkindex].arraychunkvertslod4.arrayoftrigs.Length, 0, 0);
                                                //renderingContext.Draw(somecubeaschunk.arraychunkdatalod4[chunkindex].arraychunkvertslod4.arraychunkvertslod4.Length, 0);
                                            }
                                        }
                                        else
                                        {
                                            if (somecubeaschunk.arraychunkdatalod4[chunkindex].distanceculling >= minlod4)
                                            {
                                                countmeshdistculledlod4++;
                                            }
                                            if (!somecubeaschunk.arraychunkdatalod4[chunkindex].frustrumculldraw)
                                            {
                                                countmeshfrustculledlod4++;
                                            }

                                            countmeshtrigculledlod4 += somecubeaschunk.arraychunkdatalod4[chunkindex].arraychunkvertslod4.arrayoftrigs.Length;
                                            countmeshvertculledlod4 += somecubeaschunk.arraychunkdatalod4[chunkindex].arraychunkvertslod4.arrayofverts.Length;
                                            countmeshculledlod4++;
                                        }
                                    }
                                }
                            }
                        }
                        

                        if (somecubeaschunk.arraychunkdatalod0 != null)
                        {
                            if (somecubeaschunk.arraychunkdatalod0[chunkindex].arraychunkvertslod0 != null)
                            {
                                if (somecubeaschunk.arraychunkdatalod0[chunkindex].arraychunkvertslod0.arrayofverts != null)
                                {
                                    if (somecubeaschunk.arraychunkdatalod0[chunkindex].arraychunkvertslod0.arrayofverts.Length > 0)
                                    {
                                        if (somecubeaschunk.arraychunkdatalod0[chunkindex].distanceculling < minlod0 && somecubeaschunk.arraychunkdatalod0[chunkindex].frustrumculldraw)
                                        {

                                            //somedraw = somefrustrum.CheckSphere(chunkpos, someradius);

                                            //if (frustrumculllod0)
                                            {
                                                renderingContext.InputAssembler.InputLayout = somecubeaschunk.arraychunkdatalod0[chunkindex].layout;
                                                renderingContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
                                                renderingContext.InputAssembler.SetIndexBuffer(somecubeaschunk.arraychunkdatalod0[chunkindex].indicesbuffer, SharpDX.DXGI.Format.R32_UInt, 0);

                                                //renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecubeaschunk.arraychunkdatalod0[chunkindex].verticesbuffer, Utilities.SizeOf<tutorialcubeaschunk.DVertex>(), 0));
                                                renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecubeaschunk.arraychunkdatalod0[chunkindex].verticesbuffer, Utilities.SizeOf<tutorialcubeaschunk.DVertex>(), 0));
                                                renderingContext.VertexShader.SetConstantBuffer(0, currentState.UseMap ? somecubeaschunk.arraychunkdatalod0[chunkindex].dynamicConstantBuffer : somecubeaschunk.arraychunkdatalod0[chunkindex].staticContantBuffer);
                                                renderingContext.PixelShader.SetConstantBuffer(1, somecubeaschunk.ConstantLightBuffer);


                                                renderingContext.VertexShader.Set(somecubeaschunk.arraychunkdatalod0[chunkindex].vertexShader);
                                                renderingContext.Rasterizer.SetViewport(0, 0, D3D.SurfaceWidth, D3D.SurfaceHeight);
                                                renderingContext.PixelShader.Set(somecubeaschunk.arraychunkdatalod0[chunkindex].pixelShader);
                                                renderingContext.OutputMerger.SetTargets(D3D.DepthStencilView, D3D.RenderTargetView);

                                                if (currentState.UseMap)
                                                {
                                                    var dataBox0 = renderingContext.MapSubresource(somecubeaschunk.ConstantLightBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
                                                    Utilities.Write(dataBox0.DataPointer, ref somecubeaschunk.lightBuffer[0]);
                                                    renderingContext.UnmapSubresource(somecubeaschunk.ConstantLightBuffer, 0);

                                                    var dataBox = renderingContext.MapSubresource(somecubeaschunk.arraychunkdatalod0[chunkindex].dynamicConstantBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
                                                    Utilities.Write(dataBox.DataPointer, ref worldViewProj);
                                                    renderingContext.UnmapSubresource(somecubeaschunk.arraychunkdatalod0[chunkindex].dynamicConstantBuffer, 0);
                                                }
                                                else
                                                {
                                                    renderingContext.UpdateSubresource(ref worldViewProj, somecubeaschunk.arraychunkdatalod0[chunkindex].staticContantBuffer);
                                                }

                                                // Draw the cube 
                                                //renderingContext.Draw(36, 0);
                                                renderingContext.DrawIndexed(somecubeaschunk.arraychunkdatalod0[chunkindex].arraychunkvertslod0.arrayoftrigs.Length, 0, 0);
                                                //renderingContext.Draw(somecubeaschunk.arraychunkdatalod0[chunkindex].arraychunkvertslod0.arraychunkvertslod0.Length, 0);
                                            }
                                        }
                                        else
                                        {

                                            /*
                                            int countmeshculledlod0 = 0;
                                            int countmeshtrigculledlod0 = 0;
                                            int countmeshvertculledlod0 = 0;
                                            int countmeshdistculledlod0 = 0;
                                            int countmeshfrustculledlod0 = 0;


                                            if (somecubeaschunk.arraychunkdatalod0[chunkindex].distanceculling >= minlod0)
                                            {
                                                countmeshdistculledlod0++;
                                            }
                                            if (!somecubeaschunk.arraychunkdatalod0[chunkindex].frustrumculldraw)
                                            {
                                                countmeshfrustculledlod0++;
                                            }

                                            countmeshtrigculledlod0 += somecubeaschunk.arraychunkdatalod0[chunkindex].arraychunkvertslod0.arrayoftrigs.Length;
                                            countmeshvertculledlod0 += somecubeaschunk.arraychunkdatalod0[chunkindex].arraychunkvertslod0.arrayofverts.Length;
                                            countmeshculledlod0++;
                                        }
                                    }
                                }
                            }
                        }









                    }
                    */


                    //for (int x = 0; x < count; x++)
                    {


                        /*
                        if (currentState.UseMap)
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
                        //renderingContext.Draw(36, 0);
                        renderingContext.DrawIndexed(somecube.arrayoftrigs.Length,0, 0);
                        //renderingContext.Draw(36, 0);
                        */
                    }
                }




                if (currentState.Type != TestType.Immediate)
                {
                    commandsList[contextIndex] = renderingContext.FinishCommandList(false);
                }




                //Thread.Sleep(1);
                //goto someactionloop;

                hasfinishedRenderRow = 1;
                return hasfinishedRenderRow;
            };



            try
            {
                RenderDeferred = (int threadCount) =>
                {
                    hasfinishedRenderDeferred = 0;
                    //int deltaCube = currentState.CountCubes / threadCount;
                    int deltaCube = MaxNumberOfCubes / threadCount;
                    //Console.WriteLine(deltaCube);

                    if (deltaCube * threadCount != MaxNumberOfCubes)
                    {

                        for (int i = 0; i < MaxNumberOfCubes; i++)
                        {
                            
                            if (deltaCube * threadCount >= MaxNumberOfCubes)
                            {
                                break;
                            }
                            deltaCube++;
                        }
                        //Console.WriteLine("float");
                    }

                    //Console.WriteLine(threadCount);

                    //if (deltaCube == 0) deltaCube = 1;
                    int nextStartingRow = 0;
                    

                    if (threadswitchtwo == 0)
                    {
                     

                        //arrayoftasks = new Task[currentState.ThreadCount];
                        //somedataforthread = new dataforthread[currentState.ThreadCount];

                        threadswitchtwo = 1;
                    }

                    tasks = new Task[threadCount];
                    for (int i = 0; i < threadCount; i++)
                    {
                        var threadIndex = i;
                        int fromRow = nextStartingRow;

                        fromRow = i * deltaCube;
                        int toRow = (fromRow) + deltaCube;

                        if (toRow > MaxNumberOfCubes)
                        {
                            toRow = MaxNumberOfCubes;
                        }
                        //Console.WriteLine(toRow);

                        /*
                        int toRow = (i + 1) == threadCount ? currentState.CountCubes : fromRow + deltaCube;
                        if (toRow > currentState.CountCubes)
                        {
                            toRow = currentState.CountCubes;
                        }
                        nextStartingRow = toRow;*/

                        /*
                        int toRow = (i + 1) == threadCount ? currentState.CountCubes : fromRow + deltaCube;
                        if (toRow > currentState.CountCubes)
                        {
                            toRow = currentState.CountCubes;
                        }
                        nextStartingRow = toRow;*/

                        /*if (threadswitchtwo == 1)
                        {
                            tasks[i] = new Task(() => renderrow(threadIndex, fromRow, toRow));
                            tasks[i].Start();

                            if (i >= threadCount)
                            {

                                threadswitchtwo = 2;
                            }
                        }





                        commandsList[threadIndex] = contextPerThread[threadIndex].FinishCommandList(false);
                        */




                        tasks[i] = new Task(() => renderrow(threadIndex, fromRow, toRow));
                        tasks[i].Start();

                        /*
                        somedataforthread[i].contextIndex = threadIndex;
                        somedataforthread[i].fromY = fromRow;
                        somedataforthread[i].toY = toRow;
                        somedataforthread[i].rendertherow = renderrow;
                        */
                        /*if (threadswitchtwo == 1)
                        {
                            arrayoftasks[i] = Task<object[]>.Factory.StartNew((tester0001) =>
                            {
                            _thread_loop_console:

                                //var somevar = (Func<int, int, int, int>)tester0001;

                                if (i == somedataforthread[i].contextIndex)
                                {
                                    //somevar(somedataforthread[i].contextIndex, somedataforthread[i].fromY, somedataforthread[i].toY);

                                    //renderrowvoid(somedataforthread[i].contextIndex, somedataforthread[i].fromY, somedataforthread[i].toY);
                                }

                                Thread.Sleep(1);

                                goto _thread_loop_console;
                                //////CONSOLE WRITER <=
                            }, somedataforthread);


                            if (i >= threadCount)
                            {

                                threadswitchtwo = 2;
                            }
                        }*/

                        //commandsList[threadIndex] = contextPerThread[threadIndex].FinishCommandList(false);


                    }






                    /*int somenullval = 0;
                    for (int i = 0; i < tasks.Length; i++)
                    {
                        if (tasks[i] == null)
                        {
                            somenullval++;
                        }
                    }*/
                    
                    
                    
                    if (tasks != null)
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




                    /*

                    if (somenullval == 0)
                    {
                        if (tasks != null)
                        {
                            Task.WaitAll(tasks);
                        }                    
                    }
                    else
                    {
                      
                    }*/
                    //Task.WaitAll(tasks);
                    hasfinishedRenderDeferred = 1;
                };





















            }
            catch (Exception ex)
            {
                Program.MessageBox((IntPtr)0, "" + ex.ToString(), "msg", 0);
            }
        }





        public Task[] tasks;

        public int hasfinishedSetupPipeline = 1;
        public int hasfinishedRenderRow = 1;
        public int hasfinishedRenderDeferred = 1;

        int hasfinishedwork = 0;










        int writetobufferinit = 0;





        Vector3 chunkpos;
        Vector3 posplayer;
        float someradius = 1.0f;
        float distsquared;
        bool somedraw = false;
        float somedotprodlr;
        float somedotprodfb;



        Matrix hmd_matrix_current = Matrix.Identity;
        Matrix hmdmatrixcurrentforpelvis = Matrix.Identity;






















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

                if (tasks != null)
                {
                    for (int i = 0; i < tasks.Length; i++)
                    {
                        if (tasks[i] != null)
                        {
                            tasks[i].Wait();
                            tasks[i].Dispose();
                            tasks[i] = null;
                        }
                    }
                }

                if (main_thread_update != null)
                {
                    main_thread_update.Abort();
                }


            }
            // Release unmanaged resources
        }
    }
}
