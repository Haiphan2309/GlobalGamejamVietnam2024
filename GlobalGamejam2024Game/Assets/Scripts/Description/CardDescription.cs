using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "CardDescription", menuName = "ScriptableObjects/CardDescription", order = 1)]
    public class CardDescription : ScriptableObject
    {
        public int CardID;
        public Sprite CardSprite;
        public string CardName;
        public string CardEffectDescription;
        public int CardCost;
        public int [] CardEffectIntVariables;
        
        public CardPaletteDescription CardPaletteDescription;
        
        
        
    }
}
