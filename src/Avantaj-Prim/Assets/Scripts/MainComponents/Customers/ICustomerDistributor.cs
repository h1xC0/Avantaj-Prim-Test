using System;
using BaseInfrastructure;
using Services.EventBus;

namespace MainComponents.Customers
{
    public interface ICustomerDistributor : IDisposable, ISubscriber
    {
        void CreateCustomersAtStart(int count);
    }
}