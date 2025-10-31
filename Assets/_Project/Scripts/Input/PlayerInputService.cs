// ...existing code...
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Input
{
    /// <summary>
    /// Service handling player input using the new Input System.
    /// WASD/Arrow keys for movement and mouse/touch drag for directional input.
    /// </summary>
    public class PlayerInputService : IPlayerInput, System.IDisposable
    {
        private readonly InputSystem_Actions _actions;

        public Vector2 MoveDirection { get; private set; }

        // Drag handling
        private InputAction _dragAction;
        private InputAction _dragPressAction;
        private bool _isDragging;
        private Vector2 _accumulatedDrag; // Accumulated delta since press start
        private float _dragSensitivity = 0.01f; // Tune: converts pixels -> input units
        private float _maxInputMagnitude = 1f;  // Clamp final MoveDirection magnitude

        public PlayerInputService()
        {
            _actions = new InputSystem_Actions();
            _actions.Enable();

            _actions.Player.Move.performed += OnMove;
            _actions.Player.Move.canceled += OnMove;

            // Use the action map to find actions (robust if generated class shape differs)
            var playerMap = _actions.Player.Get();
            _dragAction = playerMap.FindAction("Drag", throwIfNotFound: true);
            _dragPressAction = playerMap.FindAction("DragPress", throwIfNotFound: true);

            _dragAction.performed += OnDrag;
            _dragAction.canceled += OnDrag; // keep handling canceled in case device stops sending deltas
            _dragPressAction.started += OnDragPressStarted;
            _dragPressAction.canceled += OnDragPressCanceled;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            MoveDirection = context.ReadValue<Vector2>();
        }

        private void OnDrag(InputAction.CallbackContext context)
        {
            if (!_isDragging) return;

            Vector2 delta = context.ReadValue<Vector2>(); // pointer/touch delta in pixels this event
            _accumulatedDrag += delta;                     // build displacement from press start

            // Convert displacement to a directional input with tunable sensitivity and clamp
            float mag = _accumulatedDrag.magnitude * _dragSensitivity;
            Vector2 dir = _accumulatedDrag.normalized;
            Vector2 input = dir * Mathf.Min(mag, _maxInputMagnitude);

            // If displacement is almost zero, keep zero
            if (float.IsNaN(input.x) || float.IsInfinity(input.x)) input = Vector2.zero;

            MoveDirection = input;
        }

        private void OnDragPressStarted(InputAction.CallbackContext context)
        {
            _isDragging = true;
            _accumulatedDrag = Vector2.zero;
            MoveDirection = Vector2.zero;
        }

        private void OnDragPressCanceled(InputAction.CallbackContext context)
        {
            _isDragging = false;
            _accumulatedDrag = Vector2.zero;
            MoveDirection = Vector2.zero;
        }

        public void Dispose()
        {
            _actions.Player.Move.performed -= OnMove;
            _actions.Player.Move.canceled -= OnMove;

            if (_dragAction != null)
            {
                _dragAction.performed -= OnDrag;
                _dragAction.canceled -= OnDrag;
            }

            if (_dragPressAction != null)
            {
                _dragPressAction.started -= OnDragPressStarted;
                _dragPressAction.canceled -= OnDragPressCanceled;
            }

            _actions.Disable();
        }
    }

}
// ...existing code...