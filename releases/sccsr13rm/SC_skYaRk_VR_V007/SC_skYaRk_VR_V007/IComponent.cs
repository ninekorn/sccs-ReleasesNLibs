using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Jitter.Collision;
using Jitter;
using Jitter.Dynamics;
using Jitter.DataStructures;
using Jitter.Collision.Shapes;

namespace SC_skYaRk_VR_V007
{
    public interface IComponent
    {

        //IComponent ITransform { get; set; }
        RigidBody rigidbody { get; set; }
        SoftBody softbody { get; set; }
        //object rigidbody { get; set; }
    }
}
