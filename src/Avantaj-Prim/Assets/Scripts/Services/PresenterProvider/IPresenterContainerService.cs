using System;
using BaseInfrastructure;
using GameState;
using MainComponents.Gameplay;

namespace Services.PresenterProvider
{
    public interface IPresenterContainerService : IDisposable
    {
        void BindPresenter<TPresenter>(TPresenter presenter) where TPresenter : class, IDisposable;
        TPresenter Resolve<TPresenter>() where TPresenter : class, IDisposable;
    }
}