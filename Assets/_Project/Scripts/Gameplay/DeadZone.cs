using Project.Services.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Project.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class DeadZone : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ReloadSceneAfterDelay(0.5f);
            }
        }
        private void ReloadSceneAfterDelay(float delay)
        {
            Invoke(nameof(ReloadScene), delay);
        }

        private void ReloadScene()
        {
            _sceneLoader.ReloadCurrentSceneAsync();
        }

    }
}
