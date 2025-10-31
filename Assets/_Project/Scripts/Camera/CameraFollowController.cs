using UnityEngine;

namespace Project.CameraSystem
{
    public class CameraFollowController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -7f);
        [SerializeField, Range(1f, 20f)] private float followSpeed = 10f;
        [SerializeField, Range(1f, 20f)] private float rotationSpeed = 10f;
        [SerializeField] private bool lookAtTarget = true;

        private Vector3 _previousTargetPos;

        private void Start()
        {
            if (target != null)
                _previousTargetPos = target.position;
        }

        private void LateUpdate()
        {
            if (target == null) return;

            // Інтерполяція між попередньою позицією і новою фізичною позицією
            Vector3 targetPos = target.position;
            Vector3 interpolatedPos = Vector3.Lerp(_previousTargetPos, targetPos, followSpeed * Time.deltaTime);
            transform.position = interpolatedPos + offset;

            if (lookAtTarget)
            {
                Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
            }

            _previousTargetPos = targetPos; // зберігаємо позицію для наступного кадру
        }
    }
}
