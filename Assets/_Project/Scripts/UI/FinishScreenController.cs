using Project.Services.SceneManagement;
using Project.Services.SaveSystem;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using DG.Tweening;

namespace Project.UI
{
    public class FinishScreenController : MonoBehaviour
    {
        [SerializeField] private Text coinsText;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button menuButton;

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
            coinsText.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
            menuButton.gameObject.SetActive(false);
        }

        public void UpdateFinishInfo()
        {
            int coins = _saveService.Load("coins", 0);
            coinsText.text = $"You collected: {coins} coins!";
            PlayIntroAnimation();
        }
        /// <summary>
        /// Play intro animation for the finish screen elements
        /// </summary>
        private void PlayIntroAnimation()
        {
            coinsText.transform.localScale = Vector3.zero;
            restartButton.transform.localScale = Vector3.zero;
            menuButton.transform.localScale = Vector3.zero;

            coinsText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(true);

            Sequence introSequence = DOTween.Sequence();

            // Add animations in sequence with slight delays
            introSequence.Append(coinsText.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack))
                        .Append(restartButton.transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack))
                        .Append(menuButton.transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack));

            introSequence.Play();
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
