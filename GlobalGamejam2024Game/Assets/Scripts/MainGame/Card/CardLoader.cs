using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Scriptable_Objects;
using UnityEngine;

public class CardLoader : MonoBehaviour
{
    [SerializeField] private CardDescription _cardDescription;
    
    private CardView _cardView;
    private CardHoverInfoProvider _cardHoverInfoProvider;
    
    private void Awake()
    {
        _cardView = GetComponent<CardView>();
        _cardHoverInfoProvider = GetComponent<CardHoverInfoProvider>();
    }

    void Start()
    {
        _cardView.LoadCardView(_cardDescription);
        _cardHoverInfoProvider.SetCardDescription(_cardDescription);
            
    }
    
    
    
    
}
