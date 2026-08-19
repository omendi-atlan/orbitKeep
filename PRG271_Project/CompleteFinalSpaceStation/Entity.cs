using System;

namespace ProjectSpaceStation
{
    public abstract class Entity : IEntity
    {
        private string _id = "UNKNOWN_ID";
        private string _name = "UNKNOWN_NAME";

        public string Id
        {
            get => _id; // => Same as using {}
            protected set => _id = string.IsNullOrWhiteSpace(value) ? "UNKNOWN_ID" : value;
        }

        public string Name
        {
            get => _name;
            protected set => _name = string.IsNullOrWhiteSpace(value) ? "UNKNOWN_NAME" : value;
        }

        protected Entity(string id, string name)
        {
            Id = id;
            Name = name;
        }

        // <summary>
        // Polymorphic status report – each derived type overrides this.
        public abstract string GetStatus();
    }
}
