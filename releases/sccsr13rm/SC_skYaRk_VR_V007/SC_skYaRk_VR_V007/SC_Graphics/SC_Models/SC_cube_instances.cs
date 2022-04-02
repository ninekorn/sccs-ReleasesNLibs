using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

//using SC_skYaRk_VR_V007.SC_Graphics.SC_Textures.SC_VR_Touch_Textures;

using System.Linq;
using System;



using Jitter.Collision;
using Jitter;
using Jitter.Dynamics;
using Jitter.DataStructures;
using Jitter.Collision.Shapes;



using System.Runtime.InteropServices;


using SC_skYaRk_VR_V007.SC_Graphics;

namespace SC_skYaRk_VR_V007.SC_Graphics
{
    public class SC_cube_instances : ITransform, IComponent
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

        public SC_cube_instances()
        {
            transform = this;
            component = this;
        }
    }
}