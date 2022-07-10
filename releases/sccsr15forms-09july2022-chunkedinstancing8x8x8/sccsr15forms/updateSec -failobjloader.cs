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

        public tutorialcubeaschunkleft somecubeaschunkleft;
        public tutorialcubeaschunkright somecubeaschunkright;
        public tutorialcubeaschunktop somecubeaschunktop;
        public tutorialcubeaschunkbottom somecubeaschunkbottom;
        public tutorialcubeaschunkfront somecubeaschunkfront;
        public tutorialcubeaschunkback somecubeaschunkback;

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

        public updateSec(updatePrim updateprim_, directx D3D_)
        {
            D3D = D3D_;
            currentupdatesec = this;
            updateprim = updateprim_;

            Console.WriteLine("created updatesec");



            somecube = new tutorialcube(D3D.Device);
            //tutorialobj = new sctutorialobj(D3D.Device);
            somecubeaschunkleft = new tutorialcubeaschunkleft(D3D.Device);
            /*somecubeaschunkright = new tutorialcubeaschunkright(D3D.Device);
            somecubeaschunktop = new tutorialcubeaschunktop(D3D.Device);
            somecubeaschunkbottom = new tutorialcubeaschunkbottom(D3D.Device);
            somecubeaschunkfront = new tutorialcubeaschunkfront(D3D.Device);
            somecubeaschunkback = new tutorialcubeaschunkback(D3D.Device);*/

            if (activatelevelgen == 1)
            {

            }

            //D3D.currentState.CountCubes = somelevelgenprim[0].arrayOfChunkDatalod0.Length;

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


                    
                    renderingContext.InputAssembler.InputLayout = somecubeaschunkleft.layout;
                    renderingContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
                    renderingContext.InputAssembler.SetIndexBuffer(somecubeaschunkleft.IndicesBuffer, SharpDX.DXGI.Format.R32_UInt, 0);

                    //renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecubeaschunkleft.verticesbuffer, Utilities.SizeOf<tutorialcubeaschunk.DVertex>(), 0));
                    renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecubeaschunkleft.verticesbuffer, Utilities.SizeOf<Vector4>(), 0));
                    renderingContext.VertexShader.SetConstantBuffer(0, D3D.currentState.UseMap ? somecubeaschunkleft.dynamicConstantBuffer : somecubeaschunkleft.staticContantBuffer);
                    renderingContext.VertexShader.Set(somecubeaschunkleft.vertexShader);
                    renderingContext.Rasterizer.SetViewport(0, 0, D3D.SurfaceWidth, D3D.SurfaceHeight);
                    renderingContext.PixelShader.Set(somecubeaschunkleft.pixelShader);
                    renderingContext.OutputMerger.SetTargets(D3D.DepthStencilView, D3D.RenderTargetView);
                    


                    /*
                    renderingContext.InputAssembler.InputLayout = somecube.layout;
                    renderingContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;

                    renderingContext.InputAssembler.SetIndexBuffer(somecube.IndicesBuffer, SharpDX.DXGI.Format.R32_UInt, 0);
                    renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecube.verticesbuffer, Utilities.SizeOf<Vector4>()*2, 0));

                    renderingContext.VertexShader.SetConstantBuffer(0, D3D.currentState.UseMap ? somecube.dynamicConstantBuffer : somecube.staticContantBuffer);
                    renderingContext.VertexShader.Set(somecube.vertexShader);
                    renderingContext.Rasterizer.SetViewport(0, 0, D3D.SurfaceWidth, D3D.SurfaceHeight);
                    renderingContext.PixelShader.Set(somecube.pixelShader);
                    renderingContext.OutputMerger.SetTargets(D3D.DepthStencilView, D3D.RenderTargetView);
                    */

                }
                hasfinishedSetupPipeline = 1;
            };





            renderrow = (contextIndex, fromY, toY) =>
            {

                hasfinishedRenderRow = 0;
                var renderingContext = D3D.contextPerThread[contextIndex];
                var time = Program.clock.ElapsedMilliseconds / 1000.0f;




                //Console.WriteLine(contextIndex);
                if (contextIndex == 0)
                {
                    //updateprim.startrender();
                    D3D.contextPerThread[0].ClearDepthStencilView(D3D.DepthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);
                    D3D.contextPerThread[0].ClearRenderTargetView(D3D.RenderTargetView, SharpDX.Color.LightGray);
                    //Console.WriteLine("clear");

                    //updateprim.updatescriptsupdatetext(D3D.contextPerThread[0]);

                }

                /*
                if ( D3D.currentState.Type == directx.TestType.FrozenDeferred )
                {
                    //updateprim.updatescriptsupdatetext(D3D.contextPerThread[0]);
                    //updateprim.updatescriptsupdatetext(D3D.DeviceContext);
                }*/




                //if ( D3D.currentState.Type != directx.TestType.Immediate || contextIndex == 0 && D3D.currentState.Type == directx.TestType.FrozenDeferred || contextIndex > 0 && D3D.currentState.Type != directx.TestType.FrozenDeferred && contextIndex > 0 && D3D.currentState.Type != directx.TestType.Immediate)
                {
                    //updateprim.updatescriptsupdatetext(D3D.contextPerThread[D3D.currentState.ThreadCount - 1]);
                    //Console.WriteLine("updating text");
                    //updateprim.updatescriptsupdatetext();





                    //updateprim.updatescriptsupdatetext();

                    //updateprim_.camera.Render();

                    var view = updateprim_.camera.ViewMatrix;// Matrix.LookAtLH(new Vector3(0, 0, -viewZ), new Vector3(0, 0, 0), Vector3.UnitY);
                    var proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, D3D.SurfaceWidth / (float)D3D.SurfaceHeight, 0.1f, 1000.0f);


                    var viewProj = Matrix.Multiply(view, D3D.ProjectionMatrix);








                    int count = D3D.currentState.CountCubes;
                    float divCubes = (float)count / (viewZ - 1);

                    var rotateMatrix = Matrix.Scaling(1.0f / count) * Matrix.Identity; //Matrix.Scaling(1.0f / count) * Matrix.RotationX(time) * Matrix.RotationY(time * 2) * Matrix.RotationZ(time * .7f);  /// Matrix.Identity; //

                    for (int y = fromY; y < toY; y++)
                    {
                        for (int x = 0; x < count; x++)
                        {
                            rotateMatrix.M41 = (x + .5f - count * .5f) / divCubes;
                            rotateMatrix.M42 = (y + .5f - count * .5f) / divCubes;

                            // Update WorldViewProj Matrix 

                            // Simulate CPU usage in order to see benefits of worlViewProj
                            Matrix worldViewProj;
                            Matrix.Multiply(ref rotateMatrix, ref viewProj, out worldViewProj);
                            worldViewProj.Transpose();
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
                                var dataBox = renderingContext.MapSubresource(somecubeaschunkleft.dynamicConstantBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
                                Utilities.Write(dataBox.DataPointer, ref worldViewProj);
                                renderingContext.UnmapSubresource(somecubeaschunkleft.dynamicConstantBuffer, 0);
                            }
                            else
                            {
                                renderingContext.UpdateSubresource(ref worldViewProj, somecubeaschunkleft.staticContantBuffer);
                            }

                            // Draw the cube 
                            //renderingContext.Draw(36, 0);
                            renderingContext.DrawIndexed(somecubeaschunkleft.arrayoftrigs.Length, 0, 0);
                            //renderingContext.Draw(somecubeaschunkleft.arrayofverts.Length, 0);


                            /*
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
                            //renderingContext.Draw(36, 0);
                            renderingContext.DrawIndexed(somecube.arrayoftrigs.Length,0, 0);
                            //renderingContext.Draw(36, 0);
                            */



                        }
                    }

                    /*
                    if (D3D.currentState.Type != directx.TestType.Immediate)
                    {
                        updateprim.updatescriptsupdatetext(renderingContext);
                    }
                    */

                    if (D3D.currentState.Type != directx.TestType.Immediate)
                        D3D.commandsList[contextIndex] = renderingContext.FinishCommandList(false);
                }


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
                //updateprim.updatescriptsupdatetext(D3D.DeviceContext);

                //updateprim.updatescriptsupdatetext(D3D.DeviceContext);

                //Task.WaitAll(tasks);
                hasfinishedRenderDeferred = 1;
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

            //updateprim.updatescriptsupdatetext(D3D.DeviceContext);
            // Setup the pipeline before any rendering
            SetupPipeline();
            //updateprim.updatescriptsupdatetext(D3D.DeviceContext);
            // Execute on the rendering thread when ThreadCount == 1 or No deferred rendering is selected
            if (D3D.currentState.Type == directx.TestType.Immediate || (D3D.currentState.Type == directx.TestType.Deferred && D3D.currentState.ThreadCount == 1))
            {
                renderrow(0, 0, D3D.currentState.CountCubes);
                //updateprim.updatescriptsupdatetext(D3D.DeviceContext);
            }

            //updateprim.updatescriptsupdatetext(D3D.DeviceContext);



            // In case of deferred context, use of FinishCommandList / ExecuteCommandList
            if (D3D.currentState.Type != directx.TestType.Immediate)
            {
                if (D3D.currentState.Type != directx.TestType.FrozenDeferred)
                {
                    updateprim.updatescriptsupdatetext(D3D.DeviceContext);
                }


                
                if (D3D.currentState.Type == directx.TestType.FrozenDeferred)
                {
                    //updateprim.updatescriptsupdatetext(D3D.DeviceContext);
                    //Console.WriteLine("frozendeferred");
                    if (D3D.commandsList[0] == null)
                        RenderDeferred(1);
                    //updateprim.updatescriptsupdatetext(D3D.DeviceContext);
                }
                else if (D3D.currentState.ThreadCount > 1)
                {
                    RenderDeferred(D3D.currentState.ThreadCount);
                }

              

                for (int i = 0; i < D3D.currentState.ThreadCount; i++)
                {
                    var commandList = D3D.commandsList[i];

                    if (D3D != null)
                    {
                        if (D3D.DeviceContext!= null)
                        {
                            if (commandList != null)
                            {
                                // Execute the deferred command list on the immediate context
                                D3D.DeviceContext.ExecuteCommandList(commandList, false);
                                //updateprim.updatescriptsupdatetext(D3D.DeviceContext);
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
                    }                        
                }
            }
            //updateprim.updatescriptsupdatetext(D3D.DeviceContext);
            if (switchToNextState)
            {
                D3D.currentState = D3D.nextState;
                switchToNextState = false;
            }

            //updateprim.updatescriptsupdatetext(D3D.DeviceContext);



            //DRAWING SOMETHING TO THE SCENE TO THEN DRAW THE FONT IS WORKING FOR THE FONT. OTHERWISE THE FONT ISN'T SHOWING FOR DEFERRED AND FROZEN BUT ONLY SHOWING IN IMMEDIATE
            //DRAWING SOMETHING TO THE SCENE TO THEN DRAW THE FONT IS WORKING FOR THE FONT. OTHERWISE THE FONT ISN'T SHOWING FOR DEFERRED AND FROZEN BUT ONLY SHOWING IN IMMEDIATE
            //DRAWING SOMETHING TO THE SCENE TO THEN DRAW THE FONT IS WORKING FOR THE FONT. OTHERWISE THE FONT ISN'T SHOWING FOR DEFERRED AND FROZEN BUT ONLY SHOWING IN IMMEDIATE
            /*D3D.DeviceContext.InputAssembler.InputLayout = somecube.layout;
            D3D.DeviceContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
            D3D.DeviceContext.InputAssembler.SetIndexBuffer(somecube.IndicesBuffer, SharpDX.DXGI.Format.R32_UInt, 0);

            //renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecube.verticesbuffer, Utilities.SizeOf<tutorialcubeaschunk.DVertex>(), 0));
            D3D.DeviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(somecube.verticesbuffer, Utilities.SizeOf<Vector4>() * 2, 0));
            D3D.DeviceContext.VertexShader.SetConstantBuffer(0, D3D.currentState.UseMap ? somecube.dynamicConstantBuffer : somecube.staticContantBuffer);
            D3D.DeviceContext.VertexShader.Set(somecube.vertexShader);
            D3D.DeviceContext.Rasterizer.SetViewport(0, 0, D3D.SurfaceWidth, D3D.SurfaceHeight);
            D3D.DeviceContext.PixelShader.Set(somecube.pixelShader);
            D3D.DeviceContext.OutputMerger.SetTargets(D3D.DepthStencilView, D3D.RenderTargetView);

            var view = updateprim.camera.ViewMatrix;// Matrix.LookAtLH(new Vector3(0, 0, -viewZ), new Vector3(0, 0, 0), Vector3.UnitY);
            var proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, D3D.SurfaceWidth / (float)D3D.SurfaceHeight, 0.1f, 1000.0f);
            var viewProj = Matrix.Multiply(view, D3D.ProjectionMatrix);


            Matrix rotateMatrix = Matrix.Identity; //Matrix.Identity; //Matrix.Scaling(1.0f / 1.0f) * 
            Matrix worldViewProj;
            Matrix.Multiply(ref rotateMatrix, ref viewProj, out worldViewProj);
            worldViewProj.Transpose();

            if (D3D.currentState.UseMap)
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
            //DRAWING SOMETHING TO THE SCENE TO THEN DRAW THE FONT IS WORKING FOR THE FONT. OTHERWISE THE FONT ISN'T SHOWING FOR DEFERRED AND FROZEN BUT ONLY SHOWING IN IMMEDIATE
            //DRAWING SOMETHING TO THE SCENE TO THEN DRAW THE FONT IS WORKING FOR THE FONT. OTHERWISE THE FONT ISN'T SHOWING FOR DEFERRED AND FROZEN BUT ONLY SHOWING IN IMMEDIATE
            //DRAWING SOMETHING TO THE SCENE TO THEN DRAW THE FONT IS WORKING FOR THE FONT. OTHERWISE THE FONT ISN'T SHOWING FOR DEFERRED AND FROZEN BUT ONLY SHOWING IN IMMEDIATE
            //updateprim.updatescriptsupdatetext(D3D.DeviceContext);






            /*
            //DRAWING SOMETHING TO THE SCENE TO THEN DRAW THE FONT IS WORKING FOR THE FONT. OTHERWISE THE FONT ISN'T SHOWING FOR DEFERRED AND FROZEN BUT ONLY SHOWING IN IMMEDIATE
            //DRAWING SOMETHING TO THE SCENE TO THEN DRAW THE FONT IS WORKING FOR THE FONT. OTHERWISE THE FONT ISN'T SHOWING FOR DEFERRED AND FROZEN BUT ONLY SHOWING IN IMMEDIATE
            //DRAWING SOMETHING TO THE SCENE TO THEN DRAW THE FONT IS WORKING FOR THE FONT. OTHERWISE THE FONT ISN'T SHOWING FOR DEFERRED AND FROZEN BUT ONLY SHOWING IN IMMEDIATE
            D3D.DeviceContext.InputAssembler.InputLayout = tutorialobj.layout;
            D3D.DeviceContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
            D3D.DeviceContext.InputAssembler.SetIndexBuffer(tutorialobj.IndicesBuffer, SharpDX.DXGI.Format.R32_UInt, 0);

            //renderingContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(tutorialobj.verticesbuffer, Utilities.SizeOf<tutorialcubeaschunk.DVertex>(), 0));
            D3D.DeviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(tutorialobj.verticesbuffer, Utilities.SizeOf<Vector4>() * 1, 0));
            D3D.DeviceContext.VertexShader.SetConstantBuffer(0, D3D.currentState.UseMap ? tutorialobj.dynamicConstantBuffer : tutorialobj.staticContantBuffer);
            D3D.DeviceContext.VertexShader.Set(tutorialobj.vertexShader);
            D3D.DeviceContext.Rasterizer.SetViewport(0, 0, D3D.SurfaceWidth, D3D.SurfaceHeight);
            D3D.DeviceContext.PixelShader.Set(tutorialobj.pixelShader);
            D3D.DeviceContext.OutputMerger.SetTargets(D3D.DepthStencilView, D3D.RenderTargetView);

            Matrix view = updateprim.camera.ViewMatrix;// Matrix.LookAtLH(new Vector3(0, 0, -viewZ), new Vector3(0, 0, 0), Vector3.UnitY);
            Matrix proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, D3D.SurfaceWidth / (float)D3D.SurfaceHeight, 0.1f, 1000.0f);
            Matrix viewProj = Matrix.Multiply(view, D3D.ProjectionMatrix);

            Matrix rotateMatrix = Matrix.Identity; //Matrix.Identity; //Matrix.Scaling(1.0f / 1.0f) * 
            Matrix worldViewProj;
            Matrix.Multiply(ref rotateMatrix, ref viewProj, out worldViewProj);
            worldViewProj.Transpose();

            if (D3D.currentState.UseMap)
            {
                var dataBox = D3D.DeviceContext.MapSubresource(tutorialobj.dynamicConstantBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
                Utilities.Write(dataBox.DataPointer, ref worldViewProj);
                D3D.DeviceContext.UnmapSubresource(tutorialobj.dynamicConstantBuffer, 0);
            }
            else
            {
                D3D.DeviceContext.UpdateSubresource(ref worldViewProj, tutorialobj.staticContantBuffer);
            }

            // Draw the cube 
            //D3D.DeviceContext.Draw(24, 0);
            D3D.DeviceContext.DrawIndexed(tutorialobj.arrayoftrigs.Length, 0, 0);
            //D3D.DeviceContext.Draw(tutorialobj.vertices.Length, 0);
            //DRAWING SOMETHING TO THE SCENE TO THEN DRAW THE FONT IS WORKING FOR THE FONT. OTHERWISE THE FONT ISN'T SHOWING FOR DEFERRED AND FROZEN BUT ONLY SHOWING IN IMMEDIATE
            //DRAWING SOMETHING TO THE SCENE TO THEN DRAW THE FONT IS WORKING FOR THE FONT. OTHERWISE THE FONT ISN'T SHOWING FOR DEFERRED AND FROZEN BUT ONLY SHOWING IN IMMEDIATE
            //DRAWING SOMETHING TO THE SCENE TO THEN DRAW THE FONT IS WORKING FOR THE FONT. OTHERWISE THE FONT ISN'T SHOWING FOR DEFERRED AND FROZEN BUT ONLY SHOWING IN IMMEDIATE
            updateprim.updatescriptsupdatetext(D3D.DeviceContext);*/








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
