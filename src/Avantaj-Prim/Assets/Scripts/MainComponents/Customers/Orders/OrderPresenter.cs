using System;
using BaseInfrastructure;
using MainComponents.Gifts;
using MainComponents.Gifts.Models;
using Services.InputService;
using UniRx;

namespace MainComponents.Customers.Orders
{
    public class OrderPresenter : BasePresenter<IOrderView>
    {
        public IObservable<Unit> OnOrderComplete => _onOrderComplete;

        private readonly IInputService _inputService;
        private readonly GiftRecipes _craftingRecipes;
        private readonly Order _order;

        private readonly Subject<Unit> _onOrderComplete = new();
        private int _currentOrderIndex = 0;

        public OrderPresenter(
            IOrderView viewContract,
            IInputService inputService,
            GiftRecipes craftingRecipes,
            Order order
            ) : base(viewContract)
        {
            _inputService = inputService;
            _craftingRecipes = craftingRecipes;
            _order = order;
            
            View.OnItemDropped
                .Subscribe(_ => CompareGiftToOrder())
                .AddTo(CompositeDisposable);

            RefreshOrderView(_order.OrderList[_currentOrderIndex]);
        }

        private void CompareGiftToOrder()
        {
            var giftDraggable = _inputService.CurrentGiftPartDraggableItem as GiftDraggablePresenter;
            if (giftDraggable is null) return;

            if (!giftDraggable.Gift.Compare(_order.OrderList[_currentOrderIndex]))
            {
                View.ShakeView();
                return;
            }
            
            giftDraggable.DestroyGift();
            PrepareNextOrder();
        }

        private void PrepareNextOrder()
        {
            _currentOrderIndex += 1;
            if (_currentOrderIndex >= _order.OrderList.Count)
            {
                _onOrderComplete?.OnNext(Unit.Default);
                DestroyOrder();
                return;
            }

            RefreshOrderView(_order.OrderList[_currentOrderIndex]);
        }

        private void RefreshOrderView(Gift currentOrder)
        {
            View.SetOrderView(_craftingRecipes.GetGiftSpriteByRecipe(currentOrder), currentOrder);
            View.SetOrdersCountText(_order.OrderList.Count - _currentOrderIndex);
        } 
            

        private void DestroyOrder()
        {
            Dispose();
        }

    }
}