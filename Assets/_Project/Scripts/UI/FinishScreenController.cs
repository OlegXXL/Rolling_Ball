using Project.Services.SceneManagement;
using Project.Services.SaveSystem;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Project.UI
{
    public class FinishScreenController : MonoBehaviour
    {
        [SerializeField] private Text coinsText;

        private ISaveService _saveService;
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISaveService saveService, ISceneLoader sceneLoader)
        {
            _saveService = saveService;
            _sceneLoader = sceneLoader;
        }

        public void UpdateFinishInfo()
        {
            int coins = _saveService.Load("coins", 0);
            coinsText.text = $"You collected: {coins} coins!";
        }

        public async void OnRestartButton()
        {
            await _sceneLoader.ReloadCurrentSceneAsync();
        }

        public async void OnMenuButton()
        {
            await _sceneLoader.LoadSceneAsync("MainMenu");
        }
    }
}
