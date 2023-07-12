using UnityEngine;

namespace SolarSystem
{
    public class OrbitCamera : MonoBehaviour
    {
        private RaycastHit hit;
        private Vector3 position;
        private Ray ray;
        private float targetDistance = 10f;
        private bool spinning;
        private float spinSpeed;
        private float x;
        private float xVelocity;
        private float y;
        private float yVelocity;
        private float zoomVelocity;

        [HideInInspector]
        public int AutoRotateReverseValue = 1;

        [HideInInspector]
        public int InvertXValue = 1;

        [HideInInspector]
        public int InvertYValue = 1;

        [HideInInspector]
        public int InvertZoomValue = 1;

        public bool AutoRotateOn;
        public bool AutoRotateReverse;

        public float AutoRotateSpeed = 0.1f;
        public bool CameraCollision;
        public bool ClickToRotate = true;
        public float CollisionRadius = 0.25f;
        public float DampeningX = 0.9f;
        public float DampeningY = 0.9f;
        public float Distance = 10f;
        public float InitialAngleX;
        public float InitialAngleY;
        public bool InvertAxisX;
        public bool InvertAxisY;
        public bool InvertAxisZoom;

        public string KbPanAxisX = "Horizontal";
        public string KbPanAxisY = "Vertical";
        public bool KbUseZoomAxis;
        public string KbZoomAxisName = string.Empty;
        public bool KeyboardControl;
        public bool LeftClickToRotate = true;
        public bool LimitX;
        public bool LimitY = true;
        public float MaxDistance = 25f;
        public float MaxSpinSpeed = 3f;
        public float MinDistance = 5f;
        public string MouseAxisX = "Mouse X";
        public string MouseAxisY = "Mouse Y";
        public string MouseAxisZoom = "Mouse ScrollWheel";
        public bool MouseControl = true;

        public bool RightClickToRotate;
        public float SmoothingZoom = 0.1f;
        public string SpinAxis = string.Empty;
        public bool SpinEnabled;
        public KeyCode SpinKey;
        public bool SpinUseAxis;
        public Transform Target;

        public float XLimitOffset;
        public float XMaxLimit = 60f;
        public float XMinLimit = -60f;
        public float XSpeed = 1f;

        public float YLimitOffset;
        public float YMaxLimit = 60f;
        public float YMinLimit = -60f;
        public float YSpeed = 1f;

        public KeyCode ZoomInKey = KeyCode.R;
        public KeyCode ZoomOutKey = KeyCode.F;
        public float ZoomSpeed = 5f;

        private void Start()
        {
            targetDistance = Distance;

            if (InvertAxisX)
            {
                InvertXValue = -1;
            }
            else
            {
                InvertXValue = 1;
            }

            if (InvertAxisY)
            {
                InvertYValue = -1;
            }
            else
            {
                InvertYValue = 1;
            }

            if (InvertAxisZoom)
            {
                InvertZoomValue = -1;
            }
            else
            {
                InvertZoomValue = 1;
            }

            if (AutoRotateOn)
            {
                AutoRotateReverseValue = -1;
            }
            else
            {
                AutoRotateReverseValue = 1;
            }

            x = InitialAngleX;
            y = InitialAngleY;
            transform.Rotate(new Vector3(0f, InitialAngleX, 0f), Space.World);
            Transform tr;
            (tr = transform).Rotate(new Vector3(InitialAngleY, 0f, 0f), Space.Self);
            position = tr.rotation * new Vector3(0f, 0f, -Distance) + Target.position;
        }

        private void Update()
        {
            if (Target != null)
            {
                if (AutoRotateOn)
                {
                    xVelocity += AutoRotateSpeed * AutoRotateReverseValue * Time.deltaTime;
                }

                if (MouseControl)
                {
                    if (!ClickToRotate || (LeftClickToRotate && Input.GetMouseButton(1)) || (RightClickToRotate && Input.GetMouseButton(1)))
                    {
                        xVelocity += Input.GetAxis(MouseAxisX) * XSpeed * InvertXValue;
                        yVelocity -= Input.GetAxis(MouseAxisY) * YSpeed * InvertYValue;
                        spinning = false;
                    }
                }

                zoomVelocity -= Input.GetAxis(MouseAxisZoom) * ZoomSpeed * InvertZoomValue;
            }

            if (KeyboardControl)
            {
                if (Input.GetAxis(KbPanAxisX) != 0f || Input.GetAxis(KbPanAxisY) != 0f)
                {
                    xVelocity -= Input.GetAxisRaw(KbPanAxisX) * (XSpeed / 2f) * InvertXValue;
                    yVelocity += Input.GetAxisRaw(KbPanAxisY) * (YSpeed / 2f) * InvertYValue;
                    spinning = false;
                }

                if (KbUseZoomAxis)
                {
                    zoomVelocity += Input.GetAxis(KbZoomAxisName) * (ZoomSpeed / 10f) * InvertXValue;
                }

                if (Input.GetKey(ZoomInKey))
                {
                    zoomVelocity -= ZoomSpeed / 10f * InvertZoomValue;
                }

                if (Input.GetKey(ZoomOutKey))
                {
                    zoomVelocity += ZoomSpeed / 10f * InvertZoomValue;
                }
            }

            if (SpinEnabled && ((MouseControl && ClickToRotate) || KeyboardControl))
            {
                if ((SpinUseAxis && Input.GetAxis(SpinAxis) != 0f) || (!SpinUseAxis && Input.GetKey(SpinKey)))
                {
                    spinning = true;
                    spinSpeed = Mathf.Min(xVelocity, MaxSpinSpeed);
                }

                if (spinning)
                {
                    xVelocity = spinSpeed;
                }
            }

            if (LimitX)
            {
                if (x + xVelocity < XMinLimit + XLimitOffset)
                {
                    xVelocity = XMinLimit + XLimitOffset - x;
                }
                else if (x + xVelocity > XMaxLimit + XLimitOffset)
                {
                    xVelocity = XMaxLimit + XLimitOffset - x;
                }

                x += xVelocity;
                transform.Rotate(new Vector3(0f, xVelocity, 0f), Space.World);
            }
            else
            {
                transform.Rotate(new Vector3(0f, xVelocity, 0f), Space.World);
            }

            if (LimitY)
            {
                if (y + yVelocity < YMinLimit + YLimitOffset)
                {
                    yVelocity = YMinLimit + YLimitOffset - y;
                }
                else if (y + yVelocity > YMaxLimit + YLimitOffset)
                {
                    yVelocity = YMaxLimit + YLimitOffset - y;
                }

                y += yVelocity;
                transform.Rotate(new Vector3(yVelocity, 0f, 0f), Space.Self);
            }
            else
            {
                transform.Rotate(new Vector3(yVelocity, 0f, 0f), Space.Self);
            }

            if (targetDistance + zoomVelocity < MinDistance)
            {
                zoomVelocity = MinDistance - targetDistance;
            }
            else if (targetDistance + zoomVelocity > MaxDistance)
            {
                zoomVelocity = MaxDistance - targetDistance;
            }

            targetDistance += zoomVelocity;
            Distance = Mathf.Lerp(Distance, targetDistance, SmoothingZoom);
            if (CameraCollision)
            {
                var targetPosition = Target.position;
                var vector = transform.position - targetPosition;
                ray = new Ray(targetPosition, vector.normalized);

                if (Physics.SphereCast(ray.origin, CollisionRadius, ray.direction, out hit, Distance))
                {
                    Distance = hit.distance;
                }
            }

            var tr = transform;
            tr.position = tr.rotation * new Vector3(0f, 0f, -Distance) + Target.position;

            if (!SpinEnabled || !spinning)
            {
                xVelocity *= DampeningX;
            }

            yVelocity *= DampeningY;
            zoomVelocity = 0f;
        }
    }
}