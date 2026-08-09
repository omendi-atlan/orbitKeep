using OrbitKeep.Domain.Interfaces;

namespace OrbitKeep.Domain.Entities
{
    /// <summary>
    /// Abstract base class for all station entities.
    /// Demonstrates Abstraction + Encapsulation.
    /// </summary>
    public abstract class Entity : IEntity
    {
        private string _id;
        private string _name;

        public string Id
        {
            get => _id;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Entity Id cannot be empty.");
                _id = value;
            }
        }

        public string Name
        {
            get => _name;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Entity Name cannot be empty.");
                _name = value;
            }
        }

        protected Entity(string id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Polymorphic status report – each derived type overrides this.
        /// </summary>
        public abstract string GetStatus();
    }
}
