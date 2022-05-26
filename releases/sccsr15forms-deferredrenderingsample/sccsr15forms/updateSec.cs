//DEVELOPED BY STEVE CHASSÉ

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


namespace sccsr15forms
{
    internal class updateSec : IDisposable
    {
        public static updateSec currentupdatesec;// = Instance;

        public tutorialcube somecube;
        private directx D3D;
        public updatePrim updateprim;

        Func<int, int, int, int> renderrow;
        Action<int> RenderDeferred;
        //Action<int, int, int> RenderRow;
        Action SetupPipeline;

        public bool switchToNextState = false;

        public updateSec(updatePrim updateprim_, directx D3D_)
        {
            D3D = D3D_;
            currentupdatesec = this;
            updateprim = updateprim_;

            Console.WriteLine("created updatesec");

            somecube = new tutorialcube(D3D.Device);

            const float viewZ = 5.0f;
            //updateprim_.camera.ViewMatrix;//
            // Prepare matrices 
            var view = Matrix.LookAtLH(new Vector3(0, 0, -viewZ), new Vector3(0, 0, 0), Vector3.UnitY);
            var proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, D3D.SurfaceWidth / (float)D3D.SurfaceHeight, 0.1f, 1000.0f);


            var viewProj = Matrix.Multiply(view, D3D.ProjectionMatrix);


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
