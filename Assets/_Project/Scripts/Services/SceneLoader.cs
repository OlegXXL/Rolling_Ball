using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Services.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        public async Task LoadSceneAsync(string sceneName)
        {
            Debug.Log($"[SceneLoader] Loading scene: {sceneName}");
            var operation = SceneManager.LoadSceneAsync(sceneName);
            if (operation == null)
            {
                Debug.LogError($"[SceneLoader] Scene '{sceneName}' not found!");
                return;
            }

            while (!operation.isDone)
                await Task.Yield();

            Debug.Log($"[SceneLoader] Scene '{sceneName}' loaded successfully.");
        }

        public async Task ReloadCurrentSceneAsync()
        {
            var current = SceneManager.GetActiveScene();
            Debug.Log($"[SceneLoader] Reloading scene: {current.name}");
            await LoadSceneAsync(current.name);
        }

        public async Task LoadSceneAsync(string sceneName, Action onLoaded)
        {
            await LoadSceneAsync(sceneName);
            onLoaded?.Invoke();
        }
    }
}
