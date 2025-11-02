using Project.Services.SceneManagement;
using Project.Services.SaveSystem;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Project.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Text coinText;

        private ISceneLoader _sceneLoader;
        private ISaveService _saveService;

        [Inject]
        public void Construct(ISceneLoader sceneLoader, ISaveService saveService)
        {
            _sceneLoader = sceneLoader;
            _saveService = saveService;
        }

        private void Start()
        {
            playButton.onClick.AddListener(OnPlayClicked);
            exitButton.onClick.AddListener(OnExitClicked);
            UpdateCoinsUI();
        }

        private void UpdateCoinsUI()
        {
            if (_saveService == null) return;
            int coins = _saveService.Load("coins", 0);
            coinText.text = $"{coins}";
        }

            private async void OnPlayClicked()
            {
                await _sceneLoader.LoadSceneAsync("GameScene");
            }

            private void OnExitClicked()
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
}
