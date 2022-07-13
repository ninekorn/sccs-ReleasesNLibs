////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.Direct2D1;
using SharpDX.DXGI;

using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Windows;
using Buffer = SharpDX.Direct3D11.Buffer;
using Color = SharpDX.Color;
using Device = SharpDX.Direct3D11.Device;
using MapFlags = SharpDX.Direct3D11.MapFlags;

using InputElement = SharpDX.Direct3D11.InputElement;
using System.Runtime.InteropServices;

namespace sccsr15forms
{
    public class tutorialcubeaschunk : IDisposable
    {
        public sclevelgen sclevelgen;
        public LevelGenerator4 levelgen;
        public sclevelgenglobals somelevelgenprimglobals;

        //public SharpDX.Direct3D11.Buffer IndicesBuffer;
        public Buffer staticContantBuffer;
        public Buffer dynamicConstantBuffer;
        public InputLayout layout;
        public PixelShader pixelShader;
        public VertexShader vertexShader;
        //public Buffer verticesbuffer;
        //public Vector4[] vertices;


        /*//[StructLayout(LayoutKind.Explicit, Size = 80)]
        [StructLayout(LayoutKind.Explicit)]
        public struct DVertex
        {
            [FieldOffset(0)]
            public Vector4 position;
            [FieldOffset(16)]
            public Vector4 indexPos;
            [FieldOffset(32)]
            public Vector4 color;
            [FieldOffset(48)]
            public Vector3 normal;
            [FieldOffset(60)]
            public float padding0;
            [FieldOffset(64)]
            public Vector2 tex;
            [FieldOffset(72)]
            public float padding1;
            [FieldOffset(76)]
            public float padding2;
        }*/

        [StructLayout(LayoutKind.Explicit)]
        public struct DVertex
        {
            [FieldOffset(0)]
            public Vector4 position;
            [FieldOffset(16)]
            public Vector4 color;
        }


        //sclevelgenmaps[] arraychunkmapslod0;
        //sclevelgenvert[] arraychunkvertslod0;


        public chunkdata[] arraychunkdatalod0;
        public chunkdata[] arraychunkdatalod1;
        public chunkdata[] arraychunkdatalod2;
        public chunkdata[] arraychunkdatalod3;
        public chunkdata[] arraychunkdatalod4;


        public tutorialcubeaschunk(Device device)
        {      
            // Compile Vertex and Pixel shaders 
            var bytecode = ShaderBytecode.CompileFromFile("MultiCube.fx", "VS", "vs_4_0");
            vertexShader = new VertexShader(device, bytecode);

            // Layout from VertexShader input signature 
            layout = new InputLayout(device, ShaderSignature.GetInputSignature(bytecode), new[]
                   {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0)
                    });
            bytecode.Dispose();

            bytecode = ShaderBytecode.CompileFromFile("MultiCube.fx", "PS", "ps_4_0");
            pixelShader = new PixelShader(device, bytecode);
            bytecode.Dispose();


            //VOXEL VIRTUAL DESKTOP
            //VOXEL VIRTUAL DESKTOP
            //VOXEL VIRTUAL DESKTOP
            somelevelgenprimglobals = new sclevelgenglobals();

            somelevelgenprimglobals.planeSize = 0.01f; // * 10

            /*
            somelevelgenprimglobals.widthlod0 = 7;
            somelevelgenprimglobals.heightlod0 = 7;
            somelevelgenprimglobals.depthlod0 = 7;

            somelevelgenprimglobals.widthlod1 = 3;
            somelevelgenprimglobals.heightlod1 = 3;
            somelevelgenprimglobals.depthlod1 = 3;

            somelevelgenprimglobals.widthlod2 = 2;
            somelevelgenprimglobals.heightlod2 = 2;
            somelevelgenprimglobals.depthlod2 = 2;

            somelevelgenprimglobals.widthlod3 = 1;
            somelevelgenprimglobals.heightlod3 = 1;
            somelevelgenprimglobals.depthlod3 = 1;*/

            /*
            somelevelgenprimglobals.widthlod0 = 6;
            somelevelgenprimglobals.heightlod0 = 6;
            somelevelgenprimglobals.depthlod0 = 6;

            somelevelgenprimglobals.widthlod1 = 3;
            somelevelgenprimglobals.heightlod1 = 3;
            somelevelgenprimglobals.depthlod1 = 3;

            somelevelgenprimglobals.widthlod2 = 2;
            somelevelgenprimglobals.heightlod2 = 2;
            somelevelgenprimglobals.depthlod2 = 2;

            somelevelgenprimglobals.widthlod3 = 1;
            somelevelgenprimglobals.heightlod3 = 1;
            somelevelgenprimglobals.depthlod3 = 1;*/

            /*
            somelevelgenprimglobals.widthlod0 = 20;
            somelevelgenprimglobals.heightlod0 = 20;
            somelevelgenprimglobals.depthlod0 = 20;

            somelevelgenprimglobals.widthlod1 = 10;
            somelevelgenprimglobals.heightlod1 = 10;
            somelevelgenprimglobals.depthlod1 = 10;

            somelevelgenprimglobals.widthlod2 = 6;
            somelevelgenprimglobals.heightlod2 = 6;
            somelevelgenprimglobals.depthlod2 = 6;

            somelevelgenprimglobals.widthlod3 = 5;
            somelevelgenprimglobals.heightlod3 = 5;
            somelevelgenprimglobals.depthlod3 = 5;*/

            somelevelgenprimglobals.widthlod0 = 10;
            somelevelgenprimglobals.heightlod0 = 10;
            somelevelgenprimglobals.depthlod0 = 10;

            somelevelgenprimglobals.widthlod1 = 5;
            somelevelgenprimglobals.heightlod1 = 5;
            somelevelgenprimglobals.depthlod1 = 5;

            somelevelgenprimglobals.widthlod2 = 3;
            somelevelgenprimglobals.heightlod2 = 3;
            somelevelgenprimglobals.depthlod2 = 3;

            somelevelgenprimglobals.widthlod3 = 2;
            somelevelgenprimglobals.heightlod3 = 2;
            somelevelgenprimglobals.depthlod3 = 2;

            somelevelgenprimglobals.widthlod4 = 1;
            somelevelgenprimglobals.heightlod4 = 1;
            somelevelgenprimglobals.depthlod4 = 1;



            /*levelgen = new LevelGenerator4();
            //adding more tiles means the level will be bigger
            levelgen.tileAmount = 250; //10500 //15000lag
            //15000lag => my computer specs 960gtx 2gb + Amd Ryzen 2600 + 8gb ram 
            //levelgen.chunkwidth = 1;
            //levelgen.chunkheight = 1;
            //levelgen.chunkdepth = 1;
            levelgen.planesize = 1.0f;
            levelgen.chanceUp = 0.25f;
            levelgen.chanceRight = 0.5f;
            levelgen.chanceDown = 0.75f;
            levelgen.SpawnerMoveWaitTime = 0.00000000001f;
            levelgen.BuildingWaitTime = 0.00000000001f;
            //levelgen.blockSize = 1.0f;
            levelgen.StartGeneratingVoxelLevel();*/


            //
            sclevelgen = new sclevelgen();
            sclevelgen.StartGeneratingVoxelLevel();

            int totaltiles = 0;

            for (int i = 0; i < sclevelgen.somelevelgentypeoftiles.tilestypeof.Count; i++)
            {
                //var enumerator00 = sclevelgen.somelevelgentypeoftiles.tilestypeof[i].GetEnumerator();
                //int ichunkleveltile00 = 0;


                totaltiles += sclevelgen.somelevelgentypeoftiles.tilestypeof[i].Count;

                /*
                while (enumerator00.MoveNext())
                {
                    totaltiles++;
                    ichunkleveltile00++;
                }*/
            }



            Console.WriteLine(totaltiles);

            sclevelgen.levelmap = new int[sclevelgen.somewidth * sclevelgen.someheight * sclevelgen.somedepth];

            Console.WriteLine(sclevelgen.levelmap.Length);

            //arraychunkmapslod0 = new sclevelgenmaps[levelgen.typeoftiles.Count];
            //arraychunkvertslod0 = new sclevelgenvert[levelgen.typeoftiles.Count];
            arraychunkdatalod0 = new chunkdata[totaltiles];
            arraychunkdatalod1 = new chunkdata[totaltiles];
            arraychunkdatalod2 = new chunkdata[totaltiles];
            arraychunkdatalod3 = new chunkdata[totaltiles];
            arraychunkdatalod4 = new chunkdata[totaltiles];

            int[] somemap;
            Vector4[] arrayofverts;
            int[] arrayoftrigs;

            int facetype = 0;
            //List<Vector4> listofverts = new List<Vector4>();
            //List<int> listoftrigs = new List<int>();

            int ichunkleveltile = 0;

            for (int i = 0; i < sclevelgen.tilestypeof.Count; i++)
            {
                if (sclevelgen.tilestypeof[i].Count > 0)
                {
                    var enumerator00 = sclevelgen.tilestypeof[i].GetEnumerator();
                    //Console.WriteLine(ichunkleveltile);
           
                    while (enumerator00.MoveNext())
                    {
                        //levelgen.list
                        //var indexinlevelgenmap = levelgen.listofvecstileindex[ichunkleveltile];

                        var currentTuile = enumerator00.Current;

                        var chunkPos = currentTuile.Key;
                        var typeofterraintile = currentTuile.Value;
                        
                        Vector3 newchunkpos = chunkPos;
                        
                        newchunkpos.X = newchunkpos.X * (somelevelgenprimglobals.widthlod0 * somelevelgenprimglobals.planeSize);
                        newchunkpos.Y = newchunkpos.Y * (somelevelgenprimglobals.heightlod0 * somelevelgenprimglobals.planeSize);
                        newchunkpos.Z = newchunkpos.Z * (somelevelgenprimglobals.depthlod0 * somelevelgenprimglobals.planeSize);

                        //Console.WriteLine(newchunkpos);

                        int xx = (int)Math.Round(chunkPos.X);
                        int yy = (int)Math.Round(chunkPos.Y);
                        int zz = (int)Math.Round(chunkPos.Z);
                        //int xx = 0; //
                        //int yy = 0; //
                        //int zz = 0;
                        
                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = xx + (sclevelgen.maxx - 1);
                        }

                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = yy + (sclevelgen.maxy - 1);
                        }
                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = zz + (sclevelgen.maxz - 1);
                        }
                        //Console.WriteLine(ichunkleveltile);
                        int indexofmaparray = xx + sclevelgen.somewidth * (yy + sclevelgen.someheight * zz); //y is always 0 on floor tiles

                        //Console.WriteLine(indexofmaparray);
                        var indexoftile = sclevelgen.levelmap[indexofmaparray];

                        //Console.WriteLine("count:" + sclevelgen.levelmap.Length + " " + indexofmaparray + " " + ichunkleveltile);
                        sclevelgen.levelmap[indexofmaparray] = ichunkleveltile;

                        //Console.WriteLine("count:" + sclevelgen.levelmap.Length + " " + indexofmaparray + " " + ichunkleveltile);




                        /*if (indexintile == ichunkleveltile)
                        {
                            Console.WriteLine("same index0");
                        }*/
                        //sclevelgen.levelmap[indexofmaparray] = ichunkleveltile;

                        /*if (indexoftile != -1 && indexoftile != ichunkleveltile)
                        {
                            Console.WriteLine("index ISSUE " + indexoftile + " " + ichunkleveltile);
                        }*/
                        
                        arraychunkdatalod0[ichunkleveltile] = new chunkdata();
                        //arraychunkdatalod0[ichunkleveltile].arraychunkmapslod0 = new sclevelgenmaps();
                        //somemap = arraychunkdatalod0[ichunkleveltile].arraychunkmapslod0.buildchunkmaps(newchunkpos, somelevelgenprimglobals.planeSize, chunkPos, typeofterraintile, this, 1);
                        arraychunkdatalod0[ichunkleveltile].arraychunkvertslod0 = new sclevelgenvert();
                        arraychunkdatalod0[ichunkleveltile].arraychunkvertslod0.buildchunkmaps(newchunkpos, chunkPos, typeofterraintile, this, 1);
                        arraychunkdatalod0[ichunkleveltile].realpos = newchunkpos;
                        arraychunkdatalod0[ichunkleveltile].chunkPos = chunkPos;
                        arraychunkdatalod0[ichunkleveltile].indexinlevelgenmap = indexoftile;
                        arraychunkdatalod0[ichunkleveltile].indexintypeoftiles = ichunkleveltile;
                        arraychunkdatalod0[ichunkleveltile].X = (int)Math.Round(chunkPos.X);
                        arraychunkdatalod0[ichunkleveltile].Y = (int)Math.Round(chunkPos.Y);
                        arraychunkdatalod0[ichunkleveltile].Z = (int)Math.Round(chunkPos.Z);
                        arraychunkdatalod0[ichunkleveltile].arraychunkvertslod0.chunkPos = chunkPos;
                        arraychunkdatalod0[ichunkleveltile].arraychunkvertslod0.newchunkpos = newchunkpos;
                        //arraychunkdatalod0[ichunkleveltile].arraychunkvertslod0.map = somemap;
                        arraychunkdatalod0[ichunkleveltile].typeofterraintile = typeofterraintile;
                        arraychunkdatalod0[ichunkleveltile].arraychunkvertslod0.X = (int)Math.Round(chunkPos.X);
                        arraychunkdatalod0[ichunkleveltile].arraychunkvertslod0.Y = (int)Math.Round(chunkPos.Y);
                        arraychunkdatalod0[ichunkleveltile].arraychunkvertslod0.Z = (int)Math.Round(chunkPos.Z);
                        
                        


                        
                        //lod2
                        arraychunkdatalod1[ichunkleveltile] = new chunkdata();
                        //arraychunkdatalod1[ichunkleveltile].arraychunkmapslod1 = new sclevelgenmaps();
                        //somemap = arraychunkdatalod1[ichunkleveltile].arraychunkmapslod1.buildchunkmaps(newchunkpos, chunkPos, typeofterraintile, this, 1);
                        arraychunkdatalod1[ichunkleveltile].arraychunkvertslod1 = new sclevelgenvert();
                        arraychunkdatalod1[ichunkleveltile].arraychunkvertslod1.buildchunkmaps(newchunkpos, chunkPos, typeofterraintile, this, 2);
                        arraychunkdatalod1[ichunkleveltile].realpos = newchunkpos;
                        arraychunkdatalod1[ichunkleveltile].chunkPos = chunkPos;
                        arraychunkdatalod1[ichunkleveltile].indexinlevelgenmap = indexoftile;
                        arraychunkdatalod1[ichunkleveltile].indexintypeoftiles = ichunkleveltile;
                        arraychunkdatalod1[ichunkleveltile].X = (int)Math.Round(chunkPos.X);
                        arraychunkdatalod1[ichunkleveltile].Y = (int)Math.Round(chunkPos.Y);
                        arraychunkdatalod1[ichunkleveltile].Z = (int)Math.Round(chunkPos.Z);
                        arraychunkdatalod1[ichunkleveltile].arraychunkvertslod1.chunkPos = chunkPos;
                        arraychunkdatalod1[ichunkleveltile].arraychunkvertslod1.newchunkpos = newchunkpos;
                        //arraychunkdatalod1[ichunkleveltile].arraychunkvertslod1.map = somemap;
                        arraychunkdatalod1[ichunkleveltile].typeofterraintile = typeofterraintile;
                        arraychunkdatalod1[ichunkleveltile].arraychunkvertslod1.X = (int)Math.Round(chunkPos.X);
                        arraychunkdatalod1[ichunkleveltile].arraychunkvertslod1.Y = (int)Math.Round(chunkPos.Y);
                        arraychunkdatalod1[ichunkleveltile].arraychunkvertslod1.Z = (int)Math.Round(chunkPos.Z);


                        //lod3
                        arraychunkdatalod2[ichunkleveltile] = new chunkdata();
                        //arraychunkdatalod2[ichunkleveltile].arraychunkmapslod2 = new sclevelgenmaps();
                        //somemap = arraychunkdatalod2[ichunkleveltile].arraychunkmapslod2.buildchunkmaps(newchunkpos, chunkPos, typeofterraintile, this, 1);
                        arraychunkdatalod2[ichunkleveltile].arraychunkvertslod2 = new sclevelgenvert();
                        arraychunkdatalod2[ichunkleveltile].arraychunkvertslod2.buildchunkmaps(newchunkpos, chunkPos, typeofterraintile, this, 3);
                        arraychunkdatalod2[ichunkleveltile].realpos = newchunkpos;
                        arraychunkdatalod2[ichunkleveltile].chunkPos = chunkPos;
                        arraychunkdatalod2[ichunkleveltile].indexinlevelgenmap = indexoftile;
                        arraychunkdatalod2[ichunkleveltile].indexintypeoftiles = ichunkleveltile;
                        arraychunkdatalod2[ichunkleveltile].X = (int)Math.Round(chunkPos.X);
                        arraychunkdatalod2[ichunkleveltile].Y = (int)Math.Round(chunkPos.Y);
                        arraychunkdatalod2[ichunkleveltile].Z = (int)Math.Round(chunkPos.Z);
                        arraychunkdatalod2[ichunkleveltile].arraychunkvertslod2.chunkPos = chunkPos;
                        arraychunkdatalod2[ichunkleveltile].arraychunkvertslod2.newchunkpos = newchunkpos;
                        //arraychunkdatalod2[ichunkleveltile].arraychunkvertslod2.map = somemap;
                        arraychunkdatalod2[ichunkleveltile].typeofterraintile = typeofterraintile;
                        arraychunkdatalod2[ichunkleveltile].arraychunkvertslod2.X = (int)Math.Round(chunkPos.X);
                        arraychunkdatalod2[ichunkleveltile].arraychunkvertslod2.Y = (int)Math.Round(chunkPos.Y);
                        arraychunkdatalod2[ichunkleveltile].arraychunkvertslod2.Z = (int)Math.Round(chunkPos.Z);


                        //lod4
                        arraychunkdatalod3[ichunkleveltile] = new chunkdata();
                        //arraychunkdatalod3[ichunkleveltile].arraychunkmapslod3 = new sclevelgenmaps();
                        //somemap = arraychunkdatalod3[ichunkleveltile].arraychunkmapslod3.buildchunkmaps(newchunkpos, chunkPos, typeofterraintile, this, 1);
                        arraychunkdatalod3[ichunkleveltile].arraychunkvertslod3 = new sclevelgenvert();
                        arraychunkdatalod3[ichunkleveltile].arraychunkvertslod3.buildchunkmaps(newchunkpos, chunkPos, typeofterraintile, this, 4);
                        arraychunkdatalod3[ichunkleveltile].realpos = newchunkpos;
                        arraychunkdatalod3[ichunkleveltile].chunkPos = chunkPos;
                        arraychunkdatalod3[ichunkleveltile].indexinlevelgenmap = indexoftile;
                        arraychunkdatalod3[ichunkleveltile].indexintypeoftiles = ichunkleveltile;
                        arraychunkdatalod3[ichunkleveltile].X = (int)Math.Round(chunkPos.X);
                        arraychunkdatalod3[ichunkleveltile].Y = (int)Math.Round(chunkPos.Y);
                        arraychunkdatalod3[ichunkleveltile].Z = (int)Math.Round(chunkPos.Z);
                        arraychunkdatalod3[ichunkleveltile].arraychunkvertslod3.chunkPos = chunkPos;
                        arraychunkdatalod3[ichunkleveltile].arraychunkvertslod3.newchunkpos = newchunkpos;
                        //arraychunkdatalod3[ichunkleveltile].arraychunkvertslod3.map = somemap;
                        arraychunkdatalod3[ichunkleveltile].typeofterraintile = typeofterraintile;
                        arraychunkdatalod3[ichunkleveltile].arraychunkvertslod3.X = (int)Math.Round(chunkPos.X);
                        arraychunkdatalod3[ichunkleveltile].arraychunkvertslod3.Y = (int)Math.Round(chunkPos.Y);
                        arraychunkdatalod3[ichunkleveltile].arraychunkvertslod3.Z = (int)Math.Round(chunkPos.Z);


                        //lod5
                        arraychunkdatalod4[ichunkleveltile] = new chunkdata();
                        //arraychunkdatalod4[ichunkleveltile].arraychunkmapslod4 = new sclevelgenmaps();
                        //somemap = arraychunkdatalod4[ichunkleveltile].arraychunkmapslod4.buildchunkmaps(newchunkpos, chunkPos, typeofterraintile, this, 1);
                        arraychunkdatalod4[ichunkleveltile].arraychunkvertslod4 = new sclevelgenvert();
                        arraychunkdatalod4[ichunkleveltile].arraychunkvertslod4.buildchunkmaps(newchunkpos, chunkPos, typeofterraintile, this, 5);
                        arraychunkdatalod4[ichunkleveltile].realpos = newchunkpos;
                        arraychunkdatalod4[ichunkleveltile].chunkPos = chunkPos;
                        arraychunkdatalod4[ichunkleveltile].indexinlevelgenmap = indexoftile;
                        arraychunkdatalod4[ichunkleveltile].indexintypeoftiles = ichunkleveltile;
                        arraychunkdatalod4[ichunkleveltile].X = (int)Math.Round(chunkPos.X);
                        arraychunkdatalod4[ichunkleveltile].Y = (int)Math.Round(chunkPos.Y);
                        arraychunkdatalod4[ichunkleveltile].Z = (int)Math.Round(chunkPos.Z);
                        arraychunkdatalod4[ichunkleveltile].arraychunkvertslod4.chunkPos = chunkPos;
                        arraychunkdatalod4[ichunkleveltile].arraychunkvertslod4.newchunkpos = newchunkpos;
                        //arraychunkdatalod4[ichunkleveltile].arraychunkvertslod4.map = somemap;
                        arraychunkdatalod4[ichunkleveltile].typeofterraintile = typeofterraintile;
                        arraychunkdatalod4[ichunkleveltile].arraychunkvertslod4.X = (int)Math.Round(chunkPos.X);
                        arraychunkdatalod4[ichunkleveltile].arraychunkvertslod4.Y = (int)Math.Round(chunkPos.Y);
                        arraychunkdatalod4[ichunkleveltile].arraychunkvertslod4.Z = (int)Math.Round(chunkPos.Z);
                        

                        ichunkleveltile++;
                    }
                }
            }














            
            staticContantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
            dynamicConstantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

            //enumerator1 = sclevelgen.typeoftiles.GetEnumerator();
            ichunkleveltile = 0;

            for (int i = 0;i < arraychunkdatalod0.Length;i++)
            {
                //var currentTuile = enumerator1.Current;
                //var chunkPos = currentTuile.Key;
                //var typeofterraintile = currentTuile.Value;
                //Vector3 newchunkpos = chunkPos;
                //newchunkpos.X = newchunkpos.X * (somelevelgenprimglobals.widthlod0 * somelevelgenprimglobals.planeSize);
                //newchunkpos.Y = newchunkpos.Y * (somelevelgenprimglobals.heightlod0 * somelevelgenprimglobals.planeSize);
                //newchunkpos.Z = newchunkpos.Z * (somelevelgenprimglobals.depthlod0 * somelevelgenprimglobals.planeSize);

                facetype = 0;

                //lod1
                arraychunkdatalod0[ichunkleveltile].arraychunkvertslod0.startBuildingArray(facetype, this, 1);
                //Console.WriteLine(arraychunkdatalod0[ichunkleveltile].arraychunkvertslod0.chunkPos);
                if (arraychunkdatalod0[ichunkleveltile].arraychunkvertslod0.arrayofverts.Length > 0)
                {
                    // Create Constant Buffer 
                    //staticContantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
                    //dynamicConstantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

                    arraychunkdatalod0[ichunkleveltile].verticesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, arraychunkdatalod0[ichunkleveltile].arraychunkvertslod0.arrayofverts);
                    arraychunkdatalod0[ichunkleveltile].indicesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, arraychunkdatalod0[ichunkleveltile].arraychunkvertslod0.arrayoftrigs);
                    arraychunkdatalod0[ichunkleveltile].staticContantBuffer = staticContantBuffer;
                    arraychunkdatalod0[ichunkleveltile].dynamicConstantBuffer = dynamicConstantBuffer;

                    arraychunkdatalod0[ichunkleveltile].vertexShader = vertexShader;
                    arraychunkdatalod0[ichunkleveltile].pixelShader = pixelShader;
                    arraychunkdatalod0[ichunkleveltile].layout = layout;
                }

                
                //lod2
                arraychunkdatalod1[ichunkleveltile].arraychunkvertslod1.startBuildingArray(facetype, this, 2);

                //Console.WriteLine(arraychunkdatalod1[ichunkleveltile].arraychunkvertslod1.chunkPos);
                if (arraychunkdatalod1[ichunkleveltile].arraychunkvertslod1.arrayofverts.Length > 0)
                {
                    // Create Constant Buffer 
                    //staticContantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
                    //dynamicConstantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

                    arraychunkdatalod1[ichunkleveltile].verticesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, arraychunkdatalod1[ichunkleveltile].arraychunkvertslod1.arrayofverts);
                    arraychunkdatalod1[ichunkleveltile].indicesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, arraychunkdatalod1[ichunkleveltile].arraychunkvertslod1.arrayoftrigs);
                    arraychunkdatalod1[ichunkleveltile].staticContantBuffer = staticContantBuffer;
                    arraychunkdatalod1[ichunkleveltile].dynamicConstantBuffer = dynamicConstantBuffer;

                    arraychunkdatalod1[ichunkleveltile].vertexShader = vertexShader;
                    arraychunkdatalod1[ichunkleveltile].pixelShader = pixelShader;
                    arraychunkdatalod1[ichunkleveltile].layout = layout;
                }


                //lod3
                arraychunkdatalod2[ichunkleveltile].arraychunkvertslod2.startBuildingArray(facetype, this, 3);

                //Console.WriteLine(arraychunkdatalod2[ichunkleveltile].arraychunkvertslod2.chunkPos);
                if (arraychunkdatalod2[ichunkleveltile].arraychunkvertslod2.arrayofverts.Length > 0)
                {
                    // Create Constant Buffer 
                    //staticContantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
                    //dynamicConstantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

                    arraychunkdatalod2[ichunkleveltile].verticesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, arraychunkdatalod2[ichunkleveltile].arraychunkvertslod2.arrayofverts);
                    arraychunkdatalod2[ichunkleveltile].indicesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, arraychunkdatalod2[ichunkleveltile].arraychunkvertslod2.arrayoftrigs);
                    arraychunkdatalod2[ichunkleveltile].staticContantBuffer = staticContantBuffer;
                    arraychunkdatalod2[ichunkleveltile].dynamicConstantBuffer = dynamicConstantBuffer;

                    arraychunkdatalod2[ichunkleveltile].vertexShader = vertexShader;
                    arraychunkdatalod2[ichunkleveltile].pixelShader = pixelShader;
                    arraychunkdatalod2[ichunkleveltile].layout = layout;
                }



                //lod4
                arraychunkdatalod3[ichunkleveltile].arraychunkvertslod3.startBuildingArray(facetype, this, 4);

                //Console.WriteLine(arraychunkdatalod3[ichunkleveltile].arraychunkvertslod3.chunkPos);
                if (arraychunkdatalod3[ichunkleveltile].arraychunkvertslod3.arrayofverts.Length > 0)
                {
                    // Create Constant Buffer 
                    //staticContantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
                    //dynamicConstantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

                    arraychunkdatalod3[ichunkleveltile].verticesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, arraychunkdatalod3[ichunkleveltile].arraychunkvertslod3.arrayofverts);
                    arraychunkdatalod3[ichunkleveltile].indicesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, arraychunkdatalod3[ichunkleveltile].arraychunkvertslod3.arrayoftrigs);
                    arraychunkdatalod3[ichunkleveltile].staticContantBuffer = staticContantBuffer;
                    arraychunkdatalod3[ichunkleveltile].dynamicConstantBuffer = dynamicConstantBuffer;

                    arraychunkdatalod3[ichunkleveltile].vertexShader = vertexShader;
                    arraychunkdatalod3[ichunkleveltile].pixelShader = pixelShader;
                    arraychunkdatalod3[ichunkleveltile].layout = layout;
                }



                //lod5
                arraychunkdatalod4[ichunkleveltile].arraychunkvertslod4.startBuildingArray(facetype, this, 5);

                //Console.WriteLine(arraychunkdatalod4[ichunkleveltile].arraychunkvertslod4.chunkPos);
                if (arraychunkdatalod4[ichunkleveltile].arraychunkvertslod4.arrayofverts.Length > 0)
                {
                    // Create Constant Buffer 
                    //staticContantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
                    //dynamicConstantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

                    arraychunkdatalod4[ichunkleveltile].verticesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, arraychunkdatalod4[ichunkleveltile].arraychunkvertslod4.arrayofverts);
                    arraychunkdatalod4[ichunkleveltile].indicesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, arraychunkdatalod4[ichunkleveltile].arraychunkvertslod4.arrayoftrigs);
                    arraychunkdatalod4[ichunkleveltile].staticContantBuffer = staticContantBuffer;
                    arraychunkdatalod4[ichunkleveltile].dynamicConstantBuffer = dynamicConstantBuffer;

                    arraychunkdatalod4[ichunkleveltile].vertexShader = vertexShader;
                    arraychunkdatalod4[ichunkleveltile].pixelShader = pixelShader;
                    arraychunkdatalod4[ichunkleveltile].layout = layout;
                }




                ichunkleveltile++;
            }
            
            



            /*
            //unLOADING CHUNK to XML
            //unLOADING CHUNK to XML
            string pathofrelease = Directory.GetCurrentDirectory();
            //Console.WriteLine(pathofrelease);
            string pathofchunkmap = pathofrelease + @"\chunkmaps\";

            if (!Directory.Exists(pathofchunkmap))
            {
                //Console.WriteLine("created directory");
                Directory.CreateDirectory(pathofchunkmap);
            }

            //int writetofilecounter = 0;

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            var path = pathofchunkmap + @"\levelgenbytemap" + ".xml";

            var writer = new XmlTextWriter(path, System.Text.Encoding.UTF8);

            writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;

            writer.WriteStartElement("root"); // open 0

            for (int i = 0; i < arrayofchunkslod0.Length; i++)
            {                             
                //writer.WriteStartElement("bytemap"); //open 4
                writer.WriteStartElement("x" + arrayofchunkslod0[i].chunkPos.X + "y" + arrayofchunkslod0[i].chunkPos.Y + "z" + arrayofchunkslod0[i].chunkPos.Z); //open 4

                int[] somemapp = arrayofchunksmapslod0[i].map;//
                writer.WriteValue(somemapp);
                //writer.WriteEndElement(); //close 4    
                writer.WriteEndElement(); //close 4                    
            }
            //unLOADING CHUNK to XML
            //unLOADING CHUNK to XML
            writer.WriteEndElement(); //close 2
            writer.Close();*/






            //LOADING CHUNK BACK INTO MEMORY
            //LOADING CHUNK BACK INTO MEMORY
            /*
            //writetofilecounter = 0;
            for (int i = 0; i < arrayofchunks.Length; i++)
            {
                //https://stackoverflow.com/questions/18891207/how-to-get-value-from-a-specific-child-element-in-xml-using-xmlreader
                //var path = @"C:\Users\steve\Desktop\#chunkmaps\" + "chunkmap" + writetofilecounter + ".xml";
                var reader = new XmlTextReader(path);

                if (reader.ReadToDescendant("x" + arrayofchunks[i].chunkPos.X + "y" + arrayofchunks[i].chunkPos.X + "z" + arrayofchunks[i].chunkPos.Z))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    int[] ia = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
            }*/
            //LOADING CHUNK BACK INTO MEMORY
            //LOADING CHUNK BACK INTO MEMORY





            /*
            float x0 = (float)(Math.Round(10.1f / 10.0f) * 10.0f);
            float y0 = (float)(Math.Round(10.1f / 10.0f) * 10.0f);
            float z0 = (float)(Math.Round(10.1f / 10.0f) * 10.0f);


            Console.WriteLine(x0);*/





        }



        public struct chunkdata
        {
            public SharpDX.Direct3D11.Buffer indicesbuffer;
            public SharpDX.Direct3D11.Buffer verticesbuffer;
            public SharpDX.Direct3D11.Buffer staticContantBuffer;
            public SharpDX.Direct3D11.Buffer dynamicConstantBuffer;
            //public sclevelgenmaps arraychunkmapslod0;
            public sclevelgenvert arraychunkvertslod0;

            //public sclevelgenmaps arraychunkmapslod1;
            public sclevelgenvert arraychunkvertslod1;

            //public sclevelgenmaps arraychunkmapslod2;
            public sclevelgenvert arraychunkvertslod2;

            //public sclevelgenmaps arraychunkmapslod3;
            public sclevelgenvert arraychunkvertslod3;

            //public sclevelgenmaps arraychunkmapslod4;
            public sclevelgenvert arraychunkvertslod4;
            //public Vector4[] arrayofverts;
            //public int[] arrayoftrigs;
            public PixelShader pixelShader;
            public VertexShader vertexShader;
            public InputLayout layout;

            public float distanceculling;
            public bool frustrumculldraw;
            public Vector3 realpos;
            public Vector3 chunkPos;
            public int indexinlevelgenmap;
            public int indexintypeoftiles;
            public int typeofterraintile;

            public int X;
            public int Y;
            public int Z;
        }







        
        public sclevelgenvert getchunkinlevelgenmap(int x, int y, int z,int levelofdetail) //, Vector3 chunkPos
        {
            int orix = x;
            int oriy = y;
            int oriz = z;

            if (x < 0)
            {
                x *= -1;
                x = x + (sclevelgen.maxx - 1);
            }

            if (y < 0)
            {
                y *= -1;
                y = y + (sclevelgen.maxy - 1);
            }
            if (z < 0)
            {
                z *= -1;
                z = z + (sclevelgen.maxz - 1);
            }
            //Console.WriteLine(y);
            /*
            //int indexinarray = xx * somewidth + (zz);
            int indexinsclevelgenmap = x + sclevelgen.somewidth * (y + sclevelgen.someheight * z);

            if (indexinsclevelgenmap < sclevelgen.somewidth * sclevelgen.someheight * sclevelgen.somedepth)
            {
                int indexinvectorarray = sclevelgen.levelmap[indexinsclevelgenmap]; //levelgen.levelindexmap[indexinsclevelgenmap]

                if (indexinvectorarray != -1)
                {
                    if (arraychunkdatalod0[indexinvectorarray].arraychunkvertslod0 != null)
                    {
                        //if (indexinsclevelgenmap == arraychunkdatalod0[indexinvectorarray].indexinlevelgenmap && indexinvectorarray == arraychunkdatalod0[indexinvectorarray].indexintypeoftiles)
                        //if (indexinvectorarray == arraychunkdatalod0[indexinvectorarray].indexintypeoftiles)
                        {
                            if (arraychunkdatalod0[indexinvectorarray].X == orix &&
                                arraychunkdatalod0[indexinvectorarray].Y == oriy &&
                                arraychunkdatalod0[indexinvectorarray].Z == oriz)
                            

                            //if ((int)Math.Round(arraychunkdatalod0[indexinvectorarray].chunkPos.X) == (int)Math.Round(sclevelgen.listoftiles[indexinvectorarray].X) &&
                            //    (int)Math.Round(arraychunkdatalod0[indexinvectorarray].chunkPos.Y) == (int)Math.Round(sclevelgen.listoftiles[indexinvectorarray].Y) &&
                            //    (int)Math.Round(arraychunkdatalod0[indexinvectorarray].chunkPos.Z) == (int)Math.Round(sclevelgen.listoftiles[indexinvectorarray].Z))
                            {

                                //Console.WriteLine("same index");
                                if (levelofdetail == 1)
                                {
                                    return arraychunkdatalod0[indexinvectorarray].arraychunkvertslod0;
                                }
                                else if (levelofdetail == 2)
                                {
                                    return arraychunkdatalod1[indexinvectorarray].arraychunkvertslod1;
                                }
                                else if (levelofdetail == 3)
                                {
                                    return arraychunkdatalod2[indexinvectorarray].arraychunkvertslod2;
                                }
                                else if (levelofdetail == 4)
                                {
                                    return arraychunkdatalod3[indexinvectorarray].arraychunkvertslod3;
                                }
                                else if (levelofdetail == 5)
                                {
                                    return arraychunkdatalod4[indexinvectorarray].arraychunkvertslod4;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                return null;
                Console.WriteLine("Out of Range. The chunk is a border tile");
            }*/




            /*
            float x0 = (float)(Math.Round(x * 10.0f) / 10.0f);
            float y0 = (float)(Math.Round(y * 10.0f) / 10.0f);
            float z0 = (float)(Math.Round(z * 10.0f) / 10.0f);
            */
            /*//var enumerator0 = sclevelgenchunk.arraychunkdatalod0.GetEnumerator();
            for (int i = 0; i < levelgen.Length; i++)
            {

                float x1 = (float)(Math.Round(LevelGenerator4[i].chunkPos.X * 10.0f) / 10.0f);
                float y1 = (float)(Math.Round(arraychunkdatalod0[i].chunkPos.Y * 10.0f) / 10.0f);
                float z1 = (float)(Math.Round(arraychunkdatalod0[i].chunkPos.Z * 10.0f) / 10.0f);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    arrayindex = i;
                    return arraychunkdatalod0[i].arraychunkvertslod0;
                }
            }

            //arrayindex = -1;
            //Console.WriteLine();*/
            return null;
        }



        public sclevelgenvert getchunkinlevelgenmaplevelgen4(int x, int y, int z, int levelofdetail) //, Vector3 chunkPos
        {
            int orix = x;
            int oriy = y;
            int oriz = z;

            if (x < 0)
            {
                x *= -1;
                x = x + (levelgen.maxx - 1);
            }

            if (y < 0)
            {
                y *= -1;
                y = y + (levelgen.maxy - 1);
            }
            if (z < 0)
            {
                z *= -1;
                z = z + (levelgen.maxz - 1);
            }
            //Console.WriteLine(y);

            //int indexinarray = xx * somewidth + (zz);
            int indexinsclevelgenmap = x + levelgen.somewidth * (y + levelgen.someheight * z);

            if (indexinsclevelgenmap < levelgen.somewidth * levelgen.someheight * levelgen.somedepth)
            {
                int indexinvectorarray = levelgen.levelmap[indexinsclevelgenmap]; //levelgen.levelindexmap[indexinsclevelgenmap]

                if (indexinvectorarray != -1)
                {
                    if (arraychunkdatalod0[indexinvectorarray].arraychunkvertslod0 != null)
                    {
                        //if (indexinsclevelgenmap == arraychunkdatalod0[indexinvectorarray].indexinlevelgenmap && indexinvectorarray == arraychunkdatalod0[indexinvectorarray].indexintypeoftiles)
                        //if (indexinvectorarray == arraychunkdatalod0[indexinvectorarray].indexintypeoftiles)
                        {
                            //if (arraychunkdatalod0[indexinvectorarray].X == orix &&
                            //    arraychunkdatalod0[indexinvectorarray].Y == oriy &&
                            //    arraychunkdatalod0[indexinvectorarray].Z == oriz)


                            //if ((int)Math.Round(arraychunkdatalod0[indexinvectorarray].chunkPos.X) == (int)Math.Round(sclevelgen.listoftiles[indexinvectorarray].X) &&
                            //    (int)Math.Round(arraychunkdatalod0[indexinvectorarray].chunkPos.Y) == (int)Math.Round(sclevelgen.listoftiles[indexinvectorarray].Y) &&
                            //    (int)Math.Round(arraychunkdatalod0[indexinvectorarray].chunkPos.Z) == (int)Math.Round(sclevelgen.listoftiles[indexinvectorarray].Z))
                            {

                                //Console.WriteLine("same index");
                                if (levelofdetail == 1)
                                {
                                    return arraychunkdatalod0[indexinvectorarray].arraychunkvertslod0;
                                }
                                else if (levelofdetail == 2)
                                {
                                    return arraychunkdatalod1[indexinvectorarray].arraychunkvertslod1;
                                }
                                else if (levelofdetail == 3)
                                {
                                    return arraychunkdatalod2[indexinvectorarray].arraychunkvertslod2;
                                }
                                else if (levelofdetail == 4)
                                {
                                    return arraychunkdatalod3[indexinvectorarray].arraychunkvertslod3;
                                }
                                else if (levelofdetail == 5)
                                {
                                    return arraychunkdatalod4[indexinvectorarray].arraychunkvertslod4;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                return null;
                Console.WriteLine("Out of Range. The chunk is a border tile");
            }




            /*
            float x0 = (float)(Math.Round(x * 10.0f) / 10.0f);
            float y0 = (float)(Math.Round(y * 10.0f) / 10.0f);
            float z0 = (float)(Math.Round(z * 10.0f) / 10.0f);
            */
            /*//var enumerator0 = sclevelgenchunk.arraychunkdatalod0.GetEnumerator();
            for (int i = 0; i < levelgen.Length; i++)
            {

                float x1 = (float)(Math.Round(LevelGenerator4[i].chunkPos.X * 10.0f) / 10.0f);
                float y1 = (float)(Math.Round(arraychunkdatalod0[i].chunkPos.Y * 10.0f) / 10.0f);
                float z1 = (float)(Math.Round(arraychunkdatalod0[i].chunkPos.Z * 10.0f) / 10.0f);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    arrayindex = i;
                    return arraychunkdatalod0[i].arraychunkvertslod0;
                }
            }

            //arrayindex = -1;
            //Console.WriteLine();*/
            return null;
        }


        /*
        public sclevelgenvert getChunklod0(float x, float y, float z, out int arrayindex) //, Vector3 chunkPos
        {

            float x0 = (float)(Math.Round(x / 10.0f) * 10.0f);
            float y0 = (float)(Math.Round(y / 10.0f) * 10.0f);
            float z0 = (float)(Math.Round(z / 10.0f) * 10.0f);

            //var enumerator0 = sclevelgenchunk.arraychunkdatalod0.GetEnumerator();
            for (int i = 0; i < arraychunkdatalod0.Length; i++)
            {

                float x1 = (float)(Math.Round(arraychunkdatalod0[i].chunkPos.X / 10.0f) * 10.0f);
                float y1 = (float)(Math.Round(arraychunkdatalod0[i].chunkPos.Y / 10.0f) * 10.0f);
                float z1 = (float)(Math.Round(arraychunkdatalod0[i].chunkPos.Z / 10.0f) * 10.0f);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    arrayindex = i;
                    return arraychunkdatalod0[i].arraychunkvertslod0;
                }

            }

            arrayindex = -1;
            return null;

        }*/





        public sclevelgenvert getChunklod0(int x, int y, int z, out int arrayindex) //, Vector3 chunkPos
        {

            /*int x0 = ((x));
            int y0 = ((y));
            int z0 = ((z));*/

            for (int i = 0; i < arraychunkdatalod0.Length; i++)
            {

                int x1 = ((arraychunkdatalod0[i].X));
                int y1 = ((arraychunkdatalod0[i].Y));
                int z1 = ((arraychunkdatalod0[i].Z));


                if (x == x1 && y == y1 && z == z1)
                {
                    arrayindex = i;
                    return arraychunkdatalod0[i].arraychunkvertslod0;
                }

            }

            arrayindex = -1;
            return null;

        }
        
        public sclevelgenvert getChunklod1(float x, float y, float z, out int arrayindex) //, Vector3 chunkPos
        {

            float x0 = (float)(Math.Round(x * 10.0f) / 10.0f);
            float y0 = (float)(Math.Round(y * 10.0f) / 10.0f);
            float z0 = (float)(Math.Round(z * 10.0f) / 10.0f);

            //var enumerator0 = sclevelgenchunk.arraychunkdatalod0.GetEnumerator();
            for (int i = 0; i < arraychunkdatalod1.Length; i++)
            {

                float x1 = (float)(Math.Round(arraychunkdatalod1[i].chunkPos.X * 10.0f) / 10.0f);
                float y1 = (float)(Math.Round(arraychunkdatalod1[i].chunkPos.Y * 10.0f) / 10.0f);
                float z1 = (float)(Math.Round(arraychunkdatalod1[i].chunkPos.Z * 10.0f) / 10.0f);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    arrayindex = i;
                    return arraychunkdatalod1[i].arraychunkvertslod1;
                }

            }

            arrayindex = -1;
            return null;

        }


        public sclevelgenvert getChunklod2(float x, float y, float z, out int arrayindex) //, Vector3 chunkPos
        {
            float x0 = (float)(Math.Round(x * 10.0f) / 10.0f);
            float y0 = (float)(Math.Round(y * 10.0f) / 10.0f);
            float z0 = (float)(Math.Round(z * 10.0f) / 10.0f);

            //var enumerator0 = sclevelgenchunk.arraychunkdatalod0.GetEnumerator();
            for (int i = 0; i < arraychunkdatalod2.Length; i++)
            {

                float x1 = (float)(Math.Round(arraychunkdatalod2[i].chunkPos.X * 10.0f) / 10.0f);
                float y1 = (float)(Math.Round(arraychunkdatalod2[i].chunkPos.Y * 10.0f) / 10.0f);
                float z1 = (float)(Math.Round(arraychunkdatalod2[i].chunkPos.Z * 10.0f) / 10.0f);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    arrayindex = i;
                    return arraychunkdatalod2[i].arraychunkvertslod2;
                }
            }

            arrayindex = -1;
            return null;

        }


        public sclevelgenvert getChunklod3(float x, float y, float z, out int arrayindex) //, Vector3 chunkPos
        {

            float x0 = (float)(Math.Round(x * 10.0f) / 10.0f);
            float y0 = (float)(Math.Round(y * 10.0f) / 10.0f);
            float z0 = (float)(Math.Round(z * 10.0f) / 10.0f);

            //var enumerator0 = sclevelgenchunk.arraychunkdatalod0.GetEnumerator();
            for (int i = 0; i < arraychunkdatalod3.Length; i++)
            {

                float x1 = (float)(Math.Round(arraychunkdatalod3[i].chunkPos.X * 10.0f) / 10.0f);
                float y1 = (float)(Math.Round(arraychunkdatalod3[i].chunkPos.Y * 10.0f) / 10.0f);
                float z1 = (float)(Math.Round(arraychunkdatalod3[i].chunkPos.Z * 10.0f) / 10.0f);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    arrayindex = i;
                    return arraychunkdatalod3[i].arraychunkvertslod3;
                }
            }

            arrayindex = -1;
            return null;
        }






        public sclevelgenvert getChunklod4(float x, float y, float z, out int arrayindex) //, Vector3 chunkPos
        {
            float x0 = (float)(Math.Round(x * 10.0f) / 10.0f);
            float y0 = (float)(Math.Round(y * 10.0f) / 10.0f);
            float z0 = (float)(Math.Round(z * 10.0f) / 10.0f);

            //var enumerator0 = sclevelgenchunk.arraychunkdatalod0.GetEnumerator();
            for (int i = 0; i < arraychunkdatalod4.Length; i++)
            {

                float x1 = (float)(Math.Round(arraychunkdatalod4[i].chunkPos.X * 10.0f) / 10.0f);
                float y1 = (float)(Math.Round(arraychunkdatalod4[i].chunkPos.Y * 10.0f) / 10.0f);
                float z1 = (float)(Math.Round(arraychunkdatalod4[i].chunkPos.Z * 10.0f) / 10.0f);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    arrayindex = i;
                    return arraychunkdatalod4[i].arraychunkvertslod4;
                }
            }

            arrayindex = -1;
            return null;
        }













        ~tutorialcubeaschunk()
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
                if (staticContantBuffer != null)
                {
                    staticContantBuffer.Dispose();
                    staticContantBuffer = null;
                }
                if (dynamicConstantBuffer != null)
                {
                    dynamicConstantBuffer.Dispose();
                    dynamicConstantBuffer = null;
                }

                if (layout != null)
                {
                    layout.Dispose();
                    layout = null;
                }
                if (pixelShader != null)
                {
                    pixelShader.Dispose();
                    pixelShader = null;
                }

                if (vertexShader != null)
                {
                    vertexShader.Dispose();
                    vertexShader = null;
                }

                /*if (verticesbuffer != null)
                {
                    verticesbuffer.Dispose();
                    verticesbuffer = null;
                }

                if (arrayofverts != null)
                {
                    arrayofverts = null;
                }

                if (arrayoftrigs != null)
                {
                    arrayoftrigs = null;
                }*/





                // Dispose all owned managed objects
            }

            // Release unmanaged resources
        }
    }
}
