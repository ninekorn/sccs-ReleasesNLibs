using Jitter.Dynamics;
namespace _sc_core_systems
{
    public interface IComponent
    {
        RigidBody rigidbody { get; set; }
        SoftBody softbody { get; set; }
    }
}
