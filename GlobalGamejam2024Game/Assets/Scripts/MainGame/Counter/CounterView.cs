using DG.Tweening;
using TMPro;
using UnityEngine;

namespace MainGame.Counter
{
    public class CounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _counterText;
        [SerializeField] private string _counterName = "CARD TO USE: ";
        
        [SerializeField] private float _scaleDuration = 0.5f;
        [SerializeField] private float _scaleSize = 1.5f;
        [SerializeField] private Ease _scaleEase = Ease.OutElastic;
        
        [SerializeField] private RectTransform _popInTransform;
        [SerializeField] private RectTransform _popOutTransform;
        
        private Tween _scaleTween;
        private Tween _moveTween;
        
        
        public void SetCounter(int counter)
        {
            _counterText.text = _counterName + counter;
            
            AnimateText();
        }


        private void AnimateText()
        {
            
            _scaleTween?.Kill();
            _scaleTween = _counterText.transform.DOScale(_scaleSize, _scaleDuration).SetEase(_scaleEase).OnComplete(() =>
            {
                _counterText.transform.DOScale(1, _scaleDuration).SetEase(_scaleEase);
            });
            
            _moveTween?.Kill();
            _moveTween= _counterText.rectTransform.DOMove(_popOutTransform.position, _scaleDuration).SetEase(_scaleEase).OnComplete(() =>
            {
                _counterText.rectTransform.DOMove(_popInTransform.position, _scaleDuration).SetEase(_scaleEase);
            });
            
        }
        
 
        
    }
}