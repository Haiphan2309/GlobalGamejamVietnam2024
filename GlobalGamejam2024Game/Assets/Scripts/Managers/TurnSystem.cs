using GDC.Enums;
using MainGame.Dialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using System;

namespace Level_1
{
    [Serializable]
    public class NPCDict:SerializableDictionaryBase<CharacterState,GameObject> { }
    public class TurnSystem : MonoBehaviour
    {
        public static TurnSystem Instance { get; private set; }
        bool catExist = false;
        CharacterState currentState;
        CharacterState CurrentState
        {
            get => currentState;
            set
            {
                if (currentState == value)
                    return;
                dict[currentState].SetActive(false);
                currentState = value;
                dict[currentState].SetActive(true);
                if (currentState == CharacterState.VERY_SAD) ;//lose
                if (currentState == CharacterState.LAUGH) ;//win
            }
        }
        [SerializeField] NPCDict dict;
        [SerializeField] GameObject idleCat, playCat, scaryCat;

        private void Awake()
        {
            Instance = this;
        }

        public void PickUpCard(CardType type)//call when a card is clicked
        {
            switch (type)
            {
                case CardType.CAT_PICTURE:
                    CatPicture();
                    break;
                case CardType.DOG_PICTURE:
                    DogPicture();
                    break;
                case CardType.MOUSE_PICTURE: 
                    MousePicture();
                    break;
                case CardType.PENGUIN_PICTURE:
                    PenguinPicture();
                    break;
                case CardType.MOUSE: 
                    Mouse();
                    break;
                case CardType.CAT_FOOD:
                    CatFood();
                    break;
                case CardType.BONE:
                    Bone();
                    break;
                case CardType.RICE:
                    Rice();
                    break;
                case CardType.WOOL:
                    Wool();
                    break;
                case CardType.SCARE_MASK:
                    ScareMask();
                    break;
                case CardType.MILK:
                    Milk();
                    break;
                case CardType.SOFA:
                    Sofa();
                    break;
            }
        }

        void CatPicture()
        {
            DialogueManager.Instance.StartDialogue("STORY TELLER", "You used a cat picture card.");
            if (currentState < CharacterState.LITTLE_HAPPY) 
                CurrentState++;
            
            switch (currentState)
            {
                case CharacterState.LITTLE_SAD:
                    DialogueManager.Instance.StartDialogue("STORY TELLER", "The man looks in the picture hang on the wall and thinks of something."); 
                    break;
                case CharacterState.NORMAL:
                    DialogueManager.Instance.StartDialogue("STORY TELLER", "The man looks in the picture hang on the wall and feels better.");
                    break;
                case CharacterState.LITTLE_HAPPY:
                    DialogueManager.Instance.StartDialogue("STORY TELLER", "The man looks in the picture hang on the wall and has a small smile.");
                    break;
            }
        }

        void DogPicture()
        {
            DialogueManager.Instance.StartDialogue("STORY TELLER", "You used a dog card.");
            if (currentState < CharacterState.NORMAL)
                CurrentState = CharacterState.VERY_SAD;
            else
                CurrentState -= 2;
            switch (currentState)
            {
                case CharacterState.VERY_SAD:
                    DialogueManager.Instance.StartDialogue("STORY TELLER", "The man looks in the picture hung on the wall and frightened.");
                    break;
                case CharacterState.SAD:
                    DialogueManager.Instance.StartDialogue("STORY TELLER", "The man looks in the picture hung on the wall and feels unhappy and little frightened.");
                    break;
                case CharacterState.LITTLE_SAD:
                    DialogueManager.Instance.StartDialogue("STORY TELLER", "The man looks in the picture hung on the wall and feels unhappy.");
                    break;
            }
        }

        void MousePicture()
        {
            DialogueManager.Instance.StartDialogue("STORY TELLER", "You used a mouse picture card.");
            DialogueManager.Instance.StartDialogue("STORY TELLER", "Nothing happened.");
        }

        void PenguinPicture()
        {
            DialogueManager.Instance.StartDialogue("STORY TELLER", "You used a penguin picture card.");
            DialogueManager.Instance.StartDialogue("STORY TELLER", "Nothing happened.");
        }    

        void Mouse()
        {
            DialogueManager.Instance.StartDialogue("STORY TELLER", "You used a mouse card.");
            if (!catExist)
            {
                CurrentState = ((currentState < CharacterState.LITTLE_SAD)) ? (currentState + 2) : CharacterState.LITTLE_HAPPY;
                catExist = true;
                DialogueManager.Instance.StartDialogue("STORY TELLER", "A wild cat senses the smell.\nThe man wants to adopt the cat but the cat seems not human-oriented so the man feels bored");
            }
            else
            {
                CurrentState = ((currentState < CharacterState.NORMAL)) ? (currentState + 2) : CharacterState.LAUGH;
                if (currentState== CharacterState.LAUGH)
                    DialogueManager.Instance.StartDialogue("STORY TELLER", "The cat sees the mouse and chases toward it happily.\nThe man feels amazing.");
                else
                DialogueManager.Instance.StartDialogue("STORY TELLER", "The cat sees the mouse and chases toward it happily.\nThe man feels amazing and LAUGH");
            }
        }

        void CatFood()
        {
            DialogueManager.Instance.StartDialogue("STORY TELLER", "You used a cat food card.");
            if (!catExist)
            {
                CurrentState = ((currentState < CharacterState.LITTLE_SAD)) ? (currentState + 2) : CharacterState.LITTLE_HAPPY;
                catExist = true;
                idleCat.SetActive(true);
                DialogueManager.Instance.StartDialogue("STORY TELLER", "A wild cat senses the smell.\nThe man wants to adopt the cat but the cat seems not human-oriented so the man feels bored.");
            }
            else
            {
                idleCat.SetActive(true);
                playCat.SetActive(false);
                scaryCat.SetActive(false);
                CurrentState--;
                DialogueManager.Instance.StartDialogue("STORY TELLER", "The cat sees the food but it doesn't seem hungry.\nThe man feels worried.");
            }
        }

        void Bone()
        {
            DialogueManager.Instance.StartDialogue("STORY TELLER", "You used a bone card.");
            DialogueManager.Instance.StartDialogue("STORY TELLER", "Nothing happened.");
        }

        void Rice()
        {
            DialogueManager.Instance.StartDialogue("STORY TELLER", "You used a rice card.");
            DialogueManager.Instance.StartDialogue("STORY TELLER", "Nothing happened.");
        }

        void Wool()
        {
            DialogueManager.Instance.StartDialogue("STORY TELLER", "You used a wool card.");
            if (!catExist)
            {
                DialogueManager.Instance.StartDialogue("STORY TELLER", "Nothing happened.");
            }
            else
            {
                CurrentState = ((currentState < CharacterState.NORMAL)) ? (currentState + 2) : CharacterState.LAUGH;
                idleCat.SetActive(false);
                playCat.SetActive(true);
                scaryCat.SetActive(false);
                if (currentState == CharacterState.LAUGH)
                    DialogueManager.Instance.StartDialogue("STORY TELLER", "The cat sees the wool roll and plays with it happily.\nThe man feels amazing.");
                else
                    DialogueManager.Instance.StartDialogue("STORY TELLER", "The cat sees the wool roll and plays with it happily.\nThe man feels amazing and LAUGH");
            }
        }

        void ScareMask()
        {
            DialogueManager.Instance.StartDialogue("STORY TELLER", "You used a scare mask card.");
            if (!catExist)
            {
                DialogueManager.Instance.StartDialogue("STORY TELLER", "Nothing happened.");
            }
            else
            {
                idleCat.SetActive(false);
                playCat.SetActive(false);
                scaryCat.SetActive(true);
                CurrentState = ((currentState > CharacterState.LITTLE_SAD)) ? CharacterState.LITTLE_SAD : CharacterState.VERY_SAD;
                if (currentState == CharacterState.LITTLE_SAD)
                    DialogueManager.Instance.StartDialogue("STORY TELLER", "The cat looks terrified when it sees the mask.\nThe man feels worried.");
                else
                    DialogueManager.Instance.StartDialogue("STORY TELLER", "The cat looks terrified when it sees the mask then it runs away.\nThe man feels VERY SAD");
            }
        }

        void Milk()
        {
            DialogueManager.Instance.StartDialogue("STORY TELLER", "You used a milk card.");
            if (!catExist)
            {
                CurrentState = CharacterState.LITTLE_HAPPY;
                catExist = true;
                idleCat.SetActive(true);
                DialogueManager.Instance.StartDialogue("STORY TELLER", "A wild cat senses the smell.\nThe man wants to adopt the cat but the cat seems not human-oriented so the man feels bored.");
            }
            else
            {
                idleCat.SetActive(true);
                playCat.SetActive(false);
                scaryCat.SetActive(false);
                CurrentState--;
                DialogueManager.Instance.StartDialogue("STORY TELLER", "The cat sees the food but it doesn't seem hungry.\nThe man feels worried.");
            }
        }

        void Sofa()
        {
            DialogueManager.Instance.StartDialogue("STORY TELLER", "You used a sofa card.");
            if (!catExist)
            {
                DialogueManager.Instance.StartDialogue("STORY TELLER", "Nothing happened.");
            }
            else
            {
                idleCat.SetActive(false);
                playCat.SetActive(true);
                scaryCat.SetActive(false);
                CurrentState = ((currentState >= CharacterState.NORMAL)) ? CharacterState.LITTLE_SAD : currentState - 1;
                if (currentState == CharacterState.LITTLE_SAD)
                    DialogueManager.Instance.StartDialogue("STORY TELLER", "The cat tears the sofa.\nThe room turns into a mess.\n The man feed disappointed.");
                else
                    DialogueManager.Instance.StartDialogue("STORY TELLER", "The cat tears the sofa.\nThe room turns into a mess.\n The man feed exhausted.");
            }
        }
    }
}

