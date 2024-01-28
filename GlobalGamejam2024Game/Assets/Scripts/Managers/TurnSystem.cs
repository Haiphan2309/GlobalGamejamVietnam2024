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
                if (currentState == CharacterState.VERY_SAD) 
                    losePanel.SetActive(true);
                if (currentState == CharacterState.LAUGH)
                    winPanel.SetActive(true);
            }
        }
        [SerializeField] NPCDict dict;
        [SerializeField] GameObject idleCat, playCat, scaryCat;
        [SerializeField] GameObject winPanel, losePanel;
        [SerializeField] GameObject catPicture, dogPicture, mousePicture, penguinPicture, mouse, wool,scaryMask, sofa, damagedSofa;

        private void Awake()
        {
            Instance = this;
            SetUp(CharacterState.LITTLE_SAD);
        }

        void SetUp(CharacterState state)
        {
            currentState = state;
            dict[state].SetActive(true);
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
                case CardType.SCARY_MASK:
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
            DialogueManager.Instance.StartDialogue("", "You used a CAT PICTURE card.");
            if (currentState < CharacterState.LITTLE_HAPPY) 
                CurrentState++;
            catPicture.SetActive(true);
            switch (currentState)
            {
                case CharacterState.LITTLE_SAD:
                    DialogueManager.Instance.StartDialogue("", "The man looks in the picture hang on the wall and thinks of something."); 
                    break;
                case CharacterState.NORMAL:
                    DialogueManager.Instance.StartDialogue("", "The man looks in the picture hang on the wall and feels better.");
                    break;
                case CharacterState.LITTLE_HAPPY:
                    DialogueManager.Instance.StartDialogue("", "The man looks in the picture hang on the wall and has a small smile.");
                    break;
            }
        }

        void DogPicture()
        {
            DialogueManager.Instance.StartDialogue("", "You used a DOG PICTURE card.");
            if (currentState < CharacterState.NORMAL)
                CurrentState = CharacterState.VERY_SAD;
            else
                CurrentState -= 2;
            dogPicture.SetActive(true);
            switch (currentState)
            {
                case CharacterState.VERY_SAD:
                    DialogueManager.Instance.StartDialogue("", "The man looks in the picture hung on the wall and frightened.");
                    break;
                case CharacterState.SAD:
                    DialogueManager.Instance.StartDialogue("", "The man looks in the picture hung on the wall and feels unhappy and little frightened.");
                    break;
                case CharacterState.LITTLE_SAD:
                    DialogueManager.Instance.StartDialogue("", "The man looks in the picture hung on the wall and feels unhappy.");
                    break;
            }
        }

        void MousePicture()
        {
            DialogueManager.Instance.StartDialogue("", "You used a MOUSE PICTURE card.");
            mousePicture.SetActive(true);
            DialogueManager.Instance.StartDialogue("", "Nothing happened.");
        }

        void PenguinPicture()
        {
            DialogueManager.Instance.StartDialogue("", "You used a PENGUIN PICTURE card.");
            penguinPicture.SetActive(true);
            DialogueManager.Instance.StartDialogue("", "Nothing happened.");
        }    

        void Mouse()
        {
            DialogueManager.Instance.StartDialogue("", "You used a MOUSE card.");
            mouse.SetActive(true);
            if (!catExist)
            {
                CurrentState = ((currentState < CharacterState.LITTLE_SAD)) ? (currentState + 2) : CharacterState.LITTLE_HAPPY;
                catExist = true;
                DialogueManager.Instance.StartDialogue("", "A wild cat senses the smell.\nThe man wants to adopt the cat but the cat seems not human-oriented so the man feels bored");
            }
            else
            {
                CurrentState = ((currentState < CharacterState.NORMAL)) ? (currentState + 2) : CharacterState.LAUGH;
                if (currentState== CharacterState.LAUGH)
                    DialogueManager.Instance.StartDialogue("", "The cat sees the mouse and chases toward it happily.\nThe man feels amazing.");
                else
                DialogueManager.Instance.StartDialogue("", "The cat sees the mouse and chases toward it happily.\nThe man feels amazing and LAUGH");
            }
        }

        void CatFood()
        {
            DialogueManager.Instance.StartDialogue("", "You used a CAT FOOD card.");
            if (!catExist)
            {
                CurrentState = ((currentState < CharacterState.LITTLE_SAD)) ? (currentState + 2) : CharacterState.LITTLE_HAPPY;
                catExist = true;
                idleCat.SetActive(true);
                DialogueManager.Instance.StartDialogue("", "A wild cat senses the smell.\nThe man wants to adopt the cat but the cat seems not human-oriented so the man feels bored.");
            }
            else
            {
                idleCat.SetActive(true);
                playCat.SetActive(false);
                scaryCat.SetActive(false);
                CurrentState--;
                DialogueManager.Instance.StartDialogue("", "The cat sees the food but it doesn't seem hungry.\nThe man feels worried.");
            }
        }

        void Bone()
        {
            DialogueManager.Instance.StartDialogue("", "You used a BONE card.");
            DialogueManager.Instance.StartDialogue("", "Nothing happened.");
        }

        void Rice()
        {
            DialogueManager.Instance.StartDialogue("", "You used a RICE card.");
            DialogueManager.Instance.StartDialogue("", "Nothing happened.");
        }

        void Wool()
        {
            DialogueManager.Instance.StartDialogue("", "You used a WOOL card.");
            wool.SetActive(true);
            if (!catExist)
            {
                DialogueManager.Instance.StartDialogue("", "Nothing happened.");
            }
            else
            {
                CurrentState = ((currentState < CharacterState.NORMAL)) ? (currentState + 2) : CharacterState.LAUGH;
                idleCat.SetActive(false);
                playCat.SetActive(true);
                scaryCat.SetActive(false);
                if (currentState == CharacterState.LAUGH)
                    DialogueManager.Instance.StartDialogue("", "The cat sees the wool roll and plays with it happily.\nThe man feels amazing.");
                else
                    DialogueManager.Instance.StartDialogue("", "The cat sees the wool roll and plays with it happily.\nThe man feels amazing and LAUGH");
            }
        }

        void ScareMask()
        {
            DialogueManager.Instance.StartDialogue("", "You used a SCARY MASK card.");
            scaryMask.SetActive(true);
            if (!catExist)
            {
                DialogueManager.Instance.StartDialogue("", "Nothing happened.");
            }
            else
            {
                idleCat.SetActive(false);
                playCat.SetActive(false);
                scaryCat.SetActive(true);
                CurrentState = ((currentState > CharacterState.LITTLE_SAD)) ? CharacterState.LITTLE_SAD : CharacterState.VERY_SAD;
                if (currentState == CharacterState.LITTLE_SAD)
                    DialogueManager.Instance.StartDialogue("", "The cat looks terrified when it sees the mask.\nThe man feels worried.");
                else
                    DialogueManager.Instance.StartDialogue("", "The cat looks terrified when it sees the mask then it runs away.\nThe man feels VERY SAD");
            }
        }

        void Milk()
        {
            DialogueManager.Instance.StartDialogue("", "You used a MILK card.");
            if (!catExist)
            {
                CurrentState = CharacterState.LITTLE_HAPPY;
                catExist = true;
                idleCat.SetActive(true);
                DialogueManager.Instance.StartDialogue("", "A wild cat senses the smell.\nThe man wants to adopt the cat but the cat seems not human-oriented so the man feels bored.");
            }
            else
            {
                idleCat.SetActive(true);
                playCat.SetActive(false);
                scaryCat.SetActive(false);
                CurrentState--;
                DialogueManager.Instance.StartDialogue("", "The cat sees the food but it doesn't seem hungry.\nThe man feels worried.");
            }
        }

        void Sofa()
        {
            DialogueManager.Instance.StartDialogue("", "You used a SOFA card.");
            if (!catExist)
            {
                DialogueManager.Instance.StartDialogue("", "Nothing happened.");
                sofa.SetActive(true);
            }
            else
            {
                idleCat.SetActive(false);
                playCat.SetActive(true);
                scaryCat.SetActive(false);
                if (sofa.activeInHierarchy)
                { 
                    sofa.SetActive(false);
                    damagedSofa.SetActive(true);
                }
                CurrentState = ((currentState >= CharacterState.NORMAL)) ? CharacterState.LITTLE_SAD : currentState - 1;
                if (currentState == CharacterState.LITTLE_SAD)
                    DialogueManager.Instance.StartDialogue("", "The cat tears the sofa.\nThe room turns into a mess.\n The man feed disappointed.");
                else
                    DialogueManager.Instance.StartDialogue("", "The cat tears the sofa.\nThe room turns into a mess.\n The man feed exhausted.");
            }
        }
    }
}

