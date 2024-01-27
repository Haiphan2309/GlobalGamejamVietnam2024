using GDC.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Card", menuName = "ScriptableObjects/SO_Card")]
public class SO_Card : ScriptableObject
{
    public CardType CardType;
    public Sprite Icon;
    public string Name;
    public string Description;
    public string Dialogue;

}
