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

using System;
using SharpDX.Toolkit;

namespace SharpDX.Toolkit.Graphics
{
    /// <summary>
    /// A constant buffer exposed by an effect.
    /// </summary>
    /// <remarks>
    /// Constant buffers are created and shared inside a same <see cref="EffectPool"/>. The creation of the underlying GPU buffer
    /// can be overridden using <see cref="EffectPool.ConstantBufferAllocator"/>.
    /// </remarks>
    public sealed class EffectConstantBuffer : Component, IEquatable<EffectConstantBuffer>
    {
        private GraphicsDevice device;
        private readonly Buffer nativeBuffer;
        internal EffectData.ConstantBuffer Description;
        private readonly int hashCode;
        
        public readonly DataBuffer BackingBuffer;

        internal EffectConstantBuffer(GraphicsDevice device, EffectData.ConstantBuffer description)
        {
            BackingBuffer = new DataBuffer(description.Size);
            this.device = device;
            Description = description;
            Name = description.Name;
            Parameters = new EffectParameterCollection(description.Parameters.Count);
            hashCode = description.GetHashCode();

            // Add all parameters to this constant buffer.
            for (int i = 0; i < description.Parameters.Count; i++)
            {
                var parameterRaw = description.Parameters[i];
                var parameter = new EffectParameter(parameterRaw, this) {Index = i};
                Parameters.Add(parameter);
            }

            // By default, all constant buffers are cleared with 0
            BackingBuffer.Clear();

            nativeBuffer = ToDispose(Buffer.Constant.New(device, BackingBuffer.Size));

            // The buffer is considered dirty for the first usage.
            IsDirty = true;
        }

        /// <summary>
        /// Set this flag to true to notify that the buffer was changed
        /// </summary>
        /// <remarks>
        /// When using Set(value) methods on this buffer, this property must be set to true to ensure that the buffer will
        /// be uploaded.
        /// </remarks>
        public bool IsDirty;

        /// <summary>
        /// Gets the parameters registered for this constant buffer.
        /// </summary>
        public readonly EffectParameterCollection Parameters;

        /// <summary>
        /// Updates the specified constant buffer from all parameters value.
        /// </summary>
        public void Update()
        {
            Update(device);
        }

        /// <summary>
        /// Copies the CPU content of this buffer to another constant buffer. 
        /// Destination buffer will be flagged as dirty.
        /// </summary>
        /// <param name="toBuffer">To buffer to receive the content.</param>
        public void CopyTo(EffectConstantBuffer toBuffer)
        {
            if (toBuffer == null)
                throw new ArgumentNullException("toBuffer");

            if (BackingBuffer.Size != toBuffer.BackingBuffer.Size)
            {
                throw new ArgumentOutOfRangeException("toBuffer",
                                                      "Size of the source and destination buffer are not the same");
            }

            Utilities.CopyMemory(toBuffer.BackingBuffer.DataPointer, BackingBuffer.DataPointer, BackingBuffer.Size);
            toBuffer.IsDirty = true;
        }

        /// <summary>
        /// Updates the specified constant buffer from all parameters value.
        /// </summary>
        /// <param name="device">The device.</param>
        public void Update(GraphicsDevice device)
        {
            if (IsDirty)
            {
                nativeBuffer.SetData(device, new DataPointer(BackingBuffer.DataPointer, BackingBuffer.Size));
                IsDirty = false;
            }
        }

        public bool Equals(EffectConstantBuffer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            // Fast comparison using hashCode.
            return hashCode == other.hashCode && Description.Equals(other.Description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EffectConstantBuffer) obj);
        }

        public override int GetHashCode()
        {
            // Return precalculated hashcode
            return hashCode;
        }

        public static bool operator ==(EffectConstantBuffer left, EffectConstantBuffer right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EffectConstantBuffer left, EffectConstantBuffer right)
        {
            return !Equals(left, right);
        }

        public static implicit operator SharpDX.Direct3D11.Buffer(EffectConstantBuffer from)
        {
            return from.nativeBuffer;
        }

        public static implicit operator Buffer(EffectConstantBuffer from)
        {
            return from.nativeBuffer;
        }
    }
}