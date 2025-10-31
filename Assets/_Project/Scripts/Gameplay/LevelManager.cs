using Project.Services.SceneManagement;
using UnityEngine;
using VContainer;

namespace Project.Gameplay
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float fallThreshold = -5f;

        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Update()
        {
            if (player.position.y < fallThreshold)
            {
                RestartLevel();
            }
        }

        public async void RestartLevel()
        {
            Debug.Log("[LevelManager] Restarting level...");
            await _sceneLoader.ReloadCurrentSceneAsync();
        }
    }
}
