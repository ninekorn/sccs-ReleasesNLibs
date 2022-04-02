using System;
using System.Collections.Generic;
using System.Text;
using SharpDX;
using Jitter;
using Jitter.Dynamics;
using Jitter.Collision;
using Jitter.LinearMath;
using Jitter.Collision.Shapes;
using Jitter.Forces;
using scmessageobject = sccs.scmessageobject.scmessageobject;
using scmessageobjectjitter = sccs.scmessageobject.scmessageobjectjitter;
using scgraphicssecpackage = sccs.scmessageobject.scgraphicssecpackage;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Ab3d.OculusWrap;
using sccs.scgraphics;
using Ab3d.OculusWrap.DemoDX11;
using System.Runtime.InteropServices;
using System.IO;
using System.Security;
using System.Security.Permissions;
using Jitter.DataStructures;
using SingleBodyConstraints = Jitter.Dynamics.Constraints.SingleBody;
using Jitter.Dynamics.Constraints;
using Vector2 = SharpDX.Vector2;
using Vector3 = SharpDX.Vector3;
using Vector4 = SharpDX.Vector4;
using Quaternion = SharpDX.Quaternion;
using Matrix = SharpDX.Matrix;
using Plane = SharpDX.Plane;
using Ray = SharpDX.Ray;
using SharpDX.Multimedia;
using SharpDX.IO;
using System.Xml;
using SharpDX.XAudio2;
using System.Linq;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

//using System.Windows.Forms;
using System.IO.Ports;

using SharpDX.D3DCompiler;

namespace sccs.scgraphics
{
    public class scgraphicssec //: SC_Update//SC_Intermediate_Update
    {

        public static int activatevrheightmapfeature = 0;
        scgraphicssecpackage scgraphicssecpackagemessage;

        
        public scgraphicssec() //SC_console_directx _SC_console_directx, IntPtr _HWND
        {
            
        }

        public int mapWidth = 20;
        public int mapHeight = 1;
        public int mapDepth = 20;

        public int tinyChunkWidth = 20;
        public int tinyChunkHeight = 20;
        public int tinyChunkDepth = 20;

        public int mapObjectInstanceWidth = 2;
        public int mapObjectInstanceHeight = 1;
        public int mapObjectInstanceDepth = 2;

        float _planeSize = 0.1f;
        int InstanceCount = 0;

        SC_VR_Chunk[] arrayOfChunks;

        SC_VR_Chunk.DInstanceType[] instances;

        public scmessageobjectjitter[][] _sc_create_world_objects(scmessageobjectjitter[][] _sc_jitter_tasks)
        {

            MainWindow.MessageBox((IntPtr)0, "test0", "scmsg", 0);

            //CHUNK CREATION
            arrayOfChunks = new SC_VR_Chunk[mapObjectInstanceWidth * mapObjectInstanceHeight * mapObjectInstanceDepth];
            Vector3 originchunkpos = new Vector3(0, 0, 0);
            //InstanceCount = mapWidth * mapHeight * mapDepth; //(widther * heigther * depther)
            //instances = new SC_VR_Chunk.DInstanceType[mapWidth * mapHeight * mapDepth]; //(widther * heigther * depther)

            for (int x = 0; x < mapObjectInstanceWidth; x++)
            {
                for (int y = 0; y < mapObjectInstanceHeight; y++)
                {
                    for (int z = 0; z < mapObjectInstanceDepth; z++)
                    {
                        Vector3 chunkPos = new Vector3(x, y, z) + originchunkpos;

                        chunkPos.X = chunkPos.X * (mapWidth * tinyChunkWidth * _planeSize);// * (mapWidth * tinyChunkWidth * _planeSize); //4
                        chunkPos.Y = chunkPos.Y * (mapHeight * tinyChunkHeight * _planeSize);// * (mapHeight * tinyChunkHeight * _planeSize); //1
                        chunkPos.Z = chunkPos.Z * (mapDepth * tinyChunkDepth * _planeSize);// * (mapDepth * tinyChunkDepth * _planeSize); //4

                        //instances[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)] = new SC_VR_Chunk.DInstanceType[InstanceCount];
                        arrayOfChunks[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)] = new SC_VR_Chunk(sccs.scgraphics.scdirectx.D3D.device, 1f, 1f, 1f, new Vector4(0.1f, 0.1f, 0.1f, 1), mapWidth, mapHeight, mapDepth, chunkPos, mapWidth, mapHeight, mapDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, mapObjectInstanceWidth, mapObjectInstanceHeight, mapObjectInstanceDepth); //, instances[x + mapObjectInstanceWidth * (y + mapObjectInstanceHeight * z)]
                    }
                }
            }

            _sc_jitter_tasks[0][0].hasinit = 1;

            return _sc_jitter_tasks;
        }


        SC_VR_Chunk.chunkData chunkDat = new SC_VR_Chunk.chunkData();
        public scmessageobjectjitter[][] workonshaders(scgraphicssecpackage scgraphicssecpackagemessage)
        {
            scmessageobjectjitter[][] _sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;
            Matrix viewMatrix = scgraphicssecpackagemessage.viewMatrix;
            Matrix projectionMatrix = scgraphicssecpackagemessage.projectionMatrix; //_projectionMatrix;
            Matrix originRot = scgraphicssecpackagemessage.originRot; // originRot;
            Matrix rotatingMatrix = scgraphicssecpackagemessage.rotatingMatrix; //rotatingMatrix;
            Matrix hmdrotMatrix = scgraphicssecpackagemessage.hmdmatrixRot; //hmdmatrixRot;
            Matrix hmd_matrix = scgraphicssecpackagemessage.hmd_matrix; //hmd_matrix;
            Matrix rotatingMatrixForPelvis = scgraphicssecpackagemessage.rotatingMatrixForPelvis; //rotatingMatrixForPelvis;
            Matrix _rightTouchMatrix = scgraphicssecpackagemessage.rightTouchMatrix; //_rightTouchMatrix;
            Matrix _leftTouchMatrix = scgraphicssecpackagemessage.leftTouchMatrix; //_leftTouchMatrix;
            Matrix oriProjectionMatrix = scgraphicssecpackagemessage.oriProjectionMatrix; //oriProjectionMatrix;
            Matrix extramatrix = scgraphicssecpackagemessage.someextrapelvismatrix; //someextrapelvismatrix;
            Vector3 OFFSETPOS = scgraphicssecpackagemessage.offsetpos; // OFFSETPOS;
            Posef handPoseRight = scgraphicssecpackagemessage.handPoseRight; //handPoseRight;
            Posef handPoseLeft = scgraphicssecpackagemessage.handPoseLeft; //handPoseLeft;


            Matrix _worldMatrix = sccs.scgraphics.scdirectx.WorldMatrix;
            Matrix _viewMatrix = viewMatrix;
            Matrix _projectionMatrix = oriProjectionMatrix;
            //_worldMatrix.Transpose();

            
            if (MainWindow._useOculusRift == 1)
            {
                _projectionMatrix = oriProjectionMatrix;
                _viewMatrix.Transpose();
            }
            else
            {
                _viewMatrix.Transpose();
                _projectionMatrix = scgraphicssecpackagemessage.projectionMatrix;
                _projectionMatrix.Transpose();
                _worldMatrix.Transpose();
            }





            for (int c = 0; c < arrayOfChunks.Length; c++)
            {

                /*_worldMatrix.Transpose();
                _viewMatrix.Transpose();
                _projectionMatrix.Transpose();
                */


                arrayOfChunks[c].arrayOfMatrixBuff[0] = new SC_VR_Chunk.DMatrixBuffer()
                {
                    world = _worldMatrix,
                    view = _viewMatrix,
                    proj = _projectionMatrix,
                };



                chunkDat = new SC_VR_Chunk.chunkData();
                chunkDat.instanceBuffer = arrayOfChunks[c].InstanceBuffer;
                chunkDat.arrayOfInstanceVertex = arrayOfChunks[c].arrayOfInstanceVertex;
                chunkDat.arrayOfInstancePos = arrayOfChunks[c].instances;
                chunkDat.arrayOfInstanceIndices = arrayOfChunks[c].arrayOfInstanceIndices;
                chunkDat.arrayOfInstanceNormals = arrayOfChunks[c].arrayOfInstanceNormals;
                chunkDat.arrayOfInstanceTextureCoordinates = arrayOfChunks[c].arrayOfInstanceTexturesCoordinates;
                //arrayOfInstanceColors


                chunkDat.dVertexData = arrayOfChunks[c].arrayOfDVertex;

                chunkDat.Device = sccs.scgraphics.scdirectx.D3D.device;
                chunkDat.worldMatrix = _worldMatrix;
                chunkDat.viewMatrix = _viewMatrix;
                chunkDat.projectionMatrix = _projectionMatrix;
                chunkDat.chunkShader = arrayOfChunks[c].shaderOfChunk;
                chunkDat.matrixBuffer = arrayOfChunks[c].arrayOfMatrixBuff;
                chunkDat.vertBuffers = arrayOfChunks[c].vertBuffers;
                chunkDat.colorBuffers = arrayOfChunks[c].colorBuffers;
                chunkDat.indexBuffers = arrayOfChunks[c].indexBuffers;
                chunkDat.instanceBuffers = arrayOfChunks[c].instanceBuffers;
                chunkDat.dVertBuffers = arrayOfChunks[c].dVertBuffers;
                chunkDat.texBuffers = arrayOfChunks[c].texBuffers;
                chunkDat.normalBuffers = arrayOfChunks[c].normalBuffers;
                chunkDat.lightBuffer = arrayOfChunks[c].lightBuffer;


                arrayOfChunks[c].shaderOfChunk.Renderer(chunkDat);
            }



            return scgraphicssecpackagemessage.scjittertasks;
        }

        public  scmessageobjectjitter[][] oculuscontrolsNRecordSoundNMousePointer(scgraphicssecpackage scgraphicssecpackagemessage)
        {
            


            return scgraphicssecpackagemessage.scjittertasks;
        }

        //scmessageobjectjitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix hmdrotMatrix, Matrix hmd_matrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft, Matrix oriProjectionMatrix, Matrix extramatrix
        public  scmessageobjectjitter[][] workOnDestroyingBytes(scgraphicssecpackage scgraphicssecpackagemessage)
        {

            return scgraphicssecpackagemessage.scjittertasks;
        }

        public scmessageobjectjitter[][] sccswriteikrigtobuffer(scgraphicssecpackage scgraphicssecpackagemessage)
        {




            return scgraphicssecpackagemessage.scjittertasks;
        }

        public scmessageobjectjitter[][] scwritetobuffer(scmessageobjectjitter[][] _sc_jitter_tasks)
        {
            return _sc_jitter_tasks;
        }
        public scmessageobjectjitter[][] sccswritescreenassetstobuffer(scmessageobjectjitter[][] _sc_jitter_tasks)
        {


            return _sc_jitter_tasks;
        }

        private void _oculus_touch_controls(double percentXRight, double percentYRight, Vector2f[] thumbStickRight, double percentXLeft, double percentYLeft, Vector2f[] thumbStickLeft, double realMousePosX, double realMousePosY) //
        {
            
        }

        private void _MicrosoftWindowsMouseRight(double percentXRight, double percentYRight, Vector2f[] thumbStickRight, double percentXLeft, double percentYLeft, Vector2f[] thumbStickLeft, double realMousePosX, double realMousePosY) //, double realOculusRiftCursorPosX, double realOculusRiftCursorPosY
        {
            
        }
    }
}