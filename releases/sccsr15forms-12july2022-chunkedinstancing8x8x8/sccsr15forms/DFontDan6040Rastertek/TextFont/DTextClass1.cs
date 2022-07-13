using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

//using DSharpDXRastertek.Tut12.Graphics.TextFont;

using sccsr15forms;

namespace sccsr15forms// DSharpDXRastertek.Tut13.Graphics.TextFont
{
    public class DTextClass                 // 276 lines
    {
        // Structs
        [StructLayout(LayoutKind.Sequential)]
        public struct DSentence
        {
            public SharpDX.Direct3D11.Buffer VertexBuffer;
            public SharpDX.Direct3D11.Buffer IndexBuffer;
            public int VertexCount;
            public int IndexCount;
            public int MaxLength;
            public float red;
            public float green;
            public float blue;
            public string sentenceText;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct DVertex
        {
            public Vector3 position;
            public Vector2 texture;
        }

        // Properties
        public DFont Font;
        public DFontShader FontShader;
        public int ScreenWidth;
        public int ScreenHeight;
        public Matrix BaseViewMatrix;
        public DSentence[] sentences = new DSentence[51];

        // Methods
        public bool Initialize(SharpDX.Direct3D11.Device device, DeviceContext deviceContext, IntPtr windowHandle, int screanWidth, int screenHeight, Matrix baseViewMatrix)
        {
            // Store the screen width and height.
            ScreenWidth = screanWidth;
            ScreenHeight = screenHeight;

            // Store the base view matrix.
            BaseViewMatrix = baseViewMatrix;

            // Create the font object.
            Font = new DFont();

            // Initialize the font object.
            if (!Font.Initialize(device, "fontdata.txt", "font.bmp"))
                return false;

            // Create the font shader object.
            FontShader = new DFontShader();

            // Initialize the font shader object.
            if (!FontShader.Initialize(device, windowHandle))
                return false;

            // Initialize the first sentence.
            if (!InitializeSentence(out sentences[0], 20, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[0], "playerx", 20, 20, 1, 1, 1, deviceContext))
                return false;

            // Initialize the second sentence.
            if (!InitializeSentence(out sentences[1], 20, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[1], "playery", 20, 40, 1, 1, 0, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[2], 20, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[2], "playerz: ", 20, 60, 0, 1, 1, deviceContext))
                return false;



            /*
            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[3], 32, device))
                return false;
            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[3], "Vertex total count: ", 20, 80, 0, 1, 1, deviceContext))
                return false;


            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[4], 32, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[4], "Triangle total count: ", 20, 100, 0, 1, 1, deviceContext))
                return false;



            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[5], 32, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[5], "Vertex culled count: ", 20, 120, 0, 1, 1, deviceContext))
                return false;


            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[6], 32, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[6], "Triangle culled count: ", 20, 140, 0, 1, 1, deviceContext))
                return false;
            */





            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[3], 32, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[3], "total meshes: ", 20, 80, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[4], 128, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[4], "total vertices: ", 20, 100, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[5], 128, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[5], "total triangles: ", 20, 120, 0, 1, 1, deviceContext))
                return false;






            //LOD 0 MESHES DATA AS LEVEL OF DETAIL 1
            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[6], 32, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[6], "LOD1 MESHES DATA: ", 20, 140, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[7], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[7], "total meshes: ", 20, 160, 0, 1, 1, deviceContext))
                return false;


            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[8], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[8], "total meshes culled: ", 20, 180, 0, 1, 1, deviceContext))
                return false;


            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[9], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[9], "distance culled: ", 20, 200, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[10], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[10], "dot frustrum culled: ", 20, 220, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[11], 128, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[11], "total vertices: ", 20, 240, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[12], 128, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[12], "total vertices culled: ", 20, 260, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[13], 128, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[13], "total triangles: ", 20, 280, 0, 1, 1, deviceContext))
                return false;
            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[14], 1280, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[14], "total triangles culled: ", 20, 300, 0, 1, 1, deviceContext))
                return false;





            int somerowcount = 20;

            //LOD 1 MESHES DATA AS LEVEL OF DETAIL 2
            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[15], 32, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[15], "LOD2 MESHES DATA: ", 20, 320, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[16], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[16], "total meshes: ", 20, 340, 0, 1, 1, deviceContext))
                return false;


            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[17], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[17], "total meshes culled: ", 20, 360, 0, 1, 1, deviceContext))
                return false;


            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[18], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[18], "distance culled: ", 20, 380, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[19], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[19], "dot frustrum culled: ", 20, 400, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[20], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[20], "total vertices: ", 20, 420, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[21], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[21], "total vertices culled: ", 20, 440, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[22], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[22], "total triangles: ", 20, 460, 0, 1, 1, deviceContext))
                return false;
            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[23], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[23], "total triangles culled: ", 20, 480, 0, 1, 1, deviceContext))
                return false;









            //LOD 2 MESHES DATA AS LEVEL OF DETAIL 3
            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[24], 32, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[24], "LOD3 MESHES DATA: ", 20, 500, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[25], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[25], "total meshes: ", 20, 520, 0, 1, 1, deviceContext))
                return false;


            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[26], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[26], "total meshes culled: ", 20, 540, 0, 1, 1, deviceContext))
                return false;


            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[27], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[27], "distance culled: ", 20, 560, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[28], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[28], "dot frustrum culled: ", 20, 580, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[29], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[29], "total vertices: ", 20, 600, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[30], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[30], "total vertices culled: ", 20, 620, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[31], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[31], "total triangles: ", 20, 640, 0, 1, 1, deviceContext))
                return false;
            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[32], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[32], "total triangles culled: ", 20, 660, 0, 1, 1, deviceContext))
                return false;







            //LOD 3 MESHES DATA AS LEVEL OF DETAIL 4
            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[33], 32, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[33], "LOD4 MESHES DATA: ", 20, 680, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[34], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[34], "total meshes: ", 20, 700, 0, 1, 1, deviceContext))
                return false;


            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[35], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[35], "total meshes culled: ", 20, 720, 0, 1, 1, deviceContext))
                return false;


            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[36], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[36], "distance culled: ", 20, 740, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[37], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[37], "dot frustrum culled: ", 20, 760, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[38], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[38], "total vertices: ", 20, 780, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[39], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[39], "total vertices culled: ", 20, 800, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[40], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[40], "total triangles: ", 20, 820, 0, 1, 1, deviceContext))
                return false;
            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[41], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[41], "total triangles culled: ", 20, 840, 0, 1, 1, deviceContext))
                return false;












            //LOD 4 MESHES DATA AS LEVEL OF DETAIL 5
            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[42], 32, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[42], "LOD5 MESHES DATA: ", 20, 860, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[43], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[43], "total meshes: ", 20, 880, 0, 1, 1, deviceContext))
                return false;


            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[44], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[44], "total meshes culled: ", 20, 900, 0, 1, 1, deviceContext))
                return false;


            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[45], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[45], "distance culled: ", 20, 920, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[46], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[46], "dot frustrum culled: ", 20, 940, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[47], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[47], "total vertices: ", 20, 960, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[48], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[48], "total vertices culled: ", 20, 980, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[49], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[49], "total triangles: ", 20, 1000, 0, 1, 1, deviceContext))
                return false;
            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[50], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[50], "total triangles culled: ", 20, 1020, 0, 1, 1, deviceContext))
                return false;









            /*
            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[3], 128, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[3], "distance culled mesh data count: ", 20, 80, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[4], 128, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[4], "dot frustrum culled mesh data count: ", 20, 100, 0, 1, 1, deviceContext))
                return false;


            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[5], 32, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[5], "LOD1 meshes data: ", 20, 120, 0, 1, 1, deviceContext))
                return false;
            */













            /*// Initialize the Third sentence.
            if (!InitializeSentence(out sentences[6], 256, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[6], "LOD2 meshes data count: ", 20, 140, 0, 1, 1, deviceContext))
                return false;


            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[7], 256, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[7], "LOD3 meshes data count:", 20, 160, 0, 1, 1, deviceContext))
                return false;


            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[8], 256, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[8], "LOD4 meshes data count:", 20, 180, 0, 1, 1, deviceContext))
                return false;


            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[9], 256, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[9], "LOD5 meshes data count:", 20, 200, 0, 1, 1, deviceContext))
                return false;
            */








            return true;
        }

        public void Shutdown()
        {
            // Release all sentances however many there may be.
            foreach (DSentence sentance in sentences)
                ReleaseSentences(sentance);
            sentences = null;

            // Release the font shader object.
            FontShader?.Shuddown();
            FontShader = null;
            // Release the font object.
            Font?.Shutdown();
            Font = null;
        }
        public bool Render(DeviceContext deviceContext, Matrix worldMatrix, Matrix orthoMatrix)
        {
            // Render all Sentances however many there mat be.
            foreach (DSentence sentance in sentences)
            {
                if (!RenderSentence(deviceContext, sentance, worldMatrix, orthoMatrix))
                    return false;
            }

            return true;
        }
        public bool setoverlaydata(float playerx, float playery, float playerz, updateSec updatesec, DeviceContext deviceContext)
        {
            string somestring;

            // Setup the mouseX string.
            somestring = "Player X: " + playerx.ToString();

            // Update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[0], somestring, 20, 20, 1, 1, 1, deviceContext))
            {
                Console.WriteLine("Player x fail text");
                return false;
            }

            // Setup the mouseY string.
            somestring = "Player Y: " + playery.ToString();

            // Update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[1], somestring, 20, 40, 1, 1, 1, deviceContext))
            {
                Console.WriteLine("Player y fail text");
                return false;
            }

            // Setup the mouseY string.
            somestring = "Player Z: " + playerz.ToString();

            // Update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[2], somestring, 20, 60, 1, 1, 1, deviceContext))
            {
                Console.WriteLine("Player z fail text");
                return false;
            }



            // Setup the mouseY string.
            somestring = "Player Z: " + playerz.ToString();

            // Update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[2], somestring, 20, 60, 1, 1, 1, deviceContext))
            {
                Console.WriteLine("Player z fail text");
                return false;
            }


            if (updatesec != null)
            {

                somestring = "total meshes: " + " " + updatesec.totalmesh.ToString();
                if (!UpdateSentece(ref sentences[3], somestring, 20, 80, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 3 fail text");
                    return false;
                }

                somestring = "total vertices: " + " " + updatesec.totalvert.ToString();
                if (!UpdateSentece(ref sentences[4], somestring, 20, 100, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 4 fail text");
                    return false;
                }

                somestring = "total triangles: " + " " + updatesec.totaltrigs.ToString();
                if (!UpdateSentece(ref sentences[5], somestring, 20, 120, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 5 fail text");
                    return false;
                }



                /*somestring = "LOD1 MESHES DATA: " + " " + updatesec.totaltrigs.ToString();
                if (!UpdateSentece(ref sentences[6], somestring, 20, 140, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 6 fail text");
                    return false;
                }*/


                somestring = "total meshes: " + " " + updatesec.countmeshtotallod0.ToString();
                if (!UpdateSentece(ref sentences[7], somestring, 20, 160, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 7 fail text");
                    return false;
                }


                somestring = "total meshes culled: " + " " + updatesec.countmeshculledlod0.ToString();
                if (!UpdateSentece(ref sentences[8], somestring, 20, 180, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 8 fail text");
                    return false;
                }

                somestring = "distance culled: " + " " + updatesec.countmeshdistculledlod0.ToString();
                if (!UpdateSentece(ref sentences[9], somestring, 20, 200, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 9 fail text");
                    return false;
                }

                somestring = "dot frustrum culled: " + " " + updatesec.countmeshfrustculledlod0.ToString();
                if (!UpdateSentece(ref sentences[10], somestring, 20, 220, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 10 fail text");
                    return false;
                }

                somestring = "total vertices: " + " " + updatesec.countmeshverttotallod0.ToString();
                if (!UpdateSentece(ref sentences[11], somestring, 20, 240, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 11 fail text");
                    return false;
                }

                somestring = "total vertices culled: " + " " + updatesec.countmeshvertculledlod0.ToString();
                if (!UpdateSentece(ref sentences[12], somestring, 20, 260, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 12 fail text");
                    return false;
                }

                somestring = "total triangles: " + " " + updatesec.countmeshtrigtotallod0.ToString();
                if (!UpdateSentece(ref sentences[13], somestring, 20, 280, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 13 fail text");
                    return false;
                }

                somestring = "total triangles culled: " + " " + updatesec.countmeshtrigculledlod0.ToString();
                if (!UpdateSentece(ref sentences[14], somestring, 20, 300, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 14 fail text");
                    return false;
                }





                //LOD1 LEVEL OF DETAIL 2
                /*somestring = "LOD2 MESHES DATA: " + " " + updatesec.totaltrigs.ToString();
                if (!UpdateSentece(ref sentences[15], somestring, 20, 320, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 6 fail text");
                    return false;
                }*/


                somestring = "total meshes: " + " " + updatesec.countmeshtotallod1.ToString();
                if (!UpdateSentece(ref sentences[16], somestring, 20, 340, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 7 fail text");
                    return false;
                }


                somestring = "total meshes culled: " + " " + updatesec.countmeshculledlod1.ToString();
                if (!UpdateSentece(ref sentences[17], somestring, 20, 360, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 8 fail text");
                    return false;
                }

                somestring = "distance culled: " + " " + updatesec.countmeshdistculledlod1.ToString();
                if (!UpdateSentece(ref sentences[18], somestring, 20, 380, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 9 fail text");
                    return false;
                }

                somestring = "dot frustrum culled: " + " " + updatesec.countmeshfrustculledlod1.ToString();
                if (!UpdateSentece(ref sentences[19], somestring, 20, 400, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 10 fail text");
                    return false;
                }

                somestring = "total vertices: " + " " + updatesec.countmeshverttotallod1.ToString();
                if (!UpdateSentece(ref sentences[20], somestring, 20, 420, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 11 fail text");
                    return false;
                }

                somestring = "total vertices culled: " + " " + updatesec.countmeshvertculledlod1.ToString();
                if (!UpdateSentece(ref sentences[21], somestring, 20, 440, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 12 fail text");
                    return false;
                }

                somestring = "total triangles: " + " " + updatesec.countmeshtrigtotallod1.ToString();
                if (!UpdateSentece(ref sentences[22], somestring, 20, 460, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 13 fail text");
                    return false;
                }

                somestring = "total triangles culled: " + " " + updatesec.countmeshtrigculledlod1.ToString();
                if (!UpdateSentece(ref sentences[23], somestring, 20, 480, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 14 fail text");
                    return false;
                }



                //LOD2 LEVEL OF DETAIL 3
                /*somestring = "LOD3 MESHES DATA: " + " " + updatesec.totaltrigs.ToString();
                if (!UpdateSentece(ref sentences[24], somestring, 20, 500, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 6 fail text");
                    return false;
                }*/


                somestring = "total meshes: " + " " + updatesec.countmeshtotallod2.ToString();
                if (!UpdateSentece(ref sentences[25], somestring, 20, 520, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 7 fail text");
                    return false;
                }


                somestring = "total meshes culled: " + " " + updatesec.countmeshculledlod2.ToString();
                if (!UpdateSentece(ref sentences[26], somestring, 20, 540, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 8 fail text");
                    return false;
                }

                somestring = "distance culled: " + " " + updatesec.countmeshdistculledlod2.ToString();
                if (!UpdateSentece(ref sentences[27], somestring, 20, 560, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 9 fail text");
                    return false;
                }

                somestring = "dot frustrum culled: " + " " + updatesec.countmeshfrustculledlod2.ToString();
                if (!UpdateSentece(ref sentences[28], somestring, 20, 580, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 10 fail text");
                    return false;
                }

                somestring = "total vertices: " + " " + updatesec.countmeshverttotallod2.ToString();
                if (!UpdateSentece(ref sentences[29], somestring, 20, 600, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 11 fail text");
                    return false;
                }

                somestring = "total vertices culled: " + " " + updatesec.countmeshvertculledlod2.ToString();
                if (!UpdateSentece(ref sentences[30], somestring, 20, 620, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 12 fail text");
                    return false;
                }

                somestring = "total triangles: " + " " + updatesec.countmeshtrigtotallod2.ToString();
                if (!UpdateSentece(ref sentences[31], somestring, 20, 640, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 13 fail text");
                    return false;
                }

                somestring = "total triangles culled: " + " " + updatesec.countmeshtrigculledlod2.ToString();
                if (!UpdateSentece(ref sentences[32], somestring, 20, 660, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 14 fail text");
                    return false;
                }







                //LOD3 LEVEL OF DETAIL 4
                /*somestring = "LOD4 MESHES DATA: " + " " + updatesec.totaltrigs.ToString();
                if (!UpdateSentece(ref sentences[33], somestring, 20, 680, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 6 fail text");
                    return false;
                }*/


                somestring = "total meshes: " + " " + updatesec.countmeshtotallod3.ToString();
                if (!UpdateSentece(ref sentences[34], somestring, 20, 700, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 7 fail text");
                    return false;
                }


                somestring = "total meshes culled: " + " " + updatesec.countmeshculledlod3.ToString();
                if (!UpdateSentece(ref sentences[35], somestring, 20, 720, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 8 fail text");
                    return false;
                }

                somestring = "distance culled: " + " " + updatesec.countmeshdistculledlod3.ToString();
                if (!UpdateSentece(ref sentences[36], somestring, 20, 740, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 9 fail text");
                    return false;
                }

                somestring = "dot frustrum culled: " + " " + updatesec.countmeshfrustculledlod3.ToString();
                if (!UpdateSentece(ref sentences[37], somestring, 20, 760, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 10 fail text");
                    return false;
                }

                somestring = "total vertices: " + " " + updatesec.countmeshverttotallod3.ToString();
                if (!UpdateSentece(ref sentences[38], somestring, 20, 780, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 11 fail text");
                    return false;
                }

                somestring = "total vertices culled: " + " " + updatesec.countmeshvertculledlod3.ToString();
                if (!UpdateSentece(ref sentences[39], somestring, 20, 800, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 12 fail text");
                    return false;
                }

                somestring = "total triangles: " + " " + updatesec.countmeshtrigtotallod3.ToString();
                if (!UpdateSentece(ref sentences[40], somestring, 20, 820, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 13 fail text");
                    return false;
                }

                somestring = "total triangles culled: " + " " + updatesec.countmeshtrigculledlod3.ToString();
                if (!UpdateSentece(ref sentences[41], somestring, 20, 840, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 14 fail text");
                    return false;
                }









                //LOD4 LEVEL OF DETAIL 5
                /*somestring = "LOD4 MESHES DATA: " + " " + updatesec.totaltrigs.ToString();
                if (!UpdateSentece(ref sentences[42], somestring, 20, 860, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 6 fail text");
                    return false;
                }*/


                somestring = "total meshes: " + " " + updatesec.countmeshtotallod4.ToString();
                if (!UpdateSentece(ref sentences[43], somestring, 20, 880, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 7 fail text");
                    return false;
                }


                somestring = "total meshes culled: " + " " + updatesec.countmeshculledlod4.ToString();
                if (!UpdateSentece(ref sentences[44], somestring, 20, 900, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 8 fail text");
                    return false;
                }

                somestring = "distance culled: " + " " + updatesec.countmeshdistculledlod4.ToString();
                if (!UpdateSentece(ref sentences[45], somestring, 20, 920, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 9 fail text");
                    return false;
                }

                somestring = "dot frustrum culled: " + " " + updatesec.countmeshfrustculledlod4.ToString();
                if (!UpdateSentece(ref sentences[46], somestring, 20, 940, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 10 fail text");
                    return false;
                }

                somestring = "total vertices: " + " " + updatesec.countmeshverttotallod4.ToString();
                if (!UpdateSentece(ref sentences[47], somestring, 20, 960, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 11 fail text");
                    return false;
                }

                somestring = "total vertices culled: " + " " + updatesec.countmeshvertculledlod4.ToString();
                if (!UpdateSentece(ref sentences[48], somestring, 20, 980, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 12 fail text");
                    return false;
                }

                somestring = "total triangles: " + " " + updatesec.countmeshtrigtotallod4.ToString();
                if (!UpdateSentece(ref sentences[49], somestring, 20, 1000, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 13 fail text");
                    return false;
                }

                somestring = "total triangles culled: " + " " + updatesec.countmeshtrigculledlod4.ToString();
                if (!UpdateSentece(ref sentences[50], somestring, 20, 1020, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 14 fail text");
                    return false;
                }





















                /*

                
            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[6], 32, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[6], "LOD1 MESHES DATA: ", 20, 140, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[7], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[7], "total meshes: ", 20, 160, 0, 1, 1, deviceContext))
                return false;


            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[8], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[8], "total meshes culled: ", 20, 180, 0, 1, 1, deviceContext))
                return false;


            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[9], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[9], "distance culled: ", 20, 200, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[10], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[10], "dot frustrum culled: ", 20, 220, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[11], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[14], "total vertices: ", 20, 240, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[12], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[12], "total vertices culled: ", 20, 260, 0, 1, 1, deviceContext))
                return false;

            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[13], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[13], "total triangles: ", 20, 280, 0, 1, 1, deviceContext))
                return false;
            // Initialize the Third sentence.
            if (!InitializeSentence(out sentences[14], 64, device))
                return false;

            // Now update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[14], "total triangles culled: ", 20, 300, 0, 1, 1, deviceContext))
                return false;
            */









                //Console.WriteLine("test");

                // Setup the mouseY string.
                /*somestring = "distance culled mesh data count: " + " " + updatesec.totalmeshdistculled.ToString();

                // Update the sentence vertex buffer with the new string information.
                if (!UpdateSentece(ref sentences[3], somestring, 20, 80, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 3 fail text");
                    return false;
                }

                // Setup the mouseY string.
                somestring = "dot frustrum culled mesh data count: " + " " + updatesec.totalmeshfrustculled.ToString();

                // Update the sentence vertex buffer with the new string information.
                if (!UpdateSentece(ref sentences[4], somestring, 20, 100, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 4 fail text");
                    return false;
                }

                // Setup the mouseY string.
                somestring = "LOD1 meshes data count: " + " " + updatesec.totalmeshfrustculled.ToString()+
                " " + updatesec.countmeshculledlod0.ToString() +
                " " + updatesec.countmeshtrigculledlod0.ToString() +
                " " + updatesec.countmeshvertculledlod0.ToString() +
                " " + updatesec.countmeshdistculledlod0.ToString() +
                " " + updatesec.countmeshfrustculledlod0.ToString() +
                " " + updatesec.countmeshtotallod0.ToString() +
                " " + updatesec.countmeshtrigtotallod0.ToString() +
                " " + updatesec.countmeshverttotallod0.ToString();

                // Update the sentence vertex buffer with the new string information.
                if (!UpdateSentece(ref sentences[5], somestring, 20, 120, 1, 1, 1, deviceContext))
                {
                    Console.WriteLine("text 5 fail text");
                    return false;
                }*/


            }

            /*public int countmeshculledlod0 = 0;
            public int countmeshtrigculledlod0 = 0;
            public int countmeshvertculledlod0 = 0;
            public int countmeshdistculledlod0 = 0;
            public int countmeshfrustculledlod0 = 0;
            public int countmeshtotallod0 = 0;
            public int countmeshtrigtotallod0 = 0;
            public int countmeshverttotallod0 = 0;*/



            return true;
        }


        public bool SetNewSenctenceDataVertex(string passedInText, DeviceContext deviceContext)
        {
            int newTextLength = passedInText.Length;
            int sentenceTextLength = sentences[2].sentenceText.Length;
            int resultLength = sentenceTextLength - newTextLength;
            string test = sentences[2].sentenceText.Substring(resultLength);

            // Dont repeat the same character thats already added.
            if (passedInText.Length == 0 || test.Equals(passedInText))
                return false;

            string mouseString;

            // Setup the mouseX string.
            mouseString = sentences[2].sentenceText + passedInText;
            // Update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[2], mouseString, 20, 80, 0, 1, 1, deviceContext))
                return false;

            return true;
        }
        public bool SetNewSenctenceDataTriangle(string passedInText, DeviceContext deviceContext)
        {
            int newTextLength = passedInText.Length;
            int sentenceTextLength = sentences[3].sentenceText.Length;
            int resultLength = sentenceTextLength - newTextLength;
            string test = sentences[3].sentenceText.Substring(resultLength);

            // Dont repeat the same character thats already added.
            if (passedInText.Length == 0 || test.Equals(passedInText))
                return false;

            string mouseString;

            // Setup the mouseX string.
            mouseString = sentences[3].sentenceText + passedInText;
            // Update the sentence vertex buffer with the new string information.
            if (!UpdateSentece(ref sentences[3], mouseString, 20, 100, 0, 1, 1, deviceContext))
                return false;

            return true;
        }





        private bool InitializeSentence(out DSentence sentence, int maxLength, SharpDX.Direct3D11.Device device)
        {
            // Create a new sentence object.
            sentence = new DSentence();

            // Initialize the sentence buffers to null;
            sentence.VertexBuffer = null;
            sentence.IndexBuffer = null;

            // Set the maximum length of the sentence.
            sentence.MaxLength = maxLength;

            // Set the number of vertices in vertex array.
            sentence.VertexCount = 6 * maxLength;
            // Set the number of vertices in the vertex array.
            sentence.IndexCount = sentence.VertexCount;

            // Create the vertex array.
            var vertices = new DTextClass.DVertex[sentence.VertexCount];
            // Create the index array.
            var indices = new int[sentence.IndexCount];

            // Initialize the index array.
            for (var i = 0; i < sentence.IndexCount; i++)
                indices[i] = i;

            // Set up the description of the dynamic vertex buffer.
            var vertexBufferDesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Utilities.SizeOf<DTextClass.DVertex>() * sentence.VertexCount,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            // Create the vertex buffer.
            sentence.VertexBuffer = SharpDX.Direct3D11.Buffer.Create(device, vertices, vertexBufferDesc);

            // Create the index buffer.
            sentence.IndexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, indices);

            vertices = null;
            indices = null;

            return true;
        }
        public bool UpdateSentece(ref DSentence sentence, string text, int positionX, int positionY, float red, float green, float blue, DeviceContext deviceContext)
        {
            // Store the Text to update the given sentence.
            sentence.sentenceText = text;

            // Store the color of the sentence.
            sentence.red = red;
            sentence.green = green;
            sentence.blue = blue;

            // Get the number of the letter in the sentence.
            var numLetters = text.Length;

            // Check for possible buffer overflow.
            if (numLetters > sentence.MaxLength)
                return false;

            // Calculate the X and Y pixel position on screen to start drawing to.
            var drawX = -(ScreenWidth >> 1) + positionX;
            var drawY = (ScreenHeight >> 1) - positionY;

            // Use the font class to build the vertex array from the sentence text and sentence draw location.
            List<DTextClass.DVertex> vertices;
            Font.BuildVertexArray(out vertices, text, drawX, drawY);

            DataStream mappedResource;

            #region Vertex Buffer 
            // Lock the vertex buffer so it can be written to.
            deviceContext.MapSubresource(sentence.VertexBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);

            // Copy the data into the vertex buffer.
            mappedResource.WriteRange<DTextClass.DVertex>(vertices.ToArray());

            // Unlock the vertex buffer.
            deviceContext.UnmapSubresource(sentence.VertexBuffer, 0);
            #endregion

            vertices?.Clear();
            vertices = null;

            return true;
        }
        private void ReleaseSentences(DSentence sentence)
        {
            // Release the sentence vertex buffer.
            sentence.VertexBuffer?.Dispose();
            sentence.VertexBuffer = null;
            // Release the sentence index buffer.
            sentence.IndexBuffer?.Dispose();
            sentence.IndexBuffer = null;
        }
        private bool RenderSentence(DeviceContext deviceContext, DSentence sentence, Matrix worldMatrix, Matrix orthoMatrix)
        {
            // Set vertex buffer stride and offset.
            var stride = Utilities.SizeOf<DTextClass.DVertex>();
            var offset = 0;

            // Set the vertex buffer to active in the input assembler so it can be rendered.
            deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(sentence.VertexBuffer, stride, offset));

            // Set the index buffer to active in the input assembler so it can be rendered.
            deviceContext.InputAssembler.SetIndexBuffer(sentence.IndexBuffer, Format.R32_UInt, 0);

            // Set the type of the primitive that should be rendered from this vertex buffer, in this case triangles.
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;

            // Create a pixel color vector with the input sentence color.
            var pixelColor = new Vector4(sentence.red, sentence.green, sentence.blue, 1);

            // Render the text using the font shader.
            if (!FontShader.Render(deviceContext, sentence.IndexCount, worldMatrix, BaseViewMatrix, orthoMatrix, Font.Texture.TextureResource, pixelColor))
                return false;

            return true;
        }
    }
}