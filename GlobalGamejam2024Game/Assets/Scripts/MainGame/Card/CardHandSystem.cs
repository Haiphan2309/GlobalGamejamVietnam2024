using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHandSystem : MonoBehaviour
{
    [SerializeField] private LayoutGroup _layoutGroup;
    
    [SerializeField] private CardController _cardPrefab;
    
    private List<CardController> _cardControllers = new List<CardController>();
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
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
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
