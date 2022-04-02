using Jitter.Dynamics;
namespace SCCoreSystems
{
    public interface IComponent
    {
        RigidBody rigidbody { get; set; }
        SoftBody softbody { get; set; }
    }
}
