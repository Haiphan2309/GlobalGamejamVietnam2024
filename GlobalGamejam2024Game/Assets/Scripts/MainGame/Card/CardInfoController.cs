using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardInfoController : MonoBehaviour
{
    [SerializeField] private CardInfoView _cardInfoView;
    [SerializeField] private float _delayBeforeShowingCardInfo = 2;

    [SerializeField] private RectTransform _cardInfoTransform;
    [SerializeField] private RectTransform _showTransform;
    [SerializeField] private RectTransform _hideTransform;
    [SerializeField] private Ease _moveEase = Ease.OutCubic;
    [SerializeField] private float _moveDuration = 0.25f;
    
    private Tween _moveTween;
    
    
    private Coroutine delay;
    public void ShowCardInfo(SO_Card cardSo)
    {
        delay = StartCoroutine(ShowCardInfoCoroutine(cardSo));
    }
    
    public void HideCardInfo()
    {
        StopCoroutine(delay);
        
        _moveTween.Kill();
        _moveTween = _cardInfoTransform.transform.DOMove(_hideTransform.position, _moveDuration).SetEase(_moveEase)
            .OnComplete(() =>
            {
                _cardInfoView.gameObject.SetActive(false);
            });
    }
    
    IEnumerator ShowCardInfoCoroutine(SO_Card cardSo)
    {
        yield return new WaitForSeconds(_delayBeforeShowingCardInfo);
        _cardInfoView.gameObject.SetActive(true);
        _cardInfoView.LoadInfo(cardSo);
        
        _cardInfoTransform.transform.position = _hideTransform.position;
        
        _moveTween.Kill();
        _moveTween = _cardInfoTransform.transform.DOMove(_showTransform.position, _moveDuration).SetEase(_moveEase);
        
    }
    
    
}
