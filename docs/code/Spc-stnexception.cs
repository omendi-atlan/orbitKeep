using System;

namespace ProjectSpaceStation
{
    // Custom domain exception
    public class SpaceStationException : Exception
    {
        public SpaceStationException(string message) : base(message) { }
    }
}