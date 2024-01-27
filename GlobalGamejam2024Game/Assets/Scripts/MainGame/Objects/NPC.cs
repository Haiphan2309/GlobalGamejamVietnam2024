using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using GDC.Enums;
[System.Serializable]
public class StateInfo
{
    public Sprite sprite;
    public Vector2 position;
}
[System.Serializable]
public class StateDict : SerializableDictionaryBase<CharacterState,StateInfo> { }

public class NPC : MonoBehaviour
{
    public static NPC Instance {  get; private set; }

    CharacterState currentState;
    public CharacterState CurrentState
    {
        set
        {
            if (currentState == value)
                return;
            graphic.sprite = stateDict[value].sprite;
            transform.position = stateDict[value].position;
            currentState = value;
        }
    }
    [SerializeField] StateDict stateDict;
    [SerializeField] SpriteRenderer graphic;

    public void SetUp(CharacterState init)
    {
        graphic.sprite = stateDict[init].sprite;
        transform.position = stateDict[init].position;
        currentState = init;
    }

    private void Awake()
    {
        Instance = this;
        SetUp(CharacterState.LITTLE_SAD);
    }
}
