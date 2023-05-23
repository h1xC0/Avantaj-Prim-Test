using System;
using System.Collections.Generic;

namespace Services.PresenterProvider
{
    public class PresenterContainerService : IPresenterContainerService
    {
        private Dictionary<Type, object> _presenterContainer = new Dictionary<Type, object>();

        public void BindPresenter<TPresenter>(TPresenter presenter) where TPresenter : class, IDisposable
        {
            var key = presenter.GetType();
            
            if (KeyExist(key) == false) 
                _presenterContainer.Add(key, presenter);
        }

        public TPresenter Resolve<TPresenter>() where TPresenter : class, IDisposable => 
            KeyExist(typeof(TPresenter)) ? (TPresenter)_presenterContainer[typeof(TPresenter)] : null;

        private bool KeyExist(Type key) => 
            _presenterContainer.ContainsKey(key);

        public void Dispose()
        {
            _presenterContainer.Clear();
        }
    }
}