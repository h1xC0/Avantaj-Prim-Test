using BaseInfrastructure;
using Services.InputService;
using UniRx;

namespace MainComponents.Gifts.TrashBin
{
    public class TrashBinPresenter : BasePresenter<ITrashBinView>
    {
        private readonly IInputService _inputService;

        public TrashBinPresenter(
            ITrashBinView viewContract,
            IInputService inputService) 
            : base(viewContract)
        {
            _inputService = inputService;
            
            View.OnItemDropped
                .Subscribe(_ => OnItemDropped())
                .AddTo(CompositeDisposable);
        }

        private void OnItemDropped()
        {
            if (_inputService.CurrentGiftPartDraggableItem is not GiftDraggablePresenter draggable) return;
            draggable.DestroyGiftWithAnimation();
        }

    }
}