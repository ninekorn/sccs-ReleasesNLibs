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
    public class tutorialcubeaschunkinst : IDisposable
    {
        int lastkeyindex;
        //public Buffer staticContantBuffer;
        //public Buffer dynamicConstantBuffer;


        //instancetype[] instances;

        int last_bit(int number)
        {
            return number - (number >> 1 << 1);
        }

        int nth_bit(int number, int position)
        {
            return last_bit(number >> position);
        }

        public struct instancetype
        {
            public Vector4 instancePos;
        };

        public SharpDX.Direct3D11.Buffer ConstantLightBuffer;

        public sccslevelgen sccslevelgen;

        //public sclevelgen sclevelgen;
        //public LevelGenerator4 levelgen;
        public sclevelgenglobals somelevelgenprimglobals;

        //public SharpDX.Direct3D11.Buffer IndicesBuffer;
        //public Buffer staticContantBuffer;
        // public Buffer dynamicConstantBuffer;
        public InputLayout layout;
        public PixelShader pixelShader;
        public VertexShader vertexShader;
        //public Buffer verticesbuffer;
        //public Vector4[] vertices;

        public List<tutorialfacemesh> somefacemeshlisttodraw;

        public static tutorialcubeaschunkinst currenttutorialcubeaschunkinst;


        //[StructLayout(LayoutKind.Explicit, Size = 80)]
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
        }


        /*

        [StructLayout(LayoutKind.Explicit)]
        public struct DVertex
        {
            [FieldOffset(0)]
            public Vector4 position;
            [FieldOffset(16)]
            public Vector4 color;
            [FieldOffset(32)]
            public Vector4 normal;
        }*/


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

        tutorialfacemesh[] arrayoffacemesh;

        public DLightBuffer[] lightBuffer = new DLightBuffer[1];

        //sclevelgenmaps[] arraychunkmapslod0;
        //sclevelgenvert[] arraychunkvertslod0;


        //public chunkdata[] arraychunkdatalod0;
        /*public chunkdata[] arraychunkdatalod1;
        public chunkdata[] arraychunkdatalod2;
        public chunkdata[] arraychunkdatalod3;
        public chunkdata[] arraychunkdatalod4;*/

        //int[] arrayofindexes;


        SharpDX.Direct3D11.Device device;
        public tutorialcubeaschunkinst(Device device_, int facetype, sccslevelgen sccslevelgen_, int mainminx, int mainminy, int mainminz, int mainmaxx, int mainmaxy, int mainmaxz, int somemaincounter, out int somemaincounter_)
        {

            device = device_;
            sccslevelgen = sccslevelgen_;

            currenttutorialcubeaschunkinst = this;




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


            //for 4 by 4 by 4 and placing 8 digits in a float.
            //0-4-1-5-2-6-3-7
            //8-12-9-13-10-14-11-15
            //16-20-17-21-18-22-19-23
            //24-28-25-29-26-30-27-31
            //32-36-33-37-34-38-35-39
            //40-44-41-45-42-46-43-47
            //48-52-49-53-50-54-51-55
            //56-60-57-61-58-62-59-63



            //000 111 222
            //333 444 555
            //666 777 888
            //999 101010 111111
            //121212 131313 141414
            //151515 161616 171717 

            //float proposedmapdata = 876543210.0f;



            //64 bytes or ints. each bytes/ints take 3 digits in each float to a max of 9 digits per floats.

            //float someint = 312331232;

            //432143214 - not receiving data

            float somefloat = 432143; //312331232.0f
            int currentvertdimdata = (int)Math.Truncate(somefloat);

            int somemaxvecdigit = 9;
            int currentIndex = 63;

            int somecountermul = (int)Math.Floor((currentIndex) / 3.0f);
            //int someOtherIndex = 0;

            //var somemin = (somecountermul) * somemaxvecdigit; //0 //8 * 8 = 64 //63 * 8 == 504
            //var somemid = (int)Math.Round(somemaxvecdigit / 3.0f) + somemin; // 4 //504 + 4 = 508
            //var somemax = (somemaxvecdigit) + somemin; //504 + 8 = 512

            /*
            //var someresult = currentIndex % 3;
            var theNumber = 3;
            var remainder = 0;
            var totalTimes = 0;

            for (int i = 0; i < currentIndex; i++)
            {
                if (remainder == theNumber)
                {
                    remainder = 0;
                    totalTimes++;
                }
                if (totalTimes * theNumber >= currentIndex) // >=?? why not only >
                {
                    break;
                }
                remainder++;
            }



            int somemainindex = 0;

            int somevertdata = currentvertdimdata;

            for (int i = 0; i < (somemainindex); i++)
            {
                somevertdata = somevertdata / 10;
            }
            int someres = somevertdata / 10;

            somevertdata = somevertdata - (someres * 10);

            ////Console.WriteLine("/w:" + somevertdata);

            int somemulfordigit = 3;

            somevertdata = currentvertdimdata;
            for (int i = 0; i < (somemulfordigit + 0); i++)
            {
                somevertdata = somevertdata / 10;
            }
            someres = somevertdata / 10;
            int firstvertlocz = somevertdata - (someres * 10);

            somevertdata = currentvertdimdata;
            for (int i = 0; i < (somemulfordigit + 1); i++)
            {
                somevertdata = somevertdata / 10;
            }
            someres = somevertdata / 10;
            int firstvertlocy = somevertdata - (someres * 10);

            somevertdata = currentvertdimdata;
            for (int i = 0; i < (somemulfordigit + 2); i++)
            {
                somevertdata = somevertdata / 10;
            }
            someres = somevertdata / 10;
            int firstvertlocx = somevertdata - (someres * 10);
            */

            int somemulfordigit = 3;

            int somevertdata = (int)Math.Truncate(somefloat);
            for (int i = 0; i < (somemulfordigit + 0); i++)
            {
                somevertdata = somevertdata / 10;
            }
            int someres = somevertdata / 10;
            int somedepth = (somevertdata - (someres * 10));

            somevertdata = (int)Math.Truncate(somefloat);
            for (int i = 0; i < (somemulfordigit + 1); i++)
            {
                somevertdata = somevertdata / 10;
            }
            someres = somevertdata / 10;
            int someheight = (somevertdata - (someres * 10));

            somevertdata = (int)Math.Truncate(somefloat);
            for (int i = 0; i < (somemulfordigit + 2); i++)
            {
                somevertdata = somevertdata / 10;
            }
            someres = somevertdata / 10;
            int somewidth = (somevertdata - (someres * 10));

            //Console.WriteLine("/x:" + somewidth + "/y:" + someheight + "/z:" + somedepth);

            //Console.WriteLine("/x:" + firstvertlocx + "/y:" + firstvertlocy + "/z:" + firstvertlocz);





            Console.WriteLine("minx:" + mainminx + "/miny:" + mainminy + "/minz:" + mainminz + "/maxx:" + mainmaxx + "/maxy:" + mainmaxy + "/maxz:" + mainmaxz);

            //float someresult = somevertdata - (int)Math.Floor(somevertdata);
            //int somedepth = (int)Math.Floor(someresult * 10.0f);

            /*
            float somefloat = 876543210.0f;
            int currentvertdimdata = (int)Math.Round(somefloat);

            int somemainindex = 0;

            float somevertdata = currentvertdimdata;
            for (int i = 0; i < (somemainindex + 1); i++)
            {
                somevertdata = (somevertdata * 0.1f);
            }

            float someresult = somevertdata - (float)Math.Floor(somevertdata);

            int somedepth = (int)Math.Floor(someresult * 10.0f);


            somevertdata = currentvertdimdata;
            for (int i = 0; i < (somemainindex + 2); i++)
            {
                somevertdata = (somevertdata * 0.1f);
            }
            someresult = somevertdata - (float)Math.Floor(somevertdata);
            int someheight = (int)Math.Floor(someresult * 10.0f);


            somevertdata = currentvertdimdata;
            for (int i = 0; i < (somemainindex + 3); i++)
            {
                somevertdata = (somevertdata * 0.1f);
            }
            someresult = somevertdata - (float)Math.Floor(somevertdata);
            int somewidth = (int)Math.Floor(someresult * 10.0f);
            */

            //int somemax = ((somecountermul) * somemaxvecdigit); //8

            /*if (currentIndex >= somemin && currentIndex <= somemid - 1) // 0 // 3
            {
                //511010110
                someOtherIndex = 1 + (((somemid - 1) - currentIndex) * 3);
                //index 0 => 1 + (3 - 0) * 2 = 7
                //index 1 => 1 + (3 - 1) * 2 = 5
                //index 2 => 1 + (3 - 2) * 2 = 3
                //index 3 => 1 + (3 - 3) * 2 = 1

                /*if(someOtherIndex == 7)
                {
                    someOtherIndex = 0;
                }
                else if(someOtherIndex == 5)
                {
                    someOtherIndex = 2;
                }
                else if(someOtherIndex == 3)
                {
                    someOtherIndex = 4;
                }
                else if(someOtherIndex == 1)
                {
                    someOtherIndex = 6;
                }

            }
            else if (currentIndex >= somemid && currentIndex <= somemax - 1) // 4 // 7
            {
                //511010110

                someOtherIndex = 0 + (((somemax - 1) - currentIndex) * 3);
                //index 4 => 0 + (7 - 4) * 2 = 6
                //index 5 => 0 + (7 - 5) * 2 = 4
                //index 6 => 0 + (7 - 6) * 2 = 2
                //index 7 => 0 + (7 - 7) * 2 = 0

                //index 7 => 0 + (512-1 - 508) * 2 = 0

                /*if(someOtherIndex == 6)
                {
                    someOtherIndex = 1;
                }
                else if(someOtherIndex == 4)
                {
                    someOtherIndex = 3;
                }
                else if(someOtherIndex == 2)
                {
                    someOtherIndex = 5;
                }
                else if(someOtherIndex == 0)
                {
                    someOtherIndex = 7;
                }
            }*/


            ////Console.WriteLine("/w:" + somewidth+ "/h:" + someheight+ "/d:" + somedepth);


            ////Console.WriteLine("somecountermul:" + somecountermul + "/result0:" + someresult + "/result1:" + remainder);




            /*int somecurrentMapData = 432101234;
            int somenewindex = 1;

            float somenewmapdata = somecurrentMapData;
            for (int i = 0; i < (somenewindex); i++)
            {
                somenewmapdata = (somenewmapdata * 0.1f);
            }

            float somenewresult = somenewmapdata - (float)Math.Floor(somenewmapdata);
            somenewresult = somenewresult * 10.0f;
            //Console.WriteLine("somenewmapresult" + somenewresult);
            */

            /*
            int somecurrentMapData = 1110;
            int someOtherIndex = 0;
            int testera = 0;
            int theByte = 0;
            int before0 = 0;

            if (someOtherIndex == 0)
            {
                testera = (int)somecurrentMapData >> 1 << 1;
                theByte = (int)somecurrentMapData - testera;
            }
            else
            {
                float someData0 = somecurrentMapData;

                for (int i = 0; i < someOtherIndex; i++)
                {
                    someData0 = (int)(someData0 * 0.1f);
                }

                before0 = (int)(Math.Truncate(someData0));
                testera = before0 >> 1 << 1;
                theByte = before0 - testera;
            }

            //Console.WriteLine(theByte);*/





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

            /*
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
            somelevelgenprimglobals.depthlod4 = 1;*/

            somelevelgenprimglobals.widthlod0 = 4;
            somelevelgenprimglobals.heightlod0 = 4;
            somelevelgenprimglobals.depthlod0 = 4;



            //6w*6h*6d * 1 face per byte since we are building the faces in individual meshes here. = 216 * 2 = 432 digits needed for inserting the width and depth for the top face.

            //4w*4h*4d * 1 face per byte since we are building the faces in individual meshes here. = 64 * 2 = 128 digits needed for inserting the width and depth for the top face.
            //64 digits needed for each bytes that will give the index for the start of the first vertex of each face.

            //4 * 4 * 4 = 64 bytes total
            //minimum of 128 bytes to cover witdh and depth of faces//
            //we are going to start with the top face.


            //4 * 8 * 10 == 320
            //10*4*4 = 160 max chunk bytes included in 4 floats x 4 coordinates per 1 worldmatrix => 2 worldmatrices = 320
            //10*4*4*4 for 4 worldmatrices = 640 / 2 for width and height of the specific vertex set to 1 in the 5th/6th worldmatrix.

            //Not gonna work
            //8*6*10 == 480 for 3 vectors...
            //max chunk value for that is 4*5*8
            //int somefloat = nth_bit(11111011, 6);// (int)Math.Truncate(0111111111111111.1111111111111111f);
            //double somefloat = 11111111.011111111d;// - 11111111.0d;////nth_bit(11111011, 14);// (int)Math.Truncate(0111111111111111.1111111111111111f);

            //int somefloat = 1111111111; 
            //int somefloat = 4294967295;
            /*float somefloat = 9876543210f; //1111111111
            //double somefloatright = somefloat - (double)Math.Floor(somefloat);

            int someindex = 7;
            //var someData0 = somefloat;

            for (int i = 0; i < someindex; i++)
            {
                somefloat = (somefloat * 0.1f);
            }

            somefloat = somefloat - (float)Math.Floor(somefloat);
            somefloat = (float)Math.Floor(somefloat * 10.0f);
            //somefloat = (float)(somefloat * 1000000.0f);// - (float)Math.Truncate(somefloat * 1000000.0f);
            */
            /*var before0 = (int)((somefloat));
            //https://stackoverflow.com/questions/46312893/how-do-you-use-bit-shift-operators-to-find-out-a-certain-digit-of-a-number-in-ba
            var someremains = before0 >> 1 << 1;
            var currentByte = before0 - someremains;*/

            ////Console.WriteLine("byte: " + currentByte + " " + somefloat); // + " " + somefloatright.ToString()

            ////Console.WriteLine("byte: " + " " + somefloat); // + " " + somefloatright.ToString()



            /*
            float somefloat = 876543210f; //1111111111
            //double somefloatright = somefloat - (double)Math.Floor(somefloat);

            int someindex = 7;
            //var someData0 = somefloat;

            for (int i = 0; i < someindex; i++)
            {
                somefloat = (somefloat * 0.1f);
            }

            somefloat = somefloat - (float)Math.Floor(somefloat);
            somefloat = (float)Math.Floor(somefloat * 10.0f);

            //Console.WriteLine("byte: " + " " + somefloat);*/

            //int somenthbit = nth_bit((int)somefloat, 0);
            ////Console.WriteLine("byte: " + somefloat);

            /*
            uint someleft = (uint)(11111111);
            uint someright = (uint)(01111111);

            //BitConverter.

            byte[] array = BitConverter.GetBytes(somefloat);
            var someres = BitConverter.ToDouble(array, 0);

            //Console.WriteLine(someres);*/


            //uint sometest = 0xFFFFFFFF;
            ////Console.WriteLine(sometest);
            //4294967295

            /////////////////
            //xxxx
            //x1x3
            //x0x2
            //xxxx

            /////////////////

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

































            int totaltilescounter = 0;

            int sometotalmaplength = sccslevelgen.somewidth * sccslevelgen.someheight * sccslevelgen.somedepth;

            for (var x = mainminx; x < mainmaxx; x++)
            {
                for (var y = mainminy; y < mainmaxy; y++)
                {
                    for (var z = mainminz; z < mainmaxz; z++)
                    {
                        //Console.WriteLine("/x:" + x + "/y:" + y + "/z:" + z);

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

            ////Console.WriteLine("originallength:" + sometotalmaplength + "/newlength:" + totaltilescounter);

            //arrayofindexes = new int[sccslevelgen.levelmap.Length];
            //int[][] arrayofpositions = new int[sccslevelgen.levelmap.Length][];

            //arraychunkmapslod0 = new sclevelgenmaps[levelgen.typeoftiles.Count];
            //arraychunkvertslod0 = new sclevelgenvert[levelgen.typeoftiles.Count];


            /*
            arraychunkdatalod0 = new chunkdata[totaltilescounter * 8];


            arraychunkdatalod1 = new chunkdata[totaltilescounter];
            arraychunkdatalod2 = new chunkdata[totaltilescounter];
            arraychunkdatalod3 = new chunkdata[totaltilescounter];
            arraychunkdatalod4 = new chunkdata[totaltilescounter];*/


            int[] somemap;
            Vector4[] arrayofverts;
            int[] arrayoftrigs;

            //int facetype = 0;
            //List<Vector4> listofverts = new List<Vector4>();
            //List<int> listoftrigs = new List<int>();

            /*for (int i = 0; i < arrayofindexes.Length; i++)
            {
                arrayofindexes[i] = -1;
            }*/




            arrayoffacemesh = new tutorialfacemesh[somelevelgenprimglobals.widthlod0 * somelevelgenprimglobals.heightlod0 * somelevelgenprimglobals.depthlod0];
            //tutorialfacemesh[] arrayoffacemeshright = new tutorialfacemesh[somelevelgenprimglobals.widthlod0 * somelevelgenprimglobals.heightlod0 * somelevelgenprimglobals.depthlod0];



            ShaderBytecode bytecodevert = null;
            ShaderBytecode bytecodepix = null;


            /*
             * 
            if (Program.useOculusRift == 0)
            {

                bytecodevert = ShaderBytecode.CompileFromFile("multicubenew.fx", "VS", "vs_5_0");
                bytecodepix = ShaderBytecode.CompileFromFile("multicubenew.fx", "PS", "ps_5_0");


            }
            else if (Program.useOculusRift == 1)
            {

                bytecodevert = ShaderBytecode.CompileFromFile("multicubenewovr.fx", "VS", "vs_5_0");
                bytecodepix = ShaderBytecode.CompileFromFile("multicubenewovr.fx", "PS", "ps_5_0");
            }
            */

            //bytecodevert = ShaderBytecode.CompileFromFile("multicubenew.fx", "VS", "vs_5_0");
            //bytecodepix = ShaderBytecode.CompileFromFile("multicubenew.fx", "PS", "ps_5_0");




            List<int> indexofmesh = new List<int>();

            int sometotalinarray = somelevelgenprimglobals.widthlod0 * somelevelgenprimglobals.heightlod0 * somelevelgenprimglobals.depthlod0;

            int someixx = 0;
            int someiyy = 0;
            int someizz = 0;

            somefacemeshlisttodraw = new List<tutorialfacemesh>();

            for (int i = 0; i < sometotalinarray; i++)
            {

                //, bytecodevert, bytecodepix
                arrayoffacemesh[i] = new tutorialfacemesh(device, someixx + 1, someiyy + 1, someizz + 1, somelevelgenprimglobals.planeSize, i + 1, facetype);
                //arrayoffacemeshright[i] = new tutorialfacemesh(device, someixx + 1, someiyy + 1, someizz + 1, somelevelgenprimglobals.planeSize, this, bytecodevert, bytecodepix, i + 1, 1);

                int someflatindex = someixx + somelevelgenprimglobals.widthlod0 * ((someiyy) + somelevelgenprimglobals.heightlod0 * (someizz));
                ////Console.WriteLine("x:" + someixx + "/y:" + someiyy + "/z:" + someizz);
                someizz++;
                if (someizz == somelevelgenprimglobals.widthlod0)
                {
                    someiyy++;
                    someizz = 0;
                }
                if (someiyy == somelevelgenprimglobals.heightlod0)
                {
                    someixx++;
                    someiyy = 0;
                }
                if (someixx == somelevelgenprimglobals.depthlod0)
                {
                    someixx = 0;
                }
            }



            int indextest = 0 + somelevelgenprimglobals.widthlod0 * ((0) + somelevelgenprimglobals.heightlod0 * (1));

            ////Console.WriteLine("indextest " + indextest);



            //bytecodevert.Dispose();
            //bytecodepix.Dispose();



            int somenewcounter = 0;

            for (var x = mainminx; x < mainmaxx; x++)
            {
                for (var y = mainminy; y < mainmaxy; y++)
                {
                    for (var z = mainminz; z < mainmaxz; z++)
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


                        //currently the main tile is a unit of 1... but we need to divide each unit of 1 by 8 to build
                        //8 chunks for 1 unit instead of building 1 chunk for 1 unit.

                        //indexinlevelarray + 0
                        //indexinlevelarray + 1 => use map of indexinlevelarray + 0
                        //indexinlevelarray + 2 => use map of indexinlevelarray + 0
                        //indexinlevelarray + 3 => use map of indexinlevelarray + 0
                        //indexinlevelarray + 4 => use map of indexinlevelarray + 0
                        //indexinlevelarray + 5 => use map of indexinlevelarray + 0
                        //indexinlevelarray + 6 => use map of indexinlevelarray + 0
                        //indexinlevelarray + 7 => use map of indexinlevelarray + 0


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

                                int[] referencemap = null;

                                int chunkposx = x;
                                int chunkposy = y;
                                int chunkposz = z;

                                float[] newchunkpos = new float[3];

                                newchunkpos[0] = (float)chunkposx;// + (xi * 0.1f);
                                newchunkpos[1] = (float)chunkposy;// + (yi * 0.1f);
                                newchunkpos[2] = (float)chunkposz;// + (zi * 0.1f);

                                /*newchunkpos[0] = newchunkpos[0] * ((somelevelgenprimglobals.widthlod0 * 2) * (somelevelgenprimglobals.planeSize * 0.5f));
                                newchunkpos[1] = newchunkpos[1] * ((somelevelgenprimglobals.heightlod0 * 2) * (somelevelgenprimglobals.planeSize * 0.5f));
                                newchunkpos[2] = newchunkpos[2] * ((somelevelgenprimglobals.depthlod0 * 2) * (somelevelgenprimglobals.planeSize * 0.5f));
                                */

                                newchunkpos[0] = newchunkpos[0] * ((somelevelgenprimglobals.widthlod0) * (somelevelgenprimglobals.planeSize));
                                newchunkpos[1] = newchunkpos[1] * ((somelevelgenprimglobals.heightlod0) * (somelevelgenprimglobals.planeSize));
                                newchunkpos[2] = newchunkpos[2] * ((somelevelgenprimglobals.depthlod0) * (somelevelgenprimglobals.planeSize));



                                for (int xi = 0; xi < 2; xi++)
                                {
                                    for (int yi = 0; yi < 2; yi++)
                                    {
                                        for (int zi = 0; zi < 2; zi++)
                                        {


                                            //if (facetype == 0)
                                            {
                                                float[] arrayofcoords = newchunkpos;
                                                //arrayofcoords[0] += (xi * somelevelgenprimglobals.planeSize * 0.5f);
                                                //arrayofcoords[1] += (yi * somelevelgenprimglobals.planeSize * 0.5f);
                                                //arrayofcoords[2] += (zi * somelevelgenprimglobals.planeSize * 0.5f);



                                                //firstmap
                                                sccslevelgen.arraychunkdatalod0[facetype][somemaincounter] = new chunkdata();


                                                if (xi == 0 && yi == 0 && zi == 0)
                                                {
                                                    if (facetype == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].arraychunkvertslod0 = new tutorialchunkcubemap(chunkposx, chunkposy, chunkposz, arrayofcoords);

                                                        sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].arraychunkvertslod0.buildchunkmaps(0, typeofterraintile, this, 1); //, somechunkkeyboardpriminstanceindex_, chunkprimindex_, chunkinstindex                            

                                                        sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].arraychunkvertslod0.X = chunkposx;
                                                        sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].arraychunkvertslod0.Y = chunkposy;
                                                        sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].arraychunkvertslod0.Z = chunkposz;
                                                    }

                                                    //keep main map as reference                                                                                                                             
                                                    //keep main map as reference
                                                    //referencemap = sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].arraychunkvertslod0.map;
                                                    //keep main map as reference
                                                    //keep main map as reference

                                                }
                                                else
                                                {
                                                    //sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].arraychunkvertslod0.buildchunkmaps(1, typeofterraintile, this, 1); //, somechunkkeyboardpriminstanceindex_, chunkprimindex_, chunkinstindex                            
                                                    //sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].arraychunkvertslod0.map = referencemap;
                                                }

                                                /*sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].arraychunkvertslod0 = new tutorialchunkcubemap(chunkposx, chunkposy, chunkposz, arrayofcoords);

                                                sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].arraychunkvertslod0.buildchunkmaps(0, typeofterraintile, this, 1); //, somechunkkeyboardpriminstanceindex_, chunkprimindex_, chunkinstindex                            

                                                sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].arraychunkvertslod0.X = chunkposx;
                                                sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].arraychunkvertslod0.Y = chunkposy;
                                                sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].arraychunkvertslod0.Z = chunkposz;
                                                */



                                                sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].realpos = arrayofcoords;
                                                sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].chunkPos = new int[] { chunkposx, chunkposy, chunkposz };
                                                sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].X = chunkposx;
                                                sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].Y = chunkposy;
                                                sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].Z = chunkposz;

                                                sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].typeofterraintile = typeofterraintile;
                                                


                                                if (facetype == 0)
                                                {
                                                    //Console.WriteLine(somemaincounter);
                                                    //sccslevelgen.arraychunkdatalod0[facetype][somemaincounter] = arraychunkdatalod0[somenewcounter];

                                                    if (xi == 0 && yi == 0 && zi == 0)
                                                    {
                                                        sccslevelgen.arraychunkdatalod0[facetype][somemaincounter].indexintypeoftiles = indexinlevelarray;

                                                        sccslevelgen.arrayofindexes[indexinlevelarray] = somemaincounter;
                                                    }
                                                    //sccslevelgen.arrayofindexes[indexinlevelarray] = somemaincounter;
                                                }

                                                somemaincounter++;
                                                somenewcounter++;

                                            }
                                        }
                                    }

                                   
                                }
                                //Console.WriteLine("count tiles: " + somecounteroftiles);
                            }
                        }
                    }
                }
            }
            somemaincounter_ = somemaincounter;
        }

        //int somemaincounter_ = 0;







        Dictionary<int, int> arrayofchunkvertsinst = new Dictionary<int, int>();


        public void createthechunks(int mainminx, int mainminy, int mainminz, int mainmaxx, int mainmaxy, int mainmaxz, int facetype, int someseccounter, out int someseccounter_)
        {

            //staticContantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
            //dynamicConstantBuffer = new SharpDX.Direct3D11.Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);


            //Dictionary<int, int> arrayofchunkvertsinstright = new Dictionary<int, int>();

            //Dictionary<int, tutorialchunkcubemap> arrayofchunkvertsinstnew = new Dictionary<int, tutorialchunkcubemap>();

            //List<tutorialchunkcubemap> arrayofchunkvertsinstnew = new List<tutorialchunkcubemap>();

            int counterofmeshtiles = 0;

            int somenewcounter = 0;

            for (var x = mainminx; x < mainmaxx; x++)
            {
                for (var y = mainminy; y < mainmaxy; y++)
                {
                    for (var z = mainminz; z < mainmaxz; z++)
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

                        //indexinlevelarray + 0
                        //indexinlevelarray + 1
                        //indexinlevelarray + 2
                        //indexinlevelarray + 3
                        //indexinlevelarray + 4
                        //indexinlevelarray + 5
                        //indexinlevelarray + 6
                        //indexinlevelarray + 7

                        if (indexinlevelarray < sccslevelgen.somewidth * sccslevelgen.someheight * sccslevelgen.somedepth)
                        {
                            int chunkposx = x;
                            int chunkposy = y;
                            int chunkposz = z;

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

                                int thefirstbundlechunkindex = 0;

                                //int somecounter = 0;
                                for (int xi = 0; xi < 2; xi++)
                                {
                                    for (int yi = 0; yi < 2; yi++)
                                    {
                                        for (int zi = 0; zi < 2; zi++)
                                        {

                                            if (xi == 0 && yi == 0 && zi == 0)
                                            {
                                                thefirstbundlechunkindex = someseccounter;// somenewcounter;

                                                //sccslevelgen.arraychunkdatalod0[0][thefirstbundlechunkindex].arraychunkvertslod0.buildchunkmaps(0, typeofterraintile, this, 1);
                                                sccslevelgen.arraychunkdatalod0[0][thefirstbundlechunkindex].arraychunkvertslod0.startBuildingArray(facetype, this, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                                /*//if (facetype == 0)
                                                {

                                                }
                                                else
                                                {
                                                    sccslevelgen.arraychunkdatalod0[facetype][thefirstbundlechunkindex].arraychunkvertslod0.newregenerate(facetype, xi, yi, zi, xi + 1, yi + 1, zi + 1);
                                                }*/
                                            }
                                            else
                                            {
                                                sccslevelgen.arraychunkdatalod0[0][thefirstbundlechunkindex].arraychunkvertslod0.newregenerate(facetype, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                            }



                                            //whatever was set is now unset... by regenerating at everychunk.


                                            //somecounter++;

                                            //lod1

                                            //Console.WriteLine("xi:" + (xi + 1));

                                            int cango = 0;

                                            sccslevelgen.arraychunkdatalod0[facetype][someseccounter].vertexcount = sccslevelgen.arraychunkdatalod0[0][thefirstbundlechunkindex].arraychunkvertslod0.arrayofvertstop.Length;


                                           
                                            if (sccslevelgen.arraychunkdatalod0[0][thefirstbundlechunkindex].arraychunkvertslod0.arrayofvertstop.Length > 0)
                                            {
                                                cango = 1;
                                            }

                                            ////Console.WriteLine(sccslevelgen.arraychunkdatalod0[facetype][someseccounter].arraychunkvertslod0.chunkPos);
                                            if (cango == 1)
                                            {
                                                //arrayofchunkvertsinst.Add(somenewcounter, sccslevelgen.arraychunkdatalod0[facetype][someseccounter].arraychunkvertslod0.arrayofvertstop.Length);
                                                arrayofchunkvertsinst.Add(someseccounter, sccslevelgen.arraychunkdatalod0[0][thefirstbundlechunkindex].arraychunkvertslod0.arrayofvertstop.Length);

                                                double m11a = 0;
                                                double m12a = 0;
                                                double m13a = 0;
                                                double m14a = 0;
                                                double m21a = 0;
                                                double m22a = 0;
                                                double m23a = 0;
                                                double m24a = 0;
                                                double m31a = 0;
                                                double m32a = 0;
                                                double m33a = 0;
                                                double m34a = 0;
                                                double m41a = 0;
                                                double m42a = 0;
                                                double m43a = 0;
                                                double m44a = 0;

                                                double m11b = 0;
                                                double m12b = 0;
                                                double m13b = 0;
                                                double m14b = 0;
                                                double m21b = 0;
                                                double m22b = 0;
                                                double m23b = 0;
                                                double m24b = 0;
                                                double m31b = 0;
                                                double m32b = 0;
                                                double m33b = 0;
                                                double m34b = 0;
                                                double m41b = 0;
                                                double m42b = 0;
                                                double m43b = 0;
                                                double m44b = 0;

                                                sccslevelgen.arraychunkdatalod0[0][thefirstbundlechunkindex].arraychunkvertslod0.insertdimensionsinint(xi, yi, zi, xi + 1, yi + 1, zi + 1, typeofterraintile, this, 1, 6, facetype,
                                                out m11a, out m12a, out m13a, out m14a, out m21a, out m22a, out m23a, out m24a, out m31a, out m32a, out m33a, out m34a, out m41a, out m42a, out m43a, out m44a,
                                                out m11b, out m12b, out m13b, out m14b, out m21b, out m22b, out m23b, out m24b, out m31b, out m32b, out m33b, out m34b, out m41b, out m42b, out m43b, out m44b); //, somechunkkeyboardpriminstanceindex_, chunkprimindex_, chunkinstindex


                                                Matrix somechunkmap = Matrix.Identity;

                                                somechunkmap.M11 = (float)m11a;
                                                somechunkmap.M12 = (float)m12a;
                                                somechunkmap.M13 = (float)m13a;
                                                somechunkmap.M14 = (float)m14a;

                                                somechunkmap.M21 = (float)m21a;
                                                somechunkmap.M22 = (float)m22a;
                                                somechunkmap.M23 = (float)m23a;
                                                somechunkmap.M24 = (float)m24a;

                                                somechunkmap.M31 = (float)m31a;
                                                somechunkmap.M32 = (float)m32a;
                                                somechunkmap.M33 = (float)m33a;
                                                somechunkmap.M34 = (float)m34a;

                                                somechunkmap.M41 = (float)m41a;
                                                somechunkmap.M42 = (float)m42a;
                                                somechunkmap.M43 = (float)m43a;
                                                somechunkmap.M44 = (float)m44a;

                                                sccslevelgen.arraychunkdatalod0[facetype][someseccounter].instancesmatrixa = new scinstancevertdimensions()
                                                {
                                                    instancematrix = somechunkmap,
                                                };

                                                somechunkmap = Matrix.Identity;

                                                somechunkmap.M11 = (float)m11b;
                                                somechunkmap.M12 = (float)m12b;
                                                somechunkmap.M13 = (float)m13b;
                                                somechunkmap.M14 = (float)m14b;

                                                somechunkmap.M21 = (float)m21b;
                                                somechunkmap.M22 = (float)m22b;
                                                somechunkmap.M23 = (float)m23b;
                                                somechunkmap.M24 = (float)m24b;

                                                somechunkmap.M31 = (float)m31b;
                                                somechunkmap.M32 = (float)m32b;
                                                somechunkmap.M33 = (float)m33b;
                                                somechunkmap.M34 = (float)m34b;

                                                somechunkmap.M41 = (float)m41b;
                                                somechunkmap.M42 = (float)m42b;
                                                somechunkmap.M43 = (float)m43b;
                                                somechunkmap.M44 = (float)m44b;

                                                sccslevelgen.arraychunkdatalod0[facetype][someseccounter].instancesmatrixb = new scinstancevertdimensions()
                                                {
                                                    instancematrix = somechunkmap,
                                                };

                                                m11a = 0;
                                                m12a = 0;
                                                m13a = 0;
                                                m14a = 0;
                                                m21a = 0;
                                                m22a = 0;
                                                m23a = 0;
                                                m24a = 0;
                                                m31a = 0;
                                                m32a = 0;
                                                m33a = 0;
                                                m34a = 0;
                                                m41a = 0;
                                                m42a = 0;
                                                m43a = 0;
                                                m44a = 0;
                                                

                                                /*
                                                arraychunkdatalod0[thefirstbundlechunkindex].arraychunkvertslod0.setmapforchunks(out m11a, out m12a, out m13a, out m14a, out m21a, out m22a, out m23a, out m24a, out m31a, out m32a, out m33a, out m34a, out m41a, out m42a, out m43a, out m44a);


                                                somechunkmap = Matrix.Identity;

                                                somechunkmap.M11 = (float)m11a;
                                                somechunkmap.M12 = (float)m12a;
                                                somechunkmap.M13 = (float)m13a;
                                                somechunkmap.M14 = (float)m14a;

                                                somechunkmap.M21 = (float)m21a;
                                                somechunkmap.M22 = (float)m22a;
                                                somechunkmap.M23 = (float)m23a;
                                                somechunkmap.M24 = (float)m24a;

                                                somechunkmap.M31 = (float)m31a;
                                                somechunkmap.M32 = (float)m32a;
                                                somechunkmap.M33 = (float)m33a;
                                                somechunkmap.M34 = (float)m34a;

                                                somechunkmap.M41 = (float)m41a;
                                                somechunkmap.M42 = (float)m42a;
                                                somechunkmap.M43 = (float)m43a;
                                                somechunkmap.M44 = (float)m44a;

                                                sccslevelgen.arraychunkdatalod0[facetype][someseccounter].instanceintmap = new scinstanceintmaps()
                                                {
                                                    instanceintmap = somechunkmap,
                                                };
                                                */
                                                


                                                m11a = 0;
                                                m12a = 0;
                                                m13a = 0;
                                                m14a = 0;
                                                m21a = 0;
                                                m22a = 0;
                                                m23a = 0;
                                                m24a = 0;
                                                m31a = 0;
                                                m32a = 0;
                                                m33a = 0;
                                                m34a = 0;
                                                m41a = 0;
                                                m42a = 0;
                                                m43a = 0;
                                                m44a = 0;

                                                m11b = 0;
                                                m12b = 0;
                                                m13b = 0;
                                                m14b = 0;
                                                m21b = 0;
                                                m22b = 0;
                                                m23b = 0;
                                                m24b = 0;
                                                m31b = 0;
                                                m32b = 0;
                                                m33b = 0;
                                                m34b = 0;
                                                m41b = 0;
                                                m42b = 0;
                                                m43b = 0;
                                                m44b = 0;

                                                sccslevelgen.arraychunkdatalod0[0][thefirstbundlechunkindex].arraychunkvertslod0.setmapforfirstverts(xi, yi, zi, xi + 1, yi + 1, zi + 1, typeofterraintile, this, 1, 6, facetype,
                                                out m11a, out m12a, out m13a, out m14a, out m21a, out m22a, out m23a, out m24a, out m31a, out m32a, out m33a, out m34a, out m41a, out m42a, out m43a, out m44a,
                                                out m11b, out m12b, out m13b, out m14b, out m21b, out m22b, out m23b, out m24b, out m31b, out m32b, out m33b, out m34b, out m41b, out m42b, out m43b, out m44b); //, somechunkkeyboardpriminstanceindex_, chunkprimindex_, chunkinstindex

                                                somechunkmap = Matrix.Identity;

                                                somechunkmap.M11 = (float)m11a;
                                                somechunkmap.M12 = (float)m12a;
                                                somechunkmap.M13 = (float)m13a;
                                                somechunkmap.M14 = (float)m14a;

                                                somechunkmap.M21 = (float)m21a;
                                                somechunkmap.M22 = (float)m22a;
                                                somechunkmap.M23 = (float)m23a;
                                                somechunkmap.M24 = (float)m24a;

                                                somechunkmap.M31 = (float)m31a;
                                                somechunkmap.M32 = (float)m32a;
                                                somechunkmap.M33 = (float)m33a;
                                                somechunkmap.M34 = (float)m34a;

                                                somechunkmap.M41 = (float)m41a;
                                                somechunkmap.M42 = (float)m42a;
                                                somechunkmap.M43 = (float)m43a;
                                                somechunkmap.M44 = (float)m44a;

                                                sccslevelgen.arraychunkdatalod0[facetype][someseccounter].instanceintmapfirstvertexa = new scinstancevertdimensions()
                                                {
                                                    instancematrix = somechunkmap,
                                                };

                                                somechunkmap = Matrix.Identity;

                                                somechunkmap.M11 = (float)m11b;
                                                somechunkmap.M12 = (float)m12b;
                                                somechunkmap.M13 = (float)m13b;
                                                somechunkmap.M14 = (float)m14b;

                                                somechunkmap.M21 = (float)m21b;
                                                somechunkmap.M22 = (float)m22b;
                                                somechunkmap.M23 = (float)m23b;
                                                somechunkmap.M24 = (float)m24b;

                                                somechunkmap.M31 = (float)m31b;
                                                somechunkmap.M32 = (float)m32b;
                                                somechunkmap.M33 = (float)m33b;
                                                somechunkmap.M34 = (float)m34b;

                                                somechunkmap.M41 = (float)m41b;
                                                somechunkmap.M42 = (float)m42b;
                                                somechunkmap.M43 = (float)m43b;
                                                somechunkmap.M44 = (float)m44b;

                                                sccslevelgen.arraychunkdatalod0[facetype][someseccounter].instanceintmapfirstvertexb = new scinstancevertdimensions()
                                                {
                                                    instancematrix = somechunkmap,
                                                };
                                            }

                                            somenewcounter++;
                                            someseccounter++;
                                        }
                                    }
                                }

                                //Console.WriteLine("counter:"+somecounter);
                            }
                        }
                    }
                }
            }

            ////Console.WriteLine("somenewcounter:" + somenewcounter);












            Console.WriteLine("generated vertices for face dimensions and locations " + facetype);































            /*
            //unLOADING CHUNK to XML
            //unLOADING CHUNK to XML
            string pathofrelease = Directory.GetCurrentDirectory();
            ////Console.WriteLine(pathofrelease);
            string pathofchunkmap = pathofrelease + @"\chunkmaps\";

            if (!Directory.Exists(pathofchunkmap))
            {
                ////Console.WriteLine("created directory");
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
                    //    //Console.WriteLine(ia[by]);
                    //}
                }
            }*/
            //LOADING CHUNK BACK INTO MEMORY
            //LOADING CHUNK BACK INTO MEMORY





            /*
            float x0 = (float)(Math.Round(10.1f / 10.0f) * 10.0f);
            float y0 = (float)(Math.Round(10.1f / 10.0f) * 10.0f);
            float z0 = (float)(Math.Round(10.1f / 10.0f) * 10.0f);


            //Console.WriteLine(x0);*/

            //staticContantBuffer = new Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);
            //dynamicConstantBuffer = new Buffer(device, Utilities.SizeOf<Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);



            someseccounter_ = someseccounter;

        }





        public void createinstances(int facetype, int sometiercounter, out int sometiercounter_)
        {



            //each type of map can have an identical number of vertex. and we need to spawn a certain number of instances per mesh.

            //maybe calculate the different number of instances needed?

            //now the goal is to spawn a specific number of instances for the number of levelgen tiles. so we need to calculate the number of vertex per tiles and assign those
            //to the corresponding instances that has those number of vertices.








            int typeofmeshes = somelevelgenprimglobals.widthlod0 * somelevelgenprimglobals.heightlod0 * somelevelgenprimglobals.depthlod0;
            var somelisttop = arrayofchunkvertsinst.OrderBy(v => v.Value);

            int somenewcounter = 0;

            int vertexcounter = 0;
            int lastvertexcounter = 0;

            /*List<scinstanceintmaps> arrayoffacemesh[(lastvertexcounter / 4) - 1].mapints = new List<scinstanceintmaps>();

            List<scinstancevertdimensions> arrayoffacemesh[(lastvertexcounter / 4) - 1].dimensionsmapsa = new List<scinstancevertdimensions>();
            List<scinstancevertdimensions> arrayoffacemesh[(lastvertexcounter / 4) - 1].dimensionsmapsb = new List<scinstancevertdimensions>();

            List<scinstancevertdimensions> arrayoffacemesh[(lastvertexcounter / 4) - 1].firstvertloca = new List<scinstancevertdimensions>();
            List<scinstancevertdimensions> arrayoffacemesh[(lastvertexcounter / 4) - 1].firstvertlocb = new List<scinstancevertdimensions>();
            List<instancetype> arrayoffacemesh[(lastvertexcounter / 4) - 1].instancetypelist = new List<instancetype>();*/

            int counterfacemeshtodraw = 0;


            lastvertexcounter = 0;
            somenewcounter = 0;

            var enumerator = somelisttop.GetEnumerator();

            int someix = 0;
            int someiy = 0;
            int someiz = 0;

            int someenumcounter = 0;

            int somevertcounter = 0;

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                var keyindex = current.Key;
                var value = current.Value;

                if (somenewcounter == 0)
                {

                    lastvertexcounter = sccslevelgen.arraychunkdatalod0[facetype][keyindex].vertexcount;// arraychunkvertslod0.arrayofvertstop.Length;

                    /*if (facetype == 0)
                    {
                        lastvertexcounter = arraychunkdatalod0[keyindex].arraychunkvertslod0.arrayofvertstop.Length;
                    }
                    else if (facetype == 1)
                    {
                        lastvertexcounter = arraychunkdatalod0[keyindex].arraychunkvertslod0.arrayofvertsleft.Length;
                    }
                    else if (facetype == 2)
                    {
                        lastvertexcounter = arraychunkdatalod0[keyindex].arraychunkvertslod0.arrayofvertsright.Length;
                    }
                    else if (facetype == 3)
                    {
                        lastvertexcounter = arraychunkdatalod0[keyindex].arraychunkvertslod0.arrayofvertsfront.Length;
                    }
                    else if (facetype == 4)
                    {
                        lastvertexcounter = arraychunkdatalod0[keyindex].arraychunkvertslod0.arrayofvertsback.Length;
                    }
                    else if (facetype == 5)
                    {
                        lastvertexcounter = arraychunkdatalod0[keyindex].arraychunkvertslod0.arrayofvertsbottom.Length;
                    }*/
                    //lastkeyindex = keyindex;
                    somenewcounter = 1;
                }





                vertexcounter = sccslevelgen.arraychunkdatalod0[facetype][keyindex].vertexcount;//.arraychunkvertslod0.arrayofvertstop.Length;

                /*
                if (facetype == 0)
                {
                    vertexcounter = arraychunkdatalod0[keyindex].arraychunkvertslod0.arrayofvertstop.Length;
                    
                }
                else if (facetype == 1)
                {

                    vertexcounter = arraychunkdatalod0[keyindex].arraychunkvertslod0.arrayofvertsleft.Length;
                    ////Console.WriteLine(vertexcounter);
                }
                else if (facetype == 2)
                {
                    vertexcounter = arraychunkdatalod0[keyindex].arraychunkvertslod0.arrayofvertsright.Length;
                }
                else if (facetype == 3)
                {
                    vertexcounter = arraychunkdatalod0[keyindex].arraychunkvertslod0.arrayofvertsfront.Length;
                }
                else if (facetype == 4)
                {
                    vertexcounter = arraychunkdatalod0[keyindex].arraychunkvertslod0.arrayofvertsback.Length;
                }
                else if (facetype == 5)
                {
                    vertexcounter = arraychunkdatalod0[keyindex].arraychunkvertslod0.arrayofvertsbottom.Length;
                }*/









                if (vertexcounter == lastvertexcounter) // && someenumcounter != arrayofchunkvertsinst.Count - 1
                {
                    arrayoffacemesh[(lastvertexcounter / 4) - 1].instancetypelist.Add(keyindex, new instancetype()
                    {
                        instancePos = new Vector4(sccslevelgen.arraychunkdatalod0[facetype][keyindex].realpos[0], sccslevelgen.arraychunkdatalod0[facetype][keyindex].realpos[1], sccslevelgen.arraychunkdatalod0[facetype][keyindex].realpos[2], 1.0f)
                    });

                    //positionsofinstances.Add(new Vector4(sccslevelgen.arraychunkdatalod0[facetype][keyindex].realpos[0], sccslevelgen.arraychunkdatalod0[facetype][keyindex].realpos[1], sccslevelgen.arraychunkdatalod0[facetype][keyindex].realpos[2], 1.0f));
                    sccslevelgen.arraychunkdatalod0[facetype][keyindex].indexinmainarray = arrayoffacemesh[(lastvertexcounter / 4) - 1].mapints.Count;

                    //sccslevelgen.arraychunkdatalod0[facetype][keyindex].arraychunkvertslod0.indexinmainarray = arrayoffacemesh[(lastvertexcounter / 4) - 1].mapints.Count;



                    //sccslevelgen.arraychunkdatalod0[facetype][keyindex].mapints = sccslevelgen.arraychunkdatalod0[facetype][keyindex].instanceintmap;

                    /*sccslevelgen.arraychunkdatalod0[facetype][keyindex].instancetypelist = new instancetype()
                    {
                        instancePos = new Vector4(sccslevelgen.arraychunkdatalod0[facetype][keyindex].realpos[0], sccslevelgen.arraychunkdatalod0[facetype][keyindex].realpos[1], sccslevelgen.arraychunkdatalod0[facetype][keyindex].realpos[2], 1.0f)
                    };*/
                    //sccslevelgen.arraychunkdatalod0[facetype][keyindex].dimensionsmapsa = sccslevelgen.arraychunkdatalod0[facetype][keyindex].instancesmatrixa;
                    //////sccslevelgen.arraychunkdatalod0[facetype][keyindex].dimensionsmapsb = sccslevelgen.arraychunkdatalod0[facetype][keyindex].instancesmatrixb;
                    //sccslevelgen.arraychunkdatalod0[facetype][keyindex].firstvertloca = sccslevelgen.arraychunkdatalod0[facetype][keyindex].instanceintmapfirstvertexa;
                    ////.arraychunkdatalod0[facetype][keyindex].firstvertlocb = sccslevelgen.arraychunkdatalod0[facetype][keyindex].instanceintmapfirstvertexb;
                    //sccslevelgen.arraychunkdatalod0[facetype][keyindex].arraychunkvertslod0.indexinmainarray = arrayoffacemesh[(lastvertexcounter / 4) - 1].mapints.Count;



                    arrayoffacemesh[(lastvertexcounter / 4) - 1].mapints.Add(keyindex, sccslevelgen.arraychunkdatalod0[facetype][keyindex].instanceintmap);

                    arrayoffacemesh[(lastvertexcounter / 4) - 1].dimensionsmapsa.Add(keyindex, sccslevelgen.arraychunkdatalod0[facetype][keyindex].instancesmatrixa);
                    arrayoffacemesh[(lastvertexcounter / 4) - 1].dimensionsmapsb.Add(keyindex, sccslevelgen.arraychunkdatalod0[facetype][keyindex].instancesmatrixb);
                    arrayoffacemesh[(lastvertexcounter / 4) - 1].firstvertloca.Add(keyindex, sccslevelgen.arraychunkdatalod0[facetype][keyindex].instanceintmapfirstvertexa);
                    arrayoffacemesh[(lastvertexcounter / 4) - 1].firstvertlocb.Add(keyindex, sccslevelgen.arraychunkdatalod0[facetype][keyindex].instanceintmapfirstvertexb);


                    sccslevelgen.arraychunkdatalod0[facetype][keyindex].instanceintmap = new scinstanceintmaps()
                    {

                    };

                    sccslevelgen.arraychunkdatalod0[facetype][keyindex].instancesmatrixa = new scinstancevertdimensions()
                    {

                    };
                    sccslevelgen.arraychunkdatalod0[facetype][keyindex].instancesmatrixb = new scinstancevertdimensions()
                    {

                    };

                    sccslevelgen.arraychunkdatalod0[facetype][keyindex].instanceintmapfirstvertexa = new scinstancevertdimensions()
                    {

                    };
                    sccslevelgen.arraychunkdatalod0[facetype][keyindex].instanceintmapfirstvertexb = new scinstancevertdimensions()
                    {

                    };







                }
                else if (vertexcounter != lastvertexcounter) // || someenumcounter == arrayofchunkvertsinst.Count - 1  && someenumcounter != arrayofchunkvertsinst.Count - 1
                {
                    arrayoffacemesh[(lastvertexcounter / 4) - 1].createinstances(device, -1);
                    somefacemeshlisttodraw.Add(arrayoffacemesh[(lastvertexcounter / 4) - 1]);

                    arrayoffacemesh[(vertexcounter / 4) - 1].instancetypelist.Add(keyindex, new instancetype()
                    {
                        instancePos = new Vector4(sccslevelgen.arraychunkdatalod0[facetype][keyindex].realpos[0], sccslevelgen.arraychunkdatalod0[facetype][keyindex].realpos[1], sccslevelgen.arraychunkdatalod0[facetype][keyindex].realpos[2], 1.0f)
                    });

                    //positionsofinstances.Add(new Vector4(sccslevelgen.arraychunkdatalod0[facetype][keyindex].realpos[0], sccslevelgen.arraychunkdatalod0[facetype][keyindex].realpos[1], sccslevelgen.arraychunkdatalod0[facetype][keyindex].realpos[2], 1.0f));
                    sccslevelgen.arraychunkdatalod0[facetype][keyindex].indexinmainarray = arrayoffacemesh[(vertexcounter / 4) - 1].mapints.Count;


                    //sccslevelgen.arraychunkdatalod0[facetype][keyindex].arraychunkvertslod0.indexinmainarray = arrayoffacemesh[(vertexcounter / 4) - 1].mapints.Count;



                    //sccslevelgen.arraychunkdatalod0[facetype][keyindex].mapints = sccslevelgen.arraychunkdatalod0[facetype][keyindex].instanceintmap;
                    /*sccslevelgen.arraychunkdatalod0[facetype][keyindex].instancetypelist = new instancetype()
                    {
                        instancePos = new Vector4(sccslevelgen.arraychunkdatalod0[facetype][keyindex].realpos[0], sccslevelgen.arraychunkdatalod0[facetype][keyindex].realpos[1], sccslevelgen.arraychunkdatalod0[facetype][keyindex].realpos[2], 1.0f)
                    };*/

                    //sccslevelgen.arraychunkdatalod0[facetype][keyindex].dimensionsmapsa = sccslevelgen.arraychunkdatalod0[facetype][keyindex].instancesmatrixa;
                    //sccslevelgen.arraychunkdatalod0[facetype][keyindex].dimensionsmapsb = sccslevelgen.arraychunkdatalod0[facetype][keyindex].instancesmatrixb;
                    //sccslevelgen.arraychunkdatalod0[facetype][keyindex].firstvertloca = sccslevelgen.arraychunkdatalod0[facetype][keyindex].instanceintmapfirstvertexa;
                    //sccslevelgen.arraychunkdatalod0[facetype][keyindex].firstvertlocb = sccslevelgen.arraychunkdatalod0[facetype][keyindex].instanceintmapfirstvertexb;
                    //sccslevelgen.arraychunkdatalod0[facetype][keyindex].arraychunkvertslod0.indexinmainarray = arrayoffacemesh[(vertexcounter / 4) - 1].mapints.Count;



                    arrayoffacemesh[(vertexcounter / 4) - 1].mapints.Add(keyindex, sccslevelgen.arraychunkdatalod0[facetype][keyindex].instanceintmap);

                    arrayoffacemesh[(vertexcounter / 4) - 1].dimensionsmapsa.Add(keyindex, sccslevelgen.arraychunkdatalod0[facetype][keyindex].instancesmatrixa);
                    arrayoffacemesh[(vertexcounter / 4) - 1].dimensionsmapsb.Add(keyindex, sccslevelgen.arraychunkdatalod0[facetype][keyindex].instancesmatrixb);
                    arrayoffacemesh[(vertexcounter / 4) - 1].firstvertloca.Add(keyindex, sccslevelgen.arraychunkdatalod0[facetype][keyindex].instanceintmapfirstvertexa);
                    arrayoffacemesh[(vertexcounter / 4) - 1].firstvertlocb.Add(keyindex, sccslevelgen.arraychunkdatalod0[facetype][keyindex].instanceintmapfirstvertexb);
                    ////arrayoffacemesh[(vertexcounter / 4) - 1].listofindexes.Add(keyindex);
                    ///

                    sccslevelgen.arraychunkdatalod0[facetype][keyindex].instanceintmap = new scinstanceintmaps()
                    {

                    };

                    sccslevelgen.arraychunkdatalod0[facetype][keyindex].instancesmatrixa = new scinstancevertdimensions()
                    {

                    };
                    sccslevelgen.arraychunkdatalod0[facetype][keyindex].instancesmatrixb = new scinstancevertdimensions()
                    {

                    };

                    sccslevelgen.arraychunkdatalod0[facetype][keyindex].instanceintmapfirstvertexa = new scinstancevertdimensions()
                    {

                    };
                    sccslevelgen.arraychunkdatalod0[facetype][keyindex].instanceintmapfirstvertexb = new scinstancevertdimensions()
                    {

                    };


                }


                if (someenumcounter == arrayofchunkvertsinst.Count - 1)
                {

                    arrayoffacemesh[(vertexcounter / 4) - 1].createinstances(device, -1);
                    somefacemeshlisttodraw.Add(arrayoffacemesh[(vertexcounter / 4) - 1]);
                }




                //Console.WriteLine("vertexc:" + vertexcounter);
                lastvertexcounter = vertexcounter;




                someenumcounter++;
            }





            /*for (int i = 0;i < arrayoffacemesh.Length;i++)
            {
                //if (!somefacemeshlisttodraw.Contains(arrayoffacemesh[i]))
                {
                    somefacemeshlisttodraw.Add(arrayoffacemesh[i]);
                }
            }*/




            for (int i = 0; i < sccslevelgen.arraychunkdatalod0[facetype].Length; i++)
            {
                if (sccslevelgen.arraychunkdatalod0[facetype][i].arraychunkvertslod0 != null)
                {

                    sccslevelgen.arraychunkdatalod0[facetype][i].arraychunkvertslod0.vertexcountermemory = 0;
                    sccslevelgen.arraychunkdatalod0[facetype][i].arraychunkvertslod0.cleararrays();
                    //sccslevelgen.arraychunkdatalod0[facetype][i].arraychunkvertslod0 = null;

                    //GC.SuppressFinalize(sccslevelgen.arraychunkdatalod0[facetype][i].arraychunkvertslod0);
                    //arraychunkdatalod0[i] = null;
                }
            }
            //sccslevelgen.arraychunkdatalod0[facetype] = null;

            /*GC.Collect(0, GCCollectionMode.Forced, true, true);
            GC.Collect(1, GCCollectionMode.Forced, true, true);
            GC.Collect(2, GCCollectionMode.Forced, true, true);*/


            GC.Collect();

            arrayofchunkvertsinst.Clear();
            arrayofchunkvertsinst = null;

            somelisttop = null;//.Clear();
            enumerator = null;


            /*if (sccslevelgen.arraychunkdatalod0 != null)
            {
                for (int ii = 0; ii < sccslevelgen.arraychunkdatalod0.Length; ii++)
                {
                    if (sccslevelgen.arraychunkdatalod0[ii] != null)
                    {
                        for (int i = 0; i < sccslevelgen.arraychunkdatalod0[ii].Length; i++)
                        {
                            if (sccslevelgen.arraychunkdatalod0[ii][i].arraychunkvertslod0 != null)
                            {
                                //sccslevelgen.arraychunkdatalod0[facetype][i].arraychunkvertslod0.cleararrays();
                                sccslevelgen.arraychunkdatalod0[ii][i].vertexcount = 0;//.cleararrays();
                                
                            }
                        }

                        sccslevelgen.arraychunkdatalod0[ii] = null;
                    }
                }
            }
          */
            //sccslevelgen.arraychunkdatalod0 = null;



           sometiercounter_ = sometiercounter;
        }









        //, tutorialchunkcubemap chunkmap
        //int indexofthechunkininstanceslist, 
        public void removefromarray(int oldvertexcounter, int facetype, int indexofchunk)
        {

            //Console.WriteLine(oldvertexcounter);

            if (oldvertexcounter > 0)
            {
                arrayoffacemesh[(oldvertexcounter / 4) - 1].mapints.Remove(indexofchunk);
                arrayoffacemesh[(oldvertexcounter / 4) - 1].instancetypelist.Remove(indexofchunk);
                arrayoffacemesh[(oldvertexcounter / 4) - 1].dimensionsmapsa.Remove(indexofchunk);
                arrayoffacemesh[(oldvertexcounter / 4) - 1].dimensionsmapsb.Remove(indexofchunk);
                arrayoffacemesh[(oldvertexcounter / 4) - 1].firstvertloca.Remove(indexofchunk);
                arrayoffacemesh[(oldvertexcounter / 4) - 1].firstvertlocb.Remove(indexofchunk);


                if (arrayoffacemesh[(oldvertexcounter / 4) - 1].instancetypelist.Count > 0)
                {
                    arrayoffacemesh[(oldvertexcounter / 4) - 1].createinstances(device, -1);
                    arrayoffacemesh[(oldvertexcounter / 4) - 1].writetobufferswtc = 0;
                }
                else
                {
                    somefacemeshlisttodraw.Remove(arrayoffacemesh[(oldvertexcounter / 4) - 1]);
                }
                
            }
           
        }

        





        /*
        List<scinstanceintmaps> arrayoffacemesh[(lastvertexcounter / 4) - 1].mapints = new List<scinstanceintmaps>();

        List<scinstancevertdimensions> arrayoffacemesh[(lastvertexcounter / 4) - 1].dimensionsmapsa = new List<scinstancevertdimensions>();
        List<scinstancevertdimensions> arrayoffacemesh[(lastvertexcounter / 4) - 1].dimensionsmapsb = new List<scinstancevertdimensions>();

        List<scinstancevertdimensions> arrayoffacemesh[(lastvertexcounter / 4) - 1].firstvertloca = new List<scinstancevertdimensions>();
        List<scinstancevertdimensions> arrayoffacemesh[(lastvertexcounter / 4) - 1].firstvertlocb = new List<scinstancevertdimensions>();
        List<instancetype> arrayoffacemesh[(lastvertexcounter / 4) - 1].instancetypelist = new List<instancetype>();*/


        /*
        public void setcorrectindexes(int indexofchunk, int indexofthechunkininstanceslist, int oldvertexcount)
        {
            //for everything higher in the list, than the chunk that is being removed, set the correct index location.

            //arraychunkdatalod0[indexofchunk]


            for (int i = 0; i < arrayoffacemesh[(oldvertexcount / 4) - 1].listofindexes.Count;i++) //indexofthechunkininstanceslist
            {
                //Console.WriteLine("oriindex:" + arraychunkdatalod0[arrayoffacemesh[(oldvertexcount / 4) - 1].listofindexes[i]].arraychunkvertslod0.indexinmainarray + "/newindex:" + i);
                //arrayoffacemesh[(vertexcounter / 4) - 1].index
                //arraychunkdatalod0[arrayoffacemesh[(oldvertexcount / 4) - 1].listofindexes[i]].indexinmainarray = i;
                arraychunkdatalod0[arrayoffacemesh[(oldvertexcount / 4) - 1].listofindexes[i]].arraychunkvertslod0.indexinmainarray = i;

            }

            //arrayoffacemesh[(vertexcounter / 4) - 1].listofindexes.RemoveAt(indexofchunk);
            
            arrayoffacemesh[(oldvertexcount / 4) - 1].createinstances(device, -1);
            arrayoffacemesh[(oldvertexcount / 4) - 1].writetobufferswtc = 0;

            /*if (arrayoffacemesh[(oldvertexcount / 4) - 1].mapints.Count > 0)
            {
                
            }
        }*/








        
        public void findinstancemeshtoinsertinto(int indexofchunk, int vertexcounter, int facetype)
        {




            //int lastvertexcounter = vertexcounter;
            //int vertexcounter = thevertexcounter;
            int somevertcounter = 0;






            arrayoffacemesh[(vertexcounter / 4) - 1].instancetypelist.Remove(indexofchunk);
            //if (!arrayoffacemesh[(vertexcounter / 4) - 1].instancetypelist.ContainsKey(indexofchunk))
            {

                arrayoffacemesh[(vertexcounter / 4) - 1].instancetypelist.Add(indexofchunk, new instancetype()
                {
                    instancePos = new Vector4(sccslevelgen.arraychunkdatalod0[facetype][indexofchunk].realpos[0], sccslevelgen.arraychunkdatalod0[facetype][indexofchunk].realpos[1], sccslevelgen.arraychunkdatalod0[facetype][indexofchunk].realpos[2], 1.0f)
                });
            }


            //positionsofinstances.Add(new Vector4(sccslevelgen.arraychunkdatalod0[facetype][indexofchunk].realpos[0], sccslevelgen.arraychunkdatalod0[facetype][indexofchunk].realpos[1], sccslevelgen.arraychunkdatalod0[facetype][indexofchunk].realpos[2], 1.0f));

            arrayoffacemesh[(vertexcounter / 4) - 1].mapints.Remove(indexofchunk);
            //if (!arrayoffacemesh[(vertexcounter / 4) - 1].mapints.ContainsKey(indexofchunk))
            {
                arrayoffacemesh[(vertexcounter / 4) - 1].mapints.Add(indexofchunk, sccslevelgen.arraychunkdatalod0[facetype][indexofchunk].instanceintmap);
            }

            arrayoffacemesh[(vertexcounter / 4) - 1].dimensionsmapsa.Remove(indexofchunk);
            //if (!arrayoffacemesh[(vertexcounter / 4) - 1].dimensionsmapsa.ContainsKey(indexofchunk))
            {
                arrayoffacemesh[(vertexcounter / 4) - 1].dimensionsmapsa.Add(indexofchunk, sccslevelgen.arraychunkdatalod0[facetype][indexofchunk].instancesmatrixa);
            }

            arrayoffacemesh[(vertexcounter / 4) - 1].dimensionsmapsb.Remove(indexofchunk);
            //if (!arrayoffacemesh[(vertexcounter / 4) - 1].dimensionsmapsb.ContainsKey(indexofchunk))
            {
                arrayoffacemesh[(vertexcounter / 4) - 1].dimensionsmapsb.Add(indexofchunk, sccslevelgen.arraychunkdatalod0[facetype][indexofchunk].instancesmatrixb);
            }

            arrayoffacemesh[(vertexcounter / 4) - 1].firstvertloca.Remove(indexofchunk);
            //if (!arrayoffacemesh[(vertexcounter / 4) - 1].firstvertloca.ContainsKey(indexofchunk))
            {
                arrayoffacemesh[(vertexcounter / 4) - 1].firstvertloca.Add(indexofchunk, sccslevelgen.arraychunkdatalod0[facetype][indexofchunk].instanceintmapfirstvertexa);
            }

            arrayoffacemesh[(vertexcounter / 4) - 1].firstvertlocb.Remove(indexofchunk);
            //if (!arrayoffacemesh[(vertexcounter / 4) - 1].firstvertlocb.ContainsKey(indexofchunk))
            {
                arrayoffacemesh[(vertexcounter / 4) - 1].firstvertlocb.Add(indexofchunk, sccslevelgen.arraychunkdatalod0[facetype][indexofchunk].instanceintmapfirstvertexb);
            }







            //arrayoffacemesh[(vertexcounter / 4) - 1].listofindexes.Add(indexofchunk);
            //if (arrayoffacemesh[(vertexcounter / 4) - 1].instances != null)
            {
                //if (arrayoffacemesh[(vertexcounter / 4) - 1].instancetypelist.Count > 0)
                {
                    arrayoffacemesh[(vertexcounter / 4) - 1].createinstances(device, -1);
                    arrayoffacemesh[(vertexcounter / 4) - 1].writetobufferswtc = 0;
                }
            }

            if (!somefacemeshlisttodraw.Contains(arrayoffacemesh[(vertexcounter / 4) - 1]))
            {
                somefacemeshlisttodraw.Add(arrayoffacemesh[(vertexcounter / 4) - 1]);
            }

        }

        






        

        public int insertdatainbufferstructs(int indexofchunk, int facetype, int indexoffirstchunkinbundle)
        {

            int vertexlength = 0;

            int typeofterraintile = sccslevelgen.arraychunkdatalod0[facetype][indexofchunk].typeofterraintile;

            int cango = 1;

      




            ////Console.WriteLine(arraychunkdatalod0[indexofchunk].arraychunkvertslod0.chunkPos);
            if (cango == 1)
            {
                //arrayofchunkvertsinst.Add(indexofchunk, arraychunkdatalod0[indexofchunk].arraychunkvertslod0.arrayofvertstop.Length);



                vertexlength = sccslevelgen.arraychunkdatalod0[facetype][indexofchunk].vertexcount;// arraychunkvertslod0.arrayofvertstop.Length;//


                /*if (facetype == 0)
                {
                    vertexlength = arraychunkdatalod0[facetype][indexofchunk].arraychunkvertslod0.arrayofvertstop.Length;// arrayofchunkvertsinst.Add(indexofchunk, arraychunkdatalod0[facetype][indexofchunk].arraychunkvertslod0.arrayofvertstop.Length);
                }
                else if (facetype == 1)
                {
                    vertexlength = arraychunkdatalod0[facetype][indexofchunk].arraychunkvertslod0.arrayofvertsleft.Length;//arrayofchunkvertsinst.Add(indexofchunk, arraychunkdatalod0[facetype][indexofchunk].arraychunkvertslod0.arrayofvertsleft.Length);
                }
                else if (facetype == 2)
                {
                    vertexlength = arraychunkdatalod0[facetype][indexofchunk].arraychunkvertslod0.arrayofvertsright.Length;// arrayofchunkvertsinst.Add(indexofchunk, arraychunkdatalod0[facetype][indexofchunk].arraychunkvertslod0.arrayofvertsright.Length);
                }
                else if (facetype == 3)
                {
                    vertexlength = arraychunkdatalod0[facetype][indexofchunk].arraychunkvertslod0.arrayofvertsfront.Length;//arrayofchunkvertsinst.Add(indexofchunk, arraychunkdatalod0[facetype][indexofchunk].arraychunkvertslod0.arrayofvertsfront.Length);
                }
                else if (facetype == 4)
                {
                    vertexlength = arraychunkdatalod0[facetype][indexofchunk].arraychunkvertslod0.arrayofvertsback.Length;//arrayofchunkvertsinst.Add(indexofchunk, arraychunkdatalod0[facetype][indexofchunk].arraychunkvertslod0.arrayofvertsback.Length);
                }
                else if (facetype == 5)
                {
                    vertexlength = arraychunkdatalod0[facetype][indexofchunk].arraychunkvertslod0.arrayofvertsbottom.Length;// arrayofchunkvertsinst.Add(indexofchunk, arraychunkdatalod0[facetype][indexofchunk].arraychunkvertslod0.arrayofvertsbottom.Length);
                }

                //arrayofchunkvertsinstright.Add(indexofchunk, arraychunkdatalod0[facetype][indexofchunk].arraychunkvertslod0.arrayoftrigsright.Length);
                */

                int thefirstbundlechunkindex = 0;
                //if (vertexlength > 0)
                {
                    for (int xi = 0; xi < 2; xi++)
                    {
                        for (int yi = 0; yi < 2; yi++)
                        {
                            for (int zi = 0; zi < 2; zi++)
                            {
                                /*
                                if (xi == 0 && yi == 0 && zi == 0)
                                {
                                    thefirstbundlechunkindex = indexofchunk;// somenewcounter;

                                    sccslevelgen.arraychunkdatalod0[0][thefirstbundlechunkindex].arraychunkvertslod0.startBuildingArray(facetype, this, 1, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                }
                                else
                                {
                                    sccslevelgen.arraychunkdatalod0[0][thefirstbundlechunkindex].arraychunkvertslod0.newregenerate(facetype, xi, yi, zi, xi + 1, yi + 1, zi + 1);

                                }
                                */




                                double m11a = 0;
                                double m12a = 0;
                                double m13a = 0;
                                double m14a = 0;
                                double m21a = 0;
                                double m22a = 0;
                                double m23a = 0;
                                double m24a = 0;
                                double m31a = 0;
                                double m32a = 0;
                                double m33a = 0;
                                double m34a = 0;
                                double m41a = 0;
                                double m42a = 0;
                                double m43a = 0;
                                double m44a = 0;

                                double m11b = 0;
                                double m12b = 0;
                                double m13b = 0;
                                double m14b = 0;
                                double m21b = 0;
                                double m22b = 0;
                                double m23b = 0;
                                double m24b = 0;
                                double m31b = 0;
                                double m32b = 0;
                                double m33b = 0;
                                double m34b = 0;
                                double m41b = 0;
                                double m42b = 0;
                                double m43b = 0;
                                double m44b = 0;






                                sccslevelgen.arraychunkdatalod0[0][indexoffirstchunkinbundle].arraychunkvertslod0.insertdimensionsinint(xi, yi, zi, xi + 1, yi + 1, zi + 1, typeofterraintile, this, 1, 6, facetype,
                                out m11a, out m12a, out m13a, out m14a, out m21a, out m22a, out m23a, out m24a, out m31a, out m32a, out m33a, out m34a, out m41a, out m42a, out m43a, out m44a,
                                out m11b, out m12b, out m13b, out m14b, out m21b, out m22b, out m23b, out m24b, out m31b, out m32b, out m33b, out m34b, out m41b, out m42b, out m43b, out m44b); //, somechunkkeyboardpriminstanceindex_, chunkprimindex_, chunkinstindex



                                Matrix somechunkmap = Matrix.Identity;

                                somechunkmap.M11 = (float)m11a;
                                somechunkmap.M12 = (float)m12a;
                                somechunkmap.M13 = (float)m13a;
                                somechunkmap.M14 = (float)m14a;

                                somechunkmap.M21 = (float)m21a;
                                somechunkmap.M22 = (float)m22a;
                                somechunkmap.M23 = (float)m23a;
                                somechunkmap.M24 = (float)m24a;

                                somechunkmap.M31 = (float)m31a;
                                somechunkmap.M32 = (float)m32a;
                                somechunkmap.M33 = (float)m33a;
                                somechunkmap.M34 = (float)m34a;

                                somechunkmap.M41 = (float)m41a;
                                somechunkmap.M42 = (float)m42a;
                                somechunkmap.M43 = (float)m43a;
                                somechunkmap.M44 = (float)m44a;

                                sccslevelgen.arraychunkdatalod0[facetype][indexofchunk].instancesmatrixa = new scinstancevertdimensions()
                                {
                                    instancematrix = somechunkmap,
                                };

                                somechunkmap = Matrix.Identity;

                                somechunkmap.M11 = (float)m11b;
                                somechunkmap.M12 = (float)m12b;
                                somechunkmap.M13 = (float)m13b;
                                somechunkmap.M14 = (float)m14b;

                                somechunkmap.M21 = (float)m21b;
                                somechunkmap.M22 = (float)m22b;
                                somechunkmap.M23 = (float)m23b;
                                somechunkmap.M24 = (float)m24b;

                                somechunkmap.M31 = (float)m31b;
                                somechunkmap.M32 = (float)m32b;
                                somechunkmap.M33 = (float)m33b;
                                somechunkmap.M34 = (float)m34b;

                                somechunkmap.M41 = (float)m41b;
                                somechunkmap.M42 = (float)m42b;
                                somechunkmap.M43 = (float)m43b;
                                somechunkmap.M44 = (float)m44b;

                                sccslevelgen.arraychunkdatalod0[facetype][indexofchunk].instancesmatrixb = new scinstancevertdimensions()
                                {
                                    instancematrix = somechunkmap,
                                };

                                m11a = 0;
                                m12a = 0;
                                m13a = 0;
                                m14a = 0;
                                m21a = 0;
                                m22a = 0;
                                m23a = 0;
                                m24a = 0;
                                m31a = 0;
                                m32a = 0;
                                m33a = 0;
                                m34a = 0;
                                m41a = 0;
                                m42a = 0;
                                m43a = 0;
                                m44a = 0;
                                /*

                                sccslevelgen.arraychunkdatalod0[0][indexofchunk].arraychunkvertslod0.setmapforchunks(out m11a, out m12a, out m13a, out m14a, out m21a, out m22a, out m23a, out m24a, out m31a, out m32a, out m33a, out m34a, out m41a, out m42a, out m43a, out m44a);


                                somechunkmap = Matrix.Identity;

                                somechunkmap.M11 = (float)m11a;
                                somechunkmap.M12 = (float)m12a;
                                somechunkmap.M13 = (float)m13a;
                                somechunkmap.M14 = (float)m14a;

                                somechunkmap.M21 = (float)m21a;
                                somechunkmap.M22 = (float)m22a;
                                somechunkmap.M23 = (float)m23a;
                                somechunkmap.M24 = (float)m24a;

                                somechunkmap.M31 = (float)m31a;
                                somechunkmap.M32 = (float)m32a;
                                somechunkmap.M33 = (float)m33a;
                                somechunkmap.M34 = (float)m34a;

                                somechunkmap.M41 = (float)m41a;
                                somechunkmap.M42 = (float)m42a;
                                somechunkmap.M43 = (float)m43a;
                                somechunkmap.M44 = (float)m44a;

                                sccslevelgen.arraychunkdatalod0[facetype][indexofchunk].instanceintmap = new scinstanceintmaps()
                                {
                                    instanceintmap = somechunkmap,
                                };
                                */

                                m11a = 0;
                                m12a = 0;
                                m13a = 0;
                                m14a = 0;
                                m21a = 0;
                                m22a = 0;
                                m23a = 0;
                                m24a = 0;
                                m31a = 0;
                                m32a = 0;
                                m33a = 0;
                                m34a = 0;
                                m41a = 0;
                                m42a = 0;
                                m43a = 0;
                                m44a = 0;

                                m11b = 0;
                                m12b = 0;
                                m13b = 0;
                                m14b = 0;
                                m21b = 0;
                                m22b = 0;
                                m23b = 0;
                                m24b = 0;
                                m31b = 0;
                                m32b = 0;
                                m33b = 0;
                                m34b = 0;
                                m41b = 0;
                                m42b = 0;
                                m43b = 0;
                                m44b = 0;

                                sccslevelgen.arraychunkdatalod0[0][indexoffirstchunkinbundle].arraychunkvertslod0.setmapforfirstverts(xi, yi, zi, xi + 1, yi + 1, zi + 1, typeofterraintile, this, 1, 6, facetype,
                                out m11a, out m12a, out m13a, out m14a, out m21a, out m22a, out m23a, out m24a, out m31a, out m32a, out m33a, out m34a, out m41a, out m42a, out m43a, out m44a,
                                out m11b, out m12b, out m13b, out m14b, out m21b, out m22b, out m23b, out m24b, out m31b, out m32b, out m33b, out m34b, out m41b, out m42b, out m43b, out m44b); //, somechunkkeyboardpriminstanceindex_, chunkprimindex_, chunkinstindex

                                somechunkmap = Matrix.Identity;

                                somechunkmap.M11 = (float)m11a;
                                somechunkmap.M12 = (float)m12a;
                                somechunkmap.M13 = (float)m13a;
                                somechunkmap.M14 = (float)m14a;

                                somechunkmap.M21 = (float)m21a;
                                somechunkmap.M22 = (float)m22a;
                                somechunkmap.M23 = (float)m23a;
                                somechunkmap.M24 = (float)m24a;

                                somechunkmap.M31 = (float)m31a;
                                somechunkmap.M32 = (float)m32a;
                                somechunkmap.M33 = (float)m33a;
                                somechunkmap.M34 = (float)m34a;

                                somechunkmap.M41 = (float)m41a;
                                somechunkmap.M42 = (float)m42a;
                                somechunkmap.M43 = (float)m43a;
                                somechunkmap.M44 = (float)m44a;

                                sccslevelgen.arraychunkdatalod0[facetype][indexofchunk].instanceintmapfirstvertexa = new scinstancevertdimensions()
                                {
                                    instancematrix = somechunkmap,
                                };

                                somechunkmap = Matrix.Identity;

                                somechunkmap.M11 = (float)m11b;
                                somechunkmap.M12 = (float)m12b;
                                somechunkmap.M13 = (float)m13b;
                                somechunkmap.M14 = (float)m14b;

                                somechunkmap.M21 = (float)m21b;
                                somechunkmap.M22 = (float)m22b;
                                somechunkmap.M23 = (float)m23b;
                                somechunkmap.M24 = (float)m24b;

                                somechunkmap.M31 = (float)m31b;
                                somechunkmap.M32 = (float)m32b;
                                somechunkmap.M33 = (float)m33b;
                                somechunkmap.M34 = (float)m34b;

                                somechunkmap.M41 = (float)m41b;
                                somechunkmap.M42 = (float)m42b;
                                somechunkmap.M43 = (float)m43b;
                                somechunkmap.M44 = (float)m44b;

                                sccslevelgen.arraychunkdatalod0[facetype][indexofchunk].instanceintmapfirstvertexb = new scinstancevertdimensions()
                                {
                                    instancematrix = somechunkmap,
                                };
                            }
                        }
                    }
                }
            }

            return vertexlength;
        }












        public struct scinstanceintmaps
        {
            public Matrix instanceintmap;
        };


        public struct scinstancevertdimensions
        {
            public Matrix instancematrix;
            public Matrix instancematrixb;
            public Matrix instancematrixc;
            public Matrix instancematrixd;
        };

        public struct chunkdata
        {


            /*public tutorialcubeaschunkinst.instancetype instancetypelist;//=  tutorialcubeaschunkinst.instancetype>();

            public tutorialcubeaschunkinst.scinstanceintmaps mapints;//= tutorialcubeaschunkinst.scinstanceintmaps>();

            public tutorialcubeaschunkinst.scinstancevertdimensions dimensionsmapsa;// = tutorialcubeaschunkinst.scinstancevertdimensions;
            public tutorialcubeaschunkinst.scinstancevertdimensions dimensionsmapsb;//= tutorialcubeaschunkinst.scinstancevertdimensions;

            public tutorialcubeaschunkinst.scinstancevertdimensions firstvertloca;// =  tutorialcubeaschunkinst.scinstancevertdimensions;
            public tutorialcubeaschunkinst.scinstancevertdimensions firstvertlocb;// =  tutorialcubeaschunkinst.scinstancevertdimensions;
            */

            public int vertexcount;

            /*public SharpDX.Direct3D11.Buffer indicesbuffer;
            public SharpDX.Direct3D11.Buffer verticesbuffer;
            public SharpDX.Direct3D11.Buffer staticContantBuffer;
            public SharpDX.Direct3D11.Buffer dynamicConstantBuffer;*/
            //public sclevelgenmaps arraychunkmapslod0;
            public tutorialchunkcubemap arraychunkvertslod0;

            //public sclevelgenmaps arraychunkmapslod1;
            //public sclevelgenvert arraychunkvertslod1;

            //public sclevelgenmaps arraychunkmapslod2;
            //public sclevelgenvert arraychunkvertslod2;

            //public sclevelgenmaps arraychunkmapslod3;
            //public sclevelgenvert arraychunkvertslod3;

            //public sclevelgenmaps arraychunkmapslod4;
            //public sclevelgenvert arraychunkvertslod4;
            //public Vector4[] arrayofverts;
            //public int[] arrayoftrigs;
            /*public PixelShader pixelShader;
            public VertexShader vertexShader;
            public InputLayout layout;*/

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




            public scinstancevertdimensions instanceintmapfirstvertexa;
            //public SharpDX.Direct3D11.Buffer instanceintmapbufferfirstvertexa;

            public scinstancevertdimensions instanceintmapfirstvertexb;
            //public SharpDX.Direct3D11.Buffer instanceintmapbufferfirstvertexb;

            /*public scinstancevertdimensions instanceintmapfirstvertexc;
            //public SharpDX.Direct3D11.Buffer instanceintmapbufferfirstvertexc;

            public scinstancevertdimensions instanceintmapfirstvertexd;
            //public SharpDX.Direct3D11.Buffer instanceintmapbufferfirstvertexd;
            */





            public scinstanceintmaps instanceintmap;
            //public SharpDX.Direct3D11.Buffer instanceintmapbuffer;

            public scinstancevertdimensions instancesmatrixa;
            //public SharpDX.Direct3D11.Buffer instancesmatrixbuffera;

            public scinstancevertdimensions instancesmatrixb;
            //public SharpDX.Direct3D11.Buffer instancesmatrixbufferb;

            /*public scinstancevertdimensions instancesmatrixc;
            //public SharpDX.Direct3D11.Buffer instancesmatrixbufferc;

            public scinstancevertdimensions instancesmatrixd;
            //public SharpDX.Direct3D11.Buffer instancesmatrixbufferd;*/

            public int indexinmainarray;

            //public SharpDX.Direct3D11.Buffer ConstantLightBuffer;
        }



        /*
        public tutorialchunkcubemap getchunkinlevelgenmap(int x, int y, int z, int levelofdetail, out int indexinarray)
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




                    if (sccslevelgen.arraychunkdatalod0[facetype][sccslevelgen.arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod0 != null)
                    {
                        if (levelofdetail == 1)
                        {
                            indexinarray = sccslevelgen.arrayofindexes[indexinsclevelgenmap];
                            return sccslevelgen.arraychunkdatalod0[facetype][sccslevelgen.arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod0;
                        }
                        /*else if (levelofdetail == 2)
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
            indexinarray = -1;
            return null;
        }*/













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
            ////Console.WriteLine(y);

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

                                ////Console.WriteLine("same index");
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
                        //Console.WriteLine("null chunk");
                    }
                }
            }
            else
            {
                return null;
                //Console.WriteLine("Out of Range. The chunk is a border tile");
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
        ////Console.WriteLine();
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
        ////Console.WriteLine(y);

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

                            ////Console.WriteLine("same index");
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
            //Console.WriteLine("Out of Range. The chunk is a border tile");
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
        ////Console.WriteLine();
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













        ~tutorialcubeaschunkinst()
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
                /*if (staticContantBuffer != null)
                {
                    staticContantBuffer.Dispose();
                    staticContantBuffer = null;
                }
                if (dynamicConstantBuffer != null)
                {
                    dynamicConstantBuffer.Dispose();
                    dynamicConstantBuffer = null;
                }*/

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
