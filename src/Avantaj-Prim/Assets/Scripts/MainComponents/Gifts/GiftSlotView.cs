using System;
using BaseInfrastructure;
using Constants;
using MainComponents.DraggableItems;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MainComponents.Gifts
{
    public class GiftSlotView : BaseView, IGiftSlotView, IDropHandler
    {
        public IObservable<Unit> OnItemDropped => _onItemDropped;
        public IObservable<Unit> OnBuySlot { get; private set; }

        [SerializeField] private ConstantGameResources constantGameResources;
        [SerializeField] private Button buySlotButton;
        [SerializeField] private TMP_Text priceText;

        private Subject<Unit> _onItemDropped = new();
        [SerializeField] private Image slotImage;
        
        public override void Construct()
        {
            OnBuySlot = buySlotButton.OnClickAsObservable();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out DraggableItemView draggableItem))
            {
                _onItemDropped?.OnNext(Unit.Default);
            }
        }

        public void SetAvailability(bool state, int price)
        {
            slotImage.raycastTarget = state;
            slotImage.sprite = constantGameResources.GetGiftSlotSprite(state);
            
            buySlotButton.gameObject.SetActive(state == false);
            priceText.text = $"{price}<sprite=0>";
        }

        public DraggableItemView CreateViewForGift(DraggableItemView prefab)
        {
            return Instantiate(prefab, transform);
        }

    }
}