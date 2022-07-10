//DEVELOPED BY STEVE CHASSÉ

using SharpDX.Windows;
using System.Threading;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Diagnostics;

namespace sccsr15forms
{
    internal static class Program
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        public static bool runapptype = false;
        public static bool lastrunapptype = false;
        //runapptype == 0 => running in a system.thread
        //runapptype == 1 => running in sharpdx renderloop

        static Thread mainthreadloop;

        public static directx directx;
        public static updatePrim updateprim;
        public static updateSec updatesec;

        static int startmainthread = 0;
        public static Stopwatch clock;
        public static Stopwatch fpsTimer;



        static int createtimers = 0;
        public static int fpsCounter = 0;
        static int hasfinishedwork = 0;
        static int hasfinishedworkuithread = 0;
        static int hasfinishedworksystemthread = 0;

        static int lasthasfinishedworkuithread = 0;
        static int lasthasfinishedworksystemthread = 0;

        static int canworkuithread = 1;
        static int canworksysthread = 2;



        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());

            //ApplicationConfiguration.Initialize();
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            // sccsr14sc.Form1.someform = new Form1();
            sccsr15forms.Form1.currentform = new Form1();

            AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;



            if (runapptype)
            {
                canworkuithread = 1;
                canworksysthread = 2;
            }
            if (!runapptype)
            {
                canworkuithread = 2;
                canworksysthread = 1;
            }





            //Application.Run(sccsr15forms.Form1.currentform);
            RenderLoop.Run(sccsr15forms.Form1.currentform, () =>
            {

                if (sccsr15forms.Form1.currentform.formwasloadedswtc == 1 && startmainthread == 0)
                {
                    directx = new directx();
                    updateprim = new updatePrim(directx);
                    updatesec = new updateSec(updateprim, directx);



                    if (createtimers == 0)
                    {
                        // Use clock 
                        clock = new Stopwatch();
                        clock.Start();

                        fpsTimer = new Stopwatch();
                        fpsTimer.Start();

                        createtimers = 1;

                    }



                    //Console.WriteLine("test");

                    // Install keys handlers 
                    sccsr15forms.Form1.currentform.KeyDown += (target, arg) =>
                    {
                        if (arg.KeyCode == Keys.Left && directx.nextState.CountCubes > 1)
                            directx.nextState.CountCubes--;
                        if (arg.KeyCode == Keys.Right && directx.nextState.CountCubes < directx.MaxNumberOfCubes)
                            directx.nextState.CountCubes++;

                        if (arg.KeyCode == Keys.F1)
                            directx.nextState.Type = (directx.TestType)((((int)directx.nextState.Type) + 1) % 3);
                        if (arg.KeyCode == Keys.F2)
                            directx.nextState.UseMap = !directx.nextState.UseMap;
                        if (arg.KeyCode == Keys.F3)
                        {
                            Console.WriteLine("pressed F3");
                            directx.nextState.SimulateCpuUsage = !directx.nextState.SimulateCpuUsage;
                        }
                        if (arg.KeyCode == Keys.F4)
                        {
                            //threadCount = D3D.currentState.Type == directx.TestType.Deferred ? D3D.currentState.ThreadCount : 1;
                            //Console.WriteLine("pressed F3");
                            //runapptype = true || false & true;//: true;// true == true ? false : true;
                            runapptype = runapptype == false ? true : false;
                        }

                        if (directx.nextState.Type == directx.TestType.Deferred)
                        {
                            if (arg.KeyCode == Keys.Down && directx.nextState.ThreadCount > 1)
                                directx.nextState.ThreadCount--;
                            if (arg.KeyCode == Keys.Up && directx.nextState.ThreadCount < directx.MaxNumberOfThreads)
                                directx.nextState.ThreadCount++;
                        }
                        if (arg.KeyCode == Keys.Escape)
                            directx.nextState.Exit = true;
                        updatesec.switchToNextState = true;
                    };

                    //somedirectx = new directx();
                    sccsr15forms.Form1.currentform.formwasloadedswtc = 2;
                }

                //Console.WriteLine(runapptype);

                hasfinishedwork = 0;

                bool somerunapptype = false;

                if (lastrunapptype != runapptype) // && runapptype == true //  somerunapptype = true;
                {
                    if (!runapptype)
                    {
                        canworkuithread = 1;
                        canworksysthread = 2;
                    }
                    else if (runapptype)
                    {
                        canworkuithread = 2;
                        canworksysthread = 1;

                    }
                }


                //Console.WriteLine("canworkuithread:" + canworkuithread);

                //Console.WriteLine(runapptype);

                if (runapptype)
                {
                    if (startmainthread == 0)
                    {


                        mainthreadloop = new Thread(() =>
                        {

                        loopgoto:

                            /*
                            if (lastrunapptype != runapptype) // && runapptype == true //  somerunapptype = true;
                            {
                                if (!runapptype)
                                {
                                    canworksysthread = 0;
                                }
                                else if (runapptype)
                                {
                                    canworksysthread = 1;

                                }
                            }*/


                            if (lastrunapptype != runapptype && runapptype == false)
                            {
                                Thread.Sleep(1);
                            }
                            else
                            {


                                if (canworksysthread == 1 && canworkuithread == 2)
                                {

                                    //Console.WriteLine("working sys thread");
                                    updateprim.updatescriptsprimstartrender();
                                    hasfinishedworksystemthread = updatesec.updatescriptssec(somerunapptype);
                                    updateprim.updatescriptsprimstoprender();

                                }

                                if (canworksysthread == 0)
                                {
                                    Console.WriteLine("starting sys thread. disposing UI stuff0");

                                    for (int i = 0; i < directx.currentState.ThreadCount; i++)
                                    {
                                        var commandList = directx.commandsListUIThread[i];
                                        // Execute the deferred command list on the immediate context
                                        directx.DeviceContext.ExecuteCommandList(commandList, false);

                                        // For classic deferred we release the command list. Not for frozen
                                        if (directx.currentState.Type == directx.TestType.Deferred)
                                        {
                                            if (commandList != null)
                                            {
                                                // Release the command list
                                                commandList.Dispose();
                                            }

                                            directx.commandsListUIThread[i] = null;
                                        }
                                    }


                                    if (updatesec != null)
                                    {
                                        if (updatesec.tasks != null)
                                        {
                                            for (int t = 0; t < updatesec.tasks.Length; t++)
                                            {
                                                if (updatesec.tasks[t] != null)
                                                {
                                                    updatesec.tasks[t].Wait();
                                                    updatesec.tasks[t].Dispose();
                                                }

                                            }
                                        }
                                    }
                                    canworksysthread = 2;
                                    Console.WriteLine("starting sys thread. disposing UI stuff");

                                    //hasfinishedworkuithread = 0;
                                    //mainthreadloop.Join();
                                    //mainthreadloop.Suspend();
                                }
                            }


                            /*
                            if (lastrunapptype != runapptype && runapptype == false)
                            {
                                Thread.Sleep(1);
                            }
                            else
                            {
                                updateprim.updatescriptsprimstartrender();
                                hasfinishedworksystemthread = updatesec.updatescriptssec(somerunapptype);
                                updateprim.updatescriptsprimstoprender();

                                /*if (updatesec.hasfinishedSetupPipeline == 1 && updatesec.hasfinishedRenderRow == 1 && updatesec.hasfinishedRenderDeferred == 1)
                                {
                                    
                                }
                            }*/


                            //Console.WriteLine("UIThread:" + updateprim.counteruithread + "/SystemThread:" + updateprim.countersystemthread);
                            Thread.Sleep(1);
                            goto loopgoto;

                        }, 0); //100000

                        //Process.GetCurrentProcess().Exited

                        mainthreadloop.IsBackground = true;
                        mainthreadloop.Priority = ThreadPriority.Lowest;
                        mainthreadloop.SetApartmentState(ApartmentState.STA);
                        mainthreadloop.Start();


                        startmainthread = 1;
                    }
                }
                else //if (!runapptype)
                {

                    if (canworkuithread == 1 && canworksysthread == 2)
                    {
                        //if (updatesec.hasfinishedSetupPipeline == 1 && updatesec.hasfinishedRenderRow == 1 && updatesec.hasfinishedRenderDeferred == 1)
                        {
                            //Console.WriteLine("working UI thread");
                            updateprim.updatescriptsprimstartrender();
                            hasfinishedworkuithread = updatesec.updatescriptssec(somerunapptype);
                            updateprim.updatescriptsprimstoprender();
                        }

                    }

                }

                if (canworkuithread == 0)
                {
                    Console.WriteLine("starting UI thread. disposing systhread stuff 0");
                    //MessageBox((IntPtr)0, "", "msg", 0);

                    //Console.WriteLine("starting UI thread. disposing systhread stuff 0");

                    for (int i = 0; i < directx.currentState.ThreadCount; i++)
                    {
                        var commandList = directx.commandsListSysThread[i];
                        // Execute the deferred command list on the immediate context
                        directx.DeviceContext.ExecuteCommandList(commandList, false);

                        // For classic deferred we release the command list. Not for frozen
                        if (directx.currentState.Type == directx.TestType.Deferred)
                        {
                            if (commandList != null)
                            {
                                // Release the command list
                                commandList.Dispose();
                            }

                            directx.commandsListSysThread[i] = null;
                        }
                    }

                    if (updatesec != null)
                    {
                        if (updatesec.tasks != null)
                        {
                            for (int t = 0; t < updatesec.tasks.Length; t++)
                            {
                                if (updatesec.tasks[t] != null)
                                {
                                    updatesec.tasks[t].Wait();
                                    updatesec.tasks[t].Dispose();
                                }

                            }
                        }
                    }

                    canworkuithread = 2;

                    Console.WriteLine("starting UI thread. disposing systhread stuff 1");


                    //hasfinishedworksystemthread = 0;
                    //mainthreadloop.Join();
                    //mainthreadloop.Suspend();
                }








                fpsCounter++;
                if (fpsTimer.ElapsedMilliseconds > 1000)
                {
                    var typeStr = directx.currentState.Type.ToString();
                    if (directx.currentState.Type != directx.TestType.Immediate && !directx.supportCommandList) typeStr += "*";

                    //-(Multithread){0}
                    //sccsr15forms.Form1.currentform.Text = string.Format("SharpDX - MultiCube D3D11 - (Multithread) {0} - (F1) {0} - (F2) {1} - (F3) {2} - Threads ↑↓{3} - Count ←{4}→ - FPS: {5:F2} ({6:F2}ms)", runapptype, typeStr, directx.currentState.UseMap ? "Map/UnMap" : "UpdateSubresource", directx.currentState.SimulateCpuUsage ? "BurnCPU On" : "BurnCpu Off", directx.currentState.Type == directx.TestType.Deferred ? directx.currentState.ThreadCount : 1, directx.currentState.CountCubes * directx.currentState.CountCubes, 1000.0 * fpsCounter / fpsTimer.ElapsedMilliseconds, (float)fpsTimer.ElapsedMilliseconds / fpsCounter);
                    sccsr15forms.Form1.currentform.Text = string.Format("SharpDX - MultiCube D3D11  - (F1) {0} - (F2) {1} - (F3) {2} - Threads ↑↓{3} - Count ←{4}→ - FPS: {5:F2} ({6:F2}ms)  - Multithread: (F4) {7}", typeStr, directx.currentState.UseMap ? "Map/UnMap" : "UpdateSubresource", directx.currentState.SimulateCpuUsage ? "BurnCPU On" : "BurnCpu Off", directx.currentState.Type == directx.TestType.Deferred ? directx.currentState.ThreadCount : 1, directx.currentState.CountCubes * directx.currentState.CountCubes, 1000.0 * fpsCounter / fpsTimer.ElapsedMilliseconds, (float)fpsTimer.ElapsedMilliseconds / fpsCounter, runapptype);
                    fpsTimer.Reset();
                    fpsTimer.Stop();
                    fpsTimer.Start();
                    fpsCounter = 0;
                }

                //Console.WriteLine("UIThread:" + updateprim.counteruithread + "/SystemThread:" + updateprim.countersystemthread);

                //Console.WriteLine(runapptype);
                lastrunapptype = runapptype;

                lasthasfinishedworkuithread = hasfinishedworkuithread;
                lasthasfinishedworksystemthread = hasfinishedworksystemthread;

                Thread.Sleep(1);
            });
        }








        static void ProcessExitHandler(object sender, EventArgs e)
        {
            if (updatePrim.currentupdatePrim != null)
            {
                updatePrim.currentupdatePrim.Dispose();
            }
            if (updateSec.currentupdatesec != null)
            {
                updateSec.currentupdatesec.Dispose();

            }
            if (directx.D3D != null)
            {
                directx.D3D.Dispose();
            }

            updatePrim.currentupdatePrim = null;
            updateSec.currentupdatesec = null;
            directx.D3D = null;

            Environment.Exit(0);
            Application.Exit();

            //Console.WriteLine("closing app");
            //MessageBox((IntPtr)0, "closing app", "window name", 0);
        }


    }
}