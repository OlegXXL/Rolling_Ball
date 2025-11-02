using Project.Input;
using Project.Services.SaveSystem;
using System;
using UnityEngine;
using VContainer.Unity;

namespace Project.Player
{
    /// <summary>
    /// Handles player input and interaction logic.
    /// Captures input every frame and applies physics in FixedUpdate.
    /// Also manages coin collection and save data.
    /// </summary>
    public class PlayerPresenter : ITickable, IFixedTickable
    {
        private readonly IPlayerInput _input;
        private readonly PlayerController _controller;
        private readonly ISaveService _saveService;
        private readonly PlayerView _view;

        private Vector2 _pendingInput = Vector2.zero;

        public static event Action OnCoinCollected;

        public PlayerPresenter(
            IPlayerInput input,
            PlayerController controller,
            ISaveService saveService,
            PlayerView view)
        {
            _input = input;
            _controller = controller;
            _saveService = saveService;
            _view = view;

            // Subscribe to collision events from the view
            _view.OnPlayerTriggerEnter += HandleTriggerEnter;
        }

        // Capture player input (every frame)
        public void Tick()
        {
            _pendingInput = _input.MoveDirection;
        }

        // Apply physics (FixedUpdate)
        public void FixedTick()
        {
            _controller.Move(_pendingInput);
        }

        // Handle interactions with game objects
        private void HandleTriggerEnter(Collider other)
        {
            if (other.CompareTag("Coin"))
            {
                AddCoin();
            }
        }

        private void AddCoin()
        {
            int coins = _saveService.Load("coins", 0);
            coins++;
            _saveService.Save("coins", coins);
            Debug.Log($"[PlayerPresenter] Coin collected! Total coins: {coins}");
            
            // Notify UI that a coin was collected
            OnCoinCollected?.Invoke();
        }
    }
}
