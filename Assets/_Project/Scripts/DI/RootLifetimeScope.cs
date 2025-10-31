using VContainer;
using VContainer.Unity;
using static VContainer.Lifetime;
using Project.Services.SceneManagement;
using Project.Services.SaveSystem;

namespace Project.Lifetime
{
    /// <summary>
    /// Root lifetime scope for registering global services.
    /// </summary>
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ISceneLoader, SceneLoader>(Singleton);
            builder.Register<ISaveService, PlayerPrefsSaveService>(Singleton);
        }
    }
}