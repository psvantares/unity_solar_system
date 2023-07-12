using UnityEngine;

namespace SolarSystem
{
    public sealed class TimeScale : MonoBehaviour
    {
        public float TimeScaleValue { get; set; } = 1.0f;

        private void Update()
        {
            Time.timeScale = Mathf.Clamp(TimeScaleValue, 0.0f, 100.0f);
        }
    }
}