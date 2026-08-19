using System;

namespace ProjectSpaceStation
{
    // <summary>
    // A station module that consumes power and holds local O2.
    // Implements IPowerConsumer for the auto power-shedding feature.
    public class StationModule : Entity, IPowerConsumer
    {
        private double _oxygenPercent;
        private double _powerDrawKw;

        public double OxygenPercent
        {
            get => _oxygenPercent;
            set
            {
                _oxygenPercent = Math.Max(0.0, Math.Min(100.0, value));
            }
        }

        public double PowerDrawKw
        {
            get => _powerDrawKw;
            private set
            {
                _powerDrawKw = Math.Max(0.0, value);
            }
        }

        public bool IsEssential { get; private set; }

        public bool IsOnline { get; private set; }

        public StationModule(string id, string name, double oxygenPercent, double powerDrawKw, bool isEssential)
            : base(id, name)
        {
            OxygenPercent = oxygenPercent;
            PowerDrawKw = powerDrawKw;
            IsEssential = isEssential;
            IsOnline = true;
        }

        public void ShutDown(string reason)
        {
            if (!IsOnline) return;
            IsOnline = false;
            Console.WriteLine($"  [POWER] Module '{Name}' shut down: {reason}");
        }

        public void PowerOn()
        {
            if (IsOnline) return;
            IsOnline = true;
            Console.WriteLine($"  [POWER] Module '{Name}' powered back online.");
        }

        // <summary>
        // Drain a small amount of local oxygen (called by background monitor).
        public void DrainOxygen(double amount)
        {
            if (!IsOnline) return;
            OxygenPercent = Math.Max(0, OxygenPercent - amount);
        }

        public override string GetStatus()
        {
            string power = IsOnline ? $"Online ({PowerDrawKw:F1} kW)" : "OFFLINE";
            string ess = IsEssential ? "ESSENTIAL" : "non-essential";
            return $"{Name} [{Id}] | O2: {OxygenPercent:F1}% | {power} | {ess}";
        }
    }
}
