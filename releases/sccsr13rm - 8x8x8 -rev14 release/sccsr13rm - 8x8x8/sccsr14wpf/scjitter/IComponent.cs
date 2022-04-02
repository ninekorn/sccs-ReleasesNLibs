using Jitter.Dynamics;
namespace sccs
{
    public interface IComponent
    {
        RigidBody rigidbody { get; set; }
        SoftBody softbody { get; set; }
    }
}
