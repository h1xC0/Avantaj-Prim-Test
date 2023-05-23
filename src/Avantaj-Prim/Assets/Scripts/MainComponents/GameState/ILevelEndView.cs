using System;
using BaseInfrastructure;
using Services.AnimationService;
using Services.ResourceProvider;
using UniRx;

namespace GameState
{
    public interface ILevelEndView : IView, IResource
    {
        void Construct(IAnimationService animationService);
        IObservable<Unit> OnNextLevelButtonPressed { get; }
        void AnimateEnter();
    }
}