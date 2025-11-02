using Project.Player;
using Project.Services.SaveSystem;
using Project.Services.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Project.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private Text coinText;
        [SerializeField] private Button restartButton;

        private ISaveService _saveService;
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISaveService saveService, ISceneLoader sceneLoader)
        {
            _saveService = saveService;
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            UpdateCoinsUI();
            restartButton.onClick.AddListener(OnRestartButtonClicked);
            
            // Subscribe to coin collection event
            PlayerPresenter.OnCoinCollected += UpdateCoinsUI;
        }

        private void OnDestroy()
        {
            // Unsubscribe to avoid memory leaks
            PlayerPresenter.OnCoinCollected -= UpdateCoinsUI;
        }

        public void UpdateCoinsUI()
        {
            int coins = _saveService.Load("coins", 0);
            coinText.text = $"{coins}";
        }
        private void OnRestartButtonClicked()
        {
            _sceneLoader.ReloadCurrentSceneAsync();
        }
    }
}
