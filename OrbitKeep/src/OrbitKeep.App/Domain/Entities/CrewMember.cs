namespace OrbitKeep.Domain.Entities
{
    /// <summary>
    /// Intermediate base for crew. Demonstrates multi-level inheritance:
    /// Entity -> CrewMember -> Astronaut
    /// </summary>
    public abstract class CrewMember : Entity
    {
        private string _rank;
        private bool _isOnDuty;

        public string Rank
        {
            get => _rank;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Rank cannot be empty.");
                _rank = value;
            }
        }

        public bool IsOnDuty
        {
            get => _isOnDuty;
            set => _isOnDuty = value;
        }

        protected CrewMember(string id, string name, string rank) : base(id, name)
        {
            Rank = rank;
            IsOnDuty = true;
        }

        public override string GetStatus()
        {
            return $"{Name} ({Rank}) – {(IsOnDuty ? "On Duty" : "Off Duty")}";
        }
    }
}
