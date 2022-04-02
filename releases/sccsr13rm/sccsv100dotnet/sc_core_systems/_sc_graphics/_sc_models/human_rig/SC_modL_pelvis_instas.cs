using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

//using _sc_core_systems.SC_Graphics.SC_Textures.SC_VR_Touch_Textures;

using System.Linq;
using System;



using Jitter.Collision;
using Jitter;
using Jitter.Dynamics;
using Jitter.DataStructures;
using Jitter.Collision.Shapes;



using System.Runtime.InteropServices;


using _sc_core_systems.SC_Graphics;

namespace _sc_core_systems.SC_Graphics
{
    public class SC_modL_pelvis_instas : ITransform, IComponent
    {
        public ITransform transform { get; private set; }
        IComponent ITransform.Component
        {
            get => component;
        }
        IComponent component;
        RigidBody IComponent.rigidbody { get; set; }

        SoftBody IComponent.softbody { get; set; }

        public Matrix _POSITION { get; set; }

        public float RotationY { get; set; }
        public float RotationX { get; set; }
        public float RotationZ { get; set; }

        // Properties
        //private SharpDX.Direct3D11.Buffer VertexBuffer { get; set; }
        //private SharpDX.Direct3D11.Buffer IndexBuffer { get; set; }
        //private int VertexCount { get; set; }
        //public int IndexCount { get; set; }

        //private float _touchSize = 1f;

        //public SharpDX.Vector3 Position { get; set; }
        //public SharpDX.Quaternion Rotation { get; set; }
        //public SharpDX.Vector3 Forward { get; set; }

        //public DVertex[] Vertices { get; set; }

        /*[StructLayout(LayoutKind.Sequential)]
        public struct DVertex
        {
            public static int AppendAlignedElement = 12;
            public Vector3 position;
            public Vector4 color;
            public Vector3 normal;
        }*/

        /*[StructLayout(LayoutKind.Sequential)]
        public struct DVertex
        {
            public Vector3 position;
            //public Vector2 texture;
            public Vector4 color;
            public Vector3 normal;
        };*/





        /*[StructLayout(LayoutKind.Sequential)]
        public struct DMatrixBuffer
        {
            public Matrix world;
            public Matrix view;
            public Matrix projection;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DInstanceType
        {
            public Vector3 position;
        };*/
        //public int InstanceCount { get; private set; }

        //private SharpDX.Direct3D11.Buffer InstanceBuffer { get; set; }


        float _tileSize = 0;
        int _divX;
        int _divY;


        float _a;
        float _r;
        float _g;
        float _b;
        // Variables
        private int m_TerrainWidth, m_TerrainHeight;

        public Vector4 _color;



        //public DTextureShader _this_object_texture_shader { get; set; }

        /*//LIGHTS
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
            public float padding; // Added extra padding so structure is a multiple of 16.
        }

        DLightBuffer[] _DLightBuffer = new DLightBuffer[1];
        */


        private float _sizeX = 0;
        private float _sizeY = 0;
        private float _sizeZ = 0;




        // Constructor
        public SC_modL_pelvis_instas()
        {
            transform = this;
            component = this;

            //this._color = color;
            //this._sizeX = _sizeX;
            //this._sizeY = _sizeY;
            //this._sizeZ = _sizeZ;

            //_tileSize = tileSize;
            // Manually set the width and height of the terrain.
            //m_TerrainWidth = width;
           // m_TerrainHeight = height;

            //this._divX = divX;
            //this._divY = divY;

            //this._a = color.W;
            //this._r = color.X;
            //this._g = color.Y;
            //this._b = color.Z;
        }

        /*public bool Initialize(SC_Console_DIRECTX D3D, int width, int height, float tileSize, int divX, int divY,float _sizeX, float _sizeY, float _sizeZ, Vector4 color,int instX, int instY, int instZ, IntPtr windowsHandle)
        {
            //_POSITION = 


            transform = this;
            component = this;
   
            this._color = color;
            this._sizeX = _sizeX;
            this._sizeY = _sizeY;
            this._sizeZ = _sizeZ;

            _tileSize = tileSize;
            // Manually set the width and height of the terrain.
            m_TerrainWidth = width;
            m_TerrainHeight = height;

            this._divX = divX;
            this._divY = divY;

            this._a = color.W;
            this._r = color.X;
            this._g = color.Y;
            this._b = color.Z;

            return true;
        }*/
    }
}













/*new DVertex()
                   {
                       position = new Vector3(-1*_sizeX, -1*_sizeY, 1*_sizeZ),
                       color = _color,
                   },
                   new DVertex()
                   {
                       position = new Vector3(-1*_sizeX, 1*_sizeY, 1*_sizeZ),
                       color = _color,
                   },
                   new DVertex()
                   {
                       position = new Vector3(1*_sizeX, -1*_sizeY, 1*_sizeZ),
                       color = _color,
                   },
                   new DVertex()
                   {
                       position = new Vector3(1*_sizeX, 1*_sizeY, 1*_sizeZ),
                       color = _color,
                   },


                   new DVertex()
                   {
                       position = new Vector3(-1*_sizeX, -1*_sizeY, -1*_sizeZ),
                       color = _color,
                   },
                   new DVertex()
                   {
                       position = new Vector3(-1*_sizeX, 1*_sizeY, -1*_sizeZ),
                       color = _color,
                   },
                   new DVertex()
                   {
                       position = new Vector3(1*_sizeX, -1*_sizeY, -1*_sizeZ),
                       color = _color,
                   },
                   new DVertex()
                   {
                       position = new Vector3(1*_sizeX, 1*_sizeY, -1*_sizeZ),
                       color = _color,
                   },*/
