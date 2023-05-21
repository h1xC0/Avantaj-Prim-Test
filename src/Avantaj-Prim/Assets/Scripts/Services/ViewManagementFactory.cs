using BaseInfrastructure;
using UnityEngine;
using Zenject;

namespace Services
{
    public class ViewManagementFactory : IViewManagementFactory
    {
        private readonly DiContainer _container;

        public ViewManagementFactory(DiContainer container)
        {
            _container = container;
        }
        
        public void InstantiateView<TView>(TView view, Transform spawnPoint) where TView : Object, IView
        {
            _container.InstantiatePrefabForComponent<TView>(view, spawnPoint);
        }

        public void BindViewToPresenter<TPresenter>(IView view, TPresenter presenter) where TPresenter : BasePresenter<IView>
        {
            _container.BindInstance(_container.Instantiate<TPresenter>(new[] {view})).AsSingle();
        }

        public void Dispose()
        {
            
        }
    }
}