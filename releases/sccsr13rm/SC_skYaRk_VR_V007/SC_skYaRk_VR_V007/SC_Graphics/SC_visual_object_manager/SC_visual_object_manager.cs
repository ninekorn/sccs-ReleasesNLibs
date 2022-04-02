using System;
using System.Collections.Generic;
using System.Text;


using SC_skYaRk_VR_V007.SC_Graphics.SC_Models.human_rig;


using System.Collections;
using SharpDX;

using Jitter;
using Jitter.Dynamics;
using Jitter.Collision;
using Jitter.LinearMath;
using Jitter.Collision.Shapes;





namespace SC_skYaRk_VR_V007.SC_Graphics
{
    public class SC_visual_object_manager
    {
       
        public SC_human_RIG _humRig;

        public SC_visual_object_manager()
        {

        }

        public void _create_human_rig(SC_Console_DIRECTX D3D, IntPtr HWND, World World, Matrix WorldMatrix, float size_screen, float r, float g, float b, float a, int instX, int instY, int instZ, float offsetPosX, float offsetPosY, float offsetPosZ)
        {
            _humRig = new SC_human_RIG( D3D,  HWND,  World,  WorldMatrix,  size_screen,  r,  g,  b,  a,  instX,  instY,  instZ,  offsetPosX,  offsetPosY,  offsetPosZ);
        }
    }
}
