using System;
using BaseInfrastructure;
using MainComponents.DraggableItems;
using UniRx;
using UnityEngine.EventSystems;

namespace MainComponents.Gifts.TrashBin
{
    public class TrashBinView : BaseView, IDropHandler, ITrashBinView
    {
        public IObservable<Unit> OnItemDropped => _onItemDropped;
        private Subject<Unit> _onItemDropped = new();
        
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out DraggableItemView draggableItem))
            {
                _onItemDropped?.OnNext(Unit.Default);
            }
        }
    }
}