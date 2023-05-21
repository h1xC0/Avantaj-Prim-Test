using BaseInfrastructure;
using Services;
using Services.AnimationService;
using Services.InputService;
using UniRx;
using UnityEngine;

namespace MainComponents.DraggableItems
{
    public class DraggableItemPresenter : BasePresenter<IDraggableItemView>
    {
        protected readonly IInputService _inputService;
        protected readonly Canvas _canvas;

        public DraggableItemPresenter(
            IDraggableItemView viewContract,
            IInputService inputService,
            Canvas canvas, IAnimationService animationService) : base(viewContract)
        {
            _inputService = inputService;
            _canvas = canvas;
            
            View.Construct(animationService);
            
            View.OnDragBegin
                .Subscribe(_ => AddDraggableToService())
                .AddTo(CompositeDisposable);

            View.OnDragEnd
                .Subscribe(_ => ResetDraggable())
                .AddTo(CompositeDisposable);
        }
        
        protected virtual void AddDraggableToService()
        {
            _inputService.SetCurrentDraggableItem(this);
        }

        protected virtual void ResetDraggable()
        {
            _inputService.SetCurrentDraggableItem(null);
        }

    }
}