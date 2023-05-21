using DG.Tweening;
using UnityEngine;

namespace Services.AnimationService
{
    public interface IAnimationService
    {
        Sequence SetupDisposeAnimation(TweenCallback setSpriteCallBack, Transform target, float animationSpeed);
        Sequence SetupChangeSpriteAnimation(TweenCallback setSpriteCallBack, Transform target, float animationSpeed);
    }
}