using System;
using BaseInfrastructure;
using DG.Tweening;
using Services.AnimationService;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MainComponents.DraggableItems
{
    public class DraggableItemView : BaseView, IDraggableItemView, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private const float AnimationSpeed = 0.5f;
        
        public IObservable<Unit> OnDragBegin => _onBeginDrag;
        public IObservable<Unit> OnDragEnd => _onEndDrag;

        private readonly Subject<Unit> _onBeginDrag = new ();
        private readonly Subject<Unit> _onEndDrag = new ();

        private CanvasGroup _canvasGroup;
        private Image _image;
        private RectTransform _transform;
        private Transform _parent;
        private Vector3 _cashedPosition;

        private IAnimationService _animationService;
        
        public void Construct(IAnimationService animationService)
        {
            _animationService = animationService;
            
            _canvasGroup = GetComponent<CanvasGroup>();
            _image = GetComponent<Image>();
            _transform = GetComponent<RectTransform>();
            _parent = _transform.parent;
            _cashedPosition = _transform.localPosition;
        }

        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public void SetParent(Transform parent)
        {
            _transform.SetParent(parent);
        }

        public void SetSpriteWithAnimation(Sprite sprite)
        {
            var setSpriteAnimation = _animationService.SetupChangeSpriteAnimation(() => SetSprite(sprite), _transform, AnimationSpeed);
            setSpriteAnimation.Play();
        }

        public void RunDestroyAnimation(TweenCallback callback)
        {
            var destroyAnimation = _animationService.SetupDisposeAnimation(callback, _transform, AnimationSpeed);
            destroyAnimation.Play();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;
            _onBeginDrag?.OnNext(Unit.Default);
        }

        public void OnDrag(PointerEventData eventData)
        {
            ChangePosition(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
            _onEndDrag?.OnNext(Unit.Default);
            ReturnBack();
        }

        private void ReturnBack()
        {
            _transform.localPosition = _cashedPosition;
        }

        private void ChangePosition(Vector2 position)
        {
            _transform.position = position;
        }
    }
}