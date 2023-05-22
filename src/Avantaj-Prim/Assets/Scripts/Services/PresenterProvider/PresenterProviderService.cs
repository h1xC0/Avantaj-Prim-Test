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

        public void BindGameplayPresenter(GameplayPresenter gameplayPresenter) 
            => GameplayPresenter = gameplayPresenter;

        public void Dispose()
        {
            
        }
    }
}