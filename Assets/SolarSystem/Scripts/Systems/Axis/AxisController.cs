using System.Collections;
using UnityEngine;

namespace SolarSystem
{
    [RequireComponent(typeof(LineRenderer))]
    public class AxisController : MonoBehaviour
    {
        [SerializeField]
        private Axis axis;

        [SerializeField]
        private float axisProgress;

        [SerializeField]
        private float days;

        [SerializeField]
        private float periodPlanet = 365.2564f;

        private LineRenderer lineRenderer;

        private void Start()
        {
            SetIncline();
            SetAxis();
            StartCoroutine(RotateAsync());
        }

        private void OnValidate()
        {
            SetIncline();
            SetAxis();
        }

        private void SetIncline()
        {
            transform.localRotation = Quaternion.AngleAxis(axis.Angle, Vector3.right);
        }

        private void SetAxis()
        {
            var points = new Vector3[2];
            var position = axis.Evaluate();

            points[0] = axis.PlaneType switch
            {
                PlaneType.XY => new Vector3(position.x, position.y, 0f),
                PlaneType.XZ => new Vector3(position.x, 0f, position.y),
                PlaneType.YZ => new Vector3(0f, position.x, position.y),
                _ => points[0]
            };

            points[1] = -points[0];

            if (lineRenderer == null)
            {
                lineRenderer = GetComponent<LineRenderer>();
            }

            lineRenderer.startWidth = 0.01f;
            lineRenderer.endWidth = 0.01f;
            lineRenderer.useWorldSpace = false;
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(points);
        }

        private IEnumerator RotateAsync()
        {
            var orbitSpeed = 1.0f / periodPlanet;

            while (true)
            {
                axisProgress += Time.deltaTime * orbitSpeed;
                axisProgress %= 1.0f;
                days = axisProgress * periodPlanet;
                transform.Rotate(Vector3.down * axisProgress);
                yield return new WaitForSeconds(orbitSpeed);
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}