using Jitter.Dynamics;
namespace sccsr15forms
{
    public interface IComponent
    {
        RigidBody rigidbody { get; set; }
        SoftBody softbody { get; set; }
    }
}
