using System.Collections;
using UnityEngine;

namespace SolarSystem
{
    public class OrbitMotion : MonoBehaviour
    {
        public Transform OrbitObject;
        public Ellipse OrbitPath;

        [Range(0f, 1f)]
        public float OrbitProgress;

        public float OrbitPeriod = 3.0f;
        public bool OrbitActive = true;

        private void Start()
        {
            if (OrbitObject == null)
            {
                OrbitActive = false;
                return;
            }

            SetOrbitingObjectPosition();
            StartCoroutine(AnimateOrbit());
        }

        private void SetOrbitingObjectPosition()
        {
            var orbitPos = OrbitPath.Evaluate(OrbitProgress);
            OrbitObject.localPosition = new Vector3(orbitPos.x, 0, orbitPos.y);
        }

        private IEnumerator AnimateOrbit()
        {
            if (OrbitPeriod < 0.1f)
            {
                OrbitPeriod = 0.1f;
            }

            float orbitSpeed = 1.0f / OrbitPeriod;

            while (OrbitActive)
            {
                OrbitProgress += Time.deltaTime * orbitSpeed;
                OrbitProgress %= 1.0f;
                SetOrbitingObjectPosition();
                yield return null;
            }
        }
    }
}