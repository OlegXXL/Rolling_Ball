using UnityEngine;
using VContainer;
using Project.UI;
using Project.Player;

namespace Project.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class FinishZone : MonoBehaviour
    {
        private UIManager _uiManager;
        private PlayerController _playerController;
        [Inject]
        public void Construct(UIManager uiManager, PlayerController playerController)
        {
            _uiManager = uiManager;
            _playerController = playerController;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                FinishComplete();
                Debug.Log("[FinishZone] Level completed!");
            }
        }
        private void FinishComplete()
        {
            _playerController.Stop();
            _uiManager.ShowFinishScreen();
        }
    }
}
