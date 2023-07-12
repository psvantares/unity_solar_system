using System;
using UnityEngine;

namespace SolarSystem
{
    [Serializable]
    public class Ellipse
    {
        public float XAxis;
        public float YAxis;

        public Ellipse(float xAxis, float yAxis)
        {
            XAxis = xAxis;
            YAxis = yAxis;
        }

        public Vector2 Evaluate(float t)
        {
            var angle = Mathf.Deg2Rad * 360.0f * t;
            var x = Mathf.Sin(angle) * XAxis;
            var y = Mathf.Cos(angle) * YAxis;
            return new Vector2(x, y);
        }
    }
}