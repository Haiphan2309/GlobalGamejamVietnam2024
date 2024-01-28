using GDC.Enums;
using MainGame.Dialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using MainGame;
using MainGame.Happy;

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
                ScoreManager.Instance.InitializeHappy((int)currentState * 0.2f);
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
        [SerializeField] float timeGap = 1;

        private void Awake()
        {
            Instance = this;
            SetUp(CharacterState.LITTLE_SAD);
            DialogueManager.Instance.Hide();
            CardManager.Instance.Hide();
            StartCoroutine(Cor_Init());
        }

        void SetUp(CharacterState state)
        {
            currentState = state;
            dict[state].SetActive(true);
        }

        IEnumerator Cor_Init()
        {
            yield return new WaitForSeconds(2);
            DialogueManager.Instance.Show();
            DialogueManager.Instance.StartDialogue("Someone needs a LAUGH here.", "");
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.StartDialogue("I'm bored.\nI want to buy a cat but I don't have enough money...", "THE MAN");
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.Hide();
            CardManager.Instance.Show();
        }

        public void PickUpCard(CardType type)//call when a card is clicked
        {
            CardManager.Instance.Hide();
            DialogueManager.Instance.Show();
            switch (type)
            {
                case CardType.CAT_PICTURE:
                    StartCoroutine(Cor_CatPicture());
                    break;
                case CardType.DOG_PICTURE:
                    StartCoroutine(Cor_DogPicture());
                    break;
                case CardType.MOUSE_PICTURE:
                    StartCoroutine(Cor_MousePicture());
                    break;
                case CardType.PENGUIN_PICTURE:
                    StartCoroutine(Cor_PenguinPicture());
                    break;
                case CardType.MOUSE:
                    StartCoroutine(Cor_Mouse());
                    break;
                case CardType.CAT_FOOD:
                    StartCoroutine(Cor_CatFood());
                    break;
                case CardType.BONE:
                    StartCoroutine(Cor_Bone());
                    break;
                case CardType.RICE:
                    StartCoroutine(Cor_Rice());
                    break;
                case CardType.WOOL:
                    StartCoroutine(Cor_Wool());
                    break;
                case CardType.SCARY_MASK:
                    StartCoroutine(Cor_ScareMask());
                    break;
                case CardType.MILK:
                    StartCoroutine(Cor_Milk());
                    break;
                case CardType.SOFA:
                    StartCoroutine(Cor_Sofa());
                    break;
            }
        }

        IEnumerator Cor_CatPicture()
        {
            DialogueManager.Instance.StartDialogue("You used a CAT PICTURE card.", "");

            if (currentState < CharacterState.LITTLE_HAPPY) 
                CurrentState++;
            catPicture.SetActive(true);
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            switch (currentState)
            {
                case CharacterState.LITTLE_SAD:
                    DialogueManager.Instance.StartDialogue( "The man looks in the picture hang on the wall and thinks of something.",""); 
                    break;
                case CharacterState.NORMAL:
                    DialogueManager.Instance.StartDialogue( "The man looks in the picture hang on the wall and feels better." ,"");
                    break;
                case CharacterState.LITTLE_HAPPY:
                    DialogueManager.Instance.StartDialogue("The man looks in the picture hang on the wall and has a small smile.","");
                    break;
            }
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.Hide();
            CardManager.Instance.Show();
        }

        IEnumerator Cor_DogPicture()
        {
            DialogueManager.Instance.StartDialogue( "You used a DOG PICTURE card.","");
            if (currentState < CharacterState.NORMAL)
                CurrentState = CharacterState.VERY_SAD;
            else
                CurrentState -= 2;
            dogPicture.SetActive(true);
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            switch (currentState)
            {
                case CharacterState.VERY_SAD:
                    DialogueManager.Instance.StartDialogue("The man looks in the picture hung on the wall and frightened.","");
                    break;
                case CharacterState.SAD:
                    DialogueManager.Instance.StartDialogue("The man looks in the picture hung on the wall and feels unhappy and little frightened.","");
                    break;
                case CharacterState.LITTLE_SAD:
                    DialogueManager.Instance.StartDialogue( "The man looks in the picture hung on the wall and feels unhappy.","");
                    break;
            }
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.Hide();
            CardManager.Instance.Show();
        }

        IEnumerator Cor_MousePicture()
        {
            DialogueManager.Instance.StartDialogue("You used a MOUSE PICTURE card.","");
            mousePicture.SetActive(true); 
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.StartDialogue("Nothing happened.","");
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.Hide();
            CardManager.Instance.Show();
        }

        IEnumerator Cor_PenguinPicture()
        {
            DialogueManager.Instance.StartDialogue("You used a PENGUIN PICTURE card.","");
            penguinPicture.SetActive(true);
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.StartDialogue("Nothing happened.","");
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.Hide();
            CardManager.Instance.Show();
        }    

        IEnumerator Cor_Mouse()
        {
            DialogueManager.Instance.StartDialogue("You used a MOUSE card.","");
            if (!catExist)
            {
                CurrentState = ((currentState < CharacterState.LITTLE_SAD)) ? (currentState + 2) : CharacterState.LITTLE_HAPPY;
                catExist = true;
                playCat.SetActive(true);
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.StartDialogue("A wild cat senses the smell.\nThe man wants to adopt the cat but the cat seems not human-oriented so the man feels bored","");
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.Hide();
                CardManager.Instance.Show();
            }
            else
            {
                CurrentState = ((currentState < CharacterState.NORMAL)) ? (currentState + 2) : CharacterState.LAUGH;
                mouse.SetActive(true);
                idleCat.SetActive(false);
                playCat.SetActive(true);
                scaryCat.SetActive(false);
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                if (currentState== CharacterState.LAUGH)
                    DialogueManager.Instance.StartDialogue("The cat sees the mouse and chases toward it happily.\nThe man feels amazing.","");
                else
                    DialogueManager.Instance.StartDialogue("The cat sees the mouse and chases toward it happily.\nThe man feels amazing and LAUGH","");
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.Hide();
                CardManager.Instance.Show();
            }
        }

        IEnumerator Cor_CatFood()
        {
            DialogueManager.Instance.StartDialogue("You used a CAT FOOD card.","");
            if (!catExist)
            {
                CurrentState = ((currentState < CharacterState.LITTLE_SAD)) ? (currentState + 2) : CharacterState.LITTLE_HAPPY;
                catExist = true;
                idleCat.SetActive(true);
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.StartDialogue("A wild cat senses the smell.\nThe man wants to adopt the cat but the cat seems not human-oriented so the man feels bored.","");
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.Hide();
                CardManager.Instance.Show();
            }
            else
            {
                idleCat.SetActive(true);
                playCat.SetActive(false);
                scaryCat.SetActive(false);
                CurrentState--;
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.StartDialogue( "The cat sees the food but it doesn't seem hungry.\nThe man feels worried.","");
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.Hide();
                CardManager.Instance.Show();
            }
        }

        IEnumerator Cor_Bone()
        {
            DialogueManager.Instance.StartDialogue( "You used a BONE card.","");
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.StartDialogue( "Nothing happened.","");
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.Hide();
            CardManager.Instance.Show();
        }

        IEnumerator Cor_Rice()
        {
            DialogueManager.Instance.StartDialogue("You used a RICE card.","");
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.StartDialogue( "Nothing happened.","");
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.Hide();
            CardManager.Instance.Show();
        }

        IEnumerator Cor_Wool()
        {
            DialogueManager.Instance.StartDialogue( "You used a WOOL card.","");          
            if (!catExist)
            {
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.StartDialogue("Nothing happened.","");
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.Hide();
                CardManager.Instance.Show();
            }
            else
            {
                CurrentState = ((currentState < CharacterState.NORMAL)) ? (currentState + 2) : CharacterState.LAUGH;
                idleCat.SetActive(false);
                playCat.SetActive(true);
                scaryCat.SetActive(false);
                wool.SetActive(true);
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                if (currentState == CharacterState.LAUGH)
                    DialogueManager.Instance.StartDialogue( "The cat sees the wool roll and plays with it happily.\nThe man feels amazing.","");
                else
                    DialogueManager.Instance.StartDialogue("The cat sees the wool roll and plays with it happily.\nThe man feels amazing and LAUGH","");
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.Hide();
                CardManager.Instance.Show();
            }
        }

        IEnumerator Cor_ScareMask()
        {
            DialogueManager.Instance.StartDialogue("You used a SCARY MASK card.","");
            scaryMask.SetActive(true);
            if (!catExist)
            {
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.StartDialogue( "Nothing happened.","");
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.Hide();
                CardManager.Instance.Show();
            }
            else
            {
                idleCat.SetActive(false);
                playCat.SetActive(false);
                scaryCat.SetActive(true);
                CurrentState = ((currentState > CharacterState.LITTLE_SAD)) ? CharacterState.LITTLE_SAD : CharacterState.VERY_SAD;
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                if (currentState == CharacterState.LITTLE_SAD)
                    DialogueManager.Instance.StartDialogue("The cat looks terrified when it sees the mask.\nThe man feels worried.","");
                else
                    DialogueManager.Instance.StartDialogue("The cat looks terrified when it sees the mask then it runs away.\nThe man feels VERY SAD","");
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.Hide();
                CardManager.Instance.Show();
            }
        }

        IEnumerator Cor_Milk()
        {
            DialogueManager.Instance.StartDialogue( "You used a MILK card.","");
            if (!catExist)
            {
                CurrentState = CharacterState.LITTLE_HAPPY;
                catExist = true;
                idleCat.SetActive(true);
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.StartDialogue("A wild cat senses the smell.\nThe man wants to adopt the cat but the cat seems not human-oriented so the man feels bored.", "");
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.Hide();
                CardManager.Instance.Show();
            }
            else
            {
                idleCat.SetActive(true);
                playCat.SetActive(false);
                scaryCat.SetActive(false);
                CurrentState--;
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.StartDialogue( "The cat sees the food but it doesn't seem hungry.\nThe man feels worried.","");
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.Hide();
                CardManager.Instance.Show();
            }
        }

        IEnumerator Cor_Sofa()
        {
            DialogueManager.Instance.StartDialogue( "You used a SOFA card.","");
            if (!catExist)
            {
                sofa.SetActive(true);
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.StartDialogue( "Nothing happened.","");
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.Hide();
                CardManager.Instance.Show();
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
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                if (currentState == CharacterState.LITTLE_SAD)
                    DialogueManager.Instance.StartDialogue("The cat tears the sofa.\nThe room turns into a mess.\n The man feed disappointed.","");
                else
                    DialogueManager.Instance.StartDialogue( "The cat tears the sofa.\nThe room turns into a mess.\n The man feed exhausted.","");
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.Hide();
                CardManager.Instance.Show();
            }
        }
    }
}

