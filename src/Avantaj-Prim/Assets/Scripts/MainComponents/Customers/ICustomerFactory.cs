using System;

namespace MainComponents.Customers
{
    public interface ICustomerFactory : IDisposable
    {
        CustomerPresenter CreateCustomer();
    }
}