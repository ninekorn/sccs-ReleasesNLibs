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

using DSharpDXRastertek.Tut16.Graphics.Data;




namespace sccsr15forms
{
    public class updateSec : IDisposable
    {

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

        public tutorialcubeaschunkinst somecubeaschunkinst;
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

            Console.WriteLine("created updatesec");


            somecube = new tutorialcube(D3D.Device);
            //tutorialobj = new sctutorialobj(D3D.Device);
            //somecubeaschunk = new tutorialcubeaschunk(D3D.Device);

            somecubeaschunkinst = new tutorialcubeaschunkinst(D3D.Device);


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

        public int updatescriptssec(bool runapptype)
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
            countmeshtrigculledlod2= 0;
            countmeshvertculledlod2= 0;
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




            posplayer = updateprim.camera.GetPosition();
            //Console.WriteLine(posplayer);

            hasfinishedwork = 0;
            if (currentState.Exit)
                sccsr15forms.Form1.currentform.Close();




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





            totalmeshdistculled = countmeshdistculledlod0 + countmeshdistculledlod1 + countmeshdistculledlod2 + countmeshdistculledlod3 + countmeshdistculledlod4;
            totalmeshfrustculled = countmeshfrustculledlod0 + countmeshfrustculledlod1 + countmeshfrustculledlod2 + countmeshfrustculledlod3 + countmeshfrustculledlod4;







            var view = updateprim.camera.ViewMatrix;// Matrix.LookAtLH(new Vector3(0, 0, -viewZ), new Vector3(0, 0, 0), Vector3.UnitY);
            //var proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, D3D.SurfaceWidth / (float)D3D.SurfaceHeight, 0.1f, 1000.0f);          
            var viewProj = Matrix.Multiply(view, D3D.ProjectionMatrix);

            Matrix rotateMatrix = Matrix.Identity;
            Matrix worldViewProj;
            Matrix.Multiply(ref rotateMatrix, ref viewProj, out worldViewProj);
            worldViewProj.Transpose();



            for (int i = 0; i < somecubeaschunkinst.somefacemeshlisttodraw.Count; i++)
            {
                somecubeaschunkinst.somefacemeshlisttodraw[i].render(D3D.Device, worldViewProj, somecubeaschunkinst);
            }
           




            
            /////////////////////////////////////////////
            /////////////////////////////////////////////
            D3D.DeviceContext.Rasterizer.SetViewport(0, 0, D3D.SurfaceWidth, D3D.SurfaceHeight);
            D3D.DeviceContext.OutputMerger.SetTargets(D3D.DepthStencilView, D3D.RenderTargetView);
            updateprim.updatescriptsupdatetext(D3D.DeviceContext);
            /////////////////////////////////////////////
            /////////////////////////////////////////////






            hasfinishedwork = 1;
            return hasfinishedwork;
        }


        Vector3 chunkpos;
        Vector3 posplayer;
        float someradius = 1.0f;
        float distsquared;
        bool somedraw = false;
        float somedotprodlr;
        float somedotprodfb;

        public void workonculling()
        {




             //OFFSETPOS;//.camera.GetPosition();// OFFSETPOS;


            //dfrustrum.ConstructFrustum(1000.0f, D3D.ProjectionMatrix, updateprim.camera.ViewMatrix);

          
            int somecounterfrustrumdraw = 0;
            //Console.WriteLine(posplayer);








            for (int c = 0; c < somecubeaschunk.arraychunkdatalod0.Length; c++)
            {

                if (somecubeaschunk.arraychunkdatalod0 != null)
                {
                    if (somecubeaschunk.arraychunkdatalod0[c].arraychunkvertslod0 != null)
                    {
                        if (somecubeaschunk.arraychunkdatalod0[c].arraychunkvertslod0.arrayofverts != null)
                        {
                            if (somecubeaschunk.arraychunkdatalod0[c].arraychunkvertslod0.arrayofverts.Length > 0)
                            {
                                var chunkpos = somecubeaschunk.arraychunkdatalod0[c].realpos;

                                //distsquared = sc_maths.trying_ellipsoid_with_sc_sebastian_lague_check_distanceconvertedto3dkinda(posplayer, chunkpos);
                                //distsquared = sc_maths.sc_check_distance_node_3d_geometry(posplayer, chunkpos,3.0f, 3.0f, 3.0f, 3.0f, 3.0f, 100.0f);

                                //Console.WriteLine(distsquared);
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


                                /*float xview;
                                float yview;

                                Vector3 dirf = new Vector3(scupdate.dirikvoxelbodyInstanceForward0.X, scupdate.dirikvoxelbodyInstanceForward0.Y, scupdate.dirikvoxelbodyInstanceForward0.Z);
                                Vector3 dirr = new Vector3(scupdate.dirikvoxelbodyInstanceRight0.X, scupdate.dirikvoxelbodyInstanceRight0.Y, scupdate.dirikvoxelbodyInstanceRight0.Z);
                                Vector3 diru = new Vector3(scupdate.dirikvoxelbodyInstanceUp0.X, scupdate.dirikvoxelbodyInstanceUp0.Y, scupdate.dirikvoxelbodyInstanceUp0.Z);
                                */

                                var dirf = new Vector3(updateprim.dircamf.X, updateprim.dircamf.Y, updateprim.dircamf.Z);
                                Vector3 dirr = new Vector3(updateprim.dircamr.X, updateprim.dircamr.Y, updateprim.dircamr.Z);


                                Vector3 temppos = new Vector3(chunkpos[0], chunkpos[1], chunkpos[2]);
                                distsquared = sc_maths.distancebasedondirection(out somedotprodlr, out somedotprodfb, dirf, dirr, posplayer, temppos, minx, miny, minz, diagminx, diagminy, diagminz, diagmaxx, diagmaxy, diagmaxz); // chunkwidthl, chunkwidthr, chunkheightl, chunkheightr, chunkdepthl, chunkdepthr

                                //distsquared = Vector3.DistanceSquared(posplayer, chunkpos);
                                //distsquared = Vector3.Distance(posplayer, chunkpos);


                                //Console.WriteLine(distsquared);
                                //maindist = 16.0f;
                                //distsquared = sc_maths.sc_check_distance_node_3d(posplayer, chunkpos, minx, miny, minz, diagminx, diagminy, diagminz, diagmaxx, diagmaxy, diagmaxz);

                                //Console.WriteLine(distsquared);
                                //Vector3 lineright = posplayer + (dirr);
                                //Vector3 lineup = posplayer + (diru);
                                //float res = 0;

                                //Vector3 dirtopoint = chunkpos - posplayer;

                                //bool ispointlorr = sc_maths.isLeft(lineright, lineup, chunkpos);

                                //Vector3.Dot(ref lineright, ref dirtopoint, out res);

                                //bool somebool = scsomefrustrum.CheckSphere(chunkpos,1.0f);

                                /*
                                if (distsquared < 4.0f)
                                {
                                    Console.WriteLine(chunkpos);
                                }*/

                                //somedotprod < 0.35f && somedotprod >= 0 || somedotprod > -0.35f && somedotprod  < 0
                                if (somedotprodfb < -0.75f) //-0.50f
                                {
                                    somedraw = true;
                                }
                                else
                                {
                                    somedraw = false;
                                }






                                if (distsquared < maindist)
                                {
                                 
                                    someradius = 2.0f;
                                    /*somedraw = dfrustrum.CheckSphere(chunkpos, someradius);
                                    if (!somedraw)
                                    {
                                        somecounterfrustrumdraw++;

                                    }*/
                                    //somedraw = somefrustrum.CheckSphere(chunkpos, someradius);
                                    //somedraw = scsomefrustrum.CheckSphere(chunkpos, someradius);
                                    somecubeaschunk.arraychunkdatalod0[c].frustrumculldraw = somedraw;
                                    //Console.WriteLine(chunkpos + " " + somedraw);
                                    somecubeaschunk.arraychunkdatalod0[c].distanceculling = distsquared;


                                    
                                    //somedraw = scsomefrustrum.CheckSphere(chunkpos, someradius);
                                    if (somecubeaschunk.arraychunkdatalod1 != null)
                                    {
                                        if (somecubeaschunk.arraychunkdatalod1[c].arraychunkvertslod1 != null)
                                        {
                                            if (somecubeaschunk.arraychunkdatalod1[c].arraychunkvertslod1.arrayofverts != null)
                                            {
                                                if (somecubeaschunk.arraychunkdatalod1[c].arraychunkvertslod1.arrayofverts.Length > 0)
                                                {
                                                    someradius = 4.0f;
                                                   
                                                    //somedraw = dfrustrum.CheckSphere(chunkpos, someradius);
                                                  
                                                    //somedraw = somefrustrum.CheckSphere(chunkpos, someradius);
                                                    somecubeaschunk.arraychunkdatalod1[c].frustrumculldraw = somedraw;
                                                    somecubeaschunk.arraychunkdatalod1[c].distanceculling = distsquared;
                                                }
                                            }
                                        }
                                    }
                                  

                                    if (somecubeaschunk.arraychunkdatalod2 != null)
                                    {
                                        if (somecubeaschunk.arraychunkdatalod2[c].arraychunkvertslod2 != null)
                                        {
                                            if (somecubeaschunk.arraychunkdatalod2[c].arraychunkvertslod2.arrayofverts != null)
                                            {
                                                if (somecubeaschunk.arraychunkdatalod2[c].arraychunkvertslod2.arrayofverts.Length > 0)
                                                {
                                                    someradius = 6.0f;
                                                    //somedraw = dfrustrum.CheckSphere(chunkpos, someradius);
                                                   
                                                    //somedraw = somefrustrum.CheckSphere(chunkpos, someradius);
                                                    //somedraw = scsomefrustrum.CheckSphere(chunkpos, someradius);
                                                    somecubeaschunk.arraychunkdatalod2[c].frustrumculldraw = somedraw;
                                                    somecubeaschunk.arraychunkdatalod2[c].distanceculling = distsquared;
                                                }
                                            }
                                        }
                                    }



                                    if (somecubeaschunk.arraychunkdatalod3 != null)
                                    {
                                        if (somecubeaschunk.arraychunkdatalod3[c].arraychunkvertslod3 != null)
                                        {
                                            if (somecubeaschunk.arraychunkdatalod3[c].arraychunkvertslod3.arrayofverts != null)
                                            {
                                                if (somecubeaschunk.arraychunkdatalod3[c].arraychunkvertslod3.arrayofverts.Length > 0)
                                                {
                                                    someradius = 8.0f;
                                                    //somedraw = dfrustrum.CheckSphere(chunkpos, someradius);
                                        
                                                    //somedraw = somefrustrum.CheckSphere(chunkpos, someradius);
                                                    //somedraw = scsomefrustrum.CheckSphere(chunkpos, someradius);
                                                    somecubeaschunk.arraychunkdatalod3[c].frustrumculldraw = somedraw;
                                                    somecubeaschunk.arraychunkdatalod3[c].distanceculling = distsquared;
                                                }
                                            }
                                        }
                                    }


                                    if (somecubeaschunk.arraychunkdatalod4 != null)
                                    {
                                        if (somecubeaschunk.arraychunkdatalod4[c].arraychunkvertslod4 != null)
                                        {
                                            if (somecubeaschunk.arraychunkdatalod4[c].arraychunkvertslod4.arrayofverts != null)
                                            {
                                                if (somecubeaschunk.arraychunkdatalod4[c].arraychunkvertslod4.arrayofverts.Length > 0)
                                                {
                                                    someradius = 10.0f;
                                                    //somedraw = dfrustrum.CheckSphere(chunkpos, someradius);
                                                   
                                                    //somedraw = somefrustrum.CheckSphere(chunkpos, someradius);
                                                    //somedraw = scsomefrustrum.CheckSphere(chunkpos, someradius);
                                                    somecubeaschunk.arraychunkdatalod4[c].frustrumculldraw = somedraw;
                                                    somecubeaschunk.arraychunkdatalod4[c].distanceculling = distsquared;
                                                }
                                            }
                                        }
                                    }
                                    



                                }
                                else
                                {
                                    somecubeaschunk.arraychunkdatalod0[c].frustrumculldraw = false;
                                    somecubeaschunk.arraychunkdatalod0[c].distanceculling = distsquared;

                                    if (somecubeaschunk.arraychunkdatalod1 != null)
                                    {
                                        if (somecubeaschunk.arraychunkdatalod1[c].arraychunkvertslod1 != null)
                                        {
                                            if (somecubeaschunk.arraychunkdatalod1[c].arraychunkvertslod1.arrayofverts != null)
                                            {
                                                if (somecubeaschunk.arraychunkdatalod1[c].arraychunkvertslod1.arrayofverts.Length > 0)
                                                {

                                                    somecubeaschunk.arraychunkdatalod1[c].frustrumculldraw = false;
                                                    somecubeaschunk.arraychunkdatalod1[c].distanceculling = distsquared;
                                                }
                                            }
                                        }
                                    }

                                    
                                    if (somecubeaschunk.arraychunkdatalod2 != null)
                                    {
                                        if (somecubeaschunk.arraychunkdatalod2[c].arraychunkvertslod2 != null)
                                        {
                                            if (somecubeaschunk.arraychunkdatalod2[c].arraychunkvertslod2.arrayofverts != null)
                                            {
                                                if (somecubeaschunk.arraychunkdatalod2[c].arraychunkvertslod2.arrayofverts.Length > 0)
                                                {

                                                    somecubeaschunk.arraychunkdatalod2[c].frustrumculldraw = false;
                                                    somecubeaschunk.arraychunkdatalod2[c].distanceculling = distsquared;
                                                }
                                            }
                                        }
                                    }



                                    if (somecubeaschunk.arraychunkdatalod3 != null)
                                    {
                                        if (somecubeaschunk.arraychunkdatalod3[c].arraychunkvertslod3 != null)
                                        {
                                            if (somecubeaschunk.arraychunkdatalod3[c].arraychunkvertslod3.arrayofverts != null)
                                            {
                                                if (somecubeaschunk.arraychunkdatalod3[c].arraychunkvertslod3.arrayofverts.Length > 0)
                                                {
                                                    somecubeaschunk.arraychunkdatalod3[c].frustrumculldraw = false;
                                                    somecubeaschunk.arraychunkdatalod3[c].distanceculling = distsquared;
                                                }
                                            }
                                        }
                                    }

                                    if (somecubeaschunk.arraychunkdatalod4 != null)
                                    {
                                        if (somecubeaschunk.arraychunkdatalod4[c].arraychunkvertslod4 != null)
                                        {
                                            if (somecubeaschunk.arraychunkdatalod4[c].arraychunkvertslod4.arrayofverts != null)
                                            {
                                                if (somecubeaschunk.arraychunkdatalod4[c].arraychunkvertslod4.arrayofverts.Length > 0)
                                                {
                                                    somecubeaschunk.arraychunkdatalod4[c].frustrumculldraw = false;
                                                    somecubeaschunk.arraychunkdatalod4[c].distanceculling = distsquared;
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
        }


        dataforthread[] somedataforthread;// = new dataforthread[];

        public struct dataforthread
        {
            public int contextIndex;
            public int fromY;
            public int toY;
            public Func<int, int, int, int> rendertherow;
        }



       public void renderrowvoid(int contextIndex, int fromY, int toY)
       {




           //hasfinishedRenderRow = 0;
           var renderingContext = contextPerThread[contextIndex];
           var time = Program.clock.ElapsedMilliseconds / 1000.0f;




           //Console.WriteLine(contextIndex);
           if (contextIndex == 0)
           {
               //updateprim.startrender();
               contextPerThread[0].ClearDepthStencilView(D3D.DepthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);
               contextPerThread[0].ClearRenderTargetView(D3D.RenderTargetView, SharpDX.Color.LightGray);
               //Console.WriteLine("clear");

           }




           //updateprim_.camera.Render();

           var view = updateprim.camera.ViewMatrix;// Matrix.LookAtLH(new Vector3(0, 0, -viewZ), new Vector3(0, 0, 0), Vector3.UnitY);
           var proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, D3D.SurfaceWidth / (float)D3D.SurfaceHeight, 0.1f, 1000.0f);


           var viewProj = Matrix.Multiply(view, D3D.ProjectionMatrix);



















           //int count = currentState.CountCubes;
           //float divCubes = (float)count / (viewZ - 1);
           //Matrix.Scaling(1.0f / count) *
           var rotateMatrix = Matrix.Identity; //Matrix.Scaling(1.0f / count) * Matrix.RotationX(time) * Matrix.RotationY(time * 2) * Matrix.RotationZ(time * .7f);  /// Matrix.Identity; //

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

                                   if (somecubeaschunk.arraychunkdatalod1[chunkindex].distanceculling >= minlod1 && somecubeaschunk.arraychunkdatalod1[chunkindex].distanceculling < maxlod1 && somecubeaschunk.arraychunkdatalod1[chunkindex].frustrumculldraw)
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
                                               //mappedResourceLight.Dispose();



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
                   }



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
                   }*/





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
                                       int countmeshfrustculledlod0 = 0;*/


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
               commandsList[contextIndex] = renderingContext.FinishCommandList(false);



           // = 1;
           //return hasfinishedRenderRow;
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
