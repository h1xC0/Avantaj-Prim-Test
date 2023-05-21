using System;
using MainComponents.Gifts;
using Services;
using Services.AnimationService;
using Services.InputService;
using UniRx;
using UnityEngine;

namespace MainComponents.DraggableItems
{
    public class GiftPartDraggablePresenter : DraggableItemPresenter
    {
        public GiftPartSO GiftPart { get; private set; }
        public IObservable<Unit> OnItemApplied => _onItemApplied;
        private readonly Subject<Unit> _onItemApplied = new();

        public GiftPartDraggablePresenter(
            IDraggableItemView viewContract,
            IInputService inputService,
            GiftPartSO giftPart,
            Canvas canvas, IAnimationService animationService) 
            : base(viewContract, inputService, canvas, animationService)
        {
            GiftPart = giftPart;
            View.SetSprite(giftPart.Sprite);
        }

        protected override void AddDraggableToService()
        {
            base.AddDraggableToService();
            View.SetParent(_canvas.transform);
        }

        public void ApplyItem()
        {
            ResetDraggable();

            _onItemApplied?.OnNext(Unit.Default);
            View.Dispose();
            Dispose();
        }
    }
}