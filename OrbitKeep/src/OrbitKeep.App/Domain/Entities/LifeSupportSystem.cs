namespace OrbitKeep.Domain.Entities
{
    /// <summary>
    /// Central life-support aggregate. Holds station-wide O2 reserve.
    /// Always treated as essential – never shut down by power-shedding.
    /// </summary>
    public class LifeSupportSystem : Entity
    {
        private double _stationOxygenReserve;
        private double _powerDrawKw;

        public double StationOxygenReserve
        {
            get => _stationOxygenReserve;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(nameof(value), "Station O2 reserve must be 0-100.");
                _stationOxygenReserve = value;
            }
        }

        public double PowerDrawKw
        {
            get => _powerDrawKw;
            private set => _powerDrawKw = value;
        }

        public bool IsOnline { get; private set; } = true;

        public LifeSupportSystem(string id, double initialOxygen, double powerDrawKw)
            : base(id, "Life Support Core")
        {
            StationOxygenReserve = initialOxygen;
            PowerDrawKw = powerDrawKw;
        }

        public void DrainOxygen(double amount)
        {
            StationOxygenReserve = Math.Max(0, StationOxygenReserve - amount);
        }

        public void AddOxygen(double amount)
        {
            StationOxygenReserve = Math.Min(100, StationOxygenReserve + amount);
        }

        public override string GetStatus()
        {
            return $"Life Support | Station O2 Reserve: {StationOxygenReserve:F1}% | Power: {PowerDrawKw:F1} kW | {(IsOnline ? "ONLINE" : "OFFLINE")}";
        }
    }
}
