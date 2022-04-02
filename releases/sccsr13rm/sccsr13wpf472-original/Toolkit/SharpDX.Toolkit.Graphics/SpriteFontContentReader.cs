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

using System.IO;
using SharpDX.Toolkit.Content;

namespace SharpDX.Toolkit.Graphics
{
    /// <summary>
    /// Internal class to load a SpriteFont.
    /// </summary>
    internal class SpriteFontContentReader : GraphicsResourceContentReaderBase<SpriteFont>
    {
        protected override SpriteFont ReadContent(IContentManager readerManager, GraphicsDevice device, ref ContentReaderParameters parameters)
        {
            SpriteFont spriteFont = null;
            var assetPath = Path.GetDirectoryName(parameters.AssetName);

            // Load the sprite font data
            var spriteFontData = SpriteFontData.Load(parameters.Stream, name => readerManager.Load<Texture2D>(Path.Combine(assetPath ?? string.Empty, name)));

            // If sprite font was fine, then instantiate SpriteFont graphics object.
            if (spriteFontData != null)
                spriteFont = SpriteFont.New(device, spriteFontData);

            return spriteFont;
        }
    }
}