using MainComponents.DraggableItems;

namespace Services.InputService
{
    public class InputService : IInputService
    {
        public DraggableItemPresenter CurrentGiftPartDraggableItem => _currentGiftPartDraggableItem;
        private DraggableItemPresenter _currentGiftPartDraggableItem;

        public void SetCurrentDraggableItem(DraggableItemPresenter current)
        {
            _currentGiftPartDraggableItem = current;
        }
        
        public void Dispose()
        {
            _currentGiftPartDraggableItem = null;
        }
    }
}