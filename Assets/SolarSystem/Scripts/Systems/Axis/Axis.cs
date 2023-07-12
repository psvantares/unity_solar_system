using System;
using UnityEngine;

namespace SolarSystem
{
    [Serializable]
    public class Axis
    {
        [Range(0.0f, 90.0f)]
        public float Angle;

        [Range(0.0f, 5.0f)]
        public float Diameter;

        public PlaneType PlaneType;
        public bool IsFreeAngle;

        public Axis(float angle, float diameter, PlaneType planeType)
        {
            Angle = angle;
            Diameter = diameter;
            PlaneType = planeType;
        }

        public Vector2 Evaluate()
        {
            float x;
            float y;

            if (IsFreeAngle)
            {
                x = Mathf.Sin(Mathf.Deg2Rad * (90 - Angle)) * Diameter;
                y = Mathf.Cos(Mathf.Deg2Rad * (90 - Angle)) * Diameter;
            }
            else
            {
                x = Mathf.Sin(Mathf.Deg2Rad * 90) * Diameter;
                y = Mathf.Cos(Mathf.Deg2Rad * 90) * Diameter;
            }

            return new Vector2(x, y);
        }
    }
}