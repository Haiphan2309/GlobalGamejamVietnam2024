using System;
using System.Collections;
using System.Collections.Generic;
using Level_1;
using Level_2;
using MainGame;
using MainGame.Card;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    [SerializeField] private SO_Card _cardSo;
    [SerializeField] private Button _useButton;
    private CardView _cardView;
    private CardHoverInfoProvider _cardHoverInfoProvider;
    private CardHandSystem cardHandSystem;
    
    private ICardSkill _cardSkill;
    
    [SerializeField] private bool _isUsable = true;
    private void Awake()
    {
        _cardView = GetComponent<CardView>();
        _cardHoverInfoProvider = GetComponent<CardHoverInfoProvider>();
        
        _useButton.onClick.AddListener(UseCard);
        
        _cardSkill = GetComponent<ICardSkill>();
        
        cardHandSystem = FindObjectOfType<CardHandSystem>();
    }
    
    public void SetCard(SO_Card cardSo)
    {
        _cardSo = cardSo;
    }


    void Start()
    {
        _cardView.LoadCardView(_cardSo);
        _cardHoverInfoProvider.SetCardDescription(_cardSo);
            
    }
    
    private void UseCard()
    {
        StartCoroutine(nameof(UseCardCoroutine));
    }

    private IEnumerator UseCardCoroutine()
    {
        if (!_isUsable)
        {
            yield break;
        }
        
        cardHandSystem.DisableAllCards();

        TurnSystem.Instance?.PickUpCard(this._cardSo.CardType);
        TurnSystem1.Instance?.PickUpCard(this._cardSo.CardType);
        
        //GameLoopManager.Instance.UseCard(this, _cardSo);
        
        cardHandSystem.EnableAllCards();
        cardHandSystem.RemoveCard(this);
        _cardHoverInfoProvider.HideCardInfo();
        Destroy(gameObject);
    }


    public void DisableCard()
    {
        _isUsable = false;
    }

    public void EnableCard()
    {
        _isUsable = true;
    }
}
