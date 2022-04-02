using System;
using SharpDX;
using System.Runtime.InteropServices;

using SCCoreSystems.sc_graphics;

using SharpDX.Direct3D;
using SharpDX.Direct3D11;

using System.Drawing;
using Jitter;
using Jitter.Dynamics;
using Jitter.Collision.Shapes;

using Jitter.LinearMath;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;

using SharpDX.DXGI;

using SCCoreSystems.SC_Graphics.SC_Grid;
using System.Text;
using System.IO;
using SharpDX.Multimedia;
using SharpDX.IO;
using System.Xml;
using SharpDX.XAudio2;
using System.Linq;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace SCCoreSystems
{
    public class SC_instancedChunk
    {
        //public Matrix[] matrixofinstances;


        public chunk[] somechunk;

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public SC_instancedChunk_instances[] arrayofzeromeshinstances;
        public Matrix _POSITION { get; set; }

        public DVertex[] Vertices { get; set; }
        public int[] indices;

        private float _sizeX = 0;
        private float _sizeY = 0;
        private float _sizeZ = 0;



        DataStream somedatastream;
        Texture2D texture2D;
        ShaderResourceView someshaderresview;
        Texture2DDescription _textureDescription;
        SharpDX.Direct3D11.Device device;

        public DInstancesccsbytemapxyz[] instancesccsbytemapxyz { get; set; }
        public DInstanceColorNFace[] colorsNFaces { get; set; }


        public DInstanceType[] instances { get; set; }
        public DInstanceShipData[] instancesDataFORWARD { get; set; }
        public DInstanceShipData[] instancesDataRIGHT { get; set; }
        public DInstanceShipData[] instancesDataUP { get; set; }

        public DInstancesByteMap[] instancesbytemaps { get; set; }


        System.Drawing.Bitmap[] imagearray;
        System.Drawing.Bitmap image;
        System.Drawing.Bitmap lastimage;
        public DInstancesccsbytemapxyz[] instancessccsbytemap { get; set; }


        //public DIndexType[] instancesindex { get; set; }

        public DInstanceType[][] instancesIndex { get; set; }

        public int[][] arrayOfSomeMap { get; set; }
        public Matrix[] totalArrayOfMatrixData { get; set; }
        public SC_instancedChunk.DInstanceTypeLocW[] instancesLocationW;
        public SC_instancedChunk.DInstanceTypeLocH[] instancesLocationH;
        public SC_instancedChunk.DInstanceTypeLocD[] instancesLocationD;



        [StructLayout(LayoutKind.Explicit, Size = 64)]
        public struct DVertex
        {
            [FieldOffset(0)]
            public Vector4 position;
            [FieldOffset(16)]
            public Vector4 color;
            [FieldOffset(32)]
            public Vector3 normal;
            [FieldOffset(44)]
            public float padding0;
            [FieldOffset(48)]
            public Vector2 tex;
            [FieldOffset(56)]
            public float padding1;
            [FieldOffset(60)]
            public float padding2;
        }




        /*
        [StructLayout(LayoutKind.Explicit, Size = 80)]
        public struct DVertex
        {
            [FieldOffset(0)]
            public Vector4 position;
            [FieldOffset(16)]
            public Vector4 indexPos;
            [FieldOffset(32)]
            public Vector4 color;
            [FieldOffset(48)]
            public Vector4 normal;
            [FieldOffset(64)]
            public Vector4 tex;
        }*/










        [StructLayout(LayoutKind.Sequential)]
        public struct DInstanceTypeLocW
        {
            public int index;
        };
        [StructLayout(LayoutKind.Sequential)]
        public struct DInstanceTypeLocH
        {
            public int index;
        };
        [StructLayout(LayoutKind.Sequential)]
        public struct DInstanceTypeLocD
        {
            public int index;
        };


        [StructLayout(LayoutKind.Explicit, Size = 16)] //, Pack = 48
        public struct DInstanceType
        {
            [FieldOffset(0)]
            public Vector4 instancePos;
        };

        public struct DInstanceMatrix
        {
            public Matrix instancematrix;
        };

        /*[StructLayout(LayoutKind.Explicit, Size = 32)] //, Pack = 48
        public struct DInstancesByteMap
        {
            [FieldOffset(0)]
            public int one;
            [FieldOffset(4)]
            public int oneTwo;
            [FieldOffset(8)]
            public int two;
            [FieldOffset(12)]
            public int twoTwo;
            [FieldOffset(16)]
            public int three;
            [FieldOffset(20)]
            public int threeTwo;
            [FieldOffset(24)]
            public int four;
            [FieldOffset(28)]
            public int fourTwo;
        };*/


        [StructLayout(LayoutKind.Sequential)] //, Pack = 48 //, Size = 32)
        public struct DInstancesByteMap
        {
            //[FieldOffset(0)]
            public int one;
            //[FieldOffset(4)]
            public int oneTwo;
            //[FieldOffset(8)]
            public int two;
            //[FieldOffset(12)]
            public int twoTwo;
            //[FieldOffset(16)]
            public int three;
            //[FieldOffset(20)]
            public int threeTwo;
            //[FieldOffset(24)]
            public int four;
            //[FieldOffset(28)]
            public int fourTwo;
        };


        [StructLayout(LayoutKind.Explicit)]
        public struct DInstancesccsbytemapxyz
        {
            [FieldOffset(0)]
            public Matrix xyzmap;
            //[FieldOffset(64)]
            //public Matrix ymap;
            //[FieldOffset(128)]
            //public Matrix zmap;
        }




        public SC_instancedChunk.DInstanceMatrix[] instancesmatrix;
        public SC_instancedChunk.DInstanceMatrix[] instancesmatrixb;
        public SC_instancedChunk.DInstanceMatrix[] instancesmatrixc;
        public SC_instancedChunk.DInstanceMatrix[] instancesmatrixd;






        //[StructLayout(LayoutKind.Explicit, Size = 48)] //, Pack = 48
        public struct DInstanceColorNFace
        {
            public Vector4 colorsNFaces;
        };







        [StructLayout(LayoutKind.Explicit, Size = 16)] //, Pack = 48
        public struct DInstanceShipData
        {
            [FieldOffset(0)]
            public Vector4 instancePos;
        };


        /*[StructLayout(LayoutKind.Sequential, Pack = 16)]
        public struct DInstanceTypeTwo
        {
            //public Matrix matrix;
            public Vector4 instancedir;
        };*/

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct DIndexType
        {
            public int indexPos;
        };

        Matrix maininstancematrix;

        public Matrix[] _WORLDMATRIXINSTANCES { get; set; }

        int somewidth;
        int someheight;
        int somedepth;
        //Matrix[] DInstanceMatrixarray;

        public SC_instancedChunk_instances getChunk(int x, int y, int z)
        {
            if ((x < 0) || (y < 0) || (z < 0) || (x >= somewidth) || (y >= (someheight)) || (z >= (somedepth)))
            {
                return null;
            }
            /*
            if (x < 0)
            {
                x *= -1;
                x = (PlanetChunkWidth_R) + x;
            }
            if (y < 0)
            {
                y *= -1;
                y = (PlanetChunkHeight_R) + y;
            }
            if (z < 0)
            {
                z *= -1;
                z = (PlanetChunkDepth_R) + z;
            }*/

            int _index = x + (somewidth) * (y + (someheight) * z);

            return arrayofzeromeshinstances[_index];
        }


        int numberOfObjectInWidth; int numberOfObjectInHeight; int numberOfObjectInDepth; int numberOfInstancesPerObjectInWidth; int numberOfInstancesPerObjectInHeight; int numberOfInstancesPerObjectInDepth; float planeSize;

        int tinyChunkWidth; int tinyChunkHeight; int tinyChunkDepth;

        int voxeltype;
        int mapofbytes;

        public SC_instancedChunk(float sizex, float sizey, float sizez, Vector4 color, int width, int height, int depth, Vector3 pos, Matrix _maininstancematrix, chunk someobjectmapchunk, int _addtoworld, float _mass, World _the_world, bool _is_static, sc_console.SC_console_directx.BodyTag _tag, int numberofmainobjectx, int numberofmainobjecty, int numberofmainobjectz, int numberOfObjectInWidth_, int numberOfObjectInHeight_, int numberOfObjectInDepth_, int numberOfInstancesPerObjectInWidth_, int numberOfInstancesPerObjectInHeight_, int numberOfInstancesPerObjectInDepth_, int tinyChunkWidth_, int tinyChunkHeight_, int tinyChunkDepth_, float planeSize_, float offsetinstance, sccstrigvertbuilder someoriginalmapNVerts, SC_instancedChunk.DInstancesByteMap[] instancesbytemaps_, int somechunkkeyboardpriminstanceindex_, int chunkprimindex_, int voxeltype_, int mapofbytes_, int mainix, int mainiy, int mainiz, int meshzeroix, int meshzeroiy, int meshzeroiz)
        {
            mapofbytes = mapofbytes_;
            voxeltype = voxeltype_;
            //instancesbytemaps = instancesbytemaps_;
            //instancesbytemaps = instancesbytemaps_;

            tinyChunkWidth = tinyChunkWidth_;
            tinyChunkHeight = tinyChunkHeight_;
            tinyChunkDepth = tinyChunkDepth_;

            numberOfObjectInWidth = numberOfObjectInWidth_;
            numberOfObjectInHeight = numberOfObjectInHeight_;
            numberOfObjectInDepth = numberOfObjectInDepth_;
            numberOfInstancesPerObjectInWidth = numberOfInstancesPerObjectInWidth_;
            numberOfInstancesPerObjectInHeight = numberOfInstancesPerObjectInHeight_;
            numberOfInstancesPerObjectInDepth = numberOfInstancesPerObjectInDepth_;
            planeSize = planeSize_;




            somewidth = numberOfInstancesPerObjectInWidth;
            someheight = numberOfInstancesPerObjectInHeight;
            somedepth = numberOfInstancesPerObjectInDepth;

            maininstancematrix = _maininstancematrix;

            this._color = color;
            this._sizeX = sizex;
            this._sizeY = sizey;
            this._sizeZ = sizez;

            this._chunkPos = pos;

            int[] theMap;



            
            arrayofzeromeshinstances = new SC_instancedChunk_instances[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];




            //instancesbytemaps = new DInstancesByteMap[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];
            //instancessccsbytemap = new DInstancesccsbytemapxyz[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];


            instancesmatrix = new DInstanceMatrix[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];
            instancesmatrixb = new DInstanceMatrix[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];
            instancesmatrixc = new DInstanceMatrix[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];
            instancesmatrixd = new DInstanceMatrix[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];




            colorsNFaces = new DInstanceColorNFace[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];

            instancesLocationW = new DInstanceTypeLocW[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];
            instancesLocationH = new DInstanceTypeLocH[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];

            instancesDataFORWARD = new DInstanceShipData[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];
            instancesDataRIGHT = new DInstanceShipData[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];
            instancesDataUP = new DInstanceShipData[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];

            instances = new DInstanceType[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];

            arrayOfSomeMap = new int[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth][];

            Vector4 position;
            //chunk newChunker;

            instancesIndex = new DInstanceType[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth][];

            imagearray = new Bitmap[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];




            double m11 = 0;
            double m12 = 0;
            double m13 = 0;
            double m14 = 0;
            double m21 = 0;
            double m22 = 0;
            double m23 = 0;
            double m24 = 0;
            double m31 = 0;
            double m32 = 0;
            double m33 = 0;
            double m34 = 0;
            double m41 = 0;
            double m42 = 0;
            double m43 = 0;
            double m44 = 0;



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






            double m11c = 0;
            double m12c = 0;
            double m13c = 0;
            double m14c = 0;
            double m21c = 0;
            double m22c = 0;
            double m23c = 0;
            double m24c = 0;
            double m31c = 0;
            double m32c = 0;
            double m33c = 0;
            double m34c = 0;
            double m41c = 0;
            double m42c = 0;
            double m43c = 0;
            double m44c = 0;


            double m11d = 0;
            double m12d = 0;
            double m13d = 0;
            double m14d = 0;
            double m21d = 0;
            double m22d = 0;
            double m23d = 0;
            double m24d = 0;
            double m31d = 0;
            double m32d = 0;
            double m33d = 0;
            double m34d = 0;
            double m41d = 0;
            double m42d = 0;
            double m43d = 0;
            double m44d = 0;














            var offsetX = 1;
            var offsetY = 1;
            var offsetZ = 1;

            var offsetInstancedShips = offsetinstance;// 2.5f;



            somechunk = new chunk[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight * numberOfInstancesPerObjectInDepth];

            /*
            _textureDescription = new Texture2DDescription
            {
                CpuAccessFlags = CpuAccessFlags.Write,
                BindFlags = BindFlags.ShaderResource,//BindFlags.None, //| BindFlags.RenderTarget
                Format = Format.B8G8R8A8_UNorm,
                Width = 1000,
                Height = 1000,
                OptionFlags = ResourceOptionFlags.None,
                MipLevels = 1,
                ArraySize = 1,
                SampleDescription = { Count = 1, Quality = 0 },
                Usage = ResourceUsage.Dynamic //ResourceUsage.Staging // | 
            };*/

            /*
            _textureDescription = new Texture2DDescription
            {
                CpuAccessFlags = CpuAccessFlags.Write,
                BindFlags = BindFlags.None,//BindFlags.None, //| BindFlags.RenderTarget
                Format = Format.B8G8R8A8_UNorm,
                Width = 1000,
                Height = 1000,
                OptionFlags = ResourceOptionFlags.None,
                MipLevels = 1,
                ArraySize = 1,
                SampleDescription = { Count = 1, Quality = 0 },
                Usage = ResourceUsage.Default
            };*/
            /*
            Texture2DDescription _textureDescription = new Texture2DDescription
            {
                CpuAccessFlags = CpuAccessFlags.Write,
                BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                Format = Format.B8G8R8A8_UNorm,
                Width = 1000,
                Height = 1000,
                OptionFlags = ResourceOptionFlags.GenerateMipMaps,
                MipLevels = 1,
                ArraySize = 1,
                SampleDescription = { Count = 1, Quality = 0 },
                Usage = ResourceUsage.Default
            };*/


            /*
            device = sc_console.SC_console_directx.D3D.device;


            texture2D = new Texture2D(device, new Texture2DDescription()
            {
                CpuAccessFlags = CpuAccessFlags.Write,
                BindFlags = BindFlags.ShaderResource, //BindFlags.ShaderResource 
                Format = Format.B8G8R8A8_UNorm,
                Width = 1000,
                Height = 1000,
                OptionFlags = ResourceOptionFlags.None,
                MipLevels = 1,
                ArraySize = 1,
                SampleDescription = { Count = 1, Quality = 0 },
                Usage = ResourceUsage.Dynamic | ResourceUsage.Staging

            });

            var _bitmap = new System.Drawing.Bitmap(1000, 1000, PixelFormat.Format32bppArgb);
            var boundsRect = new System.Drawing.Rectangle(0, 0, 1000, 1000);
            var bmpData = _bitmap.LockBits(boundsRect, ImageLockMode.ReadOnly, _bitmap.PixelFormat);
            var _bytesTotal = Math.Abs(bmpData.Stride) * _bitmap.Height;
            _bitmap.UnlockBits(bmpData);

            //someshaderresview = new ShaderResourceView(device, texture2D);
            somedatastream = new DataStream(_bytesTotal, true, true);*/






            //texture2D = new Texture2D(sc_console.SC_console_directx.D3D.device, _textureDescription);

      

            //MainWindow.MessageBox((IntPtr)0, "" + _bytesTotal, "sccsmsg", 0);



            for (int x = 0; x < numberOfInstancesPerObjectInWidth; x++)
            {
                for (int y = 0; y < numberOfInstancesPerObjectInHeight; y++)
                {
                    for (int z = 0; z < numberOfInstancesPerObjectInDepth; z++)
                    {


                        
                        var chunkinstindex = x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z);
                        somechunk[chunkinstindex] = new chunk();






                        
                        position = new Vector4(x * offsetInstancedShips, y * offsetInstancedShips, z * offsetInstancedShips, 1);

                        position.X *= (tinyChunkWidth);
                        position.Y *= (tinyChunkHeight);
                        position.Z *= (tinyChunkDepth);

                        position.X *= (planeSize);
                        position.Y *= (planeSize);
                        position.Z *= (planeSize);

                        position.X += (_chunkPos.X);
                        position.Y += (_chunkPos.Y);
                        position.Z += (_chunkPos.Z);



                        somechunk[chunkinstindex].somechunkpos = position;

                        Matrix _tempMatrix = maininstancematrix;



                        
                        Quaternion somedirquat;
                        Quaternion.RotationMatrix(ref _tempMatrix, out somedirquat);

                        var dirInstanceRight = sc_maths._getDirection(Vector3.Right, somedirquat);
                        var dirInstanceUp = sc_maths._newgetdirup(somedirquat);
                        var dirInstanceForward = sc_maths._newgetdirforward(somedirquat);

                        //position = position - (new Vector4(dirInstanceRight.X, dirInstanceRight.Y, dirInstanceRight.Z, 1.0f) * (tinyChunkWidth * planeSize * 0.5f));
                        //position = position - (new Vector4(dirInstanceUp.X, dirInstanceUp.Y, dirInstanceUp.Z, 1.0f) * (tinyChunkWidth * planeSize * 0.5f));
                        //position = position + (new Vector4(dirInstanceForward.X, dirInstanceForward.Y, dirInstanceForward.Z, 1.0f) * (tinyChunkWidth * planeSize * 0.5f));

                        Vector4 somevecx = new Vector4(dirInstanceRight.X, dirInstanceRight.Y, dirInstanceRight.Z, 1.0f);
                        Vector4 somevecy = new Vector4(dirInstanceUp.X, dirInstanceUp.Y, dirInstanceUp.Z, 1.0f);
                        Vector4 somevecz = new Vector4(dirInstanceForward.X, dirInstanceForward.Y, dirInstanceForward.Z, 1.0f);

                        Vector4 somemainpos = new Vector4(_tempMatrix.M41, _tempMatrix.M42, _tempMatrix.M43, 1.0f);
                        Vector4 someinstancepostest = somemainpos;// Vector4.Zero;

                        someinstancepostest = someinstancepostest + (somevecx * ((x * offsetInstancedShips) * tinyChunkWidth * planeSize));
                        someinstancepostest = someinstancepostest + (somevecy * ((y * offsetInstancedShips) * tinyChunkWidth * planeSize));
                        someinstancepostest = someinstancepostest + (somevecz * ((z * offsetInstancedShips) * tinyChunkWidth * planeSize));

                        position = someinstancepostest;








                        
                        instancesIndex[chunkinstindex] = new DInstanceType[tinyChunkWidth * tinyChunkHeight * tinyChunkDepth];


                        /*
                        somechunk[chunkinstindex].startBuildingArray(position, out m11, out m12, out m13, out m14, out m21, out m22, out m23, out m24, out m31, out m32, out m33, out m34, out m41, out m42, out m43, out m44, out theMap, this, 1, null, numberofmainobjectx, numberofmainobjecty, numberofmainobjectz, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, voxeltype, mapofbytes, mainix, mainiy, mainiz, meshzeroix, meshzeroiy, meshzeroiz, x, y, z

                            , out m11b, out m12b, out m13b, out m14b, out m21b, out m22b, out m23b, out m24b, out m31b, out m32b, out m33b, out m34b, out m41b, out m42b, out m43b, out m44b,
                            out m11c, out m12c, out m13c, out m14c, out m21c, out m22c, out m23c, out m24c, out m31c, out m32c, out m33c, out m34c, out m41c, out m42c, out m43c, out m44c,
                            out m11d, out m12d, out m13d, out m14d, out m21d, out m22d, out m23d, out m24d, out m31d, out m32d, out m33d, out m34d, out m41d, out m42d, out m43d, out m44d); //, somechunkkeyboardpriminstanceindex_, chunkprimindex_, chunkinstindex
                        */






                        arrayofzeromeshinstances[chunkinstindex] = new SC_instancedChunk_instances();

                        arrayofzeromeshinstances[chunkinstindex].map = somechunk[chunkinstindex].map;

                        _tempMatrix.M41 = somechunk[chunkinstindex].somechunkpos.X;
                        _tempMatrix.M42 = somechunk[chunkinstindex].somechunkpos.Y;
                        _tempMatrix.M43 = somechunk[chunkinstindex].somechunkpos.Z;

                        int columns0 = 1000;
                        int rows0 = 1000;
                        int memoryBitmapStride0 = columns0 * 4;

                        arrayofzeromeshinstances[chunkinstindex].map = null;
                        somechunk[chunkinstindex].map = null;
                        theMap = null;





                        //System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                        //customCulture.NumberFormat.NumberDecimalSeparator = ".";
                        //System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

                        //short_path = "wave_audio_" + _records_counter;
                        //path = "c:\\Users\\steve\\Desktop\\testXML\\" + 0 + ".xml";
                        var path = @"C:\Users\steve\Desktop\savedchunkdata\" + "chunkdata" + bitmapcounter + ".xml";
                        //last_xml_filepath = path;
                        //sc_can_start_rec_counter_before_add_index = sc_can_start_rec_counter;
                        //doc = new XmlDocument();
                        //writer = new XmlTextWriter(Console.Out);
                        //XmlTextWriter writer = new XmlTextWriter(path, System.Text.Encoding.UTF8);
                        //sc_can_start_rec_counter_before_add_index = sc_can_start_rec_counter;

                        //root = doc.DocumentElement;
                        /*
                        writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");

                        writer.Formatting = Formatting.Indented;
                        writer.Indentation = 2;
                        */

                        /*

                        FileStream somefilestream = new FileStream(path, FileMode.CreateNew);

                        /*byte[] somebytearray = new byte[somechunk[chunkinstindex].map.Length];

                        for (int i = 0; i < somechunk[chunkinstindex].map.Length; i++)
                        {
                            somebytearray[i] = 1;// (byte)somechunk[chunkinstindex].map[i];
                        }

                        somefilestream.Write(somechunk[chunkinstindex].map, 0, somechunk[chunkinstindex].map.Length);

                        somefilestream.Close();

                        */




                        /*
                        
                        writer.WriteStartElement("root"); // open 0
                        writer.WriteStartElement("chunk"); // open 1
                        writer.WriteStartElement("data"); // open 2





                        
                        writer.WriteStartElement("bytes"); //open 3
                        //writer.WriteString("" + _time_of_recording_start);
                        writer.WriteBase64(somechunk[chunkinstindex].map, 0, somechunk[chunkinstindex].map.Length);


                        writer.WriteEndElement(); //close 3

                        /*_time_of_recording_end = DateTime.Now;
                        writer.WriteStartElement("EndTime"); //open 3
                        writer.WriteString("" + _time_of_recording_end);
                        writer.WriteEndElement(); //close 3

                        writer.WriteStartElement("size"); //open 3
                        long size = new System.IO.FileInfo(last_wave_filepath).Length;
                        writer.WriteString("" + size);
                        writer.WriteEndElement(); //close 3

                        writer.WriteStartElement("length"); //open 4
                        int length = GetSoundLength(last_wave_filepath);
                        writer.WriteString("" + length);
                        writer.WriteEndElement(); //close 4
                        

                        writer.WriteEndElement(); //close 0
                        writer.WriteEndElement(); //close 1
                        writer.WriteEndElement(); //close 2

                        writer.Close();*/
























                        //copytexture(chunkinstindex,_bytesTotal);



                        //somechunk[chunkinstindex].map = null;






                        //Pixelb


                        //device.ImmediateContext.CopyResource(Marshal.UnsafeAddrOfPinnedArrayElement(theMap, 0), texture2D);
                        /*
                        IntPtr mapptr = Marshal.UnsafeAddrOfPinnedArrayElement(somechunk[chunkinstindex].map, 0);


                        var dataBox1 = device.ImmediateContext.MapSubresource(texture2D, 0, SharpDX.Direct3D11.MapMode.Write, SharpDX.Direct3D11.MapFlags.None);

                        int memoryBitmapStride = _textureDescription.Width * 4;

                        int columns = _textureDescription.Width;
                        int rows = _textureDescription.Height;
                        IntPtr interptr1 = dataBox1.DataPointer;

                        // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (_width * 4)
                        if (dataBox1.RowPitch == memoryBitmapStride)
                        {
                            // Stride is the same
                            Marshal.Copy(interptr1, _textureByteArray, 0, _bytesTotal);
                        }
                        else
                        {
                            // Stride not the same - copy line by line
                            for (int y = 0; y < _height; y++)
                            {
                                Marshal.Copy(interptr1 + y * dataBox1.RowPitch, _textureByteArray, y * memoryBitmapStride, memoryBitmapStride);
                            }
                        }
                        
                        var somebitmap = new System.Drawing.Bitmap(160, 128, 160 * 4, PixelFormat.Format32bppArgb, interptr1);
                        SharpDX.Utilities.CopyMemory(mapptr, interptr1, _bytesTotal);

                        MainWindow.MessageBox((IntPtr)0, "" + memoryBitmapStride, "sccsmsg", 0);

                        device.ImmediateContext.UnmapSubresource(texture2D, 0);*/


                        //DeleteObject(interptr1);






                        //texture2D

                        //image = new System.Drawing.Bitmap(columns0, rows0, memoryBitmapStride0, PixelFormat.Indexed, Marshal.UnsafeAddrOfPinnedArrayElement(theMap, 0));
                        //image.Save(@"C:\Users\steve\Desktop\screenrecord\" + bitmapcounter + "_" + rows0.ToString("00") + columns0.ToString("00") + ".png");

                        //imagearray[chunkinstindex] = image;





                        bitmapcounter++;

                        /*
                        if (lastimage != null)
                        {
                            lastimage.Dispose();
                        }
                        lastimage = image;*/









                        //somechunk[chunkinstindex].map = null;






                        


                        Matrix tempMatrixinst = _tempMatrix;





                        arrayofzeromeshinstances[chunkinstindex].current_pos = tempMatrixinst;
                        arrayofzeromeshinstances[chunkinstindex]._POSITION = tempMatrixinst;







                        
                        float r = 0.35f;
                        float g = 0.95f;
                        float b = 0.15f;
                        float a = 1; //

                        colorsNFaces[chunkinstindex] = new DInstanceColorNFace()
                        {
                            colorsNFaces = new Vector4(r, g, b, 0.1f)//255
                        };


                        arrayofzeromeshinstances[chunkinstindex].current_pos = _tempMatrix;

                        instances[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceType()
                        {
                            instancePos = new Vector4(somechunk[chunkinstindex].somechunkpos.X, somechunk[chunkinstindex].somechunkpos.Y, somechunk[chunkinstindex].somechunkpos.Z, 1),
                        };


                        Matrix somechunkmap = Matrix.Identity;

                        somechunkmap.M11 = (float)m11;
                        somechunkmap.M12 = (float)m12;
                        somechunkmap.M13 = (float)m13;
                        somechunkmap.M14 = (float)m14;

                        somechunkmap.M21 = (float)m21;
                        somechunkmap.M22 = (float)m22;
                        somechunkmap.M23 = (float)m23;
                        somechunkmap.M24 = (float)m24;

                        somechunkmap.M31 = (float)m31;
                        somechunkmap.M32 = (float)m32;
                        somechunkmap.M33 = (float)m33;
                        somechunkmap.M34 = (float)m34;

                        somechunkmap.M41 = (float)m41;
                        somechunkmap.M42 = (float)m42;
                        somechunkmap.M43 = (float)m43;
                        somechunkmap.M44 = (float)m44;



                        instancesmatrix[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceMatrix()
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




                        instancesmatrixb[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceMatrix()
                        {
                            instancematrix = somechunkmap,
                        };
                        somechunkmap = Matrix.Identity;

                        somechunkmap.M11 = (float)m11c;
                        somechunkmap.M12 = (float)m12c;
                        somechunkmap.M13 = (float)m13c;
                        somechunkmap.M14 = (float)m14c;

                        somechunkmap.M21 = (float)m21c;
                        somechunkmap.M22 = (float)m22c;
                        somechunkmap.M23 = (float)m23c;
                        somechunkmap.M24 = (float)m24c;

                        somechunkmap.M31 = (float)m31c;
                        somechunkmap.M32 = (float)m32c;
                        somechunkmap.M33 = (float)m33c;
                        somechunkmap.M34 = (float)m34c;

                        somechunkmap.M41 = (float)m41c;
                        somechunkmap.M42 = (float)m42c;
                        somechunkmap.M43 = (float)m43c;
                        somechunkmap.M44 = (float)m44c;


                        instancesmatrixc[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceMatrix()
                        {
                            instancematrix = somechunkmap,
                        };

                        somechunkmap = Matrix.Identity;

                        somechunkmap.M11 = (float)m11d;
                        somechunkmap.M12 = (float)m12d;
                        somechunkmap.M13 = (float)m13d;
                        somechunkmap.M14 = (float)m14d;

                        somechunkmap.M21 = (float)m21d;
                        somechunkmap.M22 = (float)m22d;
                        somechunkmap.M23 = (float)m23d;
                        somechunkmap.M24 = (float)m24d;

                        somechunkmap.M31 = (float)m31d;
                        somechunkmap.M32 = (float)m32d;
                        somechunkmap.M33 = (float)m33d;
                        somechunkmap.M34 = (float)m34d;

                        somechunkmap.M41 = (float)m41d;
                        somechunkmap.M42 = (float)m42d;
                        somechunkmap.M43 = (float)m43d;
                        somechunkmap.M44 = (float)m44d;

                        instancesmatrixd[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceMatrix()
                        {
                            instancematrix = somechunkmap,
                        };














                        instancesLocationW[chunkinstindex] = new DInstanceTypeLocW()
                        {
                            index = numberOfInstancesPerObjectInWidth - 1 - x
                            //index = x
                        };

                        instancesLocationH[chunkinstindex] = new DInstanceTypeLocH()
                        {
                            index = numberOfInstancesPerObjectInHeight - 1 - y //instY - 1 - y
                        };













                        instancesDataFORWARD[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceShipData()
                        {
                            instancePos = new Vector4(somechunk[chunkinstindex].somechunkpos.X, somechunk[chunkinstindex].somechunkpos.Y, somechunk[chunkinstindex].somechunkpos.Z, 1),
                        };
                        instancesDataRIGHT[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceShipData()
                        {
                            instancePos = new Vector4(somechunk[chunkinstindex].somechunkpos.X, somechunk[chunkinstindex].somechunkpos.Y, somechunk[chunkinstindex].somechunkpos.Z, 1),
                        };
                        instancesDataUP[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = new DInstanceShipData()
                        {
                            instancePos = new Vector4(somechunk[chunkinstindex].somechunkpos.X, somechunk[chunkinstindex].somechunkpos.Y, somechunk[chunkinstindex].somechunkpos.Z, 1),
                        };

                        arrayOfSomeMap[x + numberOfInstancesPerObjectInWidth * (y + numberOfInstancesPerObjectInHeight * z)] = theMap;
                        

                    }
                }
            }


            /*
            for (int i = 0;i < imagearray.Length;i++)
            {
                int columns0 = 1000;
                int rows0 = 1000;
                int memoryBitmapStride0 = columns0 * 4;


                imagearray[i].Save(@"C:\Users\steve\Desktop\screenrecord\" + i + "_" + rows0.ToString("00") + columns0.ToString("00") + ".png");
            }
            */





            instancesbytemaps = instancesbytemaps_;
            


        }


        //stackoverflow49915147

        public unsafe void copytexture(int chunkinstindex, int _bytesTotal)
        {
            IntPtr mapptr = Marshal.UnsafeAddrOfPinnedArrayElement(somechunk[chunkinstindex].map, 0);


            byte[] somebytearray = new byte[somechunk[chunkinstindex].map.Length];

            for (int i = 0; i < somechunk[chunkinstindex].map.Length; i++)
            {
                somebytearray[i] = (byte)somechunk[chunkinstindex].map[i];
            }

            var dataBox0 = device.ImmediateContext.MapSubresource(texture2D, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None);
            //var dataBox0 = device.ImmediateContext.MapSubresource(texture2D, 0, texture2D.Description.Usage == ResourceUsage.Dynamic ? MapMode.WriteDiscard : MapMode.Write, SharpDX.Direct3D11.MapFlags.None);

            somedatastream.Write(somebytearray, 0, somebytearray.Length);

            device.ImmediateContext.UnmapSubresource(texture2D, 0);




            /*var dataBox0 = device.ImmediateContext.MapSubresource(texture2D, 0, texture2D.Description.Usage == ResourceUsage.Dynamic ? MapMode.WriteDiscard : MapMode.Write, SharpDX.Direct3D11.MapFlags.None);

            //var dataBox0 = device.ImmediateContext.MapSubresource(texture2D, 0, SharpDX.Direct3D11.MapMode.Write, SharpDX.Direct3D11.MapFlags.None);

            int memoryBitmapStride0 = _textureDescription.Width * 4;

            int columns0 = _textureDescription.Width;
            int rows0 = _textureDescription.Height;
            IntPtr interptr0 = dataBox0.DataPointer;
            IntPtr mapptr = Marshal.UnsafeAddrOfPinnedArrayElement(somechunk[chunkinstindex].map, 0);

            // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (_width * 4)
            if (dataBox0.RowPitch == memoryBitmapStride0)
            {
                // Stride is the same
                //Marshal.Copy(interptr0, _textureByteArray, 0, _bytesTotal);
                Utilities.CopyMemory((IntPtr)interptr0, (IntPtr)mapptr, _bytesTotal);
                //Marshal.Copy((IntPtr)interptr0, (IntPtr)mapptr, 0, _bytesTotal);
            }
            else
            {
                // Stride not the same - copy line by line
                for (int y = 0; y < rows0; y++)
                {
                    //Marshal.Copy(interptr0 + y * dataBox0.RowPitch, _textureByteArray, y * memoryBitmapStride0, memoryBitmapStride0);
                    //Marshal.Copy(mapptr + y * dataBox0.RowPitch, interptr0, y * memoryBitmapStride0, memoryBitmapStride0);
                    Utilities.CopyMemory((IntPtr)interptr0, mapptr + y * dataBox0.RowPitch, memoryBitmapStride0);

                }
            }
            //var somebitmap = new System.Drawing.Bitmap(160, 128, 160 * 4, PixelFormat.Format32bppArgb, interptr0);   
            device.ImmediateContext.UnmapSubresource(texture2D, 0);
            DeleteObject(interptr0);
            DeleteObject(mapptr);*/










            /*// var box = device.ImmediateContext.MapSubresource(texture2D, 0, texture2D.Description.Usage == ResourceUsage.Dynamic ? MapMode.WriteDiscard : MapMode.Write, SharpDX.Direct3D11.MapFlags.None);
            var box = device.ImmediateContext.MapSubresource(texture2D, 0, MapMode.Write, SharpDX.Direct3D11.MapFlags.None);

            // If depth == 1 (Texture1D, Texture2D or TextureCube), then depthStride is not used
            var boxDepthStride = box.SlicePitch;

            IntPtr mapptr = Marshal.UnsafeAddrOfPinnedArrayElement(somechunk[chunkinstindex].map, 0);
            // Otherwise, the long way by copying each scanline
            var destPerDepthPtr = (byte*)box.DataPointer;
            var sourcePtr = (byte*)mapptr;

            int columns0 = 1000;
            int rows0 = 1000;
            int memoryBitmapStride0 = columns0 * 4;


            // Iterate on all depths
            for (int j = 0; j < 1000; j++)
            {
                var destPtr = destPerDepthPtr;
                // Iterate on each line
                for (int i = 0; i < 1000; i++)
                {
                    Utilities.CopyMemory((IntPtr)destPtr, (IntPtr)sourcePtr, memoryBitmapStride0);
                    destPtr += box.RowPitch;
                    sourcePtr += memoryBitmapStride0;
                }
                destPerDepthPtr += box.SlicePitch;
            }

            device.ImmediateContext.UnmapSubresource(texture2D, 0);
            */

            /*// The fast way: If same stride, we can directly copy the whole texture in one shot
            if (box.RowPitch == rowStride && boxDepthStride == textureDepthStride)
            {
                Utilities.CopyMemory(box.DataPointer, fromData.Pointer, sizeOfTextureData);
            }
            else
            {
                // Otherwise, the long way by copying each scanline
                var destPerDepthPtr = (byte*)box.DataPointer;
                var sourcePtr = (byte*)fromData.Pointer;

                // Iterate on all depths
                for (int j = 0; j < depth; j++)
                {
                    var destPtr = destPerDepthPtr;
                    // Iterate on each line
                    for (int i = 0; i < height; i++)
                    {
                        Utilities.CopyMemory((IntPtr)destPtr, (IntPtr)sourcePtr, rowStride);
                        destPtr += box.RowPitch;
                        sourcePtr += rowStride;
                    }
                    destPerDepthPtr += box.SlicePitch;
                }

            }*/









            /*//device.ImmediateContext.CopyResource(Marshal.UnsafeAddrOfPinnedArrayElement(theMap, 0), texture2D);

            IntPtr mapptr = Marshal.UnsafeAddrOfPinnedArrayElement(somechunk[chunkinstindex].map, 0);


            //var dataBox1 = device.ImmediateContext.MapSubresource(texture2D, 0, SharpDX.Direct3D11.MapMode.Write, SharpDX.Direct3D11.MapFlags.None);
            var dataBox1 = device.ImmediateContext.MapSubresource(texture2D, 0, texture2D.Description.Usage == ResourceUsage.Dynamic ? MapMode.WriteDiscard : MapMode.Write, SharpDX.Direct3D11.MapFlags.None);

            int memoryBitmapStride = _textureDescription.Width * 4;

            int columns = _textureDescription.Width;
            int rows = _textureDescription.Height;
            var interptr1 = (byte*)dataBox1.DataPointer;
            var oriptr = (byte*)mapptr;

            SharpDX.Utilities.CopyMemory(new IntPtr(interptr1), mapptr, _bytesTotal);

            //MainWindow.MessageBox((IntPtr)0, "" + memoryBitmapStride, "sccsmsg", 0);

            device.ImmediateContext.UnmapSubresource(texture2D, 0);

            */
            //DeleteObject(interptr1);

            // Otherwise, the long way by copying each scanline
            //var sourcePerDepthPtr = (byte*)box.DataPointer;
            //var destPtr = (byte*)toData.Pointer;

        }


























        int bitmapcounter = 0;





        //SC_instancedChunk[] arrayOfChunks;
        //chunkData[] arrayOfChunkData;

        public int InstanceCount = 0;

        public Vector4 _color;

        public int instanceCounter { get; set; }
        public byte[] map { get; set; }

        public Vector3 _chunkPos { get; set; }

    }
}