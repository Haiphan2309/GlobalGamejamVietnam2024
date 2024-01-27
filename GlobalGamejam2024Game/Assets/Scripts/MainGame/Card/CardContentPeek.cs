using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MainGame.Card
{
    public class CardContentPeek : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RectTransform _peekTransform;
        [SerializeField] private RectTransform _showTransform;
        [SerializeField] private RectTransform _hideTransform;
        
        [SerializeField] private Ease _moveEase = Ease.OutCubic;
        [SerializeField] private float _moveDuration = 0.25f;
        
        private Tween _moveTween;
        public void OnPointerEnter(PointerEventData eventData)
        {
            _moveTween.Kill();
            _moveTween = _peekTransform.DOMoveY(_showTransform.position.y, _moveDuration).SetEase(_moveEase);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _moveTween.Kill();
            _moveTween = _peekTransform.DOMoveY(_hideTransform.position.y, _moveDuration).SetEase(_moveEase);
        }
    }
}