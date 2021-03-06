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

using System.IO;
using System.Text;

namespace SharpDX.Toolkit.Graphics
{
    /// <summary>
    /// Use this class to generate a code with embedded effect bytecode.
    /// </summary>
    public class EffectDataCodeWriter
    {
        /// <summary>
        /// Gets or sets the class declaration (Default: "public partial").
        /// </summary>
        public string ClassDeclaration = "public partial";

        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        public string Namespace;

        /// <summary>
        /// Gets or sets the class name.
        /// </summary>
        public string ClassName;

        /// <summary>
        /// Gets or sets the field declaration (default: "private").
        /// </summary>
        public string FieldDeclaration = "private";

        /// <summary>
        /// Gets or sets the field name (default: "effectByteCode").
        /// </summary>
        public string FieldName = "effectByteCode";

        public void Write(EffectData effectData, TextWriter writer)
        {
            const string codeTemplate = @"//------------------------------------------------------------------------------
// <auto-generated>
//     SharpDX Toolkit Compiler File Generated:
{0}//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace {1} 
{{
    {2} class {3}
    {{
        {4} static readonly SharpDX.Toolkit.Graphics.EffectData {5} = SharpDX.Toolkit.Graphics.EffectData.Load(new byte[] {{
{6}
        }});
    }}
}}
";

        var effectToGenerateText = new StringBuilder();
            effectToGenerateText.AppendFormat("//     Effect [{0}]\r\n", effectData.Description.Name);

            var buffer = new MemoryStream();
            effectData.Save(buffer);

            var bufferAsText = new StringBuilder();
            var bufferArray = buffer.ToArray();
            for(int i = 0; i < bufferArray.Length; i++)
            {
                bufferAsText.Append(bufferArray[i]).Append(", ");
                if (i > 0 && (i % 64) == 0)
                {
                    bufferAsText.AppendLine();
                }
            }

            writer.Write(codeTemplate,
                         effectToGenerateText, // {0} 
                         Namespace,            // {1} 
                         ClassDeclaration,     // {2} 
                         ClassName,            // {3} 
                         FieldDeclaration,     // {4} 
                         FieldName,            // {5} 
                         bufferAsText);        // {6} 

            writer.Flush();
        }
    }
}