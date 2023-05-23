using System;
using System.Linq;
using BaseInfrastructure;
using Constants;
using MainComponents.DraggableItems;
using MainComponents.Gifts.Models;
using Services.AnimationService;
using Services.InputService;
using Services.PlayerProgression;
using Services.ResourceProvider;
using UniRx;
using UnityEngine;

namespace MainComponents.Gifts
{
    public class GiftSlotPresenter : BasePresenter<IGiftSlotView>
    {
        private readonly GiftRecipes _craftingRecipes;
        private readonly IInputService _inputService;
        private readonly IPlayerProgressionService _playerProgressService;
        private readonly IAnimationService _animationService;
        private readonly IResourceProviderService _resourceProviderService;
        private readonly GiftSlot _slot;
        private readonly Canvas _canvas;

        private GiftDraggablePresenter _generatedDraggableGift;
        private IDisposable _giftDestroySubscription;

        public GiftSlotPresenter(IGiftSlotView viewContract,
            GiftRecipes craftingRecipes,
            IInputService inputService,
            IPlayerProgressionService playerProgressService,
            IAnimationService animationService,
            IResourceProviderService resourceProviderService,
            GiftSlot slot,
            Canvas canvas) : base(viewContract)
        {
            _craftingRecipes = craftingRecipes;
            _inputService = inputService;
            _playerProgressService = playerProgressService;
            _animationService = animationService;
            _resourceProviderService = resourceProviderService;
            _slot = slot;
            _canvas = canvas;

            View.OnItemDropped
                .Subscribe(_ => OnItemDropped())
                .AddTo(CompositeDisposable);

            View.OnBuySlot
                .Subscribe(_ => TryBuySlot())
                .AddTo(CompositeDisposable);

            View.SetAvailability(_playerProgressService.GiftSlots.Contains(slot.Title), slot.Price);
        }

        private void OnItemDropped()
        {
            if (_inputService.CurrentGiftPartDraggableItem is not GiftPartDraggablePresenter draggable) return;
            var isBox = draggable.GiftPart is BoxModel;

            if (_generatedDraggableGift is null && isBox == false) return;

            if (_generatedDraggableGift is null) SetupGiftView();
            
            if (isBox && _generatedDraggableGift.Gift.Box != null) return;

            var gift = _generatedDraggableGift?.Gift;
            gift.ApplyGiftPart(draggable.GiftPart);

            var craftResult = _craftingRecipes.GetGiftSpriteByRecipe(gift);
            if (craftResult == null) return;

            _generatedDraggableGift.Gift.Copy(gift);
            draggable.ApplyItem();

            _generatedDraggableGift.ChangeSprite(craftResult, isBox == false);
        }

        private void SetupGiftView()
        {
            var giftPrefab = _resourceProviderService.LoadResource<DraggableItemView>(ResourceNames.DraggableItem);
            var giftView = View.CreateViewForGift(giftPrefab);
            _generatedDraggableGift = new GiftDraggablePresenter(giftView, _inputService, _canvas, _animationService);
            _giftDestroySubscription = _generatedDraggableGift.OnGiftDestroy
                .Subscribe(_ => OnGiftDestroyed());
        }

        private void OnGiftDestroyed()
        {
            _giftDestroySubscription?.Dispose();
            _generatedDraggableGift = null;
        }

        private void TryBuySlot()
        {
            if (_playerProgressService.SpendResources(_slot.Price) == false) return;
            _playerProgressService.BuyGiftSlots(_slot.Title);
            View.SetAvailability(true, _slot.Price);
        }
    }
}