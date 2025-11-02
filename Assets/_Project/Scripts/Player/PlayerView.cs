using UnityEngine;
using System;

namespace Project.Player
{
    /// <summary>
    /// Holds player-related components and exposes movement parameters.
    /// Handles Rigidbody setup and trigger events.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerView : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        public Rigidbody Rigidbody => _rigidbody ??= GetComponent<Rigidbody>();

        [field: SerializeField, Range(5f, 50f)] public float MoveForce { get; private set; } = 15f;
        [field: SerializeField, Range(5f, 20f)] public float MaxVelocity { get; private set; } = 8f;
        [field: SerializeField, Range(0.1f, 1f)] public float AirControlMultiplier { get; private set; } = 0.3f;

        public event Action<Collider> OnPlayerTriggerEnter;

        private void OnTriggerEnter(Collider other)
        {
            OnPlayerTriggerEnter?.Invoke(other);
        }
    }
}
