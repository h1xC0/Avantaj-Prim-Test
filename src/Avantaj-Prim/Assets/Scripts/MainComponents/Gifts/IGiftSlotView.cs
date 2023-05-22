using System;
using BaseInfrastructure;
using MainComponents.DraggableItems;
using UniRx;

namespace MainComponents.Gifts
{
    public interface IGiftSlotView : IView
    {
        IObservable<Unit> OnItemDropped { get; }
        IObservable<Unit> OnBuySlot { get; }

        void SetAvailability(bool state, int price);
        DraggableItemView CreateViewForGift(DraggableItemView prefab);
    }
}