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
using System.IO;
using System.Xml;

namespace sccs
{
    public class sclevelgenclassPrim
    {
        public sclevelgenvert[] arrayofchunkslod0;//= new sclevelgenvert[levelgen.typeoftiles.Count];
        public sclevelgenvert[] arrayofchunkslod1;
        public sclevelgenvert[] arrayofchunkslod2;
        public sclevelgenvert[] arrayofchunkslod3;


        public sclevelgenmaps[] arrayofchunksmapslod0;// = new sclevelgenmaps[levelgen.typeoftiles.Count];
        public sclevelgenmaps[] arrayofchunksmapslod1;
        public sclevelgenmaps[] arrayofchunksmapslod2;
        public sclevelgenmaps[] arrayofchunksmapslod3;


        public sclevelgenvert getChunklod0(float x, float y, float z, out int arrayindex) //, Vector3 chunkPos
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

            //var enumerator0 = sclevelgenchunk.arrayofchunkslod0.GetEnumerator();
            for (int i = 0; i < arrayofchunkslod0.Length; i++)
            {

                float x1 = (float)(Math.Round(arrayofchunkslod0[i].chunkPos.X * 10.0f) / 10.0f);
                float y1 = (float)(Math.Round(arrayofchunkslod0[i].chunkPos.Y * 10.0f) / 10.0f);
                float z1 = (float)(Math.Round(arrayofchunkslod0[i].chunkPos.Z * 10.0f) / 10.0f);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    arrayindex = i;
                    return arrayofchunkslod0[i];
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

        public sclevelgenvert getChunklod1(float x, float y, float z, out int arrayindex) //, Vector3 chunkPos
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

            //var enumerator0 = sclevelgenchunk.arrayofchunkslod0.GetEnumerator();
            for (int i = 0; i < arrayofchunkslod1.Length; i++)
            {

                float x1 = (float)(Math.Round(arrayofchunkslod1[i].chunkPos.X * 10.0f) / 10.0f);
                float y1 = (float)(Math.Round(arrayofchunkslod1[i].chunkPos.Y * 10.0f) / 10.0f);
                float z1 = (float)(Math.Round(arrayofchunkslod1[i].chunkPos.Z * 10.0f) / 10.0f);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    arrayindex = i;
                    return arrayofchunkslod1[i];
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


        public sclevelgenvert getChunklod2(float x, float y, float z, out int arrayindex) //, Vector3 chunkPos
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

            //var enumerator0 = sclevelgenchunk.arrayofchunkslod0.GetEnumerator();
            for (int i = 0; i < arrayofchunkslod2.Length; i++)
            {

                float x1 = (float)(Math.Round(arrayofchunkslod2[i].chunkPos.X * 10.0f) / 10.0f);
                float y1 = (float)(Math.Round(arrayofchunkslod2[i].chunkPos.Y * 10.0f) / 10.0f);
                float z1 = (float)(Math.Round(arrayofchunkslod2[i].chunkPos.Z * 10.0f) / 10.0f);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    arrayindex = i;
                    return arrayofchunkslod2[i];
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


        InputElement[] inputElements;

        public SharpDX.Direct3D11.Buffer ConstantTessellationBuffer;

        [StructLayout(LayoutKind.Sequential)]
        public struct DTessellationBufferType
        {
            public float tessellationAmount;
            public Vector3 padding;
        }

        public void ShutDown()
        {
            // Release the model texture.
            ReleaseTexture();
            // Release the vertex and index buffers.
            ShutdownBuffers();
        }
        private void ReleaseTexture()
        {
            // Release the texture object.
            //Texture?.ShutDown();
            //Texture = null;
        }



        private void ShutdownBuffers()
        {





            someovrdir = null;

            InstanceCount = 0;


            if (Layout != null)
            {
                Layout.Dispose();
                Layout = null;
            }

            if (samplerState != null)
            {
                samplerState.Dispose();
                samplerState = null;
            }






            lightBuffer = null;
            originalArrayOfVerts = null;
            originalArrayOfIndices = null;
            originalMap = null;
            arrayOfMatrixBuff = null;
            instancesLocationW = null;
            instancesLocationH = null;
            instancesLocationD = null;




            if (HullShader != null)
            {
                HullShader.Dispose();
                HullShader = null;
            }
            if (DomainShader != null)
            {
                DomainShader.Dispose();
                DomainShader = null;
            }
            if (VertexShader != null)
            {
                VertexShader.Dispose();
                VertexShader = null;
            }

            if (PixelShader != null)
            {
                PixelShader.Dispose();
                PixelShader = null;
            }

            if (GeometryShader != null)
            {
                GeometryShader.Dispose();
                GeometryShader = null;
            }






            /*public Matrix currentWorldMatrix;
            public Matrix worldmatofobj;
            public Vector3 somechunkpriminstancepos;*/

            chunkindex = 0;

            usegeometryshader = 0;

            numberOfObjectInWidth = 0;
            numberOfObjectInHeight = 0;
            numberOfObjectInDepth = 0;
            numberOfInstancesPerObjectInWidth = 0;
            numberOfInstancesPerObjectInHeight = 0;
            numberOfInstancesPerObjectInDepth = 0;
            planeSize = 0;

            tinyChunkWidth = 0;
            tinyChunkHeight = 0;
            tinyChunkDepth = 0;
            /*string vsFileNameByteArray = "";
            string psFileNameByteArray = "";
            string gsFileName = "";*/

            typeofbytemapobject = 0;
            fullface = 0;

            voxeltype = 0;

            // Return the index buffer.
            //IndexBuffer?.Dispose();
            //IndexBuffer = null;
            // Release the vertex buffer.
            //VertexBuffer?.Dispose();
            //VertexBuffer = null;
            //heightmapmatrix = null;
            instancesmatrix = null;
            instancesmatrixb = null;
            instancesmatrixc = null;
            instancesmatrixd = null;

            //arrayOfSomeMap = null;
            instancesDataFORWARD = null;
            instancesDataRIGHT = null;
            instancesDataUP = null;

            //arrayofzeromeshinstances = null;
            instances = null;
            //somechunk = null;
            //instancesIndex = null;
            instancesLocationW = null;
            instancesLocationH = null;




            if (contantBuffer != null)
            {
                contantBuffer.Dispose();
                contantBuffer = null;
            }


            if (ConstantLightBuffer != null)
            {
                ConstantLightBuffer.Dispose();
                ConstantLightBuffer = null;
            }





            instancesbytemaps = null;

            instancesccsbytemapxyz = null;
            heightmapmatrixbuff = null;
            instances = null;
            instancesmatrix = null;
            instancesmatrixb = null;
            instancesmatrixc = null;
            instancesmatrixd = null;
            instancesDataFORWARD = null;
            instancesDataRIGHT = null;
            instancesDataUP = null;
            instancesLocationW = null;
            instancesLocationH = null;
            lightBufferInstChunk = null;


            //VertexShader VertexShader;
            //PixelShader PixelShader;
            //GeometryShader GeometryShader;


            if (ConstantLightBuffer != null)
            {
                ConstantLightBuffer.Dispose();
                ConstantLightBuffer = null;
            }

            if (someoculusdirbuffer != null)
            {
                someoculusdirbuffer.Dispose();
                someoculusdirbuffer = null;
            }

            if (somerandomvaluebuffer != null)
            {
                somerandomvaluebuffer.Dispose();
                somerandomvaluebuffer = null;
            }




            if (InstanceRotationBufferRIGHT != null)
            {
                InstanceRotationBufferRIGHT.Dispose();
                InstanceRotationBufferRIGHT = null;
            }

            if (InstanceRotationBufferUP != null)
            {
                InstanceRotationBufferUP.Dispose();
                InstanceRotationBufferUP = null;
            }

            if (InstanceRotationBufferFORWARD != null)
            {
                InstanceRotationBufferFORWARD.Dispose();
                InstanceRotationBufferFORWARD = null;
            }

            if (IndexBuffer != null)
            {
                IndexBuffer.Dispose();
                IndexBuffer = null;
            }

            if (instancesbytemapsbuffer != null)
            {
                instancesbytemapsbuffer.Dispose();
                instancesbytemapsbuffer = null;
            }

            /*
            if (IndicesBuffer != null)
            {
                IndicesBuffer.Dispose();
                IndicesBuffer = null;
            }
            if (VertexBuffer != null)
            {
                VertexBuffer.Dispose();
                VertexBuffer = null;
            }*/

            if (instancesmatrixbuffer != null)
            {
                instancesmatrixbuffer.Dispose();
                instancesmatrixbuffer = null;
            }

            if (instancesmatrixbufferb != null)
            {
                instancesmatrixbufferb.Dispose();
                instancesmatrixbufferb = null;
            }
            if (instancesmatrixbufferc != null)
            {
                instancesmatrixbufferc.Dispose();
                instancesmatrixbufferc = null;
            }
            if (instancesmatrixbufferd != null)
            {
                instancesmatrixbufferd.Dispose();
                instancesmatrixbufferd = null;
            }

            if (InstanceBuffer != null)
            {
                InstanceBuffer.Dispose();
                InstanceBuffer = null;
            }

            if (instanceBufferHeightmap != null)
            {
                instanceBufferHeightmap.Dispose();
                instanceBufferHeightmap = null;
            }


            if (InstanceBufferLocW != null)
            {
                InstanceBufferLocW.Dispose();
                InstanceBufferLocW = null;
            }


            if (instancesbytemapsmatrixbuffer != null)
            {
                instancesbytemapsmatrixbuffer.Dispose();
                instancesbytemapsmatrixbuffer = null;
            }


            if (InstanceBufferLocH != null)
            {
                InstanceBufferLocH.Dispose();
                InstanceBufferLocH = null;
            }






            if (shaderOfChunk != null)
            {
                shaderOfChunk.Dispose();
                shaderOfChunk = null;
            }
            /*
            if (sccstrigvertbuilder != null)
            {
                sccstrigvertbuilder.Dispose();
                sccstrigvertbuilder = null;
            }*/


            for (int j = 0; j < arrayOfChunkDatalod0.Length; j++)
            {
                if (arrayOfChunkDatalod0[j] != null)
                {
                    if (arrayOfChunkDatalod0[j].constantLightBuffer != null)
                    {
                        arrayOfChunkDatalod0[j].constantLightBuffer.Dispose();
                        arrayOfChunkDatalod0[j].constantLightBuffer = null;
                    }

                    if (arrayOfChunkDatalod0[j].constantMatrixPosBuffer != null)
                    {
                        arrayOfChunkDatalod0[j].constantMatrixPosBuffer.Dispose();
                        arrayOfChunkDatalod0[j].constantMatrixPosBuffer = null;
                    }
                    /*
                    if (arrayOfChunkDatalod0[j].ConstantTessellationBuffer != null)
                    {
                        arrayOfChunkDatalod0[j].ConstantTessellationBuffer.Dispose();
                        arrayOfChunkDatalod0[j].ConstantTessellationBuffer = null;
                    }
                    */
                    if (arrayOfChunkDatalod0[j].IndicesBuffer != null)
                    {
                        arrayOfChunkDatalod0[j].IndicesBuffer.Dispose();
                        arrayOfChunkDatalod0[j].IndicesBuffer = null;
                    }
                    /*
                    if (arrayOfChunkDatalod0[j].instanceBuffer != null)
                    {
                        arrayOfChunkDatalod0[j].instanceBuffer.Dispose();
                        arrayOfChunkDatalod0[j].instanceBuffer = null;
                    }

                    if (arrayOfChunkDatalod0[j].instanceBufferHeightmap != null)
                    {
                        arrayOfChunkDatalod0[j].instanceBufferHeightmap.Dispose();
                        arrayOfChunkDatalod0[j].instanceBufferHeightmap = null;
                    }
                    if (arrayOfChunkDatalod0[j].InstanceBufferLocD != null)
                    {
                        arrayOfChunkDatalod0[j].InstanceBufferLocD.Dispose();
                        arrayOfChunkDatalod0[j].InstanceBufferLocD = null;
                    }*/
                    if (arrayOfChunkDatalod0[j].InstanceBufferLocH != null)
                    {
                        arrayOfChunkDatalod0[j].InstanceBufferLocH.Dispose();
                        arrayOfChunkDatalod0[j].InstanceBufferLocH = null;
                    }
                    if (arrayOfChunkDatalod0[j].InstanceBufferLocW != null)
                    {
                        arrayOfChunkDatalod0[j].InstanceBufferLocW.Dispose();
                        arrayOfChunkDatalod0[j].InstanceBufferLocW = null;
                    }
                    /*
                    if (arrayOfChunkDatalod0[j].InstanceRotationBufferFORWARD != null)
                    {
                        arrayOfChunkDatalod0[j].InstanceRotationBufferFORWARD.Dispose();
                        arrayOfChunkDatalod0[j].InstanceRotationBufferFORWARD = null;
                    }
                    if (arrayOfChunkDatalod0[j].InstanceRotationBufferRIGHT != null)
                    {
                        arrayOfChunkDatalod0[j].InstanceRotationBufferRIGHT.Dispose();
                        arrayOfChunkDatalod0[j].InstanceRotationBufferRIGHT = null;
                    }
                    if (arrayOfChunkDatalod0[j].InstanceRotationBufferUP != null)
                    {
                        arrayOfChunkDatalod0[j].InstanceRotationBufferUP.Dispose();
                        arrayOfChunkDatalod0[j].InstanceRotationBufferUP = null;
                    }


                    if (arrayOfChunkDatalod0[j].mapBuffer != null)
                    {
                        arrayOfChunkDatalod0[j].mapBuffer.Dispose();
                        arrayOfChunkDatalod0[j].mapBuffer = null;
                    }
                    */
                    if (arrayOfChunkDatalod0[j].vertexBuffer != null)
                    {
                        arrayOfChunkDatalod0[j].vertexBuffer.Dispose();
                        arrayOfChunkDatalod0[j].vertexBuffer = null;
                    }
                    /*
                    if (arrayOfChunkDatalod0[j].DomainShader != null)
                    {
                        arrayOfChunkDatalod0[j].DomainShader.Dispose();
                        arrayOfChunkDatalod0[j].DomainShader = null;
                    }
                    */
                    if (arrayOfChunkDatalod0[j].GeometryShader != null)
                    {
                        arrayOfChunkDatalod0[j].GeometryShader.Dispose();
                        arrayOfChunkDatalod0[j].GeometryShader = null;
                    }

                    /*
                    if (arrayOfChunkDatalod0[j].HullShader != null)
                    {
                        arrayOfChunkDatalod0[j].HullShader.Dispose();
                        arrayOfChunkDatalod0[j].HullShader = null;
                    }*/


                    if (arrayOfChunkDatalod0[j].PixelShader != null)
                    {
                        arrayOfChunkDatalod0[j].PixelShader.Dispose();
                        arrayOfChunkDatalod0[j].PixelShader = null;
                    }


                    if (arrayOfChunkDatalod0[j].VertexShader != null)
                    {
                        arrayOfChunkDatalod0[j].VertexShader.Dispose();
                        arrayOfChunkDatalod0[j].VertexShader = null;
                    }


                    /*
                    arrayOfChunkDatalod0[j].arrayOfDeVectorMapTemp = null;
                    arrayOfChunkDatalod0[j].arrayOfDeVectorMapTempTwo = null;
                    arrayOfChunkDatalod0[j].arrayOfSomeMap = null;*/


                    //arrayOfChunkDatalod0[j].arrayOfVertex = null;


                    /*arrayOfChunkDatalod0[j].heightmapmatrix = null;
                    arrayOfChunkDatalod0[j].instancesbytemaps = null;
                    arrayOfChunkDatalod0[j].instancesccsbytemapxyz = null;
                    arrayOfChunkDatalod0[j].instancesIndex = null;
                    arrayOfChunkDatalod0[j].instancesLocationD = null;*/


                    arrayOfChunkDatalod0[j].instancesLocationH = null;
                    arrayOfChunkDatalod0[j].instancesLocationW = null;

                    /*
                    arrayOfChunkDatalod0[j].instancesmatrix = null;
                    arrayOfChunkDatalod0[j].instancesmatrixb = null;
                    arrayOfChunkDatalod0[j].instancesmatrixc = null;
                    arrayOfChunkDatalod0[j].instancesmatrixd = null;*/



                    /*
                    if (arrayOfChunkDatalod0[j].instancesmatrixbuffer != null)
                    {
                        arrayOfChunkDatalod0[j].instancesmatrixbuffer.Dispose();
                        arrayOfChunkDatalod0[j].instancesmatrixbuffer = null;
                    }


                    if (arrayOfChunkDatalod0[j].instancesmatrixbufferb != null)
                    {
                        arrayOfChunkDatalod0[j].instancesmatrixbufferb.Dispose();
                        arrayOfChunkDatalod0[j].instancesmatrixbufferb = null;
                    }

                    if (arrayOfChunkDatalod0[j].instancesmatrixbufferc != null)
                    {
                        arrayOfChunkDatalod0[j].instancesmatrixbufferc.Dispose();
                        arrayOfChunkDatalod0[j].instancesmatrixbufferc = null;
                    }

                    if (arrayOfChunkDatalod0[j].instancesmatrixbufferd != null)
                    {
                        arrayOfChunkDatalod0[j].instancesmatrixbufferd.Dispose();
                        arrayOfChunkDatalod0[j].instancesmatrixbufferd = null;
                    }*/


                    if (arrayOfChunkDatalod0[j].Layout != null)
                    {
                        arrayOfChunkDatalod0[j].Layout.Dispose();
                        arrayOfChunkDatalod0[j].Layout = null;
                    }

                    if (arrayOfChunkDatalod0[j].lightBuffer != null)
                    {

                        arrayOfChunkDatalod0[j].lightBuffer = null;
                    }

                    if (arrayOfChunkDatalod0[j].matrixBuffer != null)
                    {

                        arrayOfChunkDatalod0[j].matrixBuffer = null;
                    }


                    if (arrayOfChunkDatalod0[j].samplerState != null)
                    {
                        arrayOfChunkDatalod0[j].samplerState.Dispose();
                        arrayOfChunkDatalod0[j].samplerState = null;
                    }
                    /*
                    if (arrayOfChunkDatalod0[j].someoculusdirbuffer != null)
                    {
                        arrayOfChunkDatalod0[j].someoculusdirbuffer.Dispose();
                        arrayOfChunkDatalod0[j].someoculusdirbuffer = null;
                    }

                    if (arrayOfChunkDatalod0[j].somerandomvaluebuffer != null)
                    {
                        arrayOfChunkDatalod0[j].somerandomvaluebuffer.Dispose();
                        arrayOfChunkDatalod0[j].somerandomvaluebuffer = null;
                    }

                    arrayOfChunkDatalod0[j].originalArrayOfIndices = null;

                    arrayOfChunkDatalod0[j].sclevelgenclass_Instances = null;
                    arrayOfChunkDatalod0[j].sclevelgenclass_InstancesFORWARD = null;
                    arrayOfChunkDatalod0[j].sclevelgenclass_InstancesRIGHT = null;
                    arrayOfChunkDatalod0[j].sclevelgenclass_InstancesUP = null;

                    arrayOfChunkDatalod0[j].someovrdir = null;
                    arrayOfChunkDatalod0[j].tessellationBuffer = null;
                    */
                    //arrayOfChunkDatalod0[j].arrayOfVertex = null;

                    arrayOfChunkDatalod0[j] = null;

                }
            }

            for (int j = 0; j < arrayofindexzeromeshlod0.Length; j++)
            {
                if (arrayofindexzeromeshlod0[j] != null)
                {
                    arrayofindexzeromeshlod0[j].ShutDown();
                    arrayofindexzeromeshlod0[j] = null;
                }
            }
            for (int j = 0; j < arrayofindexzeromeshlod1.Length; j++)
            {
                if (arrayofindexzeromeshlod1[j] != null)
                {
                    arrayofindexzeromeshlod1[j].ShutDown();
                    arrayofindexzeromeshlod1[j] = null;
                }
            }
            for (int j = 0; j < arrayofindexzeromeshlod2.Length; j++)
            {
                if (arrayofindexzeromeshlod2[j] != null)
                {
                    arrayofindexzeromeshlod2[j].ShutDown();
                    arrayofindexzeromeshlod2[j] = null;
                }
            }
            for (int j = 0; j < arrayofindexzeromeshlod3.Length; j++)
            {
                if (arrayofindexzeromeshlod3[j] != null)
                {
                    arrayofindexzeromeshlod3[j].ShutDown();
                    arrayofindexzeromeshlod3[j] = null;
                }
            }

            /*
            for (int j = 0; j < arrayofindexzeromeshlod4.Length; j++)
            {
                if (arrayofindexzeromeshlod4[j] != null)
                {
                    arrayofindexzeromeshlod4[j].ShutDown();
                    arrayofindexzeromeshlod4[j] = null;
                }
            }
            for (int j = 0; j < arrayofindexzeromeshlod5.Length; j++)
            {
                if (arrayofindexzeromeshlod5[j] != null)
                {
                    arrayofindexzeromeshlod5[j].ShutDown();
                    arrayofindexzeromeshlod5[j] = null;
                }
            }
            */






            /*
            if (somechunk != null)
            {
                somechunk.Dispose();
                somechunk = null;
            }*/


            if (inputElements != null)
            {
                inputElements = null;
            }



            if (IndicesBuffer5faces != null)
            {
                IndicesBuffer5faces.Dispose();
                IndicesBuffer5faces = null;
            }

            if (VertexBuffer5faces != null)
            {
                VertexBuffer5faces.Dispose();
                VertexBuffer5faces = null;
            }



            somevertlist = null;
            sometriglist = null;

            //somevertlistoriginal = null;
            //sometriglistoriginal = null;



            /*
            SharpDX.Direct3D11.Buffer contantBuffer;
            //VertexShader VertexShader;
            //PixelShader PixelShader;
            //GeometryShader GeometryShader;
            SharpDX.Direct3D11.Buffer ConstantLightBuffer;

            SharpDX.Direct3D11.Buffer someoculusdirbuffer;

            SharpDX.Direct3D11.Buffer InstanceRotationBufferRIGHT;
            SharpDX.Direct3D11.Buffer InstanceRotationBufferUP;
            SharpDX.Direct3D11.Buffer InstanceRotationBufferFORWARD;

            SharpDX.Direct3D11.Buffer IndexBuffer;
            SharpDX.Direct3D11.Buffer instancesbytemapsbuffer;

            SharpDX.Direct3D11.Buffer IndicesBuffer;
            SharpDX.Direct3D11.Buffer VertexBuffer;

            SharpDX.Direct3D11.Buffer instancesmatrixbufferd;
            SharpDX.Direct3D11.Buffer instancesmatrixbufferc;

            SharpDX.Direct3D11.Buffer InstanceBuffer;
            SharpDX.Direct3D11.Buffer instanceBufferHeightmap;
            SharpDX.Direct3D11.Buffer instancesmatrixbuffer;
            SharpDX.Direct3D11.Buffer instancesmatrixbufferb;

            SharpDX.Direct3D11.Buffer InstanceBufferLocW;

            SharpDX.Direct3D11.Buffer instancesbytemapsmatrixbuffer;
            SharpDX.Direct3D11.Buffer InstanceBufferLocH;

        public OVRDir[] someovrdir;*/
        }



        [StructLayout(LayoutKind.Explicit)]
        public struct randomval
        {
            [FieldOffset(0)]
            public Vector4 randomvalue;
        }




        [StructLayout(LayoutKind.Explicit)]
        public struct OVRDir
        {
            [FieldOffset(0)]
            public Vector4 ovrdirf;
            [FieldOffset(16)]
            public Vector4 ovrdirr;
            [FieldOffset(32)]
            public Vector4 ovrdiru;
            [FieldOffset(48)]
            public Vector4 ovrpos;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct DMatrixBuffer
        {
            [FieldOffset(0)]
            public Matrix world;
            [FieldOffset(64)]
            public Matrix view;
            [FieldOffset(128)]
            public Matrix proj;
        }

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
        public class chunkData
        {
            public Vector3 realpos;
            public Vector3 chunkPos;
            public int istypeofmesh;

            public VertexShader VertexShader;//= new VertexShader(D3D.device, vertexShaderByteCode);
            public PixelShader PixelShader;// = new PixelShader(D3D.device, pixelShaderByteCode);
            public GeometryShader GeometryShader;// = new GeometryShader(D3D.device, geometryShaderByteCode);
            /*public HullShader HullShader;
            public DomainShader DomainShader;

            public SharpDX.Direct3D11.Buffer ConstantTessellationBuffer;*/

            public int[] arrayofindices;
            public sclevelgenclass.DVertex[] arrayofverts;

            public InputLayout Layout;

            /*
            public DTessellationBufferType[] tessellationBuffer;

            public sclevelgenclass.DInstanceMatrix[] instancesmatrix;
            public SharpDX.Direct3D11.Buffer instancesmatrixbuffer;

            public sclevelgenclass.DInstanceMatrix[] instancesmatrixb;
            public SharpDX.Direct3D11.Buffer instancesmatrixbufferb;

            public sclevelgenclass.DInstanceMatrix[] instancesmatrixc;
            public SharpDX.Direct3D11.Buffer instancesmatrixbufferc;

            public sclevelgenclass.DInstanceMatrix[] instancesmatrixd;
            public SharpDX.Direct3D11.Buffer instancesmatrixbufferd;


            public SharpDX.Direct3D11.Buffer somerandomvaluebuffer;
            public randomval[] randomvaluearray;

            public SharpDX.Direct3D11.Buffer someoculusdirbuffer;

            public OVRDir[] someovrdir;
            public sclevelgenclass.DInstancesccsbytemapxyz[] instancesccsbytemapxyz;
            public SharpDX.Direct3D11.Buffer instancesbytemapsmatrixbuffer;

            public sclevelgenclass.heightmapinstance[] heightmapmatrix;
            //public sclevelgenclass.DInstanceType[] indexArray;
            public sclevelgenclass.DInstancesByteMap[] instancesbytemaps;
            public sclevelgenclass.DInstanceType[] sclevelgenclass_Instances;
            public sclevelgenclass.DInstanceShipData[] sclevelgenclass_InstancesFORWARD;
            public sclevelgenclass.DInstanceShipData[] sclevelgenclass_InstancesRIGHT;
            public sclevelgenclass.DInstanceShipData[] sclevelgenclass_InstancesUP;*/

            public Matrix worldMatrix;
            public Matrix viewMatrix;
            public Matrix projectionMatrix;
            // public sclevelgenclassshader chunkShader;
            public DMatrixBuffer[] matrixBuffer;

            public DLightBufferEr[] lightBuffer;
            public int switchForRender;
            /*public sclevelgenclass.DInstanceType[] instancesIndex;
            public sclevelgenclass.DInstanceType[] arrayOfDeVectorMapTemp;
            public sclevelgenclass.DInstanceShipData[] arrayOfDeVectorMapTempTwo;
            */
            //public SharpDX.Direct3D11.Buffer instanceBufferHeightmap;
            //public SharpDX.Direct3D11.Buffer instanceBuffer;

            public SharpDX.Direct3D11.Buffer constantLightBuffer;
            public SharpDX.Direct3D11.Buffer vertexBuffer;
            public SharpDX.Direct3D11.Buffer IndicesBuffer;

            public SharpDX.Direct3D11.Buffer constantMatrixPosBuffer;
            //public int[][] arrayOfSomeMap;
            //public SharpDX.Direct3D11.Buffer mapBuffer;

            public sclevelgenclass.DInstanceTypeLocW[] instancesLocationW;
            public sclevelgenclass.DInstanceTypeLocH[] instancesLocationH;
            //public sclevelgenclass.DInstanceTypeLocD[] instancesLocationD;

            public SharpDX.Direct3D11.Buffer InstanceBufferLocW;
            public SharpDX.Direct3D11.Buffer InstanceBufferLocH;
            /*public SharpDX.Direct3D11.Buffer InstanceBufferLocD;
            public SharpDX.Direct3D11.Buffer instancesbytemapsbuffer;

            public SharpDX.Direct3D11.Buffer InstanceRotationBufferFORWARD;
            public SharpDX.Direct3D11.Buffer InstanceRotationBufferRIGHT;
            public SharpDX.Direct3D11.Buffer InstanceRotationBufferUP;*/

            //public sclevelgenclass.DVertex[] arrayOfVertex;

            public int copytobuffer;

            /*public int numberOfObjectInWidth;
            public int numberOfObjectInHeight;
            public int numberOfObjectInDepth;
            public int numberOfInstancesPerObjectInWidth;
            public int numberOfInstancesPerObjectInHeight;
            public int numberOfInstancesPerObjectInDepth;*/
            public float planeSize;

            public sclevelgenclassshader shaderOfChunk;

            public SamplerState samplerState;

            public int candrawswtc;
        }



        [StructLayout(LayoutKind.Explicit)]
        public struct DLightBufferEr
        {
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



        public randomval[] randomvaluearray;

        SharpDX.Direct3D11.Buffer somerandomvaluebuffer;

        //chunkscreen somechunk;

        SharpDX.Direct3D11.Buffer contantBuffer;
        //VertexShader VertexShader;
        //PixelShader PixelShader;
        //GeometryShader GeometryShader;
        SharpDX.Direct3D11.Buffer ConstantLightBuffer;

        SharpDX.Direct3D11.Buffer someoculusdirbuffer;

        SharpDX.Direct3D11.Buffer InstanceRotationBufferRIGHT;
        SharpDX.Direct3D11.Buffer InstanceRotationBufferUP;
        SharpDX.Direct3D11.Buffer InstanceRotationBufferFORWARD;

        SharpDX.Direct3D11.Buffer IndexBuffer;
        SharpDX.Direct3D11.Buffer instancesbytemapsbuffer;

        //SharpDX.Direct3D11.Buffer IndicesBuffer;
        //SharpDX.Direct3D11.Buffer VertexBuffer;

        SharpDX.Direct3D11.Buffer instancesmatrixbufferd;
        SharpDX.Direct3D11.Buffer instancesmatrixbufferc;

        SharpDX.Direct3D11.Buffer InstanceBuffer;
        SharpDX.Direct3D11.Buffer instanceBufferHeightmap;
        SharpDX.Direct3D11.Buffer instancesmatrixbuffer;
        SharpDX.Direct3D11.Buffer instancesmatrixbufferb;

        SharpDX.Direct3D11.Buffer InstanceBufferLocW;

        SharpDX.Direct3D11.Buffer instancesbytemapsmatrixbuffer;
        SharpDX.Direct3D11.Buffer InstanceBufferLocH;

        public OVRDir[] someovrdir;
        //public sclevelgenclass.DVertex[] indexArray;

        //CREATING MY VR DESKTOP INSTANCED SCREEN WITH MY ADDED BYTE TO SHADER CHUNK FROM MY PROGRAM TEST CALLED 1ST VERSION. 
        //CREATING MY VR DESKTOP INSTANCED SCREEN WITH MY ADDED BYTE TO SHADER CHUNK FROM MY PROGRAM TEST CALLED 1ST VERSION. 
        //CREATING MY VR DESKTOP INSTANCED SCREEN WITH MY ADDED BYTE TO SHADER CHUNK FROM MY PROGRAM TEST CALLED 1ST VERSION. 

        public chunkData[] arrayOfChunkDatalod0;
        public chunkData[] arrayOfChunkDatalod1;
        public chunkData[] arrayOfChunkDatalod2;
        public chunkData[] arrayOfChunkDatalod3;
        //public chunkData[] arrayOfChunkDatalod4;
        //public chunkData[] arrayOfChunkDatalod5;

        public sclevelgenclass[] arrayofindexzeromeshlod0;
        public sclevelgenclass[] arrayofindexzeromeshlod1;
        public sclevelgenclass[] arrayofindexzeromeshlod2;
        public sclevelgenclass[] arrayofindexzeromeshlod3;
        //public sclevelgenclass[] arrayofindexzeromeshlod4;
        //public sclevelgenclass[] arrayofindexzeromeshlod5;


        int InstanceCount = 0;

        InputLayout Layout;
        VertexShader VertexShader;
        PixelShader PixelShader;

        public SamplerState samplerState;
        /*[StructLayout(LayoutKind.Explicit, Size = 48)] //, Pack = 48
        public struct DInstanceType
        {
            [FieldOffset(0)]
            public int one;
            [FieldOffset(4)]
            public int two;
            [FieldOffset(8)]
            public int three;
            [FieldOffset(12)]
            public int four;
            [FieldOffset(16)]
            public int oneTwo;
            [FieldOffset(20)]
            public int twoTwo;
            [FieldOffset(24)]
            public int threeTwo;
            [FieldOffset(28)]
            public int fourTwo;
            [FieldOffset(32)]
            public Vector4 instancePos;
        };*/

        /*[StructLayout(LayoutKind.Sequential, Pack = 16)]
        public struct DInstanceTypeTwo
        {
            public Matrix matrix;
        };*/

        DLightBuffer[] lightBuffer = new DLightBuffer[1];
        public sclevelgenclass.DVertex[] originalArrayOfVerts;
        public int[] originalArrayOfIndices;

        public sclevelgenclass.DVertex[] chunkarrayofverts;
        public int[] chunkarrayofindices;

        int[] originalMap;

        public sclevelgenclassshader shaderOfChunk;
        /*
        sccstrigvertbuilderscreen sccstrigvertbuilder;
        sccstrigvertbuilderscreenreduced sccstrigvertbuilderreduced;
        sccstrigvertbuilderscreenmorescreens sccstrigvertbuildermorescreen;*/

        chunkpermesh chunkpermeshreduced;

        public DMatrixBuffer[] arrayOfMatrixBuff = new DMatrixBuffer[1];
        public sclevelgenclass.DInstanceTypeLocW[] instancesLocationW { get; set; }
        public sclevelgenclass.DInstanceTypeLocH[] instancesLocationH { get; set; }
        public sclevelgenclass.DInstanceTypeLocD[] instancesLocationD { get; set; }

        GeometryShader GeometryShader;






        public DLightBufferEr[] lightBufferInstChunk = new DLightBufferEr[1];

        public sclevelgenclass.DInstancesccsbytemapxyz[] instancesccsbytemapxyz;
        public sclevelgenclass.DInstancesByteMap[] instancesbytemaps;

        public sclevelgenclass.DInstanceType[] instances;
        public sclevelgenclass.DInstanceShipData[] instancesDataFORWARD;
        public sclevelgenclass.DInstanceShipData[] instancesDataRIGHT;
        public sclevelgenclass.DInstanceShipData[] instancesDataUP;

        public sclevelgenclass.DInstanceMatrix[] instancesmatrix;
        public sclevelgenclass.DInstanceMatrix[] instancesmatrixb;
        public sclevelgenclass.DInstanceMatrix[] instancesmatrixc;
        public sclevelgenclass.DInstanceMatrix[] instancesmatrixd;
        public HullShader HullShader { get; set; }
        public DomainShader DomainShader { get; set; }

        //SC_AI_Start[][] perceptronRotationRL;
        //SC_AI_Start[][] perceptronRotationTB;
        //SC_AI_Start[][] perceptronRotationFB;

        //SC_AI.data_input dataForPerceptronRL;
        //SC_AI.data_input dataForPerceptronTB;
        //SC_AI.data_input dataForPerceptronFB;

       
        sclevelgenclass.heightmapinstance[] heightmapmatrixbuff;

        public Matrix currentWorldMatrix;
        public Matrix worldmatofobj;
        public Vector3 somechunkpriminstancepos;
        public int chunkindex;

        int usegeometryshader;

        int numberOfObjectInWidth; int numberOfObjectInHeight; int numberOfObjectInDepth; int numberOfInstancesPerObjectInWidth; int numberOfInstancesPerObjectInHeight; int numberOfInstancesPerObjectInDepth; float planeSize;

        int tinyChunkWidth; int tinyChunkHeight; int tinyChunkDepth;
        string vsFileNameByteArray = "";
        string psFileNameByteArray = "";
        string gsFileName = "";

        int typeofbytemapobject;
        int fullface;

        public int voxeltype;


        [StructLayout(LayoutKind.Sequential)]
        public struct DInstanceData
        {
            public Vector4 rotation;
        }

        public int hasalreadycreated5faces = 0;


        SharpDX.Direct3D11.Buffer IndicesBuffer5faces;
        SharpDX.Direct3D11.Buffer VertexBuffer5faces;
        List<sclevelgenclass.DVertex> somevertlist;
        List<int> sometriglist;

        /*
        List<sclevelgenclass.DVertex> somevertlistoriginal;
        List<int> sometriglistoriginal;


        public void createdifferentvirtualdesktop(sccs.scgraphics.scdirectx D3D, int fullface_, int chunkprimindex)
        {
            if (hasalreadycreated5faces == 0 && fullface_ == 1)
            {
                /*VertexBuffer?.Dispose();
                VertexBuffer = null;

                IndicesBuffer?.Dispose();
                IndicesBuffer = null;
                



                if (voxeltype == 0 || voxeltype == 1 || voxeltype == 2 || voxeltype == 3)
                {
                    sccstrigvertbuilder = new sccstrigvertbuilderscreen();
                    //chunkTerrain.startBuildingArray(new Vector4(0, 0, 0, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, this);
                    sccstrigvertbuilder.startBuildingArray(new Vector4(somechunkpriminstancepos.X, somechunkpriminstancepos.Y, somechunkpriminstancepos.Z, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, this, null, null, fullface_, voxeltype); //,surfaceWidth,surfaceHeight
                }
                
                VertexBuffer5faces = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, originalArrayOfVerts);
                IndicesBuffer5faces = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.IndexBuffer, originalArrayOfIndices);

                somevertlist = sccstrigvertbuilder.vertexlist;
                sometriglist = sccstrigvertbuilder.listOfTriangleIndices;

                arrayOfChunkData[chunkprimindex].IndicesBuffer = IndicesBuffer5faces;
                arrayOfChunkData[chunkprimindex].vertexBuffer = VertexBuffer5faces;
                arrayOfChunkData[chunkprimindex].arrayOfVertex = somevertlist.ToArray();//.ToArray();
                arrayOfChunkData[chunkprimindex].originalArrayOfIndices = sometriglist.ToArray();//.ToArray();
                hasalreadycreated5faces = 1;

            }
            else if (hasalreadycreated5faces == 1 && fullface_ == 1)
            {

                arrayOfChunkData[chunkprimindex].IndicesBuffer = IndicesBuffer5faces;
                arrayOfChunkData[chunkprimindex].vertexBuffer = VertexBuffer5faces;
                arrayOfChunkData[chunkprimindex].arrayOfVertex = somevertlist.ToArray();//.ToArray();
                arrayOfChunkData[chunkprimindex].originalArrayOfIndices = sometriglist.ToArray();//.ToArray();
            }
            else if (fullface_ == 0)
            {

                arrayOfChunkData[chunkprimindex].IndicesBuffer = IndicesBuffer;
                arrayOfChunkData[chunkprimindex].vertexBuffer = VertexBuffer;
                arrayOfChunkData[chunkprimindex].arrayOfVertex = somevertlistoriginal.ToArray();//.ToArray();
                arrayOfChunkData[chunkprimindex].originalArrayOfIndices = sometriglistoriginal.ToArray();//.ToArray();
            }
        }*/


        public float moffsetinstance;

        public void createChunk(sccs.scgraphics.scdirectx D3D, float surfaceWidth, float surfaceHeight, Vector3 lightpos, Vector3 dirLight, Vector3 somechunkpriminstancepos_, int chunkindex_, int _addtoworld, float _mass, World _the_world, bool _is_static, sccs.scgraphics.scdirectx.BodyTag _tag, int numberofmainobjectx, int numberofmainobjecty, int numberofmainobjectz, int tinyChunkWidth_, int tinyChunkHeight_, int tinyChunkDepth_, float planeSize_, int shaderswtch, float offsetinstance, int fullface_, int usegeometryshader_, int somechunkkeyboardpriminstanceindex_, int voxeltype_, Matrix worldmatofobj_, int typeofbytemapobject_, int mainix, int mainiy, int mainiz,int typeofvoxelmesh, int swtcsetneighboors)
        {
            try
            {
                moffsetinstance = offsetinstance;

                typeofbytemapobject = typeofbytemapobject_;
                worldmatofobj = worldmatofobj_;

                /* Quaternion somedirquat;
                Quaternion.RotationMatrix(ref worldmatofobj, out somedirquat);

                var dirDroneInstanceRight = -sc_maths._newgetdirleft(somedirquat);
                var dirDroneInstanceUp = sc_maths._newgetdirup(somedirquat);
                var dirDroneInstanceForward = sc_maths._newgetdirforward(somedirquat);

                //position = position - (new Vector4(dirDroneInstanceRight.X, dirDroneInstanceRight.Y, dirDroneInstanceRight.Z, 1.0f) * (tinyChunkWidth * planeSize * 0.5f));
                //position = position - (new Vector4(dirDroneInstanceUp.X, dirDroneInstanceUp.Y, dirDroneInstanceUp.Z, 1.0f) * (tinyChunkWidth * planeSize * 0.5f));
                //position = position + (new Vector4(dirDroneInstanceForward.X, dirDroneInstanceForward.Y, dirDroneInstanceForward.Z, 1.0f) * (tinyChunkWidth * planeSize * 0.5f));

                Vector4 somevecx = new Vector4(dirDroneInstanceRight.X, dirDroneInstanceRight.Y, dirDroneInstanceRight.Z, 1.0f);
                Vector4 somevecy = new Vector4(dirDroneInstanceUp.X, dirDroneInstanceUp.Y, dirDroneInstanceUp.Z, 1.0f);
                Vector4 somevecz = new Vector4(dirDroneInstanceForward.X, dirDroneInstanceForward.Y, dirDroneInstanceForward.Z, 1.0f);

                Vector4 somemainpos = new Vector4(worldmatofobj.M41, worldmatofobj.M42, worldmatofobj.M43, 1.0f);
                Vector4 someinstancepostest = somemainpos;// Vector4.Zero;

                someinstancepostest = someinstancepostest + (somevecx * (x * tinyChunkWidth * planeSize));
                someinstancepostest = someinstancepostest + (somevecy * (y * tinyChunkWidth * planeSize));
                someinstancepostest = someinstancepostest + (somevecz * (z * tinyChunkWidth * planeSize));*/
                /*
                someovrdir = new OVRDir[1];
                someovrdir[0].ovrdirr = new Vector4(0, 0, 0, 1);
                someovrdir[0].ovrdirf = new Vector4(0, 0, 0, 1);
                someovrdir[0].ovrdiru = new Vector4(0, 0, 0, 1);
                someovrdir[0].ovrpos = new Vector4(0, 0, 0, 1);*/

                voxeltype = voxeltype_;

                usegeometryshader = usegeometryshader_;

                fullface = fullface_;

                tinyChunkWidth = tinyChunkWidth_;
                tinyChunkHeight = tinyChunkHeight_;
                tinyChunkDepth = tinyChunkDepth_;
                /*
                numberOfObjectInWidth = numberOfObjectInWidth_;
                numberOfObjectInHeight = numberOfObjectInHeight_;
                numberOfObjectInDepth = numberOfObjectInDepth_;
                numberOfInstancesPerObjectInWidth = numberOfInstancesPerObjectInWidth_;
                numberOfInstancesPerObjectInHeight = numberOfInstancesPerObjectInHeight_;
                numberOfInstancesPerObjectInDepth = numberOfInstancesPerObjectInDepth_;*/
                planeSize = planeSize_;

                chunkindex = chunkindex_;

                somechunkpriminstancepos = somechunkpriminstancepos_;

                //arrayofindexzeromesh = new sclevelgenclass[numberOfObjectInWidth_ * numberOfObjectInHeight_ * numberOfObjectInDepth_];
                //arrayOfChunkData = new chunkData[numberOfObjectInWidth_ * numberOfObjectInHeight_ * numberOfObjectInDepth_];
                //arrayofindexzeromesh = new sclevelgenclass[numberOfObjectInWidth_ * numberOfObjectInHeight_ * numberOfObjectInDepth_];
                //arrayOfChunkData = new chunkData[numberOfObjectInWidth_ * numberOfObjectInHeight_ * numberOfObjectInDepth_];

                /*randomvaluearray = new randomval[numberOfInstancesPerObjectInWidth * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];


                for (int i = 0; i < randomvaluearray.Length; i++)
                {
                    randomvaluearray[i] = new randomval();
                    randomvaluearray[i].randomvalue = new Vector4();
                    randomvaluearray[i].randomvalue.X = 0;
                    randomvaluearray[i].randomvalue.Y = 0;
                    randomvaluearray[i].randomvalue.Z = 0;
                    randomvaluearray[i].randomvalue.W = 0;
                }*/



                //Console.WriteLine(voxeltype_);

                /*//SC_Globals.numberOfInstancesPerObjectInWidth_* numberOfInstancesPerObjectInHeight_* numberOfInstancesPerObjectInDepth_
                if (voxeltype_ == 0 || voxeltype_ == 1 || voxeltype_ == 2 || voxeltype_ == 3)
                {
                    sccstrigvertbuilder = new sccstrigvertbuilderscreen();
                    //chunkTerrain.startBuildingArray(new Vector4(0, 0, 0, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, this);
                    sccstrigvertbuilder.startBuildingArray(new Vector4(somechunkpriminstancepos.X, somechunkpriminstancepos.Y, somechunkpriminstancepos.Z, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, numberOfInstancesPerObjectInWidth_, numberOfInstancesPerObjectInHeight_, numberOfInstancesPerObjectInDepth_, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, this, null, null, fullface, voxeltype); //,surfaceWidth,surfaceHeight
                    somevertlistoriginal = sccstrigvertbuilder.vertexlist;
                    sometriglistoriginal = sccstrigvertbuilder.listOfTriangleIndices;
                }
                else if (voxeltype_ == 4)
                {
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

                     int[] theMap;
                     int mapofbytes = typeofbytemapobject;

                    /*
                     chunkpermeshreduced = new chunkpermesh();
                    //chunkTerrain.startBuildingArray(new Vector4(0, 0, 0, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, this);
                    //chunkpermeshreduced.startBuildingArray(new Vector4(somechunkpriminstancepos.X, somechunkpriminstancepos.Y, somechunkpriminstancepos.Z, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, this, null, null, fullface_, voxeltype); //,surfaceWidth,surfaceHeight

                    chunkpermeshreduced.startBuildingArray(new Vector4(somechunkpriminstancepos.X, somechunkpriminstancepos.Y, somechunkpriminstancepos.Z, 1.0f),
                         out m11, out m12, out m13, out m14, out m21, out m22, out m23, out m24, out m31, out m32, out m33, out m34, out m41, out m42, out m43, out m44, out theMap, null,
                         swtcsetneighboors, null, numberofmainobjectx, numberofmainobjecty, numberofmainobjectz, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth,
                         numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, voxeltype, mapofbytes,
                         mainix, mainiy, mainiz, 0, 0, 0, 0, 0, 0
                        , out m11b, out m12b, out m13b, out m14b, out m21b, out m22b, out m23b, out m24b, out m31b, out m32b, out m33b, out m34b, out m41b, out m42b, out m43b, out m44b,
                        out m11c, out m12c, out m13c, out m14c, out m21c, out m22c, out m23c, out m24c, out m31c, out m32c, out m33c, out m34c, out m41c, out m42c, out m43c, out m44c,
                        out m11d, out m12d, out m13d, out m14d, out m21d, out m22d, out m23d, out m24d, out m31d, out m32d, out m33d, out m34d, out m41d, out m42d, out m43d, out m44d, -1, out chunkarrayofverts, out chunkarrayofindices); //, somechunkkeyboardpriminstanceindex_, chunkprimindex_, chunkinstindex
                    

                    //Console.WriteLine(chunkarrayofverts.Length);
                    //BY CALCULATING THE ENTIRE SIZE OF THE CHUNK 8*8*8 * INSTX * INSTY * INSTZ bytemap and the face sizes, we are able to determine the location x,y,z of each faces vertexes. //each set
                    //of 4 vertex per face, is taking a larger surface when using my triangle/vertex reducer technique.

                    //sccstrigvertbuilderreduced = new sccstrigvertbuilderscreenreduced();
                    //sccstrigvertbuilderreduced.startBuildingArray(new Vector4(somechunkpriminstancepos.X, somechunkpriminstancepos.Y, somechunkpriminstancepos.Z, 1), out chunkarrayofverts, out chunkarrayofindices, out originalMap, numberOfInstancesPerObjectInWidth_, numberOfInstancesPerObjectInHeight_, numberOfInstancesPerObjectInDepth_, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, this, null, null, fullface, voxeltype);

                    //Console.WriteLine(chunkarrayofverts.Length);




                    voxeltype = 0;
                    sccstrigvertbuilder = new sccstrigvertbuilderscreen();
                    //chunkTerrain.startBuildingArray(new Vector4(0, 0, 0, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, this);
                    sccstrigvertbuilder.startBuildingArray(new Vector4(somechunkpriminstancepos.X, somechunkpriminstancepos.Y, somechunkpriminstancepos.Z, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, numberOfInstancesPerObjectInWidth_, numberOfInstancesPerObjectInHeight_, numberOfInstancesPerObjectInDepth_, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, this, null, null, fullface, voxeltype); //,surfaceWidth,surfaceHeight
                    somevertlistoriginal = sccstrigvertbuilder.vertexlist;
                    sometriglistoriginal = sccstrigvertbuilder.listOfTriangleIndices;
                }*/



                /*else if (voxeltype_ == 2)
                {
                    sccstrigvertbuilderreduced = new sccstrigvertbuilderscreenreduced();
                    //chunkTerrain.startBuildingArray(new Vector4(0, 0, 0, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, this);
                    sccstrigvertbuilderreduced.startBuildingArray(new Vector4(somechunkpriminstancepos.X, somechunkpriminstancepos.Y, somechunkpriminstancepos.Z, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, numberOfInstancesPerObjectInWidth_, numberOfInstancesPerObjectInHeight_, numberOfInstancesPerObjectInDepth_, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, this, null, null, fullface, voxeltype); //,surfaceWidth,surfaceHeight
                }*/
                /*else if (voxeltype_ == 3)
                {
                    sccstrigvertbuildermorescreen = new sccstrigvertbuilderscreenmorescreens();
                    //chunkTerrain.startBuildingArray(new Vector4(0, 0, 0, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, this);
                    sccstrigvertbuildermorescreen.startBuildingArray(new Vector4(somechunkpriminstancepos.X, somechunkpriminstancepos.Y, somechunkpriminstancepos.Z, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, numberOfInstancesPerObjectInWidth_, numberOfInstancesPerObjectInHeight_, numberOfInstancesPerObjectInDepth_, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, this, null, null, fullface, voxeltype, mainix, mainiy, mainiz); //,surfaceWidth,surfaceHeight
                }*/





                //InstanceCount = numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_;
                //instancesbytemaps = new sclevelgenclass.DInstancesByteMap[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];

                //instancesccsbytemapxyz = new sclevelgenclass.DInstancesccsbytemapxyz[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];

                //instances = new sclevelgenclass.DInstanceType[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                //heightmapmatrixbuff = new sclevelgenclass.heightmapinstance[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];

                //instancesmatrix = new sclevelgenclass.DInstanceMatrix[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                //instancesmatrixb = new sclevelgenclass.DInstanceMatrix[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                //instancesmatrixc = new sclevelgenclass.DInstanceMatrix[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                //instancesmatrixd = new sclevelgenclass.DInstanceMatrix[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];

                //instancesDataFORWARD = new sclevelgenclass.DInstanceShipData[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                //instancesDataRIGHT = new sclevelgenclass.DInstanceShipData[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                //instancesDataUP = new sclevelgenclass.DInstanceShipData[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                /*
                instancesLocationW = new sclevelgenclass.DInstanceTypeLocW[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                instancesLocationH = new sclevelgenclass.DInstanceTypeLocH[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                *///instancesLocationD = new DInstanceTypeLocD[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];

                //var vsFileNameByteArray = sccs.Properties.Resources.textureTrigVS; //for cubes use shader textureTrigChunkVSMOD //textureTrigChunkVS
                //var psFileNameByteArray = sccs.Properties.Resources.textureTrigPS; //textureTrigChunkPS
                /*int istypeofmesh = 0;

                if (shaderswtch == 0 && Program._useOculusRift == 1)
                {
                    istypeofmesh = 0;
                    vsFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkVS;//textureTrigChunkVSMOD
                    psFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkPS;
                    //gsFileName = sccs.Properties.Resources.HLSLchunkkeyboard;
                    gsFileName = sccsr14sc.Properties.Resources.HLSL;
                }
                else if (shaderswtch == 1 && Program._useOculusRift == 1)
                {
                    istypeofmesh = 0;
                    vsFileNameByteArray = sccsr14sc.Properties.Resources.textureTrigChunkVS;//textureTrigChunkVSMOD
                    psFileNameByteArray = sccsr14sc.Properties.Resources.textureTrigChunkPS;
                    gsFileName = sccsr14sc.Properties.Resources.HLSL;
                }
                else if (shaderswtch == 2 && Program._useOculusRift == 1)
                {
                    istypeofmesh = 0;
                    vsFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkheightmapVS;//textureTrigChunkVSMOD
                    psFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkheightmapPS;
                    gsFileName = sccsr14sc.Properties.Resources.HLSL;
                }
                else if (shaderswtch == 3 && Program._useOculusRift == 1)
                {
                    istypeofmesh = 0;
                    vsFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkheightmapVSeight;//textureTrigChunkVSMOD
                    psFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkheightmapPSeight;
                    gsFileName = sccsr14sc.Properties.Resources.HLSL;
                }
                else if (shaderswtch == 0 && Program._useOculusRift == 0)
                {
                    istypeofmesh = 0;
                    vsFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkVSdx;//textureTrigChunkVSMOD
                    psFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkPSdx;
                    //gsFileName = sccs.Properties.Resources.HLSLchunkkeyboard;
                    gsFileName = sccsr14sc.Properties.Resources.HLSL;
                }
                else if (shaderswtch == 1 && Program._useOculusRift == 0)
                {
                    istypeofmesh = 0;
                    vsFileNameByteArray = sccsr14sc.Properties.Resources.textureTrigChunkVSdx; //textureTrigChunkVSMOD
                    psFileNameByteArray = sccsr14sc.Properties.Resources.textureTrigChunkPSdx;
                    gsFileName = sccsr14sc.Properties.Resources.HLSL;
                }
                else if (shaderswtch == 2 && Program._useOculusRift == 0)
                {
                    istypeofmesh = 0;
                    vsFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkheightmapVSdx; //textureTrigChunkVSMOD
                    psFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkheightmapPSdx;
                    gsFileName = sccsr14sc.Properties.Resources.HLSL;
                }
                else if (shaderswtch == 3 && Program._useOculusRift == 0)
                {
                    istypeofmesh = 0;
                    vsFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkheightmapVSeightdx; //textureTrigChunkVSMOD
                    psFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkheightmapPSeightdx;
                    gsFileName = sccsr14sc.Properties.Resources.HLSL;
                }
                else if (shaderswtch == 4 && Program._useOculusRift == 0)
                {
                    istypeofmesh = 0;
                    vsFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkheightmapVSsixteendx; //textureTrigChunkVSMOD
                    psFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkheightmapPSsixteendx;
                    gsFileName = sccsr14sc.Properties.Resources.HLSL;
                }

                else if (shaderswtch == 5 && Program._useOculusRift == 0)
                {
                    istypeofmesh = 0;
                    vsFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkheightmapVSeightbreakdx; //textureTrigChunkVSMOD
                    psFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkheightmapPSeightbreakdx;
                    gsFileName = sccsr14sc.Properties.Resources.HLSL;
                }

                else if (shaderswtch == 6 && Program._useOculusRift == 1)
                {
                    istypeofmesh = 1;
                    vsFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkheightmapVSeightbreakvr; //textureTrigChunkVSMOD
                    psFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkheightmapPSeightbreakvr;
                    gsFileName = sccsr14sc.Properties.Resources.HLSLchunkkeyboard;
                }
                else if (shaderswtch == 7 && Program._useOculusRift == 1)
                {
                    istypeofmesh = 1;
                    vsFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkterrainVSeightbreakvr; //textureTrigChunkVSMOD
                    psFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkterrainPSeightbreakvr;
                    gsFileName = sccsr14sc.Properties.Resources.HLSL;

                    /*istypeofmesh = 1;
                    vsFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkheightmapVSeightbreakvr; //textureTrigChunkVSMOD
                    psFileNameByteArray = sccsr14sc.Properties.Resources.sccsvertbindchunkheightmapPSeightbreakvr;
                    gsFileName = sccsr14sc.Properties.Resources.HLSLchunkkeyboard;
                }*/






                //var hsFileName = sccsr14sc.Properties.Resources.colorhull; //textureTrigChunkVSMOD
                //var dsFileName = sccsr14sc.Properties.Resources.colordom;



                // Compile the Hull shader code.
                //ShaderBytecode hullShaderByteCode = ShaderBytecode.Compile(hsFileName, "ColorHullShader", "hs_5_0", ShaderFlags.None, EffectFlags.None);
                // Compile the Domain shader code.
                //ShaderBytecode domainShaderByteCode = ShaderBytecode.Compile(dsFileName, "ColorDomainShader", "ds_5_0", ShaderFlags.None, EffectFlags.None);



                //var vsFileNameByteArray = sccs.Properties.Resources.texture1; //for cubes use shader textureTrigChunkVSMOD //textureTrigChunkVS
                //var psFileNameByteArray = sccs.Properties.Resources.texture; //textureTrigChunkPS
                var vsFileNameByteArray = sccsr14sc.Properties.Resources.textureTrigLevelVS;
                var psFileNameByteArray = sccsr14sc.Properties.Resources.textureTrigLevelPS;
                var gsFileNameByteArray = sccsr14sc.Properties.Resources.geometryshaderLevel;

                ShaderBytecode vertexShaderByteCode = ShaderBytecode.Compile(vsFileNameByteArray, "TextureVertexShader", "vs_5_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);
                ShaderBytecode pixelShaderByteCode = ShaderBytecode.Compile(psFileNameByteArray, "TexturePixelShader", "ps_5_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);
                ShaderBytecode geometryshaderbytecode = ShaderBytecode.Compile(gsFileNameByteArray, "GS", "gs_5_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);

                // Create the vertex shader from the buffer.
                //VertexShader = new VertexShader(device, vertexShaderByteCode);
                // Create the pixel shader from the buffer.
                //PixelShader = new PixelShader(device, pixelShaderByteCode);

                //GeometryShader = new GeometryShader(device, geometryshaderbytecode);



                //ShaderBytecode vertexShaderByteCode = ShaderBytecode.Compile(vsFileNameByteArray, "TextureVertexShader", "vs_5_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);
                //ShaderBytecode pixelShaderByteCode = ShaderBytecode.Compile(psFileNameByteArray, "TexturePixelShader", "ps_5_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);
                //ShaderBytecode geometryShaderByteCode = ShaderBytecode.Compile(gsFileName, "GS", "gs_5_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);

                VertexShader = new VertexShader(D3D.device, vertexShaderByteCode);
                PixelShader = new PixelShader(D3D.device, pixelShaderByteCode);
                GeometryShader = new GeometryShader(D3D.device, geometryshaderbytecode);
                // Create the vertex shader from the buffer.
                //HullShader = new HullShader(D3D.device, hullShaderByteCode);
                // Create the vertex shader from the buffer.
                //DomainShader = new DomainShader(D3D.device, domainShaderByteCode);




                inputElements = new InputElement[]
                {
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 1,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "COLOR",// 16
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset =InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "NORMAL", // 12
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification =InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement() // 2.2f
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "TEXCOORD", // 8
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32_Float,
                        Slot = 0,
                        AlignedByteOffset =InputElement.AppendAligned,
                        Classification =InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 1,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 2,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },

                    /*new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 3,
                        Format = SharpDX.DXGI.Format.R32_SInt,
                        Slot = 9,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 4,
                        Format = SharpDX.DXGI.Format.R32_SInt,
                        Slot = 10,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },*/
                };










                Layout = new InputLayout(D3D.device, ShaderSignature.GetInputSignature(vertexShaderByteCode), inputElements);

                contantBuffer = new SharpDX.Direct3D11.Buffer(D3D.device, Utilities.SizeOf<DMatrixBuffer>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);


                vertexShaderByteCode.Dispose();
                //domainShaderByteCode.Dispose();
                //hullShaderByteCode.Dispose();
                pixelShaderByteCode.Dispose();
                geometryshaderbytecode.Dispose();


                Vector4 ambientColor = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);
                Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
                Vector4 lightDirection = new Vector4(1, 0, 0,1.0f);
                Vector4 lightpos0 = new Vector4(1, 0, 0, 1.0f);
                Vector4 dirLight0 = new Vector4(1, 0, 0, 1.0f);

                lightBufferInstChunk[0] = new DLightBufferEr()
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
                    SizeInBytes = Utilities.SizeOf<DLightBufferEr>(),
                    BindFlags = BindFlags.ConstantBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };


                ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(D3D.device, lightBufferDesc);



                //var ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(D3D.device, Utilities.SizeOf<DMatrixBuffer>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);






                /*sclevelgenclass.DIndexType[] indexArray = new sclevelgenclass.DIndexType[SC_Globals.tinyChunkWidth* SC_Globals.tinyChunkHeight* SC_Globals.tinyChunkDepth];
                for (int i = 0; i < SC_Globals.tinyChunkWidth * SC_Globals.tinyChunkHeight * SC_Globals.tinyChunkDepth; i++)
                {
                    indexArray[i] = new sclevelgenclass.DIndexType()
                    {
                        indexPos = originalMap[i],
                    };
                }*/


                /*
                var matrixBufferDescriptionVertex00 = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(sclevelgenclass.DInstanceType)) * instances.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                InstanceBuffer = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertex00);



                var matrixBufferDescriptionVertexcNF = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(sclevelgenclass.heightmapinstance)) * heightmapmatrixbuff.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                instanceBufferHeightmap = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertexcNF);





                var matrixBufferDescriptionVertexcNFm = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(sclevelgenclass.DInstanceMatrix)) * instancesmatrix.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                instancesmatrixbuffer = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertexcNFm);



                var matrixBufferDescriptionVertexcNFmb = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(sclevelgenclass.DInstanceMatrix)) * instancesmatrixb.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                instancesmatrixbufferb = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertexcNFmb);


                var matrixBufferDescriptionVertexcNFmc = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(sclevelgenclass.DInstanceMatrix)) * instancesmatrixc.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                instancesmatrixbufferc = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertexcNFmc);



                var matrixBufferDescriptionVertexcNFmd = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(sclevelgenclass.DInstanceMatrix)) * instancesmatrixd.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                instancesmatrixbufferd = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertexcNFmd);




                var matrixBufferDescriptionVertexx = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(sclevelgenclass.DInstancesByteMap)) * instancesbytemaps.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                instancesbytemapsbuffer = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertexx);


                IndexBuffer = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertex00);



                matrixBufferDescriptionVertex00 = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(sclevelgenclass.DInstanceShipData)) * instancesDataFORWARD.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                InstanceRotationBufferFORWARD = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertex00);

                //var InstanceRotationBufferFORWARD = SharpDX.Direct3D11.Buffer.Create(SC_console_directx.D3D.device, BindFlags.VertexBuffer, instances);

                matrixBufferDescriptionVertex00 = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(sclevelgenclass.DInstanceShipData)) * instancesDataRIGHT.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                InstanceRotationBufferRIGHT = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertex00);

                //var InstanceRotationBufferRIGHT = SharpDX.Direct3D11.Buffer.Create(SC_console_directx.D3D.device, BindFlags.VertexBuffer, instances);

                matrixBufferDescriptionVertex00 = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(sclevelgenclass.DInstanceShipData)) * instancesDataUP.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                InstanceRotationBufferUP = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertex00);*/
                //var InstanceRotationBufferUP = SharpDX.Direct3D11.Buffer.Create(SC_console_directx.D3D.device, BindFlags.VertexBuffer, instances);
                //InstanceRotationMatrixBuffer = SharpDX.Direct3D11.Buffer.Create(SC_console_directx.D3D.device, BindFlags.VertexBuffer, instancesDataForward);




                /*var matrixBufferDescriptionbytemaparray = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(sclevelgenclass.DInstancesccsbytemapxyz)) * instancesccsbytemapxyz.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                instancesbytemapsmatrixbuffer = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionbytemaparray);
                */
                /*
                BufferDescription matrixBufferDescription = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<sclevelgenclass.DInstanceTypeLocW>() * instancesLocationW.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                InstanceBufferLocW = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescription);

                matrixBufferDescription = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<sclevelgenclass.DInstanceTypeLocW>() * instancesLocationH.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                InstanceBufferLocH = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescription);
                */




                /*
                // Setup the description of the dynamic matrix constant buffer that is in the vertex shader.
                BufferDescription someoculusheadsetdirection = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<OVRDir>(),
                    BindFlags = BindFlags.ConstantBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                // Create the constant buffer pointer so we can access the vertex shader constant buffer from within this class.
                someoculusdirbuffer = new SharpDX.Direct3D11.Buffer(D3D.device, someoculusheadsetdirection);

                // Setup the description of the dynamic matrix constant buffer that is in the vertex shader.
                BufferDescription somerandomvaluebufferdesc = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<randomval>() * instancesmatrix.Length,
                    BindFlags = BindFlags.ConstantBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                // Create the constant buffer pointer so we can access the vertex shader constant buffer from within this class.
                somerandomvaluebuffer = new SharpDX.Direct3D11.Buffer(D3D.device, somerandomvaluebufferdesc);



                // Setup the description of the dynamic tessellation constant buffer that is in the hull shader.
                BufferDescription tessellationBufferDesc = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<DTessellationBufferType>(), // was Matrix
                    BindFlags = BindFlags.ConstantBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                // Create the constant buffer pointer so we can access the hull shader constant buffer from within this class.
                ConstantTessellationBuffer = new SharpDX.Direct3D11.Buffer(D3D.device, tessellationBufferDesc);


                */





                // Create a texture sampler state description.
                SamplerStateDescription samplerDesc = new SamplerStateDescription()
                {
                    Filter = Filter.MinMagMipLinear,
                    AddressU = TextureAddressMode.Wrap,
                    AddressV = TextureAddressMode.Wrap,
                    AddressW = TextureAddressMode.Wrap,
                    MipLodBias = 0,
                    MaximumAnisotropy = 1,
                    ComparisonFunction = Comparison.Always,
                    BorderColor = new Color4(0, 0, 0, 0),  // Black Border.
                    MinimumLod = 0,
                    MaximumLod = float.MaxValue
                };

                // Create the texture sampler state.
                samplerState = new SamplerState(D3D.device, samplerDesc);







                /*matrixBufferDescription = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<DInstanceTypeLocD>() * instancesLocationD.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                SharpDX.Direct3D11.Buffer InstanceBufferLocD = new SharpDX.Direct3D11.Buffer(SC_console_directx.D3D.device, matrixBufferDescription);
                */

                /*for (int x = 0; x < numberOfObjectInWidth_; x++)
                {
                    for (int y = 0; y < numberOfObjectInHeight_; y++)
                    {
                        for (int z = 0; z < numberOfObjectInDepth_; z++)
                        {
                            var chunkprimindex = x + numberOfObjectInWidth_ * (y + numberOfObjectInHeight_ * z);

                            arrayOfChunkData[chunkprimindex].InstanceBufferLocW = InstanceBufferLocW;
                            arrayOfChunkData[chunkprimindex].InstanceBufferLocH = InstanceBufferLocH;
                            //arrayOfChunkData[chunkprimindex].InstanceBufferLocD = InstanceBufferLocD;
                        }
                    }
                }*/





                //Console.WriteLine(perceptronRotationRL.Length);
                //Vector3 chunkscreenoriginposition = new Vector3((numberOfObjectInDepth_ * numberOfInstancesPerObjectInDepth_ * planeSize_) * 2, 0, -(numberOfObjectInDepth_ * numberOfInstancesPerObjectInDepth_ * SC_Globals.tinyChunkDepth * planeSize_) * 2);
                //Vector3 chunkscreenoriginposition = new Vector3(10f, 0, -(numberOfObjectInDepth_ * numberOfInstancesPerObjectInDepth_ * numberOfObjectInDepth_ * planeSize_) / 2);





                Matrix worldMat = Matrix.Identity;

                //somechunk = new chunkscreen();






                
                LevelGenerator4 levelgen = new LevelGenerator4();

                //adding more tiles means the level will be bigger
                levelgen.tileAmount = 750; //10500 //15000lag
                                          //15000lag => my computer specs 960gtx 2gb + Amd Ryzen 2600 + 8gb ram 

                levelgen.chunkwidth = 1;
                levelgen.chunkheight = 1;
                levelgen.chunkdepth = 1;
                levelgen.planesize = 1.0f;

                levelgen.chanceUp = 0.25f;
                levelgen.chanceRight = 0.5f;
                levelgen.chanceDown = 0.75f;
                levelgen.SpawnerMoveWaitTime = 0.00000000001f;
                levelgen.BuildingWaitTime = 0.00000000001f;
                levelgen.blockSize = 1.0f;
                levelgen.StartGeneratingVoxelLevel();




                int[] somemap;
                //var arrayOfInstanceIndices = new int[levelgen.typeoftiles.Count][];
                //var arrayOfDVertex = new sclevelgenclass.DVertex[levelgen.typeoftiles.Count][];

                arrayofchunkslod0 = new sclevelgenvert[levelgen.typeoftiles.Count];
                arrayofchunkslod1 = new sclevelgenvert[levelgen.typeoftiles.Count];
                arrayofchunkslod2 = new sclevelgenvert[levelgen.typeoftiles.Count];
                arrayofchunkslod3 = new sclevelgenvert[levelgen.typeoftiles.Count];

                arrayofchunksmapslod0 = new sclevelgenmaps[levelgen.typeoftiles.Count];
                arrayofchunksmapslod1 = new sclevelgenmaps[levelgen.typeoftiles.Count];
                arrayofchunksmapslod2 = new sclevelgenmaps[levelgen.typeoftiles.Count];
                arrayofchunksmapslod3 = new sclevelgenmaps[levelgen.typeoftiles.Count];


                arrayofindexzeromeshlod0 = new sclevelgenclass[levelgen.typeoftiles.Count];
                arrayofindexzeromeshlod1 = new sclevelgenclass[levelgen.typeoftiles.Count];
                arrayofindexzeromeshlod2 = new sclevelgenclass[levelgen.typeoftiles.Count];
                arrayofindexzeromeshlod3 = new sclevelgenclass[levelgen.typeoftiles.Count];
                //arrayofindexzeromeshlod4 = new sclevelgenclass[levelgen.typeoftiles.Count];
                //arrayofindexzeromeshlod5 = new sclevelgenclass[levelgen.typeoftiles.Count];

                arrayOfChunkDatalod0 = new chunkData[levelgen.typeoftiles.Count];
                arrayOfChunkDatalod1 = new chunkData[levelgen.typeoftiles.Count];
                arrayOfChunkDatalod2 = new chunkData[levelgen.typeoftiles.Count];
                arrayOfChunkDatalod3 = new chunkData[levelgen.typeoftiles.Count];
                //arrayOfChunkDatalod4 = new chunkData[levelgen.typeoftiles.Count];
                //arrayOfChunkDatalod5 = new chunkData[levelgen.typeoftiles.Count];

                sclevelgenmaps sometrigchunkmap;
                sclevelgenvert newChunker;

                
                var enumerator1 = levelgen.typeoftiles.GetEnumerator();
                int ichunkleveltile = 0;

                while (enumerator1.MoveNext())
                {
                    var currentTuile = enumerator1.Current;

                    var chunkPos = currentTuile.Key;
                    var typeofterraintile = currentTuile.Value;

                    Vector3 newchunkpos = chunkPos;

                    newchunkpos.X = newchunkpos.X * (tinyChunkWidth * planeSize);
                    newchunkpos.Y = newchunkpos.Y * (tinyChunkHeight * planeSize);
                    newchunkpos.Z = newchunkpos.Z * (tinyChunkDepth * planeSize);

                    sometrigchunkmap = new sclevelgenmaps();
                    newChunker = new sclevelgenvert();
                    //arrayofchunksmapslod0[ichunkleveltile] = new sclevelgenmaps();
                    //arrayofchunkslod0[ichunkleveltile] = new sclevelgenvert();

                    arrayofindexzeromeshlod0[ichunkleveltile] = new sclevelgenclass(new Vector4(0.1f, 0.1f, 0.1f, 1), chunkPos, worldmatofobj, _addtoworld, _mass, _the_world, _is_static, _tag, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, offsetinstance, levelgen); //, instances[ichunkleveltile]        
                    arrayofindexzeromeshlod0[ichunkleveltile].createbytemaps(newchunkpos, levelgen, chunkPos, sometrigchunkmap, typeofterraintile, out somemap, this, 1);
                    
                    arrayofchunksmapslod0[ichunkleveltile] = sometrigchunkmap;
                    arrayofchunkslod0[ichunkleveltile] = newChunker;
                    arrayofchunkslod0[ichunkleveltile].chunkPos = chunkPos;
                    arrayofchunkslod0[ichunkleveltile].map = somemap;
                    
                    
                    sometrigchunkmap = new sclevelgenmaps();
                    newChunker = new sclevelgenvert();
                    arrayofindexzeromeshlod1[ichunkleveltile] = new sclevelgenclass(new Vector4(0.1f, 0.1f, 0.1f, 1), chunkPos, worldmatofobj, _addtoworld, _mass, _the_world, _is_static, _tag, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, offsetinstance, levelgen); //, instances[ichunkleveltile]        
                    arrayofindexzeromeshlod1[ichunkleveltile].createbytemaps(newchunkpos, levelgen, chunkPos, sometrigchunkmap, typeofterraintile, out somemap, this, 2);
                    arrayofchunksmapslod1[ichunkleveltile] = sometrigchunkmap;         
                    arrayofchunkslod1[ichunkleveltile] = newChunker;
                    arrayofchunkslod1[ichunkleveltile].chunkPos = chunkPos;
                    arrayofchunkslod1[ichunkleveltile].map = somemap;
                        
                    
                    sometrigchunkmap = new sclevelgenmaps();
                    newChunker = new sclevelgenvert();
                    arrayofindexzeromeshlod2[ichunkleveltile] = new sclevelgenclass(new Vector4(0.1f, 0.1f, 0.1f, 1), chunkPos, worldmatofobj, _addtoworld, _mass, _the_world, _is_static, _tag, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, offsetinstance, levelgen); //, instances[ichunkleveltile]        
                    arrayofindexzeromeshlod2[ichunkleveltile].createbytemaps(newchunkpos, levelgen, chunkPos, sometrigchunkmap, typeofterraintile, out somemap, this, 3);
                    arrayofchunksmapslod2[ichunkleveltile] = sometrigchunkmap;
                    arrayofchunkslod2[ichunkleveltile] = newChunker;
                    arrayofchunkslod2[ichunkleveltile].chunkPos = chunkPos;
                    arrayofchunkslod2[ichunkleveltile].map = somemap;
                    
                    
                    sometrigchunkmap = new sclevelgenmaps();
                    newChunker = new sclevelgenvert();
                    arrayofindexzeromeshlod3[ichunkleveltile] = new sclevelgenclass(new Vector4(0.1f, 0.1f, 0.1f, 1), chunkPos, worldmatofobj, _addtoworld, _mass, _the_world, _is_static, _tag, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, offsetinstance, levelgen); //, instances[ichunkleveltile]        
                    arrayofindexzeromeshlod3[ichunkleveltile].createbytemaps(newchunkpos, levelgen, chunkPos, sometrigchunkmap, typeofterraintile, out somemap, this, 4);
                    arrayofchunksmapslod3[ichunkleveltile] = sometrigchunkmap;
                    arrayofchunkslod3[ichunkleveltile] = newChunker;
                    arrayofchunkslod3[ichunkleveltile].chunkPos = chunkPos;
                    arrayofchunkslod3[ichunkleveltile].map = somemap;
                    









                    /*
                    arrayofindexzeromeshlod4[ichunkleveltile] = new sclevelgenclass(new Vector4(0.1f, 0.1f, 0.1f, 1), chunkPos, worldmatofobj, _addtoworld, _mass, _the_world, _is_static, _tag, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, offsetinstance, levelgen); //, instances[ichunkleveltile]        
                    arrayofindexzeromeshlod4[ichunkleveltile].createbytemaps(newchunkpos, levelgen, chunkPos, sometrigchunkmap, typeofterraintile, out somemap, this, 1);

                    arrayofindexzeromeshlod5[ichunkleveltile] = new sclevelgenclass(new Vector4(0.1f, 0.1f, 0.1f, 1), chunkPos, worldmatofobj, _addtoworld, _mass, _the_world, _is_static, _tag, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, offsetinstance, levelgen); //, instances[ichunkleveltile]        
                    arrayofindexzeromeshlod5[ichunkleveltile].createbytemaps(newchunkpos, levelgen, chunkPos, sometrigchunkmap, typeofterraintile, out somemap, this, 1);
                    */








                    ichunkleveltile++;
                }






                int[] indicesArray0;
                sclevelgenclass.DVertex[] arrayOfD;

                //Vector3 someoffset = new Vector3(2,2,2) * 0.15f;
                enumerator1 = levelgen.typeoftiles.GetEnumerator();
                ichunkleveltile = 0;

                while (enumerator1.MoveNext())
                {
                    var currentTuile = enumerator1.Current;
                    var chunkPos = currentTuile.Key;

                    Vector3 newchunkpos = chunkPos;

                    newchunkpos.X = newchunkpos.X * (tinyChunkWidth * planeSize);
                    newchunkpos.Y = newchunkpos.Y * (tinyChunkHeight * planeSize);
                    newchunkpos.Z = newchunkpos.Z * (tinyChunkDepth * planeSize);

                    //chunkPos.X *= 2.0f;
                    //chunkPos.Y *= 2.0f;
                    //chunkPos.Z *= 2.0f;
             
                    var typeofterraintile = currentTuile.Value;



                    //int[] tester = arrayofchunks[ichunkleveltile].startBuildingArray(newchunkpos, out indicesArray0, out somemap, out arrayOfD, planeSize, levelgen, chunkPos, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, typeofterraintile, this, arrayofchunksmaps[ichunkleveltile].map);

                    //arrayOfInstanceIndices[ichunkleveltile] = indicesArray0;
                    //arrayOfDVertex[ichunkleveltile] = arrayOfD;
                    arrayofchunkslod0[ichunkleveltile] = arrayofindexzeromeshlod0[ichunkleveltile].createchunkvert(newchunkpos, levelgen, chunkPos, arrayofchunkslod0[ichunkleveltile], typeofterraintile, out somemap, out indicesArray0, out arrayOfD, this, arrayofchunksmapslod0[ichunkleveltile], 1);

                    if (indicesArray0.Length > 0)
                    {
                        var VertexBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, arrayOfD);
                        var IndicesBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.IndexBuffer, indicesArray0);

                        arrayOfChunkDatalod0[ichunkleveltile] = new chunkData();
                        arrayOfChunkDatalod0[ichunkleveltile].chunkPos = chunkPos;
                        arrayOfChunkDatalod0[ichunkleveltile].realpos = newchunkpos;
                        arrayOfChunkDatalod0[ichunkleveltile].VertexShader = VertexShader;
                        arrayOfChunkDatalod0[ichunkleveltile].PixelShader = PixelShader;
                        arrayOfChunkDatalod0[ichunkleveltile].GeometryShader = GeometryShader;
                        arrayOfChunkDatalod0[ichunkleveltile].Layout = Layout;
                        arrayOfChunkDatalod0[ichunkleveltile].planeSize = planeSize;
                        arrayOfChunkDatalod0[ichunkleveltile].samplerState = samplerState;
                        arrayOfChunkDatalod0[ichunkleveltile].arrayofverts = arrayOfD;
                        arrayOfChunkDatalod0[ichunkleveltile].arrayofindices = indicesArray0;
                        arrayOfChunkDatalod0[ichunkleveltile].switchForRender = 1;
                        arrayOfChunkDatalod0[ichunkleveltile].IndicesBuffer = IndicesBuffer;
                        arrayOfChunkDatalod0[ichunkleveltile].vertexBuffer = VertexBuffer;
                        arrayOfChunkDatalod0[ichunkleveltile].constantLightBuffer = ConstantLightBuffer;
                        arrayOfChunkDatalod0[ichunkleveltile].constantMatrixPosBuffer = contantBuffer;
                        arrayOfChunkDatalod0[ichunkleveltile].lightBuffer = lightBufferInstChunk;
                        arrayOfChunkDatalod0[ichunkleveltile].worldMatrix = worldmatofobj; //mainObjectInstance1stCubeMatrix
                        //arrayOfChunkDatalod0[ichunkleveltile].shaderOfChunk = new sclevelgenclassshader(D3D.device);
                    }







                    arrayofindexzeromeshlod1[ichunkleveltile].createchunkvert(newchunkpos, levelgen, chunkPos, arrayofchunkslod1[ichunkleveltile], typeofterraintile, out somemap, out indicesArray0, out arrayOfD, this, arrayofchunksmapslod1[ichunkleveltile], 2);

                    if (indicesArray0.Length > 0)
                    {
                        var VertexBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, arrayOfD);
                        var IndicesBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.IndexBuffer, indicesArray0);

                        arrayOfChunkDatalod1[ichunkleveltile] = new chunkData();
                        arrayOfChunkDatalod1[ichunkleveltile].chunkPos = chunkPos;
                        arrayOfChunkDatalod1[ichunkleveltile].realpos = newchunkpos;
                        arrayOfChunkDatalod1[ichunkleveltile].VertexShader = VertexShader;
                        arrayOfChunkDatalod1[ichunkleveltile].PixelShader = PixelShader;
                        arrayOfChunkDatalod1[ichunkleveltile].GeometryShader = GeometryShader;
                        arrayOfChunkDatalod1[ichunkleveltile].Layout = Layout;
                        arrayOfChunkDatalod1[ichunkleveltile].planeSize = planeSize;
                        arrayOfChunkDatalod1[ichunkleveltile].samplerState = samplerState;
                        arrayOfChunkDatalod1[ichunkleveltile].arrayofverts = arrayOfD;
                        arrayOfChunkDatalod1[ichunkleveltile].arrayofindices = indicesArray0;
                        arrayOfChunkDatalod1[ichunkleveltile].switchForRender = 1;
                        arrayOfChunkDatalod1[ichunkleveltile].IndicesBuffer = IndicesBuffer;
                        arrayOfChunkDatalod1[ichunkleveltile].vertexBuffer = VertexBuffer;
                        arrayOfChunkDatalod1[ichunkleveltile].constantLightBuffer = ConstantLightBuffer;
                        arrayOfChunkDatalod1[ichunkleveltile].constantMatrixPosBuffer = contantBuffer;
                        arrayOfChunkDatalod1[ichunkleveltile].lightBuffer = lightBufferInstChunk;
                        arrayOfChunkDatalod1[ichunkleveltile].worldMatrix = worldmatofobj; //mainObjectInstance1stCubeMatrix
                        //arrayOfChunkDatalod1[ichunkleveltile].shaderOfChunk = new sclevelgenclassshader(D3D.device);

                    }



                    
                    arrayofindexzeromeshlod2[ichunkleveltile].createchunkvert(newchunkpos, levelgen, chunkPos, arrayofchunkslod2[ichunkleveltile], typeofterraintile, out somemap, out indicesArray0, out arrayOfD, this, arrayofchunksmapslod2[ichunkleveltile], 3);
                    if (indicesArray0.Length > 0)
                    {
                        var VertexBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, arrayOfD);
                        var IndicesBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.IndexBuffer, indicesArray0);

                        arrayOfChunkDatalod2[ichunkleveltile] = new chunkData();
                        arrayOfChunkDatalod2[ichunkleveltile].chunkPos = chunkPos;
                        arrayOfChunkDatalod2[ichunkleveltile].realpos = newchunkpos;
                        arrayOfChunkDatalod2[ichunkleveltile].VertexShader = VertexShader;
                        arrayOfChunkDatalod2[ichunkleveltile].PixelShader = PixelShader;
                        arrayOfChunkDatalod2[ichunkleveltile].GeometryShader = GeometryShader;
                        arrayOfChunkDatalod2[ichunkleveltile].Layout = Layout;
                        arrayOfChunkDatalod2[ichunkleveltile].planeSize = planeSize;
                        arrayOfChunkDatalod2[ichunkleveltile].samplerState = samplerState;
                        arrayOfChunkDatalod2[ichunkleveltile].arrayofverts = arrayOfD;
                        arrayOfChunkDatalod2[ichunkleveltile].arrayofindices = indicesArray0;
                        arrayOfChunkDatalod2[ichunkleveltile].switchForRender = 1;
                        arrayOfChunkDatalod2[ichunkleveltile].IndicesBuffer = IndicesBuffer;
                        arrayOfChunkDatalod2[ichunkleveltile].vertexBuffer = VertexBuffer;
                        arrayOfChunkDatalod2[ichunkleveltile].constantLightBuffer = ConstantLightBuffer;
                        arrayOfChunkDatalod2[ichunkleveltile].constantMatrixPosBuffer = contantBuffer;
                        arrayOfChunkDatalod2[ichunkleveltile].lightBuffer = lightBufferInstChunk;
                        arrayOfChunkDatalod2[ichunkleveltile].worldMatrix = worldmatofobj; //mainObjectInstance1stCubeMatrix
                        //arrayOfChunkDatalod2[ichunkleveltile].shaderOfChunk = new sclevelgenclassshader(D3D.device);
                    }







                    
                    arrayofindexzeromeshlod3[ichunkleveltile].createchunkvert(newchunkpos, levelgen, chunkPos, arrayofchunkslod3[ichunkleveltile], typeofterraintile, out somemap, out indicesArray0, out arrayOfD, this, arrayofchunksmapslod3[ichunkleveltile], 4);
                    if (indicesArray0.Length > 0)
                    {
                        var VertexBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, arrayOfD);
                       var IndicesBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.IndexBuffer, indicesArray0);

                        arrayOfChunkDatalod3[ichunkleveltile] = new chunkData();
                        arrayOfChunkDatalod3[ichunkleveltile].chunkPos = chunkPos;
                        arrayOfChunkDatalod3[ichunkleveltile].realpos = newchunkpos;
                        arrayOfChunkDatalod3[ichunkleveltile].VertexShader = VertexShader;
                        arrayOfChunkDatalod3[ichunkleveltile].PixelShader = PixelShader;
                        arrayOfChunkDatalod3[ichunkleveltile].GeometryShader = GeometryShader;
                        arrayOfChunkDatalod3[ichunkleveltile].Layout = Layout;
                        arrayOfChunkDatalod3[ichunkleveltile].planeSize = planeSize;
                        arrayOfChunkDatalod3[ichunkleveltile].samplerState = samplerState;
                        arrayOfChunkDatalod3[ichunkleveltile].arrayofverts = arrayOfD;
                        arrayOfChunkDatalod3[ichunkleveltile].arrayofindices = indicesArray0;
                        arrayOfChunkDatalod3[ichunkleveltile].switchForRender = 1;
                        arrayOfChunkDatalod3[ichunkleveltile].IndicesBuffer = IndicesBuffer;
                        arrayOfChunkDatalod3[ichunkleveltile].vertexBuffer = VertexBuffer;
                        arrayOfChunkDatalod3[ichunkleveltile].constantLightBuffer = ConstantLightBuffer;
                        arrayOfChunkDatalod3[ichunkleveltile].constantMatrixPosBuffer = contantBuffer;
                        arrayOfChunkDatalod3[ichunkleveltile].lightBuffer = lightBufferInstChunk;
                        arrayOfChunkDatalod3[ichunkleveltile].worldMatrix = worldmatofobj; //mainObjectInstance1stCubeMatrix
                        arrayOfChunkDatalod3[ichunkleveltile].shaderOfChunk = new sclevelgenclassshader(D3D.device);
                    }


                    /*
                    arrayofindexzeromeshlod4[ichunkleveltile].createchunkvert(newchunkpos, levelgen, chunkPos, arrayofchunks[ichunkleveltile], typeofterraintile, out somemap, out indicesArray0, out arrayOfD, this, arrayofchunksmaps[ichunkleveltile], 5);
                    VertexBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, arrayOfD);
                    IndicesBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.IndexBuffer, indicesArray0);

                    arrayOfChunkDatalod4[ichunkleveltile] = new chunkData();
                    arrayOfChunkDatalod4[ichunkleveltile].chunkPos = chunkPos;
                    arrayOfChunkDatalod4[ichunkleveltile].realpos = newchunkpos;
                    arrayOfChunkDatalod4[ichunkleveltile].VertexShader = VertexShader;
                    arrayOfChunkDatalod4[ichunkleveltile].PixelShader = PixelShader;
                    arrayOfChunkDatalod4[ichunkleveltile].GeometryShader = GeometryShader;
                    arrayOfChunkDatalod4[ichunkleveltile].Layout = Layout;
                    arrayOfChunkDatalod4[ichunkleveltile].planeSize = planeSize;
                    arrayOfChunkDatalod4[ichunkleveltile].samplerState = samplerState;
                    arrayOfChunkDatalod4[ichunkleveltile].arrayofverts = arrayOfD;
                    arrayOfChunkDatalod4[ichunkleveltile].arrayofindices = indicesArray0;
                    arrayOfChunkDatalod4[ichunkleveltile].switchForRender = 1;
                    arrayOfChunkDatalod4[ichunkleveltile].IndicesBuffer = IndicesBuffer;
                    arrayOfChunkDatalod4[ichunkleveltile].vertexBuffer = VertexBuffer;
                    arrayOfChunkDatalod4[ichunkleveltile].constantLightBuffer = ConstantLightBuffer;
                    arrayOfChunkDatalod4[ichunkleveltile].constantMatrixPosBuffer = contantBuffer;
                    arrayOfChunkDatalod4[ichunkleveltile].lightBuffer = lightBufferInstChunk;
                    arrayOfChunkDatalod4[ichunkleveltile].worldMatrix = worldmatofobj; //mainObjectInstance1stCubeMatrix
                    arrayOfChunkDatalod4[ichunkleveltile].shaderOfChunk = new sclevelgenclassshader(D3D.device);

                    arrayofindexzeromeshlod5[ichunkleveltile].createchunkvert(newchunkpos, levelgen, chunkPos, arrayofchunks[ichunkleveltile], typeofterraintile, out somemap, out indicesArray0, out arrayOfD, this, arrayofchunksmaps[ichunkleveltile], 6);
                    VertexBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, arrayOfD);
                    IndicesBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.IndexBuffer, indicesArray0);

                    arrayOfChunkDatalod5[ichunkleveltile] = new chunkData();
                    arrayOfChunkDatalod5[ichunkleveltile].chunkPos = chunkPos;
                    arrayOfChunkDatalod5[ichunkleveltile].realpos = newchunkpos;
                    arrayOfChunkDatalod5[ichunkleveltile].VertexShader = VertexShader;
                    arrayOfChunkDatalod5[ichunkleveltile].PixelShader = PixelShader;
                    arrayOfChunkDatalod5[ichunkleveltile].GeometryShader = GeometryShader;
                    arrayOfChunkDatalod5[ichunkleveltile].Layout = Layout;
                    arrayOfChunkDatalod5[ichunkleveltile].planeSize = planeSize;
                    arrayOfChunkDatalod5[ichunkleveltile].samplerState = samplerState;
                    arrayOfChunkDatalod5[ichunkleveltile].arrayofverts = arrayOfD;
                    arrayOfChunkDatalod5[ichunkleveltile].arrayofindices = indicesArray0;
                    arrayOfChunkDatalod5[ichunkleveltile].switchForRender = 1;
                    arrayOfChunkDatalod5[ichunkleveltile].IndicesBuffer = IndicesBuffer;
                    arrayOfChunkDatalod5[ichunkleveltile].vertexBuffer = VertexBuffer;
                    arrayOfChunkDatalod5[ichunkleveltile].constantLightBuffer = ConstantLightBuffer;
                    arrayOfChunkDatalod5[ichunkleveltile].constantMatrixPosBuffer = contantBuffer;
                    arrayOfChunkDatalod5[ichunkleveltile].lightBuffer = lightBufferInstChunk;
                    arrayOfChunkDatalod5[ichunkleveltile].worldMatrix = worldmatofobj; //mainObjectInstance1stCubeMatrix
                    arrayOfChunkDatalod5[ichunkleveltile].shaderOfChunk = new sclevelgenclassshader(D3D.device);
                    */




                    /*
                    for (int v = 0; v < arrayOfD.Length; v++)
                    {
                        arrayOfD[v].position.X += arrayofchunks[ichunkleveltile].chunkPos.X * (tinyChunkWidth * planeSize);
                        arrayOfD[v].position.Y += arrayofchunks[ichunkleveltile].chunkPos.Y * (tinyChunkHeight * planeSize);
                        arrayOfD[v].position.Z += arrayofchunks[ichunkleveltile].chunkPos.Z * (tinyChunkDepth * planeSize);
                    }*/


                    /*
                    var matrixBufferDescriptionVertex = new BufferDescription()
                    {
                        Usage = ResourceUsage.Dynamic,
                        SizeInBytes = Marshal.SizeOf(typeof(sclevelgenclass.DVertex)) * arrayOfD.Length,
                        BindFlags = BindFlags.VertexBuffer,
                        CpuAccessFlags = CpuAccessFlags.Write,
                        OptionFlags = ResourceOptionFlags.None,
                        StructureByteStride = 0
                    };

                    var VertexBuffer = new SharpDX.Direct3D11.Buffer(scgraphics.scdirectx.D3D.device, matrixBufferDescriptionVertex);
                    */


                    /*matrixBufferDescriptionVertex = new BufferDescription()
                    {
                        Usage = ResourceUsage.Dynamic,
                        SizeInBytes = Marshal.SizeOf(typeof(int)) * indicesArray0.Length,
                        BindFlags = BindFlags.IndexBuffer,
                        CpuAccessFlags = CpuAccessFlags.Write,
                        OptionFlags = ResourceOptionFlags.None,
                        StructureByteStride = 0
                    };
                    var IndicesBuffer = new SharpDX.Direct3D11.Buffer(scgraphics.scdirectx.D3D.device, matrixBufferDescriptionVertex);
                    */

                    //var VertexBuffer = SharpDX.Direct3D11.Buffer.Create(scgraphics.scdirectx.D3D.device, BindFlags.VertexBuffer, somelevelgenprim[0].arrayOfChunkData[somearrayindex].arrayofverts);
                    //var IndicesBuffer = SharpDX.Direct3D11.Buffer.Create(scgraphics.scdirectx.D3D.device, BindFlags.IndexBuffer, somelevelgenprim[0].arrayOfChunkData[somearrayindex].arrayofindices);


                    ichunkleveltile++;
                }







                /*
                //unLOADING CHUNK to XML
                //unLOADING CHUNK to XML
                string pathofrelease = Directory.GetCurrentDirectory();
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

                for (int i = 0; i < arrayofchunkslod0.Length; i++)
                {                             
                    //writer.WriteStartElement("bytemap"); //open 4
                    writer.WriteStartElement("x" + arrayofchunkslod0[i].chunkPos.X + "y" + arrayofchunkslod0[i].chunkPos.Y + "z" + arrayofchunkslod0[i].chunkPos.Z); //open 4

                    int[] somemapp = arrayofchunksmapslod0[i].map;//
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
                for (int i = 0; i < arrayofchunkslod0.Length; i++)
                {
                    arrayofchunkslod0[i].map = null;
                    arrayofchunksmapslod0[i].map = null;

                    arrayofchunkslod1[i].map = null;
                    arrayofchunksmapslod1[i].map = null;

                    arrayofchunkslod2[i].map = null;
                    arrayofchunksmapslod2[i].map = null;
                }*/










                shaderOfChunk = new sclevelgenclassshader(D3D.device);

            }
            catch (Exception ex)
            {
                Program.MessageBox((IntPtr)0, ex.ToString(), "sccs", 0);
            }
        }
    }
}
