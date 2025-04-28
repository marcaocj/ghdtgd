using UnityEngine;

namespace Game.CameraControls
{
    /// <summary>
    /// Camera orbit controller: right-click to rotate around target, scroll to zoom.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public float distance = 5f;
        public float minDistance = 2f;
        public float maxDistance = 10f;
        public float zoomSpeed = 2f;
        public float rotateSpeed = 5f;
        public float minYAngle = -20f;
        public float maxYAngle = 80f;

        private float currentX = 0f;
        private float currentY = 20f;

        void Start()
        {
            if (target == null)
            {
                var player = GameObject.FindGameObjectWithTag("Player");
                if (player) target = player.transform;
            }

            Vector3 angles = transform.eulerAngles;
            currentX = angles.y;
            currentY = angles.x;
        }

        void Update()
        {
            if (Input.GetMouseButton(1))
            {
                currentX += Input.GetAxis("Mouse X") * rotateSpeed;
                currentY -= Input.GetAxis("Mouse Y") * rotateSpeed;
                currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            distance -= scroll * zoomSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
        }

        void LateUpdate()
        {
            if (target == null) return;
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            Vector3 dir = new Vector3(0, 0, -distance);
            transform.position = target.position + rotation * dir;
            transform.LookAt(target.position);
        }
    }
}
