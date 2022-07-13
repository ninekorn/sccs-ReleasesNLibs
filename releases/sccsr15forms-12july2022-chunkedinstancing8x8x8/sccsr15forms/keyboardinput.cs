//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// using this script as reference from rastertek github user Dan6040 c# sample.
//DEVELOPED BY STEVE CHASSÉ using xoofx's sharpdx original deferred rendering sample. This is a software of mixed architecture// 
//using rastertek c# github user dan6040's sample architecture and smartrak's sample architecture and xoofx sharpdx samples/////
//architecture./////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Copyright (c) 2010-2013 SharpDX - Alexandre Mutel 
//  
// Permission is hereby granted, free of charge, to any person obtaining a copy 
// of this software and associated documentation files (the "Software"), to deal 
// in the Software without restriction, including without limitation the rights 
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
// copies of the Software, and to permit persons to whom the Software is 
// furnished to do so, subject to the following conditions: 
//  
// The above copyright notice and this permission notice shall be included in 
// all copies or substantial portions of the Software. 
//  
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
// THE SOFTWARE. 


//The MIT License (MIT)
//
//Copyright(c) 2016 Smartrak

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.


//https://github.com/Dan6040/SharpDX-Rastertek-Tutorials
//https://github.com/Smartrak/WpfSharpDxControl
//https://github.com/sharpdx/SharpDX-Samples

using System;
using SharpDX.DirectInput;

namespace sccsr15forms
{
    public class keyboardinput: IDisposable
    {
        public SharpDX.DirectInput.Keyboard _Keyboard;
        DirectInput directInput = new DirectInput();
        public KeyboardState _KeyboardState;
        int _InitializeKeyboardAuth = 0;

        public keyboardinput()
        {
            _InitializeKeyboard();
            _KeyboardState = new KeyboardState();
        }

        public int _InitializeKeyboard()
        {
            _InitializeKeyboardAuth = 1;
            try
            {
                directInput = new DirectInput();
                _Keyboard = new SharpDX.DirectInput.Keyboard(directInput);
                _Keyboard.Properties.BufferSize = 128;
                _Keyboard.Acquire();
            }
            catch
            {

                _InitializeKeyboardAuth = 0;
            }
            return _InitializeKeyboardAuth;
        }


        public bool ReadKeyboard()
        {
            var resultCode = SharpDX.DirectInput.ResultCode.Ok;

            try
            {
                _Keyboard.GetCurrentState(ref _KeyboardState);
            }
            catch (SharpDX.SharpDXException ex)
            {
                resultCode = ex.Descriptor;
            }
            catch (Exception ex)
            {
                Program.MessageBox((IntPtr)0, "cannot get keyboard info 00: " + ex.ToString() + "", "Oculus error", 0);
                return false;
            }

            if (resultCode == SharpDX.DirectInput.ResultCode.InputLost || resultCode == SharpDX.DirectInput.ResultCode.NotAcquired)
            {
                try
                {
                    _Keyboard.Acquire();
                }
                catch (Exception ex)
                {
                    Program.MessageBox((IntPtr)0, "cannot get keyboard info 01: " + ex.ToString() + "", "Oculus error", 0);
                }

                return true;
            }

            if (resultCode == SharpDX.DirectInput.ResultCode.Ok)
            {
                return true;
            }

            return false;
        }


        ~keyboardinput()
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
                if (_KeyboardState != null)
                {
                    _KeyboardState = null;
                }

                if (directInput != null)
                {
                    directInput.Dispose();
                    directInput = null;
                }

                if (_Keyboard != null)
                {
                    _Keyboard.Dispose();
                    _Keyboard = null;
                }


                // Dispose all owned managed objects
            }

            // Release unmanaged resources
        }
    }
}
