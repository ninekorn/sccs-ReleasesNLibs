using SharpDX;
using SharpDX.Direct3D11;
using System;

//using SC_SkYaRk_Clean.SC_Graphics.SC_Textures.SC_VR_Desktop_Screen_Textures;
//using SC_skYaRk_VR_V007.SC_Graphics.SC_Textures.SC_VR_Touch_Textures;
//using _sc_core_systems.SC_Graphics.SC_Grid;

using System.Runtime.InteropServices;


namespace _sc_core_systems.SC_Graphics.SC_ShaderManager
{
    public class DShaderManager                 // 77 lines
    {

        public _sc_voxel_spheroid_shader_final _sc_voxel_spheroid_shader_final { get; set; }
        public SC_cube_shader_final _this_object_texture_shader { get; set; }
        public _sc_spectrum_shader_final _spectrum_texture_shader { get; set; }
        public _sc_texture_shader TextureShader { get; set; }


        SC_cube.DLightBuffer[] _DLightBuffer = new SC_cube.DLightBuffer[1];
        _sc_spectrum.DLightBuffer[] _DLightBuffer_spectrum = new _sc_spectrum.DLightBuffer[1];
        _sc_voxel.DLightBuffer[] _DLightBuffer_voxel_spheroid = new _sc_voxel.DLightBuffer[1];



        Vector4 ambientColor = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);
        Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
        Vector3 lightDirection = new Vector3(1, 0, 0);
        Vector3 lightPosition = new Vector3(0, 0, 0);



        BufferDescription lightBufferDesc = new BufferDescription()
        {
            Usage = ResourceUsage.Dynamic,
            SizeInBytes = Utilities.SizeOf<_sc_voxel.DLightBuffer>(),
            BindFlags = BindFlags.ConstantBuffer,
            CpuAccessFlags = CpuAccessFlags.Write,
            OptionFlags = ResourceOptionFlags.None,
            StructureByteStride = 0
        };



        //public SC_Screen_Shader _shader_screen { get; set; }
        //DModelClass2.DLightBuffer[] _DLightBuffer_screen;

        /*_sc_voxel.DLightBuffer[] _DLightBuffer0;
        _sc_voxel.DLightBuffer[] _DLightBuffer1;
        _sc_voxel.DLightBuffer[] _DLightBuffer2;
        _sc_voxel.DLightBuffer[] _DLightBuffer4;
        _sc_voxel.DLightBuffer[] _DLightBuffer5;
        _sc_voxel.DLightBuffer[] _DLightBuffer6;
        _sc_voxel.DLightBuffer[] _DLightBuffer7;
        _sc_voxel.DLightBuffer[] _DLightBuffer8;
        _sc_voxel.DLightBuffer[] _DLightBuffer9;
        _sc_voxel.DLightBuffer[] _DLightBuffer10;
        _sc_voxel.DLightBuffer[] _DLightBuffer11;
        _sc_voxel.DLightBuffer[] _DLightBuffer12;
        _sc_voxel.DLightBuffer[] _DLightBuffer13;
        _sc_voxel.DLightBuffer[] _DLightBuffer14;
        _sc_voxel.DLightBuffer[] _DLightBuffer15;
        _sc_voxel.DLightBuffer[] _DLightBuffer16;
        _sc_voxel.DLightBuffer[] _DLightBuffer17;
        SC_modL_head.DLightBuffer[] _DLightBuffer18;


        _sc_voxel.DLightBuffer[] _DLightBuffer19;
        _sc_voxel.DLightBuffer[] _DLightBuffer20;
        _sc_voxel.DLightBuffer[] _DLightBuffer21;
        _sc_voxel.DLightBuffer[] _DLightBuffer22;*/







        _sc_voxel.DLightBuffer[] _DLightBuffer0;
        _sc_voxel.DLightBuffer[] _DLightBuffer1;
        _sc_voxel.DLightBuffer[] _DLightBuffer2;
        _sc_voxel.DLightBuffer[] _DLightBuffer4;
        _sc_voxel.DLightBuffer[] _DLightBuffer5;
        _sc_voxel.DLightBuffer[] _DLightBuffer6;
        _sc_voxel.DLightBuffer[] _DLightBuffer7;
        _sc_voxel.DLightBuffer[] _DLightBuffer8;
        _sc_voxel.DLightBuffer[] _DLightBuffer9;
        _sc_voxel.DLightBuffer[] _DLightBuffer10;
        _sc_voxel.DLightBuffer[] _DLightBuffer11;
        _sc_voxel.DLightBuffer[] _DLightBuffer12;
        _sc_voxel.DLightBuffer[] _DLightBuffer13;
        _sc_voxel.DLightBuffer[] _DLightBuffer14;
        _sc_voxel.DLightBuffer[] _DLightBuffer15;
        _sc_voxel.DLightBuffer[] _DLightBuffer16;
        _sc_voxel.DLightBuffer[] _DLightBuffer17;
        _sc_voxel.DLightBuffer[] _DLightBuffer18;


        _sc_voxel.DLightBuffer[] _DLightBuffer19;
        _sc_voxel.DLightBuffer[] _DLightBuffer20;
        _sc_voxel.DLightBuffer[] _DLightBuffer21;
        _sc_voxel.DLightBuffer[] _DLightBuffer22;









        // Properties
        //public SC_VR_Desktop_Screen_Shader TextureShader { get; set; }

        //public SC_VR_Touch_Shader touchShader { get; set; }
        //public DLightShader LightShader { get; set; }
        //public DBumpMapShader BumpMapShader { get; set; }

        // Methods


        //public SC_VR_Terrain_Shader terrainShader { get; set; }

        //public DColorShader colorShader { get; set; }

        //public DColorShader objectColorShader { get; set; }

        //public SC_VR_ICO_Shader icoColorShader { get; set; }




        //public SC_Cloth_Shader_Final TextureShaderCLOTH { get; set; }

        public _sc_voxel_spheroid_shader_final _SC_sdr_head { get; set; }




        public _sc_voxel_spheroid_shader_final _SC_sdr_lft_elbow_target { get; set; }
        public _sc_voxel_spheroid_shader_final _SC_sdr_lft_elbow_target_two { get; set; }
        public _sc_voxel_spheroid_shader_final _SC_sdr_lft_foot { get; set; }
        public _sc_voxel_spheroid_shader_final _SC_sdr_lft_hnd { get; set; }
        public _sc_voxel_spheroid_shader_final _SC_sdr_lft_shldr { get; set; }
        public _sc_voxel_spheroid_shader_final _SC_sdr_lower_left_arm { get; set; }
        public _sc_voxel_spheroid_shader_final _SC_sdr_lower_left_leg { get; set; }


        public _sc_voxel_spheroid_shader_final _SC_sdr_pelvis { get; set; }




        public _sc_voxel_spheroid_shader_final _SC_sdr_rght_elbow_target { get; set; }
        public _sc_voxel_spheroid_shader_final _SC_sdr_rght_elbow_target_two { get; set; }
        public _sc_voxel_spheroid_shader_final _SC_sdr_rght_foot { get; set; }
        public _sc_voxel_spheroid_shader_final _SC_sdr_rght_hnd { get; set; }
        public _sc_voxel_spheroid_shader_final _SC_sdr_rght_shldr { get; set; }
        public _sc_voxel_spheroid_shader_final _SC_sdr_lower_right_arm { get; set; }
        public _sc_voxel_spheroid_shader_final _SC_sdr_lower_right_leg { get; set; }




        public _sc_voxel_spheroid_shader_final _SC_sdr_upper_left_arm { get; set; }
        public _sc_voxel_spheroid_shader_final _SC_sdr_upper_left_leg { get; set; }

        public _sc_voxel_spheroid_shader_final _SC_sdr_upper_right_arm { get; set; }
        public _sc_voxel_spheroid_shader_final _SC_sdr_upper_right_leg { get; set; }


        public _sc_voxel_spheroid_shader_final _SC_sdr_torso { get; set; }

        /*//LIGHTS
        [StructLayout(LayoutKind.Explicit)]
        public struct DLightBuffer
        {
            [FieldOffset(0)]
            public Vector4 ambientColor; //16
            [FieldOffset(16)]
            public Vector4 diffuseColor; //16
            [FieldOffset(32)]
            public Vector3 lightDirection; //12
            [FieldOffset(44)]
            public float padding0;
            [FieldOffset(48)]
            public Vector3 lightPosition; //12
            [FieldOffset(60)]
            public float padding1;
        }*/






        _sc_voxel.DLightBuffer[] _DLightBuffer_cloth = new _sc_voxel.DLightBuffer[1];





        public bool Initialize(Device device, IntPtr windowsHandle) //, float x, float y, float z, Vector4 color,Matrix worldMatrix
        {
            _DLightBuffer[0] = new SC_cube.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };



            /*_DLightBuffer_cloth[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };


            TextureShaderCLOTH = new SC_Cloth_Shader_Final();
            SharpDX.Direct3D11.Buffer ConstantLightBufferCloth = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            TextureShaderCLOTH.Initialize(device, windowsHandle, ConstantLightBufferCloth, _DLightBuffer_cloth);
            */

            //touchShader = new SC_VR_Touch_Shader();
            //if (!touchShader.Initialize(device, windowsHandle))
            //    return false;




            //////////////////////
            //Texture that can be used on the cube shader if modifying the shader
            //////////////////////
            TextureShader = new _sc_texture_shader();
            if (!TextureShader.Initialize(device, windowsHandle))
            {
                return false;
            }
            //////////////////////
            //Texture that can be used on the cube shader if modifying the shader
            //////////////////////

            //////////////////////
            //SC PHYSICS CUBES
            //////////////////////
            _DLightBuffer[0] = new SC_cube.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };

            SharpDX.Direct3D11.Buffer ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _this_object_texture_shader = new SC_cube_shader_final();
            _this_object_texture_shader.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer);
            //////////////////////
            //SC PHYSICS CUBES
            //////////////////////





            //////////////////////
            //SC PHYSICS VOXEL SPHEROID
            //////////////////////
            _DLightBuffer_voxel_spheroid[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };

            SharpDX.Direct3D11.Buffer ConstantLightBuffer01 = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _sc_voxel_spheroid_shader_final = new _sc_voxel_spheroid_shader_final();
            _sc_voxel_spheroid_shader_final.Initialize(device, windowsHandle, ConstantLightBuffer01, _DLightBuffer_voxel_spheroid);
            //////////////////////
            //SC PHYSICS CUBES
            //////////////////////

            //////////////////////
            //SC PHYSICS SPECTRUM
            //////////////////////
            _DLightBuffer_spectrum[0] = new _sc_spectrum.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };

            SharpDX.Direct3D11.Buffer ConstantLightBuffar01 = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _spectrum_texture_shader = new _sc_spectrum_shader_final();
            _spectrum_texture_shader.Initialize(device, windowsHandle, ConstantLightBuffar01, _DLightBuffer_spectrum);
            //////////////////////
            //SC PHYSICS SPECTRUM
            //////////////////////



            //////////////////////
            //SC VR CLOTH
            //////////////////////
            /*TextureShaderCLOTH = new SC_Cloth_Shader_Final();
            if (!TextureShaderCLOTH.Initialize(device, windowsHandle))
            {
                return false;
            }*/
            //////////////////////
            //SC VR CLOTH
            //////////////////////





            //////////////////////
            //SC PHYSICS CUBES
            //////////////////////
            SharpDX.Direct3D11.Buffer ConstantLightBuffer00 = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _this_object_texture_shader = new SC_cube_shader_final();
            _this_object_texture_shader.Initialize(device, windowsHandle, ConstantLightBuffer00, _DLightBuffer);
            //////////////////////
            //SC PHYSICS CUBES
            //////////////////////







            //////////////////////
            //SC_sdr_lft_elbow_target
            //////////////////////
           _DLightBuffer0 = new _sc_voxel.DLightBuffer[1];
            _DLightBuffer0[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };


            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_lft_elbow_target = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_lft_elbow_target.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer0);
            //////////////////////
            //SC_sdr_lft_elbow_target
            //////////////////////



            //////////////////////
            //_SC_sdr_lft_elbow_target_two
            //////////////////////
             _DLightBuffer1 = new _sc_voxel.DLightBuffer[1];
            _DLightBuffer1[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_lft_elbow_target_two = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_lft_elbow_target_two.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer1);
            //////////////////////
            //_SC_sdr_lft_elbow_target_two
            //////////////////////



            //////////////////////
            //_SC_sdr_lft_foot
            //////////////////////
            _DLightBuffer2 = new _sc_voxel.DLightBuffer[1];
            _DLightBuffer2[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_lft_foot = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_lft_foot.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer2);
            //////////////////////
            //_SC_sdr_lft_foot
            //////////////////////

     


            //////////////////////
            //_SC_sdr_lft_hnd
            //////////////////////
            _DLightBuffer4 = new _sc_voxel.DLightBuffer[1];
            _DLightBuffer4[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_lft_hnd = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_lft_hnd.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer4);
            //////////////////////
            //_SC_sdr_lft_hnd
            //////////////////////



            //////////////////////
            //_sc_voxel
            //////////////////////
            _DLightBuffer5 = new _sc_voxel.DLightBuffer[1];
            _DLightBuffer5[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_lft_shldr = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_lft_shldr.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer5);
            //////////////////////
            //SC_sdr_lft_elbow_target
            //////////////////////

     



            //////////////////////
            //_SC_sdr_lower_left_arm
            //////////////////////
            _DLightBuffer6 = new _sc_voxel.DLightBuffer[1];
            _DLightBuffer6[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_lower_left_arm = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_lower_left_arm.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer6);
            //////////////////////
            //_SC_sdr_lower_left_arm
            //////////////////////





            //////////////////////
            //_SC_sdr_lower_left_leg
            //////////////////////
             _DLightBuffer7 = new _sc_voxel.DLightBuffer[1];
            _DLightBuffer7[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_lower_left_leg = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_lower_left_leg.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer7);
            //////////////////////
            //_SC_sdr_lower_left_leg
            //////////////////////




            //////////////////////
            //_SC_sdr_pelvis
            //////////////////////
             _DLightBuffer8 = new _sc_voxel.DLightBuffer[1];
            _DLightBuffer8[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_pelvis = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_pelvis.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer8);
            //////////////////////
            //_SC_sdr_pelvis
            //////////////////////





            //////////////////////
            //_sc_voxel
            //////////////////////
             _DLightBuffer9 = new _sc_voxel.DLightBuffer[1];
            _DLightBuffer9[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_pelvis = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_pelvis.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer9);
            //////////////////////
            //SC_sdr_lft_elbow_target
            //////////////////////







            //////////////////////
            //_SC_sdr_rght_elbow_target
            //////////////////////
            _DLightBuffer10 = new _sc_voxel.DLightBuffer[1];
            _DLightBuffer10[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_rght_elbow_target = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_rght_elbow_target.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer10);
            //////////////////////
            //_SC_sdr_rght_elbow_target
            //////////////////////
      


            //////////////////////
            //_SC_sdr_rght_elbow_target_two
            //////////////////////
            _DLightBuffer11 = new _sc_voxel.DLightBuffer[1];
            _DLightBuffer11[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_rght_elbow_target_two = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_rght_elbow_target_two.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer11);
            //////////////////////
            //_SC_sdr_rght_elbow_target_two
            //////////////////////

  

            //////////////////////
            //_SC_sdr_rght_foot
            //////////////////////
             _DLightBuffer12 = new _sc_voxel.DLightBuffer[1];
            _DLightBuffer12[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_rght_foot = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_rght_foot.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer12);
            //////////////////////
            //_SC_sdr_rght_foot
            //////////////////////




     


            //////////////////////
            //_SC_sdr_rght_hnd
            //////////////////////
             _DLightBuffer13 = new _sc_voxel.DLightBuffer[1];
            _DLightBuffer13[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_rght_hnd = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_rght_hnd.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer13);
            //////////////////////
            //_SC_sdr_rght_hnd
            //////////////////////



  
            //////////////////////
            //_SC_sdr_rght_shldr
            //////////////////////
             _DLightBuffer14 = new _sc_voxel.DLightBuffer[1];
            _DLightBuffer14[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_rght_shldr = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_rght_shldr.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer14);
            //////////////////////
            //_SC_sdr_rght_shldr
            //////////////////////


            //////////////////////
            //_SC_sdr_lower_right_arm
            //////////////////////
             _DLightBuffer15 = new _sc_voxel.DLightBuffer[1];
            _DLightBuffer15[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_lower_right_arm = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_lower_right_arm.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer15);
            //////////////////////
            //_SC_sdr_lower_right_arm
            //////////////////////




 
            //////////////////////
            //_SC_sdr_lower_right_leg
            //////////////////////
            _DLightBuffer16 = new _sc_voxel.DLightBuffer[1];
            _DLightBuffer16[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_lower_right_leg = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_lower_right_leg.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer16);
            //////////////////////
            //_SC_sdr_lower_right_leg
            //////////////////////



            //////////////////////
            //_SC_sdr_lower_right_leg
            //////////////////////
           _DLightBuffer17 = new _sc_voxel.DLightBuffer[1];
            _DLightBuffer17[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_torso = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_torso.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer17);
            //////////////////////
            //_SC_sdr_lower_right_leg
            //////////////////////




            //MISSING HEAD





            //////////////////////
            //_SC_sdr_upper_right_arm
            //////////////////////
            _DLightBuffer19 = new _sc_voxel.DLightBuffer[1];

            _DLightBuffer19[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_upper_right_arm = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_upper_right_arm.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer19);
            //////////////////////
            //_SC_sdr_upper_right_arm
            //////////////////////


            //////////////////////
            //_SC_sdr_upper_right_leg
            //////////////////////
            _DLightBuffer20 = new _sc_voxel.DLightBuffer[1];

            _DLightBuffer20[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_upper_right_leg = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_upper_right_leg.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer20);
            //////////////////////
            //_SC_sdr_upper_right_leg
            //////////////////////





            //////////////////////
            //_SC_sdr_upper_left_arm
            //////////////////////
            _DLightBuffer21 = new _sc_voxel.DLightBuffer[1];

            _DLightBuffer21[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_upper_left_arm = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_upper_left_arm.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer21);
            //////////////////////
            //_SC_sdr_upper_left_arm
            //////////////////////


            //////////////////////
            //_SC_sdr_upper_right_leg
            //////////////////////
            _DLightBuffer22 = new _sc_voxel.DLightBuffer[1];

            _DLightBuffer22[0] = new _sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_upper_left_leg = new _sc_voxel_spheroid_shader_final();
            _SC_sdr_upper_left_leg.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer22);
            //////////////////////
            //_SC_sdr_upper_right_leg
            //////////////////////





            //////////////////////
            //_shader_screen
            //////////////////////
            /*_DLightBuffer_screen = new DModelClass2.DLightBuffer[1];

            _DLightBuffer_screen[0] = new DModelClass2.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);*/
            /*_shader_screen = new SC_Screen_Shader();
            _shader_screen.Initialize(device, windowsHandle); //, ConstantLightBuffer, _DLightBuffer_screen
            *///////////////////////
            //_shader_screen
            //////////////////////














            /*



            SC_sdr_rght_elbow_target _SC_sdr_rght_elbow_target;
            SC_sdr_rght_elbow_target_two _SC_sdr_rght_elbow_target_two;
            SC_sdr_rght_foot _SC_sdr_rght_foot;
            SC_sdr_rght_hnd _SC_sdr_rght_hnd;
            SC_sdr_rght_shldr _SC_sdr_rght_shldr;
            SC_sdr_lower_right_arm _SC_sdr_lower_right_arm;
            SC_sdr_lower_right_leg _SC_sdr_lower_right_leg;

            SC_sdr_upper_left_arm _SC_sdr_upper_left_arm;
            SC_sdr_upper_left_leg _SC_sdr_upper_left_leg;

            SC_sdr_upper_right_arm _SC_sdr_upper_right_arm;
            SC_sdr_upper_right_leg _SC_sdr_upper_right_leg;
            */













































































            // Create the texture shader object.
            /*TextureShader = new SC_VR_Desktop_Screen_Shader();

            // Initialize the texture shader object.
            if (!TextureShader.Initialize(device, windowsHandle))
                return false;
            */

       

            /*terrainShader = new SC_VR_Terrain_Shader();

            // Initialize the texture shader object.
            if (!terrainShader.Initialize(device, windowsHandle))
                return false;
            */


            /*colorShader = new DColorShader();

            // Initialize the texture shader object.
            if (!colorShader.Initialize(device, windowsHandle))
                return false;
            */
            /*objectColorShader = new DColorShader();

            // Initialize the texture shader object.
            if (!objectColorShader.Initialize(device, windowsHandle))
                return false;
            */


            /*icoColorShader = new SC_VR_ICO_Shader();

            // Initialize the texture shader object.
            if (!icoColorShader.Initialize(device, windowsHandle))
                return false;
            */


            /*// Create the texture shader object.
            TextureShader = new DTextureShader();

            // Initialize the texture shader object.
            if (!TextureShader.Initialize(device, windowsHandle))
                return false;

            // Create the light shader object.
            LightShader = new DLightShader();

            // Initialize the light shader object.
            if (!LightShader.Initialize(device, windowsHandle))
                return false;

            // Create the bump map shader object.
            BumpMapShader = new DBumpMapShader();

            // Initialize the bump map shader object.
            if (!BumpMapShader.Initialize(device, windowsHandle))
                return false;
                */
            return true;
        }

        public bool RenderInstancedObject(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, SC_cube.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, SC_cube _cuber)
        {
            _this_object_texture_shader.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }
        public bool RenderInstancedObject_voxel_spheroid(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {
            _sc_voxel_spheroid_shader_final.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }

        public bool RenderInstancedObjectSpectrum(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_spectrum.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_spectrum _cuber)
        {
            _spectrum_texture_shader.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }

        public bool RenderInstancedObjecter(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture)
        {
            TextureShader.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture);
            return true;
        }

        /*public bool RenderInstancedCloth(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, Matrix[] worldMatrix_instances, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {

            TextureShaderCLOTH.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _cuber.instances, _cuber.instancesData, _cuber.instancesDataRotter, _cuber.InstanceBuffer, _cuber.InstanceRotationBuffer, _cuber.InstanceRotationMatrixBuffer, worldMatrix_instances, _cuber._instX, _cuber._instY, _cuber._instZ, _DLightBuffer_, oculusRiftDir, _cuber.InstanceRotationBufferRIGHT, _cuber.InstanceRotationBufferUP);

            return true;
        }*/








        /*public void ShutDown()
        {
            // Release the bump map shader object.
            BumpMapShader?.ShutDown();
            BumpMapShader = null;
            // Release the light shader object.
            LightShader?.ShutDown();
            LightShader = null;
            // Release the texture shader object.
            TextureShader?.ShutDown();
            TextureShader = null;
        }*/

        /*public bool RenderTextureShader(DeviceContext deviceContext, int indexCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture)
        {
            // Render the model using the texture shader.
            if (!TextureShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, texture))
                return false;

            return true;
        }*/

        /*public bool RenderTouchTextureShader(DeviceContext deviceContext, int indexCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix)
        {
            // Render the model using the texture shader.
            if (!touchShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix)) //, worldMatrix, viewMatrix, projectionMatrix, texture
                return false;

            return true;
        }*/


        /*public bool RenderInstancedObject(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture)
        {
            // Render the model using the texture shader.

            TextureShaderCLOTH.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture);


            //TextureShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount, _worldMatrix);
            //if (!touchShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount)) //, worldMatrix, viewMatrix, projectionMatrix, texture
            //    return false;

            return true;
        }*/
        /*public bool RenderInstancedObject(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture)
        {
            // Render the model using the texture shader.

            TextureShaderCLOTH.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture);


            //TextureShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount, _worldMatrix);
            //if (!touchShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount)) //, worldMatrix, viewMatrix, projectionMatrix, texture
            //    return false;

            return true;
        }*/



















        /*public bool RenderInstancedObjectCloth(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, Matrix[] worldMatrix_instances, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, SC_VR_Cloth _cuber)
        {
            touchShader.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _cuber.instances, _cuber.instancesData, _cuber.instancesDataRotter, _cuber.InstanceBuffer, _cuber.InstanceRotationBuffer, _cuber.InstanceRotationMatrixBuffer, worldMatrix_instances, _cuber._instX, _cuber._instY, _cuber._instZ, _DLightBuffer_, oculusRiftDir, _cuber.InstanceRotationBufferRIGHT, _cuber.InstanceRotationBufferUP);

            return true;
        }*/



        /*public bool RenderClother(DeviceContext deviceContext, int indexCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix)
        {
            // Render the model using the texture shader.
            if (!touchShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix)) //, worldMatrix, viewMatrix, projectionMatrix, texture
                return false;

            return true;
        }*/








        /*public bool RenderTouchTextureShader(DeviceContext deviceContext, int indexCount , Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, int vertexCount, int instanceCount, Matrix[] _worldMatrix)
        {
            // Render the model using the texture shader.

            touchShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount,  _worldMatrix);
            //if (!touchShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount)) //, worldMatrix, viewMatrix, projectionMatrix, texture
            //    return false;
            
            return true;
        }*/

        /*public bool RenderTerrain(DeviceContext deviceContext, int indexCount,  Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix)
        {
            // Render the model using the texture shader.

            terrainShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix);

            //if (!terrainShader.Render(deviceContext, indexCount, worldViewProjection)) //, worldMatrix, viewMatrix, projectionMatrix, texture
            //    return false;

            return true;
        }*/



        /*public bool RenderGrid(DeviceContext deviceContext, int indexCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix)
        {
            // Render the model using the texture shader.

            colorShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix);

            //if (!terrainShader.Render(deviceContext, indexCount, worldViewProjection)) //, worldMatrix, viewMatrix, projectionMatrix, texture
            //    return false;

            return true;
        }*/


        /*public bool RenderInstancedObject(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, Matrix[] worldMatrix_instances, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {
            /*Vector4 ambientColor = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);
            Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
            Vector3 lightDirection = new Vector3(1, 0, 0);

            _DLightBuffer[0] = new DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding = 0
            };

            // Render the model using the texture shader.
            
            _this_object_texture_shader.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _cuber.instances, _cuber.instancesData, _cuber.instancesDataRotter, _cuber.InstanceBuffer, _cuber.InstanceRotationBuffer, _cuber.InstanceRotationMatrixBuffer, worldMatrix_instances, _cuber._instX, _cuber._instY, _cuber._instZ, _DLightBuffer_, oculusRiftDir, _cuber.InstanceRotationBufferRIGHT, _cuber.InstanceRotationBufferUP);

            return true;
        }*/


        /*public bool _rend_head(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, Matrix[] worldMatrix_instances, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, SC_modL_head _cuber)
        {
            _SC_sdr_head.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _cuber.instances, _cuber.instancesData, _cuber.instancesDataRotter, _cuber.InstanceBuffer, _cuber.InstanceRotationBuffer, _cuber.InstanceRotationMatrixBuffer, worldMatrix_instances, _cuber._instX, _cuber._instY, _cuber._instZ, _DLightBuffer0, oculusRiftDir, _cuber.InstanceRotationBufferRIGHT, _cuber.InstanceRotationBufferUP);

            return true;
        }*/

        public bool _rend_lft_elbow_targ(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {
            _SC_sdr_lft_elbow_target.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }


        public bool _rend_lft_elbow_targ_two(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {
            _SC_sdr_lft_elbow_target_two.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }


        public bool _rend_lft_foot(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {
            _SC_sdr_lft_foot.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }




        public bool _rend_lft_hnd(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {
            _SC_sdr_lft_hnd.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }

        public bool _rend_lft_shldr(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {
            _SC_sdr_lft_shldr.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }

        public bool _rend_lft_lower_arm(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {
            _SC_sdr_lower_left_arm.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }



        public bool _rend_lft_lower_leg(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {
            _SC_sdr_lower_left_leg.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }

        public bool _rend_pelvis(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {
            _SC_sdr_pelvis.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }


























        public bool _rend_rgt_elbow_targ(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {
            _SC_sdr_rght_elbow_target.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }


        public bool _rend_rgt_elbow_targ_two(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {
            _SC_sdr_rght_elbow_target_two.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }



        public bool _rend_rgt_foot(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {
            _SC_sdr_rght_foot.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }


        public bool _rend_rgt_hnd(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {
            _SC_sdr_rght_hnd.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }
       

           public bool _rend_rgt_shldr(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {
            _SC_sdr_rght_shldr.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }


        public bool _rend_rgt_lower_arm(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {
            _SC_sdr_lower_right_arm.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }




        public bool _rend_rgt_lower_leg(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {
            _SC_sdr_lower_right_leg.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }




        public bool _rend_torso(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {
            _SC_sdr_torso.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }




        public bool _rend_rgt_upper_arm(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {

            _SC_sdr_upper_right_arm.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }



        public bool _rend_rgt_upper_leg(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {

            _SC_sdr_upper_right_leg.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }


        public bool _rend_lft_upper_arm(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {

            _SC_sdr_upper_left_arm.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }



        public bool _rend_lft_upper_leg(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, _sc_voxel.DLightBuffer[] _DLightBuffer_, Vector3 oculusRiftDir, _sc_voxel _cuber)
        {

            _SC_sdr_upper_left_leg.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, oculusRiftDir, _cuber);
            return true;
        }

 




        /*public bool RenderObjectGrid(DeviceContext deviceContext, int indexCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix)
        {
            // Render the model using the texture shader.

            objectColorShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix);

            //if (!terrainShader.Render(deviceContext, indexCount, worldViewProjection)) //, worldMatrix, viewMatrix, projectionMatrix, texture
            //    return false;

            return true;
        }*/

        /*public bool RenderIcoShader(DeviceContext deviceContext, int indexCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, int vertexCount, int instanceCount, Matrix[] _worldMatrix)
        {
            // Render the model using the texture shader.

            icoColorShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount, _worldMatrix);
            //if (!touchShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount)) //, worldMatrix, viewMatrix, projectionMatrix, texture
            //    return false;

            return true;
        }*/

        //Device.ImmediateContext, InstancedModel.VertexCount, InstancedModel.InstanceCount, _WorldMatrix, viewMatrix, _projectionMatrix, InstancedModel.Texture.TextureResource
       /* public bool RenderInstancedObjecter(DeviceContext deviceContext,int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture)
        {
            // Render the model using the texture shader.

            TextureShader.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture);
             

            //TextureShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount, _worldMatrix);
            /*if (!touchShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount)) //, worldMatrix, viewMatrix, projectionMatrix, texture
                return false;
            
            return true;
        }*/








        /*
        public bool RenderInstancedObjectScreen(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture)
        {
            _shader_screen.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture);

            return true;
        }*/





        /*public bool RenderLightShader(DeviceContext deviceContext, int indexCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, Vector3 lightDirection, Vector4 ambiant, Vector4 diffuse, Vector3 cameraPosition, Vector4 specular, float specualrPower)
        {
            // Render the model using the light shader.
            if (!LightShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, texture, lightDirection, ambiant, diffuse, cameraPosition, specular, specualrPower))
                return false;

            return true;
        }
        public bool RenderBumpMapShader(DeviceContext deviceContext, int indexCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView colorTexture, ShaderResourceView normalTexture, Vector3 lightDirection, Vector4 diffuse)
        {
            // Render the model using the bump map shader.
            if (!BumpMapShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, colorTexture, normalTexture, lightDirection, diffuse))
                return false;

            return true;
        }*/
    }
    }