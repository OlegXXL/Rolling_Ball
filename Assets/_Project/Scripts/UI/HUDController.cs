using Project.Services.SaveSystem;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Project.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private Text coinText;

        private ISaveService _saveService;

        [Inject]
        public void Construct(ISaveService saveService)
        {
            _saveService = saveService;
        }

        private void Start()
        {
            UpdateCoinsUI();
        }

        public void UpdateCoinsUI()
        {
            int coins = _saveService.Load("coins", 0);
            coinText.text = $"Coins: {coins}";
        }
    }
}
