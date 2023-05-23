using System.Collections.Generic;
using System.Linq;
using Constants;
using MainComponents.Customers.Orders;
using MainComponents.Gifts.Models;
using MainComponents.Level;
using Services.AnimationService;
using Services.EventBus;
using Services.InputService;
using Services.ResourceProvider;
using UnityEngine;

namespace MainComponents.Customers
{
    public class CustomerFactory : ICustomerFactory
    {
        private readonly GiftRecipes _giftRecipes;
        private readonly LevelConfiguration _levelConfiguration;
        private readonly IInputService _inputService;
        private readonly IResourceProviderService _resourceProviderService;
        private readonly IAnimationService _animationService;
        private readonly IEventBusService _eventBusService;
        private readonly CustomerSpawnPoint[] _spawnPoints;

        public CustomerFactory(
            GiftRecipes giftRecipes,
            LevelConfiguration levelConfiguration,
            IInputService inputService,
            IResourceProviderService resourceProviderService,
            IAnimationService animationService,
            IEventBusService eventBusService,
            CustomerSpawnPoint[] spawnPoints)
        {
            _giftRecipes = giftRecipes;
            _levelConfiguration = levelConfiguration;
            _inputService = inputService;
            _resourceProviderService = resourceProviderService;
            _animationService = animationService;
            _eventBusService = eventBusService;
            _spawnPoints = spawnPoints;
        }

        public CustomerPresenter CreateCustomer()
        {
            var giftsCount = Random.Range(1, _levelConfiguration.Difficulty + 1);

            var spawnPoint = GetEmptySpawnPoint();
            if (spawnPoint is null) return null;

            var customerPrefab = _resourceProviderService.LoadResource<CustomerView>(ResourceNames.CustomerView);
            var customerView = GenerateCustomerView(spawnPoint, customerPrefab);
            if (customerView is null) return null;
            
            var customerOrder = new Order(GenerateCustomerOrder(giftsCount));
            if (customerOrder.OrderList is null)
            {
                Debug.LogError("Cannot create order, please check <color=red>Gift Recipes and Level Configuration</color>");
                return null;
            }

            var customer = new CustomerModel(customerOrder, _levelConfiguration.OrderWaitingTime);
            return new CustomerPresenter(customerView, _inputService, _eventBusService, _giftRecipes, customer, spawnPoint);
        }

        private CustomerSpawnPoint GetEmptySpawnPoint()
        {
            return _spawnPoints.FirstOrDefault(x => x.IsEmpty);
        }

        private CustomerView GenerateCustomerView(CustomerSpawnPoint spawnPoint, CustomerView customerView)
        {
            // var selectedView = _customerViews[Random.Range(0, _customerViews.Length)];
            spawnPoint.SetEmptyState(false);
            var instantiatedCustomer = Object.Instantiate(customerView, spawnPoint.transform);
            instantiatedCustomer.transform.localPosition = Vector3.zero;
            instantiatedCustomer.Construct(_animationService);
            
            return instantiatedCustomer;
        }

        private List<Gift> GenerateCustomerOrder(int giftsCount)
        {
            var gifts = new List<Gift>();
            for (int i = 0; i < giftsCount; i++)
            {
                var gift = GenerateCustomerOrder();
                if (gift is null) continue;
                
                gifts.Add(gift);
            }

            return gifts.Any() ? gifts : null;
        }
        
        private Gift GenerateCustomerOrder()
        {
            var box = _levelConfiguration.AvailableBoxes[Random.Range(0, _levelConfiguration.AvailableBoxes.Length)];
            var availableRecipe = GetAvailableGiftPartsForBox(box);

            if (availableRecipe is null) return null;
            
            var gift = new Gift();
            foreach (var part in availableRecipe.GiftParts)
                gift.ApplyGiftPart(part);

            return gift;
        }

        private GiftCraftingRecipe GetAvailableGiftPartsForBox(BoxModel box)
        {
            var availableParts = _levelConfiguration.AvailableBoxes
                .Select(x => x as GiftPartSO)
                .Concat(_levelConfiguration.AvailableBows)
                .Concat(_levelConfiguration.AvailableOrnaments)
                .ToArray();
            
            var availableRecipes = _giftRecipes.GetAvailableRecipes(box, availableParts);
            return availableRecipes.Any() ? availableRecipes[Random.Range(0, availableRecipes.Count)] : null;
        }

        public void Dispose()
        {
            _inputService?.Dispose();
        }
    }
}