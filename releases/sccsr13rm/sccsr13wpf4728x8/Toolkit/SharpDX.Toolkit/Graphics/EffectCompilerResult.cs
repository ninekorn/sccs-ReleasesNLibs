﻿// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
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

using SharpDX.Toolkit.Diagnostics;

namespace SharpDX.Toolkit.Graphics
{
    /// <summary>
    /// Result of a compilation.
    /// </summary>
    public sealed class EffectCompilerResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EffectCompilerResult" /> class.
        /// </summary>
        /// <param name="dependencyFilePath">The path to dependency file (may be null).</param>
        /// <param name="effectData">The EffectData.</param>
        /// <param name="logger">The logger.</param>
        public EffectCompilerResult(string dependencyFilePath, EffectData effectData, Logger logger)
        {
            DependencyFilePath = dependencyFilePath;
            EffectData = effectData;
            Logger = logger;
        }

        /// <summary>
        /// The effect dependency list (a list of files and includes that this effect is timestamp dependent).
        /// </summary>
        public string DependencyFilePath;

        /// <summary>
        /// Gets the EffectData.
        /// </summary>
        /// <value>The EffectData.</value>
        public EffectData EffectData { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance has errors.
        /// </summary>
        /// <value><c>true</c> if this instance has errors; otherwise, <c>false</c>.</value>
        public bool HasErrors
        {
            get { return Logger.HasErrors; }
        }

        /// <summary>
        /// Gets the logger containing compilation messages..
        /// </summary>
        /// <value>The logger.</value>
        public Logger Logger { get; private set; }
    }
}