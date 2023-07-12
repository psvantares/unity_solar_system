using System;

namespace SolarSystem
{
    public enum PlanetType
    {
        Default = 0,
        Mercury = 1,
        Venus = 2,
        Earth = 3,
        Mars = 4,
        Jupiter = 5,
        Saturn = 6,
        Uranus = 7,
        Neptune = 8,
        Pluto = 9
    }

    [Serializable]
    public class PlanetModel
    {
        public int Planet;
        public string Name;
        public float OrbitPeriod;
        public float AxisPeriod;
        public float AxisIncline;
        public float DistanceToSun;
    }
}