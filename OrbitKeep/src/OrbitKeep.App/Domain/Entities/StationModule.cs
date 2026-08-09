using OrbitKeep.Domain.Interfaces;

namespace OrbitKeep.Domain.Entities
{
    /// <summary>
    /// A station module that consumes power and holds local O2.
    /// Implements IPowerConsumer for the auto power-shedding feature.
    /// </summary>
    public class StationModule : Entity, IPowerConsumer
    {
        private double _oxygenPercent;
        private double _powerDrawKw;
        private bool _isEssential;
        private bool _isOnline;

        public double OxygenPercent
        {
            get => _oxygenPercent;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(nameof(value), "Oxygen % must be 0-100.");
                _oxygenPercent = value;
            }
        }

        public double PowerDrawKw
        {
            get => _powerDrawKw;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Power draw cannot be negative.");
                _powerDrawKw = value;
            }
        }

        public bool IsEssential
        {
            get => _isEssential;
            private set => _isEssential = value;
        }

        public bool IsOnline
        {
            get => _isOnline;
            private set => _isOnline = value;
        }

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

        /// <summary>
        /// Drain a small amount of local oxygen (called by background monitor).
        /// </summary>
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
