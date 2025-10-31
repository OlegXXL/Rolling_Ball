using System.Threading.Tasks;

namespace Project.Services.SceneManagement
{
    public interface ISceneLoader
    {
        /// <summary>
        /// Loads a scene asynchronously by name.
        /// </summary>
        Task LoadSceneAsync(string sceneName);

        /// <summary>
        /// Asynchronously reloads the current scene.
        /// </summary>
        Task ReloadCurrentSceneAsync();

        /// <summary>
        /// Loads a scene asynchronously and executes a callback upon completion.
        /// </summary>
        Task LoadSceneAsync(string sceneName, System.Action onLoaded);
    }
}
