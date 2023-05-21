using System;
using MainComponents.DraggableItems;

namespace Services.InputService
{
    public interface IInputService : IDisposable
    {
        DraggableItemPresenter CurrentGiftPartDraggableItem { get; }
        void SetCurrentDraggableItem(DraggableItemPresenter draggableItemView);
    }
}