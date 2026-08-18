using System;

namespace ProjectSpaceStation
{
    public class Astronaut : CrewMember
    {
        public bool IsOnEva { get; private set; }
        public string BuddyId { get; private set; }

        public Astronaut(string id, string name, string rank) : base(id, name, rank)
        {
            IsOnEva = false;
            BuddyId = null;
        }

        public bool StartEva(string buddyId)
        {
            if (IsOnEva)
            {
                throw new SpaceStationException($"{Name} is already outside!"); // throw custom error
            }

            if (string.IsNullOrWhiteSpace(buddyId) || buddyId.Equals(Id, StringComparison.OrdinalIgnoreCase))
            {
                throw new SpaceStationException($"Invalid buddy assignment for {Name}."); // throw custom error
            }

            IsOnEva = true;
            BuddyId = buddyId;
            IsOnDuty = false;
            return true;
        }

        public bool EndEva()
        {
            if (!IsOnEva)
            {
                Console.WriteLine($"  [EVA WARNING] {Name} is not currently on EVA.");
                return false;
            }

            IsOnEva = false;
            BuddyId = null;
            IsOnDuty = true;
            return true;
        }

        public override string GetStatus()
        {
            string baseStatus = base.GetStatus();
            return IsOnEva ? $"{baseStatus} | ON EVA (Buddy: {BuddyId})" : baseStatus;
        }
    }
}
