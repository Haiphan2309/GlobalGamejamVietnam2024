using System.Collections;
using System.Collections.Generic;
using MainGame.Card;
using UnityEngine;
using UnityEngine.UI;

public class CardHandController : MonoBehaviour
{
    [SerializeField] private LayoutGroup _layoutGroup;
    [SerializeField] private CardContentPeekView _cardContentPeekView;
    
    
    [SerializeField] private CardController _cardPrefab;
    
    private List<CardController> _cardControllers = new List<CardController>();
    public bool IsHandActive { get; private set; } = false;
        

    public void AddCard(SO_Card cardSo)
    {
        var cardController = Instantiate(_cardPrefab, _layoutGroup.transform);
        
        cardController.SetCard(cardSo);
        
        _cardControllers.Add(cardController);
    }
    
    
    public void ClearHand()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void Show()
    {
        IsHandActive = true;
        gameObject.SetActive(true);
        _cardContentPeekView.Show();
        
    }
    
    public void Hide()
    {
        IsHandActive = false;
        
        _cardContentPeekView.Hide();
        
    }
    
    
    public void DisableAllCards()
    {
        foreach (var cardController in _cardControllers)
        {
            cardController.DisableCard();
        }
    }
    
    public void EnableAllCards()
    {
        foreach (var cardController in _cardControllers)
        {
            cardController.EnableCard();
        }
    }
    
    public void RemoveCard(CardController cardController)
    {
        _cardControllers.Remove(cardController);
    }
    
    
    
    



}
