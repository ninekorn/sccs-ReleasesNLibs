using SharpDX;
using SharpDX.Direct3D11;
using System;

//using SC_SkYaRk_Clean.SC_Graphics.SC_Textures.SC_VR_Desktop_Screen_Textures;
//using SC_skYaRk_VR_V007.SC_Graphics.SC_Textures.SC_VR_Touch_Textures;
using SC_skYaRk_VR_V007.SC_Graphics;
using System.Collections.Generic;



namespace SC_skYaRk_VR_V007
{
    public class DShaderManager                 // 77 lines
    {



        public SC_Screen_Shader _screen_texture { get; set; }

        // Properties
        //public DTextureShader TextureShader { get; set; }

        //public SC_VR_Touch_Shader touchShader { get; set; }
        //public DLightShader LightShader { get; set; }
        //public DBumpMapShader BumpMapShader { get; set; }

        // Methods


        //public SC_VR_Terrain_Shader terrainShader { get; set; }

        /*public DColorShader colorShader { get; set; }

        //public DColorShader objectColorShader { get; set; }

        //public SC_VR_ICO_Shader icoColorShader { get; set; }



        public DTextureShader TextureShader { get; set; }
        public DTextureShader TextureShaderer { get; set; }

        public DTextureShader_Cube TextureShadererCube { get; set; }

        public DTextureShader _DTerrain_World_Axis_X { get; set; }*/

        public List<object> _listOfShaders = new List<object>();


        Device _Device;
        IntPtr _HWND;

        public bool Initialize_Shader_Manager(Device Device, IntPtr HWND) //, float x, float y, float z, Vector4 color,Matrix worldMatrix
        {
            _Device = Device;
            _HWND = HWND;
            Initialize_Texture();


            return true;
        }


        public bool Initialize_Texture() //, float x, float y, float z, Vector4 color,Matrix worldMatrix
        {




            _screen_texture = new SC_Screen_Shader();
            _screen_texture.Initialize(_Device, _HWND);



            /*Vector4 ambientColor = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);
            Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
            Vector3 lightDirection = new Vector3(1, 0, 0);
            Vector3 lightPosition = new Vector3(0, 0, 0);


            SC_Desk_Screen.DLightBuffer[] _DLightBuffer = new SC_Desk_Screen.DLightBuffer[1];


            _DLightBuffer[0] = new SC_Desk_Screen.DLightBuffer()
            {
                ambientColor = ambientColor,
                diffuseColor = diffuseColour,
                lightDirection = lightDirection,
                padding0 = 0,
                lightPosition = lightPosition,
                padding1 = 0
            };

            BufferDescription lightBufferDesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Utilities.SizeOf<SC_Desk_Screen.DLightBuffer>(),
                BindFlags = BindFlags.ConstantBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            var ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(_Device, lightBufferDesc);
            

            _screen_texture.Initialize(_Device, _HWND, ConstantLightBuffer, _DLightBuffer);*/




























            //SC_Console_GRAPHICS.MessageBox((IntPtr)0, "test0", "Oculus Error", 0);

            // Create the texture shader object.

            /*SC_Console_GRAPHICS.MessageBox((IntPtr)0, "test1", "Oculus Error", 0);

            TextureShaderer = new DTextureShader();

            // Initialize the texture shader object.
            if (!TextureShaderer.Initialize(device, windowsHandle))
                return false;
                */

            //SC_Console_GRAPHICS.MessageBox((IntPtr)0, "test2", "Oculus Error", 0);

            /*TextureShadererCube = new DTextureShader_Cube();

            // Initialize the texture shader object.
            if (!TextureShadererCube.Initialize(device, windowsHandle))
                return false;
            */

            //_DTerrain_World_Axis_X = new DTextureShader();
            //bool tester =  _DTerrain_World_Axis_X.Initialize(device, windowsHandle);

            //SC_Console_GRAPHICS.MessageBox((IntPtr)0, tester.ToString(), "Oculus Error", 0);
            /*// Initialize the texture shader object.
            if (!_DTerrain_World_Axis_X.Initialize(device, windowsHandle))
            {
                SC_Console_GRAPHICS.MessageBox((IntPtr)0, "test", "Oculus Error", 0);
                //return false;
            }*/
            //return false;

            // Create the texture shader object.
            /*TextureShader = new SC_VR_Desktop_Screen_Shader();

            // Initialize the texture shader object.
            if (!TextureShader.Initialize(device, windowsHandle))
                return false;
            */


            /*touchShader = new SC_VR_Touch_Shader();

            // Initialize the texture shader object.
            if (!touchShader.Initialize(device, windowsHandle)) //, x, y, z,  color, worldMatrix
                return false;
            */


            /*terrainShader = new SC_VR_Terrain_Shader();

            // Initialize the texture shader object.
            if (!terrainShader.Initialize(device, windowsHandle))
                return false;
            */

            /*SC_Console_GRAPHICS.MessageBox((IntPtr)0, "test3", "Oculus Error", 0);

            colorShader = new DColorShader();

            // Initialize the texture shader object.
            if (!colorShader.Initialize(device, windowsHandle))
                return false;


            SC_Console_GRAPHICS.MessageBox((IntPtr)0, "test4", "Oculus Error", 0);

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


        public bool RenderInstancedScreenObject(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture)
        {
            // Render the model using the texture shader.

            _screen_texture.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture);

            //TextureShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount, _worldMatrix);
            /*if (!touchShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount)) //, worldMatrix, viewMatrix, projectionMatrix, texture
                return false;
            */
            return true;
        }









        /*public bool _initTexture(SharpDX.Direct3D11.Device device, IntPtr windowsHandle, SC_Console_GRAPHICS.DLightBuffer[] _DLightBuffer, SharpDX.Direct3D11.Buffer ConstantLightBuffer)
        {
            TextureShader = new DTextureShader();

            // Initialize the texture shader object.
            if (!TextureShader.Initialize(device, windowsHandle, ConstantLightBuffer, _DLightBuffer))
            {
                return false;
            }
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

        /*public bool RenderGridTerrain(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture)
        {

            //if (texture == null)
            //{
            //    SC_Console_GRAPHICS.MessageBox((IntPtr)0, "test00", "Oculus Error", 0);
            //}

            _DTerrain_World_Axis_X.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture);
            //if (!_DTerrain_World_Axis_X.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture)) //, worldMatrix, viewMatrix, projectionMatrix, texture
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

        /*public bool RenderTerrainInstanced(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture)
        {
            // Render the model using the texture shader.

            TextureShaderer.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture);


            //TextureShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount, _worldMatrix);
            //if (!touchShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount)) //, worldMatrix, viewMatrix, projectionMatrix, texture
             //   return false;
            
            return true;
        }*/

        /*public bool RenderTerrainInstanced_Cube(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture)
        {
            // Render the model using the texture shader.

            TextureShadererCube.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture);


            //TextureShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount, _worldMatrix);
            /*if (!touchShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount)) //, worldMatrix, viewMatrix, projectionMatrix, texture
                return false;
            
            return true;
        }*/



        /*public bool RenderTerrainInstanced_Cube(DeviceContext deviceContext, int indexCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, int vertexCount, int instanceCount)
        {
            // Render the model using the texture shader.

            TextureShadererCube.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount);
            //if (!touchShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount)) //, worldMatrix, viewMatrix, projectionMatrix, texture
            //    return false;
            
            return true;
        }*/












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
        /*public bool RenderInstancedObject(DeviceContext deviceContext,int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture)
        {
            // Render the model using the texture shader.

            TextureShader.Render(deviceContext, VertexCount, InstanceCount, worldMatrix, viewMatrix, projectionMatrix, texture);
             

            //TextureShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount, _worldMatrix);
            //if (!touchShader.Render(deviceContext, indexCount, worldMatrix, viewMatrix, projectionMatrix, vertexCount, instanceCount)) //, worldMatrix, viewMatrix, projectionMatrix, texture
            //    return false;
            
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