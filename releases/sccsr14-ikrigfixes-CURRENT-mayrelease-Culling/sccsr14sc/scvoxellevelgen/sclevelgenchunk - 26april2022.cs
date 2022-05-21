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

namespace sccs
{
    public class sclevelgenchunk
    {
        

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


        public DInstanceType[] instances { get; set; }
        // Constructor


        VertexShader VertexShader;
        PixelShader PixelShader;
        GeometryShader GeometryShader;
        InputLayout Layout;

        public sclevelgenchunkshader shaderOfChunk0;
        public sclevelgenchunkshader shaderOfChunk1;
        public sclevelgenchunkshader shaderOfChunk2;
        public sclevelgenchunkshader shaderOfChunk3;
        public sclevelgenchunkshader shaderOfChunk4;
        public sclevelgenchunkshader shaderOfChunk5;


        public DMatrixBuffer[] arrayOfMatrixBuff;

        public SharpDX.Direct3D11.Buffer[] vertBuffers0;
        public SharpDX.Direct3D11.Buffer[] colorBuffers0;
        public SharpDX.Direct3D11.Buffer[] indexBuffers0;
        public SharpDX.Direct3D11.Buffer[] normalBuffers0;
        public SharpDX.Direct3D11.Buffer[] texBuffers0;
        public SharpDX.Direct3D11.Buffer[] dVertBuffers0;

        public SharpDX.Direct3D11.Buffer[] vertBuffers1;
        public SharpDX.Direct3D11.Buffer[] colorBuffers1;
        public SharpDX.Direct3D11.Buffer[] indexBuffers1;
        public SharpDX.Direct3D11.Buffer[] normalBuffers1;
        public SharpDX.Direct3D11.Buffer[] texBuffers1;
        public SharpDX.Direct3D11.Buffer[] dVertBuffers1;

        public SharpDX.Direct3D11.Buffer[] vertBuffers2;
        public SharpDX.Direct3D11.Buffer[] colorBuffers2;
        public SharpDX.Direct3D11.Buffer[] indexBuffers2;
        public SharpDX.Direct3D11.Buffer[] normalBuffers2;
        public SharpDX.Direct3D11.Buffer[] texBuffers2;
        public SharpDX.Direct3D11.Buffer[] dVertBuffers2;

        public SharpDX.Direct3D11.Buffer[] vertBuffers3;
        public SharpDX.Direct3D11.Buffer[] colorBuffers3;
        public SharpDX.Direct3D11.Buffer[] indexBuffers3;
        public SharpDX.Direct3D11.Buffer[] normalBuffers3;
        public SharpDX.Direct3D11.Buffer[] texBuffers3;
        public SharpDX.Direct3D11.Buffer[] dVertBuffers3;

        public SharpDX.Direct3D11.Buffer[] vertBuffers4;
        public SharpDX.Direct3D11.Buffer[] colorBuffers4;
        public SharpDX.Direct3D11.Buffer[] indexBuffers4;
        public SharpDX.Direct3D11.Buffer[] normalBuffers4;
        public SharpDX.Direct3D11.Buffer[] texBuffers4;
        public SharpDX.Direct3D11.Buffer[] dVertBuffers4;

        public SharpDX.Direct3D11.Buffer[] vertBuffers5;
        public SharpDX.Direct3D11.Buffer[] colorBuffers5;
        public SharpDX.Direct3D11.Buffer[] indexBuffers5;
        public SharpDX.Direct3D11.Buffer[] normalBuffers5;
        public SharpDX.Direct3D11.Buffer[] texBuffers5;
        public SharpDX.Direct3D11.Buffer[] dVertBuffers5;



        public SharpDX.Direct3D11.Buffer[] instanceBuffers;
        public DLightBuffer[] lightBuffer;


        public struct chunkData
        {
            public int startOnce;

            public Vector4[][] arrayOfInstanceVertex0;
            public int[][] arrayOfInstanceIndices0;
            public Vector3[][] arrayOfInstanceNormals0;
            public Vector2[][] arrayOfInstanceTextureCoordinates0;
            public Vector4[][] arrayOfInstanceColors0;
            public DVertex[][] dVertexData0;

            public Vector4[][] arrayOfInstanceVertex1;
            public int[][] arrayOfInstanceIndices1;
            public Vector3[][] arrayOfInstanceNormals1;
            public Vector2[][] arrayOfInstanceTextureCoordinates1;
            public Vector4[][] arrayOfInstanceColors1;
            public DVertex[][] dVertexData1;

            public Vector4[][] arrayOfInstanceVertex2;
            public int[][] arrayOfInstanceIndices2;
            public Vector3[][] arrayOfInstanceNormals2;
            public Vector2[][] arrayOfInstanceTextureCoordinates2;
            public Vector4[][] arrayOfInstanceColors2;
            public DVertex[][] dVertexData2;

            public Vector4[][] arrayOfInstanceVertex3;
            public int[][] arrayOfInstanceIndices3;
            public Vector3[][] arrayOfInstanceNormals3;
            public Vector2[][] arrayOfInstanceTextureCoordinates3;
            public Vector4[][] arrayOfInstanceColors3;
            public DVertex[][] dVertexData3;

            public Vector4[][] arrayOfInstanceVertex4;
            public int[][] arrayOfInstanceIndices4;
            public Vector3[][] arrayOfInstanceNormals4;
            public Vector2[][] arrayOfInstanceTextureCoordinates4;
            public Vector4[][] arrayOfInstanceColors4;
            public DVertex[][] dVertexData4;

            public Vector4[][] arrayOfInstanceVertex5;
            public int[][] arrayOfInstanceIndices5;
            public Vector3[][] arrayOfInstanceNormals5;
            public Vector2[][] arrayOfInstanceTextureCoordinates5;
            public Vector4[][] arrayOfInstanceColors5;
            public DVertex[][] dVertexData5;

            public SharpDX.Direct3D11.Buffer instanceBuffer;

            public DInstanceType[] arrayOfInstancePos;

            public SharpDX.Direct3D11.Device Device;
            public Matrix worldMatrix;
            public Matrix viewMatrix;
            public Matrix projectionMatrix;
            //public DShaderManager shaderManager;
            public sclevelgenchunkshader chunkShader0;
            public sclevelgenchunkshader chunkShader1;
            public sclevelgenchunkshader chunkShader2;
            public sclevelgenchunkshader chunkShader3;
            public sclevelgenchunkshader chunkShader4;
            public sclevelgenchunkshader chunkShader5;

            public DMatrixBuffer[] matrixBuffer;
            public DLightBuffer[] lightBuffer;



            public SharpDX.Direct3D11.Buffer[] vertBuffers0;
            public SharpDX.Direct3D11.Buffer[] colorBuffers0;
            public SharpDX.Direct3D11.Buffer[] indexBuffers0;
            public SharpDX.Direct3D11.Buffer[] normalBuffers0;
            public SharpDX.Direct3D11.Buffer[] texBuffers0;
            public SharpDX.Direct3D11.Buffer[] dVertBuffers0;


            public SharpDX.Direct3D11.Buffer[] vertBuffers1;
            public SharpDX.Direct3D11.Buffer[] colorBuffers1;
            public SharpDX.Direct3D11.Buffer[] indexBuffers1;
            public SharpDX.Direct3D11.Buffer[] normalBuffers1;
            public SharpDX.Direct3D11.Buffer[] texBuffers1;
            public SharpDX.Direct3D11.Buffer[] dVertBuffers1;


            public SharpDX.Direct3D11.Buffer[] vertBuffers2;
            public SharpDX.Direct3D11.Buffer[] colorBuffers2;
            public SharpDX.Direct3D11.Buffer[] indexBuffers2;
            public SharpDX.Direct3D11.Buffer[] normalBuffers2;
            public SharpDX.Direct3D11.Buffer[] texBuffers2;
            public SharpDX.Direct3D11.Buffer[] dVertBuffers2;


            public SharpDX.Direct3D11.Buffer[] vertBuffers3;
            public SharpDX.Direct3D11.Buffer[] colorBuffers3;
            public SharpDX.Direct3D11.Buffer[] indexBuffers3;
            public SharpDX.Direct3D11.Buffer[] normalBuffers3;
            public SharpDX.Direct3D11.Buffer[] texBuffers3;
            public SharpDX.Direct3D11.Buffer[] dVertBuffers3;


            public SharpDX.Direct3D11.Buffer[] vertBuffers4;
            public SharpDX.Direct3D11.Buffer[] colorBuffers4;
            public SharpDX.Direct3D11.Buffer[] indexBuffers4;
            public SharpDX.Direct3D11.Buffer[] normalBuffers4;
            public SharpDX.Direct3D11.Buffer[] texBuffers4;
            public SharpDX.Direct3D11.Buffer[] dVertBuffers4;


            public SharpDX.Direct3D11.Buffer[] vertBuffers5;
            public SharpDX.Direct3D11.Buffer[] colorBuffers5;
            public SharpDX.Direct3D11.Buffer[] indexBuffers5;
            public SharpDX.Direct3D11.Buffer[] normalBuffers5;
            public SharpDX.Direct3D11.Buffer[] texBuffers5;
            public SharpDX.Direct3D11.Buffer[] dVertBuffers5;





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


        sccstriglevelchunk newChunkertop;
        sccstriglevelchunk newChunkerleft;
        sccstriglevelchunk newChunkerright;
        sccstriglevelchunk newChunkerbottom;
        sccstriglevelchunk newChunkerfront;
        sccstriglevelchunk newChunkerback;

        public sclevelgenchunk(SharpDX.Direct3D11.Device device, float xi, float yi, float zi, Vector4 color, int width, int height, int depth, Vector3 pos,int mapWidth_, int mapHeight_, int mapDepth_, int tinyChunkWidth_, int tinyChunkHeight_,int tinyChunkDepth_, int mapObjectInstanceWidth_, int mapObjectInstanceHeight_,int mapObjectInstanceDepth_, float planesize_, LevelGenerator4 levelgen) //,DInstanceType[] _instances
        {

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

            int[] mapperanus;

            arrayOfInstanceVertextopface = new Vector4[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceIndicestopface = new int[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceNormalstopface = new Vector3[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceTexturesCoordinatestopface = new Vector2[mapWidth * mapHeight * mapDepth][];
            arrayOfDVertextopface = new DVertex[mapWidth * mapHeight * mapDepth][];

            arrayOfInstanceVertexbackface = new Vector4[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceIndicesbackface = new int[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceNormalsbackface = new Vector3[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceTexturesCoordinatesbackface = new Vector2[mapWidth * mapHeight * mapDepth][];
            arrayOfDVertexbackface = new DVertex[mapWidth * mapHeight * mapDepth][];

            arrayOfInstanceVertexleftface = new Vector4[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceIndicesleftface = new int[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceNormalsleftface = new Vector3[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceTexturesCoordinatesleftface = new Vector2[mapWidth * mapHeight * mapDepth][];
            arrayOfDVertexleftface = new DVertex[mapWidth * mapHeight * mapDepth][];

            arrayOfInstanceVertexrightface = new Vector4[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceIndicesrightface = new int[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceNormalsrightface = new Vector3[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceTexturesCoordinatesrightface = new Vector2[mapWidth * mapHeight * mapDepth][];
            arrayOfDVertexrightface = new DVertex[mapWidth * mapHeight * mapDepth][];

            arrayOfInstanceVertexbottomface = new Vector4[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceIndicesbottomface = new int[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceNormalsbottomface = new Vector3[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceTexturesCoordinatesbottomface = new Vector2[mapWidth * mapHeight * mapDepth][];
            arrayOfDVertexbottomface = new DVertex[mapWidth * mapHeight * mapDepth][];

            arrayOfInstanceVertexfrontface = new Vector4[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceIndicesfrontface = new int[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceNormalsfrontface = new Vector3[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceTexturesCoordinatesfrontface = new Vector2[mapWidth * mapHeight * mapDepth][];
            arrayOfDVertexfrontface = new DVertex[mapWidth * mapHeight * mapDepth][];







            InstanceCount = mapWidth * mapHeight * mapDepth;
            instances = new DInstanceType[InstanceCount];

            Vector3 position;
            //chunk newChunker;

            


        
            DVertex[] arrayOfD0;// = new DVertex[];
            DVertex[] arrayOfD1;// = new DVertex[];
            DVertex[] arrayOfD2;// = new DVertex[];
            DVertex[] arrayOfD3;// = new DVertex[];
            DVertex[] arrayOfD4;// = new DVertex[];
            DVertex[] arrayOfD5;// = new DVertex[];
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    for (int z = 0; z < mapDepth; z++)
                    {
                        var xx = x;
                        var yy = y;// (mapHeight - 1) - y;
                        var zz = z;

                        position = new Vector3(x, y, z);
                        //newChunker = new chunk();
                        newChunkertop = new sccstriglevelchunk(0);
                        newChunkerbottom = new sccstriglevelchunk(1);
                        newChunkerleft = new sccstriglevelchunk(2);
                        newChunkerright = new sccstriglevelchunk(3);
                        newChunkerfront = new sccstriglevelchunk(4);
                        newChunkerback = new sccstriglevelchunk(5);
              


                        //position.X = position.X + (_chunkPos.X ); //*1.05f
                        //position.Y = position.Y + (_chunkPos.Y );
                        //position.Z = position.Z + (_chunkPos.Z );

                        position.X *= ((tinyChunkWidth * planeSize));
                        position.Y *= ((tinyChunkHeight * planeSize));
                        position.Z *= ((tinyChunkDepth * planeSize));

                        //Console.WriteLine(_chunkPos.X);

                        position.X = position.X + (_chunkPos.X * tinyChunkWidth * planeSize); //*1.05f
                        position.Y = position.Y + (_chunkPos.Y * tinyChunkHeight * planeSize);
                        position.Z = position.Z + (_chunkPos.Z * tinyChunkDepth * planeSize);

                        //byte[] tester = newChunker.startBuildingArray(position, out vertexArray0, out indicesArray0, out mapperanus, out arrayOfD, out normals, out texturesCoordinates);
                        int[] tester = newChunkertop.startBuildingArray(position, out vertexArray0, out indicesArray0, out mapperanus, out arrayOfD0, out normals0, out texturesCoordinates0, planeSize, levelgen, _chunkPos, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth);
                        /*tester = newChunkerleft.startBuildingArray(position, out vertexArray1, out indicesArray1, out mapperanus, out arrayOfD1, out normals1, out texturesCoordinates1, planeSize, levelgen, _chunkPos, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth);
                        tester = newChunkerright.startBuildingArray(position, out vertexArray2, out indicesArray2, out mapperanus, out arrayOfD2, out normals2, out texturesCoordinates2, planeSize, levelgen, _chunkPos, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth);
                        tester = newChunkerbottom.startBuildingArray(position, out vertexArray3, out indicesArray3, out mapperanus, out arrayOfD3, out normals3, out texturesCoordinates3, planeSize, levelgen, _chunkPos, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth);
                        tester = newChunkerfront.startBuildingArray(position, out vertexArray4, out indicesArray4, out mapperanus, out arrayOfD4, out normals4, out texturesCoordinates4, planeSize, levelgen, _chunkPos, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth);
                        tester = newChunkerback.startBuildingArray(position, out vertexArray5, out indicesArray5, out mapperanus, out arrayOfD5, out normals5, out texturesCoordinates5, planeSize, levelgen, _chunkPos, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth);

                        */


                        arrayOfInstanceVertextopface[xx + mapWidth * (yy + mapHeight * zz)] = vertexArray0; //new Vector4(vertexArray0[v].X, vertexArray0[v].Y, vertexArray0[v].Z, 1);
                        arrayOfInstanceIndicestopface[xx + mapWidth * (yy + mapHeight * zz)] = indicesArray0;
                        arrayOfInstanceNormalstopface[xx + mapWidth * (yy + mapHeight * zz)] = normals0;
                        arrayOfInstanceTexturesCoordinatestopface[xx + mapWidth * (yy + mapHeight * zz)] = texturesCoordinates0;
                        arrayOfDVertextopface[xx + mapWidth * (yy + mapHeight * zz)] = arrayOfD0;


                        /*
                        arrayOfInstanceVertexbottomface[xx + mapWidth * (yy + mapHeight * zz)] = vertexArray1; //new Vector4(vertexArray0[v].X, vertexArray0[v].Y, vertexArray0[v].Z, 1);
                        arrayOfInstanceIndicesbottomface[xx + mapWidth * (yy + mapHeight * zz)] = indicesArray1;
                        arrayOfInstanceNormalsbottomface[xx + mapWidth * (yy + mapHeight * zz)] = normals1;
                        arrayOfInstanceTexturesCoordinatesbottomface[xx + mapWidth * (yy + mapHeight * zz)] = texturesCoordinates1;
                        arrayOfDVertexbottomface[xx + mapWidth * (yy + mapHeight * zz)] = arrayOfD1;

                        

                        arrayOfInstanceVertexleftface[xx + mapWidth * (yy + mapHeight * zz)] = vertexArray2; //new Vector4(vertexArray0[v].X, vertexArray0[v].Y, vertexArray0[v].Z, 1);
                        arrayOfInstanceIndicesleftface[xx + mapWidth * (yy + mapHeight * zz)] = indicesArray2;
                        arrayOfInstanceNormalsleftface[xx + mapWidth * (yy + mapHeight * zz)] = normals2;
                        arrayOfInstanceTexturesCoordinatesleftface[xx + mapWidth * (yy + mapHeight * zz)] = texturesCoordinates2;
                        arrayOfDVertexleftface[xx + mapWidth * (yy + mapHeight * zz)] = arrayOfD2;



                        arrayOfInstanceVertexrightface[xx + mapWidth * (yy + mapHeight * zz)] = vertexArray3; //new Vector4(vertexArray0[v].X, vertexArray0[v].Y, vertexArray0[v].Z, 1);
                        arrayOfInstanceIndicesrightface[xx + mapWidth * (yy + mapHeight * zz)] = indicesArray3;
                        arrayOfInstanceNormalsrightface[xx + mapWidth * (yy + mapHeight * zz)] = normals3;
                        arrayOfInstanceTexturesCoordinatesrightface[xx + mapWidth * (yy + mapHeight * zz)] = texturesCoordinates3;
                        arrayOfDVertexrightface[xx + mapWidth * (yy + mapHeight * zz)] = arrayOfD3;



                        arrayOfInstanceVertexfrontface[xx + mapWidth * (yy + mapHeight * zz)] = vertexArray4; //new Vector4(vertexArray0[v].X, vertexArray0[v].Y, vertexArray0[v].Z, 1);
                        arrayOfInstanceIndicesfrontface[xx + mapWidth * (yy + mapHeight * zz)] = indicesArray4;
                        arrayOfInstanceNormalsfrontface[xx + mapWidth * (yy + mapHeight * zz)] = normals4;
                        arrayOfInstanceTexturesCoordinatesfrontface[xx + mapWidth * (yy + mapHeight * zz)] = texturesCoordinates4;
                        arrayOfDVertexfrontface[xx + mapWidth * (yy + mapHeight * zz)] = arrayOfD4;



                        arrayOfInstanceVertexbackface[xx + mapWidth * (yy + mapHeight * zz)] = vertexArray5; //new Vector4(vertexArray0[v].X, vertexArray0[v].Y, vertexArray0[v].Z, 1);
                        arrayOfInstanceIndicesbackface[xx + mapWidth * (yy + mapHeight * zz)] = indicesArray5;
                        arrayOfInstanceNormalsbackface[xx + mapWidth * (yy + mapHeight * zz)] = normals5;
                        arrayOfInstanceTexturesCoordinatesbackface[xx + mapWidth * (yy + mapHeight * zz)] = texturesCoordinates5;
                        arrayOfDVertexbackface[xx + mapWidth * (yy + mapHeight * zz)] = arrayOfD5;
                        */












                        //instances[xx + mapWidth * (yy + mapHeight * zz)] = new Vector4[1];
                        //instances[xx + mapWidth * (yy + mapHeight * zz)][0] = new Vector4(position.X, position.Y, position.Z, 1);

                        instances[xx + mapWidth * (yy + mapHeight * zz)] = new DInstanceType()
                        {
                            position = position,
                        };

                        /*= new DInstanceType()
                    {
                        position = position,
                    };*/

                    }
                }
            }

            /*for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    for (int z = 0; z < mapDepth; z++)
                    {
                        var xx = x;
                        var yy = y;//  (mapHeight - 1) - y;
                        var zz = z;


                        Vector3 position = new Vector3(x, y, z);

                        position.X *= ((tinyChunkWidth * planeSize));
                        position.Y *= ((tinyChunkHeight * planeSize));
                        position.Z *= ((tinyChunkDepth * planeSize));

                        position.X = position.X + (_chunkPos.X); //*1.05f
                        position.Y = position.Y + (_chunkPos.Y);
                        position.Z = position.Z + (_chunkPos.Z);

                        instances[xx + mapWidth * (yy + mapHeight * zz)] = new DInstanceType()
                        {
                            position = position,
                        };
                    }
                }
            }*/



            arrayOfMatrixBuff = new DMatrixBuffer[1];

            var contantBuffer = new SharpDX.Direct3D11.Buffer(sccs.scgraphics.scdirectx.D3D.device, Utilities.SizeOf<DMatrixBuffer>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);
            var InstanceBuffer = new SharpDX.Direct3D11.Buffer(sccs.scgraphics.scdirectx.D3D.device, Utilities.SizeOf<DInstanceType>() * instances.Length, ResourceUsage.Dynamic, BindFlags.VertexBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);
            
            
            /*
            Vector4 ambientColor = new Vector4(0.05f, 0.05f, 0.05f, 1.0f);
            Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
            Vector3 lightDirection = new Vector3(0, -1, 0);

            lightBuffer = new DLightBuffer[1];


            // Copy the lighting variables into the constant buffer.
            lightBuffer[0] = new DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding = 0
            };*/


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

            vertBuffers0 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            colorBuffers0 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            indexBuffers0 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            normalBuffers0 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            texBuffers0 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            dVertBuffers0 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];


            vertBuffers1 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            colorBuffers1 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            indexBuffers1 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            normalBuffers1 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            texBuffers1 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            dVertBuffers1 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];


            vertBuffers2 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            colorBuffers2 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            indexBuffers2 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            normalBuffers2 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            texBuffers2 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            dVertBuffers2 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];


            vertBuffers3 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            colorBuffers3 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            indexBuffers3 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            normalBuffers3 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            texBuffers3 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            dVertBuffers3 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];


            vertBuffers4 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            colorBuffers4 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            indexBuffers4 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            normalBuffers4 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            texBuffers4 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            dVertBuffers4 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];


            vertBuffers5 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            colorBuffers5 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            indexBuffers5 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            normalBuffers5 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            texBuffers5 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            dVertBuffers5 = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];

            instanceBuffers = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];

            shaderOfChunk0 = new sclevelgenchunkshader(device, contantBuffer, Layout, VertexShader, PixelShader, GeometryShader, InstanceBuffer, ConstantLightBuffer); //InstanceBuffer
            shaderOfChunk1 = new sclevelgenchunkshader(device, contantBuffer, Layout, VertexShader, PixelShader, GeometryShader, InstanceBuffer, ConstantLightBuffer); //InstanceBuffer
            shaderOfChunk2 = new sclevelgenchunkshader(device, contantBuffer, Layout, VertexShader, PixelShader, GeometryShader, InstanceBuffer, ConstantLightBuffer); //InstanceBuffer
            shaderOfChunk3 = new sclevelgenchunkshader(device, contantBuffer, Layout, VertexShader, PixelShader, GeometryShader, InstanceBuffer, ConstantLightBuffer); //InstanceBuffer
            shaderOfChunk4 = new sclevelgenchunkshader(device, contantBuffer, Layout, VertexShader, PixelShader, GeometryShader, InstanceBuffer, ConstantLightBuffer); //InstanceBuffer
            shaderOfChunk5 = new sclevelgenchunkshader(device, contantBuffer, Layout, VertexShader, PixelShader, GeometryShader, InstanceBuffer, ConstantLightBuffer); //InstanceBuffer











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

        Vector4[] vertexArray1;
        int[] indicesArray1;
        Vector4[] vertexArray2;
        int[] indicesArray2;
        Vector4[] vertexArray3;
        int[] indicesArray3;
        Vector4[] vertexArray4;
        int[] indicesArray4;
        Vector4[] vertexArray5;
        int[] indicesArray5;




        Vector3[] normals0;
        Vector2[] texturesCoordinates0;




        Vector3[] normals1;
        Vector2[] texturesCoordinates1;



        Vector3[] normals2;
        Vector2[] texturesCoordinates2;



        Vector3[] normals3;
        Vector2[] texturesCoordinates3;



        Vector3[] normals4;
        Vector2[] texturesCoordinates4;


        Vector3[] normals5;
        Vector2[] texturesCoordinates5;






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


        public DVertex[][] arrayOfDVertextopface { get; set; }
        public Vector4[][] arrayOfInstanceVertextopface { get; set; }
        public int[][] arrayOfInstanceIndicestopface { get; set; }
        public Vector3[][] arrayOfInstanceNormalstopface { get; set; }
        public Vector2[][] arrayOfInstanceTexturesCoordinatestopface { get; set; }

        public DVertex[][] arrayOfDVertexleftface{ get; set; }
        public Vector4[][] arrayOfInstanceVertexleftface { get; set; }
        public int[][] arrayOfInstanceIndicesleftface { get; set; }
        public Vector3[][] arrayOfInstanceNormalsleftface { get; set; }
        public Vector2[][] arrayOfInstanceTexturesCoordinatesleftface { get; set; }


        public DVertex[][] arrayOfDVertexrightface { get; set; }
        public Vector4[][] arrayOfInstanceVertexrightface { get; set; }
        public int[][] arrayOfInstanceIndicesrightface { get; set; }
        public Vector3[][] arrayOfInstanceNormalsrightface { get; set; }
        public Vector2[][] arrayOfInstanceTexturesCoordinatesrightface { get; set; }


        public DVertex[][] arrayOfDVertexfrontface { get; set; }
        public Vector4[][] arrayOfInstanceVertexfrontface { get; set; }
        public int[][] arrayOfInstanceIndicesfrontface { get; set; }
        public Vector3[][] arrayOfInstanceNormalsfrontface { get; set; }
        public Vector2[][] arrayOfInstanceTexturesCoordinatesfrontface { get; set; }


        public DVertex[][] arrayOfDVertexbackface { get; set; }
        public Vector4[][] arrayOfInstanceVertexbackface { get; set; }
        public int[][] arrayOfInstanceIndicesbackface { get; set; }
        public Vector3[][] arrayOfInstanceNormalsbackface { get; set; }
        public Vector2[][] arrayOfInstanceTexturesCoordinatesbackface { get; set; }


        public DVertex[][] arrayOfDVertexbottomface { get; set; }
        public Vector4[][] arrayOfInstanceVertexbottomface { get; set; }
        public int[][] arrayOfInstanceIndicesbottomface { get; set; }
        public Vector3[][] arrayOfInstanceNormalsbottomface { get; set; }
        public Vector2[][] arrayOfInstanceTexturesCoordinatesbottomface { get; set; }



        private void ShutDownBuffers()
        {
            InstanceBuffer?.Dispose();
            InstanceBuffer = null;
        }
    }
}