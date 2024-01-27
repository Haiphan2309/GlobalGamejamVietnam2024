using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfoController : MonoBehaviour
{
    [SerializeField] private CardInfoView cardInfoView;
    
    public void ShowCardInfo(string cardEffectDescription)
    {
        cardInfoView.gameObject.SetActive(true);
            
        cardInfoView.LoadInfo(cardEffectDescription);
    }
    
    public void HideCardInfo()
    {
        cardInfoView.gameObject.SetActive(false);
    }
    
    
    
    
}
