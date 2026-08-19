namespace ProjectSpaceStation
{
    // <summary>
    // Contract for any entity that can be identified and report its status.
    // Demonstrates Abstraction via interface.
    public interface IEntity
    {
        string Id { get; }
        string Name { get; }
        string GetStatus();
    }
}