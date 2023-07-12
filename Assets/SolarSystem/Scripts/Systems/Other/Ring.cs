using UnityEngine;

namespace SolarSystem
{
    public class Ring : MonoBehaviour
    {
        [Range(3, 360)]
        public int Segments = 36;

        public float InnerRadius = 0.7f;
        public float Thickness = 0.5f;
        public Material RingMat;

        private GameObject ring;
        private Mesh ringMesh;
        private MeshFilter ringFilter;
        private MeshRenderer meshRenderer;

        private void Start()
        {
            ring = new GameObject(name + " Ring");
            ring.transform.SetParent(transform, false);
            ring.transform.localScale = Vector3.one;
            ring.transform.localRotation = Quaternion.identity;
            ringFilter = ring.AddComponent<MeshFilter>();
            ringMesh = ringFilter.mesh;
            meshRenderer = ring.AddComponent<MeshRenderer>();
            meshRenderer.material = RingMat;

            var vertices = new Vector3[(Segments + 1) * 2 * 2];
            var triangles = new int[Segments * 6 * 2];
            var uv = new Vector2[(Segments + 1) * 2 * 2];
            var halfway = (Segments + 1) * 2;

            for (var i = 0; i < Segments + 1; i++)
            {
                var progress = i / (float)Segments;
                var angle = Mathf.Deg2Rad * progress * 360.0f;
                var x = Mathf.Sin(angle);
                var z = Mathf.Cos(angle);

                vertices[i * 2] = vertices[i * 2 + halfway] = new Vector3(x, 0.0f, z) * (InnerRadius + Thickness);
                vertices[i * 2 + 1] = vertices[i * 2 + 1 + halfway] = new Vector3(x, 0.0f, z) * InnerRadius;
                uv[i * 2] = uv[i * 2 + halfway] = new Vector2(progress, 0.0f);
                uv[i * 2 + 1] = uv[i * 2 + 1 + halfway] = new Vector2(progress, 1.0f);

                if (i != Segments)
                {
                    triangles[i * 12] = i * 2;
                    triangles[i * 12 + 1] = triangles[i * 12 + 4] = (i + 1) * 2;
                    triangles[i * 12 + 2] = triangles[i * 12 + 3] = i * 2 + 1;
                    triangles[i * 12 + 5] = (i + 1) * 2 + 1;

                    triangles[i * 12 + 6] = i * 2 + halfway;
                    triangles[i * 12 + 7] = triangles[i * 12 + 10] = i * 2 + 1 + halfway;
                    triangles[i * 12 + 8] = triangles[i * 12 + 9] = (i + 1) * 2 + halfway;
                    triangles[i * 12 + 11] = (i + 1) * 2 + 1 + halfway;
                }
            }

            ringMesh.vertices = vertices;
            ringMesh.triangles = triangles;
            ringMesh.uv = uv;
            ringMesh.RecalculateNormals();
        }
    }
}