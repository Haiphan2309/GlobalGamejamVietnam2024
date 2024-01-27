using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfoController : MonoBehaviour
{
    [SerializeField] private CardInfoView _cardInfoView;
    [SerializeField] private float _delayBeforeShowingCardInfo = 2;

    private Coroutine delay;
    public void ShowCardInfo(SO_Card cardSo)
    {
        delay = StartCoroutine(ShowCardInfoCoroutine(cardSo));
    }
    
    public void HideCardInfo()
    {
        StopCoroutine(delay);
        
        _cardInfoView.gameObject.SetActive(false);
    }
    
    IEnumerator ShowCardInfoCoroutine(SO_Card cardSo)
    {
        yield return new WaitForSeconds(_delayBeforeShowingCardInfo);
        _cardInfoView.gameObject.SetActive(true);
        _cardInfoView.LoadInfo(cardSo);
    }
    
    
}
