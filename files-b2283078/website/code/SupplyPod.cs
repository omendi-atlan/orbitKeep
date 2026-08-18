using System;

namespace ProjectSpaceStation
{
    // <summary>
    // Incoming supply pod. Can be docked to restock oxygen / power buffers.
    public class SupplyPod : Entity
    {
        private double _oxygenCargo;
        private int _estimatedDockSeconds;

        public double OxygenCargo
        {
            get => _oxygenCargo;
            private set => _oxygenCargo = Math.Max(0, value);
        }

        public int EstimatedDockSeconds
        {
            get => _estimatedDockSeconds;
            private set => _estimatedDockSeconds = Math.Max(1, value);
        }

        public bool IsDocked { get; private set; }

        public SupplyPod(string id, string name, double oxygenCargo, int estimatedDockSeconds)
            : base(id, name)
        {
            OxygenCargo = oxygenCargo;
            EstimatedDockSeconds = estimatedDockSeconds;
            IsDocked = false;
        }

        public void MarkDocked()
        {
            IsDocked = true;
        }

        public override string GetStatus()
        {
            return $"Pod {Name} [{Id}] | O2 cargo: {OxygenCargo:F1}% | ETA: {EstimatedDockSeconds}s | {(IsDocked ? "DOCKED" : "IN TRANSIT")}";
        }
    }
}
