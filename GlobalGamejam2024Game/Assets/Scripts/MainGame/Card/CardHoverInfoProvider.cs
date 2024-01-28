using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHoverInfoProvider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CardInfoController _cardInfoController;

    private SO_Card _cardSo;
    
    
    private void Awake()
    {
        _cardInfoController = FindObjectOfType<CardInfoController>();
    }
    
    public void SetCardDescription(SO_Card cardSo)
    {
        _cardSo = cardSo;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _cardInfoController.ShowCardInfo(_cardSo);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _cardInfoController.HideCardInfo();
    }


    public void HideCardInfo()
    {
        _cardInfoController.HideCardInfo();
    }
}
