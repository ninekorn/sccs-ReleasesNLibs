//DEVELOPED BY STEVE CHASSÉ

namespace sccsr15forms
{
    public partial class Form1 : Form
    {
        public static Form1 currentform;
        public static IntPtr theHandle;

        public Form1()
        {
            currentform = this;
            InitializeComponent();
            this.Load += Form1_Load;
        }
        public int formwasloadedswtc = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            theHandle = this.Handle;
            formwasloadedswtc = 1;
        }

    }
}