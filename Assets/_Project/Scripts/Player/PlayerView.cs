using UnityEngine;
using System;

namespace Project.Player
{
    public class PlayerView : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        public Rigidbody Rigidbody => _rigidbody ??= GetComponent<Rigidbody>();
        [field: SerializeField, Range(20f, 400f)] public float MoveSpeed { get; private set; } = 5f;
        [field: SerializeField, Range(20f, 800)] public float MaxVelocity { get; private set; } = 10f;
        // Event to notify Presenter
        public event Action<Collider> OnPlayerTriggerEnter;

        private void Awake()
        {
            // Smooth rendering between physics steps
            if (Rigidbody != null)
                Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        }
        private void OnTriggerEnter(Collider other)
        {
            OnPlayerTriggerEnter?.Invoke(other);
        }
    }
}