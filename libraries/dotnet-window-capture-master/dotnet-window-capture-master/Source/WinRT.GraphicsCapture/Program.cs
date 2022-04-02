using System;

using Win32.Shared;

namespace WinRT.GraphicsCapture
{
    internal static class Program
    {
        [STAThread]
        public static void Main()
        {
            using var window = new DxWindow(".NET Window Capture Samples - WinRT.GraphicsCapture", new GraphicsCapture());
            window.Show();
        }
    }
}