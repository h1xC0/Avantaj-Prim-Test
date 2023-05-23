using System;
using BaseInfrastructure;
using Constants;
using DG.Tweening;
using Services.AnimationService;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GameState
{
    public class LevelEndView : BaseView, ILevelEndView
    {
        public IObservable<Unit> OnNextLevelButtonPressed { get; private set; }

        [SerializeField] private Button nextLevelButton;

        private IAnimationService _animationService;
        
        public void Construct(IAnimationService animationService)
        {
            OnNextLevelButtonPressed = nextLevelButton.OnClickAsObservable();
            _animationService = animationService;
        }

        public void AnimateEnter()
        {
            var enterAnimation = _animationService.SetupEnterAnimation(transform, AnimationConstants.LevelAnimationPositionOffset, AnimationConstants.AnimationSpeed);
            enterAnimation.Play();
        }
    }
}