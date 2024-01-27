using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardInfoView : MonoBehaviour
{
    [SerializeField] private TMP_Text _descriptionText;
    
    public void LoadInfo(string description)
    {
        _descriptionText.text = description;
    }
    
    
    
}
