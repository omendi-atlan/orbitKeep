using System;

namespace ProjectSpaceStation
{
    // <summary>
    // Central life-support aggregate. Holds station-wide O2 reserve.
    // Always treated as essential – never shut down by power-shedding.
    public class LifeSupportSystem : Entity
    {
        private double _stationOxygenReserve;
        private double _powerDrawKw;

        public double StationOxygenReserve
        {
            get => _stationOxygenReserve;
            set => _stationOxygenReserve = Math.Max(0.0, Math.Min(100.0, value));
        }

        public double PowerDrawKw
        {
            get => _powerDrawKw;
            set => _powerDrawKw = Math.Max(0.0, value);
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
