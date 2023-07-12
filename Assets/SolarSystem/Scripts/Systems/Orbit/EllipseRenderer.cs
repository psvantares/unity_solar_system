using UnityEngine;

namespace SolarSystem
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(LineRenderer))]
    public class EllipseRenderer : MonoBehaviour
    {
        [Range(3, 360)]
        public int Segments = 360;

        public bool IsPlaneY;
        public Ellipse Ellipse;
        
        private LineRenderer lineRenderer;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startWidth = 0.01f;
            lineRenderer.endWidth = 0.01f;

            CalculateEllipse();
        }

        private void OnValidate()
        {
            if (lineRenderer != null)
            {
                CalculateEllipse();
            }
        }

        private void CalculateEllipse()
        {
            var points = new Vector3[Segments + 1];

            for (var i = 0; i < Segments; i++)
            {
                var position2D = Ellipse.Evaluate(i / (float)Segments);

                if (IsPlaneY)
                {
                    points[i] = new Vector3(position2D.x, position2D.y, 0f);
                }
                else
                {
                    points[i] = new Vector3(position2D.x, 0f, position2D.y);
                }
            }

            points[Segments] = points[0];

            lineRenderer.positionCount = Segments + 1;
            lineRenderer.SetPositions(points);
        }
    }
}