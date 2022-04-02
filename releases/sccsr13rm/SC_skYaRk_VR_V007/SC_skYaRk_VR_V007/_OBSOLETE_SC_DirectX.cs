using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
//using System.Windows.Forms;

using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;

using SharpDX.WIC;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

//using System.Windows.Media;
using System.Collections;

using System.Threading;
//using System.Windows.Media.Imaging;

using Matrix = SharpDX.Matrix;

using SharpDX.DirectInput;

using ovrSession = System.IntPtr;
using ovrTextureSwapChain = System.IntPtr;
using ovrMirrorTexture = System.IntPtr;
using Result = Ab3d.OculusWrap.Result;

using Ab3d.OculusWrap;
using System.ComponentModel;

using Ab3d.OculusWrap.DemoDX11;

namespace SC_skYaRk_VR_Edition_v004
{
    public class SC_DirectX
    {

        //[StructLayout(LayoutKind.Sequential)]
        /*public struct DInstanceType
        {
            public Vector3 position;
        };*/

        public static SharpDX.Matrix originRot = SharpDX.Matrix.Identity;
        public static SharpDX.Matrix rotatingMatrix = SharpDX.Matrix.Identity;


        //public static DShaderManager shaderManager { get; set; }
        public static Device device;

        public static DeviceContext context;

        public static SharpDX.DirectInput.Keyboard _Keyboard;// { get; set; }

        Vector3 VRPos = new Vector3(0, 0, 0);

        public VertexShader VertexShader;
        public PixelShader PixelShader;

        public InputLayout Layout;
        //SC_ThreadPool threadPool;
        //public static System.Windows.Forms.Control MainControl;

        public static float RotationY { get; set; }
        public static float RotationX { get; set; }
        public static float RotationZ { get; set; }

        Vector3 originPos = new Vector3(0, 1, 0);
        Matrix _WorldMatrix = Matrix.Identity;
        DMatrixBuffer[] arrayOfMatrixBuff = new DMatrixBuffer[1];
        DLightBuffer[] lightBuffer = new DLightBuffer[1];

       
        [StructLayout(LayoutKind.Sequential)]
        public struct DMatrixBuffer
        {
            public Matrix world;
            public Matrix view;
            public Matrix proj;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DLightBuffer
        {
            public Vector4 ambientColor;
            public Vector4 diffuseColor;
            public Vector3 lightDirection;
            public float padding; // Added extra padding so structure is a multiple of 16.
        }


        public SC_DirectX()
        {
            var form = new RenderForm("SharpDX - MiniCube Direct3D11 Sample");

            try
            {
                int startOnce = 1;

                RenderLoop.Run(form, () =>
                {
                    if (startOnce == 1)
                    {
                        Func<int> formatDelegate0 = () =>
                        {
                            return 1;
                        };
                        var t1 = new Task<int>(formatDelegate0);
                        t1.RunSynchronously();
                        t1.Dispose();
                    }
                });
            }
            finally
            {

            }
        }


        public void renderOculus()
        {
            try
            {
                
            }
            catch
            {

            }
            finally
            {

            }
        }













        /*private void callFunctionSafe(Func<int> text, RenderForm form)
        {
            var test = new SafeCallDelegate(callFunctionSafe);

            var result = form.BeginInvoke(text);
            form.EndInvoke(result);

        }*/
        private delegate void SafeCallDelegate(Func<int> someFunction, RenderForm form);

        //public static List<chunkData> queueOfFunctions = new List<chunkData>();
        //public static List<Func<int>> queueOfFunctions = new List<Func<int>>();

        //public static Queue<chunkData> queueOfFunctions = new Queue<chunkData>();

        public static Queue<Func<int>> queueOfFunctions = new Queue<Func<int>>();

        Stopwatch timeWatch = new Stopwatch();

        KeyboardState _KeyboardState;
        private bool ReadKeyboard()
        {
            var resultCode = SharpDX.DirectInput.ResultCode.Ok;
            _KeyboardState = new KeyboardState();

            try
            {
                // Read the keyboard device.
                _Keyboard.GetCurrentState(ref _KeyboardState);
            }
            catch (SharpDX.SharpDXException ex)
            {
                resultCode = ex.Descriptor; // ex.ResultCode;
            }
            catch (Exception)
            {
                return false;
            }

            // If the mouse lost focus or was not acquired then try to get control back.
            if (resultCode == SharpDX.DirectInput.ResultCode.InputLost || resultCode == SharpDX.DirectInput.ResultCode.NotAcquired)
            {
                try
                {
                    _Keyboard.Acquire();
                }
                catch
                { }

                return true;
            }

            if (resultCode == SharpDX.DirectInput.ResultCode.Ok)
                return true;

            return false;
        }

        public static void Dispose(IDisposable disposable)
        {
            if (disposable != null)
                disposable.Dispose();
        }
    }
}







/*SharpDX.Vector3 eyePos = new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z);
Vector3 pos = ((originPos + VRPos) + eyePos);

var eyeQuaternionMatrix = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X,
                                                                                   eyePoses[eyeIndex].Orientation.Y,
                                                                                  eyePoses[eyeIndex].Orientation.Z,
                                                                                  eyePoses[eyeIndex].Orientation.W));
Matrix rot = eyeQuaternionMatrix;

Vector3 finalUp = Vector3.Transform(new Vector3(0, 1, 0), rot).ToVector3();
Vector3 finalForward = Vector3.Transform(new Vector3(0, 0, 1), rot).ToVector3();


Matrix viewMatrix = SharpDX.Matrix.LookAtLH(pos, (pos + finalForward), finalUp);
Matrix projectionMatrix = OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.1f, 100.0f, ProjectionModifier.None).ToMatrix();
projectionMatrix.Transpose();*/

