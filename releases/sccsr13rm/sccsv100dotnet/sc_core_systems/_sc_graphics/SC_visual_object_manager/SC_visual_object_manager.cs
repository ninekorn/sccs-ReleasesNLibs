using System;
using System.Collections.Generic;
using System.Text;


using _sc_core_systems.SC_Graphics.SC_Models.human_rig;


using System.Collections;
using SharpDX;

using Jitter;
using Jitter.Dynamics;
using Jitter.Collision;
using Jitter.LinearMath;
using Jitter.Collision.Shapes;





namespace _sc_core_systems.SC_Graphics
{
    public class SC_visual_object_manager
    {
       
        public SC_human_RIG _humRig;

        int _instX;
        int _instY;
        int _instZ;


        public SC_visual_object_manager() //int instX, int instY, int instZ
        {
            //this._instX = instX;
            //this._instY = instY;
            //this._instZ = instZ;

            //_humRig = new SC_human_RIG[_instX* _instY* _instZ];
        }

        //int 
        public void _create_human_rig(SC_console_directx D3D, IntPtr HWND, World World, Matrix WorldMatrix, float size_screen, float r, float g, float b, float a, float offsetPosX, float offsetPosY, float offsetPosZ,float mass)
        {
            _humRig = new SC_human_RIG( D3D,  HWND,  World,  WorldMatrix,  size_screen,  r,  g,  b,  a, offsetPosX,  offsetPosY,  offsetPosZ,mass);
            // _humRig.SC_human_RIG_create(D3D, HWND, World, WorldMatrix, size_screen, r, g, b, a, instX, instY, instZ, offsetPosX, offsetPosY, offsetPosZ);
        }
    }
}
