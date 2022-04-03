using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

using Win32.Shared.Interop;

using System.Runtime.InteropServices;

namespace Win32.Shared
{
    /// <summary>
    ///     MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class WindowPicker : Window, IDisposable
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        //[DllImport("User32.dll", CharSet = CharSet.Unicode)]
        //public static extern int MessageBox(IntPtr h, string m, string c, int type);



        private string[] _ignoreProcesses = { "applicationframehost", "shellexperiencehost", "systemsettings", "winstore.app", "searchui" };

        public string selectedwindowname = "";

        public void Dispose()
        {
            DeleteObject(somewininterophelper);
            DeleteObject(valreturn);

            if (wih!= null)
            {
                DeleteObject(wih.Handle);
            }
            wih = null;
            process?.Dispose();
            process = null;
            title = null;

            for (int i = 0;i < (Windows.Items.Count);i++)
            {
                DeleteObject(((CapturableWindow?)Windows.Items[i]).Value.Handle);
            }
            _ignoreProcesses = null;
        }
        StringBuilder title;

        public WindowPicker()
        {
            InitializeComponent();
            Loaded += OnLoaded;

            //this.Cursor = Cursors.None;

        }



        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            FindWindows();
        }
        Process process;
        IntPtr valreturn;

        IntPtr somewininterophelper;
        public IntPtr PickCaptureTarget(IntPtr hWnd)
        {
            somewininterophelper = new WindowInteropHelper(this).Owner = hWnd;
            ShowDialog();

            //(CapturableWindow?)Windows.SelectedItem.
            //var selecteditem = Windows.SelectedItem;

            valreturn = ((CapturableWindow?)Windows.SelectedItem)?.Handle ?? IntPtr.Zero;

            selectedwindowname = ((CapturableWindow?)Windows.SelectedItem)?.Title;

            return valreturn;
        }


        WindowInteropHelper wih;
        private void FindWindows()
        {
            wih = new WindowInteropHelper(this);
            NativeMethodsother.EnumWindows((hWnd, lParam) =>
            {
                // ignore invisible windows
                if (!NativeMethodsother.IsWindowVisible(hWnd))
                    return true;

                // ignore untitled windows
                title = new StringBuilder(1024);
                NativeMethodsother.GetWindowText(hWnd, title, title.Capacity);
                if (string.IsNullOrWhiteSpace(title.ToString()))
                    return true;

                // ignore me
                if (wih.Handle == hWnd)
                    return true;

                NativeMethodsother.GetWindowThreadProcessId(hWnd, out var processId);

                // ignore by process name
                process = Process.GetProcessById((int)processId);
                if (_ignoreProcesses.Contains(process.ProcessName.ToLower()))
                    return true;

                Windows.Items.Add(new CapturableWindow
                {
                    Handle = hWnd,
                    Name = $"{title} ({process.ProcessName}.exe)",
                    Title = $"{title}"
                });

                return true;
            }, IntPtr.Zero);
        }

        private void WindowsOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Dispose();
            //Hide();
            Close();
        }
    }

    public struct CapturableWindow
    {
        public string Name { get; set; }
        public IntPtr Handle { get; set; }
        public string Title { get; set; }
    }
}