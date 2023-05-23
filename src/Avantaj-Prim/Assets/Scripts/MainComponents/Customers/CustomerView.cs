using BaseInfrastructure;
using Constants;
using DG.Tweening;
using MainComponents.Customers.Orders;
using Services.AnimationService;
using UnityEngine;
using UnityEngine.UI;

namespace MainComponents.Customers
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CustomerView : BaseView, ICustomerView
    {
        private IAnimationService _animationService;

        [SerializeField] private ConstantGameResources constantGameResources;
        [SerializeField] private float amplitude;
        [SerializeField] private float duration;
        public OrderView OrderView => orderView;
        [SerializeField] private OrderView orderView;

        [SerializeField] private Image customerImage;
        [SerializeField] private float unsatisfiedLimit;

        [SerializeField] private Image timerFront;
        [SerializeField] private Image timerBack;

        [SerializeField] private Color normalColor;
        [SerializeField] private Color lateColor;

        private bool _unsatisfied;
        private CanvasGroup _canvasGroup;
            
        private Sequence _idleAnimation;

        private ConstantGameResources.CustomerSprites _randomCustomer;
        
        public void Construct(IAnimationService animationService)
        {
            _animationService = animationService;
            _canvasGroup = GetComponent<CanvasGroup>();

            timerFront.fillAmount = 1;
            _canvasGroup.alpha = 0f;

            _randomCustomer = constantGameResources.GetRandomCustomer();
            customerImage.sprite = _randomCustomer.satisfiedCustomer;
            customerImage.SetNativeSize();
        }

        public void SetTimer(float value)
        {
            timerFront.fillAmount = value;
            var timerColor = Color.Lerp(normalColor, lateColor, 1 - value);
            timerFront.color = timerColor;
            timerBack.color = timerColor;

            if (timerFront.fillAmount <= unsatisfiedLimit && _unsatisfied == false)
            {
                customerImage.sprite = _randomCustomer.unsatisfiedCustomer;
                _unsatisfied = true;
            }
        }

        public void ShowCustomerAnimation()
        {
            var showAnimation = _animationService.SetupMoveAnimation(transform, _canvasGroup,
                -AnimationConstants.CustomerAnimationPositionOffset, 1f, RunFloatingAnimation);
            showAnimation.Play();
        }

        public void HideCustomerAnimation(TweenCallback callback)
        {
            _idleAnimation.Kill();

            var hideAnimation = _animationService.SetupMoveAnimation(transform, _canvasGroup,
                AnimationConstants.CustomerAnimationPositionOffset, 0f, callback);
            hideAnimation.Play();
        }

        private void RunFloatingAnimation()
        {
            _idleAnimation = _animationService.SetupFloatingAnimation(transform, amplitude, duration);
            _idleAnimation.Play();
        }

        public void Dispose()
        {
            _animationService?.Dispose();
        }
    }
}
