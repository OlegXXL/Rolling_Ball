using Project.Input;
using Project.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static VContainer.Lifetime;
using Project.Services.SaveSystem;
using Project.UI;

namespace Project.Lifetime
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerView playerView;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private FinishScreenController finishScreenManager;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IPlayerInput, PlayerInputService>(Singleton);
            builder.Register<ISaveService, PlayerPrefsSaveService>(Scoped);
            builder.RegisterEntryPoint<PlayerPresenter>();
            builder.RegisterInstance(playerView);
            builder.RegisterInstance(uiManager);
            builder.RegisterInstance(finishScreenManager);

            builder.Register<PlayerController>(resolver =>
            {
                var view = resolver.Resolve<PlayerView>();
                return new PlayerController(view.Rigidbody, view.MoveSpeed, view.MaxVelocity);
            }, Singleton);
            
            Debug.Log("Configure");
        }
    }
}
