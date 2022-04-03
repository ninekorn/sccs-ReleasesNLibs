using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpDX;
using SharpDX.Direct3D11;

using System.Drawing;


using SharpDX.D3DCompiler;

using System.Runtime.InteropServices;
//using System.Windows.Forms;



namespace sccs
{
    public struct sccsscreenframe
    {
        public int width;
        public int height;
        public byte[] bitmapByteArray;
        public byte[] bitmapEmptyByteArray;
        public byte[] frameByteArray;
        //public Bitmap mouseCursor;
        //public byte[] cursorByteArray;
        //public byte[] cursorByteArray;

        public byte[][] arrayOfFRACSCREENSPECTRUMBytes;

        public byte[][] screencapturearrayofbytes;

        public System.Drawing.Point cursorPointPos;

        public int _desktopWidth;
        public int _desktopHeight;
        public ShaderResourceView ShaderResource;
        public ShaderResourceView[] ShaderResourceArray;
        public Texture2D _texture2DFinal;
        public Bitmap somebitmapforarduino;
        public int memorymapstride;
    }
}
