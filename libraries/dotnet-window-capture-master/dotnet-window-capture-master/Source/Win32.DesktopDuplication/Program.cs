using System;

using Win32.Shared;
using System.Diagnostics;
using SharpDX.DXGI;


namespace Win32.DesktopDuplication
{
    internal static class Program
    {
        static IntPtr vewindowsfoundedz;
        [STAThread]
        public static void Main()
        {

            Process[] processlist = Process.GetProcesses();

            foreach (Process process in processlist)
            {
                if (process.ProcessName.ToLower() == "voidexpanse")
                {
                    //Console.WriteLine("Process: {0} ID: {1} Window title: {2}", process.ProcessName, process.Id, process.MainWindowTitle);
                    //MessageBox((IntPtr)0, "ED" + " " + process.MainWindowHandle, "sccoresystems0", 0);
                    vewindowsfoundedz = process.MainWindowHandle;
                }
            }

            const int numAdapter = 0;
            const int numOutput = 0;
            var factory = new Factory1();
            factory.MakeWindowAssociation(Program.vewindowsfoundedz, WindowAssociationFlags.IgnoreAll);
            var adapter = factory.GetAdapter1(numAdapter);
            var deviceforscreencap = new SharpDX.Direct3D11.Device(adapter);


            DesktopDuplication desktopdupe = new DesktopDuplication();
            //desktopdupe.StartCapture(vewindowsfoundedz, deviceforscreencap, factory);


            var window = new DxWindow(".NET Window Capture Samples - Win32.DesktopDuplication", desktopdupe);
            //window.Show();
            //desktopdupe.StopCapture();
            //desktopdupe.StartCapture(vewindowsfoundedz, deviceforscreencap, factory);
            window.Show();
            //var window = new DxWindow(".NET Window Capture Samples - Win32.DesktopDuplication", new DesktopDuplication());
            //window.Show();
            //desktopdupe.StopCapture();
            //desktopdupe.StartCapture(vewindowsfoundedz, window.device, factory);



        }
    }
}