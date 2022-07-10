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
        Vector3 chunkdivpos = Vector3.Zero;

        public SharpDX.Direct3D11.Buffer ConstantLightBuffer;

        public sccslevelgen sccslevelgen;

        //public sclevelgen sclevelgen;
        //public LevelGenerator4 levelgen;
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
            [FieldOffset(32)]
            public Vector4 normal;
        }


        [StructLayout(LayoutKind.Explicit)]
        public struct DLightBuffer
        {
            [FieldOffset(0)]
            public Vector4 ambientColor;
            [FieldOffset(16)]
            public Vector4 diffuseColor;
            [FieldOffset(32)]
            public Vector3 lightDirection;
            [FieldOffset(44)]
            public float padding0; // Added extra padding so structure is a multiple of 16.
            [FieldOffset(48)]
            public Vector3 lightPosition;
            [FieldOffset(60)]
            public float padding1; // Added extra padding so structure is a multiple of 16.
        }



        public DLightBuffer[] lightBuffer = new DLightBuffer[1];

        //sclevelgenmaps[] arraychunkmapslod0;
        //sclevelgenvert[] arraychunkvertslod0;


        public chunkdata[] arraychunkdatalod0;
        public chunkdata[] arraychunkdatalod1;
        public chunkdata[] arraychunkdatalod2;
        public chunkdata[] arraychunkdatalod3;
        public chunkdata[] arraychunkdatalod4;

        int[] arrayofindexes;





        public tutorialcubeaschunk(Device device)
        {

            indextest someindextest = new indextest();


            // Compile Vertex and Pixel shaders 
            var bytecode = ShaderBytecode.CompileFromFile("MultiCube.fx", "VS", "vs_5_0");
            vertexShader = new VertexShader(device, bytecode);

            // Layout from VertexShader input signature 
            /*layout = new InputLayout(device, ShaderSignature.GetInputSignature(bytecode), new[]
            {
                new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0),
                new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 32, 0)
            });
            bytecode.Dispose();*/


            InputElement[] inputElements = new InputElement[]
                {
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "COLOR", // 16
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "NORMAL",// 16
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset =InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                };

            layout = new InputLayout(device, ShaderSignature.GetInputSignature(bytecode), inputElements);

            bytecode.Dispose();








            bytecode = ShaderBytecode.CompileFromFile("MultiCube.fx", "PS", "ps_4_0");
            pixelShader = new PixelShader(device, bytecode);
            bytecode.Dispose();




            Vector4 ambientColor = new Vector4(0.4f, 0.4f, 0.4f, 1.0f);
            Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
            Vector4 lightDirection = new Vector4(1, 0, 0, 1.0f);
            Vector3 lightpos0 = new Vector3(0, 1, 0);
            Vector3 dirLight0 = new Vector3(0, -1, 0);




            lightBuffer[0] = new DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight0,
                padding0 = 100,
                lightPosition = lightpos0,
                padding1 = 0,
                //padding1 = 100
            };


            BufferDescription lightBufferDesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Utilities.SizeOf<DLightBuffer>(),
                BindFlags = BindFlags.ConstantBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };


            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);












            //VOXEL VIRTUAL DESKTOP
            //VOXEL VIRTUAL DESKTOP
            //VOXEL VIRTUAL DESKTOP
            somelevelgenprimglobals = new sclevelgenglobals();

            somelevelgenprimglobals.planeSize = 0.005f; // * 10

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


            /*somelevelgenprimglobals.widthlod0 = 20;
            somelevelgenprimglobals.heightlod0 = 20;
            somelevelgenprimglobals.depthlod0 = 20;

            somelevelgenprimglobals.widthlod1 = 10;
            somelevelgenprimglobals.heightlod1 = 10;
            somelevelgenprimglobals.depthlod1 = 10;

            somelevelgenprimglobals.widthlod2 = 6;
            somelevelgenprimglobals.heightlod2 = 6;
            somelevelgenprimglobals.depthlod2 = 6;

            somelevelgenprimglobals.widthlod3 = 3;
            somelevelgenprimglobals.heightlod3 = 3;
            somelevelgenprimglobals.depthlod3 = 3;

            somelevelgenprimglobals.widthlod4 = 1;
            somelevelgenprimglobals.heightlod4 = 1;
            somelevelgenprimglobals.depthlod4 = 1;*/


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
            //sclevelgen = new sclevelgen();
            //sclevelgen.StartGeneratingVoxelLevel();

            sccslevelgen = new sccslevelgen();
            sccslevelgen.createlevel();


            int totaltilescounter = 0;

            int sometotalmaplength = sccslevelgen.somewidth * sccslevelgen.someheight * sccslevelgen.somedepth;

            for (var x = sccslevelgen.minx; x < sccslevelgen.maxx; x++)
            {
                for (var y = sccslevelgen.miny; y < sccslevelgen.maxy; y++)
                {
                    for (var z = sccslevelgen.minz; z < sccslevelgen.maxz; z++)
                    {
                        int xx = x;
                        int yy = y;
                        int zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = xx + (sccslevelgen.maxx - 1);
                        }

                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = yy + (sccslevelgen.maxy - 1);
                        }
                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = zz + (sccslevelgen.maxz - 1);
                        }

                        int indexinlevelarray = xx + sccslevelgen.somewidth * (yy + sccslevelgen.someheight * zz); //y is always 0 on floor tiles


                        if (indexinlevelarray < sccslevelgen.somewidth * sccslevelgen.someheight * sccslevelgen.somedepth)
                        {

                            int typeofterraintile = sccslevelgen.levelmap[indexinlevelarray];

                            if (typeofterraintile == 0 ||
                                typeofterraintile == 1101 ||
                                typeofterraintile == 1102 ||
                                typeofterraintile == 1103 ||
                                typeofterraintile == 1104 ||
                                typeofterraintile == 1105 ||
                                typeofterraintile == 1106 ||
                                typeofterraintile == 1107 ||
                                typeofterraintile == 1108 ||
                                typeofterraintile == 1109 ||
                                typeofterraintile == 1110 ||
                                typeofterraintile == 1111 ||
                                typeofterraintile == 1112 ||
                                typeofterraintile == -99 ||
                                typeofterraintile == 1115)
                            {
                                totaltilescounter++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("originallength:" + sometotalmaplength + "/newlength:" + totaltilescounter);


            arrayofindexes = new int[sccslevelgen.levelmap.Length];
            //int[][] arrayofpositions = new int[sccslevelgen.levelmap.Length][];




            int levelminx = sccslevelgen.minx;
            int levelminy = sccslevelgen.miny;
            int levelminz = sccslevelgen.minz;

            int levelmaxx = sccslevelgen.maxx;
            int levelmaxy = sccslevelgen.maxy;
            int levelmaxz = sccslevelgen.maxz;

            int divx = 10;
            int divy = 10;
            int divz = 10;

            int incrementsdivx = sccslevelgen.somewidth / divx;
            int incrementsdivy = sccslevelgen.someheight / divy;
            int incrementsdivz = sccslevelgen.somedepth / divz;

            int halfincrementsdivx = incrementsdivx / 2;
            int halfincrementsdivy = incrementsdivy / 2;
            int halfincrementsdivz = incrementsdivz / 2;

            int incrementsdivxc = 0;
            int incrementsdivyc = 0;
            int incrementsdivzc = 0;

            mainchunkdivlod0 = new chunkdata[divx * divy * divz][];

            for (int i = 0; i < mainchunkdivlod0.Length; i++)
            {
                mainchunkdivlod0[i] = new chunkdata[incrementsdivx * incrementsdivy * incrementsdivz];
            }

            //arraychunkmapslod0 = new sclevelgenmaps[levelgen.typeoftiles.Count];
            //arraychunkvertslod0 = new sclevelgenvert[levelgen.typeoftiles.Count];
            arraychunkdatalod0 = new chunkdata[totaltilescounter];
            arraychunkdatalod1 = new chunkdata[totaltilescounter];
            arraychunkdatalod2 = new chunkdata[totaltilescounter];
            arraychunkdatalod3 = new chunkdata[totaltilescounter];
            arraychunkdatalod4 = new chunkdata[totaltilescounter];


            int[] somemap;
            Vector4[] arrayofverts;
            int[] arrayoftrigs;

            int facetype = 0;
            //List<Vector4> listofverts = new List<Vector4>();
            //List<int> listoftrigs = new List<int>();

            for (int i = 0; i < arrayofindexes.Length; i++)
            {
                arrayofindexes[i] = -1;
            }



            int mainchunkdivindex;
            int somecountermainchunkdiv = 0;


            int mainmaxx = divx;
            int mainmaxy = divy;
            int mainmaxz = divz;

            int mainx = 0;
            int mainy = 0;
            int mainz = 0;

            int maincx = 0;
            int maincy = 0;
            int maincz = 0;


            int swtchx = 0;
            int swtchy = 0;
            int swtchz = 0;

            int somenewcounter = 0;

            List<int> somelistofdata = new List<int>();

            Dictionary<int, int> somelistofint = new Dictionary<int, int>();


            int xi = 0;
            int yi = 0;
            int zi = 0;


            int xis = 0;
            int yis = 0;
            int zis = 0;

            int someindexmain = 0;
            int someindexsec = 0;


            int somecounterofarray = 0;

            for (var x = sccslevelgen.minx; x < sccslevelgen.maxx; x++)
            {
                for (var y = sccslevelgen.miny; y < sccslevelgen.maxy; y++)
                {
                    for (var z = sccslevelgen.minz; z < sccslevelgen.maxz; z++)
                    {

                        /*if (incrementsdivxc == incrementsdivx)
                        {
                            mainx++;
                            if (mainx >= mainmaxx)
                            {
                                mainx = 0;
                            }
                            incrementsdivxc = 0;
                        }

                        if (incrementsdivyc == incrementsdivy)
                        {
                            mainy++;
                            if (mainy >= mainmaxy)
                            {
                                mainy = 0;
                            }
                            incrementsdivyc = 0;
                        }

                        if (incrementsdivzc == incrementsdivz)
                        {
                            mainz++;
                            if (mainz >= mainmaxz)
                            {
                                mainz = 0;
                            }
                            incrementsdivzc = 0;
                        }*/




                        //posx = (xx);
                        //posy = (yy);
                        ///posz = (zz);

                        //var planetchunkpos = new Vector3(posx * realplanetwidth, posy * realplanetwidth, posz * realplanetwidth);
                        //planetchunkpos = new Vector3(posx0, posy0, posz0);



                        /*var someindexmain = xi + (chunkwidthl + chunkwidthr + 1) * (yi + (chunkheightl + chunkheightr + 1) * zi);

                        if (someindexmain < sometotal)
                        {


                        }
                        else
                        {
                            ////t = sometotal;
                            //taskcancelFlagTwo = 1;
                        }*/




                        //t++;




                        /*incrementsdivzc++;
                        //maincz++;

                        incrementsdivyc++;
                        //maincy++;

                        incrementsdivxc++;
                        if (incrementsdivxc >= incrementsdivx)
                        {                       
                            mainx++;
                            if (mainx >= mainmaxx)
                            {
                                mainx = 0;
                            }
                            incrementsdivxc = 0;
                        }

                        if (incrementsdivyc >= incrementsdivy)
                        {
                            mainy++;

                            if (mainy >= mainmaxy)
                            {
                                mainy = 0;
                            }
                            incrementsdivyc = 0;
                        }

                        if (incrementsdivzc >= incrementsdivz)
                        {
                            mainz++;

                            if (mainz >= mainmaxz)
                            {
                                mainz = 0;
                            }

                            incrementsdivzc = 0;
                        }*/


                        int xx = x;
                        int yy = y;
                        int zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = xx + (sccslevelgen.maxx - 1);
                        }

                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = yy + (sccslevelgen.maxy - 1);
                        }
                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = zz + (sccslevelgen.maxz - 1);
                        }

                        int indexinlevelarray = xx + sccslevelgen.somewidth * (yy + sccslevelgen.someheight * zz); //y is always 0 on floor tiles

                        //Vector3 chunkdivpos = new Vector3(x,y,z);

                        /*if (incrementsdivxc >= halfincrementsdivx)
                        {
                            if (incrementsdivyc >= halfincrementsdivy)
                            {
                                if (incrementsdivzc >= halfincrementsdivz)
                                {
                                    chunkdivpos = new Vector3(x, y, z);
                                }
                            }
                        }*/

                        if (indexinlevelarray < sccslevelgen.somewidth * sccslevelgen.someheight * sccslevelgen.somedepth)
                        {
                            int chunkposx = x;
                            int chunkposy = y;
                            int chunkposz = z;

                            int typeofterraintile = sccslevelgen.levelmap[indexinlevelarray];


                            float[] newchunkpos = new float[3];

                            newchunkpos[0] = (float)chunkposx;
                            newchunkpos[1] = (float)chunkposy;
                            newchunkpos[2] = (float)chunkposz;

                            newchunkpos[0] = newchunkpos[0] * (somelevelgenprimglobals.widthlod0 * somelevelgenprimglobals.planeSize);
                            newchunkpos[1] = newchunkpos[1] * (somelevelgenprimglobals.heightlod0 * somelevelgenprimglobals.planeSize);
                            newchunkpos[2] = newchunkpos[2] * (somelevelgenprimglobals.depthlod0 * somelevelgenprimglobals.planeSize);




                            if (typeofterraintile == 0 ||
                                typeofterraintile == 1101 ||
                                typeofterraintile == 1102 ||
                                typeofterraintile == 1103 ||
                                typeofterraintile == 1104 ||
                                typeofterraintile == 1105 ||
                                typeofterraintile == 1106 ||
                                typeofterraintile == 1107 ||
                                typeofterraintile == 1108 ||
                                typeofterraintile == 1109 ||
                                typeofterraintile == 1110 ||
                                typeofterraintile == 1111 ||
                                typeofterraintile == 1112 ||
                                typeofterraintile == -99 ||
                                typeofterraintile == 1115)
                            {







                                int mainindexchunkdiv = mainx + divx * (mainy + divy * mainz);
                                //Console.WriteLine(mainx + " /y: " + mainy + "z/:" + mainz + "/mi:" + mainindexchunkdiv);
                                //Console.WriteLine(incrementsdivxc + " /y: " + incrementsdivyc + "z/:" + incrementsdivzc + "/mi:" + mainindexchunkdiv);
                                //int secindexchunkdiv = incrementsdivxc + incrementsdivx * (incrementsdivyc + incrementsdivy * incrementsdivzc);




                                arrayofindexes[indexinlevelarray] = somenewcounter;

                                //lod1
                                arraychunkdatalod0[somenewcounter] = new chunkdata();
                                //arraychunkdatalod0[somenewcounter].arraychunkmapslod0 = new sclevelgenmaps();
                                //somemap = arraychunkdatalod0[somenewcounter].arraychunkmapslod0.buildchunkmaps(newchunkpos, somelevelgenprimglobals.planeSize, chunkPos, typeofterraintile, this, 1);
                                arraychunkdatalod0[somenewcounter].arraychunkvertslod0 = new sclevelgenvert(chunkposx, chunkposy, chunkposz, newchunkpos);
                                arraychunkdatalod0[somenewcounter].realpos = newchunkpos;
                                arraychunkdatalod0[somenewcounter].chunkPos = new int[] { chunkposx, chunkposy, chunkposz };
                                arraychunkdatalod0[somenewcounter].arraychunkvertslod0.buildchunkmaps(typeofterraintile, this, 1);

                                //arraychunkdatalod0[somenewcounter].indexinlevelgenmap = indexoftile;
                                arraychunkdatalod0[somenewcounter].indexintypeoftiles = indexinlevelarray;
                                arraychunkdatalod0[somenewcounter].X = chunkposx;
                                arraychunkdatalod0[somenewcounter].Y = chunkposy;
                                arraychunkdatalod0[somenewcounter].Z = chunkposz;
                                //arraychunkdatalod0[somenewcounter].arraychunkvertslod0.chunkPos = chunkPos;
                                //arraychunkdatalod0[somenewcounter].arraychunkvertslod0.newchunkpos = newchunkpos;
                                //arraychunkdatalod0[somenewcounter].arraychunkvertslod0.map = somemap;
                                arraychunkdatalod0[somenewcounter].typeofterraintile = typeofterraintile;
                                arraychunkdatalod0[somenewcounter].arraychunkvertslod0.X = chunkposx;
                                arraychunkdatalod0[somenewcounter].arraychunkvertslod0.Y = chunkposy;
                                arraychunkdatalod0[somenewcounter].arraychunkvertslod0.Z = chunkposz;
                                arraychunkdatalod0[somenewcounter].somenewcounter = somenewcounter;

                                //Console.WriteLine(mainx + " /y: " + mainy + "z/:" + mainz + "/mi:" + mainindexchunkdiv);

                                //Console.WriteLine(incrementsdivxc + " /y: " + incrementsdivyc + "z/:" + incrementsdivzc + "/mi:" + secindexchunkdiv);

                                //int indexinlevelarray = xx + sccslevelgen.somewidth * (yy + sccslevelgen.someheight * zz); //y is always 0 on floor tiles
                                 someindexsec = xis + (incrementsdivx) * (yis + (incrementsdivy) * zis);

                                //y is always 0 on floor tiles
                                //[secindexchunkdiv] = arraychunkdatalod0[somenewcounter];
                                mainchunkdivlod0[someindexmain][somecounterofarray].chunkdivpos = new Vector3(newchunkpos[0], newchunkpos[1], newchunkpos[2]);// new Vector3(mainx * 10, mainy * 10, mainz * 10);
                                mainchunkdivlod0[someindexmain][somecounterofarray] = arraychunkdatalod0[somenewcounter];
                                mainchunkdivlod0[someindexmain][somecounterofarray].somenewcounter = somenewcounter;
                                //mainchunkdivlod0[mainindexchunkdiv][someindexsec].chunkdivpos = new Vector3(mainx * divx * 0.005f, mainy * divy * 0.005f, mainz * divz * 0.005f);

                                //Console.WriteLine("/x:" + xi + " /y:" + yi + "/z:" + zi + "/mi:" + someindexmain);




                                somecounterofarray++;

                                if (somecounterofarray == mainchunkdivlod0[someindexmain].Length)
                                {
                                    somecounterofarray = 0;
                                }








                                /*if (!somelistofdata.Contains(mainindexchunkdiv))
                                {
                                    somelistofdata.Add(mainindexchunkdiv);
                                }
                                else
                                {
                                    Console.WriteLine("ERROR");
                                }*/


                                /*if (!somelistofint.ContainsKey(mainindexchunkdiv))
                                {
                                    somelistofint.Add(mainindexchunkdiv, secindexchunkdiv);
                                }
                                */

                                /*
                                //lod2
                                arraychunkdatalod1[somenewcounter] = new chunkdata();
                                //arraychunkdatalod1[somenewcounter].arraychunkmapslod1 = new sclevelgenmaps();
                                //somemap = arraychunkdatalod1[somenewcounter].arraychunkmapslod1.buildchunkmaps(newchunkpos, somelevelgenprimglobals.planeSize, chunkPos, typeofterraintile, this, 1);
                                arraychunkdatalod1[somenewcounter].arraychunkvertslod1 = new sclevelgenvert(chunkposx, chunkposy, chunkposz, newchunkpos);
                                arraychunkdatalod1[somenewcounter].realpos = newchunkpos;
                                arraychunkdatalod1[somenewcounter].chunkPos = new int[] { chunkposx, chunkposy, chunkposz }; ;
                                arraychunkdatalod1[somenewcounter].arraychunkvertslod1.buildchunkmaps(typeofterraintile, this, 2);

                                //arraychunkdatalod1[somenewcounter].indexinlevelgenmap = indexoftile;
                                arraychunkdatalod1[somenewcounter].indexintypeoftiles = indexinlevelarray;
                                arraychunkdatalod1[somenewcounter].X = chunkposx;
                                arraychunkdatalod1[somenewcounter].Y = chunkposy;
                                arraychunkdatalod1[somenewcounter].Z = chunkposz;

                                //arraychunkdatalod1[somenewcounter].arraychunkvertslod1.chunkPos = chunkPos;
                                //arraychunkdatalod1[somenewcounter].arraychunkvertslod1.newchunkpos = newchunkpos;
                                //arraychunkdatalod1[somenewcounter].arraychunkvertslod1.map = somemap;
                                arraychunkdatalod1[somenewcounter].typeofterraintile = typeofterraintile;
                                arraychunkdatalod1[somenewcounter].arraychunkvertslod1.X = chunkposx;
                                arraychunkdatalod1[somenewcounter].arraychunkvertslod1.Y = chunkposy;
                                arraychunkdatalod1[somenewcounter].arraychunkvertslod1.Z = chunkposz;
                                
                                //lod3
                                arraychunkdatalod2[somenewcounter] = new chunkdata();
                                //arraychunkdatalod2[somenewcounter].arraychunkmapslod2 = new sclevelgenmaps();
                                //somemap = arraychunkdatalod2[somenewcounter].arraychunkmapslod2.buildchunkmaps(newchunkpos, somelevelgenprimglobals.planeSize, chunkPos, typeofterraintile, this, 1);
                                arraychunkdatalod2[somenewcounter].arraychunkvertslod2 = new sclevelgenvert(chunkposx, chunkposy, chunkposz, newchunkpos);
                                arraychunkdatalod2[somenewcounter].realpos = newchunkpos;
                                arraychunkdatalod2[somenewcounter].chunkPos = new int[] { chunkposx, chunkposy, chunkposz }; ;
                                arraychunkdatalod2[somenewcounter].arraychunkvertslod2.buildchunkmaps(typeofterraintile, this, 3);

                                //arraychunkdatalod2[somenewcounter].indexinlevelgenmap = indexoftile;
                                arraychunkdatalod2[somenewcounter].indexintypeoftiles = indexinlevelarray;
                                arraychunkdatalod2[somenewcounter].X = chunkposx;
                                arraychunkdatalod2[somenewcounter].Y = chunkposy;
                                arraychunkdatalod2[somenewcounter].Z = chunkposz;
                                //arraychunkdatalod2[somenewcounter].arraychunkvertslod2.chunkPos = chunkPos;
                                //arraychunkdatalod2[somenewcounter].arraychunkvertslod2.newchunkpos = newchunkpos;
                                //arraychunkdatalod2[somenewcounter].arraychunkvertslod2.map = somemap;
                                arraychunkdatalod2[somenewcounter].typeofterraintile = typeofterraintile;
                                arraychunkdatalod2[somenewcounter].arraychunkvertslod2.X = chunkposx;
                                arraychunkdatalod2[somenewcounter].arraychunkvertslod2.Y = chunkposy;
                                arraychunkdatalod2[somenewcounter].arraychunkvertslod2.Z = chunkposz;
                                */

                                /*
                                //lod4
                                arraychunkdatalod3[somenewcounter] = new chunkdata();
                                //arraychunkdatalod3[somenewcounter].arraychunkmapslod3 = new sclevelgenmaps();
                                //somemap = arraychunkdatalod3[somenewcounter].arraychunkmapslod3.buildchunkmaps(newchunkpos, somelevelgenprimglobals.planeSize, chunkPos, typeofterraintile, this, 1);
                                arraychunkdatalod3[somenewcounter].arraychunkvertslod3 = new sclevelgenvert(chunkposx, chunkposy, chunkposz, newchunkpos);
                                arraychunkdatalod3[somenewcounter].realpos = newchunkpos;
                                arraychunkdatalod3[somenewcounter].chunkPos = new int[] { chunkposx, chunkposy, chunkposz }; ;
                                arraychunkdatalod3[somenewcounter].arraychunkvertslod3.buildchunkmaps(typeofterraintile, this, 4);

                                //arraychunkdatalod3[somenewcounter].indexinlevelgenmap = indexoftile;
                                arraychunkdatalod3[somenewcounter].indexintypeoftiles = indexinlevelarray;
                                arraychunkdatalod3[somenewcounter].X = chunkposx;
                                arraychunkdatalod3[somenewcounter].Y = chunkposy;
                                arraychunkdatalod3[somenewcounter].Z = chunkposz;
                                //arraychunkdatalod3[somenewcounter].arraychunkvertslod3.chunkPos = chunkPos;
                                //arraychunkdatalod3[somenewcounter].arraychunkvertslod3.newchunkpos = newchunkpos;
                                //arraychunkdatalod3[somenewcounter].arraychunkvertslod3.map = somemap;
                                arraychunkdatalod3[somenewcounter].typeofterraintile = typeofterraintile;
                                arraychunkdatalod3[somenewcounter].arraychunkvertslod3.X = chunkposx;
                                arraychunkdatalod3[somenewcounter].arraychunkvertslod3.Y = chunkposy;
                                arraychunkdatalod3[somenewcounter].arraychunkvertslod3.Z = chunkposz;

                                //lod5
                                arraychunkdatalod4[somenewcounter] = new chunkdata();
                                //arraychunkdatalod4[somenewcounter].arraychunkmapslod4 = new sclevelgenmaps();
                                //somemap = arraychunkdatalod4[somenewcounter].arraychunkmapslod4.buildchunkmaps(newchunkpos, somelevelgenprimglobals.planeSize, chunkPos, typeofterraintile, this, 1);
                                arraychunkdatalod4[somenewcounter].arraychunkvertslod4 = new sclevelgenvert(chunkposx, chunkposy, chunkposz, newchunkpos);
                                arraychunkdatalod4[somenewcounter].realpos = newchunkpos;
                                arraychunkdatalod4[somenewcounter].chunkPos = new int[] { chunkposx, chunkposy, chunkposz }; ;
                                arraychunkdatalod4[somenewcounter].arraychunkvertslod4.buildchunkmaps(typeofterraintile, this, 5);

                                //arraychunkdatalod4[somenewcounter].indexinlevelgenmap = indexoftile;
                                arraychunkdatalod4[somenewcounter].indexintypeoftiles = indexinlevelarray;
                                arraychunkdatalod4[somenewcounter].X = chunkposx;
                                arraychunkdatalod4[somenewcounter].Y = chunkposy;
                                arraychunkdatalod4[somenewcounter].Z = chunkposz;
                                //arraychunkdatalod4[somenewcounter].arraychunkvertslod4.chunkPos = chunkPos;
                                //arraychunkdatalod4[somenewcounter].arraychunkvertslod4.newchunkpos = newchunkpos;
                                //arraychunkdatalod4[somenewcounter].arraychunkvertslod4.map = somemap;
                                arraychunkdatalod4[somenewcounter].typeofterraintile = typeofterraintile;
                                arraychunkdatalod4[somenewcounter].arraychunkvertslod4.X = chunkposx;
                                arraychunkdatalod4[somenewcounter].arraychunkvertslod4.Y = chunkposy;
                                arraychunkdatalod4[somenewcounter].arraychunkvertslod4.Z = chunkposz;
                                */

                                somenewcounter++;


                                zis++;
                                if (zis == (incrementsdivz))
                                {
                                    xis++;
                                    zis = 0;
                                }

                                if (xis == incrementsdivx)
                                {
                                    yis++;
                                    xis = 0;
                                }
                                if (yis == (incrementsdivy)) // 
                                {
                                    zi++;
                                    if (zi == (divz))
                                    {
                                        xi++;
                                        zi = 0;
                                    }
                                    if (xi == divx)
                                    {
                                        yi++;
                                        xi = 0;
                                    }
                                    if (yi == (divy)) // 
                                    {
                                        yi = 0;
                                    }
                                    yis = 0;
                                }

                                someindexmain = xi + (divx) * (yi + (divy) * zi);

                            }
                        }












                        /*if (xi >= incrementsdivx)
                        {
                            xi = 0;
                        }
                        else
                        {
                            xi++;
                        }*/





                        /*
                        if (yi >= (incrementsdivy)) // 
                        {
                            yi = 0;
                        }
                        */

                        /*if (xi >= (incrementsdivx) && swtchx == 1) //
                        {
                            yi++;
                            xi = 0;
                            swtchx = 0;
                            swtchy = 1;
                        }
                        if (yi >= (incrementsdivy) && swtchy == 1) // 
                        {
                            yi = 0;
                            swtchy = 0;
                            swtchx = 0;
                            swtchz = 1;
                        }*/

                        incrementsdivzc++;
                        //maincz++;
                    }
                    incrementsdivyc++;
                    //maincy++;
                }
                incrementsdivxc++;
                //maincx++;
            }


            //Console.WriteLine(somelistofint.Count);

            /*var someenumerator = somelistofint.GetEnumerator();

            while (someenumerator.MoveNext())
            {
                var somecurrent = someenumerator.Current;
                var somekey = somecurrent.Key;
                var somevalue = somecurrent.Value;
            }*/


            /*
            int somecounterchunk = 0;

            for (int m = 0; m< mainchunkdivlod0.Length;m++)
            {
                for (int c = 0; c < mainchunkdivlod0[m].Length; c++)
                {
                    if (mainchunkdivlod0[m][c].arraychunkvertslod0 != null)
                    {
                        if (mainchunkdivlod0[m][c].arraychunkvertslod0.arrayofverts != null)
                        {
                            if (mainchunkdivlod0[m][c].arraychunkvertslod0.arrayofverts.Length > 0)
                            {
                                somecounterchunk++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("somecountttotal:" + somecounterchunk);*/







            staticContantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
            dynamicConstantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

            incrementsdivxc = 0;
            incrementsdivyc = 0;
            incrementsdivzc = 0;

            somenewcounter = 0;

            for (var x = sccslevelgen.minx; x < sccslevelgen.maxx; x++)
            {

                if (incrementsdivxc >= incrementsdivx)
                {
                    /*if (maincx >= mainmaxx)
                    {

                      
                        maincx = 0;
                    }*/
                    mainx++;
                    if (mainx >= mainmaxx)
                    {
                        mainx = 0;
                    }
                    incrementsdivxc = 0;
                }




                for (var y = sccslevelgen.miny; y < sccslevelgen.maxy; y++)
                {
                    if (incrementsdivyc >= incrementsdivy)
                    {
                        mainy++;
                        /*if (maincy >= mainmaxy)
                        {

                          
                            maincy = 0;
                        }*/
                        if (mainy >= mainmaxy)
                        {
                            mainy = 0;
                        }
                        incrementsdivyc = 0;
                    }
                    for (var z = sccslevelgen.minz; z < sccslevelgen.maxz; z++)
                    {
                        if (incrementsdivzc >= incrementsdivz)
                        {
                            mainz++;

                            /*if (maincz >= mainmaxz)
                            {

                         
                                maincz = 0;
                            }*/
                            if (mainz >= mainmaxz)
                            {
                                mainz = 0;
                            }

                            incrementsdivzc = 0;
                        }
                        int xx = x;
                        int yy = y;
                        int zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = xx + (sccslevelgen.maxx - 1);
                        }

                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = yy + (sccslevelgen.maxy - 1);
                        }
                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = zz + (sccslevelgen.maxz - 1);
                        }

                        int indexinlevelarray = xx + sccslevelgen.somewidth * (yy + sccslevelgen.someheight * zz); //y is always 0 on floor tiles


                        if (indexinlevelarray < sccslevelgen.somewidth * sccslevelgen.someheight * sccslevelgen.somedepth)
                        {
                            int chunkposx = x;
                            int chunkposy = y;
                            int chunkposz = z;

                            int typeofterraintile = sccslevelgen.levelmap[indexinlevelarray];

                            facetype = 0;

                            if (typeofterraintile == 0 ||
                               typeofterraintile == 1101 ||
                               typeofterraintile == 1102 ||
                               typeofterraintile == 1103 ||
                               typeofterraintile == 1104 ||
                               typeofterraintile == 1105 ||
                               typeofterraintile == 1106 ||
                               typeofterraintile == 1107 ||
                               typeofterraintile == 1108 ||
                               typeofterraintile == 1109 ||
                               typeofterraintile == 1110 ||
                               typeofterraintile == 1111 ||
                               typeofterraintile == 1112 ||
                                typeofterraintile == -99 ||
                                typeofterraintile == 1115)
                            {
                                //lod1
                                arraychunkdatalod0[somenewcounter].arraychunkvertslod0.startBuildingArray(facetype, this, 1);
                                //Console.WriteLine(arraychunkdatalod0[somenewcounter].arraychunkvertslod0.chunkPos);
                                if (arraychunkdatalod0[somenewcounter].arraychunkvertslod0.arrayofverts.Length > 0)
                                {
                                    // Create Constant Buffer 
                                    //staticContantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
                                    //dynamicConstantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

                                    arraychunkdatalod0[somenewcounter].verticesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, arraychunkdatalod0[somenewcounter].arraychunkvertslod0.arrayofverts);
                                    arraychunkdatalod0[somenewcounter].indicesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, arraychunkdatalod0[somenewcounter].arraychunkvertslod0.arrayoftrigs);
                                    arraychunkdatalod0[somenewcounter].staticContantBuffer = staticContantBuffer;
                                    arraychunkdatalod0[somenewcounter].dynamicConstantBuffer = dynamicConstantBuffer;

                                    arraychunkdatalod0[somenewcounter].vertexShader = vertexShader;
                                    arraychunkdatalod0[somenewcounter].pixelShader = pixelShader;
                                    arraychunkdatalod0[somenewcounter].layout = layout;

                                    //int mainindexchunkdiv = mainx + divx * (mainy + divy * mainz);
                                    //Console.WriteLine(mainx + " /y: " + mainy + "z/:" + mainz + "/mi:" + mainindexchunkdiv);

                                    //int secindexchunkdiv = incrementsdivxc + incrementsdivx * (incrementsdivyc + incrementsdivy * incrementsdivzc); //y is always 0 on floor tiles
                                    //mainchunkdivlod0[mainindexchunkdiv][secindexchunkdiv] = arraychunkdatalod0[somenewcounter];
                                }


                                /*//lod2
                                arraychunkdatalod1[somenewcounter].arraychunkvertslod1.startBuildingArray(facetype, this, 2);

                                //Console.WriteLine(arraychunkdatalod1[somenewcounter].arraychunkvertslod1.chunkPos);
                                if (arraychunkdatalod1[somenewcounter].arraychunkvertslod1.arrayofverts.Length > 0)
                                {
                                    // Create Constant Buffer 
                                    //staticContantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
                                    //dynamicConstantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

                                    arraychunkdatalod1[somenewcounter].verticesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, arraychunkdatalod1[somenewcounter].arraychunkvertslod1.arrayofverts);
                                    arraychunkdatalod1[somenewcounter].indicesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, arraychunkdatalod1[somenewcounter].arraychunkvertslod1.arrayoftrigs);
                                    arraychunkdatalod1[somenewcounter].staticContantBuffer = staticContantBuffer;
                                    arraychunkdatalod1[somenewcounter].dynamicConstantBuffer = dynamicConstantBuffer;

                                    arraychunkdatalod1[somenewcounter].vertexShader = vertexShader;
                                    arraychunkdatalod1[somenewcounter].pixelShader = pixelShader;
                                    arraychunkdatalod1[somenewcounter].layout = layout;
                                }

                                
                                //lod3
                                arraychunkdatalod2[somenewcounter].arraychunkvertslod2.startBuildingArray(facetype, this, 3);

                                //Console.WriteLine(arraychunkdatalod2[somenewcounter].arraychunkvertslod2.chunkPos);
                                if (arraychunkdatalod2[somenewcounter].arraychunkvertslod2.arrayofverts.Length > 0)
                                {
                                    // Create Constant Buffer 
                                    //staticContantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
                                    //dynamicConstantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

                                    arraychunkdatalod2[somenewcounter].verticesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, arraychunkdatalod2[somenewcounter].arraychunkvertslod2.arrayofverts);
                                    arraychunkdatalod2[somenewcounter].indicesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, arraychunkdatalod2[somenewcounter].arraychunkvertslod2.arrayoftrigs);
                                    arraychunkdatalod2[somenewcounter].staticContantBuffer = staticContantBuffer;
                                    arraychunkdatalod2[somenewcounter].dynamicConstantBuffer = dynamicConstantBuffer;

                                    arraychunkdatalod2[somenewcounter].vertexShader = vertexShader;
                                    arraychunkdatalod2[somenewcounter].pixelShader = pixelShader;
                                    arraychunkdatalod2[somenewcounter].layout = layout;
                                }
                                */

                                /*
                                //lod4
                                arraychunkdatalod3[somenewcounter].arraychunkvertslod3.startBuildingArray(facetype, this, 4);

                                //Console.WriteLine(arraychunkdatalod3[somenewcounter].arraychunkvertslod3.chunkPos);
                                if (arraychunkdatalod3[somenewcounter].arraychunkvertslod3.arrayofverts.Length > 0)
                                {
                                    // Create Constant Buffer 
                                    //staticContantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
                                    //dynamicConstantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

                                    arraychunkdatalod3[somenewcounter].verticesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, arraychunkdatalod3[somenewcounter].arraychunkvertslod3.arrayofverts);
                                    arraychunkdatalod3[somenewcounter].indicesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, arraychunkdatalod3[somenewcounter].arraychunkvertslod3.arrayoftrigs);
                                    arraychunkdatalod3[somenewcounter].staticContantBuffer = staticContantBuffer;
                                    arraychunkdatalod3[somenewcounter].dynamicConstantBuffer = dynamicConstantBuffer;

                                    arraychunkdatalod3[somenewcounter].vertexShader = vertexShader;
                                    arraychunkdatalod3[somenewcounter].pixelShader = pixelShader;
                                    arraychunkdatalod3[somenewcounter].layout = layout;
                                }



                                //lod5
                                arraychunkdatalod4[somenewcounter].arraychunkvertslod4.startBuildingArray(facetype, this, 5);

                                //Console.WriteLine(arraychunkdatalod4[somenewcounter].arraychunkvertslod4.chunkPos);
                                if (arraychunkdatalod4[somenewcounter].arraychunkvertslod4.arrayofverts.Length > 0)
                                {
                                    // Create Constant Buffer 
                                    //staticContantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
                                    //dynamicConstantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

                                    arraychunkdatalod4[somenewcounter].verticesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, arraychunkdatalod4[somenewcounter].arraychunkvertslod4.arrayofverts);
                                    arraychunkdatalod4[somenewcounter].indicesbuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, arraychunkdatalod4[somenewcounter].arraychunkvertslod4.arrayoftrigs);
                                    arraychunkdatalod4[somenewcounter].staticContantBuffer = staticContantBuffer;
                                    arraychunkdatalod4[somenewcounter].dynamicConstantBuffer = dynamicConstantBuffer;

                                    arraychunkdatalod4[somenewcounter].vertexShader = vertexShader;
                                    arraychunkdatalod4[somenewcounter].pixelShader = pixelShader;
                                    arraychunkdatalod4[somenewcounter].layout = layout;
                                }*/
                                somenewcounter++;
                            }
                        }
                        //incrementsdivzc++;
                        //maincz++;
                    }
                    //incrementsdivyc++;
                    //maincy++;
                }
                //incrementsdivxc++;
                //maincx++;
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
            public float[] realpos;
            public int[] chunkPos;
            //public int indexinlevelgenmap;
            public int indexintypeoftiles;
            public int typeofterraintile;

            public int X;
            public int Y;
            public int Z;

            public Vector3 chunkdivpos;
            public int mainchunkdivindex;
            public int somenewcounter;
            //public SharpDX.Direct3D11.Buffer ConstantLightBuffer;
        }

        public chunkdata[][] mainchunkdivlod0;


        public sclevelgenvert getchunkinlevelgenmap(int x, int y, int z, int levelofdetail)
        {
            int orix = x;
            int oriy = y;
            int oriz = z;

            if (x < 0)
            {
                x *= -1;
                x = x + (sccslevelgen.maxx - 1);
            }

            if (y < 0)
            {
                y *= -1;
                y = y + (sccslevelgen.maxy - 1);
            }
            if (z < 0)
            {
                z *= -1;
                z = z + (sccslevelgen.maxz - 1);
            }

            int indexinsclevelgenmap = x + sccslevelgen.somewidth * (y + sccslevelgen.someheight * z);

            if (indexinsclevelgenmap < sccslevelgen.somewidth * sccslevelgen.someheight * sccslevelgen.somedepth)
            {
                int typeofterraintile = sccslevelgen.levelmap[indexinsclevelgenmap];

                if (typeofterraintile == 0 ||
                    typeofterraintile == 1101 ||
                    typeofterraintile == 1102 ||
                    typeofterraintile == 1103 ||
                    typeofterraintile == 1104 ||
                    typeofterraintile == 1105 ||
                    typeofterraintile == 1106 ||
                    typeofterraintile == 1107 ||
                    typeofterraintile == 1108 ||
                    typeofterraintile == 1109 ||
                    typeofterraintile == 1110 ||
                    typeofterraintile == 1111 ||
                    typeofterraintile == 1112 ||
                    typeofterraintile == -99 ||
                    typeofterraintile == 1115)
                {




                    if (arraychunkdatalod0[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod0 != null)
                    {
                        if (levelofdetail == 1)
                        {
                            return arraychunkdatalod0[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod0;
                        }
                        else if (levelofdetail == 2)
                        {
                            return arraychunkdatalod1[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod1;
                        }
                        else if (levelofdetail == 3)
                        {
                            return arraychunkdatalod2[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod2;
                        }
                        else if (levelofdetail == 4)
                        {
                            return arraychunkdatalod3[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod3;
                        }
                        else if (levelofdetail == 5)
                        {
                            return arraychunkdatalod4[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod4;
                        }
                    }
                }
            }

            return null;
        }


        /*
        public sclevelgenvert getchunkinlevelgenmap(int x, int y, int z, int levelofdetail) //, Vector3 chunkPos
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
                        if (indexinvectorarray == arraychunkdatalod0[indexinvectorarray].indexinlevelgenmap)
                        {
                            //if (arraychunkdatalod0[indexinvectorarray].X == orix &&
                            ///    arraychunkdatalod0[indexinvectorarray].Y == oriy &&
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
                    else
                    {
                        Console.WriteLine("null chunk");
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
        //Console.WriteLine();
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
        //Console.WriteLine();
        return null;
    }*/


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




        /*
        public sclevelgenvert getChunklod0(int x, int y, int z, out int arrayindex) //, Vector3 chunkPos
        {

            //int x0 = ((x));
            //int y0 = ((y));
            //int z0 = ((z));

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
        }*/













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
