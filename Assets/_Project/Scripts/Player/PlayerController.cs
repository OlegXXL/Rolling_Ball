using UnityEngine;

namespace Project.Player
{
    /// <summary>
    /// Controller managing player movement and physics interactions.
    /// </summary>
    public class PlayerController
    {
        private readonly Rigidbody _rigidbody;
        private readonly float _moveSpeed;
        private readonly float _maxVelocity;

        public PlayerController(Rigidbody rigidbody, float moveSpeed = 5f, float maxVelocity = 10f)
        {
            _rigidbody = rigidbody;
            _moveSpeed = moveSpeed;
            _maxVelocity = maxVelocity;
        }

        public void Move(Vector2 input)
        {
            Debug.Log(_rigidbody);
            if (_rigidbody == null) return;

            Vector3 force = new Vector3(input.x, 0, input.y) * _moveSpeed;
            _rigidbody.AddForce(force, ForceMode.Force);

            if (_rigidbody.velocity.magnitude > _maxVelocity)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _maxVelocity;
            }
            Debug.Log("Moving");
        }
        /// <summary>
        /// Stops the player's movement.
        /// </summary>
        public void Stop()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}
