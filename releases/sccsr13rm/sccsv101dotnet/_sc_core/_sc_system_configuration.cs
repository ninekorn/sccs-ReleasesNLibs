
namespace _sc_core_systems._sc_core
{
    public class _sc_system_configuration                   // 58 lines
    {
        // Properties
        public string Title { get; set; }
        public int Width = 0;//{ get; set; }
        public int Height = 0;// { get; set; }

        // Static Variables.
        public static bool FullScreen { get; private set; }
        public static bool VerticalSyncEnabled { get; private set; }
        public static float ScreenDepth { get; private set; }
        public static float ScreenNear { get; private set; }
        //public static FormBorderStyle BorderStyle { get; private set; }
        public static string VertexShaderProfile = "vs_5_0";
        public static string PixelShaderProfile = "ps_5_0";
        public static string ShaderFilePath { get; private set; }
        public static string DataFilePath { get; private set; }
        public static string ModelFilePath { get; set; }

        // Constructors
        //public _sc_system_configuration(bool fullScreen, bool vSync) : this("SharpDX Demo", fullScreen, vSync) { }
        //public _sc_system_configuration(string title, int width, int height,bool fullScreen, bool vSync) :this(title, 800, 600, fullScreen, vSync) { }
        public _sc_system_configuration(string title, int width, int height, bool fullScreen, bool vSync)
        {
            //FullScreen = fullScreen;
            Title = title;
            VerticalSyncEnabled = vSync;
            Width = width;
            Height = height;
            if (!FullScreen)
            {
                Width = width;
                Height = height;
            }
            else
            {
                //Width = Screen.PrimaryScreen.Bounds.Width;
                //Height = Screen.PrimaryScreen.Bounds.Height;
            }
        }

        // Static Constructor
        static _sc_system_configuration()
        {
            FullScreen = false;
            VerticalSyncEnabled = false;
            ScreenDepth = 100f;   // 1000.0f
            ScreenNear = 0.1f;      // 0.1f
            //BorderStyle = FormBorderStyle.None;

            ShaderFilePath = @"SC_Graphics\SC_ShaderFiles\";

            //C:\Users\steve\source\
            //DataFilePath = @"Externals\Data\";
            //ModelFilePath = @"Externals\Models\";
        }
    }
}