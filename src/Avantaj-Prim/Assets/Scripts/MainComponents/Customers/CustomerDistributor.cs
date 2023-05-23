using System.Collections.Generic;
using Constants;
using Services.EventBus;
using Services.LevelProgressionService;
using Services.PlayerProgression;

namespace MainComponents.Customers
{
    public class CustomerDistributor : ICustomerDistributor
    {
        private readonly ICustomerFactory _customerFactory;
        private readonly ILevelProgressionService _levelProgressionService;
        private readonly IPlayerProgressionService _playerProgressionService;
        private readonly IEventBusService _eventBusService;
        private readonly Rewards _rewardList;
        private readonly List<CustomerPresenter> _customers = new();
        
        public CustomerDistributor(
            ICustomerFactory customerFactory,
            ILevelProgressionService levelProgressionService,
            IPlayerProgressionService playerProgressionService,
            IEventBusService eventBusService,
            Rewards rewardList)
        {
            _customerFactory = customerFactory;
            _levelProgressionService = levelProgressionService;
            _playerProgressionService = playerProgressionService;
            _eventBusService = eventBusService;
            _rewardList = rewardList;
            
            eventBusService.Subscribe(this);
        }

        public void CreateCustomersAtStart(int count)
        {
            for (int i = 0; i < count; i++)
                CreateNextCustomer();
        }

        private void TrackServedCustomer(CustomerPresenter customer, int ordersCount)
        {
            _levelProgressionService.CompleteCustomerOrder();
            _playerProgressionService.AddResources(_rewardList.GetRewardByOrderCount(ordersCount));
            RemoveCustomer(customer);
        }

        private void RemoveCustomer(CustomerPresenter customer)
        {
            UnsubscribeFromCustomerEvents(customer);
            _customers.Remove(customer);
            CreateNextCustomer();
        }

        private void CreateNextCustomer()
        {
            if (_levelProgressionService.CustomersCount.Value - _customers.Count <= 0) return;

            var customer = _customerFactory.CreateCustomer();
            if (customer is null) return;

            SubscribeOnCustomerEvents(customer);
            _customers.Add(customer);
        }

        private void SubscribeOnCustomerEvents(CustomerPresenter customer)
        {
            customer.OnCustomerOrderComplete += TrackServedCustomer;
            customer.OnCustomerTimeLeft += RemoveCustomer;
        }
        
        private void UnsubscribeFromCustomerEvents(CustomerPresenter customer)
        {
            customer.OnCustomerOrderComplete -= TrackServedCustomer;
            customer.OnCustomerTimeLeft -= RemoveCustomer;
        }
        
        public void Dispose()
        {
            foreach (var customer in _customers)
                UnsubscribeFromCustomerEvents(customer);
            
            _customers.Clear();
        }

    }
}