using UnityEngine;
using VContainer;
using Project.UI;

namespace Project.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class FinishZone : MonoBehaviour
    {
        private UIManager _uiManager;

        [Inject]
        public void Construct(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _uiManager.ShowFinishScreen();
                Debug.Log("[FinishZone] Level completed!");
            }
        }
    }
}
