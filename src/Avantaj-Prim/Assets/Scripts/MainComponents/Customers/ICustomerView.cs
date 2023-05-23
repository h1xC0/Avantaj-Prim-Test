using System;
using BaseInfrastructure;
using DG.Tweening;
using MainComponents.Customers.Orders;
using Services.AnimationService;
using Services.ResourceProvider;

namespace MainComponents.Customers
{
    public interface ICustomerView : IView, IResource
    {
        void Construct(IAnimationService animationService);
        OrderView OrderView { get; }
        void SetTimer(float value);
        void ShowCustomerAnimation();
        void HideCustomerAnimation(TweenCallback callback);

    }
}