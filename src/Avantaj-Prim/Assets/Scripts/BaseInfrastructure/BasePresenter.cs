using System;
using UniRx;

namespace BaseInfrastructure
{
    public class BasePresenter<TViewContract> : IDisposable where TViewContract : IBaseView
    {
        protected readonly TViewContract View;
        protected readonly CompositeDisposable CompositeDisposable;

        public BasePresenter(TViewContract viewContract)
        {
            View = viewContract;
            CompositeDisposable = new CompositeDisposable();
            
            View.Initialize();
        }

        public void AddDisposable(IDisposable disposable)
        {
            CompositeDisposable.Add(disposable);
        }

        public void Dispose()
        {
            CompositeDisposable?.Dispose();
        }
    }
}
