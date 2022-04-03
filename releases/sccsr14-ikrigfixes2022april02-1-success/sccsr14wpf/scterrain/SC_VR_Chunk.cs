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
    public class SC_VR_Chunk
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

        public DVertex[][] arrayOfDVertex { get; set; }
        public DInstanceType[] instances { get; set; }
        // Constructor


        VertexShader VertexShader;
        PixelShader PixelShader;
        GeometryShader GeometryShader;
        InputLayout Layout;

        public SC_VR_Chunk_Shader shaderOfChunk;
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
            public SharpDX.Direct3D11.Buffer instanceBuffer;
            public Vector4[][] arrayOfInstanceVertex;
            public DInstanceType[] arrayOfInstancePos;
            public int[][] arrayOfInstanceIndices;
            public Vector3[][] arrayOfInstanceNormals;
            public Vector2[][] arrayOfInstanceTextureCoordinates;
            public Vector4[][] arrayOfInstanceColors;
            public DVertex[][] dVertexData;

            public SharpDX.Direct3D11.Device Device;
            public Matrix worldMatrix;
            public Matrix viewMatrix;
            public Matrix projectionMatrix;
            //public DShaderManager shaderManager;
            public SC_VR_Chunk_Shader chunkShader;
            public DMatrixBuffer[] matrixBuffer;
            public DLightBuffer[] lightBuffer;
            public SharpDX.Direct3D11.Buffer[] vertBuffers;
            public SharpDX.Direct3D11.Buffer[] colorBuffers;
            public SharpDX.Direct3D11.Buffer[] indexBuffers;
            public SharpDX.Direct3D11.Buffer[] normalBuffers;
            public SharpDX.Direct3D11.Buffer[] texBuffers;
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

        [StructLayout(LayoutKind.Sequential)]
        public struct DLightBuffer
        {
            public Vector4 ambientColor;
            public Vector4 diffuseColor;
            public Vector3 lightDirection;
            public float padding; // Added extra padding so structure is a multiple of 16.
        }



        public float planeSize = 0.05f;

        public SC_VR_Chunk(SharpDX.Direct3D11.Device device, float xi, float yi, float zi, Vector4 color, int width, int height, int depth, Vector3 pos,int mapWidth_, int mapHeight_, int mapDepth_, int tinyChunkWidth_, int tinyChunkHeight_,int tinyChunkDepth_, int mapObjectInstanceWidth_, int mapObjectInstanceHeight_,int mapObjectInstanceDepth_, float planesize_) //,DInstanceType[] _instances
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



            var vsFileNameByteArray = sccsr14forms.Properties.Resources.textureTrig;
            var psFileNameByteArray = sccsr14forms.Properties.Resources.textureTrig1;
            var gsFileNameByteArray = sccsr14forms.Properties.Resources.geometryshader;

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

            arrayOfInstanceVertex = new Vector4[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceIndices = new int[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceNormals = new Vector3[mapWidth * mapHeight * mapDepth][];
            arrayOfInstanceTexturesCoordinates = new Vector2[mapWidth * mapHeight * mapDepth][];

            InstanceCount = mapWidth * mapHeight * mapDepth;
            instances = new DInstanceType[InstanceCount];

            Vector3 position;
            //chunk newChunker;

            sccstrigvertbuilderreduced newChunker;


            arrayOfDVertex = new DVertex[InstanceCount][];
            DVertex[] arrayOfD;// = new DVertex[];

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
                        newChunker = new sccstrigvertbuilderreduced();




                        //position.X = position.X + (_chunkPos.X ); //*1.05f
                        //position.Y = position.Y + (_chunkPos.Y );
                        //position.Z = position.Z + (_chunkPos.Z );

                        position.X *= ((tinyChunkWidth * planeSize));
                        position.Y *= ((tinyChunkHeight * planeSize));
                        position.Z *= ((tinyChunkDepth * planeSize));

                        //Console.WriteLine(_chunkPos.X);

                        position.X = position.X + (_chunkPos.X ); //*1.05f
                        position.Y = position.Y + (_chunkPos.Y );
                        position.Z = position.Z + (_chunkPos.Z );

                        //byte[] tester = newChunker.startBuildingArray(position, out vertexArray0, out indicesArray0, out mapperanus, out arrayOfD, out normals, out texturesCoordinates);
                        int[] tester = newChunker.startBuildingArray(position, out vertexArray0, out indicesArray0, out mapperanus, out arrayOfD, out normals, out texturesCoordinates, planeSize);




                        arrayOfInstanceVertex[xx + mapWidth * (yy + mapHeight * zz)] = vertexArray0; //new Vector4(vertexArray0[v].X, vertexArray0[v].Y, vertexArray0[v].Z, 1);
                        arrayOfInstanceIndices[xx + mapWidth * (yy + mapHeight * zz)] = indicesArray0;
                        arrayOfDVertex[xx + mapWidth * (yy + mapHeight * zz)] = arrayOfD;



                        arrayOfInstanceNormals[xx + mapWidth * (yy + mapHeight * zz)] = normals;
                        arrayOfInstanceTexturesCoordinates[xx + mapWidth * (yy + mapHeight * zz)] = texturesCoordinates;





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

            vertBuffers = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            colorBuffers = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            indexBuffers = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            instanceBuffers = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            normalBuffers = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            texBuffers = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];
            dVertBuffers = new SharpDX.Direct3D11.Buffer[mapWidth * mapHeight * mapDepth];



            shaderOfChunk = new SC_VR_Chunk_Shader(device, contantBuffer, Layout, VertexShader, PixelShader, GeometryShader, InstanceBuffer, ConstantLightBuffer); //InstanceBuffer




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