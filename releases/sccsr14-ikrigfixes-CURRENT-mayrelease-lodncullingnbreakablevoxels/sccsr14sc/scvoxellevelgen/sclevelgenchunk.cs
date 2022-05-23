using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System.Linq;
using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System;
using SharpDX;
using System.Runtime.InteropServices;

using sccs.scgraphics;

using SharpDX.Direct3D;
using SharpDX.Direct3D11;

using System.Drawing;
using Jitter;
using Jitter.Dynamics;
using Jitter.Collision.Shapes;

using Jitter.LinearMath;
using System.Collections;
using System.Collections.Generic;

using SharpDX.D3DCompiler;

using System.Xml;

using System.IO;

namespace sccs
{
    public class sclevelgenchunk
    {

        public chunkData chunkdatlevel;

        public static int mapWidth = 20;
        public static int mapHeight = 1;
        public static int mapDepth = 20;


        public static int tinyChunkWidth = 20;
        public static int tinyChunkHeight = 20;
        public static int tinyChunkDepth = 20;

        public static int mapObjectInstanceWidth = 1;
        public static int mapObjectInstanceHeight = 1;
        public static int mapObjectInstanceDepth = 1;

        public SharpDX.Direct3D11.Buffer InstanceBuffer { get; set; }

        public int VertexCount { get; set; }
        public int IndexCount { get; set; }

        public DVertex[] Vertices { get; set; }
        public int[] indices;

        private float _sizeX = 0;
        private float _sizeY = 0;
        private float _sizeZ = 0;

        public DVertex[][] arrayOfDVertex { get; set; }
        public DInstanceType[] instances { get; set; }
        // Constructor


        VertexShader VertexShader;
        PixelShader PixelShader;
        GeometryShader GeometryShader;
        InputLayout Layout;

        public sclevelgenchunkshader shaderOfChunk;
        public DMatrixBuffer[] arrayOfMatrixBuff;

        public SharpDX.Direct3D11.Buffer[] vertBuffers;
        public SharpDX.Direct3D11.Buffer[] colorBuffers;
        public SharpDX.Direct3D11.Buffer[] indexBuffers;
        public SharpDX.Direct3D11.Buffer[] instanceBuffers;
        public SharpDX.Direct3D11.Buffer[] normalBuffers;
        public SharpDX.Direct3D11.Buffer[] texBuffers;
        public SharpDX.Direct3D11.Buffer[] dVertBuffers;

        public DLightBuffer[] lightBuffer;


        public struct chunkData
        {
            public int startOnce;
            public SharpDX.Direct3D11.Buffer instanceBuffer;
            //public Vector4[][] arrayOfInstanceVertex;
            public DInstanceType[] arrayOfInstancePos;
            public int[][] arrayOfInstanceIndices;
            //public Vector3[][] arrayOfInstanceNormals;
            //public Vector2[][] arrayOfInstanceTextureCoordinates;
            //public Vector4[][] arrayOfInstanceColors;
            public DVertex[][] dVertexData;

            public int[] someswitches;


            public SharpDX.Direct3D11.Device Device;
            public Matrix worldMatrix;
            public Matrix viewMatrix;
            public Matrix projectionMatrix;
            //public DShaderManager shaderManager;
            public sclevelgenchunkshader chunkShader;
            public DMatrixBuffer[] matrixBuffer;
            public DLightBuffer[] lightBuffer;
            //public SharpDX.Direct3D11.Buffer[] vertBuffers;
            //public SharpDX.Direct3D11.Buffer[] colorBuffers;
            public SharpDX.Direct3D11.Buffer[] indexBuffers;
            //public SharpDX.Direct3D11.Buffer[] normalBuffers;
            //public SharpDX.Direct3D11.Buffer[] texBuffers;
            public SharpDX.Direct3D11.Buffer[] dVertBuffers;

            public DeviceContext _renderingContext;
            public SharpDX.Direct3D11.Buffer[] instanceBuffers;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct DMatrixBuffer
        {
            public Matrix world;
            public Matrix view;
            public Matrix proj;
        }

        /*[StructLayout(LayoutKind.Sequential)]
        public struct DLightBuffer
        {
            public Vector4 ambientColor;
            public Vector4 diffuseColor;
            public Vector3 lightDirection;
            public float padding; // Added extra padding so structure is a multiple of 16.
        }*/

        [StructLayout(LayoutKind.Explicit)]
        public struct DLightBuffer
        {
            /*public Vector4 ambientColor;
            public Vector4 diffuseColor;
            public Vector3 lightDirection;
            public float padding; // Added extra padding so structure is a multiple of 16.
            */
            [FieldOffset(0)]
            public Vector4 ambientColor;
            [FieldOffset(16)]
            public Vector4 diffuseColor;
            [FieldOffset(32)]
            public Vector4 specularcolor;
            [FieldOffset(48)]
            public Vector4 lightDirection;
            [FieldOffset(64)]
            public Vector4 lightPosition;
            [FieldOffset(80)]
            public Vector4 lightextras;
            /*
            [FieldOffset(60)]
            public float padding0;
            [FieldOffset(64)]
            public Vector3 lightPosition;
            [FieldOffset(76)]
            public float padding1;*/
        }


        public float planeSize = 0.05f;

        public sccstriglevelchunk newChunker;
        public sccstriglevelchunk[] arrayofchunks;
        public sccstriglevelchunkmaps[] arrayofchunksmaps;

        public static sclevelgenchunk thesclevelgenchunk;

        public sclevelgenchunk(SharpDX.Direct3D11.Device device, float xi, float yi, float zi, Vector4 color, int width, int height, int depth, Vector3 pos, int mapWidth_, int mapHeight_, int mapDepth_, int tinyChunkWidth_, int tinyChunkHeight_, int tinyChunkDepth_, int mapObjectInstanceWidth_, int mapObjectInstanceHeight_, int mapObjectInstanceDepth_, float planesize_, LevelGenerator4 levelgen, int typeofterraintiles, int somearraycount) //,DInstanceType[] _instances
        {
            thesclevelgenchunk = this;

            planeSize = planesize_;

            mapWidth = mapWidth_;
            mapHeight = mapHeight_;
            mapDepth = mapDepth_;

            tinyChunkWidth = tinyChunkWidth_;
            tinyChunkHeight = tinyChunkHeight_;
            tinyChunkDepth = tinyChunkDepth_;

            mapObjectInstanceWidth = mapObjectInstanceWidth_;
            mapObjectInstanceHeight = mapObjectInstanceHeight_;
            mapObjectInstanceDepth = mapObjectInstanceDepth_;

            var vsFileNameByteArray = sccsr14sc.Properties.Resources.textureTrigLevelVS;
            var psFileNameByteArray = sccsr14sc.Properties.Resources.textureTrigLevelPS;
            var gsFileNameByteArray = sccsr14sc.Properties.Resources.geometryshaderLevel;

            ShaderBytecode vertexShaderByteCode = ShaderBytecode.Compile(vsFileNameByteArray, "TextureVertexShader", "vs_5_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);
            ShaderBytecode pixelShaderByteCode = ShaderBytecode.Compile(psFileNameByteArray, "TexturePixelShader", "ps_5_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);
            ShaderBytecode geometryshaderbytecode = ShaderBytecode.Compile(gsFileNameByteArray, "GS", "gs_5_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);

            // Create the vertex shader from the buffer.
            VertexShader = new VertexShader(device, vertexShaderByteCode);
            // Create the pixel shader from the buffer.
            PixelShader = new PixelShader(device, pixelShaderByteCode);

            GeometryShader = new GeometryShader(device, geometryshaderbytecode);

            //new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
            //new InputElement("NORMAL", 0, Format.R32G32B32_Float, 12, 0),
            //new InputElement("TANGENT", 0, Format.R32G32B32_Float, 24, 0),
            //new InputElement("BINORMAL", 0, Format.R32G32B32_Float, 36, 0),
            //new InputElement("TEXCOORD", 0, Format.R32G32_Float, 48, 0)

            arrayOfMatrixBuff = new DMatrixBuffer[1];


            InputElement[] inputElements = new InputElement[]
            {
                    new InputElement()
                    {
                        SemanticName = "POSITION",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset = 0,
                        Classification =InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "COLOR",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification =InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "NORMAL",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification =InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    /* new InputElement()
                    {
                        SemanticName = "TANGENT",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification =InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "BINORMAL",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification =InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },*/
                    new InputElement()
                    {
                        SemanticName = "TEXCOORD",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification =InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION",
                        SemanticIndex = 1,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 1,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1,
                    },

                    /*new InputElement()
                    {
                        SemanticName = "COLOR",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification =InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "NORMAL",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification =InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "TEXCOORD",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification =InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },


                   new InputElement()
                    {
                        SemanticName = "POSITION",
                        SemanticIndex = 1,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 1,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1,
                    },*/
            };

            // Create the vertex input the layout. Kin dof like a Vertex Declaration.
             Layout = new InputLayout(device, ShaderSignature.GetInputSignature(vertexShaderByteCode), inputElements);

            //instances = new SC_VR_Chunk.DInstanceType[mapWidth * mapHeight * mapDepth];

            this._color = color;
            this._sizeX = xi;
            this._sizeY = yi;
            this._sizeZ = zi;

            this._chunkPos = pos;

            VertexCount = 1;
            // Set number of vertices in the index array.
            IndexCount = 3;

            int[] somemap;

            arrayOfInstanceVertex = new Vector4[somearraycount][];
            arrayOfInstanceIndices = new int[somearraycount][];
            arrayOfInstanceNormals = new Vector3[somearraycount][];
            arrayOfInstanceTexturesCoordinates = new Vector2[somearraycount][];

            InstanceCount = somearraycount;
            instances = new DInstanceType[InstanceCount];

            //Vector3 position;
            //chunk newChunker;

            arrayOfDVertex = new DVertex[InstanceCount][];
            DVertex[] arrayOfD;// = new DVertex[];

            arrayofchunks = new sccstriglevelchunk[somearraycount];
            arrayofchunksmaps = new sccstriglevelchunkmaps[somearraycount];

            sccstriglevelchunkmaps sometrigchunkmap;
            sccstriglevelchunk newChunker;

            var enumerator1 = levelgen.typeoftiles.GetEnumerator();
            int ichunkleveltile = 0;
            while (enumerator1.MoveNext())
            {
                var currentTuile = enumerator1.Current;

                var chunkPos = currentTuile.Key;
                var typeofterraintile = currentTuile.Value;

                sometrigchunkmap = new sccstriglevelchunkmaps();
                newChunker = new sccstriglevelchunk();

                Vector3 newchunkpos = chunkPos;

                newchunkpos.X = newchunkpos.X * (tinyChunkWidth * planesize_);
                newchunkpos.Y = newchunkpos.Y * (tinyChunkHeight * planesize_);
                newchunkpos.Z = newchunkpos.Z * (tinyChunkDepth * planesize_);

                //chunkPos.X *= 2.0f;
                //chunkPos.Y *= 2.0f;
                //chunkPos.Z *= 2.0f;

                //int[] tester = 
                sometrigchunkmap.buildchunkmaps(newchunkpos, out somemap, planeSize, levelgen, chunkPos, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, typeofterraintile, this);      
                
                arrayofchunksmaps[ichunkleveltile] = sometrigchunkmap;

                arrayofchunks[ichunkleveltile] = newChunker;
                arrayofchunks[ichunkleveltile].chunkPos = chunkPos;
                arrayofchunks[ichunkleveltile].map = somemap;

                ichunkleveltile++;
            }
            

            
            //Vector3 someoffset = new Vector3(2,2,2) * 0.15f;
            enumerator1 = levelgen.typeoftiles.GetEnumerator();
            ichunkleveltile = 0;

            while (enumerator1.MoveNext())
            {
                var currentTuile = enumerator1.Current;
                var chunkPos = currentTuile.Key;

                Vector3 newchunkpos = chunkPos;

                newchunkpos.X = newchunkpos.X * (tinyChunkWidth * planesize_);
                newchunkpos.Y = newchunkpos.Y * (tinyChunkHeight * planesize_);
                newchunkpos.Z = newchunkpos.Z * (tinyChunkDepth * planesize_);

                //chunkPos.X *= 2.0f;
                //chunkPos.Y *= 2.0f;
                //chunkPos.Z *= 2.0f;

                //Console.WriteLine(chunkPos);

                var typeofterraintile = currentTuile.Value;
                //, out normals, out texturesCoordinates                
                int[] tester = arrayofchunks[ichunkleveltile].startBuildingArray(newchunkpos, out vertexArray0, out indicesArray0, out somemap, out arrayOfD, planeSize, levelgen, chunkPos, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, typeofterraintile, this, arrayofchunksmaps[ichunkleveltile].map);

                //arrayOfInstanceVertex[ichunkleveltile] = vertexArray0; //new Vector4(vertexArray0[v].X, vertexArray0[v].Y, vertexArray0[v].Z, 1);
                arrayOfInstanceIndices[ichunkleveltile] = indicesArray0;
                arrayOfDVertex[ichunkleveltile] = arrayOfD;
                //arrayOfInstanceNormals[ichunkleveltile] = normals;
                //arrayOfInstanceTexturesCoordinates[ichunkleveltile] = texturesCoordinates;

                //instances[ichunkleveltile] = new Vector4[1];
                //instances[ichunkleveltile][0] = new Vector4(position.X, position.Y, position.Z, 1);
                //Console.WriteLine(newchunkpos);

                //Console.WriteLine(chunkPos);

                instances[ichunkleveltile] = new DInstanceType()
                {
                    position = newchunkpos,
                };
             
                ichunkleveltile++;
            }

            /*
            for (int i = 0; i < arrayOfDVertex.Length; i++)
            {
                for (int v = 0; v < arrayOfDVertex[v].Length; v++)
                {

                    arrayOfDVertex[i][v].position.X += arrayofchunks[i].chunkPos.X * (tinyChunkWidth * planesize_);
                    arrayOfDVertex[i][v].position.Y += arrayofchunks[i].chunkPos.Y * (tinyChunkHeight * planesize_);
                    arrayOfDVertex[i][v].position.Z += arrayofchunks[i].chunkPos.Z * (tinyChunkDepth * planesize_);
                }
            }*/








            var contantBuffer = new SharpDX.Direct3D11.Buffer(sccs.scgraphics.scdirectx.D3D.device, Utilities.SizeOf<DMatrixBuffer>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);
            var InstanceBuffer = new SharpDX.Direct3D11.Buffer(sccs.scgraphics.scdirectx.D3D.device, Utilities.SizeOf<DInstanceType>() * instances.Length, ResourceUsage.Dynamic, BindFlags.VertexBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);
            
            Vector4 ambientColor = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);
            Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
            Vector4 lightDirection = new Vector4(1, 0, 0, 1.0f);
            Vector4 lightpos0 = new Vector4(1, 0, 0, 1.0f);
            Vector4 dirLight0 = new Vector4(1, 0, 0, 1.0f);
           
            lightBuffer = new DLightBuffer[1];
            lightBuffer[0] = new DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = dirLight0,
                specularcolor = lightpos0,
                //padding0 = 35,
                lightPosition = lightpos0,
                lightextras = lightpos0,
                //padding1 = 100
            };

            BufferDescription lightBufferDesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Utilities.SizeOf<DLightBuffer>(), // Must be divisable by 16 bytes, so this is equated to 32.
                BindFlags = BindFlags.ConstantBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            // Create the constant buffer pointer so we can access the vertex shader constant buffer from within this class.
            var ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(sccs.scgraphics.scdirectx.D3D.device, lightBufferDesc);

            //vertBuffers = new SharpDX.Direct3D11.Buffer[somearraycount];
            //colorBuffers = new SharpDX.Direct3D11.Buffer[somearraycount];
            indexBuffers = new SharpDX.Direct3D11.Buffer[somearraycount];
            instanceBuffers = new SharpDX.Direct3D11.Buffer[somearraycount];
            //normalBuffers = new SharpDX.Direct3D11.Buffer[somearraycount];
            //texBuffers = new SharpDX.Direct3D11.Buffer[somearraycount];
            dVertBuffers = new SharpDX.Direct3D11.Buffer[somearraycount];

            shaderOfChunk = new sclevelgenchunkshader(device, contantBuffer, Layout, VertexShader, PixelShader, GeometryShader, InstanceBuffer, ConstantLightBuffer, somearraycount); //InstanceBuffer













            //unLOADING CHUNK to XML
            //unLOADING CHUNK to XML
            /*string pathofrelease = Directory.GetCurrentDirectory();
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

            for (int i = 0; i < arrayofchunks.Length; i++)
            {                             
                //writer.WriteStartElement("bytemap"); //open 4
                writer.WriteStartElement("x" + arrayofchunks[i].chunkPos.X + "y" + arrayofchunks[i].chunkPos.Y + "z" + arrayofchunks[i].chunkPos.Z); //open 4

                int[] somemapp = arrayofchunksmaps[i].map;//
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
            for (int i = 0; i < arrayofchunks.Length; i++)
            {
                arrayofchunks[i].map = null;
                arrayofchunksmaps[i].map = null;
            }*/
        }




        //[StructLayout(LayoutKind.Sequential)]
        public struct DInstanceType
        {
            public Vector3 position;
            //public int[] chunkMap;
        };

        //[StructLayout(LayoutKind.Sequential)]
        public struct DColorType
        {
            public Vector4[] Color;
            //public int[] chunkMap;
        };

        DColorType[] arrayOfColor;

        public int InstanceCount = 0;

        Vector4[] vertexArray0;
        int[] indicesArray0;

        Vector3[] normals;
        Vector2[] texturesCoordinates;


        Vector4[] vertexArray => vertexArray0;
        int[] indicesArray => indicesArray0;

        // Structures.
        [StructLayout(LayoutKind.Sequential)]
        public struct DVertex
        {
            public Vector4 position;
            public Vector4 color;
            public Vector3 normal;
            //public Vector4 tangent;
            //public Vector3 binormal;
            public Vector2 tex;

        }

        public Vector4 _color;


        //public static int instanceCounter = 0;
        public int instanceCounter { get; set; }
        public byte[] map { get; set; }
        // Methods.
        public Vector3 _chunkPos { get; set; }


        /*
        public mainChunk getChunk(int x, int y, int z)
        {
            if ((x < -PlanetChunkWidth_L) || (y < -PlanetChunkHeight_L) || (z < -PlanetChunkDepth_L) || (x >= PlanetChunkWidth_R + 1) || (y >= (PlanetChunkHeight_R + 1)) || (z >= (PlanetChunkDepth_R + 1)))
            {
                return null;
            }

            if (x < 0)
            {
                x *= -1;
                x = (mapWidth) + x;
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
            }

            int _index = x + (mapWidth) * (y + (PlanetChunkHeight_L + PlanetChunkHeight_R + 1) * z);

            return blockers[_index];

            //return map[_index] == 0;
            /*if ((x < -planetwidth) || (y < -planetheight) || (z < -planetdepth) || (y >= planetwidth) || (x >= planetheight) || (z >= planetdepth))
            {
                return null;
            }

            return blockers[x, y, z];*/




        /*if ((x < -planetwidth) || (y < -planetheight) || (z < -planetdepth) || (y >= planetwidth) || (x >= planetheight) || (z >= planetdepth))
        {
            return null;
        }
        if (blockers[x, y, z] == null)
        {
            return null;
        }
        return blockers[x, y, z];
    }*/






        /*public sccstriglevelchunk getChunk(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < mapWidth && _y < mapHeight && _z < mapDepth)
            {
                return arrayofchunks[_x + mapWidth * (_y + mapHeight * _z)]; //_chunkArray
            }
            return null;
        }*/

        /*
        public sccstriglevelchunk getChunk(float x, float y, float z,Vector3 chunkPos)
        {
            float x0 = (float)(Math.Round(chunkPos.X * 10) / 10);
            float y0 = (float)(Math.Round(chunkPos.Y * 10) / 10);
            float z0 = (float)(Math.Round(chunkPos.Z * 10) / 10);



            var enumerator0 = arrayofchunks.GetEnumerator();
            for (int i = 0; i < arrayofchunks.Length; i++)
            {




                if (chunkPos.X == arrayofchunks[i].chunkPos.X && chunkPos.Y == arrayofchunks[i].chunkPos.Y && chunkPos.Z == arrayofchunks[i].chunkPos.Z)
                {
                    return arrayofchunks[i];
                }
                /*
                if (x >= 0 && y >= 0 && z >= 0 && x < mapWidth && y < mapHeight && z < mapDepth)
                {
                    
                }
               
            }

            return null;

            /*while (enumerator0.MoveNext())
            {
                var tls0 = enumerator0.Current;

                if ((x < tls0.Value.X) || y < tls0.Value.Y || (z < tls0.Value.Z) || (x >= (tls0.Value.X) + 10) || (y >= (tls0.Value.Y) + 10) || (z >= tls0.Value.Z + 10))
                {
                    continue;
                }
                return tls0.Key;
            }
            return null;
        }*/


        public sccstriglevelchunk getChunk(float x, float y, float z, out int arrayindex) //, Vector3 chunkPos
        {

            //var somevalueindict = arrayofchunks.Where(xi => xi.chunkPos == new Vector3(x, y, z)).Select().ToArray();

            /*var query = arrayofchunks.Where(xi => xi.chunkPos == new Vector3(x, y, z)).Select((somevar, index) => new { index});


            if (query != null)
            {
                var someenum = query.GetEnumerator();

                while (someenum.MoveNext())
                {
                    var tls0 = someenum.Current;
                    return arrayofchunks[tls0.index];

                }
                return null;
            }
            return null;*/




            float x0 = (float)(Math.Round(x * 10.0f) / 10.0f);
            float y0 = (float)(Math.Round(y * 10.0f) / 10.0f);
            float z0 = (float)(Math.Round(z * 10.0f) / 10.0f);

            //var enumerator0 = sclevelgenchunk.arrayofchunks.GetEnumerator();
            for (int i = 0; i < arrayofchunks.Length; i++)
            {

                float x1 = (float)(Math.Round(arrayofchunks[i].chunkPos.X * 10.0f) / 10.0f);
                float y1 = (float)(Math.Round(arrayofchunks[i].chunkPos.Y * 10.0f) / 10.0f);
                float z1 = (float)(Math.Round(arrayofchunks[i].chunkPos.Z * 10.0f) / 10.0f);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    arrayindex = i;
                    return arrayofchunks[i];
                }

                //if (x >= 0 && y >= 0 && z >= 0 && x < mapWidth && y < mapHeight && z < mapDepth)
                //{
                //    
                //}

            }

            arrayindex = -1;
            return null;

            /*while (enumerator0.MoveNext())
            {
                var tls0 = enumerator0.Current;

                if ((x < tls0.Value.X) || y < tls0.Value.Y || (z < tls0.Value.Z) || (x >= (tls0.Value.X) + 10) || (y >= (tls0.Value.Y) + 10) || (z >= tls0.Value.Z + 10))
                {
                    continue;
                }
                return tls0.Key;
            }
            return null;*/
        }


        private bool InitializeBuffer(SharpDX.Direct3D11.Device device)
        {
            try
            {



                


                /*VertexCount = vertexArray0.Length;
                IndexCount = indicesArray.Length;

                Vertices = new DVertex[VertexCount];

                for (int v = 0; v < vertexArray0.Length; v++)
                {
                    Vertices[v] = new DVertex()
                    {
                        position = vertexArray0[v],
                        color = _color,
                    };
                }
                indices = indicesArray;*/






                //Set number of vertices in the vertex array.
                /*VertexCount = 4;
                // Set number of vertices in the index array.
                IndexCount = 6;


                // Create the vertex array and load it with data.
                Vertices = new[]
                 {
                     //new DVertex()
                     //{
                     //    position = new Vector3(-1*_sizeX, -1*_sizeY, 1*_sizeZ),
                     //    color = _color,                    
                     //},
                     new DVertex()
                     {
                         position = new Vector4(0*_sizeX, 1*_sizeY, 1*_sizeZ,1),
                         color = _color,
                         //texture = new Vector2(0, 1),
                     },
                     //new DVertex()
                     //{
                     //    position = new Vector3(1*_sizeX, -1*_sizeY, 1*_sizeZ),
                     //    color = _color,
                     //},
                     new DVertex()
                     {
                         position = new Vector4(1*_sizeX, 1*_sizeY, 1*_sizeZ,1),
                         color = _color,
                         //texture = new Vector2(0, 1),
                     },

                     //new DVertex()
                     //{
                     //    position = new Vector3(-1*_sizeX, -1*_sizeY, -1*_sizeZ),
                     //    color = _color,
                     //},
                     new DVertex()
                     {
                         position = new Vector4(0*_sizeX, 1*_sizeY, 0*_sizeZ,1),
                         color = _color,
                         //texture = new Vector2(0, 1),
                     },
                     //new DVertex()
                     //{
                     //    position = new Vector3(1*_sizeX, -1*_sizeY, -1*_sizeZ),
                     //    color = _color,
                     //},
                     new DVertex()
                     {
                         position = new Vector4(1*_sizeX, 1*_sizeY, 0*_sizeZ,1),
                         color = _color,
                         //texture = new Vector2(0, 1),
                     },
                 };

                indices = new int[]
                {
                     2, // Bottom left.
                     1, // Top middle.
                     0,  // Bottom right.
                     1,
                     2,
                     3,
                };*/



                /*// Create Indicies to load into the IndexBuffer.
                indices = new int[]
                {
                     0, // Bottom left.
                     1, // Top middle.
                     2,  // Bottom right.
                     3,
                     2,
                     1,

                     1,
                     5,
                     3,
                     7,
                     3,
                     5,

                     2,
                     3,
                     6,
                     7,
                     6,
                     3,

                     6,
                     7,
                     4,
                     5,
                     4,
                     7,

                     4,
                     5,
                     0,
                     1,
                     0,
                     5,

                     4,
                     0,
                     6,
                     2,
                     6,
                     0
                 };*/

                
                //VertexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, Vertices, Utilities.SizeOf<DVertex>() * Vertices.Length, ResourceUsage.Dynamic, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

                //IndexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, indices);

                //InstanceBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, instances, Utilities.SizeOf<DInstanceType>() * instances.Length, ResourceUsage.Dynamic, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Vector4[][] arrayOfInstanceVertex { get; set; }
        public int[][] arrayOfInstanceIndices { get; set; }


        public Vector3[][] arrayOfInstanceNormals { get; set; }
        public Vector2[][] arrayOfInstanceTexturesCoordinates { get; set; }



        private void ShutDownBuffers()
        {
            InstanceBuffer?.Dispose();
            InstanceBuffer = null;
        }
    }
}