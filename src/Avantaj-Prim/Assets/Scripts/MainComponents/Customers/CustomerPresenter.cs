using System;
using BaseInfrastructure;
using MainComponents.Customers.Orders;
using MainComponents.Gifts.Models;
using Services.EventBus;
using Services.InputService;
using UniRx;
using UnityEngine;

namespace MainComponents.Customers
{
    public class CustomerPresenter : BasePresenter<ICustomerView>
    {
        public event Action<CustomerPresenter, int> OnCustomerOrderComplete; 
        public event Action<CustomerPresenter> OnCustomerTimeLeft; 

        private readonly IEventBusService _eventBusService;
        private readonly CustomerModel _customerModel;
        private readonly CustomerSpawnPoint _spawnPoint;
        private readonly IDisposable _orderCompleteSubscription;

        private IObservable<long> _timer;
        private float _currentTimerValue = 0f;

        public CustomerPresenter(
            ICustomerView viewContract,
            IInputService inputService,
            IEventBusService eventBusService,
            GiftRecipes giftRecipes,
            CustomerModel customerModel,
            CustomerSpawnPoint spawnPoint) : base(viewContract)
        {
            _eventBusService = eventBusService;
            _customerModel = customerModel;
            _spawnPoint = spawnPoint;

            var orderPresenter =
                new OrderPresenter(View.OrderView, inputService, giftRecipes, _customerModel.Order);

            _orderCompleteSubscription = orderPresenter.OnOrderComplete
                .Subscribe(_ => OnOrderComplete());

            View.ShowCustomerAnimation();
            SetupTimer();
        }

        private void UpdateTimer()
        {
            if (_currentTimerValue <= 0 && View != null)
            {
                OnTimeLeft();
                return;
            }

            _currentTimerValue -= Time.fixedDeltaTime;
            var step = (1 / _customerModel.OrderWaitingTime);
            var normalizedValue = _currentTimerValue * step;
            
            View.SetTimer(normalizedValue);
        }

        private void SetupTimer()
        {
            _currentTimerValue = _customerModel.OrderWaitingTime;
            _timer = Observable.Timer(TimeSpan.FromSeconds(Time.fixedDeltaTime));
            _timer
                .RepeatSafe()
                .Subscribe(_ => UpdateTimer())
                .AddTo(CompositeDisposable);
        }

        private void OnOrderComplete()
        {
            DisposeCustomerWithAnimation(LogOrderComplete);
        }

        private void OnTimeLeft()
        {
            DisposeCustomerWithAnimation(LogTimeLeft);
        }

        private void DisposeCustomerWithAnimation(Action action)
        {
            View.HideCustomerAnimation(() => DisposeCustomer(action));
        }

        private void DisposeCustomer(Action action)
        {
            _orderCompleteSubscription?.Dispose();
            EmptySpawnPoint();
            action.Invoke();

            View.Dispose();
            Dispose();
        }

        private void LogOrderComplete() =>
            OnCustomerOrderComplete?.Invoke(this, _customerModel.Order.OrderList.Count);
            
            private void LogTimeLeft() => 
                OnCustomerTimeLeft?.Invoke(this);

        private void EmptySpawnPoint()
        {
            _spawnPoint.SetEmptyState(true);
        }
    }
}