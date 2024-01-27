using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHoverInfoProvider : MonoBehaviour/*, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler*/
{
    private CardInfoController cardInfoController;

    private SO_Card _cardSo;
    
    
    private void Awake()
    {
        cardInfoController = FindObjectOfType<CardInfoController>();
    }
    
    public void SetCardDescription(SO_Card cardSo)
    {
        _cardSo = cardSo;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        cardInfoController.ShowCardInfo(_cardSo.Description);
        Debug.Log("Cursor Entering " + name + " GameObject");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cardInfoController.HideCardInfo();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked on " + name + " GameObject");
    }
}
