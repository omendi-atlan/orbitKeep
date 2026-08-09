namespace OrbitKeep.Domain.Interfaces
{
    /// <summary>
    /// Contract for any entity that can be identified and report its status.
    /// Demonstrates Abstraction via interface.
    /// </summary>
    public interface IEntity
    {
        string Id { get; }
        string Name { get; }
        string GetStatus();
    }
}
