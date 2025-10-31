using UnityEngine;
using DG.Tweening;

namespace Project.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class Coin : MonoBehaviour
    {
        [Header("Animation Settings")]
        [SerializeField] private float rotateSpeed = 90f;
        [SerializeField] private float floatAmplitude = 0.25f;
        [SerializeField] private float floatSpeed = 1f;
        [SerializeField] private float collectJumpHeight = 2f;
        [SerializeField] private float collectDuration = 0.4f;

        private Vector3 _startPos;
        private bool _isCollected;

        private void Start()
        {
            _startPos = transform.position;

            // Animation (up-down)
            transform.DOMoveY(_startPos.y + floatAmplitude, floatSpeed)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void Update()
        {
            // Constant rotation
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isCollected) return;

            if (other.CompareTag("Player"))
            {
                _isCollected = true;
                CollectAnimation();
            }
        }

        private void CollectAnimation()
        {
            // Stop floating
            DOTween.Kill(transform);

            // Jump up and destroy
            transform.DOJump(transform.position + Vector3.up * collectJumpHeight, 1f, 1, collectDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    Destroy(gameObject);
                });
        }
    }
}
