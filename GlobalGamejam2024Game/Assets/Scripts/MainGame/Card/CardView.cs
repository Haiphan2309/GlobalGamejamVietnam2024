using System.Collections;
using System.Collections.Generic;


using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    
    [SerializeField] private Image _cardIcon;
    [SerializeField] private TMP_Text _cardName;
    
    public void LoadCardView(SO_Card cardDescription)
    {
        _cardIcon.sprite = cardDescription.Icon;
        _cardName.text = cardDescription.Name;
        
    }
    
    
    
    
    
    
}
