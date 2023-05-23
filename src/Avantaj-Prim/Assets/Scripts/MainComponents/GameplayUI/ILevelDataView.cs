using System;
using BaseInfrastructure;

namespace MainComponents.GameplayUI
{
    public interface ILevelDataView : IView
    {
        void SetCustomersCount(int value);
        void SetPlayerCurrency(int value);
        void SetLevelTimer(TimeSpan timeSpan);
    }
}