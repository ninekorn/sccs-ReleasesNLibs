using SharpDX;
using Jitter.Dynamics;
namespace _sc_core_systems.SC_Graphics
{
    public class _sc_spectrum_instances : ITransform, IComponent
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

        public _sc_spectrum_instances()
        {
            transform = this;
            component = this;
        }
    }
}