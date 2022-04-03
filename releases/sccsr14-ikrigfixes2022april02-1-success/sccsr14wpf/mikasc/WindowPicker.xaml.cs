using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

using Win32.Shared.Interop;

namespace Win32.Shared
{
    /// <summary>
    ///     MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class WindowPicker : Window
    {
        private readonly string[] _ignoreProcesses = { "applicationframehost", "shellexperiencehost", "systemsettings", "winstore.app", "searchui" };

        public string selectedwindowname = "";

        public WindowPicker()
        {
            InitializeComponent();
            Loaded += OnLoaded;

            this.Cursor = Cursors.None;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            FindWindows();
        }

        public IntPtr PickCaptureTarget(IntPtr hWnd)
        {
            new WindowInteropHelper(this).Owner = hWnd;
            ShowDialog();

            //(CapturableWindow?)Windows.SelectedItem.
            //var selecteditem = Windows.SelectedItem;

            var valreturn = ((CapturableWindow?)Windows.SelectedItem)?.Handle ?? IntPtr.Zero;

            selectedwindowname = ((CapturableWindow?)Windows.SelectedItem)?.Title;

            return valreturn;
        }

        private void FindWindows()
        {
            var wih = new WindowInteropHelper(this);
            NativeMethodsother.EnumWindows((hWnd, lParam) =>
            {
                // ignore invisible windows
                if (!NativeMethodsother.IsWindowVisible(hWnd))
                    return true;

                // ignore untitled windows
                var title = new StringBuilder(1024);
                NativeMethodsother.GetWindowText(hWnd, title, title.Capacity);
                if (string.IsNullOrWhiteSpace(title.ToString()))
                    return true;

                // ignore me
                if (wih.Handle == hWnd)
                    return true;

                NativeMethodsother.GetWindowThreadProcessId(hWnd, out var processId);

                // ignore by process name
                var process = Process.GetProcessById((int) processId);
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