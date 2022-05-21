using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Runtime.InteropServices;


namespace sccs.scgraphics.scshadermanager
{
    public class SC_ShaderManager                 // 77 lines
    {

        /*
        public sc_voxel_shader_final sc_voxel_shader_final { get; set; }
        public sc_voxel_shader_final _SC_sdr_torso { get; set; }
        sc_voxel.DLightBuffer[] _DLightBuffer17;

        sc_voxel.DLightBuffer[] _DLightBuffer8;


        sc_voxel.DLightBuffer[] _DLightBuffer_voxel_spheroid = new sc_voxel.DLightBuffer[1];*/

        SC_cube.DLightBuffer[] _DLightBuffer_cube = new SC_cube.DLightBuffer[1];
        BufferDescription lightBufferDesc = new BufferDescription()
        {
            Usage = ResourceUsage.Dynamic,
            SizeInBytes = Utilities.SizeOf<SC_cube.DLightBuffer>(),
            BindFlags = BindFlags.ConstantBuffer,
            CpuAccessFlags = CpuAccessFlags.Write,
            OptionFlags = ResourceOptionFlags.None,
            StructureByteStride = 0
        };

        /*
        sc_voxel.DLightBuffer[] _DLightBuffer13;
        sc_voxel.DLightBuffer[] _DLightBuffer14;
        sc_voxel.DLightBuffer[] _DLightBuffer11;
        sc_voxel.DLightBuffer[] _DLightBuffer10;
        sc_voxel.DLightBuffer[] _DLightBuffer19;
        sc_voxel.DLightBuffer[] _DLightBuffer15;
        */
        Vector4 ambientColor = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);
        Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
        Vector3 lightDirection = new Vector3(1, 0, 0);
        Vector3 lightPosition = new Vector3(0, 0, 0);

        SharpDX.Direct3D11.Buffer ConstantLightBuffer;
        

        /*
        public sc_voxel_shader_final _SC_sdr_rght_hnd { get; set; }
        public sc_voxel_shader_final _SC_sdr_lower_right_arm { get; set; }
        public sc_voxel_shader_final _SC_sdr_upper_right_arm { get; set; }
        public sc_voxel_shader_final _SC_sdr_rght_elbow_target { get; set; }
        public sc_voxel_shader_final _SC_sdr_rght_elbow_target_two { get; set; }



        public sc_voxel_shader_final _SC_sdr_pelvis { get; set; }
        public sc_voxel_shader_final _SC_sdr_rght_shldr { get; set; }*/
        public SC_cube_shader_final _this_object_texture_shader { get; set; }


        public bool Initialize(Device device, IntPtr windowsHandle) //, float x, float y, float z, Vector4 color,Matrix worldMatrix
        {


            /*
            //////////////////////
            //_SC_sdr_lower_right_leg
            //////////////////////
            _DLightBuffer17 = new sc_voxel.DLightBuffer[1];
            _DLightBuffer17[0] = new sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_torso = new sc_voxel_shader_final();
            _SC_sdr_torso.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer17);
            //////////////////////
            //_SC_sdr_lower_right_leg
            //////////////////////
            ///


            //////////////////////
            //SC PHYSICS VOXEL SPHEROID
            //////////////////////
            _DLightBuffer_voxel_spheroid[0] = new sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };

            SharpDX.Direct3D11.Buffer ConstantLightBuffer01 = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            sc_voxel_shader_final = new sc_voxel_shader_final();
            sc_voxel_shader_final.Initialize(device, windowsHandle, ConstantLightBuffer01, _DLightBuffer_voxel_spheroid);
            //////////////////////
            //SC PHYSICS CUBES
            //////////////////////


            //////////////////////
            //_SC_sdr_pelvis
            //////////////////////
            _DLightBuffer8 = new sc_voxel.DLightBuffer[1];
            _DLightBuffer8[0] = new sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_pelvis = new sc_voxel_shader_final();
            _SC_sdr_pelvis.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer8);
            //////////////////////
            //_SC_sdr_pelvis
            //////////////////////



            //////////////////////
            //_SC_sdr_rght_hnd
            //////////////////////
            _DLightBuffer13 = new sc_voxel.DLightBuffer[1];
            _DLightBuffer13[0] = new sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_rght_hnd = new sc_voxel_shader_final();
            _SC_sdr_rght_hnd.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer13);
            //////////////////////
            //_SC_sdr_rght_hnd
            //////////////////////



            //////////////////////
            //_SC_sdr_rght_shldr
            //////////////////////
            _DLightBuffer14 = new sc_voxel.DLightBuffer[1];
            _DLightBuffer14[0] = new sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_rght_shldr = new sc_voxel_shader_final();
            _SC_sdr_rght_shldr.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer14);
            //////////////////////
            //_SC_sdr_rght_shldr
            //////////////////////
            ///


            //////////////////////
            //_SC_sdr_lower_right_arm
            //////////////////////
            _DLightBuffer15 = new sc_voxel.DLightBuffer[1];
            _DLightBuffer15[0] = new sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_lower_right_arm = new sc_voxel_shader_final();
            _SC_sdr_lower_right_arm.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer15);
            //////////////////////
            //_SC_sdr_lower_right_arm
            //////////////////////



            //////////////////////
            //_SC_sdr_upper_right_arm
            //////////////////////
            _DLightBuffer19 = new sc_voxel.DLightBuffer[1];

            _DLightBuffer19[0] = new sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_upper_right_arm = new sc_voxel_shader_final();
            _SC_sdr_upper_right_arm.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer19);
            //////////////////////
            //_SC_sdr_upper_right_arm
            //////////////////////



            //////////////////////
            //_SC_sdr_rght_elbow_target
            //////////////////////
            _DLightBuffer10 = new sc_voxel.DLightBuffer[1];
            _DLightBuffer10[0] = new sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_rght_elbow_target = new sc_voxel_shader_final();
            _SC_sdr_rght_elbow_target.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer10);
            //////////////////////
            //_SC_sdr_rght_elbow_target
            //////////////////////



            //////////////////////
            //_SC_sdr_rght_elbow_target_two
            //////////////////////
            _DLightBuffer11 = new sc_voxel.DLightBuffer[1];
            _DLightBuffer11[0] = new sc_voxel.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _SC_sdr_rght_elbow_target_two = new sc_voxel_shader_final();
            _SC_sdr_rght_elbow_target_two.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer11);
            //////////////////////
            //_SC_sdr_rght_elbow_target_two
            //////////////////////
            */





            //////////////////////
            //SC PHYSICS CUBES
            //////////////////////
            _DLightBuffer_cube[0] = new SC_cube.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };
            SharpDX.Direct3D11.Buffer ConstantLightBuffer00 = new SharpDX.Direct3D11.Buffer(device, lightBufferDesc);
            _this_object_texture_shader = new SC_cube_shader_final();
            _this_object_texture_shader.Initialize(device, windowsHandle, ConstantLightBuffer00, _DLightBuffer_cube);
            //////////////////////
            //SC PHYSICS CUBES
            //////////////////////

            return true;
        }


        /*
        public bool RenderInstancedObjectsc_perko_voxel(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, sc_voxel.DLightBuffer[] _DLightBuffer_, sc_voxel _cuber)
        {
            sc_voxel_shader_final.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, _cuber);
            return true;
        }*/

        public bool RenderInstancedObject(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, SC_cube.DLightBuffer[] _DLightBuffer_, SC_cube _cuber,int instancetorenderonly)
        {
            _this_object_texture_shader.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, _cuber, instancetorenderonly);
            return true;
        }

        /*
        public bool _rend_torso(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, sc_voxel.DLightBuffer[] _DLightBuffer_, sc_voxel _cuber)
        {
            _SC_sdr_torso.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, _cuber);
            return true;
        }
        public bool _rend_pelvis(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, sc_voxel.DLightBuffer[] _DLightBuffer_, sc_voxel _cuber)
        {
            _SC_sdr_pelvis.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, _cuber);
            return true;
        }

        public bool _rend_rgt_hnd(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, sc_voxel.DLightBuffer[] _DLightBuffer_, sc_voxel _cuber)
        {
            _SC_sdr_rght_hnd.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, _cuber);
            return true;
        }

        public bool _rend_rgt_shldr(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, sc_voxel.DLightBuffer[] _DLightBuffer_, sc_voxel _cuber)
        {
            _SC_sdr_rght_shldr.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, _cuber);
            return true;
        }


        public bool _rend_rgt_lower_arm(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, sc_voxel.DLightBuffer[] _DLightBuffer_, sc_voxel _cuber)
        {
            _SC_sdr_lower_right_arm.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, _cuber);
            return true;
        }

        public bool _rend_rgt_upper_arm(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, sc_voxel.DLightBuffer[] _DLightBuffer_, sc_voxel _cuber)
        {

            _SC_sdr_upper_right_arm.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, _cuber);
            return true;
        }



        public bool _rend_rgt_elbow_targ(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, sc_voxel.DLightBuffer[] _DLightBuffer_, sc_voxel _cuber)
        {
            _SC_sdr_rght_elbow_target.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, _cuber);
            return true;
        }

        public bool _rend_rgt_elbow_targ_two(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, sc_voxel.DLightBuffer[] _DLightBuffer_, sc_voxel _cuber)
        {
            _SC_sdr_rght_elbow_target_two.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture, _DLightBuffer_, _cuber);
            return true;
        }*/
    }
}