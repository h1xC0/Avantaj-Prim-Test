using System;
using BaseInfrastructure;
using DG.Tweening;
using Services.AnimationService;
using Services.ResourceProvider;
using UniRx;
using UnityEngine;

namespace MainComponents.DraggableItems
{
    public interface IDraggableItemView : IView, IResource
    {
        void Construct(IAnimationService animationService);
        IObservable<Unit> OnDragBegin { get; }
        IObservable<Unit> OnDragEnd { get; }
        void SetSprite(Sprite sprite);
        void SetParent(Transform parent);
        void SetSpriteWithAnimation(Sprite sprite);
        void RunDestroyAnimation(TweenCallback callback);
    }
}