using System;
using BaseInfrastructure;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Services
{
    public interface IViewManagementFactory : IDisposable
    {
        void InstantiateView<TView>(TView view, Transform spawnPoint)
            where TView : Object, IView;

        void BindViewToPresenter<TPresenter>(IView view, TPresenter presenter) where TPresenter : BasePresenter<IView>;
    }
}