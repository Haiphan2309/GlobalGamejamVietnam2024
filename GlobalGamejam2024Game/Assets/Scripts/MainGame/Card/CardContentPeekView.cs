using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MainGame.Card
{
    public class CardContentPeekView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RectTransform _moveRectTransform;
        [SerializeField] private RectTransform _showTransform;
        [SerializeField] private RectTransform _peakTransform;
        [SerializeField] private RectTransform _hideTransform;
        
        [SerializeField] private Ease _moveEase = Ease.OutCubic;
        [SerializeField] private float _moveDuration = 0.25f;
        
        private Tween _moveTween;
        
        [SerializeField] private bool _isShowcase = false;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isShowcase)
            {
                return;
            }
            
            _moveTween?.Kill();
            _moveTween = _moveRectTransform.DOMoveY(_showTransform.position.y, _moveDuration).SetEase(_moveEase);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isShowcase)
            {
                return;
            }
            
            _moveTween?.Kill();
            _moveTween = _moveRectTransform.DOMoveY(_peakTransform.position.y, _moveDuration).SetEase(_moveEase);
        }
        
        public void Hide()
        {
            _isShowcase = false;
            
            _moveTween?.Kill();
            _moveTween = _moveRectTransform.DOMoveY(_hideTransform.position.y, _moveDuration).SetEase(_moveEase);
        }
        
        public void Show()
        {
            _isShowcase = true;
            
            _moveTween?.Kill();
            _moveTween = _moveRectTransform.DOMoveY(_peakTransform.position.y, _moveDuration).SetEase(_moveEase);
        }
        
    }
    
}