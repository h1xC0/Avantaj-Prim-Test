using System;
using BaseInfrastructure;
using Constants;
using MainComponents.Gifts;
using Services.AnimationService;
using Services.InputService;
using Services.ResourceProvider;
using UniRx;
using UnityEngine;

namespace MainComponents.DraggableItems
{
    public class DraggableItemContainerPresenter : BasePresenter<IDraggableItemContainerView>
    {
        private readonly GiftPartSO _generatedPart;
        private readonly IAnimationService _animationService;
        private readonly Canvas _gameplayCanvas;
        private readonly IInputService _inputService;
        private readonly IResourceProviderService _resourceProviderService;

        private GiftPartDraggablePresenter _currentGiftPartDraggable;
        private IDisposable _itemApplySubscription;
        
        public DraggableItemContainerPresenter(
            IDraggableItemContainerView viewContract,
            IInputService inputService,
            IResourceProviderService resourceProviderService,
            Canvas canvas,
            GiftPartSO giftPart, IAnimationService animationService) : base(viewContract)
        {
            _generatedPart = giftPart;
            _animationService = animationService;
            _inputService = inputService;
            _resourceProviderService = resourceProviderService;
            _gameplayCanvas = canvas;

            SetupGiftPartView();
        }
        
        private void SetupGiftPartView()
        {
            var giftPartPrefab = _resourceProviderService.LoadResource<DraggableItemView>(ResourceNames.DraggableItem);
            var giftView = View.CreateViewForGift(giftPartPrefab);
            _currentGiftPartDraggable = new GiftPartDraggablePresenter(giftView, _inputService, _generatedPart, _gameplayCanvas, _animationService);
            _itemApplySubscription = _currentGiftPartDraggable.OnItemApplied
                .Subscribe(_ => OnItemApplied());
        }

        private void OnItemApplied()
        {
            _itemApplySubscription?.Dispose();
            SetupGiftPartView();
        }
    }
}