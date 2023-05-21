using System;
using MainComponents.Gameplay;

namespace Services.PresenterProvider
{
    public interface IPresenterProviderService : IDisposable
    {
        GameplayPresenter GameplayPresenter { get; }
        void BindGameplayPresenter(GameplayPresenter gameplayPresenter);
    }
}