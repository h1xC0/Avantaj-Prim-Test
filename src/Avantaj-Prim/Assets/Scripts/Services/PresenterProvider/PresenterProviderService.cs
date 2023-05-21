using MainComponents.Gameplay;

namespace Services.PresenterProvider
{
    public class PresenterProviderService : IPresenterProviderService
    {
        public GameplayPresenter GameplayPresenter
        {
            get;
            private set;
        }

        public PresenterProviderService(GameplayPresenter gameplayPresenter)
        {
            GameplayPresenter = gameplayPresenter;
        }

        public void BindGameplayPresenter(GameplayPresenter gameplayPresenter) 
            => GameplayPresenter = gameplayPresenter;

        public void Dispose()
        {
            
        }
    }
}