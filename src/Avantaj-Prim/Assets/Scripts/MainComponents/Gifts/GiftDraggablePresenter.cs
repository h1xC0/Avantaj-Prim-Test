using System;
using MainComponents.DraggableItems;
using MainComponents.Gifts.Models;
using Services.AnimationService;
using Services.InputService;
using UniRx;
using UnityEngine;

namespace MainComponents.Gifts
{
    public class GiftDraggablePresenter : DraggableItemPresenter
    {
        private readonly IAnimationService _animationService;
        public Gift Gift;

        public IObservable<Unit> OnGiftDestroy => _onGiftDestroy;
        private readonly Subject<Unit> _onGiftDestroy = new();

        public GiftDraggablePresenter(
            IDraggableItemView viewContract,
            IInputService inputService,
            Canvas canvas, IAnimationService animationService) 
            : base(viewContract, inputService, canvas, animationService)
        {
            _animationService = animationService;
            Gift = new Gift();
        }

        public void ChangeSprite(Sprite giftSprite, bool changeWithAnimation)
        {
            if (changeWithAnimation)
                View.SetSpriteWithAnimation(giftSprite);
            else
                View.SetSprite(giftSprite);
        }
        
        public void DestroyGift()
        {
            _onGiftDestroy?.OnNext(Unit.Default);
            View.Dispose();
            Dispose();
        }

        public void DestroyGiftWithAnimation()
        {
            View.RunDestroyAnimation(DestroyGift);
        }

        protected override void AddDraggableToService()
        {
            base.AddDraggableToService();
            View.SetParent(_canvas.transform);
        }
    }
}