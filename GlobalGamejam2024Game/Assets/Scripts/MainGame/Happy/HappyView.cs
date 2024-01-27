using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappyView : MonoBehaviour
{
    
    [SerializeField] private Image _happyFiller;
    
    
    public void SetHappyFiller(float happyValue)
    {
        _happyFiller.fillAmount = happyValue;
    }
    
    
}
