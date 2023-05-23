using BaseInfrastructure;
using Constants;
using MainComponents.Customers;
using MainComponents.DraggableItems;
using MainComponents.Gifts;
using MainComponents.Gifts.TrashBin;
using MainComponents.Level;
using Services.AnimationService;
using Services.EventBus;
using Services.InputService;
using Services.LevelConfigurationService;
using Services.LevelProgressionService;
using Services.PlayerProgression;
using Services.ResourceProvider;
using UnityEngine;

namespace MainComponents.Gameplay
{
    public class GameplayPresenter : BasePresenter<IGameplayView>
    {
        private readonly IInputService _inputService;
        private readonly IResourceProviderService _resourceProviderService;
        private readonly IAnimationService _animationService;
        private readonly ILevelConfigurationService _levelConfigurationService;
        private readonly IPlayerProgressionService _playerProgressionService;
        private readonly ILevelProgressionService _levelProgressionService;
        private readonly IEventBusService _eventBusService;

        private ICustomerFactory _customerFactory;
        private ICustomerDistributor _customerDistributor;
        
        private Canvas _mainCanvas;

        public GameplayPresenter(
            IGameplayView viewContract,
            IInputService inputService,
            IResourceProviderService resourceProviderService,
            IAnimationService animationService,
            ILevelConfigurationService levelConfigurationService,
            IPlayerProgressionService playerProgressionService,
            ILevelProgressionService levelProgressionService,
            IEventBusService eventBusService,
            Canvas canvas) : base(viewContract)
        {
            _inputService = inputService;
            _resourceProviderService = resourceProviderService;
            _animationService = animationService;
            _levelConfigurationService = levelConfigurationService;
            _playerProgressionService = playerProgressionService;
            _levelProgressionService = levelProgressionService;
            _eventBusService = eventBusService;

            _mainCanvas = canvas;
        }

        public void ConstructGameplay(LevelConfiguration levelConfiguration)
        {
            View.Construct(_mainCanvas);
            View.Hide();

            ConstructCustomerFactory(levelConfiguration);
            
            ConstructBoxVariants(levelConfiguration);
            ConstructBowVariants(levelConfiguration);
            ConstructsOrnamentVariants(levelConfiguration);
            ConstructGiftSlots();
            ConstructTrashBin();

            CreateCustomersAtStart();

            View.Show();
        }

        private void ConstructTrashBin() => 
            AddDisposable(new TrashBinPresenter(View.TrashBinView, _inputService));

        private void ConstructsOrnamentVariants(LevelConfiguration levelConfiguration)
        {
            for (int i = 0; i < levelConfiguration.AvailableOrnaments.Length; i++)
            {
                if (i >= View.DesignContainers.Length) break;
                var giftPart = levelConfiguration.AvailableOrnaments[i];
                var view = View.DesignContainers[i];

                AddDisposable(new DraggableItemContainerPresenter(view, _inputService, _resourceProviderService, _mainCanvas, giftPart, _animationService));
            }
        }

        private void ConstructBowVariants(LevelConfiguration levelConfiguration)
        {
            for (int i = 0; i < levelConfiguration.AvailableBows.Length; i++)
            {
                if (i >= View.BowContainers.Length) break;
                var giftPart = levelConfiguration.AvailableBows[i];
                var view = View.BowContainers[i];

                AddDisposable(new DraggableItemContainerPresenter(view, _inputService, _resourceProviderService,  _mainCanvas, giftPart, _animationService));
            }
        }

        private void ConstructBoxVariants(LevelConfiguration levelConfiguration)
        {
            for (int i = 0; i < levelConfiguration.AvailableBoxes.Length; i++)
            {
                if (i >= View.BoxContainers.Length) break;
                var giftPart = levelConfiguration.AvailableBoxes[i];
                var view = View.BoxContainers[i];

                AddDisposable(new DraggableItemContainerPresenter(view, _inputService, _resourceProviderService, _mainCanvas, giftPart, _animationService));
            }
        }
        
        private void ConstructGiftSlots()
        {
            for (int i = 0; i < _levelConfigurationService.GiftSlots.Length; i++)
            {
                if (i >= View.GiftSlots.Length) break;
                var view = View.GiftSlots[i];
                var giftSlotConfig = _levelConfigurationService.GiftSlots[i];
                
                AddDisposable(new GiftSlotPresenter(view, _levelConfigurationService.GiftRecipes, 
                    _inputService, _playerProgressionService,_animationService,_resourceProviderService, giftSlotConfig, _mainCanvas));
            }
        }
        
        private void ConstructCustomerFactory(LevelConfiguration levelConfiguration)
        {
            _customerFactory = CreateCustomerFactory(levelConfiguration);
            _customerDistributor = CreateCustomerDistributor();
            
            AddDisposable(_customerFactory);
            AddDisposable(_customerDistributor);
        }

        private void CreateCustomersAtStart()
        {
            _customerDistributor.CreateCustomersAtStart(View.CustomerSpawnPoints.Length);
        }

        private ICustomerFactory CreateCustomerFactory(LevelConfiguration levelConfiguration) =>
            new CustomerFactory(
                _levelConfigurationService.GiftRecipes,
                levelConfiguration,
                _inputService,
                _resourceProviderService,
                _animationService,
                View.CustomerSpawnPoints);

        private ICustomerDistributor CreateCustomerDistributor() =>
            new CustomerDistributor(
                _customerFactory, 
                _levelProgressionService, 
                _playerProgressionService,
                _eventBusService,
                _levelConfigurationService.Rewards);
    }
}