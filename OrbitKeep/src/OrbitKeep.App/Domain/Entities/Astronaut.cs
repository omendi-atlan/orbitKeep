namespace OrbitKeep.Domain.Entities
{
    /// <summary>
    /// Concrete crew member that can perform EVAs.
    /// Inheritance: Entity -> CrewMember -> Astronaut
    /// </summary>
    public class Astronaut : CrewMember
    {
        private bool _isOnEva;
        private string? _buddyId;

        public bool IsOnEva
        {
            get => _isOnEva;
            private set => _isOnEva = value;
        }

        public string? BuddyId
        {
            get => _buddyId;
            private set => _buddyId = value;
        }

        public Astronaut(string id, string name, string rank) : base(id, name, rank)
        {
            IsOnEva = false;
            BuddyId = null;
        }

        public void StartEva(string buddyId)
        {
            if (IsOnEva)
                throw new Exceptions.InvalidEvaOperationException($"{Name} is already on EVA.");
            IsOnEva = true;
            BuddyId = buddyId;
            IsOnDuty = false; // temporarily not available for station duties
        }

        public void EndEva()
        {
            if (!IsOnEva)
                throw new Exceptions.InvalidEvaOperationException($"{Name} is not currently on EVA.");
            IsOnEva = false;
            BuddyId = null;
            IsOnDuty = true;
        }

        public override string GetStatus()
        {
            string baseStatus = base.GetStatus();
            if (IsOnEva)
                return $"{baseStatus} | ON EVA (buddy: {BuddyId})";
            return baseStatus;
        }
    }
}
