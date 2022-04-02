using SharpDX;
using SharpDX.Direct3D11;
using System;

namespace _sc_core_systems.SC_Graphics.SC_ShaderManager
{
    public class _sc_shader_manager
    {

        public _sc_voxel_spheroid_shader_final _sc_voxel_spheroid_shader_final { get; set; }
        public SC_cube_shader_final _this_object_texture_shader { get; set; }
        public _sc_spectrum_shader_final _spectrum_texture_shader { get; set; }
        public _sc_texture_shader TextureShader { get; set; }


        SC_cube.DLightBuffer[] _DLightBuffer = new SC_cube.DLightBuffer[1];
        _sc_spectrum.DLightBuffer[] _DLightBuffer_spectrum = new _sc_spectrum.DLightBuffer[1];
        _sc_voxel.DLightBuffer[] _DLightBuffer_voxel_spheroid= new _sc_voxel.DLightBuffer[1];



        Vector4 ambientColor = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);
        Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
        Vector3 lightDirection = new Vector3(1, 0, 0);
        Vector3 lightPosition = new Vector3(0, 0, 0);



        BufferDescription lightBufferDesc = new BufferDescription()
        {
            Usage = ResourceUsage.Dynamic,
            SizeInBytes = Utilities.SizeOf<SC_cube.DLightBuffer>(),
            BindFlags = BindFlags.ConstantBuffer,
            CpuAccessFlags = CpuAccessFlags.Write,
            OptionFlags = ResourceOptionFlags.None,
            StructureByteStride = 0
        };


    




        public bool Initialize(Device device, IntPtr windowsHandle)
        {
          


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
    }
}