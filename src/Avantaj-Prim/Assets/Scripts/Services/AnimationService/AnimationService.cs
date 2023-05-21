using DG.Tweening;
using UnityEngine;

namespace Services.AnimationService
{
    public class AnimationService : IAnimationService
    {
        public Sequence SetupDisposeAnimation(TweenCallback setSpriteCallBack, Transform target, float animationSpeed)
        {
            var sequence = DOTween.Sequence(target);
            sequence.SetTarget(target);
            sequence.SetAutoKill();

            var position = target.position;
            var parent = target.parent;
            
            sequence
                .Append(target.DOScale(Vector3.one * 0.1f, animationSpeed)
                .OnStart(() => 
                { 
                    target.position = position; 
                    target.SetParent(parent);
                })
                .OnComplete(setSpriteCallBack));

            return sequence;
        }
        
        public Sequence SetupChangeSpriteAnimation(TweenCallback setSpriteCallBack, Transform target, float animationSpeed)
        {
            var sequence = DOTween.Sequence(target);
            sequence.SetTarget(target);
            sequence.SetAutoKill();

            sequence.Append(target.DOScale(Vector3.one * 0.1f, animationSpeed).OnComplete(setSpriteCallBack));
            sequence.Insert(0f, target.DORotate(Vector3.forward * 180, animationSpeed, RotateMode.FastBeyond360));
            sequence.Append(target.DOScale(Vector3.one, animationSpeed));
            sequence.Insert(animationSpeed, target.DORotate(Vector3.zero, 0.5f));

            return sequence;
        }
    }
}