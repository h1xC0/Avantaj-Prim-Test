using System;
using BaseInfrastructure;
using Constants;
using DG.Tweening;
using MainComponents.DraggableItems;
using MainComponents.Gifts.Models;
using Services.AnimationService;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MainComponents.Customers.Orders
{
    public class OrderView : BaseView, IOrderView, IDropHandler
    {
        public IObservable<Unit> OnItemDropped => _onItemDropped;

        [SerializeField] private Image orderImage;

        [System.Serializable]
        public struct GiftPartsImages
        {
            public Image BoxImage;
            public Image BowImage;
            public Image OrnamentImage;
        }

        [SerializeField] private GiftPartsImages _giftPartsImages;
        
        [SerializeField] private TMP_Text ordersCountText;

        private Subject<Unit> _onItemDropped = new();

        private IAnimationService _animationService;

        public void Construct(IAnimationService animationService)
        {
            _animationService = animationService;
        }

        public void SetOrderView(Sprite giftImage, Gift gift)
        {
            orderImage.sprite = giftImage;
            
            ChangeGiftPartSprite(gift.Box, _giftPartsImages.BoxImage);
            ChangeGiftPartSprite(gift.Bow, _giftPartsImages.BowImage);
            ChangeGiftPartSprite(gift.Ornament, _giftPartsImages.OrnamentImage);
        }

        public void SetOrdersCountText(int ordersCount)
        {
            ordersCountText.gameObject.SetActive(ordersCount > 1);
            ordersCountText.text = $"x{ordersCount}";
        }

        public void ShakeView()
        {
            var sequence = _animationService.SetupShakeSequence(transform, AnimationConstants.AnimationSpeed, AnimationConstants.ShakeStrength, AnimationConstants.ShakeVibrato);
            sequence.Play();
        }

        private void ChangeGiftPartSprite(GiftPartSO giftPart, Image image)
        {
            bool partExist = giftPart is not null;
            image.enabled = partExist;
            image.sprite = partExist ? giftPart.Sprite : null;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out DraggableItemView draggable))
            {
                _onItemDropped?.OnNext(Unit.Default);
            }
        }

    }
}