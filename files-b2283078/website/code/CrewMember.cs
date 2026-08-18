using System;

namespace ProjectSpaceStation
{
    // <summary>
    // Intermediate base for crew. Demonstrates multi-level inheritance:
    public abstract class CrewMember : Entity
    {
        private string _rank = "Unassigned";

        public string Rank
        {
            get => _rank;
            protected set
            {
                _rank = string.IsNullOrWhiteSpace(value) ? "Unassigned" : value;
            }
        }

        public bool IsOnDuty { get; set; }

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
