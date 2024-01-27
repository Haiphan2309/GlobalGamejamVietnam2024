using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CardPaletteDescription", menuName = "ScriptableObjects/CardPaletteDescription", order = 1)]
public class CardPaletteDescription : ScriptableObject
{
    public Color CardImageBoxColor = Color.gray;
    public Color CardBorderColor = Color.white;
    public Color CardEffectBoxColor = Color.gray;
    public Color CardBannerBoxColor = Color.cyan;
        
    public Color CardNameTextColor = Color.black;
    public Color CardEffectTextColor = Color.white;
}