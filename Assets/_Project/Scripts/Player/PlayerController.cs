using UnityEngine;

namespace Project.Player
{
    /// <summary>
    /// Handles realistic player movement with Rigidbody physics.
    /// Uses AddForce for acceleration and limits control in the air.
    /// </summary>
    public class PlayerController
    {
        private readonly Rigidbody _rigidbody;
        private readonly float _moveForce;
        private readonly float _maxVelocity;
        private readonly float _airControlMultiplier;

        private bool _isGrounded;
        private bool _movementEnabled = true;           

        public PlayerController(
            Rigidbody rigidbody,
            float moveForce = 15f,
            float maxVelocity = 8f,
            float airControlMultiplier = 0.3f)
        {
            _rigidbody = rigidbody;
            _moveForce = moveForce;
            _maxVelocity = maxVelocity;
            _airControlMultiplier = airControlMultiplier;
        }

        public void Move(Vector2 input)
        {
            if (_rigidbody == null || !_movementEnabled) return;

            // Detect if player is grounded
            _isGrounded = Physics.Raycast(_rigidbody.position, Vector3.down, 1.05f);

            Vector3 moveDir = new Vector3(input.x, 0f, input.y).normalized;

            // Reduce control while not grounded
            float controlFactor = _isGrounded ? 1f : _airControlMultiplier;

            // Apply acceleration-based force
            _rigidbody.AddForce(moveDir * _moveForce * controlFactor, ForceMode.Acceleration);

            // Limit max horizontal velocity
            Vector3 horizontalVelocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            if (horizontalVelocity.magnitude > _maxVelocity)
            {
                Vector3 limitedVel = horizontalVelocity.normalized * _maxVelocity;
                _rigidbody.velocity = new Vector3(limitedVel.x, _rigidbody.velocity.y, limitedVel.z);
            }
        }

        /// <summary>
        /// Disables player movement completely
        /// </summary>
        public void DisableMovement()
        {
            _movementEnabled = false;
        }

        /// <summary>
        /// Enables player movement
        /// </summary>
        public void EnableMovement()
        {
            _movementEnabled = true;
        }

        /// <summary>
        /// Stops player movement completely and disables further movement
        /// </summary>
        public void Stop()
        {
            _movementEnabled = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.Sleep();
        }
    }
}
