namespace OrbitKeep.Domain.Interfaces
{
    /// <summary>
    /// Contract for modules that consume station power.
    /// Used by the power-shedding system to decide which modules to shut down.
    /// </summary>
    public interface IPowerConsumer
    {
        bool IsEssential { get; }
        double PowerDrawKw { get; }
        bool IsOnline { get; }
        void ShutDown(string reason);
        void PowerOn();
    }
}
