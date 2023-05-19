using System;

namespace BaseInfrastructure
{
    public interface IBaseView : IDisposable
    {
        void Initialize();
        void DisposeView();
    }
}