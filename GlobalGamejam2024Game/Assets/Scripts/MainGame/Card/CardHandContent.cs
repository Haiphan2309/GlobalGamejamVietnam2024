//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class CardHandContent : MonoBehaviour
//{
//    [SerializeField] private LayoutGroup _layoutGroup;
    
    
//    public void AddCardToHand(CardView card)
//    {
//        Transform transform1 = card.transform;
//        transform1.SetParent(transform);
//        transform1.localScale = Vector3.one;
//    }
    
//    public void RemoveCardFromHand(CardView card)
//    {
//        card.transform.SetParent(null);
//    }
    
//    public void RemoveCardFromHand(int index)
//    {
//        transform.GetChild(index).SetParent(null);
//    }
    
    
//    public void ClearHand()
//    {
//        foreach (Transform child in transform)
//        {
//            Destroy(child.gameObject);
//        }
//    }
    
    
    
    



//}
