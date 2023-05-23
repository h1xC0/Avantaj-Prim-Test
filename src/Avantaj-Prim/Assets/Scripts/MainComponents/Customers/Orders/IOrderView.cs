using System;
using BaseInfrastructure;
using MainComponents.Gifts.Models;
using UniRx;
using UnityEngine;

namespace MainComponents.Customers.Orders
{
    public interface IOrderView : IView
    {
        IObservable<Unit> OnItemDropped { get; }
        void SetOrderView(Sprite giftImage, Gift gift);
        void SetOrdersCountText(int ordersCount);
        void ShakeView();
    }
}