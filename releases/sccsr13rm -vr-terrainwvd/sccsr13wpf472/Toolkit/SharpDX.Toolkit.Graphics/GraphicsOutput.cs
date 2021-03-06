// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
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

using System;
using System.Collections.Generic;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics;
using ResultCode = SharpDX.DXGI.ResultCode;

namespace SharpDX.Toolkit.Graphics
{
    /// <summary>
    /// Provides methods to retrieve and manipulate an graphics output (a monitor), it is equivalent to <see cref="Output"/>.
    /// </summary>
    /// <msdn-id>bb174546</msdn-id>	
    /// <unmanaged>IDXGIOutput</unmanaged>	
    /// <unmanaged-short>IDXGIOutput</unmanaged-short>	
    public class GraphicsOutput : Component
    {
        private readonly GraphicsAdapter adapter;
        private readonly Output output;
        private readonly OutputDescription outputDescription;
        private DisplayMode currentDisplayMode;
        private DisplayMode[] supportedDisplayModes;

        /// <summary>
        /// Initializes a new instance of <see cref="GraphicsOutput" />.
        /// </summary>
        /// <param name="adapter">The adapter.</param>
        /// <param name="output">The DXGI <see cref="Output" /> counterpart of this instance.</param>
        /// <exception cref="System.ArgumentNullException">output</exception>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when <paramref name="outputOrdinal" /> is less than zero.</exception>
        /// <exception cref="ArgumentNullException">Is thrown when <paramref name="output" /> is null.</exception>
        internal GraphicsOutput(GraphicsAdapter adapter, Output output)
        {
            if(adapter == null) throw new ArgumentNullException("adapter");
            if (output == null) throw new ArgumentNullException("output");

            this.adapter = adapter;
            this.output = ToDispose(output);
            outputDescription = output.Description;
        }

        /// <summary>
        /// Find the display mode that most closely matches the requested display mode.
        /// </summary>
        /// <param name="targetProfiles">The target profile, as available formats are different depending on the feature level..</param>
        /// <param name="mode">The mode.</param>
        /// <returns>Returns the closes display mode.</returns>
        /// <unmanaged>HRESULT IDXGIOutput::FindClosestMatchingMode([In] const DXGI_MODE_DESC* pModeToMatch,[Out] DXGI_MODE_DESC* pClosestMatch,[In, Optional] IUnknown* pConcernedDevice)</unmanaged>
        /// <remarks>Direct3D devices require UNORM formats. This method finds the closest matching available display mode to the mode specified in pModeToMatch. Similarly ranked fields (i.e. all specified, or all unspecified, etc) are resolved in the following order.  ScanlineOrdering Scaling Format Resolution RefreshRate  When determining the closest value for a particular field, previously matched fields are used to filter the display mode list choices, and  other fields are ignored. For example, when matching Resolution, the display mode list will have already been filtered by a certain ScanlineOrdering,  Scaling, and Format, while RefreshRate is ignored. This ordering doesn't define the absolute ordering for every usage scenario of FindClosestMatchingMode, because  the application can choose some values initially, effectively changing the order that fields are chosen. Fields of the display mode are matched one at a time, generally in a specified order. If a field is unspecified, FindClosestMatchingMode gravitates toward the values for the desktop related to this output.  If this output is not part of the desktop, then the default desktop output is used to find values. If an application uses a fully unspecified  display mode, FindClosestMatchingMode will typically return a display mode that matches the desktop settings for this output.   Unspecified fields are lower priority than specified fields and will be resolved later than specified fields.</remarks>
        public DisplayMode FindClosestMatchingDisplayMode(FeatureLevel[] targetProfiles,  DisplayMode mode)
        {
            ModeDescription closestDescription;
            SharpDX.Direct3D11.Device deviceTemp = null;
            try
            {
                deviceTemp = new SharpDX.Direct3D11.Device(adapter, DeviceCreationFlags.None, targetProfiles);
            }
            catch(Exception ex) {}

            var descriprtion = new ModeDescription()
                                   {
                                       Width = mode.Width,
                                       Height = mode.Height,
                                       RefreshRate = mode.RefreshRate,
                                       Format = mode.Format,
                                       Scaling = DisplayModeScaling.Unspecified,
                                       ScanlineOrdering = DisplayModeScanlineOrder.Unspecified
                                   };
            using (var device = deviceTemp)
                output.GetClosestMatchingMode(device, descriprtion, out closestDescription);

            return DisplayMode.FromDescription(closestDescription);
        }

        /// <summary>
        /// Gets the adapter this output is attached.
        /// </summary>
        /// <value>The adapter.</value>
        public GraphicsAdapter Adapter
        {
            get { return adapter; }
        }

        /// <summary>
        /// Gets the current display mode.
        /// </summary>
        /// <value>The current display mode.</value>
        public DisplayMode CurrentDisplayMode
        {
            get
            {
                lock(output)
                {
                    if(currentDisplayMode == null)
                    {
                        InitializeCurrentDisplayMode();
                    }
                }
                return currentDisplayMode;
            }
        }

        /// <summary>
        /// Returns a collection of supported display modes for this <see cref="GraphicsOutput"/>.
        /// </summary>
        public DisplayMode[] SupportedDisplayModes
        {
            get
            {
                lock(output)
                {
                    if(supportedDisplayModes == null)
                    {
                        InitializeSupportedDisplayModes();
                    }
                }
                return supportedDisplayModes;
            }
        }

        /// <summary>
        /// Retrieves the handle of the monitor associated with this <see cref="GraphicsOutput"/>.
        /// </summary>
        /// <msdn-id>bb173068</msdn-id>	
        /// <unmanaged>HMONITOR Monitor</unmanaged>	
        /// <unmanaged-short>HMONITOR Monitor</unmanaged-short>	
        public IntPtr MonitorHandle { get { return outputDescription.MonitorHandle; } }

        /// <summary>
        /// Gets the desktop bounds of the current output.
        /// </summary>
        public Rectangle DesktopBounds { get { return outputDescription.DesktopBounds; } }

        /// <summary>
        /// <see cref="Output"/> casting operator.
        /// </summary>
        /// <param name="from">Source of the.</param>
        /// <returns>The DXGI <see cref="Output"/> counterpart of the <paramref name="from"/> instance.</returns>
        public static implicit operator Output(GraphicsOutput from)
        {
            return from.output;
        }

        /// <summary>
        /// Enumerates all available display modes for this output and stores them in <see cref="SupportedDisplayModes"/>.
        /// </summary>
        private void InitializeSupportedDisplayModes()
        {
            var modesAvailable = new List<DisplayMode>();
            var modesMap = new Dictionary<string, DisplayMode>();

#if DIRECTX11_1
            var output1 = output.QueryInterface<Output1>();
#endif

            try
            {
                const DisplayModeEnumerationFlags displayModeEnumerationFlags = DisplayModeEnumerationFlags.Interlaced | DisplayModeEnumerationFlags.Scaling;

                foreach (var format in Enum.GetValues(typeof(DXGI.Format)))
                {
                    var dxgiFormat = (Format)format;
#if DIRECTX11_1
                    var modes = output1.GetDisplayModeList1(dxgiFormat, displayModeEnumerationFlags);
#else
                    var modes = output.GetDisplayModeList(dxgiFormat, displayModeEnumerationFlags);
#endif

                    foreach (var mode in modes)
                    {
                        if (mode.Scaling == DisplayModeScaling.Unspecified)
                        {
                            var key = format + ";" + mode.Width + ";" + mode.Height + ";" + mode.RefreshRate.Numerator + ";" + mode.RefreshRate.Denominator;

                            DisplayMode oldMode;
                            if (!modesMap.TryGetValue(key, out oldMode))
                            {
                                var displayMode = DisplayMode.FromDescription(mode);

                                modesMap.Add(key, displayMode);
                                modesAvailable.Add(displayMode);
                            }
                        }
                    }
                }
            }
            catch (SharpDXException dxgiException)
            {
                if (dxgiException.ResultCode != ResultCode.NotCurrentlyAvailable)
                    throw;
            }

#if DIRECTX11_1
            output1.Dispose();
#endif
            supportedDisplayModes = modesAvailable.ToArray();
        }

        /// <summary>
        /// Initializes <see cref="CurrentDisplayMode"/> with the most appropiate mode from <see cref="SupportedDisplayModes"/>.
        /// </summary>
        /// <remarks>It checks first for a mode with <see cref="Format.R8G8B8A8_UNorm"/>,
        /// if it is not found - it checks for <see cref="Format.B8G8R8A8_UNorm"/>.</remarks>
        private void InitializeCurrentDisplayMode()
        {
            currentDisplayMode = TryFindMatchingDisplayMode(Format.R8G8B8A8_UNorm)
                                 ?? TryFindMatchingDisplayMode(Format.B8G8R8A8_UNorm);
        }

        /// <summary>
        /// Tries to find a display mode that has the same size as the current <see cref="OutputDescription"/> associated with this instance
        /// of the specified format.
        /// </summary>
        /// <param name="format">The format to match with.</param>
        /// <returns>A matched <see cref="DisplayMode"/> or null if nothing is found.</returns>
        private DisplayMode TryFindMatchingDisplayMode(Format format)
        {
            Rectangle desktopBounds = outputDescription.DesktopBounds;

            foreach (var supportedDisplayMode in SupportedDisplayModes)
            {
                if (supportedDisplayMode.Width == desktopBounds.Width
                    && supportedDisplayMode.Height == desktopBounds.Height
                    && supportedDisplayMode.Format == format)
                {
                    // Stupid DXGI, there is no way to get the DXGI.Format, nor the refresh rate.
                    return new DisplayMode(format, desktopBounds.Width, desktopBounds.Height, supportedDisplayMode.RefreshRate);
                }
            }

            return null;
        }
    }
}