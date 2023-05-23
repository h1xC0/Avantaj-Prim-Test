using System;
using BaseInfrastructure;
using Services.EventBus;

namespace MainComponents.Customers
{
    public interface ICustomerDistributor : IDisposable, ISubscriber
    {
        void CreateCustomersAtStart(int count);

        void FulfillCustomer(CustomerPresenter customer, int ordersCount);
        void RemoveCustomer(CustomerPresenter customer);
    }
}