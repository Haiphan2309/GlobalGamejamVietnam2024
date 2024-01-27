using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoView : MonoBehaviour
{
    [SerializeField] private Image _cardIcon;
    [SerializeField] private TMP_Text _cardName;
    [SerializeField] private TMP_Text _cardDescriptionText;


    public void LoadInfo(SO_Card cardDescription)
    {
        _cardIcon.sprite = cardDescription.Icon;
        _cardName.text = cardDescription.Name;
        _cardDescriptionText.text = cardDescription.Description;

    }


}
